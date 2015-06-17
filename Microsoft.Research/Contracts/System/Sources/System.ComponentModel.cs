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

// File System.ComponentModel.cs
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


namespace System.ComponentModel
{
  public delegate void AddingNewEventHandler(Object sender, AddingNewEventArgs e);

  public delegate void AsyncCompletedEventHandler(Object sender, AsyncCompletedEventArgs e);

  public enum BindableSupport
  {
    No = 0, 
    Yes = 1, 
    Default = 2, 
  }

  public enum BindingDirection
  {
    OneWay = 0, 
    TwoWay = 1, 
  }

  public delegate void CancelEventHandler(Object sender, CancelEventArgs e);

  public enum CollectionChangeAction
  {
    Add = 1, 
    Remove = 2, 
    Refresh = 3, 
  }

  public delegate void CollectionChangeEventHandler(Object sender, CollectionChangeEventArgs e);

  public enum DataObjectMethodType
  {
    Fill = 0, 
    Select = 1, 
    Update = 2, 
    Insert = 3, 
    Delete = 4, 
  }

  public enum DesignerSerializationVisibility
  {
    Hidden = 0, 
    Visible = 1, 
    Content = 2, 
  }

  public delegate void DoWorkEventHandler(Object sender, DoWorkEventArgs e);

  public enum EditorBrowsableState
  {
    Always = 0, 
    Never = 1, 
    Advanced = 2, 
  }

  public delegate void HandledEventHandler(Object sender, HandledEventArgs e);

  public enum InheritanceLevel
  {
    Inherited = 1, 
    InheritedReadOnly = 2, 
    NotInherited = 3, 
  }

  public enum LicenseUsageMode
  {
    Runtime = 0, 
    Designtime = 1, 
  }

  public delegate void ListChangedEventHandler(Object sender, ListChangedEventArgs e);

  public enum ListChangedType
  {
    Reset = 0, 
    ItemAdded = 1, 
    ItemDeleted = 2, 
    ItemMoved = 3, 
    ItemChanged = 4, 
    PropertyDescriptorAdded = 5, 
    PropertyDescriptorDeleted = 6, 
    PropertyDescriptorChanged = 7, 
  }

  public enum ListSortDirection
  {
    Ascending = 0, 
    Descending = 1, 
  }

  public enum MaskedTextResultHint
  {
    Unknown = 0, 
    CharacterEscaped = 1, 
    NoEffect = 2, 
    SideEffect = 3, 
    Success = 4, 
    AsciiCharacterExpected = -1, 
    AlphanumericCharacterExpected = -2, 
    DigitExpected = -3, 
    LetterExpected = -4, 
    SignedDigitExpected = -5, 
    InvalidInput = -51, 
    PromptCharNotAllowed = -52, 
    UnavailableEditPosition = -53, 
    NonEditPosition = -54, 
    PositionOutOfRange = -55, 
  }

  public delegate void ProgressChangedEventHandler(Object sender, ProgressChangedEventArgs e);

  public delegate void PropertyChangedEventHandler(Object sender, PropertyChangedEventArgs e);

  public delegate void PropertyChangingEventHandler(Object sender, PropertyChangingEventArgs e);

  public enum PropertyTabScope
  {
    Static = 0, 
    Global = 1, 
    Document = 2, 
    Component = 3, 
  }

  public delegate void RefreshEventHandler(RefreshEventArgs e);

  public enum RefreshProperties
  {
    None = 0, 
    All = 1, 
    Repaint = 2, 
  }

  public delegate void RunWorkerCompletedEventHandler(Object sender, RunWorkerCompletedEventArgs e);

  public enum ToolboxItemFilterType
  {
    Allow = 0, 
    Custom = 1, 
    Prevent = 2, 
    Require = 3, 
  }
}
