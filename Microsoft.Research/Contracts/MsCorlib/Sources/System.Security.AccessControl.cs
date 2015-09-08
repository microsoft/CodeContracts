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

// File System.Security.AccessControl.cs
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


namespace System.Security.AccessControl
{
  public enum AccessControlActions
  {
    None = 0, 
    View = 1, 
    Change = 2, 
  }

  public enum AccessControlModification
  {
    Add = 0, 
    Set = 1, 
    Reset = 2, 
    Remove = 3, 
    RemoveAll = 4, 
    RemoveSpecific = 5, 
  }

  public enum AccessControlSections
  {
    None = 0, 
    Audit = 1, 
    Access = 2, 
    Owner = 4, 
    Group = 8, 
    All = 15, 
  }

  public enum AccessControlType
  {
    Allow = 0, 
    Deny = 1, 
  }

  public enum AceFlags : byte
  {
    None = 0, 
    ObjectInherit = 1, 
    ContainerInherit = 2, 
    NoPropagateInherit = 4, 
    InheritOnly = 8, 
    Inherited = 16, 
    SuccessfulAccess = 64, 
    FailedAccess = 128, 
    InheritanceFlags = 15, 
    AuditFlags = 192, 
  }

  public enum AceQualifier
  {
    AccessAllowed = 0, 
    AccessDenied = 1, 
    SystemAudit = 2, 
    SystemAlarm = 3, 
  }

  public enum AceType : byte
  {
    AccessAllowed = 0, 
    AccessDenied = 1, 
    SystemAudit = 2, 
    SystemAlarm = 3, 
    AccessAllowedCompound = 4, 
    AccessAllowedObject = 5, 
    AccessDeniedObject = 6, 
    SystemAuditObject = 7, 
    SystemAlarmObject = 8, 
    AccessAllowedCallback = 9, 
    AccessDeniedCallback = 10, 
    AccessAllowedCallbackObject = 11, 
    AccessDeniedCallbackObject = 12, 
    SystemAuditCallback = 13, 
    SystemAlarmCallback = 14, 
    SystemAuditCallbackObject = 15, 
    SystemAlarmCallbackObject = 16, 
    MaxDefinedAceType = 16, 
  }

  public enum AuditFlags
  {
    None = 0, 
    Success = 1, 
    Failure = 2, 
  }

  public enum CompoundAceType
  {
    Impersonation = 1, 
  }

  public enum ControlFlags
  {
    None = 0, 
    OwnerDefaulted = 1, 
    GroupDefaulted = 2, 
    DiscretionaryAclPresent = 4, 
    DiscretionaryAclDefaulted = 8, 
    SystemAclPresent = 16, 
    SystemAclDefaulted = 32, 
    DiscretionaryAclUntrusted = 64, 
    ServerSecurity = 128, 
    DiscretionaryAclAutoInheritRequired = 256, 
    SystemAclAutoInheritRequired = 512, 
    DiscretionaryAclAutoInherited = 1024, 
    SystemAclAutoInherited = 2048, 
    DiscretionaryAclProtected = 4096, 
    SystemAclProtected = 8192, 
    RMControlValid = 16384, 
    SelfRelative = 32768, 
  }

  public enum CryptoKeyRights
  {
    ReadData = 1, 
    WriteData = 2, 
    ReadExtendedAttributes = 8, 
    WriteExtendedAttributes = 16, 
    ReadAttributes = 128, 
    WriteAttributes = 256, 
    Delete = 65536, 
    ReadPermissions = 131072, 
    ChangePermissions = 262144, 
    TakeOwnership = 524288, 
    Synchronize = 1048576, 
    FullControl = 2032027, 
    GenericAll = 268435456, 
    GenericExecute = 536870912, 
    GenericWrite = 1073741824, 
    GenericRead = -2147483648, 
  }

  public enum EventWaitHandleRights
  {
    Modify = 2, 
    Delete = 65536, 
    ReadPermissions = 131072, 
    ChangePermissions = 262144, 
    TakeOwnership = 524288, 
    Synchronize = 1048576, 
    FullControl = 2031619, 
  }

  public enum FileSystemRights
  {
    ReadData = 1, 
    ListDirectory = 1, 
    WriteData = 2, 
    CreateFiles = 2, 
    AppendData = 4, 
    CreateDirectories = 4, 
    ReadExtendedAttributes = 8, 
    WriteExtendedAttributes = 16, 
    ExecuteFile = 32, 
    Traverse = 32, 
    DeleteSubdirectoriesAndFiles = 64, 
    ReadAttributes = 128, 
    WriteAttributes = 256, 
    Delete = 65536, 
    ReadPermissions = 131072, 
    ChangePermissions = 262144, 
    TakeOwnership = 524288, 
    Synchronize = 1048576, 
    FullControl = 2032127, 
    Read = 131209, 
    ReadAndExecute = 131241, 
    Write = 278, 
    Modify = 197055, 
  }

  public enum InheritanceFlags
  {
    None = 0, 
    ContainerInherit = 1, 
    ObjectInherit = 2, 
  }

  public enum MutexRights
  {
    Modify = 1, 
    Delete = 65536, 
    ReadPermissions = 131072, 
    ChangePermissions = 262144, 
    TakeOwnership = 524288, 
    Synchronize = 1048576, 
    FullControl = 2031617, 
  }

  public enum ObjectAceFlags
  {
    None = 0, 
    ObjectAceTypePresent = 1, 
    InheritedObjectAceTypePresent = 2, 
  }

  public enum PropagationFlags
  {
    None = 0, 
    NoPropagateInherit = 1, 
    InheritOnly = 2, 
  }

  public enum RegistryRights
  {
    QueryValues = 1, 
    SetValue = 2, 
    CreateSubKey = 4, 
    EnumerateSubKeys = 8, 
    Notify = 16, 
    CreateLink = 32, 
    ExecuteKey = 131097, 
    ReadKey = 131097, 
    WriteKey = 131078, 
    Delete = 65536, 
    ReadPermissions = 131072, 
    ChangePermissions = 262144, 
    TakeOwnership = 524288, 
    FullControl = 983103, 
  }

  public enum ResourceType
  {
    Unknown = 0, 
    FileObject = 1, 
    Service = 2, 
    Printer = 3, 
    RegistryKey = 4, 
    LMShare = 5, 
    KernelObject = 6, 
    WindowObject = 7, 
    DSObject = 8, 
    DSObjectAll = 9, 
    ProviderDefined = 10, 
    WmiGuidObject = 11, 
    RegistryWow6432Key = 12, 
  }

  public enum SecurityInfos
  {
    Owner = 1, 
    Group = 2, 
    DiscretionaryAcl = 4, 
    SystemAcl = 8, 
  }
}
