// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ClousotCacheTests
{
    public class GroupInfo
    {
        public readonly string TestGroupName;
        private int currentInstance;
        private readonly string rootDir;

        public GroupInfo(string testGroupName, string rootDir)
        {
            this.TestGroupName = testGroupName;
            this.rootDir = rootDir;
        }

        internal void Increment(out int instance)
        {
            currentInstance++;
            instance = currentInstance;
        }

        public void WriteFailure()
        {
            var failureFile = FailureFile();

            XElement failures;
            if (File.Exists(failureFile))
            {
                failures = XElement.Load(failureFile);
            }
            else
            {
                failures = new XElement(new XElement("Failures"));
            }
            var failure = new XElement("Failure");
            failure.Add(new XAttribute("Index", currentInstance));
            failures.Add(failure);
            failures.Save(failureFile);
        }

        private string FailureFile()
        {
            return Path.Combine(rootDir, TestGroupName + ".xml");
        }

        public bool Selected
        {
            get
            {
                // find if the current index is a previously failed one.
                var failureFile = FailureFile();
                if (!File.Exists(failureFile)) return true; // select all
                                                            //
                var failures = XElement.Load(failureFile);
                var found = from failure in failures.Descendants("Failure")
                            where (string)failure.Attribute("Index") == currentInstance.ToString()
                            select failure;
                // TODO: how do we release the file?
                return found.Count() != 0;
            }
        }

        internal void DeleteFailureFile()
        {
            var failureFile = FailureFile();
            if (File.Exists(failureFile))
            {
                try
                {
                    File.Delete(failureFile);
                }
                catch { }
            }
        }
    }
}