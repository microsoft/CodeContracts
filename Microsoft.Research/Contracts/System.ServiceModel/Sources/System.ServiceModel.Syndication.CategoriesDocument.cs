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

// File System.ServiceModel.Syndication.CategoriesDocument.cs
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
  abstract public partial class CategoriesDocument : IExtensibleSyndicationObject
  {
    #region Methods and constructors
    internal CategoriesDocument()
    {
    }

    public static InlineCategoriesDocument Create(System.Collections.ObjectModel.Collection<SyndicationCategory> categories)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Syndication.InlineCategoriesDocument>() != null);

      return default(InlineCategoriesDocument);
    }

    public static InlineCategoriesDocument Create(System.Collections.ObjectModel.Collection<SyndicationCategory> categories, bool isFixed, string scheme)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Syndication.InlineCategoriesDocument>() != null);

      return default(InlineCategoriesDocument);
    }

    public static ReferencedCategoriesDocument Create(Uri linkToCategoriesDocument)
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Syndication.ReferencedCategoriesDocument>() != null);

      return default(ReferencedCategoriesDocument);
    }

    public CategoriesDocumentFormatter GetFormatter()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Syndication.CategoriesDocumentFormatter>() != null);

      return default(CategoriesDocumentFormatter);
    }

    public static CategoriesDocument Load(System.Xml.XmlReader reader)
    {
      return default(CategoriesDocument);
    }

    public void Save(System.Xml.XmlWriter writer)
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

    public SyndicationElementExtensionCollection ElementExtensions
    {
      get
      {
        return default(SyndicationElementExtensionCollection);
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
    #endregion
  }
}
