using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// When a class is used to express the contracts for an interface (or a third-party class)
    /// certain modifications must be made to the code in the contained contracts. For instance,
    /// if the contract class uses implicit interface implementations, then it might have a call
    /// to one of those implementations in a contract, Requires(this.P), for some boolean property
    /// P. That call has to be changed to be a call to the interface method.
    /// 
    /// Note!! This modifies the contract class so that in the rewritten assembly it is defined
    /// differently than it was in the original assembly!!
    /// </summary>
    internal sealed class ScrubContractClass : InspectorIncludingClosures
    {
        private Class contractClass;
        private TypeNode abstractClass;
        private ExtractorVisitor parent;

        private TypeNode OriginalType
        {
            get { return this.abstractClass; }
        }

        public ScrubContractClass(ExtractorVisitor parent, Class contractClass, TypeNode originalType)
        {
            Contract.Requires(TypeNode.IsCompleteTemplate(contractClass));
            Contract.Requires(TypeNode.IsCompleteTemplate(originalType));

            this.parent = parent;
            this.contractClass = contractClass;
            this.abstractClass = originalType;
        }

        private Method currentMethod;

        public override void VisitMethod(Method method)
        {
            this.currentMethod = method;
            this.currentSourceContext = default(SourceContext);
            base.VisitMethod(method);
        }

        private SourceContext currentSourceContext;

        public override void VisitStatementList(StatementList statements)
        {
            if (statements == null) return;

            for (int i = 0; i < statements.Count; i++)
            {
                var stmt = statements[i];
                if (stmt == null) continue;

                if (stmt.SourceContext.IsValid)
                {
                    this.currentSourceContext = stmt.SourceContext;
                }

                this.Visit(stmt);
            }
        }

        public override void VisitMemberBinding(MemberBinding memberBinding)
        {
            if (memberBinding == null) return;

            base.VisitMemberBinding(memberBinding);

            var member = memberBinding.BoundMember;
            if (member == null) return;

            Contract.Assume(member.DeclaringType != null, "top-level types should not be memberbound");

            var declaringType = HelperMethods.Unspecialize(member.DeclaringType);
            if (declaringType == contractClass)
            {
                // must reroute
                Method method = member as Method;
                if (method != null)
                {
                    if (HelperMethods.IsClosureMethod(this.contractClass, HelperMethods.Unspecialize(method))) return;

                    var template = method.Template;

                    if (method.IsGeneric && template != null)
                    {
                        Method targetTemplate = HelperMethods.FindImplementedMethodSpecialized(this.OriginalType, template);
                        if (targetTemplate != null)
                        {
                            var target = targetTemplate.GetTemplateInstance(this.OriginalType, method.TemplateArguments);
                            memberBinding.BoundMember = target;
                            return;
                        }
                    }
                    else
                    {
                        Method target = HelperMethods.FindImplementedMethodSpecialized(this.OriginalType, method);
                        if (target != null)
                        {
                            memberBinding.BoundMember = target;
                            return;
                        }
                    }

                    // couldn't find target: must issue error
                    parent.HandleError(this.currentMethod, 1075,
                        string.Format(
                            "Contract class '{0}' references member '{1}' which is not part of the abstract class/interface being annotated.",
                            this.contractClass.FullName, member.FullName), currentSourceContext);
                }
            }
        }

        public override void VisitField(Field field)
        {
            base.VisitField(field);

            var type = HelperMethods.Unspecialize(field.Type);

            if (type == contractClass)
            {
                if (field.Type.TemplateArguments != null)
                {
                    var inst = this.OriginalType.GetTemplateInstance(field.Type, field.Type.TemplateArguments);
                    field.Type = inst;
                }
                else
                {
                    field.Type = this.OriginalType;
                }
            }
        }

        public override void VisitMethodCall(MethodCall call)
        {
            base.VisitMethodCall(call);

            // patch call to virtual if we patched the method to an interface method
            if (call == null) return;

            if (call.NodeType == NodeType.Callvirt) return;

            var memberBinding = call.Callee as MemberBinding;
            if (memberBinding == null) return;

            Method method = (Method) memberBinding.BoundMember;
            var dt = method.DeclaringType;
            if (dt is Interface)
            {
                call.NodeType = NodeType.Callvirt;
            }
        }
    }
}