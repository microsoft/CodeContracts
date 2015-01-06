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

// File System.Web.UI.HtmlControls.HtmlControl.cs
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


namespace System.Web.UI.HtmlControls
{
  abstract public partial class HtmlControl : System.Web.UI.Control, System.Web.UI.IAttributeAccessor
  {
    #region Methods and constructors
    protected override System.Web.UI.ControlCollection CreateControlCollection ()
    {
      return default(System.Web.UI.ControlCollection);
    }

    protected virtual new string GetAttribute (string name)
    {
      return default(string);
    }

    protected HtmlControl ()
    {
    }

    protected HtmlControl (string tag)
    {
    }

    protected internal override void Render (System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RenderAttributes (System.Web.UI.HtmlTextWriter writer)
    {
      Contract.Requires (writer != null);
    }

    protected virtual new void RenderBeginTag (System.Web.UI.HtmlTextWriter writer)
    {
      Contract.Requires (writer != null);
    }

    protected virtual new void SetAttribute (string name, string value)
    {
    }

    string System.Web.UI.IAttributeAccessor.GetAttribute (string name)
    {
      return default(string);
    }

    void System.Web.UI.IAttributeAccessor.SetAttribute (string name, string value)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Web.UI.AttributeCollection Attributes
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.AttributeCollection>() != null);

        return default(System.Web.UI.AttributeCollection);
      }
    }

    public bool Disabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Web.UI.CssStyleCollection Style
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.CssStyleCollection>() != null);

        return default(System.Web.UI.CssStyleCollection);
      }
    }

    public virtual new string TagName
    {
      get
      {
        return default(string);
      }
    }

    protected override bool ViewStateIgnoresCase
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
