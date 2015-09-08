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

  Class Conversions

    Private Sub New()
    End Sub

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal b As Boolean) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal b As Byte) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal c As Char) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal d As Decimal) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal d As Double) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal i16 As Int16) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal i32 As Int32) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal i64 As Int64) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal obj As Object) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal s As Single) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal ui32 As UInt32) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function

    <Pure()> _
    Public Shared Shadows Function ToString(ByVal ui64 As UInt64) As String

      Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
      Return Nothing
    End Function
  End Class
End Namespace

