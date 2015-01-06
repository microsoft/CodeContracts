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

// File System.DirectoryServices.ActiveDirectory.ReplicationNeighbor.cs
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
  public partial class ReplicationNeighbor
  {
    #region Enums
    public enum ReplicationNeighborOptions : long
    {
      Writeable = 16, 
      SyncOnStartup = 32, 
      ScheduledSync = 64, 
      UseInterSiteTransport = 128, 
      TwoWaySync = 512, 
      ReturnObjectParent = 2048, 
      FullSyncInProgress = 65536, 
      FullSyncNextPacket = 131072, 
      NeverSynced = 2097152, 
      Preempted = 16777216, 
      IgnoreChangeNotifications = 67108864, 
      DisableScheduledSync = 134217728, 
      CompressChanges = 268435456, 
      NoChangeNotifications = 536870912, 
      PartialAttributeSet = 1073741824, 
    }
    #endregion

    #region Methods and constructors
    private ReplicationNeighbor()
    {
    }
    #endregion

    #region Properties and indexers
    public int ConsecutiveFailureCount
    {
      get
      {
        return default(int);
      }
    }

    public DateTime LastAttemptedSync
    {
      get
      {
        return default(DateTime);
      }
    }

    public DateTime LastSuccessfulSync
    {
      get
      {
        return default(DateTime);
      }
    }

    public string LastSyncMessage
    {
      get
      {
        return default(string);
      }
    }

    public int LastSyncResult
    {
      get
      {
        return default(int);
      }
    }

    public string PartitionName
    {
      get
      {
        return default(string);
      }
    }

    public System.DirectoryServices.ActiveDirectory.ReplicationNeighbor.ReplicationNeighborOptions ReplicationNeighborOption
    {
      get
      {
        return default(System.DirectoryServices.ActiveDirectory.ReplicationNeighbor.ReplicationNeighborOptions);
      }
    }

    public Guid SourceInvocationId
    {
      get
      {
        return default(Guid);
      }
    }

    public string SourceServer
    {
      get
      {
        return default(string);
      }
    }

    public ActiveDirectoryTransportType TransportType
    {
      get
      {
        return default(ActiveDirectoryTransportType);
      }
    }

    public long UsnAttributeFilter
    {
      get
      {
        return default(long);
      }
    }

    public long UsnLastObjectChangeSynced
    {
      get
      {
        return default(long);
      }
    }
    #endregion
  }
}
