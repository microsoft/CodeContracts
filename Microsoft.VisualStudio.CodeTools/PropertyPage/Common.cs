// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            if (VsRoot == null)
            {
                ILocalRegistry2 localReg = GetService(typeof(SLocalRegistry)) as ILocalRegistry2;
                if (localReg != null)
                {
                    localReg.GetLocalRegistryRoot(out VsRoot);
                }

                if (VsRoot == null)
                {
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
