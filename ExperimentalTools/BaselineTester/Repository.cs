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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Baseline
{

    [ContractClass(typeof(RepositoryContract))]
    abstract class Repository
    {
        public Repository() { }

        #region exceptions
        public class ExeInvokeException : Exception
        {
            public ExeInvokeException()
            {
            }
            public ExeInvokeException(string message)
                : base(message)
            {
            }
            public ExeInvokeException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class ExeNotInstalledException : Exception
        {
            public ExeNotInstalledException()
            {
            }

            public ExeNotInstalledException(string message)
                : base(message)
            {
            }

            public ExeNotInstalledException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        #endregion exceptions

        #region contracts
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.WorkingDirectory != null);
            Contract.Invariant(this.Url != null);
            Contract.Invariant(this.Name != null);
        }

        [ContractClassFor(typeof(Repository))]
        abstract class RepositoryContract : Repository
        {

            public override IEnumerable<string> GetVersionNumbers()
            {
                Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
                return default(IEnumerable<string>);
            }

            public override string Checkout(string version)
            {
                Contract.Requires(version != null);
                Contract.Ensures(Contract.Result<string>() == null || CheckedOut(version));
                return default(string);
            }

            public override bool Build(string version)
            {
                Contract.Requires(version != null);
                Contract.Ensures(Contract.Result<bool>() == false || Built(version));
                return default(bool);
            }

            public override bool CheckedOut(string version)
            {
                Contract.Requires(version != null);
                return default(bool);
            }


            public override bool Built(string version)
            {
                Contract.Requires(version != null);
                return default(bool);
            }

        }
        #endregion

        #region members, constants, constructor(s)
        public readonly string WorkingDirectory;
        public readonly string Url;
        public readonly string Name;

        protected const int shortTimeout = 10000; // 10s
        protected const int mediumTimeout = 120000; // 2min
        protected const int longTimeout = 36000000; // 10h

        public Repository(string workingDirectory, string url, string name)
        {
            Contract.Requires(workingDirectory != null);
            //Initialize();
            this.WorkingDirectory = Path.Combine(workingDirectory, name);
            this.Url = url;
            this.Name = name;
        }
        #endregion

        #region abstract methods
        /// <summary>
        /// Return enumerable containing all version numbers (descending)
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<string> GetVersionNumbers();

        /// <summary>
        /// Check out single version of the repository
        /// </summary>
        /// <param name="versionNum"></param>
        /// <returns></returns>
        public abstract string Checkout(string version);

        /// <summary>
        /// Build single version of the repository
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public abstract bool Build(string version);

        /// <summary>
        /// Is version already built?
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [Pure]
        public abstract bool Built(string version);

        /// <summary>
        /// Is version already checked out?
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [Pure]
        public abstract bool CheckedOut(string version);

        #endregion abstract methods

        #region implemented methods

        #region initialization

        //static private string ExeFileName;

        abstract protected string ExeName { get; }

        virtual protected string FallbackLocateExe()
        {
            return null;
        }

        public string MakeRepoPath(string versionNum)
        {
            Contract.Requires(versionNum != null);
            Contract.Ensures(Contract.Result<string>() != null);
            return Path.Combine(this.WorkingDirectory, this.Name + "-" + versionNum);
        }

        private string LocateExe()
        {
            var envPath = Environment.GetEnvironmentVariable("PATH");
            if (envPath == null)
                return null;

            foreach (var path in envPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var fileName = Path.Combine(path, this.ExeName);
                if (File.Exists(fileName))
                    return fileName;
            }
            return this.FallbackLocateExe();
        }

        static private bool initialized = false;

        /*
        public void Initialize()
        {
            if (initialized)
                return;

            ExeFileName = this.LocateExe();
            if (String.IsNullOrWhiteSpace(ExeFileName))
            {
                throw new ExeNotInstalledException();
            }

            initialized = true;
        }
         */
        #endregion

        /// <summary>
        /// Check out and build all versions of this repository
        /// </summary>
        public void CheckoutAndRebuildAll()
        {
            CheckoutAllImpl(true, true);
        }

        /// <summary>
        /// Check out all versions of this repository and build versions that aren't already built
        /// </summary>
        public void CheckoutAndBuildAll()
        {
            CheckoutAllImpl(true);
        }

        /// <summary>
        /// Check out all versions of this repository; don't build
        /// </summary>
        public void CheckoutAll()
        {
            CheckoutAllImpl();
        }

        // check out and optionally build all versions of repo
        private void CheckoutAllImpl(bool build = false, bool rebuild = false)
        {
            foreach (string version in GetVersionNumbers())
            {
                string repoPath = Checkout(version); // get repo
                if (repoPath == null) continue; // checkout failed
                if (build)
                {
                    if (rebuild || !Built(version))
                    {
                        Build(version);
                    }
                    else
                    {
                        Console.WriteLine(version + " already built");
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if incovation succeeded
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <returns></returns>
        protected bool InvokeSourceManager(string arguments, string workingDir, int timeoutMilliseconds = 0)
        {/*
            var info = new ProcessStartInfo(arguments);
            info.FileName = ExeName;
            info.WorkingDirectory = this.WorkingDirectory;
            var process = Process.Start(info);
            Console.WriteLine("waiting for process...");
            //process.WaitForInputIdle();
            process.WaitForExit();
            while (process.Responding) {
                
            }

            return true;
            */
            Contract.Requires(!String.IsNullOrWhiteSpace(arguments));
            var psi = new ProcessStartInfo
            {
                Arguments = arguments,
                //CreateNoWindow = true,
                //ErrorDialog = false,
                FileName = ExeName,
                //RedirectStandardError = true,
                //RedirectStandardInput = true,
                //RedirectStandardOutput = true,
                //UseShellExecute = false,
                //WindowStyle = ProcessWindowStyle.Hidden,
                //WorkingDirectory = this.WorkingDirectory
                WorkingDirectory = workingDir
            };

            try
            {
                var process = new Process();
                process.StartInfo = psi;
                process.Start();
                Console.WriteLine("waiting for process...");
                process.WaitForExit();
                Console.WriteLine("done waiting.");
                //return true;
                return process.ExitCode == 0;
            }
            catch (Exception e)
            {
                throw new ExeInvokeException("Arguments = \"" + arguments + "\". Refer to inner exception for details.", e);
            }
            
        }

        protected string InvokeSourceManagerForOutput(string arguments, int timeoutMilliseconds = 0)
        {
            Contract.Requires(!String.IsNullOrWhiteSpace(arguments));
            Contract.Ensures(Contract.Result<String>() != null);
            var psi = new ProcessStartInfo
            {
                Arguments = arguments,
                CreateNoWindow = true,
                ErrorDialog = false,
                FileName = ExeName,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                //WorkingDirectory = this.WorkingDirectory
                WorkingDirectory = Environment.CurrentDirectory
            };

            try
            {
                var process = new Process();
                process.StartInfo = psi;
                process.Start();
                return process.StandardOutput.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new ExeInvokeException("Arguments = \"" + arguments + "\". Refer to inner exception for details.", e);
            }
        }

        #endregion implemented methods
    }


    

}


