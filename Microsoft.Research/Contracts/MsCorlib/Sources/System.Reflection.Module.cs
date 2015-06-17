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

// File System.Reflection.Module.cs
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
  abstract public partial class Module : System.Runtime.InteropServices._Module, System.Runtime.Serialization.ISerializable, ICustomAttributeProvider
  {
    #region Methods and constructors
    public static bool operator != (System.Reflection.Module left, System.Reflection.Module right)
    {
      Contract.Ensures(Contract.Result<bool>() == ((left.Equals(right)) == false));

      return default(bool);
    }

    public static bool operator == (System.Reflection.Module left, System.Reflection.Module right)
    {
      return default(bool);
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public virtual new Type[] FindTypes(TypeFilter filter, Object filterCriteria)
    {
      return default(Type[]);
    }

    public virtual new Object[] GetCustomAttributes(bool inherit)
    {
      return default(Object[]);
    }

    public virtual new Object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return default(Object[]);
    }

    public virtual new IList<CustomAttributeData> GetCustomAttributesData()
    {
      return default(IList<CustomAttributeData>);
    }

    public FieldInfo GetField(string name)
    {
      return default(FieldInfo);
    }

    public virtual new FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      return default(FieldInfo);
    }

    public virtual new FieldInfo[] GetFields(BindingFlags bindingFlags)
    {
      return default(FieldInfo[]);
    }

    public FieldInfo[] GetFields()
    {
      return default(FieldInfo[]);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public MethodInfo GetMethod(string name)
    {
      return default(MethodInfo);
    }

    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return default(MethodInfo);
    }

    public MethodInfo GetMethod(string name, Type[] types)
    {
      return default(MethodInfo);
    }

    protected virtual new MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return default(MethodInfo);
    }

    public MethodInfo[] GetMethods()
    {
      return default(MethodInfo[]);
    }

    public virtual new MethodInfo[] GetMethods(BindingFlags bindingFlags)
    {
      return default(MethodInfo[]);
    }

    public virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public virtual new void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
    {
      peKind = default(PortableExecutableKinds);
      machine = default(ImageFileMachine);
    }

    public virtual new System.Security.Cryptography.X509Certificates.X509Certificate GetSignerCertificate()
    {
      return default(System.Security.Cryptography.X509Certificates.X509Certificate);
    }

    public virtual new Type GetType(string className, bool ignoreCase)
    {
      return default(Type);
    }

    public virtual new Type GetType(string className, bool throwOnError, bool ignoreCase)
    {
      return default(Type);
    }

    public virtual new Type GetType(string className)
    {
      return default(Type);
    }

    public virtual new Type[] GetTypes()
    {
      return default(Type[]);
    }

    public virtual new bool IsDefined(Type attributeType, bool inherit)
    {
      return default(bool);
    }

    public virtual new bool IsResource()
    {
      return default(bool);
    }

    protected Module()
    {
    }

    public FieldInfo ResolveField(int metadataToken)
    {
      return default(FieldInfo);
    }

    public virtual new FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return default(FieldInfo);
    }

    public MemberInfo ResolveMember(int metadataToken)
    {
      return default(MemberInfo);
    }

    public virtual new MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return default(MemberInfo);
    }

    public virtual new MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return default(MethodBase);
    }

    public MethodBase ResolveMethod(int metadataToken)
    {
      return default(MethodBase);
    }

    public virtual new byte[] ResolveSignature(int metadataToken)
    {
      return default(byte[]);
    }

    public virtual new string ResolveString(int metadataToken)
    {
      return default(string);
    }

    public Type ResolveType(int metadataToken)
    {
      return default(Type);
    }

    public virtual new Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
    {
      return default(Type);
    }

    void System.Runtime.InteropServices._Module.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._Module.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._Module.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._Module.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public virtual new Assembly Assembly
    {
      get
      {
        return default(Assembly);
      }
    }

    public virtual new string FullyQualifiedName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new int MDStreamVersion
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int MetadataToken
    {
      get
      {
        return default(int);
      }
    }

    public ModuleHandle ModuleHandle
    {
      get
      {
        return default(ModuleHandle);
      }
    }

    public virtual new Guid ModuleVersionId
    {
      get
      {
        return default(Guid);
      }
    }

    public virtual new string Name
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string ScopeName
    {
      get
      {
        return default(string);
      }
    }
    #endregion

    #region Fields
    public readonly static TypeFilter FilterTypeName;
    public readonly static TypeFilter FilterTypeNameIgnoreCase;
    #endregion
  }
}
