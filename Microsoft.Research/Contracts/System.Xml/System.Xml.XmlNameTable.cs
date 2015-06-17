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
using System.Diagnostics.Contracts;

namespace System.Xml
{
  // Summary:
  //     Table of atomized string objects.
  public /*abstract*/ class XmlNameTable
  {
    protected XmlNameTable() { }

    // Summary:
    //     Initializes a new instance of the System.Xml.XmlNameTable class.
    //protected XmlNameTable();

    // Summary:
    //     When overridden in a derived class, atomizes the specified string and adds
    //     it to the XmlNameTable.
    //
    // Parameters:
    //   array:
    //     The name to add.
    //
    // Returns:
    //     The new atomized string or the existing one if it already exists.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.
    public virtual string Add(string array)
    {
      Contract.Requires(array != null);
      Contract.Ensures(Contract.Result<String>() != null);

      return default(string);
    }
    //
    // Summary:
    //     When overridden in a derived class, atomizes the specified string and adds
    //     it to the XmlNameTable.
    //
    // Parameters:
    //   array:
    //     The character array containing the name to add.
    //
    //   offset:
    //     Zero-based index into the array specifying the first character of the name.
    //
    //   length:
    //     The number of characters in the name.
    //
    // Returns:
    //     The new atomized string or the existing one if it already exists. If length
    //     is zero, String.Empty is returned.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     0 > offset -or- offset >= array.Length -or- length > array.Length The above
    //     conditions do not cause an exception to be thrown if length =0.
    //
    //   System.ArgumentOutOfRangeException:
    //     length < 0.
    public virtual string Add(char[] array, int offset, int length)
    {
      Contract.Requires(array != null);

      Contract.Requires(length >= 0);
      Contract.Requires(offset >= 0);
      Contract.Requires(offset < array.Length);
      Contract.Requires(length < array.Length);
      Contract.Ensures(Contract.Result<String>() != null);

      return default(string);
    }
    //
    // Summary:
    //     When overridden in a derived class, gets the atomized string containing the
    //     same value as the specified string.
    //
    // Parameters:
    //   array:
    //     The name to look up.
    //
    // Returns:
    //     The atomized string or null if the string has not already been atomized.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is null.
    public virtual string Get(string array)
    {
      Contract.Requires(array != null);

      return default(string);
    }

    //
    // Summary:
    //     When overridden in a derived class, gets the atomized string containing the
    //     same characters as the specified range of characters in the given array.
    //
    // Parameters:
    //   array:
    //     The character array containing the name to look up.
    //
    //   offset:
    //     The zero-based index into the array specifying the first character of the
    //     name.
    //
    //   length:
    //     The number of characters in the name.
    //
    // Returns:
    //     The atomized string or null if the string has not already been atomized.
    //     If length is zero, String.Empty is returned.
    //
    // Exceptions:
    //   System.IndexOutOfRangeException:
    //     0 > offset -or- offset >= array.Length -or- length > array.Length The above
    //     conditions do not cause an exception to be thrown if length =0.
    //
    //   System.ArgumentOutOfRangeException:
    //     length < 0.
    public virtual string Get(char[] array, int offset, int length)
    {
      Contract.Requires(array != null);

      Contract.Requires(offset >= 0);
      Contract.Requires(offset < array.Length);

      Contract.Requires(length < array.Length);
      Contract.Requires(length >= 0);

      return default(string);
    }
  }

}