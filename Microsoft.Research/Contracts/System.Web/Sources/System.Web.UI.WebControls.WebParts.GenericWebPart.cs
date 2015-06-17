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

// File System.Web.UI.WebControls.WebParts.GenericWebPart.cs
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


namespace System.Web.UI.WebControls.WebParts
{
  public partial class GenericWebPart : WebPart
  {
    #region Methods and constructors
    protected internal override void CreateChildControls()
    {
    }

    protected override System.Web.UI.ControlCollection CreateControlCollection()
    {
      return default(System.Web.UI.ControlCollection);
    }

    public override EditorPartCollection CreateEditorParts()
    {
      return default(EditorPartCollection);
    }

    protected internal GenericWebPart(System.Web.UI.Control control)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }
    #endregion

    #region Properties and indexers
    public override string CatalogIconImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.Control ChildControl
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

    public override string Description
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override System.Web.UI.WebControls.Unit Height
    {
      get
      {
        return default(System.Web.UI.WebControls.Unit);
      }
      set
      {
      }
    }

    public override sealed string ID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string Subtitle
    {
      get
      {
        return default(string);
      }
    }

    public override string Title
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string TitleIconImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string TitleUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override WebPartVerbCollection Verbs
    {
      get
      {
        return default(WebPartVerbCollection);
      }
    }

    public override Object WebBrowsableObject
    {
      get
      {
        return default(Object);
      }
    }

    public override System.Web.UI.WebControls.Unit Width
    {
      get
      {
        return default(System.Web.UI.WebControls.Unit);
      }
      set
      {
      }
    }
    #endregion
  }
}
