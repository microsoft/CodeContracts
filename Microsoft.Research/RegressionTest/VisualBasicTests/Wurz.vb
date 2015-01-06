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

Option Strict On

Imports Microsoft.Research.ClousotRegression
Imports System.Diagnostics.Contracts
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports System.Diagnostics.CodeAnalysis

<Assembly: RegressionOutcome("Method 'VisualBasicTests.Wurz16.Test' has custom parameter validation but assembly mode is not set to support this. It will be treated as Requires<E>.")> 

Public Class Wurz1

  <ClousotRegressionTest()> _
  Public Sub Test(ByVal MyObj As String)
    Contracts.Contract.Requires(MyObj IsNot Nothing)
  End Sub

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.False, Message:="requires is false: MyObj IsNot Nothing", PrimaryILOffset:=7, MethodILOffset:=2)> _
  Public Sub Test2()
    Test(Nothing)
  End Sub

End Class

Public Class Wurz2
  Public Structure MyStructure
    Public MyVar As Integer
  End Structure
  Public Shared MyStruct As MyStructure
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=6, MethodILOffset:=0)> _
  Sub Test()
    MyStruct.MyVar = 5
  End Sub
End Class

Public Class Wurz3
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Array creation : ok", PrimaryILOffset:=1, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Array creation : ok", PrimaryILOffset:=8, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Array creation : ok", PrimaryILOffset:=58, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Array creation : ok", PrimaryILOffset:=83, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=106, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="Array access might be above the upper bound", PrimaryILOffset:=106, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=111, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=111, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=115, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=115, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=128, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=128, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=131, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=131, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=150, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=150, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=166, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=166, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=173, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=173, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=189, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=189, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=194, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="Array access might be above the upper bound", PrimaryILOffset:=194, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=106, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=111, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=115, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=128, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=131, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=150, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=166, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=173, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=189, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=194, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=161, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=184, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=161)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=33, MethodILOffset:=161)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=184)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=33, MethodILOffset:=184)> _
  Sub Test()
    Dim arrParams() As String
    Dim arrFields() As String
    Dim j As Integer, i As Integer
    Dim txt As String
    Const MyCount As Integer = 1
    ReDim arrParams(0)
    ReDim arrFields(0)
    j = 0
    For i = 0 To MyCount - 1 'txtDisplay.Count - 1
      txt = "Whatever"
      If txt IsNot Nothing AndAlso Len(txt) <> 0 Then
        j = j + 1
        ReDim Preserve arrParams(j)
        ReDim Preserve arrFields(j)
        arrParams(j) = "" 'l_convToServerText(txt.Text, False)
        arrParams(j) = Mid(arrParams(j), 2, Len(arrParams(j)) - 2)

        If InStr(arrParams(j), "[") > 0 Then
          arrParams(j) = arrParams(j).Replace("[", "[[]")
        Else
          arrParams(j) = arrParams(j).Replace("]", "[]]")
        End If
        arrFields(j) = CStr(txt)
      End If
    Next
  End Sub



  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=22, MethodILOffset:=0)> _
  Sub TestInStr(ByVal str As String)
    If InStr(str, "[") >= 0 Then
      Contracts.Contract.Assert(str IsNot Nothing)
    End If

  End Sub


End Class

Public Class Wurz4

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=12, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=19, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=29, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=34, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=34)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=21, MethodILOffset:=34)> _
  Private Sub Test()
    Dim sbText As System.Text.StringBuilder
    sbText = New Text.StringBuilder
    sbText.Append("Test")
    If sbText.Length > 0 Then
      Test2(sbText.ToString())
    End If
  End Sub
  Private Sub Test2(ByVal str As String)
    Contracts.Contract.Requires(str IsNot Nothing)
    Contracts.Contract.Requires(str.Length > 0)
  End Sub

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=19, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=32, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=37, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=19)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=37)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=36, MethodILOffset:=37)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=45, MethodILOffset:=19)> _
  Private Sub Test3(ByVal strTmp As String)
    Contract.Requires(strTmp IsNot Nothing)
    Dim LastDisclaimerVersion As String
    If strTmp.StartsWith("LastDisclaimerVersion=", StringComparison.CurrentCulture) Then
      LastDisclaimerVersion = strTmp.Substring("LastDisclaimerVersion=".Length)
    End If
  End Sub


End Class

Public Class Wurz5
  Private Shared MyColClass1 As System.Collections.ObjectModel.Collection(Of Wurz5)

  <ContractInvariantMethod()> _
  Private Sub ObjectInvariantBlah()
    Contract.Invariant(Not DirectCast(MyColClass1, ICollection(Of Wurz5)).IsReadOnly)
  End Sub

  Private MyAssembly As System.Reflection.Assembly
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=1, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=37, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=65, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=19, MethodILOffset:=88)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=30, MethodILOffset:=88)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=75, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="invariant is valid", PrimaryILOffset:=13, MethodILOffset:=88)> _
  Public Sub New(ByVal aAssembly As System.Reflection.Assembly)
    Contract.Requires(aAssembly IsNot Nothing)
    Contract.Ensures(MyAssembly IsNot Nothing)
    'Contract.Assume(Not DirectCast(MyColClass1, ICollection(Of Wurz5)).IsReadOnly)
    MyAssembly = aAssembly
    If MyColClass1 Is Nothing Then
      MyColClass1 = New System.Collections.ObjectModel.Collection(Of Wurz5)
      'Contract.Assert(Not DirectCast(MyColClass1, ICollection(Of Wurz5)).IsReadOnly)
    End If
    MyColClass1.Add(Me)
    Contract.Assume(Not DirectCast(MyColClass1, ICollection(Of Wurz5)).IsReadOnly)
  End Sub


End Class

Public Class Wurz6
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Array creation : ok", PrimaryILOffset:=29, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=39, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=39, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=49, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=49, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=20, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=39, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=41, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=49, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=53, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=58, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=78, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=86, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=105, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=112, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=58)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=36, MethodILOffset:=58)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=78)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=97, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=112)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=31, MethodILOffset:=112)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=54, MethodILOffset:=112)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=79, MethodILOffset:=112)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=20)> _
  Public Sub Test(ByVal strString As String)
    If strString Is Nothing Then
      Throw New System.ArgumentNullException("strString")
    End If
    Dim strTemp As String
    If strString.Contains("=") Then
      Dim strSplit As String() = strString.Split("="c)
      Dim strTemp2 As String = strSplit(0)
      strTemp = strString.Substring(strTemp2.Length)
      Debug.Print(strTemp)
    ElseIf strString.EndsWith(":") Then
      Contract.Assert(strString.Length >= 1)
      strString = strString.Substring(0, strString.Length - 1)
    End If
  End Sub
End Class

Public Class Wurz7
  '<ClousotRegressionTest()> _
  Public Shared Function f_GetFileFromPath(ByVal sPath As String, ByVal bPathorFile As Boolean) As String
    Dim nPos As Integer
    f_GetFileFromPath = sPath
    Dim length = Len(sPath)
    Contract.Assert(length = sPath.Length)
    For nPos = Len(sPath) - 1 To 1 Step -1
      Contract.Assert(nPos >= 1)
      Contract.Assert(nPos < sPath.Length)
      If Mid(sPath, nPos, 1) = "\" Then
        If bPathorFile Then
          f_GetFileFromPath = sPath.Substring(0, nPos)
        Else
          f_GetFileFromPath = sPath.Substring(nPos)
        End If
        Exit Function
      End If
      Contract.Assert(nPos < sPath.Length)
    Next
  End Function


End Class
Module Wurz8

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Array creation : ok", PrimaryILOffset:=28, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=36, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=36)> _
  <Extension()> Public Function ToByteArray(ByVal source As String, ByVal encoding As Text.Encoding) As Byte()
    Contract.Requires(Of ArgumentNullException)(encoding IsNot Nothing)
    '    If encoding Is Nothing Then
    '      Throw New ArgumentNullException("encoding")
    '    End If
    '    Contract.EndContractBlock()

    If source = "" Then Return New Byte() {}
    Return encoding.GetBytes(source)
  End Function

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="requires unproven: encoding IsNot Nothing", PrimaryILOffset:=7, MethodILOffset:=6)> _
  Sub Test(ByVal encoding As Text.Encoding)
    ToByteArray("", encoding)
  End Sub

End Module

Module Wurz9
  Public Class TestVBGetEnumerator
    Implements System.Collections.IEnumerable
    Private MyObjects As New Microsoft.VisualBasic.Collection

    <ClousotRegressionTest()> _
     <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="Possibly calling a method on a null reference 'this.MyObjects'", PrimaryILOffset:=6, MethodILOffset:=0)> _
     <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=17, MethodILOffset:=11)> _
     <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=1, MethodILOffset:=0)> _
    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
      Return MyObjects.GetEnumerator
    End Function
  End Class
End Module

Module Wurz10
  Friend NotInheritable Class MissingEnsuresInReflection

    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=563, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=563, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=469, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=469, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=10, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=30, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=50, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=70, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=90, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=110, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=130, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=150, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=170, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=190, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=210, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=230, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=250, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=270, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=290, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=310, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=330, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=337, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=348, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=359, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=370, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=391, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=402, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=413, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=424, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=435, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=455, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=533, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=547, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=641, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=563, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=566, MethodILOffset:=0)> _
     <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=577, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=588, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=599, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=610, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=621, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=469, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=472, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=483, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=494, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=505, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=516, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="requires unproven: obj IsNot Nothing", PrimaryILOffset:=7, MethodILOffset:=15)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=35)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=55)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=75)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=95)> _
    <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="requires unproven: obj IsNot Nothing", PrimaryILOffset:=7, MethodILOffset:=115)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=135)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=155)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=175)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=195)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=215)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=235)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=255)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=275)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=295)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=315)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=342)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=353)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=364)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=375)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=385)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=396)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=407)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=418)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=429)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=440)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=571)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=582)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=593)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=604)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=615)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=626)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=477)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=488)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=499)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=510)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=7, MethodILOffset:=521)> _
    Public Shared Sub main() 'CallTests()  
      TestNotNull(GetType(Object).DeclaringType)
      TestNotNull(GetType(Object).GetConstructors)
      TestNotNull(GetType(Object).GetDefaultMembers)
      TestNotNull(GetType(Object).GetEvents)
      TestNotNull(GetType(Object).GetFields)
      TestNotNull(GetType(Object).GetGenericArguments)
      TestNotNull(GetType(Object).GetInterfaces)
      TestNotNull(GetType(Object).GetMembers)
      TestNotNull(GetType(Object).GetMethods)
      TestNotNull(GetType(Object).GetNestedTypes)
      TestNotNull(GetType(Object).GetProperties)
      TestNotNull(GetType(Object).GetType)
      TestNotNull(GetType(Object).MakeArrayType)
      TestNotNull(GetType(Object).Module)
      TestNotNull(GetType(Object).ReflectedType)
      TestNotNull(GetType(Object).UnderlyingSystemType)
      Dim a As System.Reflection.Assembly = GetType(Object).Assembly
      TestNotNull(a.CodeBase)
      TestNotNull(a.EscapedCodeBase)
      TestNotNull(a.Evidence)
      TestNotNull(a.FullName)
      TestNotNull(Reflection.Assembly.GetCallingAssembly)
      TestNotNull(a.GetLoadedModules)
      TestNotNull(a.GetReferencedAssemblies)
      TestNotNull(a.ImageRuntimeVersion)
      TestNotNull(a.Location)
      TestNotNull(a.ManifestModule)
      For Each ev In GetType(System.Windows.Forms.Control).GetEvents
        TestNotNull(ev.EventHandlerType)
        TestNotNull(ev.GetAddMethod)
        TestNotNull(ev.GetOtherMethods)
        TestNotNull(ev.GetRemoveMethod)
        TestNotNull(ev.Module)
      Next
      For Each pr In GetType(System.Windows.Forms.Control).GetProperties
        TestNotNull(pr.GetAccessors)
        TestNotNull(pr.GetIndexParameters)
        TestNotNull(pr.GetOptionalCustomModifiers)
        TestNotNull(pr.GetRequiredCustomModifiers)
        TestNotNull(pr.Module)
        TestNotNull(pr.PropertyType)
      Next

    End Sub
    Private Shared Sub TestNotNull(ByVal obj As Object)
      Contract.Requires(obj IsNot Nothing)
    End Sub
  End Class

End Module


Module Wurz11
  Friend NotInheritable Class ContractError

    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=68, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=68, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=54, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=102, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=108, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=113, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=118, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=123, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=34, MethodILOffset:=127)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=39, MethodILOffset:=127)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=68, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="Possibly calling a method on a null reference 'param'", PrimaryILOffset:=71, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=79, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=23, MethodILOffset:=127)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="ensures is valid", PrimaryILOffset:=46, MethodILOffset:=127)> _
    Private Function GetMinParameters(ByVal MyMethod As System.Reflection.MethodBase) As Integer
      Contracts.Contract.Requires(MyMethod IsNot Nothing)
      Contracts.Contract.Ensures(Contracts.Contract.Result(Of Integer)() >= 0)
      Contracts.Contract.Ensures(Contracts.Contract.Result(Of Integer)() <= MyMethod.GetParameters.Length)
      Dim param As System.Reflection.ParameterInfo
      Dim lngCount As Integer
      lngCount = 0
      For Each param In MyMethod.GetParameters
        ' Warning here because we do not have quantified invariants yet
        If (Not param.IsOptional OrElse param.DefaultValue IsNot System.DBNull.Value) Then
          lngCount += 1
        End If
      Next
      If lngCount > MyMethod.GetParameters.Length Then
        lngCount = MyMethod.GetParameters.Length
      End If
      Return lngCount
    End Function


    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=37, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=58, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=79, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=100, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=121, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=126, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=147, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=152, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=173, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=194, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=295, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=300, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=305, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=316, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=321, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=326, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=331, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=11, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=27, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=48, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=69, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=90, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=111, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=137, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=163, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=184, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=205, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=221, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=237, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=253, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=269, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=285, MethodILOffset:=0)> _
    Sub AutoGenerated()
      Contracts.Contract.Assert(VisualBasicTests.My.Resources.Test IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Application IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Application.CommandLineArgs IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Application.Culture IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Application.Deployment IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Application.Info IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Application.Info.AssemblyName IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Application.Info.Version IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Application.Log IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Application.UICulture IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Computer IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Computer IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.Settings IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.User IsNot Nothing)
      Contracts.Contract.Assert(VisualBasicTests.My.WebServices IsNot Nothing)

      Dim s = My.Application.Info.Version.ToString()
      s = My.Application.Culture.NumberFormat.NumberGroupSeparator.ToString
    End Sub

  End Class
End Module


Class Wurz12
  Private ID As String
  Private Name As String

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=37, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=37, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=52, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=52, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=60, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=60, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=73, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=73, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=26, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=37, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=52, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=53, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=60, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=61, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=66, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=73, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=89, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=95, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=100, MethodILOffset:=0)> _
  Sub Example(ByVal MyImages As Wurz12(), ByVal i As Integer)
    Contract.Requires(MyImages IsNot Nothing)
    Contract.Requires(i >= 0)
    Contract.Requires(i < MyImages.Length)

    Contract.Assume(MyImages(i) IsNot Nothing)
    SomeProc(MyImages(i).ID, MyImages(i).Name)


    Dim MyImage As Wurz12 = MyImages(i)
    Contract.Assume(MyImage IsNot Nothing)
    SomeProc(MyImage.ID, MyImage.Name)

  End Sub

  Sub SomeProc(ByVal s1 As String, ByVal s2 As String)

  End Sub

End Class

Public Class Wurz13
  Private Shared MyBool As Boolean
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="ensures unproven: Contract.Result(Of Integer)() > 0 Or MyBool", PrimaryILOffset:=14, MethodILOffset:=20)> _
  Public Shared Function Test(ByVal ret As Integer) As Integer
    Contract.Ensures(Contract.Result(Of Integer)() > 0 Or MyBool)
    'Contract.Assume(ret > 0 Or MyBool)
    Return ret
  End Function
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="ensures unproven: Contract.Result(Of Integer)() > 0 OrElse MyBool", PrimaryILOffset:=19, MethodILOffset:=25)> _
  Public Shared Function Test2(ByVal ret As Integer) As Integer
    Contract.Ensures(Contract.Result(Of Integer)() > 0 OrElse MyBool)
    'Contract.Assume(ret > 0 OrElse MyBool)
    Return ret
  End Function

End Class

Public Class Wurz14
  Private MyBool As Boolean
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=9, MethodILOffset:=21)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="ensures unproven: Contract.Result(Of Integer)() > 0 Or MyBool", PrimaryILOffset:=15, MethodILOffset:=21)> _
  Public Function Test(ByVal ret As Integer) As Integer
    Contract.Ensures(Contract.Result(Of Integer)() > 0 Or MyBool)
    'Contract.Assume(ret > 0 Or MyBool)
    Return ret
  End Function
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=9, MethodILOffset:=26)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="ensures unproven: Contract.Result(Of Integer)() > 0 OrElse MyBool", PrimaryILOffset:=20, MethodILOffset:=26)> _
  Public Function Test2(ByVal ret As Integer) As Integer
    Contract.Ensures(Contract.Result(Of Integer)() > 0 OrElse MyBool)
    'Contract.Assume(ret > 0 OrElse MyBool)
    Return ret
  End Function

End Class


Public Class Wurz15
  Private Class cImage
    Friend Image As Drawing.Image
  End Class
  Private pic As Panel

  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=6, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=13, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as field receiver)", PrimaryILOffset:=30, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=36, MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="Possibly calling a method on a null reference 'iImage'", PrimaryILOffset:=42, MethodILOffset:=0)> _
  Private Sub Test(ByVal MyImage As cImage)
    'Contract.Requires(MyImage IsNot Nothing)
    Dim iImage As Drawing.Image
    iImage = Nothing
    If MyImage IsNot Nothing Then
      iImage = MyImage.Image
    End If
    Contract.Assume(pic IsNot Nothing)
    pic.BackgroundImage = iImage
    'Contract.Assume(pic.BackgroundImage IsNot Nothing)
    Debug.Print(CStr(iImage.HorizontalResolution))
  End Sub
End Class

Public Structure WurzStruct1
  Dim x As String

  Public Sub New(ByVal args As String())
    Contract.Requires(Contract.ForAll(args, Function(s As String) s IsNot Nothing))
  End Sub

  Public Sub New(ByVal x As Integer)
    Me.New(New String(x) {})
    Contract.Requires(x >= 0)
  End Sub

End Structure

Public Class ConfigForWurz16
  Public ReadOnly Property IsValid() As Boolean
    Get
      Return False
    End Get
  End Property
  Friend ReadOnly Property getException() As System.Exception
    Get
      Return New System.InvalidOperationException("Invalid")
    End Get
  End Property
End Class

Public NotInheritable Class Wurz16

  Private disposedValue As Boolean
  Private MyConfig As ConfigForWurz16
  Private MyRunning As Boolean
  Public Sub New()
    MyConfig = New ConfigForWurz16
  End Sub
  Public ReadOnly Property Config() As ConfigForWurz16
    Get
      Return MyConfig
    End Get
  End Property
  Public ReadOnly Property IsDisposed() As Boolean
    Get
      Return disposedValue
    End Get
  End Property
  Public ReadOnly Property Running() As Boolean
    Get
      Return MyRunning
    End Get
  End Property
  Function Getvalue() As Integer
    Return 5
  End Function

  <SuppressMessage("Microsoft.Contracts", "CC1057")> _
  Public Sub Test()
    If Not Config.IsValid Then
      Throw Config.getException()
    End If
    If IsDisposed Then
      Throw New InvalidOperationException("Is Disposed")
    End If
    If Running Then
      Throw New InvalidOperationException("Already running")
    End If
    Contracts.Contract.EndContractBlock()
    Try
      MyRunning = True
      Dim obj = New Object
      If obj Is Nothing Then
        Dim x = Getvalue()
        Dim y = Me.Config
        Dim z = Me.Running

      Else
        Dim x = Getvalue()
        Dim y = Me.Config
        Dim z = Me.Running

      End If
    Finally
      MyRunning = False
    End Try
  End Sub

  ' Check extraction of legacy if then else
#If CLOUSOT2 Then
  <ClousotRegressionTest()> _
  <RegressionOutcome(Outcome:=ProofOutcome.True,Message:="valid non-null reference (as receiver)",PrimaryILOffset:=1,MethodILOffset:=0)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top,Message:="requires unproven: Not(Not Config.IsValid)",PrimaryILOffset:=6,MethodILOffset:=1)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top,Message:="requires unproven: Not(IsDisposed)",PrimaryILOffset:=26,MethodILOffset:=1)> _
  <RegressionOutcome(Outcome:=ProofOutcome.Top,Message:="requires unproven: Not(Running)",PrimaryILOffset:=45,MethodILOffset:=1)> _
  Public Sub CallTest()
    Test()
  End Sub
#Else
  <ClousotRegressionTest()> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=1, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="requires unproven: Not(Not Config.IsValid)", PrimaryILOffset:=24, MethodILOffset:=1)> _
<RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="requires unproven: Not(IsDisposed)", PrimaryILOffset:=43, MethodILOffset:=1)> _
<RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="requires unproven: Not(Running)", PrimaryILOffset:=62, MethodILOffset:=1)> _
  Public Sub CallTest()
    Test()
  End Sub
#End If

End Class

Public Class Wurz17

  ' Private WithEvents X As New Object
  Private Witness As String()

  Private Sub New(ByVal args As String())
    'malformed contract error on this line when withEvents is present
    Contract.Requires(Contract.ForAll(args, Function(s As String) s IsNot Nothing))
    'Contract.Ensures(Contract.ForAll(Witness, Function(s As String) Contract.Exists(args, Function(a) a.Equals(s))))
  End Sub

End Class
