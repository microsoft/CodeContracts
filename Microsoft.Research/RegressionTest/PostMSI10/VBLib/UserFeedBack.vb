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
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.CompilerServices

Namespace Strilanc

  Public Module M
    Public Sub R(ByVal arg As Double)
      Contract.Requires(Not Double.IsNaN(arg))
    End Sub
    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=9, MethodILOffset:=9)> _
    Public Sub S()
      Call R(0)
    End Sub


  End Module

  Public Module M2
    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=26)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=47)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=36, MethodILOffset:=47)> _
    Public Function TryGetTail(ByVal value As String, ByVal divider As String) As String
      Contract.Requires(value IsNot Nothing)
      Contract.Requires(divider IsNot Nothing)
      Dim p = value.IndexOf(divider)
      If p = -1 Then Return Nothing
      Return value.Substring(p + divider.Length)
    End Function
  End Module

  <ContractClass(GetType(A(Of )))> _
  Public Interface I(Of In T)
    Sub R(ByVal o As T)
  End Interface

  <ContractClassFor(GetType(I(Of )))>
  Public MustInherit Class A(Of T)
    Implements I(Of T)
    Public Sub R(ByVal o As T) Implements I(Of T).R
      Contract.Requires(o IsNot Nothing)
    End Sub
  End Class

  Public MustInherit Class B(Of T)
    Implements I(Of T)
    Public MustOverride Sub R(ByVal o As T) Implements I(Of T).R
  End Class
  Public Class C(Of T)
    Inherits B(Of List(Of T))

    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="assert unproven", PrimaryILOffset:=7, MethodILOffset:=0)> _
    Public Overrides Sub R(ByVal o As List(Of T))
      '[implicit o-non-null requirement inherited from B<T> which inherited it from I<T>]
      Contract.Assert(o IsNot Nothing) 'verifier fails to prove this assert due to missing specialization
    End Sub
  End Class

  Module MByRefOut
    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=1, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=62, MethodILOffset:=0)> _
    Sub S()
      Contract.Assert(True) ' make sure we reach this point
      Dim d = New Dictionary(Of String, Object)()
      d("") = New Object()
      Dim o As Object = Nothing
      d.TryGetValue("", o)
      Contract.Assume(o IsNot Nothing)
      Contract.Assert(True) ' make sure we reach this point
    End Sub

    <ClousotRegressionTest()> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=1, MethodILOffset:=0)> _
    <RegressionOutcome(Outcome:=ProofOutcome.True, Message:="assert is valid", PrimaryILOffset:=57, MethodILOffset:=0)> _
    Sub S2()
      Contract.Assert(True) ' make sure we reach this point
      Dim d = New Dictionary(Of String, String)()
      d("") = "target"
      Dim o As String = Nothing
      d.TryGetValue("", o)
      Contract.Assume(o IsNot Nothing)
      Contract.Assert(True) ' make sure we reach this point
    End Sub

  End Module


End Namespace

Namespace Allen
  Class TestLinq
    Sub Test(Of T)(ByVal array As T(,), ByVal sortColumn As Integer)
      Contract.Requires(array IsNot Nothing)
      Dim sorted = (From fragment In array).ToArray
      Contract.Assert(sorted IsNot Nothing) '<-- error here   
    End Sub
  End Class


End Namespace

Namespace BrianMAnderson
  Public Class Class1

    Private propChanged As INotifyPropertyChanged

    Public Sub New(ByVal num As Integer)
      Contract.Requires(num > 0)
    End Sub

  End Class




#If HANDLEVBTRYCATCHCTOR Then
  Public Class Class2

    Private WithEvents propChanged As INotifyPropertyChanged

    Public Sub New(ByVal num As Integer)
      Contract.Requires(num > 0)
    End Sub

  End Class
#End If




#If HANDLEVBTRYCATCHCTOR Then
  Public Class Class3

    Private propChanged As INotifyPropertyChanged
    Public Event myEvent()

    Public Sub New(ByVal num As Integer)
      Contract.Requires(num > 0)
    End Sub

  End Class
#End If

End Namespace

Namespace ShiranPuri
  Public Module PixelCounter
    Public Sub Test()
      Dim bmp = New Bitmap(10, 10)
      Dim pixels = bmp.GetPixels()
      Console.WriteLine(pixels.Count())
    End Sub
    <Extension()>
    Public Function GetPixels(ByVal image As Bitmap) As IEnumerable(Of Point)
      Contract.Requires(Of ArgumentNullException)(image IsNot Nothing, "image")
      Contract.Ensures(Contract.Result(Of IEnumerable(Of Point))() IsNot Nothing)

      Dim columns = Enumerable.Range(0, image.Width)
      Dim rows = Enumerable.Range(0, image.Height)
      Dim pixels = From c In columns
             From r In rows
             Select New Point(c, r)

      Return pixels
    End Function
  End Module
End Namespace
