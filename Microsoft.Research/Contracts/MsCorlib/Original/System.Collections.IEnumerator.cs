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

using System.Collections;
using System.Diagnostics.Contracts;
//using System.Runtime.InteropServices;

namespace System.Collections
{
    // Summary:
    //     Supports a simple iteration over a nongeneric collection.
    [ComVisible(true)]
    //[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
    public interface IEnumerator
    {
        // Summary:
        //     Gets the current element in the collection.
        //
        // Returns:
        //     The current element in the collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The enumerator is positioned before the first element of the collection or
        //     after the last element.
        
        object Current { get; }

        // Summary:
        //     Advances the enumerator to the next element of the collection.
        //
        // Returns:
        //     true if the enumerator was successfully advanced to the next element; false
        //     if the enumerator has passed the end of the collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        [WriteConfined] 
        bool MoveNext();
        //
        // Summary:
        //     Sets the enumerator to its initial position, which is before the first element
        //     in the collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        [WriteConfined] 
        void Reset();
    }
}
