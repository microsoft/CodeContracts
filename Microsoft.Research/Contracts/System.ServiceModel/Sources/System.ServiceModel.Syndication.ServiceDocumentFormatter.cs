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

// File System.ServiceModel.Syndication.ServiceDocumentFormatter.cs
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
  abstract public partial class ServiceDocumentFormatter
  {
    #region Methods and constructors
    public abstract bool CanRead(System.Xml.XmlReader reader);

    protected static SyndicationCategory CreateCategory(InlineCategoriesDocument inlineCategories)
    {
      return default(SyndicationCategory);
    }

    protected static ResourceCollectionInfo CreateCollection(Workspace workspace)
    {
      return default(ResourceCollectionInfo);
    }

    protected virtual new ServiceDocument CreateDocumentInstance()
    {
      return default(ServiceDocument);
    }

    protected static InlineCategoriesDocument CreateInlineCategories(ResourceCollectionInfo collection)
    {
      Contract.Requires(collection != null);

      return default(InlineCategoriesDocument);
    }

    protected static ReferencedCategoriesDocument CreateReferencedCategories(ResourceCollectionInfo collection)
    {
      Contract.Requires(collection != null);

      return default(ReferencedCategoriesDocument);
    }

    protected static Workspace CreateWorkspace(ServiceDocument document)
    {
      return default(Workspace);
    }

    protected static void LoadElementExtensions(System.Xml.XmlReader reader, ResourceCollectionInfo collection, int maxExtensionSize)
    {
    }

    protected static void LoadElementExtensions(System.Xml.XmlReader reader, Workspace workspace, int maxExtensionSize)
    {
    }

    protected static void LoadElementExtensions(System.Xml.XmlReader reader, CategoriesDocument categories, int maxExtensionSize)
    {
    }

    protected static void LoadElementExtensions(System.Xml.XmlReader reader, ServiceDocument document, int maxExtensionSize)
    {
    }

    public abstract void ReadFrom(System.Xml.XmlReader reader);

    protected ServiceDocumentFormatter(ServiceDocument documentToWrite)
    {
    }

    protected ServiceDocumentFormatter()
    {
    }

    protected virtual new void SetDocument(ServiceDocument document)
    {
    }

    protected static bool TryParseAttribute(string name, string ns, string value, Workspace workspace, string version)
    {
      return default(bool);
    }

    protected static bool TryParseAttribute(string name, string ns, string value, ServiceDocument document, string version)
    {
      return default(bool);
    }

    protected static bool TryParseAttribute(string name, string ns, string value, ResourceCollectionInfo collection, string version)
    {
      return default(bool);
    }

    protected static bool TryParseAttribute(string name, string ns, string value, CategoriesDocument categories, string version)
    {
      return default(bool);
    }

    protected static bool TryParseElement(System.Xml.XmlReader reader, CategoriesDocument categories, string version)
    {
      return default(bool);
    }

    protected static bool TryParseElement(System.Xml.XmlReader reader, ResourceCollectionInfo collection, string version)
    {
      return default(bool);
    }

    protected static bool TryParseElement(System.Xml.XmlReader reader, ServiceDocument document, string version)
    {
      return default(bool);
    }

    protected static bool TryParseElement(System.Xml.XmlReader reader, Workspace workspace, string version)
    {
      return default(bool);
    }

    protected static void WriteAttributeExtensions(System.Xml.XmlWriter writer, CategoriesDocument categories, string version)
    {
    }

    protected static void WriteAttributeExtensions(System.Xml.XmlWriter writer, Workspace workspace, string version)
    {
    }

    protected static void WriteAttributeExtensions(System.Xml.XmlWriter writer, ServiceDocument document, string version)
    {
    }

    protected static void WriteAttributeExtensions(System.Xml.XmlWriter writer, ResourceCollectionInfo collection, string version)
    {
    }

    protected static void WriteElementExtensions(System.Xml.XmlWriter writer, CategoriesDocument categories, string version)
    {
    }

    protected static void WriteElementExtensions(System.Xml.XmlWriter writer, ServiceDocument document, string version)
    {
    }

    protected static void WriteElementExtensions(System.Xml.XmlWriter writer, ResourceCollectionInfo collection, string version)
    {
    }

    protected static void WriteElementExtensions(System.Xml.XmlWriter writer, Workspace workspace, string version)
    {
    }

    public abstract void WriteTo(System.Xml.XmlWriter writer);
    #endregion

    #region Properties and indexers
    public ServiceDocument Document
    {
      get
      {
        return default(ServiceDocument);
      }
    }

    public abstract string Version
    {
      get;
    }
    #endregion
  }
}
