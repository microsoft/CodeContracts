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
using Microsoft.Cci;
using Microsoft.Cci.Contracts;
using Microsoft.Cci.MutableContracts;
using System.IO;
using System.Diagnostics.Contracts;
using System.Xml;
using Microsoft.Research.DataStructures;
using Microsoft.Cci.ILToCodeModel;
using System.Diagnostics.CodeAnalysis;


namespace CCDoc {

  sealed class ContractTraverser : MetadataTraverser {
    IMetadataHost host;
    public ContractTraverser(IMetadataHost host) {
      this.host = host;
    }
    private bool IsContractClass(ITypeDefinition typeDefinition) {
      Contract.Requires(typeDefinition != null);
      return AttributeHelper.Contains(typeDefinition.Attributes, ContractClassForAttribute);
    }
    INamespaceTypeReference ContractClassForAttribute {
      get {
        if (contractClassForAttribute == null)
          contractClassForAttribute = ContractHelper.CreateTypeReference(this.host, ContractAssemblyReference, "System.Diagnostics.Contracts.ContractClassForAttribute");
        return contractClassForAttribute;
      }
    }
    INamespaceTypeReference contractClassForAttribute = null;
    IAssemblyReference ContractAssemblyReference {
      get {
        if (contractAssemblyReference == null)
          contractAssemblyReference = new Microsoft.Cci.Immutable.AssemblyReference(host, host.ContractAssemblySymbolicIdentity);
        return contractAssemblyReference;
      }
    }
    IAssemblyReference contractAssemblyReference = null;

    public override void TraverseChildren(ITypeDefinition typeDefinition) {
      if (CCDocContractHelper.IsCompilerGenerated(this.host, typeDefinition) || IsContractClass(typeDefinition))
        return; //Don't continue traverser
      base.TraverseChildren(typeDefinition);
    }

  }

  /// <summary>
  /// Responsible for traversing an assembly and extracting contracts. The contracts are stored in the "contracts" dictionary that is passed as a parameter to the constructor.
  /// </summary>
  [ContractVerification(false)]
  sealed class ContractVisitor : MetadataVisitor {
    /// <summary>
    /// The host environment of the traverser.
    /// </summary>
    CodeContractAwareHostEnvironment host;
    /// <summary>
    /// Dictionary of contracts found while visiting methods, properties and types.
    /// </summary>
    IDictionary<string, XContract[]> contracts;
    /// <summary>
    /// DocTracker used to write output.
    /// </summary>
    DocTracker docTracker;

    IAssemblyReference ContractAssemblyReference {
      get {
        if (contractAssemblyReference == null)
          contractAssemblyReference = new Microsoft.Cci.Immutable.AssemblyReference(host, host.ContractAssemblySymbolicIdentity);
        return contractAssemblyReference;
      }
    }
    IAssemblyReference contractAssemblyReference = null;
    INamespaceTypeReference PureAttribute {
      get {
        if (pureAttribute == null)
          pureAttribute = ContractHelper.CreateTypeReference(this.host, ContractAssemblyReference, "System.Diagnostics.Contracts.PureAttribute");
        return pureAttribute;
      }
    }
    INamespaceTypeReference pureAttribute;

    [ContractInvariantMethod]
    void Invariants() {
      Contract.Invariant(host != null);
      //Contract.Invariant(options != null);
      Contract.Invariant(contracts != null);
    }

    /// <summary>
    /// Creates a ContractTraverser
    /// </summary>
    /// <param name="contracts">The dictionary of contracts. This gets filled with contracts during the traversal.</param>
    /// <param name="options">The options.</param>
    /// <param name="host">The host environment of the traverser used to load any required assemblies.</param>
    /// <param name="docTracker">The DocTracker used to provide metrics and to be written into during the traversal.</param>
    public ContractVisitor(CodeContractAwareHostEnvironment host, IDictionary<string, XContract[]> contracts, Options options, DocTracker docTracker) {
      Contract.Requires(host != null);
      Contract.Requires(contracts != null);
      //Contract.Requires(options != null);
      Contract.Requires(docTracker != null);

      this.contracts = contracts;
      //this.options = options;
      this.docTracker = docTracker;
      this.host = host;
    }

    /// <summary>
    /// Extracts object invariants.
    /// </summary>
    public override void Visit(ITypeDefinition typeDefinition) {
      Contract.Assert(typeDefinition != null);

      string typeId = TypeHelper.GetTypeName(typeDefinition, NameFormattingOptions.DocumentationId);
      docTracker.WriteLine(typeId);

      ContractPackager packager = new ContractPackager(host, docTracker);
      packager.PackageTypeContracts(typeDefinition);
      XContract[] contractArray;
      if (packager.TryGetContracts(out contractArray)) {
        docTracker.AddMemberInfo(contractArray.Length, MemberKind.Type);
        contracts.Add(new KeyValuePair<string, XContract[]>(typeId, contractArray));
      } else {
        docTracker.AddMemberInfo(0, MemberKind.Type);
      }

      //base.TraverseChildren(typeDefinition);
    }
    /// <summary>
    /// Extracts method contracts for the accessors and packages them under the property itself.
    /// </summary>
    public override void Visit(IPropertyDefinition propertyDefinition) {
      Contract.Assert(propertyDefinition != null);

      string propertyId = MemberHelper.GetMemberSignature(propertyDefinition, NameFormattingOptions.DocumentationId);
      docTracker.WriteLine(propertyId);

      int contractsCounter = 0;

      XAccessorContract getterContracts = null;
      if (propertyDefinition.Getter != null) {
        var getterMethod = propertyDefinition.Getter.ResolvedMethod;
        Contract.Assume(getterMethod != null);
        bool isPure = IsPure(getterMethod); //TODO: Remove, this should be handled on the packager side.
        ContractPackager packager = new ContractPackager(host, docTracker);
        packager.PackageMethodContracts(getterMethod, isPure);
        XContract[] getterContractArray;
        if (packager.TryGetContracts(out getterContractArray)) {
          contractsCounter += getterContractArray.Length;
          docTracker.AddMemberInfo(getterContractArray.Length, MemberKind.Getter);
          getterContracts = new XAccessorContract(this.host, XAccessorKind.Getter, getterContractArray, docTracker);
        }
      }

      XAccessorContract setterContracts = null;
      if (propertyDefinition.Setter != null) {
        var setterMethod = propertyDefinition.Setter.ResolvedMethod;
        Contract.Assume(setterMethod != null);
        bool isPure = IsPure(setterMethod); //TODO: Remove, this should be handled on the packager side.
        ContractPackager packager = new ContractPackager(host, docTracker);
        packager.PackageMethodContracts(setterMethod, isPure);
        XContract[] setterContractArray;
        if (packager.TryGetContracts(out setterContractArray)) {
          contractsCounter += setterContractArray.Length;
          docTracker.AddMemberInfo(setterContractArray.Length, MemberKind.Setter);
          setterContracts = new XAccessorContract(this.host, XAccessorKind.Setter, setterContractArray, docTracker);
        }
      }

      XContract[] accessorContracts = null;
      if (getterContracts != null && setterContracts != null)
        accessorContracts = new XContract[2] { getterContracts, setterContracts };
      else if (getterContracts != null)
        accessorContracts = new XContract[1] { getterContracts };
      else if (setterContracts != null)
        accessorContracts = new XContract[1] { setterContracts };

      if (accessorContracts != null) {
        contracts.Add(new KeyValuePair<string, XContract[]>(propertyId, accessorContracts));
      }

      docTracker.AddMemberInfo(contractsCounter, MemberKind.Property);

      //base.TraverseChildren(propertyDefinition);
    }
    /// <summary>
    /// Extracts the method contracts. Does not visit property accessors.
    /// </summary>
    public override void Visit(IMethodDefinition methodDefinition) {
      Contract.Assert(methodDefinition != null);

      if (CCDocContractHelper.IsCompilerGenerated(this.host, methodDefinition))
        return; //Don't continue traverser

      if (MethodHelper.IsGetter(methodDefinition) || MethodHelper.IsSetter(methodDefinition)) {
        return;
      }

      string methodId = MemberHelper.GetMemberSignature(methodDefinition, NameFormattingOptions.DocumentationId);
      docTracker.WriteLine(methodId);

      bool isPure = IsPure(methodDefinition);//TODO: Remove, this should be handled on the packager side.

      ContractPackager packager = new ContractPackager(host, docTracker);
      packager.PackageMethodContracts(methodDefinition, isPure);
      XContract[] contractArray;
      if (packager.TryGetContracts(out contractArray)) {
        docTracker.AddMemberInfo(contractArray.Length, MemberKind.Method);
        contracts.Add(new KeyValuePair<string, XContract[]>(methodId, contractArray));
      } else {
        docTracker.AddMemberInfo(0, MemberKind.Method);
      }

      //base.TraverseChildren(methodDefinition);
    }

    #region Attribute helpers
    private bool IsPure(IMethodDefinition methodDefinition) {
      Contract.Requires(methodDefinition != null);
      return AttributeHelper.Contains(methodDefinition.Attributes, PureAttribute);
    }
    #endregion

    /// <summary>
    /// Aggregates method and type contracts into a list of <see cref="XContract"/>s.
    /// </summary>
    /// <remarks>
    /// In CCDoc, this is called once per method, type, and property so that the contracts can be paired with a special id string.
    /// </remarks>
    [ContractVerification(true)]
    class ContractPackager {
      List<XContract> contractList;
      CodeContractAwareHostEnvironment host;
      DocTracker docTracker;

      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(docTracker != null);
        Contract.Invariant(host != null);
        Contract.Invariant(contractList == null || contractList.Count > 0);
      }

      /// <summary>
      /// Creates a new contract packager.
      /// </summary>
      public ContractPackager(CodeContractAwareHostEnvironment host, DocTracker docTracker) {
        Contract.Requires(host != null);
        Contract.Requires(docTracker != null);
        this.host = host;
        this.docTracker = docTracker;
      }
      /// <summary>
      /// Attempts to gather the contracts the packager collected into an array.
      /// </summary>
      /// <returns>
      /// Returns false and sets the <paramref name="contractArray"/> to null if and only if no contracts 
      /// have been packaged. Otherwise it returns true and stores the packaged contracts in <paramref name="contractArray"/>.
      /// </returns>
      /// <remarks>This doesn't do any packaging, this only retrieves contracts that have been packaged. Use PackageMethodContracts or 
      /// PackageTypeContracts to package contracts.</remarks>
      //[Pure]
      public bool TryGetContracts(out XContract[] contractArray) {
        Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out contractArray).Length > 0);

        if (this.contractList != null)
        {
          var oldCount = this.contractList.Count;
          contractArray = this.contractList.ToArray();
          return true;
        }
        contractArray = null;
        return false;
      }
      void PackageContract(XContract contract) {
        if (contractList == null)
          contractList = new List<XContract>(2);
        contractList.Add(contract);
      }

      //[SuppressMessage("Microsoft.Contracts", "CC1036", Justification="CCI2 has no contracts yet")]
      [ContractVerification(false)]
      void PackagePreconditions(IEnumerable<IPrecondition> preconditions, string inheritedFrom, string inheritedFromTypeName) {
        Contract.Requires(preconditions != null);
        Contract.Requires(IteratorHelper.EnumerableIsNotEmpty<IPrecondition>(preconditions));

        foreach (var precondition in preconditions) {
          Contract.Assume(precondition != null, "lack of contracts for collections");
          PackageContract(new XPrecondition(host, precondition, inheritedFrom, inheritedFromTypeName, docTracker));
        }
      }
      void PackagePostconditions(IEnumerable<IPostcondition> postconditions, string inheritedFrom, string inheritedFromTypeName) {
        Contract.Requires(postconditions != null);
        Contract.Requires(IteratorHelper.EnumerableIsNotEmpty<IPostcondition>(postconditions));

        foreach (var postcondition in postconditions) {
          Contract.Assume(postcondition != null, "Can't prove this for now");
          PackageContract(new XPostcondition(this.host, postcondition, inheritedFrom, inheritedFromTypeName, docTracker));
        }
      }
      void PackageThrownExceptions(IEnumerable<IThrownException> thrownExceptions, string inheritedFrom, string inheritedFromTypeName) {
        Contract.Requires(thrownExceptions != null);
        Contract.Requires(IteratorHelper.EnumerableIsNotEmpty<IThrownException>(thrownExceptions));

        foreach (var thrownException in thrownExceptions) {
          Contract.Assume(thrownException != null, "lack of contracts for collections");
          Contract.Assume(thrownException.Postcondition != null, "lack of CCI2 contracts");
          Contract.Assume(thrownException.ExceptionType != null, "lack of CCI2 contracts");
          PackageContract(new XThrownException(this.host, thrownException, inheritedFrom, inheritedFromTypeName, docTracker));
        }
      }
      [ContractVerification(false)]
      void PackageInvariants(IEnumerable<ITypeInvariant> invariants) {
        Contract.Requires(IteratorHelper.EnumerableIsNotEmpty<ITypeInvariant>(invariants));

        foreach (var invariant in invariants) {
          Contract.Assume(invariant != null, "Can't prove this for now");
          PackageContract(new XInvariant(this.host, invariant, docTracker));
        }
      }
      [ContractVerification(false)]
      void PackageMethodContracts(IMethodContract methodContract, string methodId, string typeName)
      {
        Contract.Requires(methodContract != null);

        if (IteratorHelper.EnumerableIsNotEmpty<IPrecondition>(methodContract.Preconditions))
          PackagePreconditions(methodContract.Preconditions, methodId, typeName);
        if (IteratorHelper.EnumerableIsNotEmpty<IPostcondition>(methodContract.Postconditions))
          PackagePostconditions(methodContract.Postconditions, methodId, typeName);
        if (IteratorHelper.EnumerableIsNotEmpty<IThrownException>(methodContract.ThrownExceptions))
          PackageThrownExceptions(methodContract.ThrownExceptions, methodId, typeName);
      }
      /// <summary>
      /// Attempts to package all contracts for a method: the direct contracts and any inherited contracts.
      /// If <paramref name="method"/> overrides a method, then the overridden method's contracts are
      /// packaged also. If <paramref name="method"/> implements an interface method, then the interface
      /// method's contracts are packaged also.
      /// </summary>
      [ContractVerification(false)]
      public void PackageMethodContracts(IMethodDefinition method, bool isPure) {
        Contract.Requires(method != null);

        #region Package purity
        if (isPure)
          PackageContract(new XPure(this.host, isPure, docTracker));
        //Don't package anything if it isn't pure. (That way it is easyer to deal with on the Sandcastle side)
        #endregion
        #region Package contracts from this implementation
        IMethodContract thisContract;
        if (CCDocContractHelper.TryGetMethodContract(host, method, out thisContract, docTracker)) {
          PackageMethodContracts(thisContract, null, null);
        }
        #endregion
        #region Package contracts from base overrides
        if (!method.IsNewSlot) { // REVIEW: Is there a better test?
          IMethodDefinition overriddenMethod = MemberHelper.GetImplicitlyOverriddenBaseClassMethod(method) as IMethodDefinition;
          while (overriddenMethod != null && overriddenMethod != Dummy.Method) {
            IMethodContract/*?*/ overriddenContract;
            if (CCDocContractHelper.TryGetMethodContract(host, overriddenMethod, out overriddenContract, docTracker)) {
              SubstituteParameters sps = new SubstituteParameters(this.host, method, overriddenMethod);
              IMethodContract newContract = sps.Rewrite(overriddenContract) as MethodContract;
              var unspecializedMethod = MethodHelper.Unspecialize(overriddenMethod);
              var methodId = MemberHelper.GetMemberSignature(unspecializedMethod, NameFormattingOptions.DocumentationId);
              var typeName = TypeHelper.GetTypeName(unspecializedMethod.ContainingType, NameFormattingOptions.OmitContainingNamespace | NameFormattingOptions.OmitContainingType);
              PackageMethodContracts(newContract, methodId, typeName);
            }
            overriddenMethod = MemberHelper.GetImplicitlyOverriddenBaseClassMethod(overriddenMethod) as IMethodDefinition;
          }
        }
        #endregion
        #region Package contracts from implicit interface implementations
        foreach (IMethodDefinition ifaceMethod in MemberHelper.GetImplicitlyImplementedInterfaceMethods(method)) {
          IMethodContract/*?*/ ifaceContract;
          if (!CCDocContractHelper.TryGetMethodContract(host, ifaceMethod, out ifaceContract, docTracker)) {
            continue;
          }
          SubstituteParameters sps = new SubstituteParameters(this.host, method, ifaceMethod);
          IMethodContract newContract = sps.Rewrite(ifaceContract) as MethodContract;
          var unspecializedMethod = MethodHelper.Unspecialize(ifaceMethod);
          var methodId = MemberHelper.GetMemberSignature(unspecializedMethod, NameFormattingOptions.DocumentationId);
          var typeName = TypeHelper.GetTypeName(unspecializedMethod.ContainingType, NameFormattingOptions.OmitContainingNamespace | NameFormattingOptions.OmitContainingType);
          PackageMethodContracts(newContract, methodId, typeName);
        }
        #endregion
        #region Package contracts from explicit interface implementations
        // REVIEW: Why does GetExplicitlyOverriddenMethods return IMethodReference when GetImplicitlyImplementedInterfaceMethods
        // returns IMethodDefinition?
        foreach (IMethodReference ifaceMethodRef in MemberHelper.GetExplicitlyOverriddenMethods(method)) {
          IMethodDefinition/*?*/ ifaceMethod = ifaceMethodRef.ResolvedMethod;
          if (ifaceMethod == null) continue;
          IMethodContract/*?*/ ifaceContract;
          if (!CCDocContractHelper.TryGetMethodContract(host, ifaceMethod, out ifaceContract, docTracker)) {
            continue;
          }
          SubstituteParameters sps = new SubstituteParameters(this.host, method, ifaceMethod);
          IMethodContract newContract = sps.Rewrite(ifaceContract) as MethodContract;
          var unspecializedMethod = MethodHelper.Unspecialize(ifaceMethod);
          var methodId = MemberHelper.GetMemberSignature(unspecializedMethod, NameFormattingOptions.DocumentationId);
          var typeName = TypeHelper.GetTypeName(unspecializedMethod.ContainingType, NameFormattingOptions.OmitContainingNamespace | NameFormattingOptions.OmitContainingType);
          PackageMethodContracts(newContract, methodId, typeName); ;
        }
        #endregion
      }
      /// <summary>
      /// Attempts to package all contracts for a type. Does not include inherited contracts.
      /// </summary>
      public void PackageTypeContracts(ITypeDefinition type) {
        Contract.Requires(type != null);

        #region Package contracts from this implementation
        ITypeContract typeContract;
        if (CCDocContractHelper.TryGetTypeContract(this.host, type, out typeContract, docTracker)) {
          if (IteratorHelper.EnumerableIsNotEmpty<ITypeInvariant>(typeContract.Invariants)) {
            PackageInvariants(typeContract.Invariants);
          }
        }
        #endregion
      }
    }
  }
  /// <summary>
  /// A helper class for getting contracts from methods and types.
  /// </summary>
  [ContractVerification(false)]
  public static class CCDocContractHelper {
    /// <summary>
    /// Wraps a call to GetMethodContractFor inside of a try-catch statement.
    /// </summary>
    public static bool TryGetMethodContract(CodeContractAwareHostEnvironment host, IMethodReference method, out IMethodContract methodContract, DocTracker docTracker) {
      Contract.Requires(host != null);
      Contract.Requires(method != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out methodContract) != null);
      try {
        methodContract = ContractHelper.GetMethodContractFor(host, method.ResolvedMethod);
      } catch (NullReferenceException) {
        docTracker.WriteLine("ERROR: NullReferenceException was thrown in CCI!");
        methodContract = null;
      }
      //} catch (Exception e) {
      //  docTracker.WriteLine("ERROR: Exception of type '{0}' was thrown in CCI!", e.GetType().Name);
      //  docTracker.WriteLine("\t'{0}'", e.Message);
      //  methodContract = null;
      //}
      return methodContract != null;
    }
    /// <summary>
    /// Wraps a call to GetTypeContractFor inside of a try-catch statement.
    /// </summary>
    public static bool TryGetTypeContract(CodeContractAwareHostEnvironment host, ITypeReference type, out ITypeContract typeContract, DocTracker docTracker) {
      Contract.Requires(host != null);
      Contract.Requires(type != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out typeContract) != null);
      try {
        var unit = TypeHelper.GetDefiningUnit(type.ResolvedType);
        if (unit == null) {
          typeContract = null;
        } else {
          IContractProvider lcp = host.GetContractExtractor(unit.UnitIdentity);
          if (lcp == null) {
            typeContract = null;
          } else {
            typeContract = lcp.GetTypeContractFor(type);
          }
        }
      } catch (NullReferenceException) {
        docTracker.WriteLine("ERROR: NullReferenceException was thrown in CCI!");
        typeContract = null;
      }
      return typeContract != null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="host"></param>
    /// <param name="typeDefinition"></param>
    /// <returns></returns>
    public static bool IsCompilerGenerated(IMetadataHost host, ITypeDefinition typeDefinition) {
      Contract.Requires(typeDefinition != null);
      foreach (ICustomAttribute attribute in typeDefinition.Attributes) {
        if (TypeHelper.TypesAreEquivalent(attribute.Type, host.PlatformType.SystemRuntimeCompilerServicesCompilerGeneratedAttribute))
          return true;
      }
      return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="host"></param>
    /// <param name="methodDefinition"></param>
    /// <returns></returns>
    public static bool IsCompilerGenerated(IMetadataHost host, IMethodDefinition methodDefinition) {
      Contract.Requires(methodDefinition != null);
      foreach (ICustomAttribute attribute in methodDefinition.Attributes) {
        if (TypeHelper.TypesAreEquivalent(attribute.Type, host.PlatformType.SystemRuntimeCompilerServicesCompilerGeneratedAttribute))
          return true;
      }
      ITypeDefinition typeDefinition = methodDefinition.ContainingTypeDefinition;
      return IsCompilerGenerated(host, typeDefinition);
    }
  }
}
