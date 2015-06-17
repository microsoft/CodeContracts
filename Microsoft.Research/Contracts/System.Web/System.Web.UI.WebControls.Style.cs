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

// File System.Web.UI.WebControls.Style.cs
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
  public partial class Style : System.ComponentModel.Component, System.Web.UI.IStateManager
  {
    #region Methods and constructors
    public virtual new void AddAttributesToRender (System.Web.UI.HtmlTextWriter writer, WebControl owner)
    {
      Contract.Requires (writer != null);
    }

    public void AddAttributesToRender (System.Web.UI.HtmlTextWriter writer)
    {
    }

    public virtual new void CopyFrom (System.Web.UI.WebControls.Style s)
    {
    }

    protected virtual new void FillStyleAttributes (System.Web.UI.CssStyleCollection attributes, System.Web.UI.IUrlResolutionService urlResolver)
    {
      Contract.Requires (attributes != null);
    }

    public System.Web.UI.CssStyleCollection GetStyleAttributes (System.Web.UI.IUrlResolutionService urlResolver)
    {
      Contract.Ensures (Contract.Result<System.Web.UI.CssStyleCollection>() != null);

      return default(System.Web.UI.CssStyleCollection);
    }

    protected internal void LoadViewState (Object state)
    {
    }

    public virtual new void MergeWith (System.Web.UI.WebControls.Style s)
    {
    }

    public virtual new void Reset ()
    {
    }

    protected internal virtual new Object SaveViewState ()
    {
      return default(Object);
    }

    protected internal virtual new void SetBit (int bit)
    {
    }

    public void SetDirty ()
    {
    }

    public Style ()
    {
    }

    public Style (System.Web.UI.StateBag bag)
    {
    }

    void System.Web.UI.IStateManager.LoadViewState (Object state)
    {
    }

    Object System.Web.UI.IStateManager.SaveViewState ()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.TrackViewState ()
    {
    }

    protected internal virtual new void TrackViewState ()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Drawing.Color BackColor
    {
      get
      {
        return default(System.Drawing.Color);
      }
      set
      {
      }
    }

    public System.Drawing.Color BorderColor
    {
      get
      {
        return default(System.Drawing.Color);
      }
      set
      {
      }
    }

    public BorderStyle BorderStyle
    {
      get
      {
        return default(BorderStyle);
      }
      set
      {
      }
    }

    public Unit BorderWidth
    {
      get
      {
        return default(Unit);
      }
      set
      {
      }
    }

    public string CssClass
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public FontInfo Font
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.WebControls.FontInfo>() != null);

        return default(FontInfo);
      }
    }

    public System.Drawing.Color ForeColor
    {
      get
      {
        return default(System.Drawing.Color);
      }
      set
      {
      }
    }

    public Unit Height
    {
      get
      {
        return default(Unit);
      }
      set
      {
      }
    }

    public virtual new bool IsEmpty
    {
      get
      {

        return default(bool);
      }
    }

    protected bool IsTrackingViewState
    {
      get
      {
        return default(bool);
      }
    }

    public string RegisteredCssClass
    {
      get
      {
        return default(string);
      }
    }

    bool System.Web.UI.IStateManager.IsTrackingViewState
    {
      get
      {
        return default(bool);
      }
    }

    internal protected System.Web.UI.StateBag ViewState
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.StateBag>() != null);

        return default(System.Web.UI.StateBag);
      }
    }

    public Unit Width
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
