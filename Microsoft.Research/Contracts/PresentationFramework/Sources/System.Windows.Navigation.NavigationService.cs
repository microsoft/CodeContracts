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

// File System.Windows.Navigation.NavigationService.cs
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


namespace System.Windows.Navigation
{
  sealed public partial class NavigationService : MS.Internal.AppModel.IContentContainer
  {
    #region Methods and constructors
    public void AddBackEntry(CustomContentState state)
    {
    }

    public static NavigationService GetNavigationService(System.Windows.DependencyObject dependencyObject)
    {
      return default(NavigationService);
    }

    public void GoBack()
    {
    }

    public void GoForward()
    {
    }

    void MS.Internal.AppModel.IContentContainer.OnNavigationProgress(Uri sourceUri, long bytesRead, long maxBytes)
    {
    }

    void MS.Internal.AppModel.IContentContainer.OnStreamClosed(Uri sourceUri)
    {
    }

    public bool Navigate(Uri source, Object navigationState)
    {
      return default(bool);
    }

    public bool Navigate(Object root)
    {
      return default(bool);
    }

    public bool Navigate(Uri source)
    {
      return default(bool);
    }

    public bool Navigate(Object root, Object navigationState)
    {
      return default(bool);
    }

    public bool Navigate(Uri source, Object navigationState, bool sandboxExternalContent)
    {
      return default(bool);
    }

    internal NavigationService()
    {
    }

    public void Refresh()
    {
    }

    public JournalEntry RemoveBackEntry()
    {
      return default(JournalEntry);
    }

    public void StopLoading()
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

    public Uri CurrentSource
    {
      get
      {
        return default(Uri);
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
    public event FragmentNavigationEventHandler FragmentNavigation
    {
      add
      {
      }
      remove
      {
      }
    }

    public event LoadCompletedEventHandler LoadCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event NavigatedEventHandler Navigated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event NavigatingCancelEventHandler Navigating
    {
      add
      {
      }
      remove
      {
      }
    }

    public event NavigationFailedEventHandler NavigationFailed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event NavigationProgressEventHandler NavigationProgress
    {
      add
      {
      }
      remove
      {
      }
    }

    public event NavigationStoppedEventHandler NavigationStopped
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
