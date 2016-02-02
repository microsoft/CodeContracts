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

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
//using System.Windows.Forms;
using CodeUnderTest;
using Xunit;
using Xunit.Abstractions;
using System.Reflection;

namespace Tests {

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
      StringBuilder result = new StringBuilder();
      using (Process p = Process.Start(i))
      {
        p.OutputDataReceived += (sender, e) => result.AppendLine(e.Data);
        p.ErrorDataReceived += (sender, e) => result.AppendLine(e.Data);
        p.BeginOutputReadLine();
        p.BeginErrorReadLine();

        Assert.True(p.WaitForExit(200000), String.Format("{0} timed out", i.FileName));
        if (p.ExitCode != 0)
        {
          Assert.True(false, string.Format("{0} returned an errorcode of {1} (expected 0).\n\n{2}", i.FileName, p.ExitCode, result.ToString()));
        }
        return p.ExitCode;
      }
    }

    public RewriteCodeUnderTestBeforeRunningUnitTests()
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

        var deploymentDir = @"..\..\..\..\..\TestResults\Out\Deploy";

        var testResultPosition = deploymentDir.IndexOf(@"TestResults");
        
        Assert.True(testResultPosition != -1, 
            string.Format("Can't find the TestResults directory!!! Current deployment directory is '{0}'", deploymentDir));
        
        var testDirRoot = deploymentDir.Substring(0, testResultPosition);

        Assert.True(!string.IsNullOrEmpty(testDirRoot), "I was expecting the extraction of a valid dir");

        var RootDirectory = Path.GetFullPath(testDirRoot);

        var foxtrotPath = SelectFoxtrot(RootDirectory, deploymentDir);

      Assert.True(File.Exists("OutOfBand.dll"));
      var tool = Path.GetFullPath(Path.Combine(RootDirectory, AsmMetaExe));

      Assert.True(File.Exists(tool), string.Format("The tool {0} does not exists!!!!", tool));

      var refAssemblies = @"..\..\..\Microsoft.Research\Imported\ReferenceAssemblies\v3.5";
      
    Assert.True(Directory.Exists(Path.Combine(deploymentDir, refAssemblies)),
          string.Format("Can't find reference assembly folder at {0}", Path.Combine(deploymentDir, refAssemblies)));

      Assert.True(File.Exists(tool), string.Format("The tool {0} does not exists!!!!", tool));

      Directory.CreateDirectory(deploymentDir);
      File.Delete(Path.Combine(deploymentDir, "OutOfBand.dll"));
      File.Delete(Path.Combine(deploymentDir, "OutOfBand.pdb"));
      File.Delete(Path.Combine(deploymentDir, "CodeUnderTest.dll"));
      File.Delete(Path.Combine(deploymentDir, "CodeUnderTest.pdb"));
      File.Delete(Path.Combine(deploymentDir, "RewriterMethods.dll"));
      File.Delete(Path.Combine(deploymentDir, "RewriterMethods.pdb"));
      File.Delete(Path.Combine(deploymentDir, "BaseClassWithContracts.dll"));
      File.Delete(Path.Combine(deploymentDir, "BaseClassWithContracts.pdb"));
      File.Delete(Path.Combine(deploymentDir, "SubtypeWithoutContracts.dll"));
      File.Delete(Path.Combine(deploymentDir, "SubtypeWithoutContracts.pdb"));

      File.Move("OutOfBand.dll", Path.Combine(deploymentDir, "OutOfBand.dll"));
      File.Move("OutOfBand.pdb", Path.Combine(deploymentDir, "OutOfBand.pdb"));
      File.Move("CodeUnderTest.dll", Path.Combine(deploymentDir, "CodeUnderTest.dll"));
      File.Move("CodeUnderTest.pdb", Path.Combine(deploymentDir, "CodeUnderTest.pdb"));
      File.Move("RewriterMethods.dll", Path.Combine(deploymentDir, "RewriterMethods.dll"));
      File.Move("RewriterMethods.pdb", Path.Combine(deploymentDir, "RewriterMethods.pdb"));
      File.Move("BaseClassWithContracts.dll", Path.Combine(deploymentDir, "BaseClassWithContracts.dll"));
      File.Move("BaseClassWithContracts.pdb", Path.Combine(deploymentDir, "BaseClassWithContracts.pdb"));
      File.Move("SubtypeWithoutContracts.dll", Path.Combine(deploymentDir, "SubtypeWithoutContracts.dll"));
      File.Move("SubtypeWithoutContracts.pdb", Path.Combine(deploymentDir, "SubtypeWithoutContracts.pdb"));

      File.Copy("Microsoft.Contracts.dll", Path.Combine(deploymentDir, "Microsoft.Contracts.dll"), true);

      x = RunProcess(deploymentDir, tool, string.Format(@"/libpaths:{0} OutOfBand.dll", refAssemblies));

      Assert.True(x == 0, "AsmMeta failed on OutOfBand.dll");
      Assert.True(File.Exists(Path.Combine(deploymentDir, "OutOfBand.Contracts.dll")), "AsmMeta must have failed to produce a reference assembly for OutOfBand.dll");

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

      var peVerify = Path.GetFullPath(Path.Combine(RootDirectory, @"Microsoft.Research\Imported\tools\v3.5\peverify.exe"));
      Assert.True(File.Exists(peVerify), string.Format("Can't find peVerifiy: {0} ", peVerify));

      x = RunProcess(deploymentDir, peVerify, "/unique " + codeUnderTest);
      Assert.True(x == 0, "Peverify failed on " + codeUnderTest);

      x = RunProcess(deploymentDir, foxtrotPath, "/rewrite /assemblyMode=standard /rw:RewriterMethods.dll,TestRewriterMethods /nobox /throwonfailure /contracts:OutOfBand.Contracts.dll /autoRef:- OutOfBand.dll");
      Assert.True(x == 0, "Rewriter failed on OutOfBand.dll");

      x = RunProcess(deploymentDir, peVerify, "/unique OutOfBand.dll");
      Assert.True(x == 0, "Peverify failed on OutOfBand.dll");

      x = RunProcess(deploymentDir, Path.GetFullPath(Path.Combine(RootDirectory, AsmMetaExe)), @"/libpaths:..\..\..\Microsoft.Research\Imported\ReferenceAssemblies\v3.5 BaseClassWithContracts.dll");
      Assert.True(x == 0, "AsmMeta failed on BaseClassWithContracts.dll");

      Assert.True(File.Exists(Path.Combine(deploymentDir, "SubtypeWithoutContracts.dll")));
      x = RunProcess(deploymentDir, foxtrotPath, "/rewrite /rw:RewriterMethods.dll,TestRewriterMethods /nobox /throwonfailure SubtypeWithoutContracts.dll");
      Assert.True(x == 0, "Rewriter failed on SubtypeWithoutContracts.dll");

      x = RunProcess(deploymentDir, peVerify, "/unique SubtypeWithoutContracts.dll");
      Assert.True(x == 0, "Peverify failed on SubtypeWithoutContracts.dll");

      AppDomain.CurrentDomain.AssemblyResolve +=
        (sender, e) =>
        {
          switch (e.Name)
          {
          case "BaseClassWithContracts, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null":
            return Assembly.LoadFrom(Path.Combine(deploymentDir, "BaseClassWithContracts.dll"));

          case "CodeUnderTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null":
            return Assembly.LoadFrom(Path.Combine(deploymentDir, "CodeUnderTest.dll"));

          case "OutOfBand, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null":
            return Assembly.LoadFrom(Path.Combine(deploymentDir, "OutOfBand.dll"));

          case "RewriterMethods, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null":
            return Assembly.LoadFrom(Path.Combine(deploymentDir, "RewriterMethods.dll"));

          case "SubtypeWithoutContracts, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null":
            return Assembly.LoadFrom(Path.Combine(deploymentDir, "SubtypeWithoutContracts.dll"));

          default:
            return null;
          }
        };
    }

    static readonly string[] DeployedForRunningFoxtrotFromDeployment = new string[] { "Foxtrot.exe", "DataStructures.dll", "System.Compiler.dll" };

    private static string SelectFoxtrot(string RootDirectory, string deploymentdir) {
      
      if (Contract.ForAll(DeployedForRunningFoxtrotFromDeployment, name => 
                          File.Exists(Path.GetFullPath(Path.Combine(deploymentdir, name))))){

        return Path.GetFullPath(Path.Combine(deploymentdir, "Foxtrot.exe"));
      } else {
        return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Foxtrot.exe"));
        //return Path.GetFullPath(Path.Combine(RootDirectory, @"Foxtrot\Driver\bin\Debug\Foxtrot.exe"));
      }
    }

#if false
    public RewriteCodeUnderTestBeforeRunningUnitTests() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
#endif
  }

  [CollectionDefinition("unittests")]
  public class AssemblyCollection : ICollectionFixture<RewriteCodeUnderTestBeforeRunningUnitTests>
  {
  }

  [Collection("unittests")]
  public class DisableAssertUI {
    static DisableAssertUI() {
      for (int i = 0; i < System.Diagnostics.Debug.Listeners.Count; i++) {
        System.Diagnostics.DefaultTraceListener dtl = System.Diagnostics.Debug.Listeners[i] as System.Diagnostics.DefaultTraceListener;
        if (dtl != null) dtl.AssertUiEnabled = false;
      }
    }
  }

  public class RewrittenContractTest : DisableAssertUI {
    #region Tests

    public RewrittenContractTest() {
      TestRewriterMethods.FailureCount = 0;
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveAssertTest() {
      new CodeUnderTest.RewrittenContractTest().AssertsTrue(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAssertTest() {
      try {
        new CodeUnderTest.RewrittenContractTest().AssertsTrue(false);
        throw new Exception();
      } catch (TestRewriterMethods.AssertException e) {
        Assert.Equal("mustBeTrue", e.Condition);
      }
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveAssumeTest() {
      new CodeUnderTest.RewrittenContractTest().AssumesTrue(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAssumeTest() {
      try {
        new CodeUnderTest.RewrittenContractTest().AssumesTrue(false);
        throw new Exception();
      } catch (TestRewriterMethods.AssumeException e) {
        Assert.Equal("mustBeTrue", e.Condition);
      }
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveRequiresTest() {
      new CodeUnderTest.RewrittenContractTest().RequiresTrue(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeRequiresTest() {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new CodeUnderTest.RewrittenContractTest().RequiresTrue(false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveRequiresWithExceptionTest() {
      new CodeUnderTest.RewrittenContractTest().RequiresTrue<ArgumentException>(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeRequiresWithExceptionTest() {
      Assert.Throws<ArgumentException>(() => new CodeUnderTest.RewrittenContractTest().RequiresTrue<ArgumentException>(false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeRequiresWithExceptionTest2() {
      try {
        new CodeUnderTest.RewrittenContractTest().RequiresTrue<ArgumentException>(false);
      } catch (ArgumentException e) {
        Assert.Equal("Precondition failed: mustBeTrue", e.Message, true);
      }
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositivePrivateRequiresTest() {
      new CodeUnderTest.RewrittenContractTest().CallPrivateRequiresTrue(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    
    [Fact(Skip = "This test should be ignored, because the whole test suite is running without /publicsurface flag!")]
    [Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositivePrivateRequiresTest2() {
      // Does not trigger due to /publicsurface
      new CodeUnderTest.RewrittenContractTest().CallPrivateRequiresTrue(false);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEnsuresTest() {
      new CodeUnderTest.RewrittenContractTest().EnsuresTrue(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeEnsuresTest() {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new CodeUnderTest.RewrittenContractTest().EnsuresTrue(false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositivePrivateEnsuresTest() {
      new CodeUnderTest.RewrittenContractTest().CallPrivateEnsuresTrue(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact(Skip = "This test should be ignored, because the whole test suite is running without /publicsurface flag!")]
    [Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositivePrivateEnsuresTest2() {
      // Does not trigger due to /publicsurface
      new CodeUnderTest.RewrittenContractTest().CallPrivateEnsuresTrue(false);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEnsureReturnTest() {
      new CodeUnderTest.RewrittenContractTest().EnsuresReturnsTrue(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeEnsuresReturnTest() {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new CodeUnderTest.RewrittenContractTest().EnsuresReturnsTrue(false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEnsuresAnythingTest() {
      new CodeUnderTest.RewrittenContractTest().EnsuresAnything(true);
      new CodeUnderTest.RewrittenContractTest().EnsuresAnything(false);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveRequiresAndEnsuresTest() {
      new CodeUnderTest.RewrittenContractTest().RequiresAndEnsuresTrue(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeRequiresAndEnsuresTest() {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new CodeUnderTest.RewrittenContractTest().RequiresAndEnsuresTrue(false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveInvariantTest() {
      new CodeUnderTest.RewrittenContractTest().MakeInvariantTrue();
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeInvariantTest() {
      Assert.Throws<TestRewriterMethods.InvariantException>(() => new CodeUnderTest.RewrittenContractTest().MakeInvariantFalse());
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEnsureResultTest() {
      bool output;
      new CodeUnderTest.RewrittenContractTest().EnsuresSetsTrue(true, out output);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeEnsureResultTest() {
      bool output;
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new CodeUnderTest.RewrittenContractTest().EnsuresSetsTrue(false, out output));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveOldTest() {
      bool anything = true;
      Assert.Equal(true, new CodeUnderTest.RewrittenContractTest().SetsAndReturnsOld(ref anything));
      Assert.False(anything);
      Assert.Equal(false, new CodeUnderTest.RewrittenContractTest().SetsAndReturnsOld(ref anything));
      Assert.True(anything);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveThrowTest() {
      new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(0, false);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveThrowTest1() {
      Assert.Throws<ApplicationException>(() => new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(1, true));
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveThrowTest2() {
      Assert.Throws<SystemException>(() => new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(2, true));
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    //[ExpectedException(typeof(Exception))] VS Unit Tests can't handle this.
    public void PositiveThrowTest3() {
      try {
        new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(3, true);
      } catch (Exception e) {
        Assert.True(e.GetType() == typeof(Exception));
        Assert.Equal(0, TestRewriterMethods.FailureCount);
        return;
      }
      Assert.True(false);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeThrowTest0() {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(0, true));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeThrowTest1() {
      Assert.Throws<TestRewriterMethods.PostconditionOnThrowException>(() => new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(1, false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeThrowTest2() {
      Assert.Throws<TestRewriterMethods.PostconditionOnThrowException>(() => new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(2, false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeThrowTest3() {
      Assert.Throws<TestRewriterMethods.PostconditionOnThrowException>(() => new CodeUnderTest.RewrittenContractTest().ThrowsEnsuresTrue(3, false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveRequiresForAll() {
      int[] A = new int[] { 3, 4, 5 };
      new CodeUnderTest.RewrittenContractTest().RequiresForAll(A);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeRequiresForAll() {
      int[] A = new int[] { 3, -4, 5 };
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new CodeUnderTest.RewrittenContractTest().RequiresForAll(A));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveRequiresExists() {
      int[] A = new int[] { -3, -4, 5 };
      new CodeUnderTest.RewrittenContractTest().RequiresExists(A);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeRequiresExists() {
      int[] A = new int[] { -3, -4, -5 };
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new CodeUnderTest.RewrittenContractTest().RequiresExists(A));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEnsuresForAll() {
      int[] A = new int[] { 3, 4, 5 };
      int[] B = new CodeUnderTest.RewrittenContractTest().EnsuresForAll(A, true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeEnsuresForAll() {
      int[] A = new int[] { 3, -4, 5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(
        () =>
        {
          int[] B = new CodeUnderTest.RewrittenContractTest().EnsuresForAll(A, false);
        });
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEnsuresExists() {
      int[] A = new int[] { -3, -4, 5 };
      int[] B = new CodeUnderTest.RewrittenContractTest().EnsuresExists(A, true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeEnsuresExists() {
      int[] A = new int[] { -3, -4, -5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(
        () =>
        {
          int[] B = new CodeUnderTest.RewrittenContractTest().EnsuresExists(A, false);
        });
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }


    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEnsuresForAllWithOld() {
      int[] A = new int[] { 3, 4, 5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresForAllWithOld(A, true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeEnsuresForAllWithOld() {
      int[] A = new int[] { 3, -4, 5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new CodeUnderTest.RewrittenContractTest().EnsuresForAllWithOld(A, false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEnsuresDoubleClosure() {
      int[] A = new int[] { 3, 4, 5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresDoubleClosure(A, A, true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeEnsuresDoubleClosure() {
      int[] A = new int[] { 3, 4, 5 };
      int[] B = new int[] { 3, 4 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new CodeUnderTest.RewrittenContractTest().EnsuresDoubleClosure(A, B, false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEnsuresExistsWithOld() {
      int[] A = new int[] { -3, -4, 5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresExistsWithOld(A, true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeEnsuresExistsWithOld() {
      int[] A = new int[] { -3, -4, -5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new CodeUnderTest.RewrittenContractTest().EnsuresExistsWithOld(A, false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEnsuresWithResultAndQuantifiers() {
      int[] A = new int[] { -3, -4, -5 };
      new CodeUnderTest.RewrittenContractTest().EnsuresWithResultAndQuantifiers(A, true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeEnsuresWithResultAndQuantifiers() {
      int[] A = new int[] { -3, -4, -5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new CodeUnderTest.RewrittenContractTest().EnsuresWithResultAndQuantifiers(A, false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }
#if TURNINGSTATICCLOSUREINTOOBJECTCLOSURE
        [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
        public void PositiveEnsuresWithStaticClosure()
        {
          EnsuresWithStaticClosure(true);
          Assert.Equal(0, TestRewriterMethods.FailureCount);
        }
        [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
        public void NegativeEnsuresWithStaticClosure()
        {
          Assert.Throws<TestRewriterMethods.PostconditionException>(() => EnsuresWithStaticClosure(false));
          Assert.Equal(1, TestRewriterMethods.FailureCount);
        }
#endif

    #region IfThenThrow
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveIfThenThrowAsPrecondition() {
      new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeIfThenThrowAsPrecondition() {
      Assert.Throws<ArgumentException>(() => new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition(false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveIfThenThrowAsPrecondition2() {
      new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition2(1);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeIfThenThrowAsPrecondition2() {
      Assert.Throws<ArgumentOutOfRangeException>(() => new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition2(-1));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveIfThenThrowAsPrecondition3() {
      new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition3(3);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeIfThenThrowAsPrecondition3a() {
      Assert.Throws<ArgumentOutOfRangeException>(() => new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition3(-1));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeIfThenThrowAsPrecondition3b() {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new CodeUnderTest.RewrittenContractTest().IfThenThrowAsPrecondition3(2));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    #endregion IfThenThrow

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveUseAParamsMethodInContract() {
      new CodeUnderTest.RewrittenContractTest().UseAParamsMethodInContract(3);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeUseAParamsMethodInContract() {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new CodeUnderTest.RewrittenContractTest().UseAParamsMethodInContract(5));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveAssertArgumentIsPositive() {
      new CodeUnderTest.RewrittenContractTest().AssertArgumentIsPositive(3);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAssertArgumentIsPositive() {
      try {
        new CodeUnderTest.RewrittenContractTest().AssertArgumentIsPositive(-3);
        throw new Exception();
      } catch (TestRewriterMethods.AssertException e) {
        Assert.Equal("0 < y", e.Condition);
      }
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveOldExpressionWithoutGoodNameToUseForLocal() {
      int a = 3;
      int b = 5;
      new CodeUnderTest.RewrittenContractTest().Swap(ref a, ref b, true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeOldExpressionWithoutGoodNameToUseForLocal1() {
      int a = 3;
      int b = 3;
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new CodeUnderTest.RewrittenContractTest().Swap(ref a, ref b, true));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeOldExpressionWithoutGoodNameToUseForLocal2() {
      int a = 3;
      int b = 5;
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new CodeUnderTest.RewrittenContractTest().Swap(ref a, ref b, false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositivePostConditionUsingResultAsCallee() {
      new CodeUnderTest.RewrittenContractTest().PostConditionUsingResultAsCallee(true);
      Assert.Equal(0, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativePostConditionUsingResultAsCallee() {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new CodeUnderTest.RewrittenContractTest().PostConditionUsingResultAsCallee(false));
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeRequiresExceptionTest1() {
      try {
        new CodeUnderTest.RewrittenContractTest().RequiresArgumentNullException(null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentNullException a) {
        Assert.Equal("parameter", a.ParamName);
      }
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeRequiresExceptionTest2() {
      try {
        new CodeUnderTest.RewrittenContractTest().RequiresArgumentOutOfRangeException(-1);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentOutOfRangeException a) {
        Assert.Equal("parameter", a.ParamName);
      }
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeRequiresExceptionTest3() {
      try {
        new CodeUnderTest.RewrittenContractTest().RequiresArgumentException(0, 1);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        //Assert.Equal(null, a.ParamName);
        Assert.Equal("Precondition failed: x >= y: x must be at least y\r\nParameter name: x must be at least y", a.Message);
      }
      Assert.Equal(1, TestRewriterMethods.FailureCount);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveTestForMemberVisibility1() {
      CodeUnderTest.RewrittenContractTest.TestForMemberVisibility1(3); // call is not really needed since the test is to make sure the rewriter doesn't crash.
      Assert.Equal(0, TestRewriterMethods.FailureCount);
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
  public class ReEntrantInvariantTest : DisableAssertUI {
    #region Test Management
    public ReEntrantInvariantTest() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void TestReenterInvariant() {
      var c = new CodeUnderTest.ReEntrantInvariantTest.ClassWithInvariant();
      c.ReEnter();
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void FailInvariant1() {
      var c = new CodeUnderTest.ReEntrantInvariantTest.ClassWithInvariant();
      try {
        c.FailInvariant1();
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.InvariantException e) {
        Assert.Equal("b", e.Condition);
      }
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void FailInvariant2() {
      var c = new CodeUnderTest.ReEntrantInvariantTest.ClassWithInvariant();
      try {
        c.FailInvariant2();
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.InvariantException e) {
        Assert.Equal("PureMethod()", e.Condition);
      }
    }
    #endregion Tests
  }

  /// <summary>
  /// Test to make sure that an invariant in a class does not get
  /// "inherited" by any nested classes.
  /// </summary>
  public class ClassWithInvariantAndNestedClass : DisableAssertUI {
    #region Test Management
    public ClassWithInvariantAndNestedClass() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void ReEnterInvariant() {
      // Don't need to call another method because this method
      // must be public and so gets rewritten to call the invariant.
    }

    class Inner { }

    #endregion Tests
  }

  public class LocalsOfStructTypeEndingUpInAssignments : DisableAssertUI {
    #region Test Management
    public LocalsOfStructTypeEndingUpInAssignments() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveLocalOfStructType() {
      var l = new CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.MyCollection<int, CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.Positive>();
      l.Add(5);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeLocalOfStructType1() {
      var l = new CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.MyCollection<int, CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.Positive>();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => l.Add(0));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeLocalOfStructType2() {
      var l = new CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.MyCollection<int, CodeUnderTest.LocalsOfStructTypeEndingUpInAssignments.Positive>();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => l.Pull());
    }
    #endregion Tests
  }

  public class LegacyPreconditions : DisableAssertUI {
    #region Test Management
    public LegacyPreconditions() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveLegacyRequiresDirectThrow() {
      var c = new CodeUnderTest.LegacyPreconditions.C();
      c.DirectThrow(3);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeLegacyRequiresDirectThrow() {
      var c = new CodeUnderTest.LegacyPreconditions.C();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => c.DirectThrow(-3));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveLegacyRequiresIndirectThrow() {
      var c = new CodeUnderTest.LegacyPreconditions.C();
      c.IndirectThrow(3);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeLegacyRequiresIndirectThrow() {
      var c = new CodeUnderTest.LegacyPreconditions.C();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => c.IndirectThrow(-3));
    }
    #endregion Tests
  }

  public class GenericClassWithDeferringCtor :DisableAssertUI {
    #region Test Management
    public GenericClassWithDeferringCtor() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveGenericClassWithDeferringCtor() {
      var c = new CodeUnderTest.GenericClassWithDeferringCtor.C<int>('a');
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeGenericClassWithDeferringCtor() {
      Assert.Throws<TestRewriterMethods.PreconditionException>(
        () =>
        {
          var c = new CodeUnderTest.GenericClassWithDeferringCtor.C<int>('z');
        });
    }
    #endregion Tests
  }

  public class IfaceContractUsingLocalToHoldSelf : DisableAssertUI {
    #region Test Management
    public IfaceContractUsingLocalToHoldSelf() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveIfaceContractUsingLocalToHoldSelf() {
      CodeUnderTest.IfaceContractUsingLocalToHoldSelf.J j = new CodeUnderTest.IfaceContractUsingLocalToHoldSelf.B();
      int[] A = new int[] { 3, 4, 5 };
      j.M(A, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeIfaceContractUsingLocalToHoldSelf1() {
      CodeUnderTest.IfaceContractUsingLocalToHoldSelf.J j = new CodeUnderTest.IfaceContractUsingLocalToHoldSelf.B();
      int[] A = new int[] { 3, 0, 5 };
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => j.M(A, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeIfaceContractUsingLocalToHoldSelf2() {
      CodeUnderTest.IfaceContractUsingLocalToHoldSelf.J j = new CodeUnderTest.IfaceContractUsingLocalToHoldSelf.B();
      int[] A = new int[] { 3, 4, 5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.M(A, false));
    }
    #endregion Tests
  }

  public class EvaluateOldExpressionsAfterPreconditions :DisableAssertUI {
    #region Test Management
    public EvaluateOldExpressionsAfterPreconditions() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEvaluateOldExpressionsAfterPreconditions() {
      var c = new CodeUnderTest.EvaluateOldExpressionsAfterPreconditions.C();
      int[] A = new int[] { 3, 4, 5 };
      c.M(A);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeEvaluateOldExpressionsAfterPreconditions() {
      var c = new CodeUnderTest.EvaluateOldExpressionsAfterPreconditions.C();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => c.M(null)); // if the old value for xs.Length is evaluated before the precondition this results in a null dereference
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEvaluateFailingOldExpression1() {
      var c = new CodeUnderTest.EvaluateOldExpressionsAfterPreconditions.C();
      c.ConditionalOld(null);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveEvaluateFailingOldExpression2() {
      var c = new CodeUnderTest.EvaluateOldExpressionsAfterPreconditions.C();
      c.ConditionalOld(new int[] { 3, 4 });
    }
    #endregion Tests
  }

  public class PostConditionsInStructCtorsMentioningFields : DisableAssertUI {
    #region Test Management
    public PostConditionsInStructCtorsMentioningFields() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveFieldsInStructCtorEnsures() {
      var s = new CodeUnderTest.PostConditionsInStructCtorsMentioningFields.S(3, 4, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeFieldsInStructCtorEnsures() {
      Assert.Throws<TestRewriterMethods.PostconditionException>(
        () =>
        {
          var s = new CodeUnderTest.PostConditionsInStructCtorsMentioningFields.S(3, 4, false);
        });
    }
    #endregion Tests
  }

  public class PrivateInvariantInSealedType : DisableAssertUI {
    #region Test Management
    public PrivateInvariantInSealedType() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositivePrivateInvariantMethod() {
      var t = new CodeUnderTest.PrivateInvariantInSealedType.T(3, 4, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativePrivateInvariantMethod() {
      Assert.Throws<TestRewriterMethods.InvariantException>(
        () =>
        {
          var t = new CodeUnderTest.PrivateInvariantInSealedType.T(3, 4, false);
        });
    }
    #endregion Tests
  }

  public class ConstructorsWithClosures : DisableAssertUI {
    #region Test Management
    public ConstructorsWithClosures() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveCtorWithClosure1() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping("hello");
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveCtorWithClosure2() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(new Object());
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveCtorWithClosure3() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping("hello", true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveCtorWithClosure4() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(new object(), false);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveCtorWithClosure5() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping("Hello", new[] { "Foo", "Bar", "Hello" });
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveCtorWithClosure6() {
      var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping("Hello", "Hello");
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeCtorWithClosure1() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.Equal("Precondition failed: encoder != null", a.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeCtorWithClosure2() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(null, false);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.Equal("Precondition failed: encoder != null", a.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeCtorWithClosure3() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(null, (string[])null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.Equal("Precondition failed: encoder != null", a.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeCtorWithClosure4() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping("Hello", new[] { "A", "b", "D" });
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException a) {
        Assert.Equal(": Contract.Exists(0, data.Length, i => data[i] == this.name)", a.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeCtorWithClosure5() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.ValueMapping(null, (string)null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.Equal("Precondition failed: encoder != null", a.Message);
      }
    }

    /// <summary>
    /// Structs
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveStructCtorWithClosure1() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors("hello");
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveStructCtorWithClosure2() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(new Object());
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveStructCtorWithClosure3() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors("hello", true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveStructCtorWithClosure4() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(new object(), false);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveStructCtorWithClosure5() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors("Hello", new[] { "Foo", "Bar", "Hello" });
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveStructCtorWithClosure6() {
      var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors("Hello", "Hello");
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeStructCtorWithClosure1() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.Equal("Precondition failed: encoder != null", a.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeStructCtorWithClosure2() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(null, false);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.Equal("Precondition failed: encoder != null", a.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeStructCtorWithClosure3() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(null, (string[])null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.Equal("Precondition failed: encoder != null", a.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeStructCtorWithClosure4() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors("Hello", new[] { "A", "b", "D" });
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException a) {
        Assert.Equal(": Contract.Exists(0, data.Length, i => data[i] == (encoder as string))", a.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeStructCtorWithClosure5() {
      try {
        var c = new CodeUnderTest.ConstructorsWithClosures.StructCtors(null, (string)null);
        throw new Exception("Expected failure did not happen");
      } catch (ArgumentException a) {
        Assert.Equal("Precondition failed: encoder != null", a.Message);
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
  public class IteratorSimpleContract : DisableAssertUI {
    #region Test Drivers
    #region Non-generic class drivers
    /// <summary>
    /// This driver passes the preconditions of Test1a.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorPositiveNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      test2.Test1a("hello"); // Precondition of Test1a holds
    }

    /// <summary>
    /// This driver violates the precondition of Test1a.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorNegativeNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test2.Test1a(null)); // Precondition of Test1a violated.
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1aa.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorNegativePostAllNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => test2.Test1aa("hello")); // Postcondition of Test1aa is invalid. 
    }

    /// <summary>
    /// This driver tests a legacy precondition
    /// Legacy contract throws an exception when violated (and is caught by the driver).
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorNegativeLegacyArgNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      try
      {
        test2.Test1b(null);
      } catch (ArgumentException) {
        // An ArgumentException is thrown by the legacy precondition. 
      }
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorPositiveLegacyArgNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      test2.Test1b("hello");
    }

    /// <summary>
    /// This driver fails the first precondition of Test1d.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorNegativeCollectionNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      IEnumerable<string> x = null;
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test2.Test1d(x));
    }

    /// <summary>
    /// This driver fails the second precondition of Test1d.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorNegativeElemNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      string[] strs = { null };
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test2.Test1d(strs));
    }

    /// <summary>
    /// This driver passes the preconditions of Test1d.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorPositivePostElemNN() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      string[] strs = { "hello" };
      test2.Test1d(strs);
    }

    /// <summary>
    /// This driver passes preconditions of Test1f.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorPositiveElemLTMax() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      int[] xs = { 2, 3, 4, 8 };
      test2.Test1f(xs, 10);
    }

    /// <summary>
    /// This driver fails the precondition of Test1f.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorNegativeElemLTMax() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      int[] xs = { 2, 3, 4, 11 };
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test2.Test1f(xs, 10));
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1f.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorNegativePostElemLTMax() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      int[] xs = { 2, 3, 4, 9 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => test2.Test1f(xs, 10));
    }

    /// <summary>
    /// This driver passes Test1g.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorPositivePostElemEqParam2() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      string[] strs = { "hello", "hello" };
      test2.Test1g(strs, "hello");
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorNegativePostElemEqParam2() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      string[] strs = { "aaa", "hello" };
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test2.Test1g(strs, "hello"));
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1h. 
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorNegativePostElemLTMaxConstrained() {
      var test2 = new CodeUnderTest.IteratorSimpleContract.Test2();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => test2.Test2h());
    }
    #endregion NonGeneric Class Drivers
    #region Generic Class Drivers
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsPosNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      test3.Test1a(new CodeUnderTest.IteratorSimpleContract.A("hello"));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsNegNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test3.Test1a(null));
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1aa.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsNegPostAllNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => test3.Test1aa(new CodeUnderTest.IteratorSimpleContract.A("")));
    }

    /// <summary>
    /// This driver tests a legacy precondition
    /// Legacy contract throws an exception when violated (and is caught by the driver).
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsNegLegacyArgNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      try
      {
        test3.Test1b(null);
      } catch (ArgumentException) {
        // An ArgumentException is thrown by the legacy precondition. 
      }
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsPosLegacyArgNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      test3.Test1b(new CodeUnderTest.IteratorSimpleContract.A("hello"));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsMethNegCollectionNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      IEnumerable<CodeUnderTest.IteratorSimpleContract.A> x = null;
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test3.Test1d(x));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsMethNegElemNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      CodeUnderTest.IteratorSimpleContract.A[] aa = { null };
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test3.Test1d(aa));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsMethPosPostElemNN() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A[] strs = { new CodeUnderTest.IteratorSimpleContract.A("hello") };
      test3.Test1d(strs);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsMethPosElemMethEqParamMeth() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("54321") };
      test3.Test1e(aa, a);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsMethNegElemMethEqParamMeth() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("5432") };
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test3.Test1e(aa, a));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsMethInsClPosElemEqParam() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("54321") };
      test3.Test1g(aa, a);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsMethInsClNegElemEqParam() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("321") };
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test3.Test1g(aa, a));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsMethInsClNegPostElemLTProp() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("45"), new CodeUnderTest.IteratorSimpleContract.A("321") };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => test3.Test1h(aa));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenClsMethPostElemLTProp() {
      var test3 = new CodeUnderTest.IteratorSimpleContract.Test3<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("1234567"), new CodeUnderTest.IteratorSimpleContract.A("213456") };
      test3.Test1h(aa);
    }
    #endregion Generic Class Drivers
    #region Generic Struct Drivers
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenStrPosNN() {
      var test4 = new CodeUnderTest.IteratorSimpleContract.Test4<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      test4.Test1a(new CodeUnderTest.IteratorSimpleContract.A("hello"));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenStrNegNN() {
      var test4 = new CodeUnderTest.IteratorSimpleContract.Test4<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test4.Test1a(null));
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1aa.
    /// </summary>
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenStrNegPostAllNN() {
      var test4 = new CodeUnderTest.IteratorSimpleContract.Test4<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A("hello"));
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => test4.Test1aa(new CodeUnderTest.IteratorSimpleContract.A("")));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenStrMethPosElemMethEqParamMeth() {
      var test4 = new CodeUnderTest.IteratorSimpleContract.Test4<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("54321") };
      test4.Test1e(aa, a);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IteratorGenStrMethNegElemMethEqParamMeth() {
      var test4 = new CodeUnderTest.IteratorSimpleContract.Test4<CodeUnderTest.IteratorSimpleContract.A>(new CodeUnderTest.IteratorSimpleContract.A(null));
      CodeUnderTest.IteratorSimpleContract.A a = new CodeUnderTest.IteratorSimpleContract.A("hello");
      CodeUnderTest.IteratorSimpleContract.A[] aa = { new CodeUnderTest.IteratorSimpleContract.A("12345"), new CodeUnderTest.IteratorSimpleContract.A("5432") };
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => test4.Test1e(aa, a));
    }

    #endregion Generic Struct Drivers
    #region IEnumerator Drivers
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IEnumeratorPos() {
      new CodeUnderTest.IteratorSimpleContract().Test5a(2);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void IEnumeratorNeg() {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new CodeUnderTest.IteratorSimpleContract().Test5a(0));
    }
    #endregion
    #endregion Test Drivers
  }

  public class LocalVariableForResult : DisableAssertUI {
    #region Test Management
    public LocalVariableForResult() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveLocalAsResult1() {
      var c = new CodeUnderTest.LocalVariableForResult.C();
      c.Test1(1);
    }
    public void PositiveLocalAsResult2() {
      var c = new CodeUnderTest.LocalVariableForResult.C();
      c.Test2(1, new bool[] { });
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveLocalAsResult3() {
      var c = new CodeUnderTest.LocalVariableForResult.C();
      c.Test2(1, new bool[] { true, true, true });
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeLocalAsResult1() {
      try {
        var c = new CodeUnderTest.LocalVariableForResult.C();
        c.Test1(2);
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException p) {
        Assert.Equal(": result != false", p.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeLocalAsResult2() {
      try {
        var c = new CodeUnderTest.LocalVariableForResult.C();
        c.Test2(2, new bool[] { });
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException p) {
        Assert.Equal(": result != false", p.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeLocalAsResult3() {
      try {
        var c = new CodeUnderTest.LocalVariableForResult.C();
        c.Test2(1, new bool[] { true, false });
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException p) {
        Assert.Equal(": Contract.ForAll(0, arr.Length, i => arr[i] == result)", p.Message);
      }
    }

    #endregion Tests
  }

  public class IgnoreRuntimeAttributeTest : DisableAssertUI {
    #region Test Management
    public IgnoreRuntimeAttributeTest() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveIgnoreRuntimeAttributeTest() {
      var c = new CodeUnderTest.IgnoreRuntimeAttributeTest.C();
      c.M(3, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeIgnoreRuntimeAttributeTest() {
      try {
        var c = new CodeUnderTest.IgnoreRuntimeAttributeTest.C();
        c.M(2, false);
        throw new Exception("Expected failure did not happen");
      } catch (TestRewriterMethods.PostconditionException p) {
        Assert.Equal(": Contract.Result<bool>()", p.Message);
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
  public class WrapParametersInOld : DisableAssertUI {
    #region Test Management
    public WrapParametersInOld() {
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
      Assert.True(found, "This assembly must have been rewritten before running these tests.");
    }
    #endregion Test Management
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveWrapParam1() {
      var c = new CodeUnderTest.WrapParametersInOld.C(3);
      c.Identity(true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveWrapParam2() {
      var c = new CodeUnderTest.WrapParametersInOld.C(3);
      c.Identity(false);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveWrapParam3() {
      var c = new CodeUnderTest.WrapParametersInOld.C(3);
      var s = new CodeUnderTest.WrapParametersInOld.C.S();
      s.x = 3;
      c.F(s);
    }
    #endregion Tests
  }
}