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
using System.Threading;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts 
{
  sealed class RunCCheckInContextMenuCommand
      : EditorMenuCommand
  {
    public RunCCheckInContextMenuCommand(ContractsVsPackage package)
      : base(package, IdTable.ContractsVsPackageCmdSet.RunCCheckInContextMenuCommand)
    {
      Contract.Requires(package != null);
    }

    protected override void Execute()
    {
      if (VsShellUtilities.IsSolutionBuilding(this.Package))
        return;

      try
      {
         
        UIThreadInvoker.Invoke( // Use it to force the execution in the UI thread 
          delegate
        {
          var EnvVars = new Dictionary<string, string>();

          Project project;
          string projectGuid;
          if (!ProjectHelper.TryGetActiveProject(this.Package, out project) || !ProjectHelper.TryGetProjectGuid(this.Package, project, out projectGuid))
          {
            MessageBox.Show(
                        "Code Contracts could not detect the current project",
                        "Code Contracts: Could not detect the current project",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            return;
          }

          EnvVars["CodeContractsTargetProjectGuid"] = projectGuid;

          string name;
          if (ContextHelper.TryGetSelectedMemberFullNameMangled(this.Package, out name))
          {
            EnvVars["CodeContractsTargetMember"] = name;
          }
          else if (ContextHelper.TryGetSelectedTypeFullNameMangled(this.Package, out name))
          {
            EnvVars["CodeContractsTargetType"] = name;
          }
          else if (ContextHelper.TryGetSelectedNamespaceFullNameMangled(this.Package, out name))
          {
            EnvVars["CodeContractsTargetNamespace"] = name;
          }
          else
          {
            MessageBox.Show(
                        "Code Contracts could not detect the selected type, member or namespace. " +
                        "Please make sure that the editor caret is inside the member to test.",
                        "Code Contracts: Could not detect the code context",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            return;
          }
          
          ThreadPool.QueueUserWorkItem(o =>
          {
            try
            {
              foreach (var p in EnvVars)
                Environment.SetEnvironmentVariable(p.Key, p.Value);

              ProjectHelper.TryBuildProject(this.Package, project);
            }
            catch (Exception e)
            {
              ContractsVsPackage.ReportException(e);
            }
            finally
            {
              foreach (var p in EnvVars)
                Environment.SetEnvironmentVariable(p.Key, null);
            }
          });
        });
      }
      catch (Exception ex)
      {
        ContractsVsPackage.ReportException(ex);
      }
    }
  }
}
