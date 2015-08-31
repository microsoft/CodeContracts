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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a shortcut menu.
    /// </summary>
    public class ContextMenuStrip : ToolStripDropDownMenu
    {
        /// <summary>
        /// Gets the last control that caused this <see cref="T:System.Windows.Forms.ContextMenuStrip"/> to be displayed.
        /// </summary>
        /// 
        /// <returns>
        /// The control that caused this <see cref="T:System.Windows.Forms.ContextMenuStrip"/> to be displayed.
        /// </returns>
        public Control SourceControl
        {
            get
            {
                Contract.Ensures(Contract.Result<Control>() != null);
                return default(Control);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenuStrip"/> class and associates it with the specified container.
        /// </summary>
        /// <param name="container">A component that implements <see cref="T:System.ComponentModel.IContainer"/> that is the container of the <see cref="T:System.Windows.Forms.ContextMenuStrip"/>.</param>
        public ContextMenuStrip(IContainer container)
        {
            Contract.Requires(container != null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenuStrip"/> class.
        /// </summary>
        public ContextMenuStrip()
        {
        }

        // <summary>
        // Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ContextMenuStrip"/> and optionally releases the managed resources.
        // </summary>
        // <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        // protected override void Dispose(bool disposing)
        
        
        // <param name="visible">true to make the control visible; otherwise, false.</param>
        // protected override void SetVisibleCore(bool visible)
    }
}
