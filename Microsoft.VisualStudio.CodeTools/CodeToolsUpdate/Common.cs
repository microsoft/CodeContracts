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
using Microsoft.Win32;
using System.Windows.Forms;

namespace CodeToolsUpdate
{
	static internal class Common
	{
		
		#region Utilities

    static public bool verbose = false;

		static public void Trace(string message)
		{
#if !NO_CONSOLE
      if (verbose) {
        Console.WriteLine("Codetools: " + message);
      }
#endif
      System.Diagnostics.Trace.WriteLine("CodeTools: Trace: " + message);
		}

		static public void Message(string message)
		{
#if !NO_CONSOLE
			Console.WriteLine(message + ".");
#endif
			System.Diagnostics.Trace.WriteLine("CodeTools: Message: " + message);
		}

		static public void ErrorMessage(string message)
		{
			Message(message);
			MessageBox.Show("CodeTools Error: " + message);
		}

		private static RegistryKey OpenKey(RegistryKey key, string subKey, bool forWrite)
		{
			if (forWrite) {
				return key.CreateSubKey(subKey);
			}
			else {
				return key.OpenSubKey(subKey, forWrite);
			}
		}

		// The Local Machine VS registry root: HKEY_LOCAL_MACHINE\Software\Microsoft\VisualStudio\10.0
		static public RegistryKey GetLocalRegistryRoot(string vsRoot, string subKey)
		{
			return GetLocalRegistryRoot(vsRoot, subKey, false);
		}

		// The user configuration VS registry root: use this for writing to the VS root
		// For us, this is equal to the local machine registry root since VS routes these
		// magically to HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\10.0_Config
		public static RegistryKey GetLocalRegistryRoot(string vsRoot, string subKey, bool forWrite)
		{
			return OpenKey(Registry.LocalMachine, vsRoot + "\\" + subKey, forWrite);
		}

		// The user VS registry root: HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\10.0
		internal static RegistryKey GetUserRegistryRoot(string vsRoot, string subKey, bool forWrite)
		{
			return OpenKey(Registry.CurrentUser, vsRoot + "\\" + subKey, forWrite);
		}
		#endregion
	}
}