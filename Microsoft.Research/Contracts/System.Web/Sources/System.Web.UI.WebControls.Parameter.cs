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

// File System.Web.UI.WebControls.Parameter.cs
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
  public partial class Parameter : ICloneable, System.Web.UI.IStateManager
  {
    #region Methods and constructors
    protected virtual new System.Web.UI.WebControls.Parameter Clone()
    {
      return default(System.Web.UI.WebControls.Parameter);
    }

    public static TypeCode ConvertDbTypeToTypeCode(System.Data.DbType dbType)
    {
      return default(TypeCode);
    }

    public static System.Data.DbType ConvertTypeCodeToDbType(TypeCode typeCode)
    {
      return default(System.Data.DbType);
    }

    protected internal virtual new Object Evaluate(System.Web.HttpContext context, System.Web.UI.Control control)
    {
      return default(Object);
    }

    public System.Data.DbType GetDatabaseType()
    {
      return default(System.Data.DbType);
    }

    protected virtual new void LoadViewState(Object savedState)
    {
    }

    protected void OnParameterChanged()
    {
    }

    public Parameter(string name, System.Data.DbType dbType, string defaultValue)
    {
    }

    public Parameter()
    {
    }

    public Parameter(string name)
    {
    }

    public Parameter(string name, System.Data.DbType dbType)
    {
    }

    public Parameter(string name, TypeCode type, string defaultValue)
    {
    }

    protected Parameter(System.Web.UI.WebControls.Parameter original)
    {
    }

    public Parameter(string name, TypeCode type)
    {
    }

    protected virtual new Object SaveViewState()
    {
      return default(Object);
    }

    protected internal virtual new void SetDirty()
    {
    }

    Object System.ICloneable.Clone()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.LoadViewState(Object savedState)
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
    #endregion

    #region Properties and indexers
    public bool ConvertEmptyStringToNull
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Data.DbType DbType
    {
      get
      {
        return default(System.Data.DbType);
      }
      set
      {
      }
    }

    public string DefaultValue
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Data.ParameterDirection Direction
    {
      get
      {
        return default(System.Data.ParameterDirection);
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

    bool System.Web.UI.IStateManager.IsTrackingViewState
    {
      get
      {
        return default(bool);
      }
    }

    public TypeCode Type
    {
      get
      {
        return default(TypeCode);
      }
      set
      {
      }
    }

    protected System.Web.UI.StateBag ViewState
    {
      get
      {
        return default(System.Web.UI.StateBag);
      }
    }
    #endregion
  }
}
