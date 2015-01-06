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
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Security {
  // Summary:
  //     Represents text that should be kept confidential. The text is encrypted for
  //     privacy when being used, and deleted from computer memory when no longer
  //     needed. This class cannot be inherited.
  public sealed class SecureString : IDisposable {
    // Summary:
    //     Initializes a new instance of the System.Security.SecureString class.
    //
    // Exceptions:
    //   System.Security.Cryptography.CryptographicException:
    //     An error occurred while encrypting or decrypting the value of this instance.
    //
    //   System.NotSupportedException:
    //     This operation is not supported on this platform.
    extern public SecureString();
    //
    // Summary:
    //     Initializes a new instance of the System.Security.SecureString class from
    //     a subarray of System.Char objects.
    //
    // Parameters:
    //   value:
    //     A pointer to an array of System.Char objects.
    //
    //   length:
    //     The number of elements of value to include in the new instance.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     value is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     length is less than zero or greater than 65536.
    //
    //   System.Security.Cryptography.CryptographicException:
    //     An error occurred while encrypting or decrypting the value of this secure
    //     string.
    //
    //   System.NotSupportedException:
    //     This operation is not supported on this platform.
    extern unsafe public SecureString(char* value, int length);

    // Summary:
    //     Gets the length of the current secure string.
    //
    // Returns:
    //     The number of System.Char objects in this secure string.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     This secure string has already been disposed.
    public int Length
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }

    // Summary:
    //     Appends a character to the end of the current secure string.
    //
    // Parameters:
    //   c:
    //     A character to append to this secure string.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     This secure string has already been disposed.
    //
    //   System.InvalidOperationException:
    //     This secure string is read-only.
    //
    //   System.ArgumentOutOfRangeException:
    //     Performing this operation would make the length of this secure string greater
    //     than 65536 characters.
    //
    //   System.Security.Cryptography.CryptographicException:
    //     An error occurred while encrypting or decrypting the value of this secure
    //     string.
    extern public void AppendChar(char c);
    //
    // Summary:
    //     Deletes the value of the current secure string.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     This secure string has already been disposed.
    //
    //   System.InvalidOperationException:
    //     This secure string is read-only.
    extern public void Clear();
    //
    // Summary:
    //     Creates a copy of the current secure string.
    //
    // Returns:
    //     A duplicate of this secure string.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     This secure string has already been disposed.
    //
    //   System.Security.Cryptography.CryptographicException:
    //     An error occurred while encrypting or decrypting the value of this secure
    //     string.
    [Pure]
    extern public SecureString Copy();
    //
    // Summary:
    //     Releases all resources used by the current System.Security.SecureString object.
    extern public void Dispose();
    //
    // Summary:
    //     Inserts a character in this secure string at the specified index position.
    //
    // Parameters:
    //   index:
    //     The index position where parameter c is inserted.
    //
    //   c:
    //     The character to insert.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     This secure string has already been disposed.
    //
    //   System.InvalidOperationException:
    //     This secure string is read-only.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero, or greater than the length of this secure string.-or-Performing
    //     this operation would make the length of this secure string greater than 65536
    //     characters.
    //
    //   System.Security.Cryptography.CryptographicException:
    //     An error occurred while encrypting or decrypting the value of this secure
    //     string.
    extern public void InsertAt(int index, char c);
    //
    // Summary:
    //     Indicates whether this secure string is marked read-only.
    //
    // Returns:
    //     true if this secure string is marked read-only; otherwise, false.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     This secure string has already been disposed.
    [Pure]
    extern public bool IsReadOnly();
    //
    // Summary:
    //     Makes the text value of this secure string read-only.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     This secure string has already been disposed.
    extern public void MakeReadOnly();
    //
    // Summary:
    //     Removes the character at the specified index position from this secure string.
    //
    // Parameters:
    //   index:
    //     The index position of a character in this secure string.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     This secure string has already been disposed.
    //
    //   System.InvalidOperationException:
    //     This secure string is read-only.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero, or greater than or equal to the length of this secure
    //     string.
    //
    //   System.Security.Cryptography.CryptographicException:
    //     An error occurred while encrypting or decrypting the value of this secure
    //     string.
    extern public void RemoveAt(int index);
    //
    // Summary:
    //     Replaces the existing character at the specified index position with another
    //     character.
    //
    // Parameters:
    //   index:
    //     The index position of an existing character in this secure string
    //
    //   c:
    //     A character that replaces the existing character.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     This secure string has already been disposed.
    //
    //   System.InvalidOperationException:
    //     This secure string is read-only.
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero, or greater than or equal to the length of this secure
    //     string.
    //
    //   System.Security.Cryptography.CryptographicException:
    //     An error occurred while encrypting or decrypting the value of this secure
    //     string.
    extern public void SetAt(int index, char c);
  }
}

#endif