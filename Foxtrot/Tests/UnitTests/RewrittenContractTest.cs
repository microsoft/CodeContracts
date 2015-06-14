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

#define CONTRACTS_PRECONDITIONS
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
//using System.Windows.Forms;
using CodeUnderTest;


namespace Tests {

  [TestClass]
  public class RewriteCodeUnderTestBeforeRunningUnitTests : DisableAssertUI {

    static void ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
      Console.WriteLine("{0}", e.Data);
    }

    static void OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
      Console.WriteLine("{0}", e.Data);
    }
    private static int RunProcess(string cwd, string tool, string arguments)
    {
      ProcessStartInfo i = new ProcessStartInfo(tool, arguments);
      Console.WriteLine("Running '{0}'", i.FileName);
      Console.WriteLine("         {0}", i.Arguments);
      i.RedirectStandardOutput = true;
      i.RedirectStandardError = true;
      i.UseShellExecute = false;
      i.CreateNoWindow = true;
      i.WorkingDirectory = cwd;
      i.ErrorDialog = false;
      using (Process p = Process.Start(i))
      {
        p.OutputDataReceived += new DataReceivedEventHandler(OutputDataReceived);
        p.ErrorDataReceived += new DataReceivedEventHandler(ErrorDataReceived);
        p.BeginOutputReadLine();
        p.BeginErrorReadLine();

        Assert.IsTrue(p.WaitForExit(200000), String.Format("{0} timed out", i.FileName));
        if (p.ExitCode != 0)
        {
          Assert.AreEqual(0, p.ExitCode, "{0} returned an errorcode of {1}.", i.FileName, p.ExitCode);
        }
        return p.ExitCode;
      }
    }


    [AssemblyInitialize]
    public static void AssembyInitialize(TestContext context)
    {

        //
        // VERY IMPORTANT!! Do NOT do anything that caused any type from CodeUnderTest.dll to be loaded
        // before the assembly is rewritten!!!
        // If that happens, then the assembly is loaded (but not locked) so that the rewriting appears
        // to happen successfully, but the code that is executed is from the unrewritten assembly!!!!!
        //

        Console.WriteLine(System.DateTime.Now.ToString());

        int x;
        const string AsmMetaExe = @"Microsoft.Research\ImportedCCI2\AsmMeta.exe";

        var deploymentDir = Directory.GetCurrentDirectory();

        var testResultPosition = deploymentDir.IndexOf(@"TestResults");

        Assert.IsTrue(testResultPosition != -1, "Can't find the TestResults directory!!!");

        var testDirRoot = deploymentDir.Substring(0, testResultPosition);

        Assert.IsTrue(!string.IsNullOrEmpty(testDirRoot), "I was expecting the extraction of a valid dir");

        var RootDirectory = Path.GetFullPath(testDirRoot);

        var foxtrotPath = SelectFoxtrot(RootDirectory, deploymentDir);


      Assert.IsTrue(File.Exists("OutOfBand.dll"));
      var tool = Path.GetFullPath(Path.Combine(RootDirectory, AsmMetaExe));

      Assert.IsTrue(File.Exists(tool), string.Format("The tool {0} does not exists!!!!", tool));

      x = RunProcess(deploymentDir, tool, @"/libpaths:..\..\..\Microsoft.Research\Imported\ReferenceAssemblies\v3.5 OutOfBand.dll");
      Assert.AreEqual(0, x, "AsmMeta failed on OutOfBand.dll");
      Assert.IsTrue(File.Exists("OutOfBand.Contracts.dll"), "AsmMeta must have failed to produce a reference assembly for OutOfBand.dll");

      string codeUnderTest = "CodeUnderTest.dll";
#if LEGACY
      var explicitValidation = "legacy";
#else
      var explicitValidation = "standard";
#endif
      if (System.Diagnostics.Debugger.IsAttached) {
        x = RunProcess(deploymentDir, foxtrotPath, String.Format("/originalFiles /break /useGAC /assemblyMode={0} /rewrite /rw:RewriterMethods.dll,TestRewriterMethods /callsiterequires /throwonfailure=false {1}", explicitValidation, codeUnderTest));
      }
      else {
        x = RunProcess(deploymentDir, foxtrotPath, String.Format("/useGAC /assemblyMode={0} /rewrite /rw:RewriterMethods.dll,TestRewriterMethods /nobox /callsiterequires /throwonfailure=false {1}", explicitValidation, codeUnderTest));
      }

      var peVerify = Path.GetFullPath(Path.Combine(RootDirectory, @"Microsoft.Research\Imported\tools\.NetFramework\v4.0\peverify.exe"));
      Assert.IsTrue(File.Exists(peVerify), string.Format("Can't find peVerifiy: {0} ", peVerify));

      x = RunProcess(deploymentDir, peVerify, "/unique " + codeUnderTest);
      Assert.AreEqual(0, x, "Peverify failed on " + codeUnderTest);

      x = RunProcess(deploymentDir, foxtrotPath, "/rewrite /assemblyMode=standard /rw:RewriterMethods.dll,TestRewriterMethods /nobox /throwonfailure /contracts:OutOfBand.Contracts.dll /autoRef:- OutOfBand.dll");
      Assert.AreEqual(0, x, "Rewriter failed on OutOfBand.dll");

      x = RunProcess(deploymentDir, peVerify, "/unique OutOfBand.dll");
      Assert.AreEqual(0, x, "Peverify failed on OutOfBand.dll");

      x = RunProcess(deploymentDir, Path.GetFullPath(Path.Combine(RootDirectory, AsmMetaExe)), @"/libpaths:..\..\..\Microsoft.Research\Imported\ReferenceAssemblies\v3.5 BaseClassWithContracts.dll");
      Assert.AreEqual(0, x, "AsmMeta failed on BaseClassWithContracts.dll");

      Assert.IsTrue(File.Exists("SubtypeWithoutContracts.dll"));
      x = RunProcess(deploymentDir, foxtrotPath, "/rewrite /rw:RewriterMethods.dll,TestRewriterMethods /nobox /throwonfailure SubtypeWithoutContracts.dll");
      Assert.AreEqual(0, x, "Rewriter failed on SubtypeWithoutContracts.dll");

      x = RunProcess(deploymentDir, peVerify, "/unique SubtypeWithoutContracts.dll");
      Assert.AreEqual(0, x, "Peverify failed on SubtypeWithoutContracts.dll");

    }

    static readonly string[] DeployedForRunningFoxtrotFromDeployment = new string[] { "Foxtrot.exe", "DataStructures.dll", "System.Compiler.dll" };

    private static string SelectFoxtrot(string RootDirectory, string deploymentdir) {
      
      if (Contract.ForAll(DeployedForRunningFoxtrotFromDeployment, name => 
                          File.Exists(Path.GetFullPath(Path.Combine(deploymentdir, name))))){

        return Path.GetFullPath(Path.Combine(deploymentdir, "Foxtrot.exe"));
      } else {
        return Path.GetFullPath(Path.Combine(RootDirectory, @"Foxtrot\Driver\bin\Debug\Foxtrot.exe"));
      }
    }

    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.RewrittenContractTest).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }
  }

  public class DisableAssertUI {
    static DisableAssertUI() {
      for (int i = 0; i < System.Diagnostics.Debug.Listeners.Count; i++) {
        System.Diagnostics.DefaultTraceListener dtl = System.Diagnostics.Debug.Listeners[i] as System.Diagnostics.DefaultTraceListener;
        if (dtl != null) dtl.AssertUiEnabled = false;
      }
    }
  }

  [TestClass]
  public class RewrittenContractTest : DisableAssertUI {
    #region Tests

    [TestInitialize]
    public void TestInitialize() {
      TestRewriterMethods.FailureCount = 0;
    }

    /// <summary>
    /// Checks that RaiseContractFailedEvent hook is called even on legacy requires methods.
    /// </summary>
    [TestCleanup]
    public void TestCleanup() {
      int expectedFailures = this.TestContext.TestName.StartsWith("Positive") ? 0 : 1;
      Assert.AreEqual(expectedFailures, TestRewriterMethods.FailureCount);
    }

    public TestContext TestContext {
      get;
      set;
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveAssertTest() {
      new CodeUnderTest.RewrittenContractTest().AssertsTrue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeAssertTest() {
      try {
        new CodeUnderTest.RewrittenContractTest().AssertsTrue(false);
        throw new Exception();
      } catch (TestRewriterMethods.AssertException e) {
        Assert.AreEqual("mustBeTrue", e.Condition);
      }
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveAssumeTest() {
      new CodeUnderTest.RewrittenContractTest().AssumesTrue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeAssumeTest() {
      try {
        new CodeUnderTest.RewrittenContractTest().AssumesTrue(false);
        throw new Exception();
      } catch (TestRewriterMethods.AssumeException e) {
        Assert.AreEqual("mustBeTrue", e.Condition);
      }
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveRequiresTest() {
      new CodeUnderTest.RewrittenContractTest().RequiresTrue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeRequiresTest() {
      new CodeUnderTest.RewrittenContractTest().RequiresTrue(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveRequiresWithExceptionTest() {
      new CodeUnderTest.RewrittenContractTest().RequiresTrue<ArgumentException>(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(ArgumentException))]
    public void NegativeRequiresWithExceptionTest() {
      new CodeUnderTest.RewrittenContractTest().RequiresTrue<ArgumentException>(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeRequiresWithExceptionTest2() {
      try {
        new CodeUnderTest.RewrittenContractTest().RequiresTrue<ArgumentException>(false);
      } catch (ArgumentException e) {
        Assert.AreEqual("Precondition failed: mustBeTrue", e.Message, true);
      }
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositivePrivateRequiresTest() {
      new CodeUnderTest.RewrittenContractTest().CallPrivateRequiresTrue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositivePrivateRequiresTest2() {
      // Does not trigger due to /publicsurface
      new CodeUnderTest.RewrittenContractTest().CallPrivateRequiresTrue(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEnsuresTest() {
      new CodeUnderTest.RewrittenContractTest().EnsuresTrue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresTest() {
      new CodeUnderTest.RewrittenContractTest().EnsuresTrue(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositivePrivateEnsuresTest() {
      new CodeUnderTest.RewrittenContractTest().CallPrivateEnsuresTrue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositivePrivateEnsuresTest2() {
      // Does not trigger due to /publicsurface
      new CodeUnderTest.RewrittenContractTest().CallPrivateEnsuresTrue(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEnsureReturnTest() {
      new CodeUnderTest.RewrittenContractTest().EnsuresReturnsTrue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresReturnTest() {
      new CodeUnderTest.RewrittenContractTest().EnsuresReturnsTrue(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEnsuresAnythingTest() {
      new CodeUnderTest.RewrittenContractTest().EnsuresAnything(true);
      new CodeUnderTest.RewrittenContractTest().EnsuresAnything(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveRequiresAndEnsuresTest() {
      new CodeUnderTest.RewrittenContractTest().RequiresAndEnsuresTrue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeRequiresAndEnsuresTest() {
      new CodeUnderTest.RewrittenContractTest().RequiresAndEnsuresTrue(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveInvariantTest() {
      new CodeUnderTest.RewrittenContractTest().MakeInvariantTrue();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeInvariantTest() {
      new CodeUnderTest.RewrittenContractTest().MakeInvariantFalse();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEnsureResultTest() {
      bool output;
      new CodeUnderTest.RewrittenContractTest().EnsuresSetsTrue(true, out output);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsureResultTest() {
      bool output;
      new CodeUnderTest.RewrittenContractTest().EnsuresSetsTrue(false, out output);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveOldTest() {
      bool anything = true;
      Assert.AreEqual(true, new CodeUnderTest.RewrittenContractTest().SetsAndReturnsOld(ref anything));
      Assert.IsFalse(anything);
      Assert.AreEqual(false, new CodeUnderTest.RewrittenContractTest().SetsAndReturnsOld(ref anything));
      Assert.IsTrue(anything);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveThrowTest() {
      new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(0, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(ApplicationException))]
    public void PositiveThrowTest1() {
      new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(1, true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(SystemException))]
    public void PositiveThrowTest2() {
      new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(2, true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    //[ExpectedException(typeof(Exception))] VS Unit Tests can't handle this.
    public void PositiveThrowTest3() {
      try {
        new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(3, true);
      } catch (Exception e) {
        Assert.IsTrue(e.GetType() == typeof(Exception));
        return;
      }
      Assert.Fail();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeThrowTest0() {
      new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(0, true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionOnThrowException))]
    public void NegativeThrowTest1() {
      new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(1, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionOnThrowException))]
    public void NegativeThrowTest2() {
      new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(2, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionOnThrowException))]
    public void NegativeThrowTest3() {
      new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(3, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveRequiresForAll() {
      int[] A = new int[] { 3, 4, 5 };
      new CodeUnderTest.RewrittenContractTest().RequiresForAll(A);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeRequiresForAll() {
      int[] A = new int[] { 3, -4, 5 };
      new CodeUnderTest.RewrittenContractTest().RequiresForAll(A);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveRequiresExists() {
      int[] A = new int[] { -3, -4, 5 };
      new CodeUnderTest.RewrittenContractTest().RequiresExists(A);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeRequiresExists() {
      int[] A = new int[] { -3, -4, -5 };
      new CodeUnderTest.RewrittenContractTest().RequiresExists(A);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEnsuresForAll() {
      int[] A = new int[] { 3, 4, 5 };
      int[] B = new CodeUnderTest.RewrittenContractTest().EnsuresForAll(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresForAll() {
      int[] A = new int[] { 3, -4, 5 };
      int[] B = new CodeUnderTest.RewrittenContractTest().EnsuresForAll(A, false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEnsuresExists() {
      int[] A = new int[] { -3, -4, 5 };
      int[] B = new CodeUnderTest.RewrittenContractTest().EnsuresExists(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresExists() {
      int[] A = new int[] { -3, -4, -5 };
      int[] B = new CodeUnderTest.RewrittenContractTest().EnsuresExists(A, false);
    }


    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEnsuresForAllWithOld() {
      int[] A = new int[] { 3, 4, 5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresForAllWithOld(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresForAllWithOld() {
      int[] A = new int[] { 3, -4, 5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresForAllWithOld(A, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEnsuresDoubleClosure() {
      int[] A = new int[] { 3, 4, 5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresDoubleClosure(A, A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresDoubleClosure() {
      int[] A = new int[] { 3, 4, 5 };
      int[] B = new int[] { 3, 4 };
      new CodeUnderTest.RewrittenContractTest().EnsuresDoubleClosure(A, B, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEnsuresExistsWithOld() {
      int[] A = new int[] { -3, -4, 5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresExistsWithOld(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresExistsWithOld() {
      int[] A = new int[] { -3, -4, -5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresExistsWithOld(A, false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEnsuresWithResultAndQuantifiers() {
      int[] A = new int[] { -3, -4, -5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresWithResultAndQuantifiers(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresWithResultAndQuantifiers() {
      int[] A = new int[] { -3, -4, -5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresWithResultAndQuantifiers(A, false);
    }
#if TURNINGSTATICCLOSUREINTOOBJECTCLOSURE
        [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
        public void PositiveEnsuresWithStaticClosure()
        {
          EnsuresWithStaticClosure(true);
        }
        [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
        [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
        public void NegativeEnsuresWithStaticClosure()
        {
          EnsuresWithStaticClosure(false);
        }
#endif

    #region IfThenThrow
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveIfThenThrowAsPrecondition() {
      new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition(true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(System.ArgumentException))]
    public void NegativeIfThenThrowAsPrecondition() {
      new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveIfThenThrowAsPrecondition2() {
      new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition2(1);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
    public void NegativeIfThenThrowAsPrecondition2() {
      new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition2(-1);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveIfThenThrowAsPrecondition3() {
      new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition3(3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
    public void NegativeIfThenThrowAsPrecondition3a() {
      new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition3(-1);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeIfThenThrowAsPrecondition3b() {
      new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition3(2);
    }

    #endregion IfThenThrow

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveUseAParamsMethodInContract() {
      new CodeUnderTest.RewrittenContractTest().UseAParamsMethodInContract(3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeUseAParamsMethodInContract() {
      new CodeUnderTest.RewrittenContractTest().UseAParamsMethodInContract(5);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveAssertArgumentIsPositive() {
      new CodeUnderTest.RewrittenContractTest().AssertArgumentIsPositive(3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeAssertArgumentIsPositive() {
      try {
        new CodeUnderTest.RewrittenContractTest().AssertArgumentIsPositive(-3);
        throw new Exception();
      } catch (TestRewriterMethods.AssertException e) {
        Assert.AreEqual("0 < y", e.Condition);
      }
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveOldExpressionWithoutGoodNameToUseForLocal() {
      int a = 3;
      int b = 5;
      new CodeUnderTest.RewrittenContractTest().Swap(ref a, ref b, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeOldExpressionWithoutGoodNameToUseForLocal1() {
      int a = 3;
      int b = 3;
      new CodeUnderTest.RewrittenContractTest().Swap(ref a, ref b, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeOldExpressionWithoutGoodNameToUseForLocal2() {
      int a = 3;
      int b = 5;
      new CodeUnderTest.RewrittenContractTest().Swap(ref a, ref b, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositivePostConditionUsingResultAsCallee() {
      new CodeUnderTest.RewrittenContractTest().PostConditionUsingResultAsCallee(true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativePostConditionUsingResultAsCallee() {
      new CodeUnderTest.RewrittenContractTest().PostConditionUsingResultAsCallee(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeRequiresExceptionTest1() {
      try {
        new CodeUnderTest.RewrittenContractTest().RequiresArgumentNullException(null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentNullException a) {
        Assert.AreEqual("parameter", a.ParamName);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeRequiresExceptionTest2() {
      try {
        new CodeUnderTest.RewrittenContractTest().RequiresArgumentOutOfRangeException(-1);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentOutOfRangeException a) {
        Assert.AreEqual("parameter", a.ParamName);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeRequiresExceptionTest3() {
      try {
        new CodeUnderTest.RewrittenContractTest().RequiresArgumentException(0, 1);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        //Assert.AreEqual(null, a.ParamName);
        Assert.AreEqual("Precondition failed: x >= y: x must be at least y\r\nParameter name: x must be at least y", a.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveTestForMemberVisibility1() {
      CodeUnderTest.RewrittenContractTest.TestForMemberVisibility1(3); // call is not really needed since the test is to make sure the rewriter doesn't crash.
    }
    #endregion
  }

  /// <summary>
  /// Test to make sure that if an invariant mentions a public method
  /// (which itself then checks the invariant) an infinite loop does
  /// not result. When re-entered, an invariant is supposed to just
  /// "return true" (i.e., it is a void method so it just returns
  /// without throwing an exception).
  /// </summary>
  [TestClass]
  public class ReEntrantInvariantTest : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.ReEntrantInvariantTest).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestReenterInvariant() {
      var c = new CodeUnderTest.ReEntrantInvariantTest.ClassWithInvariant();
      c.ReEnter();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void FailInvariant1() {
      var c = new CodeUnderTest.ReEntrantInvariantTest.ClassWithInvariant();
      try {
        c.FailInvariant1();
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.InvariantException e) {
        Assert.AreEqual("b", e.Condition);
      }
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void FailInvariant2() {
      var c = new CodeUnderTest.ReEntrantInvariantTest.ClassWithInvariant();
      try {
        c.FailInvariant2();
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.InvariantException e) {
        Assert.AreEqual("PureMethod()", e.Condition);
      }
    }
    #endregion Tests
  }

  /// <summary>
  /// Test to make sure that an invariant in a class does not get
  /// "inherited" by any nested classes.
  /// </summary>
  [TestClass]
  public class ClassWithInvariantAndNestedClass : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.ClassWithInvariantAndNestedClass).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void ReEnterInvariant() {
      // Don't need to call another method because this method
      // must be public and so gets rewritten to call the invariant.
    }

    class Inner { }

    #endregion Tests
  }

  [TestClass]
  public class LocalsOfStructTypeEndingUpInAssignments : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveLocalOfStructType() {
      var l = new CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.MyCollection<int, CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.Positive>();
      l.Add(5);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeLocalOfStructType1() {
      var l = new CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.MyCollection<int, CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.Positive>();
      l.Add(0);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeLocalOfStructType2() {
      var l = new CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.MyCollection<int, CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.Positive>();
      l.Pull();
    }
    #endregion Tests
  }

  [TestClass]
  public class LegacyPreconditions : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.LegacyPreconditions).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveLegacyRequiresDirectThrow() {
      var c = new CodeUnderTest.LegacyPreconditions.C();
      c.DirectThrow(3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeLegacyRequiresDirectThrow() {
      var c = new CodeUnderTest.LegacyPreconditions.C();
      c.DirectThrow(-3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveLegacyRequiresIndirectThrow() {
      var c = new CodeUnderTest.LegacyPreconditions.C();
      c.IndirectThrow(3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeLegacyRequiresIndirectThrow() {
      var c = new CodeUnderTest.LegacyPreconditions.C();
      c.IndirectThrow(-3);
    }
    #endregion Tests
  }

  [TestClass]
  public class GenericClassWithDeferringCtor :DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.GenericClassWithDeferringCtor).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveGenericClassWithDeferringCtor() {
      var c = new CodeUnderTest.GenericClassWithDeferringCtor.C<int>('a');
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeGenericClassWithDeferringCtor() {
      var c = new CodeUnderTest.GenericClassWithDeferringCtor.C<int>('z');
    }
    #endregion Tests
  }

  [TestClass]
  public class IfaceContractUsingLocalToHoldSelf : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.IfaceContractUsingLocalToHoldSelf).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveIfaceContractUsingLocalToHoldSelf() {
      CodeUnderTest.IfaceContractUsingLocalToHoldSelf.J j = new CodeUnderTest.IfaceContractUsingLocalToHoldSelf.B();
      int[] A = new int[] { 3, 4, 5 };
      j.M(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeIfaceContractUsingLocalToHoldSelf1() {
      CodeUnderTest.IfaceContractUsingLocalToHoldSelf.J j = new CodeUnderTest.IfaceContractUsingLocalToHoldSelf.B();
      int[] A = new int[] { 3, 0, 5 };
      j.M(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeIfaceContractUsingLocalToHoldSelf2() {
      CodeUnderTest.IfaceContractUsingLocalToHoldSelf.J j = new CodeUnderTest.IfaceContractUsingLocalToHoldSelf.B();
      int[] A = new int[] { 3, 4, 5 };
      j.M(A, false);
    }
    #endregion Tests
  }

  [TestClass]
  public class EvaluateOldExpressionsAfterPreconditions :DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.EvaluateOldExpressionsAfterPreconditions).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEvaluateOldExpressionsAfterPreconditions() {
      var c = new CodeUnderTest.EvaluateOldExpressionsAfterPreconditions.C();
      int[] A = new int[] { 3, 4, 5 };
      c.M(A);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeEvaluateOldExpressionsAfterPreconditions() {
      var c = new CodeUnderTest.EvaluateOldExpressionsAfterPreconditions.C();
      c.M(null); // if the old value for xs.Length is evaluated before the precondition this results in a null dereference
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEvaluateFailingOldExpression1() {
      var c = new CodeUnderTest.EvaluateOldExpressionsAfterPreconditions.C();
      c.ConditionalOld(null);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveEvaluateFailingOldExpression2() {
      var c = new CodeUnderTest.EvaluateOldExpressionsAfterPreconditions.C();
      c.ConditionalOld(new int[] { 3, 4 });
    }
    #endregion Tests
  }

  [TestClass]
  public class PostConditionsInStructCtorsMentioningFields : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.PostConditionsInStructCtorsMentioningFields).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveFieldsInStructCtorEnsures() {
      var s = new CodeUnderTest.PostConditionsInStructCtorsMentioningFields.S(3, 4, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeFieldsInStructCtorEnsures() {
      var s = new CodeUnderTest.PostConditionsInStructCtorsMentioningFields.S(3, 4, false);
    }
    #endregion Tests
  }

  [TestClass]
  public class PrivateInvariantInSealedType : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.PrivateInvariantInSealedType).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositivePrivateInvariantMethod() {
      var t = new CodeUnderTest.PrivateInvariantInSealedType.T(3, 4, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativePrivateInvariantMethod() {
      var t = new CodeUnderTest.PrivateInvariantInSealedType.T(3, 4, false);
    }
    #endregion Tests
  }

  [TestClass]
  public class ConstructorsWithClosures : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.ConstructorsWithClosures).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveCtorWithClosure1() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping("hello");
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveCtorWithClosure2() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(new Object());
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveCtorWithClosure3() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping("hello", true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveCtorWithClosure4() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(new object(), false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveCtorWithClosure5() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping("Hello", new[] { "Foo", "Bar", "Hello" });
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveCtorWithClosure6() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping("Hello", "Hello");
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeCtorWithClosure1() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeCtorWithClosure2() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(null, false);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeCtorWithClosure3() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(null, (string[])null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeCtorWithClosure4() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping("Hello", new[] { "A", "b", "D" });
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException a) {
        Assert.AreEqual(": Contract.Exists(0, data.Length, i => data[i] == this.name)", a.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeCtorWithClosure5() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(null, (string)null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }

    /// <summary>
    /// Structs
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveStructCtorWithClosure1() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors("hello");
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveStructCtorWithClosure2() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(new Object());
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveStructCtorWithClosure3() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors("hello", true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveStructCtorWithClosure4() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(new object(), false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveStructCtorWithClosure5() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors("Hello", new[] { "Foo", "Bar", "Hello" });
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveStructCtorWithClosure6() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors("Hello", "Hello");
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeStructCtorWithClosure1() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeStructCtorWithClosure2() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(null, false);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeStructCtorWithClosure3() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(null, (string[])null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeStructCtorWithClosure4() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors("Hello", new[] { "A", "b", "D" });
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException a) {
        Assert.AreEqual(": Contract.Exists(0, data.Length, i => data[i] == (encoder as string))", a.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeStructCtorWithClosure5() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(null, (string)null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }

    #endregion Tests
  }

  /// <summary>
  /// Test Matrix: 
  /// Variable 1: Is it class/struct/generic? (non-generic class, generic class, generic struct)
  /// Variable 2: Is Method generic? (generic/non-generic)
  /// Variable 3: Does contract involve (static| instance) closure? (no closure/static closure/instance closure with capture/static closure with capture)
  /// 
  /// In Test2 and Test3: we further test:
  /// Variable 4: (legacy precondition| precondition) + postcondition + p|n
  /// 
  /// Test2.Test1a: non-generic method static closure requires-ensures positive | requires negative (2 calls)
  /// Test2.Test1aa: non-genric method static closure requires positive ensures negative (1 call)
  /// Test2.Test1b: non-generic method legacy requires no clousre: (2 calls p|n)
  /// Test2.Test1c: generic method legacy requires static closure with capture
  /// Test2.Test1d: generic method static closure precondition failable (3 calls, 2n | 1p)
  /// Test2.Test1f: generic method static closure with capture (3 calls, 2n-precondition fails different ways| 1p)
  /// Test2.Test1g: generic method instance closure with capture (3 calls, 2n-precondition fails different ways |1p)
  /// Test2.Test1h: generic method static closure with capture/generic with constraints
  /// 
  /// Test3.Test1a: generic class, non-generic method, static closure
  /// Test3.Test1aa: generic class, non generic method, static closure, requires positive, ensures negative
  /// Test3.Test1b: generic class, non-generic method, legacy requires, no clousre
  /// Test3.Test1c: generic class, non-generic method, mixed requires, static closure
  /// Test3.Test1d: generic class, generic method, static closure
  /// Test3.Test1e: generic class, generic method, static closure
  /// Test3.Test1g: generic class, generic method, instance closure
  /// Test3.Test1h: a comprehensive test case
  /// 
  /// Test4.Test1a: generic struct, non-generic method static closure
  /// Test4.Test1c: generic struct, generic method static closure
  ///
  /// In addtion: Test5x tests when the return type is IEnumerator.
  /// 
  /// Drivers' naming convention:
  /// 
  /// Iterator + ("" (non-generic type non-generic method) | GenCls | GenMeth | GenClsMeth | GenStr |GenStrMth) + (Pos | Neg) + ( "" (meaning precondition) | Post) + ( "" (meaning parameter) | collection | elem) + "NN | Eq ... | LT |...".
  /// </summary>
  [TestClass]
  public class IteratorSimpleContract : DisableAssertUI {
    #region Test Drivers
    #region Non-generic class drivers
    /// <summary>
    /// This driver passes the preconditions of Test1a.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorPositiveNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      test2.Test1a("hello"); // Precondition of Test1a holds
    }

    /// <summary>
    /// This driver violates the precondition of Test1a.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorNegativeNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      test2.Test1a(null); // Precondition of Test1a violated.
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1aa.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorNegativePostAllNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      test2.Test1aa("hello"); // Postcondition of Test1aa is invalid. 
    }

    /// <summary>
    /// This driver tests a legacy precondition
    /// Legacy contract throws an exception when violated (and is caught by the driver).
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorNegativeLegacyArgNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      try
      {
        test2.Test1b(null);
      } catch (ArgumentException) {
        // An ArgumentException is thrown by the legacy precondition. 
      }
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorPositiveLegacyArgNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      test2.Test1b("hello");
    }

    /// <summary>
    /// This driver fails the first precondition of Test1d.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorNegativeCollectionNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      IEnumerable<string> x = null;
      test2.Test1d(x);
    }

    /// <summary>
    /// This driver fails the second precondition of Test1d.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorNegativeElemNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      string[] strs = { null };
      test2.Test1d(strs);
    }

    /// <summary>
    /// This driver passes the preconditions of Test1d.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorPositivePostElemNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      string[] strs = { "hello" };
      test2.Test1d(strs);
    }

    /// <summary>
    /// This driver passes preconditions of Test1f.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorPositiveElemLTMax() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      int[] xs = { 2, 3, 4, 8 };
      test2.Test1f(xs, 10);
    }

    /// <summary>
    /// This driver fails the precondition of Test1f.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorNegativeElemLTMax() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      int[] xs = { 2, 3, 4, 11 };
      test2.Test1f(xs, 10);
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1f.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorNegativePostElemLTMax() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      int[] xs = { 2, 3, 4, 9 };
      test2.Test1f(xs, 10);
    }

    /// <summary>
    /// This driver passes Test1g.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorPositivePostElemEqParam2() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      string[] strs = { "hello", "hello" };
      test2.Test1g(strs, "hello");
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorNegativePostElemEqParam2() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      string[] strs = { "aaa", "hello" };
      test2.Test1g(strs, "hello");
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1h. 
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorNegativePostElemLTMaxConstrained() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      test2.Test2h();
    }
    #endregion NonGeneric Class Drivers
    #region Generic Class Drivers
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorGenClsPosNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      test3.Test1a(new CodeUnderTest.IteratorSimpleContract.A("hello"));
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenClsNegNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      test3.Test1a(null);
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1aa.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorGenClsNegPostAllNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      test3.Test1aa(new CodeUnderTest.IteratorSimpleContract.A(""));
    }

    /// <summary>
    /// This driver tests a legacy precondition
    /// Legacy contract throws an exception when violated (and is caught by the driver).
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorGenClsNegLegacyArgNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      try
      {
        test3.Test1b(null);
      } catch (ArgumentException) {
        // An ArgumentException is thrown by the legacy precondition. 
      }
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorGenClsPosLegacyArgNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      test3.Test1b(new CodeUnderTest.IteratorSimpleContract.A("hello"));
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenClsMethNegCollectionNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      IEnumerable<CodeUnderTest.IteratorSimpleContract.A> x = null;
      test3.Test1d(x);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenClsMethNegElemNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      CodeUnderTest.IteratorSimpleContract.A[] aa = { null };
      test3.Test1d(aa);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorGenClsMethPosPostElemNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A[] strs = { new CodeUnderTest.IteratorSimpleContract.A("hello") };
      test3.Test1d(strs);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorGenClsMethPosElemMethEqParamMeth() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("54321") };
      test3.Test1e(aa, a);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenClsMethNegElemMethEqParamMeth() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("5432") };
      test3.Test1e(aa, a);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorGenClsMethInsClPosElemEqParam() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("54321") };
      test3.Test1g(aa, a);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenClsMethInsClNegElemEqParam() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("321") };
      test3.Test1g(aa, a);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorGenClsMethInsClNegPostElemLTProp() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("45"), new CodeUnderTest.IteratorSimpleContract.A("321") };
      test3.Test1h(aa);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorGenClsMethPostElemLTProp() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("1234567"), new CodeUnderTest.IteratorSimpleContract.A("213456") };
      test3.Test1h(aa);
    }
    #endregion Generic Class Drivers
    #region Generic Struct Drivers
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorGenStrPosNN() {
      var test4 = new CodeUnderTest.IteratorSimpleContract.Test4<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      test4.Test1a(new CodeUnderTest.IteratorSimpleContract.A("hello"));
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenStrNegNN() {
      var test4 = new CodeUnderTest.IteratorSimpleContract.Test4<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      test4.Test1a(null);
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1aa.
    /// </summary>
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorGenStrNegPostAllNN() {
      var test4 = new CodeUnderTest.IteratorSimpleContract.Test4<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      test4.Test1aa(new CodeUnderTest.IteratorSimpleContract.A(""));
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IteratorGenStrMethPosElemMethEqParamMeth() {
      var test4 = new CodeUnderTest.IteratorSimpleContract.Test4<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("54321") };
      test4.Test1e(aa, a);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenStrMethNegElemMethEqParamMeth() {
      var test4 = new CodeUnderTest.IteratorSimpleContract.Test4<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("5432") };
      test4.Test1e(aa, a);
    }

    #endregion Generic Struct Drivers
    #region IEnumerator Drivers
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void IEnumeratorPos() {
      new CodeUnderTest.IteratorSimpleContract().Test5a(2);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IEnumeratorNeg() {
      new CodeUnderTest.IteratorSimpleContract().Test5a(0);
    }
    #endregion
    #endregion Test Drivers
  }

  [TestClass]
  public class LocalVariableForResult : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.LocalVariableForResult).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveLocalAsResult1() {
      var c = new CodeUnderTest.LocalVariableForResult.C();
      c.Test1(1);
    }
    public void PositiveLocalAsResult2() {
      var c = new CodeUnderTest.LocalVariableForResult.C();
      c.Test2(1, new bool[] { });
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveLocalAsResult3() {
      var c = new CodeUnderTest.LocalVariableForResult.C();
      c.Test2(1, new bool[] { true, true, true });
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeLocalAsResult1() {
      try {
        var c = new CodeUnderTest.LocalVariableForResult.C();
        c.Test1(2);
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException p) {
        Assert.AreEqual(": result != false", p.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeLocalAsResult2() {
      try {
        var c = new CodeUnderTest.LocalVariableForResult.C();
        c.Test2(2, new bool[] { });
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException p) {
        Assert.AreEqual(": result != false", p.Message);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeLocalAsResult3() {
      try {
        var c = new CodeUnderTest.LocalVariableForResult.C();
        c.Test2(1, new bool[] { true, false });
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException p) {
        Assert.AreEqual(": Contract.ForAll(0, arr.Length, i => arr[i] == result)", p.Message);
      }
    }

    #endregion Tests
  }

  [TestClass]
  public class IgnoreRuntimeAttributeTest : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.IgnoreRuntimeAttributeTest).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveIgnoreRuntimeAttributeTest() {
      var c = new CodeUnderTest.IgnoreRuntimeAttributeTest.C();
      c.M(3, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeIgnoreRuntimeAttributeTest() {
      try {
        var c = new CodeUnderTest.IgnoreRuntimeAttributeTest.C();
        c.M(2, false);
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException p) {
        Assert.AreEqual(": Contract.Result<bool>()", p.Message);
      }
    }

    #endregion Tests
  }

  /// <summary>
  /// All parameters mentioned in postconditions that are not already
  /// within an old expression should get wrapped in one so that
  /// their initial value is used in the postcondition in case the
  /// parameter is updated in the method body.
  /// </summary>
  [TestClass]
  public class WrapParametersInOld : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(CodeUnderTest.WrapParametersInOld).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveWrapParam1() {
      var c = new CodeUnderTest.WrapParametersInOld.C(3);
      c.Identity(true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveWrapParam2() {
      var c = new CodeUnderTest.WrapParametersInOld.C(3);
      c.Identity(false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveWrapParam3() {
      var c = new CodeUnderTest.WrapParametersInOld.C(3);
      var s = new CodeUnderTest.WrapParametersInOld.C.S();
      s.x = 3;
      c.F(s);
    }
    #endregion Tests
  }
}