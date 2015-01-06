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

// File System.Configuration.Internal.IInternalConfigHost.cs
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


namespace System.Configuration.Internal
{
  public partial interface IInternalConfigHost
  {
    #region Methods and constructors
    Object CreateConfigurationContext(string configPath, string locationSubPath);

    Object CreateDeprecatedConfigContext(string configPath);

    string DecryptSection(string encryptedXml, System.Configuration.ProtectedConfigurationProvider protectionProvider, System.Configuration.ProtectedConfigurationSection protectedConfigSection);

    void DeleteStream(string streamName);

    string EncryptSection(string clearTextXml, System.Configuration.ProtectedConfigurationProvider protectionProvider, System.Configuration.ProtectedConfigurationSection protectedConfigSection);

    string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath);

    Type GetConfigType(string typeName, bool throwOnError);

    string GetConfigTypeName(Type t);

    void GetRestrictedPermissions(IInternalConfigRecord configRecord, out System.Security.PermissionSet permissionSet, out bool isHostReady);

    string GetStreamName(string configPath);

    string GetStreamNameForConfigSource(string streamName, string configSource);

    Object GetStreamVersion(string streamName);

    IDisposable Impersonate();

    void Init(IInternalConfigRoot configRoot, Object[] hostInitParams);

    void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, Object[] hostInitConfigurationParams);

    bool IsAboveApplication(string configPath);

    bool IsConfigRecordRequired(string configPath);

    bool IsDefinitionAllowed(string configPath, System.Configuration.ConfigurationAllowDefinition allowDefinition, System.Configuration.ConfigurationAllowExeDefinition allowExeDefinition);

    bool IsFile(string streamName);

    bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord);

    bool IsInitDelayed(IInternalConfigRecord configRecord);

    bool IsLocationApplicable(string configPath);

    bool IsSecondaryRoot(string configPath);

    bool IsTrustedConfigPath(string configPath);

    Stream OpenStreamForRead(string streamName, bool assertPermissions);

    Stream OpenStreamForRead(string streamName);

    Stream OpenStreamForWrite(string streamName, string templateStreamName, ref Object writeContext, bool assertPermissions);

    Stream OpenStreamForWrite(string streamName, string templateStreamName, ref Object writeContext);

    bool PrefetchAll(string configPath, string streamName);

    bool PrefetchSection(string sectionGroupName, string sectionName);

    void RequireCompleteInit(IInternalConfigRecord configRecord);

    Object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback);

    void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback);

    void VerifyDefinitionAllowed(string configPath, System.Configuration.ConfigurationAllowDefinition allowDefinition, System.Configuration.ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo);

    void WriteCompleted(string streamName, bool success, Object writeContext, bool assertPermissions);

    void WriteCompleted(string streamName, bool success, Object writeContext);
    #endregion

    #region Properties and indexers
    bool IsRemote
    {
      get;
    }

    bool SupportsChangeNotifications
    {
      get;
    }

    bool SupportsLocation
    {
      get;
    }

    bool SupportsPath
    {
      get;
    }

    bool SupportsRefresh
    {
      get;
    }
    #endregion
  }
}
