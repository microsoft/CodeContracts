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
  //     Defines a set of flags that specifies the source or destination context for
  //     the stream during serialization.
  public enum StreamingContextStates {
    // Summary:
    //     Specifies that the source or destination context is a different process on
    //     the same computer.
    CrossProcess = 1,
    //
    // Summary:
    //     Specifies that the source or destination context is a different computer.
    CrossMachine = 2,
    //
    // Summary:
    //     Specifies that the source or destination context is a file. Users can assume
    //     that files will last longer than the process that created them and not serialize
    //     objects in such a way that deserialization will require accessing any data
    //     from the current process.
    File = 4,
    //
    // Summary:
    //     Specifies that the source or destination context is a persisted store, which
    //     could include databases, files, or other backing stores. Users can assume
    //     that persisted data will last longer than the process that created the data
    //     and not serialize objects so that deserialization will require accessing
    //     any data from the current process.
    Persistence = 8,
    //
    // Summary:
    //     Specifies that the data is remoted to a context in an unknown location. Users
    //     cannot make any assumptions whether this is on the same computer.
    Remoting = 16,
    //
    // Summary:
    //     Specifies that the serialization context is unknown.
    Other = 32,
    //
    // Summary:
    //     Specifies that the object graph is being cloned. Users can assume that the
    //     cloned graph will continue to exist within the same process and be safe to
    //     access handles or other references to unmanaged resources.
    Clone = 64,
    //
    // Summary:
    //     Specifies that the source or destination context is a different AppDomain.
    //     (For a description of AppDomains, see [<topic://cpconapplicationdomains>]).
    CrossAppDomain = 128,
    //
    // Summary:
    //     Specifies that the serialized data can be transmitted to or received from
    //     any of the other contexts.
    All = 255,
  }
}
