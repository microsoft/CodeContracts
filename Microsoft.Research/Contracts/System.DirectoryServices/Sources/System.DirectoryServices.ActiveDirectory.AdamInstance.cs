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

// File System.DirectoryServices.ActiveDirectory.AdamInstance.cs
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


namespace System.DirectoryServices.ActiveDirectory
{
  public partial class AdamInstance : DirectoryServer
  {
    #region Methods and constructors
    private AdamInstance()
    {
    }

    public override void CheckReplicationConsistency()
    {
    }

    protected override void Dispose(bool disposing)
    {
    }

    public static AdamInstanceCollection FindAll(DirectoryContext context, string partitionName)
    {
      return default(AdamInstanceCollection);
    }

    public static AdamInstance FindOne(DirectoryContext context, string partitionName)
    {
      return default(AdamInstance);
    }

    public static AdamInstance GetAdamInstance(DirectoryContext context)
    {
      return default(AdamInstance);
    }

    public override ReplicationNeighborCollection GetAllReplicationNeighbors()
    {
      return default(ReplicationNeighborCollection);
    }

    public override ReplicationFailureCollection GetReplicationConnectionFailures()
    {
      return default(ReplicationFailureCollection);
    }

    public override ReplicationCursorCollection GetReplicationCursors(string partition)
    {
      return default(ReplicationCursorCollection);
    }

    public override ActiveDirectoryReplicationMetadata GetReplicationMetadata(string objectPath)
    {
      return default(ActiveDirectoryReplicationMetadata);
    }

    public override ReplicationNeighborCollection GetReplicationNeighbors(string partition)
    {
      return default(ReplicationNeighborCollection);
    }

    public override ReplicationOperationInformation GetReplicationOperationInformation()
    {
      return default(ReplicationOperationInformation);
    }

    public void Save()
    {
    }

    public void SeizeRoleOwnership(AdamRole role)
    {
    }

    public override void SyncReplicaFromAllServers(string partition, SyncFromAllServersOptions options)
    {
    }

    public override void SyncReplicaFromServer(string partition, string sourceServer)
    {
    }

    public void TransferRoleOwnership(AdamRole role)
    {
    }

    public override void TriggerSyncReplicaFromNeighbors(string partition)
    {
    }
    #endregion

    #region Properties and indexers
    public ConfigurationSet ConfigurationSet
    {
      get
      {
        return default(ConfigurationSet);
      }
    }

    public string DefaultPartition
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string HostName
    {
      get
      {
        return default(string);
      }
    }

    public override ReplicationConnectionCollection InboundConnections
    {
      get
      {
        return default(ReplicationConnectionCollection);
      }
    }

    public override string IPAddress
    {
      get
      {
        return default(string);
      }
    }

    public int LdapPort
    {
      get
      {
        return default(int);
      }
    }

    public override ReplicationConnectionCollection OutboundConnections
    {
      get
      {
        return default(ReplicationConnectionCollection);
      }
    }

    public AdamRoleCollection Roles
    {
      get
      {
        return default(AdamRoleCollection);
      }
    }

    public override string SiteName
    {
      get
      {
        return default(string);
      }
    }

    public int SslPort
    {
      get
      {
        return default(int);
      }
    }

    public override SyncUpdateCallback SyncFromAllServersCallback
    {
      get
      {
        return default(SyncUpdateCallback);
      }
      set
      {
      }
    }
    #endregion
  }
}
