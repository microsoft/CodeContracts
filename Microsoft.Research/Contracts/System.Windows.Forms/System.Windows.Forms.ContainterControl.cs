// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
  // Summary:
  //     Provides focus-management functionality for controls that can function as
  //     a container for other controls.
 // [ClassInterface(ClassInterfaceType.AutoDispatch)]
 // [ComVisible(true)]
  public class ContainerControl //: ScrollableControl, IContainerControl
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ContainerControl class.
    //public ContainerControl();

    // Summary:
    //     Gets or sets the active control on the container control.
    //
    // Returns:
    //     The System.Windows.Forms.Control that is currently active on the System.Windows.Forms.ContainerControl.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Windows.Forms.Control assigned could not be activated.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Browsable(false)]
    //public Control ActiveControl { get; set; }
    //
    // Summary:
    //     Gets or sets the dimensions that the control was designed to.
    //
    // Returns:
    //     A System.Drawing.SizeF containing the dots per inch (DPI) or System.Drawing.Font
    //     size that the control was designed to.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The width or height of the System.Drawing.SizeF value is less than 0 when
    //     setting this value.
   // [Localizable(true)]
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
   // [Browsable(false)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public SizeF AutoScaleDimensions { get; set; }
    //
    // Summary:
    //     Gets the scaling factor between the current and design-time automatic scaling
    //     dimensions.
    //
    // Returns:
    //     A System.Drawing.SizeF containing the scaling ratio between the current and
    //     design-time scaling automatic scaling dimensions.
    //protected SizeF AutoScaleFactor { get; }
    //
    // Summary:
    //     Gets or sets the automatic scaling mode of the control.
    //
    // Returns:
    //     An System.Windows.Forms.AutoScaleMode that represents the current scaling
    //     mode. The default is System.Windows.Forms.AutoScaleMode.None.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     An System.Windows.Forms.AutoScaleMode value that is not valid was used to
    //     set this property.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
   // [Browsable(false)]
    //public AutoScaleMode AutoScaleMode { get; set; }
    //
    // Summary:
    //     Gets or sets a value that indicates whether controls in this container will
    //     be automatically validated when the focus changes.
    //
    // Returns:
    //     An System.Windows.Forms.AutoValidate enumerated value that indicates whether
    //     contained controls are implicitly validated on focus change. The default
    //     is System.Windows.Forms.AutoValidate.Inherit.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     A System.Windows.Forms.AutoValidate value that is not valid was used to set
    //     this property.
   // [Browsable(false)]
   // [EditorBrowsable(EditorBrowsableState.Never)]
    //public virtual AutoValidate AutoValidate { get; set; }
    //
    //
    // Returns:
    //     A System.Windows.Forms.BindingContext for the control.
   // [Browsable(false)]
    //public override BindingContext BindingContext { get; set; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Windows.Forms.Control.ImeMode
    //     property can be set to an active value, to enable IME support.
    //
    // Returns:
    //     false in all cases.
    //// protected override bool CanEnableIme { get; }
    //
    //// protected override CreateParams CreateParams { get; }
    //
    // Summary:
    //     Gets the current run-time dimensions of the screen.
    //
    // Returns:
    //     A System.Drawing.SizeF containing the current dots per inch (DPI) or System.Drawing.Font
    //     size of the screen.
    //
    // Exceptions:
    //   System.ComponentModel.Win32Exception:
    //     A Win32 device context could not be created for the current screen.
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
   // [Browsable(false)]
    //public SizeF CurrentAutoScaleDimensions { get; }
    //
    // Summary:
    //     Gets the form that the container control is assigned to.
    //
    // Returns:
    //     The System.Windows.Forms.Form that the container control is assigned to.
    //     This property will return null if the control is hosted inside of Internet
    //     Explorer or in another hosting context where there is no parent form.
   // [Browsable(false)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public Form ParentForm { get; }

    // Summary:
    //     Occurs when the System.Windows.Forms.ContainerControl.AutoValidate property
    //     changes.
   // [Browsable(false)]
   // [EditorBrowsable(EditorBrowsableState.Never)]
    //public event EventHandler AutoValidateChanged;

    //
    // Parameters:
    //   displayScrollbars:
    //     true to show the scroll bars; otherwise, false.
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void AdjustFormScrollbars(bool displayScrollbars);
    //
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    // protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ContainerControl.AutoValidateChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected virtual void OnAutoValidateChanged(EventArgs e);
    //
    // protected override void OnCreateControl();
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.FontChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void OnFontChanged(EventArgs e);
    //
    // protected override void OnLayout(LayoutEventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnParentChanged(EventArgs e);
    //
    // Summary:
    //     Performs scaling of the container control and its children.
    //public void PerformAutoScale();
    //
    //
    // Parameters:
    //   msg:
    //     A System.Windows.Forms.Message, passed by reference, that represents the
    //     window message to process.
    //
    //   keyData:
    //     One of the System.Windows.Forms.Keys values that represents the key to process.
    //
    // Returns:
    //     true if the character was processed by the control; otherwise, false.
    // protected override bool ProcessCmdKey(ref Message msg, Keys keyData);
    //
    //
    // Parameters:
    //   charCode:
    //     The character to process.
    //
    // Returns:
    //     true if the character was processed by the control; otherwise, false.
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override bool ProcessDialogChar(char charCode);
    //
    //
    // Parameters:
    //   keyData:
    //     One of the System.Windows.Forms.Keys values that represents the key to process.
    //
    // Returns:
    //     true if the key was processed by the control; otherwise, false.
    // protected override bool ProcessDialogKey(Keys keyData);
    //
    //
    // Parameters:
    //   charCode:
    //     The character to process.
    //
    // Returns:
    //     true if the character was processed as a mnemonic by the control; otherwise,
    //     false.
    //protected internal override bool ProcessMnemonic(char charCode);
    //
    // Summary:
    //     Selects the next available control and makes it the active control.
    //
    // Parameters:
    //   forward:
    //     true to cycle forward through the controls in the System.Windows.Forms.ContainerControl;
    //     otherwise, false.
    //
    // Returns:
    //     true if a control is selected; otherwise, false.
    //protected virtual bool ProcessTabKey(bool forward);
    //
    //
    // Parameters:
    //   directed:
    //     true to specify the direction of the control to select; otherwise, false.
    //
    //   forward:
    //     true to move forward in the tab order; false to move backward in the tab
    //     order.
    // protected override void Select(bool directed, bool forward);
    //
    // Summary:
    //     When overridden by a derived class, updates which button is the default button.
    //protected virtual void UpdateDefaultButton();
    //
    // Summary:
    //     Verifies the value of the control losing focus by causing the System.Windows.Forms.Control.Validating
    //     and System.Windows.Forms.Control.Validated events to occur, in that order.
    //
    // Returns:
    //     true if validation is successful; otherwise, false. If called from the System.Windows.Forms.Control.Validating
    //     or System.Windows.Forms.Control.Validated event handlers, this method will
    //     always return false.
    //public bool Validate();
    //
    // Summary:
    //     Verifies the value of the control that is losing focus; conditionally dependent
    //     on whether automatic validation is turned on.
    //
    // Parameters:
    //   checkAutoValidate:
    //     If true, the value of the System.Windows.Forms.ContainerControl.AutoValidate
    //     property is used to determine if validation should be performed; if false,
    //     validation is unconditionally performed.
    //
    // Returns:
    //     true if validation is successful; otherwise, false. If called from the System.Windows.Forms.Control.Validating
    //     or System.Windows.Forms.Control.Validated event handlers, this method will
    //     always return false.
    //public bool Validate(bool checkAutoValidate);
    //
    // Summary:
    //     Causes all of the child controls within a control that support validation
    //     to validate their data.
    //
    // Returns:
    //     true if all of the children validated successfully; otherwise, false. If
    //     called from the System.Windows.Forms.Control.Validating or System.Windows.Forms.Control.Validated
    //     event handlers, this method will always return false.
   // [Browsable(false)]
   // [EditorBrowsable(EditorBrowsableState.Never)]
    //public virtual bool ValidateChildren();
    ////
    // Summary:
    //     Causes all of the child controls within a control that support validation
    //     to validate their data.
    //
    // Parameters:
    //   validationConstraints:
    //     Tells System.Windows.Forms.ContainerControl.ValidateChildren(System.Windows.Forms.ValidationConstraints)
    //     how deeply to descend the control hierarchy when validating the control's
    //     children.
    //
    // Returns:
    //     true if all of the children validated successfully; otherwise, false. If
    //     called from the System.Windows.Forms.Control.Validating or System.Windows.Forms.Control.Validated
    //     event handlers, this method will always return false.
   // [Browsable(false)]
   // [EditorBrowsable(EditorBrowsableState.Never)]
    //public virtual bool ValidateChildren(ValidationConstraints validationConstraints);
    //
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void WndProc(ref Message m);
  }
}
