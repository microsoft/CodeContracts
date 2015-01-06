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

// File System.Runtime.Remoting.RemotingConfiguration.cs
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


namespace System.Runtime.Remoting
{
  static public partial class RemotingConfiguration
  {
    #region Methods and constructors
    public static void Configure(string filename)
    {
    }

    public static void Configure(string filename, bool ensureSecurity)
    {
    }

    public static bool CustomErrorsEnabled(bool isLocalRequest)
    {
      return default(bool);
    }

    public static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
    {
      Contract.Ensures(Contract.Result<System.Runtime.Remoting.ActivatedClientTypeEntry[]>() != null);

      return default(ActivatedClientTypeEntry[]);
    }

    public static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
    {
      Contract.Ensures(Contract.Result<System.Runtime.Remoting.ActivatedServiceTypeEntry[]>() != null);

      return default(ActivatedServiceTypeEntry[]);
    }

    public static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
    {
      Contract.Ensures(Contract.Result<System.Runtime.Remoting.WellKnownClientTypeEntry[]>() != null);

      return default(WellKnownClientTypeEntry[]);
    }

    public static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
    {
      Contract.Ensures(Contract.Result<System.Runtime.Remoting.WellKnownServiceTypeEntry[]>() != null);

      return default(WellKnownServiceTypeEntry[]);
    }

    public static bool IsActivationAllowed(Type svrType)
    {
      return default(bool);
    }

    public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
    {
      return default(ActivatedClientTypeEntry);
    }

    public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
    {
      Contract.Requires(assemblyName != null);

      return default(ActivatedClientTypeEntry);
    }

    public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
    {
      return default(WellKnownClientTypeEntry);
    }

    public static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
    {
      Contract.Requires(assemblyName != null);

      return default(WellKnownClientTypeEntry);
    }

    public static void RegisterActivatedClientType(Type type, string appUrl)
    {
    }

    public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
    {
      Contract.Requires(entry != null);
      Contract.Requires(entry.AssemblyName != null);
    }

    public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
    {
      Contract.Requires(entry != null);
    }

    public static void RegisterActivatedServiceType(Type type)
    {
    }

    public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
    {
      Contract.Requires(entry != null);
      Contract.Requires(entry.AssemblyName != null);
    }

    public static void RegisterWellKnownClientType(Type type, string objectUrl)
    {
    }

    public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
    {
      Contract.Requires(entry != null);
      Contract.Requires(entry.AssemblyName != null);
      Contract.Requires(entry.ObjectUri != null);
    }

    public static void RegisterWellKnownServiceType(Type type, string objectUri, WellKnownObjectMode mode)
    {
      Contract.Requires(type.Module != null);
      Contract.Requires(type.Module.Assembly != null);
      Contract.Ensures(type.Module.Assembly != null);
    }
    #endregion

    #region Properties and indexers
    public static string ApplicationId
    {
      get
      {
        return default(string);
      }
    }

    public static string ApplicationName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public static CustomErrorsModes CustomErrorsMode
    {
      get
      {
        return default(CustomErrorsModes);
      }
      set
      {
      }
    }

    public static string ProcessId
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
