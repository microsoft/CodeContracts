' CodeContracts
' 
' Copyright (c) Microsoft Corporation
' 
' All rights reserved. 
' 
' MIT License
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Imports System.Diagnostics.Contracts
Imports System
Namespace Microsoft.VisualBasic

  '' Summary:
  ''     The Information module contains the procedures used to return, test for,
  ''     or verify information.
  ''[StandardModule]
  Public Class Information
  
    Private Sub New()
    End Sub

    '' Summary:
    ''     Returns an integer indicating the line number of the last executed statement.
    ''     Read-only.
    ''
    '' Returns:
    ''     Returns an integer indicating the line number of the last executed statement.
    ''     Read-only.
    ''[EditorBrowsable(EditorBrowsableState.Never)]
    
    '' F: I am not sure result >= 0
    ''public static int Erl()
    ''
    '' Summary:
    ''     Contains information about run-time errors.
    ''
    '' Returns:
    ''     Contains information about run-time errors.
    Public Shared Function Err() As ErrObject
    
      Contract.Ensures(Contract.Result(Of ErrObject)() IsNot Nothing)

      Return Nothing
    End Function
    ''
    '' Summary:
    ''     Returns a Boolean value indicating whether a variable points to an array.
    ''
    '' Parameters:
    ''   VarName:
    ''     Required. Object variable.
    ''
    '' Returns:
    ''     Returns a Boolean value indicating whether a variable points to an array.
    '' F: the parameter can be null
    <Pure()> _
    Public Shared Function IsArray(ByVal VarName As Object) As Boolean
    
      Return Nothing
    End Function

    ''
    '' Summary:
    ''     Returns a Boolean value indicating whether an expression represents a valid
    ''     Date value.
    ''
    '' Parameters:
    ''   Expression:
    ''     Required. Object expression.
    ''
    '' Returns:
    ''     Returns a Boolean value indicating whether an expression represents a valid
    ''     Date value.
    '' F: the parameter can be null
    <Pure()> _
    Public Shared Function IsDate(ByVal Expression As Object) As Boolean
        
      Return Nothing
    End Function

    ''
    '' Summary:
    ''     Returns a Boolean value indicating whether an expression evaluates to the
    ''     System.DBNull class.
    ''
    '' Parameters:
    ''   Expression:
    ''     Required. Object expression.
    ''
    '' Returns:
    ''     Returns a Boolean value indicating whether an expression evaluates to the
    ''     System.DBNull class.
    '' F: the parameter can be null
    <Pure()> _
    Public Shared Function IsDBNull(ByVal Expression As Object) As Boolean
    
      Return Nothing
    End Function
    
    ''
    '' Summary:
    ''     Returns a Boolean value indicating whether an expression is an exception
    ''     type.
    ''
    '' Parameters:
    ''   Expression:
    ''     Required. Object expression.
    ''
    '' Returns:
    ''     Returns a Boolean value indicating whether an expression is an exception
    ''     type.
    '' F: the parameter can be null
    <Pure()> _
    Public Shared Function IsError(ByVal Expression As Object) As Boolean
    
      Return Nothing
    End Function
    
    ''
    '' Summary:
    ''     Returns a Boolean value indicating whether an expression has no object assigned
    ''     to it.
    ''
    '' Parameters:
    ''   Expression:
    ''     Required. Object expression.
    ''
    '' Returns:
    ''     Returns a Boolean value indicating whether an expression has no object assigned
    ''     to it.
    '' F: the parameter can be null
    <Pure()> _
    Public Shared Function IsNothing(ByVal Expression As Object) As Boolean
    
      Contract.Ensures(Contract.Result(Of Boolean)() = (Expression Is Nothing))

      Return Nothing
    End Function
#If Not SILVERLIGHT Then
    ''
    '' Summary:
    ''     Returns a Boolean value indicating whether an expression can be evaluated
    ''     as a number.
    ''
    '' Parameters:
    ''   Expression:
    ''     Required. Object expression.
    ''
    '' Returns:
    ''     Returns a Boolean value indicating whether an expression can be evaluated
    ''     as a number.
    <Pure()> _
    Public Shared Function IsNumeric(ByVal Expression As Object) As Boolean
    
      Return Nothing
    End Function
#End If
    ''
    '' Summary:
    ''     Returns a Boolean value indicating whether an expression evaluates to a reference
    ''     type.
    ''
    '' Parameters:
    ''   Expression:
    ''     Required. Object expression.
    ''
    '' Returns:
    ''     Returns a Boolean value indicating whether an expression evaluates to a reference
    ''     type.
    <Pure()> _
    Public Shared Function IsReference(ByVal Expression As Object) As Boolean
    
      Contract.Ensures(Contract.Result(Of Boolean)() = (Not TypeOf (Expression) Is ValueType))

      Return Nothing
    End Function
    ''
    '' Summary:
    ''     Returns the lowest available subscript for the indicated dimension of an
    ''     array.
    ''
    '' Parameters:
    ''   Array:
    ''     Required. Array of any data type. The array in which you want to find the
    ''     lowest possible subscript of a dimension.
    ''
    ''   Rank:
    ''     Optional. Integer. The dimension for which the lowest possible subscript
    ''     is to be returned. Use 1 for the first dimension, 2 for the second, and so
    ''     on. If Rank is omitted, 1 is assumed.
    ''
    '' Returns:
    ''     Integer. The lowest value the subscript for the specified dimension can contain.
    ''     LBound always returns 0 as long as Array has been initialized, even if it
    ''     has no elements, for example if it is a zero-length string. If Array is Nothing,
    ''     LBound throws an System.ArgumentNullException.
   
    <Pure()> _
    Public Shared Function LBound(ByVal Array As Array, ByVal Rank As Integer) As Integer
    
      Contract.Requires(Array IsNot Nothing)

      Contract.Requires(Rank > 0)
      Contract.Requires(Rank <= Array.Rank)

      Return Nothing
    End Function

#If Not SILVERLIGHT Then
    ''
    '' Summary:
    ''     Returns an Integer value representing the RGB color code corresponding to
    ''     the specified color number.
    ''
    '' Parameters:
    ''   Color:
    ''     Required. A whole number in the range 0–15.
    ''
    '' Returns:
    ''     Returns an Integer value representing the RGB color code corresponding to
    ''     the specified color number.
    Public Shared Function QBColor(ByVal Color As Integer) As Integer

      Contract.Requires(Color >= 0)
      Contract.Requires(Color <= 15)

      Contract.Ensures(Contract.Result(Of Integer)() >= 0)

      Return Nothing
    End Function

    ''
    '' Summary:
    ''     Returns an Integer value representing an RGB color value from a set of red,
    ''     green and blue color components.
    ''
    '' Parameters:
    ''   Red:
    ''     Required. Integer in the range 0–255, inclusive, that represents the intensity
    ''     of the red component of the color.
    ''
    ''   Green:
    ''     Required. Integer in the range 0–255, inclusive, that represents the intensity
    ''     of the green component of the color.
    ''
    ''   Blue:
    ''     Required. Integer in the range 0–255, inclusive, that represents the intensity
    ''     of the blue component of the color.
    ''
    '' Returns:
    ''     Returns an Integer value representing an RGB color value from a set of red,
    ''     green and blue color components.
    <Pure()> _
    Public Shared Function RGB(ByVal Red As Integer, ByVal Green As Integer, ByVal Blue As Integer) As Integer
    
      Contract.Requires(Red >= 0)
      Contract.Requires(Red <= 255)

      Contract.Requires(Green >= 0)
      Contract.Requires(Green <= 255)

      Contract.Requires(Blue >= 0)
      Contract.Requires(Blue <= 255)

      Contract.Ensures(Contract.Result(Of Integer)() >= 0)

      Return Nothing
    End Function
    ''
    '' Summary:
    ''     Returns a String value containing the system data type name of a variable.
    ''
    '' Parameters:
    ''   VbName:
    ''     Required. A String variable containing a Visual Basic type name.
    ''
    '' Returns:
    ''     Returns a String value containing the system data type name of a variable.
    <Pure()> _
    Public Shared Function SystemTypeName(ByVal VbName As String) As String
    
      Return Nothing
    End Function
    ''
    '' Summary:
    ''     Returns a String value containing data-type information about a variable.
    ''
    '' Parameters:
    ''   VarName:
    ''     Required. Object variable. If Option Strict is Off, you can pass a variable
    ''     of any data type except a structure.
    ''
    '' Returns:
    ''     Returns a String value containing data-type information about a variable.
    <Pure()> _
    Public Shared Function TypeName(ByVal VarName As Object) As String
    
      Return Nothing
    End Function
#End If
    ''
    '' Summary:
    ''     Returns the highest available subscript for the indicated dimension of an
    ''     array.
    ''
    '' Parameters:
    ''   Array:
    ''     Required. Array of any data type. The array in which you want to find the
    ''     highest possible subscript of a dimension.
    ''
    ''   Rank:
    ''     Optional. Integer. The dimension for which the highest possible subscript
    ''     is to be returned. Use 1 for the first dimension, 2 for the second, and so
    ''     on. If Rank is omitted, 1 is assumed.
    ''
    '' Returns:
    ''     Integer. The highest value the subscript for the specified dimension can
    ''     contain. If Array has only one element, UBound returns 0. If Array has no
    ''     elements, for example if it is a zero-length string, UBound returns -1.
    <Pure()> _
    Public Shared Function UBound(ByVal Array As Array, ByVal Rank As Integer) As Integer
    
      Contract.Requires(Array IsNot Nothing)

      Contract.Requires(Rank > 0)
      Contract.Requires(Rank <= Array.Rank)

      Return Nothing
    End Function

#If Not SILVERLIGHT Then
    ''
    '' Summary:
    ''     Returns an Integer value containing the data type classification of a variable.
    ''
    '' Parameters:
    ''   VarName:
    ''     Required. Object variable. If Option Strict is Off, you can pass a variable
    ''     of any data type except a structure.
    ''
    '' Returns:
    ''     Returns an Integer value containing the data type classification of a variable.
    ''public static VariantType VarType(object VarName)
    ''
    '' Summary:
    ''     Returns a String value containing the Visual Basic data type name of a variable.
    ''
    '' Parameters:
    ''   UrtName:
    ''     Required. String variable containing a type name used by the common language
    ''     runtime.
    ''
    '' Returns:
    ''     Returns a String value containing the Visual Basic data type name of a variable.
    <Pure()> _
    Public Shared Function VbTypeName(ByVal UrtName As String) As String
    
      Return Nothing
    End Function
#End If
  End Class
End Namespace
