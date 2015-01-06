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

// File System.Windows.Automation.Peers.cs
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


namespace System.Windows.Automation.Peers
{
  public enum AutomationControlType
  {
    Button = 0, 
    Calendar = 1, 
    CheckBox = 2, 
    ComboBox = 3, 
    Edit = 4, 
    Hyperlink = 5, 
    Image = 6, 
    ListItem = 7, 
    List = 8, 
    Menu = 9, 
    MenuBar = 10, 
    MenuItem = 11, 
    ProgressBar = 12, 
    RadioButton = 13, 
    ScrollBar = 14, 
    Slider = 15, 
    Spinner = 16, 
    StatusBar = 17, 
    Tab = 18, 
    TabItem = 19, 
    Text = 20, 
    ToolBar = 21, 
    ToolTip = 22, 
    Tree = 23, 
    TreeItem = 24, 
    Custom = 25, 
    Group = 26, 
    Thumb = 27, 
    DataGrid = 28, 
    DataItem = 29, 
    Document = 30, 
    SplitButton = 31, 
    Window = 32, 
    Pane = 33, 
    Header = 34, 
    HeaderItem = 35, 
    Table = 36, 
    TitleBar = 37, 
    Separator = 38, 
  }

  public enum AutomationEvents
  {
    ToolTipOpened = 0, 
    ToolTipClosed = 1, 
    MenuOpened = 2, 
    MenuClosed = 3, 
    AutomationFocusChanged = 4, 
    InvokePatternOnInvoked = 5, 
    SelectionItemPatternOnElementAddedToSelection = 6, 
    SelectionItemPatternOnElementRemovedFromSelection = 7, 
    SelectionItemPatternOnElementSelected = 8, 
    SelectionPatternOnInvalidated = 9, 
    TextPatternOnTextSelectionChanged = 10, 
    TextPatternOnTextChanged = 11, 
    AsyncContentLoaded = 12, 
    PropertyChanged = 13, 
    StructureChanged = 14, 
    InputReachedTarget = 15, 
    InputReachedOtherElement = 16, 
    InputDiscarded = 17, 
  }

  public enum AutomationOrientation
  {
    None = 0, 
    Horizontal = 1, 
    Vertical = 2, 
  }

  public enum PatternInterface
  {
    Invoke = 0, 
    Selection = 1, 
    Value = 2, 
    RangeValue = 3, 
    Scroll = 4, 
    ScrollItem = 5, 
    ExpandCollapse = 6, 
    Grid = 7, 
    GridItem = 8, 
    MultipleView = 9, 
    Window = 10, 
    SelectionItem = 11, 
    Dock = 12, 
    Table = 13, 
    TableItem = 14, 
    Toggle = 15, 
    Transform = 16, 
    Text = 17, 
    ItemContainer = 18, 
    VirtualizedItem = 19, 
    SynchronizedInput = 20, 
  }
}
