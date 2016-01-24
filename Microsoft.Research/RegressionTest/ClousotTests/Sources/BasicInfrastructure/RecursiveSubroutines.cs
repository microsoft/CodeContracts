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
using System.Text;
using Microsoft.Research.ClousotRegression;

namespace RecursiveSubroutines
{
  /// <summary>
  /// Here we check that the infrastructure handles recursive predicates without falling over.
  /// It doesn't prove anything interesting about them.
  /// </summary>
  class MyList
  {
    Node head;
    Node tail;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(LS(head, tail));

    }
    public class Node
    {
      public int value;
      public Node next;

      public Node(int value, Node next)
      {
        this.value = value;
        this.next = next;
      }
    }

    [Pure]
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly accessing a field on a null reference 'start'. The static checker determined that the condition 'start != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(start != null);", PrimaryILOffset = 33, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 39, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 7, MethodILOffset = 51)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 13, MethodILOffset = 51)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: start == end || LS(start.next, end)", PrimaryILOffset = 21, MethodILOffset = 51)]
    private bool LS(Node start, Node end)
    {
      // need more support of multiargument pure functions before this will pass, in particular for old and expressions
      Contract.Ensures(start == end || LS(start.next, end));
      return start == end || LS(start.next, end);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"invariant unproven: LS(head, tail)", PrimaryILOffset = 19, MethodILOffset = 34)]
    public MyList() {
      this.head = new Node(0,null);
      this.tail = this.head;
    }
  }

}
