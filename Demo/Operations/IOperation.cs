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
using System.Diagnostics.CodeAnalysis;

namespace Operations
{
  public partial interface IOperation
  {
    Type[] ArgumentTypes { get; }
    Type ResultType { get; }

    //[SuppressMessage("Microsoft.Contracts", "RequiresAtCall-Contract.ForAll(0, arguments.Length, i => this.ArgumentTypes[i].IsAssignableFrom(arguments[i].GetType()))")] 
    object Perform(params object[] arguments);

  }

  #region IOperation contract binding
  [ContractClass(typeof(IOperationContract))]
  public partial interface IOperation
  {
  }

  [ContractClassFor(typeof(IOperation))]
  abstract class IOperationContract : IOperation
  {

    #region IOperation Members

    public Type[] ArgumentTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<Type[]>() != null);

        return default(Type[]);
      }
    }

    public Type ResultType
    {
      get {
        Contract.Ensures(Contract.Result<Type>() != null);
        return default(Type);
      }
    }

    public object Perform(params object[] arguments)
    {
      Contract.Requires(arguments != null);
      Contract.Requires(arguments.Length == ArgumentTypes.Length);
      Contract.Requires(Contract.ForAll(0, arguments.Length, i => arguments[i] != null));
      Contract.Requires(Contract.ForAll(0, arguments.Length, i => this.ArgumentTypes[i].IsAssignableFrom(arguments[i].GetType())));

      Contract.Ensures(Contract.Result<object>() != null);
      Contract.Ensures(this.ResultType.IsAssignableFrom(Contract.Result<object>().GetType()));

      return default(object);
    }

    #endregion
  }
  #endregion

 
}
