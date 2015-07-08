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
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    [ContractVerification(true)]
    internal class InspectorIncludingClosures : Inspector
    {
        private Method currentMethod;

        protected Method CurrentMethod
        {
            get { return this.currentMethod; }
            set
            {
                this.currentMethod = value;
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

        private readonly Stack<Method> delegates = new Stack<Method>();
        private readonly Stack<Func<TypeNode, TypeNode>> substitution = new Stack<Func<TypeNode, TypeNode>>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic"),
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(delegates != null);
            Contract.Invariant(substitution != null);
        }

        public override void VisitBlock(Block block)
        {
            Contract.Ensures(this.delegates.Count == Contract.OldValue(this.delegates.Count));

            base.VisitBlock(block);
        }

        public virtual void VisitAnonymousDelegate(Method method, Method instantiated)
        {
            if (method == null) return;

            this.delegates.Push(method);
            this.substitution.Push(Substitution(method, instantiated));
            
            try
            {
                this.VisitBlock(method.Body);
            }
            finally
            {
                this.delegates.Pop();
                this.substitution.Pop();
            }
        }

        protected Func<TypeNode, TypeNode> CurrentSubstitution
        {
            get
            {
                if (this.substitution.Count == 0)
                {
                    return t => t;
                }

                return this.substitution.Peek();
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
                    if ((TypeNode) tp.DeclaringMember == decl)
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
            
            if (declaringType != null && delegates.Count > 0 && this.CurrentType != null &&
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