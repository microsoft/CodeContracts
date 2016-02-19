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
using System.Drawing;

namespace System.Windows.Forms
{
	public class DataGridViewColumnCollection : BaseCollection, IList, ICollection, IEnumerable
	{
		private class ColumnOrderComparer : IComparer
		{
			// Methods
			public int Compare(object x, object y)
			{
				return default(int);
			}
		}

		private int columnCountsVisible;
		private int columnCountsVisibleSelected;
		private static ColumnOrderComparer columnOrderComparer;
		private int columnsWidthVisible;
		private int columnsWidthVisibleFrozen;
		private DataGridView dataGridView;
		private ArrayList items;
		private ArrayList itemsSorted;
		private int lastAccessedSortedIndex;
		private CollectionChangeEventHandler onCollectionChanged;

		public DataGridViewColumnCollection(DataGridView dataGridView)
		{
			Contract.Requires(dataGridView != null);
			Contract.Ensures(items != null);
			Contract.Ensures(this.dataGridView != null);
		}
		internal int ActualDisplayIndexToColumnIndex(int actualDisplayIndex, DataGridViewElementStates includeFilter)
		{
			return default(int);
		}

		public virtual int Add(DataGridViewColumn dataGridViewColumn)
		{
			Contract.Requires(dataGridViewColumn != null);
			Contract.Ensures(Contract.Result<int>() >= 0);
			return default(int);
		}

		public virtual int Add(string columnName, string headerText)
		{
			Contract.Ensures(Contract.Result<int>() >= 0);
			return default(int);
		}

		public virtual void AddRange(params DataGridViewColumn[] dataGridViewColumns)
		{
			Contract.Requires(dataGridViewColumns != null);
			Contract.Requires(Contract.ForAll(dataGridViewColumns, (column) => { return column != null; }));
		}

		public virtual void Clear()
		{
			Contract.Ensures(this.Count == 0);
		}

		internal int ColumnIndexToActualDisplayIndex(int columnIndex, DataGridViewElementStates includeFilter)
		{
			return default(int);
		}

		public virtual bool Contains(string columnName)
		{
			Contract.Requires(columnName != null);
			return default(bool);
		}

		public virtual bool Contains(DataGridViewColumn dataGridViewColumn)
		{
			return default(bool);
		}

		public void CopyTo(DataGridViewColumn[] array, int index)
		{
			Contract.Requires(array != null);
		}

		internal bool DisplayInOrder(int columnIndex1, int columnIndex2)
		{
			return default(bool);
		}

		internal DataGridViewColumn GetColumnAtDisplayIndex(int displayIndex)
		{
			return default(DataGridViewColumn);
		}

		public int GetColumnCount(DataGridViewElementStates includeFilter)
		{
			return default(int);
		}

		internal int GetColumnCount(DataGridViewElementStates includeFilter, int fromColumnIndex, int toColumnIndex)
		{
			return default(int);
		}

		internal float GetColumnsFillWeight(DataGridViewElementStates includeFilter)
		{
			Contract.Ensures(Contract.Result<float>() >= 0.0f);
			return default(float);
		}

		private int GetColumnSortedIndex(DataGridViewColumn dataGridViewColumn)
		{
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}

		public int GetColumnsWidth(DataGridViewElementStates includeFilter)
		{
			return default(int);
		}

		public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter)
		{
			return default(DataGridViewColumn);
		}

		public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			return default(DataGridViewColumn);
		}

		public DataGridViewColumn GetLastColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			return default(DataGridViewColumn);
		}

		public DataGridViewColumn GetNextColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			Contract.Requires(dataGridViewColumnStart != null);
			return default(DataGridViewColumn);
		}


		public DataGridViewColumn GetPreviousColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
		{
			Contract.Requires(dataGridViewColumnStart != null);
			return default(DataGridViewColumn);
		}

		public int IndexOf(DataGridViewColumn dataGridViewColumn)
		{
			Contract.Ensures(Contract.Result<int>() >= -1);
			return default(int);
		}

		public virtual void Insert(int columnIndex, DataGridViewColumn dataGridViewColumn)
		{
			Contract.Requires(columnIndex >= 0);
			Contract.Requires(columnIndex <= this.Count);
			Contract.Requires(dataGridViewColumn != null);
		}

		internal void InvalidateCachedColumnCount(DataGridViewElementStates includeFilter)
		{
			
		}

		internal void InvalidateCachedColumnCounts()
		{
			
		}

		internal void InvalidateCachedColumnsOrder()
		{
			
		}

		internal void InvalidateCachedColumnsWidth(DataGridViewElementStates includeFilter)
		{
			
		}

		internal void InvalidateCachedColumnsWidths()
		{
			
		}

		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			
		}

		private void OnCollectionChanged(CollectionChangeEventArgs ccea, bool changeIsInsertion, Point newCurrentCell)
		{
			Contract.Requires(ccea != null);
		}

		private void OnCollectionChanged_PostNotification(CollectionChangeEventArgs ccea, bool changeIsInsertion, Point newCurrentCell)
		{
			Contract.Requires(ccea != null);
		}

		private void OnCollectionChanged_PreNotification(CollectionChangeEventArgs ccea)
		{
			
		}

		public virtual void Remove(string columnName)
		{
			Contract.Requires(columnName != null);
		}

		public virtual void Remove(DataGridViewColumn dataGridViewColumn)
		{
			Contract.Requires(dataGridViewColumn != null);
		}

		public virtual void RemoveAt(int index)
		{
			Contract.Requires(index >= 0);
			Contract.Requires(index < this.Count);
		}

		internal void RemoveAtInternal(int index, bool force)
		{
			Contract.Requires(index >= 0);
			Contract.Requires(index < this.Count);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			Contract.Requires(array != null);
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

		private void UpdateColumnCaches(DataGridViewColumn dataGridViewColumn, bool adding)
		{

		}

		private void UpdateColumnOrderCache()
		{
			Contract.Ensures(this.itemsSorted != null);
		}

		internal static IComparer ColumnCollectionOrderComparer
		{
			get
			{
				Contract.Ensures(Contract.Result<IComparer>() != null);
				return default(IComparer);
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

		public DataGridViewColumn this[string columnName]
		{
			get
			{
				Contract.Requires(columnName != null);
				return default(DataGridViewColumn);
			}
		}

		public DataGridViewColumn this[int index]
		{
			get
			{
				Contract.Ensures(Contract.Result<DataGridViewColumn>() != null);
				return default(DataGridViewColumn);
			}
		}

		protected override ArrayList List
		{
			get
			{
				Contract.Ensures(Contract.Result<ArrayList>() != null);
				return this.items;
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
				Contract.Ensures(Contract.Result<DataGridViewColumn>() != null);
				return default(DataGridViewColumn);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public event CollectionChangeEventHandler CollectionChanged;
	}
}