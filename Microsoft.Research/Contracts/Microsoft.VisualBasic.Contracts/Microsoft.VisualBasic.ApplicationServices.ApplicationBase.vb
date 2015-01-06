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

#If Not SILVERLIGHT Then

Imports System
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Diagnostics.Contracts
Imports Microsoft.VisualBasic.Logging


Namespace Microsoft.VisualBasic.ApplicationServices

  Class ApplicationBase

    Public ReadOnly Property Culture() As CultureInfo
      Get
        Contract.Ensures(Contract.Result(Of CultureInfo)() IsNot Nothing)
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property Info() As AssemblyInfo
      Get
        Contract.Ensures(Contract.Result(Of AssemblyInfo)() IsNot Nothing)
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property Log() As Log
      Get
        Contract.Ensures(Contract.Result(Of Log)() IsNot Nothing)
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property UICulture As CultureInfo
      Get
        Contract.Ensures(Contract.Result(Of CultureInfo)() IsNot Nothing)
        Return Nothing
      End Get
    End Property

  End Class
End Namespace

#End If
