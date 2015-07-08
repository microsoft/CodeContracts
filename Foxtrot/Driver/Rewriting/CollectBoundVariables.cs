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
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    internal sealed class CollectBoundVariables : Inspector
    {
        public readonly List<Parameter> FoundVariables = null;
        public readonly List<Expression> FoundReferences = null;
        private readonly List<Parameter> BoundVars;

        public CollectBoundVariables(List<Parameter> boundVars)
        {
            Contract.Requires(boundVars != null);

            this.FoundVariables = new List<Parameter>(boundVars.Count);
            this.FoundReferences = new List<Expression>(boundVars.Count);
            this.BoundVars = boundVars;
        }

        public override void VisitParameter(Parameter parameter)
        {
            if (parameter == null) return;
            if (this.BoundVars.Contains(parameter) && !this.FoundVariables.Contains(parameter))
            {
                this.FoundVariables.Add(parameter);
                this.FoundReferences.Add(parameter);
            }
        }

        public override void VisitMemberBinding(MemberBinding memberBinding)
        {
            if (memberBinding == null) return;

            if (memberBinding.TargetObject.NodeType == NodeType.This
                || memberBinding.TargetObject.NodeType == NodeType.Local)
            {
                // search in list of parameters to see if any have the same name as the bound member
                foreach (Parameter p in this.BoundVars)
                {
                    if (p.Name.UniqueIdKey == memberBinding.BoundMember.Name.UniqueIdKey)
                    {
                        if (!this.FoundVariables.Contains(p))
                        {
                            this.FoundVariables.Add(p);
                            this.FoundReferences.Add(memberBinding);
                        }
                    }
                }
            }

            base.VisitMemberBinding(memberBinding);
        }
    }
}