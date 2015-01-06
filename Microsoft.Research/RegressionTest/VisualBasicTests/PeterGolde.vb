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
Imports System.Diagnostics.Contracts.Contract
Imports Microsoft.Research.ClousotRegression

Public Class StupidList
  Private _elements As Object()

  <ContractInvariantMethod()> _
  Private Sub ObjectInvariant()
    Invariant(_elements IsNot Nothing)
  End Sub

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Array creation : ok", PrimaryILOffset:=24, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=1, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=29, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="invariant is valid", PrimaryILOffset:=12, MethodILOffset:=34)> _
  Public Sub New(ByVal size As Integer)
    Requires(size >= 0)
    _elements = New Object(0 To size - 1) {}
  End Sub

  Public ReadOnly Property Count() As Integer
    Get
      Ensures(Result(Of Integer)() >= 0)

      Return _elements.Length
    End Get
  End Property

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=33, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="Array access might be above the upper bound", PrimaryILOffset:=33, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=54, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=54, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=14, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=27, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=33, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=48, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=54, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (in unbox)", PrimaryILOffset:=55, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="invariant is valid", PrimaryILOffset:=12, MethodILOffset:=60)> _
  Public Function RemoveItem(Of T)(ByVal index As Integer) As T
    Requires(index >= 0)
    Requires(index < Count)

    Assume(TypeOf _elements(index) Is T)
    Return DirectCast(_elements(index), T)
  End Function


  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=33, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="Array access might be above the upper bound", PrimaryILOffset:=33, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=14, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=27, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=33, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="Possibly unboxing a null reference", PrimaryILOffset:=34, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="invariant is valid", PrimaryILOffset:=12, MethodILOffset:=39)> _
  Public Function RemoveItemFail(Of T)(ByVal index As Integer) As T
    Requires(index >= 0)
    Requires(index < Count)

    'Assume(TypeOf _elements(index) Is T)
    Return DirectCast(_elements(index), T)
  End Function
End Class

''' <summary>
''' Check that we extract properly when ctor has initialization of local temps after base ctor call
''' </summary>
Friend Class Iterator(Of T)
  Implements IEnumerable(Of T)

  ''' <summary>
  ''' special func to create generators. the value is returned as a ByRef argument 
  ''' while result used to indicate when itearating finishes.
  ''' </summary>
  Friend Delegate Function GeneratorFunc(Of U As T)(ByRef val As U) As Boolean

  Private ReadOnly _generatorFactory As Func(Of GeneratorFunc(Of T))

  <ContractInvariantMethod()> _
  Private Sub ObjectInvariant()
    Contract.Invariant(_generatorFactory IsNot Nothing)
  End Sub

  Friend Sub New(ByVal generatorFactory As Func(Of GeneratorFunc(Of T)))
    Contract.Requires(generatorFactory IsNot Nothing)
    _generatorFactory = generatorFactory
  End Sub

  Friend Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
    Return New Enumerator(Of T)(_generatorFactory())
  End Function

  Friend Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    Return New Enumerator(Of T)(_generatorFactory())
  End Function

  ''' <summary>
  ''' Enumerator wrapper for a generator lambda.
  ''' </summary>
  Private Class Enumerator(Of U As T)
    Implements IEnumerator(Of U)

    Private ReadOnly _generator As GeneratorFunc(Of U)
    Private _current As U = Nothing

    <ContractInvariantMethod()> _
    Private Sub ObjectInvariant()
      Contract.Invariant(_generator IsNot Nothing)
    End Sub

    Friend Sub New(ByVal generator As GeneratorFunc(Of U))
      Contract.Requires(generator IsNot Nothing)
      _generator = generator
      ' _current = Nothing
    End Sub

    Private ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
      Get
        Return Current
      End Get
    End Property

    Friend Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
      Return _generator(_current)
    End Function

    Friend Sub Reset() Implements System.Collections.IEnumerator.Reset
      Throw New InvalidOperationException("Reset is not supported.")
    End Sub

    Friend ReadOnly Property Current() As U Implements System.Collections.Generic.IEnumerator(Of U).Current
      Get
        Return _current
      End Get
    End Property

    Friend Sub Dispose() Implements IDisposable.Dispose
    End Sub
  End Class ' Enumerator
End Class

