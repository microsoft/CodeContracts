// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
    internal class Program
    {
        private static int Main(string[] args)
        {
            string formatString = "Assembly: {0}, Version: {1}, Types: {2}";
            string assemblyFile = "";

            if (args == null || args.Length < 1 || args.Length > 2)
            {
                Console.WriteLine("AssemblyInfo: error: Expecting a format string and assembly file as arguments: " + args.Length);
                foreach (string arg in args)
                {
                    Console.WriteLine(arg);
                }
                return 1;
            }
            else if (args.Length == 1)
            {
                assemblyFile = args[0];
            }
            else if (args.Length == 2)
            {
                if (args[0] != null && args[0].Length > 0)
                {
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
