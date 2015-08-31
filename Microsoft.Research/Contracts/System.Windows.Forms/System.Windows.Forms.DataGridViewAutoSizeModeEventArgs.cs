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
    /// Provides data for the <see cref="T:System.Windows.Forms.DataGridView"/><see cref="E:System.Windows.Forms.DataGridView.AutoSizeRowsModeChanged"/> and <see cref="E:System.Windows.Forms.DataGridView.RowHeadersWidthSizeModeChanged"/> events.
    /// </summary>
    
    public class DataGridViewAutoSizeModeEventArgs : EventArgs
    {
        // <summary>
        // Gets a value specifying whether the <see cref="T:System.Windows.Forms.DataGridView"/> was previously set to automatically resize.
        // </summary>
        // 
        // <returns>
        // true if the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeRowsMode"/> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode"/> value other than <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None"/> or the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersWidthSizeMode"/> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> value other than <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing"/> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing"/>; otherwise, false.
        // </returns>
        // 
        // public bool PreviousModeAutoSized
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs"/> class.
        // </summary>
        // <param name="previousModeAutoSized">true if the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeRowsMode"/> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode"/> value other than <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None"/> or the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersWidthSizeMode"/> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> value other than <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing"/> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing"/>; otherwise, false.</param>
        // public DataGridViewAutoSizeModeEventArgs(bool previousModeAutoSized);
    }
}
