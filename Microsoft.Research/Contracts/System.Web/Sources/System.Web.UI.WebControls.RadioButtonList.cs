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

// File System.Web.UI.WebControls.RadioButtonList.cs
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
  public partial class RadioButtonList : ListControl, IRepeatInfoUser, System.Web.UI.INamingContainer, System.Web.UI.IPostBackDataHandler
  {
    #region Methods and constructors
    protected override Style CreateControlStyle()
    {
      return default(Style);
    }

    protected override System.Web.UI.Control FindControl(string id, int pathOffset)
    {
      return default(System.Web.UI.Control);
    }

    protected virtual new Style GetItemStyle(ListItemType itemType, int repeatIndex)
    {
      return default(Style);
    }

    protected virtual new bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
    {
      return default(bool);
    }

    public RadioButtonList()
    {
    }

    protected virtual new void RaisePostDataChangedEvent()
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, System.Web.UI.HtmlTextWriter writer)
    {
    }

    bool System.Web.UI.IPostBackDataHandler.LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
    {
      return default(bool);
    }

    void System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent()
    {
    }

    Style System.Web.UI.WebControls.IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
    {
      return default(Style);
    }

    void System.Web.UI.WebControls.IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, System.Web.UI.HtmlTextWriter writer)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new int CellPadding
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new int CellSpacing
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    protected virtual new bool HasFooter
    {
      get
      {
        return default(bool);
      }
    }

    protected virtual new bool HasHeader
    {
      get
      {
        return default(bool);
      }
    }

    protected virtual new bool HasSeparators
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new int RepeatColumns
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new RepeatDirection RepeatDirection
    {
      get
      {
        return default(RepeatDirection);
      }
      set
      {
      }
    }

    protected virtual new int RepeatedItemCount
    {
      get
      {
        return default(int);
      }
    }

    public virtual new RepeatLayout RepeatLayout
    {
      get
      {
        return default(RepeatLayout);
      }
      set
      {
      }
    }

    bool System.Web.UI.WebControls.IRepeatInfoUser.HasFooter
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Web.UI.WebControls.IRepeatInfoUser.HasHeader
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Web.UI.WebControls.IRepeatInfoUser.HasSeparators
    {
      get
      {
        return default(bool);
      }
    }

    int System.Web.UI.WebControls.IRepeatInfoUser.RepeatedItemCount
    {
      get
      {
        return default(int);
      }
    }

    public virtual new TextAlign TextAlign
    {
      get
      {
        return default(TextAlign);
      }
      set
      {
      }
    }
    #endregion
  }
}
