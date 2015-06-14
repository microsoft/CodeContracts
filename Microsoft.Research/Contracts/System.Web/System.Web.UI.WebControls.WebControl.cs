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

// File System.Web.UI.WebControls.WebControl.cs
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


namespace System.Web.UI.WebControls
{
  public partial class WebControl : System.Web.UI.Control, System.Web.UI.IAttributeAccessor
  {
    #region Methods and constructors
    protected virtual new void AddAttributesToRender (System.Web.UI.HtmlTextWriter writer)
    {
      Contract.Requires (writer != null);
    }

    public void ApplyStyle (Style s)
    {
    }

    public void CopyBaseAttributes (System.Web.UI.WebControls.WebControl controlSrc)
    {
    }

    protected virtual new Style CreateControlStyle ()
    {
      return default(Style);
    }

    protected override void LoadViewState (Object savedState)
    {
    }

    public void MergeStyle (Style s)
    {
    }

    protected internal override void Render (System.Web.UI.HtmlTextWriter writer)
    {
    }

    public virtual new void RenderBeginTag (System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal virtual new void RenderContents (System.Web.UI.HtmlTextWriter writer)
    {
    }

    public virtual new void RenderEndTag (System.Web.UI.HtmlTextWriter writer)
    {
      Contract.Requires (writer != null);
    }

    protected override Object SaveViewState ()
    {
      return default(Object);
    }

    string System.Web.UI.IAttributeAccessor.GetAttribute (string name)
    {
      return default(string);
    }

    void System.Web.UI.IAttributeAccessor.SetAttribute (string name, string value)
    {
    }

    protected override void TrackViewState ()
    {
    }

    public WebControl (System.Web.UI.HtmlTextWriterTag tag)
    {
    }

    protected WebControl (string tag)
    {
    }

    protected WebControl ()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new string AccessKey
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.AttributeCollection Attributes
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.AttributeCollection>() != null);

        return default(System.Web.UI.AttributeCollection);
      }
    }

    public virtual new System.Drawing.Color BackColor
    {
      get
      {
        return default(System.Drawing.Color);
      }
      set
      {
      }
    }

    public virtual new System.Drawing.Color BorderColor
    {
      get
      {
        return default(System.Drawing.Color);
      }
      set
      {
      }
    }

    public virtual new BorderStyle BorderStyle
    {
      get
      {
        return default(BorderStyle);
      }
      set
      {
      }
    }

    public virtual new Unit BorderWidth
    {
      get
      {
        return default(Unit);
      }
      set
      {
      }
    }

    public Style ControlStyle
    {
      get
      {
        return default(Style);
      }
    }

    public bool ControlStyleCreated
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string CssClass
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

#if NETFRAMEWORK_4_0
    public static string DisabledCssClass
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
#endif

    public virtual new bool Enabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool EnableTheming
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new FontInfo Font
    {
      get
      {

        return default(FontInfo);
      }
    }

    public virtual new System.Drawing.Color ForeColor
    {
      get
      {
        return default(System.Drawing.Color);
      }
      set
      {
      }
    }

    public bool HasAttributes
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new Unit Height
    {
      get
      {
        return default(Unit);
      }
      set
      {
      }
    }

    internal protected bool IsEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public override string SkinID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.CssStyleCollection Style
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.CssStyleCollection>() == this.Attributes.CssStyle);

        return default(System.Web.UI.CssStyleCollection);
      }
    }

#if NETFRAMEWORK_4_0
    public virtual new bool SupportsDisabledAttribute
    {
      get
      {
        return default(bool);
      }
    }
#endif

    public virtual new short TabIndex
    {
      get
      {
        return default(short);
      }
      set
      {
      }
    }

    protected virtual new System.Web.UI.HtmlTextWriterTag TagKey
    {
      get
      {
        return default(System.Web.UI.HtmlTextWriterTag);
      }
    }

    protected virtual new string TagName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string ToolTip
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new Unit Width
    {
      get
      {
        return default(Unit);
      }
      set
      {
      }
    }
    #endregion
  }
}
