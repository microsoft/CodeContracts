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
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Xml.Linq
{
  // Summary:
  //     Represents an XML attribute.
  public class XAttribute : XObject
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XAttribute class from another
    //     System.Xml.Linq.XAttribute object.
    //
    // Parameters:
    //   other:
    //     An System.Xml.Linq.XAttribute object to copy from.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The other parameter is null.
    public XAttribute(XAttribute other)
    {
      Contract.Requires(other != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XAttribute class from the
    //     specified name and value.
    //
    // Parameters:
    //   name:
    //     The System.Xml.Linq.XName of the attribute.
    //
    //   value:
    //     An System.Object containing the value of the attribute.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The name or value parameter is null.
    public XAttribute(XName name, object value)
    {
      Contract.Requires(name != null);
      Contract.Requires(value != null);
    }

    extern public static explicit operator bool?(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to an System.Int32.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.Int32.
    //
    // Returns:
    //     A System.Int32 that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.Int32 value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator int(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(int);
    }
    extern public static explicit operator int?(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.Boolean.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.Boolean.
    //
    // Returns:
    //     A System.Boolean that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.Boolean value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator bool(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(bool);
    }
    extern public static explicit operator uint?(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to an System.Int64.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.Int64.
    //
    // Returns:
    //     A System.Int64 that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.Int64 value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator long(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(long);
    }
    extern public static explicit operator long?(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.UInt64.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.UInt64.
    //
    // Returns:
    //     A System.UInt64 that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.UInt64 value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator ulong(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(ulong);
    }
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.UInt32.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.UInt32.
    //
    // Returns:
    //     A System.UInt32 that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.UInt32 value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator uint(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(uint);
    }
    extern public static explicit operator DateTime?(XAttribute attribute);
    extern public static explicit operator Guid?(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.Guid.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.Guid.
    //
    // Returns:
    //     A System.Guid that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.Guid value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator Guid(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(Guid);
    }
    extern public static explicit operator TimeSpan?(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.TimeSpan.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.TimeSpan.
    //
    // Returns:
    //     A System.TimeSpan that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.TimeSpan value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator TimeSpan(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(TimeSpan);
    }
    extern public static explicit operator DateTimeOffset?(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.DateTimeOffset.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.DateTimeOffset.
    //
    // Returns:
    //     A System.DateTimeOffset that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.DateTimeOffset value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator DateTimeOffset(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(DateTimeOffset);
    }
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.String.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.String.
    //
    // Returns:
    //     A System.String that contains the content of this System.Xml.Linq.XAttribute.
    extern public static explicit operator string(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.DateTime.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.DateTime.
    //
    // Returns:
    //     A System.DateTime that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.DateTime value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator DateTime(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(DateTime);
    }
    extern public static explicit operator decimal?(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.Decimal.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.Decimal.
    //
    // Returns:
    //     A System.Decimal that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.Decimal value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator decimal(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(Decimal);
    }
    extern public static explicit operator double?(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.Double.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.Double.
    //
    // Returns:
    //     A System.Double that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.Double value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator double(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(double);
    }
    extern public static explicit operator float?(XAttribute attribute);
    //
    // Summary:
    //     Cast the value of this System.Xml.Linq.XAttribute to a System.Single.
    //
    // Parameters:
    //   attribute:
    //     The System.Xml.Linq.XAttribute to cast to System.Single.
    //
    // Returns:
    //     A System.Single that contains the content of this System.Xml.Linq.XAttribute.
    //
    // Exceptions:
    //   System.FormatException:
    //     The attribute does not contain a valid System.Single value.
    //
    //   System.ArgumentNullException:
    //     The attribute parameter is null.
    public static explicit operator float(XAttribute attribute)
    {
      Contract.Requires(attribute != null);
      return default(float);
    }
    extern public static explicit operator ulong?(XAttribute attribute);

    // Summary:
    //     Gets an empty collection of attributes.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XAttribute
    //     containing an empty collection.
    public static IEnumerable<XAttribute> EmptySequence
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<XAttribute>>() != null);
        return default(IEnumerable<XAttribute>);
      }
    }
    //
    // Summary:
    //     Determines if this attribute is a namespace declaration.
    //
    // Returns:
    //     true if this attribute is a namespace declaration; otherwise false.
    extern public bool IsNamespaceDeclaration { get; }
    //
    // Summary:
    //     Gets the expanded name of this attribute.
    //
    // Returns:
    //     An System.Xml.Linq.XName containing the name of this attribute.
    public XName Name
    {
      get
      {
        Contract.Ensures(Contract.Result<XName>() != null);
        return default(XName);
      }
    }
    //
    // Summary:
    //     Gets the next attribute of the parent element.
    //
    // Returns:
    //     An System.Xml.Linq.XAttribute containing the next attribute of the parent
    //     element.
    extern public XAttribute NextAttribute { get; }
    //
    // Summary:
    //     Gets the previous attribute of the parent element.
    //
    // Returns:
    //     An System.Xml.Linq.XAttribute containing the previous attribute of the parent
    //     element.
    extern public XAttribute PreviousAttribute { get; }
    //
    // Summary:
    //     Gets or sets the value of this attribute.
    //
    // Returns:
    //     A System.String containing the value of this attribute.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     When setting, the value is null.
    public string Value
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    // Summary:
    //     Removes this attribute from its parent element.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The parent element is null.
    extern public void Remove();
    //
    // Summary:
    //     Sets the value of this attribute.
    //
    // Parameters:
    //   value:
    //     The value to assign to this attribute.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value parameter is null.
    //
    //   System.ArgumentException:
    //     The value is an System.Xml.Linq.XObject.
    public void SetValue(object value)
    {
      Contract.Requires(value != null);
    }
  }
}
