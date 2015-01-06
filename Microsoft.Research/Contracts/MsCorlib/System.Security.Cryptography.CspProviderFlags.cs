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
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  // Summary:
  //     Specifies flags that modify the behavior of the cryptographic service providers
  //     (CSP).
  public enum CspProviderFlags
  {
    // Summary:
    //     Don't specify any settings.
    NoFlags = 0,
    //
    // Summary:
    //     Use key information from the computer's key store.
    UseMachineKeyStore = 1,
    //
    // Summary:
    //     Use key information from the default key container.
    UseDefaultKeyContainer = 2,
    //
    // Summary:
    //     Use key information that can not be exported.
    UseNonExportableKey = 4,
    //
    // Summary:
    //     Use key information from the current key.
    UseExistingKey = 8,
    //
    // Summary:
    //     Allow a key to be exported for archival or recovery.
    UseArchivableKey = 16,
    //
    // Summary:
    //     Notify the user through a dialog box or another method when certain actions
    //     are attempting to use a key. This flag is not compatible with the System.Security.Cryptography.CspProviderFlags.NoPrompt
    //     flag.
    UseUserProtectedKey = 32,
    //
    // Summary:
    //     Prevent the CSP from displaying any user interface (UI) for this context.
    NoPrompt = 64,
  }
}
