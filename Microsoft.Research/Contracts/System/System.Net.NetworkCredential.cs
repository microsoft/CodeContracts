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

#region Assembly System.dll, v4.0.30319
// C:\Program Files\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.dll
#endregion

using System;
using System.Runtime;
using System.Security;

namespace System.Net {
  // Summary:
  //     Provides credentials for password-based authentication schemes such as basic,
  //     digest, NTLM, and Kerberos authentication.
  public class NetworkCredential : ICredentials, ICredentialsByHost {
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkCredential class.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    extern public NetworkCredential();
    //
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkCredential class with
    //     the specified user name and password.
    //
    // Parameters:
    //   userName:
    //     The user name associated with the credentials.
    //
    //   password:
    //     The password for the user name associated with the credentials.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Security.SecureString class is not supported on this platform.
    //extern public NetworkCredential(string userName, SecureString password);
    //
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkCredential class with
    //     the specified user name and password.
    //
    // Parameters:
    //   userName:
    //     The user name associated with the credentials.
    //
    //   password:
    //     The password for the user name associated with the credentials.
    extern public NetworkCredential(string userName, string password);
    //
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkCredential class with
    //     the specified user name, password, and domain.
    //
    // Parameters:
    //   userName:
    //     The user name associated with the credentials.
    //
    //   password:
    //     The password for the user name associated with the credentials.
    //
    //   domain:
    //     The domain associated with these credentials.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Security.SecureString class is not supported on this platform.
    //extern public NetworkCredential(string userName, SecureString password, string domain);
    //
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkCredential class with
    //     the specified user name, password, and domain.
    //
    // Parameters:
    //   userName:
    //     The user name associated with the credentials.
    //
    //   password:
    //     The password for the user name associated with the credentials.
    //
    //   domain:
    //     The domain associated with these credentials.
    extern public NetworkCredential(string userName, string password, string domain);

    // Summary:
    //     Gets or sets the domain or computer name that verifies the credentials.
    //
    // Returns:
    //     The name of the domain associated with the credentials.
    public string Domain { get; set; }
    //
    // Summary:
    //     Gets or sets the password for the user name associated with the credentials.
    //
    // Returns:
    //     The password associated with the credentials. If this System.Net.NetworkCredential
    //     instance was initialized with the password parameter set to null, then the
    //     System.Net.NetworkCredential.Password property will return an empty string.
    public string Password { get; set; }
    //
    // Summary:
    //     Gets or sets the password as a System.Security.SecureString instance.
    //
    // Returns:
    //     The password for the user name associated with the credentials.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Security.SecureString class is not supported on this platform.
    //public SecureString SecurePassword { get; set; }
    //
    // Summary:
    //     Gets or sets the user name associated with the credentials.
    //
    // Returns:
    //     The user name associated with the credentials.
    public string UserName { get; set; }

    // Summary:
    //     Returns an instance of the System.Net.NetworkCredential class for the specified
    //     Uniform Resource Identifier (URI) and authentication type.
    //
    // Parameters:
    //   uri:
    //     The URI that the client provides authentication for.
    //
    //   authType:
    //     The type of authentication requested, as defined in the System.Net.IAuthenticationModule.AuthenticationType
    //     property.
    //
    // Returns:
    //     A System.Net.NetworkCredential object.
    extern public NetworkCredential GetCredential(Uri uri, string authType);
    //
    // Summary:
    //     Returns an instance of the System.Net.NetworkCredential class for the specified
    //     host, port, and authentication type.
    //
    // Parameters:
    //   host:
    //     The host computer that authenticates the client.
    //
    //   port:
    //     The port on the host that the client communicates with.
    //
    //   authenticationType:
    //     The type of authentication requested, as defined in the System.Net.IAuthenticationModule.AuthenticationType
    //     property.
    //
    // Returns:
    //     A System.Net.NetworkCredential for the specified host, port, and authentication
    //     protocol, or null if there are no credentials available for the specified
    //     host, port, and authentication protocol.
    extern public NetworkCredential GetCredential(string host, int port, string authenticationType);
  }
}

#endif