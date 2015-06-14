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

// File System.Tuple_5.cs
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


namespace System
{
  public partial class Tuple<T1, T2, T3, T4, T5> : System.Collections.IStructuralEquatable, System.Collections.IStructuralComparable, IComparable, ITuple
  {
    #region Methods and constructors
    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    int System.Collections.IStructuralComparable.CompareTo(Object other, System.Collections.IComparer comparer)
    {
      return default(int);
    }

    bool System.Collections.IStructuralEquatable.Equals(Object other, System.Collections.IEqualityComparer comparer)
    {
      return default(bool);
    }

    int System.Collections.IStructuralEquatable.GetHashCode(System.Collections.IEqualityComparer comparer)
    {
      return default(int);
    }

    int System.IComparable.CompareTo(Object obj)
    {
      return default(int);
    }

    int System.ITuple.GetHashCode(System.Collections.IEqualityComparer comparer)
    {
      return default(int);
    }

    string System.ITuple.ToString(StringBuilder sb)
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }

    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
    }
    #endregion

    #region Properties and indexers
    public T1 Item1
    {
      get
      {
        return default(T1);
      }
    }

    public T2 Item2
    {
      get
      {
        return default(T2);
      }
    }

    public T3 Item3
    {
      get
      {
        return default(T3);
      }
    }

    public T4 Item4
    {
      get
      {
        return default(T4);
      }
    }

    public T5 Item5
    {
      get
      {
        return default(T5);
      }
    }

    int System.ITuple.Size
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
