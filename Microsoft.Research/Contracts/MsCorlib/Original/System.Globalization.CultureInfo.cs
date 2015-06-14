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
using System.Diagnostics.Contracts;

namespace System.Globalization
{

    public class CultureInfo
    {

        public static CultureInfo! CurrentUICulture
        {
          get;
        }

        public TextInfo! TextInfo
        {
          get;
        }

        public Calendar[]! OptionalCalendars
        {
          get;
        }

        public string! DisplayName
        {
          get;
        }

        public bool UseUserOverride
        {
          get;
        }

        public int LCID
        {
          get;
        }

        public NumberFormatInfo! NumberFormat
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string! ThreeLetterWindowsLanguageName
        {
          get;
        }

        public CompareInfo! CompareInfo
        {
          get;
        }

        public string! ThreeLetterISOLanguageName
        {
          get;
        }

        public string! NativeName
        {
          get;
        }

        public static CultureInfo! CurrentCulture
        {
          get;
        }

        public bool IsNeutralCulture
        {
          get;
        }

        public string! Name
        {
          get;
        }

        public Calendar! Calendar
        {
          get;
        }

        public string! TwoLetterISOLanguageName
        {
          get;
        }

        public static CultureInfo! InstalledUICulture
        {
          get;
        }

        public DateTimeFormatInfo! DateTimeFormat
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public CultureInfo Parent
        {
          get;
        }

        public string! EnglishName
        {
          get;
        }

        public static CultureInfo! InvariantCulture
        {
          get;
        }

        public bool IsReadOnly
        {
          get;
        }

        public static CultureInfo ReadOnly (CultureInfo! ci) {
            CodeContract.Requires(ci != null);

          return default(CultureInfo);
        }
        public object Clone () {

          return default(object);
        }
        public void ClearCachedData () {

        }
        public object GetFormat (Type formatType) {

          return default(object);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int GetHashCode () {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public bool Equals (object value) {

          return default(bool);
        }
        public static CultureInfo[] GetCultures (CultureTypes types) {

          return default(CultureInfo[]);
        }
        public static CultureInfo CreateSpecificCulture (string name) {

          return default(CultureInfo);
        }
        public CultureInfo (int culture, bool useUserOverride) {

          return default(CultureInfo);
        }
        public CultureInfo (int culture) {

          return default(CultureInfo);
        }
        public CultureInfo (string! name, bool useUserOverride) {
            CodeContract.Requires(name != null);

          return default(CultureInfo);
        }
        public CultureInfo (string name) {
          return default(CultureInfo);
        }
    }
}
