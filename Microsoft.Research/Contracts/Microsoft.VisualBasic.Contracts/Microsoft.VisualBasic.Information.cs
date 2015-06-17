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



using System.Diagnostics.Contracts;
using System;
namespace Microsoft.VisualBasic
{
  // Summary:
  //     The Information module contains the procedures used to return, test for,
  //     or verify information.
  //[StandardModule]
  public static class Information
  {
    // Summary:
    //     Returns an integer indicating the line number of the last executed statement.
    //     Read-only.
    //
    // Returns:
    //     Returns an integer indicating the line number of the last executed statement.
    //     Read-only.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    
    // F: I am not sure result >= 0
    //public static int Erl();
    //
    // Summary:
    //     Contains information about run-time errors.
    //
    // Returns:
    //     Contains information about run-time errors.
    public static ErrObject Err()
    {
      Contract.Ensures(Contract.Result<ErrObject>() != null);

      return default(ErrObject);
    }
    //
    // Summary:
    //     Returns a Boolean value indicating whether a variable points to an array.
    //
    // Parameters:
    //   VarName:
    //     Required. Object variable.
    //
    // Returns:
    //     Returns a Boolean value indicating whether a variable points to an array.
    // F: the parameter can be null
    [Pure]
    public static bool IsArray(object VarName)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a Boolean value indicating whether an expression represents a valid
    //     Date value.
    //
    // Parameters:
    //   Expression:
    //     Required. Object expression.
    //
    // Returns:
    //     Returns a Boolean value indicating whether an expression represents a valid
    //     Date value.
    // F: the parameter can be null
    [Pure]
    public static bool IsDate(object Expression)
        {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a Boolean value indicating whether an expression evaluates to the
    //     System.DBNull class.
    //
    // Parameters:
    //   Expression:
    //     Required. Object expression.
    //
    // Returns:
    //     Returns a Boolean value indicating whether an expression evaluates to the
    //     System.DBNull class.
    // F: the parameter can be null
    [Pure]
    public static bool IsDBNull(object Expression)
    {
      return default(bool);
    }
    
    //
    // Summary:
    //     Returns a Boolean value indicating whether an expression is an exception
    //     type.
    //
    // Parameters:
    //   Expression:
    //     Required. Object expression.
    //
    // Returns:
    //     Returns a Boolean value indicating whether an expression is an exception
    //     type.
    // F: the parameter can be null
    [Pure]
    public static bool IsError(object Expression)
    {
      return default(bool);
    }
    
    //
    // Summary:
    //     Returns a Boolean value indicating whether an expression has no object assigned
    //     to it.
    //
    // Parameters:
    //   Expression:
    //     Required. Object expression.
    //
    // Returns:
    //     Returns a Boolean value indicating whether an expression has no object assigned
    //     to it.
    // F: the parameter can be null
    [Pure]
    public static bool IsNothing(object Expression)
    {
      Contract.Ensures(Contract.Result<bool>() == (Expression == null));

      return default(bool);
    }
#if !SILVERLIGHT
    //
    // Summary:
    //     Returns a Boolean value indicating whether an expression can be evaluated
    //     as a number.
    //
    // Parameters:
    //   Expression:
    //     Required. Object expression.
    //
    // Returns:
    //     Returns a Boolean value indicating whether an expression can be evaluated
    //     as a number.
    [Pure]
    public static bool IsNumeric(object Expression)
    {
      return default(bool);
    }
#endif
    //
    // Summary:
    //     Returns a Boolean value indicating whether an expression evaluates to a reference
    //     type.
    //
    // Parameters:
    //   Expression:
    //     Required. Object expression.
    //
    // Returns:
    //     Returns a Boolean value indicating whether an expression evaluates to a reference
    //     type.
    [Pure]
    public static bool IsReference(object Expression)
    {
      Contract.Ensures(Contract.Result<bool>() == (!(Expression is ValueType)));

      return default(bool);
    }
    //
    // Summary:
    //     Returns the lowest available subscript for the indicated dimension of an
    //     array.
    //
    // Parameters:
    //   Array:
    //     Required. Array of any data type. The array in which you want to find the
    //     lowest possible subscript of a dimension.
    //
    //   Rank:
    //     Optional. Integer. The dimension for which the lowest possible subscript
    //     is to be returned. Use 1 for the first dimension, 2 for the second, and so
    //     on. If Rank is omitted, 1 is assumed.
    //
    // Returns:
    //     Integer. The lowest value the subscript for the specified dimension can contain.
    //     LBound always returns 0 as long as Array has been initialized, even if it
    //     has no elements, for example if it is a zero-length string. If Array is Nothing,
    //     LBound throws an System.ArgumentNullException.
   
    [Pure]
    public static int LBound(Array Array, int Rank)
    {
      Contract.Requires(Array != null);

      Contract.Requires(Rank > 0);
      Contract.Requires(Rank <= Array.Rank);

      return default(int);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Returns an Integer value representing the RGB color code corresponding to
    //     the specified color number.
    //
    // Parameters:
    //   Color:
    //     Required. A whole number in the range 0–15.
    //
    // Returns:
    //     Returns an Integer value representing the RGB color code corresponding to
    //     the specified color number.
    public static int QBColor(int Color)
    {
      Contract.Requires(Color >= 0);
      Contract.Requires(Color <= 15);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }

    //
    // Summary:
    //     Returns an Integer value representing an RGB color value from a set of red,
    //     green and blue color components.
    //
    // Parameters:
    //   Red:
    //     Required. Integer in the range 0–255, inclusive, that represents the intensity
    //     of the red component of the color.
    //
    //   Green:
    //     Required. Integer in the range 0–255, inclusive, that represents the intensity
    //     of the green component of the color.
    //
    //   Blue:
    //     Required. Integer in the range 0–255, inclusive, that represents the intensity
    //     of the blue component of the color.
    //
    // Returns:
    //     Returns an Integer value representing an RGB color value from a set of red,
    //     green and blue color components.
    [Pure]
    public static int RGB(int Red, int Green, int Blue)
    {
      Contract.Requires(Red >= 0);
      Contract.Requires(Red <= 255);

      Contract.Requires(Green >= 0);
      Contract.Requires(Green <= 255);

      Contract.Requires(Blue >= 0);
      Contract.Requires(Blue <= 255);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Returns a String value containing the system data type name of a variable.
    //
    // Parameters:
    //   VbName:
    //     Required. A String variable containing a Visual Basic type name.
    //
    // Returns:
    //     Returns a String value containing the system data type name of a variable.
    [Pure]
    public static string SystemTypeName(string VbName)
    {
      return default(string);
    }
    //
    // Summary:
    //     Returns a String value containing data-type information about a variable.
    //
    // Parameters:
    //   VarName:
    //     Required. Object variable. If Option Strict is Off, you can pass a variable
    //     of any data type except a structure.
    //
    // Returns:
    //     Returns a String value containing data-type information about a variable.
    [Pure]
    public static string TypeName(object VarName)
    {
      return default(string);
    }
#endif
    //
    // Summary:
    //     Returns the highest available subscript for the indicated dimension of an
    //     array.
    //
    // Parameters:
    //   Array:
    //     Required. Array of any data type. The array in which you want to find the
    //     highest possible subscript of a dimension.
    //
    //   Rank:
    //     Optional. Integer. The dimension for which the highest possible subscript
    //     is to be returned. Use 1 for the first dimension, 2 for the second, and so
    //     on. If Rank is omitted, 1 is assumed.
    //
    // Returns:
    //     Integer. The highest value the subscript for the specified dimension can
    //     contain. If Array has only one element, UBound returns 0. If Array has no
    //     elements, for example if it is a zero-length string, UBound returns -1.
    [Pure]
    public static int UBound(Array Array, int Rank)
    {
      Contract.Requires(Array != null);

      Contract.Requires(Rank > 0);
      Contract.Requires(Rank <= Array.Rank);

      return default(int);
    }
#if !SILVERLIGHT
    //
    // Summary:
    //     Returns an Integer value containing the data type classification of a variable.
    //
    // Parameters:
    //   VarName:
    //     Required. Object variable. If Option Strict is Off, you can pass a variable
    //     of any data type except a structure.
    //
    // Returns:
    //     Returns an Integer value containing the data type classification of a variable.
    //public static VariantType VarType(object VarName);
    //
    // Summary:
    //     Returns a String value containing the Visual Basic data type name of a variable.
    //
    // Parameters:
    //   UrtName:
    //     Required. String variable containing a type name used by the common language
    //     runtime.
    //
    // Returns:
    //     Returns a String value containing the Visual Basic data type name of a variable.
    [Pure]
    public static string VbTypeName(string UrtName)
    {
      return default(string);
    }
#endif
  }
}
