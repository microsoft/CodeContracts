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

// File System.Windows.Automation.Peers.DataGridCellItemAutomationPeer.cs
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
  sealed public partial class DataGridCellItemAutomationPeer : AutomationPeer, System.Windows.Automation.Provider.ITableItemProvider, System.Windows.Automation.Provider.IGridItemProvider, System.Windows.Automation.Provider.IInvokeProvider, System.Windows.Automation.Provider.IScrollItemProvider, System.Windows.Automation.Provider.ISelectionItemProvider, System.Windows.Automation.Provider.IValueProvider, System.Windows.Automation.Provider.IVirtualizedItemProvider
  {
    #region Methods and constructors
    public DataGridCellItemAutomationPeer(Object item, System.Windows.Controls.DataGridColumn dataGridColumn)
    {
    }

    protected override string GetAcceleratorKeyCore()
    {
      return default(string);
    }

    protected override string GetAccessKeyCore()
    {
      return default(string);
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return default(AutomationControlType);
    }

    protected override string GetAutomationIdCore()
    {
      return default(string);
    }

    protected override System.Windows.Rect GetBoundingRectangleCore()
    {
      return default(System.Windows.Rect);
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      return default(List<AutomationPeer>);
    }

    protected override string GetClassNameCore()
    {
      return default(string);
    }

    protected override System.Windows.Point GetClickablePointCore()
    {
      return default(System.Windows.Point);
    }

    protected override string GetHelpTextCore()
    {
      return default(string);
    }

    protected override string GetItemStatusCore()
    {
      return default(string);
    }

    protected override string GetItemTypeCore()
    {
      return default(string);
    }

    protected override AutomationPeer GetLabeledByCore()
    {
      return default(AutomationPeer);
    }

    protected override string GetNameCore()
    {
      return default(string);
    }

    protected override AutomationOrientation GetOrientationCore()
    {
      return default(AutomationOrientation);
    }

    public override Object GetPattern(PatternInterface patternInterface)
    {
      return default(Object);
    }

    protected override bool HasKeyboardFocusCore()
    {
      return default(bool);
    }

    protected override bool IsContentElementCore()
    {
      return default(bool);
    }

    protected override bool IsControlElementCore()
    {
      return default(bool);
    }

    protected override bool IsEnabledCore()
    {
      return default(bool);
    }

    protected override bool IsKeyboardFocusableCore()
    {
      return default(bool);
    }

    protected override bool IsOffscreenCore()
    {
      return default(bool);
    }

    protected override bool IsPasswordCore()
    {
      return default(bool);
    }

    protected override bool IsRequiredForFormCore()
    {
      return default(bool);
    }

    protected override void SetFocusCore()
    {
    }

    void System.Windows.Automation.Provider.IInvokeProvider.Invoke()
    {
    }

    void System.Windows.Automation.Provider.IScrollItemProvider.ScrollIntoView()
    {
    }

    void System.Windows.Automation.Provider.ISelectionItemProvider.AddToSelection()
    {
    }

    void System.Windows.Automation.Provider.ISelectionItemProvider.RemoveFromSelection()
    {
    }

    void System.Windows.Automation.Provider.ISelectionItemProvider.Select()
    {
    }

    System.Windows.Automation.Provider.IRawElementProviderSimple[] System.Windows.Automation.Provider.ITableItemProvider.GetColumnHeaderItems()
    {
      return default(System.Windows.Automation.Provider.IRawElementProviderSimple[]);
    }

    System.Windows.Automation.Provider.IRawElementProviderSimple[] System.Windows.Automation.Provider.ITableItemProvider.GetRowHeaderItems()
    {
      return default(System.Windows.Automation.Provider.IRawElementProviderSimple[]);
    }

    void System.Windows.Automation.Provider.IValueProvider.SetValue(string value)
    {
    }

    void System.Windows.Automation.Provider.IVirtualizedItemProvider.Realize()
    {
    }
    #endregion

    #region Properties and indexers
    int System.Windows.Automation.Provider.IGridItemProvider.Column
    {
      get
      {
        return default(int);
      }
    }

    int System.Windows.Automation.Provider.IGridItemProvider.ColumnSpan
    {
      get
      {
        return default(int);
      }
    }

    System.Windows.Automation.Provider.IRawElementProviderSimple System.Windows.Automation.Provider.IGridItemProvider.ContainingGrid
    {
      get
      {
        return default(System.Windows.Automation.Provider.IRawElementProviderSimple);
      }
    }

    int System.Windows.Automation.Provider.IGridItemProvider.Row
    {
      get
      {
        return default(int);
      }
    }

    int System.Windows.Automation.Provider.IGridItemProvider.RowSpan
    {
      get
      {
        return default(int);
      }
    }

    bool System.Windows.Automation.Provider.ISelectionItemProvider.IsSelected
    {
      get
      {
        return default(bool);
      }
    }

    System.Windows.Automation.Provider.IRawElementProviderSimple System.Windows.Automation.Provider.ISelectionItemProvider.SelectionContainer
    {
      get
      {
        return default(System.Windows.Automation.Provider.IRawElementProviderSimple);
      }
    }

    bool System.Windows.Automation.Provider.IValueProvider.IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    string System.Windows.Automation.Provider.IValueProvider.Value
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
