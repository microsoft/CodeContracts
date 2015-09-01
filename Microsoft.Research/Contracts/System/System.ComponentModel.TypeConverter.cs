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
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.ComponentModel
{
  // Summary:
  //     Provides a unified way of converting types of values to other types, as well
  //     as for accessing standard values and subproperties.
  //[ComVisible(true)]
  public class TypeConverter
  {
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.TypeConverter class.
    //public TypeConverter();

    // Summary:
    //     Returns whether this converter can convert an object of the given type to
    //     the type of this converter.
    //
    // Parameters:
    //   sourceType:
    //     A System.Type that represents the type you want to convert from.
    //
    // Returns:
    //     true if this converter can perform the conversion; otherwise, false.
    //public bool CanConvertFrom(Type sourceType);
    //
    // Summary:
    //     Returns whether this converter can convert an object of the given type to
    //     the type of this converter, using the specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   sourceType:
    //     A System.Type that represents the type you want to convert from.
    //
    // Returns:
    //     true if this converter can perform the conversion; otherwise, false.
    //public virtual bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType);
    //
    // Summary:
    //     Returns whether this converter can convert the object to the specified type.
    //
    // Parameters:
    //   destinationType:
    //     A System.Type that represents the type you want to convert to.
    //
    // Returns:
    //     true if this converter can perform the conversion; otherwise, false.
    //public bool CanConvertTo(Type destinationType);
    //
    // Summary:
    //     Returns whether this converter can convert the object to the specified type,
    //     using the specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   destinationType:
    //     A System.Type that represents the type you want to convert to.
    //
    // Returns:
    //     true if this converter can perform the conversion; otherwise, false.
    //public virtual bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
    //
    // Summary:
    //     Converts the given value to the type of this converter.
    //
    // Parameters:
    //   value:
    //     The System.Object to convert.
    //
    // Returns:
    //     An System.Object that represents the converted value.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    public object ConvertFrom(object value)
    {
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
    
    // Summary:
    //     Converts the given object to the type of this converter, using the specified
    //     context and culture information.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   culture:
    //     The System.Globalization.CultureInfo to use as the current culture.
    //
    //   value:
    //     The System.Object to convert.
    //
    // Returns:
    //     An System.Object that represents the converted value.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public virtual object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value);
    //
    // Summary:
    //     Converts the given string to the type of this converter, using the invariant
    //     culture.
    //
    // Parameters:
    //   text:
    //     The System.String to convert.
    //
    // Returns:
    //     An System.Object that represents the converted text.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public object ConvertFromInvariantString(string text);
    //
    // Summary:
    //     Converts the given string to the type of this converter, using the invariant
    //     culture and the specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   text:
    //     The System.String to convert.
    //
    // Returns:
    //     An System.Object that represents the converted text.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public object ConvertFromInvariantString(ITypeDescriptorContext context, string text);
    //
    // Summary:
    //     Converts the specified text to an object.
    //
    // Parameters:
    //   text:
    //     The text representation of the object to convert.
    //
    // Returns:
    //     An System.Object that represents the converted text.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The string cannot be converted into the appropriate object.
    //public object ConvertFromString(string text);
    //
    // Summary:
    //     Converts the given text to an object, using the specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   text:
    //     The System.String to convert.
    //
    // Returns:
    //     An System.Object that represents the converted text.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public object ConvertFromString(ITypeDescriptorContext context, string text);
    //
    // Summary:
    //     Converts the given text to an object, using the specified context and culture
    //     information.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   culture:
    //     A System.Globalization.CultureInfo. If null is passed, the current culture
    //     is assumed.
    //
    //   text:
    //     The System.String to convert.
    //
    // Returns:
    //     An System.Object that represents the converted text.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public object ConvertFromString(ITypeDescriptorContext context, CultureInfo culture, string text);
    //
    // Summary:
    //     Converts the given value object to the specified type, using the arguments.
    //
    // Parameters:
    //   value:
    //     The System.Object to convert.
    //
    //   destinationType:
    //     The System.Type to convert the value parameter to.
    //
    // Returns:
    //     An System.Object that represents the converted value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The destinationType parameter is null.
    //
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    public object ConvertTo(object value, Type destinationType)
    {
      Contract.Requires(destinationType != null);

      return default(object);
    }
    //
    // Summary:
    //     Converts the given value object to the specified type, using the specified
    //     context and culture information.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   culture:
    //     A System.Globalization.CultureInfo. If null is passed, the current culture
    //     is assumed.
    //
    //   value:
    //     The System.Object to convert.
    //
    //   destinationType:
    //     The System.Type to convert the value parameter to.
    //
    // Returns:
    //     An System.Object that represents the converted value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The destinationType parameter is null.
    //
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public virtual object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
    //
    // Summary:
    //     Converts the specified value to a culture-invariant string representation.
    //
    // Parameters:
    //   value:
    //     The System.Object to convert.
    //
    // Returns:
    //     A System.String that represents the converted value.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public string ConvertToInvariantString(object value);
    //
    // Summary:
    //     Converts the specified value to a culture-invariant string representation,
    //     using the specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   value:
    //     The System.Object to convert.
    //
    // Returns:
    //     A System.String that represents the converted value.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public string ConvertToInvariantString(ITypeDescriptorContext context, object value);
    //
    // Summary:
    //     Converts the specified value to a string representation.
    //
    // Parameters:
    //   value:
    //     The System.Object to convert.
    //
    // Returns:
    //     An System.Object that represents the converted value.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public string ConvertToString(object value);
    //
    // Summary:
    //     Converts the given value to a string representation, using the given context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   value:
    //     The System.Object to convert.
    //
    // Returns:
    //     An System.Object that represents the converted value.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public string ConvertToString(ITypeDescriptorContext context, object value);
    //
    // Summary:
    //     Converts the given value to a string representation, using the specified
    //     context and culture information.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   culture:
    //     A System.Globalization.CultureInfo. If null is passed, the current culture
    //     is assumed.
    //
    //   value:
    //     The System.Object to convert.
    //
    // Returns:
    //     An System.Object that represents the converted value.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The conversion cannot be performed.
    //public string ConvertToString(ITypeDescriptorContext context, CultureInfo culture, object value);
    //
    // Summary:
    //     Re-creates an System.Object given a set of property values for the object.
    //
    // Parameters:
    //   propertyValues:
    //     An System.Collections.IDictionary that represents a dictionary of new property
    //     values.
    //
    // Returns:
    //     An System.Object representing the given System.Collections.IDictionary, or
    //     null if the object cannot be created. This method always returns null.
    //public object CreateInstance(IDictionary propertyValues);
   
    //
    // Summary:
    //     Creates an instance of the type that this System.ComponentModel.TypeConverter
    //     is associated with, using the specified context, given a set of property
    //     values for the object.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   propertyValues:
    //     An System.Collections.IDictionary of new property values.
    //
    // Returns:
    //     An System.Object representing the given System.Collections.IDictionary, or
    //     null if the object cannot be created. This method always returns null.
    //public virtual object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues);
    //
    // Summary:
    //     Returns an exception to throw when a conversion cannot be performed.
    //
    // Parameters:
    //   value:
    //     The System.Object to convert, or null if the object is not available.
    //
    // Returns:
    //     An System.Exception that represents the exception to throw when a conversion
    //     cannot be performed.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     Automatically thrown by this method.
    //protected Exception GetConvertFromException(object value);
    //
    // Summary:
    //     Returns an exception to throw when a conversion cannot be performed.
    //
    // Parameters:
    //   value:
    //     The System.Object to convert, or null if the object is not available.
    //
    //   destinationType:
    //     A System.Type that represents the type the conversion was trying to convert
    //     to.
    //
    // Returns:
    //     An System.Exception that represents the exception to throw when a conversion
    //     cannot be performed.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     Automatically thrown by this method.
    //protected Exception GetConvertToException(object value, Type destinationType);
    //
    // Summary:
    //     Returns whether changing a value on this object requires a call to the System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)
    //     method to create a new value.
    //
    // Returns:
    //     true if changing a property on this object requires a call to System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)
    //     to create a new value; otherwise, false.
    //public bool GetCreateInstanceSupported();
    //
    // Summary:
    //     Returns whether changing a value on this object requires a call to System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)
    //     to create a new value, using the specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    // Returns:
    //     true if changing a property on this object requires a call to System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)
    //     to create a new value; otherwise, false.
    //public virtual bool GetCreateInstanceSupported(ITypeDescriptorContext context);
    //
    // Summary:
    //     Returns a collection of properties for the type of array specified by the
    //     value parameter.
    //
    // Parameters:
    //   value:
    //     An System.Object that specifies the type of array for which to get properties.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     that are exposed for this data type, or null if there are no properties.
    //public PropertyDescriptorCollection GetProperties(object value);
    //
    // Summary:
    //     Returns a collection of properties for the type of array specified by the
    //     value parameter, using the specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   value:
    //     An System.Object that specifies the type of array for which to get properties.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     that are exposed for this data type, or null if there are no properties.
    //public PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value);
    //
    // Summary:
    //     Returns a collection of properties for the type of array specified by the
    //     value parameter, using the specified context and attributes.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   value:
    //     An System.Object that specifies the type of array for which to get properties.
    //
    //   attributes:
    //     An array of type System.Attribute that is used as a filter.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     that are exposed for this data type, or null if there are no properties.
    //public virtual PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes);
    //
    // Summary:
    //     Returns whether this object supports properties.
    //
    // Returns:
    //     true if System.ComponentModel.TypeConverter.GetProperties(System.Object)
    //     should be called to find the properties of this object; otherwise, false.
    //public bool GetPropertiesSupported();
    //
    // Summary:
    //     Returns whether this object supports properties, using the specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    // Returns:
    //     true if System.ComponentModel.TypeConverter.GetProperties(System.Object)
    //     should be called to find the properties of this object; otherwise, false.
    //public virtual bool GetPropertiesSupported(ITypeDescriptorContext context);
    //
    // Summary:
    //     Returns a collection of standard values from the default context for the
    //     data type this type converter is designed for.
    //
    // Returns:
    //     A System.ComponentModel.TypeConverter.StandardValuesCollection containing
    //     a standard set of valid values, or null if the data type does not support
    //     a standard set of values.
    //public ICollection GetStandardValues();
    //
    // Summary:
    //     Returns a collection of standard values for the data type this type converter
    //     is designed for when provided with a format context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context
    //     that can be used to extract additional information about the environment
    //     from which this converter is invoked. This parameter or properties of this
    //     parameter can be null.
    //
    // Returns:
    //     A System.ComponentModel.TypeConverter.StandardValuesCollection that holds
    //     a standard set of valid values, or null if the data type does not support
    //     a standard set of values.
    //public virtual TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context);
    //
    // Summary:
    //     Returns whether the collection of standard values returned from System.ComponentModel.TypeConverter.GetStandardValues()
    //     is an exclusive list.
    //
    // Returns:
    //     true if the System.ComponentModel.TypeConverter.StandardValuesCollection
    //     returned from System.ComponentModel.TypeConverter.GetStandardValues() is
    //     an exhaustive list of possible values; false if other values are possible.
    //public bool GetStandardValuesExclusive();
    //
    // Summary:
    //     Returns whether the collection of standard values returned from System.ComponentModel.TypeConverter.GetStandardValues()
    //     is an exclusive list of possible values, using the specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    // Returns:
    //     true if the System.ComponentModel.TypeConverter.StandardValuesCollection
    //     returned from System.ComponentModel.TypeConverter.GetStandardValues() is
    //     an exhaustive list of possible values; false if other values are possible.
    //public virtual bool GetStandardValuesExclusive(ITypeDescriptorContext context);
    //
    // Summary:
    //     Returns whether this object supports a standard set of values that can be
    //     picked from a list.
    //
    // Returns:
    //     true if System.ComponentModel.TypeConverter.GetStandardValues() should be
    //     called to find a common set of values the object supports; otherwise, false.
    //public bool GetStandardValuesSupported();
    //
    // Summary:
    //     Returns whether this object supports a standard set of values that can be
    //     picked from a list, using the specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    // Returns:
    //     true if System.ComponentModel.TypeConverter.GetStandardValues() should be
    //     called to find a common set of values the object supports; otherwise, false.
    //public virtual bool GetStandardValuesSupported(ITypeDescriptorContext context);
    //
    // Summary:
    //     Returns whether the given value object is valid for this type.
    //
    // Parameters:
    //   value:
    //     The object to test for validity.
    //
    // Returns:
    //     true if the specified value is valid for this object; otherwise, false.
    //public bool IsValid(object value);
    //
    // Summary:
    //     Returns whether the given value object is valid for this type and for the
    //     specified context.
    //
    // Parameters:
    //   context:
    //     An System.ComponentModel.ITypeDescriptorContext that provides a format context.
    //
    //   value:
    //     The System.Object to test for validity.
    //
    // Returns:
    //     true if the specified value is valid for this object; otherwise, false.
    //public virtual bool IsValid(ITypeDescriptorContext context, object value);
    //
    // Summary:
    //     Sorts a collection of properties.
    //
    // Parameters:
    //   props:
    //     A System.ComponentModel.PropertyDescriptorCollection that has the properties
    //     to sort.
    //
    //   names:
    //     An array of names in the order you want the properties to appear in the collection.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection that contains the sorted
    //     properties.
    //protected PropertyDescriptorCollection SortProperties(PropertyDescriptorCollection props, string[] names);

#if !SILVERLIGHT
    // Summary:
    //     Represents an abstract class that provides properties for objects that do
    //     not have properties.
    protected abstract class SimplePropertyDescriptor //: PropertyDescriptor
    {
      // Summary:
      //     Initializes a new instance of the System.ComponentModel.TypeConverter.SimplePropertyDescriptor
      //     class.
      //
      // Parameters:
      //   componentType:
      //     A System.Type that represents the type of component to which this property
      //     descriptor binds.
      //
      //   name:
      //     The name of the property.
      //
      //   propertyType:
      //     A System.Type that represents the data type for this property.
      //protected SimplePropertyDescriptor(Type componentType, string name, Type propertyType);
      //
      // Summary:
      //     Initializes a new instance of the System.ComponentModel.TypeConverter.SimplePropertyDescriptor
      //     class.
      //
      // Parameters:
      //   componentType:
      //     A System.Type that represents the type of component to which this property
      //     descriptor binds.
      //
      //   name:
      //     The name of the property.
      //
      //   propertyType:
      //     A System.Type that represents the data type for this property.
      //
      //   attributes:
      //     An System.Attribute array with the attributes to associate with the property.
      //protected SimplePropertyDescriptor(Type componentType, string name, Type propertyType, Attribute[] attributes);

      // Summary:
      //     Gets the type of component to which this property description binds.
      //
      // Returns:
      //     A System.Type that represents the type of component to which this property
      //     binds.
      //public override Type ComponentType { get; }
      //
      // Summary:
      //     Gets a value indicating whether this property is read-only.
      //
      // Returns:
      //     true if the property is read-only; false if the property is read/write.
      //public override bool IsReadOnly { get; }
      //
      // Summary:
      //     Gets the type of the property.
      //
      // Returns:
      //     A System.Type that represents the type of the property.
      //public override Type PropertyType { get; }

      // Summary:
      //     Returns whether resetting the component changes the value of the component.
      //
      // Parameters:
      //   component:
      //     The component to test for reset capability.
      //
      // Returns:
      //     true if resetting the component changes the value of the component; otherwise,
      //     false.
      //public override bool CanResetValue(object component);
      //
      // Summary:
      //     Resets the value for this property of the component.
      //
      // Parameters:
      //   component:
      //     The component with the property value to be reset.
      //public override void ResetValue(object component);
      //
      // Summary:
      //     Returns whether the value of this property can persist.
      //
      // Parameters:
      //   component:
      //     The component with the property that is to be examined for persistence.
      //
      // Returns:
      //     true if the value of the property can persist; otherwise, false.
      //public override bool ShouldSerializeValue(object component);
    }

    // Summary:
    //     Represents a collection of values.
    public class StandardValuesCollection //: ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.ComponentModel.TypeConverter.StandardValuesCollection
      //     class.
      //
      // Parameters:
      //   values:
      //     An System.Collections.ICollection that represents the objects to put into
      //     the collection.
      //public StandardValuesCollection(ICollection values);

      // Summary:
      //     Gets the number of objects in the collection.
      //
      // Returns:
      //     The number of objects in the collection.
      //public int Count { get; }

      // Summary:
      //     Gets the object at the specified index number.
      //
      // Parameters:
      //   index:
      //     The zero-based index of the System.Object to get from the collection.
      //
      // Returns:
      //     The System.Object with the specified index.
      //public object this[int index] { get; }

      // Summary:
      //     Copies the contents of this collection to an array.
      //
      // Parameters:
      //   array:
      //     An System.Array that represents the array to copy to.
      //
      //   index:
      //     The index to start from.
      //public void CopyTo(Array array, int index);
      //
      // Summary:
      //     Returns an enumerator for this collection.
      //
      // Returns:
      //     An enumerator of type System.Collections.IEnumerator.
      //public IEnumerator GetEnumerator();
    }

#endif

  }
}
