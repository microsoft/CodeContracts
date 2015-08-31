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
    /// Provides data for mouse events raised by a <see cref="T:System.Windows.Forms.DataGridView"/> whenever the mouse is moved within a <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
    /// </summary>
    
    public class DataGridViewCellMouseEventArgs : MouseEventArgs
    {
       
        /// <summary>
        /// Gets the zero-based column index of the cell.
        /// </summary>
        /// 
        /// <returns>
        /// An integer specifying the column index.
        /// </returns>
        public int ColumnIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > -1);
                return default(int);
            }
        }

        /// <summary>
        /// Gets the zero-based row index of the cell.
        /// </summary>
        /// 
        /// <returns>
        /// An integer specifying the row index.
        /// </returns>
        /// 
        public int RowIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > -1);
                return default(int);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> class.
        /// </summary>
        /// <param name="columnIndex">The cell's zero-based column index.</param><param name="rowIndex">The cell's zero-based row index.</param><param name="localX">The x-coordinate of the mouse, in pixels.</param><param name="localY">The y-coordinate of the mouse, in pixels.</param><param name="e">The originating <see cref="T:System.Windows.Forms.MouseEventArgs"/>.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than -1.-or-<paramref name="rowIndex"/> is less than -1.</exception>
        public DataGridViewCellMouseEventArgs(int columnIndex, int rowIndex, int localX, int localY, MouseEventArgs e)
          : base(e.Button, e.Clicks, localX, localY, e.Delta)
        {

            Contract.Requires(columnIndex >= -1);
            Contract.Requires(rowIndex >= -1);
        }
    }
}

