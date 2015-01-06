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

// File System.Windows.Controls.Calendar.cs
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


namespace System.Windows.Controls
{
  public partial class Calendar : Control
  {
    #region Methods and constructors
    public Calendar()
    {
    }

    public override void OnApplyTemplate()
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected virtual new void OnDisplayDateChanged(CalendarDateChangedEventArgs e)
    {
    }

    protected virtual new void OnDisplayModeChanged(CalendarModeChangedEventArgs e)
    {
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnKeyUp(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected virtual new void OnSelectedDatesChanged(SelectionChangedEventArgs e)
    {
    }

    protected virtual new void OnSelectionModeChanged(EventArgs e)
    {
    }

    #endregion

    #region Properties and indexers
    public CalendarBlackoutDatesCollection BlackoutDates
    {
      get
      {
        return default(CalendarBlackoutDatesCollection);
      }
    }

    public System.Windows.Style CalendarButtonStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public System.Windows.Style CalendarDayButtonStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public System.Windows.Style CalendarItemStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public DateTime DisplayDate
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public Nullable<DateTime> DisplayDateEnd
    {
      get
      {
        return default(Nullable<DateTime>);
      }
      set
      {
      }
    }

    public Nullable<DateTime> DisplayDateStart
    {
      get
      {
        return default(Nullable<DateTime>);
      }
      set
      {
      }
    }

    public CalendarMode DisplayMode
    {
      get
      {
        return default(CalendarMode);
      }
      set
      {
      }
    }

    public DayOfWeek FirstDayOfWeek
    {
      get
      {
        return default(DayOfWeek);
      }
      set
      {
      }
    }

    public bool IsTodayHighlighted
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Nullable<DateTime> SelectedDate
    {
      get
      {
        return default(Nullable<DateTime>);
      }
      set
      {
      }
    }

    public SelectedDatesCollection SelectedDates
    {
      get
      {
        return default(SelectedDatesCollection);
      }
    }

    public CalendarSelectionMode SelectionMode
    {
      get
      {
        return default(CalendarSelectionMode);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler<CalendarDateChangedEventArgs> DisplayDateChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<CalendarModeChangedEventArgs> DisplayModeChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<SelectionChangedEventArgs> SelectedDatesChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<EventArgs> SelectionModeChanged
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty CalendarButtonStyleProperty;
    public readonly static System.Windows.DependencyProperty CalendarDayButtonStyleProperty;
    public readonly static System.Windows.DependencyProperty CalendarItemStyleProperty;
    public readonly static System.Windows.DependencyProperty DisplayDateEndProperty;
    public readonly static System.Windows.DependencyProperty DisplayDateProperty;
    public readonly static System.Windows.DependencyProperty DisplayDateStartProperty;
    public readonly static System.Windows.DependencyProperty DisplayModeProperty;
    public readonly static System.Windows.DependencyProperty FirstDayOfWeekProperty;
    public readonly static System.Windows.DependencyProperty IsTodayHighlightedProperty;
    public readonly static System.Windows.DependencyProperty SelectedDateProperty;
    public readonly static System.Windows.RoutedEvent SelectedDatesChangedEvent;
    public readonly static System.Windows.DependencyProperty SelectionModeProperty;
    #endregion
  }
}
