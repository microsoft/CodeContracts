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

// File System.Windows.Media.Imaging.BitmapMetadata.cs
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


namespace System.Windows.Media.Imaging
{
  public partial class BitmapMetadata : System.Windows.Media.ImageMetadata, IEnumerable<string>, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public BitmapMetadata(string containerFormat)
    {
    }

    public System.Windows.Media.Imaging.BitmapMetadata Clone()
    {
      return default(System.Windows.Media.Imaging.BitmapMetadata);
    }

    protected override void CloneCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override void CloneCurrentValueCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public bool ContainsQuery(string query)
    {
      return default(bool);
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    protected override void GetAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override void GetCurrentValueAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public Object GetQuery(string query)
    {
      return default(Object);
    }

    public void RemoveQuery(string query)
    {
    }

    public void SetQuery(string query, Object value)
    {
    }

    IEnumerator<string> System.Collections.Generic.IEnumerable<System.String>.GetEnumerator()
    {
      return default(IEnumerator<string>);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }
    #endregion

    #region Properties and indexers
    public string ApplicationName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<string> Author
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<string>);
      }
      set
      {
      }
    }

    public string CameraManufacturer
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string CameraModel
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Comment
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Copyright
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string DateTaken
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Format
    {
      get
      {
        return default(string);
      }
    }

    public bool IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<string> Keywords
    {
      get
      {
        return default(System.Collections.ObjectModel.ReadOnlyCollection<string>);
      }
      set
      {
      }
    }

    public string Location
    {
      get
      {
        return default(string);
      }
    }

    public int Rating
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string Subject
    {
      get
      {
        return default(string);
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
    #endregion
  }
}
