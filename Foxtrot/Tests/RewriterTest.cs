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
using System.Diagnostics.Contracts;

namespace Tests {
  public class DisableAssertUI {
    static DisableAssertUI() {
      for (int i = 0; i < System.Diagnostics.Debug.Listeners.Count; i++) {
        System.Diagnostics.DefaultTraceListener dtl = System.Diagnostics.Debug.Listeners[i] as System.Diagnostics.DefaultTraceListener;
        if (dtl != null) dtl.AssertUiEnabled = false;
      }
    }
  }
  [TestClass]
  public class RewriterTest : DisableAssertUI{
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize (TestContext context) {
    }

    [ClassCleanup]
    public static void ClassCleanup () {
    }

    [TestInitialize]
    public void TestInitialize () {
    }

    [TestCleanup]
    public void TestCleanup () {
    }
    #endregion Test Management

    #region Tests
    /// <summary>
    /// Runs PEVerify on the rewritten Tests.dll.
    /// </summary>
    [TestMethod]
    public void PEVerifyRewrittenTest()
    {
      string codebase = typeof(RewriterTest).Assembly.CodeBase;
      codebase = codebase.Replace("#", "%23");
      Uri codebaseUri = new Uri(codebase);
      string path = "\"" + codebaseUri.LocalPath + "\"";
      object winsdkfolder = Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SDKs\Windows", "CurrentInstallFolder", null);
      string peVerifyPath;
      if (winsdkfolder == null) {
        peVerifyPath = Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Microsoft Visual Studio 8\Common7\IDE\PEVerify.exe");
      }
      else {
        peVerifyPath = (string)winsdkfolder + @"bin\peverify.exe";
      }
      string commandlineargs = "-unique " + path; // Use "-unique" to reduce the number of output lines
      ProcessStartInfo i = new ProcessStartInfo(peVerifyPath, commandlineargs);
      i.RedirectStandardOutput = true;
      i.UseShellExecute = false;
      using (Process p = Process.Start(i))
      {
        // A readtoend() call is suggested by a blog pointed to on the WaitForExit(miliscnds) page in MSDN.
        // Otherwise sometimes we may hit the time out. 
        string s1 = p.StandardOutput.ReadToEnd();
        bool b = p.WaitForExit(1000); 
        string s = p.StandardOutput.ReadToEnd();
        Assert.IsTrue(b, "PEVerify timed out.");
        Assert.AreEqual(0, p.ExitCode, "PEVerify returned an errorcode of {0}.{1}", p.ExitCode, s1 + s);
      }
    }

    [TestMethod]
    public void PEVerifyTests00()
    {
      PEVerify("Tests00.dll");
    }
    [TestMethod]
    public void PEVerifyTests01()
    {
      PEVerify("Tests01.dll");
    }
    [TestMethod]
    public void PEVerifyTests02()
    {
      PEVerify("Tests02.dll");
    }
    [TestMethod]
    public void PEVerifyTests03()
    {
      PEVerify("Tests03.dll");
    }
    [TestMethod]
    public void PEVerifyTests04()
    {
      PEVerify("Tests04.dll");
    }
    [TestMethod]
    public void PEVerifyTests10()
    {
      PEVerify("Tests10.dll");
    }
    [TestMethod]
    public void PEVerifyTests11()
    {
      PEVerify("Tests11.dll");
    }
    [TestMethod]
    public void PEVerifyTests12()
    {
      PEVerify("Tests12.dll");
    }
    [TestMethod]
    public void PEVerifyTests13()
    {
      PEVerify("Tests13.dll");
    }
    [TestMethod]
    public void PEVerifyTests14()
    {
      PEVerify("Tests14.dll");
    }
    [TestMethod]
    public void PEVerifyTests20()
    {
      PEVerify("Tests20.dll");
    }
    [TestMethod]
    public void PEVerifyTests21()
    {
      PEVerify("Tests21.dll");
    }
    [TestMethod]
    public void PEVerifyTests22()
    {
      PEVerify("Tests22.dll");
    }
    [TestMethod]
    public void PEVerifyTests23()
    {
      PEVerify("Tests23.dll");
    }
    [TestMethod]
    public void PEVerifyTests24()
    {
      PEVerify("Tests24.dll");
    }

    private static void PEVerify(string assemblyFile)
    {
      var path = @"..\..\..\Foxtrot\Tests\bin\Debug";
      var oldCWD = Environment.CurrentDirectory;
      try
      {
        Environment.CurrentDirectory = path;

        object winsdkfolder = Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Microsoft SDKs\Windows", "CurrentInstallFolder", null);
        string peVerifyPath;
        if (winsdkfolder == null)
        {
          peVerifyPath = Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Microsoft Visual Studio 8\Common7\IDE\PEVerify.exe");
        }
        else
        {
          peVerifyPath = (string)winsdkfolder + @"bin\peverify.exe";
        }
        ProcessStartInfo i = new ProcessStartInfo(peVerifyPath, "/unique " + assemblyFile);
        i.RedirectStandardOutput = true;
        i.UseShellExecute = false;
        i.CreateNoWindow = true;
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

    #endregion Tests

    #region Test code with disjunctions

    public class Disjunctions
    {
      public static void TestRequiresDisjunction1(string arg, int index)
      {
        Contract.Requires(arg == null || arg.Length > index || index < 0);
      }
      public static void TestRequiresDisjunction2(string arg, int index)
      {
        Contract.Requires<ArgumentException>(arg == null || arg.Length > index || index < 0);
      }
      public static void TestRequiresDisjunction3(string arg, int index)
      {
        if (arg == null || arg.Length > index || index < 0)
        {
          throw new ArgumentException();
        }
        Contract.EndContractBlock();
      }
      public static void TestEnsuresDisjunction1(string arg, int index)
      {
        Contract.Ensures(arg == null || arg.Length > index || index < 0);
      }
      public static void TestEnsuresDisjunction2(string arg, int index)
      {
        Contract.EnsuresOnThrow<ArgumentException>(arg == null || arg.Length > index || index < 0);
      }

      public string _arg;
      public int _index;

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(_arg == null || _arg.Length > _index || _index < 0);
      }

      public static void TestAssertDisjunction1(string arg, int index)
      {
        Contract.Assert(arg == null || arg.Length > index || index < 0);
      }

      public static void TestAssumeDisjunction1(string arg, int index)
      {
        Contract.Assume(arg == null || arg.Length > index || index < 0);
      }


    }
    #endregion
  }
}

namespace TypeParameterWritingTest
{
  public abstract class nHibernateSql2005Dal<TType1, TType2> : IDal<TType1, TType2>
  {
    TEntity IDal<TType1, TType2>.Load<TEntity, TKey>(TKey key)
    {
      return default(TEntity);
    }
    TEntity IDal<TType1, TType2>.Load<TEntity, TKey>(TEntity key)
    {
      return default(TEntity);
    }
    TType1 IDal<TType1, TType2>.Load<TEntity, TKey>(TType1 key)
    {
      return default(TType1);
    }
    TType1 IDal<TType1, TType2>.Load<TEntity, TKey>(TType2 key)
    {
      return default(TType1);
    }
    TType2 IDal<TType1, TType2>.Load<TEntity, TKey>(TKey key, bool b)
    {
      throw new NotImplementedException();
    }
    TType2 IDal<TType1, TType2>.Load<TEntity, TKey>(TEntity key, bool b)
    {
      throw new NotImplementedException();
    }
    TKey IDal<TType1, TType2>.Load<TEntity, TKey>(TType1 key, bool b)
    {
      throw new NotImplementedException();
    }
    TKey IDal<TType1, TType2>.Load<TEntity, TKey>(TType2 key, bool b)
    {
      throw new NotImplementedException();
    }
  }
}
