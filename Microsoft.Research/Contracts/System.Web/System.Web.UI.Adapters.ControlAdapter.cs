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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Web.UI.Adapters
{
  public abstract class ControlAdapter
  {
    protected internal virtual void BeginRender(HtmlTextWriter writer)
    {
      Contract.Requires(writer != null);
    }
    // protected internal virtual void CreateChildControls();
    protected internal virtual void EndRender(HtmlTextWriter writer)
    {
      Contract.Requires(writer != null);
    }
    // protected internal virtual void LoadAdapterControlState(object state);
    // protected internal virtual void LoadAdapterViewState(object state);
    protected internal virtual void OnInit(EventArgs e)
    {
      Contract.Requires(e != null);
    }
    protected internal virtual void OnLoad(EventArgs e)
    {
      Contract.Requires(e != null);
    }
    protected internal virtual void OnPreRender(EventArgs e)
    {
      Contract.Requires(e != null);
    }
    protected internal virtual void OnUnload(EventArgs e)
    {
      Contract.Requires(e != null);
    }
    protected internal virtual void Render(HtmlTextWriter writer)
    {
      Contract.Requires(writer != null);
    }
    protected virtual void RenderChildren(HtmlTextWriter writer)
    {
      Contract.Requires(writer != null);
    }
    //protected internal virtual object SaveAdapterControlState();
    //protected internal virtual object SaveAdapterViewState();


    protected HttpBrowserCapabilities Browser
    {
      get
      {
        Contract.Ensures(Contract.Result<HttpBrowserCapabilities>() != null);
        return default(HttpBrowserCapabilities);
      }
    }

    // protected Control Control { get; }

    //protected Page Page { get; }

    // protected PageAdapter PageAdapter { get; }
  }

}
