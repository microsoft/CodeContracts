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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace AssumeInvariant
{
    class C
    {
        public int field;

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(field > 0);
        }

        public C()
        {
            field = 1;
        }
    }

    class Test
    {
        [Pure]
        static void AssumeInvariant<T>(T o) { }

        static void Main(string[] args)
        {


            var p = new C();

            TestMe1(p);
            TestMe2(p);
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=10,MethodILOffset=0)]
        static void TestMe1(C p) {
            Contract.Assert(p.field > 0);
        }

        [ClousotRegressionTest]
        static void TestMe2(C p)
        {
            AssumeInvariant(p);

            Contract.Assert(p.field > 0);

        }
    }

}

namespace AssumeInvariantOldIssue {
  using System.Collections;

  public class Host
  {
    public string Name = "";
    
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(Name != null);
    }
  }

  class InvariantAtCallAndOldHandling {
    public static class ContractHelpers
    {
      [ContractVerification(false)]
      public static void AssumeInvariant<T>(T o)
      {
      }
    }

    [ClousotRegressionTest]
    static void AssumeInvariantTrue()
    {
      foreach (Host h in new ArrayList())
      {
        Contract.Assume(h != null);

        ContractHelpers.AssumeInvariant(h);

        Contract.Assert(h.Name != null);
      }

    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=53,MethodILOffset=0)]
    static void AssumeInvariantUnproven()
    {
      foreach (Host h in new ArrayList())
      {
        Contract.Assume(h != null);

        Contract.Assert(h.Name != null);
      }

    }

  }

}
