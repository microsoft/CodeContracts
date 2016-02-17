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
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace System.Compiler{
  /* The idea here is to do a tree traversal of the IR graph, rewriting the IR with duplicate nodes from the bottom up. Nodes that may appear
   * more than once in the graph keep track of their duplicates in the DuplicateFor hashtable and all references to these nodes are replaced
   * with references to the corresponding duplicates.
   * 
   * A complication arises because of the need to duplicate IR subgraphs, such as Methods, Types, CompilationUnits and individual Modules.
   * The subgraphs contain references to "foreign" nodes that should not be duplicated and it is thus necessary to be able to tell whether
   * or not a node should be duplicated. This done by tracking all the types that are members of the subgraph to be duplicated in the
   * TypesToBeDuplicated hashtable. Types are duplicated only if they are members of this table, while fields and methods are duplicated
   * only if their declaring types are members of this table. 
   * 
   * Since every type contains a reference to its declaring module, the module in which duplicated types will be inserted must be specified
   * to the constructor.
   * */

  /// <summary>
  /// Walks an IR, duplicating it while fixing up self references to point to the duplicate IR. Only good for one duplication. 
  /// Largest unit of duplication is a single module.
  /// </summary>
  public class Duplicator : StandardVisitor{
    public TrivialHashtable/*!*/ DuplicateFor { get; private set; }
    public TrivialHashtable/*!*/ TypesToBeDuplicated { get; private set; }
    public Module/*!*/ TargetModule;
    public TypeNode TargetType;
    public Method TargetMethod;
    public TypeNode OriginalTargetType;
    public bool SkipBodies;
    public bool RecordOriginalAsTemplate;
    public bool CopyDocumentation;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(TypesToBeDuplicated != null);
      Contract.Invariant(DuplicateFor != null);
    }

    /// <param name="module">The module into which the duplicate IR will be grafted.</param>
    /// <param name="type">The type into which the duplicate Member will be grafted. Ignored if entire type, or larger unit is duplicated.</param>
    public Duplicator(Module/*!*/ module, TypeNode type)
      : this(module, type, 4)
    {
      
    }
    /// <param name="module">The module into which the duplicate IR will be grafted.</param>
    /// <param name="type">The type into which the duplicate Member will be grafted. Ignored if entire type, or larger unit is duplicated.</param>
    /// <param name="initialCapacity">initial capacity of dup forwarding table. Default 4</param>
    public Duplicator(Module/*!*/ module, TypeNode type, int initialCapacity)
    {
      this.TargetModule = module;
      this.TargetType = this.OriginalTargetType = type;
      this.DuplicateFor = new TrivialHashtable(initialCapacity);
      this.TypesToBeDuplicated = new TrivialHashtable();
      //^ base();
    }
    public virtual void FindTypesToBeDuplicated(NamespaceList namespaces){
      if (namespaces == null) return;
      for (int i = 0, n = namespaces.Count; i < n; i++){
        Namespace nspace = namespaces[i];
        if (nspace == null) continue;
        this.FindTypesToBeDuplicated(nspace.Types);
      }
    }
    public virtual void FindTypesToBeDuplicated(TypeNodeList types){
      if (types == null) return;
      for (int i = 0, n = types.Count; i < n; i++){
        TypeNode t = types[i];
        RegisterTypeToBeDuplicated(t);
      }
    }
    bool RegisterTypeToBeDuplicated(TypeNode t)
    {
      if (t == null) return false;
      if (this.DuplicateFor[t.UniqueKey] != null) return false;
      if (this.TypesToBeDuplicated[t.UniqueKey] != null) return false;
      Debug.Assert(TypeNode.IsCompleteTemplate(t));

      this.TypesToBeDuplicated[t.UniqueKey] = t;
      // dup the type now and fill it in later
      var dup = this.VisitTypeNode(t, null, null, null, true);
      var savedTargetType = this.TargetType;
      this.TargetType = dup;
      this.FindTypesToBeDuplicated(t.TemplateParameters);
      this.FindMembersToBeDuplicated(t.Members);
      this.TargetType = savedTargetType;
      return true;
    }
    bool RegisterMemberToBeDuplicated(Member m)
    {
      if (m == null) return false;
      TypeNode nested = m as TypeNode;
      if (nested != null) return RegisterTypeToBeDuplicated(nested);

      return false;
    }
    public virtual void FindMembersToBeDuplicated(MemberList members)
    {
      if (members == null) return;
      for (int i = 0; i < members.Count; i++)
      {
        RegisterMemberToBeDuplicated(members[i]);
      }
    }
    public override Node Visit(Node node)
    {
      node = base.Visit(node);
      Expression e = node as Expression;
      if (e != null) e.Type = this.VisitTypeReference(e.Type);
      return node;
    }
    public override Expression VisitAddressDereference(AddressDereference addr){
      if (addr == null) return null;
      return base.VisitAddressDereference((AddressDereference)addr.Clone());
    }
    public override AssemblyNode VisitAssembly(AssemblyNode assembly){
      if (assembly == null) return null;
      this.FindTypesToBeDuplicated(assembly.Types);
      return base.VisitAssembly((AssemblyNode)assembly.Clone());
    }
    public override AssemblyReference VisitAssemblyReference(AssemblyReference assemblyReference){
      if (assemblyReference == null) return null;
      return base.VisitAssemblyReference((AssemblyReference)assemblyReference.Clone());
    }

    public override Statement VisitAssertion(Assertion assertion){
      if (assertion == null) return null;
      return base.VisitAssertion((Assertion)assertion.Clone());
    }
    public override Statement VisitAssumption(Assumption Assumption){
      if (Assumption == null) return null;
      return base.VisitAssumption((Assumption)Assumption.Clone());
    }
    public override Expression VisitAssignmentExpression(AssignmentExpression assignment){
      if (assignment == null) return null;
      return base.VisitAssignmentExpression((AssignmentExpression)assignment.Clone());
    }

    public override Statement VisitAssignmentStatement(AssignmentStatement assignment){
      if (assignment == null) return null;
      return base.VisitAssignmentStatement((AssignmentStatement)assignment.Clone());
    }
    public override Expression VisitAttributeConstructor(AttributeNode attribute){
      if (attribute == null || attribute.Constructor == null) return null;
      return this.VisitExpression((Expression)attribute.Constructor.Clone());
    }
    public override AttributeNode VisitAttributeNode(AttributeNode attribute){
      if (attribute == null) return null;
      return base.VisitAttributeNode((AttributeNode)attribute.Clone());
    }
    public override AttributeList VisitAttributeList(AttributeList attributes){
      Contract.Ensures(Contract.Result<AttributeList>() != null || attributes == null);

      if (attributes == null) return null;
      return base.VisitAttributeList(attributes.Clone());
    }
    public override Expression VisitBinaryExpression(BinaryExpression binaryExpression){
      if (binaryExpression == null) return null;
      binaryExpression = (BinaryExpression)base.VisitBinaryExpression((BinaryExpression)binaryExpression.Clone());
      return binaryExpression;
    }
    public override Block VisitBlock(Block block){
      if (block == null) return null;
      Block dup = (Block)this.DuplicateFor[block.UniqueKey];
      if (dup != null) return dup;     
      this.DuplicateFor[block.UniqueKey] = dup = (Block)block.Clone();
      return base.VisitBlock(dup);
    }

    public override Expression VisitBlockExpression(BlockExpression blockExpression){
      if (blockExpression == null) return null;
      return base.VisitBlockExpression((BlockExpression)blockExpression.Clone());
    }
    public override BlockList VisitBlockList(BlockList blockList){
      if (blockList == null) return null;
      return base.VisitBlockList(blockList.Clone());
    }
    public override Statement VisitBranch(Branch branch){
      if (branch == null) return null;
      branch = (Branch)base.VisitBranch((Branch)branch.Clone());
      if (branch == null) return null;
      branch.Target = this.VisitBlock(branch.Target);
      return branch;
    }
    public override Expression VisitConstruct(Construct cons){
      if (cons == null) return null;
      return base.VisitConstruct((Construct)cons.Clone());
    }
    public override Expression VisitConstructArray(ConstructArray consArr){
      if (consArr == null) return null;
      return base.VisitConstructArray((ConstructArray)consArr.Clone());
    }
 
    public override DelegateNode VisitDelegateNode(DelegateNode delegateNode){
      return this.VisitTypeNode(delegateNode) as DelegateNode;
    }
    public override Statement VisitEndFilter(EndFilter endFilter){
      if (endFilter == null) return null;
      return base.VisitEndFilter((EndFilter)endFilter.Clone());
    }
    public override Statement VisitEndFinally(EndFinally endFinally){
      if (endFinally == null) return null;
      return base.VisitEndFinally((EndFinally)endFinally.Clone());
    }
    public override EnsuresList VisitEnsuresList(EnsuresList Ensures){
      if (Ensures == null) return null;
      return base.VisitEnsuresList(Ensures.Clone());
    }
    public override Event VisitEvent(Event evnt){
      if (evnt == null) return null;
      Event dup = (Event)this.DuplicateFor[evnt.UniqueKey];
      if (dup != null) return dup;
      this.DuplicateFor[evnt.UniqueKey] = dup = (Event)evnt.Clone();
      if (this.CopyDocumentation) dup.Documentation = evnt.Documentation;
      dup.HandlerAdder = this.VisitMethod(evnt.HandlerAdder);
      dup.HandlerCaller = this.VisitMethod(evnt.HandlerCaller);
      dup.HandlerRemover = this.VisitMethod(evnt.HandlerRemover);
      dup.OtherMethods = this.VisitMethodList(evnt.OtherMethods);
      dup.DeclaringType = this.TargetType;
      return base.VisitEvent(dup);
    }

    public virtual ExceptionHandler VisitExceptionHandler(ExceptionHandler handler){
      if (handler == null) return null;
      handler = (ExceptionHandler)handler.Clone();
      handler.BlockAfterHandlerEnd = this.VisitBlock(handler.BlockAfterHandlerEnd);
      handler.BlockAfterTryEnd = this.VisitBlock(handler.BlockAfterTryEnd);
      handler.FilterExpression = this.VisitBlock(handler.FilterExpression);
      handler.FilterType = this.VisitTypeReference(handler.FilterType);
      handler.HandlerStartBlock = this.VisitBlock(handler.HandlerStartBlock);
      handler.TryStartBlock = this.VisitBlock(handler.TryStartBlock);
      return handler;
    }
    public virtual ExceptionHandlerList VisitExceptionHandlerList(ExceptionHandlerList handlers){
      if (handlers == null) return null;
      int n = handlers.Count;
      ExceptionHandlerList result = new ExceptionHandlerList(n);
      for (int i = 0; i < n; i++)
        result.Add(this.VisitExceptionHandler(handlers[i]));
      return result;
    }

    public override EnsuresExceptional VisitEnsuresExceptional(EnsuresExceptional exceptional)
    {
      if (exceptional == null) return null;
      return base.VisitEnsuresExceptional((EnsuresExceptional)exceptional.Clone());
    }
    public override Expression VisitExpression(Expression expression){
      if (expression == null) return null;
      switch(expression.NodeType){
        case NodeType.Dup: 
        case NodeType.Arglist:
          expression = (Expression)expression.Clone();
          break;
        case NodeType.Pop:
          UnaryExpression uex = expression as UnaryExpression;
          if (uex != null){
            uex = (UnaryExpression)uex.Clone();
            uex.Operand = this.VisitExpression(uex.Operand);
            expression = uex;
          }else
            expression = (Expression)expression.Clone();
          break;
        default:
          expression = (Expression)this.Visit(expression);
          break;
      }
      if (expression == null) return null;
      expression.Type = this.VisitTypeReference(expression.Type);
      return expression;
    }
    public override ExpressionList VisitExpressionList(ExpressionList expressions){
      if (expressions == null) return null;
      return base.VisitExpressionList(expressions.Clone());
    }
    public override Statement VisitExpressionStatement(ExpressionStatement statement){
      if (statement == null) return null;
      return base.VisitExpressionStatement((ExpressionStatement)statement.Clone());
    }

    public override Statement VisitFaultHandler(FaultHandler faultHandler){
      if (faultHandler == null) return null;
      return base.VisitFaultHandler((FaultHandler)faultHandler.Clone());
    }
    public override FaultHandlerList VisitFaultHandlerList(FaultHandlerList faultHandlers){
      if (faultHandlers == null) return null;
      return base.VisitFaultHandlerList(faultHandlers.Clone());
    }

    public override Field VisitField(Field field){
      if (field == null) return null;
      Field dup = (Field)this.DuplicateFor[field.UniqueKey];
      if (dup != null) return dup;
      this.DuplicateFor[field.UniqueKey] = dup = (Field)field.Clone();
      if (field.MarshallingInformation != null)
        dup.MarshallingInformation = field.MarshallingInformation.Clone();

      ParameterField pField = dup as ParameterField;
      if (pField != null)
        pField.Parameter = (Parameter)this.VisitParameter(pField.Parameter);

      dup.DeclaringType = this.TargetType;

      if (this.CopyDocumentation) dup.Documentation = field.Documentation;

      return base.VisitField(dup);
    }
    public override Expression VisitIdentifier(Identifier identifier){
      if (identifier == null) return null;
      return base.VisitIdentifier((Identifier)identifier.Clone());
    }
    public override Expression VisitIndexer(Indexer indexer){
      if (indexer == null) return null;
      indexer = (Indexer)base.VisitIndexer((Indexer)indexer.Clone());
      if (indexer == null) return null;
      indexer.ElementType = this.VisitTypeReference(indexer.ElementType);
      return indexer;
    }
    public override InterfaceList VisitInterfaceReferenceList(InterfaceList interfaceReferences){
      if (interfaceReferences == null) return null;
      return base.VisitInterfaceReferenceList(interfaceReferences.Clone());
    }
    public override InvariantList VisitInvariantList(InvariantList Invariants){
      if (Invariants == null) return null;
      return base.VisitInvariantList(Invariants.Clone());
    }

    public override Statement VisitLabeledStatement(LabeledStatement lStatement){
      if (lStatement == null) return null;
      return base.VisitLabeledStatement((LabeledStatement)lStatement.Clone());
    }

    public override Expression VisitLiteral(Literal literal){
      if (literal == null) return null;
      TypeNode cloneType = this.VisitTypeReference(literal.Type);
      TypeNode t = literal.Value as TypeNode;
      if (t != null)
        return new Literal(this.VisitTypeReference(t), cloneType, literal.SourceContext);
      TypeNode[] tarr = literal.Value as TypeNode[];
      if (tarr != null) {
        int len = tarr == null ? 0 : tarr.Length;
        TypeNode[] newarr = tarr == null ? null : new TypeNode[len];
        for (int i = 0; i < len; i++) newarr[i] = this.VisitTypeReference(tarr[i]);
        return new Literal(newarr, cloneType);
      }
      object[] arr = literal.Value as object[];
      if (arr != null) {
        int len = arr.Length;
        object[] newarr = new object[len];
        for (int i = 0; i < len; i++) {
          Literal litelt = arr[i] as Literal;
          if (litelt != null)
            newarr[i] = VisitLiteral(litelt);
          else
            newarr[i] = arr[i];
        }
        return new Literal(newarr, cloneType);
      }
      Literal result = (Literal)literal.Clone();
      result.Type = cloneType;
      return result;
    }
    public override Expression VisitLocal(Local local){
      if (local == null) return null;
      Local dup = (Local)this.DuplicateFor[local.UniqueKey];
      if (dup != null) return dup;
      this.DuplicateFor[local.UniqueKey] = dup = (Local)local.Clone();
      return base.VisitLocal(dup);
    }
    public override Expression VisitMemberBinding(MemberBinding memberBinding){
      if (memberBinding == null) return null;
      memberBinding = (MemberBinding)memberBinding.Clone();
      memberBinding.TargetObject = this.VisitExpression(memberBinding.TargetObject);
      memberBinding.Type = this.VisitTypeReference(memberBinding.Type);
      memberBinding.BoundMember = this.VisitMemberReference(memberBinding.BoundMember);
      return memberBinding;
    }
    public override MemberList VisitMemberList(MemberList members){
      if (members == null) return null;
      var dup = members.Clone();
      for (int i = 0; i < dup.Count; i++)
      {
        var member = dup[i];
        if (this.RecordOriginalAsTemplate && member is TypeNode)
        {
          dup[i] = null;
        }
        else
        {
          dup[i] = (Member)this.Visit(member);
          Debug.Assert(member == null || dup[i] != null);
        }
      }
      return dup;
    }
    public virtual Member VisitMemberReference(Member member){
      if (member == null) return null;
      Member dup = (Member)this.DuplicateFor[member.UniqueKey];
      if (dup != null) return dup;
      TypeNode t = member as TypeNode;
      if (t != null) return this.VisitTypeReference(t);

      if (this.RecordOriginalAsTemplate) return member; // mapping done in Specializer
      Method method = member as Method;
      if (method != null && method.Template != null && method.TemplateArguments != null && method.TemplateArguments.Count > 0){
        Method template = this.VisitMemberReference(method.Template) as Method;
        bool needNewInstance = template != null && template != method.Template;
        TypeNodeList args = method.TemplateArguments.Clone();
        for (int i = 0, n = args.Count; i < n; i++){
          TypeNode arg = this.VisitTypeReference(args[i]);
          if (arg != null && arg != args[i]){
            args[i] = arg;
            needNewInstance = true;
          }
        }
        if (needNewInstance) {
          //^ assert template != null;
          return template.GetTemplateInstance(this.TargetType, args);
        }
        return method;
      }
      TypeNode declaringType = this.VisitTypeReference(member.DeclaringType);
      if (declaringType == null) return member;
      if (declaringType == member.DeclaringType) return member;
      // this could delay things...if (declaringType.Template == null && this.TypesToBeDuplicated[declaringType.UniqueKey] == null) return member;
      // TypeNode tgtType = this.VisitTypeReference(declaringType); //duplicates its members

      dup = (Member)this.DuplicateFor[member.UniqueKey];
      if (dup == null){
        dup = Specializer.GetCorrespondingMember(member, declaringType);
        if (dup != null) return dup;
        Debug.Assert(false);
        //Can get here when declaringType has not yet been completely duplicated
        TypeNode savedTargetType = this.TargetType;
        this.TargetType = declaringType;
        dup = (Member)this.Visit(member);
        this.TargetType = savedTargetType;

      }
      return dup;
    }
    public virtual MemberList VisitMemberReferenceList(MemberList members){
      if (members == null) return null;
      int n = members.Count;
      MemberList dup = new MemberList(n);
      for (int i = 0; i < n; i++)
        dup.Add(this.VisitMemberReference(members[i]));
      return dup;
    }
    public readonly Block DummyBody = new Block();

    public override Method VisitMethod(Method method) {
      if (method == null) return null;
      Method dup = (Method)this.DuplicateFor[method.UniqueKey];
      if (dup != null) return dup;

      if (TargetPlatform.UseGenerics
          && !this.RecordOriginalAsTemplate
         )
      {
          // leave generic template parameters unchanged if we create an instance
          this.FindTypesToBeDuplicated(method.TemplateParameters);
      }
      return VisitMethodInternal(method);
    }

    /// <summary>
    /// Does not copy the method's template parameters unless they are marked for duplication already.
    /// </summary>
    public Method VisitMethodInternal(Method method){
      if (method == null) return null;
      Method dup = (Method)this.DuplicateFor[method.UniqueKey];
      if (dup != null) return dup;
      this.DuplicateFor[method.UniqueKey] = dup = (Method)method.Clone();
      dup.ProviderHandle = null;
      dup.LocalList = null;
      Method savedTarget = this.TargetMethod;
      this.TargetMethod = dup;
      var savedTemplateParameters = method.TemplateParameters;
      if (TargetPlatform.UseGenerics
          && !this.RecordOriginalAsTemplate
          )
      {
          dup.TemplateParameters = this.VisitTypeParameterList(savedTemplateParameters);
          savedTemplateParameters = null;
      }
      else
      {
          dup.TemplateParameters = null; // avoid visiting them
      }

      if (this.CopyDocumentation) dup.Documentation = method.Documentation;

      dup.OverriddenMember = null; // let instantiation be recomputed
      dup.ImplementedInterfaceMethods = this.VisitMethodReferenceList(method.ImplementedInterfaceMethods);
      dup.ImplicitlyImplementedInterfaceMethods = null; // just reset it so it gets recomputed if ever asked for
      dup.DeclaringType = this.TargetType;
      if (!method.IsAbstract) dup.Body = this.DummyBody;
      if (this.RecordOriginalAsTemplate) {
        if (method.Template != null)
          dup.Template = method.Template;
        else
          dup.Template = method;
      }
      dup.PInvokeModule = this.VisitModuleReference(dup.PInvokeModule);
      if (method.ReturnTypeMarshallingInformation != null)
        dup.ReturnTypeMarshallingInformation = method.ReturnTypeMarshallingInformation.Clone();
      dup.ThisParameter = (This)this.VisitParameter(dup.ThisParameter);
      dup.ProvideContract = null;
      dup.contract = null;
      dup = base.VisitMethod(dup);
      //^ assume dup != null;

      // restore template parameters if we need to
      if (savedTemplateParameters != null)
      {
          dup.TemplateParameters = savedTemplateParameters.Clone();
          savedTemplateParameters = null;
      }
      // Visiting the declaring member can cause this method to be re-entered,
      // so only visit after we duplicated the other properties so that we 
      // do not return a half-duplicated method.

      // it doesn't make sense to copy a getter/setter/event method without the corresponding event
      // (one can always pre-populate if necessary)
      dup.DeclaringMember = (Member)this.Visit(dup.DeclaringMember);
      dup.fullName = null;
      dup.DocumentationId = null;
      dup.ProviderHandle = method; // we always need the handle, as we may use it for attributes.
      dup.Attributes = null;
      dup.ProvideMethodAttributes = new Method.MethodAttributeProvider(this.ProvideMethodAttributes);
      dup.ProvideContract = new Method.MethodContractProvider(this.ProvideMethodContract);
      if (!this.SkipBodies && !method.IsAbstract)
      {
        dup.Body = null;
        dup.ProvideBody = new Method.MethodBodyProvider(this.ProvideMethodBody);
      }
      if (this.SkipBodies) dup.Instructions = new InstructionList(0);
      
      this.TargetMethod = savedTarget;
      return dup;
    }
    public override Expression VisitMethodCall(MethodCall call){
      if (call == null) return null;
      return base.VisitMethodCall((MethodCall)call.Clone());
    }
    public override MethodContract VisitMethodContract(MethodContract contract)
    {
      if (contract == null) return null;
      MethodContract dup = (MethodContract)this.DuplicateFor[contract.UniqueKey];
      if (dup != null) return dup;
      //Make sure not to break the relation between contract.LocalForResult and
      //references to contract.LocalForResult in the contract:
      //Revised: seems that code for implementing an interface property depends on relation being broken.
      /*
      Local localForResult = contract.LocalForResult;
      if (localForResult != null) {
        localForResult = new Local(localForResult.Name, localForResult.Type);
        this.DuplicateFor[contract.LocalForResult.UniqueKey] = localForResult;
      }
      */      
      dup = (MethodContract)contract.Clone();
      //contract.LocalForResult = localForResult;
      //^ assume this.TargetMethod != null;
      dup.contractInitializer = this.VisitBlock(contract.ContractInitializer);
      dup.postPreamble = this.VisitBlock(contract.PostPreamble);
      dup.DeclaringMethod = this.TargetMethod;
      dup.ensures = this.VisitEnsuresList(contract.Ensures);
      dup.asyncEnsures = this.VisitEnsuresList(contract.AsyncEnsures);
      dup.modelEnsures = this.VisitEnsuresList(contract.ModelEnsures);
      dup.modifies = this.VisitExpressionList(contract.Modifies);
      dup.requires = this.VisitRequiresList(contract.Requires);
      return dup;
    }
    public virtual MethodList VisitMethodList(MethodList methods)
    {
      if (methods == null) return null;
      int n = methods.Count;
      MethodList dup = new MethodList(n);
      for (int i = 0; i < n; i++)
        dup.Add(this.VisitMethod(methods[i]));
      return dup;
    }
    public virtual MethodList VisitMethodReferenceList(MethodList methods){
      if (methods == null) return null;
      int n = methods.Count;
      MethodList dup = new MethodList(n);
      for (int i = 0; i < n; i++)
        dup.Add((Method)this.VisitMemberReference(methods[i]));
      return dup;
    }
    public override Module VisitModule(Module module){
      if (module == null) return null;
      Module dup = (Module)module.Clone();
      if (this.TargetModule == null) this.TargetModule = dup;
      this.FindTypesToBeDuplicated(module.Types);
      return base.VisitModule(dup);
    }
    public virtual Module VisitModuleReference(Module module){
      if (module == null) return null;
      Module dup = (Module)this.DuplicateFor[module.UniqueKey];
      if (dup != null) return dup;
      for (int i = 0, n = this.TargetModule.ModuleReferences == null ? 0 : this.TargetModule.ModuleReferences.Count; i < n; i++){
        //^ assert this.TargetModule.ModuleReferences != null;
        ModuleReference modRef = this.TargetModule.ModuleReferences[i];
        if (modRef == null) continue;
        if (string.Compare(module.Name, modRef.Name, true, System.Globalization.CultureInfo.InvariantCulture) != 0) continue;
        this.DuplicateFor[module.UniqueKey] = modRef.Module; return modRef.Module;
      }
      if (this.TargetModule.ModuleReferences == null)
        this.TargetModule.ModuleReferences = new ModuleReferenceList();
      this.TargetModule.ModuleReferences.Add(new ModuleReference(module.Name, module));
      this.DuplicateFor[module.UniqueKey] = module;
      return module;
    }
    public override ModuleReference VisitModuleReference(ModuleReference moduleReference){
      if (moduleReference == null) return null;
      return base.VisitModuleReference((ModuleReference)moduleReference.Clone());
    }
    public override Expression VisitNamedArgument(NamedArgument namedArgument){
      if (namedArgument == null) return null;
      return base.VisitNamedArgument((NamedArgument)namedArgument.Clone());
    }
    public override EnsuresNormal VisitEnsuresNormal(EnsuresNormal normal)
    {
      if (normal == null) return null;
      return base.VisitEnsuresNormal((EnsuresNormal)normal.Clone());
    }
    public override Expression VisitOldExpression (OldExpression oldExpression) {
      if (oldExpression == null) return null;
      return base.VisitOldExpression((OldExpression)oldExpression.Clone());
    }
    public override Expression VisitReturnValue(ReturnValue retval)
    {
      if (retval == null) return null;
      ReturnValue dup = (ReturnValue)this.DuplicateFor[retval.UniqueKey];
      if (dup != null) return dup;
      this.DuplicateFor[retval.UniqueKey] = dup = (ReturnValue)retval.Clone();
      return base.VisitReturnValue(dup);
    }
    public override RequiresOtherwise VisitRequiresOtherwise(RequiresOtherwise otherwise)
    {
      if (otherwise == null) return null;
      RequiresOtherwise dup = (RequiresOtherwise)this.DuplicateFor[otherwise.UniqueKey];
      if (dup != null) return dup;
      this.DuplicateFor[otherwise.UniqueKey] = dup = (RequiresOtherwise)otherwise.Clone();
      return base.VisitRequiresOtherwise(dup);
    }
    public override Expression VisitParameter(Parameter parameter)
    {
      if (parameter == null) return null;
      Parameter dup = (Parameter)this.DuplicateFor[parameter.UniqueKey];
      if (dup != null) {
        if (dup.DeclaringMethod == null) dup.DeclaringMethod = this.TargetMethod;
        return dup;
      }
      this.DuplicateFor[parameter.UniqueKey] = dup = (Parameter)parameter.Clone();
      if (dup.MarshallingInformation != null)
        dup.MarshallingInformation = dup.MarshallingInformation.Clone();
      dup.DeclaringMethod = this.TargetMethod;
      dup.paramArrayElementType = null;
      return base.VisitParameter(dup);
    }
    public override ParameterList VisitParameterList(ParameterList parameterList){
      if (parameterList == null) return null;
      return base.VisitParameterList(parameterList.Clone());
    }
    public override RequiresPlain VisitRequiresPlain(RequiresPlain plain)
    {
      if (plain == null) return null;
      var dup = (RequiresPlain)this.DuplicateFor[plain.UniqueKey];
      if (dup != null) return dup;
      this.DuplicateFor[plain.UniqueKey] = dup = (RequiresPlain)plain.Clone();

      var result = base.VisitRequiresPlain(dup);
      return result;
    }
    public override Expression VisitPrefixExpression(PrefixExpression pExpr)
    {
      if (pExpr == null) return null;
      return base.VisitPrefixExpression((PrefixExpression)pExpr.Clone());
    }
    public override Expression VisitPostfixExpression(PostfixExpression pExpr){
      if (pExpr == null) return null;
      return base.VisitPostfixExpression((PostfixExpression)pExpr.Clone());
    }
    public override Property VisitProperty(Property property){
      if (property == null) return null;
      Property dup = (Property)this.DuplicateFor[property.UniqueKey];
      if (dup != null) return dup;
      this.DuplicateFor[property.UniqueKey] = dup = (Property)property.Clone();
      dup.Attributes = this.VisitAttributeList(property.Attributes);
      if (this.CopyDocumentation) dup.Documentation = property.Documentation;
      dup.Type = this.VisitTypeReference(property.Type);
      dup.Getter = this.VisitMethod(property.Getter);
      dup.Setter = this.VisitMethod(property.Setter);
      dup.OtherMethods = this.VisitMethodList(property.OtherMethods);
      dup.DeclaringType = this.TargetType;
      dup.Parameters = this.VisitParameterList(dup.Parameters);
      return dup;
    }
    public override RequiresList VisitRequiresList(RequiresList Requires)
    {
      if (Requires == null) return null;
      return base.VisitRequiresList(Requires.Clone());
    }
    public override Statement VisitReturn(Return Return)
    {
      if (Return == null) return null;
      return base.VisitReturn((Return)Return.Clone());
    }
    public override SecurityAttribute VisitSecurityAttribute(SecurityAttribute attribute){
      if (attribute == null) return null;
      return base.VisitSecurityAttribute((SecurityAttribute)attribute.Clone());;
    }
    public override SecurityAttributeList VisitSecurityAttributeList(SecurityAttributeList attributes){
      if (attributes == null) return null;
      return base.VisitSecurityAttributeList(attributes.Clone());
    }
    public override StatementList VisitStatementList(StatementList statements){
      if (statements == null) return null;
      return base.VisitStatementList(statements.Clone());
    }
    public override Statement VisitSwitchInstruction(SwitchInstruction switchInstruction){
      if (switchInstruction == null) return null;
      switchInstruction = (SwitchInstruction)base.VisitSwitchInstruction((SwitchInstruction)switchInstruction.Clone());
      if (switchInstruction == null) return null;
      switchInstruction.Targets = this.VisitBlockList(switchInstruction.Targets);
      return switchInstruction;
    }
    public override Expression VisitTernaryExpression(TernaryExpression expression){
      if (expression == null) return null;
      return base.VisitTernaryExpression((TernaryExpression)expression.Clone());
    }
    public override Expression VisitThis(This This){
      if (This == null) return null;
      This dup = (This)this.DuplicateFor[This.UniqueKey];
      if (dup != null) return dup;      
      this.DuplicateFor[This.UniqueKey] = dup = (This)This.Clone();
      return base.VisitThis(dup);
    }
    public override Statement VisitThrow(Throw Throw){
      if (Throw == null) return null;
      return base.VisitThrow((Throw)Throw.Clone());
    }
    public override TypeContract VisitTypeContract(TypeContract contract){
      if (this.RecordOriginalAsTemplate) return null; //A type template instance does not need invariants
      if (contract == null) return null;
      contract = (TypeContract)contract.Clone();
      contract.DeclaringType = this.VisitTypeReference(contract.DeclaringType);
      contract.Invariants = this.VisitInvariantList(contract.invariants);
      return contract;
    }
    public override TypeModifier VisitTypeModifier(TypeModifier typeModifier){
      if (typeModifier == null) return null;
      return base.VisitTypeModifier((TypeModifier)typeModifier.Clone());
    }
    public override TypeNode VisitTypeNode(TypeNode type){
      if (type == null) return null;
      TypeNode dup = this.VisitTypeNode(type, null, null, null, true);
      //^ assume dup != null;
      TypeNodeList nestedTypes = type.NestedTypes;
      if (nestedTypes != null && nestedTypes.Count > 0)
      {
        Debug.Assert(dup != type);
        this.VisitNestedTypes(dup, nestedTypes);
      }
      return dup;
    }
    internal TypeNode VisitTypeNode(TypeNode type, Identifier mangledName, TypeNodeList templateArguments, TypeNode template, bool delayVisitToNestedTypes){
      if (type == null) return null;
      Debug.Assert(this.TypesToBeDuplicated[type.UniqueKey] != null);
      TypeNode dup = (TypeNode)this.DuplicateFor[type.UniqueKey];
      if (dup != null) return dup;
      this.DuplicateFor[type.UniqueKey] = dup = (TypeNode)type.Clone();
      //if (mangledName != null)
      if (templateArguments != null)
      {
        //this.TargetModule.StructurallyEquivalentType[mangledName.UniqueIdKey] = dup;
        dup.TemplateArguments = templateArguments;
      }
      dup.arrayTypes = null;
      dup.constructors = null;
      dup.consolidatedTemplateArguments = null;
      dup.consolidatedTemplateParameters = null;
      dup.DocumentationId = null;
      if (this.CopyDocumentation) dup.Documentation = type.Documentation;
      dup.defaultMembers = null;
      dup.explicitCoercionFromTable = null;
      dup.explicitCoercionMethods = null;
      dup.implicitCoercionFromTable = null;
      dup.implicitCoercionMethods = null;
      dup.implicitCoercionToTable = null;
      dup.memberCount = 0;
      dup.memberTable = null;
      dup.modifierTable = null;
      dup.NestedTypes = null;
      dup.pointerType = null;
      dup.ProviderHandle = null;
      dup.ProvideTypeAttributes = null;
      dup.ProvideTypeMembers = null;
      dup.ProvideNestedTypes = null;
      dup.referenceType = null;
      dup.runtimeType = null;
      dup.structurallyEquivalentMethod = null;
      dup.ClearTemplateInstanceCache();
      TypeParameter tp = dup as TypeParameter;
      if (tp != null) tp.structuralElementTypes = null;
      ClassParameter cp = dup as ClassParameter;
      if (cp != null) cp.structuralElementTypes = null;
      dup.szArrayTypes = null;
      if (this.RecordOriginalAsTemplate && !(dup is ITypeParameter)) dup.Template = type;
      dup.TemplateArguments = null;
      dup.DeclaringModule = this.TargetModule;
      dup.DeclaringType = (dup is ITypeParameter) ? null : this.TargetType;
      dup.ProviderHandle = type;
      dup.Attributes = null;
      dup.SecurityAttributes = null;
      dup.ProvideTypeAttributes = new TypeNode.TypeAttributeProvider(this.ProvideTypeAttributes);
      Class c = dup as Class;
      if (c != null) { c.BaseClass = null; }
      dup.Interfaces = null;
      dup.templateParameters = null;
      dup.consolidatedTemplateParameters = null;
      dup.ProvideTypeSignature = new TypeNode.TypeSignatureProvider(this.ProvideTypeSignature);
      if (!this.RecordOriginalAsTemplate){
        dup.members = null;
        dup.ProvideTypeMembers = new TypeNode.TypeMemberProvider(this.ProvideTypeMembers);
      } else {
        dup.members = null;
        //dup.ProvideNestedTypes = new TypeNode.NestedTypeProvider(this.ProvideNestedTypes);
        dup.ProvideTypeMembers = new TypeNode.TypeMemberProvider(this.ProvideTypeMembers);
      }
      DelegateNode delegateNode = dup as DelegateNode;
      if (delegateNode != null){
        {
          delegateNode.Parameters = null;
          delegateNode.ReturnType = null;
        }
      }
      dup.Contract = null;
      dup.membersBeingPopulated = false;
      return dup;
    }
    private void ProvideTypeSignature(TypeNode/*!*/ dup, object/*!*/ handle)
    {
      TypeNode type = (TypeNode)handle;
      TypeNode savedTargetType = this.TargetType;
      this.TargetType = dup;
      this.TargetModule = dup.DeclaringModule;
      
      // There could be type instantiations of this thing during the following visits.
      // They need to know what type parameters we have.
      // Null check is here so if someone copies a type and then immediately updates the template parameters
      //  we don't override it
      if (!this.RecordOriginalAsTemplate && dup.templateParameters == null)
      {
        dup.TemplateParameters = this.VisitTypeReferenceList(type.TemplateParameters);
      }
      if (dup.DeclaringType == null && !(dup is ITypeParameter))
      {
        var originalDeclaringType = type.DeclaringType;
        var declaringType = this.VisitTypeReference(originalDeclaringType);
        if (originalDeclaringType != null && (declaringType == null || declaringType == originalDeclaringType))
        {
          dup.DeclaringType = this.OriginalTargetType;
        }
        else
        {
          dup.DeclaringType = declaringType;
        }
      }
      Class c = dup as Class;
      if (c != null && c.baseClass == null)
      {
        Class templateClass = (Class)type;
        c.BaseClass = (Class)this.VisitTypeReference(templateClass.BaseClass);
      }
      dup.Interfaces = this.VisitInterfaceReferenceList(type.Interfaces);

      this.TargetType = savedTargetType;
    }
    private void ProvideNestedTypes(TypeNode/*!*/ dup, object/*!*/ handle) {
      TypeNode template = (TypeNode)handle;
      TypeNode savedTargetType = this.TargetType;
      Module savedTargetModule = this.TargetModule;
      this.TargetType = dup;
      //^ assume dup.DeclaringModule != null;
      this.TargetModule = dup.DeclaringModule;
      this.FindTypesToBeDuplicated(template.NestedTypes);
      dup.NestedTypes = this.VisitNestedTypes(dup, template.NestedTypes);
      this.TargetModule = savedTargetModule;
      this.TargetType = savedTargetType;
    }
    private void ProvideTypeMembers(TypeNode/*!*/ dup, object/*!*/ handle) {
      TypeNode template = (TypeNode)handle;
      Debug.Assert(!template.membersBeingPopulated);
      TypeNode savedTargetType = this.TargetType;
      Module savedTargetModule = this.TargetModule;
      this.TargetType = dup;
      //^ assume dup.DeclaringModule != null;
      this.TargetModule = dup.DeclaringModule;
      //if (!this.RecordOriginalAsTemplate) this.FindTypesToBeDuplicated(template.NestedTypes);
      dup.Members = this.VisitMemberList(template.Members);
      DelegateNode delegateNode = dup as DelegateNode;
      if (delegateNode != null && delegateNode.IsNormalized){
        Debug.Assert(dup.Members != null && dup.Members.Count > 0 && dup.Members[0] != null);
        DelegateNode templateDelegateNode = template as DelegateNode;
        delegateNode.Parameters = this.VisitParameterList(templateDelegateNode.Parameters);
        delegateNode.ReturnType = this.VisitTypeReference(templateDelegateNode.ReturnType);
      }
      dup.Contract = this.VisitTypeContract(template.Contract);

      this.TargetModule = savedTargetModule;
      this.TargetType = savedTargetType;
    }
    protected virtual void ProvideMethodBody(Method/*!*/ dup, object/*!*/ handle, bool asInstructionList) {
      if (asInstructionList) {
        // We don't really have a way to provide instructions, but we set it to an empty list
        dup.Instructions = new InstructionList(0);
        return;
      }
      Method template = (Method)handle;
      Block tbody = template.Body;
      if (tbody == null){
        dup.ProvideBody = null;
        return;
      }
      TypeNode savedTargetType = this.TargetType;
      this.TargetType = dup.DeclaringType;
      dup.Body = this.VisitBlock(tbody);
      dup.ExceptionHandlers = this.VisitExceptionHandlerList(template.ExceptionHandlers);
      this.TargetType = savedTargetType;
    }
    protected virtual void ProvideMethodAttributes(Method/*!*/ dup, object/*!*/ handle) {
      Method template = (Method)handle;
      AttributeList tattributes = template.Attributes;
      SecurityAttributeList tSecurityAttributes = template.SecurityAttributes;
      if (tattributes == null && tSecurityAttributes == null) {
        dup.ProvideMethodAttributes = null;
        return;
      }
      TypeNode savedTargetType = this.TargetType;
      this.TargetType = dup.DeclaringType;
      dup.Attributes = this.VisitAttributeList(tattributes);
      dup.SecurityAttributes = this.VisitSecurityAttributeList(tSecurityAttributes);
      this.TargetType = savedTargetType;
    }
    protected virtual void ProvideMethodContract(Method dup, object handle)
    {
      dup.ProvideContract = null;
      Method template = (Method)handle;
      var tcontract = template.Contract;
      if (tcontract == null)
      {
        return;
      }
      TypeNode savedTargetType = this.TargetType;
      Method savedTargetMethod = this.TargetMethod;
      this.TargetType = dup.DeclaringType;
      this.TargetMethod = dup;
      dup.contract = this.VisitMethodContract(tcontract);
      this.TargetType = savedTargetType;
      this.TargetMethod = savedTargetMethod;
    }

    private void ProvideTypeAttributes(TypeNode/*!*/ dup, object/*!*/ handle) {
      TypeNode template = (TypeNode)handle;
      AttributeList templateAttributes = template.Attributes;
      SecurityAttributeList templateSecurityAttributes = template.SecurityAttributes;
      if (templateAttributes == null && templateSecurityAttributes == null) {
        return;
      }
      TypeNode savedTargetType = this.TargetType;
      this.TargetType = dup;
      dup.Attributes = this.VisitAttributeList(templateAttributes);
      dup.SecurityAttributes = this.VisitSecurityAttributeList(templateSecurityAttributes);
      this.TargetType = savedTargetType;
    }
    public virtual TypeNodeList VisitNestedTypes(TypeNode/*!*/ declaringType, TypeNodeList types) {
      if (types == null) return null;
      var savedTargetType = this.TargetType;
      this.TargetType = declaringType;
      TypeNodeList dupTypes = types.Clone();
      for (int i = 0, n = types.Count; i < n; i++){
        TypeNode nt = types[i];
        if (nt == null) continue;
        TypeNode ntdup;
        if (TargetPlatform.UseGenerics) {
          ntdup = dupTypes[i] = this.VisitTypeNode(nt, null, null, null, true);
        }
        else {
          ntdup = dupTypes[i] = this.VisitTypeReference(nt);
        }
        Debug.Assert(ntdup != nt);
        if (ntdup != nt && ntdup != null){
          if (this.RecordOriginalAsTemplate) ntdup.Template = nt;
          ntdup.DeclaringType = declaringType;
          ntdup.DeclaringModule = declaringType.DeclaringModule;
        }
      }
      for (int i = 0, n = types.Count; i < n; i++) {
        TypeNode nt = types[i];
        if (nt == null) continue;
        TypeNodeList nestedTypes = nt.NestedTypes;
        if (nestedTypes == null || nestedTypes.Count == 0) continue;
        TypeNode ntDup = dupTypes[i];
        if (ntDup == null) { Debug.Fail(""); continue; }
        Debug.Assert(ntDup != nt);
        this.VisitNestedTypes(ntDup, nestedTypes);
      }
      this.TargetType = savedTargetType;
      return dupTypes;
    }
    public override TypeNodeList VisitTypeNodeList(TypeNodeList types){
      if (types == null) return null;
      types = base.VisitTypeNodeList(types.Clone());
      if (this.TargetModule == null) return types;
      if (types == null) return null;
      if (this.TargetModule.Types == null) this.TargetModule.Types = new TypeNodeList();
      for (int i = 0, n = types.Count; i < n; i++)
        this.TargetModule.Types.Add(types[i]);
      return types;
    }
    public override TypeNode VisitTypeParameter(TypeNode typeParameter){
      if (typeParameter == null) return null;

      var dup = (TypeNode)this.DuplicateFor[typeParameter.UniqueKey];
      if (dup == null) {
        if (this.TypesToBeDuplicated[typeParameter.UniqueKey] != null) {

          dup = this.VisitTypeNode(typeParameter);
          TypeParameter tp = typeParameter as TypeParameter;
          if (tp != null) {
            var dupTP = (TypeParameter)dup;
            dupTP.structuralElementTypes = this.VisitTypeReferenceList(tp.StructuralElementTypes);
          } else {
            ClassParameter cp = typeParameter as ClassParameter;
            if (cp != null) {
              var dupCP = (ClassParameter)dup;
              dupCP.structuralElementTypes = this.VisitTypeReferenceList(cp.StructuralElementTypes);
            }
          }
        }
        return base.VisitTypeParameter(typeParameter);
      }
      return base.VisitTypeParameter(dup);
    }
    public override TypeNodeList VisitTypeParameterList(TypeNodeList typeParameters){
      if (typeParameters == null) return null;
      return base.VisitTypeParameterList(typeParameters.Clone());
    }
    public override TypeNode VisitTypeReference(TypeNode type){
      if (type == null) return null;
      TypeNode dup = (TypeNode)this.DuplicateFor[type.UniqueKey];
      if (dup != null && (dup.Template != type || this.RecordOriginalAsTemplate)) return dup;
      if (this.RecordOriginalAsTemplate) {
          // [11/1/12 MAF: There was a bug that made it not possible to skip the copy here because generic methods have 
          // type parameters that needed to be duplicated including types instantiated with these type parameters, 
          // e.g.,  Task<T> Foo<T>().
          // Returning here had left Task<T> whereas the method has been changed to Task<T'>
          // Fixing this required not copying the generic method parameters when creating an instance, and instead copying
          // the template parameters during specializing. This fixed some bugs in the specializer too where we lost constraints
          // or mistakenly updated constraints of the generic type rather than the instance.
        return type; // mapping will be done by Specializer
      }
      switch (type.NodeType){
        case NodeType.ArrayType:
          ArrayType arrType = (ArrayType)type;
          TypeNode elemType = this.VisitTypeReference(arrType.ElementType);
          if (elemType == arrType.ElementType) return arrType;
          if (elemType == null) { Debug.Fail(""); return null; }
          //this.TypesToBeDuplicated[arrType.UniqueKey] = arrType;
          dup = elemType.GetArrayType(arrType.Rank, arrType.Sizes, arrType.LowerBounds);
          break;
        case NodeType.ClassParameter:
        case NodeType.TypeParameter:
          if (this.RecordOriginalAsTemplate) return type;
          if (this.TypesToBeDuplicated[type.UniqueKey] == null) return type;
          dup = this.VisitTypeNode(type);
          break;
        case NodeType.DelegateNode:{
          FunctionType ftype = type as FunctionType;
          if (ftype == null) goto default;
          dup = FunctionType.For(this.VisitTypeReference(ftype.ReturnType), this.VisitParameterList(ftype.Parameters), this.TargetType);
          break;}
        case NodeType.Pointer:
          Pointer pType = (Pointer)type;
          elemType = this.VisitTypeReference(pType.ElementType);
          if (elemType == pType.ElementType) return pType;
          if (elemType == null) { Debug.Fail(""); return null; }
          dup = elemType.GetPointerType();
          break;
        case NodeType.Reference:
          Reference rType = (Reference)type;
          elemType = this.VisitTypeReference(rType.ElementType);
          if (elemType == rType.ElementType) return rType;
          if (elemType == null) { Debug.Fail(""); return null; }
          dup = elemType.GetReferenceType();
          break;
        //These types typically have only one reference and do not have pointer identity. Just duplicate them.
        case NodeType.ArrayTypeExpression:
          ArrayTypeExpression aExpr = (ArrayTypeExpression)type.Clone();
          elemType = this.VisitTypeReference(aExpr.ElementType);
          if (elemType == null) { Debug.Fail(""); return aExpr; }
          aExpr.ElementType = elemType;
          return aExpr;
        case NodeType.BoxedTypeExpression:
          BoxedTypeExpression bExpr = (BoxedTypeExpression)type.Clone();
          bExpr.ElementType = this.VisitTypeReference(bExpr.ElementType);
          return bExpr;
        case NodeType.ClassExpression:
          ClassExpression cExpr = (ClassExpression)type.Clone();
          cExpr.Expression = this.VisitExpression(cExpr.Expression);
          cExpr.TemplateArguments = this.VisitTypeReferenceList(cExpr.TemplateArguments);
          return cExpr;   
        case NodeType.FlexArrayTypeExpression:
          FlexArrayTypeExpression flExpr = (FlexArrayTypeExpression)type.Clone();
          flExpr.ElementType = this.VisitTypeReference(flExpr.ElementType);
          return flExpr;
        case NodeType.FunctionPointer:
          FunctionPointer funcPointer = (FunctionPointer)type.Clone();
          funcPointer.ParameterTypes = this.VisitTypeReferenceList(funcPointer.ParameterTypes);
          funcPointer.ReturnType = this.VisitTypeReference(funcPointer.ReturnType);
          return funcPointer;
        case NodeType.FunctionTypeExpression:
          FunctionTypeExpression ftExpr = (FunctionTypeExpression)type.Clone();
          ftExpr.Parameters = this.VisitParameterList(ftExpr.Parameters);
          ftExpr.ReturnType = this.VisitTypeReference(ftExpr.ReturnType);
          return ftExpr;
        case NodeType.InvariantTypeExpression:
          InvariantTypeExpression invExpr = (InvariantTypeExpression)type.Clone();
          invExpr.ElementType = this.VisitTypeReference(invExpr.ElementType);
          return invExpr;
        case NodeType.InterfaceExpression:
          InterfaceExpression iExpr = (InterfaceExpression)type.Clone();
          iExpr.Expression = this.VisitExpression(iExpr.Expression);
          iExpr.TemplateArguments = this.VisitTypeReferenceList(iExpr.TemplateArguments);
          return iExpr;
        case NodeType.NonEmptyStreamTypeExpression:
          NonEmptyStreamTypeExpression neExpr = (NonEmptyStreamTypeExpression)type.Clone();
          neExpr.ElementType = this.VisitTypeReference(neExpr.ElementType);
          return neExpr;
        case NodeType.NonNullTypeExpression:
          NonNullTypeExpression nnExpr = (NonNullTypeExpression)type.Clone();
          nnExpr.ElementType = this.VisitTypeReference(nnExpr.ElementType);
          return nnExpr;
        case NodeType.NonNullableTypeExpression:
          NonNullableTypeExpression nbExpr = (NonNullableTypeExpression)type.Clone();
          nbExpr.ElementType = this.VisitTypeReference(nbExpr.ElementType);
          return nbExpr;
        case NodeType.NullableTypeExpression:
          NullableTypeExpression nuExpr = (NullableTypeExpression)type.Clone();
          nuExpr.ElementType = this.VisitTypeReference(nuExpr.ElementType);
          return nuExpr;
        case NodeType.OptionalModifier:
          TypeModifier modType = (TypeModifier)type;
          TypeNode modified = this.VisitTypeReference(modType.ModifiedType);
          TypeNode modifier = this.VisitTypeReference(modType.Modifier);
          if (modified == null || modifier == null) { Debug.Fail(""); return null; }
          return OptionalModifier.For(modifier, modified);
        case NodeType.RequiredModifier:
          modType = (TypeModifier)type;
          modified = this.VisitTypeReference(modType.ModifiedType);
          modifier = this.VisitTypeReference(modType.Modifier);
          if (modified == null || modifier == null) { Debug.Fail(""); return null; }
          return RequiredModifier.For(modifier, modified);
        default:
          if (type.Template != null)
          {
            var templ = type.Template;
            Debug.Assert(TypeNode.IsCompleteTemplate(templ));
            if (!this.RecordOriginalAsTemplate)
            {
              templ = this.VisitTemplateTypeReference(type.Template);
              Debug.Assert(templ.DeclaringType == null || type.Template.DeclaringType != null);
              Debug.Assert(templ.DeclaringType != null || type.Template.DeclaringType == null);
              Debug.Assert(TypeNode.IsCompleteTemplate(templ));
            }
            bool duplicateReference = templ != type.Template;
            var originalConsolidatedParameterCount = type.Template.ConsolidatedTemplateParameters.Count;
            var newConsolidatedParameterCount = templ.ConsolidatedTemplateParameters.Count;
            TypeNodeList targs;
            if (newConsolidatedParameterCount != originalConsolidatedParameterCount)
            {
              var missing = newConsolidatedParameterCount - originalConsolidatedParameterCount;
              Debug.Assert(missing > 0);
              Debug.Assert(duplicateReference);
              // prefill with new template parameters
              targs = new TypeNodeList(newConsolidatedParameterCount);
              for (int i = 0; i < newConsolidatedParameterCount; i++)
              {
                if (i < missing)
                {
                  targs.Add(templ.ConsolidatedTemplateParameters[i]);
                }
                else
                {
                  targs.Add(this.VisitTypeReference(type.ConsolidatedTemplateArguments[i - missing]));
                }
              }
            }
            else
            {
              targs = type.ConsolidatedTemplateArguments == null ? new TypeNodeList() : type.ConsolidatedTemplateArguments.Clone();
              for (int i = 0, n = targs == null ? 0 : targs.Count; i < n; i++)
              {
                TypeNode targ = targs[i];
                if (targ == null) continue;
                TypeNode targDup = this.VisitTypeReference(targ);
                if (targ != targDup) duplicateReference = true;
                targs[i] = targDup;
              }
            }
            if (!duplicateReference)
            {
              // cache translation
              Debug.Assert(this.TypesToBeDuplicated[type.UniqueKey] == null);
              this.DuplicateFor[type.UniqueKey] = type;
              return type;
            }
            dup = templ.GetGenericTemplateInstance(this.TargetModule, targs);
            Debug.Assert(dup != type);
            this.DuplicateFor[type.UniqueKey] = dup;
            return dup;
            
          }
          // Must be ground and not copied, so just return it.
          Debug.Assert(this.TypesToBeDuplicated[type.UniqueKey] == null);

          return type;
      }
      Debug.Assert(this.TypesToBeDuplicated[type.UniqueKey] == null || type != dup);
      this.DuplicateFor[type.UniqueKey] = dup;
      return dup;
    }
    public virtual TypeNode VisitTemplateTypeReference(TypeNode type)
    {
      if (type == null) return null;
      Debug.Assert(TypeNode.IsCompleteTemplate(type));

      // template type refs can be dealt with as follows:
      //   if we are doing a template instantiation, then we always return the original.
      //   otherwise, if the template itself is to be duplicated, dup it, otherwise return original
      if (this.RecordOriginalAsTemplate) return type;
      var dup = (TypeNode)this.DuplicateFor[type.UniqueKey];
      if (dup != null) return dup;

      if (this.TypesToBeDuplicated[type.UniqueKey] != null)
      {
        Debug.Assert(false, "we already duped all types on entry");
        // dup it
        var savedTargetType = this.TargetType;
        this.TargetType = this.VisitTemplateTypeReference(type.DeclaringType);
        dup = this.VisitTypeNode(type, null, null, null, true);
        this.TargetType = savedTargetType;
        return dup;
      }
      return type;
    }
    public override TypeNodeList VisitTypeReferenceList(TypeNodeList typeReferences){
      if (typeReferences == null) return null;
      return base.VisitTypeReferenceList(typeReferences.Clone());
    }
    public override Expression VisitUnaryExpression(UnaryExpression unaryExpression){
      if (unaryExpression == null) return null;
      unaryExpression = (UnaryExpression)base.VisitUnaryExpression((UnaryExpression)unaryExpression.Clone());
      return unaryExpression;
    }
  }
}
