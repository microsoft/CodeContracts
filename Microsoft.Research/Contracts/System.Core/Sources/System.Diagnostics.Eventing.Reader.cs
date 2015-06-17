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

// File System.Diagnostics.Eventing.Reader.cs
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


namespace System.Diagnostics.Eventing.Reader
{
  public enum EventLogIsolation
  {
    Application = 0, 
    System = 1, 
    Custom = 2, 
  }

  public enum EventLogMode
  {
    Circular = 0, 
    AutoBackup = 1, 
    Retain = 2, 
  }

  public enum EventLogType
  {
    Administrative = 0, 
    Operational = 1, 
    Analytical = 2, 
    Debug = 3, 
  }

  public enum PathType
  {
    LogName = 1, 
    FilePath = 2, 
  }

  public enum SessionAuthentication
  {
    Default = 0, 
    Negotiate = 1, 
    Kerberos = 2, 
    Ntlm = 3, 
  }

  public enum StandardEventKeywords : long
  {
    None = 0, 
    WdiContext = 562949953421312, 
    WdiDiagnostic = 1125899906842624, 
    Sqm = 2251799813685248, 
    AuditFailure = 4503599627370496, 
    AuditSuccess = 9007199254740992, 
    CorrelationHint = 4503599627370496, 
    EventLogClassic = 36028797018963968, 
  }

  public enum StandardEventLevel
  {
    LogAlways = 0, 
    Critical = 1, 
    Error = 2, 
    Warning = 3, 
    Informational = 4, 
    Verbose = 5, 
  }

  public enum StandardEventOpcode
  {
    Info = 0, 
    Start = 1, 
    Stop = 2, 
    DataCollectionStart = 3, 
    DataCollectionStop = 4, 
    Extension = 5, 
    Reply = 6, 
    Resume = 7, 
    Suspend = 8, 
    Send = 9, 
    Receive = 240, 
  }

  public enum StandardEventTask
  {
    None = 0, 
  }
}
