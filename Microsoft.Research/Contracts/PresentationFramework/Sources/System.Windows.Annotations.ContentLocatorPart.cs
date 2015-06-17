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

// File System.Windows.Annotations.ContentLocatorPart.cs
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


namespace System.Windows.Annotations
{
  sealed public partial class ContentLocatorPart : MS.Internal.Annotations.INotifyPropertyChanged2, System.ComponentModel.INotifyPropertyChanged, MS.Internal.Annotations.IOwnedObject
  {
    #region Methods and constructors
    public Object Clone()
    {
      Contract.Ensures(Contract.Result<System.Object>() != null);

      return default(Object);
    }

    public ContentLocatorPart(System.Xml.XmlQualifiedName partType)
    {
      Contract.Ensures(!string.IsNullOrEmpty(partType.Name));
      Contract.Ensures(!string.IsNullOrEmpty(partType.Namespace));
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    bool MS.Internal.Annotations.IOwnedObject.Owned
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public IDictionary<string, string> NameValuePairs
    {
      get
      {
        return default(IDictionary<string, string>);
      }
    }

    public System.Xml.XmlQualifiedName PartType
    {
      get
      {
        return default(System.Xml.XmlQualifiedName);
      }
    }
    #endregion

    #region Events
    event System.ComponentModel.PropertyChangedEventHandler System.ComponentModel.INotifyPropertyChanged.PropertyChanged
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
