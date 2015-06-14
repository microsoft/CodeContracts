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

// File System.AggregateException.cs
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


namespace System
{
  public partial class AggregateException : Exception
  {
    #region Methods and constructors
    public AggregateException(string message, IEnumerable<Exception> innerExceptions)
    {
      Contract.Ensures(0 <= message.Length);
    }

    public AggregateException(Exception[] innerExceptions)
    {
      Contract.Ensures(0 <= ((System.Collections.Generic.ICollection)innerExceptions).Count);
      Contract.Ensures(innerExceptions.Length == ((System.Collections.Generic.ICollection)innerExceptions).Count);
    }

    protected AggregateException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public AggregateException(string message, Exception[] innerExceptions)
    {
      Contract.Requires(0 <= ((System.Collections.Generic.ICollection)innerExceptions).Count);
      Contract.Ensures(0 <= ((System.Collections.Generic.ICollection)innerExceptions).Count);
      Contract.Ensures(0 <= message.Length);
    }

    public AggregateException(string message)
    {
    }

    public AggregateException()
    {
    }

    public AggregateException(IEnumerable<Exception> innerExceptions)
    {
    }

    public AggregateException(string message, Exception innerException)
    {
    }

    public System.AggregateException Flatten()
    {
      Contract.Ensures(0 <= this.Message.Length);
      Contract.Ensures(Contract.Result<System.AggregateException>() != null);

      return default(System.AggregateException);
    }

    public override Exception GetBaseException()
    {
      return default(Exception);
    }

    public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public void Handle(Func<Exception, bool> predicate)
    {
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public System.Collections.ObjectModel.ReadOnlyCollection<Exception> InnerExceptions
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<Exception>);
      }
    }
    #endregion
  }
}
