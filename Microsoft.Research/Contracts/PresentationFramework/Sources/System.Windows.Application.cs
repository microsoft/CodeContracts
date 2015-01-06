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

// File System.Windows.Application.cs
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
  public partial class Application : System.Windows.Threading.DispatcherObject, System.Windows.Markup.IHaveResources, System.Windows.Markup.IQueryAmbient
  {
    #region Methods and constructors
    public Application()
    {
    }

    public Object FindResource(Object resourceKey)
    {
      return default(Object);
    }

    public static System.Windows.Resources.StreamResourceInfo GetContentStream(Uri uriContent)
    {
      return default(System.Windows.Resources.StreamResourceInfo);
    }

    public static string GetCookie(Uri uri)
    {
      return default(string);
    }

    public static System.Windows.Resources.StreamResourceInfo GetRemoteStream(Uri uriRemote)
    {
      return default(System.Windows.Resources.StreamResourceInfo);
    }

    public static System.Windows.Resources.StreamResourceInfo GetResourceStream(Uri uriResource)
    {
      return default(System.Windows.Resources.StreamResourceInfo);
    }

    public static Object LoadComponent(Uri resourceLocator)
    {
      Contract.Requires(resourceLocator != null);
      Contract.Requires(resourceLocator.OriginalString != null);
      Contract.Requires(!resourceLocator.IsAbsoluteUri);
      Contract.Ensures(Contract.Result<Object>() != null);

      return default(Object);
    }

    public static void LoadComponent(Object component, Uri resourceLocator)
    {
      Contract.Requires(component != null);
      Contract.Requires(resourceLocator != null);
      Contract.Requires(resourceLocator.OriginalString != null);
      Contract.Requires(!resourceLocator.IsAbsoluteUri);
    }

    protected virtual new void OnActivated(EventArgs e)
    {
    }

    protected virtual new void OnDeactivated(EventArgs e)
    {
    }

    protected virtual new void OnExit(ExitEventArgs e)
    {
    }

    protected virtual new void OnFragmentNavigation(System.Windows.Navigation.FragmentNavigationEventArgs e)
    {
    }

    protected virtual new void OnLoadCompleted(System.Windows.Navigation.NavigationEventArgs e)
    {
    }

    protected virtual new void OnNavigated(System.Windows.Navigation.NavigationEventArgs e)
    {
    }

    protected virtual new void OnNavigating(System.Windows.Navigation.NavigatingCancelEventArgs e)
    {
    }

    protected virtual new void OnNavigationFailed(System.Windows.Navigation.NavigationFailedEventArgs e)
    {
    }

    protected virtual new void OnNavigationProgress(System.Windows.Navigation.NavigationProgressEventArgs e)
    {
    }

    protected virtual new void OnNavigationStopped(System.Windows.Navigation.NavigationEventArgs e)
    {
    }

    protected virtual new void OnSessionEnding(SessionEndingCancelEventArgs e)
    {
    }

    protected virtual new void OnStartup(StartupEventArgs e)
    {
    }

    public int Run(Window window)
    {
      return default(int);
    }

    public int Run()
    {
      return default(int);
    }

    public static void SetCookie(Uri uri, string value)
    {
    }

    public void Shutdown(int exitCode)
    {
    }

    public void Shutdown()
    {
    }

    bool System.Windows.Markup.IQueryAmbient.IsAmbientPropertyAvailable(string propertyName)
    {
      return default(bool);
    }

    public Object TryFindResource(Object resourceKey)
    {
      return default(Object);
    }
    #endregion

    #region Properties and indexers
    public static System.Windows.Application Current
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Application>() != null);

        return default(System.Windows.Application);
      }
    }

    public Window MainWindow
    {
      get
      {
        return default(Window);
      }
      set
      {
      }
    }

    public System.Collections.IDictionary Properties
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.IDictionary>() != null);

        return default(System.Collections.IDictionary);
      }
    }

    public static System.Reflection.Assembly ResourceAssembly
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Reflection.Assembly>() != null);

        return default(System.Reflection.Assembly);
      }
      set
      {
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

    public ShutdownMode ShutdownMode
    {
      get
      {
        return default(ShutdownMode);
      }
      set
      {
      }
    }

    public Uri StartupUri
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    ResourceDictionary System.Windows.Markup.IHaveResources.Resources
    {
      get
      {
        return default(ResourceDictionary);
      }
      set
      {
      }
    }

    public WindowCollection Windows
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.WindowCollection>() != null);

        return default(WindowCollection);
      }
    }
    #endregion

    #region Events
    public event EventHandler Activated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Deactivated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Threading.DispatcherUnhandledExceptionEventHandler DispatcherUnhandledException
    {
      add
      {
      }
      remove
      {
      }
    }

    public event ExitEventHandler Exit
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Navigation.FragmentNavigationEventHandler FragmentNavigation
    {
      add
      {
      }
      remove
      {
      }
    }

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

    public event System.Windows.Navigation.NavigationFailedEventHandler NavigationFailed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Navigation.NavigationProgressEventHandler NavigationProgress
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Navigation.NavigationStoppedEventHandler NavigationStopped
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SessionEndingCancelEventHandler SessionEnding
    {
      add
      {
      }
      remove
      {
      }
    }

    public event StartupEventHandler Startup
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
