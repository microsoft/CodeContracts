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

using System.Collections.Generic;
using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// FIXME! Need to do a sanity check that the argument to the Old method
    /// is a member binding (or at least something that is directly there and
    /// not something sitting on the stack as a result of some code sequence
    /// that this class doesn't capture and store in the OldExpressions list).
    internal sealed class SubstituteClosureClassWithinOldExpressions : StandardVisitor
    {
        private readonly Dictionary<TypeNode, Local> closureLocals;

        public SubstituteClosureClassWithinOldExpressions(Dictionary<TypeNode, Local> closureLocals)
        {
            this.closureLocals = closureLocals;
        }

        public override Expression VisitMemberBinding(MemberBinding memberBinding)
        {
            Local closureLocal;
            if (memberBinding.TargetObject != null &&
                this.closureLocals.TryGetValue(memberBinding.TargetObject.Type, out closureLocal))
            {
                return new MemberBinding(closureLocal, memberBinding.BoundMember);
            }
            
            return base.VisitMemberBinding(memberBinding);
        }
    }
}