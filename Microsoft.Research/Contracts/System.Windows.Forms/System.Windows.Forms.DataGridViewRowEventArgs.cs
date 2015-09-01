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
// Decompiled with JetBrains decompiler
// Type: System.Windows.Forms.DataGridViewRowEventArgs
// Assembly: System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: E99EB19F-1780-41E7-95D6-F9A9962C7B8D
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\System.Windows.Forms\2.0.0.0__b77a5c561934e089\System.Windows.Forms.dll

using System;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides data for row-related <see cref="T:System.Windows.Forms.DataGridView"/> events.
    /// </summary>
    
    public class DataGridViewRowEventArgs : EventArgs
    {
       
        /// <summary>
        /// Gets the <see cref="T:System.Windows.Forms.DataGridViewRow"/> associated with the event.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewRow"/> associated with the event.
        /// </returns>
        /// 
        public DataGridViewRow Row
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewRow>() != null);
                return default(DataGridViewRow);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> class.
        /// </summary>
        /// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow"/> that the event occurred for.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewRow"/> is null.</exception>
        public DataGridViewRowEventArgs(DataGridViewRow dataGridViewRow)
        {
            Contract.Requires(dataGridViewRow != null);
        }
    }
}
