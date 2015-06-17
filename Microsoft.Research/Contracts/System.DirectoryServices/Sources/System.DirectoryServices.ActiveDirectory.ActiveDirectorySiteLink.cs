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

// File System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink.cs
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
  public partial class ActiveDirectorySiteLink : IDisposable
  {
    #region Methods and constructors
    public ActiveDirectorySiteLink(DirectoryContext context, string siteLinkName, ActiveDirectoryTransportType transport, ActiveDirectorySchedule schedule)
    {
      Contract.Requires(context != null);
      Contract.Requires(siteLinkName != null);
      // Schedule can be null
    }

    public ActiveDirectorySiteLink(DirectoryContext context, string siteLinkName, ActiveDirectoryTransportType transport)
    {
      Contract.Requires(context != null);
      Contract.Requires(siteLinkName != null);
    }

    public ActiveDirectorySiteLink(DirectoryContext context, string siteLinkName)
    {
      Contract.Requires(context != null);
      Contract.Requires(siteLinkName != null);
    }

    public void Delete()
    {
    }

    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public static System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink FindByName(DirectoryContext context, string siteLinkName, ActiveDirectoryTransportType transport)
    {
      Contract.Requires(context != null);
      Contract.Requires(siteLinkName != null);

      Contract.Ensures(Contract.Result<ActiveDirectorySiteLink>() != null);

      return default(System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink);
    }

    public static System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink FindByName(DirectoryContext context, string siteLinkName)
    {
      Contract.Requires(context != null);
      Contract.Requires(siteLinkName != null);
      
      Contract.Ensures(Contract.Result<ActiveDirectorySiteLink>() != null);

      return default(System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink);
    }

    public System.DirectoryServices.DirectoryEntry GetDirectoryEntry()
    {
      return default(System.DirectoryServices.DirectoryEntry);
    }

    public void Save()
    {
    }

    #endregion

    #region Properties and indexers
    public int Cost
    {
      get
      {
        return default(int);
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

    public ActiveDirectorySchedule InterSiteReplicationSchedule
    {
      // can get null
      get
      {
        return default(ActiveDirectorySchedule);
      }
      set
      {
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public bool NotificationEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
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

    public TimeSpan ReplicationInterval
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public ActiveDirectorySiteCollection Sites
    {
      get
      {
        Contract.Ensures(Contract.Result<ActiveDirectorySiteCollection>() != null);
        return default(ActiveDirectorySiteCollection);
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
