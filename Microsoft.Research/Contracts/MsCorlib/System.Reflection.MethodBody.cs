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
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  // Summary:
  //     Provides access to the metadata and MSIL for the body of a method.
  public class MethodBody
  {
    protected MethodBody() { }

    // Summary:
    //     Gets a list that includes all the exception-handling clauses in the method
    //     body.
    //
    // Returns:
    //     An System.Collections.Generic.IList<T> of System.Reflection.ExceptionHandlingClause
    //     objects representing the exception-handling clauses in the body of the method.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    public IList<ExceptionHandlingClause> ExceptionHandlingClauses
    {
      get
      {
        Contract.Ensures(Contract.Result<IList<ExceptionHandlingClause>>() != null);
        return null;
      }
    }
    //
    // Summary:
    //     Gets a value indicating whether local variables in the method body are initialized
    //     to the default values for their types.
    //
    // Returns:
    //     true if the method body contains code to initialize local variables to null
    //     for reference types, or to the zero-initialized value for value types; otherwise,
    //     false.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    extern public bool InitLocals { get; }
    //
    // Summary:
    //     Gets a metadata token for the signature that describes the local variables
    //     for the method in metadata.
    //
    // Returns:
    //     An integer that represents the metadata token.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    extern public int LocalSignatureMetadataToken { get; }
    //
    // Summary:
    //     Gets the list of local variables declared in the method body.
    //
    // Returns:
    //     An System.Collections.Generic.IList<T> of System.Reflection.LocalVariableInfo
    //     objects that describe the local variables declared in the method body.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    public IList<LocalVariableInfo> LocalVariables
    {
      get
      {
        Contract.Ensures(Contract.Result<IList<LocalVariableInfo>>() != null);
        return null;
      }
    }
    //
    // Summary:
    //     Gets the maximum number of items on the operand stack when the method is
    //     executing.
    //
    // Returns:
    //     The maximum number of items on the operand stack when the method is executing.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    extern public int MaxStackSize { get; }

    // Summary:
    //     Returns the MSIL for the method body, as an array of bytes.
    //
    // Returns:
    //     An array of type System.Byte that contains the MSIL for the method body.
    [Pure]
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    public byte[] GetILAsByteArray()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return null;
    }
  }
}

#endif