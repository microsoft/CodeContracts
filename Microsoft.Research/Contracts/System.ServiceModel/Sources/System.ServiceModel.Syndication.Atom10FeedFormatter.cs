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

// File System.ServiceModel.Syndication.Atom10FeedFormatter.cs
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


namespace System.ServiceModel.Syndication
{
  public partial class Atom10FeedFormatter : SyndicationFeedFormatter, System.Xml.Serialization.IXmlSerializable
  {
    #region Methods and constructors
    public Atom10FeedFormatter(SyndicationFeed feedToWrite)
    {
      Contract.Requires(feedToWrite != null);
    }

    public Atom10FeedFormatter(Type feedTypeToCreate)
    {
    }

    public Atom10FeedFormatter()
    {
    }

    public override bool CanRead(System.Xml.XmlReader reader)
    {
      return default(bool);
    }

    protected override SyndicationFeed CreateFeedInstance()
    {
      return default(SyndicationFeed);
    }

    public override void ReadFrom(System.Xml.XmlReader reader)
    {
    }

    protected virtual new SyndicationItem ReadItem(System.Xml.XmlReader reader, SyndicationFeed feed)
    {
      return default(SyndicationItem);
    }

    protected virtual new IEnumerable<SyndicationItem> ReadItems(System.Xml.XmlReader reader, SyndicationFeed feed, out bool areAllItemsRead)
    {
      areAllItemsRead = default(bool);

      return default(IEnumerable<SyndicationItem>);
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

    protected virtual new void WriteItem(System.Xml.XmlWriter writer, SyndicationItem item, Uri feedBaseUri)
    {
      Contract.Requires(item != null);
      Contract.Requires(writer != null);
    }

    protected virtual new void WriteItems(System.Xml.XmlWriter writer, IEnumerable<SyndicationItem> items, Uri feedBaseUri)
    {
    }

    public override void WriteTo(System.Xml.XmlWriter writer)
    {
    }
    #endregion

    #region Properties and indexers
    protected Type FeedType
    {
      get
      {
        return default(Type);
      }
    }

    public bool PreserveAttributeExtensions
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool PreserveElementExtensions
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override string Version
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
