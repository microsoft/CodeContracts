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
    /// Provides data for the <see cref="E:System.Windows.Forms.Binding.Format"/> and <see cref="E:System.Windows.Forms.Binding.Parse"/> events.
    /// </summary>
    public class ConvertEventArgs : EventArgs
    {
        // <summary>
        // Gets or sets the value of the <see cref="T:System.Windows.Forms.ConvertEventArgs"/>.
        // </summary>
        // 
        // <returns>
        // The value of the <see cref="T:System.Windows.Forms.ConvertEventArgs"/>.
        // </returns>
        // public object Value {get; set;}
       
        // <summary>
        // Gets the data type of the desired value.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Type"/> of the desired value.
        // </returns>
        // public System.Type DesiredType { get; }
        

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.ConvertEventArgs"/> class.
        /// </summary>
        /// <param name="value">An <see cref="T:System.Object"/> that contains the value of the current property. </param><param name="desiredType">The <see cref="T:System.Type"/> of the value. </param>
        public ConvertEventArgs(object value, System.Type desiredType)
        {
            
        }
    }
}
