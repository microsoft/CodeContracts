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
  using System.Collections.Generic;
  using Microsoft.VisualStudio.Shell.Interop;
  using System.Diagnostics;
	using Microsoft.Win32;
	using System.IO;
  
  // Common static helper functions
	static internal class Common
	{
		#region Private fields
		static private IServiceProvider serviceProvider;
		#endregion

		#region Construction
		static public void Initialize(IServiceProvider provider)
		{
			serviceProvider = provider;
		}

		static public void Release()
		{
			serviceProvider = null;
		}
		#endregion

		#region Trace and GetService
		[Conditional("TRACE")]
		static public void Trace(string message)
		{
			System.Diagnostics.Trace.WriteLine("CodeTools: " + message);
		}

		static public object GetService(Type serviceType)
		{
			if (serviceProvider != null)
				return serviceProvider.GetService(serviceType);
			else
				return null;
		}
#endregion

		#region Registry stuff
		static private string VsRoot = null;
		static public String GetLocalRegistryRoot()
		{
			// Get the visual studio root key
			if (VsRoot == null) {
				ILocalRegistry2 localReg = GetService(typeof(ILocalRegistry)) as ILocalRegistry2;
				if (localReg != null) {
					localReg.GetLocalRegistryRoot(out VsRoot);
				}

				if (VsRoot == null) {
					VsRoot = @"Software\Microsoft\VisualStudio\10.0"; // Guess on failure
				}
			}
			return VsRoot;
		}

		static public String GetCodeToolsRegistryRoot()
		{
			return (GetLocalRegistryRoot() + "\\CodeTools");
		}
		

		#endregion
	}
}
