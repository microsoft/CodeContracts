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

// File System.DirectoryServices.ActiveDirectory.ReplicationConnection.cs
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
  public partial class ReplicationConnection : IDisposable
  {
    #region Methods and constructors
    public void Delete()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    // it seems it may return null?
    public static ReplicationConnection FindByName(DirectoryContext context, string name)
    {
      Contract.Requires(context != null);
      Contract.Requires(!string.IsNullOrEmpty(name));

      return default(ReplicationConnection);
    }

    public System.DirectoryServices.DirectoryEntry GetDirectoryEntry()
    {
      Contract.Ensures(Contract.Result<DirectoryEntry>() != null);

      return default(System.DirectoryServices.DirectoryEntry);
    }

    public ReplicationConnection(DirectoryContext context, string name, DirectoryServer sourceServer, ActiveDirectorySchedule schedule, ActiveDirectoryTransportType transport)
    {
      Contract.Requires(context != null);
      Contract.Requires(!string.IsNullOrEmpty(name));
    }

    public ReplicationConnection(DirectoryContext context, string name, DirectoryServer sourceServer, ActiveDirectoryTransportType transport)
    {
      Contract.Requires(context != null);
      Contract.Requires(!string.IsNullOrEmpty(name));
    }

    public ReplicationConnection(DirectoryContext context, string name, DirectoryServer sourceServer)
    {
      Contract.Requires(context != null);
      Contract.Requires(!string.IsNullOrEmpty(name));
    }

    public ReplicationConnection(DirectoryContext context, string name, DirectoryServer sourceServer, ActiveDirectorySchedule schedule)
    {
      Contract.Requires(context != null);
      Contract.Requires(!string.IsNullOrEmpty(name));
    }

    public void Save()
    {
    }

    #endregion

    #region Properties and indexers
    public NotificationStatus ChangeNotificationStatus
    {
      get
      {
        return default(NotificationStatus);
      }
      set
      {
      }
    }

    public bool DataCompressionEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string DestinationServer
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public bool Enabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool GeneratedByKcc
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string Name
    {
      get
      {
        Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

        return default(string);
      }
    }

    public bool ReciprocalReplicationEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ActiveDirectorySchedule ReplicationSchedule
    {
      // it may return null
      get
      {
        return default(ActiveDirectorySchedule);
      }
      set
      {
      }
    }

    public bool ReplicationScheduleOwnedByUser
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ReplicationSpan ReplicationSpan
    {
      get
      {
        return default(ReplicationSpan);
      }
    }

    public string SourceServer
    {
      // it may return null
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
    #endregion
  }
}
