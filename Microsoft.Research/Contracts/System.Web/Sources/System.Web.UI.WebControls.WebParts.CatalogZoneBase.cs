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

// File System.Web.UI.WebControls.WebParts.CatalogZoneBase.cs
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


namespace System.Web.UI.WebControls.WebParts
{
  abstract public partial class CatalogZoneBase : ToolZone, System.Web.UI.IPostBackDataHandler
  {
    #region Methods and constructors
    protected CatalogZoneBase() : base (default(System.Collections.ICollection))
    {
    }

    protected override void Close()
    {
    }

    protected virtual new CatalogPartChrome CreateCatalogPartChrome()
    {
      return default(CatalogPartChrome);
    }

    protected abstract CatalogPartCollection CreateCatalogParts();

    protected internal override void CreateChildControls()
    {
    }

    protected void InvalidateCatalogParts()
    {
    }

    protected internal override void LoadControlState(Object savedState)
    {
    }

    protected virtual new bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
    {
      return default(bool);
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected override void RaisePostBackEvent(string eventArgument)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override void RenderBody(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RenderCatalogPartLinks(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override void RenderFooter(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override void RenderVerbs(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override Object SaveControlState()
    {
      return default(Object);
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    bool System.Web.UI.IPostBackDataHandler.LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
    {
      return default(bool);
    }

    void System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent()
    {
    }

    protected override void TrackViewState()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new WebPartVerb AddVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    public CatalogPartChrome CatalogPartChrome
    {
      get
      {
        return default(CatalogPartChrome);
      }
    }

    public CatalogPartCollection CatalogParts
    {
      get
      {
        return default(CatalogPartCollection);
      }
    }

    public virtual new WebPartVerb CloseVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    public override string EmptyZoneText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string HeaderText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string InstructionText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.Style PartLinkStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    public string SelectedCatalogPartID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.Style SelectedPartLinkStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    public virtual new string SelectTargetZoneText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool ShowCatalogIcons
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}
