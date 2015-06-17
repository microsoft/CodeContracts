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
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Runtime.Serialization
{
  // Summary:
  //     Provides a formatter-friendly mechanism for parsing the data in System.Runtime.Serialization.SerializationInfo.
  //     This class cannot be inherited.
  public sealed class SerializationInfoEnumerator 
  {
    // Summary:
    //     Gets the item currently being examined.
    //
    // Returns:
    //     The item currently being examined.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The enumerator has not started enumerating items or has reached the end of
    //     the enumeration.
#if !SILVERLIGHT
    extern public SerializationEntry Current { get; }
#endif
    //
    // Summary:
    //     Gets the name for the item currently being examined.
    //
    // Returns:
    //     The item name.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The enumerator has not started enumerating items or has reached the end of
    //     the enumeration.
    public string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the type of the item currently being examined.
    //
    // Returns:
    //     The type of the item currently being examined.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The enumerator has not started enumerating items or has reached the end of
    //     the enumeration.
    public Type ObjectType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);
        return default(Type);
      }
    }
    //
    // Summary:
    //     Gets the value of the item currently being examined.
    //
    // Returns:
    //     The value of the item currently being examined.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The enumerator has not started enumerating items or has reached the end of
    //     the enumeration.
    extern public object Value { get; }

  }
}

#endif