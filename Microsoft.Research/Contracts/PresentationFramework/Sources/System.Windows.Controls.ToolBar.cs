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

// File System.Windows.Controls.ToolBar.cs
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
  public partial class ToolBar : HeaderedItemsControl
  {
    #region Methods and constructors
    public static bool GetIsOverflowItem(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static OverflowMode GetOverflowMode(System.Windows.DependencyObject element)
    {
      return default(OverflowMode);
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnLostMouseCapture(System.Windows.Input.MouseEventArgs e)
    {
    }

    protected override void PrepareContainerForItemOverride(System.Windows.DependencyObject element, Object item)
    {
    }

    public static void SetOverflowMode(System.Windows.DependencyObject element, OverflowMode mode)
    {
    }

    public ToolBar()
    {
    }
    #endregion

    #region Properties and indexers
    public int Band
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int BandIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static System.Windows.ResourceKey ButtonStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public static System.Windows.ResourceKey CheckBoxStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public static System.Windows.ResourceKey ComboBoxStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public bool HasOverflowItems
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsOverflowOpen
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public static System.Windows.ResourceKey MenuStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public Orientation Orientation
    {
      get
      {
        return default(Orientation);
      }
    }

    public static System.Windows.ResourceKey RadioButtonStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public static System.Windows.ResourceKey SeparatorStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public static System.Windows.ResourceKey TextBoxStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public static System.Windows.ResourceKey ToggleButtonStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty BandIndexProperty;
    public readonly static System.Windows.DependencyProperty BandProperty;
    public readonly static System.Windows.DependencyProperty HasOverflowItemsProperty;
    public readonly static System.Windows.DependencyProperty IsOverflowItemProperty;
    public readonly static System.Windows.DependencyProperty IsOverflowOpenProperty;
    public readonly static System.Windows.DependencyProperty OrientationProperty;
    public readonly static System.Windows.DependencyProperty OverflowModeProperty;
    #endregion
  }
}
