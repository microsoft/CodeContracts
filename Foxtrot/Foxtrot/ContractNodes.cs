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
using System.Compiler;
using System.CodeDom.Compiler; // needed for CompilerError
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    internal sealed class RepresentationFor : Attribute
    {
        internal string runtimeName;
        internal bool required;

        internal RepresentationFor(string s)
        {
            this.runtimeName = s;
            this.required = true;
        }

        internal RepresentationFor(string s, bool required)
        {
            this.runtimeName = s;
            this.required = required;
        }
    }

    /// <summary>
    /// Contains CCI nodes for common types and members in the contract library.
    /// </summary>
    /// <remarks>
    /// This class could be static if it referenced the Contracts assembly statically as a project reference.
    /// </remarks>
    public class ContractNodes
    {
        /// <summary>Namespace that the contract types are defined in.</summary>
        public static readonly Identifier ContractNamespace = Identifier.For("System.Diagnostics.Contracts");

        public static readonly Identifier ContractClassName = Identifier.For("Contract");
        public static readonly Identifier ContractInternalNamespace = Identifier.For("System.Runtime.CompilerServices");
        
        //public static readonly Identifier ContractInternalNamespace = Identifier.For("System.Diagnostics.Contracts.Internal");
        
        public static readonly Identifier RaiseContractFailedEventName = Identifier.For("RaiseContractFailedEvent");
        public static readonly Identifier TriggerFailureName = Identifier.For("TriggerFailure");
        public static readonly Identifier ReportFailureName = Identifier.For("ReportFailure");
        public static readonly Identifier RequiresName = Identifier.For("Requires");
        public static readonly Identifier RequiresAlwaysName = Identifier.For("RequiresAlways");
        public static readonly Identifier EnsuresName = Identifier.For("Ensures");
        public static readonly Identifier EnsuresOnThrowName = Identifier.For("EnsuresOnThrow");
        public static readonly Identifier InvariantName = Identifier.For("Invariant");
        public static readonly Identifier ResultName = Identifier.For("Result");
        public static readonly Identifier OldName = Identifier.For("OldValue");
        public static readonly Identifier ValueAtReturnName = Identifier.For("ValueAtReturn");
        public static readonly Identifier ForallName = Identifier.For("ForAll");
        public static readonly Identifier ExistsName = Identifier.For("Exists");
        public static readonly Identifier ValidatorAttributeName = Identifier.For("ContractArgumentValidatorAttribute");
        public static readonly Identifier AbbreviatorAttributeName = Identifier.For("ContractAbbreviatorAttribute");
        public static readonly Identifier ContractOptionAttributeName = Identifier.For("ContractOptionAttribute");
        public static readonly Identifier RuntimeIgnoredAttributeName = Identifier.For("ContractRuntimeIgnoredAttribute");
        public static readonly Identifier ModelAttributeName = Identifier.For("ContractModelAttribute");
        public static readonly Identifier ContractClassAttributeName = Identifier.For("ContractClassAttribute");
        public static readonly Identifier ContractClassForAttributeName = Identifier.For("ContractClassForAttribute");
        public static readonly Identifier SpecPublicAttributeName = Identifier.For(SpecPublicAttributeStringName);

        /// <summary>
        /// Event that is raised when an error is found.
        /// </summary>
        public event Action<CompilerError> ErrorFound;

        /// <summary>
        /// Need this just so other classes that have a reference to this instance can call it.
        /// For some reason it can be called only from within this class??
        /// </summary>
        /// <param name="e">The error to pass to ErrorFound</param>
        public void CallErrorFound(CompilerError e)
        {
            ErrorFound(e);
        }

        // Fields that represent methods, attributes, and types in the contract library

        [RepresentationFor("System.Diagnostics.Contracts.PureAttribute")] public readonly Class /*?*/ PureAttribute;

        [RepresentationFor("System.Diagnostics.Contracts.ContractInvariantMethodAttribute")] public readonly Class /*?*/ InvariantMethodAttribute;

        [RepresentationFor("System.Diagnostics.Contracts.ContractClassAttribute")] internal readonly Class /*?*/ ContractClassAttribute;

        [RepresentationFor("System.Diagnostics.Contracts.ContractClassForAttribute")] internal readonly Class /*?*/ ContractClassForAttribute;

        [RepresentationFor("System.Diagnostics.Contracts.ContractVerificationAttribute")] public readonly Class /*?*/ VerifyAttribute;

        [RepresentationFor("System.Diagnostics.Contracts.ContractPublicPropertyNameAttribute")] internal readonly Class /*?*/ SpecPublicAttribute;

        [RepresentationFor("System.Diagnostics.Contracts.ContractReferenceAssemblyAttribute", false)] public readonly Class /*?*/ ReferenceAssemblyAttribute;

        [RepresentationFor("System.Diagnostics.Contracts.ContractRuntimeIgnoredAttribute")] public readonly Class /*?*/ IgnoreAtRuntimeAttribute;

        [RepresentationFor("System.Diagnostics.Contracts.ContractClass")] public readonly Class /*?*/ ContractClass;

        [RepresentationFor("System.Diagnostics.Contracts.Internal.ContractHelper", false)] public readonly Class /*?*/ ContractHelperClass;

        [RepresentationFor("System.Diagnostics.Contracts.ContractFailureKind")] public readonly EnumNode /*?*/ ContractFailureKind;

        [RepresentationFor("Contract.Assert(bool)")] public readonly Method /*?*/ AssertMethod;
        [RepresentationFor("Contract.Assert(bool, string)")] public readonly Method /*?*/ AssertWithMsgMethod;
        [RepresentationFor("Contract.Assume(bool b)")] public readonly Method /*?*/ AssumeMethod;
        [RepresentationFor("Contract.Assume(bool b, string)")] public readonly Method /*?*/ AssumeWithMsgMethod;

        [RepresentationFor("Contract.Requires(bool)")] public readonly Method /*?*/ RequiresMethod;
        [RepresentationFor("Contract.Requires(bool,string)")] public readonly Method /*?*/ RequiresWithMsgMethod;

        [RepresentationFor("Contract.Requires<TException>(bool)", false)] public readonly Method /*?*/ RequiresExceptionMethod;

        [RepresentationFor("Contract.Requires<TException>(bool,string)", false)] public readonly Method /*?*/ RequiresExceptionWithMsgMethod;

        [RepresentationFor("Contract.Ensures(bool)")] public readonly Method /*?*/ EnsuresMethod;
        [RepresentationFor("Contract.Ensures(bool,string)")] public readonly Method /*?*/ EnsuresWithMsgMethod;
        [RepresentationFor("Contract.EnsuresOnThrow<T>(bool)")] public readonly Method /*?*/ EnsuresOnThrowTemplate;

        [RepresentationFor("Contract.EnsuresOnThrow<T>(bool,string)")] public readonly Method /*?*/ EnsuresOnThrowWithMsgTemplate;

        [RepresentationFor("Contract.Invariant(bool)")] public readonly Method /*?*/ InvariantMethod;
        [RepresentationFor("Contract.Invariant(bool,msg)")] public readonly Method /*?*/ InvariantWithMsgMethod;

        [RepresentationFor("Contract.Result<T>()")] private readonly Method /*?*/ ResultTemplate;
        [RepresentationFor("Contract.ValueAtReturn<T>(out T t)")] private readonly Method /*?*/ ParameterTemplate;
        [RepresentationFor("Contract.OldValue<T>(T t)")] private readonly Method /*?*/ OldTemplate;

        [RepresentationFor("Contract.ForAll(int lo, int hi, Predicate<int> P)", false)] private readonly Method /*?*/ ForAllTemplate;

        [RepresentationFor("Contract.ForAll<T>(IEnumerable<T> collection, Predicate<int> P)", false)] private readonly Method /*?*/ ForAllGenericTemplate;

        [RepresentationFor("Contract.Exists(int lo, int hi, Predicate<int> P)", false)] private readonly Method /*?*/ ExistsTemplate;

        [RepresentationFor("Contract.Exists<T>(IEnumerable<T> collection, Predicate<int> P)", false)] private readonly Method /*?*/ ExistsGenericTemplate;

        [RepresentationFor("Contract.EndContractBlock()")] public readonly Method /*?*/ EndContract;

        [RepresentationFor("ContractHelper.RaiseContractFailedEvent(ContractFailureKind failureKind, String userProvidedMessage, String condition, Exception originalException)", false)] 
        public readonly Method /*?*/ RaiseFailedEventMethod;

        [RepresentationFor( "ContractHelper.ShowFailure(ContractFailureKind failureKind, String displayMessage, String userProvidedMessage, String condition, Exception originalException)", false)] 
        public readonly Method /*?*/ TriggerFailureMethod;

        /// <summary>
        /// If <paramref name="assembly"/> is null then this looks for the contract class in mscorlib.
        /// If it isn't null then it searches in <paramref name="assembly"/> for the contract class.
        /// Returns null if the contract class is not found.
        /// </summary>
        public static ContractNodes GetContractNodes(AssemblyNode assembly, Action<CompilerError> errorHandler)
        {
            ContractNodes contractNodes = new ContractNodes(assembly, errorHandler);
            if (contractNodes.ContractClass == null)
            {
                return null;
            }

            return contractNodes;
        }

        private void CallWarningFound(Module assembly, string message)
        {
            if (ErrorFound == null)
            {
                throw new InvalidOperationException(message);
            }
            
            var error = new CompilerError(assembly.Location, 0, 0, "", message);
            
            error.IsWarning = true;
            
            ErrorFound(error);
        }

        private void CallErrorFound(Module assembly, string message)
        {
            if (ErrorFound == null)
            {
                throw new InvalidOperationException(message);
            }
            
            ErrorFound(new System.CodeDom.Compiler.CompilerError(assembly.Location, 0, 0, "", message));
        }

        public const string SpecPublicAttributeStringName = "ContractPublicPropertyNameAttribute";

        /// <summary>
        /// Creates a new instance of this class that contains the CCI nodes for contract class.
        /// </summary>
        /// <param name="errorHandler">Delegate receiving errors. If null, errors are thrown as exceptions.</param>
        ///
        private ContractNodes(AssemblyNode assembly, Action<System.CodeDom.Compiler.CompilerError> errorHandler)
        {
            if (errorHandler != null) this.ErrorFound += errorHandler;

            AssemblyNode assemblyContainingContractClass = null;

            // Get the contract class and all of its members

            assemblyContainingContractClass = assembly;
            ContractClass = assembly.GetType(ContractNamespace, Identifier.For("Contract")) as Class;
            if (ContractClass == null)
            {
                // This is not a candidate, no warning as we try to find the right place where contracts live
                return;
            }

            ContractHelperClass = assembly.GetType(ContractInternalNamespace, Identifier.For("ContractHelper")) as Class;
            if (ContractHelperClass == null || !ContractHelperClass.IsPublic)
            {
                // look in alternate location
                var alternateNs = Identifier.For("System.Diagnostics.Contracts.Internal");
                ContractHelperClass = assembly.GetType(alternateNs, Identifier.For("ContractHelper")) as Class;
            }

            // Get ContractFailureKind

            ContractFailureKind = assemblyContainingContractClass.GetType(ContractNamespace, Identifier.For("ContractFailureKind")) as EnumNode;

            // Look for each member of the contract class

            var requiresMethods = ContractClass.GetMethods(Identifier.For("Requires"), SystemTypes.Boolean);
            for (int i = 0; i < requiresMethods.Count; i++)
            {
                var method = requiresMethods[i];
                if (method.TemplateParameters != null && method.TemplateParameters.Count == 1)
                {
                    // Requires<E>
                    RequiresExceptionMethod = method;
                }
                else if (method.TemplateParameters == null || method.TemplateParameters.Count == 0)
                {
                    RequiresMethod = method;
                }
            }

            // early test to see if the ContractClass we found has a Requires(bool) method. If it doesn't, we 
            // silently think that this is not the right place.
            // We use this because contract reference assemblies have a Contract class, but it is not the right one, as it holds
            // just the 3 argument versions of all the contract methods.
            if (RequiresMethod == null)
            {
                ContractClass = null;
                return;
            }

            var requiresMethodsWithMsg = ContractClass.GetMethods(Identifier.For("Requires"), SystemTypes.Boolean, SystemTypes.String);
            for (int i = 0; i < requiresMethodsWithMsg.Count; i++)
            {
                var method = requiresMethodsWithMsg[i];
                if (method.TemplateParameters != null && method.TemplateParameters.Count == 1)
                {
                    // Requires<E>
                    RequiresExceptionWithMsgMethod = method;
                }

                else if (method.TemplateParameters == null || method.TemplateParameters.Count == 0)
                {
                    RequiresWithMsgMethod = method;
                }
            }

            EnsuresMethod = ContractClass.GetMethod(Identifier.For("Ensures"), SystemTypes.Boolean);
            
            EnsuresWithMsgMethod = ContractClass.GetMethod(Identifier.For("Ensures"), SystemTypes.Boolean, SystemTypes.String);
            EnsuresOnThrowTemplate = ContractClass.GetMethod(Identifier.For("EnsuresOnThrow"), SystemTypes.Boolean);
            EnsuresOnThrowWithMsgTemplate = ContractClass.GetMethod(Identifier.For("EnsuresOnThrow"), SystemTypes.Boolean, SystemTypes.String);

            InvariantMethod = ContractClass.GetMethod(Identifier.For("Invariant"), SystemTypes.Boolean);
            InvariantWithMsgMethod = ContractClass.GetMethod(Identifier.For("Invariant"), SystemTypes.Boolean, SystemTypes.String);

            AssertMethod = ContractClass.GetMethod(Identifier.For("Assert"), SystemTypes.Boolean);
            AssertWithMsgMethod = ContractClass.GetMethod(Identifier.For("Assert"), SystemTypes.Boolean, SystemTypes.String);

            AssumeMethod = ContractClass.GetMethod(Identifier.For("Assume"), SystemTypes.Boolean);
            AssumeWithMsgMethod = ContractClass.GetMethod(Identifier.For("Assume"), SystemTypes.Boolean, SystemTypes.String);

            ResultTemplate = ContractClass.GetMethod(ResultName);

            TypeNode GenericPredicate = SystemTypes.SystemAssembly.GetType(
                Identifier.For("System"),
                Identifier.For("Predicate" + TargetPlatform.GenericTypeNamesMangleChar + "1"));

            if (GenericPredicate != null)
            {
                ForAllGenericTemplate = ContractClass.GetMethod(ForallName, SystemTypes.GenericIEnumerable, GenericPredicate);
                ExistsGenericTemplate = ContractClass.GetMethod(ExistsName, SystemTypes.GenericIEnumerable, GenericPredicate);

                if (ForAllGenericTemplate == null)
                {
                    // The problem might be that we are in the pre 4.0 scenario and using an out-of-band contract for mscorlib
                    // in which case the contract library is defined in that out-of-band contract assembly.
                    // If so, then ForAll and Exists are defined in terms of the System.Predicate defined in the out-of-band assembly.
                    var tempGenericPredicate = assemblyContainingContractClass.GetType(
                        Identifier.For("System"),
                        Identifier.For("Predicate" + TargetPlatform.GenericTypeNamesMangleChar + "1"));

                    if (tempGenericPredicate != null)
                    {
                        GenericPredicate = tempGenericPredicate;
                        TypeNode genericIEnum =
                            assemblyContainingContractClass.GetType(Identifier.For("System.Collections.Generic"),
                                Identifier.For("IEnumerable" + TargetPlatform.GenericTypeNamesMangleChar + "1"));

                        ForAllGenericTemplate = ContractClass.GetMethod(Identifier.For("ForAll"), genericIEnum, GenericPredicate);
                        ExistsGenericTemplate = ContractClass.GetMethod(Identifier.For("Exists"), genericIEnum, GenericPredicate);
                    }
                }

                TypeNode PredicateOfInt = GenericPredicate.GetTemplateInstance(ContractClass, SystemTypes.Int32);
                if (PredicateOfInt != null)
                {
                    ForAllTemplate = ContractClass.GetMethod(Identifier.For("ForAll"), SystemTypes.Int32, SystemTypes.Int32, PredicateOfInt);
                    ExistsTemplate = ContractClass.GetMethod(Identifier.For("Exists"), SystemTypes.Int32, SystemTypes.Int32, PredicateOfInt);
                }
            }

            foreach (Member member in ContractClass.GetMembersNamed(ValueAtReturnName))
            {
                Method method = member as Method;
                if (method != null && method.Parameters.Count == 1)
                {
                    Reference reference = method.Parameters[0].Type as Reference;
                    if (reference != null && reference.ElementType.IsTemplateParameter)
                    {
                        ParameterTemplate = method;
                        break;
                    }
                }
            }

            foreach (Member member in ContractClass.GetMembersNamed(OldName))
            {
                Method method = member as Method;
                if (method != null && method.Parameters.Count == 1 && method.Parameters[0].Type.IsTemplateParameter)
                {
                    OldTemplate = method;
                    break;
                }
            }

            EndContract = ContractClass.GetMethod(Identifier.For("EndContractBlock"));
            if (this.ContractFailureKind != null)
            {
                if (ContractHelperClass != null)
                {
                    RaiseFailedEventMethod = ContractHelperClass.GetMethod(ContractNodes.RaiseContractFailedEventName,
                        this.ContractFailureKind, SystemTypes.String, SystemTypes.String, SystemTypes.Exception);

                    TriggerFailureMethod = ContractHelperClass.GetMethod(ContractNodes.TriggerFailureName,
                        this.ContractFailureKind, SystemTypes.String, SystemTypes.String, SystemTypes.String, SystemTypes.Exception);
                }
            }

            // Get the attributes

            PureAttribute = assemblyContainingContractClass.GetType(ContractNamespace, Identifier.For("PureAttribute")) as Class;
            InvariantMethodAttribute =
                assemblyContainingContractClass.GetType(ContractNamespace, Identifier.For("ContractInvariantMethodAttribute")) as Class;

            ContractClassAttribute =
                assemblyContainingContractClass.GetType(ContractNamespace, ContractClassAttributeName) as Class;

            ContractClassForAttribute =
                assemblyContainingContractClass.GetType(ContractNamespace, ContractClassForAttributeName) as Class;

            VerifyAttribute =
                assemblyContainingContractClass.GetType(ContractNamespace, Identifier.For("ContractVerificationAttribute")) as Class;
            SpecPublicAttribute =
                assemblyContainingContractClass.GetType(ContractNamespace, SpecPublicAttributeName) as Class;

            ReferenceAssemblyAttribute =
                assemblyContainingContractClass.GetType(ContractNamespace, Identifier.For("ContractReferenceAssemblyAttribute")) as Class;
            IgnoreAtRuntimeAttribute =
                assemblyContainingContractClass.GetType(ContractNamespace, RuntimeIgnoredAttributeName) as Class;

            // Check that every field in this class has been set

            foreach (System.Reflection.FieldInfo field in typeof (ContractNodes).GetFields())
            {
                if (field.GetValue(this) == null)
                {
                    string sig = null;
                    bool required = false;

                    object[] cas = field.GetCustomAttributes(typeof (RepresentationFor), false);
                    for (int i = 0, n = cas.Length; i < n; i++)
                    {
                        // should be exactly one
                        RepresentationFor rf = cas[i] as RepresentationFor;
                        if (rf != null)
                        {
                            sig = rf.runtimeName;
                            required = rf.required;
                            break;
                        }
                    }

                    if (!required) continue;
                    
                    string msg = "Could not find contract node for '" + field.Name + "'";
                    if (sig != null)
                        msg = "Could not find the method/type '" + sig + "'";

                    if (ContractClass != null && ContractClass.DeclaringModule != null)
                    {
                        msg += " in assembly '" + ContractClass.DeclaringModule.Location + "'";
                    }

                    Module dm = ContractClass.DeclaringModule;
                    
                    ClearFields();
                    CallErrorFound(dm, msg);
                    
                    return;
                }
            }

            // Check that ContractFailureKind is okay

            if (this.ContractFailureKind.GetField(Identifier.For("Assert")) == null
                || this.ContractFailureKind.GetField(Identifier.For("Assume")) == null
                || this.ContractFailureKind.GetField(Identifier.For("Invariant")) == null
                || this.ContractFailureKind.GetField(Identifier.For("Postcondition")) == null
                || this.ContractFailureKind.GetField(Identifier.For("Precondition")) == null
                )
            {
                Module dm = ContractClass.DeclaringModule;
                
                ClearFields();
                CallErrorFound(dm, "The enum ContractFailureKind must have the values 'Assert', 'Assume', 'Invariant', 'Postcondition', and 'Precondition'.");
            }
        }

        /// <summary>
        /// For those fields in this class that represent things (methods, types, etc.) from the contract library,
        /// this method (re)sets them to null. It uses Reflection.
        /// </summary>
        private void ClearFields()
        {
            foreach (System.Reflection.FieldInfo field in typeof (ContractNodes).GetFields())
            {
                object[] cas = field.GetCustomAttributes(typeof (RepresentationFor), false);
                if (cas != null && cas.Length == 1)
                {
                    field.SetValue(this, null);
                }
            }
        }

        [Pure]
        public bool IsContractOrValidatorOrAbbreviatorCall(Statement s)
        {
            Contract.Ensures(!Contract.Result<bool>() || s != null);

            Method m = HelperMethods.IsMethodCall(s);
            if (m == null) return false;

            return IsContractOrValidatorOrAbbreviatorMethod(m);
        }

        /// <summary>
        /// Analyzes a statement to see if it is a call to one of the contract methods,
        /// such as Contract.Requires.
        /// </summary>
        /// <param name="s">Any statement.</param>
        /// <returns>System.Contracts.Contract.xxx where xxx is Requires, Ensures, etc. or null</returns>
        public Method IsContractCall(Statement s)
        {
            Method m = HelperMethods.IsMethodCall(s);
            if (m == null) return null;
            if (this.IsContractMethod(m))
            {
                return m;
            }
            
            return null;
        }

        public static bool IsValidatorCall(Statement s)
        {
            Method m = HelperMethods.IsMethodCall(s);
            if (m == null) return false;
            if (IsValidatorMethod(m))
            {
                return true;
            }
            
            return false;
        }

        public static bool IsAlreadyRewritten(Module module)
        {
            foreach (var attr in module.Attributes)
            {
                if (attr == null) continue;

                if (attr.Type.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
                    return true;
            }

            return false;
        }

        public bool IsContractOrValidatorOrAbbreviatorMethod(Method m)
        {
            return IsValidatorMethod(m) || IsContractMethod(m) || IsAbbreviatorMethod(m);
        }

        public static bool IsAbbreviatorMethod(Method m)
        {
            if (m == null) return false;

            while (m.Template != null)
            {
                m = m.Template;
            }

            return (m.Attributes.HasAttribute(AbbreviatorAttributeName));
        }

        public static bool IsValidatorMethod(Method m)
        {
            if (m == null) return false;

            while (m.Template != null)
            {
                m = m.Template;
            }

            return (m.Attributes.HasAttribute(ValidatorAttributeName));
        }

        public bool IsContractMethod(Method m)
        {
            if (m == null) return false;

            return this.IsPlainPrecondition(m)
                   || this.IsPostcondition(m) || this.IsExceptionalPostcondition(m)
                   || this.IsEndContract(m);
        }

        public bool IsEndContract(Method m)
        {
            return m == this.EndContract;
        }

        public bool IsPlainPrecondition(Method m)
        {
            TypeNode typeArg;
            return IsContractMethod(RequiresName, m, out typeArg) ||
                   // Beta1 backward compat
                   IsContractMethod(RequiresAlwaysName, m, out typeArg);
        }

        public bool IsRequiresWithException(Method m, out TypeNode texception)
        {
            if (IsContractMethod(RequiresName, m, out texception) && texception != null) return true;

            // backward compat
            if (IsContractMethod(RequiresAlwaysName, m, out texception))
            {
                texception = SystemTypes.ArgumentException;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the invariant condition or null if statement is not invariant method call
        /// </summary>
        /// <param name="name">Optional string of invariant</param>
        public Expression IsInvariant(Statement s, out Literal name, out Literal sourceText)
        {
            name = null;
            sourceText = null;
            if (s == null) return null;

            ExpressionStatement es = s as ExpressionStatement;
            if (es == null) return null;

            MethodCall mc = es.Expression as MethodCall;
            if (mc == null) return null;

            MemberBinding mb = mc.Callee as MemberBinding;
            if (mb == null) return null;

            Method m = mb.BoundMember as Method;
            if (m == null) return null;

            if (this.IsInvariantMethod(m))
            {
                if (mc.Operands.Count > 1)
                {
                    name = mc.Operands[1] as Literal;
                }

                if (mc.Operands.Count > 2)
                {
                    sourceText = mc.Operands[2] as Literal;
                }

                return mc.Operands[0];
            }
            
            return null;
        }

        public bool IsInvariantMethod(Method m)
        {
            if (m == null) return false;

            TypeNode typeArg;
            return IsContractMethod(InvariantName, m, out typeArg) && typeArg == null;
        }

        public bool IsPostcondition(Method m)
        {
            TypeNode typeArg;
            return IsContractMethod(EnsuresName, m, out typeArg) && typeArg == null;
        }

        public bool IsExceptionalPostcondition(Method m)
        {
            TypeNode excType;
            return IsContractMethod(EnsuresOnThrowName, m, out excType) && excType != null;
        }

        public bool IsContractMethod(Identifier name, Method m, out TypeNode genericArg)
        {
            genericArg = null;
            if (m == null) return false;

            if (m.Template != null)
            {
                if (m.TemplateArguments == null) return false;

                if (m.TemplateArguments.Count != 1) return false;

                genericArg = m.TemplateArguments[0];
                m = m.Template;
            }

            if (m.Name == null) return false;

            if (m.Name.UniqueIdKey != name.UniqueIdKey) return false;

            if (m.DeclaringType == this.ContractClass) return true;

            // Reference assemblies *always* use a three-argument method which is defined in the
            // reference assembly itself.
            // REVIEW: Could also check that the assembly m is defined in is marked with
            // the [ContractReferenceAssembly] attribute
            if (m.Parameters == null || m.Parameters.Count != 3) return false;

            if (m.DeclaringType == null || m.DeclaringType.Namespace == null) return false;

            if (m.DeclaringType.Namespace.Name == null ||
                m.DeclaringType.Namespace.Name != "System.Diagnostics.Contracts")
            {
                return false;
            }

            if (m.DeclaringType.Name != null && m.DeclaringType.Name.Name == "Contract") return true;

            return false;
        }


        private bool AreAnyPure(MethodList methods)
        {
            if (methods == null || methods.Count == 0) return false;

            for (int i = 0; i < methods.Count; i++)
            {
                if (IsPure(methods[i])) return true;
            }

            return false;
        }

        private static bool HasPureAttribute(AttributeList attributes)
        {
            if (attributes == null) return false;

            for (int i = 0; i < attributes.Count; i++)
            {
                if (attributes[i] == null) continue;
                if (attributes[i].Type == null) continue;
                if (attributes[i].Type.Name == null) continue;

                if (attributes[i].Type.Name.Name == "PureAttribute") return true;
            }

            return false;
        }

        [Pure]
        public bool IsPure(Method method)
        {
            if (method == null) return false;

            while (method.Template != null)
            {
                method = method.Template;
            }
            if (method.Contract != null && method.Contract.IsPure) return true;

            if (method.IsPropertyGetter) return true;

            if (HasPureAttribute(method.Attributes)) return true;

            if (!(method is InstanceInitializer) && HasPureAttribute(method.DeclaringType.Attributes)) return true;

            // Operators are pure by default
            if (IsOperator(method)) return true;

            if (method.OverriddenMethod != null && IsPure(method.OverriddenMethod)) return true;

            if (AreAnyPure(method.ImplementedInterfaceMethods)) return true;

            if (AreAnyPure(method.ImplicitlyImplementedInterfaceMethods)) return true;

            if (IsForallMethod(method) || IsGenericForallMethod(method) || IsExistsMethod(method) ||
                IsGenericExistsMethod(method)) return true;

            if (IsFuncOrPredicate(method)) return true;

            return false;
        }

        private static bool IsOperator(Method method)
        {
            return method.IsSpecialName
                   && method.IsStatic
                   && method.Name != null && method.Name.Name != null && method.Name.Name.StartsWith("op_")
                   && method.Parameters != null
                   && (method.Parameters.Count == 1 || method.Parameters.Count == 2)
                   && !HelperMethods.IsVoidType(method.ReturnType);
        }

        public bool IsResultMethod(Method method)
        {
            if (method == null) return false;

            if (method.Template != null)
            {
                method = method.Template;
            }

            if (method == ResultTemplate) return true;

            // by name matching
            return method.MatchesContractByName(ResultName);
        }

        public bool IsOldMethod(Method method)
        {
            if (method == null) return false;

            if (method.Template != null)
            {
                method = method.Template;
            }

            if (method == OldTemplate) return true;

            // by name matching
            return method.MatchesContractByName(OldName);
        }

        public bool IsValueAtReturnMethod(Method method)
        {
            if (method == null) return false;

            if (method.Template != null)
            {
                method = method.Template;
            }

            if (method == ParameterTemplate) return true;

            // by name matching
            return method.MatchesContractByName(ValueAtReturnName);
        }

        public bool IsForallMethod(Method method)
        {
            if (method == null) return false;

            if (method.Template != null)
            {
                method = method.Template;
            }

            if (method == ForAllTemplate) return true;

            // by name matching
            if (method.TemplateParameters != null && method.TemplateParameters.Count > 0) return false;

            return method.MatchesContractByName(ForallName);
        }

        public bool IsGenericForallMethod(Method method)
        {
            if (method == null) return false;

            if (method.Template != null)
            {
                method = method.Template;
            }

            if (method == ForAllGenericTemplate) return true;

            // by name matching
            if (method.TemplateParameters == null || method.TemplateParameters.Count != 1) return false;

            return method.MatchesContractByName(ForallName);
        }


        public Method GetForAllTemplate
        {
            get { return this.ForAllTemplate; }
        }

        public Method GetForAllGenericTemplate
        {
            get { return this.ForAllGenericTemplate; }
        }

        public bool IsExistsMethod(Method method)
        {
            if (method == null) return false;

            if (method.Template != null)
            {
                method = method.Template;
            }

            if (method == ExistsTemplate) return true;

            // by name matching
            if (method.TemplateParameters != null && method.TemplateParameters.Count > 0) return false;

            return method.MatchesContractByName(ExistsName);
        }

        public bool IsGenericExistsMethod(Method method)
        {
            if (method == null) return false;

            if (method.Template != null)
            {
                method = method.Template;
            }

            if (method == ExistsGenericTemplate) return true;

            // by name matching
            if (method.TemplateParameters == null || method.TemplateParameters.Count != 1) return false;

            return method.MatchesContractByName(ExistsName);
        }

        public Method GetExistsTemplate
        {
            get { return this.ExistsTemplate; }
        }

        public Method GetExistsGenericTemplate
        {
            get { return this.ExistsGenericTemplate; }
        }

        [Pure]
        public bool IsObjectInvariantMethod(Method method)
        {
            if (method == null) return false;

            if (method.Attributes != null)
            {
                foreach (var a in method.Attributes)
                {
                    if (a == null) continue;
                    if (a.Type == this.InvariantMethodAttribute) return true;

                    // by name matching
                    if (a.Type.MatchesContractByName(this.InvariantMethodAttribute.Name)) return true;
                }
            }

            return false;
        }

        [Pure]
        public static bool IsFuncOrPredicate(Method method)
        {
            if (method == null) return false;

            if (!(method.DeclaringType.IsDelegateType())) return false;

            var declaringRoot = HelperMethods.Unspecialize(method.DeclaringType);
            if (declaringRoot.Namespace.Name != "System") return false;

            if (declaringRoot.Name.Name == "Predicate`1") return true;

            if (declaringRoot.Name.Name.StartsWith("Func`")) return true;

            return false;
        }
    }
}
