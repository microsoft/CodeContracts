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
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  // Summary:
  //     Holds the value, System.Type, and name of a serialized object.
  public struct SerializationEntry
  {

    // Summary:
    //     Gets the name of the object.
    //
    // Returns:
    //     The name of the object.
    public string Name { get { return default(string); } }
    //
    // Summary:
    //     Gets the System.Type of the object.
    //
    // Returns:
    //     The System.Type of the object.
    public Type ObjectType { get { return default(Type); } }
    //
    // Summary:
    //     Gets the value contained in the object.
    //
    // Returns:
    //     The value contained in the object.
    public object Value { get { return default(object); } }
  }
}

#endif