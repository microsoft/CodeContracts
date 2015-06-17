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

// Copyright (c) 2010 Microsoft Research
//
// A small sample program that will be run after each C# or VB build.
// The program takes two arguments: a format string and an assembly file.
// and displays some simple information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AssemblyInfo
{
	class Program
	{
		static int Main(string[] args)
		{
			string formatString = "Assembly: {0}, Version: {1}, Types: {2}";
			string assemblyFile = "";

			if (args == null || args.Length < 1 || args.Length > 2) {
				Console.WriteLine( "AssemblyInfo: error: Expecting a format string and assembly file as arguments: " + args.Length);
				foreach (string arg in args) {
					Console.WriteLine(arg);
				}
				return 1;
			}
			else if (args.Length == 1) {
				assemblyFile = args[0];
			}
			else if (args.Length == 2) {
				if (args[0] != null && args[0].Length > 0) {
					formatString = args[0];
				}
				assemblyFile = args[1];
			}

			Assembly assembly = Assembly.LoadFile(assemblyFile);
			Console.Write("AssemblyInfo: ");
			Console.WriteLine(formatString, assembly.GetName().Name, assembly.GetName().Version.ToString(), assembly.GetTypes().Count());
			return 0;
		}
	}
}
