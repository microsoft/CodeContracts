using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Compiler;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Contracts.Foxtrot
{
    internal class ExtractorVisitor : StandardVisitor
    {
        // Private Fields

        protected internal readonly ContractNodes contractNodes;

        /// <summary>
        /// Non-null if we are extracting from a X.Contracts reference assembly and X is the ultimate target so 
        /// we can filter types that don't appear in X. This is important at the moment because our target X might
        /// be silverlight, but our contract reference assembly is desktop and contains more stuff. 
        /// </summary>
        protected readonly AssemblyNode ultimateTargetAssembly;

        protected readonly Dictionary<Method, Method> visitedMethods = new Dictionary<Method, Method>();
        private readonly bool verbose = false;
        private readonly VisibilityHelper /*!*/ visibility;
        private bool errorFound = false;
        private SourceContext currentMethodSourceContext;
        private readonly bool fSharp = false;
        private bool isVB = false;
        private readonly ExtractionFinalizer extractionFinalizer;


        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
         SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.visitedMethods != null);
            Contract.Invariant(this.contractNodes != null);
            Contract.Invariant(this.extractionFinalizer != null);
            Contract.Invariant(this.realAssembly != null);
        }

        // Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractNodes"></param>
        /// <param name="targetContractNodes"></param>
        /// <param name="ultimateTargetAssembly">specify X if extracting from X.Contracts.dll, otherwise null</param>
        public ExtractorVisitor(ContractNodes /*!*/ contractNodes, AssemblyNode ultimateTargetAssembly, AssemblyNode realAssembly)
            : this(contractNodes, ultimateTargetAssembly, realAssembly, false, false)
        {
            Contract.Requires(contractNodes != null);
            Contract.Requires(realAssembly != null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ultimateTargetAssembly">specify X if extracting from X.Contracts.dll, otherwise null</param>
        [ContractVerification(true)]
        public ExtractorVisitor(ContractNodes /*!*/ contractNodes,
            AssemblyNode ultimateTargetAssembly,
            AssemblyNode realAssembly,
            bool verbose,
            bool fSharp)
        {
            Contract.Requires(contractNodes != null);
            Contract.Requires(realAssembly != null);

            this.contractNodes = contractNodes;
            this.verbose = verbose;
            this.fSharp = fSharp;
            this.visibility = new VisibilityHelper();
            this.errorFound = false;
            this.extractionFinalizer = new ExtractionFinalizer(contractNodes);
            this.ultimateTargetAssembly = ultimateTargetAssembly;
            this.realAssembly = realAssembly;

            this.contractNodes.ErrorFound += delegate(CompilerError error)
            {
                // Commented out because the ErrorFound event already had a handler that was printing out a message
                // and so error messages were getting printed out twice
                //if (!error.IsWarning || warningLevel > 0) {
                //  Console.WriteLine(error.ToString());
                //}
                errorFound |= !error.IsWarning;
            };

            this.TaskType = new Cache<TypeNode>(() => HelperMethods.FindType(realAssembly, Identifier.For("System.Threading.Tasks"), Identifier.For("Task")));

            this.GenericTaskType = new Cache<TypeNode>(() =>
                HelperMethods.FindType(realAssembly, Identifier.For("System.Threading.Tasks"),
                    Identifier.For("Task" + TargetPlatform.GenericTypeNamesMangleChar + "1")));
        }

        // Helper Methods

        public void CallErrorFound(CompilerError e)
        {
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
                CallErrorFound(new Error(errorCode,
                    string.Format("{0}: In method {1}, assembly {2}", s, method.FullName,
                        method.DeclaringType.DeclaringModule.Location), context));
            }
        }

        /// <summary>
        /// Specialized in ClousotExtractor
        /// </summary>
        protected virtual Statement ExtraAssumeFalseOnThrow()
        {
            return null;
        }

        protected virtual bool IncludeModels
        {
            get { return false; }
        }

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
            ref HelperMethods.StackDepthTracker dupStackTracker)
        {
            if (this.verbose)
            {
                Console.WriteLine("Method : " + method.FullName);
            }

            if (method == null || method.Body == null || method.Body.Statements == null ||
                method.Body.Statements.Count <= 0)
            {
                return;
            }

            Block methodBody = method.Body;
            int n = methodBody.Statements.Count;
            int beginning = 0;

            while (beginning < n && methodBody.Statements[beginning] is PreambleBlock)
            {
                beginning++;
            }

            int lastBlockContainingContract;
            int lastStatementContainingContract;

            bool anyContractCall = FindLastBlockWithContracts(methodBody.Statements, beginning,
                out lastBlockContainingContract, out lastStatementContainingContract);

            // Make sure any locals in the contracts are disjoint from the locals in the rest of the body

            // can use the same one throughout
            GatherLocals gatherLocals = new GatherLocals();

            SourceContext lastContractSourceContext = method.SourceContext;
            if (!anyContractCall)
            {
                if (this.verbose)
                {
                    Console.WriteLine("\tNo contracts found");
                }

                // still need to check for bad other contract calls in method body

                goto CheckBody;
            }

            Block lastBlock = methodBody.Statements[lastBlockContainingContract] as Block;
            lastContractSourceContext = lastBlock.SourceContext;

            // probably not a good context, what to do if one can't be found?
            if (lastBlock.Statements != null && 0 <= lastStatementContainingContract &&
                lastStatementContainingContract < lastBlock.Statements.Count)
            {
                lastContractSourceContext = lastBlock.Statements[lastStatementContainingContract].SourceContext;
            }

            // Make sure contract section is not in any try-catch region

            TrivialHashtable<int> block2Index = new TrivialHashtable<int>(methodBody.Statements.Count);
            for (int i = 0, nn = methodBody.Statements.Count; i < nn; i++)
            {
                if (methodBody.Statements[i] == null) continue;
                block2Index[methodBody.Statements[i].UniqueKey] = i;
            }
            // Check each exception handler and see if any overlap with the contract section
            for (int i = 0, nn = method.ExceptionHandlers == null ? 0 : method.ExceptionHandlers.Count; i < nn; i++)
            {
                ExceptionHandler eh = method.ExceptionHandlers[i];
                if (eh == null) continue;

                if (((int) block2Index[eh.BlockAfterTryEnd.UniqueKey]) < beginning ||
                    lastBlockContainingContract < ((int) block2Index[eh.TryStartBlock.UniqueKey]))
                {
                    continue; // can't overlap
                }

                this.HandleError(method, 1024, "Contract section within try block.", lastContractSourceContext);
                return;
            }

            // Extract <beginning,0> to <lastBlockContainingContract,lastStatmentContainingContract>
            StatementList contractClump = HelperMethods.ExtractClump(methodBody.Statements, beginning, 0,
                lastBlockContainingContract, lastStatementContainingContract);

            // Look for bad stuff

            BadStuff(method, contractClump, lastContractSourceContext);

            // Make sure that the entire contract section is closed.
            if (!CheckClump(method, gatherLocals, currentMethodSourceContext, new Block(contractClump)))
            {
                return;
            }

            // Checking that had the side effect of populating the hashtable, but now each contract will be individually visited.
            // That process needs to start with a fresh table.
            gatherLocals.Locals = new TrivialHashtable();

            Preconditions = new RequiresList();
            Postconditions = new EnsuresList();
            Validations = new RequiresList();
            modelPostConditions = new EnsuresList();

            if (!ExtractFromClump(
                contractClump, method, gatherLocals, Preconditions, Postconditions, Validations,
                modelPostConditions, lastContractSourceContext, method, contractInitializerBlock, ref dupStackTracker))
            {
                return;
            }

            CheckBody:

            // Check "real" method body for use of any locals used in contracts

            //      var checkMethodBody = new CheckForBadContractStuffInMethodBody(gatherLocals, this.CallErrorFound, method);
            var checkMethodBody = new CheckLocals(gatherLocals);
            checkMethodBody.Visit(methodBody);

            if (!this.fSharp && checkMethodBody.reUseOfExistingLocal != null)
            {
                SourceContext sc = lastContractSourceContext;

                this.HandleError(method, 1025,
                    "After contract block, found use of local variable '" +
                    checkMethodBody.reUseOfExistingLocal.Name.Name + "' defined in contract block", sc);
            }
        }

        [ContractVerification(true)]
        private bool FindLastBlockWithContracts(StatementList statements, int beginning,
            out int lastBlockContainingContract, out int lastStatementContainingContract)
        {
            Contract.Requires(statements != null);
            Contract.Requires(beginning >= 0);

            Contract.Ensures(Contract.ValueAtReturn(out lastBlockContainingContract) >= 0 || !Contract.Result<bool>());
            Contract.Ensures(Contract.ValueAtReturn(out lastBlockContainingContract) < statements.Count ||
                             !Contract.Result<bool>());

            Contract.Ensures(Contract.ValueAtReturn(out lastStatementContainingContract) >= 0 ||
                             !Contract.Result<bool>());

            lastBlockContainingContract = -1;
            lastStatementContainingContract = -1;
            int n = statements.Count;

            for (int i = n - 1; beginning <= i; i--)
            {
                Block b = statements[i] as Block;
                if (b == null || b.Statements == null || b.Statements.Count <= 0) continue;

                for (int j = b.Statements.Count - 1; 0 <= j; j--)
                {
                    if (this.contractNodes.IsContractOrValidatorOrAbbreviatorCall(b.Statements[j]))
                    {
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
        private bool ExtractFromClump(StatementList contractClump, Method method, GatherLocals gatherLocals,
            RequiresList Preconditions, EnsuresList Postconditions, RequiresList validations,
            EnsuresList modelPostconditions, SourceContext defaultContext, Method originalMethod,
            Block contractInitializer, ref HelperMethods.StackDepthTracker dupStackTracker)
        {
            // set the state so that the contract clump is used for extraction (as opposed to the method body as it used to)
            StatementList stmts = contractClump;

            int beginning = 0;
            int n = stmts.Count;
            int seginning = HelperMethods.FindNextRealStatement(((Block) stmts[beginning]).Statements, 0);

            bool endContractFound = false;
            bool postConditionFound = false;

            SourceContext currentSourceContext;

            for (int i = beginning; i < n; i++)
            {
                Block b = (Block) stmts[i];
                if (b == null) continue;

                for (int j = 0, m = b.Statements == null ? 0 : b.Statements.Count; j < m; j++)
                {
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
                          !this.contractNodes.IsContractOrValidatorOrAbbreviatorMethod(calledMethod))))
                    {
                        // Treat throw statements as (part of) a precondition

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
                        if (!(i < n - 1))
                        {
                            this.HandleError(method, 1027, "Malformed contract.", s.SourceContext);
                            return false;
                        }

                        Block nextBlock = (Block) stmts[i + 1]; // cast succeeds because body is clump
                        Local valueOfPrecondition = new Local(Identifier.For("_preConditionHolds"), SystemTypes.Boolean);
                        Block preconditionHolds = new Block(new StatementList(new AssignmentStatement(valueOfPrecondition, Literal.True)));

                        ReplaceBranchTarget rbt = new ReplaceBranchTarget(nextBlock, preconditionHolds);
                        rbt.VisitBlock(currentClump);

                        int ILOffset;
                        CountPopExpressions cpe = new CountPopExpressions();
                        currentSourceContext = s.SourceContext;
                        cpe.Visit(s);

                        if (0 < cpe.PopOccurrences)
                        {
                            // then there is a set of blocks that represent the exception: the Reader
                            // was not able to decompile it back into an expression. Extract the set
                            // from the current clump and make it into a block expression

                            // Find the last block that has a branch to "preconditionHolds". After that are all of the blocks
                            // that represent the evaluation of the exception
                            int branchBlockIndex = currentClumpLength - 2;

                            // can't be the current block: that has the throw in it
                            while (0 <= branchBlockIndex)
                            {
                                Block possibleBranchBlock = currentClump.Statements[branchBlockIndex] as Block;
                                Branch br = possibleBranchBlock.Statements[possibleBranchBlock.Statements.Count - 1] as Branch;
                                if (br != null && br.Target == preconditionHolds)
                                {
                                    break;
                                }

                                branchBlockIndex--;
                            }

                            if (branchBlockIndex < 0)
                            {
                                this.HandleError(method, 1028, "Malformed exception constructor in contract.", defaultContext);
                                return false;
                            }

                            Block exceptionBlock =
                                new Block(HelperMethods.ExtractClump(currentClump.Statements, branchBlockIndex + 1, 0,
                                    currentClumpLength - 1,
                                    ((Block) currentClump.Statements[currentClumpLength - 1]).Statements.Count - 1));

                            exceptionBlock.Statements.Add(new ExpressionStatement(t.Expression));
                            SourceContext sctx = ((Block) exceptionBlock.Statements[0]).Statements[0].SourceContext;

                            if (sctx.IsValid)
                            {
                                currentSourceContext = sctx;
                            }
                            else
                            {
                                SourceContext tmp;
                                bool foundContext = HelperMethods.GetLastSourceContext(exceptionBlock.Statements, out tmp);
                                if (foundContext)
                                    currentSourceContext = tmp;
                            }

                            if (!CheckClump(method, gatherLocals, currentSourceContext, exceptionBlock)) return false;
                            
                            exception = new BlockExpression(exceptionBlock, SystemTypes.Exception);
                            ILOffset = t.ILOffset;
                        }
                        else
                        {
                            currentSourceContext = s.SourceContext;
                            if (t != null)
                            {
                                // then the statement is "throw ..."
                                exception = t.Expression;
                                ILOffset = t.ILOffset;
                            }
                            else
                            {
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

                        Block preconditionFails =
                            new Block(new StatementList(new AssignmentStatement(valueOfPrecondition, Literal.False),
                                extraAssumeFalse, new Branch(null, returnValueOfPrecondition, true, false, false)));

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
                    }
                    else
                    {
                        if (contractNodes.IsContractMethod(calledMethod))
                        {
                            // Treat calls to contract methods

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

                            MethodCall mc = ((ExpressionStatement) s).Expression as MethodCall;
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

                                if (!CheckClump(originalMethod, gatherLocals, currentSourceContext, currentClump))
                                    return false;

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

                            // determine Model status

                            mce.UsesModels = CodeInspector.UsesModel(mce.Assertion, this.contractNodes);

                            // Check context rules

                            switch (mce.NodeType)
                            {
                                case NodeType.RequiresPlain:
                                    if (postConditionFound)
                                    {
                                        this.HandleError(originalMethod, 1014, "Precondition found after postcondition.", currentSourceContext);
                                        return false;
                                    }
                                    if (mce.UsesModels)
                                    {
                                        this.HandleError(originalMethod, 1073, "Preconditions may not refer to model members.", currentSourceContext);
                                        return false;
                                    }

                                    var rp = (RequiresPlain) mce;

                                    Preconditions.Add(rp);
                                    validations.Add(rp); // also add to the internal validation list
                                    break;

                                // TODO: check visibility of post conditions based on visibility of possible implementation
                                case NodeType.EnsuresNormal:
                                case NodeType.EnsuresExceptional:
                                    Ensures ensures = (Ensures) mce;
                                    if (mce.UsesModels)
                                    {
                                        if (this.IncludeModels)
                                        {
                                            modelPostconditions.Add(ensures);
                                        }
                                    }
                                    else
                                    {
                                        Postconditions.Add(ensures);
                                    }
                                    postConditionFound = true;
                                    break;
                            }
                        }
                        else if (ContractNodes.IsValidatorMethod(calledMethod))
                        {
                            // Treat calls to Contract validators

                            if (endContractFound)
                            {
                                this.HandleError(originalMethod, 1012,
                                    "Contract call found after prior EndContractBlock.", s.SourceContext);
                                break;
                            }

                            MethodCall mc = ((ExpressionStatement) s).Expression as MethodCall;
                            var memberBinding = (MemberBinding) mc.Callee;

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

                                if (CountPopExpressions.Count(mc) > 0)
                                {
                                    this.HandleError(method, 1071,
                                        "Arguments to contract validator call are too complicated. Please simplify.",
                                        currentSourceContext);
                                    return false;
                                }

                                if (!CheckClump(originalMethod, gatherLocals, currentSourceContext, validationPrefix))
                                    return false;

                                validation = new Block(new StatementList(validationPrefix, s));
                                validation.SourceContext = currentSourceContext;
                            }
                            var ro = new RequiresOtherwise(null, new BlockExpression(new Block(new StatementList(validation))));

                            validations.Add(ro);
                            CopyValidatorContracts(
                                method, calledMethod, memberBinding.TargetObject, mc.Operands,
                                Preconditions, currentSourceContext, validationPrefix);
                        }
                        else if (ContractNodes.IsAbbreviatorMethod(calledMethod))
                        {
                            // Treat calls to Contract abbreviators

                            if (endContractFound)
                            {
                                this.HandleError(originalMethod, 1012, "Contract call found after prior EndContractBlock.", s.SourceContext);
                                break;
                            }

                            MethodCall mc = ((ExpressionStatement) s).Expression as MethodCall;
                            var memberBinding = (MemberBinding) mc.Callee;
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

                                if (CountPopExpressions.Count(mc) > 0)
                                {
                                    this.HandleError(method, 1070,
                                        "Arguments to contract abbreviator call are too complicated. Please simplify.",
                                        currentSourceContext);
                                    return false;
                                }

                                if (!CheckClump(originalMethod, gatherLocals, currentSourceContext, currentClump))
                                    return false;

                                if (HelperMethods.IsNonTrivial(currentClump))
                                {
                                    contractInitializer.Statements.Add(currentClump);
                                }
                            }

                            CopyAbbreviatorContracts(method, calledMethod, memberBinding.TargetObject, mc.Operands,
                                Preconditions, Postconditions, currentSourceContext, validations, contractInitializer);
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

                    // Re-initialize current state after contract has been found

                    beginning = i;
                    seginning = j + 1;

                    //seginning = HelperMethods.FindNextRealStatement(((Block)stmts[i]).Statements, j + 1);
                    if (seginning < 0) seginning = 0;
                    
                    //b = (Block)stmts[i]; // IMPORTANT! Need this to keep "b" in sync
                }
            }

            if (this.verbose)
            {
                Console.WriteLine("\tNumber of Preconditions: " + Preconditions.Count);
                Console.WriteLine("\tNumber of Postconditions: " + Postconditions.Count);
            }

            return true;
        }

        private class FindExceptionThrown : Inspector
        {
            private TypeNode foundExceptionType;

            private FindExceptionThrown()
            {
            }

            public override void VisitConstruct(Construct cons)
            {
                if (cons == null) return;

                var memberBinding = cons.Constructor as MemberBinding;
                if (memberBinding != null && memberBinding.BoundMember != null &&
                    HelperMethods.DerivesFromException(memberBinding.BoundMember.DeclaringType))
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


        private void CopyValidatorContracts(Method targetMethod, Method validatorMethodInstance, Expression targetObject,
            ExpressionList actuals, RequiresList Preconditions, SourceContext useSite, Block validatorPrefix)
        {
            // make sure to have extracted contracts from the validator method prior
            if (validatorMethodInstance.DeclaringType.DeclaringModule == targetMethod.DeclaringType.DeclaringModule)
            {
                var validatorMethod = validatorMethodInstance;

                while (validatorMethod.Template != null)
                {
                    validatorMethod = validatorMethod.Template;
                }

                this.VisitMethod(validatorMethod);
            }

            if (validatorMethodInstance.Contract == null) return;

            var copier = new AbbreviationDuplicator(validatorMethodInstance, targetMethod, this.contractNodes,
                validatorMethodInstance, targetObject, actuals);

            var validatorContract = HelperMethods.DuplicateContractAndClosureParts(copier, targetMethod,
                validatorMethodInstance, this.contractNodes, false);

            if (validatorContract == null) return;

            MoveValidatorRequires(targetMethod, validatorContract.Requires, Preconditions, useSite,
                validatorContract.ContractInitializer, validatorPrefix);
        }

        private void CopyAbbreviatorContracts(Method targetMethod, Method abbreviatorMethodInstance,
            Expression targetObject, ExpressionList actuals, RequiresList Preconditions, EnsuresList Postconditions,
            SourceContext useSite, RequiresList validations, Block contractInitializer)
        {
            Contract.Requires(validations != null);

            // make sure to have extracted contracts from the abbreviator method prior
            if (abbreviatorMethodInstance.DeclaringType.DeclaringModule == targetMethod.DeclaringType.DeclaringModule)
            {
                var abbrevMethod = abbreviatorMethodInstance;

                while (abbrevMethod.Template != null)
                {
                    abbrevMethod = abbrevMethod.Template;
                }

                this.VisitMethod(abbrevMethod);
            }

            if (abbreviatorMethodInstance.Contract == null) return;

            var copier = new AbbreviationDuplicator(abbreviatorMethodInstance, targetMethod, this.contractNodes,
                abbreviatorMethodInstance, targetObject, actuals);

            var abbrevContract = HelperMethods.DuplicateContractAndClosureParts(copier, targetMethod,
                abbreviatorMethodInstance, this.contractNodes, true);

            if (abbrevContract == null) return;

            if (HelperMethods.IsNonTrivial(abbrevContract.ContractInitializer))
            {
                contractInitializer.Statements.Add(abbrevContract.ContractInitializer);
            }

            MoveAbbreviatorRequires(targetMethod, abbrevContract.Requires, Preconditions, validations, useSite);
            MoveAbbreviatorEnsures(targetMethod, abbrevContract.Ensures, Postconditions, useSite);
        }

        private void MoveAbbreviatorEnsures(Method targetMethod, EnsuresList ensuresList, EnsuresList Postconditions, SourceContext useSite)
        {
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
        private void MoveValidatorRequires(Method targetMethod, RequiresList requiresList, RequiresList Preconditions,
            SourceContext useSite, Block validationContractInitializer, Block validationPrefix)
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
                if (HelperMethods.IsNonTrivial(validationPrefix))
                {
                    req.Condition =
                        new BlockExpression(
                            new Block(new StatementList(validationPrefix, new ExpressionStatement(req.Condition))));
                    validationPrefix = null;
                }
                
                if (HelperMethods.IsNonTrivial(validationContractInitializer))
                {
                    req.Condition =
                        new BlockExpression(
                            new Block(new StatementList(validationContractInitializer,
                                new ExpressionStatement(req.Condition))));
                    validationContractInitializer = null;
                }

                Preconditions.Add(req);
            }
        }

        private void MoveAbbreviatorRequires(Method targetMethod, RequiresList requiresList, RequiresList Preconditions,
            RequiresList validations, SourceContext useSite)
        {
            Contract.Requires(validations != null);

            if (requiresList == null) return;

            foreach (RequiresPlain req in requiresList)
            {
                if (!req.DefSite.IsValid)
                {
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
            if (result != null)
            {
                // filter by visibility
                if (member != null && !HelperMethods.IsVisibleFrom(member, containing.DeclaringType)) return null;

                return result;
            }

            this.HandleError(containing, 1065,
                "User message to contract call can only be string literal, or a static field, or static property that is at least internally visible.",
                sc);

            return null;
        }

        public static Expression FilterUserMessage(Method targetMethod, Expression expression)
        {
            if (expression == null) return null;

            Member member;
            var result = SanitizeUserMessageInternal(targetMethod, expression, out member);
            if (result != null)
            {
                // filter by visibility
                if (member != null && !HelperMethods.IsVisibleFrom(member, targetMethod.DeclaringType)) return null;

                return result;
            }

            return null;
        }

        private static Expression SanitizeUserMessageInternal(Method containing, Expression expression,
            out Member member)
        {
            member = null;

            Literal l = expression as Literal;
            if (l != null)
            {
                return l;
            }

            // if it is an abbrev, we sanitize later.
            if (ContractNodes.IsAbbreviatorMethod(containing) || ContractNodes.IsValidatorMethod(containing))
                return expression;

            MemberBinding b = expression as MemberBinding;
            if (b != null)
            {
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

        private void BadStuff(Method method, StatementList contractClump, SourceContext defaultSourceContext)
        {
            LookForBadStuff lfbs = new LookForBadStuff(this.contractNodes);
            lfbs.CheckForBadStuff(contractClump);

            if (lfbs.ReturnStatement != null)
            {
                SourceContext sctx = lfbs.ReturnStatement.SourceContext;
                if (!sctx.IsValid)
                    sctx = defaultSourceContext;

                this.HandleError(method, 1015, "Return statement found in contract section.", sctx);
                return;
            }

            if (lfbs.BadStuffFound)
            {
                this.HandleError(method, 1016,
                    "Contract.Assert/Contract.Assume cannot be used in contract section. Use only Requires and Ensures.",
                    defaultSourceContext);
            }
        }

        private bool CheckClump(Method method, GatherLocals gatherLocals, SourceContext sctx, Block clump)
        {
            if (!HelperMethods.ClumpIsClosed(clump.Statements))
            {
                this.contractNodes.CallErrorFound(new Error(1017,
                    "Malformed contract section in method '" + method.FullName + "'", sctx));
                return false;
                //            throw new ExtractorException();
            }

            TypeNode t = method.DeclaringType;
            bool IsContractTypeForSomeOtherType = t == null
                ? false
                : HelperMethods.GetTypeFromAttribute(t, ContractNodes.ContractClassForAttributeName) != null;

            // We require that a contract class implement the interface it is holding contracts for. In addition, the methods in that class must be explicit
            // interface implementations. This creates a problem when the contracts need to refer to other methods in the interface. The "this" reference has
            // the wrong type: it needs to be the interface type. Writing an explicit cast before every reference is painful. It is better to allow the user
            // to create a local variable of the interface type and assign it before the contracts. This can't cause a problem with the local being used later
            // in the method body, because methods in contract classes don't have method bodies, just contracts. So don't check for re-use of locals for such
            // methods.
            //

            // Example of above comment

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

            if (!IsContractTypeForSomeOtherType)
            {
                // First, make sure the clump doesn't use any locals that were used
                // in any previous contract, need a new instance each time
                CheckLocals checkLocals = new CheckLocals(gatherLocals);
                checkLocals.Visit(clump);

                if (!this.fSharp && checkLocals.reUseOfExistingLocal != null)
                {
                    this.HandleError(method, 1040,
                        "Reuse of existing local variable '" + checkLocals.reUseOfExistingLocal.Name.Name +
                        "' in contract.", sctx);
                    return false;
                }

                // If that test passes, then add in the locals used in the clump into the table of locals that have been used
                gatherLocals.Visit(clump);
            }

            return true;
        }

        private void ExtractIndividualInvariants(TypeNode type, InvariantList result, Method invariantMethod)
        {
            if (invariantMethod == null || invariantMethod.Body == null || invariantMethod.Body.Statements == null ||
                invariantMethod.Body.Statements.Count == 0)
            {
                return;
            }

            Block invariantMethodBody = invariantMethod.Body;
            int n = invariantMethodBody.Statements.Count;

            int beginning = 0;

            while (beginning < invariantMethodBody.Statements.Count &&
                   invariantMethodBody.Statements[beginning] is PreambleBlock)
            {
                beginning++;
            }

            for (int i = beginning; i < n; i++)
            {
                Block b = (Block) invariantMethodBody.Statements[i];
                int seginning = 0;
                for (int j = 0, m = b.Statements == null ? 0 : b.Statements.Count; j < m; j++)
                {
                    ExpressionStatement s = b.Statements[j] as ExpressionStatement;
                    if (s == null) continue;

                    Literal invariantName;
                    Literal sourceText;
                    Expression invariantCondition = this.contractNodes.IsInvariant(s, out invariantName, out sourceText);
                    if (invariantCondition == null)
                    {
                        Method called = HelperMethods.IsMethodCall(s);
                        if (called != null)
                        {
                            if (HelperMethods.IsVoidType(called.ReturnType))
                            {
                                this.CallErrorFound(new Error(1045,
                                    "Invariant methods must be a sequence of calls to Contract.Invariant(...)",
                                    s.SourceContext));
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
                    if (HelperMethods.FindContext(currentClump, s.SourceContext, out sctx))
                    {
                        inv.SourceContext = sctx;
                        inv.Condition.SourceContext = sctx;
                    }

                    inv.ILOffset = s.ILOffset;
                    inv.UsesModels = CodeInspector.UsesModel(inv.Condition, this.contractNodes);
                    if (inv.UsesModels)
                    {
                        this.HandleError(invariantMethod, 1072, "Invariants cannot refer to model members.", sctx);
                    }
                    else
                    {
                        result.Add(inv);
                    }

                    // Re-initialize current state

                    beginning = i;
                    seginning = j + 1;
                    b = (Block) invariantMethodBody.Statements[i]; // IMPORTANT! Need this to keep "b" in sync
                }
            }
        }

        private Cache<TypeNode> TaskType;
        private Cache<TypeNode> GenericTaskType;

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

            if (HelperMethods.IsType(returnType, SystemTypes.IEnumerable) ||
                HelperMethods.IsType(returnType, SystemTypes.IEnumerator))
            {
                return true;
            }

            while (returnType.Template != null) returnType = returnType.Template;

            if (HelperMethods.IsType(returnType, SystemTypes.GenericIEnumerable) ||
                HelperMethods.IsType(returnType, SystemTypes.GenericIEnumerator))
            {
                return true;
            }

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

        private static TypeNode FindClosureClass(Method original)
        {
            return (new ClosureClassFinder(original)).ClosureClass;
        }

        private class ClosureClassFinder : Inspector
        {
            private Method method;
            private string closureTag;
            private TypeNode closureClass = null;

            private static TypeNode iAsyncStateMachineType =
                System.Compiler.SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.CompilerServices"),
                    Identifier.For("IAsyncStateMachine"));

            private TypeNode IAsyncStateMachineType
            {
                get
                {
                    if (iAsyncStateMachineType == null)
                    {
                        // pre v4.5
                        var module = this.method.DeclaringType.DeclaringModule;
                        foreach (var aref in module.AssemblyReferences)
                        {
                            if (aref.Name == "System.Threading.Tasks")
                            {
                                iAsyncStateMachineType =
                                    aref.Assembly.GetType(Identifier.For("System.Runtime.CompilerServices"),
                                        Identifier.For("IAsyncStateMachine"));
                                break;
                            }
                        }
                    }

                    return iAsyncStateMachineType;
                }
            }

            public ClosureClassFinder(Method method)
            {
                this.method = method;
                this.closureTag = "<" + method.Name.Name + ">d_";
            }

            public TypeNode ClosureClass
            {
                get
                {
                    if (closureClass == null)
                    {
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

                            this.closureClass = (Class) template;
                        }
                        else
                        {
                            this.closureClass = (Class) sourceConstruct.Type;
                        }
                    }
                }
            }

            public override void VisitAssignmentStatement(AssignmentStatement assignment)
            {
                Expression source = assignment.Source;
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
                            this.closureClass = (Class) template;
                        }
                        else
                        {
                            this.closureClass = (Class) sourceConstruct.Type;
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
                            }
                        }
                    }
                }
            }
        }

        [ContractVerification(true)]
        private void ProcessClosureClass(Method method, TypeNode closure, bool isAsync)
        {
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

            StatementList contractClump = GetContractClumpFromMoveNext(method, movenext, contractNodes,
                contractInitializerBlock.Statements, out defaultSourceContext, ref dupStackTracker,
                out originalContractPosition);

            if (contractClump != null)
            {
                // Look for bad stuff

                BadStuff(method, contractClump, defaultSourceContext);

                // Make sure any locals in the contracts are disjoint from the locals in the rest of the body

                // can use the same one throughout
                GatherLocals gatherLocals = new GatherLocals();

                // Make sure that the entire contract section is closed.
                if (!CheckClump(movenext, gatherLocals, currentMethodSourceContext, new Block(contractClump)))
                {
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
                if (!ExtractFromClump(contractClump, movenext, gatherLocals, Preconditions, Postconditions, Validations,
                    modelPostconditions, defaultSourceContext, method, contractInitializerBlock, ref dupStackTracker))
                {
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
                    // customers (neither CCRewriter customers nor CCCheck customers).
                    // If this assumption would not be true in the future, proper fix should be applied.
                    // put requires as assumes into movenext method at original position
                    // ReplaceRequiresWithAssumeInMoveNext(origPreconditions, originalContractPosition);

                    // no postPreamble to initialize, as method is not a ctor
                }
                finally
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

                    var assume = new MethodCall(
                        new MemberBinding(null, this.contractNodes.AssumeMethod),
                        new ExpressionList(pre.Condition), NodeType.Call);

                    assume.SourceContext = pre.SourceContext;
                    var assumeStmt = new ExpressionStatement(assume);

                    assumeStmt.SourceContext = pre.SourceContext;
                    origStatements.Add(assumeStmt);
                }
            }
        }

        private static Identifier tasknamespace;

        ///
        /// Share exceptional posts on async list
        /// Move posts meantioning async.Result to async ensures
        /// 
        /// Only do this on method returning a Task !
        /// 
        [ContractVerification(true)]
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

            var actualReturnType = (currentMethod.ReturnType.TemplateArguments == null ||
                                    currentMethod.ReturnType.TemplateArguments.Count == 0)
                ? null
                : currentMethod.ReturnType.TemplateArguments[0];

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

        private class MyMethodBodySpecializer : MethodBodySpecializer
        {
            public MyMethodBodySpecializer(Module module, TypeNodeList source, TypeNodeList target)
                : base(module, source, target)
            {
            }
        }

        private static MapClosureExpressionToOriginalExpression BuildMappingFromClosureToOriginal(TypeNode ClosureClass,
            Method MoveNextMethod, Method OriginalMethod)
        {
            Contract.Ensures(Contract.Result<MapClosureExpressionToOriginalExpression>() != null);

            Dictionary<string, Parameter> closureFieldsMapping = GetClosureFieldsMapping(ClosureClass, OriginalMethod);

            TypeNodeList TPListSource = ClosureClass.ConsolidatedTemplateParameters;

            if (TPListSource == null) TPListSource = new TypeNodeList();

            TypeNodeList TPListTarget = new TypeNodeList();

            if (OriginalMethod.DeclaringType != null &&
                OriginalMethod.DeclaringType.ConsolidatedTemplateParameters != null)
            {
                foreach (TypeNode tn in OriginalMethod.DeclaringType.ConsolidatedTemplateParameters)
                    TPListTarget.Add(tn);
            }

            if (OriginalMethod.TemplateParameters != null)
            {
                foreach (TypeNode tn in OriginalMethod.TemplateParameters)
                {
                    TPListTarget.Add(tn);
                }
            }

            Debug.Assert((TPListSource == null && TPListTarget == null) || TPListSource.Count == TPListTarget.Count);

            return new MapClosureExpressionToOriginalExpression(
                ClosureClass, closureFieldsMapping, TPListSource,
                TPListTarget, OriginalMethod);
        }

        private static Dictionary<string, Parameter> GetClosureFieldsMapping(TypeNode /*!*/ ClosureClass,
            Method OriginalMethod)
        {
            Dictionary<string, Parameter> result = new Dictionary<string, Parameter>();
            foreach (Member mem in ClosureClass.Members)
            {
                Field field = mem as Field;
                if (field != null)
                {
                    Parameter p;
                    if (FieldAssociatedWithParameter(field, OriginalMethod, out p))
                    {
                        result.Add(field.Name.Name, p);
                    }
                }
            }

            return result;
        }

        private static bool FieldAssociatedWithParameter(Field field, Method originalMethod, out Parameter p)
        {
            string fname = field.Name.Name;

            if (fname.EndsWith("__this"))
            {
                // check that it is not a nested one
                if (fname.Length > 8 && !fname.Substring(2, fname.Length - 8).Contains("__"))
                {
                    p = originalMethod.ThisParameter;
                    return true;
                }
            }

            foreach (Parameter par in originalMethod.Parameters)
            {
                string pname = par.Name.Name;
                if (fname == pname)
                {
                    p = par;
                    return true;
                }
            }

            p = null;
            return false;
        }

        private class MapClosureExpressionToOriginalExpression : StandardVisitor
        {
            private Dictionary<string, Parameter> closureParametersMapping;
            private Dictionary<string, Local> closureLocalsMapping = new Dictionary<string, Local>();
            private MethodBodySpecializer specializer;
            private Method method;
            private TypeNode closureUnspec;

            public MapClosureExpressionToOriginalExpression(TypeNode Closure,
                Dictionary<string, Parameter> closureParametersMapping,
                TypeNodeList TPListSource, TypeNodeList TPListTarget, Method method)
            {
                this.closureParametersMapping = closureParametersMapping;

                if (TPListTarget == null || TPListSource == null || TPListSource.Count == 0 || TPListTarget.Count == 0)
                    specializer = null;
                else
                    specializer = new MyMethodBodySpecializer(Closure.DeclaringModule, TPListSource, TPListTarget);

                this.method = method;
                this.closureUnspec = HelperMethods.Unspecialize(Closure);
            }

            public override Expression VisitMemberBinding(MemberBinding memberBinding)
            {
                Field field = memberBinding.BoundMember as Field;
                if (field != null)
                {
                    // Case 1: iterator_closure's this.field should be turned into either a parameter or a local.
                    if (memberBinding.TargetObject is This)
                    {
                        var thisParam = (This) memberBinding.TargetObject;
                        var declType = thisParam.Type;

                        Reference declRef = declType as Reference;
                        if (declRef != null)
                        {
                            declType = declRef.ElementType;
                        }

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
                            
                            if (field.Name.Name.Contains("__locals" /* csc.exe */) ||
                                field.Name.Name.Contains("<>8__") /* roslyn-based csc */||
                                field.Name.Name.Contains("<>9__")
                                /* roslyn-based cached anonymous method delegates */||
                                field.Name.Name.Contains("__spill") /* rcsc.exe */||
                                field.Name.Name.Contains("__CachedAnonymousMethodDelegate") /* junk, revisit */)
                            {
                                Local newLocal = new Local(field.Name, field.Type);
                                closureLocalsMapping.Add(field.Name.Name, newLocal);
                                return newLocal;
                            }

                            throw new NotImplementedException(
                                String.Format("Found field {0} in contract that shouldn't be here", field.Name.Name));
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

            public RequiresList Apply(RequiresList reqs)
            {
                if (specializer != null)
                {
                    specializer.CurrentMethod = method;
                    specializer.CurrentType = method.DeclaringType;
                    reqs = specializer.VisitRequiresList(reqs);
                }
                RequiresList result = this.VisitRequiresList(reqs);
                return result;
            }

            public EnsuresList Apply(EnsuresList ens)
            {
                if (specializer != null)
                {
                    specializer.CurrentMethod = method;
                    specializer.CurrentType = method.DeclaringType;
                    ens = specializer.VisitEnsuresList(ens);
                }

                EnsuresList result = this.VisitEnsuresList(ens);
                return result;
            }

            public Block Apply(Block block)
            {
                if (specializer != null)
                {
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
        private StatementList GetContractClumpFromMoveNext(Method iteratorMethod, Method moveNext,
            ContractNodes contractNodes, StatementList contractInitializer, out SourceContext defaultSourceContext,
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

            if (blockIndex < 0)
            {
                // Couldn't find state 0 in MoveNext method
                // This can happen if the iterator is trivial (like yield break; )
                defaultSourceContext = default(SourceContext);
                return null;
            }

            int beginning = blockIndex; // the block number in the body of movenext
            int sbeginning = statementIndex; // the statement no. in the beginnning block after the preamble
            int blast = -1;

            // the block number in the body of movenext, of the last block where there is a contract call
            int slast = -1; // the statement no. in the blast block

            // Next we move sbeginning past the preamble area
            sbeginning = MovePastPreamble(iteratorMethod, moveNext, beginning, sbeginning, contractInitializer,
                contractNodes, ref dupStackTracker);

            Contract.Assert(moveNext.Body != null, "should be provable");
            Contract.Assert(moveNext.Body.Statements != null, "should be provable");

            if (sbeginning < 0 ||
                !this.FindLastBlockWithContracts(moveNext.Body.Statements, beginning, out blast, out slast))
            {
                if (verbose)
                {
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
                lastContractSourceContext = lastBlock.SourceContext;
                // probably not a good context, what to do if one can't be found?
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
            StatementList result = HelperMethods.ExtractClump(
                moveNext.Body.Statements, beginning, sbeginning, blast,
                slast, assumeBlock: originalContractPosition);

            defaultSourceContext = lastContractSourceContext;
            return result;
        }

        [ContractVerification(true)]
        private static int MovePastPreamble([Pure] Method iteratorMethod, [Pure] Method moveNext, int beginning,
            int currentIndex, StatementList contractInitializer, ContractNodes contractNodes,
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
            while (state0Block == null && beginning + 1 < moveNext.Body.Statements.Count)
            {
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
            currentIndex = HelperMethods.MovePastClosureInit(iteratorMethod, state0Block, contractNodes,
                contractInitializer, null, currentIndex, ref dupStackTracker, out closureType);
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

        private enum EvalKind
        {
            None = 0,
            IsStateValue,
            IsFinalCompare,
            IsDisposingTest
        }

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
        private static int ContractStartInMoveNext(ContractNodes contractNodes, Method moveNext, out int statementIndex,
            Method origMethod)
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
                if (field != null && field.Name != null && field.Name.Name.Contains("<>") &&
                    field.Name.Name.Contains("__state"))
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

                    if (contractNodes.IsContractOrValidatorOrAbbreviatorCall(stmt))
                    {
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
                        
                        continue;
                    }

                    if (isAsync && lastBranchNonConditional)
                    {
                        // not a branch and last one was non-conditional
                        // must be the end.
                        statementIndex = i;
                        return currentBlockIndex;
                    }

                    var swtch = stmt as SwitchInstruction;
                    if (swtch != null)
                    {
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

        private static bool IsThisDotState(Expression exp)
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

        private static bool IsDoFinallyBodies(Expression expression)
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
        private static Pair<int, EvalKind> EvaluateExpression(Expression expression,
            Dictionary<Variable, Pair<int, EvalKind>> env, bool ignoreUnknown, bool isAsync)
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
                if (lit.Value is int) return Pair.For((int) lit.Value, EvalKind.None);

                return Pair.For(-2, EvalKind.None);
            }

            var mb = expression as MemberBinding;
            if (mb != null)
            {
                if (mb.TargetObject != null && mb.TargetObject is This && mb.BoundMember != null &&
                    mb.BoundMember.Name != null && mb.BoundMember.Name.Name != null)
                {
                    var name = mb.BoundMember.Name.Name;
                    if (name.EndsWith("$__disposing"))
                    {
                        return Pair.For(0, EvalKind.IsDisposingTest);
                    }

                    if (name.Contains("<>") && name.Contains("__state"))
                    {
                        var initialState = isAsync ? -1 : 0;
                        return Pair.For(initialState, EvalKind.IsStateValue);
                    }
                }

                return Pair.For(-2, EvalKind.None);
            }

            var variable = expression as Variable;
            if (variable != null)
            {
                Pair<int, EvalKind> value;
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
        private static int FindTargetBlock(StatementList blocks, Block target, int current)
        {
            Contract.Requires(blocks != null);
            Contract.Requires(current >= 0);
            Contract.Ensures(Contract.Result<int>() >= -1);
            Contract.Ensures(Contract.Result<int>() < blocks.Count);

            for (int i = current + 1; i < blocks.Count; i++)
            {
                if (blocks[i] == target) return i;
            }

            return -1;
        }

        // Visitor Overrides

        public override AssemblyNode VisitAssembly(AssemblyNode assembly)
        {
            if (assembly == null) return null;

            if (this.verbose)
            {
                Console.WriteLine("Extracting from {0}", assembly.Location);
            }

            if (ContractNodes.IsAlreadyRewritten(assembly))
            {
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

        public override TypeNode VisitTypeNode(TypeNode typeNode)
        {
            if (typeNode == null) return null;

            if (!this.IncludeModels && HelperMethods.HasAttribute(typeNode.Attributes, ContractNodes.ModelAttributeName))
                return null;

            // early cop out if we are extracting from X.Contracts.dll and X.dll does not have this type

            if (SkipThisTypeDueToMismatchInReferenceAssemblyPlatform(ultimateTargetAssembly, typeNode)) return typeNode;

            TypeNode result;
            if (typeNode.Contract != null)
            {
                TypeContract tc = typeNode.Contract;
                TypeNode t = base.VisitTypeNode(typeNode);
                t.Contract = tc;
                result = t;
            }
            else
            {
                result = base.VisitTypeNode(typeNode);

                TryLiftInvariantsToPropertyRequiresEnsures(typeNode);
            }

            return result;
        }

        private bool SkipThisTypeDueToMismatchInReferenceAssemblyPlatform(AssemblyNode ultimateTargetAssembly,
            TypeNode typeNode)
        {
            if (ultimateTargetAssembly == null) return false;

            if (typeNode == this.contractNodes.ContractClass)
                return false; // don't skip contract methods as we need to extract their contracts

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
        private readonly AssemblyNode realAssembly;

        /// <summary>
        /// Extracts contracts from a class into the appropriate fields in the CCI tree.
        /// </summary>
        /// <param name="Class">Class to visit.</param>
        /// <returns>The same class but with contracts extracted.</returns>
        public override Class VisitClass(Class Class)
        {
            // Special processing for classes that are holding the contract for an interface or abstract class

            var originalType = HelperMethods.IsContractTypeForSomeOtherTypeUnspecialized(Class, this.contractNodes);
            var originalScrubber = this.currentScrubber;

            if (originalType != null)
            {
                this.currentScrubber = new ScrubContractClass(this, Class, originalType);
            }

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

            if (!this.IncludeModels && HelperMethods.HasAttribute(property.Attributes, ContractNodes.ModelAttributeName))
                return null;

            return base.VisitProperty(property);
        }

        public override Field VisitField(Field field)
        {
            if (field == null) return null;

            if (!this.IncludeModels && HelperMethods.HasAttribute(field.Attributes, ContractNodes.ModelAttributeName))
                return null;

            return base.VisitField(field);
        }

        /// <summary>
        /// Extracts method-level contracts from a method into the appropriate fields in the CCI tree.
        /// </summary>
        /// <param name="method">Method to visit.</param>
        /// <returns>The same method but with contracts extracted.</returns>
        [ContractVerification(true)]
        public override Method VisitMethod(Method method)
        {
            if (method == null) return null;

            Contract.Assume(method.Template == null);
            Contract.Assume(TypeNode.IsCompleteTemplate(method.DeclaringType));

            if (this.contractNodes.IsObjectInvariantMethod(method))
            {
                ExtractInvariantsFromMethod(method);
                return method;
            }

            if (!this.IncludeModels && HelperMethods.HasAttribute(method.Attributes, ContractNodes.ModelAttributeName))
                return null;

            // skip iterator movenext methods
            if (HelperMethods.IsCompilerGenerated(method.DeclaringType) &&
                method.Name != null && method.Name.Name == "MoveNext")
            {
                // generated MoveNext method, skip it.
                return method;
            }

            // Keep track of visited methods

            if (visitedMethods.ContainsKey(method))
                return method;

            visitedMethods.Add(method, method);

            var scrubber = this.currentScrubber;
            if (scrubber != null)
            {
                method.SetDelayedContract((m, dummy) =>
                {
                    scrubber.VisitMethod(m);
                    ExtractContractsForMethod(m, dummy);
                });
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

                TypeNode /*?*/ closure = null;
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

                // Find first source context, use it if no better one is found on errors

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

                Block contractInitializerBlock = new Block(new StatementList());
                Block postPreamble = null;

                HelperMethods.StackDepthTracker dupStackTracker = new HelperMethods.StackDepthTracker();

                var contractLocalAliasingThis = HelperMethods.ExtractPreamble(method, this.contractNodes,
                    contractInitializerBlock, out postPreamble, ref dupStackTracker, this.isVB);

                bool saveErrorFound = this.errorFound;
                this.errorFound = false;

                // Extract pre- and postconditions

                if (method.Body != null && method.Body.Statements != null)
                {
                    this.CheapAndDirty(method, ref preconditions, ref postconditions, ref validations,
                        ref modelPostconditions, contractInitializerBlock, ref dupStackTracker);

                    if (this.errorFound)
                    {
                        method.ClearBody();

                        this.errorFound = saveErrorFound;
                        return;
                    }

                    this.errorFound = saveErrorFound;
                }

                // Sanitize contract by renaming local aliasing "this" to This

                if (contractLocalAliasingThis != null && method.ThisParameter != null)
                {
                    var renamer = new ContractLocalToThis(contractLocalAliasingThis, method.ThisParameter);
                    renamer.Visit(preconditions);
                    renamer.Visit(postconditions);
                    renamer.Visit(modelPostconditions);
                }

                // Split out async ensures

                var asyncPostconditions = SplitAsyncEnsures(ref postconditions, method);

                // Store contracts into the appropriate slots

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
                    methodContract.ValidationsCount == 0)
                {
                    method.Contract = null;
                }
                else
                {
                    // turn helper method calls to Result, OldValue, ValueAtReturn into proper AST nodes.
                    this.extractionFinalizer.VisitMethodContract(methodContract);
                }
            }
        }

        private class ContractLocalToThis : StandardVisitor
        {
            private This @this;
            private Local local;

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
            // Well formedness

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

                if (getter.Contract == null)
                {
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

        private class ReplaceAutoPropertiesWithCorrespondingFields : StandardVisitor
        {
            private List<Property> autoprops;

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
            private Property autoProp;
            private ExtractorVisitor parent;
            private Expression userMessage;
            private Literal conditionString;
            private bool makeRequires;

            private ChangePropertyInvariantIntoRequiresEnsures(ExtractorVisitor parent, Property autoProp)
                : base(autoProp.DeclaringType.DeclaringModule, autoProp.DeclaringType)
            {
                this.autoProp = autoProp;
                this.parent = parent;
            }

            public static void Transform(ExtractorVisitor parent, Property autoProp, Invariant invariant,
                out Requires req, out Ensures ens)
            {
                var makeReq = new ChangePropertyInvariantIntoRequiresEnsures(parent, autoProp);
                req = makeReq.MakeRequires(invariant.Condition);

                var makeEns = new ChangePropertyInvariantIntoRequiresEnsures(parent, autoProp);
                ens = makeEns.MakeEnsures(invariant.Condition);

                ens.SourceContext = invariant.SourceContext;
                ens.PostCondition.SourceContext = ens.SourceContext;
                ens.ILOffset = invariant.ILOffset;

                if (ens.SourceConditionText == null)
                {
                    ens.SourceConditionText = invariant.SourceConditionText;
                }

                req.SourceContext = invariant.SourceContext;
                req.Condition.SourceContext = req.SourceContext;
                req.ILOffset = invariant.ILOffset;

                if (req.SourceConditionText == null)
                {
                    req.SourceConditionText = invariant.SourceConditionText;
                }
            }

            private Ensures MakeEnsures(Expression expression)
            {
                makeRequires = false;

                var condition = (Expression) this.Visit(expression);
                var result = new EnsuresNormal(condition);

                result.SourceConditionText = conditionString;
                result.UserMessage = userMessage;

                return result;
            }

            private Requires MakeRequires(Expression expression)
            {
                makeRequires = true;

                var condition = (Expression) this.Visit(expression);
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
                    
                    return new ReturnValue(autoProp.Type);
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
            private List<Property> foundAutoProperties = new List<Property>();
            private List<Member> referencedMembers = new List<Member>();

            private TypeNode containingType;
            private ExtractorVisitor parent;
            private Expression invariantCondition;

            public static List<Property> FindAutoProperty(ExtractorVisitor parent, TypeNode containingType,
                Expression expression, out List<Member> referencedMembers)
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
                    if (memberBinding.BoundMember.Name.Matches(ContractNodes.InvariantName) &&
                        memberBinding.BoundMember.DeclaringType.Name.Matches(ContractNodes.ContractClassName))
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
                    ||
                    (getter.ImplicitlyImplementedInterfaceMethods != null &&
                     0 < getter.ImplicitlyImplementedInterfaceMethods.Count)
                    || getter.OverridesBaseClassMember)
                    // if the property is an override/implementation, then it is going to inherit any contracts there might be from above
                {
                    return;
                }

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
    }
}