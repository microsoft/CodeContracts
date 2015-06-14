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

// File System.ServiceModel.MsmqIntegration.MsmqIntegrationMessageProperty.cs
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
  sealed public partial class MsmqIntegrationMessageProperty
  {
    #region Methods and constructors
    public static MsmqIntegrationMessageProperty Get(System.ServiceModel.Channels.Message message)
    {
      return default(MsmqIntegrationMessageProperty);
    }

    public MsmqIntegrationMessageProperty()
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
      internal set
      {
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
      internal set
      {
      }
    }

    public Nullable<bool> Authenticated
    {
      get
      {
        return default(Nullable<bool>);
      }
      internal set
      {
      }
    }

    public Object Body
    {
      get
      {
        return default(Object);
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
      internal set
      {
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
      internal set
      {
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
      internal set
      {
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
      internal set
      {
      }
    }

    public Nullable<DateTime> SentTime
    {
      get
      {
        return default(Nullable<DateTime>);
      }
      internal set
      {
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

    #region Fields
    #endregion
  }
}
