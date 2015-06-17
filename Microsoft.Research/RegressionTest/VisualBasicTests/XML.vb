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

Public Class FilterCompilerGenerated


#If Not CLOUSOT2 Then
    <ClousotRegressionTest()> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Array creation : ok", PrimaryILOffset:=300, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=318, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=318, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=327, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=327, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=344, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=344, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=353, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=353, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=370, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=370, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=19, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=33, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=47, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=61, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=75, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=112, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=134, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=141, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=163, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=170, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=176, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=203, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=225, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=232, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=254, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=261, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=267, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=277, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=282, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=385, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=293, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=318, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=327, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="Possibly calling a method on a null reference 'customer'", PrimaryILOffset:=334, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=344, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=353, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=360, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=370, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=5)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=19)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=33)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=47)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=61)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=75)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=84)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=93)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=107)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=37, MethodILOffset:=107)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=120)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=149)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=184)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=198)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=37, MethodILOffset:=198)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=211)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=240)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=373)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=400, MethodILOffset:=0)> _
    Public Sub TestXML()
#Else
    <ClousotRegressionTest()> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Array creation : ok", PrimaryILOffset:=300, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=318, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=318, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=327, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=327, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=344, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=344, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=353, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=353, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Lower bound access ok", PrimaryILOffset:=370, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="Upper bound access ok", PrimaryILOffset:=370, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=19, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=33, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=47, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=61, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=75, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=112, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=134, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=141, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=163, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=170, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=176, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=203, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=225, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=232, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=254, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=261, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=267, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=277, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=282, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=385, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=293, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=318, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=327, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.Top, Message:="Possibly calling a method on a null reference 'customer'", PrimaryILOffset:=334, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=344, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=353, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=360, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as array)", PrimaryILOffset:=370, MethodILOffset:=0)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=5)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=19)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=33)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=47)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=61)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=75)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=84)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=93)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=107)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=37, MethodILOffset:=107)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=120)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=149)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=184)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=198)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=37, MethodILOffset:=198)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=211)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=19, MethodILOffset:=240)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="requires is valid", PrimaryILOffset:=13, MethodILOffset:=373)> _
<RegressionOutcome(Outcome:=ProofOutcome.True, Message:="valid non-null reference (as receiver)", PrimaryILOffset:=400, MethodILOffset:=406)> _
    Public Sub TestXML()
#End If
        Dim customers = <customers>
                            <customer id="1">
                                <firstname>Jim</firstname>
                                <lastname>Fiorato</lastname>
                            </customer>
                            <customer id="2">
                                <firstname>Mister</firstname>
                                <lastname>Pickles</lastname>
                            </customer>
                        </customers>

        For Each customer In customers.<customer>

            Console.WriteLine(customer.@id & " - " & customer.<firstname>.Value & "  " & customer.<lastname>.Value)

        Next
        Console.ReadLine()
    End Sub

End Class