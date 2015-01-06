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

// File System.Windows.Controls.WebBrowser.cs
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
  sealed public partial class WebBrowser : System.Windows.Interop.ActiveXHost
  {
    #region Methods and constructors
    public void GoBack()
    {
    }

    public void GoForward()
    {
    }

    public Object InvokeScript(string scriptName)
    {
      Contract.Ensures(!string.IsNullOrEmpty(scriptName));

      return default(Object);
    }

    public Object InvokeScript(string scriptName, Object[] args)
    {
      Contract.Ensures(!string.IsNullOrEmpty(scriptName));

      return default(Object);
    }

    public void Navigate(Uri source)
    {
    }

    public void Navigate(string source, string targetFrameName, byte[] postData, string additionalHeaders)
    {
      Contract.Requires(source != null);
    }

    public void Navigate(Uri source, string targetFrameName, byte[] postData, string additionalHeaders)
    {
    }

    public void Navigate(string source)
    {
      Contract.Requires(source != null);
    }

    public void NavigateToStream(Stream stream)
    {
    }

    public void NavigateToString(string text)
    {
      Contract.Requires(text != null);
      Contract.Ensures(!string.IsNullOrEmpty(text));
      Contract.Ensures(0 <= text.Length);
    }

    public void Refresh(bool noCache)
    {
    }

    public void Refresh()
    {
    }

    protected override bool TabIntoCore(System.Windows.Input.TraversalRequest request)
    {
      return default(bool);
    }

    protected override bool TranslateAcceleratorCore(ref System.Windows.Interop.MSG msg, System.Windows.Input.ModifierKeys modifiers)
    {
      return default(bool);
    }

    public WebBrowser() 
    {
    }
    #endregion

    #region Properties and indexers
    public bool CanGoBack
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanGoForward
    {
      get
      {
        return default(bool);
      }
    }

    public Object Document
    {
      get
      {
        return default(Object);
      }
    }

    public Object ObjectForScripting
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public Uri Source
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event System.Windows.Navigation.LoadCompletedEventHandler LoadCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Navigation.NavigatedEventHandler Navigated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Navigation.NavigatingCancelEventHandler Navigating
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
