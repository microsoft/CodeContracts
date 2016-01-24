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

using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace CodeContractProblems
{
    [ContractClass(typeof(ICC5Contract))]
    public interface ICC5
    {
        string Property { get; }
        bool SomeFlag { get; }
    }

    public class CC5 : ICC5
    {
        private string _field;
        private bool _someFlag;

         public string Property
        {
        [ClousotRegressionTest]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=24,MethodILOffset=0)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=7,MethodILOffset=33)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=17,MethodILOffset=33)]
            get
            {
                Contract.Ensures(Contract.Result<string>() == _field);
                return _field;
            }
        }

        public bool SomeFlag
        {
        [ClousotRegressionTest]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=21,MethodILOffset=0)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=7,MethodILOffset=30)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=7,MethodILOffset=30)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=20,MethodILOffset=30)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=14,MethodILOffset=30)]
		get
            {
                Contract.Ensures(Contract.Result<bool>() == _someFlag);
                return _someFlag;
            }
        }

        /// <summary>Defines the invariants that hold on all objects of this class that are visible to a client.</summary>
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(_someFlag == (_field != null));
        }
    }

    [ContractClassFor(typeof(ICC5))]
    public abstract class ICC5Contract : ICC5
    {
        public string Property
        {
            get { return null; }
        }

        public bool SomeFlag
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>() == (Property != null));
                return false;
            }
        }
    }
	
    /*
     * The static checker incorrectly warns about `Property2` being possibly `null` in `Foo` and `Foo2`.
     * Additionally, it warns about `Property1` in `Foo2` but not in `Foo`.
     * It shouldn't warn at all.
     * Changing the contract in ICC8Contract to use `Property2` first will switch the behavior around:
     * It warns about `Property1` in both methods and about `Property2` only in `Foo2`.
     * Additionally, in both cases it will warn about a missing pre-conditions for both properties in `Foo2`
     * but not in `Foo`.
     */
    [ContractClass(typeof(ICC8Contract))]
    public interface ICC8
    {
        [Pure]
        string Property1 { get; }

        [Pure]
        string Property2 { get; }

        [Pure]
        bool SomeFlag { get; }

        void Foo();
    }

    public class CC8 : ICC8
    {
        private string _field1;
        private string _field2;

        [Pure]
        public string Property1
        {
            get { return _field1; }
        }

        [Pure]
        public string Property2
        {
            get { return _field2; }
        }

        [Pure]
        public bool SomeFlag
        {
            get { return Property1 != null && Property2 != null; }
        }

		[ClousotRegressionTest]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=2,MethodILOffset=0)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=7,MethodILOffset=0)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=14,MethodILOffset=0)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=19,MethodILOffset=0)]
        public void Foo()
        {
            Property1.Trim();
            Property2.Trim();
        }

		// As Foo2 is not in ICC8, we do not inherit it 
		[ClousotRegressionTest]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=2,MethodILOffset=0)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this.Property2'",PrimaryILOffset=7,MethodILOffset=0)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=14,MethodILOffset=0)]
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this.Property1'",PrimaryILOffset=19,MethodILOffset=0)]
        public void Foo2()
        {
            Property2.Trim();
            Property1.Trim();
        }
    }

    [ContractClassFor(typeof(ICC8))]
    public abstract class ICC8Contract : ICC8
    {
        [Pure]
        public string Property1
        {
            get { return null; }
        }

        [Pure]
        public string Property2
        {
            get { return null; }
        }

        [Pure]
        public bool SomeFlag
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>() == (Property2 != null && Property1 != null));
              
              return false;
            }
        }

        public void Foo()
        {
            Contract.Requires(SomeFlag);
        }

		// Please note, it is not in the ICC8 contract
        public void Foo2()
        {
            Contract.Requires(SomeFlag);
        }
    }
}
