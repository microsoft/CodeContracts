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

// File System.Runtime.InteropServices.cs
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


namespace System.Runtime.InteropServices
{
  public enum AssemblyRegistrationFlags
  {
    None = 0, 
    SetCodeBase = 1, 
  }

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

  public enum CallingConvention
  {
    Winapi = 1, 
    Cdecl = 2, 
    StdCall = 3, 
    ThisCall = 4, 
    FastCall = 5, 
  }

  public enum CharSet
  {
    None = 1, 
    Ansi = 2, 
    Unicode = 3, 
    Auto = 4, 
  }

  public enum ClassInterfaceType
  {
    None = 0, 
    AutoDispatch = 1, 
    AutoDual = 2, 
  }

  public enum ComInterfaceType
  {
    InterfaceIsDual = 0, 
    InterfaceIsIUnknown = 1, 
    InterfaceIsIDispatch = 2, 
  }

  public enum ComMemberType
  {
    Method = 0, 
    PropGet = 1, 
    PropSet = 2, 
  }

  public enum CustomQueryInterfaceMode
  {
    Ignore = 0, 
    Allow = 1, 
  }

  public enum CustomQueryInterfaceResult
  {
    Handled = 0, 
    NotHandled = 1, 
    Failed = 2, 
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

  public enum ExporterEventKind
  {
    NOTIF_TYPECONVERTED = 0, 
    NOTIF_CONVERTWARNING = 1, 
    ERROR_REFTOINVALIDASSEMBLY = 2, 
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

  public enum GCHandleType
  {
    Weak = 0, 
    WeakTrackResurrection = 1, 
    Normal = 2, 
    Pinned = 3, 
  }

  public enum IDispatchImplType
  {
    SystemDefinedImpl = 0, 
    InternalImpl = 1, 
    CompatibleImpl = 2, 
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

  public enum ImporterEventKind
  {
    NOTIF_TYPECONVERTED = 0, 
    NOTIF_CONVERTWARNING = 1, 
    ERROR_REFTOINVALIDTYPELIB = 2, 
  }

  public enum INVOKEKIND
  {
    INVOKE_FUNC = 1, 
    INVOKE_PROPERTYGET = 2, 
    INVOKE_PROPERTYPUT = 4, 
    INVOKE_PROPERTYPUTREF = 8, 
  }

  public enum LayoutKind
  {
    Sequential = 0, 
    Explicit = 2, 
    Auto = 3, 
  }

  public enum LIBFLAGS : short
  {
    LIBFLAG_FRESTRICTED = 1, 
    LIBFLAG_FCONTROL = 2, 
    LIBFLAG_FHIDDEN = 4, 
    LIBFLAG_FHASDISKIMAGE = 8, 
  }

  public delegate IntPtr ObjectCreationDelegate(IntPtr aggregator);

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

  public enum RegistrationClassContext
  {
    InProcessServer = 1, 
    InProcessHandler = 2, 
    LocalServer = 4, 
    InProcessServer16 = 8, 
    RemoteServer = 16, 
    InProcessHandler16 = 32, 
    Reserved1 = 64, 
    Reserved2 = 128, 
    Reserved3 = 256, 
    Reserved4 = 512, 
    NoCodeDownload = 1024, 
    Reserved5 = 2048, 
    NoCustomMarshal = 4096, 
    EnableCodeDownload = 8192, 
    NoFailureLog = 16384, 
    DisableActivateAsActivator = 32768, 
    EnableActivateAsActivator = 65536, 
    FromDefaultContext = 131072, 
  }

  public enum RegistrationConnectionType
  {
    SingleUse = 0, 
    MultipleUse = 1, 
    MultiSeparate = 2, 
    Suspended = 4, 
    Surrogate = 8, 
  }

  public enum SYSKIND
  {
    SYS_WIN16 = 0, 
    SYS_WIN32 = 1, 
    SYS_MAC = 2, 
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

  public enum TypeLibExporterFlags
  {
    None = 0, 
    OnlyReferenceRegistered = 1, 
    CallerResolvedReferences = 2, 
    OldNames = 4, 
    ExportAs32Bit = 16, 
    ExportAs64Bit = 32, 
  }

  public enum TypeLibFuncFlags
  {
    FRestricted = 1, 
    FSource = 2, 
    FBindable = 4, 
    FRequestEdit = 8, 
    FDisplayBind = 16, 
    FDefaultBind = 32, 
    FHidden = 64, 
    FUsesGetLastError = 128, 
    FDefaultCollelem = 256, 
    FUiDefault = 512, 
    FNonBrowsable = 1024, 
    FReplaceable = 2048, 
    FImmediateBind = 4096, 
  }

  public enum TypeLibImporterFlags
  {
    None = 0, 
    PrimaryInteropAssembly = 1, 
    UnsafeInterfaces = 2, 
    SafeArrayAsSystemArray = 4, 
    TransformDispRetVals = 8, 
    PreventClassMembers = 16, 
    SerializableValueClasses = 32, 
    ImportAsX86 = 256, 
    ImportAsX64 = 512, 
    ImportAsItanium = 1024, 
    ImportAsAgnostic = 2048, 
    ReflectionOnlyLoading = 4096, 
    NoDefineVersionResource = 8192, 
  }

  public enum TypeLibTypeFlags
  {
    FAppObject = 1, 
    FCanCreate = 2, 
    FLicensed = 4, 
    FPreDeclId = 8, 
    FHidden = 16, 
    FControl = 32, 
    FDual = 64, 
    FNonExtensible = 128, 
    FOleAutomation = 256, 
    FRestricted = 512, 
    FAggregatable = 1024, 
    FReplaceable = 2048, 
    FDispatchable = 4096, 
    FReverseBind = 8192, 
  }

  public enum TypeLibVarFlags
  {
    FReadOnly = 1, 
    FSource = 2, 
    FBindable = 4, 
    FRequestEdit = 8, 
    FDisplayBind = 16, 
    FDefaultBind = 32, 
    FHidden = 64, 
    FRestricted = 128, 
    FDefaultCollelem = 256, 
    FUiDefault = 512, 
    FNonBrowsable = 1024, 
    FReplaceable = 2048, 
    FImmediateBind = 4096, 
  }

  public enum UnmanagedType
  {
    Bool = 2, 
    I1 = 3, 
    U1 = 4, 
    I2 = 5, 
    U2 = 6, 
    I4 = 7, 
    U4 = 8, 
    I8 = 9, 
    U8 = 10, 
    R4 = 11, 
    R8 = 12, 
    Currency = 15, 
    BStr = 19, 
    LPStr = 20, 
    LPWStr = 21, 
    LPTStr = 22, 
    ByValTStr = 23, 
    IUnknown = 25, 
    IDispatch = 26, 
    Struct = 27, 
    Interface = 28, 
    SafeArray = 29, 
    ByValArray = 30, 
    SysInt = 31, 
    SysUInt = 32, 
    VBByRefStr = 34, 
    AnsiBStr = 35, 
    TBStr = 36, 
    VariantBool = 37, 
    FunctionPtr = 38, 
    AsAny = 40, 
    LPArray = 42, 
    LPStruct = 43, 
    CustomMarshaler = 44, 
    Error = 45, 
  }

  public enum VarEnum
  {
    VT_EMPTY = 0, 
    VT_NULL = 1, 
    VT_I2 = 2, 
    VT_I4 = 3, 
    VT_R4 = 4, 
    VT_R8 = 5, 
    VT_CY = 6, 
    VT_DATE = 7, 
    VT_BSTR = 8, 
    VT_DISPATCH = 9, 
    VT_ERROR = 10, 
    VT_BOOL = 11, 
    VT_VARIANT = 12, 
    VT_UNKNOWN = 13, 
    VT_DECIMAL = 14, 
    VT_I1 = 16, 
    VT_UI1 = 17, 
    VT_UI2 = 18, 
    VT_UI4 = 19, 
    VT_I8 = 20, 
    VT_UI8 = 21, 
    VT_INT = 22, 
    VT_UINT = 23, 
    VT_VOID = 24, 
    VT_HRESULT = 25, 
    VT_PTR = 26, 
    VT_SAFEARRAY = 27, 
    VT_CARRAY = 28, 
    VT_USERDEFINED = 29, 
    VT_LPSTR = 30, 
    VT_LPWSTR = 31, 
    VT_RECORD = 36, 
    VT_FILETIME = 64, 
    VT_BLOB = 65, 
    VT_STREAM = 66, 
    VT_STORAGE = 67, 
    VT_STREAMED_OBJECT = 68, 
    VT_STORED_OBJECT = 69, 
    VT_BLOB_OBJECT = 70, 
    VT_CF = 71, 
    VT_CLSID = 72, 
    VT_VECTOR = 4096, 
    VT_ARRAY = 8192, 
    VT_BYREF = 16384, 
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
}
