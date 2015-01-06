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
	using System.Collections.Generic;
  
  // The main package class
  #region Attributes
  [Microsoft.VisualStudio.Shell.DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\9.0")
  ,Microsoft.VisualStudio.Shell.ProvideLoadKey("Standard", "1.0", "Microsoft.VisualStudio.CodeTools.PropertyPage", "Microsoft", 1)
  ,Guid("072DD0C6-AE1E-4ed6-A0BF-B99D5B68D29E")]
  #endregion
  public sealed class PropertyPagePackage : Microsoft.VisualStudio.Shell.Package
  {
    #region Construction
		private PropertyPanes panes;

    protected override void Initialize()
    {
      base.Initialize();
      // Initialize common functionality
      Common.Initialize(this);
			// Store and COM register any user property panes
			panes = new PropertyPanes();			
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing) {
				panes.Release(); // COM unregister any user property panes
        Common.Release();
        GC.SuppressFinalize(this);
      }
      base.Dispose(disposing);
    }
    #endregion
  }
}
