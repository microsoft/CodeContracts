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
		private class RowArrayList : ArrayList
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

			private void CustomQuickSort(int left, int right)
			{
				
			}

			public void CustomSort(DataGridViewRowCollection.RowComparer rowComparer)
			{
				Contract.Requires(rowComparer != null);
				Contract.Ensures(this.rowComparer != null);
			}

			private object Pivot(int left, int center, int right)
			{
				return default(object);
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

			internal int CompareObjects(object value1, object value2, int rowIndex1, int rowIndex2)
			{
				return default(int);
			}

			internal object GetComparedObject(int rowIndex)
			{
				return default(object);
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

		//public event CollectionChangeEventHandler CollectionChanged;

		public DataGridViewRowCollection(DataGridView dataGridView)
		{
			Contract.Requires(dataGridView != null);
			Contract.Ensures(this.dataGridView != null);
			Contract.Ensures(this.rowStates != null);
			Contract.Ensures(this.items != null);
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

		internal int AddCopiesInternal(int indexSource, int count)
		{
			return default(int);
		}

		internal int AddCopiesInternal(int indexSource, int count, DataGridViewElementStates dgvesAdd, DataGridViewElementStates dgvesRemove)
		{
			Contract.Requires(indexSource >= 0);
			Contract.Requires(count > indexSource);
			Contract.Requires(count > 0);
			return default(int);
		}

		private int AddCopiesPrivate(DataGridViewRow rowTemplate, DataGridViewElementStates rowTemplateState, int count)
		{
			Contract.Requires(rowTemplate != null);
			return default(int);
		}

		public virtual int AddCopy(int indexSource)
		{
			return default(int);
		}

		internal int AddCopyInternal(int indexSource, DataGridViewElementStates dgvesAdd, DataGridViewElementStates dgvesRemove, bool newRow)
		{
			return default(int);
		}

		private int AddDuplicateRow(DataGridViewRow rowTemplate, bool newRow)
		{
			Contract.Requires(rowTemplate != null);
			return default(int);
		}

		internal int AddInternal(DataGridViewRow dataGridViewRow)
		{
			Contract.Requires(dataGridViewRow != null);
			Contract.Ensures(Contract.Result<int>() >= 0);
			return default(int);
		}

		internal int AddInternal(bool newRow, object[] values)
		{
			Contract.Ensures(Contract.Result<int>() >= 0);
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

		internal void ClearInternal(bool recreateNewRow)
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

		internal int DisplayIndexToRowIndex(int visibleRowIndex)
		{
			return default(int);
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

		internal int GetNextRow(int indexStart, DataGridViewElementStates includeFilter, int skipRows)
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

		internal int GetRowCount(DataGridViewElementStates includeFilter, int fromRowIndex, int toRowIndex)
		{
			Contract.Ensures(Contract.Result<int>() >= 0);
			return default(int);
		}

		public int GetRowsHeight(DataGridViewElementStates includeFilter)
		{
			return default(int);
		}

		internal int GetRowsHeight(DataGridViewElementStates includeFilter, int fromRowIndex, int toRowIndex)
		{
			return default(int);
		}

		private bool GetRowsHeightExceedLimit(DataGridViewElementStates includeFilter, int fromRowIndex, int toRowIndex, int heightLimit)
		{
			return default(bool);
		}

		public virtual DataGridViewElementStates GetRowState(int rowIndex)
		{
			Contract.Requires(rowIndex >= 0);
			Contract.Requires(rowIndex < this.items.Count);
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

		private void InsertCopiesPrivate(int indexSource, int indexDestination, int count)
		{
			Contract.Requires(indexSource >= 0);
			Contract.Requires(this.Count > indexSource);
			Contract.Requires(indexDestination >= 0);
			Contract.Requires(this.Count >= indexDestination);
			Contract.Requires(count > 0);
		}

		private void InsertCopiesPrivate(DataGridViewRow rowTemplate, DataGridViewElementStates rowTemplateState, int indexDestination, int count)
		{
			Contract.Requires(rowTemplate != null);
		}

		public virtual void InsertCopy(int indexSource, int indexDestination)
		{
			Contract.Requires(indexSource >= 0);
			Contract.Requires(this.Count > indexSource);
			Contract.Requires(indexDestination >= 0);
			Contract.Requires(this.Count >= indexDestination);
		}

		private void InsertDuplicateRow(int indexDestination, DataGridViewRow rowTemplate, bool firstInsertion, ref Point newCurrentCell)
		{
			Contract.Requires(rowTemplate != null);
		}

		internal void InsertInternal(int rowIndex, DataGridViewRow dataGridViewRow)
		{
			Contract.Requires(rowIndex >= 0);
			Contract.Requires(this.Count >= rowIndex);
			Contract.Requires(dataGridViewRow != null);
		}

		internal void InsertInternal(int rowIndex, DataGridViewRow dataGridViewRow, bool force)
		{
			Contract.Requires(dataGridViewRow != null);
		}

		public virtual void InsertRange(int rowIndex, params DataGridViewRow[] dataGridViewRows)
		{
			Contract.Requires(dataGridViewRows != null);
			Contract.Requires(Contract.ForAll(dataGridViewRows, (row) => { return row != null; }));
			Contract.Requires(rowIndex >= 0);
			Contract.Requires(this.Count >= rowIndex);
		}

		internal void InvalidateCachedRowCount(DataGridViewElementStates includeFilter)
		{

		}

		internal void InvalidateCachedRowCounts()
		{

		}

		internal void InvalidateCachedRowsHeight(DataGridViewElementStates includeFilter)
		{

		}

		internal void InvalidateCachedRowsHeights()
		{

		}

		protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
		{

		}

		private void OnCollectionChanged(CollectionChangeEventArgs e, int rowIndex, int rowCount)
		{

		}

		private void OnCollectionChanged(CollectionChangeEventArgs e, int rowIndex, int rowCount, bool changeIsDeletion, bool changeIsInsertion, bool recreateNewRow, Point newCurrentCell)
		{
			
		}

		private void OnCollectionChanged_PostNotification(CollectionChangeAction cca, int rowIndex, int rowCount, DataGridViewRow dataGridViewRow, bool changeIsDeletion, bool changeIsInsertion, bool recreateNewRow, Point newCurrentCell)
		{

		}

		private void OnCollectionChanged_PreNotification(CollectionChangeAction cca, int rowIndex, int rowCount, ref DataGridViewRow dataGridViewRow, bool changeIsInsertion)
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

		internal void RemoveAtInternal(int index, bool force)
		{

		}

		private static bool RowHasValueOrToolTipText(DataGridViewRow dataGridViewRow)
		{
			return default(bool);
		}

		internal bool RowIsSharable(int index)
		{
			return default(bool);
		}

		internal void SetRowState(int rowIndex, DataGridViewElementStates state, bool value)
		{

		}

		public DataGridViewRow SharedRow(int rowIndex)
		{
			Contract.Ensures(Contract.Result<DataGridViewRow>() != null);
			return default(DataGridViewRow);
		}

		internal DataGridViewElementStates SharedRowState(int rowIndex)
		{
			return default(DataGridViewElementStates);
		}

		internal void Sort(IComparer customComparer, bool ascending)
		{

		}

		internal void SwapSortedRows(int rowIndex1, int rowIndex2)
		{

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

		private void UnshareRow(int rowIndex)
		{
			
		}

		private void UpdateRowCaches(int rowIndex, ref DataGridViewRow dataGridViewRow, bool adding)
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

		internal bool IsCollectionChangedListenedTo
		{
			get
			{
				return default(bool);
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

		internal ArrayList SharedList
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