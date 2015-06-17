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
using Microsoft.Win32;

namespace CodeToolsUpdate
{
	enum InstallMode
	{
		Install, 		// just installed: register with VS projects
		UnInstall, 	// just uninstalled: unregister with VS projects
		Active			// yeah, ready to run
	};

	enum UpdateMode
	{
		ForceInstall, ForceUnInstall, Normal
	}

	/// <summary>
	/// The metadata contained in a CodeTools entry
	/// </summary>
	internal class CodeToolInfo
	{
		#region Fields
		string vsRoot;					// the info for this particular VS version
		string toolName;				// unique tool identifier
		string displayName;
		int refreshDelay;
    double vsVersion;        // approximate vs version, like 11.0; use 0 for unparseable versions
    string vsVersionString;

		List<PropertyPageInfo> propertyPages;
		List<TargetInfo> targets;

		// We do not keep track of 'Tasks' as there is not update need for those
		// since they are directly tracked by the TaskManager itself
		// List<TaskInfo> tasks;

		InstallMode installMode;


		public string DisplayName { get { return displayName; } }
		public string ToolName { get { return toolName; } }
		public string VsRoot { get { return vsRoot; } }
    public double VsVersion { get { return vsVersion; } }

		const int defaultRefreshDelay = 1000;
		#endregion

		#region Constructor
		private CodeToolInfo(string _vsRoot, string _toolName, string _displayName, int _refreshDelay)
		{
			vsRoot = _vsRoot;
			toolName = _toolName;
			displayName = _displayName;
			refreshDelay = _refreshDelay;
			propertyPages = new List<PropertyPageInfo>();
			targets = new List<TargetInfo>();
			installMode = InstallMode.Install;

      // parse version parts of the visual studio root key
      int i = vsRoot.LastIndexOf('\\');
      if (i < 0)  vsVersionString = vsRoot;
             else vsVersionString = vsRoot.Substring(i + 1);
      
      try {
        double vsMajor = 0;
        double vsMinor = 0;
        i = 0;
        // skip whitespace
        while (i < vsVersionString.Length && (" \t".IndexOf(vsVersionString[i]) >= 0)) i++;
        // parse main part
        while (i < vsVersionString.Length && Char.IsDigit(vsVersionString[i])) {
          vsMajor = vsMajor * 10 + Char.GetNumericValue(vsVersionString, i);
          i++;
        }
        while (i < vsVersionString.Length && (" \t.,".IndexOf(vsVersionString[i]) >= 0)) i++;
        // parse minor part
        while (i < vsVersionString.Length && Char.IsDigit(vsVersionString[i])){
          vsMinor = vsMinor / 10 + Char.GetNumericValue(vsVersionString, i) / 10;
          i++;
        }
        vsVersion = vsMajor + vsMinor;
        if (vsVersion <= 0) vsVersion = 11.0;  // assume latest version?
        Common.Trace("Found version " + vsVersion.ToString() + " from: " + vsRoot);
      }
      catch {
        vsVersion = 0;
      }
		}

		private bool IsRelevant()
		{
			return (targets.Count > 0 || propertyPages.Count > 0);
		}

		public override bool Equals(object obj)
		{
			CodeToolInfo t = obj as CodeToolInfo;
			if (t == null) return false;
			if (vsRoot != t.vsRoot) return false;
			if (toolName != t.toolName) return false;
			if (displayName != t.displayName) return false;
			if (refreshDelay != t.refreshDelay) return false;
			if (propertyPages.Count != t.propertyPages.Count) return false;
			for (int i = 0; i < propertyPages.Count; i++) {
				if (!propertyPages[i].Equals(t.propertyPages[i])) return false;
			}
			if (targets.Count != t.targets.Count) return false;
			for (int i = 0; i < targets.Count; i++) {
				if (!targets[i].Equals(t.targets[i])) return false;
			}
			return true;
		}

		public override int GetHashCode()
		{
			// just a simple hash; as long as equals implies same hash
			return toolName.GetHashCode();
		}
		#endregion

		#region Standard Registry roots
		private static RegistryKey GetInstalledCodeToolsKey(string vsRoot, bool forWrite)
		{
			return Common.GetLocalRegistryRoot(vsRoot, "CodeTools\\$Installed", forWrite);
		}


		internal RegistryKey GetInstalledPropertyPagesKey(bool forWrite)
		{
			RegistryKey installedKey = GetInstalledCodeToolsKey(vsRoot, forWrite);
			if (installedKey != null) {
				string subKey = toolName + "\\PropertyPages";
				if (forWrite) {
					return installedKey.CreateSubKey(subKey);
				}
				else {
					return installedKey.OpenSubKey(subKey);
				}
			}
			return null;
		}

		internal RegistryKey GetInstalledTargetsKey(bool forWrite)
		{
			RegistryKey installedKey = GetInstalledCodeToolsKey(vsRoot, forWrite);
			if (installedKey != null) {
				string subKey = toolName + "\\CommonTargets";
				if (forWrite) {
					return installedKey.CreateSubKey(subKey);
				}
				else {
					return installedKey.OpenSubKey(subKey);
				}
			}
			return null;
		}

		#endregion

		#region Update
		public void Update()
		{
			switch (installMode) {
				case InstallMode.Active: return;
				case InstallMode.Install: Install(); break;
				case InstallMode.UnInstall: UnInstall(); break;
			}
		}

		public void Install()
		{
			if (installMode != InstallMode.Install) return;

			Common.Message("Installing " + displayName);

			// register under installed key
			RegistryKey installedKey = GetInstalledCodeToolsKey(vsRoot, true);
			if (installedKey != null) {
				RegistryKey toolKey = installedKey.CreateSubKey(toolName);
				toolKey.SetValue("DisplayName", displayName);
				if (refreshDelay != defaultRefreshDelay) {
					toolKey.SetValue("RefreshDelay", refreshDelay, RegistryValueKind.DWord);
				}
			}

			foreach (PropertyPageInfo page in propertyPages) {
				page.Install();
			}

			foreach (TargetInfo target in targets) {
				target.Install();
			}
		}

		public void UnInstall()
		{
			if (installMode != InstallMode.UnInstall) return;

			Common.Message("Uninstalling " + displayName);

			foreach (TargetInfo target in targets) {
				target.UnInstall();
			}

			foreach (PropertyPageInfo page in propertyPages) {
				page.UnInstall();
			}

			// unregister our entire tree
			RegistryKey installedKey = GetInstalledCodeToolsKey(vsRoot, true);
			if (installedKey != null) {
				installedKey.DeleteSubKeyTree(toolName);
			}
		}
		#endregion

		#region Static: read the codetools info from the registry

		private static CodeToolInfo CodeToolsInfoFromRegistry(string vsRoot, string toolName, RegistryKey toolKey)
		{
			int refreshDelay = defaultRefreshDelay;
			try { refreshDelay = (int)toolKey.GetValue("RefreshDelay", defaultRefreshDelay); }
			catch { }
			string displayName = toolKey.GetValue("DisplayName", toolName) as string;

			CodeToolInfo tool = new CodeToolInfo(vsRoot, toolName, displayName, refreshDelay);

			RegistryKey propPagesKey = toolKey.OpenSubKey("PropertyPages", false);
			if (propPagesKey != null) {
				foreach (string propPage in propPagesKey.GetSubKeyNames()) {
					PropertyPageInfo pageInfo = PropertyPageInfo.PropertyPageInfoFromRegistry(tool, propPagesKey, propPage);
					if (pageInfo != null) tool.propertyPages.Add(pageInfo);
				}
			}

			RegistryKey targetsKey = toolKey.OpenSubKey("CommonTargets", false);
			if (targetsKey != null) {
				foreach (string target in targetsKey.GetSubKeyNames()) {
					RegistryKey targetKey = targetsKey.OpenSubKey(target, false);
					if (targetKey != null) {
						TargetInfo targetInfo = TargetInfo.ReadFromRegistry(tool, targetKey, target);
						if (targetInfo != null) tool.targets.Add(targetInfo);
					}
				}
			}

			return tool;
		}

		public static IList<CodeToolInfo> ReadAllFromRegistry(UpdateMode mode, string vsRoot)
		{
			List<CodeToolInfo> infos = new List<CodeToolInfo>();

			if (mode != UpdateMode.ForceUnInstall) {
				RegistryKey root = Common.GetLocalRegistryRoot(vsRoot, "CodeTools");
				if (root != null) {
					String[] toolKeys = root.GetSubKeyNames();
					foreach (string toolName in toolKeys) {
						RegistryKey toolKey = root.OpenSubKey(toolName, false);
						if (toolKey != null) {
							CodeToolInfo tool = CodeToolsInfoFromRegistry(vsRoot, toolName, toolKey);
							if (tool.IsRelevant()) infos.Add(tool);
						}
					}
				}
			}

			if (mode != UpdateMode.ForceInstall) {
				RegistryKey installedKey = GetInstalledCodeToolsKey(vsRoot, false);
				if (installedKey != null) {
					foreach (string toolName in installedKey.GetSubKeyNames()) {
						RegistryKey toolKey = installedKey.OpenSubKey(toolName, false);
						if (toolKey != null) {
							CodeToolInfo itool = CodeToolsInfoFromRegistry(vsRoot, toolName, toolKey);
							CodeToolInfo tool = infos.Find(delegate(CodeToolInfo t) { return t.toolName == itool.toolName; });
							if (tool != null) {
								if (tool.Equals(itool)) {
									// installed tool == described tool
									tool.installMode = InstallMode.Active;
								}
								else {
									// described tool is updated: uninstall and reinstall
									itool.installMode = InstallMode.UnInstall;
									tool.installMode = InstallMode.Install;  // is actually the default
									infos.Add(itool);
								}
							}
							else if (tool == null) {
								// described tool is gone: uninstall
								itool.installMode = InstallMode.UnInstall;
								infos.Add(itool);
							}
						}
					}
				}
			}

			infos.Reverse(); // reverse the list so uninstalls happen before installs
			return infos;
		}
		#endregion

		#region Show
		internal void Show()
		{
			string installState = "<unknown>";
			switch (installMode) {
				case InstallMode.Active: installState = "active"; break;
				case InstallMode.Install: installState = "installed"; break;
				case InstallMode.UnInstall: installState = "uninstalled"; break;
			}
			Common.Message(ToolName + ": " + DisplayName + ": " + installState + " version" );
		}
		#endregion
	}
}