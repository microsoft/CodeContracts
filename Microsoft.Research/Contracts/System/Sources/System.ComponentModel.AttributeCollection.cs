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

// File System.ComponentModel.AttributeCollection.cs
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


namespace System.ComponentModel
{
  public partial class AttributeCollection : System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    protected AttributeCollection()
    {
    }

    public AttributeCollection(Attribute[] attributes)
    {
      Contract.Ensures((attributes.Length - Contract.OldValue(attributes.Length)) <= 0);
    }

    public bool Contains(Attribute[] attributes)
    {
      return default(bool);
    }

    public bool Contains(Attribute attribute)
    {
      Contract.Requires(attribute != null);

      return default(bool);
    }

    public void CopyTo(Array array, int index)
    {
    }

    public static System.ComponentModel.AttributeCollection FromExisting(System.ComponentModel.AttributeCollection existing, Attribute[] newAttributes)
    {
      Contract.Ensures((newAttributes.Length - Contract.OldValue(newAttributes.Length)) <= 0);
      Contract.Ensures(Contract.Result<System.ComponentModel.AttributeCollection>() != null);

      return default(System.ComponentModel.AttributeCollection);
    }

    protected Attribute GetDefaultAttribute(Type attributeType)
    {
      return default(Attribute);
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public bool Matches(Attribute[] attributes)
    {
      Contract.Requires(attributes != null);

      return default(bool);
    }

    public bool Matches(Attribute attribute)
    {
      Contract.Ensures(0 <= this.Attributes.Length);

      return default(bool);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }
    #endregion

    #region Properties and indexers
    protected virtual new Attribute[] Attributes
    {
      get
      {
        return default(Attribute[]);
      }
    }

    public int Count
    {
      get
      {
        Contract.Ensures((this.Attributes.Length - Contract.Result<int>()) <= 0);
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 2147483647);
        Contract.Ensures(Contract.Result<int>() == (int)(this.Attributes.Length));
        Contract.Ensures(Contract.Result<int>() == this.Attributes.Length);

        return default(int);
      }
    }

    public virtual new Attribute this [int index]
    {
      get
      {
        Contract.Requires(0 <= index);

        return default(Attribute);
      }
    }

    public virtual new Attribute this [Type attributeType]
    {
      get
      {
        return default(Attribute);
      }
    }

    int System.Collections.ICollection.Count
    {
      get
      {
        return default(int);
      }
    }

    bool System.Collections.ICollection.IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Collections.ICollection.SyncRoot
    {
      get
      {
        return default(Object);
      }
    }
    #endregion

    #region Fields
    public readonly static System.ComponentModel.AttributeCollection Empty;
    #endregion
  }
}
