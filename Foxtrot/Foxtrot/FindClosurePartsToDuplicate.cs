// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Compiler;
using System.Diagnostics;

namespace Microsoft.Contracts.Foxtrot
{
    internal class FindClosurePartsToDuplicate : InspectorIncludingClosures
    {
        /// <summary>
        /// Type containing the method containing the closure
        /// </summary>
        private TypeNode _containingType;

        internal MemberList MembersToDuplicate = new MemberList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containingType">Type containing the method containing the closures</param>
        public FindClosurePartsToDuplicate(TypeNode containingType, Method sourceMethod)
        {
            this.CurrentMethod = HelperMethods.Unspecialize(sourceMethod);
            _containingType = HelperMethods.Unspecialize(containingType);
        }

        private void TryAddTypeToMembersToDuplicate(TypeNode type)
        {
            TypeNode template = type;
            while (template.Template != null)
            {
                template = template.Template;
            }

            if (!this.MembersToDuplicate.Contains(template))
            {
                this.MembersToDuplicate.Add(template);
            }
        }

        public override void VisitTypeReference(TypeNode type)
        {
            if (type == null) return;

            if (HelperMethods.IsClosureType(_containingType, type))
            {
                TryAddTypeToMembersToDuplicate(type);
            }
            else
            {
                Debug.Assert(!type.Name.Name.Contains("DisplayClass"));
                //Console.WriteLine("Non-closure part: {0}", type.FullName);
            }

            base.VisitTypeReference(type);
        }

        public override void VisitConstruct(Construct cons)
        {
            if ((cons.Type.IsDelegateType()))
            {
                UnaryExpression ue = cons.Operands[1] as UnaryExpression;
                if (ue != null)
                {
                    MemberBinding mb = ue.Operand as MemberBinding;
                    if (mb != null)
                    {
                        Method m = (Method)mb.BoundMember;
                        if (HelperMethods.IsClosureMethod(_containingType, m))
                        {
                            if (HelperMethods.IsClosureType(_containingType, m.DeclaringType))
                            {
                                // Roslyn-based compiler changed non-capturing lambda caching.
                                // Instead of storing delegate in a field like CS$<>9_CachedAnonymousMethodDelegate1
                                // Roslyn-based compiler generates a closure class called <>c with a set of instance
                                // methods and a singleton instance.
                                // This change allows to duplicate this new closure type
                                if (HelperMethods.IsRoslynBasedStaticClosure(m.DeclaringType))
                                {
                                    TryAddTypeToMembersToDuplicate(m.DeclaringType);
                                }
                            }
                            else
                            {
                                // Logic for pre-Roslyn compiler.

                                // then there is no closure class, m is just a method the compiler
                                // added to the class itself

                                // we record the instance here, although we will eventually make a copy of the
                                // template by making the method generic in its parent type type-parameters.
                                MembersToDuplicate.Add(m);
                            }
                        }
                        else
                        {
                            //Console.WriteLine("Not atomic closure part: {0}", m.FullName);
                            var declaringTypeName = m.DeclaringType.Name.Name;
                            var name = m.Name.Name;
                            string message =
                                String.Format(
                                    "DeclaringName should contain 'DisplayClass', <>c or Name should not have '__'. \r\nDeclaringTypeName: {0}, Name: {1}",
                                    declaringTypeName, name);

                            Debug.Assert(
                                declaringTypeName.Contains("DisplayClass") || declaringTypeName.Contains("<>c") ||
                                !name.Contains("__"), message);
                        }
                    }
                }
            }

            base.VisitConstruct(cons);
        }

        public override void VisitAssignmentStatement(AssignmentStatement assignment)
        {
            if (assignment == null) return;

            Construct cons = assignment.Source as Construct;
            if (cons == null) goto JustVisit;

            if (!(cons.Type.IsDelegateType())) goto JustVisit;

            UnaryExpression ue = cons.Operands[1] as UnaryExpression;
            if (ue == null) goto JustVisit;

            MemberBinding mb = ue.Operand as MemberBinding;
            if (mb == null) goto JustVisit;

            Method m = mb.BoundMember as Method;

            if (m.IsStatic)
            {
                mb = assignment.Target as MemberBinding;
                if (mb == null) goto JustVisit;

                if (mb.TargetObject != null) goto JustVisit;

                // Record the static cache field used to hold the static closure
                MembersToDuplicate.Add(mb.BoundMember);

                goto End;
            }

        JustVisit:
            if (assignment.Source == null) goto JustVisit2;

            if (assignment.Source.NodeType != NodeType.Pop) goto JustVisit2;

            mb = assignment.Target as MemberBinding;

            if (mb == null) goto JustVisit2;

            if (mb.TargetObject != null) goto JustVisit2;

            if (mb.BoundMember == null) goto JustVisit2;

            if (HelperMethods.Unspecialize(mb.BoundMember.DeclaringType) != _containingType) goto JustVisit2;

            MembersToDuplicate.Add(mb.BoundMember);

        JustVisit2:
            ;

        End:
            base.VisitAssignmentStatement(assignment);
        }
    }
}