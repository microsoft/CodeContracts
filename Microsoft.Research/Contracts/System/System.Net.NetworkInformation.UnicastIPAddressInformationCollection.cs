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


#if !SILVERLIGHT

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace System.Net.NetworkInformation
{
  // Summary:
  //     Stores a set of System.Net.NetworkInformation.UnicastIPAddressInformation
  //     types.
  public class UnicastIPAddressInformationCollection : ICollection<UnicastIPAddressInformation>, IEnumerable<UnicastIPAddressInformation>, IEnumerable
  {
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkInformation.UnicastIPAddressInformationCollection
    //     class.
    extern protected internal UnicastIPAddressInformationCollection();

    // Summary:
    //     Gets the number of System.Net.NetworkInformation.UnicastIPAddressInformation
    //     types in this collection.
    //
    // Returns:
    //     An System.Int32 value that contains the number of System.Net.NetworkInformation.UnicastIPAddressInformation
    //     types in this collection.
    extern public virtual int Count { get; }
    //
    // Summary:
    //     Gets a value that indicates whether access to this collection is read-only.
    //
    // Returns:
    //     true in all cases.
    extern public virtual bool IsReadOnly { get; }

    // Summary:
    //     Gets the System.Net.NetworkInformation.UnicastIPAddressInformation instance
    //     at the specified index in the collection.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the element.
    //
    // Returns:
    //     The System.Net.NetworkInformation.UnicastIPAddressInformation at the specified
    //     location.
    extern public virtual UnicastIPAddressInformation this[int index] { get; }

    // Summary:
    //     Throws a System.NotSupportedException because this operation is not supported
    //     for this collection.
    //
    // Parameters:
    //   address:
    //     The object to be added to the collection.
    extern public virtual void Add(UnicastIPAddressInformation address);
    //
    // Summary:
    //     Throws a System.NotSupportedException because this operation is not supported
    //     for this collection.
    extern public virtual void Clear();
    //
    // Summary:
    //     Checks whether the collection contains the specified System.Net.NetworkInformation.UnicastIPAddressInformation
    //     object.
    //
    // Parameters:
    //   address:
    //     The System.Net.NetworkInformation.UnicastIPAddressInformation object to be
    //     searched in the collection.
    //
    // Returns:
    //     true if the System.Net.NetworkInformation.UnicastIPAddressInformation object
    //     exists in the collection; otherwise, false.
    extern public virtual bool Contains(UnicastIPAddressInformation address);
    //
    // Summary:
    //     Copies the elements in this collection to a one-dimensional array of type
    //     System.Net.NetworkInformation.UnicastIPAddressInformation.
    //
    // Parameters:
    //   array:
    //     A one-dimensional array that receives a copy of the collection.
    //
    //   offset:
    //     The zero-based index in array at which the copy begins.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.
    //
    //   System.ArgumentException:
    //     array is multidimensional.-or- offset is equal to or greater than the length
    //     of array.-or- The number of elements in this System.Net.NetworkInformation.UnicastIPAddressInformationCollection
    //     is greater than the available space from offset to the end of the destination
    //     array.
    //
    //   System.InvalidCastException:
    //     The elements in this System.Net.NetworkInformation.UnicastIPAddressInformationCollection
    //     cannot be cast automatically to the type of the destination array.
    extern public virtual void CopyTo(UnicastIPAddressInformation[] array, int offset);
    //
    // Summary:
    //     Returns an object that can be used to iterate through this collection.
    //
    // Returns:
    //     An object that implements the System.Collections.IEnumerator interface and
    //     provides access to the System.Net.NetworkInformation.IPUnicastAddressInformation
    //     types in this collection.
    extern public virtual IEnumerator<UnicastIPAddressInformation> GetEnumerator();
    extern IEnumerator IEnumerable.GetEnumerator();
    //
    // Summary:
    //     Throws a System.NotSupportedException because the collection is read-only
    //     and elements cannot be removed.
    //
    // Parameters:
    //   address:
    //     The object to be removed.
    //
    // Returns:
    //     Always throws a System.NotSupportedException.
    extern public virtual bool Remove(UnicastIPAddressInformation address);
  }
}

#endif