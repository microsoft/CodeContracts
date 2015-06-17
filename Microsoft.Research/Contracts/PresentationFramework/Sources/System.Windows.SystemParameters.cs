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

// File System.Windows.SystemParameters.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Windows
{
  static public partial class SystemParameters
  {
    #region Properties and indexers
    public static int Border
    {
      get
      {
        return default(int);
      }
    }

    public static ResourceKey BorderKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double BorderWidth
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey BorderWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double CaptionHeight
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey CaptionHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double CaptionWidth
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey CaptionWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double CaretWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey CaretWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool ClientAreaAnimation
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey ClientAreaAnimationKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool ComboBoxAnimation
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey ComboBoxAnimationKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static System.Windows.Controls.Primitives.PopupAnimation ComboBoxPopupAnimation
    {
      get
      {
        Contract.Ensures(((System.Windows.Controls.Primitives.PopupAnimation)(0)) <= Contract.Result<System.Windows.Controls.Primitives.PopupAnimation>());
        Contract.Ensures(Contract.Result<System.Windows.Controls.Primitives.PopupAnimation>() <= ((System.Windows.Controls.Primitives.PopupAnimation)(2)));

        return default(System.Windows.Controls.Primitives.PopupAnimation);
      }
    }

    public static ResourceKey ComboBoxPopupAnimationKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double CursorHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey CursorHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool CursorShadow
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey CursorShadowKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double CursorWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey CursorWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool DragFullWindows
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey DragFullWindowsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool DropShadow
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey DropShadowKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double FixedFrameHorizontalBorderHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey FixedFrameHorizontalBorderHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double FixedFrameVerticalBorderWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey FixedFrameVerticalBorderWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool FlatMenu
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey FlatMenuKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double FocusBorderHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey FocusBorderHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double FocusBorderWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey FocusBorderWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double FocusHorizontalBorderHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey FocusHorizontalBorderHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double FocusVerticalBorderWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey FocusVerticalBorderWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static ResourceKey FocusVisualStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static int ForegroundFlashCount
    {
      get
      {
        return default(int);
      }
    }

    public static ResourceKey ForegroundFlashCountKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double FullPrimaryScreenHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey FullPrimaryScreenHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double FullPrimaryScreenWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey FullPrimaryScreenWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool GradientCaptions
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey GradientCaptionsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool HighContrast
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey HighContrastKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double HorizontalScrollBarButtonWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey HorizontalScrollBarButtonWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double HorizontalScrollBarHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey HorizontalScrollBarHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double HorizontalScrollBarThumbWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey HorizontalScrollBarThumbWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool HotTracking
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey HotTrackingKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double IconGridHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey IconGridHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double IconGridWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey IconGridWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double IconHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey IconHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double IconHorizontalSpacing
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey IconHorizontalSpacingKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IconTitleWrap
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IconTitleWrapKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double IconVerticalSpacing
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey IconVerticalSpacingKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double IconWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey IconWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsImmEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsImmEnabledKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsMediaCenter
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsMediaCenterKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsMenuDropRightAligned
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsMenuDropRightAlignedKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsMiddleEastEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsMiddleEastEnabledKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsMousePresent
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsMousePresentKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsMouseWheelPresent
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsMouseWheelPresentKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsPenWindows
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsPenWindowsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsRemotelyControlled
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsRemotelyControlledKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsRemoteSession
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsRemoteSessionKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsSlowMachine
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsSlowMachineKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool IsTabletPC
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey IsTabletPCKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double KanjiWindowHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey KanjiWindowHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool KeyboardCues
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey KeyboardCuesKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static int KeyboardDelay
    {
      get
      {
        return default(int);
      }
    }

    public static ResourceKey KeyboardDelayKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool KeyboardPreference
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey KeyboardPreferenceKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static int KeyboardSpeed
    {
      get
      {
        return default(int);
      }
    }

    public static ResourceKey KeyboardSpeedKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool ListBoxSmoothScrolling
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey ListBoxSmoothScrollingKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MaximizedPrimaryScreenHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MaximizedPrimaryScreenHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MaximizedPrimaryScreenWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MaximizedPrimaryScreenWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MaximumWindowTrackHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MaximumWindowTrackHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MaximumWindowTrackWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MaximumWindowTrackWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool MenuAnimation
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey MenuAnimationKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MenuBarHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MenuBarHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MenuButtonHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MenuButtonHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MenuButtonWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MenuButtonWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MenuCheckmarkHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MenuCheckmarkHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MenuCheckmarkWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MenuCheckmarkWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool MenuDropAlignment
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey MenuDropAlignmentKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool MenuFade
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey MenuFadeKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MenuHeight
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey MenuHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static System.Windows.Controls.Primitives.PopupAnimation MenuPopupAnimation
    {
      get
      {
        Contract.Ensures(((System.Windows.Controls.Primitives.PopupAnimation)(0)) <= Contract.Result<System.Windows.Controls.Primitives.PopupAnimation>());
        Contract.Ensures(Contract.Result<System.Windows.Controls.Primitives.PopupAnimation>() <= ((System.Windows.Controls.Primitives.PopupAnimation)(3)));

        return default(System.Windows.Controls.Primitives.PopupAnimation);
      }
    }

    public static ResourceKey MenuPopupAnimationKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static int MenuShowDelay
    {
      get
      {
        return default(int);
      }
    }

    public static ResourceKey MenuShowDelayKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MenuWidth
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey MenuWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool MinimizeAnimation
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey MinimizeAnimationKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MinimizedGridHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MinimizedGridHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MinimizedGridWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MinimizedGridWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MinimizedWindowHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MinimizedWindowHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MinimizedWindowWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MinimizedWindowWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MinimumHorizontalDragDistance
    {
      get
      {
        return default(double);
      }
    }

    public static double MinimumVerticalDragDistance
    {
      get
      {
        return default(double);
      }
    }

    public static double MinimumWindowHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MinimumWindowHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MinimumWindowTrackHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MinimumWindowTrackHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MinimumWindowTrackWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MinimumWindowTrackWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MinimumWindowWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MinimumWindowWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MouseHoverHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MouseHoverHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static TimeSpan MouseHoverTime
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public static ResourceKey MouseHoverTimeKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double MouseHoverWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey MouseHoverWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static ResourceKey NavigationChromeDownLevelStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static ResourceKey NavigationChromeStyleKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static PowerLineStatus PowerLineStatus
    {
      get
      {
        return default(PowerLineStatus);
      }
    }

    public static ResourceKey PowerLineStatusKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double PrimaryScreenHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey PrimaryScreenHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double PrimaryScreenWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey PrimaryScreenWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double ResizeFrameHorizontalBorderHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey ResizeFrameHorizontalBorderHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double ResizeFrameVerticalBorderWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey ResizeFrameVerticalBorderWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double ScrollHeight
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey ScrollHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double ScrollWidth
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey ScrollWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool SelectionFade
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey SelectionFadeKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool ShowSounds
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey ShowSoundsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double SmallCaptionHeight
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey SmallCaptionHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double SmallCaptionWidth
    {
      get
      {
        Contract.Ensures(-(Contract.Result<double>()) <= 206158430208);
        Contract.Ensures(-206158430208 <= Contract.Result<double>());

        return default(double);
      }
    }

    public static ResourceKey SmallCaptionWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double SmallIconHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey SmallIconHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double SmallIconWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey SmallIconWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double SmallWindowCaptionButtonHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey SmallWindowCaptionButtonHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double SmallWindowCaptionButtonWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey SmallWindowCaptionButtonWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool SnapToDefaultButton
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey SnapToDefaultButtonKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool StylusHotTracking
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey StylusHotTrackingKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool SwapButtons
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey SwapButtonsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double ThickHorizontalBorderHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey ThickHorizontalBorderHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double ThickVerticalBorderWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey ThickVerticalBorderWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double ThinHorizontalBorderHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey ThinHorizontalBorderHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double ThinVerticalBorderWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey ThinVerticalBorderWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool ToolTipAnimation
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey ToolTipAnimationKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool ToolTipFade
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey ToolTipFadeKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static System.Windows.Controls.Primitives.PopupAnimation ToolTipPopupAnimation
    {
      get
      {
        Contract.Ensures(((System.Windows.Controls.Primitives.PopupAnimation)(0)) <= Contract.Result<System.Windows.Controls.Primitives.PopupAnimation>());
        Contract.Ensures(Contract.Result<System.Windows.Controls.Primitives.PopupAnimation>() <= ((System.Windows.Controls.Primitives.PopupAnimation)(1)));

        return default(System.Windows.Controls.Primitives.PopupAnimation);
      }
    }

    public static ResourceKey ToolTipPopupAnimationKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static bool UIEffects
    {
      get
      {
        return default(bool);
      }
    }

    public static ResourceKey UIEffectsKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double VerticalScrollBarButtonHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey VerticalScrollBarButtonHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double VerticalScrollBarThumbHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey VerticalScrollBarThumbHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double VerticalScrollBarWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey VerticalScrollBarWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double VirtualScreenHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey VirtualScreenHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double VirtualScreenLeft
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey VirtualScreenLeftKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double VirtualScreenTop
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey VirtualScreenTopKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double VirtualScreenWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey VirtualScreenWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static int WheelScrollLines
    {
      get
      {
        return default(int);
      }
    }

    public static ResourceKey WheelScrollLinesKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double WindowCaptionButtonHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey WindowCaptionButtonHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double WindowCaptionButtonWidth
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey WindowCaptionButtonWidthKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static double WindowCaptionHeight
    {
      get
      {
        return default(double);
      }
    }

    public static ResourceKey WindowCaptionHeightKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }

    public static Rect WorkArea
    {
      get
      {
        return default(Rect);
      }
    }

    public static ResourceKey WorkAreaKey
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceKey>() != null);

        return default(ResourceKey);
      }
    }
    #endregion
  }
}
