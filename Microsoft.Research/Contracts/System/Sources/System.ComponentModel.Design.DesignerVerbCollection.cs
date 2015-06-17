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

// File System.ComponentModel.Design.DesignerVerbCollection.cs
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


namespace System.ComponentModel.Design
{
  public partial class DesignerVerbCollection : System.Collections.CollectionBase
  {
    #region Methods and constructors
    public int Add(DesignerVerb value)
    {
      return default(int);
    }

    public void AddRange(DesignerVerb[] value)
    {
    }

    public void AddRange(System.ComponentModel.Design.DesignerVerbCollection value)
    {
    }

    public bool Contains(DesignerVerb value)
    {
      return default(bool);
    }

    public void CopyTo(DesignerVerb[] array, int index)
    {
    }

    public DesignerVerbCollection()
    {
    }

    public DesignerVerbCollection(DesignerVerb[] value)
    {
    }

    public int IndexOf(DesignerVerb value)
    {
      return default(int);
    }

    public void Insert(int index, DesignerVerb value)
    {
    }

    protected override void OnClear()
    {
    }

    protected override void OnInsert(int index, Object value)
    {
    }

    protected override void OnRemove(int index, Object value)
    {
    }

    protected override void OnSet(int index, Object oldValue, Object newValue)
    {
    }

    protected override void OnValidate(Object value)
    {
    }

    public void Remove(DesignerVerb value)
    {
    }
    #endregion

    #region Properties and indexers
    public DesignerVerb this [int index]
    {
      get
      {
        return default(DesignerVerb);
      }
      set
      {
      }
    }
    #endregion
  }
}
