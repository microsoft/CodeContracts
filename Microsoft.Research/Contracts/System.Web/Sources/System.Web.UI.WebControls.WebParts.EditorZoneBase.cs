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

// File System.Web.UI.WebControls.WebParts.EditorZoneBase.cs
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
  abstract public partial class EditorZoneBase : ToolZone
  {
    #region Methods and constructors
    protected override void Close()
    {
    }

    protected internal override void CreateChildControls()
    {
    }

    protected virtual new EditorPartChrome CreateEditorPartChrome()
    {
      return default(EditorPartChrome);
    }

    protected abstract EditorPartCollection CreateEditorParts();

    protected EditorZoneBase() : base (default(System.Collections.ICollection))
    {
    }

    protected void InvalidateEditorParts()
    {
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected override void OnDisplayModeChanged(Object sender, WebPartDisplayModeEventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected override void OnSelectedWebPartChanged(Object sender, WebPartEventArgs e)
    {
    }

    protected override void RaisePostBackEvent(string eventArgument)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override void RenderBody(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override void RenderVerbs(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    protected override void TrackViewState()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new WebPartVerb ApplyVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    public virtual new WebPartVerb CancelVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    protected override bool Display
    {
      get
      {
        return default(bool);
      }
    }

    public EditorPartChrome EditorPartChrome
    {
      get
      {
        return default(EditorPartChrome);
      }
    }

    public EditorPartCollection EditorParts
    {
      get
      {
        return default(EditorPartCollection);
      }
    }

    public override string EmptyZoneText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string ErrorText
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

    public override string InstructionText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new WebPartVerb OKVerb
    {
      get
      {
        return default(WebPartVerb);
      }
    }

    protected WebPart WebPartToEdit
    {
      get
      {
        return default(WebPart);
      }
    }
    #endregion
  }
}
