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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

[assembly:RegressionOutcome("Detected call to method 'System.Object.Equals(System.Object)' without [Pure] in contracts of method 'FixedExamples.SortAlgorithms.Swap(type parameter.T@,type parameter.T@)'.")]

namespace ExamplesFromPapers
{

  class Examples
  {
    public static bool NonDeterministic()
    {
      return System.DateTime.Now.Ticks % 2 == 0;
    }

    // from Sankaranarayanan, Ivancic and Gupta, SAS 07
    [ClousotRegressionTest("fast")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=28,MethodILOffset=0)]
    void Test1_Fast(int i, int j)
    {
      int x = i;
      int y = j;

      if (x <= 0)
        return;

      while (x > 0)
      {
        x--;
        y--;
      }

      if (y == 0)
        Contract.Assert(i == j);
    }

    // from Sankaranarayanan, Ivancic and Gupta, SAS 07
    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)28, MethodILOffset = 0)]
    void Test1_Simplex(int i, int j)
    {
      int x = i;
      int y = j;

      if (x <= 0)
        return;

      while (x > 0)
      {
        x--;
        y--;
      }

      if (y == 0)
        Contract.Assert(i == j);
    }

    // from Gulavani, Chakraborty, Nori and Rajamani, TACAS 08
    [ClousotRegressionTest("fast")]
    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)58, MethodILOffset = 0)]
    void Test2()
    {
      int x = 0;
      int y = 0;

      while (NonDeterministic())
      {
        if (NonDeterministic())
        {
          x++;
          y += 100;
        }
        else if (NonDeterministic())
        {
          if (x >= 4)
          {
            x++;
            y++;
          }
        }
      }
      if (x >= 4)
        Contract.Assert(y > 2);
    }

    // from Gulavani, Chakraborty, Nori and Rajamani, TACAS 08
    // reduction=Fast is briddle sometimes. This example exposes the problem.
    [ClousotRegressionTest("fast")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)90, MethodILOffset = 0)]
    //[RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 90, MethodILOffset = 0)]
    void Test3_Fast()
    {
      int x = 0;
      int y = 0;
      int w = 0;
      int z = 0;

      while (NonDeterministic())
      {
        if (NonDeterministic())
        {
          x++;
          y += 100;
        }
        else if (NonDeterministic())
        {
          if (x >= 4)
          {
            x++;
            y++;
          }
        }
        else if (y > 10 * w)
          if (z >= 100 * x)
          {
            y = -y;
          }
        w++;
        z += 10;
      }
      if (x >= 4)
        Contract.Assert(y > 2);
    }

    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=117,MethodILOffset=0)]
//    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=17,MethodILOffset=0)]
//    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=117,MethodILOffset=0)]
//    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 32, MethodILOffset = 0)]
// F: Fast reduction is no more interesting. Improving the join/widening precision caused less precision in fast, but we do not care about it anymore
// F: - update. by improving the clean up of betadependencies now this work
//    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
//    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 117, MethodILOffset = 0)]
    void Test3_FastNoOptionsWithInvariant()
    {
      int x = 0;
      int y = 0;
      int w = 0;
      int z = 0;

      while (NonDeterministic())
      {
        Contract.Assert(x <= y);
        Contract.Assert(y <= 100 * x);

        if (NonDeterministic())
        {
          x++;
          y += 100;
        }
        else if (NonDeterministic())
        {
          if (x >= 4)
          {
            x++;
            y++;
          }
        }
        else if (y > 10 * w)
          if (z >= 100 * x)
          {
            y = -y;
          }
        w++;
        z += 10;
      }
      if (x >= 4)
        Contract.Assert(y > 2);
    }


    [ClousotRegressionTest("fast_cci1")]
    // F: the -infoct switch generates too many hints which may confuse the fast reduction, and causes a loss of precision in the widening
    // There is also a mismatch 
    
    // TODO: Solve this issue with a mixed Simplex/Fast reduction
    //   [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 32, MethodILOffset = 0)]
    //[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=17,MethodILOffset=0)]
    //[RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 105, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=105,MethodILOffset=0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 32, MethodILOffset = 0)]
    void Test3_FastWithInvariant(bool b1, bool b2, bool b3)
    {
      int x = 0;
      int y = 0;
      int w = 0;
      int z = 0;

      while (b1)
      {
        Contract.Assert(x <= y);
        Contract.Assert(y <= 100 * x);
        
        if (b2)
        {
          x++;
          y += 100;
        }
        else if (b3)
        {
          if (x >= 4)
          {
            x++;
            y++;
          }
        }
        else if (y > 10 * w)
          if (z >= 100 * x)
          {
            y = -y;
          }
        w++;
        z += 10;
      }
      if (x >= 4)
        Contract.Assert(y > 2);
    }

    [ClousotRegressionTest("fast_cci2")]
    // F: the -infoct switch generates too many hints which may confuse the fast reduction, and causes a loss of precision in the widening
    // TODO: Solve this issue with a mixed Simplex/Fast reduction
    // [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=17,MethodILOffset=0)]
    // [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 105, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=105,MethodILOffset=0)]
    void Test3_FastWithInvariant_CCI2(bool b1, bool b2, bool b3)
    {
      int x = 0;
      int y = 0;
      int w = 0;
      int z = 0;

      while (b1)
      {
        Contract.Assert(x <= y);
        Contract.Assert(y <= 100 * x);

        if (b2)
        {
          x++;
          y += 100;
        }
        else if (b3)
        {
          if (x >= 4)
          {
            x++;
            y++;
          }
        }
        else if (y > 10 * w)
          if (z >= 100 * x)
          {
            y = -y;
          }
        w++;
        z += 10;
      }
      if (x >= 4)
        Contract.Assert(y > 2);
    }

    // from Gulavani, Chakraborty, Nori and Rajamani, TACAS 08
    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)90, MethodILOffset = 0)]
    void Test3_Simplex()
    {
      int x = 0;
      int y = 0;
      int w = 0;
      int z = 0;

      while (NonDeterministic())
      {
        if (NonDeterministic())
        {
          x++;
          y += 100;
        }
        else if (NonDeterministic())
        {
          if (x >= 4)
          {
            x++;
            y++;
          }
        }
        else if (y > 10 * w)
          if (z >= 100 * x)
          {
            y = -y;
          }
        w++;
        z += 10;
      }
      if (x >= 4)
        Contract.Assert(y > 2);
    }

    // from Cousot Halbwachs 1978
    [ClousotRegressionTest("fast")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)45, MethodILOffset = 0)]
    private void Test4()
    {
      int i = 2;
      int j = 0;
      while (NonDeterministic())
      {
        if (NonDeterministic())
        {
          i += 4;
        }
        else
        {
          i += 2;
          j++;
        }
      }
      Contract.Assert(2 * j + 2 <= i);
    }

    [ClousotRegressionTest("simplex-convexhull")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)45, MethodILOffset = 0)]
    private void Test4Prime()
    {
      int i = 2;
      int j = 0;
      while (NonDeterministic())
      {
        if (NonDeterministic())
        {
          i += 4;
        }
        else
        {
          i += 2;
          j++;
        }
      }
      Contract.Assert(2 * j + 2 <= i);
    }

    // bubblesort simplified, from Cousot Halbwachs 1978
    // [ClousotRegressionTest("fast")] // it fails with fast, but we do not really care anymore
    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 28, MethodILOffset = 0)]
    private void Test5(int N, int[] K)
    {
      Contract.Requires(K.Length == N);

      int b, j, t;
      b = N;
      while (b >= 1)
      {
        j = 1;
        t = 0;
        while (j <= b - 1)
        {
          if (K[j - 1] > K[j])
          {
            // exchange j and j + 1; no side effects
            t = j;
          }
          j++;
        }
        if (t == 0) return;
        b = t;
      }
    }

    // From Ashutosh Gupta1, Rupak Majumdar2, and Andrey Rybalchenko, TACAS 2010
    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=37,MethodILOffset=0)]
    public void Test6_FastNoOptions(int n, int m)
    {
      Contract.Assume(n <= m);
      int i, j, k;

      for (i = 0; i < n; i++)
        for (j = 0; j < n; j++)
          for (k = j; k < n + m; k++)
            Contract.Assert(i + j <= n + k + m);
    }

    // From Ashutosh Gupta1, Rupak Majumdar2, and Andrey Rybalchenko, TACAS 2010
    [ClousotRegressionTest("fast")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=37,MethodILOffset=0)]
    public void Test6(int n, int m)
    {
      Contract.Assume(n <= m);
      int i, j, k;

      for (i = 0; i < n; i++)
        for (j = 0; j < n; j++)
          for (k = j; k < n + m; k++)
            Contract.Assert(i + j <= n + k + m);
    }

    // From Ashutosh Gupta1, Rupak Majumdar2, and Andrey Rybalchenko, TACAS 2010
    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 37, MethodILOffset = 0)]
    public void Test6_Simplex(int n, int m)
    {
      Contract.Assume(n <= m);
      int i, j, k;

      for (i = 0; i < n; i++)
        for (j = 0; j < n; j++)
          for (k = j; k < n + m; k++)
            Contract.Assert(i + j <= n + k + m);
    }


    // From a Bertand Jeannet paper where he gave the example below as example of widening + narrowing in Polyhedra
    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=42,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=55,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=82,MethodILOffset=0)]
    public static void Test7()
    {
      var x = 5;
      var y = 100;
      var z = 0;

      while (x >= 0)
      {
        x = x - 1;
        y = y + 10;
        if (x + y >= 150)
        {
          z = x;
        }
        else
        {
          z = y;
        }
      }

      Contract.Assert(x == -1);
      Contract.Assert(y == 160);
      Contract.Assert(z == -1);  // To prove it we need loop unrolling, or simply the WPs
      Contract.Assert(10 * x + y == 150);
    }

    // from Vance's StringBuilder implementation, minimized
    // initial example :
    // internal unsafe StringBuilder Append(char* value, int valueCount)
    [ClousotRegressionTest("fast")]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=58,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven. The static checker determined that the condition 'value >= (2 * (m_ChunkChars.Length - m_ChunkLength))' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(value >= (2 * (m_ChunkChars.Length - m_ChunkLength)));", PrimaryILOffset = (int)58, MethodILOffset = 0)]
#endif
    private void StringBuilderAppend(int value /*represents WritableBytes(value)*/, int valueCount, int m_ChunkLength, char[] m_ChunkChars)
    {
      Contract.Requires(valueCount >= 0);
      Contract.Requires(value >= 2 * valueCount);
      int newIndex = valueCount + m_ChunkLength;
      if (newIndex <= m_ChunkChars.Length)
      { }
      else
      {
        int firstLength = m_ChunkChars.Length - m_ChunkLength;
        if (firstLength > 0)
          Contract.Assert(value >= 2 * firstLength);
      }
    }

    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)58, MethodILOffset = 0)]
    private void StringBuilderAppend_Simplex(int value /*represents WritableBytes(value)*/, int valueCount, int m_ChunkLength, char[] m_ChunkChars)
    {
      Contract.Requires(valueCount >= 0);
      Contract.Requires(value >= 2 * valueCount);
      int newIndex = valueCount + m_ChunkLength;
      if (newIndex <= m_ChunkChars.Length)
      { }
      else
      {
        int firstLength = m_ChunkChars.Length - m_ChunkLength;
        if (firstLength > 0)
          Contract.Assert(value >= 2 * firstLength);
      }
    }
  }

  class FromSumit
  {
    // All the examples come with an associated Invariant method.
    // The invariant method has the invariant we would like to find as a precondition.
    // Putting Contract.Assert instead would give the domain the invariants we want, whereas we want the domain to discover them without help
    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 6, MethodILOffset = 22)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 18, MethodILOffset = 22)]
    private static void Example6_1(int Len_L, int Pos_f)
    {
      Contract.Requires(Pos_f <= Len_L);
      int c = 0;
      for (int Pos_e = Pos_f; Pos_e < Len_L /*e != null*/; Pos_e++ /*e = L.GetNext(e)*/)
      {
        Invariant6_1(c, Pos_e, Pos_f, Len_L);
        c++;
      }
    }

    private static void Invariant6_1(int c, int Pos_e, int Pos_f, int Len_L)
    {
      Contract.Requires(c == Pos_e - Pos_f);
      Contract.Requires(Pos_e <= Len_L);
    }

    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 6, MethodILOffset = 21)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 18, MethodILOffset = 21)]
    private static void Example6_2(int OldLen_L)
    {
      Contract.Requires(OldLen_L >= 0);
      int c = 0;
      int Len_L = OldLen_L;
      while (Len_L > 0/*!L.IsEmpty()*/)
      {
        Invariant6_2(c, OldLen_L, Len_L);
        // L.RemoveHead()
        if (Len_L > 0)
        {
          Len_L--;
        }

        c++;

      }
    }

    private static void Invariant6_2(int c, int OldLen_L, int Len_L)
    {
      Contract.Requires(c == OldLen_L - Len_L);
      Contract.Requires(Len_L >= 0);
    }

    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 24)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 20, MethodILOffset = 24)]
    private static void Example6_3(int OldLen_L)
    {
      Contract.Requires(OldLen_L >= 0);
      int Len_L = OldLen_L;
      int c = 0;
      for (int Pos_e = 0 /*e = L.Head()*/; Pos_e < Len_L /*e != null*/; )
      {
        Invariant6_3(c, Pos_e, OldLen_L, Len_L);
        int tmp = Pos_e;
        Pos_e++; // e = L.GetNext(e);
        if (Examples.NonDeterministic())
        {
          //L.Remove(tmp);
          Len_L--;
          if (tmp < Pos_e)
          {
            Pos_e--;
          }
        }
        c++;
      }
    }

    private static void Invariant6_3(int c, int Pos_e, int OldLen_L, int Len_L)
    {
      Contract.Requires(c == Pos_e + OldLen_L - Len_L);
      Contract.Requires(Pos_e <= Len_L);
    }

    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 7, MethodILOffset = 86)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 19, MethodILOffset = 86)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 86)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 11, MethodILOffset = 30)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 21, MethodILOffset = 30)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 33, MethodILOffset = 30)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 45, MethodILOffset = 30)]
    private static void Example6_4(int OldLen_L)
    {
      Contract.Requires(OldLen_L > 0);
      int Len_ToDo = 0; // ToDo.Init();
      int Len_Done = 0; // Done.Init();

      // L.MoveTo(L.Head(), ToDo);
      int Len_L = OldLen_L - 1;
      Len_ToDo++;

      int c1 = 0;
      while (Len_ToDo > 0/*!ToDo.IsEmpty()*/)
      {
        Invariant6_4_1(c1, OldLen_L, Len_L, Len_ToDo, Len_Done);
        // e = ToDo.Head();

        Len_ToDo--; // ToDo.RemoveHead();
        Len_Done++; // Done.Insert(e);
        while (Examples.NonDeterministic()) // foreach successor s ine e.Successors()
        {
          if (Len_L > 0/*L.contains(s)*/)
          {
            //L.MoveTo(s, ToDo);
            Len_L--;
            Len_ToDo++;
          }
        }
        c1++;
      }

      int c2 = 0;
      for (int Pos_e = 0; Pos_e < Len_Done; Pos_e++)
      {
        Invariant6_4_2(c2, Pos_e, Len_Done, OldLen_L);
        c2++;
      }
    }

    private static void Invariant6_4_1(int c1, int OldLen_L, int Len_L, int Len_ToDo, int LenDone)
    {
      Contract.Requires(c1 <= OldLen_L - Len_L - Len_ToDo);
      Contract.Requires(c1 == LenDone);
      Contract.Requires(Len_L >= 0);
      Contract.Requires(Len_ToDo >= 0);
    }

    private static void Invariant6_4_2(int c2, int Pos_e, int Len_Done, int OldLen_L)
    {
      Contract.Requires(c2 <= Pos_e);
      Contract.Requires(Pos_e <= Len_Done);
      Contract.Requires(Len_Done <= OldLen_L);
    }

    private void Example7_2(int Len_L, int TotalNodes_L)
    {
      Contract.Requires(Len_L >= 0);
      Contract.Requires(TotalNodes_L >= 0);
      int c = 0;
      int TotalPos_e = 0;
      int Len_e;
      for (int Pos_e = 0; Pos_e < Len_L;)
      {
        Len_e = new Random().Next();
        Contract.Assume(Len_e >= 0);
        for (int Pos_f = 0; Pos_f < Len_e; Pos_f++)
        {
          c++;
        }
        TotalPos_e += Len_e;
        Pos_e++;
        Contract.Assume(TotalPos_e <= TotalNodes_L);
        c++;
      }
    }

    private void Example7_3(int Len_L, int MaxNodes_L)
    {
      Contract.Requires(Len_L >= 0);
      Contract.Requires(MaxNodes_L >= 0);
      int c = 0;
      int Pos_e = 0;
      int TotalPos_e = 0;
      int Len_e = new Random().Next();
      Contract.Assume(Len_e >= 0);
      Contract.Assume(Len_e <= MaxNodes_L);
      while (Pos_e < Len_L)
      {
        if (Examples.NonDeterministic())
          break;
        Pos_e++;
        TotalPos_e += Len_e;
        Len_e = new Random().Next();
        Contract.Assume(Len_e >= 0);
        Contract.Assume(Len_e <= MaxNodes_L);
        c++;
      }
      // there should be something like if (e == null) return; in the original code, but here we don't care about it
      for (int Pos_f = 0; Pos_f < Len_e; Pos_f++)
      {
        c++;
      }
    }
  }

  class FromOctagons
  {
    [ClousotRegressionTest("fastnooptions_cci1")] // F: For some reason, it fails with cci2
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 44, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 59, MethodILOffset = 0)]
    public void JournalPaper()
    {
      var x = 10;
      var y = 0;

      while (x >= 0)
      {
        x = x - 1;
        if (Random())
        {
          y = y + 1;
        }
      }

      Contract.Assert(x == -1); // thanks to threshold -1 for the widening of intervals 
      Contract.Assert(y <= 11);
      Contract.Assert(x + y <= 10);
    }

    [ClousotRegressionTest("fast")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 44, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 59, MethodILOffset = 0)]
    public void JournalPaper_fast()
    {
      var x = 10;
      var y = 0;

      while (x >= 0)
      {
        x = x - 1;
        if (Random())
        {
          y = y + 1;
        }
      }

      Contract.Assert(x == -1); // thanks to threshold -1 for the widening of intervals 
      Contract.Assert(y <= 11);
      Contract.Assert(x + y <= 10);
    }

    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 44, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 59, MethodILOffset = 0)]
    public void JournalPaper_simplex()
    {
      var x = 10;
      var y = 0;

      while (x >= 0)
      {
        x = x - 1;
        if (Random())
        {
          y = y + 1;
        }
      }

      Contract.Assert(x == -1); // thanks to threshold -1 for the widening of intervals 
      Contract.Assert(y <= 11);
      Contract.Assert(x + y <= 10);
    }

    private bool Random()
    {
      return (new Random()).NextDouble() > 0.5;
    }
  }

  class FromAstreePaper
  {
    public bool Coin()
    {
      return System.Environment.TickCount % 12 == 0;
    }

    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 11, MethodILOffset = 79)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 31, MethodILOffset = 79)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 11, MethodILOffset = 69)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 31, MethodILOffset = 69)]
    public int Rand()
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= 987);

      int i = 0, x = 0;

      while (i < 987)
      {
        x++;
        if (Coin())
        {
          x = 0;
        }
        i++;

        if (Coin())
          return x;
      }

      return x;
    }

    [ClousotRegressionTest("fast")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 20, MethodILOffset = 80)]
//    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 36, MethodILOffset = 80)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 20, MethodILOffset = 74)]
 //   [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 36, MethodILOffset = 74)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.Result<int>() <= n",PrimaryILOffset=36,MethodILOffset=80)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.Result<int>() <= n",PrimaryILOffset=36,MethodILOffset=74)]
    public int Rand(int n)
    {
      Contract.Requires(n > 0);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= n);

      int i = 0, x = 0;

      while (i < n)
      {
        x++;
        if (Coin())
        {
          x = 0;
        }
        i++;

        if (Coin())
          return x;
      }

      return x;
    }
  }

  class FromCousotCousotTechnicalReport75
  {    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=49,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=58,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=67,MethodILOffset=0)]
    public void CousotCousot75(int counter)
    {
      int v = 1, w = 2, x = 3, y = 3, z = 0;
      while (counter > 0)
	{
	  w = 2 * v; y = y + 1; z = z - v;
	  v = w - v; x = y + z;

	  counter--;
	}
      Contract.Assert(v == 1); 
      Contract.Assert(w == 2);

      Contract.Assert(x == 3); // need to infer the loop invariant y + z = 3
    }
  }
}

namespace FixedExamples
{
  public class Vance_StringBuilder
  {
    public char[] m_ChunkChars;
    public int m_ChunkLength;
    public int m_ChunkOffset;

    public int Length
    {
      [ClousotRegressionTest("fastnooptions")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 20, MethodILOffset = 38)]
      get
      {
        Contract.Ensures(Contract.Result<int>() == m_ChunkOffset + m_ChunkLength);

        return m_ChunkOffset + m_ChunkLength;
      }
    }

    public int Capacity
    {
      [ClousotRegressionTest("fastnooptions")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 22, MethodILOffset = 42)]
      get
      {
        Contract.Ensures(Contract.Result<int>() == m_ChunkChars.Length + m_ChunkOffset);  // f: this should be inferred !!!

        return m_ChunkChars.Length + m_ChunkOffset;
      }
    }

    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 60, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 79, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 19, MethodILOffset = 84)]
    public void M()
    {
      int firstLength = m_ChunkChars.Length - m_ChunkLength;

      Contract.Assert(firstLength >= 0);    // F: Should prove it

      if (firstLength > 0)
      {
        // ThreadSafeCopy(value, m_ChunkChars, m_ChunkLength, firstLength);
        m_ChunkLength = m_ChunkChars.Length;

        Contract.Assert(Length == Capacity); // Trivial
      }
      else
      {
        // Contract.Assert(Length == Capacity);
      }

      Contract.Assert(Length == Capacity); // Should prove it
    }

    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 60, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 81, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 100, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 19, MethodILOffset = 105)]
    public void M_With_Hint()
    {
      int firstLength = m_ChunkChars.Length - m_ChunkLength;

      Contract.Assert(firstLength >= 0);    // F: Should prove it

      if (firstLength > 0)
      {
        // ThreadSafeCopy(value, m_ChunkChars, m_ChunkLength, firstLength);
        m_ChunkLength = m_ChunkChars.Length;

        Contract.Assert(Length == Capacity); // Trivial
      }
      else
      {
        Contract.Assert(Length == Capacity);
      }

      Contract.Assert(Length == Capacity); // Should prove it
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.m_ChunkChars.Length >= this.m_ChunkLength);
    }
  }

  public class SortAlgorithms
  {
    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 20, MethodILOffset = 0)]
    private void If0(int[] K)
    {
      int b, j;

      b = K.Length;

      if (b >= 1)
      {
        j = 1;

        if (j < b)
        {
          K[j - 1] = 12;
        }
      }
    }

    // F: For some reason we need the infoct option to prove it
    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 18, MethodILOffset = 0)]
    private void If1(int[] K)
    {
      int b, j, t;

      b = K.Length;

      while (b >= 1)
      {
        j = 1;
        t = 0;

        if (j < b)
        {
          // Contract.Assert(b <= K.Length); // ok
          // Contract.Assert(j < K.Length); // ok
          K[j] = 12; // not ok  

          // Contract.Assert(j -1< K.Length); // not ok

          // K[j - 1] = 12;  // not ok

          j++;
        }

        b = t;
      }
    }

    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 18, MethodILOffset = 0)]
    private void BubbleSort0(int[] K)
    {
      int b, j, t;

      b = K.Length;

      while (b >= 1)
      {
        j = 1;
        t = 0;

        while (j < b)
        {
          // Contract.Assert(b <= K.Length);

          K[j - 1] = 12;

          j++;
        }
        if (t == 0)
        {
          return;
        }

        b = t;
      }
    }

    // [ClousotRegressionTest("fast")]
    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 27, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 27, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True, Message=@"Upper bound access ok", PrimaryILOffset=16, MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True, Message=@"Upper bound access ok", PrimaryILOffset=19, MethodILOffset=0)]
    private void BubbleSort(int[] K)
    {
      int b, j, t;

      b = K.Length;

      while (b >= 1)
      {
        j = 1;
        t = 0;

        while (j < b)
        {
          // Contract.Assert(b <= K.Length);

          if (K[j - 1] > K[j])
          {
            Swap(ref K[j - 1], ref K[j]);

            t = j;
          }

          j++;
        }
        if (t == 0)
        {
          return;
        }

        b = t;
      }
    }

    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 26, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 26, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 39, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 39, MethodILOffset = 0)]
    private void BubbleSortSimplex(int[] K)
    {
      int b, j, t;

      b = K.Length;

      while (b >= 1)
      {
        j = 1;
        t = 0;

        while (j < b)
        {
          if (K[j - 1] > K[j])
          {
            var tmp = K[j - 1];
            K[j - 1] = K[j];
            K[j] = tmp;
            
            t = j;
          }

          j++;
        }
        if (t == 0)
        {
          return;
        }

        b = t;
      }
    }

    private void Swap<T>(ref T x, ref T y)
    {
      Contract.Ensures(x.Equals(Contract.OldValue(y)));
      Contract.Ensures(y.Equals(Contract.OldValue(x)));

      T tmp = x;
      x = y;
      y = tmp;
    }
  }
}

namespace BugRepros
{
  public class BitArray
  {
    public int[] m_array;
    public int m_length;

    private static int GetArrayLength(int n, int div)
    {
      return (((n - 1) / div) + 1);
    }

    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 66, MethodILOffset = 0)]
    public BitArray And()
    {
      Contract.Requires(m_length > 0);
      Contract.Requires(m_array.Length <= m_length);

      int ints = GetArrayLength(m_length, 32);

      // There is no reason Subpolyhedra can prove it through
      // this was a bug, now fixed
      Contract.Assert(ints >= m_array.Length);

      return this;
    }
  }

  public class BitArray2
  {

    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 84, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 32, MethodILOffset = 94)] 
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 48, MethodILOffset = 94)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 66, MethodILOffset = 94)] 
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 32, MethodILOffset = 102)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<int>() <= n. The static checker determined that the condition '((n - 1) / div + 1) <= n' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(((n - 1) / div + 1) <= n);", PrimaryILOffset = 48, MethodILOffset = 102)]
    //[RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 66, MethodILOffset = 102)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "ensures unproven: Contract.Result<int>() * div >= n. The static checker determined that the condition '(((n - 1) / div + 1) * div) >= n' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((((n - 1) / div + 1) * div) >= n);", PrimaryILOffset = 66, MethodILOffset = 102)]
    private static int GetArrayLength(int n, int div)
    {
      Contract.Requires(div > 0);
      Contract.Requires(n >= 0);

      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= n); // Proving this is quite hard, as it involves reasoning on integer multiplication >= 0
      Contract.Ensures(Contract.Result<int>() * div >= n); // Can't prove it anymore

      Contract.Assert((((n - 1) / div) + 1) >= 0);

      return n > 0 ? (((n - 1) / div) + 1) : 0;
    }

    [ClousotRegressionTest("fastnooptions")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 34, MethodILOffset = 0)]
    private static void SubPolyRepro(int n, int div)
    {
      Contract.Requires(div > 0);
      Contract.Requires(n >= 0);

      Contract.Assert((((n - 1) / div) + 1) >= 0);
    }
  }
}

namespace TestForComponentsOfSubpoly
{
  class Subpoly
  {

    [ClousotRegressionTest("fast")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 27, MethodILOffset = 0)]
    private void Test5(bool b1, bool b2)
    {
      int i = 2;
      int j = 0;
      while (b1)
      {
        if (b2)
        {
          i += 4;
        }
      }
      // Proven by intervals
      Contract.Assert(2 * j + 2 <= i);
    }

    [ClousotRegressionTest("fast")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    private void Test6(bool b1, bool b2)
    {
      int i = 2;
      int j = 0;
      while (b1)
      {
        if (b2)
        {
          i += 2;
          j++;
        }
      }
      // Proven by Karr
      Contract.Assert(2 * j + 2 == i);
    }

    [ClousotRegressionTest("simplex-convexhull")]
    // F: This example does not work anymore after we've added the abstraction of the system of equations before running the simplex
    //  It seems that the convex hull generates some monsters that we abstract way, but in this case are needed
    //    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 39, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=39,MethodILOffset=0)]
    private void Test6(bool b1, bool b2, int i)
    {
      Contract.Requires(i >= 2);
      int j = 0;

      while (b1)
      {
        i += 2;
        j++;
      }
      // Proven by Subpoly + CH
      Contract.Assert(2 * j + 2 <= i);
    }

   
    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Array creation : ok",PrimaryILOffset=5,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=26,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=26,MethodILOffset=0)]
    public static string[] Test7(string[] x)
    {
      var max = 0;
      var result = new string[x.Length];

      foreach (string s in x)
      {
        // proven by infoct (which infers max < x.Length)
        result[max] = s;

        if (s == null)
        {
          max++;
        }
      }

      return result;
    }

    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=36,MethodILOffset=62)]
    public int Test7(int x, int y)
    {
      Contract.Requires(x >=0);
      Contract.Requires(y >= 0);
      
      Contract.Ensures(Contract.Result<int>() == 2*x + y);

      int r = x;
      for (int n = 0; n < y; n++)
	//invariant r == x+n && n <= y;
	{
	  r++;
	}
      return r+x;
    }
  }

  public class HandlingofDisequalities
  {
    public int _size;
    public object[] _array;

    // We test the three scenarios, as the handling of "neq" involves a join
    [ClousotRegressionTest("fastnooptions")]
    [ClousotRegressionTest("fast")]
    [ClousotRegressionTest("simplex")]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=57,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 66, MethodILOffset = 0)]
#endif
    public void Test8(Object obj)
    {
      Contract.Assume(_size <= _array.Length);

      if (_size == _array.Length)
      {
        // This is just to force the compiler to emit the neq instruction
        Object[] newArray = new Object[2 * _array.Length];
      }
      else
      {
        // This is ok as it is implied by _size <= _array.Length && _size != _array.Length
        Contract.Assert(_size < _array.Length);
      }
     
    }
  }
}

namespace BugRepros
{
  public class Increments
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    public void IncSymbolic_Eq(int n)
    {
      Contract.Requires(n >= 0);

      var x = 0;

      while (x < n)
      {
        x++;
      }

      // here x == n
      Contract.Assert(x == n);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 28, MethodILOffset = 0)]
    public void IncSymbolic_Eq_OffByOne_Wrong(int n)
    {
      Contract.Requires(n >= 0);

      var x = 0;

      while (x <= n)
      {
        x++;
      }

      // here x == n+1
      Contract.Assert(x == n);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 30, MethodILOffset = 0)]
    public void IncSymbolic_Eq_OffByOne_Ok(int n)
    {
      Contract.Requires(n >= 0);

      var x = 0;

      while (x <= n)
      {
        x++;
      }

      Contract.Assert(x == n + 1);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 40, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 40, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 47, MethodILOffset = 0)]
    public void TestCausingABugInRemovingRedundancies(string[] myStrings)
    {
      Contract.Requires(myStrings != null);
      for (int i = 0; i < myStrings.Length; i++)
      {
        myStrings[i] = "a";
      }

      for (int i = 0; i < myStrings.Length; i++)
      {
        Contract.Assert(myStrings[i] != null);  // Needs -arrays
      }
    }

    [ClousotRegressionTest("simplex")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 29, MethodILOffset = 0)]
    public static void Partition(int[] arr)
    {
      Contract.Requires(arr != null);

      int z = 0;

      for (int i = 0; i < arr.Length; i++)
      {
        //Contract.Assert(z <= i); // This is inferred by Subpolyhedra
        if (arr[i] == 0)
        {
          Contract.Assert(z < arr.Length);
          z++;
        }
      }
    }
  }
}


namespace Numbers
{
  public class Numbers
  {    
    // Signed

    public void _Sbyte(sbyte x)
    {
      Contract.Assert(x >= sbyte.MinValue);
      Contract.Assert(x <= sbyte.MaxValue);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=27,MethodILOffset=0)]
    public void _Int16(Int16 x)
    {
      Contract.Assert(x >= Int16.MinValue);
      Contract.Assert(x <= Int16.MaxValue);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=27,MethodILOffset=0)]
    public void _Int32(Int32 x)
    {
      Contract.Assert(x >= Int32.MinValue);
      Contract.Assert(x <= Int32.MaxValue);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=35,MethodILOffset=0)]
    public void _Int64(Int64 x)
    {
      Contract.Assert(x >= Int64.MinValue);
      Contract.Assert(x <= Int64.MaxValue);
    }

    // Unsigned 

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=23,MethodILOffset=0)]
    public void _Char(char x)
    {
      Contract.Assert(x >=  char.MinValue);
      Contract.Assert(x <= char.MaxValue);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=23,MethodILOffset=0)]
    public void _Byte(byte x)
    {
      Contract.Assert(x >= byte.MinValue);
      Contract.Assert(x <= byte.MaxValue);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=23,MethodILOffset=0)]
    public void _UInt16(UInt16 x)
    {
      Contract.Assert(x >= UInt16.MinValue);
      Contract.Assert(x <= UInt16.MaxValue);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=19,MethodILOffset=0)]
    public void _UInt32(uint x)
    {
      Contract.Assert(x >=0);
      Contract.Assert(x <= uint.MaxValue);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=21,MethodILOffset=0)]
    public void _UInt64(UInt64 x)
    {
      Contract.Assert(x >= 0);
      Contract.Assert(x <= UInt64.MaxValue);
    }
  }

  public class Repro
  {
    public uint Length;
    private uint capacity;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.capacity >= 0x100);
      Contract.Invariant(this.capacity >= this.Length);
    }

    [ContractVerification(false)]
    public Repro(uint capacity)
    {
      this.capacity = capacity;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=35,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=63,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=85,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=16,MethodILOffset=90)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=38,MethodILOffset=90)]
    public void Grow(uint newLength)
    {
      Contract.Requires(newLength > this.Length);

      Contract.Assert(newLength <= uint.MaxValue);     
      Contract.Assert(this.capacity < uint.MaxValue); // This caused an unreached

      this.Length = newLength;
   
      Contract.Assert(this.capacity >= 0x100);
      Contract.Assert(this.capacity >= this.Length);   
    }
  }

  public class ArrayExtensible<T>
  {
    public uint Length;

    ulong capacity = 0x100;
    public  T[][][][] index3;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=57,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=77,MethodILOffset=0)]
    private void AddCapacity()
    {
      Contract.Requires(this.capacity >= this.Length);
      Contract.Requires(this.capacity < uint.MaxValue);  
      var n = this.capacity >> 24;                        // There was a bug here causing the inference of a wrong constraint
      Contract.Assert(n < 0x100);
      Contract.Assert(this.index3.Length == 0x100);
    }
  }
}

namespace Strilanc
{
  public class QuadraticRepro
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=15,MethodILOffset=28)]
    public long Milliseconds(long quantity)
    {
      // There was a bug in setting up the LP problem for the simplex, causing the ensures to be unreached     
      Contract.Ensures(checked(Contract.Result<long>() == quantity * 10000L));

      return checked(quantity * 10000L);
    }
  }
}


/*
namespace JonathanTapicer
{
  public class C_2
  {
    public static int C1;
    
    public void M1()
    {
        Contract.Ensures(C1 <= 1);
        C1 = 1;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=44,MethodILOffset=0)]
    public void M2(int n)
    {
      Contract.Requires(n > 0);
      
      var C2 = 0;
      for (int i = 0; i < n; i++)
        {
          //Contract.Assert(C2 <= i);
	  
          M1();
          C2 += C1;
        }

      Contract.Assert(C2 <= n);
    }

    [ClousotRegressionTest]
    public void M2_WithAssert(int n)
    {
      Contract.Requires(n > 0);
      
      var C2 = 0;
      for (int i = 0; i < n; i++)
        {
          Contract.Assert(C2 <= i);
	  
          M1();
          C2 += C1;
        }

      Contract.Assert(C2 <= n);
    }

  }
}
*/

namespace FromPapers
{
   class DilligsPLDI12
   {
#if CLOUSOT2 || !SIMPLEXCONVEX

	[ClousotRegressionTest]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=55,MethodILOffset=0)]
		static void Foo2(bool flag, int n)
		{
			Contract.Requires(n >= 0);

			int k = 1;
			if (flag)
			{
				k = n * n;
				//Contract.Assume(k >= 0); // We should infer it
			}

			int i = 0, j = 0;
			{
				i = 1;
				j = 1;
				while (i <= n)
				{
					i++;
					j += i;
				}
			}

			int z = k + i + j;
			Contract.Assert(z > 2 * n);
		}
#endif
	}
}