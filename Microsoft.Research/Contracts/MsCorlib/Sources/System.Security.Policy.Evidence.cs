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

// File System.Security.Policy.Evidence.cs
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
  sealed public partial class Evidence : System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void AddAssembly(Object id)
    {
    }

    public void AddAssemblyEvidence<T>(T evidence)
    {
    }

    public void AddHost(Object id)
    {
    }

    public void AddHostEvidence<T>(T evidence)
    {
    }

    public void Clear()
    {
    }

    public System.Security.Policy.Evidence Clone()
    {
      Contract.Ensures(Contract.Result<System.Security.Policy.Evidence>() != null);

      return default(System.Security.Policy.Evidence);
    }

    public void CopyTo(Array array, int index)
    {
    }

    public Evidence()
    {
    }

    public Evidence(System.Security.Policy.Evidence evidence)
    {
    }

    public Evidence(Object[] hostEvidence, Object[] assemblyEvidence)
    {
    }

    public Evidence(EvidenceBase[] hostEvidence, EvidenceBase[] assemblyEvidence)
    {
    }

    public System.Collections.IEnumerator GetAssemblyEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Collections.IEnumerator>() != null);

      return default(System.Collections.IEnumerator);
    }

    public T GetAssemblyEvidence<T>()
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public System.Collections.IEnumerator GetHostEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Collections.IEnumerator>() != null);

      return default(System.Collections.IEnumerator);
    }

    public T GetHostEvidence<T>()
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public void Merge(System.Security.Policy.Evidence evidence)
    {
    }

    public void RemoveType(Type t)
    {
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

    public bool IsReadOnly
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == false);

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

    public bool Locked
    {
      get
      {
        return default(bool);
      }
      set
      {
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
