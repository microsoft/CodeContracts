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
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellStateChanged"/> event.
    /// </summary>
    
    public class DataGridViewCellStateChangedEventArgs : EventArgs
    {
       
        /// <summary>
        /// Gets the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that has a changed state.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCell"/> whose state has changed.
        /// </returns>
        /// 
        // public DataGridViewCell Cell {get;}
       
        /// <summary>
        /// Gets the state that has changed on the cell.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values indicating the state that has changed on the cell.
        /// </returns>
        // public DataGridViewElementStates StateChanged {get;}
       

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStateChangedEventArgs"/> class.
        /// </summary>
        /// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell"/> that has a changed state.</param><param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values indicating the state that has changed on the cell.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewCell"/> is null.</exception>
        public DataGridViewCellStateChangedEventArgs(DataGridViewCell dataGridViewCell, DataGridViewElementStates stateChanged)
        {
            Contract.Requires(dataGridViewCell != null);
        }
    }
}