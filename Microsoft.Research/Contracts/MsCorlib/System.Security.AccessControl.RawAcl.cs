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

namespace System.Security.AccessControl
{
  // Summary:
  //     Represents an Access Control List (ACL).
  public sealed class RawAcl : GenericAcl
  {
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.RawAcl class
    //     with the specified revision level.
    //
    // Parameters:
    //   revision:
    //     The revision level of the new Access Control List (ACL).
    //
    //   capacity:
    //     The number of Access Control Entries (ACEs) this System.Security.AccessControl.RawAcl
    //     object can contain. This number is to be used only as a hint.
    public RawAcl(byte revision, int capacity);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.RawAcl class
    //     from the specified binary form.
    //
    // Parameters:
    //   binaryForm:
    //     An array of byte values that represent an Access Control List (ACL).
    //
    //   offset:
    //     The offset in the binaryForm parameter at which to begin unmarshaling data.
    public RawAcl(byte[] binaryForm, int offset);


    // Parameters:
    //   index:
    //     The position at which to add the new ACE. Specify the value of the System.Security.AccessControl.RawAcl.Count
    //     property to insert an ACE at the end of the System.Security.AccessControl.RawAcl
    //     object.
    //
    //   ace:
    //     The ACE to insert.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     offset is negative or too high to allow the entire System.Security.AccessControl.GenericAcl
    //     to be copied into array.
    public void InsertAce(int index, GenericAce ace);
    //
    // Summary:
    //     Removes the Access Control Entry (ACE) at the specified location.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the ACE to remove.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value of the index parameter is higher than the value of the System.Security.AccessControl.RawAcl.Count
    //     property minus one or is negative.
    public void RemoveAce(int index);
  }
}
