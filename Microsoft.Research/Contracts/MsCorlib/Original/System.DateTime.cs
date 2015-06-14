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

namespace System
{

    public struct DateTime
    {

        public static DateTime Now
        {
          get;
        }

        public DayOfWeek DayOfWeek
        {
          get;
        }

        public int Second
        {
          get;
        }

        public static DateTime UtcNow
        {
          get;
        }

        public DateTime Date
        {
          get;
        }

        public int Hour
        {
          get;
        }

        public static DateTime Today
        {
          get;
        }

        public int Day
        {
          get;
        }

        public int Millisecond
        {
          get;
        }

        public int DayOfYear
        {
          get;
        }

        public int Year
        {
          get;
        }

        public int Minute
        {
          get;
        }

        public int Month
        {
          get;
        }

        public Int64 Ticks
        {
          get;
        }

        public TimeSpan TimeOfDay
        {
          get;
        }

        public TypeCode GetTypeCode () {

          return default(TypeCode);
        }
        public String[] GetDateTimeFormats (Char format, IFormatProvider provider) {

          return default(String[]);
        }
        public String[] GetDateTimeFormats (Char format) {

          return default(String[]);
        }
        public String[] GetDateTimeFormats (IFormatProvider provider) {

          return default(String[]);
        }
        public String[] GetDateTimeFormats () {

          return default(String[]);
        }
        public static bool operator >= (DateTime t1, DateTime t2) {

          return default(bool);
        }
        public static bool operator > (DateTime t1, DateTime t2) {

          return default(bool);
        }
        public static bool operator <= (DateTime t1, DateTime t2) {

          return default(bool);
        }
        public static bool operator < (DateTime t1, DateTime t2) {

          return default(bool);
        }
        public static bool operator != (DateTime d1, DateTime d2) {

          return default(bool);
        }
        public static bool operator == (DateTime d1, DateTime d2) {

          return default(bool);
        }
        public static TimeSpan operator - (DateTime d1, DateTime d2) {

          return default(TimeSpan);
        }
        public static DateTime operator - (DateTime d, TimeSpan t) {

          return default(DateTime);
        }
        public static DateTime operator + (DateTime d, TimeSpan t) {

          return default(DateTime);
        }
        public DateTime ToUniversalTime () {

          return default(DateTime);
        }
        public string ToString (string format, IFormatProvider provider) {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public string ToString (IFormatProvider provider) {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public string ToString (string format) {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public string ToString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public string ToShortTimeString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public string ToShortDateString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public string ToLongTimeString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public string ToLongDateString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public DateTime ToLocalTime () {

          return default(DateTime);
        }
        public Int64 ToFileTimeUtc () {

          return default(Int64);
        }
        public Int64 ToFileTime () {

          return default(Int64);
        }
        public double ToOADate () {

          return default(double);
        }
        public DateTime Subtract (TimeSpan value) {

          return default(DateTime);
        }
        public TimeSpan Subtract (DateTime value) {

          return default(TimeSpan);
        }
        public static DateTime ParseExact (string s, String[] formats, IFormatProvider provider, System.Globalization.DateTimeStyles style) {

          return default(DateTime);
        }
        public static DateTime ParseExact (string s, string format, IFormatProvider provider, System.Globalization.DateTimeStyles style) {

          return default(DateTime);
        }
        public static DateTime ParseExact (string s, string format, IFormatProvider provider) {

          return default(DateTime);
        }
        public static DateTime Parse (string s, IFormatProvider provider, System.Globalization.DateTimeStyles styles) {

          return default(DateTime);
        }
        public static DateTime Parse (string s, IFormatProvider provider) {

          return default(DateTime);
        }
        public static DateTime Parse (string s) {

          return default(DateTime);
        }
        public static bool IsLeapYear (int year) {

          return default(bool);
        }
        public int GetHashCode () {

          return default(int);
        }
        public static DateTime FromOADate (double d) {

          return default(DateTime);
        }
        public static DateTime FromFileTimeUtc (Int64 fileTime) {
            CodeContract.Requires(fileTime >= 0);

          return default(DateTime);
        }
        public static DateTime FromFileTime (Int64 fileTime) {

          return default(DateTime);
        }
        public static bool Equals (DateTime t1, DateTime t2) {

          return default(bool);
        }
        public bool Equals (object value) {

          return default(bool);
        }
        public static int DaysInMonth (int year, int month) {
            CodeContract.Requires(month >= 1);
            CodeContract.Requires(month <= 12);

          return default(int);
        }
        public int CompareTo (object value) {

          return default(int);
        }
        public static int Compare (DateTime t1, DateTime t2) {

          return default(int);
        }
        public DateTime AddYears (int value) {

          return default(DateTime);
        }
        public DateTime AddTicks (Int64 value) {

          return default(DateTime);
        }
        public DateTime AddSeconds (double value) {

          return default(DateTime);
        }
        public DateTime AddMonths (int months) {
            CodeContract.Requires(months >= -120000);
            CodeContract.Requires(months <= 120000);

          return default(DateTime);
        }
        public DateTime AddMinutes (double value) {

          return default(DateTime);
        }
        public DateTime AddMilliseconds (double value) {

          return default(DateTime);
        }
        public DateTime AddHours (double value) {

          return default(DateTime);
        }
        public DateTime AddDays (double value) {

          return default(DateTime);
        }
        public DateTime Add (TimeSpan value) {

          return default(DateTime);
        }
        public DateTime (int year, int month, int day, int hour, int minute, int second, int millisecond, System.Globalization.Calendar! calendar) {
            CodeContract.Requires(calendar != null);
            CodeContract.Requires(millisecond >= 0);
            CodeContract.Requires(millisecond < 1000);

          return default(DateTime);
        }
        public DateTime (int year, int month, int day, int hour, int minute, int second, int millisecond) {
            CodeContract.Requires(millisecond >= 0);
            CodeContract.Requires(millisecond < 1000);

          return default(DateTime);
        }
        public DateTime (int year, int month, int day, int hour, int minute, int second, System.Globalization.Calendar! calendar) {
            CodeContract.Requires(calendar != null);

          return default(DateTime);
        }
        public DateTime (int year, int month, int day, int hour, int minute, int second) {

          return default(DateTime);
        }
        public DateTime (int year, int month, int day, System.Globalization.Calendar calendar) {

          return default(DateTime);
        }
        public DateTime (int year, int month, int day) {

          return default(DateTime);
        }
        public DateTime (Int64 ticks) {
            CodeContract.Requires(ticks >= 0);
            CodeContract.Requires(ticks <= 4097261567);
          return default(DateTime);
        }
    }
}
