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
using System.Collections.Generic;
using System.Text;
using System.Diagnostics; // needed for Debug.Assert (etc.)
using System.Runtime.Serialization; // needed for defining exception .ctors
using System.Compiler;
using System.IO;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Contracts.Foxtrot {

  [Flags]
  internal enum RuntimeContractEmitFlags
  {
    None = 0,
    LegacyRequires        = 0x0001,
    RequiresWithException = 0x0002,
    Requires              = 0x0004,
    Ensures               = 0x0008,
    Invariants            = 0x0010,
    Asserts               = 0x0020,
    Assumes               = 0x0040,
    AsyncEnsures          = 0x0080,
    ThrowOnFailure        = 0x1000,
    StandardMode          = 0x2000,
    InheritContracts      = 0x4000,
    /// <summary>
    /// Takes precedence over individual bits (like a mask)
    /// </summary>
    NoChecking            = 0x8000,
  }

  internal static class FlagExtensions
  {

    public static bool Emit(this RuntimeContractEmitFlags have, RuntimeContractEmitFlags want)
    {
      return ((have & RuntimeContractEmitFlags.NoChecking) == 0) && (have & want) != 0;
    }

  }

  internal sealed class ReplaceReturns : StandardVisitor {
    readonly private Local result;
    readonly private Block newExit;
    private int returnCount;
    readonly private bool leaveExceptionBody;
    private SourceContext lastReturnSourceContext;
    public ReplaceReturns(Local r, Block b, bool leaveExceptionBody = false) {
      result = r;
      newExit = b;
      this.leaveExceptionBody = leaveExceptionBody;
    }
    //public override Block VisitBlock(Block block) {
    //  if(block.Statements != null && block.Statements.Count == 1) {
    //    Return r = block.Statements[0] as Return;
    //    if(r != null) {
    //      Statement s = this.VisitReturn(r);
    //      Block retBlock = s as Block;
    //      if(retBlock != null) {
    //        block.Statements = retBlock.Statements;
    //        return block;
    //      } else {
    //        return base.VisitBlock(block);
    //      }
    //    } else {
    //      return base.VisitBlock(block);
    //    }
    //  } else {
    //    return base.VisitBlock(block);
    //  }
    //}
    public override Statement VisitReturn(Return Return) {

      if (Return == null) { return null; }
      returnCount++;
      this.lastReturnSourceContext = Return.SourceContext;
      StatementList stmts = new StatementList();
      Return.Expression = this.VisitExpression(Return.Expression);
      if (Return.Expression != null) {
        MethodCall mc = Return.Expression as MethodCall;
        if (mc != null && mc.IsTailCall) {
          mc.IsTailCall = false;
        }
        var assgnmt = new AssignmentStatement(result, Return.Expression);
        assgnmt.SourceContext = Return.SourceContext;
        stmts.Add(assgnmt);
      }
      // the branch is a "leave" out of the try block that the body will be
      // in.
      var branch = new Branch(null, newExit, false, false, this.leaveExceptionBody);
      branch.SourceContext = Return.SourceContext;
      stmts.Add(branch);

      return new Block(stmts);
    }

    public SourceContext LastReturnSourceContext
    {
      get { return this.lastReturnSourceContext; }
    }
  }
  internal sealed class ReplaceResult : StandardVisitor {
    /// <summary>
    /// Result starts out as a Local that is created by the client of this class. But if during
    /// the replacement of Result&lt;T&gt;(), an occurrence is found within an anonymous delegate,
    /// then the local is replaced with a member binding.
    /// 
    /// If the delegate is within a closure class, then the member binding is "this.f", where
    /// f is a new field that is added to the top-level closure class and "this" will be the
    /// type of the top-level closure class.
    /// 
    /// If the delegate is a static method added to the class itself, then the member binding
    /// is "null.f" where f is a new static field that is added to the class containing the method
    /// that contained the contract in which this occurrence of Result was found.
    /// 
    /// When this happens, a client should use the value of ReplaceResult.Result
    /// to assign the return value of the method to.
    /// 
    /// Note: The client should assign the value that the method returns to *both* the local
    /// originally passed in to the constructor of this visitor *and* to the member binding
    /// (if Result is modified because there is an occurrence of Result&lt;T&gt()
    /// in a delegate) because if any occurrences are found before visiting the closure,
    /// those occurrences will be replaced by the local and not the member binding!
    /// 
    /// When Result is found *not* within a delegate, then it is always replaced with
    /// the original local that the client handed to the constructor.
    readonly private Local originalLocalForResult;

    /// if non-null, used to store result in declaring type.
    /// NOTE: the field type is the type of the
    /// result in the instantiated context. For generic closure classes (which are generated by the 
    /// rewriter during copying of OOB contracts), the closure potentially expects a generic form for
    /// the result value. We thus have to generate access to the result by casting (box InstanceType, unbox GenericType).
    /// </summary>
    private Field topLevelStaticResultField;
    /// <summary>
    /// if non-null, used to store result in top-level closure instance. 
    /// NOTE: the field type is the type of the
    /// result in the instantiated context. For generic closure classes (which are generated by the 
    /// rewriter during copying of OOB contracts), the closure potentially expects a generic form for
    /// the result value. We thus have to generate access to the result by casting (box InstanceType, unbox GenericType).
    /// </summary>
    private Field topLevelClosureResultField;

    public IEnumerable<MemberBinding> NecessaryResultInitialization(Dictionary<TypeNode, Local> closureLocals)
    {
      if (topLevelStaticResultField != null)
      {
        yield return new MemberBinding(null, topLevelStaticResultField);
      }
      if (topLevelClosureResultField != null)
      {
        // note: this field is the field in the generic context of the closure. For the method to initialize this
        // we need the instantiated form, which we remember in topLevelClosureClassInstance
        Local local;
        if (closureLocals.TryGetValue(topLevelClosureClassInstance, out local))
        {
          yield return new MemberBinding(local, GetInstanceField(this.originalLocalForResult.Type, topLevelClosureResultField, topLevelClosureClassInstance));
        } else
        {
          var access = new This(topLevelClosureClassInstance);
          yield return new MemberBinding(access, GetInstanceField(this.originalLocalForResult.Type, topLevelClosureResultField, topLevelClosureClassInstance));
        }
      }
    }

    public IEnumerable<MemberBinding> NecessaryResultInitializationAsync(Dictionary<Local, MemberBinding> closureLocals)
    {
        if (topLevelStaticResultField != null)
        {
            yield return new MemberBinding(null, topLevelStaticResultField);
        }
        if (topLevelClosureResultField != null)
        {
            // note: this field is the field in the generic context of the closure. For the method to initialize this
            // we need the instantiated form, which we remember in topLevelClosureClassInstance
            MemberBinding mb = null;
            foreach (var pair in closureLocals) {
                var keyType = HelperMethods.Unspecialize(pair.Key.Type) ;
                if (keyType == topLevelClosureClassDefinition)
                {
                    mb = pair.Value;
                    yield return new MemberBinding(mb, GetInstanceField(this.originalLocalForResult.Type, topLevelClosureResultField, topLevelClosureClassInstance));
                    break;
                }
            }
            Debug.Assert(mb != null, "Should have found access");
        }
    }

    readonly private Module assemblyBeingRewritten;
    private Method currentClosureMethod; // definition
    private Expression currentAccessToTopLevelClosure;

    private TypeNode topLevelClosureClassDefinition;
    private TypeNode topLevelClosureClassInstance;
    private TypeNode currentClosureClassInstance;
    readonly private TypeNodeList topLevelMethodFormals;

    /// <summary>
    /// Used when the method with the closure is generic and the field ends up on the corresponding generic closure class
    /// </summary>
    private TypeNode properlyInstantiatedFieldType;

    private int delegateNestingLevel;
    readonly private TypeNode declaringType; // needed to copy anonymous delegates into

    public ReplaceResult(Method containingMethod, Local r, Module assemblyBeingRewritten) {
      Contract.Requires(containingMethod != null);
      
      this.assemblyBeingRewritten = assemblyBeingRewritten;
      this.declaringType = containingMethod.DeclaringType;
      this.topLevelMethodFormals = containingMethod.TemplateParameters;
      this.originalLocalForResult = r;
      this.delegateNestingLevel = 0;
    }

    public override Expression VisitReturnValue(ReturnValue returnValue) {
      if (this.delegateNestingLevel == 0)
      {
        // not inside a closure method
        return this.originalLocalForResult;
      }
      else {
        if (this.currentClosureMethod.IsStatic)
        {
          // static closure: no place to store result. Current hack is to use a static field of the 
          // declaring type. However, if we have a static closure inside a non-static closure, this
          // breaks down, as we support only one kind of storage for return values.
          var field = GetReturnValueClosureField(this.declaringType, this.originalLocalForResult.Type, FieldFlags.CompilerControlled | FieldFlags.Private | FieldFlags.Static, originalLocalForResult.UniqueKey);

          Contract.Assume(returnValue != null);

          return CreateProperResultAccess(returnValue, null, field);
        }
        else
        {
          Debug.Assert(this.currentAccessToTopLevelClosure != null);
          Debug.Assert(this.topLevelClosureClassDefinition != null);
          {
            // Return an expression that is the value of the field defined in the
            // top-level closure class to hold the method's return value.
            // This will be this.up.Result where "up" is the field C#
            // generated to point to the instance of the top-level closure class.
            // "Result" is the field defined in this visitor's VisitConstruct when
            // it finds a reference to a anonymous delegate.
            var field = GetReturnValueClosureField(this.topLevelClosureClassDefinition, this.properlyInstantiatedFieldType, FieldFlags.CompilerControlled | FieldFlags.Assembly, this.topLevelClosureClassDefinition.UniqueKey);
            
            Contract.Assume(returnValue != null);

            return CreateProperResultAccess(returnValue, this.currentAccessToTopLevelClosure, field);
          }
        }
      }
    }

    private static Expression CreateProperResultAccess(ReturnValue returnValue, Expression closureObject, Field resultField)
    {
      Contract.Requires(returnValue != null);
      Contract.Requires(resultField != null);

      var fieldAccess = new MemberBinding(closureObject, resultField);

      if (resultField.Type != returnValue.Type)
      {
        // must cast to generic type expected in this context (box instance unbox.any Generic)
        return new BinaryExpression(new BinaryExpression(fieldAccess, new Literal(resultField.Type), NodeType.Box), new Literal(returnValue.Type), NodeType.UnboxAny);
      }
      else
      {
        return fieldAccess;
      }
    }

    /// <summary>
    /// If there is an anonymous delegate within a postcondition, then there
    /// will be a call to a delegate constructor.
    /// That call looks like "d..ctor(o,m)" where d is the type of the delegate.
    /// There are two cases depending on whether the anonymous delegate captured
    /// anything. In both cases, m is the method implementing the anonymous delegate.
    /// (1) It does capture something. Then o is the instance of the closure class
    /// implementing the delegate, and m is an instance method in the closure
    /// class.
    /// (2) It does *not* capture anything. Then o is the literal for null and
    /// m is a static method that was added directly to the class.
    /// 
    /// This method will cause the method (i.e., m) to be visited to collect any
    /// Result&lt;T&gt;() expressions that occur in it.
    /// </summary>
    /// <param name="cons">The AST representing the call to the constructor
    /// of the delegate type.</param>
    /// <returns>Whatever the base visitor returns</returns>
    public override Expression VisitConstruct(Construct cons) {
      if (cons.Type is DelegateNode) {
        UnaryExpression ue = cons.Operands[1] as UnaryExpression;
        if (ue == null) goto JustVisit;
        MemberBinding mb = ue.Operand as MemberBinding;
        if (mb == null) goto JustVisit;
        Method m = mb.BoundMember as Method;
        if (!HelperMethods.IsCompilerGenerated(m)) goto JustVisit;

        Contract.Assume(m != null);

        m = Definition(m);
        this.delegateNestingLevel++;
        TypeNode savedClosureClass = this.currentClosureClassInstance;
        Method savedClosureMethod = this.currentClosureMethod;
        Expression savedCurrentAccessToTopLevelClosure = this.currentAccessToTopLevelClosure;

        try
        {
          this.currentClosureMethod = m;

          if (m.IsStatic)
          {
            this.currentClosureClassInstance = null; // no closure object
          }
          else
          {
            this.currentClosureClassInstance = cons.Operands[0].Type;
            if (savedClosureClass == null)
            {
              // Then this is the top-level closure class.
              this.topLevelClosureClassInstance = this.currentClosureClassInstance;
              this.topLevelClosureClassDefinition = Definition(this.topLevelClosureClassInstance);
              this.currentAccessToTopLevelClosure = new This(this.topLevelClosureClassDefinition);
              this.properlyInstantiatedFieldType = this.originalLocalForResult.Type;

              if (this.topLevelMethodFormals != null) 
              {
                Contract.Assume(this.topLevelClosureClassDefinition.IsGeneric);
                Contract.Assume(topLevelClosureClassDefinition.TemplateParameters.Count >= this.topLevelMethodFormals.Count);

                // replace method type parameters in result properly with last n corresponding type parameters of closure class
                TypeNodeList closureFormals = topLevelClosureClassDefinition.TemplateParameters;
                if (closureFormals.Count > this.topLevelMethodFormals.Count)
                {
                  int offset = closureFormals.Count - this.topLevelMethodFormals.Count;
                  closureFormals = new TypeNodeList(this.topLevelMethodFormals.Count);
                  for (int i = 0; i < this.topLevelMethodFormals.Count; i++)
                  {
                    closureFormals.Add(topLevelClosureClassDefinition.TemplateParameters[i + offset]);
                  }
                }
                Duplicator dup = new Duplicator(this.declaringType.DeclaringModule, this.declaringType);
                Specializer spec = new Specializer(this.declaringType.DeclaringModule, topLevelMethodFormals, closureFormals);
                var type = dup.VisitTypeReference(this.originalLocalForResult.Type);
                type = spec.VisitTypeReference(type);
                this.properlyInstantiatedFieldType = type;
              }
            }
            else
            {
              while (currentClosureClassInstance.Template != null) currentClosureClassInstance = currentClosureClassInstance.Template;

              // Find the field in this.closureClass that the C# compiler generated
              // to point to the top-level closure
              foreach (Member mem in this.currentClosureClassInstance.Members)
              {
                Field f = mem as Field;
                if (f == null) continue;
                if (f.Type == this.topLevelClosureClassDefinition)
                {
                  var consolidatedTemplateParams = this.currentClosureClassInstance.ConsolidatedTemplateParameters;
                  TypeNode thisType;
                  if (consolidatedTemplateParams != null && consolidatedTemplateParams.Count > 0) {
                    thisType = this.currentClosureClassInstance.GetGenericTemplateInstance(this.assemblyBeingRewritten, consolidatedTemplateParams);
                  }
                  else {
                    thisType = this.currentClosureClassInstance;
                  }
                  this.currentAccessToTopLevelClosure = new MemberBinding(new This(thisType), f);

                  break;
                }
              }
            }
          }
          this.VisitBlock(m.Body);
        }
        finally
        {
          this.delegateNestingLevel--;
          this.currentClosureMethod = savedClosureMethod;
          this.currentClosureClassInstance = savedClosureClass;
          this.currentAccessToTopLevelClosure = savedCurrentAccessToTopLevelClosure;
        }
      }
    JustVisit:
      return base.VisitConstruct(cons);
    }

    private static Method Definition(Method m)
    {
      Contract.Requires(m != null);

      while (m.Template != null) m = m.Template;
      return m;
    }
    private static TypeNode Definition(TypeNode t)
    {
      Contract.Requires(t != null);

      while (t.Template != null) t = t.Template;
      return t;
    }

    private Field GetReturnValueClosureField(TypeNode declaringType, TypeNode resultType, FieldFlags flags, int uniqueKey)
    {
      Contract.Requires(declaringType != null);
      
      Contract.Assume(declaringType.Template == null);
      Identifier name = Identifier.For("_result" + uniqueKey.ToString()); // unique name for this field

      Field f = declaringType.GetField(name);
      if (f != null) return f;

      f = new Field(declaringType,
        null,
        flags,
        name,
        resultType,
        null);

      declaringType.Members.Add(f);
      // remember we added it so we can make it part of initializations
      if (f.IsStatic)
      {
        topLevelStaticResultField = f;
      }
      else
      {
        topLevelClosureResultField = f;
      }
      return f;
    }

    private static Field GetInstanceField(TypeNode originalReturnType, Field possiblyGenericField, TypeNode instanceDeclaringType)
    {
      Contract.Requires(instanceDeclaringType != null);

      if (instanceDeclaringType.Template == null) return possiblyGenericField;
      var declaringTemplate = instanceDeclaringType;
      while (declaringTemplate.Template != null) { declaringTemplate = declaringTemplate.Template; }
      Contract.Assume(declaringTemplate == possiblyGenericField.DeclaringType);

      return Rewriter.GetFieldInstanceReference(possiblyGenericField, instanceDeclaringType);
#if false
      Field f = instanceDeclaringType.GetField(possiblyGenericField.Name);
      if (f != null)
      {
        // already instantiated
        return f;
      }
      // pseudo instance
      Field instance = new Field(instanceDeclaringType, possiblyGenericField.Attributes, possiblyGenericField.Flags, possiblyGenericField.Name, originalReturnType, null);
      instanceDeclaringType.Members.Add(instance);
      return instance;
#endif
    }

  }

  internal sealed class CollectBoundVariables : Inspector {
    readonly public List<Parameter> FoundVariables = null;
    readonly public List<Expression> FoundReferences = null;
    readonly List<Parameter> BoundVars;
    public CollectBoundVariables(List<Parameter> boundVars) {
      Contract.Requires(boundVars != null);

      this.FoundVariables = new List<Parameter>(boundVars.Count);
      this.FoundReferences = new List<Expression>(boundVars.Count);
      this.BoundVars = boundVars;
    }
    public override void VisitParameter(Parameter parameter) {
      if (parameter == null) return;
      if (this.BoundVars.Contains(parameter) && !this.FoundVariables.Contains(parameter)) {
        this.FoundVariables.Add(parameter);
        this.FoundReferences.Add(parameter);
      }
    }
    public override void VisitMemberBinding(MemberBinding memberBinding) {
      if (memberBinding == null) return;
      if (memberBinding.TargetObject.NodeType == NodeType.This
      || memberBinding.TargetObject.NodeType == NodeType.Local
        ) {
        // search in list of parameters to see if any have the same name as the bound member
        foreach (Parameter p in this.BoundVars) {
          if (p.Name.UniqueIdKey == memberBinding.BoundMember.Name.UniqueIdKey) {
            if (!this.FoundVariables.Contains(p)) {
              this.FoundVariables.Add(p);
              this.FoundReferences.Add(memberBinding);
            }
          }
        }
      }
      base.VisitMemberBinding(memberBinding);
    }
  }

  // FIXME! Need to do a sanity check that the argument to the Old method
  // is a member binding (or at least something that is directly there and
  // not something sitting on the stack as a result of some code sequence
  // that this class doesn't capture and store in the OldExpressions list).
  internal sealed class CollectOldExpressions : StandardVisitor {

    public Block PrestateValuesOfOldExpressions
    {
      get {
        return this.prestateValuesOfOldExpressions;
      }
    }
    readonly private Block prestateValuesOfOldExpressions;
    readonly private Dictionary<TypeNode, Local> closureLocals;
    private TypeNode topLevelClosureClass;
    private TypeNode currentClosureClass;
    private Field PointerToTopLevelClosureClass;

    readonly private ContractNodes contractNodes;

    readonly private List<MethodCall> stackOfMethods; // contains nested calls of ForAll and Exists methods
    readonly private List<Parameter> stackOfBoundVariables; // contains the parameters of the ForAll and Exists methods

    readonly private Module module;
    readonly private Method currentMethod;

    private int counter;
    public int Counter { get { return this.counter; } }


    public CollectOldExpressions(
      Module module,
      Method method,
      ContractNodes contractNodes,
      Dictionary<TypeNode, Local> closureLocals,
      int localCounterStart,
      Class initialClosureClass)
        :this(module, method, contractNodes, closureLocals, localCounterStart)
    {
        this.topLevelClosureClass = initialClosureClass;
        this.currentClosureClass = initialClosureClass;
    }

    public CollectOldExpressions(
      Module module,
      Method method,
      ContractNodes contractNodes,
      Dictionary<TypeNode,Local> closureLocals,
      int localCounterStart)
    {
      this.contractNodes = contractNodes;
      this.prestateValuesOfOldExpressions = new Block(new StatementList());
      this.closureLocals = closureLocals;
      this.stackOfMethods = new List<MethodCall>();
      this.stackOfBoundVariables = new List<Parameter>();
      this.module = module;
      this.currentMethod = method;
      this.counter = localCounterStart;
    }
    public override Expression VisitOldExpression(OldExpression oldExpression) {
      if (this.topLevelClosureClass != null) {
        #region In Closure ==> Create a field
        // Since we're within a closure, we can't create a local to hold the value of the old expression
        // but instead have to create a field for it. That field can be a member of the top-level
        // closure class since nothing mentioned in the old expression (except possibly for the
        // bound variables of enclosing quantifications) should be anything captured from
        // an inner anonymous delegate.

        // BUT, first we have to know if the old expression depends on any of the bound
        // variables of the closures in which it is located. If not, then we can implement
        // it as a scalar and just generate the assignment "closure_class.field := e" for
        // "Old(e)" to take a snapshot of e's value in the prestate. If it does depend on
        // any of the bound variables, then we need to generate a set of for-loops that
        // compute the indices and values of e for each tuple of indices so it can be retrieved
        // (given the indices) in the post-state.
        CollectBoundVariables cbv = new CollectBoundVariables(this.stackOfBoundVariables);
        cbv.VisitExpression(oldExpression.expression);

        SubstituteClosureClassWithinOldExpressions subst =
            new SubstituteClosureClassWithinOldExpressions(this.closureLocals);
        Expression e = subst.VisitExpression(oldExpression.expression);
        if (cbv.FoundVariables.Count == 0) {
          #region Use a scalar for the old variable
          Local closureLocal;
          if (!this.closureLocals.TryGetValue(this.topLevelClosureClass, out closureLocal))
          {
            Contract.Assume(false, "can't find closure local!");
          }
          #region Define a scalar
          var clTemplate = HelperMethods.Unspecialize(this.topLevelClosureClass);
          Field f = new Field(clTemplate,
            null,
            FieldFlags.CompilerControlled | FieldFlags.Public,
            Identifier.For("_old" + oldExpression.expression.UniqueKey.ToString()), // unique name for this old expr.
            oldExpression.Type,
            null);
          clTemplate.Members.Add(f);
          // now produce properly instantiated field
          f = (Field)Rewriter.GetMemberInstanceReference(f, this.topLevelClosureClass);
          #endregion
          #region Generate code to store value in prestate
          this.prestateValuesOfOldExpressions.Statements.Add(new AssignmentStatement(new MemberBinding(closureLocal, f), e));
          #endregion
          #region Return expression to be used in poststate
          // Return an expression that will evaluate in the poststate to the value of the old
          // expression in the prestate. This will be this.up.f where "up" is the field C#
          // generated to point to the instance of the top-level closure class.
          if (this.PointerToTopLevelClosureClass == null) {
            // then the old expression occurs in the top-level closure class. Just return "this.f"
            // where "this" refers to the top-level closure class.
            return new MemberBinding(new This(this.currentClosureClass), f);
          } else {
            return new MemberBinding(
              new MemberBinding(new This(this.currentClosureClass), this.PointerToTopLevelClosureClass),
              f);
          }
          #endregion
          #endregion
        } else {
          // the Old expression *does* depend upon at least one of the bound variable
          // in a ForAll or Exists expression
          #region Use an indexed variable for the old variable
          TypeNode oldVariableTypeDomain;
          #region Decide if domain is one-dimensional or not
          bool oneDimensional = cbv.FoundVariables.Count == 1 && cbv.FoundVariables[0].Type.IsValueType;
          if (oneDimensional) {
            // a one-dimensional old-expression can use the index variable directly
            oldVariableTypeDomain = cbv.FoundVariables[0].Type;
          } else {
            oldVariableTypeDomain = SystemTypes.GenericList.GetTemplateInstance(this.module, SystemTypes.Int32);
          }
          #endregion
          TypeNode oldVariableTypeRange = oldExpression.Type;
          TypeNode oldVariableType = SystemTypes.GenericDictionary.GetTemplateInstance(this.module, oldVariableTypeDomain, oldVariableTypeRange);
          Local closureLocal;
          if (!this.closureLocals.TryGetValue(this.topLevelClosureClass, out closureLocal))
          {
            Contract.Assume(false, "can't find closure local");
          }
          #region Define an indexed variable
          var clTemplate = HelperMethods.Unspecialize(this.topLevelClosureClass);
          Field f = new Field(clTemplate,
            null,
            FieldFlags.CompilerControlled | FieldFlags.Assembly, // can't be private or protected because it needs to be accessed from inner (closure) classes that don't inherit from the class this field is added to.
            Identifier.For("_old" + oldExpression.expression.UniqueKey.ToString()), // unique name for this old expr.
            oldVariableType,
            null);
          clTemplate.Members.Add(f);
          // instantiate f
          f = (Field)Rewriter.GetMemberInstanceReference(f, closureLocal.Type);
          #endregion
          #region Generate code to initialize the indexed variable
          Statement init = new AssignmentStatement(
            new MemberBinding(closureLocal, f),
            new Construct(new MemberBinding(null, oldVariableType.GetConstructor()), null));
          this.prestateValuesOfOldExpressions.Statements.Add(init);
          #endregion
          #region Generate code to store values in prestate
          #region Create assignment: this.closure.f[i,j,k,...] = e;
          Method setItem = oldVariableType.GetMethod(Identifier.For("set_Item"), oldVariableTypeDomain, oldVariableTypeRange);
          Expression index;
          if (oneDimensional) {
            index = cbv.FoundVariables[0];
          } else {
            //InstanceInitializer ctor =
            //  ContractNodes.TupleClass.GetConstructor(SystemTypes.Int32.GetArrayType(1));
            //Expression index = new Construct(new MemberBinding(null,ctor),new ExpressionList(
            index = Literal.Null;
          }
          MethodCall mc = new MethodCall(new MemberBinding(new MemberBinding(closureLocal, f), setItem),
            new ExpressionList(index, e));
          Statement stat = new ExpressionStatement(mc);
          #endregion
          List<Local> locals = new List<Local>(this.stackOfBoundVariables.Count);
          TrivialHashtable paramMap = new TrivialHashtable();
          #region Generate a local for each bound variable to use in for-loop
          foreach (Variable v in this.stackOfBoundVariables) {
            Local l = new Local(Identifier.Empty, v.Type);
            paramMap[v.UniqueKey] = l;
            locals.Add(l);
          }
          #endregion
          #region Substitute locals for bound variables in old expression *AND* in inner loop bounds
          SubstituteParameters sps = new SubstituteParameters(paramMap, this.stackOfBoundVariables);
          sps.Visit(stat);
          #endregion
          #region Create nested for-loops around assignment
          // keep track of when the first variable is used (from innermost to outermost)
          // as soon as the first one is needed because the old expression depends on it,
          // then keep all enclosing loops. It would be possible to keep only those where
          // the necessary loops have loop bounds that depend on an enclosing loop, but I
          // haven't calculated that, so just keep them all. For instance, if the old expression
          // depends on j and the loops are "for i,0,n" and inside that "for j,0,i", then need
          // both loops. If the inner loop bounds were 0 and n, then wouldn't need the outer
          // loop.
          bool usedAVariable = false;
          for (int i = this.stackOfBoundVariables.Count - 1; 0 <= i; i--) {
            if (!usedAVariable
              && !cbv.FoundVariables.Contains(this.stackOfBoundVariables[i])) continue;
            usedAVariable = true;
            Expression lowerBound = new Duplicator(this.module, this.currentClosureClass).VisitExpression(
              this.stackOfMethods[i].Operands[0]);
            lowerBound = subst.VisitExpression(lowerBound);
            lowerBound = sps.VisitExpression(lowerBound);
            Expression upperBound = new Duplicator(this.module, this.currentClosureClass).VisitExpression(
              this.stackOfMethods[i].Operands[1]);
            upperBound = subst.VisitExpression(upperBound);
            upperBound = sps.VisitExpression(upperBound);
            stat = RewriteHelper.GenerateForLoop(locals[i], lowerBound, upperBound, stat);
          }
          #endregion
          this.prestateValuesOfOldExpressions.Statements.Add(stat);
          #endregion
          #region Return expression to be used in poststate
          Method getItem = oldVariableType.GetMethod(Identifier.For("get_Item"), oldVariableTypeDomain);
          if (oneDimensional) {
            index = cbv.FoundReferences[0];
          } else {
            //InstanceInitializer ctor =
            //  ContractNodes.TupleClass.GetConstructor(SystemTypes.Int32.GetArrayType(1));
            //Expression index = new Construct(new MemberBinding(null,ctor),new ExpressionList(
            index = Literal.Null;
          }
          // Return an expression that will evaluate in the poststate to the value of the old
          // expression in the prestate. This will be this.up.f[i,j,k,...] where "up" is the field C#
          // generated to point to the instance of the top-level closure class.
          MemberBinding thisDotF;
          if (this.PointerToTopLevelClosureClass == null) {
            // then the old expression occurs in the top-level closure class. Just return "this.f"
            // where "this" refers to the top-level closure class.
            Contract.Assume(f != null);
            thisDotF = new MemberBinding(new This(clTemplate), HelperMethods.Unspecialize(f));
          } else {
            thisDotF = new MemberBinding(
              new MemberBinding(new This(clTemplate), this.PointerToTopLevelClosureClass),
              f);
          }
          return new MethodCall(new MemberBinding(thisDotF, getItem), new ExpressionList(index));
          #endregion
          #endregion
        }
        #endregion
      } else {
        #region Not in closure ==> Create a local variable
        Local l = GetLocalForOldExpression(oldExpression);
        #region Make sure local can be seen in the debugger (for the entire method, unfortunately)
        if (currentMethod.LocalList == null) {
          currentMethod.LocalList = new LocalList();
        }
        currentMethod.LocalList.Add(l);
        currentMethod.Body.HasLocals = true;
        #endregion
        this.prestateValuesOfOldExpressions.Statements.Add(
          new AssignmentStatement(l, oldExpression.expression));

        // Return an expression that will evaluate in the poststate to the value of the old
        // expression in the prestate. When we're not in a closure, this is just the local
        // itself.
        return l;
        #endregion
      }
    }

    private string BestGuessForLocalName(OldExpression oldExpression) {
      Contract.Requires(oldExpression != null);

      MemberBinding mb = oldExpression.expression as MemberBinding;
      if (mb != null) {
        return mb.BoundMember.Name.Name;
      }
      MethodCall mc = oldExpression.expression as MethodCall;
      if (mc != null) {
        mb = mc.Callee as MemberBinding;
        if (mb != null) {
          Method calledMethod = mb.BoundMember as Method;
          if (calledMethod != null && calledMethod.IsPropertyGetter) {
            Property prop = calledMethod.DeclaringMember as Property;
            if (prop != null)
              return prop.Name.Name;
            else
              return mb.BoundMember.Name.Name;
          } else {
            return mb.BoundMember.Name.Name;
          }
        }
      }
      Parameter p = oldExpression.expression as Parameter;
      if (p != null) {
        return p.Name.Name;
      }
      // Didn't find something good to use for the name, so just make it unique.
      var x = this.counter.ToString();
      this.counter++;
      return x;
    }
    private Local GetLocalForOldExpression(OldExpression oldExpression) {
      Contract.Requires(oldExpression != null);

      string localName = BestGuessForLocalName(oldExpression);
      return new Local(Identifier.For("Contract.Old(" + localName + ")"), oldExpression.Type);
    }

    public override Expression VisitMethodCall(MethodCall call) {
      if (call == null || call.Callee == null) { return call; }
      MemberBinding mb = call.Callee as MemberBinding;
      if (mb == null) { return base.VisitMethodCall(call); }
      Method m = mb.BoundMember as Method;
      if (m == null) { return base.VisitMethodCall(call); }

      #region ForAll(...) or Exists(...)
      if (this.contractNodes.IsForallMethod(m) || this.contractNodes.IsExistsMethod(m)) {
        Parameter closureParameter = null;
        // "ForAll(lb,ub, delegate(int i) { ... })" ==> "i" (or Exists)
        ExpressionList operands = call.Operands;
        if (operands == null || operands.Count < 3) goto callBase;
        Construct construct = operands[2] as Construct;
        if (construct == null) {
          // then the predicate is just a static method because it didn't capture
          // and parameters and so was not a method on a closure class
          goto callBase;
        }
        ExpressionList operands2 = construct.Operands;
        if (operands2 == null || operands2.Count < 2) goto callBase;
        UnaryExpression ue = operands2[1] as UnaryExpression;
        if (ue == null) goto callBase;
        MemberBinding memberBinding = ue.Operand as MemberBinding;
        if (memberBinding == null) goto callBase;
        Method closure = memberBinding.BoundMember as Method;
        if (closure == null || closure.Parameters == null || closure.Parameters.Count < 1) goto callBase;
        closureParameter = closure.Parameters[0];

      callBase:
        if (closureParameter != null) {
          this.stackOfMethods.Add(call); // imitate Stack.Push()
          this.stackOfBoundVariables.Add(closureParameter);
        }
        Expression result = base.VisitMethodCall(call);

        if (closureParameter != null)
        {
          this.stackOfMethods.RemoveAt(this.stackOfMethods.Count - 1); // imitate Stack.Pop()
          this.stackOfBoundVariables.RemoveAt(this.stackOfBoundVariables.Count - 1); // imitate Stack.Pop()
        }
        return result;
      }
      #endregion
      #region All other method calls
      return base.VisitMethodCall(call);
      #endregion
    }
    /// <summary>
    /// If there is an anonymous delegate within a postcondition, then there
    /// will be a call to a delegate constructor.
    /// That call looks like "d..ctor(o,m)" where d is the type of the delegate.
    /// There are two cases depending on whether the anonymous delegate captured
    /// anything. In both cases, m is the method implementing the anonymous delegate.
    /// 
    /// (1) It does capture something. Then o is the instance of the closure class
    /// implementing the delegate, and m is an instance method in the closure
    /// class.
    /// (2) It does *not* capture anything. Then o is the literal for null and
    /// m is a static method that was added directly to the class.
    /// 
    /// This method will cause m to be visited to collect any old expressions
    /// that occur in it. But those old expressions are not turned into locals,
    /// but fields. The fields are created as either (1) members of the closure class,
    /// or (2) members of the class in which the method containing the contract
    /// containing the Old expression is defined.
    /// The fields are initialized when (1) the closure class instance is created,
    /// or (2) ... ??? (when!?)
    /// So set up enough state so that when those old expressions are visited, the
    /// correct ASTs can be built.
    /// </summary>
    /// <param name="cons">The AST representing the call to the constructor
    /// of the delegate type.</param>
    /// <returns>Whatever the base visitor returns</returns>
    public override Expression VisitConstruct(Construct cons) {
      if (cons.Type is DelegateNode) {
        UnaryExpression ue = cons.Operands[1] as UnaryExpression;
        if (ue == null) goto JustVisit;
        MemberBinding mb = ue.Operand as MemberBinding;
        if (mb == null) goto JustVisit;
        Method m = mb.BoundMember as Method;

        Contract.Assume(m != null);

        if (m.IsStatic) {
          // then there is no closure class, m is just a static method the compiler
          // added to the class itself
          goto JustVisit;
        }
        if (HelperMethods.IsCompilerGenerated(m)) {
          Local l = cons.Operands[0] as Local;
          if (l == null) goto JustVisit; // but then what is it??
          TypeNode savedClosureClass = this.currentClosureClass;
          this.currentClosureClass = l.Type;
          if (savedClosureClass == null) {
            // then this is the top-level closure class
            // have to treat it special: it doesn't contain a field that points 
            // to the top-level closure class. The field introduced to hold the value of the
            // old expression will be declared in this class.
            this.topLevelClosureClass = this.currentClosureClass;
          } else {
            // Find the field in this.closureClass that the C# compiler generated
            // to point to the top-level closure
            foreach (Member mem in this.currentClosureClass.Members) {
              Field f = mem as Field;
              if (f == null) continue;
              if (f.Type == this.topLevelClosureClass) {
                this.PointerToTopLevelClosureClass = f;
                break;
              }
            }
          }

          this.VisitBlock(m.Body);

          if (savedClosureClass == null) {
            this.topLevelClosureClass = null;
          }
          this.currentClosureClass = savedClosureClass;
          this.PointerToTopLevelClosureClass = null;
        }
      }
      JustVisit:
      return base.VisitConstruct(cons);
    }
  }
  internal sealed class SubstituteClosureClassWithinOldExpressions : StandardVisitor {
    readonly private Dictionary<TypeNode, Local> closureLocals;
    public SubstituteClosureClassWithinOldExpressions(Dictionary<TypeNode,Local> closureLocals) {
      this.closureLocals = closureLocals;
    }
    public override Expression VisitMemberBinding(MemberBinding memberBinding) {
      Local closureLocal;
      if (memberBinding.TargetObject != null && this.closureLocals.TryGetValue(memberBinding.TargetObject.Type, out closureLocal)) {
        return new MemberBinding(closureLocal, memberBinding.BoundMember);
      } else {
        return base.VisitMemberBinding(memberBinding);
      }
    }
  }
  internal sealed class SubstituteParameters : StandardVisitor {
    readonly private TrivialHashtable map;
    readonly private List<Parameter> parameters;
    public SubstituteParameters(TrivialHashtable map, List<Parameter> parameters) {
      this.map = map;
      this.parameters = parameters;
    }
    public override Expression VisitMemberBinding(MemberBinding memberBinding) {
      if (memberBinding.TargetObject != null &&
        (memberBinding.TargetObject.NodeType == NodeType.This
        || memberBinding.TargetObject.NodeType == NodeType.Local)
        ) {
        // search in list of parameters to see if any have the same name as the bound member
        foreach (Parameter p in this.parameters) {
          if (p.Name.UniqueIdKey == memberBinding.BoundMember.Name.UniqueIdKey) {
            return (Expression)this.map[p.UniqueKey];
          }
        }
        return base.VisitMemberBinding(memberBinding);
      } else {
        return base.VisitMemberBinding(memberBinding);
      }
    }
    public override Expression VisitParameter(Parameter parameter) {
      if (parameter == null) return null;
      object result = map[parameter.UniqueKey];
      if (result != null) return (Expression)result;
      return base.VisitParameter(parameter);
    }
  }

  /// <summary>
  /// Visitor is used for invariants only!
  /// </summary>
  internal sealed class RewriteInvariant : StandardVisitor {
    readonly RuntimeContractMethods rcm;
    Literal sourceTextOfInvariant = null;
    // Requires:
    //  replacementMethod.Keys == replacementExceptions.Keys
    //  
    public RewriteInvariant(RuntimeContractMethods rcm) {
      this.rcm = rcm;
    }

    // Requires:
    //  statement.Expression is MethodCall
    //  statement.Expression.Callee is MemberBinding
    //  statement.Expression.Callee.BoundMember is Method
    //  statement.Expression.Callee.BoundMember == "Requires" or "Ensures"
    //
    //  inline  <==> replacementMethod == null
    //  replacementMethod != null
    //           ==> replacementMethod.ReturnType == methodToReplace.ReturnType
    //               && replacementMethod.Parameters.Count == 1
    //               && methodToReplace.Parameters.Count == 1
    //               && replacementMethod.Parameters[0].Type == methodToReplace.Parameters[0].Type
    //
    private static Statement RewriteContractCall(
      ExpressionStatement statement,
      Method/*!*/ methodToReplace,
      Method/*?*/ replacementMethod,
      Literal/*?*/ sourceTextToUseAsSecondArg
      ) {

        Contract.Requires(statement != null);

      MethodCall call = statement.Expression as MethodCall;

      if (call == null || call.Callee == null) { return statement; }
      MemberBinding mb = call.Callee as MemberBinding;
      if (mb == null) { return statement; }
      Method m = mb.BoundMember as Method;
      if (m == null) { return statement; }
      if (m != methodToReplace) { return statement; }

      mb.BoundMember = replacementMethod;

      if (call.Operands.Count == 3)
      { // then the invariant was found in a reference assembly
        // it already has all of its arguments
        return statement;
      }

      if (call.Operands.Count == 1) {
        call.Operands.Add(Literal.Null);
      }

      Literal extraArg = sourceTextToUseAsSecondArg;
      if (extraArg == null)
      {
        extraArg = Literal.Null;
        //extraArg = new Literal("No other information available", SystemTypes.String);
      }
      call.Operands.Add(extraArg);

      return statement;

    }

    public override Statement VisitExpressionStatement(ExpressionStatement statement) {
      Method contractMethod = Rewriter.ExtractCallFromStatement(statement);
      if (contractMethod == null) {
        return base.VisitExpressionStatement(statement);
      }
      if (this.rcm.ContractNodes.IsInvariantMethod(contractMethod))
      {
        Statement s =
          RewriteInvariant.RewriteContractCall(
          statement,
          contractMethod,
          this.rcm.InvariantMethod,
          this.sourceTextOfInvariant
        );
        return s;
      } else {
        return base.VisitExpressionStatement(statement);
      }
    }

    public override Invariant VisitInvariant(Invariant @invariant) {
      if (@invariant == null) return null;
      SourceContext sctx = @invariant.SourceContext;
      if (invariant.SourceConditionText != null)
      {
        this.sourceTextOfInvariant = invariant.SourceConditionText;
      }
      else if (sctx.IsValid && sctx.Document.Text != null && sctx.Document.Text.Source != null)
      {
        this.sourceTextOfInvariant = new Literal(sctx.Document.Text.Source, SystemTypes.String);
      } else {
        this.sourceTextOfInvariant = Literal.Null;
        //this.sourceTextOfInvariant = new Literal("No other information available", SystemTypes.String);
      }
      return base.VisitInvariant(@invariant);
    }

  }


  internal sealed class RemoveShortBranches : Inspector {
    public override void VisitBranch(Branch branch) {
      branch.ShortOffset = false;
    }
  }

  internal sealed class WrapParametersInOldExpressions : StandardVisitor {
    public override Expression VisitOldExpression(OldExpression oldExpression) {
      // no point descending down into any old expressions
      return oldExpression;
    }
    public override Expression VisitParameter(Parameter parameter) {
      if (parameter == null) return null;
      var oe = new OldExpression(parameter);
      oe.Type = parameter.Type;
      return oe;
    }
  }

  /// <summary>
  /// Encapsulates the runtime methods to use when emitting checks.
  /// Generates necessary code on demand.
  /// </summary>
  public sealed class RuntimeContractMethods
  {
    public readonly bool ThrowOnFailure;
    public readonly int RewriteLevel;
    public readonly bool PublicSurfaceOnly;
    public readonly bool CallSiteRequires;
    readonly int regularRecursionGuard;
    public readonly bool HideFromDebugger;

    public bool AssertOnFailure { get { return !ThrowOnFailure; } }

    /// <summary>
    /// Controls how validations are emitted and how inheritance works for all requires that throw exceptions.
    /// </summary>
    public readonly bool UseExplicitValidation;

    public RuntimeContractMethods(TypeNode userContractType, ContractNodes contractNodes, AssemblyNode targetAssembly,
                                  bool throwOnFailure, int rewriteLevel, bool publicSurfaceOnly, bool callSiteRequires,
                                  int recursionGuard, bool hideFromDebugger,
                                  bool userExplicitValidation
                                 )
    {
      this.contractNodes = contractNodes;
      this.targetAssembly = targetAssembly;
      this.ThrowOnFailure = throwOnFailure;
      this.RewriteLevel = rewriteLevel;
      this.PublicSurfaceOnly = publicSurfaceOnly;
      this.CallSiteRequires = callSiteRequires;
      this.regularRecursionGuard = recursionGuard;
      this.HideFromDebugger = hideFromDebugger;
      this.UseExplicitValidation = userExplicitValidation;

      // extract methods from user methods
      #region Get the user-specified rewriter methods (optional) REVIEW!! Needs a lot of error handling
      if (userContractType != null)
      {
        Method method = null;
        MethodList reqMethods = userContractType.GetMethods(Identifier.For("Requires"), SystemTypes.Boolean, SystemTypes.String, SystemTypes.String);
        for (int i = 0; i < reqMethods.Count; i++)
        {
          method = reqMethods[i];
          if (method != null)
          {
            if (method.TemplateParameters == null || method.TemplateParameters.Count != 1)
            {
              /*if (method != null) */ this.requiresMethod = method;
            }
            else
            {
              this.requiresWithExceptionMethod = method;
            }
          }
        }
        method = userContractType.GetMethod(Identifier.For("Ensures"), SystemTypes.Boolean, SystemTypes.String, SystemTypes.String);
        if (method != null) this.ensuresMethod = method;
        method = userContractType.GetMethod(Identifier.For("EnsuresOnThrow"), SystemTypes.Boolean, SystemTypes.String, SystemTypes.String, SystemTypes.Exception);
        if (method != null) this.ensuresOnThrowMethod = method;
        method = userContractType.GetMethod(Identifier.For("Invariant"), SystemTypes.Boolean, SystemTypes.String, SystemTypes.String);
        if (method != null) this.invariantMethod = method;
        method = userContractType.GetMethod(Identifier.For("Assert"), SystemTypes.Boolean, SystemTypes.String, SystemTypes.String);
        if (method != null) this.assertMethod = method;
        method = userContractType.GetMethod(Identifier.For("Assume"), SystemTypes.Boolean, SystemTypes.String, SystemTypes.String);
        if (method != null) this.assumeMethod = method;

        // Need to make sure that the type ContractFailureKind is the one used in the user-supplied methods, which is not necessarily
        // the one that is defined in the assembly that defines the contract class. For instance, extracting/rewriting from a 4.0 assembly
        // but where the user-supplied assembly is pre-4.0.
        var mems = userContractType.GetMembersNamed(ContractNodes.ReportFailureName);
        TypeNode contractFailureKind = contractNodes.ContractFailureKind;
        //if (mems != null) 
        {
          foreach(var mem in mems){
            method = mem as Method;
            if (method == null) continue;
            if (method.Parameters.Count != 4) continue;
            if (method.Parameters[0].Type.Name != contractNodes.ContractFailureKind.Name) continue;
            if (method.Parameters[1].Type != SystemTypes.String) continue;
            if (method.Parameters[2].Type != SystemTypes.String) continue;
            if (method.Parameters[3].Type != SystemTypes.Exception) continue;
            this.failureMethod = method;
            contractFailureKind = method.Parameters[0].Type;
            break;
          }
        }

        if (this.failureMethod == null) 
        {
          mems = userContractType.GetMembersNamed(ContractNodes.RaiseContractFailedEventName);
          // if (mems != null) 
          {
            foreach (var mem in mems) {
              method = mem as Method;
              if (method == null) continue;
              if (method.Parameters.Count != 4) continue;
              if (method.Parameters[0].Type.Name.UniqueIdKey != contractNodes.ContractFailureKind.Name.UniqueIdKey) continue;
              if (method.Parameters[1].Type != SystemTypes.String) continue;
              if (method.Parameters[2].Type != SystemTypes.String) continue;
              if (method.Parameters[3].Type != SystemTypes.Exception) continue;
              this.raiseFailureEventMethod = method;
              contractFailureKind = method.Parameters[0].Type;
              break;
            }
          }
        } else {
          method = userContractType.GetMethod(ContractNodes.RaiseContractFailedEventName, contractFailureKind, SystemTypes.String, SystemTypes.String, SystemTypes.Exception);
          if (method != null) this.raiseFailureEventMethod = method;
        }
        if (this.raiseFailureEventMethod != null) { // either take all both RaiseContractFailedEvent and TriggerFailure or neither
          method = userContractType.GetMethod(ContractNodes.TriggerFailureName, contractFailureKind, SystemTypes.String, SystemTypes.String, SystemTypes.String, SystemTypes.Exception);
          if (method != null) this.triggerFailureMethod = method;
        }

      }
      #endregion Get the user-specified rewriter methods (optional) REVIEW!! Needs a lot of error handling

    }

    public int RecursionGuardCountFor(Method method)
    {
      Contract.Requires(method != null);

      if (method.IsPropertyGetter) return Math.Min(1, regularRecursionGuard);
      return regularRecursionGuard;
    }

    #region Runtime contract methods
    Method/*?*/ requiresMethod = null;
    Method/*?*/ requiresWithExceptionMethod = null;
    Method/*?*/ ensuresMethod = null;
    Method/*?*/ ensuresOnThrowMethod = null;
    Method/*?*/ invariantMethod = null;
    Method/*?*/ assertMethod = null;
    Method/*?*/ assumeMethod = null;
    Method/*?*/ failureMethod = null;

    public Method RequiresMethod
    {
      get
      {
        if (requiresMethod == null)
        {
          // generate it
          this.requiresMethod = MakeMethod("Requires", ContractFailureKind.Precondition);
        }
        return requiresMethod;
      }
    }
    public Method RequiresWithExceptionMethod
    {
      get
      {
        if (this.requiresWithExceptionMethod == null)
        {
          this.requiresWithExceptionMethod = MakeRequiresWithExceptionMethod("Requires");
        }
        return this.requiresWithExceptionMethod;
      }
    }

    public Method EnsuresMethod
    {
      get
      {
        if (ensuresMethod == null)
        {
          // generate it
          this.ensuresMethod = MakeMethod("Ensures", ContractFailureKind.Postcondition);
        }
        return ensuresMethod;
      }
    }
    public Method EnsuresOnThrowMethod
    {
      get
      {
        if (this.ensuresOnThrowMethod == null)
        {
          // generate it
          this.ensuresOnThrowMethod = MakeMethod("EnsuresOnThrow", ContractFailureKind.PostconditionOnException);
        }
        return this.ensuresOnThrowMethod;
      }
    }
    public Method InvariantMethod
    {
      get
      {
        if (this.invariantMethod == null)
        {
          // generate it
          this.invariantMethod = MakeMethod("Invariant", ContractFailureKind.Invariant);
        }
        return this.invariantMethod;
      }
    }
    public Method AssertMethod
    {
      get
      {
        if (this.assertMethod == null)
        {
          // generate it
          this.assertMethod = MakeMethod("Assert", ContractFailureKind.Assert);
        }
        return this.assertMethod;
      }
    }
    public Method AssumeMethod
    {
      get
      {
        if (this.assumeMethod == null)
        {
          // generate it
          this.assumeMethod = MakeMethod("Assume", ContractFailureKind.Assume);
        }
        return this.assumeMethod;
      }
    }

    #endregion

    #region FailureKind literals
    internal Literal PreconditionKind
    {
      get
      {
        return contractNodes.ContractFailureKind.GetField(Identifier.For("Precondition")).DefaultValue;
      }
    }
    #endregion

    #region other private fields

    Class/*?*/ runtimeContractType;

    readonly ContractNodes contractNodes;
    readonly AssemblyNode targetAssembly;

    #endregion

    public ContractNodes ContractNodes { get { return this.contractNodes; } }

    private Method FailureMethod
    {
      get
      {
        if (this.failureMethod == null)
        {
          // Generate it.
          Method m = MakeFailureMethod();

          this.failureMethod = m;
        }
        return this.failureMethod;
      }
    }


    private Method raiseFailureEventMethod;
    internal Method RaiseFailureEventMethod
    {
      get
      {
        if (this.raiseFailureEventMethod == null)
        {
          this.raiseFailureEventMethod = this.contractNodes.RaiseFailedEventMethod;
        }
        if (this.raiseFailureEventMethod == null)
        {
          // Generate it
          Method m = MakeRaiseFailureEventMethod();

          this.raiseFailureEventMethod = m;
        }
        return this.raiseFailureEventMethod;
      }
    }


    private Method triggerFailureMethod;
    private Method TriggerFailureMethod
    {
      get
      {
        if (this.triggerFailureMethod == null && !ThrowOnFailure)
        {
          this.triggerFailureMethod = this.contractNodes.TriggerFailureMethod;
        }
        if (this.triggerFailureMethod == null)
        {
          // Generate it
          Method m = MakeTriggerFailureMethod(ThrowOnFailure);

          this.triggerFailureMethod = m;
        }

        return this.triggerFailureMethod;
      }
    }

    private Class contractExceptionType;
    private Class ContractExceptionType
    {
      get
      {
        if (this.contractExceptionType == null)
        {
          if (this.contractNodes.ContractClass.DeclaringModule == this.targetAssembly) {
            // If we're rewriting an assembly that defines the contract class itself,
            // see if it already defines a contract exception that can be used
            this.contractExceptionType =
              this.targetAssembly.GetType(
              ContractNodes.ContractNamespace,
              Identifier.For("ContractException")) as Class;
          }
          // If that fails for any reason, then go ahead and make our own contract exception type
          if (this.contractExceptionType == null) {
            this.contractExceptionType = MakeContractException();
          }
        }
        return this.contractExceptionType;
      }
    }

    private Class MakeContractException() {
      Class contractExceptionType;

      #region If we're rewriting an assembly for v4 or above and it *isn't* Silverlight (so serialization support is needed), then use new embedded dll as the type
      if (4 <= TargetPlatform.MajorVersion) {
        var iSafeSerializationData = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.Serialization"), Identifier.For("ISafeSerializationData")) as Interface;
        if (iSafeSerializationData != null) {
          // Just much easier to write the C# and have the compiler generate everything than to try and create it all manually
          System.Reflection.Assembly embeddedAssembly;
          Stream embeddedAssemblyStream;
          embeddedAssembly = System.Reflection.Assembly.GetExecutingAssembly();
          embeddedAssemblyStream = embeddedAssembly.GetManifestResourceStream("Microsoft.Contracts.Foxtrot.InternalException.dll");
          byte[] data = new byte[0];
          using (var br = new BinaryReader(embeddedAssemblyStream)) {
            var len = embeddedAssemblyStream.Length;
            if (len < Int32.MaxValue)
              data = br.ReadBytes((int)len);
            AssemblyNode assemblyNode = AssemblyNode.GetAssembly(data, TargetPlatform.StaticAssemblyCache, true, false, true);
            contractExceptionType = assemblyNode.GetType(Identifier.For(""), Identifier.For("ContractException")) as Class;
          }
          if (contractExceptionType == null)
            throw new RewriteException("Tried to create the ContractException type from the embedded dll, but failed");
          var d = new Duplicator(this.targetAssembly, this.RuntimeContractType);
          d.FindTypesToBeDuplicated(new TypeNodeList(contractExceptionType));

          var ct = d.Visit(contractExceptionType);
          contractExceptionType = (Class)ct;
          contractExceptionType.Flags |= TypeFlags.NestedPrivate;
          this.RuntimeContractType.Members.Add(contractExceptionType);
          return contractExceptionType;
        }
      }
      #endregion

      contractExceptionType = new Class(this.targetAssembly, this.RuntimeContractType, new AttributeList(), TypeFlags.Class | TypeFlags.NestedPrivate | TypeFlags.Serializable, null, Identifier.For("ContractException"), SystemTypes.Exception, null, null);

      RewriteHelper.TryAddCompilerGeneratedAttribute(contractExceptionType);

      var kindField = new Field(contractExceptionType, null, FieldFlags.Private, Identifier.For("_Kind"), contractNodes.ContractFailureKind, null);
      var userField = new Field(contractExceptionType, null, FieldFlags.Private, Identifier.For("_UserMessage"), SystemTypes.String, null);
      var condField = new Field(contractExceptionType, null, FieldFlags.Private, Identifier.For("_Condition"), SystemTypes.String, null);
      contractExceptionType.Members.Add(kindField);
      contractExceptionType.Members.Add(userField);
      contractExceptionType.Members.Add(condField);

      #region Constructor for setting the fields
      var parameters = new ParameterList();
      var kindParam = new Parameter(Identifier.For("kind"), this.contractNodes.ContractFailureKind);
      var failureParam = new Parameter(Identifier.For("failure"), SystemTypes.String);
      var usermsgParam = new Parameter(Identifier.For("usermsg"), SystemTypes.String);
      var conditionParam = new Parameter(Identifier.For("condition"), SystemTypes.String);
      var innerParam = new Parameter(Identifier.For("inner"), SystemTypes.Exception);
      parameters.Add(kindParam);
      parameters.Add(failureParam);
      parameters.Add(usermsgParam);
      parameters.Add(conditionParam);
      parameters.Add(innerParam);
      var body = new Block(new StatementList());
      var ctor = new InstanceInitializer(contractExceptionType, null, parameters, body);
      ctor.Flags |= MethodFlags.Public | MethodFlags.HideBySig;
      ctor.CallingConvention = CallingConventionFlags.HasThis;
      body.Statements.Add(new ExpressionStatement(new MethodCall(new MemberBinding(ctor.ThisParameter, contractExceptionType.BaseClass.GetConstructor(SystemTypes.String, SystemTypes.Exception)), new ExpressionList(failureParam, innerParam))));
      body.Statements.Add(new AssignmentStatement(new MemberBinding(ctor.ThisParameter, kindField), kindParam));
      body.Statements.Add(new AssignmentStatement(new MemberBinding(ctor.ThisParameter, userField), usermsgParam));
      body.Statements.Add(new AssignmentStatement(new MemberBinding(ctor.ThisParameter, condField), conditionParam));
      body.Statements.Add(new Return());
      contractExceptionType.Members.Add(ctor);
      #endregion

      if (SystemTypes.SerializationInfo != null && SystemTypes.SerializationInfo.BaseClass != null) {
        // Silverlight (e.g.) is a platform that doesn't support serialization. So check to make sure the type really exists.
        // 
        var baseCtor = SystemTypes.Exception.GetConstructor(SystemTypes.SerializationInfo, SystemTypes.StreamingContext);

        if (baseCtor != null) {
          #region Deserialization Constructor
          parameters = new ParameterList();
          var info = new Parameter(Identifier.For("info"), SystemTypes.SerializationInfo);
          var context = new Parameter(Identifier.For("context"), SystemTypes.StreamingContext);
          parameters.Add(info);
          parameters.Add(context);
          body = new Block(new StatementList());
          ctor = new InstanceInitializer(contractExceptionType, null, parameters, body);
          ctor.Flags |= MethodFlags.Private | MethodFlags.HideBySig;
          ctor.CallingConvention = CallingConventionFlags.HasThis;
          // : base(info, context) 
          body.Statements.Add(new ExpressionStatement(new MethodCall(new MemberBinding(ctor.ThisParameter, baseCtor), new ExpressionList(info, context))));
          // _Kind = (ContractFailureKind)info.GetInt32("Kind");
          var getInt32 = SystemTypes.SerializationInfo.GetMethod(Identifier.For("GetInt32"), SystemTypes.String);
          body.Statements.Add(new AssignmentStatement(
              new MemberBinding(new This(), kindField),
              new MethodCall(new MemberBinding(info, getInt32), new ExpressionList(new Literal("Kind", SystemTypes.String)))
              ));
          // _UserMessage = info.GetString("UserMessage");
          var getString = SystemTypes.SerializationInfo.GetMethod(Identifier.For("GetString"), SystemTypes.String);
          body.Statements.Add(new AssignmentStatement(
              new MemberBinding(new This(), userField),
              new MethodCall(new MemberBinding(info, getString), new ExpressionList(new Literal("UserMessage", SystemTypes.String)))
              ));
          // _Condition = info.GetString("Condition");
          body.Statements.Add(new AssignmentStatement(
              new MemberBinding(new This(), condField),
              new MethodCall(new MemberBinding(info, getString), new ExpressionList(new Literal("Condition", SystemTypes.String)))
              ));
          body.Statements.Add(new Return());
          contractExceptionType.Members.Add(ctor);
          #endregion

          #region GetObjectData

          var securityCriticalCtor = SystemTypes.SecurityCriticalAttribute.GetConstructor();
          var securityCriticalAttribute = new AttributeNode(new MemberBinding(null, securityCriticalCtor), null, System.AttributeTargets.Method);
          var attrs = new AttributeList(securityCriticalAttribute);
          parameters = new ParameterList();
          info = new Parameter(Identifier.For("info"), SystemTypes.SerializationInfo);
          context = new Parameter(Identifier.For("context"), SystemTypes.StreamingContext);
          parameters.Add(info);
          parameters.Add(context);
          body = new Block(new StatementList());
          var getObjectDataName = Identifier.For("GetObjectData");
          var getObjectData = new Method(contractExceptionType, attrs, getObjectDataName, parameters, SystemTypes.Void, body);
          getObjectData.CallingConvention = CallingConventionFlags.HasThis;
          // public override
          getObjectData.Flags = MethodFlags.Public | MethodFlags.Virtual;
          // base.GetObjectData(info, context);
          var baseGetObjectData = SystemTypes.Exception.GetMethod(getObjectDataName, SystemTypes.SerializationInfo, SystemTypes.StreamingContext);
          body.Statements.Add(new ExpressionStatement(
            new MethodCall(new MemberBinding(new This(), baseGetObjectData), new ExpressionList(info, context), NodeType.Call, SystemTypes.Void)
            ));
          // info.AddValue("Kind", _Kind);
          var addValueObject = SystemTypes.SerializationInfo.GetMethod(Identifier.For("AddValue"), SystemTypes.String, SystemTypes.Object);
          body.Statements.Add(new ExpressionStatement(
            new MethodCall(new MemberBinding(info, addValueObject), new ExpressionList(new Literal("Kind", SystemTypes.String), new BinaryExpression(new MemberBinding(new This(), kindField), new Literal(contractNodes.ContractFailureKind), NodeType.Box)), NodeType.Call, SystemTypes.Void)
            ));
          // info.AddValue("UserMessage", _UserMessage);
          body.Statements.Add(new ExpressionStatement(
            new MethodCall(new MemberBinding(info, addValueObject), new ExpressionList(new Literal("UserMessage", SystemTypes.String), new MemberBinding(new This(), userField)), NodeType.Call, SystemTypes.Void)
            ));
          // info.AddValue("Condition", _Condition);
          body.Statements.Add(new ExpressionStatement(
            new MethodCall(new MemberBinding(info, addValueObject), new ExpressionList(new Literal("Condition", SystemTypes.String), new MemberBinding(new This(), condField)), NodeType.Call, SystemTypes.Void)
            ));

          body.Statements.Add(new Return());
          contractExceptionType.Members.Add(getObjectData);
          #endregion
        }
      }

      this.RuntimeContractType.Members.Add(contractExceptionType);
      return contractExceptionType;
    }


    private Method ContractExceptionCtor
    {
      get
      {
        TypeNode contractException = this.ContractExceptionType;
        var result = contractException.GetConstructor(contractNodes.ContractFailureKind, SystemTypes.String, SystemTypes.String, SystemTypes.String, SystemTypes.Exception);
        if (result == null)
        {
          throw new ApplicationException("Can't find constructor ContractException(ContractFailureKind, string, string, string, Exception).");
        }
        return result;
      }
    }

    private TypeNode RuntimeContractType
    {
      get
      {
        Contract.Ensures(Contract.Result<TypeNode>() != null);

        if (this.runtimeContractType == null)
        {
          // generate
          this.runtimeContractType = new Class(this.targetAssembly,
            null, /* declaringType */
            new AttributeList(),
            TypeFlags.Abstract | TypeFlags.Sealed,
            ContractNodes.ContractNamespace,
// for debugging            Identifier.For("__ContractsRuntime" + this.targetAssembly.UniqueKey),
            Identifier.For("__ContractsRuntime"),
            SystemTypes.Object,
            new InterfaceList(),
            new MemberList(0));
          RewriteHelper.TryAddCompilerGeneratedAttribute(runtimeContractType);
        }
        return runtimeContractType;
      }
    }

    private Field inContractEvaluationField;
    internal Field InContractEvaluationField
    {
      get
      {
        if (inContractEvaluationField == null)
        {
          inContractEvaluationField = new Field(RuntimeContractType, null, FieldFlags.Assembly | FieldFlags.Static, Identifier.For("insideContractEvaluation"), SystemTypes.Int32, null);
          TypeNode threadStaticAttribute = SystemTypes.SystemAssembly.GetType(Identifier.For("System"), Identifier.For("ThreadStaticAttribute"));
          if (threadStaticAttribute != null)
          {
            inContractEvaluationField.Attributes.Add(new AttributeNode(new MemberBinding(null, threadStaticAttribute.GetConstructor()), null));
          }
          runtimeContractType.Members.Add(inContractEvaluationField);
        }
        return inContractEvaluationField;
      }
    }

    /// <summary>
    /// Constructs (and returns) a method that looks like this:
    /// 
    ///   [System.Diagnostics.DebuggerNonUserCodeAttribute]
    ///   [System.Runtime.ConstrainedExecution.ReliabilityContractReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
    ///   static void name(bool condition, string message, string conditionText){
    ///     if (!condition) {
    ///       System.Diagnostics.Contracts.Contract.Failure(kind, message, conditionText, null);
    ///     }
    ///   }
    ///
    /// Or, if the ContractFailureKind is PostconditionOnException, then the generated method
    /// gets an extra parameter which is an Exception and that parameter is passed to Contract.Failure instead of null
    /// </summary>
    private Method MakeMethod(string name, ContractFailureKind kind)
    {
      Parameter conditionParameter = new Parameter(Identifier.For("condition"), SystemTypes.Boolean);
      Parameter messageParameter = new Parameter(Identifier.For("msg"), SystemTypes.String);
      Parameter conditionTextParameter = new Parameter(Identifier.For("conditionTxt"), SystemTypes.String);
      Parameter exceptionParameter = new Parameter(Identifier.For("originalException"), SystemTypes.Exception);

      Block returnBlock = new Block(new StatementList(new Return()));

      Block body = new Block(new StatementList());
      Block b = new Block(new StatementList());
      b.Statements.Add(new Branch(conditionParameter, returnBlock));
      ExpressionList elist = new ExpressionList();
      elist.Add(TranslateKindForBackwardCompatibility(kind));
      elist.Add(messageParameter);
      elist.Add(conditionTextParameter);
      if (kind == ContractFailureKind.PostconditionOnException)
      {
        elist.Add(exceptionParameter);
      }
      else
      {
        elist.Add(Literal.Null);
      }
      b.Statements.Add(new ExpressionStatement(new MethodCall(new MemberBinding(null, this.FailureMethod), elist)));
      b.Statements.Add(returnBlock);
      body.Statements.Add(b);

      ParameterList pl = new ParameterList(conditionParameter, messageParameter, conditionTextParameter);
      if (kind == ContractFailureKind.PostconditionOnException)
      {
        pl.Add(exceptionParameter);
      }
      Method m = new Method(this.RuntimeContractType, null, Identifier.For(name), pl, SystemTypes.Void, body);
      m.Flags = MethodFlags.Assembly | MethodFlags.Static;
      m.Attributes = new AttributeList();
      this.RuntimeContractType.Members.Add(m);

      Member constructor = null;
      AttributeNode attribute = null;

      #region Add [DebuggerNonUserCodeAttribute]
      if (this.HideFromDebugger) {
        TypeNode debuggerNonUserCode = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Diagnostics"), Identifier.For("DebuggerNonUserCodeAttribute"));
        if (debuggerNonUserCode != null)
        {
          constructor = debuggerNonUserCode.GetConstructor();
          attribute = new AttributeNode(new MemberBinding(null, constructor), null, AttributeTargets.Method);
          m.Attributes.Add(attribute);
        }
      }
      #endregion Add [DebuggerNonUserCodeAttribute]

      TypeNode reliabilityContract = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"), Identifier.For("ReliabilityContractAttribute"));
      TypeNode consistency = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"), Identifier.For("Consistency"));
      TypeNode cer = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"), Identifier.For("Cer"));
      if (reliabilityContract != null && consistency != null && cer != null) {
        constructor = reliabilityContract.GetConstructor(consistency, cer);
        if (constructor != null) {
          attribute = new AttributeNode(
            new MemberBinding(null, constructor),
            new ExpressionList(
              new Literal(System.Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, consistency),
              new Literal(System.Runtime.ConstrainedExecution.Cer.MayFail, cer)),
              AttributeTargets.Method
              );
          m.Attributes.Add(attribute);
        }
      }
      return m;
    }

    private Literal TranslateKindForBackwardCompatibility(ContractFailureKind kind)
    {
      switch (kind)
      {
        case ContractFailureKind.Assert:
          {
            var field = this.contractNodes.ContractFailureKind.GetField(Identifier.For("Assert"));
            Contract.Assume(field != null);
            return field.DefaultValue;
          }
        case ContractFailureKind.Assume:
          {
            var field = this.contractNodes.ContractFailureKind.GetField(Identifier.For("Assume"));
            Contract.Assume(field != null);
            return field.DefaultValue;
          }
        case ContractFailureKind.Invariant:
          {
            var field = this.contractNodes.ContractFailureKind.GetField(Identifier.For("Invariant"));
            Contract.Assume(field != null);
            return field.DefaultValue;
          }
        case ContractFailureKind.Postcondition:
          {
            var field = this.contractNodes.ContractFailureKind.GetField(Identifier.For("Postcondition"));
            Contract.Assume(field != null);
            return field.DefaultValue;
          }
        case ContractFailureKind.PostconditionOnException:
          {
            var pe = this.contractNodes.ContractFailureKind.GetField(Identifier.For("PostconditionOnException"));
            // hack
            if (pe == null)
            { // Old CLR 4.0 beta
              var field = this.contractNodes.ContractFailureKind.GetField(Identifier.For("Postcondition"));
              Contract.Assume(field != null);
              return field.DefaultValue;
            }
            return pe.DefaultValue;
          }
        case ContractFailureKind.Precondition:
          {
            var field = this.contractNodes.ContractFailureKind.GetField(Identifier.For("Precondition"));
            Contract.Assume(field != null);
            return field.DefaultValue;
          }
        default:
          {
            throw new ApplicationException(String.Format("Unexpected failure kind {0}", kind.ToString()));
          }
      }
    }

    [ContractVerification(true)]
    private Method MakeRequiresWithExceptionMethod(string name)
    {
      Parameter conditionParameter = new Parameter(Identifier.For("condition"), SystemTypes.Boolean);
      Parameter messageParameter = new Parameter(Identifier.For("msg"), SystemTypes.String);
      Parameter conditionTextParameter = new Parameter(Identifier.For("conditionTxt"), SystemTypes.String);
      MethodClassParameter typeArg = new MethodClassParameter();
      ParameterList pl = new ParameterList(conditionParameter, messageParameter, conditionTextParameter);

      Block body = new Block(new StatementList());
      Method m = new Method(this.RuntimeContractType, null, Identifier.For(name), pl, SystemTypes.Void, body);
      m.Flags = MethodFlags.Assembly | MethodFlags.Static;
      m.Attributes = new AttributeList();
      m.TemplateParameters = new TypeNodeList(typeArg);
      m.IsGeneric = true;
      this.RuntimeContractType.Members.Add(m);

      typeArg.Name = Identifier.For("TException");
      typeArg.DeclaringMember = m;
      typeArg.BaseClass = SystemTypes.Exception;
      typeArg.ParameterListIndex = 0;
      typeArg.DeclaringModule = this.RuntimeContractType.DeclaringModule;

      Block returnBlock = new Block(new StatementList(new Return()));

      Block b = new Block(new StatementList());
      b.Statements.Add(new Branch(conditionParameter, returnBlock));
      //
      // Generate the following
      //

      // // message == null means: yes we handled it. Otherwise it is the localized failure message
      // var message = RaiseFailureEvent(ContractFailureKind.Precondition, userMessage, conditionString, null);
      // #if assertOnFailure
      // if (message != null) {
      //   Assert(false, message);
      // }
      // #endif

      var messageLocal = new Local(Identifier.For("msg"), SystemTypes.String);
      
      ExpressionList elist = new ExpressionList();
      elist.Add(this.PreconditionKind);
      elist.Add(messageParameter);
      elist.Add(conditionTextParameter);
      elist.Add(Literal.Null);
      b.Statements.Add(new AssignmentStatement(messageLocal, new MethodCall(new MemberBinding(null, this.RaiseFailureEventMethod), elist)));

      if (this.AssertOnFailure)
      {
        var assertMethod = GetSystemDiagnosticsAssertMethod();
        if (assertMethod != null)
        {
          var skipAssert = new Block();
          b.Statements.Add(new Branch(new UnaryExpression(messageLocal, NodeType.LogicalNot), skipAssert));
          // emit assert call
          ExpressionList assertelist = new ExpressionList();
          assertelist.Add(Literal.False);
          assertelist.Add(messageLocal);
          b.Statements.Add(new ExpressionStatement(new MethodCall(new MemberBinding(null, assertMethod), assertelist)));
          b.Statements.Add(skipAssert);
        }
      }
      // // construct exception
      // Exception obj = null;
      // ConstructorInfo ci = typeof(TException).GetConstructor(new[] { typeof(string), typeof(string) });
      // if (ci != null)
      // {
      //   if (reflection.firstArgName == "paramName") {
      //     exceptionObject = ci.Invoke(new[] { userMessage, message }) as Exception;
      //   }
      //   else {
      //     exceptionObject = ci.Invoke(new[] { message, userMessage }) as Exception;
      //   }
      // }
      // else {
      //   ci = typeof(TException).GetConstructor(new[] { typeof(string) });
      //   if (ci != null)
      //   {
      //     exceptionObject = ci.Invoke(new[] { message }) as Exception;
      //   }
      // }
      var exceptionLocal = new Local(Identifier.For("obj"), SystemTypes.Exception);
      b.Statements.Add(new AssignmentStatement(exceptionLocal, Literal.Null));
      var constructorInfoType = TypeNode.GetTypeNode(typeof(System.Reflection.ConstructorInfo));
      Contract.Assume(constructorInfoType != null);
      var methodBaseType = TypeNode.GetTypeNode(typeof(System.Reflection.MethodBase));
      Contract.Assume(methodBaseType != null);
      var constructorLocal = new Local(Identifier.For("ci"), constructorInfoType);
      Contract.Assume(SystemTypes.Type != null);
      var getConstructorMethod = SystemTypes.Type.GetMethod(Identifier.For("GetConstructor"), SystemTypes.Type.GetArrayType(1));
      var typeofExceptionArg = GetTypeFromHandleExpression(typeArg);
      var typeofString = GetTypeFromHandleExpression(SystemTypes.String);
      var typeArrayLocal = new Local(Identifier.For("typeArray"), SystemTypes.Type.GetArrayType(1));
      var typeArray2 = new ConstructArray();
      typeArray2.ElementType = SystemTypes.Type;
      typeArray2.Rank = 1;
      typeArray2.Operands = new ExpressionList(Literal.Int32Two);
      var typeArrayInit2 = new Block(new StatementList());
      typeArrayInit2.Statements.Add(new AssignmentStatement(typeArrayLocal, typeArray2));
      typeArrayInit2.Statements.Add(new AssignmentStatement(new Indexer(typeArrayLocal, new ExpressionList(Literal.Int32Zero), SystemTypes.Type), typeofString));
      typeArrayInit2.Statements.Add(new AssignmentStatement(new Indexer(typeArrayLocal, new ExpressionList(Literal.Int32One), SystemTypes.Type), typeofString));
      typeArrayInit2.Statements.Add(new ExpressionStatement(typeArrayLocal));
      var typeArrayExpression2 = new BlockExpression(typeArrayInit2);
      b.Statements.Add(new AssignmentStatement(constructorLocal, new MethodCall(new MemberBinding(typeofExceptionArg, getConstructorMethod), new ExpressionList(typeArrayExpression2))));
      var elseBlock = new Block();
      b.Statements.Add(new Branch(new UnaryExpression(constructorLocal, NodeType.LogicalNot), elseBlock));
      var endifBlock2 = new Block();

      var invokeMethod = constructorInfoType.GetMethod(Identifier.For("Invoke"), SystemTypes.Object.GetArrayType(1));
      var argArray2 = new ConstructArray();
      argArray2.ElementType = SystemTypes.Object;
      argArray2.Rank = 1;
      argArray2.Operands = new ExpressionList(Literal.Int32Two);
      var argArrayLocal = new Local(Identifier.For("argArray"), SystemTypes.Object.GetArrayType(1));

      var parameterInfoType = TypeNode.GetTypeNode(typeof(System.Reflection.ParameterInfo));
      Contract.Assume(parameterInfoType != null);
      var parametersMethod = methodBaseType.GetMethod(Identifier.For("GetParameters"));
      var get_NameMethod = parameterInfoType.GetMethod(Identifier.For("get_Name"));
      var string_op_EqualityMethod = SystemTypes.String.GetMethod(Identifier.For("op_Equality"), SystemTypes.String, SystemTypes.String);
      var elseArgMsgBlock = new Block();
      var endIfArgMsgBlock = new Block();
      b.Statements.Add(new Branch(new UnaryExpression(new MethodCall(new MemberBinding(null, string_op_EqualityMethod), new ExpressionList(new MethodCall(new MemberBinding(new Indexer(new MethodCall(new MemberBinding(constructorLocal, parametersMethod), new ExpressionList(), NodeType.Callvirt), new ExpressionList(Literal.Int32Zero), parameterInfoType), get_NameMethod), new ExpressionList(), NodeType.Callvirt), new Literal("paramName", SystemTypes.String))), NodeType.LogicalNot), elseArgMsgBlock));

      var argArrayInit2 = new Block(new StatementList());
      argArrayInit2.Statements.Add(new AssignmentStatement(argArrayLocal, argArray2));
      argArrayInit2.Statements.Add(new AssignmentStatement(new Indexer(argArrayLocal, new ExpressionList(Literal.Int32Zero), SystemTypes.Object), messageParameter));
      argArrayInit2.Statements.Add(new AssignmentStatement(new Indexer(argArrayLocal, new ExpressionList(Literal.Int32One), SystemTypes.Object), messageLocal));
      argArrayInit2.Statements.Add(new ExpressionStatement(argArrayLocal));
      var argArrayExpression2 = new BlockExpression(argArrayInit2);
      b.Statements.Add(new AssignmentStatement(exceptionLocal, new BinaryExpression(new MethodCall(new MemberBinding(constructorLocal, invokeMethod), new ExpressionList(argArrayExpression2)), new Literal(SystemTypes.Exception), NodeType.Isinst)));
      b.Statements.Add(new Branch(null, endIfArgMsgBlock));
 
      b.Statements.Add(elseArgMsgBlock);
      var argArrayInit2_1 = new Block(new StatementList());
      argArrayInit2_1.Statements.Add(new AssignmentStatement(argArrayLocal, argArray2));
      argArrayInit2_1.Statements.Add(new AssignmentStatement(new Indexer(argArrayLocal, new ExpressionList(Literal.Int32Zero), SystemTypes.Object), messageLocal));
      argArrayInit2_1.Statements.Add(new AssignmentStatement(new Indexer(argArrayLocal, new ExpressionList(Literal.Int32One), SystemTypes.Object), messageParameter));
      argArrayInit2_1.Statements.Add(new ExpressionStatement(argArrayLocal));
      var argArrayExpression2_1 = new BlockExpression(argArrayInit2_1);
      b.Statements.Add(new AssignmentStatement(exceptionLocal, new BinaryExpression(new MethodCall(new MemberBinding(constructorLocal, invokeMethod), new ExpressionList(argArrayExpression2_1)), new Literal(SystemTypes.Exception), NodeType.Isinst)));

      b.Statements.Add(endIfArgMsgBlock);

      b.Statements.Add(new Branch(null, endifBlock2));

      b.Statements.Add(elseBlock);
      var typeArray1 = new ConstructArray();
      typeArray1.ElementType = SystemTypes.Type;
      typeArray1.Rank = 1;
      typeArray1.Operands = new ExpressionList(Literal.Int32One);
      var typeArrayInit1 = new Block(new StatementList());
      typeArrayInit1.Statements.Add(new AssignmentStatement(typeArrayLocal, typeArray1));
      typeArrayInit1.Statements.Add(new AssignmentStatement(new Indexer(typeArrayLocal, new ExpressionList(Literal.Int32Zero), SystemTypes.Type), typeofString));
      typeArrayInit1.Statements.Add(new ExpressionStatement(typeArrayLocal));
      var typeArrayExpression1 = new BlockExpression(typeArrayInit1);
      b.Statements.Add(new AssignmentStatement(constructorLocal, new MethodCall(new MemberBinding(typeofExceptionArg, getConstructorMethod), new ExpressionList(typeArrayExpression1))));

      b.Statements.Add(new Branch(new UnaryExpression(constructorLocal, NodeType.LogicalNot), endifBlock2));
      var argArray1 = new ConstructArray();
      argArray1.ElementType = SystemTypes.Object;
      argArray1.Rank = 1;
      argArray1.Operands = new ExpressionList(Literal.Int32One);
      var argArrayInit1 = new Block(new StatementList());
      argArrayInit1.Statements.Add(new AssignmentStatement(argArrayLocal, argArray1));
      argArrayInit1.Statements.Add(new AssignmentStatement(new Indexer(argArrayLocal, new ExpressionList(Literal.Int32Zero), SystemTypes.Object), messageLocal));
      argArrayInit1.Statements.Add(new ExpressionStatement(argArrayLocal));
      var argArrayExpression1 = new BlockExpression(argArrayInit1);
      b.Statements.Add(new AssignmentStatement(exceptionLocal, new BinaryExpression(new MethodCall(new MemberBinding(constructorLocal, invokeMethod), new ExpressionList(argArrayExpression1)), new Literal(SystemTypes.Exception), NodeType.Isinst)));

      b.Statements.Add(endifBlock2);

      // // throw it
      // if (exceptionObject == null)
      // {
      //   throw new ArgumentException(displayMessage, message);
      // }
      // else
      // {
      //   throw exceptionObject;
      // }
      var thenBlock3 = new Block();

      b.Statements.Add(new Branch(exceptionLocal, thenBlock3));
      b.Statements.Add(new Throw(new Construct(new MemberBinding(null, SystemTypes.ArgumentException.GetConstructor(SystemTypes.String, SystemTypes.String)), new ExpressionList(messageLocal, messageParameter))));
      b.Statements.Add(thenBlock3);
      b.Statements.Add(new Throw(exceptionLocal));
      b.Statements.Add(returnBlock);
      Contract.Assume(body.Statements != null);
      body.Statements.Add(b);

      Member constructor = null;
      AttributeNode attribute = null;

      #region Add [DebuggerNonUserCodeAttribute]
      if (this.HideFromDebugger) {
        TypeNode debuggerNonUserCode = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Diagnostics"), Identifier.For("DebuggerNonUserCodeAttribute"));
        if (debuggerNonUserCode != null)
        {
            constructor = debuggerNonUserCode.GetConstructor();
            attribute = new AttributeNode(new MemberBinding(null, constructor), null, AttributeTargets.Method);
            m.Attributes.Add(attribute);
        }
      }
      #endregion Add [DebuggerNonUserCodeAttribute]

      TypeNode reliabilityContract = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"), Identifier.For("ReliabilityContractAttribute"));
      TypeNode consistency = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"), Identifier.For("Consistency"));
      TypeNode cer = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"), Identifier.For("Cer"));
      if (reliabilityContract != null && consistency != null && cer != null)
      {
          constructor = reliabilityContract.GetConstructor(consistency, cer);
          if (constructor != null)
          {
              attribute = new AttributeNode(
                new MemberBinding(null, constructor),
                new ExpressionList(
                  new Literal(System.Runtime.ConstrainedExecution.Consistency.WillNotCorruptState, consistency),
                  new Literal(System.Runtime.ConstrainedExecution.Cer.MayFail, cer)),
                  AttributeTargets.Method
                  );
              m.Attributes.Add(attribute);
          }
      }
      return m;
    }

    internal static Method GetSystemDiagnosticsAssertMethod()
    {
      var sysref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[Identifier.For("System").UniqueIdKey];
      if (sysref == null) return null;
      var sysassem = AssemblyNode.GetAssembly(sysref, TargetPlatform.StaticAssemblyCache, true, false, true);
      if (sysassem == null) return null;
      var debugType = sysassem.GetType(Identifier.For("System.Diagnostics"), Identifier.For("Debug"));
      if (debugType == null) return null;
      var assertMethod = debugType.GetMethod(Identifier.For("Assert"), SystemTypes.Boolean, SystemTypes.String);
      if (assertMethod == null) return null;

      return assertMethod;
    }

    private static Expression GetTypeFromHandleExpression(TypeNode typeArg)
    {
      return new MethodCall(new MemberBinding(null, SystemTypes.Type.GetMethod(Identifier.For("GetTypeFromHandle"), SystemTypes.RuntimeTypeHandle)), new ExpressionList(new UnaryExpression(new Literal(typeArg), NodeType.Ldtoken)));
    }

    private Method MakeFailureMethod()
    {
      Parameter kindParameter = new Parameter(Identifier.For("kind"), contractNodes.ContractFailureKind);
      Parameter messageParameter = new Parameter(Identifier.For("msg"), SystemTypes.String);
      Parameter conditionTextParameter = new Parameter(Identifier.For("conditionTxt"), SystemTypes.String);
      Parameter exceptionParameter = new Parameter(Identifier.For("inner"), SystemTypes.Exception);
      ParameterList pl = new ParameterList(kindParameter, messageParameter, conditionTextParameter, exceptionParameter);

      Block body = new Block(new StatementList());
      Method m = new Method(this.RuntimeContractType, null, ContractNodes.ReportFailureName, pl, SystemTypes.Void, body);
      m.Flags = MethodFlags.Assembly | MethodFlags.Static;
      m.Attributes = new AttributeList();
      this.RuntimeContractType.Members.Add(m);

      Block returnBlock = new Block(new StatementList(new Return()));

      //
      // Generate the following
      //

      // // message == null means: yes we handled it. Otherwise it is the localized failure message
      // var message = RaiseFailureEvent(kind, userMessage, conditionString, inner);
      // if (message == null) return;

      var messageLocal = new Local(Identifier.For("msg"), SystemTypes.String);

      ExpressionList elist = new ExpressionList();
      elist.Add(kindParameter);
      elist.Add(messageParameter);
      elist.Add(conditionTextParameter);
      elist.Add(exceptionParameter);
      body.Statements.Add(new AssignmentStatement(messageLocal, new MethodCall(new MemberBinding(null, this.RaiseFailureEventMethod), elist)));
      body.Statements.Add(new Branch(new UnaryExpression(messageLocal, NodeType.LogicalNot), returnBlock));

      body.Statements.Add(new ExpressionStatement(new MethodCall(new MemberBinding(null, this.TriggerFailureMethod), new ExpressionList(kindParameter, messageLocal, messageParameter, conditionTextParameter, exceptionParameter))));
      body.Statements.Add(returnBlock);
      return m;
    }

    private Method MakeRaiseFailureEventMethod()
    {
      Parameter kindParameter = new Parameter(Identifier.For("kind"), contractNodes.ContractFailureKind);
      Parameter messageParameter = new Parameter(Identifier.For("msg"), SystemTypes.String);
      Parameter conditionTextParameter = new Parameter(Identifier.For("conditionTxt"), SystemTypes.String);
      Parameter exceptionParameter = new Parameter(Identifier.For("inner"), SystemTypes.Exception);
      ParameterList pl = new ParameterList(kindParameter, messageParameter, conditionTextParameter, exceptionParameter);

      Block body = new Block(new StatementList());
      Method m = new Method(this.RuntimeContractType, null, ContractNodes.RaiseContractFailedEventName, pl, SystemTypes.String, body);
      m.Flags = MethodFlags.Assembly | MethodFlags.Static;
      m.Attributes = new AttributeList();
      this.RuntimeContractType.Members.Add(m);

      //
      // Generate the following
      //

      // return String.Format("{0} failed: {1}: {2}", box(kind), conditionText, message);

      var stringFormat3 = SystemTypes.String.GetMethod(Identifier.For("Format"), SystemTypes.String, SystemTypes.Object, SystemTypes.Object, SystemTypes.Object);
      body.Statements.Add(new Return(new MethodCall(new MemberBinding(null, stringFormat3), new ExpressionList(new Literal("{0} failed: {1}: {2}"), new BinaryExpression(kindParameter, new Literal(contractNodes.ContractFailureKind), NodeType.Box), conditionTextParameter, messageParameter))));

      return m;
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant", Justification = "Static field")]
    private Method MakeTriggerFailureMethod(bool throwOnFailure)
    {
      Parameter kindParameter = new Parameter(Identifier.For("kind"), contractNodes.ContractFailureKind);
      Parameter messageParameter = new Parameter(Identifier.For("msg"), SystemTypes.String);
      Parameter userMessageParameter = new Parameter(Identifier.For("userMessage"), SystemTypes.String);
      Parameter conditionTextParameter = new Parameter(Identifier.For("conditionTxt"), SystemTypes.String);
      Parameter exceptionParameter = new Parameter(Identifier.For("inner"), SystemTypes.Exception);
      ParameterList pl = new ParameterList(kindParameter, messageParameter, userMessageParameter, conditionTextParameter, exceptionParameter);

      Block body = new Block(new StatementList());
      Method m = new Method(this.RuntimeContractType, null, ContractNodes.TriggerFailureName, pl, SystemTypes.Void, body);
      m.Flags = MethodFlags.Assembly | MethodFlags.Static;
      m.Attributes = new AttributeList();
      this.RuntimeContractType.Members.Add(m);

      //
      // Generate the following
      //
      // #if throwOnFailure
      // throw new ContractException(kind, message, userMessage, conditionText, inner);
      // #else
      // Debug.Assert(false, messageParameter);
      // #endif

      if (throwOnFailure)
      {
        ExpressionList elist = new ExpressionList();
        elist.Add(kindParameter);
        elist.Add(messageParameter);
        elist.Add(userMessageParameter);
        elist.Add(conditionTextParameter);
        elist.Add(exceptionParameter);
        body.Statements.Add(new Throw(new Construct(new MemberBinding(null, this.ContractExceptionCtor), elist)));
      }
      else
      {
        var sysassem = SystemTypes.SystemDllAssembly;
        if (sysassem == null) {
          throw new ApplicationException("Cannot find System.dll");
        }
        var debugType = sysassem.GetType(Identifier.For("System.Diagnostics"), Identifier.For("Debug"));
        if (debugType == null)
        {
          throw new ApplicationException("Cannot find System.Diagnostics.Debug");
        }
        var assertMethod = debugType.GetMethod(Identifier.For("Assert"), SystemTypes.Boolean, SystemTypes.String);
        if (assertMethod == null)
        {
          throw new ApplicationException("Cannot find System.Diagnostics.Debug.Assert(bool,string)");
        }
        ExpressionList elist = new ExpressionList();
        elist.Add(Literal.False);
        elist.Add(messageParameter);
        body.Statements.Add(new ExpressionStatement(new MethodCall(new MemberBinding(null, assertMethod), elist)));
        body.Statements.Add(new Return());
      }

      return m;
    }
    /// <summary>
    /// Called after assembly visit to avoid visiting the generated runtime contract class, we only add it to the target
    /// here.
    /// </summary>
    internal void Commit()
    {
      if (this.runtimeContractType != null)
      {
        this.targetAssembly.Types.Add(this.runtimeContractType);
      }
    }
  }

  [ContractVerification(false)]
  public sealed class Rewriter : Inspector {

    #region Private fields
    ContractNodes rewriterNodes;

    /// <summary>
    /// Set for the subvisit from a type with invariants. Used for constructors to avoid checking invariants too early
    /// when methods are called from the constructor.
    /// </summary>
    Field ReentrancyFlag = null;
    Method InvariantMethod = null;

    bool verbose = false;
    Module module = null;

    AssemblyNode assemblyBeingRewritten;

    private Method IDisposeMethod = null;

    private RuntimeContractEmitFlags contractEmitFlags;

    private RuntimeContractMethods runtimeContracts;

    private Dictionary<Method, bool> assertAssumeRewriteMethodTable;

    private Dictionary<Method, bool> AssertAssumeRewriteMethodTable
    {
      get
      {
        if (assertAssumeRewriteMethodTable == null)
        {
          assertAssumeRewriteMethodTable = new Dictionary<Method, bool>(4);
          assertAssumeRewriteMethodTable.Add(this.rewriterNodes.AssertMethod, true);
          assertAssumeRewriteMethodTable.Add(this.rewriterNodes.AssertWithMsgMethod, true);
          assertAssumeRewriteMethodTable.Add(this.rewriterNodes.AssumeMethod, true);
          assertAssumeRewriteMethodTable.Add(this.rewriterNodes.AssumeWithMsgMethod, true);
        }
        return assertAssumeRewriteMethodTable;
      }
    }

    Action<System.CodeDom.Compiler.CompilerError> m_handleError;

    private CurrentState currentState;

    private struct CurrentState {
      public readonly Method Method;
      public readonly TypeNode Type;
      private Dictionary<string, object> typeSuppressed;
      private Dictionary<string, object> methodSuppressed;

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
      private CurrentState(TypeNode type, Dictionary<string, object> typeSuppressed, Method method) {
        this.Type = type;
        this.typeSuppressed = typeSuppressed;
        this.Method = method;
        this.methodSuppressed = null;
      }

      public CurrentState(TypeNode type) {
        this.Type = type;
        this.Method = null;
        this.typeSuppressed = null;
        this.methodSuppressed = null;
      }

      public bool IsSuppressed(string errorNumber) {
        if (this.Type != null && this.typeSuppressed == null) {
          this.typeSuppressed = GrabSuppressAttributes(this.Type);
        }
        if (this.typeSuppressed != null && this.typeSuppressed.ContainsKey(errorNumber)) return true;

        if (this.Method != null && this.methodSuppressed == null) {
          this.methodSuppressed = GrabSuppressAttributes(this.Method);
        }
        if (this.methodSuppressed != null && this.methodSuppressed.ContainsKey(errorNumber)) return true;

        return false;
      }

      private static readonly Identifier SuppressMessageIdentifier = Identifier.For("SuppressMessageAttribute");

      private static Dictionary<string, object> GrabSuppressAttributes(Method method) {
        var result = new Dictionary<string, object>();
        GrabSuppressAttributes(result, method.Attributes);
        if (method.DeclaringMember != null) {
          GrabSuppressAttributes(result, method.DeclaringMember.Attributes);
        }
        return result;
      }

      private static Dictionary<string, object> GrabSuppressAttributes(TypeNode type) {
        var result = new Dictionary<string, object>();
        while (type != null) {
          GrabSuppressAttributes(result, type.Attributes);
          type = type.DeclaringType;
        }
        return result;
      }

      private static void GrabSuppressAttributes(Dictionary<string, object> result, AttributeList attributes) {
        if (attributes != null) {
          for (int i = 0; i < attributes.Count; i++) {
            var attr = attributes[i];
            if (attr == null) continue;
            if (attr.Type == null) continue;
            if (attr.Type.Name.Matches(SuppressMessageIdentifier)) {
              if (attr.Expressions != null && attr.Expressions.Count >= 2) {
                var expr = attr.Expressions[0] as Literal;
                if (expr == null) continue;
                string category = expr.Value as string;
                if (category != "Microsoft.Contracts") continue;
                var errorCode = attr.Expressions[1] as Literal;
                if (errorCode == null) continue;
                string errorCodeLit = errorCode.Value as string;
                if (errorCodeLit.StartsWith("CC")) {
                  result.Add(errorCodeLit, errorCodeLit);
                }
              }
            }
          }
        }
      }

      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
      internal CurrentState Derive(Method method) {
        return new CurrentState(this.Type, this.typeSuppressed, method);
      }
    }

    #endregion Private fields

    /// <summary>
    /// For level, see TranslateLevel
    /// </summary>
    /// <param name="assemblyBeingRewritten"></param>
    /// <param name="rewriterNodes"></param>
    /// <param name="level"></param>
    public Rewriter(AssemblyNode assemblyBeingRewritten, RuntimeContractMethods runtimeContracts, Action<System.CodeDom.Compiler.CompilerError> handleError, bool inheritInvariantsAcrossAssemblies, bool skipQuantifiers)
    {
      Contract.Requires(handleError != null);

      // F:
      Contract.Requires(runtimeContracts != null);

      #region Find IDisposable.Dispose method
      TypeNode iDisposable = SystemTypes.IDisposable;
      if (iDisposable != null)
      {
        IDisposeMethod = iDisposable.GetMethod(Identifier.For("Dispose"));
      }
      #endregion

      this.runtimeContracts = runtimeContracts;
      this.assemblyBeingRewritten = assemblyBeingRewritten;
      this.rewriterNodes = runtimeContracts.ContractNodes;

      this.contractEmitFlags = TranslateLevel(this.runtimeContracts.RewriteLevel);

      this.contractEmitFlags |= RuntimeContractEmitFlags.InheritContracts; // default

      if (runtimeContracts.ThrowOnFailure)
      {
        this.contractEmitFlags |= RuntimeContractEmitFlags.ThrowOnFailure;
      }
      if (!runtimeContracts.UseExplicitValidation)
      {
        this.contractEmitFlags |= RuntimeContractEmitFlags.StandardMode;
      }
      this.m_handleError = handleError;
      this.InheritInvariantsAcrossAssemblies = inheritInvariantsAcrossAssemblies;
      this.skipQuantifiers = skipQuantifiers;
    }

    readonly bool InheritInvariantsAcrossAssemblies;
    readonly bool skipQuantifiers;

    /// <summary>
    /// Performs filtering of errors based on SuppressMessage attributes
    /// </summary>
    void HandleError(System.CodeDom.Compiler.CompilerError error) {
      // F:
      Contract.Requires(error != null);

      if (error.IsWarning && this.currentState.IsSuppressed(error.ErrorNumber)) return;
      this.m_handleError(error);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="assemblyBeingRewritten"></param>
    /// <param name="rewriterNodes"></param>
    /// <param name="level">
    /// level = 0       // all contracts are stripped (except Assume/Assert if they are there)
    /// level = 1       // only RequiresAlways and inherited ones
    /// level = 2       // all Requires including inherited ones
    /// level = 3       // all Pre/Post including inherited ones
    /// level = 4       // all contracts
    /// </param>
    private static RuntimeContractEmitFlags TranslateLevel(int level)
    {
      switch (level)
      {
        case 1:
          return RuntimeContractEmitFlags.LegacyRequires |
                 RuntimeContractEmitFlags.RequiresWithException;
        case 2:
          return RuntimeContractEmitFlags.LegacyRequires |
                 RuntimeContractEmitFlags.Requires |
                 RuntimeContractEmitFlags.RequiresWithException;
        case 3:
          return RuntimeContractEmitFlags.Ensures |
                 RuntimeContractEmitFlags.LegacyRequires |
                 RuntimeContractEmitFlags.Requires |
                 RuntimeContractEmitFlags.RequiresWithException;
        case 4:
          return RuntimeContractEmitFlags.Asserts |
                 RuntimeContractEmitFlags.Assumes |
                 RuntimeContractEmitFlags.Ensures |
                 RuntimeContractEmitFlags.AsyncEnsures |
                 RuntimeContractEmitFlags.Invariants |
                 RuntimeContractEmitFlags.LegacyRequires |
                 RuntimeContractEmitFlags.Requires |
                 RuntimeContractEmitFlags.RequiresWithException;
            
        default:
          return RuntimeContractEmitFlags.None;
      }
    }

    #region Public Fields and Properties

    public bool Verbose {
      get { return this.verbose; }
      set { this.verbose = value; }
    }

    #endregion Public Fields and Properties

    private bool HasRequiresContracts(Method template, out Method methodWithContract) {
      // F:
      Contract.Requires(template != null);

      while (template.Template != null) template = template.Template;
      // create self instantiation
      template = SelfInstantiation(template);

      var candidate = GetRootMethod(template);
      candidate = HelperMethods.GetContractMethod(candidate);
      methodWithContract = candidate;
      if (candidate == null) return false;
      var c = candidate.Contract;
      return c != null && c.RequiresCount > 0;
    }

    private Method SelfInstantiation(Method template)
    {
      // F:
      Contract.Requires(template != null);

      int index = MemberIndexInTypeMembers(template);

      var declaringType = template.DeclaringType;

      // F:
      Contract.Assume(declaringType != null);

      // make sure it is a template
      while (declaringType.Template != null) declaringType = declaringType.Template;

      // self instantiate declaring type
      var consolidated = declaringType.ConsolidatedTemplateParameters;
      if (consolidated != null && consolidated.Count > 0) {
        declaringType = declaringType.GetGenericTemplateInstance(this.assemblyBeingRewritten, consolidated);

        // F: GetGenericTemplateInstance has a /*!*/ annotation
        Contract.Assume(declaringType != null);
      }

      // find method again
      if (index < declaringType.Members.Count) {
        var instance = declaringType.Members[index] as Method;
        if (instance == null) {
          // we are in trouble. Need to cook up instance reference ourselves
          throw new Exception("TODO: cook up instance reference");
        }
        // now instantiate method if generic
        if (instance.TemplateParameters != null && instance.TemplateParameters.Count > 0) {
          instance = instance.GetTemplateInstance(declaringType, instance.TemplateParameters);
        }
        return instance;
      }
      else {
        // we are in trouble. Need to cook up instance reference ourselves
        throw new Exception("TODO: cook up instance reference");
      }
    }

    public override void VisitInterface(Interface Interface) {
      // No need to think about rewriting any interface methods, so
      // just return the interface without visiting down into it.
      // NB: We need to do this otherwise some interface methods get
      // a body, but I haven't been able to figure out why.
    }

    TrivialHashtable visitedClasses = new TrivialHashtable();

    public override void VisitClass(Class Class)
    {
      if (Class == null) return;
      if (visitedClasses[Class.UniqueKey] != null) return;
      visitedClasses[Class.UniqueKey] = Class;

      VisitBaseClass(Class);

      base.VisitClass(Class);
    }


    private void VisitBaseClass(Class Class)
    {
      // F:
      Contract.Requires(Class != null);

      // make sure the possibly generic base class is rewritten before this class
      var baseClass = Class.BaseClass;
      if (baseClass == null) return;

      while (baseClass.Template is Class)
      {
        baseClass = (Class)baseClass.Template;
      }

      // make sure base class is in same assembly
      if (Class.DeclaringModule != baseClass.DeclaringModule) return;

      VisitClass(baseClass);
    }

    public override void VisitTypeNode(TypeNode typeNode) {
      if (typeNode == null) return;

      if (HelperMethods.IsContractTypeForSomeOtherType(typeNode, this.rewriterNodes) != null) {
        return;
      }
      Method savedInvariantMethod = this.InvariantMethod;
      Field savedReentrancyFlag = this.ReentrancyFlag;
      this.InvariantMethod = null;
      this.ReentrancyFlag = null;
      var savedState = this.currentState;
      this.currentState = new CurrentState(typeNode);
      var savedEmitFlags = this.AdaptRuntimeOptionsBasedOnAttributes(typeNode.Attributes);

      try
      {
        if (this.Emit(RuntimeContractEmitFlags.Invariants) && rewriterNodes.InvariantMethod != null)
        {
          InvariantList userWrittenInvariants = typeNode.Contract == null ? null : typeNode.Contract.Invariants;
          Class asClass = typeNode as Class;
          Field baseReentrancyFlag;
          Method baseInvariantMethod = FindAndInstantiateBaseClassInvariantMethod(asClass, out baseReentrancyFlag);


          if ((userWrittenInvariants != null && 0 < userWrittenInvariants.Count) || baseInvariantMethod != null)
          {
            Field reEntrancyFlag = null;
            var isStructWithExplicitLayout = IsStructWithExplicitLayout(typeNode as Struct);
            if (isStructWithExplicitLayout)
            {
              this.HandleError(new Warning(1044, String.Format("Struct '{0}' has explicit layout and an invariant. Invariant recursion guards will not be emitted and evaluation of invariants may occur too eagerly.", typeNode.FullName), new SourceContext()));
            }
            else
            {
              #region Find or create re-entrancy flag to the class
              if (baseReentrancyFlag != null)
              {
                // grab base reEntrancyFlag
                reEntrancyFlag = baseReentrancyFlag;
              }
              else
              {
                FieldFlags reentrancyFlagProtection;
                if (typeNode.IsSealed)
                {
                  reentrancyFlagProtection = FieldFlags.Private | FieldFlags.CompilerControlled;
                }
                else if (this.InheritInvariantsAcrossAssemblies)
                {
                  reentrancyFlagProtection = FieldFlags.Family | FieldFlags.CompilerControlled;
                }
                else
                {
                  reentrancyFlagProtection = FieldFlags.FamANDAssem | FieldFlags.CompilerControlled;
                }
                reEntrancyFlag = new Field(typeNode, null, reentrancyFlagProtection, Identifier.For("$evaluatingInvariant$"), SystemTypes.Boolean, null);
                RewriteHelper.TryAddCompilerGeneratedAttribute(reEntrancyFlag);
                RewriteHelper.TryAddDebuggerBrowsableNeverAttribute(reEntrancyFlag);
                typeNode.Members.Add(reEntrancyFlag);
              }
              #endregion Add re-entrancy flag to the class
            }
            Block newBody = new Block(new StatementList(3));
            // newBody ::=
            //   if (this.$evaluatingInvariant$){
            //     return (true); // don't really return true since this is a void method, but it means the invariant is assumed to hold
            //   this.$evaluatingInvariant$ := true;
            //   try{
            //     <evaluate invariants and call base invariant method>
            //   } finally {
            //     this.$evaluatingInvariant$ := false;
            //   }
            Method invariantMethod =
              new Method(
                typeNode,
                new AttributeList(),
                Identifier.For("$InvariantMethod$"),
                null,
                SystemTypes.Void,
                newBody);
            RewriteHelper.TryAddCompilerGeneratedAttribute(invariantMethod);
            invariantMethod.CallingConvention = CallingConventionFlags.HasThis;
            if (this.InheritInvariantsAcrossAssemblies)
              invariantMethod.Flags = MethodFlags.Family | MethodFlags.Virtual;
            else
              invariantMethod.Flags = MethodFlags.FamANDAssem | MethodFlags.Virtual;
            invariantMethod.Attributes.Add(new AttributeNode(new MemberBinding(null, this.runtimeContracts.ContractNodes.InvariantMethodAttribute.GetConstructor()), null));

            #region call base class invariant
            if (baseInvariantMethod != null)
            {
              newBody.Statements.Add(
                new ExpressionStatement(
                new MethodCall(
                new MemberBinding(invariantMethod.ThisParameter, baseInvariantMethod), null, NodeType.Call, SystemTypes.Void)));
            }
            #endregion
            #region Add re-entrancy test to the method
            Block invariantExit = new Block();
            if (reEntrancyFlag != null)
            {
              Block reEntrancyTest = new Block(new StatementList());
              reEntrancyTest.Statements.Add(
                new Branch(new MemberBinding(invariantMethod.ThisParameter, reEntrancyFlag), invariantExit));
              reEntrancyTest.Statements.Add(
                new AssignmentStatement(new MemberBinding(invariantMethod.ThisParameter, reEntrancyFlag), Literal.True)
                );
              newBody.Statements.Add(reEntrancyTest);
            }
            #endregion Add re-entrancy test to the method

            Block invariantChecks = new Block(new StatementList());
            if (userWrittenInvariants != null)
            {
              #region Filter out invariants that aren't runtime checkable
              var filteredInvariants = new InvariantList();
              foreach (var i in userWrittenInvariants) {
                if (!EmitInvariant(i, this.skipQuantifiers)) continue;
                filteredInvariants.Add(i);
              }
              #endregion Filter out invariants that aren't runtime checkable
              #region Duplicate the invariants
              // need to duplicate the invariants so they aren't shared
              Duplicator d = new Duplicator(typeNode.DeclaringModule, typeNode);
              InvariantList duplicatedInvariants = d.VisitInvariantList(filteredInvariants);
              // F: seems to have the invariant duplictedInvariants != null
              Contract.Assume(duplicatedInvariants != null);

              #endregion Duplicate the invariants
              #region Rewrite the body of the invariant method
              // then we need to replace calls to Contract.Invariant with calls to Contract.RewriterInvariant
              // in the body of the invariant method
              RewriteInvariant rc = new RewriteInvariant(this.runtimeContracts);
              rc.VisitInvariantList(duplicatedInvariants);
              #endregion Rewrite the body of the invariant method
              for (int i = 0, n = duplicatedInvariants.Count; i < n; i++)
              {
                Expression e = duplicatedInvariants[i].Condition;
                var blockExpr = e as BlockExpression;
                if (blockExpr != null)
                {
                  invariantChecks.Statements.Add(blockExpr.Block);
                }
                else
                {
                  invariantChecks.Statements.Add(new ExpressionStatement(e));
                }
              }
            }
            Block finallyB = new Block(new StatementList(1));
            if (reEntrancyFlag != null)
            {
              finallyB.Statements.Add(
                new AssignmentStatement(new MemberBinding(invariantMethod.ThisParameter, reEntrancyFlag), Literal.False)
                );
            }
            this.cleanUpCodeCoverage.VisitBlock(invariantChecks);
            Block b = RewriteHelper.CreateTryFinallyBlock(invariantMethod, invariantChecks, finallyB);
            newBody.Statements.Add(b);
            newBody.Statements.Add(invariantExit);
            newBody.Statements.Add(new Return());

            // set the field "this.InvariantMethod" to this one so it is used in calls within each method
            // but don't add it yet to the class so it doesn't get visited!
            this.InvariantMethod = invariantMethod;
            this.ReentrancyFlag = reEntrancyFlag;
          }
        }

        base.VisitTypeNode(typeNode);
        // Now add invariant method to the class
        if (this.InvariantMethod != null)
        {
          typeNode.Members.Add(this.InvariantMethod);
        }
        return;
      }
      finally
      {
        this.InvariantMethod = savedInvariantMethod;
        this.ReentrancyFlag = savedReentrancyFlag;
        this.currentState = savedState;
        this.contractEmitFlags = savedEmitFlags;
      }
    }

    private static bool IsStructWithExplicitLayout(Struct asStruct)
    {
      if (asStruct == null) return false;
      if ((asStruct.Flags & TypeFlags.ExplicitLayout) != 0) return true;
      return false;
    }


    private Method FindAndInstantiateBaseClassInvariantMethod(Class asClass, out Field baseReentrancyFlag)
    {
      baseReentrancyFlag = null;
      if (asClass == null || asClass.BaseClass == null) return null;
      if (!this.Emit(RuntimeContractEmitFlags.InheritContracts)) return null; // don't call base class invariant if we don't inherit

      var baseClass = asClass.BaseClass;

      if (!this.InheritInvariantsAcrossAssemblies && (baseClass.DeclaringModule != asClass.DeclaringModule))
        return null;

      var result = baseClass.GetMethod(Identifier.For("$InvariantMethod$"), null);

      if (result != null && !HelperMethods.IsVisibleFrom(result, asClass)) return null;

      if (result == null && baseClass.Template != null)
      {
        // instantiation of generated method has not happened.
        var generic = baseClass.Template.GetMethod(Identifier.For("$InvariantMethod$"), null);
        if (generic != null)
        {
          if (!HelperMethods.IsVisibleFrom(generic, asClass)) return null;
          // generate proper reference.
          result = GetMethodInstanceReference(generic, baseClass);
        }
      }
      // extract base reentrancy flag
      if (result != null) {
        var instantiatedParent = result.DeclaringType;
        baseReentrancyFlag = instantiatedParent.GetField(Identifier.For("$evaluatingInvariant$"));

        if (baseReentrancyFlag == null && baseClass.Template != null)
        {
          // instantiation of generated baseReentrancy flag has not happened.
          var generic = baseClass.Template.GetField(Identifier.For("$evaluatingInvariant$"));
          if (generic != null)
          {
            if (HelperMethods.IsVisibleFrom(generic, asClass))
            {
              baseReentrancyFlag = GetFieldInstanceReference(generic, baseClass);
            }
          }
        }
      }

      return result;
    }

#if false
    bool IsAccessibleFrom(Member reference, Method from)
    {
      if (reference == null) return true;
      if (!IsAccessibleFrom(reference.DeclaringType, from)) return false;

      Method method = reference as Method;
      if (method != null)
      {
        return IsAccessibleFrom(method, from);
      }
      Field field = reference as Field;
      if (field != null)
      {
        return IsAccessibleFrom(field, from);
      }
      TypeNode type = reference as TypeNode;
      if (type != null)
      {
        return IsAccessibleFrom(type, from);
      }
      return true;
    }

    bool IsAccessibleFrom(Method method, Method from)
    {
      if (method == null) return true;
      if (!IsAccessibleFrom(method.Template, from)) return false;
      if (method.TemplateArguments != null)
      {
        for (int i = 0; i < method.TemplateArguments.Count; i++)
        {
          if (!IsAccessibleFrom(method.TemplateArguments[i], from)) return false;
        }
      }
    }

    bool IsAccessibleFrom(Field field, Method from)
    {
    }

    bool IsAccessibleFrom(TypeNode type, Method from)
    {
      if (type == null) return true;
      if (type.DeclaringType != null)
      {
        if (!IsAccessibleFrom(type.DeclaringType, from)) return false;
      }
      else
      {
        if (type.DeclaringModule != from.DeclaringType.DeclaringModule)
        {
          if (!type.IsPublic) return false;
        }
      }
    }
#endif

    private bool Emit(RuntimeContractEmitFlags want)
    {
      return this.contractEmitFlags.Emit(want);
    }


    /// <summary>
    /// Helper method for CCI decoding
    /// </summary>
    internal static Method ExtractCallFromStatement(Statement s)
    {
      MethodCall dummy;
      return ExtractCallFromStatement(s, out dummy);
    }
    internal static Method ExtractCallFromStatement(Statement s, out MethodCall call)
    {
      call = null;
      ExpressionStatement es = s as ExpressionStatement;
      if (es == null) return null;
      call = es.Expression as MethodCall;
      if (call == null || call.Callee == null) { return null; }
      MemberBinding mb = call.Callee as MemberBinding;
      if (mb == null) { return null; }
      Method m = mb.BoundMember as Method;
      if (m == null) { return null; }
      return m;
    }


    static bool IsRewriterGenerated(Method method)
    {
      // F:
      Contract.Requires(method != null);
      Contract.Requires(method.Name != null);
      Contract.Requires(method.Name.Name != null);

      // check if it is a wrapper method
      if (method.Name.Name.StartsWith("V$") || method.Name.Name.StartsWith("NV$")) return true;
      return false;
    }

    // Requires:
    //  method.Body != null ==>
    //    ForAll{Statement s in method.Body.Statements;
    //           "BasicBlock"(s)};
    public override void VisitMethod(Method method)
    {
      if (method == null) return;
      var savedEmitFlags = AdaptRuntimeOptionsBasedOnAttributes(method.Attributes);
      try
      {
        VisitMethodInternal(method);
      }
      finally
      {
        this.contractEmitFlags = savedEmitFlags;
      }
    }

    public override void VisitMemberList(MemberList members)
    {
      if (members == null) return;
      for (int i = 0, n = members.Count; i < n; i++)
      {
        var member = members[i];
        var method = member as Method;
        if (method != null && this.runtimeContracts.ContractNodes.IsObjectInvariantMethod(method))
        {
        // remove method from rewritten binaries
          members[i] = null;
          continue;
        }
        this.Visit(member);
      }
      }

    //[ContractVerification(true)]
    private void VisitMethodInternal(Method method) {
      if (method.IsAbstract || IsRewriterGenerated(method)) {
        return;
      }
      Block origBody = method.Body;
      if (origBody == null || origBody.Statements == null || origBody.Statements.Count == 0) return;

#if DEBUG
      if (!RewriteHelper.PostNormalizedFormat(method))
        throw new RewriteException("The method body for '" + method.FullName + "' is not structured correctly");
#endif


      #region Rewrite all Assert and Assume methods
      // Happens in-place so any modifications made are persisted even if nothing else is done to method

      RewriteAssertAssumeAndCallSiteRequires raa = new RewriteAssertAssumeAndCallSiteRequires(this.AssertAssumeRewriteMethodTable, this.runtimeContracts, this.contractEmitFlags, this, method);
      method.Body = raa.VisitBlock(method.Body);

      #endregion Rewrite all Assert and Assume methods (if user specified)

      #region Bail out early if publicSurfaceOnly and method is not visible (but not for contract validators!) or contract abbreviator
      if (this.runtimeContracts.PublicSurfaceOnly && !IsCallableFromOutsideAssembly(method) && !ContractNodes.IsValidatorMethod(method)
          || ContractNodes.IsAbbreviatorMethod(method)) return;
      #endregion

      int oldLocalUniqueNameCounter = 0;
      List<Block> contractInitializationBlocks = new List<Block>();
      Block postPreambleBlock = null;
      Dictionary<TypeNode, Local> closureLocals = new Dictionary<TypeNode, Local>();
      Block oldExpressionPreStateValues = new Block(new StatementList());
      #region Gather pre- and postconditions from supertypes and from method contract
      // Create lists of unique preconditions and postconditions.
      List<Requires> preconditions = new List<Requires>();
      List<Ensures> postconditions = new List<Ensures>();
      List<Ensures> asyncPostconditions = new List<Ensures>();
      RequiresList validations = null;
      // For postconditions, wrap all parameters within an old expression (if they are not already within one)
      var wrap = new WrapParametersInOldExpressions();
      EmitAsyncClosure asyncBuilder = null;

      #region Copy the method's contracts.
      if (method.Contract != null && (method.Contract.RequiresCount > 0 || method.Contract.EnsuresCount > 0 || 
                                      method.Contract.ValidationsCount > 0 || method.Contract.AsyncEnsuresCount > 0))
      {
        // Use duplicate of contract because it is going to get modified from processing Old(...) and
        // Result(). Can't have the modified contract get inherited (or else the Old(...) processing
        // will not work properly.)
        // Can't use MethodContract.CopyFrom or HelperMethods.DuplicateMethodBodyAndContract
        // because they assume the new contract is in a different method. Plus the latter duplicates
        // any closure classes needed for anonymous delegates in the contract.
        MethodContract mc = HelperMethods.DuplicateContractAndClosureParts(method, method, this.runtimeContracts.ContractNodes, true);
        validations = mc.Validations;


        if (mc != null) {
          contractInitializationBlocks.Add(mc.ContractInitializer);
          postPreambleBlock = mc.PostPreamble;

          RecordEnsures(method, ref oldLocalUniqueNameCounter, contractInitializationBlocks, closureLocals, oldExpressionPreStateValues, postconditions, asyncPostconditions, wrap, ref asyncBuilder, mc);

        }
      }
      #endregion Copy the method's contracts.
      #region Search the class hierarchy for overridden methods to propagate their contracts.
      if (this.Emit(RuntimeContractEmitFlags.InheritContracts) && method.OverridesBaseClassMember && method.DeclaringType != null && method.OverriddenMethod != null) {
        for (TypeNode super = method.DeclaringType.BaseType; super != null; super = HelperMethods.DoesInheritContracts(super)?super.BaseType:null) {
          var baseMethod = super.GetImplementingMethod(method.OverriddenMethod, false);
          Method baseContractMethod = HelperMethods.GetContractMethod(baseMethod);
          if (baseContractMethod != null) {
            MethodContract mc = HelperMethods.DuplicateContractAndClosureParts(method, baseContractMethod, this.runtimeContracts.ContractNodes, false);
            RewriteHelper.ReplacePrivateFieldsThatHavePublicProperties(method.DeclaringType, super, mc, this.rewriterNodes);
            if (mc != null) {
              contractInitializationBlocks.Add(mc.ContractInitializer);
              // can't have post preambles in overridden methods, since they cannot be constructors.

              // only add requires if baseMethod is the root method
              if (mc.Requires != null && baseMethod.OverriddenMethod == null)
              {
                foreach (RequiresPlain requires in mc.Requires) {
                  if (!EmitRequires(requires, this.skipQuantifiers)) continue;
                  // Debug.Assert(!preconditions.Contains(requires));
                  preconditions.Add(requires);
                }
              }
              RecordEnsures(method, ref oldLocalUniqueNameCounter, contractInitializationBlocks, closureLocals, oldExpressionPreStateValues, postconditions, asyncPostconditions, wrap, ref asyncBuilder, mc);
            }
          }

          // bail out if this base type does not inherit contracts
          if (!HelperMethods.DoesInheritContracts(super)) break;
        }
      }
      #endregion Search the class hierarchy for overridden methods to propagate their contracts.
      #region Propagate explicit interface method contracts.
      if (this.Emit(RuntimeContractEmitFlags.InheritContracts) && method.ImplementedInterfaceMethods != null) {
        foreach (Method interfaceMethod in method.ImplementedInterfaceMethods) {
          if (interfaceMethod != null)
          {
            Method contractMethod = HelperMethods.GetContractMethod(interfaceMethod);

            if (contractMethod != null)
            { // if null, then no contract for this interface method

              // Maybe it would be easier to just duplicate the entire method and then pull the
              // initialization code from the duplicate?
              MethodContract mc=HelperMethods.DuplicateContractAndClosureParts(method, contractMethod, this.runtimeContracts.ContractNodes, false);
              if (mc != null)
              {
                contractInitializationBlocks.Add(mc.ContractInitializer);
                // can't have post preambles in interface methods (not constructors)
                if (mc.Requires != null)
                {
                  foreach (RequiresPlain requires in mc.Requires)
                  {
                    if (!EmitRequires(requires, this.skipQuantifiers)) continue;
                    // Debug.Assert(!preconditions.Contains(requires));
                    preconditions.Add(requires);
                  }
                }
                RecordEnsures(method, ref oldLocalUniqueNameCounter, contractInitializationBlocks, closureLocals, oldExpressionPreStateValues, postconditions, asyncPostconditions, wrap, ref asyncBuilder, mc);
              }
            }
          }
        }
      }
      #endregion Propagate explicit interface method contracts.
      #region Propagate implicit interface method contracts.
      if (this.Emit(RuntimeContractEmitFlags.InheritContracts) && method.ImplicitlyImplementedInterfaceMethods != null) {
        foreach (Method interfaceMethod in method.ImplicitlyImplementedInterfaceMethods) {
          if (interfaceMethod != null) {
            Method contractMethod = HelperMethods.GetContractMethod(interfaceMethod);
            if (contractMethod != null)
            { // if null, then no contract for this method


              // Maybe it would be easier to just duplicate the entire method and then pull the
              // initialization code from the duplicate?
              MethodContract mc = HelperMethods.DuplicateContractAndClosureParts(method, contractMethod, this.runtimeContracts.ContractNodes, false);
              if (mc != null)
              {
                contractInitializationBlocks.Add(mc.ContractInitializer);
                // can't have post preambles in implicit interface method implementations, as they are not constructors.
                if (mc.Requires != null)
                {
                  foreach (RequiresPlain requires in mc.Requires)
                  {
                    if (!EmitRequires(requires, this.skipQuantifiers)) continue;
                    // Debug.Assert(!preconditions.Contains(requires));
                    preconditions.Add(requires);
                  }
                }
                RecordEnsures(method, ref oldLocalUniqueNameCounter, contractInitializationBlocks, closureLocals, oldExpressionPreStateValues, postconditions, asyncPostconditions, wrap, ref asyncBuilder, mc);
              }
            }
          }
        }
      }
      #endregion Propagate implicit interface method contracts.
      #endregion Gather pre- and postconditions from supertypes and from method contract


      #region Return if there is nothing to do
      if (preconditions.Count < 1 && postconditions.Count < 1 && asyncPostconditions.Count < 1 && (validations == null || validations.Count == 0) && this.InvariantMethod == null)
        return;
      #endregion Return if there is nothing to do

      #region Change all short branches to long because code modifications can add code
      {
        // REVIEW: This does *not* remove any short branches in the contracts.
        // I think that is okay, but we should think about it.
        RemoveShortBranches rsb = new RemoveShortBranches();
        rsb.VisitBlock(method.Body);
      }
      #endregion Change all short branches to long because code modifications can add code
      #region Modify method body to change all returns into assignments and branch to unified exit
      // now modify the method body to change all return statements
      // into assignments to the local "result" and a branch to a
      // block at the end that can check the post-condition using
      // "result" and then finally return it.
      // [MAF] we do it later once we know if the branches are leaves or just branch.
      Local result = null;
      if (!HelperMethods.IsVoidType(method.ReturnType)) {
        // don't write huge names. The debugger chokes on them and shows no locals at all.
        //   result = new Local(Identifier.For("Contract.Result<" + typeName + ">()"), method.ReturnType);
        result = new Local(Identifier.For("Contract.Result()"), method.ReturnType);
        if (method.LocalList == null) {
          method.LocalList = new LocalList();
        }
        method.LocalList.Add(result);
      }
      Block newExit = new Block(new StatementList());
      #endregion Modify method body to change all returns into assignments and branch to unified exit

      #region Create the new method's body
      Block newBody = new Block(new StatementList());
      newBody.HasLocals = true;

      #endregion Create the new method's body
      #region If there had been any closure initialization code, put it first in the new body
      if (0 < contractInitializationBlocks.Count) {
        foreach (Block b in contractInitializationBlocks) {
          newBody.Statements.Add(b);
        }
      }
      #endregion

      EmitInterleavedValidationsAndRequires(method, preconditions, validations, newBody);

      #region Turn off invariant checking until the end
      Local oldReEntrancyFlagLocal = null;
      if (MethodShouldHaveInvariantChecked(method) && this.ReentrancyFlag != null && BodyHasCalls(method.Body))
      {
        oldReEntrancyFlagLocal = new Local(SystemTypes.Boolean);
        newBody.Statements.Add(new Block(new StatementList(
          new AssignmentStatement(oldReEntrancyFlagLocal, new MemberBinding(method.ThisParameter, this.ReentrancyFlag)),
          new AssignmentStatement(new MemberBinding(method.ThisParameter, this.ReentrancyFlag), Literal.True))));
      }
      #endregion

      #region Put all of the collected initializations from "old" expressions into the method
      if (oldExpressionPreStateValues.Statements.Count > 0) {
        newBody.Statements.Add(oldExpressionPreStateValues);
      }
      #endregion

      // if there are preamble blocks we need to move them from the origBody in case we will wrap a try-catch
      var preambleIndex = 0;
      while (origBody.Statements.Count > preambleIndex && origBody.Statements[preambleIndex] is PreambleBlock) {
        newBody.Statements.Add(origBody.Statements[preambleIndex]);
        origBody.Statements[preambleIndex] = null;
        preambleIndex++;
      }
      Block newBodyBlock = new Block(new StatementList());
      newBody.Statements.Add(newBodyBlock); // placeholder for eventual body
      newBody.Statements.Add(newExit);

      #region Replace "result" in postconditions (both for method return and out parameters)
      if (result != null)
      {
        foreach (Ensures e in postconditions)
        {
          if (e == null) continue;
          ReplaceResult repResult = new ReplaceResult(method, result, this.assemblyBeingRewritten);
          repResult.Visit(e);
          // now need to initialize closure result fields
          foreach (var target in repResult.NecessaryResultInitialization(closureLocals))
          {
            newBody.Statements.Add(new AssignmentStatement(target, result));
          }
        }
      }
      #endregion

      #region Emit potential post preamble block (from contract duplicate) in constructors
      if (postPreambleBlock != null)
      {
        newBody.Statements.Add(postPreambleBlock);
      }
      #endregion

      #region Emit normal postconditions

      SourceContext lastEnsuresSourceContext = default(SourceContext);
      bool hasLastEnsuresContext = false;
      bool containsExceptionalPostconditions = false;
      var ensuresChecks = new StatementList();
      foreach (Ensures e in postconditions) {
        // Exceptional postconditions are handled separately.
        if (e is EnsuresExceptional) {
          containsExceptionalPostconditions = true;
          continue;
        }

        lastEnsuresSourceContext = e.SourceContext;
        hasLastEnsuresContext = true;
        // call Contract.RewriterEnsures
        Method ensMethod = this.runtimeContracts.EnsuresMethod;
        ExpressionList args = new ExpressionList();
        args.Add(e.PostCondition);
        if (e.UserMessage != null)
          args.Add(e.UserMessage);
        else
          args.Add(Literal.Null);
        if (e.SourceConditionText != null) {
          args.Add(e.SourceConditionText);
        } else {
          args.Add(Literal.Null);
        }
        ensuresChecks.Add(new ExpressionStatement(
          new MethodCall(new MemberBinding(null, ensMethod),
          args, NodeType.Call, SystemTypes.Void), e.SourceContext));
      }
      this.cleanUpCodeCoverage.VisitStatementList(ensuresChecks);
      EmitRecursionGuardAroundChecks(method, newBody, ensuresChecks);
      #endregion Normal postconditions

      #region Emit object invariant
      if (MethodShouldHaveInvariantChecked(method)) {
        // Now turn checking on by restoring old reentrancy flag
        if (this.ReentrancyFlag != null && oldReEntrancyFlagLocal != null) 
        {
          newBody.Statements.Add(new AssignmentStatement(new MemberBinding(method.ThisParameter, this.ReentrancyFlag), oldReEntrancyFlagLocal));
        }
        var callType = (method is InstanceInitializer) || this.InvariantMethod.DeclaringType.IsValueType ? NodeType.Call : NodeType.Callvirt;

        // just add a call to the already existing invariant method, "this.InvariantMethod();"
        // all of the processing needed is done as part of VisitClass
        newBody.Statements.Add(
          new ExpressionStatement(
          new MethodCall(
          new MemberBinding(method.ThisParameter, this.InvariantMethod), null, callType, SystemTypes.Void)));
      }
      #endregion Object invariant

      #region Emit exceptional postconditions

      ReplaceReturns rr;
      if (containsExceptionalPostconditions)
      {
        // -- The following code comes from System.Compiler.Normalizer.CreateTryCatchBlock --

        // The tryCatch holds the try block, the catch blocks, and an empty block that is the
        // target of an unconditional branch for normal execution to go from the try block
        // around the catch blocks.
        if (method.ExceptionHandlers == null) method.ExceptionHandlers = new ExceptionHandlerList();

        Block afterCatches = new Block(new StatementList());

        Block tryCatch = newBodyBlock;
        Block tryBlock = new Block(new StatementList());
        tryBlock.Statements.Add(origBody);
        rr = new ReplaceReturns(result, newExit, leaveExceptionBody:true);
        rr.Visit(origBody);

        tryBlock.Statements.Add(new Branch(null, afterCatches, false, true, true));
        // the EH needs to have a pointer to this block so the writer can
        // calculate the length of the try block. So it should be the *last*
        // thing in the try body.
        Block blockAfterTryBody = new Block(null);
        tryBlock.Statements.Add(blockAfterTryBody);
        tryCatch.Statements.Add(tryBlock);
        for (int i = 0, n = postconditions.Count; i < n; i++)
        {
          // Normal postconditions are handled separately.
          EnsuresExceptional e = postconditions[i] as EnsuresExceptional;
          if (e == null)
            continue;

          // The catchBlock contains the catchBody, and then
          // an empty block that is used in the EH.
          Block catchBlock = new Block(new StatementList());
          Local l = new Local(e.Type);
          Throw rethrow = new Throw();
          rethrow.NodeType = NodeType.Rethrow;

          // call Contract.RewriterEnsures
          ExpressionList args = new ExpressionList();
          args.Add(e.PostCondition);
          if (e.UserMessage != null)
            args.Add(e.UserMessage);
          else
            args.Add(Literal.Null);
          if (e.SourceConditionText != null)
          {
            args.Add(e.SourceConditionText);
          }
          else
          {
            args.Add(Literal.Null);
          }
          args.Add(l);
          var checks = new StatementList();
          checks.Add(new ExpressionStatement(
            new MethodCall(new MemberBinding(null, this.runtimeContracts.EnsuresOnThrowMethod),
            args, NodeType.Call, SystemTypes.Void), e.SourceContext));

          catchBlock.Statements.Add(new AssignmentStatement(l, new Expression(NodeType.Pop), e.SourceContext));
          this.cleanUpCodeCoverage.VisitStatementList(checks);
          EmitRecursionGuardAroundChecks(method, catchBlock, checks);

          #region Emit object invariant on EnsuresOnThrow check
          if (MethodShouldHaveInvariantChecked(method, inExceptionCase: true))
          {
            // Now turn checking on by restoring old reentrancy flag
            if (this.ReentrancyFlag != null && oldReEntrancyFlagLocal != null)
            {
              catchBlock.Statements.Add(new AssignmentStatement(new MemberBinding(method.ThisParameter, this.ReentrancyFlag), oldReEntrancyFlagLocal));
            }
            // just add a call to the already existing invariant method, "this.InvariantMethod();"
            // all of the processing needed is done as part of VisitClass
            catchBlock.Statements.Add(
              new ExpressionStatement(
              new MethodCall(
              new MemberBinding(method.ThisParameter, this.InvariantMethod), null, NodeType.Call, SystemTypes.Void)));
          }
          #endregion Object invariant

          catchBlock.Statements.Add(rethrow);
          // The last thing in each catch block is an empty block that is the target of
          // BlockAfterHandlerEnd in each exception handler.
          // It is used in the writer to determine the length of each catch block
          // so it should be the last thing added to each catch block.
          Block blockAfterHandlerEnd = new Block(new StatementList());
          catchBlock.Statements.Add(blockAfterHandlerEnd);
          tryCatch.Statements.Add(catchBlock);

          // add information to the ExceptionHandlers of this method
          ExceptionHandler exHandler = new ExceptionHandler();
          exHandler.TryStartBlock = origBody;
          exHandler.BlockAfterTryEnd = blockAfterTryBody;
          exHandler.HandlerStartBlock = catchBlock;
          exHandler.BlockAfterHandlerEnd = blockAfterHandlerEnd;
          exHandler.FilterType = l.Type;
          exHandler.HandlerType = NodeType.Catch;
          method.ExceptionHandlers.Add(exHandler);
        }
        tryCatch.Statements.Add(afterCatches);
      }
      else // no exceptional post conditions
      {
        newBodyBlock.Statements.Add(origBody);
        rr = new ReplaceReturns(result, newExit, leaveExceptionBody: false);
        rr.Visit(origBody);
      }
      #endregion Exceptional and finally postconditions

      #region Create a block for the return statement and insert it
      // this is the block that contains the return statements
      // it is (supposed to be) the single exit from the method
      // that way, anything that must always be done can be done
      // in this block
      Block returnBlock = new Block(new StatementList(1));

      if (asyncPostconditions != null && asyncPostconditions.Count > 0)
      {
          asyncBuilder.AddAsyncPost(asyncPostconditions);
          var funcLocal = new Local(asyncBuilder.FuncCtor.DeclaringType);
          var ldftn = new UnaryExpression(new MemberBinding(null, asyncBuilder.CheckMethod), NodeType.Ldftn, CoreSystemTypes.IntPtr);
          returnBlock.Statements.Add(new AssignmentStatement(funcLocal, new Construct(new MemberBinding(null, asyncBuilder.FuncCtor), new ExpressionList(asyncBuilder.ClosureLocal, ldftn))));
          returnBlock.Statements.Add(new AssignmentStatement(result, new MethodCall(new MemberBinding(result, asyncBuilder.ContinueWithMethod), new ExpressionList(funcLocal))));
      }
      Statement returnStatement;
      if (!HelperMethods.IsVoidType(method.ReturnType)) {
        returnStatement = new Return(result);
      }
      else
      {
        returnStatement = new Return();
      }
      if (hasLastEnsuresContext)
      {
        returnStatement.SourceContext = lastEnsuresSourceContext;
      }
      else
      {
        returnStatement.SourceContext = rr.LastReturnSourceContext;
      }
      returnBlock.Statements.Add(returnStatement);
      newBody.Statements.Add(returnBlock);
      #endregion

      method.Body = newBody;

      #region Make sure InitLocals is marked for this method
      // 15 April 2003
      // Since each method has locals added to it, need to make sure this flag is
      // on. Otherwise, the generated code cannot pass peverify.
      if (!method.InitLocals) {
        method.InitLocals = true;
        //WriteToLog("Setting InitLocals for method: {0}", method.FullName);
      }
      #endregion
    }

    private void RecordEnsures(
        Method method, 
        ref int oldLocalUniqueNameCounter, 
        List<Block> contractInitializationBlocks, 
        Dictionary<TypeNode, Local> closureLocals, 
        Block oldExpressionPreStateValues, 
        List<Ensures> postconditions, 
        List<Ensures> asyncPostconditions, 
        WrapParametersInOldExpressions wrap, 
        ref EmitAsyncClosure asyncBuilder,
        MethodContract mc)
    {
        RewriteHelper.RecordClosureInitialization(method, mc.ContractInitializer, closureLocals);
        if (this.Emit(RuntimeContractEmitFlags.Ensures) && mc.Ensures != null)
        {
            mc.Ensures = wrap.VisitEnsuresList(mc.Ensures);
            Block oldInits = ProcessOldExpressions(method, mc.Ensures, closureLocals, ref oldLocalUniqueNameCounter);
            if (oldInits != null)
            {
                oldExpressionPreStateValues.Statements.Add(oldInits);
            }
            if (mc.Ensures != null)
            {
                foreach (Ensures ensures in mc.Ensures)
                {
                    if (!EmitEnsures(ensures, method.DeclaringType, this.skipQuantifiers)) continue;
                    postconditions.Add(ensures);
                }
            }
        }
        if (this.Emit(RuntimeContractEmitFlags.AsyncEnsures) && mc.AsyncEnsuresCount > 0)
        {
            mc.AsyncEnsures = wrap.VisitEnsuresList(mc.AsyncEnsures);
            var found = false;
            foreach (Ensures postcondition in mc.AsyncEnsures)
            {
                if (!EmitEnsures(postcondition, method.DeclaringType, this.skipQuantifiers) || asyncPostconditions.Contains(postcondition)) continue;

                EnsureAsyncBuilder(method, contractInitializationBlocks, closureLocals, ref asyncBuilder);
                found = true;
                asyncPostconditions.Add(postcondition);
            }
            if (found)
            {
                Block oldInit = ProcessOldExpressionsInAsync(method, mc.AsyncEnsures, closureLocals, ref oldLocalUniqueNameCounter, asyncBuilder.ClosureClass);
                if (oldInit != null && oldInit.Statements != null && oldInit.Statements.Count > 0)
                {
                    oldExpressionPreStateValues.Statements.Add(oldInit);
                }
            }
        }
    }

    private void EnsureAsyncBuilder(Method method, List<Block> contractInitializationBlocks, Dictionary<TypeNode, Local> closureLocals, ref EmitAsyncClosure asyncBuilder)
    {
        if (asyncBuilder == null)
        {
            // create a wrapper
            asyncBuilder = new EmitAsyncClosure(method, this);
            contractInitializationBlocks.Add(asyncBuilder.ClosureInitializer);
            closureLocals.Add(asyncBuilder.ClosureClass, asyncBuilder.ClosureLocal);
        }
    }

    private class EmitAsyncClosure : StandardVisitor
    {
        Method fromMethod;
        TypeNode declaringType;
        Class closureClass;
        Class closureClassInstance;
        Specializer forwarder;
        Duplicator dup;
        InstanceInitializer constructor;
        Method checkMethod;
        Method checkExceptionMethod;
        Local originalResultLocal;
        Local newResultLocal;
        StatementList checkBody;
        Rewriter parent;
        InstanceInitializer funcCtor;
        private Identifier checkMethodId;
        private Identifier checkExceptionMethodId;
        private Method continuewithMethod;

        /// <summary>
        /// Instance used in calling method context
        /// </summary>
        public Class ClosureClass { get { return this.closureClassInstance; } }
        public Class ClosureClassGeneric { get { return this.closureClass; } }
        public InstanceInitializer Ctor { get { return (InstanceInitializer)this.closureClassInstance.GetMembersNamed(StandardIds.Ctor)[0]; } }
        public Method CheckMethod { get { return (Method)this.closureClassInstance.GetMembersNamed(this.checkMethodId)[0]; } }
        public InstanceInitializer FuncCtor { get { return this.funcCtor; } }
        public Member ContinueWithMethod { get { return this.continuewithMethod; } }

        public Local ClosureLocal { get; private set; }
        public Block ClosureInitializer { get; private set; }

        Cache<TypeNode> AggregateExceptionType;
        Cache<TypeNode> Func2Type;

        /// <summary>
        /// There are 2 cases:
        /// 1) Task has no return value. In this case, we emit
        ///      void CheckMethod(Task t) {
        ///         var ae = t.Exception as AggregateException;
        ///         if (ae != null) { ae.Handle(this.CheckException); throw ae; }
        ///      }
        ///      bool CheckException(Exception e) {
        ///          .. check exceptional post
        ///      }
        /// 2) Task(T) returns a T value
        ///      T CheckMethod(Task t) {
        ///         try {
        ///            var r = t.Result;
        ///            .. check ensures on r ..
        ///            return r;
        ///         }
        ///         catch (AggregateException ae) {
        ///            ae.Handle(this.CheckException); 
        ///            throw;
        ///         }
        ///      }
        ///      bool CheckException(Exception e) {
        ///          .. check exceptional post
        ///      }
        /// </summary>
        public EmitAsyncClosure(Method from, Rewriter parent)
        {
            this.fromMethod = from;
            this.parent = parent;
            this.checkMethodId = Identifier.For("CheckPost");
            this.checkExceptionMethodId = Identifier.For("CheckException");
            this.declaringType = from.DeclaringType;
            var closureName = HelperMethods.NextUnusedMemberName(declaringType, "<" + from.Name.Name + ">AsyncContractClosure");
            this.closureClass = new Class(declaringType.DeclaringModule, declaringType, null, TypeFlags.NestedPrivate, null, Identifier.For(closureName), SystemTypes.Object, null, null);
            declaringType.Members.Add(this.closureClass);
            RewriteHelper.TryAddCompilerGeneratedAttribute(this.closureClass);

            this.dup = new Duplicator(this.declaringType.DeclaringModule, this.declaringType);

            var taskType = from.ReturnType;
            var taskArgs = taskType.TemplateArguments == null ? 0 : taskType.TemplateArguments.Count;

            this.AggregateExceptionType = new Cache<TypeNode>(() =>
                HelperMethods.FindType(parent.assemblyBeingRewritten, StandardIds.System, Identifier.For("AggregateException")));
            this.Func2Type = new Cache<TypeNode>(() =>
                HelperMethods.FindType(SystemTypes.SystemAssembly, StandardIds.System, Identifier.For("Func`2")));

            if (from.IsGeneric)
            {
                this.closureClass.TemplateParameters = new TypeNodeList();
                var parentCount = this.declaringType.ConsolidatedTemplateParameters == null ? 0 : this.declaringType.ConsolidatedTemplateParameters.Count;
                for (int i = 0; i < from.TemplateParameters.Count; i++)
                {
                    var tp = HelperMethods.NewEqualTypeParameter(dup, (ITypeParameter)from.TemplateParameters[i], this.closureClass, parentCount + i);

                    this.closureClass.TemplateParameters.Add(tp);
                }
                this.closureClass.IsGeneric = true;
                this.closureClass.EnsureMangledName();
                this.forwarder = new Specializer(this.declaringType.DeclaringModule, from.TemplateParameters, this.closureClass.TemplateParameters);
                this.forwarder.VisitTypeParameterList(this.closureClass.TemplateParameters);

                taskType = this.forwarder.VisitTypeReference(taskType);

            }
            else
            {
                this.closureClassInstance = this.closureClass;
            }
            var taskTemplate = HelperMethods.Unspecialize(taskType);
            var continueWithCandidates = taskTemplate.GetMembersNamed(Identifier.For("ContinueWith"));
            Method continueWithMethod = null;
            for (int i = 0; i < continueWithCandidates.Count; i++)
            {
                var cand = continueWithCandidates[i] as Method;
                if (cand == null) continue;
                if (taskArgs == 0)
                {
                    if (cand.IsGeneric) continue;
                    if (cand.ParameterCount != 1) continue;
                    var p = cand.Parameters[0];
                    var ptype = p.Type;
                    var ptypeTemplate = ptype;
                    while (ptypeTemplate.Template != null)
                    {
                        ptypeTemplate = ptypeTemplate.Template;
                    }
                    if (ptypeTemplate.Name.Name != "Action`1") continue;

                    continueWithMethod = cand;
                    break;
                }
                else
                {
                    if (!cand.IsGeneric) continue;
                    if (cand.TemplateParameters.Count != 1) continue;
                    if (cand.ParameterCount != 1) continue;
                    var p = cand.Parameters[0];
                    var ptype = p.Type;
                    var ptypeTemplate = ptype;
                    while (ptypeTemplate.Template != null)
                    {
                        ptypeTemplate = ptypeTemplate.Template;
                    }
                    if (ptypeTemplate.Name.Name != "Func`2") continue;

                    // now create instance, first of task
                    var taskInstance = taskTemplate.GetTemplateInstance(this.closureClass.DeclaringModule, taskType.TemplateArguments[0]);
                    var candMethod = taskInstance.GetMembersNamed(Identifier.For("ContinueWith"))[i] as Method;
                    continueWithMethod = candMethod.GetTemplateInstance(null, taskType.TemplateArguments[0]);
                    break;
                }
            }
            if (continueWithMethod != null)
            {
                this.continuewithMethod = continueWithMethod;
                EmitCheckMethod(taskType, taskArgs == 1);

                var ctor = new InstanceInitializer(this.closureClass, null, null, null);
                this.constructor = ctor;
                ctor.CallingConvention = CallingConventionFlags.HasThis;
                ctor.Flags |= MethodFlags.Public | MethodFlags.HideBySig;
                ctor.Body = new Block(new StatementList(
                    new ExpressionStatement(new MethodCall(new MemberBinding(ctor.ThisParameter, SystemTypes.Object.GetConstructor()), new ExpressionList())),
                    new Return()
                    ));
                this.closureClass.Members.Add(ctor);

            }

            // now that we added the ctor and the check method, let's instantiate the closure class if necessary
            if (this.closureClassInstance == null)
            {
                var consArgs = new TypeNodeList();
                var args = new TypeNodeList();
                var parentCount = this.closureClass.DeclaringType.ConsolidatedTemplateParameters == null ? 0 : this.closureClass.DeclaringType.ConsolidatedTemplateParameters.Count;
                for (int i = 0; i < parentCount; i++)
                {
                    consArgs.Add(this.closureClass.DeclaringType.ConsolidatedTemplateParameters[i]);
                }
                var methodCount = from.TemplateParameters == null ? 0: from.TemplateParameters.Count;
                for (int i = 0; i < methodCount; i++)
                {
                    consArgs.Add(from.TemplateParameters[i]);
                    args.Add(from.TemplateParameters[i]);
                }
                this.closureClassInstance = (Class)this.closureClass.GetConsolidatedTemplateInstance(this.parent.assemblyBeingRewritten, closureClass.DeclaringType, closureClass.DeclaringType, args, consArgs);
            }

            // create closure initializer for context method
            this.ClosureLocal = new Local(this.ClosureClass);
            this.ClosureInitializer = new Block(new StatementList());

            this.ClosureInitializer.Statements.Add(new AssignmentStatement(this.ClosureLocal, new Construct(new MemberBinding(null, this.Ctor), new ExpressionList())));

        }

        private void EmitCheckMethod(TypeNode taskType, bool hasResult)
        {
            var funcType = this.continuewithMethod.Parameters[0].Type;
            this.funcCtor = funcType.GetConstructor(SystemTypes.Object, SystemTypes.IntPtr);

            var taskParameter = new Parameter(Identifier.For("task"), taskType);
            this.originalResultLocal = new Local(taskParameter.Type);

            if (hasResult)
            {
                this.checkMethod = new Method(this.closureClass, null, this.checkMethodId, new ParameterList(taskParameter), taskType.TemplateArguments[0], null);
                checkMethod.CallingConvention = CallingConventionFlags.HasThis;
                checkMethod.Flags |= MethodFlags.Public;
                this.checkBody = new StatementList();
                var tmpresult = new Local(checkMethod.ReturnType);
                this.newResultLocal = tmpresult;
                checkBody.Add(new AssignmentStatement(this.originalResultLocal, taskParameter));
                checkBody.Add(new AssignmentStatement(tmpresult, new MethodCall(new MemberBinding(checkMethod.Parameters[0], checkMethod.Parameters[0].Type.GetMethod(Identifier.For("get_Result"))), new ExpressionList())));
            }
            else
            {
                this.checkMethod = new Method(this.closureClass, null, this.checkMethodId, new ParameterList(taskParameter), SystemTypes.Void, null);
                checkMethod.CallingConvention = CallingConventionFlags.HasThis;
                checkMethod.Flags |= MethodFlags.Public;
                this.checkBody = new StatementList();
                var aggregateType = AggregateExceptionType.Value;
                this.newResultLocal = new Local(aggregateType);
                checkBody.Add(new AssignmentStatement(this.originalResultLocal, taskParameter));
                checkBody.Add(new AssignmentStatement(this.newResultLocal, new BinaryExpression(new MethodCall(new MemberBinding(checkMethod.Parameters[0], checkMethod.Parameters[0].Type.GetMethod(Identifier.For("get_Exception"))), new ExpressionList()), new MemberBinding(null, aggregateType), NodeType.Isinst)));
            }
            this.closureClass.Members.Add(this.checkMethod);
        }


        public void AddAsyncPost(List<Ensures> asyncPostconditions)
        {
            var origBody = new Block(this.checkBody);
            origBody.HasLocals = true;

            var newBodyBlock = new Block(new StatementList());
            newBodyBlock.HasLocals = true;

            var methodBody = new StatementList();
            var methodBodyBlock = new Block(methodBody);
            methodBodyBlock.HasLocals = true;
            checkMethod.Body = methodBodyBlock;

            methodBody.Add(newBodyBlock);
            Block newExitBlock = new Block();
            methodBody.Add(newExitBlock);

            #region Map closure locals to fields and initialize closure fields

            foreach (Ensures e in asyncPostconditions)
            {
                if (e == null) continue;
                this.Visit(e);
                if (this.forwarder != null) { this.forwarder.Visit(e); }
                ReplaceResult repResult = new ReplaceResult(this.checkMethod, this.originalResultLocal, this.parent.assemblyBeingRewritten);
                repResult.Visit(e);
                // now need to initialize closure result fields
                foreach (var target in repResult.NecessaryResultInitializationAsync(this.closureLocals))
                {
                    // note: target here 
                    methodBody.Add(new AssignmentStatement(target, this.originalResultLocal));
                }
            }

            #endregion

            #region Emit normal postconditions

            SourceContext lastEnsuresSourceContext = default(SourceContext);
            bool hasLastEnsuresContext = false;
            bool containsExceptionalPostconditions = false;
            var ensuresChecks = new StatementList();
            foreach (Ensures e in asyncPostconditions)
            {
                // Exceptional postconditions are handled separately.
                if (e is EnsuresExceptional)
                {
                    containsExceptionalPostconditions = true;
                    continue;
                }

                if (IsVoidTask()) break; // something is wrong in the original contract

                lastEnsuresSourceContext = e.SourceContext;
                hasLastEnsuresContext = true;
                // call Contract.RewriterEnsures
                Method ensMethod = this.parent.runtimeContracts.EnsuresMethod;
                ExpressionList args = new ExpressionList();
                args.Add(e.PostCondition);
                if (e.UserMessage != null)
                    args.Add(e.UserMessage);
                else
                    args.Add(Literal.Null);
                if (e.SourceConditionText != null)
                {
                    args.Add(e.SourceConditionText);
                }
                else
                {
                    args.Add(Literal.Null);
                }
                ensuresChecks.Add(new ExpressionStatement(
                  new MethodCall(new MemberBinding(null, ensMethod),
                  args, NodeType.Call, SystemTypes.Void), e.SourceContext));
            }
            this.parent.cleanUpCodeCoverage.VisitStatementList(ensuresChecks);
            this.parent.EmitRecursionGuardAroundChecks(this.checkMethod, methodBodyBlock, ensuresChecks);
            #endregion Normal postconditions

            #region Exceptional postconditions
            if (containsExceptionalPostconditions)
            {
                // Because async tasks wrap exceptions into Aggregate exceptions, we have to catch AggregateException
                // and iterate over the internal exceptions of the Aggregate.

                // We thus emit the following handler:
                //
                //  catch(AggregateException ae) {
                //    ae.Handle(this.CheckException);
                //    rethrow;
                //  }
                //
                //  alternatively, if the Task has no Result, we emit
                //
                //    var ae = t.Exception as AggregateException;
                //    if (ae != null) {
                //      ae.Handle(this.CheckException);
                //      throw ae;
                //    }

                var aggregateType = AggregateExceptionType.Value;

                var exnParameter = new Parameter(Identifier.For("e"), SystemTypes.Exception);
                this.checkExceptionMethod = new Method(this.closureClass, null, this.checkExceptionMethodId, new ParameterList(exnParameter), SystemTypes.Boolean, new Block(new StatementList()));
                this.checkExceptionMethod.Body.HasLocals = true;
                checkExceptionMethod.CallingConvention = CallingConventionFlags.HasThis;
                checkExceptionMethod.Flags |= MethodFlags.Public;

                this.closureClass.Members.Add(checkExceptionMethod);

                if (this.IsVoidTask())
                {
                    var blockAfterTest = new Block();
                    methodBody.Add(origBody);
                    methodBody.Add(new Branch(new UnaryExpression(this.newResultLocal, NodeType.LogicalNot), blockAfterTest));
                    var funcType = Func2Type.Value;
                    funcType = funcType.GetTemplateInstance(this.parent.assemblyBeingRewritten, SystemTypes.Exception, SystemTypes.Boolean);
                    var handleMethod = aggregateType.GetMethod(Identifier.For("Handle"), funcType);

                    var funcLocal = new Local(funcType);
                    var ldftn = new UnaryExpression(new MemberBinding(null, this.checkExceptionMethod), NodeType.Ldftn, CoreSystemTypes.IntPtr);
                    methodBody.Add(new AssignmentStatement(funcLocal, new Construct(new MemberBinding(null, funcType.GetConstructor(SystemTypes.Object, SystemTypes.IntPtr)), new ExpressionList(this.checkMethod.ThisParameter, ldftn))));
                    methodBody.Add(new ExpressionStatement(new MethodCall(new MemberBinding(this.newResultLocal, handleMethod), new ExpressionList(funcLocal))));
                    methodBody.Add(new Throw(this.newResultLocal));
                    methodBody.Add(blockAfterTest);
                }
                else
                {
                    // The tryCatch holds the try block, the catch blocks, and an empty block that is the
                    // target of an unconditional branch for normal execution to go from the try block
                    // around the catch blocks.
                    if (this.checkMethod.ExceptionHandlers == null) this.checkMethod.ExceptionHandlers = new ExceptionHandlerList();

                    Block afterCatches = new Block(new StatementList());

                    Block tryCatch = newBodyBlock;
                    Block tryBlock = new Block(new StatementList());
                    tryBlock.Statements.Add(origBody);
                    tryBlock.Statements.Add(new Branch(null, afterCatches, false, true, true));
                    // the EH needs to have a pointer to this block so the writer can
                    // calculate the length of the try block. So it should be the *last*
                    // thing in the try body.
                    Block blockAfterTryBody = new Block(null);
                    tryBlock.Statements.Add(blockAfterTryBody);
                    tryCatch.Statements.Add(tryBlock);
                    // The catchBlock contains the catchBody, and then
                    // an empty block that is used in the EH.

                    Block catchBlock = new Block(new StatementList());
                    Local l = new Local(aggregateType);
                    Throw rethrow = new Throw();
                    rethrow.NodeType = NodeType.Rethrow;

                    catchBlock.Statements.Add(new AssignmentStatement(l, new Expression(NodeType.Pop)));

                    var funcType = Func2Type.Value;
                    funcType = funcType.GetTemplateInstance(this.parent.assemblyBeingRewritten, SystemTypes.Exception, SystemTypes.Boolean);
                    var handleMethod = aggregateType.GetMethod(Identifier.For("Handle"), funcType);

                    var funcLocal = new Local(funcType);
                    var ldftn = new UnaryExpression(new MemberBinding(null, this.checkExceptionMethod), NodeType.Ldftn, CoreSystemTypes.IntPtr);
                    catchBlock.Statements.Add(new AssignmentStatement(funcLocal, new Construct(new MemberBinding(null, funcType.GetConstructor(SystemTypes.Object, SystemTypes.IntPtr)), new ExpressionList(this.checkMethod.ThisParameter, ldftn))));
                    catchBlock.Statements.Add(new ExpressionStatement(new MethodCall(new MemberBinding(l, handleMethod), new ExpressionList(funcLocal))));
                    // Handle method should return if all passes
                    catchBlock.Statements.Add(rethrow);
                    // The last thing in each catch block is an empty block that is the target of
                    // BlockAfterHandlerEnd in each exception handler.
                    // It is used in the writer to determine the length of each catch block
                    // so it should be the last thing added to each catch block.
                    Block blockAfterHandlerEnd = new Block(new StatementList());
                    catchBlock.Statements.Add(blockAfterHandlerEnd);
                    tryCatch.Statements.Add(catchBlock);

                    // add information to the ExceptionHandlers of this method
                    ExceptionHandler exHandler = new ExceptionHandler();
                    exHandler.TryStartBlock = origBody;
                    exHandler.BlockAfterTryEnd = blockAfterTryBody;
                    exHandler.HandlerStartBlock = catchBlock;
                    exHandler.BlockAfterHandlerEnd = blockAfterHandlerEnd;
                    exHandler.FilterType = l.Type;
                    exHandler.HandlerType = NodeType.Catch;
                    this.checkMethod.ExceptionHandlers.Add(exHandler);

                    tryCatch.Statements.Add(afterCatches);
                }
                EmitCheckExceptionBody(asyncPostconditions);
            }
            else // no exceptional post conditions
            {
                newBodyBlock.Statements.Add(origBody);
            }
            #endregion Exceptional and finally postconditions


            #region Create a block for the return statement and insert it
            // this is the block that contains the return statements
            // it is (supposed to be) the single exit from the method
            // that way, anything that must always be done can be done
            // in this block
            Statement returnStatement;
            if (this.IsVoidTask())
            {
                returnStatement = new Return();
            }
            else
            {
                returnStatement = new Return(this.newResultLocal);
            }
            if (hasLastEnsuresContext)
            {
                returnStatement.SourceContext = lastEnsuresSourceContext;
            }
            Block returnBlock = new Block(new StatementList(1));
            returnBlock.Statements.Add(returnStatement);
            methodBody.Add(returnBlock);
            #endregion
        }

        private bool IsVoidTask()
        {
            return this.checkMethod.ReturnType == SystemTypes.Void;
        }

        void EmitCheckExceptionBody(List<Ensures> asyncPostconditions)
        {
            // We emit the following method:
            //   bool CheckException(Exception e) {
            //     var c1 = e as C1;
            //     if (c1 != null) {
            //       code for handling c1
            //       goto Exit;
            //     }
            //     ...
            //   Exit:
            //     return true; // handled

            if (this.checkExceptionMethod.ExceptionHandlers == null) this.checkExceptionMethod.ExceptionHandlers = new ExceptionHandlerList();

            var body = this.checkExceptionMethod.Body.Statements;
            var returnBlock = new Block(new StatementList());

            for (int i = 0, n = asyncPostconditions.Count; i < n; i++)
            {
                // Normal postconditions are handled separately.
                EnsuresExceptional e = asyncPostconditions[i] as EnsuresExceptional;
                if (e == null)
                    continue;

                // The catchBlock contains the catchBody, and then
                // an empty block that is used in the EH.
                Block catchBlock = new Block(new StatementList());
                Local l = new Local(e.Type);

                body.Add(new AssignmentStatement(l, new BinaryExpression(this.checkExceptionMethod.Parameters[0], new MemberBinding(null, e.Type), NodeType.Isinst)));
                Block skipBlock = new Block();
                body.Add(new Branch(new UnaryExpression(l, NodeType.LogicalNot), skipBlock));
                body.Add(catchBlock);
                body.Add(skipBlock);

                // call Contract.RewriterEnsures
                ExpressionList args = new ExpressionList();
                args.Add(e.PostCondition);
                if (e.UserMessage != null)
                    args.Add(e.UserMessage);
                else
                    args.Add(Literal.Null);
                if (e.SourceConditionText != null)
                {
                    args.Add(e.SourceConditionText);
                }
                else
                {
                    args.Add(Literal.Null);
                }
                args.Add(l);
                var checks = new StatementList();
                checks.Add(new ExpressionStatement(
                  new MethodCall(new MemberBinding(null, this.parent.runtimeContracts.EnsuresOnThrowMethod),
                  args, NodeType.Call, SystemTypes.Void), e.SourceContext));

                this.parent.cleanUpCodeCoverage.VisitStatementList(checks);
                parent.EmitRecursionGuardAroundChecks(this.checkExceptionMethod, catchBlock, checks);

                catchBlock.Statements.Add(new Branch(null, returnBlock));
            }
            // recurse on AggregateException itself
            {
                // var ae = e as AggregateException;
                // if (ae != null) {
                //   ae.Handle(this.CheckException);
                // }
                Block catchBlock = new Block(new StatementList());
                var aggregateType = AggregateExceptionType.Value;

                Local l = new Local(aggregateType);
                body.Add(new AssignmentStatement(l, new BinaryExpression(this.checkExceptionMethod.Parameters[0], new MemberBinding(null, aggregateType), NodeType.Isinst)));
                Block skipBlock = new Block();
                body.Add(new Branch(new UnaryExpression(l, NodeType.LogicalNot), skipBlock));
                body.Add(catchBlock);
                body.Add(skipBlock);

                var funcType = Func2Type.Value;
                funcType = funcType.GetTemplateInstance(this.parent.assemblyBeingRewritten, SystemTypes.Exception, SystemTypes.Boolean);
                var handleMethod = aggregateType.GetMethod(Identifier.For("Handle"), funcType);

                var funcLocal = new Local(funcType);
                var ldftn = new UnaryExpression(new MemberBinding(null, this.checkExceptionMethod), NodeType.Ldftn, CoreSystemTypes.IntPtr);
                catchBlock.Statements.Add(new AssignmentStatement(funcLocal, new Construct(new MemberBinding(null, funcType.GetConstructor(SystemTypes.Object, SystemTypes.IntPtr)), new ExpressionList(this.checkMethod.ThisParameter, ldftn))));
                catchBlock.Statements.Add(new ExpressionStatement(new MethodCall(new MemberBinding(l, handleMethod), new ExpressionList(funcLocal))));
            }

            // add return true to CheckExceptionMethod
            body.Add(returnBlock);
            body.Add(new Return(Literal.True));

        }
        #region Visitor for changing closure locals to fields
        Dictionary<Local, MemberBinding> closureLocals = new Dictionary<Local, MemberBinding>();

        public override Expression VisitLocal(Local local)
        {
            if (HelperMethods.IsClosureType(this.declaringType, local.Type))
            {
                MemberBinding mb;
                if (!closureLocals.TryGetValue(local, out mb))
                {
                    var closureField = new Field(this.closureClass, null, FieldFlags.Public, local.Name, this.forwarder.VisitTypeReference(local.Type), null);
                    this.closureClass.Members.Add(closureField);
                    mb = new MemberBinding(this.checkMethod.ThisParameter, closureField);
                    closureLocals.Add(local, mb);

                    // initialize the closure field
                    var instantiatedField = GetMemberInstanceReference(closureField, this.closureClassInstance);
                    this.ClosureInitializer.Statements.Add(new AssignmentStatement(new MemberBinding(this.ClosureLocal, instantiatedField), local));

                }
                return mb;
            }
            return local;
        }
        #endregion

    }

    private void EmitInterleavedValidationsAndRequires(Method method, List<Requires> inherited, RequiresList validations, Block newBody)
    {
      List<Requires> reqSequence = new List<Requires>();
      if (validations != null)
      {
        foreach (var val in validations)
        {
          var ro = val as RequiresOtherwise;
          if (ro != null)
          {
            FlushEmitOrdinaryRequires(reqSequence, method, newBody);
            if (this.Emit(RuntimeContractEmitFlags.LegacyRequires))
            {
              newBody.Statements.Add(GenerateValidationCode(ro));
            }
          }
          else
          {
            if (EmitRequires((RequiresPlain)val, this.skipQuantifiers))
            {
              reqSequence.Add(val);
            }
          }
        }
      }
      if (inherited.Count > 0)
      {
        foreach (var req in inherited) { reqSequence.Add(req); }
      }
      FlushEmitOrdinaryRequires(reqSequence, method, newBody);
    }

    void FlushEmitOrdinaryRequires(List<Requires> reqs, Method method, Block newBody)
    {
      Contract.Ensures(reqs.Count == 0);
      if (reqs.Count == 0) return;

      EmitRecursionGuardAroundChecks(method, newBody, EmitRequiresList(reqs));
      reqs.Clear();
    }

    private static bool BodyHasCalls(Block block) {
      var callcounter = new UnknownCallCounter(false);
      callcounter.VisitBlock(block);
      return callcounter.Count > 0;
    }

    private Statement GenerateValidationCode(RequiresOtherwise ro)
    {
      if (ro.Condition == null)
      {
        // just a validation code block
        BlockExpression be = (BlockExpression)ro.ThrowException;
        return be.Block;
      }
      // make an if-then for it so the exception doesn't get evaluated if the condition is true
      Block b = new Block(new StatementList());
      Block afterIf = new Block(new StatementList());
      b.Statements.Add(new Branch(ro.Condition, afterIf));
      // add call to RaiseContractFailedEvent method
      ExpressionList elist = new ExpressionList();
      elist.Add(this.runtimeContracts.PreconditionKind);
      if (ro.UserMessage != null)
        elist.Add(ro.UserMessage);
      else
        elist.Add(Literal.Null);
      if (ro.SourceConditionText != null)
      {
        elist.Add(ro.SourceConditionText);
      }
      else
      {
        elist.Add(Literal.Null);
      }
      elist.Add(Literal.Null); // exception arg
      if (this.runtimeContracts.AssertOnFailure)
      {
        var messageLocal = new Local(SystemTypes.String);
        b.Statements.Add(new AssignmentStatement(messageLocal, new MethodCall(new MemberBinding(null, this.runtimeContracts.RaiseFailureEventMethod), elist)));
        // if we should assert and the msgLocal is non-null, then assert
        var assertMethod = RuntimeContractMethods.GetSystemDiagnosticsAssertMethod();
        if (assertMethod != null)
        {
          var skipAssert = new Block();
          b.Statements.Add(new Branch(new UnaryExpression(messageLocal, NodeType.LogicalNot), skipAssert));
          // emit assert call
          ExpressionList assertelist = new ExpressionList();
          assertelist.Add(Literal.False);
          assertelist.Add(messageLocal);
          b.Statements.Add(new ExpressionStatement(new MethodCall(new MemberBinding(null, assertMethod), assertelist)));
          b.Statements.Add(skipAssert);
        }

      }
      else
      {
        b.Statements.Add(new ExpressionStatement(new UnaryExpression(new MethodCall(new MemberBinding(null, this.runtimeContracts.RaiseFailureEventMethod), elist), NodeType.Pop)));
      }

      // need to check whether it was "throw ..." in the original source or else "ThrowHelper.Throw(...)"
      if (HelperMethods.IsVoidType(ro.ThrowException.Type))
      {
        // then it is a call to a throw helper method
        b.Statements.Add(new ExpressionStatement(ro.ThrowException));
      }
      else
      {
        b.Statements.Add(new Throw(ro.ThrowException, ro.ThrowException.SourceContext));
      }
      b.Statements.Add(afterIf);
      return b;
    }

    private bool EmitRequires(RequiresPlain precondition, bool skipQuantifiers)
    {
      if (CodeInspector.IsRuntimeIgnored(precondition, this.runtimeContracts.ContractNodes, null, skipQuantifiers)) return false;

      // if (precondition is RequiresOtherwise) { return Emit(RuntimeContractEmitFlags.LegacyRequires); }
      if (precondition.IsWithException)
      {
        // skip Requires<E> when emitting validations
        if (this.runtimeContracts.UseExplicitValidation) return false;
        return Emit(RuntimeContractEmitFlags.RequiresWithException);
      }
      return Emit(RuntimeContractEmitFlags.Requires);
    }
    private bool EmitEnsures(Ensures postcondition, TypeNode referencingType, bool skipQuantifiers) {
      if (CodeInspector.IsRuntimeIgnored(postcondition, this.runtimeContracts.ContractNodes, referencingType, skipQuantifiers)) return false;

      return this.Emit(RuntimeContractEmitFlags.Ensures);
    }
    private bool EmitInvariant(Invariant invariant, bool skipQuantifiers) {
      return !CodeInspector.IsRuntimeIgnored(invariant, this.runtimeContracts.ContractNodes, null, skipQuantifiers);
    }

    static bool IsLiteral0Or1(Expression expr)
    {
      Literal lit = expr as Literal;
      if (lit == null) return false;
      if (!(lit.Value is int)) return false;
      int value = (int)lit.Value;
      return value == 0 || value == 1;
    }

    IncreaseCodeCoverageOfContractCode cleanUpCodeCoverage = new IncreaseCodeCoverageOfContractCode();

    class IncreaseCodeCoverageOfContractCode : Inspector
    {
      public override void VisitExpressionStatement(ExpressionStatement statement)
      {
        if (statement == null) return;
        if (IsLiteral0Or1(statement.Expression))
        {
          // assume this is from an && or || shortcut evaluation. A single block
          // whack out its source context so code coverage handles it as covered
          statement.SourceContext = new SourceContext(HiddenDocument.Document);
        }
        base.VisitExpressionStatement(statement);
      }
    }

    private void EmitRecursionGuardAroundChecks(Method method, Block newBody, StatementList checks)
    {
      StatementList stmts = new StatementList();
      Block preconditionsStart = new Block(stmts);
      Block finallyStart = null;
      Block finallyEnd = new Block(); // branch target for skipping the checks

      // test if we are an auto property and need to disable the check if we are in construction
      if (this.ReentrancyFlag != null && (method.IsPropertyGetter || method.IsPropertySetter) && HelperMethods.IsAutoPropertyMember(method) && !method.IsStatic) {
        newBody.Statements.Add(new Branch(new MemberBinding(method.ThisParameter, this.ReentrancyFlag), finallyEnd));
      }

      // don't add try finally if there are no precondition or if the method is a constructor (peverify issue)
      if (NeedsRecursionGuard(method, checks))
      {
        // emit recursion check
        finallyStart = new Block(new StatementList());

        // if (insideContractEvaluation > $recursionGuard$) goto finallyEnd
        // try {
        //   insideContractEvaluation++;
        //   checks;
        //   leave;
        // } finally {
        //   insideContractEvaluation--;
        // }
        //
        // SPECIAL CASE for auto properties where we made invariants into pre/post, we need to avoid the check if we are 
        // evaluating the invariant.
        //
        // if (this.$evaluatingInvariant || insideContractEvaluation > $recursionGuard$) goto finallyEnd
        // 

        newBody.Statements.Add(new Branch(new BinaryExpression(new MemberBinding(null, this.runtimeContracts.InContractEvaluationField), new Literal(this.runtimeContracts.RecursionGuardCountFor(method), SystemTypes.Int32), NodeType.Gt), finallyEnd));

        stmts.Add(new AssignmentStatement(new MemberBinding(null, this.runtimeContracts.InContractEvaluationField),
                                          new BinaryExpression(new MemberBinding(null, this.runtimeContracts.InContractEvaluationField), Literal.Int32One, NodeType.Add)));
        stmts.Add(new Block(checks));
        var leave = new Branch();
        leave.Target = finallyEnd;
        leave.LeavesExceptionBlock = true;
        stmts.Add(leave);
        
        finallyStart.Statements.Add(new AssignmentStatement(new MemberBinding(null, this.runtimeContracts.InContractEvaluationField),
                                    new BinaryExpression(new MemberBinding(null, this.runtimeContracts.InContractEvaluationField), Literal.Int32One, NodeType.Sub)));

        finallyStart.Statements.Add(new EndFinally());
      }
      else
      {
        stmts.Add(new Block(checks));
      }

      newBody.Statements.Add(preconditionsStart);
      if (finallyStart != null)
      {
        newBody.Statements.Add(finallyStart);
        var finallyHandler = new ExceptionHandler();
        finallyHandler.TryStartBlock = preconditionsStart;
        finallyHandler.BlockAfterTryEnd = finallyStart;
        finallyHandler.HandlerStartBlock = finallyStart;
        finallyHandler.BlockAfterHandlerEnd = finallyEnd;
        finallyHandler.HandlerType = NodeType.Finally;

        method.ExceptionHandlers.Add(finallyHandler);
      }
      // branch target for skipping the checks
      newBody.Statements.Add(finallyEnd);
    }

    private bool NeedsRecursionGuard(Method method, StatementList checks) {
      if (ContractContainsOnlyKnownMethodCalls(checks)) return false;
      if (method is InstanceInitializer) return false;
      if (this.runtimeContracts.RecursionGuardCountFor(method) <= 0) return false;

      return true;
    }

    private static bool ContractContainsOnlyKnownMethodCalls(StatementList checks) {
      if (checks == null) return true;
      if (checks.Count == 0) return true;

      var unknownMethodCallCounter = new UnknownCallCounter(true);
      unknownMethodCallCounter.VisitStatementList(checks);
      return unknownMethodCallCounter.Count == 0;
    }

    private class UnknownCallCounter : Inspector {

      bool ignoreVoidMethods;

      public UnknownCallCounter(bool ignoreVoidMethods) {
        this.ignoreVoidMethods = ignoreVoidMethods;
      }
      public int Count { get; private set; }

      public override void VisitMethodCall(MethodCall call) {
        if (call == null) return;
        MemberBinding mb = call.Callee as MemberBinding;
        if (mb != null) {
          Method method = mb.BoundMember as Method;
          if (method != null) {
            HandleMethod(method);
          }
        }
        base.VisitMethodCall(call);
      }

      private static readonly Identifier MathIdentifier = Identifier.For("Math");

      private void HandleMethod(Method method) {
        // F:
        Contract.Requires(method != null);

        if (this.ignoreVoidMethods && HelperMethods.IsVoidType(method.ReturnType)) return; // contract calls and validators are okay

        // F: ?
        Contract.Assume(method.DeclaringType != null);
        if (method.DeclaringType.IsPrimitive) return; // don't bother with primitive ops
        if (method.DeclaringType.Name.Matches(MathIdentifier)) return;
        Count++;
      }
    }

    private StatementList EmitRequiresList(List<Requires> preconditions)
    {
      var stmts = new StatementList();
      foreach (Requires r in preconditions)
      {
        RequiresOtherwise ro = r as RequiresOtherwise;
        if (ro != null)
        {
          throw new InvalidOperationException("found legacy-requires in normal requires list");
        } else
        {
          RequiresPlain reqplain = r as RequiresPlain;
          Method reqMethod;
          if (reqplain != null && reqplain.ExceptionType != null)
          {
            reqMethod = this.runtimeContracts.RequiresWithExceptionMethod.GetTemplateInstance(null, reqplain.ExceptionType);
          } else
          {
            reqMethod = this.runtimeContracts.RequiresMethod;
          }
          ExpressionList args = new ExpressionList();
          args.Add(r.Condition);
          if (r.UserMessage != null)
            args.Add(r.UserMessage);
          else
            args.Add(Literal.Null);
          if (r.SourceConditionText != null)
          {
            args.Add(r.SourceConditionText);
          } else
          {
            args.Add(Literal.Null);
          }
          stmts.Add(new ExpressionStatement(
            new MethodCall(new MemberBinding(null, reqMethod),
            args, NodeType.Call, SystemTypes.Void), r.SourceContext));
        }
        stmts.Add(new Statement(NodeType.Nop, r.SourceContext)); // for debugging.
      }
      this.cleanUpCodeCoverage.VisitStatementList(stmts);
      return stmts;
    }

    /// <summary>
    /// CCI1's IsVisibleOutsideAssembly is not the same
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    [ContractVerification(true)]
    private static bool IsCallableFromOutsideAssembly(Method method)
    {
      Contract.Requires(method != null);

      if (method.IsVisibleOutsideAssembly) return true;
      if (method.ImplementedInterfaceMethods != null)
      {
        foreach (Method m in method.ImplementedInterfaceMethods)
        {
          if (m == null) continue;
          Contract.Assume(m.DeclaringType != null);
          Contract.Assume(method.DeclaringType != null);
          if (m.DeclaringType.DeclaringModule != method.DeclaringType.DeclaringModule) return true;
          if (m.IsVisibleOutsideAssembly) return true;
        }
      }
      if (method.ImplicitlyImplementedInterfaceMethods != null)
      {
        foreach (Method m in method.ImplicitlyImplementedInterfaceMethods)
        {
          if (m == null) continue;
          Contract.Assume(m.DeclaringType != null);
          Contract.Assume(method.DeclaringType != null);
          if (m.DeclaringType.DeclaringModule != method.DeclaringType.DeclaringModule) return true;
          if (m.IsVisibleOutsideAssembly) return true;
        }
      }
      Method overridden = method;
      while ((overridden = overridden.OverriddenMethod) != null)
      {
          if (overridden.IsVisibleOutsideAssembly) return true;
      }
      return false;
    }

    private bool MethodShouldHaveInvariantChecked(Method method, bool inExceptionCase = false) {
      return
        this.Emit(RuntimeContractEmitFlags.Invariants)
        && !method.IsStatic
        // Check invariants even for non-public ctors
        && (method.IsPublic || method is InstanceInitializer)
        && this.InvariantMethod != null
        && !(inExceptionCase && method is InstanceInitializer)
        && !rewriterNodes.IsPure(method) // don't check invariant for pure methods
        && !RewriteHelper.Implements(method, this.IDisposeMethod)
        && !RewriteHelper.IsFinalizer(method)
        ;
    }

    /// <summary>
    /// Returns a root method this method is overriding/implementing. A root method is one that does not itself 
    /// override/implement another method. In case of ambiguity (mulitple interface methods, or base class + interface),
    /// the first interface method found is used.
    /// </summary>
    private Method GetRootMethod(Method instantiatedMethod)
    {
      // F:
      Contract.Requires(instantiatedMethod != null);

      if (instantiatedMethod.IsVirtual && HelperMethods.DoesInheritContracts(instantiatedMethod)) {
        // find method introducing this

        // first check for implemented interfaces
        if (instantiatedMethod.ImplementedInterfaceMethods != null) {
          foreach (Method interfaceMethod in instantiatedMethod.ImplementedInterfaceMethods) {
            if (interfaceMethod != null) {
              return interfaceMethod;
            }
          }
        }
        // check for implicit interface implementations
        if (instantiatedMethod.ImplicitlyImplementedInterfaceMethods != null) {
          foreach (Method interfaceMethod in instantiatedMethod.ImplicitlyImplementedInterfaceMethods) {
            if (interfaceMethod != null) {
              return interfaceMethod;
            }
          }
        }

        // now check for base overrides
        if ((instantiatedMethod.Flags & MethodFlags.NewSlot) != 0) return instantiatedMethod;
        // search overrides
        if (instantiatedMethod.OverridesBaseClassMember && instantiatedMethod.DeclaringType != null && instantiatedMethod.OverriddenMethod != null) {
          for (TypeNode super = instantiatedMethod.DeclaringType.BaseType; super != null; super = super.BaseType) {
            var candidate = super.GetImplementingMethod(instantiatedMethod.OverriddenMethod, false);
            if (candidate != null) { return GetRootMethod(candidate); }
          }
        }
      }
      return instantiatedMethod;
    }



    /// <summary>
    /// Collect all of the "old" expressions and returns a block consisting of code
    /// that captures the values of the old expressions in the prestate. The postconditions
    /// are modified by replacing any "old" expressions with an expression that evaluates
    /// to the snapshot made in the prestate.
    /// Simplest case is when "old(x)" is replaced by a local, l, and the prestate  code
    /// is just "l := x".
    /// Most complicated case is if the old expression occurs in a set of nested anonymous
    /// delegates and bound variables from the delegates are captured in the old expression.
    /// </summary>
    /// <param name="method"></param>
    /// <param name="postconditions">These get modified!!</param>
    /// <param name="closureLocal"></param>
    /// <returns></returns>
    private Block ProcessOldExpressions(Method method, EnsuresList postconditions, Dictionary<TypeNode,Local> closureLocals, ref int oldLocalNameCounter)
    {
      Contract.Requires(postconditions != null);
      CollectOldExpressions coe = new CollectOldExpressions(
        this.module,
        method,
        this.rewriterNodes,
        closureLocals,
        oldLocalNameCounter
        );
      foreach (Ensures e in postconditions)
      {
        if (!EmitEnsures(e, method.DeclaringType, this.skipQuantifiers)) continue; 
        coe.Visit(e);
      }
      oldLocalNameCounter = coe.Counter;
      var oldAssignments = coe.PrestateValuesOfOldExpressions;
      // don't wrap in a try catch if the method is a constructor (peverify issue)
      if (!(method is InstanceInitializer))
      {
        WrapOldAssignmentsInTryCatch(method, oldAssignments);
      }
      return oldAssignments;
    }

    private Block ProcessOldExpressionsInAsync(Method method, EnsuresList asyncpostconditions, Dictionary<TypeNode, Local> closureLocals, ref int oldLocalNameCounter, Class asyncClosure)
    {
        Contract.Requires(asyncpostconditions != null);
        CollectOldExpressions coe = new CollectOldExpressions(
          this.module,
          method,
          this.rewriterNodes,
          closureLocals,
          oldLocalNameCounter,
          asyncClosure
          );
        foreach (Ensures e in asyncpostconditions)
        {
            if (!EmitEnsures(e, method.DeclaringType, this.skipQuantifiers)) continue;
            coe.Visit(e);
        }
        oldLocalNameCounter = coe.Counter;
        var oldAssignments = coe.PrestateValuesOfOldExpressions;
        // don't wrap in a try catch if the method is a constructor (peverify issue)
        if (!(method is InstanceInitializer))
        {
            WrapOldAssignmentsInTryCatch(method, oldAssignments);
        }
        return oldAssignments;
    }

    private static void WrapOldAssignmentsInTryCatch(Method method, Block oldAssignments)
    {
      if (oldAssignments == null || oldAssignments.Statements == null) return;
      // this is a list of simple statements or blocks. Each corresponds to a separate old expression
      // We wrap each of them in a try/catch block to avoid issues with old expressions failing when they
      // aren't needed
      for (int i = 0; i < oldAssignments.Statements.Count; i++)
      {
        oldAssignments.Statements[i] = WrapTryCatch(method, oldAssignments.Statements[i]);
      }
    }

    private static Statement WrapTryCatch(Method method, Statement statement)
    {
      Block afterCatches = new Block(new StatementList());
      Block tryBlock = new Block(new StatementList());
      Block blockAfterTryBody = new Block(null);
      tryBlock.Statements.Add(statement);
      tryBlock.Statements.Add(new Branch(null, afterCatches, false, true, true));
      tryBlock.Statements.Add(blockAfterTryBody);
      Block catchBlock = new Block(new StatementList());
      // emit code that pops the exception and fools fxcop
      Block branchTargetToFoolFxCop = new Block(null);
      var branch = new Branch(new Expression(NodeType.Pop), branchTargetToFoolFxCop);
      SourceContext hiddenContext = new SourceContext(HiddenDocument.Document);
      branch.SourceContext = hiddenContext;
      catchBlock.Statements.Add(branch);
      var rethrowStatement = new Throw();
      rethrowStatement.SourceContext = hiddenContext;
      rethrowStatement.NodeType = NodeType.Rethrow;
      catchBlock.Statements.Add(rethrowStatement);
      catchBlock.Statements.Add(branchTargetToFoolFxCop);
      var leave = new Branch(null, afterCatches, false, true, true);
      leave.SourceContext = hiddenContext;
      catchBlock.Statements.Add(leave);
      Block tryCatch = new Block(new StatementList());
      tryCatch.Statements.Add(tryBlock);
      tryCatch.Statements.Add(catchBlock);
      tryCatch.Statements.Add(afterCatches);

      if (method.ExceptionHandlers == null) method.ExceptionHandlers = new ExceptionHandlerList();

      ExceptionHandler exHandler = new ExceptionHandler();
      exHandler.TryStartBlock = tryBlock;
      exHandler.BlockAfterTryEnd = blockAfterTryBody;
      exHandler.HandlerStartBlock = catchBlock;
      exHandler.BlockAfterHandlerEnd = afterCatches;
      exHandler.FilterType = SystemTypes.Exception;
      exHandler.HandlerType = NodeType.Catch;
      method.ExceptionHandlers.Add(exHandler);

      return tryCatch;
    }


    public override void VisitAssembly(AssemblyNode assembly)
    {
      // Don't rewrite assemblies twice.
      if (ContractNodes.IsAlreadyRewritten(assembly)) {
        throw new RewriteException("Cannot rewrite an assembly that has already been rewritten!");
      }

      this.module = assembly;

      this.AdaptRuntimeOptionsBasedOnAttributes(assembly.Attributes);

      // Extract all inline foxtrot contracts and place them in the object model.
      //if (this.extractContracts) {
      //  new Extractor(rewriterNodes, this.Verbose, this.Decompile).Visit(assembly);
      //}
      base.VisitAssembly(assembly);

      this.runtimeContracts.Commit();

      // Set the flag that indicates the assembly has been rewritten.
      SetRuntimeContractFlag(assembly);

      // Add wrapper types for call-site requires. We do it here to avoid visiting them multiple times
      foreach (TypeNode t in this.wrapperTypes.Values)
      {
        assembly.Types.Add(t);
      }

      // in principle we shouldn't have old and result left over, but because of the call-site requires copying
      // we end up having them in closures that were used in ensures but not needed at call site requires
#if !DEBUG || true
      CleanUpOldAndResult cuoar = new CleanUpOldAndResult();
      assembly = cuoar.VisitAssembly(assembly);
#endif
      RemoveContractClasses rcc = new RemoveContractClasses();
      rcc.VisitAssembly(assembly);
    }

    private RuntimeContractEmitFlags AdaptRuntimeOptionsBasedOnAttributes(AttributeList attributeList)
    {
      var oldflags = this.contractEmitFlags;

      if (attributeList != null)
      {
        for (int i = 0; i < attributeList.Count; i++)
        {
          string category, setting;
          bool toggle;
          if (!HelperMethods.IsContractOptionAttribute(attributeList[i], out category, out setting, out toggle)) continue;
          if (string.Compare(category, "Contract", true) == 0)
          {
            if (string.Compare(setting, "Inheritance", true) == 0)
            {
              if (toggle)
              {
                this.contractEmitFlags |= RuntimeContractEmitFlags.InheritContracts;
              }
              else
              {
                this.contractEmitFlags &= ~(RuntimeContractEmitFlags.InheritContracts);
              }
            }
          }
          if (string.Compare(category, "Runtime", true) == 0)
          {
            if (string.Compare(setting, "Checking", true) == 0)
            {
              if (toggle)
              {
                this.contractEmitFlags &= ~(RuntimeContractEmitFlags.NoChecking);
              }
              else
              {
                this.contractEmitFlags |= RuntimeContractEmitFlags.NoChecking;
              }
            }
          }
        }
      }
      return oldflags;
    }

    #region Runtime Contract attribute
    /// <summary>
    /// Adds a flag to an assembly that designates it as having runtime contract checks.
    /// Does this by defining the type of the attribute and then marking the assembly with
    /// and instance of that attribute.
    /// </summary>
    /// <param name="assembly">Assembly to flag.</param>
    private void SetRuntimeContractFlag(AssemblyNode assembly) {

      InstanceInitializer ctor = GetRuntimeContractsAttributeCtor(assembly);
      ExpressionList args = new ExpressionList();
      args.Add(new Literal(this.contractEmitFlags, ctor.Parameters[0].Type));
      AttributeNode attribute = new AttributeNode(new MemberBinding(null, ctor), args, AttributeTargets.Assembly);
      assembly.Attributes.Add(attribute);
    }

    /// <summary>
    /// Tries to reuse or create the attribute
    /// </summary>
    private static InstanceInitializer GetRuntimeContractsAttributeCtor(AssemblyNode assembly)
    {
      EnumNode runtimeContractsFlags = assembly.GetType(ContractNodes.ContractNamespace, Identifier.For("RuntimeContractsFlags")) as EnumNode;
      Class RuntimeContractsAttributeClass = assembly.GetType(ContractNodes.ContractNamespace, Identifier.For("RuntimeContractsAttribute")) as Class;

      if (runtimeContractsFlags == null)
      {
        #region Add [Flags]
        Member flagsConstructor = RewriteHelper.flagsAttributeNode.GetConstructor();
        AttributeNode flagsAttribute = new AttributeNode(new MemberBinding(null, flagsConstructor), null, AttributeTargets.Class);
        #endregion Add [Flags]
        runtimeContractsFlags = new EnumNode(assembly,
          null, /* declaringType */
          new AttributeList(2),
          TypeFlags.Sealed,
          ContractNodes.ContractNamespace,
          Identifier.For("RuntimeContractsFlags"),
          new InterfaceList(),
          new MemberList());
        runtimeContractsFlags.Attributes.Add(flagsAttribute);
        RewriteHelper.TryAddCompilerGeneratedAttribute(runtimeContractsFlags);
        runtimeContractsFlags.UnderlyingType = SystemTypes.Int32;

        Type copyFrom = typeof(RuntimeContractEmitFlags);
        foreach (System.Reflection.FieldInfo fi in copyFrom.GetFields())
        {
          if (fi.IsLiteral)
          {
            AddEnumValue(runtimeContractsFlags, fi.Name, fi.GetRawConstantValue());
          }
        }
        assembly.Types.Add(runtimeContractsFlags);

      }


      InstanceInitializer ctor = (RuntimeContractsAttributeClass == null) ? null : RuntimeContractsAttributeClass.GetConstructor(runtimeContractsFlags);

      if (RuntimeContractsAttributeClass == null)
      {
        RuntimeContractsAttributeClass = new Class(assembly,
          null, /* declaringType */
          new AttributeList(),
          TypeFlags.Sealed,
          ContractNodes.ContractNamespace,
          Identifier.For("RuntimeContractsAttribute"),
          SystemTypes.Attribute,
          new InterfaceList(),
          new MemberList(0));

        RewriteHelper.TryAddCompilerGeneratedAttribute(RuntimeContractsAttributeClass);
        assembly.Types.Add(RuntimeContractsAttributeClass);
      }
      if (ctor == null) {

        Block returnBlock = new Block(new StatementList(new Return()));

        Block body = new Block(new StatementList());
        Block b = new Block(new StatementList());
        ParameterList pl = new ParameterList();
        Parameter levelParameter = new Parameter(Identifier.For("contractFlags"), runtimeContractsFlags);
        pl.Add(levelParameter);

        ctor = new InstanceInitializer(RuntimeContractsAttributeClass, null, pl, body);
        ctor.Flags = MethodFlags.Assembly | MethodFlags.HideBySig | MethodFlags.SpecialName | MethodFlags.RTSpecialName;

        Method baseCtor = SystemTypes.Attribute.GetConstructor();

        b.Statements.Add(new ExpressionStatement(new MethodCall(new MemberBinding(null, baseCtor), new ExpressionList(ctor.ThisParameter))));
        b.Statements.Add(returnBlock);
        body.Statements.Add(b);

        RuntimeContractsAttributeClass.Members.Add(ctor);
      }

      return ctor;
    }

    private static void AddEnumValue(EnumNode enumType, string name, object value)
    {
      var enumValue = new Field(enumType, null, FieldFlags.Assembly | FieldFlags.HasDefault | FieldFlags.Literal | FieldFlags.Static, Identifier.For(name), enumType, new Literal(value, SystemTypes.Int32));
      enumType.Members.Add(enumValue);
    }
    #endregion

    /// <summary>
    /// Inserts source strings for Asserts/Assumes (or removes checks based on level)
    /// Inserts call-site requires checks based on flags.
    /// </summary>
    internal sealed class RewriteAssertAssumeAndCallSiteRequires : StandardVisitor
    {
      Dictionary<Method, bool> methodTable;
      RuntimeContractMethods runtimeContracts;
      RuntimeContractEmitFlags emitFlags;
      Rewriter parent;
      Method containingMethod;
      private SourceContext lastStmtContext;

      public RewriteAssertAssumeAndCallSiteRequires(Dictionary<Method, bool> replacementMethods, RuntimeContractMethods runtimeContracts, RuntimeContractEmitFlags emitFlags, Rewriter parent,
                                    Method containingMethod)
      {
        this.methodTable = replacementMethods;
        this.runtimeContracts = runtimeContracts;
        this.emitFlags = emitFlags;
        this.parent = parent;
        this.containingMethod = containingMethod;
      }
      // Requires:
      //  statement.Expression is MethodCall
      //  statement.Expression.Callee is MemberBinding
      //  statement.Expression.Callee.BoundMember is Method
      //  statement.Expression.Callee.BoundMember == "Assert" or "Assume"
      //
      //  replacementMethod.ReturnType == methodToReplace.ReturnType
      //  && replacementMethod.Parameters.Count == 1
      //  && methodToReplace.Parameters.Count == 1
      //  && replacementMethod.Parameters[0].Type == methodToReplace.Parameters[0].Type
      //
      private static Statement RewriteContractCall(
        MethodCall call,
        ExpressionStatement statement,
        Method/*!*/ methodToReplace,
        Method/*?*/ replacementMethod
        )
      {
        Contract.Requires(call != null);

        Debug.Assert(call == statement.Expression as MethodCall);

        MemberBinding mb = (MemberBinding)call.Callee;
        Debug.Assert(mb.BoundMember == methodToReplace);

        mb.BoundMember = replacementMethod;

        if (call.Operands.Count == 1)
        {
          call.Operands.Add(Literal.Null);
        }

#if false
        SourceContext sctx = statement.SourceContext;
        if (sctx.IsValid && sctx.Document.Text != null && sctx.Document.Text.Source != null)
        {
          call.Operands.Add(new Literal(sctx.Document.Text.Source, SystemTypes.String));
        }
#else
        ContractAssumeAssertStatement casas = statement as ContractAssumeAssertStatement;
        if (casas != null) {
          call.Operands.Add(new Literal(casas.SourceText, SystemTypes.String));
        }
#endif
        else {
          call.Operands.Add(Literal.Null);
          //call.Operands.Add(new Literal("No other information available", SystemTypes.String));
        }

        return statement;
      }

      public override StatementList VisitStatementList(StatementList statements)
      {
        if (statements == null) return null;
        for (int i = 0; i < statements.Count; i++)
        {
          var stmt = statements[i];
          if (stmt == null) continue;
          if (stmt.SourceContext.IsValid)
          {
            this.lastStmtContext = stmt.SourceContext;
          }
          statements[i] = (Statement)base.Visit(stmt);
        }
        return statements;
      }

      public override Statement VisitExpressionStatement(ExpressionStatement statement)
      {
        MethodCall call;
        Method contractMethod = Rewriter.ExtractCallFromStatement(statement, out call);
        if (contractMethod == null)
        {
          return base.VisitExpressionStatement(statement);
        }
        Method keyToLookUp = contractMethod;
        if (this.methodTable.ContainsKey(keyToLookUp))
        {
          var isAssert = keyToLookUp.Name.Name == "Assert";
          Method repMethod;
          if (isAssert)
          {
            repMethod = this.runtimeContracts.AssertMethod;
          }
          else
          {
            // assume it is Assume
            repMethod = this.runtimeContracts.AssumeMethod;
          }
          if (repMethod == null)
          {
            return base.VisitExpressionStatement(statement);
          }
          if (isAssert && (!this.emitFlags.Emit(RuntimeContractEmitFlags.Asserts) || this.runtimeContracts.PublicSurfaceOnly))
          {
            var pops = CountPopExpressions.Count(call);
            if (pops > 0)
            {
              var block = new Block(new StatementList(pops));
              while (pops-- > 0)
              {
                // emit a pop
                block.Statements.Add(new ExpressionStatement(new UnaryExpression(null, NodeType.Pop)));
              }
              return block;
            }
            return null;
          }
          if (!isAssert && (!this.emitFlags.Emit(RuntimeContractEmitFlags.Assumes) || this.runtimeContracts.PublicSurfaceOnly))
          {
            var pops = CountPopExpressions.Count(call);
            if (pops > 0)
            {
              var block = new Block(new StatementList(pops));
              while (pops-- > 0)
              {
                // emit a pop
                block.Statements.Add(new ExpressionStatement(new UnaryExpression(null, NodeType.Pop)));
              }
              return block;
            }
            return null;
          }
          var result =
            RewriteAssertAssumeAndCallSiteRequires.RewriteContractCall(
            call,
            statement,
            contractMethod,
            repMethod
          );
 
          return result;
        }
        else
        {
          return base.VisitExpressionStatement(statement);
        }
      }

      public override Expression VisitMethodCall(MethodCall call)
      {
        // if call-site requires are needed, create wrapper types here:
        MemberBinding mb = call.Callee as MemberBinding;
        if (mb != null)
        {
          Method m = mb.BoundMember as Method;
          if (m != null)
          {
            bool virtcall = call.NodeType == NodeType.Callvirt;
            Method methodWithContract;
            if (parent.NeedCallSiteRequires(m, virtcall, out methodWithContract))
            {
              // materialize generic wrapper type
              var wrapped = parent.WrapperMethod(m, virtcall, call.Constraint, containingMethod, methodWithContract, this.lastStmtContext);
              if (wrapped != null)
              {
                if (IsProtected(wrapped))
                {
                  // wrapper was produced in same declaring type, still an instance call with same parameters
                  call.Callee = new MemberBinding(mb.TargetObject, wrapped);
                }
                else
                {
                  // wrapper is now static in a wrapper class
                  call.Callee = new MemberBinding(null, wrapped);
                  call.NodeType = NodeType.Call;
                  call.Constraint = null;
                  if (mb.TargetObject != null)
                  {
                    // instance into static, need to add target object as first parameter
                    var origargcount = call.Operands == null ? 0 : call.Operands.Count;
                    var elist = new ExpressionList(origargcount + 1);
                    elist.Add(mb.TargetObject);
                    for (int i = 0; i < origargcount; i++)
                    {
                      elist.Add(call.Operands[i]);
                    }
                    call.Operands = elist;
                  }
                }
              }
            }
          }
        }
        return base.VisitMethodCall(call);
      }
    }


    #region Call-site Requires instrumentation

    private Dictionary<string, TypeNode> wrapperTypes = new Dictionary<string, TypeNode>();
    private Dictionary<int, Method> constrainedVirtualWrapperMethods = new Dictionary<int, Method>();
    private Dictionary<int, Method> virtualWrapperMethods = new Dictionary<int, Method>();
    private Dictionary<int, Method> nonVirtualWrapperMethods = new Dictionary<int, Method>();

    /// <summary>
    /// Return or produce a wrapper type for t
    /// If t is an instance, we only produce types for the underlying generic type (including parent types)
    /// </summary>
    private TypeNode WrapperType(TypeNode t)
    {
      while (t.Template != null) t = t.Template;

      if (t.DeclaringType != null)
      {
        var parent = WrapperType(t.DeclaringType);
        var nested = parent.GetNestedType(t.Name);
        if (nested != null) return nested;
        // generate nested type
        nested = CreateWrapperType(t, parent);
        parent.Members.Add(nested);
        return nested;
      }
      TypeNode candidate;
      if (this.wrapperTypes.TryGetValue(t.FullName, out candidate))
      {
        return candidate;
      }
      var @namespace = CombineNamespace("System.Diagnostics.Contracts.Wrappers", t.Namespace);

      candidate = CreateWrapperType(t, null);
      candidate.Namespace = @namespace;
      this.wrapperTypes.Add(t.FullName, candidate);
      return candidate;
    }

    private static Identifier CombineNamespace(string prefix, Identifier @namespace)
    {
      if (@namespace == null || String.IsNullOrEmpty(@namespace.Name)) return Identifier.For(prefix);
      if (String.IsNullOrEmpty(prefix)) return @namespace;

      return Identifier.For(prefix + "." + @namespace.Name);
    }

    private TypeNode CreateWrapperType(TypeNode original, TypeNode declaringWrapper/*?*/)
    {
      TypeNode wrapper = null;
      switch (original.NodeType)
      {
        case NodeType.Class:
          wrapper = CreateWrapperClass((Class)original);
          break;

        case NodeType.Struct:
          wrapper = CreateWrapperStruct((Struct)original);
          break;

        case NodeType.Interface:
          wrapper = CreateWrapperInterface((Interface)original);
          break;

        default:
          throw new Exception("Don' know how to produce a wrapper type for " + original.NodeType.ToString());
      }
      if (declaringWrapper != null) {
        wrapper.DeclaringType = declaringWrapper;
      }
      // need to specialize w.r.t type parameters due to type parameter bounds referring to each other
      var origConsolidatedTemplateParameters = original.ConsolidatedTemplateParameters;
      var wrapperConsolideateTemplateParameters = wrapper.ConsolidatedTemplateParameters;
      if (origConsolidatedTemplateParameters != null && origConsolidatedTemplateParameters.Count > 0) {
        var spec = new Specializer(this.assemblyBeingRewritten, origConsolidatedTemplateParameters, wrapperConsolideateTemplateParameters);
        spec.VisitTypeParameterList(wrapper.TemplateParameters);
      }
      return wrapper;
    }

    private Class CreateWrapperInterface(Interface intf)
    {
      var flags = WrapperTypeFlags(intf);
      var wrapper = new Class(this.assemblyBeingRewritten, null, null, flags, null, intf.Name, SystemTypes.Object, null, null);
      RewriteHelper.TryAddCompilerGeneratedAttribute(wrapper);
      if (intf.TemplateParameters != null)
      {
        Duplicator d = new Duplicator(this.assemblyBeingRewritten, wrapper);
        d.FindTypesToBeDuplicated(intf.TemplateParameters);
        var templateParams = CopyTypeParameterList(wrapper, intf, d);
        wrapper.TemplateParameters = templateParams;
        wrapper.IsGeneric = true;
      }
      return wrapper;
    }

    // We create classes here because we don't want any fields etc...
    private Class CreateWrapperStruct(Struct s)
    {
      var flags = WrapperTypeFlags(s);
      var wrapper = new Class(this.assemblyBeingRewritten, null, null, flags, null, s.Name, SystemTypes.Object, null, null);
      RewriteHelper.TryAddCompilerGeneratedAttribute(wrapper);
      if (s.TemplateParameters != null)
      {
        Duplicator d = new Duplicator(this.assemblyBeingRewritten, wrapper);
        d.FindTypesToBeDuplicated(s.TemplateParameters);
        var templateParams = CopyTypeParameterList(wrapper, s, d);
        wrapper.TemplateParameters = templateParams;
        wrapper.IsGeneric = true;
      }
      return wrapper;
    }

    private Class CreateWrapperClass(Class c)
    {
      var flags = WrapperTypeFlags(c);
      var wrapper = new Class(this.assemblyBeingRewritten, null, null, flags, null, c.Name, SystemTypes.Object, null, null);
      RewriteHelper.TryAddCompilerGeneratedAttribute(wrapper);
      if (c.TemplateParameters != null)
      {
        Duplicator d = new Duplicator(this.assemblyBeingRewritten, wrapper);
        d.FindTypesToBeDuplicated(c.TemplateParameters);
        var templateParams = CopyTypeParameterList(wrapper, c, d);
        wrapper.TemplateParameters = templateParams;
        wrapper.IsGeneric = true;
      }
      return wrapper;
    }

    private static TypeFlags WrapperTypeFlags(TypeNode c)
    {
      var flags = c.Flags & ~(TypeFlags.Interface | TypeFlags.HasSecurity | TypeFlags.VisibilityMask) | TypeFlags.Abstract | TypeFlags.Sealed;
      if (c.DeclaringType != null)
      {
        flags |= TypeFlags.NestedPublic; // make accessible internally
      }
      return flags;
    }

    private static TypeNodeList CopyTypeParameterList(TypeNode target, TypeNode source, Duplicator d)
    {
      var result = d.VisitTypeParameterList(source.TemplateParameters);
      foreach (var tp in result)
      {

        var itp = (ITypeParameter)tp;

        itp.DeclaringMember = target;
        itp.TypeParameterFlags = itp.TypeParameterFlags & ~TypeParameterFlags.VarianceMask;
      }
      return result;
    }


    /// <summary>
    /// Determines if a call to m needs call-site instrumentation of requires
    /// </summary>
    internal bool NeedCallSiteRequires(Method m, bool virtcall, out Method methodWithContract)
    {
      if (!runtimeContracts.CallSiteRequires) {
        methodWithContract = null;
        return false;
      }

      if (m.Template != null) m = m.Template;
      if (!HasRequiresContracts(m, out methodWithContract)) return false;

      // approximate for now.
      var declaringType = m.DeclaringType;
      if (declaringType.DeclaringModule != this.assemblyBeingRewritten)
      {
        return true;
      }
      if (declaringType.IsVisibleOutsideAssembly)
      {
        return virtcall && m.IsVirtual && !m.IsAssembly;
      }
      return false;
    }

    internal Method WrapperMethod(Method instanceMethod, bool virtcall, TypeNode constraint, Method methodContainingCall, Method methodWithContract, SourceContext callingContext)
    {
      // for now disable the following
      //  - protected methods
      //  - constructors
      //  - base calls
      if (IsProtected(instanceMethod)) return null;
      if (instanceMethod is InstanceInitializer) return null;
      if (!virtcall && instanceMethod.IsVirtual) return null;

      var templateMethod = instanceMethod;
      while (templateMethod.Template != null) templateMethod = templateMethod.Template;

      var templateType = templateMethod.DeclaringType;
      TypeNode wrapperType;
      if (IsProtected(instanceMethod)) {
        // can only be called on "this" of derived type, so no wrapper class
        wrapperType = methodContainingCall.DeclaringType;
        while (wrapperType.Template != null) { wrapperType = wrapperType.Template; }
      }
      else {
        wrapperType = WrapperType(templateType);
      }
      var wrapperMethod = LookupWrapperMethod(virtcall, constraint, wrapperType, templateMethod);
      if (wrapperMethod == null) {
        wrapperMethod = CreateWrapperMethod(virtcall, constraint, templateMethod, templateType, wrapperType, methodWithContract, instanceMethod, callingContext);
        StoreWrapperMethod(virtcall, constraint, templateMethod, wrapperMethod);
      }
      return InstantiateWrapperMethod(constraint, wrapperMethod, instanceMethod);
    }

    private static bool IsProtected(Method instanceMethod)
    {
      return instanceMethod.IsFamily || instanceMethod.IsFamilyAndAssembly || instanceMethod.IsFamilyOrAssembly;
    }

    /// <summary>
    /// properly instantiate wrapper according to original instance (including declaring type and method instance)
    /// </summary>
    private Method InstantiateWrapperMethod(TypeNode virtcallConstraint, Method wrapperMethod, Method originalInstanceMethod)
    {
      // if CCI does it for us, great. Otherwise, we have to fake it:
      var instWrapperType = wrapperMethod.DeclaringType;
      if (instWrapperType.TemplateParameters != null && instWrapperType.TemplateParameters.Count > 0) {
        instWrapperType = instWrapperType.GetGenericTemplateInstance(this.assemblyBeingRewritten, originalInstanceMethod.DeclaringType.ConsolidatedTemplateArguments);
      }


      Method instMethod = GetMethodInstanceReference(wrapperMethod, instWrapperType);
      // instantiate method if generic
      if (instMethod.TemplateParameters != null && instMethod.TemplateParameters.Count > 0) {
        if (virtcallConstraint != null) {
          TypeNodeList args = new TypeNodeList();
          var templateArgCount = (originalInstanceMethod.TemplateArguments == null) ? 0 : originalInstanceMethod.TemplateArguments.Count;
          for (int i = 0; i < templateArgCount; i++) {
            args.Add(originalInstanceMethod.TemplateArguments[i]);
          }
          args.Add(virtcallConstraint);
          instMethod = instMethod.GetTemplateInstance(instWrapperType, args);
        }
        else {
          instMethod = instMethod.GetTemplateInstance(instWrapperType, originalInstanceMethod.TemplateArguments);
        }
      }
      return instMethod;
    }

    internal static Method GetMethodInstanceReference(Method methodOfGenericType, TypeNode instantiatedParentType)
    {
      //F:
      Contract.Requires(methodOfGenericType != null);
      Contract.Requires(instantiatedParentType != null);

      return (Method)GetMemberInstanceReference(methodOfGenericType, instantiatedParentType);
#if false
      int index = MemberIndexInTypeMembers(methodOfGenericType);

      // now try to find same index in instance wrapper type

      Method instMethod = null;
      var members = instantiatedParentType.Members;
      if (index < members.Count)
      {
        instMethod = members[index] as Method;
        Debug.Assert(instMethod == null || instMethod.Name.UniqueIdKey == methodOfGenericType.Name.UniqueIdKey);
      }
      if (instMethod == null)
      {
        // instantiation order did not work out, so we need to fake it.
        Duplicator dup = new Duplicator(this.assemblyBeingRewritten, instantiatedParentType);
        dup.RecordOriginalAsTemplate = true;
        instMethod = dup.VisitMethod(methodOfGenericType);
        var spec = TypeParameterSpecialization(methodOfGenericType.DeclaringType, instantiatedParentType);
        instMethod = spec.VisitMethod(instMethod);
        instMethod.DeclaringType = instantiatedParentType;
        AddMemberAtIndex(index, instantiatedParentType, instMethod);
      }
      return instMethod;
#endif
    }

    internal static Field GetFieldInstanceReference(Field fieldOfGenericType, TypeNode instantiatedParentType) {
      return (Field)GetMemberInstanceReference(fieldOfGenericType, instantiatedParentType);
    }

    internal static Member GetMemberInstanceReference(Member memberOfGenericType, TypeNode instantiatedParentType)
    {
      // F:
      Contract.Requires(memberOfGenericType != null);
      Contract.Requires(instantiatedParentType != null);

      if (instantiatedParentType.Template == null) { return memberOfGenericType; }

      int index = MemberIndexInTypeMembers(memberOfGenericType);

      // now try to find same index in instance wrapper type

      Member instMember = null;
      var members = instantiatedParentType.Members;
      if (index < members.Count)
      {
        instMember = members[index];
        //F: 
        Contract.Assume(memberOfGenericType.Name != null);
        Debug.Assert(instMember == null || instMember.Name.UniqueIdKey == memberOfGenericType.Name.UniqueIdKey);
      }
      if (instMember == null)
      {
        // F:
        Contract.Assume(memberOfGenericType.DeclaringType != null);

        // instantiation order did not work out, so we need to fake it.
        Duplicator dup = new Duplicator(memberOfGenericType.DeclaringType.DeclaringModule, instantiatedParentType);
        dup.RecordOriginalAsTemplate = true;
        instMember = (Member)dup.Visit(memberOfGenericType);
        var spec = TypeParameterSpecialization(memberOfGenericType.DeclaringType, instantiatedParentType);
        instMember = (Member)spec.Visit(instMember);
        instMember.DeclaringType = instantiatedParentType;
        AddMemberAtIndex(index, instantiatedParentType, instMember);
      }
      return instMember;
    }

    internal static void AddMemberAtIndex(int index, TypeNode instWrapperType, Member instMember)
    {
      var members = instWrapperType.Members;
      while (index >= members.Count)
      {
        members.Add(null); // pad members with null entries
      }
      members[index] = instMember;
    }

    internal static int MemberIndexInTypeMembers(Member member)
    {
      // F:
      Contract.Requires(member != null);
      Contract.Ensures(Contract.Result<int>() >= 0);

      // find index of generic method first:
      var members = member.DeclaringType.Members;
      var index = -1;
      for (int i = 0; i < members.Count; i++) {
        if (members[i] == member) {
          index = i;
          break;
        }
      }
      // F:
      //Debug.Assert(index >= 0, "Can't find original member");
      Contract.Assume(index >= 0, "Can't find original member");
      return index;
    }

    private static Specializer TypeParameterSpecialization(TypeNode genericType, TypeNode atInstanceType)
    {
      // F:
      Contract.Requires(genericType != null);
      Contract.Requires(atInstanceType != null);

      TypeNodeList formals = new TypeNodeList();
      TypeNodeList actuals = new TypeNodeList();

      var wrapperTypeParams = genericType.ConsolidatedTemplateParameters;
      var instanceTypeParams = atInstanceType.ConsolidatedTemplateArguments;
      if (wrapperTypeParams != null && wrapperTypeParams.Count > 0) {
        for (int i = 0; i < wrapperTypeParams.Count; i++) {
          formals.Add(wrapperTypeParams[i]);
          actuals.Add(instanceTypeParams[i]);
        }
      }

#if false // method specs
      if (includeMethodSpec) {
        var wrapperMethodParams = wrapperMethod.TemplateParameters;
        var instanceMethodParams = originalInstanceMethod.TemplateArguments;
        if (wrapperMethodParams != null && wrapperMethodParams.Count > 0) {
          for (int i = 0; i < wrapperMethodParams.Count; i++) {
            formals.Add(wrapperMethodParams[i]);
            actuals.Add(instanceMethodParams[i]);
          }
        }
      }
#endif 
      if (formals.Count > 0) {
        return new Specializer(genericType.DeclaringModule, formals, actuals);
      }
      return null;
    }

    private Method CreateWrapperMethod(bool virtcall, TypeNode virtcallConstraint, Method templateMethod, TypeNode templateType, TypeNode wrapperType, Method methodWithContract, Method instanceMethod, SourceContext callingContext)
    {
      bool isProtected = IsProtected(templateMethod);
      Identifier name = templateMethod.Name;
      if (virtcall) {
        if (virtcallConstraint != null) {
          name = Identifier.For("CV$" + name.Name);
        }
        else {
          name = Identifier.For("V$" + name.Name);
        }
      }
      else {
        name = Identifier.For("NV$" + name.Name);
      }
      Duplicator dup = new Duplicator(this.assemblyBeingRewritten, wrapperType);
      TypeNodeList typeParameters = null;
      TypeNodeList typeParameterFormals = new TypeNodeList();
      TypeNodeList typeParameterActuals = new TypeNodeList();

      if (templateMethod.TemplateParameters != null) {
        dup.FindTypesToBeDuplicated(templateMethod.TemplateParameters);
        typeParameters = dup.VisitTypeParameterList(templateMethod.TemplateParameters);
        for (int i = 0; i < typeParameters.Count; i++) {
          typeParameterFormals.Add(typeParameters[i]);
          typeParameterActuals.Add(templateMethod.TemplateParameters[i]);
        }
      }
      ITypeParameter constraintTypeParam = null;
      if (virtcallConstraint != null) {
        if (typeParameters == null) { typeParameters = new TypeNodeList(); }
        var constraint = templateMethod.DeclaringType;
        var classConstraint = constraint as Class;
        if (classConstraint != null) {
          var classParam = new MethodClassParameter();
          classParam.BaseClass = classConstraint;
          classParam.Name = Identifier.For("TC");
          classParam.DeclaringType = wrapperType;
          typeParameters.Add(classParam);
          constraintTypeParam = classParam;
        }
        else {
          var mtp = new MethodTypeParameter();
          Interface intf = constraint as Interface;
          if (intf != null) {
            mtp.Interfaces.Add(intf);
          }
          mtp.Name = Identifier.For("TC");
          mtp.DeclaringType = wrapperType;
          typeParameters.Add(mtp);
          constraintTypeParam = mtp;
        }
      }
      var consolidatedTemplateTypeParameters = templateType.ConsolidatedTemplateParameters;
      if (consolidatedTemplateTypeParameters != null && consolidatedTemplateTypeParameters.Count > 0) {
        var consolidatedWrapperTypeParameters = wrapperType.ConsolidatedTemplateParameters;
        for (int i = 0; i < consolidatedTemplateTypeParameters.Count; i++) {
          typeParameterFormals.Add(consolidatedWrapperTypeParameters[i]);
          typeParameterActuals.Add(consolidatedTemplateTypeParameters[i]);
        }
      }
      Specializer spec = null;
      if (typeParameterActuals.Count > 0) {
        spec = new Specializer(this.assemblyBeingRewritten, typeParameterActuals, typeParameterFormals);
      }
      var parameters = new ParameterList();
      var asTypeConstraintTypeParam = constraintTypeParam as TypeNode;

      if (!isProtected && !templateMethod.IsStatic) {
        TypeNode thisType = GetThisTypeInstance(templateType, wrapperType, asTypeConstraintTypeParam);
        parameters.Add(new Parameter(Identifier.For("@this"), thisType));
      }
      for (int i = 0; i < templateMethod.Parameters.Count; i++) {
        parameters.Add((Parameter)dup.VisitParameter(templateMethod.Parameters[i]));
      }
      var retType = dup.VisitTypeReference(templateMethod.ReturnType);
      if (spec != null) {
        parameters = spec.VisitParameterList(parameters);
        retType = spec.VisitTypeReference(retType);
      }

      var wrapperMethod = new Method(wrapperType, null, name, parameters, retType, null);
      RewriteHelper.TryAddCompilerGeneratedAttribute(wrapperMethod);

      if (isProtected) {
        wrapperMethod.Flags = templateMethod.Flags & ~MethodFlags.Abstract;
        wrapperMethod.CallingConvention = templateMethod.CallingConvention;
      }
      else {
        wrapperMethod.Flags |= MethodFlags.Static | MethodFlags.Assembly;
      }
      if (constraintTypeParam != null) {
        constraintTypeParam.DeclaringMember = wrapperMethod;
      }
      if (typeParameters != null) {
        if (spec != null) {
          typeParameters = spec.VisitTypeParameterList(typeParameters);
        }
        wrapperMethod.IsGeneric = true;
        wrapperMethod.TemplateParameters = typeParameters;
      }

      // create body
      var sl = new StatementList();
      Block b = new Block(sl);

      // insert requires
      AddRequiresToWrapperMethod(wrapperMethod, b, methodWithContract);

      // create original call
      var targetType = templateType;
      if (isProtected)
      {
        // need to use base chain instantiation of target type.
        targetType = instanceMethod.DeclaringType;
      }
      else
      {
        if (targetType.ConsolidatedTemplateParameters != null && targetType.ConsolidatedTemplateParameters.Count > 0)
        {
          // need selfinstantiation
          targetType = targetType.GetGenericTemplateInstance(this.assemblyBeingRewritten, wrapperType.ConsolidatedTemplateParameters);
        }
      }
      Method targetMethod = GetMatchingMethod(targetType, templateMethod, wrapperMethod);
      if (targetMethod.IsGeneric) {
        if (typeParameters.Count > targetMethod.TemplateParameters.Count) {
          // omit the extra constrained type arg.
          TypeNodeList origArgs = new TypeNodeList();
          for (int i = 0; i < targetMethod.TemplateParameters.Count; i++) {
            origArgs.Add(typeParameters[i]);
          }
          targetMethod = targetMethod.GetTemplateInstance(wrapperType, origArgs);
        }
        else {
          targetMethod = targetMethod.GetTemplateInstance(wrapperType, typeParameters);
        }
      }
      MethodCall call;
      NodeType callType = virtcall ? NodeType.Callvirt : NodeType.Call;
      if (isProtected) {
        var mb = new MemberBinding(wrapperMethod.ThisParameter, targetMethod);
        var elist = new ExpressionList(wrapperMethod.Parameters.Count);
        for (int i = 0; i < wrapperMethod.Parameters.Count; i++) {
          elist.Add(wrapperMethod.Parameters[i]);
        }
        call = new MethodCall(mb, elist, callType);
      }
      else if (templateMethod.IsStatic) {
        var elist = new ExpressionList(wrapperMethod.Parameters.Count);
        for (int i = 0; i < wrapperMethod.Parameters.Count; i++) {
          elist.Add(wrapperMethod.Parameters[i]);
        }
        call = new MethodCall(new MemberBinding(null, targetMethod), elist, callType);
      }
      else {
        var mb = new MemberBinding(wrapperMethod.Parameters[0], targetMethod);
        var elist = new ExpressionList(wrapperMethod.Parameters.Count - 1);
        for (int i = 1; i < wrapperMethod.Parameters.Count; i++) {
          elist.Add(wrapperMethod.Parameters[i]);
        }
        call = new MethodCall(mb, elist, callType);
      }
      if (constraintTypeParam != null) {
        call.Constraint = asTypeConstraintTypeParam;
      }
      if (HelperMethods.IsVoidType(templateMethod.ReturnType)) {
        sl.Add(new ExpressionStatement(call,callingContext));
        sl.Add(new Return(callingContext));
      }
      else {
        sl.Add(new Return(call,callingContext));
      }
      wrapperMethod.Body = b;

      wrapperType.Members.Add(wrapperMethod);
      return wrapperMethod;
    }

    private void AddRequiresToWrapperMethod(Method wrapper, Block body, Method contractMethod)
    {
      // need to use instantiation of contractMethod at wrapper self instantiation to make this work.
      
      List<Requires> preconditions = new List<Requires>();
      MethodContract mc=HelperMethods.DuplicateContractAndClosureParts(wrapper, contractMethod, this.runtimeContracts.ContractNodes, false);
      RewriteHelper.ReplacePrivateFieldsThatHavePublicProperties(wrapper.DeclaringType, contractMethod.DeclaringType, mc, this.rewriterNodes);
      if (mc != null) {
        if (mc.Requires != null)
        {
          foreach (RequiresPlain requires in mc.Requires)
          {
            //if (requires is RequiresOtherwise && !Emit(RuntimeContractEmitFlags.LegacyRequires)) continue;
            if (requires.IsWithException)
            {
              if (!Emit(RuntimeContractEmitFlags.RequiresWithException)) continue;
            }
            else
            {
            if (!Emit(RuntimeContractEmitFlags.Requires)) continue;
            }
            preconditions.Add(requires);
          }
        }

#if false // Needed when we add post conditions
        Local l = null;
        if (HelperMethods.IsClosureInitialization(closureInitializerBlock, out l))
        {
          if (l != null) closureLocals.Add(l.Type, l);
        }
#endif
      }
      // Emit closure initializer
      body.Statements.Add(mc.ContractInitializer);
      EmitRecursionGuardAroundChecks(wrapper, body, EmitRequiresList(preconditions));
    }

    private static Method GetMatchingMethod(TypeNode targetType, Method templateMethod, Method wrapperMethod)
    {
      var argCount = templateMethod.Parameters == null?0:templateMethod.Parameters.Count;
      TypeNode[] argTypes = new TypeNode[argCount];
      var argOffset = 0;
      if (!IsProtected(templateMethod) && !templateMethod.IsStatic) {
        argOffset = 1;
      }
      for (int i = 0; i < argCount; i++) {
        argTypes[i] = wrapperMethod.Parameters[i + argOffset].Type;
      }
      var methods = targetType.GetMethods(templateMethod.Name, argTypes);
      for (int i = 0; i < methods.Count; i++) {
        if (TypeParameterCount(methods[i]) == TypeParameterCount(templateMethod)) {
          return methods[i];
        }
      }
      Debug.Assert(false, "Wrapper instance method not found");
      return null;
    }



    private TypeNode GetThisTypeInstance(TypeNode templateType, TypeNode wrapperType, TypeNode constraintTypeParam)
    {
      if (constraintTypeParam != null) {
        return constraintTypeParam.GetReferenceType();
      }
      TypeNode thisType = templateType;
      if (thisType.TemplateParameters != null && thisType.TemplateParameters.Count > 0) {
        // need selfinstantiation
        thisType = thisType.GetGenericTemplateInstance(this.assemblyBeingRewritten, wrapperType.ConsolidatedTemplateParameters);
      }

      if (templateType.IsValueType) {
        thisType = thisType.GetReferenceType();
      }
      return thisType;
    }

    private void StoreWrapperMethod(bool virtcall, TypeNode virtCallConstraint, Method templateMethod, Method wrapperMethod)
    {
      if (virtcall) {
        if (virtCallConstraint != null) {
          this.constrainedVirtualWrapperMethods.Add(templateMethod.UniqueKey, wrapperMethod);
        }
        else {
          this.virtualWrapperMethods.Add(templateMethod.UniqueKey, wrapperMethod);
        }
      }
      else {
        this.nonVirtualWrapperMethods.Add(templateMethod.UniqueKey, wrapperMethod);
      }
    }

    private Method LookupWrapperMethod(bool virtcall, TypeNode virtCallConstraint, TypeNode wrapperType, Method templateMethod)
    {
      Method result = null; ;
      if (virtcall) {
        if (virtCallConstraint != null) {
          this.constrainedVirtualWrapperMethods.TryGetValue(templateMethod.UniqueKey, out result);
        }
        else {
          this.virtualWrapperMethods.TryGetValue(templateMethod.UniqueKey, out result);
        }
      }
      else {
        this.nonVirtualWrapperMethods.TryGetValue(templateMethod.UniqueKey, out result);
      }
      return result;
    }

    private static int TypeParameterCount(Method method)
    {
      if (method.TemplateParameters == null) return 0;
      return method.TemplateParameters.Count;
    }

  }

    #endregion


  /// <summary>
  /// The Extractor has removed all dependencies on whatever contract class was
  /// used in the assembly. (See the class ExtractAllContractNodesDependencies.)
  /// 
  /// This class exists just to make sure that those pseudo-nodes are replaced with
  /// things the Writer won't barf on.
  /// For instance, don't leave any ReturnValue or OldExpression ASTs in the assembly.
  /// </summary>
  internal sealed class CleanUpOldAndResult : StandardVisitor {
    public CleanUpOldAndResult() {
    }
    public override Expression VisitOldExpression(OldExpression oldExpression) {
      //Debug.Assert(false, "old was not substituted");
      TypeNode returnType = oldExpression.Type;
      if (returnType.IsValueType)
        return new Literal(0, returnType);
      else
        return new Literal(null, returnType);
    }
    public override Expression VisitReturnValue(ReturnValue returnValue) {
      // return a default value of the same type as the return value
      TypeNode returnType = returnValue.Type;
      ITypeParameter itp = returnType as ITypeParameter;
      if (itp != null) {
        Local loc = new Local(returnType);
        UnaryExpression loca = new UnaryExpression(loc, NodeType.AddressOf, loc.Type.GetReferenceType());
        StatementList statements = new StatementList(2);
        statements.Add(new AssignmentStatement(new AddressDereference(loca, returnType, false, 0), new Literal(null, SystemTypes.Object)));
        statements.Add(new ExpressionStatement(loc));
        return new BlockExpression(new Block(statements), returnType);
      }
      if (returnType.IsValueType)
        return new Literal(0, returnType);
      else
        return new Literal(null, returnType);
    }
  }

  /// <summary>
  /// There is no need in the rewritten assembly for the contract classes.
  /// And they just cause problems if there is a disagreement between a
  /// contract class that is in the real assembly and the one in the reference
  /// assembly. (Admittedly a low probability event, but it has occurred in our
  /// hand-generated reference assemblies for the framework.)
  /// </summary>
  internal sealed class RemoveContractClasses : Inspector {
    public RemoveContractClasses() {
    }
    private static void ScrubAttributeList(AttributeList attributes) {
      if (attributes == null) return;
      for (int i = 0, n = attributes.Count; i < n; i++) {
        if (attributes[i] == null) continue;
        if (attributes[i].Type == null) continue;
        if (ContractNodes.ContractClassAttributeName.Matches(attributes[i].Type.Name)) 
        {
          attributes[i] = null;
        }
      }
    }
    public override void VisitTypeNode(TypeNode typeNode)
    {
      if (typeNode == null) return;
      ScrubAttributeList(typeNode.Attributes);
      this.VisitMemberList(typeNode.Members);
    }

    public override void VisitMemberList(MemberList members)
    {
      if (members == null) return;
      for (int i = 0, n = members.Count; i < n; i++)
      {
        var type = members[i] as TypeNode;

        if (type == null) continue;
        Class c = type as Class;
        if (c != null && HelperMethods.IsContractTypeForSomeOtherType(c))
        {
          members[i] = null;
        }
        else
        {
          // for nested types
          this.VisitTypeNode(type);
        }
      }
    }

    public override void VisitTypeNodeList(TypeNodeList types) {
      if (types == null) return;
      for (int i = 0, n = types.Count; i < n; i++) {
        var type = types[i];
        if (type == null) continue;
        Class c = type as Class;
        if (c != null && HelperMethods.IsContractTypeForSomeOtherType(c))
        {
          types[i] = null;
        }
        else
        {
          // for nested types
          this.VisitTypeNode(type);
        }
      }
    }
  }

  /// <summary>
  /// Application-level exceptions thrown by this component.
  /// </summary>
  public class RewriteException : Exception {
    /// <summary>
    /// Exception specific to an error occurring in the contract rewriter
    /// </summary>
    public RewriteException() { }
    /// <summary>
    /// Exception specific to an error occurring in the contract rewriter
    /// </summary>
    public RewriteException(string s) : base(s) { }
    /// <summary>
    /// Exception specific to an error occurring in the contract rewriter
    /// </summary>
    public RewriteException(string s, Exception inner) : base(s, inner) { }
    /// <summary>
    /// Exception specific to an error occurring in the contract rewriter
    /// </summary>
    public RewriteException(SerializationInfo info, StreamingContext context) : base(info, context) { }
  }

}
