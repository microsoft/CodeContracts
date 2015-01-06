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
using System.Diagnostics.Contracts;
using System.Text;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization {
  // Summary:
  //     Describes the source and destination of a given serialized stream, and provides
  //     an additional caller-defined context.
  public struct StreamingContext {

#if !SILVERLIGHT
    //
    // Summary:
    //     Initializes a new instance of the System.Runtime.Serialization.StreamingContext
    //     class with a given context state.
    //
    // Parameters:
    //   state:
    //     A bitwise combination of the System.Runtime.Serialization.StreamingContextStates
    //     values that specify the source or destination context for this System.Runtime.Serialization.StreamingContext.
    extern public StreamingContext(StreamingContextStates state);

    //
    // Summary:
    //     Initializes a new instance of the System.Runtime.Serialization.StreamingContext
    //     class with a given context state, and some additional information.
    //
    // Parameters:
    //   state:
    //     A bitwise combination of the System.Runtime.Serialization.StreamingContextStates
    //     values that specify the source or destination context for this System.Runtime.Serialization.StreamingContext.
    //
    //   additional:
    //     Any additional information to be associated with the System.Runtime.Serialization.StreamingContext.
    //     This information is available to any object that implements System.Runtime.Serialization.ISerializable
    //     or any serialization surrogate. Most users do not need to set this parameter.
    extern public StreamingContext(StreamingContextStates state, object additional);
#endif

#if !SILVERLIGHT
    // Summary:
    //     Gets context specified as part of the additional context.
    //
    // Returns:
    //     The context specified as part of the additional context.
    public object Context { get { return default(object); } }
#endif

    //
    // Summary:
    //     Gets the source or destination of the transmitted data.
    //
    // Returns:
    //     During serialization, the destination of the transmitted data. During deserialization,
    //     the source of the data.
    // public StreamingContextStates State { get { return default(object); } }

  }
}
