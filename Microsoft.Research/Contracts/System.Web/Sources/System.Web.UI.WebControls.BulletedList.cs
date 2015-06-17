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

// File System.Web.UI.WebControls.BulletedList.cs
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
  public partial class BulletedList : ListControl, System.Web.UI.IPostBackEventHandler
  {
    #region Methods and constructors
    protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
    {
    }

    public BulletedList()
    {
    }

    protected virtual new void OnClick(BulletedListEventArgs e)
    {
    }

    protected virtual new void RaisePostBackEvent(string eventArgument)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RenderBulletText(ListItem item, int index, System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override void RenderContents(System.Web.UI.HtmlTextWriter writer)
    {
    }

    void System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
    {
    }
    #endregion

    #region Properties and indexers
    public override bool AutoPostBack
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string BulletImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new BulletStyle BulletStyle
    {
      get
      {
        return default(BulletStyle);
      }
      set
      {
      }
    }

    public override System.Web.UI.ControlCollection Controls
    {
      get
      {
        return default(System.Web.UI.ControlCollection);
      }
    }

    public virtual new BulletedListDisplayMode DisplayMode
    {
      get
      {
        return default(BulletedListDisplayMode);
      }
      set
      {
      }
    }

    public virtual new int FirstBulletNumber
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public override int SelectedIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public override ListItem SelectedItem
    {
      get
      {
        return default(ListItem);
      }
    }

    public override string SelectedValue
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

    public virtual new string Target
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string Text
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
    public event BulletedListEventHandler Click
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
