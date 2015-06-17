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

// File System.Security.Permissions.cs
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


namespace System.Security.Permissions
{
  public enum EnvironmentPermissionAccess
  {
    NoAccess = 0, 
    Read = 1, 
    Write = 2, 
    AllAccess = 3, 
  }

  public enum FileDialogPermissionAccess
  {
    None = 0, 
    Open = 1, 
    Save = 2, 
    OpenSave = 3, 
  }

  public enum FileIOPermissionAccess
  {
    NoAccess = 0, 
    Read = 1, 
    Write = 2, 
    Append = 4, 
    PathDiscovery = 8, 
    AllAccess = 15, 
  }

  public enum HostProtectionResource
  {
    None = 0, 
    Synchronization = 1, 
    SharedState = 2, 
    ExternalProcessMgmt = 4, 
    SelfAffectingProcessMgmt = 8, 
    ExternalThreading = 16, 
    SelfAffectingThreading = 32, 
    SecurityInfrastructure = 64, 
    UI = 128, 
    MayLeakOnAbort = 256, 
    All = 511, 
  }

  public enum IsolatedStorageContainment
  {
    None = 0, 
    DomainIsolationByUser = 16, 
    ApplicationIsolationByUser = 21, 
    AssemblyIsolationByUser = 32, 
    DomainIsolationByMachine = 48, 
    AssemblyIsolationByMachine = 64, 
    ApplicationIsolationByMachine = 69, 
    DomainIsolationByRoamingUser = 80, 
    AssemblyIsolationByRoamingUser = 96, 
    ApplicationIsolationByRoamingUser = 101, 
    AdministerIsolatedStorageByUser = 112, 
    UnrestrictedIsolatedStorage = 240, 
  }

  public enum KeyContainerPermissionFlags
  {
    NoFlags = 0, 
    Create = 1, 
    Open = 2, 
    Delete = 4, 
    Import = 16, 
    Export = 32, 
    Sign = 256, 
    Decrypt = 512, 
    ViewAcl = 4096, 
    ChangeAcl = 8192, 
    AllFlags = 13111, 
  }

  public enum PermissionState
  {
    Unrestricted = 1, 
    None = 0, 
  }

  public enum ReflectionPermissionFlag
  {
    NoFlags = 0, 
    TypeInformation = 1, 
    MemberAccess = 2, 
    ReflectionEmit = 4, 
    RestrictedMemberAccess = 8, 
    AllFlags = 7, 
  }

  public enum RegistryPermissionAccess
  {
    NoAccess = 0, 
    Read = 1, 
    Write = 2, 
    Create = 4, 
    AllAccess = 7, 
  }

  public enum SecurityAction
  {
    Demand = 2, 
    Assert = 3, 
    Deny = 4, 
    PermitOnly = 5, 
    LinkDemand = 6, 
    InheritanceDemand = 7, 
    RequestMinimum = 8, 
    RequestOptional = 9, 
    RequestRefuse = 10, 
  }

  public enum SecurityPermissionFlag
  {
    NoFlags = 0, 
    Assertion = 1, 
    UnmanagedCode = 2, 
    SkipVerification = 4, 
    Execution = 8, 
    ControlThread = 16, 
    ControlEvidence = 32, 
    ControlPolicy = 64, 
    SerializationFormatter = 128, 
    ControlDomainPolicy = 256, 
    ControlPrincipal = 512, 
    ControlAppDomain = 1024, 
    RemotingConfiguration = 2048, 
    Infrastructure = 4096, 
    BindingRedirects = 8192, 
    AllFlags = 16383, 
  }

  public enum UIPermissionClipboard
  {
    NoClipboard = 0, 
    OwnClipboard = 1, 
    AllClipboard = 2, 
  }

  public enum UIPermissionWindow
  {
    NoWindows = 0, 
    SafeSubWindows = 1, 
    SafeTopLevelWindows = 2, 
    AllWindows = 3, 
  }
}
