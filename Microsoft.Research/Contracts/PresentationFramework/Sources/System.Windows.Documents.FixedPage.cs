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

// File System.Windows.Documents.FixedPage.cs
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


namespace System.Windows.Documents
{
  sealed public partial class FixedPage : System.Windows.FrameworkElement, System.Windows.Markup.IAddChild, IFixedNavigate, System.Windows.Markup.IUriContext
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
    {
      return default(System.Windows.Size);
    }

    public FixedPage()
    {
    }

    public static double GetBottom(System.Windows.UIElement element)
    {
      return default(double);
    }

    public static double GetLeft(System.Windows.UIElement element)
    {
      return default(double);
    }

    public static Uri GetNavigateUri(System.Windows.UIElement element)
    {
      return default(Uri);
    }

    public static double GetRight(System.Windows.UIElement element)
    {
      return default(double);
    }

    public static double GetTop(System.Windows.UIElement element)
    {
      return default(double);
    }

    protected override System.Windows.Media.Visual GetVisualChild(int index)
    {
      return default(System.Windows.Media.Visual);
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnPreviewMouseWheel(System.Windows.Input.MouseWheelEventArgs e)
    {
    }

    protected override void OnRender(System.Windows.Media.DrawingContext dc)
    {
    }

    protected override void OnVisualParentChanged(System.Windows.DependencyObject oldParent)
    {
    }

    public static void SetBottom(System.Windows.UIElement element, double length)
    {
    }

    public static void SetLeft(System.Windows.UIElement element, double length)
    {
    }

    public static void SetNavigateUri(System.Windows.UIElement element, Uri uri)
    {
    }

    public static void SetRight(System.Windows.UIElement element, double length)
    {
    }

    public static void SetTop(System.Windows.UIElement element, double length)
    {
    }

    System.Windows.UIElement System.Windows.Documents.IFixedNavigate.FindElementByID(string elementID, out System.Windows.Documents.FixedPage rootFixedPage)
    {
      rootFixedPage = default(System.Windows.Documents.FixedPage);

      return default(System.Windows.UIElement);
    }

    void System.Windows.Documents.IFixedNavigate.NavigateAsync(string elementID)
    {
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
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

    public System.Windows.Rect BleedBox
    {
      get
      {
        return default(System.Windows.Rect);
      }
      set
      {
      }
    }

    public System.Windows.Controls.UIElementCollection Children
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Controls.UIElementCollection>() != null);

        return default(System.Windows.Controls.UIElementCollection);
      }
    }

    public System.Windows.Rect ContentBox
    {
      get
      {
        return default(System.Windows.Rect);
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

    public Object PrintTicket
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    Uri System.Windows.Markup.IUriContext.BaseUri
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    protected override int VisualChildrenCount
    {
      get
      {
        return default(int);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty BackgroundProperty;
    public readonly static System.Windows.DependencyProperty BleedBoxProperty;
    public readonly static System.Windows.DependencyProperty BottomProperty;
    public readonly static System.Windows.DependencyProperty ContentBoxProperty;
    public readonly static System.Windows.DependencyProperty LeftProperty;
    public readonly static System.Windows.DependencyProperty NavigateUriProperty;
    public readonly static System.Windows.DependencyProperty PrintTicketProperty;
    public readonly static System.Windows.DependencyProperty RightProperty;
    public readonly static System.Windows.DependencyProperty TopProperty;
    #endregion
  }
}
