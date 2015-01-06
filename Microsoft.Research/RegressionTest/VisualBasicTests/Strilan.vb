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
Imports Microsoft.Research.ClousotRegression
Imports System.Diagnostics.CodeAnalysis

<Assembly: RegressionOutcome("Method 'VisualBasicTests.Strilan.C.S(System.Int32)' cannot implement/override two methods 'VisualBasicTests.Strilan.IPositive.P(System.Int32)' and 'VisualBasicTests.Strilan.INonNegative.N(System.Int32)', where one has Requires.")> 

Namespace Strilan

  <Pure()> <ContractVerification(True)> _
  Public NotInheritable Class MStack(Of T)
    <ContractInvariantMethod()> Private Sub invariant()
      Contract.Invariant(Me.size >= 0)

      Contract.Invariant((Me.size = 0) = (Me.[next] Is Nothing))
      Contract.Invariant(Me.size = 0 OrElse Me.size = Me.[next].size + 1)
    End Sub

    Private ReadOnly [next] As MStack(Of T)
    Private ReadOnly val As T
    Public ReadOnly size As Integer

    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=1, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=7, MethodILOffset:=34)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=21, MethodILOffset:=34)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=15, MethodILOffset:=34)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=29, MethodILOffset:=34)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="invariant is valid", PrimaryILOffset:=12, MethodILOffset:=34)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="invariant is valid", PrimaryILOffset:=37, MethodILOffset:=34)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="invariant is valid", PrimaryILOffset:=76, MethodILOffset:=34)> _
    Private Sub New()
      Contract.Ensures(Me.size = 0)
      Contract.Ensures(Me.[next] Is Nothing)
    End Sub

    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=19, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=1, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=75, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=82, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=89, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=96, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=36, MethodILOffset:=101)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=42, MethodILOffset:=101)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=57, MethodILOffset:=101)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=51, MethodILOffset:=101)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=68, MethodILOffset:=101)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="invariant is valid", PrimaryILOffset:=12, MethodILOffset:=101)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="invariant is valid", PrimaryILOffset:=37, MethodILOffset:=101)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="invariant is valid", PrimaryILOffset:=76, MethodILOffset:=101)> _
    Private Sub New(ByVal val As T, ByVal [next] As MStack(Of T))
      Contract.Requires([next] IsNot Nothing)
      Contract.Requires([next].size >= 0)
      Contract.Ensures(Me.size = [next].size + 1)
      Contract.Ensures(Me.next IsNot Nothing)

      Me.val = val
      Me.next = [next]
      Me.size = [next].size + 1
    End Sub

    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=1, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=15, MethodILOffset:=0)> _
    Public Function peek() As T
      Contract.Requires(size > 0)

      Return val
    End Function

    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=1, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=56, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=35, MethodILOffset:=61)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=41, MethodILOffset:=61)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=25, MethodILOffset:=61)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=50, MethodILOffset:=61)> _
    Public Function popped() As MStack(Of T)
      Contract.Requires(size > 0)
      Contract.Ensures(Contract.Result(Of MStack(Of T))() IsNot Nothing)
      Contract.Ensures(Contract.Result(Of MStack(Of T)).size = Me.size - 1)

      Return Me.next
    End Function



    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=21, MethodILOffset:=48)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=27, MethodILOffset:=48)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=43)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=30, MethodILOffset:=43)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=11, MethodILOffset:=48)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=36, MethodILOffset:=48)> _
    Public Function pushed(ByVal val As T) As MStack(Of T)
      Contract.Ensures(Contract.Result(Of MStack(Of T))() IsNot Nothing)
      Contract.Ensures(Contract.Result(Of MStack(Of T)).size = Me.size + 1)

      Return New MStack(Of T)(val, Me)
    End Function
  End Class

  Public Class CtorContractProblem
    Dim malformed As Boolean = X(New TimeSpan(0, 0, 0))

    Private Shared Function X(ByVal t As TimeSpan) As Boolean
      Return True
    End Function

    Public Sub New()
      Contract.Requires(True)
    End Sub


    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=29, MethodILOffset:=0)> _
    Public Sub Test()
      Dim x = New CtorContractProblem()
    End Sub

  End Class

  Public Interface I
    Event E()
  End Interface
  Public Class CtorContractProblem2
    Implements I
    Public Event E() Implements I.E
    Public Sub New()
      Contract.Requires(True)
    End Sub

    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=0)> _
    Public Sub Test()
      Dim x = New CtorContractProblem2()
    End Sub

  End Class


  Public Module LinqTest
    <ClousotRegressionTest()> _
    Public Sub Callee(ByVal arg As IEnumerable(Of Byte))
      Contract.Requires(arg IsNot Nothing)
    End Sub
    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=25)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=31, MethodILOffset:=25)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=30)> _
    Public Sub Caller(ByVal arg As IEnumerable(Of Byte))
      Contract.Requires(arg IsNot Nothing)
      'Requirement unproven: M.Callee."arg IsNot Nothing"
      Call Callee(From i In arg Select i)
    End Sub
  End Module



  <ContractClass(GetType(CPositive))> _
  Public Interface IPositive
    Sub P(ByVal arg As Integer)
  End Interface
  <ContractClass(GetType(CNonNegative))> _
  Public Interface INonNegative
    Sub N(ByVal arg As Integer)
  End Interface

  <ContractClassFor(GetType(IPositive))> _
  MustInherit Class CPositive
    Implements IPositive
    Public Sub P(ByVal arg As Integer) Implements IPositive.P
      Contract.Requires(arg > 0)
    End Sub
  End Class
  <ContractClassFor(GetType(INonNegative))> _
  MustInherit Class CNonNegative
    Implements INonNegative
    Public Sub N(ByVal arg As Integer) Implements INonNegative.N
      Contract.Requires(arg >= 0)
    End Sub
  End Class

  <SuppressMessage("Microsoft.Contracts", "CC1035")> _
  Public Class C
    Implements IPositive
    Implements INonNegative

    ''' <summary>
    ''' Expect a warning from the rewriter for this method. 
    ''' </summary>
    Public Sub S(ByVal arg As Integer) Implements IPositive.P, INonNegative.N
    End Sub
  End Class

  Public Module M
    Public Sub Test()
      Dim x As INonNegative = New C()
      Call x.N(0) 'oops!
    End Sub
  End Module

  ''' <summary>
  ''' Check for bad instantiations of return types in a method. This code used to cause assertions in the writer
  ''' and badly instantiated return types of methods. A fix to CCI1's Specializer removes this problem (maf 9/30/2009)
  ''' </summary>
  Public Interface I(Of T)
  End Interface

  Public Class B(Of T)
    Public Class C
    End Class

    Public Function F() As I(Of C)
      Return Nothing
    End Function
  End Class

  Public Module BadInstantiationRepro
    Public Sub S()
      Call New B(Of Byte)().F()
    End Sub
  End Module

  Public Module Doubles
    <ClousotRegressionTest()> _
     <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=28, MethodILOffset:=52)> _
      <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="ensures unproven: Contract.Result(Of Boolean)() = Not Double.IsInfinity(value) AndAlso Not Double.IsNaN(value)", PrimaryILOffset:=28, MethodILOffset:=50)> _
    Public Function IsFinite_NOTOK(ByVal value As Double) As Boolean
      Contract.Ensures(Contract.Result(Of Boolean)() = Not Double.IsInfinity(value) AndAlso Not Double.IsNaN(value))  ' wrong parentheses
      Return Not Double.IsInfinity(value) AndAlso Not Double.IsNaN(value)
    End Function

    <ClousotRegressionTest()> _
     <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=27, MethodILOffset:=49)> _
      <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=27, MethodILOffset:=51)> _
    Public Function IsFinite(ByVal value As Double) As Boolean
      Contract.Ensures(Contract.Result(Of Boolean)() = (Not Double.IsInfinity(value) AndAlso Not Double.IsNaN(value)))
      Return Not Double.IsInfinity(value) AndAlso Not Double.IsNaN(value)
    End Function
  End Module

  Public Module Median
    <ClousotRegressionTest()> _
    <Pure()> <Extension()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=12, MethodILOffset:=92)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=29, MethodILOffset:=92)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=46, MethodILOffset:=92)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=67, MethodILOffset:=97)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=12, MethodILOffset:=118)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=29, MethodILOffset:=118)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=46, MethodILOffset:=118)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=67, MethodILOffset:=123)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=67, MethodILOffset:=125)> _
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
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=12, MethodILOffset:=4)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=29, MethodILOffset:=4)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=46, MethodILOffset:=4)> _
    Public Sub Test(ByVal i As Integer)
      Dim r = i.Between(0, 10)
    End Sub
  End Module

  Public Structure StructWithLocalReuseIssue
    Public ReadOnly x As Integer
    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=16, MethodILOffset:=28)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=23, MethodILOffset:=0)> _
    Public Sub New(ByVal x As Integer)
      Contract.Ensures(Me.x = x)
      Me.x = x
    End Sub
    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=16, MethodILOffset:=30)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=24, MethodILOffset:=0)> _
    Public Function F() As StructWithLocalReuseIssue
      Contract.Ensures(Contract.Result(Of StructWithLocalReuseIssue)().x = 0)
      Return New StructWithLocalReuseIssue(0)
    End Function
  End Structure

  Public Class ModOperator
    <Pure()> _
    <ClousotRegressionTest()> _
     <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=17, MethodILOffset:=31)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=17, MethodILOffset:=33)> _
    Public Function ProperMod(ByVal value As Integer, ByVal divisor As Integer) As Integer
      Contract.Requires(divisor > 0)
      Contract.Ensures(Contract.Result(Of Integer)() < divisor)

      If (value >= 0) Then
        Return value Mod divisor + 0 ' we had a bug here because the VB compiler leaves "+0" in the IL
      Else
        Return 0
      End If
    End Function
  End Class

  Module Ranges
    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=7, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=19, MethodILOffset:=0)> _
    Public Sub SU32(ByVal u32 As UInt32)
      Contract.Assert(u32 >= UInt32.MinValue)
      Contract.Assert(u32 <= UInt32.MaxValue)
    End Sub

    <ClousotRegressionTest()> _
            <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=8, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="assert unproven", PrimaryILOffset:=21, MethodILOffset:=0)> _
    Public Sub SU64(ByVal u64 As UInt64)
      Contract.Assert(u64 >= UInt64.MinValue)
      Contract.Assert(u64 <= UInt64.MaxValue) ' we had a bug here because of wrong handling of lt_un
    End Sub
  End Module

End Namespace
