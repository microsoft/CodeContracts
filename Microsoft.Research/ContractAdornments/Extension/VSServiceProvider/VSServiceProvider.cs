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
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using ContractAdornments.Interfaces;
using ContractAdornments.OptionsPage;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using UtilitiesNamespace;
using VSLangProj;

namespace ContractAdornments {



  #region Package attributes
  [PackageRegistration(UseManagedResourcesOnly = true)]
  [ProvideOptionPageAttribute(typeof(ContractOptionsPage), "Code Contracts Editor Extensions", "General", 100, 101, true)]
  [ProvideProfileAttribute(typeof(ContractOptionsPage), "Code Contracts Editor Extensions", "General", 100, 101, true, DescriptionResourceID = 100)]
  [InstalledProductRegistration("CodeContractsHelper_Package", "A package for the CodeContractsHelper extension.", "1.0")]
  [Guid(GuidList.guidVSServiceProviderString)]
  [ProvideAutoLoad("ADFC4E64-0397-11D1-9F4E-00A0C911004F")]
  #endregion
  public sealed class VSServiceProvider : Package, IContractsPackage, IVsSolutionEvents, IVsUpdateSolutionEvents2, IOleComponent
  {
    public const string InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate = "The snapshot is out of date";
    public const string COMExceptionMessage_BindingFailed = "Binding failed because IntelliSense for source file";
    public const int LeaderBoardToolId = 0x4;
    public const int LeaderBoardMask = LeaderBoardToolId << 12;
    const string CrashMailRecepients = "ccixfb@microsoft.com";

    public static VSServiceProvider Current
    {
      get {
        Contract.Ensures(Contract.Result<VSServiceProvider>() != null); 
        return (VSServiceProvider)ContractsPackageAccessor.Current;
      }
    }
    public readonly Logger logger;

    public Logger Logger
    {
      get
      {
        Contract.Ensures(Contract.Result<Logger>() != null);
        return this.logger;
      }
    }

    public bool InBuild { get; private set; }
    ICompilerHost _compilerHost;
    DTE _dte;
    uint _componentID;
    bool _solutionLoaded = false;
    private readonly Dictionary<string, ProjectTracker> _projectTrackers;
    public Dictionary<string, ProjectTracker> ProjectTrackers
    {
      get
      {
        Contract.Ensures(Contract.Result<Dictionary<string, ProjectTracker>>() != null);

        return this._projectTrackers;
      }
    }
    IVsOutputWindowPane _outputPane;
    readonly DateTime _startTime;
    StreamWriter _outputStream = null;
    int _processIdUsedByStreamWriter = 0;
    public bool ExtensionHasFailed { get; private set; }
    bool _failOnce = false;
    readonly Queue<Action> _workItems = new Queue<Action>(16);
    bool _isGettingVSModel = false;
    NonlockingHost _v3Host;
    NonlockingHost _v4Host;
    NonlockingHost _v45Host; // review: do we really need a different host for each framework?
    private ContractOptionsPage vsOptionsPage;
    public ContractOptionsPage VSOptionsPage
    {
      get
      {
        Contract.Ensures(Contract.Result<ContractOptionsPage>() != null);

        if (this.vsOptionsPage == null)
        {
          throw new InvalidOperationException("VSOption page called before initialization");
        }
        return this.vsOptionsPage;
      }
    }

    IContractOptionsPage IContractsPackage.VSOptionsPage
    {
        get
        {
            return VSOptionsPage;
        }
    }

    public event Action<object> NewCompilation;
    public event Action BuildDone;
    public event Action<string> BuildBegin;
    public event Action ExtensionFailed;

    [ContractInvariantMethod]
    void Invariants() {
      Contract.Invariant(Current != null);
      //Contract.Invariant(FilesNeedingNewSemanticInfo != null);
      //Contract.Invariant(FilesNeedingNewSyntacticInfo != null);
      Contract.Invariant(this.logger != null);
      Contract.Invariant(this._workItems != null);
      Contract.Invariant(this._projectTrackers != null);
    }

    /// <summary>
    /// Sets VSSeriviceProvider.Current.
    /// </summary>
    public VSServiceProvider() {
      ContractsPackageAccessor.Current = this;
      _startTime = DateTime.Now;

#if false
      if (!System.Diagnostics.Debugger.IsAttached)
      {
          System.Diagnostics.Debugger.Launch();
      }
#endif
      this.logger = new Logger(PublicEntryException, QueueWorkItem,
        (s) => {
          if (_outputPane != null) {
            var elapsed = DateTime.Now - _startTime;
            var outputString = s + " " + (int)elapsed.TotalMinutes + ":" + elapsed.Seconds + "." + elapsed.Milliseconds;
            _outputPane.OutputString(outputString + "\n");
          }
        },
        (s) => {
          Debug.WriteLine(s);
        },
        (s) => {
          if (_outputStream != null) {
            var elapsed = DateTime.Now - _startTime;
            var outputString = s + " " + (int)elapsed.TotalMinutes + ":" + elapsed.Seconds + "." + elapsed.Milliseconds;
            _outputStream.WriteLine(outputString);
          }
        });
      this._projectTrackers = new Dictionary<string, ProjectTracker>();
    }
    /// <summary>
    /// Registers this component and hooks this up for solution events.
    /// </summary>
    protected override void Initialize() {
      Logger.PublicEntry(() => {
        base.Initialize();

        //Grab the options page
        this.vsOptionsPage = GetDialogPage(typeof(ContractOptionsPage)) as ContractOptionsPage;
        if (VSOptionsPage == null) {
          //If we can't get our options page, just use the default options
          this.vsOptionsPage = new ContractOptionsPage();
          Logger.WriteAlways("Error: Options page 'Code Contracts Editor Extensions' could not be found!");
        }
        Logger.EnableLogging = VSOptionsPage.Logging;

#if LEADERBOARD
        //Tell leaderboard our startup options
        LeaderBoard.LeaderBoardAPI.SendLeaderBoardFeatureUse(VSOptionsPage.Options.GetId() | (int)LeaderBoardMasks.CodeContractsHelperId);
#endif

        QueueWorkItem(() => {
          _outputPane = GetOutputPane(VSConstants.SID_SVsGeneralOutputWindowPane, "Code Contracts Editor Extensions");
        });

        //TODO: Is this the best place for this code?
        if (Logger.EnableLogging) {
          try {
            if (!String.IsNullOrEmpty(VSOptionsPage.OutputPath) && Directory.Exists(VSOptionsPage.OutputPath)) {
              _processIdUsedByStreamWriter = System.Diagnostics.Process.GetCurrentProcess().Id;
              _outputStream = File.CreateText(Path.Combine(VSOptionsPage.OutputPath, String.Format("ContractsHelperLog_{0}.txt", _processIdUsedByStreamWriter)));
              _outputStream.WriteLine("Log for Code Contracts Visual Studio Extension. Start time: " + _startTime);
            } else {
              //TODO: Failed to get output path!
            }
          } catch 
#if DEBUG
          (Exception exn) {
            //TODO: What to do on shipping?
            System.Windows.MessageBox.Show(@"An exception was thrown while trying to create a log file. Please send a picture of this stack trace to " + CrashMailRecepients
              + "\nException: " + exn, "Sorry!",
              System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information, System.Windows.MessageBoxResult.OK, System.Windows.MessageBoxOptions.None);
#else
          {
#endif
            _outputStream = null;
          }
        }

        #region Register this component
        IOleComponentManager componentManager = (IOleComponentManager)this.GetService(typeof(SOleComponentManager));
        if (_componentID == 0 && componentManager != null) {
          OLECRINFO[] crinfo = new OLECRINFO[1];
          crinfo[0].cbSize = (uint)Marshal.SizeOf(typeof(OLECRINFO));
          crinfo[0].grfcrf = (uint)_OLECRF.olecrfNeedIdleTime | (uint)_OLECRF.olecrfNeedPeriodicIdleTime;
          crinfo[0].grfcadvf = (uint)_OLECADVF.olecadvfModal | (uint)_OLECADVF.olecadvfRedrawOff | (uint)_OLECADVF.olecadvfWarningsOff;
          crinfo[0].uIdleTimeInterval = 1000;
          ErrorHandler.ThrowOnFailure(componentManager.FRegisterComponent(this, crinfo, out _componentID));
        }
        #endregion

        #region Hook up solution events
        // Attach to solution events
        var solution = GetService(typeof(SVsSolution)) as IVsSolution;
        if (solution != null)
        {
          uint solutionEventsCookie;
          solution.AdviseSolutionEvents(this, out solutionEventsCookie);
        }

        // Attach to build events
        var buildManager = GetService(typeof(SVsSolutionBuildManager)) as IVsSolutionBuildManager;
        if (buildManager != null)
        {
          uint buildManagerCookie; // TODO: add dispose and unregister
          buildManager.AdviseUpdateSolutionEvents(this, out buildManagerCookie);
        }

        _dte = GetService<DTE>();
        #endregion
      }, "Initialize");
    }

    /// <summary>
    /// Gets the project that the given file is a part of.
    /// </summary>
    public VSProject GetProjectForFile(string fileName) {
      Contract.Requires(!string.IsNullOrEmpty(fileName), "File name is null or empty.");

      if (this._dte == null || this._dte.Solution == null) return null;
      var projectItem = this._dte.Solution.FindProjectItem(fileName);
      if (projectItem == null)
        return null;
      if (projectItem.ContainingProject == null) return null;
      var result = projectItem.ContainingProject.Object as VSProject;
      if (result != null)
        return result;
      return null;
    }

    public IVersionedServicesFactory GetVersionedServicesFactory() {
      int vsMajorVersion = typeof(ErrorHandler).Assembly.GetName().Version.Major;
      string assemblyName = "ContractAdornments.CSharp." + vsMajorVersion;
      Assembly assembly = Assembly.Load(assemblyName);
      return (IVersionedServicesFactory)assembly.CreateInstance("ContractAdornments.CSharp.VersionedServicesFactory");
    }

    NonlockingHost CreateHost(Version version) {

      string[] libpaths = null;
      if (!String.IsNullOrEmpty(VSOptionsPage.ContractPaths)) {
          libpaths = VSOptionsPage.ContractPaths.Split(';');
      }
      //If no version has been provided, just return a default host
      if (version == null) goto NoFrameWork;

      string codeContractsInstallDir = Environment.GetEnvironmentVariable("CodeContractsInstallDir");

      //if (String.IsNullOrEmpty(codeContractsInstallDir)) {
      //  VSServiceProvider.Current.Logger.WriteToLog("CodeContractsInstallDir environment variable not found. Creating default host.");
      //  return new NonlockingHost();
      //}

      string frameworkSubFolder = "";
      if (version.Major == 4) {
        if (version.Minor == 0)
          frameworkSubFolder = @"Contracts\.NETFramework\v4.0";
        else if (version.Minor == 5)
          frameworkSubFolder = @"Contracts\.NETFramework\v4.5";
      } else if (version.Major < 4) {
        frameworkSubFolder = @"Contracts\v3.5";
      } else {
        VSServiceProvider.Current.Logger.WriteToLog("Unsupported .NET framework version. Creating default host.");
        goto NoFrameWork;
      }

      string contractsDir;
      if (String.IsNullOrEmpty(codeContractsInstallDir)) {
        VSServiceProvider.Current.Logger.WriteToLog("CodeContractsInstallDir environment variable not found.");
        var d = Assembly.GetExecutingAssembly().Location;
        var adornmentsInstallDir = Path.GetDirectoryName(d);
        contractsDir = Path.Combine(adornmentsInstallDir, frameworkSubFolder);
      } else {
        contractsDir = Path.Combine(codeContractsInstallDir, frameworkSubFolder);
      }
      VSServiceProvider.Current.Logger.WriteToLog("Creating host for framework version: " + version.ToString() + " and with lib paths: " + contractsDir);
      if (libpaths != null)
      {
          var newPaths = new string[libpaths.Length + 1];
          Array.Copy(libpaths, newPaths, libpaths.Length);
          newPaths[libpaths.Length] = contractsDir;
          return new NonlockingHost(newPaths);
      }
      return new NonlockingHost(contractsDir);

      NoFrameWork:

      if (libpaths == null)
      {
          return new NonlockingHost();
      }
      else
      {
          return new NonlockingHost(libpaths);
      }
    }
    public NonlockingHost GetHost(Version version = null) {
      Contract.Ensures(Contract.Result<NonlockingHost>() != null);

      //Are we given a verison?
      if (version == null) {
        VSServiceProvider.Current.Logger.WriteToLog("Unknown .NET framework version. Defaulting to no version.");
        return CreateHost(null);
      }

      Contract.Assume(this.VSOptionsPage != null);

      if (version.Major == 4) {
        if (version.Minor == 0) {
          if (_v4Host == null)
            _v4Host = CreateHost(version);
          return _v4Host;
        } else if (version.Minor == 5) {
          if (_v45Host == null)
            _v45Host = CreateHost(version);
          return _v45Host;
        } else {
          VSServiceProvider.Current.Logger.WriteToLog("Unsupported .NET framework version." + version.ToString());
          return CreateHost(null);
        }
      } else if (version.Major < 4) {
        if (_v3Host == null)
          _v3Host = CreateHost(version);
        return _v3Host;
      } else {
        VSServiceProvider.Current.Logger.WriteToLog("Unsupported .NET framework version." + version.ToString());
        return CreateHost(null);
      }
    }

    static public string GetProjectName(IVsHierarchy hierarchy)
    {
      object name = null;
      if (hierarchy != null)
      {
        hierarchy.GetProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_Name, out name);
      }
      if (name != null)
      {
        return name as String;
      }
      else
      {
        return null;
      }
    }

    public Project GetProject(IVsHierarchy hierarchy)
    {
      if (hierarchy == null) return null;
      object project;

      try
      {
        hierarchy.GetProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_ExtObject, out project);
        return (project as Project);
      }
      catch
      {
        return null;
      }
    }


    void ProjectRemoved(Project project) {
      if (project == null) return;
      Logger.PublicEntry(() => {
        if (ProjectTrackers.ContainsKey(project.UniqueName))
          ProjectTrackers.Remove(project.UniqueName);
      }, "ProjectRemoved");
    }

    T GetService<T>() where T : class {
      return this.GetService(typeof(T)) as T;
    }

    void PublicEntryException(Exception exn, string entryName = "") {
      Contract.Requires(entryName != null);
      Contract.Requires(exn != null);
      try {
        if (entryName != null && exn != null)
          Logger.WriteAlways("{0} failed\n{1}", entryName, exn.ToString());
      } catch (Exception) { }
      try {
        OnExtensionFailed();
      } catch (Exception) { }
      try {
        if (_outputStream != null) {
          _outputStream.Flush();
          _outputStream.Close();
          _outputStream = null;
        }
        //TODO: What do we want to do with this on ship?
        if (entryName != null && exn != null && !_failOnce) {
          AskToReportError(exn);
        }
      } catch (Exception) { }
      _failOnce = true;
    }

    void OnExtensionFailed() {
#if DEBUG
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            System.Diagnostics.Debugger.Launch();
        }
        else
        {
            System.Diagnostics.Debugger.Break();
        }
#endif
      ExtensionHasFailed = true;
      NewCompilation = null;
      BuildDone = null;
      BuildBegin = null;

      var version = typeof(VSServiceProvider).Assembly.GetName().Version;
      var fileVersion = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo;
      //Write version number
      Logger.WriteAlways("Code Contracts Editor Extensions {0} {1} Visual Studio failed.",
        (version == null) ? "<no version>" : version.ToString(),
        (fileVersion == null) ? "<no file version>" : fileVersion.ProductVersion);

      //Print the work queue
      Logger.WriteToLog("Work queue:");
      foreach (var workItem in _workItems) {
        if (workItem == null) continue;
        try
        {
          var methodInfo = workItem.Method;

          Logger.WriteToLog("  " + methodInfo.Name);
        }
        catch { 
          // could be security exceptions on accessing the MethodInfo
        }
      }

      if (ExtensionFailed != null)
        ExtensionFailed();
    }


    void AskToReportError(Exception exn) {
      Contract.Requires(exn != null);

#if LEADERBOARD
      //Report to leaderboard
      LeaderBoard.LeaderBoardAPI.SendLeaderBoardFailure(LeaderBoardToolId, typeof(VSServiceProvider).Assembly.GetName().Version);
#endif

      var messageBoxMessage = new StringBuilder();
      messageBoxMessage.Append("An exception was thrown while the Code Contracts Editor Extension was running. Please assist our development team by sending the crash details. \n\nSend crash details?");
      var result = System.Windows.MessageBox.Show(messageBoxMessage.ToString(), "Sorry!", System.Windows.MessageBoxButton.YesNoCancel, System.Windows.MessageBoxImage.Information, System.Windows.MessageBoxResult.OK, System.Windows.MessageBoxOptions.None);
      if (result == System.Windows.MessageBoxResult.Yes) {
        string logLocation = null;
        var appDataDir = Environment.GetEnvironmentVariable("APPDATA");
        if (appDataDir != null)
          logLocation = Path.Combine(appDataDir, @"Microsoft\CodeContacts", String.Format("ContractsHelperLog_{0}.txt", _processIdUsedByStreamWriter));
        if (logLocation == null || !File.Exists(logLocation))
          logLocation = String.Format(@"..\Users\<username>\AppData\Roaming\Microsoft\CodeContacts\ContractsHelperLog_" + _processIdUsedByStreamWriter + ".txt");
        var emailBody = new StringBuilder();
        emailBody.AppendLine("Hi Code Contracts user,");
        emailBody.AppendLine();
        emailBody.Append("\tSorry about the crash, it would greatly help our development team if you could attach to this email the log file found here: ");
        emailBody.AppendLine(logLocation);
        emailBody.AppendLine();
        emailBody.AppendLine("Also, if you have any additional details/comments, feel free to write them here: ");
        emailBody.AppendLine("------------------------------------------");
        emailBody.AppendLine();
        emailBody.AppendLine();
        emailBody.AppendLine("------------------------------------------");
        emailBody.AppendLine("Exception details:");
        emailBody.AppendLine(exn.ToString());
        emailBody.AppendLine();
        emailBody.AppendLine("------------------------------------------");
        emailBody.AppendLine(String.Format("Code Contracts Editor Extensions {0} {1} Visual Studio {2} Bug Report",
                                           typeof(VSServiceProvider).Assembly.GetName().Version,
#if DEBUG
                                                 "Debug"
#else
                                                 "Release"
#endif
                                           ));
        emailBody.AppendLine("------------------------------------------");
        emailBody.AppendLine();
        emailBody.AppendLine("Thanks for using Code Contracts!");
        try {
          string mailto =
              String.Format("mailto:{2}?Subject={0}&Body={1}",
                  Uri.EscapeDataString(String.Format("Code Contracts Editor Extensions v{0} crashed with {1}", typeof(VSServiceProvider).Assembly.GetName().Version, exn.GetType().FullName)),
                  Uri.EscapeDataString(emailBody.ToString()),
                  CrashMailRecepients
                  );

          System.Diagnostics.Process.Start(mailto);
        } catch (Exception exn3) {
          System.Windows.MessageBox.Show(
            String.Format(
            "Failed to send mail. Please send this repro issue manually to {0}. \nMore information: {1}: {2}\n\nCrash report:\n{3}",
            CrashMailRecepients, exn3.GetType().FullName, exn3.Message, emailBody.ToString()),
            "Code Contracts Editor Extensions: Failure to send mail", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }
      }
    }

    void SolutionOpened() {
      Logger.PublicEntry(() => {
        try {
          this._compilerHost = GetVersionedServicesFactory().CreateCompilerHost();
        } catch {
          this._compilerHost = null;
        }
        _solutionLoaded = true;
        //FilesNeedingNewSemanticInfo.Clear();
        //FilesNeedingNewSyntacticInfo.Clear();
      }, "SolutionOpened");
    }
    void SolutionClosed() {
      Logger.PublicEntry(() =>
      {
        _solutionLoaded = false;
        _v3Host = null;
        _v4Host = null;
        ProjectTrackers.Clear();
        if (_outputStream != null)
        {
          _outputStream.Flush();
          _outputStream.Close();
          _outputStream = null;
          //FilesNeedingNewSyntacticInfo.Clear();
          //FilesNeedingNewSemanticInfo.Clear();
        }
      }, "SolutionClosed");
    }
    void OnProjectBuildBegin(Project Project) {
      if (Project == null) return;

      Logger.PublicEntry(() => {
        var reportProjBuildBegin = this.BuildBegin;
        if (reportProjBuildBegin != null)
          reportProjBuildBegin(Project.UniqueName);
      }, "OnProjectBuildBegin");
    }
    void OnProjectBuildEnd(Project Project)
    {
      if (Project == null) return;

      Logger.PublicEntry(() =>
      {
        var handlers = this.BuildDone;
        if (handlers != null)
          handlers();
      }, "OnProjectBuildEnd");
    }
 
    void DoOnCondition(Action action, Func<bool> condition, bool requeueIfConditionIsntMet = true) {
      Contract.Requires(action != null);
      Contract.Requires(condition != null);

      if (condition())
        action();
      else if (requeueIfConditionIsntMet)
        QueueWorkItem(action, condition, requeueIfConditionIsntMet);
    }
    public void QueueWorkItem(Action action, Func<bool> condition, bool requeueIfConditionIsntMet = true) {
      Contract.Requires(action != null);
      Contract.Requires(condition != null);

      QueueWorkItem(() => DoOnCondition(action, condition, requeueIfConditionIsntMet));
    }
    public void QueueWorkItem(Action action) {
      Contract.Requires(action != null);

      if (action == null) {
        Logger.WriteToLog("Warning: null action passed to 'QueueWorkItem'!");
        return;
      }

      _workItems.Enqueue(action);
    }
    public void AskForNewVSModel() {
     if (!_isGettingVSModel) {
        QueueWorkItem(GetNewModel);
        _isGettingVSModel = true;
      }
    }
    void GetNewModel() {
      _isGettingVSModel = false;
      if (InBuild || _compilerHost == null || _compilerHost.Compilers == null) {
        // AskForNewVSModel();
        return;
      }
      var startTime = DateTime.Now;
      var compilations = 0;
      try
      {
          Logger.WriteToLog("GetNewModel start");
          foreach (var compiler in _compilerHost.Compilers)
          {
            if (compiler == null) continue;
#if false
              foreach (var sourceFile in compiler.SourceFiles.Values)
              {
                  var reportNewSourceFile = this.NewSourceFile;
                  if (reportNewSourceFile != null)
                  {
                      sources++;
                      reportNewSourceFile(sourceFile);
                  }
              }
#endif
              var compilation = compiler.GetCompilation();
              var reportNewCompilation = this.NewCompilation;
              if (reportNewCompilation != null)
              {
                  compilations++;
                  reportNewCompilation(compilation);
              }
          }
      }
      catch (InvalidOperationException exn)
      {
          if (exn.Message.Contains(InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate))
          {
              //For some reason, occasionaly InvalidOperationExceptions can be thrown when operating on the semantic tree.
              Logger.WriteToLog("The Visual Studio Semantic/Syntactic model threw an exception, it's snapshot is out of date.");
              AskForNewVSModel();
              return;
          }
          else
          {
              throw exn;
          }
      }
      catch (COMException exn)
      {
          if (exn.Message.Contains(COMExceptionMessage_BindingFailed))
          {
              //For some reason, occasionaly InvalidOperationExceptions can be thrown when operating on the semantic tree.
              Logger.WriteToLog("The Visual Studio Semantic/Syntactic model threw an exception, it's snapshot is out of date.");
              AskForNewVSModel();
              return;
          }
          else
          {
              throw exn;
          }
      }
      finally
      {
          //Print our elapsed time for preformance considerations
          var elapseTime = DateTime.Now - startTime;
          VSServiceProvider.Current.Logger.WriteToLog(String.Format("Time to get semantic model for {0} compilations: {1}ms", compilations, elapseTime.Milliseconds));

      }
    }

    public int FDoIdle(uint grfidlef) {
      try {
        if (!_solutionLoaded || ExtensionHasFailed || InBuild)
          return 0;
        var startTime = DateTime.Now;
        //bool idleNow = (grfidlef & (uint)_OLEIDLEF.oleidlefPeriodic) > 0;
        if (_workItems.Count > 0) {
          Action workItem = null;
          workItem = _workItems.Dequeue();
          if (workItem != null) {
            workItem();
            Logger.WriteToLog("Idle loop ran action in {1}ms, {0} remaining", _workItems.Count.ToString(), (DateTime.Now - startTime).Milliseconds.ToString());
          } else {
            Logger.WriteToLog("Warning: Null action found on queue! Item not executed.");
          }
        }
      } catch (Exception exn) {
        PublicEntryException(exn, "FDoIdle");
      }
      return 0;
    }

#if false
    /// <summary>
    /// Don't call this!!!
    /// </summary>
    [Obsolete]
    static public bool IsModelReady(IDECompilerHost compilerHost) {
      Contract.Requires(compilerHost != null);

      try {

        if (compilerHost == null || compilerHost.Compilers == null)
          return false;


        foreach (var compiler in compilerHost.Compilers) {
          var comp = compiler.GetCompilation();

          foreach (var sourceFile in compiler.SourceFiles.Values) {
            var parseTree = sourceFile.GetParseTree();
            var rootNode = parseTree.RootNode;
          }
        }

      } catch (InvalidOperationException e) {
        if (e.Message.Contains(InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate))
          return false;
        else
          throw e;
      } catch (COMException e) {
        if (e.Message.Contains(COMExceptionMessage_BindingFailed))
          return false;
        else
          throw e;
      }

      return true;
    }
#endif
    #region Not Implemented
    public int FContinueMessageLoop(uint uReason, IntPtr pvLoopData, MSG[] pMsgPeeked) { return 1; }
    public int FPreTranslateMessage(MSG[] pMsg) { return 1; }
    public int FQueryTerminate(int fPromptUser) { return 1; }
    public int FReserved1(uint dwReserved, uint message, IntPtr wParam, IntPtr lParam) { return 1; }
    public IntPtr HwndGetWindow(uint dwWhich, uint dwReserved) { return IntPtr.Zero; }
    public void OnActivationChange(IOleComponent pic, int fSameComponent, OLECRINFO[] pcrinfo, int fHostIsActivating, OLECHOSTINFO[] pchostinfo, uint dwReserved) { }
    public void OnAppActivate(int fActive, uint dwOtherThreadID) { }
    public void OnEnterState(uint uStateID, int fEnter) { }
    public void OnLoseActivation() { }
    public void Terminate() { }
    #endregion

    #region IVsUpdateSolutionEvents

    int IVsUpdateSolutionEvents2.UpdateProjectCfg_Begin(IVsHierarchy pHierProj, IVsCfg pCfgProj, IVsCfg pCfgSln, uint dwAction, ref int pfCancel)
    {
      OnProjectBuildBegin(GetProject(pHierProj));
      return 0;
    }

    int IVsUpdateSolutionEvents2.UpdateProjectCfg_Done(IVsHierarchy pHierProj, IVsCfg pCfgProj, IVsCfg pCfgSln, uint dwAction, int fSuccess, int fCancel)
    {
      OnProjectBuildEnd(GetProject(pHierProj));
      return 0;
    }


    public int OnActiveProjectCfgChange(IVsHierarchy pIVsHierarchy)
    {
      try
      {
        if (pIVsHierarchy == null)
        {
          // solution?
          if (this._v3Host != null) { this._v3Host.ConfigurationChanged(); }
          if (this._v4Host != null) { this._v4Host.ConfigurationChanged(); }
          if (this._v45Host != null) { this._v45Host.ConfigurationChanged(); }
        }
        var project = GetProject(pIVsHierarchy);
        if (project != null && this.ProjectTrackers != null)
        {
          ProjectTracker tracker;
          if (this.ProjectTrackers.TryGetValue(project.UniqueName, out tracker))
          {
            Contract.Assume(tracker != null, "non-null only dictionary");

            tracker.OnProjectConfigChange();
            tracker.OnBuildBegin(project.UniqueName);
            tracker.OnBuildDone();
          }
        }
#if false
        if (project != null)
        {
          OnProjectBuildBegin(project);
          OnProjectBuildEnd(project);
          // pretend its a build
        }
#endif
      }
      catch { }
      return 0;
    }

    public int UpdateSolution_Begin(ref int pfCancelUpdate)
    {
      InBuild = true;
      return 0;
    }

    public int UpdateSolution_Cancel()
    {
      //InBuild = false;
      return 0;
    }

    public int UpdateSolution_Done(int fSucceeded, int fModified, int fCancelCommand)
    {
      InBuild = false;
      return 0;
    }

    public int UpdateSolution_StartUpdate(ref int pfCancelUpdate)
    {
      return 0;
    }
    #endregion

    #region IVsSolutionEvents
    int IVsSolutionEvents.OnAfterCloseSolution(object pUnkReserved)
    {
      return 0;
    }

    int IVsSolutionEvents.OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
    {
      return 0;
    }

    int IVsSolutionEvents.OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
    {
      return 0;
    }

    int IVsSolutionEvents.OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
    {
      SolutionOpened();
      return 0;
    }

    int IVsSolutionEvents.OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
    {
      return 0;
    }

    int IVsSolutionEvents.OnBeforeCloseSolution(object pUnkReserved)
    {
      SolutionClosed();
      return 0;
    }

    int IVsSolutionEvents.OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
    {

      ProjectRemoved(GetProject(pRealHierarchy));
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

  }
  internal enum LeaderBoardMasks {
    Zero = 0x0,
    //4th nibble
    CodeContractsHelperId = VSServiceProvider.LeaderBoardMask,
    //1st & 2nd bit
    InheritanceNoneOption = 0x0,
    InheritanceMethodsOption = 0x1,
    InheritancePropertiesOption = 0x1 << 1,
    InheritanceAllOption = InheritanceMethodsOption | InheritancePropertiesOption,
    //3rd bit
    MetadataOption = 0x1 << 2,
    //4th bit
    QuickInfoOption = 0x1 << 3,
    //5th bit
    SignatureHelpOption = 0x1 << 4,
    //6th bit
    LoggingOption = 0x1 << 5,
    //7th bit
    SmartFormattingOption = 0x1 << 6,
    //8th bit
    SyntaxColoringOption = 0x1 << 7,
    //9th bit
    CachingOption = 0x1 << 8
  }

}
