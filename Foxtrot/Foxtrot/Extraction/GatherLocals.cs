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
        private Local exemptResultLocal;
        public TrivialHashtable Locals = new TrivialHashtable();

        /// <summary>
        /// Use stricter checking if local has a real name (other than "localXXX")
        /// </summary>
        private bool IsLocalExempt(Local local)
        {
            if (local == exemptResultLocal) return true;

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
                exemptResultLocal = (Local) assignment.Target;
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