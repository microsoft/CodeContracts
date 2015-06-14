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

#if !SILVERLIGHT_4_0_WP

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging {
  // Summary:
  //     Defines the out-of-band data for a call.
  //[Serializable]
  //[ComVisible(true)]
  public class Header {

#if !SILVERLIGHT
    // Summary:
    //     Indicates the XML namespace that the current System.Runtime.Remoting.Messaging.Header
    //     belongs to.
    public string HeaderNamespace;
    //
    // Summary:
    //     Indicates whether the receiving end must understand the out-of-band data.
    public bool MustUnderstand;
    //
    // Summary:
    //     Contains the name of the System.Runtime.Remoting.Messaging.Header.
    public string Name;
    //
    // Summary:
    //     Contains the value for the System.Runtime.Remoting.Messaging.Header.
    public object Value;

    // Summary:
    //     Initializes a new instance of the System.Runtime.Remoting.Messaging.Header
    //     class with the given name and value.
    //
    // Parameters:
    //   _Name:
    //     The name of the System.Runtime.Remoting.Messaging.Header.
    //
    //   _Value:
    //     The object that contains the value for the System.Runtime.Remoting.Messaging.Header.
    // public Header(string _Name, object _Value);
    //
    // Summary:
    //     Initializes a new instance of the System.Runtime.Remoting.Messaging.Header
    //     class with the given name, value, and additional configuration information.
    //
    // Parameters:
    //   _Name:
    //     The name of the System.Runtime.Remoting.Messaging.Header.
    //
    //   _Value:
    //     The object that contains the value for the System.Runtime.Remoting.Messaging.Header.
    //
    //   _MustUnderstand:
    //     Indicates whether the receiving end must understand the out-of-band data.
    //public Header(string _Name, object _Value, bool _MustUnderstand);
    //
    // Summary:
    //     Initializes a new instance of the System.Runtime.Remoting.Messaging.Header
    //     class.
    //
    // Parameters:
    //   _Name:
    //     The name of the System.Runtime.Remoting.Messaging.Header.
    //
    //   _Value:
    //     The object that contains the value of the System.Runtime.Remoting.Messaging.Header.
    //
    //   _MustUnderstand:
    //     Indicates whether the receiving end must understand out-of-band data.
    //
    //   _HeaderNamespace:
    //     The System.Runtime.Remoting.Messaging.Header XML namespace.
    //public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace);
#endif
  }
}

#endif