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
Imports Contracts.Regression


Module PeterRepro1

	<ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="requires is valid",PrimaryILOffset:=7,MethodILOffset:=21)> _
	Sub Main()
		Dim o1 As New C1(14)
		Dim o2 As New C2

		o2.DoStuff(o1.Width)
	End Sub
End Module

Public Class C1
	Public Property Width As Integer

	<ContractInvariantMethod()> _
	 Private Sub ObjectInvariant()
		Invariant(Width >= 0)
	End Sub

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="invariant unproven: Width >= 0", MethodILOffset:=13, PrimaryILOffset:=12)> _
  Public Sub New(ByVal width As Integer)
    'Requires(width >= 0)
    Me.Width = width
  End Sub
End Class

Public Class C2
	Public Sub DoStuff(ByVal width As Integer)
		Requires(width >= 0)
	End Sub
End Class
