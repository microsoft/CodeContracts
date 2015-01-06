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

// File System.Windows.Controls.MenuItem.cs
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
  public partial class MenuItem : HeaderedItemsControl, System.Windows.Input.ICommandSource
  {
    #region Methods and constructors
    protected override System.Windows.DependencyObject GetContainerForItemOverride()
    {
      return default(System.Windows.DependencyObject);
    }

    protected override bool IsItemItsOwnContainerOverride(Object item)
    {
      return default(bool);
    }

    public MenuItem()
    {
    }

    protected override void OnAccessKey(System.Windows.Input.AccessKeyEventArgs e)
    {
    }

    public override void OnApplyTemplate()
    {
    }

    protected virtual new void OnChecked(System.Windows.RoutedEventArgs e)
    {
    }

    protected virtual new void OnClick()
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnGotKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
    {
    }

    protected override void OnInitialized(EventArgs e)
    {
    }

    protected override void OnIsKeyboardFocusWithinChanged(System.Windows.DependencyPropertyChangedEventArgs e)
    {
    }

    protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
    {
    }

    protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
    {
    }

    protected override void OnMouseRightButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnMouseRightButtonUp(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected virtual new void OnSubmenuClosed(System.Windows.RoutedEventArgs e)
    {
    }

    protected virtual new void OnSubmenuOpened(System.Windows.RoutedEventArgs e)
    {
    }

    protected virtual new void OnUnchecked(System.Windows.RoutedEventArgs e)
    {
    }

    protected override void OnVisualParentChanged(System.Windows.DependencyObject oldParent)
    {
    }

    protected override void PrepareContainerForItemOverride(System.Windows.DependencyObject element, Object item)
    {
    }

    protected override bool ShouldApplyItemContainerStyle(System.Windows.DependencyObject container, Object item)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Input.ICommand Command
    {
      get
      {
        return default(System.Windows.Input.ICommand);
      }
      set
      {
      }
    }

    public Object CommandParameter
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public System.Windows.IInputElement CommandTarget
    {
      get
      {
        return default(System.Windows.IInputElement);
      }
      set
      {
      }
    }

    internal protected override bool HandlesScrolling
    {
      get
      {
        return default(bool);
      }
    }

    public Object Icon
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public string InputGestureText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool IsCheckable
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsChecked
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected override bool IsEnabledCore
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsHighlighted
    {
      get
      {
        return default(bool);
      }
      protected set
      {
      }
    }

    public bool IsPressed
    {
      get
      {
        return default(bool);
      }
      protected set
      {
      }
    }

    public bool IsSubmenuOpen
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsSuspendingPopupAnimation
    {
      get
      {
        return default(bool);
      }
      internal set
      {
      }
    }

    public MenuItemRole Role
    {
      get
      {
        return default(MenuItemRole);
      }
    }

    public static System.Windows.ResourceKey SeparatorStyleKey
    {
      get
      {
        return default(System.Windows.ResourceKey);
      }
    }

    public bool StaysOpenOnClick
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public static System.Windows.ResourceKey SubmenuHeaderTemplateKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public static System.Windows.ResourceKey SubmenuItemTemplateKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public static System.Windows.ResourceKey TopLevelHeaderTemplateKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }

    public static System.Windows.ResourceKey TopLevelItemTemplateKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(System.Windows.ResourceKey);
      }
    }
    #endregion

    #region Events
    public event System.Windows.RoutedEventHandler Checked
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler Click
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler SubmenuClosed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler SubmenuOpened
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler Unchecked
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
    public readonly static System.Windows.RoutedEvent CheckedEvent;
    public readonly static System.Windows.RoutedEvent ClickEvent;
    public readonly static System.Windows.DependencyProperty CommandParameterProperty;
    public readonly static System.Windows.DependencyProperty CommandProperty;
    public readonly static System.Windows.DependencyProperty CommandTargetProperty;
    public readonly static System.Windows.DependencyProperty IconProperty;
    public readonly static System.Windows.DependencyProperty InputGestureTextProperty;
    public readonly static System.Windows.DependencyProperty IsCheckableProperty;
    public readonly static System.Windows.DependencyProperty IsCheckedProperty;
    public readonly static System.Windows.DependencyProperty IsHighlightedProperty;
    public readonly static System.Windows.DependencyProperty IsPressedProperty;
    public readonly static System.Windows.DependencyProperty IsSubmenuOpenProperty;
    public readonly static System.Windows.DependencyProperty IsSuspendingPopupAnimationProperty;
    public readonly static System.Windows.DependencyProperty RoleProperty;
    public readonly static System.Windows.DependencyProperty StaysOpenOnClickProperty;
    public readonly static System.Windows.RoutedEvent SubmenuClosedEvent;
    public readonly static System.Windows.RoutedEvent SubmenuOpenedEvent;
    public readonly static System.Windows.RoutedEvent UncheckedEvent;
    #endregion
  }
}
