// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Can be used to find any member uses tagged with particular attributes (e.g. Model, RuntimeIgnored).
    /// Goes into anonymous delegates as well.
    /// </summary>
    public class CodeInspector : Inspector
    {
        public static bool IsRuntimeIgnored(Node node, ContractNodes contractNodes, TypeNode referencingType, bool skipQuantifiers)
        {
            CodeInspector ifrv = new CodeInspector(
                ContractNodes.RuntimeIgnoredAttributeName, contractNodes, referencingType, skipQuantifiers);

            ifrv.Visit(node);

            return ifrv.foundAttribute;
        }

        public static bool UsesModel(Node node, ContractNodes contractNodes)
        {
            CodeInspector ifrv = new CodeInspector(ContractNodes.ModelAttributeName, contractNodes, null, false);

            ifrv.Visit(node);

            return ifrv.foundAttribute;
        }

        /// <summary>
        /// True iff node should be ignored at runtime
        /// </summary>
        public bool foundAttribute;

        private readonly Identifier _attributeToFind;
        private readonly ContractNodes _contractNodes;
        private readonly bool _skipQuantifiers;
        private readonly Stack<TypeNode> _referencingType;
        private bool _generatedMethodContainsInvisibleMemberReference;

        private CodeInspector(Identifier attributeToFind, ContractNodes contractNodes, TypeNode referencingType, bool skipQuantifiers)
        {
            this.foundAttribute = false;

            _attributeToFind = attributeToFind;
            _contractNodes = contractNodes;
            _skipQuantifiers = skipQuantifiers;
            _referencingType = new Stack<TypeNode>();

            if (referencingType != null)
            {
                _referencingType.Push(referencingType);
            }
        }

        public override void VisitMemberBinding(MemberBinding memberBinding)
        {
            if (memberBinding.BoundMember != null)
            {
                if (HelperMethods.HasAttribute(memberBinding.BoundMember.Attributes, _attributeToFind))
                {
                    this.foundAttribute = true;
                    return;
                }

                if (_referencingType.Count > 0 &&
                    !HelperMethods.IsVisibleFrom(memberBinding.BoundMember, _referencingType.Peek()))
                {
                    _generatedMethodContainsInvisibleMemberReference = true;
                    this.foundAttribute = true;
                    return;
                }

                Method referencedMethod = HelperMethods.Unspecialize(memberBinding.BoundMember as Method);
                if (referencedMethod != null)
                {
                    if (HasAttribute(referencedMethod))
                    {
                        this.foundAttribute = true;
                        return;
                    }

                    if (_skipQuantifiers &&
                        (_contractNodes.IsForallMethod(referencedMethod) ||
                         _contractNodes.IsGenericForallMethod(referencedMethod) ||
                         _contractNodes.IsExistsMethod(referencedMethod) ||
                         _contractNodes.IsGenericExistsMethod(referencedMethod))
                        )
                    {
                        this.foundAttribute = true;
                        return;
                    }

                    // check if we deal with a contract method on an interface/abstract method that is annotated
                    var origType = HelperMethods.IsContractTypeForSomeOtherTypeUnspecialized(referencedMethod.DeclaringType, _contractNodes);

                    if (origType != null)
                    {
                        var origMethod = HelperMethods.FindImplementedMethodSpecialized(origType, referencedMethod);
                        if (HasAttribute(origMethod))
                        {
                            this.foundAttribute = true;
                            return;
                        }
                    }
                }
            }

            base.VisitMemberBinding(memberBinding);
        }

        private bool HasAttribute(Method referencedMethod)
        {
            if (referencedMethod != null)
            {
                // look for attribute on property
                if (referencedMethod.IsPropertyGetter && referencedMethod.DeclaringMember != null)
                {
                    if (HelperMethods.HasAttribute(referencedMethod.DeclaringMember.Attributes, _attributeToFind))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Handle bodies of anonymous delegates
        public override void VisitConstruct(Construct cons)
        {
            if (cons.Type.IsDelegateType())
            {
                UnaryExpression ue = cons.Operands[1] as UnaryExpression;
                if (ue == null) goto JustVisit;

                MemberBinding mb = ue.Operand as MemberBinding;
                if (mb == null) goto JustVisit;

                Method m = mb.BoundMember as Method;

                if (HelperMethods.IsCompilerGenerated(m))
                {
                    bool savedInvisibleMemberRef = _generatedMethodContainsInvisibleMemberReference;
                    try
                    {
                        _generatedMethodContainsInvisibleMemberReference = false;

                        var unspecedM = HelperMethods.Unspecialize(m);
                        var unspecedT = unspecedM.DeclaringType;

                        Contract.Assert(unspecedT.Template == null);

                        _referencingType.Push(unspecedT);
                        this.VisitBlock(unspecedM.Body);

                        if (_generatedMethodContainsInvisibleMemberReference)
                        {
                            // remove method (and all containing closure methods)
                            savedInvisibleMemberRef = true;
                            for (int i = 0; i < unspecedT.Members.Count; i++)
                            {
                                if (unspecedT.Members[i] == unspecedM)
                                {
                                    unspecedT.Members[i] = null;
                                    break;
                                }
                            }
                        }
                    }
                    finally
                    {
                        _generatedMethodContainsInvisibleMemberReference = savedInvisibleMemberRef;
                        _referencingType.Pop();
                    }
                }
            }

        JustVisit:

            base.VisitConstruct(cons);
        }
    }
}