// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            if (verbose)
            {
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
            if (forWrite)
            {
                return key.CreateSubKey(subKey);
            }
            else
            {
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