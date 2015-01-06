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

// File System.Web.XmlSiteMapProvider.cs
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


namespace System.Web
{
  public partial class XmlSiteMapProvider : StaticSiteMapProvider, IDisposable
  {
    #region Methods and constructors
    protected internal override void AddNode(SiteMapNode node, SiteMapNode parentNode)
    {
    }

    protected virtual new void AddProvider(string providerName, SiteMapNode parentNode)
    {
    }

    public override SiteMapNode BuildSiteMap()
    {
      return default(SiteMapNode);
    }

    protected override void Clear()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public override SiteMapNode FindSiteMapNode(string rawUrl)
    {
      return default(SiteMapNode);
    }

    public override SiteMapNode FindSiteMapNodeFromKey(string key)
    {
      return default(SiteMapNode);
    }

    protected internal override SiteMapNode GetRootNodeCore()
    {
      return default(SiteMapNode);
    }

    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
    {
    }

    protected internal override void RemoveNode(SiteMapNode node)
    {
    }

    protected virtual new void RemoveProvider(string providerName)
    {
    }

    public XmlSiteMapProvider()
    {
    }
    #endregion

    #region Properties and indexers
    public override SiteMapNode RootNode
    {
      get
      {
        return default(SiteMapNode);
      }
    }
    #endregion
  }
}
