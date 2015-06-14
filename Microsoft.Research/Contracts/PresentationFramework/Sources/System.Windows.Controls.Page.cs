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

// File System.Windows.Controls.Page.cs
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
  public partial class Page : System.Windows.FrameworkElement, System.Windows.IWindowService, System.Windows.Markup.IAddChild
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeBounds)
    {
      return default(System.Windows.Size);
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    protected virtual new void OnTemplateChanged(ControlTemplate oldTemplate, ControlTemplate newTemplate)
    {
    }

    protected sealed override void OnVisualParentChanged(System.Windows.DependencyObject oldParent)
    {
    }

    public Page()
    {
    }

    public bool ShouldSerializeShowsNavigationUI()
    {
      return default(bool);
    }

    public bool ShouldSerializeTitle()
    {
      return default(bool);
    }

    public bool ShouldSerializeWindowHeight()
    {
      return default(bool);
    }

    public bool ShouldSerializeWindowTitle()
    {
      return default(bool);
    }

    public bool ShouldSerializeWindowWidth()
    {
      return default(bool);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object obj)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string str)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Media.Brush Background
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    public Object Content
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public System.Windows.Media.FontFamily FontFamily
    {
      get
      {
        return default(System.Windows.Media.FontFamily);
      }
      set
      {
      }
    }

    public double FontSize
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.Media.Brush Foreground
    {
      get
      {
        return default(System.Windows.Media.Brush);
      }
      set
      {
      }
    }

    public bool KeepAlive
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    internal protected override System.Collections.IEnumerator LogicalChildren
    {
      get
      {
        return default(System.Collections.IEnumerator);
      }
    }

    public System.Windows.Navigation.NavigationService NavigationService
    {
      get
      {
        return default(System.Windows.Navigation.NavigationService);
      }
    }

    public bool ShowsNavigationUI
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    double System.Windows.IWindowService.Height
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    string System.Windows.IWindowService.Title
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    bool System.Windows.IWindowService.UserResized
    {
      get
      {
        return default(bool);
      }
    }

    double System.Windows.IWindowService.Width
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public ControlTemplate Template
    {
      get
      {
        return default(ControlTemplate);
      }
      set
      {
      }
    }

    public string Title
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public double WindowHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public string WindowTitle
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public double WindowWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty BackgroundProperty;
    public readonly static System.Windows.DependencyProperty ContentProperty;
    public readonly static System.Windows.DependencyProperty FontFamilyProperty;
    public readonly static System.Windows.DependencyProperty FontSizeProperty;
    public readonly static System.Windows.DependencyProperty ForegroundProperty;
    public readonly static System.Windows.DependencyProperty KeepAliveProperty;
    public readonly static System.Windows.DependencyProperty TemplateProperty;
    public readonly static System.Windows.DependencyProperty TitleProperty;
    #endregion
  }
}
