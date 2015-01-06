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
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  // Summary:
  //     Represents a collection of System.Security.Principal.IdentityReference objects
  //     and provides a means of converting sets of System.Security.Principal.IdentityReference-derived
  //     objects to System.Security.Principal.IdentityReference-derived types.
  public class IdentityReferenceCollection : ICollection<IdentityReference>, IEnumerable<IdentityReference>, IEnumerable
  {
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.IdentityReferenceCollection
    //     class with zero items in the collection.
    public IdentityReferenceCollection();
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.IdentityReferenceCollection
    //     class by using the specified initial size.
    //
    // Parameters:
    //   capacity:
    //     The initial number of items in the collection. The value of capacity is a
    //     hint only; it is not necessarily the maximum number of items created.
    public IdentityReferenceCollection(int capacity);

    // Summary:
    //     Gets the number of items in the System.Security.Principal.IdentityReferenceCollection
    //     collection.
    //
    // Returns:
    //     The number of System.Security.Principal.IdentityReference objects in the
    //     System.Security.Principal.IdentityReferenceCollection collection.
    public int Count { get; }
    //
    // Summary:
    //     Gets a value that indicates whether the System.Security.Principal.IdentityReferenceCollection
    //     collection is read-only.
    //
    // Returns:
    //     true if the System.Security.Principal.IdentityReferenceCollection collection
    //     is read-only.
    public bool IsReadOnly { get; }

    // Summary:
    //     Sets or gets the node at the specified index of the System.Security.Principal.IdentityReferenceCollection
    //     collection.
    //
    // Parameters:
    //   index:
    //     The zero-based index in the System.Security.Principal.IdentityReferenceCollection
    //     collection.
    //
    // Returns:
    //     The System.Security.Principal.IdentityReference at the specified index in
    //     the collection. If index is greater than or equal to the number of nodes
    //     in the collection, the return value is null.
    public IdentityReference this[int index] { get; set; }

    // Summary:
    //     Adds an System.Security.Principal.IdentityReference object to the System.Security.Principal.IdentityReferenceCollection
    //     collection.
    //
    // Parameters:
    //   identity:
    //     The System.Security.Principal.IdentityReference object to add to the collection.
    public void Add(IdentityReference identity);
    //
    // Summary:
    //     Clears all System.Security.Principal.IdentityReference objects from the System.Security.Principal.IdentityReferenceCollection
    //     collection.
    public void Clear();
    //
    // Summary:
    //     Indicates whether the System.Security.Principal.IdentityReferenceCollection
    //     collection contains the specified System.Security.Principal.IdentityReference
    //     object.
    //
    // Parameters:
    //   identity:
    //     The System.Security.Principal.IdentityReference object to check for.
    //
    // Returns:
    //     true if the collection contains the specified object.
    public bool Contains(IdentityReference identity);
    //
    // Summary:
    //     Copies the System.Security.Principal.IdentityReferenceCollection collection
    //     to an System.Security.Principal.IdentityReferenceCollection array, starting
    //     at the specified index.
    //
    // Parameters:
    //   array:
    //     An System.Security.Principal.IdentityReferenceCollection array object to
    //     which the System.Security.Principal.IdentityReferenceCollection collection
    //     is to be copied.
    //
    //   offset:
    //     The zero-based index in array where the System.Security.Principal.IdentityReferenceCollection
    //     collection is to be copied.
    public void CopyTo(IdentityReference[] array, int offset);
    //
    // Summary:
    //     Gets an enumerator that can be used to iterate through the System.Security.Principal.IdentityReferenceCollection
    //     collection.
    //
    // Returns:
    //     An enumerator for the System.Security.Principal.IdentityReferenceCollection
    //     collection.
    public IEnumerator<IdentityReference> GetEnumerator();
    //
    // Summary:
    //     Removes the specified System.Security.Principal.IdentityReference object
    //     from the collection.
    //
    // Parameters:
    //   identity:
    //     The System.Security.Principal.IdentityReference object to remove.
    //
    // Returns:
    //     true if the specified object was removed from the collection.
    public bool Remove(IdentityReference identity);
    //
    // Summary:
    //     Converts the objects in the collection to the specified type. Calling this
    //     method is the same as calling System.Security.Principal.IdentityReferenceCollection.Translate(System.Type,System.Boolean)
    //     with the second parameter set to false, which means that exceptions will
    //     not be thrown for items that fail conversion.
    //
    // Parameters:
    //   targetType:
    //     The type to which items in the collection are being converted.
    //
    // Returns:
    //     A System.Security.Principal.IdentityReferenceCollection collection that represents
    //     the converted contents of the original collection.
    public IdentityReferenceCollection Translate(Type targetType);
    //
    // Summary:
    //     Converts the objects in the collection to the specified type and uses the
    //     specified fault tolerance to handle or ignore errors associated with a type
    //     not having a conversion mapping.
    //
    // Parameters:
    //   targetType:
    //     The type to which items in the collection are being converted.
    //
    //   forceSuccess:
    //     A Boolean value that determines how conversion errors are handled.If forceSuccess
    //     is true, conversion errors due to a mapping not being found for the translation
    //     result in a failed conversion and exceptions being thrown.If forceSuccess
    //     is false, types that failed to convert due to a mapping not being found for
    //     the translation are copied without being converted into the collection being
    //     returned.
    //
    // Returns:
    //     A System.Security.Principal.IdentityReferenceCollection collection that represents
    //     the converted contents of the original collection.
    public IdentityReferenceCollection Translate(Type targetType, bool forceSuccess);
  }
}
