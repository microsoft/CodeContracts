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

namespace System.Data
{
  // Summary:
  //     Specifies the transaction locking behavior for the connection.
  public enum IsolationLevel
  {
    // Summary:
    //     A different isolation level than the one specified is being used, but the
    //     level cannot be determined.
    Unspecified = -1,
    //
    // Summary:
    //     The pending changes from more highly isolated transactions cannot be overwritten.
    Chaos = 16,
    //
    // Summary:
    //     A dirty read is possible, meaning that no shared locks are issued and no
    //     exclusive locks are honored.
    ReadUncommitted = 256,
    //
    // Summary:
    //     Shared locks are held while the data is being read to avoid dirty reads,
    //     but the data can be changed before the end of the transaction, resulting
    //     in non-repeatable reads or phantom data.
    ReadCommitted = 4096,
    //
    // Summary:
    //     Locks are placed on all data that is used in a query, preventing other users
    //     from updating the data. Prevents non-repeatable reads but phantom rows are
    //     still possible.
    RepeatableRead = 65536,
    //
    // Summary:
    //     A range lock is placed on the System.Data.DataSet, preventing other users
    //     from updating or inserting rows into the dataset until the transaction is
    //     complete.
    Serializable = 1048576,
    //
    // Summary:
    //     Reduces blocking by storing a version of data that one application can read
    //     while another is modifying the same data. Indicates that from one transaction
    //     you cannot see changes made in other transactions, even if you requery.
    Snapshot = 16777216,
  }
}
