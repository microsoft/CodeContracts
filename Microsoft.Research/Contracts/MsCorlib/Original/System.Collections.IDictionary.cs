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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Collections {
  // Summary:
  //     Represents a nongeneric collection of key/value pairs.
  [ComVisible(true)]
  public interface IDictionary : ICollection, IEnumerable {
    // Summary:
    //     Gets a value indicating whether the System.Collections.IDictionary object
    //     has a fixed size.
    //
    // Returns:
    //     true if the System.Collections.IDictionary object has a fixed size; otherwise,
    //     false.
    bool IsFixedSize { get; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Collections.IDictionary object
    //     is read-only.
    //
    // Returns:
    //     true if the System.Collections.IDictionary object is read-only; otherwise,
    //     false.
    bool IsReadOnly { get; }
    //
    // Summary:
    //     Gets an System.Collections.ICollection object containing the keys of the
    //     System.Collections.IDictionary object.
    //
    // Returns:
    //     An System.Collections.ICollection object containing the keys of the System.Collections.IDictionary
    //     object.
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    ICollection! Keys { [ElementCollection] get; }
    //
    // Summary:
    //     Gets an System.Collections.ICollection object containing the values in the
    //     System.Collections.IDictionary object.
    //
    // Returns:
    //     An System.Collections.ICollection object containing the values in the System.Collections.IDictionary
    //     object.
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    ICollection! Values { [ElementCollection] get; }

    // Summary:
    //     Gets or sets the element with the specified key.
    //
    // Parameters:
    //   key:
    //     The key of the element to get or set.
    //
    // Returns:
    //     The element with the specified key.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The property is set and the System.Collections.IDictionary object is read-only.-or-
    //     The property is set, key does not exist in the collection, and the System.Collections.IDictionary
    //     has a fixed size.
    //
    //   System.ArgumentNullException:
    //     key is null.
    object this[object! key] { get; set; }

    // Summary:
    //     Adds an element with the provided key and value to the System.Collections.IDictionary
    //     object.
    //
    // Parameters:
    //   value:
    //     The System.Object to use as the value of the element to add.
    //
    //   key:
    //     The System.Object to use as the key of the element to add.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An element with the same key already exists in the System.Collections.IDictionary
    //     object.
    //
    //   System.ArgumentNullException:
    //     key is null.
    //
    //   System.NotSupportedException:
    //     The System.Collections.IDictionary is read-only.-or- The System.Collections.IDictionary
    //     has a fixed size.
    [WriteConfined]
    void Add(object! key, object value);
    //
    // Summary:
    //     Removes all elements from the System.Collections.IDictionary object.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.IDictionary object is read-only.
    [WriteConfined]
    void Clear();
    //
    // Summary:
    //     Determines whether the System.Collections.IDictionary object contains an
    //     element with the specified key.
    //
    // Parameters:
    //   key:
    //     The key to locate in the System.Collections.IDictionary object.
    //
    // Returns:
    //     true if the System.Collections.IDictionary contains an element with the key;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     key is null.
    [Pure]
    bool Contains(object! key);
    //
    // Summary:
    //     Returns an System.Collections.IDictionaryEnumerator object for the System.Collections.IDictionary
    //     object.
    //
    // Returns:
    //     An System.Collections.IDictionaryEnumerator object for the System.Collections.IDictionary
    //     object.
    
    [Pure] [GlobalAccess(false)] [Escapes(true,false)]
    [return: Fresh]
    IDictionaryEnumerator! GetEnumerator();
      CodeContract.Ensures(result.IsNew);
    //
    // Summary:
    //     Removes the element with the specified key from the System.Collections.IDictionary
    //     object.
    //
    // Parameters:
    //   key:
    //     The key of the element to remove.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Collections.IDictionary object is read-only.-or- The System.Collections.IDictionary
    //     has a fixed size.
    //
    //   System.ArgumentNullException:
    //     key is null.
    [WriteConfined]
    void Remove(object! key);
  }
}
