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

#if !SILVERLIGHT || SILVERLIGHT_5_0

using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  // Summary:
  //     Identifies kinds of exception-handling clauses.
  public enum ExceptionHandlingClauseOptions
  {
    // Summary:
    //     The clause accepts all exceptions that derive from a specified type.
    Clause = 0,
    //
    // Summary:
    //     The clause contains user-specified instructions that determine whether the
    //     exception should be ignored (that is, whether normal execution should resume),
    //     be handled by the associated handler, or be passed on to the next clause.
    Filter = 1,
    //
    // Summary:
    //     The clause is executed whenever the try block exits, whether through normal
    //     control flow or because of an unhandled exception.
    Finally = 2,
    //
    // Summary:
    //     The clause is executed if an exception occurs, but not on completion of normal
    //     control flow.
    Fault = 4,
  }
}

#endif