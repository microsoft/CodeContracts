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


#if !SILVERLIGHT

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging {
  // Summary:
  //     Defines the method call message interface.
  // [ComVisible(true)]
  [ContractClass(typeof(IMethodCallMessageContracts))]
  public interface IMethodCallMessage // : IMethodMessage, IMessage 
  {
    // Summary:
    //     Gets the number of arguments in the call that are not marked as out parameters.
    //
    // Returns:
    //     The number of arguments in the call that are not marked as out parameters.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    int InArgCount { get; }
    //
    // Summary:
    //     Gets an array of arguments that are not marked as out parameters.
    //
    // Returns:
    //     An array of arguments that are not marked as out parameters.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    object[] InArgs { get; }

    // Summary:
    //     Returns the specified argument that is not marked as an out parameter.
    //
    // Parameters:
    //   argNum:
    //     The number of the requested in argument.
    //
    // Returns:
    //     The requested argument that is not marked as an out parameter.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    object GetInArg(int argNum);
    //
    // Summary:
    //     Returns the name of the specified argument that is not marked as an out parameter.
    //
    // Parameters:
    //   index:
    //     The number of the requested in argument.
    //
    // Returns:
    //     The name of a specific argument that is not marked as an out parameter.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The immediate caller makes the call through a reference to the interface
    //     and does not have infrastructure permission.
    string GetInArgName(int index);
  }

  [ContractClassFor(typeof(IMethodCallMessage))]
  abstract class IMethodCallMessageContracts : IMethodCallMessage
  {
    #region IMethodCallMessage Members

    public int InArgCount
    {
      get 
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
    }

    public object[] InArgs
    {
      get 
      { 
        return default(object[]);
      }
    }

    public object GetInArg(int argNum)
    {
      Contract.Requires(argNum >= 0);

      return default(object);
    }

    public string GetInArgName(int index)
    {
      Contract.Requires(index >= 0);

      return default(string);
    }

    #endregion
  }
}

#endif