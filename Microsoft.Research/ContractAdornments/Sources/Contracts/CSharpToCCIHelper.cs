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

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Cci;
using Microsoft.RestrictedUsage.CSharp.Semantics;
using Microsoft.RestrictedUsage.CSharp.Compiler;
using Microsoft.RestrictedUsage.CSharp.Core;
using Microsoft.RestrictedUsage.CSharp.Extensions;
using Microsoft.RestrictedUsage.CSharp.Syntax;
using Microsoft.RestrictedUsage.CSharp.Utilities;
using System;

namespace ContractAdornments {
  /// <summary>
  /// A helper class for the Visual Studio semantic CSharp model. This helper is aware of cci.
  /// </summary>
  /// <remarks>
  /// All methods in this class are [Pure], meaning they are side-effect free.
  /// </remarks>
  public static class CSharpToCCIHelper {
    /// <summary>
    /// Gets the CCI CallingConventions for a semantic member. The calling convention for a CSharp method is "Defualt" (always) and "Generic" (if it is generic) and/or "HasThis" (if it is non-static).
    /// </summary>
    /// <exception cref="IllFormedSemanticModelException">Thrown if the semantic member/type has fields that are null or empty and are required to not be so for the proper operation of this method.</exception>
    [Pure]
    public static CallingConvention GetCallingConventionFor(CSharpMember semanticMember) {
      Contract.Requires(semanticMember != null);

      var callingConvention = CallingConvention.Default;
      if (semanticMember.IsMethod) {
        if (semanticMember.TypeParameters != null && semanticMember.TypeParameters.Count > 0)
          callingConvention = callingConvention | CallingConvention.Generic;
      }
      if (!semanticMember.IsStatic)
        callingConvention = callingConvention | CallingConvention.HasThis;
      return callingConvention;
    }
    /// <summary>
    /// Gets the CCI PrimitiveTypeCode for a semantic type. If the semantic type isn't a primitive type, the PrimitiveTypeCode "NotPrimitive" is used.
    /// </summary>
    /// <exception cref="IllFormedSemanticModelException">Thrown if the semantic member/type has fields that are null or empty and are required to not be so for the proper operation of this method.</exception>
    [Pure]
    public static PrimitiveTypeCode GetPrimitiveTypeCode(CSharpType type) {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<PrimitiveTypeCode>() != PrimitiveTypeCode.Pointer &&
                       Contract.Result<PrimitiveTypeCode>() != PrimitiveTypeCode.Reference &&
                       Contract.Result<PrimitiveTypeCode>() != PrimitiveTypeCode.Invalid,
                       "These types aren't checked for; all others are.");
      if(type.Name == null || String.IsNullOrEmpty(type.Name.Text)) throw new IllFormedSemanticModelException("A CSharpType was found with a null or empty 'Name' field.", type);
      switch (type.Name.Text) {
        case "Boolean": return PrimitiveTypeCode.Boolean;
        case "Char": return PrimitiveTypeCode.Char;
        case "SByte": return PrimitiveTypeCode.Int8;
        case "Single": return PrimitiveTypeCode.Float32;
        case "Double": return PrimitiveTypeCode.Float64;
        case "Int16": return PrimitiveTypeCode.Int16;
        case "Int32": return PrimitiveTypeCode.Int32;
        case "Int64": return PrimitiveTypeCode.Int64;
        case "IntPtr": return PrimitiveTypeCode.IntPtr;
        case "String": return PrimitiveTypeCode.String;
        case "Byte": return PrimitiveTypeCode.UInt8;
        case "UInt16": return PrimitiveTypeCode.UInt32;
        case "UInt32": return PrimitiveTypeCode.UInt64;
        case "UIntPtr": return PrimitiveTypeCode.UIntPtr;
        case "Void": return PrimitiveTypeCode.Void;
        default: return PrimitiveTypeCode.NotPrimitive;
      }
    }
    /// <summary>
    /// Checks if two enumerables are equivalent.
    /// </summary>
    [Pure]
    public static bool EnumerablesAreEquivalent<T1, T2>(IEnumerable<T1> list1, IEnumerable<T2> list2, Func<T1, T2, bool> comparison) {
      Contract.Requires(list1 != null);
      Contract.Requires(list2 != null);
      Contract.Requires(comparison != null);

      var list1Enum = list1.GetEnumerator();
      var list2Enum = list2.GetEnumerator();
      bool list1Moved = list1Enum.MoveNext();
      bool list2Moved = list2Enum.MoveNext();
      if (list1Moved ^ list2Moved)
        return false;
      while (list1Moved && list2Moved) {
        var item1 = list1Enum.Current;
        var item2 = list2Enum.Current;
        if (!comparison(item1, item2))
          return false;
        list1Moved = list1Enum.MoveNext();
        list2Moved = list2Enum.MoveNext();
        if (list1Moved ^ list2Moved)
          return false;
      }
      return true;
    }
    /// <summary>
    /// Checks if two semantic methods are equivalent.
    /// </summary>
    /// <exception cref="IllFormedSemanticModelException">Thrown if the semantic member/type has fields that are null or empty and are required to not be so for the proper operation of this method.</exception>
    [Pure]
    public static bool MembersAreEquivalent(CSharpMember member1, CSharpMember member2) {
      Contract.Requires(member1 != null);
      Contract.Requires(member2 != null);
      #region Check kind
      if (member1.IsProperty ^ member2.IsProperty) return false;
      if (member1.IsMethod ^ member2.IsMethod) return false;
      if (member1.IsField ^ member2.IsField) return false;
      if (member1.IsIndexer ^ member2.IsIndexer) return false;
      #endregion
      #region Check name
      if (member1.Name == null ^ member2.Name == null) return false;
      if (member1.Name!= null && member2.Name != null
          && !member1.Name.Equals(member2.Name)) return false;
      #endregion
      #region Check explicit interface implementation
      if (member1.ExplicitInterfaceImplementation != null ^ member2.ExplicitInterfaceImplementation != null) return false;
      if (member1.ExplicitInterfaceImplementation != null && member2.ExplicitInterfaceImplementation != null 
          && !TypesAreEquivalent(member1.ExplicitInterfaceImplementation, member2.ExplicitInterfaceImplementation)) return false;
      #endregion
      #region Check parameters
      if (member1.Parameters == null ^ member2.Parameters == null) return false;
      if (member1.Parameters != null && member2.Parameters != null
          && !ParameterListsAreEquivalent(member1.Parameters, member2.Parameters)) return false;
      #endregion
      #region Check return type
      if (member1.ReturnType == null) throw new IllFormedSemanticModelException("A CSharpMember (member) was found with a null 'ReturnType' field.", member1);
      if (member2.ReturnType == null) throw new IllFormedSemanticModelException("A CSharpMember (member) was found with a null 'ReturnType' field.", member2);
      if (!TypesAreEquivalent(member1.ReturnType, member2.ReturnType)) return false;
      #endregion
      return true;
    }
    /// <summary>
    /// Checks if two parameters are equivalent.
    /// </summary>
    /// <exception cref="IllFormedSemanticModelException">Thrown if the semantic member/type has fields that are null or empty and are required to not be so for the proper operation of this method.</exception>
    [Pure]
    public static bool ParametersAreEquivalent(CSharpParameter param1, CSharpParameter param2) {
      Contract.Requires(param1 != null);
      Contract.Requires(param2 != null);
      //return param1.Equals(param2); //Doesn't work for our purposes.
      if (param1.Type == null) throw new IllFormedSemanticModelException("A CSharpParameter was found with a null 'Type' field.", param1);
      if (param2.Type == null) throw new IllFormedSemanticModelException("A CSharpParameter was found with a null 'Type' field.", param2);
      if (!TypesAreEquivalent(param1.Type, param2.Type)) return false;
      if (param1.IsOut ^ param2.IsOut) return false;
      if (param1.IsParams ^ param2.IsParams) return false;
      if (param1.IsRef ^ param2.IsRef) return false;
      if (param1.IsThis ^ param2.IsThis) return false;
      return true;
    }
    /// <summary>
    /// Checks if two parameter lists are equivalent.
    /// </summary>
    /// <exception cref="IllFormedSemanticModelException">Thrown if the semantic member/type has fields that are null or empty and are required to not be so for the proper operation of this method.</exception>
    [Pure]
    public static bool ParameterListsAreEquivalent(IEnumerable<CSharpParameter> paramList1, IEnumerable<CSharpParameter> paramList2) {
      Contract.Requires(paramList1 != null);
      Contract.Requires(paramList2 != null);
      return EnumerablesAreEquivalent(paramList1, paramList2, (p1, p2) => ParametersAreEquivalent(p1, p2));
    }
    /// <summary>
    /// Crawls upward in the semantic tree looking for the first base method this method inherits from.
    /// </summary>
    /// <exception cref="IllFormedSemanticModelException">Thrown if the semantic member/type has fields that are null or empty and are required to not be so for the proper operation of this method.</exception>
    [Pure]
    public static bool TryGetBaseMember(CSharpMember member, out CSharpMember baseMember) {
      Contract.Requires(member != null);
      Contract.Ensures(!Contract.Result<bool>() ||
                       member.IsMethod == Contract.ValueAtReturn(out baseMember).IsMethod
                    && member.IsProperty == Contract.ValueAtReturn(out baseMember).IsProperty
                    && member.IsIndexer == Contract.ValueAtReturn(out baseMember).IsIndexer
                    && member.IsField == Contract.ValueAtReturn(out baseMember).IsField);

      baseMember = null;

      if (member.ContainingType == null) throw new IllFormedSemanticModelException("A CSharpMember (method) was found with a null 'ContainingType' field.", member);
      if (!member.ContainingType.IsClass) return false;
      var containingType = member.ContainingType;
      var baseClass = containingType.BaseClass;
      while (baseClass != null) {
        if (TryGetMemberWithSameSignatureFromType(baseClass, member, out baseMember))
        {
          Contract.Assume(
              member.IsMethod == baseMember.IsMethod
           && member.IsProperty == baseMember.IsProperty
           && member.IsIndexer == baseMember.IsIndexer
           && member.IsField == baseMember.IsField);
          return true;
        }
        baseClass = baseClass.BaseClass;
      }
      return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="IllFormedSemanticModelException">Thrown if the semantic member/type has fields that are null or empty and are required to not be so for the proper operation of this method.</exception>
    [Pure]
    public static bool TryGetMemberWithSameSignatureFromType(CSharpType type, CSharpMember memberToMatch, out CSharpMember member) {
      Contract.Requires(type != null);
      Contract.Requires(memberToMatch != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out member) != null);

      member = null;
      var members = type.Members;
      if (members == null) throw new IllFormedSemanticModelException("A CSharpType was found with a null 'Members' field.", type);
      foreach (var m in members) {
        if (m == null) throw new IllFormedSemanticModelException("An null 'member' was found in a CSharpType's 'Members' field.", type);
        if (MembersAreEquivalent(m, memberToMatch)) {
          member = m;
          return true;
        }
      }
      return false;
    }
    /// <summary>
    /// Crawls upward in the semantic tree looking for the first interface method that this method implements.
    /// </summary>
    /// <exception cref="IllFormedSemanticModelException">Thrown if the semantic member/type has fields that are null or empty and are required to not be so for the proper operation of this method.</exception>
    [Pure]
    public static bool TryGetInterfaceMember(CSharpMember member, out CSharpMember interfaceMethod) {
      Contract.Requires(member != null);
      Contract.Ensures(!Contract.Result<bool>() ||
                       Contract.ValueAtReturn(out interfaceMethod) != null);

      interfaceMethod = null;

      if (member.ExplicitInterfaceImplementation != null)
        if (TryGetMemberWithSameSignatureFromType(member.ExplicitInterfaceImplementation, member, out interfaceMethod))
        {
          return true;
        }

      if (member.ContainingType == null || member.ContainingType.BaseInterfaces == null)
      {
        return false;
      }
      
      foreach (var i in member.ContainingType.BaseInterfaces)
      {
        if (i == null) continue;
        if (TryGetMemberWithSameSignatureFromType(i, member, out interfaceMethod))
        {
          return true;
        }
      }
      return false;
    }
    /// <summary>
    /// Checks if two types are equivalent.
    /// </summary>
    /// <exception cref="IllFormedSemanticModelException">Thrown if the semantic member/type has fields that are null or empty and are required to not be so for the proper operation of this method.</exception>
    [Pure]
    public static bool TypesAreEquivalent(CSharpType type1, CSharpType type2) {
      if (type1 == null && type2 == null) return true;
      if (type1 == null) return false;
      if (type2 == null) return false;

      #region Check if type parameter
      if (type1.IsTypeParameter ^ type2.IsTypeParameter) return false;
      if (type1.IsTypeParameter && type2.IsTypeParameter) {
        if (type1.HasReferenceTypeConstraint ^ type2.HasReferenceTypeConstraint) return false;
        if (type1.HasValueTypeConstraint ^ type2.HasValueTypeConstraint) return false;
        return true;
      }
      #endregion
      //return type1.Equals(type2); //TODO: Can we use this? Doesn't seem to work for generic types like 'List<J>'
      #region Check name
      //if (type1.Name == null) throw new IllFormedSemanticModelException("A CSharpType was founded with a null 'Name' field.", type1);
      //if (type2.Name == null) throw new IllFormedSemanticModelException("A CSharpType was founded with a null 'Name' field.", type2);
      //It seems array types always have null names
      if (type1.Name != null ^ type2.Name != null) return false;
      if (type1.Name != null && type2.Name != null &&
          !type1.Name.Equals(type2.Name)) return false;
      #endregion
      #region Check containing type and namespace
      if (type1.ContainingType != null ^ type2.ContainingType != null) return false;
      if (type1.ContainingType != null && type2.ContainingType != null) {
        if (!TypesAreEquivalent(type1.ContainingType, type2.ContainingType)) return false;
      } else {
        if (type1.ContainingNamespace != null ^ type2.ContainingNamespace != null) return false;
        if (type1.ContainingNamespace != null && type2.ContainingNamespace != null &&
          !type1.ContainingNamespace.Equals(type2.ContainingNamespace)) return false;
      }
      #endregion
      #region Check type parameters
      if (type1.TypeParameters != null ^ type2.TypeParameters != null) return false;
      if (type1.TypeParameters != null && type2.TypeParameters != null &&
        type1.TypeParameters.Count != type2.TypeParameters.Count) return false;
      #endregion
      #region Check type arguments
      
      if (type1.TypeArguments != null ^ type2.TypeArguments != null) return false;
      if (type1.TypeArguments != null && type2.TypeArguments != null &&
        !TypeListsAreEquivalent(type1.TypeArguments, type2.TypeArguments)) return false;
      #endregion
      #region Check array
      if (type1.IsArray ^ type2.IsArray) return false;
      if (type1.IsArray && type2.IsArray)
        return TypesAreEquivalent(type1.ElementType, type2.ElementType);
      #endregion
      #region Check pointer
      if (type1.IsPointer ^ type2.IsPointer) return false;
      if (type1.IsPointer && type2.IsPointer)
        return TypesAreEquivalent(type1.ElementType, type2.ElementType);
      #endregion
      if (type1.IsClass != type2.IsClass
        || type1.IsStruct != type2.IsStruct
        || type1.IsStatic != type2.IsStatic
        || type1.IsValueType != type2.IsValueType)
        return false;
      return true;
    }
    /// <summary>
    /// Checks if two type lists are equivalent.
    /// </summary>
    /// <exception cref="IllFormedSemanticModelException">Thrown if the semantic member/type has fields that are null or empty and are required to not be so for the proper operation of this method.</exception>
    [Pure]
    public static bool TypeListsAreEquivalent(IEnumerable<CSharpType> typeList1, IEnumerable<CSharpType> typeList2) {
      Contract.Requires(typeList1 != null);
      Contract.Requires(typeList2 != null);
      return EnumerablesAreEquivalent(typeList1, typeList2, (t1, t2) => TypesAreEquivalent(t1, t2));
    }
    [Pure]
    public static CSharpMember Uninstantiate(this CSharpMember member) {
      Contract.Requires(member != null);
      Contract.Ensures(Contract.Result<CSharpMember>() != null);
      Contract.Ensures(member.IsMethod == Contract.Result<CSharpMember>().IsMethod);
      Contract.Ensures(member.IsConstructor == Contract.Result<CSharpMember>().IsConstructor);
      Contract.Ensures(member.IsProperty == Contract.Result<CSharpMember>().IsProperty);
      Contract.Ensures(member.IsIndexer == Contract.Result<CSharpMember>().IsIndexer);

      var uninstantiatedMember = member;

      var definingMember = member.DefiningMember;
      while (definingMember != null) {
        uninstantiatedMember = definingMember;
        definingMember = definingMember.DefiningMember;
      }

      Contract.Assume(member.IsMethod == uninstantiatedMember.IsMethod);
      Contract.Assume(member.IsConstructor == uninstantiatedMember.IsConstructor);
      Contract.Assume(member.IsProperty == uninstantiatedMember.IsProperty);
      Contract.Assume(member.IsIndexer == uninstantiatedMember.IsIndexer);
      return uninstantiatedMember;
    }
    [Pure]
    public static CSharpType Uninstantiate(this CSharpType type) {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<CSharpType>() != null);

      var uninstantiatedType = type;

      var definingType = type.DefiningType;
      while (definingType != null) {
        uninstantiatedType = definingType;
        definingType = definingType.DefiningType;
      }

      return uninstantiatedType;
    }
  }

  [Serializable]
  public class IllFormedSemanticModelException : Exception {
    public CSharpMember BadMember { get; private set; }
    public CSharpType BadType { get; private set; }
    public CSharpParameter BadParameter { get; private set; }
    public CSharpNamespace BadNamespace { get; private set; }
    public CSharpKind Kind { get; private set; }

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant((BadMember != null) == (Kind == CSharpKind.CSharpMember));
      Contract.Invariant((BadType != null) == (Kind == CSharpKind.CSharpType));
      Contract.Invariant((BadParameter != null) == (Kind == CSharpKind.CSharpParameter));
      Contract.Invariant((BadNamespace != null) == (Kind == CSharpKind.CSharpNamespace));
    }

    public IllFormedSemanticModelException() { }
    public IllFormedSemanticModelException(string message) : base(message) { }
    public IllFormedSemanticModelException(string message, Exception inner) : base(message, inner) { }
    public IllFormedSemanticModelException(string message, CSharpType badType) : base(message) {
        Contract.Requires(badType != null);

        BadType = badType;
        Kind = CSharpKind.CSharpType; 
    }
    public IllFormedSemanticModelException(string message, CSharpMember badMember) : base(message) {
        Contract.Requires(badMember != null);

        BadMember = badMember;
        Kind = CSharpKind.CSharpMember;
    }
    public IllFormedSemanticModelException(string message, CSharpParameter badParameter) : base(message) {
        Contract.Requires(badParameter != null);

        BadParameter = badParameter; 
        Kind = CSharpKind.CSharpParameter; 
    }
    public IllFormedSemanticModelException(string message, CSharpNamespace badNamespace) : base(message) {
        Contract.Requires(badNamespace != null);

        BadNamespace = badNamespace;
        Kind = CSharpKind.CSharpNamespace; 
    }
    protected IllFormedSemanticModelException(
    System.Runtime.Serialization.SerializationInfo info,
    System.Runtime.Serialization.StreamingContext context)
      : base(info, context) { }
  }

  public enum CSharpKind {
    None,
    CSharpMember,
    CSharpType,
    CSharpParameter,
    CSharpNamespace
  }
}