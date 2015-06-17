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
using System.Text;
using System.Diagnostics.Contracts;

namespace AssemblyWithContracts
{
  public class BaseClass
  {
    public virtual void WithForallRequires(string s)
    {
      Contract.Requires(Contract.ForAll(0, s.Length, i => s[i] != ' '));
    }
    public virtual void WithExistsRequires(string s)
    {
      Contract.Requires(!Contract.Exists(0, s.Length, i => s[i] == ' '));
    }
    public virtual void WithGenericForallRequires(string s)
    {
      Contract.Requires(Contract.ForAll(s, c => c != ' '));
    }
    public virtual void WithGenericExistsRequires(string s)
    {
      Contract.Requires(!Contract.Exists(s, c => c == ' '));
    }

    protected string name;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(name == null);
    }

  }
}

// The code in Onur isn't used but appears in the contract reference assembly. When
// extracted and copied when referenced from a third assembly, this caused copying problems in the past
// Thus it appears here to be part of the regular regression.
namespace Onurg {
  public class Foo : IFoo
  {
    IEnumerable<T> IFoo.Bar<T>()
    {
      return null;
    }
  }

  [ContractClass(typeof(FooContract))]
  public interface IFoo
  {
    IEnumerable<T> Bar<T>();
  }

  [ContractClassFor(typeof(IFoo))]
  abstract class FooContract : IFoo
  {
    IEnumerable<T> IFoo.Bar<T>()
    {
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), x => x != null));
      return default(IEnumerable<T>);
    }
  }

}


namespace Strilanc
{
  #region ICheckCopyingOOB contract binding
  [ContractClass(typeof(ICheckCopyingOOBContract<>))]
  public partial interface ICheckCopyingOOB<T>
  {
    int Count { get; }
    object this[int index] { get; }
  }

  [ContractClassFor(typeof(ICheckCopyingOOB<>))]
  abstract class ICheckCopyingOOBContract<T> : ICheckCopyingOOB<T>
  {
    #region ICheckCopyingOOB<T> Members

    public int Count
    {
      get { throw new NotImplementedException(); }
    }

    public object this[int index]
    {
      get
      {
        Contract.Requires(index < this.Count);
        return default(object);
      }
    }

    #endregion
  }
  #endregion



}