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

// File System.Windows.Media.TextFormatting.TextRunTypographyProperties.cs
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


namespace System.Windows.Media.TextFormatting
{
  abstract public partial class TextRunTypographyProperties
  {
    #region Methods and constructors
    protected void OnPropertiesChanged()
    {
    }

    protected TextRunTypographyProperties()
    {
    }
    #endregion

    #region Properties and indexers
    public abstract int AnnotationAlternates
    {
      get;
    }

    public abstract System.Windows.FontCapitals Capitals
    {
      get;
    }

    public abstract bool CapitalSpacing
    {
      get;
    }

    public abstract bool CaseSensitiveForms
    {
      get;
    }

    public abstract bool ContextualAlternates
    {
      get;
    }

    public abstract bool ContextualLigatures
    {
      get;
    }

    public abstract int ContextualSwashes
    {
      get;
    }

    public abstract bool DiscretionaryLigatures
    {
      get;
    }

    public abstract bool EastAsianExpertForms
    {
      get;
    }

    public abstract System.Windows.FontEastAsianLanguage EastAsianLanguage
    {
      get;
    }

    public abstract System.Windows.FontEastAsianWidths EastAsianWidths
    {
      get;
    }

    public abstract System.Windows.FontFraction Fraction
    {
      get;
    }

    public abstract bool HistoricalForms
    {
      get;
    }

    public abstract bool HistoricalLigatures
    {
      get;
    }

    public abstract bool Kerning
    {
      get;
    }

    public abstract bool MathematicalGreek
    {
      get;
    }

    public abstract System.Windows.FontNumeralAlignment NumeralAlignment
    {
      get;
    }

    public abstract System.Windows.FontNumeralStyle NumeralStyle
    {
      get;
    }

    public abstract bool SlashedZero
    {
      get;
    }

    public abstract bool StandardLigatures
    {
      get;
    }

    public abstract int StandardSwashes
    {
      get;
    }

    public abstract int StylisticAlternates
    {
      get;
    }

    public abstract bool StylisticSet1
    {
      get;
    }

    public abstract bool StylisticSet10
    {
      get;
    }

    public abstract bool StylisticSet11
    {
      get;
    }

    public abstract bool StylisticSet12
    {
      get;
    }

    public abstract bool StylisticSet13
    {
      get;
    }

    public abstract bool StylisticSet14
    {
      get;
    }

    public abstract bool StylisticSet15
    {
      get;
    }

    public abstract bool StylisticSet16
    {
      get;
    }

    public abstract bool StylisticSet17
    {
      get;
    }

    public abstract bool StylisticSet18
    {
      get;
    }

    public abstract bool StylisticSet19
    {
      get;
    }

    public abstract bool StylisticSet2
    {
      get;
    }

    public abstract bool StylisticSet20
    {
      get;
    }

    public abstract bool StylisticSet3
    {
      get;
    }

    public abstract bool StylisticSet4
    {
      get;
    }

    public abstract bool StylisticSet5
    {
      get;
    }

    public abstract bool StylisticSet6
    {
      get;
    }

    public abstract bool StylisticSet7
    {
      get;
    }

    public abstract bool StylisticSet8
    {
      get;
    }

    public abstract bool StylisticSet9
    {
      get;
    }

    public abstract System.Windows.FontVariants Variants
    {
      get;
    }
    #endregion
  }
}
