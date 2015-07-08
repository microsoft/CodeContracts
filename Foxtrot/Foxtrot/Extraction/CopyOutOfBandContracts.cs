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
using System.Compiler;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// This one expects to visit the OOB assembly
    /// </summary>
    [ContractVerification(true)]
    internal sealed class CopyOutOfBandContracts : Inspector
    {
        //private Duplicator outOfBandDuplicator;
        private readonly ForwardingDuplicator outOfBandDuplicator;
        private readonly AssemblyNode targetAssembly;

        private readonly List<KeyValuePair<Member, TypeNode>> toBeDuplicatedMembers =
            new List<KeyValuePair<Member, TypeNode>>();

        private TrivialHashtable duplicatedMembers;

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
         SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.toBeDuplicatedMembers != null);
            Contract.Invariant(this.outOfBandDuplicator != null);
            Contract.Invariant(this.duplicatedMembers != null);
        }

        [Conditional("CONTRACTTRACE")]
        private static void Trace(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public CopyOutOfBandContracts(AssemblyNode targetAssembly, AssemblyNode sourceAssembly,
            ContractNodes contractNodes, ContractNodes targetContractNodes)
        {
            Contract.Requires(targetAssembly != null);
            Contract.Requires(sourceAssembly != null);
            Contract.Requires(contractNodes != null);

            if (targetAssembly == sourceAssembly)
            {
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

        private void CopyMissingMembers()
        {
            Contract.Ensures(this.duplicatedMembers != null);

            this.duplicatedMembers = new TrivialHashtable(this.toBeDuplicatedMembers.Count*2);
            foreach (var missing in this.toBeDuplicatedMembers)
            {
                Contract.Assume(missing.Value != null);
                Contract.Assume(missing.Key != null);

                InstanceInitializer ctor = missing.Key as InstanceInitializer;
                if (ctor != null && ctor.ParameterCount == 0) continue;

                var targetType = missing.Value;

                Trace("COPYOOB: copying {0} to {1}", missing.Key.FullName, targetType.FullName);

                this.Duplicator.TargetType = targetType;

                var dup = (Member) this.Duplicator.Visit(missing.Key);

                targetType.Members.Add(dup);
                duplicatedMembers[missing.Key.UniqueKey] = missing;
            }
        }

        private Duplicator Duplicator
        {
            get
            {
                Contract.Ensures(Contract.Result<Duplicator>() != null);

                return this.outOfBandDuplicator;
            }
        }

        // Methods for setting up a duplicator to use to copy out-of-band contracts

        [ContractVerification(false)]
        private static bool FuzzyEqual(TypeNode t1, TypeNode t2)
        {
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

        private static bool FuzzyEqual(Parameter p1, Parameter p2)
        {
            if (p1 == null && p2 == null) return true;

            if (p1 == null) return false;

            if (p2 == null) return false;

            return FuzzyEqual(p1.Type, p2.Type);
        }

        private static bool FuzzyEqual(ParameterList xs, ParameterList ys)
        {
            if (xs == null && ys == null) return true;

            if (xs == null) return false;

            if (ys == null) return false;

            if (xs.Count != ys.Count) return false;

            for (int i = 0, n = xs.Count; i < n; i++)
            {
                if (!FuzzyEqual(xs[i], ys[i])) return false;
            }

            return true;
        }

        [Pure]
        private static Member FuzzilyGetMatchingMember(TypeNode t, Member m)
        {
            Contract.Requires(t != null);
            Contract.Requires(m != null);

            var candidates = t.GetMembersNamed(m.Name);
            Contract.Assert(candidates != null, "Clousot can prove it");
            for (int i = 0, n = candidates.Count; i < n; i++)
            {
                Member mem = candidates[i];
                if (mem == null) continue;

                if (!mem.Name.Matches(m.Name)) continue;

                // type case statement would be *so* nice right now
                // Can't test the NodeType because for mscorlib.Contracts, structs are read in as classes
                // because they don't extend the "real" System.ValueType, but the one declared in mscorlib.Contracts.
                //if (mem.NodeType != m.NodeType) continue;
                Method x = mem as Method; // handles regular Methods and InstanceInitializers
                if (x != null)
                {
                    Method m_prime = m as Method;
                    if (m_prime == null) continue;

                    if ((x.TemplateParameters == null) != (m_prime.TemplateParameters == null)) continue;

                    if (FuzzyEqual(m_prime.Parameters, x.Parameters)
                        && FuzzyEqual(m_prime.ReturnType, x.ReturnType)
                        && TemplateParameterCount(x) == TemplateParameterCount(m_prime))
                    {
                        return mem;
                    }

                    continue;
                }

                Field memAsField = mem as Field;
                if (memAsField != null)
                {
                    Field mAsField = m as Field;
                    if (mAsField == null) continue;

                    if (FuzzyEqual(mAsField.Type, memAsField.Type)) return mem;
                    
                    continue;
                }

                Event memAsEvent = mem as Event;
                if (memAsEvent != null)
                {
                    Event mAsEvent = m as Event;
                    if (mAsEvent == null) continue;
                    
                    if (FuzzyEqual(mAsEvent.HandlerType, memAsEvent.HandlerType)) return mem;
                    
                    continue;
                }

                Property memAsProperty = mem as Property;
                if (memAsProperty != null)
                {
                    Property mAsProperty = m as Property;
                    if (mAsProperty == null) continue;

                    if (FuzzyEqual(mAsProperty.Type, memAsProperty.Type)) return mem;

                    continue;
                }

                TypeNode memAsTypeNode = mem as TypeNode; // handles Class, Interface, etc.
                if (memAsTypeNode != null)
                {
                    TypeNode mAsTypeNode = m as TypeNode;
                    if (mAsTypeNode == null) continue;

                    if (FuzzyEqual(mAsTypeNode, memAsTypeNode)) return mem;

                    continue;
                }

                Contract.Assume(false, "Pseudo-typecase failed to find a match");
            }

            return null;
        }

        private void FuzzilyForwardReferencesFromSource2Target(AssemblyNode targetAssembly, AssemblyNode sourceAssembly)
        {
            Contract.Requires(targetAssembly != null);
            Contract.Requires(sourceAssembly != null);

            for (int i = 1, n = sourceAssembly.Types.Count; i < n; i++)
            {
                TypeNode currentType = sourceAssembly.Types[i];
                if (currentType == null) continue;

                TypeNode targetType = targetAssembly.GetType(currentType.Namespace, currentType.Name);
                if (targetType == null)
                {
                    if (Duplicator.TypesToBeDuplicated[currentType.UniqueKey] == null)
                    {
                        Duplicator.FindTypesToBeDuplicated(new TypeNodeList(currentType));
                    }

                    Trace("COPYOOB: type to be duplicated {0}", currentType.FullName);
                }
                else
                {
                    if (HelperMethods.IsContractTypeForSomeOtherType(currentType as Class))
                    {
                        // dummy contract target type. Ignore it. 
                        targetType.Members = new MemberList();
                        targetType.ClearMemberTable();
                    }

                    Contract.Assume(TemplateParameterCount(currentType) == TemplateParameterCount(targetType),
                        "Name mangling should ensure this");

                    Duplicator.DuplicateFor[currentType.UniqueKey] = targetType;

                    Trace("COPYOOB: forwarding {1} to {0}", currentType.FullName, targetType.FullName);

                    FuzzilyForwardType(currentType, targetType);
                }
            }
        }

        [Pure]
        private static int TemplateParameterCount(TypeNode type)
        {
            Contract.Requires(type != null);
            Contract.Ensures(type.TemplateParameters != null || Contract.Result<int>() == 0);

            return type.TemplateParameters == null ? 0 : type.TemplateParameters.Count;
        }

        [Pure]
        private static int TemplateParameterCount(Method method)
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

            for (int j = 0, o = currentType.Members.Count; j < o; j++)
            {
                Member currentMember = currentType.Members[j];
                if (currentMember == null) continue;

                Member existingMember = FuzzilyGetMatchingMember(targetType, currentMember);
                if (existingMember != null)
                {
                    FuzzilyForwardTypeMemberFromSource2Target(targetType, currentMember, existingMember);
                }
                else
                {
                    this.toBeDuplicatedMembers.Add(new KeyValuePair<Member, TypeNode>(currentMember, targetType));
                    // For types, prepare a bit more.
                    var nestedType = currentMember as TypeNode;
                    if (nestedType != null)
                    {
                        if (Duplicator.TypesToBeDuplicated[nestedType.UniqueKey] == null)
                        {
                            Duplicator.FindTypesToBeDuplicated(new TypeNodeList(nestedType));
                        }
                    }
                }
            }
        }

        private void FuzzilyForwardTypeMemberFromSource2Target(TypeNode targetType, Member currentMember,
            Member existingMember)
        {
            Contract.Requires(targetType != null);
            Contract.Requires(currentMember != null);
            Contract.Requires(existingMember != null);

            Trace("COPYOOB: Forwarding member {0} to {1}", currentMember.FullName, existingMember.FullName);

            Duplicator.DuplicateFor[currentMember.UniqueKey] = existingMember;
            Method method = currentMember as Method;
            if (method != null)
            {
                Method existingMethod = (Method) existingMember;

                Contract.Assume(TemplateParameterCount(method) == TemplateParameterCount(existingMethod));

                // forward any type parameters that the method has
                for (int i = 0, n = TemplateParameterCount(method); i < n; i++)
                {
                    Contract.Assert(TemplateParameterCount(existingMethod) > 0);
                    TypeNode currentTypeParameter = method.TemplateParameters[i];
                    if (currentTypeParameter == null) continue;
                    Duplicator.DuplicateFor[currentTypeParameter.UniqueKey] = existingMethod.TemplateParameters[i];
                }
            }

            TypeNode currentNested = currentMember as TypeNode;
            TypeNode targetNested = existingMember as TypeNode;

            if (currentNested != null)
            {
                Contract.Assume(targetNested != null);
                Contract.Assume(TemplateParameterCount(currentNested) == TemplateParameterCount(targetNested),
                    "should be true by mangled name matching");

                FuzzilyForwardType(currentNested, targetNested);
            }
        }

        /// <summary>
        /// Visiting a method in the shadow assembly
        /// </summary>
        public override void VisitMethod(Method method)
        {
            if (method == null) return;
            // we might have copied this method already
            if (this.duplicatedMembers[method.UniqueKey] != null)
            {
                return;
            }

            Method targetMethod = method.FindShadow(this.targetAssembly);

            if (targetMethod != null)
            {
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

                targetMethod.SetDelayedContract((m, dummy) =>
                {
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

        public override void VisitTypeNode(TypeNode typeNode)
        {
            if (typeNode == null) return;

            // we might have copied this type already
            if (this.duplicatedMembers[typeNode.UniqueKey] != null)
            {
                return;
            }

            TypeNode targetType = typeNode.FindShadow(this.targetAssembly);
            if (targetType != null)
            {
                if (targetType.Contract == null)
                {
                    targetType.Contract = new TypeContract(targetType, true);
                }

                this.outOfBandDuplicator.TargetType = targetType;
                if (typeNode.Contract != null)
                {
                    InvariantList duplicatedInvariants =
                        this.outOfBandDuplicator.VisitInvariantList(typeNode.Contract.Invariants);

                    targetType.Contract.Invariants = duplicatedInvariants;
                }

                CopyAttributesWithoutDuplicateUnlessAllowMultiple(targetType, typeNode);

                base.VisitTypeNode(typeNode);
            }
            else
            {
                // target type does not exist. Copy it
                if (typeNode.DeclaringType != null)
                {
                    // nested types are members and have been handled by CopyMissingMembers
                }
                else
                {
                    this.outOfBandDuplicator.VisitTypeNode(typeNode);
                }
            }
        }

        private void CopyAttributesWithoutDuplicateUnlessAllowMultiple(Member targetMember, Member sourceMember)
        {
            Contract.Requires(targetMember != null);
            Contract.Requires(sourceMember != null);

//      if (sourceMember.Attributes == null) return;
//      if (targetMember.Attributes == null) targetMember.Attributes = new AttributeList();
            var attrs = this.outOfBandDuplicator.VisitAttributeList(sourceMember.Attributes);

            Contract.Assume(attrs != null, "We fail to specialize the ensures");

            foreach (var a in attrs)
            {
                if (a == null) continue;

                // Can't look at a.Type because that doesn't get visited by the VisitAttributeList above
                // (Seems like a bug in the StandardVisitor...)
                TypeNode typeOfA = AttributeType(a);
                if (!a.AllowMultiple && targetMember.GetAttribute(typeOfA) != null)
                {
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

        private void CopyReturnAttributesWithoutDuplicateUnlessAllowMultiple(Method targetMember, Method sourceMember)
        {
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
                if (!a.AllowMultiple && HasAttribute(targetMember.ReturnAttributes, typeOfA))
                {
                    continue;
                }

                targetMember.ReturnAttributes.Add(a);
            }
        }

        private static bool HasAttribute(AttributeList attributes, TypeNode attributeType)
        {
            if (attributeType == null) return false;

            if (attributes == null) return false;

            for (int i = 0, n = attributes.Count; i < n; i++)
            {
                AttributeNode attr = attributes[i];
                if (attr == null) continue;

                MemberBinding mb = attr.Constructor as MemberBinding;
                if (mb != null)
                {
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
}