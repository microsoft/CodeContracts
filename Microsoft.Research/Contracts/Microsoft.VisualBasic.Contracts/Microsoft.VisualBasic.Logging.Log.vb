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
Imports System.Diagnostics
Imports System.Diagnostics.Contracts

Namespace Microsoft.VisualBasic.Logging

  Public Class Log
  
    Public Sub New(ByVal name As String)
    
      Contract.Requires(name IsNot Nothing)
    End Sub

    ''/ Properties
    Public ReadOnly Property DefaultFileLogWriter() As FileLogTraceListener

      Get

        Contract.Ensures(Contract.Result(Of FileLogTraceListener)() IsNot Nothing)
        Return Nothing
      End Get
    End Property

    Public ReadOnly Property TraceSource() As TraceSource

      Get

        Contract.Ensures(Contract.Result(Of TraceSource)() IsNot Nothing)
        Return Nothing
      End Get
    End Property

  End Class
End Namespace

#End If
