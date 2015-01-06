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

// File System.Windows.Documents.SpellerInterop.ITextChunk.cs
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
    private partial interface ITextChunk
    {
      #region Methods and constructors
      void get_Count(out int val);

      void get_ReuseObjects(out bool val);

      void put_Context(System.Windows.Documents.SpellerInterop.ITextContext val);

      void put_Locale(int val);

      void put_ReuseObjects(bool val);

      void SetInputArray(IntPtr inputArray, int size);

      void stub_get__NewEnum();

      void stub_get_InputArray();

      void stub_get_InputArrayRange();

      void stub_get_InputText();

      void stub_get_IsEndOfDocument();

      void stub_get_IsLocaleReliable();

      void stub_get_Locale();

      void stub_get_Property();

      void stub_get_PropertyCount();

      void stub_ProcessStream();

      void stub_put_InputArrayRange();

      void stub_put_InputText();

      void stub_put_IsEndOfDocument();

      void stub_put_IsLocaleReliable();

      void stub_put_Property();

      void stub_RegisterEngine();

      void stub_ToString();

      void stub_UnregisterEngine();
      #endregion
    }
  }
}
