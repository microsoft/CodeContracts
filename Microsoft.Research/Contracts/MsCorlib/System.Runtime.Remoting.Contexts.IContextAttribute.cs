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
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts {
  // Summary:
  //     Identifies a context attribute.
  [ComVisible(true)]
  public interface IContextAttribute {
    // Summary:
    //     Returns context properties to the caller in the given message.
    //
    // Parameters:
    //   msg:
    //     The System.Runtime.Remoting.Activation.IConstructionCallMessage to which
    //     to add the context properties.
    void GetPropertiesForNewContext(IConstructionCallMessage msg);
    //
    // Summary:
    //     Returns a Boolean value indicating whether the specified context meets the
    //     context attribute's requirements.
    //
    // Parameters:
    //   ctx:
    //     The context to check against the current context attribute.
    //
    //   msg:
    //     The construction call, parameters of which need to be checked against the
    //     current context.
    //
    // Returns:
    //     true if the passed in context is okay; otherwise, false.
    bool IsContextOK(Context ctx, IConstructionCallMessage msg);
  }
}
