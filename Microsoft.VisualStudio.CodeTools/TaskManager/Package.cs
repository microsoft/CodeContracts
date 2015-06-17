// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

namespace Microsoft.VisualStudio.CodeTools
{
  using System;
  using System.Runtime.InteropServices; 
  using Microsoft.VisualStudio.Shell.Interop;
  using Microsoft.VisualStudio.Shell;

  using Microsoft.VisualStudio.TextManager.Interop;
  using System.ComponentModel.Design;
  using Microsoft.VisualStudio.OLE.Interop;

  using System.Collections.Generic;
  using System.Threading;
  using System.Diagnostics;

  // The main package class
  #region Attributes
  [Microsoft.VisualStudio.Shell.DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\9.0")
  ,Microsoft.VisualStudio.Shell.ProvideLoadKey("Standard", "1.0", "Microsoft.VisualStudio.CodeTools.TaskManager", "Microsoft", 1)
  ,Microsoft.VisualStudio.Shell.ProvideMenuResource(1000, 1)
  ,Guid("DA85543E-97EC-4478-90EC-45CBCB4FA5C1")
  ,ComVisible(true)]
  #endregion
  internal sealed class TaskManagerPackage : Microsoft.VisualStudio.Shell.Package
                                , IVsSolutionEvents, IVsUpdateSolutionEvents2
                                , ITaskManagerFactory, IOleCommandTarget 
  {
    #region ITaskManagerFactory

    public ITaskManager CreateTaskManager(string providerName)
    {
      return new TaskManager(providerName); 
    }

    private IList<TaskManager> sharedTaskManagers;

    public ITaskManager QuerySharedTaskManager(string providerName, bool createIfAbsent)
    {
      foreach (TaskManager taskManager in sharedTaskManagers) {
        string name;
        taskManager.GetProviderName(out name);
        if (name == providerName) {
          Interlocked.Increment(ref taskManager.sharedCount);
          return taskManager;
        }
      }
      if (createIfAbsent) {
        TaskManager taskManager = new TaskManager(providerName);
        Interlocked.Increment(ref taskManager.sharedCount);
        sharedTaskManagers.Add(taskManager);
        return taskManager;
      }
      else
        return null;
    }

    public void ReleaseSharedTaskManager(ITaskManager itaskManager)
    {
      if (itaskManager == null) return;
      if (!(itaskManager is TaskManager)) return;
      TaskManager taskManager = (TaskManager)itaskManager;
      if (sharedTaskManagers.Contains(taskManager)) {
        Interlocked.Decrement(ref taskManager.sharedCount);
        if (taskManager.sharedCount == 0) {
          taskManager.Release();
          sharedTaskManagers.Remove(taskManager);
        }
      }      
    }

    public ITaskManager GetToolTaskManager(string toolName)
    {
      return tools.GetTaskManager(toolName);
    }

    #endregion

    #region Private fields
    private Tools tools;
    private RootMenu taskListMenu;
    #endregion

    #region Construction
    private IVsSolution solution;
    private uint solutionEventsCookie;
    private IVsSolutionBuildManager buildManager;
    private uint buildManagerCookie;
    private bool allTasksCleanedDuringThisSolutionUpdate;

    protected override void Initialize()
    {
      Common.Trace("Package intialize");
      base.Initialize();
      Common.Trace("Task manager.Initialize()");

      // Initialize common functionality
      Common.Initialize(this);
      taskListMenu = new RootMenu();
      
      // Initialize fields
      tools = new Tools();
      sharedTaskManagers = new List<TaskManager>();

      // Attach to solution events
      solution = GetService(typeof(SVsSolution)) as IVsSolution;
      if (solution == null) Common.Log("Could not get solution");
      solution.AdviseSolutionEvents(this as IVsSolutionEvents, out solutionEventsCookie);

      // Attach to build events
      buildManager = GetService(typeof(SVsSolutionBuildManager)) as IVsSolutionBuildManager;
      if (buildManager == null) Common.Log("Could not get build manager");
      buildManager.AdviseUpdateSolutionEvents(this, out buildManagerCookie);

      // Add a TaskManagerFactory service
      ITaskManagerFactory factory = this as ITaskManagerFactory;
      (this as System.ComponentModel.Design.IServiceContainer).AddService(typeof(ITaskManagerFactory), factory, true);

      // Set PID for msbuild tasks
      Environment.SetEnvironmentVariable( "VSPID", System.Diagnostics.Process.GetCurrentProcess().Id.ToString() );
    }

    protected override void Dispose(bool disposing)
    {
      Common.Trace("Package release start");

      if (disposing) {
        if (tools != null)
        {
          tools.Release();
          tools = null;
        }
        if (sharedTaskManagers != null)
        {
          sharedTaskManagers.Clear();
          sharedTaskManagers = null;
        }
        if (buildManager != null) {
          buildManager.UnadviseUpdateSolutionEvents(buildManagerCookie);
          buildManager = null;
        }
        
        if (solution != null) {
          solution.UnadviseSolutionEvents(solutionEventsCookie);
          solution = null;
        }
        if (taskListMenu != null)
        {
          taskListMenu.Release();
          taskListMenu = null;
        }
        Common.Release();
        GC.SuppressFinalize(this);
      }
      base.Dispose(disposing);
      Common.Trace("Package release done");
    } 
    #endregion

    #region IVsSolutionEvents Members

    // We watch solution events to automatically set the host objects
    // for newly loaded/opened projects
    int IVsSolutionEvents.OnAfterCloseSolution(object pUnkReserved)
    {
      return 0;
    }

    int IVsSolutionEvents.OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
    {
      var start = DateTime.Now;
      tools.ProjectSetHostObjects(pRealHierarchy);
      Debug.WriteLine("OnAfterLoadProject: {0}", ElapsedSince(start));
      return 0;
    }

    int IVsSolutionEvents.OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
    {
      var start = DateTime.Now;
      tools.ProjectSetHostObjects(pHierarchy);
      Debug.WriteLine("OnAfterOpenProject: {0}", ElapsedSince(start));
      /*
      object objClsids;
      int hr = pHierarchy.GetProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID2.VSHPROPID_CfgPropertyPagesCLSIDList, out objClsids);
      ErrorHandler.ThrowOnFailure(hr);
      string clsids = objClsids as String;
      clsids = clsids + ";{35A69422-A11A-4ce8-8962-061DFABB02EB}";
      hr = pHierarchy.SetProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID2.VSHPROPID_CfgPropertyPagesCLSIDList, clsids);
      ErrorHandler.ThrowOnFailure(hr);
      */
      return 0;
    }

    int IVsSolutionEvents.OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
    {
      var start = DateTime.Now;
      tools.RefreshTasks();
      Debug.WriteLine("OnAfterOpenSolution: {0}", ElapsedSince(start));
      return 0;
    }

    int IVsSolutionEvents.OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
    {
      if (this.allTasksCleanedDuringThisSolutionUpdate) return 0;
      var start = DateTime.Now;
      tools.ClearTasksOnHierarchy(pHierarchy);
      Debug.WriteLine("OnBeforeCloseProject: {0}", ElapsedSince(start));
      return 0;
    }

    int IVsSolutionEvents.OnBeforeCloseSolution(object pUnkReserved)
    {
      this.allTasksCleanedDuringThisSolutionUpdate = true;
      var start = DateTime.Now;
      tools.ClearTasks();
      tools.SolutionRelease();
      Debug.WriteLine("OnBeforeCloseSolution: {0}", ElapsedSince(start));
      return 0;
    }

    int IVsSolutionEvents.OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
    {
      return 0;
    }

    int IVsSolutionEvents.OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
    {
      return 0;
    }

    int IVsSolutionEvents.OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
    {
      return 0;
    }

    int IVsSolutionEvents.OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
    {
      return 0;
    }

    #endregion

    #region IVsUpdateSolutionEvents Members

    // We watch build to automatically clear and refresh the tool lists
    public int OnActiveProjectCfgChange(IVsHierarchy pIVsHierarchy)
    {
      if (this.allTasksCleanedDuringThisSolutionUpdate) { return 0; }
      this.allTasksCleanedDuringThisSolutionUpdate = true;
      var start = DateTime.Now;
      tools.ClearTasks(); // amortize configuration changings for entire solution
      tools.SolutionBuildCancelAsync();
      Debug.WriteLine("OnActiveProjectCfgChange: {0}", ElapsedSince(start));
      return 0;
    }

    public int UpdateSolution_Begin(ref int pfCancelUpdate)
    {
      pfCancelUpdate = 0;

      this.allTasksCleanedDuringThisSolutionUpdate = false;
      var start = DateTime.Now;
      tools.SolutionBuildStart();
      Debug.WriteLine("UpdateSolution_Begin: {0}", ElapsedSince(start));
      return 0;
    }

    private string ElapsedSince(DateTime start)
    {
      var now = DateTime.Now;
      return (now - start).Milliseconds.ToString() + "ms";
    }

    public int UpdateSolution_Cancel()
    {
      var start = DateTime.Now;
      tools.SolutionBuildCancel();
      Debug.WriteLine("UpdateSolution_Cancel: {0}", ElapsedSince(start));
      return 0;
    }

    public int UpdateSolution_Done(int fSucceeded, int fModified, int fCancelCommand)
    {
      var start = DateTime.Now;
      tools.SolutionBuildEnd(fSucceeded > 0 && fCancelCommand == 0);
      tools.RefreshTasks();
      Debug.WriteLine("UpdateSolution_Done: {0}", ElapsedSince(start));
      return 0;
    }

    public int UpdateSolution_StartUpdate(ref int pfCancelUpdate)
    {
      return 0;
    }
    #endregion

    #region IOleCommandTarget Members

    public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
    {
      if (taskListMenu != null)
      {
        var start = DateTime.Now;
        try
        {
          return taskListMenu.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
        }
        finally
        {
          Debug.WriteLine("Exec: {0}", ElapsedSince(start));
        }
      }
      else
        return OLECMDERR.OLECMDERR_E_NOTSUPPORTED;
    }

    public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
    {
      if (taskListMenu != null)
      {
        var start = DateTime.Now;
        try
        {
          return taskListMenu.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }
        finally
        {
          Debug.WriteLine("QueryStatus: {0}", ElapsedSince(start));
        }
      }
      else
        return OLECMDERR.OLECMDERR_E_UNKNOWNGROUP;
    }

    #endregion


    public int UpdateProjectCfg_Begin(IVsHierarchy pHierProj, IVsCfg pCfgProj, IVsCfg pCfgSln, uint dwAction, ref int pfCancel)
    {
      if (this.allTasksCleanedDuringThisSolutionUpdate) return 0;

      var start = DateTime.Now;
      bool cleanAll = false;
      VSSOLNBUILDUPDATEFLAGS flags = (VSSOLNBUILDUPDATEFLAGS)dwAction;

      if ((flags & VSSOLNBUILDUPDATEFLAGS.SBF_OPERATION_FORCE_UPDATE) != 0)
      {
        cleanAll = true;
      }
      if (cleanAll)
      {
        this.allTasksCleanedDuringThisSolutionUpdate = true;
        tools.ClearTasks();
      }
      else
      {
        tools.ClearTasksOnHierarchy(pHierProj);
      }
      Debug.WriteLine("UpdateProjectCfg_Begin: {0}", ElapsedSince(start));

      return 0;
    }

    public int UpdateProjectCfg_Done(IVsHierarchy pHierProj, IVsCfg pCfgProj, IVsCfg pCfgSln, uint dwAction, int fSuccess, int fCancel)
    {
      return 0;
    }
  }
}