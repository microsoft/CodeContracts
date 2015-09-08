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
using System.IO;
using System.Diagnostics.Contracts;

public class ClassWithProtocol
{
  public enum S
  {
    NotReady, Initialized, Computed
  }

  private S _state;
  public S State
  {
    get
    {
      return _state;
    }
  }

  [ContractInvariantMethod]
  private void ObjectInvariant()
  {
    Contract.Invariant(_state != S.Computed || _computedData != null);
  }

  public ClassWithProtocol()
  {
    Contract.Ensures(this.State == S.NotReady);
    _state = S.NotReady;
  }

  string _data;

  public void Initialize(string data)
  {
    Contract.Requires(State == S.NotReady);
    Contract.Ensures(State == S.Initialized);

    this._data = data;
    _state = S.Initialized;
  }

  public bool Compute(string prefix)
  {
    Contract.Requires(prefix != null);
    Contract.Requires(State == S.Initialized);
    Contract.Ensures(Contract.Result<bool>() && State == S.Computed ||
                     !Contract.Result<bool>() && State == S.Initialized);

    this._computedData = prefix + _data;
    _state = S.Computed;

    return true;
  }

  public string Data
  {
    get
    {
      Contract.Requires(State != S.NotReady);

      return _data;
    }
  }


  string _computedData;
  public string ComputedData
  {
    get
    {
      Contract.Requires(State == S.Computed);
      Contract.Ensures(Contract.Result<string>() != null);

      return _computedData;
    }
  }

  
}
