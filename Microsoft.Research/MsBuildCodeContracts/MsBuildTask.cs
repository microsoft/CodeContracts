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

using System;
using System.Collections.Generic;

namespace Microsoft.Research
{
  using Microsoft.Build.Framework;
  using Microsoft.Build.Utilities;
  using System.Collections;
  using System.Globalization;
  using System.Diagnostics;

#if !NoVS
  using Microsoft.Build.Tasks;
  using Microsoft.VisualStudio.CodeTools;
  using Microsoft.VisualStudio.Shell.Interop;

  // needed for VS2012 to connect to DTE
  using System.Runtime.InteropServices;
  using System.Runtime.InteropServices.ComTypes;
#endif

  using System.Threading;
  using System.Text;
	using System.IO;
  using System.Diagnostics.Contracts;
  using System.Diagnostics.CodeAnalysis;

  /// <summary>
  /// The options available for the analysis task
  /// </summary>
  struct AnalysisOptions
  {
    public string projectName;
    public string command;
    public string commandOptions;
    public string workingDirectory;
    public string outputPrefix;
    public string additionalOutputPrefix;  

    // by default, all the bool options are "false"
    public bool createWindow;
    public bool runInBackground;
    public bool verbose;
    public bool hideMarkers;        // hide squigglies on all errors
    public bool multiLineMarkers;   // show multiple line squigglies
    public bool showRelatedMarkers; // show squigglies on related locations
    public bool hideOutput;
    public bool noRedirectOutput;   // do not redirect stdout
    public bool keepErrors;

    public bool ValidateOptions(TaskLoggingHelper log)
    {
      Contract.Ensures(this.IsValid || !Contract.Result<bool>());

      return
        ValidNonNull("Command", this.command, log) &&
        ValidNonNull("CommandOptions", this.commandOptions, log)
        ;
    }

    public bool IsValid
    {
      get
      {
        Contract.Ensures(!Contract.Result<bool>() ||
          (
            this.command != null &&
            this.commandOptions != null
          ));
        Contract.Ensures(Contract.Result<bool>() || this.command == null || this.commandOptions == null);

        return
          ValidNonNull("Command", this.command, null) &&
          ValidNonNull("CommandOptions", this.commandOptions, null)
          ;
      }
    }

    [Pure]
    private bool ValidNonNull(string name, object value, TaskLoggingHelper log)
    {
      Contract.Requires(name != null);
      Contract.Ensures(!Contract.Result<bool>() || (value != null));
      Contract.Ensures(Contract.Result<bool>() || (value == null));

      if (value == null)
      {
        if (log != null)
        {
          log.LogError("Parameter '{0}' must be non-empty", name);
        }
        return false;
      }
      return true;
    }


  }

  /// <summary>
  /// The code analysis msbuild task
  /// </summary>
  public class CodeContractsAnalysis : Task
  {
    #region Properties

    private AnalysisOptions options;

    public string WorkingDirectory
    {
      set { options.workingDirectory = value; }
      get { return options.workingDirectory; }
    }

    public string Command
    {
      set { options.command = value; }
      get { return options.command; }
    }

    public string CommandOptions
    {
      set { options.commandOptions = value; }
      get { return options.commandOptions; }
    }

    public string ProjectName
    {
      set { options.projectName = value; }
      get { return options.projectName; }
    }

    public bool RunInBackground
    {
      get { return options.runInBackground; }
      set { options.runInBackground = value; }
    }

    public bool CreateWindow
    {
      get { return options.createWindow; }
      set { options.createWindow = value; }
    }

    public bool HideMarkers
    {
      get { return options.hideMarkers; }
      set { options.hideMarkers = value; }
    }
  
    public bool MultiLineMarkers
    {
      get { return options.multiLineMarkers; }
      set { options.multiLineMarkers = value; }
    }

    public bool ShowRelatedMarkers
    {
      get { return options.showRelatedMarkers; }
      set { options.showRelatedMarkers = value; }
    }

    public bool Verbose
    {
      get { return options.verbose; }
      set { options.verbose = value; }
    }

    public bool HideOutput
    {
      get { return options.hideOutput; }
      set { options.hideOutput = value; }
    }

    public bool NoRedirectOutput
    {
      get { return options.noRedirectOutput; }
      set { options.noRedirectOutput = value; }
    }

    public bool KeepErrors
    {
      get { return options.keepErrors; }
      set { options.keepErrors = value; }
    }

    public string OutputPrefix
    {
      get { return options.outputPrefix; }
      set { options.outputPrefix = value; }
    }

    public string AdditionalOutputPrefix
    {
      get { return options.additionalOutputPrefix; }
      set { options.additionalOutputPrefix = value; }
    }

#if !NoVS
    private ITaskManager taskManager = null;
    private bool runOutOfProc = false;
#endif

    #endregion

    #region MSBuild entry
    // Main entry point; this is called by MSBuild.
    public override bool Execute()
    {
      try {
#if DEBUG
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            System.Diagnostics.Debugger.Launch();
        }
#endif
        // default prefixes
        if (options.additionalOutputPrefix == null) options.additionalOutputPrefix = "  + ";
        if (options.outputPrefix == null) options.outputPrefix = "Microsoft Code Contracts: ";

#if !NoVS
        taskManager = HostObject as ITaskManager;

        if (taskManager == null) {
          taskManager = GetTaskManagerFromROT();
          if (taskManager != null) runOutOfProc = true;
        }
#endif
        if (taskManager == null && Log != null) {
          Log.LogMessage(MessageImportance.High, options.outputPrefix + "Task manager is unavailable" + (options.runInBackground ? " (unable to run in background)." : "."));
          options.runInBackground = false;  // can only send messages in background if the taskmanager is available         
        }

        if (!options.ValidateOptions(this.Log))
        {
          return false;
        }

        // Set up an analysis task
        AnalysisTask analysisTask = new AnalysisTask(taskManager, Log, options, runOutOfProc);
        return analysisTask.Run();
      }
      catch (Exception e) {
        if (Log != null) {
          Log.LogErrorFromException(e);
        }
        return false;
      }
      finally {
        taskManager = null;
      }
    }

#endregion

    #region VS2012: get taskmanager via ROT
#if !NoVS
    [DllImport("ole32.dll")]
    static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable prot);

    [DllImport("ole32.dll")]
    static extern int CreateBindCtx(int reserved, out IBindCtx ppbc);

    static Hashtable GetRunningObjectTable()
    {
      Hashtable result = new Hashtable();

      IntPtr numFetched = IntPtr.Zero;
      IRunningObjectTable runningObjectTable;
      IEnumMoniker monikerEnumerator;
      IMoniker[] monikers = new IMoniker[1];

      GetRunningObjectTable(0, out runningObjectTable);
      if (runningObjectTable == null) return result;

      runningObjectTable.EnumRunning(out monikerEnumerator);
      if (monikerEnumerator == null) return result;
      monikerEnumerator.Reset();

      while (monikerEnumerator.Next(1, monikers, numFetched) == 0)
      {
        IBindCtx ctx;
        CreateBindCtx(0, out ctx);
        if (monikers[0] == null) continue;
        string runningObjectName;
        monikers[0].GetDisplayName(ctx, null, out runningObjectName);
        if (runningObjectName == null) continue;

        object runningObjectVal;
        runningObjectTable.GetObject(monikers[0], out runningObjectVal);

        result[runningObjectName] = runningObjectVal;
      }

      return result;
    }

    static ITaskManager GetTaskManagerFromIDE( string toolName, string vsPID = "" )
    {
      if (String.IsNullOrEmpty(vsPID)) {
        vsPID = Environment.GetEnvironmentVariable("VSPID");  // This is set by the TaskManager component
      }
      if (String.IsNullOrEmpty(vsPID))
      {
        return null; // no task manager
      }
      Hashtable runningObjects = GetRunningObjectTable();
      IDictionaryEnumerator rotEnumerator = runningObjects.GetEnumerator();
      while (rotEnumerator.MoveNext())
      {
        string candidateName = rotEnumerator.Key as string;
        if (candidateName == null) 
          continue;
        if (!candidateName.StartsWith("!VisualStudio.DTE"))
          continue;

        // we want only the VS that is doing our build
        if (!String.IsNullOrEmpty(vsPID))
        {
          int i = candidateName.IndexOf(':');
          if (i >= 0 && i < candidateName.Length - 1)
          {
            string dtePID = candidateName.Substring(i + 1);
            if (vsPID != dtePID) continue;
          }
        }

        EnvDTE.DTE ide = rotEnumerator.Value as EnvDTE.DTE;
        if (ide != null)
        {
          Microsoft.VisualStudio.OLE.Interop.IServiceProvider spide = ide as Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
          if (spide != null)
          {
            var taskMFGuid = Guid.Parse("6EF71237-5832-4cd5-A75C-067911AF5D14");
            IntPtr objIntPtr;
            var hr = spide.QueryService(ref taskMFGuid, ref taskMFGuid, out objIntPtr);
            if (hr == 0 && !objIntPtr.Equals(IntPtr.Zero))
            {
              var objService = System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(objIntPtr);
              System.Runtime.InteropServices.Marshal.Release(objIntPtr);

              //Microsoft.VisualStudio.Shell.ServiceProvider serviceProvider = new Microsoft.VisualStudio.Shell.ServiceProvider(spide);
              //object objService = serviceProvider.GetService(typeof(ITaskManagerFactory));
              ITaskManagerFactory taskManagerFactory = objService as ITaskManagerFactory;
              if (taskManagerFactory != null)
              {
                ITaskManager taskManager = taskManagerFactory.GetToolTaskManager(toolName);
                if (taskManager != null) return taskManager;
              }
            }
          }
        }
      }
      return null; 
    }


    private ITaskManager GetTaskManagerFromROT()
    {
      try {
        return GetTaskManagerFromIDE("Microsoft Code Contracts");
      }
      catch {
        return null; // we never want to fail on this
      }
    }
#endif
    #endregion
  }

  class ClearTasksEvent : IClearTasksEvent
  {
    readonly AnalysisTask task;

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(task != null);
    }

    public ClearTasksEvent(AnalysisTask _task)
    {
      Contract.Requires(_task != null);
      task = _task;
    }
    
    public void Invoke(IVsTaskManager taskManager, string projectName)
    {
      task.ivsTaskManager_OnClearTasks(taskManager, projectName);
    }
  }

  /// <summary>
  /// Each build results in a self-contained analysis task that potentially run in the background 
  /// </summary>
  class AnalysisTask
  {
    #region properties
    readonly TaskLoggingHelper log;
    readonly ITaskManager taskManager;
    AnalysisOptions options;
    Process process;
    readonly bool runOutOfProc;

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.options.IsValid);
    }

    #endregion

    #region Static properties for background task management

    // The backgroundTasks queue is never null and also serves
    // as the lock for all these static properties
    static Queue<AnalysisTask> backgroundTasks = new Queue<AnalysisTask>();   // the analysis tasks todo
    static AnalysisTask[] savedTasks = null; // during builds.
    static Thread currentThread = null;     // the background thread that spawns analysis processes
    static Process currentProcess = null;   // the current analysis process that is running
    static AnalysisTask currentTask = null;
    static IClearTasksEvent clearTasksEvent = null;  // event handler that is called when tasks are cleared in VS (i.e. time to clear the analysis queue)

    // Invariant on static fields: currentThread != null => clearTasksEvent != null
    #endregion

    #region Construction 
    public AnalysisTask(ITaskManager _taskManager, TaskLoggingHelper _log, AnalysisOptions _options, bool _runOutOfProc )
    {
      Contract.Requires(_options.IsValid);

      taskManager = _taskManager;
      log = _log;
      options = _options;
      runOutOfProc = _runOutOfProc;
      //Debugger.Launch();
    }
    #endregion

    #region Run a task
    public bool Run()
    {
      SetupProcess();
      if (options.runInBackground) {
        RunInBackground();
        return true;
      }
      else {
        return RunSequential();
      }
    }
    
    bool RunSequential()
    {
      Contract.Requires(this.process != null);

      process.Start();
      process.BeginErrorReadLine();
      process.BeginOutputReadLine();
      // WriteLine(options.projectName + ": analysis started.");
      process.WaitForExit();
      WriteLine(options.projectName + ": Static contract analysis done.");
      return (process.ExitCode == 0);
    }
    #endregion

    #region Run a background task 
    public void RunInBackground()
    {
      lock (backgroundTasks) {
        /* add this Analysis task to the back ground tasks */
        backgroundTasks.Enqueue(this);

        StartupBackgroundThreadIfNeeded();
      }
    }

    private void StartupBackgroundThreadIfNeeded()
    {
      if (backgroundTasks.Count > 0)
      {
        SubscribeToBuildEvents();

        /* if necessary fire off a new thread to spawn processes */
        if (currentThread == null)
        {
          currentThread = new Thread(RunBackgroundTasks);
          currentThread.Name = "Code Analysis background thread";
          currentThread.Start();
        }
      }
      else
      {
        // don't close our communication if we have a thread running. We could be running tasks.
        if (currentThread == null)
        {
          UnsubscribeFromBuildEvents();
        }
      }
    }


    // Background thread loop that spawns processes 
    static void RunBackgroundTasks()
    {
			AnalysisTask task = null;
			try {
				while (true) {
					lock (backgroundTasks) {
						// exit the thread if no work is left
						if (backgroundTasks.Count == 0) {
							currentThread = null;
							return;
						}

						// dequeue a task and start a process
						task = backgroundTasks.Dequeue();
						if (task != null) {
							currentProcess = task.process;
              currentTask = task;
						}
					}

					if (task != null) {
            try
            {
              if (task.process != null)
              {
                task.process.Start();
                task.process.BeginErrorReadLine(); // redirect stderr
                if (!task.options.noRedirectOutput)
                {
                  task.process.BeginOutputReadLine(); // redirect stdout
                }
                task.WriteLine(task.options.projectName + ": Background contract analysis started.");
                task.process.WaitForExit();
              }
            }
            catch (Exception exn)
            {
              task.WriteLine(task.options.projectName + ": Background contract analysis: Exception occurred:\n" + exn.ToString());
            }
						finally {
							task.WriteLine(task.options.projectName + ": Background contract analysis done.");
              task = null;
						}
					}

					lock (backgroundTasks) {
						currentProcess = null;
            currentTask = null;
					}
				}
			}
			catch (Exception exn) {
				System.Diagnostics.Trace.WriteLine("Code contracts task manager exception:\n" + exn.ToString());
				if (task != null) {
					task.WriteLine("Code contracts task manager exception:\n" + exn.ToString());
				}
			}
			finally {
				lock (backgroundTasks) {
					currentProcess = null;
					currentThread = null;
          currentTask = null;
				}
			}
    }

    /// <summary>
    /// This is called under the following cirumstances:
    /// 1. Whenever solution update happens it is called with "buildStart" in angle brackets.
    /// 2. Whenever a project is built, it is called with the project name
    /// 3. If all projects are cleared (on rebuilds) it is called with an emtpyt string
    /// 4. Whenever the solution build is done, it is called with "buildDone"
    /// 
    /// If we are running Clousot in a separate process, then we need to kill the process WHENEVER a build starts.
    /// Otherwise, the build may fail due to locked files. The tasks that haven't been analyzed at that point are
    /// saved away in a saved task list.
    /// 
    /// During the build, we get to clear certain projects from the queue (those that are rebuilt) and the saved task list. 
    /// Some of these will be added again by the build task executing. We can start processing these again as we get notified
    /// only when all dependencies are ready.
    /// 
    /// At the end of the build "buildDone", we can put all saved tasks that weren't cleared back into the queue.
    /// </summary>
    /// <param name="taskManager"></param>
    /// <param name="projectName"></param>
    public void ivsTaskManager_OnClearTasks(IVsTaskManager taskManager, string projectName)
    {
      try
      {

        lock (backgroundTasks)
        {

          switch (projectName)
          {
            case "":
              backgroundTasks.Clear();
              savedTasks = null;
              break;

            case "<buildStart>":
              var offset = 1; // save space for possible current task
              savedTasks = new AnalysisTask[backgroundTasks.Count + offset];
              backgroundTasks.CopyTo(savedTasks, offset);
              backgroundTasks.Clear(); 
              var currentTask = KillProc(taskManager);
              if (currentTask != null) {
                currentTask.process = null; // clear old process
                savedTasks[0] = currentTask; 
              }
              break;

            case "<buildCancel>":
              // make sure not to restart any tasks
              backgroundTasks.Clear();
              KillProc(taskManager); // for good measure
              savedTasks = null;
              break;

            case "<buildFailed>":
              // don't restart the tasks
              backgroundTasks.Clear();
              KillProc(taskManager);
              savedTasks = null;
              break;

            case "<buildDone>":
              // add saved tasks back on queue
              if (savedTasks != null)
              {
                for (int i = 0; i < savedTasks.Length; i++)
                {
                  var task = savedTasks[i];
                  if (task == null) continue;
                  if (task.process == null)
                  {
                    task.SetupProcess(); // reset the process to start again when build is done.
                  }
                  backgroundTasks.Enqueue(task);
                }
                savedTasks = null;
              }
              StartupBackgroundThreadIfNeeded();
              break;

            case "<release>":
              KillProc(taskManager);
              UnsubscribeFromBuildEvents(taskManager);
              backgroundTasks.Clear();
              savedTasks = null;
              break;

            default:
              ClearProject(projectName);
              break;
          }
        }
      }
      catch (Exception exn)
      {
        System.Diagnostics.Trace.WriteLine("Code contracts task manager exception:\n" + exn.ToString());
      }
    }

    private static void ClearProject(string projectName)
    {
      // find it in the task list
      var tasks = backgroundTasks.ToArray(); // missing corlib contract. Will be in next release.
      backgroundTasks.Clear();

      for (int i = 0; i < tasks.Length; i++)
      {
        var task = tasks[i];
        if (task == null) continue;
        if (task.options.projectName == projectName) { continue; }
        backgroundTasks.Enqueue(task);
      }
      if (savedTasks != null)
      {
        for (int i = 0; i < savedTasks.Length; i++)
        {
          if (savedTasks[i] == null) continue; // clousot found this high value bug!!!
          if (savedTasks[i].options.projectName == projectName) { savedTasks[i] = null; }
        }
      }
    }

    private static AnalysisTask KillProc(IVsTaskManager taskManager)
    {
      AnalysisTask result = null;
      // kill current process
      if (currentProcess != null)
      {
        try
        {
          if (!currentProcess.HasExited)
          {
            currentProcess.Kill();
            result = currentTask;
          }
        }
        catch (Exception exn)
        {
          System.Diagnostics.Trace.WriteLine("Code contracts: Task manager exception: kill process:\n" + exn.ToString());
        }
        finally
        {
          currentProcess = null;
          currentTask = null;
        }
      }
      return result;
    }

    private bool SubscribeToBuildEvents()
    {
      /* install a handler to remove tasks if necessary*/
      if (taskManager != null && clearTasksEvent == null)
      {
        try
        {
          IVsTaskManager ivsTaskManager = taskManager as IVsTaskManager;
          if (ivsTaskManager != null)
          { // && !runOutOfProc) {
            // System.Diagnostics.Debugger.Launch();
            clearTasksEvent = new ClearTasksEvent(this);
            ivsTaskManager.AddClearTasksEvent(clearTasksEvent);
            return true;
          }
        }
        catch
        { }
      }
      return false;
    }

    private void UnsubscribeFromBuildEvents()
    {
      IVsTaskManager ivsTaskManager = taskManager as IVsTaskManager;
      if (ivsTaskManager != null)
      { // && !runOutOfProc) {
        // System.Diagnostics.Debugger.Launch();

        UnsubscribeFromBuildEvents(ivsTaskManager);
      }
    }

    private static void UnsubscribeFromBuildEvents(IVsTaskManager taskManager)
    {
      // unsubscribe from the VS task manager clear events
      if (taskManager != null && clearTasksEvent != null)
      {
        try
        {
          taskManager.RemoveClearTasksEvent(clearTasksEvent);
        }
        catch { }
        clearTasksEvent = null;
      }
    }
    #endregion

    #region Initialize a process object
    internal void SetupProcess()
    {
      Contract.Ensures(this.process != null);

      // Set the right working directory for the analyzer
			if (options.workingDirectory == null || options.workingDirectory.Length == 0) {
				options.workingDirectory = System.Environment.CurrentDirectory;
			}
			else {
				options.workingDirectory = Path.GetFullPath(options.workingDirectory);
			}
      Contract.Assert(options.workingDirectory != null);

      // Output a build message
      WriteLine(options.projectName + ": " + (options.runInBackground ? "Schedule" : "Run") + " static contract analysis.");
      if (options.verbose) {
        WriteLine(" Command: " + options.command + "\n" +
                  " Options: " + options.commandOptions + "\n" +
                  " Dir    : " + options.workingDirectory );
      }

      process = new Process();
      process.StartInfo.FileName = options.command; 
      process.StartInfo.Arguments = options.commandOptions;
      process.StartInfo.WorkingDirectory = options.workingDirectory;
      process.StartInfo.UseShellExecute = false;
      process.StartInfo.CreateNoWindow = !options.createWindow;
      
      // redirect standard error & stdout
      process.StartInfo.RedirectStandardError = true;
      process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceived);
      if (!options.noRedirectOutput) {
        process.StartInfo.RedirectStandardOutput = true;
        process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
      }
    }


    #endregion

    #region Output actions for the analysis process

#if !NoVS
    private Location relatedLocation;
#endif

    public void OutputError(TaskCategory category, string description, string code, string help, string proofOutcome, bool additional, string subCategory, string fullPath, int startLine, int startColumn, int endLine, int endColumn)
    {
#if !NoVS
      if (taskManager != null) {
        // Show a full task item for this error 
        TaskMarker marker = TaskMarker.Invisible;
        TaskPriority priority = TaskPriority.Normal;

        if (category == TaskCategory.Warning) {
          marker = TaskMarker.Other;  // purple
        }
        else if (category == TaskCategory.Error) {
          marker = TaskMarker.Error; // blue
          priority = TaskPriority.High;
          if (!options.keepErrors) category = TaskCategory.Warning; // show all contract errors as warnings
        }

        if (options.hideMarkers || (additional && !options.showRelatedMarkers)) {
          marker = TaskMarker.Invisible;
        }

        if (!options.multiLineMarkers && startLine < endLine) {
          endLine = startLine;
          endColumn = startColumn + 100; // bug: should really look at the source to determine the end of the line
        }

        if (additional && options.additionalOutputPrefix != null) {
          description = options.additionalOutputPrefix + description;
        }
        else if (options.outputPrefix != null) {
          description = options.outputPrefix + description;
        }

        ITaskCommands commands = null;
        if (!additional) {
          relatedLocation = new Location();
          commands = new TaskCommands(relatedLocation);
        }
        else {
          if (relatedLocation != null) {
            // set related location slots from the previous error
            relatedLocation.projectName = options.projectName;
            relatedLocation.fileName = fullPath;
            relatedLocation.startLine = startLine;
            relatedLocation.startColumn = startColumn;
            relatedLocation.endLine = endLine;
            relatedLocation.endColumn = endColumn;
            // and set the related location to null again
            relatedLocation = null;
          }
        }

        try
        {
          taskManager.AddTask(
              description, null,
              code, help,
              priority, category, marker,
              (options.hideOutput ? TaskOutputPane.None : TaskOutputPane.Build),
              options.projectName, fullPath,
              startLine, startColumn, endLine, endColumn,
              commands
              );
        }
        catch { }

        //taskManager.Refresh();        
      }
      else
#endif
        if (log != null && !options.runInBackground) {
          if (options.outputPrefix != null) {
            description = options.outputPrefix + description;
          }

          if (category == TaskCategory.Warning || (category == TaskCategory.Error && !options.keepErrors))
            log.LogWarning("", code, help, fullPath,
                            startLine, startColumn, endLine, endColumn,
                            description, null);
          else if (category == TaskCategory.Error )
          {
            Contract.Assert(options.keepErrors, "Follows from the check above");
            log.LogError("", code, help, fullPath,
                          startLine, startColumn, endLine, endColumn,
                          description, null);
          }
          else
            log.LogMessage(MessageImportance.High, description);
        }
    }

    public void OutputSuggestion(string kind, string suggestion, string fullPath, int startLine, int startColumn, int endLine, int endColumn)
    {
      Contract.Requires(kind != null);

      OutputError(TaskCategory.Message, (kind.Length>0 ? "Suggested " + kind + ": " + suggestion : suggestion), 
                  "", "", "", false, "", fullPath, startLine, startColumn, endLine, endColumn);
    }

    public void OutputLine(string msg)
    {
      if (!options.hideOutput) WriteLine(options.projectName + ": " + msg);
    }

    public void OutputLine(string msg, params string[] args)
    {
      Contract.Requires(msg != null);
      Contract.Requires(args != null);

      if (!options.hideOutput) WriteLine(options.projectName + ": " + msg, args);
    }

    public void Output(string msg, params string[] args)
    {
      Contract.Requires(msg != null);
      Contract.Requires(args != null);

      if (!options.hideOutput) Write(msg, args);
    }

    public void Output(string msg)
    {
      if (!options.hideOutput) Write(msg);
    }
    public void Close()
    {
    }

    #endregion

    # region Write to VS build pane
    
    public void WriteLine(string format, params string[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);

      string message = String.Format(format, args);
      WriteLine(message);
    }

    [Pure]
    public void WriteLine(string message)
    {
      string prefix = (options.outputPrefix != null ? options.outputPrefix : "");

#if !NoVS
      if (taskManager != null)
      {
        try
        {
          taskManager.OutputString(prefix + message + "\n", TaskOutputPane.Build);
        }
        catch
        { }
      }
      else
#endif
        if (log != null && !options.runInBackground)
        {
          log.LogMessage(MessageImportance.High, prefix + message, null);
        }

    }

    public void Write(string format, params string[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);

      string message = String.Format(format, args);
      Write(message);
   }
    public void Write(string message)
    {
#if !NoVS
      if (taskManager != null)
      {
        try
        {
          taskManager.OutputString(message, TaskOutputPane.Build);
        }
        catch
        { }
      }
      else
#endif
        if (log != null && !options.runInBackground)
        {
          log.LogMessage(MessageImportance.High, message, null);  // ouch, this also outputs a newline..
        }
    }
    #endregion

    # region Clousot output handler
    // the stdout output handler
    void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
      OutputLine(e.Data);
    }
    
    // The stderr output handler
    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant", Justification ="Bug in Clousot")]
    void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
      if (e.Data != null)
      {
        try
        {
          // System.Diagnostics.Debugger.Break();
          string[] args = Split(e.Data, ',');

          if (args.Length > 0)
          {
            if (args[0] == "OutputError" && args.Length == 13)
            {
              bool warning = bool.Parse(args[5]);
              OutputError(warning ? TaskCategory.Warning : TaskCategory.Error,
                          args[1], args[2], args[3], args[4], bool.Parse(args[6]), args[7], args[8],
                             int.Parse(args[9]), int.Parse(args[10]), int.Parse(args[11]), int.Parse(args[12]));
            }
            else if (args[0] == "OutputSuggestion" && args.Length == 8)
            {
              OutputSuggestion(args[1], args[2], args[3], int.Parse(args[4]), int.Parse(args[5]), int.Parse(args[6]), int.Parse(args[7]));
            }
            else if (args[0] == "OutputLine" && args.Length == 2)
            {
              OutputLine(args[1]);
            }
            else if (args[0] == "Output" && args.Length == 2)
            {
              Output(args[1]);
            }
            else if (args[0] == "Close" && args.Length == 1)
            {
              Close();
            }
            else
            {
              OutputLine(e.Data);  // output unknown message as is?
            }
          }
        }
        catch (Exception exn)
        {
          System.Diagnostics.Debugger.Log(1, "Error", (options.outputPrefix != null ? options.outputPrefix : "") + "internal error:\n" + exn.ToString() + "\n");
        }
      }
    }

    // Split analysis process output.
    // Format: a comma seperated list where the first item is the action, and the rest the arguments
    // Each item is escape character encoded:
    //   ,        ->   \,
    //   \        ->   \\
    //  newline   ->   \n
    //   > '~'    ->   \x1234  or \X12345678
    // where we assume that \, is never a valid escape character
    [ContractVerification(false)]
    private static string[] Split(string s, char splitC)
    {
      Contract.Requires(s != null);
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), elem => elem != null));

      List<string> parts = new List<string>();
      if (s != null) {
        int len = s.Length;
        char[] buf = new char[len];
        int i = 0;
        while (i < len) {
          int count = 0;
          int bufCount = 0;
          while (i + count < len && s[i + count] != splitC) {
            Contract.Assert(bufCount <= i + count);

            if (s[i + count] == '\\') {
              if (i + count + 1 < len) {
                char c = s[i + count + 1];
                if (c == 'n') {
                  buf[bufCount] = '\n';
                  count += 2;
                }
                else if (c == 'x' && i + count + 1 + 4 < len) {
                  string num = s.Substring(i + count + 2, 4);
                  buf[bufCount] = (char)(Convert.ToInt32(num, 16));
                  count += 6;
                }
                else if (c == 'X' && i + count + 1 + 8 < len) {
                  string num = s.Substring(i + count + 2, 8);
                  buf[bufCount] = (char)(Convert.ToInt32(num, 16));
                  count += 10;
                }
                else {
                  buf[bufCount] = c;
                  count += 2;
                }
              }
              else {
                // trailing '\\' character...
                buf[bufCount] = '\\';
                count += 1;
              }
            }
            else {
              buf[bufCount] = s[i + count];
              count += 1;
            }
            bufCount++;
          }

          if (bufCount > 0) {
            string part = new string(buf, 0, bufCount);
            parts.Add(part.Trim());
          }
          else {
            parts.Add("");
          }
          i = i + count + 1; // skip separator
        }
      }
      return parts.ToArray();
    }

    #endregion
  }


  class Location
  {
    public string projectName;
    public string fileName;
    public int startLine;
    public int startColumn;
    public int endLine;
    public int endColumn;
  }

  class TaskCommands : Microsoft.VisualStudio.CodeTools.ITaskCommands
  {
    #region ITaskCommands Members

    private static ITaskMenuItem
      menuJumpToRelated = new TaskMenuItem("Go to contract definition", TaskMenuKind.Normal);

    private static ITaskMenuItem[]
      groupMain = { menuJumpToRelated };

    readonly Location location;

    public TaskCommands(Location _location)
    {
      location = _location;
    }

    public ITaskMenuItem[] GetMenuItems(ITaskMenuItem parent)
    {
      if (parent == null)
        return groupMain;
      else 
        return null;
    }

    public bool IsChecked(ITaskMenuItem menuItem)
    {
      Contract.Assume(menuItem.Kind == TaskMenuKind.Checkable);
      return false;
    }

    public bool IsEnabled(ITaskMenuItem menuItem)
    {
      if (menuItem == menuJumpToRelated) 
        return (location != null && location.projectName != null);
      else
        return true;
    }

    public void OnContextMenu(Microsoft.VisualStudio.CodeTools.ITask task, ITaskMenuItem menuItem)
    {
      if (menuItem == menuJumpToRelated) {
        OnDoubleClick(task);
      }
    }

    public void OnDoubleClick(Microsoft.VisualStudio.CodeTools.ITask task)
    {
      if (task != null && location != null && location.projectName != null) {
        IVsTask vsTask = task as IVsTask;
        if (vsTask != null) {
          vsTask.NavigateToLocation(location.projectName, location.fileName, location.startLine, location.startColumn, location.endLine, location.endColumn, true);
        }
      }
    }

    public void OnHover(Microsoft.VisualStudio.CodeTools.ITask task)
    {
      if (task != null) {
        // opportunity to change the "TipText"
      }
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
      return;
    }

    #endregion
  }

}



/*
 * The following class is an example of how to attach context menu
 * commands to a task item. They are just here to show the capabilities
 * of menus and submenus.
*/
/*
class TaskCommand : Microsoft.VisualStudio.CodeTools.ITaskCommands
{
  #region ITaskCommands Members

  private static ITaskMenuItem
    menuCheckable = new TaskMenuItem("Checkable", TaskMenuKind.Checkable),
    menuDelete = new TaskMenuItem("Delete?", TaskMenuKind.Normal),
    menuEven = new TaskMenuItem("Even", TaskMenuKind.Normal),
    menuOdd = new TaskMenuItem("Odd", TaskMenuKind.Normal),
    menuSub1 = new TaskMenuItem("SubMenu1", TaskMenuKind.SubMenu),
    menuSub2 = new TaskMenuItem("SubMenu2", TaskMenuKind.SubMenu),
    menuSubSub = new TaskMenuItem("SubSubMenu", TaskMenuKind.SubMenu);

  private static ITaskMenuItem[]
    groupEven = { menuCheckable, menuDelete, menuEven, menuSub1, menuSub2 },
    groupOdd = { menuCheckable, menuDelete, menuEven, menuOdd, menuSub1 },
    groupSub1 = { menuDelete, menuSubSub },
    groupSub2 = { menuCheckable, menuDelete },
    groupSubSub = { menuCheckable, menuDelete };

  private bool odd;
  private bool isChecked;

  public TaskCommand(bool isOdd )
  {
    odd = isOdd;
    isChecked = false;
  }

  public ITaskMenuItem[] GetMenuItems( ITaskMenuItem parent ) {
    if (parent ==null)
      return (odd ? groupOdd : groupEven);
    else if (parent == menuSub1) {
      return groupSub1;
    }
    else if (parent == menuSub2 ) {
      return groupSub2;
    }
    else if (parent == menuSubSub ) {
      return groupSubSub;
    }
    else {
      return null;
    }
  }

  public bool IsChecked(ITaskMenuItem menuItem)
  {
    Debug.Assert(menuItem.Kind == TaskMenuKind.Checkable);
    return isChecked;
  }

  public bool IsEnabled(ITaskMenuItem menuItem)
  {
    if (menuItem == menuOdd)
      return odd;
    else if (menuItem == menuEven)
      return !odd;
    else
      return true;
  }

  public void OnContextMenu(Microsoft.VisualStudio.CodeTools.ITask task, ITaskMenuItem menuItem)
  {
    Trace.WriteLine("Contracts menu: " + menuItem.Caption);
    if (task != null) {
      if (menuItem == menuDelete) {
        task.Remove();
      }
      else if (menuItem == menuCheckable) {
        isChecked = task.Checked = !task.Checked;
        if (task.Checked) {
          task.Marker = TaskMarker.Invisible;
        }
        else {
          task.Marker = TaskMarker.Error;
        }
      }
    }
  }

  public void OnDoubleClick(Microsoft.VisualStudio.CodeTools.ITask task)
  {
    Trace.WriteLine("Contracts on double click");      
  }

  public void OnHover(Microsoft.VisualStudio.CodeTools.ITask task)
  {
    Trace.WriteLine("Contracts on hover");
  }

  #endregion

  #region IDisposable Members

  public void Dispose()
  {
    return;
  }

  #endregion
}
*/
