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

// File System.Web.UI.WebControls.BoundField.cs
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
  public partial class BoundField : DataControlField
  {
    #region Methods and constructors
    public BoundField()
    {
    }

    protected override void CopyProperties(DataControlField newField)
    {
    }

    protected override DataControlField CreateField()
    {
      return default(DataControlField);
    }

    public override void ExtractValuesFromCell(System.Collections.Specialized.IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
    {
    }

    protected virtual new string FormatDataValue(Object dataValue, bool encode)
    {
      return default(string);
    }

    protected virtual new Object GetDesignTimeValue()
    {
      return default(Object);
    }

    protected virtual new Object GetValue(System.Web.UI.Control controlContainer)
    {
      return default(Object);
    }

    public override bool Initialize(bool enableSorting, System.Web.UI.Control control)
    {
      return default(bool);
    }

    public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
    {
    }

    protected virtual new void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
    {
    }

    protected override void LoadViewState(Object state)
    {
    }

    protected virtual new void OnDataBindField(Object sender, EventArgs e)
    {
    }

    public override void ValidateSupportsCallback()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool ApplyFormatInEditMode
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool ConvertEmptyStringToNull
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string DataField
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string DataFormatString
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

    public virtual new bool HtmlEncode
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool HtmlEncodeFormatString
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string NullDisplayText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool ReadOnly
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected virtual new bool SupportsHtmlEncode
    {
      get
      {
        return default(bool);
      }
    }
    #endregion

    #region Fields
    public readonly static string ThisExpression;
    #endregion
  }
}
