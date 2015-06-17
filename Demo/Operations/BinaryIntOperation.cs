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
  public abstract partial class BinaryIntOperation : BinaryOperation
  {
    #region IOperation Members

    private static Type[] types = new[] { typeof(int), typeof(int) };

    public override Type[] ArgumentTypes
    {
      get
      {
        Contract.Assume(types.Length == 2);
        return types;
      }
    }

    private static Type result = typeof(int);

    public override Type ResultType
    {
      get
      {
        return result;
      }
    }

    [SuppressMessage("Microsoft.Contracts", "Ensures-159-31")]
    public override object Perform(params object[] arguments)
    {
      var arg1 = (int)arguments[0];
      var arg2 = (int)arguments[1];

      return Operation(arg1, arg2);
    }

    public abstract int Operation(int arg1, int arg2);

    #endregion
  }


}
