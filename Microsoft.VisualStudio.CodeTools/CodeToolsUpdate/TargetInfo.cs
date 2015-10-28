// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Text;
using System.IO;

namespace CodeToolsUpdate
{
    internal class TargetInfo
    {
        #region Fields
        private readonly CodeToolInfo tool;
        private readonly string targetName;  // name of this target; used to create unique filenames and identifiers
        private readonly string fileName;       // targets file
        private readonly string kind;               // ImportBefore | ImportAfter
        private readonly string condition;   // possible import condition
        private readonly string versions;     // just for these (semi colon seperated) msbuild versions: empty is for all
        private readonly Dictionary<string, string> properties;  // msbuild properties

        // magic constants
        private const string importAfter = "ImportAfter";
        private const string importBefore = "ImportBefore";

        // derived
        private readonly string prefixComment;
        private readonly string postfixComment;
        private readonly string projectTag = "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">";
        private readonly string projectEndTag = "</Project>";

        #endregion

        #region Constructor and equality
        private TargetInfo(CodeToolInfo tool, string targetName, string fileName, string kind, string _versions, string condition, Dictionary<string, string> properties)
        {
            this.targetName = targetName;
            this.tool = tool;
            this.fileName = fileName;
            this.kind = kind;
            versions = _versions;
            this.condition = condition;
            this.properties = properties;

            // sanitize
            if (String.Compare(kind, importAfter, true) == 0) this.kind = importAfter;
            if (String.Compare(kind, importBefore, true) == 0) this.kind = importBefore;
            if (versions == null)
                versions = "";
            else
                versions = versions.Trim();

            // derived
            prefixComment = "  <!-- Begin CodeTools: " + tool.ToolName + ": " + targetName + " -->";
            postfixComment = "  <!-- End CodeTools: " + tool.ToolName + ": " + targetName + " -->";
        }

        public override bool Equals(object obj)
        {
            TargetInfo t = obj as TargetInfo;
            if (t == null) return false;

            if (tool.ToolName != t.tool.ToolName) return false;
            if (tool.VsRoot != t.tool.VsRoot) return false;

            if (targetName != t.targetName) return false;
            if (fileName != t.fileName) return false;
            if (kind != t.kind) return false;
            if (condition != t.condition) return false;
            if (versions != t.versions) return false;
            if (properties.Count != t.properties.Count) return false;
            foreach (string prop in properties.Keys)
            {
                if (!t.properties.ContainsKey(prop)) return false;
                if (properties[prop] != t.properties[prop]) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            // just do a simple hash
            return (tool.ToolName.GetHashCode() * targetName.GetHashCode() * fileName.GetHashCode());
        }
        #endregion

        #region Create from registry

        public static TargetInfo ReadFromRegistry(CodeToolInfo tool, RegistryKey targetKey, string targetName)
        {
            string fileName = null;
            string kind = importAfter;
            string condition = "";
            string versions = "";

            Dictionary<string, string> properties = new Dictionary<string, string>();
            foreach (string name in targetKey.GetValueNames())
            {
                if (String.Compare(name, "TargetsFile", true) == 0)
                {
                    fileName = targetKey.GetValue(name) as string;
                }
                else if (String.Compare(name, "TargetsKind", true) == 0)
                {
                    kind = targetKey.GetValue(name) as string;
                }
                else if (String.Compare(name, "TargetsCondition", true) == 0)
                {
                    condition = targetKey.GetValue(name) as string;
                }
                else if (String.Compare(name, "MSBuildVersions", true) == 0 || String.Compare(name, "MSBuildVersion", true) == 0)
                {
                    versions = targetKey.GetValue(name) as string;
                }
                else if (name != null && name.Length > 0)
                {
                    string val = targetKey.GetValue(name) as string;
                    properties.Add(name, val);
                }
            }

            if (fileName == null) return null;

            return new TargetInfo(tool, targetName, fileName, kind, versions, condition, properties);
        }
        #endregion

        #region Install and UnInstall
        private /* ISet */ List<string> GetAssociatedMSBuildVersions()
        {
            List<string> buildVersions = new List<string>();
            if (versions == null || versions.Length == 0)
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                using (RegistryKey msbuild = baseKey.OpenSubKey(@"Software\Microsoft\MSBuild"))
                {
                    if (msbuild != null)
                    {
                        foreach (string ver in msbuild.GetSubKeyNames())
                        {
                            if (ver != null && ver.Length > 0 && Char.IsDigit(ver[0]))
                            {
                                buildVersions.Add(ver);
                            }
                        }
                    }
                }
            }
            else
            {
                // add the specified versions
                foreach (string version in versions.Split(';'))
                {
                    buildVersions.Add(version);
                }
            }
            return buildVersions;
        }

        public void Install()
        {
            List<string> buildVersions = GetAssociatedMSBuildVersions();

            // Create msbuild targets
            foreach (string ver in buildVersions)
            {
                InstallVersion(ver);
            }

            // register as installed
            RegistryKey installedKey = tool.GetInstalledTargetsKey(true);
            if (installedKey != null)
            {
                RegistryKey targetKey = installedKey.CreateSubKey(targetName);
                if (targetKey != null)
                {
                    targetKey.SetValue("TargetsFile", fileName);
                    targetKey.SetValue("TargetsKind", kind);
                    if (condition != null && condition.Length > 0) targetKey.SetValue("TargetsCondition", condition);
                    if (versions != null && versions.Length > 0) targetKey.SetValue("MSBuildVersions", versions);
                    foreach (string key in properties.Keys)
                    {
                        targetKey.SetValue(key, properties[key]);
                    }
                }
            }


            Common.Message("Installed " + tool.DisplayName + " MSBuild targets");
        }

        public void UnInstall()
        {
            try
            {
                List<string> buildVersions = GetAssociatedMSBuildVersions();

                // remove build targets
                foreach (string ver in buildVersions)
                {
                    UnInstallVersion(ver);
                }

                // register as uninstalled
                RegistryKey installedKey = tool.GetInstalledTargetsKey(true);
                if (installedKey != null)
                {
                    installedKey.DeleteSubKeyTree(targetName);
                }

                Common.Message("Uninstalled " + tool.DisplayName + " MSBuild targets");
            }
            catch (Exception exn)
            {
                Common.ErrorMessage("Failed to uninstall " + tool.DisplayName + " MSBuild targets:\n" + exn.ToString());
            }
        }
        #endregion

        #region Install for a specific msbuild version
        private string TargetsFile(string msbuildVersion)
        {
            string msbuildRoot = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\MSBuild\\";

            if (CompareVersion(msbuildVersion, "4.0") < 0)
            {
                if (kind == importAfter)
                {
                    return msbuildRoot + "v" + msbuildVersion + "\\Custom.After.Microsoft.Common.targets";
                }
                else if (kind == importBefore)
                {
                    return msbuildRoot + "v" + msbuildVersion + "\\Custom.Before.Microsoft.Common.targets";
                }
                else
                {
                    //not good :-(
                    return msbuildRoot + "v" + msbuildVersion + "\\Custom." + kind + ".Microsoft.Common.targets";
                }
            }
            else
            {
                return msbuildRoot + msbuildVersion + "\\Microsoft.Common.Targets\\" + kind + "\\" + tool.ToolName + targetName + ".targets";
            }
        }

        private void InstallVersion(string msbuildVersion)
        {
            // Build an import block
            StringBuilder content = new StringBuilder();
            content.AppendLine(prefixComment);
            if (properties.Count > 0)
            {
                content.AppendLine("  <PropertyGroup>");
                foreach (string key in properties.Keys)
                {
                    content.AppendFormat("    <{0} Condition=\"'$({0})'==''\">{1}</{0}>", key, properties[key]);
                    content.AppendLine("");
                }
                content.AppendLine("  </PropertyGroup>");
            }
            if (condition != null && condition.Length > 0)
            {
                content.AppendFormat("  <Import Condition=\"{1}\" Project=\"{0}\" />\n", fileName, condition);
                content.AppendLine("");
            }
            else
            {
                content.AppendFormat("  <Import Project=\"{0}\" />", fileName);
                content.AppendLine("");
            }
            content.AppendLine(postfixComment);

            string targetsFile = TargetsFile(msbuildVersion);
            bool createFresh = true;

            // legacy versions: inject into one common file				
            if (CompareVersion(msbuildVersion, "4.0") < 0)
            {
                createFresh = !File.Exists(targetsFile);
            }

            if (createFresh)
            {
                EnsureDir(targetsFile);
                using (StreamWriter f = File.CreateText(targetsFile))
                {
                    f.WriteLine(projectTag);
                    f.Write(content.ToString());
                    f.WriteLine(projectEndTag);
                    f.Close();
                }
            }
            else
            {
                // inject into a common file. Only happens for v3.5 and lower
                string fcontent = File.ReadAllText(targetsFile);
                string fproject;
                string fafterproject;
                string fprefix;
                string fpostfix;

                if (SplitAfter(projectTag, fcontent, out fproject, out fafterproject))
                {
                    if (SplitBetween(prefixComment, postfixComment, fcontent, out fprefix, out fpostfix))
                    {
                        // found previous version of us: overwrite that part
                        fcontent = fprefix + content.ToString() + fpostfix;
                    }
                    else
                    {
                        // no previous version of us: inject fresh
                        fcontent = fproject + "\r\n" + content.ToString() + fafterproject;
                    }
                    File.WriteAllText(targetsFile, fcontent);
                }
                else
                {
                    // this is not a project file -- give up
                }
            }
        }
        #endregion

        #region UnInstall for a specific msbuild version
        private void UnInstallVersion(string msbuildVersion)
        {
            string targetsFile = TargetsFile(msbuildVersion);
            if (File.Exists(targetsFile))
            {
                if (CompareVersion(msbuildVersion, "4.0") < 0)
                {
                    // legacy, un-inject from the common file
                    string fcontent = File.ReadAllText(targetsFile);
                    string fprefix;
                    string fpostfix;

                    if (SplitBetween(prefixComment, postfixComment, fcontent, out fprefix, out fpostfix))
                    {
                        // found our part, extract it
                        fcontent = fprefix + fpostfix;
                        File.WriteAllText(targetsFile, fcontent);
                    }
                }
                else
                {
                    // msbuild v4.0 and higher: remove our file
                    File.Delete(targetsFile);
                }
            }
        }
        #endregion

        #region String helpers
        private static int CompareVersion(string version1, string version2)
        {
            // strip off the 'v'
            string v1 = (version1 != null && version1.Length > 0 && (version1[0] == 'v' || version1[0] == 'V')) ? version1.Substring(1) : version1;
            string v2 = (version2 != null && version2.Length > 0 && (version2[0] == 'v' || version2[0] == 'V')) ? version2.Substring(1) : version2;
            if (v1 == v2) return 0;
            try
            {
                var vn1 = float.Parse(v1);
                var vn2 = float.Parse(v2);
                return (vn1 - vn2 < 0) ? -1 : 1;
            }
            catch
            {
                return -1;
            }
        }

        private void EnsureDir(string path)
        {
            List<string> directories = new List<string>();
            {
                string dir = Path.GetDirectoryName(path);
                int i;
                while ((i = dir.LastIndexOf(Path.DirectorySeparatorChar)) >= 0)
                {
                    directories.Add(dir);
                    dir = dir.Substring(0, i);
                }
            }

            directories.Reverse();
            foreach (string dir in directories)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
        }

        private bool MatchAt(string pattern, string s, int i)
        {
            if (pattern == null) return true;
            if (s == null) return false;

            for (int j = 0; j < pattern.Length; j++)
            {
                if (pattern[j] != s[i + j]) return false;
            }
            return true;
        }

        private bool SplitOn(bool after, string pattern, string s, out string prefix, out string postfix)
        {
            if (pattern == null)
            {
                prefix = "";
                postfix = s;
                return true;
            }

            prefix = s;
            postfix = null;
            if (s == null) return false;

            for (int i = 0; i < s.Length; i++)
            {
                if (MatchAt(pattern, s, i))
                {
                    if (after) i += pattern.Length;
                    prefix = s.Substring(0, i);
                    postfix = s.Substring(i);
                    return true;
                }
            }
            return false;
        }

        private bool SplitAfter(string pattern, string s, out string prefix, out string postfix)
        {
            return SplitOn(true, pattern, s, out prefix, out postfix);
        }

        private bool SplitBefore(string pattern, string s, out string prefix, out string postfix)
        {
            return SplitOn(false, pattern, s, out prefix, out postfix);
        }


        private bool SplitBetween(string patternStart, string patternEnd, string s, out string prefix, out string postfix)
        {
            string afterstart;
            string fragment;

            if (SplitBefore(patternStart, s, out prefix, out afterstart))
            {
                if (SplitAfter(patternEnd, afterstart, out fragment, out postfix))
                {
                    return true;
                }
            }

            prefix = s;
            postfix = "";
            return false;
        }
        #endregion
    }
}