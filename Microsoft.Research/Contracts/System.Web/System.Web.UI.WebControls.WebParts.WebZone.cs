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

// File System.Web.UI.WebControls.WebParts.WebZone.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
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
  abstract public partial class WebZone : System.Web.UI.WebControls.CompositeControl
  {
    #region Methods and constructors
    public virtual new PartChromeType GetEffectiveChromeType (Part part)
    {
      return default(PartChromeType);
    }

    protected override void LoadViewState (Object savedState)
    {
    }

    protected internal override void OnInit (EventArgs e)
    {
    }

    protected internal override void OnPreRender (EventArgs e)
    {
    }

    public override void RenderBeginTag (System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RenderBody (System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override void RenderContents (System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RenderFooter (System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RenderHeader (System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override Object SaveViewState ()
    {
      return default(Object);
    }

    protected override void TrackViewState ()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new string BackImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string EmptyZoneText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.Style EmptyZoneTextStyle
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.WebControls.Style>() != null);

        return default(System.Web.UI.WebControls.Style);
      }
    }

    public System.Web.UI.WebControls.Style ErrorStyle
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.WebControls.Style>() != null);

        return default(System.Web.UI.WebControls.Style);
      }
    }

    public TitleStyle FooterStyle
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.WebControls.WebParts.TitleStyle>() != null);

        return default(TitleStyle);
      }
    }

    protected virtual new bool HasFooter
    {
      get
      {
        return default(bool);
      }
    }

    protected virtual new bool HasHeader
    {
      get
      {
        return default(bool);
      }
    }

    public TitleStyle HeaderStyle
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.WebControls.WebParts.TitleStyle>() != null);

        return default(TitleStyle);
      }
    }

    public virtual new string HeaderText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new int Padding
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.Unit PartChromePadding
    {
      get
      {
        return default(System.Web.UI.WebControls.Unit);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.Style PartChromeStyle
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.WebControls.Style>() != null);

        return default(System.Web.UI.WebControls.Style);
      }
    }

    public virtual new PartChromeType PartChromeType
    {
      get
      {
        return default(PartChromeType);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.TableStyle PartStyle
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.WebControls.TableStyle>() != null);

        return default(System.Web.UI.WebControls.TableStyle);
      }
    }

    public TitleStyle PartTitleStyle
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.WebControls.WebParts.TitleStyle>() != null);

        return default(TitleStyle);
      }
    }

    internal protected bool RenderClientScript
    {
      get
      {
        return default(bool);
      }
    }

    protected override System.Web.UI.HtmlTextWriterTag TagKey
    {
      get
      {
        return default(System.Web.UI.HtmlTextWriterTag);
      }
    }

    public virtual new System.Web.UI.WebControls.ButtonType VerbButtonType
    {
      get
      {
        return default(System.Web.UI.WebControls.ButtonType);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.Style VerbStyle
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.WebControls.Style>() != null);

        return default(System.Web.UI.WebControls.Style);
      }
    }

    protected WebPartManager WebPartManager
    {
      get
      {
        return default(WebPartManager);
      }
    }
    #endregion
  }
}
