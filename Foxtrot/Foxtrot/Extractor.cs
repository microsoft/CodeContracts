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
using System.Text;
using System.Runtime.Serialization; // needed for defining exception .ctors
using System.CodeDom.Compiler; // needed for CompilerError
using System.Diagnostics; // needed for Debug.Assert (etc.)
using System.Compiler;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.DataStructures;

namespace Microsoft.Contracts.Foxtrot {

  public struct Cache<T>
  {
    private T cache;
    private Func<T> compute;
    private bool initialized;

    public Cache(Func<T> compute)
    {
      this.compute = compute;
      this.initialized = true;
      cache = default(T);
    }

    public bool IsInitialized { get { return this.initialized; } }

    public T Value
    {
      get
      {
        if (compute != null)
        {
          cache = compute();
          compute = null;
        }
        return cache;
      }
    }
  }

  [ContractVerification(true)]
  public static class Extractor {

    #region Public API
    /// <summary>
    /// Modifies <paramref name="assembly"/> by extracting any Code Contracts
    /// from the method bodies in the assembly. 
    /// <param name="assembly">
    /// The assembly to which the contracts will be added (i.e., the AST for the assembly
    /// will be enriched with method contracts and object invariants).
    /// </param>
    /// <param name="referenceAssembly">
    /// When not null, then the contracts are extracted from this assembly and then copied over
    /// to <paramref name="assembly"/>. Note that in that case, no contracts are extracted
    /// directly from <paramref name="assembly"/>: all of the contracts in <paramref name="assembly"/>
    /// after this method returns came from this assembly.
    /// </param>
    /// <param name="contracts">
    /// When not null, this will be used for the definitions of the contract methods (and
    /// attributes) in <paramref name="assembly"/> (or <paramref name="referenceAssembly"/>, if that
    /// is not null). When null, then we will make an effort to find the contract methods. In
    /// either case, the definitions might be found within <paramref name="assembly"/> (or
    /// <paramref name="referenceAssembly"/>) anyway.
    /// </param>
    /// <param name="targetContractNodes">
    /// This parameter is used for the definitions of the contract methods (and
    /// attributes) that are the definitions that are embedded within the contracts,
    /// e.g., the definition of the ForAll method are found in the return value.
    /// It may be the same as <paramref name="contracts"/>. If it is passed in as null,
    /// then it will be filled in with the nodes used by the Extractor.
    /// </param>
    /// <param name="useClousotExtractor">
    /// When true, use a "Clousot" extractor, which does some extra processing needed for static
    /// analysis.
    /// </param>
    /// <returns>
    /// True iff extraction was possible. (I.e., a set of contractNodes was found to use for
    /// the extraction --- it does *not* guarantee that any contracts were found in the assembly.)
    /// </returns>
    /// </summary>
    /// 
    public static bool ExtractContracts(AssemblyNode/*!*/ assembly,
      AssemblyNode/*?*/ referenceAssembly,
      ContractNodes/*?*/ contracts,
      ContractNodes/*?*/ backupContracts,
      ContractNodes/*?*/ targetContractNodes,
      out ContractNodes/*?*/ contractNodesUsedToExtract,
      Action<CompilerError>/*?*/ errorHandler,
      bool useClousotExtractor
      ) {

        Contract.Requires(assembly != null);

      AssemblyNode assemblyToVisit = referenceAssembly != null ? referenceAssembly : assembly;

      // Try to use supplied contracts, if present. But don't just try extracting and somehow
      // figuring out if any contracts had been present. Instead, see if:
      //   a) the contract methods are defined in the assembly we are extracting from, or
      //   b) the assembly reference microsoft.contracts.dll (the backup contracts and we found that assembly)
      //   c) the assembly we are extracting from has an external reference to the assembly
      //      the supplied contract methods came from.
      //   d) the contracts found in mscorlib
      //

      // see if the assembly references the backup contracts (Microsoft.Contracts.dll)
      contractNodesUsedToExtract = IdentifyContractAssemblyIfReferenced(backupContracts, assemblyToVisit);

      // see if assembly defines the contracts itself
      if (contractNodesUsedToExtract == null)
      { 
        contractNodesUsedToExtract = ContractNodes.GetContractNodes(assemblyToVisit, errorHandler);
      }
      // see if the assembly references the supplied contract assembly
      if (contractNodesUsedToExtract == null)
      {
        contractNodesUsedToExtract = IdentifyContractAssemblyIfReferenced(contracts, assemblyToVisit);
      }
      // see if the contracts are in the system assembly
      if (contractNodesUsedToExtract == null && assemblyToVisit != SystemTypes.SystemAssembly)
      {
        contractNodesUsedToExtract = ContractNodes.GetContractNodes(SystemTypes.SystemAssembly, errorHandler);
      }
      if (contractNodesUsedToExtract == null) return false;

      var fSharp = false;
      // TODO: Thread the program options through here somehow and let this be specified as an option
      Contract.Assume(assemblyToVisit.Attributes != null);
      foreach (var attr in assemblyToVisit.Attributes) {
        Contract.Assume(attr != null);
        Contract.Assume(attr.Type != null);
        Contract.Assume(attr.Type.Name != null);
        Contract.Assume(attr.Type.Name.Name != null);
        if (attr.Type.Name.Name.Contains("FSharpInterfaceDataVersionAttribute")) {
          fSharp = true;
          break;
        }
      }

      
      var ultimateTarget = (referenceAssembly != null) ? assembly : null;
      Contract.Assert(assembly != null);
      ExtractorVisitor ev = useClousotExtractor
        ? new ClousotExtractor(contractNodesUsedToExtract, ultimateTarget, assembly, errorHandler)
        : new ExtractorVisitor(contractNodesUsedToExtract, ultimateTarget, assembly, false, fSharp);
      ev.Visit(assemblyToVisit);

      if (!useClousotExtractor) {
        FilterForRuntime eoar = new FilterForRuntime(contractNodesUsedToExtract, targetContractNodes);
        assemblyToVisit = eoar.TransformForTarget(assemblyToVisit);
      }

      if (referenceAssembly != null) {
        CopyOutOfBandContracts coob = new CopyOutOfBandContracts(assembly, referenceAssembly, contractNodesUsedToExtract, targetContractNodes);
        coob.VisitAssembly(referenceAssembly);
      }

      return true;
    }

    private static ContractNodes IdentifyContractAssemblyIfReferenced(ContractNodes contracts, AssemblyNode assemblyToVisit)
    {
      Contract.Requires(assemblyToVisit != null);
      if (contracts != null)
      {
        AssemblyNode assemblyContractsLiveIn = contracts.ContractClass == null ? null : contracts.ContractClass.DeclaringModule as AssemblyNode;
        if (assemblyContractsLiveIn != null)
        {
          if (assemblyContractsLiveIn == assemblyToVisit)
          {
            return contracts;
          }
          else
          {
            string nameOfAssemblyContainingContracts = assemblyContractsLiveIn.Name;
            Contract.Assume(assemblyToVisit.AssemblyReferences != null);
            foreach (var ar in assemblyToVisit.AssemblyReferences)
            {
              Contract.Assume(ar != null);

              if (ar.Name == nameOfAssemblyContainingContracts)
              { // just do name matching to avoid loading the referenced assembly
                return contracts;
              }
            }
          }
        }
      }
      return null;
    }
    #endregion Public API
  }

  internal static class ExtractorExtensions {
    public static TypeNode FindShadow(this TypeNode typeNode, AssemblyNode shadowAssembly) {
      if (typeNode.DeclaringType != null) {
        // nested type
        var parent = typeNode.DeclaringType.FindShadow(shadowAssembly);
        if (parent == null) return null;
        return parent.GetNestedType(typeNode.Name);
      } else {
        // namespace type
        return shadowAssembly.GetType(typeNode.Namespace, typeNode.Name);
      }
    }

    public static Method FindShadow(this Method method, AssemblyNode shadowAssembly) {
      var shadowParent = method.DeclaringType.FindShadow(shadowAssembly);
      if (shadowParent == null) return null;
      return shadowParent.FindShadow(method);
    }

    /// <summary>
    /// Find shadow method in given type corresponding to method.
    /// Note: argument types might not match by identity. We have to match them by name.
    /// </summary>
    public static Method FindShadow(this TypeNode parent, Method method) {
      int dummyIndex;
      return FindShadow(parent, method, out dummyIndex);
    }

    /// <summary>
    /// Find shadow field in given type corresponding to field.
    /// </summary>
    public static Field FindShadow(this TypeNode parent, Field field)
    {
      if (field == null || field.Name == null)
      {
        return null;
      }
      MemberList members = parent.GetMembersNamed(field.Name);
      for (int i = 0, n = members == null ? 0 : members.Count; i < n; i++)
      {
        Field f = members[i] as Field;
        if (f != null) return f;
      }
      return null;
    }

    /// <summary>
    /// Find shadow property in given type corresponding to property.
    /// </summary>
    public static Property FindShadow(this TypeNode parent, Property property)
    {
      if (property == null || property.Name == null)
      {
        return null;
      }
      MemberList members = parent.GetMembersNamed(property.Name);
      for (int i = 0, n = members == null ? 0 : members.Count; i < n; i++)
      {
        var p = members[i] as Property;
        if (p == null) continue;
        if (p.Parameters.MatchShadow(property.Parameters))
        {
          return p;
        }

      }
      return null;
    }

    /// <summary>
    /// Find shadow event in given type corresponding to event.
    /// </summary>
    public static Event FindShadow(this TypeNode parent, Event evnt)
    {
      if (evnt == null || evnt.Name == null)
      {
        return null;
      }
      MemberList members = parent.GetMembersNamed(evnt.Name);
      for (int i = 0, n = members == null ? 0 : members.Count; i < n; i++)
      {
        var e = members[i] as Event;
        if (e == null) continue;
        return e;
      }
      return null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="method"></param>
    /// <param name="index">if the result is nonnull, this is the index of the method within
    /// the memberlist returned by parent.GetMembersNamed(method.Name)</param>
    /// <returns></returns>
    public static Method FindShadow(this TypeNode parent, Method method, out int index) {
      if (method == null || method.Name == null) {
        index = 0;
        return null;
      }
      MemberList members = parent.GetMembersNamed(method.Name);
      for (int i = 0, n = members == null ? 0 : members.Count; i < n; i++) {
        Method m = members[i] as Method;
        if (m == null) continue;
        if ((m.TemplateParameters == null) != (method.TemplateParameters == null)) continue;
        if (m.Parameters.MatchShadow(method.Parameters)
          && m.ReturnType.MatchesShadow(method.ReturnType)
          && (((m.TemplateParameters == null) && (method.TemplateParameters == null))
            || (m.TemplateParameters.Count == method.TemplateParameters.Count))) {
          index = i;
          return m;
        }
      }
      index = 0;
      return null;
    }

    public static bool MatchShadow(this ParameterList shadow, ParameterList parameters) {
      ParameterList pars = shadow;
      int n = pars == null ? 0 : pars.Count;
      int m = parameters == null ? 0 : parameters.Count;
      if (n != m) return false;
      if (parameters == null) return true;
      for (int i = 0; i < n; i++) {
        Parameter par1 = pars[i];
        Parameter par2 = parameters[i];
        if (par1 == null || par2 == null) return false;
        if (par1.Type == null || par2.Type == null) return false;
        if (!par1.Type.MatchesShadow(par2.Type)) return false;
      }
      return true;
    }

    public static bool MatchesShadow(this TypeNode type, TypeNode that) {
      if (type == that) return true;
      // recurse structurally. For now, we cheat and just use the string names
      if (type.IsStructurallyEquivalentTo(that)) return true;
      return type.FullName == that.FullName;
    }


  }


  /// <summary>
  /// When a class is used to express the contracts for an interface (or a third-party class)
  /// certain modifications must be made to the code in the contained contracts. For instance,
  /// if the contract class uses implicit interface implementations, then it might have a call
  /// to one of those implementations in a contract, Requires(this.P), for some boolean property
  /// P. That call has to be changed to be a call to the interface method.
  /// 
  /// Note!! This modifies the contract class so that in the rewritten assembly it is defined
  /// differently than it was in the original assembly!!
  /// </summary>
  internal sealed class ScrubContractClass : InspectorIncludingClosures {
    Class contractClass;
    TypeNode abstractClass;
    ExtractorVisitor parent;

    TypeNode OriginalType { get { return this.abstractClass; } }

    public ScrubContractClass(ExtractorVisitor parent, Class contractClass, TypeNode originalType) {
      Contract.Requires(TypeNode.IsCompleteTemplate(contractClass));
      Contract.Requires(TypeNode.IsCompleteTemplate(originalType));
      this.parent = parent;
      this.contractClass = contractClass;
      this.abstractClass = originalType;
    }

    Method currentMethod;

    public override void VisitMethod(Method method)
    {
      this.currentMethod = method;
      this.currentSourceContext = default(SourceContext);
      base.VisitMethod(method);
    }

    SourceContext currentSourceContext;
    public override void VisitStatementList(StatementList statements)
    {
      if (statements == null) return;
      for (int i = 0; i < statements.Count; i++)
      {
        var stmt = statements[i];
        if (stmt == null) continue;
        if (stmt.SourceContext.IsValid) { this.currentSourceContext = stmt.SourceContext; }
        this.Visit(stmt);
      }
    }
    public override void VisitMemberBinding(MemberBinding memberBinding)
    {
      if (memberBinding == null) return;
      base.VisitMemberBinding(memberBinding);

      var member = memberBinding.BoundMember;
      if (member == null) return;

      Contract.Assume(member.DeclaringType != null, "top-level types should not be memberbound");
      var declaringType = HelperMethods.Unspecialize(member.DeclaringType);
      if (declaringType == contractClass)
      {
        // must reroute
        Method method = member as Method;
        if (method != null)
        {
          if (HelperMethods.IsClosureMethod(this.contractClass, HelperMethods.Unspecialize(method))) return;
          var template = method.Template;
          if (method.IsGeneric && template != null)
          {
            Method targetTemplate = HelperMethods.FindImplementedMethodSpecialized(this.OriginalType, template);
            if (targetTemplate != null)
            {
              var target = targetTemplate.GetTemplateInstance(this.OriginalType, method.TemplateArguments);
              memberBinding.BoundMember = target;
              return;
            }
          }
          else
          {
            Method target = HelperMethods.FindImplementedMethodSpecialized(this.OriginalType, method);
            if (target != null)
            {
              memberBinding.BoundMember = target;
              return;
            }
          }
          // couldn't find target: must issue error
          parent.HandleError(this.currentMethod, 1075,
            string.Format("Contract class '{0}' references member '{1}' which is not part of the abstract class/interface being annotated.", this.contractClass.FullName, member.FullName), currentSourceContext);
        }
      }
    }

    public override void VisitField(Field field)
    {
        base.VisitField(field);
        var type = HelperMethods.Unspecialize(field.Type);
        if (type == contractClass)
        {
            if (field.Type.TemplateArguments != null)
            {
                var inst = this.OriginalType.GetTemplateInstance(field.Type, field.Type.TemplateArguments);
                field.Type = inst;
            }
            else
            {
                field.Type = this.OriginalType;
            }
        }
    }

    public override void VisitMethodCall(MethodCall call)
    {
      base.VisitMethodCall(call);
      // patch call to virtual if we patched the method to an interface method
      if (call == null) return;
      if (call.NodeType == NodeType.Callvirt) return;
      var memberBinding = call.Callee as MemberBinding;
      if (memberBinding == null) return;
      Method method = (Method)memberBinding.BoundMember;
      var dt = method.DeclaringType;
      if (dt is Interface) { 
        call.NodeType = NodeType.Callvirt;
      }
    }

  }

  /// <summary>
  /// Scans code and collects all locals into the hashtable Locals,
  /// which can be accessed after each visit. The hashtable is
  /// static, so it is shared by all instances of this class. That
  /// way it functions as an accumulator. But the hashtable is
  /// public so it can be re-initialized if desired.
  /// </summary>
  internal sealed class GatherLocals : Inspector {

    private Local exemptResultLocal;
    public TrivialHashtable Locals = new TrivialHashtable();

    /// <summary>
    /// Use stricter checking if local has a real name (other than "localXXX")
    /// </summary>
    private bool IsLocalExempt(Local local) {
      if (local == exemptResultLocal) return true;
      bool strict = false;
      if (local.Name != null && local.Name.Name != null && !local.Name.Name.StartsWith("local"))
      {
        strict = true;
      }
      var localType = local.Type;
      if (localType == null) return true;
      return HelperMethods.IsCompilerGenerated(localType)
        || local.Name.Name == "_preConditionHolds" // introduced by extractor itself for legacy pre-conditions
        || (strict
             ? (LocalNameIsExempt(local.Name.Name))
             : (HelperMethods.IsDelegateType(localType)
                || HelperMethods.IsTypeParameterType(localType)
                || localType.IsValueType))
        ;
    }

    private static bool LocalNameIsExempt(string localName)
    {
      return localName.StartsWith("CS$") || localName.StartsWith("VB$");
    }
    public override void VisitLocal(Local local) {
      // if the local is a reference to a closure class, then
      // don't mark it because it is sure to be used in both
      // the contracts and the method body
      if (!IsLocalExempt(local) && Locals[local.UniqueKey] == null) {
        //Console.Write("{0} ", local.Name.Name);
        Locals[local.UniqueKey] = local;
      }
       base.VisitLocal(local);
    }

    public override void VisitAssignmentStatement(AssignmentStatement assignment)
    {
      if (assignment.Target is Local && IsResultExpression(assignment.Source))
      {
        exemptResultLocal = (Local)assignment.Target;
      }
      base.VisitAssignmentStatement(assignment);
    }

    private static bool IsResultExpression(Expression expression)
    {
      var call = expression as MethodCall;
      if (call == null) return false;
      MemberBinding mb = call.Callee as MemberBinding;
      if (mb == null) return false;
      Method calledMethod = mb.BoundMember as Method;
      if (calledMethod == null) return false;
      Method template = calledMethod.Template;
      if (template == null) return false;
      if (template.Name != null && template.Name.Name == "Result" && template.DeclaringType != null && template.DeclaringType.Name != null && template.DeclaringType.Name.Name == "Contract")
      {
        return true;
      }
      return false;
    }
  }
  /// <summary>
  /// Scans code and flags an error if any local is found that
  /// is already in the static hashtable GatherLocals.Locals
  /// Each instance of the class starts out with the flag off
  /// and turns it on if a local is found that is in the
  /// hashtable. Create a new instance for each check.
  /// </summary>
  internal class CheckLocals : Inspector {
    internal Local reUseOfExistingLocal = null;
    GatherLocals gatherLocals;
    public CheckLocals(GatherLocals gatherLocals) {
      this.gatherLocals = gatherLocals;
    }
    /// <summary>
    /// check for reused local.
    /// HACK: ignore VB cached boolean locals used in tests. VB does this seemingly for debuggability.
    /// These are always assigned before use in the same scope, so we ignore them.
    /// </summary>
    /// <param name="local"></param>
    /// <returns></returns>
    public override void VisitLocal(Local local) {
      if (gatherLocals.Locals[local.UniqueKey] != null) {
        if (local.Name != null && local.Name.Name.StartsWith("VB$CG$")) {
          // ignore VB compiler generated locals.
        } else {
          reUseOfExistingLocal = local;
        }
      }
      base.VisitLocal(local);
    }
  }

  public sealed class CountPopExpressions : Inspector {
    internal int PopOccurrences;
    public override void VisitExpression(Expression expression) {
      if (expression == null) return;
      if (expression.NodeType == NodeType.Pop)
        PopOccurrences++;
      base.VisitExpression(expression);
    }
    public static int Count(Node node) {
      var counter = new CountPopExpressions();
      counter.Visit(node);
      return counter.PopOccurrences;
    }
  }
  internal sealed class LookForBadStuff : Inspector {
    private ContractNodes contractNodes;
    internal Statement ReturnStatement;
    internal bool BadStuffFound = false;
    internal LookForBadStuff(ContractNodes contractNodes) {
      this.contractNodes = contractNodes;
    }
    public override void VisitReturn(Return Return) {
      this.ReturnStatement = Return;
      base.VisitReturn(Return);
    }
    public override void VisitExpressionStatement(ExpressionStatement statement) {
      Method methodCall = HelperMethods.IsMethodCall(statement);
      if (
        methodCall == this.contractNodes.AssertMethod ||
        methodCall == this.contractNodes.AssertWithMsgMethod ||
        methodCall == this.contractNodes.AssumeMethod ||
        methodCall == this.contractNodes.AssumeWithMsgMethod
        )
        this.BadStuffFound = true;
      base.VisitExpressionStatement(statement);
    }

    internal void CheckForBadStuff(StatementList contractClump)
    {
      this.VisitStatementList(contractClump);
    }
  }
  /// <summary>
  /// Used to deal with legacy if-then-throw requires.
  /// The original structure looks like this:
  /// 
  ///    blocks evaluating the condition and jumping to "success" when
  ///    the pre-condition is true (due to || and &&)
  ///    
  ///    blocks evaluating and throwing the exception
  ///    
  ///    success:
  ///    
  /// We turn this into the following code:
  /// 
  ///    (unchanged blocks evaluating condition and jumping to success)
  ///    
  ///    preconditionHolds = false;
  ///    goto Common;
  ///    
  ///    success:  preConditionHolds=true;
  ///              
  ///    common: preConditionHolds
  /// </summary>
  internal sealed class ReplaceBranchTarget : Inspector {
    Block branchTargetToReplace;
    Block newBranchTarget;
    public ReplaceBranchTarget(Block blockToReplace, Block newTarget) {
      branchTargetToReplace = blockToReplace;
      newBranchTarget = newTarget;
    }
    public override void VisitBranch(Branch branch) {
      if (branch == null || branch.Target == null) return;
      if (branch.Target == branchTargetToReplace) {
        branch.Target = this.newBranchTarget;
        return;
      }
      base.VisitBranch(branch);
    }
  }

  /// <summary>
  /// Used to duplicate contracts from an abbreviation method (validator). It must instantiate 
  /// the contracts to the calling context by duplicating the actual argument expressions into
  /// the contracts where the helper parameters occur.
  /// </summary>
  internal class AbbreviationDuplicator : HelperMethods.DuplicatorForContractsAndClosures
  {
    ExpressionList actuals;
    Expression targetObject;
    Method abbreviation;
    TrivialHashtable localsInActuals;

    public AbbreviationDuplicator(Method sourceMethod, Method targetMethod, ContractNodes contractNodes, Method abbreviation, Expression targetObject, ExpressionList actuals)
      : base(targetMethod.DeclaringType.DeclaringModule, sourceMethod, targetMethod, contractNodes, false)
    {
      this.targetObject = targetObject;
      this.abbreviation = abbreviation;
      this.actuals = actuals;
      this.localsInActuals = new TrivialHashtable();
      PopulateLocalsInActuals();
    }

    private void PopulateLocalsInActuals() {
      var finder = new FindLocalsInActuals(this.localsInActuals);
      finder.VisitExpression(this.targetObject);
      finder.VisitExpressionList(this.actuals);
    }

    private class FindLocalsInActuals : Inspector {
      TrivialHashtable localsFound;

      public FindLocalsInActuals(TrivialHashtable target) {
        this.localsFound = target;
      }
      public override void VisitLocal(Local local) {
        if (local == null) return;
        this.localsFound[local.UniqueKey] = local;
      }
    }
    public override Expression VisitThis(This This)
    {
      if (This.DeclaringMethod == abbreviation) 
      return (Expression)this.Visit(this.targetObject);
      if (This.DeclaringMethod == this.targetMethod) // important lower case. Upper case is Duplicator's target method
        return This;
      return base.VisitThis(This);
    }
    public override Expression VisitParameter(Parameter parameter)
    {
      if (parameter == null) return null;
      if (parameter.DeclaringMethod == abbreviation) {
      var argIndex = parameter.ParameterListIndex;
      if (argIndex >= actuals.Count) throw new InvalidOperationException("bad parameter/actual list");
      return (Expression)this.Visit(actuals[argIndex]);
    }
      if (parameter.DeclaringMethod == this.targetMethod) { // important lower case. Upper case is Duplicator's target method
        return parameter;
      }
      return base.VisitParameter(parameter);
    }
    public override Expression VisitLocal(Local local) {
      if (local == null) return null;
      if (this.localsInActuals[local.UniqueKey] != null) return local; // don't copy
      return base.VisitLocal(local);
    }
    public override Expression VisitUnaryExpression(UnaryExpression unaryExpression)
    {
      var temp = base.VisitUnaryExpression(unaryExpression);
      if (temp.NodeType == NodeType.AddressOf)
      {
        var unary = temp as UnaryExpression;
        if (unary != null)
        {
          if (!IsAddressoffable(unary.Operand.NodeType))
          {
            var newVar = new Local(unary.Operand.Type);
            var sl = new StatementList();
            sl.Add(new AssignmentStatement(newVar, unary.Operand));
            unary.Operand = newVar;
            sl.Add(new ExpressionStatement(unary));
            return new BlockExpression(new Block(sl));
          }
        }
      }
      return temp;
    }

    private static bool IsAddressoffable(NodeType kind)
    {
      switch (kind) {
        case NodeType.Indexer:
        case NodeType.MemberBinding:
        case NodeType.Local:
        case NodeType.Parameter:
          return true;
      }
      return false;
    }
    public override void SafeAddMember(TypeNode targetType, Member duplicatedMember, Member originalMember)
    {
      // don't add these contracts to target assembly yet, as we will copy them again and add them then

      //base.SafeAddMember(targetType, duplicatedMember, originalMember);
    }

    #region Special case substitution on UserMessage of contracts. Regular visitor does not visit this field.
    public override RequiresPlain VisitRequiresPlain(RequiresPlain plain)
    {
      var result = base.VisitRequiresPlain(plain);
      result.UserMessage = (Expression)Visit(result.UserMessage);
      return result;
    }
    public override EnsuresExceptional VisitEnsuresExceptional(EnsuresExceptional exceptional)
    {
      var result = base.VisitEnsuresExceptional(exceptional);
      result.UserMessage = (Expression)Visit(result.UserMessage);
      return result;
    }
    public override EnsuresNormal VisitEnsuresNormal(EnsuresNormal normal)
    {
      var result = base.VisitEnsuresNormal(normal);
      result.UserMessage = (Expression)Visit(result.UserMessage);
      return result;
    }
    #endregion
  }

  internal class ExtractorVisitor : StandardVisitor {

    #region Private Fields
    internal readonly protected ContractNodes contractNodes;
    /// <summary>
    /// Non-null if we are extracting from a X.Contracts reference assembly and X is the ultimate target so 
    /// we can filter types that don't appear in X. This is important at the moment because our target X might
    /// be silverlight, but our contract reference assembly is desktop and contains more stuff. 
    /// </summary>
    readonly protected AssemblyNode ultimateTargetAssembly;
    readonly protected Dictionary<Method, Method> visitedMethods = new Dictionary<Method, Method>();
    private readonly bool verbose = false;
    private readonly VisibilityHelper/*!*/ visibility;
    private bool errorFound = false;
    private SourceContext currentMethodSourceContext;
    readonly private bool fSharp = false;
    private bool isVB = false;
    readonly ExtractionFinalizer extractionFinalizer;


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.visitedMethods != null);
      Contract.Invariant(this.contractNodes != null);
      Contract.Invariant(this.extractionFinalizer != null);
      Contract.Invariant(this.realAssembly != null);
    }

    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// 
    /// </summary>
    /// <param name="contractNodes"></param>
    /// <param name="targetContractNodes"></param>
    /// <param name="ultimateTargetAssembly">specify X if extracting from X.Contracts.dll, otherwise null</param>
    public ExtractorVisitor(ContractNodes/*!*/ contractNodes, AssemblyNode ultimateTargetAssembly, AssemblyNode realAssembly)
      : this(contractNodes, ultimateTargetAssembly, realAssembly, false, false) {
        Contract.Requires(contractNodes != null);
        Contract.Requires(realAssembly != null);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ultimateTargetAssembly">specify X if extracting from X.Contracts.dll, otherwise null</param>
    [ContractVerification(true)]
    public ExtractorVisitor(ContractNodes/*!*/ contractNodes,
                            AssemblyNode ultimateTargetAssembly,
                            AssemblyNode realAssembly, 
                            bool verbose, 
                            bool fSharp) {

      Contract.Requires(contractNodes != null);
      Contract.Requires(realAssembly != null);

      this.contractNodes = contractNodes;
      this.verbose = verbose;
      this.fSharp = fSharp;
      this.visibility= new VisibilityHelper();
      this.errorFound = false;
      this.extractionFinalizer = new ExtractionFinalizer(contractNodes);
      this.ultimateTargetAssembly = ultimateTargetAssembly;
      this.realAssembly = realAssembly;
      this.contractNodes.ErrorFound += delegate(System.CodeDom.Compiler.CompilerError error) {
        // Commented out because the ErrorFound event already had a handler that was printing out a message
        // and so error messages were getting printed out twice
        //if (!error.IsWarning || warningLevel > 0) {
        //  Console.WriteLine(error.ToString());
        //}
        errorFound |= !error.IsWarning;
      };

      this.TaskType = new Cache<TypeNode>(() =>
                        HelperMethods.FindType(realAssembly, Identifier.For("System.Threading.Tasks"), Identifier.For("Task")));
      this.GenericTaskType = new Cache<TypeNode>(() =>
                        HelperMethods.FindType(realAssembly, Identifier.For("System.Threading.Tasks"), Identifier.For("Task" + TargetPlatform.GenericTypeNamesMangleChar + "1")));


    }
    #endregion

    #region Helper Methods
    public void CallErrorFound(CompilerError e) {
      this.contractNodes.CallErrorFound(e);
    }

    public void HandleError(Method method, int errorCode, string s, SourceContext context)
    {
      if (context.IsValid)
      {
        CallErrorFound(new Error(errorCode, s, context));
      }
      else
      {
        CallErrorFound(new Error(errorCode, string.Format("{0}: In method {1}, assembly {2}", s, method.FullName, method.DeclaringType.DeclaringModule.Location), context));
      }
    }

    /// <summary>
    /// Specialized in ClousotExtractor
    /// </summary>
    protected virtual Statement ExtraAssumeFalseOnThrow() { return null; }

    protected virtual bool IncludeModels { get { return false; } }

    // Ensures:
    //  Preconditions != null
    //  && ForAll{Requires r in Preconditions;
    //              r.Condition is BlockExpression
    //              && r.Condition.Type == Void
    //              && IsClump(r.Condition.Block.Statements)
    //  Postconditions != null
    //  && ForAll{Ensures e in Posconditions;
    //              e.PostCondition is BlockExpression
    //              && e.PostCondition.Type == Void
    //              && IsClump(e.PostCondition.Block.Statements)
    //  (In addition, each Requires is a RequiresPlain when the contract
    //   call is Contract.Requires and is a RequiresOtherwise when the
    //   contract call is Critical.Requires. In the latter case, the
    //   ThrowException is filled in correctly.)
    //       
    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    /// <param name="Preconditions"></param>
    /// <param name="Postconditions"></param>
    /// <param name="Validations"></param>
    /// <param name="contractInitializerBlock">used to store extra closure initializations from abbrevs and validators</param>
    public void CheapAndDirty(
      Method method, 
      ref RequiresList Preconditions, 
      ref EnsuresList Postconditions, 
      ref RequiresList Validations, 
      ref EnsuresList modelPostConditions, 
      Block contractInitializerBlock,
      ref HelperMethods.StackDepthTracker dupStackTracker
      ) {

      if (this.verbose) {
        Console.WriteLine("Method : " + method.FullName);
      }

      if (method == null || method.Body == null || method.Body.Statements == null || method.Body.Statements.Count <= 0)
        return;

      Block methodBody = method.Body;
      int n = methodBody.Statements.Count;
      int beginning = 0;

      while (beginning < n && methodBody.Statements[beginning] is PreambleBlock) {
        beginning++;
      }

      int lastBlockContainingContract;
      int lastStatementContainingContract;
      bool anyContractCall = FindLastBlockWithContracts(methodBody.Statements, beginning, out lastBlockContainingContract, out lastStatementContainingContract);

      #region Make sure any locals in the contracts are disjoint from the locals in the rest of the body
      // can use the same one throughout
      GatherLocals gatherLocals = new GatherLocals();
      #endregion
      SourceContext lastContractSourceContext = method.SourceContext;
      if (!anyContractCall)
      {
        if (this.verbose) {
          Console.WriteLine("\tNo contracts found");
        }
        // still need to check for bad other contract calls in method body

        goto CheckBody;
      }
      Block lastBlock = methodBody.Statements[lastBlockContainingContract] as Block;
      lastContractSourceContext = lastBlock.SourceContext; // probably not a good context, what to do if one can't be found?
      if (lastBlock.Statements != null && 0 <= lastStatementContainingContract && lastStatementContainingContract < lastBlock.Statements.Count) {
        lastContractSourceContext = lastBlock.Statements[lastStatementContainingContract].SourceContext;
      }
      #region Make sure contract section is not in any try-catch region
      TrivialHashtable<int> block2Index = new TrivialHashtable<int>(methodBody.Statements.Count);
      for (int i = 0, nn = methodBody.Statements.Count; i < nn; i++) {
        if (methodBody.Statements[i] == null) continue;
        block2Index[methodBody.Statements[i].UniqueKey] = i;
      }
      // Check each exception handler and see if any overlap with the contract section
      for (int i = 0, nn = method.ExceptionHandlers == null ? 0 : method.ExceptionHandlers.Count; i < nn; i++) {
        ExceptionHandler eh = method.ExceptionHandlers[i];
        if (eh == null) continue;
        if (((int)block2Index[eh.BlockAfterTryEnd.UniqueKey]) < beginning || lastBlockContainingContract < ((int)block2Index[eh.TryStartBlock.UniqueKey]))
          continue; // can't overlap
        this.HandleError(method, 1024, "Contract section within try block.", lastContractSourceContext);
        return;

      }
      #endregion

      // Extract <beginning,0> to <lastBlockContainingContract,lastStatmentContainingContract>
      StatementList contractClump = HelperMethods.ExtractClump(methodBody.Statements, beginning, 0, lastBlockContainingContract, lastStatementContainingContract);

      #region Look for bad stuff
      BadStuff(method, contractClump, lastContractSourceContext);
      #endregion Look for bad stuff


      // Make sure that the entire contract section is closed.
      if (!CheckClump(method, gatherLocals, currentMethodSourceContext, new Block(contractClump))) {
        return;
      }
      // Checking that had the side effect of populating the hashtable, but now each contract will be individually visited.
      // That process needs to start with a fresh table.
      gatherLocals.Locals = new TrivialHashtable();

      Preconditions = new RequiresList();
      Postconditions = new EnsuresList();
      Validations = new RequiresList();
      modelPostConditions = new EnsuresList();


      if (!ExtractFromClump(contractClump, method, gatherLocals, Preconditions, Postconditions, Validations, modelPostConditions, lastContractSourceContext, method, contractInitializerBlock, ref dupStackTracker)) {
        return;
      }

    CheckBody:

      #region Check "real" method body for use of any locals used in contracts
      //      var checkMethodBody = new CheckForBadContractStuffInMethodBody(gatherLocals, this.CallErrorFound, method);
      var checkMethodBody = new CheckLocals(gatherLocals);
      checkMethodBody.Visit(methodBody);
      if (!this.fSharp && checkMethodBody.reUseOfExistingLocal != null) {
        SourceContext sc = lastContractSourceContext;
        this.HandleError(method, 1025, "After contract block, found use of local variable '" + checkMethodBody.reUseOfExistingLocal.Name.Name + "' defined in contract block", sc);
      }
      #endregion Check "real" method body for use of any locals used in contracts
    }

    [ContractVerification(true)]
    private bool FindLastBlockWithContracts(StatementList statements, int beginning, out int lastBlockContainingContract, out int lastStatementContainingContract)
    {
      Contract.Requires(statements != null);
      Contract.Requires(beginning >= 0);
      Contract.Ensures(Contract.ValueAtReturn(out lastBlockContainingContract) >= 0 || !Contract.Result<bool>());
      Contract.Ensures(Contract.ValueAtReturn(out lastBlockContainingContract) < statements.Count || !Contract.Result<bool>());
      Contract.Ensures(Contract.ValueAtReturn(out lastStatementContainingContract) >= 0 || !Contract.Result<bool>());

      lastBlockContainingContract = -1;
      lastStatementContainingContract = -1;
      int n = statements.Count;
      for (int i = n - 1; beginning <= i; i--)
      {
        Block b = statements[i] as Block;
        if (b == null || b.Statements == null || b.Statements.Count <= 0) continue;
        for (int j = b.Statements.Count - 1; 0 <= j; j--)
        {
          if (this.contractNodes.IsContractOrValidatorOrAbbreviatorCall(b.Statements[j])) {
            lastBlockContainingContract = i;
            // special case: the nop following the last contract has the same source context as the contract
            // call, let's include it.
            if (j + 1 < b.Statements.Count)
            {
              var nextStmt = b.Statements[j + 1];
              if (nextStmt != null && nextStmt.NodeType == NodeType.Nop)
              {
                // include the nop
                j++;
              }
            }
            lastStatementContainingContract = j;
            return true;
          }
        }
      }
      return false;
    }

    /// <param name="contractInitializer">A block to which closure initialiazation can be added when contracts are copied
    /// from abbreviators and validators</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
    private bool ExtractFromClump(StatementList contractClump, Method method, GatherLocals gatherLocals, RequiresList Preconditions, EnsuresList Postconditions, RequiresList validations, EnsuresList modelPostconditions, SourceContext defaultContext, Method originalMethod, Block contractInitializer, 
      ref HelperMethods.StackDepthTracker dupStackTracker
      ) {
      // set the state so that the contract clump is used for extraction (as opposed to the method body as it used to)
      StatementList stmts = contractClump;
      int beginning = 0;
      int n = stmts.Count;
      int seginning = HelperMethods.FindNextRealStatement(((Block)stmts[beginning]).Statements, 0);

      bool endContractFound = false;
      bool postConditionFound = false;

      SourceContext currentSourceContext;

      for (int i = beginning; i < n; i++) {
        Block b = (Block)stmts[i];
        if (b == null) continue;
        for (int j = 0, m = b.Statements == null ? 0 : b.Statements.Count; j < m; j++) {
          if (dupStackTracker.IsValid && dupStackTracker.Depth >= 0)
          {
            b.Statements[j] = dupStackTracker.Visit(b.Statements[j]);
          }
          Statement s = b.Statements[j];
          if (s == null) continue;
          Block currentClump;
          Throw t = null;
          t = s as Throw;
          Method calledMethod = HelperMethods.IsMethodCall(s);
          if ((t != null ||
                (calledMethod != null &&
                 calledMethod.DeclaringType != null &&
                 calledMethod.DeclaringType != this.contractNodes.ContractClass &&
                 HelperMethods.IsVoidType(calledMethod.ReturnType) &&
                 !this.contractNodes.IsContractOrValidatorOrAbbreviatorMethod(calledMethod)))
            ) {
            #region Treat throw statements as (part of) a precondition

            // don't accept "throw ..." unless it comes in the "precondition section"
            // then treat the current clump as a precondition, but need to massage it a bit:
            // all branches to the block just after the throw should be modified to be branches to
            // a new manufactured block that sets a fresh local to "true". The
            // throw itself should be changed to set the same local to "false". That way the
            // clump can be treated as the value of precondition (because the branch polarity has
            // already been negated as part of the code gen).

            // This test was supposed to be a sanity check that the current block contained
            // only "throw ..." or else "nop; throw ...". But I've also seen "ThrowHelper.Throw(...); nop",
            // so I'm just going to comment this out for now.
            //if (!((m == 1 && j == 0) || (m == 2 && j == 1))) {
            //  Preconditions = new RequiresList();
            //  Postconditions = new EnsuresList();
            //  return; // throw new ExtractorException();
            //}

            Expression exception;

            // The clump being extracted may contain code/blocks that represent (part of)
            // the expression that is being thrown (if the original throw expression had
            // control flow in it from boolean expressions and/or ternary expressions).

            b.Statements[j] = null; // wipe out throw statement

            currentClump = new Block(HelperMethods.ExtractClump(stmts, beginning, seginning, i, j));
            int currentClumpLength = i - beginning + 1;

            // there better be a next block because that must have been the target for all of the branches
            // that didn't cause the throw to happen
            if (!(i < n - 1)) {
              this.HandleError(method, 1027, "Malformed contract.", s.SourceContext);
              return false;
            }
            Block nextBlock = (Block)stmts[i + 1]; // cast succeeds because body is clump
            Local valueOfPrecondition = new Local(Identifier.For("_preConditionHolds"), SystemTypes.Boolean);
            Block preconditionHolds = new Block(new StatementList(new AssignmentStatement(valueOfPrecondition, Literal.True)));
            ReplaceBranchTarget rbt = new ReplaceBranchTarget(nextBlock, preconditionHolds);
            rbt.VisitBlock(currentClump);
            int ILOffset;
            CountPopExpressions cpe = new CountPopExpressions();
            currentSourceContext = s.SourceContext;
            cpe.Visit(s);
            if (0 < cpe.PopOccurrences) {
              // then there is a set of blocks that represent the exception: the Reader
              // was not able to decompile it back into an expression. Extract the set
              // from the current clump and make it into a block expression

              // Find the last block that has a branch to "preconditionHolds". After that are all of the blocks
              // that represent the evaluation of the exception
              int branchBlockIndex = currentClumpLength - 2; // can't be the current block: that has the throw in it
              while (0 <= branchBlockIndex) {
                Block possibleBranchBlock = currentClump.Statements[branchBlockIndex] as Block;
                Branch br = possibleBranchBlock.Statements[possibleBranchBlock.Statements.Count - 1] as Branch;
                if (br != null && br.Target == preconditionHolds) {
                  break;
                }
                branchBlockIndex--;
              }
              if (branchBlockIndex < 0) {
                this.HandleError(method, 1028, "Malformed exception constructor in contract.", defaultContext);
                return false;
              }
              Block exceptionBlock = new Block(HelperMethods.ExtractClump(currentClump.Statements, branchBlockIndex + 1, 0, currentClumpLength - 1, ((Block)currentClump.Statements[currentClumpLength - 1]).Statements.Count - 1));
              exceptionBlock.Statements.Add(new ExpressionStatement(t.Expression));
              SourceContext sctx = ((Block)exceptionBlock.Statements[0]).Statements[0].SourceContext;
              if (sctx.IsValid) {
                currentSourceContext = sctx;
              } else {
                SourceContext tmp;
                bool foundContext = HelperMethods.GetLastSourceContext(exceptionBlock.Statements, out tmp);
                if (foundContext)
                  currentSourceContext = tmp;
              }
              if (!CheckClump(method, gatherLocals, currentSourceContext, exceptionBlock)) return false;
              exception = new BlockExpression(exceptionBlock, SystemTypes.Exception);
              ILOffset = t.ILOffset;
            } else {
              currentSourceContext = s.SourceContext;
              if (t != null) { // then the statement is "throw ..."
                exception = t.Expression;
                ILOffset = t.ILOffset;
              } else {
                ExpressionStatement throwHelperCall = s as ExpressionStatement;
                Debug.Assert(throwHelperCall != null);
                exception = throwHelperCall.Expression;
                ILOffset = s.ILOffset;
              }
              exception.SourceContext = currentSourceContext;
              SourceContext tmp;
              bool foundContext = HelperMethods.GetLastSourceContext(currentClump.Statements, out tmp);
              if (foundContext)
                currentSourceContext = tmp;
            }

            Block returnValueOfPrecondition = new Block(new StatementList(new ExpressionStatement(valueOfPrecondition)));
            Statement extraAssumeFalse = this.ExtraAssumeFalseOnThrow();
            Block preconditionFails = new Block(new StatementList(new AssignmentStatement(valueOfPrecondition, Literal.False), extraAssumeFalse, new Branch(null, returnValueOfPrecondition, true, false, false)));
            //Block preconditionFails = new Block(new StatementList(new AssignmentStatement(valueOfPrecondition, Literal.False), new Branch(null, returnValueOfPrecondition, true, false, false)));
            currentClump.Statements.Add(preconditionFails); // replace throw statement
            currentClump.Statements.Add(preconditionHolds);
            currentClump.Statements.Add(returnValueOfPrecondition);

            if (!CheckClump(originalMethod, gatherLocals, currentSourceContext, currentClump)) return false;

            BlockExpression be = new BlockExpression(currentClump, SystemTypes.Boolean);
            be.SourceContext = currentSourceContext;

            var ro = new RequiresOtherwise(be, exception);
            ro.ILOffset = ILOffset;
            ro.SourceContext = currentSourceContext;
            if (postConditionFound)
            {
              HandleError(originalMethod, 1013, "Precondition found after postcondition.", currentSourceContext);
              return false;
            }
            validations.Add(ro);

              var req = new RequiresPlain(be, FindExceptionThrown.Find(exception));
              req.IsFromValidation = true;
              req.ILOffset = ro.ILOffset;
              req.SourceContext = ro.SourceContext;
              Preconditions.Add(req);
            #endregion
          } else {
            if (contractNodes.IsContractMethod(calledMethod))
            {
              #region Treat calls to contract methods
              if (endContractFound)
              {
                HandleError(originalMethod, 1012, "Contract call found after prior EndContractBlock.", s.SourceContext);
                break;
              }
              if (contractNodes.IsEndContract(calledMethod))
              {
                endContractFound = true;
                continue;
              }

              MethodCall mc = ((ExpressionStatement)s).Expression as MethodCall;
              Expression arg = mc.Operands[0];
              arg.SourceContext = s.SourceContext;
              MethodContractElement mce;
              currentSourceContext = s.SourceContext;
              Expression condition;
              if (beginning == i && seginning == j)
              {
                // Deal with the simple case: the reader decompiled the call into a single statement
                condition = arg;
              }
              else
              {
                b.Statements[j] = new ExpressionStatement(arg);

                // construct a clump from
                // methodBody.Statements[beginning].Statements[seginning] to
                // methodBody.Statements[i].Statements[j]
                currentClump = new Block(HelperMethods.ExtractClump(stmts, beginning, seginning, i, j));
                if (!currentSourceContext.IsValid)
                {
                  // then a good source context has not been found yet. Grovel around in the clump
                  // to see if there is a better one
                  SourceContext sctx;
                  if (HelperMethods.FindContext(currentClump, currentSourceContext, out sctx))
                    currentSourceContext = sctx;
                }
                if (!CheckClump(originalMethod, gatherLocals, currentSourceContext, currentClump)) return false;

                BlockExpression be = new BlockExpression(currentClump);
                condition = be;
              }

              condition.SourceContext = currentSourceContext; 
              if (contractNodes.IsPlainPrecondition(calledMethod))
              {
                var req = new RequiresPlain(condition);
                contractNodes.IsRequiresWithException(calledMethod, out req.ExceptionType);

                mce = req;
              }
              else if (this.contractNodes.IsPostcondition(calledMethod))
              {
                mce = new EnsuresNormal(condition);
              }
              else if (contractNodes.IsExceptionalPostcondition(calledMethod))
              {
                EnsuresExceptional ee = new EnsuresExceptional(condition);
                // Extract the type of exception.
                ee.Type = calledMethod.TemplateArguments[0];
                mce = ee;
              }
              else
              {
                throw new InvalidOperationException("Cannot recognize contract method");
              }

              mce.SourceContext = currentSourceContext;
              mce.ILOffset = mc.ILOffset;
              if (1 < mc.Operands.Count)
              {
                var candidate = SanitizeUserMessage(method, mc.Operands[1], currentSourceContext);
                mce.UserMessage = candidate;
              }
              if (2 < mc.Operands.Count)
              {
                Literal lit = mc.Operands[2] as Literal;
                if (lit != null)
                {
                  mce.SourceConditionText = lit;
                }
              }
              #region determine Model status
              mce.UsesModels = CodeInspector.UsesModel(mce.Assertion, this.contractNodes);
              #endregion

              #region Check context rules
              switch (mce.NodeType)
              {
                case NodeType.RequiresPlain:
                  if (postConditionFound)
                  {
                    this.HandleError(originalMethod, 1014, "Precondition found after postcondition.", currentSourceContext);
                    return false;
                  }
                  if (mce.UsesModels) {
                    this.HandleError(originalMethod, 1073, "Preconditions may not refer to model members.", currentSourceContext);
                    return false;
                  }
                  var rp = (RequiresPlain)mce;
                  Preconditions.Add(rp);
                  validations.Add(rp); // also add to the internal validation list
                  break;
                // TODO: check visibility of post conditions based on visibility of possible implementation
                case NodeType.EnsuresNormal:
                case NodeType.EnsuresExceptional:
                  Ensures ensures = (Ensures)mce;
                  if (mce.UsesModels) {
                    if (this.IncludeModels) {
                      modelPostconditions.Add(ensures);
                    }
                  } else {
                    Postconditions.Add(ensures);
                  }
                  postConditionFound = true;
                  break;
              }
              #endregion

              #endregion
            }
            else if (ContractNodes.IsValidatorMethod(calledMethod))
            {
              #region Treat calls to Contract validators
              if (endContractFound)
              {
                this.HandleError(originalMethod, 1012, "Contract call found after prior EndContractBlock.", s.SourceContext);
                break;
              }

              MethodCall mc = ((ExpressionStatement)s).Expression as MethodCall;
              var memberBinding = (MemberBinding)mc.Callee;
              currentSourceContext = s.SourceContext;
              Statement validation;
              Block validationPrefix;
              if (beginning == i && seginning == j)
              {
                // Deal with the simple case: the reader decompiled the call into a single statement
                validation = s;
                validationPrefix = null;
              }
              else
              {
                // The clump may contain multiple statements ending in the validator call.
                //   to extract the code as Requires<E>, we need to keep the statements preceeding
                //   the validator call, as they may contain local initialization etc. These should go
                //   into the first Requires<E> that the validator expands to. This way, if there are
                //   no Requires<E> expanded from the validator, then the statements can be omitted.
                //   At the same time, the statements won't be duplicated when validations are emitted.
                //
                //   If the validator call contains any pops, then the extraction must fail saying it
                //   is too complicated.

                // must null out statement with call before extract clump
                b.Statements[j] = null; // we have a copy in mc, s
                validationPrefix = new Block(HelperMethods.ExtractClump(stmts, beginning, seginning, i, j));
                if (!currentSourceContext.IsValid)
                {
                  // then a good source context has not been found yet. Grovel around in the clump
                  // to see if there is a better one
                  SourceContext sctx;
                  if (HelperMethods.FindContext(validationPrefix, currentSourceContext, out sctx))
                    currentSourceContext = sctx;
                }
                if (CountPopExpressions.Count(mc) > 0) {
                  this.HandleError(method, 1071, "Arguments to contract validator call are too complicated. Please simplify.", currentSourceContext);
                  return false;
                }
                if (!CheckClump(originalMethod, gatherLocals, currentSourceContext, validationPrefix)) return false;

                validation = new Block(new StatementList(validationPrefix, s));
                validation.SourceContext = currentSourceContext;
              }
              var ro = new RequiresOtherwise(null, new BlockExpression(new Block(new StatementList(validation))));
              validations.Add(ro);
              CopyValidatorContracts(method, calledMethod, memberBinding.TargetObject, mc.Operands, Preconditions, currentSourceContext, validationPrefix);
              #endregion

            }
            else if (ContractNodes.IsAbbreviatorMethod(calledMethod)) {
              #region Treat calls to Contract abbreviators
              if (endContractFound)
              {
                this.HandleError(originalMethod, 1012, "Contract call found after prior EndContractBlock.", s.SourceContext);
                break;
              }

              MethodCall mc = ((ExpressionStatement)s).Expression as MethodCall;
              var memberBinding = (MemberBinding)mc.Callee;
              currentSourceContext = s.SourceContext;
              if (beginning == i && seginning == j)
              {
                // Deal with the simple case: the reader decompiled the call into a single statement

                // nothing to do. All is in the call and its arguments
              }
              else
              {
                // The clump may contain multiple statements ending in the abbreviator call.
                // We need to keep the statements preceeding the abbreviator call and add them to the 
                // contract initializer block. The reason we cannot add them to the first expansion contract
                // of the abbreviator is that the abbreviator may give rise to closure initialization which will
                // be hoisted into the closure initializer block. This closure initializer may refer to the
                // locals initialized by the present statement sequence, so it must precede it. 
                //
                //   If the abbreviator call contains any pops, then the extraction must fail saying it
                //   is too complicated.
                // grab prefix of clump minus last call statement.

                // must null out current call statement before we extract clump (ow. it stays in body)
                b.Statements[j] = null;
                currentClump = new Block(HelperMethods.ExtractClump(stmts, beginning, seginning, i, j));
                if (!currentSourceContext.IsValid)
                {
                  // then a good source context has not been found yet. Grovel around in the clump
                  // to see if there is a better one
                  SourceContext sctx;
                  if (HelperMethods.FindContext(currentClump, currentSourceContext, out sctx))
                    currentSourceContext = sctx;
                }
                if (CountPopExpressions.Count(mc) > 0) {
                  this.HandleError(method, 1070, "Arguments to contract abbreviator call are too complicated. Please simplify.", currentSourceContext);
                  return false;
                }
                if (!CheckClump(originalMethod, gatherLocals, currentSourceContext, currentClump)) return false;
                if (HelperMethods.IsNonTrivial(currentClump)) {
                  contractInitializer.Statements.Add(currentClump);
                }
              }
              CopyAbbreviatorContracts(method, calledMethod, memberBinding.TargetObject, mc.Operands, Preconditions, Postconditions, currentSourceContext, validations, contractInitializer);
              #endregion
            }
            else
            {
              // important to continue here and accumulate blocks/statements for next contract!
              if (i == beginning && j == seginning && s.NodeType == NodeType.Nop)
              {
                // nop following contract is often associated with previous code, so skip it
                seginning = j + 1;
              }
              continue;
            }
          }

          #region Re-initialize current state after contract has been found
          beginning = i;
          seginning = j + 1;
          //seginning = HelperMethods.FindNextRealStatement(((Block)stmts[i]).Statements, j + 1);
          if (seginning < 0) seginning = 0;
          //b = (Block)stmts[i]; // IMPORTANT! Need this to keep "b" in sync
          #endregion Re-initialize current state
        }
      }
      if (this.verbose) {
        Console.WriteLine("\tNumber of Preconditions: " + Preconditions.Count);
        Console.WriteLine("\tNumber of Postconditions: " + Postconditions.Count);
      }
      return true;
    }

    private class FindExceptionThrown : Inspector
    {
      private TypeNode foundExceptionType;
      private FindExceptionThrown() {}

      public override void VisitConstruct(Construct cons)
      {
        if (cons == null) return;
        var memberBinding = cons.Constructor as MemberBinding;
        if (memberBinding != null && memberBinding.BoundMember != null && HelperMethods.DerivesFromException(memberBinding.BoundMember.DeclaringType))
        {
          this.foundExceptionType = memberBinding.BoundMember.DeclaringType;
        }
      }

      public override void VisitExpression(Expression expression)
      {
        if (foundExceptionType != null) return;
        base.VisitExpression(expression);
      }
      public override void VisitBlock(Block block)
      {
        if (foundExceptionType != null) return;
        base.VisitBlock(block);
      }

      public static TypeNode Find(Node expression)
      {
        var visitor = new FindExceptionThrown();
        visitor.Visit(expression);
        if (visitor.foundExceptionType == null)
        {
          return SystemTypes.ArgumentException;
        }
        return visitor.foundExceptionType;
      }
    }


    private void CopyValidatorContracts(Method targetMethod, Method validatorMethodInstance, Expression targetObject, ExpressionList actuals, RequiresList Preconditions, SourceContext useSite, Block validatorPrefix)
    {
      // make sure to have extracted contracts from the validator method prior
      if (validatorMethodInstance.DeclaringType.DeclaringModule == targetMethod.DeclaringType.DeclaringModule)
      {
        var validatorMethod = validatorMethodInstance;
        while (validatorMethod.Template != null) { validatorMethod = validatorMethod.Template; }
        this.VisitMethod(validatorMethod);
      }
      if (validatorMethodInstance.Contract == null) return;

      var copier = new AbbreviationDuplicator(validatorMethodInstance, targetMethod, this.contractNodes, validatorMethodInstance, targetObject, actuals);
      var validatorContract = HelperMethods.DuplicateContractAndClosureParts(copier, targetMethod, validatorMethodInstance, this.contractNodes, false);
      if (validatorContract == null) return;
      MoveValidatorRequires(targetMethod, validatorContract.Requires, Preconditions, useSite, validatorContract.ContractInitializer, validatorPrefix);
    }

    private void CopyAbbreviatorContracts(Method targetMethod, Method abbreviatorMethodInstance, Expression targetObject, ExpressionList actuals, RequiresList Preconditions, EnsuresList Postconditions, SourceContext useSite, RequiresList validations, Block contractInitializer)
    {
      Contract.Requires(validations != null);

      // make sure to have extracted contracts from the abbreviator method prior
      if (abbreviatorMethodInstance.DeclaringType.DeclaringModule == targetMethod.DeclaringType.DeclaringModule)
      {
        var abbrevMethod = abbreviatorMethodInstance;
        while (abbrevMethod.Template != null) { abbrevMethod = abbrevMethod.Template; }
        this.VisitMethod(abbrevMethod);
      }
      if (abbreviatorMethodInstance.Contract == null) return;
      var copier = new AbbreviationDuplicator(abbreviatorMethodInstance, targetMethod, this.contractNodes, abbreviatorMethodInstance, targetObject, actuals);
      var abbrevContract = HelperMethods.DuplicateContractAndClosureParts(copier, targetMethod, abbreviatorMethodInstance, this.contractNodes, true);
      if (abbrevContract == null) return;
      if (HelperMethods.IsNonTrivial(abbrevContract.ContractInitializer)) { contractInitializer.Statements.Add(abbrevContract.ContractInitializer); }
      MoveAbbreviatorRequires(targetMethod, abbrevContract.Requires, Preconditions, validations, useSite);
      MoveAbbreviatorEnsures(targetMethod, abbrevContract.Ensures, Postconditions, useSite);
    }

    private void MoveAbbreviatorEnsures(Method targetMethod, EnsuresList ensuresList, EnsuresList Postconditions, SourceContext useSite){
      if (ensuresList == null) return;
      foreach (var ens in ensuresList)
      {
        if (!ens.DefSite.IsValid)
        {
          ens.DefSite = ens.SourceContext;
        }
        ens.SourceContext = useSite;
        // re-sanitize user message for this context
        ens.UserMessage = SanitizeUserMessage(targetMethod, ens.UserMessage, useSite);
        Postconditions.Add(ens);
      }
    }

    /// <summary>
    /// </summary>
    /// <param name="validationContractInitializer">possibly empty initializer from closures in validators. Must be added as prefix to first requires</param>
    /// <param name="validationPrefix">Possibly empty statement list of code preceeding validator call. Must be added to first
    /// requires.</param>
    private void MoveValidatorRequires(Method targetMethod, RequiresList requiresList, RequiresList Preconditions, SourceContext useSite, Block validationContractInitializer, Block validationPrefix)
    {
      bool isFromValidation = true;

      if (requiresList == null) return;
      foreach (RequiresPlain req in requiresList)
      {
        if (!req.DefSite.IsValid)
        {
          req.DefSite = req.SourceContext;
        }
        req.SourceContext = useSite;
        req.IsFromValidation = isFromValidation; // mark that we copied this from a validator when necessary
        // re-sanitize user message for this context
        req.UserMessage = SanitizeUserMessage(targetMethod, req.UserMessage, useSite);

        // check if first requires and add prefixes
        if (HelperMethods.IsNonTrivial(validationPrefix)) {
          req.Condition = new BlockExpression(new Block(new StatementList(validationPrefix, new ExpressionStatement(req.Condition))));
          validationPrefix = null;
        }
        if (HelperMethods.IsNonTrivial(validationContractInitializer)) {
          req.Condition = new BlockExpression(new Block(new StatementList(validationContractInitializer, new ExpressionStatement(req.Condition))));
          validationContractInitializer = null;
        }
        Preconditions.Add(req);
      }
    }

    private void MoveAbbreviatorRequires(Method targetMethod, RequiresList requiresList, RequiresList Preconditions, RequiresList validations, SourceContext useSite) {
      Contract.Requires(validations != null);

      if (requiresList == null) return;
      foreach (RequiresPlain req in requiresList) {
        if (!req.DefSite.IsValid) {
          req.DefSite = req.SourceContext;
        }
        req.SourceContext = useSite;
        req.IsFromValidation = false;
        // re-sanitize user message for this context
        req.UserMessage = SanitizeUserMessage(targetMethod, req.UserMessage, useSite);
        Preconditions.Add(req);
        validations.Add(req); // we keep everything on both lists so we can emit for validation or standard
      }
    }


    /// <summary>
    /// Only allows literal and static field reference based on visibility
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    private Expression SanitizeUserMessage(Method containing, Expression expression, SourceContext sc)
    {
      if (expression == null) return null;
      Member member;
      var result = SanitizeUserMessageInternal(containing, expression, out member);
      if (result != null) {
        // filter by visibility
        if (member != null && !HelperMethods.IsVisibleFrom(member, containing.DeclaringType)) return null;

        return result;
      }
      this.HandleError(containing, 1065, "User message to contract call can only be string literal, or a static field, or static property that is at least internally visible.", sc);
      return null;
    }

    public static Expression FilterUserMessage(Method targetMethod, Expression expression) {
      if (expression == null) return null;
      Member member;
      var result = SanitizeUserMessageInternal(targetMethod, expression, out member);
      if (result != null) {
        // filter by visibility
        if (member != null && !HelperMethods.IsVisibleFrom(member, targetMethod.DeclaringType)) return null;

        return result;
      }
      return null;
    }

    private static Expression SanitizeUserMessageInternal(Method containing, Expression expression, out Member member)
    {
      member = null;
      Literal l = expression as Literal;
      if (l != null) { return l; }

      // if it is an abbrev, we sanitize later.
      if (ContractNodes.IsAbbreviatorMethod(containing) || ContractNodes.IsValidatorMethod(containing)) return expression;

      MemberBinding b = expression as MemberBinding;
      if (b != null) {
        if (b.TargetObject != null) return null; // instance binding
        member = b.BoundMember;
        return b;
      }

      MethodCall mc = expression as MethodCall;
      if (mc != null)
      {
        if (mc.Operands.Count != 0) return null; //  0-arg
        MemberBinding target = mc.Callee as MemberBinding;
        if (target == null) return null;
        if (target.TargetObject != null) return null; // instance binding
        member = target.BoundMember;
        return mc;
      }
      return null;
    }

    private void BadStuff(Method method, StatementList contractClump, SourceContext defaultSourceContext) {

      LookForBadStuff lfbs = new LookForBadStuff(this.contractNodes);
      lfbs.CheckForBadStuff(contractClump);
      if (lfbs.ReturnStatement != null) {
        SourceContext sctx = lfbs.ReturnStatement.SourceContext;
        if (!sctx.IsValid)
          sctx = defaultSourceContext;
        this.HandleError(method, 1015, "Return statement found in contract section.", sctx);
        return;
      }
      if (lfbs.BadStuffFound) {
        this.HandleError(method, 1016, "Contract.Assert/Contract.Assume cannot be used in contract section. Use only Requires and Ensures.", defaultSourceContext);
        return;
      }
    }

    private bool CheckClump(Method method, GatherLocals gatherLocals, SourceContext sctx, Block clump) {
      if (!HelperMethods.ClumpIsClosed(clump.Statements)) {
        this.contractNodes.CallErrorFound(new Error(1017, "Malformed contract section in method '" + method.FullName + "'", sctx));
        return false;
        //            throw new ExtractorException();
      }

      TypeNode t = method.DeclaringType;
      bool IsContractTypeForSomeOtherType = t == null ? false : HelperMethods.GetTypeFromAttribute(t, ContractNodes.ContractClassForAttributeName) != null;
      // We require that a contract class implement the interface it is holding contracts for. In addition, the methods in that class must be explicit
      // interface implementations. This creates a problem when the contracts need to refer to other methods in the interface. The "this" reference has
      // the wrong type: it needs to be the interface type. Writing an explicit cast before every reference is painful. It is better to allow the user
      // to create a local variable of the interface type and assign it before the contracts. This can't cause a problem with the local being used later
      // in the method body, because methods in contract classes don't have method bodies, just contracts. So don't check for re-use of locals for such
      // methods.
      //
      #region Example of above comment
      //[ContractClass(typeof(ContractForJ))]
      //interface J{
      //  bool M(int x);
      //  [Pure]
      //  bool P { get; }
      //}

      //[ContractClassFor(typeof(J))]
      //class ContractForJ : J{
      //  bool J.M(int x) {
      //    J jThis = this;
      //    Contract.Requires(x != 3);
      //    Contract.Requires(jThis.P);
      //    Contract.Ensures(x != 5 || !jThis.P);
      //    return default(bool);
      //  }
      //  bool J.P {
      //    get {
      //      Contract.Ensures(Contract.Result<bool>());
      //      return default(bool);
      //    }
      //  }
      //}
      #endregion Example of above comment
      if (!IsContractTypeForSomeOtherType) {
        // First, make sure the clump doesn't use any locals that were used
        // in any previous contract, need a new instance each time
        CheckLocals checkLocals = new CheckLocals(gatherLocals);
        checkLocals.Visit(clump);
        if (!this.fSharp && checkLocals.reUseOfExistingLocal != null) {
          this.HandleError(method, 1040, "Reuse of existing local variable '" + checkLocals.reUseOfExistingLocal.Name.Name + "' in contract.", sctx);
          return false;
        }
        // If that test passes, then add in the locals used in the clump into the table of locals that have been used
        gatherLocals.Visit(clump);
      }
      return true;
    }
    private void ExtractIndividualInvariants(TypeNode type, InvariantList result, Method invariantMethod) {

      if (invariantMethod == null || invariantMethod.Body == null || invariantMethod.Body.Statements == null || invariantMethod.Body.Statements.Count == 0)
        return;
      Block invariantMethodBody = invariantMethod.Body;
      int n = invariantMethodBody.Statements.Count;

      int beginning = 0;

      while (beginning < invariantMethodBody.Statements.Count &&
        invariantMethodBody.Statements[beginning] is PreambleBlock) {
        beginning++;
      }

      for (int i = beginning; i < n; i++) {
        Block b = (Block)invariantMethodBody.Statements[i];
        int seginning = 0;
        for (int j = 0, m = b.Statements == null ? 0 : b.Statements.Count; j < m; j++) {
          ExpressionStatement s = b.Statements[j] as ExpressionStatement;
          if (s == null) continue;
          Literal invariantName;
          Literal sourceText;
          Expression invariantCondition = this.contractNodes.IsInvariant(s, out invariantName, out sourceText);
          if (invariantCondition == null) {
            Method called = HelperMethods.IsMethodCall(s);
            if (called != null)
            {
              if (HelperMethods.IsVoidType(called.ReturnType))
              {
                this.CallErrorFound(new Error(1045, "Invariant methods must be a sequence of calls to Contract.Invariant(...)", s.SourceContext));
                return;
              }
            }
            continue;
          }
          // construct a clump from
          // invariantMethodBody.Statements[beginning].Statements[seginning] to
          // invariantMethodBody.Statements[i].Statements[j]
          Block currentClump = new Block(HelperMethods.ExtractClump(invariantMethodBody.Statements, beginning, seginning, i, j));

          BlockExpression be = new BlockExpression(currentClump, SystemTypes.Void);
          Invariant inv = new Invariant(type, be, invariantMethod.Name.Name);
          inv.UserMessage = invariantName;
          inv.SourceConditionText = sourceText;

          SourceContext sctx;
          if (HelperMethods.FindContext(currentClump, s.SourceContext, out sctx)) {
            inv.SourceContext = sctx;
            inv.Condition.SourceContext = sctx;
          }

          inv.ILOffset = s.ILOffset;
          inv.UsesModels = CodeInspector.UsesModel(inv.Condition, this.contractNodes);
          if (inv.UsesModels) {
            this.HandleError(invariantMethod, 1072, "Invariants cannot refer to model members.", sctx);
          } else {
            result.Add(inv);
          }
          #region Re-initialize current state
          beginning = i;
          seginning = j + 1;
          b = (Block)invariantMethodBody.Statements[i]; // IMPORTANT! Need this to keep "b" in sync
          #endregion Assign clump to invariant list
        }
      }

    }

    Cache<TypeNode> TaskType;
    Cache<TypeNode> GenericTaskType;


    /// <summary>
    /// A method is an iterator class is it returns an IEnumerable AND a closure class is created
    /// to implement the enumerator. 
    /// </summary>
    [Pure]
    private bool IsIteratorOrAsyncMethodCandidate(Method method, out bool possiblyAsync)
    {
      possiblyAsync = false;
      TypeNode returnType = method.ReturnType;
      if (returnType == null) return false;
      if (HelperMethods.IsType(returnType, SystemTypes.Void))
      {
        possiblyAsync = true;
        return true; // void async methods
      }
      if (returnType.IsPrimitive) return false;
      if (HelperMethods.IsType(returnType, SystemTypes.IEnumerable) || HelperMethods.IsType(returnType, SystemTypes.IEnumerator)) return true;
      while (returnType.Template != null) returnType = returnType.Template;
      if (HelperMethods.IsType(returnType, SystemTypes.GenericIEnumerable) || HelperMethods.IsType(returnType, SystemTypes.GenericIEnumerator)) return true;
      if (HelperMethods.IsType(returnType, TaskType.Value))
      {
        possiblyAsync = true;
        return true;
      }
      if (HelperMethods.IsType(returnType, GenericTaskType.Value))
      {
        possiblyAsync = true;
        return true;
      }
      return false;
    }

    static TypeNode FindClosureClass(Method original) {
      return (new ClosureClassFinder(original)).ClosureClass;
    }

    class ClosureClassFinder : Inspector {
      Method method;
      string closureTag;
      TypeNode closureClass = null;
      static private TypeNode iAsyncStateMachineType = System.Compiler.SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.CompilerServices"), Identifier.For("IAsyncStateMachine"));

      private TypeNode IAsyncStateMachineType {
        get {
          if (iAsyncStateMachineType == null)
          {
            // pre v4.5
            var module = this.method.DeclaringType.DeclaringModule;
            foreach (var aref in module.AssemblyReferences) {
              if (aref.Name == "System.Threading.Tasks")
              {
                iAsyncStateMachineType = aref.Assembly.GetType(Identifier.For("System.Runtime.CompilerServices"), Identifier.For("IAsyncStateMachine"));
                break;
              }
            }
          }
          return iAsyncStateMachineType;
        }
      }

      public ClosureClassFinder(Method method) {
        this.method = method;
        this.closureTag = "<" + method.Name.Name + ">d_";
      }

      public TypeNode ClosureClass {
        get {
          if (closureClass == null) {
            this.Visit(method);
          }
          return closureClass;
        }
      }

      public override void VisitExpressionStatement(ExpressionStatement estmt)
      {
        Expression source = estmt.Expression;
        Construct sourceConstruct = source as Construct;
        if (sourceConstruct != null)
        {
          if (sourceConstruct.Type != null && sourceConstruct.Type.Name.Name.StartsWith(this.closureTag))
          {
            if (sourceConstruct.Type.Template != null)
            {
              TypeNode template = sourceConstruct.Type.Template;
              while (template.Template != null)
              {
                template = template.Template;
              }
              this.closureClass = (Class)template;
            }
            else
            {
              this.closureClass = (Class)sourceConstruct.Type;
            }
            return;
          }
        }
      }
      public override void VisitAssignmentStatement(AssignmentStatement assignment)
      {
        Expression source = assignment.Source;
        Construct sourceConstruct = source as Construct;
        if (sourceConstruct != null) {
          if (sourceConstruct.Type != null && sourceConstruct.Type.Name.Name.StartsWith(this.closureTag)) {
            if (sourceConstruct.Type.Template != null)
            {
              TypeNode template = sourceConstruct.Type.Template;
              while (template.Template != null)
              {
                template = template.Template;
              }
              this.closureClass = (Class)template;
            }
            else
            {
              this.closureClass = (Class)sourceConstruct.Type;
            }
            return;
          }
        }
        if (IAsyncStateMachineType == null)
        {
          return;
        }
        var mb = assignment.Target as MemberBinding;
        if (mb != null)
        {
          var addrOf = mb.TargetObject as UnaryExpression;
          if (addrOf != null && addrOf.NodeType == NodeType.AddressOf)
          {
            var loc = addrOf.Operand as Local;
            if (loc != null)
            {
              var cand = loc.Type;
              if (HelperMethods.IsCompilerGenerated(cand) && cand.IsAssignableTo(IAsyncStateMachineType))
              {
                this.closureClass = HelperMethods.Unspecialize(cand);
                return;
              }
            }
          }
        }
      }
    }

    [ContractVerification(true)]
    private void ProcessClosureClass(Method method, TypeNode closure, bool isAsync) {
      Contract.Requires(method != null);
      Contract.Requires(closure != null);

      Method movenext = closure.GetMethod(StandardIds.MoveNext);
      if (movenext == null) return;
      movenext.IsAsync = isAsync;
      if (movenext.Body == null) return;
      if (movenext.Body.Statements == null) return;
      SourceContext defaultSourceContext;
      Block contractInitializerBlock = new Block(new StatementList());
      HelperMethods.StackDepthTracker dupStackTracker = new HelperMethods.StackDepthTracker();
      AssumeBlock originalContractPosition;
      StatementList contractClump = GetContractClumpFromMoveNext(method, movenext, contractNodes, contractInitializerBlock.Statements, out defaultSourceContext, ref dupStackTracker, out originalContractPosition);

      if (contractClump != null) {
        #region Look for bad stuff
        BadStuff(method, contractClump, defaultSourceContext);
        #endregion Look for bad stuff

        #region Make sure any locals in the contracts are disjoint from the locals in the rest of the body
        // can use the same one throughout
        GatherLocals gatherLocals = new GatherLocals();
        #endregion

        // Make sure that the entire contract section is closed.
        if (!CheckClump(movenext, gatherLocals, currentMethodSourceContext, new Block(contractClump))) {
          movenext.ClearBody();
          return;
        }
        // Checking that had the side effect of populating the hashtable, but now each contract will be individually visited.
        // That process needs to start with a fresh table.
        gatherLocals.Locals = new TrivialHashtable();
        RequiresList Preconditions = new RequiresList();
        EnsuresList Postconditions = new EnsuresList();
        RequiresList Validations = new RequiresList();
        EnsuresList modelPostconditions = new EnsuresList();
        EnsuresList asyncPostconditions = null;

        // REVIEW: What should we do with the Validations in this case? Should we map them to the enumerator method? Maybe not, since without
        // rewriting this won't happen.
        if (!ExtractFromClump(contractClump, movenext, gatherLocals, Preconditions, Postconditions, Validations, modelPostconditions, defaultSourceContext, method, contractInitializerBlock, ref dupStackTracker)) {
          movenext.ClearBody();
          return;
        }

        if (isAsync)
        {
            asyncPostconditions = SplitAsyncEnsures(ref Postconditions, method);
        }
        try
        {
          // Next is to attach the preconditions to method (instead of movenext)
          // To do so, we have to duplicate the expressions and statements in Precondition, Postcondition and contractInitializerBlock
          Duplicator dup = new Duplicator(closure.DeclaringModule, method.DeclaringType);
          var origPreconditions = Preconditions;
          var origValidations = Validations;
          var origcontractInitializerBlock = contractInitializerBlock;
          Preconditions = dup.VisitRequiresList(Preconditions);
          Postconditions = dup.VisitEnsuresList(Postconditions);
          Validations = dup.VisitRequiresList(Validations);
          contractInitializerBlock = dup.VisitBlock(contractInitializerBlock);
          asyncPostconditions = dup.VisitEnsuresList(asyncPostconditions);

          var mapClosureExpToOriginal = BuildMappingFromClosureToOriginal(closure, movenext, method);
          Preconditions = mapClosureExpToOriginal.Apply(Preconditions);
          Postconditions = mapClosureExpToOriginal.Apply(Postconditions);
          Validations = mapClosureExpToOriginal.Apply(Validations);
          contractInitializerBlock = mapClosureExpToOriginal.Apply(contractInitializerBlock);
          asyncPostconditions = mapClosureExpToOriginal.Apply(asyncPostconditions);

          //MemberList members = FindClosureMembersInContract(closure, movenext);
          // MakeClosureAccessibleToOriginalMethod(closure, members);
          if (method.Contract == null)
            method.Contract = new MethodContract(method);

          method.Contract.Requires = Preconditions;
          method.Contract.Validations = Validations;
          // Postconditions are sanity checked here, because Result<T> must be compared against the
          // return type of the original method. It is most conveniently done after the type substitution. 
          // TODO: refactor the checking part altogether out of ExtractFromClump. 
          method.Contract.Ensures = Postconditions;
          method.Contract.ModelEnsures = modelPostconditions;
          method.Contract.ContractInitializer = contractInitializerBlock;
          method.Contract.AsyncEnsures = asyncPostconditions;

          
          // Following replacement causes some weird issues for complex preconditions (like x != null && x.Length > 0)
          // when CCRewriter is used with /publicsurface or Preconditions only.
          // This fix could be temporal and proper fix would be applied in the future.
          // After discussion this issue with original CC authors (Mike Barnett and Francesco Logozzo),
          // we decided that this fix is safe and lack of Assume statement in the MoveNext method will not affect
          // customers (neither CCRewriter customers now CCCheck customers).
          // If this assumption would not be true in the future, proper fix should be applied.
          // put requires as assumes into movenext method at original position
          ReplaceRequiresWithAssumeInMoveNext(origPreconditions, originalContractPosition);

          // no postPreamble to initialize, as method is not a ctor
        } finally
        {
          // this is done in caller!!!

          //// normalize contract by forcing IsPure to look at attributes and removing contract it is empty
          //var contract = method.Contract;
          //var isPure = contract.IsPure;

          //if (!isPure && contract.RequiresCount == 0 && contract.EnsuresCount == 0 && contract.ModelEnsuresCount == 0 && contract.ValidationsCount == 0 && contract.AsyncEnsuresCount == 0)
          //{
          //  method.Contract = null;
          //} else
          //{
          //  // turn helper method calls to Result, OldValue, ValueAtReturn into proper AST nodes.
          //  this.extractionFinalizer.VisitMethodContract(method.Contract);
          //}
        }
      }
    }

      /// <summary>
      /// Method replaces Requires to Assume in the MoveNext method of the async or iterator state machine.
      /// </summary>
      private void ReplaceRequiresWithAssumeInMoveNext(RequiresList origPreconditions, AssumeBlock originalContractPosition)
      {
          Contract.Assert(origPreconditions != null);
          if (originalContractPosition != null && originalContractPosition.Statements != null
              /*&& origPreconditions != null */&& origPreconditions.Count > 0)
          {
              var origStatements = originalContractPosition.Statements;
              foreach (var pre in origPreconditions)
              {
                  if (pre == null) continue;
                  var assume = new MethodCall(new MemberBinding(null, this.contractNodes.AssumeMethod),
                      new ExpressionList(pre.Condition), NodeType.Call);
                  assume.SourceContext = pre.SourceContext;
                  var assumeStmt = new ExpressionStatement(assume);
                  assumeStmt.SourceContext = pre.SourceContext;
                  origStatements.Add(assumeStmt);
              }
          }
      }

    static Identifier tasknamespace;

    [ContractVerification(true)]
    ///
    /// Share exceptional posts on async list
    /// Move posts meantioning async.Result to async ensures
    /// 
    /// Only do this on method returning a Task !
    /// 
    private EnsuresList SplitAsyncEnsures(ref EnsuresList Postconditions, Method currentMethod)
    {
        Contract.Requires(currentMethod != null);
        Contract.Ensures(Contract.Result<EnsuresList>() != null || Postconditions == null);
        Contract.Ensures(Contract.ValueAtReturn(out Postconditions) != null || Postconditions == null);

        if (Postconditions == null) return null;
        var result = new EnsuresList();
        Contract.Assume(currentMethod.ReturnType != null);
        var taskType = HelperMethods.Unspecialize(currentMethod.ReturnType);
        if (tasknamespace == null)
        {
            tasknamespace = Identifier.For("System.Threading.Tasks");
        }
        if (!taskType.Namespace.Matches(tasknamespace)) return result;
        Contract.Assume(taskType.Name != null);
        if (taskType.Name.Name != "Task" && taskType.Name.Name != "Task`1") return result;

        var actualReturnType = (currentMethod.ReturnType.TemplateArguments == null || currentMethod.ReturnType.TemplateArguments.Count == 0) ? null : currentMethod.ReturnType.TemplateArguments[0];
        var declType = currentMethod.DeclaringType;
        Contract.Assume(declType != null);
        var asyncDup = new AsyncContractDuplicator(declType, declType.DeclaringModule);

        var filtered = new EnsuresList();
        for (int i = 0; i < Postconditions.Count; i++)
        {
            var post = Postconditions[i];
            var epost = post as EnsuresExceptional;
            if (epost != null)
            {
                result.Add(asyncDup.VisitEnsuresExceptional(epost));
                filtered.Add(epost);
            }
            else
            {
                if (AsyncReturnValueQuery.Contains(post, this.contractNodes, currentMethod, actualReturnType))
                {
                    result.Add(post);
                }
                else
                {
                    filtered.Add(post);
                }
            }
        }
        Postconditions = filtered;
        return result;
    }

    class MyMethodBodySpecializer : MethodBodySpecializer {
      public MyMethodBodySpecializer(Module module, TypeNodeList source, TypeNodeList target) 
        : base(module, source, target)
      {
      }
    }

    static MapClosureExpressionToOriginalExpression BuildMappingFromClosureToOriginal(TypeNode ClosureClass, Method MoveNextMethod, Method OriginalMethod) {
      Contract.Ensures(Contract.Result<MapClosureExpressionToOriginalExpression>() != null);

      Dictionary<string, Parameter> closureFieldsMapping = GetClosureFieldsMapping(ClosureClass, OriginalMethod);
      TypeNodeList TPListSource = ClosureClass.ConsolidatedTemplateParameters;
      if (TPListSource == null) TPListSource = new TypeNodeList();
      TypeNodeList TPListTarget = new TypeNodeList();
      if (OriginalMethod.DeclaringType != null && OriginalMethod.DeclaringType.ConsolidatedTemplateParameters!= null) {
        foreach (TypeNode tn in OriginalMethod.DeclaringType.ConsolidatedTemplateParameters)
          TPListTarget.Add(tn);
      }
      if (OriginalMethod.TemplateParameters != null) {
        foreach (TypeNode tn in OriginalMethod.TemplateParameters) {
          TPListTarget.Add(tn);
        }
      }
      Debug.Assert((TPListSource == null && TPListTarget == null) || TPListSource.Count == TPListTarget.Count);
      return new MapClosureExpressionToOriginalExpression(ClosureClass, closureFieldsMapping, TPListSource, TPListTarget, OriginalMethod);
    }

    static Dictionary<string, Parameter> GetClosureFieldsMapping(TypeNode/*!*/ ClosureClass, Method OriginalMethod) {
      Dictionary<string, Parameter> result = new Dictionary<string, Parameter>();
      foreach (Member mem in ClosureClass.Members) {
        Field field = mem as Field;
        if (field != null) {
          Parameter p;
          if (FieldAssociatedWithParameter(field, OriginalMethod, out p)) {
            result.Add(field.Name.Name, p);
          }
        }
      }
      return result;
    }

    static bool FieldAssociatedWithParameter(Field field, Method originalMethod, out Parameter p) {
      string fname = field.Name.Name;

      if (fname.EndsWith("__this")) {
        // check that it is not a nested one
        if (fname.Length > 8 && !fname.Substring(2, fname.Length - 8).Contains("__"))
        {
          p = originalMethod.ThisParameter;
          return true;
        }
      }
      foreach (Parameter par in originalMethod.Parameters) {
        string pname = par.Name.Name;
        if (fname == pname) {
          p = par;
          return true;
        }
      }
      p = null;
      return false;
    }

    class MapClosureExpressionToOriginalExpression : StandardVisitor {
      Dictionary<string, Parameter> closureParametersMapping;
      Dictionary<string, Local> closureLocalsMapping = new Dictionary<string,Local>();
      MethodBodySpecializer specializer;
      Method method;
      TypeNode closureUnspec;

      public MapClosureExpressionToOriginalExpression(TypeNode Closure, Dictionary<string, Parameter> closureParametersMapping,
        TypeNodeList TPListSource, TypeNodeList TPListTarget, Method method) {
        this.closureParametersMapping = closureParametersMapping;
        if (TPListTarget == null || TPListSource == null || TPListSource.Count == 0 || TPListTarget.Count == 0)
          specializer = null;
        else
          specializer = new MyMethodBodySpecializer(Closure.DeclaringModule, TPListSource, TPListTarget);
        this.method = method;
        this.closureUnspec = HelperMethods.Unspecialize(Closure);
      }

      public override Expression VisitMemberBinding(MemberBinding memberBinding) {
        Field field = memberBinding.BoundMember as Field;
        if (field != null) {
          // Case 1: iterator_closure's this.field should be turned into either a parameter or a local.
          if (memberBinding.TargetObject is This) {
            var thisParam = (This)memberBinding.TargetObject;
            var declType = thisParam.Type;
            Reference declRef = declType as Reference;
            if (declRef != null) { declType = declRef.ElementType; }
            declType = HelperMethods.Unspecialize(declType);
            if (declType == this.closureUnspec)
            {
              // actually a closure field.

              if (closureParametersMapping.ContainsKey(field.Name.Name))
              {
                return closureParametersMapping[field.Name.Name];
              }
              if (closureLocalsMapping.ContainsKey(field.Name.Name))
              {
                return closureLocalsMapping[field.Name.Name];
              }
              else
              {
                if (field.Name.Name.Contains("__locals" /* csc.exe */) ||
                    field.Name.Name.Contains("<>8__") /* roslyn-based csc */ ||
                    field.Name.Name.Contains("__spill") /* rcsc.exe */ ||
                    field.Name.Name.Contains("__CachedAnonymousMethodDelegate") /* junk, revisit */
                  )
                {
                  Local newLocal = new Local(field.Name, field.Type);
                  closureLocalsMapping.Add(field.Name.Name, newLocal);
                  return newLocal;
                }
                throw new NotImplementedException(String.Format("Found field {0} in contract that shouldn't be here", field.Name.Name));
              }
            }
            else
            {
              // some other field. Leave it alone.
            }
          } 
        }
        // case 2: assert (memberbinding.targetObject is memberbinding &&  memberbinding.boundmember is capturing 
        // either a parameter or a local). Then targetObject is hopefully turned into a parameter or a local, with
        // the boundmember untouched. We simply use base.VisitMemberBinding for this case. 
        return base.VisitMemberBinding(memberBinding);
      }

      public RequiresList Apply(RequiresList reqs) {
        if (specializer != null) {
          specializer.CurrentMethod = method;
          specializer.CurrentType = method.DeclaringType;
          reqs = specializer.VisitRequiresList(reqs);
        }
        RequiresList result = this.VisitRequiresList(reqs);
        return result;
      }
      public EnsuresList Apply(EnsuresList ens) {
        if (specializer != null) {
          specializer.CurrentMethod = method;
          specializer.CurrentType = method.DeclaringType;
          ens = specializer.VisitEnsuresList(ens);
        }
        EnsuresList result = this.VisitEnsuresList(ens);
        return result;
      }
      public Block Apply(Block block) {
        if (specializer != null) {
          specializer.CurrentMethod = method;
          specializer.CurrentType = method.DeclaringType;
          block = specializer.VisitBlock(block);
        }
        Block result = this.VisitBlock(block);
        return result;
      }
    }

    /// <summary>
    /// Use the same assumption as the extractor for a non-iterator method: the preambles are
    /// in the first block. 
    /// </summary>
    /// <param name="moveNext"></param>
    /// <returns></returns>
    [ContractVerification(true)]
    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    StatementList GetContractClumpFromMoveNext(Method iteratorMethod, Method moveNext, ContractNodes contractNodes, StatementList contractInitializer, out SourceContext defaultSourceContext,
      ref HelperMethods.StackDepthTracker dupStackTracker,
      out AssumeBlock originalContractPosition)
    {
      Contract.Requires(moveNext != null);
      Contract.Requires(moveNext.Body != null);
      Contract.Requires(moveNext.Body.Statements != null);
      Contract.Requires(contractInitializer != null);
      Contract.Requires(iteratorMethod != null);


      var linkerVersion = 0;
      if (iteratorMethod.DeclaringType != null && iteratorMethod.DeclaringType.DeclaringModule != null)
      {
        linkerVersion = iteratorMethod.DeclaringType.DeclaringModule.LinkerMajorVersion;
      }

      var initialState = moveNext.IsAsync ? -1 : 0;
      moveNext.MoveNextStartState = initialState;
      originalContractPosition = null;
      int statementIndex;
      Contract.Assume(moveNext.Body != null);
      Contract.Assume(moveNext.Body.Statements != null);
      int blockIndex = ContractStartInMoveNext(this.contractNodes, moveNext, out statementIndex, iteratorMethod);
      Contract.Assert(statementIndex >= 0, "should follow from the postcondiiton");
      if (blockIndex < 0) {
        // Couldn't find state 0 in MoveNext method
        // This can happen if the iterator is trivial (like yield break; )
        defaultSourceContext = default(SourceContext);
        return null;
      }

      int beginning = blockIndex; // the block number in the body of movenext
      int sbeginning = statementIndex; // the statement no. in the beginnning block after the preamble
      int blast = -1; // the block number in the body of movenext, of the last block where there is a contract call
      int slast = -1; // the statement no. in the blast block

      // Next we move sbeginning past the preamble area
      sbeginning = MovePastPreamble(iteratorMethod, moveNext, beginning, sbeginning, contractInitializer, contractNodes, ref dupStackTracker);
      Contract.Assert(moveNext.Body!= null, "should be provable");
      Contract.Assert(moveNext.Body.Statements != null, "should be provable");
      if (sbeginning < 0 || !this.FindLastBlockWithContracts(moveNext.Body.Statements, beginning, out blast, out slast))
      {
        if (verbose) {
          if (moveNext.Name != null)
          {
            Console.WriteLine("Method {0} doesnt have a contract method invocation at the right place.", moveNext.Name.Name);
          }
        }
        defaultSourceContext = default(SourceContext);
        return null;
      }
      Block methodBody = moveNext.Body;
      Block lastBlock = methodBody.Statements[blast] as Block;
      SourceContext lastContractSourceContext;
      if (lastBlock != null)
      {
        lastContractSourceContext = lastBlock.SourceContext; // probably not a good context, what to do if one can't be found?
        if (lastBlock.Statements != null && 0 <= slast && slast < lastBlock.Statements.Count)
        {
          if (lastBlock.Statements[slast] != null)
          {
            lastContractSourceContext = lastBlock.Statements[slast].SourceContext;
          }
        }
      }
      else
      {
        lastContractSourceContext = default(SourceContext);
      }
      // TODO: check the clump is not in a try-catch block. 

      originalContractPosition = new AssumeBlock(new StatementList());
      StatementList result = HelperMethods.ExtractClump(moveNext.Body.Statements, beginning, sbeginning, blast, slast, 
        assumeBlock: originalContractPosition);
      defaultSourceContext = lastContractSourceContext;
      return result;
    }

    [ContractVerification(true)]
    static int MovePastPreamble([Pure] Method iteratorMethod, [Pure] Method moveNext, int beginning, int currentIndex, StatementList contractInitializer, ContractNodes contractNodes,
      ref HelperMethods.StackDepthTracker dupStackTracker)
    {
      Contract.Requires(moveNext != null);
      Contract.Requires(moveNext.Body != null);
      Contract.Requires(moveNext.Body.Statements != null);
      Contract.Requires(beginning >= 0);
      Contract.Requires(beginning < moveNext.Body.Statements.Count);
      Contract.Requires(currentIndex >= 0);
      Contract.Requires(contractInitializer != null);

      var state0Block = moveNext.Body.Statements[beginning] as Block;
      while (state0Block == null && beginning + 1 < moveNext.Body.Statements.Count) { 
        state0Block = moveNext.Body.Statements[++beginning] as Block;
        if (state0Block != null && (state0Block.Statements == null || state0Block.Statements.Count == 0))
        {
          // skip empty blocks
          state0Block = null;
        }
      }
      if (state0Block == null) return -1;
      if (state0Block.Statements == null) return -1;

#if true
      TypeNode closureType;
      currentIndex = HelperMethods.MovePastClosureInit(iteratorMethod, state0Block, contractNodes, contractInitializer, null, currentIndex, ref dupStackTracker, out closureType);
#else
      int indexForClosureCreationStatement = currentIndex;
      TypeNode closureType = null;
      while (indexForClosureCreationStatement < state0Block.Statements.Count)
      {
        closureType = HelperMethods.IsClosureCreation(iteratorMethod, state0Block.Statements[indexForClosureCreationStatement]);
        if (closureType != null)
        {
          break;
        }
        indexForClosureCreationStatement++;
      }
      if (closureType != null && indexForClosureCreationStatement < state0Block.Statements.Count)
      { // then there is a set of statements to add to the preamble block
        // up to and including "this.<>local := new ClosureClass();"
        for (int i = currentIndex; i <= indexForClosureCreationStatement; i++)
        {
          // preambleBlock.Statements.Add(state0Block.Statements[i]);
          if (state0Block.Statements[i] == null) continue;
          contractInitializer.Add((Statement)state0Block.Statements[i].Clone());
          // state0Block.Statements[i] = null; 
        }
        // Some number of assignment statements of the form "this.local.f := f;" where "f" is a parameter
        // Or of the form local.f := f;
        // that is captured by the closure.
        int endOfAssignmentsToClosureFields = indexForClosureCreationStatement + 1;
        for (; endOfAssignmentsToClosureFields < state0Block.Statements.Count; endOfAssignmentsToClosureFields++)
        {
          Statement s = state0Block.Statements[endOfAssignmentsToClosureFields];
          if (s == null) continue;
          if (s.NodeType == NodeType.Nop) continue;
          AssignmentStatement assign = s as AssignmentStatement;
          if (assign == null) break;
          MemberBinding mb = assign.Target as MemberBinding;
          if (mb == null) break;
          if (mb.TargetObject == null) break;
          if (mb.TargetObject.Type != closureType) break;
        }
        for (int i = indexForClosureCreationStatement + 1; i < endOfAssignmentsToClosureFields; i++)
        {
          // preambleBlock.Statements.Add(state0Block.Statements[i]);
          if (state0Block.Statements[i] == null) continue;
          contractInitializer.Add((Statement)state0Block.Statements[i].Clone());
          // state0Block.Statements[i] = null; // need to null them out so search below can be done starting at beginning of m's body
        }
        currentIndex = endOfAssignmentsToClosureFields;
      }
#endif
      return currentIndex;
    }

    enum EvalKind { None = 0, IsStateValue, IsFinalCompare, IsDisposingTest }

    /// <summary>
    /// Returns -1 if start is not found. Otherwise, returns the block index and statement index.
    /// Works by tracing the actual code assuming this.state == 0 /\ this.disposing == false
    /// We stop the tracing when one of 3 conditions arises:
    ///   - we branched on this.disposing == false. That's the start (4.5)
    ///   - we assigned to the state, next statement is start        (4.0)
    ///   - we saw state compared against 0 or -1 and next statement is not 
    ///       - conditional branch on disposing
    ///       - assignment to state
    ///       - unconditional branch
    ///       
    /// Wrinkle is async methods that are not really async and C# still emits a closure etc, but
    /// the method does not test the async state at all.
    /// </summary>
    [ContractVerification(true)]
    [Pure]
    static int ContractStartInMoveNext(ContractNodes contractNodes, Method moveNext, out int statementIndex, Method origMethod)
    {
      Contract.Requires(contractNodes != null);
      Contract.Requires(moveNext != null);
      Contract.Requires(moveNext.Body != null);
      Contract.Requires(moveNext.Body.Statements != null);
      Contract.Ensures(Contract.ValueAtReturn(out statementIndex) >= 0);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < moveNext.Body.Statements.Count);

      Block body = moveNext.Body;
      bool isAsync = moveNext.IsAsync;
      statementIndex = 0;
      var blocks = body.Statements;

      bool stateIsRead = false;
      var fieldScanner = new HelperMethods.ExpressionScanner((field, isStore) =>
        {
          if (isStore) return;
          if (field != null && field.Name != null && field.Name.Name.Contains("<>") && field.Name.Name.Contains("__state"))
          {
            stateIsRead = true;
          }
        });
      fieldScanner.Visit(body.Statements);

      // Dictionary tracks local variable values and a bit saying if the value is the state (in that case it is 0).
      Dictionary<Variable, Pair<int, EvalKind>> env = new Dictionary<Variable, Pair<int, EvalKind>>();
      int currentBlockIndex = 0;
      bool seenFinalCompare = false;
      bool lastBranchNonConditional = false;

      while (currentBlockIndex >= 0 && currentBlockIndex < blocks.Count)
      {
        var block = blocks[currentBlockIndex] as Block;
        if (block == null || block.Statements == null) continue;
        for (int i = 0; i < block.Statements.Count; i++)
        {
          var stmt = block.Statements[i];
          if (stmt == null) continue;
          if (contractNodes.IsContractOrValidatorOrAbbreviatorCall(stmt)) {
              statementIndex = i;
              return currentBlockIndex;      
          }
          Branch branch = stmt as Branch;
          if (branch != null)
          {
            if (branch.Condition == null)
            {
              currentBlockIndex = FindTargetBlock(blocks, branch.Target, currentBlockIndex);
              lastBranchNonConditional = true;
              goto OuterLoop;
            }
            var value = EvaluateExpression(branch.Condition, env, seenFinalCompare, isAsync);
            if (value.Two == EvalKind.IsDisposingTest)
            {
              if (value.One != 0)
              {
                return FindTargetBlock(blocks, branch.Target, currentBlockIndex);
              }
              else
              {
                if (i + 1 < block.Statements.Count)
                {
                  statementIndex = i + 1;
                  return currentBlockIndex;
                }
                else
                {
                  if (currentBlockIndex + 1 < body.Statements.Count)
                  {
                    return currentBlockIndex + 1;
                  }
                  return -1;
                }
              }
            }
            if (seenFinalCompare)
            {
              // must be the end.
              statementIndex = i;
              return currentBlockIndex;
            }
            seenFinalCompare = (value.Two == EvalKind.IsFinalCompare || value.Two == EvalKind.IsStateValue);

            if (value.One != 0)
            {
              currentBlockIndex = FindTargetBlock(blocks, branch.Target, currentBlockIndex);
              goto OuterLoop;
            }
            else continue;
          }
          if (isAsync && lastBranchNonConditional)
          {
            // not a branch and last one was non-conditional
            // must be the end.
            statementIndex = i;
            return currentBlockIndex;
          }
          var swtch = stmt as SwitchInstruction;
          if (swtch != null) {
            if (seenFinalCompare)
            {
              // must be the end.
              statementIndex = i;
              return currentBlockIndex;
            }
            var value = EvaluateExpression(swtch.Expression, env, seenFinalCompare, isAsync);
            if (value.One < 0 || swtch.Targets == null || value.One >= swtch.Targets.Count)
            {
              // fall through
              if (isAsync)
              {
                seenFinalCompare = true;
              }
              continue;
            }
            currentBlockIndex = FindTargetBlock(blocks, swtch.Targets[value.One], currentBlockIndex);
            // assume seen final compare
            seenFinalCompare = true;
            goto OuterLoop;
          }
          if (HelperMethods.IsClosureCreation(origMethod, stmt) != null)
          {
              // end of trace
              statementIndex = i;
              return currentBlockIndex;
          }
          AssignmentStatement assign = stmt as AssignmentStatement;
          if (assign != null)
          {
              if (assign.Source is ConstructArray)
              {
                  // treat as beginning of contract (typically a params array)
                  statementIndex = i;
                  return currentBlockIndex;
              }
            var value = EvaluateExpression(assign.Source, env, seenFinalCompare, isAsync);
            if (IsThisDotState(assign.Target))
            {
              // end of trace
              if (i + 1 < block.Statements.Count)
              {
                statementIndex = i + 1;
                return currentBlockIndex;
              }
              if (currentBlockIndex + 1 < body.Statements.Count)
              {
                return currentBlockIndex + 1;
              }
              return -1;
            }
            if (IsDoFinallyBodies(assign.Target) && !stateIsRead)
            {
              // end of trace
              if (i + 1 < block.Statements.Count)
              {
                statementIndex = i + 1;
                return currentBlockIndex;
              }
              if (currentBlockIndex + 1 < body.Statements.Count)
              {
                return currentBlockIndex + 1;
              }
              return -1;
            }
            var target = assign.Target as Variable;
            if (target != null) env[target] = value;
            if (seenFinalCompare && !(value.Two == EvalKind.IsDisposingTest))
            {
              // must be the end.
              statementIndex = i;
              return currentBlockIndex;
            }
            continue;
          }
          if (seenFinalCompare)
          {
            // must be the end.
            statementIndex = i;
            return currentBlockIndex;
          }
          switch (stmt.NodeType)
          {
            case NodeType.Nop:
            case NodeType.ExpressionStatement:
              // skip: C# compiler emits funky pushes then pops
              break;

            default:
              Contract.Assume(false, string.Format("Unexpected node type '{0}'", stmt.NodeType));
              return -1;
          }
        }
        // next block in body
        currentBlockIndex++;
      OuterLoop:
        ;
      }
      return -1;
    }

    static bool IsThisDotState(Expression exp)
    {
      MemberBinding mb = exp as MemberBinding;
      if (mb != null)
      {
        if (mb.TargetObject != null && mb.TargetObject is This && mb.BoundMember is Field)
        {
          return (mb.BoundMember.Name.Name.Contains("<>") && mb.BoundMember.Name.Name.Contains("state"));
        }
      }
      return false;
    }


    static bool IsDoFinallyBodies(Expression expression)
    {
      Local l = expression as Local;
      if (l != null)
      {
        if (l.Name != null && l.Name.Name.Contains("__doFinallyBodies")) return true;
        return false;
      }
      MemberBinding mb = expression as MemberBinding;
      if (mb != null)
      {
        if (mb.TargetObject != null && mb.TargetObject is This && mb.BoundMember is Field)
        {
          return (mb.BoundMember.Name != null && mb.BoundMember.Name.Name.Contains("__doFinallyBodies"));
        }
      }
      return false;
    }

    [ContractVerification(true)]
    static private Pair<int, EvalKind> EvaluateExpression(Expression expression, Dictionary<Variable, Pair<int, EvalKind>> env, bool ignoreUnknown, bool isAsync)
    {
      Contract.Requires(env != null);

      var binary = expression as BinaryExpression;
      if (binary != null)
      {
        var op1 = EvaluateExpression(binary.Operand1, env, ignoreUnknown, isAsync);
        var op2 = EvaluateExpression(binary.Operand2, env, ignoreUnknown, isAsync);
        var resultKind = CombineEvalKind(ref op1, ref op2);
        switch (binary.NodeType)
        {
          case NodeType.Eq:
          case NodeType.Ceq:
            return Pair.For((op1.One == op2.One) ? 1 : 0, resultKind);
          case NodeType.Ne:
            return Pair.For((op1.One != op2.One) ? 1 : 0, resultKind);
          case NodeType.Gt:
            return Pair.For((op1.One > op2.One) ? 1 : 0, resultKind);
          case NodeType.Ge:
            return Pair.For((op1.One >= op2.One) ? 1 : 0, resultKind);
          case NodeType.Lt:
            return Pair.For((op1.One < op2.One) ? 1 : 0, resultKind);
          case NodeType.Le:
            return Pair.For((op1.One <= op2.One) ? 1 : 0, resultKind);

          case NodeType.Sub:
          case NodeType.Sub_Ovf:
          case NodeType.Sub_Ovf_Un:
            return Pair.For(op1.One - op2.One, EvalKind.None);

          case NodeType.Add:
          case NodeType.Add_Ovf:
          case NodeType.Add_Ovf_Un:
            return Pair.For(op1.One + op2.One, EvalKind.None);

          default:
            return Pair.For(-2, EvalKind.None);
        }
      }
      var unary = expression as UnaryExpression;
      if (unary != null)
      {
        var op = EvaluateExpression(unary.Operand, env, ignoreUnknown, isAsync);
        var resultKind = EvalKind.None;
        if (op.Two == EvalKind.IsDisposingTest)
        {
          resultKind = EvalKind.IsDisposingTest;
        }
        else if (op.Two == EvalKind.IsFinalCompare)
        {
          resultKind = EvalKind.IsFinalCompare;
        }
        else if (op.Two == EvalKind.IsStateValue)
        {
          resultKind = EvalKind.IsFinalCompare;
        }
        switch (unary.NodeType)
        {
          case NodeType.LogicalNot:
            return Pair.For((op.One == 0) ? 1 : 0, resultKind);
          default:
            return Pair.For(-2, EvalKind.None);
        }
      }
      var lit = expression as Literal;
      if (lit != null)
      {
        if (lit.Value is int) return Pair.For((int)lit.Value, EvalKind.None);
        return Pair.For(-2, EvalKind.None);
      }
      var mb = expression as MemberBinding;
      if (mb != null)
      {
        if (mb.TargetObject != null && mb.TargetObject is This && mb.BoundMember != null && mb.BoundMember.Name != null && mb.BoundMember.Name.Name != null)
        {
          var name = mb.BoundMember.Name.Name;
          if (name.EndsWith("$__disposing"))
          {
            return Pair.For(0, EvalKind.IsDisposingTest);
          }
          if (name.Contains("<>") && name.Contains("__state")) {
            var initialState = isAsync ? -1 : 0;
            return Pair.For(initialState, EvalKind.IsStateValue);
          }
        }
        return Pair.For(-2, EvalKind.None);
      }
      var variable = expression as Variable;
      if (variable != null)
      {
        Pair<int,EvalKind> value;
        if (env.TryGetValue(variable, out value))
        {
          return value;
        }
        return Pair.For(-2, EvalKind.None);
      }

      // Roslyn-based compiler in Release mode can skip statemachine initialization
      var methodCall = expression as MethodCall;
      if (methodCall != null)
      {
          return Pair.For(-2, EvalKind.None);
      }

      if (ignoreUnknown)
      {
        return Pair.For(-2, EvalKind.None);
      }

      throw new NotImplementedException("async/iterator issue");
    }

    private static EvalKind CombineEvalKind(ref Pair<int, EvalKind> op1, ref Pair<int, EvalKind> op2)
    {
        var resultKind = EvalKind.None;
        if (op1.Two == EvalKind.IsStateValue && (op2.One == 0 || op2.One == -1) ||
            op2.Two == EvalKind.IsStateValue && (op1.One == 0 || op1.One == -1) ||
            op1.Two == EvalKind.IsFinalCompare ||
            op2.Two == EvalKind.IsFinalCompare)
        {
            resultKind = EvalKind.IsFinalCompare;
        }
        else if (op1.Two == EvalKind.IsDisposingTest ||
                 op2.Two == EvalKind.IsDisposingTest)
        {
            resultKind = EvalKind.IsDisposingTest;
        }
        return resultKind;
    }

    [ContractVerification(true)]
    static int FindTargetBlock(StatementList blocks, Block target, int current)
    {
      Contract.Requires(blocks != null);
      Contract.Requires(current >= 0);
      Contract.Ensures(Contract.Result<int>() >= -1);
      Contract.Ensures(Contract.Result<int>() < blocks.Count);

      for (int i = current+1; i < blocks.Count; i++)
      {
        if (blocks[i] == target) return i;
      }
      return -1;
    }


    #endregion Helper Methods

    #region Visitor Overrides

    public override AssemblyNode VisitAssembly(AssemblyNode assembly) {
      if (assembly == null) return null;

      if (this.verbose)
      {
        Console.WriteLine("Extracting from {0}", assembly.Location);
      }
      if (ContractNodes.IsAlreadyRewritten(assembly)) {
        // if the assembly has an out of band contract, then get the contracts from the shadow assembly, so we don't
        // care if the assembly itself has been rewritten.
        return assembly;
      }
      this.isVB = IsVBAssembly(assembly);

      AssemblyNode a = base.VisitAssembly(assembly);
      //if (this.errorFound) {
      //  throw new ExtractorException("Error found: cannot continue");
      //}

      //AfterExtractionCleanup aec = new AfterExtractionCleanup(this.contractNodes);
      //a = aec.VisitAssembly(a);

      return a;
    }

    private bool IsVBAssembly(AssemblyNode assembly)
    {
      foreach (var r in assembly.AssemblyReferences)
      {
        if (r == null) continue;
        if (r.Name == "Microsoft.VisualBasic") return true;
      }
      return false;
    }

    public override TypeNode VisitTypeNode(TypeNode typeNode) {
      if (typeNode == null) return null;

      if (!this.IncludeModels && HelperMethods.HasAttribute(typeNode.Attributes, ContractNodes.ModelAttributeName)) return null;

      #region early cop out if we are extracting from X.Contracts.dll and X.dll does not have this type
      if (SkipThisTypeDueToMismatchInReferenceAssemblyPlatform(ultimateTargetAssembly, typeNode)) return typeNode;
      #endregion

      TypeNode result;
      if (typeNode.Contract != null) {
        TypeContract tc = typeNode.Contract;
        TypeNode t = base.VisitTypeNode(typeNode);
        t.Contract = tc;
        result = t;
      } else {
        result = base.VisitTypeNode(typeNode);

        TryLiftInvariantsToPropertyRequiresEnsures(typeNode);
      }
      return result;
    }

    private bool SkipThisTypeDueToMismatchInReferenceAssemblyPlatform(AssemblyNode ultimateTargetAssembly, TypeNode typeNode)
    {
      if (ultimateTargetAssembly == null) return false;
      if (typeNode == this.contractNodes.ContractClass) return false; // don't skip contract methods as we need to extract their contracts
      if (HelperMethods.IsCompilerGenerated(typeNode)) return false; // don't skip closures etc.
      var typeWithSeparateContractClass = HelperMethods.IsContractTypeForSomeOtherTypeUnspecialized(typeNode, this.contractNodes);
      if (typeWithSeparateContractClass != null)
      {
        typeNode = typeWithSeparateContractClass; // see if this one is skipped
      }
      // now see if we have corresponding target type
      if (typeNode.FindShadow(ultimateTargetAssembly) != null) return false; // have target

      return true; // skip it.
    }

    private ScrubContractClass currentScrubber;
    readonly private AssemblyNode realAssembly;

    /// <summary>
    /// Extracts contracts from a class into the appropriate fields in the CCI tree.
    /// </summary>
    /// <param name="Class">Class to visit.</param>
    /// <returns>The same class but with contracts extracted.</returns>
    public override Class VisitClass(Class Class)
    {

      #region Special processing for classes that are holding the contract for an interface or abstract class
      var originalType = HelperMethods.IsContractTypeForSomeOtherTypeUnspecialized(Class, this.contractNodes);
      var originalScrubber = this.currentScrubber;

      if (originalType != null) {
        this.currentScrubber = new ScrubContractClass(this, Class, originalType);
      }
      #endregion

      try
      {
        return base.VisitClass(Class);
      }
      finally
      {
        this.currentScrubber = originalScrubber;
      }
    }

    public override Property VisitProperty(Property property)
    {
      if (property == null) return null;
      if (!this.IncludeModels && HelperMethods.HasAttribute(property.Attributes, ContractNodes.ModelAttributeName)) return null;
      return base.VisitProperty(property);
    }

    public override Field VisitField(Field field)
    {
      if (field == null) return null;
      if (!this.IncludeModels && HelperMethods.HasAttribute(field.Attributes, ContractNodes.ModelAttributeName)) return null;
      return base.VisitField(field);
    }

    /// <summary>
    /// Extracts method-level contracts from a method into the appropriate fields in the CCI tree.
    /// </summary>
    /// <param name="method">Method to visit.</param>
    /// <returns>The same method but with contracts extracted.</returns>
    [ContractVerification(true)]
    public override Method VisitMethod(Method method) {
      if (method == null) return null;
      Contract.Assume(method.Template == null);
      Contract.Assume(TypeNode.IsCompleteTemplate(method.DeclaringType));
      if (this.contractNodes.IsObjectInvariantMethod(method)) {
        ExtractInvariantsFromMethod(method);
        return method;
      }
      if (!this.IncludeModels && HelperMethods.HasAttribute(method.Attributes, ContractNodes.ModelAttributeName)) return null;

      // skip iterator movenext methods
      if (HelperMethods.IsCompilerGenerated(method.DeclaringType) &&
          method.Name != null && method.Name.Name == "MoveNext")
      {
        // generated MoveNext method, skip it.
        return method;
      }
      #region Keep track of visited methods
      if (visitedMethods.ContainsKey(method))
        return method;
      visitedMethods.Add(method, method);
      #endregion Keep track of visited methods

      var scrubber = this.currentScrubber;
      if (scrubber != null)
      {
        method.SetDelayedContract((m, dummy) => { scrubber.VisitMethod(m); ExtractContractsForMethod(m, dummy); });
      }
      else
      {
        method.SetDelayedContract(ExtractContractsForMethod);
      }
      return method;
    }

    [ContractVerification(true)]
    private void ExtractContractsForMethod(Method method, object dummy)
    {
      Contract.Requires(method != null);

      RequiresList preconditions = null;
      EnsuresList postconditions = null;
      RequiresList validations = null;
      EnsuresList modelPostconditions = null;

      //Console.WriteLine("Extracting contract for method {0}", method.FullName);

      // set its contract so pure is properly handled.
      var methodContract = method.Contract = new MethodContract(method);

      try
      {

        if (method.IsAbstract /* && contractClass == null */)
        {
          // Abstract methods cannot have a body, so nothing to extract
          return;
        }

        TypeNode/*?*/ closure = null;
        bool possiblyAsync;
        if (IsIteratorOrAsyncMethodCandidate(method, out possiblyAsync))
        {
          closure = FindClosureClass(method);
          if (closure != null)
          {
            this.ProcessClosureClass(method, closure, possiblyAsync);
            return;
          }
        }

        if (method.Body == null || method.Body.Statements == null)
          return;

        #region Find first source context, use it if no better one is found on errors
        // if (method.Body != null && method.Body.Statements != null)
        {
          if (this.verbose)
          {
            Console.WriteLine(method.FullName);
          }
          bool found = false;
          for (int i = 0, n = method.Body.Statements.Count; i < n && !found; i++)
          {
            Block b = method.Body.Statements[i] as Block;
            if (b != null)
            {
              for (int j2 = 0, m = b.Statements == null ? 0 : b.Statements.Count; j2 < m; j2++)
              {
                Contract.Assert(m == b.Statements.Count, "loop invariant not inferred");

                Statement s = b.Statements[j2];
                if (s != null)
                {
                  SourceContext sctx = s.SourceContext;
                  if (sctx.IsValid)
                  {
                    found = true;
                    // s.SourceContext = new SourceContext(); // wipe out the source context because this statement will no longer be the first one in the method body if there are any contract calls
                    this.currentMethodSourceContext = sctx;
                    if (this.verbose)
                    {
                      Console.WriteLine("block {0}, statement {1}: ({2},{3})", i, j2, sctx.StartLine, sctx.StartColumn);
                    }
                    break;
                  }
                }
              }
            }
          }
        }
        #endregion Find first source context, use it if no better one is found on errors

        Block contractInitializerBlock = new Block(new StatementList());
        Block postPreamble = null;
        HelperMethods.StackDepthTracker dupStackTracker = new HelperMethods.StackDepthTracker();
        var contractLocalAliasingThis = HelperMethods.ExtractPreamble(method, this.contractNodes, contractInitializerBlock, out postPreamble, ref dupStackTracker, this.isVB);



        bool saveErrorFound = this.errorFound;
        this.errorFound = false;
        #region Extract pre- and postconditions
        if (method.Body != null && method.Body.Statements != null)
        {
          this.CheapAndDirty(method, ref preconditions, ref postconditions, ref validations, ref modelPostconditions, contractInitializerBlock, ref dupStackTracker);
          if (this.errorFound)
          {
            method.ClearBody();
            this.errorFound = saveErrorFound;
            return;
          }
          this.errorFound = saveErrorFound;
        }
        #endregion Extract pre- and postconditions



        #region Sanitize contract by renaming local aliasing "this" to This
        if (contractLocalAliasingThis != null && method.ThisParameter != null)
        {
          var renamer = new ContractLocalToThis(contractLocalAliasingThis, method.ThisParameter);
          renamer.Visit(preconditions);
          renamer.Visit(postconditions);
          renamer.Visit(modelPostconditions);
        }
        #endregion

        #region Split out async ensures
        var asyncPostconditions = SplitAsyncEnsures(ref postconditions, method);
        #endregion

        #region Store contracts into the appropriate slots
        Contract.Assume(methodContract.RequiresCount == 0);
        Contract.Assume(methodContract.EnsuresCount == 0);
        Contract.Assume(methodContract.ModelEnsuresCount == 0);

        methodContract.ContractInitializer = contractInitializerBlock;
        methodContract.PostPreamble = postPreamble;
        methodContract.Requires = preconditions;
        methodContract.Ensures = postconditions;
        methodContract.AsyncEnsures = asyncPostconditions;
        methodContract.ModelEnsures = modelPostconditions;
        methodContract.Validations = validations;
        #endregion Store contracts into the appropriate slots
        return;
      }
      catch (NotImplementedException ni)
      {
        // indicates a problem
        this.HandleError(method, 1099, "Contract extraction failed: " + ni.Message, default(SourceContext));
      }
      finally
      {
        // normalize contract by forcing IsPure to look at attributes and removing contract it is empty
        var isPure = methodContract.IsPure;

        if (!isPure && methodContract.RequiresCount == 0 && 
            methodContract.EnsuresCount == 0 &&
            methodContract.ModelEnsuresCount == 0 &&
            methodContract.AsyncEnsuresCount == 0 &&
            methodContract.ValidationsCount == 0
            )
        {
          method.Contract = null;
        } else
        {
          // turn helper method calls to Result, OldValue, ValueAtReturn into proper AST nodes.
          this.extractionFinalizer.VisitMethodContract(methodContract);
        }
      }
    }

    private class ContractLocalToThis : StandardVisitor
    {
      This @this;
      Local local;

      public ContractLocalToThis(Local local, This @this)
      {
        this.@this = @this;
        this.local = local;
      }

      public void Visit(RequiresList requires)
      {
        if (requires == null) return;
        for (int i = 0; i < requires.Count; i++)
        {
          this.Visit(requires[i]);
        }
      }
      public void Visit(EnsuresList ensures)
      {
        if (ensures == null) return;
        for (int i = 0; i < ensures.Count; i++)
        {
          this.Visit(ensures[i]);
        }
      }
      public override Expression VisitLocal(Local local)
      {
        if (local == this.local)
        {
          return this.@this;
        }
        return local;
      }
    }

    /// <summary>
    /// Check for well-formedness of invariant method.
    /// Add body to $invariantMethod$ and extract individual invariants
    /// </summary>
    /// <param name="method"></param>
    private void ExtractInvariantsFromMethod(Method method)
    {
      #region Well formedness
      if (!HelperMethods.IsVoidType(method.ReturnType))
      {
        this.HandleError(method, 1030, "Invariant method must have void return type.", HelperMethods.SourceContextOfMethod(method));
        return;
      }
      if (method.ParameterCount > 0)
      {
        this.HandleError(method, 1031, "Invariant method must have no argument types.", HelperMethods.SourceContextOfMethod(method));
        return;
      }
      if (!method.IsPrivate)
      {
        this.HandleError(method, 1041, "Invariant method must be private", HelperMethods.SourceContextOfMethod(method));
      }
      #endregion

      var typeNode = method.DeclaringType;
      var contract = typeNode.Contract;
      if (contract == null)
      {
        contract = typeNode.Contract = new TypeContract(typeNode, true);
      }
      var invariantList = typeNode.Contract.Invariants;
      ExtractIndividualInvariants(typeNode, invariantList, method);
    }

    private void TryLiftInvariantsToPropertyRequiresEnsures(TypeNode typeNode)
    {
        if (typeNode.Contract != null)
        {
            for (int i = 0; i < typeNode.Contract.InvariantCount; i++)
            {
                TryLiftingPropertyInvariantToPropertyRequiresEnsures(typeNode, typeNode.Contract.Invariants[i]);
            }
        }
    }


    private Invariant TryLiftingPropertyInvariantToPropertyRequiresEnsures(TypeNode typeNode, Invariant invariant)
    {
      List<Member> referencedMembers;
      var autoprops = AutoPropFinder.FindAutoProperty(this, typeNode, invariant.Condition, out referencedMembers);

      if (autoprops == null || autoprops.Count == 0) return invariant;

      foreach (var autoPropertyUsed in autoprops)
      {
        var getter = HelperMethods.Unspecialize(autoPropertyUsed.Getter);
        if (!AllReferencedMembersAsVisibleAs(getter, referencedMembers)) continue;

        var setter = HelperMethods.Unspecialize(autoPropertyUsed.Setter);

        if (getter.Contract == null) {
          getter.Contract = new MethodContract(getter);
        }
        if (setter.Contract == null)
        {
          setter.Contract = new MethodContract(setter);
        }
        var ensuresList = getter.Contract.Ensures;
        if (ensuresList == null)
        {
          getter.Contract.Ensures = ensuresList = new EnsuresList();
        }
        var requiresList = setter.Contract.Requires;
        if (requiresList == null)
        {
          setter.Contract.Requires = requiresList = new RequiresList();
        }
        var validationList = setter.Contract.Validations;
        if (validationList == null)
        {
          setter.Contract.Validations = validationList = new RequiresList();
        }
        Requires req;
        Ensures ens;
        ChangePropertyInvariantIntoRequiresEnsures.Transform(this, autoPropertyUsed, invariant, out req, out ens);
        requiresList.Add(req);
        validationList.Add(req);
        ensuresList.Add(ens);

        // make sure the property getter isn't visited again
        if (!this.visitedMethods.ContainsKey(autoPropertyUsed.Getter))
        {
          this.visitedMethods.Add(autoPropertyUsed.Getter, autoPropertyUsed.Getter);
        }
        if (!this.visitedMethods.ContainsKey(autoPropertyUsed.Setter))
        {
          this.visitedMethods.Add(autoPropertyUsed.Setter, autoPropertyUsed.Setter);
        }
      }
      ReplaceAutoPropertiesWithCorrespondingFields.Replace(autoprops, invariant);
      return invariant;

    }

    private static bool AllReferencedMembersAsVisibleAs(Method getter, List<Member> referencedMembers)
    {
      foreach (var member in referencedMembers)
      {
        if (!HelperMethods.IsReferenceAsVisibleAs(member, getter))
        {
          return false;
        }
      }
      return true;
    }

    private class ReplaceAutoPropertiesWithCorrespondingFields :StandardVisitor
    {
      List<Property> autoprops;
      private ReplaceAutoPropertiesWithCorrespondingFields(List<Property> autoprops)
      {
        this.autoprops = autoprops;
      }

      public static void Replace(List<Property> autoprops, Invariant condition)
      {
        var v = new ReplaceAutoPropertiesWithCorrespondingFields(autoprops);
        v.Visit(condition);
      }

      public override Expression VisitMethodCall(MethodCall call)
      {
        var result = base.VisitMethodCall(call);
        call = result as MethodCall;
        if (call == null) return result;

        var mb = call.Callee as MemberBinding;
        if (mb == null) return call;
        if (!(mb.TargetObject is This)) return call;
        var getter = mb.BoundMember as Method;
        if (getter == null) return call;
        if (!getter.IsPropertyGetter) return call;

        Property prop = getter.DeclaringMember as Property;
        if (prop == null) return call;
        if (this.autoprops.Contains(prop))
        {
          Contract.Assert(call.Operands == null || call.Operands.Count == 0);
          return new MemberBinding(mb.TargetObject, HelperMethods.GetBackingField(prop.Setter));
        }
        return call;
      }

    }

    private class ChangePropertyInvariantIntoRequiresEnsures : Duplicator
    {
      Property autoProp;
      ExtractorVisitor parent;
      Expression userMessage;
      Literal conditionString;
      bool makeRequires;

      private ChangePropertyInvariantIntoRequiresEnsures(ExtractorVisitor parent, Property autoProp)
        : base(autoProp.DeclaringType.DeclaringModule, autoProp.DeclaringType)
      {
        this.autoProp = autoProp;
        this.parent = parent;
      }

      public static void Transform(ExtractorVisitor parent, Property autoProp, Invariant invariant, out Requires req, out Ensures ens)
      {
        var makeReq = new ChangePropertyInvariantIntoRequiresEnsures(parent, autoProp);
        req = makeReq.MakeRequires(invariant.Condition);

        var makeEns = new ChangePropertyInvariantIntoRequiresEnsures(parent, autoProp);
        ens = makeEns.MakeEnsures(invariant.Condition);

        ens.SourceContext = invariant.SourceContext;
        ens.PostCondition.SourceContext = ens.SourceContext;
        ens.ILOffset = invariant.ILOffset;
        if (ens.SourceConditionText == null) { ens.SourceConditionText = invariant.SourceConditionText; }

        req.SourceContext = invariant.SourceContext;
        req.Condition.SourceContext = req.SourceContext;
        req.ILOffset = invariant.ILOffset;
        if (req.SourceConditionText == null) { req.SourceConditionText = invariant.SourceConditionText; }
      }

      private Ensures MakeEnsures(Expression expression)
      {
        makeRequires = false;
        var condition = (Expression)this.Visit(expression);
        var result = new EnsuresNormal(condition);
        result.SourceConditionText = conditionString;
        result.UserMessage = userMessage;
        return result;
      }

      private Requires MakeRequires(Expression expression)
      {
        makeRequires = true;
        var condition = (Expression)this.Visit(expression);
        var result = new RequiresPlain(condition);
        result.SourceConditionText = conditionString;
        result.UserMessage = userMessage;
        return result;
      }

      public override TypeNode VisitTypeReference(TypeNode type)
      {
        return type;
      }

      public override Expression VisitMethodCall(MethodCall call)
      {
        var result = base.VisitMethodCall(call);
        call = result as MethodCall;
        if (call == null) return result;

        var mb = call.Callee as MemberBinding;

        if (IsInvariantCall(mb))
        {
          if (1 < call.Operands.Count)
          {
            Member dummy;
            this.userMessage = SanitizeUserMessageInternal(this.autoProp.Getter, call.Operands[1], out dummy);
          }
          if (2 < call.Operands.Count)
          {
            this.conditionString = call.Operands[2] as Literal;
          }
          return call.Operands[0];
        }

        if (call.Operands.Count != 0) return call; // only nullary properties.

        if (IsAutoPropGetterCall(mb))
        {
          if (makeRequires)
          {
            return this.autoProp.Setter.Parameters[0];
          }
          else
          {
            return new ReturnValue(autoProp.Type);
          }
        }

        return result;
      }

      private bool IsAutoPropGetterCall(MemberBinding mb)
      {
        if (!(mb.TargetObject is This)) return false;

        if (mb.BoundMember == autoProp.Getter) return true;

        return false;
      }

      private bool IsInvariantCall(MemberBinding mb)
      {
        if (mb.TargetObject != null) return false;
        if (this.parent.contractNodes.IsInvariantMethod(mb.BoundMember as Method)) return true;
        return false;
      }
    }

    private class AutoPropFinder : Inspector
    {
      List<Property> foundAutoProperties = new List<Property>();
      List<Member> referencedMembers = new List<Member>();

      TypeNode containingType;
      ExtractorVisitor parent;
      Expression invariantCondition;

      public static List<Property> FindAutoProperty(ExtractorVisitor parent, TypeNode containingType, Expression expression, out List<Member> referencedMembers)
      {
        var v = new AutoPropFinder(parent, containingType, expression);
        v.VisitExpression(expression);

        referencedMembers = v.referencedMembers;
        return v.foundAutoProperties;
      }

      private bool IsVisibilityOkay(Property prop)
      {
        return parent.visibility.IsAsVisibleAs(this.invariantCondition, prop.Setter);
      }

      private AutoPropFinder(ExtractorVisitor parent, TypeNode containingType, Expression invariantCondition)
      {
        this.parent = parent;
        this.containingType = HelperMethods.Unspecialize(containingType);
        this.invariantCondition = invariantCondition;
      }

      public override void VisitMemberBinding(MemberBinding memberBinding)
      {
        if (memberBinding == null) return;
        if (memberBinding.BoundMember != null)
        {
          if (memberBinding.BoundMember.Name.Matches(ContractNodes.InvariantName) && memberBinding.BoundMember.DeclaringType.Name.Matches(ContractNodes.ContractClassName))
          {
            // skip
          }
          else
          {
            this.referencedMembers.Add(memberBinding.BoundMember);
          }
        }
        base.VisitMemberBinding(memberBinding);
      }
      public override void VisitMethodCall(MethodCall call)
      {
        if (call == null) return;
        base.VisitMethodCall(call);
        if (call.Operands != null && call.Operands.Count > 0) return;

        var mb = call.Callee as MemberBinding;
        if (mb == null) return;
        if (!(mb.TargetObject is This)) return;
        var getter = mb.BoundMember as Method;
        if (getter == null) return;
        if (HelperMethods.Unspecialize(getter.DeclaringType) != this.containingType) return;
        if (!getter.IsPropertyGetter) return;
        if (!HelperMethods.IsAutoPropertyMember(getter)) return;
        if ((getter.ImplementedInterfaceMethods != null && 0 < getter.ImplementedInterfaceMethods.Count)
           || (getter.ImplicitlyImplementedInterfaceMethods != null && 0 < getter.ImplicitlyImplementedInterfaceMethods.Count)
           || getter.OverridesBaseClassMember)
          // if the property is an override/implementation, then it is going to inherit any contracts there might be from above
          return;

        // now we have an auto prop
        // make sure we have a setter too
        Property prop = getter.DeclaringMember as Property;
        if (prop == null) return;
        if (prop.Setter == null) return;
        if (!HelperMethods.IsAutoPropertyMember(prop.Setter)) return;

        if (!IsVisibilityOkay(prop)) return;

        this.foundAutoProperties.Add(prop);
      }
    }
    #endregion Visitor Overrides

  }

  /// <summary>
  /// Application-level exceptions thrown by this component.
  /// </summary>
  public class ExtractorException : Exception {
    /// <summary>
    /// Exception specific to an error occurring in the contract rewriter
    /// </summary>
    public ExtractorException() { }
    /// <summary>
    /// Exception specific to an error occurring in the contract rewriter
    /// </summary>
    public ExtractorException(string s) : base(s) { }
    /// <summary>
    /// Exception specific to an error occurring in the contract rewriter
    /// </summary>
    public ExtractorException(string s, Exception inner) : base(s, inner) { }
    /// <summary>
    /// Exception specific to an error occurring in the contract rewriter
    /// </summary>
    public ExtractorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }


  internal class ClousotExtractor : ExtractorVisitor {
    public ClousotExtractor(ContractNodes contractNodes, AssemblyNode ultimateTargetAssembly, AssemblyNode realAssembly, Action<System.CodeDom.Compiler.CompilerError> errorHandler)
      : base(contractNodes, ultimateTargetAssembly, realAssembly) {

        Contract.Requires(contractNodes != null);
        Contract.Requires(realAssembly != null);
    }

    public override Method VisitMethod(Method method) {
      if (method == null) return null;

      if (visitedMethods.ContainsKey(method))
        return method;

      base.VisitMethod(method);

      method.SetDelayedContract((m, dummy) => {
        // cleanup contracts for clousot
        this.CleanupRequires(method);
        this.CleanupEnsures(method);
        // this.CleanupBody(method.Body);
      });

      return method;
    }

    protected override Statement ExtraAssumeFalseOnThrow()
    {
        return new ExpressionStatement(new MethodCall(new MemberBinding(null, this.contractNodes.AssumeMethod), new ExpressionList(Literal.False), NodeType.Call));
    }

    public override EnsuresExceptional VisitEnsuresExceptional(EnsuresExceptional exceptional) {
      // since clousot doesn't handle exceptional post yet, we filter them out here.
      // TODO: refactor Ensures into two lists: normal and exceptional
      return null;
    }

    /// <summary>
    /// Visits the ensures clauses to clean up Old expressions
    /// </summary>
    /// <param name="m"></param>
    public void CleanupEnsures(Method m) {
      if (m.Contract != null) {
        this.VisitEnsuresList(m.Contract.Ensures);
        this.VisitEnsuresList(m.Contract.AsyncEnsures);
        this.VisitEnsuresList(m.Contract.ModelEnsures);
      }
    }

    protected override bool IncludeModels {
      get {
        return true;
      }
    }

    /// <summary>
    /// Visits the requires clauses to clean up overloaded calls
    /// </summary>
    public void CleanupRequires(Method m) {
      if (m.Contract != null) {
        this.VisitRequiresList(m.Contract.Requires);
      }
    }

    bool insideInvariant;

#if true// ExtractorVisitor now does this
    /// <summary>
    /// Performs a bunch of transformations that the basic Extractor doesn't seem to do:
    ///  - Turns calls to Old contract method into OldExpressions
    ///  - Turns references to "result" into ResultExpressions
    ///  - 
    /// </summary>
    public override Expression VisitMethodCall(MethodCall call) {
      MemberBinding mb = call.Callee as MemberBinding;
      if (mb == null) return call;
      Method calledMethod = mb.BoundMember as Method;
      if (calledMethod == null) return call;
      Method template = calledMethod.Template;
      if (contractNodes.IsOldMethod(template)) {
        Expression result = base.VisitMethodCall(call);
        call = result as MethodCall;
        if (call == null) return result;
        result = new OldExpression(call.Operands[0]);
        result.Type = call.Type;
        return result;
      }
      if (contractNodes.IsValueAtReturnMethod(template)) {
        return new AddressDereference(call.Operands[0], calledMethod.TemplateArguments[0], call.SourceContext);
      }
      if (calledMethod.Parameters != null) {
        if (calledMethod.Parameters.Count == 0) {
          if (contractNodes.IsResultMethod(template)) {
            return new ReturnValue(calledMethod.ReturnType, call.SourceContext);
          }
        } else if (insideInvariant &&
                   contractNodes.IsInvariantMethod(calledMethod)) {
          Expression condition = call.Operands[0];
          condition.SourceContext = call.SourceContext;
          return VisitExpression(condition);
        }
      }
      return base.VisitMethodCall(call);
    }

    public override Invariant VisitInvariant(Invariant invariant) {
      this.insideInvariant = true;
      try {
        return base.VisitInvariant(invariant);
      } finally {
        this.insideInvariant = false;
      }
    }

#endif
  }

  /// <summary>
  /// This one expects to visit the OOB assembly
  /// </summary>
  [ContractVerification(true)]
  internal sealed class CopyOutOfBandContracts : Inspector {
    //private Duplicator outOfBandDuplicator;
    readonly private ForwardingDuplicator outOfBandDuplicator;
    readonly private AssemblyNode targetAssembly;
    readonly private List<KeyValuePair<Member, TypeNode>> toBeDuplicatedMembers = new List<KeyValuePair<Member, TypeNode>>();
    private TrivialHashtable duplicatedMembers;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"), ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(this.toBeDuplicatedMembers != null);
      Contract.Invariant(this.outOfBandDuplicator != null);
      Contract.Invariant(this.duplicatedMembers != null);
    }

    [Conditional("CONTRACTTRACE")]
    private static void Trace(string format, params object[] args) {
      Console.WriteLine(format, args);
    }

    public CopyOutOfBandContracts(AssemblyNode targetAssembly, AssemblyNode sourceAssembly, ContractNodes contractNodes, ContractNodes targetContractNodes) {
      Contract.Requires(targetAssembly != null);
      Contract.Requires(sourceAssembly != null);
      Contract.Requires(contractNodes != null);

      if (targetAssembly == sourceAssembly) {
        // this happened when a reference assembly for mscorlib had the assembly name "mscorlib"
        // instead of "mscorlib.Contracts" because only one assembly named "mscorlib" can be
        // loaded
        throw new ExtractorException("CopyOutOfBandContracts was given the same assembly as both the source and target!");
      }

      this.outOfBandDuplicator = new ForwardingDuplicator(targetAssembly, null, contractNodes, targetContractNodes);
      this.targetAssembly = targetAssembly;

      FuzzilyForwardReferencesFromSource2Target(targetAssembly, sourceAssembly);
      CopyMissingMembers();
      // FixupMissingProperties(); shouldn't be needed with new duplicator
    }

    private void CopyMissingMembers() {
      Contract.Ensures(this.duplicatedMembers != null);
      
      this.duplicatedMembers = new TrivialHashtable(this.toBeDuplicatedMembers.Count*2);
      foreach (var missing in this.toBeDuplicatedMembers) {
        Contract.Assume(missing.Value != null);
        Contract.Assume(missing.Key != null);

        InstanceInitializer ctor = missing.Key as InstanceInitializer;
        if (ctor != null && ctor.ParameterCount == 0) continue;
        
        var targetType = missing.Value;
        Trace("COPYOOB: copying {0} to {1}", missing.Key.FullName, targetType.FullName);
        this.Duplicator.TargetType = targetType;
        var dup = (Member)this.Duplicator.Visit(missing.Key);
        targetType.Members.Add(dup);
        duplicatedMembers[missing.Key.UniqueKey] = missing;
      }
    }

    Duplicator Duplicator {
      get {
        Contract.Ensures(Contract.Result<Duplicator>() != null);

        return this.outOfBandDuplicator;
      }
    }
    #region Methods for setting up a duplicator to use to copy out-of-band contracts
    [ContractVerification(false)]
    private static bool FuzzyEqual(TypeNode t1, TypeNode t2) {
      return t1 == t2 ||
        (t1 != null &&
        t2 != null &&
        t1.Namespace != null &&
        t2.Namespace != null &&
        t1.Name != null &&
        t2.Name != null &&
        t1.Namespace.Name == t2.Namespace.Name &&
        t1.Name.Name == t2.Name.Name);
    }

    private static bool FuzzyEqual(Parameter p1, Parameter p2) {
      if (p1 == null && p2 == null) return true;
      if (p1 == null) return false;
      if (p2 == null) return false;
      return FuzzyEqual(p1.Type, p2.Type);
    }

    private static bool FuzzyEqual(ParameterList xs, ParameterList ys) {
      if (xs == null && ys == null) return true;
      if (xs == null) return false;
      if (ys == null) return false;

      if (xs.Count != ys.Count) return false;
      for (int i = 0, n = xs.Count; i < n; i++) {
        if (!FuzzyEqual(xs[i], ys[i])) return false;
      }
      return true;
    }
    [Pure]
    private static Member FuzzilyGetMatchingMember(TypeNode t, Member m) {
      Contract.Requires(t != null);
      Contract.Requires(m != null);

      var candidates = t.GetMembersNamed(m.Name);
      Contract.Assert(candidates != null, "Clousot can prove it");
      for (int i = 0, n = candidates.Count; i < n; i++) {
        Member mem = candidates[i];
        if (mem == null) continue;
        if (!mem.Name.Matches(m.Name)) continue;
        // type case statement would be *so* nice right now
        // Can't test the NodeType because for mscorlib.Contracts, structs are read in as classes
        // because they don't extend the "real" System.ValueType, but the one declared in mscorlib.Contracts.
        //if (mem.NodeType != m.NodeType) continue;
        Method x = mem as Method; // handles regular Methods and InstanceInitializers
        if (x != null) {
          Method m_prime = m as Method;
          if (m_prime == null) continue;
          if ((x.TemplateParameters == null) != (m_prime.TemplateParameters == null)) continue;
          if (FuzzyEqual(m_prime.Parameters, x.Parameters) 
              && FuzzyEqual(m_prime.ReturnType, x.ReturnType)
              && TemplateParameterCount(x) == TemplateParameterCount(m_prime))
            return mem;
          else continue;
        }
        Field memAsField = mem as Field;
        if (memAsField != null){
          Field mAsField = m as Field;
          if (mAsField == null) continue;
          if (FuzzyEqual(mAsField.Type, memAsField.Type)) return mem; else continue;
        }
        Event memAsEvent = mem as Event;
        if (memAsEvent != null){
          Event mAsEvent = m as Event;
          if (mAsEvent == null) continue;
          if (FuzzyEqual(mAsEvent.HandlerType, memAsEvent.HandlerType)) return mem; else continue;
        }
        Property memAsProperty = mem as Property;
        if (memAsProperty != null){
          Property mAsProperty = m as Property;
          if (mAsProperty == null) continue;
          if (FuzzyEqual(mAsProperty.Type, memAsProperty.Type)) return mem; else continue;
        }
        TypeNode memAsTypeNode = mem as TypeNode; // handles Class, Interface, etc.
        if (memAsTypeNode != null) {
          TypeNode mAsTypeNode = m as TypeNode;
          if (mAsTypeNode == null) continue;
          if (FuzzyEqual(mAsTypeNode, memAsTypeNode)) return mem; else continue;
        }

        Contract.Assume(false, "Pseudo-typecase failed to find a match");
      }
      return null;
    }
    void FuzzilyForwardReferencesFromSource2Target(AssemblyNode targetAssembly, AssemblyNode sourceAssembly) {
      Contract.Requires(targetAssembly != null);
      Contract.Requires(sourceAssembly != null);

      for (int i = 1, n = sourceAssembly.Types.Count; i < n; i++) {
        TypeNode currentType = sourceAssembly.Types[i];
        if (currentType == null) continue;
        TypeNode targetType = targetAssembly.GetType(currentType.Namespace, currentType.Name);
        if (targetType == null)
        {
          if (Duplicator.TypesToBeDuplicated[currentType.UniqueKey] == null) {
            Duplicator.FindTypesToBeDuplicated(new TypeNodeList(currentType));
          }
          Trace("COPYOOB: type to be duplicated {0}", currentType.FullName);

        } else {
          if (HelperMethods.IsContractTypeForSomeOtherType(currentType as Class))
          {
            // dummy contract target type. Ignore it. 
            targetType.Members = new MemberList();
            targetType.ClearMemberTable();
          }
          Contract.Assume(TemplateParameterCount(currentType) == TemplateParameterCount(targetType), "Name mangling should ensure this");

          Duplicator.DuplicateFor[currentType.UniqueKey] = targetType;
          Trace("COPYOOB: forwarding {1} to {0}", currentType.FullName, targetType.FullName);
          FuzzilyForwardType(currentType, targetType);
        }
      }
    }
    [Pure]
    static int TemplateParameterCount(TypeNode type)
    {
      Contract.Requires(type!= null);
      Contract.Ensures(type.TemplateParameters != null || Contract.Result<int>() == 0);
      
      return type.TemplateParameters == null ? 0 : type.TemplateParameters.Count;
    }
    [Pure]
    static int TemplateParameterCount(Method method)
    {
      Contract.Requires(method != null);
      Contract.Ensures(method.TemplateParameters != null || Contract.Result<int>() == 0);

      return method.TemplateParameters == null ? 0 : method.TemplateParameters.Count;
    }

    private void FuzzilyForwardType(TypeNode currentType, TypeNode targetType)
    {
      // forward any type parameters that the type has
      Contract.Requires(currentType != null);
      Contract.Requires(targetType != null);
      Contract.Requires(TemplateParameterCount(currentType) == TemplateParameterCount(targetType));

      for (int j = 0, m = TemplateParameterCount(currentType); j < m; j++)
      {
        Contract.Assert(TemplateParameterCount(targetType) > 0);
        TypeNode currentTypeParameter = currentType.TemplateParameters[j];
        if (currentTypeParameter == null) continue;
        Duplicator.DuplicateFor[currentTypeParameter.UniqueKey] = targetType.TemplateParameters[j];
      }
      FuzzilyForwardTypeMembersFromSource2Target(currentType, targetType);
    }

    [ContractVerification(true)]
    private void FuzzilyForwardTypeMembersFromSource2Target(TypeNode currentType, TypeNode targetType)
    {
      Contract.Requires(currentType != null);
      Contract.Requires(targetType != null);

      for (int j = 0, o = currentType.Members.Count; j < o; j++) {
        Member currentMember = currentType.Members[j];
        if (currentMember == null) continue;
        Member existingMember = FuzzilyGetMatchingMember(targetType, currentMember);
        if (existingMember != null) {
          FuzzilyForwardTypeMemberFromSource2Target(targetType, currentMember, existingMember);
        } else {
          this.toBeDuplicatedMembers.Add(new KeyValuePair<Member, TypeNode>(currentMember, targetType));
          // For types, prepare a bit more.
          var nestedType = currentMember as TypeNode;
          if (nestedType != null) {
            if (Duplicator.TypesToBeDuplicated[nestedType.UniqueKey] == null) {
              Duplicator.FindTypesToBeDuplicated(new TypeNodeList(nestedType));
            }
          }
        }
      }
    }

    private void FuzzilyForwardTypeMemberFromSource2Target(TypeNode targetType, Member currentMember, Member existingMember) {
      Contract.Requires(targetType != null);
      Contract.Requires(currentMember != null);
      Contract.Requires(existingMember != null);

      Trace("COPYOOB: Forwarding member {0} to {1}", currentMember.FullName, existingMember.FullName);

      Duplicator.DuplicateFor[currentMember.UniqueKey] = existingMember;
      Method method = currentMember as Method;
      if (method != null) {
        Method existingMethod = (Method)existingMember;

        Contract.Assume(TemplateParameterCount(method) == TemplateParameterCount(existingMethod));

        // forward any type parameters that the method has
        for (int i = 0, n = TemplateParameterCount(method); i < n; i++) {
          Contract.Assert(TemplateParameterCount(existingMethod) > 0);
          TypeNode currentTypeParameter = method.TemplateParameters[i];
          if (currentTypeParameter == null) continue;
          Duplicator.DuplicateFor[currentTypeParameter.UniqueKey] = existingMethod.TemplateParameters[i];
        }
      }
      TypeNode currentNested = currentMember as TypeNode;
      TypeNode targetNested = existingMember as TypeNode;
      if (currentNested != null) {
        Contract.Assume(targetNested != null);
        Contract.Assume(TemplateParameterCount(currentNested) == TemplateParameterCount(targetNested), "should be true by mangled name matching");
        FuzzilyForwardType(currentNested, targetNested);
      }
    }
    #endregion Methods for setting up a duplicator to use to copy out-of-band contracts

    /// <summary>
    /// Visiting a method in the shadow assembly
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public override void VisitMethod(Method method) {
      if (method == null) return;
      // we might have copied this method already
      if (this.duplicatedMembers[method.UniqueKey] != null) { return; }
      Method targetMethod = method.FindShadow(this.targetAssembly);

      if (targetMethod != null) {
        Contract.Assume(targetMethod.ParameterCount == method.ParameterCount);
        for (int i = 0, n = method.ParameterCount; i < n; i++)
        {
          Contract.Assert(targetMethod.ParameterCount > 0);
          var parameter = method.Parameters[i];
          if (parameter == null) continue;
          this.outOfBandDuplicator.DuplicateFor[parameter.UniqueKey] = targetMethod.Parameters[i];
        }
        CopyAttributesWithoutDuplicateUnlessAllowMultiple(targetMethod, method);
        CopyReturnAttributesWithoutDuplicateUnlessAllowMultiple(targetMethod, method);
        targetMethod.SetDelayedContract( (m, dummy) => {
          var savedMethod = this.outOfBandDuplicator.TargetMethod;
          this.outOfBandDuplicator.TargetMethod = method;
          this.outOfBandDuplicator.TargetType = method.DeclaringType;
          Contract.Assume(method.DeclaringType != null);
          this.outOfBandDuplicator.TargetModule = method.DeclaringType.DeclaringModule;
          MethodContract mc = this.outOfBandDuplicator.VisitMethodContract(method.Contract);
          targetMethod.Contract = mc;
          if (savedMethod != null)
          {
            this.outOfBandDuplicator.TargetMethod = savedMethod;
            this.outOfBandDuplicator.TargetType = savedMethod.DeclaringType;
          }
        });
      }
    }
    public override void VisitTypeNode(TypeNode typeNode) {
      if (typeNode == null) return;
      // we might have copied this type already
      if (this.duplicatedMembers[typeNode.UniqueKey] != null) { return; }

      TypeNode targetType = typeNode.FindShadow(this.targetAssembly);
      if (targetType != null) {

        if (targetType.Contract == null) {
          targetType.Contract = new TypeContract(targetType, true);
        }
        this.outOfBandDuplicator.TargetType = targetType;
        if (typeNode.Contract != null)
        {
          InvariantList duplicatedInvariants = this.outOfBandDuplicator.VisitInvariantList(typeNode.Contract.Invariants);
          targetType.Contract.Invariants = duplicatedInvariants;
        }

        CopyAttributesWithoutDuplicateUnlessAllowMultiple(targetType, typeNode);

        base.VisitTypeNode(typeNode);
      } else {
        // target type does not exist. Copy it
        if (typeNode.DeclaringType != null) {
          // nested types are members and have been handled by CopyMissingMembers
        } else {
          this.outOfBandDuplicator.VisitTypeNode(typeNode);
        }
      }
    }

    private void CopyAttributesWithoutDuplicateUnlessAllowMultiple(Member targetMember, Member sourceMember) {
      Contract.Requires(targetMember != null);
      Contract.Requires(sourceMember != null);

//      if (sourceMember.Attributes == null) return;
//      if (targetMember.Attributes == null) targetMember.Attributes = new AttributeList();
      var attrs = this.outOfBandDuplicator.VisitAttributeList(sourceMember.Attributes);
      Contract.Assume(attrs != null, "We fail to specialize the ensures");
      foreach (var a in attrs) {
        if (a == null) continue;
        // Can't look at a.Type because that doesn't get visited by the VisitAttributeList above
        // (Seems like a bug in the StandardVisitor...)
        TypeNode typeOfA = AttributeType(a);
        if (!a.AllowMultiple && targetMember.GetAttribute(typeOfA) != null) {
          continue;
        }
        targetMember.Attributes.Add(a);
      }
    }

    private static TypeNode AttributeType(AttributeNode a)
    {
      Contract.Requires(a != null);
      var ctor = a.Constructor as MemberBinding;
      if (ctor == null) return null;
      var mb = ctor.BoundMember;
      if (mb == null) return null;
      return mb.DeclaringType;
    }

    private void CopyReturnAttributesWithoutDuplicateUnlessAllowMultiple(Method targetMember, Method sourceMember) {
      Contract.Requires(sourceMember != null);
      Contract.Requires(targetMember != null);

      if (sourceMember.ReturnAttributes == null) return;
      if (targetMember.ReturnAttributes == null) targetMember.ReturnAttributes = new AttributeList();
      var attrs = this.outOfBandDuplicator.VisitAttributeList(sourceMember.ReturnAttributes);
      Contract.Assume(attrs != null, "We fail to specialize the ensures");
      foreach (var a in attrs)
      {
        if (a == null) continue;
        // Can't look at a.Type because that doesn't get visited by the VisitAttributeList above
        // (Seems like a bug in the StandardVisitor...)
        TypeNode typeOfA = AttributeType(a);
        if (!a.AllowMultiple && HasAttribute(targetMember.ReturnAttributes, typeOfA)) {
          continue;
        }
        targetMember.ReturnAttributes.Add(a);
      }
    }

    static bool HasAttribute(AttributeList attributes, TypeNode attributeType) {
      if (attributeType == null) return false;
      if (attributes == null) return false;
      for (int i = 0, n = attributes.Count; i < n; i++) {
        AttributeNode attr = attributes[i];
        if (attr == null) continue;
        MemberBinding mb = attr.Constructor as MemberBinding;
        if (mb != null) {
          if (mb.BoundMember == null) continue;
          if (mb.BoundMember.DeclaringType != attributeType) continue;
          return true;
        }
        Literal lit = attr.Constructor as Literal;
        if (lit == null) continue;
        if ((lit.Value as TypeNode) != attributeType) continue;
        return true;
      }
      return false;
    }

  }

  internal class AsyncContractDuplicator : Duplicator
  {
      TypeNode parentType;

      public AsyncContractDuplicator(TypeNode parentType, Module module)
          : base(module, parentType)
      {
          this.parentType = parentType;
      }

      public override Expression VisitLocal(Local local)
      {
          if (HelperMethods.IsClosureType(this.parentType, local.Type))
          {
              // don't copy local as we need to share this closure local
              return local;
          }
          return base.VisitLocal(local);
      }
  }

  /// <summary>
  /// This duplicator is used to copy contract from Out-of-band assemblies onto the real assembly
  /// 
  /// It is in charge of rebinding all the members to the target assembly members.
  /// 
  /// Different assemblies can use contracts defined in different places:
  ///   - Microsoft.Contracts.dll for pre-v4.0 assemblies
  ///   - Mscorlib.Contracts.dll for the contracts within the shadow assembly for mscorlib
  ///   - mscorlib.dll for post-v4.0 assemblies
  /// Therefore, for an extracted assembly to be independently processed (e.g., by the
  /// rewriter), anything in the assembly dependent on the contracts used must be replaced.
  /// This visitor does that by turning calls to:
  ///   - OldValue into OldExpressions
  ///   - Result into ResultExpressions
  ///   - ForAll into a method call to the ForAll defined in targetContractNodes
  ///   - Exists into a method call to the Exists defined in targetContractNodes
  ///   - ValueAtReturn(x) into the AST "AddressDereference(x,T)" where T is the type instantiation
  ///       of the generic type that ValueAtReturn is defined over. [Note: this transformation
  ///       is *not* undone by the Rewriter -- or anyone else -- since its only purpose was to
  ///       get past the compiler and isn't needed anymore.]
  ///   - All attributes defined in contractNodes are turned into the equivalent attribute in targetContractNodes
  ///
  /// When the regular Duplicator visits a member reference and that member is a generic
  /// instance, its DuplicateFor table has only the templates in it. To find the corresponding
  /// generic instance, it uses Specializer.GetCorrespondingMember. That method assumes the
  /// the member's DeclaringType, which is in the duplicator's source has the same members
  /// as the corresponding type in the duplicator's target. It furthermore assumes that those
  /// members are in exactly the same order in the respective member lists.
  /// 
  /// But we cannot assume that when forwarding things from one assembly to another.
  /// So this subtype of duplicator just has an override for VisitMemberReference that
  /// uses a different technique for generic method instances.
  /// 
  /// </summary>
  [ContractVerification(true)]
  internal class ForwardingDuplicator : Duplicator {
    readonly private ContractNodes contractNodes;
    readonly private ContractNodes targetContractNodes;

    public ForwardingDuplicator(Module/*!*/ module, TypeNode type, ContractNodes contractNodes, ContractNodes targetContractNodes) : base(module, type) {
      this.contractNodes = contractNodes;
      this.targetContractNodes = targetContractNodes;
    }

    /// <summary>
    /// Note, we can't just use the duplicator's logic for Member references, because the member offsets in the source and
    /// target are vastly different.
    /// </summary>
    [ContractVerification(false)]
    public override Member VisitMemberReference(Member member) {
      Contract.Ensures(Contract.Result<Member>() != null || member == null);

      var asType = member as TypeNode;
      if (asType != null) return this.VisitTypeReference(asType);

      var targetDeclaringType = this.VisitTypeReference(member.DeclaringType);

      Method sourceMethod = member as Method;
      if (sourceMethod != null) {
        // Here's how this works:
        //
        // 2. Identify MArgs (type arguments of method instance)
        //
        // 4. Find index i of method template in TargetDeclaringTypeTemplate by name and type names
        // 5. TargetDeclaringType = instantiate TargetDeclaringType with mapped TArgs
        // 6. MethodTemplate = TargetDeclaringType[i]
        // 7. Instantiate MethodTemplate with MArgs
        //
        var sourceMethodTemplate = sourceMethod;
        // Find source method template, but leave type instantiation
        while (sourceMethodTemplate.Template != null && sourceMethodTemplate.TemplateArguments != null && sourceMethodTemplate.TemplateArguments.Count > 0) {
          sourceMethodTemplate = sourceMethodTemplate.Template; 
        }

        #region Steps 1 and 2
        TypeNodeList targetMArgs = null;
        TypeNodeList sourceMArgs = sourceMethod.TemplateArguments;
        if (sourceMArgs != null) {
          targetMArgs = new TypeNodeList(sourceMArgs.Count);
          foreach (var mArg in sourceMArgs) {
            targetMArgs.Add(this.VisitTypeReference(mArg));
          }
        }

        #endregion

        Method targetMethod;

        var targetMethodTemplate = targetDeclaringType.FindShadow(sourceMethodTemplate);
        if (targetMethodTemplate == null)
        {
          // something is wrong. Let's not crash and simply keep the original
          return sourceMethod;
        }
        if (targetMArgs != null)
        {
          targetMethod = targetMethodTemplate.GetTemplateInstance(targetMethodTemplate.DeclaringType, targetMArgs);
        }
        else
        {
          targetMethod = targetMethodTemplate;
        }
        return targetMethod;
      }
      Field sourceField = member as Field;
      if (sourceField != null) {
        return targetDeclaringType.FindShadow(sourceField);
      }
      Property sourceProperty = member as Property;
      if (sourceProperty != null)
      {
        return targetDeclaringType.FindShadow(sourceProperty);
      }
      Event sourceEvent = member as Event;
      if (sourceEvent!= null)
      {
        return targetDeclaringType.FindShadow(sourceEvent);
      }
      Debug.Assert(false, "what other members are there?");
      Member result = base.VisitMemberReference(member);
      return result;
    }

    /// <summary>
    /// Need to make sure that references to ForAll/Exists that could be to another assembly go to the target contract nodes
    /// implementation.
    /// </summary>
    public override Expression VisitMemberBinding(MemberBinding memberBinding)
    {
      if (memberBinding == null) return null;
      var result = base.VisitMemberBinding(memberBinding);
      memberBinding = result as MemberBinding;
      if (this.targetContractNodes != null && memberBinding != null && memberBinding.TargetObject == null)
      { // all methods are static
        Method method = memberBinding.BoundMember as Method;
        if (method == null) return memberBinding;

        Contract.Assume(this.contractNodes != null);

        if (method.Template == null)
        {
          if (contractNodes.IsExistsMethod(method))
          {
            return new MemberBinding(null, targetContractNodes.GetExistsTemplate);
          } else if (contractNodes.IsForallMethod(method))
          {
            return new MemberBinding(null, targetContractNodes.GetForAllTemplate);
          }
        } else
        { // template != null
          Method template = method.Template;
          var templateArgs = method.TemplateArguments; 
          if (contractNodes.IsGenericForallMethod(template)) {
            Contract.Assume(templateArgs != null);
            Contract.Assume(targetContractNodes.GetForAllGenericTemplate != null);
            return new MemberBinding(null, targetContractNodes.GetForAllGenericTemplate.GetTemplateInstance(targetContractNodes.GetForAllGenericTemplate.DeclaringType, templateArgs[0]));
          } else if (contractNodes.IsGenericExistsMethod(template)) {
            Contract.Assume(templateArgs != null);
            Contract.Assume(targetContractNodes.GetExistsGenericTemplate != null);
            return new MemberBinding(null, targetContractNodes.GetExistsGenericTemplate.GetTemplateInstance(targetContractNodes.GetExistsGenericTemplate.DeclaringType, templateArgs[0]));
          }
        }
      }
      return result;
    }

    [ContractVerification(false)]
    public override AttributeNode VisitAttributeNode(AttributeNode attribute)
    {
      attribute = base.VisitAttributeNode(attribute);
      if (attribute == null) return null;
      if (this.targetContractNodes == null) return attribute;

      if (attribute.Type == this.contractNodes.ContractClassAttribute)
      {
        return new AttributeNode(new MemberBinding(null, this.targetContractNodes.ContractClassAttribute.GetConstructor(SystemTypes.Type)), attribute.Expressions);
      } else if (attribute.Type == this.contractNodes.ContractClassForAttribute)
      {
        return new AttributeNode(new MemberBinding(null, this.targetContractNodes.ContractClassForAttribute.GetConstructor(SystemTypes.Type)), attribute.Expressions);
      } else if (attribute.Type == this.contractNodes.IgnoreAtRuntimeAttribute)
      {
        return new AttributeNode(new MemberBinding(null, this.targetContractNodes.IgnoreAtRuntimeAttribute.GetConstructor()), null);
      } else if (attribute.Type == this.contractNodes.InvariantMethodAttribute)
      {
        return new AttributeNode(new MemberBinding(null, this.targetContractNodes.InvariantMethodAttribute.GetConstructor()), null);
      } else if (attribute.Type == this.contractNodes.PureAttribute)
      {
        return new AttributeNode(new MemberBinding(null, this.targetContractNodes.PureAttribute.GetConstructor()), null);
      } else if (attribute.Type == this.contractNodes.SpecPublicAttribute)
      {
        return new AttributeNode(new MemberBinding(null, this.targetContractNodes.SpecPublicAttribute.GetConstructor(SystemTypes.String)), attribute.Expressions);
      } else if (attribute.Type == this.contractNodes.VerifyAttribute)
      {
        return new AttributeNode(new MemberBinding(null, this.targetContractNodes.VerifyAttribute.GetConstructor(SystemTypes.Boolean)), attribute.Expressions);
      }
      return attribute;
    }

  }

  internal class VisitorIncludingClosures : StandardVisitor
  {
    Method currentMethod;
    protected Method CurrentMethod
    {
      get { return this.currentMethod; }
      set
      {
        this.currentMethod = value;
        if (value != null)
        {
          this.CurrentType = value.DeclaringType;
        } else
        {
          this.CurrentType = null;
        }
      }
    }
    protected TypeNode CurrentType
    {
      get;
      set;
    }
    public override Method VisitMethod(Method method)
    {
      if (method == null) return null;
      var savedCurrentMethod = this.CurrentMethod;
      this.CurrentMethod = method;
      try
      {
        return base.VisitMethod(method);
      } finally
      {
        this.CurrentMethod = savedCurrentMethod;
      }
    }

    public override MethodContract VisitMethodContract(MethodContract contract)
    {
      if (contract == null) return null;
      var savedCurrentMethod = this.CurrentMethod;
      this.CurrentMethod = contract.DeclaringMethod;
      try
      {
        return base.VisitMethodContract(contract);
      } finally
      {
        this.CurrentMethod = savedCurrentMethod;
      }
    }

    /// <summary>
    /// Need to visit closures as well
    /// </summary>
    /// <param name="cons"></param>
    /// <returns></returns>
    public override Expression VisitConstruct(Construct cons)
    {
      if (cons.Type.IsDelegateType())
      {
        UnaryExpression ue = cons.Operands[1] as UnaryExpression;
        if (ue == null) goto JustVisit;
        MemberBinding mb = ue.Operand as MemberBinding;
        if (mb == null) goto JustVisit;
        Method m = mb.BoundMember as Method;
        m = HelperMethods.Unspecialize(m);
        if (HelperMethods.IsAnonymousDelegate(m, this.CurrentType))
        {
          this.VisitAnonymousDelegate(m);
        }
      }
    JustVisit:
      return base.VisitConstruct(cons);
    }

    Stack<Method> delegates = new Stack<Method>();

    public virtual void VisitAnonymousDelegate(Method method)
    {
      if (method == null) return;

      delegates.Push(method);
      try
      {
        this.VisitBlock(method.Body);
      } finally
      {
        delegates.Pop();
      }
    }
  }

  internal class AsyncReturnValueQuery : InspectorIncludingClosures
  {
    bool foundReturnValueTaskResult;
    ContractNodes contractNodes;
    private TypeNode actualResultType;

    public AsyncReturnValueQuery(ContractNodes contractNodes, Method currentMethod, TypeNode actualResultType)
    {
      this.contractNodes = contractNodes;
      this.CurrentMethod = currentMethod;
      this.actualResultType = actualResultType;
    }

    /// <summary>
    /// actualReturn type is null if Task is not generic, otherwise ,the Task result type.
    /// </summary>
    public static bool Contains(Node node, ContractNodes contractNodes, Method currentMethod, TypeNode actualReturnType)
    {
        var v = new AsyncReturnValueQuery(contractNodes, currentMethod, actualReturnType);
        v.Visit(node);
        return v.foundReturnValueTaskResult;
    }

    public override void VisitMethodCall(MethodCall call)
    {
      MemberBinding mb = call.Callee as MemberBinding;
      if (mb == null) return;
      Method calledMethod = mb.BoundMember as Method;
      if (calledMethod == null) return;
      Method template = calledMethod.Template;

      if (template != null && template.Name.Name == "get_Result" && template.DeclaringType.Name.Name == "Task`1")
      {
          // check if callee is result
          var innercall = mb.TargetObject as MethodCall;
          if (innercall != null)
          {
              MemberBinding mb2 = innercall.Callee as MemberBinding;
              if (mb2 != null)
              {
                  Method calledMethod2 = mb2.BoundMember as Method;
                  if (calledMethod2 != null)
                  {
                      Method template2 = calledMethod2.Template;
                      if (contractNodes.IsResultMethod(template2))
                      {
                          this.foundReturnValueTaskResult = true;
                          //return new ReturnValue(calledMethod.DeclaringType.TemplateArguments[0]);
                          return;
                      }
                  }
              }
          }
      }
      if (this.actualResultType != null && contractNodes.IsResultMethod(template) && calledMethod.ReturnType == this.actualResultType) 
      {
          // using Contract.Result<T>() in a Task<T> returning method, this is a shorthand for
          // Contract.Result<Task<T>>().Result
          this.foundReturnValueTaskResult = true;
          return;
      }
      base.VisitMethodCall(call);
    }

    
  }

  /// <summary>
  /// Used to perform the translation from Contract.OldValue -> OldValue expression and
  /// same for Result and ValueAtReturn in the extracted contract and its used closure methods.
  /// </summary>
  internal class ExtractionFinalizer : VisitorIncludingClosures
  {
    ContractNodes contractNodes;
    private Method declaringMethod;
    public ExtractionFinalizer(ContractNodes contractNodes)
    {
      this.contractNodes = contractNodes;
    }

    StatementList currentSL;
    int currentSLindex;

    public override StatementList VisitStatementList(StatementList statements)
    {
      var oldSL = this.currentSL;
      var oldSLi = this.currentSLindex;
      this.currentSL = statements;

      try
      {
        if (statements == null) return null;
        for (int i = 0, n = statements.Count; i < n; i++)
        {
          this.currentSLindex = i;
          statements[i] = (Statement)this.Visit(statements[i]);
        }
        return statements;
      }
      finally
      {
        this.currentSLindex = oldSLi;
        this.currentSL = oldSL;
      }
    }

    public override MethodContract VisitMethodContract(MethodContract contract)
    {
        this.declaringMethod = contract.DeclaringMethod;

        return base.VisitMethodContract(contract);
    }
    public override Expression VisitMethodCall(MethodCall call)
    {
      MemberBinding mb = call.Callee as MemberBinding;
      if (mb == null) return call;
      Method calledMethod = mb.BoundMember as Method;
      if (calledMethod == null) return call;
      Method template = calledMethod.Template;
      if (contractNodes.IsOldMethod(template))
      {
        OldExpression oe = new OldExpression(ExtractOldExpression(call));
        oe.Type = call.Type;
        return oe;
      }
      if (contractNodes.IsValueAtReturnMethod(template))
      {
        return new AddressDereference(call.Operands[0], calledMethod.TemplateArguments[0], call.SourceContext);
      }
      if (contractNodes.IsResultMethod(template))
      {
        // check if we are in an Task returning method
          if (this.declaringMethod != null && this.declaringMethod.ReturnType != null)
          {
              var rt = this.declaringMethod.ReturnType;
              var templ = rt.Template;
              if (templ != null && templ.Name.Name == "Task`1" && rt.TemplateArguments != null && rt.TemplateArguments.Count == 1)
              {
                  var targ = rt.TemplateArguments[0];
                  if (calledMethod.TemplateArguments[0]== targ) {
                      // use of ReturnValue<T>() instead of ReturnValue<Task<T>>().Result
                      var retExp = new ReturnValue(rt, call.SourceContext);
                      var resultProp = rt.GetProperty(Identifier.For("Result"));
                      if (resultProp != null && resultProp.Getter != null)
                      {
                          return new MethodCall(new MemberBinding(retExp, resultProp.Getter), new ExpressionList());
                      }
                  }
              }

          }
        return new ReturnValue(calledMethod.ReturnType, call.SourceContext);
      }
      return base.VisitMethodCall(call);
    }

    private Expression ExtractOldExpression(MethodCall call)
    {
      var cand = call.Operands[0];

      if (this.currentSL != null)
      {
        var locs = FindLocals.Get(cand);
        if (locs.Count > 0)
        {
          // find the instructions that set these locals
          var assignments = new List<Statement>();
          for (int i = this.currentSLindex - 1; i >= 0; i--)
          {
            var a = this.currentSL[i] as AssignmentStatement;
            if (a == null) continue;
            var loc = a.Target as Local;
            if (loc == null) continue;
            if (locs.Contains(loc))
            {
              assignments.Add(a);
              this.currentSL[i] = null;
              locs.Remove(loc);
            }
            if (locs.Count == 0) break;
          }
          assignments.Reverse();
          var be = new StatementList();
          assignments.ForEach(astmt => be.Add(astmt));
          be.Add(new ExpressionStatement(cand));
          var sc = cand.SourceContext;
          cand = new BlockExpression(new Block(be));
          cand.SourceContext = sc;

          if (locs.Count > 0)
          {
            // warn that we couldn't extract the local
          }
        }
      }
      return cand;
    }

  }

  internal class FindLocals : Inspector
  {
    Set<Local> locals = new Set<Local>();

    private FindLocals() { }

    public static Set<Local> Get(Expression e)
    {
      var n = new FindLocals();
      n.Visit(e);
      return n.locals;
    }

    public override void VisitLocal(Local local)
    {
      this.locals.Add(local);
    }
  }

  /// <summary>
  /// Removes some attributes we don't want in the rewritten assembly
  /// </summary>
  internal class FilterForRuntime : StandardVisitor
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    ContractNodes contractNodes;
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    ContractNodes targetContractNodes;
    public FilterForRuntime(ContractNodes contractNodes, ContractNodes targetContractNodes) {
      this.contractNodes = contractNodes;
      this.targetContractNodes = targetContractNodes;
    }
    public override AttributeNode VisitAttributeNode(AttributeNode attribute) {
      if (attribute == null) return null;
      if (attribute.Type.Namespace != null && ContractNodes.ContractNamespace.Matches(attribute.Type.Namespace))
      {
        switch (attribute.Type.Name.Name)
        {
          case "ContractClassAttribute":
          case "ContractInvariantMethodAttribute":
          case "ContractClassForAttribute":
          case "ContractVerificationAttribute":
          case "ContractPublicPropertyNameAttribute":
          case "ContractArgumentValidatorAttribute":
          case "ContractAbbreviatorAttribute":
          case "ContractOptionAttribute":
          case "ContractRuntimeIgnoredAttribute":
          case "PureAttribute":
            return attribute;  
          default:
            return null; // Don't propagate any other attributes from System.Diagnostics.Contracts, they might be types defined only in mscorlib.contracts.dll
        }
      }
      return base.VisitAttributeNode(attribute);
    }

    public override Method VisitMethod(Method method)
    {
      if (method == null) return null;
      this.VisitAttributeList(method.Attributes);
      this.VisitAttributeList(method.ReturnAttributes);
      // don't visit further into the method.
      return method;
    }

    internal AssemblyNode TransformForTarget(AssemblyNode assemblyToVisit)
    {
      return this.VisitAssembly(assemblyToVisit);
    }
  }

}


