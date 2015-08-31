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
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellFormatting"/> event of a <see cref="T:System.Windows.Forms.DataGridView"/>.
    /// </summary>
    
    public class DataGridViewCellFormattingEventArgs : ConvertEventArgs
    {
       /// <summary>
        /// Gets or sets the style of the cell that is being formatted.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents the display style of the cell being formatted. The default is the value of the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle"/> property.
        /// </returns>
        // public DataGridViewCellStyle CellStyle { get; set; }
        
        /// <summary>
        /// Gets the column index of the cell that is being formatted.
        /// </summary>
        /// 
        /// <returns>
        /// The column index of the cell that is being formatted.
        /// </returns>
        public int ColumnIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= -1);
                return default(int);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the cell value has been successfully formatted.
        /// </summary>
        /// 
        /// <returns>
        /// true if the formatting for the cell value has been handled; otherwise, false. The default value is false.
        /// </returns>
        // public bool FormattingApplied
       
        /// <summary>
        /// Gets the row index of the cell that is being formatted.
        /// </summary>
        /// 
        /// <returns>
        /// The row index of the cell that is being formatted.
        /// </returns>
        public int RowIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= -1);
                return default(int);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> class.
        /// </summary>
        /// <param name="columnIndex">The column index of the cell that caused the event.</param><param name="rowIndex">The row index of the cell that caused the event.</param><param name="value">The cell's contents.</param><param name="desiredType">The type to convert <paramref name="value"/> to. </param><param name="cellStyle">The style of the cell that caused the event.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than -1-or-<paramref name="rowIndex"/> is less than -1.</exception>
        public DataGridViewCellFormattingEventArgs(int columnIndex, int rowIndex, object value, System.Type desiredType, DataGridViewCellStyle cellStyle)
          : base(value, desiredType)
        {
            Contract.Requires(columnIndex >= -1);
            Contract.Requires(rowIndex >= -1);
        }
    }
}
