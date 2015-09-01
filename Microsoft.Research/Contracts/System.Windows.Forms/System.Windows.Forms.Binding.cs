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
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
    // <summary>
    // Represents the simple binding between the property value of an object and the property value of a control.
    // </summary>
    //
    public class Binding
    {
       
        // <summary>
        // Gets the data source for this binding.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Object"/> that represents the data source.
        // </returns>
        // public object DataSource {get;}
        
        // <summary>
        // Gets an object that contains information about this binding based on the <paramref name="dataMember"/> parameter in the <see cref="Overload:System.Windows.Forms.Binding.#ctor"/> constructor.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.BindingMemberInfo"/> that contains information about this <see cref="T:System.Windows.Forms.Binding"/>.
        // </returns>
        //
        // public BindingMemberInfo BindingMemberInfo {get;}
        
        /// <summary>
        /// Gets the control the <see cref="T:System.Windows.Forms.Binding"/> is associated with.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.IBindableComponent"/> the <see cref="T:System.Windows.Forms.Binding"/> is associated with.
        /// </returns>
        ///<PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
        /// [DefaultValue(null)]
        public IBindableComponent BindableComponent {get;}
        
        // <summary>
        // Gets the control that the binding belongs to.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Windows.Forms.Control"/> that the binding belongs to.
        // </returns>
        //<PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
        // [DefaultValue(null)]
        // public Control Control {get;}
        
        // <summary>
        // Gets a value indicating whether the binding is active.
        // </summary>
        // 
        // <returns>
        // true if the binding is active; otherwise, false.
        // </returns>
        //
        //  public bool IsBinding {get;}
        
        // <summary>
        // Gets the <see cref="T:System.Windows.Forms.BindingManagerBase"/> for this <see cref="T:System.Windows.Forms.Binding"/>.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Windows.Forms.BindingManagerBase"/> that manages this <see cref="T:System.Windows.Forms.Binding"/>.
        // </returns>
        //
        // public BindingManagerBase BindingManagerBase {get;}
        
        // <summary>
        // Gets or sets the name of the control's data-bound property.
        // </summary>
        // 
        // <returns>
        // The name of a control property to bind to.
        // </returns>
        //
        // [DefaultValue("")]
        // public string PropertyName {get;}
        
        // <summary>
        // Gets or sets a value indicating whether type conversion and formatting is applied to the control property data.
        // </summary>
        // 
        // <returns>
        // true if type conversion and formatting of control property data is enabled; otherwise, false. The default is false.
        // </returns>
        //
        // [DefaultValue(false)]
        // public bool FormattingEnabled {get; set;}
        
        // <summary>
        // Gets or sets the <see cref="T:System.IFormatProvider"/> that provides custom formatting behavior.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.IFormatProvider"/> implementation that provides custom formatting behavior.
        // </returns>
        //
        // [DefaultValue(null)]
        //  public IFormatProvider FormatInfo {get; set;}
       
        // <summary>
        // Gets or sets the format specifier characters that indicate how a value is to be displayed.
        // </summary>
        // 
        // <returns>
        // The string of format specifier characters that indicate how a value is to be displayed.
        // </returns>
        //
        // public string FormatString {get; set;}
        
        // <summary>
        // Gets or sets the <see cref="T:System.Object"/> to be set as the control property when the data source contains a <see cref="T:System.DBNull"/> value.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Object"/> to be set as the control property when the data source contains a <see cref="T:System.DBNull"/> value. The default is null.
        // </returns>
        //
        // public object NullValue {get; set;}
        
        // <summary>
        // Gets or sets the value to be stored in the data source if the control value is null or empty.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Object"/> to be stored in the data source when the control property is empty or null. The default is <see cref="T:System.DBNull"/> for value types and null for non-value types.
        // </returns>
        // public object DataSourceNullValue {get; set;}
       
        // <summary>
        // Gets or sets when changes to the data source are propagated to the bound control property.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.ControlUpdateMode"/> values. The default is <see cref="F:System.Windows.Forms.ControlUpdateMode.OnPropertyChanged"/>.
        // </returns>
        // [DefaultValue(ControlUpdateMode.OnPropertyChanged)]
        // public ControlUpdateMode ControlUpdateMode {get; set;}
       
        // <summary>
        // Gets or sets a value that indicates when changes to the bound control property are propagated to the data source.
        // </summary>
        // 
        // <returns>
        // A value that indicates when changes are propagated. The default is <see cref="F:System.Windows.Forms.DataSourceUpdateMode.OnValidation"/>.
        // </returns>
        // [DefaultValue(DataSourceUpdateMode.OnValidation)]
        // public DataSourceUpdateMode DataSourceUpdateMode {get; set;}
        
        // <summary>
        // Occurs when the <see cref="P:System.Windows.Forms.Binding.FormattingEnabled"/> property is set to true and a binding operation is complete, such as when data is pushed from the control to the data source or vice versa
        // </summary>
        // public event BindingCompleteEventHandler BindingComplete
        
        // <summary>
        // Occurs when the value of a data-bound control changes.
        // </summary>
        //
        // public event ConvertEventHandler Parse
        
        // <summary>
        // Occurs when the property of a control is bound to a data value.
        // </summary>
        //
        // public event ConvertEventHandler Format
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding"/> class that simple-binds the indicated control property to the specified data member of the data source.
        // </summary>
        // <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> that represents the data source. </param><param name="dataMember">The property or list to bind to. </param><exception cref="T:System.Exception"><paramref name="propertyName"/> is neither a valid property of a control nor an empty string (""). </exception><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control.</exception>
        //public Binding(string propertyName, object dataSource, string dataMember)
        //  : this(propertyName, dataSource, dataMember, false, DataSourceUpdateMode.OnValidation, (object)null, string.Empty, (IFormatProvider)null)
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding"/> class that binds the indicated control property to the specified data member of the data source, and optionally enables formatting to be applied.
        // </summary>
        // <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> that represents the data source. </param><param name="dataMember">The property or list to bind to. </param><param name="formattingEnabled">true to format the displayed data; otherwise, false. </param><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control.-or-The property given is a read-only property.</exception><exception cref="T:System.Exception">Formatting is disabled and <paramref name="propertyName"/> is neither a valid property of a control nor an empty string (""). </exception>
        //public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled)
        //  : this(propertyName, dataSource, dataMember, formattingEnabled, DataSourceUpdateMode.OnValidation, (object)null, string.Empty, (IFormatProvider)null)
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding"/> class that binds the specified control property to the specified data member of the specified data source. Optionally enables formatting and propagates values to the data source based on the specified update setting.
        // </summary>
        // <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> representing the data source. </param><param name="dataMember">The property or list to bind to.</param><param name="formattingEnabled">true to format the displayed data; otherwise, false.</param><param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode"/> values.</param><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
        //public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode)
        //  : this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, (object)null, string.Empty, (IFormatProvider)null)
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding"/> class that binds the indicated control property to the specified data member of the specified data source. Optionally enables formatting, propagates values to the data source based on the specified update setting, and sets the property to the specified value when a <see cref="T:System.DBNull"/> is returned from the data source.
        // </summary>
        // <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> representing the data source. </param><param name="dataMember">The property or list to bind to.</param><param name="formattingEnabled">true to format the displayed data; otherwise, false.</param><param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode"/> values.</param><param name="nullValue">The <see cref="T:System.Object"/> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull"/>.</param><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
        //public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue)
        //  : this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, string.Empty, (IFormatProvider)null)
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding"/> class that binds the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; and sets the property to the specified value when a <see cref="T:System.DBNull"/> is returned from the data source.
        // </summary>
        // <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> representing the data source. </param><param name="dataMember">The property or list to bind to.</param><param name="formattingEnabled">true to format the displayed data; otherwise, false.</param><param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode"/> values.</param><param name="nullValue">The <see cref="T:System.Object"/> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull"/>.</param><param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
        //public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString)
        //  : this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, formatString, (IFormatProvider)null)
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding"/> class with the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; enables formatting with the specified format string; sets the property to the specified value when a <see cref="T:System.DBNull"/> is returned from the data source; and sets the specified format provider.
        // </summary>
        // <param name="propertyName">The name of the control property to bind. </param><param name="dataSource">An <see cref="T:System.Object"/> representing the data source. </param><param name="dataMember">The property or list to bind to.</param><param name="formattingEnabled">true to format the displayed data; otherwise, false.</param><param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode"/> values.</param><param name="nullValue">The <see cref="T:System.Object"/> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull"/>.</param><param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param><param name="formatInfo">An implementation of <see cref="T:System.IFormatProvider"/> to override default formatting behavior.</param><exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName"/> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
        // public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString, IFormatProvider formatInfo)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Binding.BindingComplete"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs"/>  that contains the event data. </param>
        // protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Binding.Parse"/> event.
        // </summary>
        // <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs"/> that contains the event data. </param>
        // protected virtual void OnParse(ConvertEventArgs cevent)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Binding.Format"/> event.
        // </summary>
        // <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs"/> that contains the event data. </param>
        // protected virtual void OnFormat(ConvertEventArgs cevent)
        
        // <summary>
        // Sets the control property to the value read from the data source.
        // </summary>
        // public void ReadValue()
        
        // <summary>
        // Reads the current value from the control property and writes it to the data source.
        // </summary>
        // public void WriteValue()
    }
}

