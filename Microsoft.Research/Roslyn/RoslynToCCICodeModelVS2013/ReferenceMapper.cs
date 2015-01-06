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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Cci;
using Microsoft.Cci.MutableCodeModel;
using System.Diagnostics.Contracts;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using System.Collections.Immutable;

namespace CSharp2CCI {

  internal class ReferenceMapper {

    #region Fields
    private IMetadataHost host;
    private INameTable nameTable;
    private Module module;
    private IAssemblySymbol assemblyBeingTranslated;
    #endregion

    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(this.host != null);
      Contract.Invariant(this.nameTable != null);
      Contract.Invariant(this.nameTable == this.host.NameTable);
    }

    private ReferenceMapper(IMetadataHost host, IAssemblySymbol assemblySymbol) {
      this.host = host;
      this.nameTable = host.NameTable;
      this.assemblyBeingTranslated = assemblySymbol;
    }

    #region Mapping Roslyn symbols to CCI references

    readonly Dictionary<IAssemblySymbol, IAssemblyReference> assemblySymbolCache = new Dictionary<IAssemblySymbol, IAssemblyReference>();
    public IAssemblyReference Map(IAssemblySymbol assembly) {
      Contract.Requires(assembly != null);
      Contract.Ensures(Contract.Result<IAssemblyReference>() != null);

      IAssemblyReference cciAssembly = null;
      if (!assemblySymbolCache.TryGetValue(assembly, out cciAssembly)) {
        var an = assembly.Identity;
        IEnumerable<byte> pkt = an.PublicKeyToken.AsEnumerable();
        if (pkt == null)
          pkt = new byte[0];
        var identity = new Microsoft.Cci.AssemblyIdentity(
          this.nameTable.GetNameFor(an.Name),
          an.CultureName == null ? "" : an.CultureName, // REVIEW: This can't be right
          an.Version,
          pkt,
          "unknown://location" // BUGBUG an.Location == null ? "unknown://location" : an.Location
          );
        cciAssembly = new Microsoft.Cci.Immutable.AssemblyReference(this.host, identity);
        assemblySymbolCache[assembly] = cciAssembly;
      }
      Contract.Assume(cciAssembly != null);
      return cciAssembly;
    }

    readonly Dictionary<INamespaceSymbol, IUnitNamespaceReference> namespaceSymbolCache = new Dictionary<INamespaceSymbol, IUnitNamespaceReference>();
    public IUnitNamespaceReference Map(INamespaceSymbol namespaceSymbol) {
      Contract.Requires(namespaceSymbol != null);
      Contract.Ensures(Contract.Result<IUnitNamespaceReference>() != null);
      IUnitNamespaceReference nsr = null;
      if (!namespaceSymbolCache.TryGetValue(namespaceSymbol, out nsr)) {

        if (namespaceSymbol.ContainingAssembly.Equals(this.assemblyBeingTranslated)) {
          var n = this.CreateNamespaceDefinition(namespaceSymbol);
          return n;
        }

        if (namespaceSymbol.IsGlobalNamespace) {
          var n = new Microsoft.Cci.MutableCodeModel.RootUnitNamespaceReference() {
            Unit = Map(namespaceSymbol.ContainingAssembly),
          };
          nsr = n;
        } else {
          var ns = new Microsoft.Cci.MutableCodeModel.NestedUnitNamespaceReference() {
            ContainingUnitNamespace = Map(namespaceSymbol.ContainingNamespace),
            Name = this.nameTable.GetNameFor(namespaceSymbol.Name),
          };
          nsr = ns;
        }
        namespaceSymbolCache[namespaceSymbol] = nsr;
      }
      Contract.Assume(nsr != null);
      return nsr;
    }

    readonly Dictionary<ITypeSymbol, ITypeReference> typeSymbolCache = new Dictionary<ITypeSymbol, ITypeReference>();
    public ITypeReference Map(ITypeSymbol typeSymbol) {
      Contract.Requires(typeSymbol != null);
      Contract.Ensures(Contract.Result<ITypeReference>() != null);

      ITypeReference itr = null;
      var arrayType = typeSymbol as IArrayTypeSymbol;
      if (arrayType != null)
        typeSymbol = arrayType.ElementType;

      TypeReference tr = null;

      if (!typeSymbolCache.TryGetValue(typeSymbol, out itr)) {

        if (this.assemblyBeingTranslated.Equals(typeSymbol.ContainingAssembly)) {
          // then we have reached this type symbol from a place where it is being referenced,
          // before its definition has been visited
          var t = this.CreateTypeDefinition(typeSymbol);
          return t;
        }

        var genericTypeSymbol = typeSymbol as ITypeParameterSymbol;
        if (genericTypeSymbol != null) {
          var containingSymbol = typeSymbol.ContainingSymbol;
          if (containingSymbol is IMethodSymbol) {
            tr = new GenericMethodParameterReference() {
              DefiningMethod = this.Map((IMethodSymbol)containingSymbol),
              Name = this.host.NameTable.GetNameFor(typeSymbol.Name),
            };
          } else {
            // assume it is a class parameter?
            tr = new GenericTypeParameterReference() {
              DefiningType = this.Map((ITypeSymbol)containingSymbol),
              Name = this.host.NameTable.GetNameFor(typeSymbol.Name),
            };
          }
        }

        var namedTypeSymbol = typeSymbol as INamedTypeSymbol;
        // if the symbol and its ConstructedFrom are the same then it is the template
        if (namedTypeSymbol != null) {
          if (namedTypeSymbol.IsGenericType && namedTypeSymbol != namedTypeSymbol.ConstructedFrom) {
            var gas = new List<ITypeReference>();
            foreach (var a in namedTypeSymbol.TypeArguments) {
              gas.Add(this.Map(a));
            }
            var gtr = new Microsoft.Cci.MutableCodeModel.GenericTypeInstanceReference() {
              GenericArguments = gas,
              GenericType = (INamedTypeReference)this.Map(namedTypeSymbol.ConstructedFrom),
            };
            tr = gtr;
          } else {

            if (typeSymbol.ContainingType == null) {
              var ntr = new Microsoft.Cci.MutableCodeModel.NamespaceTypeReference() {
                ContainingUnitNamespace = Map(typeSymbol.ContainingNamespace),
                GenericParameterCount = (ushort)(namedTypeSymbol == null ? 0 : namedTypeSymbol.TypeParameters.Length),
                IsValueType = typeSymbol.IsValueType,
                Name = this.nameTable.GetNameFor(typeSymbol.Name),
              };
              tr = ntr;
            } else {
              var nestedTr = new Microsoft.Cci.MutableCodeModel.NestedTypeReference() {
                ContainingType = Map(typeSymbol.ContainingType),
                GenericParameterCount = (ushort)namedTypeSymbol.TypeParameters.Length,
                Name = this.nameTable.GetNameFor(typeSymbol.Name),
              };
              tr = nestedTr;
            }

          }
        }
        Contract.Assume(tr != null, "Above type tests meant to be exhaustive");
        tr.InternFactory = this.host.InternFactory;
        tr.PlatformType = this.host.PlatformType;
        tr.TypeCode = GetPrimitiveTypeCode(typeSymbol);
        this.typeSymbolCache[typeSymbol] = tr;
        itr = tr;
      }
      if (arrayType != null) {
        itr = (IArrayTypeReference)Microsoft.Cci.Immutable.Vector.GetVector(itr, this.host.InternFactory);
      }
      return itr;
    }

    readonly Dictionary<IFieldSymbol, IFieldReference> fieldSymbolCache = new Dictionary<IFieldSymbol, IFieldReference>();
    public IFieldReference Map(IFieldSymbol fieldSymbol) {
      IFieldReference fr = null;
      if (!fieldSymbolCache.TryGetValue(fieldSymbol, out fr)) {
        fr = new FieldReference() {
          ContainingType = Map(fieldSymbol.ContainingType),
          InternFactory = this.host.InternFactory,
          Name = this.nameTable.GetNameFor(fieldSymbol.Name),
          Type = Map(fieldSymbol.Type),
        };
        this.fieldSymbolCache[fieldSymbol] = fr;
      }
      return fr;
    }

    readonly Dictionary<IMethodSymbol, IMethodReference> methodSymbolCache = new Dictionary<IMethodSymbol, IMethodReference>();
    public IMethodReference Map(IMethodSymbol methodSymbol) {
      Contract.Requires(methodSymbol != null);
      IMethodReference mr = null;
      if (!methodSymbolCache.TryGetValue(methodSymbol, out mr)) {
        List<IParameterTypeInformation> ps = methodSymbol.Parameters.Length == 0 ? null : new List<IParameterTypeInformation>();
        if (methodSymbol.IsGenericMethod && methodSymbol.ConstructedFrom != methodSymbol) {
          var gArgs = new List<ITypeReference>();
          foreach (var a in methodSymbol.TypeArguments){
            gArgs.Add(Map(a));
          }
          mr = new Microsoft.Cci.MutableCodeModel.GenericMethodInstanceReference() {
            CallingConvention = methodSymbol.IsStatic ? CallingConvention.Default : CallingConvention.HasThis,
            ContainingType = Map(methodSymbol.ContainingType),
            GenericArguments = gArgs,
            GenericMethod = Map(methodSymbol.ConstructedFrom),
            GenericParameterCount = (ushort) methodSymbol.TypeParameters.Length,
            InternFactory = this.host.InternFactory,
            Name = this.nameTable.GetNameFor(methodSymbol.Name),
            Parameters = ps,
            Type = Map(methodSymbol.ReturnType),
          };
        } else {
          var m = new Microsoft.Cci.MutableCodeModel.MethodReference();
          // IMPORTANT: Have to add it to the cache before doing anything else because it may
          // get looked up if it is generic and a parameter's type involves the generic
          // method parameter.
          this.methodSymbolCache.Add(methodSymbol, m);
          m.CallingConvention = methodSymbol.IsStatic ? CallingConvention.Default : CallingConvention.HasThis;
          m.ContainingType = Map(methodSymbol.ContainingType);
          m.InternFactory = this.host.InternFactory;
          m.Name = this.nameTable.GetNameFor(methodSymbol.Name);
          m.Parameters = ps;
          m.Type = Map(methodSymbol.ReturnType);
          mr = m;
        }
        foreach (var p in methodSymbol.Parameters) {
          var p_prime = new ParameterTypeInformation() {
            ContainingSignature = mr,
            Index = (ushort)p.Ordinal,
            Type = Map(p.Type),
          };
          ps.Add(p_prime);
        }
      }
      return mr;
    }

    //public PropertyDefinition Map(PropertySymbol propertySymbol) {
    //  PropertyDefinition pd = null;
    //  if (!propertySymbolCache.TryGetValue(propertySymbol, out pd)) {
    //    pd = new PropertyDefinition() {
    //       CallingConvention = propertySymbol.IsStatic ? CallingConvention.Default : CallingConvention.HasThis,
    //       ContainingTypeDefinition = (ITypeDefinition) Map(propertySymbol.ContainingType),
    //       Name = this.nameTable.GetNameFor(propertySymbol.Name),
    //       Type = Map(propertySymbol.Type),
    //    };
    //    if (propertySymbol.GetMethod != null) {
    //      pd.Getter = Map(propertySymbol.GetMethod);
    //    }
    //    if (propertySymbol.SetMethod != null) {
    //      pd.Setter = Map(propertySymbol.SetMethod);
    //    }
    //  }
    //  return pd;
    //}

    public TypeMemberVisibility Map(Accessibility a) {
      switch (a) {
        case Accessibility.Internal:
          return TypeMemberVisibility.Assembly;
        case Accessibility.NotApplicable:
          return TypeMemberVisibility.Other;
        case Accessibility.Private:
          return TypeMemberVisibility.Private;
        case Accessibility.Protected:
          return TypeMemberVisibility.Family;
        case Accessibility.ProtectedAndInternal:
          return TypeMemberVisibility.FamilyAndAssembly;
        //case R.Compilers.CommonAccessibility.ProtectedInternal:
        //  return TypeMemberVisibility.FamilyOrAssembly;
        case Accessibility.Public:
          return TypeMemberVisibility.Public;
        default:
          throw new InvalidDataException("Mapping Accessibility: switch is supposed to be exhaustive.");
      }
    }

    [Pure]
    public static PrimitiveTypeCode GetPrimitiveTypeCode(ITypeSymbol type) {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<PrimitiveTypeCode>() != PrimitiveTypeCode.Pointer &&
                       Contract.Result<PrimitiveTypeCode>() != PrimitiveTypeCode.Reference &&
                       Contract.Result<PrimitiveTypeCode>() != PrimitiveTypeCode.Invalid,
                       "These types aren't checked for; all others are.");
      //if (type.Name == null || String.IsNullOrEmpty(type.Name.Text)) throw new IllFormedSemanticModelException("A CSharpType was found with a null or empty 'Name' field.", type);
      switch (type.SpecialType) {
        case Microsoft.CodeAnalysis.SpecialType.System_Boolean: return PrimitiveTypeCode.Boolean;
        case Microsoft.CodeAnalysis.SpecialType.System_Char: return PrimitiveTypeCode.Char;
        case Microsoft.CodeAnalysis.SpecialType.System_SByte: return PrimitiveTypeCode.Int8;
        case Microsoft.CodeAnalysis.SpecialType.System_Single: return PrimitiveTypeCode.Float32;
        case Microsoft.CodeAnalysis.SpecialType.System_Double: return PrimitiveTypeCode.Float64;
        case Microsoft.CodeAnalysis.SpecialType.System_Int16: return PrimitiveTypeCode.Int16;
        case Microsoft.CodeAnalysis.SpecialType.System_Int32: return PrimitiveTypeCode.Int32;
        case Microsoft.CodeAnalysis.SpecialType.System_Int64: return PrimitiveTypeCode.Int64;
        case Microsoft.CodeAnalysis.SpecialType.System_IntPtr: return PrimitiveTypeCode.IntPtr;
        case Microsoft.CodeAnalysis.SpecialType.System_String: return PrimitiveTypeCode.String;
        case Microsoft.CodeAnalysis.SpecialType.System_Byte: return PrimitiveTypeCode.UInt8;
        case Microsoft.CodeAnalysis.SpecialType.System_UInt16: return PrimitiveTypeCode.UInt16;
        case Microsoft.CodeAnalysis.SpecialType.System_UInt32: return PrimitiveTypeCode.UInt32;
        case Microsoft.CodeAnalysis.SpecialType.System_UInt64: return PrimitiveTypeCode.UInt64;
        case Microsoft.CodeAnalysis.SpecialType.System_UIntPtr: return PrimitiveTypeCode.UIntPtr;
        case Microsoft.CodeAnalysis.SpecialType.System_Void: return PrimitiveTypeCode.Void;
        default: return PrimitiveTypeCode.NotPrimitive;
      }
    }

    #endregion

    /// <summary>
    /// Translates the metadata "backbone" of the Roslyn assembly, creating a CCI
    /// assembly that is held onto by the returned reference mapper. The mapper
    /// can be used during a traversal of any syntax tree whose semantic model
    /// corresponds to the <paramref name="assemblySymbol"/>. The mapper will
    /// return the equivalent CCI node that corresponds to the semantic node it
    /// is given.
    /// </summary>
    public static ReferenceMapper TranslateAssembly(IMetadataHost host, IAssemblySymbol assemblySymbol) {
      Contract.Requires(host != null);
      Contract.Requires(assemblySymbol != null);
      Contract.Ensures(Contract.Result<ReferenceMapper>() != null);

      var rm = new ReferenceMapper(host, assemblySymbol);
      rm.TranslateMetadata(assemblySymbol);
      return rm;
    }

    #region Recursive tree walk of definition structure

    private Assembly TranslateMetadata(IAssemblySymbol assemblySymbol) {
      Contract.Requires(assemblySymbol != null);
      Contract.Ensures(Contract.Result<Assembly>() != null);

      IAssemblyReference cciAssemblyReference = null;
      Assembly cciAssembly = null;
      if (assemblySymbolCache.TryGetValue(assemblySymbol, out cciAssemblyReference)) {
        cciAssembly = cciAssemblyReference as Assembly;
        System.Diagnostics.Debug.Assert(cciAssembly != null);
        return cciAssembly;
      }

      var coreAssembly = host.LoadAssembly(host.CoreAssemblySymbolicIdentity);
      var name = assemblySymbol.Identity.Name;
      var iname = nameTable.GetNameFor(name);
      cciAssembly = new Assembly() {
        Attributes = this.TranslateMetadata(assemblySymbol.GetAttributes()),
        Name = iname,
        Locations = Helper.WrapLocations(assemblySymbol.Locations),
        ModuleName = iname,
        Kind = ModuleKind.DynamicallyLinkedLibrary,
        RequiresStartupStub = this.host.PointerSize == 4,
        TargetRuntimeVersion = coreAssembly.TargetRuntimeVersion,
        Version = assemblySymbol.Identity.Version,
      };
      cciAssembly.AssemblyReferences.Add(coreAssembly);
      this.assemblySymbolCache.Add(assemblySymbol, cciAssembly);
      this.module = cciAssembly;

      var rootUnitNamespace = new RootUnitNamespace();
      cciAssembly.UnitNamespaceRoot = rootUnitNamespace;
      rootUnitNamespace.Unit = cciAssembly;
      this.namespaceSymbolCache.Add(assemblySymbol.GlobalNamespace, rootUnitNamespace);

      var moduleClass = new NamespaceTypeDefinition() {
        ContainingUnitNamespace = rootUnitNamespace,
        InternFactory = host.InternFactory,
        IsClass = true,
        Name = nameTable.GetNameFor("<Module>"),
      };
      cciAssembly.AllTypes.Add(moduleClass);


      foreach (var m in assemblySymbol.GlobalNamespace.GetMembers()) {

        var namespaceSymbol = m as INamespaceSymbol;
        if (namespaceSymbol != null) {
          var cciNtd = TranslateMetadata(namespaceSymbol);
          rootUnitNamespace.Members.Add(cciNtd);
          continue;
        }

        var typeSymbol = m as ITypeSymbol;
        if (typeSymbol != null) {
          var namedType = TranslateMetadata((INamedTypeSymbol) typeSymbol);
          // TODO: fix
          //namedType.Attributes = TranslateMetadata(typeSymbol.GetAttributes());
          var cciType = (INamespaceTypeDefinition) namedType;
          rootUnitNamespace.Members.Add(cciType);
          //cciAssembly.AllTypes.Add(cciType);
          continue;
        }

      }

      //if (this.entryPoint != null) {
      //  cciAssembly.Kind = ModuleKind.ConsoleApplication;
      //  cciAssembly.EntryPoint = this.entryPoint;
      //}

      return cciAssembly;
    }

    private NestedUnitNamespace TranslateMetadata(INamespaceSymbol namespaceSymbol) {
      Contract.Requires(namespaceSymbol != null);
      Contract.Ensures(Contract.Result<UnitNamespace>() != null);

      IUnitNamespaceReference nsr;
      if (this.namespaceSymbolCache.TryGetValue(namespaceSymbol, out nsr)) {
        var alreadyTranslatedNamespace = nsr as IUnitNamespace;
        if (alreadyTranslatedNamespace != null) return alreadyTranslatedNamespace as NestedUnitNamespace;
      }

      var ns = CreateNamespaceDefinition(namespaceSymbol);

      var mems = new List<INamespaceMember>();
      foreach (var m in namespaceSymbol.GetMembers()) {

        var nestedNamespaceSymbol = m as INamespaceSymbol;
        if (nestedNamespaceSymbol != null) {
          var cciNtd = TranslateMetadata(nestedNamespaceSymbol);
          mems.Add(cciNtd);
        }

        var typeSymbol = m as INamedTypeSymbol;
        if (typeSymbol != null) {
          var cciType = (INamespaceTypeDefinition)TranslateMetadata(typeSymbol);
          mems.Add(cciType);
          continue;
        }

      }
      ns.Members = mems;
      return ns;
    }

    private NamedTypeDefinition TranslateMetadata(INamedTypeSymbol typeSymbol) {
      Contract.Requires(typeSymbol != null);
      Contract.Ensures(Contract.Result<NamedTypeDefinition>() != null);

      NamedTypeDefinition cciType;
      ITypeReference cciTypeRef;
      if (this.typeSymbolCache.TryGetValue(typeSymbol, out cciTypeRef))
       cciType = (NamedTypeDefinition) cciTypeRef;
      else
        cciType = CreateTypeDefinition(typeSymbol);
      this.module.AllTypes.Add(cciType);

      var mems = new List<ITypeDefinitionMember>();
      foreach (var m in typeSymbol.GetMembers()) {

        var attributes = this.TranslateMetadata(m.GetAttributes());

        switch (m.Kind) {
          case SymbolKind.Field:
            var field = TranslateMetadata(m as IFieldSymbol);
            field.Attributes = attributes;
            if (cciType.Fields == null) cciType.Fields = new List<IFieldDefinition>();
            cciType.Fields.Add(field);
            break;
          case SymbolKind.Method:
            var methodSymbol = m as IMethodSymbol;
            var meth = TranslateMetadata(methodSymbol);
            meth.Attributes = attributes;
            if (cciType.Methods == null) cciType.Methods = new List<IMethodDefinition>();
            cciType.Methods.Add(meth);
            break;
          case SymbolKind.NamedType:
            var namedType = TranslateMetadata((INamedTypeSymbol) m);
            Contract.Assert(namedType is NestedTypeDefinition);
            var nestedType = (NestedTypeDefinition)namedType;
            nestedType.Attributes = attributes;
            if (cciType.NestedTypes == null) cciType.NestedTypes = new List<INestedTypeDefinition>();
            cciType.NestedTypes.Add((NestedTypeDefinition)nestedType);
            break;
          case SymbolKind.Property:
            var property = TranslateMetadata(m as IPropertySymbol);
            property.Attributes = attributes;
            if (cciType.Properties == null) cciType.Properties = new List<IPropertyDefinition>();
            cciType.Properties.Add(property);
            break;
          default:
            throw new NotImplementedException();
        }
      }

      if (typeSymbol.IsValueType) {
        cciType.Layout = LayoutKind.Sequential;
        if (IteratorHelper.EnumerableIsEmpty(cciType.Fields)) {
          cciType.SizeOf = 1;
        }
      }

      return cciType;
    }

    private List<ICustomAttribute> TranslateMetadata(ImmutableArray<AttributeData> attributes) {
      var cciAttributes = new List<ICustomAttribute>();
      foreach (var a in attributes) {
        var cciA = this.TranslateMetadata(a);
        cciAttributes.Add(cciA);
      }
      return cciAttributes;
    }

    private CustomAttribute TranslateMetadata(AttributeData a) {
      Contract.Requires(a != null);
      Contract.Ensures(Contract.Result<CustomAttribute>() != null);
      var a2 = new CustomAttribute() {
        Arguments = this.TranslateMetadata(a.ConstructorArguments),
        Constructor = this.Map(a.AttributeConstructor),
        NamedArguments = this.TranslateMetadata(a.NamedArguments),
      };
      return a2;
    }

    private List<IMetadataNamedArgument> TranslateMetadata(ImmutableArray<KeyValuePair<string, TypedConstant>> kvs) {
      var args = new List<IMetadataNamedArgument>();
      foreach (var kv in kvs) {
        var a = new MetadataNamedArgument() {
          ArgumentName = this.host.NameTable.GetNameFor(kv.Key),
          ArgumentValue = this.TranslateMetadata(kv.Value),
        };
        args.Add(a);
      }
      return args;
    }

    private IMetadataExpression TranslateMetadata(TypedConstant typedConstant) {
      return new CompileTimeConstant() {
        Type = this.Map(typedConstant.Type),
        Value = typedConstant.Value,
      };
    }

    private List<IMetadataExpression> TranslateMetadata(ImmutableArray<TypedConstant> typedConstants) {
      var exprs = new List<IMetadataExpression>();
      foreach (var e in typedConstants) {
        var e2 = this.TranslateMetadata(e);
        exprs.Add(e2);
      }
      return exprs;
    }

    private FieldDefinition TranslateMetadata(IFieldSymbol fieldSymbol) {
      Contract.Requires(fieldSymbol != null);
      Contract.Ensures(Contract.Result<FieldDefinition>() != null);

      FieldDefinition f = new FieldDefinition() {
        ContainingTypeDefinition = (ITypeDefinition)this.typeSymbolCache[fieldSymbol.ContainingType],
        InternFactory = this.host.InternFactory,
        IsReadOnly = fieldSymbol.IsReadOnly,
        IsStatic = fieldSymbol.IsStatic,
        Locations = Helper.WrapLocations(fieldSymbol.Locations),
        Name = this.nameTable.GetNameFor(fieldSymbol.Name),
        Type = this.Map(fieldSymbol.Type),
        Visibility = this.Map(fieldSymbol.DeclaredAccessibility),
      };
      return f;
    }

    /// <summary>
    /// Returns a method whose body is empty, but which can be replaced with the real body when it is
    /// available.
    /// </summary>
    /// <remarks>
    /// Public because it needs to get called when an anonymous delegate is encountered
    /// while translating a method body. Just mapping the anonymous delegate doesn't
    /// provide the information needed. The parameters of a method reference are not
    /// IParameterDefinitions.
    /// </remarks>
    public MethodDefinition TranslateMetadata(IMethodSymbol methodSymbol) {
      Contract.Requires(methodSymbol != null);
      Contract.Ensures(Contract.Result<MethodDefinition>() != null);

      var containingType = (ITypeDefinition)this.typeSymbolCache[methodSymbol.ContainingType];
      var isConstructor = methodSymbol.MethodKind == MethodKind.Constructor;
      List<IParameterDefinition> parameters = new List<IParameterDefinition>();

      var m = new MethodDefinition() {
        CallingConvention = methodSymbol.IsStatic ? CallingConvention.Default : CallingConvention.HasThis,
        ContainingTypeDefinition = containingType,
        InternFactory = this.host.InternFactory,
        IsHiddenBySignature = true, // REVIEW
        IsNewSlot = containingType.IsInterface, // REVIEW
        IsRuntimeSpecial = isConstructor,
        IsSpecialName = isConstructor,
        IsStatic = methodSymbol.IsStatic,
        IsVirtual = containingType.IsInterface, // REVIEW: Why doesn't using methodSymbol.Virtual work for interface methods?
        Locations = Helper.WrapLocations(methodSymbol.Locations),
        Name = this.nameTable.GetNameFor(methodSymbol.Name),
        Parameters = parameters,
        Type = this.Map(methodSymbol.ReturnType),
        Visibility = this.Map(methodSymbol.DeclaredAccessibility),
      };

      // IMPORTANT: Have to add it to the cache before doing anything else because it may
      // get looked up if it is generic and a parameter's type involves the generic
      // method parameter.
      this.methodSymbolCache.Add(methodSymbol, m);

      #region Define the generic parameters
      if (methodSymbol.IsGenericMethod) {
      var genericParameters = new List<IGenericMethodParameter>();
      foreach (var gp in methodSymbol.TypeParameters) {
        var gp2 = this.CreateTypeDefinition(gp);
        genericParameters.Add((IGenericMethodParameter) gp2);
      }
      m.GenericParameters = genericParameters;
      }
      #endregion

      #region Define the parameters
      ushort i = 0;
      foreach (var p in methodSymbol.Parameters) {
        var p_prime = new ParameterDefinition() {
          ContainingSignature = m,
          IsByReference = p.RefKind == RefKind.Ref,
          IsIn = p.RefKind == RefKind.None,
          IsOut = p.RefKind == RefKind.Out,
          Name = nameTable.GetNameFor(p.Name),
          Type = this.Map(p.Type),
          Index = i++,
        };
        parameters.Add(p_prime);
      }
      #endregion Define the parameters

      #region Define default ctor, if needed
      if (/*methodSymbol.IsSynthesized &&*/ isConstructor) { // BUGBUG!!
        m.IsHiddenBySignature = true;
        m.IsRuntimeSpecial = true;
        m.IsSpecialName = true;
        var statements = new List<IStatement>();
        var body = new SourceMethodBody(this.host, null, null) {
          LocalsAreZeroed = true,
          Block = new BlockStatement() { Statements = statements },
        };
        var thisRef = new ThisReference() { Type = containingType, };
        // base();
        foreach (var baseClass in containingType.BaseClasses) {
          var baseCtor = new Microsoft.Cci.MutableCodeModel.MethodReference() {
            CallingConvention = CallingConvention.HasThis,
            ContainingType = baseClass,
            GenericParameterCount = 0,
            InternFactory = this.host.InternFactory,
            Name = nameTable.Ctor,
            Type = this.host.PlatformType.SystemVoid,
          };
          statements.Add(
            new ExpressionStatement() {
              Expression = new MethodCall() {
                MethodToCall = baseCtor,
                IsStaticCall = false, // REVIEW: Is this needed in addition to setting the ThisArgument?
                ThisArgument = thisRef,
                Type = this.host.PlatformType.SystemVoid, // REVIEW: Is this the right way to do this?
                Arguments = new List<IExpression>(),
              }
            }
            );
          break;
        }
        // return;
        statements.Add(new ReturnStatement());
        body.MethodDefinition = m;
        m.Body = body;
      }
      #endregion

      return m;

    }

    readonly Dictionary<IPropertySymbol, PropertyDefinition> propertySymbolCache = new Dictionary<IPropertySymbol, PropertyDefinition>();
    private PropertyDefinition TranslateMetadata(IPropertySymbol propertySymbol) {
      Contract.Requires(propertySymbol != null);
      Contract.Ensures(Contract.Result<PropertyDefinition>() != null);

      var containingType = (ITypeDefinition)this.typeSymbolCache[propertySymbol.ContainingType];
      List<IParameterDefinition> parameters = new List<IParameterDefinition>();
      var p = new PropertyDefinition() {
        CallingConvention = propertySymbol.IsStatic ? CallingConvention.Default : CallingConvention.HasThis,
        ContainingTypeDefinition = containingType,
        Getter = propertySymbol.GetMethod == null ? null : this.Map(propertySymbol.GetMethod),
        Locations = Helper.WrapLocations(propertySymbol.Locations),
        Name = this.nameTable.GetNameFor(propertySymbol.Name),
        Parameters = parameters,
        Setter = propertySymbol.SetMethod == null ? null : this.Map(propertySymbol.SetMethod),
        Type = this.Map(propertySymbol.Type),
        Visibility = this.Map(propertySymbol.DeclaredAccessibility),
      };

      //#region Define the parameters
      //ushort i = 0;
      //foreach (var p in methodSymbol.Parameters) {
      //  var p_prime = new ParameterDefinition() {
      //    ContainingSignature = p,
      //    Name = nameTable.GetNameFor(p.Name),
      //    Type = this.Map(p.Type),
      //    Index = i++,
      //  };
      //  parameters.Add(p_prime);
      //}
      //#endregion Define the parameters

      this.propertySymbolCache.Add(propertySymbol, p);
      return p;
    }

    #endregion

    #region Methods that create CCI definitions from Roslyn symbols

    /// <summary>
    /// Creates a nested namespace (because only the root namespace is not nested
    /// and that is created when the assembly is created).
    /// Adds it to the namespace cache.
    /// </summary>
    /// <param name="namespaceSymbol"></param>
    /// <returns></returns>
    private NestedUnitNamespace CreateNamespaceDefinition(INamespaceSymbol namespaceSymbol) {
      var ns = new NestedUnitNamespace() {
        ContainingUnitNamespace = (IUnitNamespace)this.namespaceSymbolCache[namespaceSymbol.ContainingNamespace],
        Locations = Helper.WrapLocations(namespaceSymbol.Locations),
        Name = this.host.NameTable.GetNameFor(namespaceSymbol.Name),
        //Unit = (IUnit)this.assemblySymbolCache[namespaceSymbol.ContainingAssembly],
      };
      this.namespaceSymbolCache.Add(namespaceSymbol, ns);
      return ns;
    }

    private ITypeDefinition CreateTypeDefinition(ITypeSymbol typeSymbol) {
      Contract.Requires(typeSymbol != null);

      var nts = typeSymbol as INamedTypeSymbol;
      if (nts != null) return this.CreateTypeDefinition(nts);

      var gts = typeSymbol as ITypeParameterSymbol;
      if (gts != null) return this.CreateTypeDefinition(gts);

      throw new InvalidDataException("CreateTypeDefinition: type case is supposed to be exhaustive.");

    }

    /// <summary>
    /// Creates the appropriate kind of type definition (nested or not) and
    /// adds it to the type symbol cache.
    /// </summary>
    private NamedTypeDefinition CreateTypeDefinition(INamedTypeSymbol typeSymbol) {
      Contract.Requires(typeSymbol != null);

      NamedTypeDefinition cciType;

      if (typeSymbol.ContainingType == null) {
        cciType = new NamespaceTypeDefinition() {
          ContainingUnitNamespace = (IUnitNamespace)this.namespaceSymbolCache[typeSymbol.ContainingNamespace],
          IsPublic = typeSymbol.DeclaredAccessibility == Accessibility.Public,
        };
      } else {
        cciType = new NestedTypeDefinition() {
          ContainingTypeDefinition = (ITypeDefinition)this.typeSymbolCache[typeSymbol.ContainingType],
          Visibility = TypeMemberVisibility.Private,
        };
      }
      if (typeSymbol.TypeKind == TypeKind.Interface) {
        cciType.IsAbstract = true;
        cciType.IsInterface = true;
      } else {
        cciType.BaseClasses = new List<ITypeReference> { this.Map(typeSymbol.BaseType) };
        cciType.IsBeforeFieldInit = true; // REVIEW: How to determine from typeSymbol?
      }
      cciType.IsClass = typeSymbol.IsReferenceType;
      cciType.IsValueType = typeSymbol.IsValueType;
      cciType.IsSealed = typeSymbol.IsSealed;
      cciType.InternFactory = this.host.InternFactory;
      cciType.Locations = Helper.WrapLocations(typeSymbol.Locations);
      cciType.Name = this.host.NameTable.GetNameFor(typeSymbol.Name);

      this.typeSymbolCache.Add(typeSymbol, cciType);
      return cciType;
    }

    /// <summary>
    /// Creates either a generic method parameter or a generic type parameter
    /// and adds it to the type symbol cache.
    /// </summary>
    /// <param name="typeSymbol"></param>
    /// <returns></returns>
    private GenericParameter CreateTypeDefinition(ITypeParameterSymbol typeSymbol) {
      Contract.Requires(typeSymbol != null);
      Contract.Ensures(Contract.Result<GenericParameter>() != null);

      GenericParameter cciType;

      var containingSymbol = typeSymbol.ContainingSymbol;
      if (containingSymbol is IMethodSymbol) {
        cciType = new GenericMethodParameter() {
          DefiningMethod = (IMethodDefinition)this.Map((IMethodSymbol)containingSymbol),
          Name  = this.host.NameTable.GetNameFor(typeSymbol.Name),
        };
      } else {
        // assume it is a class parameter?
        cciType = new GenericTypeParameter() {
          DefiningType = (ITypeDefinition)this.Map((ITypeSymbol)containingSymbol),
          Name = this.host.NameTable.GetNameFor(typeSymbol.Name),
        };
      }

      var constraints = new List<ITypeReference>();
      foreach (var c in typeSymbol.ConstraintTypes) {
        var c2 = this.Map(c);
        constraints.Add(c2);

      }
      cciType.Constraints = constraints;

      this.typeSymbolCache.Add(typeSymbol, cciType);
      return cciType;
    }

    #endregion

  }

  internal class Helper {

    public static IBlockStatement MkBlockStatement(IStatement statement) {
      var block = statement as IBlockStatement;
      if (block == null) {
        block = new BlockStatement() { Statements = new List<IStatement> { statement }, };
      }
      return block;
    }

    public static List<ILocation> SourceLocation(SyntaxTree tree, CSharpSyntaxNode node) {
      var s = new SourceLocationWrapper(tree, node.Span);
      var l = (ILocation)s;
      return new List<ILocation> { l };
    }
    public static List<ILocation> SourceLocation(SyntaxTree tree, SyntaxToken token) {
      var s = new SourceLocationWrapper(tree, token.Span);
      var l = (ILocation)s;
      return new List<ILocation> { l };
    }

    public static List<ILocation> WrapLocations(ImmutableArray<Microsoft.CodeAnalysis.Location> roslynLocations) {
      return new List<ILocation>(LazyWrapLocations(roslynLocations));
    }
    public static IEnumerable<ILocation> LazyWrapLocations(ImmutableArray<Microsoft.CodeAnalysis.Location> roslynLocations) {
      foreach (var rl in roslynLocations)
        yield return WrapLocation(rl);
    }
    public static ILocation WrapLocation(Microsoft.CodeAnalysis.Location roslynLocation) {
      return new SourceLocationWrapper(roslynLocation.SourceTree, roslynLocation.SourceSpan);
    }

    public static ITargetExpression MakeTargetExpression(IExpression e) {
      var be = e as IBoundExpression;
      if (be != null)
        return new TargetExpression() { Definition = be.Definition, Instance = be.Instance, Type = be.Type, };
      var addressDeref = e as IAddressDereference;
      if (addressDeref != null)
        return new TargetExpression() { Definition = addressDeref, Instance = null, Type = addressDeref.Type, };
      throw new InvalidOperationException("MakeTargetExpression given an expression it can't deal with");
    }

  }
}
