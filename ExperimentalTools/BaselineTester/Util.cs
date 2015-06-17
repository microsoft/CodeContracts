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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.IO;
using System.Diagnostics;

namespace Baseline
{
    class Util
    {

        // returns printed stderr from process (needed because Clousot output goes to stderr)
        public static string Invoke(string exe, string invokeDir, string arguments)
        {
            Contract.Requires(exe != null);
            Contract.Requires(invokeDir != null);
            Contract.Ensures(Contract.Result<string>() != null) ;
            var psi = new ProcessStartInfo
            {
                Arguments = arguments,
                CreateNoWindow = true,
                ErrorDialog = false,
                FileName = Path.Combine(Directory.GetCurrentDirectory(), exe),
                RedirectStandardError = true,
                //RedirectStandardInput = true,
                //RedirectStandardOutput = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), invokeDir)
            };
            Console.WriteLine("running from " + Path.Combine(Directory.GetCurrentDirectory(), invokeDir));
            try
            {
                var process = new Process();
                process.StartInfo = psi;
                Console.WriteLine("about to start process");
                process.Start();
                Console.WriteLine("started");
                string outStr = process.StandardError.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine(outStr);
                return outStr;
                //return process.StandardError.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("Invocation " + "exe " + arguments + " failed " + e);
            }
        }

        // find all files in baseDir and its subdirectories matching pattern
        public static List<string> GetFilesMatching(string baseDir, string pattern)
        {
            Contract.Requires(baseDir != null);
            Contract.Ensures(Contract.Result<List<string>>() != null);
            List<string> matches = new List<string>();
            RecFind(baseDir, matches, pattern);
            return matches;
        }

        // get all solution files in dir and its subdirectories
        public static List<string> GetSolutionFiles(string dir)
        {
            Contract.Requires(dir != null);
            Contract.Ensures(Contract.Result<List<string>>() != null);
            return GetFilesMatching(dir, "*.sln");
        }

        // get all repro.bat files in dir and its subdirectories
        public static List<string> GetRepros(string dir)
        {
            Contract.Requires(dir != null);
            Contract.Ensures(Contract.Result<List<string>>() != null);
            return GetFilesMatching(dir, "repro.bat");
        }

        // do a recursive search of dir and its subdirectories for matches of pattern. add matches to found
        private static void RecFind(string dir, List<string> found, string pattern)
        {
            Contract.Requires(dir != null);
            Contract.Requires(found != null);
            Contract.Requires(pattern != null);
            foreach (string f in Directory.GetFiles(dir, pattern))
            {
                found.Add(f);
            }
            foreach (string d in Directory.GetDirectories(dir))
            {
                RecFind(d, found, pattern);
            }
        }
    }
}
