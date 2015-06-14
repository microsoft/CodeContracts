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

// File System.Reflection.cs
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


namespace System.Reflection
{
  public enum AssemblyNameFlags
  {
    None = 0, 
    PublicKey = 1, 
    EnableJITcompileOptimizer = 16384, 
    EnableJITcompileTracking = 32768, 
    Retargetable = 256, 
  }

  public enum BindingFlags
  {
    Default = 0, 
    IgnoreCase = 1, 
    DeclaredOnly = 2, 
    Instance = 4, 
    Static = 8, 
    Public = 16, 
    NonPublic = 32, 
    FlattenHierarchy = 64, 
    InvokeMethod = 256, 
    CreateInstance = 512, 
    GetField = 1024, 
    SetField = 2048, 
    GetProperty = 4096, 
    SetProperty = 8192, 
    PutDispProperty = 16384, 
    PutRefDispProperty = 32768, 
    ExactBinding = 65536, 
    SuppressChangeType = 131072, 
    OptionalParamBinding = 262144, 
    IgnoreReturn = 16777216, 
  }

  public enum CallingConventions
  {
    Standard = 1, 
    VarArgs = 2, 
    Any = 3, 
    HasThis = 32, 
    ExplicitThis = 64, 
  }

  public enum EventAttributes
  {
    None = 0, 
    SpecialName = 512, 
    ReservedMask = 1024, 
    RTSpecialName = 1024, 
  }

  public enum ExceptionHandlingClauseOptions
  {
    Clause = 0, 
    Filter = 1, 
    Finally = 2, 
    Fault = 4, 
  }

  public enum FieldAttributes
  {
    FieldAccessMask = 7, 
    PrivateScope = 0, 
    Private = 1, 
    FamANDAssem = 2, 
    Assembly = 3, 
    Family = 4, 
    FamORAssem = 5, 
    Public = 6, 
    Static = 16, 
    InitOnly = 32, 
    Literal = 64, 
    NotSerialized = 128, 
    SpecialName = 512, 
    PinvokeImpl = 8192, 
    ReservedMask = 38144, 
    RTSpecialName = 1024, 
    HasFieldMarshal = 4096, 
    HasDefault = 32768, 
    HasFieldRVA = 256, 
  }

  public enum GenericParameterAttributes
  {
    None = 0, 
    VarianceMask = 3, 
    Covariant = 1, 
    Contravariant = 2, 
    SpecialConstraintMask = 28, 
    ReferenceTypeConstraint = 4, 
    NotNullableValueTypeConstraint = 8, 
    DefaultConstructorConstraint = 16, 
  }

  public enum ImageFileMachine
  {
    I386 = 332, 
    IA64 = 512, 
    AMD64 = 34404, 
  }

  public delegate bool MemberFilter(MemberInfo m, Object filterCriteria);

  public enum MemberTypes
  {
    Constructor = 1, 
    Event = 2, 
    Field = 4, 
    Method = 8, 
    Property = 16, 
    TypeInfo = 32, 
    Custom = 64, 
    NestedType = 128, 
    All = 191, 
  }

  public enum MethodAttributes
  {
    MemberAccessMask = 7, 
    PrivateScope = 0, 
    Private = 1, 
    FamANDAssem = 2, 
    Assembly = 3, 
    Family = 4, 
    FamORAssem = 5, 
    Public = 6, 
    Static = 16, 
    Final = 32, 
    Virtual = 64, 
    HideBySig = 128, 
    CheckAccessOnOverride = 512, 
    VtableLayoutMask = 256, 
    ReuseSlot = 0, 
    NewSlot = 256, 
    Abstract = 1024, 
    SpecialName = 2048, 
    PinvokeImpl = 8192, 
    UnmanagedExport = 8, 
    RTSpecialName = 4096, 
    ReservedMask = 53248, 
    HasSecurity = 16384, 
    RequireSecObject = 32768, 
  }

  public enum MethodImplAttributes
  {
    CodeTypeMask = 3, 
    IL = 0, 
    Native = 1, 
    OPTIL = 2, 
    Runtime = 3, 
    ManagedMask = 4, 
    Unmanaged = 4, 
    Managed = 0, 
    ForwardRef = 16, 
    PreserveSig = 128, 
    InternalCall = 4096, 
    Synchronized = 32, 
    NoInlining = 8, 
    NoOptimization = 64, 
    MaxMethodImplVal = 65535, 
  }

  public delegate Module ModuleResolveEventHandler(Object sender, ResolveEventArgs e);

  public enum ParameterAttributes
  {
    None = 0, 
    In = 1, 
    Out = 2, 
    Lcid = 4, 
    Retval = 8, 
    Optional = 16, 
    ReservedMask = 61440, 
    HasDefault = 4096, 
    HasFieldMarshal = 8192, 
    Reserved3 = 16384, 
    Reserved4 = 32768, 
  }

  public enum PortableExecutableKinds
  {
    NotAPortableExecutableImage = 0, 
    ILOnly = 1, 
    Required32Bit = 2, 
    PE32Plus = 4, 
    Unmanaged32Bit = 8, 
  }

  public enum ProcessorArchitecture
  {
    None = 0, 
    MSIL = 1, 
    X86 = 2, 
    IA64 = 3, 
    Amd64 = 4, 
  }

  public enum PropertyAttributes
  {
    None = 0, 
    SpecialName = 512, 
    ReservedMask = 62464, 
    RTSpecialName = 1024, 
    HasDefault = 4096, 
    Reserved2 = 8192, 
    Reserved3 = 16384, 
    Reserved4 = 32768, 
  }

  public enum ResourceAttributes
  {
    Public = 1, 
    Private = 2, 
  }

  public enum ResourceLocation
  {
    Embedded = 1, 
    ContainedInAnotherAssembly = 2, 
    ContainedInManifestFile = 4, 
  }

  public enum TypeAttributes
  {
    VisibilityMask = 7, 
    NotPublic = 0, 
    Public = 1, 
    NestedPublic = 2, 
    NestedPrivate = 3, 
    NestedFamily = 4, 
    NestedAssembly = 5, 
    NestedFamANDAssem = 6, 
    NestedFamORAssem = 7, 
    LayoutMask = 24, 
    AutoLayout = 0, 
    SequentialLayout = 8, 
    ExplicitLayout = 16, 
    ClassSemanticsMask = 32, 
    Class = 0, 
    Interface = 32, 
    Abstract = 128, 
    Sealed = 256, 
    SpecialName = 1024, 
    Import = 4096, 
    Serializable = 8192, 
    StringFormatMask = 196608, 
    AnsiClass = 0, 
    UnicodeClass = 65536, 
    AutoClass = 131072, 
    CustomFormatClass = 196608, 
    CustomFormatMask = 12582912, 
    BeforeFieldInit = 1048576, 
    ReservedMask = 264192, 
    RTSpecialName = 2048, 
    HasSecurity = 262144, 
  }

  public delegate bool TypeFilter(Type m, Object filterCriteria);
}
