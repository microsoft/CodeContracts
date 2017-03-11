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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace System.Windows.Forms
{
	public class DataGridViewRowCollection : ICollection, IEnumerable, IList
	{
		/*private class RowArrayList : ArrayList
		{
			// Fields
			private DataGridViewRowCollection owner;
			private DataGridViewRowCollection.RowComparer rowComparer;

			// Methods
			public RowArrayList(DataGridViewRowCollection owner)
			{
				Contract.Requires(owner != null);
				Contract.Ensures(this.owner != null);
			}

			public void CustomSort(DataGridViewRowCollection.RowComparer rowComparer)
			{
				Contract.Requires(rowComparer != null);
				Contract.Ensures(this.rowComparer != null);
			}
		}

		private class RowComparer
		{
			// Fields
			//private bool ascending;
			//private IComparer customComparer;
			private DataGridView dataGridView;
			private DataGridViewRowCollection dataGridViewRows;
			//private DataGridViewColumn dataGridViewSortedColumn;
			private static ComparedObjectMax max = new ComparedObjectMax();
			//private int sortedColumnIndex;

			// Methods
			public RowComparer(DataGridViewRowCollection dataGridViewRows, IComparer customComparer, bool ascending)
			{
				Contract.Requires(dataGridViewRows != null);
				Contract.Ensures(this.dataGridViewRows != null);
				Contract.Ensures(this.dataGridView != null);
			}

			// Nested Types
			private class ComparedObjectMax
			{
			}
		}

		private class UnsharingRowEnumerator : IEnumerator
		{
			// Fields
			private int current;
			private DataGridViewRowCollection owner;

			// Methods
			public UnsharingRowEnumerator(DataGridViewRowCollection owner)
			{
				Contract.Requires(owner != null);
				Contract.Ensures(this.owner != null);
				Contract.Ensures(this.current == -1);
			}

			bool IEnumerator.MoveNext()
			{
				return default(bool);
			}

			void IEnumerator.Reset()
			{
				Contract.Ensures(this.current == -1);
				this.current = -1;
			}

			// Properties
			object IEnumerator.Current
			{
				get
				{
					return default(object);
				}
			}
		}

		private DataGridView dataGridView;
		private RowArrayList items;
		//private CollectionChangeEventHandler onCollectionChanged;
		//private int rowCountsVisible;
		//private int rowCountsVisibleFrozen;
		//private int rowCountsVisibleSelected;
		//private int rowsHeightVisible;
		//private int rowsHeightVisibleFrozen;
		private List<DataGridViewElementStates> rowStates;

		//public event CollectionChangeEventHandler CollectionChanged;*/

		public DataGridViewRowCollection(DataGridView dataGridView)
		{
			Contract.Requires(dataGridView != null);
		}

		public virtual int Add()
		{
			Contract.Ensures(Contract.Result<int>() >= 0);
			return default(int);
		}

		public virtual int Add(int count)
		{
			Contract.Requires(count > 0);
			return default(int);
		}
		public virtual int Add(params object[] values)
		{
			Contract.Requires(values != null);
			Contract.Ensures(Contract.Result<int>() >= 0);
			return default(int);
		}

		public virtual int Add(DataGridViewRow dataGridViewRow)
		{
			Contract.Requires(dataGridViewRow != null);
			Contract.Ensures(Contract.Result<int>() >= 0);
			return default(int);
		}

		public virtual int AddCopies(int indexSource, int count)
		{
			return default(int);
		}

		public virtual int AddCopy(int indexSource)
		{
			return default(int);
		}

		public virtual void AddRange(params DataGridViewRow[] dataGridViewRows)
		{
			Contract.Requires(dataGridViewRows != null);
			Contract.Requires(Contract.ForAll(dataGridViewRows, (row) => { return row != null; }));
		}

		public virtual void Clear()
		{

		}

		public virtual bool Contains(DataGridViewRow dataGridViewRow)
		{
			return default(bool);
		}

		public void CopyTo(DataGridViewRow[] array, int index)
		{
			Contract.Requires(array != null);
		}

		public int GetFirstRow(DataGridViewElementStates includeFilter)
		{
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}
		public int GetFirstRow(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}
		public int GetLastRow(DataGridViewElementStates includeFilter)
		{
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}

		public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter)
		{
			Contract.Requires(indexStart >= -1);
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}

		public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			Contract.Requires(indexStart >= -1);
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}

		public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter)
		{
			Contract.Requires(indexStart <= this.Count);
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}

		public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			Contract.Requires(indexStart <= this.Count);
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}

		public int GetRowCount(DataGridViewElementStates includeFilter)
		{
			return default(int);
		}

		public int GetRowsHeight(DataGridViewElementStates includeFilter)
		{
			return default(int);
		}

		public virtual DataGridViewElementStates GetRowState(int rowIndex)
		{
			Contract.Requires(rowIndex >= 0);
			Contract.Requires(rowIndex < this.Count);
			return default(DataGridViewElementStates);
		}

		public int IndexOf(DataGridViewRow dataGridViewRow)
		{
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}

		public virtual void Insert(int rowIndex, int count)
		{
			Contract.Requires(rowIndex >= 0);
			Contract.Requires(this.Count >= rowIndex);
			Contract.Requires(count > 0);
		}

		public virtual void Insert(int rowIndex, params object[] values)
		{
			Contract.Requires(values != null);
		}

		public virtual void Insert(int rowIndex, DataGridViewRow dataGridViewRow)
		{
			Contract.Requires(rowIndex >= 0);
			Contract.Requires(this.Count >= rowIndex);
			Contract.Requires(dataGridViewRow != null);
		}

		public virtual void InsertCopies(int indexSource, int indexDestination, int count)
		{
			Contract.Requires(indexSource >= 0);
			Contract.Requires(this.Count > indexSource);
			Contract.Requires(indexDestination >= 0);
			Contract.Requires(this.Count >= indexDestination);
			Contract.Requires(count > 0);
		}

		public virtual void InsertCopy(int indexSource, int indexDestination)
		{
			Contract.Requires(indexSource >= 0);
			Contract.Requires(this.Count > indexSource);
			Contract.Requires(indexDestination >= 0);
			Contract.Requires(this.Count >= indexDestination);
		}

		public virtual void InsertRange(int rowIndex, params DataGridViewRow[] dataGridViewRows)
		{
			Contract.Requires(dataGridViewRows != null);
			Contract.Requires(Contract.ForAll(dataGridViewRows, (row) => { return row != null; }));
			Contract.Requires(rowIndex >= 0);
			Contract.Requires(this.Count >= rowIndex);
		}

		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{

		}

		public virtual void Remove(DataGridViewRow dataGridViewRow)
		{
			Contract.Requires(dataGridViewRow != null);
		}

		public virtual void RemoveAt(int index)
		{
			Contract.Requires(index >= 0);
			Contract.Requires(index < this.Count);
		}

		public DataGridViewRow SharedRow(int rowIndex)
		{
			Contract.Ensures(Contract.Result<DataGridViewRow>() != null);
			return default(DataGridViewRow);
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

		public int Count
		{
			get
			{
				Contract.Ensures(Contract.Result<int>() >= 0);
				return default(int);
			}
		}

		protected DataGridView DataGridView
		{
			get
			{
				Contract.Ensures(Contract.Result<DataGridView>() != null);
				return default(DataGridView);
			}
		}

		public DataGridViewRow this[int index]
		{
			get
			{
				Contract.Requires(index >= 0);
				Contract.Requires(index < this.Count);
				Contract.Ensures(Contract.Result<DataGridViewRow>() != null);
				return default(DataGridViewRow);
			}
		}

		protected ArrayList List
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
				Contract.Ensures(Contract.Result<object>() != null);
				return default(object);
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
				Contract.Ensures(Contract.Result<DataGridViewRow>() != null);
				return default(DataGridViewRow);
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}