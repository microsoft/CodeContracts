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

// File System.Diagnostics.Contracts.Contract.cs
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


namespace System.Diagnostics.Contracts
{
  static public partial class Contract
  {
    #region Methods and constructors
    public static void Assert(bool condition, string userMessage)
    {
    }

    public static void Assert(bool condition)
    {
    }

    public static void Assume(bool condition)
    {
    }

    public static void Assume(bool condition, string userMessage)
    {
    }

    public static void EndContractBlock()
    {
    }

    public static void Ensures(bool condition, string userMessage)
    {
    }

    public static void Ensures(bool condition)
    {
    }

    public static void EnsuresOnThrow<TException>(bool condition)
    {
    }

    public static void EnsuresOnThrow<TException>(bool condition, string userMessage)
    {
    }

    public static bool Exists(int fromInclusive, int toExclusive, Predicate<int> predicate)
    {
      return default(bool);
    }

    public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> predicate)
    {
      return default(bool);
    }

    public static bool ForAll(int fromInclusive, int toExclusive, Predicate<int> predicate)
    {
      return default(bool);
    }

    public static bool ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
    {
      return default(bool);
    }

    public static void Invariant(bool condition)
    {
    }

    public static void Invariant(bool condition, string userMessage)
    {
    }

    public static T OldValue<T>(T value)
    {
      return default(T);
    }

    public static void Requires<TException>(bool condition, string userMessage)
    {
    }

    public static void Requires<TException>(bool condition)
    {
    }

    public static void Requires(bool condition)
    {
    }

    public static void Requires(bool condition, string userMessage)
    {
    }

    public static T Result<T>()
    {
      return default(T);
    }

    public static T ValueAtReturn<T>(out T value)
    {
      Contract.Ensures(Contract.Result<T>() == Contract.ValueAtReturn(out value));

      value = default(T);

      return default(T);
    }
    #endregion

    #region Events
    public static event EventHandler<ContractFailedEventArgs> ContractFailed
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
