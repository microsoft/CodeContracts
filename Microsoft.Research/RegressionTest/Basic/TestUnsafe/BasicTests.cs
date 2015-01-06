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

// Basic tests for the new unsafe analysis (now called buffers)

#define CONTRACTS_FULL
#if !NETFRAMEWORK_4_0
#define WRITABLE_BYTES
#endif

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace UnsafeTest
{
  public unsafe class LocAlloc
  {
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Localloc size ok", PrimaryILOffset = 5, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 15, MethodILOffset = 0)]
    public int* LocAlloc0()
    {
      int* x = stackalloc int[10];

      x[3] = 24;

      return x;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Localloc size ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory store might be below the lower bound. The static checker determined that the condition '0 <= (2 * x - 4)' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(0 <= (2 * x - 4));", PrimaryILOffset = 29, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 29, MethodILOffset = 0)]
    public unsafe char* LocAlloc1(int x)
    {
      Contract.Requires(x >= 0);
      char* p = stackalloc char[x];

      p[x - 2] = 'f';

      return p;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Localloc size ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 29, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 29, MethodILOffset = 0)]
    public unsafe char* LocAlloc1_Pre(int x)
    {
      Contract.Requires(x >= 2);

      char* p = stackalloc char[x];

      p[x - 2] = 'f';

      return p;
    }

#if WRITABLE_BYTES
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Localloc size ok", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
    public unsafe void LocAlloc2()
    {
      var left = stackalloc byte[12];

      Contract.Assert(Contract.WritableBytes(left) >= 4);
    }
#endif
  }

  public unsafe class ReadWriteMem
  {
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 6, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory store might be above the upper bound", PrimaryILOffset = 6, MethodILOffset = 0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(true <= p->WritableBytes); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)] // TODO: prettyprint WritableBytes
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(true <= p->WritableBytes); for parameter validation",PrimaryILOffset=6,MethodILOffset=0)] // TODO: prettyprint WritableBytes
#endif
    public void Access0(int* p)
    {
      // 0 <= 4
      // wb(p) >= 4
      *p = 256;
    }

#if WRITABLE_BYTES
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 24, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 24, MethodILOffset = 0)]
    public void Access0_Pre(int* p)
    {
      Contract.Requires(Contract.WritableBytes(p) >= 4);
      // 0 <= 4
      // wb(p) >= 4
      *p = 256;
    }
#endif

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory store might be above the upper bound", PrimaryILOffset = 10, MethodILOffset = 0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires((48 + 4 != 0) <= p->WritableBytes); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)] // TODO: pretty print the expression
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires((48 + 4 != 0) <= p->WritableBytes); for parameter validation",PrimaryILOffset=3,MethodILOffset=0)] // TODO: pretty print the expression
#endif
    public void Access1(int* p)
    {
      // 0 <= 12 * 4
      // wb(p) >= 12 * 4 + 4
      *(p + 12) = 256;
    }

#if WRITABLE_BYTES
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe store is correct",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Unsafe memory store might be above the upper bound",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires((48 + 4 != 0) <= p->WritableBytes); for parameter validation",PrimaryILOffset=21,MethodILOffset=0)]
	public void Access1_PreWrong(int* p)
    {
      Contract.Requires(Contract.WritableBytes(p) >= 4);
      // 0 <= 12 * 4
      // wb(p) >= 12 * 4 + 4
      *(p + 12) = 256;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe store is correct",PrimaryILOffset=29,MethodILOffset=0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 29, MethodILOffset = 0)]
    public void Access1_Pre(int* p)
    {
      Contract.Requires(Contract.WritableBytes(p) >= (12 * 4) + 4);
      // 0 <= 12 * 4
      // wb(p) >= 12 * 4 + 4
      *(p + 12) = 256;
    }
#endif

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe load is correct", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory load might be above the upper bound", PrimaryILOffset = 7, MethodILOffset = 0)]
    public int Access2(int* p)
    {
      // 0 <= 4
      // wb(p) >= 4
      return *p++;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe store is correct",PrimaryILOffset=12,MethodILOffset=0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory store might be above the upper bound", PrimaryILOffset = 12, MethodILOffset = 0)]
    public void Access3(int* p)
    {
      // 0 <= 8
      // wb(p) >= 4 + 4
      *(++p) = -92156;
    }

#if WRITABLE_BYTES
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 30, MethodILOffset = 0)]
    public void Access3_Pre(int* p)
    {
      Contract.Requires(Contract.WritableBytes(p) >= 8);
      // 0 <= 8
      // wb(p) >= 4 + 4
      *(++p) = -92156;
    }
#endif

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Unsafe memory store might be below the lower bound. The static checker determined that the condition '0 <= (2 * a)' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(0 <= (2 * a));",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Unsafe memory store might be above the upper bound. The static checker determined that the condition '(2 * a + 2) <= p->WritableBytes' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires((2 * a + 2) <= p->WritableBytes);",PrimaryILOffset=8,MethodILOffset=0)]
    public void Access4(char* p, int a)
    {
      // 0 <= 2 * a
      // wb(p) >= 2 * a + 2 
      *(p + a) = 'f';
    }

#if WRITABLE_BYTES
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe store is correct",PrimaryILOffset=42,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The upper bound of the unsafe store is correct",PrimaryILOffset=42,MethodILOffset=0)]
    public void Access4_ok(char* p, int a)
    {
      Contract.Requires(a >= 0);
      Contract.Requires(Contract.WritableBytes(p) >= (ulong)(2 * a + 2));
      // 0 <= 2 * a
      // wb(p) >= 2 * a + 2 
      *(p + a) = 'f';
    }
#endif


  }

  public unsafe class Loops
  {
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 12, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory store might be above the upper bound", PrimaryILOffset = 12, MethodILOffset = 0)]
    public void Loop0(long* p, int count)
    {
      // WB(p) >= (uint)(count * 8));

      for (int i = 0; i < count; i++)
      {
        *(p + i) = -1;
      }
    }

#if WRITABLE_BYTES
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 32, MethodILOffset = 0)]
    public void Loop0_Pre(long* p, int count)
    {
      Contract.Requires(Contract.WritableBytes(p) >= (uint)(count * sizeof(long)));

      for (int i = 0; i < count; i++)
      {
        *(p + i) = -1;
      }
    }
#endif

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 14, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory store might be above the upper bound", PrimaryILOffset = 14, MethodILOffset = 0)]
    public void Loop1(long* p, int count)
    {
      // WB(p) >= (uint)(count * 8));
      for (int i = count - 1; i >= 0; i--)
      {
        *(p + i) = -1;
      }
    }

#if WRITABLE_BYTES    
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 34, MethodILOffset = 0)]
    public void Loop1_Pre(long* p, int count)
    {
      Contract.Requires(Contract.WritableBytes(p) >= (uint)(count * sizeof(long)));

      for (int i = count - 1; i >= 0; i--)
      {
        p[i] = -1;
      }
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 36, MethodILOffset = 0)]
    public void Loop2_Pre(long* p, int count)
    {
      Contract.Requires(Contract.WritableBytes(p) >= (uint)(count * sizeof(long)));

      for (int i = 0; i < count; i++)
      {
        p[(count - 1) - i] = 0;
      }
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 26, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 26, MethodILOffset = 0)]
    public void Loop3(byte* d, uint bytes)
    {
      Contract.Requires(Contract.WritableBytes(d) >= bytes);

      for (int i = 0; i < bytes; i += 4)
      {
        d[i] = 1;
      }
    }
#endif

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe load is correct", PrimaryILOffset = 13, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory load might be above the upper bound", PrimaryILOffset = 13, MethodILOffset = 0)]
    public int Loop4(char* p, int count)
    {
      int s = 0;
      for (int i = 0; i < count; i++)
      {
        s = *(p++);
      }

      return s;
    }

#if WRITABLE_BYTES
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe load is correct", PrimaryILOffset = 33, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe load is correct", PrimaryILOffset = 33, MethodILOffset = 0)]
    public int Loop4_Pre(char* p, int count)
    {
      Contract.Requires(Contract.WritableBytes(p) >= (uint)(count * sizeof(char)));

      int s = 0;
      for (int i = 0; i < count; i++)
      {
        s = *(p++);
      }

      return s;
    }
#endif

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe load is correct", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory load might be above the upper bound", PrimaryILOffset = 16, MethodILOffset = 0)]
    public int Loop5(int* a, int count)
    {
      int s = 0;

      for (int i = count - 1; i >= 0; i--)
      {
        s += *(a--);
      }

      return s;
    }

#if WRITABLE_BYTES
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe load is correct", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe load is correct", PrimaryILOffset = 36, MethodILOffset = 0)]
    public int Loop5_Pre(int* a, int count)
    {
      Contract.Requires(Contract.WritableBytes(a) >= (uint)(count * sizeof(int)));

      int s = 0;

      for (int i = count - 1; i >= 0; i--)
      {
        s += *(a--);
      }

      return s;
    }
#endif
  }

  public unsafe class Fixed
  {
    // ok
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe load is correct", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory load might be above the upper bound", PrimaryILOffset = 25, MethodILOffset = 0)]
    public byte Fixed0(byte[] arr)
    {
      byte r;
      fixed (byte* bs = arr)
      {
        r = bs[0];
      }

      return r;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe load is correct", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe load is correct", PrimaryILOffset = 36, MethodILOffset = 0)]
    public byte Fixed0_Pre(byte[] arr)
    {
      Contract.Requires(arr.Length > 0);

      byte r;
      fixed (byte* bs = arr)
      {
        r = bs[0];
      }

      return r;
    }

    // ok
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe load is correct", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory load might be above the upper bound", PrimaryILOffset = 25, MethodILOffset = 0)]
    public int Fixed1(int[] arr)
    {
      int r;
      fixed (int* bs = arr)
      {
        r = bs[0];
      }

      return r;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe load is correct", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe load is correct", PrimaryILOffset = 36, MethodILOffset = 0)]
    public int Fixed1_pre(int[] arr)
    {
      Contract.Requires(arr.Length > 1);

      int r;
      fixed (int* bs = arr)
      {
        r = bs[0];
      }

      return r;
    }

    // ok
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 38, MethodILOffset = 0)]
    public unsafe void Fixed2()
    {
      char[] c = new char[12];
      fixed (char* pToc = c)
      {
        pToc[1] = '2';
      }
    }

    // ok
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 27, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 27, MethodILOffset = 0)]
    public unsafe void Fixed4(char[] c)
    {
      Contract.Requires(c.Length > 16);

      fixed (char* pc = &c[2])
      {
        pc[2] = '9';
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe store is correct",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Unsafe memory store might be above the upper bound", PrimaryILOffset = 20, MethodILOffset = 0)]
    public void Fixed5(string s)
    {
      fixed (char* c = s)
      {
        c[1] = 'x';
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The lower bound of the unsafe store is correct", PrimaryILOffset = 37, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"The upper bound of the unsafe store is correct", PrimaryILOffset = 37, MethodILOffset = 0)]
    public void Fixed5_Pre(string s)
    {
      Contract.Requires(s.Length >= 2);

      fixed (char* c = s)
      {
        c[1] = 'x';
      }
    }
  }
}
