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
		private ArrayList items;
		private CollectionChangeEventHandler onCollectionChanged;
		private DataGridViewRow owner;

		public event CollectionChangeEventHandler CollectionChanged;

		public DataGridViewCellCollection(DataGridViewRow dataGridViewRow)
		{
			Contract.Requires(dataGridViewRow != null);
			Contract.Ensures(this.items != null);
			Contract.Ensures(this.owner != null);
		}

		public virtual int Add(DataGridViewCell dataGridViewCell)
		{
			Contract.Requires(dataGridViewCell != null);
			Contract.Ensures(Contract.Result<int>() >= 0);
			return default(int);
		}

		internal int AddInternal(DataGridViewCell dataGridViewCell)
		{
			Contract.Requires(dataGridViewCell != null);
			Contract.Ensures(Contract.Result<int>() >= 0);
			return default(int);
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual void AddRange(params DataGridViewCell[] dataGridViewCells)
		{
			Contract.Requires(dataGridViewCells != null);
			Contract.Requires(Contract.ForAll(dataGridViewCells, (cell) => { return cell != null; }));
		}

		public virtual void Clear()
		{
			
		}

		public virtual bool Contains(DataGridViewCell dataGridViewCell)
		{
			return default(bool);
		}

		public void CopyTo(DataGridViewCell[] array, int index)
		{
			Contract.Requires(array != null);
		}

		public int IndexOf(DataGridViewCell dataGridViewCell)
		{
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}

		public virtual void Insert(int index, DataGridViewCell dataGridViewCell)
		{
			Contract.Requires(dataGridViewCell != null);
			Contract.Requires(index >= 0);
			Contract.Requires(index <= this.Count);
		}

		internal void InsertInternal(int index, DataGridViewCell dataGridViewCell)
		{
			Contract.Requires(dataGridViewCell != null);
			Contract.Requires(index >= 0);
			Contract.Requires(index <= this.Count);
		}

		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{

		}

		public virtual void Remove(DataGridViewCell cell)
		{
			Contract.Requires(cell != null);
		}

		public virtual void RemoveAt(int index)
		{
			Contract.Requires(index >= 0);
			Contract.Requires(index < this.Count);
		}

		internal void RemoveAtInternal(int index)
		{
			Contract.Requires(index >= 0);
			Contract.Requires(index < this.Count);
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
				Contract.Requires(index >= 0);
				Contract.Requires(index < this.Count);
				Contract.Ensures(Contract.Result<DataGridViewCell>() != null);
				return default(DataGridViewCell);
			}
			set
			{
				Contract.Requires(index >= 0);
				Contract.Requires(index < this.Count);
			}
		}

		public DataGridViewCell this[string columnName]
		{
			get
			{
				Contract.Ensures(Contract.Result<DataGridViewCell>() != null);
				return default(DataGridViewCell);
			}
			set
			{
				
			}
		}

		protected override ArrayList List
		{
			get
			{
				Contract.Ensures(Contract.Result<ArrayList>() != null);
				return default(ArrayList);
			}
		}

		int ICollection.Count
		{
			get
			{
				return default(int);
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return default(bool);
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				return default(ICollection);
			}
		}

		bool IList.IsFixedSize
		{
			get
			{
				return default(bool);
			}
		}

		bool IList.IsReadOnly
		{
			get
			{
				return default(bool);
			}
		}

		object IList.this[int index]
		{
			get
			{
				Contract.Ensures(Contract.Result<DataGridViewCell>() != null);
				return default(object);
			}
			set
			{
			}
		}

	}
}