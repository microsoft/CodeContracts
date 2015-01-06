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

// File System.Web.UI.WebControls.ImageButton.cs
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
  public partial class ImageButton : Image, System.Web.UI.IPostBackDataHandler, System.Web.UI.IPostBackEventHandler, IButtonControl
  {
    #region Methods and constructors
    protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new System.Web.UI.PostBackOptions GetPostBackOptions()
    {
      return default(System.Web.UI.PostBackOptions);
    }

    public ImageButton()
    {
    }

    protected virtual new bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
    {
      return default(bool);
    }

    protected virtual new void OnClick(System.Web.UI.ImageClickEventArgs e)
    {
    }

    protected virtual new void OnCommand(CommandEventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected virtual new void RaisePostBackEvent(string eventArgument)
    {
    }

    protected virtual new void RaisePostDataChangedEvent()
    {
    }

    bool System.Web.UI.IPostBackDataHandler.LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
    {
      return default(bool);
    }

    void System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent()
    {
    }

    void System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool CausesValidation
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string CommandArgument
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string CommandName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override bool Enabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool GenerateEmptyAlternateText
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string OnClientClick
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string PostBackUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override bool SupportsDisabledAttribute
    {
      get
      {
        return default(bool);
      }
    }

    string System.Web.UI.WebControls.IButtonControl.Text
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected override System.Web.UI.HtmlTextWriterTag TagKey
    {
      get
      {
        return default(System.Web.UI.HtmlTextWriterTag);
      }
    }

    protected virtual new string Text
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string ValidationGroup
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
    public event System.Web.UI.ImageClickEventHandler Click
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Click
    {
      add
      {
      }
      remove
      {
      }
    }

    public event CommandEventHandler Command
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
