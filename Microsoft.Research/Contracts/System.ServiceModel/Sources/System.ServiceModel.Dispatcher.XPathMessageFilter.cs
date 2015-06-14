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

// File System.ServiceModel.Dispatcher.XPathMessageFilter.cs
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


namespace System.ServiceModel.Dispatcher
{
  public partial class XPathMessageFilter : MessageFilter, System.Xml.Serialization.IXmlSerializable
  {
    #region Methods and constructors
    protected internal override IMessageFilterTable<FilterData> CreateFilterTable<FilterData>()
    {
      return default(IMessageFilterTable<FilterData>);
    }

    public override bool Match(System.ServiceModel.Channels.Message message)
    {
      return default(bool);
    }

    public bool Match(SeekableXPathNavigator navigator)
    {
      return default(bool);
    }

    public bool Match(System.Xml.XPath.XPathNavigator navigator)
    {
      return default(bool);
    }

    public override bool Match(System.ServiceModel.Channels.MessageBuffer messageBuffer)
    {
      return default(bool);
    }

    protected virtual new System.Xml.Schema.XmlSchema OnGetSchema()
    {
      return default(System.Xml.Schema.XmlSchema);
    }

    protected virtual new void OnReadXml(System.Xml.XmlReader reader)
    {
    }

    protected virtual new void OnWriteXml(System.Xml.XmlWriter writer)
    {
    }

    protected void ReadXPath(System.Xml.XmlReader reader, System.Xml.XmlNamespaceManager namespaces)
    {
      Contract.Requires(reader != null);
    }

    public static System.Xml.Schema.XmlSchemaType StaticGetSchema(System.Xml.Schema.XmlSchemaSet schemas)
    {
      Contract.Ensures(Contract.Result<System.Xml.Schema.XmlSchemaType>() != null);

      return default(System.Xml.Schema.XmlSchemaType);
    }

    System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema()
    {
      return default(System.Xml.Schema.XmlSchema);
    }

    void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
    {
    }

    void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
    {
    }

    public void TrimToSize()
    {
    }

    protected void WriteXPath(System.Xml.XmlWriter writer, System.Xml.IXmlNamespaceResolver resolver)
    {
    }

    public void WriteXPathTo(System.Xml.XmlWriter writer, string prefix, string localName, string ns, bool writeNamespaces)
    {
      Contract.Requires(!String.IsNullOrEmpty(localName));
    }

    public XPathMessageFilter()
    {
    }

    public XPathMessageFilter(System.Xml.XmlReader reader)
    {
    }

    public XPathMessageFilter(string xpath)
    {
    }

    public XPathMessageFilter(System.Xml.XmlReader reader, System.Xml.XmlNamespaceManager namespaces)
    {
    }

    public XPathMessageFilter(string xpath, System.Xml.Xsl.XsltContext context)
    {
    }

    public XPathMessageFilter(string xpath, System.Xml.XmlNamespaceManager namespaces)
    {
    }

    public XPathMessageFilter(System.Xml.XmlReader reader, System.Xml.Xsl.XsltContext context)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Xml.XmlNamespaceManager Namespaces
    {
      get
      {
        return default(System.Xml.XmlNamespaceManager);
      }
    }

    public int NodeQuota
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string XPath
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
