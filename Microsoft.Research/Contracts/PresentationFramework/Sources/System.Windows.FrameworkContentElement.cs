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

// File System.Windows.FrameworkContentElement.cs
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


namespace System.Windows
{
  public partial class FrameworkContentElement : ContentElement, IFrameworkInputElement, IInputElement, System.ComponentModel.ISupportInitialize, System.Windows.Markup.IQueryAmbient
  {
    #region Methods and constructors
    protected internal void AddLogicalChild(Object child)
    {
    }

    public virtual new void BeginInit()
    {
    }

    public void BeginStoryboard(System.Windows.Media.Animation.Storyboard storyboard, System.Windows.Media.Animation.HandoffBehavior handoffBehavior, bool isControllable)
    {
    }

    public void BeginStoryboard(System.Windows.Media.Animation.Storyboard storyboard, System.Windows.Media.Animation.HandoffBehavior handoffBehavior)
    {
    }

    public void BeginStoryboard(System.Windows.Media.Animation.Storyboard storyboard)
    {
    }

    public void BringIntoView()
    {
      Contract.Ensures(System.Windows.Rect.Empty.Width == System.Windows.Rect.Empty.Height);
      Contract.Ensures(System.Windows.Rect.Empty.X == System.Windows.Rect.Empty.Y);
    }

    public virtual new void EndInit()
    {
    }

    public Object FindName(string name)
    {
      return default(Object);
    }

    public Object FindResource(Object resourceKey)
    {
      return default(Object);
    }

    public FrameworkContentElement()
    {
    }

    public System.Windows.Data.BindingExpression GetBindingExpression(DependencyProperty dp)
    {
      return default(System.Windows.Data.BindingExpression);
    }

    protected override DependencyObject GetUIParentCore()
    {
      return default(DependencyObject);
    }

    public sealed override bool MoveFocus(System.Windows.Input.TraversalRequest request)
    {
      return default(bool);
    }

    protected virtual new void OnContextMenuClosing(System.Windows.Controls.ContextMenuEventArgs e)
    {
    }

    protected virtual new void OnContextMenuOpening(System.Windows.Controls.ContextMenuEventArgs e)
    {
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
    }

    protected virtual new void OnInitialized(EventArgs e)
    {
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
    }

    protected internal virtual new void OnStyleChanged(Style oldStyle, Style newStyle)
    {
    }

    protected virtual new void OnToolTipClosing(System.Windows.Controls.ToolTipEventArgs e)
    {
    }

    protected virtual new void OnToolTipOpening(System.Windows.Controls.ToolTipEventArgs e)
    {
    }

    public sealed override DependencyObject PredictFocus(System.Windows.Input.FocusNavigationDirection direction)
    {
      return default(DependencyObject);
    }

    public void RegisterName(string name, Object scopedElement)
    {
    }

    protected internal void RemoveLogicalChild(Object child)
    {
    }

    public System.Windows.Data.BindingExpressionBase SetBinding(DependencyProperty dp, System.Windows.Data.BindingBase binding)
    {
      return default(System.Windows.Data.BindingExpressionBase);
    }

    public System.Windows.Data.BindingExpression SetBinding(DependencyProperty dp, string path)
    {
      return default(System.Windows.Data.BindingExpression);
    }

    public void SetResourceReference(DependencyProperty dp, Object name)
    {
    }

    public bool ShouldSerializeResources()
    {
      Contract.Ensures(0 <= this.Resources.Count);

      return default(bool);
    }

    public bool ShouldSerializeStyle()
    {
      return default(bool);
    }

    bool System.Windows.Markup.IQueryAmbient.IsAmbientPropertyAvailable(string propertyName)
    {
      return default(bool);
    }

    public Object TryFindResource(Object resourceKey)
    {
      return default(Object);
    }

    public void UnregisterName(string name)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Data.BindingGroup BindingGroup
    {
      get
      {
        return default(System.Windows.Data.BindingGroup);
      }
      set
      {
      }
    }

    public System.Windows.Controls.ContextMenu ContextMenu
    {
      get
      {
        return default(System.Windows.Controls.ContextMenu);
      }
      set
      {
      }
    }

    public System.Windows.Input.Cursor Cursor
    {
      get
      {
        return default(System.Windows.Input.Cursor);
      }
      set
      {
      }
    }

    public Object DataContext
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    internal protected Object DefaultStyleKey
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public System.Windows.Style FocusVisualStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public bool ForceCursor
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Input.InputScope InputScope
    {
      get
      {
        return default(System.Windows.Input.InputScope);
      }
      set
      {
      }
    }

    public bool IsInitialized
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsLoaded
    {
      get
      {
        return default(bool);
      }
    }

    public System.Windows.Markup.XmlLanguage Language
    {
      get
      {
        return default(System.Windows.Markup.XmlLanguage);
      }
      set
      {
      }
    }

    internal protected virtual new System.Collections.IEnumerator LogicalChildren
    {
      get
      {
        return default(System.Collections.IEnumerator);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool OverridesDefaultStyle
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public DependencyObject Parent
    {
      get
      {
        return default(DependencyObject);
      }
    }

    public ResourceDictionary Resources
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceDictionary>() != null);

        return default(ResourceDictionary);
      }
      set
      {
      }
    }

    public Style Style
    {
      get
      {
        return default(Style);
      }
      set
      {
      }
    }

    public Object Tag
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public DependencyObject TemplatedParent
    {
      get
      {
        return default(DependencyObject);
      }
    }

    public Object ToolTip
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event System.Windows.Controls.ContextMenuEventHandler ContextMenuClosing
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Controls.ContextMenuEventHandler ContextMenuOpening
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DependencyPropertyChangedEventHandler DataContextChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Initialized
    {
      add
      {
      }
      remove
      {
      }
    }

    public event RoutedEventHandler Loaded
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<System.Windows.Data.DataTransferEventArgs> SourceUpdated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<System.Windows.Data.DataTransferEventArgs> TargetUpdated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Controls.ToolTipEventHandler ToolTipClosing
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Controls.ToolTipEventHandler ToolTipOpening
    {
      add
      {
      }
      remove
      {
      }
    }

    public event RoutedEventHandler Unloaded
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
    public readonly static DependencyProperty BindingGroupProperty;
    public readonly static RoutedEvent ContextMenuClosingEvent;
    public readonly static RoutedEvent ContextMenuOpeningEvent;
    public readonly static DependencyProperty ContextMenuProperty;
    public readonly static DependencyProperty CursorProperty;
    public readonly static DependencyProperty DataContextProperty;
    internal protected readonly static DependencyProperty DefaultStyleKeyProperty;
    public readonly static DependencyProperty FocusVisualStyleProperty;
    public readonly static DependencyProperty ForceCursorProperty;
    public readonly static DependencyProperty InputScopeProperty;
    public readonly static DependencyProperty LanguageProperty;
    public readonly static RoutedEvent LoadedEvent;
    public readonly static DependencyProperty NameProperty;
    public readonly static DependencyProperty OverridesDefaultStyleProperty;
    public readonly static DependencyProperty StyleProperty;
    public readonly static DependencyProperty TagProperty;
    public readonly static RoutedEvent ToolTipClosingEvent;
    public readonly static RoutedEvent ToolTipOpeningEvent;
    public readonly static DependencyProperty ToolTipProperty;
    public readonly static RoutedEvent UnloadedEvent;
    #endregion
  }
}
