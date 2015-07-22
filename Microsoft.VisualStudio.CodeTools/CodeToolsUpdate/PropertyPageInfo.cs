// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace CodeToolsUpdate
{
    internal class PropertyPageInfo
    {
        #region Fields
        private readonly CodeToolInfo tool;
        private readonly Guid clsid;
        private readonly Guid pageid;
        private readonly string category;           // CommonPropertyPages | ConfigPropertyPages
        private readonly List<string> projects;     // projects on which to register

        #endregion

        #region Constructor
        private PropertyPageInfo(CodeToolInfo tool, Guid clsid, Guid pageid, string _category, List<string> _projects)
        {
            this.tool = tool;
            this.clsid = clsid;
            this.pageid = pageid;
            category = _category;
            projects = _projects;

            // sensitize data from the registry
            if (category == null || category.Length == 0) category = commonPropertyPages;
            if (String.Compare(category, "Common", true) == 0 || String.Compare(category, commonPropertyPages, true) == 0)
            {
                category = commonPropertyPages;
            }
            if (String.Compare(category, "Config", true) == 0 || String.Compare(category, configPropertyPages, true) == 0)
            {
                category = configPropertyPages;
            }
            if (projects == null) projects = new List<string>();
        }

        public override bool Equals(object obj)
        {
            PropertyPageInfo p = obj as PropertyPageInfo;
            if (p == null) return false;

            // watch out for recursion -- just check ToolName and VsRoot
            if (tool.ToolName != p.tool.ToolName) return false;
            if (tool.VsRoot != p.tool.VsRoot) return false;

            if (clsid != p.clsid) return false;
            if (pageid != p.pageid) return false;
            if (category != p.category) return false;
            if (projects.Count != p.projects.Count) return false;
            for (int i = 0; i < projects.Count; i++)
            {
                if (projects[i] != p.projects[i]) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            // just a simple hash
            return (tool.ToolName.GetHashCode() * clsid.GetHashCode() * pageid.GetHashCode());
        }

        public override string ToString()
        {
            return (tool.DisplayName + " property page: " + pageid.ToString("B"));
        }
        #endregion

        #region Static methods: the registry magic guids for default property pages
        private const string configPropertyPages = "ConfigPropertyPages";
        private const string commonPropertyPages = "CommonPropertyPages";

        private const string fxcopGuid = "{984AE51A-4B21-44E7-822C-DD5E046893EF}";      // fxcop property page guid
        private const string fxcopPackage = "{72391CE3-743A-4a55-8927-4217541F6517}";
        private const string fxcopPackage11 = "{B20604B0-72BC-4953-BB92-95BF26D30CFA}"; // new fxcop package guid for VS2011

        private static Guid fsharpProject = new Guid("{F2A71F9B-5D33-465A-A702-920D77279786}");
        private static Guid csharpProject = new Guid("{FAE04EC0-301F-11d3-BF4B-00C04F79EFBC}");
        private static Guid vbProject = new Guid("{F184B08F-C81C-45F6-A57F-5ABD9991F28F}");

        private const string vsLoadOnSolution = "{F1536EF8-92EC-443C-9ED7-FDADF150DA82}";  // autoload key for loading on a solution load

        private static bool IsFxCopInstalled(string vsRoot)
        {
            return (null != Common.GetLocalRegistryRoot(vsRoot, "CLSID\\" + fxcopGuid));
        }

        private static List<string> GetDefaultPropertyPages(string vsRoot, Guid projectId, string category)
        {
            List<string> defaults = new List<string>();

            // C# project
            if (projectId == csharpProject)
            {
                if (category == configPropertyPages)
                {
                    defaults.Add("{A54AD834-9219-4AA6-B589-607AF21C3E26}"); // CBuildPropPg2Guid
                    defaults.Add("{6185191F-1008-4FB2-A715-3A4E4F27E610}"); // CDebugPropPg2Guid
                    if (IsFxCopInstalled(vsRoot)) defaults.Add(fxcopGuid);
                }
                else if (category == commonPropertyPages)
                {
                    defaults.Add("{5E9A8AC2-4F34-4521-858F-4C248BA31532}"); // Application
                    defaults.Add("{43E38D2E-43B8-4204-8225-9357316137A4}"); // Services
                    defaults.Add("{031911C8-6148-4e25-B1B1-44BCA9A0C45C}"); // Reference Paths
                    defaults.Add("{F8D6553F-F752-4DBF-ACB6-F291B744A792}"); // Signing
                    defaults.Add("{1E78F8DB-6C07-4d61-A18F-7514010ABD56}"); // Build Events

                    // not for type libraries -- but there is no way for us to do that... :-(
                    defaults.Add("{DF8F7042-0BB1-47D1-8E6D-DEB3D07698BD}"); // Security
                    defaults.Add("{CC4014F5-B18D-439C-9352-F99D984CCA85}"); // Publish
                }
            }
            // VB project
            else if (projectId == vbProject)
            {
                if (category == configPropertyPages)
                {
                    defaults.Add("{6185191F-1008-4FB2-A715-3A4E4F27E610}"); // Debug
                    defaults.Add("{EDA661EA-DC61-4750-B3A5-F6E9C74060F5}"); // Compile
                    if (IsFxCopInstalled(vsRoot)) defaults.Add(fxcopGuid);
                }
                else if (category == commonPropertyPages)
                {
                    defaults.Add("{1C25D270-6E41-4360-9221-1D22E4942FAD}"); // Application
                                                                            // The next entry is CVbApplicationWithMyPropPg but is auto translated from the Application guid above. 
                                                                            // We could use either one but the above one is most compatible at the moment.
                                                                            // defaults.Add("{8998E48E-B89A-4034-B66E-353D8C1FDC2E}"); 
                    defaults.Add("{43E38D2E-43B8-4204-8225-9357316137A4}"); // Services
                    defaults.Add("{4E43F4AB-9F03-4129-95BF-B8FF870AF6AB}"); // References
                    defaults.Add("{F8D6553F-F752-4DBF-ACB6-F291B744A792}"); // Signing
                    defaults.Add("{F24459FC-E883-4A8E-9DA2-AEF684F0E1F4}"); // 'my extensions' prop page ?? 

                    // not for type libraries -- but there is no way for us to do that... :-(
                    defaults.Add("{DF8F7042-0BB1-47D1-8E6D-DEB3D07698BD}"); // Security
                    defaults.Add("{CC4014F5-B18D-439C-9352-F99D984CCA85}"); // Publish
                }
            }
            // F# project
            else if (projectId == fsharpProject)
            {
                // Yahoo: F# is additive -- no need for default pages!
            }
            return defaults;
        }
        #endregion

        #region GetAssociatedProjects
        /// <summary>
        /// Return the projects associated with this property page
        /// </summary>
        /// <returns></returns>
        private List<string> GetAssociatedProjects()
        {
            List<string> associatedProjects = new List<string>();

            RegistryKey keyProjects = Common.GetLocalRegistryRoot(tool.VsRoot, "Projects");
            if (keyProjects != null)
            {
                // Go through the projects
                foreach (string project in keyProjects.GetSubKeyNames())
                {
                    RegistryKey projectKey = keyProjects.OpenSubKey(project);
                    if (projectKey != null)
                    {
                        // project can be specified by guid
                        if (projects.Contains(NormalizeProject(project)))
                        {
                            Common.Trace("Found associated project by guid: " + tool.DisplayName + ": " + project);
                            associatedProjects.Add(project);
                        }
                        // or by a language name
                        else if (projectKey.GetValue("DefaultProjectExtension") != null)
                        {
                            string languageName = projectKey.GetValue("Language(VsTemplate)") as string;
                            if (languageName != null && projects.Contains(languageName))
                            {
                                Common.Trace("Found associated project by name: " + tool.DisplayName + ": " + languageName + ": " + project);
                                associatedProjects.Add(project);
                            }
                        }
                        // debug messages
                        else if (Common.verbose)
                        {
                            Common.Trace(" Checked project: " + project);
                        }
                    }
                }
            }
            return associatedProjects;
        }
        #endregion

        #region Uninstall with VS: ie. unregister from the associated VS projects
        public void UnInstall()
        {
            try
            {
                // uninstall for VS projects
                foreach (string project in GetAssociatedProjects())
                {
                    VsUninstallPropertyPage(project);
                }

                // deregister for CodeTools
                RegistryKey installKey = tool.GetInstalledPropertyPagesKey(true);
                if (installKey != null)
                {
                    installKey.DeleteSubKeyTree(pageid.ToString("B"));
                }

                Common.Message("Uninstalled " + tool.DisplayName + " property pages");
            }
            catch (Exception exn)
            {
                // we don't want ever want to fail on uninstall
                Common.Trace("Uninstall of property pages for " + tool.DisplayName + " failed.\n" + exn.ToString());
            }
        }

        private void VsUninstallPropertyPage(string project)
        {
            RegistryKey projectKey = Common.GetLocalRegistryRoot(tool.VsRoot, "Projects\\" + project, true);
            if (projectKey != null)
            {
                RegistryKey propPagesKey = projectKey.OpenSubKey(category, true);
                if (propPagesKey != null)
                {
                    // just try to delete it
                    try
                    {
                        propPagesKey.DeleteSubKeyTree(pageid.ToString("B"));
                    }
                    catch { }

                    // remove entire node?
                    string[] subKeys = propPagesKey.GetSubKeyNames();
                    bool removeNode = (subKeys == null || subKeys.Length == 0);
                    if (!removeNode)
                    {
                        List<string> defaultPages = GetDefaultPropertyPages(tool.VsRoot, new Guid(project), category);
                        bool allDefault = true;
                        foreach (string subKey in subKeys)
                        {
                            if (!defaultPages.Contains(subKey))
                            {
                                allDefault = false;
                                break;
                            }
                        }
                        if (allDefault && subKeys.Length == defaultPages.Count)
                        {
                            removeNode = true;
                        }
                    }

                    if (removeNode)
                    {
                        // remove key completely
                        propPagesKey.Close();
                        projectKey.DeleteSubKeyTree(category);
                    }

                    // special logic for FSharp. For vs2010 we need to remove it from the user 10.0_Config too
                    if (tool.VsVersion >= 10.0 && new Guid(project) == fsharpProject)
                    {
                        RegistryKey fsharpPropPagesKey = Registry.CurrentUser.OpenSubKey(tool.VsRoot + "_Config\\Projects\\" + project + "\\" + category, true);
                        if (fsharpPropPagesKey != null)
                        {
                            fsharpPropPagesKey.DeleteSubKeyTree(pageid.ToString("B"));
                        }
                    }
                }
            }

            // Uninstall autoload for fxcop (or it sometimes crashes)
            RegistryKey autoload = Common.GetLocalRegistryRoot(tool.VsRoot, "AutoLoadPackages\\" + vsLoadOnSolution, true);
            if (autoload != null)
            {
                if (tool.VsVersion >= 11.0)
                {
                    if (autoload.GetValue(fxcopPackage11) != null) autoload.DeleteValue(fxcopPackage11);
                    // Fix registry for people that accidently got the old package here
                    if (autoload.GetValue(fxcopPackage) != null) autoload.DeleteValue(fxcopPackage);
                }
                else
                {
                    if (autoload.GetValue(fxcopPackage) != null) autoload.DeleteValue(fxcopPackage);
                }
            }

            Common.Trace("Uninstalled " + tool.DisplayName + " property page for " + project + " projects");
        }

        #endregion

        #region Install with VS: ie. register with associated VS projecgts
        public void Install()
        {
            // if (installState != InstallMode.Installed) return;

            Common.Trace("Install property pane for " + tool.DisplayName);

            // Install for VS
            foreach (string project in GetAssociatedProjects())
            {
                VsInstallPropertyPage(project);
            }

            // Register this property page as installed
            Common.Trace("Register installed property pane for " + tool.DisplayName);
            RegistryKey regPagesKey = tool.GetInstalledPropertyPagesKey(true);
            if (regPagesKey != null)
            {
                RegistryKey propPageKey = regPagesKey.CreateSubKey(pageid.ToString("B"));
                if (propPageKey != null)
                {
                    propPageKey.SetValue("clsid", clsid.ToString("B"));
                    propPageKey.SetValue("category", category);
                    RegistryKey projectsKey = propPageKey.CreateSubKey("Projects");
                    if (projectsKey != null)
                    {
                        foreach (string project in projects)
                        {
                            projectsKey.SetValue(project, "", RegistryValueKind.String);
                        }
                    }
                }
            }

            Common.Message("Installed " + tool.DisplayName + " property pages");
        }

        private void VsInstallPropertyPage(string project)
        {
            RegistryKey propPagesKey = Common.GetLocalRegistryRoot(tool.VsRoot, "Projects\\" + project + "\\" + category, true);
            if (propPagesKey != null)
            {
                string[] subKeys = propPagesKey.GetSubKeyNames();
                if (subKeys == null || subKeys.Length == 0)
                {
                    // add default pages
                    List<string> defaults = GetDefaultPropertyPages(tool.VsRoot, new Guid(project), category);
                    foreach (string defaultPage in defaults)
                    {
                        Common.Trace("project " + project + ": install default page: " + defaultPage);

                        RegistryKey defaultKey = propPagesKey.CreateSubKey(defaultPage);
                        if (defaultPage == fxcopGuid && defaultKey != null)
                        {
                            // Force page order for fxcop
                            defaultKey.SetValue("PageOrder", 3, RegistryValueKind.DWord);

                            // Force autoload for fxcop (or it sometimes crashes)
                            RegistryKey autoload = Common.GetLocalRegistryRoot(tool.VsRoot, "AutoLoadPackages\\" + vsLoadOnSolution, true);
                            if (autoload != null)
                            {
                                if (tool.VsVersion >= 11.0)
                                {
                                    autoload.SetValue(fxcopPackage11, 0, RegistryValueKind.DWord);
                                    // Fix registry for people that accidently got the old package here
                                    if (autoload.GetValue(fxcopPackage) != null)
                                    {
                                        autoload.DeleteValue(fxcopPackage);
                                    }
                                }
                                else
                                {
                                    autoload.SetValue(fxcopPackage, 0, RegistryValueKind.DWord);
                                }
                            }
                        }
                    }
                }

                RegistryKey propPageKey = propPagesKey.CreateSubKey(pageid.ToString("B"));
                if (propPageKey != null)
                {
                    propPageKey.SetValue("", tool.DisplayName + " Property Page");
                }

                // special logic for FSharp. Somehow for vs2010 we need to put it in the user 10.0_Config too
                if (tool.VsVersion >= 10.0 && new Guid(project) == fsharpProject)
                {
                    Registry.CurrentUser.CreateSubKey(tool.VsRoot + "_Config\\Projects\\" + project + "\\" + category + "\\" + pageid.ToString("B"));
                }


                Common.Trace("Installed " + tool.DisplayName + " property page for " + project + " projects");

                propPageKey.Close();
                propPagesKey.Close();
            }
        }

        #endregion

        #region Static: read property page info from the registry
        private static string NormalizeProject(string name)
        {
            string norm = (name == null ? "" : name.Trim());

            if (!String.IsNullOrEmpty(norm) && norm[0] == '{')
            {
                try
                {
                    norm = new Guid(norm).ToString("B");  // normalize guid's
                }
                catch { }
            }
            // Common.Trace("Normalized project name " + name + " to " + norm);
            return norm;
        }

        public static PropertyPageInfo PropertyPageInfoFromRegistry(CodeToolInfo tool, RegistryKey propPagesKey, string propPage)
        {
            Guid propPageId = new Guid(propPage);
            if (propPageId != Guid.Empty)
            {
                RegistryKey propPageKey = propPagesKey.OpenSubKey(propPage, false);
                if (propPageKey != null)
                {
                    string clsid = propPageKey.GetValue("clsid") as String;
                    if (clsid != null)
                    {
                        Guid propPaneClsid = new Guid(clsid);
                        if (propPaneClsid != Guid.Empty)
                        {
                            Common.Trace("Property pane found: " + propPagesKey + "\\" + clsid);

                            string category = propPageKey.GetValue("Category") as String;

                            RegistryKey projectsKey = propPageKey.OpenSubKey("Projects", false);
                            List<string> projects = new List<string>();
                            if (projectsKey != null)
                            {
                                foreach (string key in projectsKey.GetValueNames())
                                {
                                    projects.Add(NormalizeProject(key));
                                }
                            }
                            return new PropertyPageInfo(tool, propPaneClsid, propPageId, category, projects);
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}