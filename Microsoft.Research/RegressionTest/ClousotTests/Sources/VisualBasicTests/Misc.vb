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

Imports Microsoft.Research.ClousotRegression
Imports System.Diagnostics.Contracts

Public Class Misc1

  <ClousotRegressionTest()> _
  Public Sub MySub()
    Dim R = New With {.Prop = "Property Value"}
  End Sub

End Class

Public Class JimitBase(Of T)

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="valid non-null reference (as receiver)",PrimaryILOffset:=2,MethodILOffset:=0)> _
  Public Sub New(ByVal initialValues As ICollection(Of T), _
                 ByVal comparer As IComparer(Of T))
    Me.New(comparer)
    Contract.Requires(initialValues IsNot Nothing)
    'Additional code...  
  End Sub





  '<ClousotRegressionTest()> _
  Public Sub New(ByVal comparer As IComparer(Of T))
    'No preconditions...   
    'Additional code...  
  End Sub

End Class

Public Class JimitDerived(Of T)
  Inherits JimitBase(Of T)

  '<ClousotRegressionTest()> _
  Public Sub New(ByVal initialValues As ICollection(Of T), _
                 ByVal comparer As IComparer(Of T), _
                 Optional ByVal onAdd As Action(Of T) = Nothing, _
                 Optional ByVal onRemove As Action(Of T) = Nothing)
    MyBase.New(initialValues, comparer)
    Contract.Requires(Of ArgumentNullException)(initialValues IsNot Nothing)
    'Additional code...  
  End Sub
End Class

Public Class Initializers

  Structure Mine
    Private m_A As Integer
    Private m_B As Integer
    Private m_C As Integer
    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer)
      m_A = x
      m_B = y
      m_C = z
    End Sub
  End Structure

  Private m_ClassName As String = String.Empty
  Private m_OptInt As Integer? = 5
  Private m_Mine As Mine = New Mine(1, 2, 3)

  Public Sub New(ByVal Param As Object)
    Contract.Requires(Param IsNot Nothing)
  End Sub

End Class

Class cls1
  Dim _m As Integer

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="valid non-null reference (as receiver)",PrimaryILOffset:=37,MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="valid non-null reference (as field receiver)",PrimaryILOffset:=8,MethodILOffset:=42)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="ensures is valid",PrimaryILOffset:=30,MethodILOffset:=42)> _
  Function moo(ByVal x As Integer) As Object
    Contract.Ensures(Contract.Result(Of Object)() IsNot Nothing OrElse _
                     _m = Contract.OldValue(_m))

    Return foo(x)
  End Function


  Function foo(ByVal x As Integer) As Object
    Contract.Ensures(Contract.Result(Of Object)() IsNot Nothing OrElse _
                     _m = Contract.OldValue(_m))

 
    If x > 0 Then
      _m += 1
      Return "hello"
    Else
      Return Nothing
    End If
  End Function
End Class

Public Class XmlLiteralTest
  Public Shared Sub bind(ByRef dmnd As Demande, ByVal intval As Integer)
    Contract.Requires(dmnd IsNot Nothing)

    Dim writeline1 = Sub() Console.WriteLine(intval)


    Dim sql As String = <sql>
			[MySQLString]
		</sql>.Value
  End Sub
End Class

Public Class Demande

End Class



Public Class RunTests
  Public Shared Sub Run()

    Dim d As New Demande
    XmlLiteralTest.bind(d, 5)
    Try
      XmlLiteralTest.bind(Nothing, 5)
    Catch ex As Exception
    End Try


    Dim e As New ValidationException(d, 5)
    Try
      Dim e2 As New ValidationException(Nothing, 5)
    Catch ex As Exception
    End Try

  End Sub
End Class

Public NotInheritable Class ValidationException
  Inherits Exception

  Public Property Result() As Object
  ' VB compiler emits a strange __ENCAddToList call in the constructor that we need to move to the preamble.
  ' This happens only in Debug mode and is documented here: http://forum.memprofiler.com/viewtopic.php?p=2025
  '
  Public Sub New(ByVal ValidateResult As Object, ByVal intval As Integer)
    Contract.Requires(Of ArgumentNullException)(ValidateResult IsNot Nothing, "ValidateResult is nothing.")

    Dim writeline1 = Sub() Console.WriteLine(intval)


    Dim sql As String = <sql>
			[MySQLString]
		</sql>.Value

    Me.Result = ValidateResult
  End Sub
End Class