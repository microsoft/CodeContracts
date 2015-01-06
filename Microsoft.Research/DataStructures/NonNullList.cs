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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{  
  [ContractVerification(true)]
  public class NonNullList<T>
    : IEnumerable<T>
    where T : class
  {
    #region ObjectInvariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.values != null);
      Contract.Invariant(this.pos >= 0);
      Contract.Invariant(this.pos <= this.values.Length);
      Contract.Invariant(Contract.ForAll(0, this.pos, i => this.values[i] != null));
    }

    #endregion

    #region Private state
    private T[] values;
    private int pos;
    #endregion

    #region Constructor
    public NonNullList()
      : this(4)
    {
      Contract.Ensures(this.Count == 0);
    }

    public NonNullList(int size)
    {
      Contract.Requires(size >= 0);

      Contract.Ensures(this.Count == 0);

      this.values = new T[size];
      this.pos = 0;
    }

    public NonNullList(NonNullList<T> other)
    {
      Contract.Requires(other != null);

      Contract.Ensures(this.Count == other.Count);

      // F: Assuming the object invariant for other
      Contract.Assume(other.values != null);
      Contract.Assume(other.pos >= 0);
      Contract.Assume(other.pos <= other.values.Length);
      Contract.Assume(Contract.ForAll(0, other.pos, i => other.values[i] != null));

      this.values = new T[other.values.Length];
      this.pos = other.pos;

      for (var i = 0; i < other.pos; i++)
      {
        Contract.Assert(other.values[i] != null);

        this.values[i] = other.values[i];
      }

    }

    private NonNullList(T[] values, int pos)
    {
      Contract.Requires(values != null);
      Contract.Requires(pos >= 0);
      Contract.Requires(pos <= values.Length);
      Contract.Requires(Contract.ForAll(0, pos, i => values[i] != null));

      Contract.Ensures(this.Count == pos);

      this.values = values;
      this.pos = pos;
    }

    #endregion

    #region Getters

    public bool IsEmpty
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == (this.pos == 0));

        return this.pos == 0;
      }
    }

    public int Count
    {
      get
      {
       Contract.Ensures(Contract.Result<int>() >= 0);
       Contract.Ensures(Contract.Result<int>() == this.pos);

        return this.pos;
      }
    }

    public T this[int index]
    {
      get
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);

        Contract.Ensures(Contract.Result<T>() != null);

        Contract.Assert(index < this.pos);

        return this.values[index];
      }
      set
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
 
        Contract.Requires(value != null);

        Contract.Ensures(this.pos == Contract.OldValue(this.pos));

        this.values[index] = value;
      }
    }

    #endregion

    public void Add(T value)
    {
      Contract.Requires(value != null);

      Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);

      if (this.pos == this.values.Length)
      {
        var newArr = new T[this.values.Length * 2 + 1];

        // TODO F: there is an imprecision in the array analysis: If we use this.values.Length instead of this.pos it does not work
        for (var i = 0; i < this.pos; i++)
        {
          newArr[i] = this.values[i];
        }

        this.values = newArr;
      }

      this.values[pos++] = value;
    }

    public void InsertOrAdd(int index, T value)
    {
      Contract.Requires(value != null);

      Contract.Requires(index >= 0);
      Contract.Requires(index <= this.Count);

      Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);

      if (index == this.Count)
      {
        this.Add(value);
        return;
      }

      if (this.pos == this.values.Length)
      {
        var newArr = new T[this.values.Length * 2 + 1];
        for (var i = 0; i < index; i++)
        {
          newArr[i] = this.values[i];
        }

        newArr[index] = value;
        this.pos++;

        for (var i = index + 1; i < this.pos; i++)
        {
          newArr[i] = this.values[i - 1];
        }

        this.values = newArr;
      }
      else
      {
        // F: unrolling the first loop helps Clousot to prove the invariant.
        this.values[pos] = this.values[pos - 1];

        for (var i = pos - 1; i > index; i--)
        {
          Contract.Assert(this.values[i] != null);
          Contract.Assert(this.values[i - 1] != null);

          this.values[i] = this.values[i - 1];
        }

        this.values[index] = value;
        this.pos++;
      }
    }

    [Pure]
    public T GetLastElement()
    {
      Contract.Requires(!this.IsEmpty);

      Contract.Ensures(Contract.Result<T>() != null);

      return this.values[pos - 1];
    }

    public override string ToString()
    {
      var result = new StringBuilder();

      for (var i = 0; i < this.pos; i++)
      {
        result.AppendFormat("{0} ", this.values[i]);
      }

      return result.ToString();
    }

    #region Convert
    [Pure]
    public NonNullList<U> ConvertAll<U>(Converter<T, U> converter)
      where U: class
    {
      Contract.Requires(converter != null);
      Contract.Ensures(Contract.Result<NonNullList<U>>() != null);
      Contract.Ensures(Contract.Result<NonNullList<U>>().Count == this.Count);

      var result = new U[this.values.Length];

      for (var i = 0; i < this.pos; i++)
      {
        var conv = converter(this.values[i]);
        Contract.Assume(conv != null);  // We cannot really prove it as it involves a higher-order contract on converter 

        result[i] = conv;
      }

      return new NonNullList<U>(result, this.pos);
    }
    #endregion

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      for (var i = 0; i < this.pos; i++)
      {
        yield return this.values[i];
      }
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.values.GetEnumerator();
    }

    #endregion
  }
}
