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

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellPainting"/> event.
    /// </summary>
    public class DataGridViewCellPaintingEventArgs : HandledEventArgs
    {
        
        // <summary>
        // Gets the border style of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that represents the border style of the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // public DataGridViewAdvancedBorderStyle AdvancedBorderStyle {get;}
       
        // <summary>
        // Get the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // public Rectangle CellBounds {get;}
        
        // <summary>
        // Gets the cell style of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that contains the cell style of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // public DataGridViewCellStyle CellStyle { get; }
        
        // <summary>
        // Gets the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be repainted.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Rectangle"/> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be repainted.
        // </returns>
        // public Rectangle ClipBounds {get;}
        
        /// <summary>
        /// Gets the column index of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </summary>
        /// <returns>
        /// The column index of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </returns>
        public int ColumnIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > -1);
                return default(int);
            }
        }

        // <summary>
        // Gets a string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </summary>
        // <returns>
        // A string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // public string ErrorText {get;}
      
        // <summary>
        // Gets the formatted value of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </summary>
        // <returns>
        // The formatted value of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // public object FormattedValue {get;}
        
        // <summary>
        // Gets the <see cref="T:System.Drawing.Graphics"/> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </summary>
        // <returns>
        // The <see cref="T:System.Drawing.Graphics"/> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // public Graphics Graphics
       
        // <summary>
        // The cell parts that are to be painted.
        // </summary>
        // <returns>
        // A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts"/> values specifying the parts to be painted.
        // </returns>
        // public DataGridViewPaintParts PaintParts {get;}
        
        /// <summary>
        /// Gets the row index of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </summary>
        /// <returns>
        /// The row index of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </returns>
        /// 
        public int RowIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > -1);
                return default(int);
            }
        }

        // <summary>
        // Gets the state of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </summary>
        // <returns>
        // A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that specifies the state of the cell.
        // </returns>
        // public DataGridViewElementStates State
        
        // <summary>
        // Gets the value of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </summary>
        // <returns>
        // The value of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // 
        // public object Value {get;}
       
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellPaintingEventArgs"/> class.
        /// </summary>
        /// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView"/> that contains the cell to be painted.</param><param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.</param><param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be repainted.</param><param name="cellBounds">A <see cref="T:System.Drawing.Rectangle"/> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that is being painted.</param><param name="rowIndex">The row index of the cell that is being painted.</param><param name="columnIndex">The column index of the cell that is being painted.</param><param name="cellState">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that specifies the state of the cell.</param><param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that is being painted.</param><param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that is being painted.</param><param name="errorText">An error message that is associated with the cell.</param><param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that contains formatting and style information about the cell.</param><param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that contains border styles for the cell that is being painted.</param><param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts"/> values specifying the parts to paint.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridView"/> is null.-or-<paramref name="graphics"/> is null.-or-<paramref name="cellStyle"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="paintParts"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts"/> values.</exception>
        public DataGridViewCellPaintingEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, int columnIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            Contract.Requires(dataGridView != null);
            Contract.Requires(graphics != null);
            Contract.Requires(cellStyle != null);
            Contract.Requires((paintParts & ~DataGridViewPaintParts.All) == DataGridViewPaintParts.None);
        }
        
        /// <summary>
        /// Paints the specified parts of the cell for the area in the specified bounds.
        /// </summary>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView"/> to be painted.</param><param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts"/> values specifying the parts to paint.</param><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex"/> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-<see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex"/> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        public void Paint(Rectangle clipBounds, DataGridViewPaintParts paintParts)
        {
            Contract.Requires(!(RowIndex < -1));
            Contract.Requires(!(ColumnIndex < -1));
        }

        /// <summary>
        /// Paints the cell background for the area in the specified bounds.
        /// </summary>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView"/> to be painted.</param><param name="cellsPaintSelectionBackground">true to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor"/> property of the <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle"/>; false to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor"/> property of the <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle"/>.</param><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex"/> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-<see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex"/> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        public void PaintBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
        {
            Contract.Requires(!(RowIndex < -1));
            Contract.Requires(!(ColumnIndex < -1));
        }

        /// <summary>
        /// Paints the cell content for the area in the specified bounds.
        /// </summary>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView"/> to be painted.</param><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex"/> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-<see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex"/> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        public void PaintContent(Rectangle clipBounds)
        {
            Contract.Requires(!(RowIndex < -1));
            Contract.Requires(!(ColumnIndex < -1));
        }
    }
}
