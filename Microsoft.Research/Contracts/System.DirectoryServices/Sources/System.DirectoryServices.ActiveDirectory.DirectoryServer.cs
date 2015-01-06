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

// File System.DirectoryServices.ActiveDirectory.DirectoryServer.cs
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

  [ContractClass(typeof(DirectoryServerContracts))]
  abstract public partial class DirectoryServer : IDisposable
  {
    #region Methods and constructors
    public abstract void CheckReplicationConsistency();

    protected DirectoryServer()
    {
    }

    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public abstract ReplicationNeighborCollection GetAllReplicationNeighbors();

    public System.DirectoryServices.DirectoryEntry GetDirectoryEntry()
    {
      Contract.Ensures(Contract.Result<DirectoryEntry>() != null);

      return default(System.DirectoryServices.DirectoryEntry);
    }

    public abstract ReplicationFailureCollection GetReplicationConnectionFailures();

    public abstract ReplicationCursorCollection GetReplicationCursors(string partition);

    public abstract ActiveDirectoryReplicationMetadata GetReplicationMetadata(string objectPath);

    public abstract ReplicationNeighborCollection GetReplicationNeighbors(string partition);

    public abstract ReplicationOperationInformation GetReplicationOperationInformation();

    public void MoveToAnotherSite(string siteName)
    {
    }

    public abstract void SyncReplicaFromAllServers(string partition, SyncFromAllServersOptions options);

    public abstract void SyncReplicaFromServer(string partition, string sourceServer);

    public abstract void TriggerSyncReplicaFromNeighbors(string partition);
    #endregion

    #region Properties and indexers
    public abstract ReplicationConnectionCollection InboundConnections
    {
      get;
    }

    public abstract string IPAddress
    {
      get;
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public abstract ReplicationConnectionCollection OutboundConnections
    {
      get;
    }

    public ReadOnlyStringCollection Partitions
    {
      get
      {
        return default(ReadOnlyStringCollection);
      }
    }

    public abstract string SiteName
    {
      get;
    }

    public abstract SyncUpdateCallback SyncFromAllServersCallback
    {
      get;
      set;
    }
    #endregion
  }

  [ContractClassFor(typeof(DirectoryServer))]
  abstract class DirectoryServerContracts : DirectoryServer
  {
    public override void CheckReplicationConsistency()
    {
      throw new NotImplementedException();
    }

    public override ReplicationNeighborCollection GetAllReplicationNeighbors()
    {
      Contract.Ensures(Contract.Result<ReplicationNeighborCollection>() != null);

      throw new NotImplementedException();
    }

    public override ReplicationFailureCollection GetReplicationConnectionFailures()
    {
      Contract.Ensures(Contract.Result<ReplicationFailureCollection>() != null);

      throw new NotImplementedException();
    }

    public override ReplicationCursorCollection GetReplicationCursors(string partition)
    {
      Contract.Ensures(Contract.Result<ReplicationCursorCollection>() != null);

      throw new NotImplementedException();
    }

    public override ActiveDirectoryReplicationMetadata GetReplicationMetadata(string objectPath)
    {
      Contract.Ensures(Contract.Result<ActiveDirectoryReplicationMetadata>() != null);

      throw new NotImplementedException();
    }

    public override ReplicationNeighborCollection GetReplicationNeighbors(string partition)
    {
      Contract.Ensures(Contract.Result<ReplicationNeighborCollection>() != null);

      throw new NotImplementedException();
    }

    public override ReplicationOperationInformation GetReplicationOperationInformation()
    {
      Contract.Ensures(Contract.Result<ReplicationOperationInformation>() != null);

      throw new NotImplementedException();
    }

    public override void SyncReplicaFromAllServers(string partition, SyncFromAllServersOptions options)
    {
      throw new NotImplementedException();
    }

    public override void SyncReplicaFromServer(string partition, string sourceServer)
    {
      throw new NotImplementedException();
    }

    public override void TriggerSyncReplicaFromNeighbors(string partition)
    {
      throw new NotImplementedException();
    }

    public override ReplicationConnectionCollection InboundConnections
    {
      get {
        Contract.Ensures(Contract.Result<ReplicationConnectionCollection>() != null);
        throw new NotImplementedException(); }
    }

    public override string IPAddress
    {
      get { throw new NotImplementedException(); }
    }

    public override ReplicationConnectionCollection OutboundConnections
    {
      get {
        Contract.Ensures(Contract.Result<ReplicationConnectionCollection>() != null);

        throw new NotImplementedException(); }
    }

    public override string SiteName
    {
      get { throw new NotImplementedException(); }
    }

    public override SyncUpdateCallback SyncFromAllServersCallback
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }
  }
}
