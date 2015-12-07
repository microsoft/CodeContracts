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

#undef Enable_Csharp2CCI

using Microsoft.Cci;
using System.IO;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;
using System;
using System.Collections.Generic;
using Microsoft.Cci.Contracts;
using System.Diagnostics;

using Unit = Microsoft.Research.DataStructures.Unit;
using System.Linq;
using Microsoft.Cci.MutableContracts;
using System.Diagnostics.Contracts;
using Microsoft.Cci.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Cci.Analysis
{

  internal class ThisSubstituter : MutableCodeModel.Contracts.CodeAndContractRewriter
  {
    private readonly IParameterDefinition thisParameter;
    internal ThisSubstituter(IMetadataHost host, IParameterDefinition thisParam)
      : base(host, true)
    {
      this.thisParameter = thisParam;
    }
    public override IExpression Rewrite(IThisReference thisReference)
    {
      var b = new MutableCodeModel.BoundExpression();
      b.Definition = this.thisParameter;
      b.Locations = new List<ILocation>(thisReference.Locations);
      b.Type = this.thisParameter.Type;
      return b;
    }
  }

  // REVIEW: This is really *not* the way to do this. For all contracts, we don't necessarily know that they came from IL. When we get them from 
  // the contract provider, all we know is that they are in the object model. So we either need to lower the object model back to IL, or else
  // modify the provider's interface so that we can get the IL from which the contracts came from. (Or have that logic not visible, but we just
  // can get the IL from a contract.)
  public class CciContractDecoder : IDecodeContracts<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor>
  {
    private readonly CciILCodeProvider parent;
    internal readonly CciMetadataDecoder mdDecoder;
    private readonly IContractAwareHost host;

    internal CciContractDecoder(CciILCodeProvider parent, IContractAwareHost host, CciMetadataDecoder mdDecoder)
    {
      this.parent = parent;
      this.host = host;
      this.mdDecoder = mdDecoder;
    }

    #region IDecodeContracts<IMethodReference,IFieldReference,TypeReferenceAdaptor> Members

    public bool IsContractVerificationAttributeTrue(MethodReferenceAdaptor method)
    {
      return VerifyMethod(method, false, false, false, false);
    }

    public bool VerifyMethod(MethodReferenceAdaptor methodAdaptor, bool analyzeNonUserCode, bool namespaceSelected, bool typeSelected, bool memberSelected)
    {
      return VerifyMethod(methodAdaptor, analyzeNonUserCode, namespaceSelected, typeSelected, memberSelected, true);
    }

    private bool VerifyMethod(MethodReferenceAdaptor methodAdaptor, bool analyzeNonUserCode, bool namespaceSelected, bool typeSelected, bool memberSelected, bool defaultValue)
    {
      if (methodAdaptor.reference is DummyArrayMethodReference) return false;
      IMethodReference method = methodAdaptor.reference;
      if (!analyzeNonUserCode && NonUserCode(method.ResolvedMethod.Attributes)) return false;
      if (memberSelected) return true;
      ThreeValued tv = CheckIfVerify(method.ResolvedMethod.Attributes);
      if (tv.IsDetermined) return tv.Truth;
      if (typeSelected) return true;
      var declaringType = method.ContainingType;
      do
      {
        tv = CheckIfVerify(declaringType.ResolvedType.Attributes);
        if (tv.IsDetermined) return tv.Truth;
        if (!analyzeNonUserCode && NonUserCode(declaringType.ResolvedType.Attributes)) return false;
        var nested = declaringType as INestedTypeReference;
        if (nested == null) break;
        declaringType = nested.ContainingType;
      }
      while (true);
      if (namespaceSelected) return true;
      var toplevel = (INamespaceTypeDefinition)declaringType.ResolvedType;
      var unit = TypeHelper.GetDefiningUnit(toplevel) as IAssembly;
      if (unit == null) return true; //default
      tv = CheckIfVerify(unit.Attributes);
      if (tv.IsFalse) return false;
      return defaultValue; // default
    }

    [Pure]
    private ThreeValued CheckIfVerify(IEnumerable<ICustomAttribute> attributes)
    {
      Contract.Requires(attributes != null);

      foreach (var ca in attributes)
      {
        INamespaceTypeReference nstr = ca.Type as INamespaceTypeReference;
        if (nstr != null && nstr.Name.Value == "ContractVerificationAttribute")
        {
          foreach (var arg in ca.Arguments)
          {
            object obj = CciMetadataDecoder.TranslateToObject(arg);
            if (obj is bool)
            {
              bool value = (bool)obj;
              return new ThreeValued(value);
            }
          }
        }
      }
      return new ThreeValued();
    }

    public bool HasOptionForClousot(MethodReferenceAdaptor methodAdaptor, string optionName, out string optionValue)
    {
      // TODO TODO: the code is not doing what the CCI1 implementation is doing

      optionValue = null;
      if (methodAdaptor.reference is DummyArrayMethodReference) return false;
      IMethodReference method = methodAdaptor.reference;

      ThreeValued tv = CheckIfClousotOption(method.ResolvedMethod.Attributes);
      var declaringType = method.ContainingType;
      do
      {
        tv = CheckIfClousotOption(declaringType.ResolvedType.Attributes);
        if (tv.IsDetermined) return tv.Truth;
        var nested = declaringType as INestedTypeReference;
        if (nested == null) break;
        declaringType = nested.ContainingType;
      }
      while (true);

      var toplevel = (INamespaceTypeDefinition)declaringType.ResolvedType;
      var unit = TypeHelper.GetDefiningUnit(toplevel) as IAssembly;
      if (unit == null) return false;
      tv = CheckIfClousotOption(unit.Attributes);
      if (tv.IsFalse) return false;

      return false;
    }

    [Pure]
    [ContractVerification(false)]
    private ThreeValued CheckIfClousotOption(IEnumerable<ICustomAttribute> attributes)
    {
      Contract.Requires(attributes != null);

      foreach (var ca in attributes)
      {
        INamespaceTypeReference nstr = ca.Type as INamespaceTypeReference;
        if (nstr != null && nstr.Name.Value == "ContractOptionAttribute")
        {
          var args = new string[3];
          var i = 0;

          foreach (var arg in ca.Arguments)
          {
            var obj = CciMetadataDecoder.TranslateToObject(arg);
            if(obj is string )
            {
              if(i >= args.Length)
              {
                return new ThreeValued(false);
              }

              args[i++] = obj as string;
            }
          }

          if(i == 3)
          {
            var toolName = args[0];
            var optionName = args[1];
            var optionValue = args[2];

            if (toolName != null && optionName != null && optionValue != null)
            {
              toolName = toolName.ToLower();
              if (toolName.Contains("cccheck") || toolName.Contains("clousot"))
              {
                if (optionName.ToLower() == "inherit")
                {
                  return new ThreeValued(true);
                }
              }
            }

          }
        }
      }
      return new ThreeValued();
    }

    [Pure]
    public static ThreeValued IsContractOptionAttribute(ICustomAttribute ca, string category, string setting)
    {
      string tmpcategory, tmpsetting;
      var result = IsContractOptionAttribute(ca, out tmpcategory, out tmpsetting);
      if (result.IsDetermined)
      {
        if (string.Compare(category, tmpcategory, true) == 0 && string.Compare(setting, tmpsetting, true) == 0)
        {
          return result;
        }
        return new ThreeValued();
      }
      return result;
    }

    [Pure]
    public static ThreeValued IsContractOptionAttribute(ICustomAttribute ca, out string category, out string setting)
    {
      category = null; setting = null;
      var result = new ThreeValued();
      if (ca == null)
      {
        return result;
      }
      INamespaceTypeReference nstr = ca.Type as INamespaceTypeReference;
      if (nstr == null) return result;
      if (nstr.Name.Value != "ContractOptionAttribute") return result;
      int index = 0;
      foreach (var arg in ca.Arguments)
      {
        object obj = CciMetadataDecoder.TranslateToObject(arg);
        if (index < 2)
        {
          string lit = obj as string;
          if (lit == null) return result;
          if (index == 0) category = lit;
          if (index == 1) setting = lit;
        }
        if (index == 2)
        {
          if (obj is bool)
          {
            var toggle = (bool)obj;
            result = new ThreeValued(toggle);
          }
        }
      }
      return result;
    }

    private static ThreeValued IsMutableHeapIndependent(IEnumerable<ICustomAttribute> attributes)
    {
      Contract.Requires(attributes != null);

      foreach (var ca in attributes)
      {
        var attributeValue = IsContractOptionAttribute(ca, "reads", "mutable");
        if (attributeValue.IsDetermined)
        {
          if (attributeValue.IsFalse) return new ThreeValued(true);
          else return new ThreeValued(false);
        }
      }
      return new ThreeValued(false);
    }

    private static ThreeValued CanInheritContracts(IEnumerable<ICustomAttribute> attributes)
    {
      Contract.Requires(attributes != null);

      foreach (var ca in attributes)
      {
        INamespaceTypeReference nstr = ca.Type as INamespaceTypeReference;
        if (nstr != null && nstr.Name.Value == "ContractOptionAttribute")
        {
          int index = 0;
          foreach (var arg in ca.Arguments)
          {
            object obj = CciMetadataDecoder.TranslateToObject(arg);
            if (index < 2)
            {
              string lit = obj as string;
              if (lit == null) return new ThreeValued();
              if (index == 0 && string.Compare(lit, "Contract", true) != 0) return new ThreeValued();
              if (index == 1 && string.Compare(lit, "Inheritance", true) != 0) return new ThreeValued();
            }
            if (index == 2)
            {
              if (obj is bool)
              {
                bool value = (bool)obj;
                return new ThreeValued(value);
              }
              else return new ThreeValued();
            }
          }
        }
      }
      return new ThreeValued();
    }

    public static bool CanInheritContractsInternal(IMethodReference method)
    {
      Contract.Requires(method != null);

      ThreeValued tv = CanInheritContracts(method.ResolvedMethod.Attributes);
      if (tv.IsDetermined) return tv.Truth;
      var declaringType = method.ContainingType;
      return CanInheritContracts(declaringType);
    }

    public bool CanInheritContracts(MethodReferenceAdaptor method)
    {
      return CanInheritContractsInternal(method.reference);
    }

    public bool CanInheritContracts(TypeReferenceAdaptor declaringType)
    {
      return CanInheritContracts(declaringType.reference);
    }

    public static bool CanInheritContracts(ITypeReference type)
    {
      type = CciMetadataDecoder.Unspecialized(type);
      Contract.Assume(type != null);
      var rt = type.ResolvedType;
      var tv = CanInheritContracts(rt.Attributes);
      if (tv.IsDetermined) return tv.Truth;
      var nested = rt as INestedTypeReference;
      if (nested != null) { return CanInheritContracts(nested.ContainingType); }
      var toplevel = (INamespaceTypeDefinition)rt;
      var unit = TypeHelper.GetDefiningUnit(toplevel) as IAssembly;
      if (unit == null) return true; //default
      tv = CanInheritContracts(unit.Attributes);
      if (tv.IsFalse) return false;
      return true; // default
    }

    public static bool IsMutableHeapIndependent(IMethodReference method)
    {
      Contract.Requires(method != null);

      var unspec = MemberHelper.UninstantiateAndUnspecialize(method);
      ThreeValued tv = IsMutableHeapIndependent(unspec.ResolvedMethod.Attributes);
      //      ThreeValued tv = IsMutableHeapIndependent(method.ResolvedMethod.Attributes);
      if (tv.IsDetermined) return tv.Truth;
      var declaringType = method.ContainingType;
      return IsMutableHeapIndependent(declaringType);
    }

    public bool IsMutableHeapIndependent(MethodReferenceAdaptor method)
    {
      return IsMutableHeapIndependent(method.reference);
    }

    public bool IsMutableHeapIndependent(TypeReferenceAdaptor declaringType)
    {
      return IsMutableHeapIndependent(declaringType.reference);
    }

    public static bool IsMutableHeapIndependent(ITypeReference type)
    {
      type = CciMetadataDecoder.Unspecialized(type);
      Contract.Assume(type != null);
      var rt = type.ResolvedType;
      var tv = IsMutableHeapIndependent(rt.Attributes);
      if (tv.IsDetermined) return tv.Truth;
      var nested = rt as INestedTypeReference;
      if (nested != null) { return IsMutableHeapIndependent(nested.ContainingType); }
      var toplevel = (INamespaceTypeDefinition)rt;
      var unit = TypeHelper.GetDefiningUnit(toplevel) as IAssembly;
      if (unit == null) return true; //default
      tv = IsMutableHeapIndependent(unit.Attributes);
      if (tv.IsFalse) return false;
      return true; // default
    }


    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    private bool NonUserCode(IEnumerable<ICustomAttribute> attributes)
    {
      Contract.Requires(attributes != null);

      foreach (var ca in attributes)
      {
        INamespaceTypeReference nstr = ca.Type as INamespaceTypeReference;
        if (nstr != null)
        {
          switch (nstr.Name.Value)
          {
            case "DebuggerNonUserCodeAttribute":
            case "CompilerGeneratedAttribute":
            case "GeneratedCodeAttribute":
              return true;
          }
        }
      }
      return false;
    }


    public bool IsPure(MethodReferenceAdaptor methodAdaptor)
    {
      if (IsPure(methodAdaptor.reference)) return true;
      var method = CciMetadataDecoder.Unspecialized(methodAdaptor.reference.ResolvedMethod);
      if (IsPure(method)) return true;

      var unspecializedAdapter = MethodReferenceAdaptor.AdaptorOf(method);
      // getters are pure
      if (this.mdDecoder.IsPropertyGetter(unspecializedAdapter)) return true;

      // all operators are considered heap independent
      if (this.mdDecoder.IsStatic(method))
      {
        if (method.Name.Value.StartsWith("op_"))
          return true;
      }

      // if containing type is pure all methods are pure except constructors
      if (!method.IsConstructor)
      {
        var type = method.ContainingType as ITypeDefinition;
        if (type != null)
        {
          if (IsPure(type)) return true;
        }
      }
      foreach (var baseMethod in mdDecoder.OverriddenAndImplementedMethods(unspecializedAdapter))
      {
        if (IsPure(baseMethod)) return true;
      }

      return false;
    }



    public bool IsPure(IReference reference)
    {
      var meth = reference as IMethodReference;
      if (meth != null)
      {
        var imc = this.GetMethodContract(meth);
        if (imc != null && imc.IsPure) return true;
      }

      if (HasAttributeWithName(reference.Attributes, "PureAttribute")) return true;
      return false;
    }

    public static bool IsContractAbbreviator(IMethodDefinition method)
    {
      Contract.Requires(method != null);

      return HasAttributeWithName(method.Attributes, "ContractAbbreviatorAttribute");
    }

    static string TypeName(ITypeReference type)
    {
      INamespaceTypeReference nstr = type as INamespaceTypeReference;
      if (nstr != null) return nstr.Name.Value;
      return null;
    }

    static bool HasAttributeWithName(IEnumerable<ICustomAttribute> attributes, string expected)
    {
      Contract.Requires(attributes != null);

      foreach (var attr in attributes)
      {
        string name = TypeName(attr.Type);
        if (name == expected) return true;
      }
      return false;
    }

    public bool IsFreshResult(MethodReferenceAdaptor methodAdaptor)
    {
      var method = CciMetadataDecoder.Unspecialized(methodAdaptor.reference.ResolvedMethod);
      Contract.Assume(method != null);

      if (HasAttributeWithName(method.Attributes, "FreshAttribute")) return true;
      foreach (var baseMethod in mdDecoder.OverriddenAndImplementedMethods(MethodReferenceAdaptor.AdaptorOf(method)))
      {
        if (IsFreshResult(baseMethod)) return true;
      }
      return false;
    }

    /// <summary>
    /// Some model method references cannot be resolved since they may not actually be members of the type
    /// they claim to be their containing type. (This is the case for ones defined in reference assemblies.)
    /// That means that any attempt to resolve them ends up in a dummy method definition.
    /// So check the model methods list stored in the type contract of the method's containing type.
    /// If that fails, then sure, go ahead and search for the attribute (and for property getters, search 
    /// the property).
    /// </summary>
    public bool IsModel(MethodReferenceAdaptor methodAdaptor)
    {
      bool isModel = false;
      var method = methodAdaptor.reference;

      var tc = this.GetTypeContract(method.ContainingType);
      if (tc != null) {
        foreach (var mm in tc.ContractMethods) {
          if (mm.InternedKey == method.InternedKey) { isModel = true; goto Ret; }
        }
      }

      method = CciMetadataDecoder.Unspecialized(method.ResolvedMethod);
      Contract.Assume(method != null);
      if (HasAttributeWithName(method.Attributes, "ContractModelAttribute")) { isModel = true; goto Ret; }
      if (this.mdDecoder.IsPropertyGetter(methodAdaptor))
      {
        var prop = this.mdDecoder.GetPropertyFromAccessor(methodAdaptor);
        isModel = prop != null && HasAttributeWithName(prop.Attributes, "ContractModelAttribute");
        goto Ret;
      }
    Ret:
      //var name = MemberHelper.GetMethodSignature(method, NameFormattingOptions.DocumentationId);
      //Console.WriteLine("{0}: {1}", name, isModel ? "true" : "false");
    return isModel;
    }

    public IEnumerable<FieldReferenceAdaptor> ModelFields(TypeReferenceAdaptor typeAdaptor)
    {
      var typeReference = typeAdaptor.reference;
      if (typeReference == null) return Enumerable<FieldReferenceAdaptor>.Empty;
      ITypeContract/*?*/ typeContract = this.GetTypeContract(typeReference);
      if (typeContract == null || IteratorHelper.EnumerableIsEmpty(typeContract.ContractFields)) return Enumerable<FieldReferenceAdaptor>.Empty;
      return IteratorHelper.GetConversionEnumerable<IFieldReference, FieldReferenceAdaptor>(typeContract.ContractFields, FieldReferenceAdaptor.AdaptorOf);
    }

    public IEnumerable<MethodReferenceAdaptor> ModelMethods(TypeReferenceAdaptor typeAdaptor)
    {
      var typeReference = typeAdaptor.reference;
      if (typeReference == null) return Enumerable<MethodReferenceAdaptor>.Empty;
      ITypeContract/*?*/ typeContract = this.GetTypeContract(typeReference);
      if (typeContract == null || IteratorHelper.EnumerableIsEmpty(typeContract.ContractMethods)) return Enumerable<MethodReferenceAdaptor>.Empty;
      return IteratorHelper.GetConversionEnumerable<IMethodDefinition, MethodReferenceAdaptor>(typeContract.ContractMethods, MethodReferenceAdaptor.AdaptorOf);
    }

    public bool HasInvariant(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      var type = method.ContainingType;
      ITypeContract/*?*/ typeContract = this.GetTypeContract(type);
      if (typeContract == null) return false;
      var invs = new List<ITypeInvariant>(typeContract.Invariants);
      return 0 < invs.Count;
    }
    public bool HasInvariant(ITypeReference type)
    {
      Contract.Requires(type != null);

      ITypeContract/*?*/ typeContract = this.GetTypeContract(type);
      if (typeContract == null) return false;
      var invs = new List<ITypeInvariant>(typeContract.Invariants);
      return 0 < invs.Count;
    }

    public bool HasInvariant(TypeReferenceAdaptor typeAdaptor)
    {
      ITypeReference type = typeAdaptor.reference;
      ITypeContract/*?*/ typeContract = this.GetTypeContract(type);
      if (typeContract == null) return false;
      var invs = new List<ITypeInvariant>(typeContract.Invariants);
      return 0 < invs.Count;
    }

    #endregion

    #region IDecodeContracts<IMethodReference,IFieldReference,TypeReferenceAdaptor> Members

    public bool HasRequires(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      IMethodContract methodContract = this.GetMethodContract(methodAdaptor);
      if (methodContract == null) return false;
      // TODO: handle contract initializer block!
      return IteratorHelper.Any(methodContract.Preconditions, p => !p.IsModel);
    }

    public bool HasEnsures(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      IMethodContract methodContract = this.GetMethodContract(methodAdaptor);
      if (methodContract == null) return false;
      return IteratorHelper.Any(methodContract.Postconditions, p => !p.IsModel);
    }

    public bool HasModelEnsures(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      IMethodContract methodContract = this.GetMethodContract(methodAdaptor);
      if (methodContract == null) return false;
      return IteratorHelper.Any(methodContract.Postconditions, p => p.IsModel);
    }

    #endregion

    #region IDecodeContracts<LocalVariableAdaptor,IParameterTypeInformation,IMethodReference,IFieldReference,TypeReferenceAdaptor> Members

    public Result AccessRequires<Data, Result>(MethodReferenceAdaptor methodAdaptor, ICodeConsumer<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Data, Result> consumer, Data data)
    {
      Contract.Assume(consumer != null);

      IMethodReference method = methodAdaptor.reference;
      IMethodContract methodContract = this.GetMethodContract(methodAdaptor);
      Contract.Assume(methodContract != null);
      var thisRef = this.mdDecoder.This(methodAdaptor);
      IParameterDefinition pDef = thisRef as IParameterDefinition;
      // BUGBUG? What to do if pDef is null?
      ThisSubstituter ts = new ThisSubstituter(this.host, pDef);

      var requires = new List<IPrecondition>();
      foreach (var p in methodContract.Preconditions)
      {
        if (!p.IsModel)
        {
          requires.Add(ts.Rewrite(p));
        }
      }
      return consumer.Accept(this.parent.ContractProvider, new CciContractPC(requires), data);
    }

    public Result AccessEnsures<Data, Result>(MethodReferenceAdaptor methodAdaptor, ICodeConsumer<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Data, Result> consumer, Data data)
    {
      Contract.Assume(consumer != null);

      IMethodReference method = methodAdaptor.reference;
      IMethodContract methodContract = this.GetMethodContract(methodAdaptor);
      // Contract.Assume(methodContract != null && methodContract.Postconditions != null && 0 < methodContract.Postconditions.Count);

      Contract.Assume(methodContract != null);

      var thisRef = this.mdDecoder.This(methodAdaptor);
      IParameterDefinition pDef = thisRef as IParameterDefinition;
      // BUGBUG? What to do if pDef is null?
      ThisSubstituter ts = new ThisSubstituter(this.host, pDef);

      var ensures = new List<IPostcondition>();
      foreach (var p in methodContract.Postconditions)
      {
        if (!p.IsModel)
        {
          ensures.Add(ts.Rewrite(p));
        }
      }
      return consumer.Accept(this.parent.ContractProvider, new CciContractPC(ensures), data);

    }

    public Result AccessModelEnsures<Data, Result>(MethodReferenceAdaptor methodAdaptor, ICodeConsumer<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Data, Result> consumer, Data data)
    {
      Contract.Assume(consumer != null);

      IMethodReference method = methodAdaptor.reference;
      IMethodContract methodContract = this.GetMethodContract(methodAdaptor);
      Contract.Assume(methodContract != null);
      var thisRef = this.mdDecoder.This(methodAdaptor);
      IParameterDefinition pDef = thisRef as IParameterDefinition;
      // BUGBUG? What to do if pDef is null?
      ThisSubstituter ts = new ThisSubstituter(this.host, pDef);

      var modelEnsures = new List<IPostcondition>();
      foreach (var p in methodContract.Postconditions)
      {
        if (p.IsModel)
        {
          modelEnsures.Add(ts.Rewrite(p));
        }
      }
      return consumer.Accept(this.parent.ContractProvider, new CciContractPC(modelEnsures), data);

    }

    private readonly Dictionary<TypeReferenceAdaptor, IParameterTypeInformation> thisParameterFor = new Dictionary<TypeReferenceAdaptor, IParameterTypeInformation>();

    public Result AccessInvariant<Data, Result>(TypeReferenceAdaptor typeAdaptor, ICodeConsumer<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Data, Result> consumer, Data data)
    {

      ITypeReference type = typeAdaptor.reference;
      ITypeContract/*?*/ typeContract = this.GetTypeContract(type);
      Contract.Assume(typeContract != null, "why?");
      var invariants = new List<ITypeInvariant>(typeContract.Invariants);

      IParameterTypeInformation/*?*/ thisPar = null;
      if (!this.thisParameterFor.TryGetValue(typeAdaptor, out thisPar))
      {
        Microsoft.Cci.MutableCodeModel.ParameterDefinition par = new Microsoft.Cci.MutableCodeModel.ParameterDefinition();
        par.Name = this.host.NameTable.GetNameFor("this");
        par.Index = ushort.MaxValue; // used to communicate with ArgumentIndex and ArgumentStackIndex
        if (typeAdaptor.IsValueType)
        {
          par.Type = Microsoft.Cci.Immutable.ManagedPointerType.GetManagedPointerType(typeAdaptor.reference, host.InternFactory);
        }
        else
        {
          par.Type = typeAdaptor.reference;
        }
        //par.IsByReference = method.ContainingType.IsValueType;
        this.thisParameterFor[typeAdaptor] = par;
        thisPar = par;
      }

      IParameterDefinition pDef = thisPar as IParameterDefinition;
      // BUGBUG? What to do if pDef is null?
      ThisSubstituter ts = new ThisSubstituter(this.host, pDef);
      invariants = ts.Rewrite(invariants);

      return consumer.Accept(this.parent.ContractProvider, new CciContractPC(invariants), data);
    }

    #endregion

    /// <summary>
    /// Gets the contract associated with the method. If needed, the decompiler will be used
    /// to extract any contracts.
    /// </summary>
    /// <param name="method">The method for which the contract is attached.</param>
    /// <returns>
    /// Null if there is no direct contract (no preconditions, postconditions, or modifies clauses)
    /// on the method. That is, it does *not* do contract inheritance!
    /// </returns>
    private IMethodContract/*?*/ GetMethodContract(MethodReferenceAdaptor methodAdaptor)
    {
      Contract.Requires(methodAdaptor.reference != null);
      return GetMethodContract(methodAdaptor.reference);
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public IMethodContract/*?*/ GetMethodContract(IMethodReference method)
    {
      Contract.Requires(method != null);

      #region Special hack to not extract from iterator MoveNext for now
      if (method.Name.Value == "MoveNext" && this.mdDecoder.IsCompilerGenerated(method.ContainingType))
      {
        return null;
      }
      #endregion

      if (this.mdDecoder.IsSpecialized(method))
      {
        method = CciMetadataDecoder.Unspecialized(method);
        Contract.Assume(method != null);
      }

      IUnitReference unit = TypeHelper.GetDefiningUnitReference(method.ContainingType);
      if (unit == null) return null; // But this shouldn't ever happen!! Flag an error somehow?

      var mc = ContractHelper.GetMethodContractFor(host, method.ResolvedMethod);
      if (mc != null) {
        var wr = new WeakReference(method);
        if (!this.mdDecoder.lambdaNames.ContainsKey(wr)) {
          var lambdaNumberer = new LambdaNumberer(null, MemberHelper.GetMethodSignature(method), this.mdDecoder.lambdaNames);
          lambdaNumberer.Traverse(mc);
          this.mdDecoder.lambdaNames[wr] = "foo"; // just a flag so it doesn't get done more than once
        }
      }
      return mc;
    }

    private class LambdaNumberer : CodeAndContractTraverser {
      readonly private string methodName;
      private int number = 0;
      readonly Dictionary<WeakReference, string> table;
      public LambdaNumberer(IContractProvider contractProvider, string methodName, Dictionary<WeakReference, string> table) : base(contractProvider) {
        this.methodName = methodName;
        this.table = table;
      }
      public override void TraverseChildren(IAnonymousDelegate anonymousDelegate) {
        this.number++;
        var name = this.methodName + this.number.ToString();
        this.table[new WeakReference(anonymousDelegate)] = name;
        base.TraverseChildren(anonymousDelegate);
      }
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    internal CciILPC GetContextForMethodBody(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;

      // If this method is an anonymous delegate, then it was decompiled (as part of a contract)
      // and there is no IL. Just return a contract PC to walk it.
      var anonymousDelegateWrapper = method as LambdaMethodReference;
      if (anonymousDelegateWrapper != null)
      {
        return new CciILPC(new CciILPCLambdaContext(anonymousDelegateWrapper), 0);
      }

      // Need to first see if there is a contract in the method body's IL
      IMethodContract methodContract = GetMethodContract(methodAdaptor);
      IMethodDefinition methodDef = method.ResolvedMethod;
      if (methodDef == null) methodDef = Dummy.Method;
      CciILPCContext context;
      var ops = new List<IOperation>(methodDef.Body.Operations);

      if (methodContract == null || NoContracts(methodContract))
      {
        context = new CciILPCMethodBodyContext(method, ops);
        return new CciILPC(context, 0);
      }
      IBlockStatement bs = null;
      if (!this.mdDecoder.methodBodyCallback.extractedMethods.TryGetValue(methodDef, out bs))
      {
        context = new CciILPCMethodBodyContext(method, ops);
        return new CciILPC(context, 0);
      }
      if (this.mdDecoder.IsAutoPropertyMember(methodAdaptor))
      {
        // auto-property getters and setters have no contracts other than preconditions and
        // postconditions that are generated from any object invariants. in any case, their
        // operations have no contracts in them.
        context = new CciILPCMethodBodyContext(method, ops);
        return new CciILPC(context, 0);
      }

      // First, find the primary source context for the contract.
      var unit = TypeHelper.GetDefiningUnitReference(methodDef.ContainingType);
      var contractLocations = new List<ILocation>(methodContract.Locations);
      if (contractLocations.Count == 0)
      {
        throw new InvalidOperationException("Contract found, but no locations.");
      }
      // Find the operation whose location corresponds to the first location of the method contract
      var startIndex = 0;
      var count = ops.Count;
      var contractLocation0 = contractLocations[0];
      IILLocation ilLocation = contractLocation0 as IILLocation;
      if (ilLocation != null)
      {
        while (startIndex < count)
        {
          var currentLocation = ops[startIndex].Location as IILLocation;
          if (currentLocation == null)
          {
            throw new InvalidOperationException("Couldn't find first operation that corresponds to first location in method contract.");
          }
          if (currentLocation.Offset == ilLocation.Offset) break;
          startIndex++;
        }
      }
      else
      {
        // Depends on object identity, which is not always guaranteed.
        while (startIndex < count && ops[startIndex].Location != contractLocation0) startIndex++;
      }
      if (startIndex == count)
      {
        context = new CciILPCMethodBodyContext(method, ops);
        return new CciILPC(context, 0);
        //throw new InvalidOperationException("Couldn't find first operation that corresponds to first location in method contract.");
      }
      // But that is not necessarily the first operation that is part of the contract: the location
      // is, e.g., on the call to Requires, not the first operation of the code that is part of the
      // method call.
      int firstContractIndex;
      var firstContractLocations = this.mdDecoder.GetPrimarySourceLocationsFor(unit, ops, startIndex, false, out firstContractIndex);

      // Now find the last operation that is part of the method contract. Assume for now that is exactly the operation whose
      // location is the same as the last location of the method contract.
      var lastContractLocation = contractLocations.FindLast(l => { var ilLocation2 = l as IILLocation; return ilLocation2 == null || ilLocation2.MethodDefinition == methodDef; });
      ilLocation = lastContractLocation as IILLocation;
      if (ilLocation != null)
      {
        while (startIndex < count)
        {
          var currentLocation = ops[startIndex].Location as IILLocation;
          if (currentLocation == null)
          {
            throw new InvalidOperationException("Couldn't find first operation that corresponds to first location in method contract.");
          }
          if (currentLocation.Offset == ilLocation.Offset) break;
          startIndex++;
        }
      }
      else
      {
        // Depends on object identity, which is not always guaranteed.
        while (startIndex < count && ops[startIndex].Location != lastContractLocation) startIndex++;
      }
      var lastContractIndex = startIndex;

      context = new CciILPCMethodBodyWithContractContext(method, ops, firstContractIndex, lastContractIndex);
      int firstIndex = 0 < firstContractIndex ? 0 : lastContractIndex + 1;
      return new CciILPC(context, firstIndex);
    }

    /// <summary>
    /// A method contract might not have any preconditions, postconditions, or exceptional
    /// postconditions because it might just have "IsPure" set to true. Or maybe for some
    /// other reason.
    /// </summary>
    private bool NoContracts(IMethodContract methodContract)
    {
      return (methodContract == null
        || (IteratorHelper.EnumerableIsEmpty(methodContract.Preconditions) &&
            IteratorHelper.EnumerableIsEmpty(methodContract.Postconditions) &&
            IteratorHelper.EnumerableIsEmpty(methodContract.ThrownExceptions)
            )
            );
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public ITypeContract/*?*/ GetTypeContract(ITypeReference typeReference)
    {
      Contract.Requires(typeReference != null);

      IUnitReference unit = TypeHelper.GetDefiningUnitReference(typeReference);
      if (unit == null) return null; // But this shouldn't ever happen!! Flag an error somehow?

      IContractProvider lcp = this.host.GetContractExtractor(unit.UnitIdentity);
      if (lcp == null) return null; // Again, isn't this an error?

      var contract = lcp.GetTypeContractFor(typeReference);
      if (contract != null) {
        var wr = new WeakReference(typeReference);
        if (!this.mdDecoder.lambdaNames.ContainsKey(wr)) {
          var lambdaNumberer = new LambdaNumberer(null, TypeHelper.GetTypeName(typeReference), this.mdDecoder.lambdaNames);
          lambdaNumberer.Traverse(contract);
          this.mdDecoder.lambdaNames[wr] = "foo"; // just a flag so it doesn't get done more than once
        }
      }
      return contract;
    }

  }

  public class CciContractProvider :
    IMethodCodeProvider<CciContractPC, LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, CciExceptionHandlerInfo>
  {

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.host != null);
    }

    internal readonly IContractAwareHost host;
    internal readonly CciMetadataDecoder mdDecoder;
    internal readonly CciContractDecoder contractDecoder;

    /// <summary>
    /// Used just for its double-dispatch to avoid type tests and get a value that can be used in a switch-case.
    /// </summary>
    private object contractProviderVisitor;

    internal CciContractProvider(IContractAwareHost host, CciMetadataDecoder mdDecoder, CciContractDecoder contractDecoder)
    {
      Contract.Requires(host != null);

      this.host = host;
      this.mdDecoder = mdDecoder;
      this.contractDecoder = contractDecoder;
    }

    #region ICodeProvider<CciContractPC,IMethodReference,TypeReferenceAdaptor> Members
#if false
    public R Decode<T, R>(T data, CciContractPC label, ICodeQuery<CciContractPC, TypeReferenceAdaptor, MethodReferenceAdaptor, T, R> query) {
      object nestedAggregate;
      object finalOperation = DecodeInternal(label, out nestedAggregate);

      if (IsAtomicNested(nestedAggregate)) {
        return query.Atomic(data, label);
      }
      if (nestedAggregate != null) {
        return query.Aggregate(data, label, new CciContractPC(nestedAggregate), (nestedAggregate is ILabeledStatement || nestedAggregate is IBlockStatement));
      }
      if (finalOperation == null) {
        // special encodings
        IConditional conditional = label.Node as IConditional;
        if (conditional != null) {
          switch (label.Index) {
            case 1:
              return query.BranchCond(data, label, new CciContractPC(conditional, 4), false, true, new CciContractPC());
            case 3:
              return query.Branch(data, label, new CciContractPC(conditional, 6));
          }
        }
        IConditionalStatement conditionalStatement = label.Node as IConditionalStatement;
        if (conditionalStatement != null) {
          switch (label.Index) {
            case 1:
              return query.BranchCond(data, label, new CciContractPC(conditionalStatement, 4), false, true, new CciContractPC());
            case 3:
              return query.Branch(data, label, new CciContractPC(conditionalStatement, 6));
          }
        }
        IOldValue oldValue = label.Node as IOldValue;
        if (oldValue != null && label.Index == 0) {
          return query.BeginOld(data, label);

        }
        return query.Nop(data, label);
      }
      IOldValue oldValue2 = finalOperation as IOldValue;
      if (oldValue2 != null) {
        return query.EndOld(data, label);
      }
      IMethodCall methodCall = finalOperation as IMethodCall;
      if (methodCall != null) {
        return query.Call(data, label, MethodReferenceAdaptor.AdaptorOf(methodCall.MethodToCall), methodCall.IsVirtualCall);
      }
      IGotoStatement gotoStatement = finalOperation as IGotoStatement;
      if (gotoStatement != null) {
        return query.Branch(data, label, new CciContractPC(gotoStatement.TargetStatement));
      }
      // default
      return query.Atomic(data, label);
    }
#endif
    public bool Next(CciContractPC current, out CciContractPC nextLabel)
    {
      DecoderAction action;
      object operation = this.DecodeInternal(current, out action);
      switch (action)
      {
        case DecoderAction.NopAndEnd:
        case DecoderAction.DecodeNowAndEnd:
          nextLabel = default(CciContractPC);
          return false; // no next

        default:
          // has next
          nextLabel = new CciContractPC(current.Node, current.Index + 1);
          return true;
      }
    }

    private ILocation/*?*/ GetLocation(object o)
    {
      IEnumerable<ILocation> locs = null;
      IExpression e = o as IExpression;
      if (e != null) locs = e.Locations;
      if (locs == null)
      {
        IPrecondition p = o as IPrecondition;
        if (p != null) locs = p.Locations;
      }
      if (locs == null)
      {
        IPostcondition p = o as IPostcondition;
        if (p != null) locs = p.Locations;
      }
      if (locs == null)
      {
        ITypeInvariant inv = o as ITypeInvariant;
        if (inv != null) locs = inv.Locations;
      }
      if (locs == null) return null;
      foreach (var location in locs)
      {
        var primary = location as IPrimarySourceLocation;
        if (primary != null)
        {
          return primary;
        }
        IILLocation loc = location as IILLocation;
        if (loc != null)
        {
          var unit = TypeHelper.GetDefiningUnitReference(loc.MethodDefinition.ContainingType);
          // REVIEW: Does this need to find the closest source location and not just an exact one?
          var locList = new List<IOperation>() { new Microsoft.Cci.MutableCodeModel.Operation() { Location = loc, }, };
          int dummy;
          var primaryLocations = this.mdDecoder.GetPrimarySourceLocationsFor(unit, locList, 0, true, out dummy);
          foreach (IPrimarySourceLocation psloc in primaryLocations)
            if (psloc != null) return psloc;
        }
      }
      return null;
    }
    private ILocation/*?*/ GetBinaryLocation(object o)
    {
      IEnumerable<ILocation> locs = null;
      IExpression e = o as IExpression;
      if (e != null) locs = e.Locations;
      if (locs == null)
      {
        IPrecondition p = o as IPrecondition;
        if (p != null) locs = p.Locations;
      }
      if (locs == null)
      {
        IPostcondition p = o as IPostcondition;
        if (p != null) locs = p.Locations;
      }
      if (locs == null)
      {
        ITypeInvariant inv = o as ITypeInvariant;
        if (inv != null) locs = inv.Locations;
      }
      if (locs == null) return null;
      foreach (var location in locs)
      {
        IILLocation loc = location as IILLocation;
        if (loc != null) return loc;
      }
      return null;
    }

    public bool HasSourceContext(CciContractPC pc)
    {
      ILocation loc = GetLocation(pc.Node);
      return loc != null;
    }

    public string SourceDocument(CciContractPC pc)
    {
      ILocation loc = GetLocation(pc.Node);
      IPrimarySourceLocation psloc = loc as IPrimarySourceLocation;
      if (psloc != null)
      {
        IIncludedSourceLocation iloc = psloc as IIncludedSourceLocation;
        if (iloc != null) return iloc.OriginalSourceDocumentName;
        return psloc.Document.Location;
      }
      IBinaryLocation bloc = loc as IBinaryLocation;
      if (bloc != null) return bloc.Document.Location;
      return "unknown source document";
    }

    public string SourceAssertionCondition(CciContractPC pc)
    {
      IContractElement ce = pc.Node as IContractElement;
      if (ce != null)
      {
        var sourceCondition = ce.OriginalSource;
        var userTextExpr = ce.Description;
        string userText = null;
        ICompileTimeConstant ctc = userTextExpr as ICompileTimeConstant;
        if (ctc != null)
        {
          userText = ctc.Value as string;
        }
        if (sourceCondition != null)
        {
          if (userText != null)
          {
            return String.Format("{0} ({1})", sourceCondition, userText);
          }
          else
          {
            return sourceCondition;
          }
        }
        else
        {
          return userText; // may be null
        }
      }
      return null;
    }

    public int SourceStartLine(CciContractPC pc)
    {
      IPrimarySourceLocation psloc = GetLocation(pc.Node) as IPrimarySourceLocation;
      if (psloc == null) return 0;
      IIncludedSourceLocation iloc = psloc as IIncludedSourceLocation;
      if (iloc != null) return iloc.OriginalStartLine;
      return psloc.StartLine;
    }

    public int SourceEndLine(CciContractPC pc)
    {
      IPrimarySourceLocation psloc = GetLocation(pc.Node) as IPrimarySourceLocation;
      if (psloc == null) return 0;
      IIncludedSourceLocation iloc = psloc as IIncludedSourceLocation;
      if (iloc != null) return iloc.OriginalEndLine;
      return psloc.EndLine;
    }

    public int SourceStartColumn(CciContractPC pc)
    {
      IPrimarySourceLocation psloc = GetLocation(pc.Node) as IPrimarySourceLocation;
      if (psloc == null) return 0;
      return psloc.StartColumn;
    }

    public int SourceEndColumn(CciContractPC pc)
    {
      ILocation loc = GetLocation(pc.Node);
      IPrimarySourceLocation psloc = loc as IPrimarySourceLocation;
      if (psloc == null) return 0;
      return psloc.EndColumn;
    }

    public int SourceStartIndex(CciContractPC pc)
    {
      ILocation loc = GetLocation(pc.Node);
      ISourceLocation sloc = loc as ISourceLocation;
      if (sloc == null) return 0;
      return sloc.StartIndex;
    }

    public int SourceLength(CciContractPC pc)
    {
      ILocation loc = GetLocation(pc.Node);
      ISourceLocation sloc = loc as ISourceLocation;
      if (sloc == null) return 0;
      return sloc.Length;
    }

    public int ILOffset(CciContractPC pc)
    {
      IILLocation bloc = GetBinaryLocation(pc.Node) as IILLocation;
      if (bloc != null) return (int)bloc.Offset;
      return -1;
    }

    #endregion

    #region IDecodeMSIL<CciContractPC,LocalVariableAdaptor,IParameterTypeInformation,MethodReferenceAdaptor,FieldReferenceAdaptor,TypeReferenceAdaptor,Unit,Unit,Unit> Members

    public Result Decode<Visitor, Data, Result>(CciContractPC pc, Visitor visitor, Data data)
      where Visitor : ICodeQuery<CciContractPC, LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Data, Result>
    {
      DecoderAction action;
      object currentOp = this.DecodeInternal(pc, out action);
      switch (action)
      {
        case DecoderAction.NopAndEnd:
          return visitor.Nop(pc, data);

        case DecoderAction.StartNested:
          return visitor.Aggregate(pc, new CciContractPC(currentOp), (currentOp is ILabeledStatement || currentOp is IBlockStatement), data);

        case DecoderAction.BeginOld:
          return visitor.BeginOld(pc, new CciContractPC(pc.Node, 2), data);

        case DecoderAction.Branch_5:
          return visitor.Branch(pc, new CciContractPC(pc.Node, 5), false, data);

        case DecoderAction.BranchFalse_4:
          return visitor.BranchFalse(pc, new CciContractPC(pc.Node, 4), Unit.Value, data);

        case DecoderAction.DecodeNowAndEnd:
          return DecodeOperation<Visitor, Data, Result>(pc, currentOp, visitor, data);

        case DecoderAction.LdInstanceForAnonymousDelegate:
          var p = ThisReferenceFinder.FindThisReference(pc.Node as IAnonymousDelegate);
          if (p == null)
            return visitor.Ldnull(pc, Unit.Value, data);
          else
            return visitor.Ldarg(pc, p, false, Unit.Value, data);


        case DecoderAction.LdftnForAnonymousDelegate:
          var anonymousdelegate = pc.Node as IAnonymousDelegate;
          Contract.Assume(anonymousdelegate != null);
          var mr = new LambdaMethodReference(this.host, anonymousdelegate, this.mdDecoder.lambdaNames[new WeakReference(pc.Node)]);
          return visitor.Ldftn(pc, MethodReferenceAdaptor.AdaptorOf(mr), Unit.Value, data);

        case DecoderAction.LoadLocalAddress:
          var dv = pc.Node as IDefaultValue;
          var loc = LocalDefAdaptor.AdaptorOf(dv);
          return visitor.Ldloca(pc, loc, Unit.Value, data);

        case DecoderAction.InitObj:
          var dv2 = pc.Node as IDefaultValue;
          Contract.Assume(dv2 != null);
          return visitor.Initobj(pc, TypeReferenceAdaptor.AdaptorOf(dv2.Type), Unit.Value, data);

        case DecoderAction.StoreIntoPseudoLocal:
          var addrOf = pc.Node as IAddressOf;
          return visitor.Stloc(pc, LocalDefAdaptor.AdaptorOf(addrOf), Unit.Value, data);

        default:
          Contract.Assume(false);
          return visitor.Nop(pc, data);
      }
    }


    /// <summary>
    /// If there are any references to "this" in the body of the anonymous
    /// delegate, then the anonymous delegate should be walked as "ldarg this; ldftn lambda". Otherwise
    /// it should be walked as "ldnull; ldftn lambda".
    /// But there is no back link from an anonymous delegate to the enclosing method
    /// (and then to the containing type) so need some way to get "this". Perhaps there is a better
    /// way? Maintain a table mapping anonymous delegates to their "this" value? But for now, just
    /// search the body for an occurrence of the fake parameter that is created to represent "this", 
    /// which is identified by its having the largest possible index.
    /// </summary>
    internal class ThisReferenceFinder : CodeTraverser
    {
      public static IParameterTypeInformation/*?*/ FindThisReference(IAnonymousDelegate iAnonymousDelegate)
      {
        if (iAnonymousDelegate == null) return null;

        var t = new ThisReferenceFinder();
        t.Traverse(iAnonymousDelegate);
        return t.thisReference;
      }
      private IParameterTypeInformation/*?*/ thisReference;
      private ThisReferenceFinder() { }
      public override void TraverseChildren(IParameterDefinition parameterDefinition)
      {
        if (this.thisReference == null && parameterDefinition.Index == ushort.MaxValue)
        {
          this.thisReference = parameterDefinition;
        }
      }
    }

    internal MethodReferenceAdaptor GetTypeFromHandleAdaptor
    {
      get
      {
        return MethodReferenceAdaptor.AdaptorOf(
          new MethodReference(host, host.PlatformType.SystemType, CallingConvention.Default, host.PlatformType.SystemType, host.NameTable.GetNameFor("GetTypeFromHandle"), 0, host.PlatformType.SystemRuntimeTypeHandle)
          );
      }
    }

    public Result DecodeOperation<Visitor, Data, Result>(CciContractPC pc, object currentOp, Visitor visitor, Data data)
      where Visitor : ICodeQuery<CciContractPC, LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Data, Result>
    {
      // decode current op
      IExpression expression = currentOp as IExpression;
      IStatement stmt = currentOp as IStatement;
      if (expression != null || stmt != null)
      {
        ContractProviderVisitor<Data, Result, Visitor> cpv
          = this.contractProviderVisitor as ContractProviderVisitor<Data, Result, Visitor>;
        if (cpv == null)
        {
          //Console.WriteLine("generating a new one");
          this.contractProviderVisitor = cpv =
            new ContractProviderVisitor<Data, Result, Visitor>(this);
        }
        else
        {
          //Console.WriteLine("using the old one");
        }
        cpv.data = data;
        cpv.visitor = visitor;
        cpv.pc = pc;
        if (expression != null)
          cpv.Visit(expression);
        else
          cpv.Visit(stmt);
        return cpv.result;
      }
      // not a statement or expression
      IPrecondition precondition = currentOp as IPrecondition;
      if (precondition != null)
      {
        if (precondition.IsModel)
          return visitor.Assume(pc, "assume", Unit.Value, null, data);
        else
          return visitor.Assume(pc, "requires", Unit.Value, null, data);
      }
      IPostcondition postcondition = currentOp as IPostcondition;
      if (postcondition != null)
      {
        if (postcondition.IsModel)
          return visitor.Assume(pc, "assume", Unit.Value, null, data);
        else
          return visitor.Assert(pc, "ensures", Unit.Value, null, data);
      }
      ITypeInvariant invariant = currentOp as ITypeInvariant;
      if (invariant != null)
      {
        if (invariant.IsModel)
          return visitor.Assume(pc, "assume", Unit.Value, null, data);
        else
          return visitor.Assume(pc, "invariant", Unit.Value, null, data);
      }
      Contract.Assume(false);
      return visitor.Nop(pc, data);
    }
#if false
    public Transformer<CciContractPC, Data, Result> CacheForwardDecoder<Data, Result>(IVisitMSIL<CciContractPC, ILocalDefinition, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Unit, Unit, Data, Result> visitor) {
      throw new NotImplementedException();
    }
#endif
    public Unit GetContext
    {
      get { throw new NotImplementedException(); }
    }

    #endregion

    #region IMethodCodeProvider
    /// <summary>
    /// Returns the handlers associated with the provided method
    /// </summary>
    public IEnumerable<CciExceptionHandlerInfo> TryBlocks(MethodReferenceAdaptor method) { return Enumerable<CciExceptionHandlerInfo>.Empty; }

    /// <summary>
    /// Decodes ExceptionInfo for the kind of handler
    /// </summary>
    public bool IsFaultHandler(CciExceptionHandlerInfo info) { return false; }
    /// <summary>
    /// Decodes ExceptionInfo for the kind of handler
    /// </summary>
    public bool IsFinallyHandler(CciExceptionHandlerInfo info) { return false; }
    /// <summary>
    /// Decodes ExceptionInfo for the kind of handler
    /// </summary>
    public bool IsCatchHandler(CciExceptionHandlerInfo info) { return false; }
    /// <summary>
    /// Decodes ExceptionInfo for the kind of handler
    /// </summary>
    public bool IsFilterHandler(CciExceptionHandlerInfo info) { return false; }

    /// <summary>
    /// If the handler is a CatchHandler, this method returns the type of exception caught.
    /// Fails if !IsCatchHandler
    /// </summary>
    public TypeReferenceAdaptor CatchType(CciExceptionHandlerInfo info) { throw new NotImplementedException(); }

    /// <summary>
    /// Should return true if this handler catches all Exceptions
    /// </summary>
    public bool IsCatchAllHandler(CciExceptionHandlerInfo info) { return false; }

    /// <summary>
    /// Decodes ExceptionInfo for the Label corresponding to the start of try block
    /// </summary>
    public CciContractPC TryStart(CciExceptionHandlerInfo info) { throw new NotImplementedException(); }

    /// <summary>
    /// Decodes ExceptionInfo for the Label corresponding to the end of try block
    /// </summary>
    public CciContractPC TryEnd(CciExceptionHandlerInfo info) { throw new NotImplementedException(); }

    /// <summary>
    /// Decodes ExceptionInfo for the Label corresponding to the start of filter decision block (if any)
    /// </summary>
    public CciContractPC FilterDecisionStart(CciExceptionHandlerInfo info) { throw new NotImplementedException(); }

    /// <summary>
    /// Decodes ExceptionInfo for the Label corresponding to the start of handler block
    /// </summary>
    public CciContractPC HandlerStart(CciExceptionHandlerInfo info) { throw new NotImplementedException(); }

    /// <summary>
    /// Decodes ExceptionInfo for the Label corresponding to the end of handler block
    /// </summary>
    public CciContractPC HandlerEnd(CciExceptionHandlerInfo info) { throw new NotImplementedException(); }
    #endregion

    #region Private methods

    private bool IsAtomicNested(object nestedStart)
    {
      if (nestedStart == null) return false;
      IThisReference thisRef = nestedStart as IThisReference;
      if (thisRef != null) return true;
      IParameterDefinition parameterDefinition = nestedStart as IParameterDefinition;
      if (parameterDefinition != null) return true;
      ILocalDefinition localDefinition = nestedStart as ILocalDefinition;
      if (localDefinition != null) return true;
      ICompileTimeConstant compileTimeConstant = nestedStart as ICompileTimeConstant;
      if (compileTimeConstant != null) return true;
      return false;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    private static IAssignment IsAssignmentToLocalStatement(object node)
    {
      IExpressionStatement exprStatement = node as IExpressionStatement;
      if (exprStatement == null) return null;
      IAssignment assignment = exprStatement.Expression as IAssignment;
      if (assignment == null) return null;
      ITargetExpression targetExpression = assignment.Target as ITargetExpression;
      if (targetExpression == null) return null;
      if (targetExpression.Definition is ILocalDefinition && targetExpression.Instance == null)
        return assignment;
      else
        return null;
    }

    enum DecoderAction
    {
      DecodeNowAndEnd,
      NopAndEnd,
      BranchFalse_4,
      Branch_5,
      BeginOld,
      StartNested,
      NewObjForDelegate,
      LdInstanceForAnonymousDelegate, // null for static anonymous delegates, "this" for instance anonymous delegates
      LdftnForAnonymousDelegate,
      LoadLocalAddress,
      InitObj,
      StoreIntoPseudoLocal, // needed for &(Contract.Result<T>())
    }

    private object DecodeInternal(CciContractPC pc, out DecoderAction action)
    {
      object node = pc.Node;
      if (node == null) { action = DecoderAction.NopAndEnd; return node; }
      int index = pc.Index;
      #region Preconditions
      List<IPrecondition> requiresList = node as List<IPrecondition>;
      if (requiresList != null)
      {
        if (index < requiresList.Count)
        {
          action = DecoderAction.StartNested;
          return requiresList[index];
        }
        else
        {
          action = DecoderAction.NopAndEnd;
          return requiresList;
        }
      }
      IPrecondition precondition = node as IPrecondition;
      if (precondition != null)
      {
        // 0 is nested condition
        // 1 is actual requires
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return precondition.Condition;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return node;
      }
      #endregion Preconditions
      #region Postconditions
      List<IPostcondition> ensuresList = node as List<IPostcondition>;
      if (ensuresList != null)
      {
        if (index < ensuresList.Count)
        {
          action = DecoderAction.StartNested;
          return ensuresList[index];
        }
        else
        {
          action = DecoderAction.NopAndEnd;
          return ensuresList;
        }
      }
      IPostcondition postcondition = node as IPostcondition;
      if (postcondition != null)
      {
        // 0 is nested condition
        // 1 is actual ensures
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return postcondition.Condition;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return node;
      }
      #endregion Postconditions
      #region Invariants
      List<ITypeInvariant> invariantList = node as List<ITypeInvariant>;
      if (invariantList != null)
      {
        if (index < invariantList.Count)
        {
          action = DecoderAction.StartNested;
          return invariantList[index];
        }
        else
        {
          action = DecoderAction.NopAndEnd;
          return invariantList;
        }
      }
      ITypeInvariant invariant = node as ITypeInvariant;
      if (invariant != null)
      {
        // 0 is nested condition
        // 1 is actual invariant
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return invariant.Condition;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return node;
      }
      #endregion Invariants
      #region Binary Expressions
      IBinaryOperation binaryExpression = node as IBinaryOperation;
      if (binaryExpression != null)
      {
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return binaryExpression.LeftOperand;
        }
        if (index == 1)
        {
          action = DecoderAction.StartNested;
          return binaryExpression.RightOperand;
        }
        // effect binary operation
        action = DecoderAction.DecodeNowAndEnd;
        return binaryExpression;
      }
      #endregion Binary Expressions
      #region Unary Expressions
      IUnaryOperation unaryExpression = node as IUnaryOperation;
      if (unaryExpression != null)
      {
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return unaryExpression.Operand;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return unaryExpression;
      }
      #endregion Unary Expressions
      #region Conversions (which aren't unary expressions!)
      IConversion conversion = node as IConversion;
      if (conversion != null)
      {
        if (conversion.TypeAfterConversion is IPointerTypeReference &&
          conversion.ValueToConvert.Type is IPointerTypeReference)
        {
          // conversion is a nop: ignore it
          action = DecoderAction.DecodeNowAndEnd;
          return conversion.ValueToConvert;
        }
        if (conversion.TypeAfterConversion.IsEnum &&
          TypeHelper.IsPrimitiveInteger(conversion.ValueToConvert.Type))
        {
          // conversion is a nop: ignore it. decompiler now produces
          // such conversions even though they don't correspond to any
          // IL.
          action = DecoderAction.DecodeNowAndEnd;
          return conversion.ValueToConvert;
        }
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return conversion.ValueToConvert;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return conversion;
      }
      #endregion Conversions (which aren't unary expressions!)
      #region Conditional
      IConditional conditional = node as IConditional;
      if (conditional != null)
      {
        switch (index)
        {
          case 0:
            action = DecoderAction.StartNested;
            return conditional.Condition;
          case 1:
            // synthetic brfalse to 4
            action = DecoderAction.BranchFalse_4;
            return conditional;
          case 2:
            action = DecoderAction.StartNested;
            return conditional.ResultIfTrue;
          case 3:
            // synthetic br L2 (5)
            action = DecoderAction.Branch_5;
            return conditional;
          case 4:
            action = DecoderAction.StartNested;
            return conditional.ResultIfFalse;
          case 5:
            // synthetic L2: nop;
            action = DecoderAction.NopAndEnd;
            return conditional;

          default:
            Contract.Assume(false);
            action = DecoderAction.NopAndEnd;
            return null;
        }
      }
      #endregion Conditional
      #region Conditional Statement
      IConditionalStatement conditionalStatement = node as IConditionalStatement;
      if (conditionalStatement != null)
      {
        switch (index)
        {
          case 0:
            action = DecoderAction.StartNested;
            return conditionalStatement.Condition;
          case 1:
            // synthetic brfalse to L1 (4)
            action = DecoderAction.BranchFalse_4;
            return conditionalStatement;

          case 2:
            action = DecoderAction.StartNested;
            return conditionalStatement.TrueBranch;

          case 3:
            // synthetic br L2 (5)
            action = DecoderAction.Branch_5;
            return conditionalStatement;

          case 4:
            action = DecoderAction.StartNested;
            return conditionalStatement.FalseBranch;

          case 5:
            // synthetic L2: nop;
            action = DecoderAction.NopAndEnd;
            return conditionalStatement;

          default:
            Contract.Assume(false);
            action = DecoderAction.NopAndEnd;
            return null;
        }
      }
      #endregion Conditional Statement
      #region OldValue
      IOldValue oldValue = node as IOldValue;
      if (oldValue != null)
      {
        // has 3 points:
        //   0 is begin_old
        //   1 is nested aggregate expression
        //   2 is end_old
        if (index == 0)
        {
          action = DecoderAction.BeginOld;
          return oldValue;
        }
        if (index == 1)
        {
          action = DecoderAction.StartNested;
          return oldValue.Expression;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return node;
      }
      #endregion OldValue
      #region Address Dereference
      IAddressDereference addressDereference = node as IAddressDereference;
      if (addressDereference != null)
      {
        // there are 2 program points
        // 0 - evaluating the address
        // 1 - doing the dereference
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return addressDereference.Address;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return addressDereference;
      }
      #endregion Address Dereference
      #region AddressOf
      IAddressOf addressOf = node as IAddressOf;
      if (addressOf != null)
      {
        object container = addressOf.Expression.Definition;
        IExpression/*?*/ instance = addressOf.Expression.Instance;
        // This depends on the element whose address is taken
        // a) static field, local variable, or parameter (single program point)
        // b) instance field (two points, object, and ldfldaddr)
        // c) indexer (three points, array, index, and ldelema)

        ILocalDefinition/*?*/ local = container as ILocalDefinition;
        if (local != null) { // (a)
          action = DecoderAction.DecodeNowAndEnd;
          return addressOf;
        }
        IParameterDefinition/*?*/ parameter = container as IParameterDefinition;
        if (parameter != null) { //(a)
          action = DecoderAction.DecodeNowAndEnd;
          return addressOf;
        }
        IFieldReference/*?*/ field = container as IFieldReference;
        if (field != null) {
          if (index == 0 && instance != null) {
            action = DecoderAction.StartNested;
            return instance;
          }
          action = DecoderAction.DecodeNowAndEnd;
          return addressOf;
        }
        IArrayIndexer arrayIndexer = container as IArrayIndexer;
        if (arrayIndexer != null) { // case (c)
          if (index == 0) {
            action = DecoderAction.StartNested;
            return arrayIndexer.IndexedObject;
          }
          if (index == 1) {
            foreach (var x in arrayIndexer.Indices) {
              action = DecoderAction.StartNested;
              return x;
            }
          }
          // effect ldelema
          action = DecoderAction.DecodeNowAndEnd;
          return addressOf;
        }
        var addressDeref = container as IAddressDereference;
        if (addressDeref != null) {
          if (index == 0) {
            action = DecoderAction.StartNested;
            return addressDeref.Address;
          }
          action = DecoderAction.NopAndEnd;
          return addressOf;
        }
        IExpression/*?*/ expression = container as IExpression;
        if (expression != null) {
          // can't take the address of the return value, so model it as
          // ldresult; stloc l; ldloca l;
          // for a pseudo-local that is generated for this
          switch (index) {
            case 0:
              action = DecoderAction.StartNested;
              return container;
            case 1:
              action = DecoderAction.StoreIntoPseudoLocal;
              return addressOf;
            default:
              action = DecoderAction.DecodeNowAndEnd;
              return addressOf;
          }
        }

        //IAddressableExpression addressOfExpression = addressOf.Expression;


        //var returnValue = addressOfExpression.Definition as IReturnValue;
        //if (returnValue != null) {
        //  // can't take the address of the return value, so model it as
        //  // ldresult; stloc l; ldloca l;
        //  // for a pseudo-local that is generated for this
        //  switch (index) {
        //    case 0:
        //      action = DecoderAction.StartNested;
        //      return returnValue;
        //    case 1:
        //      action = DecoderAction.StoreIntoPseudoLocal;
        //      return addressOf;
        //    default:
        //      action = DecoderAction.DecodeNowAndEnd;
        //      return addressOf;
        //  }
        //}

        //var adressableMethodCall = addressOfExpression.Definition as IMethodCall;
        //if (adressableMethodCall != null) {
        //  // can't take the address of a method call, so model it as
        //  // call; stloc l; ldloca l;
        //  // for a pseudo-local that is generated for this
        //  switch (index) {
        //    case 0:
        //      action = DecoderAction.StartNested;
        //      return adressableMethodCall;
        //    case 1:
        //      action = DecoderAction.StoreIntoPseudoLocal;
        //      return addressOf;
        //    default:
        //      action = DecoderAction.DecodeNowAndEnd;
        //      return addressOf;
        //  }
        //}

        //if (index == 0 && addressOfExpression.Instance != null)
        //{
        //  action = DecoderAction.StartNested;
        //  return addressOfExpression.Instance;
        //}
        //action = DecoderAction.DecodeNowAndEnd;
        //return addressOf;
      }
      #endregion AddressOf
      #region Method Call
      IMethodCall methodCall = node as IMethodCall;
      if (methodCall != null)
      {
        // there are 2 cases: 1) static method call 2) instance method call
        // In case 1) there are n+1 program points if there are n arguments, one for each arg + the actual op
        // in case 2) there are n+2 program points, the target object, the arguments, and the actual call
        IMethodReference callTarget = methodCall.MethodToCall;
        List<IExpression> arguments = new List<IExpression>(methodCall.Arguments);
        if (this.mdDecoder.IsStatic(callTarget))
        {
          if (index < arguments.Count)
          {
            action = DecoderAction.StartNested;
            return arguments[index];
          }
          action = DecoderAction.DecodeNowAndEnd;
          return methodCall;
        }
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return methodCall.ThisArgument;
        }
        if (index < arguments.Count + 1)
        {
          action = DecoderAction.StartNested;
          return arguments[index - 1];
        }
        // effect the call
        action = DecoderAction.DecodeNowAndEnd;
        return methodCall;
      }
      #endregion Method Call
      #region Bound Expression
      IBoundExpression boundExpression = node as IBoundExpression;
      if (boundExpression != null)
      {
        // there are 1 or 2 program points, depending on whether the field is static
        // 0 - object whose field is read
        // 1 - instance field read
        //
        // 0 - static field read
        if (index == 0 && boundExpression.Instance != null)
        {
          action = DecoderAction.StartNested;
          return boundExpression.Instance;
        }
        // effect ldfld, ldsfld, or ldloc
        action = DecoderAction.DecodeNowAndEnd;
        return boundExpression;
      }
      #endregion Bound Expression
      #region Addressable Expression
      IAddressableExpression addressableExpression = node as IAddressableExpression;
      if (addressableExpression != null)
      {
        Contract.Assume(false, "AddressableExpressions should never get visited: dealt with at AddressOf level");
      }
      #endregion Addressable Expression
      #region VectorLength
      IVectorLength vectorLength = node as IVectorLength;
      if (vectorLength != null)
      {
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return vectorLength.Vector;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return vectorLength;
      }
      #endregion VectorLength
      #region Block Expression
      IBlockExpression blockExpression = node as IBlockExpression;
      if (blockExpression != null)
      {
        // Three program points: one for the block statement one for the expression
        // and one for the block expression itself.
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return blockExpression.BlockStatement;
        }
        if (index == 1)
        {
          action = DecoderAction.StartNested;
          return blockExpression.Expression;
        }
        action = DecoderAction.NopAndEnd;
        return blockExpression;
      }
      #endregion Block Expression
      #region Block Statement
      IBlockStatement blockStatement = node as IBlockStatement;
      if (blockStatement != null)
      {
        // One program point for each statement in the block statement
        List<IStatement> statements = new List<IStatement>(blockStatement.Statements);
        if (index < statements.Count)
        {
          action = DecoderAction.StartNested;
          return statements[index];
        }
        action = DecoderAction.NopAndEnd;
        return blockStatement;
      }
      #endregion Block Statement
      #region Assignments
      IAssignment assignment = node as IAssignment;
      if (assignment != null)
      {
        // There are 3 cases
        // a) the left-hand-side top-level is an Indexer e0[e1] := e2
        //    Then we have 4 program points.
        //    0 - e0
        //    1 - e1
        //    2 - e2
        //    3 - stelem
        //
        // b) the left hand side is an instance member binding e0.f := e1 or an indirect store *e0 := e1
        //    Then we have 3 program points
        //    0 - e0
        //    1 - e1
        //    2 - ldfld or stind
        //
        // c) the left hand side is a variable x := e0 or a static field sf := e0
        //    Then we have 2 program points
        //    0 - e0
        //    1 - st or stsfld
        var adjustedIndex = index;

        // (b)
        var addrDeref = assignment.Target.Definition as IAddressDereference;
        if (addrDeref != null)
        {
          if (index == 0)
          {
            action = DecoderAction.StartNested;
            return addrDeref.Address;
          }
          adjustedIndex = index + 1;
          goto assignmentDefault;
        }
        // (c)
        adjustedIndex += 2;
      assignmentDefault:
        // all different targets have now adjusted so that 2 means source, 3 means execute
        if (adjustedIndex == 2)
        {
          action = DecoderAction.StartNested;
          return assignment.Source;
        }
        // adjustedIndex == 3
        action = DecoderAction.DecodeNowAndEnd;
        return assignment;
      }
      #endregion Assignments
      #region Definition of Local
      ILocalDeclarationStatement localDeclaration = node as ILocalDeclarationStatement;
      if (localDeclaration != null)
      {
        if (index == 0 && localDeclaration.InitialValue != null)
        {
          System.Diagnostics.Debug.Assert(localDeclaration.InitialValue != null);
          action = DecoderAction.StartNested;
          return localDeclaration.InitialValue;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return localDeclaration;
      }
      #endregion Definition of Local
      #region Branch
      IGotoStatement gotoStatement = node as IGotoStatement;
      if (gotoStatement != null)
      {
        // effect branch return
        action = DecoderAction.DecodeNowAndEnd;
        return gotoStatement;
      }
      #endregion Branch
      #region Labeled Statement
      ILabeledStatement labeledStatement = node as ILabeledStatement;
      if (labeledStatement != null)
      {
        // we have 2 points 
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return labeledStatement.Statement;
        }
        action = DecoderAction.NopAndEnd;
        return labeledStatement;
      }
      #endregion Labeled Statement
      #region IArrayIndexer
      IArrayIndexer arrayIndexer2 = node as IArrayIndexer;
      if (arrayIndexer2 != null)
      {
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return arrayIndexer2.IndexedObject;
        }
        if (index == 1)
        {
          foreach (var x in arrayIndexer2.Indices)
          {
            action = DecoderAction.StartNested;
            return x;
          }
        }
        // effect ldelema
        action = DecoderAction.DecodeNowAndEnd;
        return arrayIndexer2;
      }
      #endregion IArrayIndexer
      #region IExpressionStatement
      IExpressionStatement iexprst = node as IExpressionStatement;
      if (iexprst != null)
      {
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return iexprst.Expression;
        }
        action = DecoderAction.NopAndEnd;
        return iexprst;
      }
      #endregion IExpressionStatement
      #region ITypeOf
      ITypeOf typeOf = node as ITypeOf;
      if (typeOf != null)
      {
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return new DummyTokenOf(typeOf.TypeToGet);
        }
        action = DecoderAction.DecodeNowAndEnd;
        return typeOf;
      }
      #endregion ITypeOf
      #region ICastIfPossible
      ICastIfPossible castIfPossible = node as ICastIfPossible;
      if (castIfPossible != null)
      {
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return castIfPossible.ValueToCast;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return castIfPossible;
      }
      #endregion ICastIfPossible
      #region ICreateObjectInstance
      ICreateObjectInstance createObjectInstance = node as ICreateObjectInstance;
      if (createObjectInstance != null)
      {
        IMethodReference callTarget = createObjectInstance.MethodToCall;
        List<IExpression> arguments = new List<IExpression>(createObjectInstance.Arguments);
        if (index < arguments.Count)
        {
          action = DecoderAction.StartNested;
          return arguments[index];
        }
        action = DecoderAction.DecodeNowAndEnd;
        return createObjectInstance;
      }
      #endregion
      #region ICheckIfInstance
      var checkIfInstance = node as ICheckIfInstance;
      if (checkIfInstance != null)
      {
        switch (index)
        {
          case 0:
            action = DecoderAction.StartNested;
            return checkIfInstance.Operand;
          default:
            action = DecoderAction.DecodeNowAndEnd;
            return checkIfInstance;
        }
      }
      #endregion
      #region IAnonymousDelegate
      var anonymousDelegate = node as IAnonymousDelegate;
      if (anonymousDelegate != null)
      {
        // need to make it look how it looks in IL so that
        // it triggers the quantifier decompilation in Clousot
        switch (index)
        {
          case 0:
            action = DecoderAction.LdInstanceForAnonymousDelegate;
            return anonymousDelegate;

          case 1:
            action = DecoderAction.LdftnForAnonymousDelegate;
            return anonymousDelegate;

          default:
            action = DecoderAction.DecodeNowAndEnd;
            return anonymousDelegate;
        }
      }
      #endregion
      #region IReturnStatement
      var returnStmt = node as IReturnStatement;
      if (returnStmt != null)
      {
        if (index == 0 && returnStmt.Expression != null)
        {
          action = DecoderAction.StartNested;
          return returnStmt.Expression;
        }
        action = DecoderAction.DecodeNowAndEnd;
        return returnStmt;
      }
      #endregion
      #region IPush
      var pushStatement = node as IPushStatement;
      if (pushStatement != null)
      {
        if (index == 0)
        {
          action = DecoderAction.StartNested;
          return pushStatement.ValueToPush;
        }
        action = DecoderAction.NopAndEnd;
        return pushStatement;
      }
      #endregion
      #region IDefaultValue
      var defaultValue = node as IDefaultValue;
      if (defaultValue != null)
      {
        var typ = defaultValue.Type;
        if (CciContractProvider.ShouldBeInitObj(typ))
        {
          switch (index)
          {
            case 0:
              action = DecoderAction.LoadLocalAddress;
              break;
            case 1:
              action = DecoderAction.InitObj;
              break;
            default:
              action = DecoderAction.DecodeNowAndEnd;
              break;
          }
          return defaultValue;
        }
        else
        {
          action = DecoderAction.DecodeNowAndEnd;
          return node;
        }
      }
      #endregion
      // all other cases points that have no successor (atomic) and no nested sub-expressions
      action = DecoderAction.DecodeNowAndEnd;
      return node;
    }

    /// <summary>
    /// Special case: need to produce "ldloca x; initobj" (which is used for a struct's default value)
    /// but the decompiler an assignment of the form "*(&x) := DefaultValue(T)" where x is of type T.
    /// So this test is intended to return "true" iff the <paramref name="typ"/> is the kind of T
    /// for which assignments of default values of that type shoudl instead be initobj instructions.
    /// </summary>
    internal static bool ShouldBeInitObj(ITypeReference typ)
    {
      Contract.Requires(typ != null);

      return (typ.IsValueType || typ is IGenericParameterReference) && !typ.IsEnum && typ.TypeCode == PrimitiveTypeCode.NotPrimitive;
    }

    #endregion Private methods

  }

  internal class ContractProviderVisitor<Data, Result, Visitor> : ICodeVisitor
   where Visitor : IVisitMSIL<CciContractPC, LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Unit, Unit, Data, Result>
  {
    internal Result result;
    internal Visitor visitor;
    internal Data data;
    internal CciContractPC pc;
    private readonly CciContractProvider parent;

    internal ContractProviderVisitor(CciContractProvider parent)
    {
      this.parent = parent;
    }

    #region ICodeVisitor Members

    public void Visit(IYieldReturnStatement yieldReturnStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IYieldBreakStatement yieldBreakStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IWhileDoStatement whileDoStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IVectorLength vectorLength)
    {
      this.result = visitor.Ldlen(pc, Unit.Value, Unit.Value, data);
    }

    public void Visit(IUnaryPlus unaryPlus)
    {
      throw new NotImplementedException();
    }

    public void Visit(IUnaryNegation unaryNegation)
    {
      this.result = visitor.Unary(pc, UnaryOperator.Neg, unaryNegation.CheckOverflow, false, Unit.Value, Unit.Value, data);
    }

    public void Visit(ITypeOf typeOf)
    {
      this.result = visitor.Call(pc, this.parent.GetTypeFromHandleAdaptor, false, false, new TypeList(Enumerable<IParameterTypeInformation>.Empty),
              Unit.Value, new UnitIndexable(1), data);
    }

    public void Visit(ITokenOf tokenOf)
    {
      object tok = tokenOf.Definition;
      IFieldReference/*?*/ frTok = tok as IFieldReference;
      if (frTok != null)
      {
        result = visitor.Ldfieldtoken(pc, FieldReferenceAdaptor.AdaptorOf(frTok), Unit.Value, data);
        goto End;
      }
      IMethodReference/*?*/ mrTok = tok as IMethodReference;
      if (mrTok != null)
      {
        result = visitor.Ldmethodtoken(pc, MethodReferenceAdaptor.AdaptorOf(mrTok), Unit.Value, data);
        goto End;
      }
      ITypeReference/*?*/ trTok = tok as ITypeReference;
      if (trTok != null)
      {
        result = visitor.Ldtypetoken(pc, TypeReferenceAdaptor.AdaptorOf(trTok), Unit.Value, data);
        goto End;
      }
      //^ assume false;
      throw new NotImplementedException();
    End:
      return;
    }

    public void Visit(ITryCatchFinallyStatement tryCatchFilterFinallyStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IThrowStatement throwStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IThisReference thisReference)
    {
      throw new NotImplementedException();
      //var thisParam = new DummyThisParameter(thisReference.Type);
      //this.result = visitor.Ldarg(pc, thisParam, false, Unit.Value, data);
    }

    public void Visit(ITargetExpression targetExpression)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISwitchStatement switchStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISwitchCase switchCase)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISubtraction subtraction)
    {
      var op = BinaryOperator.Sub;
      if (subtraction.CheckOverflow)
      {
        if (subtraction.TreatOperandsAsUnsignedIntegers)
          op = BinaryOperator.Sub_Ovf_Un;
        else
          op = BinaryOperator.Sub_Ovf;
      }
      this.result = visitor.Binary(pc, op, Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(IStackArrayCreate stackArrayCreate)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISizeOf sizeOf)
    {
      var typ = TypeReferenceAdaptor.AdaptorOf(sizeOf.TypeToSize);
      this.result = visitor.Sizeof(pc, typ, Unit.Value, data);
    }

    public void Visit(IRuntimeArgumentHandleExpression runtimeArgumentHandleExpression)
    {
      throw new NotImplementedException();
    }

    public void Visit(IRightShift rightShift)
    {
      this.result = visitor.Binary(pc, BinaryOperator.Shr, Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(IReturnStatement returnStatement)
    {
      this.result = visitor.Return(pc, Unit.Value, data);
    }

    public void Visit(IRethrowStatement rethrowStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IReturnValue returnValue)
    {
      this.result = visitor.Ldresult(pc, TypeReferenceAdaptor.AdaptorOf(returnValue.Type), Unit.Value, Unit.Value, data);
    }

    public void Visit(IResourceUseStatement resourceUseStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IRefArgument refArgument)
    {
      throw new NotImplementedException();
    }

    public void Visit(IPushStatement pushStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IPopValue popValue)
    {
      // Since IPushStatements are interpreted by just evaluating the value being pushed,
      // there is nothing to do for a pop: the value on the stack will be consumed by
      // whatever operation is using the value.
      // REVIEW: What if there is a pop instruction just to get rid of something on the stack?
      this.result = visitor.Nop(pc, data);
    }

    public void Visit(IPointerCall pointerCall)
    {
      throw new NotImplementedException();
    }

    public void Visit(IOutArgument outArgument)
    {
      throw new NotImplementedException();
    }

    public void Visit(IOnesComplement onesComplement)
    {
      this.result = visitor.Unary(pc, UnaryOperator.Not, false, false, Unit.Value, Unit.Value, data);
    }

    public void Visit(IOldValue oldValue)
    {
      ITypeReference oldExpType = oldValue.Type;
      this.result = visitor.EndOld(pc, new CciContractPC(pc.Node, 0), CciILCodeProvider.Adapt(oldExpType), Unit.Value, Unit.Value, data);
    }

    public void Visit(INotEquality notEquality)
    {
      this.result = visitor.Binary(pc, BinaryOperator.Cne_Un, Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(INamedArgument namedArgument)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMultiplication multiplication)
    {
      var op = BinaryOperator.Mul;
      if (multiplication.CheckOverflow)
      {
        if (multiplication.TreatOperandsAsUnsignedIntegers)
          op = BinaryOperator.Mul_Ovf_Un;
        else
          op = BinaryOperator.Mul_Ovf;
      }
      this.result = visitor.Binary(pc, op, Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(IModulus modulus)
    {
      this.result = visitor.Binary(pc, BinaryOperator.Rem, Unit.Value, Unit.Value, Unit.Value, data);
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public void Visit(IMethodCall methodCall)
    {
      IMethodReference mr = methodCall.MethodToCall;
      if (mr.ContainingType.InternedKey == mr.ContainingType.PlatformType.SystemDiagnosticsContractsContract.InternedKey)
      {
        int paramCount = CciILCodeProvider.ActualParameterCount(mr);
        switch (mr.Name.Value)
        {
          case "WritableBytes":
            if (paramCount == 1)
            {
              this.result = visitor.Unary(pc, UnaryOperator.WritableBytes, false, true, Unit.Value, Unit.Value, data);
              return;
            }
            break;
        }
      }

      var wrapper = MethodReferenceAdaptor.AdaptorOf(mr);
      // The method call might be to a model method. Such method references may not be able to be resolved,
      // if they were defined in a reference assembly. In that scenario, they claim to belong to a type
      // that doesn't know anything about them. So the actual model method definition has to be found and
      // returned here so that if it gets resolved (e.g., via API calls that require it to be resolved,
      // like "IsVirtual") it will not be a dummy method definition.
      var methodDefinition = mr.ResolvedMethod;
      if (methodDefinition is Dummy) {
        var containingType = mr.ContainingType;
        var modelMethods = this.parent.contractDecoder.ModelMethods(TypeReferenceAdaptor.AdaptorOf(containingType));
        foreach (var modelMethod in modelMethods) {
          if (modelMethod.reference.InternedKey == mr.InternedKey) {
            wrapper = modelMethod;
            break;
          }
        }
      }
      //var mname = MemberHelper.GetMethodSignature(methodCall.MethodToCall);
      //if (mname.Contains("Model"))
      //  Console.WriteLine("{0}: {1}", mname, methodCall.MethodToCall.InternedKey);
      if (methodCall.IsVirtualCall)
      {
        // REVIEW: What to do about constrained calls?
        // REVIEW: Need to deal with varargs
        this.result = visitor.Call(pc, wrapper, methodCall.IsTailCall, methodCall.IsVirtualCall,
          new TypeList(mr.ExtraParameters), Unit.Value, new UnitIndexable(0), data);
        return;
      }
      // REVIEW: What to do about calli?

      // regular calls?
      // REVIEW: Need to deal with varargs
      //      this.result = visitor.Call(pc, MethodReferenceAdaptor.AdaptorOf(methodCall.MethodToCall), methodCall.IsTailCall, false, new TypeList(methodCall.MethodToCall.ExtraParameters), Unit.Value, new UnitIndexable((int)methodCall.MethodToCall.ParameterCount), data);
      this.result = visitor.Call(pc, wrapper, methodCall.IsTailCall, false, new TypeList(mr.ExtraParameters), Unit.Value, new UnitIndexable(CciILCodeProvider.ActualParameterCount(mr)), data);
      return;
    }

    public void Visit(IMakeTypedReference makeTypedReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(ILogicalNot logicalNot)
    {
      this.result = visitor.Unary(pc, UnaryOperator.Not, false, false, Unit.Value, Unit.Value, data);
    }

    public void Visit(ILockStatement lockStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(ILocalDeclarationStatement localDeclarationStatement)
    {
      // was an initializer loc = Exp;
      if (localDeclarationStatement.InitialValue == null)
        this.result = visitor.Nop(pc, data);
      else
        this.result = visitor.Stloc(pc, LocalDefAdaptor.AdaptorOf(localDeclarationStatement.LocalVariable), Unit.Value, data);
    }

    public void Visit(ILessThanOrEqual lessThanOrEqual)
    {
      if (lessThanOrEqual.IsUnsignedOrUnordered)
      {
        this.result = visitor.Binary(pc, BinaryOperator.Cle_Un, Unit.Value, Unit.Value, Unit.Value, data);
      }
      else
      {
        this.result = visitor.Binary(pc, BinaryOperator.Cle, Unit.Value, Unit.Value, Unit.Value, data);
      }
    }

    public void Visit(ILessThan lessThan)
    {
      if (lessThan.IsUnsignedOrUnordered)
      {
        this.result = visitor.Binary(pc, BinaryOperator.Clt_Un, Unit.Value, Unit.Value, Unit.Value, data);
      }
      else
      {
        this.result = visitor.Binary(pc, BinaryOperator.Clt, Unit.Value, Unit.Value, Unit.Value, data);
      }
    }

    public void Visit(ILeftShift leftShift)
    {
      this.result = visitor.Binary(pc, BinaryOperator.Shl, Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(ILabeledStatement labeledStatement)
    {
      this.result = visitor.Nop(pc, data);
    }

    public void Visit(IGreaterThanOrEqual greaterThanOrEqual)
    {
      if (greaterThanOrEqual.IsUnsignedOrUnordered)
      {
        this.result = visitor.Binary(pc, BinaryOperator.Cge_Un, Unit.Value, Unit.Value, Unit.Value, data);
      }
      else
      {
        this.result = visitor.Binary(pc, BinaryOperator.Cge, Unit.Value, Unit.Value, Unit.Value, data);
      }
    }

    public void Visit(IGreaterThan greaterThan)
    {
      if (greaterThan.IsUnsignedOrUnordered)
      {
        this.result = visitor.Binary(pc, BinaryOperator.Cgt_Un, Unit.Value, Unit.Value, Unit.Value, data);
      }
      else
      {
        this.result = visitor.Binary(pc, BinaryOperator.Cgt, Unit.Value, Unit.Value, Unit.Value, data);
      }
    }

    public void Visit(IGetValueOfTypedReference getValueOfTypedReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGetTypeOfTypedReference getTypeOfTypedReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGotoSwitchCaseStatement gotoSwitchCaseStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGotoStatement gotoStatement)
    {
      this.result = visitor.Branch(pc, new CciContractPC(gotoStatement.TargetStatement), false, data);
    }

    public void Visit(IForStatement forStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IForEachStatement forEachStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IFillMemoryStatement fillMemoryStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IExpressionStatement expressionStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IExpression expression)
    {
      Contract.Requires(expression != null);

      expression.Dispatch(this);
    }
    public void Visit(IStatement statement)
    {
      Contract.Requires(statement != null);

      statement.Dispatch(this);
    }

    public void Visit(IExclusiveOr exclusiveOr)
    {
      this.result = visitor.Binary(pc, BinaryOperator.Xor, Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(IEquality equality)
    {
      this.result = visitor.Binary(pc, BinaryOperator.Ceq, Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(IEmptyStatement emptyStatement)
    {
      this.result = visitor.Nop(pc, data);
    }

    public void Visit(IDupValue dupValue)
    {
      throw new NotImplementedException();
    }

    public void Visit(IDoUntilStatement doUntilStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IDivision division)
    {
      this.result = visitor.Binary(pc, BinaryOperator.Div, Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(IDefaultValue defaultValue)
    {
      var typ = defaultValue.Type;
      var adaptedType = CciILCodeProvider.Adapt(typ);
      if (CciContractProvider.ShouldBeInitObj(typ))
      {
        var temp = LocalDefAdaptor.AdaptorOf(defaultValue);
        this.result = visitor.Ldloc(pc, temp, Unit.Value, data);
      }
      else if (typ.IsValueType)
      {
        this.result = visitor.Ldconst(pc, 0, adaptedType, Unit.Value, data);
      }
      else
      {
        this.result = visitor.Ldnull(pc, Unit.Value, data);
      }
    }

    public void Visit(IDebuggerBreakStatement debuggerBreakStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(ICreateObjectInstance createObjectInstance)
    {
      var mr = (IMethodReference)createObjectInstance.MethodToCall;
      this.result = visitor.Newobj(pc, MethodReferenceAdaptor.AdaptorOf(mr), Unit.Value, new UnitIndexable(CciILCodeProvider.ActualParameterCount(mr)), data);
    }

    public void Visit(ICreateDelegateInstance createDelegateInstance)
    {
      throw new NotImplementedException();
    }

    public void Visit(ICreateArray createArray)
    {
      var elementType = CciILCodeProvider.Adapt(createArray.ElementType);
      this.result = visitor.Newarray(pc, elementType, Unit.Value, new UnitIndexable((int)createArray.Rank), data);
    }

    public void Visit(ICopyMemoryStatement copyMemoryStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IContinueStatement continueStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IConditionalStatement conditionalStatement)
    {
      this.result = visitor.Nop(pc, data);
    }

    public void Visit(IConditional conditional)
    {
      this.result = visitor.Nop(pc, data);
    }

    public void Visit(IConversion conversion)
    {
      var sourceType = conversion.ValueToConvert.Type;
      var targetType = conversion.Type;
      if (conversion.CheckNumericRange)
        this.VisitCheckedConversion(sourceType, targetType);
      else
        this.VisitUncheckedConversion(sourceType, targetType);
      return;
    }

    private void VisitCheckedConversion(ITypeReference sourceType, ITypeReference targetType) {
      Contract.Requires(sourceType != null);
      Contract.Requires(targetType != null);

      UnaryOperator op = UnaryOperator.Not;
      switch (sourceType.TypeCode) {
        case PrimitiveTypeCode.Boolean:
          switch (targetType.TypeCode) {
            case PrimitiveTypeCode.Boolean:
            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.Int16:
            case PrimitiveTypeCode.Int32:
            case PrimitiveTypeCode.Int8:
            case PrimitiveTypeCode.UInt16:
            case PrimitiveTypeCode.UInt32:
            case PrimitiveTypeCode.UInt8:
              break;

            case PrimitiveTypeCode.Int64:
            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Int8:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int8:
            case PrimitiveTypeCode.Int16:
            case PrimitiveTypeCode.Int32:
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.UInt8:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.UInt8:
            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
            case PrimitiveTypeCode.Int32:
            case PrimitiveTypeCode.UInt32:
              break;

            case PrimitiveTypeCode.Int64:
            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Int16:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Char:
        case PrimitiveTypeCode.UInt16:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
            case PrimitiveTypeCode.Int32:
            case PrimitiveTypeCode.UInt32:
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Int32:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.UInt32:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Int64:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_I8);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.UInt64:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_U8);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Float32:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_R4);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Float64:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_R8);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.IntPtr:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_I);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Pointer:
        case PrimitiveTypeCode.Reference:
        case PrimitiveTypeCode.UIntPtr:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_U8);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        default:
          //TODO: conversion from &func to function pointer
          //Debug.Assert(false); //Only numeric conversions should be checked
          break;
      }
      Contract.Assume(op != UnaryOperator.Not);
      this.result =  visitor.Unary(pc, op, true, true, Unit.Value, Unit.Value, data);

    }

    private void VisitUncheckedConversion(ITypeReference sourceType, ITypeReference targetType) {
      Contract.Requires(sourceType != null);
      Contract.Requires(targetType != null);

      UnaryOperator op = UnaryOperator.Not;
      switch (sourceType.TypeCode) {
        case PrimitiveTypeCode.Boolean:
          switch (targetType.TypeCode) {
            case PrimitiveTypeCode.Boolean:
            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.Int16:
            case PrimitiveTypeCode.Int32:
            case PrimitiveTypeCode.Int8:
            case PrimitiveTypeCode.UInt16:
            case PrimitiveTypeCode.UInt32:
            case PrimitiveTypeCode.UInt8:
              break;

            case PrimitiveTypeCode.Int64:
            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Int8:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int8:
            case PrimitiveTypeCode.Int16:
            case PrimitiveTypeCode.Int32:
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.UInt8:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
            case PrimitiveTypeCode.UInt8:
            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
            case PrimitiveTypeCode.Int16:
            case PrimitiveTypeCode.Int32:
            case PrimitiveTypeCode.UInt32:
              break;

            case PrimitiveTypeCode.Int64:
            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Int16:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Char:
        case PrimitiveTypeCode.UInt16:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
            case PrimitiveTypeCode.Int32:
            case PrimitiveTypeCode.UInt32:
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Int32:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.UInt32:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
            case PrimitiveTypeCode.UInt32:
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Int64:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_I8);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
            case PrimitiveTypeCode.UInt64:
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.UInt64:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_U8);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
            case PrimitiveTypeCode.UInt64:
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Float32:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  op = UnaryOperator.Conv_r4;
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Float64:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_R8);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
              op = UnaryOperator.Conv_i;
              break;

            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              op = UnaryOperator.Conv_u;
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.IntPtr:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_I);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        case PrimitiveTypeCode.Pointer:
        case PrimitiveTypeCode.Reference:
        case PrimitiveTypeCode.UIntPtr:
          switch (targetType.TypeCode) {
            //case PrimitiveTypeCode.Boolean:
            //  this.generator.Emit(OperationCode.Ldc_I4_1);
            //  this.StackSize++;
            //  this.generator.Emit(OperationCode.Conv_U8);
            //  this.generator.Emit(OperationCode.Ceq);
            //  this.StackSize--;
            //  break;

            case PrimitiveTypeCode.Int8:
              op = UnaryOperator.Conv_i1;
              break;

            case PrimitiveTypeCode.UInt8:
              op = UnaryOperator.Conv_u1;
              break;

            case PrimitiveTypeCode.Int16:
              op = UnaryOperator.Conv_i2;
              break;

            case PrimitiveTypeCode.Char:
            case PrimitiveTypeCode.UInt16:
              op = UnaryOperator.Conv_u2;
              break;

            case PrimitiveTypeCode.Int32:
              op = UnaryOperator.Conv_i4;
              break;

            case PrimitiveTypeCode.UInt32:
              op = UnaryOperator.Conv_u4;
              break;

            case PrimitiveTypeCode.Int64:
              op = UnaryOperator.Conv_i8;
              break;

            case PrimitiveTypeCode.UInt64:
              op = UnaryOperator.Conv_u8;
              break;

            case PrimitiveTypeCode.Float32:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r4;
              break;

            case PrimitiveTypeCode.Float64:
              //this.generator.Emit(OperationCode.Conv_R_Un);
              op = UnaryOperator.Conv_r8;
              break;

            case PrimitiveTypeCode.IntPtr:
            case PrimitiveTypeCode.UIntPtr:
            case PrimitiveTypeCode.Pointer:
            case PrimitiveTypeCode.Reference:
              if (sourceType.TypeCode != targetType.TypeCode)
                op = (targetType.TypeCode == PrimitiveTypeCode.UIntPtr ? UnaryOperator.Conv_u : UnaryOperator.Conv_i);
              break;

            default:
              this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
              return;
          }
          break;

        default:
          if (TypeHelper.TypesAreEquivalent(targetType, this.parent.host.PlatformType.SystemObject)) {
            this.result = visitor.Box(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
            return;
          }
          //TODO: conversion from method to (function) pointer
          if (!sourceType.IsValueType && !TypeHelper.TypesAreEquivalent(sourceType, this.parent.host.PlatformType.SystemObject) && targetType.TypeCode == PrimitiveTypeCode.IntPtr)
            op = UnaryOperator.Conv_i;
          else if (TypeHelper.TypesAreEquivalent(sourceType, this.parent.host.PlatformType.SystemObject)) {
            var mptr = targetType as IManagedPointerTypeReference;
            if (mptr != null) {
              this.result = visitor.Unbox(pc, CciILCodeProvider.Adapt(mptr.TargetType), Unit.Value, Unit.Value, data);
              return;
            } else {
              this.result = visitor.Unboxany(pc, CciILCodeProvider.Adapt(targetType), Unit.Value, Unit.Value, data);
              return;
            }
          } else if (sourceType.IsValueType && TypeHelper.IsPrimitiveInteger(targetType)) {
            //This can only be legal if sourceType is an unresolved enum type
            switch (targetType.TypeCode) {
              case PrimitiveTypeCode.Int16: op = UnaryOperator.Conv_i2; break;
              case PrimitiveTypeCode.Int32: op = UnaryOperator.Conv_i4; break;
              case PrimitiveTypeCode.Int64: op = UnaryOperator.Conv_i8; break;
              case PrimitiveTypeCode.UInt16: op = UnaryOperator.Conv_u2; break;
              case PrimitiveTypeCode.UInt32: op = UnaryOperator.Conv_u4; break;
              case PrimitiveTypeCode.UInt64: op = UnaryOperator.Conv_u8; break;
              case PrimitiveTypeCode.UInt8: op = UnaryOperator.Conv_u1; break;
              default:
                  Contract.Assume(false, "Not expected to happen. Please create an issue at https://github.com/Microsoft/CodeContracts/issues with a way to reproduce this.");
                break;
            }
          } else if (!sourceType.IsValueType) {
            this.result = visitor.Unboxany(pc, CciILCodeProvider.Adapt(sourceType), Unit.Value, Unit.Value, data);
            return;
          } else {
              Contract.Assume(false, "Not expected to happen. Please create an issue at https://github.com/Microsoft/CodeContracts/issues with a way to reproduce this.");
          }
          break;
      }
      Contract.Assume(op != UnaryOperator.Not); 
      this.result = visitor.Unary(pc, op, false, false, Unit.Value, Unit.Value, data);
    }

    public void Visit(ICompileTimeConstant constant)
    {
      if (constant.Value == null) { this.result = visitor.Ldnull(pc, Unit.Value, data); return; }
      switch (constant.Type.TypeCode)
      {
        case PrimitiveTypeCode.Boolean:
          {
            // the decompiler introduces Boolean values.
            bool value = (bool)constant.Value;
            if (value)
            {
              this.result = visitor.Ldconst(pc, (Int32)1, this.parent.mdDecoder.System_Int32, Unit.Value, data);
              return;
            }
            else
            {
              this.result = visitor.Ldconst(pc, (Int32)0, this.parent.mdDecoder.System_Int32, Unit.Value, data);
              return;
            }
          }
        case PrimitiveTypeCode.Char:
          // the decompiler introduces char values.
          {
            int value = (int)(char)constant.Value;
            this.result = visitor.Ldconst(pc, value, this.parent.mdDecoder.System_Int32, Unit.Value, data);
            return;
          }
        default:
          System.Diagnostics.Debug.Assert(constant.Type.TypeCode != PrimitiveTypeCode.Int32 || constant.Value is Int32);
          this.result = visitor.Ldconst(pc, constant.Value, CciILCodeProvider.Adapt(constant.Type), Unit.Value, data);
          return;
      }
    }

    public void Visit(ICheckIfInstance checkIfInstance)
    {
      this.result = visitor.Isinst(pc, CciILCodeProvider.Adapt(checkIfInstance.TypeToCheck), Unit.Value, Unit.Value, data);
      return;
    }

    public void Visit(ICatchClause catchClause)
    {
      throw new NotImplementedException();
    }

    public void Visit(ICastIfPossible castIfPossible)
    {
      this.result = visitor.Isinst(pc, CciILCodeProvider.Adapt(castIfPossible.TargetType), Unit.Value, Unit.Value, data);
      return;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public void Visit(IBoundExpression boundExpression)
    {
      IParameterDefinition parameterDefinition = boundExpression.Definition as IParameterDefinition;
      if (parameterDefinition != null)
      {
        this.Visit(parameterDefinition);
        return;
      }
      IFieldReference fieldReference = boundExpression.Definition as IFieldReference;
      if (fieldReference != null)
      {
        // BUGBUG!! How to tell if read is volatile? What does the decompiler do with that?
        if (fieldReference.ResolvedField.IsStatic)
        {
          this.result = visitor.Ldsfld(pc, FieldReferenceAdaptor.AdaptorOf(fieldReference), false /*fieldDefinition.Volatile*/, Unit.Value, data);
          return;
        }
        //        this.result = visitor.Ldfld(pc, fieldDefinition, false /*fieldDefinition.Volatile*/, Unit.Value, Unit.Value, data);
        this.result = visitor.Ldfld(pc, FieldReferenceAdaptor.AdaptorOf(fieldReference), false /*fieldDefinition.Volatile*/, Unit.Value, Unit.Value, data);
        return;
      }
      //IFieldDefinition fieldDefinition = boundExpression.Definition as IFieldDefinition;
      //if (fieldDefinition != null) {
      //  // BUGBUG!! How to tell if read is volatile? What does the decompiler do with that?
      //  if (fieldDefinition.IsStatic) {
      //    this.result = visitor.Ldsfld(pc, fieldDefinition, false /*fieldDefinition.Volatile*/, Unit.Value, data);
      //    return;
      //  }
      //  //        this.result = visitor.Ldfld(pc, fieldDefinition, false /*fieldDefinition.Volatile*/, Unit.Value, Unit.Value, data);
      //  this.result = visitor.Ldfld(pc, FieldReferenceAdaptor.AdaptorOf(fieldDefinition), false /*fieldDefinition.Volatile*/, Unit.Value, Unit.Value, data);
      //  return;
      //}
      ILocalDefinition localDefinition = boundExpression.Definition as ILocalDefinition;
      if (localDefinition != null)
      {
        System.Diagnostics.Contracts.Contract.Assume(boundExpression.Instance == null, "local definition with non-null instance!");
        this.result = visitor.Ldloc(pc, LocalDefAdaptor.AdaptorOf(localDefinition), Unit.Value, data);
        return;
      }
      //if (boundExpression.Definition != null) {
      //}
      if (boundExpression.Instance != null)
      {
        this.Visit(boundExpression.Instance);
      }

    }

    public void Visit(IBreakStatement breakStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IBlockStatement block)
    {
      throw new NotImplementedException();
    }

    public void Visit(IBlockExpression blockExpression)
    {
      result = visitor.Nop(pc, data); // REVIEW WITH MAF
      return;
    }

    public void Visit(IBitwiseOr bitwiseOr)
    {
      this.result = visitor.Binary(pc, BinaryOperator.Or, Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(IBitwiseAnd bitwiseAnd)
    {
      this.result = visitor.Binary(pc, BinaryOperator.And, Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(IAssumeStatement assumeStatement)
    {
      throw new NotImplementedException();
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public void Visit(IAssignment assignment)
    {
      ITargetExpression targetExpression = assignment.Target as ITargetExpression;
      if (targetExpression != null)
      {
        ILocalDefinition localDefinition = targetExpression.Definition as ILocalDefinition;
        if (localDefinition != null)
        {
          this.result = visitor.Stloc(pc, LocalDefAdaptor.AdaptorOf(localDefinition), Unit.Value, data);
          return;
        }
        IFieldDefinition fieldDefinition = targetExpression.Definition as IFieldDefinition;
        if (fieldDefinition != null)
        {
          this.result = visitor.Stfld(pc, FieldReferenceAdaptor.AdaptorOf(fieldDefinition), targetExpression.IsVolatile, Unit.Value, Unit.Value, data);
          return;
        }
        IAddressDereference addressDereference = targetExpression.Definition as IAddressDereference;
        if (addressDereference != null)
        {
          var typ = assignment.Type;
          var targetType = addressDereference.Type;
          this.result = visitor.Stind(pc, TypeReferenceAdaptor.AdaptorOf(targetType), addressDereference.IsVolatile, Unit.Value, Unit.Value, data);
          return;
        }
      }
      throw new NotImplementedException();
    }

    public void Visit(IAssertStatement assertStatement)
    {
      throw new NotImplementedException();
    }

    public void Visit(IArrayIndexer arrayIndexer)
    {
      this.result = visitor.Ldelem(pc, TypeReferenceAdaptor.AdaptorOf(arrayIndexer.Type), Unit.Value, Unit.Value, Unit.Value, data);
    }

    public void Visit(IAnonymousDelegate anonymousMethod)
    {
      // visit.ldloc
      //return visit.Ldftn(pc, Method(ref pc), Unit.Value, data);
      var tr = anonymousMethod.Type;
      var host = this.parent.host;
      var platformType = host.PlatformType;
      var mr = new MethodReference(host, tr, CallingConvention.Default | CallingConvention.HasThis,
        platformType.SystemVoid, host.NameTable.Ctor, 0, platformType.SystemObject, platformType.SystemIntPtr);

      this.result = visitor.Newobj(pc, MethodReferenceAdaptor.AdaptorOf(mr), Unit.Value, new UnitIndexable(CciILCodeProvider.ActualParameterCount(mr)), data);

      // TODO: This is *not* the right thing to return. Need to figure out what the right thing is...
      //this.result = visitor.Ldconst(pc, anonymousMethod, CciILCodeProvider.Adapt(anonymousMethod.Type), Unit.Value, data);
    }

    public void Visit(IAddressOf addressOf)
    {
      IParameterDefinition parameterDefinition = addressOf.Expression.Definition as IParameterDefinition;
      if (parameterDefinition != null)
      {
        this.result = visitor.Ldarga(pc, parameterDefinition, false, Unit.Value, data);
        return;
      }
      IFieldReference fieldReference = addressOf.Expression.Definition as IFieldReference;
      if (fieldReference != null)
      {
        if (fieldReference.IsStatic)
        {
          this.result = visitor.Ldsflda(pc, FieldReferenceAdaptor.AdaptorOf(fieldReference), Unit.Value, data);
        }
        else
        {
          this.result = visitor.Ldflda(pc, FieldReferenceAdaptor.AdaptorOf(fieldReference), Unit.Value, Unit.Value, data);
        }
        return;
      }
      ILocalDefinition localDefinition = addressOf.Expression.Definition as ILocalDefinition;
      if (localDefinition != null)
      {
        this.result = visitor.Ldloca(pc, LocalDefAdaptor.AdaptorOf(localDefinition), Unit.Value, data);
        return;
      }
      IArrayIndexer arrayIndexer = addressOf.Expression.Definition as IArrayIndexer;
      if (arrayIndexer != null)
      {
        this.result = visitor.Ldelema(pc, TypeReferenceAdaptor.AdaptorOf(arrayIndexer.Type), addressOf.ObjectControlsMutability, Unit.Value, Unit.Value, Unit.Value, data);
        return;
      }
      var expression = addressOf.Expression.Definition as IExpression;
      if (expression != null) {
        this.result = visitor.Ldloca(pc, LocalDefAdaptor.AdaptorOf(addressOf), Unit.Value, data);
        return;
      }
      //var returnValue = addressOf.Expression.Definition as IReturnValue;
      //if (returnValue != null)
      //{
      //  this.result = visitor.Ldloca(pc, LocalDefAdaptor.AdaptorOf(addressOf), Unit.Value, data);
      //  return;
      //}
      //var methodCall = addressOf.Expression.Definition as IMethodCall;
      //if (methodCall != null)
      //{
      //  this.result = visitor.Ldloca(pc, LocalDefAdaptor.AdaptorOf(addressOf), Unit.Value, data);
      //  return;
      //}
      throw new NotImplementedException();
    }

    public void Visit(IAddressDereference addressDereference)
    {
      this.result = visitor.Ldind(pc, CciILCodeProvider.Adapt(addressDereference.Type), addressDereference.IsVolatile, Unit.Value, Unit.Value, data);
    }

    public void Visit(IAddressableExpression addressableExpression)
    {
      IParameterDefinition parameterDefinition = addressableExpression.Definition as IParameterDefinition;
      if (parameterDefinition != null)
      {
        this.result = visitor.Ldarga(pc, parameterDefinition, false, Unit.Value, data);
        return;
      }
      ILocalDefinition localDefinition = addressableExpression.Definition as ILocalDefinition;
      if (localDefinition != null)
      {
        this.result = visitor.Ldloca(pc, LocalDefAdaptor.AdaptorOf(localDefinition), Unit.Value, data);
        return;
      }
      IFieldReference fieldReference = addressableExpression.Definition as IFieldReference;
      if (fieldReference != null)
      {
        // BUGBUG!! How to tell if read is volatile? What does the decompiler do with that?
        if (fieldReference.IsStatic)
        {
          this.result = visitor.Ldsfld(pc, FieldReferenceAdaptor.AdaptorOf(fieldReference), false /*fieldDefinition.Volatile*/, Unit.Value, data);
          return;
        }
        //        this.result = visitor.Ldfld(pc, fieldDefinition, false /*fieldDefinition.Volatile*/, Unit.Value, Unit.Value, data);
        this.result = visitor.Ldfld(pc, FieldReferenceAdaptor.AdaptorOf(fieldReference), false /*fieldDefinition.Volatile*/, Unit.Value, Unit.Value, data);
        return;
      }
      //IFieldDefinition fieldDefinition = boundExpression.Definition as IFieldDefinition;
      //if (fieldDefinition != null) {
      //  // BUGBUG!! How to tell if read is volatile? What does the decompiler do with that?
      //  if (fieldDefinition.IsStatic) {
      //    this.result = visitor.Ldsfld(pc, fieldDefinition, false /*fieldDefinition.Volatile*/, Unit.Value, data);
      //    return;
      //  }
      //  //        this.result = visitor.Ldfld(pc, fieldDefinition, false /*fieldDefinition.Volatile*/, Unit.Value, Unit.Value, data);
      //  this.result = visitor.Ldfld(pc, FieldReferenceAdaptor.AdaptorOf(fieldDefinition), false /*fieldDefinition.Volatile*/, Unit.Value, Unit.Value, data);
      //  return;
      //}
      if (addressableExpression.Definition != null)
      {
      }
      if (addressableExpression.Instance != null)
      {
        this.Visit(addressableExpression.Instance);
      }
    }

    public void Visit(IAddition addition)
    {
      var op = BinaryOperator.Add;
      if (addition.CheckOverflow)
      {
        if (addition.TreatOperandsAsUnsignedIntegers)
          op = BinaryOperator.Add_Ovf_Un;
        else
          op = BinaryOperator.Add_Ovf;
      }
      this.result = visitor.Binary(pc, op, Unit.Value, Unit.Value, Unit.Value, data);
    }

    #endregion

    #region IMetadataVisitor Members

    public void Visit(IWin32Resource win32Resource)
    {
      throw new NotImplementedException();
    }

    public void Visit(IUnitSet unitSet)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISecurityAttribute securityAttribute)
    {
      throw new NotImplementedException();
    }

    public void Visit(IRootUnitSetNamespace rootUnitSetNamespace)
    {
      throw new NotImplementedException();
    }

    public void Visit(IRootUnitNamespaceReference rootUnitNamespaceReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IRootUnitNamespace rootUnitNamespace)
    {
      throw new NotImplementedException();
    }

    public void Visit(IResourceReference resourceReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IPropertyDefinition propertyDefinition)
    {
      throw new NotImplementedException();
    }

    public void Visit(IPointerTypeReference pointerTypeReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IPESection peSection)
    {
      throw new NotImplementedException();
    }

    public void Visit(IParameterTypeInformation parameterTypeInformation)
    {
      throw new NotImplementedException();
    }

    public void Visit(IParameterDefinition parameterDefinition)
    {

      IParameterTypeInformation p;
      var sig = parameterDefinition.ContainingSignature;
      if (parameterDefinition.Index == ushort.MaxValue)
      {
        IMethodReference mref = (IMethodReference)sig;
        p = this.parent.mdDecoder.This(MethodReferenceAdaptor.AdaptorOf(mref));
        this.result = visitor.Ldarg(pc, p, false, Unit.Value, data);
        return;
      }
      else
      {
        //mref = CciMetadataDecoder.Unspecialized(mref);
        var ps = new List<IParameterTypeInformation>(sig.Parameters);
        p = ps[parameterDefinition.Index];
        this.result = visitor.Ldarg(pc, p, false, Unit.Value, data);
        return;
      }
    }

    public void Visit(INestedUnitSetNamespace nestedUnitSetNamespace)
    {
      throw new NotImplementedException();
    }

    public void Visit(INestedUnitNamespaceReference nestedUnitNamespaceReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(INestedUnitNamespace nestedUnitNamespace)
    {
      throw new NotImplementedException();
    }

    public void Visit(INestedTypeReference nestedTypeReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(INestedTypeDefinition nestedTypeDefinition)
    {
      throw new NotImplementedException();
    }

    public void Visit(INestedAliasForType nestedAliasForType)
    {
      throw new NotImplementedException();
    }

    public void Visit(INamespaceTypeReference namespaceTypeReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(INamespaceTypeDefinition namespaceTypeDefinition)
    {
      throw new NotImplementedException();
    }

    public void Visit(INamespaceAliasForType namespaceAliasForType)
    {
      throw new NotImplementedException();
    }

    public void Visit(IModuleReference moduleReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IModule module)
    {
      throw new NotImplementedException();
    }

    public void Visit(IModifiedTypeReference modifiedTypeReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMethodReference methodReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMethodImplementation methodImplementation)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMethodDefinition method)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMethodBody methodBody)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMetadataTypeOf typeOf)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMetadataNamedArgument namedArgument)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMetadataExpression expression)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMetadataCreateArray createArray)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMetadataConstant constant)
    {
      throw new NotImplementedException();
    }

    public void Visit(IMarshallingInformation marshallingInformation)
    {
      throw new NotImplementedException();
    }

    public void Visit(IManagedPointerTypeReference managedPointerTypeReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGenericTypeParameterReference genericTypeParameterReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGenericTypeParameter genericTypeParameter)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGenericTypeInstanceReference genericTypeInstanceReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGlobalMethodDefinition globalMethodDefinition)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGlobalFieldDefinition globalFieldDefinition)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGenericMethodParameterReference genericMethodParameterReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGenericMethodParameter genericMethodParameter)
    {
      throw new NotImplementedException();
    }

    public void Visit(IGenericMethodInstanceReference genericMethodInstanceReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IFunctionPointerTypeReference functionPointerTypeReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IFileReference fileReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IFieldReference fieldReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IFieldDefinition fieldDefinition)
    {
      // BUGBUG!! How to tell if read is volatile? What does the decompiler do with that?
      if (fieldDefinition.IsStatic)
      {
        this.result = visitor.Ldsfld(pc, FieldReferenceAdaptor.AdaptorOf(fieldDefinition), false /*fieldDefinition.Volatile*/, Unit.Value, data);
        return;
      }
      //        this.result = visitor.Ldfld(pc, fieldDefinition, false /*fieldDefinition.Volatile*/, Unit.Value, Unit.Value, data);
      this.result = visitor.Ldfld(pc, FieldReferenceAdaptor.AdaptorOf(fieldDefinition), false /*fieldDefinition.Volatile*/, Unit.Value, Unit.Value, data);
      return;
    }

    public void Visit(IEventDefinition eventDefinition)
    {
      throw new NotImplementedException();
    }

    public void Visit(ICustomModifier customModifier)
    {
      throw new NotImplementedException();
    }

    public void Visit(ICustomAttribute customAttribute)
    {
      throw new NotImplementedException();
    }

    public void Visit(IAssemblyReference assemblyReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IAssembly assembly)
    {
      throw new NotImplementedException();
    }

    public void Visit(IArrayTypeReference arrayTypeReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(IAliasForType aliasForType)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region Unimplemented IMetadataVisitor Members


    public void Visit(IPlatformInvokeInformation platformInvokeInformation)
    {
      throw new NotImplementedException();
    }

    public void Visit(IOperationExceptionInformation operationExceptionInformation)
    {
      throw new NotImplementedException();
    }

    public void Visit(IOperation operation)
    {
      throw new NotImplementedException();
    }

    public void Visit(ILocalDefinition localDefinition)
    {
      throw new NotImplementedException();
    }

    public void VisitReference(IParameterDefinition parameterDefinition)
    {
      Visit(parameterDefinition);
    }

    public void VisitReference(ILocalDefinition localDefinition)
    {
      Visit(localDefinition);
    }

    public void Visit(ISpecializedNestedTypeReference specializedNestedTypeReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISpecializedNestedTypeDefinition specializedNestedTypeDefinition)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISpecializedPropertyDefinition specializedPropertyDefinition)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISpecializedMethodReference specializedMethodReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISpecializedMethodDefinition specializedMethodDefinition)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISpecializedFieldReference specializedFieldReference)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISpecializedFieldDefinition specializedFieldDefinition)
    {
      throw new NotImplementedException();
    }

    public void Visit(ISpecializedEventDefinition specializedEventDefinition)
    {
      throw new NotImplementedException();
    }
    #endregion

  }

  public class CciILCodeProvider : IDisposable,
    IMethodCodeProvider<CciILPC, LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, CciExceptionHandlerInfo>
  {

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.metaDataDecoder != null);
      Contract.Invariant(this.disposableObjectAllocatedByThisHost != null);
    }

    public static CciILCodeProvider CreateCodeProvider()
    {
      Contract.Ensures(Contract.Result<CciILCodeProvider>() != null);

      var host = new HostEnvironment();
      var provider = CreateCodeProvider(host);
      provider.disposableObjectAllocatedByThisHost.Add(host);
      return provider;
    }
    public static CciILCodeProvider CreateCodeProvider(HostEnvironment host)
    {
      Contract.Requires(host != null);
      return new CciILCodeProvider(host, null/*platformType*/);
    }

    private readonly CodeContractAwareHostEnvironment host;
    private readonly CciContractDecoder contractDecoder;
    private readonly CciMetadataDecoder metaDataDecoder;
    private IPlatformType _platformType;
    private CciContractProvider contractProvider;
    protected List<IDisposable> disposableObjectAllocatedByThisHost = new List<IDisposable>();

    private CciILCodeProvider(HostEnvironment host, IPlatformType platformType)
    {
      Contract.Requires(host != null);

      this._platformType = platformType;
      this.metaDataDecoder = new CciMetadataDecoder(this, host, platformType);
      this.contractDecoder = new CciContractDecoder(this, host, this.metaDataDecoder);
      this.host = host;
    }

    #region IDisposable members & co

    private void Close()
    {
      foreach (var disposable in this.disposableObjectAllocatedByThisHost)
        disposable.Dispose();
    }

    public virtual void Dispose()
    {
      this.Close();
      GC.SuppressFinalize(this);
    }

    ~CciILCodeProvider()
    {
      this.Close();
    }

    #endregion

    internal IPlatformType platformType
    {
      get { if (_platformType == null) _platformType = this.host.PlatformType; return _platformType; }
    }

    internal CciContractProvider ContractProvider // MB: I do not know how often this is needed, if it is always constructed then we'd better put it as a readonly field
    {
      get
      {
        if (this.contractProvider == null)
          this.contractProvider = new CciContractProvider(this.host, this.MetaDataDecoder, this.contractDecoder);
        return this.contractProvider;
      }
    }

    public CciContractDecoder ContractDecoder
    {
      get { return this.contractDecoder; }
    }

    public CciMetadataDecoder MetaDataDecoder
    {
      get { return this.metaDataDecoder; }
    }

    #region ICodeProvider<TypeReferenceAdaptor,MethodReferenceAdaptor,PC,ExceptionHandler> Members

    internal static TypeReferenceAdaptor Adapt(ITypeReference type)
    {
      return TypeReferenceAdaptor.AdaptorOf(type);
    }

    private static CciILPC SingletonTargetPC(CciILPC origin)
    {
      return new CciILPC(origin.Context, FindIndexFor(origin.Operations, (uint)origin.Operation.Value));
    }

    private IEnumerable<Pair<object, CciILPC>> ListOfTargetPCs(CciILPC origin)
    {
      IList<IOperation> operations = origin.Operations;
      int[] targets = (int[])origin.Operation.Value;
      for (int i = 0; i < targets.Length; i++)
      {
        uint target = (uint)targets[i];
        yield return new Pair<object, CciILPC>(i, new CciILPC(origin.Context, FindIndexFor(operations, target)));
      }
    }

    internal static int FindIndexFor(IList<IOperation> operations, uint offset)
    {
      Contract.Requires(operations != null);

      for (int i = 0, n = operations.Count; i < n; i++)
      {
        if (operations[i].Offset == offset) return i;
      }
      return 0; // REVIEW: But this is a valid index?! Shouldn't it be -1?
    }

    public bool Next(CciILPC current, out CciILPC nextLabel)
    {
      return current.Next(out nextLabel);
    }

    public CciILPC Entry(MethodReferenceAdaptor method)
    {
      return this.contractDecoder.GetContextForMethodBody(method);
    }

    public bool HasBody(IMethodReference method)
    {
      Contract.Requires(method != null);

      IMethodDefinition methodDef = method.ResolvedMethod;
      if (methodDef is ISpecializedMethodDefinition) return false;
      return !methodDef.IsAbstract && !methodDef.IsExternal;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public bool IsConstructor(IMethodReference method)
    {
      Contract.Requires(method != null);

      IMethodDefinition methodDef = method.ResolvedMethod;
      return methodDef != null && methodDef.IsConstructor;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public bool IsInterface(TypeReferenceAdaptor type)
    {
      Contract.Assume(type.reference != null);
      ITypeDefinition typeDef = type.ResolvedType;
      return typeDef != null && typeDef.IsInterface;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public bool IsVirtual(IMethodReference method)
    {
      Contract.Requires(method != null);

      IMethodDefinition methodDef = method.ResolvedMethod;
      return methodDef != null && methodDef.IsVirtual;
    }

    public TypeReferenceAdaptor DeclaringType(IMethodReference method)
    {
      Contract.Requires(method != null);

      return Adapt(method.ContainingType);
    }

    public IEnumerable<IMethodReference> OverriddenAndImplementedMethods(IMethodReference method)
    {
      var methodDefinition = method.ResolvedMethod;
      foreach (var m in MemberHelper.GetExplicitlyOverriddenMethods(methodDefinition))
      {
        yield return m;
      }
      var m2 = MemberHelper.GetImplicitlyOverriddenBaseClassMethod(methodDefinition);
      if (m2 != null && (!(m2 is Dummy)))
        yield return m2;

      foreach (var m3 in MemberHelper.GetImplicitlyImplementedInterfaceMethods(methodDefinition))
      {
        yield return m3;
      }

      yield break;
    }

    public CciILPC Invariant(TypeReferenceAdaptor type)
    {
      return new CciILPC();
    }

    public IEnumerable<CciExceptionHandlerInfo> TryBlocks(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      IMethodDefinition methodDef = method.ResolvedMethod;
      if (methodDef == null) yield break;
      CciILPCMethodBodyContext context = new CciILPCMethodBodyContext(method, new List<IOperation>(methodDef.Body.Operations));
      foreach (IOperationExceptionInformation info in methodDef.Body.OperationExceptionInformation)
        yield return new CciExceptionHandlerInfo(info, method, context);
    }

    public bool IsFaultHandler(CciExceptionHandlerInfo info)
    {
      return info.Info.HandlerKind == HandlerKind.Fault;
    }

    public bool IsFinallyHandler(CciExceptionHandlerInfo info)
    {
      return info.Info.HandlerKind == HandlerKind.Finally;
    }

    public bool IsCatchHandler(CciExceptionHandlerInfo info)
    {
      return info.Info.HandlerKind == HandlerKind.Catch;
    }

    public bool IsFilterHandler(CciExceptionHandlerInfo info)
    {
      return info.Info.HandlerKind == HandlerKind.Filter;
    }

    public TypeReferenceAdaptor CatchType(CciExceptionHandlerInfo info)
    {
      return Adapt(info.Info.ExceptionType);
    }

    public bool IsCatchAllHandler(CciExceptionHandlerInfo info)
    {
      return info.Info.ExceptionType is Dummy;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public CciILPC TryStart(CciExceptionHandlerInfo info)
    {
      IMethodDefinition methodDef = info.Method.ResolvedMethod;
      if (methodDef == null) methodDef = Dummy.Method;
      return new CciILPC(info.Context, FindIndexFor(info.Context.Operations, info.Info.TryStartOffset));
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public CciILPC TryEnd(CciExceptionHandlerInfo info)
    {
      IMethodDefinition methodDef = info.Method.ResolvedMethod;
      if (methodDef == null) methodDef = Dummy.Method;
      return new CciILPC(info.Context, FindIndexFor(info.Context.Operations, info.Info.TryEndOffset));
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public CciILPC FilterDecisionStart(CciExceptionHandlerInfo info)
    {
      IMethodDefinition methodDef = info.Method.ResolvedMethod;
      if (methodDef == null) methodDef = Dummy.Method;
      return new CciILPC(info.Context, FindIndexFor(info.Context.Operations, info.Info.FilterDecisionStartOffset));
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public CciILPC HandlerStart(CciExceptionHandlerInfo info)
    {
      IMethodDefinition methodDef = info.Method.ResolvedMethod;
      if (methodDef == null) methodDef = Dummy.Method;
      return new CciILPC(info.Context, FindIndexFor(info.Context.Operations, info.Info.HandlerStartOffset));
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public CciILPC HandlerEnd(CciExceptionHandlerInfo info)
    {
      IMethodDefinition methodDef = info.Method.ResolvedMethod;
      if (methodDef == null) methodDef = Dummy.Method;
      return new CciILPC(info.Context, FindIndexFor(info.Context.Operations, info.Info.HandlerEndOffset));
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public bool IsVoidMethod(IMethodReference method)
    {
      Contract.Requires(method != null);

      ITypeDefinition typeDef = method.Type.ResolvedType;
      return typeDef != null && typeDef.TypeCode == PrimitiveTypeCode.Void;
    }

    public string MethodName(IMethodReference method)
    {
      Contract.Requires(method != null);

      return method.Name.Value;
    }

    public void PrintCodeAt(CciILPC pc, string indent, System.IO.TextWriter tw)
    {
      Contract.Requires(tw != null);

      tw.Write(indent);
      IPrimarySourceLocation/*?*/ ploc = pc.Operation.Location as IPrimarySourceLocation;
      if (ploc != null) tw.Write(ploc.Source); else tw.Write(pc.Operation.OperationCode);
      tw.WriteLine();
    }

    private IPrimarySourceLocation/*?*/ GetPrimarySourceLocation(CciILPC pc)
    {
      Contract.Assume(pc.Context != null);
      var unit = TypeHelper.GetDefiningUnitReference(pc.Context.Method.ContainingType);
      int dummy; // don't care about the index where the location may be found
      var primaryLocations = this.metaDataDecoder.GetPrimarySourceLocationsFor(unit, pc.Operations, pc.RealIndex, false, out dummy);
      Contract.Assert(primaryLocations != null);
      foreach (IPrimarySourceLocation psloc in primaryLocations)
      {
        if (psloc != null) return psloc;
      }
      return null;
    }

    public bool HasSourceContext(CciILPC pc)
    {
      return GetPrimarySourceLocation(pc) != null;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    private static T First<T>(IEnumerable<T> enumerable) where T : class
    {
      if (enumerable == null) return null;
      var enumerator = enumerable.GetEnumerator();
      if (enumerator == null) return null;
      if (!enumerator.MoveNext()) return null;
      return enumerator.Current;
    }

    public string SourceDocument(CciILPC pc)
    {
      var psloc = GetPrimarySourceLocation(pc);
      if (psloc != null)
      {
        IIncludedSourceLocation iloc = psloc as IIncludedSourceLocation;
        if (iloc != null) return iloc.OriginalSourceDocumentName;
        return psloc.Document.Location; //TODO: perhaps Location?
      }
      IBinaryLocation bloc = pc.Operation.Location as IBinaryLocation;
      if (bloc != null) return bloc.Document.Location;
      return "unknown source document";
    }

    public string SourceAssertionCondition(CciILPC pc)
    {
      return null;
    }

    public int SourceStartLine(CciILPC pc)
    {
      var psloc = GetPrimarySourceLocation(pc);
      if (psloc != null)
      {
        IIncludedSourceLocation iloc = psloc as IIncludedSourceLocation;
        if (iloc != null) return iloc.OriginalStartLine;
        return psloc.StartLine;
      }
      return 0;
    }

    public int SourceEndLine(CciILPC pc)
    {
      var psloc = GetPrimarySourceLocation(pc);
      if (psloc != null)
      {
        IIncludedSourceLocation iloc = psloc as IIncludedSourceLocation;
        if (iloc != null) return iloc.OriginalEndLine;
        return psloc.EndLine;
      }
      return 0;
    }

    public int SourceStartColumn(CciILPC pc)
    {
      var psloc = GetPrimarySourceLocation(pc);
      if (psloc != null) return psloc.StartColumn;
      return 0;
    }

    public int SourceEndColumn(CciILPC pc)
    {
      var psloc = GetPrimarySourceLocation(pc);
      if (psloc != null) return psloc.EndColumn;
      return 0;
    }

    public int SourceStartIndex(CciILPC pc)
    {
      var psloc = GetPrimarySourceLocation(pc);
      if (psloc != null) return psloc.StartIndex;
      return 0;
    }

    public int SourceLength(CciILPC pc)
    {
      var psloc = GetPrimarySourceLocation(pc);
      if (psloc != null) return psloc.Length;
      return 0;
    }

    public int ILOffset(CciILPC pc)
    {
      IBinaryLocation bloc = pc.Operation.Location as IBinaryLocation;
      if (bloc != null) return (int)bloc.Offset;
      return (int)pc.Operation.Offset;
    }

    #endregion


    #region IDecodeMSIL<CciILPC,ILocalDefinition,IParameterTypeInformation,MethodReferenceAdaptor,FieldReferenceAdaptor,TypeReferenceAdaptor,Unit,Unit,Unit> Members

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public Result Decode<Visitor, Data, Result>(CciILPC pc, Visitor visit, Data data)
      where Visitor : ICodeQuery<CciILPC, LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Data, Result>
    {

      //if (pc.Context is CciILPCLambdaContext) {
      //  var contractProvider = CciContractProvider.Value;
      //  var anonDel = (DummyLambdaMethodReference) pc.Context.Method;
      //  var contractPC = new CciContractPC(anonDel, 0);
      //  var result = contractProvider.Decode(contractPC, visitor2, data);
      //  return result;
      //}

      var operation = pc.Operation;
      switch (operation.OperationCode)
      {
        case OperationCode.Add:
          return visit.Binary(pc, BinaryOperator.Add, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Add_Ovf:
          return visit.Binary(pc, BinaryOperator.Add_Ovf, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Add_Ovf_Un:
          return visit.Binary(pc, BinaryOperator.Add_Ovf_Un, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.And:
          return visit.Binary(pc, BinaryOperator.And, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Arglist:
          return visit.Arglist(pc, Unit.Value, data);

        case OperationCode.Array_Addr:
          {
            ITypeReference type = operation.Value as ITypeReference;
            IArrayTypeReference arrayType = type as IArrayTypeReference;
            IMethodReference mref = new DummyArrayMethodReference(arrayType, operation.OperationCode, this.platformType);
            return visit.Call(pc, MethodReferenceAdaptor.AdaptorOf(mref), Tail(ref pc), false, new TypeList(mref.ExtraParameters),
              Unit.Value, new UnitIndexable(mref.ParameterCount), data);
          }
        case OperationCode.Array_Create:
        case OperationCode.Array_Create_WithLowerBound:
          return visit.Newarray(pc, ArrayElementType(ref pc), Unit.Value, new UnitIndexable(ArrayRank(ref pc)), data);

        case OperationCode.Array_Get:
        case OperationCode.Array_Set:
          {
            ITypeReference type = operation.Value as ITypeReference;
            IArrayTypeReference arrayType = type as IArrayTypeReference;
            IMethodReference mref = new DummyArrayMethodReference(arrayType, operation.OperationCode, this.platformType);
            return visit.Call(pc, MethodReferenceAdaptor.AdaptorOf(mref), Tail(ref pc), false, new TypeList(mref.ExtraParameters),
              Unit.Value, new UnitIndexable(mref.ParameterCount), data);
            //goto default; //TODO: figure out if the original call to the dummy method is recognized and dealt with.
          }
        case OperationCode.Beq:
        case OperationCode.Beq_S:
          return visit.BranchCond(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), BranchOperator.Beq, Unit.Value, Unit.Value, data);

        case OperationCode.Bge:
        case OperationCode.Bge_S:
          return visit.BranchCond(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), BranchOperator.Bge, Unit.Value, Unit.Value, data);

        case OperationCode.Bge_Un:
        case OperationCode.Bge_Un_S:
          return visit.BranchCond(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), BranchOperator.Bge_un, Unit.Value, Unit.Value, data);

        case OperationCode.Bgt:
        case OperationCode.Bgt_S:
          return visit.BranchCond(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), BranchOperator.Bgt, Unit.Value, Unit.Value, data);

        case OperationCode.Bgt_Un:
        case OperationCode.Bgt_Un_S:
          return visit.BranchCond(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), BranchOperator.Bgt_un, Unit.Value, Unit.Value, data);

        case OperationCode.Ble:
        case OperationCode.Ble_S:
          return visit.BranchCond(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), BranchOperator.Ble, Unit.Value, Unit.Value, data);

        case OperationCode.Ble_Un:
        case OperationCode.Ble_Un_S:
          return visit.BranchCond(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), BranchOperator.Ble_un, Unit.Value, Unit.Value, data);

        case OperationCode.Blt:
        case OperationCode.Blt_S:
          return visit.BranchCond(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), BranchOperator.Blt, Unit.Value, Unit.Value, data);

        case OperationCode.Blt_Un:
        case OperationCode.Blt_Un_S:
          return visit.BranchCond(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), BranchOperator.Blt_un, Unit.Value, Unit.Value, data);

        case OperationCode.Bne_Un:
        case OperationCode.Bne_Un_S:
          return visit.BranchCond(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), BranchOperator.Bne_un, Unit.Value, Unit.Value, data);

        case OperationCode.Brfalse:
        case OperationCode.Brfalse_S:
          return visit.BranchFalse(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), Unit.Value, data);

        case OperationCode.Brtrue:
        case OperationCode.Brtrue_S:
          return visit.BranchTrue(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), Unit.Value, data);

        case OperationCode.Br:
        case OperationCode.Br_S:
          return visit.Branch(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), false, data);

        case OperationCode.Box:
          return visit.Box(pc, Type(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Break:
          return visit.Break(pc, data);

        case OperationCode.Call:
          IMethodReference mr = (IMethodReference)operation.Value;
          int paramCount = ActualParameterCount(mr);
          if (mr.ContainingType.InternedKey == mr.ContainingType.PlatformType.SystemDiagnosticsContractsContract.InternedKey)
          {
            switch (mr.Name.Value)
            {
              case "Assume":
                if (paramCount == 1) return visit.Assume(pc, "assume", Unit.Value, null, data);
                break;

              case "Assert":
                if (paramCount == 1) return visit.Assert(pc, "assert", Unit.Value, null, data);
                break;

              case "Ensures":
                return visit.Assert(pc, "ensures", Unit.Value, null, data);

              case "Invariant":
                return visit.Assume(pc, "invariant", Unit.Value, null, data);

              case "Requires":
                return visit.Assume(pc, "requires", Unit.Value, null, data);

              case "WritableBytes":
                if (paramCount == 1) return visit.Unary(pc, UnaryOperator.WritableBytes, false, true, Unit.Value, Unit.Value, data);
                break;

              case "ValueAtReturn":
                IGenericMethodInstanceReference gmir = (IGenericMethodInstanceReference)mr;
                ITypeReference returnType = gmir.GenericArguments.AsIndexable(1)[0];
                return visit.Ldind(pc, TypeReferenceAdaptor.AdaptorOf(returnType), false, Unit.Value, Unit.Value, data);

            }
          }
          return visit.Call(pc, MethodReferenceAdaptor.AdaptorOf(mr), Tail(ref pc), false, new TypeList(mr.ExtraParameters), Unit.Value,
                            new UnitIndexable(ActualParameterCount(mr)), data);

        case OperationCode.Calli:
          IFunctionPointerTypeReference fpr = (IFunctionPointerTypeReference)operation.Value;
          bool isInstanceCall = (fpr.CallingConvention & CallingConvention.HasThis) != 0;
          int fprParamCount = (int)IteratorHelper.EnumerableCount(fpr.Parameters) + (isInstanceCall ? 2 : 1);
          return visit.Calli(pc, Adapt(fpr.Type), new TypeList(fpr.Parameters), Tail(ref pc), isInstanceCall, Unit.Value, Unit.Value, new UnitIndexable(fprParamCount), data);

        case OperationCode.Callvirt:
          mr = (IMethodReference)operation.Value;
          TypeReferenceAdaptor/*?*/ constraint = Constraint(ref pc);
          if (constraint.reference != null)
            return visit.ConstrainedCallvirt(pc, MethodReferenceAdaptor.AdaptorOf(mr), Tail(ref pc), constraint, new TypeList(mr.ExtraParameters), Unit.Value, new ArrayIndexable<Unit>(null), data);
          else
            return visit.Call(pc, MethodReferenceAdaptor.AdaptorOf(mr), Tail(ref pc), true, new TypeList(mr.ExtraParameters), Unit.Value, new UnitIndexable(ActualParameterCount(mr)), data);

        case OperationCode.Castclass:
          return visit.Castclass(pc, Type(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ceq:
          return visit.Binary(pc, BinaryOperator.Ceq, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Cgt:
          return visit.Binary(pc, BinaryOperator.Cgt, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Cgt_Un:
          return visit.Binary(pc, BinaryOperator.Cgt_Un, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ckfinite:
          return visit.Ckfinite(pc, Unit.Value, Unit.Value, data);

        case OperationCode.Clt:
          return visit.Binary(pc, BinaryOperator.Clt, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Clt_Un:
          return visit.Binary(pc, BinaryOperator.Clt_Un, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Constrained_:
          return visit.Nop(pc, data);

        case OperationCode.Conv_I:
          return visit.Unary(pc, UnaryOperator.Conv_i, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_I1:
          return visit.Unary(pc, UnaryOperator.Conv_i1, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_I2:
          return visit.Unary(pc, UnaryOperator.Conv_i2, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_I4:
          return visit.Unary(pc, UnaryOperator.Conv_i4, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_I8:
          return visit.Unary(pc, UnaryOperator.Conv_i8, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_I:
          return visit.Unary(pc, UnaryOperator.Conv_i, true, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_I_Un:
          return visit.Unary(pc, UnaryOperator.Conv_i, true, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_I1:
          return visit.Unary(pc, UnaryOperator.Conv_i1, true, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_I1_Un:
          return visit.Unary(pc, UnaryOperator.Conv_i1, true, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_I2:
          return visit.Unary(pc, UnaryOperator.Conv_i2, true, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_I2_Un:
          return visit.Unary(pc, UnaryOperator.Conv_i2, true, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_I4:
          return visit.Unary(pc, UnaryOperator.Conv_i4, true, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_I4_Un:
          return visit.Unary(pc, UnaryOperator.Conv_i4, true, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_I8:
          return visit.Unary(pc, UnaryOperator.Conv_i8, true, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_I8_Un:
          return visit.Unary(pc, UnaryOperator.Conv_i8, true, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_U:
          return visit.Unary(pc, UnaryOperator.Conv_u, true, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_U_Un:
          return visit.Unary(pc, UnaryOperator.Conv_u, true, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_U1:
          return visit.Unary(pc, UnaryOperator.Conv_u1, true, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_U1_Un:
          return visit.Unary(pc, UnaryOperator.Conv_u1, true, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_U2:
          return visit.Unary(pc, UnaryOperator.Conv_u2, true, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_U2_Un:
          return visit.Unary(pc, UnaryOperator.Conv_u2, true, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_U4:
          return visit.Unary(pc, UnaryOperator.Conv_u4, true, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_U4_Un:
          return visit.Unary(pc, UnaryOperator.Conv_u4, true, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_U8:
          return visit.Unary(pc, UnaryOperator.Conv_u8, true, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_Ovf_U8_Un:
          return visit.Unary(pc, UnaryOperator.Conv_u8, true, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_R_Un:
          return visit.Unary(pc, UnaryOperator.Conv_r_un, false, true, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_R4:
          return visit.Unary(pc, UnaryOperator.Conv_r4, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_R8:
          return visit.Unary(pc, UnaryOperator.Conv_r8, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_U:
          return visit.Unary(pc, UnaryOperator.Conv_u, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_U1:
          return visit.Unary(pc, UnaryOperator.Conv_u1, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_U2:
          return visit.Unary(pc, UnaryOperator.Conv_u2, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_U4:
          return visit.Unary(pc, UnaryOperator.Conv_u4, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Conv_U8:
          return visit.Unary(pc, UnaryOperator.Conv_u8, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Cpblk:
          return visit.Cpblk(pc, Volatile(ref pc), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Cpobj:
          return visit.Cpobj(pc, Type(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Div:
          return visit.Binary(pc, BinaryOperator.Div, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Div_Un:
          return visit.Binary(pc, BinaryOperator.Div_Un, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Dup:
          return visit.Ldstack(pc, 0, Unit.Value, Unit.Value, false, data);

        case OperationCode.Endfilter:
          return visit.Endfilter(pc, Unit.Value, data);

        case OperationCode.Endfinally:
          return visit.Endfinally(pc, data);

        case OperationCode.Initblk:
          return visit.Initblk(pc, Volatile(ref pc), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Initobj:
          return visit.Initobj(pc, Type(ref pc), Unit.Value, data);

        case OperationCode.Isinst:
          return visit.Isinst(pc, Type(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Jmp:
          return visit.Jmp(pc, Method(ref pc), data);

        case OperationCode.Ldarg:
        case OperationCode.Ldarg_0:
        case OperationCode.Ldarg_1:
        case OperationCode.Ldarg_2:
        case OperationCode.Ldarg_3:
        case OperationCode.Ldarg_S:
          return visit.Ldarg(pc, this.Parameter(ref pc), false, Unit.Value, data);

        case OperationCode.Ldarga:
        case OperationCode.Ldarga_S:
          return visit.Ldarga(pc, this.Parameter(ref pc), false, Unit.Value, data);

        case OperationCode.Ldc_I4:
          return visit.Ldconst(pc, operation.Value, Adapt(this.platformType.SystemInt32), Unit.Value, data);
        case OperationCode.Ldc_I4_S:
          IConvertible ic = (IConvertible)operation.Value;
          return visit.Ldconst(pc, ic.ToInt32(null), Adapt(this.platformType.SystemInt32), Unit.Value, data);

        case OperationCode.Ldc_I4_M1:
        case OperationCode.Ldc_I4_0:
        case OperationCode.Ldc_I4_1:
        case OperationCode.Ldc_I4_2:
        case OperationCode.Ldc_I4_3:
        case OperationCode.Ldc_I4_4:
        case OperationCode.Ldc_I4_5:
        case OperationCode.Ldc_I4_6:
        case OperationCode.Ldc_I4_7:
        case OperationCode.Ldc_I4_8:
          return visit.Ldconst(pc, (int)(operation.OperationCode - OperationCode.Ldc_I4_0), Adapt(this.platformType.SystemInt32), Unit.Value, data);

        case OperationCode.Ldc_I8:
          return visit.Ldconst(pc, operation.Value, Adapt(this.platformType.SystemInt64), Unit.Value, data);

        case OperationCode.Ldc_R4:
          return visit.Ldconst(pc, operation.Value, Adapt(this.platformType.SystemFloat32), Unit.Value, data);

        case OperationCode.Ldc_R8:
          return visit.Ldconst(pc, operation.Value, Adapt(this.platformType.SystemFloat64), Unit.Value, data);

        case OperationCode.Ldelem:
          return visit.Ldelem(pc, Type(ref pc), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelem_I:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemIntPtr), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelem_I1:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemInt8), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelem_I2:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemInt16), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelem_I4:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemInt32), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelem_I8:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemInt64), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelem_R4:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemFloat32), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelem_R8:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemFloat64), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelem_Ref:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemObject), Unit.Value, Unit.Value, Unit.Value, data);
        //TODO: not the same as CCI1

        case OperationCode.Ldelem_U1:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemUInt8), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelem_U2:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemUInt16), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelem_U4:
          return visit.Ldelem(pc, Adapt(this.platformType.SystemUInt32), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldelema:
          return visit.Ldelema(pc, Type(ref pc), Readonly(ref pc), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ldfld:
          return visit.Ldfld(pc, Field(ref pc), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldflda:
          return visit.Ldflda(pc, Field(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldftn:
          return visit.Ldftn(pc, Method(ref pc), Unit.Value, data);

        case OperationCode.Ldind_I:
          return visit.Ldind(pc, Adapt(this.platformType.SystemIntPtr), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldind_I1:
          return visit.Ldind(pc, Adapt(this.platformType.SystemInt8), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldind_I2:
          return visit.Ldind(pc, Adapt(this.platformType.SystemInt16), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldind_I4:
          return visit.Ldind(pc, Adapt(this.platformType.SystemInt32), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldind_I8:
          return visit.Ldind(pc, Adapt(this.platformType.SystemInt64), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldind_R4:
          return visit.Ldind(pc, Adapt(this.platformType.SystemFloat32), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldind_R8:
          return visit.Ldind(pc, Adapt(this.platformType.SystemFloat64), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldind_Ref:
          return visit.Ldind(pc, Adapt(this.platformType.SystemObject), Volatile(ref pc), Unit.Value, Unit.Value, data);
        //TODO: different from CCI1

        case OperationCode.Ldind_U1:
          return visit.Ldind(pc, Adapt(this.platformType.SystemUInt8), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldind_U2:
          return visit.Ldind(pc, Adapt(this.platformType.SystemUInt16), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldind_U4:
          return visit.Ldind(pc, Adapt(this.platformType.SystemUInt32), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldlen:
          return visit.Ldlen(pc, Unit.Value, Unit.Value, data);

        case OperationCode.Ldloc:
        case OperationCode.Ldloc_0:
        case OperationCode.Ldloc_1:
        case OperationCode.Ldloc_2:
        case OperationCode.Ldloc_3:
        case OperationCode.Ldloc_S:
          return visit.Ldloc(pc, Local(ref pc), Unit.Value, data);

        case OperationCode.Ldloca:
        case OperationCode.Ldloca_S:
          return visit.Ldloca(pc, Local(ref pc), Unit.Value, data);

        case OperationCode.Ldnull:
          return visit.Ldnull(pc, Unit.Value, data);

        case OperationCode.Ldobj:
          return visit.Ldind(pc, Type(ref pc), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Ldsfld:
          return visit.Ldsfld(pc, Field(ref pc), Volatile(ref pc), Unit.Value, data);

        case OperationCode.Ldsflda:
          return visit.Ldsflda(pc, Field(ref pc), Unit.Value, data);
        //TODO: why no volatile?

        case OperationCode.Ldstr:
          return visit.Ldconst(pc, operation.Value, Adapt(this.platformType.SystemString), Unit.Value, data);

        case OperationCode.Ldtoken:
          object tok = operation.Value;
          IFieldReference/*?*/ frTok = tok as IFieldReference;
          if (frTok != null) return visit.Ldfieldtoken(pc, FieldReferenceAdaptor.AdaptorOf(frTok), Unit.Value, data);
          IMethodReference/*?*/ mrTok = tok as IMethodReference;
          if (mrTok != null) return visit.Ldmethodtoken(pc, MethodReferenceAdaptor.AdaptorOf(mrTok), Unit.Value, data);
          ITypeReference/*?*/ trTok = tok as ITypeReference;
          if (trTok != null) return visit.Ldtypetoken(pc, Adapt(trTok), Unit.Value, data);
          //^ assume false;
          goto default;

        case OperationCode.Ldvirtftn:
          return visit.Ldvirtftn(pc, Method(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Leave:
        case OperationCode.Leave_S:
          return visit.Branch(pc, new CciILPC(pc.Context, FindIndexFor(pc.Operations, (uint)operation.Value)), true, data);

        case OperationCode.Localloc:
          return visit.Localloc(pc, Unit.Value, Unit.Value, data);

        case OperationCode.Mkrefany:
          return visit.Mkrefany(pc, Type(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Mul:
          return visit.Binary(pc, BinaryOperator.Mul, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Mul_Ovf:
          return visit.Binary(pc, BinaryOperator.Mul_Ovf, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Mul_Ovf_Un:
          return visit.Binary(pc, BinaryOperator.Mul_Ovf_Un, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Neg:
          return visit.Unary(pc, UnaryOperator.Neg, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Newarr:
          return visit.Newarray(pc, ArrayElementType(ref pc), Unit.Value, new UnitIndexable(1), data);

        case OperationCode.Newobj:
          mr = (IMethodReference)pc.Operation.Value;
          return visit.Newobj(pc, Method(ref pc), Unit.Value, new UnitIndexable(ActualParameterCount(mr)), data);

        case OperationCode.No_:
          return visit.Nop(pc, data);

        case OperationCode.Nop:
          return visit.Nop(pc, data);

        case OperationCode.Not:
          return visit.Unary(pc, UnaryOperator.Not, false, false, Unit.Value, Unit.Value, data);

        case OperationCode.Or:
          return visit.Binary(pc, BinaryOperator.Or, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Pop:
          return visit.Pop(pc, Unit.Value, data);

        case OperationCode.Readonly_:
          return visit.Nop(pc, data);

        case OperationCode.Refanytype:
          return visit.Refanytype(pc, Unit.Value, Unit.Value, data);

        case OperationCode.Refanyval:
          return visit.Refanyval(pc, Type(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Rem:
          return visit.Binary(pc, BinaryOperator.Rem, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Rem_Un:
          return visit.Binary(pc, BinaryOperator.Rem_Un, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Ret:
          return visit.Return(pc, Unit.Value, data);

        case OperationCode.Rethrow:
          return visit.Rethrow(pc, data);

        case OperationCode.Shl:
          return visit.Binary(pc, BinaryOperator.Shl, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Shr:
          return visit.Binary(pc, BinaryOperator.Shr, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Shr_Un:
          return visit.Binary(pc, BinaryOperator.Shr_Un, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Sizeof:
          return visit.Sizeof(pc, Type(ref pc), Unit.Value, data);

        case OperationCode.Starg:
        case OperationCode.Starg_S:
          return visit.Starg(pc, this.Parameter(ref pc), Unit.Value, data);

        case OperationCode.Stelem:
          return visit.Stelem(pc, Type(ref pc), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Stelem_I:
          return visit.Stelem(pc, Adapt(this.platformType.SystemIntPtr), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Stelem_I1:
          return visit.Stelem(pc, Adapt(this.platformType.SystemInt8), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Stelem_I2:
          return visit.Stelem(pc, Adapt(this.platformType.SystemInt16), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Stelem_I4:
          return visit.Stelem(pc, Adapt(this.platformType.SystemInt32), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Stelem_I8:
          return visit.Stelem(pc, Adapt(this.platformType.SystemInt64), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Stelem_R4:
          return visit.Stelem(pc, Adapt(this.platformType.SystemFloat32), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Stelem_R8:
          return visit.Stelem(pc, Adapt(this.platformType.SystemFloat64), Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Stelem_Ref:
          return visit.Stelem(pc, Adapt(this.platformType.SystemObject), Unit.Value, Unit.Value, Unit.Value, data);
        //TODO: this is incompatible with CCI1.

        case OperationCode.Stfld:
          return visit.Stfld(pc, Field(ref pc), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Stind_I:
          return visit.Stind(pc, Adapt(this.platformType.SystemIntPtr), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Stind_I1:
          return visit.Stind(pc, Adapt(this.platformType.SystemInt8), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Stind_I2:
          return visit.Stind(pc, Adapt(this.platformType.SystemInt16), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Stind_I4:
          return visit.Stind(pc, Adapt(this.platformType.SystemInt32), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Stind_I8:
          return visit.Stind(pc, Adapt(this.platformType.SystemInt64), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Stind_R4:
          return visit.Stind(pc, Adapt(this.platformType.SystemFloat32), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Stind_R8:
          return visit.Stind(pc, Adapt(this.platformType.SystemFloat64), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Stind_Ref:
          return visit.Stind(pc, Adapt(this.platformType.SystemObject), Volatile(ref pc), Unit.Value, Unit.Value, data);
        //TODO: this is incompatible with CCI1.

        case OperationCode.Stloc:
        case OperationCode.Stloc_0:
        case OperationCode.Stloc_1:
        case OperationCode.Stloc_2:
        case OperationCode.Stloc_3:
        case OperationCode.Stloc_S:
          return visit.Stloc(pc, Local(ref pc), Unit.Value, data);

        case OperationCode.Stobj:
          return visit.Stind(pc, Type(ref pc), Volatile(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Stsfld:
          return visit.Stsfld(pc, Field(ref pc), Volatile(ref pc), Unit.Value, data);

        case OperationCode.Sub:
          return visit.Binary(pc, BinaryOperator.Sub, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Sub_Ovf:
          return visit.Binary(pc, BinaryOperator.Sub_Ovf, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Sub_Ovf_Un:
          return visit.Binary(pc, BinaryOperator.Sub_Ovf_Un, Unit.Value, Unit.Value, Unit.Value, data);

        case OperationCode.Switch:
          return visit.Switch(pc, Adapt(this.platformType.SystemInt32), ListOfTargetPCs(pc), Unit.Value, data);

        case OperationCode.Tail_:
          return visit.Nop(pc, data);

        case OperationCode.Throw:
          return visit.Throw(pc, Unit.Value, data);

        case OperationCode.Unaligned_:
          return visit.Nop(pc, data);

        case OperationCode.Unbox:
          return visit.Unbox(pc, Type(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Unbox_Any:
          return visit.Unboxany(pc, Type(ref pc), Unit.Value, Unit.Value, data);

        case OperationCode.Volatile_:
          return visit.Nop(pc, data);

        case OperationCode.Xor:
          return visit.Binary(pc, BinaryOperator.Xor, Unit.Value, Unit.Value, Unit.Value, data);

        default:
          Console.WriteLine("Warning: unprocessed operation code: {0}", operation.OperationCode);
          return visit.Nop(pc, data);
      }
    }

    internal static int ActualParameterCount(IMethodReference mr)
    {
      Contract.Requires(mr != null);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return mr.ParameterCount + (((mr.CallingConvention & CallingConvention.HasThis) != 0) ? 1 : 0);
    }

    private IEnumerable<CciILPC> ListOfPCs(CciILPC pc)
    {
      int[] targets = (int[])pc.Operation.Value;
      for (int i = 0; i < targets.Length; i++)
      {
        uint target = (uint)targets[i];
        yield return new CciILPC(pc.Context, FindIndexFor(pc.Operations, target));
      }
    }

    private static MethodReferenceAdaptor Method(ref CciILPC pc)
    {
      return MethodReferenceAdaptor.AdaptorOf((IMethodReference)pc.Operation.Value);
    }

    private static TypeReferenceAdaptor ArrayElementType(ref CciILPC pc)
    {
      Contract.Requires(pc.Operation.Value != null);
      return Adapt(((IArrayTypeReference)pc.Operation.Value).ElementType);
    }

    private static int ArrayRank(ref CciILPC pc)
    {
      Contract.Requires(pc.Operation.Value != null);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return (int)((IArrayTypeReference)pc.Operation.Value).Rank;
    }

    private IParameterTypeInformation Parameter(ref CciILPC pc)
    {
      IParameterTypeInformation result = (IParameterTypeInformation)pc.Operation.Value;
      if (result == null) result = this.metaDataDecoder.This(MethodReferenceAdaptor.AdaptorOf(pc.Context.Method));
      return result;
    }

    private static LocalDefAdaptor Local(ref CciILPC pc)
    {
      var l = (ILocalDefinition)pc.Operation.Value;
      return LocalDefAdaptor.AdaptorOf(l);
    }

    private static FieldReferenceAdaptor Field(ref CciILPC pc)
    {
      //      return (IFieldReference)pc.Operation.Value;
      return FieldReferenceAdaptor.AdaptorOf((IFieldReference)pc.Operation.Value);
    }

    private static TypeReferenceAdaptor Type(ref CciILPC pc)
    {
      return TypeReferenceAdaptor.AdaptorOf((ITypeReference)pc.Operation.Value);
    }

    private static TypeReferenceAdaptor/*?*/ Constraint(ref CciILPC pc)
    {
      if (pc.RealIndex < 1) return Adapt(null);
      IOperation operation = pc.Operations[pc.RealIndex - 1];
      if (operation.OperationCode == OperationCode.Constrained_)
        return Adapt((ITypeReference)operation.Value);
      else
        return Adapt(null);
    }

    private static bool Readonly(ref CciILPC pc)
    {
      if (pc.RealIndex < 1) return false;
      return pc.Operations[pc.RealIndex - 1].OperationCode == OperationCode.Readonly_;
    }

    private static bool Tail(ref CciILPC pc)
    {
      if (pc.RealIndex < 1) return false;
      return pc.Operations[pc.RealIndex - 1].OperationCode == OperationCode.Tail_;
    }

    private static bool Volatile(ref CciILPC pc)
    {
      if (pc.RealIndex < 1) return false;
      IOperation operation = pc.Operations[pc.RealIndex - 1];
      if (operation.OperationCode == OperationCode.Unaligned_)
      {
        if (pc.RealIndex < 2) return false;
        operation = pc.Operations[pc.RealIndex - 2];
      }
      return operation.OperationCode == OperationCode.Volatile_;
    }

#if false
    public Transformer<CciILPC, Data, Result> CacheForwardDecoder<Data, Result>(IVisitMSIL<CciILPC, ILocalDefinition, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Unit, Unit, Data, Result> visitor) {
      // can't further cache
      return delegate(CciILPC label, Data data) { 
        return this.ForwardDecode<
          Data, Result, IVisitMSIL<CciILPC, ILocalDefinition, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Unit, Unit, Data, Result>>(label, visitor, data);
      };
    }
#endif

    public Unit GetContext { get { return Unit.Value; } }

    #endregion
  }

  public class CciExceptionHandlerInfo
  {

    internal CciExceptionHandlerInfo(IOperationExceptionInformation info, IMethodReference method, CciILPCContext context)
    {
      this.Info = info;
      this.Method = method;
      this.Context = context;
    }

    public readonly IOperationExceptionInformation Info;
    public readonly IMethodReference Method;
    internal readonly CciILPCContext Context;

  }

  internal abstract class CciILPCContext
  {
    public readonly IMethodReference Method;
    public readonly IList<IOperation> Operations;
    public CciILPCContext(IMethodReference method, IList<IOperation> operations)
    {
      this.Method = method;
      this.Operations = operations;
    }
    public abstract bool Next(CciILPC pc, out CciILPC nextPC);
    public abstract IOperation Operation(CciILPC pc);
    public abstract CciILPC FirstPC { get; }
  }

  /// <summary>
  /// An instance of this class represents that a PC pointing to it is enumerating
  /// the instructions of a method body and should just enumerate all instructions.
  /// The IL in a method body has no pseudo-instructions, so just use the even numbers.
  /// </summary>
  internal class CciILPCMethodBodyContext : CciILPCContext
  {
    private readonly CciILPC _firstPC;
    public CciILPCMethodBodyContext(IMethodReference method, IList<IOperation> operations)
      : base(method, operations)
    {
      this._firstPC = new CciILPC(this, 0);
    }
    public override bool Next(CciILPC pc, out CciILPC nextPC)
    {
      var nextIndex = pc.RealIndex + 1;
      if (nextIndex < this.Operations.Count)
      {
        nextPC = new CciILPC(this, nextIndex);
        return true;
      }
      else
      {
        nextPC = new CciILPC();
        return false;
      }
    }
    public override IOperation Operation(CciILPC pc)
    {
      if (this.Operations.Count == 0) return Dummy.Operation;
      return this.Operations[pc.Index / 2];
    }
    public override CciILPC FirstPC
    {
      get { return _firstPC; }
    }
  }

  /// <summary>
  /// An instance of this class represents that a PC pointing to it is enumerating
  /// the instructions of a method body and should skip over the contiguous instructions
  /// that indicate the start and end of the method's contract.
  /// The IL in a method body has no pseudo-instructions, so just use the even numbers.
  /// </summary>
  internal class CciILPCMethodBodyWithContractContext : CciILPCContext
  {
    private readonly CciILPC _firstPC;
    public readonly int ContractStartIndex; // real index into Operations
    public readonly int ContractEndIndex; // real index into Operations
    public CciILPCMethodBodyWithContractContext(IMethodReference method, IList<IOperation> operations, int start, int end)
      : base(method, operations)
    {
      this.ContractStartIndex = start;
      this.ContractEndIndex = end;
      if (0 < start)
        this._firstPC = new CciILPC(this, 0);
      else
        this._firstPC = new CciILPC(this, end + 1);
    }

    public override bool Next(CciILPC pc, out CciILPC nextPC)
    {
      int nextRealIndexIntoOperations = pc.RealIndex + 1; // assume we're just going to the next instruction
      if (0 <= ContractStartIndex && ContractStartIndex <= nextRealIndexIntoOperations
        && 0 <= ContractEndIndex && nextRealIndexIntoOperations <= ContractEndIndex)
        nextRealIndexIntoOperations = ContractEndIndex + 1;
      if (nextRealIndexIntoOperations < this.Operations.Count)
      {
        nextPC = new CciILPC(this, nextRealIndexIntoOperations);
        return true;
      }
      else
      {
        nextPC = new CciILPC();
        return false;
      }

    }
    public override IOperation Operation(CciILPC pc)
    {
      return this.Operations[pc.Index / 2];
    }
    public override CciILPC FirstPC
    {
      get { return _firstPC; }
    }
  }

  /// <summary>
  /// An instance of this class represents that a PC pointing to it is enumerating
  /// the instructions of an anonymous delegate. These exist because they were
  /// in a contract and so are Code Model trees, not IL. But Clousot thinks of them
  /// as methods (because they were in the un-decompiled CCI1 model) and so uses
  /// the code provider to access them. The code provider uses CciILPC as a program
  /// counter, and those have a context.
  /// </summary>
  internal class CciILPCLambdaContext : CciILPCContext
  {
    private readonly CciILPC _firstPC;
    public CciILPCLambdaContext(LambdaMethodReference lambdaWrapper)
      : base(lambdaWrapper, new List<IOperation>())
    {
      this._firstPC = new CciILPC(this, 0);
    }
    public override bool Next(CciILPC pc, out CciILPC nextPC)
    {
      var nextIndex = pc.RealIndex + 1;
      if (nextIndex < this.Operations.Count)
      {
        nextPC = new CciILPC(this, nextIndex);
        return true;
      }
      else
      {
        nextPC = new CciILPC();
        return false;
      }
    }
    public override IOperation Operation(CciILPC pc)
    {
      if (this.Operations.Count == 0) return Dummy.Operation;
      return this.Operations[pc.Index / 2];
    }
    public override CciILPC FirstPC
    {
      get { return _firstPC; }
    }
  }

  public struct CciILPC : IEquatable<CciILPC>
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.Context != null);
    }


    internal readonly CciILPCContext/*!*/ Context;
    /// <summary>
    /// In general, this is twice the real index of the operation.
    /// When Operations[i] represents the first operation of an old expression,
    /// then a pseudo-instruction is generated with index 2*i+1.
    /// </summary>
    public readonly int Index;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context">The context that has the methods to interpret this PC</param>
    /// <param name="index">The real index into the Operations. this.Index == 2*index</param>
    internal CciILPC(CciILPCContext/*!*/ context, int index)
    {
      Contract.Requires(context != null);

      this.Context = context;
      this.Index = index * 2;
    }
    internal CciILPC(CciILPCContext/*!*/ context, int index, bool raw)
    {
      Contract.Requires(context != null);

      this.Context = context;
      this.Index = raw ? index : index * 2;
    }

    public IList<IOperation> Operations
    {
      get {
        return this.Context.Operations; }
    }

    public IOperation Operation
    {
      get
      {
        Contract.Ensures(Contract.Result<IOperation>() != null);

        return this.Context.Operation(this).AssumeNotNull();
      }
    }

    public bool Next(out CciILPC nextLabel)
    {
      return this.Context.Next(this, out nextLabel);
    }

    public override int GetHashCode()
    {
      return (this.Context.Method.GetHashCode() << 2) ^ this.Index;
    }

    #region IEquatable<CciILPC> Members

    public bool Equals(CciILPC other)
    {
      return this.Context.Method == other.Context.Method && this.Index == other.Index;
    }

    #endregion

    internal int RealIndex { get { return this.Index / 2; } }
  }

  public struct CciContractPC : IEquatable<CciContractPC>
  {

    public readonly object Node;
    public readonly int Index;

    internal CciContractPC(object/*!*/ node, int index)
    {
      this.Node = node;
      this.Index = index;
    }

    internal CciContractPC(object/*!*/ node) : this(node, 0) { }

    public override int GetHashCode()
    {
      var labelStmt = this.Node as ILabeledStatement;
      if (labelStmt != null)
        return labelStmt.Label.GetHashCode() << 2 ^ this.Index;
      return (this.Node.GetHashCode() << 2) ^ this.Index;
    }

    #region IEquatable<CciContractPC> Members

    public bool Equals(CciContractPC other)
    {
      // can't count on object identity for labeled statements
      // the object model might have multiple objects representing
      // the same labeled statement
      var labelStmt = this.Node as ILabeledStatement;
      if (labelStmt != null) {
        var otherLabel = other.Node as ILabeledStatement;
        if (otherLabel == null) return false;
        return labelStmt.Label == otherLabel.Label && this.Index == other.Index;
      }
      return this.Node == other.Node && this.Index == other.Index;
    }

    #endregion
  }

  internal class DummyArrayMethodParameter : IParameterDefinition
  {

    internal DummyArrayMethodParameter(ISignature containingSignature, ushort index, ITypeReference type)
    {
      this.containingSignature = containingSignature;
      this.index = index;
      this.type = type;
    }

    private readonly ISignature containingSignature;
    public ISignature ContainingSignature
    {
      get { return this.containingSignature; }
    }

    public IEnumerable<ICustomModifier> CustomModifiers
    {
      get { return Enumerable<ICustomModifier>.Empty; }
    }

    private readonly ushort index;
    public ushort Index
    {
      get { return this.index; }
    }

    public bool IsByReference
    {
      get { return false; }
    }

    public bool IsModified
    {
      get { return false; }
    }

    private readonly ITypeReference type;
    public ITypeReference Type
    {
      get { return type; }
    }

    public IMetadataConstant DefaultValue
    {
      get { throw new NotImplementedException(); }
    }

    public bool HasDefaultValue
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsIn
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsMarshalledExplicitly
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsOptional
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsOut
    {
      get { return false; }
    }

    public bool IsParameterArray
    {
      get { throw new NotImplementedException(); }
    }

    public IMarshallingInformation MarshallingInformation
    {
      get { throw new NotImplementedException(); }
    }

    public ITypeReference ParamArrayElementType
    {
      get { throw new NotImplementedException(); }
    }

    public IEnumerable<ICustomAttribute> Attributes
    {
      get { return Enumerable.Empty<ICustomAttribute>(); }
    }

    public void Dispatch(IMetadataVisitor visitor)
    {
      throw new NotImplementedException();
    }

    public void DispatchAsReference(IMetadataVisitor visitor)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<ILocation> Locations
    {
      get { throw new NotImplementedException(); }
    }

    public IName Name
    {
      get { throw new NotImplementedException(); }
    }

    public IMetadataConstant Constant
    {
      get { throw new NotImplementedException(); }
    }

  }

  internal class DummyName : IName
  {

    internal DummyName(string name)
    {
      this.value = name;
    }

    [ContractVerification(false)]
    public int UniqueKey
    {
      get { return 0; }
    }

    [ContractVerification(false)]
    public int UniqueKeyIgnoringCase
    {
      get { return 0; }
    }

    public string Value
    {
      get { return this.value; }
    }
    private readonly string value;
  }

  internal class DummyReturnValueParameter : IParameterDefinition
  {

    internal DummyReturnValueParameter(IMethodDefinition containingMethod)
    {
      this.containingMethod = containingMethod;
    }

    public IEnumerable<ICustomAttribute> Attributes
    {
      get { return this.containingMethod.ReturnValueAttributes; }
    }

    public ISignature ContainingSignature
    {
      get { return this.containingMethod; }
    }
    private readonly IMethodDefinition containingMethod;

    public IMetadataConstant Constant
    {
      get { return Dummy.Constant; }
    }

    public IEnumerable<ICustomModifier> CustomModifiers
    {
      get { return this.containingMethod.ReturnValueCustomModifiers; }
    }

    public IMetadataConstant DefaultValue
    {
      get { return Dummy.Constant; }
    }

    public void Dispatch(IMetadataVisitor visitor)
    {
    }

    public bool HasDefaultValue
    {
      get { return false; }
    }

    public ushort Index
    {
      get { return 0; }
    }

    public bool IsIn
    {
      get { return false; }
    }

    public bool IsByReference
    {
      get { return this.containingMethod.ReturnValueIsByRef; }
    }

    public bool IsModified
    {
      get { return this.containingMethod.ReturnValueIsModified; }
    }

    public bool IsMarshalledExplicitly
    {
      get { return this.containingMethod.ReturnValueIsMarshalledExplicitly; }
    }

    public bool IsOptional
    {
      get { return false; }
    }

    public bool IsOut
    {
      get { return false; }
    }

    public bool IsParameterArray
    {
      get { return false; }
    }

    public IEnumerable<ILocation> Locations
    {
      get { return Enumerable<ILocation>.Empty; }
    }

    public IMarshallingInformation MarshallingInformation
    {
      get { return this.containingMethod.ReturnValueMarshallingInformation; }
    }

    public IName Name
    {
      get { return Dummy.Name; }
    }

    public ITypeDefinition ParamArrayElementType
    {
      get { return Dummy.Type; }
    }

    public ITypeReference Type
    {
      get { return (this.containingMethod.Type); }
    }


    #region IParameterDefinition Members


    ITypeReference IParameterDefinition.ParamArrayElementType
    {
      get { throw new NotImplementedException(); }
    }

    #endregion

    #region IReference Members


    public void DispatchAsReference(IMetadataVisitor visitor)
    {
      visitor.VisitReference(this);
    }

    #endregion
  }

  internal class DummyThisParameter : IParameterDefinition
  {

    private readonly ITypeReference thisType;

    internal DummyThisParameter(ITypeReference type)
    {
      this.containingMethod = Dummy.Method;
      this.thisType = type;
    }

    public IEnumerable<ICustomAttribute> Attributes
    {
      get { return this.containingMethod.ReturnValueAttributes; }
    }

    public ISignature ContainingSignature
    {
      get { return this.containingMethod; }
    }
    private readonly IMethodDefinition containingMethod;

    public IMetadataConstant Constant
    {
      get { return Dummy.Constant; }
    }

    public IEnumerable<ICustomModifier> CustomModifiers
    {
      get { return this.containingMethod.ReturnValueCustomModifiers; }
    }

    public IMetadataConstant DefaultValue
    {
      get { return Dummy.Constant; }
    }

    public void Dispatch(IMetadataVisitor visitor)
    {
    }

    public bool HasDefaultValue
    {
      get { return false; }
    }

    public ushort Index
    {
      get
      {
        //return 0; 
        return ushort.MaxValue; // used to communicate with ArgumentIndex and ArgumentStackIndex
      }
    }

    public bool IsIn
    {
      get { return false; }
    }

    public bool IsByReference
    {
      get { return this.containingMethod.ReturnValueIsByRef; }
    }

    public bool IsModified
    {
      get { return this.containingMethod.ReturnValueIsModified; }
    }

    public bool IsMarshalledExplicitly
    {
      get { return this.containingMethod.ReturnValueIsMarshalledExplicitly; }
    }

    public bool IsOptional
    {
      get { return false; }
    }

    public bool IsOut
    {
      get { return false; }
    }

    public bool IsParameterArray
    {
      get { return false; }
    }

    public IEnumerable<ILocation> Locations
    {
      get { return Enumerable<ILocation>.Empty; }
    }

    public IMarshallingInformation MarshallingInformation
    {
      get { return this.containingMethod.ReturnValueMarshallingInformation; }
    }

    public IName Name
    {
      get
      {
        return new DummyName("thisGuy"); // BUGBUG: Don't create a new one each time
      }
    }

    public ITypeDefinition ParamArrayElementType
    {
      get { return Dummy.Type; }
    }

    public ITypeReference Type
    {
      get { return this.thisType; }
    }


    #region IParameterDefinition Members


    ITypeReference IParameterDefinition.ParamArrayElementType
    {
      get { throw new NotImplementedException(); }
    }

    #endregion

    #region IReference Members


    public void DispatchAsReference(IMetadataVisitor visitor)
    {
      visitor.VisitReference(this);
    }

    #endregion
  }

  internal class DummyArrayMethodReference : IMethodReference
  {

    private readonly IArrayTypeReference arrayType;
    private readonly OperationCode arrayOperation;
    private readonly IPlatformType platformType;

    internal DummyArrayMethodReference(IArrayTypeReference arrayType, OperationCode arrayOperation, IPlatformType platformType)
    {
      this.arrayType = arrayType;
      this.arrayOperation = arrayOperation;
      this.platformType = platformType;
    }

    public ushort GenericParameterCount
    {
      get { return 0; }
    }

    public bool IsGeneric
    {
      get { return false; }
    }

    public IMethodDefinition ResolvedMethod
    {
      get { return new DummyArrayMethodDefinition(this); }
    }

    public IEnumerable<IParameterTypeInformation> ExtraParameters
    {
      get { return Enumerable<IParameterTypeInformation>.Empty; }
    }

    public CallingConvention CallingConvention
    {
      get { return CallingConvention.HasThis; }
    }

    public void Dispatch(IMetadataVisitor visitor)
    {
    }

    public IEnumerable<IParameterTypeInformation> Parameters
    {
      get
      {
        ushort n = (ushort)this.arrayType.Rank;
        if (this.arrayOperation == OperationCode.Array_Create_WithLowerBound) n *= 2;
        for (ushort i = 0; i < n; i++)
          yield return new DummyArrayMethodParameter(this, i, this.platformType.SystemInt32);
        if (this.arrayOperation == OperationCode.Array_Set)
          yield return new DummyArrayMethodParameter(this, n, this.arrayType.ElementType);
      }
    }

    public ushort ParameterCount
    {
      get
      {
        ushort n = (ushort)this.arrayType.Rank;
        if (this.arrayOperation == OperationCode.Array_Create_WithLowerBound) n *= 2;
        if (this.arrayOperation == OperationCode.Array_Set) n++;
        return n;
      }
    }

    public IEnumerable<ICustomModifier> ReturnValueCustomModifiers
    {
      get { return Enumerable<ICustomModifier>.Empty; }
    }

    public bool ReturnValueIsByRef
    {
      get { return this.arrayOperation == OperationCode.Array_Addr; }
    }

    public bool ReturnValueIsModified
    {
      get { return false; }
    }

    public ITypeReference Type
    {
      get
      {
        if (this.arrayOperation == OperationCode.Array_Addr || this.arrayOperation == OperationCode.Array_Get)
          return (this.arrayType.ElementType);
        else
          return (this.platformType.SystemVoid);
      }
    }

    public ITypeReference ContainingType
    {
      get { return (this.arrayType); }
    }

    public ITypeDefinitionMember ResolvedTypeDefinitionMember
    {
      get { return Dummy.Method; }
    }

    public IEnumerable<ICustomAttribute> Attributes
    {
      get { return Enumerable<ICustomAttribute>.Empty; }
    }

    public IEnumerable<ILocation> Locations
    {
      get { return Enumerable<ILocation>.Empty; }
    }

    public IName Name
    {
      get
      {
        switch (this.arrayOperation)
        {
          case OperationCode.Array_Addr: return new DummyName("Address");
          case OperationCode.Array_Create:
          case OperationCode.Array_Create_WithLowerBound: return new DummyName(".ctor");
          case OperationCode.Array_Get: return new DummyName("Get");
          case OperationCode.Array_Set: return new DummyName("Set");
        }
        return Dummy.Name;
      }
    }

    public uint InternedKey
    {
      get { return 0; }
    }

    public bool AcceptsExtraArguments
    {
      get { return false; }
    }

    public bool IsStatic { get { return false; } }

    #region IReference Members


    public void DispatchAsReference(IMetadataVisitor visitor)
    {
      visitor.Visit(this);
    }

    #endregion

    private class DummyArrayMethodDefinition : Dummy, IMethodDefinition
    {
      readonly DummyArrayMethodReference reference;

      public DummyArrayMethodDefinition(DummyArrayMethodReference reference)
      {
        this.reference = reference;
      }

      public IMethodBody Body
      {
        get { return Dummy.MethodBody; }
      }

      public IEnumerable<IGenericMethodParameter> GenericParameters
      {
        get { return Enumerable<IGenericMethodParameter>.Empty; }
      }

      public bool HasDeclarativeSecurity
      {
        get { throw new NotImplementedException(); }
      }

      public bool HasExplicitThisParameter
      {
        get { return false; }
      }

      public bool IsAbstract
      {
        get { return false; }
      }

      public bool IsAccessCheckedOnOverride
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsAggressivelyInlined { get { return false; } }

      public bool IsCil
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsConstructor
      {
        get { return false; }
      }

      public bool IsExternal
      {
        get { return false; }
      }

      public bool IsForwardReference
      {
        get { return false; }
      }

      public bool IsHiddenBySignature
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsNativeCode
      {
        get { return false; }
      }

      public bool IsNeverInlined
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsNeverOptimized
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsNewSlot
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsPlatformInvoke
      {
        get { return false; }
      }

      public bool IsRuntimeImplemented
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsRuntimeInternal
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsRuntimeSpecial
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsSealed
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsSpecialName
      {
        get { return false; }
      }

      public bool IsStatic
      {
        get { return false; }
      }

      public bool IsStaticConstructor
      {
        get { return false; }
      }

      public bool IsSynchronized
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsUnmanaged
      {
        get { return false; }
      }

      public bool IsVirtual
      {
        get { return false; }
      }

      public IEnumerable<IParameterDefinition> Parameters
      {
        get
        {
          ushort n = (ushort)this.reference.arrayType.Rank;
          if (this.reference.arrayOperation == OperationCode.Array_Create_WithLowerBound) n *= 2;
          for (ushort i = 0; i < n; i++)
            yield return new DummyArrayMethodParameter(this, i, this.reference.platformType.SystemInt32);
          if (this.reference.arrayOperation == OperationCode.Array_Set)
            yield return new DummyArrayMethodParameter(this, n, this.reference.arrayType.ElementType);
        }
      }

      public IPlatformInvokeInformation PlatformInvokeData
      {
        get { throw new NotImplementedException(); }
      }

      public bool PreserveSignature
      {
        get { throw new NotImplementedException(); }
      }

      public bool RequiresSecurityObject
      {
        get { throw new NotImplementedException(); }
      }

      public IEnumerable<ICustomAttribute> ReturnValueAttributes
      {
        get { throw new NotImplementedException(); }
      }

      public bool ReturnValueIsMarshalledExplicitly
      {
        get { throw new NotImplementedException(); }
      }

      public IMarshallingInformation ReturnValueMarshallingInformation
      {
        get { throw new NotImplementedException(); }
      }

      public IName ReturnValueName
      {
        get { throw new NotImplementedException(); }
      }

      public IEnumerable<ISecurityAttribute> SecurityAttributes
      {
        get { throw new NotImplementedException(); }
      }

      public ITypeDefinition ContainingTypeDefinition
      {
        get { return this.reference.ContainingType.ResolvedType; }
      }

      public TypeMemberVisibility Visibility
      {
        get { throw new NotImplementedException(); }
      }

      public ITypeReference ContainingType
      {
        get { return this.reference.ContainingType; }
      }

      public ITypeDefinitionMember ResolvedTypeDefinitionMember
      {
        get { return this; }
      }

      public IEnumerable<ICustomAttribute> Attributes
      {
        get { return Enumerable<ICustomAttribute>.Empty; }
      }

      public void Dispatch(IMetadataVisitor visitor)
      {
        throw new NotImplementedException();
      }

      public void DispatchAsReference(IMetadataVisitor visitor)
      {
        throw new NotImplementedException();
      }

      public IEnumerable<ILocation> Locations
      {
        get { throw new NotImplementedException(); }
      }

      public new IName Name
      {
        get { return this.reference.Name; }
      }

      public ITypeDefinition Container
      {
        get { throw new NotImplementedException(); }
      }

      public IScope<ITypeDefinitionMember> ContainingScope
      {
        get { throw new NotImplementedException(); }
      }

      public bool AcceptsExtraArguments
      {
        get { throw new NotImplementedException(); }
      }

      public IEnumerable<IParameterTypeInformation> ExtraParameters
      {
        get { throw new NotImplementedException(); }
      }

      public ushort GenericParameterCount
      {
        get { return 0; }
      }

      public uint InternedKey
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsGeneric
      {
        get { return false; }
      }

      public ushort ParameterCount
      {
        get { throw new NotImplementedException(); }
      }

      public IMethodDefinition ResolvedMethod
      {
        get { return this; }
      }

      public CallingConvention CallingConvention
      {
        get { return Cci.CallingConvention.HasThis; }
      }

      IEnumerable<IParameterTypeInformation> ISignature.Parameters
      {
        get { throw new NotImplementedException(); }
      }

      public IEnumerable<ICustomModifier> ReturnValueCustomModifiers
      {
        get { throw new NotImplementedException(); }
      }

      public bool ReturnValueIsByRef
      {
        get { throw new NotImplementedException(); }
      }

      public bool ReturnValueIsModified
      {
        get { throw new NotImplementedException(); }
      }

      public new ITypeReference Type
      {
        get { throw new NotImplementedException(); }
      }
    }

  }

  /// <summary>
  /// A wrapper to make a lambda look like a method to the decoder.
  /// </summary>
  internal class LambdaMethodReference : IMethodReference
  {
    internal readonly IAnonymousDelegate anonymousDelegate;
    private readonly IMetadataHost host;
    private readonly IMethodReference fakeMethodReferenceJustToGetInternedKey;

    internal LambdaMethodReference(IMetadataHost host, IAnonymousDelegate anonymousDelegate, string uniqueName) {
      Contract.Requires(anonymousDelegate != null);

      this.host = host;
      this.anonymousDelegate = anonymousDelegate;

      var self = CciContractProvider.ThisReferenceFinder.FindThisReference(anonymousDelegate);
      ITypeReference containingType = self != null ? self.Type : host.PlatformType.SystemObject;


      var paramTypes = new ITypeReference[anonymousDelegate.Parameters.Count()];
      var i = 0;
      foreach (var p in anonymousDelegate.Parameters) {
        paramTypes[i++] = p.Type;
      }
      var name = "Lambda_" + uniqueName;
      var mr = new MethodReference(host,
        containingType,
        Cci.CallingConvention.Default,
        anonymousDelegate.ReturnType,
        this.host.NameTable.GetNameFor(name),
        0,
        paramTypes);
      this.fakeMethodReferenceJustToGetInternedKey = mr;
    }

    public ushort GenericParameterCount
    {
      get {
        Contract.Ensures(Contract.Result<ushort>() == 0);
        return 0; }
    }

    public bool IsGeneric
    {
      get { return false; }
    }

    public IMethodDefinition ResolvedMethod
    {
      get { return new LambdaMethodDefinition(this); }
    }

    public IEnumerable<IParameterTypeInformation> ExtraParameters
    {
      get { return Enumerable<IParameterTypeInformation>.Empty; }
    }

    public CallingConvention CallingConvention
    {
      get { return this.anonymousDelegate.CallingConvention; }
    }

    public void Dispatch(IMetadataVisitor visitor)
    {
    }

    public IEnumerable<IParameterTypeInformation> Parameters
    {
      get
      {
        foreach (var p in this.anonymousDelegate.Parameters)
        {
          yield return p;
        }
      }
    }

    public ushort ParameterCount
    {
      get
      {
        return (ushort)this.anonymousDelegate.Parameters.Count();
      }
    }

    public IEnumerable<ICustomModifier> ReturnValueCustomModifiers
    {
      get { return Enumerable<ICustomModifier>.Empty; }
    }

    public bool ReturnValueIsByRef
    {
      get { return false; }
    }

    public bool ReturnValueIsModified
    {
      get { return false; }
    }

    public ITypeReference Type
    {
      get
      {
        return this.anonymousDelegate.Type; // REVIEW: should this be a delegate type?
      }
    }

    public ITypeReference ContainingType
    {
      get { return this.fakeMethodReferenceJustToGetInternedKey.ContainingType; } // BUGBUG: This says System.Object for all lambdas!
    }

    public ITypeDefinitionMember ResolvedTypeDefinitionMember
    {
      get { return Dummy.Method; }
    }

    public IEnumerable<ICustomAttribute> Attributes
    {
      get { return Enumerable<ICustomAttribute>.Empty; }
    }

    public IEnumerable<ILocation> Locations
    {
      get { return Enumerable<ILocation>.Empty; }
    }

    public IName Name
    {
      get { return this.fakeMethodReferenceJustToGetInternedKey.Name; }
    }

    public uint InternedKey
    {
      get { return this.fakeMethodReferenceJustToGetInternedKey.InternedKey; }
    }

    public bool AcceptsExtraArguments
    {
      get { return false; }
    }

    public bool IsStatic { get { return this.anonymousDelegate.IsStatic; } }

    #region IReference Members


    public void DispatchAsReference(IMetadataVisitor visitor)
    {
      visitor.Visit(this);
    }

    #endregion

    private class LambdaMethodDefinition : Dummy, IMethodDefinition
    {
      readonly LambdaMethodReference reference;

      public LambdaMethodDefinition(LambdaMethodReference reference)
      {
        this.reference = reference;
      }

      public IMethodBody Body
      {
        get { return Dummy.MethodBody; }
      }

      public IEnumerable<IGenericMethodParameter> GenericParameters
      {
        get { return Enumerable<IGenericMethodParameter>.Empty; }
      }

      public bool HasDeclarativeSecurity
      {
        get { throw new NotImplementedException(); }
      }

      public bool HasExplicitThisParameter
      {
        get { return false; }
      }

      public bool IsAbstract
      {
        get { return false; }
      }

      public bool IsAccessCheckedOnOverride
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsAggressivelyInlined { get { return false; } }

      public bool IsCil
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsConstructor
      {
        get { return false; }
      }

      public bool IsExternal
      {
        get { return false; }
      }

      public bool IsForwardReference
      {
        get { return false; }
      }

      public bool IsHiddenBySignature
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsNativeCode
      {
        get { return false; }
      }

      public bool IsNeverInlined
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsNeverOptimized
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsNewSlot
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsPlatformInvoke
      {
        get { return false; }
      }

      public bool IsRuntimeImplemented
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsRuntimeInternal
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsRuntimeSpecial
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsSealed
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsSpecialName
      {
        get { return false; }
      }

      public bool IsStatic
      {
        get { return this.reference.IsStatic; }
      }

      public bool IsStaticConstructor
      {
        get { return false; }
      }

      public bool IsSynchronized
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsUnmanaged
      {
        get { return false; }
      }

      public bool IsVirtual
      {
        get { return false; }
      }

      public IEnumerable<IParameterDefinition> Parameters
      {
        get
        {
          foreach (var p in this.reference.anonymousDelegate.Parameters)
          {
            yield return p;
          }
        }
      }

      public IPlatformInvokeInformation PlatformInvokeData
      {
        get { throw new NotImplementedException(); }
      }

      public bool PreserveSignature
      {
        get { throw new NotImplementedException(); }
      }

      public bool RequiresSecurityObject
      {
        get { throw new NotImplementedException(); }
      }

      public IEnumerable<ICustomAttribute> ReturnValueAttributes
      {
        get { throw new NotImplementedException(); }
      }

      public bool ReturnValueIsMarshalledExplicitly
      {
        get { throw new NotImplementedException(); }
      }

      public IMarshallingInformation ReturnValueMarshallingInformation
      {
        get { throw new NotImplementedException(); }
      }

      public IName ReturnValueName
      {
        get { throw new NotImplementedException(); }
      }

      public IEnumerable<ISecurityAttribute> SecurityAttributes
      {
        get { throw new NotImplementedException(); }
      }

      public ITypeDefinition ContainingTypeDefinition
      {
        get { return this.reference.ContainingType.ResolvedType; }
      }

      public TypeMemberVisibility Visibility
      {
        get { return TypeMemberVisibility.Public; }
      }

      public ITypeReference ContainingType
      {
        get { return this.reference.ContainingType; }
      }

      public ITypeDefinitionMember ResolvedTypeDefinitionMember
      {
        get { return this; }
      }

      public IEnumerable<ICustomAttribute> Attributes
      {
        get { return Enumerable<ICustomAttribute>.Empty; }
      }

      public void Dispatch(IMetadataVisitor visitor)
      {
        throw new NotImplementedException();
      }

      public void DispatchAsReference(IMetadataVisitor visitor)
      {
        throw new NotImplementedException();
      }

      public IEnumerable<ILocation> Locations
      {
        get { return this.reference.Locations; }
      }

      public new IName Name
      {
        get { return this.reference.Name; }
      }

      public ITypeDefinition Container
      {
        get { throw new NotImplementedException(); }
      }

      public IScope<ITypeDefinitionMember> ContainingScope
      {
        get { throw new NotImplementedException(); }
      }

      public bool AcceptsExtraArguments
      {
        get { return this.reference.AcceptsExtraArguments; }
      }

      public IEnumerable<IParameterTypeInformation> ExtraParameters
      {
        get { return this.reference.ExtraParameters; }
      }

      public ushort GenericParameterCount
      {
        get { return this.reference.GenericParameterCount; }
      }

      public uint InternedKey
      {
        get { return this.reference.InternedKey; }
      }

      public bool IsGeneric
      {
        get { return this.reference.IsGeneric; }
      }

      public ushort ParameterCount
      {
        get { return this.reference.ParameterCount; }
      }

      public IMethodDefinition ResolvedMethod
      {
        get { return this; }
      }

      public CallingConvention CallingConvention
      {
        get { return this.reference.CallingConvention; }
      }

      IEnumerable<IParameterTypeInformation> ISignature.Parameters
      {
        get { return this.reference.Parameters; }
      }

      public IEnumerable<ICustomModifier> ReturnValueCustomModifiers
      {
        get { return this.reference.ReturnValueCustomModifiers; }
      }

      public bool ReturnValueIsByRef
      {
        get { return this.reference.ReturnValueIsByRef; }
      }

      public bool ReturnValueIsModified
      {
        get { return this.reference.ReturnValueIsModified; }
      }

      public new ITypeReference Type
      {
        get { return this.reference.Type; }
      }
    }

  }

  internal class DummyTokenOf : ITokenOf
  {

    private readonly ITypeReference thisType;

    internal DummyTokenOf(ITypeReference type)
    {
      this.thisType = type;
    }

    public object Definition
    {
      get { return thisType; }
    }

    #region IExpression Members

    public void Dispatch(ICodeVisitor visitor)
    {
      visitor.Visit(this);
    }

    public bool IsPure
    {
      get { return true; }
    }

    public ITypeReference Type
    {
      get { return this.thisType; }
    }

    #endregion

    #region IErrorCheckable Members

    public bool HasErrors()
    {
      return false;
    }

    #endregion

    #region IObjectWithLocations Members

    public IEnumerable<ILocation> Locations
    {
      get { return Enumerable<ILocation>.Empty; }
    }

    #endregion
  }

  public struct FieldReferenceAdaptor : IEquatable<FieldReferenceAdaptor>
  {
    public readonly IFieldReference/*?*/ reference;
    private FieldReferenceAdaptor(IFieldReference/*?*/ fieldReference)
    {
      reference = fieldReference;
    }
    public static FieldReferenceAdaptor AdaptorOf(IFieldReference iref)
    {
      return new FieldReferenceAdaptor(iref);
    }
    public static FieldReferenceAdaptor Identity(FieldReferenceAdaptor field)
    {
      return field;
    }
    public static IEnumerable<FieldReferenceAdaptor> AdaptorsOf(IEnumerable<IFieldReference> fields)
    {
      foreach (IFieldReference fref in fields)
        yield return AdaptorOf(fref);
    }
    public override string ToString()
    {
      if (reference == null) return "(null)";
      return "FieldAdaptor:" + reference;
    }
    #region IEquatable
    public bool Equals(FieldReferenceAdaptor that)
    {
      return this.reference == that.reference
        || this.reference != null && that.reference != null &&
        TypeHelper.TypesAreEquivalent(this.reference.ContainingType, that.reference.ContainingType)
        && this.reference.Name == that.reference.Name;
    }
    #endregion

    #region Equals
    public override bool Equals(object obj)
    {
      IFieldReference other = obj as IFieldReference;
      if (other == null) return false;
      return TypeHelper.TypesAreEquivalent(this.reference.ContainingType, other.ContainingType)
        && this.reference.Name == other.Name;
    }

    public override int GetHashCode()
    {
      var x = this.reference.ContainingType.InternedKey << 2 ^ this.reference.Name.UniqueKey;
      return (int)x;
    }
    #endregion
  }

  public struct MethodReferenceAdaptor : IEquatable<MethodReferenceAdaptor>
  {
    public readonly IMethodReference/*?*/ reference;
    private MethodReferenceAdaptor(IMethodReference/*?*/ methodReference)
    {
      reference = methodReference;
    }
    public static MethodReferenceAdaptor AdaptorOf(IMethodReference iref)
    {
      return new MethodReferenceAdaptor(iref);
    }
    public static MethodReferenceAdaptor Identity(MethodReferenceAdaptor method)
    {
      return method;
    }
    public static IEnumerable<MethodReferenceAdaptor> AdaptorsOf(IEnumerable<IMethodReference> methods)
    {
      foreach (IMethodReference mref in methods)
        yield return AdaptorOf(mref);
    }
    public override string ToString()
    {
      if (reference == null) return "(null)";
      return "MethodAdaptor:" + reference;
    }
    #region IEquatable
    public bool Equals(MethodReferenceAdaptor that)
    {
      return this.reference == that.reference ||
             (this.reference != null && that.reference != null && this.reference.InternedKey == that.reference.InternedKey);
    }
    #endregion

    #region Equals
    public override bool Equals(object obj)
    {
      return (obj is MethodReferenceAdaptor) && this.Equals((MethodReferenceAdaptor)obj);
    }

    public override int GetHashCode()
    {
      return (int)this.reference.InternedKey;
    }
    #endregion
  }

  public struct TypeReferenceAdaptor : IEquatable<TypeReferenceAdaptor>
  {
    public readonly ITypeReference/*?*/ reference;
    private TypeReferenceAdaptor(ITypeReference/*?*/ typeReference)
    {
      reference = typeReference;
    }
    public static TypeReferenceAdaptor AdaptorOf(ITypeReference iref)
    {
      //if (iref is TypeReferenceAdaptor) return iref;
      return new TypeReferenceAdaptor(iref);
    }
    public static TypeReferenceAdaptor Identity(TypeReferenceAdaptor type)
    {
      return type;
    }
    public static IEnumerable<TypeReferenceAdaptor> AdaptorsOf(IEnumerable<ITypeReference> types)
    {
      foreach (ITypeReference tref in types)
        yield return AdaptorOf(tref);
    }
    public override string ToString()
    {
      if (reference == null) return "(null)";
      var s = TypeHelper.GetTypeName(this.reference);
      return s;
    }
    public IAliasForType AliasForType
    {
      get
      {
        if (reference == null) return null;
        return reference.AliasForType;
      }
    }
    public IArrayTypeReference AsArrayTypeReference
    {
      get
      {
        if (reference == null) return null;
        return reference as IArrayTypeReference;
      }
    }
    public IFunctionPointerTypeReference AsFunctionPointerTypeReference
    {
      get
      {
        if (reference == null) return null;
        return reference as IFunctionPointerTypeReference;
      }
    }
    public IGenericMethodParameterReference AsGenericMethodParameterReference
    {
      get
      {
        if (reference == null) return null;
        return reference as IGenericMethodParameterReference;
      }
    }
    public IGenericTypeInstanceReference AsGenericTypeInstanceReference
    {
      get
      {
        if (reference == null) return null;
        return reference as IGenericTypeInstanceReference;
      }
    }
    public IGenericTypeParameterReference AsGenericTypeParameterReference
    {
      get
      {
        if (reference == null) return null;
        return reference as IGenericTypeParameterReference;
      }
    }
    public INamespaceTypeReference AsNamespaceTypeReference
    {
      get
      {
        if (reference == null) return null;
        return reference as INamespaceTypeReference;
      }
    }
    public INestedTypeReference AsNestedTypeReference
    {
      get
      {
        if (reference == null) return null;
        return reference as INestedTypeReference;
      }
    }
    public IPointerTypeReference AsPointerTypeReference
    {
      get
      {
        if (reference == null) return null;
        return reference as IPointerTypeReference;
      }
    }
    public uint InternedKey
    {
      // requires reference != null
      get
      {
        Contract.Assume(this.reference != null);

        return reference.InternedKey;
      }
    }
    public bool IsAlias
    {
      get
      {
        return this.reference != null && reference.IsAlias;
      }
    }
    public bool IsValueType
    {
      get {
        Contract.Requires(this.reference != null);
        return reference.IsValueType; }
    }
    public IPlatformType PlatformType
    {
      get
      {
        Contract.Assume(this.reference != null);

        return reference.PlatformType;
      }
    }
    public ITypeDefinition ResolvedType
    {
      get
      {
        Contract.Requires(this.reference != null);
        return reference.ResolvedType;
      }
    }    
    
    public PrimitiveTypeCode TypeCode
    {
      get
      {
        Contract.Requires(this.reference != null);
        return reference.TypeCode;
      }
    }
    public bool IsEnum { get { return reference != null && reference.IsEnum; } }

    #region ireference members
    public void Dispatch(IMetadataVisitor visitor)
    {
      Contract.Requires(visitor != null);
      Contract.Requires(this.reference != null);
      
      reference.Dispatch(visitor);
    }

    public IEnumerable<ICustomAttribute> Attributes { get { Contract.Requires(this.reference != null); return reference.ResolvedType.Attributes; } }
    public IEnumerable<ILocation> Locations { get { Contract.Requires(this.reference != null); return reference.Locations; } }
    #endregion

    #region IEquatable
    public bool Equals(TypeReferenceAdaptor that)
    {
      // make sure null is treated equal
      return this.reference == that.reference || TypeHelper.TypesAreEquivalent(this.reference, that.reference);
    }
    #endregion

    #region Equals
    public override bool Equals(object obj)
    {
      if (this.reference == null)
        return obj == null || (obj is TypeReferenceAdaptor && ((TypeReferenceAdaptor)obj).reference == null);
      if (obj is TypeReferenceAdaptor)
      {
        var that = ((TypeReferenceAdaptor)obj).reference;

        return that != null && this.reference.InternedKey == that.InternedKey;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return (int)this.reference.InternedKey;
    }
    #endregion

    #region IReference Members


    public void DispatchAsReference(IMetadataVisitor visitor)
    {
      Contract.Requires(visitor != null);
      Contract.Requires(this.reference != null);
      this.reference.DispatchAsReference(visitor);
    }

    #endregion
  }

  public class CciMetadataDecoder : IDecodeMetaData<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, IPropertyDefinition, IEventDefinition, TypeReferenceAdaptor, ICustomAttribute, IAssemblyReference>
  {

    internal CciMetadataDecoder(CciILCodeProvider parent, HostEnvironment host, IPlatformType platform)
    {
      Contract.Requires(host != null);

      this.parent = parent;
      this.host = host;
      this.internFactory = host.InternFactory;
      this.platform = platform ?? host.PlatformType;
      this.methodBodyCallback = new MethodBodyCallback();
    }

    private readonly CciILCodeProvider parent;
    private readonly HostEnvironment host;
    private readonly IInternFactory internFactory;
    private readonly IPlatformType platform;
    internal readonly MethodBodyCallback methodBodyCallback;
    private readonly Dictionary<MethodReferenceAdaptor, IParameterTypeInformation> thisParameterFor = new Dictionary<MethodReferenceAdaptor, IParameterTypeInformation>();
    private readonly Dictionary<IUnitReference, ISourceLocationProvider> unit2SourceLocationProvider = new Dictionary<IUnitReference, ISourceLocationProvider>();
    /// <summary>
    /// Lambdas are returned from this provider as Methods. As such, they need a unique name so that the caching that is done on the
    /// Clousot side works correctly. However, this unique name must be stable across different analyses of the same assembly. That is,
    /// it *cannot* depend on the object identity of the anonymous delegate. When Clousot runs as a service it may analyze the same
    /// assembly more than once and if the name of the lambda encodes the object identity then it will be missed when Clousot tries to
    /// look it up in its cache.
    /// </summary>
    internal readonly Dictionary<WeakReference /* AnonymousDelegate | IMethodReference */, string /*uniqueName*/> lambdaNames =
      new Dictionary<WeakReference, string>(new WeakReferenceComparer());

    class WeakReferenceComparer : IEqualityComparer<WeakReference> {
      public bool Equals(WeakReference x, WeakReference y) {
        return x.Target == y.Target;
      }

      public int GetHashCode(WeakReference obj) {
        return obj.Target.GetHashCode();
      }
    }

    #region Fields

    public TypeReferenceAdaptor FieldType(FieldReferenceAdaptor field)
    {
      return TypeReferenceAdaptor.AdaptorOf(field.reference.Type);
    }

    public string FullName(FieldReferenceAdaptor field)
    {
      return TypeHelper.GetTypeName(field.reference.ContainingType) + "." + field.reference.Name.Value;
    }

    public bool IsStatic(FieldReferenceAdaptor field)
    {
      return field.reference.IsStatic;
    }

    public TypeReferenceAdaptor DeclaringType(FieldReferenceAdaptor field)
    {
      return TypeReferenceAdaptor.AdaptorOf(field.reference.ContainingType);
    }

    public bool IsPrivate(FieldReferenceAdaptor field)
    {
      return field.reference.ResolvedField.Visibility == TypeMemberVisibility.Private;
    }

    public bool IsProtected(FieldReferenceAdaptor field)
    {
      return field.reference.ResolvedField.Visibility == TypeMemberVisibility.Family
        || field.reference.ResolvedField.Visibility == TypeMemberVisibility.FamilyAndAssembly
        || field.reference.ResolvedField.Visibility == TypeMemberVisibility.FamilyOrAssembly;
    }

    public bool IsPublic(FieldReferenceAdaptor field)
    {
      return field.reference.ResolvedField.Visibility == TypeMemberVisibility.Public;
    }

    public bool IsInternal(FieldReferenceAdaptor field)
    {
      return field.reference.ResolvedField.Visibility == TypeMemberVisibility.Assembly
        || field.reference.ResolvedField.Visibility == TypeMemberVisibility.FamilyAndAssembly
        || field.reference.ResolvedField.Visibility == TypeMemberVisibility.FamilyOrAssembly;
    }

    public string Name(FieldReferenceAdaptor field)
    {
      return field.reference.Name.Value;
    }

    public bool IsCompilerGenerated(FieldReferenceAdaptor field)
    {
      foreach (var attr in field.reference.Attributes)
      {
        string name = this.Name(attr.Type);
        if (name == "CompilerGeneratedAttribute") return true;
      }
      return false;
    }

    public bool TryInitialValue(FieldReferenceAdaptor field, out object value)
    {
      if (field.reference.ResolvedField.IsCompileTimeConstant)
      {
        value = field.reference.ResolvedField.CompileTimeValue.Value;
        return true;
      }
      else
      {
        value = null;
        return false;
      }
    }

    public bool Equal(FieldReferenceAdaptor f1, FieldReferenceAdaptor f2)
    {
      return f1.Equals(f2);
    }

    public IEnumerable<ICustomAttribute> GetAttributes(FieldReferenceAdaptor field)
    {
      return field.reference.ResolvedField.Attributes;
    }


    public bool IsReadonly(FieldReferenceAdaptor field)
    {
      IFieldDefinition fieldDef = field.reference as IFieldDefinition;
      return fieldDef != null && fieldDef.IsReadOnly;
    }

    public bool IsVolatile(FieldReferenceAdaptor field)
    {
      // TODO: get volatile ?
      IFieldDefinition fieldDef = field.reference.ResolvedField as IFieldDefinition;

      TypeReferenceAdaptor type = this.FieldType(field);
      TypeReferenceAdaptor modified;
      Microsoft.Research.DataStructures.IIndexable<Pair<bool, TypeReferenceAdaptor>> modifiers;
      if (this.IsModified(type, out modified, out modifiers))
      {
        for (int i = 0; i < modifiers.Count; ++i)
        {
          if (modifiers[i].Two.ToString() == "System.Runtime.CompilerServices.IsVolatile")
            return true;
        }
      }

      return false;
      //return fieldDef != null && fieldDef.IsVolatile;
    }

    public bool IsNewSlot(FieldReferenceAdaptor field)
    {
      // TODO: get newslot ?
      return false;
      //return field.reference.ResolvedField.IsNewSlot;
    }

    public bool IsAsVisibleAs(FieldReferenceAdaptor field, MethodReferenceAdaptor method)
    {
      return AsVisibleAs(field.reference, method.reference);
    }

    public bool IsAsVisibleAs(TypeReferenceAdaptor type, MethodReferenceAdaptor method)
    {
      return AsVisibleAs(type.reference, method.reference);
    }

    public bool IsSpecialized(FieldReferenceAdaptor field)
    {
      return field.reference is ISpecializedFieldReference;
    }

    public FieldReferenceAdaptor Unspecialized(FieldReferenceAdaptor field)
    {
      var specfield = field.reference as ISpecializedFieldReference;
      if (specfield != null)
      {
        return FieldReferenceAdaptor.AdaptorOf(specfield.UnspecializedVersion);
      }
      return field;
    }
    #endregion

    #region Properties

    public bool IsPropertyGetter(MethodReferenceAdaptor methodAdaptor)
    {
      var method = methodAdaptor.reference;
      var methodDefinition = method.ResolvedMethod;
      if (methodDefinition is Dummy) {
        return method.Name.Value.StartsWith("get_");
      }
      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("get_");
    }

    public bool IsPropertySetter(MethodReferenceAdaptor methodAdaptor)
    {
      var method = methodAdaptor.reference;
      var methodDefinition = method.ResolvedMethod;
      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("set_");
    }

    public IPropertyDefinition GetPropertyFromAccessor(MethodReferenceAdaptor methodAdaptor)
    {
      if (!IsPropertyGetter(methodAdaptor) && !IsPropertySetter(methodAdaptor)) return null;
      var method = methodAdaptor.reference;
      var methodDefinition = method.ResolvedMethod;
      if (!(methodDefinition is Dummy)) {
        // TODO: Need to cache this information. This is expensive.
        foreach (var p in methodDefinition.ContainingTypeDefinition.Properties) {
          if (p.Setter != null && p.Setter.ResolvedMethod.InternedKey == methodDefinition.InternedKey) {
            return p;
          } else if (p.Getter != null && p.Getter.ResolvedMethod.InternedKey == methodDefinition.InternedKey) {
            return p;
          }
        }
      } else {
      }
      // Some model method references cannot be resolved since they may not actually be members of the type
      // they claim to be their containing type. (This is the case for ones defined in reference assemblies.)
      // That means that any attempt to resolve them ends up in a dummy method definition.
      // So check the model methods list stored in the type contract of the method's containing type.

      return null;
    }

    public bool IsEventAdder(MethodReferenceAdaptor methodAdaptor, out IEventDefinition @event)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out @event) != null);

      IMethodReference method = methodAdaptor.reference;
      var dot = method.Name.Value.LastIndexOf('.');
      if (!method.Name.Value.StartsWith("add_")
        && (dot == -1 || !method.Name.Value.Substring(dot).StartsWith(".add_")))
      {
        @event = null;
        return false;
      }
      ITypeReference declaringType = method.ContainingType;
      foreach (var e in declaringType.ResolvedType.Events)
      {
        if (e.Adder != null && e.Adder.InternedKey == method.InternedKey)
        {
          @event = e;
          return true;
        }
      }
      @event = null;
      return false;
    }

    public bool IsEventRemover(MethodReferenceAdaptor methodAdaptor, out IEventDefinition @event)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out @event) != null);

      IMethodReference method = methodAdaptor.reference;
      var dot = method.Name.Value.LastIndexOf('.');
      if (!method.Name.Value.StartsWith("remove_")
        && (dot == -1 || !method.Name.Value.Substring(dot).StartsWith(".remove_")))
      {
        @event = null;
        return false;
      }
      ITypeReference declaringType = method.ContainingType;
      foreach (var e in declaringType.ResolvedType.Events)
      {
        if (e.Remover != null && e.Remover.InternedKey == method.InternedKey)
        {
          @event = e;
          return true;
        }
      }
      @event = null;
      return false;
    }

    public bool IsEventCaller(MethodReferenceAdaptor methodAdaptor, out IEventDefinition @event)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out @event) != null);

      Contract.Assume(methodAdaptor.reference != null);
      IMethodReference method = methodAdaptor.reference;
      var dot = method.Name.Value.LastIndexOf('.');
      if (!method.Name.Value.StartsWith("raise_")
        && (dot == -1 || !method.Name.Value.Substring(dot).StartsWith(".raise_")))
      {
        @event = null;
        return false;
      }
      ITypeReference declaringType = method.ContainingType;
      foreach (var e in declaringType.ResolvedType.Events)
      {
        if (e.Caller != null && e.Caller.InternedKey == method.InternedKey)
        {
          @event = e;
          return true;
        }
      }
      @event = null;
      return false;
    }

    public string Name(IPropertyDefinition property)
    {
      return property.Name.Value;
    }

    public TypeReferenceAdaptor PropertyType(IPropertyDefinition property)
    {
      return TypeReferenceAdaptor.AdaptorOf(property.Type);
    }

    public bool HasGetter(IPropertyDefinition property, out MethodReferenceAdaptor getter)
    {
      IMethodReference getRef = property.Getter;
      getter = MethodReferenceAdaptor.AdaptorOf(getRef);
      return (getRef != null);
    }

    public bool HasSetter(IPropertyDefinition property, out MethodReferenceAdaptor setter)
    {
      IMethodReference setRef = property.Setter;
      setter = MethodReferenceAdaptor.AdaptorOf(setRef);
      return (setRef != null);
    }

    public bool IsStatic(IPropertyDefinition prop)
    {
      return (prop.Getter != null && IsStatic(prop.Getter)) ||
        (prop.Setter != null && IsStatic(prop.Setter));
    }

    public bool IsOverride(IPropertyDefinition prop)
    {
      if (prop.Getter != null)
        return IsOverride(MethodReferenceAdaptor.AdaptorOf(prop.Getter));
      else if (prop.Setter != null)
        return IsOverride(MethodReferenceAdaptor.AdaptorOf(prop.Setter));
      throw new ArgumentException("prop is not a valid property");
    }

    public bool IsNewSlot(IPropertyDefinition prop)
    {
      if (prop.Getter != null)
        return IsNewSlot(MethodReferenceAdaptor.AdaptorOf(prop.Getter));
      else if (prop.Setter != null)
        return IsNewSlot(MethodReferenceAdaptor.AdaptorOf(prop.Setter));
      throw new ArgumentException("prop is not a valid property");
    }

    public bool IsSealed(IPropertyDefinition prop)
    {
      if (prop.Getter != null)
        return IsSealed(MethodReferenceAdaptor.AdaptorOf(prop.Getter));
      else if (prop.Setter != null)
        return IsSealed(MethodReferenceAdaptor.AdaptorOf(prop.Setter));
      throw new ArgumentException("prop is not a valid property");
    }

    public TypeReferenceAdaptor DeclaringType(IPropertyDefinition prop)
    {
      IMethodReference get_or_set = prop.Getter != null ? prop.Getter : prop.Setter;
      if (get_or_set == null)
        return TypeReferenceAdaptor.AdaptorOf(Dummy.Type);
      return DeclaringType(MethodReferenceAdaptor.AdaptorOf(get_or_set));
    }

    public bool Equal(IPropertyDefinition p1, IPropertyDefinition p2)
    {
      MethodReferenceAdaptor g1, g2, s1, s2;
      bool bg1 = HasGetter(p1, out g1);
      bool sg1 = HasSetter(p1, out s1);
      bool bg2 = HasGetter(p2, out g2);
      bool sg2 = HasSetter(p2, out s2);

      if (bg1 != bg2 || sg1 != sg2)
        return false;

      if (bg1 && !Equal(g1, g2))
        return false;
      if (sg1 && !Equal(s1, s2))
        return false;

      return true;
    }

    #endregion

    #region Types

    public bool Equal(TypeReferenceAdaptor type1, TypeReferenceAdaptor type2)
    {
      return type1.Equals(type2);
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public bool IsInterface(TypeReferenceAdaptor type)
    {
      ITypeDefinition typeDef = type.ResolvedType;
      return typeDef != null && typeDef.IsInterface;
    }

    public bool IsVoid(TypeReferenceAdaptor type)
    {
      return (type.TypeCode == PrimitiveTypeCode.Void);
    }

    public bool IsUnmanagedPointer(TypeReferenceAdaptor type)
    {
      return type.reference is IPointerTypeReference;
    }

    public bool IsManagedPointer(TypeReferenceAdaptor type)
    {
      return type.reference is IManagedPointerTypeReference;
    }

    public bool IsPrimitive(TypeReferenceAdaptor type)
    {
      switch (type.TypeCode)
      {
        case PrimitiveTypeCode.Boolean:
        case PrimitiveTypeCode.Char:
        case PrimitiveTypeCode.Int16:
        case PrimitiveTypeCode.Int32:
        case PrimitiveTypeCode.Int64:
        case PrimitiveTypeCode.Int8:
        case PrimitiveTypeCode.UInt16:
        case PrimitiveTypeCode.UInt32:
        case PrimitiveTypeCode.UInt64:
        case PrimitiveTypeCode.UInt8:
        case PrimitiveTypeCode.Float32:
        case PrimitiveTypeCode.Float64:
        case PrimitiveTypeCode.String:
        case PrimitiveTypeCode.UIntPtr:
        case PrimitiveTypeCode.IntPtr:
          return true;
        default:
          if (TypeHelper.TypesAreEquivalent(this.platform.SystemDecimal, type.reference)) return true;
          return false;
      }
    }

    public bool IsStruct(TypeReferenceAdaptor type)
    {
      return type.IsValueType;
    }

    public bool IsArray(TypeReferenceAdaptor type)
    {
      return type.AsArrayTypeReference != null;
    }

    public bool IsAbstract(TypeReferenceAdaptor type)
    {
      return type.reference.ResolvedType.IsAbstract;
    }

    public bool IsPublic(TypeReferenceAdaptor type)
    {
      var t = TypeHelper.TypeVisibilityAsTypeMemberVisibility(type.reference.ResolvedType);
      return t == TypeMemberVisibility.Public;
    }

    public bool IsPrivate(TypeReferenceAdaptor type)
    {
      var t = TypeHelper.TypeVisibilityAsTypeMemberVisibility(type.reference.ResolvedType);
      return t == TypeMemberVisibility.Private;
    }

    public bool IsProtected(TypeReferenceAdaptor type)
    {
      var t = TypeHelper.TypeVisibilityAsTypeMemberVisibility(type.reference.ResolvedType);
      return t == TypeMemberVisibility.Family || t == TypeMemberVisibility.FamilyAndAssembly || t == TypeMemberVisibility.FamilyOrAssembly;
    }

    public bool IsInternal(TypeReferenceAdaptor type)
    {
      var t = TypeHelper.TypeVisibilityAsTypeMemberVisibility(type.reference.ResolvedType);

      return t == TypeMemberVisibility.Assembly
        || t == TypeMemberVisibility.FamilyAndAssembly
        || t == TypeMemberVisibility.FamilyOrAssembly;
    }

    public bool IsSealed(TypeReferenceAdaptor type)
    {
      return type.reference.ResolvedType.IsSealed;
    }

    public bool IsStatic(TypeReferenceAdaptor type)
    {
      return type.reference.ResolvedType.IsStatic;
    }

    public bool IsDelegate(TypeReferenceAdaptor type)
    {
      return type.reference.ResolvedType.IsDelegate;
    }

    public int Rank(TypeReferenceAdaptor type)
    {
      IArrayTypeReference at = type.AsArrayTypeReference;
      Contract.Assume(at != null);
      return (int)at.Rank;
    }

    public bool IsNested(TypeReferenceAdaptor type, out TypeReferenceAdaptor/*?*/ parentType)
    {
      INestedTypeReference/*?*/ nestedType = type.AsNestedTypeReference;
      if (nestedType != null)
        parentType = TypeReferenceAdaptor.AdaptorOf(nestedType.ContainingType);
      else
        parentType = TypeReferenceAdaptor.AdaptorOf(null);
      return nestedType != null;
    }

    public bool IsModified(TypeReferenceAdaptor type, out TypeReferenceAdaptor modified, out IIndexable<Pair<bool, TypeReferenceAdaptor>> modifiers)
    {
      var mod = type.reference as IModifiedTypeReference;
      if (mod != null)
      {
        List<Pair<bool, TypeReferenceAdaptor>> mods = new List<Pair<bool, TypeReferenceAdaptor>>();
        foreach (ICustomModifier modifier in mod.CustomModifiers)
        {
          mods.Add(new Pair<bool, TypeReferenceAdaptor>(modifier.IsOptional, TypeReferenceAdaptor.AdaptorOf(modifier.Modifier)));
        }
        modified = TypeReferenceAdaptor.AdaptorOf(mod.UnmodifiedType);
        modifiers = mods.AsIndexable();
        return true;
      }
      modified = new TypeReferenceAdaptor();
      modifiers = null;
      return false;
    }

    public bool IsRequiredModifier(TypeReferenceAdaptor type, out TypeReferenceAdaptor modified, out TypeReferenceAdaptor modifier)
    {
      modified = new TypeReferenceAdaptor();
      modifier = new TypeReferenceAdaptor();
      return false; // TODO
    }

    public bool IsSpecialized(TypeReferenceAdaptor type, out IIndexable<TypeReferenceAdaptor> typeParameters)
    {
      var instance = type.AsGenericTypeInstanceReference;
      if (instance != null)
      {
        typeParameters = new IndexableCollection<TypeReferenceAdaptor, TypeReferenceAdaptor>(
          TypeReferenceAdaptor.AdaptorsOf(instance.GenericArguments), TypeReferenceAdaptor.Identity);
        return true;
      }
      typeParameters = null;
      return false;
    }

    private void AccumulateTypeParameters(ITypeReference type, List<TypeReferenceAdaptor> typeParameters)
    {
      INestedTypeReference nested;
      var gtir = type as IGenericTypeInstanceReference;
      nested = gtir != null ? gtir.GenericType as INestedTypeReference : type as INestedTypeReference;
      if (nested != null)
      {
        AccumulateTypeParameters(nested.ContainingType, typeParameters);
      }
      var instance = type as IGenericTypeInstanceReference;
      if (instance != null)
      {
        typeParameters.AddRange(TypeReferenceAdaptor.AdaptorsOf(instance.GenericArguments));
      }
    }

    private bool NormalizedIsSpecialized(ITypeReference type, out IIndexable<TypeReferenceAdaptor> typeParameters)
    {
      List<TypeReferenceAdaptor> parameters = new List<TypeReferenceAdaptor>();
      AccumulateTypeParameters(type, parameters);
      if (parameters.Count > 0)
      {
        typeParameters = parameters.AsIndexable();
        return true;
      }
      typeParameters = null;
      return false;
    }

    [ContractVerification(false)] // Switching off, because the postcondition is too complex, and we should go and add it all over the places
    public bool NormalizedIsSpecialized(TypeReferenceAdaptor type, out IIndexable<TypeReferenceAdaptor> typeParameters)
    {
      return this.NormalizedIsSpecialized(type.reference, out typeParameters);
    }

    private void AccumulateTypeFormals(ITypeReference type, List<TypeReferenceAdaptor> typeFormals, bool normalized)
    {
      Contract.Requires(typeFormals != null);

      var nested = type as INestedTypeReference;
      if (nested != null)
      {
        if (normalized) AccumulateTypeFormals(nested.ContainingType, typeFormals, normalized);
        foreach (var p in nested.ResolvedType.GenericParameters)
        {
          typeFormals.Add(TypeReferenceAdaptor.AdaptorOf(p));
        }
        return;
      }
      var nstype = type as INamespaceTypeReference;
      if (nstype == null) { return; }
      foreach (var p in nstype.ResolvedType.GenericParameters)
      {
        typeFormals.Add(TypeReferenceAdaptor.AdaptorOf(p));
      }
    }

    public bool IsGeneric(TypeReferenceAdaptor type, out IIndexable<TypeReferenceAdaptor> typeFormals, bool normalized)
    {
      List<TypeReferenceAdaptor> formals = new List<TypeReferenceAdaptor>();
      AccumulateTypeFormals(type.reference, formals, normalized);

      if (formals.Count > 0)
      {
        typeFormals = formals.AsIndexable();
        return true;
      }
      typeFormals = null;
      return false;
    }


    /// <summary>
    /// Instantiate as many args as needed and produce new remaining args array
    /// </summary>
    private ITypeDefinition Specialize(INamedTypeDefinition typedef, ref int argStart, IEnumerable<ITypeReference> args)
    {
      Contract.Requires(typedef != null);
      Contract.Requires(args != null);
      Contract.Ensures(Contract.Result<ITypeDefinition>() != null);

      ITypeDefinition result = typedef;
      var nested = typedef as INestedTypeDefinition;
      if (nested != null)
      {
        var parent = Specialize((INamedTypeDefinition)nested.ContainingTypeDefinition, ref argStart, args);
        result = TypeHelper.GetNestedType(parent, typedef.Name, typedef.GenericParameterCount);
      }
      if (typedef.GenericParameterCount > 0 && !(result is Dummy))
      {
        result = GenericTypeInstance.GetGenericTypeInstance((INamedTypeDefinition)result, args.Skip(argStart).Take(typedef.GenericParameterCount), this.internFactory);
        argStart += typedef.GenericParameterCount;
      }
      return result;
    }


    public TypeReferenceAdaptor Specialize(TypeReferenceAdaptor type, TypeReferenceAdaptor[] typeArguments)
    {
      var named = type.reference.ResolvedType as INamedTypeDefinition;
      if (named == null)
      {
        throw new InvalidOperationException("need named type to specialize");
      }
      var startIndex = 0;
      var result = Specialize(named, ref startIndex, typeArguments.ApplyToAll(adapter => adapter.reference));

      Contract.Assume(startIndex == typeArguments.Length);
      return TypeReferenceAdaptor.AdaptorOf(result);
#if OLD
      var t = type.reference;
      var resolvedT = t.ResolvedType;

      if (!resolvedT.IsGeneric)
        throw new InvalidOperationException();
      var nsType = resolvedT as INamespaceTypeDefinition;
      if (nsType == null)
        throw new InvalidOperationException();
      var gas = new List<ITypeReference>();
      foreach (var ta in typeArguments)
      {
        gas.Add(ta.reference);

      }
      var gtir = new Microsoft.Cci.MutableCodeModel.GenericTypeInstanceReference()
      {
        GenericType = nsType,
        GenericArguments = gas,
      };
      return TypeReferenceAdaptor.AdaptorOf(gtir);
#endif
    }

    public bool IsCompilerGenerated(TypeReferenceAdaptor typeAdaptor)
    {
      return IsCompilerGenerated(typeAdaptor.reference);
    }

    public bool IsDebuggerNonUserCode(TypeReferenceAdaptor typeAdaptor)
    {
      foreach (var attr in typeAdaptor.reference.ResolvedType.Attributes)
      {
        string name = this.Name(attr.Type);
        if (name == "DebuggerNonUserCodeAttribute") return true;
      }
      return false;
    }

    public bool IsNativeCpp(TypeReferenceAdaptor typeAdaptor)
    {
      return IsNativeCpp(typeAdaptor.reference);
    }

    public bool IsCompilerGenerated(ITypeReference type)
    {
      Contract.Requires(type != null);

      foreach (var attr in type.ResolvedType.Attributes)
      {
        string name = this.Name(attr.Type);
        if (name == "CompilerGeneratedAttribute") return true;
        if (name == "GeneratedCodeAttribute") return true;
      }
      return false;
    }

    public bool IsNativeCpp(ITypeReference type)
    {
      Contract.Requires(type != null);

      foreach (var attr in type.ResolvedType.Attributes)
      {
        string name = this.Name(attr.Type);
        if (name == "NativeCppClassAttribute") return true;
      }
      return false;
    }

    public static ITypeReference Unspecialized(ITypeReference type)
    {
      var instance = type as IGenericTypeInstanceReference;
      if (instance != null)
      {
        type = instance.GenericType;
      }
      var specialized = type as ISpecializedNestedTypeReference;
      if (specialized != null)
      {
        type = specialized.UnspecializedVersion;
      }
      return type;
    }

    public TypeReferenceAdaptor Unspecialized(TypeReferenceAdaptor type)
    {
      return TypeReferenceAdaptor.AdaptorOf(Unspecialized(type.reference));
    }

    public Guid DeclaringModule(TypeReferenceAdaptor type)
    {
      INamespaceTypeDefinition/*?*/ nsType = type.ResolvedType as INamespaceTypeDefinition;
      if (nsType != null)
      {
        IModule/*?*/ module = nsType.ContainingUnitNamespace.Unit as IModule;
        if (module != null) return module.PersistentIdentifier;
      }
      else
      {
        INestedTypeReference neType = type.AsNestedTypeReference;
        if (neType != null) return DeclaringModule(TypeReferenceAdaptor.AdaptorOf(neType.ContainingType));
      }
      return Guid.Empty;
    }

    public IEnumerable<FieldReferenceAdaptor> Fields(TypeReferenceAdaptor type)
    {
      foreach (var field in type.ResolvedType.Fields)
      {
        yield return FieldReferenceAdaptor.AdaptorOf(field);
      }
    }

    public IEnumerable<IPropertyDefinition> Properties(TypeReferenceAdaptor type)
    {
      foreach (var prop in type.ResolvedType.Properties) yield return prop;
    }

    public string FullName(TypeReferenceAdaptor type)
    {
      return TypeHelper.GetTypeName(type.reference, NameFormattingOptions.PreserveSpecialNames | NameFormattingOptions.UseGenericTypeNameSuffix);
    }

    public TypeReferenceAdaptor ArrayType(TypeReferenceAdaptor type, int rank)
    {
      ITypeReference result;
      if (rank == 1)
      {
        result = Microsoft.Cci.Immutable.Vector.GetVector(type.reference, this.internFactory);
      }
      else
      {
        result = Microsoft.Cci.Immutable.Matrix.GetMatrix(type.reference, (uint)rank, this.internFactory);
      }
      return TypeReferenceAdaptor.AdaptorOf(result);
    }

    public TypeReferenceAdaptor ManagedPointer(TypeReferenceAdaptor type)
    {
      return TypeReferenceAdaptor.AdaptorOf(Microsoft.Cci.Immutable.ManagedPointerType.GetManagedPointerType(type.reference, this.internFactory));
    }

    public TypeReferenceAdaptor UnmanagedPointer(TypeReferenceAdaptor type)
    {
      return TypeReferenceAdaptor.AdaptorOf(Microsoft.Cci.Immutable.PointerType.GetPointerType(type.reference, this.internFactory));
    }

    public TypeReferenceAdaptor ElementType(TypeReferenceAdaptor type)
    {
      IArrayTypeReference/*?*/ arrayType = type.AsArrayTypeReference;
      if (arrayType != null) return TypeReferenceAdaptor.AdaptorOf(arrayType.ElementType);
      IManagedPointerTypeReference/*?*/ managedPointerType = type.reference as IManagedPointerTypeReference;
      if (managedPointerType != null) return TypeReferenceAdaptor.AdaptorOf(managedPointerType.TargetType);
      IPointerTypeReference/*?*/ pointerType = type.AsPointerTypeReference;
      if (pointerType != null) return TypeReferenceAdaptor.AdaptorOf(pointerType.TargetType);
      return TypeReferenceAdaptor.AdaptorOf(Dummy.Type);
    }

    public int TypeSize(TypeReferenceAdaptor type)
    {
      return (int)TypeHelper.SizeOfType(type.reference);
    }

    public IEnumerable<MethodReferenceAdaptor> Methods(TypeReferenceAdaptor type)
    {
      return GetMethods(type.reference);
    }

    private IEnumerable<MethodReferenceAdaptor> GetMethods(ITypeReference type)
    {
      foreach (ITypeDefinitionMember mem in type.ResolvedType.Members)
      {
        IMethodDefinition method = mem as IMethodDefinition;
        if (method != null && !CciContractDecoder.IsContractAbbreviator(method))
        {
          yield return MethodReferenceAdaptor.AdaptorOf(method);
        }
      }
    }

    public IEnumerable<TypeReferenceAdaptor> NestedTypes(TypeReferenceAdaptor type)
    {
      return GetNestedTypes(type.reference);
    }

    private IEnumerable<TypeReferenceAdaptor> GetNestedTypes(ITypeReference type)
    {
      foreach (ITypeDefinitionMember mem in type.ResolvedType.Members)
      {
        INestedTypeDefinition nested = mem as INestedTypeDefinition;
        if (nested != null) yield return TypeReferenceAdaptor.AdaptorOf(nested);
      }
    }

    public string DeclaringModuleName(TypeReferenceAdaptor type)
    {
      return TypeHelper.GetDefiningUnitReference(type.reference).Name.Value;
      //INamespaceTypeReference nst = type.AsNamespaceTypeReference;
      //if (nst != null) return nst.ContainingUnitNamespace.Unit.Name.Value;
      //INestedTypeReference nested = type.AsNestedTypeReference;
      //if (nested != null)
      //{
      //  return DeclaringModuleName(TypeReferenceAdaptor.AdaptorOf(nested.ContainingType));
      //}
      //IArrayTypeReference atr = type.reference as IArrayTypeReference;
      //if (atr != null)
      //{
      //  return this.platform.SystemArray.ContainingUnitNamespace.Unit.Name.Value;
      //}
      //throw new InvalidOperationException();
    }

    public string Name(TypeReferenceAdaptor type)
    {
      return TypeHelper.GetTypeName(type.reference, NameFormattingOptions.UseGenericTypeNameSuffix | NameFormattingOptions.PreserveSpecialNames | NameFormattingOptions.OmitContainingNamespace | NameFormattingOptions.OmitContainingType | NameFormattingOptions.OmitTypeArguments);
    }

    public string NameOfDefinition(ITypeReference type)
    {
      INamespaceTypeReference nst = type as INamespaceTypeReference;
      if (nst != null) return nst.Name.Value;
      INestedTypeReference nested = type as INestedTypeReference;
      if (nested != null) return nested.Name.Value;
      // handle type parameters as having names
      IGenericParameterReference tp = type as IGenericParameterReference;
      if (tp != null) { return tp.Name.Value; }
      return null;
    }

    public string Name(ITypeReference type)
    {
      var result = NameOfDefinition(type);
      if (result != null) return result;
      return type.ToString();
    }

    public string Namespace(TypeReferenceAdaptor type)
    {
      INamespaceTypeReference nst = type.AsNamespaceTypeReference;
      if (nst != null) return TypeHelper.GetNamespaceName(nst.ContainingUnitNamespace, NameFormattingOptions.None);
      INestedTypeReference nested = type.AsNestedTypeReference;
      if (nested != null) return Namespace(TypeReferenceAdaptor.AdaptorOf(nested.ContainingType));
      return null;
    }

    public bool IsClass(TypeReferenceAdaptor type)
    {
      return IsClass(type.reference);
    }

    private bool IsClass(ITypeReference type)
    {
      INamespaceTypeReference ntr = type as INamespaceTypeReference;
      if (ntr != null) { return ntr.ResolvedType.IsClass; }
      INestedTypeReference nntr = type as INestedTypeReference;
      if (nntr != null) { return nntr.ResolvedType.IsClass; }
      var gtir = type as IGenericTypeInstanceReference;
      if (gtir != null) { return IsClass(gtir.GenericType); }
      return false;
    }

    public bool HasBaseClass(TypeReferenceAdaptor type)
    {
      return HasBaseClass(type.reference);
    }

    public static bool HasBaseClass(ITypeReference type)
    {
      Contract.Requires(type != null);

      return BaseClass(type) != null;
    }

    static public ITypeReference BaseClass(ITypeReference type)
    {
      Contract.Requires(type != null);

      if (type.ResolvedType.IsClass)
      {
        foreach (ITypeReference baseT in type.ResolvedType.BaseClasses)
        {
          return baseT;
        }
      }
      return null;
    }

    public TypeReferenceAdaptor BaseClass(TypeReferenceAdaptor type)
    {
      return TypeReferenceAdaptor.AdaptorOf(BaseClass(type.reference));
    }

    public IEnumerable<TypeReferenceAdaptor> Interfaces(TypeReferenceAdaptor type)
    {
      if (type.ResolvedType.IsClass || type.ResolvedType.IsStruct || type.ResolvedType.IsInterface)
      {
        return TypeReferenceAdaptor.AdaptorsOf(type.ResolvedType.Interfaces);
      }
      return Enumerable<TypeReferenceAdaptor>.Empty;
    }

    public IEnumerable<TypeReferenceAdaptor> TypeParameterConstraints(TypeReferenceAdaptor type)
    {
      IGenericTypeParameterReference tp = type.reference as IGenericTypeParameterReference;
      if (tp != null)
      {
        return TypeReferenceAdaptor.AdaptorsOf(tp.ResolvedType.Constraints);
      }
      IGenericMethodParameterReference mp = type.reference as IGenericMethodParameterReference;
      if (mp != null)
      {
        return TypeReferenceAdaptor.AdaptorsOf(mp.ResolvedType.Constraints);
      }
      return Enumerable<TypeReferenceAdaptor>.Empty;
    }


    public bool IsReferenceConstrained(TypeReferenceAdaptor type)
    {
      if (IsReferenceType(type)) return true;
      if (IsFormalTypeParameter(type))
        return type.AsGenericTypeParameterReference.ResolvedType.MustBeReferenceType;
      else if (IsMethodFormalTypeParameter(type))
        return type.AsGenericMethodParameterReference.ResolvedType.MustBeReferenceType;
      else
        return false;
    }

    public bool IsValueConstrained(TypeReferenceAdaptor type)
    {
      if (IsStruct(type))
        return true;
      if (IsFormalTypeParameter(type))
        return type.AsGenericTypeParameterReference.ResolvedType.MustBeValueType;
      else if (IsMethodFormalTypeParameter(type))
        return type.AsGenericMethodParameterReference.ResolvedType.MustBeValueType;
      else
        return false;
    }

    public bool IsConstructorConstrained(TypeReferenceAdaptor type)
    {
      if (IsFormalTypeParameter(type))
        return type.AsGenericTypeParameterReference.ResolvedType.MustHaveDefaultConstructor;
      else if (IsMethodFormalTypeParameter(type))
        return type.AsGenericMethodParameterReference.ResolvedType.MustHaveDefaultConstructor;
      else
        throw new ArgumentException("Type is not a generic type parameter reference.");
    }


    public bool IsFormalTypeParameter(TypeReferenceAdaptor type)
    {
      return type.AsGenericTypeParameterReference != null;
    }

    public bool IsMethodFormalTypeParameter(TypeReferenceAdaptor type)
    {
      return type.reference as IGenericMethodParameterReference != null;
    }

    public int NormalizedFormalTypeParameterIndex(TypeReferenceAdaptor type)
    {
      var gtpr = type.reference as IGenericTypeParameterReference;
      if (gtpr == null) throw new ArgumentException("Type is not a generic type parameter reference.");
      int result = gtpr.Index;
      var nested = gtpr.DefiningType as INestedTypeReference;
      while (nested != null)
      {
        var parent = nested.ContainingType;
        result += parent.ResolvedType.GenericParameterCount;
        nested = parent as INestedTypeReference;
      }
      return result;
    }

    public TypeReferenceAdaptor FormalTypeParameterDefiningType(TypeReferenceAdaptor type)
    {
      var gtpr = type.reference as IGenericTypeParameterReference;
      if (gtpr == null) throw new ArgumentException("Type is not a generic type parameter reference.");
      return TypeReferenceAdaptor.AdaptorOf(gtpr.DefiningType);
    }

    private IEnumerable<ITypeReference> NormalizedActualTypeArguments(ITypeReference type)
    {
      Contract.Ensures(Contract.Result<IEnumerable<ITypeReference>>() != null);

      var tr = type as IGenericTypeInstanceReference;
      IEnumerable<ITypeReference> sofar;
      if (tr == null)
      {
        sofar = Enumerable.Empty<ITypeReference>();
      }
      else
      {
        sofar = tr.GenericArguments;
        type = tr.GenericType;
      }
      INestedTypeReference nested = type as INestedTypeReference;
      if (nested == null) return sofar;
      return sofar.Union(NormalizedActualTypeArguments(nested.ContainingType));
    }

    public IIndexable<TypeReferenceAdaptor> NormalizedActualTypeArguments(TypeReferenceAdaptor typeref)
    {
      return NormalizedActualTypeArguments(typeref.reference).AsIndexable<ITypeReference, TypeReferenceAdaptor>(TypeReferenceAdaptor.AdaptorOf);
    }


    public IIndexable<TypeReferenceAdaptor> ActualTypeArguments(MethodReferenceAdaptor method)
    {
      IGenericMethodInstanceReference igmir = method.reference as IGenericMethodInstanceReference;
      if (igmir == null) return EmptyIndexable<TypeReferenceAdaptor>.Empty;
      return igmir.GenericArguments.AsIndexable<ITypeReference, TypeReferenceAdaptor>(TypeReferenceAdaptor.AdaptorOf);
    }

    public int MethodFormalTypeParameterIndex(TypeReferenceAdaptor type)
    {
      var gmpr = type.reference as IGenericMethodParameterReference;
      if (gmpr == null) throw new ArgumentException("Type is not a method-bound type parameter");
      return gmpr.Index;
    }

    public MethodReferenceAdaptor MethodFormalTypeDefiningMethod(TypeReferenceAdaptor type)
    {
      var gmpr = type.reference as IGenericMethodParameterReference;
      if (gmpr == null) throw new ArgumentException("Type is not a method-bound type parameter");
      return MethodReferenceAdaptor.AdaptorOf(gmpr.DefiningMethod);
    }

    public bool IsEnum(TypeReferenceAdaptor type)
    {
      return type.ResolvedType.IsEnum;
    }

    public bool HasFlagsAttribute(TypeReferenceAdaptor type)
    {
      foreach (var attr in type.ResolvedType.Attributes)
      {
        // F: ask how to do it better in cci2
        string name = this.Name(attr.Type);
        if (name == "FlagsAttribute") return true;
      }
      return false;
    }

    public TypeReferenceAdaptor TypeEnum(TypeReferenceAdaptor type)
    {
      if (IsEnum(type))
      {
        foreach (var f in Fields(type))
        {
          if (Name(f) == "value__")
            return FieldType(f);
        }
        throw new ArgumentException("Type is not a proper enum (no value__ field)");
      }
      else
        throw new ArgumentException("Type is not an enum");
    }

    /// <summary>
    /// The meaning of this is *not* that the type is a reference type, but that
    /// it is a type which can be dereferenced. For instance, int& is not a
    /// reference type, but it is a pointer that can be dereferenced.
    /// </summary>
    public bool IsReferenceType(TypeReferenceAdaptor type)
    {
      return type.reference.ResolvedType.IsReferenceType || type.reference is IManagedPointerTypeReference;
    }

    public bool IsNativePointerType(TypeReferenceAdaptor type)
    {
      if (type.reference is IPointerTypeReference) return true;
      if (platform.SystemIntPtr.InternedKey == type.InternedKey) return true;
      if (platform.SystemUIntPtr.InternedKey == type.InternedKey) return true;
      return false;
    }

    public bool DerivesFrom(TypeReferenceAdaptor sub, TypeReferenceAdaptor super)
    {
      return DerivesFrom(sub.reference, super.reference);
    }

    public bool DerivesFrom(ITypeReference sub, ITypeReference super)
    {
      Contract.Requires(super != null);
      Contract.Requires(sub != null);

      var subResolved = sub.ResolvedType;
      if (TypeHelper.Type1DerivesFromOrIsTheSameAsType2(subResolved, super)) return true;
      if (TypeHelper.Type1ImplementsType2(subResolved, super)) return true;
      // REVIEW: Is this next line right? Arbitrary interface J needs to derive from System.Object.
      if (TypeHelper.TypesAreAssignmentCompatible(subResolved, super.ResolvedType)) return true;
      return false;
    }

    public bool DerivesFromIgnoringTypeArguments(TypeReferenceAdaptor sub, TypeReferenceAdaptor super)
    {
      var subResolved = sub.ResolvedType;
      var gtir = super.reference as IGenericTypeInstanceReference;
      if (gtir != null)
        return DerivesFromIgnoringTypeArguments(subResolved, gtir);
      else
        return DerivesFrom(sub, super);
    }
    private bool DerivesFromIgnoringTypeArguments(ITypeDefinition sub, IGenericTypeInstanceReference super)
    {
      Contract.Requires(sub != null);
      Contract.Requires(super != null);
      var gti = sub as IGenericTypeInstance;
      if (gti != null && TypeHelper.TypesAreEquivalentAssumingGenericMethodParametersAreEquivalentIfTheirIndicesMatch(gti.GenericType, super.GenericType)) return true;
      foreach (var j in sub.Interfaces)
      {
        if (DerivesFromIgnoringTypeArguments(j.ResolvedType, super)) return true;
      }
      foreach (ITypeReference baseClass in sub.BaseClasses)
      {
        if (DerivesFromIgnoringTypeArguments(baseClass.ResolvedType, super)) return true;
      }
      return false;
    }

    public int ConstructorsCount(TypeReferenceAdaptor type)
    {
      if (!IsClass(type) && !IsEnum(type))
        return 0;

      int count = 0;
      foreach (var m in Methods(type))
      {
        if (IsConstructor(m) && !IsStatic(m))
          ++count;
      }
      return count;
    }
    #endregion

    #region System Types (alphabetical)

    public TypeReferenceAdaptor System_Array
    {
      get
      {
        return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemArray);
      }
    }

    public TypeReferenceAdaptor System_Boolean
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemBoolean); }
    }

    public TypeReferenceAdaptor System_Char
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemChar); }
    }

    public TypeReferenceAdaptor System_DynamicallyTypedReference
    {
      get
      {
        return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemTypedReference);
      }
    }

    public TypeReferenceAdaptor System_Double
    {
      get
      {
        return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemFloat64);
      }
    }

    public TypeReferenceAdaptor System_Decimal
    {
        get
        {
            return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemDecimal);
        }
    }

    public bool TryGetSystemType(string fullname, out TypeReferenceAdaptor type)
    {
      // REVIEW: Doing it as a reference means it never fails. But then it might not be that informative.
      // But to resolve the reference means loading the core assembly, which otherwise might not have
      // to be done.
      if (fullname.Contains("IEnumerable")){
      }
      var coreAssemblyReference = new Microsoft.Cci.Immutable.AssemblyReference(this.host, this.host.CoreAssemblySymbolicIdentity);
      IUnitNamespaceReference ns = new Microsoft.Cci.Immutable.RootUnitNamespaceReference(coreAssemblyReference);
      string typeName;
      ushort genericParameterCount = 0;
      var backTickIndex = fullname.IndexOf('`'); // assume there is just one?
      if (backTickIndex == -1) {
        typeName = fullname;
      } else {
        typeName = fullname.Substring(0,backTickIndex);
        int x;
        if (!Int32.TryParse(fullname.Substring(backTickIndex + 1), out x)) {
          type = TypeReferenceAdaptor.AdaptorOf(Dummy.TypeReference);
          return false;
        } else {
          Contract.Assume(0 < x);
          Contract.Assume(x <= ushort.MaxValue);
          genericParameterCount = (ushort) x;
        }
      }
      var names = typeName.Split('.');
      for (int i = 0, n = names.Length - 1; i < n; i++)
        ns = new Microsoft.Cci.Immutable.NestedUnitNamespaceReference(ns, this.host.NameTable.GetNameFor(names[i]));
      var typeReference = new Microsoft.Cci.Immutable.NamespaceTypeReference(this.host, ns, this.host.NameTable.GetNameFor(names[names.Length - 1]), genericParameterCount, false, false, mangleName: (0 < genericParameterCount), typeCode: PrimitiveTypeCode.NotPrimitive);
      type = TypeReferenceAdaptor.AdaptorOf(typeReference);
      return true;
    }

    public TypeReferenceAdaptor System_Int8
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemInt8); }
    }

    public TypeReferenceAdaptor System_Int16
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemInt16); }
    }

    public TypeReferenceAdaptor System_Int32
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemInt32); }
    }

    public TypeReferenceAdaptor System_Int64
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemInt64); }
    }

    public TypeReferenceAdaptor System_IntPtr
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemIntPtr); }
    }

    public TypeReferenceAdaptor System_Object
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemObject); }
    }

    public TypeReferenceAdaptor System_RuntimeArgumentHandle
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemRuntimeArgumentHandle); }
    }

    public TypeReferenceAdaptor System_RuntimeFieldHandle
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemRuntimeFieldHandle); }
    }

    public TypeReferenceAdaptor System_RuntimeMethodHandle
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemRuntimeMethodHandle); }
    }

    public TypeReferenceAdaptor System_RuntimeTypeHandle
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemRuntimeTypeHandle); }
    }

    public TypeReferenceAdaptor System_Single
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemFloat32); }
    }

    public TypeReferenceAdaptor System_String
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemString); }
    }

    public TypeReferenceAdaptor System_Type
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemType); }
    }

    public TypeReferenceAdaptor System_UInt8
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemUInt8); }
    }

    public TypeReferenceAdaptor System_UInt16
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemUInt16); }
    }

    public TypeReferenceAdaptor System_UInt32
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemUInt32); }
    }

    public TypeReferenceAdaptor System_UInt64
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemUInt64); }
    }

    public TypeReferenceAdaptor System_UIntPtr
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemUIntPtr); }
    }

    public TypeReferenceAdaptor System_Void
    {
      get { return TypeReferenceAdaptor.AdaptorOf(this.platform.SystemVoid); }
    }

    #endregion

    #region Parameters

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public string Name(IParameterTypeInformation param)
    {
      INamedEntity namedEntity = param as INamedEntity;
      if (namedEntity == null || namedEntity.Name == null || string.IsNullOrEmpty(namedEntity.Name.Value))
      {
        return "A_" + param.Index;
      }
      return namedEntity.Name.Value;
    }

    public TypeReferenceAdaptor ParameterType(IParameterTypeInformation p)
    {
      ITypeReference result = p.Type;
      if (p.IsByReference || this.IsOut(p))
      {
        result = Microsoft.Cci.Immutable.ManagedPointerType.GetManagedPointerType(result, this.internFactory);
      }
      return TypeReferenceAdaptor.AdaptorOf(result/*.ResolvedType*/);
    }

    public IEnumerable<ICustomAttribute> GetAttributes(IParameterTypeInformation pt)
    {
      var p = GetParameter(pt);
      if (p != null)
      {
        return p.Attributes;
      }

      return new List<ICustomAttribute>(); // empty list
    }


    private IParameterDefinition GetParameter(IParameterTypeInformation p)
    {
      Contract.Requires(p != null);

      var pd = p as IParameterDefinition;
      if (pd != null) return pd;
      var mr = p.ContainingSignature as IMethodReference;
      if (mr == null) return null;
      var unspec = MemberHelper.UninstantiateAndUnspecialize(mr.ResolvedMethod);
      if (!(p.Index < unspec.Parameters.Count())) return null;
      pd = unspec.Parameters.ElementAt(p.Index);
      return pd;
    }

    public bool IsOut(IParameterTypeInformation p)
    {
      var pd = GetParameter(p);
      if (pd == null) return false;
      return pd.IsOut;
    }

    public int ArgumentIndex(IParameterTypeInformation p)
    {
      // REVIEW: This block is for the "this" parameter created for invariants
      // Can't use the if-then-else below because the dummy Method used as the
      // containing signature for that parameter is a "static" method.
      IMethodDefinition methodDefinition = p.ContainingSignature as IMethodDefinition;
      if (methodDefinition != null && methodDefinition is Dummy)
        return p.Index == ushort.MaxValue ? 0 : (int)p.Index + 1;

      if ((p.ContainingSignature.CallingConvention & CallingConvention.HasThis) == 0)
        return (int)p.Index; // static method
      else
        return p.Index == ushort.MaxValue ? 0 : (int)p.Index + 1;
    }

    public int ArgumentStackIndex(IParameterTypeInformation p)
    {
      int totalArgs = (int)IteratorHelper.EnumerableCount(p.ContainingSignature.Parameters);
      if (p.Index == ushort.MaxValue)
        return totalArgs;
      else
        return totalArgs - (int)p.Index - 1;
    }

    public MethodReferenceAdaptor DeclaringMethod(IParameterTypeInformation p)
    {
      IMethodReference/*?*/ method = p.ContainingSignature as IMethodReference;
      if (method == null) method = Dummy.Method;
      return MethodReferenceAdaptor.AdaptorOf(method);
    }

    public string Name(LocalDefAdaptor local)
    {
      if (local.Name is Dummy)
      {
        return Name(local.Type) + "_loc";
      }
      var unit = TypeHelper.GetDefiningUnitReference(local.MethodDefinition.ContainingType);
      var sourceLocationProvider = this.GetSourceLocationProvider(unit);
      if (sourceLocationProvider == null) return local.Name.Value;
      bool b;
      var name = sourceLocationProvider.GetSourceNameFor((ILocalDefinition)(local.local), out b);

      Contract.Assert(name != null, "It was a missing postcondition");

      return name;
    }

    #endregion

    #region Locals

    public TypeReferenceAdaptor LocalType(LocalDefAdaptor local)
    {
      var t = local.Type;
      if (local.IsReference)
      {
        t = Microsoft.Cci.Immutable.ManagedPointerType.GetManagedPointerType(t, this.internFactory);
      }
      return TypeReferenceAdaptor.AdaptorOf(t);
    }

    public bool IsPinned(LocalDefAdaptor local)
    {
      return local.IsPinned;
    }

    #endregion

    #region Modules

    public string Name(IAssemblyReference assembly)
    {
      return assembly.Name.Value;
    }


    public Version Version(IAssemblyReference assembly)
    {
      return assembly.Version;
    }

    public Guid AssemblyGuid(IAssemblyReference assembly)
    {
      return assembly.ResolvedAssembly.PersistentIdentifier;
    }

    private ISourceLocationProvider/*?*/ GetSourceLocationProvider(IUnitReference unit)
    {
      ISourceLocationProvider sourceLocationProvider = null;
      this.unit2SourceLocationProvider.TryGetValue(unit, out sourceLocationProvider);
      if (sourceLocationProvider != null)
        return sourceLocationProvider;

      IAssembly assem = unit as IAssembly;
      if (assem != null)
      {
        string pdbFile = Path.ChangeExtension(assem.Location, "pdb");
        if (File.Exists(pdbFile))
        {
          using (Stream pdbStream = File.OpenRead(pdbFile)) // MB: no need to keep a lock on the PDB file
            sourceLocationProvider = new PdbReader(pdbStream, host);
          this.unit2SourceLocationProvider.Add(unit, sourceLocationProvider);
        }
      }
      return sourceLocationProvider;
    }

    [ContractVerification(false)]
    public void RegisterSourceLocationProvider(IUnitReference unit, ISourceLocationProvider sourceLocationProvider)
    {
      this.unit2SourceLocationProvider.Add(unit, sourceLocationProvider);
    }

    /// <summary>
    /// This should be the only interface for retrieving primary source locations.
    /// </summary>
    /// <param name="unit">The unit the locations are from. (Locations don't necessarily have a link to the unit they
    /// belong to.) The unit is used to find a source location provider for that unit. (Providers are maintained
    /// in a cache in the MetadataDecoder.) If a provider cannot be found for the <paramref name="unit"/> then
    /// an emtpy enumerable is returned.
    /// </param>
    /// <param name="operations">
    /// If a source location provider is found, then the location of the operation indexed in <paramref name="operations"/>
    /// at <paramref name="startingIndex"/> is retrieved from the source location provider.
    /// If <paramref name="exact"/> is true, then whatever the source location provider returns is returned.
    /// Otherwise, if that operation doesn't have a primary source location, then the list of operations will
    /// be used to find the closest (preceding) primary source locations and those will be returned.
    /// </param>
    /// <param name="startingIndex">
    /// The index of the operation from whose location the corresponding primary source locations are to be found.
    /// </param>
    /// <param name="exact">
    /// True iff the client wants primary source locations only for the operation indexed by <paramref name="startingIndex"/>.
    /// </param>
    /// <param name="foundIndex">
    /// If <paramref name="exact"/> is false, then this will be the greatest index of the operation in <paramref name="operations"/>
    /// that did have a primary source location. The index will be no greater than <paramref name="startingIndex"/>.
    /// </param>
    /// <returns></returns>
    internal IEnumerable<IPrimarySourceLocation> GetPrimarySourceLocationsFor(IUnitReference unit, IList<IOperation> operations, int startingIndex, bool exact, out int foundIndex)
    {
      Contract.Requires(startingIndex >= 0);
      Contract.Ensures(Contract.Result<IEnumerable<IPrimarySourceLocation>>() != null);

      foundIndex = -1;
      var sourceLocationProvider = this.GetSourceLocationProvider(unit);
      if (sourceLocationProvider == null) return Enumerable<IPrimarySourceLocation>.Empty;
      var location = operations[startingIndex].Location;
      var primaries = sourceLocationProvider.GetPrimarySourceLocationsFor(location);
      if (exact || IteratorHelper.EnumerableIsNotEmpty(primaries))
      {
        foundIndex = startingIndex;
        return primaries;
      }
      foundIndex = startingIndex - 1;
      while (0 <= foundIndex)
      {
        location = operations[foundIndex].Location;
        primaries = sourceLocationProvider.GetPrimarySourceLocationsFor(location);
        if (IteratorHelper.EnumerableIsNotEmpty(primaries))
          return primaries;
        foundIndex--;
      }
      return Enumerable<IPrimarySourceLocation>.Empty;
    }

    internal class MethodBodyCallback : IContractProviderCallback
    {
      readonly internal Dictionary<IMethodDefinition, IBlockStatement> extractedMethods = new Dictionary<IMethodDefinition, IBlockStatement>();

      #region IContractProviderCallback Members

      public void ProvideResidualMethodBody(IMethodDefinition methodDefinition, IBlockStatement blockStatement)
      {
        // doesn't matter if it was already added: this may get called more than once
        // since generic methods (or non-generic methods in generic classes) might
        // get their contracts extracted more than
        this.extractedMethods.Add(methodDefinition, blockStatement);
      }

      #endregion
    }


    /// <summary>
    /// </summary>
    /// <returns>Must return null if assembly cannot be loaded!</returns>
    public bool TryLoadAssembly(string fileName, System.Collections.IDictionary assemblyCache, Action<System.CodeDom.Compiler.CompilerError> errorHandler, out IAssemblyReference assembly, bool legacyContractMode, List<string> referencedAssemblies, bool extractContractText)
    {
      IUnit unit = host.PossiblyCompileFirstLoadUnitFrom(fileName, referencedAssemblies, this.RegisterSourceLocationProvider);
      if (unit is Dummy) { assembly = null; return false; }
      assembly = unit as IAssembly;
      if (assembly == null)
        return false;

      var ce = host.GetContractExtractor(unit.UnitIdentity);
      ce.RegisterContractProviderCallback(this.methodBodyCallback);

      // Maintain a map from assemblies to source location providers
      this.GetSourceLocationProvider(unit);

      return true;
    }

    /// <summary>
    /// Only provide top-level (not nested) types.
    /// </summary>
    public IEnumerable<TypeReferenceAdaptor> GetTypes(IAssemblyReference/*!*/ module)
    {
      return GetTypes(module.ResolvedAssembly);
    }

    public IEnumerable<TypeReferenceAdaptor> GetTypes(IAssembly/*!*/ module)
    {
      foreach (ITypeDefinition tdef in module.GetAllTypes())
      {
        ITypeReference tref = tdef as ITypeReference;
        if (tdef as INestedTypeReference != null) continue; // skip nested types here
        yield return TypeReferenceAdaptor.AdaptorOf(tref);
      }
      foreach (var mod in module.MemberModules)
      {
        foreach (ITypeDefinition tdef in mod.GetAllTypes())
        {
          ITypeReference tref = tdef as ITypeReference;
          if (tdef as INestedTypeReference != null) continue; // skip nested types here
          yield return TypeReferenceAdaptor.AdaptorOf(tref);
        }
      }
#if false
      // make sure to find each module only once
      Set<string> modules = new Set<string>();
      IWorkList<IModuleReference> references = new WorkStack<IModuleReference>();

      module.ModuleReferences.All(mr => { references.Add(mr); return true; });

      while (!references.IsEmpty())
      {
        var modref = references.Pull();
        if (modules.Contains(modref.Name.Value)) continue;

        foreach (var tdef in modref.ResolvedModule.GetAllTypes())
        {
          if (tdef as INestedTypeReference != null) continue; // skip nested types here
          yield return TypeReferenceAdaptor.AdaptorOf(tdef as ITypeReference);
        }

        foreach (var nestedmodref in modref.ResolvedModule.ModuleReferences)
        {
          references.Add(nestedmodref);
        }
      }
#endif
    }

    public IEnumerable<IAssemblyReference> AssemblyReferences(IAssemblyReference module)
    {
      return module.ResolvedAssembly.AssemblyReferences;
    }

    public void AssemblyAllReferences(IAssemblyReference module, ref Set<string> alls)
    {
      Contract.Requires(module != null);
      Contract.Requires(alls != null);
      Contract.Ensures(alls != null);

      //for (int i = 0; i < module.ResolvedAssembly.AssemblyReferences.Count(); i++)
      foreach (var mod in module.ResolvedAssembly.AssemblyReferences)
      {
        if (!alls.Contains(Name(mod)))
        {
          // new one, add it and its descendants
          alls.Add(Name(mod));
          AssemblyAllReferences(mod, ref alls);
        }
      }
    }

    #endregion

    #region Methods

    public bool TryGetImplementingMethod(TypeReferenceAdaptor typeAdaptor, MethodReferenceAdaptor baseMethod,
        out MethodReferenceAdaptor implementing)
    {
      implementing = baseMethod;
      var typeReference = typeAdaptor.reference;
      var methodReference = baseMethod.reference;
      if (methodReference.ContainingType.InternedKey == typeReference.InternedKey)
      {
        // we're done: baseMethod is already the method we're looking for
        return true;
      }
      var typeDef = typeReference.ResolvedType;
      var implementingMethod = TypeHelper.GetMethod(typeDef, methodReference);
      if (!(implementingMethod is Dummy))
      {
        if (typeDef.IsGeneric && !(typeDef is Dummy))
        {
          //var instant = typeDef.InstanceType;
          //var method = new Microsoft.Cci.MutableCodeModel.SpecializedMethodReference()
          //{
          //  CallingConvention = implementingMethod.CallingConvention,
          //  ContainingType = instant,
          //  InternFactory = this.host.InternFactory,
          //  Name = implementingMethod.Name,
          //  Parameters = new List<IParameterTypeInformation>( 
          //   IteratorHelper.GetConversionEnumerable<IParameterDefinition, IParameterTypeInformation>(
          //    implementingMethod.Parameters)),
          //  Type = implementingMethod.Type,
          //  UnspecializedVersion = implementingMethod,
          //};
          //implementing = MethodReferenceAdaptor.AdaptorOf(method);
          //return true;

          var instant = Immutable.GenericTypeInstance.GetGenericTypeInstance((INamedTypeDefinition)typeDef,
            IteratorHelper.GetConversionEnumerable<IGenericTypeParameter, ITypeReference>(typeDef.GenericParameters),
            this.host.InternFactory);
          var method = instant.SpecializeMember(implementingMethod, this.host.InternFactory);
          implementingMethod = (IMethodDefinition)method;
        }
        implementing = MethodReferenceAdaptor.AdaptorOf(implementingMethod);
        return true;
      }
      foreach (var methodImpl in typeReference.ResolvedType.ExplicitImplementationOverrides)
      {
        if (methodImpl.ImplementedMethod.InternedKey == methodReference.InternedKey)
        {
          implementing = MethodReferenceAdaptor.AdaptorOf(methodImpl.ImplementingMethod);
          return true;
        }
      }
      return false;
    }

    public TypeReferenceAdaptor ReturnType(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      return TypeReferenceAdaptor.AdaptorOf(method.Type);
    }

    public IIndexable<IParameterTypeInformation> Parameters(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      return new IndexableCollection<IParameterTypeInformation>(method.Parameters);
    }

    public IParameterTypeInformation This(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      if ((method.CallingConvention & CallingConvention.ExplicitThis) != 0)
      {
        foreach (IParameterTypeInformation parameter in method.Parameters)
          return parameter;
      }
      IParameterTypeInformation/*?*/ thisPar = null;
      if (!this.thisParameterFor.TryGetValue(methodAdaptor, out thisPar))
      {
        Microsoft.Cci.MutableCodeModel.ParameterDefinition par = new Microsoft.Cci.MutableCodeModel.ParameterDefinition();
        par.Name = this.host.NameTable.GetNameFor("this");
        par.ContainingSignature = method;
        par.Index = ushort.MaxValue; // used to communicate with ArgumentIndex and ArgumentStackIndex
        if (method.ContainingType.IsValueType)
        {
          par.Type = Microsoft.Cci.Immutable.ManagedPointerType.GetManagedPointerType(method.ContainingType, this.internFactory);
        }
        else
        {
          par.Type = method.ContainingType;
        }
        //par.IsByReference = method.ContainingType.IsValueType;
        this.thisParameterFor[methodAdaptor] = par;
        thisPar = par;
      }
      return thisPar;
    }

    public string Name(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      return method.Name.Value;
    }

    public string FullName(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      var s = MemberHelper.GetMethodSignature(method, NameFormattingOptions.Signature & ~NameFormattingOptions.ReturnType | NameFormattingOptions.UseGenericTypeNameSuffix);
      return s;
    }

    public string DocumentationId(MethodReferenceAdaptor methodAdaptor)
    {
      return MemberHelper.GetMethodSignature(methodAdaptor.reference, NameFormattingOptions.DocumentationId);
    }
    public string DocumentationId(TypeReferenceAdaptor referenceAdaptor)
    {
      return TypeHelper.GetTypeName(referenceAdaptor.reference, NameFormattingOptions.DocumentationId);
    }

    public string DocumentationId(FieldReferenceAdaptor fieldAdaptor)
    {
      return TypeHelper.GetTypeName(fieldAdaptor.reference.ContainingType, NameFormattingOptions.DocumentationId) + "." + fieldAdaptor.reference.Name.Value;
    }

    public string DeclaringMemberCanonicalName(MethodReferenceAdaptor method)
    {
      ITypeMemberReference declaringMember;

      IEventDefinition ev;
      if (IsPropertySetter(method) || IsPropertyGetter(method))
      {
        var prop = GetPropertyFromAccessor(method);
        declaringMember = prop;
      }
      else if (IsEventAdder(method, out ev) || IsEventRemover(method, out ev) || IsEventCaller(method, out ev))
        declaringMember = ev;
      else
      {
        declaringMember = method.reference;
        Contract.Assert(declaringMember != null);
      }

      Contract.Assume(declaringMember != null);

      return MemberHelper.GetMemberSignature(declaringMember,
        NameFormattingOptions.PreserveSpecialNames | NameFormattingOptions.UseGenericTypeNameSuffix | NameFormattingOptions.UseReflectionStyleForNestedTypeNames);
    }

    public bool IsMain(MethodReferenceAdaptor methodAdaptor)
    {
      var method = methodAdaptor.reference;
      var assembly = AssemblyOf(method);
      var unit = assembly as IModule;

      return unit != null && unit.EntryPoint == method;
    }

    public bool IsStatic(MethodReferenceAdaptor methodAdaptor)
    {
      return IsStatic(methodAdaptor.reference);
    }

    internal bool IsStatic(IMethodReference methodReference)
    {
      Contract.Requires(methodReference != null);

      return (methodReference.CallingConvention & CallingConvention.HasThis) == 0;
    }

    public bool IsCompilerGenerated(MethodReferenceAdaptor methodAdaptor)
    {
      foreach (var attr in methodAdaptor.reference.ResolvedMethod.Attributes)
      {
        string name = this.Name(attr.Type);
        if (name == "CompilerGeneratedAttribute") return true;
        if (name == "GeneratedCodeAttribute") return true;
      }
      return false;
    }
    public bool IsDebuggerNonUserCode(MethodReferenceAdaptor methodAdaptor)
    {
      foreach (var attr in methodAdaptor.reference.ResolvedMethod.Attributes)
      {
        string name = this.Name(attr.Type);
        if (name == "DebuggerNonUserCodeAttribute") return true;
      }
      return false;
    }
    public bool IsAutoPropertyMember(MethodReferenceAdaptor m)
    {
      bool isPropertyGetter = m.reference.Name.Value.StartsWith("get_");
      bool isPropertySetter = m.reference.Name.Value.StartsWith("set_");
      if (!isPropertyGetter && !isPropertySetter) return false;
      return IsCompilerGenerated(m) || IsDebuggerNonUserCode(m);
    }

    public bool IsAutoPropertySetter(MethodReferenceAdaptor setter, out FieldReferenceAdaptor backingField)
    {
      if (!IsAutoPropertyMember(setter)) { backingField = default(FieldReferenceAdaptor); return false; }

      // get backing field by searching setter's body for first occurrence of a field
      var fr = FieldFinder.FindFirstField(setter.reference.ResolvedMethod);
      backingField = fr != null ? FieldReferenceAdaptor.AdaptorOf(fr) : default(FieldReferenceAdaptor);
      return fr != null;
    }
    private class FieldFinder : CodeTraverser
    {
      private IFieldReference/*?*/ foundField;
      private FieldFinder() { }
      public static IFieldReference/*?*/ FindFirstField(IMethodDefinition methodDefinition)
      {
        Contract.Requires(methodDefinition != null);
        Contract.Requires(!methodDefinition.IsAbstract);
        Contract.Requires(!methodDefinition.IsExternal);

        var body = methodDefinition.Body;
        var fff = new FieldFinder();
        fff.Traverse(body);
        return fff.foundField;
      }
      public override void TraverseChildren(IFieldReference fieldReference)
      {
        this.foundField = fieldReference;
        return;
      }
    }

    public TypeReferenceAdaptor DeclaringType(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      var t = method.ContainingType;
      return TypeReferenceAdaptor.AdaptorOf(t);
    }

    public IAssemblyReference DeclaringAssembly(MethodReferenceAdaptor methodAdaptor)
    {
      var assembly = DeclaringModule(methodAdaptor.reference.ContainingType) as IAssemblyReference;
      if (assembly != null)
        return assembly;
      var result = this.AssemblyOf(methodAdaptor.reference) as IAssemblyReference;

      Contract.Assume(result != null);

      return result;
    }

    public int MethodToken(MethodReferenceAdaptor methodAdaptor)
    {
      //TODO: get from metadata interface (to be defined)
      IMethodReference method = methodAdaptor.reference;
      return method.GetHashCode();
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public bool IsConstructor(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      IMethodDefinition methodDef = method.ResolvedMethod;
      return methodDef != null && methodDef.IsConstructor;
    }

    public bool IsFinalizer(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      return (method.Name.Value == "Finalize");
    }

    /// <summary>
    /// Dispose methods are 0-ary or take 1 boolean
    /// </summary>
    public bool IsDispose(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      if (method.Name.Value != "Dispose" && method.Name.Value != "System.IDisposable.Dispose") return false;
      if (method.ParameterCount == 0) return true;
      if (method.ParameterCount > 1) return false;
      var type = method.Parameters.AsIndexable(1)[0].Type;
      if (type.InternedKey == System_Boolean.InternedKey) return true;
      return false;
    }

    public bool IsInternal(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      return method.ResolvedMethod.Visibility == TypeMemberVisibility.Assembly
        || method.ResolvedMethod.Visibility == TypeMemberVisibility.FamilyAndAssembly
        || method.ResolvedMethod.Visibility == TypeMemberVisibility.FamilyOrAssembly;
    }

    public bool IsPrivate(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      return method.ResolvedMethod.Visibility == TypeMemberVisibility.Private;
    }

    public bool IsVisibleOutsideAssembly(TypeReferenceAdaptor typeRef)
    {
      bool parentIsSealed;
      return IsVisibleOutsideAssembly(typeRef.reference, out parentIsSealed);
    }

    public bool IsVisibleOutsideAssembly(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      IMethodDefinition md = method.ResolvedMethod;
      TypeMemberVisibility mvis = md.Visibility;
      if (mvis == TypeMemberVisibility.Private || mvis == TypeMemberVisibility.Assembly || mvis == TypeMemberVisibility.FamilyAndAssembly)
      {
        return false;
      }
      ITypeReference declaringType = method.ContainingType;
      bool parentIsSealed;
      if (IsVisibleOutsideAssembly(Unspecialized(declaringType), out parentIsSealed))
      {
        if (parentIsSealed && mvis == TypeMemberVisibility.Family)
        {
          return false;
        }
        return true;
      }
      return false;
    }

    public bool IsVisibleOutsideAssembly(IPropertyDefinition property)
    {
      TypeMemberVisibility vis = property.Visibility;
      if (vis == TypeMemberVisibility.Private || vis == TypeMemberVisibility.Assembly || vis == TypeMemberVisibility.FamilyAndAssembly)
      {
        return false;
      }
      ITypeReference declaringType = property.ContainingType;
      bool parentIsSealed;
      if (IsVisibleOutsideAssembly(declaringType, out parentIsSealed))
      {
        if (parentIsSealed && vis == TypeMemberVisibility.Family)
        {
          return false;
        }
        return true;
      }
      return false;
    }

    public bool IsVisibleOutsideAssembly(IEventDefinition @event)
    {
      TypeMemberVisibility vis = @event.Visibility;
      if (vis == TypeMemberVisibility.Private || vis == TypeMemberVisibility.Assembly || vis == TypeMemberVisibility.FamilyAndAssembly)
      {
        return false;
      }
      ITypeReference declaringType = @event.ContainingType;
      bool parentIsSealed;
      if (IsVisibleOutsideAssembly(declaringType, out parentIsSealed))
      {
        if (parentIsSealed && vis == TypeMemberVisibility.Family)
        {
          return false;
        }
        return true;
      }
      return false;
    }


    public bool IsVisibleOutsideAssembly(FieldReferenceAdaptor field)
    {
      IFieldDefinition fd = field.reference.ResolvedField;
      TypeMemberVisibility vis = fd.Visibility;
      if (vis == TypeMemberVisibility.Private || vis == TypeMemberVisibility.Assembly || vis == TypeMemberVisibility.FamilyAndAssembly)
      {
        return false;
      }
      ITypeReference declaringType = field.reference.ContainingType;
      bool parentIsSealed;
      if (IsVisibleOutsideAssembly(declaringType, out parentIsSealed))
      {
        if (parentIsSealed && vis == TypeMemberVisibility.Family)
        {
          return false;
        }
        return true;
      }
      return false;
    }

    // A type is visible outside, if it isn't internal/private and if it's parent is visible outside
    public bool IsVisibleOutsideAssembly(ITypeReference tr, out bool isSealed)
    {
      INamespaceTypeReference nstr = tr as INamespaceTypeReference;
      if (nstr != null)
      {
        INamespaceTypeDefinition nstd = nstr.ResolvedType;
        isSealed = nstd.IsSealed;
        return nstd.IsPublic;
      }
      INestedTypeReference nest = tr as INestedTypeReference;
      if (nest != null)
      {
        INestedTypeDefinition nstd = nest.ResolvedType;
        isSealed = nstd.IsSealed;
        TypeMemberVisibility vis = nstd.Visibility;
        if (vis == TypeMemberVisibility.Private || vis == TypeMemberVisibility.Assembly || vis == TypeMemberVisibility.FamilyAndAssembly) return false;
        ITypeReference declaringType = nstd.ContainingType;
        bool isParentSealed;
        if (IsVisibleOutsideAssembly(declaringType, out isParentSealed))
        {
          if (isParentSealed && vis == TypeMemberVisibility.Family) return false;
          return true;
        }
        return false;
      }
      throw new ArgumentException("arg is not a nominal type");
    }

    public bool IsAbstract(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      IMethodDefinition md = method.ResolvedMethod;
      return md.IsAbstract;
    }

    public bool IsExtern(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      IMethodDefinition md = method.ResolvedMethod;
      return md.IsExternal;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public bool IsVirtual(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      IMethodDefinition methodDef = method.ResolvedMethod;
      return methodDef != null && methodDef.IsVirtual;
    }

    public bool IsNewSlot(MethodReferenceAdaptor methodAdaptor)
    {
      return methodAdaptor.reference.ResolvedMethod.IsNewSlot;
    }

    public bool IsOverride(MethodReferenceAdaptor methodAdaptor)
    {
      if (IsNewSlot(methodAdaptor))
        return false;
      var baseMethod = MemberHelper.GetImplicitlyOverriddenBaseClassMethod(methodAdaptor.reference.ResolvedMethod);
      return (!(baseMethod is Dummy));
    }

    public bool IsSealed(MethodReferenceAdaptor methodAdaptor)
    {
      return methodAdaptor.reference.ResolvedMethod.IsSealed;
    }

    public IEnumerable<MethodReferenceAdaptor> OverriddenMethods(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference methodReference = methodAdaptor.reference;
      IMethodDefinition methodDefinition = methodReference.ResolvedMethod;
      #region Closest overridden method
      var baseMethod = MemberHelper.GetImplicitlyOverriddenBaseClassMethod(methodDefinition);
      if (!(baseMethod is Dummy))
        yield return MethodReferenceAdaptor.AdaptorOf(baseMethod);
      #endregion Closest overridden method
    }

    public bool TryGetRootMethod(MethodReferenceAdaptor method, out MethodReferenceAdaptor root)
    {
      var rootMethod = RootMethod(method.reference);
      if (rootMethod == null) { root = default(MethodReferenceAdaptor); return false; }
      root = MethodReferenceAdaptor.AdaptorOf(rootMethod);
      return true;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public IMethodReference RootMethod(IMethodReference method)
    {
      Contract.Requires(method != null);

      var originalMethod = method.ResolvedMethod;
      var current = originalMethod;

      var baseMethod = MemberHelper.GetImplicitlyOverriddenBaseClassMethod(current);

      while (baseMethod != null && (!(baseMethod is Dummy)) && CciContractDecoder.CanInheritContractsInternal(current))
      {
        current = baseMethod;
        baseMethod = MemberHelper.GetImplicitlyOverriddenBaseClassMethod(current);
      }
      if (current == originalMethod) { return null; } // no root base method
      return current;
    }

    /// <summary>
    /// Returns the (abstract, interface) methods that <paramref name="methodAdaptor"/> implements,
    /// either explicitly or implicitly.
    /// </summary>
    public IEnumerable<MethodReferenceAdaptor> ImplementedMethods(MethodReferenceAdaptor methodAdaptor) {
      IMethodReference methodReference = methodAdaptor.reference;
      IMethodDefinition methodDefinition = methodReference.ResolvedMethod;
      #region Explicit implementations of interface methods
      foreach (var ifaceMethod in MemberHelper.GetExplicitlyOverriddenMethods(methodDefinition))
        yield return MethodReferenceAdaptor.AdaptorOf(ifaceMethod);
      #endregion Explicit implementations of interface methods
      #region Implicit implementations of interface methods
      foreach (var ifaceMethod in MemberHelper.GetImplicitlyImplementedInterfaceMethods(methodDefinition))
        yield return MethodReferenceAdaptor.AdaptorOf(ifaceMethod);
      //foreach (var ifaceMethod in ContractHelper.GetAllImplicitlyImplementedInterfaceMethods(methodDefinition)) {
      //  yield return MethodReferenceAdaptor.AdaptorOf(ifaceMethod);
      //}
      #endregion Implicit implementations of interface methods
      var cd = this.parent.ContractDecoder;
      if (cd.IsModel(methodAdaptor)) {
        // then can't ask via the CCI methods because types may not know that there is a model
        // method that claims to belong to them.
        // Code from: MemberHelper.GetImplicitlyImplementedInterfaceMethods
        var method = methodAdaptor.reference.ResolvedMethod;
        foreach (var interfaceReference in method.ContainingTypeDefinition.Interfaces) {
          foreach (var modelMethod in cd.ModelMethods(TypeReferenceAdaptor.AdaptorOf(interfaceReference))) {
            if (MemberHelper.MethodsAreEquivalent(method, modelMethod.reference.ResolvedMethod))
              yield return modelMethod;
          }

        }
      }
    }

    public IEnumerable<MethodReferenceAdaptor> OverriddenAndImplementedMethods(MethodReferenceAdaptor methodAdaptor)
    {
      // Simply merge the two IEnumerable
      var enum1 = OverriddenMethods(methodAdaptor);
      var enum2 = ImplementedMethods(methodAdaptor);
      foreach (var e1 in enum1)
        yield return e1;
      foreach (var e2 in enum2)
        yield return e2;
    }

    public bool IsImplicitImplementation(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodDefinition methodDefinition = methodAdaptor.reference.ResolvedMethod;
      return MemberHelper.GetImplicitlyImplementedInterfaceMethods(methodDefinition).Count() != 0;
    }

    public bool IsVoidMethod(MethodReferenceAdaptor method)
    {
      return method.reference.Type.TypeCode == PrimitiveTypeCode.Void;
    }

    public bool HasBody(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      IMethodDefinition methodDef = method.ResolvedMethod;
      if (methodDef is ISpecializedMethodDefinition) return false;
      return !methodDef.IsAbstract && !methodDef.IsExternal;
    }

    public Result AccessMethodBody<Data, Result>(MethodReferenceAdaptor method, IMethodCodeConsumer<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, Data, Result> consumer, Data data)
    {
      var lambdaMethod = method.reference as LambdaMethodReference;
      if (lambdaMethod != null)
      {
        var contractPC = new CciContractPC(lambdaMethod.anonymousDelegate.Body, 0);
        return consumer.Accept<CciContractPC, CciExceptionHandlerInfo>(this.parent.ContractProvider, contractPC, method, data);
      }
      else
      {
        return consumer.Accept<CciILPC, CciExceptionHandlerInfo>(this.parent, this.parent.Entry(method), method, data);
      }
    }

    public bool IsProtected(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      return method.ResolvedMethod.Visibility == TypeMemberVisibility.Family
        || method.ResolvedMethod.Visibility == TypeMemberVisibility.FamilyAndAssembly
        || method.ResolvedMethod.Visibility == TypeMemberVisibility.FamilyOrAssembly;
    }

    public bool IsPublic(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      return method.ResolvedMethod.Visibility == TypeMemberVisibility.Public;
    }

    public bool IsAsyncMoveNext(MethodReferenceAdaptor method) { return false; }

    public int? MoveNextStartState(MethodReferenceAdaptor method) { return null; }



    public bool IsSpecialized(MethodReferenceAdaptor methodAdaptor)
    {
      IMethodReference method = methodAdaptor.reference;
      return IsSpecialized(method);
    }
    public bool IsSpecialized(IMethodReference method)
    {
      if (method is ISpecializedMethodReference) return true;
      if (method is IGenericMethodInstanceReference) return true;
      return false;
    }

    public bool IsSpecialized(MethodReferenceAdaptor methodAdaptor, ref IFunctionalMap<TypeReferenceAdaptor, TypeReferenceAdaptor> specialization)
    {
      Contract.Assume(specialization != null);

      var isSpecialized = false;
      var method = methodAdaptor.reference;
      var gmir = method as IGenericMethodInstanceReference;
      if (gmir != null)
      {
        isSpecialized = true;
        var actuals = new List<ITypeReference>(gmir.GenericArguments);
        var formals = new List<ITypeReference>(gmir.GenericMethod.ResolvedMethod.GenericParameters);

        for (int i = 0; i < formals.Count; i++)
        {
          var formal = formals[i];
          var actual = actuals[i];
          if (formal != null && actual != null)
          {
            specialization = specialization.Add(TypeReferenceAdaptor.AdaptorOf(formal), TypeReferenceAdaptor.AdaptorOf(actual));
          }
        }
      }
      var smr = method as ISpecializedMethodReference;
      if (smr != null)
      {
        isSpecialized = true;
        this.ComputeTypeInstantiationMap(smr.ContainingType, ref specialization);
      }
      return isSpecialized;
    }
    private void ComputeTypeInstantiationMap(ITypeReference typeReference, ref IFunctionalMap<TypeReferenceAdaptor, TypeReferenceAdaptor> specialization)
    {
      Contract.Requires(specialization != null);

      var genericTypeInstanceReference = typeReference as IGenericTypeInstanceReference;
      if (genericTypeInstanceReference != null)
      {
        var template = genericTypeInstanceReference.GenericType;
        var specializedNestedType = template as ISpecializedNestedTypeReference;
        if (specializedNestedType != null) template = specializedNestedType.UnspecializedVersion;
        var consolidatedParameters = new List<ITypeReference>();
        this.GetConsolidatedTypeParameters(consolidatedParameters, template.ResolvedType);
        var consolidatedArguments = new List<ITypeReference>();
        this.GetConsolidatedTypeArguments(consolidatedArguments, genericTypeInstanceReference);

        for (int i = 0; i < consolidatedParameters.Count; i++)
        {
          var formal = consolidatedParameters[i];
          var actual = consolidatedArguments[i];
          if (formal != null && actual != null)
          {
            specialization = specialization.Add(TypeReferenceAdaptor.AdaptorOf(formal), TypeReferenceAdaptor.AdaptorOf(actual));
          }
        }
      }
    }
    private void GetConsolidatedTypeParameters(List<ITypeReference> consolidatedTypeParameters, ITypeDefinition typeDef)
    {
      Contract.Requires(typeDef != null);
      INestedTypeDefinition/*?*/ nestedTypeDef = typeDef as INestedTypeDefinition;
      if (nestedTypeDef != null)
        GetConsolidatedTypeParameters(consolidatedTypeParameters, nestedTypeDef.ContainingTypeDefinition);
      if (typeDef.IsGeneric) consolidatedTypeParameters.AddRange(typeDef.GenericParameters);
      return;
    }

    private void GetConsolidatedTypeArguments(List<ITypeReference> consolidatedTypeArguments, ITypeReference typeReference)
    {
      var genTypeInstance = typeReference as IGenericTypeInstanceReference;
      if (genTypeInstance != null)
      {
        GetConsolidatedTypeArguments(consolidatedTypeArguments, genTypeInstance.GenericType);
        foreach (var genArg in genTypeInstance.GenericArguments)
          consolidatedTypeArguments.Add(genArg);
        return;
      }
      var nestedTypeReference = typeReference as INestedTypeReference;
      if (nestedTypeReference != null) GetConsolidatedTypeArguments(consolidatedTypeArguments, nestedTypeReference.ContainingType);
    }

    public bool IsSpecialized(MethodReferenceAdaptor methodAdaptor, out MethodReferenceAdaptor genericMethod, out IIndexable<TypeReferenceAdaptor> methodTypeArguments)
    {
      var method = methodAdaptor.reference;
      var genericMethodInstance = method as IGenericMethodInstanceReference;
      if (genericMethodInstance != null)
      {
        methodTypeArguments = genericMethodInstance.GenericArguments.AsIndexable<ITypeReference, TypeReferenceAdaptor>(TypeReferenceAdaptor.AdaptorOf);
        genericMethod = MethodReferenceAdaptor.AdaptorOf(genericMethodInstance.GenericMethod);
        return true;
      }
      genericMethod = default(MethodReferenceAdaptor);
      methodTypeArguments = null;
      return false;
    }

    public bool IsGeneric(MethodReferenceAdaptor methodAdaptor, out IIndexable<TypeReferenceAdaptor> formalTypeParameters)
    {
      IMethodDefinition md = methodAdaptor.reference.ResolvedMethod;
      if (md.GenericParameterCount > 0)
      {
        formalTypeParameters = md.GenericParameters.AsIndexable(gmp => TypeReferenceAdaptor.AdaptorOf(gmp));
        return true;
      }
      formalTypeParameters = null;
      return false;
    }

    public MethodReferenceAdaptor Unspecialized(MethodReferenceAdaptor methodAdaptor)
    {
      return MethodReferenceAdaptor.AdaptorOf(Unspecialized(methodAdaptor.reference));
    }

    public static IMethodDefinition Unspecialized(IMethodDefinition method)
    {
      Contract.Requires(method != null);

      return Unspecialized((IMethodReference)method) as IMethodDefinition;
    }

    public static IMethodReference Unspecialized(IMethodReference method)
    {
      Contract.Requires(method != null);

      return MemberHelper.UninstantiateAndUnspecialize(method);
    }

    public MethodReferenceAdaptor Specialize(MethodReferenceAdaptor methodAdaptor, TypeReferenceAdaptor[] methodTypeArguments)
    {
      throw new NotImplementedException(); // TODO
    }

    public bool Equal(MethodReferenceAdaptor m1, MethodReferenceAdaptor m2)
    {
      return m1.Equals(m2);
    }

    public IEnumerable<ICustomAttribute> GetAttributes(MethodReferenceAdaptor methodAdaptor)
    {
      return methodAdaptor.reference.ResolvedMethod.Attributes;
    }

    public bool IsAsVisibleAs(MethodReferenceAdaptor m1, MethodReferenceAdaptor m2)
    {
      return AsVisibleAs(m1.reference, m2.reference);
    }

    private bool IsNestedClassOf(TypeReferenceAdaptor t, TypeReferenceAdaptor tfrom)
    {
      TypeReferenceAdaptor parentt;
      if (IsNested(t, out parentt))
      {
        if (parentt.Equals(tfrom) || IsNestedClassOf(parentt, tfrom))
          return true;
      }
      return false;
    }

    private bool IsInheritedFrom(TypeReferenceAdaptor t, TypeReferenceAdaptor tfrom)
    {
      if (HasBaseClass(t))
      {
        TypeReferenceAdaptor basecl = BaseClass(t);
        if (basecl.Equals(tfrom))
          return true;
        else if (IsInheritedFrom(basecl, tfrom))
          return true;
      }
      return false;
    }

    static ITypeReference DeclaringType(ITypeReference type)
    {
      var nested = type as INestedTypeReference;
      if (nested != null) return nested.ContainingType;
      return null;
    }

    public static bool IsContainedIn(ITypeReference inner, ITypeReference outer)
    {
      if (TypeHelper.TypesAreEquivalent(inner, outer)) return true;
      var innerDeclaringType = DeclaringType(inner);
      if (innerDeclaringType != null) return IsContainedIn(innerDeclaringType, outer);
      return false;
    }

    private static bool IsInheritedFrom(ITypeReference t, ITypeReference tfrom)
    {
      Contract.Requires(t != null);

      if (HasBaseClass(t))
      {
        var basecl = BaseClass(t);
        basecl = Unspecialized(basecl);
        if (TypeHelper.TypesAreEquivalent(basecl, tfrom))
          return true;
        else if (IsInheritedFrom(basecl, tfrom))
          return true;
      }
      return false;
    }

    static bool IsNestedFamily(ITypeDefinitionMember t)
    {
      var nested = t as INestedTypeDefinition;
      if (nested == null) return false;
      return (nested.Visibility == TypeMemberVisibility.Family);
    }
    static bool IsNestedPublic(ITypeDefinitionMember t)
    {
      var nested = t as INestedTypeDefinition;
      if (nested == null) return false;
      return (nested.Visibility == TypeMemberVisibility.Public);
    }
    static bool IsNestedAssembly(ITypeDefinitionMember t)
    {
      var nested = t as INestedTypeDefinition;
      if (nested == null) return false;
      return (nested.Visibility == TypeMemberVisibility.Assembly);
    }
    static bool IsNestedFamilyAndAssembly(ITypeDefinitionMember t)
    {
      var nested = t as INestedTypeDefinition;
      if (nested == null) return false;
      return (nested.Visibility == TypeMemberVisibility.FamilyAndAssembly);
    }
    static bool IsNestedFamilyOrAssembly(ITypeDefinitionMember t)
    {
      var nested = t as INestedTypeDefinition;
      if (nested == null) return false;
      return (nested.Visibility == TypeMemberVisibility.FamilyOrAssembly);
    }
    static bool IsAssembly(ITypeDefinition t)
    {
      Contract.Requires(t != null);
      return !(TypeHelper.IsVisibleOutsideAssembly(t));
    }
    static bool IsPublic(ITypeDefinition t)
    {
      Contract.Requires(t != null);
      return (TypeHelper.IsVisibleOutsideAssembly(t));
    }
    static bool IsPublic(ITypeDefinitionMember member)
    {
      Contract.Requires(member != null);

      return member.Visibility == TypeMemberVisibility.Public;
    }

    static IUnitReference DeclaringModule(ITypeReference t)
    {
      Contract.Requires(t != null);
      return TypeHelper.GetDefiningUnitReference(t);
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public IIndexable<LocalDefAdaptor> Locals(MethodReferenceAdaptor mr)
    {
      IMethodDefinition d = mr.reference.ResolvedMethod;
      if (d.IsAbstract || d.IsExternal) // precondition of Body
        return EmptyIndexable<LocalDefAdaptor>.Empty;
      IMethodBody b = d.Body;
      if (b == null)
      {
        return EmptyIndexable<LocalDefAdaptor>.Empty;
      }
      else
      {
        return b.LocalVariables.AsIndexable<ILocalDefinition, LocalDefAdaptor>(l => LocalDefAdaptor.AdaptorOf(l));
      }
    }

    #endregion

    #region Attributes

    public IEnumerable<ICustomAttribute> GetAttributes(IPropertyDefinition property)
    {
      return property.Attributes;
    }

    public IEnumerable<ICustomAttribute> GetAttributes(IEventDefinition @event)
    {
      return @event.Attributes;
    }

    public IEnumerable<ICustomAttribute> GetAttributes(TypeReferenceAdaptor type)
    {
      return type.ResolvedType.Attributes;
    }

    public TypeReferenceAdaptor AttributeType(ICustomAttribute attribute)
    {
      return TypeReferenceAdaptor.AdaptorOf(attribute.Type);
    }

    public MethodReferenceAdaptor AttributeConstructor(ICustomAttribute attribute)
    {
      return MethodReferenceAdaptor.AdaptorOf(attribute.Constructor);
    }

    public IIndexable<object> PositionalArguments(ICustomAttribute attribute)
    {
      int count = 0;
      foreach (IMetadataExpression expression in attribute.Arguments)
      {
        count++;
      }
      object[] args = new object[count];
      int i = 0;
      foreach (IMetadataExpression expression in attribute.Arguments)
      {
        Contract.Assume(i < args.Length, "follows from the loop above");
        args[i++] = TranslateToObject(expression);
      }
      return new ArrayIndexable<object>(args);
    }

    public IEnumerable<ICustomAttribute> GetAttributes(IAssemblyReference assembly)
    {
      return assembly.ResolvedAssembly.Attributes;
    }

    internal static object TranslateToObject(IMetadataExpression expression)
    {
      IMetadataConstant c = expression as IMetadataConstant;
      if (c != null) return c.Value;
      IMetadataCreateArray ca = expression as IMetadataCreateArray;
      if (ca != null) throw new NotImplementedException("TranslateObject(IMetadataCreateArray)");
      IMetadataNamedArgument na = expression as IMetadataNamedArgument;
      if (na != null) throw new NotImplementedException("TranslateObject(IMetadataNamedArgument)"); // return TranslateObject(na.ArgumentValue); // but would lose the name binding
      IMetadataTypeOf to = expression as IMetadataTypeOf;
      if (to != null) return TypeReferenceAdaptor.AdaptorOf(to.TypeToGet);
      throw new NotImplementedException("TranslateObject");
    }


    public object NamedArgument(string name, ICustomAttribute attribute)
    {
      foreach (IMetadataNamedArgument namedArg in attribute.NamedArguments)
      {
        if (namedArg.ArgumentName.Value == name)
        {
          IMetadataConstant constExp = namedArg.ArgumentValue as IMetadataConstant;
          if (constExp != null) return constExp.Value;
        }
      }
      return null;
    }

    #endregion

    #region Platform

    public bool IsPlatformInitialized { get; private set; }

    public void SetTargetPlatform(string framework, System.Collections.IDictionary assemblyCache, string stdlib, IEnumerable<string> resolved, IEnumerable<string> libPaths, Action<System.CodeDom.Compiler.CompilerError> errorHandler, bool trace)
    {
      this.IsPlatformInitialized = true;

      foreach (var path in resolved) { this.AddResolvedPath(path); }
      foreach (var path in libPaths) { this.AddLibPath(path); }
    }

    public void AddLibPath(string path)
    {
      this.host.AddLibPath(path);
    }

    public void AddResolvedPath(string path)
    {
      this.host.AddResolvedPath(path);
    }

    public string SharedContractClassAssembly
    {
      //get { return this.host.SharedContractClassAssembly; }
      //set { this.host.SharedContractClassAssembly = value; }
      get;
      set;
    }
    #endregion

    #region Helpers
    /// <summary>
    /// Algorithm as provided by Eric Lippert
    /// Returns true if any context that can see m2 can also see m1.
    /// </summary>
    //foreach (Symbol s1 in T.TypeAndParentTypes where s1.access != public)
    //            asRestrictive = false
    //            foreach(Symbol s2 in S.SymbolAndParentSymbols)
    //                        if s1.access == internal
    //                           if s2.access == private or s2.access == internal
    //                                if s1 and s2 are in the same assembly or are in friend assemblies
    //                                   asRestrictive = true
    //                        if s1.access == protected
    //                                    if s2.access == private
    //                                                if s2 is inside s1 or s2 is within a subclass of s1.parent
    //                                                            asRestrictive = true
    //                                    if s2.access == protected
    //                                                if s2.parent is identical to s1.parent or s2.parent is a subclass of s1.parent
    //                                                            asRestrictive = true
    //                        if s1.access == internalprotected
    //                                    if (s2.access == private)
    //                                                if s2 is within a subclass of s1.parent, or s1 and s2 are in the same assembly, or s1 and s2 are in friend assemblies
    //                                                            asRestrictive = true
    //                                    if s2.access == internal
    //                                                if s1 and s2 are in the same assembly or are in friend assemblies 
    //                                                            asRestrictive = true
    //                                    if s2.access == protected
    //                                                if s2.parent is s1.parent or a subclass of s1.parent
    //                                                            asRestrictive = true
    //                                    if s2.access == internalprotected
    //                                                if s1 and s2 are in the same assembly or friend assemblies, AND s2.parent is s1.parent or s2.parent is a subclass of s1.parent
    //                                                            asRestrictive = true
    //                        if s1.access == private
    //                                    if s2.access == private
    //                                                if s2 is inside s1.parent
    //                                                            asRestrictive = true
    //            if !asRestrictive
    //                        return false
    //return true
    //
    // NOTE: we are assuming that originalM2 is not instantiated, but originalM1 could be
    bool AsVisibleAs(ITypeMemberReference originalM1, ITypeMemberReference originalM2)
    {
      IMethodReference mr = originalM1 as IMethodReference;
      if (mr != null)
      {
        IGenericMethodInstanceReference gmir = mr as IGenericMethodInstanceReference;
        if (gmir != null)
        {
          mr = gmir.GenericMethod;

          Contract.Assert(mr != null);

          // check type actuals visibility
          foreach (ITypeReference arg in gmir.GenericArguments)
          {
            if (!AsVisibleAs(arg, originalM2)) return false;
          }
        }
        ITypeReference declaringType = mr.ContainingType;
        ISpecializedMethodReference smr = mr as ISpecializedMethodReference;
        if (smr != null)
        {
          mr = smr.UnspecializedVersion;
          Contract.Assert(mr != null);
        }
        if (!AsVisibleAsUnspecialized(mr, originalM2)) return false;
        return AsVisibleAs(declaringType, originalM2);
      }
      IFieldReference fr = originalM1 as IFieldReference;
      if (fr != null)
      {
        ITypeReference declaringType = fr.ContainingType;
        ISpecializedFieldReference sfr = fr as ISpecializedFieldReference;
        if (sfr != null)
        {
          fr = sfr.UnspecializedVersion;
          Contract.Assert(fr != null);
        }
        if (!AsVisibleAsUnspecialized(fr, originalM2)) return false;
        return AsVisibleAs(declaringType, originalM2);
      }
      throw new NotImplementedException();
    }

    /// <summary>
    /// s1 may be specialized. In that case, check all type arg visiblities.
    /// M2 is always unspecialized.
    /// </summary>
    bool AsVisibleAs(ITypeReference s1, ITypeMemberReference m2)
    {
      IGenericTypeInstanceReference gtir = s1 as IGenericTypeInstanceReference;
      if (gtir != null)
      {
        foreach (ITypeReference arg in gtir.GenericArguments)
        {
          if (!AsVisibleAs(arg, m2)) return false;
        }
        // check visibility of uninstantiated type and its possibly specialized parents
        return AsVisibleAs(gtir.GenericType, m2);
      }
      // deals with nested types
      ITypeMemberReference tmr = s1 as ITypeMemberReference;
      if (tmr != null)
      {
        if (!AsVisibleAsUnspecialized(tmr, m2)) return false;
        return AsVisibleAs(tmr.ContainingType, m2);
      }
      INamespaceTypeReference nstr = s1 as INamespaceTypeReference;
      if (nstr != null) return AsVisibleAsUnspecialized(nstr, m2);
      if (s1 as IGenericTypeParameterReference != null) return true;
      IArrayTypeReference atr = s1 as IArrayTypeReference;
      if (atr != null)
      {
        return AsVisibleAs(atr.ElementType, m2);
      }
      IManagedPointerTypeReference mptr = s1 as IManagedPointerTypeReference;
      if (mptr != null)
      {
        return AsVisibleAs(mptr.TargetType, m2);
      }
      IPointerTypeReference uptr = s1 as IPointerTypeReference;
      if (uptr != null)
      {
        return AsVisibleAs(uptr.TargetType, m2);
      }
      IModifiedTypeReference mdtr = s1 as IModifiedTypeReference;
      if (mdtr != null)
      {
        return AsVisibleAs(mdtr.UnmodifiedType, m2);
      }
      IGenericTypeParameterReference igtpr = s1 as IGenericTypeParameterReference;
      if (igtpr != null)
      {
        return AsVisibleAs(igtpr.DefiningType, m2);
      }
      IGenericMethodParameterReference igmpr = s1 as IGenericMethodParameterReference;
      if (igmpr != null)
      {
        return AsVisibleAs(igmpr.DefiningMethod, m2);
      }
      throw new NotImplementedException();
    }

    /// <summary>
    /// Assumes neither member is specialized
    /// </summary>
    bool AsVisibleAsUnspecialized(INamespaceTypeReference t1, ITypeMemberReference m2)
    {
      Contract.Requires(t1 != null);

      if (t1.ResolvedType.IsPublic) return true;
      IUnitReference a1 = AssemblyOf(t1);
      return AsVisibleAsUnspecialized(a1, TypeMemberVisibility.Assembly, null, t1, m2);
    }

    /// <summary>
    /// Assumes neither member is specialized
    /// </summary>
    bool AsVisibleAsUnspecialized(ITypeMemberReference m1, ITypeMemberReference m2)
    {
      Contract.Requires(m1 != null);

      IUnitReference a1 = AssemblyOf(m1);
      var resolvedMember = m1.ResolvedTypeDefinitionMember;
      // Model methods/fields may not be able to be resolved if they come from an out-of-band contract.
      // In that scenario, the type they claim as their containing type doesn't have them in its members list.
      if (resolvedMember is Dummy) return true;
      TypeMemberVisibility s1Visibility = resolvedMember.Visibility;
      return AsVisibleAsUnspecialized(a1, s1Visibility, m1.ContainingType, m1 as ITypeReference, m2);
    }

    /// <summary>
    /// Assumes neither member is specialized
    /// </summary>
    bool AsVisibleAsUnspecialized(IUnitReference a1, TypeMemberVisibility s1Visibility, ITypeReference m1Parent, ITypeReference m1AsType, ITypeMemberReference m2)
    {
      Contract.Requires(m2 != null);

      IUnitReference a2 = AssemblyOf(m2);

      // handle special case of m2 being a member, but not a type.
      if (AsRestrictiveAsOneLevel(a1, a2, s1Visibility, m1Parent, m1AsType, m2.ResolvedTypeDefinitionMember.Visibility, m2.ResolvedTypeDefinitionMember)) return true;

      // else find one restrictive element in the type chain of m2
      ITypeReference s2Type = m2.ContainingType;
      do
      {
        ITypeDefinitionMember s2AsMember = (s2Type is ITypeMemberReference) ? ((ITypeMemberReference)s2Type).ResolvedTypeDefinitionMember : null;
        TypeMemberVisibility s2Visibility;
        if (s2AsMember != null)
        {
          s2Visibility = s2AsMember.Visibility;
        }
        else
        {
          INamespaceTypeReference ntr = s2Type as INamespaceTypeReference;
          s2Visibility = (ntr != null && ntr.ResolvedType.IsPublic) ? TypeMemberVisibility.Public : TypeMemberVisibility.Assembly;
        }

        if (AsRestrictiveAsOneLevel(a1, a2, s1Visibility, m1Parent, m1AsType, s2Visibility, s2AsMember)) return true;

        if (s2AsMember != null)
        {
          s2Type = s2AsMember.ContainingType;
        }
        else
        {
          s2Type = null;
        }
      }
      while (s2Type != null);
      return false;
    }

    /// <summary>
    /// Somewhat convoluted, as we need to support edge cases, where s1 is a top-level type, a nested type, or a member,
    /// and similarly for s2.
    /// </summary>
    bool AsRestrictiveAsOneLevel(IUnitReference a1, IUnitReference a2, TypeMemberVisibility s1Visibility, ITypeReference s1DeclaringType, ITypeReference s1AsType, TypeMemberVisibility s2Visibility, ITypeDefinitionMember s2)
    {
      if (s1Visibility == TypeMemberVisibility.Public) return true;

      switch (s1Visibility)
      {
        case TypeMemberVisibility.Assembly:
          if (s2Visibility == TypeMemberVisibility.Private || s2Visibility == TypeMemberVisibility.Assembly)
          {
            if (a1.UnitIdentity.Equals(a2.UnitIdentity)) return true;
          }
          break;

        case TypeMemberVisibility.Family:
          if (s2Visibility == TypeMemberVisibility.Private)
          {
            if (IsInsideOf(s2, s1AsType) || IsInsideSubclassOf(s2, s1DeclaringType))
            {
              return true;
            }
          }
          else if (s2Visibility == TypeMemberVisibility.Family)
          {
            if (IsEqualOrDerivesFrom(s2.ContainingType, s1DeclaringType))
            {
              return true;
            }
          }
          break;

        case TypeMemberVisibility.FamilyOrAssembly:
          switch (s2Visibility)
          {
            case TypeMemberVisibility.Private:

              if (a1.UnitIdentity.Equals(a2.UnitIdentity) || IsInsideSubclassOf(s2, s1DeclaringType))
              {
                return true;
              }
              break;

            case TypeMemberVisibility.Assembly:
              if (a1.UnitIdentity.Equals(a2.UnitIdentity))
              {
                return true;
              }
              break;
            case TypeMemberVisibility.Family:
              if (IsEqualOrDerivesFrom(s2.ContainingType, s1DeclaringType))
              {
                return true;
              }
              break;
            case TypeMemberVisibility.FamilyOrAssembly:
              if (a1.UnitIdentity.Equals(a2.UnitIdentity) && IsEqualOrDerivesFrom(s2.ContainingType, s1DeclaringType))
              {
                return true;
              }
              break;
          }
          break;

        case TypeMemberVisibility.Private:
          if (s2Visibility == TypeMemberVisibility.Private)
          {
            if (IsInsideOf(s2, s1DeclaringType))
            {
              return true;
            }
          }
          break;

        default:
          throw new NotImplementedException();
      }
      return false;
    }

    bool IsInsideOf(ITypeMemberReference member, ITypeReference type)
    {
      Contract.Requires(member != null);

      if (type == null) return false;
      for (ITypeReference t1 = member.ContainingType; t1 != null; )
      {
        if (TypeHelper.TypesAreEquivalent(t1, type)) return true;
        INestedTypeReference nest = t1 as INestedTypeReference;
        if (nest != null)
        {
          t1 = nest.ContainingType;
        }
        else
        {
          t1 = null;
        }
      }
      return false;
    }

    bool IsInsideSubclassOf(ITypeMemberReference member, ITypeReference type)
    {
      Contract.Requires(member != null);

      if (type == null) return false;
      for (ITypeReference t1 = member.ContainingType; t1 != null; )
      {
        if (TypeHelper.Type1DerivesFromOrIsTheSameAsType2(t1.ResolvedType, type)) return true;
        INestedTypeReference nest = t1 as INestedTypeReference;
        if (nest != null)
        {
          t1 = nest.ContainingType;
        }
        else
        {
          t1 = null;
        }
      }
      return false;
    }

    bool IsEqualOrDerivesFrom(ITypeReference t1, ITypeReference t2)
    {
      Contract.Requires(t2 == null || t1 != null);

      if (t2 == null) return false;
      return TypeHelper.Type1DerivesFromOrIsTheSameAsType2(t1.ResolvedType, t2);
    }

    IUnitReference AssemblyOf(ITypeReference typeReference)
    {
      Contract.Requires(typeReference != null);
      return TypeHelper.GetDefiningUnitReference(typeReference);
    }

    IUnitReference AssemblyOf(ITypeMemberReference m)
    {
      Contract.Requires(m != null);

      return AssemblyOf(m.ContainingType);
    }
    #endregion


    #region Visibility


    public bool IsVisibleFrom(MethodReferenceAdaptor m, TypeReferenceAdaptor t)
    {
      return TypeHelper.CanAccess(t.reference.ResolvedType, m.reference.ResolvedMethod);
    }

    public bool IsVisibleFrom(FieldReferenceAdaptor f, TypeReferenceAdaptor t)
    {
      return TypeHelper.CanAccess(t.reference.ResolvedType, f.reference.ResolvedField);
    }

    public bool IsVisibleFrom(TypeReferenceAdaptor t, TypeReferenceAdaptor tfrom)
    {
      return TypeHelper.CanAccess(tfrom.reference.ResolvedType, t.reference.ResolvedType);
    }

    #endregion

    #region Events


    public string Name(IEventDefinition @event)
    {
      return @event.Name.Value;
    }

    public bool HasAdder(IEventDefinition @event, out MethodReferenceAdaptor adder)
    {
      IMethodReference tmp = @event.Adder;
      adder = MethodReferenceAdaptor.AdaptorOf(tmp);
      return (tmp != null);
    }

    public bool HasRemover(IEventDefinition @event, out MethodReferenceAdaptor remover)
    {
      IMethodReference tmp = @event.Remover;
      remover = MethodReferenceAdaptor.AdaptorOf(tmp);
      return (tmp != null);
    }

    public IEnumerable<IEventDefinition> Events(TypeReferenceAdaptor type)
    {
      foreach (var e in type.ResolvedType.Events) yield return e;
    }

    public bool IsStatic(IEventDefinition e)
    {
      if (e.Adder != null) return IsStatic(e.Adder);
      if (e.Remover != null) return IsStatic(e.Remover);
      return true;
    }

    public bool IsOverride(IEventDefinition e)
    {
      if (e.Adder != null)
        return IsOverride(MethodReferenceAdaptor.AdaptorOf(e.Adder));
      else if (e.Remover != null)
        return IsOverride(MethodReferenceAdaptor.AdaptorOf(e.Remover));
      throw new ArgumentException("e is not a valid event");
    }

    public bool IsNewSlot(IEventDefinition e)
    {
      if (e.Adder != null)
        return IsNewSlot(MethodReferenceAdaptor.AdaptorOf(e.Adder));
//      else if (e.Adder != null)
//        return IsNewSlot(MethodReferenceAdaptor.AdaptorOf(e.Adder));
      throw new ArgumentException("e is not a valid event");
    }

    public bool IsSealed(IEventDefinition e)
    {
      if (e.Adder != null)
        return IsSealed(MethodReferenceAdaptor.AdaptorOf(e.Adder));
      else if (e.Remover != null)
        return IsSealed(MethodReferenceAdaptor.AdaptorOf(e.Remover));
      throw new ArgumentException("e is not a valid event");
    }

    public TypeReferenceAdaptor DeclaringType(IEventDefinition e)
    {
      IMethodReference any = e.Adder != null ? e.Adder : e.Remover;
      if (any == null)
        return TypeReferenceAdaptor.AdaptorOf(Dummy.Type);
      return DeclaringType(MethodReferenceAdaptor.AdaptorOf(any));
    }

    public TypeReferenceAdaptor HandlerType(IEventDefinition e)
    {
      return TypeReferenceAdaptor.AdaptorOf(e.Type);
    }

    public bool Equal(IEventDefinition e1, IEventDefinition e2)
    {
      MethodReferenceAdaptor a1, a2, r1, r2;
      bool ba1 = HasAdder(e1, out a1);
      bool br1 = HasRemover(e1, out r1);
      bool ba2 = HasAdder(e2, out a2);
      bool br2 = HasRemover(e2, out r2);

      if (ba1 != ba2 || br1 != br2)
        return false;

      if (ba1 && !Equal(a1, a2))
        return false;
      if (br1 && !Equal(r1, r2))
        return false;

      return true;
    }


    #endregion



  }

  public class IndexableCollection<ElementType> : IIndexable<ElementType>
  {

    private readonly List<ElementType> elements;

    public IndexableCollection(IEnumerable<ElementType> elements)
    {
      this.elements = new List<ElementType>(elements);
    }

    public int Count
    {
      get { return this.elements.Count; }
    }

    public ElementType this[int index]
    {
      get { return this.elements[index]; }
    }

  }

  public class IndexableCollection<FromType, ToType> : IIndexable<ToType>
  {
    private readonly Function<FromType, ToType> adapter;
    private readonly List<FromType> elements;

    public IndexableCollection(IEnumerable<FromType> elements, Function<FromType, ToType> adapter)
    {
      Contract.Requires(adapter != null);

      this.adapter = adapter;
      this.elements = new List<FromType>(elements);
    }

    public int Count { get { return this.elements.Count; } }
    public ToType this[int index]
    {
      get { return this.adapter(this.elements[index]); }
    }
  }

  public class TypeList : IIndexable<TypeReferenceAdaptor>
  {

    private readonly List<TypeReferenceAdaptor> elements;

    public TypeList(IEnumerable<IParameterTypeInformation> parameters)
    {
      Contract.Requires(parameters != null);

      List<TypeReferenceAdaptor> elements = new List<TypeReferenceAdaptor>();
      foreach (IParameterTypeInformation parameter in parameters)
        elements.Add(TypeReferenceAdaptor.AdaptorOf(parameter.Type));
      this.elements = elements;
    }

    public int Count
    {
      get { return this.elements.Count; }
    }

    public TypeReferenceAdaptor this[int index]
    {
      get { return this.elements[index]; }
    }
  }

  /// <summary>
  /// Needed because DefaultValue nodes in contracts are decoded
  /// as "ldloca new_local; initobj T; ldloc new_local" for a fresh
  /// local that has to be created and used for the first and third
  /// instructions.
  /// </summary>
  public struct LocalDefAdaptor : IEquatable<LocalDefAdaptor>
  {
    public readonly object local;

    private LocalDefAdaptor(object o)
    {
      this.local = o;
    }

    public static LocalDefAdaptor AdaptorOf(ILocalDefinition local)
    {
      return new LocalDefAdaptor(local);
    }

    public static LocalDefAdaptor AdaptorOf(IDefaultValue defaultValue)
    {
      return new LocalDefAdaptor(defaultValue);
    }

    public static LocalDefAdaptor AdaptorOf(IAddressOf addressOf)
    {
      return new LocalDefAdaptor(addressOf);
    }

    public IMetadataConstant CompileTimeValue
    {
      get
      {
        var ld = this.local as ILocalDefinition;
        return ld != null ? ld.CompileTimeValue : Dummy.Constant;
      }
    }

    public IEnumerable<ICustomModifier> CustomModifiers
    {
      get
      {
        var ld = this.local as ILocalDefinition;
        return ld != null ? ld.CustomModifiers : Enumerable<ICustomModifier>.Empty;
      }
    }

    public bool IsConstant
    {
      get
      {
        var ld = this.local as ILocalDefinition;
        return ld != null ? ld.IsConstant : false;
      }
    }

    public bool IsModified
    {
      get
      {
        var ld = this.local as ILocalDefinition;
        return ld != null ? ld.IsModified : false;
      }
    }

    public bool IsPinned
    {
      get
      {
        var ld = this.local as ILocalDefinition;
        return ld != null ? ld.IsPinned : false;
      }
    }

    public bool IsReference
    {
      get
      {
        var ld = this.local as ILocalDefinition;
        return ld != null ? ld.IsReference : false;
      }
    }

    public IName Name
    {
      get
      {
        var ld = this.local as ILocalDefinition;
        return ld != null ? ld.Name : Dummy.Name;
      }
    }

    public IEnumerable<ILocation> Locations
    {
      get
      {
        var ld = this.local as ILocalDefinition;
        return ld != null ? ld.Locations : Enumerable<ILocation>.Empty;
      }
    }

    public IMethodDefinition MethodDefinition
    {
      get
      {
        var ld = this.local as ILocalDefinition;
        return ld != null ? ld.MethodDefinition : Dummy.Method;
      }
    }

    public ITypeReference Type
    {
      get
      {
        var ld = this.local as ILocalDefinition;
        if (ld != null) return ld.Type;
        var dv = this.local as IDefaultValue;
        if (dv != null) return dv.Type;
        var addrOf = this.local as IAddressOf;
        if (addrOf != null) return addrOf.Expression.Type;
        throw new NotImplementedException();
      }
    }

    #region IEquatable
    public bool Equals(LocalDefAdaptor other)
    {
      return this.local == other.local;
    }
    #endregion

    #region Equals
    public override bool Equals(object obj)
    {
      if (obj is LocalDefAdaptor)
      {
        var lva = (LocalDefAdaptor)obj;
        return this.Equals(lva);
      }
      var ld = obj as ILocalDefinition;
      if (ld != null)
      {
        return this.local is ILocalDefinition && ld == this.local;
      }
      var dv = obj as IDefaultValue;
      if (dv != null)
      {
        return this.local is IDefaultValue && dv == this.local;
      }
      var addrOf = obj as IAddressOf;
      if (addrOf != null)
      {
        return this.local is IAddressOf && addrOf == this.local;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return (int)this.local.GetHashCode();
    }
    #endregion

    //public override string ToString() {
    //  return this.Name.Value;
    //}
  }


  public class HostEnvironment : CodeContractAwareHostEnvironment
  {
    /// <summary>
    /// Indicates whether IL locations should be preserved up into the code model
    /// by decompilers using this host.
    /// </summary>
    public override bool PreserveILLocations { get { return true; } }

    public delegate void RegisterSourceLocationProvider(IModule module, ISourceLocationProvider sourceLocationProvider);

    public IUnit PossiblyCompileFirstLoadUnitFrom(string location, List<string> referencedAssemblies, RegisterSourceLocationProvider registerSourceLocationProvider)
    {
      var ext = Path.GetExtension(location);
      switch (ext)
      {
        case "dll":
        case ".dll":
        case "exe":
        case ".exe":
          return base.LoadUnitFrom(location);
        case "cs":
        case ".cs":
          throw new NotImplementedException("We have disabled compiling from a .cs file. ");
#if Enable_Csharp2CCI
        IModule m;
          ISourceLocationProvider slp;
          CSharp2CCI.Utilities.FileToUnitAndSourceLocationProvider(this, location, this.LibPaths, referencedAssemblies, out m, out slp);
          if (registerSourceLocationProvider != null)
            registerSourceLocationProvider(m, slp);
          base.AttachContractExtractorAndLoadReferenceAssembliesFor(m);
          return m;
#endif
        default:
          return null;
      }
    }
  }
}

