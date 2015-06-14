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

// File System.Web.Management.WebEventCodes.cs
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


namespace System.Web.Management
{
  sealed public partial class WebEventCodes
  {
    #region Methods and constructors
    internal WebEventCodes()
    {
    }
    #endregion

    #region Fields
    public static int ApplicationCodeBase;
    public static int ApplicationCompilationEnd;
    public static int ApplicationCompilationStart;
    public static int ApplicationDetailCodeBase;
    public static int ApplicationHeartbeat;
    public static int ApplicationShutdown;
    public static int ApplicationShutdownBinDirChangeOrDirectoryRename;
    public static int ApplicationShutdownBrowsersDirChangeOrDirectoryRename;
    public static int ApplicationShutdownBuildManagerChange;
    public static int ApplicationShutdownChangeInGlobalAsax;
    public static int ApplicationShutdownChangeInSecurityPolicyFile;
    public static int ApplicationShutdownCodeDirChangeOrDirectoryRename;
    public static int ApplicationShutdownConfigurationChange;
    public static int ApplicationShutdownHostingEnvironment;
    public static int ApplicationShutdownHttpRuntimeClose;
    public static int ApplicationShutdownIdleTimeout;
    public static int ApplicationShutdownInitializationError;
    public static int ApplicationShutdownMaxRecompilationsReached;
    public static int ApplicationShutdownPhysicalApplicationPathChanged;
    public static int ApplicationShutdownResourcesDirChangeOrDirectoryRename;
    public static int ApplicationShutdownUnknown;
    public static int ApplicationShutdownUnloadAppDomainCalled;
    public static int ApplicationStart;
    public static int AuditCodeBase;
    public static int AuditDetailCodeBase;
    public static int AuditFileAuthorizationFailure;
    public static int AuditFileAuthorizationSuccess;
    public static int AuditFormsAuthenticationFailure;
    public static int AuditFormsAuthenticationSuccess;
    public static int AuditInvalidViewStateFailure;
    public static int AuditMembershipAuthenticationFailure;
    public static int AuditMembershipAuthenticationSuccess;
    public static int AuditUnhandledAccessException;
    public static int AuditUnhandledSecurityException;
    public static int AuditUrlAuthorizationFailure;
    public static int AuditUrlAuthorizationSuccess;
    public static int ErrorCodeBase;
    public static int ExpiredTicketFailure;
    public static int InvalidEventCode;
    public static int InvalidTicketFailure;
    public static int InvalidViewState;
    public static int InvalidViewStateMac;
    public static int MiscCodeBase;
    public static int RequestCodeBase;
    public static int RequestTransactionAbort;
    public static int RequestTransactionComplete;
    public static int RuntimeErrorPostTooLarge;
    public static int RuntimeErrorRequestAbort;
    public static int RuntimeErrorUnhandledException;
    public static int RuntimeErrorValidationFailure;
    public static int RuntimeErrorViewStateFailure;
    public static int SqlProviderEventsDropped;
    public static int StateServerConnectionError;
    public static int UndefinedEventCode;
    public static int UndefinedEventDetailCode;
    public static int WebErrorCompilationError;
    public static int WebErrorConfigurationError;
    public static int WebErrorObjectStateFormatterDeserializationError;
    public static int WebErrorOtherError;
    public static int WebErrorParserError;
    public static int WebErrorPropertyDeserializationError;
    public static int WebEventDetailCodeBase;
    public static int WebEventProviderInformation;
    public static int WebExtendedBase;
    #endregion
  }
}
