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

// File System.Windows.Controls.Primitives.DocumentViewerBase.cs
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
  abstract public partial class DocumentViewerBase : System.Windows.Controls.Control, System.Windows.Markup.IAddChild, IServiceProvider
  {
    #region Methods and constructors
    public void CancelPrint()
    {
    }

    public virtual new bool CanGoToPage(int pageNumber)
    {
      return default(bool);
    }

    protected DocumentViewerBase()
    {
    }

    public void FirstPage()
    {
    }

    public static bool GetIsMasterPage(System.Windows.DependencyObject element)
    {
      return default(bool);
    }

    protected DocumentPageView GetMasterPageView()
    {
      return default(DocumentPageView);
    }

    protected virtual new System.Collections.ObjectModel.ReadOnlyCollection<DocumentPageView> GetPageViewsCollection(out bool changed)
    {
      changed = default(bool);

      return default(System.Collections.ObjectModel.ReadOnlyCollection<DocumentPageView>);
    }

    public void GoToPage(int pageNumber)
    {
    }

    protected void InvalidatePageViews()
    {
    }

    public void LastPage()
    {
    }

    public void NextPage()
    {
    }

    public override void OnApplyTemplate()
    {
    }

    protected virtual new void OnBringIntoView(System.Windows.DependencyObject element, System.Windows.Rect rect, int pageNumber)
    {
    }

    protected virtual new void OnCancelPrintCommand()
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected virtual new void OnDocumentChanged()
    {
    }

    protected virtual new void OnFirstPageCommand()
    {
    }

    protected virtual new void OnGoToPageCommand(int pageNumber)
    {
    }

    protected virtual new void OnLastPageCommand()
    {
    }

    protected virtual new void OnMasterPageNumberChanged()
    {
    }

    protected virtual new void OnNextPageCommand()
    {
    }

    protected virtual new void OnPageViewsChanged()
    {
    }

    protected virtual new void OnPreviousPageCommand()
    {
    }

    protected virtual new void OnPrintCommand()
    {
    }

    public void PreviousPage()
    {
    }

    public void Print()
    {
    }

    public static void SetIsMasterPage(System.Windows.DependencyObject element, bool value)
    {
    }

    Object System.IServiceProvider.GetService(Type serviceType)
    {
      return default(Object);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool CanGoToNextPage
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanGoToPreviousPage
    {
      get
      {
        return default(bool);
      }
    }

    public System.Windows.Documents.IDocumentPaginatorSource Document
    {
      get
      {
        return default(System.Windows.Documents.IDocumentPaginatorSource);
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

    public virtual new int MasterPageNumber
    {
      get
      {
        return default(int);
      }
    }

    public int PageCount
    {
      get
      {
        return default(int);
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<DocumentPageView> PageViews
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<DocumentPageView>);
      }
    }
    #endregion

    #region Events
    public event EventHandler PageViewsChanged
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
    public readonly static System.Windows.DependencyProperty CanGoToNextPageProperty;
    protected readonly static System.Windows.DependencyPropertyKey CanGoToNextPagePropertyKey;
    public readonly static System.Windows.DependencyProperty CanGoToPreviousPageProperty;
    protected readonly static System.Windows.DependencyPropertyKey CanGoToPreviousPagePropertyKey;
    public readonly static System.Windows.DependencyProperty DocumentProperty;
    public readonly static System.Windows.DependencyProperty IsMasterPageProperty;
    public readonly static System.Windows.DependencyProperty MasterPageNumberProperty;
    protected readonly static System.Windows.DependencyPropertyKey MasterPageNumberPropertyKey;
    public readonly static System.Windows.DependencyProperty PageCountProperty;
    protected readonly static System.Windows.DependencyPropertyKey PageCountPropertyKey;
    #endregion
  }
}
