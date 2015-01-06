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
using System.Xml.Linq;

namespace ClousotTests
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
      this.currentInstance++;
      instance = this.currentInstance;
    }

    private int Instance { get { return this.currentInstance; } }

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
      failure.Add(new XAttribute("Index", this.currentInstance));
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
                    where (string)failure.Attribute("Index") == this.currentInstance.ToString()
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