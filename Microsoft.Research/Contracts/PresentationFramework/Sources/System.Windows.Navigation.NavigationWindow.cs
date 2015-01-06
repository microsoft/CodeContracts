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

// File System.Windows.Navigation.NavigationWindow.cs
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
  public partial class NavigationWindow : System.Windows.Window, MS.Internal.AppModel.INavigator, MS.Internal.AppModel.INavigatorImpl, MS.Internal.AppModel.IDownloader, MS.Internal.AppModel.IJournalNavigationScopeHost, MS.Internal.AppModel.INavigatorBase, System.Windows.Markup.IUriContext
  {
    #region Methods and constructors
    public void AddBackEntry(CustomContentState state)
    {
    }

    protected override void AddChild(Object value)
    {
    }

    protected override void AddText(string text)
    {
    }

    public void GoBack()
    {
    }

    public void GoForward()
    {
    }

    bool MS.Internal.AppModel.IJournalNavigationScopeHost.GoBackOverride()
    {
      return default(bool);
    }

    bool MS.Internal.AppModel.IJournalNavigationScopeHost.GoForwardOverride()
    {
      return default(bool);
    }

    void MS.Internal.AppModel.IJournalNavigationScopeHost.OnJournalAvailable()
    {
    }

    void MS.Internal.AppModel.IJournalNavigationScopeHost.VerifyContextAndObjectState()
    {
    }

    System.Windows.Media.Visual MS.Internal.AppModel.INavigatorImpl.FindRootViewer()
    {
      return default(System.Windows.Media.Visual);
    }

    void MS.Internal.AppModel.INavigatorImpl.OnSourceUpdatedFromNavService(bool journalOrCancel)
    {
    }

    public bool Navigate(Uri source)
    {
      return default(bool);
    }

    public bool Navigate(Object content, Object extraData)
    {
      return default(bool);
    }

    public bool Navigate(Uri source, Object extraData)
    {
      return default(bool);
    }

    public bool Navigate(Object content)
    {
      return default(bool);
    }

    public NavigationWindow()
    {
      Contract.Ensures(false);
    }

    public override void OnApplyTemplate()
    {
    }

    protected override void OnClosed(EventArgs args)
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    public void Refresh()
    {
    }

    public JournalEntry RemoveBackEntry()
    {
      return default(JournalEntry);
    }

    public override bool ShouldSerializeContent()
    {
      return default(bool);
    }

    public void StopLoading()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Collections.IEnumerable BackStack
    {
      get
      {
        return default(System.Collections.IEnumerable);
      }
    }

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

    public Uri CurrentSource
    {
      get
      {
        return default(Uri);
      }
    }

    public System.Collections.IEnumerable ForwardStack
    {
      get
      {
        return default(System.Collections.IEnumerable);
      }
    }

    NavigationService MS.Internal.AppModel.IDownloader.Downloader
    {
      get
      {
        return default(NavigationService);
      }
    }

    System.Windows.Navigation.NavigationService MS.Internal.AppModel.IJournalNavigationScopeHost.NavigationService
    {
      get
      {
        return default(System.Windows.Navigation.NavigationService);
      }
    }

    public NavigationService NavigationService
    {
      get
      {
        return default(NavigationService);
      }
    }

    public bool SandboxExternalContent
    {
      get
      {
        return default(bool);
      }
      set
      {
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

    #region Fields
    public readonly static System.Windows.DependencyProperty BackStackProperty;
    public readonly static System.Windows.DependencyProperty CanGoBackProperty;
    public readonly static System.Windows.DependencyProperty CanGoForwardProperty;
    public readonly static System.Windows.DependencyProperty ForwardStackProperty;
    public readonly static System.Windows.DependencyProperty SandboxExternalContentProperty;
    public readonly static System.Windows.DependencyProperty ShowsNavigationUIProperty;
    public readonly static System.Windows.DependencyProperty SourceProperty;
    #endregion
  }
}
