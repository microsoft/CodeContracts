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
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    internal class DuplicatorForContractsAndClosures : Duplicator
    {
        protected Method sourceMethod;
        protected Method targetMethod;
        private Parameter replaceThisWithParameter;

        /// <summary>
        /// Nonnull if the contract is copied from a contract class
        /// </summary>
        private TypeNode contractClass;

        private TypeNode contractClassToForward;
        private TypeNode targetTypeToForwardTo;

        public DuplicatorForContractsAndClosures(Module module, Method sourceMethod, Method targetMethod,
            ContractNodes contractNodes)
            : this(module, sourceMethod, targetMethod, contractNodes, true)
        {
        }

        public DuplicatorForContractsAndClosures(Module module, Method sourceMethod, Method targetMethod,
            ContractNodes contractNodes, bool mapParameters)
            : base(module, targetMethod.DeclaringType)
        {
            this.sourceMethod = sourceMethod;
            this.targetMethod = targetMethod;

            this.RemoveNameForLocals = true;

            Duplicator dup = this;

            if (mapParameters)
            {
                if (sourceMethod.ThisParameter != null)
                {
                    if (targetMethod.ThisParameter != null)
                    {
                        dup.DuplicateFor[sourceMethod.ThisParameter.UniqueKey] = targetMethod.ThisParameter;
                    }
                    else
                    {
                        // target is a static wrapper. But duplicator cannot handle This -> Parameter conversion
                        // so we handle it explicitly here in this visitor.
                        replaceThisWithParameter = targetMethod.Parameters[0];
                    }
                }

                if (sourceMethod.Parameters != null && targetMethod.Parameters != null
                    && sourceMethod.Parameters.Count == targetMethod.Parameters.Count)
                {
                    for (int i = 0, n = sourceMethod.Parameters.Count; i < n; i++)
                    {
                        dup.DuplicateFor[sourceMethod.Parameters[i].UniqueKey] = targetMethod.Parameters[i];
                    }
                }

                // This code makes sure that generic method parameters used by contracts inherited from contract class
                // are correctly replaced by the one defined in the target method.
                // Without this mapping <c>CheckPost</c> method in generated async closure class would contain an invalid
                // reference to a generic contract method parameter instead of generic async closure type parameter.
                // For more about this problem see comments for Microsoft.Contracts.Foxtrot.EmitAsyncClosure.GenericTypeMapper class
                // and issue #380.
                if (sourceMethod.TemplateParameters != null && targetMethod.TemplateParameters != null
                    && sourceMethod.TemplateParameters.Count == targetMethod.TemplateParameters.Count)
                {
                    for (int i = 0, n = sourceMethod.TemplateParameters.Count; i < n; i++)
                    {
                        dup.DuplicateFor[sourceMethod.TemplateParameters[i].UniqueKey] = targetMethod.TemplateParameters[i];
                    }
                }
            }

            var originalType = HelperMethods.IsContractTypeForSomeOtherType(sourceMethod.DeclaringType, contractNodes);
            if (originalType != null)
            {
                var contractType = this.contractClass = sourceMethod.DeclaringType;
                while (contractType.Template != null)
                {
                    contractType = contractType.Template;
                }
                    
                while (originalType.Template != null)
                {
                    originalType = originalType.Template;
                }

                // forward ContractType<A,B> -> originalType<A',B'>
                this.contractClassToForward = contractType;
                this.targetTypeToForwardTo = originalType;

                //dup.DuplicateFor[contractType.UniqueKey] = originalType;
            }
        }

        /// <summary>
        /// If true, all names for duplicated local variables would be removed.
        /// </summary>
        public bool RemoveNameForLocals { get; set; }

        public TypeNode PossiblyRemapContractClassToInterface(TypeNode candidate)
        {
            Contract.Requires(candidate != null);

            var type = HelperMethods.Unspecialize(candidate);
            if (type == this.contractClassToForward)
            {
                if (candidate.TemplateArguments != null)
                {
                    var inst = this.targetTypeToForwardTo.GetTemplateInstance(candidate, candidate.TemplateArguments);
                    return inst;
                }
                    
                return this.targetTypeToForwardTo;
            }

            return candidate;
        }

        public override Expression VisitLocal(Local local)
        {
            var result = base.VisitLocal(local);
                
            var asLoc = result as Local;
            if (asLoc != null && RemoveNameForLocals)
            {
                asLoc.Name = null; // Don't clash with original local name
            }
                
            return result;
        }

        public override Field VisitField(Field field)
        {
            field = base.VisitField(field);
                
            if (field == null) return field;

            field.Type = PossiblyRemapContractClassToInterface(field.Type);
                
            return field;
        }

        public override TypeNode VisitTypeReference(TypeNode type)
        {
            if (type == null) return null;

            var result = (TypeNode) this.DuplicateFor[type.UniqueKey];
                
            if (result != null) return result;
                
            result = base.VisitTypeReference(type);
                
            this.DuplicateFor[result.UniqueKey] = result;
                
            return result;
        }

        public override Member VisitMemberReference(Member member)
        {
            Method m = member as Method;
            if (m != null && m.DeclaringType != null && m.DeclaringType == this.contractClass)
            {
                // Find the corresponding reference in the interface/abstract class
                var implIntf = m.ImplementedInterfaceMethods;
                if (implIntf != null)
                {
                    foreach (var im in implIntf)
                    {
                        return im;
                    }
                }

                var impimplIntf = m.ImplicitlyImplementedInterfaceMethods;
                if (impimplIntf != null)
                {
                    foreach (var im in impimplIntf)
                    {
                        return im;
                    }
                }
            }

            return base.VisitMemberReference(member);
        }

        public override Method VisitMethod(Method method)
        {
            if (method == null) return null;

            method = base.VisitMethod(method);

            method.Template = null;
            return method;
        }

        public override RequiresPlain VisitRequiresPlain(RequiresPlain plain)
        {
            if (plain == null) return null;

            var result = base.VisitRequiresPlain(plain);
                
            // resanitize
            result.UserMessage = ExtractorVisitor.FilterUserMessage(this.targetMethod, result.UserMessage);
                
            return result;
        }

        public override EnsuresNormal VisitEnsuresNormal(EnsuresNormal normal)
        {
            if (normal == null) return null;

            var result = base.VisitEnsuresNormal(normal);
                
            result.UserMessage = ExtractorVisitor.FilterUserMessage(this.targetMethod, result.UserMessage);
                
            return result;
        }

        public override EnsuresExceptional VisitEnsuresExceptional(EnsuresExceptional exceptional)
        {
            if (exceptional == null) return null;

            var result = base.VisitEnsuresExceptional(exceptional);
            result.UserMessage = ExtractorVisitor.FilterUserMessage(this.targetMethod, result.UserMessage);
                
            return result;
        }

        public override Class VisitClass(Class Class)
        {
            Class = base.VisitClass(Class);
                
            Class.Template = null;

            return Class;
        }

        public override Struct VisitStruct(Struct Struct)
        {
            Struct = base.VisitStruct(Struct);

            Struct.Template = null;
                
            return Struct;
        }

        public override Expression VisitThis(This This)
        {
            var original = This;

            var result = base.VisitThis(This);
                
            This = result as This;
                
            if (this.replaceThisWithParameter != null && This != null)
            {
                if (original == sourceMethod.ThisParameter)
                {
                    return this.replaceThisWithParameter;
                }
            }

            return result;
        }

        public override Expression VisitMethodCall(MethodCall call)
        {
            var result = base.VisitMethodCall(call);
                
            // for value type targets we may have to insert a constrained callvirt
            call = result as MethodCall;
            if (call == null) return result; // type changed
                
            MemberBinding memberBinding = call.Callee as MemberBinding;
            if (memberBinding == null) return result;
                
            if (call.NodeType != NodeType.Callvirt)
            {
                // check if we need to turn it into a call virt because we put in an abstract method call
                if (call.NodeType != NodeType.Call) return result;

                Method abstractMethod = memberBinding.BoundMember as Method;
                if (abstractMethod == null || !abstractMethod.IsAbstract) return result;
                    
                call.NodeType = NodeType.Callvirt;
            }

            var target = memberBinding.TargetObject;
            if (target == null) return result;

            if (target is This && this.targetMethod.DeclaringType.IsValueType)
            {
                call.Constraint = this.targetMethod.DeclaringType;
            }
                
            if (target != targetMethod.ThisParameter && target != this.replaceThisWithParameter) return result;

            TypeNode constraint = null;
            if (targetMethod.DeclaringType.IsValueType)
            {
                // it's a virtcall on "this" of a value type
                // add constraint
                constraint = targetMethod.DeclaringType;
            }

            if (constraint == null)
            {
                constraint = IsConstrainedTypeVariable(this.replaceThisWithParameter);
            }
                
            if (constraint != null)
            {
                call.Constraint = constraint;
            }
                
            return call;
        }

        private static TypeNode IsConstrainedTypeVariable(Parameter target)
        {
            if (target == null) return null;

            Reference r = target.Type as Reference;
                
            if (r == null) return null;
                
            if (!(r.ElementType is ITypeParameter)) return null;
                
            return r.ElementType;
        }

        /// <summary>
        /// Have to special case assignment of "this" param into other local or closure this-field when we have call-site wrappers
        /// for constrained virtcalls. In this case, we have to insert a box.
        /// </summary>
        public override Statement VisitAssignmentStatement(AssignmentStatement assignment)
        {
            if (assignment == null) return null;

            var result = base.VisitAssignmentStatement(assignment);
                
            assignment = result as AssignmentStatement;
                
            if (assignment != null && assignment.Target != null && assignment.Target.Type is Interface)
            {
                if (assignment.Source != null & assignment.Source.Type is Reference)
                {
                    var refType = (Reference) assignment.Source.Type;
                    if (refType.ElementType is ITypeParameter)
                    {
                        // found a type mismatch
                        assignment.Source =
                            new BinaryExpression(
                                new AddressDereference(assignment.Source, refType.ElementType),
                                new Literal(refType.ElementType), NodeType.Box);
                    }
                }
            }

            return result;
        }

        public virtual void SafeAddMember(TypeNode targetType, Member duplicatedMember, Member originalMember)
        {
            HelperMethods.SafeAddMember(targetType, duplicatedMember, originalMember);
        }
    }
}