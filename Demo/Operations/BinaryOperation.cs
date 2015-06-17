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
  public abstract partial class BinaryOperation : IOperation
  {
    #region IOperation Members

    abstract public Type[] ArgumentTypes
    {
      get;
    }

    abstract public Type ResultType
    {
      get;
    }

    abstract public object Perform(params object[] arguments);

    #endregion
  }

  #region BinaryOperation contract binding
  [ContractClass(typeof(BinaryOperationContract))]
  public partial class BinaryOperation
  {
  }

  [ContractClassFor(typeof(BinaryOperation))]
  abstract class BinaryOperationContract : BinaryOperation
  {
    public override Type[] ArgumentTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<Type[]>().Length == 2);

        return default(Type[]);
      }
    }

  }
  #endregion

}
