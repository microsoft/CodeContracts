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

// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//   Code adapted from Pex
// 
// ==--==

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;
using Project = EnvDTE.Project;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Contracts
{
  internal static class ProjectHelper
  {
    public static class Properties
    {
      public const string ProjectGuid = "ProjectGuid";
    }

    public const string prjKindCSharpProject = "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
    public const string prjKindVBProject = "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}";

    public static bool IsObjectModelSupported(Project value)
    {
      Contract.Requires(value != null);
      return
          value.Kind == prjKindCSharpProject ||
          value.Kind == prjKindVBProject;
    }

    public static bool TryGetHierarchyForProject(
        IServiceProvider serviceProvider,
        Project project,
        out IVsHierarchy hierarchy)
    {
      Contract.Requires(serviceProvider != null);
      Contract.Requires(project != null);

      string uniqueName;
      try
      {
        uniqueName = project.UniqueName;
      }
      catch (Exception ex)
      {
        ContractsVsPackage.SwallowedException(ex);
        uniqueName = null;
      }

      if (uniqueName == null)
      {
        hierarchy = null;
        return false;
      }
      else
      {
        try
        {
          hierarchy = HierarchyFromUniqueName(
              serviceProvider,
              project.UniqueName);

          if (hierarchy == null)
          {
            return false;
          }

          return true;
        }
        catch (Exception ex)
        {
          ContractsVsPackage.SwallowedException(ex);
          hierarchy = null;
          return false;
        }
      }
    }

    private static IVsHierarchy HierarchyFromUniqueName(
        IServiceProvider serviceProvider,
        string uniqueName)
    {
      Contract.Requires(serviceProvider != null);
      Contract.Requires(uniqueName != null);

      var solution = VsServiceProviderHelper.GetService<IVsSolution>(serviceProvider);

      if (solution == null)
      {
        return null;
      }
      
      IVsHierarchy hierarchy;
      var hResult = solution.GetProjectOfUniqueName(uniqueName, out hierarchy);
      if (Microsoft.VisualStudio.ErrorHandler.Failed(hResult))
        throw Marshal.GetExceptionForHR(hResult);
      return hierarchy;
    }

    /// <summary>
    /// Tries to get a build property value
    /// </summary>
    /// <param name="project"></param>
    /// <param name="configuration">may be null for 'all' configuration</param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool TryGetBuildProperty(
        IServiceProvider serviceProvider,
        Project project,
        string configuration,
        string name,
        out string value)
    {
      Contract.Requires(serviceProvider != null);
      Contract.Requires(project != null);
      Contract.Requires(name != null);

      if (configuration == null) configuration = String.Empty;

      IVsHierarchy hierachy;
      if (!TryGetHierarchyForProject(serviceProvider, project, out hierachy))
      {
        value = null;
        return false;
      }

      IVsBuildPropertyStorage properties = hierachy as IVsBuildPropertyStorage;
      if (properties == null)
      {
        value = null;
        return false;
      }

      try
      {
        int hresult = properties.GetPropertyValue(
            name,
            configuration,
            (uint)_PersistStorageType.PST_USER_FILE,
            out value);
        if (Microsoft.VisualStudio.ErrorHandler.Succeeded(hresult))
          return true;
      }
      catch (Exception ex)
      {
        ContractsVsPackage.SwallowedException(ex);
      }

      try
      {
        int hresult = properties.GetPropertyValue(
            name,
            configuration,
            (uint)_PersistStorageType.PST_PROJECT_FILE,
            out value);
        return Microsoft.VisualStudio.ErrorHandler.Succeeded(hresult);
      }
      catch (Exception ex)
      {
        ContractsVsPackage.SwallowedException(ex);
      }

      value = null;
      return false;
    }

    public static bool TryGetActiveProject(IServiceProvider serviceProvider, out Project project)
    {
      Contract.Requires(serviceProvider != null);
      Contract.Ensures(Contract.ValueAtReturn(out project) != null || !Contract.Result<bool>());
      
      project = null;
      try
      {
        var dte = VsServiceProviderHelper.GetService<DTE>(serviceProvider);

        if (dte == null)
        {
          return false;
        }

        if (dte.Solution != null)
        {
          object[] projects = dte.ActiveSolutionProjects as object[];
          if (projects != null && projects.Length == 1)
            project = (Project)projects[0];
        }
        return project != null;
      }
      catch (Exception ex)
      {
        ContractsVsPackage.SwallowedException(ex);
      }

      return false;
    }

    public static bool TryGetProperty(
        IServiceProvider serviceProvider,
        Project project, string propertyName, out string value)
    {
      Contract.Requires(serviceProvider != null);
      Contract.Requires(project != null);
      Contract.Requires(propertyName != null);

      try
      {
        var properties = project.Properties;
        if (properties != null)
        {
          var property = project.Properties.Item(propertyName);
          if (property != null)
          {
            value = (string)property.Value;
            return true;
          }
        }
      }
      catch (Exception ex)
      {
        ContractsVsPackage.SwallowedException(ex);
      }

      // try msbuild properties
      return
          TryGetBuildProperty(
              serviceProvider,
              project,
              null,
              propertyName,
              out value);
    }

    public static void SaveAllFiles(IServiceProvider serviceProvider)
    {
      Contract.Requires(serviceProvider != null);

      DTE dte = VsServiceProviderHelper.GetService<DTE>(serviceProvider);
      if (dte != null)
      {
        dte.ExecuteCommand("File.SaveAll", "");
      }
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public static bool TryGetActiveSolutionConfiguration(
        IServiceProvider serviceProvider,
        out SolutionBuild solutionBuild,
        out SolutionConfiguration2 activeConfiguration)
    {
      Contract.Requires(serviceProvider != null);
      Contract.Ensures(Contract.ValueAtReturn(out solutionBuild) != null || !Contract.Result<bool>());
      Contract.Ensures(Contract.ValueAtReturn(out activeConfiguration) != null || !Contract.Result<bool>());

      solutionBuild = null;
      activeConfiguration = null;

      DTE dte = VsServiceProviderHelper.GetService<DTE>(serviceProvider);
      if (dte == null)
        return false;
      var solution = dte.Solution;
      if (solution == null)
        return false;
      solutionBuild = solution.SolutionBuild;
      Contract.Assume(solutionBuild != null);
      activeConfiguration = solutionBuild.ActiveConfiguration as SolutionConfiguration2;

      return solutionBuild != null && activeConfiguration != null;
    }

    public static bool TryBuildProject(ContractsVsPackage package, Project project)
    {
      Contract.Requires(package != null);
      Contract.Requires(project != null);

      SolutionBuild solutionBuild;
      SolutionConfiguration2 activeConfiguration;
      if (!TryGetActiveSolutionConfiguration(package, out solutionBuild, out activeConfiguration))
        return false;

      string configurationName = activeConfiguration.Name;
      string platformName = activeConfiguration.PlatformName;
      string buildName = configurationName + '|' + platformName;
      try
      {
        solutionBuild.BuildProject(
            buildName,
            project.UniqueName,
            true);
      }
      catch (Exception ex)
      {
        ContractsVsPackage.SwallowedException(ex);
        return false;
      }

      return
          solutionBuild.BuildState == vsBuildState.vsBuildStateDone &&
          solutionBuild.LastBuildInfo == 0;
    }

    public static bool TryGetProjectGuid(IServiceProvider serviceProvider, Project project, out string projectGuid)
    {
      Contract.Requires(serviceProvider != null);
      Contract.Requires(project != null);

      return ProjectHelper.TryGetProperty(serviceProvider, project, Properties.ProjectGuid, out projectGuid);
    }
  }
}
