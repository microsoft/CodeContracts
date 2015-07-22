// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.CodeTools
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.Shell.Interop;
    using System.Diagnostics;
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Threading;
    using System.IO;

    // Action without arguments
    internal delegate void Procedure();

    // Common static helper functions
    static internal class Common
    {
        #region Private fields
        static private IServiceProvider serviceProvider;
        static private IVsSolution solution;
        static private ISynchronizeInvoke syncInvoke;
        #endregion

        #region Construction
        static public void Initialize(IServiceProvider provider)
        {
            serviceProvider = provider;
            solution = GetService(typeof(SVsSolution)) as IVsSolution;

            // we must be called from the main UI thread
            syncInvoke = null;
            Form marshalingForm = new Form();
            if (marshalingForm != null)
            {
                marshalingForm.Visible = false;
                IntPtr handle = marshalingForm.Handle; // forces creation
                syncInvoke = marshalingForm;
            }
            else
            {
                Log("Unable to create a synchronization context for safe thread calls.");
            }
        }

        static public void Release()
        {
            solution = null;
            serviceProvider = null;
        }
        #endregion

        #region Utilities
        [Conditional("TRACE")]
        static public void Trace(string message)
        {
            System.Diagnostics.Debugger.Log(1, "Trace", "TaskManager: " + message + "\n");
        }

        static public void Log(string message)
        {
            System.Diagnostics.Debugger.Log(1, "Error", "TaskManager: " + message + "\n");
        }

        static public object GetService(Type serviceType)
        {
            if (serviceProvider != null)
                return serviceProvider.GetService(serviceType);
            else
                return null;
        }

        static private string VsRoot = null;
        static public String GetLocalRegistryRoot()
        {
            // Get the visual studio root key
            if (VsRoot == null)
            {
                ILocalRegistry2 localReg = GetService(typeof(ILocalRegistry)) as ILocalRegistry2;
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

        static public void ShowHelp(string helpKeyword)
        {
            if (helpKeyword != null && helpKeyword.Length > 0)
            {
                IVsHelpSystem help = GetService(typeof(SVsHelpService)) as IVsHelpSystem;
                if (help != null)
                {
                    help.KeywordSearch(helpKeyword, 0, 0);
                    help.ActivateHelpSystem(0);
                }
            }
        }

        static public void ThreadSafeInvoke(Procedure action)
        {
            if (syncInvoke != null && syncInvoke.InvokeRequired)
            {
                // Common.Trace("Use ISynchronized.Invoke");
                syncInvoke.Invoke(action, null);
            }
            else
            {
                action();
            }
        }

        static public void ThreadSafeASync(Procedure action)
        {
            if (syncInvoke != null && syncInvoke.InvokeRequired)
            {
                // Common.Trace("Use ISynchronized.Invoke");
                syncInvoke.BeginInvoke(action, null);
            }
            else
            {
                action();
            }
        }

        #endregion

        #region Projects
        static public Guid GetProjectType(IVsHierarchy hierarchy)
        {
            Guid projectType = Guid.Empty;
            if (hierarchy != null)
            {
                hierarchy.GetGuidProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_TypeGuid, out projectType);
            }
            return projectType;
        }

        static public string GetProjectName(IVsHierarchy hierarchy)
        {
            object name = null;
            if (hierarchy != null)
            {
                hierarchy.GetProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_Name, out name);
            }
            if (name != null)
            {
                return name as String;
            }
            else
            {
                return null;
            }
        }

        static public IVsHierarchy GetProjectByName(string projectName)
        {
            if (projectName != null && projectName.Length > 0 && solution != null)
            {
                foreach (IVsHierarchy project in GetProjects())
                {
                    if (String.Compare(projectName, GetProjectName(project), StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        return project;
                    }
                }
            }
            return null;
        }

        static public IEnumerable<IVsHierarchy> GetProjects()
        {
            if (solution != null)
            {
                IEnumHierarchies hiers;
                Guid emptyGuid = Guid.Empty;
                int hr = solution.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_ALLPROJECTS, ref emptyGuid, out hiers);
                if (hr == 0 && hiers != null)
                {
                    uint fetched;
                    do
                    {
                        IVsHierarchy[] hier = { null };
                        hr = hiers.Next(1, hier, out fetched);
                        if (hr == 0 && fetched == 1)
                        {
                            yield return (hier[0]);
                        }
                    }
                    while (fetched == 1);
                }
            }
        }

        #endregion
    }
}
