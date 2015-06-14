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

// File System.Threading.Barrier.cs
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


namespace System.Threading
{
  public partial class Barrier : IDisposable
  {
    #region Methods and constructors
    public long AddParticipant()
    {
      Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());

      return default(long);
    }

    public long AddParticipants(int participantCount)
    {
      Contract.Ensures(-9223372036854775807 <= Contract.Result<long>());

      return default(long);
    }

    public Barrier(int participantCount)
    {
    }

    public Barrier(int participantCount, Action<System.Threading.Barrier> postPhaseAction)
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public void RemoveParticipant()
    {
    }

    public void RemoveParticipants(int participantCount)
    {
    }

    public bool SignalAndWait(TimeSpan timeout)
    {
      return default(bool);
    }

    public void SignalAndWait(CancellationToken cancellationToken)
    {
    }

    public void SignalAndWait()
    {
    }

    public bool SignalAndWait(TimeSpan timeout, CancellationToken cancellationToken)
    {
      return default(bool);
    }

    public bool SignalAndWait(int millisecondsTimeout, CancellationToken cancellationToken)
    {
      return default(bool);
    }

    public bool SignalAndWait(int millisecondsTimeout)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public long CurrentPhaseNumber
    {
      get
      {
        return default(long);
      }
    }

    public int ParticipantCount
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 32767);

        return default(int);
      }
    }

    public int ParticipantsRemaining
    {
      get
      {
        Contract.Ensures(-32767 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 32767);

        return default(int);
      }
    }
    #endregion
  }
}
