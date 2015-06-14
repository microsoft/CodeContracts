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

// File System.Web.SiteMapNode.cs
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
  public partial class SiteMapNode : ICloneable, System.Web.UI.IHierarchyData, System.Web.UI.INavigateUIData
  {
    #region Methods and constructors
    public virtual new System.Web.SiteMapNode Clone()
    {
      return default(System.Web.SiteMapNode);
    }

    public virtual new System.Web.SiteMapNode Clone(bool cloneParentNodes)
    {
      return default(System.Web.SiteMapNode);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public SiteMapNodeCollection GetAllNodes()
    {
      return default(SiteMapNodeCollection);
    }

    public System.Web.UI.WebControls.SiteMapDataSourceView GetDataSourceView(System.Web.UI.WebControls.SiteMapDataSource owner, string viewName)
    {
      return default(System.Web.UI.WebControls.SiteMapDataSourceView);
    }

    protected string GetExplicitResourceString(string attributeName, string defaultValue, bool throwIfNotFound)
    {
      return default(string);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView GetHierarchicalDataSourceView()
    {
      return default(System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView);
    }

    protected string GetImplicitResourceString(string attributeName)
    {
      return default(string);
    }

    public virtual new bool IsAccessibleToUser(HttpContext context)
    {
      return default(bool);
    }

    public virtual new bool IsDescendantOf(System.Web.SiteMapNode node)
    {
      return default(bool);
    }

    public SiteMapNode(SiteMapProvider provider, string key, string url, string title, string description)
    {
    }

    public SiteMapNode(SiteMapProvider provider, string key, string url, string title, string description, System.Collections.IList roles, System.Collections.Specialized.NameValueCollection attributes, System.Collections.Specialized.NameValueCollection explicitResourceKeys, string implicitResourceKey)
    {
    }

    public SiteMapNode(SiteMapProvider provider, string key, string url)
    {
    }

    public SiteMapNode(SiteMapProvider provider, string key, string url, string title)
    {
    }

    public SiteMapNode(SiteMapProvider provider, string key)
    {
    }

    Object System.ICloneable.Clone()
    {
      return default(Object);
    }

    System.Web.UI.IHierarchicalEnumerable System.Web.UI.IHierarchyData.GetChildren()
    {
      return default(System.Web.UI.IHierarchicalEnumerable);
    }

    System.Web.UI.IHierarchyData System.Web.UI.IHierarchyData.GetParent()
    {
      return default(System.Web.UI.IHierarchyData);
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    protected System.Collections.Specialized.NameValueCollection Attributes
    {
      get
      {
        return default(System.Collections.Specialized.NameValueCollection);
      }
      set
      {
      }
    }

    public virtual new SiteMapNodeCollection ChildNodes
    {
      get
      {
        return default(SiteMapNodeCollection);
      }
      set
      {
      }
    }

    public virtual new string Description
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool HasChildNodes
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string this [string key]
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Key
    {
      get
      {
        return default(string);
      }
    }

    public virtual new System.Web.SiteMapNode NextSibling
    {
      get
      {
        return default(System.Web.SiteMapNode);
      }
    }

    public virtual new System.Web.SiteMapNode ParentNode
    {
      get
      {
        return default(System.Web.SiteMapNode);
      }
      set
      {
      }
    }

    public virtual new System.Web.SiteMapNode PreviousSibling
    {
      get
      {
        return default(System.Web.SiteMapNode);
      }
    }

    public SiteMapProvider Provider
    {
      get
      {
        return default(SiteMapProvider);
      }
    }

    public bool ReadOnly
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string ResourceKey
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Collections.IList Roles
    {
      get
      {
        return default(System.Collections.IList);
      }
      set
      {
      }
    }

    public virtual new System.Web.SiteMapNode RootNode
    {
      get
      {
        return default(System.Web.SiteMapNode);
      }
    }

    bool System.Web.UI.IHierarchyData.HasChildren
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Web.UI.IHierarchyData.Item
    {
      get
      {
        return default(Object);
      }
    }

    string System.Web.UI.IHierarchyData.Path
    {
      get
      {
        return default(string);
      }
    }

    string System.Web.UI.IHierarchyData.Type
    {
      get
      {
        return default(string);
      }
    }

    string System.Web.UI.INavigateUIData.Description
    {
      get
      {
        return default(string);
      }
    }

    string System.Web.UI.INavigateUIData.Name
    {
      get
      {
        return default(string);
      }
    }

    string System.Web.UI.INavigateUIData.NavigateUrl
    {
      get
      {
        return default(string);
      }
    }

    string System.Web.UI.INavigateUIData.Value
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string Title
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string Url
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
