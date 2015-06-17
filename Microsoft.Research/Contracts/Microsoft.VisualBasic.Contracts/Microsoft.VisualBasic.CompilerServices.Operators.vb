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

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Diagnostics.Contracts

Namespace Microsoft.VisualBasic.CompilerServices

  Public Class Operators

    Private Sub New()
    End Sub

    <Pure()> _
    Public Shared Function AddObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function AndObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function CompareObject(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Integer
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function CompareObjectEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function CompareObjectGreater(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function CompareObjectGreaterEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function CompareObjectLess(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function CompareObjectLessEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function CompareObjectNotEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function CompareString(ByVal Left As String, ByVal Right As String, ByVal TextCompare As Boolean) As Integer

      Contract.Ensures(Not Object.ReferenceEquals(Left, Right) OrElse Contract.Result(Of Integer)() = 0)

      Contract.Ensures(Contract.Result(Of Integer)() <> 0 OrElse
                       (Object.ReferenceEquals(Left, Nothing) AndAlso Object.ReferenceEquals(Right, Nothing)) OrElse
                       (Object.ReferenceEquals(Left, Nothing) AndAlso Right.Length = 0) OrElse
                       (Object.ReferenceEquals(Right, Nothing) AndAlso Left.Length = 0) OrElse
                       (Left.Length = Right.Length))

      Contract.Ensures(Contract.Result(Of Integer)() = 0 OrElse
                       (Object.ReferenceEquals(Right, Nothing) AndAlso Left.Length > 0) OrElse
                       (Object.ReferenceEquals(Left, Nothing) AndAlso Right.Length > 0) OrElse
                       (Not Object.ReferenceEquals(Left, Nothing) AndAlso Not Object.ReferenceEquals(Right, Nothing) AndAlso (Left.Length > 0 OrElse Right.Length > 0)))

      Return Nothing
    End Function

    <Pure()> _
    Public Shared Function ConcatenateObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function ConditionalCompareObjectEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function ConditionalCompareObjectGreater(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function ConditionalCompareObjectGreaterEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function ConditionalCompareObjectLess(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function ConditionalCompareObjectLessEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function ConditionalCompareObjectNotEqual(ByVal Left As Object, ByVal Right As Object, ByVal TextCompare As Boolean) As Boolean
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Function DivideObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Function ExponentObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Function IntDivideObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Function LeftShiftObject(ByVal Operand As Object, ByVal Amount As Object) As Object
      Return Nothing
    End Function
#If Not SILVERLIGHT Then
    <Pure()> _
    Public Shared Function LikeObject(ByVal Source As Object, ByVal Pattern As Object, ByVal CompareOption As CompareMethod) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function LikeString(ByVal Source As String, ByVal Pattern As String, ByVal CompareOption As CompareMethod) As Boolean
      Return Nothing
    End Function
#End If
    <Pure()> _
    Public Shared Function ModObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function MultiplyObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function NegateObject(ByVal Operand As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function NotObject(ByVal Operand As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function OrObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function PlusObject(ByVal Operand As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function RightShiftObject(ByVal Operand As Object, ByVal Amount As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function SubtractObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function
    <Pure()> _
    Public Shared Function XorObject(ByVal Left As Object, ByVal Right As Object) As Object
      Return Nothing
    End Function
  End Class
End Namespace
