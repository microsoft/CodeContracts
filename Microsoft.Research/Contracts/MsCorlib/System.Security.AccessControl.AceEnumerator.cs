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
using System.Collections;

namespace System.Security.AccessControl
{
  // Summary:
  //     Provides the ability to iterate through the access control entries (ACEs)
  //     in an access control list (ACL).
  public sealed class AceEnumerator 
  {
    // Summary:
    //     Gets the current element in the System.Security.AccessControl.GenericAce
    //     collection. This property gets the type-friendly version of the object.
    //
    // Returns:
    //     The current element in the System.Security.AccessControl.GenericAce collection.
    extern public GenericAce Current { get; }

    // Summary:
    //     Advances the enumerator to the next element of the System.Security.AccessControl.GenericAce
    //     collection.
    //
    // Returns:
    //     true if the enumerator was successfully advanced to the next element; false
    //     if the enumerator has passed the end of the collection.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The collection was modified after the enumerator was created.
    extern public bool MoveNext();
    //
    // Summary:
    //     Sets the enumerator to its initial position, which is before the first element
    //     in the System.Security.AccessControl.GenericAce collection.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The collection was modified after the enumerator was created.
    extern public void Reset();
  }
}
