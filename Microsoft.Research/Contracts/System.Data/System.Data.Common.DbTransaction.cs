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
using System.Data;

namespace System.Data.Common
{
  // Summary:
  //     The base class for a transaction.
  public abstract class DbTransaction // : MarshalByRefObject, IDbTransaction, IDisposable
  {
    
    // Summary:
    //     Specifies the System.Data.Common.DbConnection object associated with the
    //     transaction.
    //
    // Returns:
    //     The System.Data.Common.DbConnection object associated with the transaction.
    //public DbConnection Connection { get; }

    //
    // Summary:
    //     Specifies the System.Data.IsolationLevel for this transaction.
    //
    // Returns:
    //     The System.Data.IsolationLevel for this transaction.
    //public abstract IsolationLevel IsolationLevel { get; }

    // Summary:
    //     Commits the database transaction.
    //public abstract void Commit();
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Data.Common.DbTransaction.
    //public void Dispose();
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Data.Common.DbTransaction
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     If true, this method releases all resources held by any managed objects that
    //     this System.Data.Common.DbTransaction references.
    //protected virtual void Dispose(bool disposing);
    //
    // Summary:
    //     Rolls back a transaction from a pending state.
    //public abstract void Rollback();
  }
}
