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

// File System.Windows.Markup.Localizer.BamlLocalizationDictionaryEnumerator.cs
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


namespace System.Windows.Markup.Localizer
{
  sealed public partial class BamlLocalizationDictionaryEnumerator : System.Collections.IDictionaryEnumerator, System.Collections.IEnumerator
  {
    #region Methods and constructors
    internal BamlLocalizationDictionaryEnumerator()
    {
    }

    public bool MoveNext()
    {
      return default(bool);
    }

    public void Reset()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Collections.DictionaryEntry Current
    {
      get
      {
        return default(System.Collections.DictionaryEntry);
      }
    }

    public System.Collections.DictionaryEntry Entry
    {
      get
      {
        return default(System.Collections.DictionaryEntry);
      }
    }

    public BamlLocalizableResourceKey Key
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Markup.Localizer.BamlLocalizableResourceKey>() == ((System.Windows.Markup.Localizer.BamlLocalizableResourceKey)(this.Entry.Key)));

        return default(BamlLocalizableResourceKey);
      }
    }

    Object System.Collections.IDictionaryEnumerator.Key
    {
      get
      {
        return default(Object);
      }
    }

    Object System.Collections.IDictionaryEnumerator.Value
    {
      get
      {
        return default(Object);
      }
    }

    Object System.Collections.IEnumerator.Current
    {
      get
      {
        return default(Object);
      }
    }

    public BamlLocalizableResource Value
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Markup.Localizer.BamlLocalizableResource>() == ((System.Windows.Markup.Localizer.BamlLocalizableResource)(this.Entry.Value)));

        return default(BamlLocalizableResource);
      }
    }
    #endregion
  }
}
