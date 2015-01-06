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
Imports System.Text
Imports System.Diagnostics.Contracts
Imports System.Globalization
Imports System.Reflection

Namespace Microsoft.VisualBasic.ApplicationServices

  Class AssemblyInfo
  
    Public Sub New(ByVal currentAssembly As Assembly)
      Contract.Requires(currentAssembly IsNot Nothing)

    End Sub

    '' Properties
    Public ReadOnly Property AssemblyName() As String

      Get

        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property CompanyName() As String

      Get

        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property Copyright() As String

      Get

        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
        Return Nothing
      End Get
    End Property


    Public ReadOnly Property Description() As String

      Get

        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
        Return Nothing
      End Get
    End Property


    Public ReadOnly Property DirectoryPath() As String

      Get

        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
        Return Nothing
      End Get
    End Property


    Public ReadOnly Property LoadedAssemblies() As ReadOnlyCollection(Of Assembly)

      Get

        Contract.Ensures(Contract.Result(Of ReadOnlyCollection(Of Assembly))() IsNot Nothing)
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property ProductName() As String

      Get

        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
        Return Nothing
      End Get
    End Property


    Public ReadOnly Property StackTrace() As String

      Get

        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
        Return Nothing
      End Get
    End Property


    Public ReadOnly Property Title() As String

      Get

        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
        Return Nothing
      End Get
    End Property


    Public ReadOnly Property Trademark() As String

      Get

        Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
        Return Nothing
      End Get
    End Property


    Public ReadOnly Property Version() As Version

      Get

        Contract.Ensures(Contract.Result(Of Version)() IsNot Nothing)
        Return Nothing
      End Get
    End Property
  End Class
End Namespace


#End If

