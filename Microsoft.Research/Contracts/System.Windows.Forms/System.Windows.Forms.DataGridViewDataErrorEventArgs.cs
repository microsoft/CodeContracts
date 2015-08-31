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
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event.
    /// </summary>
    
    public class DataGridViewDataErrorEventArgs : DataGridViewCellCancelEventArgs
    {

        /// <summary>
        /// Gets details about the state of the <see cref="T:System.Windows.Forms.DataGridView"/> when the error occurred.
        /// </summary>
        /// 
        /// <returns>
        /// A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts"/> values that specifies the context in which the error occurred.
        /// </returns>
        /// 
        // public DataGridViewDataErrorContexts Context {get;}

        /// <summary>
        /// Gets the exception that represents the error.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Exception"/> that represents the error.
        /// </returns>
        /// 
        // public Exception Exception {get;}

        /// <summary>
        /// Gets or sets a value indicating whether to throw the exception after the <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventHandler"/> delegate is finished with it.
        /// </summary>
        /// 
        /// <returns>
        /// true if the exception should be thrown; otherwise, false. The default is false.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">When setting this property to true, the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.Exception"/> property value is null.</exception>
        // public bool ThrowException { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The exception that occurred.</param><param name="columnIndex">The column index of the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.DataError"/>.</param><param name="rowIndex">The row index of the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.DataError"/>.</param><param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts"/> values indicating the context in which the error occurred. </param>
        public DataGridViewDataErrorEventArgs(Exception exception, int columnIndex, int rowIndex,
            DataGridViewDataErrorContexts context)
            : base(columnIndex, rowIndex)
        {
            
        }
    }
}
