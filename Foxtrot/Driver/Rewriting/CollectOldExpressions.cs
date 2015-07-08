using System.Collections.Generic;
using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    internal sealed class CollectOldExpressions : StandardVisitor
    {
        public Block PrestateValuesOfOldExpressions
        {
            get { return this.prestateValuesOfOldExpressions; }
        }

        private readonly Block prestateValuesOfOldExpressions;
        private readonly Dictionary<TypeNode, Local> closureLocals;
        private TypeNode topLevelClosureClass;
        private TypeNode currentClosureClass;
        private Field PointerToTopLevelClosureClass;

        private readonly ContractNodes contractNodes;

        private readonly List<MethodCall> stackOfMethods; // contains nested calls of ForAll and Exists methods

        private readonly List<Parameter> stackOfBoundVariables;
        // contains the parameters of the ForAll and Exists methods

        private readonly Module module;
        private readonly Method currentMethod;

        private int counter;

        public int Counter
        {
            get { return this.counter; }
        }

        public CollectOldExpressions(Module module, Method method, ContractNodes contractNodes, Dictionary<TypeNode, Local> closureLocals, 
            int localCounterStart, Class initialClosureClass)
            : this(module, method, contractNodes, closureLocals, localCounterStart)
        {
            this.topLevelClosureClass = initialClosureClass;
            this.currentClosureClass = initialClosureClass;
        }

        public CollectOldExpressions(Module module, Method method, ContractNodes contractNodes, Dictionary<TypeNode, Local> closureLocals,
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

        public override Expression VisitOldExpression(OldExpression oldExpression)
        {
            if (this.topLevelClosureClass != null)
            {
                // In Closure ==> Create a field

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

                SubstituteClosureClassWithinOldExpressions subst = new SubstituteClosureClassWithinOldExpressions(this.closureLocals);
                Expression e = subst.VisitExpression(oldExpression.expression);
                if (cbv.FoundVariables.Count == 0)
                {
                    // Use a scalar for the old variable

                    Local closureLocal;
                    if (!this.closureLocals.TryGetValue(this.topLevelClosureClass, out closureLocal))
                    {
                        Contract.Assume(false, "can't find closure local!");
                    }

                    // Define a scalar

                    var clTemplate = HelperMethods.Unspecialize(this.topLevelClosureClass);
                    Field f = new Field(clTemplate,
                        null,
                        FieldFlags.CompilerControlled | FieldFlags.Public,
                        Identifier.For("_old" + oldExpression.expression.UniqueKey.ToString()),
                        // unique name for this old expr.
                        oldExpression.Type,
                        null);

                    clTemplate.Members.Add(f);

                    // now produce properly instantiated field
                    f = (Field) Rewriter.GetMemberInstanceReference(f, this.topLevelClosureClass);

                    // Generate code to store value in prestate

                    this.prestateValuesOfOldExpressions.Statements.Add(
                        new AssignmentStatement(new MemberBinding(closureLocal, f), e));

                    // Return expression to be used in poststate

                    // Return an expression that will evaluate in the poststate to the value of the old
                    // expression in the prestate. This will be this.up.f where "up" is the field C#
                    // generated to point to the instance of the top-level closure class.
                    if (this.PointerToTopLevelClosureClass == null)
                    {
                        // then the old expression occurs in the top-level closure class. Just return "this.f"
                        // where "this" refers to the top-level closure class.
                        return new MemberBinding(new This(this.currentClosureClass), f);
                    }
                    else
                    {
                        return new MemberBinding(
                            new MemberBinding(new This(this.currentClosureClass), this.PointerToTopLevelClosureClass),
                            f);
                    }
                }
                else
                {
                    // the Old expression *does* depend upon at least one of the bound variable
                    // in a ForAll or Exists expression

                    // Use an indexed variable for the old variable

                    TypeNode oldVariableTypeDomain;

                    // Decide if domain is one-dimensional or not

                    bool oneDimensional = cbv.FoundVariables.Count == 1 && cbv.FoundVariables[0].Type.IsValueType;
                    if (oneDimensional)
                    {
                        // a one-dimensional old-expression can use the index variable directly
                        oldVariableTypeDomain = cbv.FoundVariables[0].Type;
                    }
                    else
                    {
                        oldVariableTypeDomain = SystemTypes.GenericList.GetTemplateInstance(this.module,
                            SystemTypes.Int32);
                    }

                    TypeNode oldVariableTypeRange = oldExpression.Type;
                    TypeNode oldVariableType = SystemTypes.GenericDictionary.GetTemplateInstance(this.module,
                        oldVariableTypeDomain, oldVariableTypeRange);
                    
                    Local closureLocal;
                    if (!this.closureLocals.TryGetValue(this.topLevelClosureClass, out closureLocal))
                    {
                        Contract.Assume(false, "can't find closure local");
                    }

                    // Define an indexed variable

                    var clTemplate = HelperMethods.Unspecialize(this.topLevelClosureClass);

                    Field f = new Field(clTemplate,
                        null,
                        FieldFlags.CompilerControlled | FieldFlags.Assembly,
                        // can't be private or protected because it needs to be accessed from inner (closure) classes that don't inherit from the class this field is added to.
                        Identifier.For("_old" + oldExpression.expression.UniqueKey.ToString()),
                        // unique name for this old expr.
                        oldVariableType,
                        null);

                    clTemplate.Members.Add(f);
                    
                    // instantiate f
                    f = (Field) Rewriter.GetMemberInstanceReference(f, closureLocal.Type);

                    // Generate code to initialize the indexed variable

                    Statement init = new AssignmentStatement(
                        new MemberBinding(closureLocal, f),
                        new Construct(new MemberBinding(null, oldVariableType.GetConstructor()), null));

                    this.prestateValuesOfOldExpressions.Statements.Add(init);

                    // Generate code to store values in prestate

                    // Create assignment: this.closure.f[i,j,k,...] = e;

                    Method setItem = oldVariableType.GetMethod(Identifier.For("set_Item"), oldVariableTypeDomain, oldVariableTypeRange);

                    Expression index;
                    if (oneDimensional)
                    {
                        index = cbv.FoundVariables[0];
                    }
                    else
                    {
                        //InstanceInitializer ctor =
                        //  ContractNodes.TupleClass.GetConstructor(SystemTypes.Int32.GetArrayType(1));
                        //Expression index = new Construct(new MemberBinding(null,ctor),new ExpressionList(
                        index = Literal.Null;
                    }

                    MethodCall mc = new MethodCall(new MemberBinding(new MemberBinding(closureLocal, f), setItem),
                        new ExpressionList(index, e));

                    Statement stat = new ExpressionStatement(mc);

                    List<Local> locals = new List<Local>(this.stackOfBoundVariables.Count);
                    TrivialHashtable paramMap = new TrivialHashtable();

                    // Generate a local for each bound variable to use in for-loop

                    foreach (Variable v in this.stackOfBoundVariables)
                    {
                        Local l = new Local(Identifier.Empty, v.Type);
                        paramMap[v.UniqueKey] = l;
                        locals.Add(l);
                    }

                    // Substitute locals for bound variables in old expression *AND* in inner loop bounds

                    SubstituteParameters sps = new SubstituteParameters(paramMap, this.stackOfBoundVariables);
                    sps.Visit(stat);

                    // Create nested for-loops around assignment

                    // keep track of when the first variable is used (from innermost to outermost)
                    // as soon as the first one is needed because the old expression depends on it,
                    // then keep all enclosing loops. It would be possible to keep only those where
                    // the necessary loops have loop bounds that depend on an enclosing loop, but I
                    // haven't calculated that, so just keep them all. For instance, if the old expression
                    // depends on j and the loops are "for i,0,n" and inside that "for j,0,i", then need
                    // both loops. If the inner loop bounds were 0 and n, then wouldn't need the outer
                    // loop.
                    bool usedAVariable = false;

                    for (int i = this.stackOfBoundVariables.Count - 1; 0 <= i; i--)
                    {
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

                    this.prestateValuesOfOldExpressions.Statements.Add(stat);

                    // Return expression to be used in poststate

                    Method getItem = oldVariableType.GetMethod(Identifier.For("get_Item"), oldVariableTypeDomain);
                    if (oneDimensional)
                    {
                        index = cbv.FoundReferences[0];
                    }
                    else
                    {
                        //InstanceInitializer ctor =
                        //  ContractNodes.TupleClass.GetConstructor(SystemTypes.Int32.GetArrayType(1));
                        //Expression index = new Construct(new MemberBinding(null,ctor),new ExpressionList(
                        index = Literal.Null;
                    }

                    // Return an expression that will evaluate in the poststate to the value of the old
                    // expression in the prestate. This will be this.up.f[i,j,k,...] where "up" is the field C#
                    // generated to point to the instance of the top-level closure class.
                    MemberBinding thisDotF;

                    if (this.PointerToTopLevelClosureClass == null)
                    {
                        // then the old expression occurs in the top-level closure class. Just return "this.f"
                        // where "this" refers to the top-level closure class.
                        Contract.Assume(f != null);

                        thisDotF = new MemberBinding(new This(clTemplate), HelperMethods.Unspecialize(f));
                    }
                    else
                    {
                        thisDotF = new MemberBinding(
                            new MemberBinding(new This(clTemplate), this.PointerToTopLevelClosureClass),
                            f);
                    }

                    return new MethodCall(new MemberBinding(thisDotF, getItem), new ExpressionList(index));
                }
            }
            else
            {
                // Not in closure ==> Create a local variable

                Local l = GetLocalForOldExpression(oldExpression);

                // Make sure local can be seen in the debugger (for the entire method, unfortunately)

                if (currentMethod.LocalList == null)
                {
                    currentMethod.LocalList = new LocalList();
                }

                currentMethod.LocalList.Add(l);
                currentMethod.Body.HasLocals = true;

                this.prestateValuesOfOldExpressions.Statements.Add(new AssignmentStatement(l, oldExpression.expression));

                // Return an expression that will evaluate in the poststate to the value of the old
                // expression in the prestate. When we're not in a closure, this is just the local
                // itself.
                return l;
            }
        }

        private string BestGuessForLocalName(OldExpression oldExpression)
        {
            Contract.Requires(oldExpression != null);

            MemberBinding mb = oldExpression.expression as MemberBinding;
            if (mb != null)
            {
                return mb.BoundMember.Name.Name;
            }

            MethodCall mc = oldExpression.expression as MethodCall;
            if (mc != null)
            {
                mb = mc.Callee as MemberBinding;
                if (mb != null)
                {
                    Method calledMethod = mb.BoundMember as Method;
                    if (calledMethod != null && calledMethod.IsPropertyGetter)
                    {
                        Property prop = calledMethod.DeclaringMember as Property;
                        if (prop != null)
                            return prop.Name.Name;
                        
                        return mb.BoundMember.Name.Name;
                    }
                    
                    return mb.BoundMember.Name.Name;
                }
            }

            Parameter p = oldExpression.expression as Parameter;
            if (p != null)
            {
                return p.Name.Name;
            }

            // Didn't find something good to use for the name, so just make it unique.
            var x = this.counter.ToString();
            this.counter++;

            return x;
        }

        private Local GetLocalForOldExpression(OldExpression oldExpression)
        {
            Contract.Requires(oldExpression != null);

            string localName = BestGuessForLocalName(oldExpression);
            return new Local(Identifier.For("Contract.Old(" + localName + ")"), oldExpression.Type);
        }

        public override Expression VisitMethodCall(MethodCall call)
        {
            if (call == null || call.Callee == null)
            {
                return call;
            }

            MemberBinding mb = call.Callee as MemberBinding;
            if (mb == null)
            {
                return base.VisitMethodCall(call);
            }

            Method m = mb.BoundMember as Method;
            if (m == null)
            {
                return base.VisitMethodCall(call);
            }

            // ForAll(...) or Exists(...)

            if (this.contractNodes.IsForallMethod(m) || this.contractNodes.IsExistsMethod(m))
            {
                Parameter closureParameter = null;

                // "ForAll(lb,ub, delegate(int i) { ... })" ==> "i" (or Exists)
                ExpressionList operands = call.Operands;
                
                if (operands == null || operands.Count < 3) goto callBase;
                
                Construct construct = operands[2] as Construct;
                if (construct == null)
                {
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
                if (closureParameter != null)
                {
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

            // All other method calls

            return base.VisitMethodCall(call);
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
        public override Expression VisitConstruct(Construct cons)
        {
            if (cons.Type is DelegateNode)
            {
                UnaryExpression ue = cons.Operands[1] as UnaryExpression;
                if (ue == null) goto JustVisit;

                MemberBinding mb = ue.Operand as MemberBinding;
                if (mb == null) goto JustVisit;

                Method m = mb.BoundMember as Method;

                Contract.Assume(m != null);

                if (m.IsStatic)
                {
                    // then there is no closure class, m is just a static method the compiler
                    // added to the class itself
                    goto JustVisit;
                }
                if (HelperMethods.IsCompilerGenerated(m))
                {
                    Local l = cons.Operands[0] as Local;
                    if (l == null) goto JustVisit; // but then what is it??

                    TypeNode savedClosureClass = this.currentClosureClass;

                    this.currentClosureClass = l.Type;
                    if (savedClosureClass == null)
                    {
                        // then this is the top-level closure class
                        // have to treat it special: it doesn't contain a field that points 
                        // to the top-level closure class. The field introduced to hold the value of the
                        // old expression will be declared in this class.
                        this.topLevelClosureClass = this.currentClosureClass;
                    }
                    else
                    {
                        // Find the field in this.closureClass that the C# compiler generated
                        // to point to the top-level closure
                        foreach (Member mem in this.currentClosureClass.Members)
                        {
                            Field f = mem as Field;
                            if (f == null) continue;

                            if (f.Type == this.topLevelClosureClass)
                            {
                                this.PointerToTopLevelClosureClass = f;
                                break;
                            }
                        }
                    }

                    this.VisitBlock(m.Body);

                    if (savedClosureClass == null)
                    {
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
}