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
  // Summary:
  //     Defines the base class for controls, which are components with visual representation.
  public class ControlBindingsCollection : BindingsCollection
  {
    // <summary>
    // Gets the <see cref="T:System.Windows.Forms.IBindableComponent"/> the binding collection belongs to.
    // </summary>
    // 
    // <returns>
    // The <see cref="T:System.Windows.Forms.IBindableComponent"/> the binding collection belongs to.
    // </returns>
    // public IBindableComponent BindableComponent { get; }
        
    // <summary>
    // Gets the control that the collection belongs to.
    // </summary>
    // 
    // <returns>
    // The <see cref="T:System.Windows.Forms.Control"/> that the collection belongs to.
    // </returns>
    // public Control Control { get; }
        
    // <summary>
    // Gets the <see cref="T:System.Windows.Forms.Binding"/> specified by the control's property name.
    // </summary>
    // 
    // <returns>
    // The <see cref="T:System.Windows.Forms.Binding"/> that binds the specified control property to a data source.
    // </returns>
    // <param name="propertyName">The name of the property on the data-bound control. </param><filterpriority>1</filterpriority>
    // public Binding this[string propertyName] { get; } 
        
    // <summary>
    // Gets or sets the default <see cref="P:System.Windows.Forms.Binding.DataSourceUpdateMode"/> for a <see cref="T:System.Windows.Forms.Binding"/> in the collection.
    // </summary>
    // 
    // <returns>
    // One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode"/> values.
    // </returns>
    // public DataSourceUpdateMode DefaultDataSourceUpdateMode { get; set; }
        
    // <summary>
    // Initializes a new instance of the <see cref="T:System.Windows.Forms.ControlBindingsCollection"/> class with the specified bindable control.
    // </summary>
    // <param name="control">The <see cref="T:System.Windows.Forms.IBindableComponent"/> the binding collection belongs to.</param>
    // public ControlBindingsCollection(IBindableComponent control)
       
    /// <summary>
    /// Adds the specified <see cref="T:System.Windows.Forms.Binding"/> to the collection.
    /// </summary>
    /// <param name="binding">The <see cref="T:System.Windows.Forms.Binding"/> to add. </param><exception cref="T:System.ArgumentNullException">The <paramref name="binding"/> is null. </exception><exception cref="T:System.ArgumentException">The control property is already data-bound. </exception><exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.Binding"/> does not specify a valid column of the <see cref="P:System.Windows.Forms.Binding.DataSource"/>. </exception><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
    public void Add(Binding binding)
    {
        Contract.Requires(binding != null);
        Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
    }

    /// <summary>
    /// Creates a <see cref="T:System.Windows.Forms.Binding"/> using the specified control property name, data source, and data member, and adds it to the collection.
    /// </summary>
    /// 
    /// <returns>
    /// The newly created <see cref="T:System.Windows.Forms.Binding"/>.
    /// </returns>
    /// <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> that represents the data source. </param><param name="dataMember">The property or list to bind to. </param><exception cref="T:System.ArgumentNullException">The <paramref name="binding"/> is null. </exception><exception cref="T:System.Exception">The <paramref name="propertyName"/> is already data-bound. </exception><exception cref="T:System.Exception">The <paramref name="dataMember"/> doesn't specify a valid member of the <paramref name="dataSource"/>. </exception><filterpriority>1</filterpriority>
    public Binding Add(string propertyName, object dataSource, string dataMember)
    {
        Contract.Requires(dataSource != null);
        Contract.Ensures(Contract.Result<Binding>() != null);
        Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
        return default(Binding);
    }

    /// <summary>
    /// Creates a binding with the specified control property name, data source, data member, and information about whether formatting is enabled, and adds the binding to the collection.
    /// </summary>
    /// 
    /// <returns>
    /// The newly created <see cref="T:System.Windows.Forms.Binding"/>.
    /// </returns>
    /// <param name="propertyName">The name of the control property to bind.</param><param name="dataSource">An <see cref="T:System.Object"/> representing the data source. </param><param name="dataMember">The property or list to bind to.</param><param name="formattingEnabled">true to format the displayed data; otherwise, false</param><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control.-or-The property given is a read-only property.</exception><exception cref="T:System.Exception">If formatting is disabled and the <paramref name="propertyName"/> is neither a valid property of a control nor an empty string (""). </exception><filterpriority>1</filterpriority>
    public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled)
    {
        Contract.Requires(dataSource != null);
        Contract.Ensures(Contract.Result<Binding>() != null);
        Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
        return default(Binding);
    }

    /// <summary>
    /// Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting, propagating values to the data source based on the specified update setting, and adding the binding to the collection.
    /// </summary>
    /// 
    /// <returns>
    /// The newly created <see cref="T:System.Windows.Forms.Binding"/>.
    /// </returns>
    /// <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> representing the data source. </param><param name="dataMember">The property or list to bind to.</param><param name="formattingEnabled">true to format the displayed data; otherwise, false.</param><param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode"/> values.</param><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control or is read-only.-or-The specified data member does not exist on the data source.-or-The data source, data member, or control property specified are associated with another binding in the collection.</exception>
    public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode)
    {
        Contract.Requires(dataSource != null);
        Contract.Ensures(Contract.Result<Binding>() != null);
        Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
        return default(Binding);
    }

    /// <summary>
    /// Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting, propagating values to the data source based on the specified update setting, setting the property to the specified value when <see cref="T:System.DBNull"/> is returned from the data source, and adding the binding to the collection.
    /// </summary>
    /// 
    /// <returns>
    /// The newly created <see cref="T:System.Windows.Forms.Binding"/>
    /// </returns>
    /// <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> representing the data source. </param><param name="dataMember">The property or list to bind to.</param><param name="formattingEnabled">true to format the displayed data; otherwise, false.</param><param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode"/> values.</param><param name="nullValue">When the data source has this value, the bound property is set to <see cref="T:System.DBNull"/>. </param><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control or is read-only.-or-The specified data member does not exist on the data source.-or-The data source, data member, or control property specified are associated with another binding in the collection.</exception>
    public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue)
    {
        Contract.Requires(dataSource != null);
        Contract.Ensures(Contract.Result<Binding>() != null);
        Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
        return default(Binding);
    }

    /// <summary>
    /// Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting with the specified format string, propagating values to the data source based on the specified update setting, setting the property to the specified value when <see cref="T:System.DBNull"/> is returned from the data source, and adding the binding to the collection.
    /// </summary>
    /// 
    /// <returns>
    /// The newly created <see cref="T:System.Windows.Forms.Binding"/>
    /// </returns>
    /// <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> representing the data source. </param><param name="dataMember">The property or list to bind to.</param><param name="formattingEnabled">true to format the displayed data; otherwise, false.</param><param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode"/> values.</param><param name="nullValue">When the data source has this value, the bound property is set to <see cref="T:System.DBNull"/>. </param><param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control or is read-only.-or-The specified data member does not exist on the data source.-or-The data source, data member, or control property specified are associated with another binding in the collection.</exception>
    public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue, string formatString)
    {
        Contract.Requires(dataSource != null);
        Contract.Ensures(Contract.Result<Binding>() != null);
        Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
        return default(Binding);
    }

    /// <summary>
    /// Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting with the specified format string, propagating values to the data source based on the specified update setting, setting the property to the specified value when <see cref="T:System.DBNull"/> is returned from the data source, setting the specified format provider, and adding the binding to the collection.
    /// </summary>
    /// 
    /// <returns>
    /// The newly created <see cref="T:System.Windows.Forms.Binding"/>.
    /// </returns>
    /// <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> representing the data source. </param><param name="dataMember">The property or list to bind to.</param><param name="formattingEnabled">true to format the displayed data; otherwise, false.</param><param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode"/> values.</param><param name="nullValue">When the data source has this value, the bound property is set to <see cref="T:System.DBNull"/>. </param><param name="formatString">One or more format specifier characters that indicate how a value is to be displayed</param><param name="formatInfo">An implementation of <see cref="T:System.IFormatProvider"/> to override default formatting behavior.</param><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control or is read-only.-or-The specified data member does not exist on the data source.-or-The data source, data member, or control property specified are associated with another binding in the collection.</exception>
    public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue, string formatString, IFormatProvider formatInfo)
    {
        Contract.Requires(dataSource != null);
        Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
        return default(Binding);
    }
        
    // <summary>
    // Clears the collection of any bindings.
    // </summary>
    // public new void Clear();
        
    /// <summary>
    /// Deletes the specified <see cref="T:System.Windows.Forms.Binding"/> from the collection.
    /// </summary>
    /// <param name="binding">The <see cref="T:System.Windows.Forms.Binding"/> to remove. </param><exception cref="T:System.NullReferenceException">The <paramref name="binding"/> is null. </exception><filterpriority>1</filterpriority>
    public new void Remove(Binding binding)
    {
        Contract.Requires(binding != null);
        // Note, the binding may not be part of the collection, so we can't ensure the collection count is reduced.
    }

    /// <summary>
    /// Deletes the <see cref="T:System.Windows.Forms.Binding"/> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove. </param><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index"/> value is less than 0, or it is greater than the number of bindings in the collection. </exception><filterpriority>1</filterpriority>
    public new void RemoveAt(int index)
    {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
        Contract.Ensures(this.Count == Contract.OldValue(this.Count) - 1);
    }
  }
}
