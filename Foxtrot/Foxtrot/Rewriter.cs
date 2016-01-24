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
using System.Diagnostics; // needed for Debug.Assert (etc.)
// needed for defining exception .ctors
using System.Compiler;
using System.Diagnostics.Contracts;
using Microsoft.Contracts.Foxtrot.Utils;

namespace Microsoft.Contracts.Foxtrot
{
    [ContractVerification(false)]
    public sealed class Rewriter : Inspector
    {
        // Private fields

        private ContractNodes rewriterNodes;

        /// <summary>
        /// Set for the subvisit from a type with invariants. Used for constructors to avoid checking invariants too early
        /// when methods are called from the constructor.
        /// </summary>
        private Field ReentrancyFlag = null;

        private Method InvariantMethod = null;

        private bool verbose = false;
        private Module module = null;

        // Used only by EmitAsyncClosure. Should be private
        private AssemblyNode assemblyBeingRewritten;

        internal AssemblyNode AssemblyBeingRewritten { get { return this.assemblyBeingRewritten; } }

        private Method IDisposeMethod = null;

        private RuntimeContractEmitFlags contractEmitFlags;

        private RuntimeContractMethods runtimeContracts;

        public RuntimeContractMethods RuntimeContracts { get { return this.runtimeContracts; } }

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

        private Action<System.CodeDom.Compiler.CompilerError> m_handleError;

        private CurrentState currentState;

        private struct CurrentState
        {
            public readonly Method Method;
            public readonly TypeNode Type;
            private Dictionary<string, object> typeSuppressed;
            private Dictionary<string, object> methodSuppressed;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            private CurrentState(TypeNode type, Dictionary<string, object> typeSuppressed, Method method)
            {
                this.Type = type;
                this.typeSuppressed = typeSuppressed;
                this.Method = method;
                this.methodSuppressed = null;
            }

            public CurrentState(TypeNode type)
            {
                this.Type = type;
                this.Method = null;
                this.typeSuppressed = null;
                this.methodSuppressed = null;
            }

            public bool IsSuppressed(string errorNumber)
            {
                if (this.Type != null && this.typeSuppressed == null)
                {
                    this.typeSuppressed = GrabSuppressAttributes(this.Type);
                }

                if (this.typeSuppressed != null && this.typeSuppressed.ContainsKey(errorNumber)) return true;

                if (this.Method != null && this.methodSuppressed == null)
                {
                    this.methodSuppressed = GrabSuppressAttributes(this.Method);
                }

                if (this.methodSuppressed != null && this.methodSuppressed.ContainsKey(errorNumber)) return true;

                return false;
            }

            private static readonly Identifier SuppressMessageIdentifier = Identifier.For("SuppressMessageAttribute");

            private static Dictionary<string, object> GrabSuppressAttributes(Method method)
            {
                var result = new Dictionary<string, object>();

                GrabSuppressAttributes(result, method.Attributes);

                if (method.DeclaringMember != null)
                {
                    GrabSuppressAttributes(result, method.DeclaringMember.Attributes);
                }

                return result;
            }

            private static Dictionary<string, object> GrabSuppressAttributes(TypeNode type)
            {
                var result = new Dictionary<string, object>();

                while (type != null)
                {
                    GrabSuppressAttributes(result, type.Attributes);
                    type = type.DeclaringType;
                }

                return result;
            }

            private static void GrabSuppressAttributes(Dictionary<string, object> result, AttributeList attributes)
            {
                if (attributes != null)
                {
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        var attr = attributes[i];
                        if (attr == null) continue;

                        if (attr.Type == null) continue;

                        if (attr.Type.Name.Matches(SuppressMessageIdentifier))
                        {
                            if (attr.Expressions != null && attr.Expressions.Count >= 2)
                            {
                                var expr = attr.Expressions[0] as Literal;
                                if (expr == null) continue;

                                string category = expr.Value as string;
                                if (category != "Microsoft.Contracts") continue;

                                var errorCode = attr.Expressions[1] as Literal;
                                if (errorCode == null) continue;

                                string errorCodeLit = errorCode.Value as string;
                                if (errorCodeLit.StartsWith("CC"))
                                {
                                    result.Add(errorCodeLit, errorCodeLit);
                                }
                            }
                        }
                    }
                }
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")
            ]
            internal CurrentState Derive(Method method)
            {
                return new CurrentState(this.Type, this.typeSuppressed, method);
            }
        }

        /// <summary>
        /// For level, see TranslateLevel
        /// </summary>
        /// <param name="assemblyBeingRewritten"></param>
        /// <param name="rewriterNodes"></param>
        /// <param name="level"></param>
        public Rewriter(AssemblyNode assemblyBeingRewritten, RuntimeContractMethods runtimeContracts,
            Action<System.CodeDom.Compiler.CompilerError> handleError, bool inheritInvariantsAcrossAssemblies,
            bool skipQuantifiers)
        {
            Contract.Requires(handleError != null);

            // F:
            Contract.Requires(runtimeContracts != null);

            // Find IDisposable.Dispose method

            TypeNode iDisposable = SystemTypes.IDisposable;
            if (iDisposable != null)
            {
                IDisposeMethod = iDisposable.GetMethod(Identifier.For("Dispose"));
            }

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

        private readonly bool InheritInvariantsAcrossAssemblies;
        private readonly bool skipQuantifiers;

        /// <summary>
        /// Performs filtering of errors based on SuppressMessage attributes
        /// </summary>
        private void HandleError(System.CodeDom.Compiler.CompilerError error)
        {
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

        // Public Fields and Properties

        public bool Verbose
        {
            get { return this.verbose; }
            set { this.verbose = value; }
        }

        private bool HasRequiresContracts(Method template, out Method methodWithContract)
        {
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
            if (consolidated != null && consolidated.Count > 0)
            {
                declaringType = declaringType.GetGenericTemplateInstance(this.assemblyBeingRewritten, consolidated);

                // F: GetGenericTemplateInstance has a /*!*/ annotation
                Contract.Assume(declaringType != null);
            }

            // find method again
            if (index < declaringType.Members.Count)
            {
                var instance = declaringType.Members[index] as Method;
                if (instance == null)
                {
                    // we are in trouble. Need to cook up instance reference ourselves
                    throw new Exception("TODO: cook up instance reference");
                }

                // now instantiate method if generic
                if (instance.TemplateParameters != null && instance.TemplateParameters.Count > 0)
                {
                    instance = instance.GetTemplateInstance(declaringType, instance.TemplateParameters);
                }

                return instance;
            }

            // we are in trouble. Need to cook up instance reference ourselves
            throw new Exception("TODO: cook up instance reference");
        }

        public override void VisitInterface(Interface Interface)
        {
            // No need to think about rewriting any interface methods, so
            // just return the interface without visiting down into it.
            // NB: We need to do this otherwise some interface methods get
            // a body, but I haven't been able to figure out why.
        }

        private TrivialHashtable visitedClasses = new TrivialHashtable();

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

        public override void VisitTypeNode(TypeNode typeNode)
        {
            if (typeNode == null) return;

            if (HelperMethods.IsContractTypeForSomeOtherType(typeNode, this.rewriterNodes) != null)
            {
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
                    InvariantList userWrittenInvariants = typeNode.Contract == null
                        ? null
                        : typeNode.Contract.Invariants;

                    Class asClass = typeNode as Class;
                    Field baseReentrancyFlag;

                    Method baseInvariantMethod = FindAndInstantiateBaseClassInvariantMethod(asClass,
                        out baseReentrancyFlag);

                    if ((userWrittenInvariants != null && 0 < userWrittenInvariants.Count) || baseInvariantMethod != null)
                    {
                        Field reEntrancyFlag = null;
                        var isStructWithExplicitLayout = IsStructWithExplicitLayout(typeNode as Struct);
                        if (isStructWithExplicitLayout)
                        {
                            this.HandleError(new Warning(1044,
                                String.Format(
                                    "Struct '{0}' has explicit layout and an invariant. Invariant recursion guards will not be emitted and evaluation of invariants may occur too eagerly.",
                                    typeNode.FullName), new SourceContext()));
                        }
                        else
                        {
                            // Find or create re-entrancy flag to the class

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

                                reEntrancyFlag = new Field(typeNode, null, reentrancyFlagProtection,
                                    Identifier.For("$evaluatingInvariant$"), SystemTypes.Boolean, null);

                                RewriteHelper.TryAddCompilerGeneratedAttribute(reEntrancyFlag);
                                RewriteHelper.TryAddDebuggerBrowsableNeverAttribute(reEntrancyFlag);

                                typeNode.Members.Add(reEntrancyFlag);
                            }
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

                        invariantMethod.Attributes.Add(
                            new AttributeNode(
                                new MemberBinding(null,
                                    this.runtimeContracts.ContractNodes.InvariantMethodAttribute.GetConstructor()), null));

                        // call base class invariant

                        if (baseInvariantMethod != null)
                        {
                            newBody.Statements.Add(
                                new ExpressionStatement(
                                    new MethodCall(
                                        new MemberBinding(invariantMethod.ThisParameter, baseInvariantMethod), null,
                                        NodeType.Call, SystemTypes.Void)));
                        }

                        // Add re-entrancy test to the method

                        Block invariantExit = new Block();
                        if (reEntrancyFlag != null)
                        {
                            Block reEntrancyTest = new Block(new StatementList());
                            reEntrancyTest.Statements.Add(
                                new Branch(new MemberBinding(invariantMethod.ThisParameter, reEntrancyFlag),
                                    invariantExit));

                            reEntrancyTest.Statements.Add(
                                new AssignmentStatement(
                                    new MemberBinding(invariantMethod.ThisParameter, reEntrancyFlag), Literal.True)
                                );

                            newBody.Statements.Add(reEntrancyTest);
                        }

                        Block invariantChecks = new Block(new StatementList());
                        if (userWrittenInvariants != null)
                        {
                            // Filter out invariants that aren't runtime checkable

                            var filteredInvariants = new InvariantList();
                            foreach (var i in userWrittenInvariants)
                            {
                                if (!EmitInvariant(i, this.skipQuantifiers)) continue;
                                filteredInvariants.Add(i);
                            }

                            // Duplicate the invariants

                            // need to duplicate the invariants so they aren't shared
                            Duplicator d = new Duplicator(typeNode.DeclaringModule, typeNode);
                            InvariantList duplicatedInvariants = d.VisitInvariantList(filteredInvariants);

                            // F: seems to have the invariant duplictedInvariants != null
                            Contract.Assume(duplicatedInvariants != null);

                            // Rewrite the body of the invariant method

                            // then we need to replace calls to Contract.Invariant with calls to Contract.RewriterInvariant
                            // in the body of the invariant method
                            RewriteInvariant rc = new RewriteInvariant(this.runtimeContracts);
                            rc.VisitInvariantList(duplicatedInvariants);

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
                                new AssignmentStatement(
                                    new MemberBinding(invariantMethod.ThisParameter, reEntrancyFlag), Literal.False)
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

            if (!this.Emit(RuntimeContractEmitFlags.InheritContracts))
                return null; // don't call base class invariant if we don't inherit

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
            if (result != null)
            {
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
            if (call == null || call.Callee == null)
            {
                return null;
            }

            MemberBinding mb = call.Callee as MemberBinding;
            if (mb == null)
            {
                return null;
            }

            Method m = mb.BoundMember as Method;
            if (m == null)
            {
                return null;
            }

            return m;
        }


        private static bool IsRewriterGenerated(Method method)
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
        private void VisitMethodInternal(Method method)
        {
            if (method.IsAbstract || IsRewriterGenerated(method))
            {
                return;
            }

            Block origBody = method.Body;
            if (origBody == null || origBody.Statements == null || origBody.Statements.Count == 0) return;

#if DEBUG
            if (!RewriteHelper.PostNormalizedFormat(method))
                throw new RewriteException("The method body for '" + method.FullName + "' is not structured correctly");
#endif

            // Rewrite all Assert and Assume methods

            // Happens in-place so any modifications made are persisted even if nothing else is done to method

            RewriteAssertAssumeAndCallSiteRequires raa =
                new RewriteAssertAssumeAndCallSiteRequires(this.AssertAssumeRewriteMethodTable, this.runtimeContracts,
                    this.contractEmitFlags, this, method);

            method.Body = raa.VisitBlock(method.Body);

            // Bail out early if publicSurfaceOnly and method is not visible (but not for contract validators!) or contract abbreviator

            if (this.runtimeContracts.PublicSurfaceOnly && !IsCallableFromOutsideAssembly(method) &&
                !ContractNodes.IsValidatorMethod(method)
                || ContractNodes.IsAbbreviatorMethod(method)) return;

            int oldLocalUniqueNameCounter = 0;

            List<Block> contractInitializationBlocks = new List<Block>();
            Block postPreambleBlock = null;
            Dictionary<TypeNode, Local> closureLocals = new Dictionary<TypeNode, Local>();
            Block oldExpressionPreStateValues = new Block(new StatementList());

            // Gather pre- and postconditions from supertypes and from method contract

            // Create lists of unique preconditions and postconditions.
            List<Requires> preconditions = new List<Requires>();
            List<Ensures> postconditions = new List<Ensures>();
            List<Ensures> asyncPostconditions = new List<Ensures>();
            RequiresList validations = null;

            // For postconditions, wrap all parameters within an old expression (if they are not already within one)
            var wrap = new WrapParametersInOldExpressions();
            EmitAsyncClosure asyncBuilder = null;

            // Copy the method's contracts.

            if (method.Contract != null && (method.Contract.RequiresCount > 0 || method.Contract.EnsuresCount > 0 ||
                                            method.Contract.ValidationsCount > 0 ||
                                            method.Contract.AsyncEnsuresCount > 0))
            {
                // Use duplicate of contract because it is going to get modified from processing Old(...) and
                // Result(). Can't have the modified contract get inherited (or else the Old(...) processing
                // will not work properly.)
                // Can't use MethodContract.CopyFrom or HelperMethods.DuplicateMethodBodyAndContract
                // because they assume the new contract is in a different method. Plus the latter duplicates
                // any closure classes needed for anonymous delegates in the contract.
                MethodContract mc = HelperMethods.DuplicateContractAndClosureParts(method, method,
                    this.runtimeContracts.ContractNodes, true);

                validations = mc.Validations;

                if (mc != null)
                {
                    contractInitializationBlocks.Add(mc.ContractInitializer);
                    postPreambleBlock = mc.PostPreamble;

                    RecordEnsures(method, ref oldLocalUniqueNameCounter, contractInitializationBlocks, closureLocals,
                        oldExpressionPreStateValues, postconditions, asyncPostconditions, wrap, ref asyncBuilder, mc);
                }
            }

            // Search the class hierarchy for overridden methods to propagate their contracts.

            if (this.Emit(RuntimeContractEmitFlags.InheritContracts) && method.OverridesBaseClassMember &&
                method.DeclaringType != null && method.OverriddenMethod != null)
            {
                for (TypeNode super = method.DeclaringType.BaseType;
                    super != null;
                    super = HelperMethods.DoesInheritContracts(super) ? super.BaseType : null)
                {
                    var baseMethod = super.GetImplementingMethod(method.OverriddenMethod, false);
                    Method baseContractMethod = HelperMethods.GetContractMethod(baseMethod);

                    if (baseContractMethod != null)
                    {
                        MethodContract mc = HelperMethods.DuplicateContractAndClosureParts(method, baseContractMethod,
                            this.runtimeContracts.ContractNodes, false);

                        RewriteHelper.ReplacePrivateFieldsThatHavePublicProperties(method.DeclaringType, super, mc,
                            this.rewriterNodes);

                        if (mc != null)
                        {
                            contractInitializationBlocks.Add(mc.ContractInitializer);
                            // can't have post preambles in overridden methods, since they cannot be constructors.

                            // only add requires if baseMethod is the root method
                            if (mc.Requires != null && baseMethod.OverriddenMethod == null)
                            {
                                foreach (RequiresPlain requires in mc.Requires)
                                {
                                    if (!EmitRequires(requires, this.skipQuantifiers)) continue;

                                    // Debug.Assert(!preconditions.Contains(requires));
                                    preconditions.Add(requires);
                                }
                            }

                            RecordEnsures(method, ref oldLocalUniqueNameCounter, contractInitializationBlocks,
                                closureLocals, oldExpressionPreStateValues, postconditions, asyncPostconditions, wrap,
                                ref asyncBuilder, mc);
                        }
                    }

                    // bail out if this base type does not inherit contracts
                    if (!HelperMethods.DoesInheritContracts(super)) break;
                }
            }

            // Propagate explicit interface method contracts.

            if (this.Emit(RuntimeContractEmitFlags.InheritContracts) && method.ImplementedInterfaceMethods != null)
            {
                foreach (Method interfaceMethod in method.ImplementedInterfaceMethods)
                {
                    if (interfaceMethod != null)
                    {
                        Method contractMethod = HelperMethods.GetContractMethod(interfaceMethod);

                        if (contractMethod != null)
                        {
                            // if null, then no contract for this interface method

                            // Maybe it would be easier to just duplicate the entire method and then pull the
                            // initialization code from the duplicate?
                            MethodContract mc = HelperMethods.DuplicateContractAndClosureParts(method, contractMethod,
                                this.runtimeContracts.ContractNodes, false);

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

                                RecordEnsures(method, ref oldLocalUniqueNameCounter, contractInitializationBlocks,
                                    closureLocals, oldExpressionPreStateValues, postconditions, asyncPostconditions,
                                    wrap, ref asyncBuilder, mc);
                            }
                        }
                    }
                }
            }

            // Propagate implicit interface method contracts.

            if (this.Emit(RuntimeContractEmitFlags.InheritContracts) &&
                method.ImplicitlyImplementedInterfaceMethods != null)
            {
                foreach (Method interfaceMethod in method.ImplicitlyImplementedInterfaceMethods)
                {
                    if (interfaceMethod != null)
                    {
                        Method contractMethod = HelperMethods.GetContractMethod(interfaceMethod);
                        if (contractMethod != null)
                        {
                            // if null, then no contract for this method

                            // Maybe it would be easier to just duplicate the entire method and then pull the
                            // initialization code from the duplicate?
                            MethodContract mc = HelperMethods.DuplicateContractAndClosureParts(method, contractMethod,
                                this.runtimeContracts.ContractNodes, false);

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

                                RecordEnsures(method, ref oldLocalUniqueNameCounter, contractInitializationBlocks,
                                    closureLocals, oldExpressionPreStateValues, postconditions, asyncPostconditions,
                                    wrap, ref asyncBuilder, mc);
                            }
                        }
                    }
                }
            }

            // Return if there is nothing to do

            if (preconditions.Count < 1 && postconditions.Count < 1 && asyncPostconditions.Count < 1 &&
                (validations == null || validations.Count == 0) && this.InvariantMethod == null)
            {
                return;
            }

            // Change all short branches to long because code modifications can add code

            {
                // REVIEW: This does *not* remove any short branches in the contracts.
                // I think that is okay, but we should think about it.
                RemoveShortBranches rsb = new RemoveShortBranches();
                rsb.VisitBlock(method.Body);
            }

            // Modify method body to change all returns into assignments and branch to unified exit

            // now modify the method body to change all return statements
            // into assignments to the local "result" and a branch to a
            // block at the end that can check the post-condition using
            // "result" and then finally return it.
            // [MAF] we do it later once we know if the branches are leaves or just branch.
            Local result = null;

            if (!HelperMethods.IsVoidType(method.ReturnType))
            {
                // don't write huge names. The debugger chokes on them and shows no locals at all.
                //   result = new Local(Identifier.For("Contract.Result<" + typeName + ">()"), method.ReturnType);
                result = new Local(Identifier.For("Contract.Result()"), method.ReturnType);
                if (method.LocalList == null)
                {
                    method.LocalList = new LocalList();
                }

                method.LocalList.Add(result);
            }

            Block newExit = new Block(new StatementList());

            // Create the new method's body

            Block newBody = new Block(new StatementList());
            newBody.HasLocals = true;

            // If there had been any closure initialization code, put it first in the new body

            if (0 < contractInitializationBlocks.Count)
            {
                foreach (Block b in contractInitializationBlocks)
                {
                    newBody.Statements.Add(b);
                }
            }

            EmitInterleavedValidationsAndRequires(method, preconditions, validations, newBody);

            // Turn off invariant checking until the end

            Local oldReEntrancyFlagLocal = null;
            if (MethodShouldHaveInvariantChecked(method) && this.ReentrancyFlag != null && BodyHasCalls(method.Body))
            {
                oldReEntrancyFlagLocal = new Local(SystemTypes.Boolean);

                newBody.Statements.Add(new Block(
                    new StatementList(
                        new AssignmentStatement(oldReEntrancyFlagLocal,
                            new MemberBinding(method.ThisParameter, this.ReentrancyFlag)),
                        new AssignmentStatement(new MemberBinding(method.ThisParameter, this.ReentrancyFlag), Literal.True))));
            }

            // Put all of the collected initializations from "old" expressions into the method

            if (oldExpressionPreStateValues.Statements.Count > 0)
            {
                newBody.Statements.Add(oldExpressionPreStateValues);
            }

            // if there are preamble blocks we need to move them from the origBody in case we will wrap a try-catch
            var preambleIndex = 0;
            while (origBody.Statements.Count > preambleIndex && origBody.Statements[preambleIndex] is PreambleBlock)
            {
                newBody.Statements.Add(origBody.Statements[preambleIndex]);
                origBody.Statements[preambleIndex] = null;
                preambleIndex++;
            }

            Block newBodyBlock = new Block(new StatementList());
            newBody.Statements.Add(newBodyBlock); // placeholder for eventual body
            newBody.Statements.Add(newExit);

            // Replace "result" in postconditions (both for method return and out parameters)

            if (result != null)
            {
                foreach (Ensures e in postconditions)
                {
                    if (e == null) continue;

                    var repResult = ReplaceResult(method, result, e);

                    // now need to initialize closure result fields
                    foreach (var target in repResult.NecessaryResultInitialization(closureLocals))
                    {
                        newBody.Statements.Add(new AssignmentStatement(target, result));
                    }
                }
            }

            // Emit potential post preamble block (from contract duplicate) in constructors

            if (postPreambleBlock != null)
            {
                newBody.Statements.Add(postPreambleBlock);
            }

            // Emit normal postconditions

            SourceContext lastEnsuresSourceContext = default(SourceContext);

            bool hasLastEnsuresContext = false;
            bool containsExceptionalPostconditions = false;
            var ensuresChecks = new StatementList();

            foreach (Ensures e in postconditions)
            {
                // Exceptional postconditions are handled separately.
                if (e is EnsuresExceptional)
                {
                    containsExceptionalPostconditions = true;
                    continue;
                }

                lastEnsuresSourceContext = e.SourceContext;

                hasLastEnsuresContext = true;
                // call Contract.RewriterEnsures
                Method ensMethod = this.runtimeContracts.EnsuresMethod;
                ExpressionList args = new ExpressionList();

                args.Add(e.PostCondition);
                args.Add(e.UserMessage ?? Literal.Null);

                args.Add(e.SourceConditionText ?? Literal.Null);

                ensuresChecks.Add(
                    new ExpressionStatement(
                        new MethodCall(new MemberBinding(null, ensMethod),
                            args, NodeType.Call, SystemTypes.Void), e.SourceContext));
            }

            this.cleanUpCodeCoverage.VisitStatementList(ensuresChecks);

            EmitRecursionGuardAroundChecks(method, newBody, ensuresChecks);

            // Emit object invariant

            if (MethodShouldHaveInvariantChecked(method))
            {
                // Now turn checking on by restoring old reentrancy flag
                if (this.ReentrancyFlag != null && oldReEntrancyFlagLocal != null)
                {
                    newBody.Statements.Add(
                        new AssignmentStatement(new MemberBinding(method.ThisParameter, this.ReentrancyFlag),
                            oldReEntrancyFlagLocal));
                }

                var callType = (method is InstanceInitializer) || this.InvariantMethod.DeclaringType.IsValueType
                    ? NodeType.Call
                    : NodeType.Callvirt;

                // just add a call to the already existing invariant method, "this.InvariantMethod();"
                // all of the processing needed is done as part of VisitClass
                newBody.Statements.Add(
                    new ExpressionStatement(
                        new MethodCall(
                            new MemberBinding(method.ThisParameter, this.InvariantMethod), null, callType,
                            SystemTypes.Void)));
            }

            // Emit exceptional postconditions

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

                rr = new ReplaceReturns(result, newExit, leaveExceptionBody: true);
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

                    args.Add(e.UserMessage ?? Literal.Null);

                    args.Add(e.SourceConditionText ?? Literal.Null);

                    args.Add(l);
                    var checks = new StatementList();

                    checks.Add(new ExpressionStatement(
                        new MethodCall(new MemberBinding(null, this.runtimeContracts.EnsuresOnThrowMethod),
                            args, NodeType.Call, SystemTypes.Void), e.SourceContext));

                    catchBlock.Statements.Add(new AssignmentStatement(l, new Expression(NodeType.Pop), e.SourceContext));

                    this.cleanUpCodeCoverage.VisitStatementList(checks);
                    EmitRecursionGuardAroundChecks(method, catchBlock, checks);

                    // Emit object invariant on EnsuresOnThrow check

                    if (MethodShouldHaveInvariantChecked(method, inExceptionCase: true))
                    {
                        // Now turn checking on by restoring old reentrancy flag
                        if (this.ReentrancyFlag != null && oldReEntrancyFlagLocal != null)
                        {
                            catchBlock.Statements.Add(
                                new AssignmentStatement(new MemberBinding(method.ThisParameter, this.ReentrancyFlag),
                                    oldReEntrancyFlagLocal));
                        }

                        // just add a call to the already existing invariant method, "this.InvariantMethod();"
                        // all of the processing needed is done as part of VisitClass
                        catchBlock.Statements.Add(
                            new ExpressionStatement(
                                new MethodCall(
                                    new MemberBinding(method.ThisParameter, this.InvariantMethod), null, NodeType.Call,
                                    SystemTypes.Void)));
                    }

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

            // Create a block for the return statement and insert it

            // this is the block that contains the return statements
            // it is (supposed to be) the single exit from the method
            // that way, anything that must always be done can be done
            // in this block
            Block returnBlock = new Block(new StatementList(1));

            if (asyncPostconditions != null && asyncPostconditions.Count > 0)
            {
                asyncBuilder.AddAsyncPostconditions(asyncPostconditions, returnBlock, result);
            }

            Statement returnStatement;
            if (!HelperMethods.IsVoidType(method.ReturnType))
            {
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

            method.Body = newBody;

            // Make sure InitLocals is marked for this method

            // 15 April 2003
            // Since each method has locals added to it, need to make sure this flag is
            // on. Otherwise, the generated code cannot pass peverify.
            if (!method.InitLocals)
            {
                method.InitLocals = true;
                //WriteToLog("Setting InitLocals for method: {0}", method.FullName);
            }
        }

        private ReplaceResult ReplaceResult(Method method, Local result, Ensures e)
        {
            ReplaceResult repResult = new ReplaceResult(method, result, this.assemblyBeingRewritten);
            repResult.Visit(e);

            if (repResult.ContractResultWasCapturedInStaticContext)
            {
                this.HandleError(CreateContractResultWasCapturedInStaticContextWarning(e.Assertion.SourceContext));
            }

            return repResult;
        }

        private static Warning CreateContractResultWasCapturedInStaticContextWarning(SourceContext sourceContext)
        {
            return
                new Warning(1099,
                    "Contract.Result<T>() was captured into the static context. Current implementation stores the result in the static field that could cause runtime issues in multithreaded environment. Consider introducing local variable to store Contract.Result<T> in to avoid this issue.",
                    sourceContext);
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
                    if (!EmitEnsures(postcondition, method.DeclaringType, this.skipQuantifiers) ||
                        asyncPostconditions.Contains(postcondition))
                    {
                        continue;
                    }

                    EnsureAsyncBuilder(method, contractInitializationBlocks, closureLocals, ref asyncBuilder);
                    HandleContractResultErrors(asyncBuilder);

                    found = true;
                    asyncPostconditions.Add(postcondition);
                }
                if (found)
                {
                    Block oldInit = ProcessOldExpressionsInAsync(method, mc.AsyncEnsures, closureLocals,
                        ref oldLocalUniqueNameCounter, asyncBuilder.ClosureClass);

                    if (oldInit != null && oldInit.Statements != null && oldInit.Statements.Count > 0)
                    {
                        oldExpressionPreStateValues.Statements.Add(oldInit);
                    }
                }
            }
        }

        private void EnsureAsyncBuilder(Method method, List<Block> contractInitializationBlocks,
            Dictionary<TypeNode, Local> closureLocals, ref EmitAsyncClosure asyncBuilder)
        {
            if (asyncBuilder == null)
            {
                // create a wrapper
                asyncBuilder = new EmitAsyncClosure(method, this);

                contractInitializationBlocks.Add(asyncBuilder.ClosureInitializer);
                closureLocals.Add(asyncBuilder.ClosureClass, asyncBuilder.ClosureLocal);
            }
        }

        private void HandleContractResultErrors(EmitAsyncClosure asyncClosure)
        {
            if (asyncClosure == null)
            {
                return;
            }

            foreach (var context in asyncClosure.ContractResultCapturedInStaticContext)
            {
                this.HandleError(CreateContractResultWasCapturedInStaticContextWarning(context));
            }
        }

        private void EmitInterleavedValidationsAndRequires(Method method, List<Requires> inherited,
            RequiresList validations, Block newBody)
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
                foreach (var req in inherited)
                {
                    reqSequence.Add(req);
                }
            }

            FlushEmitOrdinaryRequires(reqSequence, method, newBody);
        }

        private void FlushEmitOrdinaryRequires(List<Requires> reqs, Method method, Block newBody)
        {
            Contract.Ensures(reqs.Count == 0);

            if (reqs.Count == 0) return;

            EmitRecursionGuardAroundChecks(method, newBody, EmitRequiresList(reqs));

            reqs.Clear();
        }

        private static bool BodyHasCalls(Block block)
        {
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
                b.Statements.Add(new AssignmentStatement(messageLocal,
                    new MethodCall(new MemberBinding(null, this.runtimeContracts.RaiseFailureEventMethod), elist)));

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

                    b.Statements.Add(
                        new ExpressionStatement(new MethodCall(new MemberBinding(null, assertMethod), assertelist)));

                    b.Statements.Add(skipAssert);
                }
            }
            else
            {
                b.Statements.Add(
                    new ExpressionStatement(
                        new UnaryExpression(
                            new MethodCall(new MemberBinding(null, this.runtimeContracts.RaiseFailureEventMethod), elist),
                            NodeType.Pop)));
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
            if (CodeInspector.IsRuntimeIgnored(precondition, this.runtimeContracts.ContractNodes, null, skipQuantifiers))
                return false;

            // if (precondition is RequiresOtherwise) { return Emit(RuntimeContractEmitFlags.LegacyRequires); }
            if (precondition.IsWithException)
            {
                // skip Requires<E> when emitting validations
                if (this.runtimeContracts.UseExplicitValidation) return false;
                return Emit(RuntimeContractEmitFlags.RequiresWithException);
            }

            return Emit(RuntimeContractEmitFlags.Requires);
        }

        private bool EmitEnsures(Ensures postcondition, TypeNode referencingType, bool skipQuantifiers)
        {
            if (CodeInspector.IsRuntimeIgnored(postcondition, this.runtimeContracts.ContractNodes, referencingType,
                skipQuantifiers)) return false;

            return this.Emit(RuntimeContractEmitFlags.Ensures);
        }

        private bool EmitInvariant(Invariant invariant, bool skipQuantifiers)
        {
            return !CodeInspector.IsRuntimeIgnored(invariant, this.runtimeContracts.ContractNodes, null, skipQuantifiers);
        }

        private static bool IsLiteral0Or1(Expression expr)
        {
            Literal lit = expr as Literal;
            if (lit == null) return false;

            if (!(lit.Value is int)) return false;

            int value = (int)lit.Value;
            return value == 0 || value == 1;
        }

        private IncreaseCodeCoverageOfContractCode cleanUpCodeCoverage = new IncreaseCodeCoverageOfContractCode();

        internal IncreaseCodeCoverageOfContractCode CleanUpCodeCoverage { get { return this.cleanUpCodeCoverage; } }

        internal class IncreaseCodeCoverageOfContractCode : Inspector
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

        internal void EmitRecursionGuardAroundChecks(Method method, Block newBody, StatementList checks)
        {
            StatementList stmts = new StatementList();
            Block preconditionsStart = new Block(stmts);
            Block finallyStart = null;
            Block finallyEnd = new Block(); // branch target for skipping the checks

            // test if we are an auto property and need to disable the check if we are in construction
            if (this.ReentrancyFlag != null && (method.IsPropertyGetter || method.IsPropertySetter) &&
                HelperMethods.IsAutoPropertyMember(method) && !method.IsStatic)
            {
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

                newBody.Statements.Add(
                    new Branch(
                        new BinaryExpression(new MemberBinding(null, this.runtimeContracts.InContractEvaluationField),
                            new Literal(this.runtimeContracts.RecursionGuardCountFor(method), SystemTypes.Int32),
                            NodeType.Gt), finallyEnd));

                stmts.Add(
                    new AssignmentStatement(new MemberBinding(null, this.runtimeContracts.InContractEvaluationField),
                        new BinaryExpression(new MemberBinding(null, this.runtimeContracts.InContractEvaluationField),
                            Literal.Int32One, NodeType.Add)));

                stmts.Add(new Block(checks));
                var leave = new Branch();
                leave.Target = finallyEnd;
                leave.LeavesExceptionBlock = true;
                stmts.Add(leave);

                finallyStart.Statements.Add(
                    new AssignmentStatement(new MemberBinding(null, this.runtimeContracts.InContractEvaluationField),
                        new BinaryExpression(new MemberBinding(null, this.runtimeContracts.InContractEvaluationField),
                            Literal.Int32One, NodeType.Sub)));

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

        private bool NeedsRecursionGuard(Method method, StatementList checks)
        {
            if (ContractContainsOnlyKnownMethodCalls(checks)) return false;

            if (method is InstanceInitializer) return false;

            if (this.runtimeContracts.RecursionGuardCountFor(method) <= 0) return false;

            return true;
        }

        private static bool ContractContainsOnlyKnownMethodCalls(StatementList checks)
        {
            if (checks == null) return true;

            if (checks.Count == 0) return true;

            var unknownMethodCallCounter = new UnknownCallCounter(true);
            unknownMethodCallCounter.VisitStatementList(checks);

            return unknownMethodCallCounter.Count == 0;
        }

        private class UnknownCallCounter : Inspector
        {
            private bool ignoreVoidMethods;

            public UnknownCallCounter(bool ignoreVoidMethods)
            {
                this.ignoreVoidMethods = ignoreVoidMethods;
            }

            public int Count { get; private set; }

            public override void VisitMethodCall(MethodCall call)
            {
                if (call == null) return;

                MemberBinding mb = call.Callee as MemberBinding;
                if (mb != null)
                {
                    Method method = mb.BoundMember as Method;
                    if (method != null)
                    {
                        HandleMethod(method);
                    }
                }

                base.VisitMethodCall(call);
            }

            private static readonly Identifier MathIdentifier = Identifier.For("Math");

            private void HandleMethod(Method method)
            {
                // F:
                Contract.Requires(method != null);

                if (this.ignoreVoidMethods && HelperMethods.IsVoidType(method.ReturnType))
                    return; // contract calls and validators are okay

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
                }
                else
                {
                    RequiresPlain reqplain = r as RequiresPlain;
                    Method reqMethod;

                    if (reqplain != null && reqplain.ExceptionType != null)
                    {
                        reqMethod = this.runtimeContracts.RequiresWithExceptionMethod.GetTemplateInstance(null,
                            reqplain.ExceptionType);
                    }
                    else
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
                    }
                    else
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

        private bool MethodShouldHaveInvariantChecked(Method method, bool inExceptionCase = false)
        {
            return
                this.Emit(RuntimeContractEmitFlags.Invariants)
                && !method.IsStatic
                // Check invariants even for non-public ctors
                && (method.IsPublic || method is InstanceInitializer)
                && this.InvariantMethod != null
                && !(inExceptionCase && method is InstanceInitializer)
                && !rewriterNodes.IsPure(method) // don't check invariant for pure methods
                && !RewriteHelper.Implements(method, this.IDisposeMethod)
                && !RewriteHelper.IsFinalizer(method);
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

            if (instantiatedMethod.IsVirtual && HelperMethods.DoesInheritContracts(instantiatedMethod))
            {
                // find method introducing this

                // first check for implemented interfaces
                if (instantiatedMethod.ImplementedInterfaceMethods != null)
                {
                    foreach (Method interfaceMethod in instantiatedMethod.ImplementedInterfaceMethods)
                    {
                        if (interfaceMethod != null)
                        {
                            return interfaceMethod;
                        }
                    }
                }

                // check for implicit interface implementations
                if (instantiatedMethod.ImplicitlyImplementedInterfaceMethods != null)
                {
                    foreach (Method interfaceMethod in instantiatedMethod.ImplicitlyImplementedInterfaceMethods)
                    {
                        if (interfaceMethod != null)
                        {
                            return interfaceMethod;
                        }
                    }
                }

                // now check for base overrides
                if ((instantiatedMethod.Flags & MethodFlags.NewSlot) != 0) return instantiatedMethod;

                // search overrides
                if (instantiatedMethod.OverridesBaseClassMember && instantiatedMethod.DeclaringType != null &&
                    instantiatedMethod.OverriddenMethod != null)
                {
                    for (TypeNode super = instantiatedMethod.DeclaringType.BaseType;
                        super != null;
                        super = super.BaseType)
                    {
                        var candidate = super.GetImplementingMethod(instantiatedMethod.OverriddenMethod, false);
                        if (candidate != null)
                        {
                            return GetRootMethod(candidate);
                        }
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
        private Block ProcessOldExpressions(Method method, EnsuresList postconditions,
            Dictionary<TypeNode, Local> closureLocals, ref int oldLocalNameCounter)
        {
            Contract.Requires(postconditions != null);

            CollectOldExpressions coe = new CollectOldExpressions(
                this.module, method, this.rewriterNodes, closureLocals, oldLocalNameCounter);

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

        private Block ProcessOldExpressionsInAsync(Method method, EnsuresList asyncpostconditions,
            Dictionary<TypeNode, Local> closureLocals, ref int oldLocalNameCounter, Class asyncClosure)
        {
            Contract.Requires(asyncpostconditions != null);

            CollectOldExpressions coe = new CollectOldExpressions(
                this.module, method, this.rewriterNodes, closureLocals, oldLocalNameCounter, asyncClosure);

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
            if (ContractNodes.IsAlreadyRewritten(assembly))
            {
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

                    if (!HelperMethods.IsContractOptionAttribute(attributeList[i], out category, out setting, out toggle))
                    {
                        continue;
                    }

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

        // Runtime Contract attribute

        /// <summary>
        /// Adds a flag to an assembly that designates it as having runtime contract checks.
        /// Does this by defining the type of the attribute and then marking the assembly with
        /// and instance of that attribute.
        /// </summary>
        /// <param name="assembly">Assembly to flag.</param>
        private void SetRuntimeContractFlag(AssemblyNode assembly)
        {
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
            EnumNode runtimeContractsFlags =
                assembly.GetType(ContractNodes.ContractNamespace, Identifier.For("RuntimeContractsFlags")) as EnumNode;

            Class RuntimeContractsAttributeClass =
                assembly.GetType(ContractNodes.ContractNamespace, Identifier.For("RuntimeContractsAttribute")) as Class;

            if (runtimeContractsFlags == null)
            {
                // Add [Flags]

                Member flagsConstructor = RewriteHelper.flagsAttributeNode.GetConstructor();
                AttributeNode flagsAttribute = new AttributeNode(new MemberBinding(null, flagsConstructor), null,
                    AttributeTargets.Class);

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

            InstanceInitializer ctor = (RuntimeContractsAttributeClass == null)
                ? null
                : RuntimeContractsAttributeClass.GetConstructor(runtimeContractsFlags);

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

            if (ctor == null)
            {
                Block returnBlock = new Block(new StatementList(new Return()));

                Block body = new Block(new StatementList());
                Block b = new Block(new StatementList());
                ParameterList pl = new ParameterList();

                Parameter levelParameter = new Parameter(Identifier.For("contractFlags"), runtimeContractsFlags);
                pl.Add(levelParameter);

                ctor = new InstanceInitializer(RuntimeContractsAttributeClass, null, pl, body);
                ctor.Flags = MethodFlags.Assembly | MethodFlags.HideBySig | MethodFlags.SpecialName |
                             MethodFlags.RTSpecialName;

                Method baseCtor = SystemTypes.Attribute.GetConstructor();

                b.Statements.Add(
                    new ExpressionStatement(new MethodCall(new MemberBinding(null, baseCtor),
                        new ExpressionList(ctor.ThisParameter))));

                b.Statements.Add(returnBlock);
                body.Statements.Add(b);

                RuntimeContractsAttributeClass.Members.Add(ctor);
            }

            return ctor;
        }

        private static void AddEnumValue(EnumNode enumType, string name, object value)
        {
            var enumValue = new Field(enumType, null,
                FieldFlags.Assembly | FieldFlags.HasDefault | FieldFlags.Literal | FieldFlags.Static,
                Identifier.For(name), enumType, new Literal(value, SystemTypes.Int32));

            enumType.Members.Add(enumValue);
        }

        // Call-site Requires instrumentation

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

        private TypeNode CreateWrapperType(TypeNode original, TypeNode declaringWrapper /*?*/)
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

            if (declaringWrapper != null)
            {
                wrapper.DeclaringType = declaringWrapper;
            }

            // need to specialize w.r.t type parameters due to type parameter bounds referring to each other
            var origConsolidatedTemplateParameters = original.ConsolidatedTemplateParameters;
            var wrapperConsolideateTemplateParameters = wrapper.ConsolidatedTemplateParameters;

            if (origConsolidatedTemplateParameters != null && origConsolidatedTemplateParameters.Count > 0)
            {
                var spec = new Specializer(this.assemblyBeingRewritten, origConsolidatedTemplateParameters,
                    wrapperConsolideateTemplateParameters);
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
            var flags = c.Flags & ~(TypeFlags.Interface | TypeFlags.HasSecurity | TypeFlags.VisibilityMask) |
                        TypeFlags.Abstract | TypeFlags.Sealed;

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
            if (!runtimeContracts.CallSiteRequires)
            {
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

        internal Method WrapperMethod(Method instanceMethod, bool virtcall, TypeNode constraint,
            Method methodContainingCall, Method methodWithContract, SourceContext callingContext)
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
            if (IsProtected(instanceMethod))
            {
                // can only be called on "this" of derived type, so no wrapper class
                wrapperType = methodContainingCall.DeclaringType;
                while (wrapperType.Template != null)
                {
                    wrapperType = wrapperType.Template;
                }
            }
            else
            {
                wrapperType = WrapperType(templateType);
            }

            var wrapperMethod = LookupWrapperMethod(virtcall, constraint, wrapperType, templateMethod);
            if (wrapperMethod == null)
            {
                wrapperMethod = CreateWrapperMethod(virtcall, constraint, templateMethod, templateType, wrapperType,
                    methodWithContract, instanceMethod, callingContext);

                StoreWrapperMethod(virtcall, constraint, templateMethod, wrapperMethod);
            }

            return InstantiateWrapperMethod(constraint, wrapperMethod, instanceMethod);
        }

        internal static bool IsProtected(Method instanceMethod)
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
            if (instWrapperType.TemplateParameters != null && instWrapperType.TemplateParameters.Count > 0)
            {
                instWrapperType = instWrapperType.GetGenericTemplateInstance(this.assemblyBeingRewritten,
                    originalInstanceMethod.DeclaringType.ConsolidatedTemplateArguments);
            }

            Method instMethod = GetMethodInstanceReference(wrapperMethod, instWrapperType);
            // instantiate method if generic
            if (instMethod.TemplateParameters != null && instMethod.TemplateParameters.Count > 0)
            {
                if (virtcallConstraint != null)
                {
                    TypeNodeList args = new TypeNodeList();

                    for (int i = 0; i < originalInstanceMethod.TemplateArgumentsCount(); i++)
                    {
                        args.Add(originalInstanceMethod.TemplateArguments[i]);
                    }

                    args.Add(virtcallConstraint);
                    instMethod = instMethod.GetTemplateInstance(instWrapperType, args);
                }
                else
                {
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

        internal static Field GetFieldInstanceReference(Field fieldOfGenericType, TypeNode instantiatedParentType)
        {
            return (Field)GetMemberInstanceReference(fieldOfGenericType, instantiatedParentType);
        }

        internal static Member GetMemberInstanceReference(Member memberOfGenericType, TypeNode instantiatedParentType)
        {
            // F:
            Contract.Requires(memberOfGenericType != null);
            Contract.Requires(instantiatedParentType != null);

            if (instantiatedParentType.Template == null)
            {
                return memberOfGenericType;
            }

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

            for (int i = 0; i < members.Count; i++)
            {
                if (members[i] == member)
                {
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

            if (wrapperTypeParams != null && wrapperTypeParams.Count > 0)
            {
                for (int i = 0; i < wrapperTypeParams.Count; i++)
                {
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
            if (formals.Count > 0)
            {
                return new Specializer(genericType.DeclaringModule, formals, actuals);
            }

            return null;
        }

        private Method CreateWrapperMethod(bool virtcall, TypeNode virtcallConstraint, Method templateMethod,
            TypeNode templateType, TypeNode wrapperType, Method methodWithContract, Method instanceMethod,
            SourceContext callingContext)
        {
            bool isProtected = IsProtected(templateMethod);

            Identifier name = templateMethod.Name;

            if (virtcall)
            {
                if (virtcallConstraint != null)
                {
                    name = Identifier.For("CV$" + name.Name);
                }
                else
                {
                    name = Identifier.For("V$" + name.Name);
                }
            }
            else
            {
                name = Identifier.For("NV$" + name.Name);
            }

            Duplicator dup = new Duplicator(this.assemblyBeingRewritten, wrapperType);

            TypeNodeList typeParameters = null;
            TypeNodeList typeParameterFormals = new TypeNodeList();
            TypeNodeList typeParameterActuals = new TypeNodeList();

            if (templateMethod.TemplateParameters != null)
            {
                dup.FindTypesToBeDuplicated(templateMethod.TemplateParameters);
                typeParameters = dup.VisitTypeParameterList(templateMethod.TemplateParameters);
                for (int i = 0; i < typeParameters.Count; i++)
                {
                    typeParameterFormals.Add(typeParameters[i]);
                    typeParameterActuals.Add(templateMethod.TemplateParameters[i]);
                }
            }

            ITypeParameter constraintTypeParam = null;
            if (virtcallConstraint != null)
            {
                if (typeParameters == null)
                {
                    typeParameters = new TypeNodeList();
                }

                var constraint = templateMethod.DeclaringType;
                var classConstraint = constraint as Class;
                if (classConstraint != null)
                {
                    var classParam = new MethodClassParameter();
                    classParam.BaseClass = classConstraint;
                    classParam.Name = Identifier.For("TC");
                    classParam.DeclaringType = wrapperType;

                    typeParameters.Add(classParam);

                    constraintTypeParam = classParam;
                }
                else
                {
                    var mtp = new MethodTypeParameter();
                    Interface intf = constraint as Interface;
                    if (intf != null)
                    {
                        mtp.Interfaces.Add(intf);
                    }

                    mtp.Name = Identifier.For("TC");
                    mtp.DeclaringType = wrapperType;

                    typeParameters.Add(mtp);
                    constraintTypeParam = mtp;
                }
            }

            var consolidatedTemplateTypeParameters = templateType.ConsolidatedTemplateParameters;
            if (consolidatedTemplateTypeParameters != null && consolidatedTemplateTypeParameters.Count > 0)
            {
                var consolidatedWrapperTypeParameters = wrapperType.ConsolidatedTemplateParameters;
                for (int i = 0; i < consolidatedTemplateTypeParameters.Count; i++)
                {
                    typeParameterFormals.Add(consolidatedWrapperTypeParameters[i]);
                    typeParameterActuals.Add(consolidatedTemplateTypeParameters[i]);
                }
            }

            Specializer spec = null;
            if (typeParameterActuals.Count > 0)
            {
                spec = new Specializer(this.assemblyBeingRewritten, typeParameterActuals, typeParameterFormals);
            }

            var parameters = new ParameterList();
            var asTypeConstraintTypeParam = constraintTypeParam as TypeNode;

            if (!isProtected && !templateMethod.IsStatic)
            {
                TypeNode thisType = GetThisTypeInstance(templateType, wrapperType, asTypeConstraintTypeParam);
                parameters.Add(new Parameter(Identifier.For("@this"), thisType));
            }

            for (int i = 0; i < templateMethod.Parameters.Count; i++)
            {
                parameters.Add((Parameter)dup.VisitParameter(templateMethod.Parameters[i]));
            }

            var retType = dup.VisitTypeReference(templateMethod.ReturnType);
            if (spec != null)
            {
                parameters = spec.VisitParameterList(parameters);
                retType = spec.VisitTypeReference(retType);
            }

            var wrapperMethod = new Method(wrapperType, null, name, parameters, retType, null);
            RewriteHelper.TryAddCompilerGeneratedAttribute(wrapperMethod);

            if (isProtected)
            {
                wrapperMethod.Flags = templateMethod.Flags & ~MethodFlags.Abstract;
                wrapperMethod.CallingConvention = templateMethod.CallingConvention;
            }
            else
            {
                wrapperMethod.Flags |= MethodFlags.Static | MethodFlags.Assembly;
            }

            if (constraintTypeParam != null)
            {
                constraintTypeParam.DeclaringMember = wrapperMethod;
            }

            if (typeParameters != null)
            {
                if (spec != null)
                {
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
                if (targetType.ConsolidatedTemplateParameters != null &&
                    targetType.ConsolidatedTemplateParameters.Count > 0)
                {
                    // need selfinstantiation
                    targetType = targetType.GetGenericTemplateInstance(this.assemblyBeingRewritten,
                        wrapperType.ConsolidatedTemplateParameters);
                }
            }

            Method targetMethod = GetMatchingMethod(targetType, templateMethod, wrapperMethod);
            if (targetMethod.IsGeneric)
            {
                if (typeParameters.Count > targetMethod.TemplateParameters.Count)
                {
                    // omit the extra constrained type arg.
                    TypeNodeList origArgs = new TypeNodeList();
                    for (int i = 0; i < targetMethod.TemplateParameters.Count; i++)
                    {
                        origArgs.Add(typeParameters[i]);
                    }

                    targetMethod = targetMethod.GetTemplateInstance(wrapperType, origArgs);
                }
                else
                {
                    targetMethod = targetMethod.GetTemplateInstance(wrapperType, typeParameters);
                }
            }

            MethodCall call;
            NodeType callType = virtcall ? NodeType.Callvirt : NodeType.Call;
            if (isProtected)
            {
                var mb = new MemberBinding(wrapperMethod.ThisParameter, targetMethod);
                var elist = new ExpressionList(wrapperMethod.Parameters.Count);

                for (int i = 0; i < wrapperMethod.Parameters.Count; i++)
                {
                    elist.Add(wrapperMethod.Parameters[i]);
                }

                call = new MethodCall(mb, elist, callType);
            }
            else if (templateMethod.IsStatic)
            {
                var elist = new ExpressionList(wrapperMethod.Parameters.Count);
                for (int i = 0; i < wrapperMethod.Parameters.Count; i++)
                {
                    elist.Add(wrapperMethod.Parameters[i]);
                }

                call = new MethodCall(new MemberBinding(null, targetMethod), elist, callType);
            }
            else
            {
                var mb = new MemberBinding(wrapperMethod.Parameters[0], targetMethod);
                var elist = new ExpressionList(wrapperMethod.Parameters.Count - 1);
                for (int i = 1; i < wrapperMethod.Parameters.Count; i++)
                {
                    elist.Add(wrapperMethod.Parameters[i]);
                }

                call = new MethodCall(mb, elist, callType);
            }
            if (constraintTypeParam != null)
            {
                call.Constraint = asTypeConstraintTypeParam;
            }

            if (HelperMethods.IsVoidType(templateMethod.ReturnType))
            {
                sl.Add(new ExpressionStatement(call, callingContext));
                sl.Add(new Return(callingContext));
            }
            else
            {
                sl.Add(new Return(call, callingContext));
            }

            wrapperMethod.Body = b;

            wrapperType.Members.Add(wrapperMethod);

            return wrapperMethod;
        }

        private void AddRequiresToWrapperMethod(Method wrapper, Block body, Method contractMethod)
        {
            // need to use instantiation of contractMethod at wrapper self instantiation to make this work.

            List<Requires> preconditions = new List<Requires>();
            MethodContract mc = HelperMethods.DuplicateContractAndClosureParts(wrapper, contractMethod,
                this.runtimeContracts.ContractNodes, false);

            RewriteHelper.ReplacePrivateFieldsThatHavePublicProperties(wrapper.DeclaringType,
                contractMethod.DeclaringType, mc, this.rewriterNodes);

            if (mc != null)
            {
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
            var argCount = templateMethod.Parameters == null ? 0 : templateMethod.Parameters.Count;
            TypeNode[] argTypes = new TypeNode[argCount];
            var argOffset = 0;

            if (!IsProtected(templateMethod) && !templateMethod.IsStatic)
            {
                argOffset = 1;
            }

            for (int i = 0; i < argCount; i++)
            {
                argTypes[i] = wrapperMethod.Parameters[i + argOffset].Type;
            }

            var methods = targetType.GetMethods(templateMethod.Name, argTypes);
            for (int i = 0; i < methods.Count; i++)
            {
                if (TypeParameterCount(methods[i]) == TypeParameterCount(templateMethod))
                {
                    return methods[i];
                }
            }

            Debug.Assert(false, "Wrapper instance method not found");

            return null;
        }

        private TypeNode GetThisTypeInstance(TypeNode templateType, TypeNode wrapperType, TypeNode constraintTypeParam)
        {
            if (constraintTypeParam != null)
            {
                return constraintTypeParam.GetReferenceType();
            }

            TypeNode thisType = templateType;
            if (thisType.TemplateParameters != null && thisType.TemplateParameters.Count > 0)
            {
                // need selfinstantiation
                thisType = thisType.GetGenericTemplateInstance(this.assemblyBeingRewritten, wrapperType.ConsolidatedTemplateParameters);
            }

            if (templateType.IsValueType)
            {
                thisType = thisType.GetReferenceType();
            }

            return thisType;
        }

        private void StoreWrapperMethod(bool virtcall, TypeNode virtCallConstraint, Method templateMethod, Method wrapperMethod)
        {
            if (virtcall)
            {
                if (virtCallConstraint != null)
                {
                    this.constrainedVirtualWrapperMethods.Add(templateMethod.UniqueKey, wrapperMethod);
                }
                else
                {
                    this.virtualWrapperMethods.Add(templateMethod.UniqueKey, wrapperMethod);
                }
            }
            else
            {
                this.nonVirtualWrapperMethods.Add(templateMethod.UniqueKey, wrapperMethod);
            }
        }

        private Method LookupWrapperMethod(bool virtcall, TypeNode virtCallConstraint, TypeNode wrapperType, Method templateMethod)
        {
            Method result = null;

            if (virtcall)
            {
                if (virtCallConstraint != null)
                {
                    this.constrainedVirtualWrapperMethods.TryGetValue(templateMethod.UniqueKey, out result);
                }
                else
                {
                    this.virtualWrapperMethods.TryGetValue(templateMethod.UniqueKey, out result);
                }
            }
            else
            {
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

    /// <summary>
    /// Inserts source strings for Asserts/Assumes (or removes checks based on level)
    /// Inserts call-site requires checks based on flags.
    /// </summary>
    internal sealed class RewriteAssertAssumeAndCallSiteRequires : StandardVisitor
    {
        private readonly Dictionary<Method, bool> methodTable;
        private readonly RuntimeContractMethods runtimeContracts;
        private readonly RuntimeContractEmitFlags emitFlags;

        private readonly Rewriter parent;
        private readonly Method containingMethod;
        private SourceContext lastStmtContext;
        public RewriteAssertAssumeAndCallSiteRequires(Dictionary<Method, bool> replacementMethods,
            RuntimeContractMethods runtimeContracts, RuntimeContractEmitFlags emitFlags, Rewriter parent,
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
        [ContractVerification(false)]
        private static Statement RewriteContractCall(
            MethodCall call,
            ExpressionStatement statement,
            Method /*!*/ methodToReplace,
            Method /*?*/ replacementMethod)
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
            if (casas != null)
            {
                call.Operands.Add(new Literal(casas.SourceText, SystemTypes.String));
            }
#endif
            else
            {
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

        [ContractVerification(false)]
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

                if (isAssert &&
                    (!this.emitFlags.Emit(RuntimeContractEmitFlags.Asserts) ||
                     this.runtimeContracts.PublicSurfaceOnly))
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

                if (!isAssert &&
                    (!this.emitFlags.Emit(RuntimeContractEmitFlags.Assumes) ||
                     this.runtimeContracts.PublicSurfaceOnly))
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

                var result = RewriteContractCall(call, statement, contractMethod, repMethod);

                return result;
            }

            return base.VisitExpressionStatement(statement);
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
                        var wrapped = parent.WrapperMethod(m, virtcall, call.Constraint, containingMethod,
                            methodWithContract, this.lastStmtContext);

                        if (wrapped != null)
                        {
                            if (Rewriter.IsProtected(wrapped))
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
}
