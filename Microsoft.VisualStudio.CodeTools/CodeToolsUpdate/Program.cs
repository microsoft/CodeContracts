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
using System.IO;
using System.Reflection;

namespace CodeToolsUpdate
{
	class Program
	{
		static int Main(string[] args)
		{
			try {
				Common.Message("CodeToolsUpdate Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString());

				UpdateMode updateMode = UpdateMode.Normal;
				List<string> toolNames = new List<string>();
				bool listMode = false;

				if (args != null) {
					foreach (string arg in args) {
						if (String.Compare(arg, "/install", true) == 0) {
							updateMode = UpdateMode.ForceInstall;
						}
						else if (String.Compare(arg, "/uninstall", true) == 0) {
							updateMode = UpdateMode.ForceUnInstall;
						}
						else if (String.Compare(arg, "/update", true) == 0) {
							updateMode = UpdateMode.Normal;
						}
						else if (String.Compare(arg, "/list", true) == 0) {
							listMode = true;
						}
            else if (String.Compare(arg, "/verbose", true) == 0) {
              Common.verbose = true;
            }
            else if (String.Compare(arg, "/help", true) == 0 || arg == "/?" || arg == "/h") {
							Common.Message("Codetools update tool, Copyright 2010 Daan Leijen");
							Common.Message("Usage  : CodeToolsUpdate [option] [toolName1] ... [toolNameN]");
							Common.Message("Options: /update | /install | /uninstall | /list | /help | /verbose");
							Common.Message("No arguments at all means update all tools as necessary");
							Common.Message("");
							return 0;
						}
						else {
							// assume a toolName
							toolNames.Add(arg);
						}
					}
				}

				string vs = @"Software\Microsoft\VisualStudio";
				RegistryKey vsKey = Registry.LocalMachine.OpenSubKey(vs);
				if (vsKey != null) {
					foreach (string version in vsKey.GetSubKeyNames()) {
						string vsRoot = vs + "\\" + version;
						if (Common.GetLocalRegistryRoot(vsRoot, "CodeTools") != null) {
							if (listMode) {
								Listing(toolNames, vsRoot);
							}
							else {
								Update(updateMode, toolNames, vsRoot);
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException) {
				Common.ErrorMessage("This program needs to be run in administrator mode");
			}
			catch (Exception exn) {
				Common.ErrorMessage(exn.ToString());
			}
			Common.Message("Done");
			return 0; // we don't want to fail?
		}

		static void Listing(List<string> toolNames, string vsRoot)
		{
			Common.Message("Listing for visual studio " + Path.GetFileName(vsRoot));

			IList<CodeToolInfo> tools = CodeToolInfo.ReadAllFromRegistry(UpdateMode.Normal, vsRoot);
			foreach (CodeToolInfo tool in tools) {
				if (toolNames.Count == 0 || toolNames.Contains(tool.ToolName)) {
					tool.Show(); 
				}
			}
		}

		static void Update(UpdateMode updateMode, List<string> toolNames, string vsRoot)
		{
			Common.Message("Update for visual studio " + Path.GetFileName(vsRoot));

			IList<CodeToolInfo> tools = CodeToolInfo.ReadAllFromRegistry(updateMode, vsRoot);
			foreach (CodeToolInfo tool in tools) {
				if (toolNames.Count == 0 || toolNames.Contains(tool.ToolName)) {
					tool.UnInstall(); // only runs if necessary
				}
			}
			foreach (CodeToolInfo tool in tools) {
				if (toolNames.Count == 0 || toolNames.Contains(tool.ToolName)) {
					tool.Install(); // only runs if necessary
				}
			}
		}
	}
}
