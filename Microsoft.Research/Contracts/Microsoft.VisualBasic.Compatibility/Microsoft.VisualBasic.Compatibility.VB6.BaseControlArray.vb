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
Imports System.Collections
Imports System.ComponentModel
Imports System.Diagnostics.Contracts

Namespace Microsoft.VisualBasic.Compatibility.VB6

  '' Summary:
  ''     Implements the basic functionality common to control arrays in applications
  ''     upgraded from Visual Basic 6.0.
  Public Class BaseControlArray '': Component, ISupportInitialize
    '' Summary:
    ''     Stores the container for a control array.
    ''protected IContainer components;
    ''
    '' Summary:
    ''     Stores values to track whether a control in a control array was created at
    ''     design time or at run time.
    ''protected Hashtable controlAddedAtDesignTime;
    ''
    '' Summary:
    ''''     Stores values that represent the controls in a control array.
    ''protected Hashtable controls;
    ''
    '' Summary:
    ''     Stores a value indicating whether the initialization code for a control array's
    ''     container has finished executing.
    ''protected bool fIsEndInitCalled;
    ''
    '' Summary:
    ''     Stores the indices for a control array.
    ''protected Hashtable indices;

    '' Summary:
    ''     Initializes a new instance of the Microsoft.VisualBasic.Compatibility.VB6.BaseControlArray
    ''     class.
    ''protected BaseControlArray();
    ''
    '' Summary:
    ''     Initializes a new instance of the Microsoft.VisualBasic.Compatibility.VB6.BaseControlArray
    ''     class, optionally specifying a container.
    ''
    '' Parameters:
    ''   Container:
    ''     The System.ComponentModel.IContainer where the control array will be hosted.
    Protected Sub New(ByVal Container As IContainer)

      Contract.Requires(Container IsNot Nothing)

    End Sub

    '' Summary:
    ''     Gets a value that indicates whether a control is a member of a control array.
    ''
    '' Parameters:
    ''   target:
    ''     The System.Windows.Forms.Control to test.
    ''
    '' Returns:
    ''     true if the control is a member of the control array; otherwise, false.
    Protected Function BaseCanExtend(ByVal target As Object) As Boolean

      Contract.Requires(target IsNot Nothing)

      Return Nothing

    End Function
    ''
    '' Summary:
    ''     Gets the index of a control in a control array.
    ''
    '' Parameters:
    ''   ctl:
    ''     The System.Windows.Forms.Control for which you want to retrieve the index.
    ''
    '' Returns:
    ''     A Short integer representing the index of the control in the control array.
    Protected Function BaseGetIndex(ByVal ctl As Object) As Short

      Contract.Requires(ctl IsNot Nothing)

      Return Nothing
    End Function

    ''
    '' Summary:
    ''     Gets the control for a specified index in a control array.
    ''
    '' Parameters:
    ''   Index:
    ''     An integer that represents the index of the control in the control array.
    ''
    '' Returns:
    ''     The System.Windows.Forms.Control at the specified Index.
    ''protected object BaseGetItem(short Index);
    ''
    '' Summary:
    ''     Not supported in the Microsoft.VisualBasic.Compatibility.VB6.BaseControlArray
    ''     class.
    ''
    '' Parameters:
    ''   o:
    ''     A System.Windows.Forms.Control.
    ''protected void BaseResetIndex(object o);
    ''
    '' Summary:
    ''     Sets the index for a control in a control array.
    ''
    '' Parameters:
    ''   ctl:
    ''     The System.Windows.Forms.Control for which you want to set the index.
    ''
    ''   Index:
    ''     A Short integer that represents the index for the control.
    ''
    ''   fIsDynamic:
    ''     Optional. A Boolean specifying whether the control was created at design
    ''     time (false) or at run time (true).
    Protected Sub BaseSetIndex(ByVal ctl As Object, ByVal Index As Short, ByVal fIsDynamic As Boolean)

      Contract.Requires(ctl IsNot Nothing)
      Contract.Requires(Index >= 0)

    End Sub
    ''
    '' Summary:
    ''     Returns a value that indicates whether a control is a member of a control
    ''     array.
    ''
    '' Parameters:
    ''   o:
    ''     A System.Windows.Forms.Control.
    ''
    '' Returns:
    ''     true if the control is a member of the control array; otherwise, false.
    ''protected bool BaseShouldSerializeIndex(object o);
    ''
    '' Summary:
    ''     Returns the number of controls in a control array.
    ''
    '' Returns:
    ''     A Short that contains the number of controls.
    <Pure()> _
    Public Function Count() As Short

      Contract.Ensures(Contract.Result(Of Short)() >= 0)

      Return Nothing

    End Function
    ''
    '' Summary:
    ''     Releases the unmanaged resources that are used by a control in a control
    ''     array and optionally releases the managed resources.
    ''
    '' Parameters:
    ''   disposing:
    ''     true to release both managed and unmanaged resources; false to release only
    ''     unmanaged resources.
    ''protected override void Dispose(bool disposing);
    ''
    '' Summary:
    ''     Returns the type of a control in a control array.
    ''
    '' Returns:
    ''     This method must be overridden.The Microsoft.VisualBasic.Compatibility.VB6.BaseControlArray
    ''     class is the base class for all control arrays that are used in applications
    ''     upgraded from Visual Basic 6.0. Because this class is not typically used
    ''     to create an instance of the class, this Protected method is usually not
    ''     called directly but is instead called by a derived class.Functions and objects
    ''     in the Microsoft.VisualBasic.Compatibility.VB6 namespace are provided for
    ''     use by the tools for upgrading from Visual Basic 6.0 to Visual Basic 2008.
    ''     In most cases, these functions and objects duplicate functionality that you
    ''     can find in other namespaces in the .NET Framework. They are necessary only
    ''     when the Visual Basic 6.0 code model differs significantly from the .NET
    ''     Framework implementation.
    ''protected abstract Type GetControlInstanceType();
    ''
    '' Summary:
    ''     Returns a reference to an enumerator object, which is used to iterate over
    ''     a control array.
    ''
    '' Returns:
    ''     A System.Collections.IEnumerator object.
    ''public IEnumerator GetEnumerator();
    ''
    '' Summary:
    ''     Adds event handlers for a control in a control array.
    ''
    '' Parameters:
    ''   o:
    ''     A System.Windows.Forms.Control.
    ''protected abstract void HookUpControlEvents(object o);
    ''
    '' Summary:
    ''     Returns a Short that contains the smallest available subscript for a control
    ''     array.
    ''
    '' Returns:
    ''     A Short that contains the lower bounds of a control array.
    Public Function LBound() As Short

      Contract.Ensures(Contract.Result(Of Short)() >= 0)

      Return Nothing

    End Function
    ''
    '' Summary:
    ''     Creates a new element in a control array.
    ''
    '' Parameters:
    ''   Index:
    ''     A Short that represents the index of the new control.
    Public Sub Load(ByVal Index As Short)

      Contract.Requires(Index >= 0)

    End Sub
    ''
    '' Summary:
    ''     Returns a Short that contains the largest available subscript for a control
    ''     array.
    ''
    '' Returns:
    ''     A Short that contains the upper bounds of a control array.
    Public Function UBound() As Short

      Contract.Ensures(Contract.Result(Of Short)() >= -1)

      Return Nothing
    End Function

    ''
    '' Summary:
    ''     Removes a control from a control array.
    ''
    '' Parameters:
    ''   Index:
    ''     A Short that represents the index of the control to remove.
    Public Sub Unload(ByVal Index As Short)

      Contract.Requires(Index >= 0)

    End Sub
  End Class
End Namespace
