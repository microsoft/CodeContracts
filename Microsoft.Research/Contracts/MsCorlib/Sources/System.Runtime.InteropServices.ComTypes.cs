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
  public enum CALLCONV
  {
    CC_CDECL = 1, 
    CC_MSCPASCAL = 2, 
    CC_PASCAL = 2, 
    CC_MACPASCAL = 3, 
    CC_STDCALL = 4, 
    CC_RESERVED = 5, 
    CC_SYSCALL = 6, 
    CC_MPWCDECL = 7, 
    CC_MPWPASCAL = 8, 
    CC_MAX = 9, 
  }

  public enum DESCKIND
  {
    DESCKIND_NONE = 0, 
    DESCKIND_FUNCDESC = 1, 
    DESCKIND_VARDESC = 2, 
    DESCKIND_TYPECOMP = 3, 
    DESCKIND_IMPLICITAPPOBJ = 4, 
    DESCKIND_MAX = 5, 
  }

  public enum FUNCFLAGS : short
  {
    FUNCFLAG_FRESTRICTED = 1, 
    FUNCFLAG_FSOURCE = 2, 
    FUNCFLAG_FBINDABLE = 4, 
    FUNCFLAG_FREQUESTEDIT = 8, 
    FUNCFLAG_FDISPLAYBIND = 16, 
    FUNCFLAG_FDEFAULTBIND = 32, 
    FUNCFLAG_FHIDDEN = 64, 
    FUNCFLAG_FUSESGETLASTERROR = 128, 
    FUNCFLAG_FDEFAULTCOLLELEM = 256, 
    FUNCFLAG_FUIDEFAULT = 512, 
    FUNCFLAG_FNONBROWSABLE = 1024, 
    FUNCFLAG_FREPLACEABLE = 2048, 
    FUNCFLAG_FIMMEDIATEBIND = 4096, 
  }

  public enum FUNCKIND
  {
    FUNC_VIRTUAL = 0, 
    FUNC_PUREVIRTUAL = 1, 
    FUNC_NONVIRTUAL = 2, 
    FUNC_STATIC = 3, 
    FUNC_DISPATCH = 4, 
  }

  public enum IDLFLAG : short
  {
    IDLFLAG_NONE = 0, 
    IDLFLAG_FIN = 1, 
    IDLFLAG_FOUT = 2, 
    IDLFLAG_FLCID = 4, 
    IDLFLAG_FRETVAL = 8, 
  }

  public enum IMPLTYPEFLAGS
  {
    IMPLTYPEFLAG_FDEFAULT = 1, 
    IMPLTYPEFLAG_FSOURCE = 2, 
    IMPLTYPEFLAG_FRESTRICTED = 4, 
    IMPLTYPEFLAG_FDEFAULTVTABLE = 8, 
  }

  public enum INVOKEKIND
  {
    INVOKE_FUNC = 1, 
    INVOKE_PROPERTYGET = 2, 
    INVOKE_PROPERTYPUT = 4, 
    INVOKE_PROPERTYPUTREF = 8, 
  }

  public enum LIBFLAGS : short
  {
    LIBFLAG_FRESTRICTED = 1, 
    LIBFLAG_FCONTROL = 2, 
    LIBFLAG_FHIDDEN = 4, 
    LIBFLAG_FHASDISKIMAGE = 8, 
  }

  public enum PARAMFLAG : short
  {
    PARAMFLAG_NONE = 0, 
    PARAMFLAG_FIN = 1, 
    PARAMFLAG_FOUT = 2, 
    PARAMFLAG_FLCID = 4, 
    PARAMFLAG_FRETVAL = 8, 
    PARAMFLAG_FOPT = 16, 
    PARAMFLAG_FHASDEFAULT = 32, 
    PARAMFLAG_FHASCUSTDATA = 64, 
  }

  public enum SYSKIND
  {
    SYS_WIN16 = 0, 
    SYS_WIN32 = 1, 
    SYS_MAC = 2, 
    SYS_WIN64 = 3, 
  }

  public enum TYPEFLAGS : short
  {
    TYPEFLAG_FAPPOBJECT = 1, 
    TYPEFLAG_FCANCREATE = 2, 
    TYPEFLAG_FLICENSED = 4, 
    TYPEFLAG_FPREDECLID = 8, 
    TYPEFLAG_FHIDDEN = 16, 
    TYPEFLAG_FCONTROL = 32, 
    TYPEFLAG_FDUAL = 64, 
    TYPEFLAG_FNONEXTENSIBLE = 128, 
    TYPEFLAG_FOLEAUTOMATION = 256, 
    TYPEFLAG_FRESTRICTED = 512, 
    TYPEFLAG_FAGGREGATABLE = 1024, 
    TYPEFLAG_FREPLACEABLE = 2048, 
    TYPEFLAG_FDISPATCHABLE = 4096, 
    TYPEFLAG_FREVERSEBIND = 8192, 
    TYPEFLAG_FPROXY = 16384, 
  }

  public enum TYPEKIND
  {
    TKIND_ENUM = 0, 
    TKIND_RECORD = 1, 
    TKIND_MODULE = 2, 
    TKIND_INTERFACE = 3, 
    TKIND_DISPATCH = 4, 
    TKIND_COCLASS = 5, 
    TKIND_ALIAS = 6, 
    TKIND_UNION = 7, 
    TKIND_MAX = 8, 
  }

  public enum VARFLAGS : short
  {
    VARFLAG_FREADONLY = 1, 
    VARFLAG_FSOURCE = 2, 
    VARFLAG_FBINDABLE = 4, 
    VARFLAG_FREQUESTEDIT = 8, 
    VARFLAG_FDISPLAYBIND = 16, 
    VARFLAG_FDEFAULTBIND = 32, 
    VARFLAG_FHIDDEN = 64, 
    VARFLAG_FRESTRICTED = 128, 
    VARFLAG_FDEFAULTCOLLELEM = 256, 
    VARFLAG_FUIDEFAULT = 512, 
    VARFLAG_FNONBROWSABLE = 1024, 
    VARFLAG_FREPLACEABLE = 2048, 
    VARFLAG_FIMMEDIATEBIND = 4096, 
  }

  public enum VARKIND
  {
    VAR_PERINSTANCE = 0, 
    VAR_STATIC = 1, 
    VAR_CONST = 2, 
    VAR_DISPATCH = 3, 
  }
}
