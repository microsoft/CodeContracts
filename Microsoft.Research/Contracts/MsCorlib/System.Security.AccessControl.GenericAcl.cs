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
using System.Reflection;

namespace System.Security.AccessControl
{
  // Summary:
  //     Represents an access control list (ACL) and is the base class for the System.Security.AccessControl.CommonAcl,
  //     System.Security.AccessControl.DiscretionaryAcl, System.Security.AccessControl.RawAcl,
  //     and System.Security.AccessControl.SystemAcl classes.
  public abstract class GenericAcl 
  {
    // Summary:
    //     The revision level of the current System.Security.AccessControl.GenericAcl.
    //     This value is returned by the System.Security.AccessControl.GenericAcl.Revision
    //     property for Access Control Lists (ACLs) that are not associated with Directory
    //     Services objects.
    public static readonly byte AclRevision;
    //
    // Summary:
    //     The revision level of the current System.Security.AccessControl.GenericAcl.
    //     This value is returned by the System.Security.AccessControl.GenericAcl.Revision
    //     property for Access Control Lists (ACLs) that are associated with Directory
    //     Services objects.
    public static readonly byte AclRevisionDS;
    //
    // Summary:
    //     The maximum allowed binary length of a System.Security.AccessControl.GenericAcl
    //     object.
    public static readonly int MaxBinaryLength;

    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.GenericAcl
    //     class.
    extern protected GenericAcl();

    // Summary:
    //     Gets the length, in bytes, of the binary representation of the current System.Security.AccessControl.GenericAcl
    //     object. This length should be used before marshaling the ACL into a binary
    //     array with the System.Security.AccessControl.GenericAcl.GetBinaryForm() method.
    //
    // Returns:
    //     The length, in bytes, of the binary representation of the current System.Security.AccessControl.GenericAcl
    //     object.
    public abstract int BinaryLength { get; }
    //
    // Summary:
    //     Gets the number of access control entries (ACEs) in the current System.Security.AccessControl.GenericAcl
    //     object.
    //
    // Returns:
    //     The number of ACEs in the current System.Security.AccessControl.GenericAcl
    //     object.
    public abstract int Count { get; }
    //
    // Summary:
    //     This property is always set to false. It is implemented only because it is
    //     required for the implementation of the System.Collections.ICollection interface.
    //
    // Returns:
    //     Always false.
    extern public bool IsSynchronized { get; }
    //
    // Summary:
    //     Gets the revision level of the System.Security.AccessControl.GenericAcl.
    //
    // Returns:
    //     A byte value that specifies the revision level of the System.Security.AccessControl.GenericAcl.
    public abstract byte Revision { get; }
    //
    // Summary:
    //     This property always returns null. It is implemented only because it is required
    //     for the implementation of the System.Collections.ICollection interface.
    //
    // Returns:
    //     Always returns null.
    extern public object SyncRoot { get; }

    // Summary:
    //     Gets or sets the System.Security.AccessControl.GenericAce at the specified
    //     index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the System.Security.AccessControl.GenericAce to get
    //     or set.
    //
    // Returns:
    //     The System.Security.AccessControl.GenericAce at the specified index.
    public abstract GenericAce this[int index] { get; set; }

    // Summary:
    //     Copies each System.Security.AccessControl.GenericAce of the current System.Security.AccessControl.GenericAcl
    //     into the specified array.
    //
    // Parameters:
    //   array:
    //     The array into which copies of the System.Security.AccessControl.GenericAce
    //     objects contained by the current System.Security.AccessControl.GenericAcl
    //     are placed.
    //
    //   index:
    //     The zero-based index of array where the copying begins.
    extern public void CopyTo(GenericAce[] array, int index);
    //
    // Summary:
    //     Marshals the contents of the System.Security.AccessControl.GenericAcl object
    //     into the specified byte array beginning at the specified offset.
    //
    // Parameters:
    //   binaryForm:
    //     The byte array into which the contents of the System.Security.AccessControl.GenericAcl
    //     is marshaled.
    //
    //   offset:
    //     The offset at which to start marshaling.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     offset is negative or too high to allow the entire System.Security.AccessControl.GenericAcl
    //     to be copied into array.
    public abstract void GetBinaryForm(byte[] binaryForm, int offset);
    //
    // Summary:
    //     Returns a new instance of the System.Security.AccessControl.AceEnumerator
    //     class.
    //
    // Returns:
    //     The Security.AccessControl.AceEnumerator that this method returns.
    extern public AceEnumerator GetEnumerator();

    #region ICollection Members


    public void CopyTo(Array array, int index)
    {
      throw new global::System.NotImplementedException();
    }

    #endregion
  }
}
