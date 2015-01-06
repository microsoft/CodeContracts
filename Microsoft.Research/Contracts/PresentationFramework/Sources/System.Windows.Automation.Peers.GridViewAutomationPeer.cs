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

// File System.Windows.Automation.Peers.GridViewAutomationPeer.cs
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
  public partial class GridViewAutomationPeer : IViewAutomationPeer, System.Windows.Automation.Provider.ITableProvider, System.Windows.Automation.Provider.IGridProvider
  {
    #region Methods and constructors
    public GridViewAutomationPeer(System.Windows.Controls.GridView owner, System.Windows.Controls.ListView listview)
    {
      Contract.Requires(listview != null);
      Contract.Requires(owner != null);
    }

    ItemAutomationPeer System.Windows.Automation.Peers.IViewAutomationPeer.CreateItemAutomationPeer(Object item)
    {
      return default(ItemAutomationPeer);
    }

    AutomationControlType System.Windows.Automation.Peers.IViewAutomationPeer.GetAutomationControlType()
    {
      return default(AutomationControlType);
    }

    List<AutomationPeer> System.Windows.Automation.Peers.IViewAutomationPeer.GetChildren(List<AutomationPeer> children)
    {
      return default(List<AutomationPeer>);
    }

    Object System.Windows.Automation.Peers.IViewAutomationPeer.GetPattern(PatternInterface patternInterface)
    {
      return default(Object);
    }

    void System.Windows.Automation.Peers.IViewAutomationPeer.ItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
    }

    void System.Windows.Automation.Peers.IViewAutomationPeer.ViewDetached()
    {
    }

    System.Windows.Automation.Provider.IRawElementProviderSimple System.Windows.Automation.Provider.IGridProvider.GetItem(int row, int column)
    {
      return default(System.Windows.Automation.Provider.IRawElementProviderSimple);
    }

    System.Windows.Automation.Provider.IRawElementProviderSimple[] System.Windows.Automation.Provider.ITableProvider.GetColumnHeaders()
    {
      return default(System.Windows.Automation.Provider.IRawElementProviderSimple[]);
    }

    System.Windows.Automation.Provider.IRawElementProviderSimple[] System.Windows.Automation.Provider.ITableProvider.GetRowHeaders()
    {
      return default(System.Windows.Automation.Provider.IRawElementProviderSimple[]);
    }
    #endregion

    #region Properties and indexers
    int System.Windows.Automation.Provider.IGridProvider.ColumnCount
    {
      get
      {
        return default(int);
      }
    }

    int System.Windows.Automation.Provider.IGridProvider.RowCount
    {
      get
      {
        return default(int);
      }
    }

    System.Windows.Automation.RowOrColumnMajor System.Windows.Automation.Provider.ITableProvider.RowOrColumnMajor
    {
      get
      {
        return default(System.Windows.Automation.RowOrColumnMajor);
      }
    }
    #endregion
  }
}
