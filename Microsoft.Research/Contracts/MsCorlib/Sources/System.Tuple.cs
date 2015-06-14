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

// File System.Tuple.cs
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
  static public partial class Tuple
  {
    #region Methods and constructors
    public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    {
      Contract.Ensures(Contract.Result<System.Tuple<T1, T2, T3, T4, T5, T6>>() != null);

      return default(Tuple<T1, T2, T3, T4, T5, T6>);
    }

    public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
      Contract.Ensures(Contract.Result<System.Tuple<T1, T2, T3, T4, T5>>() != null);

      return default(Tuple<T1, T2, T3, T4, T5>);
    }

    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
    {
      Contract.Ensures(Contract.Result<System.Tuple<T1, T2, T3, T4, T5, T6, T7, System.Tuple<T8>>>() != null);

      return default(Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>);
    }

    public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    {
      Contract.Ensures(Contract.Result<System.Tuple<T1, T2, T3, T4, T5, T6, T7>>() != null);

      return default(Tuple<T1, T2, T3, T4, T5, T6, T7>);
    }

    public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
    {
      Contract.Ensures(Contract.Result<System.Tuple<T1, T2>>() != null);

      return default(Tuple<T1, T2>);
    }

    public static Tuple<T1> Create<T1>(T1 item1)
    {
      Contract.Ensures(Contract.Result<System.Tuple<T1>>() != null);

      return default(Tuple<T1>);
    }

    public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
    {
      Contract.Ensures(Contract.Result<System.Tuple<T1, T2, T3, T4>>() != null);

      return default(Tuple<T1, T2, T3, T4>);
    }

    public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
    {
      Contract.Ensures(Contract.Result<System.Tuple<T1, T2, T3>>() != null);

      return default(Tuple<T1, T2, T3>);
    }
    #endregion
  }
}
