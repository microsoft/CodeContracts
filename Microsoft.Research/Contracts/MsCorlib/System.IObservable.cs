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

#if NETFRAMEWORK_4_0

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace System
{
  public partial interface IObservable<T>
  {
    IDisposable Subscribe(IObserver<T> observer);
  }

  #region IObservable contract binding
  [ContractClass(typeof(IObservableContract<>))]
  public partial interface IObservable<T>
  {}

  [ContractClassFor(typeof(IObservable<>))]
  abstract class IObservableContract<T> : IObservable<T>
  {
    public IDisposable Subscribe(IObserver<T> observer)
    {
      Contract.Requires(observer != null);
      Contract.Ensures(Contract.Result<IDisposable>() != null);
      return default(IDisposable);
    }
  }
  #endregion

  public partial interface IObserver<T>
  {
    void OnCompleted();
    void OnError(Exception error);
    void OnNext(T value);
  }

  #region IObserver contract binding
  [ContractClass(typeof(IObserverContract<>))]
  public partial interface IObserver<T>
  {}

  [ContractClassFor(typeof(IObserver<>))]
  abstract class IObserverContract<T> : IObserver<T>
  {
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
      Contract.Requires(error != null);
    }

    public void OnNext(T value)
    {
    }
  }
  #endregion

}

#endif