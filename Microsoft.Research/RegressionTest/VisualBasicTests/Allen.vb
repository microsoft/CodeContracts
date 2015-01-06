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
Imports System.Runtime.CompilerServices
Imports System.Net.Mail

Public Class Allen


    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=7, MethodILOffset:=0)> _
    Public Sub Test1()
        Dim x As Byte = 5
        Dim s As String = Nothing
        Console.WriteLine(x.ToString(s))
    End Sub
    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=22, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=28, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=41, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=34)> _
    Function test(ByVal source As IEnumerable(Of Byte), ByVal format As String) As String
        Dim temp = TryCast(source, ICollection(Of Byte))
        Dim result As Text.StringBuilder
        If temp Is Nothing OrElse format Is Nothing Then
            result = New Text.StringBuilder()
        Else
            result = New Text.StringBuilder(temp.Count * format.Length)
        End If
        Return result.ToString
    End Function

    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=13)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=18)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=31, MethodILOffset:=0)> _
    Sub Test(Of T)(ByVal array As T(,), ByVal sortColumn As Integer)
        Contract.Requires(array IsNot Nothing)
        Dim sorted = (From fragment In array).ToArray
        Contract.Assert(sorted IsNot Nothing) '<-- error here   
    End Sub

    Function SortByColumn(Of T)(ByVal array As T(,), ByVal sortColumn As Integer) As T(,)
        Contract.Requires(Of ArgumentNullException)(array IsNot Nothing)
        Contract.Requires(Of ArgumentException)(array.GetUpperBound(0) >= sortColumn)
        Contract.Ensures(Contract.Result(Of T(,))() IsNot Nothing)
        Return Nothing
    End Function

End Class

Public Module AllenModule
    <ClousotRegressionTest("cci1only")> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=21, MethodILOffset:=18)> _
    <Extension()> Public Function IsEmailAddress(ByVal value As String) As Boolean
        If value = "" Then Return False
        Try
            Dim temp = New MailAddress(value)
            Return True
        Catch ex As FormatException
            Return False
        End Try
    End Function

End Module
