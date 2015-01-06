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

// File System.Diagnostics.Eventing.Reader.EventRecord.cs
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
  abstract public partial class EventRecord : IDisposable
  {
    #region Methods and constructors
    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    protected EventRecord()
    {
    }

    public abstract string FormatDescription(IEnumerable<Object> values);

    public abstract string FormatDescription();

    public abstract string ToXml();
    #endregion

    #region Properties and indexers
    public abstract Nullable<Guid> ActivityId
    {
      get;
    }

    public abstract EventBookmark Bookmark
    {
      get;
    }

    public abstract int Id
    {
      get;
    }

    public abstract Nullable<long> Keywords
    {
      get;
    }

    public abstract IEnumerable<string> KeywordsDisplayNames
    {
      get;
    }

    public abstract Nullable<byte> Level
    {
      get;
    }

    public abstract string LevelDisplayName
    {
      get;
    }

    public abstract string LogName
    {
      get;
    }

    public abstract string MachineName
    {
      get;
    }

    public abstract Nullable<short> Opcode
    {
      get;
    }

    public abstract string OpcodeDisplayName
    {
      get;
    }

    public abstract Nullable<int> ProcessId
    {
      get;
    }

    public abstract IList<EventProperty> Properties
    {
      get;
    }

    public abstract Nullable<Guid> ProviderId
    {
      get;
    }

    public abstract string ProviderName
    {
      get;
    }

    public abstract Nullable<int> Qualifiers
    {
      get;
    }

    public abstract Nullable<long> RecordId
    {
      get;
    }

    public abstract Nullable<Guid> RelatedActivityId
    {
      get;
    }

    public abstract Nullable<int> Task
    {
      get;
    }

    public abstract string TaskDisplayName
    {
      get;
    }

    public abstract Nullable<int> ThreadId
    {
      get;
    }

    public abstract Nullable<DateTime> TimeCreated
    {
      get;
    }

    public abstract System.Security.Principal.SecurityIdentifier UserId
    {
      get;
    }

    public abstract Nullable<byte> Version
    {
      get;
    }
    #endregion
  }
}
