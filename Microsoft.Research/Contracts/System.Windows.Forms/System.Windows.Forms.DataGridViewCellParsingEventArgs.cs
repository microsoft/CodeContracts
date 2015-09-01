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
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellParsing"/> event of a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    
    public class DataGridViewCellParsingEventArgs : ConvertEventArgs
    {
        /// <summary>
        /// Gets the row index of the cell that requires parsing.
        /// </summary>
        /// 
        /// <returns>
        /// The row index of the cell that was changed.
        /// </returns>
        /// 
        public int RowIndex {get;}

        /// <summary>
        /// Gets the column index of the cell data that requires parsing.
        /// </summary>
        /// 
        /// <returns>
        /// The column index of the cell that was changed.
        /// </returns>
        /// 
        public int ColumnIndex {get;}

        // <summary>
        // Gets or sets the style applied to the edited cell.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents the current style of the cell being edited. The default value is the value of the cell <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle"/> property.
        // </returns>
        // public DataGridViewCellStyle InheritedCellStyle {get; set;}

        // <summary>
        // Gets or sets a value indicating whether a cell's value has been successfully parsed.
        // </summary>
        // 
        // <returns>
        // true if the cell's value has been successfully parsed; otherwise, false. The default is false.
        // </returns>
        // 
        // public bool ParsingApplied {get; set;}


        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> class.
        /// </summary>
        /// <param name="rowIndex">The row index of the cell that was changed.</param><param name="columnIndex">The column index of the cell that was changed.</param><param name="value">The new value.</param><param name="desiredType">The type of the new value.</param><param name="inheritedCellStyle">The style applied to the cell that was changed.</param>
        public DataGridViewCellParsingEventArgs(int rowIndex, int columnIndex, object value, System.Type desiredType,
            DataGridViewCellStyle inheritedCellStyle)
            : base(value, desiredType)
        {
            
        }
    }
}
