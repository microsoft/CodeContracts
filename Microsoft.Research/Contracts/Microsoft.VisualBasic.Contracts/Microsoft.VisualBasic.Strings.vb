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
Imports System.Diagnostics.Contracts

Namespace Microsoft.VisualBasic

  Public Enum CompareMethod
    Binary
    Text
  End Enum


  Public Class Strings
    Private Sub New()
    End Sub

    Public Shared Function Len(ByVal str As String) As Integer

      Contract.Ensures(Contract.Result(Of Integer)() = str.Length)
      Return Nothing

    End Function

    Public Shared Function InStr(ByVal string1 As String, ByVal string2 As String, ByVal cmp As CompareMethod) As Integer

      Contract.Ensures(Contract.Result(Of Integer)() < 0 OrElse string1 IsNot Nothing)
      Return Nothing
    End Function

    Public Shared Function Mid(ByVal str As String, ByVal Start As Integer, ByVal Length As Integer) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)

      Return Nothing
    End Function

    Public Shared Function Split(ByVal Expression As String, ByVal Delimiter As String, ByVal Limit As Integer, ByVal Compare As CompareMethod) As String()

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)

      Return Nothing
    End Function
  End Class
End Namespace
