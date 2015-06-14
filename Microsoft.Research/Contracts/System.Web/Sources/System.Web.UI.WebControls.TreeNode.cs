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

// File System.Web.UI.WebControls.TreeNode.cs
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
  public partial class TreeNode : System.Web.UI.IStateManager, ICloneable
  {
    #region Methods and constructors
    protected virtual new Object Clone()
    {
      return default(Object);
    }

    public void Collapse()
    {
    }

    public void CollapseAll()
    {
    }

    public void Expand()
    {
    }

    public void ExpandAll()
    {
    }

    protected virtual new void LoadViewState(Object state)
    {
    }

    protected virtual new void RenderPostText(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new void RenderPreText(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected virtual new Object SaveViewState()
    {
      return default(Object);
    }

    public void Select()
    {
    }

    Object System.ICloneable.Clone()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.LoadViewState(Object state)
    {
    }

    Object System.Web.UI.IStateManager.SaveViewState()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.TrackViewState()
    {
    }

    public void ToggleExpandState()
    {
    }

    protected void TrackViewState()
    {
    }

    public TreeNode()
    {
    }

    public TreeNode(string text, string value, string imageUrl, string navigateUrl, string target)
    {
    }

    public TreeNode(string text, string value, string imageUrl)
    {
    }

    public TreeNode(string text, string value)
    {
    }

    protected internal TreeNode(TreeView owner, bool isRoot)
    {
    }

    public TreeNode(string text)
    {
    }
    #endregion

    #region Properties and indexers
    public bool Checked
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TreeNodeCollection ChildNodes
    {
      get
      {
        return default(TreeNodeCollection);
      }
    }

    public bool DataBound
    {
      get
      {
        return default(bool);
      }
    }

    public Object DataItem
    {
      get
      {
        return default(Object);
      }
    }

    public string DataPath
    {
      get
      {
        return default(string);
      }
    }

    public int Depth
    {
      get
      {
        return default(int);
      }
    }

    public Nullable<bool> Expanded
    {
      get
      {
        return default(Nullable<bool>);
      }
      set
      {
      }
    }

    public string ImageToolTip
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ImageUrl
    {
      get
      {
        return default(string);
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

    public string NavigateUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.WebControls.TreeNode Parent
    {
      get
      {
        return default(System.Web.UI.WebControls.TreeNode);
      }
    }

    public bool PopulateOnDemand
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TreeNodeSelectAction SelectAction
    {
      get
      {
        return default(TreeNodeSelectAction);
      }
      set
      {
      }
    }

    public bool Selected
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Nullable<bool> ShowCheckBox
    {
      get
      {
        return default(Nullable<bool>);
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

    public string Target
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Text
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ToolTip
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Value
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ValuePath
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
