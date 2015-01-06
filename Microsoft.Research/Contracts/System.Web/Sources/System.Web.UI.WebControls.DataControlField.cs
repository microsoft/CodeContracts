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

// File System.Web.UI.WebControls.DataControlField.cs
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
  abstract public partial class DataControlField : System.Web.UI.IStateManager, System.Web.UI.IDataSourceViewSchemaAccessor
  {
    #region Methods and constructors
    protected internal System.Web.UI.WebControls.DataControlField CloneField()
    {
      return default(System.Web.UI.WebControls.DataControlField);
    }

    protected virtual new void CopyProperties(System.Web.UI.WebControls.DataControlField newField)
    {
    }

    protected abstract System.Web.UI.WebControls.DataControlField CreateField();

    protected DataControlField()
    {
    }

    public virtual new void ExtractValuesFromCell(System.Collections.Specialized.IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
    {
    }

    public virtual new bool Initialize(bool sortingEnabled, System.Web.UI.Control control)
    {
      return default(bool);
    }

    public virtual new void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
    {
    }

    protected virtual new void LoadViewState(Object savedState)
    {
    }

    protected virtual new void OnFieldChanged()
    {
    }

    protected virtual new Object SaveViewState()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.LoadViewState(Object state)
    {
    }

    Object System.Web.UI.IStateManager.SaveViewState()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.TrackViewState()
    {
    }

    public override string ToString()
    {
      return default(string);
    }

    protected virtual new void TrackViewState()
    {
    }

    public virtual new void ValidateSupportsCallback()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new string AccessibleHeaderText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected System.Web.UI.Control Control
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

    public Style ControlStyle
    {
      get
      {
        return default(Style);
      }
    }

    protected bool DesignMode
    {
      get
      {
        return default(bool);
      }
    }

    public TableItemStyle FooterStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new string FooterText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string HeaderImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TableItemStyle HeaderStyle
    {
      get
      {
        return default(TableItemStyle);
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

    public virtual new bool InsertVisible
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected bool IsTrackingViewState
    {
      get
      {
        return default(bool);
      }
    }

    public TableItemStyle ItemStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new bool ShowHeader
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string SortExpression
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    Object System.Web.UI.IDataSourceViewSchemaAccessor.DataSourceViewSchema
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    bool System.Web.UI.IStateManager.IsTrackingViewState
    {
      get
      {
        return default(bool);
      }
    }

    protected System.Web.UI.StateBag ViewState
    {
      get
      {
        return default(System.Web.UI.StateBag);
      }
    }

    public bool Visible
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
