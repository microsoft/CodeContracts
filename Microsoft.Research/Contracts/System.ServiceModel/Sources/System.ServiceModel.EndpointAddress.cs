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

// File System.ServiceModel.EndpointAddress.cs
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


namespace System.ServiceModel
{
  public partial class EndpointAddress
  {
    #region Methods and constructors
    public static bool operator != (System.ServiceModel.EndpointAddress address1, System.ServiceModel.EndpointAddress address2)
    {
      return default(bool);
    }

    public static bool operator == (System.ServiceModel.EndpointAddress address1, System.ServiceModel.EndpointAddress address2)
    {
      return default(bool);
    }

    public void ApplyTo(System.ServiceModel.Channels.Message message)
    {
      Contract.Requires(message != null);
    }

    public EndpointAddress(string uri)
    {
      Contract.Requires(uri != null);
    }

    public EndpointAddress(Uri uri, EndpointIdentity identity, System.ServiceModel.Channels.AddressHeaderCollection headers)
    {
      Contract.Requires(uri != null);
      Contract.Requires(uri.IsAbsoluteUri);
    }

    public EndpointAddress(Uri uri, System.ServiceModel.Channels.AddressHeader[] addressHeaders)
    {
      Contract.Requires(uri != null);
      Contract.Requires(uri.IsAbsoluteUri);
    }

    public EndpointAddress(Uri uri, EndpointIdentity identity, System.ServiceModel.Channels.AddressHeader[] addressHeaders)
    {
      Contract.Requires(uri != null);
      Contract.Requires(uri.IsAbsoluteUri);
    }

    public EndpointAddress(Uri uri, EndpointIdentity identity, System.ServiceModel.Channels.AddressHeaderCollection headers, System.Xml.XmlDictionaryReader metadataReader, System.Xml.XmlDictionaryReader extensionReader)
    {
      Contract.Requires(uri != null);
      Contract.Requires(uri.IsAbsoluteUri);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public System.Xml.XmlDictionaryReader GetReaderAtExtensions()
    {
      return default(System.Xml.XmlDictionaryReader);
    }

    public System.Xml.XmlDictionaryReader GetReaderAtMetadata()
    {
      return default(System.Xml.XmlDictionaryReader);
    }

    public static System.ServiceModel.EndpointAddress ReadFrom(System.ServiceModel.Channels.AddressingVersion addressingVersion, System.Xml.XmlDictionaryReader reader, System.Xml.XmlDictionaryString localName, System.Xml.XmlDictionaryString ns)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointAddress>() != null);

      return default(System.ServiceModel.EndpointAddress);
    }

    public static System.ServiceModel.EndpointAddress ReadFrom(System.ServiceModel.Channels.AddressingVersion addressingVersion, System.Xml.XmlReader reader, string localName, string ns)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointAddress>() != null);

      return default(System.ServiceModel.EndpointAddress);
    }

    public static System.ServiceModel.EndpointAddress ReadFrom(System.ServiceModel.Channels.AddressingVersion addressingVersion, System.Xml.XmlDictionaryReader reader)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointAddress>() != null);

      return default(System.ServiceModel.EndpointAddress);
    }

    public static System.ServiceModel.EndpointAddress ReadFrom(System.ServiceModel.Channels.AddressingVersion addressingVersion, System.Xml.XmlReader reader)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointAddress>() != null);

      return default(System.ServiceModel.EndpointAddress);
    }

    public static System.ServiceModel.EndpointAddress ReadFrom(System.Xml.XmlDictionaryReader reader)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointAddress>() != null);

      return default(System.ServiceModel.EndpointAddress);
    }

    public static System.ServiceModel.EndpointAddress ReadFrom(System.Xml.XmlDictionaryReader reader, System.Xml.XmlDictionaryString localName, System.Xml.XmlDictionaryString ns)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.EndpointAddress>() != null);

      return default(System.ServiceModel.EndpointAddress);
    }

    public void WriteContentsTo(System.ServiceModel.Channels.AddressingVersion addressingVersion, System.Xml.XmlWriter writer)
    {
    }

    public void WriteContentsTo(System.ServiceModel.Channels.AddressingVersion addressingVersion, System.Xml.XmlDictionaryWriter writer)
    {
    }

    public void WriteTo(System.ServiceModel.Channels.AddressingVersion addressingVersion, System.Xml.XmlDictionaryWriter writer)
    {
      Contract.Requires(addressingVersion != null);
    }

    public void WriteTo(System.ServiceModel.Channels.AddressingVersion addressingVersion, System.Xml.XmlWriter writer)
    {
      Contract.Requires(addressingVersion != null);
    }

    public void WriteTo(System.ServiceModel.Channels.AddressingVersion addressingVersion, System.Xml.XmlWriter writer, string localName, string ns)
    {
    }

    public void WriteTo(System.ServiceModel.Channels.AddressingVersion addressingVersion, System.Xml.XmlDictionaryWriter writer, System.Xml.XmlDictionaryString localName, System.Xml.XmlDictionaryString ns)
    {
    }
    #endregion

    #region Properties and indexers
    public static Uri AnonymousUri
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Uri>() != null);

        return default(Uri);
      }
    }

    public System.ServiceModel.Channels.AddressHeaderCollection Headers
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ServiceModel.Channels.AddressHeaderCollection>() != null);

        return default(System.ServiceModel.Channels.AddressHeaderCollection);
      }
    }

    public EndpointIdentity Identity
    {
      get
      {
        return default(EndpointIdentity);
      }
    }

    public bool IsAnonymous
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNone
    {
      get
      {
        return default(bool);
      }
    }

    public static Uri NoneUri
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Uri>() != null);

        return default(Uri);
      }
    }

    public Uri Uri
    {
      get
      {
        Contract.Ensures(Contract.Result<Uri>() != null);

        return default(Uri);
      }
    }
    #endregion
  }
}
