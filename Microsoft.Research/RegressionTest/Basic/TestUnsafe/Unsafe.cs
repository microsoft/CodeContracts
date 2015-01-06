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

// Pietro's regression 
// here to check backward compatibility of the new analysis

#if !NETFRAMEWORK_4_0
#define WRITABLE_BYTES
#endif

using System.Diagnostics.Contracts;
using System;
using System.Runtime.InteropServices;
using Microsoft.Research.ClousotRegression;

namespace TestUnsafe
{

  public class SimpleUnsafe
  {
    // #1 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Unsafe memory store might be above the upper bound", PrimaryILOffset = 3, MethodILOffset = 0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(true <= p->WritableBytes); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)] // TODO: pretty print expression
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(true <= p->WritableBytes); for parameter validation",PrimaryILOffset=3,MethodILOffset=0)] // TODO: pretty print expression
#endif
    public unsafe void Write0(int* p)
    {
      *p = 12;
    }

    // #2 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Unsafe memory store might be above the upper bound", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 7, MethodILOffset = 0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(true <= p->WritableBytes); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)] // TODO: pretty print expression
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(true <= p->WritableBytes); for parameter validation",PrimaryILOffset=3,MethodILOffset=0)] // TODO: pretty print expression
#endif
    public unsafe void Write1(int* p)
    {
      *p = 12;
      *p = 22;
    }

#if WRITABLE_BYTES
    //#3 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 21, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 21, MethodILOffset = 0)]
    public unsafe void Write2(int* p)
    {
      Contract.Requires(Contract.WritableBytes(p) >= sizeof(int));
      *p = 12;
    }
#endif

    // #4 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "Unsafe memory store IS below the lower bound", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Unsafe memory store might be above the upper bound", PrimaryILOffset = 7, MethodILOffset = 0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires((-(64) + 4) <= p->WritableBytes); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires((-(64) + 4) <= p->WritableBytes); for parameter validation",PrimaryILOffset=3,MethodILOffset=0)]
#endif
    public unsafe void Write3(int* p)
    {
      *(p - 16) = 11;
    }


#if WRITABLE_BYTES
    // #6 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 26, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 26, MethodILOffset = 0)]
    public unsafe void InitToZero0(byte* buff, int len)
    {
      Contract.Requires(Contract.WritableBytes(buff) >= ((uint)len) * sizeof(byte));

      for (int i = 0; i < len; i++)
      {
        *(buff + i) = 0;
      }
    }

    // #7 One warning 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Unsafe memory store might be above the upper bound", PrimaryILOffset = 30, MethodILOffset = 0)]
    public unsafe void InitToZero1(long* buff, int len)
    {
      Contract.Requires(Contract.WritableBytes(buff) >= ((uint)len)); // We do NOT consider that sizeof(long) == 8

      for (int i = 0; i < len; i++)
      {
        *(buff + i) = 0;
      }
    }

    // #8 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 32, MethodILOffset = 0)]
    public unsafe void InitToZero2(long* buff, int len)
    {
      Contract.Requires(Contract.WritableBytes(buff) >= ((uint)len) * sizeof(long)); // We consider that sizeof(long) == 8

      for (int i = 0; i < len; i++)
      {
        *(buff + i) = 0;
      }
    }

    // #9 One warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 58, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Unsafe memory store might be above the upper bound", PrimaryILOffset = 58, MethodILOffset = 0)]
    public unsafe void InitToZero3(long* buff, int len, int start)
    {
      Contract.Requires(start >= 0);
      Contract.Requires(start < len);
      Contract.Requires(Contract.WritableBytes(buff) >= ((uint)len) * sizeof(long));

      for (int i = start; i < len; i++)
      {
        *(buff + start + i) = 0;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = (int)38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = (int)38, MethodILOffset = 0)]
    public unsafe void InitToZero_WithPointer0(int* buff, int len)
    {
      Contract.Requires(len >= 0);
      Contract.Requires(Contract.WritableBytes(buff) >= (uint)len * sizeof(int));

      for (int i = 0; i < len; i++)
      {
        *buff = 0;
        buff++;
      }
    }
#endif

  }

  // Examples extracted while proving StringBuilder
  public class SimpleUnsafe_WithBasicTypes
  {
#if WRITABLE_BYTES
    // ok
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = (int)15, MethodILOffset = (int)5)]
    unsafe public void Insert(/*int index, */ char value)
    { // F: tests the ldarga transfer function
      Insert(/*index,*/ &value, 1);
    }

    private char GetChar()
    {
      return 'a';
    }

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 15, MethodILOffset = 12)]
    unsafe public void Insert()
    { // F: tests the ldloca tranfer function
      var a = GetChar();
      Insert(&a, 1);
    }

    // ok
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = (int)15, MethodILOffset = (int)49)]
    unsafe public void WithArray(char[] value, int startIndex, int charCount)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(charCount > 0);
      Contract.Requires(startIndex <= value.Length - charCount);

      fixed (char* sourcePtr = &value[startIndex])
        Insert(/*index,*/ sourcePtr, charCount);
    }

    // ok
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = (int)15, MethodILOffset = (int)62)]
    unsafe public void Append(String value, int startIndex, int count)
    {
      Contract.Requires(count > 0);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(startIndex <= value.Length - count);

      fixed (char* valueChars = value)
        Insert(valueChars + startIndex, count);
    }

    // ok
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = (int)15, MethodILOffset = (int)33)]
    unsafe public void ToString0(int chunkLength, char[] sourceArray)
    {
      if ((uint)chunkLength <= (uint)sourceArray.Length)
      {
        fixed (char* sourcePtr = sourceArray)
          Insert(sourcePtr, chunkLength);
      }
    }

    unsafe private void Insert(/*int index, */char* value, int count)
    {
      Contract.Requires((Contract.WritableBytes(value) >= (System.UInt64)(count * 2)));
      // Does nothing
    }
#endif
  }

  unsafe class SimpleLoops
  {
    // ok
    // [MAF] broken when I added bread crumbs. From a glance at the dfa output, looks like a bug in Stripes join
    // F: It works with the new analysis
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe load is correct", PrimaryILOffset = 46, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe load is correct", PrimaryILOffset = 46, MethodILOffset = 0)]
    public static void Foo(byte[] value)
    {
      Contract.Requires(value != null);

      int sum = 0;

      fixed (byte* ptr = value)
      {
        for (int i = 0; i < value.Length; i++)
        {
          sum += (int)ptr[i];
        }
      }
    }

    // ok
    // [MAF] broken when I added bread crumbs. From a glance at the dfa output, looks like a bug in Stripes join
    // F: It works with the new analysis
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe load is correct", PrimaryILOffset = 55, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe load is correct", PrimaryILOffset = 55, MethodILOffset = 0)]
    public static void Bar0(int[] value, int i)
    {
      Contract.Requires(value != null);

      int sum = 0;

      fixed (int* ptr = value)
      {
        if (i >= 0)
        {
          if (i < value.Length)
          {
            sum += (int)ptr[i];
          }
        }
      }
    }

    // ok
    // [MAF] broken when I added bread crumbs. From a glance at the dfa output, looks like a bug in Stripes join
    // F: It works with the new analysis
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe load is correct", PrimaryILOffset = 49, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe load is correct", PrimaryILOffset = 49, MethodILOffset = 0)]
    public static void Bar1(int[] value)
    {
      Contract.Requires(value != null);

      int sum = 0;

      fixed (int* ptr = value)
      {
        for (int i = 0; i < value.Length; i++)
        {
          sum += (int)ptr[i];
        }
      }
    }

    // ok
    // [MAF] broken when I added bread crumbs. From a glance at the dfa output, looks like a bug in Stripes join
    // F: It works with the new analysis
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe load is correct", PrimaryILOffset = 50, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe load is correct", PrimaryILOffset = 50, MethodILOffset = 0)]
    public static void Bar2(long[] value)
    {
      Contract.Requires(value != null);

      long sum = 0;

      fixed (long* ptr = value)
      {
        for (int i = 0; i < value.Length; i++)
        {
          sum += (int)ptr[i];
        }
      }
    }
  }

  // TODO: add inference outcomes to regression checking
#if false
  public class FromSystemDrawing0
  {
    // Should infer the precondition : Contract.Requires(Contract.WritableBytes(pb) >= sizeof(byte));
    unsafe short GetShort(byte* pb)
    { 
      int num = 0;

      num = *(pb);

      return (short) num;
    }

    // ok
    unsafe short GetShort(int* pb)
    {
      Contract.Requires(Contract.WritableBytes(pb) >= sizeof(int));
      int num /*= 0*/;

      num = *(pb);

      return (short)num;
    }
  }
#endif

  public class FromSystemDrawing1
  {
    [StructLayout(LayoutKind.Sequential)]
    internal struct BITMAPINFO_FLAT
    {
      public int bmiHeader_biSize;
      public int bmiHeader_biWidth;
      public int bmiHeader_biHeight;
      public short bmiHeader_biPlanes;
      public short bmiHeader_biBitCount;
      public int bmiHeader_biCompression;
      public int bmiHeader_biSizeImage;
      public int bmiHeader_biXPelsPerMeter;
      public int bmiHeader_biYPelsPerMeter;
      public int bmiHeader_biClrUsed;
      public int bmiHeader_biClrImportant;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x400)]
      public byte[] bmiColors;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PALETTEENTRY
    {
      public byte peRed;
      public byte peGreen;
      public byte peBlue;
      public byte peFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct RGBQUAD
    {
      public byte rgbBlue;
      public byte rgbGreen;
      public byte rgbRed;
      public byte rgbReserved;
    }

    // ok
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 48, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 48, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 56, MethodILOffset = 0)]
    private unsafe bool bFillColorTable0(IntPtr hdc, uint num3, ref BITMAPINFO_FLAT pbmi)
    {
      byte[] lppe = new byte[sizeof(PALETTEENTRY) * 0x100];

      fixed (byte* numRef2 = lppe)
      {
        *(numRef2 + 2) = 12;
      }
      Contract.Assert(num3 > 1); // Why is this here?
      return false;
    }

    // ok
    // [MAF] broken when I added bread crumbs. From a glance at the dfa output, looks like a bug in Stripes join
    // F: It works with the new analysis
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 60, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 60, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 74, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 74, MethodILOffset = 0)]
    private unsafe bool bFillColorTable1(IntPtr hdc, uint num3, ref BITMAPINFO_FLAT pbmi)
    {
      byte[] lppe = new byte[sizeof(PALETTEENTRY) * 0x100];

      fixed (byte* numRef2 = lppe)
      {
        Contract.Assume(numRef2 != null);
        PALETTEENTRY* paletteentryPtr = (PALETTEENTRY*)numRef2;
        paletteentryPtr[0].peRed = 0;
        paletteentryPtr[1].peRed = 0;
      }

      return false;
    }

    // ok
    // [MAF] broken when I added bread crumbs. From a glance at the dfa output, looks like a bug in Stripes join
    // F: It works with the new analysis
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 85, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 85, MethodILOffset = 0)]
    private unsafe bool bFillColorTable2(IntPtr hdc, IntPtr hpal, int nEntries, ref BITMAPINFO_FLAT pbmi)
    {
      byte[] lppe = new byte[sizeof(PALETTEENTRY) * 0x100];
      {
        fixed (byte* numRef2 = lppe)
        {
          Contract.Assume(numRef2 != null);

          PALETTEENTRY* paletteentryPtr = (PALETTEENTRY*)numRef2;

          if (nEntries <= 0x100)
          {
            for (int i = 0; i < nEntries; i++)
            {
              paletteentryPtr[i].peRed = 0;
            }
          }
        }
      }
      return false;
    }

    // ok
    // [MAF] broken when I added bread crumbs. From a glance at the dfa output, looks like a bug in Stripes join
    // F: It works with the new analysis
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 100, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 100, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 119, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 119, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 138, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 138, MethodILOffset = 0)]
    private unsafe bool bFillColorTable3(IntPtr hdc, IntPtr hpal, ref BITMAPINFO_FLAT pbmi)
    {
      byte[] lppe = new byte[sizeof(PALETTEENTRY) * 0x100];
      {
        fixed (byte* numRef2 = lppe)
        {
          Contract.Assume(numRef2 != null);

          PALETTEENTRY* paletteentryPtr = (PALETTEENTRY*)numRef2;

          int nEntries = ((int)1) << pbmi.bmiHeader_biBitCount;

          if (nEntries <= 0x100)
          {
            for (int i = 0; i < nEntries; i++)
            {
              paletteentryPtr[i].peRed = 11;
              paletteentryPtr[i].peGreen = 12;
              paletteentryPtr[i].peGreen = 13;
            }
          }
        }
      }
      return false;
    }

    
    // [MAF] broken when I added bread crumbs. From a glance at the dfa output, looks like a bug in Stripes join
    // F: It works with the new analysis
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe load is correct", PrimaryILOffset = 170, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe load is correct", PrimaryILOffset = 170, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 175, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Unsafe memory store might be above the upper bound", PrimaryILOffset = 175, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe load is correct", PrimaryILOffset = 205, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe load is correct", PrimaryILOffset = 205, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 210, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 210, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe load is correct", PrimaryILOffset = 240, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe load is correct", PrimaryILOffset = 240, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 245, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 245, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 263, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 263, MethodILOffset = 0)]
    private unsafe bool bFillColorTable(IntPtr hdc, IntPtr hpal, uint num3, ref BITMAPINFO_FLAT pbmi)
    {
      // bool flag = false;
      byte[] lppe = new byte[sizeof(PALETTEENTRY) * 0x100];
      fixed (byte* numRef = pbmi.bmiColors)
      {
        Contract.Assume(numRef != null);

        fixed (byte* numRef2 = lppe)
        {
          Contract.Assume(numRef2 != null);

          RGBQUAD* rgbquadPtr = (RGBQUAD*)numRef;
          PALETTEENTRY* paletteentryPtr = (PALETTEENTRY*)numRef2;

          int nEntries = ((int)1) << pbmi.bmiHeader_biBitCount;

          if (nEntries <= 0x100)
          {
            // if (num3 != 0)
            {
              for (int i = 0; i < nEntries; i++)
              {
                rgbquadPtr[i].rgbRed = paletteentryPtr[i].peRed;
                rgbquadPtr[i].rgbGreen = paletteentryPtr[i].peGreen;
                rgbquadPtr[i].rgbBlue = paletteentryPtr[i].peBlue;
                rgbquadPtr[i].rgbReserved = 0;
              }
            }
          }
        }
      }
      return false;
    }
  }

  public unsafe class FromSystemDrawing2
  {
    public int* parameterValue;

    // ok
    // [MAF] broken when I added bread crumbs. From a glance at the dfa output, looks like a bug in Stripes join
    // F: It works with the new analysis
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe load is correct", PrimaryILOffset = 59, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe load is correct", PrimaryILOffset = 59, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 61, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Unsafe memory store might be above the upper bound", PrimaryILOffset = 61, MethodILOffset = 0)]
    public unsafe void EncoderParameter(long[] value)
    {
      Contract.Requires(value != null);
      int* parameterValue = (int*)this.parameterValue;
      fixed (long* numRef = value)
      {
        for (int i = 0; i < value.Length; i++)
        {
          parameterValue[i] = (int)numRef[i];
        }
      }
    }
  }

  namespace TestIntervals.Sizeof
  {
    public struct Header
    {
      public short tag;
      public short count;
    }

    unsafe class Program
    {
      [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 38, MethodILOffset = 0)]
      static void Initialize(byte[] buffer)
      {
        Contract.Requires(buffer != null);

        if (buffer.Length < sizeof(Header))
        {
          throw new Exception();
        }

        Contract.Assert(buffer.Length >= 1);
      }
    }
  }
}

namespace ReproFrom_StringBuilder_Vance
{
  public class Repro_ToString
  {
    public Repro_ToString m_ChunkPrevious;
    public char[] m_ChunkChars;
    public int m_ChunkLength;

#if WRITABLE_BYTES

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = (int)16, MethodILOffset = (int)75)]
    unsafe public String ToString_Francesco(bool b)
    {
      Repro_ToString chunk = this;
      do
      {
        if (chunk.m_ChunkLength > 0)
        {
          char[] sourceArray = chunk.m_ChunkChars;
          int chunkLength = chunk.m_ChunkLength;

          // Check that we will not overrun our boundaries. 
          if (/*(uint)*/chunkLength <= /*(uint)*/sourceArray.Length)
          {
            fixed (char* sourcePtr = sourceArray)
            {
              Contract.Assume(chunkLength <= sourceArray.Length); // F: should add the assume, as the join in subpolyhedra loses it. In a more general case, it can be proven by bounds
              Dummy(sourcePtr, chunkLength);
            }
          }

        }
        //chunk = chunk.m_ChunkPrevious;
      }
      while (b);

      return "";
    }


    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = (int)16, MethodILOffset = (int)52)]
    unsafe public String ToString_Francesco_0(bool b)
    {
      Repro_ToString chunk = this;
      do
      {
        // if (chunk.m_ChunkLength > 0)
        {
          char[] sourceArray = chunk.m_ChunkChars;
          int chunkLength = chunk.m_ChunkLength;

          // Check that we will not overrun our boundaries. 
          // if (/*(uint)*/chunkLength <= /*(uint)*/sourceArray.Length)
          {
            fixed (char* sourcePtr = sourceArray)
            {
              if (/*(uint)*/chunkLength <= /*(uint)*/sourceArray.Length)
                Dummy(sourcePtr, chunkLength);
            }
          }

        }
        // chunk = chunk.m_ChunkPrevious;
      }
      while (b);

      return "";
    }

    [Pure]
    unsafe void Dummy(char* buff, int count)
    {
      Contract.Requires(Contract.WritableBytes(buff) >= (ulong)count * sizeof(char));
    }
#endif

  }

  // There was an imprecision in the AssignInParallel
  public class Repro_Ctor
  {
#if WRITABLE_BYTES

    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 123)]
    unsafe public Repro_Ctor(String value, int startIndex, int length, int capacity)
    {
      if (capacity < 0)
      {
        throw new ArgumentOutOfRangeException("capacity", "");
      }
      if (length < 0)
      {
        throw new ArgumentOutOfRangeException("length", "");
      }
      if (startIndex < 0)
      {
        throw new ArgumentOutOfRangeException("startIndex", "");
      }
      if (startIndex > value.Length - length)
      {
        throw new ArgumentOutOfRangeException("length", "");
      }

      // This caused the bug, because of some imprecision in handling the AssignInParallel
      if (capacity < length)
        capacity = length;

      fixed (char* sourcePtr = value)
        ThreadSafeCopy(sourcePtr + startIndex, length);
    }

    unsafe private static void ThreadSafeCopy(char* sourcePtr, int count)
    {
      Contract.Requires(Contract.WritableBytes(sourcePtr) >= (uint)count * sizeof(char));

      // does nothing ...
    }
#endif
  }

  public class Repro_Remove
  {
    public int m_ChunkOffset, m_ChunkLength;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 84, MethodILOffset = 0)]
    unsafe public void Remove(int startIndex, int count)
    {
      int length = m_ChunkOffset + m_ChunkLength;

      if (count < 0)
      {
        throw new ArgumentOutOfRangeException("length", "");
      }

      if (startIndex < 0)
      {
        throw new ArgumentOutOfRangeException("startIndex", "");
      }

      if (count > length - startIndex)
      { 
        throw new ArgumentOutOfRangeException("index", "");
      } 

      if (count > 0)
      {
 //       Contract.Assert(length == m_ChunkOffset + m_ChunkLength);   // Ok, we can prove it
 //       Contract.Assert(count + startIndex <= length);              // Ok, we can prove it

        Contract.Assert(startIndex < length);

//        Ghost_Remove(startIndex, count);
      }
      
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 61, MethodILOffset = 0)]
    unsafe public void Remov_Mini(int startIndex, int count)
    {
      Contract.Requires(count >= 0);
      Contract.Requires(startIndex >= 0);

      int length = Length;

      if (count > length - startIndex)
      {
        throw new ArgumentOutOfRangeException("index", "");
      }

      if (count > 0)
      {
        Contract.Assert(startIndex < length);
        //        Ghost_Remove(startIndex, count);
      }

    }

    unsafe private void Ghost_Remove(int startIndex, int count)
    {
      Contract.Requires(startIndex < Length);
    }

    public int Length
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() == m_ChunkOffset + m_ChunkLength);    // f: this should be inferred by Clousot
        
        return m_ChunkOffset + m_ChunkLength;
      }
    }
  }

  public class Repro_ReplaceAll
  {
    public int m_ChunkOffset;
    public Repro_ReplaceAll m_ChunkPrevious;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 66, MethodILOffset = 0)]
    unsafe private void Remove(int startIndex, int count, out Repro_ReplaceAll chunk, int indexInChunk)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(count >= 0);    

      int endIndex = startIndex + count;

      chunk = this;
      Repro_ReplaceAll endChunk = null;
      int endIndexInChunk = 0;

      for (; ; )
      {
        if (endIndex - chunk.m_ChunkOffset >= 0)
        {
          // if (endChunk == null)
          {
            endChunk = chunk;
            endIndexInChunk = endIndex - endChunk.m_ChunkOffset;

            Contract.Assert(endIndexInChunk >= 0); 
          }

        }
        else
        {
          chunk.m_ChunkOffset -= count;
        }
        chunk = chunk.m_ChunkPrevious;
      }

      // Contract.Assert(endIndexInChunk >= 0);
    }
  }
}

namespace ExamplesFromPapers
{
  class SomePLDISubmission
  {
#if WRITABLE_BYTES
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Localloc size ok", PrimaryILOffset = 46, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe load is correct",PrimaryILOffset=61,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The upper bound of the unsafe load is correct",PrimaryILOffset=61,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe store is correct",PrimaryILOffset=77,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The upper bound of the unsafe store is correct",PrimaryILOffset=77,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe load is correct",PrimaryILOffset=94,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The upper bound of the unsafe load is correct",PrimaryILOffset=94,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe store is correct",PrimaryILOffset=95,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The upper bound of the unsafe store is correct",PrimaryILOffset=95,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe store is correct",PrimaryILOffset=131,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The upper bound of the unsafe store is correct",PrimaryILOffset=131,MethodILOffset=0)]
    unsafe char* tosunds(char* str, int len, int n)
    {
      Contract.Requires(len > 0);
      Contract.Requires(n >= 2 * len);
      Contract.Requires(Contract.WritableBytes(str) == (ulong)(sizeof(char) * len));

      //int  n = len *2;
      char* buf = stackalloc char[n * sizeof(char)];

      int j = 0;
      for (int i = 0; i < len; i++)
      {
        if (char.IsUpper(str[i]))
        {
          buf[j] = '%';
          j++;
        }
        buf[j] = str[i];
        j++;

        if (j >= n - 1)
          break;
      }

      if (j + 1 >= n)
      {
        j = n - 1;
      }

      buf[j] = '\0';

      return buf;
    }
#endif
  }

}
