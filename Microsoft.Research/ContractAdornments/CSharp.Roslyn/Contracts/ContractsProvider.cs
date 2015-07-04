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
using Microsoft.Cci;
using Microsoft.Cci.Contracts;
using System.Diagnostics;
using System.Linq;
using Microsoft.Cci.MutableContracts;
using ContractAdornments.Interfaces;
using CSharpAssembly = Microsoft.CodeAnalysis.IAssemblySymbol;
using CSharpMember = Microsoft.CodeAnalysis.ISymbol;
using CSharpNamespace = Microsoft.CodeAnalysis.INamespaceSymbol;
using CSharpParameter = Microsoft.CodeAnalysis.IParameterSymbol;
using CSharpType = Microsoft.CodeAnalysis.ITypeSymbol;

using IArrayTypeSymbol = Microsoft.CodeAnalysis.IArrayTypeSymbol;
using ITypeParameterSymbol = Microsoft.CodeAnalysis.ITypeParameterSymbol;
using RefKind = Microsoft.CodeAnalysis.RefKind;
using SymbolKind = Microsoft.CodeAnalysis.SymbolKind;
using TypeKind = Microsoft.CodeAnalysis.TypeKind;

namespace ContractAdornments {
  public class ContractsProvider : IContractsProvider {
    readonly IProjectTracker _projectTracker;
    INonlockingHost Host {
      get {
        Contract.Requires(_projectTracker.Host != null);
        Contract.Ensures(Contract.Result<CodeContractAwareHostEnvironment>() != null);

        return _projectTracker.Host;
      }
    }

    readonly Dictionary<CSharpMember, IMethodReference> _semanticMembersToCCIMethods;
    readonly Dictionary<CSharpType, ITypeReference> _semanticTypesToCCITypes;
    readonly Dictionary<CSharpAssembly, IAssemblyReference> _semanticAssemblysToCCIAssemblys;
    readonly Dictionary<CSharpMember, Tuple<IMethodReference, IMethodReference>> _semanticPropertiesToCCIAccessorMethods;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_semanticAssemblysToCCIAssemblys != null);
      Contract.Invariant(_semanticMembersToCCIMethods != null);
      Contract.Invariant(_semanticTypesToCCITypes != null);
      Contract.Invariant(_semanticPropertiesToCCIAccessorMethods != null);
      Contract.Invariant(_projectTracker != null);
      Contract.Invariant(Host != null);
    }

    public ContractsProvider(IProjectTracker projectTracker) {
      Contract.Requires(projectTracker != null);

      //Grab a pointer back to our project tracker
      _projectTracker = projectTracker;

      //Initialize our caches
      _semanticMembersToCCIMethods = new Dictionary<CSharpMember, IMethodReference>();
      _semanticTypesToCCITypes = new Dictionary<CSharpType, ITypeReference>();
      _semanticAssemblysToCCIAssemblys = new Dictionary<CSharpAssembly, IAssemblyReference>();
      _semanticPropertiesToCCIAccessorMethods = new Dictionary<CSharpMember, Tuple<IMethodReference, IMethodReference>>();
    }

    public void Clear() {
      //host = null;
      ContractsPackageAccessor.Current.Logger.WriteToLog("Clearing all caches in ContractsProvider.");

      //Clear caches
      _semanticAssemblysToCCIAssemblys.Clear();
      _semanticMembersToCCIMethods.Clear();
      _semanticTypesToCCITypes.Clear();
      _semanticPropertiesToCCIAccessorMethods.Clear();
    }
    //int counter = 0;
    [ContractVerification(true)]
    public bool TryGetAssemblyReference(CSharpAssembly semanticAssembly, out IAssemblyReference cciAssembly) {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out cciAssembly) != null);

      cciAssembly = null;

      #region Check input
      if (semanticAssembly == null) {
        return false;
      }
      #endregion
      #region Check cache
      if (ContractsPackageAccessor.Current.VSOptionsPage.Caching)
        if (_semanticAssemblysToCCIAssemblys.TryGetValue(semanticAssembly, out cciAssembly))
          return cciAssembly != Dummy.AssemblyReference && cciAssembly != null;
      #endregion
      // distinguish between the AssemblyName and the ProjectName

      var semanticAssemblyFileName = (semanticAssembly.Name == null) ? null : semanticAssembly.Name;
      if (string.IsNullOrWhiteSpace(semanticAssemblyFileName)) return false;
      var semanticAssemblyName = Path.GetFileName(semanticAssemblyFileName);
      if (semanticAssemblyName.EndsWith(".dll") || semanticAssemblyName.EndsWith(".exe"))
        semanticAssemblyName = semanticAssemblyName.Remove(semanticAssemblyName.Length - 4, 4);

      #region Try to get assembly from previously loaded assemblies from host
      foreach (var unit in Host.LoadedUnits) {
        if (unit == null) continue;
        if (unit is Dummy) continue;
        if (unit.Name.Value == semanticAssemblyName) {
          cciAssembly = (IAssemblyReference)unit;
          if (cciAssembly.ResolvedAssembly.Location == semanticAssemblyFileName)
          {
            goto ReturnTrue;
          }
        }
      }
      #endregion
      #region Check if assembly is the same as the current project's output assembly
      if (_projectTracker.AssemblyIdentity != null && _projectTracker.AssemblyIdentity.Name != null) {
        if (semanticAssemblyName.Equals(_projectTracker.AssemblyIdentity.Name.Value, StringComparison.OrdinalIgnoreCase) || semanticAssemblyName.Equals(_projectTracker.ProjectName, StringComparison.OrdinalIgnoreCase)) {
          cciAssembly = new Microsoft.Cci.Immutable.AssemblyReference(this.Host, _projectTracker.AssemblyIdentity);
          Host.AddLibPath(Path.Combine(Path.GetDirectoryName(semanticAssemblyFileName), "CodeContracts"));
          Host.AddLibPath(Path.Combine(Path.GetDirectoryName(semanticAssemblyFileName), @"..\Debug\CodeContracts"));
          goto ReturnTrue;
        }
      } else
        ContractsPackageAccessor.Current.Logger.WriteToLog("Assembly identity for the project: " + _projectTracker.ProjectName + " was null.");
      #endregion
      #region Build assembly reference
      if (semanticAssembly.Name == null || string.IsNullOrWhiteSpace(semanticAssembly.Name)) goto ReturnFalseNoOutput; // because we have no name.
      var projectName = Path.GetFileName(semanticAssembly.Name);
      if (projectName.EndsWith(".dll") || projectName.EndsWith(".exe"))
          projectName = projectName.Remove(projectName.Length - 4, 4);
      var references = _projectTracker.References;
      VSLangProj.Reference reference = null;
      for (int i = 1, refcount = references == null ? 0 : references.Count; i <= refcount; i++)
      {
        var tempRef = references.Item(i);
        if (tempRef == null) continue;
        string refName = tempRef.Name;//Path.GetFileNameWithoutExtension(tempRef.Name);
        if (refName == null) continue;
        if (refName.Equals(projectName, StringComparison.OrdinalIgnoreCase)) {
          reference = tempRef;
          break;
        }
      }
      if (reference != null) {
        IName iName = Host.NameTable.GetNameFor(Path.GetFileNameWithoutExtension(reference.Path));
        string culture = reference.Culture ?? "en";
        Version version = new Version(reference.MajorVersion, reference.MinorVersion, reference.BuildNumber, reference.RevisionNumber);
        string location = reference.Path;
        if (!string.IsNullOrEmpty(location))
        {
          Host.AddLibPath(Path.Combine(location.Substring(0, location.Length - Path.GetFileName(location).Length), "CodeContracts"));
          var assemblyIdentity = new AssemblyIdentity(iName, culture, version, Enumerable<byte>.Empty, location);
          cciAssembly = new Microsoft.Cci.Immutable.AssemblyReference(this.Host, assemblyIdentity);
          goto ReturnTrue;
        }
      }
      goto ReturnFalse;
      #endregion
      #region ReturnTrue:
    ReturnTrue:
      if (ContractsPackageAccessor.Current.VSOptionsPage.Caching)
        _semanticAssemblysToCCIAssemblys[semanticAssembly] = cciAssembly;
      EnsureAssemblyIsLoaded(semanticAssembly, ref cciAssembly);
      return true;
      #endregion
      #region ReturnFalse:
    ReturnFalse:
      ContractsPackageAccessor.Current.Logger.WriteToLog("Failed to build assembly reference for: " + semanticAssembly.Name);
    ReturnFalseNoOutput:
      if (ContractsPackageAccessor.Current.VSOptionsPage.Caching)
        _semanticAssemblysToCCIAssemblys[semanticAssembly] = Dummy.AssemblyReference;
      return false;
      #endregion
    }
    public bool TryGetPropertyContract(CSharpMember semanticMember, out IMethodContract getterContract, out IMethodContract setterContract)
    {
        Contract.Requires(semanticMember == null || semanticMember.Kind == SymbolKind.Property);

        getterContract = null;
        setterContract = null;

        #region Check input
        if (semanticMember == null)
        {
            return false;
        }
        #endregion
        IMethodReference getter, setter;
        if (!TryGetPropertyAccessorReferences(semanticMember, out getter, out setter)) { return false; }

        var success = false;
        if (getter != null)
        {
            success |= TryGetMethodContract(getter, out getterContract);
        }
        if (setter != null)
        {
            success |= TryGetMethodContract(setter, out setterContract);
        }
        return success;
    }
    public bool TryGetMethodContract(CSharpMember semanticMethod, out IMethodContract methodContract)
    {
      Contract.Requires(semanticMethod == null || semanticMethod.Kind == SymbolKind.Method);
      Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out methodContract) != null));

      methodContract = null;

      #region Check input
      if (semanticMethod == null) {
        return false;
      }
      #endregion

      #region Try get the reference then check for contracts
      IMethodReference cciMethod;
      if (TryGetMethodReference(semanticMethod, out cciMethod)) {
        if (TryGetMethodContract(cciMethod, out methodContract)) {
          return true;
        } else {
          //No need, detailed logs are written at all code paths in "TryGetMethodContract"
          //ContractsPackageAccessor.Current.Logger.WriteToLog("Failed to get method contracts for: " + cciMethod.Name);
        }
      } else {
        methodContract = null;
        if (semanticMethod.Name != null)
        {
          ContractsPackageAccessor.Current.Logger.WriteToLog("Failed to get CCI reference for: " + semanticMethod.Name);
        }
      }
      return false;
      #endregion
    }
    public bool TryGetMethodContract(IMethodReference method, out IMethodContract methodContract) {
      Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out methodContract) != null));

      methodContract = null;

      #region Check input
      if (method == null) {
        return false;
      }
      #endregion
      try {
        // Resolving the method works *only* if the unit it is defined in has already been loaded.
        // That should have happened as part of creating the IMethodReference.
        var resolvedMethod = method.ResolvedMethod;
        if (resolvedMethod != Dummy.Method) {
          methodContract = ContractHelper.GetMethodContractForIncludingInheritedContracts(Host, resolvedMethod);
          if (methodContract == null) {
            methodContract = ContractDummy.MethodContract;
            ContractsPackageAccessor.Current.Logger.WriteToLog(String.Format("Did not find any method contract(s) for '{0}'", method.Name));
          } else {
            ContractsPackageAccessor.Current.Logger.WriteToLog(
              String.Format("Got method contract(s) for '{0}': {1} preconditions, {2} postconditions",
                method.Name,
                Microsoft.Cci.IteratorHelper.EnumerableCount(methodContract.Preconditions),
                Microsoft.Cci.IteratorHelper.EnumerableCount(methodContract.Postconditions)));
          }
        } else {
          ContractsPackageAccessor.Current.Logger.WriteToLog(String.Format("Method '{0}' resolved to dummy", method.Name));
        }
      } catch (NullReferenceException) {
        methodContract = null;
        ContractsPackageAccessor.Current.Logger.WriteToLog(String.Format("NullReferenceException thrown when getting contracts for '{0}'", method.Name));
      }
      return methodContract != null;
    }
    public bool TryGetMethodContractSafe(CSharpMember semanticMehod, out IMethodContract methodContract) {
      Contract.Requires(semanticMehod == null || semanticMehod.Kind == SymbolKind.Method);
      Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out methodContract) != null));

      methodContract = null;

      #region Check input
      if (semanticMehod == null) {
        return false;
      }
      #endregion
      #region Call TryGetMethodContract cautiously
      try {
        //Can we get contracts?
        if (!TryGetMethodContract(semanticMehod, out methodContract))
          return false;
      }
      #endregion
      #region Abort on exception
        //Give up on our contracts if we get an exception
      catch (IllFormedSemanticModelException) {
        return false;
      } catch (InvalidOperationException e) {
        if (!e.Message.Contains(ContractsPackageAccessor.InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate))
          throw e;
        else
          return false;
      } catch (System.Runtime.InteropServices.COMException e) {
        if (!e.Message.Contains(ContractsPackageAccessor.COMExceptionMessage_BindingFailed))
          throw e;
        else
          return false;
      }
      #endregion

      return methodContract != null;
    }
    public bool TryGetMethodReference(CSharpMember semanticMethod, out IMethodReference cciMethod) {
      Contract.Requires(semanticMethod == null || semanticMethod.Kind == SymbolKind.Method);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out cciMethod) != null);

      cciMethod = null;

      #region Check input
      if (semanticMethod == null) {
        return false;
      }
      #endregion
      #region Check cache
      if (ContractsPackageAccessor.Current.VSOptionsPage.Caching)
        if (_semanticMembersToCCIMethods.TryGetValue(semanticMethod, out cciMethod))
          return (!(cciMethod is Dummy)) && cciMethod != null;
      #endregion
      #region Set up our working cci method
      var workingCciMethod = new Microsoft.Cci.MutableCodeModel.MethodReference();
      #endregion
      #region Set the intern factory
      workingCciMethod.InternFactory = Host.InternFactory;
      #endregion
      #region Get calling conventions
      workingCciMethod.CallingConvention = CSharpToCCIHelper.GetCallingConventionFor(semanticMethod);
      #endregion
      #region Get containing type reference
      ITypeReference containingType;
      if (!TryGetTypeReference(semanticMethod.ContainingType, out containingType))
        goto ReturnFalse;
      workingCciMethod.ContainingType = containingType;
      #endregion
      #region Get return type reference
      if (semanticMethod.IsConstructor())
        workingCciMethod.Type = this.Host.PlatformType.SystemVoid;
      else {
        ITypeReference returnType;
        if (!TryGetTypeReference(semanticMethod.ReturnType(), out returnType))
          goto ReturnFalse;
        workingCciMethod.Type = returnType;
      }
      #endregion
      #region Get name
      if (!semanticMethod.IsConstructor() && semanticMethod.Name == null) goto ReturnFalse;
      workingCciMethod.Name = Host.NameTable.GetNameFor(semanticMethod.IsConstructor()?".ctor":semanticMethod.Name);
      #endregion
      #region Get generic param count
      if (semanticMethod.TypeParameters().IsDefault)
        workingCciMethod.GenericParameterCount = 0;
      else
        workingCciMethod.GenericParameterCount = (ushort)semanticMethod.TypeParameters().Length;
      #endregion
      #region Get parameter references
      List<IParameterTypeInformation> cciParameters;
      if (semanticMethod.Parameters().IsDefault) goto ReturnFalse;
      Contract.Assume(semanticMethod.Parameters().Length <= ushort.MaxValue, "Should be a postcondition?");
      if (!TryGetParametersList(semanticMethod.Parameters(), out cciParameters))
        goto ReturnFalse;
      workingCciMethod.Parameters = cciParameters;
      #endregion
      #region Get the assembly reference (this also ensures the assembly gets loaded properly)
      IAssemblyReference assemblyReference;
      TryGetAssemblyReference(semanticMethod.ContainingAssembly, out assemblyReference);
      #endregion
      cciMethod = workingCciMethod;
      return true;
      #region ReturnFalse:
    ReturnFalse:
      cciMethod = Dummy.MethodReference;
      if (ContractsPackageAccessor.Current.VSOptionsPage.Caching)
        _semanticMembersToCCIMethods[semanticMethod] = cciMethod;
      return false;
      #endregion
    }
    public bool TryGetNamespaceReference(CSharpNamespace semanticNamespace, IAssemblyReference cciAssembly, out IUnitNamespaceReference cciNamespace) {
      Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out cciNamespace) != null));

      cciNamespace = null;

      #region Check input
      if (semanticNamespace == null || cciAssembly == null) {
        return false;
      }
      #endregion
      #region If root
      try {
        if (semanticNamespace.IsGlobalNamespace) {
          cciNamespace = new Microsoft.Cci.MutableCodeModel.RootUnitNamespaceReference() { Unit = cciAssembly };
          goto ReturnTrue;
        }
      } catch (InvalidOperationException e) { //For some reason, an InvalidOperationException may get thrown.
        if (e.Message.Contains(ContractsPackageAccessor.InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate))
          goto ReturnFalse;
        else
          throw e;
      }
      #endregion
      #region If nested
      if (semanticNamespace.ContainingNamespace != null) {
        IUnitNamespaceReference parentNs;
        if (!TryGetNamespaceReference(semanticNamespace.ContainingNamespace, cciAssembly, out parentNs))
          goto ReturnFalse;
        if (semanticNamespace.Name == null) goto ReturnFalse;
        cciNamespace = new Microsoft.Cci.Immutable.NestedUnitNamespaceReference(parentNs, Host.NameTable.GetNameFor(semanticNamespace.Name));
        goto ReturnTrue;
      }
      #endregion
      Contract.Assert(cciNamespace == null);
      goto ReturnFalse;

      #region ReturnTrue:
    ReturnTrue:
      return true;
      #endregion
      #region ReturnFalse:
    ReturnFalse:
      return false;
      #endregion
    }
    public bool TryGetParametersList(IList<CSharpParameter> semanticParameters, out List<IParameterTypeInformation> cciParameters, ushort indexOffset = 0) {
      Contract.Requires(semanticParameters != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out cciParameters) != null);
      cciParameters = null;

      #region Check input
      if (semanticParameters == null || semanticParameters.Count + indexOffset > ushort.MaxValue) {
        return false;
      }
      #endregion
      #region Get parameter count
      var paramCount = semanticParameters.Count;
      #endregion
      #region Initialize our parameter list
      cciParameters = new List<IParameterTypeInformation>(paramCount);
      #endregion
      #region Populate the parameter list
      for (ushort i = 0; i < paramCount; i++) {
        var semanticParam = semanticParameters[i];
        Contract.Assume(semanticParam != null);
        IParameterTypeInformation cciParam;
        if (!TryGetParameterReference(semanticParam, (ushort)(i + indexOffset), out cciParam))
          goto ReturnFalse;
        cciParameters.Add(cciParam);
      }
      #endregion
      return true;
      #region ReturnFalse:
    ReturnFalse:
      return false;
      #endregion
    }
    public bool TryGetParameterReference(CSharpParameter semanticParameter, ushort index, out IParameterTypeInformation cciParameter) {

      cciParameter = null;

      #region Check input
      if (semanticParameter == null) {
        return false;
      }
      #endregion
      #region Set up our working cci parameter
      Microsoft.Cci.MutableCodeModel.ParameterTypeInformation workingParameter = null;
      cciParameter = workingParameter = new Microsoft.Cci.MutableCodeModel.ParameterTypeInformation();
      #endregion
      #region Get our parameter type
      ITypeReference paramType;
      if (!TryGetTypeReference(semanticParameter.Type, out paramType))
        goto ReturnFalse;
      workingParameter.Type = paramType;
      #endregion
      #region Get our index
      workingParameter.Index = index;
      #endregion
      #region Get our reference status
      workingParameter.IsByReference = semanticParameter.RefKind == RefKind.Out || semanticParameter.RefKind == RefKind.Ref;
      #endregion
      #region ReturnTrue:
    //ReturnTrue:
      return cciParameter != Dummy.ParameterTypeInformation;
      #endregion
      #region ReturnFalse:
    ReturnFalse:
      if (semanticParameter.Name != null)
      {
        ContractsPackageAccessor.Current.Logger.WriteToLog("Failed to build parameter reference for: " + semanticParameter.Name);
      }
      return false;
      #endregion
    }
    public bool TryGetPropertyAccessorReferences(CSharpMember semanticProperty, out IMethodReference getter, out IMethodReference setter) {
      Contract.Requires(semanticProperty == null || semanticProperty.Kind == SymbolKind.Property);

      getter = setter = null;

      #region Check input
      if (semanticProperty == null) {
        return false;
      }
      #endregion
      #region Check cache
      Tuple<IMethodReference, IMethodReference> cacheOutput;
      if (_semanticPropertiesToCCIAccessorMethods.TryGetValue(semanticProperty, out cacheOutput)) {
        Contract.Assume(cacheOutput != null, "Non-null only dictionary");
        getter = cacheOutput.Item1;
        setter = cacheOutput.Item2;
        return (getter != null && getter != Dummy.MethodReference) || (setter != null && setter != Dummy.MethodReference);
      }
      #endregion
      #region Get calling conventions
      var callingConventions = CSharpToCCIHelper.GetCallingConventionFor(semanticProperty);
      #endregion
      #region get containing type
      ITypeReference containingType;
      if (!TryGetTypeReference(semanticProperty.ContainingType, out containingType))
        goto ReturnFalse;
      #endregion
      #region Get return type
      ITypeReference returnType;
      if (!TryGetTypeReference(semanticProperty.ReturnType(), out returnType))
        goto ReturnFalse;
      #endregion
      #region Get the property's name
      string propertyName;
      if (semanticProperty.IsIndexer())
        propertyName = "Item";
      else
      {
        if (semanticProperty.Name == null) goto ReturnFalse;
        propertyName = semanticProperty.Name;
      }
      #endregion
      #region Get the parameters
      List<IParameterTypeInformation> getterParams;
      if (!semanticProperty.Parameters().IsDefault) {
        if (!TryGetParametersList(semanticProperty.Parameters(), out getterParams))
          goto ReturnFalse;
      } else
        getterParams = new List<IParameterTypeInformation>();

      List<IParameterTypeInformation> setterParams;
      if (!semanticProperty.Parameters().IsDefault) {
        if (!TryGetParametersList(semanticProperty.Parameters(), out setterParams, 1))
          goto ReturnFalse;
        #region Append the "value" param
        var valParam = new Microsoft.Cci.MutableCodeModel.ParameterTypeInformation() {
          Type = returnType,
          Index = 0,
          IsByReference = false
        };
        setterParams.Insert(0, valParam);
        #endregion
      } else
        setterParams = new List<IParameterTypeInformation>();
      #endregion
      #region Build the getter
      getter = new Microsoft.Cci.MutableCodeModel.MethodReference() {
        InternFactory = Host.InternFactory,
        CallingConvention = callingConventions,
        ContainingType = containingType,
        Type = returnType,
        Name = Host.NameTable.GetNameFor("get_" + propertyName),
        GenericParameterCount = 0,
        Parameters = getterParams
      };
      #endregion
      #region Build the setter
      setter = new Microsoft.Cci.MutableCodeModel.MethodReference() {
        InternFactory = Host.InternFactory,
        CallingConvention = callingConventions,
        ContainingType = containingType,
        Type = Host.PlatformType.SystemVoid,
        Name = Host.NameTable.GetNameFor("set_" + propertyName),
        GenericParameterCount = 0,
        Parameters = setterParams
      };
      #endregion
      #region Get the assembly reference (this also ensures the assembly gets loaded properly)
      IAssemblyReference assemblyReference;
      TryGetAssemblyReference(semanticProperty.ContainingAssembly, out assemblyReference);
      #endregion
      #region ReturnTrue:
    //ReturnTrue:
      _semanticPropertiesToCCIAccessorMethods[semanticProperty] = new Tuple<IMethodReference, IMethodReference>(getter, setter);
      return true;
      #endregion
      #region ReturnFalse:
    ReturnFalse:
      if (semanticProperty.Name != null)
      {
        ContractsPackageAccessor.Current.Logger.WriteToLog("Failed to build accessor references for: " + semanticProperty.Name);
      }
      _semanticPropertiesToCCIAccessorMethods[semanticProperty] = new Tuple<IMethodReference, IMethodReference>(Dummy.MethodReference, Dummy.MethodReference);
      return false;
      #endregion
    }
    public bool TryGetTypeReference(CSharpType semanticType, IAssemblyReference cciAssembly, out ITypeReference cciType) {
      Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out cciType) != null));

      cciType = null;

      #region Check input
      if (semanticType == null || cciAssembly == null) {
        return false;
      }
      #endregion
      #region Check cache
      if (ContractsPackageAccessor.Current.VSOptionsPage.Caching)
        if (_semanticTypesToCCITypes.TryGetValue(semanticType, out cciType))
          return cciType != null && cciType != Dummy.TypeReference;
      #endregion
      #region If generic
      if (!semanticType.TypeArguments().IsDefault && semanticType.TypeArguments().Length > 0) {
        var genericArguments = new List<ITypeReference>();
        foreach (var semanticTypeArg in semanticType.TypeArguments()) {
          if (semanticTypeArg == null) goto ReturnFalse;
          ITypeReference cciTypeArg = null;
          if (TryGetTypeReference(semanticTypeArg, out cciTypeArg)) {
            genericArguments.Add(cciTypeArg);
          } else {
            goto ReturnFalse;
          }
        }
        ITypeReference genericType = null;
        if (!TryGetTypeReference(semanticType.ContainingType, out genericType)) {
          goto ReturnFalse;
        }
        cciType = new Microsoft.Cci.MutableCodeModel.GenericTypeInstanceReference() {
          InternFactory = this.Host.InternFactory,
          GenericArguments = genericArguments,
          GenericType = (INamedTypeReference) genericType,
        };
        goto ReturnTrue;
      }
      #endregion
      #region If array
      if (semanticType.TypeKind == TypeKind.Array) {
        ITypeReference eleType;
        if (!TryGetTypeReference(semanticType.ElementType(), out eleType))
          goto ReturnFalse;
        if (semanticType.ElementType().TypeKind == TypeKind.Array) {
          Contract.Assume(((IArrayTypeSymbol)semanticType).Rank > 0);
          cciType = new Microsoft.Cci.MutableCodeModel.MatrixTypeReference() {
            ElementType = eleType,
            InternFactory = this.Host.InternFactory,
            Rank = (uint)((IArrayTypeSymbol)semanticType).Rank
          };
          goto ReturnTrue;
        } else {
          cciType = new Microsoft.Cci.MutableCodeModel.VectorTypeReference() {
            ElementType = eleType,
            InternFactory = this.Host.InternFactory,
          };
          goto ReturnTrue;
        }
      }
      #endregion
      #region If type parameter
      if (semanticType.TypeKind == TypeKind.TypeParameter) {
        if (semanticType.ContainingSymbol != null && semanticType.ContainingSymbol.Kind == SymbolKind.Method) {
          cciType = new Microsoft.Cci.MutableCodeModel.GenericMethodParameterReference() {
            Index = (ushort)(!semanticType.ContainingSymbol.TypeParameters().IsDefault ? semanticType.ContainingSymbol.TypeParameters().IndexOf((ITypeParameterSymbol)semanticType) : 0),
            InternFactory = this.Host.InternFactory,
            Name = Host.NameTable.GetNameFor(semanticType.Name != null ? semanticType.Name : "T"),
          };
          goto ReturnTrue;
        } else if (semanticType.ContainingType != null) {
          ITypeReference cciDefiningType;
          if (!TryGetTypeReference(semanticType.ContainingType, out cciDefiningType))
            goto ReturnFalse;
          cciType = new Microsoft.Cci.MutableCodeModel.GenericTypeParameterReference() {
            DefiningType = cciDefiningType,
            Index = (ushort)(semanticType.ContainingType.TypeParameters != null ? semanticType.ContainingType.TypeParameters.IndexOf((ITypeParameterSymbol)semanticType) : 0),
            InternFactory = this.Host.InternFactory,
            Name = Host.NameTable.GetNameFor(semanticType.Name != null ? semanticType.Name : "T"),
          };
          goto ReturnTrue;
        }
      }
      #endregion
      #region If namespace type
      if (semanticType.ContainingType == null)
      {
        IUnitNamespaceReference cciNamespace;
        var namespaceName = semanticType.ContainingNamespace;
        if (namespaceName == null || !TryGetNamespaceReference(namespaceName, cciAssembly, out cciNamespace))
        {
          cciNamespace = new Microsoft.Cci.MutableCodeModel.RootUnitNamespaceReference() { Unit = cciAssembly };
        }
        if (semanticType.ContainingType == null)
        {
          if (semanticType.Name == null || semanticType.Name == null) goto ReturnFalse;
          cciType = new Microsoft.Cci.MutableCodeModel.NamespaceTypeReference()
          {
            ContainingUnitNamespace = cciNamespace,
            GenericParameterCount = (ushort) (semanticType.TypeParameters().IsDefault ? 0 : semanticType.TypeParameters().Length),
            InternFactory = Host.InternFactory,
            IsValueType = semanticType.IsValueType,
            IsEnum = semanticType.TypeKind == TypeKind.Enum,
            Name = Host.NameTable.GetNameFor(semanticType.Name),
            TypeCode = CSharpToCCIHelper.GetPrimitiveTypeCode(semanticType),
          };
          goto ReturnTrue;
        }
      }
      #endregion
      #region If nested type
      if (semanticType.ContainingType != null) {
        ITypeReference containingType;
        if (!TryGetTypeReference(semanticType.ContainingType, cciAssembly, out containingType))
          goto ReturnFalse;
        if (semanticType.Name == null || semanticType.Name == null) goto ReturnFalse;
        cciType = new Microsoft.Cci.MutableCodeModel.NestedTypeReference()
        {
          ContainingType = containingType,
          GenericParameterCount = (ushort)(semanticType.TypeParameters().IsDefault ? 0 : semanticType.TypeParameters().Length),
          InternFactory = this.Host.InternFactory,
          MangleName = true,
          Name = Host.NameTable.GetNameFor(semanticType.Name)
        };
        goto ReturnTrue;
      }
      #endregion
      #region ReturnTrue:
    ReturnTrue:
      if (ContractsPackageAccessor.Current.VSOptionsPage.Caching)
        _semanticTypesToCCITypes[semanticType] = cciType;
      return true;
      #endregion
      #region ReturnFalse:
    ReturnFalse:
      ContractsPackageAccessor.Current.Logger.WriteToLog("Failed to build type reference for: " + (semanticType.Name != null ? semanticType.Name : semanticType.ToString()));
      if (ContractsPackageAccessor.Current.VSOptionsPage.Caching)
        _semanticTypesToCCITypes[semanticType] = Dummy.TypeReference;
      return false;
      #endregion
    }
    public bool TryGetTypeReference(CSharpType semanticType, out ITypeReference cciType) {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out cciType) != null && semanticType != null);

      cciType = null;

      #region Check input
      if (semanticType == null) {
        return false;
      }
      #endregion
      #region Check cache
      if (ContractsPackageAccessor.Current.VSOptionsPage.Caching)
        if (_semanticTypesToCCITypes.TryGetValue(semanticType, out cciType))
          return cciType != null && cciType != Dummy.TypeReference;
      #endregion
      #region Get assembly reference
      IAssemblyReference cciAssembly;
      if (!TryGetAssemblyReference(semanticType.ContainingAssembly, out cciAssembly))
        goto ReturnFalse;
      #endregion
      return TryGetTypeReference(semanticType, cciAssembly, out cciType);
      #region ReturnFalse:
    ReturnFalse:
      if (ContractsPackageAccessor.Current.VSOptionsPage.Caching)
        _semanticTypesToCCITypes[semanticType] = Dummy.TypeReference;
      return false;
      #endregion
    }

    [ContractVerification(true)]
    void EnsureAssemblyIsLoaded(CSharpAssembly semanticAssembly, ref IAssemblyReference assemblyReference) {
      Contract.Ensures(assemblyReference != null || Contract.OldValue(assemblyReference) == null);
      #region Check input
      if (semanticAssembly == null || assemblyReference == null) {
        return;
      }
      #endregion
      
      var assembly = assemblyReference as IAssembly;
      if (assembly == null) {
        assembly = Host.FindAssembly(assemblyReference.AssemblyIdentity);
        if (assembly == Dummy.Assembly) {
          var location = assemblyReference.AssemblyIdentity.Location;
          if (File.Exists(location)) {
            ContractsPackageAccessor.Current.Logger.WriteToLog(String.Format("Calling LoadUnitFrom on assembly '{0}' for future resolution.", location));
            ContractsPackageAccessor.Current.Logger.WriteToLog(String.Format("core assembly: '{0}'", Host.CoreAssemblySymbolicIdentity.ToString()));
            assembly = Host.LoadUnitFrom(location) as IAssembly;
            if (assembly != null){
              assemblyReference = assembly;
              if(ContractsPackageAccessor.Current.VSOptionsPage.Caching)
                _semanticAssemblysToCCIAssemblys[semanticAssembly] = assembly;
            }
            else{
              ContractsPackageAccessor.Current.Logger.WriteToLog("Warning: Found a unit at '" + location + "', but it wasn't an assembly!");
            }
          } else{
            ContractsPackageAccessor.Current.Logger.WriteToLog("Assembly not found at: '" + location + "'. This could be because the assembly hasn't been built yet.");
          }
        } else {
          assemblyReference = assembly;
          if(ContractsPackageAccessor.Current.VSOptionsPage.Caching)
            _semanticAssemblysToCCIAssemblys[semanticAssembly] = assembly;
        }
      }
    }
  }
}