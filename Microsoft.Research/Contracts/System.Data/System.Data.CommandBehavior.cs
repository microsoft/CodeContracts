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
  //     Provides a description of the results of the query and its effect on the
  //     database.
  [Flags]
  public enum CommandBehavior
  {
    // Summary:
    //     The query may return multiple result sets. Execution of the query may affect
    //     the database state. Default sets no System.Data.CommandBehavior flags, so
    //     calling ExecuteReader(CommandBehavior.Default) is functionally equivalent
    //     to calling ExecuteReader().
    Default = 0,
    //
    // Summary:
    //     The query returns a single result set.
    SingleResult = 1,
    //
    // Summary:
    //     The query returns column information only. When using System.Data.CommandBehavior.SchemaOnly,
    //     the .NET Framework Data Provider for SQL Server precedes the statement being
    //     executed with SET FMTONLY ON.
    SchemaOnly = 2,
    //
    // Summary:
    //     The query returns column and primary key information.
    KeyInfo = 4,
    //
    // Summary:
    //     The query is expected to return a single row. Execution of the query may
    //     affect the database state. Some .NET Framework data providers may, but are
    //     not required to, use this information to optimize the performance of the
    //     command. When you specify System.Data.CommandBehavior.SingleRow with the
    //     System.Data.OleDb.OleDbCommand.ExecuteReader() method of the System.Data.OleDb.OleDbCommand
    //     object, the .NET Framework Data Provider for OLE DB performs binding using
    //     the OLE DB IRow interface if it is available. Otherwise, it uses the IRowset
    //     interface. If your SQL statement is expected to return only a single row,
    //     specifying System.Data.CommandBehavior.SingleRow can also improve application
    //     performance. It is possible to specify SingleRow when executing queries that
    //     return multiple result sets. In that case, multiple result sets are still
    //     returned, but each result set has a single row.
    SingleRow = 8,
    //
    // Summary:
    //     Provides a way for the DataReader to handle rows that contain columns with
    //     large binary values. Rather than loading the entire row, SequentialAccess
    //     enables the DataReader to load data as a stream. You can then use the GetBytes
    //     or GetChars method to specify a byte location to start the read operation,
    //     and a limited buffer size for the data being returned.
    SequentialAccess = 16,
    //
    // Summary:
    //     When the command is executed, the associated Connection object is closed
    //     when the associated DataReader object is closed.
    CloseConnection = 32,
  }
}
