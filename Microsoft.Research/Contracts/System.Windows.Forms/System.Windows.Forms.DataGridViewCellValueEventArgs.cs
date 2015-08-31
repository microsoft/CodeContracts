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
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellValueNeeded"/> and <see cref="E:System.Windows.Forms.DataGridView.CellValuePushed"/> events of the <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    
    public class DataGridViewCellValueEventArgs : EventArgs
    {
        private int rowIndex;
        private int columnIndex;
        private object val;

        /// <summary>
        /// Gets a value indicating the column index of the cell that the event occurs for.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the column containing the cell that the event occurs for.
        /// </returns>
        /// 
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>
        /// Gets a value indicating the row index of the cell that the event occurs for.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the row containing the cell that the event occurs for.
        /// </returns>
        /// 
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>
        /// Gets or sets the value of the cell that the event occurs for.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Object"/> representing the cell's value.
        /// </returns>
        /// 
        public object Value
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = value;
            }
        }

        internal DataGridViewCellValueEventArgs()
        {
            this.columnIndex = this.rowIndex = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellValueEventArgs"/> class.
        /// </summary>
        /// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param><param name="rowIndex">The index of the row containing the cell that the event occurs for.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than 0.-or-<paramref name="rowIndex"/> is less than 0.</exception>
        public DataGridViewCellValueEventArgs(int columnIndex, int rowIndex)
        {
            if (columnIndex < 0)
                throw new ArgumentOutOfRangeException("columnIndex");
            if (rowIndex < 0)
                throw new ArgumentOutOfRangeException("rowIndex");
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
        }

        internal void SetProperties(int columnIndex, int rowIndex, object value)
        {
            this.columnIndex = columnIndex;
            this.rowIndex = rowIndex;
            this.val = value;
        }
    }
}
