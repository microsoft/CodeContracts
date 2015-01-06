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

// File System.Web.UI.HtmlControls.HtmlSelect.cs
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


namespace System.Web.UI.HtmlControls
{
  public partial class HtmlSelect : HtmlContainerControl, System.Web.UI.IPostBackDataHandler, System.Web.UI.IParserAccessor
  {
    #region Methods and constructors
    protected override void AddParsedSubObject(Object obj)
    {
    }

    protected virtual new void ClearSelection()
    {
    }

    protected override System.Web.UI.ControlCollection CreateControlCollection()
    {
      return default(System.Web.UI.ControlCollection);
    }

    protected void EnsureDataBound()
    {
    }

    protected virtual new System.Collections.IEnumerable GetData()
    {
      return default(System.Collections.IEnumerable);
    }

    public HtmlSelect()
    {
    }

    protected virtual new bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
    {
      return default(bool);
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected override void OnDataBinding(EventArgs e)
    {
    }

    protected virtual new void OnDataPropertyChanged()
    {
    }

    protected virtual new void OnDataSourceViewChanged(Object sender, EventArgs e)
    {
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected internal override void OnLoad(EventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected virtual new void OnServerChange(EventArgs e)
    {
    }

    protected virtual new void RaisePostDataChangedEvent()
    {
    }

    protected override void RenderAttributes(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override void RenderChildren(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    protected virtual new void Select(int[] selectedIndices)
    {
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
    public virtual new string DataMember
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new Object DataSource
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public virtual new string DataSourceID
    {
      get
      {
        return default(string);
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

    public override string InnerHtml
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string InnerText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected bool IsBoundUsingDataSourceID
    {
      get
      {
        return default(bool);
      }
    }

    public System.Web.UI.WebControls.ListItemCollection Items
    {
      get
      {
        return default(System.Web.UI.WebControls.ListItemCollection);
      }
    }

    public bool Multiple
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected bool RequiresDataBinding
    {
      get
      {
        return default(bool);
      }
      set
      {
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

    protected virtual new int[] SelectedIndices
    {
      get
      {
        return default(int[]);
      }
    }

    public int Size
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string Value
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
    public event EventHandler ServerChange
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
