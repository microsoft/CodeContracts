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

// File System.Security.Policy.ApplicationTrustCollection.cs
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


namespace System.Security.Policy
{
  sealed public partial class ApplicationTrustCollection : System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public int Add(ApplicationTrust trust)
    {
      return default(int);
    }

    public void AddRange(ApplicationTrustCollection trusts)
    {
    }

    public void AddRange(ApplicationTrust[] trusts)
    {
    }

    internal ApplicationTrustCollection()
    {
    }

    public void Clear()
    {
    }

    public void CopyTo(ApplicationTrust[] array, int index)
    {
    }

    public ApplicationTrustCollection Find(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
    {
      Contract.Ensures(Contract.Result<System.Security.Policy.ApplicationTrustCollection>() != null);

      return default(ApplicationTrustCollection);
    }

    public ApplicationTrustEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Security.Policy.ApplicationTrustEnumerator>() != null);

      return default(ApplicationTrustEnumerator);
    }

    public void Remove(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
    {
    }

    public void Remove(ApplicationTrust trust)
    {
    }

    public void RemoveRange(ApplicationTrust[] trusts)
    {
    }

    public void RemoveRange(ApplicationTrustCollection trusts)
    {
    }

    void System.Collections.ICollection.CopyTo(Array array, int index)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }
    #endregion

    #region Properties and indexers
    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public ApplicationTrust this [int index]
    {
      get
      {
        return default(ApplicationTrust);
      }
    }

    public ApplicationTrust this [string appFullName]
    {
      get
      {
        return default(ApplicationTrust);
      }
    }

    public Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }
    #endregion
  }
}
