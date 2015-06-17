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
using System.IO;

namespace Baseline
{
    class Program
    {

        public static void RunAll()
        {
            // working directory
            const string dir = "Versions";
            // experimental subjects
            var benchmarks = new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("elysium", "https://elysium.svn.codeplex.com/svn"),  
                                                                  new KeyValuePair<string,string>("autodiff", "https://autodiff.svn.codeplex.com/svn"),
                                                                  new KeyValuePair<string,string>("mishrareader", "https://mishrareader.svn.codeplex.com/svn"),
                                                                  new KeyValuePair<string,string>("imageresizer", "https://imageresizer.svn.codeplex.com/svn"),
                                                                  new KeyValuePair<string,string>("virtualrouter", "https://virtualrouter.svn.codeplex.com/svn"),
                                                                  new KeyValuePair<string,string>("wbfsmanager", "https://wbfsmanager.svn.codeplex.com/svn") };
            // run experiments
            foreach (var pair in benchmarks)
            {
                var repo = new SVNRepository(dir, pair.Value, pair.Key);
                repo.CheckoutAll();
                var baseliner = new Baseliner(repo);
                try
                {
                    baseliner.BaselineWithRebuild("typeBased");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception encountered: " + e);
                }

                try
                {
                    baseliner.BaselineWithRebuild("ilBased");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception encountered: " + e);
                }
            }
        }

        static void Main(string[] args)
        {
     
            if (args.Count() != 4) 
            {
                Console.WriteLine("Usage: baseline <repoDir> <repoUrl> <baseLineStrategy> <git || svn>");
                Environment.Exit(1);
            }
                
            var dir = Directory.GetCurrentDirectory();
            var path = Path.Combine(Path.GetFullPath(dir), dir);
            Console.WriteLine(path);
            Repository repo = null;
            if (args[3] == "svn") {
              repo = new SVNRepository(path, args[1], args[0]);

            }
            else if (args[3] == "git")
            {
              repo = new GitRepository(path, args[1], args[0]);
            }
            else
            {
              Console.WriteLine("Unrecognized version control " + args[3]);
              System.Environment.Exit(1);
            }
            repo.CheckoutAll();
            var baseliner = new Baseliner(repo);
            baseliner.BaselineWithRebuild(args[2]);
        }
    }
}
