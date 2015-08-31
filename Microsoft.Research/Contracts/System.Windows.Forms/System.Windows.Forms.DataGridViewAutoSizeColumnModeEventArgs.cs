// Decompiled with JetBrains decompiler
// Type: System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs
// Assembly: System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: E99EB19F-1780-41E7-95D6-F9A9962C7B8D
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\System.Windows.Forms\2.0.0.0__b77a5c561934e089\System.Windows.Forms.dll

using System;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides data for the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnModeChanged"/> event.
    /// </summary>
    public class DataGridViewAutoSizeColumnModeEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the column with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> property that changed.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> property that changed.
        /// </returns>
        public DataGridViewColumn Column {get;}
        
        // <summary>
        // Gets the previous value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> property of the column.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> value representing the previous value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> property of the <see cref="P:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs.Column"/>.
        // </returns>
        // public DataGridViewAutoSizeColumnMode PreviousMode {get;}
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs"/> class.
        // </summary>
        // <param name="dataGridViewColumn">The column with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> property that changed.</param><param name="previousMode">The previous <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> value of the column's <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> property. </param>
        // public DataGridViewAutoSizeColumnModeEventArgs(DataGridViewColumn dataGridViewColumn, DataGridViewAutoSizeColumnMode previousMode);
    }

}
