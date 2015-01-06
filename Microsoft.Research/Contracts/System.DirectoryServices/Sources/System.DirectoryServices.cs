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

// File System.DirectoryServices.cs
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


namespace System.DirectoryServices
{
  public enum ActiveDirectoryRights
  {
    Delete = 65536, 
    ReadControl = 131072, 
    WriteDacl = 262144, 
    WriteOwner = 524288, 
    Synchronize = 1048576, 
    AccessSystemSecurity = 16777216, 
    GenericRead = 131220, 
    GenericWrite = 131112, 
    GenericExecute = 131076, 
    GenericAll = 983551, 
    CreateChild = 1, 
    DeleteChild = 2, 
    ListChildren = 4, 
    Self = 8, 
    ReadProperty = 16, 
    WriteProperty = 32, 
    DeleteTree = 64, 
    ListObject = 128, 
    ExtendedRight = 256, 
  }

  public enum ActiveDirectorySecurityInheritance
  {
    None = 0, 
    All = 1, 
    Descendents = 2, 
    SelfAndChildren = 3, 
    Children = 4, 
  }

  public enum AuthenticationTypes
  {
    None = 0, 
    Secure = 1, 
    Encryption = 2, 
    SecureSocketsLayer = 2, 
    ReadonlyServer = 4, 
    Anonymous = 16, 
    FastBind = 32, 
    Signing = 64, 
    Sealing = 128, 
    Delegation = 256, 
    ServerBind = 512, 
  }

  public enum DereferenceAlias
  {
    Never = 0, 
    InSearching = 1, 
    FindingBaseObject = 2, 
    Always = 3, 
  }

  public enum DirectoryServicesPermissionAccess
  {
    None = 0, 
    Browse = 2, 
    Write = 6, 
  }

  public enum DirectorySynchronizationOptions : long
  {
    None = 0, 
    ObjectSecurity = 1, 
    ParentsFirst = 2048, 
    PublicDataOnly = 8192, 
    IncrementalValues = 2147483648, 
  }

  public enum ExtendedDN
  {
    None = -1, 
    HexString = 0, 
    Standard = 1, 
  }

  public enum PasswordEncodingMethod
  {
    PasswordEncodingSsl = 0, 
    PasswordEncodingClear = 1, 
  }

  public enum PropertyAccess
  {
    Read = 0, 
    Write = 1, 
  }

  public enum ReferralChasingOption
  {
    None = 0, 
    Subordinate = 32, 
    External = 64, 
    All = 96, 
  }

  public enum SearchScope
  {
    Base = 0, 
    OneLevel = 1, 
    Subtree = 2, 
  }

  public enum SecurityMasks
  {
    None = 0, 
    Owner = 1, 
    Group = 2, 
    Dacl = 4, 
    Sacl = 8, 
  }

  public enum SortDirection
  {
    Ascending = 0, 
    Descending = 1, 
  }
}
