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

  #define CONTRACTS_FULL

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using Microsoft.Research.ClousotRegression;
  using System.Diagnostics.Contracts;


namespace IteratorAnalysis {
  class ClousotRegressionTestAttribute : Attribute {
  }
  class LinkedList {
    public string Value=null;
    public LinkedList Next=null;
    public int Random =0;

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=206,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=169,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=103,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=213,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=112,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=117,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=141,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=146,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=151,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=158,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=178,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=183,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=188,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=195,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=52,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=60,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=65,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=72,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=77,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=82,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=89,MethodILOffset=0)]
    IEnumerable<string> AllValues1() {
      LinkedList cursor = this;
      yield return cursor.Value;
      while (cursor != null) {
        if (cursor.Value == "")
          yield return cursor.Value;
        else
          yield return cursor.Value;
      }
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=135,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=142,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=147,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=152,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=159,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly accessing a field on a null reference 'cursor'",PrimaryILOffset=164,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=169,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=176,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=187,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=97,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=194,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=107,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=112,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=117,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=124,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=49,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=57,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=62,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=69,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=74,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=79,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=86,MethodILOffset=0)]
    IEnumerable<string> AllValues2() {
      LinkedList cursor = this;
      yield return cursor.Value;
      while (cursor != null) {
        yield return cursor.Value;
        cursor = cursor.Next;
        yield return cursor.Value;
      }
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=126,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=133,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=138,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly accessing a field on a null reference 'cursor.Next'",PrimaryILOffset=143,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=148,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=88,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=155,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=98,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=103,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=108,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=115,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=48,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=53,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=60,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=65,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=70,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=77,MethodILOffset=0)]
    IEnumerable<string> AllValues3() {
      LinkedList cursor = this;
      yield return cursor.Value;
      while (cursor != null) {
        yield return cursor.Value;
        cursor = cursor.Next.Next;
      }
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=135,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=142,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=147,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=152,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=159,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=170,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=97,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=177,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=107,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=112,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=117,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=124,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=49,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=57,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=62,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=69,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=74,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=79,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=86,MethodILOffset=0)]
    IEnumerable<string> AllValues4() {
      LinkedList cursor = this;
      yield return cursor.Value;
      while (cursor != null) {
        yield return cursor.Value;
        yield return cursor.Value;
      }
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=123,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=85,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=130,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=95,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=100,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=105,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=112,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=45,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=57,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=62,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=67,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=74,MethodILOffset=0)]
    IEnumerable<string> AllValues5() {
      LinkedList cursor = this;
      yield return cursor.Value;
      while (cursor != null) {
        yield return cursor.Value;
      }
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=104,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=31,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=39,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=44,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=111,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=54,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly accessing a field on a null reference 'cursor.Next'",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=69,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=76,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly accessing a field on a null reference 'cursor'",PrimaryILOffset=81,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=86,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=93,MethodILOffset=0)]
    IEnumerable<string> AllValues6() {
      LinkedList cursor = this;
      while (cursor != null) {
        cursor = cursor.Next.Next;
        yield return cursor.Value;
      }
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=99,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=31,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=39,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=44,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=106,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=54,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=71,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=81,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=88,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly accessing a field on a null reference 'cursor'", PrimaryILOffset = 76, MethodILOffset = 0)]
    IEnumerable<string> AllValues7() {
      LinkedList cursor = this;
      while (cursor != null) {
        cursor = cursor.Next;
        yield return cursor.Value;
      }
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=82,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=89,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=94,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=99,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=105,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=31,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=39,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=44,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=54,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=71,MethodILOffset=0)]
    IEnumerable<string> AllValues8() {
      LinkedList cursor = this;
      while (true) {
        yield return cursor.Value;
        cursor = cursor.Next;
        if (cursor == null) break;
      }
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=82,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=89,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=94,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=99,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=31,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=39,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=44,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=106,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=54,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=71,MethodILOffset=0)]
    IEnumerable<string> AllValues9() {
      LinkedList cursor = this;
      while (cursor != null) {
        yield return cursor.Value;
        cursor = cursor.Next;
      }
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=88,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=95,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=100,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=105,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=112,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=123,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=45,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=50,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=130,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=60,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=65,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=70,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=77,MethodILOffset=0)]
    IEnumerable<string> AllValues10() {
      LinkedList cursor = this;
      while (cursor != null) {
        yield return cursor.Value;
        yield return cursor.Value;
      }
    }
  }

  class Test2 {
    public IEnumerable<string> Test1a(string s) {
      Contract.Requires(s != null);
      //if (s.Length < 0) throw new Exception("");
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), (string s1) => s1 != null));
      int[] x = { 1, 2 };
      x.First(((int y) => y > 0));
      yield return s;
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=55,MethodILOffset=20)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=55,MethodILOffset=28)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=41,MethodILOffset=0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: s != null", PrimaryILOffset = 55, MethodILOffset = 49)]
    public string Test2a(string s) {
      Contract.Requires(s != null);
      // Contract.Ensures(Contract.Result<string>() != null);
      var strs = Test1a("hello");
      strs = Test1a(s);
      Contract.Assert(strs != null);
      strs = Test1a(null);
      return s;
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: s != null", PrimaryILOffset = 63, MethodILOffset = 3)]
    public void Test2b(string s) {
      Test1b(null);
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: s != null", PrimaryILOffset = 22, MethodILOffset = 3)]
    public void Test2c(string s) {
      Test1c(null);
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=59,MethodILOffset=16)]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=39,MethodILOffset=54)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=44,MethodILOffset=54)]
#endif
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 59, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: input != null", PrimaryILOffset = 59, MethodILOffset = 70)]
#if !NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=22,MethodILOffset=54)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: collection != null (collection)",PrimaryILOffset=17,MethodILOffset=54)]
#endif
    public void Test2d(IEnumerable<string> s) {
      Contract.Requires(s != null);
      IEnumerable<string> resultEnum = Test1d(s);
      Contract.Assert(Contract.ForAll(resultEnum, (string s1) => s1 != null));
      s = null;
      Test1d(s);
    }

    public IEnumerable<string> Test1b(string s) {
      if (s == null) throw new Exception("");
      Contract.EndContractBlock();
      yield return s;
    }

    public void Test1c(string s) {
      if (s == null) throw new Exception("");
      Contract.EndContractBlock();
    }
    //public string s1;
    //[ContractInvariantMethod]
    //protected void ObjectInvariant() {
    //  Contract.Invariant(s1 != null);
    //}

    // iteartor methods with type parameters
    public IEnumerable<T> Test1d<T>(IEnumerable<T> input) {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
      foreach (T t in input)
        yield return t;
    }

    public void Test1e<T>(IEnumerable<T> input) {
      Contract.Requires(Contract.ForAll(input, (T s) => s != null));
    }
  }
}
