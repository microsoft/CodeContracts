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

// File System.ServiceModel.Syndication.SyndicationFeed.cs
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
  public partial class SyndicationFeed : IExtensibleSyndicationObject
  {
    #region Methods and constructors
    public virtual new System.ServiceModel.Syndication.SyndicationFeed Clone(bool cloneItems)
    {
      return default(System.ServiceModel.Syndication.SyndicationFeed);
    }

    protected internal virtual new SyndicationCategory CreateCategory()
    {
      return default(SyndicationCategory);
    }

    protected internal virtual new SyndicationItem CreateItem()
    {
      return default(SyndicationItem);
    }

    protected internal virtual new SyndicationLink CreateLink()
    {
      return default(SyndicationLink);
    }

    protected internal virtual new SyndicationPerson CreatePerson()
    {
      return default(SyndicationPerson);
    }

    public Atom10FeedFormatter GetAtom10Formatter()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Syndication.Atom10FeedFormatter>() != null);

      return default(Atom10FeedFormatter);
    }

    public Rss20FeedFormatter GetRss20Formatter(bool serializeExtensionsAsAtom)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Syndication.Rss20FeedFormatter>() != null);

      return default(Rss20FeedFormatter);
    }

    public Rss20FeedFormatter GetRss20Formatter()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Syndication.Rss20FeedFormatter>() != null);

      return default(Rss20FeedFormatter);
    }

    public static TSyndicationFeed Load<TSyndicationFeed>(System.Xml.XmlReader reader)
    {
      Contract.Ensures(Contract.Result<TSyndicationFeed>() != null);

      return default(TSyndicationFeed);
    }

    public static System.ServiceModel.Syndication.SyndicationFeed Load(System.Xml.XmlReader reader)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Syndication.SyndicationFeed>() != null);

      return default(System.ServiceModel.Syndication.SyndicationFeed);
    }

    public void SaveAsAtom10(System.Xml.XmlWriter writer)
    {
    }

    public void SaveAsRss20(System.Xml.XmlWriter writer)
    {
    }

    public SyndicationFeed(string title, string description, Uri feedAlternateLink)
    {
    }

    public SyndicationFeed(IEnumerable<SyndicationItem> items)
    {
    }

    public SyndicationFeed()
    {
    }

    public SyndicationFeed(string title, string description, Uri feedAlternateLink, IEnumerable<SyndicationItem> items)
    {
    }

    protected SyndicationFeed(System.ServiceModel.Syndication.SyndicationFeed source, bool cloneItems)
    {
    }

    public SyndicationFeed(string title, string description, Uri feedAlternateLink, string id, DateTimeOffset lastUpdatedTime, IEnumerable<SyndicationItem> items)
    {
    }

    public SyndicationFeed(string title, string description, Uri feedAlternateLink, string id, DateTimeOffset lastUpdatedTime)
    {
    }

    protected internal virtual new bool TryParseAttribute(string name, string ns, string value, string version)
    {
      return default(bool);
    }

    protected internal virtual new bool TryParseElement(System.Xml.XmlReader reader, string version)
    {
      return default(bool);
    }

    protected internal virtual new void WriteAttributeExtensions(System.Xml.XmlWriter writer, string version)
    {
    }

    protected internal virtual new void WriteElementExtensions(System.Xml.XmlWriter writer, string version)
    {
    }
    #endregion

    #region Properties and indexers
    public Dictionary<System.Xml.XmlQualifiedName, string> AttributeExtensions
    {
      get
      {
        return default(Dictionary<System.Xml.XmlQualifiedName, string>);
      }
    }

    public System.Collections.ObjectModel.Collection<SyndicationPerson> Authors
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.ServiceModel.Syndication.SyndicationPerson>>() != null);

        return default(System.Collections.ObjectModel.Collection<SyndicationPerson>);
      }
    }

    public Uri BaseUri
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.Collection<SyndicationCategory> Categories
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.ServiceModel.Syndication.SyndicationCategory>>() != null);

        return default(System.Collections.ObjectModel.Collection<SyndicationCategory>);
      }
    }

    public System.Collections.ObjectModel.Collection<SyndicationPerson> Contributors
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.ServiceModel.Syndication.SyndicationPerson>>() != null);

        return default(System.Collections.ObjectModel.Collection<SyndicationPerson>);
      }
    }

    public TextSyndicationContent Copyright
    {
      get
      {
        return default(TextSyndicationContent);
      }
      set
      {
      }
    }

    public TextSyndicationContent Description
    {
      get
      {
        return default(TextSyndicationContent);
      }
      set
      {
      }
    }

    public SyndicationElementExtensionCollection ElementExtensions
    {
      get
      {
        return default(SyndicationElementExtensionCollection);
      }
    }

    public string Generator
    {
      get
      {
        return default(string);
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
      set
      {
      }
    }

    public Uri ImageUrl
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public IEnumerable<SyndicationItem> Items
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Generic.IEnumerable<System.ServiceModel.Syndication.SyndicationItem>>() != null);

        return default(IEnumerable<SyndicationItem>);
      }
      set
      {
      }
    }

    public string Language
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public DateTimeOffset LastUpdatedTime
    {
      get
      {
        return default(DateTimeOffset);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.Collection<SyndicationLink> Links
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.ServiceModel.Syndication.SyndicationLink>>() != null);

        return default(System.Collections.ObjectModel.Collection<SyndicationLink>);
      }
    }

    public TextSyndicationContent Title
    {
      get
      {
        return default(TextSyndicationContent);
      }
      set
      {
      }
    }
    #endregion
  }
}
