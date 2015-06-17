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

// File System.Web.UI.WebControls.WebParts.WebPartVerb.cs
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


namespace System.Web.UI.WebControls.WebParts
{
  public partial class WebPartVerb : System.Web.UI.IStateManager
  {
    #region Methods and constructors
    protected virtual new void LoadViewState (Object savedState)
    {
    }

    protected virtual new Object SaveViewState ()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.LoadViewState (Object savedState)
    {
    }

    Object System.Web.UI.IStateManager.SaveViewState ()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.TrackViewState ()
    {
    }

    protected virtual new void TrackViewState ()
    {
    }

    public WebPartVerb (string id, WebPartEventHandler serverClickHandler)
    {
      Contract.Ensures (!string.IsNullOrEmpty(id));
    }

    public WebPartVerb (string id, WebPartEventHandler serverClickHandler, string clientClickHandler)
    {
      Contract.Ensures (!string.IsNullOrEmpty(id));
      Contract.Ensures (!string.IsNullOrEmpty(clientClickHandler));
    }

    public WebPartVerb (string id, string clientClickHandler)
    {
      Contract.Ensures (!string.IsNullOrEmpty(id));
      Contract.Ensures (!string.IsNullOrEmpty(clientClickHandler));
    }
    #endregion

    #region Properties and indexers
    public virtual new bool Checked
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string ClientClickHandler
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string Description
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool Enabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string ID
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string ImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected virtual new bool IsTrackingViewState
    {
      get
      {
        return default(bool);
      }
    }

    public WebPartEventHandler ServerClickHandler
    {
      get
      {
        return default(WebPartEventHandler);
      }
    }

    bool System.Web.UI.IStateManager.IsTrackingViewState
    {
      get
      {
        return default(bool);
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

    protected System.Web.UI.StateBag ViewState
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.StateBag>() != null);

        return default(System.Web.UI.StateBag);
      }
    }

    public virtual new bool Visible
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
