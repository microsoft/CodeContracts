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

// File System.Web.UI.WebControls.WebParts.WebPartZoneBase.cs
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
  abstract public partial class WebPartZoneBase : WebZone, System.Web.UI.IPostBackEventHandler, IWebPartMenuUser
  {
    #region Methods and constructors
    protected virtual new void CloseWebPart(WebPart webPart)
    {
    }

    protected virtual new void ConnectWebPart(WebPart webPart)
    {
    }

    protected internal override void CreateChildControls()
    {
    }

    protected override System.Web.UI.ControlCollection CreateControlCollection()
    {
      return default(System.Web.UI.ControlCollection);
    }

    protected override System.Web.UI.WebControls.Style CreateControlStyle()
    {
      return default(System.Web.UI.WebControls.Style);
    }

    protected virtual new WebPartChrome CreateWebPartChrome()
    {
      return default(WebPartChrome);
    }

    protected virtual new void DeleteWebPart(WebPart webPart)
    {
    }

    protected virtual new void EditWebPart(WebPart webPart)
    {
    }

    public override PartChromeType GetEffectiveChromeType(Part part)
    {
      return default(PartChromeType);
    }

    protected internal abstract WebPartCollection GetInitialWebParts();

    protected override void LoadViewState(Object savedState)
    {
    }

    protected virtual new void MinimizeWebPart(WebPart webPart)
    {
    }

    protected virtual new void OnCreateVerbs(WebPartVerbsEventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected virtual new void RaisePostBackEvent(string eventArgument)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override void RenderBody(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RenderDropCue(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override void RenderHeader(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RestoreWebPart(WebPart webPart)
    {
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    void System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
    {
    }

    void System.Web.UI.WebControls.WebParts.IWebPartMenuUser.OnBeginRender(System.Web.UI.HtmlTextWriter writer)
    {
    }

    void System.Web.UI.WebControls.WebParts.IWebPartMenuUser.OnEndRender(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override void TrackViewState()
    {
    }

    protected WebPartZoneBase()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool AllowLayoutChange
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override System.Drawing.Color BorderColor
    {
      get
      {
        return default(System.Drawing.Color);
      }
      set
      {
      }
    }

    public override System.Web.UI.WebControls.BorderStyle BorderStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.BorderStyle);
      }
      set
      {
      }
    }

    public override System.Web.UI.WebControls.Unit BorderWidth
    {
      get
      {
        return default(System.Web.UI.WebControls.Unit);
      }
      set
      {
      }
    }

    public virtual new WebPartVerb CloseVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    public virtual new WebPartVerb ConnectVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    public virtual new WebPartVerb DeleteVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    public virtual new string DisplayTitle
    {
      get
      {
        return default(string);
      }
    }

    internal protected bool DragDropEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new System.Drawing.Color DragHighlightColor
    {
      get
      {
        return default(System.Drawing.Color);
      }
      set
      {
      }
    }

    public virtual new WebPartVerb EditVerb
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

    public virtual new WebPartVerb ExportVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    protected override bool HasFooter
    {
      get
      {
        return default(bool);
      }
    }

    protected override bool HasHeader
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new WebPartVerb HelpVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    public virtual new System.Web.UI.WebControls.Orientation LayoutOrientation
    {
      get
      {
        return default(System.Web.UI.WebControls.Orientation);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.Style MenuCheckImageStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    public virtual new string MenuCheckImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.Style MenuLabelHoverStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    public System.Web.UI.WebControls.Style MenuLabelStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    public virtual new string MenuLabelText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string MenuPopupImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public WebPartMenuStyle MenuPopupStyle
    {
      get
      {
        return default(WebPartMenuStyle);
      }
    }

    public System.Web.UI.WebControls.Style MenuVerbHoverStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    public System.Web.UI.WebControls.Style MenuVerbStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    public virtual new WebPartVerb MinimizeVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    public virtual new WebPartVerb RestoreVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    public System.Web.UI.WebControls.Style SelectedPartChromeStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    public virtual new bool ShowTitleIcons
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    System.Web.UI.WebControls.Style System.Web.UI.WebControls.WebParts.IWebPartMenuUser.CheckImageStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    string System.Web.UI.WebControls.WebParts.IWebPartMenuUser.CheckImageUrl
    {
      get
      {
        return default(string);
      }
    }

    string System.Web.UI.WebControls.WebParts.IWebPartMenuUser.ClientID
    {
      get
      {
        return default(string);
      }
    }

    System.Web.UI.WebControls.Style System.Web.UI.WebControls.WebParts.IWebPartMenuUser.ItemHoverStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    System.Web.UI.WebControls.Style System.Web.UI.WebControls.WebParts.IWebPartMenuUser.ItemStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    System.Web.UI.WebControls.Style System.Web.UI.WebControls.WebParts.IWebPartMenuUser.LabelHoverStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    string System.Web.UI.WebControls.WebParts.IWebPartMenuUser.LabelImageUrl
    {
      get
      {
        return default(string);
      }
    }

    System.Web.UI.WebControls.Style System.Web.UI.WebControls.WebParts.IWebPartMenuUser.LabelStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    string System.Web.UI.WebControls.WebParts.IWebPartMenuUser.LabelText
    {
      get
      {
        return default(string);
      }
    }

    WebPartMenuStyle System.Web.UI.WebControls.WebParts.IWebPartMenuUser.MenuPopupStyle
    {
      get
      {
        return default(WebPartMenuStyle);
      }
    }

    System.Web.UI.Page System.Web.UI.WebControls.WebParts.IWebPartMenuUser.Page
    {
      get
      {
        return default(System.Web.UI.Page);
      }
    }

    string System.Web.UI.WebControls.WebParts.IWebPartMenuUser.PopupImageUrl
    {
      get
      {
        return default(string);
      }
    }

    string System.Web.UI.WebControls.WebParts.IWebPartMenuUser.PostBackTarget
    {
      get
      {
        return default(string);
      }
    }

    System.Web.UI.IUrlResolutionService System.Web.UI.WebControls.WebParts.IWebPartMenuUser.UrlResolver
    {
      get
      {
        return default(System.Web.UI.IUrlResolutionService);
      }
    }

    public virtual new System.Web.UI.WebControls.ButtonType TitleBarVerbButtonType
    {
      get
      {
        return default(System.Web.UI.WebControls.ButtonType);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.Style TitleBarVerbStyle
    {
      get
      {
        return default(System.Web.UI.WebControls.Style);
      }
    }

    public override System.Web.UI.WebControls.ButtonType VerbButtonType
    {
      get
      {
        return default(System.Web.UI.WebControls.ButtonType);
      }
      set
      {
      }
    }

    public WebPartChrome WebPartChrome
    {
      get
      {
        return default(WebPartChrome);
      }
    }

    public WebPartCollection WebParts
    {
      get
      {
        return default(WebPartCollection);
      }
    }

    public virtual new WebPartVerbRenderMode WebPartVerbRenderMode
    {
      get
      {
        return default(WebPartVerbRenderMode);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event WebPartVerbsEventHandler CreateVerbs
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
