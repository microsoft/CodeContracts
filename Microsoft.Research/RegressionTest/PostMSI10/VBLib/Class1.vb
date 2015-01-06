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

Public Class Class1


  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="requires is valid",PrimaryILOffset:=13,MethodILOffset:=112)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="assert is valid",PrimaryILOffset:=82,MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="requires is valid",PrimaryILOffset:=13,MethodILOffset:=104)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="requires is valid",PrimaryILOffset:=31,MethodILOffset:=104)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="requires is valid",PrimaryILOffset:=56,MethodILOffset:=104)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="ensures is valid",PrimaryILOffset:=37,MethodILOffset:=120)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="ensures is valid",PrimaryILOffset:=56,MethodILOffset:=120)> _
  Public Shared Function TrimSuffix(ByVal source As String, ByVal suffix As String) As String

    Contract.Requires(source IsNot Nothing)
    Contract.Requires(Not String.IsNullOrEmpty(suffix))
    Contract.Ensures(Contract.Result(Of String)() IsNot Nothing)
    Contract.Ensures(Not Contract.Result(Of String)().EndsWith(suffix))

    Dim result = source
    While result.EndsWith(suffix)
      Contract.Assert(result.Length >= suffix.Length)
      Dim len = result.Length - suffix.Length
      'Contract.Assert(len >= 0)
      result = result.Substring(0, len)
    End While

    Return result

  End Function

End Class

Public Module LinqTest
  <ClousotRegressionTest()> _
  Public Sub Callee(ByVal arg As IEnumerable(Of Byte))
    Contract.Requires(arg IsNot Nothing)
  End Sub
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="requires is valid",PrimaryILOffset:=13,MethodILOffset:=25)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="requires is valid",PrimaryILOffset:=31,MethodILOffset:=25)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="requires is valid",PrimaryILOffset:=7,MethodILOffset:=30)> _
  Public Sub Caller(ByVal arg As IEnumerable(Of Byte))
    Contract.Requires(arg IsNot Nothing)
    'Requirement unproven: M.Callee."arg IsNot Nothing"
    Call Callee(From i In arg Select i)
  End Sub
End Module

Public Module SourceLineIssue
  Public Sub Callee(ByVal arg As Object)
    Contract.Requires(arg IsNot Nothing)
  End Sub
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.False, Message:="requires is false: arg IsNot Nothing", PrimaryILOffset:=7, MethodILOffset:=1)> _
  Public Sub Caller()
    Call Callee( _
            Nothing)
  End Sub
End Module

