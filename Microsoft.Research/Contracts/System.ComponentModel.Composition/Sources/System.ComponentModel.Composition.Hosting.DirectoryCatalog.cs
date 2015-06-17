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

// File System.ComponentModel.Composition.Hosting.DirectoryCatalog.cs
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


namespace System.ComponentModel.Composition.Hosting
{
  public partial class DirectoryCatalog : System.ComponentModel.Composition.Primitives.ComposablePartCatalog, INotifyComposablePartCatalogChanged, System.ComponentModel.Composition.Primitives.ICompositionElement
  {
    #region Methods and constructors
    public DirectoryCatalog(string path)
    {
      Contract.Requires(!string.IsNullOrEmpty(path));
    }

    public DirectoryCatalog(string path, string searchPattern)
    {
      Contract.Requires(!string.IsNullOrEmpty(path));
      Contract.Requires(searchPattern != null);
    }

    protected override void Dispose(bool disposing)
    {
    }

    public override IEnumerable<Tuple<System.ComponentModel.Composition.Primitives.ComposablePartDefinition, System.ComponentModel.Composition.Primitives.ExportDefinition>> GetExports(System.ComponentModel.Composition.Primitives.ImportDefinition definition)
    {
      return default(IEnumerable<Tuple<System.ComponentModel.Composition.Primitives.ComposablePartDefinition, System.ComponentModel.Composition.Primitives.ExportDefinition>>);
    }

    protected virtual new void OnChanged(ComposablePartCatalogChangeEventArgs e)
    {
    }

    protected virtual new void OnChanging(ComposablePartCatalogChangeEventArgs e)
    {
    }

    public void Refresh()
    {
    }

    #endregion

    #region Properties and indexers
    public string FullPath
    {
      get
      {
        return default(string);
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<string> LoadedFiles
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<string>);
      }
    }

#if !NETFRAMEWORK_4_5
    public override System.Linq.IQueryable<System.ComponentModel.Composition.Primitives.ComposablePartDefinition> Parts
    {
      get
      {
        return default(System.Linq.IQueryable<System.ComponentModel.Composition.Primitives.ComposablePartDefinition>);
      }
    }
#endif


    public string Path
    {
      get
      {
        return default(string);
      }
    }

    public string SearchPattern
    {
      get
      {
        return default(string);
      }
    }

    string System.ComponentModel.Composition.Primitives.ICompositionElement.DisplayName
    {
      get
      {
        return default(string);
      }
    }

    System.ComponentModel.Composition.Primitives.ICompositionElement System.ComponentModel.Composition.Primitives.ICompositionElement.Origin
    {
      get
      {
        return default(System.ComponentModel.Composition.Primitives.ICompositionElement);
      }
    }
    #endregion

    #region Events
    public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing
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
