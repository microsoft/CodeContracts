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
using System.Collections.Generic;
//using System.Linq;
using System.Text;

using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates {
  // Summary:
  //     Defines where and how to export the private key of an X.509 certificate.
  [Serializable]
  [ComVisible(true)]
  [Flags]
  public enum X509KeyStorageFlags {
    // Summary:
    //     The default key set is used. The user key set is usually the default.
    DefaultKeySet = 0,
    //
    // Summary:
    //     Private keys are stored in the current user store rather than the local computer
    //     store. This occurs even if the certificate specifies that the keys should
    //     go in the local computer store.
    UserKeySet = 1,
    //
    // Summary:
    //     Private keys are stored in the local computer store rather than the current
    //     user store.
    MachineKeySet = 2,
    //
    // Summary:
    //     Imported keys are marked as exportable. .
    Exportable = 4,
    //
    // Summary:
    //     Notify the user through a dialog box or other method that the key is accessed.
    //     The Cryptographic Service Provider (CSP) in use defines the precise behavior.
    UserProtected = 8,
    //
    // Summary:
    //     The key associated with a PFX file is persisted when importing a certificate.
    PersistKeySet = 16,
  }
}
