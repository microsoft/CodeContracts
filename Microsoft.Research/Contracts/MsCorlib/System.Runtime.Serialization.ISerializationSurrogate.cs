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
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization {
  // Summary:
  //     Implements a serialization surrogate selector that allows one object to perform
  //     serialization and deserialization of another.
  [ComVisible(true)]
  public interface ISerializationSurrogate {
    // Summary:
    //     Populates the provided System.Runtime.Serialization.SerializationInfo with
    //     the data needed to serialize the object.
    //
    // Parameters:
    //   obj:
    //     The object to serialize.
    //
    //   info:
    //     The System.Runtime.Serialization.SerializationInfo to populate with data.
    //
    //   context:
    //     The destination (see System.Runtime.Serialization.StreamingContext) for this
    //     serialization.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    void GetObjectData(object obj, SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Populates the object using the information in the System.Runtime.Serialization.SerializationInfo.
    //
    // Parameters:
    //   obj:
    //     The object to populate.
    //
    //   info:
    //     The information to populate the object.
    //
    //   context:
    //     The source from which the object is deserialized.
    //
    //   selector:
    //     The surrogate selector where the search for a compatible surrogate begins.
    //
    // Returns:
    //     The populated deserialized object.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);
  }
}

