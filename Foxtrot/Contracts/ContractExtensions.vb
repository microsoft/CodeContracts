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

'
'  Include this file in your project if you want to use
'  ContractArgumentValidator methods or ContractAbbreviator methods
'

''' <summary>
''' Enables factoring legacy if-then-throw into separate methods for reuse and full control over
''' thrown exception and arguments
''' </summary>
<Global.System.AttributeUsage(Global.System.AttributeTargets.Method, AllowMultiple:=False)> _
<Global.System.Diagnostics.Conditional("CONTRACTS_FULL")> _
NotInheritable Class ContractArgumentValidatorAttribute
    Inherits Global.System.Attribute
End Class

''' <summary>
''' Enables writing abbreviations for contracts that get copied to other methods
''' </summary>
<Global.System.AttributeUsage(Global.System.AttributeTargets.Method, AllowMultiple:=False)> _
<Global.System.Diagnostics.Conditional("CONTRACTS_FULL")> _
NotInheritable Class ContractAbbreviatorAttribute
    Inherits Global.System.Attribute
End Class

''' <summary>
''' Allows setting contract and tool options at assembly, type, or method granularity.
''' </summary>
<Global.System.AttributeUsage(Global.System.AttributeTargets.All, AllowMultiple:=True, Inherited:=False)> _
<Global.System.Diagnostics.Conditional("CONTRACTS_FULL")> _
NotInheritable Class ContractOptionAttribute
    Inherits Global.System.Attribute

    <Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="category")> _
    <Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="setting")> _
    <Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="toggle")> _
    Public Sub New(ByVal category As String, ByVal setting As String, ByVal toggle As Boolean)
    End Sub

    <Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="category")> _
    <Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="setting")> _
    <Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId:="value")> _
    Public Sub New(ByVal category As String, ByVal setting As String, ByVal value As String)
    End Sub

End Class
