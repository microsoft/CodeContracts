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
using System.Drawing.Design;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents a small rectangular pop-up window that displays a brief description
  //     of a control's purpose when the user rests the pointer on the control.
 //[DefaultEvent("Popup")]
 //[ProvideProperty("ToolTip", typeof(Control))]
 //[ToolboxItemFilter("System.Windows.Forms")]
  public class ToolTip // : Component, IExtenderProvider
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolTip without a
    //     specified container.
    //public ToolTip();
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolTip class with
    //     a specified container.
    //
    // Parameters:
    //   cont:
    //     An System.ComponentModel.IContainer that represents the container of the
    //     System.Windows.Forms.ToolTip.
    public ToolTip(IContainer cont)
    {
      Contract.Requires(cont != null);

    }

    // Summary:
    //     Gets or sets a value indicating whether the ToolTip is currently active.
    //
    // Returns:
    //     true if the ToolTip is currently active; otherwise, false. The default is
    //     true.
   //[DefaultValue(true)]
    //public bool Active { get; set; }
    //
    // Summary:
    //     Gets or sets the automatic delay for the ToolTip.
    //
    // Returns:
    //     The automatic delay, in milliseconds. The default is 500.
   //[DefaultValue(500)]
   //[RefreshProperties(RefreshProperties.All)]
    public int AutomaticDelay
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }

      set
      {
        Contract.Requires(value >= 0);
      }
    }


    //
    // Summary:
    //     Gets or sets the period of time the ToolTip remains visible if the pointer
    //     is stationary on a control with specified ToolTip text.
    //
    // Returns:
    //     The period of time, in milliseconds, that the System.Windows.Forms.ToolTip
    //     remains visible when the pointer is stationary on a control. The default
    //     value is 5000.
   //[RefreshProperties(RefreshProperties.All)]
    public int AutoPopDelay
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }

      set
      {
        Contract.Requires(value >= 0);
      }
    }
    //
    // Summary:
    //     Gets or sets the background color for the ToolTip.
    //
    // Returns:
    //     The background System.Drawing.Color.
   //[DefaultValue(typeof(Color), "Info")]
    //public Color BackColor { get; set; }
    //
    // Summary:
    //     Gets the creation parameters for the ToolTip window.
    //
    // Returns:
    //     A System.Windows.Forms.CreateParams containing the information needed to
    //     create the ToolTip.
    //protected virtual CreateParams CreateParams { get; }
    //
    // Summary:
    //     Gets or sets the foreground color for the ToolTip.
    //
    // Returns:
    //     The foreground System.Drawing.Color.
   //[DefaultValue(typeof(Color), "InfoText")]
    public Color ForeColor
    {
      get
      {
        Contract.Ensures(!Contract.Result<Color>().IsEmpty);

        return default(Color);
      }
      set
      {
        Contract.Requires(!value.IsEmpty);

      }
    }
    //
    // Summary:
    //     Gets or sets the time that passes before the ToolTip appears.
    //
    // Returns:
    //     The period of time, in milliseconds, that the pointer must remain stationary
    //     on a control before the ToolTip window is displayed.
   //[RefreshProperties(RefreshProperties.All)]
    public int InitialDelay
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }

      set
      {
        Contract.Requires(value >= 0);
      }
    }
    //
    // Summary:
    //     Gets or sets a value indicating whether the ToolTip should use a balloon
    //     window.
    //
    // Returns:
    //     true if a balloon window should be used; otherwise, false if a standard rectangular
    //     window should be used. The default is false.
   //[DefaultValue(false)]
    //public bool IsBalloon { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the ToolTip is drawn by the operating
    //     system or by code that you provide.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolTip is drawn by code that you provide;
    //     false if the System.Windows.Forms.ToolTip is drawn by the operating system.
    //     The default is false.
   //[DefaultValue(false)]
    //public bool OwnerDraw { get; set; }
    //
    // Summary:
    //     Gets or sets the length of time that must transpire before subsequent ToolTip
    //     windows appear as the pointer moves from one control to another.
    //
    // Returns:
    //     The length of time, in milliseconds, that it takes subsequent ToolTip windows
    //     to appear.
   //[RefreshProperties(RefreshProperties.All)]
    public int ReshowDelay
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }

      set
      {
        Contract.Requires(value >= 0);
      }
    }
    //
    // Summary:
    //     Gets or sets a value indicating whether a ToolTip window is displayed, even
    //     when its parent control is not active.
    //
    // Returns:
    //     true if the ToolTip is always displayed; otherwise, false. The default is
    //     false.
   //[DefaultValue(false)]
    //public bool ShowAlways { get; set; }
    //
    // Summary:
    //     Gets or sets a value that determines how ampersand (&) characters are treated.
    //
    // Returns:
    //     true if ampersand characters are stripped from the ToolTip text; otherwise,
    //     false. The default is false.
   //[Browsable(true)]
   //[DefaultValue(false)]
    //public bool StripAmpersands { get; set; }
    //
    // Summary:
    //     Gets or sets the object that contains programmer-supplied data associated
    //     with the System.Windows.Forms.ToolTip.
    //
    // Returns:
    //     An System.Object that contains data about the System.Windows.Forms.ToolTip.
    //     The default is null.
   //[Localizable(false)]
   //[Bindable(true)]
   //[DefaultValue("")]
   //[TypeConverter(typeof(StringConverter))]
    //public object Tag { get; set; }
    //
    // Summary:
    //     Gets or sets a value that defines the type of icon to be displayed alongside
    //     the ToolTip text.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolTipIcon enumerated values.
    //public ToolTipIcon ToolTipIcon { get; set; }
    //
    // Summary:
    //     Gets or sets a title for the ToolTip window.
    //
    // Returns:
    //     A System.String containing the window title.
   //[DefaultValue("")]
    //public string ToolTipTitle { get; set; }
    //
    // Summary:
    //     Gets or sets a value determining whether an animation effect should be used
    //     when displaying the ToolTip.
    //
    // Returns:
    //     true if window animation should be used; otherwise, false. The default is
    //     true.
   //[Browsable(true)]
   //[DefaultValue(true)]
    //public bool UseAnimation { get; set; }
    //
    // Summary:
    //     Gets or sets a value determining whether a fade effect should be used when
    //     displaying the ToolTip.
    //
    // Returns:
    //     true if window fading should be used; otherwise, false. The default is true.
   //[DefaultValue(true)]
   //[Browsable(true)]
    //public bool UseFading { get; set; }

    // Summary:
    //     Occurs when the ToolTip is drawn and the System.Windows.Forms.ToolTip.OwnerDraw
    //     property is set to true.
    //public event DrawToolTipEventHandler Draw;
    //
    // Summary:
    //     Occurs before a ToolTip is initially displayed. This is the default event
    //     for the System.Windows.Forms.ToolTip class.
    //public event PopupEventHandler Popup;

    // Summary:
    //     Returns true if the ToolTip can offer an extender property to the specified
    //     target component.
    //
    // Parameters:
    //   target:
    //     The target object to add an extender property to.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolTip class can offer one or more extender
    //     properties; otherwise, false.
    [Pure]
    public virtual bool CanExtend(object target)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Disposes of the System.Windows.Forms.ToolTip component.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    //protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Retrieves the ToolTip text associated with the specified control.
    //
    // Parameters:
    //   control:
    //     The System.Windows.Forms.Control for which to retrieve the System.Windows.Forms.ToolTip
    //     text.
    //
    // Returns:
    //     A System.String containing the ToolTip text for the specified control.
   //[DefaultValue("")]
   //[Localizable(true)]
   //[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [Pure]
    public string GetToolTip(Control control)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Hides the specified ToolTip window.
    //
    // Parameters:
    //   win:
    //     The System.Windows.Forms.IWin32Window of the associated window or control
    //     that the ToolTip is associated with.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     win is null.
    public void Hide(IWin32Window win)
    {
      Contract.Requires(win != null);

    }
    //
    // Summary:
    //     Removes all ToolTip text currently associated with the ToolTip component.
    //public void RemoveAll();
    //
    // Summary:
    //     Associates ToolTip text with the specified control.
    //
    // Parameters:
    //   control:
    //     The System.Windows.Forms.Control to associate the ToolTip text with.
    //
    //   caption:
    //     The ToolTip text to display when the pointer is on the control.
    //public void SetToolTip(Control control, string caption);
    //
    // Summary:
    //     Sets the ToolTip text associated with the specified control, and displays
    //     the ToolTip modally.
    //
    // Parameters:
    //   text:
    //     A System.String containing the new ToolTip text.
    //
    //   window:
    //     The System.Windows.Forms.Control to display the ToolTip for.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The window parameter is null.
    public void Show(string text, IWin32Window window)
    {
      Contract.Requires(window != null);

    }
    //
    // Summary:
    //     Sets the ToolTip text associated with the specified control, and then displays
    //     the ToolTip for the specified duration.
    //
    // Parameters:
    //   text:
    //     A System.String containing the new ToolTip text.
    //
    //   window:
    //     The System.Windows.Forms.Control to display the ToolTip for.
    //
    //   duration:
    //     An System.Int32 containing the duration, in milliseconds, to display the
    //     ToolTip.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The window parameter is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     duration is less than or equal to 0.
    [Pure]
    public void Show(string text, IWin32Window window, int duration)
    {
      Contract.Requires(window != null);
      Contract.Requires(duration >= 0);
    }
    // Summary:
    //     Sets the ToolTip text associated with the specified control, and then displays
    //     the ToolTip modally at the specified relative position.
    //
    // Parameters:
    //   text:
    //     A System.String containing the new ToolTip text.
    //
    //   window:
    //     The System.Windows.Forms.Control to display the ToolTip for.
    //
    //   point:
    //     A System.Drawing.Point containing the offset, in pixels, relative to the
    //     upper-left corner of the associated control window, to display the ToolTip.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The window parameter is null.
    [Pure]
    public void Show(string text, IWin32Window window, Point point)
    {
      Contract.Requires(window != null);
      
    }
    //
    // Summary:
    //     Sets the ToolTip text associated with the specified control, and then displays
    //     the ToolTip modally at the specified relative position.
    //
    // Parameters:
    //   text:
    //     A System.String containing the new ToolTip text.
    //
    //   window:
    //     The System.Windows.Forms.Control to display the ToolTip for.
    //
    //   x:
    //     The horizontal offset, in pixels, relative to the upper-left corner of the
    //     associated control window, to display the ToolTip.
    //
    //   y:
    //     The vertical offset, in pixels, relative to the upper-left corner of the
    //     associated control window, to display the ToolTip.
    [Pure]
    public void Show(string text, IWin32Window window, int x, int y)
    {
      Contract.Requires(window != null);

    }
    //
    // Summary:
    //     Sets the ToolTip text associated with the specified control, and then displays
    //     the ToolTip for the specified duration at the specified relative position.
    //
    // Parameters:
    //   text:
    //     A System.String containing the new ToolTip text.
    //
    //   window:
    //     The System.Windows.Forms.Control to display the ToolTip for.
    //
    //   point:
    //     A System.Drawing.Point containing the offset, in pixels, relative to the
    //     upper-left corner of the associated control window, to display the ToolTip.
    //
    //   duration:
    //     An System.Int32 containing the duration, in milliseconds, to display the
    //     ToolTip.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The window parameter is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     duration is less than or equal to 0.
    [Pure]
    public void Show(string text, IWin32Window window, Point point, int duration)
          {
      Contract.Requires(window != null);
      Contract.Requires(duration >= 0);
    }
    //
    // Summary:
    //     Sets the ToolTip text associated with the specified control, and then displays
    //     the ToolTip for the specified duration at the specified relative position.
    //
    // Parameters:
    //   text:
    //     A System.String containing the new ToolTip text.
    //
    //   window:
    //     The System.Windows.Forms.Control to display the ToolTip for.
    //
    //   x:
    //     The horizontal offset, in pixels, relative to the upper-left corner of the
    //     associated control window, to display the ToolTip.
    //
    //   y:
    //     The vertical offset, in pixels, relative to the upper-left corner of the
    //     associated control window, to display the ToolTip.
    //
    //   duration:
    //     An System.Int32 containing the duration, in milliseconds, to display the
    //     ToolTip.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The window parameter is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     duration is less than or equal to 0.
    [Pure]
    public void Show(string text, IWin32Window window, int x, int y, int duration)
    {
      Contract.Requires(window != null);
      Contract.Requires(duration >= 0);
    }

    //
    // Summary:
    //     Stops the timer that hides displayed ToolTips.
    //protected void StopTimer();
    //
    // Summary:
    //     Returns a string representation for this control.
    //
    // Returns:
    //     A System.String containing a description of the System.Windows.Forms.ToolTip.
    //public override string ToString();
  }
}
