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

using System.Collections;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
	public class DataGridViewCellCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		// Fields
		private ArrayList items;
		private CollectionChangeEventHandler onCollectionChanged;
		private DataGridViewRow owner;

		// Events
		public event CollectionChangeEventHandler CollectionChanged;

		public DataGridViewCellCollection(DataGridViewRow dataGridViewRow)
		{
			Contract.Requires(dataGridViewRow != null);
			Contract.Ensures(this.items != null);
			Contract.Ensures(this.owner != null);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return default(IEnumerator);
		}

		int IList.Add(object value)
		{
			return default(int);
		}

		void IList.Clear()
		{
			
		}

		bool IList.Contains(object value)
		{
			return default(bool);
		}

		int IList.IndexOf(object value)
		{
			return default(int);
		}

		void IList.Insert(int index, object value)
		{
			
		}

		void IList.Remove(object value)
		{
			
		}

		void IList.RemoveAt(int index)
		{
			
		}

		public DataGridViewCell this[int index]
		{
			get
			{
				return default(DataGridViewCell);
			}
			set
			{
			}
		}

		object IList.this[int index]
		{
			get
			{
				return default(object);
			}
			set
			{
			}
		}


		bool IList.IsFixedSize
		{
			get
			{
				return default(bool);
			}
		}
	}
}