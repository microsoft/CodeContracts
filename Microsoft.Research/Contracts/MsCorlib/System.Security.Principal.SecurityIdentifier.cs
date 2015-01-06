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

#if NETFRAMEWORK_4_0

using System;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Security.Principal
{
  // Summary:
  //     Represents a security identifier (SID) and provides marshaling and comparison
  //     operations for SIDs.
  public sealed class SecurityIdentifier 
  {
    // Summary:
    //     Returns the maximum size, in bytes, of the binary representation of the security
    //     identifier.
    //
    // Returns:
    //     The maximum size, in bytes, of the binary representation of the security
    //     identifier.
    public static readonly int MaxBinaryLength;
    //
    // Summary:
    //     Returns the minimum size, in bytes, of the binary representation of the security
    //     identifier.
    //
    // Returns:
    //     The minimum size, in bytes, of the binary representation of the security
    //     identifier.
    public static readonly int MinBinaryLength;

    // Summary:
    //     Initializes a new instance of the System.Security.Principal.SecurityIdentifier
    //     class by using an integer that represents the binary form of a security identifier
    //     (SID).
    //
    // Parameters:
    //   binaryForm:
    //     An integer that represents the binary form of a SID.
    public SecurityIdentifier(IntPtr binaryForm)
    { }
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.SecurityIdentifier
    //     class by using the specified security identifier (SID) in Security Descriptor
    //     Definition Language (SDDL) format.
    //
    // Parameters:
    //   sddlForm:
    //     SDDL string for the SID used to created the System.Security.Principal.SecurityIdentifier
    //     object.
    public SecurityIdentifier(string sddlForm)
    {
      Contract.Requires(sddlForm != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.SecurityIdentifier
    //     class by using a specified binary representation of a security identifier
    //     (SID).
    //
    // Parameters:
    //   binaryForm:
    //     The byte array that represents the SID.
    //
    //   offset:
    //     The byte offset to use as the starting index in binaryForm.
    public SecurityIdentifier(byte[] binaryForm, int offset)
    {
      Contract.Requires(binaryForm != null);
      Contract.Requires(offset >= 0);
      Contract.Requires((binaryForm.Length - offset) >= MinBinaryLength);
      Contract.Requires(offset < binaryForm.Length); // This is implied by the other two, as MinBinaryLength >= 0
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Security.Principal.SecurityIdentifier
    //     class by using the specified well known security identifier (SID) type and
    //     domain SID.
    //
    // Parameters:
    //   sidType:
    //     A System.Security.Principal.WellKnownSidType value.This value must not be
    //     System.Security.Principal.WellKnownSidType.WinLogonIdsSid.
    //
    //   domainSid:
    //     The domain SID. This value is required for the following System.Security.Principal.WellKnownSidType
    //     values. This parameter is ignored for any other System.Security.Principal.WellKnownSidType
    //     values.System.Security.Principal.WellKnownSidType.WinAccountAdministratorSidSystem.Security.Principal.WellKnownSidType.WinAccountGuestSidSystem.Security.Principal.WellKnownSidType.WinAccountKrbtgtSidSystem.Security.Principal.WellKnownSidType.WinAccountDomainAdminsSidSystem.Security.Principal.WellKnownSidType.WinAccountDomainUsersSidSystem.Security.Principal.WellKnownSidType.WinAccountDomainGuestsSidSystem.Security.Principal.WellKnownSidType.WinAccountComputersSidSystem.Security.Principal.WellKnownSidType.WinAccountControllersSidSystem.Security.Principal.WellKnownSidType.WinAccountCertAdminsSidSystem.Security.Principal.WellKnownSidType.WinAccountSchemaAdminsSidSystem.Security.Principal.WellKnownSidType.WinAccountEnterpriseAdminsSidSystem.Security.Principal.WellKnownSidType.WinAccountPolicyAdminsSidSystem.Security.Principal.WellKnownSidType.WinAccountRasAndIasServersSid
    public SecurityIdentifier(WellKnownSidType sidType, SecurityIdentifier domainSid)
    {
      Contract.Requires(sidType != WellKnownSidType.LogonIdsSid);
      Contract.Requires(sidType >= WellKnownSidType.NullSid);
      Contract.Requires(sidType <= WellKnownSidType.WinBuiltinTerminalServerLicenseServersSid);
    }

    // Summary:
    //     Compares two System.Security.Principal.SecurityIdentifier objects to determine
    //     whether they are not equal. They are considered not equal if they have different
    //     canonical name representations than the one returned by the System.Security.Principal.SecurityIdentifier.Value
    //     property or if one of the objects is null and the other is not.
    //
    // Parameters:
    //   left:
    //     The left System.Security.Principal.SecurityIdentifier operand to use for
    //     the inequality comparison. This parameter can be null.
    //
    //   right:
    //     The right System.Security.Principal.SecurityIdentifier operand to use for
    //     the inequality comparison. This parameter can be null.
    //
    // Returns:
    //     true if left and right are not equal; otherwise, false.
    extern public static bool operator !=(SecurityIdentifier left, SecurityIdentifier right);
    //
    // Summary:
    //     Compares two System.Security.Principal.SecurityIdentifier objects to determine
    //     whether they are equal. They are considered equal if they have the same canonical
    //     representation as the one returned by the System.Security.Principal.SecurityIdentifier.Value
    //     property or if they are both null.
    //
    // Parameters:
    //   left:
    //     The left System.Security.Principal.SecurityIdentifier operand to use for
    //     the equality comparison. This parameter can be null.
    //
    //   right:
    //     The right System.Security.Principal.SecurityIdentifier operand to use for
    //     the equality comparison. This parameter can be null.
    //
    // Returns:
    //     true if left and right are equal; otherwise, false.
    extern public static bool operator ==(SecurityIdentifier left, SecurityIdentifier right);

    // Summary:
    //     Returns the account domain security identifier (SID) portion from the SID
    //     represented by the System.Security.Principal.SecurityIdentifier object if
    //     the SID represents a Windows account SID. If the SID does not represent a
    //     Windows account SID, this property returns System.ArgumentNullException.
    //
    // Returns:
    //     The account domain SID portion from the SID represented by the System.Security.Principal.SecurityIdentifier
    //     object if the SID represents a Windows account SID; otherwise, it returns
    //     System.ArgumentNullException.
    
    // It can return null
    extern public SecurityIdentifier AccountDomainSid { get; }

    //
    // Summary:
    //     Returns the length, in bytes, of the security identifier (SID) represented
    //     by the System.Security.Principal.SecurityIdentifier object.
    //
    // Returns:
    //     The length, in bytes, of the SID represented by the System.Security.Principal.SecurityIdentifier
    //     object.
    public int BinaryLength
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);
        Contract.Ensures(Contract.Result<int>() >= MinBinaryLength);
        Contract.Ensures(Contract.Result<int>() <= MaxBinaryLength);

        return default(int);
      }
    }


    // Summary:
    //     Compares the current System.Security.Principal.SecurityIdentifier object
    //     with the specified System.Security.Principal.SecurityIdentifier object.
    //
    // Parameters:
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier object with which to compare
    //     the current System.Security.Principal.SecurityIdentifier object.
    //
    // Returns:
    //     A signed number indicating the relative values of this instance and sid.Return
    //     Value Description Less than zero This instance is less than sid. Zero This
    //     instance is equal to sid. Greater than zero This instance is greater than
    //     sid.
    // public int CompareTo(SecurityIdentifier sid);
    //
    // Summary:
    //     Indicates whether the specified System.Security.Principal.SecurityIdentifier
    //     object is equal to the current System.Security.Principal.SecurityIdentifier
    //     object.
    //
    // Parameters:
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier object to compare.
    //
    // Returns:
    //     true if the value of sid is equal to the value of the current System.Security.Principal.SecurityIdentifier
    //     object.
    extern public bool Equals(SecurityIdentifier sid);
    //
    // Summary:
    //     Copies the binary representation of the specified security identifier (SID)
    //     represented by the System.Security.Principal.SecurityIdentifier class to
    //     a byte array.
    //
    // Parameters:
    //   binaryForm:
    //     The byte array to receive the copied SID.
    //
    //   offset:
    //     The byte offset to use as the starting index in binaryForm.
    public void GetBinaryForm(byte[] binaryForm, int offset)
    {
      Contract.Requires(binaryForm != null);
      Contract.Requires(binaryForm.Rank == 1);
      Contract.Requires(offset >= 0);
    }
    //
    // Summary:
    //     Returns a value that indicates whether the security identifier (SID) represented
    //     by this System.Security.Principal.SecurityIdentifier object is a valid Windows
    //     account SID.
    //
    // Returns:
    //     true if the SID represented by this System.Security.Principal.SecurityIdentifier
    //     object is a valid Windows account SID; otherwise, false.
    extern public bool IsAccountSid();
    //
    // Summary:
    //     Returns a value that indicates whether the security identifier (SID) represented
    //     by this System.Security.Principal.SecurityIdentifier object is from the same
    //     domain as the specified SID.
    //
    // Parameters:
    //   sid:
    //     The SID to compare with this System.Security.Principal.SecurityIdentifier
    //     object.
    //
    // Returns:
    //     true if the SID represented by this System.Security.Principal.SecurityIdentifier
    //     object is in the same domain as the sid SID; otherwise, false.
    extern public bool IsEqualDomainSid(SecurityIdentifier sid);
    //
    // Summary:
    //     Returns a value that indicates whether the System.Security.Principal.SecurityIdentifier
    //     object matches the specified well known security identifier (SID) type.
    //
    // Parameters:
    //   type:
    //     A System.Security.Principal.WellKnownSidType value to compare with the System.Security.Principal.SecurityIdentifier
    //     object.
    //
    // Returns:
    //     true if type is the SID type for the System.Security.Principal.SecurityIdentifier
    //     object; otherwise, false.
    extern public bool IsWellKnown(WellKnownSidType type);
  }
}
#endif