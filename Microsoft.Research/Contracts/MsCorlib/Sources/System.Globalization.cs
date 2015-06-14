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

// File System.Globalization.cs
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


namespace System.Globalization
{
  public enum CalendarAlgorithmType
  {
    Unknown = 0, 
    SolarCalendar = 1, 
    LunarCalendar = 2, 
    LunisolarCalendar = 3, 
  }

  public enum CalendarWeekRule
  {
    FirstDay = 0, 
    FirstFullWeek = 1, 
    FirstFourDayWeek = 2, 
  }

  public enum CompareOptions
  {
    None = 0, 
    IgnoreCase = 1, 
    IgnoreNonSpace = 2, 
    IgnoreSymbols = 4, 
    IgnoreKanaType = 8, 
    IgnoreWidth = 16, 
    OrdinalIgnoreCase = 268435456, 
    StringSort = 536870912, 
    Ordinal = 1073741824, 
  }

  public enum CultureTypes
  {
    NeutralCultures = 1, 
    SpecificCultures = 2, 
    InstalledWin32Cultures = 4, 
    AllCultures = 7, 
    UserCustomCulture = 8, 
    ReplacementCultures = 16, 
    WindowsOnlyCultures = 32, 
    FrameworkCultures = 64, 
  }

  public enum DateTimeStyles
  {
    None = 0, 
    AllowLeadingWhite = 1, 
    AllowTrailingWhite = 2, 
    AllowInnerWhite = 4, 
    AllowWhiteSpaces = 7, 
    NoCurrentDateDefault = 8, 
    AdjustToUniversal = 16, 
    AssumeLocal = 32, 
    AssumeUniversal = 64, 
    RoundtripKind = 128, 
  }

  public enum DigitShapes
  {
    Context = 0, 
    None = 1, 
    NativeNational = 2, 
  }

  public enum GregorianCalendarTypes
  {
    Localized = 1, 
    USEnglish = 2, 
    MiddleEastFrench = 9, 
    Arabic = 10, 
    TransliteratedEnglish = 11, 
    TransliteratedFrench = 12, 
  }

  public enum NumberStyles
  {
    None = 0, 
    AllowLeadingWhite = 1, 
    AllowTrailingWhite = 2, 
    AllowLeadingSign = 4, 
    AllowTrailingSign = 8, 
    AllowParentheses = 16, 
    AllowDecimalPoint = 32, 
    AllowThousands = 64, 
    AllowExponent = 128, 
    AllowCurrencySymbol = 256, 
    AllowHexSpecifier = 512, 
    Integer = 7, 
    HexNumber = 515, 
    Number = 111, 
    Float = 167, 
    Currency = 383, 
    Any = 511, 
  }

  public enum TimeSpanStyles
  {
    None = 0, 
    AssumeNegative = 1, 
  }

  public enum UnicodeCategory
  {
    UppercaseLetter = 0, 
    LowercaseLetter = 1, 
    TitlecaseLetter = 2, 
    ModifierLetter = 3, 
    OtherLetter = 4, 
    NonSpacingMark = 5, 
    SpacingCombiningMark = 6, 
    EnclosingMark = 7, 
    DecimalDigitNumber = 8, 
    LetterNumber = 9, 
    OtherNumber = 10, 
    SpaceSeparator = 11, 
    LineSeparator = 12, 
    ParagraphSeparator = 13, 
    Control = 14, 
    Format = 15, 
    Surrogate = 16, 
    PrivateUse = 17, 
    ConnectorPunctuation = 18, 
    DashPunctuation = 19, 
    OpenPunctuation = 20, 
    ClosePunctuation = 21, 
    InitialQuotePunctuation = 22, 
    FinalQuotePunctuation = 23, 
    OtherPunctuation = 24, 
    MathSymbol = 25, 
    CurrencySymbol = 26, 
    ModifierSymbol = 27, 
    OtherSymbol = 28, 
    OtherNotAssigned = 29, 
  }
}
