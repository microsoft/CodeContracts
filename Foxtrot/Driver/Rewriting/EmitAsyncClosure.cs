using System.Collections.Generic;
using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Visitor that creates special closure type for async postconditions.
    /// </summary>
    internal class EmitAsyncClosure : StandardVisitor
    {
        private Method fromMethod;
        private TypeNode declaringType;
        private Class closureClass;
        private Class closureClassInstance;
        private Specializer forwarder;
        private Duplicator dup;
        private InstanceInitializer constructor;
        private Method checkMethod;
        private Method checkExceptionMethod;
        private Local originalResultLocal;
        private Local newResultLocal;
        private StatementList checkBody;
        private Rewriter parent;
        private InstanceInitializer funcCtor;
        private Identifier checkMethodId;
        private Identifier checkExceptionMethodId;
        private Method continuewithMethod;
        private readonly List<SourceContext> contractResultCapturedInStaticContext = new List<SourceContext>();

        public IList<SourceContext> ContractResultCapturedInStaticContext
        {
            get { return contractResultCapturedInStaticContext; }
        }

        /// <summary>
        /// Instance used in calling method context
        /// </summary>
        public Class ClosureClass
        {
            get { return this.closureClassInstance; }
        }

        public Class ClosureClassGeneric
        {
            get { return this.closureClass; }
        }

        public InstanceInitializer Ctor
        {
            get { return (InstanceInitializer) this.closureClassInstance.GetMembersNamed(StandardIds.Ctor)[0]; }
        }

        public Method CheckMethod
        {
            get { return (Method) this.closureClassInstance.GetMembersNamed(this.checkMethodId)[0]; }
        }

        public InstanceInitializer FuncCtor
        {
            get { return this.funcCtor; }
        }

        public Member ContinueWithMethod
        {
            get { return this.continuewithMethod; }
        }

        public Local ClosureLocal { get; private set; }
        public Block ClosureInitializer { get; private set; }

        private Cache<TypeNode> AggregateExceptionType;
        private Cache<TypeNode> Func2Type;

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
            this.fromMethod = @from;
            this.parent = parent;

            this.checkMethodId = Identifier.For("CheckPost");
            this.checkExceptionMethodId = Identifier.For("CheckException");
            this.declaringType = @from.DeclaringType;

            var closureName = HelperMethods.NextUnusedMemberName(declaringType,
                "<" + @from.Name.Name + ">AsyncContractClosure");

            this.closureClass = new Class(declaringType.DeclaringModule, declaringType, null,
                TypeFlags.NestedPrivate, null, Identifier.For(closureName), SystemTypes.Object, null, null);

            declaringType.Members.Add(this.closureClass);
            RewriteHelper.TryAddCompilerGeneratedAttribute(this.closureClass);

            this.dup = new Duplicator(this.declaringType.DeclaringModule, this.declaringType);

            var taskType = @from.ReturnType;
            var taskArgs = taskType.TemplateArguments == null ? 0 : taskType.TemplateArguments.Count;

            this.AggregateExceptionType = new Cache<TypeNode>(() =>
                HelperMethods.FindType(parent.AssemblyBeingRewritten, StandardIds.System,
                    Identifier.For("AggregateException")));

            this.Func2Type = new Cache<TypeNode>(() =>
                HelperMethods.FindType(SystemTypes.SystemAssembly, StandardIds.System, Identifier.For("Func`2")));

            if (@from.IsGeneric)
            {
                this.closureClass.TemplateParameters = new TypeNodeList();

                var parentCount = this.declaringType.ConsolidatedTemplateParameters == null
                    ? 0
                    : this.declaringType.ConsolidatedTemplateParameters.Count;

                for (int i = 0; i < @from.TemplateParameters.Count; i++)
                {
                    var tp = HelperMethods.NewEqualTypeParameter(dup, (ITypeParameter) @from.TemplateParameters[i],
                        this.closureClass, parentCount + i);

                    this.closureClass.TemplateParameters.Add(tp);
                }

                this.closureClass.IsGeneric = true;
                this.closureClass.EnsureMangledName();
                this.forwarder = new Specializer(this.declaringType.DeclaringModule, @from.TemplateParameters,
                    this.closureClass.TemplateParameters);

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
                    var taskInstance = taskTemplate.GetTemplateInstance(this.closureClass.DeclaringModule,
                        taskType.TemplateArguments[0]);

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
                    new ExpressionStatement(
                        new MethodCall(new MemberBinding(ctor.ThisParameter, SystemTypes.Object.GetConstructor()),
                            new ExpressionList())),
                    new Return()
                    ));

                this.closureClass.Members.Add(ctor);
            }

            // now that we added the ctor and the check method, let's instantiate the closure class if necessary
            if (this.closureClassInstance == null)
            {
                var consArgs = new TypeNodeList();
                var args = new TypeNodeList();

                var parentCount = this.closureClass.DeclaringType.ConsolidatedTemplateParameters == null
                    ? 0
                    : this.closureClass.DeclaringType.ConsolidatedTemplateParameters.Count;

                for (int i = 0; i < parentCount; i++)
                {
                    consArgs.Add(this.closureClass.DeclaringType.ConsolidatedTemplateParameters[i]);
                }

                var methodCount = @from.TemplateParameters == null ? 0 : @from.TemplateParameters.Count;
                for (int i = 0; i < methodCount; i++)
                {
                    consArgs.Add(@from.TemplateParameters[i]);
                    args.Add(@from.TemplateParameters[i]);
                }

                this.closureClassInstance =
                    (Class)
                        this.closureClass.GetConsolidatedTemplateInstance(this.parent.AssemblyBeingRewritten,
                            closureClass.DeclaringType, closureClass.DeclaringType, args, consArgs);
            }

            // create closure initializer for context method
            this.ClosureLocal = new Local(this.ClosureClass);
            this.ClosureInitializer = new Block(new StatementList());

            this.ClosureInitializer.Statements.Add(new AssignmentStatement(this.ClosureLocal,
                new Construct(new MemberBinding(null, this.Ctor), new ExpressionList())));
        }

        private void EmitCheckMethod(TypeNode taskType, bool hasResult)
        {
            var funcType = this.continuewithMethod.Parameters[0].Type;
            this.funcCtor = funcType.GetConstructor(SystemTypes.Object, SystemTypes.IntPtr);

            var taskParameter = new Parameter(Identifier.For("task"), taskType);
            this.originalResultLocal = new Local(taskParameter.Type);

            if (hasResult)
            {
                this.checkMethod = new Method(this.closureClass, null, this.checkMethodId,
                    new ParameterList(taskParameter), taskType.TemplateArguments[0], null);

                checkMethod.CallingConvention = CallingConventionFlags.HasThis;
                checkMethod.Flags |= MethodFlags.Public;

                this.checkBody = new StatementList();
                var tmpresult = new Local(checkMethod.ReturnType);
                this.newResultLocal = tmpresult;

                checkBody.Add(new AssignmentStatement(this.originalResultLocal, taskParameter));
                checkBody.Add(new AssignmentStatement(tmpresult,
                    new MethodCall(
                        new MemberBinding(checkMethod.Parameters[0],
                            checkMethod.Parameters[0].Type.GetMethod(Identifier.For("get_Result"))),
                        new ExpressionList())));
            }
            else
            {
                this.checkMethod = new Method(this.closureClass, null, this.checkMethodId,
                    new ParameterList(taskParameter), SystemTypes.Void, null);

                checkMethod.CallingConvention = CallingConventionFlags.HasThis;
                checkMethod.Flags |= MethodFlags.Public;

                this.checkBody = new StatementList();

                var aggregateType = AggregateExceptionType.Value;
                this.newResultLocal = new Local(aggregateType);

                checkBody.Add(new AssignmentStatement(this.originalResultLocal, taskParameter));
                checkBody.Add(new AssignmentStatement(this.newResultLocal,
                    new BinaryExpression(
                        new MethodCall(
                            new MemberBinding(checkMethod.Parameters[0],
                                checkMethod.Parameters[0].Type.GetMethod(Identifier.For("get_Exception"))),
                            new ExpressionList()), new MemberBinding(null, aggregateType), NodeType.Isinst)));
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

            // Map closure locals to fields and initialize closure fields

            foreach (Ensures e in asyncPostconditions)
            {
                if (e == null) continue;

                this.Visit(e);

                if (this.forwarder != null)
                {
                    this.forwarder.Visit(e);
                }

                ReplaceResult repResult = new ReplaceResult(this.checkMethod, this.originalResultLocal,
                    this.parent.AssemblyBeingRewritten);

                repResult.Visit(e);

                if (repResult.ContractResultWasCapturedInStaticContext)
                {
                    this.contractResultCapturedInStaticContext.Add(e.Assertion.SourceContext);
                }

                // now need to initialize closure result fields
                foreach (var target in repResult.NecessaryResultInitializationAsync(this.closureLocals))
                {
                    // note: target here 
                    methodBody.Add(new AssignmentStatement(target, this.originalResultLocal));
                }
            }

            // Emit normal postconditions

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
                Method ensMethod = this.parent.RuntimeContracts.EnsuresMethod;

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

            this.parent.CleanUpCodeCoverage.VisitStatementList(ensuresChecks);
            this.parent.EmitRecursionGuardAroundChecks(this.checkMethod, methodBodyBlock, ensuresChecks);

            // Exceptional postconditions

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
                this.checkExceptionMethod = new Method(this.closureClass, null, this.checkExceptionMethodId,
                    new ParameterList(exnParameter), SystemTypes.Boolean, new Block(new StatementList()));

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
                    funcType = funcType.GetTemplateInstance(this.parent.AssemblyBeingRewritten,
                        SystemTypes.Exception, SystemTypes.Boolean);

                    var handleMethod = aggregateType.GetMethod(Identifier.For("Handle"), funcType);

                    var funcLocal = new Local(funcType);
                    var ldftn = new UnaryExpression(new MemberBinding(null, this.checkExceptionMethod),
                        NodeType.Ldftn, CoreSystemTypes.IntPtr);

                    methodBody.Add(new AssignmentStatement(funcLocal,
                        new Construct(
                            new MemberBinding(null, funcType.GetConstructor(SystemTypes.Object, SystemTypes.IntPtr)),
                            new ExpressionList(this.checkMethod.ThisParameter, ldftn))));

                    methodBody.Add(
                        new ExpressionStatement(new MethodCall(
                            new MemberBinding(this.newResultLocal, handleMethod), new ExpressionList(funcLocal))));

                    methodBody.Add(new Throw(this.newResultLocal));
                    methodBody.Add(blockAfterTest);
                }
                else
                {
                    // The tryCatch holds the try block, the catch blocks, and an empty block that is the
                    // target of an unconditional branch for normal execution to go from the try block
                    // around the catch blocks.
                    if (this.checkMethod.ExceptionHandlers == null)
                        this.checkMethod.ExceptionHandlers = new ExceptionHandlerList();

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
                    funcType = funcType.GetTemplateInstance(this.parent.AssemblyBeingRewritten,
                        SystemTypes.Exception, SystemTypes.Boolean);

                    var handleMethod = aggregateType.GetMethod(Identifier.For("Handle"), funcType);

                    var funcLocal = new Local(funcType);
                    var ldftn = new UnaryExpression(new MemberBinding(null, this.checkExceptionMethod),
                        NodeType.Ldftn, CoreSystemTypes.IntPtr);

                    catchBlock.Statements.Add(new AssignmentStatement(funcLocal,
                        new Construct(
                            new MemberBinding(null, funcType.GetConstructor(SystemTypes.Object, SystemTypes.IntPtr)),
                            new ExpressionList(this.checkMethod.ThisParameter, ldftn))));

                    catchBlock.Statements.Add(
                        new ExpressionStatement(new MethodCall(new MemberBinding(l, handleMethod),
                            new ExpressionList(funcLocal))));

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

            // Create a block for the return statement and insert it

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
        }

        private bool IsVoidTask()
        {
            return this.checkMethod.ReturnType == SystemTypes.Void;
        }

        private void EmitCheckExceptionBody(List<Ensures> asyncPostconditions)
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

            if (this.checkExceptionMethod.ExceptionHandlers == null)
                this.checkExceptionMethod.ExceptionHandlers = new ExceptionHandlerList();

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

                body.Add(new AssignmentStatement(l,
                    new BinaryExpression(this.checkExceptionMethod.Parameters[0], new MemberBinding(null, e.Type),
                        NodeType.Isinst)));

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
                    new MethodCall(new MemberBinding(null, this.parent.RuntimeContracts.EnsuresOnThrowMethod),
                        args, NodeType.Call, SystemTypes.Void), e.SourceContext));

                this.parent.CleanUpCodeCoverage.VisitStatementList(checks);
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
                body.Add(new AssignmentStatement(l,
                    new BinaryExpression(this.checkExceptionMethod.Parameters[0],
                        new MemberBinding(null, aggregateType), NodeType.Isinst)));

                Block skipBlock = new Block();
                body.Add(new Branch(new UnaryExpression(l, NodeType.LogicalNot), skipBlock));
                body.Add(catchBlock);
                body.Add(skipBlock);

                var funcType = Func2Type.Value;
                funcType = funcType.GetTemplateInstance(this.parent.AssemblyBeingRewritten, SystemTypes.Exception, SystemTypes.Boolean);

                var handleMethod = aggregateType.GetMethod(Identifier.For("Handle"), funcType);

                var funcLocal = new Local(funcType);
                var ldftn = new UnaryExpression(new MemberBinding(null, this.checkExceptionMethod), NodeType.Ldftn,
                    CoreSystemTypes.IntPtr);

                catchBlock.Statements.Add(new AssignmentStatement(funcLocal,
                    new Construct(
                        new MemberBinding(null, funcType.GetConstructor(SystemTypes.Object, SystemTypes.IntPtr)),
                        new ExpressionList(this.checkMethod.ThisParameter, ldftn))));

                catchBlock.Statements.Add(
                    new ExpressionStatement(new MethodCall(new MemberBinding(l, handleMethod),
                        new ExpressionList(funcLocal))));
            }

            // add return true to CheckExceptionMethod
            body.Add(returnBlock);
            body.Add(new Return(Literal.True));
        }

        // Visitor for changing closure locals to fields

        private Dictionary<Local, MemberBinding> closureLocals = new Dictionary<Local, MemberBinding>();

        public override Expression VisitLocal(Local local)
        {
            if (HelperMethods.IsClosureType(this.declaringType, local.Type))
            {
                MemberBinding mb;
                if (!closureLocals.TryGetValue(local, out mb))
                {
                    // Forwarder would be null, if enclosing method with async closure is not generic
                    var localType = forwarder != null ? forwarder.VisitTypeReference(local.Type) : local.Type;

                    var closureField = new Field(this.closureClass, null, FieldFlags.Public, local.Name, localType, null);
                    this.closureClass.Members.Add(closureField);

                    mb = new MemberBinding(this.checkMethod.ThisParameter, closureField);
                    closureLocals.Add(local, mb);

                    // initialize the closure field
                    var instantiatedField = Rewriter.GetMemberInstanceReference(closureField, this.closureClassInstance);
                    this.ClosureInitializer.Statements.Add(
                        new AssignmentStatement(new MemberBinding(this.ClosureLocal, instantiatedField), local));
                }

                return mb;
            }

            return local;
        }
    }
}