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
using System.Text;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace CodeUnderTest {
  [ContractClass(typeof(ContractClassForAnInterface<>))]
  public interface IGenericInterface<T> where T : class {
    T M(T x, bool behave);

    void WithStaticClosure(T[] x, bool behave);

    void WithClosureObject(T[] x, bool behave);
  }

  [ContractClassFor(typeof(IGenericInterface<>))]
  abstract class ContractClassForAnInterface<W> : IGenericInterface<W> where W : class {
    W IGenericInterface<W>.M(W y, bool b)
    {
      Contract.Requires(y != null);
      Contract.Ensures(Contract.Result<W>() != null && Contract.Result<W>().Equals(y));
      return default(W);
    }

    void IGenericInterface<W>.WithStaticClosure(W[] x, bool behave)
    {
      Contract.Ensures(Contract.ForAll(x, e => e != null));
    }

    void IGenericInterface<W>.WithClosureObject(W[] x, bool behave)
    {
      Contract.Ensures(Contract.ForAll(0, x.Length, i => x[i] != null));
    }

  }


  public class NonGenericExplicitImpl : IGenericInterface<string> {
    string IGenericInterface<string>.M(string z, bool behave)
    {
      return behave ? z : String.Concat(z, z);
    }

    void IGenericInterface<string>.WithStaticClosure(string[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }

    void IGenericInterface<string>.WithClosureObject(string[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }
  }
  public class NonGenericImplicitImpl : IGenericInterface<string> {
    public string M(string z, bool behave)
    {
      return behave ? z : String.Concat(z, z);
    }

    public void WithStaticClosure(string[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }

    public void WithClosureObject(string[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }
  }

  public class GenericExplicitImpl<U> : IGenericInterface<U> where U : class {
    U IGenericInterface<U>.M(U z, bool behave)
    {
      return behave ? z : null;
    }

    void IGenericInterface<U>.WithStaticClosure(U[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }

    void IGenericInterface<U>.WithClosureObject(U[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }
  }

  public class GenericImplicitImpl<U> : IGenericInterface<U> where U : class {
    public U M(U z, bool behave)
    {
      return behave ? z : null;
    }

    public void WithStaticClosure(U[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }

    public void WithClosureObject(U[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }
  }

  /// <summary>
  /// Check that self-recursive interfaces are instantiated properly.
  /// </summary>
  /// <typeparam name="This"></typeparam>
  [ContractClass(typeof(IAbstractDomainContracts<>))]
  public interface IAbstractDomain<This>
    where This : IAbstractDomain<This>
  {
  }

  [ContractClassFor(typeof(IAbstractDomain<>))]
  public abstract class IAbstractDomainContracts<This> : IAbstractDomain<This>
    where This : IAbstractDomain<This>
  {
  }

  [ContractClass(typeof(IAbstractEnvironmentContracts<>))]
  internal interface IAbstractEnvironment<This>
    : IAbstractDomain<This>
    where This : IAbstractEnvironment<This>
  {
  }

  /// <summary>
  /// We had a bug here where the extractor rejected this because it found that IAbstractEnvironment's instance
  /// applied to This was not implementing IAbstractDomain due to the reader/duplicator/specializer touching the 
  /// interface list too early. Fixed by making the Specializer lazy w.r.t. TypeSignatures as well so that the reader
  /// has a chance to assign the interface list before they are needed.
  /// </summary>
  /// <typeparam name="This"></typeparam>
  [ContractClassFor(typeof(IAbstractEnvironment<>))]
  internal abstract class IAbstractEnvironmentContracts<This>
      : IAbstractEnvironment<This>
      where This : IAbstractEnvironment<This>
  {
  }

}