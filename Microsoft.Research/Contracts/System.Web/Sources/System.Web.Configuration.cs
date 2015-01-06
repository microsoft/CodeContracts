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

// File System.Web.Configuration.cs
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


namespace System.Web.Configuration
{
  public enum AuthenticationMode
  {
    None = 0, 
    Windows = 1, 
    Passport = 2, 
    Forms = 3, 
  }

  public enum AuthorizationRuleAction
  {
    Deny = 0, 
    Allow = 1, 
  }

  public enum CustomErrorsMode
  {
    RemoteOnly = 0, 
    On = 1, 
    Off = 2, 
  }

  public enum CustomErrorsRedirectMode
  {
    ResponseRedirect = 0, 
    ResponseRewrite = 1, 
  }

  public enum FormsAuthPasswordFormat
  {
    Clear = 0, 
    SHA1 = 1, 
    MD5 = 2, 
  }

  public enum FormsProtectionEnum
  {
    All = 0, 
    None = 1, 
    Encryption = 2, 
    Validation = 3, 
  }

  public enum MachineKeyCompatibilityMode
  {
    Framework20SP1 = 0, 
    Framework20SP2 = 1, 
  }

  public enum MachineKeyValidation
  {
    MD5 = 0, 
    SHA1 = 1, 
    TripleDES = 2, 
    AES = 3, 
    HMACSHA256 = 4, 
    HMACSHA384 = 5, 
    HMACSHA512 = 6, 
    Custom = 7, 
  }

  public enum PagesEnableSessionState
  {
    False = 0, 
    ReadOnly = 1, 
    True = 2, 
  }

  public enum ProcessModelComAuthenticationLevel
  {
    None = 0, 
    Call = 1, 
    Connect = 2, 
    Default = 3, 
    Pkt = 4, 
    PktIntegrity = 5, 
    PktPrivacy = 6, 
  }

  public enum ProcessModelComImpersonationLevel
  {
    Default = 0, 
    Anonymous = 1, 
    Delegate = 2, 
    Identify = 3, 
    Impersonate = 4, 
  }

  public enum ProcessModelLogLevel
  {
    None = 0, 
    All = 1, 
    Errors = 2, 
  }

  public enum SerializationMode
  {
    String = 0, 
    Xml = 1, 
    Binary = 2, 
    ProviderSpecific = 3, 
  }

  public enum TicketCompatibilityMode
  {
    Framework20 = 0, 
    Framework40 = 1, 
  }

  public enum TraceDisplayMode
  {
    SortByTime = 1, 
    SortByCategory = 2, 
  }

  public enum WebApplicationLevel
  {
    AboveApplication = 10, 
    AtApplication = 20, 
    BelowApplication = 30, 
  }

  public enum XhtmlConformanceMode
  {
    Transitional = 0, 
    Legacy = 1, 
    Strict = 2, 
  }
}
