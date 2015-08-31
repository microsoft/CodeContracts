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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a text box control that can be hosted in a <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell"/>.
    /// </summary>
    
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class DataGridViewTextBoxEditingControl : TextBox //, IDataGridViewEditingControl
    {
       
        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Forms.DataGridView"/> that contains the text box control.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridView"/> that contains the <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell"/> that contains this control; otherwise, null if there is no associated <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        //  public virtual DataGridView EditingControlDataGridView {get; set;}
        
        /// <summary>
        /// Gets or sets the formatted representation of the current value of the text box control.
        /// </summary>
        /// 
        /// <returns>
        /// An object representing the current value of this control.
        /// </returns>
        // public virtual object EditingControlFormattedValue { get; set; }
        
        /// <summary>
        /// Gets or sets the index of the owning cell's parent row.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the row that contains the owning cell; -1 if there is no owning row.
        /// </returns>
        // public virtual int EditingControlRowIndex
        // Note, there is no checks on the values here. {get; set;}
        
        /// <summary>
        /// Gets or sets a value indicating whether the current value of the text box control has changed.
        /// </summary>
        /// 
        /// <returns>
        /// true if the value of the control has changed; otherwise, false.
        /// </returns>
        // public virtual bool EditingControlValueChanged {get; set;}
        
        /// <summary>
        /// Gets the cursor used when the mouse pointer is over the <see cref="P:System.Windows.Forms.DataGridView.EditingPanel"/> but not over the editing control.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.Cursor"/> that represents the mouse pointer used for the editing panel.
        /// </returns>
        public virtual Cursor EditingPanelCursor
        {
            get
            {
                Contract.Ensures(Contract.Result<Cursor>() != null);
                return default(Cursor);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the cell contents need to be repositioned whenever the value changes.
        /// </summary>
        /// 
        /// <returns>
        /// true if the cell's <see cref="P:System.Windows.Forms.DataGridViewCellStyle.WrapMode"/> is set to true and the alignment property is not set to one of the <see cref="T:System.Windows.Forms.DataGridViewContentAlignment"/> values that aligns the content to the top; otherwise, false.
        /// </returns>
        // public virtual bool RepositionEditingControlOnValueChange

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxEditingControl"/> class.
        /// </summary>
        // public DataGridViewTextBoxEditingControl()

        /// <summary>
        /// Changes the control's user interface (UI) to be consistent with the specified cell style.
        /// </summary>
        /// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to use as the model for the UI.</param>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Contract.Requires(dataGridViewCellStyle != null);
        }
        
        /// <summary>
        /// Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView"/> should process.
        /// </summary>
        /// 
        /// <returns>
        /// true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.
        /// </returns>
        /// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys"/> that represents the key that was pressed.</param><param name="dataGridViewWantsInputKey">true when the <see cref="T:System.Windows.Forms.DataGridView"/> wants to process the <paramref name="keyData"/>; otherwise, false.</param>
        // public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        
        /// <summary>
        /// Retrieves the formatted value of the cell.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Object"/> that represents the formatted version of the cell contents.
        /// </returns>
        /// <param name="context">One of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts"/> values that specifies the data error context.</param>
        // public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        
        /// <summary>
        /// Prepares the currently selected cell for editing.
        /// </summary>
        /// <param name="selectAll">true to select the cell contents; otherwise, false.</param>
        // public virtual void PrepareEditingControlForEdit(bool selectAll)
        
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        // protected override void OnMouseWheel(MouseEventArgs e)
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged"/> event and notifies the <see cref="T:System.Windows.Forms.DataGridView"/> of the text change.
        /// </summary>
        /// <param name="e">The event data.</param>
        // protected override void OnTextChanged(EventArgs e)
        
        /// <summary>
        /// Processes key events.
        /// </summary>
        /// 
        /// <returns>
        /// true if the key event was handled by the editing control; otherwise, false.
        /// </returns>
        /// <param name="m">A <see cref="T:System.Windows.Forms.Message"/> indicating the key that was pressed.</param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected override bool ProcessKeyEventArgs(ref Message m)
    }
}
