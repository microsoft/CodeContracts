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

// File System.Web.HttpApplicationState.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Web
{
  sealed public partial class HttpApplicationState : System.Collections.Specialized.NameObjectCollectionBase
  {
    #region Methods and constructors
    internal HttpApplicationState() { }

    public void Add (string name, Object value)
    {
    }

    public void Clear ()
    {
    }

    public Object Get (string name)
    {
      return default(Object);
    }

    public Object Get (int index)
    {
      return default(Object);
    }

    public string GetKey (int index)
    {
      return default(string);
    }

    public void Lock ()
    {
    }

    public void Remove (string name)
    {
    }

    public void RemoveAll ()
    {
    }

    public void RemoveAt (int index)
    {
    }

    public void Set (string name, Object value)
    {
    }

    public void UnLock ()
    {
    }
    #endregion

    #region Properties and indexers
    public string[] AllKeys
    {
      get
      {
        Contract.Ensures (Contract.Result<string[]>() != null);

        return default(string[]);
      }
    }

    public HttpApplicationState Contents
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.HttpApplicationState>() != null);

        return default(HttpApplicationState);
      }
    }

    public override int Count
    {
      get
      {
        return default(int);
      }
    }

    public Object this [int index]
    {
      get
      {
        return default(Object);
      }
    }

    public Object this [string name]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public HttpStaticObjectsCollection StaticObjects
    {
      get
      {
        return default(HttpStaticObjectsCollection);
      }
    }
    #endregion
  }
}
