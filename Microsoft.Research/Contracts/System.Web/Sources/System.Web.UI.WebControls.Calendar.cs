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

// File System.Web.UI.WebControls.Calendar.cs
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


namespace System.Web.UI.WebControls
{
  public partial class Calendar : WebControl, System.Web.UI.IPostBackEventHandler
  {
    #region Methods and constructors
    public Calendar()
    {
    }

    protected override System.Web.UI.ControlCollection CreateControlCollection()
    {
      return default(System.Web.UI.ControlCollection);
    }

    protected bool HasWeekSelectors(CalendarSelectionMode selectionMode)
    {
      return default(bool);
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected virtual new void OnDayRender(TableCell cell, CalendarDay day)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected virtual new void OnSelectionChanged()
    {
    }

    protected virtual new void OnVisibleMonthChanged(DateTime newDate, DateTime previousDate)
    {
    }

    protected virtual new void RaisePostBackEvent(string eventArgument)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    void System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
    {
    }

    protected override void TrackViewState()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new string Caption
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new TableCaptionAlign CaptionAlign
    {
      get
      {
        return default(TableCaptionAlign);
      }
      set
      {
      }
    }

    public int CellPadding
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int CellSpacing
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public TableItemStyle DayHeaderStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public DayNameFormat DayNameFormat
    {
      get
      {
        return default(DayNameFormat);
      }
      set
      {
      }
    }

    public TableItemStyle DayStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public FirstDayOfWeek FirstDayOfWeek
    {
      get
      {
        return default(FirstDayOfWeek);
      }
      set
      {
      }
    }

    public string NextMonthText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public NextPrevFormat NextPrevFormat
    {
      get
      {
        return default(NextPrevFormat);
      }
      set
      {
      }
    }

    public TableItemStyle NextPrevStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public TableItemStyle OtherMonthDayStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public string PrevMonthText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public DateTime SelectedDate
    {
      get
      {
        return default(DateTime);
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

    public TableItemStyle SelectedDayStyle
    {
      get
      {
        return default(TableItemStyle);
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

    public string SelectMonthText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TableItemStyle SelectorStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public string SelectWeekText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool ShowDayHeader
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ShowGridLines
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ShowNextPrevMonth
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ShowTitle
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool SupportsDisabledAttribute
    {
      get
      {
        return default(bool);
      }
    }

    public TitleFormat TitleFormat
    {
      get
      {
        return default(TitleFormat);
      }
      set
      {
      }
    }

    public TableItemStyle TitleStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public TableItemStyle TodayDayStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public DateTime TodaysDate
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public virtual new bool UseAccessibleHeader
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public DateTime VisibleDate
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public TableItemStyle WeekendDayStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }
    #endregion

    #region Events
    public event DayRenderEventHandler DayRender
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler SelectionChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event MonthChangedEventHandler VisibleMonthChanged
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
