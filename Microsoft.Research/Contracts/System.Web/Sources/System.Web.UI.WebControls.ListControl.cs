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

// File System.Web.UI.WebControls.ListControl.cs
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
  abstract public partial class ListControl : DataBoundControl, System.Web.UI.IEditableTextControl, System.Web.UI.ITextControl
  {
    #region Methods and constructors
    protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
    {
    }

    public virtual new void ClearSelection()
    {
    }

    public ListControl()
    {
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected override void OnDataBinding(EventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected virtual new void OnSelectedIndexChanged(EventArgs e)
    {
    }

    protected virtual new void OnTextChanged(EventArgs e)
    {
    }

    protected internal override void PerformDataBinding(System.Collections.IEnumerable dataSource)
    {
    }

    protected override void PerformSelect()
    {
    }

    protected internal override void RenderContents(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    protected void SetPostDataSelection(int selectedIndex)
    {
    }

    protected override void TrackViewState()
    {
    }

    protected internal virtual new void VerifyMultiSelect()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool AppendDataBoundItems
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool AutoPostBack
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

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

    public virtual new string DataTextField
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string DataTextFormatString
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string DataValueField
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ListItemCollection Items
    {
      get
      {
        return default(ListItemCollection);
      }
    }

    public virtual new int SelectedIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new ListItem SelectedItem
    {
      get
      {
        return default(ListItem);
      }
    }

    public virtual new string SelectedValue
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

    public virtual new string Text
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
    public event EventHandler SelectedIndexChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler TextChanged
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
