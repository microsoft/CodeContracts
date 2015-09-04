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
using System.ComponentModel;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides the base class for elements of a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    public class DataGridViewElement
    {

        /// <summary>
        /// Gets the user interface (UI) state of the element.
        /// </summary>
        /// <returns>
        /// A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values representing the state.
        /// </returns>
        public virtual DataGridViewElementStates State { get { return default(DataGridViewElementStates); } }

        /// <summary>
        /// Gets the <see cref="T:System.Windows.Forms.DataGridView"/> control associated with this element.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridView"/> control that contains this element. The default is null.
        /// </returns>
        public DataGridView DataGridView { get { return default(DataGridView); } }

        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewElement"/> class.
        // </summary>
        // public DataGridViewElement()

        // <summary>
        // Called when the element is associated with a different <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </summary>
        // protected virtual void OnDataGridViewChanged()

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.CellClick"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param>
        // protected void RaiseCellClick(DataGridViewCellEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentClick"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param>
        // protected void RaiseCellContentClick(DataGridViewCellEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentDoubleClick"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param>
        // protected void RaiseCellContentDoubleClick(DataGridViewCellEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValueChanged"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param>
        // protected void RaiseCellValueChanged(DataGridViewCellEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> that contains the event data. </param>
        // protected void RaiseDataError(DataGridViewDataErrorEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        // protected void RaiseMouseWheel(MouseEventArgs e)
    }
}

