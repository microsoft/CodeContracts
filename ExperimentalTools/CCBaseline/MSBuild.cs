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
using System.Diagnostics.Contracts;
using System.IO;
using System.Xml.Linq;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;

namespace CCBaseline
{
  class MSBuild : IBuilder
  {
    public static MSBuild Factory(string sourceDirectory)
    {
      return new MSBuild { SourceDirectory = sourceDirectory };
    }

    public string SourceDirectory { get; private set; }
    public bool SaveSemanticBaseline { get; set; }
    public string ExtraOptions { get; set; }

    public bool Build(IEnumerable<string> ProjectsPath, string versionName, string outputPrefix)
    {
      var ret = false;

      try
      {
        // Properties passed to MSBUILD (the ones of the .target file)
        var globalProperties = new Dictionary<string, string>();

        // INFERENCE off
        globalProperties.Add("CodeContractsInferRequires", "false");
        globalProperties.Add("CodeContractsInferEnsures", "false");
        globalProperties.Add("CodeContractsInferObjectInvariants", "false");
        // SUGGESTIONS off
        globalProperties.Add("CodeContractsSuggestAssumptions", "false");
        globalProperties.Add("CodeContractsSuggestRequires", "false");
        globalProperties.Add("CodeContractsSuggestEnsures", "false");
        globalProperties.Add("CodeContractsSuggestObjectInvariants", "false");

        // WARNINGS
        globalProperties.Add("CodeContractsRunCodeAnalysis", "true");
        globalProperties.Add("CodeContractsAnalysisWarningLevel", "3");
        globalProperties.Add("CodeContractsNonNullObligations", "true");
        globalProperties.Add("CodeContractsBoundsObligations", "true");
        globalProperties.Add("CodeContractsArithmeticObligations", "true");
        globalProperties.Add("CodeContractsEnumObligations", "true");

        globalProperties.Add("CodeContractsRedundantAssumptions", "false");
        globalProperties.Add("CodeContractsMissingPublicRequiresAsWarnings", "false");

        globalProperties.Add("CodeContractsRunInBackground", "false"); // get output
        globalProperties.Add("CodeContractsEnableRuntimeChecking", "false"); // speedup
        globalProperties.Add("CodeContractsReferenceAssembly", "Build"); // make sure we have it
        //globalProperties.Add("CodeContractsUseBaseLine", "true");
        //globalProperties.Add("CodeContractsBaseLineFile", "base.xml");
        //globalProperties.Add("CodeContractsExtraAnalysisOptions", "-repro");

        globalProperties.Add("CodeContractsCacheAnalysisResults", "true");
        globalProperties.Add("CodeContractsSQLServerOption", "cloudotserver");

        var extraOptions = this.ExtraOptions == null ? "" : this.ExtraOptions;
        extraOptions += " -sortwarns:false -stats:perMethod -show:progress -trace:cache";

        if (this.SaveSemanticBaseline)
        {
          extraOptions += String.Format(" -saveSemanticBaseline:{0}", versionName);
        }
        else
        {
          extraOptions += String.Format(" -useSemanticBaseline:{0}", versionName);
        }
        globalProperties.Add("CodeContractsExtraAnalysisOptions", extraOptions);
        //globalProperties.Add("CodeContractsCacheVersion", VersionId.ToString());
        //globalProperties.Add("CodeContractsCacheMaxSize", Int32.MaxValue.ToString());

        globalProperties.Add("DeployExtension", "false"); // avoid vsix deployment

        // It does not work: The log file is empty
        var logFileName = "buildlog." + versionName + "." + outputPrefix + (this.SaveSemanticBaseline ? ".save.txt" : ".use.txt");
        var fileLogger = new FileLogger();
        fileLogger.Parameters = "logfile=" + Path.Combine(this.SourceDirectory, logFileName);
        fileLogger.Verbosity = Microsoft.Build.Framework.LoggerVerbosity.Normal;
        var consoleLogger = new ConsoleLogger();
        consoleLogger.Verbosity = Microsoft.Build.Framework.LoggerVerbosity.Normal;
        var overallLoggers = new[] { fileLogger, consoleLogger };
        // with this version, loggers don't work (log files are created but empty), why?

        var loggers = new FileLogger[0];

        using (var projectCollection = new ProjectCollection(globalProperties, loggers, ToolsetDefinitionLocations.Registry))
        {
          // The only way to communicate to msbuild is to create an xml file, and pass it to msbuild
          XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
          var xmlTarget = new XElement(ns + "Target", new XAttribute("Name", "Build"));

          var properties = "";

          foreach (var projectPath in ProjectsPath)
          {
            Contract.Assert(projectPath != null);
            xmlTarget.Add(new XElement(ns + "MSBuild",
              new XAttribute("Projects", Path.Combine(this.SourceDirectory, projectPath)),
              new XAttribute("Targets", "Rebuild"),  // ensures the project will be rebuilt from scratch (is it needed?)
              new XAttribute("UseResultsCache", "false"),
              new XAttribute("UnloadProjectsOnCompletion", "true"),
              new XAttribute("ContinueOnError", "true"),
              new XAttribute("StopOnFirstFailure", "false"),
              new XAttribute("Properties", properties)));
          }

          // create the project
          var xmlProject = new XElement(ns + "Project", xmlTarget);

          // now read the project
          var project = projectCollection.LoadProject(xmlProject.CreateReader());

          // create an instance of the project
          var projectInstance = project.CreateProjectInstance();

          // create the parameters for the build
          var buildParameters = new BuildParameters(projectCollection);
          buildParameters.EnableNodeReuse = false;
          buildParameters.ResetCaches = true;
          buildParameters.Loggers = overallLoggers;
          buildParameters.MaxNodeCount = Environment.ProcessorCount;
          // Ask a build on this project
          var buildRequestData = new BuildRequestData(projectInstance, new string[] { "Build" });

          // we need to create a new BuildManager each time, otherwise it wrongly caches project files
          var buildManager = new BuildManager();

          // now do the build!
          var buildResult = buildManager.Build(buildParameters, buildRequestData);
          ret = buildResult.OverallResult == BuildResultCode.Success;

#if WITH_LOG
          logger.Shutdown();
#endif

          // now cleanup - it seems it does not improve
          projectCollection.UnregisterAllLoggers();
          projectCollection.UnloadAllProjects();
        }

        #region Version using the default BuildManager (disabled)
#if false
        // First version. It does not work, because msbuild keeps a cache of the projects, and so it compiles the new files with the old project 
        // The Log works
        Microsoft.Build.Execution.BuildManager.DefaultBuildManager.ResetCaches(); // ensures MSBuild doesn't use cached version of project files (it handles very badly versions changing!), not sure it works

        XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
        var xmlTarget = new XElement(ns + "Target", new XAttribute("Name", "Build"));

        var properties = "";
        properties += ";CodeContractsCacheDirectory=" + OutputDirectory;
        properties += ";CodeContractsCacheAnalysisResults=true";
        properties += ";CodeContractsCacheVersion=" + VersionId;

        foreach (var projectPath in ProjectsPath)
        {
          Contract.Assume(projectPath != null);
          xmlTarget.Add(new XElement(ns + "MSBuild",
            new XAttribute("Projects", Path.Combine(this.WorkingDirectory, projectPath)),
            new XAttribute("Targets", "Rebuild"),  // ensures the project will be rebuilt from scratch (is it needed?)
            new XAttribute("UseResultsCache", "false"),
            new XAttribute("UnloadProjectsOnCompletion", "true"),
            new XAttribute("ContinueOnError", "true"),
            new XAttribute("StopOnFirstFailure", "false"),
            new XAttribute("Properties", properties)));
        }

        var xmlProject = new XElement(ns + "Project", xmlTarget);

        var project = new Project(xmlProject.CreateReader());

        var logFileName = "build." + VersionId + ".log";
        var logger = new Microsoft.Build.Logging.FileLogger();
        logger.Parameters = "logfile=" + Path.Combine(OutputDirectory, logFileName);
        ret = project.Build(logger);
#endif
        #endregion
      }
      catch
      {
        ret = false;
      }

      return ret;
    }
  }
}
