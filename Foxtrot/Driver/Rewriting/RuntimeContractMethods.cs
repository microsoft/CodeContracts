using System;
using System.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;

namespace Microsoft.Contracts.Foxtrot
{
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
        private readonly int regularRecursionGuard;
        public readonly bool HideFromDebugger;

        public bool AssertOnFailure
        {
            get { return !ThrowOnFailure; }
        }

        /// <summary>
        /// Controls how validations are emitted and how inheritance works for all requires that throw exceptions.
        /// </summary>
        public readonly bool UseExplicitValidation;

        public RuntimeContractMethods(TypeNode userContractType, ContractNodes contractNodes,
            AssemblyNode targetAssembly,
            bool throwOnFailure, int rewriteLevel, bool publicSurfaceOnly, bool callSiteRequires,
            int recursionGuard, bool hideFromDebugger,
            bool userExplicitValidation)
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

            // Get the user-specified rewriter methods (optional) REVIEW!! Needs a lot of error handling

            if (userContractType != null)
            {
                Method method = null;
                MethodList reqMethods = userContractType.GetMethods(Identifier.For("Requires"), SystemTypes.Boolean,
                    SystemTypes.String, SystemTypes.String);

                for (int i = 0; i < reqMethods.Count; i++)
                {
                    method = reqMethods[i];
                    if (method != null)
                    {
                        if (method.TemplateParameters == null || method.TemplateParameters.Count != 1)
                        {
                            /*if (method != null) */
                            this.requiresMethod = method;
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
                    foreach (var mem in mems)
                    {
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
                        foreach (var mem in mems)
                        {
                            method = mem as Method;
                            if (method == null) continue;

                            if (method.Parameters.Count != 4) continue;

                            if (method.Parameters[0].Type.Name.UniqueIdKey !=
                                contractNodes.ContractFailureKind.Name.UniqueIdKey) continue;

                            if (method.Parameters[1].Type != SystemTypes.String) continue;

                            if (method.Parameters[2].Type != SystemTypes.String) continue;

                            if (method.Parameters[3].Type != SystemTypes.Exception) continue;

                            this.raiseFailureEventMethod = method;
                            contractFailureKind = method.Parameters[0].Type;

                            break;
                        }
                    }
                }
                else
                {
                    method = userContractType.GetMethod(ContractNodes.RaiseContractFailedEventName, contractFailureKind,
                        SystemTypes.String, SystemTypes.String, SystemTypes.Exception);

                    if (method != null) this.raiseFailureEventMethod = method;
                }

                if (this.raiseFailureEventMethod != null)
                {
                    // either take all both RaiseContractFailedEvent and TriggerFailure or neither
                    method = userContractType.GetMethod(ContractNodes.TriggerFailureName, contractFailureKind,
                        SystemTypes.String, SystemTypes.String, SystemTypes.String, SystemTypes.Exception);

                    if (method != null) this.triggerFailureMethod = method;
                }
            }
        }

        public int RecursionGuardCountFor(Method method)
        {
            Contract.Requires(method != null);

            if (method.IsPropertyGetter) return Math.Min(1, regularRecursionGuard);
            return regularRecursionGuard;
        }

        // Runtime contract methods

        private Method /*?*/ requiresMethod = null;
        private Method /*?*/ requiresWithExceptionMethod = null;
        private Method /*?*/ ensuresMethod = null;
        private Method /*?*/ ensuresOnThrowMethod = null;
        private Method /*?*/ invariantMethod = null;
        private Method /*?*/ assertMethod = null;
        private Method /*?*/ assumeMethod = null;
        private Method /*?*/ failureMethod = null;

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

        // FailureKind literals

        internal Literal PreconditionKind
        {
            get { return contractNodes.ContractFailureKind.GetField(Identifier.For("Precondition")).DefaultValue; }
        }

        // other private fields

        private Class /*?*/ runtimeContractType;

        private readonly ContractNodes contractNodes;
        private readonly AssemblyNode targetAssembly;

        public ContractNodes ContractNodes
        {
            get { return this.contractNodes; }
        }

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
                    if (this.contractNodes.ContractClass.DeclaringModule == this.targetAssembly)
                    {
                        // If we're rewriting an assembly that defines the contract class itself,
                        // see if it already defines a contract exception that can be used
                        this.contractExceptionType =
                            this.targetAssembly.GetType(
                                ContractNodes.ContractNamespace,
                                Identifier.For("ContractException")) as Class;
                    }

                    // If that fails for any reason, then go ahead and make our own contract exception type
                    if (this.contractExceptionType == null)
                    {
                        this.contractExceptionType = MakeContractException();
                    }
                }

                return this.contractExceptionType;
            }
        }

        private Class MakeContractException()
        {
            Class contractExceptionType;

            // If we're rewriting an assembly for v4 or above and it *isn't* Silverlight (so serialization support is needed), then use new embedded dll as the type

            if (4 <= TargetPlatform.MajorVersion)
            {
                var iSafeSerializationData =
                    SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.Serialization"),
                        Identifier.For("ISafeSerializationData")) as Interface;

                if (iSafeSerializationData != null)
                {
                    // Just much easier to write the C# and have the compiler generate everything than to try and create it all manually
                    System.Reflection.Assembly embeddedAssembly;

                    Stream embeddedAssemblyStream;
                    embeddedAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                    embeddedAssemblyStream =
                        embeddedAssembly.GetManifestResourceStream("Microsoft.Contracts.Foxtrot.InternalException.dll");

                    byte[] data = new byte[0];
                    using (var br = new BinaryReader(embeddedAssemblyStream))
                    {
                        var len = embeddedAssemblyStream.Length;
                        if (len < Int32.MaxValue)
                            data = br.ReadBytes((int) len);

                        AssemblyNode assemblyNode = AssemblyNode.GetAssembly(data, TargetPlatform.StaticAssemblyCache, true, false, true);
                        contractExceptionType = assemblyNode.GetType(Identifier.For(""), Identifier.For("ContractException")) as Class;
                    }

                    if (contractExceptionType == null)
                        throw new RewriteException("Tried to create the ContractException type from the embedded dll, but failed");

                    var d = new Duplicator(this.targetAssembly, this.RuntimeContractType);
                    d.FindTypesToBeDuplicated(new TypeNodeList(contractExceptionType));

                    var ct = d.Visit(contractExceptionType);

                    contractExceptionType = (Class) ct;
                    contractExceptionType.Flags |= TypeFlags.NestedPrivate;

                    this.RuntimeContractType.Members.Add(contractExceptionType);
                    return contractExceptionType;
                }
            }

            contractExceptionType = new Class(this.targetAssembly, this.RuntimeContractType, new AttributeList(),
                TypeFlags.Class | TypeFlags.NestedPrivate | TypeFlags.Serializable, null,
                Identifier.For("ContractException"), SystemTypes.Exception, null, null);

            RewriteHelper.TryAddCompilerGeneratedAttribute(contractExceptionType);

            var kindField = new Field(contractExceptionType, null, FieldFlags.Private, Identifier.For("_Kind"), contractNodes.ContractFailureKind, null);
            var userField = new Field(contractExceptionType, null, FieldFlags.Private, Identifier.For("_UserMessage"), SystemTypes.String, null);
            var condField = new Field(contractExceptionType, null, FieldFlags.Private, Identifier.For("_Condition"), SystemTypes.String, null);

            contractExceptionType.Members.Add(kindField);
            contractExceptionType.Members.Add(userField);
            contractExceptionType.Members.Add(condField);

            // Constructor for setting the fields

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

            body.Statements.Add(
                new ExpressionStatement(
                    new MethodCall(
                        new MemberBinding(ctor.ThisParameter,
                            contractExceptionType.BaseClass.GetConstructor(SystemTypes.String, SystemTypes.Exception)),
                        new ExpressionList(failureParam, innerParam))));

            body.Statements.Add(new AssignmentStatement(new MemberBinding(ctor.ThisParameter, kindField), kindParam));
            body.Statements.Add(new AssignmentStatement(new MemberBinding(ctor.ThisParameter, userField), usermsgParam));
            body.Statements.Add(new AssignmentStatement(new MemberBinding(ctor.ThisParameter, condField), conditionParam));

            body.Statements.Add(new Return());

            contractExceptionType.Members.Add(ctor);

            if (SystemTypes.SerializationInfo != null && SystemTypes.SerializationInfo.BaseClass != null)
            {
                // Silverlight (e.g.) is a platform that doesn't support serialization. So check to make sure the type really exists.
                // 
                var baseCtor = SystemTypes.Exception.GetConstructor(SystemTypes.SerializationInfo,
                    SystemTypes.StreamingContext);

                if (baseCtor != null)
                {
                    // Deserialization Constructor

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
                    body.Statements.Add(
                        new ExpressionStatement(new MethodCall(new MemberBinding(ctor.ThisParameter, baseCtor),
                            new ExpressionList(info, context))));

                    // _Kind = (ContractFailureKind)info.GetInt32("Kind");
                    var getInt32 = SystemTypes.SerializationInfo.GetMethod(Identifier.For("GetInt32"), SystemTypes.String);

                    body.Statements.Add(new AssignmentStatement(
                        new MemberBinding(new This(), kindField),
                        new MethodCall(new MemberBinding(info, getInt32),
                            new ExpressionList(new Literal("Kind", SystemTypes.String)))
                        ));

                    // _UserMessage = info.GetString("UserMessage");
                    var getString = SystemTypes.SerializationInfo.GetMethod(Identifier.For("GetString"), SystemTypes.String);
                    body.Statements.Add(new AssignmentStatement(
                        new MemberBinding(new This(), userField),
                        new MethodCall(new MemberBinding(info, getString),
                            new ExpressionList(new Literal("UserMessage", SystemTypes.String)))
                        ));

                    // _Condition = info.GetString("Condition");
                    body.Statements.Add(new AssignmentStatement(
                        new MemberBinding(new This(), condField),
                        new MethodCall(new MemberBinding(info, getString),
                            new ExpressionList(new Literal("Condition", SystemTypes.String)))
                        ));

                    body.Statements.Add(new Return());
                    contractExceptionType.Members.Add(ctor);

                    // GetObjectData

                    var securityCriticalCtor = SystemTypes.SecurityCriticalAttribute.GetConstructor();
                    var securityCriticalAttribute = new AttributeNode(new MemberBinding(null, securityCriticalCtor),
                        null, System.AttributeTargets.Method);

                    var attrs = new AttributeList(securityCriticalAttribute);
                    parameters = new ParameterList();

                    info = new Parameter(Identifier.For("info"), SystemTypes.SerializationInfo);
                    context = new Parameter(Identifier.For("context"), SystemTypes.StreamingContext);

                    parameters.Add(info);
                    parameters.Add(context);

                    body = new Block(new StatementList());
                    var getObjectDataName = Identifier.For("GetObjectData");
                    var getObjectData = new Method(contractExceptionType, attrs, getObjectDataName, parameters,
                        SystemTypes.Void, body);

                    getObjectData.CallingConvention = CallingConventionFlags.HasThis;

                    // public override
                    getObjectData.Flags = MethodFlags.Public | MethodFlags.Virtual;

                    // base.GetObjectData(info, context);
                    var baseGetObjectData = SystemTypes.Exception.GetMethod(getObjectDataName,
                        SystemTypes.SerializationInfo, SystemTypes.StreamingContext);
                    body.Statements.Add(new ExpressionStatement(
                        new MethodCall(new MemberBinding(new This(), baseGetObjectData),
                            new ExpressionList(info, context), NodeType.Call, SystemTypes.Void)
                        ));

                    // info.AddValue("Kind", _Kind);
                    var addValueObject = SystemTypes.SerializationInfo.GetMethod(Identifier.For("AddValue"),
                        SystemTypes.String, SystemTypes.Object);
                    body.Statements.Add(new ExpressionStatement(
                        new MethodCall(new MemberBinding(info, addValueObject),
                            new ExpressionList(new Literal("Kind", SystemTypes.String),
                                new BinaryExpression(new MemberBinding(new This(), kindField),
                                    new Literal(contractNodes.ContractFailureKind), NodeType.Box)), NodeType.Call,
                            SystemTypes.Void)
                        ));

                    // info.AddValue("UserMessage", _UserMessage);
                    body.Statements.Add(new ExpressionStatement(
                        new MethodCall(new MemberBinding(info, addValueObject),
                            new ExpressionList(new Literal("UserMessage", SystemTypes.String),
                                new MemberBinding(new This(), userField)), NodeType.Call, SystemTypes.Void)
                        ));

                    // info.AddValue("Condition", _Condition);
                    body.Statements.Add(new ExpressionStatement(
                        new MethodCall(new MemberBinding(info, addValueObject),
                            new ExpressionList(new Literal("Condition", SystemTypes.String),
                                new MemberBinding(new This(), condField)), NodeType.Call, SystemTypes.Void)
                        ));

                    body.Statements.Add(new Return());
                    contractExceptionType.Members.Add(getObjectData);
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

                var result = contractException.GetConstructor(contractNodes.ContractFailureKind, SystemTypes.String,
                    SystemTypes.String, SystemTypes.String, SystemTypes.Exception);

                if (result == null)
                {
                    throw new ApplicationException(
                        "Can't find constructor ContractException(ContractFailureKind, string, string, string, Exception).");
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
                    inContractEvaluationField = new Field(RuntimeContractType, null,
                        FieldFlags.Assembly | FieldFlags.Static, Identifier.For("insideContractEvaluation"),
                        SystemTypes.Int32, null);

                    TypeNode threadStaticAttribute = SystemTypes.SystemAssembly.GetType(Identifier.For("System"),
                        Identifier.For("ThreadStaticAttribute"));

                    if (threadStaticAttribute != null)
                    {
                        inContractEvaluationField.Attributes.Add(
                            new AttributeNode(new MemberBinding(null, threadStaticAttribute.GetConstructor()), null));
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

            // Add [DebuggerNonUserCodeAttribute]

            if (this.HideFromDebugger)
            {
                TypeNode debuggerNonUserCode = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Diagnostics"),
                    Identifier.For("DebuggerNonUserCodeAttribute"));

                if (debuggerNonUserCode != null)
                {
                    constructor = debuggerNonUserCode.GetConstructor();
                    attribute = new AttributeNode(new MemberBinding(null, constructor), null, AttributeTargets.Method);
                    m.Attributes.Add(attribute);
                }
            }

            TypeNode reliabilityContract =
                SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"),
                    Identifier.For("ReliabilityContractAttribute"));

            TypeNode consistency =
                SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"),
                    Identifier.For("Consistency"));

            TypeNode cer = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"),
                Identifier.For("Cer"));

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
                    {
                        // Old CLR 4.0 beta
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

            b.Statements.Add(new AssignmentStatement(messageLocal,
                new MethodCall(new MemberBinding(null, this.RaiseFailureEventMethod), elist)));

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

                    b.Statements.Add(
                        new ExpressionStatement(new MethodCall(new MemberBinding(null, assertMethod), assertelist)));

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

            var constructorInfoType = TypeNode.GetTypeNode(typeof (System.Reflection.ConstructorInfo));

            Contract.Assume(constructorInfoType != null);

            var methodBaseType = TypeNode.GetTypeNode(typeof (System.Reflection.MethodBase));

            Contract.Assume(methodBaseType != null);

            var constructorLocal = new Local(Identifier.For("ci"), constructorInfoType);

            Contract.Assume(SystemTypes.Type != null);

            var getConstructorMethod = SystemTypes.Type.GetMethod(Identifier.For("GetConstructor"),
                SystemTypes.Type.GetArrayType(1));

            var typeofExceptionArg = GetTypeFromHandleExpression(typeArg);
            var typeofString = GetTypeFromHandleExpression(SystemTypes.String);
            var typeArrayLocal = new Local(Identifier.For("typeArray"), SystemTypes.Type.GetArrayType(1));
            var typeArray2 = new ConstructArray();

            typeArray2.ElementType = SystemTypes.Type;
            typeArray2.Rank = 1;
            typeArray2.Operands = new ExpressionList(Literal.Int32Two);
            var typeArrayInit2 = new Block(new StatementList());

            typeArrayInit2.Statements.Add(new AssignmentStatement(typeArrayLocal, typeArray2));
            typeArrayInit2.Statements.Add(
                new AssignmentStatement(
                    new Indexer(typeArrayLocal, new ExpressionList(Literal.Int32Zero), SystemTypes.Type), typeofString));

            typeArrayInit2.Statements.Add(
                new AssignmentStatement(
                    new Indexer(typeArrayLocal, new ExpressionList(Literal.Int32One), SystemTypes.Type), typeofString));

            typeArrayInit2.Statements.Add(new ExpressionStatement(typeArrayLocal));
            var typeArrayExpression2 = new BlockExpression(typeArrayInit2);
            b.Statements.Add(new AssignmentStatement(constructorLocal,
                new MethodCall(new MemberBinding(typeofExceptionArg, getConstructorMethod),
                    new ExpressionList(typeArrayExpression2))));

            var elseBlock = new Block();
            b.Statements.Add(new Branch(new UnaryExpression(constructorLocal, NodeType.LogicalNot), elseBlock));
            var endifBlock2 = new Block();

            var invokeMethod = constructorInfoType.GetMethod(Identifier.For("Invoke"),
                SystemTypes.Object.GetArrayType(1));

            var argArray2 = new ConstructArray();
            argArray2.ElementType = SystemTypes.Object;
            argArray2.Rank = 1;
            argArray2.Operands = new ExpressionList(Literal.Int32Two);
            var argArrayLocal = new Local(Identifier.For("argArray"), SystemTypes.Object.GetArrayType(1));

            var parameterInfoType = TypeNode.GetTypeNode(typeof (System.Reflection.ParameterInfo));

            Contract.Assume(parameterInfoType != null);

            var parametersMethod = methodBaseType.GetMethod(Identifier.For("GetParameters"));
            var get_NameMethod = parameterInfoType.GetMethod(Identifier.For("get_Name"));
            var string_op_EqualityMethod = SystemTypes.String.GetMethod(Identifier.For("op_Equality"),
                SystemTypes.String, SystemTypes.String);

            var elseArgMsgBlock = new Block();
            var endIfArgMsgBlock = new Block();

            b.Statements.Add(
                new Branch(
                    new UnaryExpression(
                        new MethodCall(new MemberBinding(null, string_op_EqualityMethod),
                            new ExpressionList(
                                new MethodCall(
                                    new MemberBinding(
                                        new Indexer(
                                            new MethodCall(new MemberBinding(constructorLocal, parametersMethod),
                                                new ExpressionList(), NodeType.Callvirt),
                                            new ExpressionList(Literal.Int32Zero), parameterInfoType), get_NameMethod),
                                    new ExpressionList(), NodeType.Callvirt),
                                new Literal("paramName", SystemTypes.String))), NodeType.LogicalNot), elseArgMsgBlock));

            var argArrayInit2 = new Block(new StatementList());
            argArrayInit2.Statements.Add(new AssignmentStatement(argArrayLocal, argArray2));
            argArrayInit2.Statements.Add(
                new AssignmentStatement(
                    new Indexer(argArrayLocal, new ExpressionList(Literal.Int32Zero), SystemTypes.Object),
                    messageParameter));

            argArrayInit2.Statements.Add(
                new AssignmentStatement(
                    new Indexer(argArrayLocal, new ExpressionList(Literal.Int32One), SystemTypes.Object), messageLocal));
            argArrayInit2.Statements.Add(new ExpressionStatement(argArrayLocal));

            var argArrayExpression2 = new BlockExpression(argArrayInit2);
            b.Statements.Add(new AssignmentStatement(exceptionLocal,
                new BinaryExpression(
                    new MethodCall(new MemberBinding(constructorLocal, invokeMethod),
                        new ExpressionList(argArrayExpression2)), new Literal(SystemTypes.Exception), NodeType.Isinst)));
            b.Statements.Add(new Branch(null, endIfArgMsgBlock));

            b.Statements.Add(elseArgMsgBlock);

            var argArrayInit2_1 = new Block(new StatementList());
            argArrayInit2_1.Statements.Add(new AssignmentStatement(argArrayLocal, argArray2));
            argArrayInit2_1.Statements.Add(
                new AssignmentStatement(
                    new Indexer(argArrayLocal, new ExpressionList(Literal.Int32Zero), SystemTypes.Object), messageLocal));

            argArrayInit2_1.Statements.Add(
                new AssignmentStatement(
                    new Indexer(argArrayLocal, new ExpressionList(Literal.Int32One), SystemTypes.Object),
                    messageParameter));
            argArrayInit2_1.Statements.Add(new ExpressionStatement(argArrayLocal));

            var argArrayExpression2_1 = new BlockExpression(argArrayInit2_1);
            b.Statements.Add(new AssignmentStatement(exceptionLocal,
                new BinaryExpression(
                    new MethodCall(new MemberBinding(constructorLocal, invokeMethod),
                        new ExpressionList(argArrayExpression2_1)), new Literal(SystemTypes.Exception), NodeType.Isinst)));

            b.Statements.Add(endIfArgMsgBlock);

            b.Statements.Add(new Branch(null, endifBlock2));

            b.Statements.Add(elseBlock);

            var typeArray1 = new ConstructArray();
            typeArray1.ElementType = SystemTypes.Type;
            typeArray1.Rank = 1;
            typeArray1.Operands = new ExpressionList(Literal.Int32One);

            var typeArrayInit1 = new Block(new StatementList());
            typeArrayInit1.Statements.Add(new AssignmentStatement(typeArrayLocal, typeArray1));
            typeArrayInit1.Statements.Add(
                new AssignmentStatement(
                    new Indexer(typeArrayLocal, new ExpressionList(Literal.Int32Zero), SystemTypes.Type), typeofString));

            typeArrayInit1.Statements.Add(new ExpressionStatement(typeArrayLocal));
            var typeArrayExpression1 = new BlockExpression(typeArrayInit1);

            b.Statements.Add(new AssignmentStatement(constructorLocal,
                new MethodCall(new MemberBinding(typeofExceptionArg, getConstructorMethod),
                    new ExpressionList(typeArrayExpression1))));

            b.Statements.Add(new Branch(new UnaryExpression(constructorLocal, NodeType.LogicalNot), endifBlock2));

            var argArray1 = new ConstructArray();
            argArray1.ElementType = SystemTypes.Object;
            argArray1.Rank = 1;
            argArray1.Operands = new ExpressionList(Literal.Int32One);

            var argArrayInit1 = new Block(new StatementList());
            argArrayInit1.Statements.Add(new AssignmentStatement(argArrayLocal, argArray1));
            argArrayInit1.Statements.Add(
                new AssignmentStatement(
                    new Indexer(argArrayLocal, new ExpressionList(Literal.Int32Zero), SystemTypes.Object), messageLocal));

            argArrayInit1.Statements.Add(new ExpressionStatement(argArrayLocal));
            var argArrayExpression1 = new BlockExpression(argArrayInit1);

            b.Statements.Add(new AssignmentStatement(exceptionLocal,
                new BinaryExpression(
                    new MethodCall(new MemberBinding(constructorLocal, invokeMethod),
                        new ExpressionList(argArrayExpression1)), new Literal(SystemTypes.Exception), NodeType.Isinst)));

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
            b.Statements.Add(
                new Throw(
                    new Construct(
                        new MemberBinding(null,
                            SystemTypes.ArgumentException.GetConstructor(SystemTypes.String, SystemTypes.String)),
                        new ExpressionList(messageLocal, messageParameter))));

            b.Statements.Add(thenBlock3);
            b.Statements.Add(new Throw(exceptionLocal));
            b.Statements.Add(returnBlock);

            Contract.Assume(body.Statements != null);

            body.Statements.Add(b);

            Member constructor = null;
            AttributeNode attribute = null;

            // Add [DebuggerNonUserCodeAttribute]

            if (this.HideFromDebugger)
            {
                TypeNode debuggerNonUserCode = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Diagnostics"),
                    Identifier.For("DebuggerNonUserCodeAttribute"));

                if (debuggerNonUserCode != null)
                {
                    constructor = debuggerNonUserCode.GetConstructor();
                    attribute = new AttributeNode(new MemberBinding(null, constructor), null, AttributeTargets.Method);
                    m.Attributes.Add(attribute);
                }
            }

            TypeNode reliabilityContract =
                SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"),
                    Identifier.For("ReliabilityContractAttribute"));

            TypeNode consistency =
                SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"),
                    Identifier.For("Consistency"));

            TypeNode cer = SystemTypes.SystemAssembly.GetType(Identifier.For("System.Runtime.ConstrainedExecution"),
                Identifier.For("Cer"));

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
            var sysref = (AssemblyReference) TargetPlatform.AssemblyReferenceFor[Identifier.For("System").UniqueIdKey];
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
            return
                new MethodCall(
                    new MemberBinding(null,
                        SystemTypes.Type.GetMethod(Identifier.For("GetTypeFromHandle"), SystemTypes.RuntimeTypeHandle)),
                    new ExpressionList(new UnaryExpression(new Literal(typeArg), NodeType.Ldtoken)));
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

            body.Statements.Add(new AssignmentStatement(messageLocal,
                new MethodCall(new MemberBinding(null, this.RaiseFailureEventMethod), elist)));

            body.Statements.Add(new Branch(new UnaryExpression(messageLocal, NodeType.LogicalNot), returnBlock));

            body.Statements.Add(
                new ExpressionStatement(new MethodCall(new MemberBinding(null, this.TriggerFailureMethod),
                    new ExpressionList(kindParameter, messageLocal, messageParameter, conditionTextParameter,
                        exceptionParameter))));

            body.Statements.Add(returnBlock);

            return m;
        }

        private Method MakeRaiseFailureEventMethod()
        {
            Parameter kindParameter = new Parameter(Identifier.For("kind"), contractNodes.ContractFailureKind);
            Parameter messageParameter = new Parameter(Identifier.For("msg"), SystemTypes.String);
            Parameter conditionTextParameter = new Parameter(Identifier.For("conditionTxt"), SystemTypes.String);
            Parameter exceptionParameter = new Parameter(Identifier.For("inner"), SystemTypes.Exception);
            ParameterList pl = new ParameterList(kindParameter, messageParameter, conditionTextParameter,exceptionParameter);

            Block body = new Block(new StatementList());
            Method m = new Method(this.RuntimeContractType, null, ContractNodes.RaiseContractFailedEventName, pl,
                SystemTypes.String, body);

            m.Flags = MethodFlags.Assembly | MethodFlags.Static;
            m.Attributes = new AttributeList();

            this.RuntimeContractType.Members.Add(m);

            //
            // Generate the following
            //

            // return String.Format("{0} failed: {1}: {2}", box(kind), conditionText, message);

            var stringFormat3 = SystemTypes.String.GetMethod(Identifier.For("Format"), SystemTypes.String,
                SystemTypes.Object, SystemTypes.Object, SystemTypes.Object);

            body.Statements.Add(
                new Return(new MethodCall(new MemberBinding(null, stringFormat3),
                    new ExpressionList(new Literal("{0} failed: {1}: {2}"),
                        new BinaryExpression(kindParameter, new Literal(contractNodes.ContractFailureKind), NodeType.Box),
                        conditionTextParameter, messageParameter))));

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
                if (sysassem == null)
                {
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
}