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

// File System.Reflection.Emit.ModuleBuilder.cs
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


namespace System.Reflection.Emit
{
  public partial class ModuleBuilder : System.Reflection.Module, System.Runtime.InteropServices._ModuleBuilder
  {
    #region Methods and constructors
    public void CreateGlobalFunctions()
    {
    }

    public System.Diagnostics.SymbolStore.ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
    {
      return default(System.Diagnostics.SymbolStore.ISymbolDocumentWriter);
    }

    public EnumBuilder DefineEnum(string name, System.Reflection.TypeAttributes visibility, Type underlyingType)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.EnumBuilder>() != null);

      return default(EnumBuilder);
    }

    public MethodBuilder DefineGlobalMethod(string name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public MethodBuilder DefineGlobalMethod(string name, System.Reflection.MethodAttributes attributes, Type returnType, Type[] parameterTypes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public MethodBuilder DefineGlobalMethod(string name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public FieldBuilder DefineInitializedData(string name, byte[] data, System.Reflection.FieldAttributes attributes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.FieldBuilder>() != null);

      return default(FieldBuilder);
    }

    public void DefineManifestResource(string name, Stream stream, System.Reflection.ResourceAttributes attribute)
    {
    }

    public MethodBuilder DefinePInvokeMethod(string name, string dllName, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes, System.Runtime.InteropServices.CallingConvention nativeCallConv, System.Runtime.InteropServices.CharSet nativeCharSet)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes, System.Runtime.InteropServices.CallingConvention nativeCallConv, System.Runtime.InteropServices.CharSet nativeCharSet)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public System.Resources.IResourceWriter DefineResource(string name, string description)
    {
      Contract.Ensures(Contract.Result<System.Resources.IResourceWriter>() != null);

      return default(System.Resources.IResourceWriter);
    }

    public System.Resources.IResourceWriter DefineResource(string name, string description, System.Reflection.ResourceAttributes attribute)
    {
      Contract.Ensures(Contract.Result<System.Resources.IResourceWriter>() != null);

      return default(System.Resources.IResourceWriter);
    }

    public TypeBuilder DefineType(string name, System.Reflection.TypeAttributes attr, Type parent, PackingSize packingSize, int typesize)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineType(string name)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineType(string name, System.Reflection.TypeAttributes attr, Type parent, int typesize)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineType(string name, System.Reflection.TypeAttributes attr)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineType(string name, System.Reflection.TypeAttributes attr, Type parent)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineType(string name, System.Reflection.TypeAttributes attr, Type parent, Type[] interfaces)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineType(string name, System.Reflection.TypeAttributes attr, Type parent, PackingSize packsize)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public FieldBuilder DefineUninitializedData(string name, int size, System.Reflection.FieldAttributes attributes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.FieldBuilder>() != null);

      return default(FieldBuilder);
    }

    public void DefineUnmanagedResource(string resourceFileName)
    {
    }

    public void DefineUnmanagedResource(byte[] resource)
    {
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public System.Reflection.MethodInfo GetArrayMethod(Type arrayClass, string methodName, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.MethodInfo>() != null);

      return default(System.Reflection.MethodInfo);
    }

    public MethodToken GetArrayMethodToken(Type arrayClass, string methodName, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      return default(MethodToken);
    }

    public MethodToken GetConstructorToken(System.Reflection.ConstructorInfo con)
    {
      return default(MethodToken);
    }

    public override Object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return default(Object[]);
    }

    public override Object[] GetCustomAttributes(bool inherit)
    {
      return default(Object[]);
    }

    public override IList<System.Reflection.CustomAttributeData> GetCustomAttributesData()
    {
      return default(IList<System.Reflection.CustomAttributeData>);
    }

    public override System.Reflection.FieldInfo GetField(string name, System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.FieldInfo);
    }

    public override System.Reflection.FieldInfo[] GetFields(System.Reflection.BindingFlags bindingFlags)
    {
      return default(System.Reflection.FieldInfo[]);
    }

    public FieldToken GetFieldToken(System.Reflection.FieldInfo field)
    {
      return default(FieldToken);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    protected override System.Reflection.MethodInfo GetMethodImpl(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.MethodInfo);
    }

    public override System.Reflection.MethodInfo[] GetMethods(System.Reflection.BindingFlags bindingFlags)
    {
      return default(System.Reflection.MethodInfo[]);
    }

    public MethodToken GetMethodToken(System.Reflection.MethodInfo method)
    {
      return default(MethodToken);
    }

    public override void GetPEKind(out System.Reflection.PortableExecutableKinds peKind, out System.Reflection.ImageFileMachine machine)
    {
      peKind = default(System.Reflection.PortableExecutableKinds);
      machine = default(System.Reflection.ImageFileMachine);
    }

    public SignatureToken GetSignatureToken(byte[] sigBytes, int sigLength)
    {
      return default(SignatureToken);
    }

    public SignatureToken GetSignatureToken(SignatureHelper sigHelper)
    {
      return default(SignatureToken);
    }

    public override System.Security.Cryptography.X509Certificates.X509Certificate GetSignerCertificate()
    {
      return default(System.Security.Cryptography.X509Certificates.X509Certificate);
    }

    public StringToken GetStringConstant(string str)
    {
      return default(StringToken);
    }

    public System.Diagnostics.SymbolStore.ISymbolWriter GetSymWriter()
    {
      return default(System.Diagnostics.SymbolStore.ISymbolWriter);
    }

    public override Type GetType(string className)
    {
      return default(Type);
    }

    public override Type GetType(string className, bool ignoreCase)
    {
      return default(Type);
    }

    public override Type GetType(string className, bool throwOnError, bool ignoreCase)
    {
      return default(Type);
    }

    public override Type[] GetTypes()
    {
      return default(Type[]);
    }

    public TypeToken GetTypeToken(Type type)
    {
      return default(TypeToken);
    }

    public TypeToken GetTypeToken(string name)
    {
      return default(TypeToken);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return default(bool);
    }

    public override bool IsResource()
    {
      return default(bool);
    }

    public bool IsTransient()
    {
      return default(bool);
    }

    internal ModuleBuilder()
    {
    }

    public override System.Reflection.FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return default(System.Reflection.FieldInfo);
    }

    public override System.Reflection.MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return default(System.Reflection.MemberInfo);
    }

    public override System.Reflection.MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return default(System.Reflection.MethodBase);
    }

    public override byte[] ResolveSignature(int metadataToken)
    {
      return default(byte[]);
    }

    public override string ResolveString(int metadataToken)
    {
      return default(string);
    }

    public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return default(Type);
    }

    public void SetCustomAttribute(System.Reflection.ConstructorInfo con, byte[] binaryAttribute)
    {
    }

    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
    }

    public void SetSymCustomAttribute(string name, byte[] data)
    {
    }

    public void SetUserEntryPoint(System.Reflection.MethodInfo entryPoint)
    {
    }

    void System.Runtime.InteropServices._ModuleBuilder.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._ModuleBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._ModuleBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._ModuleBuilder.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }
    #endregion

    #region Properties and indexers
    public override System.Reflection.Assembly Assembly
    {
      get
      {
        return default(System.Reflection.Assembly);
      }
    }

    public override string FullyQualifiedName
    {
      get
      {
        return default(string);
      }
    }

    public override int MDStreamVersion
    {
      get
      {
        return default(int);
      }
    }

    public override int MetadataToken
    {
      get
      {
        return default(int);
      }
    }

    public override Guid ModuleVersionId
    {
      get
      {
        return default(Guid);
      }
    }

    public override string Name
    {
      get
      {
        return default(string);
      }
    }

    public override string ScopeName
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
