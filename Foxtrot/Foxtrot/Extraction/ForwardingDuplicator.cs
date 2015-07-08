using System.Compiler;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// This duplicator is used to copy contract from Out-of-band assemblies onto the real assembly
    /// 
    /// It is in charge of rebinding all the members to the target assembly members.
    /// 
    /// Different assemblies can use contracts defined in different places:
    ///   - Microsoft.Contracts.dll for pre-v4.0 assemblies
    ///   - Mscorlib.Contracts.dll for the contracts within the shadow assembly for mscorlib
    ///   - mscorlib.dll for post-v4.0 assemblies
    /// Therefore, for an extracted assembly to be independently processed (e.g., by the
    /// rewriter), anything in the assembly dependent on the contracts used must be replaced.
    /// This visitor does that by turning calls to:
    ///   - OldValue into OldExpressions
    ///   - Result into ResultExpressions
    ///   - ForAll into a method call to the ForAll defined in targetContractNodes
    ///   - Exists into a method call to the Exists defined in targetContractNodes
    ///   - ValueAtReturn(x) into the AST "AddressDereference(x,T)" where T is the type instantiation
    ///       of the generic type that ValueAtReturn is defined over. [Note: this transformation
    ///       is *not* undone by the Rewriter -- or anyone else -- since its only purpose was to
    ///       get past the compiler and isn't needed anymore.]
    ///   - All attributes defined in contractNodes are turned into the equivalent attribute in targetContractNodes
    ///
    /// When the regular Duplicator visits a member reference and that member is a generic
    /// instance, its DuplicateFor table has only the templates in it. To find the corresponding
    /// generic instance, it uses Specializer.GetCorrespondingMember. That method assumes the
    /// the member's DeclaringType, which is in the duplicator's source has the same members
    /// as the corresponding type in the duplicator's target. It furthermore assumes that those
    /// members are in exactly the same order in the respective member lists.
    /// 
    /// But we cannot assume that when forwarding things from one assembly to another.
    /// So this subtype of duplicator just has an override for VisitMemberReference that
    /// uses a different technique for generic method instances.
    /// 
    /// </summary>
    [ContractVerification(true)]
    internal class ForwardingDuplicator : Duplicator
    {
        private readonly ContractNodes contractNodes;
        private readonly ContractNodes targetContractNodes;

        public ForwardingDuplicator(Module /*!*/ module, TypeNode type, ContractNodes contractNodes,
            ContractNodes targetContractNodes) : base(module, type)
        {
            this.contractNodes = contractNodes;
            this.targetContractNodes = targetContractNodes;
        }

        /// <summary>
        /// Note, we can't just use the duplicator's logic for Member references, because the member offsets in the source and
        /// target are vastly different.
        /// </summary>
        [ContractVerification(false)]
        public override Member VisitMemberReference(Member member)
        {
            Contract.Ensures(Contract.Result<Member>() != null || member == null);

            var asType = member as TypeNode;
            if (asType != null) return this.VisitTypeReference(asType);

            var targetDeclaringType = this.VisitTypeReference(member.DeclaringType);

            Method sourceMethod = member as Method;
            if (sourceMethod != null)
            {
                // Here's how this works:
                //
                // 2. Identify MArgs (type arguments of method instance)
                //
                // 4. Find index i of method template in TargetDeclaringTypeTemplate by name and type names
                // 5. TargetDeclaringType = instantiate TargetDeclaringType with mapped TArgs
                // 6. MethodTemplate = TargetDeclaringType[i]
                // 7. Instantiate MethodTemplate with MArgs
                //
                var sourceMethodTemplate = sourceMethod;
                // Find source method template, but leave type instantiation
                while (sourceMethodTemplate.Template != null && sourceMethodTemplate.TemplateArguments != null &&
                       sourceMethodTemplate.TemplateArguments.Count > 0)
                {
                    sourceMethodTemplate = sourceMethodTemplate.Template;
                }

                // Steps 1 and 2

                TypeNodeList targetMArgs = null;
                TypeNodeList sourceMArgs = sourceMethod.TemplateArguments;
                if (sourceMArgs != null)
                {
                    targetMArgs = new TypeNodeList(sourceMArgs.Count);
                    foreach (var mArg in sourceMArgs)
                    {
                        targetMArgs.Add(this.VisitTypeReference(mArg));
                    }
                }

                Method targetMethod;

                var targetMethodTemplate = targetDeclaringType.FindShadow(sourceMethodTemplate);
                if (targetMethodTemplate == null)
                {
                    // something is wrong. Let's not crash and simply keep the original
                    return sourceMethod;
                }

                if (targetMArgs != null)
                {
                    targetMethod = targetMethodTemplate.GetTemplateInstance(targetMethodTemplate.DeclaringType,
                        targetMArgs);
                }
                else
                {
                    targetMethod = targetMethodTemplate;
                }

                return targetMethod;
            }

            Field sourceField = member as Field;
            if (sourceField != null)
            {
                return targetDeclaringType.FindShadow(sourceField);
            }

            Property sourceProperty = member as Property;
            if (sourceProperty != null)
            {
                return targetDeclaringType.FindShadow(sourceProperty);
            }

            Event sourceEvent = member as Event;
            if (sourceEvent != null)
            {
                return targetDeclaringType.FindShadow(sourceEvent);
            }

            Debug.Assert(false, "what other members are there?");

            Member result = base.VisitMemberReference(member);

            return result;
        }

        /// <summary>
        /// Need to make sure that references to ForAll/Exists that could be to another assembly go to the target contract nodes
        /// implementation.
        /// </summary>
        public override Expression VisitMemberBinding(MemberBinding memberBinding)
        {
            if (memberBinding == null) return null;

            var result = base.VisitMemberBinding(memberBinding);

            memberBinding = result as MemberBinding;
            if (this.targetContractNodes != null && memberBinding != null && memberBinding.TargetObject == null)
            {
                // all methods are static
                Method method = memberBinding.BoundMember as Method;
                if (method == null) return memberBinding;

                Contract.Assume(this.contractNodes != null);

                if (method.Template == null)
                {
                    if (contractNodes.IsExistsMethod(method))
                    {
                        return new MemberBinding(null, targetContractNodes.GetExistsTemplate);
                    }
                    
                    if (contractNodes.IsForallMethod(method))
                    {
                        return new MemberBinding(null, targetContractNodes.GetForAllTemplate);
                    }
                }
                else
                {
                    // template != null
                    Method template = method.Template;
                    var templateArgs = method.TemplateArguments;

                    if (contractNodes.IsGenericForallMethod(template))
                    {
                        Contract.Assume(templateArgs != null);
                        Contract.Assume(targetContractNodes.GetForAllGenericTemplate != null);

                        return new MemberBinding(null,
                            targetContractNodes.GetForAllGenericTemplate.GetTemplateInstance(
                                targetContractNodes.GetForAllGenericTemplate.DeclaringType, templateArgs[0]));
                    }
                    
                    if (contractNodes.IsGenericExistsMethod(template))
                    {
                        Contract.Assume(templateArgs != null);
                        Contract.Assume(targetContractNodes.GetExistsGenericTemplate != null);

                        return new MemberBinding(null,
                            targetContractNodes.GetExistsGenericTemplate.GetTemplateInstance(
                                targetContractNodes.GetExistsGenericTemplate.DeclaringType, templateArgs[0]));
                    }
                }
            }

            return result;
        }

        [ContractVerification(false)]
        public override AttributeNode VisitAttributeNode(AttributeNode attribute)
        {
            attribute = base.VisitAttributeNode(attribute);

            if (attribute == null) return null;

            if (this.targetContractNodes == null) return attribute;

            if (attribute.Type == this.contractNodes.ContractClassAttribute)
            {
                return
                    new AttributeNode(
                        new MemberBinding(null,
                            this.targetContractNodes.ContractClassAttribute.GetConstructor(SystemTypes.Type)),
                        attribute.Expressions);
            }
            
            if (attribute.Type == this.contractNodes.ContractClassForAttribute)
            {
                return
                    new AttributeNode(
                        new MemberBinding(null,
                            this.targetContractNodes.ContractClassForAttribute.GetConstructor(SystemTypes.Type)),
                        attribute.Expressions);
            }
            
            if (attribute.Type == this.contractNodes.IgnoreAtRuntimeAttribute)
            {
                return
                    new AttributeNode(
                        new MemberBinding(null, this.targetContractNodes.IgnoreAtRuntimeAttribute.GetConstructor()),
                        null);
            }
            
            if (attribute.Type == this.contractNodes.InvariantMethodAttribute)
            {
                return
                    new AttributeNode(
                        new MemberBinding(null, this.targetContractNodes.InvariantMethodAttribute.GetConstructor()),
                        null);
            }
            
            if (attribute.Type == this.contractNodes.PureAttribute)
            {
                return
                    new AttributeNode(new MemberBinding(null, this.targetContractNodes.PureAttribute.GetConstructor()),
                        null);
            }
            
            if (attribute.Type == this.contractNodes.SpecPublicAttribute)
            {
                return
                    new AttributeNode(
                        new MemberBinding(null,
                            this.targetContractNodes.SpecPublicAttribute.GetConstructor(SystemTypes.String)),
                        attribute.Expressions);
            }
            
            if (attribute.Type == this.contractNodes.VerifyAttribute)
            {
                return
                    new AttributeNode(
                        new MemberBinding(null,
                            this.targetContractNodes.VerifyAttribute.GetConstructor(SystemTypes.Boolean)),
                        attribute.Expressions);
            }

            return attribute;
        }
    }
}