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

// File System.Windows.Documents.SpellerInterop.ITextContext.cs
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


namespace System.Windows.Documents
{
  internal partial class SpellerInterop
  {
    private partial interface ITextContext
    {
      #region Methods and constructors
      void AddLexicon(System.Windows.Documents.SpellerInterop.ILexicon lexicon);

      void get_LexiconCount(out int lexiconCount);

      void RemoveLexicon(System.Windows.Documents.SpellerInterop.ILexicon lexicon);

      void stub_AddDefaultDialect();

      void stub_get_DefaultDialect();

      void stub_get_DefaultDialectCount();

      void stub_get_IgnorePunctuation();

      void stub_get_IsCaching();

      void stub_get_IsComputingBases();

      void stub_get_IsComputingCompounds();

      void stub_get_IsComputingExpansions();

      void stub_get_IsComputingInflections();

      void stub_get_IsComputingLemmas();

      void stub_get_IsComputingPartOfSpeechTags();

      void stub_get_IsFindingDateTimeMeasures();

      void stub_get_IsFindingDefinitions();

      void stub_get_IsFindingLocations();

      void stub_get_IsFindingOrganizations();

      void stub_get_IsFindingPersons();

      void stub_get_IsFindingPhrases();

      void stub_get_IsShowingCharacterNormalizations();

      void stub_get_IsShowingGaps();

      void stub_get_IsShowingWordNormalizations();

      void stub_get_IsSimpleWordBreaking();

      void stub_get_IsSingleLanguage();

      void stub_get_Lexicons();

      void stub_get_MaxSentences();

      void stub_get_Property();

      void stub_get_PropertyCount();

      void stub_get_ResourceLoader();

      void stub_get_UseRelativeTimes();

      void stub_get_Version();

      void stub_put_IgnorePunctuation();

      void stub_put_IsCaching();

      void stub_put_IsComputingBases();

      void stub_put_IsComputingCompounds();

      void stub_put_IsComputingExpansions();

      void stub_put_IsComputingInflections();

      void stub_put_IsComputingLemmas();

      void stub_put_IsComputingPartOfSpeechTags();

      void stub_put_IsFindingDateTimeMeasures();

      void stub_put_IsFindingDefinitions();

      void stub_put_IsFindingLocations();

      void stub_put_IsFindingOrganizations();

      void stub_put_IsFindingPersons();

      void stub_put_IsFindingPhrases();

      void stub_put_IsShowingCharacterNormalizations();

      void stub_put_IsShowingGaps();

      void stub_put_IsShowingWordNormalizations();

      void stub_put_IsSimpleWordBreaking();

      void stub_put_IsSingleLanguage();

      void stub_put_Lexicons();

      void stub_put_MaxSentences();

      void stub_put_Property();

      void stub_put_ResourceLoader();

      void stub_put_UseRelativeTimes();

      void stub_RemoveDefaultDialect();
      #endregion
    }
  }
}
