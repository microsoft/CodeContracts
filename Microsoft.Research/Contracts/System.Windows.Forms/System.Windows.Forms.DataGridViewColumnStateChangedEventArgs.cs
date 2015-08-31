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
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.ColumnStateChanged"/> event.
    /// </summary>
    
    public class DataGridViewColumnStateChangedEventArgs : EventArgs
    {
        private DataGridViewColumn dataGridViewColumn;
        private DataGridViewElementStates stateChanged;

        /// <summary>
        /// Gets the column whose state changed.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> whose state changed.
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
        /// Gets the new column state.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.
        /// </returns>
        public DataGridViewElementStates StateChanged
        {
            get
            {
                return this.stateChanged;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnStateChangedEventArgs"/> class.
        /// </summary>
        /// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> whose state has changed.</param><param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param>
        public DataGridViewColumnStateChangedEventArgs(DataGridViewColumn dataGridViewColumn, DataGridViewElementStates stateChanged)
        {
            this.dataGridViewColumn = dataGridViewColumn;
            this.stateChanged = stateChanged;
        }
    }
}

