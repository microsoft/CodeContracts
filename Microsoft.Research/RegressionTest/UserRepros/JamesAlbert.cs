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

namespace ConditionsValHashidity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
	using Microsoft.Research.ClousotRegression;

    /// <summary>
    /// Demonstrates four different cases where the "-check conditionsvalidity"
    /// experimental feature of the static checker appears to return a false positive.
    /// </summary>
    /// <remarks>Obfuscated from production-level code by James D. Albert (jalbert@costar.com).</remarks>
    public static class Class1
    {
		[ClousotRegressionTest]
        public static void Example1(params byte[][] sources)
        {
            Contract.Requires(sources != null);

            // #1: "The condition always evaluates to a constant Boolean value"
            var sum = sources.Sum(buffer => buffer.Length);
            Debug.Print("Sum = {0}", sum);
        }

		[ClousotRegressionTest]
        public static byte[] Example2()
        {
            using (var stream = new MemoryStream())
            {
                // #2: "The condition always evaluates to a constant Boolean value"
                return stream.ToArray();
            }
        }

		[ClousotRegressionTest]
        public static void Example3()
        {
            // #3: "The condition always evaluates to a constant Boolean value"
            foreach (var b in GenerateBytes())
            {
                Debug.Print("{0:x2}", b);
            }
        }
		
		/*
		[ClousotRegressionTest]
        public static Func<HttpClient, Task<HttpResponseMessage>> Example4(Uri requestUri)
        {
            if (DateTimeOffset.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                // #4: "The condition always evaluates to a constant Boolean value"
                return httpClient => httpClient.GetAsync(requestUri);
            }

            throw new InvalidOperationException();
        }
		*/
		
		[ClousotRegressionTest]
		[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition x >= y always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=11,MethodILOffset=0)]
		public static int AlawaysTrue(int x, int y)
        {
          Contract.Requires(x < y);

          if (x >= y)
            return 2;

          return 3;
        }

		[ClousotRegressionTest]
        private static IEnumerable<byte> GenerateBytes()
        {
            return new byte[]
            {
            };
        }
    }
}

namespace ReprosFromDomino
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Diagnostics.Contracts;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml.Linq;
	using Microsoft.Research.ClousotRegression;

  public class ReproWithLong
  {
    public int m_numPipsFailed;
    public int m_numPipsSucceeded;
    public int m_numPipsWaitingForInput;

	[ClousotRegressionTest]
    public void Foo(int total)
    {

      int numDigits = 0;
      int num = total;

      while (num > 0)
      {
         num /= 10;
        //num -= 10;
        numDigits++;
      }
    
	// No warning herE: we had a bug in the handling of divion with intervals
      if (numDigits == 0)
      {
        numDigits++;
      }
        
    }

    [ContractVerification(false)]
    public void GetQueuedAndRunning(out int a1, out int a2)
    {
//      Contract.Ensures(Contract.ValueAtReturn<long>(out a1) >= 0);
//      Contract.Ensures(Contract.ValueAtReturn<long>(out a2) >= 0);

      a1 = a2 = 0;
    }
  }

  public class CheckConditionsValidity
  {
    public string instanceField;
    public static string staticField;

    // No warn
	[ClousotRegressionTest]
    public int ShouldBeFiltered(int z)
    {
      if (staticField == null)
      {
        Console.WriteLine("Hello!");
      }

      return z + 1;
    }

    // We should give a warning
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition this.instanceField == null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=23,MethodILOffset=0)]
    public int ShouldWarn()
    {
      Contract.Requires(this.instanceField != null);

      if (this.instanceField == null)
      {
        Console.WriteLine("Dead code!!!");
      }

      return 0;

    }

    // We should give a warning
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition argStart <= method.Length always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=32,MethodILOffset=0)]
    public static string GetIdentifier(string method)
    {
      Contract.Requires(method != null);

      int argStart = method.IndexOf('(');
      if (argStart > 0 && argStart <= method.Length)
      {
        return method.Substring(0, argStart);
      }

      return method;
    }

    // We should give a warning
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: !String.IsNullOrEmpty(uri)",PrimaryILOffset=15,MethodILOffset=19)]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition System.Xml.Linq.XDocument.Load(project) == null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=26,MethodILOffset=0)]
    internal static bool TryGetDominoSpecFileFromProject(string project, out string specFile, out string dominoValue)
    {
      Contract.Assume(project != null);
      specFile = null;
      dominoValue = null;

      var document = XDocument.Load(project);
      if (document == null)
      {
        throw new Exception();
      }

      return true;
    }

    // No warn
	[ClousotRegressionTest]
    public static Task<DirectoryInfo> CreateDirectoryAsync(string path)
    {
      return HandleRecoverableIOException(
          () => new Task<DirectoryInfo>(() => Directory.CreateDirectory(path)),
          // () => Task.FromResult(Directory.CreateDirectory(path)),
          ex => { throw new Exception("Create directory failed", ex); });
    }


    [ContractVerification(false)]
    public static T HandleRecoverableIOException<T>(Func<T> func, Action<Exception> handler)
    {
      throw new Exception(); // Not implemented
    }
  }

  public class NMake
  {
    public IList<IMakefileElement> Elements { get; private set; }

	[ClousotRegressionTest]
    public bool DefinesValue(string macroName, string value)
    {
      Contract.Assume(this.Elements !=null);

      return
          Elements.Select(makefileElement => makefileElement as MacroAssignment)
              .Where(
                  ma =>
                      ma != null && ma.Name == macroName).Any();
    }
  }

  public unsafe struct ContentHash
  {
    public const int Length = 20;
    private fixed byte m_hashBytes[Length];

    // We emit a warning, but we should not...
	[ClousotRegressionTest]
//	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition digest.Length == 0 always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=55,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition digest.Length == 0 always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check. It may also be a byproduct of the compilation of, e.g., fixed. In this case add a SuppressMessage to shut down the warning",PrimaryILOffset=55,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition digest != null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check. It may also be a byproduct of the compilation of, e.g., fixed. In this case add a SuppressMessage to shut down the warning",PrimaryILOffset=50,MethodILOffset=0)]
    public ContentHash(byte[] digest)
    {
      Contract.Requires(digest != null);
      Contract.Requires(digest.Length == Length);

#if DEBUG
      Contract.Assert(Length == 20, "The implementation of the constructor must change if the hash size changes");
#endif

      fixed (byte* t = m_hashBytes, s = digest)
      {
        *((long*)&t[0]) = *((long*)&s[0]);
        *((long*)&t[8]) = *((long*)&s[8]);
        *((int*)&t[16]) = *((int*)&s[16]);
      }
    }

    // no warn -- but in this regression, for some reason the SuppressMessage does not work properly
    [SuppressMessage("Microsoft.Contracts", "UnreachedCode")]
	[ClousotRegressionTest]
//	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition digest.Length == 0 always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=55,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition digest.Length == 0 always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check. It may also be a byproduct of the compilation of, e.g., fixed. In this case add a SuppressMessage to shut down the warning",PrimaryILOffset=55,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition digest != null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check. It may also be a byproduct of the compilation of, e.g., fixed. In this case add a SuppressMessage to shut down the warning",PrimaryILOffset=50,MethodILOffset=0)]  
	public void ContentHash_Masked(byte[] digest)
    {
      Contract.Requires(digest != null);
      Contract.Requires(digest.Length == Length);

#if DEBUG
      Contract.Assert(Length == 20, "The implementation of the constructor must change if the hash size changes");
#endif

      fixed (byte* t = m_hashBytes, s = digest)
      {
        *((long*)&t[0]) = *((long*)&s[0]);
        *((long*)&t[8]) = *((long*)&s[8]);
        *((int*)&t[16]) = *((int*)&s[16]);
      }
    }

  }

  public class Arguments
  {
    [ContractVerification(false)]
    public Arguments(string[] args)
    {

    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: args != null (Argument args cannot be null)",PrimaryILOffset=12,MethodILOffset=1)]
    private static int Main(string[] args)
    {
      Arguments arguments = Arguments.Acquire(args);

      if (arguments == null)
      {
        return 1;
      }

      return 2;
    }

    public static Arguments Acquire(string[] args)
    {
      Contract.Requires(args != null, "Argument args cannot be null");

      try
      {
        return new Arguments(args);
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine(ex.Message);
        return null;
      }
    }
  }

  public sealed class DominoMapKeyValuePair<TKey, TValue> : IValue
  {
    public readonly TValue Value;
    public readonly TKey Key;

    // Can't repro at the beginiing because it is of an inherited contract...
    // Now we produce a false alarm because we cannot see that identified == null is not in the text
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition identifier != null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check. It may also been introduced by the C# compiler for a switch statement over strings. In this case add a SuppressMessage to shut down the warning",PrimaryILOffset=3,MethodILOffset=0)]
    public bool TryGetMember(string identifier, out object member)
    {
      switch (identifier)
      {
        case "Key":
          member = Key;
          return true;
        case "Value":
          member = Value;
          return true;
        default:
          member = null;
          return false;
      }
    }

    // ok
	[ClousotRegressionTest]
	//[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition enumText == null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=18,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition enumText == null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check. It may also been introduced by the C# compiler for a switch statement over strings. In this case add a SuppressMessage to shut down the warning",PrimaryILOffset=18,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition values.Length == 0 always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=56,MethodILOffset=0)]
    public bool TryGetEnumerationValue(string enumText, out int value)
    {
      Contract.Requires(!string.IsNullOrEmpty(enumText));

      // warn
      value = 0;
      if (enumText == null)
      {
        Contract.Assert(false, "Empty string is not a legal enumeration value.");
        return false;
      }

      // split on '|'
      string[] values = enumText.Split(new[] { '|' });

      // Warn
      if (values.Length == 0)
      {
        Contract.Assert(false, "Empty string is not a legal enumeration value.");
        return false;
      }

      return true;
    }
  }

  public class DirRepro
  {
    public string m_directory;
    public string m_ShouldBeNotNull;

    public string Directory
    {
      // Should get only one warning
      [ClousotRegressionTest]
      //[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition this.m_ShouldBeNotNull == null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=72,MethodILOffset=0)]
	  [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition this.m_ShouldBeNotNull == null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check. It may also been introduced by the C# compiler for a switch statement over strings. In this case add a SuppressMessage to shut down the warning",PrimaryILOffset=72,MethodILOffset=0)]
      get
      {
        Contract.Assume(m_ShouldBeNotNull != null);
        if (m_directory == null)
        {
          lock (this)
          {
            // No warning here
            if (m_directory == null) 
            {
              return m_directory;
            }
          }
        }

        // warning here
        if(m_ShouldBeNotNull == null) // Warn
        {
          return "<????>";
        }

        return m_directory;
      }
    }
  }

  public class EventRepro
  {
    private delegate void DataRead(string data);
    private static event DataRead OnDataRead;

    // We should filter it as it is static
	[ClousotRegressionTest]
    private static void Read(object parameter)
    {
      if(OnDataRead != null)
      {
        OnDataRead("ciao!!!");
      }
    }
  }


  // TODO TODO
  public class SetInDelegateRepro
  {
	[ClousotRegressionTest]
    public void F()
    {
      int x = 0;

      InvokeDelegate(() => { x = 12; });

      Contract.Assert(x == 0); // Should warn!!!
    }

    private void InvokeDelegate(Action a)
    {
      a();
    }
  }

  public class A
  {
    public PathAtom m_outputFileName;
    
    
    public PathAtom OutputFileName
    {
      [ContractVerification(false)]
      get
      {
        Contract.Ensures(Contract.Result<PathAtom>().IsValid);
        return m_outputFileName;
      }
    }
  }

  public class UserOfA
  {
    // should warn
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition a.OutputFileName.IsValid always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=23,MethodILOffset=0)]
	public void User(A a)
    {
      Contract.Requires(a != null);

      if(a.OutputFileName.IsValid)
      {
        Console.WriteLine("Not useful");
      }
    }
  }

  public abstract class PathAtom
  {
    abstract public bool IsValid { get; }
  }

  public interface IMakefileElement
  {
  }
  abstract public class MacroAssignment : IMakefileElement
  {

    /// <summary>
    /// Name of the macro.
    /// </summary>
    abstract public string Name
    {
      get;
    }
  }

  public interface IValue : IMemberable
  {
  }

  [ContractClass(typeof(MemberableContracts))]
  public interface IMemberable
  {
    /// <summary>
    /// Gets the value of the member associated with the identifier
    /// </summary>
    /// <param name="identifier">the member identifier</param>
    /// <param name="member">the value of the member, if any</param>
    /// <returns>true if the object has the specified member, false otherwise.</returns>
    bool TryGetMember(string identifier, out object member);
  }

  [ContractClassFor(typeof(IMemberable))]
  internal abstract class MemberableContracts : IMemberable
  {
    /// <inheritdoc />
    public bool TryGetMember(string identifier, out object member)
    {
      Contract.Requires(!string.IsNullOrEmpty(identifier));

      member = null;
      return false;
    }
  }
}

namespace RedundantCondition
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Linq;
	using Microsoft.Research.ClousotRegression;

	// Repro from Iman 
  class Program
  {
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition a.I >= 0 always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=14,MethodILOffset=0)]
    static void Main(string[] args)
    {
      var a = new A(5);

	  // We should say that this is always true
      if (a.I >= 0)
      {
        Console.WriteLine("OK");
      }
      else
      {
        Console.WriteLine("Not OK");
      }
    }
  }

  class A
  {
    private readonly int m_i;

    public A(int i)
    {
      Contract.Requires(i >= 0);
      m_i = i;
    }

    public int I
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return m_i;
      }
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(m_i >= 0);
    }
  }
  
  class DominoRepros
  {
    private readonly Queue<string> m_messageQueue = new Queue<string>();
    private bool m_cancelOperation;
    private Func<string, object> m_userCallBack;

    private int x;

	[ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this.m_messageQueue'",PrimaryILOffset=27,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition tmp > -12 always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=110,MethodILOffset=0)]
	private void FlushMessageQueue()
    {
	      Contract.Assume(x > 0);
      int tmp = x;

      while (true)
      {
        if (m_messageQueue.Count > 0)
        {
          lock (m_messageQueue)
          {
            if (m_messageQueue.Count > 0)// We do not show it because it is a field access inside a lock
            { 
              string s = m_messageQueue.Dequeue(); 
              if (!m_cancelOperation && m_userCallBack != null)
              {
                m_userCallBack(s);
              }
            }
			
			if(tmp > -12) // Should show this one, as it is on a local
            {
              Console.WriteLine("Always reachable");
              tmp++;
            }

          }
        }
        else
        {
          break;
        }
      }
    }

	[ClousotRegressionTest] 
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this.m_messageQueue'",PrimaryILOffset=6,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition this.m_messageQueue.Count > 0 always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=26,MethodILOffset=0)]
	private void FlushMessageQueue_WithoutLock()
    {
      while (true)
      {
        if (m_messageQueue.Count > 0)
        {
          // lock (m_messageQueue)
          {
            if (m_messageQueue.Count > 0) // we show it
            {
              string s = m_messageQueue.Dequeue();
              if (!m_cancelOperation && m_userCallBack != null)
              {
                m_userCallBack(s);
              }
            }
          }
        }
        else
        {
          break;
        }
      }
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: 0 <= length. Is it an off-by-one? The static checker can prove (0 - 1) <= splitIndex instead",PrimaryILOffset=31,MethodILOffset=16)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Is it an off-by-one? The static checker can prove splitIndex > (0 - 1) instead",PrimaryILOffset=26,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=51,MethodILOffset=0)]
    private void ReportLineReceived(string data)
    {
      if (data == null)
      {
        return; // EOF
      }

      int splitIndex = data.IndexOf(',');

      string reportTypeString = data.Substring(0, splitIndex);
      Contract.Assert(splitIndex > 0);


      data = data.Substring(splitIndex + 1);
      Contract.Assert(data.Length > 0);

    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition identifier != null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check. It may also been introduced by the C# compiler for a switch statement over strings. In this case add a SuppressMessage to shut down the warning",PrimaryILOffset=15,MethodILOffset=0)]
//	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition identifier != null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=15,MethodILOffset=0)]
    public bool TryGetMember(string identifier, out object member)
    {
      Contract.Assume(identifier != null);
      switch (identifier) // We should not emit the warning, but we have no way to discover that the IL originates from a switch statement
      {
        case "Key":
          member = "Key";
          return true;
        case "Value":
          member = "Value";
          return true;
        default:
          member = null;
          return false;
      }
    }
  }
  public class DisposableTest : IDisposable
  {

    public List<string> m_taskbarList;
 
	[ClousotRegressionTest]
	public void Dispose()
    {
      if (m_taskbarList != null)
      {
        var taskbarList = m_taskbarList;
        m_taskbarList = null;
        if (null != taskbarList) // Should be masked
        {
          m_taskbarList = null;
        }
      }
    }

  }

  public class DisposableTest_NonImplementingIDisposable
  {

    public List<string> m_taskbarList;

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="warning: The Boolean condition taskbarList != null always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check",PrimaryILOffset=23,MethodILOffset=0)]
    public void Dispose()
    {
      if (m_taskbarList != null)
      {
        var taskbarList = m_taskbarList;
        m_taskbarList = null;
        if (null != taskbarList) // Should be shown
        {
          m_taskbarList = null;
        }
      }
    }
  }
  
    public class PeterVilladsen
   {
	[ClousotRegressionTest] // We should not suggest to turn the Assert into an Assume
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=1,MethodILOffset=0)]
	public static void main(string[] _args)
    {
        System.Diagnostics.Contracts.Contract.Assert(1 > 2);
        System.Diagnostics.Debug.Assert(1 > 2);
    }
  }
  
}
