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
using System.Runtime.ConstrainedExecution;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.ColumnDividerDoubleClick"/> event of a <see cref="T:System.Windows.Forms.DataGridView"/>.
    /// </summary>
    public class DataGridViewColumnDividerDoubleClickEventArgs : HandledMouseEventArgs
    {
        /// <summary>
        /// The index of the column next to the column divider that was double-clicked.
        /// </summary>
        /// <returns>
        /// The index of the column next to the divider.
        /// </returns>
        public int ColumnIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnDividerDoubleClickEventArgs"/> class.
        /// </summary>
        /// <param name="columnIndex">The index of the column next to the column divider that was double-clicked. </param><param name="e">A new <see cref="T:System.Windows.Forms.HandledMouseEventArgs"/> containing the inherited event data. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than -1.</exception>
        public DataGridViewColumnDividerDoubleClickEventArgs(int columnIndex, HandledMouseEventArgs e)
          : base(e.Button, e.Clicks, e.X, e.Y, e.Delta, e.Handled)
        {
            Contract.Requires(e != null);
            Contract.Requires(columnIndex >= 0);
        }
    }
}
