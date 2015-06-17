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
Imports System.Deployment.Application
Imports System.Diagnostics.Contracts

Namespace Microsoft.VisualBasic.ApplicationServices

  Class ConsoleApplicationBase

    Public ReadOnly Property CommandLineArgs() As ReadOnlyCollection(Of String)

      Get

        Contract.Ensures(Contract.Result(Of ReadOnlyCollection(Of string))() IsNot Nothing)
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property Deployment() As ApplicationDeployment

      Get

        Contract.Ensures(Contract.Result(Of ApplicationDeployment)() IsNot Nothing)
        Return Nothing
      End Get
    End Property
    
    Protected WriteOnly Property InternalCommandLine() As ReadOnlyCollection(Of String)

      Set(ByVal value As ReadOnlyCollection(Of String))

        Contract.Requires(Value IsNot Nothing)
      End Set
    End Property


  End Class
End Namespace

#End If
