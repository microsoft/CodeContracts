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
  //     Represents a clause in a structured exception-handling block.
  public class ExceptionHandlingClause
  {
    protected ExceptionHandlingClause() { }

    // Summary:
    //     Gets the type of exception handled by this clause.
    //
    // Returns:
    //     A System.Type object that represents that type of exception handled by this
    //     clause, or null if the System.Reflection.ExceptionHandlingClause.Flags property
    //     is System.Reflection.ExceptionHandlingClauseOptions.Filter or System.Reflection.ExceptionHandlingClauseOptions.Finally.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     Invalid use of property for the object's current state.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    extern public Type CatchType { get; }
    //
    // Summary:
    //     Gets the offset within the method body, in bytes, of the user-supplied filter
    //     code.
    //
    // Returns:
    //     The offset within the method body, in bytes, of the user-supplied filter
    //     code. The value of this property has no meaning if the System.Reflection.ExceptionHandlingClause.Flags
    //     property has any value other than System.Reflection.ExceptionHandlingClauseOptions.Filter.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     Cannot get the offset because the exception handling clause is not a filter.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    extern public int FilterOffset { get; }
    //
    // Summary:
    //     Gets a value indicating whether this exception-handling clause is a finally
    //     clause, a type-filtered clause, or a user-filtered clause.
    //
    // Returns:
    //     An System.Reflection.ExceptionHandlingClauseOptions value that indicates
    //     what kind of action this clause performs.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    extern public ExceptionHandlingClauseOptions Flags { get; }
    //
    // Summary:
    //     Gets the length, in bytes, of the body of this exception-handling clause.
    //
    // Returns:
    //     An integer that represents the length, in bytes, of the MSIL that forms the
    //     body of this exception-handling clause.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    extern public int HandlerLength { get; }
    //
    // Summary:
    //     Gets the offset within the method body, in bytes, of this exception-handling
    //     clause.
    //
    // Returns:
    //     An integer that represents the offset within the method body, in bytes, of
    //     this exception-handling clause.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    extern public int HandlerOffset { get; }
    //
    // Summary:
    //     The total length, in bytes, of the try block that includes this exception-handling
    //     clause.
    //
    // Returns:
    //     The total length, in bytes, of the try block that includes this exception-handling
    //     clause.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    extern public int TryLength { get; }
    //
    // Summary:
    //     The offset within the method, in bytes, of the try block that includes this
    //     exception-handling clause.
    //
    // Returns:
    //     An integer that represents the offset within the method, in bytes, of the
    //     try block that includes this exception-handling clause.
#if NETFRAMEWORK_3_5
#else
    virtual
#endif
    extern public int TryOffset { get; }

  }
}

#endif