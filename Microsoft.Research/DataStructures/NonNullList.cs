// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        private void ObjectInvariant()
        {
            Contract.Invariant(values != null);
            Contract.Invariant(pos >= 0);
            Contract.Invariant(pos <= values.Length);
            Contract.Invariant(Contract.ForAll(0, pos, i => values[i] != null));
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

            values = new T[size];
            pos = 0;
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

            values = new T[other.values.Length];
            pos = other.pos;

            for (var i = 0; i < other.pos; i++)
            {
                Contract.Assert(other.values[i] != null);

                values[i] = other.values[i];
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
                Contract.Ensures(Contract.Result<bool>() == (pos == 0));

                return pos == 0;
            }
        }

        public int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.Ensures(Contract.Result<int>() == pos);

                return pos;
            }
        }

        public T this[int index]
        {
            get
            {
                Contract.Requires(index >= 0);
                Contract.Requires(index < this.Count);

                Contract.Ensures(Contract.Result<T>() != null);

                Contract.Assert(index < pos);

                return values[index];
            }
            set
            {
                Contract.Requires(index >= 0);
                Contract.Requires(index < this.Count);

                Contract.Requires(value != null);

                Contract.Ensures(pos == Contract.OldValue(pos));

                values[index] = value;
            }
        }

        #endregion

        public void Add(T value)
        {
            Contract.Requires(value != null);

            Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);

            if (pos == values.Length)
            {
                var newArr = new T[values.Length * 2 + 1];

                // TODO F: there is an imprecision in the array analysis: If we use this.values.Length instead of this.pos it does not work
                for (var i = 0; i < pos; i++)
                {
                    newArr[i] = values[i];
                }

                values = newArr;
            }

            values[pos++] = value;
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

            if (pos == values.Length)
            {
                var newArr = new T[values.Length * 2 + 1];
                for (var i = 0; i < index; i++)
                {
                    newArr[i] = values[i];
                }

                newArr[index] = value;
                pos++;

                for (var i = index + 1; i < pos; i++)
                {
                    newArr[i] = values[i - 1];
                }

                values = newArr;
            }
            else
            {
                // F: unrolling the first loop helps Clousot to prove the invariant.
                values[pos] = values[pos - 1];

                for (var i = pos - 1; i > index; i--)
                {
                    Contract.Assert(values[i] != null);
                    Contract.Assert(values[i - 1] != null);

                    values[i] = values[i - 1];
                }

                values[index] = value;
                pos++;
            }
        }

        [Pure]
        public T GetLastElement()
        {
            Contract.Requires(!this.IsEmpty);

            Contract.Ensures(Contract.Result<T>() != null);

            return values[pos - 1];
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            for (var i = 0; i < pos; i++)
            {
                result.AppendFormat("{0} ", values[i]);
            }

            return result.ToString();
        }

        #region Convert
        [Pure]
        public NonNullList<U> ConvertAll<U>(Converter<T, U> converter)
          where U : class
        {
            Contract.Requires(converter != null);
            Contract.Ensures(Contract.Result<NonNullList<U>>() != null);
            Contract.Ensures(Contract.Result<NonNullList<U>>().Count == this.Count);

            var result = new U[values.Length];

            for (var i = 0; i < pos; i++)
            {
                var conv = converter(values[i]);
                Contract.Assume(conv != null);  // We cannot really prove it as it involves a higher-order contract on converter 

                result[i] = conv;
            }

            return new NonNullList<U>(result, pos);
        }
        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < pos; i++)
            {
                yield return values[i];
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }

        #endregion
    }
}
