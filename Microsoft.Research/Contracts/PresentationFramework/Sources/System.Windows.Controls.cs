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

// File System.Windows.Controls.cs
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


namespace System.Windows.Controls
{
  public enum CalendarMode
  {
    Month = 0, 
    Year = 1, 
    Decade = 2, 
  }

  public enum CalendarSelectionMode
  {
    SingleDate = 0, 
    SingleRange = 1, 
    MultipleRange = 2, 
    None = 3, 
  }

  public enum CharacterCasing
  {
    Normal = 0, 
    Lower = 1, 
    Upper = 2, 
  }

  public delegate void CleanUpVirtualizedItemEventHandler(Object sender, CleanUpVirtualizedItemEventArgs e);

  public enum ClickMode
  {
    Release = 0, 
    Press = 1, 
    Hover = 2, 
  }

  public delegate void ContextMenuEventHandler(Object sender, ContextMenuEventArgs e);

  public enum DataGridClipboardCopyMode
  {
    None = 0, 
    ExcludeHeader = 1, 
    IncludeHeader = 2, 
  }

  public enum DataGridEditAction
  {
    Cancel = 0, 
    Commit = 1, 
  }

  public enum DataGridEditingUnit
  {
    Cell = 0, 
    Row = 1, 
  }

  public enum DataGridGridLinesVisibility
  {
    All = 0, 
    Horizontal = 1, 
    None = 2, 
    Vertical = 3, 
  }

  public enum DataGridHeadersVisibility
  {
    All = 3, 
    Column = 1, 
    Row = 2, 
    None = 0, 
  }

  public enum DataGridLengthUnitType
  {
    Auto = 0, 
    Pixel = 1, 
    SizeToCells = 2, 
    SizeToHeader = 3, 
    Star = 4, 
  }

  public enum DataGridRowDetailsVisibilityMode
  {
    Collapsed = 0, 
    Visible = 1, 
    VisibleWhenSelected = 2, 
  }

  public enum DataGridSelectionMode
  {
    Single = 0, 
    Extended = 1, 
  }

  public enum DataGridSelectionUnit
  {
    Cell = 0, 
    FullRow = 1, 
    CellOrRowHeader = 2, 
  }

  public delegate void DataGridSortingEventHandler(Object sender, DataGridSortingEventArgs e);

  public enum DatePickerFormat
  {
    Long = 0, 
    Short = 1, 
  }

  public enum Dock
  {
    Left = 0, 
    Top = 1, 
    Right = 2, 
    Bottom = 3, 
  }

  public enum ExpandDirection
  {
    Down = 0, 
    Up = 1, 
    Left = 2, 
    Right = 3, 
  }

  public enum FlowDocumentReaderViewingMode
  {
    Page = 0, 
    TwoPage = 1, 
    Scroll = 2, 
  }

  public enum GridResizeBehavior
  {
    BasedOnAlignment = 0, 
    CurrentAndNext = 1, 
    PreviousAndCurrent = 2, 
    PreviousAndNext = 3, 
  }

  public enum GridResizeDirection
  {
    Auto = 0, 
    Columns = 1, 
    Rows = 2, 
  }

  public enum GridViewColumnHeaderRole
  {
    Normal = 0, 
    Floating = 1, 
    Padding = 2, 
  }

  public delegate GroupStyle GroupStyleSelector(System.Windows.Data.CollectionViewGroup group, int level);

  public delegate void InitializingNewItemEventHandler(Object sender, InitializingNewItemEventArgs e);

  public enum InkCanvasClipboardFormat
  {
    InkSerializedFormat = 0, 
    Text = 1, 
    Xaml = 2, 
  }

  public enum InkCanvasEditingMode
  {
    None = 0, 
    Ink = 1, 
    GestureOnly = 2, 
    InkAndGesture = 3, 
    Select = 4, 
    EraseByPoint = 5, 
    EraseByStroke = 6, 
  }

  public delegate void InkCanvasGestureEventHandler(Object sender, InkCanvasGestureEventArgs e);

  public delegate void InkCanvasSelectionChangingEventHandler(Object sender, InkCanvasSelectionChangingEventArgs e);

  public delegate void InkCanvasSelectionEditingEventHandler(Object sender, InkCanvasSelectionEditingEventArgs e);

  public enum InkCanvasSelectionHitResult
  {
    None = 0, 
    TopLeft = 1, 
    Top = 2, 
    TopRight = 3, 
    Right = 4, 
    BottomRight = 5, 
    Bottom = 6, 
    BottomLeft = 7, 
    Left = 8, 
    Selection = 9, 
  }

  public delegate void InkCanvasStrokeCollectedEventHandler(Object sender, InkCanvasStrokeCollectedEventArgs e);

  public delegate void InkCanvasStrokeErasingEventHandler(Object sender, InkCanvasStrokeErasingEventArgs e);

  public delegate void InkCanvasStrokesReplacedEventHandler(Object sender, InkCanvasStrokesReplacedEventArgs e);

  public enum MediaState
  {
    Manual = 0, 
    Play = 1, 
    Close = 2, 
    Pause = 3, 
    Stop = 4, 
  }

  public enum MenuItemRole
  {
    TopLevelItem = 0, 
    TopLevelHeader = 1, 
    SubmenuItem = 2, 
    SubmenuHeader = 3, 
  }

  public enum Orientation
  {
    Horizontal = 0, 
    Vertical = 1, 
  }

  public enum OverflowMode
  {
    AsNeeded = 0, 
    Always = 1, 
    Never = 2, 
  }

  public enum PageRangeSelection
  {
    AllPages = 0, 
    UserPages = 1, 
  }

  public enum PanningMode
  {
    None = 0, 
    HorizontalOnly = 1, 
    VerticalOnly = 2, 
    Both = 3, 
    HorizontalFirst = 4, 
    VerticalFirst = 5, 
  }

  public enum ScrollBarVisibility
  {
    Disabled = 0, 
    Auto = 1, 
    Hidden = 2, 
    Visible = 3, 
  }

  public delegate void ScrollChangedEventHandler(Object sender, ScrollChangedEventArgs e);

  public delegate void SelectedCellsChangedEventHandler(Object sender, SelectedCellsChangedEventArgs e);

  public delegate void SelectionChangedEventHandler(Object sender, SelectionChangedEventArgs e);

  public enum SelectionMode
  {
    Single = 0, 
    Multiple = 1, 
    Extended = 2, 
  }

  public enum SelectiveScrollingOrientation
  {
    None = 0, 
    Horizontal = 1, 
    Vertical = 2, 
    Both = 3, 
  }

  public enum SpellingReform
  {
    PreAndPostreform = 0, 
    Prereform = 1, 
    Postreform = 2, 
  }

  public enum StickyNoteType
  {
    Text = 0, 
    Ink = 1, 
  }

  public enum StretchDirection
  {
    UpOnly = 0, 
    DownOnly = 1, 
    Both = 2, 
  }

  public delegate void TextChangedEventHandler(Object sender, TextChangedEventArgs e);

  public delegate void ToolTipEventHandler(Object sender, ToolTipEventArgs e);

  public enum UndoAction
  {
    None = 0, 
    Merge = 1, 
    Undo = 2, 
    Redo = 3, 
    Clear = 4, 
    Create = 5, 
  }

  public enum ValidationErrorEventAction
  {
    Added = 0, 
    Removed = 1, 
  }

  public enum ValidationStep
  {
    RawProposedValue = 0, 
    ConvertedProposedValue = 1, 
    UpdatedValue = 2, 
    CommittedValue = 3, 
  }

  public enum VirtualizationMode
  {
    Standard = 0, 
    Recycling = 1, 
  }
}
