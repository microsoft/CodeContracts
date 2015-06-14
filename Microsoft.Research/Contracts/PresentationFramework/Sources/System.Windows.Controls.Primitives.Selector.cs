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

// File System.Windows.Controls.Primitives.Selector.cs
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


namespace System.Windows.Controls.Primitives
{
  abstract public partial class Selector : System.Windows.Controls.ItemsControl
  {
    #region Methods and constructors
    public static void AddSelectedHandler(System.Windows.DependencyObject element, System.Windows.RoutedEventHandler handler)
    {
    }

    public static void AddUnselectedHandler(System.Windows.DependencyObject element, System.Windows.RoutedEventHandler handler)
    {
    }

    protected override void ClearContainerForItemOverride(System.Windows.DependencyObject element, Object item)
    {
    }

    public static bool GetIsSelected(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    public static bool GetIsSelectionActive(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    protected override void OnIsKeyboardFocusWithinChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
    }

    protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
    {
    }

    protected virtual new void OnSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e)
    {
    }

    protected override void PrepareContainerForItemOverride(System.Windows.DependencyObject element, Object item)
    {
    }

    public static void RemoveSelectedHandler(System.Windows.DependencyObject element, System.Windows.RoutedEventHandler handler)
    {
    }

    public static void RemoveUnselectedHandler(System.Windows.DependencyObject element, System.Windows.RoutedEventHandler handler)
    {
    }

    protected Selector()
    {
    }

    public static void SetIsSelected(System.Windows.DependencyObject element, bool isSelected)
    {
    }
    #endregion

    #region Properties and indexers
    public Nullable<bool> IsSynchronizedWithCurrentItem
    {
      get
      {
        return default(Nullable<bool>);
      }
      set
      {
      }
    }

    public int SelectedIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public Object SelectedItem
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public Object SelectedValue
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public string SelectedValuePath
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event System.Windows.Controls.SelectionChangedEventHandler SelectionChanged
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty IsSelectedProperty;
    public readonly static System.Windows.DependencyProperty IsSelectionActiveProperty;
    public readonly static System.Windows.DependencyProperty IsSynchronizedWithCurrentItemProperty;
    public readonly static System.Windows.RoutedEvent SelectedEvent;
    public readonly static System.Windows.DependencyProperty SelectedIndexProperty;
    public readonly static System.Windows.DependencyProperty SelectedItemProperty;
    public readonly static System.Windows.DependencyProperty SelectedValuePathProperty;
    public readonly static System.Windows.DependencyProperty SelectedValueProperty;
    public readonly static System.Windows.RoutedEvent SelectionChangedEvent;
    public readonly static System.Windows.RoutedEvent UnselectedEvent;
    #endregion
  }
}
