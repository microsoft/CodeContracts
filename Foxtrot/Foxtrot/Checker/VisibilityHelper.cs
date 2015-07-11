// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Checks for a minimum level of visibility in a code tree.
    /// </summary>
    [ContractVerification(true)]
    internal class VisibilityHelper : InspectorIncludingClosures
    {
        private Member _memberInErrorFound;

        /// <summary>
        /// The visibility of things is checked to be as visible as this member.
        /// </summary>
        private Member _asThisMember;

        private void ReInitialize(Method asThisMember)
        {
            this.CurrentMethod = asThisMember;
            _memberInErrorFound = null;
            _asThisMember = asThisMember;
        }

        /// <summary>
        /// Checks for less-than-visible member references in an expression.
        /// </summary>
        public override void VisitMemberBinding(MemberBinding binding)
        {
            if (binding == null) return;

            Member mem = binding.BoundMember;
            if (mem != null)
            {
                Field f = mem as Field;
                bool specPublic = false;

                if (f != null)
                {
                    specPublic = IsSpecPublic(f);
                }

                if (!specPublic && !HelperMethods.IsCompilerGenerated(mem))
                {
                    // F: It seems there is some type-state like invariant here justifying why this.AsThisMemeber != null
                    Contract.Assume(_asThisMember != null);

                    if (!HelperMethods.IsReferenceAsVisibleAs(mem, _asThisMember))
                    {
                        _memberInErrorFound = mem;
                        return;
                    }
                }
            }

            base.VisitMemberBinding(binding);
        }

        private static bool IsSpecPublic(Field f)
        {
            // F: 
            Contract.Requires(f != null);

            return f.Attributes.HasAttribute(ContractNodes.SpecPublicAttributeName);
        }

        /// <summary>
        /// Returns true if expr is as visible as method
        /// </summary>
        public bool IsAsVisibleAs(Expression expr, Method method)
        {
            return AsVisibleAs(expr, method) == null;
        }

        /// <summary>
        /// Returns null if okay, otherwise a member not as visible as method
        /// </summary>
        public Member AsVisibleAs(Expression expr, Method method)
        {
            this.ReInitialize(method);
            this.VisitExpression(expr);
            return _memberInErrorFound;
        }
    }
}