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

#if NETFRAMEWORK_4_0

namespace System
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;
    using System.Security;
    //
    // Summary:
    //     Represents one or more errors that occur during application execution.
    public class AggregateException : Exception
    {
      //
      // Summary:
      //     Initializes a new instance of the System.AggregateException class with a system-supplied
      //     message that describes the error.
      //public AggregateException();
      //
      // Summary:
      //     Initializes a new instance of the System.AggregateException class with a specified
      //     message that describes the error.
      //
      // Parameters:
      //   message:
      //     The message that describes the exception. The caller of this constructor is required
      //     to ensure that this string has been localized for the current system culture.
      //public AggregateException(string message);
      //
      // Summary:
      //     Initializes a new instance of the System.AggregateException class with references
      //     to the inner exceptions that are the cause of this exception.
      //
      // Parameters:
      //   innerExceptions:
      //     The exceptions that are the cause of the current exception.
      //
      // Exceptions:
      //   T:System.ArgumentNullException:
      //     The innerExceptions argument is null.
      //
      //   T:System.ArgumentException:
      //     An element of innerExceptions is null.
      //public AggregateException(IEnumerable<Exception> innerExceptions);
      //
      // Summary:
      //     Initializes a new instance of the System.AggregateException class with references
      //     to the inner exceptions that are the cause of this exception.
      //
      // Parameters:
      //   innerExceptions:
      //     The exceptions that are the cause of the current exception.
      //
      // Exceptions:
      //   T:System.ArgumentNullException:
      //     The innerExceptions argument is null.
      //
      //   T:System.ArgumentException:
      //     An element of innerExceptions is null.
      //public AggregateException(params Exception[] innerExceptions);
      //
      // Summary:
      //     Initializes a new instance of the System.AggregateException class with a specified
      //     error message and a reference to the inner exception that is the cause of this
      //     exception.
      //
      // Parameters:
      //   message:
      //     The message that describes the exception. The caller of this constructor is required
      //     to ensure that this string has been localized for the current system culture.
      //
      //   innerException:
      //     The exception that is the cause of the current exception. If the innerException
      //     parameter is not null, the current exception is raised in a catch block that
      //     handles the inner exception.
      //
      // Exceptions:
      //   T:System.ArgumentNullException:
      //     The innerException argument is null.
      //public AggregateException(string message, Exception innerException);
      //
      // Summary:
      //     Initializes a new instance of the System.AggregateException class with a specified
      //     error message and references to the inner exceptions that are the cause of this
      //     exception.
      //
      // Parameters:
      //   message:
      //     The error message that explains the reason for the exception.
      //
      //   innerExceptions:
      //     The exceptions that are the cause of the current exception.
      //
      // Exceptions:
      //   T:System.ArgumentNullException:
      //     The innerExceptions argument is null.
      //
      //   T:System.ArgumentException:
      //     An element of innerExceptions is null.
      //public AggregateException(string message, IEnumerable<Exception> innerExceptions);
      //
      // Summary:
      //     Initializes a new instance of the System.AggregateException class with a specified
      //     error message and references to the inner exceptions that are the cause of this
      //     exception.
      //
      // Parameters:
      //   message:
      //     The error message that explains the reason for the exception.
      //
      //   innerExceptions:
      //     The exceptions that are the cause of the current exception.
      //
      // Exceptions:
      //   T:System.ArgumentNullException:
      //     The innerExceptions argument is null.
      //
      //   T:System.ArgumentException:
      //     An element of innerExceptions is null.
      //public AggregateException(string message, params Exception[] innerExceptions);
      //
      // Summary:
      //     Initializes a new instance of the System.AggregateException class with serialized
      //     data.
      //
      // Parameters:
      //   info:
      //     The object that holds the serialized object data.
      //
      //   context:
      //     The contextual information about the source or destination.
      //
      // Exceptions:
      //   T:System.ArgumentNullException:
      //     The info argument is null.
      //
      //   T:System.Runtime.Serialization.SerializationException:
      //     The exception could not be deserialized correctly.
      //[SecurityCritical]
      //protected AggregateException(SerializationInfo info, StreamingContext context);
      //
      // Summary:
      //     Gets a read-only collection of the System.Exception instances that caused the
      //     current exception.
      //
      // Returns:
      //     Returns a read-only collection of the System.Exception instances that caused
      //     the current exception.
      //public ReadOnlyCollection<Exception> InnerExceptions { get; }
      //
      // Summary:
      //     Flattens an System.AggregateException instances into a single, new instance.
      //
      // Returns:
      //     A new, flattened System.AggregateException.
      //public AggregateException Flatten();
      //
      // Summary:
      //     Returns the System.AggregateException that is the root cause of this exception.
      //
      // Returns:
      //     Returns the System.AggregateException that is the root cause of this exception.
      //public override Exception GetBaseException();
      //
      // Summary:
      //     Initializes a new instance of the System.AggregateException class with serialized
      //     data.
      //
      // Parameters:
      //   info:
      //     The object that holds the serialized object data.
      //
      //   context:
      //     The contextual information about the source or destination.
      //
      // Exceptions:
      //   T:System.ArgumentNullException:
      //     The info argument is null.
      //[SecurityCritical]
      //public override void GetObjectData(SerializationInfo info, StreamingContext context);
      //
      // Summary:
      //     Invokes a handler on each System.Exception contained by this System.AggregateException.
      //
      // Parameters:
      //   predicate:
      //     The predicate to execute for each exception. The predicate accepts as an argument
      //     the System.Exception to be processed and returns a Boolean to indicate whether
      //     the exception was handled.
      //
      // Exceptions:
      //   T:System.ArgumentNullException:
      //     The predicate argument is null.
      //
      //   T:System.AggregateException:
      //     An exception contained by this System.AggregateException was not handled.
      //public void Handle(Func<Exception, bool> predicate);
      //
      // Summary:
      //     Creates and returns a string representation of the current System.AggregateException.
      //
      // Returns:
      //     A string representation of the current exception.
      //public override string ToString();
    }
}

#endif
