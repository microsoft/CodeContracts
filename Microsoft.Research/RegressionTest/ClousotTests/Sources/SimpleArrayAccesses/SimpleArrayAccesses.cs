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
using Microsoft.Research.ClousotRegression;
//using Microsoft.Research.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Bounds_Test
{
  // Simple array accesses
  class DirectArrayAccesses
  {
    // Two warnings
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "The length of the array may be negative", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 10, MethodILOffset = 0)]
    int ArrayTestForLengthOk(int l)
    {
      int[] a = new int[l];
      return a[12];
    }

    // One warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 3, MethodILOffset = 0)]
    int ArrayTestForLengthNotOk(int[] a)
    {
      return a[12];
    }

    // Overflow
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"Cannot create an array of negative length",PrimaryILOffset=16,MethodILOffset=0)]
    public int[] NewHugeArray(int x)
    {
      Contract.Requires(x == 1);

      return new int[Int32.MaxValue + x]; // Definitely an error
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Array creation : ok",PrimaryILOffset=16,MethodILOffset=0)]
    public int[] NewHugeArray_ok(int x)
    {
      Contract.Requires(x == 1);

      return new int[Int32.MaxValue - x];
    }

    // One error, one warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "Array access IS below the lower bound", PrimaryILOffset = 5, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5, MethodILOffset = 0)]
    int ArrayTest0(int[] a)
    {
      int i = -12;
      return a[i];
    }

    // Two warnings
    [ClousotRegressionTest]       
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be below the lower bound", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 2, MethodILOffset = 0)]
    char ArrayTest1(char[] cs, int i)
    {
      return cs[i];
    }

    // One warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound. The static checker determined that the condition 'index < booleans.Length' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(index < booleans.Length);", PrimaryILOffset = 6, MethodILOffset = 0)]
    bool ArrayTest2(bool[] booleans, int index)
    {
      if (index >= 0)
        return booleans[index];
      else
        return false;
    }

    // Two warnings
    [ClousotRegressionTest]        
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be below the lower bound. The static checker determined that the condition '0 <= x' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(0 <= x);", PrimaryILOffset = 6, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound. The static checker determined that the condition 'x < arr.Length' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(x < arr.Length);", PrimaryILOffset = 6, MethodILOffset = 0)]
    void ArrayTest3(int x, bool[] arr)
    {
      if (x != 0)
      {
        arr[x] = false;
      }
    }

    // Two warnings
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 15, MethodILOffset = 0)]
    void ArrayTest4(string[] a)
    {
      a[0] = "12";
      a[1] = "15";
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 15, MethodILOffset = 0)]
    void ArrayTest5(string[] a)
    {
      a[1] = "15";
      a[0] = "12"; // second upper bound okay
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 5, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 14, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 14, MethodILOffset = 0)]
    bool NewArray0()
    {
      bool[] b;
      b = new bool[128];

      return b[12];
    }

    // One error
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "Array access IS above the upper bound", PrimaryILOffset = 11, MethodILOffset = 0)]
    void NewArray1(int[] a)
    {
      int len = a.Length;
      int z = len + 12;
      int wrong = a[z];
    }

    // Ok
    [ClousotRegressionTest]       
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 6, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 17, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 17, MethodILOffset = 0)]
    void NewArray2(int l)
    {
      if (l <= 0)
      {
        return;
      }

      bool[] b = new bool[l];     // here must be correct
      b[l - 1] = true;
    }

    // Two errors
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"The length of the array may be negative (dimension 0)", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"The length of the array may be negative (dimension 1)", PrimaryILOffset = 2, MethodILOffset = 0)]
    bool[,] NewArray3(int n, int m)
    {
      bool[,] b = new bool[n, m];
      return b;
    }

    // Two errors
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"Cannot create an array of negative length (dimension 0)", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"Cannot create an array of negative length (dimension 1)", PrimaryILOffset = 18, MethodILOffset = 0)]
    bool[,] NewArray4(int n, int m)
    {
      Contract.Requires(n < 0 && m < 0);

      bool[,] b = new bool[n, m];
      return b;
    }
 }

  class DirectArrayAccessesWithUInts
  {
    // Two warnings
    [ClousotRegressionTest]       
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be below the lower bound", PrimaryILOffset = 7, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 7, MethodILOffset = 0)]
    private void UIntTest0(int[] z, int x)
    {
      z[x] = 233;
    }

    // One warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 5, MethodILOffset = 0)]
    private void UIntTest1(int[] z, uint x)
    {
      z[x] = 109;
    }

    // One warning
    [ClousotRegressionTest]   
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 8, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 8, MethodILOffset = 0)]
    private void UIntTest2(int[] z, uint x)
    {
      z[x + 12] = 109;
    }

    // One warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 10, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 10, MethodILOffset = 0)]
    private void UIntTest3(int[] z, uint x)
    {
      uint tmp = x + 12;
      z[tmp] = 109;
    }
  }

  class ArrayTraversingWithFor
  {
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 8, MethodILOffset = 0)]
    public void GoForward0(int[] arr)
    {
      for (int i = 0; i < arr.Length; i++)
      {
        arr[i] = 100;
      }
    }

    // One warning
    [ClousotRegressionTest] [
    RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 10, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 10, MethodILOffset = 0)]
    public void GoForward1(int[] arr)
    {
      for (int i = 0; i < 6; i = i + 1)
      {
        arr[i + 2] = 12;
      }
    }

    // One warning
    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 9, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 9, MethodILOffset = 0)]
    public void GoForward2(int[] arr)
    {
      byte index;
      for (index = 0; index < 6; index = (byte)(index + 1))
      {
        arr[2 + index] = 1;
      }
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 8, MethodILOffset = 0)]
    public void GoForward3(int[] arr)
    {
      short s;
      for (s = 0; s < arr.Length; s = (short)(s + 1))
      {
        arr[s] = 123;
      }
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 5, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 19, MethodILOffset = 0)]
    public void GoForward4()
    {
      int[] a = new int[256];

      for (int i = 0; i < 128; i++)
      {
        a[i] = -11;   // Must prove them correct
      }
    }

    // One warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 7, MethodILOffset = 0)]
    public void GoBackwards0(int[] x, int l)
    {
      for (int i = l; i >= 0; i--)
      {
        x[i] = 0;
      }
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 15, MethodILOffset = 0)]
    public void GoBackwards1(int[] x)
    {
      for (int i = x.Length - 1; i >= 0; i--)
      {
        x[i] = 888;
      }
    }
  }

  class ArrayNotEqual
  {
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 32, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 32, MethodILOffset = 0)]
    string GetDefaultMemberName(string[] customAttributes)
    {
      string memberName = "pippo";

      if (customAttributes.Length > 1)
      {
        throw new System.Exception("ciccio");
      }

      if (customAttributes.Length == 0)
      {
        return null;
      }

      // infers customAttributes.Length == 1
      memberName = customAttributes[0];

      return memberName;
    }
  }

  class ArrayAccessesWithShiftsAndModulos
  {
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 10, MethodILOffset = 0)]
    public void ArrayWithShift0(int[] a)
    {
      for (int i = 0; i < a.Length; i++)
      {
        a[i >> 2] = 12;
      }
    }

    // one warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 10, MethodILOffset = 0)]
    public void ArrayWithShift(int[] a)
    {
      for (int i = 0; i < a.Length; i++)
      {
        a[i << 2] = 11;
      }
    }
     
    // One warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be below the lower bound", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 8, MethodILOffset = 0)]
    public bool ArrayWithModulus(bool[] arr, int index)
    {
      int c = index % arr.Length;
      return arr[c];
    }

    // One warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 16, MethodILOffset = 0)]
    public bool ArrayWithModulusWithCheckOk0(bool[] arr, int index)
    {
      if (index < 0)
        index = -index;

      int c = index % arr.Length;

      return arr[c];
    }

  }

  class ArrayWithTwoVariables
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 27, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 27, MethodILOffset = 0)]
    public void ArrayWithTwoVariables0(int[] binaryForm, int offset)
    {
      if (offset < 0)
        throw new Exception();

      if (binaryForm.Length < offset + 1)
        throw new Exception();

      binaryForm[offset + 0] = 0;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=30,MethodILOffset = 0)]
    public void ArrayWithTwoVariables1(int[] binaryForm, int offset)
    {
      if(offset < 0)
        throw new Exception();

      if (binaryForm.Length - offset < 20)
        throw new Exception();

      binaryForm[offset + 5] = 0; 
    }

    [ClousotRegressionTest("cci1only")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 36)]
    public int[] TestKeepOldLength(int[] a, bool b)
    {
      Contract.Ensures(Contract.Result<int[]>().Length >= a.Length);

      if (b) {
        a = new int[a.Length + 1];
      } // at this join point, we still have a name for a.Length, as we keep a shadow copy of the original parameters.
      return a;
    }
  }

  // The reason this test is there is because this is some "sick" method from mscorlib.dll, and we do not want Clousot to take too much time in it
  class LongArrayAccesses
  {
    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 25, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 25, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 29, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 29, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 33, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 33, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 37, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 37, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 41, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 41, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 63, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 63, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 102, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 102, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 143, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 143, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 181, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 181, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 222, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 222, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 259, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 259, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 297, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 297, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 337, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 337, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 375, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 375, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 417, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 417, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 456, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 456, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 496, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 496, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 538, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 538, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 576, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 576, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 617, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 617, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 656, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 656, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 702, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 702, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 749, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 749, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 793, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 793, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 839, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 839, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 883, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 883, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 928, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 928, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 977, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 977, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1020, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1020, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1068, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1068, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1110, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1110, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1156, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1156, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1204, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1204, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1248, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1248, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1296, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1296, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1339, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1339, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1384, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1384, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1432, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1432, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1477, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1477, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1525, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1525, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1567, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1567, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1612, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1612, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1661, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1661, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1705, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1705, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1752, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1752, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1795, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1795, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1840, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1840, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1887, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1887, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1931, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1931, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1978, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1978, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2021, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2021, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2066, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2066, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2114, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2114, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2157, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2157, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2205, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2205, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2249, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2249, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2295, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2295, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2343, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2343, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2387, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2387, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2435, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2435, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2478, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2478, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2523, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2523, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2571, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2571, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2615, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2615, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2662, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2662, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2705, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2705, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2749, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2749, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2796, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2796, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2839, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2839, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2886, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2886, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2929, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2929, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2974, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2974, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3022, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3022, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3066, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3066, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3113, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3113, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3155, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3155, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3201, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3201, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3250, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3250, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3293, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3293, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3340, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3340, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3383, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3383, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3429, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3429, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3477, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3477, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3521, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3521, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3568, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3568, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3614, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3614, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3669, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3669, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3724, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3724, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3779, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3779, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3835, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3835, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3890, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3890, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3946, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3946, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4001, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4001, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4056, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4056, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4110, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4110, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4165, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4165, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4219, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4219, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4274, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4274, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4330, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4330, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4385, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4385, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4441, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4441, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4495, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4495, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4551, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4551, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4606, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4606, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4661, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4661, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4715, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4715, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4771, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4771, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4825, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4825, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4881, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4881, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4937, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4937, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4992, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4992, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5046, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5046, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5102, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5102, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5156, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5156, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5211, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5211, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5266, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5266, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5321, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5321, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5377, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5377, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5432, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5432, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5486, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5486, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5541, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5541, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5596, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5596, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5651, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5651, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5705, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5705, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5760, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5760, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5816, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5816, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5871, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5871, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5927, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5927, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 5981, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 5981, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6037, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6037, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6092, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6092, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6147, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6147, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6202, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6202, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6256, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6256, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6311, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6311, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6365, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6365, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6419, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6419, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6474, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6474, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6530, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6530, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6586, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6586, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6640, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6640, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6695, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6695, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6750, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6750, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6805, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6805, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6861, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6861, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6917, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6917, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 6972, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 6972, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7027, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7027, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7083, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7083, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7138, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7138, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7187, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7187, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7236, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7236, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7285, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7285, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7334, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7334, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7383, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7383, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7431, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7431, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7480, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7480, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7528, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7528, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7576, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7576, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7626, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7626, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7675, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7675, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7723, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7723, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7772, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7772, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7822, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7822, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7872, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7872, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7911, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7911, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7920, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7920, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7926, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7926, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7931, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7931, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7938, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7938, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7943, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7943, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7949, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7949, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7954, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7954, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7960, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7960, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 7965, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 7965, MethodILOffset = 0)]
    private static void MDTransform(uint[] blockDWords, uint[] state, byte[] block)
    {
      Contract.Requires(blockDWords.Length > 15);
      Contract.Requires(state.Length > 5);

      uint x = state[0];
      uint num2 = state[1];
      uint y = state[2];
      uint z = state[3];
      uint num5 = state[4];
      uint num6 = x;
      uint num7 = num2;
      uint num8 = y;
      uint num9 = z;
      uint num10 = num5;

      x += blockDWords[0] + F(num2, y, z);
      x = ((x << 11) | (x >> 0x15)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += blockDWords[1] + F(x, num2, y);
      num5 = ((num5 << 14) | (num5 >> 0x12)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += blockDWords[2] + F(num5, x, num2);
      z = ((z << 15) | (z >> 0x11)) + y;
      x = (x << 10) | (x >> 0x16);
      y += blockDWords[3] + F(z, num5, x);
      y = ((y << 12) | (y >> 20)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += blockDWords[4] + F(y, z, num5);
      num2 = ((num2 << 5) | (num2 >> 0x1b)) + x;
      z = (z << 10) | (z >> 0x16);
      x += blockDWords[5] + F(num2, y, z);
      x = ((x << 8) | (x >> 0x18)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += blockDWords[6] + F(x, num2, y);
      num5 = ((num5 << 7) | (num5 >> 0x19)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += blockDWords[7] + F(num5, x, num2);
      z = ((z << 9) | (z >> 0x17)) + y;
      x = (x << 10) | (x >> 0x16);
      y += blockDWords[8] + F(z, num5, x);
      y = ((y << 11) | (y >> 0x15)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += blockDWords[9] + F(y, z, num5);
      num2 = ((num2 << 13) | (num2 >> 0x13)) + x;
      z = (z << 10) | (z >> 0x16);
      x += blockDWords[10] + F(num2, y, z);
      x = ((x << 14) | (x >> 0x12)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += blockDWords[11] + F(x, num2, y);
      num5 = ((num5 << 15) | (num5 >> 0x11)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += blockDWords[12] + F(num5, x, num2);
      z = ((z << 6) | (z >> 0x1a)) + y;
      x = (x << 10) | (x >> 0x16);
      y += blockDWords[13] + F(z, num5, x);
      y = ((y << 7) | (y >> 0x19)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += blockDWords[14] + F(y, z, num5);
      num2 = ((num2 << 9) | (num2 >> 0x17)) + x;
      z = (z << 10) | (z >> 0x16);
      x += blockDWords[15] + F(num2, y, z);
      x = ((x << 8) | (x >> 0x18)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (G(x, num2, y) + blockDWords[7]) + 0x5a827999;
      num5 = ((num5 << 7) | (num5 >> 0x19)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (G(num5, x, num2) + blockDWords[4]) + 0x5a827999;
      z = ((z << 6) | (z >> 0x1a)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (G(z, num5, x) + blockDWords[13]) + 0x5a827999;
      y = ((y << 8) | (y >> 0x18)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (G(y, z, num5) + blockDWords[1]) + 0x5a827999;
      num2 = ((num2 << 13) | (num2 >> 0x13)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (G(num2, y, z) + blockDWords[10]) + 0x5a827999;
      x = ((x << 11) | (x >> 0x15)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (G(x, num2, y) + blockDWords[6]) + 0x5a827999;
      num5 = ((num5 << 9) | (num5 >> 0x17)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (G(num5, x, num2) + blockDWords[15]) + 0x5a827999;
      z = ((z << 7) | (z >> 0x19)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (G(z, num5, x) + blockDWords[3]) + 0x5a827999;
      y = ((y << 15) | (y >> 0x11)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (G(y, z, num5) + blockDWords[12]) + 0x5a827999;
      num2 = ((num2 << 7) | (num2 >> 0x19)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (G(num2, y, z) + blockDWords[0]) + 0x5a827999;
      x = ((x << 12) | (x >> 20)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (G(x, num2, y) + blockDWords[9]) + 0x5a827999;
      num5 = ((num5 << 15) | (num5 >> 0x11)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (G(num5, x, num2) + blockDWords[5]) + 0x5a827999;
      z = ((z << 9) | (z >> 0x17)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (G(z, num5, x) + blockDWords[2]) + 0x5a827999;
      y = ((y << 11) | (y >> 0x15)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (G(y, z, num5) + blockDWords[14]) + 0x5a827999;
      num2 = ((num2 << 7) | (num2 >> 0x19)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (G(num2, y, z) + blockDWords[11]) + 0x5a827999;
      x = ((x << 13) | (x >> 0x13)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (G(x, num2, y) + blockDWords[8]) + 0x5a827999;
      num5 = ((num5 << 12) | (num5 >> 20)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (H(num5, x, num2) + blockDWords[3]) + 0x6ed9eba1;
      z = ((z << 11) | (z >> 0x15)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (H(z, num5, x) + blockDWords[10]) + 0x6ed9eba1;
      y = ((y << 13) | (y >> 0x13)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (H(y, z, num5) + blockDWords[14]) + 0x6ed9eba1;
      num2 = ((num2 << 6) | (num2 >> 0x1a)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (H(num2, y, z) + blockDWords[4]) + 0x6ed9eba1;
      x = ((x << 7) | (x >> 0x19)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (H(x, num2, y) + blockDWords[9]) + 0x6ed9eba1;
      num5 = ((num5 << 14) | (num5 >> 0x12)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (H(num5, x, num2) + blockDWords[15]) + 0x6ed9eba1;
      z = ((z << 9) | (z >> 0x17)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (H(z, num5, x) + blockDWords[8]) + 0x6ed9eba1;
      y = ((y << 13) | (y >> 0x13)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (H(y, z, num5) + blockDWords[1]) + 0x6ed9eba1;
      num2 = ((num2 << 15) | (num2 >> 0x11)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (H(num2, y, z) + blockDWords[2]) + 0x6ed9eba1;
      x = ((x << 14) | (x >> 0x12)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (H(x, num2, y) + blockDWords[7]) + 0x6ed9eba1;
      num5 = ((num5 << 8) | (num5 >> 0x18)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (H(num5, x, num2) + blockDWords[0]) + 0x6ed9eba1;
      z = ((z << 13) | (z >> 0x13)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (H(z, num5, x) + blockDWords[6]) + 0x6ed9eba1;
      y = ((y << 6) | (y >> 0x1a)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (H(y, z, num5) + blockDWords[13]) + 0x6ed9eba1;
      num2 = ((num2 << 5) | (num2 >> 0x1b)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (H(num2, y, z) + blockDWords[11]) + 0x6ed9eba1;
      x = ((x << 12) | (x >> 20)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (H(x, num2, y) + blockDWords[5]) + 0x6ed9eba1;
      num5 = ((num5 << 7) | (num5 >> 0x19)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (H(num5, x, num2) + blockDWords[12]) + 0x6ed9eba1;
      z = ((z << 5) | (z >> 0x1b)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (I(z, num5, x) + blockDWords[1]) + 0x8f1bbcdc;
      y = ((y << 11) | (y >> 0x15)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (I(y, z, num5) + blockDWords[9]) + 0x8f1bbcdc;
      num2 = ((num2 << 12) | (num2 >> 20)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (I(num2, y, z) + blockDWords[11]) + 0x8f1bbcdc;
      x = ((x << 14) | (x >> 0x12)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (I(x, num2, y) + blockDWords[10]) + 0x8f1bbcdc;
      num5 = ((num5 << 15) | (num5 >> 0x11)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (I(num5, x, num2) + blockDWords[0]) + 0x8f1bbcdc;
      z = ((z << 14) | (z >> 0x12)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (I(z, num5, x) + blockDWords[8]) + 0x8f1bbcdc;
      y = ((y << 15) | (y >> 0x11)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (I(y, z, num5) + blockDWords[12]) + 0x8f1bbcdc;
      num2 = ((num2 << 9) | (num2 >> 0x17)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (I(num2, y, z) + blockDWords[4]) + 0x8f1bbcdc;
      x = ((x << 8) | (x >> 0x18)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (I(x, num2, y) + blockDWords[13]) + 0x8f1bbcdc;
      num5 = ((num5 << 9) | (num5 >> 0x17)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (I(num5, x, num2) + blockDWords[3]) + 0x8f1bbcdc;
      z = ((z << 14) | (z >> 0x12)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (I(z, num5, x) + blockDWords[7]) + 0x8f1bbcdc;
      y = ((y << 5) | (y >> 0x1b)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (I(y, z, num5) + blockDWords[15]) + 0x8f1bbcdc;
      num2 = ((num2 << 6) | (num2 >> 0x1a)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (I(num2, y, z) + blockDWords[14]) + 0x8f1bbcdc;
      x = ((x << 8) | (x >> 0x18)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (I(x, num2, y) + blockDWords[5]) + 0x8f1bbcdc;
      num5 = ((num5 << 6) | (num5 >> 0x1a)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (I(num5, x, num2) + blockDWords[6]) + 0x8f1bbcdc;
      z = ((z << 5) | (z >> 0x1b)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (I(z, num5, x) + blockDWords[2]) + 0x8f1bbcdc;
      y = ((y << 12) | (y >> 20)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (J(y, z, num5) + blockDWords[4]) + 0xa953fd4e;
      num2 = ((num2 << 9) | (num2 >> 0x17)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (J(num2, y, z) + blockDWords[0]) + 0xa953fd4e;
      x = ((x << 15) | (x >> 0x11)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (J(x, num2, y) + blockDWords[5]) + 0xa953fd4e;
      num5 = ((num5 << 5) | (num5 >> 0x1b)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (J(num5, x, num2) + blockDWords[9]) + 0xa953fd4e;
      z = ((z << 11) | (z >> 0x15)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (J(z, num5, x) + blockDWords[7]) + 0xa953fd4e;
      y = ((y << 6) | (y >> 0x1a)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (J(y, z, num5) + blockDWords[12]) + 0xa953fd4e;
      num2 = ((num2 << 8) | (num2 >> 0x18)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (J(num2, y, z) + blockDWords[2]) + 0xa953fd4e;
      x = ((x << 13) | (x >> 0x13)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (J(x, num2, y) + blockDWords[10]) + 0xa953fd4e;
      num5 = ((num5 << 12) | (num5 >> 20)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (J(num5, x, num2) + blockDWords[14]) + 0xa953fd4e;
      z = ((z << 5) | (z >> 0x1b)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (J(z, num5, x) + blockDWords[1]) + 0xa953fd4e;
      y = ((y << 12) | (y >> 20)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (J(y, z, num5) + blockDWords[3]) + 0xa953fd4e;
      num2 = ((num2 << 13) | (num2 >> 0x13)) + x;
      z = (z << 10) | (z >> 0x16);
      x += (J(num2, y, z) + blockDWords[8]) + 0xa953fd4e;
      x = ((x << 14) | (x >> 0x12)) + num5;
      y = (y << 10) | (y >> 0x16);
      num5 += (J(x, num2, y) + blockDWords[11]) + 0xa953fd4e;
      num5 = ((num5 << 11) | (num5 >> 0x15)) + z;
      num2 = (num2 << 10) | (num2 >> 0x16);
      z += (J(num5, x, num2) + blockDWords[6]) + 0xa953fd4e;
      z = ((z << 8) | (z >> 0x18)) + y;
      x = (x << 10) | (x >> 0x16);
      y += (J(z, num5, x) + blockDWords[15]) + 0xa953fd4e;
      y = ((y << 5) | (y >> 0x1b)) + num2;
      num5 = (num5 << 10) | (num5 >> 0x16);
      num2 += (J(y, z, num5) + blockDWords[13]) + 0xa953fd4e;
      num2 = ((num2 << 6) | (num2 >> 0x1a)) + x;
      z = (z << 10) | (z >> 0x16);
      num6 += (J(num7, num8, num9) + blockDWords[5]) + 0x50a28be6;
      num6 = ((num6 << 8) | (num6 >> 0x18)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (J(num6, num7, num8) + blockDWords[14]) + 0x50a28be6;
      num10 = ((num10 << 9) | (num10 >> 0x17)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (J(num10, num6, num7) + blockDWords[7]) + 0x50a28be6;
      num9 = ((num9 << 9) | (num9 >> 0x17)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (J(num9, num10, num6) + blockDWords[0]) + 0x50a28be6;
      num8 = ((num8 << 11) | (num8 >> 0x15)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (J(num8, num9, num10) + blockDWords[9]) + 0x50a28be6;
      num7 = ((num7 << 13) | (num7 >> 0x13)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (J(num7, num8, num9) + blockDWords[2]) + 0x50a28be6;
      num6 = ((num6 << 15) | (num6 >> 0x11)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (J(num6, num7, num8) + blockDWords[11]) + 0x50a28be6;
      num10 = ((num10 << 15) | (num10 >> 0x11)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (J(num10, num6, num7) + blockDWords[4]) + 0x50a28be6;
      num9 = ((num9 << 5) | (num9 >> 0x1b)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (J(num9, num10, num6) + blockDWords[13]) + 0x50a28be6;
      num8 = ((num8 << 7) | (num8 >> 0x19)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (J(num8, num9, num10) + blockDWords[6]) + 0x50a28be6;
      num7 = ((num7 << 7) | (num7 >> 0x19)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (J(num7, num8, num9) + blockDWords[15]) + 0x50a28be6;
      num6 = ((num6 << 8) | (num6 >> 0x18)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (J(num6, num7, num8) + blockDWords[8]) + 0x50a28be6;
      num10 = ((num10 << 11) | (num10 >> 0x15)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (J(num10, num6, num7) + blockDWords[1]) + 0x50a28be6;
      num9 = ((num9 << 14) | (num9 >> 0x12)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (J(num9, num10, num6) + blockDWords[10]) + 0x50a28be6;
      num8 = ((num8 << 14) | (num8 >> 0x12)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (J(num8, num9, num10) + blockDWords[3]) + 0x50a28be6;
      num7 = ((num7 << 12) | (num7 >> 20)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (J(num7, num8, num9) + blockDWords[12]) + 0x50a28be6;
      num6 = ((num6 << 6) | (num6 >> 0x1a)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (I(num6, num7, num8) + blockDWords[6]) + 0x5c4dd124;
      num10 = ((num10 << 9) | (num10 >> 0x17)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (I(num10, num6, num7) + blockDWords[11]) + 0x5c4dd124;
      num9 = ((num9 << 13) | (num9 >> 0x13)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (I(num9, num10, num6) + blockDWords[3]) + 0x5c4dd124;
      num8 = ((num8 << 15) | (num8 >> 0x11)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (I(num8, num9, num10) + blockDWords[7]) + 0x5c4dd124;
      num7 = ((num7 << 7) | (num7 >> 0x19)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (I(num7, num8, num9) + blockDWords[0]) + 0x5c4dd124;
      num6 = ((num6 << 12) | (num6 >> 20)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (I(num6, num7, num8) + blockDWords[13]) + 0x5c4dd124;
      num10 = ((num10 << 8) | (num10 >> 0x18)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (I(num10, num6, num7) + blockDWords[5]) + 0x5c4dd124;
      num9 = ((num9 << 9) | (num9 >> 0x17)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (I(num9, num10, num6) + blockDWords[10]) + 0x5c4dd124;
      num8 = ((num8 << 11) | (num8 >> 0x15)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (I(num8, num9, num10) + blockDWords[14]) + 0x5c4dd124;
      num7 = ((num7 << 7) | (num7 >> 0x19)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (I(num7, num8, num9) + blockDWords[15]) + 0x5c4dd124;
      num6 = ((num6 << 7) | (num6 >> 0x19)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (I(num6, num7, num8) + blockDWords[8]) + 0x5c4dd124;
      num10 = ((num10 << 12) | (num10 >> 20)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (I(num10, num6, num7) + blockDWords[12]) + 0x5c4dd124;
      num9 = ((num9 << 7) | (num9 >> 0x19)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (I(num9, num10, num6) + blockDWords[4]) + 0x5c4dd124;
      num8 = ((num8 << 6) | (num8 >> 0x1a)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (I(num8, num9, num10) + blockDWords[9]) + 0x5c4dd124;
      num7 = ((num7 << 15) | (num7 >> 0x11)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (I(num7, num8, num9) + blockDWords[1]) + 0x5c4dd124;
      num6 = ((num6 << 13) | (num6 >> 0x13)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (I(num6, num7, num8) + blockDWords[2]) + 0x5c4dd124;
      num10 = ((num10 << 11) | (num10 >> 0x15)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (H(num10, num6, num7) + blockDWords[15]) + 0x6d703ef3;
      num9 = ((num9 << 9) | (num9 >> 0x17)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (H(num9, num10, num6) + blockDWords[5]) + 0x6d703ef3;
      num8 = ((num8 << 7) | (num8 >> 0x19)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (H(num8, num9, num10) + blockDWords[1]) + 0x6d703ef3;
      num7 = ((num7 << 15) | (num7 >> 0x11)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (H(num7, num8, num9) + blockDWords[3]) + 0x6d703ef3;
      num6 = ((num6 << 11) | (num6 >> 0x15)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (H(num6, num7, num8) + blockDWords[7]) + 0x6d703ef3;
      num10 = ((num10 << 8) | (num10 >> 0x18)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (H(num10, num6, num7) + blockDWords[14]) + 0x6d703ef3;
      num9 = ((num9 << 6) | (num9 >> 0x1a)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (H(num9, num10, num6) + blockDWords[6]) + 0x6d703ef3;
      num8 = ((num8 << 6) | (num8 >> 0x1a)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (H(num8, num9, num10) + blockDWords[9]) + 0x6d703ef3;
      num7 = ((num7 << 14) | (num7 >> 0x12)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (H(num7, num8, num9) + blockDWords[11]) + 0x6d703ef3;
      num6 = ((num6 << 12) | (num6 >> 20)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (H(num6, num7, num8) + blockDWords[8]) + 0x6d703ef3;
      num10 = ((num10 << 13) | (num10 >> 0x13)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (H(num10, num6, num7) + blockDWords[12]) + 0x6d703ef3;
      num9 = ((num9 << 5) | (num9 >> 0x1b)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (H(num9, num10, num6) + blockDWords[2]) + 0x6d703ef3;
      num8 = ((num8 << 14) | (num8 >> 0x12)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (H(num8, num9, num10) + blockDWords[10]) + 0x6d703ef3;
      num7 = ((num7 << 13) | (num7 >> 0x13)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (H(num7, num8, num9) + blockDWords[0]) + 0x6d703ef3;
      num6 = ((num6 << 13) | (num6 >> 0x13)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (H(num6, num7, num8) + blockDWords[4]) + 0x6d703ef3;
      num10 = ((num10 << 7) | (num10 >> 0x19)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (H(num10, num6, num7) + blockDWords[13]) + 0x6d703ef3;
      num9 = ((num9 << 5) | (num9 >> 0x1b)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (G(num9, num10, num6) + blockDWords[8]) + 0x7a6d76e9;
      num8 = ((num8 << 15) | (num8 >> 0x11)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (G(num8, num9, num10) + blockDWords[6]) + 0x7a6d76e9;
      num7 = ((num7 << 5) | (num7 >> 0x1b)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (G(num7, num8, num9) + blockDWords[4]) + 0x7a6d76e9;
      num6 = ((num6 << 8) | (num6 >> 0x18)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (G(num6, num7, num8) + blockDWords[1]) + 0x7a6d76e9;
      num10 = ((num10 << 11) | (num10 >> 0x15)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (G(num10, num6, num7) + blockDWords[3]) + 0x7a6d76e9;
      num9 = ((num9 << 14) | (num9 >> 0x12)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (G(num9, num10, num6) + blockDWords[11]) + 0x7a6d76e9;
      num8 = ((num8 << 14) | (num8 >> 0x12)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (G(num8, num9, num10) + blockDWords[15]) + 0x7a6d76e9;
      num7 = ((num7 << 6) | (num7 >> 0x1a)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (G(num7, num8, num9) + blockDWords[0]) + 0x7a6d76e9;
      num6 = ((num6 << 14) | (num6 >> 0x12)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (G(num6, num7, num8) + blockDWords[5]) + 0x7a6d76e9;
      num10 = ((num10 << 6) | (num10 >> 0x1a)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (G(num10, num6, num7) + blockDWords[12]) + 0x7a6d76e9;
      num9 = ((num9 << 9) | (num9 >> 0x17)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (G(num9, num10, num6) + blockDWords[2]) + 0x7a6d76e9;
      num8 = ((num8 << 12) | (num8 >> 20)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += (G(num8, num9, num10) + blockDWords[13]) + 0x7a6d76e9;
      num7 = ((num7 << 9) | (num7 >> 0x17)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += (G(num7, num8, num9) + blockDWords[9]) + 0x7a6d76e9;
      num6 = ((num6 << 12) | (num6 >> 20)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += (G(num6, num7, num8) + blockDWords[7]) + 0x7a6d76e9;
      num10 = ((num10 << 5) | (num10 >> 0x1b)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += (G(num10, num6, num7) + blockDWords[10]) + 0x7a6d76e9;
      num9 = ((num9 << 15) | (num9 >> 0x11)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += (G(num9, num10, num6) + blockDWords[14]) + 0x7a6d76e9;
      num8 = ((num8 << 8) | (num8 >> 0x18)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += F(num8, num9, num10) + blockDWords[12];
      num7 = ((num7 << 8) | (num7 >> 0x18)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += F(num7, num8, num9) + blockDWords[15];
      num6 = ((num6 << 5) | (num6 >> 0x1b)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += F(num6, num7, num8) + blockDWords[10];
      num10 = ((num10 << 12) | (num10 >> 20)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += F(num10, num6, num7) + blockDWords[4];
      num9 = ((num9 << 9) | (num9 >> 0x17)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += F(num9, num10, num6) + blockDWords[1];
      num8 = ((num8 << 12) | (num8 >> 20)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += F(num8, num9, num10) + blockDWords[5];
      num7 = ((num7 << 5) | (num7 >> 0x1b)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += F(num7, num8, num9) + blockDWords[8];
      num6 = ((num6 << 14) | (num6 >> 0x12)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += F(num6, num7, num8) + blockDWords[7];
      num10 = ((num10 << 6) | (num10 >> 0x1a)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += F(num10, num6, num7) + blockDWords[6];
      num9 = ((num9 << 8) | (num9 >> 0x18)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += F(num9, num10, num6) + blockDWords[2];
      num8 = ((num8 << 13) | (num8 >> 0x13)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += F(num8, num9, num10) + blockDWords[13];
      num7 = ((num7 << 6) | (num7 >> 0x1a)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num6 += F(num7, num8, num9) + blockDWords[14];
      num6 = ((num6 << 5) | (num6 >> 0x1b)) + num10;
      num8 = (num8 << 10) | (num8 >> 0x16);
      num10 += F(num6, num7, num8) + blockDWords[0];
      num10 = ((num10 << 15) | (num10 >> 0x11)) + num9;
      num7 = (num7 << 10) | (num7 >> 0x16);
      num9 += F(num10, num6, num7) + blockDWords[3];
      num9 = ((num9 << 13) | (num9 >> 0x13)) + num8;
      num6 = (num6 << 10) | (num6 >> 0x16);
      num8 += F(num9, num10, num6) + blockDWords[9];
      num8 = ((num8 << 11) | (num8 >> 0x15)) + num7;
      num10 = (num10 << 10) | (num10 >> 0x16);
      num7 += F(num8, num9, num10) + blockDWords[11];
      num7 = ((num7 << 11) | (num7 >> 0x15)) + num6;
      num9 = (num9 << 10) | (num9 >> 0x16);
      num9 += y + state[1];
      state[1] = (state[2] + z) + num10;
      state[2] = (state[3] + num5) + num6;
      state[3] = (state[4] + x) + num7;
      state[4] = (state[0] + num2) + num8;
      state[0] = num9;
    }

    private static uint F(uint x, uint y, uint z)
    {
      return ((x ^ y) ^ z);
    }

    private static uint G(uint x, uint y, uint z)
    {
      return ((x & y) | (~x & z));
    }

    private static uint H(uint x, uint y, uint z)
    {
      return ((x | ~y) ^ z);
    }

    private static uint I(uint x, uint y, uint z)
    {
      return ((x & z) | (y & ~z));
    }

    private static uint J(uint x, uint y, uint z)
    {
      return (x ^ (y | ~z));
    }
  }

  class ExamplesFromManuel
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 66, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 66, MethodILOffset = 0)]
    public static void Write(int n)
    {
      // max:  2147483647  0x7FFFFFFF
      // min: -2147483648  0x80000000

      char[] chars = new char[11];
      bool negative = false;

      uint u;
      if (n < 0)
      {
        u = (uint)-n;
        negative = true;
      }
      else
      {
        u = (uint)n;
      }

      int i = 10;

      for (; i > 1; i--)
      {
        chars[i] = unchecked((char)('0' + (u % 10)));
        u = u / 10;

        if (u == 0)
          break;
      }

      // f: Thanks to widening with threshoulds, it can infer the good lower bound

      if (negative)
      {
         i--;
        chars[i] = '-';
      }

      string str = new String(chars, i, 11 - i);
      Console.Write(str);
    }

  }
}



namespace Materialization
{
  public class MaterializeArrayLength
  {
    // [ClousotRegressionTest]
    public static void Equality_AfterLoop_NOT_OK(int[] arr)
    {
      int i = 0;

      for (; i < arr.Length; i++)
      {
      }

      Contract.Assert(i == arr.Length);
    }
  }

}
