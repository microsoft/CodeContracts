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

// File System.ServiceModel.MsmqIntegration.MsmqMessage_1.cs
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


namespace System.ServiceModel.MsmqIntegration
{
  sealed public partial class MsmqMessage<T>
  {
    #region Methods and constructors
    public MsmqMessage(T body)
    {
    }
    #endregion

    #region Properties and indexers
    public Nullable<System.Messaging.AcknowledgeTypes> AcknowledgeType
    {
      get
      {
        return default(Nullable<System.Messaging.AcknowledgeTypes>);
      }
      set
      {
      }
    }

    public Nullable<System.Messaging.Acknowledgment> Acknowledgment
    {
      get
      {
        return default(Nullable<System.Messaging.Acknowledgment>);
      }
    }

    public Uri AdministrationQueue
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public Nullable<int> AppSpecific
    {
      get
      {
        return default(Nullable<int>);
      }
      set
      {
      }
    }

    public Nullable<DateTime> ArrivedTime
    {
      get
      {
        return default(Nullable<DateTime>);
      }
    }

    public Nullable<bool> Authenticated
    {
      get
      {
        return default(Nullable<bool>);
      }
    }

    public T Body
    {
      get
      {
        Contract.Ensures(Contract.Result<T>() != null);

        return default(T);
      }
      set
      {
      }
    }

    public Nullable<int> BodyType
    {
      get
      {
        return default(Nullable<int>);
      }
      set
      {
      }
    }

    public string CorrelationId
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Uri DestinationQueue
    {
      get
      {
        return default(Uri);
      }
    }

    public byte[] Extension
    {
      get
      {
        return default(byte[]);
      }
      set
      {
      }
    }

    public string Id
    {
      get
      {
        return default(string);
      }
    }

    public string Label
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Nullable<System.Messaging.MessageType> MessageType
    {
      get
      {
        return default(Nullable<System.Messaging.MessageType>);
      }
    }

    public Nullable<System.Messaging.MessagePriority> Priority
    {
      get
      {
        return default(Nullable<System.Messaging.MessagePriority>);
      }
      set
      {
      }
    }

    public Uri ResponseQueue
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public byte[] SenderId
    {
      get
      {
        return default(byte[]);
      }
    }

    public Nullable<DateTime> SentTime
    {
      get
      {
        return default(Nullable<DateTime>);
      }
    }

    public Nullable<TimeSpan> TimeToReachQueue
    {
      get
      {
        return default(Nullable<TimeSpan>);
      }
      set
      {
      }
    }
    #endregion
  }
}
