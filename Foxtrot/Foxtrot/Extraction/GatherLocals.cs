// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Scans code and collects all locals into the hashtable Locals,
    /// which can be accessed after each visit. The hashtable is
    /// static, so it is shared by all instances of this class. That
    /// way it functions as an accumulator. But the hashtable is
    /// public so it can be re-initialized if desired.
    /// </summary>
    internal sealed class GatherLocals : Inspector
    {
        private Local _exemptResultLocal;
        public TrivialHashtable Locals = new TrivialHashtable();

        /// <summary>
        /// Use stricter checking if local has a real name (other than "localXXX")
        /// </summary>
        private bool IsLocalExempt(Local local)
        {
            if (local == _exemptResultLocal) return true;

            bool strict = false;

            if (local.Name != null && local.Name.Name != null && !local.Name.Name.StartsWith("local"))
            {
                strict = true;
            }

            var localType = local.Type;
            if (localType == null) return true;

            return HelperMethods.IsCompilerGenerated(localType)
                   || local.Name.Name == "_preConditionHolds"
                   // introduced by extractor itself for legacy pre-conditions
                   || (strict
                       ? (LocalNameIsExempt(local.Name.Name))
                       : (HelperMethods.IsDelegateType(localType)
                          || HelperMethods.IsTypeParameterType(localType)
                          || localType.IsValueType));
        }

        private static bool LocalNameIsExempt(string localName)
        {
            return localName.StartsWith("CS$") || localName.StartsWith("VB$");
        }

        public override void VisitLocal(Local local)
        {
            // if the local is a reference to a closure class, then
            // don't mark it because it is sure to be used in both
            // the contracts and the method body
            if (!IsLocalExempt(local) && Locals[local.UniqueKey] == null)
            {
                //Console.Write("{0} ", local.Name.Name);
                Locals[local.UniqueKey] = local;
            }

            base.VisitLocal(local);
        }

        public override void VisitAssignmentStatement(AssignmentStatement assignment)
        {
            if (assignment.Target is Local && IsResultExpression(assignment.Source))
            {
                _exemptResultLocal = (Local)assignment.Target;
            }

            base.VisitAssignmentStatement(assignment);
        }

        private static bool IsResultExpression(Expression expression)
        {
            var call = expression as MethodCall;
            if (call == null) return false;

            MemberBinding mb = call.Callee as MemberBinding;
            if (mb == null) return false;

            Method calledMethod = mb.BoundMember as Method;
            if (calledMethod == null) return false;

            Method template = calledMethod.Template;
            if (template == null) return false;

            if (template.Name != null && template.Name.Name == "Result" && template.DeclaringType != null &&
                template.DeclaringType.Name != null && template.DeclaringType.Name.Name == "Contract")
            {
                return true;
            }

            return false;
        }
    }
}