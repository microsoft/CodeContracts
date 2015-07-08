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
    internal sealed class SubstituteParameters : StandardVisitor
    {
        private readonly TrivialHashtable map;
        private readonly List<Parameter> parameters;

        public SubstituteParameters(TrivialHashtable map, List<Parameter> parameters)
        {
            this.map = map;
            this.parameters = parameters;
        }

        public override Expression VisitMemberBinding(MemberBinding memberBinding)
        {
            if (memberBinding.TargetObject != null &&
                (memberBinding.TargetObject.NodeType == NodeType.This
                 || memberBinding.TargetObject.NodeType == NodeType.Local))
            {
                // search in list of parameters to see if any have the same name as the bound member
                foreach (Parameter p in this.parameters)
                {
                    if (p.Name.UniqueIdKey == memberBinding.BoundMember.Name.UniqueIdKey)
                    {
                        return (Expression) this.map[p.UniqueKey];
                    }
                }

                return base.VisitMemberBinding(memberBinding);
            }
            
            return base.VisitMemberBinding(memberBinding);
        }

        public override Expression VisitParameter(Parameter parameter)
        {
            if (parameter == null) return null;

            object result = map[parameter.UniqueKey];
            if (result != null) return (Expression) result;

            return base.VisitParameter(parameter);
        }
    }
}