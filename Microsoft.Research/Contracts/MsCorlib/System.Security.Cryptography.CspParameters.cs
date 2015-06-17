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
using System.Security;
using System.Security.AccessControl;

namespace System.Security.Cryptography {
  // Summary:
  //     Contains parameters that are passed to the cryptographic service provider
  //     (CSP) that performs cryptographic computations. This class cannot be inherited.
  [ComVisible(true)]
  public sealed class CspParameters {
    // Summary:
    //     Represents the key container name for System.Security.Cryptography.CspParameters.
    public string KeyContainerName;
    //
    // Summary:
    //     Specifies whether an asymmetric key is created as a signature key or an exchange
    //     key.
    public int KeyNumber;
    //
    // Summary:
    //     Represents the provider name for System.Security.Cryptography.CspParameters.
    public string ProviderName;
    //
    // Summary:
    //     Represents the provider type code for System.Security.Cryptography.CspParameters.
    public int ProviderType;

    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.CspParameters
    //     class.
    //
    // Returns:
    //     The newly created instance of System.Security.Cryptography.CspParameters.
    public CspParameters();
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.CspParameters
    //     class with the specified provider type code.
    //
    // Parameters:
    //   dwTypeIn:
    //     A provider type code that specifies the kind of provider to create.
    public CspParameters(int dwTypeIn);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.CspParameters
    //     class with the specified provider type code and name.
    //
    // Parameters:
    //   dwTypeIn:
    //     A provider type code that specifies the kind of provider to create.
    //
    //   strProviderNameIn:
    //     A provider name.
    public CspParameters(int dwTypeIn, string strProviderNameIn);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.CspParameters
    //     class with the specified provider type code and name, and the specified container
    //     name.
    //
    // Parameters:
    //   dwTypeIn:
    //     The provider type code that specifies the kind of provider to create.
    //
    //   strProviderNameIn:
    //     A provider name.
    //
    //   strContainerNameIn:
    //     A container name.
    public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.CspParameters
    //     class using a provider type, a provider name, a container name, access information,
    //     and a handle to an unmanaged smart card password dialog.
    //
    // Parameters:
    //   providerType:
    //     The provider type code that specifies the kind of provider to create.
    //
    //   providerName:
    //     A provider name.
    //
    //   keyContainerName:
    //     A container name.
    //
    //   cryptoKeySecurity:
    //     A System.Security.AccessControl.CryptoKeySecurity object that represents
    //     access rights and audit rules for the container.
    //
    //   parentWindowHandle:
    //     A handle to the parent window for a smart card password dialog.
    public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, IntPtr parentWindowHandle);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.CspParameters
    //     class using a provider type, a provider name, a container name, access information,
    //     and a password associated with a smart card key.
    //
    // Parameters:
    //   providerType:
    //     The provider type code that specifies the kind of provider to create.
    //
    //   providerName:
    //     A provider name.
    //
    //   keyContainerName:
    //     A container name.
    //
    //   cryptoKeySecurity:
    //     A System.Security.AccessControl.CryptoKeySecurity object that represents
    //     access rights and audit rules for a container.
    //
    //   keyPassword:
    //     A password associated with a smart card key.
    public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, SecureString keyPassword);

    // Summary:
    //     Gets or sets a System.Security.AccessControl.CryptoKeySecurity object that
    //     represents access rights and audit rules for a container.
    //
    // Returns:
    //     A System.Security.AccessControl.CryptoKeySecurity object that represents
    //     access rights and audit rules for a container.
    public CryptoKeySecurity CryptoKeySecurity { get; set; }
    //
    // Summary:
    //     Represents the flags for System.Security.Cryptography.CspParameters.
    //
    // Returns:
    //     The flags for System.Security.Cryptography.CspParameters.
    public CspProviderFlags Flags { get; set; }
    //
    // Summary:
    //     Gets or sets a password associated with a smart card key.
    //
    // Returns:
    //     A password associated with a smart card key.
    public SecureString KeyPassword { get; set; }
    //
    // Summary:
    //     Gets or sets a handle to the unmanaged parent window for a smart card password
    //     dialog.
    //
    // Returns:
    //     A handle to the parent window for a smart card password dialog.
    public IntPtr ParentWindowHandle { get; set; }
  }
}
