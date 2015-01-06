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

// File System.Web.UI.WebControls.TreeView.cs
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
  public partial class TreeView : HierarchicalDataBoundControl, System.Web.UI.IPostBackEventHandler, System.Web.UI.IPostBackDataHandler, System.Web.UI.ICallbackEventHandler
  {
    #region Methods and constructors
    protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
    {
    }

    public void CollapseAll()
    {
    }

    protected override System.Web.UI.ControlCollection CreateControlCollection()
    {
      return default(System.Web.UI.ControlCollection);
    }

    protected internal virtual new TreeNode CreateNode()
    {
      return default(TreeNode);
    }

    public sealed override void DataBind()
    {
    }

    public void ExpandAll()
    {
    }

    public TreeNode FindNode(string valuePath)
    {
      return default(TreeNode);
    }

    protected virtual new string GetCallbackResult()
    {
      return default(string);
    }

    protected virtual new bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
    {
      return default(bool);
    }

    protected override void LoadViewState(Object state)
    {
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected virtual new void OnSelectedNodeChanged(EventArgs e)
    {
    }

    protected virtual new void OnTreeNodeCheckChanged(TreeNodeEventArgs e)
    {
    }

    protected virtual new void OnTreeNodeCollapsed(TreeNodeEventArgs e)
    {
    }

    protected virtual new void OnTreeNodeDataBound(TreeNodeEventArgs e)
    {
    }

    protected virtual new void OnTreeNodeExpanded(TreeNodeEventArgs e)
    {
    }

    protected virtual new void OnTreeNodePopulate(TreeNodeEventArgs e)
    {
    }

    protected internal override void PerformDataBinding()
    {
    }

    protected virtual new void RaiseCallbackEvent(string eventArgument)
    {
    }

    protected virtual new void RaisePostBackEvent(string eventArgument)
    {
    }

    protected virtual new void RaisePostDataChangedEvent()
    {
    }

    public override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override void RenderContents(System.Web.UI.HtmlTextWriter writer)
    {
    }

    public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    protected void SetNodeDataBound(TreeNode node, bool dataBound)
    {
    }

    protected void SetNodeDataItem(TreeNode node, Object dataItem)
    {
    }

    protected void SetNodeDataPath(TreeNode node, string dataPath)
    {
    }

    string System.Web.UI.ICallbackEventHandler.GetCallbackResult()
    {
      return default(string);
    }

    void System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
    {
    }

    bool System.Web.UI.IPostBackDataHandler.LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
    {
      return default(bool);
    }

    void System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent()
    {
    }

    void System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
    {
    }

    protected override void TrackViewState()
    {
    }

    public TreeView()
    {
    }
    #endregion

    #region Properties and indexers
    public bool AutoGenerateDataBindings
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TreeNodeCollection CheckedNodes
    {
      get
      {
        return default(TreeNodeCollection);
      }
    }

    public string CollapseImageToolTip
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string CollapseImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TreeNodeBindingCollection DataBindings
    {
      get
      {
        return default(TreeNodeBindingCollection);
      }
    }

    public bool EnableClientScript
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int ExpandDepth
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string ExpandImageToolTip
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ExpandImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style HoverNodeStyle
    {
      get
      {
        return default(Style);
      }
    }

    public TreeViewImageSet ImageSet
    {
      get
      {
        return default(TreeViewImageSet);
      }
      set
      {
      }
    }

    public TreeNodeStyle LeafNodeStyle
    {
      get
      {
        return default(TreeNodeStyle);
      }
    }

    public TreeNodeStyleCollection LevelStyles
    {
      get
      {
        return default(TreeNodeStyleCollection);
      }
    }

    public string LineImagesFolder
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int MaxDataBindDepth
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int NodeIndent
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public TreeNodeCollection Nodes
    {
      get
      {
        return default(TreeNodeCollection);
      }
    }

    public TreeNodeStyle NodeStyle
    {
      get
      {
        return default(TreeNodeStyle);
      }
    }

    public bool NodeWrap
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string NoExpandImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TreeNodeStyle ParentNodeStyle
    {
      get
      {
        return default(TreeNodeStyle);
      }
    }

    public char PathSeparator
    {
      get
      {
        return default(char);
      }
      set
      {
      }
    }

    public bool PopulateNodesFromClient
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TreeNodeStyle RootNodeStyle
    {
      get
      {
        return default(TreeNodeStyle);
      }
    }

    public TreeNode SelectedNode
    {
      get
      {
        return default(TreeNode);
      }
    }

    public TreeNodeStyle SelectedNodeStyle
    {
      get
      {
        return default(TreeNodeStyle);
      }
    }

    public string SelectedValue
    {
      get
      {
        return default(string);
      }
    }

    public TreeNodeTypes ShowCheckBoxes
    {
      get
      {
        return default(TreeNodeTypes);
      }
      set
      {
      }
    }

    public bool ShowExpandCollapse
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ShowLines
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string SkipLinkText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected override System.Web.UI.HtmlTextWriterTag TagKey
    {
      get
      {
        return default(System.Web.UI.HtmlTextWriterTag);
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

    public override bool Visible
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

    #region Events
    public event EventHandler SelectedNodeChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event TreeNodeEventHandler TreeNodeCheckChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event TreeNodeEventHandler TreeNodeCollapsed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event TreeNodeEventHandler TreeNodeDataBound
    {
      add
      {
      }
      remove
      {
      }
    }

    public event TreeNodeEventHandler TreeNodeExpanded
    {
      add
      {
      }
      remove
      {
      }
    }

    public event TreeNodeEventHandler TreeNodePopulate
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
