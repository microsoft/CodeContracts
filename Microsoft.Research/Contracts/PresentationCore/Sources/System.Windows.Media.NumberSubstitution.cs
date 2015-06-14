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

// File System.Windows.Media.NumberSubstitution.cs
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


namespace System.Windows.Media
{
  public partial class NumberSubstitution
  {
    #region Methods and constructors
    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public static System.Globalization.CultureInfo GetCultureOverride(System.Windows.DependencyObject target)
    {
      return default(System.Globalization.CultureInfo);
    }

    public static NumberCultureSource GetCultureSource(System.Windows.DependencyObject target)
    {
      return default(NumberCultureSource);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static NumberSubstitutionMethod GetSubstitution(System.Windows.DependencyObject target)
    {
      return default(NumberSubstitutionMethod);
    }

    public NumberSubstitution(NumberCultureSource source, System.Globalization.CultureInfo cultureOverride, NumberSubstitutionMethod substitution)
    {
    }

    public NumberSubstitution()
    {
    }

    public static void SetCultureOverride(System.Windows.DependencyObject target, System.Globalization.CultureInfo value)
    {
    }

    public static void SetCultureSource(System.Windows.DependencyObject target, NumberCultureSource value)
    {
    }

    public static void SetSubstitution(System.Windows.DependencyObject target, NumberSubstitutionMethod value)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Globalization.CultureInfo CultureOverride
    {
      get
      {
        return default(System.Globalization.CultureInfo);
      }
      set
      {
      }
    }

    public NumberCultureSource CultureSource
    {
      get
      {
        return default(NumberCultureSource);
      }
      set
      {
      }
    }

    public NumberSubstitutionMethod Substitution
    {
      get
      {
        return default(NumberSubstitutionMethod);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty CultureOverrideProperty;
    public readonly static System.Windows.DependencyProperty CultureSourceProperty;
    public readonly static System.Windows.DependencyProperty SubstitutionProperty;
    #endregion
  }
}
