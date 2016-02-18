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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Compiler;
using System.Diagnostics.Contracts;
using Microsoft.Contracts.Foxtrot.Utils;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// A checker that can assume the entire assembly has had its contracts extracted.
    /// </summary>
    [ContractVerification(false)] // F:
    public sealed class PostExtractorChecker : Inspector
    {
        private readonly Action<System.CodeDom.Compiler.CompilerError> m_errorHandler;
        private readonly bool allowPreconditionsInOverrides;
        private readonly bool explicitUserValidations;
        private readonly bool addInterfaceWrappersWhenNeeded;

        /// <summary>
        /// Identical to the runtime checking level option.
        /// Instrumentation level: 0=no contracts, 1=ReleaseRequires, 2=Requires, 3=Ensures, 4=Invariants. (Each increase includes the previous)
        /// </summary>
        private readonly int runtimeCheckingLevel = 4; // all contracts

        private readonly PurityChecker purityChecker;
        private readonly PreconditionChecker preconditionChecker;
        private readonly PostconditionChecker postconditionChecker;
        private readonly VisibilityChecker visibilityChecker;
        private readonly CheckForCallsToInvariantMethods invariantCallChecker;
        private readonly MethodBodyChecker methodBodyChecker;
        private readonly InvariantChecker invariantChecker;
        private readonly ContractNodes contractNodes;
        private CurrentState currentState;

        [ContractVerification(false)] // F: todo
        private struct CurrentState
        {
            public readonly AssemblyNode Assembly;
            public readonly Method Method;
            public readonly TypeNode Type;

            private Dictionary<string, object> typeSuppressed;
            private Dictionary<string, object> methodSuppressed;
            private Dictionary<string, object> assemblySuppressed;

            public CurrentState(AssemblyNode assembly)
            {
                this.Assembly = assembly;
                this.Type = null;
                this.Method = null;
                this.assemblySuppressed = null;
                this.typeSuppressed = null;
                this.methodSuppressed = null;
            }

            private CurrentState(Method method, CurrentState oldState)
            {
                this.Assembly = oldState.Assembly;
                this.assemblySuppressed = oldState.assemblySuppressed;
                this.Type = oldState.Type;
                this.typeSuppressed = oldState.typeSuppressed;
                this.Method = method;
                this.methodSuppressed = null;
            }

            public CurrentState(TypeNode type, CurrentState oldState)
            {
                this.Type = type;
                this.Method = null;
                this.typeSuppressed = null;
                this.methodSuppressed = null;
                this.Assembly = oldState.Assembly;
                this.assemblySuppressed = oldState.assemblySuppressed;
            }

            public bool IsSuppressed(string errorNumber)
            {
                if (this.Assembly != null && this.assemblySuppressed == null)
                {
                    this.assemblySuppressed = new Dictionary<string, object>();
                    GrabSuppressAttributes(this.assemblySuppressed, this.Assembly.Attributes);
                }

                if (this.assemblySuppressed != null && this.assemblySuppressed.ContainsKey(errorNumber)) return true;

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
            // Should be thread-safe

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
                                
                                Contract.Assume(errorCodeLit != null); // F:
                                
                                if (errorCodeLit.StartsWith("CC"))
                                {
                                    result.Add(errorCodeLit, errorCodeLit);
                                }
                            }
                        }
                    }
                }
            }

            internal CurrentState Derive(Method method)
            {
                return new CurrentState(method, this);
            }

            internal CurrentState Derive(TypeNode type)
            {
                return new CurrentState(type, this);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode"),
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"), ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(m_errorHandler != null);
            Contract.Invariant(purityChecker != null);
            Contract.Invariant(invariantChecker != null);
            Contract.Invariant(methodBodyChecker != null);
            Contract.Invariant(visibilityChecker != null);
            Contract.Invariant(this.invariantMethods != null);
            Contract.Invariant(this.contractNodes != null);
        }

        [ContractVerification(true)]
        public PostExtractorChecker(
            ContractNodes usedToExtract,
            Action<System.CodeDom.Compiler.CompilerError> errorHandler,
            bool allowPreconditionsInOverrides, bool fSharp, bool explicitUserValidations,
            bool addInterfaceWrappersWhenNeeded, int runtimeCheckingLevel)
        {
            Contract.Requires(errorHandler != null);
            Contract.Requires(usedToExtract != null);

            this.m_errorHandler = errorHandler;
            this.allowPreconditionsInOverrides = allowPreconditionsInOverrides;
            this.explicitUserValidations = explicitUserValidations;
            this.addInterfaceWrappersWhenNeeded = addInterfaceWrappersWhenNeeded;
            this.runtimeCheckingLevel = runtimeCheckingLevel;

            this.purityChecker = new PurityChecker(this.HandleError, fSharp, usedToExtract);
            this.preconditionChecker = new PreconditionChecker(this.HandleError, usedToExtract);
            this.postconditionChecker = new PostconditionChecker(this.HandleError, fSharp);
            this.invariantCallChecker = new CheckForCallsToInvariantMethods(this);
            this.methodBodyChecker = new MethodBodyChecker(this.HandleError, usedToExtract);
            this.invariantChecker = new InvariantChecker(this.HandleError, usedToExtract);
            this.contractNodes = usedToExtract;
            this.visibilityChecker = new VisibilityChecker(this.HandleError, usedToExtract);
            this.invariantMethods = new TrivialHashtable(0);
        }


        /// <summary>
        /// Performs filtering of errors based on SuppressMessage attributes
        /// </summary>
        private void HandleError(CompilerError error)
        {
            if (error.IsWarning && this.currentState.IsSuppressed(error.ErrorNumber)) return;
            
            this.m_errorHandler(error);
        }

        /// <summary>
        /// If method overrides another method, this provides the root method that is overridden
        /// </summary>
        [ContractVerification(true)]
        private static Method OverriddenRootMethod(Method method)
        {
            Contract.Requires(method != null);

            Method current = method;

            while (current.OverriddenMethod != null && HelperMethods.DoesInheritContracts(current))
            {
                current = current.OverriddenMethod;
            }

            if (current == method) return null; // no root base method
            
            return current;
        }

        private static bool GenericConstraintsMatch(TypeNode t1, TypeNode t2)
        {
            if (t1.IsGeneric != t2.IsGeneric) return false;

            if (t1.TemplateParameters != null)
            {
                if (t1.TemplateParameters.Count != t2.TemplateParameters.Count) return false;
                
                for (int i = 0, n = t1.TemplateParameters.Count; i < n; i++)
                {
                    var tn1 = t1.TemplateParameters[i];
                    var tn2 = t2.TemplateParameters[i];

                    // F:
                    Contract.Assume(tn1 != null);
                    Contract.Assume(tn2 != null);
                    Contract.Assume(tn1.Interfaces != null);
                    Contract.Assume(tn2.Interfaces != null);

                    if ((tn1.Interfaces == null) != (tn2.Interfaces == null)) return false;

                    if (tn1.Interfaces != null && tn1.Interfaces.Count != tn2.Interfaces.Count) return false;
                    
                    TypeParameter tp1 = tn1 as TypeParameter;
                    
                    if (tp1 != null)
                    {
                        var tp2 = tn2 as TypeParameter;
                        if (tp2 == null) return false;

                        if (tp1.IsReferenceType != tp2.IsReferenceType) return false;
                        
                        if (tp1.IsValueType != tp2.IsValueType) return false;

                        if ((tp1.TypeParameterFlags & TypeParameterFlags.DefaultConstructorConstraint) !=
                            (tp2.TypeParameterFlags & TypeParameterFlags.DefaultConstructorConstraint))
                        {
                            return false;
                        }
                    }

                    ClassParameter cp1 = tn1 as ClassParameter;
                    if (cp1 != null)
                    {
                        var cp2 = tn2 as ClassParameter;
                        if (cp2 == null) return false;
                        
                        if (cp1.IsReferenceType != cp2.IsReferenceType) return false;
                        
                        if (cp1.IsValueType != cp2.IsValueType) return false;

                        if ((cp1.TypeParameterFlags & TypeParameterFlags.DefaultConstructorConstraint) !=
                            (cp2.TypeParameterFlags & TypeParameterFlags.DefaultConstructorConstraint))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Verifies that a class marked as holding the contract for an interface implements the interface.
        /// </summary>
        private void CheckClass(Class Class)
        {
            Contract.Requires(Class != null);

            // Compiler generated classes are closures that should get checked when visiting
            // the code that generated the closure, not from this end
            if (HelperMethods.IsCompilerGenerated(Class))
            {
                return;
            }

            // Contract classes need to implement the interface/abstract class that they are a contract for

            TypeNode originalType = HelperMethods.GetTypeFromAttribute(Class, ContractNodes.ContractClassForAttributeName);

            // Check that contract classes are abstract

            if (originalType != null && !Class.IsAbstract)
            {
                this.HandleError(
                    new Warning(1020, "Contract class '" + Class.FullName + "' should be an abstract class.",
                        new SourceContext()));
            }

            Interface iface = originalType as Interface;
            if (iface != null)
            {
                // base class should be object and interfaces should contain exactly 1, the interface
                if (Class.BaseClass != null && !(IsSystemObject(Class.BaseClass)))
                {
                    // should be an error
                    this.HandleError(
                        new Warning(1066,
                            "Class '" + Class.FullName + "' is annotated as being the contract for the interface '" +
                            iface.FullName + "' and cannot have an explicit base class other than System.Object.",
                            new SourceContext()));
                }

                Interface instance = null;
                for (int i = 0, n = Class.Interfaces == null ? 0 : Class.Interfaces.Count; i < n; i++)
                {
                    Interface J = Class.Interfaces[i];
                    if (J == iface || J.IsStructurallyEquivalentTo(iface))
                    {
                        instance = J;
                        break;
                    }
                }

                if (instance == null)
                {
                    this.HandleError(
                        new Error(1008,
                            "Class '" + Class.FullName + "' is annotated as being the contract for the interface '" +
                            iface.FullName + "' but doesn't implement the interface.",
                            new SourceContext()));
                }
                else
                {
                    // check other interfaces derive from core interface instance (.NET flattens the interface inheritance)
                    for (int i = 0, n = Class.Interfaces == null ? 0 : Class.Interfaces.Count; i < n; i++)
                    {
                        Interface J = Class.Interfaces[i];
                        if (J != instance && !instance.IsAssignableTo(J))
                        {
                            this.HandleError(
                                new Error(1068,
                                    string.Format("Class '{0}' is annotated as being the contract for the interface '{1}' but implements unrelated interface '{2}'.",
                                        Class.FullName, iface.FullName, J.FullName),
                                    new SourceContext()));
                        }
                    }
                }
            }
            else
            {
                Class abstractClass = originalType as Class;
                if (abstractClass != null)
                {
                    if (!(abstractClass == Class.BaseClass || abstractClass.IsStructurallyEquivalentTo(Class.BaseClass)))
                    {
                        this.HandleError(
                            new Error(1009,
                                "Class '" + Class.FullName + "' is annotated as being the contract for the abstract class '" +
                                abstractClass.FullName + "' but doesn't extend the class.",
                                new SourceContext()));
                    }

                    if (Class.Interfaces != null && Class.Interfaces.Count > 0)
                    {
                        this.HandleError(
                            new Error(1067,
                                "Class '" + Class.FullName + "' is annotated as being the contract for the abstract class '" +
                                abstractClass.FullName + "' and cannot implement any interfaces.",
                                new SourceContext()));
                    }
                }
            }

            // Make sure that if this class *is* a contractClass, that that class points back to this one

            if (originalType != null)
            {
                TypeNode backPointer = HelperMethods.GetTypeFromAttribute(originalType, ContractNodes.ContractClassAttributeName);
                if (backPointer == null || backPointer != Class)
                {
                    this.HandleError(
                        new Error(1021, "The class '" + Class.FullName
                                        + "' is supposed to be a contract class for '" +
                                        originalType.FullName
                                        + "', but that type does not point back to this class.",
                            new SourceContext()));
                }

                // Checks that contract class matches real class

                if (!GenericConstraintsMatch(originalType, Class))
                {
                    this.HandleError(
                        new Error(1043, "The contract class '" + Class.FullName
                                        + "' and the type '" + originalType.FullName + "' must agree on all generic parameters.",
                            new SourceContext()));
                }

                // Check that they have the same container if they are generic

                if (Class.IsGeneric && Class.DeclaringType != originalType.DeclaringType)
                {
                    this.HandleError(
                        new Error(1044, "The contract class '" + Class.FullName
                                        + "' and the type '" + originalType.FullName
                                        + "' must have the same declaring type if any.",
                            new SourceContext()));
                }
            }

            // No class should have a contract class as a base class

            var baseClass = Class.BaseClass;
            var baseClassIsAContractClass = HelperMethods.GetTypeFromAttribute(baseClass, ContractNodes.ContractClassForAttributeName);
            if (baseClassIsAContractClass != null)
            {
                this.HandleError(
                    new Error(1079, "The class '" + Class.FullName + "' cannot have a base type that is a contract class.",
                        new SourceContext()));
            }
        }

        private static bool IsSystemObject(Class Class)
        {
            return Class.Name.Matches(StandardIds.CapitalObject) && Class.Namespace.Matches(StandardIds.System);
        }

        [ContractVerification(true)]
        private void CheckTypeNode(TypeNode typeNode)
        {
            Contract.Requires(typeNode != null);

            // Check Invariants

            if (typeNode.Contract != null)
            {
                // F:
                Contract.Assume(typeNode.Contract.Invariants != null);

                this.purityChecker.CheckInvariants(typeNode, typeNode.Contract.Invariants);
                this.invariantChecker.Check(typeNode);
            }

            // Make sure that if this class has a contractClass, that that class points back to this one

            TypeNode contractType = HelperMethods.GetTypeFromAttribute(typeNode, ContractNodes.ContractClassAttributeName);
            if (contractType != null)
            {
                Class contractClass = contractType as Class;
                if (contractClass == null)
                {
                    this.HandleError(
                        new Error(1006, "Type '" + typeNode.FullName
                                        + "' has the attribute [ContractClass], but its argument '"
                                        + contractType.FullName + "' is not a class.",
                            new SourceContext()));

                    return;
                }

                if (!typeNode.IsAbstract)
                {
                    this.HandleError(
                        new Error(1018,
                            "Type '" + typeNode.FullName +
                            "' has the [ContractClass] attribute, but it isn't an interface or an abstract class.",
                            new SourceContext()));
                }

                TypeNode backPointer = HelperMethods.GetTypeFromAttribute(contractClass, ContractNodes.ContractClassForAttributeName);
                if (backPointer != null && backPointer.Template != null)
                {
                    backPointer = backPointer.Template;
                }

                if (backPointer != typeNode)
                {
                    this.HandleError(
                        new Error(1019, "Type '" + typeNode.FullName
                                        + "' specifies the class '" + contractClass.FullName
                                        + "' as its contract class, but that class does not point back to this type.",
                            new SourceContext()));
                }
            }
        }

        /// <summary>
        /// Check to see that if the field is marked SpecPublic and if it has a
        /// string argument then the string is the name of a public field or a public
        /// property.
        /// </summary>
        [ContractVerification(true)]
        private void CheckField(Field field)
        {
            Contract.Requires(field != null);

            string s = HelperMethods.GetStringFromAttribute(field, ContractNodes.SpecPublicAttributeName);
            if (s != null)
            {
                // okay if it is null, then it just doesn't have a named substitute to use in documentation, etc.
                // make sure it is the name of a (public) field or (public) property, but it might be inherited

                // Search class hierarchy for a field or a property

                TypeNode t = field.DeclaringType;
                bool error = true;

                while (t != null && error)
                {
                    Contract.Assert(t != null);

                    MemberList mems = t.GetMembersNamed(Identifier.For(s));
                    for (int i = 0, n = /*mems == null ? 0 : */ mems.Count; /*error && */i < n; i++)
                    {
                        Member mem = mems[i];
                        Field f = mem as Field;
                        
                        if (f != null)
                        {
                            if (!f.IsPublic) continue;

                            //if (f.Type != field.Type) continue; // REVIEW: When it becomes possible, allow widening
                            error = false;
                            
                            break;
                        }

                        Property p = mem as Property;
                        if (p != null)
                        {
                            Method getter = p.Getter;
                            
                            if (getter == null) continue; // found by Clousot.
                            
                            if (!getter.IsPublic) continue;
                            
                            //if (getter.ReturnType != field.Type) continue; // REVIEW: When it becomes possible, allow widening
                            error = false;
                            
                            break;
                        }
                    }

                    t = t.BaseType;
                }

                if (error)
                {
                    this.HandleError(
                        new Error(1010, "Field '" +
                                        field.FullName +
                                        "' is marked [ContractPublicPropertyName(\"" + s +
                                        "\")], but no public field/property named '" +
                                        s + "' with type '" + field.Type + "' can be found",
                            field.SourceContext));
                }
            }
        }

        /// <summary>
        /// In CCI1 extractor, contracts are not moved from OOB to abstract/interface methods, so we have to grab them
        /// from there.
        /// </summary>
        [ContractVerification(true)]
        private static Method GetMethodWithContractFor(Method method)
        {
            Contract.Ensures(Contract.Result<Method>() != null || method == null);

            if (method == null) return null;

            // get rid of specialization
            while (method.Template != null) method = method.Template;

            var declType = method.DeclaringType;
            
            if (declType == null) return method;
            
            if (!declType.IsAbstract) return method; // only abstract types and interfaces can have OOB

            // get rid of instantiations
            while (declType.Template != null)
            {
                declType = declType.Template;
            }

            var al = declType.Attributes;
            // if (al != null)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    var attr = al[i];
                    if (attr == null) continue;

                    if (attr.Type == null) continue;
                    
                    if (attr.Type.Name == null) continue;
                    
                    if (attr.Type.Name.Name != "ContractClassAttribute") continue;

                    if (attr.Expressions == null) return method;
                    
                    Literal lit = attr.Expressions[0] as Literal;
                    
                    if (lit == null) return method; // not found
                    TypeNode t = lit.Value as TypeNode;
                    
                    if (t == null) return method; // not found
                    if (t.Template != null)
                    {
                        t = t.Template;
                    }

                    return HelperMethods.FindMatchingMethod(t, method);
                }
            }

            return method; // not found
        }

        [ContractVerification(true)]
        private void CheckMethodContract(MethodContract contract)
        {
            if (contract == null) return;

            this.CheckRequires(contract);
            this.CheckEnsures(contract);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "<>1__state"), ContractVerification(true)]
        private IEnumerable<Method> RootMethodsForRequires(Method method)
        {
            Contract.Ensures(Contract.Result<IEnumerable<Method>>() != null);

            Method rootMethod = OverriddenRootMethod(method);
            if (rootMethod != null && !ImplementsAnyInterfaces(rootMethod)) yield return rootMethod;

            if (method.ImplementedInterfaceMethods != null)
            {
                for (int i = 0; i < method.ImplementedInterfaceMethods.Count; i++)
                {
                    var intfMethod = method.ImplementedInterfaceMethods[i];
                    if (intfMethod != null) yield return intfMethod;
                }
            }

            if (method.ShallowImplicitlyImplementedInterfaceMethods != null)
            {
                for (int i = 0; i < method.ShallowImplicitlyImplementedInterfaceMethods.Count; i++)
                {
                    var intfMethod = method.ShallowImplicitlyImplementedInterfaceMethods[i];
                    
                    if (intfMethod != null) yield return intfMethod;
                }
            }
        }

        [ContractVerification(true)]
        private static bool ImplementsAnyInterfaces(Method rootMethod)
        {
            Contract.Requires(rootMethod != null);
            
            if (rootMethod.ImplementedInterfaceMethods != null && rootMethod.ImplementedInterfaceMethods.Count > 0)
                return true;
            
            if (rootMethod.ImplicitlyImplementedInterfaceMethods != null &&
                rootMethod.ImplicitlyImplementedInterfaceMethods.Count > 0) return true;
            
            return false;
        }

        private readonly TrivialHashtable invariantMethods;

        [ContractVerification(true)]
        public override void VisitMethod(Method method)
        {
            if (method == null) return;
            
            // closures used from methods are visited from the method from which they are referenced
            if (HelperMethods.IsCompilerGenerated(method)) return;

            var savedState = this.currentState;
            try
            {
                this.currentState = savedState.Derive(method);
                if (this.contractNodes.IsObjectInvariantMethod(method))
                {
                    return;
                }

                // Check Abbreviators

                if (ContractNodes.IsAbbreviatorMethod(method))
                {
                    if (method.IsVirtual)
                    {
                        this.HandleError(
                            new Error(1064,
                                "Contract abbreviator method '" + method.FullName +
                                "' cannot be virtual or implement an interface method.'",
                                HelperMethods.SourceContextOfMethod(method)));
                    }

                    if (!HelperMethods.IsVoidType(method.ReturnType))
                    {
                        this.HandleError(
                            new Error(1060,
                                "Contract abbreviator method '" + method.FullName + "' must have void return type.'",
                                HelperMethods.SourceContextOfMethod(method)));
                    }

                    if (method.Contract != null)
                    {
                        if (method.Contract.HasLegacyValidations)
                        {
                            this.HandleError(
                                new Error(1062,
                                    "Contract abbreviator '" + method.FullName +
                                    "' cannot contain if-then-throw contracts or validator calls. Only regular contracts and abbreviator calls are allowed.",
                                    HelperMethods.SourceContextOfMethod(method)));
                        }

                        if (method.Contract.EnsuresCount + method.Contract.RequiresCount == 0)
                        {
                            this.HandleError(
                                new Warning(1063, "No contracts recognized in contract abbreviator '" + method.FullName + "'.",
                                    HelperMethods.SourceContextOfMethod(method)));
                        }
                    }
                    else
                    {
                        // no contracts
                        this.HandleError(
                            new Warning(1063, "No contracts recognized in contract abbreviator '" + method.FullName + "'.",
                                HelperMethods.SourceContextOfMethod(method)));
                    }

                    // abbreviator code must be completely visible to all callers as it is effectively inlined there.
                    this.visibilityChecker.CheckRequires(method.Contract);
                    this.visibilityChecker.CheckAbbreviatorEnsures(method.Contract);

                    return; // no further checks as we check the rest in use-site methods.
                }

                // Check Contracts contents and visibility

                this.CheckMethodContract(method.Contract);

                // Check purity

                this.purityChecker.Check(method);

                // Check Method Body

                this.methodBodyChecker.Check(method);

                SourceContext sourceContext = HelperMethods.SourceContextOfMethod(method);
                
                Method firstRootMethod = null;

                var methodDeclaringType = method.DeclaringType;

                Contract.Assume(methodDeclaringType != null);
                
                TypeNode classForWhichThisIsContractMethod = HelperMethods.IsContractTypeForSomeOtherType(methodDeclaringType, this.contractNodes);

                foreach (var rootMethod in RootMethodsForRequires(method))
                {
                    Contract.Assume(rootMethod != null);

                    if (classForWhichThisIsContractMethod != null)
                    {
                        if (method.Contract == null) continue;

                        if (rootMethod.DeclaringType != classForWhichThisIsContractMethod)
                        {
                            this.HandleError(
                                new Warning(1076,
                                    string.Format(
                                        "Contract class {2} cannot define contract for method {0} as its original definition is not in type {1}. Define the contract on type {3} instead.",
                                        rootMethod.FullName, classForWhichThisIsContractMethod, methodDeclaringType.FullName,
                                        rootMethod.DeclaringType), 
                                    sourceContext));

                            continue;
                        }

                        if (!rootMethod.IsAbstract)
                        {
                            this.HandleError(
                                new Error(1077,
                                    string.Format(
                                        "Contract class {1} cannot define contract for non-abstract method {0}. Define the contract on {0} instead.",
                                        rootMethod.FullName, methodDeclaringType.FullName), 
                                    sourceContext));

                            continue;
                        }

                        // contract class methods can only override/implement the methods of their original type
                        continue;
                    }

                    if (!allowPreconditionsInOverrides)
                    {
                        // Overrides cannot add preconditions unless this is "the" contract method for an abstract method

                        if (method.Contract != null && method.Contract.Requires != null)
                        {
                            foreach (RequiresPlain req in method.Contract.Requires)
                            {
                                if (req == null) continue;

                                if (!this.explicitUserValidations || !req.IsFromValidation)
                                {
                                    SourceContext sc = req.SourceContext;
                                    if (rootMethod.DeclaringType is Interface)
                                    {
                                        this.HandleError(
                                            new Warning(1033,
                                                string.Format( "Method '{0}' implements interface method '{1}', thus cannot add Requires.", method.FullName, rootMethod.FullName), 
                                                sc));
                                    }
                                    else
                                    {
                                        this.HandleError(
                                            new Warning(1032,
                                                string.Format("Method '{0}' overrides '{1}', thus cannot add Requires.", method.FullName, rootMethod.FullName), 
                                                sc));
                                    }

                                    return; // only one error per method
                                }
                            }
                        }

                        // Multiple root methods with contracts

                        // check that none of them have contracts
                        if (firstRootMethod == null)
                        {
                            firstRootMethod = rootMethod;
                        }
                        else
                        {
                            bool someContracts = HasPlainRequires(GetMethodWithContractFor(rootMethod)) ||
                                                 HasPlainRequires(GetMethodWithContractFor(firstRootMethod));
                            if (someContracts)
                            {
                                // found 2 that are in conflict and at least one of them has contracts
                                this.HandleError(new Warning(1035,
                                    String.Format(
                                        "Method '{0}' cannot implement/override two methods '{1}' and '{2}', where one has Requires.",
                                        method.FullName, firstRootMethod.FullName, rootMethod.FullName), sourceContext));

                                return; // only one error per method
                            }
                        }
                    }

                    // if (classForWhichThisIsContractMethod != null) continue; // skip rest of checks in this iteration

                    // Check that validations are present if necessary

                    if (this.explicitUserValidations && !HasValidations(method.Contract))
                    {
                        var rootMethodContract = GetMethodWithContractFor(rootMethod).Contract;
                        if (rootMethodContract != null)
                        {
                            for (int i = 0; i < rootMethodContract.RequiresCount; i++)
                            {
                                var req = (RequiresPlain) rootMethodContract.Requires[i];
                                if (req == null) continue;

                                if (!req.IsWithException) continue;
                                
                                var operation = (rootMethod.DeclaringType is Interface) ? "implements" : "overrides";
                                
                                if (req.SourceConditionText != null && req.ExceptionType.Name != null)
                                {
                                    this.HandleError(new Warning(1055,
                                        String.Format(
                                            "Method '{0}' should contain custom argument validation for 'Requires<{3}>({2})' as it {4} '{1}' which suggests it does. If you don't want to use custom argument validation in this assembly, change the assembly mode to 'Standard Contract Requires'.",
                                            method.FullName,
                                            rootMethod.FullName,
                                            req.SourceConditionText,
                                            req.ExceptionType.Name.Name,
                                            operation),
                                        sourceContext));
                                }
                                else
                                {
                                    this.HandleError(new Warning(1055,
                                        String.Format(
                                            "Method '{0}' should contain custom argument validation as it {2} '{1}' which suggests it does. If you don't want to use custom argument validation in this assembly, change the assembly mode to 'Standard Contract Requires'.",
                                            method.FullName,
                                            rootMethod.FullName,
                                            operation),
                                        sourceContext));

                                    return; // avoid dups
                                }
                            }
                        }
                    }
                }

                // Check that OOB types have no legacy requires (can't inherit them anyway, so should be Requires<E>)

                if (classForWhichThisIsContractMethod != null)
                {
                    if (HasValidations(method.Contract))
                    {
                        this.HandleError(new Error(1078,
                            string.Format(
                                "Method '{0}' annotating type '{1}' should use Requires<E> instead of custom validation.",
                                method.FullName,
                                classForWhichThisIsContractMethod.FullName),
                            sourceContext));
                    }

                    // skip rest of checks
                    return;
                }

                // Explicit parameter validations should not use Requires<E>

                if (this.explicitUserValidations && method.Contract != null)
                {
                    for (int i = 0; i < method.Contract.RequiresCount; i++)
                    {
                        var req = (RequiresPlain) method.Contract.Requires[i];
                        if (req == null) continue;

                        if (req.IsFromValidation) continue;
                        
                        if (!req.IsWithException) continue;
                        
                        // found Requires<E>
                        this.HandleError(new Error(1058,
                            String.Format(
                                "Method '{0}' should not have Requires<E> contract when assembly mode is set to 'Custom Parameter Validation'. Either change it to a legacy if-then-throw validation or change the assembly mode to 'Standard Contract Requires'.",
                                method.FullName), req.SourceContext));
                        
                        return; // avoid duplicate errors.
                    }
                }

                // non-user validation assembly

                if (!this.explicitUserValidations && HasValidations(method.Contract))
                {
                    if (firstRootMethod != null)
                    {
                        this.HandleError(new Warning(1056,
                            String.Format(
                                "Method '{0}' has custom parameter validation but assembly mode is not set to support this. It will be ignored (but base method contract may be inherited).",
                                method.FullName), sourceContext));
                    }
                    else
                    {
                        this.HandleError(new Warning(1057,
                            String.Format(
                                "Method '{0}' has custom parameter validation but assembly mode is not set to support this. It will be treated as Requires<E>.",
                                method.FullName), sourceContext));
                    }
                }

                // Check Validators

                if (ContractNodes.IsValidatorMethod(method))
                {
                    if (method.IsVirtual)
                    {
                        this.HandleError(new Error(1059,
                            "Contract argument validator method '" + method.FullName +
                            "' cannot be virtual or implement an interface method.'",
                            HelperMethods.SourceContextOfMethod(method)));
                    }

                    if (!HelperMethods.IsVoidType(method.ReturnType))
                    {
                        this.HandleError(new Error(1050,
                            "Contract argument validator method '" + method.FullName + "' must have void return type.'",
                            HelperMethods.SourceContextOfMethod(method)));
                    }

                    if (method.Contract != null)
                    {
                        if (!method.Contract.HasLegacyValidations)
                        {
                            this.HandleError(new Warning(1053,
                                "No validation code recognized in contract argument validator '" + method.FullName +
                                "'.",
                                HelperMethods.SourceContextOfMethod(method)));
                        }

                        if (HasNonValidationContract(method.Contract))
                        {
                            this.HandleError(new Error(1054,
                                "Contract argument validator '" + method.FullName +
                                "' cannot contain ordinary contracts. Only if-then-throw or validator calls are allowed.",
                                HelperMethods.SourceContextOfMethod(method)));
                        }
                    }
                    else
                    {
                        // no validations
                        this.HandleError(new Warning(1053,
                            "No validation code recognized in contract argument validator '" + method.FullName + "'.",
                            HelperMethods.SourceContextOfMethod(method)));
                    }
                }
            }
            finally
            {
                this.currentState = savedState;
            }
        }

        private static bool HasValidations(MethodContract methodContract)
        {
            return methodContract != null && methodContract.HasLegacyValidations;
        }

        private void CheckRequires(MethodContract methodContract)
        {
            this.preconditionChecker.CheckRequires(methodContract);
            this.visibilityChecker.CheckRequires(methodContract);
        }

        private void CheckEnsures(MethodContract methodContract)
        {
            this.postconditionChecker.CheckEnsures(methodContract);
        }

        public override void VisitAssembly(AssemblyNode assembly)
        {
            this.currentState = new CurrentState(assembly);

            if (ContractNodes.IsAlreadyRewritten(assembly))
            {
                this.HandleError(new Error(1029, "Cannot extract contracts from rewritten assembly '" + assembly.Name + "'.", assembly.SourceContext));
                return;
            }

            base.VisitAssembly(assembly);
        }

        public override void VisitTypeNode(TypeNode typeNode)
        {
            if (typeNode == null) return;

            var savedState = this.currentState;
            this.currentState = savedState.Derive(typeNode);
            
            try
            {
                GatherInvariantMethods(typeNode.Members);

                this.CheckTypeNode(typeNode);

                base.VisitTypeNode(typeNode);

                this.invariantCallChecker.VisitMemberList(typeNode.Members);
            }
            finally
            {
                this.currentState = savedState;
            }
        }

        /// <summary>
        /// Gather the invariant methods before we visit the children so we have populated them for all visible parts (which are only the type
        /// and it's nested types due to the privacy of invariant methods)
        /// </summary>
        private void GatherInvariantMethods(MemberList memberList)
        {
            for (int i = 0; i < memberList.Count; i++)
            {
                var method = memberList[i] as Method;
                if (method == null) continue;

                if (this.contractNodes.IsObjectInvariantMethod(method))
                {
                    this.invariantMethods[method.UniqueKey] = method;
                }
            }
        }

        public override void VisitField(Field field)
        {
            if (field == null) return;

            this.CheckField(field);
        }

        public override void VisitClass(Class Class)
        {
            if (Class == null) return;

            var savedState = this.currentState;

            this.currentState = savedState.Derive(Class);

            try
            {
                this.CheckClass(Class);

                CheckForWrapperImplementationsForInheritedInterfaceImplementations(Class);
            }
            finally
            {
                this.currentState = savedState;
            }

            base.VisitClass(Class);
        }

        private void CheckForWrapperImplementationsForInheritedInterfaceImplementations(Class Class)
        {
            Contract.Requires(Class != null);

            if (Class.Interfaces == null) return;

            if (this.runtimeCheckingLevel == 0) return;

            if (!HelperMethods.ContractOption(Class, "runtime", "checking") ||
                !HelperMethods.ContractOption(Class, "contract", "inheritance"))
            {
                return;
            }

            for (int i = 0; i < Class.Interfaces.Count; i++)
            {
                var intf = Class.Interfaces[i];
                if (intf == null) continue;

                CheckForWrapperImplementationsForInheritedInterfaceImplementations(Class, intf);
            }
        }

        [ContractVerification(true)]
        private void CheckForWrapperImplementationsForInheritedInterfaceImplementations(Class Class, Interface intf)
        {
            Contract.Requires(Class != null);
            Contract.Requires(intf != null);

            var members = intf.Members;
            // if (members == null) return;
            for (int i = 0; i < members.Count; i++)
            {
                var intfMethod = members[i] as Method;
                if (intfMethod == null) continue;

                if (Class.ExplicitImplementation(intfMethod) != null) continue;

                if (Class.GetExactMatchingMethod(intfMethod) != null) continue;

                var contractMethod = HelperMethods.GetContractMethod(intfMethod);
                if (contractMethod == null) continue;

                var contract = contractMethod.Contract;
                if (contract == null) continue;
                
                if (contract.RequiresCount + contract.EnsuresCount == 0) continue;

                // find base implementing method
                var baseMethod = FindInheritedMethod(Class.BaseClass, intfMethod);
                if (baseMethod == null) continue;
                
                var baseMethodImplementsInterfaceMethod = false;
                if (baseMethod.ImplicitlyImplementedInterfaceMethods != null)
                {
                    for (int j = 0; j < baseMethod.ImplicitlyImplementedInterfaceMethods.Count; j++)
                    {
                        if (baseMethod.ImplicitlyImplementedInterfaceMethods[j] == intfMethod)
                        {
                            baseMethodImplementsInterfaceMethod = true;
                            break;
                        }
                    }
                }

                if (baseMethodImplementsInterfaceMethod) continue;

                if (this.runtimeCheckingLevel < 3 && contract.RequiresCount == 0) continue;

                if (this.addInterfaceWrappersWhenNeeded)
                {
                    AddInterfaceImplementationWrapper(Class, intfMethod, baseMethod);
                }
                else
                {
                    this.HandleError(new Warning(1080,
                        string.Format(
                            "Type {0} implements {1} by inheriting {2} causing the interface contract to not be checked at runtime. Consider adding a wrapper method.",
                            Class.FullName, intfMethod.FullName, baseMethod.FullName),
                        default(SourceContext)));
                }
            }
        }

        private static void AddInterfaceImplementationWrapper(Class Class, Method intfMethod, Method baseMethod)
        {
            var d = new Duplicator(Class.DeclaringModule, Class);
            
            d.SkipBodies = true;

            var copy = d.VisitMethod(baseMethod);
            
            copy.Flags = MethodFlags.Private | MethodFlags.HideBySig | MethodFlags.Virtual | MethodFlags.NewSlot |
                         MethodFlags.Final;
            
            copy.ImplementedInterfaceMethods = new MethodList(intfMethod);
            copy.Name = Identifier.For("InheritedInterfaceImplementationContractWrapper$" + intfMethod.Name.Name);
            copy.ClearBody();
            copy.ThisParameter.Type = Class;
            
            var bodyBlock = new Block(new StatementList());
            copy.Body = new Block(new StatementList(bodyBlock));

            // add call to baseMethod
            var calledMethod = (baseMethod.TemplateParameters != null && baseMethod.TemplateParameters.Count > 0)
                ? baseMethod.GetTemplateInstance(Class, copy.TemplateParameters)
                : baseMethod;

            var argList = new ExpressionList();
            for (int i = 0; i < copy.Parameters.Count; i++)
            {
                argList.Add(copy.Parameters[i]);
            }

            var callExpression = new MethodCall(new MemberBinding(copy.ThisParameter, calledMethod), argList);
            if (HelperMethods.IsVoidType(intfMethod.ReturnType))
            {
                bodyBlock.Statements.Add(new ExpressionStatement(callExpression));
            }
            else
            {
                bodyBlock.Statements.Add(new Return(callExpression));
            }
            
            Class.Members.Add(copy);
        }

        private static Method FindInheritedMethod(Class baseClass, Method interfaceMethod)
        {
            while (baseClass != null)
            {
                if (InterfaceListContains(baseClass.Interfaces, interfaceMethod.DeclaringType))
                {
                    // instrumented in base
                    return null;
                }

                var candidate = baseClass.GetExactMatchingMethod(interfaceMethod);
                
                if (candidate != null) return candidate;
            
                baseClass = baseClass.BaseClass;
            }

            return null;
        }

        private static bool InterfaceListContains(InterfaceList interfaceList, TypeNode intf)
        {
            if (interfaceList == null) return false;

            for (int i = 0; i < interfaceList.Count; i++)
            {
                if (interfaceList[i] == intf) return true;
            }
            
            return false;
        }

        private static bool HasPlainRequires(Method method)
        {
            if (method == null) return false;

            var methodContract = method.Contract;
            
            return methodContract != null && methodContract.RequiresCount > 0;
        }

        private static bool HasNonValidationContract(MethodContract methodContract)
        {
            if (methodContract == null) return false;

            return methodContract.EnsuresCount > 0 ||
                   Contract.Exists(0, methodContract.RequiresCount,
                       i => !((RequiresPlain) methodContract.Requires[i]).IsFromValidation);
        }

        public void VisitForPostCheck(AssemblyNode assemblyNode)
        {
            this.VisitAssembly(assemblyNode);
        }

        [ContractVerification(true)]
        internal class BasicChecker : InspectorIncludingClosures
        {
            // F:
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
             System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.errorHandler != null);
                Contract.Invariant(this.usedToExtract != null);
            }

            public BasicChecker(Action<CompilerError> errorHandler, ContractNodes usedToExtract)
            {
                Contract.Requires(errorHandler != null);
                Contract.Requires(usedToExtract != null);

                this.errorHandler = errorHandler;
                this.usedToExtract = usedToExtract;
            }

            protected SourceContext lastSC;
            protected readonly Action<CompilerError> errorHandler;
            protected readonly ContractNodes usedToExtract;

            protected SourceContext CurrentSourceContext
            {
                get { return lastSC; }
            }

            public override void VisitStatementList(StatementList statements)
            {
                if (statements == null) return;
                for (int i = 0; i < statements.Count; i++)
                {
                    Statement st = statements[i];
                    if (st != null && st.SourceContext.IsValid)
                    {
                        lastSC = st.SourceContext;
                    }

                    this.Visit(st);
                }
            }

            public override void VisitOldExpression(OldExpression oldExpression)
            {
                this.errorHandler(new Error(1023, "OldValue() can be used only in Ensures.", lastSC));
            }

            public override void VisitReturnValue(ReturnValue returnValue)
            {
                this.errorHandler(new Error(1023, "Result() can be used only in Ensures.", lastSC));
            }

            internal void Check(Method method)
            {
                Contract.Requires(method != null); // F:

                this.CurrentMethod = method;
                this.VisitBlock(method.Body);
            }
        }

        [ContractVerification(true)]
        internal class MethodBodyChecker : BasicChecker
        {
            public MethodBodyChecker(Action<CompilerError> handleError, ContractNodes usedToExtract)
                : base(handleError, usedToExtract)
            {
                // F:
                Contract.Requires(handleError != null);
                Contract.Requires(usedToExtract != null);
            }

            private static Method TemplateOrMethod(Method method)
            {
                if (method == null) return null;

                if (method.Template != null) return method.Template;
                
                return method;
            }

            public override void VisitMemberBinding(MemberBinding memberBinding)
            {
                if (memberBinding == null) return;

                var method = TemplateOrMethod(memberBinding.BoundMember as Method);
                
                if (usedToExtract.IsInvariantMethod(method))
                {
                    // indicates invariant use inside a method not marked by ObjectInvariantAttribute
                    // F: Added the assumption, is it always true???
                    Contract.Assume(this.CurrentMethod != null);

                    var containingMethodName = this.CurrentMethod.FullName;
                    
                    this.errorHandler(new Error(1022,
                        string.Format("Bad use of method 'Contract.Invariant' in method body '{0}'",
                            containingMethodName), lastSC));
                    
                    return;
                }

                base.VisitMemberBinding(memberBinding);
            }
        }

        /// <summary>
        /// called on all members of every type
        /// </summary>
        [ContractVerification(true)]
        private class CheckForCallsToInvariantMethods : Inspector
        {
            // F:
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
             System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.parent != null);
            }

            private readonly PostExtractorChecker parent;

            public CheckForCallsToInvariantMethods(PostExtractorChecker parent)
            {
                Contract.Requires(parent != null);
                this.parent = parent;
            }

            private SourceContext lastSC;

            public override void VisitStatementList(StatementList statements)
            {
                if (statements == null) return;

                for (int i = 0; i < statements.Count; i++)
                {
                    var st = statements[i];
                    if (st != null && st.SourceContext.IsValid)
                    {
                        lastSC = st.SourceContext;
                    }
                    
                    this.Visit(st);
                }
            }

            public override void VisitTypeNode(TypeNode typeNode)
            {
                // don't visit nested types...
            }

            public override void VisitMemberBinding(MemberBinding mb)
            {
                if (mb == null) return;

                var m = mb.BoundMember as Method;
                if (m == null) return;
                
                Method invMethod = this.InvariantMethods[m.UniqueKey] as Method;
                if (invMethod != null)
                {
                    this.parent.HandleError(new Error(1042,
                        "Explicit use of invariant method '" + invMethod.FullName + "' is not allowed.", this.lastSC));
                }
            }

            private TrivialHashtable InvariantMethods
            {
                get
                {
                    // F:
                    Contract.Ensures(Contract.Result<TrivialHashtable>() != null);

                    // F:
                    Contract.Assume(this.parent.invariantMethods != null);
                    return this.parent.invariantMethods;
                }
            }
        }

        [ContractVerification(true)]
        internal class InvariantChecker : BasicChecker
        {
            public InvariantChecker(Action<CompilerError> errorHandler, ContractNodes usedToExtract)
                : base(errorHandler, usedToExtract)
            {
                // F:
                Contract.Requires(errorHandler != null);
                Contract.Requires(usedToExtract != null);
            }

            internal void Check(TypeNode typeNode)
            {
                // F:
                Contract.Requires(typeNode != null);

                // F:
                Contract.Assume(typeNode.Contract != null);

                this.CurrentType = typeNode;
                this.VisitInvariantList(typeNode.Contract.Invariants);
            }
        }

        [ContractVerification(true)]
        internal class PreconditionChecker : BasicChecker
        {
            private bool hasError;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")
            ]
            public bool HasError
            {
                get { return hasError; }
            }

            private Requires current;

            private Requires Current
            {
                get
                {
                    // F: this.current may be null (e.g. after the constructor)          
                    return this.current;
                }
                set
                {
                    // F:
                    Contract.Requires(value != null);
                    this.current = value;
                    this.lastSC = value.SourceContext;
                }
            }

            public PreconditionChecker(Action<CompilerError> errorHandler, ContractNodes usedToExtract)
                : base(errorHandler, usedToExtract)
            {
                // F:
                Contract.Requires(errorHandler != null);
                Contract.Requires(usedToExtract != null);
            }

            private void HandleError(string message)
            {
                // F:
                Contract.Requires(message != null);

                this.hasError = true;

                this.errorHandler(new Error(1011, message, this.CurrentSourceContext));

                // F: In general, it is not always true that this.Current != null, but it seems that at this call it is. To check
                Contract.Assume(this.Current != null);
                if (this.Current.DefSite.IsValid)
                {
                    this.errorHandler(new Warning(0, "  location related to previous warning.", this.Current.DefSite));
                }
            }

            public override void VisitThis(This This)
            {
                // F: Should this be a precondition?
                Contract.Assume(This != null);

                // the second test is to avoid complaining about "this" use in closures used in ctors
                if (this.CurrentMethod is InstanceInitializer && This.DeclaringMethod == this.CurrentMethod)
                {
                    this.HandleError("This/Me cannot be used in Requires of a constructor");
                }
            }

            internal void CheckPrecondition(Requires req)
            {
                if (req == null) return;

                this.hasError = false;
                
                this.Current = req;
                
                this.Visit(req);
            }

            internal void CheckRequires(MethodContract contract)
            {
                if (contract == null) return;

                this.CurrentMethod = contract.DeclaringMethod;
                
                var requiresList = contract.Requires;
                if (requiresList == null) return;
                
                foreach (var req in requiresList)
                {
                    CheckPrecondition(req);
                }
            }
        }

        [ContractVerification(false)] // F: TODO
        internal class PostconditionChecker : InspectorIncludingClosures
        {
            /// <summary>
            /// Parent instance.
            /// </summary>
            private bool fSharp;

            private bool checkingExceptionalPostcondition;
            private Action<CompilerError> errorHandler;

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
             System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.errorHandler != null);
            }

            private MethodContractElement Current { get; set; }

            private SourceContext CurrentSourceContext
            {
                get { return Current.SourceContext; }
            }

            private void HandleError(int number, string message)
            {
                // F:
                Contract.Requires(message != null);

                this.errorHandler(new Error(number, message, this.CurrentSourceContext));
                if (this.Current.DefSite.IsValid)
                {
                    this.errorHandler(new Warning(0, "  location related to previous warning.", this.Current.DefSite));
                }
            }

            public PostconditionChecker(Action<CompilerError> errorHandler, bool fSharp)
            {
                Contract.Requires(errorHandler != null);

                this.errorHandler = errorHandler;
                this.fSharp = fSharp;
            }

            public override void VisitOldExpression(OldExpression oldExpression)
            {
                if (oldExpression == null) return;

                // Make sure the argument to Old() isn't just a literal.
                if (oldExpression.expression is Literal)
                    this.HandleError(1000, "Literal expression in Old expression.");
                
                base.VisitOldExpression(oldExpression);
            }

            public override void VisitReturnValue(ReturnValue returnValue)
            {
                if (returnValue == null) return;

                var type = returnValue.Type;
                
                if (type == null) return;

                CheckProperUseOfReturnValueHelper(type);
            }

            private void CheckProperUseOfReturnValueHelper(TypeNode type)
            {
                if (this.checkingExceptionalPostcondition)
                {
                    Method method = this.CurrentMethod;
                    this.HandleError(1001,
                        "In method " + method.FullName + ": Cannot refer to Result in an exceptional postcondition.");
                }
                else
                {
                    // Make sure the type of result matches the type of the method.
                    //Method method = GetLastNode<Method>();
                    Method method = this.CurrentMethod;
                    if (!this.fSharp && type != method.ReturnType &&
                        !method.ReturnType.IsAssignableTo(type, this.CurrentSubstitution))
                    {
                        this.HandleError(1002,
                            "In method " + method.FullName + ": Detected a call to Result with '" + type.FullName +
                            "', should be '" + method.ReturnType.FullName + "'.");
                    }
                }
            }

            public override void VisitEnsuresExceptional(EnsuresExceptional exceptional)
            {
                this.checkingExceptionalPostcondition = true;
                base.VisitEnsuresExceptional(exceptional);
            }

            public override void VisitEnsuresNormal(EnsuresNormal normal)
            {
                this.checkingExceptionalPostcondition = false;
                base.VisitEnsuresNormal(normal);
            }

            internal void CheckPostcondition(Ensures ens)
            {
                if (ens == null) return;
                this.Current = ens;
                this.Visit(ens);
            }

            [ContractVerification(true)]
            internal void CheckEnsures(MethodContract contract)
            {
                if (contract == null) return;
                Contract.Assume(contract.DeclaringMethod != null);
                this.CurrentMethod = contract.DeclaringMethod;
                for (int i = 0; i < contract.EnsuresCount; i++)
                {
                    var ens = contract.Ensures[i];
                    CheckPostcondition(ens);
                }

                for (int i = 0; i < contract.ModelEnsuresCount; i++)
                {
                    var ens = contract.ModelEnsures[i];
                    CheckPostcondition(ens);
                }
                
                for (int i = 0; i < contract.AsyncEnsuresCount; i++)
                {
                    var ens = contract.AsyncEnsures[i];
                    CheckPostcondition(ens);
                }
            }
        }

        /// <summary>
        /// Checks for a minimum level of visibility in a code tree.
        /// </summary>
        [ContractVerification(true)]
        private class VisibilityChecker
        {
            // F:
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
             System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.visibilityHelper != null);
                Contract.Invariant(this.errorHandler != null);
            }

            private readonly Action<CompilerError> errorHandler;
            private readonly ContractNodes contractNodes;

            private readonly VisibilityHelper visibilityHelper = new VisibilityHelper();

            public VisibilityChecker(Action<CompilerError> errorHandler, ContractNodes usedToExtract)
            {
                Contract.Requires(errorHandler != null);

                this.errorHandler = errorHandler;
                this.contractNodes = usedToExtract;
            }

            private void HandleError(Method method, int number, string message, MethodContractElement contract)
            {
                Contract.Requires(contract != null);
                Contract.Requires(message != null); // F:

                this.errorHandler(new Error(number, message, contract.SourceContext));
                if (contract.DefSite.IsValid)
                {
                    this.errorHandler(new Warning(0, "  location related to previous warning.", contract.DefSite));
                }
            }

            private void CheckRequires(Method method, Requires requires)
            {
                // F:
                Contract.Requires(requires != null);
                Contract.Requires(method != null);

                CheckRequiresPlain(method, requires as RequiresPlain);

                var nonVisible = this.visibilityHelper.AsVisibleAs(requires.Condition, method);
                if (nonVisible != null)
                {
                    string message = "Member '" + nonVisible.FullName +
                                     "' has less visibility than the enclosing method '"
                                     + method.FullName + "'.";
                    this.HandleError(method, 1038, message, requires);
                }
            }

            [Pure]
            private void CheckRequiresPlain(Method method, RequiresPlain plain)
            {
                // F: Is this a precondition? Or simply return if method == null?
                Contract.Requires(method != null);

                if (plain == null) return;

                if (plain.ExceptionType != null)
                {
                    if (!HelperMethods.IsTypeAsVisibleAs(plain.ExceptionType, method))
                    {
                        var msg =
                            string.Format(
                                "Exception type {0} in Requires<E> has less visibility than enclosing method {1}",
                                plain.ExceptionType.FullName, method.FullName);

                        this.errorHandler(new Error(1037, msg, plain.SourceContext));
                    }
                }
            }

            private void CheckEnsures(Method method, Ensures ensures)
            {
                // F:
                Contract.Requires(method != null);

                Contract.Requires(ensures != null);

                CheckEnsuresExceptional(method, ensures as EnsuresExceptional);

                var nonVisible = this.visibilityHelper.AsVisibleAs(ensures.PostCondition, method);
                if (nonVisible != null)
                {
                    string message = "Member '" + nonVisible.FullName +
                                     "' has less visibility than the enclosing method '"
                                     + method.FullName + "'.";
                    this.HandleError(method, 1038, message, ensures);
                }
            }

            [Pure]
            private void CheckEnsuresExceptional(Method method, EnsuresExceptional ensuresExceptional)
            {
                // F: Is this a precondition? Or simply return when method == null
                Contract.Requires(method != null);

                if (ensuresExceptional == null) return;
                if (ensuresExceptional.Type != null)
                {
                    if (!HelperMethods.IsTypeAsVisibleAs(ensuresExceptional.Type, method))
                    {
                        var msg =
                            string.Format(
                                "Exception type {0} in EnsuresOnThrow<E> has less visibility than enclosing method {1}",
                                ensuresExceptional.Type.FullName, method.FullName);

                        this.errorHandler(new Error(1037, msg, ensuresExceptional.SourceContext));
                    }
                }
            }

            /// <summary>
            /// If the method contract is for an interface/abstract type, we need to make sure we use the visibility of the target method, not the contract method.
            /// </summary>
            internal void CheckRequires(MethodContract methodContract)
            {
                if (methodContract == null || methodContract.Requires == null) return;

                // F:
                Contract.Assume(methodContract.DeclaringMethod != null);

                Method targetMethod = FindTargetMethod(methodContract.DeclaringMethod);

                foreach (var req in methodContract.Requires)
                {
                    Contract.Assume(req != null); //F:

                    this.CheckRequires(targetMethod, req);
                }
            }

            [Pure]
            private Method FindTargetMethod(Method method)
            {
                // F:
                Contract.Requires(method != null);
                Contract.Ensures(Contract.Result<Method>() != null);

                if (!method.IsVirtual) return method;

                var origType = HelperMethods.IsContractTypeForSomeOtherTypeUnspecialized(method.DeclaringType, this.contractNodes);
                if (origType == null) return method;

                var result = HelperMethods.FindImplementedMethodUnspecialized(origType, method);
                
                Contract.Assert(result != null);

                /*
        if (result == null)
        {
          Debug.Assert(false, "can't find original method"); // Clousot should prove it
          return method;
        }
         */
                return HelperMethods.Unspecialize(result);
            }

            /// <summary>
            /// This is called for abbreviators, as the ensures there must be visible to 
            /// all callers.
            /// </summary>
            [ContractVerification(true)]
            internal void CheckAbbreviatorEnsures(MethodContract methodContract)
            {
                if (methodContract == null) return;
                
                for (int i = 0; i < methodContract.EnsuresCount; i++)
                {
                    var ens = methodContract.Ensures[i];

                    Contract.Assume(ens != null);
                    Contract.Assume(methodContract.DeclaringMethod != null); // F:
                    
                    this.CheckEnsures(methodContract.DeclaringMethod, ens);
                }

                if (methodContract.ModelEnsuresCount > 0)
                {
                    var modelEnsures = methodContract.ModelEnsures[0];
                    if (modelEnsures != null)
                    {
                        this.HandleError(methodContract.DeclaringMethod, 1074,
                            "ContractAbbreviator methods cannot contain Ensures referring to Model members.",
                            modelEnsures);
                    }
                }
            }
        }

        /// <summary>
        /// Checks strict purity in a code tree.
        /// </summary>
        [ContractVerification(true)]
        internal class PurityChecker : InspectorIncludingClosures
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
             System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.errorHandler != null);
                Contract.Invariant(this.contractNodes != null);
            }

            /// <summary>
            /// Error handler
            /// </summary>
            private readonly Action<System.CodeDom.Compiler.CompilerError> errorHandler;

            private readonly bool fSharp = false;
            private readonly ContractNodes contractNodes;

            private SourceContext lastSourceContext;
            private bool assignmentFound;

            /// <summary>
            /// Creates a new instance of this class.
            /// </summary>
            /// <param name="owner">Parent checker that is used to report errors.</param>
            public PurityChecker(Action<System.CodeDom.Compiler.CompilerError> errorHandler, bool fSharp, ContractNodes contractNodes)
            {
                Contract.Requires(errorHandler != null);
                Contract.Requires(contractNodes != null);

                this.errorHandler = errorHandler;
                this.fSharp = fSharp;
                this.contractNodes = contractNodes;
            }

            public void CheckInvariants(TypeNode t, InvariantList invariants)
            {
                Contract.Requires(invariants != null);

                this.assignmentFound = false;
                foreach (var i in invariants)
                {
                    if (i == null) continue;

                    this.lastSourceContext = i.SourceContext;
                    this.CurrentMethod = i;
                    
                    this.Visit(i);
                    
                    Contract.Assume(this.CurrentMethod != null);
                    
                    if (this.assignmentFound)
                    {
                        this.errorHandler(
                            new Error(1003,
                                "Malformed contract. Found Invariant after assignment in method '" + this.CurrentMethod.FullName + "'.", 
                                i.SourceContext));
                        
                        break;
                    }
                }
            }

            public void Check(Method currentMethod)
            {
                // F:
                Contract.Requires(currentMethod != null);

                if (currentMethod.Contract == null) return;

                this.CurrentMethod = currentMethod;
                this.assignmentFound = false;
                bool errorIssued = false;
                
                for (int i = 0; i < currentMethod.Contract.RequiresCount; i++)
                {
                    var req = currentMethod.Contract.Requires[i];
                    if (req == null) continue;

                    this.lastSourceContext = req.SourceContext;
                    
                    this.Visit(req);
                    
                    if (this.assignmentFound)
                    {
                        errorIssued = true;
                        
                        this.errorHandler(new Error(1004,
                            "Malformed contract. Found Requires after assignment in method '"
                            + currentMethod.FullName + "'.", req.SourceContext));
                        
                        break;
                    }
                }

                if (errorIssued) return;
                
                for (int i = 0; i < currentMethod.Contract.EnsuresCount; i++)
                {
                    var ens = currentMethod.Contract.Ensures[i];
                    if (ens == null) continue;

                    this.lastSourceContext = ens.SourceContext;
                    this.Visit(ens);
                    
                    if (this.assignmentFound)
                    {
                        errorIssued = true;
                        
                        this.errorHandler(new Error(1005,
                            "Malformed contract. Found Ensures after assignment in method '"
                            + currentMethod.FullName + "'.", ens.SourceContext));
                        
                        break;
                    }
                }

                if (errorIssued) return;
                
                for (int i = 0; i < currentMethod.Contract.AsyncEnsuresCount; i++)
                {
                    var ens = currentMethod.Contract.AsyncEnsures[i];
                    if (ens == null) continue;
                    
                    this.lastSourceContext = ens.SourceContext;
                    
                    this.Visit(ens);
                    
                    if (this.assignmentFound)
                    {
                        errorIssued = true;
                        this.errorHandler(new Error(1005,
                            "Malformed contract. Found Ensures after assignment in method '"
                            + currentMethod.FullName + "'.", ens.SourceContext));
                        break;
                    }
                }

                if (errorIssued) return;
                
                for (int i = 0; i < currentMethod.Contract.ModelEnsuresCount; i++)
                {
                    var ens = currentMethod.Contract.ModelEnsures[i];
                    
                    if (ens == null) continue;
                    
                    this.lastSourceContext = ens.SourceContext;

                    this.Visit(ens);

                    if (this.assignmentFound)
                    {
                        errorIssued = true;
                        
                        this.errorHandler(new Error(1005,
                            "Malformed contract. Found Ensures after assignment in method '"
                            + currentMethod.FullName + "'.", ens.SourceContext));
                        
                        break;
                    }
                }
            }

            /// <summary>
            /// Determines whether a statement is part of an anonymous delegate construction.
            /// </summary>
            /// <param name="statement">Statement to inspect.</param>
            /// <returns><c>true</c> if <paramref name="statement"/> is part of anonymous delegate construction; otherwise, false.</returns>
            internal static bool IsAnonymousDelegateConstruction(Statement statement)
            {
                AssignmentStatement assignment = statement as AssignmentStatement;
                if (assignment != null)
                {
                    if (assignment.Target != null &&
                        assignment.Target.Type != null &&
                        HelperMethods.IsCompilerGenerated(assignment.Target.Type))
                    {
                        return true;
                    }

                    MemberBinding binding = assignment.Target as MemberBinding;
                    if (binding != null)
                    {
                        if (binding.TargetObject != null &&
                            binding.TargetObject.Type != null &&
                            HelperMethods.IsCompilerGenerated(binding.TargetObject.Type))
                        {
                            return true;
                        }

                        if (HelperMethods.IsCompilerGenerated(binding.BoundMember)) return true;
                    }

                    Local local = assignment.Target as Local;
                    if (local != null &&
                        local.Name != null &&
                        local.Name.Name != null &&
                        local.Name.Name.StartsWith("<"))
                    {
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// Verifies that assignment statements are pure.
            /// </summary>
            /// <param name="assignment">Assignment to inspect.</param>
            /// <returns><paramref name="assignment"/></returns>
            public override void VisitAssignmentStatement(AssignmentStatement assignment)
            {
                // F: Should it be a Precondition?
                Contract.Assume(assignment != null);

                // Skip compiler-generated code.
                // Since we only visit contract regions, any assignment to state is not allowed.
                bool targetIsLocal = false;
                
                if (assignment.Target is Local) targetIsLocal = true;
                
                Indexer idxr = assignment.Target as Indexer;

                // Roslyn compiler introduced new pattern: in the expression tree there is an assignment
                // from local variable that holds expression to the array. But unlike old compiler
                // Roslyn is not introducing local variable for holding an array.
                // Instead of that it uses "dup" instruction and assignes parameter expression
                // directly to the stack slot.
                // This change is addressed here, because otherwise ccrewrite will fail
                // with an error.
                if (idxr != null && (idxr.Object is Local || IsConstructedArrayIndexer(idxr) || ExpressionTreeInitialization(assignment)))
                    targetIsLocal = true;
                
                // Assignments to locals that are structs show up as address deference
                AddressDereference addressDereference = assignment.Target as AddressDereference;
                if (addressDereference != null)
                {
                    UnaryExpression ue = addressDereference.Address as UnaryExpression;
                    if (ue != null)
                    {
                        // F: no idea why this is != null
                        Contract.Assume(assignment.Target.Type != null);

                        if (ue.Operand is Local &&
                            (assignment.Target.Type.IsPrimitive || assignment.Target.Type.IsStructural || assignment.Target.Type.IsTypeArgument ||
                             assignment.Target.Type is Struct || assignment.Target.Type is EnumNode))
                        {
                            targetIsLocal = true;
                        }
                    }
                }

                if (!IsAnonymousDelegateConstruction(assignment) && !targetIsLocal)
                {
                    this.assignmentFound = true;
                    //this.errorHandler(new Error("Detected assignment in a pure region in method '"
                    //  + this.currentMethod.FullName + "'.", assignment.SourceContext));
                }

                base.VisitAssignmentStatement(assignment);
            }
            
            /// <summary>
            /// Returns true the assignment was done from local variable that holds parameter expression
            /// to the indexer.
            /// </summary>
            private bool ExpressionTreeInitialization(AssignmentStatement assignment)
            {
                if (assignment == null || assignment.Source == null)
                    return false;

                var sourceAsLocal = assignment.Source as Local;
                return NameUtils.IsExpressionTreeLocal(sourceAsLocal);
            }
            
            /// <summary>
            ///     Determines whether the specified indexer refers to an array
            ///     created in the currently validated contract.
            /// </summary>
            /// <param name="indexer">
            ///     An indexer which references object origin shall be determined.
            /// </param>
            /// <returns>
            ///     <see langword="true"/> if the specified indexer refers to an array
            ///     created in the currently validated contract;
            ///     <see langword="false"/>, otherwise.
            /// </returns>
            private bool IsConstructedArrayIndexer(Indexer indexer)
            {
                if (!seenConstructArray || !seenDup)
                {
                    return false;
                }

                Node obj = indexer.Object;
                if (obj == null)
                {
                    return false;
                }

                return obj.NodeType == NodeType.Pop;
            }
            
            /// <summary>
            /// Verifies that method calls are to pure methods.
            /// </summary>
            /// <param name="call">Call to inspect.</param>
            /// <returns><paramref name="call"/></returns>
            public override void VisitMethodCall(MethodCall call)
            {
                // F: Should it be a precondition?
                Contract.Assume(call != null);

                MemberBinding binding = call.Callee as MemberBinding;
                Method method = binding != null ? binding.BoundMember as Method : null;

                bool pure = this.contractNodes.IsPure(method);

                // F: For some reason the code below relies on method != null, and hence to binding call.Callee <: MemberBinding
                Contract.Assume(method != null);

                if (!this.fSharp && !pure && method != this.contractNodes.AssumeMethod)
                {
                    // F:
                    Contract.Assume(this.CurrentMethod != null);

                    this.errorHandler(new Warning(1036,
                        "Detected call to method '" + method.FullName + "' without [Pure] in contracts of method '" +
                        this.CurrentMethod.FullName + "'.", this.lastSourceContext));
                }

                // make sure we now assume this method is pure for the rest of the analysis
                if (!pure)
                {
                    var template = method;

                    while (template.Template != null) template = template.Template;
                    
                    if (template.Contract == null)
                    {
                        template.Contract = new MethodContract(template);
                    }
                    
                    template.Contract.IsPure = true;
                }

                base.VisitMethodCall(call);
            }

            // Make sure we don't skip Pop, Dup, Arglist nodes which the standard Inspector does.
            public override void VisitExpression(Expression expression)
            {
                if (expression == null) return;

                switch (expression.NodeType)
                {
                    case NodeType.Dup:
                        seenDup = true;
                        break;

                    case NodeType.Arglist:
                        return;

                    case NodeType.Pop:
                        UnaryExpression uex = expression as UnaryExpression;
                        if (uex != null)
                        {
                            this.VisitPop(uex.Operand);
                            return;
                        }
                        return;

                    default:
                        this.Visit(expression);
                        return;
                }
            }

            private bool seenDup;
            private bool seenConstructArray;

            public override void VisitEnsuresExceptional(EnsuresExceptional exceptional)
            {
                seenDup = false;
                seenConstructArray = false;
                base.VisitEnsuresExceptional(exceptional);
            }

            public override void VisitEnsuresNormal(EnsuresNormal normal)
            {
                seenDup = false;
                seenConstructArray = false;
                base.VisitEnsuresNormal(normal);
            }

            public override void VisitInvariant(Invariant invariant)
            {
                seenDup = false;
                seenConstructArray = false;
                base.VisitInvariant(invariant);
            }

            public override void VisitRequiresOtherwise(RequiresOtherwise otherwise)
            {
                seenDup = false;
                seenConstructArray = false;
                base.VisitRequiresOtherwise(otherwise);
            }

            public override void VisitRequiresPlain(RequiresPlain plain)
            {
                seenDup = false;
                seenConstructArray = false;
                base.VisitRequiresPlain(plain);
            }

            public override void VisitConstructArray(ConstructArray consArr)
            {
                this.seenConstructArray = true;

                base.VisitConstructArray(consArr);
            }

            public void VisitPop()
            {
                Contract.Assume(this.CurrentMethod != null);

                // MaF: this is too strict. Some C# code patterns use Dup and pop in a branch to avoid loading a memory location twice.
                // we now emit the error only if there was no prior dup
                if (!seenDup)
                {
                    this.errorHandler(new Warning(1069,
                        "Detected expression statement evaluated for potential side-effect in contracts of method '" +
                        this.CurrentMethod.FullName + "'.", this.lastSourceContext));
                }
            }

            public void VisitPop(Expression expression)
            {
                if (expression == null)
                {
                    VisitPop();
                    return;
                }

                // a pop where we know what we pop.

                this.VisitPop();
            }

            public override void VisitStatementList(StatementList statements)
            {
                if (statements == null) return;
                
                for (int i = 0; i < statements.Count; i++)
                {
                    var stmt = statements[i];
                    if (stmt == null) continue;

                    if (stmt.SourceContext.IsValid)
                    {
                        lastSourceContext = stmt.SourceContext;
                    }
                    this.Visit(stmt);
                }
            }
        }
    }
}