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

using System.Diagnostics.Contracts;
using System;

namespace System.Globalization
{

    public class GregorianCalendar
    {

        public GregorianCalendarTypes CalendarType
        {
          get;
          set;
        }

        public Int32[] Eras
        {
          get;
        }

        public int TwoDigitYearMax
        {
          get;
            set
            {
                Contract.Requires(value >= 100);
                Contract.Requires(value <= 9999);
            }
        }

        public int ToFourDigitYear (int year) {
            Contract.Requires(year <= 9999);

          return default(int);
        }
        public DateTime ToDateTime (int year, int month, int day, int hour, int minute, int second, int millisecond, int era) {
            Contract.Requires(era == 0 || era == 1);

          return default(DateTime);
        }
        public bool IsLeapYear (int year, int era) {
            Contract.Requires(era == 0 || era == 1);
            Contract.Requires(year >= 1);
            Contract.Requires(year <= 9999);

          return default(bool);
        }
        public bool IsLeapMonth (int year, int month, int era) {
            Contract.Requires(era == 0 || era == 1);
            Contract.Requires(year >= 1);
            Contract.Requires(year <= 9999);
            Contract.Requires(month >= 1);
            Contract.Requires(month <= 12);

          return default(bool);
        }
        public bool IsLeapDay (int year, int month, int day, int era) {
            Contract.Requires(era == 0 || era == 1);
            Contract.Requires(year >= 1);
            Contract.Requires(year <= 9999);
            Contract.Requires(month >= 1);
            Contract.Requires(month <= 12);
            Contract.Requires(day >= 1);

          return default(bool);
        }
        public int GetYear (DateTime time) {

          return default(int);
        }
        public int GetMonthsInYear (int year, int era) {
            Contract.Requires(era == 0 || era == 1);
            Contract.Requires(year >= 1);
            Contract.Requires(year <= 9999);

          return default(int);
        }
        public int GetMonth (DateTime time) {

          return default(int);
        }
        public int GetEra (DateTime time) {

          return default(int);
        }
        public int GetDaysInYear (int year, int era) {
            Contract.Requires(era == 0 || era == 1);
            Contract.Requires(year >= 1);
            Contract.Requires(year <= 9999);

          return default(int);
        }
        public int GetDaysInMonth (int year, int month, int era) {
            Contract.Requires(era == 0 || era == 1);
            Contract.Requires(month >= 1);
            Contract.Requires(month <= 12);

          return default(int);
        }
        public int GetDayOfYear (DateTime time) {

          return default(int);
        }
        public DayOfWeek GetDayOfWeek (DateTime time) {

          return default(DayOfWeek);
        }
        public int GetDayOfMonth (DateTime time) {

          return default(int);
        }
        public DateTime AddYears (DateTime time, int years) {

          return default(DateTime);
        }
        public DateTime AddWeeks (DateTime time, int weeks) {

          return default(DateTime);
        }
        public DateTime AddMonths (DateTime time, int months) {
            Contract.Requires(months >= -120000);
            Contract.Requires(months <= 120000);

          return default(DateTime);
        }
        public GregorianCalendar (GregorianCalendarTypes type) {

          return default(GregorianCalendar);
        }
        public GregorianCalendar () {
          return default(GregorianCalendar);
        }
    }
}
