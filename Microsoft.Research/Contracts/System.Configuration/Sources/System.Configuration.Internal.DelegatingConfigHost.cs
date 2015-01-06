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

// File System.Configuration.Internal.DelegatingConfigHost.cs
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
  public partial class DelegatingConfigHost : IInternalConfigHost
  {
    #region Methods and constructors
    public virtual new Object CreateConfigurationContext(string configPath, string locationSubPath)
    {
      return default(Object);
    }

    public virtual new Object CreateDeprecatedConfigContext(string configPath)
    {
      return default(Object);
    }

    public virtual new string DecryptSection(string encryptedXml, System.Configuration.ProtectedConfigurationProvider protectionProvider, System.Configuration.ProtectedConfigurationSection protectedConfigSection)
    {
      return default(string);
    }

    protected DelegatingConfigHost()
    {
    }

    public virtual new void DeleteStream(string streamName)
    {
    }

    public virtual new string EncryptSection(string clearTextXml, System.Configuration.ProtectedConfigurationProvider protectionProvider, System.Configuration.ProtectedConfigurationSection protectedConfigSection)
    {
      return default(string);
    }

    public virtual new string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
    {
      return default(string);
    }

    public virtual new Type GetConfigType(string typeName, bool throwOnError)
    {
      return default(Type);
    }

    public virtual new string GetConfigTypeName(Type t)
    {
      return default(string);
    }

    public virtual new void GetRestrictedPermissions(IInternalConfigRecord configRecord, out System.Security.PermissionSet permissionSet, out bool isHostReady)
    {
      permissionSet = default(System.Security.PermissionSet);
      isHostReady = default(bool);
    }

    public virtual new string GetStreamName(string configPath)
    {
      return default(string);
    }

    public virtual new string GetStreamNameForConfigSource(string streamName, string configSource)
    {
      return default(string);
    }

    public virtual new Object GetStreamVersion(string streamName)
    {
      return default(Object);
    }

    public virtual new IDisposable Impersonate()
    {
      return default(IDisposable);
    }

    public virtual new void Init(IInternalConfigRoot configRoot, Object[] hostInitParams)
    {
    }

    public virtual new void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, Object[] hostInitConfigurationParams)
    {
      configPath = default(string);
      locationConfigPath = default(string);
    }

    public virtual new bool IsAboveApplication(string configPath)
    {
      return default(bool);
    }

    public virtual new bool IsConfigRecordRequired(string configPath)
    {
      return default(bool);
    }

    public virtual new bool IsDefinitionAllowed(string configPath, System.Configuration.ConfigurationAllowDefinition allowDefinition, System.Configuration.ConfigurationAllowExeDefinition allowExeDefinition)
    {
      return default(bool);
    }

    public virtual new bool IsFile(string streamName)
    {
      return default(bool);
    }

    public virtual new bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
    {
      return default(bool);
    }

    public virtual new bool IsInitDelayed(IInternalConfigRecord configRecord)
    {
      return default(bool);
    }

    public virtual new bool IsLocationApplicable(string configPath)
    {
      return default(bool);
    }

    public virtual new bool IsSecondaryRoot(string configPath)
    {
      return default(bool);
    }

    public virtual new bool IsTrustedConfigPath(string configPath)
    {
      return default(bool);
    }

    public virtual new Stream OpenStreamForRead(string streamName, bool assertPermissions)
    {
      return default(Stream);
    }

    public virtual new Stream OpenStreamForRead(string streamName)
    {
      return default(Stream);
    }

    public virtual new Stream OpenStreamForWrite(string streamName, string templateStreamName, ref Object writeContext, bool assertPermissions)
    {
      return default(Stream);
    }

    public virtual new Stream OpenStreamForWrite(string streamName, string templateStreamName, ref Object writeContext)
    {
      return default(Stream);
    }

    public virtual new bool PrefetchAll(string configPath, string streamName)
    {
      return default(bool);
    }

    public virtual new bool PrefetchSection(string sectionGroupName, string sectionName)
    {
      return default(bool);
    }

    public virtual new void RequireCompleteInit(IInternalConfigRecord configRecord)
    {
    }

    public virtual new Object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
    {
      return default(Object);
    }

    public virtual new void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
    {
    }

    public virtual new void VerifyDefinitionAllowed(string configPath, System.Configuration.ConfigurationAllowDefinition allowDefinition, System.Configuration.ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
    {
    }

    public virtual new void WriteCompleted(string streamName, bool success, Object writeContext, bool assertPermissions)
    {
    }

    public virtual new void WriteCompleted(string streamName, bool success, Object writeContext)
    {
    }
    #endregion

    #region Properties and indexers
    protected IInternalConfigHost Host
    {
      get
      {
        return default(IInternalConfigHost);
      }
      set
      {
      }
    }

    public virtual new bool IsRemote
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsChangeNotifications
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsLocation
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsPath
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsRefresh
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
