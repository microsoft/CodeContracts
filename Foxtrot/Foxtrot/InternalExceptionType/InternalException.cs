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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;

  [Serializable]
  [SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic")]
  internal sealed class ContractException : Exception {

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    public ContractFailureKind Kind { get { return this.m_data._Kind; } }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    public string Failure { get { return this.Message; } }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    public string UserMessage { get { return this.m_data._UserMessage; } }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    public string Condition { get { return this.m_data._Condition; } }

    public ContractException(ContractFailureKind kind, string failure, string userMessage, string condition, Exception innerException)
      : base(failure, innerException) {
      this.m_data._Kind = kind;
      this.m_data._UserMessage = userMessage;
      this.m_data._Condition = condition;
      SerializeObjectState += delegate(object exception, SafeSerializationEventArgs eventArgs) {
        // This is fired when the exception is being serialized and asks
        // the exception to provide a serializable chunk of state to save
        // with the rest of the exception.
        eventArgs.AddSerializedState(m_data);
      };
    }

    [Serializable]
    private struct ContractExceptionData : ISafeSerializationData {
      public ContractFailureKind _Kind;
      public string _UserMessage;
      public string _Condition;

      void ISafeSerializationData.CompleteDeserialization(object obj) {
        // This is called when the exception has been deserialized, and tells
        // this exception data object to push its value back into the deserialized
        // exception object.
        ContractException exception = obj as ContractException;
        exception.m_data = this;
      }
    }
    [NonSerialized]
    private ContractExceptionData m_data = new ContractExceptionData();

  }
