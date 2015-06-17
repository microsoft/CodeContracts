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

// File System.ServiceModel.Channels.MessageVersion.cs
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


namespace System.ServiceModel.Channels
{
  sealed public partial class MessageVersion
  {
    #region Methods and constructors
    public static MessageVersion CreateVersion(System.ServiceModel.EnvelopeVersion envelopeVersion)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);

      return default(MessageVersion);
    }

    public static MessageVersion CreateVersion(System.ServiceModel.EnvelopeVersion envelopeVersion, AddressingVersion addressingVersion)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);

      return default(MessageVersion);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    private MessageVersion()
    {
    }

    #endregion

    #region Properties and indexers
    public AddressingVersion Addressing
    {
      get
      {
        Contract.Ensures(Contract.Result<AddressingVersion>() != null);

        return default(AddressingVersion);
      }
    }

    public static System.ServiceModel.Channels.MessageVersion Default
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);

        return default(System.ServiceModel.Channels.MessageVersion);
      }
    }

    public System.ServiceModel.EnvelopeVersion Envelope
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.EnvelopeVersion>() != null);

        return default(System.ServiceModel.EnvelopeVersion);
      }
    }

    public static System.ServiceModel.Channels.MessageVersion None
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);

        return default(System.ServiceModel.Channels.MessageVersion);
      }
    }

    public static System.ServiceModel.Channels.MessageVersion Soap11
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);

        return default(System.ServiceModel.Channels.MessageVersion);
      }
    }

    public static System.ServiceModel.Channels.MessageVersion Soap11WSAddressing10
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);

        return default(System.ServiceModel.Channels.MessageVersion);
      }
    }

    public static System.ServiceModel.Channels.MessageVersion Soap11WSAddressingAugust2004
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);

        return default(System.ServiceModel.Channels.MessageVersion);
      }
    }

    public static System.ServiceModel.Channels.MessageVersion Soap12
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);

        return default(System.ServiceModel.Channels.MessageVersion);
      }
    }

    public static System.ServiceModel.Channels.MessageVersion Soap12WSAddressing10
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);

        return default(System.ServiceModel.Channels.MessageVersion);
      }
    }

    public static System.ServiceModel.Channels.MessageVersion Soap12WSAddressingAugust2004
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.MessageVersion>() != null);

        return default(System.ServiceModel.Channels.MessageVersion);
      }
    }
    #endregion
  }
}
