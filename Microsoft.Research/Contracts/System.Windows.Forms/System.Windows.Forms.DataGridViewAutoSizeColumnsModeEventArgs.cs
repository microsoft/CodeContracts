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
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnsModeChanged"/> event.
    /// </summary>
    public class DataGridViewAutoSizeColumnsModeEventArgs : EventArgs
    {
        /// <summary>
        /// Gets an array of the previous values of the column <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> properties.
        /// </summary>
        /// 
        /// <returns>
        /// An array of <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> values representing the previous values of the column <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> properties.
        /// </returns>
        public DataGridViewAutoSizeColumnMode[] PreviousModes { get; }
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs"/> class.
        // </summary>
        // <param name="previousModes">An array of <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> values representing the previous <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> property values of each column. </param>
        // public DataGridViewAutoSizeColumnsModeEventArgs(DataGridViewAutoSizeColumnMode[] previousModes);
       
    }
}
