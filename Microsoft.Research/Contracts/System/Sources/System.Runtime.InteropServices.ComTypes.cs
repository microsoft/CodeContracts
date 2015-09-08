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

// File System.Runtime.InteropServices.ComTypes.cs
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


namespace System.Runtime.InteropServices.ComTypes
{
  public enum ADVF
  {
    ADVF_NODATA = 1, 
    ADVF_PRIMEFIRST = 2, 
    ADVF_ONLYONCE = 4, 
    ADVF_DATAONSTOP = 64, 
    ADVFCACHE_NOHANDLER = 8, 
    ADVFCACHE_FORCEBUILTIN = 16, 
    ADVFCACHE_ONSAVE = 32, 
  }

  public enum DATADIR
  {
    DATADIR_GET = 1, 
    DATADIR_SET = 2, 
  }

  public enum DVASPECT
  {
    DVASPECT_CONTENT = 1, 
    DVASPECT_THUMBNAIL = 2, 
    DVASPECT_ICON = 4, 
    DVASPECT_DOCPRINT = 8, 
  }

  public enum TYMED
  {
    TYMED_HGLOBAL = 1, 
    TYMED_FILE = 2, 
    TYMED_ISTREAM = 4, 
    TYMED_ISTORAGE = 8, 
    TYMED_GDI = 16, 
    TYMED_MFPICT = 32, 
    TYMED_ENHMF = 64, 
    TYMED_NULL = 0, 
  }
}
