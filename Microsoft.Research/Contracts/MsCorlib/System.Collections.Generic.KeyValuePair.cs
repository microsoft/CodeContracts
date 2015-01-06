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

namespace System.Collections.Generic
{
  // Summary:
  //     Defines a key/value pair that can be set or retrieved.
  //
  // Type parameters:
  //   TKey:
  //     The type of the key.
  //
  //   TValue:
  //     The type of the value.
  public struct KeyValuePair<TKey, TValue>
  {
    //
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.KeyValuePair<TKey,TValue>
    //     structure with the specified key and value.
    //
    // Parameters:
    //   key:
    //     The object defined in each key/value pair.
    //
    //   value:
    //     The definition associated with key.
    public KeyValuePair(TKey key, TValue value) { }

    // Summary:
    //     Gets the key in the key/value pair.
    //
    // Returns:
    //     A TKey that is the key of the System.Collections.Generic.KeyValuePair<TKey,TValue>.
    public TKey Key { get { return default(TKey); } }
    //
    // Summary:
    //     Gets the value in the key/value pair.
    //
    // Returns:
    //     A TValue that is the value of the System.Collections.Generic.KeyValuePair<TKey,TValue>.
    public TValue Value { get { return default(TValue); } }

  }
}
