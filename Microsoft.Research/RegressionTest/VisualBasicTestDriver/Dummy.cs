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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Win32;

namespace Tests
{
  public class DisableAssertUI
  {
    static DisableAssertUI()
    {
      for (int i = 0; i < System.Diagnostics.Debug.Listeners.Count; i++)
      {
        System.Diagnostics.DefaultTraceListener dtl = System.Diagnostics.Debug.Listeners[i] as System.Diagnostics.DefaultTraceListener;
        if (dtl != null) dtl.AssertUiEnabled = false;
      }

      System.Diagnostics.Contracts.Contract.ContractFailed += Contract_ContractFailed;
    }

    private static void Contract_ContractFailed(object sender, System.Diagnostics.Contracts.ContractFailedEventArgs e)
    {
      e.SetUnwind();
    }
  }
  [TestClass]
  public class RewrittenVB : DisableAssertUI
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management

    #region Tests
    /// <summary>
    /// Runs PEVerify on the rewritte VB assembly
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("VB"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PEVerifyVisualBasicRewrite()
    {
      var path = @"..\..\..\Microsoft.Research\RegressionTest\VisualBasicTests\bin\Debug";
      var oldCWD = Environment.CurrentDirectory;
      try
      {
        Environment.CurrentDirectory = path;

        string peVerifyPath = GetPeverifyPath();
        ProcessStartInfo i = new ProcessStartInfo(peVerifyPath, "/unique vbrw.dll");
        i.RedirectStandardOutput = true;
        i.UseShellExecute = false;
        using (Process p = Process.Start(i))
        {
          Assert.IsTrue(p.WaitForExit(10000), "PEVerify timed out.");
          Assert.AreEqual(0, p.ExitCode, "PEVerify returned an errorcode of {0}.{1}", p.ExitCode, p.StandardOutput.ReadToEnd());
        }
      }
      finally
      {
        Environment.CurrentDirectory = oldCWD;
      }
    }

    private static string GetPeverifyPath()
    {
      object winsdkfolder = Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SDKs\Windows\v8.0A", "InstallationFolder", null);
      if (winsdkfolder != null)
      {
        var path = (string)winsdkfolder + @"bin\NETFX 4.0 Tools\peverify.exe";
        if (System.IO.File.Exists(path)) return path;
      }
      winsdkfolder = Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SDKs\Windows\v7.0A", "InstallationFolder", null);
      if (winsdkfolder != null)
      {
        var path = (string)winsdkfolder + @"bin\peverify.exe";
        if (System.IO.File.Exists(path)) return path;
      }
      var peVerifyPath = Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Microsoft Visual Studio 8\Common7\IDE\PEVerify.exe");
      return peVerifyPath;
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("VB"), TestCategory("CoreTest"), TestCategory("Short")]
    public void RunVBTests()
    {
      var path = @"..\..\..\Microsoft.Research\RegressionTest\VisualBasicTests\bin\Debug";
      try
      {
        var testAssembly = System.Reflection.Assembly.LoadFrom(System.IO.Path.Combine(path, "vbrw.dll"));
        var testType = testAssembly.GetType("VisualBasicTests.RunTests");
        var testMethod = testType.GetMethod("Run");
        testMethod.Invoke(null, null);
      }
      catch (Exception e)
      {
        Assert.IsTrue(false, String.Format("VB test run failed with {0}", e.Message));
      }
    }
    #endregion Tests
  }
}
