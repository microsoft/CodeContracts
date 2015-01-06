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
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts
{
  internal abstract class EditorMenuCommand
    : DynamicMenuCommand
  {
    protected EditorMenuCommand(ContractsVsPackage package, Guid commandSet, uint id)
      : base(package, commandSet, id)
    {
      Contract.Requires(package != null);
    }

    protected EditorMenuCommand(ContractsVsPackage package, uint id)
      : base(package, id)
    {
      Contract.Requires(package != null);
    }

    protected override void QueryStatus()
    {
      Project project;
      this.Visible =
          ProjectHelper.TryGetActiveProject(this.Package, out project) &&
          ProjectHelper.IsObjectModelSupported(project);
      this.Enabled =
          this.Visible
          && !VsShellUtilities.IsSolutionBuilding(this.Package);
    }
  }
}