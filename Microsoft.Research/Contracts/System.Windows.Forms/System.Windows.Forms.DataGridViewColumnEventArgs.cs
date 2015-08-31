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
    /// Provides data for column-related events of a <see cref="T:System.Windows.Forms.DataGridView"/>.
    /// </summary>
    
    public class DataGridViewColumnEventArgs : EventArgs
    {
        private DataGridViewColumn dataGridViewColumn;

        /// <summary>
        /// Gets the column that the event occurs for.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> that the event occurs for.
        /// </returns>
        /// 
        public DataGridViewColumn Column
        {
            get
            {
                return this.dataGridViewColumn;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> class.
        /// </summary>
        /// <param name="dataGridViewColumn">The column that the event occurs for.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewColumn"/> is null.</exception>
        public DataGridViewColumnEventArgs(DataGridViewColumn dataGridViewColumn)
        {
            if (dataGridViewColumn == null)
                throw new ArgumentNullException("dataGridViewColumn");
            this.dataGridViewColumn = dataGridViewColumn;
        }
    }
}
