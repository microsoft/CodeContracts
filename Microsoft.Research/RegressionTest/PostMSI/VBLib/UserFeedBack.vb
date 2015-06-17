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
Imports Contracts.Regression
Imports System.IO
Imports System.Runtime.CompilerServices

Public Module M
  Public Sub R(ByVal arg As Double)
    Contract.Requires(Not Double.IsNaN(arg))
  End Sub
  <ClousotRegressionTest()> _
  Public Sub S()
    Call R(0) 'requirement NOT proven
  End Sub
End Module

Namespace Strilanc.BadRewrite
  Public Interface B
    ReadOnly Property P() As Boolean
  End Interface

  <ContractClass(GetType(CI(Of )))> _
  Public Interface I(Of T) 'note: generic type not used; but required for bug
    Inherits B
    Sub S()
  End Interface

  <ContractClassFor(GetType(I(Of )))> _
  Public MustInherit Class CI(Of T)
    Implements I(Of T)
    Public ReadOnly Property P() As Boolean Implements I(Of T).P
      Get
        Return True
      End Get
    End Property
    Public Sub S() Implements I(Of T).S
      Contract.Requires(Me.P) 'note: req must involve B for bug
    End Sub
  End Class

  Public Class C 'note: probably needed to reify the contracts
    Implements I(Of Boolean)
    Public ReadOnly Property P() As Boolean Implements B.P
      Get
        Return True
      End Get
    End Property
    Public Sub S() Implements I(Of Boolean).S
    End Sub
  End Class



End Namespace

Namespace Wurz
  Public Module MultiInheritWarning
    Public Sub Test()
      Dim x As New System.Collections.ObjectModel.Collection(Of Object)
      x.RemoveAt(0)

    End Sub

  End Module


End Namespace

Namespace Strilanc
  Public Class CWithEvents

    <ClousotRegressionTest()> _
    Public Sub New()
    End Sub
    Private WithEvents o As New Object
    <ContractInvariantMethod()> Private Sub ObjectInvariant()
      Contract.Invariant(o IsNot Nothing)
    End Sub


    <ClousotRegressionTest()> _
     Public Sub S()
      'o = Nothing
      'invariant unproven
    End Sub
  End Class

End Namespace

