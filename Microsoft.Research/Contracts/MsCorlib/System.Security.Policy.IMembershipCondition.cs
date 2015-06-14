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
using System.Security;

namespace System.Security.Policy {
  // Summary:
  //     Defines the test to determine whether a code assembly is a member of a code
  //     group.
  [ComVisible(true)]
  public interface IMembershipCondition : ISecurityEncodable { // , ISecurityPolicyEncodable {
    // Summary:
    //     Determines whether the specified evidence satisfies the membership condition.
    //
    // Parameters:
    //   evidence:
    //     The evidence set against which to make the test.
    //
    // Returns:
    //     true if the specified evidence satisfies the membership condition; otherwise,
    //     false.
    bool Check(Evidence evidence);
    //
    // Summary:
    //     Creates an equivalent copy of the membership condition.
    //
    // Returns:
    //     A new, identical copy of the current membership condition.
    IMembershipCondition Copy();
  }
}
