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

// File System.Windows.Annotations.Storage.AnnotationStore.cs
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


namespace System.Windows.Annotations.Storage
{
  abstract public partial class AnnotationStore : IDisposable
  {
    #region Methods and constructors
    public abstract void AddAnnotation(System.Windows.Annotations.Annotation newAnnotation);

    protected AnnotationStore()
    {
    }

    public abstract System.Windows.Annotations.Annotation DeleteAnnotation(Guid annotationId);

    protected virtual new void Dispose(bool disposing)
    {
    }

    public void Dispose()
    {
    }

    public abstract void Flush();

    public abstract System.Windows.Annotations.Annotation GetAnnotation(Guid annotationId);

    public abstract IList<System.Windows.Annotations.Annotation> GetAnnotations(System.Windows.Annotations.ContentLocator anchorLocator);

    public abstract IList<System.Windows.Annotations.Annotation> GetAnnotations();

    protected virtual new void OnAnchorChanged(System.Windows.Annotations.AnnotationResourceChangedEventArgs args)
    {
      Contract.Requires(args != null);
    }

    protected virtual new void OnAuthorChanged(System.Windows.Annotations.AnnotationAuthorChangedEventArgs args)
    {
      Contract.Requires(args != null);
    }

    protected virtual new void OnCargoChanged(System.Windows.Annotations.AnnotationResourceChangedEventArgs args)
    {
      Contract.Requires(args != null);
    }

    protected virtual new void OnStoreContentChanged(StoreContentChangedEventArgs e)
    {
    }
    #endregion

    #region Properties and indexers
    public abstract bool AutoFlush
    {
      get;
      set;
    }

    protected bool IsDisposed
    {
      get
      {
        return default(bool);
      }
    }

    protected Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }
    #endregion

    #region Events
    public event System.Windows.Annotations.AnnotationResourceChangedEventHandler AnchorChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Annotations.AnnotationAuthorChangedEventHandler AuthorChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Annotations.AnnotationResourceChangedEventHandler CargoChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event StoreContentChangedEventHandler StoreContentChanged
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
