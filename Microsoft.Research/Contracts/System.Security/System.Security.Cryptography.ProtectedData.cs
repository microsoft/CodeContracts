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

namespace System.Security.Cryptography
{
  // Summary:
  //     Provides methods for protecting and unprotecting data. This class cannot
  //     be inherited.
  public sealed class ProtectedData
  {
    private ProtectedData() { }

    // Summary:
    //     Protects the userData parameter and returns a byte array.
    //
    // Parameters:
    //   userData:
    //     A byte array containing data to protect.
    //
    //   optionalEntropy:
    //     An additional byte array used to encrypt the data.
    //
    //   scope:
    //     One of the System.Security.Cryptography.DataProtectionScope values.
    //
    // Returns:
    //     A byte array representing the encrypted data.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The userData parameter is null.
    //
    //   System.Security.Cryptography.CryptographicException:
    //     The cryptographic protection failed.
    //
    //   System.PlatformNotSupportedException:
    //     The operating system does not support this method.
    //
    //   System.OutOfMemoryException:
    //     The system ran out of memory while encrypting the data.
    public static byte[] Protect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
    {
      Contract.Requires(userData != null);

      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }
    //
    // Summary:
    //     Unprotects the encryptedData parameter and returns a byte array.
    //
    // Parameters:
    //   encryptedData:
    //     A byte array containing data encrypted using the System.Security.Cryptography.ProtectedData.Protect(System.Byte[],System.Byte[],System.Security.Cryptography.DataProtectionScope)
    //     method.
    //
    //   optionalEntropy:
    //     An additional byte array used to encrypt the data.
    //
    //   scope:
    //     One of the System.Security.Cryptography.DataProtectionScope values.
    //
    // Returns:
    //     A byte array representing the unprotected data.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The encryptedData parameter is null.
    //
    //   System.Security.Cryptography.CryptographicException:
    //     The cryptographic protection failed.
    //
    //   System.PlatformNotSupportedException:
    //     The operating system does not support this method.
    //
    //   System.OutOfMemoryException:
    //     Out of memory.
    public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)
    {
      Contract.Requires(encryptedData != null);

      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }
  }
}
