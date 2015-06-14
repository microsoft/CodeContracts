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

// File System.Globalization.TextInfo.cs
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


namespace System.Globalization
{
  public partial class TextInfo : ICloneable, System.Runtime.Serialization.IDeserializationCallback
  {
    #region Methods and constructors
    public virtual new Object Clone()
    {
      return default(Object);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static TextInfo ReadOnly(TextInfo textInfo)
    {
      Contract.Ensures(Contract.Result<System.Globalization.TextInfo>() != null);

      return default(TextInfo);
    }

    void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(Object sender)
    {
    }

    internal TextInfo()
    {
    }

    public virtual new char ToLower(char c)
    {
      return default(char);
    }

    public virtual new string ToLower(string str)
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }

    public string ToTitleCase(string str)
    {
      return default(string);
    }

    public virtual new char ToUpper(char c)
    {
      return default(char);
    }

    public virtual new string ToUpper(string str)
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public virtual new int ANSICodePage
    {
      get
      {
        return default(int);
      }
    }

    public string CultureName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new int EBCDICCodePage
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
        return default(bool);
      }
    }

    public bool IsRightToLeft
    {
      get
      {
        return default(bool);
      }
    }

    public int LCID
    {
      get
      {
        return default(int);
      }
    }

    public virtual new string ListSeparator
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new int MacCodePage
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int OEMCodePage
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
