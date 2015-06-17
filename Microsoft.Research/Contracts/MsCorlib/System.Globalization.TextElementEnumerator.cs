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
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Globalization {
  // Summary:
  //     Enumerates the text elements of a string.
  [Serializable]
  [ComVisible(true)]
  public class TextElementEnumerator : IEnumerator {
    // Summary:
    //     Gets the current text element in the string.
    //
    // Returns:
    //     An object containing the current text element in the string.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The enumerator is positioned before the first text element of the string
    //     or after the last text element.
    public object Current { get; }
    //
    // Summary:
    //     Gets the index of the text element that the enumerator is currently positioned
    //     over.
    //
    // Returns:
    //     The index of the text element that the enumerator is currently positioned
    //     over.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The enumerator is positioned before the first text element of the string
    //     or after the last text element.
    public int ElementIndex { get; }

    // Summary:
    //     Gets the current text element in the string.
    //
    // Returns:
    //     A new string containing the current text element in the string being read.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The enumerator is positioned before the first text element of the string
    //     or after the last text element.
    public string GetTextElement();
    //
    // Summary:
    //     Advances the enumerator to the next text element of the string.
    //
    // Returns:
    //     true if the enumerator was successfully advanced to the next text element;
    //     false if the enumerator has passed the end of the string.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The string was modified after the enumerator was created.
    public bool MoveNext();
    //
    // Summary:
    //     Sets the enumerator to its initial position, which is before the first text
    //     element in the string.
    public void Reset();
  }
}

