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
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Diagnostics.Contracts;

namespace System.Security.Principal
{
  // Summary:
  //     Represents a Windows user.
  //[Serializable]
  //[ComVisible(true)]
  public class WindowsIdentity //: IIdentity, ISerializable, IDeserializationCallback, IDisposable
  {
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.WindowsIdentity
    //     class for the user represented by the specified Windows account token.
    //
    // Parameters:
    //   userToken:
    //     The account token for the user on whose behalf the code is running.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     userToken is 0.  -or- userToken is duplicated and invalid for impersonation.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the correct permissions. -or- A Win32 error occurred.
    public WindowsIdentity(IntPtr userToken)
    {
      Contract.Requires(userToken != IntPtr.Zero);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.WindowsIdentity
    //     class for the user represented by the specified User Principal Name (UPN).
    //
    // Parameters:
    //   sUserPrincipalName:
    //     The UPN for the user on whose behalf the code is running.
    //
    // Exceptions:
    //   System.UnauthorizedAccessException:
    //     Windows returned the Windows NT status code STATUS_ACCESS_DENIED.
    //
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the correct permissions.
    public WindowsIdentity(string sUserPrincipalName)
    {

    }
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.WindowsIdentity
    //     class for the user represented by the specified Windows account token and
    //     the specified authentication type.
    //
    // Parameters:
    //   userToken:
    //     The account token for the user on whose behalf the code is running.
    //
    //   type:
    //     The type of authentication used to identify the user.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     userToken is 0.  -or- userToken is duplicated and invalid for impersonation.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the correct permissions. -or- A Win32 error occurred.
    public WindowsIdentity(IntPtr userToken, string type)
    {
      Contract.Requires(userToken != IntPtr.Zero);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.WindowsIdentity
    //     class for the user represented by information in a System.Runtime.Serialization.SerializationInfo
    //     stream.
    //
    // Parameters:
    //   info:
    //     The System.Runtime.Serialization.SerializationInfo containing the account
    //     information for the user.
    //
    //   context:
    //     A System.Runtime.Serialization.StreamingContext that indicates the stream
    //     characteristics.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     A System.Security.Principal.WindowsIdentity cannot be serialized across processes.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the correct permissions. -or- A Win32 error occurred.
/*    public WindowsIdentity(SerializationInfo info, StreamingContext context)
    {
    }
 */ 
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.WindowsIdentity
    //     class for the user represented by the specified User Principal Name (UPN)
    //     and the specified authentication type.
    //
    // Parameters:
    //   sUserPrincipalName:
    //     The UPN for the user on whose behalf the code is running.
    //
    //   type:
    //     The type of authentication used to identify the user.
    //
    // Exceptions:
    //   System.UnauthorizedAccessException:
    //     Windows returned the Windows NT status code STATUS_ACCESS_DENIED.
    //
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the correct permissions.
    public WindowsIdentity(string sUserPrincipalName, string type) 
    { }
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.WindowsIdentity
    //     class for the user represented by the specified Windows account token, the
    //     specified authentication type, and the specified Windows account type.
    //
    // Parameters:
    //   userToken:
    //     The account token for the user on whose behalf the code is running.
    //
    //   type:
    //     The type of authentication used to identify the user.
    //
    //   acctType:
    //     One of the System.Security.Principal.WindowsAccountType values.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     userToken is 0.  -or- userToken is duplicated and invalid for impersonation.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the correct permissions. -or- A Win32 error occurred.

    //public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType);
    
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.WindowsIdentity
    //     class for the user represented by the specified Windows account token, the
    //     specified authentication type, the specified Windows account type, and the
    //     specified authentication status.
    //
    // Parameters:
    //   userToken:
    //     The account token for the user on whose behalf the code is running.
    //
    //   type:
    //     The type of authentication used to identify the user.
    //
    //   acctType:
    //     One of the System.Security.Principal.WindowsAccountType values.
    //
    //   isAuthenticated:
    //     true to indicate that the user is authenticated; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     userToken is 0.  -or- userToken is duplicated and invalid for impersonation.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the correct permissions. -or- A Win32 error occurred.
    
    //public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated);

    // Summary:
    //     Gets the type of authentication used to identify the user.
    //
    // Returns:
    //     The type of authentication used to identify the user.
    
    //public string AuthenticationType { get; }

    //
    // Summary:
    //     Gets the groups the current Windows user belongs to.
    //
    // Returns:
    //     An System.Security.Principal.IdentityReferenceCollection object representing
    //     the groups the current Windows user belongs to.
    
    //public IdentityReferenceCollection Groups { get; }
    
    //
    // Summary:
    //     Gets the impersonation level for the user.
    //
    // Returns:
    //     One of the System.Management.ImpersonationLevel values.
    
    //public TokenImpersonationLevel ImpersonationLevel { get; }
    
    //
    // Summary:
    //     Gets a value indicating whether the user account is identified as an anonymous
    //     account by the system.
    //
    // Returns:
    //     true if the user account is an anonymous account; otherwise, false.
    //public virtual bool IsAnonymous { get; }
    //
    // Summary:
    //     Gets a value indicating whether the user has been authenticated by Windows.
    //
    // Returns:
    //     true if the user was authenticated; otherwise, false.
    //public virtual bool IsAuthenticated { get; }
    //
    // Summary:
    //     Gets a value indicating whether the user account is identified as a System.Security.Principal.WindowsAccountType.Guest
    //     account by the system.
    //
    // Returns:
    //     true if the user account is a System.Security.Principal.WindowsAccountType.Guest
    //     account; otherwise, false.
    //public virtual bool IsGuest { get; }
    //
    // Summary:
    //     Gets a value indicating whether the user account is identified as a System.Security.Principal.WindowsAccountType.System
    //     account by the system.
    //
    // Returns:
    //     true if the user account is a System.Security.Principal.WindowsAccountType.System
    //     account; otherwise, false.
    //public virtual bool IsSystem { get; }
    //
    // Summary:
    //     Gets the user's Windows logon name.
    //
    // Returns:
    //     The Windows logon name of the user on whose behalf the code is being run.
    public virtual string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the security identifier (SID) for the token owner.
    //
    // Returns:
    //     A System.Security.Principal.SecurityIdentifier object for the token owner.
    //public SecurityIdentifier Owner { get; }
    //
    // Summary:
    //     Gets the Windows account token for the user.
    //
    // Returns:
    //     The handle of the access token associated with the current execution thread.
    //public virtual IntPtr Token { get; }
    //
    // Summary:
    //     Gets the security identifier (SID) for the user.
    //
    // Returns:
    //     A System.Security.Principal.SecurityIdentifier object for the user.

    //public SecurityIdentifier User { get; }

    // Summary:
    //     Releases all resources used by the System.Security.Principal.WindowsIdentity.
    //public void Dispose();
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Security.Principal.WindowsIdentity
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
   // protected virtual void Dispose(bool disposing);
    //
    // Summary:
    //     Returns a System.Security.Principal.WindowsIdentity object that represents
    //     an anonymous user.
    //
    // Returns:
    //     A System.Security.Principal.WindowsIdentity object that represents an anonymous
    //     user.
    public static WindowsIdentity GetAnonymous()
    {
      Contract.Ensures(Contract.Result<WindowsIdentity>() != null);

      return default(WindowsIdentity);
    }
    //
    // Summary:
    //     Returns a System.Security.Principal.WindowsIdentity object that represents
    //     the current Windows user.
    //
    // Returns:
    //     A System.Security.Principal.WindowsIdentity object that represents the current
    //     user.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The caller does not have the correct permissions.
    public static WindowsIdentity GetCurrent()
    {
      Contract.Ensures(Contract.Result<WindowsIdentity>() != null);

      return default(WindowsIdentity);
    }
    //
    // Summary:
    //     Returns a System.Security.Principal.WindowsIdentity object that represents
    //     the Windows identity for either the thread or the process, depending on the
    //     value of the ifImpersonating parameter.
    //
    // Parameters:
    //   ifImpersonating:
    //     true to return the System.Security.Principal.WindowsIdentity only if the
    //     thread is currently impersonating; false to return the System.Security.Principal.WindowsIdentity
    //     of the thread if it is impersonating or the System.Security.Principal.WindowsIdentity
    //     of the process if the thread is not currently impersonating.
    //
    // Returns:
    //     A System.Security.Principal.WindowsIdentity object that represents a Windows
    //     user.
    public static WindowsIdentity GetCurrent(bool ifImpersonating)
    {
      Contract.Ensures(ifImpersonating || Contract.Result<WindowsIdentity>() != null);

      return default(WindowsIdentity);
    }
    //
    // Summary:
    //     Returns a System.Security.Principal.WindowsIdentity object that represents
    //     the current Windows user, using the specified desired token access level.
    //
    // Parameters:
    //   desiredAccess:
    //     A bitwise combination of the System.Security.Principal.TokenAccessLevels
    //     values.
    //
    // Returns:
    //     A System.Security.Principal.WindowsIdentity object that represents the current
    //     user.
    //public static WindowsIdentity GetCurrent(TokenAccessLevels desiredAccess);
    //
    // Summary:
    //     Impersonates the user represented by the System.Security.Principal.WindowsIdentity
    //     object.
    //
    // Returns:
    //     A System.Security.Principal.WindowsImpersonationContext object that represents
    //     the Windows user prior to impersonation; this can be used to revert to the
    //     original user's context.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     An anonymous identity attempted to perform an impersonation.
    //
    //   System.Security.SecurityException:
    //     A Win32 error occurred.
    //public virtual WindowsImpersonationContext Impersonate();
    //
    // Summary:
    //     Impersonates the user represented by the specified user token.
    //
    // Parameters:
    //   userToken:
    //     The handle of a Windows account token. This token is usually retrieved through
    //     a call to unmanaged code, such as a call to the Win32 API LogonUser function.
    //     For more information on calls to unmanaged code, see Consuming Unmanaged
    //     DLL Functions.
    //
    // Returns:
    //     A System.Security.Principal.WindowsImpersonationContext object that represents
    //     the Windows user prior to impersonation; this object can be used to revert
    //     to the original user's context.
    //
    // Exceptions:
    //   System.UnauthorizedAccessException:
    //     Windows returned the Windows NT status code STATUS_ACCESS_DENIED.
    //
    //   System.OutOfMemoryException:
    //     There is insufficient memory available.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the correct permissions.
    //public static WindowsImpersonationContext Impersonate(IntPtr userToken);
  }
}
#endif