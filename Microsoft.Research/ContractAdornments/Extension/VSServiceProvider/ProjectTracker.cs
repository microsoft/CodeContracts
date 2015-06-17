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
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using ContractAdornments.Interfaces;
using Microsoft.Cci;
using VSLangProj;

namespace ContractAdornments {
  public class ProjectTracker : IProjectTracker {
    public const string COMExceptionMessage_ProjectUnavailable = "Project unavailable.";

    readonly VSLangProj.VSProject VS_Project;
    readonly EnvDTE.Project EnvDTE_Project;

    private readonly IContractsProvider _contractsProvider;

    public IContractsProvider ContractsProvider
    {
      get
      {
        Contract.Ensures(Contract.Result<IContractsProvider>() != null);
        return this._contractsProvider;
      }
    }

    public References References { get { return VS_Project.References; } }

    public int BuildNumber { get; private set; }
    public bool InBuild { get; private set; }

    private readonly NonlockingHost _host;

    public NonlockingHost Host
    {
      get
      {
        Contract.Ensures(Contract.Result<NonlockingHost>() != null);
        return this._host;
      }
    }
    INonlockingHost IProjectTracker.Host
    {
        get
        {
            return Host;
        }
    }

    public AssemblyIdentity AssemblyIdentity { get; private set; }
    int _assemblyIdentityRevaluations = 0;
    const int _maxAssemblyIdentityRevaluations = 5;

    public readonly string UniqueProjectName;
    public string ProjectName { get; private set; }

    public event Action BuildBegin;
    public event Action BuildDone;

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
        Contract.Invariant(this.EnvDTE_Project != null);
        Contract.Invariant(this._contractsProvider != null);
        Contract.Invariant(this.VS_Project != null);
        Contract.Invariant(this._host != null);
    }

    #region Static getters
    public static ProjectTracker GetOrCreateProjectTracker(VSProject vsProject) {

      Contract.Requires(vsProject != null);

      ProjectTracker projectTracker;
      if (!ProjectIsAvailable(vsProject.Project)) {
        VSServiceProvider.Current.Logger.WriteToLog("Error: Project is not available! We can't create a 'ProjectTracker'!");
        return null;
      }
      if (!VSServiceProvider.Current.ProjectTrackers.TryGetValue(vsProject.Project.UniqueName, out projectTracker)) {
        projectTracker = new ProjectTracker(vsProject);
        if (vsProject.Project != null && vsProject.Project.UniqueName != null)
        {
          VSServiceProvider.Current.ProjectTrackers[vsProject.Project.UniqueName] = projectTracker;
        }
        else
        {
          // issue: project has no name, so we can't store it.
        }
      }
      return projectTracker;
    }
    #endregion

    private ProjectTracker(VSProject vsProject) {
      Contract.Requires(vsProject != null);
      Contract.Requires(vsProject.Project != null);
      Contract.Requires(ProjectIsAvailable(vsProject.Project));

      //Keep a pointer to our project
      VS_Project = vsProject;
      EnvDTE_Project = vsProject.Project;

      Contract.Assume(EnvDTE_Project.Properties != null);

      //Get our name
      ProjectName = EnvDTE_Project.Name;
      UniqueProjectName = EnvDTE_Project.UniqueName;

      //Get our host
      Version frameworkVersion = null;
      frameworkVersion = GetTargetFramework(EnvDTE_Project);
      VSServiceProvider.Current.Logger.WriteToLog("fx version: " + frameworkVersion.ToString());

      this._host = VSServiceProvider.Current.GetHost(frameworkVersion);

      //Eagerly add our project's references to the host's lib paths
      AddProjectReferencesPathsIntoHost(References, Host);

      //Subscribe to build events
      VSServiceProvider.Current.BuildBegin += OnBuildBegin;
      VSServiceProvider.Current.BuildDone += OnBuildDone;

      //Initialize build variables
      BuildNumber = 0;
      InBuild = false;

      //Get our assembly identity
      AssemblyIdentity = GetAssemblyIdentity(EnvDTE_Project, Host);

      //Set the contracts provider
      _contractsProvider = VSServiceProvider.Current.GetVersionedServicesFactory().CreateContractsProvider(this);
    }

    public void OnProjectConfigChange()
    {
      if (this.Host != null && this.AssemblyIdentity != null)
      {
        this.Host.RemoveUnit(this.AssemblyIdentity);
      }
    }

    public void OnBuildBegin(string projectName) {

      //Is this build notification relevant to us?
      if (projectName == null || UniqueProjectName != projectName)
        return;

      //Update our in build status
      InBuild = true;
      VSServiceProvider.Current.Logger.WriteToLog("Build beginning for: " + projectName);
      this._contractsProvider.Clear();
      var reportBuildBegin = BuildBegin;
      if (reportBuildBegin != null)
        reportBuildBegin();
    }
    public void OnBuildDone() {
      //Contract.Requires(InBuild); saw a runtime violation and it seems like the next line is fine if it is false.

      //Is this build notification relvant to us?
      if (!InBuild)
        return;

      //Update our in build status
      InBuild = false;

      //Update our build number
      BuildNumber++;
      VSServiceProvider.Current.Logger.WriteToLog("Build done. Build number: " + BuildNumber);

      //Assembly may have changed after build
      AssemblyIdentity = null;
      _assemblyIdentityRevaluations = 0;
      VSServiceProvider.Current.QueueWorkItem(() => {
        try {
          ReloadAssemblyIdentity();
        } catch (COMException e) {
          if (e.Message.Contains("Project unavailable")) {
            VSServiceProvider.Current.Logger.WriteToLog("Project unavailable after build.");
            if (_assemblyIdentityRevaluations < _maxAssemblyIdentityRevaluations) {
              VSServiceProvider.Current.QueueWorkItem(ReloadAssemblyIdentity);
            } else
              VSServiceProvider.Current.Logger.WriteToLog("Warning: Project [" + ProjectName +"]'s assembly identity could not be reloaded! We've tried " + _maxAssemblyIdentityRevaluations + " times!");
          } else
            throw e;
        }
      });

      var reportBuildDone = BuildDone;
      if (reportBuildDone != null)
        reportBuildDone();

    }

    void ReloadAssemblyIdentity() {
      _assemblyIdentityRevaluations++;
      AssemblyIdentity = GetAssemblyIdentity(EnvDTE_Project, Host);//Reload the assembly identity because the output folder configuration may have changed.
      if (AssemblyIdentity != null && !string.IsNullOrEmpty(AssemblyIdentity.Location)) {
        var loadedAssemblyIdentity = Host.ReloadIfLoaded(AssemblyIdentity);//Reload the assembly if it is aleady loaded because it may have changed during the last build.
        if (loadedAssemblyIdentity != null) {
          AssemblyIdentity = loadedAssemblyIdentity;
          VSServiceProvider.Current.Logger.WriteToLog("Successfully revaluated the assembly identity.");
        }
      } else
        VSServiceProvider.Current.Logger.WriteToLog("A proper assembly identity could not be constructed, unable to reload assembly.");
    }

    public static void AddProjectReferencesPathsIntoHost(References references, MetadataReaderHost host) {
      Contract.Requires(host != null);

      if (references == null) return;
      
      for (int i = 1; i <= references.Count; i++) {
        var tempRef = references.Item(i);
        if (tempRef == null) continue;

        var refPath = tempRef.Path;
        if (!String.IsNullOrEmpty(refPath)) {
          var refDir = Path.GetDirectoryName(refPath);
          if (refDir != null) {
            host.AddLibPath(refDir);
            var referenceAssemblyPath = Path.Combine(refDir, "CodeContracts");
            if (System.IO.Directory.Exists(referenceAssemblyPath))
              host.AddLibPath(referenceAssemblyPath);

          }
        }
      }
    }
    [Pure]
    public static bool ProjectIsAvailable(EnvDTE.Project project) {
      Contract.Ensures(!Contract.Result<bool>() || project != null);
      
      if (project == null)
        return false;
      
      try {
        var projectName = project.Name;
      } catch (COMException e) {
        if (e.Message.Contains(COMExceptionMessage_ProjectUnavailable))
          return false;
        else
          throw e;
      }
      return true;
    }
    public static AssemblyIdentity GetAssemblyIdentity(EnvDTE.Project project, MetadataReaderHost host) {
      Contract.Requires(project != null);
      Contract.Requires(host != null);

      if (!ProjectIsAvailable(project))
        return null;

      VSServiceProvider.Current.Logger.WriteToLog("Getting the assembly identity for project: " + project.Name);

      string location_RootDir = null;
      string location_RelativeAssemblyDir = null;
      string location_FileName = null;
      string location = null;
      IName iName = null;
      string culture = "";//TODO: Find out where to get culture information.
      Version version = new Version();
      AssemblyIdentity result = null;
      try {
        var activePropCount = 
          project.ConfigurationManager != null && project.ConfigurationManager.ActiveConfiguration != null && project.ConfigurationManager.ActiveConfiguration.Properties != null ?
          project.ConfigurationManager.ActiveConfiguration.Properties.Count : 0;
        for (int i = activePropCount; 1 <= i; i--) {
          var prop = project.ConfigurationManager.ActiveConfiguration.Properties.Item(i);
          if (prop == null) continue;
          if (prop.Name == "OutputPath") {
            location_RelativeAssemblyDir = prop.Value as string;
            break;
          }
        }
        var propCount = project.Properties != null ? project.Properties.Count : 0;
        for (int i = propCount; 1 <= i; i--) {
          var prop = project.Properties.Item(i);
          if (prop == null) continue;
          switch (prop.Name) {
            case "AssemblyName":
              iName = host.NameTable.GetNameFor(prop.Value as string);
              break;
            case "AssemblyVersion":
              var stringVersion = prop.Value as string;
              if (!Version.TryParse(stringVersion, out version))
              {
                  version = new Version();
              }
              Contract.Assume(version != null);
              break;
            case "FullPath":
              location_RootDir = prop.Value as string;
              break;
            case "OutputFileName":
              location_FileName = prop.Value as string;
              break;
            default:
              break;
          }
        }
      } catch (COMException comEx) {
        VSServiceProvider.Current.Logger.WriteToLog("COM Exception while trying to access project's properties.");
        VSServiceProvider.Current.Logger.WriteToLog("Message: " + comEx.Message);
        if (comEx.Message.Contains(COMExceptionMessage_ProjectUnavailable)) {
          VSServiceProvider.Current.Logger.WriteToLog("Returning null.");
          return null;
        } else if (((uint)comEx.ErrorCode) == 0x80020009) {
          VSServiceProvider.Current.Logger.WriteToLog("Returning null.");
          return null;
        } else {
          throw comEx;
        }
      }

      //Check if we got enough information from VS to build the location
      if (location_FileName == null
        || location_RelativeAssemblyDir == null
        || location_RootDir == null) {
        VSServiceProvider.Current.Logger.WriteToLog("Couldn't find path to the project's output assembly.");
        return null;
      }

      //Set the location of the output assembly
      location = Path.Combine(location_RootDir, location_RelativeAssemblyDir, location_FileName);

      //Check that the output assembly exists
      if (!File.Exists(location)) {
        VSServiceProvider.Current.Logger.WriteToLog("Project output assembly could not be found at the location given by Visual Studio: " + location);
      }

      //Check our other information from VS
      if (iName == null
        || string.IsNullOrEmpty(location)) {
        VSServiceProvider.Current.Logger.WriteToLog("Couldn't gather sufficient information from the project to construct an assembly identity.");
        return null;
      }

      //Success
      Contract.Assert(version != null);
      Contract.Assert(culture != null);
      result = new AssemblyIdentity(iName, culture, version, Enumerable<byte>.Empty, location);
      host.AddLibPath(Path.Combine(location_RootDir, location_RelativeAssemblyDir, "CodeContracts"));
      host.AddLibPath(Path.Combine(location_RootDir, location_RelativeAssemblyDir, @"..\Debug\CodeContracts"));

      return result;
    }
    public static Version GetTargetFramework(EnvDTE.Project project) {
      Contract.Requires(project != null);
      Contract.Ensures(Contract.Result<Version>() != null);
      try {

        if (project.Properties == null)
        {
          return new Version(4, 5);
        }
        
        var fw = project.Properties.Item("TargetFramework");
        if (fw == null || !(fw.Value is uint)) {
          return new Version(4, 5);
        }

        uint fxValue = (uint)fw.Value;
        VSServiceProvider.Current.Logger.WriteToLog("TargetFramework as uint: " + fxValue.ToString());
        FrameworkVersion fv = (FrameworkVersion)fxValue;
        switch (fv) {
          case FrameworkVersion.v2:
            return new Version(2, 0);
          case FrameworkVersion.v3:
            return new Version(3, 0);
          case FrameworkVersion.v3_5:
            return new Version(3, 5);
          case FrameworkVersion.v4:
            return new Version(4, 0);
          case FrameworkVersion.v4_5:
            return new Version(4, 5);
          default:
            return new Version(4, 5);//null;
        }
      } catch (InvalidCastException) {
        return new Version(4, 5);
        //return null;
      }
    }
  }

  internal enum FrameworkVersion {
    v2 = 0x20000,
    v3 = 0x30000,
    v3_5 = 0x30005,
    v4 = 0x40000,
    v4_5 = 0x40005,
  }
}
