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

Imports System.Runtime.CompilerServices
Imports System.Diagnostics.Contracts
Imports Contracts.Regression

Namespace Strilanc

  <ContractClass(GetType(C))> _
  Public Interface I
    Sub S()
  End Interface

  <ContractClassFor(GetType(I))> _
  Public MustInherit Class C
    Implements I
    Public Sub S() Implements I.S
      Contract.Requires(True)
    End Sub
  End Class

  Public Module Median
    <ClousotRegressionTest()> _
    <Pure()> <Extension()> _
    Public Function Between(Of T As IComparable(Of T))(ByVal value1 As T, _
                                                       ByVal value2 As T, _
                                                       ByVal value3 As T) As T
      Contract.Requires(value1 IsNot Nothing)
      Contract.Requires(value2 IsNot Nothing)
      Contract.Requires(value3 IsNot Nothing)
      Contract.Ensures(Contract.Result(Of T)() IsNot Nothing)
      'recursive sort
      If value2.CompareTo(value1) > 0 Then Return Between(value2, value1, value3)
      If value2.CompareTo(value3) < 0 Then Return Between(value1, value3, value2)
      'median
      Return value2
    End Function

    <ClousotRegressionTest()> _
    Public Sub Test(ByVal i As Integer)
      Dim r = i.Between(0, 10)
    End Sub
  End Module

  Public Structure S
    Public ReadOnly x As Integer
    <ClousotRegressionTest()> _
    Public Sub New(ByVal x As Integer)
      Contract.Ensures(Me.x = x)
      Me.x = x
    End Sub
    <ClousotRegressionTest()> _
    Public Function F() As S
      Contract.Ensures(Contract.Result(Of S)().x = 0)
      Return New S(0)
    End Function
  End Structure


End Namespace
