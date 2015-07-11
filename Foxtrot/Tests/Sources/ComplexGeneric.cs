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

//
// This is a regression test for Microsoft/CodeContracts#20:
//
//   Unable to cast object of type 'System.Compiler.TypeParameter' to type 'System.Compiler.Class'.
//
// https://github.com/Microsoft/CodeContracts/issues/20
//

using System.Diagnostics.Contracts;

namespace Tests.Sources
{
// This bug was relevant only for .NET 4.0+
#if NETFRAMEWORK_3_5

    partial class TestMain
    {
        partial void Run()
        {
            if (!this.behave)
            {
                throw new System.ArgumentNullException();
            }
        }

        public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
        public string NegativeExpectedCondition = "Value cannot be null.";
    }

#else

    // CCRewriter was unable to read this code from the IL due to an issue in CCI.
    // This test just makes sure that the fix is in place.

    public interface IIdentityObject<out TIdentity>
    {
    }

    public abstract class DefaultDomainBLLBase<TPersistentDomainObjectBase, TDomainObjectBase, TDomainObject, TIdent>
        where TPersistentDomainObjectBase : class, IIdentityObject<TIdent>, TDomainObjectBase
        where TDomainObjectBase : class
        where TDomainObject : class, TPersistentDomainObjectBase
    {
        protected TDomainObject GetNested<TNestedDomainObject>(TDomainObject x) where TNestedDomainObject : TDomainObject
        {
            return default(TDomainObject);
        }
    }

    public abstract class DefaultSecurityDomainBLLBase<TPersistentDomainObjectBase, TDomainObjectBase, TDomainObject, TIdent>
        : DefaultDomainBLLBase<TPersistentDomainObjectBase, TDomainObjectBase, TDomainObject, TIdent>
        where TPersistentDomainObjectBase : class, IIdentityObject<TIdent>, TDomainObjectBase
        where TDomainObjectBase : class
        where TDomainObject : class, TPersistentDomainObjectBase
    {
    }

    partial class TestMain
    {
        partial void Run()
        {
            if (!this.behave)
            {
                throw new System.ArgumentNullException();
            }
        }

        public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
        public string NegativeExpectedCondition = "Value cannot be null.";
    }

#endif

}
