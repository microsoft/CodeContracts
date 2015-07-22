// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.CodeTools
{
    using System;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio.TextManager.Interop;
    using Microsoft.VisualStudio.CodeTools;
    using Microsoft.VisualStudio.Shell;
    using System.Timers;

    internal class TaskManager : ITaskManager
                      , IVsTaskProvider, IVsTaskProvider2, IVsTaskProvider3
                      , IVsRunningDocTableEvents
                      , IServiceProvider
                      , IVsTaskManager
                      , Microsoft.Build.Framework.ITaskHost
    {
        #region Fields
        private List<Task> tasks = new List<Task>();
        private string providerName;
        private bool released;

        private int refreshDelay;
        private Timer refreshTimer;
        private bool needRefreshPrevious;
        private bool needRefresh;

        public List<IClearTasksEvent> OnClearTasks = new List<IClearTasksEvent>();
        public const int DefaultRefreshDelay = 1000; // milliseconds

        public bool TrackMarkerEvents
        {
            get { return (refreshDelay > 0); }
        }
        #endregion

        #region Construction
        public TaskManager(string providerName) : this(providerName, DefaultRefreshDelay) { }

        public TaskManager(string providerName, int refreshDelay)
        {
            released = false;
            this.providerName = providerName;
            this.refreshDelay = refreshDelay;
            if (refreshDelay > 0 && refreshDelay < 500) this.refreshDelay = 500;

            if (refreshDelay > 0)
            {
                refreshTimer = new Timer(refreshDelay);
                refreshTimer.Elapsed += new ElapsedEventHandler(refreshTimer_Elapsed);
                refreshTimer.AutoReset = false; // one pulse only
            }

            InitializeTaskProvider();
            InitializeRunningDocTableEvents();
            InitializeOutputWindow();
        }


        ~TaskManager()
        {
            Release();
        }

        internal int sharedCount;

        #endregion

        #region Refresh timer
        internal void RefreshDelayed()
        {
            // called from Task's when text markers change
            Common.Trace("RefreshDelayed");
            needRefresh = true;
            if (!released && refreshTimer != null && !refreshTimer.Enabled)
            {
                refreshTimer.Start();
            }
        }

        private void refreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!released)
            {
                if (needRefresh)
                {
                    needRefreshPrevious = true;
                    needRefresh = false;    // cannot race as we are always in the UI thread
                    refreshTimer.Start(); // pulse again
                }
                else if (needRefreshPrevious)
                { // one cycle no change
                    Common.Trace("Refresh task list");
                    needRefreshPrevious = false;
                    refreshTimer.Stop();
                    Refresh();
                }
            }
        }
        #endregion

        #region ITaskManager
        public void Release()
        {
            if (!released)
            {
                released = true;
                if (refreshTimer != null)
                {
                    refreshTimer.Stop();
                    refreshTimer.Dispose();
                    refreshTimer = null;
                }
                ClearTasks();
                SolutionRelease();
                DisposeOutputWindow();
                DisposeTaskProvider();
                DisposeRunningDocTableEvents();
            }
        }


        public void AddTask(string description, string tipText, string code, string helpKeyword
                           , TaskPriority priority, TaskCategory category
                           , TaskMarker marker, Guid outputPane
                           , string projectName
                           , string fileName, int startLine, int startColumn, int endLine, int endColumn
                           , ITaskCommands commands
                           )
        {
            Common.ThreadSafeASync(delegate ()
            {
                // Add a new task
                Location span = new Location(projectName, fileName, startLine, startColumn, endLine, endColumn);
                Task task = new Task(this
                              , description, tipText, code, helpKeyword
                              , priority, category, marker
                              , span
                              , commands);
                tasks.Add(task);

                // show standard error in the build output pane
                if (outputPane != TaskOutputPane.None && description != null)
                {
                    string kind;
                    switch (category)
                    {
                        case TaskCategory.Error: kind = "error "; break;
                        case TaskCategory.Warning: kind = "warning "; break;
                        case TaskCategory.Message: kind = "message "; break;
                        default: kind = ""; break;
                    }
                    OutputString(span + ": " + kind + (code != null ? code : "") + ": " + description + "\n", outputPane);
                }
                RefreshDelayed();
            });
        }

        public void OutputString(string message, Guid outputPane)
        {
            if (message != null)
            {
                Common.ThreadSafeASync(delegate ()
                {
                    IVsOutputWindowPane pane = GetOutputPane(ref outputPane);
                    if (pane != null)
                    {
                        pane.OutputString(message);
                    }
                });
            }
        }

        public void Refresh()
        {
            Common.ThreadSafeASync(delegate ()
            {
                TaskListRefresh();
            });
        }

        private void InvokeHandlers(string command)
        {
            var handlers = OnClearTasks.ToArray();
            for (int i = 0; i < handlers.Length; i++)
            {
                var handler = handlers[i];
                try
                {
                    handler.Invoke(this, command);
                    handlers[i] = null;
                }
                catch
                {
                }
            }
            try
            {
                // now delete all handlers that failed
                for (int i = 0; i < handlers.Length; i++)
                {
                    var failed = handlers[i];
                    if (failed != null)
                    {
                        OnClearTasks.Remove(failed);
                    }
                }
            }
            catch { }
        }

        public void SolutionBuildStart()
        {
            Common.ThreadSafeInvoke(delegate () // synchronous to make sure processes die before we start building
            {
                InvokeHandlers("<buildStart>");
            });
        }


        public void SolutionBuildDone(bool success)
        {
            Common.ThreadSafeASync(delegate ()
            {
                InvokeHandlers((success) ? "<buildDone>" : "<buildFailed>");
            });
        }

        public void SolutionBuildCancel()
        {
            Common.ThreadSafeInvoke(delegate ()
            {
                InvokeHandlers("<buildCancel>");
            });
        }

        public void SolutionBuildCancelAsync()
        {
            Common.ThreadSafeASync(delegate ()
            {
                InvokeHandlers("<buildCancel>");
            });
        }

        public void SolutionRelease()
        {
            Common.ThreadSafeASync(delegate ()
            {
                InvokeHandlers("<release>");
            });
        }

        public void ClearTasks()
        {
            Common.ThreadSafeASync(delegate ()
            {
                InvokeHandlers("");
                foreach (Task t in tasks)
                {
                    if (t != null)
                    {
                        t.OnDeleteTask();
                    }
                }
                tasks.Clear();
                Refresh();
            });
        }

        public void ClearTasksOnSource(string projectName /* can be null */, string fileName)
        {
            if (fileName != null && fileName.Length > 0)
            {
                Common.ThreadSafeASync(delegate ()
                {
                    IVsHierarchy project = Common.GetProjectByName(providerName);
                    foreach (Task t in tasks)
                    {
                        if (t != null && t.IsSameFile(project, fileName))
                        {
                            t.OnDeleteTask();
                        }
                    }
                    tasks.RemoveAll(delegate (Task t) { return (t == null || t.IsSameFile(project, fileName)); });
                    Refresh();
                });
            }
        }

        public void ClearTasksOnSourceSpan(string projectName, string fileName
                                          , int startLine, int startColumn, int endLine, int endColumn)
        {
            if (fileName != null && fileName.Length > 0)
            {
                Common.ThreadSafeASync(delegate ()
                {
                    Location span = new Location(projectName, fileName, startLine, startColumn, endLine, endColumn);
                    foreach (Task t in tasks)
                    {
                        if (t != null && t.Overlaps(span))
                        {
                            t.OnDeleteTask();
                        }
                    }
                    tasks.RemoveAll(delegate (Task t) { return (t == null || t.Overlaps(span)); });
                    Refresh();
                });
            }
        }

        public void ClearTasksOnProject(string projectName)
        {
            Common.ThreadSafeASync(delegate ()
            {
                ClearTasksOnHierarchy(Common.GetProjectByName(projectName));
            });
        }

        internal void ClearTasksOnHierarchy(IVsHierarchy hier)
        {
            if (hier != null)
            {
                InvokeHandlers(Common.GetProjectName(hier));
                foreach (Task t in tasks)
                {
                    if (t != null && t.IsSameHierarchy(hier))
                    {
                        t.OnDeleteTask();
                    }
                }
                tasks.RemoveAll(delegate (Task t) { return (t == null ? false : t.IsSameHierarchy(hier)); });
                Refresh();
            }
        }

        internal void ClearTask(object task)
        {
            if (task != null)
            {
                int index = tasks.FindIndex(delegate (Task t) { return ((object)t == task); });
                if (index >= 0)
                {
                    Task t = tasks[index];
                    t.OnDeleteTask();
                    tasks.RemoveAt(index);
                    Refresh();
                }
            }
        }

        internal void RefreshTask(object taskObj)
        {
            // Refresh();
            if (taskObj != null)
            {
                Task task = tasks.Find(delegate (Task t) { return ((object)t == taskObj); });
                if (task != null)
                {
                    TaskListRefreshTask(task);
                }
            }
        }


        #endregion

        #region Output window
        private IVsOutputWindow outputWindow; // we cache the output window

        private IVsOutputWindowPane GetOutputPane(ref Guid outputPaneGuid)
        {
            if (outputPaneGuid != Guid.Empty)
            {
                if (outputWindow != null)
                {
                    IVsOutputWindowPane pane;
                    outputWindow.GetPane(ref outputPaneGuid, out pane);
                    return pane;
                }
            }
            return null;
        }

        private void InitializeOutputWindow()
        {
            try
            {  // sometimes failed on VS2010
                outputWindow = Common.GetService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            }
            catch
            {
                outputWindow = null;
            }
        }

        private void DisposeOutputWindow()
        {
            outputWindow = null;
        }

        #endregion

        #region IVsTaskProvider

        private uint taskListCookie;
        private IVsTaskList taskList;

        private void InitializeTaskProvider()
        {
            taskList = Common.GetService(typeof(SVsErrorList)) as IVsTaskList;
            if (taskList != null)
            {
                taskList.RegisterTaskProvider(this, out taskListCookie);
#if DEBUG
                System.Diagnostics.Debugger.Launch();
#endif
            }
        }

        private void DisposeTaskProvider()
        {
            ClearTasks();
            OnTaskListFinalRelease(taskList);
        }

        private void TaskListRefresh()
        {
            if (taskList != null && taskListCookie != 0)
            {
                taskList.RefreshTasks(taskListCookie);
            }
        }

        private void TaskListRefreshTask(Task task)
        {
            if (taskList != null && taskListCookie != 0 && task != null)
            {
                IVsTaskList2 taskList2 = taskList as IVsTaskList2;
                if (taskList2 != null)
                {
                    IVsTaskItem[] taskItems = { task };
                    taskList2.RefreshOrAddTasks(taskListCookie, 1, taskItems);
                }
            }
        }

        public int EnumTaskItems(out IVsEnumTaskItems ppenum)
        {
            // remove user deleted tasks 
            // tasks.RemoveAll( delegate( Task t ) { return (t==null || t.isDeleted); });
            ppenum = new TaskEnumerator(tasks);
            return 0;
        }

        public int ImageList(out IntPtr phImageList)
        {
            phImageList = IntPtr.Zero;
            return VSConstants.E_NOTIMPL;
        }

        public int OnTaskListFinalRelease(IVsTaskList pTaskList)
        {
            if (pTaskList != null)
            {
                if (taskListCookie != 0)
                {
                    pTaskList.UnregisterTaskProvider(taskListCookie);
                    taskListCookie = 0;
                    taskList = null;
                }
            }
            return 0;
        }

        public int ReRegistrationKey(out string pbstrKey)
        {
            pbstrKey = null;
            return VSConstants.E_NOTIMPL;
        }

        public int SubcategoryList(uint cbstr, string[] rgbstr, out uint pcActual)
        {
            pcActual = 0;
            if (cbstr != 0 || rgbstr != null)
            {
                if (rgbstr != null && rgbstr.Length > 0)
                {
                    rgbstr[0] = "Error";
                }
            }
            return 0;
        }

        #endregion

        #region IVsRunningDocTableEvents Members
        private IVsRunningDocumentTable rdt;
        private uint rdtEventsCookie; // = 0

        private void InitializeRunningDocTableEvents()
        {
            rdt = Common.GetService(typeof(SVsRunningDocumentTable)) as IVsRunningDocumentTable;
            if (rdt != null)
            {
                rdt.AdviseRunningDocTableEvents(this, out rdtEventsCookie);
            }
        }

        private void DisposeRunningDocTableEvents()
        {
            if (rdt != null && rdtEventsCookie != 0)
            {
                rdt.UnadviseRunningDocTableEvents(rdtEventsCookie);
                rdtEventsCookie = 0;
            }
            rdt = null;
        }

        private void OnOpenDocument(uint docCookie)
        {
            if (rdt != null)
            {
                uint flags;
                uint readLocks;
                uint editLocks;
                string fileName;
                IVsHierarchy hier;
                uint itemId;
                IntPtr docData;
                int hr = rdt.GetDocumentInfo(docCookie, out flags, out readLocks, out editLocks
                                        , out fileName, out hier, out itemId, out docData);
                if (hr == 0 && !docData.Equals(IntPtr.Zero))
                {
                    IVsTextLines textLines = System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(docData) as IVsTextLines;
                    if (textLines != null)
                    {
                        foreach (Task t in tasks)
                        {
                            t.OnOpenDocument(hier, itemId, fileName, textLines);
                        }
                    }
                }
            }
        }

        public int OnAfterFirstDocumentLock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            /*
            Common.Trace(string.Format("OnAfterFirstDocumentLock: {0} - {1} - {2} ", dwRDTLockType, dwReadLocksRemaining, dwEditLocksRemaining ));
            if (rdt != null && dwReadLocksRemaining == 1 && dwEditLocksRemaining==1) {
              OnOpenDocument(docCookie);
            }
             */
            return 0;
        }

        public int OnAfterAttributeChange(uint docCookie, uint grfAttribs)
        {
            /*
            Common.Trace("OnAfterAttributeChange: " + grfAttribs.ToString("X4"));
            if ((grfAttribs & (uint)__VSRDTATTRIB.RDTA_DocDataReloaded) == (uint)__VSRDTATTRIB.RDTA_DocDataReloaded)
            {
              OnOpenDocument(docCookie);
            }
             */
            return 0;
        }

        public int OnAfterDocumentWindowHide(uint docCookie, IVsWindowFrame pFrame)
        {
            return 0;
        }

        public int OnAfterSave(uint docCookie)
        {
            return 0;
        }

        public int OnBeforeDocumentWindowShow(uint docCookie, int fFirstShow, IVsWindowFrame pFrame)
        {
            // Common.Trace("OnBeforeDocumentWindowShow firstShow = " + fFirstShow);
            if (fFirstShow != 0)
            {
                OnOpenDocument(docCookie);
            }
            return 0;
        }

        public int OnBeforeLastDocumentUnlock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return 0;
        }

        #endregion

        #region IVsTaskProvider2

        public int MaintainInitialTaskOrder(out int bMaintainOrder)
        {
            bMaintainOrder = 0;
            return 0;
        }

        #endregion

        #region IVsTaskProvider3 Members

        public int GetColumn(int iColumn, VSTASKCOLUMN[] pColumn)
        {
            return VSConstants.E_NOTIMPL;
        }

        public int GetColumnCount(out int pnColumns)
        {
            pnColumns = 0;
            return VSConstants.E_NOTIMPL;
        }

        public int GetProviderFlags(out uint tpfFlags)
        {
            tpfFlags = 0;
            return VSConstants.E_NOTIMPL;
        }

        public int GetProviderGuid(out Guid pguidProvider)
        {
            pguidProvider = Guid.Empty;
            return VSConstants.E_NOTIMPL;
        }

        public int GetProviderName(out string pbstrName)
        {
            if (providerName != null)
            {
                pbstrName = providerName;
                return 0;
            }
            else
            {
                pbstrName = null;
                return VSConstants.E_NOTIMPL;
            }
        }

        public int GetProviderToolbar(out Guid pguidGroup, out uint pdwID)
        {
            pguidGroup = Guid.Empty;
            pdwID = 0;
            return VSConstants.E_NOTIMPL;
        }

        public int GetSurrogateProviderGuid(out Guid pguidProvider)
        {
            pguidProvider = Guid.Empty;
            return VSConstants.E_NOTIMPL;
        }

        public int OnBeginTaskEdit(IVsTaskItem pItem)
        {
            return 0;
        }

        public int OnEndTaskEdit(IVsTaskItem pItem, int fCommitChanges, out int pfAllowChanges)
        {
            pfAllowChanges = 1;
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            return Common.GetService(serviceType);
        }

        #endregion

        #region IVsTaskManager Members

        public object GetHierarchy(string projectName)
        {
            return Common.GetProjectByName(projectName);
        }

        public void AddClearTasksEvent(IClearTasksEvent handler)
        {
            OnClearTasks.Add(handler);
        }

        public void RemoveClearTasksEvent(IClearTasksEvent handler)
        {
            OnClearTasks.Remove(handler);
        }
        #endregion


    }

    internal class TaskEnumerator : IVsEnumTaskItems
    {
        private List<Task> tasks;
        private int pos;

        public TaskEnumerator(List<Task> tasks)
        {
            this.tasks = tasks;
            // pos = 0;
        }

        public virtual int Clone(out IVsEnumTaskItems ppenum)
        {
            ppenum = new TaskEnumerator(tasks);
            return 0;
        }

        public virtual int Next(uint celt, IVsTaskItem[] rgelt, uint[] pceltFetched)
        {
            uint fetched = 0;
            while (pos < tasks.Count && fetched < celt)
            {
                Task task = tasks[pos];
                if (task != null && task.IsVisible())
                {
                    if (rgelt != null && rgelt.Length > fetched)
                    {
                        rgelt[fetched] = task;
                    }
                    fetched++;
                }
                pos++;
            }
            if (pceltFetched != null && pceltFetched.Length > 0)
            {
                pceltFetched[0] = fetched;
            }
            return (fetched < celt ? VSConstants.S_FALSE : 0);
        }

        public virtual int Reset()
        {
            pos = 0;
            return 0;
        }

        public virtual int Skip(uint celt)
        {
            uint skipped = 0;
            while (pos < tasks.Count && skipped < celt)
            {
                Task task = tasks[pos];
                if (task != null && task.IsVisible())
                {
                    skipped++;
                }
                pos++;
            }
            return 0;
        }
    }
}
