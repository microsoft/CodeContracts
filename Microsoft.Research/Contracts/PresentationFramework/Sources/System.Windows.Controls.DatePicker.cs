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

// File System.Windows.Controls.DatePicker.cs
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
  public partial class DatePicker : Control
  {
    #region Methods and constructors
    public DatePicker()
    {
      Contract.Ensures(this.DisplayDate.Kind == ((System.DateTimeKind)(2)));
    }

    public override void OnApplyTemplate()
    {
    }

    protected virtual new void OnCalendarClosed(System.Windows.RoutedEventArgs e)
    {
    }

    protected virtual new void OnCalendarOpened(System.Windows.RoutedEventArgs e)
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected virtual new void OnDateValidationError(DatePickerDateValidationErrorEventArgs e)
    {
    }

    protected virtual new void OnSelectedDateChanged(SelectionChangedEventArgs e)
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

    public System.Windows.Style CalendarStyle
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

    public bool IsDropDownOpen
    {
      get
      {
        return default(bool);
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

    public DatePickerFormat SelectedDateFormat
    {
      get
      {
        return default(DatePickerFormat);
      }
      set
      {
      }
    }

    public string Text
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event System.Windows.RoutedEventHandler CalendarClosed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.RoutedEventHandler CalendarOpened
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<DatePickerDateValidationErrorEventArgs> DateValidationError
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<SelectionChangedEventArgs> SelectedDateChanged
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
    public readonly static System.Windows.DependencyProperty CalendarStyleProperty;
    public readonly static System.Windows.DependencyProperty DisplayDateEndProperty;
    public readonly static System.Windows.DependencyProperty DisplayDateProperty;
    public readonly static System.Windows.DependencyProperty DisplayDateStartProperty;
    public readonly static System.Windows.DependencyProperty FirstDayOfWeekProperty;
    public readonly static System.Windows.DependencyProperty IsDropDownOpenProperty;
    public readonly static System.Windows.DependencyProperty IsTodayHighlightedProperty;
    public readonly static System.Windows.RoutedEvent SelectedDateChangedEvent;
    public readonly static System.Windows.DependencyProperty SelectedDateFormatProperty;
    public readonly static System.Windows.DependencyProperty SelectedDateProperty;
    public readonly static System.Windows.DependencyProperty TextProperty;
    #endregion
  }
}
