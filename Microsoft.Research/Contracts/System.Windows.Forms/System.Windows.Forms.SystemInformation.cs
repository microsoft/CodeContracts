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

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Windows.Forms
{
    // <summary>
    // Provides information about the current system environment.
    // </summary>
    // <filterpriority>2</filterpriority>
    public class SystemInformation
    {
        // <summary>
        // Gets a value indicating whether the user has enabled full window drag.
        // </summary>
        //
        // <returns>
        // true if the user has enabled full window drag; otherwise, false.
        // </returns>
        // public static bool DragFullWindows { get; }

        // <summary>
        // Gets a value indicating whether the user has enabled the high-contrast mode accessibility feature.
        // </summary>
        //
        // <returns>
        // true if the user has enabled high-contrast mode; otherwise, false.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public static bool HighContrast { get; }

        // <summary>
        // Gets the number of lines to scroll when the mouse wheel is rotated.
        // </summary>
        //
        // <returns>
        // The number of lines to scroll on a mouse wheel rotation, or -1 if the "One screen at a time" mouse option is selected.
        // </returns>
        // public static int MouseWheelScrollLines {get;}

        // <summary>
        // Gets the dimensions, in pixels, of the current video mode of the primary display.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the dimensions, in pixels, of the current video mode of the primary display.
        // </returns>
        // public static Size PrimaryMonitorSize { get; }

        // <summary>
        // Gets the default width, in pixels, of the vertical scroll bar.
        // </summary>
        //
        // <returns>
        // The default width, in pixels, of the vertical scroll bar.
        // </returns>
        // // public static int VerticalScrollBarWidth {get;}

        // <summary>
        // Gets the default height, in pixels, of the horizontal scroll bar.
        // </summary>
        //
        // <returns>
        // The default height, in pixels, of the horizontal scroll bar.
        // </returns>
        // public static int HorizontalScrollBarHeight {get;}

        // <summary>
        // Gets the height, in pixels, of the standard title bar area of a window.
        // </summary>
        //
        // <returns>
        // The height, in pixels, of the standard title bar area of a window.
        // </returns>
        // public static int CaptionHeight {get;}

        // <summary>
        // Gets the thickness, in pixels, of a flat-style window or system control border.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the width, in pixels, of a vertical border, and the height, in pixels, of a horizontal border.
        // </returns>
        // public static Size BorderSize {get;}

        // <summary>
        // Gets the thickness, in pixels, of the frame border of a window that has a caption and is not resizable.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that specifies the thickness, in pixels, of a fixed sized window border.
        // </returns>
        // public static Size FixedFrameBorderSize {get;}

        // <summary>
        // Gets the height, in pixels, of the scroll box in a vertical scroll bar.
        // </summary>
        //
        // <returns>
        // The height, in pixels, of the scroll box in a vertical scroll bar.
        // </returns>
        // public static int VerticalScrollBarThumbHeight { get; }

        // <summary>
        // Gets the width, in pixels, of the scroll box in a horizontal scroll bar.
        // </summary>
        //
        // <returns>
        // The width, in pixels, of the scroll box in a horizontal scroll bar.
        // </returns>
        // public static int HorizontalScrollBarThumbWidth { get; }

        // <summary>
        // Gets the dimensions, in pixels, of the Windows default program icon size.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the default dimensions, in pixels, for a program icon.
        // </returns>
        // public static Size IconSize { get; }

        // <summary>
        // Gets the maximum size, in pixels, that a cursor can occupy.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the maximum dimensions of a cursor in pixels.
        // </returns>
        // public static Size CursorSize { get; }

        // <summary>
        // Gets the font used to display text on menus.
        // </summary>
        //
        // <returns>
        // The <see cref="T:System.Drawing.Font"/> used to display text on menus.
        // </returns>
        // public static Font MenuFont { get; }

        // <summary>
        // Gets the height, in pixels, of one line of a menu.
        // </summary>
        //
        // <returns>
        // The height, in pixels, of one line of a menu.
        // </returns>
        // public static int MenuHeight { get; }

        // <summary>
        // Gets the current system power status.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Windows.Forms.PowerStatus"/> that indicates the current system power status.
        // </returns>
        // public static PowerStatus PowerStatus { get; }

        // <summary>
        // Gets the size, in pixels, of the working area of the screen.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Rectangle"/> that represents the size, in pixels, of the working area of the screen.
        // </returns>
        // public static Rectangle WorkingArea { get; }

        // <summary>
        // Gets the height, in pixels, of the Kanji window at the bottom of the screen for double-byte character set (DBCS) versions of Windows.
        // </summary>
        //       
        // <returns>
        // The height, in pixels, of the Kanji window.
        // </returns>
        // public static int KanjiWindowHeight { get; }

        // <summary>
        // Gets a value indicating whether a pointing device is installed.
        // </summary>
        //
        // <returns>
        // true if a mouse is installed; otherwise, false.
        // </returns>
        // public static bool MousePresent { get; }

        // <summary>
        // Gets the height, in pixels, of the arrow bitmap on the vertical scroll bar.
        // </summary>
        //
        // <returns>
        // The height, in pixels, of the arrow bitmap on the vertical scroll bar.
        // </returns>
        // public static int VerticalScrollBarArrowHeight { get; }

        // <summary>
        // Gets the width, in pixels, of the arrow bitmap on the horizontal scroll bar.
        // </summary>
        //
        // <returns>
        // The width, in pixels, of the arrow bitmap on the horizontal scroll bar.
        // </returns>
        // public static int HorizontalScrollBarArrowWidth { get; }

        // <summary>
        // Gets a value indicating whether the debug version of USER.EXE is installed.
        // </summary>
        //
        // <returns>
        // true if the debugging version of USER.EXE is installed; otherwise, false.
        // </returns>
        // public static bool DebugOS { get; }

        // <summary>
        // Gets a value indicating whether the functions of the left and right mouse buttons have been swapped.
        // </summary>
        //
        // <returns>
        // true if the functions of the left and right mouse buttons are swapped; otherwise, false.
        // </returns>
        // public static bool MouseButtonsSwapped { get; }

        // <summary>
        // Gets the minimum width and height for a window, in pixels.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that specifies the minimum allowable dimensions of a window, in pixels.
        // </returns>
        // public static Size MinimumWindowSize { get; }

        // <summary>
        // Gets the standard size, in pixels, of a button in a window's title bar.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the standard dimensions, in pixels, of a button in a window's title bar.
        // </returns>
        // public static Size CaptionButtonSize { get; }

        // <summary>
        // Gets the thickness, in pixels, of the resizing border that is drawn around the perimeter of a window that is being drag resized.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the thickness, in pixels, of the width of a vertical resizing border and the height of a horizontal resizing border.
        // </returns>
        // public static Size FrameBorderSize { get; }

        // <summary>
        // Gets the default minimum dimensions, in pixels, that a window may occupy during a drag resize.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the default minimum width and height of a window during resize, in pixels.
        // </returns>
        // public static Size MinWindowTrackSize { get; }

        // <summary>
        // Gets the dimensions, in pixels, of the area within which the user must click twice for the operating system to consider the two clicks a double-click.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the dimensions, in pixels, of the area within which the user must click twice for the operating system to consider the two clicks a double-click.
        // </returns>
        // public static Size DoubleClickSize { get; }

        // <summary>
        // Gets the maximum number of milliseconds that can elapse between a first click and a second click for the OS to consider the mouse action a double-click.
        // </summary>
        //
        // <returns>
        // The maximum amount of time, in milliseconds, that can elapse between a first click and a second click for the OS to consider the mouse action a double-click.
        // </returns>
        // public static int DoubleClickTime { get; }

        // <summary>
        // Gets the size, in pixels, of the grid square used to arrange icons in a large-icon view.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that specifies the dimensions, in pixels, of the grid square used to arrange icons in a large-icon view.
        // </returns>
        // public static Size IconSpacingSize { get; }

        // <summary>
        // Gets a value indicating whether drop-down menus are right-aligned with the corresponding menu-bar item.
        // </summary>
        //
        // <returns>
        // true if drop-down menus are right-aligned with the corresponding menu-bar item; false if the menus are left-aligned.
        // </returns>
        // public static bool RightAlignedMenus { get; }

        // <summary>
        // Gets a value indicating whether the Microsoft Windows for Pen Computing extensions are installed.
        // </summary>
        //
        // <returns>
        // true if the Windows for Pen Computing extensions are installed; false if Windows for Pen Computing extensions are not installed.
        // </returns>
        // public static bool PenWindows { get; }

        // <summary>
        // Gets a value indicating whether the operating system is capable of handling double-byte character set (DBCS) characters.
        // </summary>
        //
        // <returns>
        // true if the operating system supports DBCS; otherwise, false.
        // </returns>
        // public static bool DbcsEnabled { get; }

        // <summary>
        // Gets the number of buttons on the mouse.
        // </summary>
        //
        // <returns>
        // The number of buttons on the mouse, or zero if no mouse is installed.
        // </returns>
        // public static int MouseButtons { get; }

        // <summary>
        // Gets a value indicating whether a Security Manager is present on this operating system.
        // </summary>
        //
        // <returns>
        // true if a Security Manager is present; otherwise, false.
        // </returns>
        // public static bool Secure { get; }

        // <summary>
        // Gets the thickness, in pixels, of a three-dimensional (3-D) style window or system control border.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the width, in pixels, of a 3-D style vertical border, and the height, in pixels, of a 3-D style horizontal border.
        // </returns>
        // public static Size Border3DSize { get; }

        // <summary>
        // Gets the dimensions, in pixels, of the area each minimized window is allocated when arranged.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the area each minimized window is allocated when arranged.
        // </returns>
        // public static Size MinimizedWindowSpacingSize { get; }

        // <summary>
        // Gets the dimensions, in pixels, of a small icon.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that specifies the dimensions, in pixels, of a small icon.
        // </returns>
        // public static Size SmallIconSize  { get; }

        // <summary>
        // Gets the height, in pixels, of a tool window caption.
        // </summary>
        //
        // <returns>
        // The height, in pixels, of a tool window caption in pixels.
        // </returns>
        // public static int ToolWindowCaptionHeight { get; }

        // <summary>
        // Gets the dimensions, in pixels, of small caption buttons.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the dimensions, in pixels, of small caption buttons.
        // </returns>
        // public static Size ToolWindowCaptionButtonSize { get; }

        // <summary>
        // Gets the default dimensions, in pixels, of menu-bar buttons.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the default dimensions, in pixels, of menu-bar buttons.
        // </returns>
        // public static Size MenuButtonSize { get; }

        // <summary>
        // Gets an <see cref="T:System.Windows.Forms.ArrangeStartingPosition"/> value that indicates the starting position from which the operating system arranges minimized windows.
        // </summary>
        //
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.ArrangeStartingPosition"/> values that indicates the starting position from which the operating system arranges minimized windows.
        // </returns>
        // public static ArrangeStartingPosition ArrangeStartingPosition { get; }

        // <summary>
        // Gets a value that indicates the direction in which the operating system arranges minimized windows.
        // </summary>
        //
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.ArrangeDirection"/> values that indicates the direction in which the operating system arranges minimized windows.
        // </returns>
        // public static ArrangeDirection ArrangeDirection { get; }

        // <summary>
        // Gets the dimensions, in pixels, of a normal minimized window.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the dimensions, in pixels, of a normal minimized window.
        // </returns>
        // public static Size MinimizedWindowSize { get; }

        // <summary>
        // Gets the default maximum dimensions, in pixels, of a window that has a caption and sizing borders.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that specifies the maximum dimensions, in pixels, to which a window can be sized.
        // </returns>
        // public static Size MaxWindowTrackSize { get; }

        // <summary>
        // Gets the default dimensions, in pixels, of a maximized window on the primary display.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that specifies the dimensions, in pixels, of a maximized window on the primary display.
        // </returns>
        // public static Size PrimaryMonitorMaximizedWindowSize { get; }

        // <summary>
        // Gets a value indicating whether a network connection is present.
        // </summary>
        //
        // <returns>
        // true if a network connection is present; otherwise, false.
        // </returns>
        // public static bool Network { get; }

        // <summary>
        // Gets a value indicating whether the calling process is associated with a Terminal Services client session.
        // </summary>
        //
        // <returns>
        // true if the calling process is associated with a Terminal Services client session; otherwise, false.
        // </returns>
        // public static bool TerminalServerSession { get; }

        // <summary>
        // Gets a <see cref="T:System.Windows.Forms.BootMode"/> value that indicates the boot mode the system was started in.
        // </summary>
        //
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.BootMode"/> values that indicates the boot mode the system was started in.
        // </returns>
        // public static BootMode BootMode { get; }

        // <summary>
        // Gets the width and height of a rectangle centered on the point the mouse button was pressed, within which a drag operation will not begin.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that specifies the area of a rectangle, in pixels, centered on the point the mouse button was pressed, within which a drag operation will not begin.
        // </returns>
        // public static Size DragSize { get; }

        // <summary>
        // Gets a value indicating whether the user prefers that an application present information in visual form in situations when it would present the information in audible form.
        // </summary>
        //
        // <returns>
        // true if the application should visually show information about audible output; false if the application does not need to provide extra visual cues for audio events.
        // </returns>
        // public static bool ShowSounds { get; }

        // <summary>
        // Gets the dimensions, in pixels, of the default size of a menu check mark area.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the default size, in pixels, of a menu check mark area.
        // </returns>
        // public static Size MenuCheckSize { get; }

        // <summary>
        // Gets a value indicating whether the operating system is enabled for the Hebrew and Arabic languages.
        // </summary>
        //
        // <returns>
        // true if the operating system is enabled for Hebrew or Arabic; otherwise, false.
        // </returns>
        // public static bool MidEastEnabled { get; }

        // <summary>
        // Gets a value indicating whether the operating system natively supports a mouse wheel.
        // </summary>
        //
        // <returns>
        // true if the operating system natively supports a mouse wheel; otherwise, false.
        // </returns>
        // public static bool NativeMouseWheelSupport { get; }

        // <summary>
        // Gets a value indicating whether a mouse with a mouse wheel is installed.
        // </summary>
        //
        // <returns>
        // true if a mouse with a mouse wheel is installed; otherwise, false.
        // </returns>
        // public static bool MouseWheelPresent { get; }

        // <summary>
        // Gets the bounds of the virtual screen.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Rectangle"/> that specifies the bounding rectangle of the entire virtual screen.
        // </returns>
        // public static Rectangle VirtualScreen { get; }

        // <summary>
        // Gets the number of display monitors on the desktop.
        // </summary>
        //
        // <returns>
        // The number of monitors that make up the desktop.
        // </returns>
        // public static int MonitorCount { get; }

        // <summary>
        // Gets a value indicating whether all the display monitors are using the same pixel color format.
        // </summary>
        //
        // <returns>
        // true if all monitors are using the same pixel color format; otherwise, false.
        // </returns>
        // public static bool MonitorsSameDisplayFormat { get; }

        // <summary>
        // Gets the NetBIOS computer name of the local computer.
        // </summary>
        //
        // <returns>
        // The name of this computer.
        // </returns>
        // public static string ComputerName { get; }

        // <summary>
        // Gets the name of the domain the user belongs to.
        // </summary>
        //
        // <returns>
        // The name of the user domain. If a local user account exists with the same name as the user name, this property gets the computer name.
        // </returns>
        // <exception cref="T:System.ComponentModel.Win32Exception">The operating system does not support this feature.</exception><filterpriority>1</filterpriority>
        // public static string UserDomainName { get; }

        // <summary>
        // Gets a value indicating whether the current process is running in user-interactive mode.
        // </summary>
        //
        // <returns>
        // true if the current process is running in user-interactive mode; otherwise, false.
        // </returns>
        // public static bool UserInteractive { get; }

        // <summary>
        // Gets the user name associated with the current thread.
        // </summary>
        //
        // <returns>
        // The user name of the user associated with the current thread.
        // </returns>
        // public static string UserName { get; }

        // <summary>
        // Gets a value indicating whether the drop shadow effect is enabled.
        // </summary>
        //
        // <returns>
        // true if the drop shadow effect is enabled; otherwise, false.
        // </returns>
        // public static bool IsDropShadowEnabled { get; }

        // <summary>
        // Gets a value indicating whether native user menus have a flat menu appearance.
        // </summary>
        //
        // <returns>
        // This property is not used and always returns false.
        // </returns>
        // public static bool IsFlatMenuEnabled { get; }

        // <summary>
        // Gets a value indicating whether font smoothing is enabled.
        // </summary>
        //
        // <returns>
        // true if the font smoothing feature is enabled; otherwise, false.
        // </returns>
        // public static bool IsFontSmoothingEnabled { get; }

        // <summary>
        // Gets the font smoothing contrast value used in ClearType smoothing.
        // </summary>
        //
        // <returns>
        // The ClearType font smoothing contrast value.
        // </returns>
        // <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception><filterpriority>1</filterpriority>
        // public static int FontSmoothingContrast { get; }

        // <summary>
        // Gets the current type of font smoothing.
        // </summary>
        //
        // <returns>
        // A value that indicates the current type of font smoothing.
        // </returns>
        // <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception><filterpriority>1</filterpriority>
        // public static int FontSmoothingType { get; }

        // <summary>
        // Gets the width, in pixels, of an icon arrangement cell in large icon view.
        // </summary>
        //
        // <returns>
        // The width, in pixels, of an icon arrangement cell in large icon view.
        // </returns>
        // public static int IconHorizontalSpacing { get; }

        // <summary>
        // Gets the height, in pixels, of an icon arrangement cell in large icon view.
        // </summary>
        //
        // <returns>
        // The height, in pixels, of an icon arrangement cell in large icon view.
        // </returns>
        // public static int IconVerticalSpacing { get; }

        // <summary>
        // Gets a value indicating whether icon-title wrapping is enabled.
        // </summary>
        //
        // <returns>
        // true if the icon-title wrapping feature is enabled; otherwise, false.
        // </returns>
        // public static bool IsIconTitleWrappingEnabled { get; }

        // <summary>
        // Gets a value indicating whether menu access keys are always underlined.
        // </summary>
        //
        // <returns>
        // true if menu access keys are always underlined; false if they are underlined only when the menu is activated or receives focus.
        // </returns>
        // public static bool MenuAccessKeysUnderlined { get; }

        // <summary>
        // Gets the keyboard repeat-delay setting.
        // </summary>
        //
        // <returns>
        // The keyboard repeat-delay setting, from 0 (approximately 250 millisecond delay) through 3 (approximately 1 second delay).
        // </returns>
        // public static int KeyboardDelay { get; }

        // <summary>
        // Gets a value indicating whether the user relies on the keyboard instead of the mouse, and prefers applications to display keyboard interfaces that would otherwise be hidden.
        // </summary>
        //
        // <returns>
        // true if keyboard preferred mode is enabled; otherwise, false.
        // </returns>
        // public static bool IsKeyboardPreferred { get; }

        // <summary>
        // Gets the keyboard repeat-speed setting.
        // </summary>
        //
        // <returns>
        // The keyboard repeat-speed setting, from 0 (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second).
        // </returns>
        // public static int KeyboardSpeed { get; }

        // <summary>
        // Gets the dimensions, in pixels, of the rectangle within which the mouse pointer has to stay for the mouse hover time before a mouse hover message is generated.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the dimensions, in pixels, of the rectangle within which the mouse pointer has to stay for the mouse hover time before a mouse hover message is generated.
        // </returns>
        // public static Size MouseHoverSize { get; }

        // <summary>
        // Gets the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle before a mouse hover message is generated.
        // </summary>
        //
        // <returns>
        // The time, in milliseconds, that the mouse pointer has to stay in the hover rectangle before a mouse hover message is generated.
        // </returns>
        // public static int MouseHoverTime { get; }

        // <summary>
        // Gets the current mouse speed.
        // </summary>
        //
        // <returns>
        // A mouse speed value between 1 (slowest) and 20 (fastest).
        // </returns>
        // public static int MouseSpeed { get; }

        // <summary>
        // Gets a value indicating whether the snap-to-default-button feature is enabled.
        // </summary>
        //
        // <returns>
        // true if the snap-to-default-button feature is enabled; otherwise, false.
        // </returns>
        // public static bool IsSnapToDefaultEnabled { get; }

        // <summary>
        // Gets the side of pop-up menus that are aligned to the corresponding menu-bar item.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Windows.Forms.LeftRightAlignment"/> that indicates whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu-bar item.
        // </returns>
        // public static LeftRightAlignment PopupMenuAlignment { get; }

        // <summary>
        // Gets a value indicating whether menu fade animation is enabled.
        // </summary>
        //
        // <returns>
        // true if fade animation is enabled; false if it is disabled.
        // </returns>
        // public static bool IsMenuFadeEnabled { get; }

        // <summary>
        // Gets the time, in milliseconds, that the system waits before displaying a cascaded shortcut menu when the mouse cursor is over a submenu item.
        // </summary>
        //
        // <returns>
        // The time, in milliseconds, that the system waits before displaying a cascaded shortcut menu when the mouse cursor is over a submenu item.
        // </returns>
        // public static int MenuShowDelay { get; }

        // <summary>
        // Gets a value indicating whether the slide-open effect for combo boxes is enabled.
        // </summary>
        //
        // <returns>
        // true if the slide-open effect for combo boxes is enabled; otherwise, false.
        // </returns>
        // public static bool IsComboBoxAnimationEnabled { get; }

        // <summary>
        // Gets a value indicating whether the gradient effect for window title bars is enabled.
        // </summary>
        //
        // <returns>
        // true if the gradient effect for window title bars is enabled; otherwise, false.
        // </returns>
        // public static bool IsTitleBarGradientEnabled { get; }

        // <summary>
        // Gets a value indicating whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled.
        // </summary>
        //
        // <returns>
        // true if hot tracking of user-interface elements is enabled; otherwise, false.
        // </returns>
        // public static bool IsHotTrackingEnabled { get; }

        // <summary>
        // Gets a value indicating whether the smooth-scrolling effect for list boxes is enabled.
        // </summary>
        //
        // <returns>
        // true if smooth-scrolling is enabled; otherwise, false.
        // </returns>
        // public static bool IsListBoxSmoothScrollingEnabled { get; }

        // <summary>
        // Gets a value indicating whether menu fade or slide animation features are enabled.
        // </summary>
        //
        // <returns>
        // true if menu fade or slide animation is enabled; otherwise, false.
        // </returns>
        // public static bool IsMenuAnimationEnabled { get; }

        // <summary>
        // Gets a value indicating whether the selection fade effect is enabled.
        // </summary>
        //
        // <returns>
        // true if the selection fade effect is enabled; otherwise, false.
        // </returns>
        // public static bool IsSelectionFadeEnabled { get; }

        // <summary>
        // Gets a value indicating whether <see cref="T:System.Windows.Forms.ToolTip"/> animation is enabled.
        // </summary>
        //
        // <returns>
        // true if <see cref="T:System.Windows.Forms.ToolTip"/> animation is enabled; otherwise, false.
        // </returns>
        // public static bool IsToolTipAnimationEnabled { get; }

        // <summary>
        // Gets a value indicating whether user interface (UI) effects are enabled or disabled.
        // </summary>
        //
        // <returns>
        // true if UI effects are enabled; otherwise, false.
        // </returns>
        // public static bool UIEffectsEnabled { get; }

        // <summary>
        // Gets a value indicating whether active window tracking is enabled.
        // </summary>
        //
        // <returns>
        // true if active window tracking is enabled; otherwise, false.
        // </returns>
        // public static bool IsActiveWindowTrackingEnabled { get; }

        // <summary>
        // Gets the active window tracking delay.
        // </summary>
        //
        // <returns>
        // The active window tracking delay, in milliseconds.
        // </returns>
        // public static int ActiveWindowTrackingDelay { get; }

        // <summary>
        // Gets a value indicating whether window minimize and restore animation is enabled.
        // </summary>
        //
        // <returns>
        // true if window minimize and restore animation is enabled; otherwise, false.
        // </returns>
        // public static bool IsMinimizeRestoreAnimationEnabled { get; }

        // <summary>
        // Gets the border multiplier factor that is used when determining the thickness of a window's sizing border.
        // </summary>
        //
        // <returns>
        // The multiplier used to determine the thickness of a window's sizing border.
        // </returns>
        // public static int BorderMultiplierFactor { get; }

        // <summary>
        // Gets the caret blink time.
        // </summary>
        //
        // <returns>
        // The caret blink time.
        // </returns>
        // public static int CaretBlinkTime { get; }

        // <summary>
        // Gets the width, in pixels, of the caret in edit controls.
        // </summary>
        //
        // <returns>
        // The width, in pixels, of the caret in edit controls.
        // </returns>
        // <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception><filterpriority>1</filterpriority>
        // public static int CaretWidth { get; }

        // <summary>
        // Gets the amount of the delta value of a single mouse wheel rotation increment.
        // </summary>
        //
        // <returns>
        // The amount of the delta value of a single mouse wheel rotation increment.
        // </returns>
        // public static int MouseWheelScrollDelta { get; }

        // <summary>
        // Gets the thickness, in pixels, of the top and bottom edges of the system focus rectangle.
        // </summary>
        //
        // <returns>
        // The thickness, in pixels, of the top and bottom edges of the system focus rectangle.
        // </returns>
        // <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception><filterpriority>1</filterpriority>
        // public static int VerticalFocusThickness { get; }

        // <summary>
        // Gets the thickness of the left and right edges of the system focus rectangle, in pixels.
        // </summary>
        //
        // <returns>
        // The thickness of the left and right edges of the system focus rectangle, in pixels.
        // </returns>
        // <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception><filterpriority>1</filterpriority>
        // public static int HorizontalFocusThickness { get; }

        // <summary>
        // Gets the thickness, in pixels, of the top and bottom edges of the sizing border around the perimeter of a window being resized.
        // </summary>
        //
        // <returns>
        // The height, in pixels, of the top and bottom edges of the sizing border around the perimeter of a window being resized, in pixels.
        // </returns>
        // public static int VerticalResizeBorderThickness { get; }

        // <summary>
        // Gets the thickness of the left and right edges of the sizing border around the perimeter of a window being resized, in pixels.
        // </summary>
        //
        // <returns>
        // The width of the left and right edges of the sizing border around the perimeter of a window being resized, in pixels.
        // </returns>
        // public static int HorizontalResizeBorderThickness { get; }

        // <summary>
        // Gets the orientation of the screen.
        // </summary>
        //
        // <returns>
        // The orientation of the screen, in degrees.
        // </returns>
        // public static ScreenOrientation ScreenOrientation { get; }

        // <summary>
        // Gets the width, in pixels, of the sizing border drawn around the perimeter of a window being resized.
        // </summary>
        //
        // <returns>
        // The width, in pixels, of the window sizing border drawn around the perimeter of a window being resized.
        // </returns>
        // public static int SizingBorderWidth { get; }

        // <summary>
        // Gets the width, in pixels, of small caption buttons, and the height, in pixels, of small captions.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the width, in pixels, of small caption buttons, and the height, in pixels, of small captions.
        // </returns>
        // public static Size SmallCaptionButtonSize { get; }

        // <summary>
        // Gets the default width, in pixels, for menu-bar buttons and the height, in pixels, of a menu bar.
        // </summary>
        //
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that indicates the default width for menu-bar buttons, in pixels, and the height of a menu bar, in pixels.
        // </returns>
        // public static Size MenuBarButtonSize { get; }

        private SystemInformation()
        { }
    }
}

