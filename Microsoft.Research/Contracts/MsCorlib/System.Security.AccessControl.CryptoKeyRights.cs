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

namespace System.Security.AccessControl {
  // Summary:
  //     Specifies the cryptographic key operation for which an authorization rule
  //     controls access or auditing.
  [Flags]
  public enum CryptoKeyRights {
    GenericRead = -2147483648,
    //
    // Summary:
    //     Read the key data.
    ReadData = 1,
    //
    // Summary:
    //     Write key data.
    WriteData = 2,
    //
    // Summary:
    //     Read extended attributes of the key.
    ReadExtendedAttributes = 8,
    //
    // Summary:
    //     Write extended attributes of the key.
    WriteExtendedAttributes = 16,
    //
    // Summary:
    //     Read attributes of the key.
    ReadAttributes = 128,
    //
    // Summary:
    //     Write attributes of the key.
    WriteAttributes = 256,
    //
    // Summary:
    //     Delete the key.
    Delete = 65536,
    //
    // Summary:
    //     Read permissions for the key.
    ReadPermissions = 131072,
    //
    // Summary:
    //     Change permissions for the key.
    ChangePermissions = 262144,
    //
    // Summary:
    //     Take ownership of the key.
    TakeOwnership = 524288,
    //
    // Summary:
    //     Use the key for synchronization.
    Synchronize = 1048576,
    //
    // Summary:
    //     Full control of the key.
    FullControl = 2032027,
    GenericAll = 268435456,
    GenericExecute = 536870912,
    GenericWrite = 1073741824,
  }
}
