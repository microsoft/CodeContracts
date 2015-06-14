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
using System.Diagnostics.Contracts;
using System.IO;
using Microsoft.Cci.MutableCodeModel;
using Microsoft.Research.CodeAnalysis;
using System.Linq;

namespace Microsoft.Cci.Analysis {
  class Prune : MetadataRewriter {

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.thingsToKeep != null);
    }


    readonly private HashSet<object> thingsToKeep;
    readonly private MetadataDeepCopier copier;
    readonly private HashSet<uint> methodDefsToKeep;
    private IDictionary<IMethodDefinition, uint> contractOffsets;
    readonly private IDictionary<IMethodDefinition, MethodHashAttribute> methodHashAttributes;


    readonly private INamespaceTypeReference systemString;
    readonly private INamespaceTypeReference systemInt;
    private MethodReference methodHashAttributeCtor;

    private Prune(
      IMetadataHost host,
      MetadataDeepCopier copier,
      HashSet<object> thingsToKeep,
      HashSet<uint> methodsWhoseBodiesShouldBeKept,
      IDictionary<IMethodDefinition, MethodHashAttribute> methodHashAttributes
      )
      : base(host) {

      Contract.Requires(host != null);
      Contract.Requires(copier != null);
      Contract.Requires(thingsToKeep != null);
      Contract.Requires(methodsWhoseBodiesShouldBeKept != null);
      Contract.Requires(methodHashAttributes != null);

      this.copier = copier;
      this.thingsToKeep = thingsToKeep;
      this.methodDefsToKeep = methodsWhoseBodiesShouldBeKept;
      this.methodHashAttributes = methodHashAttributes;

      this.systemInt = host.PlatformType.SystemInt32;
      this.systemString = host.PlatformType.SystemString;

    }

    public static Assembly PruneAssembly(HostEnvironment host, ISlice<MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, IAssemblyReference> slice) {
      Contract.Requires(host != null);
      Contract.Requires(slice != null);

      var newAssemblyName = slice.Name;
      var originalAssembly = slice.ContainingAssembly.ResolvedAssembly;
      Contract.Assume(!(originalAssembly is Dummy));
      var methodDefsToKeep = new HashSet<uint>();
      foreach (var m in slice.Methods) {
        methodDefsToKeep.Add(m.reference.InternedKey);
      }


      var copier = new MetadataDeepCopier(host);
      var thingsToKeep = new HashSet<object>();
      var methodHashAttributes = new Dictionary<IMethodDefinition, MethodHashAttribute>();
      var me = new Prune(host, copier, thingsToKeep, methodDefsToKeep, methodHashAttributes);

      // 1. everything that is specified in the slice should definitely be kept.
      foreach (var c in slice.Chains) {
        me.VisitChain(c);
      }

      // 2. everything reachable from the initial set of things to keep should be kept
      Dictionary<IMethodDefinition, uint> contractOffsets;
      FindReachable.Reachable(host, slice, thingsToKeep, methodDefsToKeep, out contractOffsets);
      me.contractOffsets = contractOffsets;

      // 3. copy the original assembly --- entirely!
      var a_prime = copier.Copy(originalAssembly);
      var nameTable = host.NameTable;
      a_prime.ModuleName = nameTable.GetNameFor(newAssemblyName + ".dll");
      a_prime.Name = nameTable.GetNameFor(newAssemblyName);

      var mutableRoot = (RootUnitNamespace)(a_prime.UnitNamespaceRoot);
      var methodHashAttributeType = DefineMethodHashAttributeType(host, mutableRoot);
      me.methodHashAttributeCtor = new Microsoft.Cci.MethodReference(
          host,
          methodHashAttributeType,
          CallingConvention.HasThis,
          host.PlatformType.SystemVoid,
          host.NameTable.Ctor,
          0,
          me.systemString,
          me.systemInt);

      // 4. delete all unwanted things from the mutable copy
      me.RewriteChildren(a_prime);

      var remainingTypes = new List<INamedTypeDefinition>(a_prime.AllTypes.Count); // will only shrink
      remainingTypes.Add(a_prime.AllTypes[0]); // <Module> class is always kept
      for (int i = 1, n = a_prime.AllTypes.Count; i < n; i++) {
        var t = a_prime.AllTypes[i];
        Contract.Assume(t!= null);
        Contract.Assume(copier.OriginalFor.ContainsKey(t));
        var orig = copier.OriginalFor[t];
        if (thingsToKeep.Contains(orig))
          remainingTypes.Add(t);
      }
      a_prime.AllTypes = remainingTypes;
      // do this afterwards so it doesn't get visited.
      mutableRoot.Members.Add(methodHashAttributeType);
      a_prime.AllTypes.Add(methodHashAttributeType);

      return a_prime;
    }

    private void VisitChain(IChain<MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor> c) {
      Contract.Requires(c != null);
      switch (c.Tag) {
        case ChainTag.Type:
          var t = c.Type.reference.ResolvedType;
          this.thingsToKeep.Add(t);
          var ntr = t as INamespaceTypeReference;
          if (ntr != null) {
            this.AddUpwardNamespace(ntr.ContainingUnitNamespace);
          }
          foreach (var child in c.Children)
            this.VisitChain(child);
          break;
        case ChainTag.Field:
          this.thingsToKeep.Add(c.Field.reference.ResolvedField);
          break;
        case ChainTag.Method:
          var m = c.Method.reference.ResolvedMethod;
          this.thingsToKeep.Add(m);
          this.methodHashAttributes.Add(m, c.MethodHashAttribute);
          break;
        default:
          Contract.Assume(false, "switch should be exhaustive");
          break;
      }
    }

    private void AddUpwardNamespace(IUnitNamespaceReference iUnitNamespaceReference) {
      this.thingsToKeep.Add(iUnitNamespaceReference);
      var nested = iUnitNamespaceReference as INestedUnitNamespaceReference;
      if (nested != null)
        this.AddUpwardNamespace(nested.ContainingUnitNamespace);
    }

    private static NamespaceTypeDefinition DefineMethodHashAttributeType(HostEnvironment host, RootUnitNamespace rootUnitNamespace) {
      Contract.Requires(host != null);
      var internFactory = host.InternFactory;

      #region Define the type
      var methodHashAttributeType = new NamespaceTypeDefinition() {
        BaseClasses = new List<ITypeReference> { host.PlatformType.SystemAttribute, },
        ContainingUnitNamespace = rootUnitNamespace,
        InternFactory = internFactory,
        //IsBeforeFieldInit = true,
        IsClass = true,
        IsPublic = true,
        //IsSealed = true,
        Methods = new List<IMethodDefinition>(1),
        Name = host.NameTable.GetNameFor("MethodHashAttribute"),
        //Layout = LayoutKind.Auto,
        //StringFormat = StringFormatKind.Ansi,
      };
      #endregion

      #region Define the ctor
      var systemVoidType = host.PlatformType.SystemVoid;
      List<IStatement> statements = new List<IStatement>();
      SourceMethodBody body = new SourceMethodBody(host) {
        LocalsAreZeroed = true,
        Block = new BlockStatement() { Statements = statements },
      };

      var ctor = new MethodDefinition() {
        Body = body,
        CallingConvention = CallingConvention.HasThis,
        ContainingTypeDefinition = methodHashAttributeType,
        InternFactory = internFactory,
        IsRuntimeSpecial = true,
        IsStatic = false,
        IsSpecialName = true,
        Name = host.NameTable.Ctor,
        Type = systemVoidType,
        Visibility = TypeMemberVisibility.Public,
      };
      var systemStringType = host.PlatformType.SystemString;
      var systemIntType = host.PlatformType.SystemInt32;
      ctor.Parameters = new List<IParameterDefinition>(){
        new ParameterDefinition() {
          ContainingSignature = ctor,
          Name = host.NameTable.GetNameFor("a"),
          Type = systemStringType,
          Index = 0,
        },
        new ParameterDefinition() {
          ContainingSignature = ctor,
          Name = host.NameTable.GetNameFor("b"),
          Type = systemIntType,
          Index = 1,
        }
      };

      body.MethodDefinition = ctor;
      var thisRef = new ThisReference() { Type = methodHashAttributeType, };
      // base();
      foreach (var baseClass in methodHashAttributeType.BaseClasses) {
        var baseCtor = new Microsoft.Cci.MutableCodeModel.MethodReference() {
          CallingConvention = CallingConvention.HasThis,
          ContainingType = baseClass,
          GenericParameterCount = 0,
          InternFactory = internFactory,
          Name = host.NameTable.Ctor,
          Type = systemVoidType,
        };
        statements.Add(
          new ExpressionStatement() {
            Expression = new MethodCall() {
              MethodToCall = baseCtor,
              IsStaticCall = false, // REVIEW: Is this needed in addition to setting the ThisArgument?
              ThisArgument = new ThisReference() { Type = methodHashAttributeType, },
              Type = systemVoidType, // REVIEW: Is this the right way to do this?
              Arguments = new List<IExpression>(),
            }
          }
          );
        break;
      }

      // return;
      statements.Add(new ReturnStatement());
      methodHashAttributeType.Methods.Add(ctor);
      #endregion Define the ctor
      return methodHashAttributeType;

    }

    public override void RewriteChildren(UnitNamespace unitNamespace) {

      Contract.Assume(this.copier.OriginalFor.ContainsKey(unitNamespace));
      var origNs = this.copier.OriginalFor[unitNamespace];
      Contract.Assume(this.thingsToKeep.Contains(origNs));

      var members_prime = new List<INamespaceMember>();
      foreach (var m in unitNamespace.Members) {
        Contract.Assume(this.copier.OriginalFor.ContainsKey(m));
        var orig = this.copier.OriginalFor[m];
        if (this.thingsToKeep.Contains(orig)) {
          members_prime.Add(m);
          base.Rewrite(m);
        }
      }
      unitNamespace.Members = members_prime;
    }

    public override void RewriteChildren(NamedTypeDefinition typeDefinition) {

      if (typeDefinition is IGenericParameter) return;
      if (TypeHelper.IsCompilerGenerated(typeDefinition)) return;

      Contract.Assume(this.copier.OriginalFor.ContainsKey(typeDefinition));
      var origTypeDefinition = this.copier.OriginalFor[typeDefinition];
      Contract.Assume(this.thingsToKeep.Contains(origTypeDefinition));

      // Lists can only shrink.

      if (typeDefinition.Methods != null) {
        var methods_prime = new List<IMethodDefinition>(typeDefinition.Methods.Count);
        foreach (var m in typeDefinition.Methods) {
          Contract.Assume(this.copier.OriginalFor.ContainsKey(m));
          var orig = (IMethodDefinition) this.copier.OriginalFor[m];
          if (this.thingsToKeep.Any(ttk => { var m2 = ttk as IMethodReference; return m2 != null && m2.InternedKey == orig.InternedKey; })) {
            methods_prime.Add(m);
            this.Rewrite(m);
          }
        }
        typeDefinition.Methods = methods_prime;
      }

      if (typeDefinition.Fields != null) {
        var fields_prime = new List<IFieldDefinition>(typeDefinition.Fields.Count);
        foreach (var f in typeDefinition.Fields) {
          Contract.Assume(this.copier.OriginalFor.ContainsKey(f));
          var orig = (IFieldDefinition) this.copier.OriginalFor[f];
          if (this.thingsToKeep.Any(ttk => { var f2 = ttk as IFieldReference; return f2 != null && f2.InternedKey == orig.InternedKey; })) {
            fields_prime.Add(f);
          }
        }
        typeDefinition.Fields = fields_prime;
      }

      if (typeDefinition.NestedTypes != null) {
        var nestedTypes_prime = new List<INestedTypeDefinition>(typeDefinition.NestedTypes.Count);
        foreach (var nt in typeDefinition.NestedTypes) {
          Contract.Assume(this.copier.OriginalFor.ContainsKey(nt));
          var orig = (INestedTypeDefinition) this.copier.OriginalFor[nt];
          if (this.thingsToKeep.Any(ttk => { var nt2 = ttk as INestedTypeReference; return nt2 != null && nt2.InternedKey == orig.InternedKey; })) {
            nestedTypes_prime.Add(nt);
            this.Rewrite(nt);
          }
        }
        typeDefinition.NestedTypes = nestedTypes_prime;
      }

      if (typeDefinition.Properties != null) {
        var properties_prime = new List<IPropertyDefinition>(typeDefinition.Properties.Count);
        foreach (var p in typeDefinition.Properties) {
          Contract.Assume(this.copier.OriginalFor.ContainsKey(p));
          var orig = this.copier.OriginalFor[p];
          var keep = this.thingsToKeep.Contains(orig); // REVIEW: How to do this without relying on object identity? prop defs don't have interned keys.
          // Even if it isn't in the list, need to check if any of the accessors are being kept: if so, keep the property.
          // But need to check the accessors even if the property is in the list: might need to delete one of the accessors if that one isn't being kept.
          var originalProperty = (IPropertyDefinition)orig;
          var keepGetter = originalProperty.Getter != null && this.thingsToKeep.Any(ttk => { var m = ttk as IMethodReference; return m != null && m.InternedKey == originalProperty.Getter.InternedKey; });
          var keepSetter = originalProperty.Setter != null && this.thingsToKeep.Any(ttk => { var m = ttk as IMethodReference; return m != null && m.InternedKey == originalProperty.Setter.InternedKey; });
          if (keep || keepGetter || keepSetter) {
            properties_prime.Add(p);
            this.Rewrite(p);
          }
        }
        typeDefinition.Properties = properties_prime;
      }

      // TODO: Events. Anything else?

    }

    public override void RewriteChildren(PropertyDefinition propertyDefinition) {
      Contract.Assume(this.copier.OriginalFor.ContainsKey(propertyDefinition));
      var orig = this.copier.OriginalFor[propertyDefinition];
      var originalProperty = (IPropertyDefinition)orig;
      if (originalProperty.Getter != null && !this.thingsToKeep.Contains(originalProperty.Getter.ResolvedMethod)) {
        propertyDefinition.Getter = null;
        var originalGetter = (IMethodDefinition)this.copier.CopyFor[originalProperty.Getter.ResolvedMethod];
        propertyDefinition.Accessors.Remove(originalGetter);
      }
      if (originalProperty.Setter != null && !this.thingsToKeep.Contains(originalProperty.Setter.ResolvedMethod)) {
        propertyDefinition.Setter = null;
        var originalSetter = (IMethodDefinition)this.copier.CopyFor[originalProperty.Setter.ResolvedMethod];
        propertyDefinition.Accessors.Remove(originalSetter);
      }
    }

    public override void RewriteChildren(MethodDefinition method) {

      if (method.Attributes != null && AttributeHelper.Contains(method.Attributes, this.host.PlatformType.SystemRuntimeCompilerServicesCompilerGeneratedAttribute)) return;
      if (TypeHelper.IsCompilerGenerated(method.ContainingTypeDefinition)) return;
      //if (Microsoft.Cci.MutableContracts.ContractHelper.IsContractClass(this.host, method.ContainingTypeDefinition)) return; // if a method in a contract class was considered reachable, then keep its method body

      #region Add attribute with its hash
      MethodHashAttribute mhAttr;
      List<IMetadataExpression> args;
      Contract.Assume(this.copier.OriginalFor.ContainsKey(method));
      var originalMethod = (IMethodDefinition)this.copier.OriginalFor[method];
      if (this.methodHashAttributes.TryGetValue(originalMethod, out mhAttr)) {
        args = new List<IMetadataExpression>{
            new MetadataConstant(){ Type = this.systemString, Value = mhAttr.Arg1, },
            new MetadataConstant(){ Type = this.systemInt, Value = mhAttr.Arg2, },
          };
      } else {
        // add attribute that says to *not* analyze this method: it is here only because it was discovered as being called somewhere
        args = new List<IMetadataExpression>{
            new MetadataConstant(){ Type = this.systemString, Value = null, },
            new MetadataConstant(){ Type = this.systemInt, Value = ((int)MethodHashAttributeFlags.ForDependenceMethod), },
          };
      }
      var attr = new CustomAttribute() { Constructor = this.methodHashAttributeCtor, Arguments = args, };
      if (method.Attributes == null)
        method.Attributes = new List<ICustomAttribute>(1);
      method.Attributes.Add(attr);
      #endregion
      base.RewriteChildren(method);
    }

    public override void RewriteChildren(MethodBody methodBody) {
      var method = methodBody.MethodDefinition;
      Contract.Assume(this.copier.OriginalFor.ContainsKey(method));
      var o = this.copier.OriginalFor[method];
      var originalMethod = (IMethodDefinition)o;
      var keepEntireBody = this.methodDefsToKeep.Contains(originalMethod.InternedKey);
      if (!keepEntireBody) {
        #region Keep only contract portion of the IL (if it has a contract), add return statement at end
        var ops = new List<IOperation>();

        uint contractOffset;
        if (this.contractOffsets.TryGetValue(originalMethod, out contractOffset)) {
          if (contractOffset != uint.MaxValue) { // then it has a contract
            foreach (var op in methodBody.Operations) {
              if (contractOffset < op.Offset) break;
              ops.Add(op);
            }
          }
        }

        LocalDefinition ld = null;
        if (method.Type.TypeCode != PrimitiveTypeCode.Void) {
          ld = new LocalDefinition() {
            MethodDefinition = method,
            Type = method.Type,
          };
          ops.Add(new Operation() { OperationCode = OperationCode.Ldloc, Value = ld, });
          methodBody.LocalsAreZeroed = true;
        }
        ops.Add(new Operation() { OperationCode = OperationCode.Ret, });
        if (ld != null) {
          if (methodBody.LocalVariables == null)
            methodBody.LocalVariables = new List<ILocalDefinition>();
          methodBody.LocalVariables.Add(ld);
          methodBody.MaxStack++;
        }
        #endregion
        methodBody.Operations = ops;
        methodBody.OperationExceptionInformation = null;
      }
    }

  }

}
