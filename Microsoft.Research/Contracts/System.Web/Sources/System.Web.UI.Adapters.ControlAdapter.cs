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

// File System.Web.UI.Adapters.ControlAdapter.cs
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


namespace System.Web.UI.Adapters
{
  abstract public partial class ControlAdapter
  {
    #region Methods and constructors
    protected internal virtual new void BeginRender(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected ControlAdapter()
    {
    }

    protected internal virtual new void CreateChildControls()
    {
    }

    protected internal virtual new void EndRender(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal virtual new void LoadAdapterControlState(Object state)
    {
    }

    protected internal virtual new void LoadAdapterViewState(Object state)
    {
    }

    protected internal virtual new void OnInit(EventArgs e)
    {
    }

    protected internal virtual new void OnLoad(EventArgs e)
    {
    }

    protected internal virtual new void OnPreRender(EventArgs e)
    {
    }

    protected internal virtual new void OnUnload(EventArgs e)
    {
    }

    protected internal virtual new void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RenderChildren(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal virtual new Object SaveAdapterControlState()
    {
      return default(Object);
    }

    protected internal virtual new Object SaveAdapterViewState()
    {
      return default(Object);
    }
    #endregion

    #region Properties and indexers
    protected System.Web.HttpBrowserCapabilities Browser
    {
      get
      {
        return default(System.Web.HttpBrowserCapabilities);
      }
    }

    protected System.Web.UI.Control Control
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

    protected System.Web.UI.Page Page
    {
      get
      {
        return default(System.Web.UI.Page);
      }
    }

    protected PageAdapter PageAdapter
    {
      get
      {
        return default(PageAdapter);
      }
    }
    #endregion
  }
}
