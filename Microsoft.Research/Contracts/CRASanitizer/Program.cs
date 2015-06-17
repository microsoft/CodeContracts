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
using System.Diagnostics;
using System.IO;
using System.Diagnostics.Contracts;

namespace CRASanitizer
{
  class Program
  {
    static int Main(string[] args)
    {
      if (args.Length != 2)
      {
        Usage();
      }
      var contractAssembly = args[0];
      var originalAssembly = args[1];

      var checker = new Checker(Console.Out);

      int result = checker.CheckSurface(contractAssembly, originalAssembly);
      if (result != 0)
      {
        Console.WriteLine("{0} has {1} errors", contractAssembly, result);
      }
      return result;
    }

    private static void Usage()
    {
      Contract.Ensures(false);

      Console.WriteLine("Usage: contract-assembly original-assembly");
      Environment.Exit(-1);
    }

  }

  public class Checker {

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.errorCount >= 0);
    }

    readonly SanitizerHostEnvironment host1;
    readonly SanitizerHostEnvironment host2;
    readonly NameTable nt;
    readonly TextWriter tw;
    int errorCount;

    public Checker(TextWriter tw)
    {
      this.tw = tw;
      this.nt = new NameTable();
      this.host1 = new SanitizerHostEnvironment(nt);
      this.host2 = new SanitizerHostEnvironment(nt);
    }


    public int CheckSurface(string contractAssemblyName, string originalAssemblyName)
    {
      Contract.Requires(originalAssemblyName != null);
      Contract.Requires(contractAssemblyName != null);

      IAssembly/*?*/ contractAssembly = host1.LoadUnitFrom(contractAssemblyName) as IAssembly;
      if (contractAssembly == null || contractAssembly == Dummy.Assembly)
      {
        Error("{0} is not a PE file containing a CLR assembly, or an error occurred when loading it.", contractAssemblyName);
        return Errors;
      }
      IAssembly/*?*/ originalAssembly = host2.LoadUnitFrom(originalAssemblyName) as IAssembly;
      if (originalAssembly == null || originalAssembly == Dummy.Assembly)
      {
        Error("{0} is not a PE file containing a CLR assembly, or an error occurred when loading it.", originalAssemblyName);
        return Errors;
      }
      if (Errors == 0)
      {
        CheckSurface(contractAssembly, originalAssembly);
      }

      return Errors;
    }

    void CheckSurface(IAssembly contractAssembly, IAssembly originalAssembly)
    {
      Contract.Requires(contractAssembly != null);

      // Check each type in contract Assembly
      foreach (var type in contractAssembly.GetAllTypes())
      {
        if (type is INestedTypeDefinition) continue; // visited during parent type
        CheckSurface(type, originalAssembly);
      }
    }

    static bool ExcludeTypeFromComparison(INamedTypeDefinition type, string name)
    {
      if (name == "<Module>") return true;
      if (name.StartsWith("System.Diagnostics.Contracts")) return true;
      if (IsContractClass(type.Attributes)) return true;
      return false;
    }

    private static bool IsContractClass(IEnumerable<ICustomAttribute> attrs)
    {
      Contract.Requires(attrs != null);

      return attrs.Any(attr => TypeHelper.GetTypeName(attr.Type) == "System.Diagnostics.Contracts.ContractClassForAttribute");
    }

    private static bool IsModel(IEnumerable<ICustomAttribute> attrs) {
      Contract.Requires(attrs != null);

      return attrs.Any(attr => TypeHelper.GetTypeName(attr.Type) == "System.Diagnostics.Contracts.ContractModelAttribute");
    }

    private static bool IsCompilerGeneratedType(IEnumerable<ICustomAttribute> attrs) {
      Contract.Requires(attrs != null);
      return attrs.Any(attr => TypeHelper.GetTypeName(attr.Type) == "System.Runtime.CompilerServices.CompilerGeneratedAttribute");
    }

    private void CheckSurface(INamedTypeDefinition type, IAssembly originalAssembly)
    {
      var typeName = TypeHelper.GetTypeName(type, NameFormattingOptions.UseGenericTypeNameSuffix);
      if (ExcludeTypeFromComparison(type, typeName)) return;
      var original = originalAssembly.GetCorresponding(type);
      if (original != null)
      {
        CheckTypeSurface(type, original);
      }
      else
      {
        if (type.IsInterface && !TypeHelper.IsVisibleOutsideAssembly(type))
        {
          // Allow internal interfaces to be in our contract reference assemblies in order to get right virtual bits on some members.
          Warning("Interface {0} only exists in contract reference assembly, but it is internal only, so allowed.", TypeHelper.GetTypeName(type));
        }
        else
        {
          Error("Type {0} is in contract assembly but not in original assembly", typeName);
        }
      }
    }

    void CheckTypeSurface(ITypeDefinition type, INamedTypeDefinition original)
    {
      Contract.Requires(type != null);
      Contract.Requires(original != null);

      var baseClassCount = IteratorHelper.EnumerableCount(type.BaseClasses);
      var originalBaseClassCount = IteratorHelper.EnumerableCount(original.BaseClasses);
      if (baseClassCount != originalBaseClassCount) {
        Error("{0} has a different number of base classes in contract reference assembly.",
          TypeHelper.GetTypeName(type));
      } else if (0 < baseClassCount) {
        var baseType = IteratorHelper.First(type.BaseClasses);
        Contract.Assume(baseType != null);
        var baseTypeName = TypeHelper.GetTypeName(baseType, NameFormattingOptions.UseGenericTypeNameSuffix);
        var originalBaseType = IteratorHelper.First(original.BaseClasses);
        Contract.Assume(originalBaseType != null);
        var originalName = TypeHelper.GetTypeName(originalBaseType, NameFormattingOptions.UseGenericTypeNameSuffix);
        if (!baseTypeName.Equals(originalName)) {
          // Warning instead of Error because existing contract assemblies have lots of violations and it
          // is a pain to chase each and every one down. Causes lots of new files/classes to be added for
          // each base class.
          this.Warning("{0} has a different base class in contract reference assembly. Should be {1} instead of {2}.",
            TypeHelper.GetTypeName(type), originalName, baseTypeName);
        }
      }

      foreach (var member in type.Members)
      {
        CheckSurface(member, original);
      }
    }

    private void CheckSurface(ITypeDefinitionMember member, INamedTypeDefinition original)
    {
      Contract.Requires(original != null);

      // find member in original
      if (member is IFieldDefinition) {
        var field = (IFieldDefinition)member;
        if (!IsCompilerGeneratedType(field.Attributes)) {
          // compiler generated types are closure implementations
          // they are not going to match up.
          CheckSurface(field, original.GetMembersNamed(field.Name, false));
        }
      }
      else if (member is IMethodDefinition) {
        var method = (IMethodDefinition)member;
        if (!(IsCompilerGeneratedType(method.Attributes))){
          // compiler generated types are closure implementations
          // they are not going to match up.
          CheckSurface(method, GetMethod(original, method));
        }
      }
      else if (member is INestedTypeDefinition) {
        var nested = (INestedTypeDefinition)member;
        if (!IsCompilerGeneratedType(nested.Attributes)) {
          // compiler generated types are closure implementations
          // they are not going to match up.
          CheckSurface(nested, original.NestedTypes);
        }
      }
      else if (member is IPropertyDefinition) {
        var nested = (IPropertyDefinition)member;
        CheckSurface(nested, original.GetMembersNamed(nested.Name, false));
      }
      else if (member is IEventDefinition) {
        var nested = (IEventDefinition)member;
        CheckSurface(nested, original.GetMembersNamed(nested.Name, false));
      }
      // ignore other members?
    }

    private void CheckSurface(INestedTypeDefinition nested, IEnumerable<INestedTypeDefinition> nestedTypes)
    {
      Contract.Requires(nested != null);
      Contract.Requires(nestedTypes != null);

      foreach (INamedTypeDefinition candidate in nestedTypes)
      {
        if (candidate.Name.UniqueKey == nested.Name.UniqueKey)
        {
          CheckTypeSurface(nested, candidate);
          return;
        }
      }
      if (nested.IsInterface && !TypeHelper.IsVisibleOutsideAssembly(nested))
      {
        // Allow internal interfaces to be in our contract reference assemblies in order to get right virtual bits on some members.
        Warning("Nested interface {0} only exists in contract reference assembly, but it is internal only, so allowed.", TypeHelper.GetTypeName(nested));
      }
      else
      {
        Error("Nested type {0} only exists in contract reference assembly", TypeHelper.GetTypeName(nested));
      }
    }

    private void CheckSurface(IFieldDefinition field, IEnumerable<ITypeDefinitionMember> fields)
    {
      Contract.Requires(field != null);

      foreach (var c in fields)
      {
        IFieldDefinition candidate = c as IFieldDefinition;
        if (candidate == null) continue;
        // What should we check?
        if (candidate.Visibility != field.Visibility)
        {
          Error("Field {0} has different visibility: contract: {1}, original: {2}", MemberHelper.GetMemberSignature(field, NameFormattingOptions.None),
            field.Visibility, candidate.Visibility);
        }
        if (candidate.IsStatic != field.IsStatic)
        {
          Error("Field {0} has different static bit: contract: {1}, original: {2}", MemberHelper.GetMemberSignature(field, NameFormattingOptions.None),
            field.IsStatic, candidate.IsStatic);
        }
        if (candidate.IsCompileTimeConstant != field.IsCompileTimeConstant)
        {
            Error("Field {0} has different const bit: contract: {1}, original: {2} (compile-time constant has value: {3})",
                MemberHelper.GetMemberSignature(field, NameFormattingOptions.None),
                field.IsCompileTimeConstant,
                candidate.IsCompileTimeConstant,
                (candidate.IsCompileTimeConstant ? candidate.CompileTimeValue.Value.ToString() : field.CompileTimeValue.Value.ToString())
               );
        }
        else if (candidate.IsCompileTimeConstant) // then both are
        {
            if (!candidate.CompileTimeValue.Value.Equals(field.CompileTimeValue.Value))
            {
                Warning("Field {0} has different values for const bit: contract: {1}, original: {2}",
                    MemberHelper.GetMemberSignature(field, NameFormattingOptions.None),
                    field.CompileTimeValue.Value.ToString(),
                    candidate.CompileTimeValue.Value.ToString()
                   );
            }
        }

        return; // found
      }
      if (IsModel(field.Attributes)) return;

      Error("Field {0} only appears in contract reference assembly", MemberHelper.GetMemberSignature(field, NameFormattingOptions.None));
    }

    private void CheckSurface(IPropertyDefinition property, IEnumerable<ITypeDefinitionMember> properties) {
      Contract.Requires(property != null);
      Contract.Requires(properties != null);
      
      foreach (var c in properties) {
        var candidate = c as IPropertyDefinition;
        if (candidate == null) continue;
        // What should we check?
        if (candidate.Visibility != property.Visibility) {
          Error("Property {0} has different visibility: contract: {1}, original: {2}", MemberHelper.GetMemberSignature(property, NameFormattingOptions.None),
            property.Visibility, candidate.Visibility);
        }
        return; // found
      }
      if (IsModel(property.Attributes)) return;

      if (property.Getter != null && IsImplementingInvisibleInterface(property.Getter.ResolvedMethod) ||
          property.Setter != null && IsImplementingInvisibleInterface(property.Setter.ResolvedMethod))
      {
        Warning("Property {0} only appears in contract reference assembly, but is invisible, so okay", MemberHelper.GetMemberSignature(property, NameFormattingOptions.None));
      }
      else
      {
        Error("Property {0} only appears in contract reference assembly", MemberHelper.GetMemberSignature(property, NameFormattingOptions.None));
      }
    }

    private bool IsImplementingInvisibleInterface(IMethodDefinition method)
    {
      if (MemberHelper.IsVisibleOutsideAssembly(method)) return false;

      foreach (var bm in MemberHelper.GetExplicitlyOverriddenMethods(method))
      {
        var containing = bm.ContainingType;
        var unitref = TypeHelper.GetDefiningUnitReference(containing);
        if (UnitHelper.UnitsAreEquivalent(unitref, TypeHelper.GetDefiningUnitReference(method.ContainingType)))
        {

          var instance = containing as IGenericTypeInstanceReference;
          if (instance != null) { containing = instance.GenericType; }
          var spec = containing as ISpecializedNestedTypeReference;
          if (spec != null) { containing = spec.UnspecializedVersion; }
          var resolved = containing.ResolvedType;
          if (resolved.IsInterface && !TypeHelper.IsVisibleOutsideAssembly(resolved))
          {
            return true;
          }

        }
      }
      return false;
    }

    private void CheckSurface(IEventDefinition Event, IEnumerable<ITypeDefinitionMember> events) {
      Contract.Requires(Event != null);
      Contract.Requires(events != null);

      foreach (var c in events) {
        var candidate = c as IEventDefinition;
        if (candidate == null) continue;
        // What should we check?
        if (candidate.Visibility != Event.Visibility) {
          Error("Event {0} has different visibility: contract: {1}, original: {2}", MemberHelper.GetMemberSignature(Event, NameFormattingOptions.None),
            Event.Visibility, candidate.Visibility);
        }
        return; // found
      }
      if (Event.Adder != null && (IsImplementingInvisibleInterface(Event.Adder.ResolvedMethod) || Event.Adder.ResolvedMethod.Visibility == TypeMemberVisibility.Private) ||
          Event.Remover != null && (IsImplementingInvisibleInterface(Event.Remover.ResolvedMethod) || Event.Adder.ResolvedMethod.Visibility == TypeMemberVisibility.Private))
      {
        Warning("Event {0} only appears in contract reference assembly, but is invisible, so okay", MemberHelper.GetMemberSignature(Event, NameFormattingOptions.None));
      }
      else
      {
        Error("Event {0} only appears in contract reference assembly", MemberHelper.GetMemberSignature(Event, NameFormattingOptions.None));
      }
    }


    private static IMethodDefinition GetMethod(INamedTypeDefinition original, IMethodDefinition method)
    {
      foreach (var candidate in original.GetMembersNamed(method.Name, false))
      {
        IMethodDefinition candidateMethod = candidate as IMethodDefinition;
        if (candidateMethod == null) continue;
        if (candidateMethod.GenericParameterCount != method.GenericParameterCount) continue;
        if (candidateMethod.ParameterCount != method.ParameterCount) continue;
        if (!ParameterTypesMatch(candidateMethod.Parameters, method.Parameters)) continue;
        if (!TypesMatchSyntactically(candidateMethod.Type, method.Type)) continue;
        return candidateMethod;
      }
      return null;
    }

    private static bool ParameterTypesMatch(IEnumerable<IParameterDefinition> pars1, IEnumerable<IParameterDefinition> pars2)
    {
      var pars2it = pars2.GetEnumerator();
      foreach (var p1 in pars1)
      {
        pars2it.MoveNext(); // must succeed (same count);
        var p2 = pars2it.Current;
        if (p1.IsByReference != p2.IsByReference) return false;
        if (!TypesMatchSyntactically(p1.Type, p2.Type)) return false;
      }
      return true;
    }

    private static bool TypesMatchSyntactically(ITypeReference tref1, ITypeReference tref2)
    {
      return (TypeHelper.GetTypeName(tref1) == TypeHelper.GetTypeName(tref2));
    }

    private void CheckSurface(IMethodDefinition method, IMethodDefinition original)
    {
      Contract.Requires(method != null);

      if (original == null || original == Dummy.Method)
      {
        // special case for generated default constructors.
        if (method.IsConstructor && method.ParameterCount == 0)
        {
          return;
        }
        if (IsModel(method)) return;

        if (IsImplementingInvisibleInterface(method))
        {
          Warning("Method {0} exists only in contract reference assembly, but is hidden, so okay", MemberHelper.GetMethodSignature(method, NameFormattingOptions.Signature));
        }
        else
        {
          Error("Method {0} exists only in contract reference assembly", MemberHelper.GetMethodSignature(method, NameFormattingOptions.Signature));
        }
        return;
      }
      // check that both are virtual or not
      if (method.IsVirtual != original.IsVirtual)
      {
        Error("Found method {0} which differs in virtual bit: contract: {1}, original: {2}", MemberHelper.GetMethodSignature(method, NameFormattingOptions.Signature), method.IsVirtual, original.IsVirtual);
      }
      if (method.Visibility != original.Visibility)
      {
        if (method.IsConstructor && method.ParameterCount == 0 && (method.Visibility == TypeMemberVisibility.Assembly || method.Visibility == TypeMemberVisibility.Family))
        {
          // skip. It's hard to match reference assemblies constructor visibility.
        }
        else if (original.Visibility == TypeMemberVisibility.FamilyOrAssembly && method.Visibility == TypeMemberVisibility.Family)
        {
          // skip, removing the internal is fine and sometimes necessary
        }
        else
        {
          Error("Found method {0} which differ in visibility: contract: {1}, original: {2}", MemberHelper.GetMethodSignature(method, NameFormattingOptions.Signature | NameFormattingOptions.UseGenericTypeNameSuffix), method.Visibility, original.Visibility);
        }
      }
      if (method.IsStatic != original.IsStatic)
      {
        Error("Found method {0} which differ in static bit: contract: {1}, original: {2}", MemberHelper.GetMethodSignature(method, NameFormattingOptions.Signature), method.IsStatic, original.IsStatic);
      }
      //if (!TypesMatchSyntactically(method.Type, original.Type)) {
      //  Error("Found method {0} which have different return types: contract: {1}, original: {2}", MemberHelper.GetMethodSignature(method, NameFormattingOptions.Signature), method.Type, original.Type);
      //}
      // TODO more
    }

    private bool IsModel(IMethodDefinition method) {
      Contract.Requires(method != null);

      if (IsModel(method.Attributes)) return true;
      IPropertyDefinition prop;
      if (IsPropertyGetter(method, out prop)) {
        if (IsModel(prop.Attributes)) return true;
      }
      return false;
    }

    public bool IsPropertyGetter(IMethodDefinition method, out IPropertyDefinition prop) {
      Contract.Requires(method != null);

      ITypeDefinition declaringType = method.ContainingTypeDefinition;
      foreach (var p in declaringType.Properties) {
        if (p.Getter != null && p.Getter.InternedKey == method.InternedKey) {
          prop = p;
          return true;
        }
      }
      prop = null;
      return false;
    }


    public int Errors { get { return this.errorCount; } }

    private void Error(string format, params object[] args)
    {
      Contract.Ensures(this.errorCount == Contract.OldValue(this.errorCount) +1);

      this.errorCount++;
      this.tw.Write("Error: ");
      this.tw.WriteLine(format, args);
    }

    private void Warning(string format, params object[] args)
    {
#if EMIT_WARNINGS
      this.tw.Write("Warning: ");
      this.tw.WriteLine(format, args);
#endif
    }


  }


  internal class SanitizerHostEnvironment : MetadataReaderHost
  {
    readonly PeReader peReader;
    internal SanitizerHostEnvironment(NameTable nameTable)
      : base(nameTable, new InternFactory(), 4, null, true)
    {
      this.peReader = new PeReader(this);
    }

    public override IUnit LoadUnitFrom(string location)
    {
      IUnit result = this.peReader.OpenModule(BinaryDocument.GetBinaryDocumentForFile(location, this));
      this.RegisterAsLatest(result);
      return result;
    }

    protected override IPlatformType GetPlatformType()
    {
      return SanitizerPlatformType;
    }
    SanitizerPlatformType _myPlatformType = null;
    public SanitizerPlatformType SanitizerPlatformType
    {
      get
      {
        if (_myPlatformType == null)
          _myPlatformType = new SanitizerPlatformType(this);
        return _myPlatformType;
      }
    }
  }

  internal class SanitizerPlatformType :  Microsoft.Cci.Immutable.PlatformType
  {
    public SanitizerPlatformType(IMetadataHost host) : base(host) { }

    /// <summary>
    /// Returns a reference to the type System.ArgumentException from the core assembly
    /// </summary>
    public INamespaceTypeReference SystemArgumentException
    {
      [DebuggerNonUserCode]
      get
      {
        if (this.systemArgumentException == null)
        {
          this.systemArgumentException = this.CreateReference(this.CoreAssemblyRef, "System", "ArgumentException");
        }
        return this.systemArgumentException;
      }
    }
    INamespaceTypeReference/*?*/ systemArgumentException;

  }

  static class CCI2UnitHelper
  {
    public static INamedTypeDefinition GetCorresponding(this IUnit unit, ITypeDefinition type)
    {
      INamespaceTypeDefinition nstd = type as INamespaceTypeDefinition;
      if (nstd != null) return unit.GetCorresponding(nstd);

      INestedTypeDefinition nested = type as INestedTypeDefinition;
      if (nested != null) return unit.GetCorresponding(nested);

      return null;
    }

    public static INamespaceTypeDefinition GetCorresponding(this IUnit unit, INamespaceTypeDefinition type)
    {
      INamespaceDefinition ns = unit.GetCorresponding(type.ContainingNamespace);
      if (ns == null) return null;
      foreach (var mem in ns.GetMembersNamed(type.Name, true)) {
        INamespaceTypeDefinition candidate = mem as INamespaceTypeDefinition;
        if (candidate == null) continue;
        if (candidate.GenericParameterCount == type.GenericParameterCount) return candidate;
      }
      return null;
    }

    private static INamespaceDefinition GetCorresponding(this IUnit unit, INamespaceDefinition nsd)
    {
      INestedUnitNamespace nested = nsd as INestedUnitNamespace;
      if (nested != null)
      {
        var parent = unit.GetCorresponding(nested.ContainingNamespace);
        if (parent == null) return null;
        foreach (var candidate in parent.GetMembersNamed(nested.Name, false)) {
          INamespaceDefinition result = candidate as INamespaceDefinition;
          if (result != null) return result;
        }
        return null;
      }
      // must be the root
      return unit.UnitNamespaceRoot;
    }

    public static INestedTypeDefinition GetCorresponding(this IUnit unit, INestedTypeDefinition type)
    {
      var parent = unit.GetCorresponding(type.ContainingTypeDefinition);
      if (parent == null) return null;
      foreach (var candidate in parent.GetMembersNamed(type.Name, false))
      {
        INestedTypeDefinition result = candidate as INestedTypeDefinition;
        if (result == null) continue;
        if (result.GenericParameterCount == type.GenericParameterCount) return result;
      }
      return null;
    }

  }
}