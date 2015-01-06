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

// File System.Windows.Interop.DocObjHost.cs
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


namespace System.Windows.Interop
{
  sealed public partial class DocObjHost : MarshalByRefObject, IServiceProvider, MS.Internal.AppModel.IHostService, MS.Internal.AppModel.IBrowserHostServices, MS.Internal.Progressivity.IByteRangeDownloaderService
  {
    #region Methods and constructors
    public DocObjHost()
    {
    }

    public override Object InitializeLifetimeService()
    {
      return default(Object);
    }

    void MS.Internal.AppModel.IBrowserHostServices.Activate(bool fActivate)
    {
    }

    bool MS.Internal.AppModel.IBrowserHostServices.CanInvokeJournalEntry(int entryId)
    {
      return default(bool);
    }

    int MS.Internal.AppModel.IBrowserHostServices.ExecCommand(Guid guidCommandGroup, uint command, Object arg)
    {
      return default(int);
    }

    bool MS.Internal.AppModel.IBrowserHostServices.FocusedElementWantsBackspace()
    {
      return default(bool);
    }

    int MS.Internal.AppModel.IBrowserHostServices.GetApplicationExitCode()
    {
      return default(int);
    }

    bool MS.Internal.AppModel.IBrowserHostServices.IsAppLoaded()
    {
      return default(bool);
    }

    void MS.Internal.AppModel.IBrowserHostServices.LoadHistory(Object ucomIStream)
    {
    }

    void MS.Internal.AppModel.IBrowserHostServices.Move(int x, int y, int width, int height)
    {
    }

    void MS.Internal.AppModel.IBrowserHostServices.PostShutdown()
    {
    }

    int MS.Internal.AppModel.IBrowserHostServices.QueryStatus(Guid guidCmdGroup, uint command, out uint flags)
    {
      flags = default(uint);

      return default(int);
    }

    void MS.Internal.AppModel.IBrowserHostServices.SaveHistory(Object comIStream, bool persistEntireJournal, out int entryIndex, out string uri, out string title)
    {
      entryIndex = default(int);
      uri = default(string);
      title = default(string);
    }

    void MS.Internal.AppModel.IBrowserHostServices.SetBrowserCallback(Object browserCallbackServices)
    {
    }

    void MS.Internal.AppModel.IBrowserHostServices.SetParent(IntPtr hParent)
    {
    }

    void MS.Internal.AppModel.IBrowserHostServices.Show(bool show)
    {
    }

    void MS.Internal.AppModel.IBrowserHostServices.TabInto(bool forward)
    {
    }

    void MS.Internal.Progressivity.IByteRangeDownloaderService.GetDownloadedByteRanges(out int[] byteRanges, out int size)
    {
      byteRanges = default(int[]);
      size = default(int);
    }

    void MS.Internal.Progressivity.IByteRangeDownloaderService.InitializeByteRangeDownloader(string url, string tempFile, Microsoft.Win32.SafeHandles.SafeWaitHandle eventHandle)
    {
    }

    void MS.Internal.Progressivity.IByteRangeDownloaderService.ReleaseByteRangeDownloader()
    {
    }

    void MS.Internal.Progressivity.IByteRangeDownloaderService.RequestDownloadByteRanges(int[] byteRanges, int size)
    {
    }

    Object System.IServiceProvider.GetService(Type serviceType)
    {
      return default(Object);
    }
    #endregion

    #region Properties and indexers
    IntPtr MS.Internal.AppModel.IHostService.HostWindowHandle
    {
      get
      {
        return default(IntPtr);
      }
    }
    #endregion
  }
}
