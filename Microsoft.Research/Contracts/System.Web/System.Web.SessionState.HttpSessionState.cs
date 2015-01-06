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

// File System.Web.SessionState.HttpSessionState.cs
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


namespace System.Web.SessionState
{
  sealed public partial class HttpSessionState : System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void Abandon ()
    {
    }

    public void Add (string name, Object value)
    {
    }

    public void Clear ()
    {
    }

    public void CopyTo (Array array, int index)
    {
    }

    public System.Collections.IEnumerator GetEnumerator ()
    {
      return default(System.Collections.IEnumerator);
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
    #endregion

    #region Properties and indexers
    public int CodePage
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public HttpSessionState Contents
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.SessionState.HttpSessionState>() != null);

        return default(HttpSessionState);
      }
    }

    public System.Web.HttpCookieMode CookieMode
    {
      get
      {
        return default(System.Web.HttpCookieMode);
      }
    }

    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public bool IsCookieless
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNewSession
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

    public bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public Object this [int index]
    {
      get
      {
        return default(Object);
      }
      set
      {
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

    public System.Collections.Specialized.NameObjectCollectionBase.KeysCollection Keys
    {
      get
      {
        return default(System.Collections.Specialized.NameObjectCollectionBase.KeysCollection);
      }
    }

    public int LCID
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public SessionStateMode Mode
    {
      get
      {
        return default(SessionStateMode);
      }
    }

    public string SessionID
    {
      get
      {
        return default(string);
      }
    }

    public System.Web.HttpStaticObjectsCollection StaticObjects
    {
      get
      {
        return default(System.Web.HttpStaticObjectsCollection);
      }
    }

    public Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }

    public int Timeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }
    #endregion
  }
}
