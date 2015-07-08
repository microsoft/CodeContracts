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

        private readonly Identifier attributeToFind;
        private readonly ContractNodes contractNodes;
        private readonly bool skipQuantifiers;
        private readonly Stack<TypeNode> referencingType;
        private bool generatedMethodContainsInvisibleMemberReference;

        private CodeInspector(Identifier attributeToFind, ContractNodes contractNodes, TypeNode referencingType, bool skipQuantifiers)
        {
            this.foundAttribute = false;
            
            this.attributeToFind = attributeToFind;
            this.contractNodes = contractNodes;
            this.skipQuantifiers = skipQuantifiers;
            this.referencingType = new Stack<TypeNode>();

            if (referencingType != null)
            {
                this.referencingType.Push(referencingType);
            }
        }

        public override void VisitMemberBinding(MemberBinding memberBinding)
        {
            if (memberBinding.BoundMember != null)
            {
                if (HelperMethods.HasAttribute(memberBinding.BoundMember.Attributes, this.attributeToFind))
                {
                    this.foundAttribute = true;
                    return;
                }

                if (referencingType.Count > 0 &&
                    !HelperMethods.IsVisibleFrom(memberBinding.BoundMember, this.referencingType.Peek()))
                {
                    this.generatedMethodContainsInvisibleMemberReference = true;
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

                    if (this.skipQuantifiers &&
                        (this.contractNodes.IsForallMethod(referencedMethod) ||
                         this.contractNodes.IsGenericForallMethod(referencedMethod) ||
                         this.contractNodes.IsExistsMethod(referencedMethod) ||
                         this.contractNodes.IsGenericExistsMethod(referencedMethod))
                        )
                    {
                        this.foundAttribute = true;
                        return;
                    }

                    // check if we deal with a contract method on an interface/abstract method that is annotated
                    var origType = HelperMethods.IsContractTypeForSomeOtherTypeUnspecialized(referencedMethod.DeclaringType, this.contractNodes);
                    
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
                    if (HelperMethods.HasAttribute(referencedMethod.DeclaringMember.Attributes, this.attributeToFind))
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
                    bool savedInvisibleMemberRef = this.generatedMethodContainsInvisibleMemberReference;
                    try
                    {
                        this.generatedMethodContainsInvisibleMemberReference = false;
                        
                        var unspecedM = HelperMethods.Unspecialize(m);
                        var unspecedT = unspecedM.DeclaringType;
                        
                        Contract.Assert(unspecedT.Template == null);
                        
                        this.referencingType.Push(unspecedT);
                        this.VisitBlock(unspecedM.Body);
                        
                        if (this.generatedMethodContainsInvisibleMemberReference)
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
                        this.generatedMethodContainsInvisibleMemberReference = savedInvisibleMemberRef;
                        this.referencingType.Pop();
                    }
                }
            }

            JustVisit:
            
            base.VisitConstruct(cons);
        }
    }
}