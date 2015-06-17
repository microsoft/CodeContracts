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

// File System.Web.UI.WebControls.SiteMapPath.cs
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
  public partial class SiteMapPath : CompositeControl
  {
    #region Methods and constructors
    protected internal override void CreateChildControls()
    {
    }

    protected virtual new void CreateControlHierarchy()
    {
    }

    public override void DataBind()
    {
    }

    protected virtual new void InitializeItem(SiteMapNodeItem item)
    {
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected override void OnDataBinding(EventArgs e)
    {
    }

    protected virtual new void OnItemCreated(SiteMapNodeItemEventArgs e)
    {
    }

    protected virtual new void OnItemDataBound(SiteMapNodeItemEventArgs e)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override void RenderContents(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    public SiteMapPath()
    {
    }

    protected override void TrackViewState()
    {
    }
    #endregion

    #region Properties and indexers
    public Style CurrentNodeStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new System.Web.UI.ITemplate CurrentNodeTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public Style NodeStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new System.Web.UI.ITemplate NodeTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new int ParentLevelsDisplayed
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new PathDirection PathDirection
    {
      get
      {
        return default(PathDirection);
      }
      set
      {
      }
    }

    public virtual new string PathSeparator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style PathSeparatorStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new System.Web.UI.ITemplate PathSeparatorTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public System.Web.SiteMapProvider Provider
    {
      get
      {
        return default(System.Web.SiteMapProvider);
      }
      set
      {
      }
    }

    public virtual new bool RenderCurrentNodeAsLink
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Style RootNodeStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new System.Web.UI.ITemplate RootNodeTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new bool ShowToolTips
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string SiteMapProvider
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string SkipLinkText
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
    public event SiteMapNodeItemEventHandler ItemCreated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SiteMapNodeItemEventHandler ItemDataBound
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
