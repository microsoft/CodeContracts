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

// File System.Security.Principal.SecurityIdentifier.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Security.Principal
{
  sealed public partial class SecurityIdentifier : IdentityReference, IComparable<SecurityIdentifier>
  {
    #region Methods and constructors
    public static bool operator != (System.Security.Principal.SecurityIdentifier left, System.Security.Principal.SecurityIdentifier right)
    {
      Contract.Ensures(Contract.Result<bool>() == ((left.Equals(right)) == false));

      return default(bool);
    }

    public static bool operator == (System.Security.Principal.SecurityIdentifier left, System.Security.Principal.SecurityIdentifier right)
    {
      return default(bool);
    }

    public int CompareTo(System.Security.Principal.SecurityIdentifier sid)
    {
      return default(int);
    }

    public bool Equals(System.Security.Principal.SecurityIdentifier sid)
    {
      return default(bool);
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public void GetBinaryForm(byte[] binaryForm, int offset)
    {
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public bool IsAccountSid()
    {
      return default(bool);
    }

    public bool IsEqualDomainSid(System.Security.Principal.SecurityIdentifier sid)
    {
      return default(bool);
    }

    public override bool IsValidTargetType(Type targetType)
    {
      return default(bool);
    }

    public bool IsWellKnown(WellKnownSidType type)
    {
      return default(bool);
    }

    public SecurityIdentifier(IntPtr binaryForm)
    {
      Contract.Ensures(false);
    }

    public SecurityIdentifier(byte[] binaryForm, int offset)
    {
      Contract.Requires((offset + 7) < binaryForm.Length);
      Contract.Ensures((System.Security.Principal.SecurityIdentifier.MinBinaryLength - binaryForm.Length) <= 0);
    }

    public SecurityIdentifier(WellKnownSidType sidType, System.Security.Principal.SecurityIdentifier domainSid)
    {
    }

    public SecurityIdentifier(string sddlForm)
    {
    }

    public override string ToString()
    {
      return default(string);
    }

    public override IdentityReference Translate(Type targetType)
    {
      return default(IdentityReference);
    }
    #endregion

    #region Properties and indexers
    public System.Security.Principal.SecurityIdentifier AccountDomainSid
    {
      get
      {
        return default(System.Security.Principal.SecurityIdentifier);
      }
    }

    public int BinaryLength
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 2147483647);

        return default(int);
      }
    }

    public override string Value
    {
      get
      {
        return default(string);
      }
    }
    #endregion

    #region Fields
    public readonly static int MaxBinaryLength;
    public readonly static int MinBinaryLength;
    #endregion
  }
}
