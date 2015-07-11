// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    [ContractVerification(true)]
    internal class InspectorIncludingClosures : Inspector
    {
        private Method _currentMethod;

        protected Method CurrentMethod
        {
            get { return _currentMethod; }
            set
            {
                _currentMethod = value;
                if (value != null)
                {
                    this.CurrentType = value.DeclaringType;
                }
            }
        }

        protected TypeNode CurrentType { get; set; }

        public override void VisitMethod(Method method)
        {
            if (method == null) return;
            var savedCurrentMethod = this.CurrentMethod;
            this.CurrentMethod = method;
            try
            {
                base.VisitMethod(method);
            }
            finally
            {
                this.CurrentMethod = savedCurrentMethod;
            }
        }

        /// <summary>
        /// Need to visit closures as well
        /// </summary>
        public override void VisitConstruct(Construct cons)
        {
            if (cons == null) return;
            if (cons.Operands == null) return;
            if (cons.Operands.Count < 2) return;

            if (cons.Type.IsDelegateType())
            {
                UnaryExpression ue = cons.Operands[1] as UnaryExpression;
                if (ue == null) goto JustVisit;

                MemberBinding mb = ue.Operand as MemberBinding;
                if (mb == null) goto JustVisit;

                Method m = mb.BoundMember as Method;

                var um = HelperMethods.Unspecialize(m);

                if (HelperMethods.IsAnonymousDelegate(um, this.CurrentType))
                {
                    this.VisitAnonymousDelegate(um, m);
                }
            }

        JustVisit:
            base.VisitConstruct(cons);
        }

        private readonly Stack<Method> _delegates = new Stack<Method>();
        private readonly Stack<Func<TypeNode, TypeNode>> _substitution = new Stack<Func<TypeNode, TypeNode>>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_delegates != null);
            Contract.Invariant(_substitution != null);
        }

        public override void VisitBlock(Block block)
        {
            Contract.Ensures(_delegates.Count == Contract.OldValue(_delegates.Count));

            base.VisitBlock(block);
        }

        public virtual void VisitAnonymousDelegate(Method method, Method instantiated)
        {
            if (method == null) return;

            _delegates.Push(method);
            _substitution.Push(Substitution(method, instantiated));

            try
            {
                this.VisitBlock(method.Body);
            }
            finally
            {
                _delegates.Pop();
                _substitution.Pop();
            }
        }

        protected Func<TypeNode, TypeNode> CurrentSubstitution
        {
            get
            {
                if (_substitution.Count == 0)
                {
                    return t => t;
                }

                return _substitution.Peek();
            }
        }

        private Func<TypeNode, TypeNode> Substitution(Method method, Method instantiated)
        {
            var previous = this.CurrentSubstitution;
            if (method == instantiated)
            {
                return previous;
            }

            return (t) =>
            {
                var tp = t as ITypeParameter;
                if (tp == null) return t;

                if (tp.DeclaringMember == method)
                {
                    if (instantiated.TemplateArguments == null ||
                        instantiated.TemplateArguments.Count <= tp.ParameterListIndex)
                    {
                        return t;
                    }

                    return instantiated.TemplateArguments[tp.ParameterListIndex];
                }

                var decl = method.DeclaringType;
                var idecl = instantiated.DeclaringType;

                while (decl != null)
                {
                    if ((TypeNode)tp.DeclaringMember == decl)
                    {
                        if (idecl.TemplateArguments == null || idecl.TemplateArguments.Count <= tp.ParameterListIndex)
                            return t;

                        return idecl.TemplateArguments[tp.ParameterListIndex];
                    }

                    decl = decl.DeclaringType;
                    idecl = idecl.DeclaringType;
                }

                return previous(t);
            };
        }

        public override void VisitMemberBinding(MemberBinding memberBinding)
        {
            if (memberBinding == null) return;

            if (memberBinding.BoundMember == null) return;

            var declaringType = memberBinding.BoundMember.DeclaringType;

            if (declaringType != null && _delegates.Count > 0 && this.CurrentType != null &&
                HelperMethods.IsClosureType(this.CurrentType, declaringType))
            {
                var parameter = FindNamedParameter(memberBinding.BoundMember);
                if (parameter != null) this.VisitParameter(parameter);
            }
            else
            {
                base.VisitMemberBinding(memberBinding);
            }
        }

        private Parameter FindNamedParameter(Member member)
        {
            if (member == null) return null;

            if (this.CurrentMethod == null) return null; // happens when we look at invariants.

            if (member.Name == null) return null;

            if (member.Name.Name == null) return null;

            var parameters = this.CurrentMethod.Parameters;

            Contract.Assert(member.Name.Name != null);

            if (this.CurrentMethod.HasThis() && member.Name.Name.StartsWith("<>") && member.Name.Name.EndsWith("_this"))
            {
                return this.CurrentMethod.ThisParameter;
            }

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    var par = parameters[i];
                    if (par == null) continue;

                    if (par.Name == null) continue;

                    if (member.Name.UniqueIdKey == par.Name.UniqueIdKey) return par;
                }
            }

            return null;
        }
    }
}