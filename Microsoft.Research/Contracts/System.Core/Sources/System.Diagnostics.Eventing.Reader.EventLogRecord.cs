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

// File System.Diagnostics.Eventing.Reader.EventLogRecord.cs
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
  public partial class EventLogRecord : EventRecord
  {
    #region Methods and constructors
    protected override void Dispose(bool disposing)
    {
    }

    internal EventLogRecord()
    {
    }

    public override string FormatDescription(IEnumerable<Object> values)
    {
      return default(string);
    }

    public override string FormatDescription()
    {
      return default(string);
    }

    public IList<Object> GetPropertyValues(EventLogPropertySelector propertySelector)
    {
      Contract.Ensures(Contract.Result<System.Collections.Generic.IList<System.Object>>() != null);

      return default(IList<Object>);
    }

    public override string ToXml()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public override Nullable<Guid> ActivityId
    {
      get
      {
        return default(Nullable<Guid>);
      }
    }

    public override EventBookmark Bookmark
    {
      get
      {
        return default(EventBookmark);
      }
    }

    public string ContainerLog
    {
      get
      {
        return default(string);
      }
    }

    public override int Id
    {
      get
      {
        return default(int);
      }
    }

    public override Nullable<long> Keywords
    {
      get
      {
        return default(Nullable<long>);
      }
    }

    public override IEnumerable<string> KeywordsDisplayNames
    {
      get
      {
        return default(IEnumerable<string>);
      }
    }

    public override Nullable<byte> Level
    {
      get
      {
        return default(Nullable<byte>);
      }
    }

    public override string LevelDisplayName
    {
      get
      {
        return default(string);
      }
    }

    public override string LogName
    {
      get
      {
        return default(string);
      }
    }

    public override string MachineName
    {
      get
      {
        return default(string);
      }
    }

    public IEnumerable<int> MatchedQueryIds
    {
      get
      {
        return default(IEnumerable<int>);
      }
    }

    public override Nullable<short> Opcode
    {
      get
      {
        return default(Nullable<short>);
      }
    }

    public override string OpcodeDisplayName
    {
      get
      {
        return default(string);
      }
    }

    public override Nullable<int> ProcessId
    {
      get
      {
        return default(Nullable<int>);
      }
    }

    public override IList<EventProperty> Properties
    {
      get
      {
        return default(IList<EventProperty>);
      }
    }

    public override Nullable<Guid> ProviderId
    {
      get
      {
        return default(Nullable<Guid>);
      }
    }

    public override string ProviderName
    {
      get
      {
        return default(string);
      }
    }

    public override Nullable<int> Qualifiers
    {
      get
      {
        return default(Nullable<int>);
      }
    }

    public override Nullable<long> RecordId
    {
      get
      {
        return default(Nullable<long>);
      }
    }

    public override Nullable<Guid> RelatedActivityId
    {
      get
      {
        return default(Nullable<Guid>);
      }
    }

    public override Nullable<int> Task
    {
      get
      {
        return default(Nullable<int>);
      }
    }

    public override string TaskDisplayName
    {
      get
      {
        return default(string);
      }
    }

    public override Nullable<int> ThreadId
    {
      get
      {
        return default(Nullable<int>);
      }
    }

    public override Nullable<DateTime> TimeCreated
    {
      get
      {
        return default(Nullable<DateTime>);
      }
    }

    public override System.Security.Principal.SecurityIdentifier UserId
    {
      get
      {
        return default(System.Security.Principal.SecurityIdentifier);
      }
    }

    public override Nullable<byte> Version
    {
      get
      {
        return default(Nullable<byte>);
      }
    }
    #endregion
  }
}
