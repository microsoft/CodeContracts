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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace Adornments {
  public sealed class Logger {
    readonly Action<string> _writeToLog;
    readonly Action<string, Action> _publicEntry;
    readonly Action<string, Exception> _publicEntryException;
    public event Action<Action<string>> Failed;
    public event Action Idle;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_writeToLog != null);
      Contract.Invariant(_publicEntry != null);
      Contract.Invariant(_publicEntryException != null);
    }

    public Logger(Action<string> writeToLog, Action<string, Action> publicEntry, Action<string, Exception> publicEntryException) {
      Contract.Requires(writeToLog != null);
      Contract.Requires(publicEntry != null);
      Contract.Requires(publicEntryException != null);
      _writeToLog = writeToLog;
      _publicEntry = publicEntry;
      _publicEntryException = publicEntryException;
    }

    public void OnFailed(Action<string> writeToLog) {
      if (Failed != null)
        Failed(writeToLog);
    }
    public void OnIdle() {
      if (Idle != null)
        Idle();
    }
    /*
     * I wrap the delegates in methods so that I can add my own checks to make 
     * sure everything is non-null/empty, and so I can conditionaly define the 
     * "WriteToLog" method.
     */

    [Conditional("DEBUG")]
    public void WriteToLog(string message) {
      if (_writeToLog != null) {
        if (!String.IsNullOrEmpty(message))
          _writeToLog(message);
      }
    }

    public void PublicEntry(string entryName, Action action) {
      if (_publicEntry != null) {
        if (!String.IsNullOrEmpty(entryName) && action != null)
          _publicEntry(entryName, action);
      }
    }

    public void PublicEntryException(string entryName, Exception exn) {
      if (_publicEntryException != null) {
        if (!String.IsNullOrEmpty(entryName) && exn != null)
          _publicEntryException(entryName, exn);
      }
    }
  }
}
