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

// File System.Runtime.CompilerServices.RuntimeOps.cs
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


namespace System.Runtime.CompilerServices
{
  static public partial class RuntimeOps
  {
    #region Methods and constructors
    public static IRuntimeVariables CreateRuntimeVariables(Object[] data, long[] indexes)
    {
      Contract.Ensures(Contract.Result<System.Runtime.CompilerServices.IRuntimeVariables>() != null);

      return default(IRuntimeVariables);
    }

    public static IRuntimeVariables CreateRuntimeVariables()
    {
      Contract.Ensures(Contract.Result<System.Runtime.CompilerServices.IRuntimeVariables>() != null);

      return default(IRuntimeVariables);
    }

    public static bool ExpandoCheckVersion(System.Dynamic.ExpandoObject expando, Object version)
    {
      Contract.Requires(expando != null);

      return default(bool);
    }

    public static void ExpandoPromoteClass(System.Dynamic.ExpandoObject expando, Object oldClass, Object newClass)
    {
      Contract.Requires(expando != null);
    }

    public static bool ExpandoTryDeleteValue(System.Dynamic.ExpandoObject expando, Object indexClass, int index, string name, bool ignoreCase)
    {
      Contract.Requires(expando != null);

      return default(bool);
    }

    public static bool ExpandoTryGetValue(System.Dynamic.ExpandoObject expando, Object indexClass, int index, string name, bool ignoreCase, out Object value)
    {
      Contract.Requires(expando != null);

      value = default(Object);

      return default(bool);
    }

    public static Object ExpandoTrySetValue(System.Dynamic.ExpandoObject expando, Object indexClass, int index, Object value, string name, bool ignoreCase)
    {
      Contract.Requires(expando != null);
      Contract.Ensures(Contract.Result<System.Object>() == value);

      return default(Object);
    }

    public static IRuntimeVariables MergeRuntimeVariables(IRuntimeVariables first, IRuntimeVariables second, int[] indexes)
    {
      Contract.Ensures(Contract.Result<System.Runtime.CompilerServices.IRuntimeVariables>() != null);

      return default(IRuntimeVariables);
    }

    public static System.Linq.Expressions.Expression Quote(System.Linq.Expressions.Expression expression, Object hoistedLocals, Object[] locals)
    {
      return default(System.Linq.Expressions.Expression);
    }
    #endregion
  }
}
