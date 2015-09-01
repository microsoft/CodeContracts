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
using System.ComponentModel;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellValidating"/> event of a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    public class DataGridViewCellValidatingEventArgs : CancelEventArgs
    {
        private int rowIndex;
        private int columnIndex;
        private object formattedValue;

        /// <summary>
        /// Gets the column index of the cell that needs to be validated.
        /// </summary>
        /// 
        /// <returns>
        /// A zero-based integer that specifies the column index of the cell that needs to be validated.
        /// </returns>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>
        /// Gets the formatted contents of the cell that needs to be validated.
        /// </summary>
        /// 
        /// <returns>
        /// A reference to the formatted value.
        /// </returns>
        public object FormattedValue
        {
            get
            {
                return this.formattedValue;
            }
        }

        /// <summary>
        /// Gets the row index of the cell that needs to be validated.
        /// </summary>
        /// 
        /// <returns>
        /// A zero-based integer that specifies the row index of the cell that needs to be validated.
        /// </returns>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        //internal DataGridViewCellValidatingEventArgs(int columnIndex, int rowIndex, object formattedValue)
    }
}
