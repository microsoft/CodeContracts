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

// File System.Web.UI.WebControls.WebParts.WebPart.cs
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
  abstract public partial class WebPart : Part, IWebPart, IWebActionable, IWebEditable
  {
    #region Methods and constructors
    public virtual new EditorPartCollection CreateEditorParts ()
    {
      return default(EditorPartCollection);
    }

    protected internal virtual new void OnClosing (EventArgs e)
    {
    }

    protected internal virtual new void OnConnectModeChanged (EventArgs e)
    {
    }

    protected internal virtual new void OnDeleting (EventArgs e)
    {
    }

    protected internal virtual new void OnEditModeChanged (EventArgs e)
    {
    }

    protected void SetPersonalizationDirty ()
    {
      Contract.Requires (this.WebPartManager.Personalization != null);
      Contract.Ensures (this.WebPartManager.Personalization != null);
    }

    public static void SetPersonalizationDirty (System.Web.UI.Control control)
    {
    }

    protected override void TrackViewState ()
    {
    }

    protected WebPart ()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool AllowClose
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool AllowConnect
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool AllowEdit
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool AllowHide
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool AllowMinimize
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool AllowZoneChange
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string AuthorizationFilter
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string CatalogIconImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override PartChromeState ChromeState
    {
      get
      {
        return default(PartChromeState);
      }
      set
      {
      }
    }

    public override PartChromeType ChromeType
    {
      get
      {
        return default(PartChromeType);
      }
      set
      {
      }
    }

    public string ConnectErrorMessage
    {
      get
      {
        return default(string);
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

    public override System.Web.UI.WebControls.ContentDirection Direction
    {
      get
      {
        return default(System.Web.UI.WebControls.ContentDirection);
      }
      set
      {
      }
    }

    public string DisplayTitle
    {
      get
      {
        return default(string);
      }
    }

    public virtual new WebPartExportMode ExportMode
    {
      get
      {
        return default(WebPartExportMode);
      }
      set
      {
      }
    }

    public bool HasSharedData
    {
      get
      {
        return default(bool);
      }
    }

    public bool HasUserData
    {
      get
      {
        return default(bool);
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

    public virtual new WebPartHelpMode HelpMode
    {
      get
      {
        return default(WebPartHelpMode);
      }
      set
      {
      }
    }

    public virtual new string HelpUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool Hidden
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string ImportErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool IsClosed
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsShared
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsStandalone
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsStatic
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string Subtitle
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

    public virtual new string TitleIconImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string TitleUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new WebPartVerbCollection Verbs
    {
      get
      {
        return default(WebPartVerbCollection);
      }
    }

    public virtual new Object WebBrowsableObject
    {
      get
      {
        return default(Object);
      }
    }

    protected WebPartManager WebPartManager
    {
      get
      {
        return default(WebPartManager);
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

    public WebPartZoneBase Zone
    {
      get
      {
        return default(WebPartZoneBase);
      }
    }

    public int ZoneIndex
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
