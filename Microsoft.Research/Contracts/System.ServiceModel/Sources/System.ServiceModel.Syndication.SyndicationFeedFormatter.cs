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

// File System.ServiceModel.Syndication.SyndicationFeedFormatter.cs
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
  abstract public partial class SyndicationFeedFormatter
  {
    #region Methods and constructors
    public abstract bool CanRead(System.Xml.XmlReader reader);

    protected internal static SyndicationCategory CreateCategory(SyndicationItem item)
    {
      return default(SyndicationCategory);
    }

    protected internal static SyndicationCategory CreateCategory(SyndicationFeed feed)
    {
      return default(SyndicationCategory);
    }

    protected abstract SyndicationFeed CreateFeedInstance();

    protected internal static SyndicationItem CreateItem(SyndicationFeed feed)
    {
      return default(SyndicationItem);
    }

    protected internal static SyndicationLink CreateLink(SyndicationItem item)
    {
      return default(SyndicationLink);
    }

    protected internal static SyndicationLink CreateLink(SyndicationFeed feed)
    {
      return default(SyndicationLink);
    }

    protected internal static SyndicationPerson CreatePerson(SyndicationItem item)
    {
      return default(SyndicationPerson);
    }

    protected internal static SyndicationPerson CreatePerson(SyndicationFeed feed)
    {
      return default(SyndicationPerson);
    }

    protected internal static void LoadElementExtensions(System.Xml.XmlReader reader, SyndicationItem item, int maxExtensionSize)
    {
    }

    protected internal static void LoadElementExtensions(System.Xml.XmlReader reader, SyndicationCategory category, int maxExtensionSize)
    {
    }

    protected internal static void LoadElementExtensions(System.Xml.XmlReader reader, SyndicationLink link, int maxExtensionSize)
    {
    }

    protected internal static void LoadElementExtensions(System.Xml.XmlReader reader, SyndicationFeed feed, int maxExtensionSize)
    {
    }

    protected internal static void LoadElementExtensions(System.Xml.XmlReader reader, SyndicationPerson person, int maxExtensionSize)
    {
    }

    public abstract void ReadFrom(System.Xml.XmlReader reader);

    protected internal virtual new void SetFeed(SyndicationFeed feed)
    {
    }

    protected SyndicationFeedFormatter(SyndicationFeed feedToWrite)
    {
    }

    protected SyndicationFeedFormatter()
    {
    }

    protected internal static bool TryParseAttribute(string name, string ns, string value, SyndicationPerson person, string version)
    {
      return default(bool);
    }

    protected internal static bool TryParseAttribute(string name, string ns, string value, SyndicationLink link, string version)
    {
      return default(bool);
    }

    protected internal static bool TryParseAttribute(string name, string ns, string value, SyndicationItem item, string version)
    {
      return default(bool);
    }

    protected internal static bool TryParseAttribute(string name, string ns, string value, SyndicationFeed feed, string version)
    {
      return default(bool);
    }

    protected internal static bool TryParseAttribute(string name, string ns, string value, SyndicationCategory category, string version)
    {
      return default(bool);
    }

    protected internal static bool TryParseContent(System.Xml.XmlReader reader, SyndicationItem item, string contentType, string version, out SyndicationContent content)
    {
      Contract.Requires(item != null);

      content = default(SyndicationContent);

      return default(bool);
    }

    protected internal static bool TryParseElement(System.Xml.XmlReader reader, SyndicationPerson person, string version)
    {
      return default(bool);
    }

    protected internal static bool TryParseElement(System.Xml.XmlReader reader, SyndicationLink link, string version)
    {
      return default(bool);
    }

    protected internal static bool TryParseElement(System.Xml.XmlReader reader, SyndicationFeed feed, string version)
    {
      return default(bool);
    }

    protected internal static bool TryParseElement(System.Xml.XmlReader reader, SyndicationItem item, string version)
    {
      return default(bool);
    }

    protected internal static bool TryParseElement(System.Xml.XmlReader reader, SyndicationCategory category, string version)
    {
      return default(bool);
    }

    protected internal static void WriteAttributeExtensions(System.Xml.XmlWriter writer, SyndicationLink link, string version)
    {
    }

    protected internal static void WriteAttributeExtensions(System.Xml.XmlWriter writer, SyndicationItem item, string version)
    {
    }

    protected internal static void WriteAttributeExtensions(System.Xml.XmlWriter writer, SyndicationFeed feed, string version)
    {
    }

    protected internal static void WriteAttributeExtensions(System.Xml.XmlWriter writer, SyndicationPerson person, string version)
    {
    }

    protected internal static void WriteAttributeExtensions(System.Xml.XmlWriter writer, SyndicationCategory category, string version)
    {
    }

    protected internal static void WriteElementExtensions(System.Xml.XmlWriter writer, SyndicationLink link, string version)
    {
    }

    protected internal static void WriteElementExtensions(System.Xml.XmlWriter writer, SyndicationPerson person, string version)
    {
    }

    protected internal static void WriteElementExtensions(System.Xml.XmlWriter writer, SyndicationItem item, string version)
    {
    }

    protected internal static void WriteElementExtensions(System.Xml.XmlWriter writer, SyndicationCategory category, string version)
    {
    }

    protected internal static void WriteElementExtensions(System.Xml.XmlWriter writer, SyndicationFeed feed, string version)
    {
    }

    public abstract void WriteTo(System.Xml.XmlWriter writer);
    #endregion

    #region Properties and indexers
    public SyndicationFeed Feed
    {
      get
      {
        return default(SyndicationFeed);
      }
    }

    public abstract string Version
    {
      get;
    }
    #endregion
  }
}
