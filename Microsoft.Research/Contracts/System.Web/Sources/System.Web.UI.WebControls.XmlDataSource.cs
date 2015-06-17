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

// File System.Web.UI.WebControls.XmlDataSource.cs
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


namespace System.Web.UI.WebControls
{
  public partial class XmlDataSource : System.Web.UI.HierarchicalDataSourceControl, System.Web.UI.IDataSource, System.ComponentModel.IListSource
  {
    #region Methods and constructors
    protected override System.Web.UI.HierarchicalDataSourceView GetHierarchicalView(string viewPath)
    {
      return default(System.Web.UI.HierarchicalDataSourceView);
    }

    public System.Xml.XmlDocument GetXmlDocument()
    {
      return default(System.Xml.XmlDocument);
    }

    protected virtual new void OnTransforming(EventArgs e)
    {
    }

    public void Save()
    {
    }

    System.Collections.IList System.ComponentModel.IListSource.GetList()
    {
      return default(System.Collections.IList);
    }

    System.Web.UI.DataSourceView System.Web.UI.IDataSource.GetView(string viewName)
    {
      return default(System.Web.UI.DataSourceView);
    }

    System.Collections.ICollection System.Web.UI.IDataSource.GetViewNames()
    {
      return default(System.Collections.ICollection);
    }

    public XmlDataSource()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new int CacheDuration
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.DataSourceCacheExpiry CacheExpirationPolicy
    {
      get
      {
        return default(System.Web.UI.DataSourceCacheExpiry);
      }
      set
      {
      }
    }

    public virtual new string CacheKeyContext
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string CacheKeyDependency
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string Data
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string DataFile
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool EnableCaching
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    bool System.ComponentModel.IListSource.ContainsListCollection
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string Transform
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new System.Xml.Xsl.XsltArgumentList TransformArgumentList
    {
      get
      {
        return default(System.Xml.Xsl.XsltArgumentList);
      }
      set
      {
      }
    }

    public virtual new string TransformFile
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string XPath
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

    #region Events
    public event EventHandler DataSourceChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Transforming
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
