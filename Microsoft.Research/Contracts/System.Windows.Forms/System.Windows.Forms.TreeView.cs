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

#region Assembly System.Windows.Forms.dll, v4.0.30319
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Windows.Forms.dll
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms {
  // Summary:
  //     Displays a hierarchical collection of labeled items, each represented by
  //     a System.Windows.Forms.TreeNode.
  [ComVisible(true)]
  [Designer("System.Windows.Forms.Design.TreeViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  //[Docking(DockingBehavior.Ask)]
  [DefaultProperty("Nodes")]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  [DefaultEvent("AfterSelect")]
  //[SRDescription("DescriptionTreeView")]
  public class TreeView : Control {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TreeView class.
    //public TreeView();

    //
    // Returns:
    //     A System.Drawing.Color that represents the background color of the control.
    //     The default is the value of the System.Windows.Forms.Control.DefaultBackColor
    //     property.
    //public override Color BackColor { get { return default(); } set { } }
    //
    // Summary:
    //     Gets or set the background image for the System.Windows.Forms.TreeView control.
    //
    // Returns:
    //     The System.Drawing.Image that is the background image for the System.Windows.Forms.TreeView
    //     control.
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    //public override Image BackgroundImage { get { return default(); } set { } }
    //
    // Summary:
    //     Gets or sets the layout of the background image for the System.Windows.Forms.TreeView
    //     control.
    //
    // Returns:
    //     One of the System.Windows.Forms.ImageLayout values. The default is System.Windows.Forms.ImageLayout.Tile.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public override ImageLayout BackgroundImageLayout { get { return default(); } set { } }
    //
    // Summary:
    //     Gets or sets the border style of the tree view control.
    //
    // Returns:
    //     One of the System.Windows.Forms.BorderStyle values. The default is System.Windows.Forms.BorderStyle.Fixed3D.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The assigned value is not one of the System.Windows.Forms.BorderStyle values.
    //[SRDescription("borderStyleDescr")]
    //[DispId(-504)]
    //[SRCategory("CatAppearance")]
    //public BorderStyle BorderStyle { get { return default(); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether check boxes are displayed next to
    //     the tree nodes in the tree view control.
    //
    // Returns:
    //     true if a check box is displayed next to each tree node in the tree view
    //     control; otherwise, false. The default is false.
    //[SRDescription("TreeViewCheckBoxesDescr")]
    //[SRCategory("CatAppearance")]
    //[DefaultValue(false)]
    public bool CheckBoxes { get { return default(bool); } set { } }
    //
    // Summary:
    //     Overrides System.Windows.Forms.Control.CreateParams.
    //
    // Returns:
    //     A System.Windows.Forms.CreateParams that contains the required creation parameters
    //     when the handle to the control is created.
    //protected override CreateParams CreateParams { get { return default(); } }
    //
    //
    // Returns:
    //     The default System.Drawing.Size of the control.
    //protected override Size DefaultSize { get { return default(); } }
    //
    // Summary:
    //     Gets or sets a value indicating whether the control should redraw its surface
    //     using a secondary buffer. The System.Windows.Forms.TreeView.DoubleBuffered
    //     property has no effect on the System.Windows.Forms.TreeView control.
    //
    // Returns:
    //     true if the control uses a secondary buffer; otherwise, false.
    [EditorBrowsable(EditorBrowsableState.Never)]
    //protected override bool DoubleBuffered { get { return default(); } set { } }
    //
    // Summary:
    //     Gets or sets the mode in which the control is drawn.
    //
    // Returns:
    //     One of the System.Windows.Forms.TreeViewDrawMode values. The default is System.Windows.Forms.TreeViewDrawMode.Normal.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The property value is not a valid System.Windows.Forms.TreeViewDrawMode value.
    //[SRDescription("TreeViewDrawModeDescr")]
    //[SRCategory("CatBehavior")]
    //public TreeViewDrawMode DrawMode { get { return default(); } set { } }
    //
    // Summary:
    //     The current foreground color for this control, which is the color the control
    //     uses to draw its text.
    //
    // Returns:
    //     The foreground System.Drawing.Color of the control. The default is the value
    //     of the System.Windows.Forms.Control.DefaultForeColor property.
    //public override Color ForeColor { get { return default(); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether the selection highlight spans the
    //     width of the tree view control.
    //
    // Returns:
    //     true if the selection highlight spans the width of the tree view control;
    //     otherwise, false. The default is false.
    //[SRDescription("TreeViewFullRowSelectDescr")]
    //[DefaultValue(false)]
    //[SRCategory("CatBehavior")]
    public bool FullRowSelect { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether the selected tree node remains highlighted
    //     even when the tree view has lost the focus.
    //
    // Returns:
    //     true if the selected tree node is not highlighted when the tree view has
    //     lost the focus; otherwise, false. The default is true.
    //[SRDescription("TreeViewHideSelectionDescr")]
    //[SRCategory("CatBehavior")]
    //[DefaultValue(true)]
    public bool HideSelection { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether a tree node label takes on the appearance
    //     of a hyperlink as the mouse pointer passes over it.
    //
    // Returns:
    //     true if a tree node label takes on the appearance of a hyperlink as the mouse
    //     pointer passes over it; otherwise, false. The default is false.
    [DefaultValue(false)]
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewHotTrackingDescr")]
    public bool HotTracking { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets the image-list index value of the default image that is displayed
    //     by the tree nodes.
    //
    // Returns:
    //     A zero-based index that represents the position of an System.Drawing.Image
    //     in an System.Windows.Forms.ImageList. The default is zero.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The specified index is less than 0.
    //[SRCategory("CatBehavior")]
    [DefaultValue(-1)]
    //[RelatedImageList("ImageList")]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    //[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
    [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    //[SRDescription("TreeViewImageIndexDescr")]
    public int ImageIndex { get { return default(int); } set { } }
    //
    // Summary:
    //     Gets or sets the key of the default image for each node in the System.Windows.Forms.TreeView
    //     control when it is in an unselected state.
    //
    // Returns:
    //     The key of the default image shown for each node System.Windows.Forms.TreeView
    //     control when the node is in an unselected state.
    //[SRCategory("CatBehavior")]
    //[RelatedImageList("ImageList")]
    [Localizable(true)]
    //[TypeConverter(typeof(ImageKeyConverter))]
    [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [DefaultValue("")]
    [RefreshProperties(RefreshProperties.Repaint)]
    //[SRDescription("TreeViewImageKeyDescr")]
    public string ImageKey { get { return default(string); } set { } }
    //
    // Summary:
    //     Gets or sets the System.Windows.Forms.ImageList that contains the System.Drawing.Image
    //     objects used by the tree nodes.
    //
    // Returns:
    //     The System.Windows.Forms.ImageList that contains the System.Drawing.Image
    //     objects used by the tree nodes. The default value is null.
    [DefaultValue("")]
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewImageListDescr")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public ImageList ImageList { get { return default(ImageList); } set { } }
    //
    // Summary:
    //     Gets or sets the distance to indent each of the child tree node levels.
    //
    // Returns:
    //     The distance, in pixels, to indent each of the child tree node levels. The
    //     default value is 19.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The assigned value is less than 0 (see Remarks).-or- The assigned value is
    //     greater than 32,000.
    [Localizable(true)]
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewIndentDescr")]
    public int Indent { get { return default(int); } set { } }
    //
    // Summary:
    //     Gets or sets the height of each tree node in the tree view control.
    //
    // Returns:
    //     The height, in pixels, of each tree node in the tree view.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The assigned value is less than one.-or- The assigned value is greater than
    //     the System.Int16.MaxValue value.
    //[SRDescription("TreeViewItemHeightDescr")]
    //[SRCategory("CatAppearance")]
    public int ItemHeight { get { return default(int); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether the label text of the tree nodes
    //     can be edited.
    //
    // Returns:
    //     true if the label text of the tree nodes can be edited; otherwise, false.
    //     The default is false.
    [DefaultValue(false)]
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewLabelEditDescr")]
    public bool LabelEdit { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets the color of the lines connecting the nodes of the System.Windows.Forms.TreeView
    //     control.
    //
    // Returns:
    //     The System.Drawing.Color of the lines connecting the tree nodes.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewLineColorDescr")]
    [DefaultValue(typeof(Color), "Black")]
    public Color LineColor { get { return default(Color); } set { } }
    //
    // Summary:
    //     Gets the collection of tree nodes that are assigned to the tree view control.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNodeCollection that represents the tree nodes
    //     assigned to the tree view control.
    //[MergableProperty(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //[Localizable(true)]
    //[SRDescription("TreeViewNodesDescr")]
    //[SRCategory("CatBehavior")]
    //public TreeNodeCollection Nodes { get { return default(); } }
    //
    // Summary:
    //     Gets or sets the spacing between the System.Windows.Forms.TreeView control's
    //     contents and its edges.
    //
    // Returns:
    //     A System.Windows.Forms.Padding indicating the space between the control edges
    //     and its contents.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public Padding Padding { get { return default(); } set { } }
    //
    // Summary:
    //     Gets or sets the delimiter string that the tree node path uses.
    //
    // Returns:
    //     The delimiter string that the tree node System.Windows.Forms.TreeNode.FullPath
    //     property uses. The default is the backslash character (\).
    [DefaultValue(@"\")]
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewPathSeparatorDescr")]
    public string PathSeparator { get { return default(string); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.TreeView
    //     should be laid out from right-to-left.
    //
    // Returns:
    //     true to indicate the control should be laid out from right-to-left; otherwise,
    //     false. The default is false.
    //[SRCategory("CatAppearance")]
    [DefaultValue(false)]
    [Localizable(true)]
    //[SRDescription("ControlRightToLeftLayoutDescr")]
    public virtual bool RightToLeftLayout { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether the tree view control displays scroll
    //     bars when they are needed.
    //
    // Returns:
    //     true if the tree view control displays scroll bars when they are needed;
    //     otherwise, false. The default is true.
    //[SRDescription("TreeViewScrollableDescr")]
    //[SRCategory("CatBehavior")]
    [DefaultValue(true)]
    public bool Scrollable { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets the image list index value of the image that is displayed when
    //     a tree node is selected.
    //
    // Returns:
    //     A zero-based index value that represents the position of an System.Drawing.Image
    //     in an System.Windows.Forms.ImageList.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The index assigned value is less than zero.
    [Localizable(true)]
    [DefaultValue(-1)]
    //[SRCategory("CatBehavior")]
    //[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
    [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    //[SRDescription("TreeViewSelectedImageIndexDescr")]
    //[RelatedImageList("ImageList")]
    public int SelectedImageIndex { get { return default(int); } set { } }
    //
    // Summary:
    //     Gets or sets the key of the default image shown when a System.Windows.Forms.TreeNode
    //     is in a selected state.
    //
    // Returns:
    //     The key of the default image shown when a System.Windows.Forms.TreeNode is
    //     in a selected state.
    //[SRCategory("CatBehavior")]
    //[RelatedImageList("ImageList")]
    [Localizable(true)]
    //[TypeConverter(typeof(ImageKeyConverter))]
    [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [DefaultValue("")]
    [RefreshProperties(RefreshProperties.Repaint)]
    //[SRDescription("TreeViewSelectedImageKeyDescr")]
    public string SelectedImageKey { get { return default(string); } set { } }
    //
    // Summary:
    //     Gets or sets the tree node that is currently selected in the tree view control.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode that is currently selected in the tree
    //     view control.
    //[SRCategory("CatAppearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    //[SRDescription("TreeViewSelectedNodeDescr")]
    public TreeNode SelectedNode { get { return default(TreeNode); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether lines are drawn between tree nodes
    //     in the tree view control.
    //
    // Returns:
    //     true if lines are drawn between tree nodes in the tree view control; otherwise,
    //     false. The default is true.
    //[SRDescription("TreeViewShowLinesDescr")]
    //[SRCategory("CatBehavior")]
    [DefaultValue(true)]
    public bool ShowLines { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating ToolTips are shown when the mouse pointer
    //     hovers over a System.Windows.Forms.TreeNode.
    //
    // Returns:
    //     true if ToolTips are shown when the mouse pointer hovers over a System.Windows.Forms.TreeNode;
    //     otherwise, false. The default is false.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewShowShowNodeToolTipsDescr")]
    [DefaultValue(false)]
    public bool ShowNodeToolTips { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether plus-sign (+) and minus-sign (-)
    //     buttons are displayed next to tree nodes that contain child tree nodes.
    //
    // Returns:
    //     true if plus sign and minus sign buttons are displayed next to tree nodes
    //     that contain child tree nodes; otherwise, false. The default is true.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewShowPlusMinusDescr")]
    [DefaultValue(true)]
    public bool ShowPlusMinus { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether lines are drawn between the tree
    //     nodes that are at the root of the tree view.
    //
    // Returns:
    //     true if lines are drawn between the tree nodes that are at the root of the
    //     tree view; otherwise, false. The default is true.
    //[SRDescription("TreeViewShowRootLinesDescr")]
    //[SRCategory("CatBehavior")]
    [DefaultValue(true)]
    public bool ShowRootLines { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets a value indicating whether the tree nodes in the tree view are
    //     sorted.
    //
    // Returns:
    //     true if the tree nodes in the tree view are sorted; otherwise, false. The
    //     default is false.
    [DefaultValue(false)]
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewSortedDescr")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool Sorted { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets the image list used for indicating the state of the System.Windows.Forms.TreeView
    //     and its nodes.
    //
    // Returns:
    //     The System.Windows.Forms.ImageList used for indicating the state of the System.Windows.Forms.TreeView
    //     and its nodes.
    [DefaultValue("")]
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewStateImageListDescr")]
    public ImageList StateImageList { get { return default(ImageList); } set { } }
    //
    // Summary:
    //     Gets or sets the text of the System.Windows.Forms.TreeView.
    //
    // Returns:
    //     Null.
    //[Browsable(false)]
    //[Bindable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public override string Text { get { return default(); } set { } }
    //
    // Summary:
    //     Gets or sets the first fully-visible tree node in the tree view control.
    //
    // Returns:
    //     A System.Windows.Forms.TreeNode that represents the first fully-visible tree
    //     node in the tree view control.
    [Browsable(false)]
    //[SRCategory("CatAppearance")]
    //[SRDescription("TreeViewTopNodeDescr")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TreeNode TopNode { get { return default(TreeNode); } set { } }
    //
    // Summary:
    //     Gets or sets the implementation of System.Collections.IComparer to perform
    //     a custom sort of the System.Windows.Forms.TreeView nodes.
    //
    // Returns:
    //     The System.Collections.IComparer to perform the custom sort.
    //[SRCategory("CatBehavior")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[SRDescription("TreeViewNodeSorterDescr")]
    public IComparer TreeViewNodeSorter { get { return default(IComparer); } set { } }
    //
    // Summary:
    //     Gets the number of tree nodes that can be fully visible in the tree view
    //     control.
    //
    // Returns:
    //     The number of System.Windows.Forms.TreeNode items that can be fully visible
    //     in the System.Windows.Forms.TreeView control.
    //[SRCategory("CatAppearance")]
    //[SRDescription("TreeViewVisibleCountDescr")]
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public int VisibleCount { get { return default(); } }

    // Summary:
    //     Occurs after the tree node check box is checked.
    //[SRDescription("TreeViewAfterCheckDescr")]
    //[SRCategory("CatBehavior")]
    //public event TreeViewEventHandler AfterCheck;
    //
    // Summary:
    //     Occurs after the tree node is collapsed.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewAfterCollapseDescr")]
    //public event TreeViewEventHandler AfterCollapse;
    //
    // Summary:
    //     Occurs after the tree node is expanded.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewAfterExpandDescr")]
    //public event TreeViewEventHandler AfterExpand;
    //
    // Summary:
    //     Occurs after the tree node label text is edited.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewAfterEditDescr")]
    //public event NodeLabelEditEventHandler AfterLabelEdit;
    //
    // Summary:
    //     Occurs after the tree node is selected.
    //[SRDescription("TreeViewAfterSelectDescr")]
    //[SRCategory("CatBehavior")]
    //public event TreeViewEventHandler AfterSelect;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.TreeView.BackgroundImage property changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event EventHandler BackgroundImageChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.TreeView.BackgroundImageLayout property
    //     changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event EventHandler BackgroundImageLayoutChanged;
    //
    // Summary:
    //     Occurs before the tree node check box is checked.
    //[SRDescription("TreeViewBeforeCheckDescr")]
    //[SRCategory("CatBehavior")]
    //public event TreeViewCancelEventHandler BeforeCheck;
    //
    // Summary:
    //     Occurs before the tree node is collapsed.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewBeforeCollapseDescr")]
    //public event TreeViewCancelEventHandler BeforeCollapse;
    //
    // Summary:
    //     Occurs before the tree node is expanded.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewBeforeExpandDescr")]
    //public event TreeViewCancelEventHandler BeforeExpand;
    //
    // Summary:
    //     Occurs before the tree node label text is edited.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewBeforeEditDescr")]
    //public event NodeLabelEditEventHandler BeforeLabelEdit;
    //
    // Summary:
    //     Occurs before the tree node is selected.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewBeforeSelectDescr")]
    //public event TreeViewCancelEventHandler BeforeSelect;
    //
    // Summary:
    //     Occurs when a System.Windows.Forms.TreeView is drawn and the System.Windows.Forms.TreeView.DrawMode
    //     property is set to a System.Windows.Forms.TreeViewDrawMode value other than
    //     System.Windows.Forms.TreeViewDrawMode.Normal.
    //[SRDescription("TreeViewDrawNodeEventDescr")]
    //[SRCategory("CatBehavior")]
    //public event DrawTreeNodeEventHandler DrawNode;
    //
    // Summary:
    //     Occurs when the user begins dragging a node.
    //[SRDescription("ListViewItemDragDescr")]
    //[SRCategory("CatAction")]
    //public event ItemDragEventHandler ItemDrag;
    //
    // Summary:
    //     Occurs when the user clicks a System.Windows.Forms.TreeNode with the mouse.
    //[SRDescription("TreeViewNodeMouseClickDescr")]
    //[SRCategory("CatBehavior")]
    //public event TreeNodeMouseClickEventHandler NodeMouseClick;
    //
    // Summary:
    //     Occurs when the user double-clicks a System.Windows.Forms.TreeNode with the
    //     mouse.
    //[SRCategory("CatBehavior")]
    //[SRDescription("TreeViewNodeMouseDoubleClickDescr")]
    //public event TreeNodeMouseClickEventHandler NodeMouseDoubleClick;
    //
    // Summary:
    //     Occurs when the mouse hovers over a System.Windows.Forms.TreeNode.
    //[SRDescription("TreeViewNodeMouseHoverDescr")]
    //[SRCategory("CatAction")]
    //public event TreeNodeMouseHoverEventHandler NodeMouseHover;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.TreeView.Padding property
    //     changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event EventHandler PaddingChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.TreeView is drawn.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event PaintEventHandler Paint;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.TreeView.RightToLeftLayout
    //     property changes.
    //[SRCategory("CatPropertyChanged")]
    //[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
    //public event EventHandler RightToLeftLayoutChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.TreeView.Text property changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event EventHandler TextChanged;

    // Summary:
    //     Disables any redrawing of the tree view.
    //public void BeginUpdate();
    //
    // Summary:
    //     Collapses all the tree nodes.
    //public void CollapseAll();
    //protected override void CreateHandle();
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Windows.Forms.TreeView
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    //protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Enables the redrawing of the tree view.
    //public void EndUpdate();
    //
    // Summary:
    //     Expands all the tree nodes.
    //public void ExpandAll();
    //
    // Summary:
    //     Returns an System.Windows.Forms.OwnerDrawPropertyBag for the specified System.Windows.Forms.TreeNode.
    //
    // Parameters:
    //   node:
    //     The System.Windows.Forms.TreeNode for which to return an System.Windows.Forms.OwnerDrawPropertyBag.
    //
    //   state:
    //     The visible state of the System.Windows.Forms.TreeNode.
    //
    // Returns:
    //     An System.Windows.Forms.OwnerDrawPropertyBag for the specified System.Windows.Forms.TreeNode.
    //protected OwnerDrawPropertyBag GetItemRenderStyles(TreeNode node, int state);
    //
    // Summary:
    //     Retrieves the tree node that is at the specified point.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.Point to evaluate and retrieve the node from.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode at the specified point, in tree view (client)
    //     coordinates, or null if there is no node at that location.
    //public TreeNode GetNodeAt(Point pt);
    //
    // Summary:
    //     Retrieves the tree node at the point with the specified coordinates.
    //
    // Parameters:
    //   x:
    //     The System.Drawing.Point.X position to evaluate and retrieve the node from.
    //
    //   y:
    //     The System.Drawing.Point.Y position to evaluate and retrieve the node from.
    //
    // Returns:
    //     The System.Windows.Forms.TreeNode at the specified location, in tree view
    //     (client) coordinates, or null if there is no node at that location.
    //public TreeNode GetNodeAt(int x, int y);
    //
    // Summary:
    //     Retrieves the number of tree nodes, optionally including those in all subtrees,
    //     assigned to the tree view control.
    //
    // Parameters:
    //   includeSubTrees:
    //     true to count the System.Windows.Forms.TreeNode items that the subtrees contain;
    //     otherwise, false.
    //
    // Returns:
    //     The number of tree nodes, optionally including those in all subtrees, assigned
    //     to the tree view control.
    //public int GetNodeCount(bool includeSubTrees);
    //
    // Summary:
    //     Provides node information, given a point.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.Point at which to retrieve node information.
    //
    // Returns:
    //     A System.Windows.Forms.TreeViewHitTestInfo.
    //public TreeViewHitTestInfo HitTest(Point pt);
    //
    // Summary:
    //     Provides node information, given x- and y-coordinates.
    //
    // Parameters:
    //   x:
    //     The x-coordinate at which to retrieve node information
    //
    //   y:
    //     The y-coordinate at which to retrieve node information.
    //
    // Returns:
    //     A System.Windows.Forms.TreeViewHitTestInfo.
    //public TreeViewHitTestInfo HitTest(int x, int y);
    //
    // Summary:
    //     Determines whether the specified key is a regular input key or a special
    //     key that requires preprocessing.
    //
    // Parameters:
    //   keyData:
    //     One of the Keys values.
    //
    // Returns:
    //     true if the specified key is a regular input key; otherwise, false.
    //protected override bool IsInputKey(Keys keyData);
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.AfterCheck event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.TreeViewEventArgs that contains the event data.
    protected virtual void OnAfterCheck(TreeViewEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.AfterCollapse event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.TreeViewEventArgs that contains the event data.
    protected internal virtual void OnAfterCollapse(TreeViewEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.AfterExpand event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.TreeViewEventArgs that contains the event data.
    protected virtual void OnAfterExpand(TreeViewEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.AfterLabelEdit event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.NodeLabelEditEventArgs that contains the event data.
    protected virtual void OnAfterLabelEdit(NodeLabelEditEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.AfterSelect event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.TreeViewEventArgs that contains the event data.
    protected virtual void OnAfterSelect(TreeViewEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.BeforeCheck event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.TreeViewCancelEventArgs that contains the event data.
    protected virtual void OnBeforeCheck(TreeViewCancelEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.BeforeCollapse event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.TreeViewCancelEventArgs that contains the event data.
    protected internal virtual void OnBeforeCollapse(TreeViewCancelEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.BeforeExpand event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.TreeViewCancelEventArgs that contains the event data.
    protected virtual void OnBeforeExpand(TreeViewCancelEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.BeforeLabelEdit event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.NodeLabelEditEventArgs that contains the event data.
    protected virtual void OnBeforeLabelEdit(NodeLabelEditEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.BeforeSelect event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.TreeViewCancelEventArgs that contains the event data.
    protected virtual void OnBeforeSelect(TreeViewCancelEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.DrawNode event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.DrawTreeNodeEventArgs that contains the event data.
    //protected virtual void OnDrawNode(DrawTreeNodeEventArgs e);
    //
    // Summary:
    //     Overrides System.Windows.Forms.Control.OnHandleCreated(System.EventArgs).
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnHandleCreated(EventArgs e);
    //
    // Summary:
    //     Overrides System.Windows.Forms.Control.OnHandleDestroyed(System.EventArgs).
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnHandleDestroyed(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.ItemDrag event.
    //
    // Parameters:
    //   e:
    //     An System.Windows.Forms.ItemDragEventArgs that contains the event data.
    protected virtual void OnItemDrag(ItemDragEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.KeyDown event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.KeyEventArgs that contains the event data.
    //protected override void OnKeyDown(KeyEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.KeyPress event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.KeyPressEventArgs that contains the event data.
    //protected override void OnKeyPress(KeyPressEventArgs e);
    //
    // Summary:
    //     Overrides System.Windows.Forms.Control.OnKeyUp(System.Windows.Forms.KeyEventArgs).
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.KeyEventArgs that contains the event data.
    //protected override void OnKeyUp(KeyEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseHover event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnMouseHover(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseLeave event.
    //
    // Parameters:
    //   e:
    //     A System.EventArgs that contains the event data.
    //protected override void OnMouseLeave(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.NodeMouseClick event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.TreeNodeMouseClickEventArgs that contains the event
    //     data.
    protected virtual void OnNodeMouseClick(TreeNodeMouseClickEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.NodeMouseDoubleClick event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.TreeNodeMouseClickEventArgs that contains the event
    //     data.
    protected virtual void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.NodeMouseHover event.
    //
    // Parameters:
    //   e:
    //     The System.Windows.Forms.TreeNodeMouseHoverEventArgs that contains the event
    //     data.
    //protected virtual void OnNodeMouseHover(TreeNodeMouseHoverEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.TreeView.RightToLeftLayoutChanged event.
    //
    // Parameters:
    //   e:
    //     A System.EventArgs that contains the event data.
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnRightToLeftLayoutChanged(EventArgs e) {
      Contract.Requires(e != null);
    }
    //
    // Summary:
    //     Sorts the items in System.Windows.Forms.TreeView control.
    //public void Sort();
    //
    // Summary:
    //     Overrides System.ComponentModel.Component.ToString().
    //public override string ToString();
    //
    // Summary:
    //     Overrides System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@).
    //
    // Parameters:
    //   m:
    //     The Windows System.Windows.Forms.Message to process.
    //protected override void WndProc(ref Message m);
  }
}
