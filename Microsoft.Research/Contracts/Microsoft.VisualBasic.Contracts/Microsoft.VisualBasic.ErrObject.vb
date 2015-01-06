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

Imports System
Imports System.Diagnostics.Contracts

Namespace Microsoft.VisualBasic

  '' Summary:
  ''     The ErrObject module contains properties and procedures used to identify
  ''     and handle run-time errors Imports the Err object.
  Public NotInheritable Class ErrObject

    Friend Sub New()
    End Sub

    '' Summary:
    ''     Returns or sets a String expression containing a descriptive string associated
    ''     with an error. Read/write.
    ''
    '' Returns:
    ''     Returns or sets a String expression containing a descriptive string associated
    ''     with an error. Read/write.
    ''Public string Description  get set End
    ''
    '' Summary:
    ''     Returns an integer indicating the line number of the last executed statement.
    ''     Read-only.
    ''
    '' Returns:
    ''     Returns an integer indicating the line number of the last executed statement.
    ''     Read-only.
    Public ReadOnly Property Erl() As Integer

      Get

        Contract.Ensures(Contract.Result(Of Integer)() >= 0)

        Return Nothing
      End Get
    End Property


    ''
    '' Summary:
    ''     Returns or sets an Integer containing the context ID for a topic in a Help
    ''     file. Read/write.
    ''
    '' Returns:
    ''     Returns or sets an Integer containing the context ID for a topic in a Help
    ''     file. Read/write.
    ''Public int HelpContext  get set End
    ''
    '' Summary:
    ''     Returns or sets a String expression containing the fully qualified path to
    ''     a Help file. Read/write.
    ''
    '' Returns:
    ''     Returns or sets a String expression containing the fully qualified path to
    ''     a Help file. Read/write.
    ''Public string HelpFile  get set End
    ''
    '' Summary:
    ''     Returns a system error code produced by a call to a dynamic-link library
    ''     (DLL). Read-only.
    ''
    '' Returns:
    ''     Returns a system error code produced by a call to a dynamic-link library
    ''     (DLL). Read-only.
    ''Public int LastDllError  get End
    ''
    '' Summary:
    ''     Returns or sets a numeric value specifying an error. Read/write.
    ''
    '' Returns:
    ''     Returns or sets a numeric value specifying an error. Read/write.
    ''Public int Number  get set End
    ''
    '' Summary:
    ''     Returns or sets a String expression specifying the name of the object or
    ''     application that originally generated the error. Read/write.
    ''
    '' Returns:
    ''     Returns or sets a String expression specifying the name of the object or
    ''     application that originally generated the error. Read/write.
    ''Public string Source  get set End
#If Not SILVERLIGHT Then
    '' Summary:
    ''     Clears all property settings of the Err object.
    ''[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    ''Public void Clear()
    ''
    '' Summary:
    ''     Returns the exception representing the error that occurred.
    ''
    '' Returns:
    ''     Returns the exception representing the error that occurred.
    ''Public Exception GetException()
    ''
    '' Summary:
    ''     Generates a run-time error can be used instead of the Error statement.
    ''
    '' Parameters:
    ''   Number:
    ''     Required. Long integer that identifies the nature of the error. Visual Basic
    ''     errors are in the range 0–65535 the range 0–512 is reserved for system errors
    ''     the range 513–65535 is available for user-defined errors. When setting the
    ''     Number property to your own error code in a class module, you add your error
    ''     code number to the vbObjectError constant. For example, to generate the error
    ''     number 513, assign vbObjectError + 513 to the Number property.
    ''
    ''   Source:
    ''     Optional. String expression naming the object or application that generated
    ''     the error. When setting this property for an object, use the form project.class.
    ''     If Source is not specified, the process ID of the current Visual Basic project
    ''     is used.
    ''
    ''   Description:
    ''     Optional. String expression describing the error. If unspecified, the value
    ''     in the Number property is examined. If it can be mapped to a Visual Basic
    ''     run-time error code, the string that would be returned by the Error function
    ''     is used as the Description property. If there is no Visual Basic error corresponding
    ''     to the Number property, the "Application-defined or object-defined error"
    ''     message is used.
    ''
    ''   HelpFile:
    ''     Optional. The fully qualified path to the Help file in which help on this
    ''     error can be found. If unspecified, Visual Basic uses the fully qualified
    ''     drive, path, and file name of the Visual Basic Help file.
    ''
    ''   HelpContext:
    ''     Optional. The context ID identifying a topic within HelpFile that provides
    ''     help for the error. If omitted, the Visual Basic Help-file context ID for
    ''     the error corresponding to the Number property is used, if it exists.
    Public Sub Raise(ByVal Number As Integer, ByVal Source As Object, ByVal Description As Object, ByVal HelpFile As Object, ByVal HelpContext As Object)

      Contract.Requires(Number >= 0)

    End Sub
#End If
  End Class
End Namespace
