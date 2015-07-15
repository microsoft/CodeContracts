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

using System.Diagnostics; // needed for Debug.Assert (etc.)
using System.Compiler;
using System.Diagnostics.Contracts;
using System;

namespace Microsoft.Contracts.Foxtrot
{
    public static class HelperMethods
    {
        private static Identifier MulticastDelegateId = Identifier.For("MulticastDelegate");
        private static Identifier DelegateId = Identifier.For("Delegate");

        public static TypeNode FindType(AssemblyNode startingPoint, Identifier ns, Identifier name)
        {
            return startingPoint.GetType(ns, name, true);
        }

        public struct ExpressionScanner
        {
            private readonly Action<Field, bool> action;
            private bool inStore;

            public bool IsValid
            {
                get { return this.action != null; }
            }

            public ExpressionScanner(Action<Field, bool> action)
            {
                Contract.Requires(action != null);
                Contract.Ensures(Contract.ValueAtReturn(out this).IsValid);
                this.action = action;
                this.inStore = false;
            }

            internal void Visit(Statement statement)
            {
                Contract.Requires(this.IsValid);

                if (statement == null) return;

                switch (statement.NodeType)
                {
                    case NodeType.Nop:
                    case NodeType.Rethrow:
                    case NodeType.EndFilter:
                    case NodeType.EndFinally:
                        break;

                    case NodeType.Pop:
                        // a pop statement rather than expression
                        return;

                    case NodeType.AssignmentStatement:
                        var astmt = (AssignmentStatement) statement;
                        Visit(astmt.Source);
                        var savedInStore = this.inStore;
                        
                        try
                        {
                            this.inStore = true;
                            Visit(astmt.Target);
                        }
                        finally
                        {
                            this.inStore = savedInStore;
                        }
                        return;

                    case NodeType.ExpressionStatement:
                        var estmt = (ExpressionStatement) statement;
                        if (estmt.Expression.NodeType == NodeType.Dup)
                        {
                            return; // not an expression
                        }
                        
                        if (estmt.Expression.NodeType == NodeType.Pop)
                        {
                            var unary = estmt.Expression as UnaryExpression;
                            if (unary != null && unary.Operand != null)
                            {
                                // we are popping the thing we just push, so the depth does not change.
                                Visit(unary.Operand);
                                return;
                            }

                            // a pop statement rather than expression
                            return;
                        }

                        Visit(estmt.Expression);

                        return;

                    case NodeType.Block:
                        var block = (Block) statement;
                        Visit(block.Statements);
                        return;

                    case NodeType.SwitchInstruction:
                        var sw = (SwitchInstruction) statement;
                        Visit(sw.Expression);
                        return;

                    case NodeType.Throw:
                        var th = (Throw) statement;
                        Visit(th.Expression);
                        return;

                    case NodeType.Return:
                        var ret = (Return) statement;
                        Visit(ret.Expression);
                        return;

                    default:
                        var br = statement as Branch;
                        if (br != null)
                        {
                            Visit(br.Condition);
                            return;
                        }

                        throw new NotImplementedException(string.Format("Expression scanner: {0}", statement.NodeType));
                }
            }

            public void Visit(StatementList statementList)
            {
                if (statementList == null) return;
                for (int i = 0; i < statementList.Count; i++)
                {
                    Visit(statementList[i]);
                }
            }

            private void Visit(Expression expression)
            {
                if (expression == null) return;
                switch (expression.NodeType)
                {
                    case NodeType.MemberBinding:
                        var mb = (MemberBinding) expression;
                        Visit(mb.TargetObject);
                        var field = mb.BoundMember as Field;
                        if (field != null)
                        {
                            action(field, this.inStore);
                        }
                        return;

                    case NodeType.This:
                    case NodeType.Parameter:
                    case NodeType.Local:
                        break;

                    case NodeType.Pop:
                        break;

                    case NodeType.Literal:
                    case NodeType.Arglist:
                        break;

                    case NodeType.AddressDereference:
                        var ad = (AddressDereference) expression;
                        Visit(ad.Address);
                        return;

                    case NodeType.Construct:
                        var c = (Construct) expression;
                        Visit(c.Operands);
                        return;

                    case NodeType.ConstructArray:
                        var ca = (ConstructArray) expression;
                        Visit(ca.Operands);
                        return;

                    case NodeType.Call:
                    case NodeType.Callvirt:
                        var call = (MethodCall) expression;
                        Visit(call.Callee);
                        Visit(call.Operands);
                        return;

                    case NodeType.Cpblk:
                    case NodeType.Initblk:
                        var tern = (TernaryExpression) expression;
                        Visit(tern.Operand1);
                        Visit(tern.Operand2);
                        Visit(tern.Operand3);
                        return;

                    case NodeType.Indexer:
                        var indexer = (Indexer) expression;
                        Visit(indexer.Object);
                        Visit(indexer.Operands);
                        return;

                    default:
                        var binary = expression as BinaryExpression;
                        if (binary != null)
                        {
                            Visit(binary.Operand1);
                            Visit(binary.Operand2);
                            
                            return;
                        }

                        var unary = expression as UnaryExpression;
                        if (unary != null)
                        {
                            Visit(unary.Operand);
                            
                            return;
                        }

                        throw new NotImplementedException(string.Format("Expression scanner: {0}", expression.NodeType));
                }
            }

            public void Visit(ExpressionList expressionList)
            {
                if (expressionList == null) return;
                for (int i = 0; i < expressionList.Count; i++)
                {
                    Visit(expressionList[i]);
                }
            }
        }

        /// <summary>
        /// Because we load non-standard mscorlibs (and multiple ones at that, think mscorlib.contracts.dll), we need to check this
        /// more robustly than just DelegateNode
        /// </summary>
        public static bool IsDelegateType(this TypeNode type)
        {
            if (type == null) return false;

            if (type.Template != null) type = type.Template;
            if (type is DelegateNode) return true;

            if (type.BaseType != null)
            {
                if (type.BaseType.Name.Matches(MulticastDelegateId))
                {
                    return true;
                }

                if (type.BaseType.Name.Matches(DelegateId))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if the member is declared in the static closure class generated by Roslyn-based compiler.
        /// </summary>
        /// <remarks>
        /// Roslyn compiler changed the way how static closures are implemented. This method helps to understand that 
        /// current type is a new static closure type and deal with it accordingly.
        /// </remarks>
        public static bool IsRoslynBasedStaticClosure(this Member member)
        {
            return
                (member != null && member.Name != null && member.Name.Name != null) &&
                (member.Name.Name.Contains("<>c") && !member.Name.Name.Contains("DisplayClass"));
        }

        public static bool HasBaseClass(TypeNode type)
        {
            Class c = type as Class;
            if (c != null && c.BaseClass != null) return true;

            return false;
        }

        public static TypeNode BaseClass(TypeNode type)
        {
            Class c = (Class) type;
            return (c.BaseClass);
        }

        private static bool IsInheritedFrom(TypeNode t, TypeNode tfrom)
        {
            if (HasBaseClass(t))
            {
                TypeNode basecl = BaseClass(t);
                
                while (basecl.Template != null) basecl = basecl.Template;
                
                if (basecl == tfrom)
                    return true;
                
                if (IsInheritedFrom(basecl, tfrom))
                    return true;
            }

            return false;
        }

        [Pure]
        public static bool IsVisibleFrom(TypeNode t, TypeNode tfrom)
        {
            Contract.Assert(TypeNode.IsCompleteTemplate(tfrom));
            if (t.Template != null)
            {
                for (int i = 0; i < t.TemplateArguments.Count; i++)
                {
                    if (!IsVisibleFrom(t.TemplateArguments[i], tfrom)) return false;
                }

                t = Unspecialize(t);
            }

            var declaringType = t.DeclaringType;
            if (declaringType != null)
            {
                // declaring type could be specialized
                if (declaringType.Template != null)
                {
                    for (int i = 0; i < declaringType.TemplateArguments.Count; i++)
                    {
                        if (!IsVisibleFrom(declaringType.TemplateArguments[i], tfrom)) return false;
                    }

                    declaringType = Unspecialize(declaringType);
                }

                // reason like for member
                if (IsContainedIn(tfrom, declaringType)) return true;

                if (IsInheritedFrom(tfrom, t)) return true;

                if (t.IsNestedFamily)
                {
                    if (IsInheritedFrom(tfrom, declaringType)) return true;

                    return false;
                }

                if (t.IsNestedPublic)
                {
                    return IsVisibleFrom(declaringType, tfrom);
                }

                if (t.IsNestedInternal)
                {
                    // fam or assembly
                    if (IsInheritedFrom(tfrom, declaringType)) return true;

                    if (declaringType.DeclaringModule == tfrom.DeclaringModule)
                    {
                        return IsVisibleFrom(declaringType, tfrom);
                    }

                    return false;
                }

                if (t.IsNestedFamilyAndAssembly)
                {
                    if (tfrom.DeclaringModule == declaringType.DeclaringModule && IsInheritedFrom(tfrom, declaringType))
                        return true;

                    return false;
                }

                if (t.IsAssembly || t.IsNestedAssembly)
                {
                    if (declaringType.DeclaringModule == tfrom.DeclaringModule)
                    {
                        return IsVisibleFrom(declaringType, tfrom);
                    }

                    return false;
                }

                return false;
            }

            // toplevel type
            if (t.DeclaringModule == tfrom.DeclaringModule) return true;
            
            if (t.IsPublic) return true;
            
            return false;
        }

        public static bool IsContainedIn(TypeNode inner, TypeNode outer)
        {
            inner = Unspecialize(inner);
            if (inner == outer) return true;

            if (inner.DeclaringType != null) return IsContainedIn(inner.DeclaringType, outer);
            
            return false;
        }

        [Pure]
        public static bool IsVisibleFrom(Member member, TypeNode tfrom)
        {
            var type = member as TypeNode;

            if (type != null) return IsVisibleFrom(type, tfrom);

            TypeNode t = member.DeclaringType;
            Method method = member as Method;
            
            if (method != null && method.Template != null)
            {
                if (method.TemplateArguments != null)
                {
                    for (int i = 0; i < method.TemplateArguments.Count; i++)
                    {
                        if (!IsVisibleFrom(method.TemplateArguments[i], tfrom)) return false;
                    }
                }

                member = Unspecialize(method);
            }

            // declaring type could be specialized
            if (t.Template != null)
            {
                for (int i = 0; i < t.TemplateArguments.Count; i++)
                {
                    if (!IsVisibleFrom(t.TemplateArguments[i], tfrom)) return false;
                }
                
                t = Unspecialize(t);
            }

            if (IsContainedIn(tfrom, t))
                return true;

            if (member.IsPublic)
            {
                return IsVisibleFrom(t, tfrom);
            }

            if (member.IsFamily)
            {
                if (IsInheritedFrom(tfrom, t)) return true;
                
                return false;
            }

            if (member.IsFamilyAndAssembly)
            {
                if (tfrom.DeclaringModule == t.DeclaringModule && IsInheritedFrom(tfrom, t)) return true;
                
                return false;
            }

            if (member.IsFamilyOrAssembly)
            {
                if (IsInheritedFrom(tfrom, t)) return true;

                if (t.DeclaringModule == tfrom.DeclaringModule)
                {
                    return IsVisibleFrom(t, tfrom);
                }

                return false;
            }

            if (member.IsAssembly)
            {
                if (t.DeclaringModule == tfrom.DeclaringModule)
                {
                    return IsVisibleFrom(t, tfrom);
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Algorithm as provided by Eric Lippert
        /// Returns true if any context that can see m2 can also see m1.
        /// </summary>
        /// <returns></returns>
        //foreach (Symbol s1 in T.TypeAndParentTypes where s1.access != public)
        //            asRestrictive = false
        //            foreach(Symbol s2 in S.SymbolAndParentSymbols)
        //                        if s1.access == internal
        //                           if s2.access == private or s2.access == internal
        //                                if s1 and s2 are in the same assembly or are in friend assemblies
        //                                   asRestrictive = true
        //                        if s1.access == protected
        //                                    if s2.access == private
        //                                                if s2 is inside s1 or s2 is within a subclass of s1.parent
        //                                                            asRestrictive = true
        //                                    if s2.access == protected
        //                                                if s2.parent is identical to s1.parent or s2.parent is a subclass of s1.parent
        //                                                            asRestrictive = true
        //                        if s1.access == internalprotected
        //                                    if (s2.access == private)
        //                                                if s2 is within a subclass of s1.parent, or s1 and s2 are in the same assembly, or s1 and s2 are in friend assemblies
        //                                                            asRestrictive = true
        //                                    if s2.access == internal
        //                                                if s1 and s2 are in the same assembly or are in friend assemblies 
        //                                                            asRestrictive = true
        //                                    if s2.access == protected
        //                                                if s2.parent is s1.parent or a subclass of s1.parent
        //                                                            asRestrictive = true
        //                                    if s2.access == internalprotected
        //                                                if s1 and s2 are in the same assembly or friend assemblies, AND s2.parent is s1.parent or s2.parent is a subclass of s1.parent
        //                                                            asRestrictive = true
        //                        if s1.access == private
        //                                    if s2.access == private
        //                                                if s2 is inside s1.parent
        //                                                            asRestrictive = true
        //            if !asRestrictive
        //                        return false
        //return true
        [Pure]
        [ContractVerification(true)]
        public static bool IsDefinitionAsVisibleAs(Member member, Member asThisMember)
        {
            Contract.Requires(member != null);
            Contract.Requires(asThisMember != null);
            Contract.Requires(IsAtomic(member));

            Module a1 = AssemblyOf(member);
            Module a2 = AssemblyOf(asThisMember);

            for (Member s1 = member; s1 != null; s1 = s1.DeclaringType)
            {
                if (s1.IsPublic) continue;
                
                bool asRestrictive = false;
                for (Member s2 = asThisMember; s2 != null; s2 = s2.DeclaringType)
                {
                    if (s1.IsAssembly)
                    {
                        if (s2.IsPrivate || s2.IsAssembly)
                        {
                            if (a1 == a2) asRestrictive = true;
                        }
                    }
                    else if (s1.IsFamily)
                    {
                        if (s2.IsPrivate)
                        {
                            if (IsInsideOf(s2, s1) || IsInsideSubclass(s2, s1.DeclaringType))
                            {
                                asRestrictive = true;
                            }
                        }
                        else if (s2.IsFamily)
                        {
                            Contract.Assume(s2.DeclaringType != null,
                                "if a member is Familiy visible it must have a declaring type");

                            if (s2.DeclaringType == s1.DeclaringType || IsSubclassOf(s2.DeclaringType, s1.DeclaringType))
                            {
                                asRestrictive = true;
                            }
                        }
                    }
                    else if (s1.IsFamilyOrAssembly)
                    {
                        if (s2.IsPrivate)
                        {
                            if (a1 == a2 || IsInsideSubclass(s2, s1.DeclaringType))
                            {
                                asRestrictive = true;
                            }
                        }
                        else if (s2.IsAssembly)
                        {
                            if (a1 == a2)
                            {
                                asRestrictive = true;
                            }
                        }
                        else if (s2.IsFamily)
                        {
                            Contract.Assume(s2.DeclaringType != null,
                                "if a member is Familiy visible it must have a declaring type");

                            if (IsSubclassOf(s2.DeclaringType, s1.DeclaringType))
                            {
                                asRestrictive = true;
                            }
                        }
                        else if (s2.IsFamilyOrAssembly)
                        {
                            Contract.Assume(s2.DeclaringType != null,
                                "if a member is Familiy visible it must have a declaring type");

                            if (a1 == a2 && IsSubclassOf(s2.DeclaringType, s1.DeclaringType))
                            {
                                asRestrictive = true;
                            }
                        }
                    }
                    else if (s1.IsPrivate)
                    {
                        if (s2.IsPrivate)
                        {
                            if (IsInsideOf(s2, s1.DeclaringType))
                            {
                                asRestrictive = true;
                            }
                        }
                    }
                }

                if (!asRestrictive) return false;
            }

            return true;
        }

        [ContractVerification(true)]
        [Pure]
        public static bool IsAtomic(Member member)
        {
            if (member == null) return true;

            TypeNode type = member as TypeNode;
            if (type != null)
            {
                if (type.Template != null) return false;

                if (type is Class || type is Interface || type is Struct || type is EnumNode || type is DelegateNode)
                    return true;
                
                return false;
            }

            Method method = member as Method;
            if (method != null)
            {
                if (method.Template != null) return false;
            }

            return IsAtomic(member.DeclaringType);
        }

        [ContractVerification(true)]
        [Pure]
        public static bool IsReferenceAsVisibleAs(Member member, Member asThisMember)
        {
            Contract.Requires(member != null);
            Contract.Requires(asThisMember != null);

            var type = member as TypeNode;
            if (type != null) return IsTypeAsVisibleAs(type, asThisMember);

            Member unspecialized;
            var method = member as Method;
            if (method != null)
            {
                if (method.TemplateArguments != null)
                {
                    for (int i = 0; i < method.TemplateArguments.Count; i++)
                    {
                        if (!IsTypeAsVisibleAs(method.TemplateArguments[i], asThisMember)) return false;
                    }
                }

                while (method.Template != null)
                {
                    method = method.Template;
                }
                
                unspecialized = method;
            }
            else
            {
                unspecialized = Unspecialize(member);
            }

            var declaringType = member.DeclaringType;
            
            Contract.Assume(declaringType != null, "non type member must have declaring type");
            
            if (declaringType.ConsolidatedTemplateArguments != null)
            {
                for (int i = 0; i < declaringType.ConsolidatedTemplateArguments.Count; i++)
                {
                    if (!IsTypeAsVisibleAs(declaringType.ConsolidatedTemplateArguments[i], asThisMember)) return false;
                }
            }
            
            Contract.Assume(IsAtomic(unspecialized));
            
            return IsDefinitionAsVisibleAs(unspecialized, asThisMember);
        }

        [ContractVerification(true)]
        public static bool IsTypeAsVisibleAs(TypeNode type, Member asThisMember)
        {
            Contract.Requires(asThisMember != null);

            if (type == null) return true;

            switch (type.NodeType)
            {
                case NodeType.ArrayType:
                    ArrayType arrType = (ArrayType) type;
                    return IsTypeAsVisibleAs(arrType.ElementType, asThisMember);

                case NodeType.ClassParameter:
                case NodeType.TypeParameter:
                    return true;
                
                case NodeType.Pointer:
                    Pointer pType = (Pointer) type;
                    return IsTypeAsVisibleAs(pType.ElementType, asThisMember);
                
                case NodeType.Reference:
                    Reference rType = (Reference) type;
                    return IsTypeAsVisibleAs(rType.ElementType, asThisMember);
                
                case NodeType.FunctionPointer:
                    FunctionPointer funcPointer = (FunctionPointer) type;
                    if (funcPointer.ParameterTypes != null)
                    {
                        for (int i = 0; i < funcPointer.ParameterTypes.Count; i++)
                        {
                            if (!IsTypeAsVisibleAs(funcPointer.ParameterTypes[i], asThisMember)) return false;
                        }
                    }

                    return IsTypeAsVisibleAs(funcPointer.ReturnType, asThisMember);
                
                case NodeType.OptionalModifier:
                case NodeType.RequiredModifier:
                    TypeModifier modType = (TypeModifier) type;
                    
                    if (!IsTypeAsVisibleAs(modType.ModifiedType, asThisMember)) return false;
                    
                    return IsTypeAsVisibleAs(modType.Modifier, asThisMember);
                
                default:
                    if (type.Template != null)
                    {
                        Contract.Assume(IsAtomic(type.Template));
                        if (!IsDefinitionAsVisibleAs(type.Template, asThisMember)) return false;
                        if (type.ConsolidatedTemplateArguments != null)
                        {
                            for (int i = 0; i < type.ConsolidatedTemplateArguments.Count; i++)
                            {
                                if (!IsTypeAsVisibleAs(type.ConsolidatedTemplateArguments[i], asThisMember))
                                    return false;
                            }
                        }

                        return true;
                    }
                    
                    // atomic type
                    Contract.Assume(IsAtomic(type));
                    
                    return IsDefinitionAsVisibleAs((Member) type, asThisMember);
            }
        }

        [Pure]
        [ContractVerification(true)]
        private static bool IsInsideOf(Member m1, Member m2)
        {
            Contract.Requires(m1 != null);

            TypeNode t2 = m2 as TypeNode;
            if (t2 == null) return false;
            
            TypeNode t1 = m1.DeclaringType;
            while (t1 != null)
            {
                if (t1 == t2) return true;
                
                t1 = t1.DeclaringType;
            }
            
            return false;
        }

        [Pure]
        [ContractVerification(true)]
        private static bool IsInsideSubclass(Member m1, Member m2)
        {
            Contract.Requires(m1 != null);

            TypeNode t2 = m2 as TypeNode;
            if (t2 == null) return false;

            TypeNode t1 = m1.DeclaringType;
            while (t1 != null)
            {
                if (t1.IsAssignableTo(t2)) return true;
                
                t1 = t1.DeclaringType;
            }
            
            return false;
        }

        [Pure]
        [ContractVerification(true)]
        private static bool IsSubclassOf(TypeNode t1, TypeNode t2)
        {
            Contract.Requires(t1 != null);

            if (t2 == null) return false;

            return t1.IsAssignableTo(t2);
        }

        [Pure]
        [ContractVerification(true)]
        public static Module AssemblyOf(Member m)
        {
            Contract.Requires(m != null);

            TypeNode t = m as TypeNode;
            if (t == null)
            {
                t = m.DeclaringType;
                Contract.Assume(t != null, "non-types must have a declaring type");
            }

            while (t.DeclaringType != null)
            {
                t = t.DeclaringType;
            }

            return t.DeclaringModule;
        }

        [Pure]
        public static bool HasThis(this Method method)
        {
            if (method == null) return false;

            return !method.IsStatic || (method is InstanceInitializer);
        }

        [Pure]
        public static bool HasAttribute(this AttributeList attributes, Identifier typeName)
        {
            if (attributes == null) return false;

            for (int i = 0; i < attributes.Count; i++)
            {
                var attr = attributes[i];
                
                if (attr == null) continue;
                
                if (attr.Type == null) continue;
                
                if (typeName.Matches(attr.Type.Name)) return true;
            }

            return false;
        }

        [Pure]
        public static bool DoesInheritContracts(Method method)
        {
            switch (ContractOption(method.Attributes, "Contract", "Inheritance"))
            {
                case ThreeValued.True:
                    return true;
                case ThreeValued.False:
                    return false;
                default:
                    return DoesInheritContracts(method.DeclaringType);
            }
        }

        [Pure]
        public static bool DoesInheritContracts(TypeNode type)
        {
            switch (ContractOption(type.Attributes, "Contract", "Inheritance"))
            {
                case ThreeValued.True:
                    return true;
                case ThreeValued.False:
                    return false;
                default:
                    if (type.DeclaringType != null)
                    {
                        return DoesInheritContracts(type.DeclaringType);
                    }
                    
                    return ContractOption(type.DeclaringModule, "Contract", "Inheritance");
            }
        }

        /// <summary>
        /// Returns the value of the [ContractOption] attribute. Inherited from containing type
        /// (or module if no containing type).
        /// </summary>
        /// <param name="method">
        /// The method to start the search at.
        /// </param>
        /// <param name="category">
        /// The first argument of the ContractOption attribute.
        /// E.g., "contract" or "runtime". (Case-insensitive)
        /// </param>
        /// <param name="setting">
        /// The second argument of the ContractOption attribute.
        /// E.g., "inheritance" or "checking". (Case-insensitive)
        /// </param>
        /// <returns>
        /// True if not found or found and third argument is "true". Otherwise "false".
        /// </returns>
        [Pure]
        public static bool ContractOption(Method method, string category, string setting)
        {
            switch (ContractOption(method.Attributes, category, setting))
            {
                case ThreeValued.True:
                    return true;
                case ThreeValued.False:
                    return false;
                default:
                    return ContractOption(method.DeclaringType, category, setting);
            }
        }

        /// <summary>
        /// Returns the value of the [ContractOption] attribute. Inherited from containing type
        /// (or module if no containing type).
        /// </summary>
        /// <param name="type">
        /// The type to start the search at.
        /// </param>
        /// <param name="category">
        /// The first argument of the ContractOption attribute.
        /// E.g., "contract" or "runtime". (Case-insensitive)
        /// </param>
        /// <param name="setting">
        /// The second argument of the ContractOption attribute.
        /// E.g., "inheritance" or "checking". (Case-insensitive)
        /// </param>
        /// <returns>
        /// True if not found or found and third argument is "true". Otherwise "false".
        /// </returns>
        [Pure]
        public static bool ContractOption(TypeNode type, string category, string setting)
        {
            switch (ContractOption(type.Attributes, category, setting))
            {
                case ThreeValued.True:
                    return true;
                case ThreeValued.False:
                    return false;
                default:
                    if (type.DeclaringType != null)
                    {
                        return ContractOption(type.DeclaringType, category, setting);
                    }
                    
                    return ContractOption(type.DeclaringModule, category, setting);
            }
        }

        /// <summary>
        /// Returns the value of the [ContractOption] attribute. 
        /// </summary>
        /// <param name="module">
        /// The module containing the attributes to search.
        /// </param>
        /// <param name="category">
        /// The first argument of the ContractOption attribute.
        /// E.g., "contract" or "runtime". (Case-insensitive)
        /// </param>
        /// <param name="setting">
        /// The second argument of the ContractOption attribute.
        /// E.g., "inheritance" or "checking". (Case-insensitive)
        /// </param>
        /// <returns>
        /// True if not found or found and third argument is "true". Otherwise "false".
        /// </returns>
        [Pure]
        public static bool ContractOption(Module module, string category, string setting)
        {
            if (module == null) return true; // default

            if (module.ContainingAssembly == null) return true; // default
            
            var tv = ContractOption(module.ContainingAssembly.Attributes, category, setting);
            
            switch (tv)
            {
                case ThreeValued.False:
                    return false;
                default:
                    return true;
            }
        }

        public enum ThreeValued
        {
            False,
            True,
            Maybe
        }

        [Pure]
        public static ThreeValued ContractOption(AttributeNode attr, string category, string setting)
        {
            string c, s;
            bool value;

            if (IsContractOptionAttribute(attr, out c, out s, out value))
            {
                if (category.Equals(c, StringComparison.OrdinalIgnoreCase))
                {
                    if (setting.Equals(s, StringComparison.OrdinalIgnoreCase))
                    {
                        if (value) return ThreeValued.True;

                        return ThreeValued.False;
                    }
                }
            }

            return ThreeValued.Maybe;
        }

        [Pure]
        public static ThreeValued ContractOption(AttributeList attributes, string category, string setting)
        {
            if (attributes == null) return ThreeValued.Maybe;

            for (int i = 0; i < attributes.Count; i++)
            {
                var tv = ContractOption(attributes[i], category, setting);
                if (tv != ThreeValued.Maybe) return tv;
            }

            return ThreeValued.Maybe;
        }

        [Pure]
        public static ThreeValued IsMutableHeapIndependent(AttributeNode attr)
        {
            string category, setting;
            bool value;

            if (IsContractOptionAttribute(attr, out category, out setting, out value))
            {
                if (string.Compare("reads", category, true) == 0)
                {
                    if (string.Compare("mutable", setting, true) == 0)
                    {
                        if (!value) return ThreeValued.True;
                        
                        return ThreeValued.False;
                    }
                }
            }

            return ThreeValued.Maybe;
        }

        [Pure]
        public static bool IsContractOptionAttribute(AttributeNode attr, out string category, out string setting,
            out bool toggle)
        {
            category = null;
            setting = null;
            toggle = false;

            if (attr == null)
            {
                return false;
            }
            
            if (attr.Type == null) return false;
            
            if (!ContractNodes.ContractOptionAttributeName.Matches(attr.Type.Name)) return false;
            
            if (attr.Expressions == null) return false;
            
            if (attr.Expressions.Count != 3) return false;
            
            if (!AsLiteralString(attr.Expressions[0], out category)) return false;
            
            if (!AsLiteralString(attr.Expressions[1], out setting)) return false;
            
            if (!AsLiteralBool(attr.Expressions[2], out toggle)) return false;
            
            return true;
        }

        [Pure]
        private static bool AsLiteralString(Expression exp, out string value)
        {
            value = null;
            var lit = exp as Literal;
            
            if (lit == null) return false;
            
            value = lit.Value as string;
            
            return value != null;
        }

        [Pure]
        public static bool AsLiteralBool(Expression exp, out bool value)
        {
            value = false;
            var lit = exp as Literal;
            if (lit == null) return false;
            
            if (!(lit.Value is bool)) return false;
            
            value = (bool) lit.Value;
            return true;
        }

        [Pure]
        public static bool Matches(this Identifier id1, Identifier id2)
        {
            if (id1 == null || id2 == null) return false;

            return id1.UniqueIdKey == id2.UniqueIdKey;
        }

        [Pure]
        public static bool MatchesContractByName(this Member member, Identifier name)
        {
            if (member == null) return false;

            Method method = member as Method;
            if (method != null)
            {
                if (method.Template != null)
                {
                    method = method.Template;
                }
                
                if (!method.Name.Matches(name)) return false;
                
                if (method.DeclaringType == null) return false;
                
                if (!method.DeclaringType.Name.Matches(ContractNodes.ContractClassName)) return false;
                
                if (!method.DeclaringType.Namespace.Matches(ContractNodes.ContractNamespace)) return false;
                
                return true;
            }

            TypeNode type = member as TypeNode;
            if (type != null)
            {
                if (type.Template != null)
                {
                    type = type.Template;
                }

                if (!type.Name.Matches(name)) return false;
                
                if (!type.Namespace.Matches(ContractNodes.ContractNamespace)) return false;
                
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find the method in t that implements/overrides m
        /// </summary>
        /// <returns>returns m if we can't find m' in t matching m</returns>
        [ContractVerification(true)]
        [Pure]
        public static Method FindMatchingMethod(TypeNode t, Method m)
        {
            Contract.Requires(m != null);
            Contract.Requires(t != null);
            Contract.Ensures(Contract.Result<Method>() != null);

            var members = t.Members;
            for (int i = 0; i < members.Count; i++)
            {
                var method = members[i] as Method;
                if (method == null) continue;

                if (method.OverriddenMethod != null && method.OverriddenMethod == m) return method;

                var explicitInterfaces = method.ImplementedInterfaceMethods;
                if (explicitInterfaces != null)
                {
                    for (int j = 0; j < explicitInterfaces.Count; j++)
                    {
                        Method ifm = Unspecialize(explicitInterfaces[j]);

                        if (ifm == m) return method;
                    }
                }

                var implicitInterfaces = method.ImplicitlyImplementedInterfaceMethods;
                if (implicitInterfaces != null)
                {
                    for (int j = 0; j < implicitInterfaces.Count; j++)
                    {
                        Method ifm = Unspecialize(implicitInterfaces[j]);

                        if (ifm == m) return method;
                    }
                }
            }

            return m;
        }

        /// <summary>
        /// Find the method in t that m implements/overrides
        /// </summary>
        /// <returns>returns null if we can't find it</returns>
        [ContractVerification(true)]
        [Pure]
        public static Method FindImplementedMethodSpecialized(TypeNode inType, Method m)
        {
            Contract.Requires(m != null);
            Contract.Requires(inType != null);

            var overridden = m.OverriddenMethod;
            if (overridden != null)
            {
                Contract.Assume(overridden.DeclaringType != null, "non-types always have declaring types");
                
                var dt = Unspecialize(overridden.DeclaringType);
                
                if (dt == Unspecialize(inType)) return overridden;
            }

            var result = FindImplementedMethod(m.ImplementedInterfaceMethods, inType);
            if (result != null) return result;

            result = FindImplementedMethod(m.ShallowImplicitlyImplementedInterfaceMethods, inType);
            
            if (result != null) return result;
            
            return null;
        }

        [Pure]
        public static Method FindImplementedMethodUnspecialized(TypeNode t, Method m)
        {
            Contract.Ensures(m == null || Contract.Result<Method>() != null);

            return Unspecialize(FindImplementedMethodSpecialized(t, m));
        }

        [Pure]
        private static Method FindImplementedMethod(MethodList candidates, TypeNode targetTypeInstance)
        {
            if (candidates != null)
            {
                for (int i = 0; i < candidates.Count; i++)
                {
                    var candidate = candidates[i];
                    if (candidate == null) continue;

                    if (targetTypeInstance.IsAssignableTo(candidate.DeclaringType)) return candidate;
                }
            }

            return null;
        }

        [Pure]
        internal static SourceContext SourceContextOfMethod(Method method)
        {
            SourceContext sourceContext;
            if (method.Body == null)
            {
                sourceContext = method.SourceContext;
            }
            else
            {
                HelperMethods.FindContext(method.Body, method.Body.SourceContext, out sourceContext);
            }

            return sourceContext;
        }

        internal static bool FindContext(Block currentClump, SourceContext initialSourceContext, out SourceContext sctx)
        {
            sctx = initialSourceContext;

            if (currentClump == null || currentClump.Statements == null || currentClump.Statements.Count <= 0)
                return false;

            for (int i = 0, n = currentClump.Statements.Count; i < n; i++)
            {
                Block b = currentClump.Statements[i] as Block;
                
                if (b == null || b.Statements == null || b.Statements.Count <= 0) continue;
                
                for (int j = 0, m = b.Statements.Count; j < m; j++)
                {
                    Statement s = b.Statements[j];
                    if (s == null) continue;

                    if (s.NodeType != NodeType.Nop
                        // these are the source contexts the compiler puts in that correspond to things like method declarations, etc.
                        && s.SourceContext.IsValid)
                    {
                        sctx = s.SourceContext;
                        return true;
                    }
                }
            }

            return false;
        }

        internal static int FindNextRealStatement(StatementList sl, int beginningIndex)
        {
            if (sl == null || sl.Count <= beginningIndex) return -1;

            int i = beginningIndex;
            
            while (i < sl.Count && (sl[i] == null || sl[i].NodeType == NodeType.Nop)) i++;
            
            return i;
        }

        /// <summary>
        /// Analyzes a statement to see if it is a call to a method.
        /// </summary>
        /// <param name="s">Any statement.</param>
        /// <returns>The method m if the statement is an expression statement that is a method call to m or null</returns>
        internal static Method IsMethodCall(Statement s)
        {
            if (s == null) return null;

            ExpressionStatement es = s as ExpressionStatement;
            
            if (es == null) return null;
            
            // If the expression statement represents a method call, then it can
            // have two different values as its expression:
            // 0. if the method's return type is void, then the expression is just the
            //    method call.
            // 1. if the method's return type is not void, then the value of the method
            //    isn't needed and it is wrapped inside of a unary expression,
            //    Pop(method call(...)) in order to throw away the return value
            //
            // We want the method call only if its return type is void, so just look
            // for pattern 0.
            //
            MethodCall mc = es.Expression as MethodCall;
            if (mc == null) return null;

            MemberBinding mb = mc.Callee as MemberBinding;
            if (mb == null) return null;
            
            return mb.BoundMember as Method;
        }

        /// <summary>
        /// Used to determine if block does not contain any nested blocks.
        /// </summary>
        /// <param name="b">Any block</param>
        /// <returns>b != null ==> ForAll{Statement s in b.Statements; s == null || typeof(s) &lt;: Block}
        /// </returns>
        public static bool IsBasicBlock(Block /*?*/ b)
        {
            if (b == null) return true;

            for (int i = 0, n = b.Statements.Count; i < n; i++)
            {
                // no nested blocks except for assume blocks we add in the middle of MoveNext methods
                if (b.Statements[i] is Block && !(b.Statements[i] is AssumeBlock)) return false;
            }
            
            return true;
        }

        /// <summary>
        /// Use to determine if a statement list is a "clump"
        /// </summary>
        /// <param name="sl">Any statement list</param>
        /// <returns>sl != null ==> ForAll{Statement s in sl; IsBasicBlock(s)}</returns>
        internal static bool IsClump(StatementList /*?*/ sl)
        {
            if (sl == null || sl.Count == 0) return true;
            
            for (int i = 0, n = sl.Count; i < n; i++)
            {
                if (!IsBasicBlock(sl[i] as Block)) return false;
            }

            return true;
        }

        /// <summary>
        /// requires IsClump(stmts);
        /// </summary>
        /// <param name="stmts">StatementList representing a clump</param>
        /// <returns>true iff stmts is a clump and all branches in stmts are to blocks in stmts</returns>
        internal static bool ClumpIsClosed(StatementList /*?*/ stmts)
        {
            if (stmts == null) return true;

            if (!IsClump(stmts)) return false;
            
            TrivialHashtable blocks = new TrivialHashtable(stmts.Count); // can't be any bigger!
            
            for (int i = 0, n = stmts.Count; i < n; i++)
            {
                if (stmts[i] == null) continue;

                blocks[stmts[i].UniqueKey] = stmts[i];
            }

            for (int i = 0, n = stmts.Count; i < n; i++)
            {
                Block b = stmts[i] as Block;
                
                if (b == null) continue; // tolerate nulls
                if (b.Statements == null || b.Statements.Count == 0) continue;
                
                Branch br = b.Statements[b.Statements.Count - 1] as Branch;
                if (br == null) continue;
                
                Block target = br.Target;
                if (target == null) continue;

                if (blocks[target.UniqueKey] == null)
                {
                    PrintClumpStructure(stmts);
                    return false; // found a branch to a block outside the clump!
                }
            }

            return true;
        }

        [Conditional("DEBUG")]
        private static void PrintClumpStructure(StatementList blocks)
        {
            for (int i = 0, n = blocks.Count; i < n; i++)
            {
                Block b = blocks[i] as Block;
                if (b == null) continue; // tolerate nulls

                Console.WriteLine("Block {0}", b.UniqueKey);
                
                if (b.Statements == null || b.Statements.Count == 0) continue;
                
                Branch br = b.Statements[b.Statements.Count - 1] as Branch;
                if (br == null) continue;
                
                Block target = br.Target;
                if (target == null) continue;
                
                Console.WriteLine("  branch to {0}", target.UniqueKey);
            }
        }

        /// <summary>
        /// TODO: Remove this when we support compiler-generated enumerators.
        /// </summary>
        public static readonly TypeNode compilerGeneratedAttributeNode =
            TypeNode.GetTypeNode(typeof (System.Runtime.CompilerServices.CompilerGeneratedAttribute));

        public static readonly TypeNode debuggerNonUserCodeAttributeNode =
            TypeNode.GetTypeNode(typeof (System.Diagnostics.DebuggerNonUserCodeAttribute));

        public static readonly TypeNode debuggerBrowsableAttributeNode =
            TypeNode.GetTypeNode(typeof (System.Diagnostics.DebuggerBrowsableAttribute));

        public static readonly TypeNode debuggerBrowsableStateType =
            TypeNode.GetTypeNode(typeof (System.Diagnostics.DebuggerBrowsableState));

        public static readonly Identifier compilerGeneratedAttributeName = Identifier.For("CompilerGeneratedAttribute");

        internal static bool IsTypeParameterType(TypeNode t)
        {
            return t is ITypeParameter;
        }

        public static bool IsCompilerGenerated(TypeNode t)
        {
            if (t == null) return false;

            if (t.Template != null) t = t.Template;
            
            if (t.GetAttributeByName(compilerGeneratedAttributeName) != null) return true;
            
            if (t.DeclaringType != null) return IsCompilerGenerated(t.DeclaringType);
            
            return false;
        }

        public static bool IsCompilerGenerated(Member m)
        {
            if (IsCompilerGeneratedMemberOnly(m)) return true;

            TypeNode t = m.DeclaringType;
            
            return IsCompilerGenerated(t);
        }

        /// <summary>
        /// C# uses CompilerGenerated, VB uses DebuggerNonUserCode
        /// </summary>
        public static bool IsAutoPropertyMember(Member m)
        {
            if (IsCompilerGeneratedMemberOnly(m)) return true;

            if (IsDebuggerNonUserCodeMemberOnly(m)) return true;
            
            return false;
        }

        public static AttributeNode GetAttributeByName(this Member member, Identifier attributeType)
        {
            if (attributeType == null) return null;

            AttributeList attributes = member.Attributes;
            
            for (int i = 0, n = attributes == null ? 0 : attributes.Count; i < n; i++)
            {
                AttributeNode attr = attributes[i];
                if (attr == null) continue;

                MemberBinding mb = attr.Constructor as MemberBinding;
                if (mb != null)
                {
                    if (mb.BoundMember == null) continue;
                    
                    if (!mb.BoundMember.DeclaringType.Name.Matches(attributeType)) continue;
                    
                    return attr;
                }
                
                Literal lit = attr.Constructor as Literal;
                if (lit == null) continue;
                
                if (!(lit.Value as TypeNode).Name.Matches(attributeType)) continue;
                
                return attr;
            }

            return null;
        }

        private static bool IsCompilerGeneratedMemberOnly(Member m)
        {
            if (m == null) return false;

            if (m.GetAttributeByName(compilerGeneratedAttributeName) != null) return true;
            
            return false;
        }

        private static bool IsDebuggerNonUserCodeMemberOnly(Member m)
        {
            if (m == null) return false;

            if (debuggerNonUserCodeAttributeNode == null) return false;
            
            if (m.GetAttribute(debuggerNonUserCodeAttributeNode) != null) return true;
            
            return false;
        }

        /// <summary>
        /// Extract prefix code prior to contracts such as closure initialization and locals
        /// </summary>
        internal static int ExtractClosureInitializationAndLocalThis(
            Method m,
            Block firstBlock,
            ContractNodes contractNodes,
            Block contractInitializer,
            Block preambleBlock,
            int currentIndex,
            out Local localAliasingThis,
            ref StackDepthTracker dupStackTracker)
        {
            localAliasingThis = null;

            // Any prefix of null statements and nops should go into the preamble block

            //      In addition, for VB structure ctors, they start with initobj this
            //      ------------------------------------------------------------------------
            Contract.Assert(currentIndex <= firstBlock.Statements.Count);

            while (currentIndex < firstBlock.Statements.Count)
            {
                Statement s = firstBlock.Statements[currentIndex];

                if (s != null && s.NodeType != NodeType.Nop)
                {
                    // check for VB "initobj this"
                    if (!m.DeclaringType.IsValueType) break;

                    if (!(m is InstanceInitializer)) break;

                    AssignmentStatement initObj = s as AssignmentStatement;
                    if (initObj == null) break;
                    
                    AddressDereference addrderef = initObj.Target as AddressDereference;
                    if (addrderef == null) break;
                    
                    if (addrderef.Address != m.ThisParameter) break;
                    
                    Literal initObjArg = initObj.Source as Literal;
                    if (initObjArg == null) break;
                    
                    if (initObjArg.Value != null) break;
                }
                
                preambleBlock.Statements.Add(s);

                var oldCount = firstBlock.Statements.Count;
                var oldStats = firstBlock.Statements;
                
                firstBlock.Statements[currentIndex] = null;

                Contract.Assert(oldStats == firstBlock.Statements);
                Contract.Assert(oldCount == firstBlock.Statements.Count);
                
                currentIndex++;
                
                Contract.Assert(currentIndex <= firstBlock.Statements.Count);
            }

            Contract.Assert(currentIndex <= firstBlock.Statements.Count);

            // Type (1): Closure creation and initialization

            //      ---------------------------------------------
            TypeNode closureType;
            currentIndex = MovePastClosureInit(m, firstBlock, contractNodes, contractInitializer.Statements,
                preambleBlock, currentIndex, ref dupStackTracker, out closureType);

            // Local @this in contract classes

            //      -------------------------------
            // For contract classes, since we require that they use explicit interface implementation,
            // allow the single assignment statement "J this_j = this;" as part of the prelude so that
            // explicit casts are not needed all over the contracts.
            Interface iface = HelperMethods.IsContractTypeForSomeOtherType(m.DeclaringType, contractNodes) as Interface;
            if (iface != null && currentIndex < firstBlock.Statements.Count)
            {
                AssignmentStatement assgn = firstBlock.Statements[currentIndex] as AssignmentStatement;
                if (assgn != null)
                {
                    if (assgn.Target is Local && assgn.Target.Type == iface && assgn.Source is This)
                    {
                        // we currently keep the local initializer in both "contractLocalsInitializer" for static decoding
                        // AND in the preamble block for runtime checking code generation.
                        localAliasingThis = (Local) assgn.Target;

                        //        contractInitializer.Statements.Add((Statement)(firstBlock.Statements[currentIndex].Clone()));

                        //        preambleBlock.Statements.Add(firstBlock.Statements[currentIndex]);
                        firstBlock.Statements[currentIndex] = null;

                        currentIndex++;
                    }
                }
            }

            // VB hoists some other literal assignments to the start of the method.

            while (currentIndex < firstBlock.Statements.Count)
            {
                Statement s = firstBlock.Statements[currentIndex];
                if (s != null && s.NodeType != NodeType.Nop)
                {
                    // check for VB literal assignments
                    if (!s.SourceContext.Hidden) break;

                    AssignmentStatement litAssgn = s as AssignmentStatement;
                    
                    if (litAssgn == null) break;
                    var local = litAssgn.Target as Local;
                    
                    if (local == null) break;
                    
                    if (local.Name == null) break;
                    
                    if (!local.Name.Name.StartsWith("VB$t_ref$L")) break;
                    // okay, we are assigning one of these locals. Put it into the preamble.
                }

                preambleBlock.Statements.Add(s);
                
                var oldCount = firstBlock.Statements.Count;
                var oldStats = firstBlock.Statements;
                
                firstBlock.Statements[currentIndex] = null;
                
                Contract.Assert(oldStats == firstBlock.Statements);
                Contract.Assert(oldCount == firstBlock.Statements.Count);
                
                currentIndex++;
                
                Contract.Assert(currentIndex <= firstBlock.Statements.Count);
            }
            
            Contract.Assert(currentIndex <= firstBlock.Statements.Count);

            return currentIndex;
        }

        public struct StackDepthTracker
        {
            public int Depth;
            private readonly Local introducedClosureLocal;

            public bool IsValid
            {
                get { return this.introducedClosureLocal != null; }
            }

            public StackDepthTracker(Local introducedClosureLocal)
            {
                Contract.Ensures(introducedClosureLocal == null || Contract.ValueAtReturn(out this).IsValid);

                this.introducedClosureLocal = introducedClosureLocal;
                this.Depth = 0;
            }

            /// <summary>
            /// Perform subsitution if necessary (create fresh nodes)
            /// </summary>
            internal Statement Visit(Statement statement)
            {
                Contract.Requires(this.IsValid);
                Contract.Requires(this.Depth >= 0);
                Contract.Ensures(this.Depth >= -1);

                if (statement == null) return null;

                switch (statement.NodeType)
                {
                    case NodeType.Nop:
                    case NodeType.Rethrow:
                    case NodeType.EndFilter:
                    case NodeType.EndFinally:
                        break;

                    case NodeType.Pop:
                        // a pop statement rather than expression
                        this.Depth--;
                        if (this.Depth < 0)
                        {
                            // we popped the original closure from the stack.
                            return null;
                        }

                        return statement;

                    case NodeType.AssignmentStatement:
                        var astmt = (AssignmentStatement) statement.Clone();
                        astmt.Target = Visit(astmt.Target);
                        astmt.Source = Visit(astmt.Source);
                        return astmt;

                    case NodeType.ExpressionStatement:
                        var estmt = (ExpressionStatement) statement.Clone();
                        if (estmt.Expression.NodeType == NodeType.Dup)
                        {
                            if (this.Depth == 0)
                            {
                                // duping the closure. Do nothing:
                                return null;
                            }

                            // otherwise, leave dup
                        }
                        else if (estmt.Expression.NodeType == NodeType.Pop)
                        {
                            var unary = estmt.Expression as UnaryExpression;
                            if (unary != null && unary.Operand != null)
                            {
                                // we are popping the thing we just push, so the depth does not change.
                                unary = (UnaryExpression) unary.Clone();
                                unary.Operand = Visit(unary.Operand);
                                estmt.Expression = unary;
                                return estmt;
                            }

                            // a pop statement rather than expression
                            this.Depth--;
                            if (this.Depth < 0)
                            {
                                // we popped the original closure from the stack.
                                return null;
                            }
                        }
                        else
                        {
                            estmt.Expression = Visit(estmt.Expression);
                            if (!IsVoidType(estmt.Expression.Type))
                            {
                                this.Depth++;
                            }
                        }

                        return estmt;

                    case NodeType.Block:
                        var block = (Block) statement.Clone();
                        block.Statements = Visit(block.Statements);
                        return block;

                    case NodeType.SwitchInstruction:
                        var sw = (SwitchInstruction) statement.Clone();
                        sw.Expression = Visit(sw.Expression);
                        return sw;

                    case NodeType.Throw:
                        var th = (Throw) statement.Clone();
                        th.Expression = Visit(th.Expression);
                        return th;

                    case NodeType.Return:
                        var ret = (Return) statement.Clone();
                        ret.Expression = Visit(ret.Expression);
                        return ret;

                    default:
                        var br = statement as Branch;
                        if (br != null)
                        {
                            br = (Branch) br.Clone();
                            br.Condition = Visit(br.Condition);
                            return br;
                        }

                        throw new NotImplementedException("ClosureNormalization Statement");
                }

                return statement;
            }

            private StatementList Visit(StatementList statementList)
            {
                Contract.Requires(this.Depth >= 0);
                Contract.Ensures(this.Depth >= -1);

                if (statementList == null) return null;

                statementList = statementList.Clone();
                for (int i = 0; i < statementList.Count; i++)
                {
                    statementList[i] = Visit(statementList[i]);
                    if (this.Depth < 0) return statementList; // stop
                }

                return statementList;
            }

            private Expression Visit(Expression expression)
            {
                Contract.Requires(this.Depth >= 0);
                Contract.Ensures(this.Depth >= 0);

                if (this.introducedClosureLocal == null) return expression;

                if (expression == null) return null;
                
                switch (expression.NodeType)
                {
                    case NodeType.MemberBinding:
                        var mb = (MemberBinding) expression.Clone();
                        mb.TargetObject = Visit(mb.TargetObject);
                        return mb;

                    case NodeType.This:
                    case NodeType.Parameter:
                    case NodeType.Local:
                        break;

                    case NodeType.Pop:
                        if (this.Depth == 0)
                        {
                            return this.introducedClosureLocal;
                        }
                        this.Depth--;
                        break;

                    case NodeType.Literal:
                    case NodeType.Arglist:
                        break;

                    case NodeType.AddressDereference:
                        var ad = (AddressDereference) expression.Clone();
                        ad.Address = Visit(ad.Address);
                        return ad;

                    case NodeType.Construct:
                        var c = (Construct) expression.Clone();
                        c.Operands = Visit(c.Operands);
                        return c;

                    case NodeType.ConstructArray:
                        var ca = (ConstructArray) expression.Clone();
                        ca.Operands = Visit(ca.Operands);
                        return ca;

                    case NodeType.Call:
                    case NodeType.Callvirt:
                        var call = (MethodCall) expression.Clone();
                        call.Callee = Visit(call.Callee);
                        call.Operands = Visit(call.Operands);
                        return call;

                    case NodeType.Cpblk:
                    case NodeType.Initblk:
                        var tern = (TernaryExpression) expression.Clone();
                        tern.Operand1 = Visit(tern.Operand1);
                        tern.Operand2 = Visit(tern.Operand2);
                        tern.Operand3 = Visit(tern.Operand3);
                        return tern;

                    case NodeType.Indexer:
                        var indexer = (Indexer) expression.Clone();
                        indexer.Object = Visit(indexer.Object);
                        indexer.Operands = Visit(indexer.Operands);
                        return indexer;

                    default:
                        var binary = expression as BinaryExpression;
                        if (binary != null)
                        {
                            binary = (BinaryExpression) binary.Clone();
                            binary.Operand1 = Visit(binary.Operand1);
                            binary.Operand2 = Visit(binary.Operand2);
                            return binary;
                        }

                        var unary = expression as UnaryExpression;
                        if (unary != null)
                        {
                            unary = (UnaryExpression) unary.Clone();
                            unary.Operand = Visit(unary.Operand);
                            return unary;
                        }
                        
                        throw new NotImplementedException("ClosureNormalization Expression");
                }

                return expression;
            }

            private ExpressionList Visit(ExpressionList expressionList)
            {
                if (expressionList == null) return null;

                expressionList = expressionList.Clone();
                
                for (int i = 0; i < expressionList.Count; i++)
                {
                    expressionList[i] = Visit(expressionList[i]);
                }
                
                return expressionList;
            }
        }

        /// <summary>
        /// Copies the closure initialization into the contractinitializer and preambleBlock (if non-null)
        /// </summary>
        internal static int MovePastClosureInit(Method m, Block firstBlock, ContractNodes contractNodes,
            StatementList contractInitializer, Block preambleBlock, int currentIndex,
            ref StackDepthTracker dupStackTracker, out TypeNode closureType)
        {
            int indexForClosureCreationStatement = currentIndex;
            
            closureType = null;
            Local introducedClosureLocal = null;
            
            while (indexForClosureCreationStatement < firstBlock.Statements.Count)
            {
                var closureCreationCandidate = firstBlock.Statements[indexForClosureCreationStatement];
                closureType = IsClosureCreation(m, closureCreationCandidate);
                
                if (closureType != null) break;
                
                if (contractNodes.IsContractOrValidatorOrAbbreviatorCall(closureCreationCandidate))
                {
                    // found contracts before closure creation, so get out
                    return currentIndex;
                }

                if (closureCreationCandidate != null && closureCreationCandidate.SourceContext.IsValid)
                    return currentIndex;
                
                indexForClosureCreationStatement++;
            }

            if (closureType != null && indexForClosureCreationStatement < firstBlock.Statements.Count)
            {
                // then there is a set of statements to add to the preamble block
                // up to and including "local := new ClosureClass();"
                for (int i = currentIndex; i <= indexForClosureCreationStatement; i++)
                {
                    if (firstBlock.Statements[i] == null) continue;

                    if (preambleBlock != null)
                    {
                        preambleBlock.Statements.Add(firstBlock.Statements[i]);
                    }

                    Local existingClosureLocal;
                    if (IsClosureCreation(m, firstBlock.Statements[i], out existingClosureLocal))
                    {
                        if (existingClosureLocal == null)
                        {
                            // introduce one
                            ExpressionStatement estmt = firstBlock.Statements[i] as ExpressionStatement;
                            if (estmt != null)
                            {
                                introducedClosureLocal = new Local(closureType);
                                contractInitializer.Add(
                                    new AssignmentStatement(introducedClosureLocal, (Expression) estmt.Expression.Clone()));
                            }
                            else
                            {
                                contractInitializer.Add((Statement) firstBlock.Statements[i].Clone());
                            }
                        }
                        else
                        {
                            contractInitializer.Add((Statement) firstBlock.Statements[i].Clone());
                        }
                    }
                    else
                    {
                        contractInitializer.Add((Statement) firstBlock.Statements[i].Clone());
                    }

                    if (preambleBlock != null)
                    {
                        firstBlock.Statements[i] = null;
                        // need to null them out so search below can be done starting at beginning of m's body
                    }
                }

                // Some number of assignment statements of the form "local.f := f;" where "f" is a parameter
                // that is captured by the closure.
                //
                // Roslyn generates code as follows:
                //   new Closure()
                //   dup
                //   ldarg f
                //   stfld f
                //   dup
                //   ldarg q
                //   stfld q
                //   dup...
                // 

                int endOfAssignmentsToClosureFields = indexForClosureCreationStatement + 1;
                for (; endOfAssignmentsToClosureFields < firstBlock.Statements.Count; endOfAssignmentsToClosureFields++)
                {
                    Statement s = firstBlock.Statements[endOfAssignmentsToClosureFields];
                    if (s == null) continue;

                    if (s.NodeType == NodeType.Nop) continue;
                    
                    if (s.SourceContext.IsValid) break; // end of closure
                    
                    if (s is Return) break; // Return is also an ExpressionStatement
                    
                    ExpressionStatement exprSt = s as ExpressionStatement;
                    if (exprSt != null && exprSt.Expression.NodeType == NodeType.Dup)
                    {
                        // dup of closure node
                        continue;
                    }

                    AssignmentStatement assign = s as AssignmentStatement;
                    if (assign == null) break;
                    
                    MemberBinding mb = assign.Target as MemberBinding;
                    
                    if (mb == null) break;
                    
                    if (mb.TargetObject == null ||
                        (mb.TargetObject.Type != closureType && mb.TargetObject.NodeType != NodeType.Pop)) break;
                }

                if (endOfAssignmentsToClosureFields - 1 < firstBlock.Statements.Count &&
                    endOfAssignmentsToClosureFields >= 1 &&
                    IsDup(firstBlock.Statements[endOfAssignmentsToClosureFields - 1]))
                {
                    endOfAssignmentsToClosureFields--; // last dup is not part of closure init
                }

                dupStackTracker = new StackDepthTracker(introducedClosureLocal);
                for (int i = indexForClosureCreationStatement + 1; i < endOfAssignmentsToClosureFields; i++)
                {
                    var stmt = firstBlock.Statements[i];
                    
                    if (stmt == null) continue;
                    
                    if (preambleBlock != null)
                    {
                        preambleBlock.Statements.Add(stmt);
                    }

                    if (stmt.NodeType != NodeType.Nop)
                    {
                        // don't add nop's to contract initializer
                        if (dupStackTracker.IsValid)
                        {
                            contractInitializer.Add(dupStackTracker.Visit(stmt));
                        }
                        else
                        {
                            contractInitializer.Add((Statement) stmt.Clone());
                        }
                    }

                    if (preambleBlock != null)
                    {
                        firstBlock.Statements[i] = null;
                        // need to null them out so search below can be done starting at beginning of m's body
                    }
                }

                currentIndex = endOfAssignmentsToClosureFields;
            }

            return currentIndex;
        }

        private static bool IsDup(Statement statement)
        {
            var expr = statement as ExpressionStatement;
            if (expr == null) return false;

            if (expr.Expression != null && expr.Expression.NodeType == NodeType.Dup) return true;
            
            return false;
        }

        /// <summary>
        /// Searches the body of the method to see if there is preamble code.
        /// There are two potential types of code that should go in the preamble.
        /// (1) Code which creates a closure class and initializes its fields.
        /// (2) If the method is a ctor:
        ///   (a) code which initializes fields that had been given initializations as part
        ///     of their declaration. (This does not appear in "deferring ctors".)
        ///   (b) code which calls the base class ctor (or the same class ctor if m is a "deferring ctor").
        /// (3) If the method has a BeginContractBlock call, everything in front of it is the preamble.
        ///
        /// Type (1) is assumed to always appear as a sequence of statements in the first block. Type (2)
        /// might be spread over several blocks if there is control flow in the field initializers or
        /// the base/this ctor call.
        /// 
        /// If preamble code is found, this method creates a new block
        /// containing only those statements, deletes them from the block they had been contained
        /// in and modifies the method body so that the preamble block is
        /// first in the body.
        /// </summary>
        /// <param name="m">The method whose body will be searched for closure initialization statements
        /// and whose body will be modified if it is found.
        /// </param>
        /// <returns>null or a local that aliases "this" in contracts</returns>
        [System.Diagnostics.Contracts.ContractVerification(false)]
        internal static Local ExtractPreamble(Method m, ContractNodes contractNodes, Block contractInitializer,
            out Block postPreamble, ref StackDepthTracker dupStackTracker, bool isVB)
        {
            postPreamble = null;

            if (m == null || m.Body == null || m.Body.Statements == null || !(m.Body.Statements.Count > 0)) return null;
            
            Block firstBlock = m.Body.Statements[0] as Block;
            if (firstBlock == null) return null;
            
            Block preambleBlock = new PreambleBlock(new StatementList(0));

            Local result;
            
            int nextInstructionInFirstBlock = ExtractClosureInitializationAndLocalThis(m, firstBlock, contractNodes,
                contractInitializer, preambleBlock, 0, out result, ref dupStackTracker);

            // Type (2): Base ctor or deferring ctor call

            if (m is InstanceInitializer)
            {
                int i, j;
                bool found;

                // Type (2a): Assignments to fields of class that had initializers

                // Just extract the statements for (2a) as part of what gets extracted for (2b)

                // Type (2b): base or deferring ctor call

                found = SearchClump(m.Body.Statements, 
                    delegate(Statement s)
                    {
                        if (s == null) return false;

                        ExpressionStatement es = s as ExpressionStatement;
                        if (es == null) return false;
                    
                        MethodCall mc = es.Expression as MethodCall;
                        if (mc == null) return false;
                    
                        MemberBinding mb = mc.Callee as MemberBinding;
                        if (mb == null) return false;
                    
                        Method callee = mb.BoundMember as Method;
                    
                        // can't depend on the TargetObject being "this": it could be "pop" if
                        // the arguments to the call had any control flow
                        // e.g., a boolean short-circuit expression or a ternary expression.
                        //if (mb.TargetObject is This && callee is InstanceInitializer) return true;
                        if (callee is InstanceInitializer
                            &&
                            ((callee.DeclaringType == m.DeclaringType || callee.DeclaringType.Template == m.DeclaringType)
                             // deferring ctor call
                             ||
                             (callee.DeclaringType == m.DeclaringType.BaseType ||
                              // base ctor call
                              callee.DeclaringType.Template == m.DeclaringType.BaseType.Template)))
                        {
                            return true;
                        }

                        return false;

                    }, 
                    out i, out j);

                if (found)
                {
                    // for VB constructors and C# constructors with closures, we need to extend the scope to include field initializers

                    Block b = (Block) m.Body.Statements[i];
                    int k = j + 1;
                    Local extraClosureLocal = null;
                    int stackDepth = 0;

                    for (; k < b.Statements.Count; k++)
                    {
                        // skip over auxiliary closure creation
                        if (extraClosureLocal == null && IsClosureCreation(m, b.Statements[k], out extraClosureLocal))
                        {
                            continue;
                        }

                        switch (b.Statements[k].NodeType)
                        {
                            case NodeType.Nop:
                                continue;

                            case NodeType.AssignmentStatement:
                            {
                                AssignmentStatement assgmt = (AssignmentStatement) b.Statements[k];
                                MemberBinding mb = assgmt.Target as MemberBinding;
                                if (mb == null)
                                {
                                    // we might be initializing locals for default values etc
                                    if (stackDepth > 0) continue;
                                    
                                    goto doneWithFieldInits;
                                }

                                if (!(mb.BoundMember is Field))
                                {
                                    // we might be initializing locals for default values etc
                                    if (stackDepth > 0) continue;
                                    goto doneWithFieldInits;
                                }

                                if (mb.TargetObject == null)
                                {
                                    // we might be initializing locals for default values etc
                                    if (stackDepth > 0) continue;
                                    goto doneWithFieldInits;
                                }

                                // there are 2 cases here. Either we are assigning to this.f = ..., which is a field initialization,
                                // or it is initializing the closure field for "this" with this.
                                if (mb.TargetObject.NodeType == NodeType.This)
                                {
                                    // okay field initialization
                                    continue;
                                }

                                if (mb.TargetObject.NodeType == NodeType.Pop)
                                {
                                    // okay field initialization (popping this)
                                    stackDepth--;
                                    continue;
                                }

                                // we might also be initializing the extra closure fields, if the target is the extraClosureLocal.
                                if (mb.TargetObject == extraClosureLocal)
                                {
                                    continue;
                                }

                                if (mb.TargetObject.NodeType == NodeType.Local && assgmt.Source != null &&
                                    assgmt.Source.NodeType == NodeType.This && mb.BoundMember.Name != null &&
                                    (mb.BoundMember.Name.Name.EndsWith("_this") || mb.BoundMember.Name.Name == "$VB$Me"))
                                {
                                    // closure initialization (storing this)
                                    // add it to postPreamble, since it needs to be inserted for duplicate closure object
                                    postPreamble = new Block(new StatementList(1));
                                    postPreamble.Statements.Add((Statement) b.Statements[k].Clone());
                                    continue;
                                }

                                goto doneWithFieldInits;
                            }

                            case NodeType.ExpressionStatement:
                            {
                                ExpressionStatement estmt = (ExpressionStatement) b.Statements[k];
                                if (estmt.Expression != null && estmt.Expression is This)
                                {
                                    // handle special push/pop pattern occurring occasionally
                                    stackDepth++;
                                    continue;
                                }

                                // handle ctor calls on addresses of value types
                                // NOTE: this could be mistakenly the first user statement.
                                MethodCall mc = estmt.Expression as MethodCall;
                                if (mc == null) goto doneWithFieldInits;

                                MemberBinding mb = mc.Callee as MemberBinding;
                                if (mb == null) goto doneWithFieldInits;
                                
                                if (!(mb.BoundMember is InstanceInitializer)) goto doneWithFieldInits;
                                
                                if (!mb.BoundMember.DeclaringType.IsValueType) goto doneWithFieldInits;
                                
                                if (mb.TargetObject.NodeType != NodeType.AddressOf) goto doneWithFieldInits;
                                
                                continue;
                            }

                            default:
                                goto doneWithFieldInits;
                        }
                    }

                    doneWithFieldInits:

                    StatementList sl2 = ExtractClump(m.Body.Statements, 0, 0, i, k - 1);
                    preambleBlock.Statements.Add(new Block(sl2));

                    ExtractVB_ENCCallToPreamble(m.Body.Statements[i] as Block, k, preambleBlock.Statements);
                }
                else
                {
                    // for struct ctors, we may not have a "this" call

                    // for C# constructors with closures, we need to extend the scope

                    Block b = firstBlock;
                    int k = nextInstructionInFirstBlock;
                    Local extraClosureLocal = null;

                    for (; k < b.Statements.Count; k++)
                    {
                        // skip over auxiliary closure creation and initialization
                        if (extraClosureLocal == null && IsClosureCreation(m, b.Statements[k], out extraClosureLocal))
                        {
                            continue;
                        }

                        switch (b.Statements[k].NodeType)
                        {
                            case NodeType.Nop:
                                continue;

                            case NodeType.AssignmentStatement:
                            {
                                AssignmentStatement assgmt = (AssignmentStatement) b.Statements[k];
                                MemberBinding mb = assgmt.Target as MemberBinding;
                                
                                if (mb == null) goto doneWithFieldInits;

                                if (!(mb.BoundMember is Field)) goto doneWithFieldInits;
                                
                                if (mb.TargetObject == null) goto doneWithFieldInits;
                                
                                // we might be initializing the extra closure fields, if the target is the extraClosureLocal.
                                if (mb.TargetObject == extraClosureLocal)
                                {
                                    continue;
                                }

                                goto doneWithFieldInits;
                            }

                            default:
                                goto doneWithFieldInits;
                        }
                    }

                    doneWithFieldInits:
                    
                    for (int toCopy = nextInstructionInFirstBlock; toCopy < k; toCopy++)
                    {
                        preambleBlock.Statements.Add(firstBlock.Statements[toCopy]);
                        firstBlock.Statements[toCopy] = null;
                    }
                }
            }

            // Create a new body, add the preamble block, then all of the other blocks

            StatementList sl = new StatementList(m.Body.Statements.Count);
            sl.Add(preambleBlock);

            if (m.Body != null)
            {
                foreach (Block b in m.Body.Statements)
                {
                    if (b != null) sl.Add(b);
                }
            }

            m.Body.Statements = sl;

            return result;
        }

        private static int ExtractVB_ENCCallToPreamble(Block firstBlock, int currentIndex, StatementList preamble)
        {
            if (firstBlock == null) return currentIndex;

            while (currentIndex < firstBlock.Statements.Count)
            {
                Statement s = firstBlock.Statements[currentIndex];
                if (s != null && s.NodeType != NodeType.Nop)
                {
                    // check for VB literal assignments
                    if (!s.SourceContext.Hidden) break;

                    var es = s as ExpressionStatement;
                    
                    if (es != null)
                    {
                        MethodCall call = es.Expression as MethodCall;
                        if (call != null)
                        {
                            var mb = call.Callee as MemberBinding;
                            if (mb == null) break;

                            var encMethod = mb.BoundMember as Method;
                            if (encMethod == null) break;
                            
                            if (encMethod.Name.Name != "__ENCAddToList") break;
                            
                            // okay, we are calling the VB memory leak, put it into the preamble
                            goto addToPreamble;
                        }
                    }

                    break; // don't skip other stuff
                }

                if (s == null) continue;
                
            addToPreamble:
                preamble.Add(s);
                
                var oldCount = firstBlock.Statements.Count;
                var oldStats = firstBlock.Statements;
                
                firstBlock.Statements[currentIndex] = null;
                
                Contract.Assert(oldStats == firstBlock.Statements);
                Contract.Assert(oldCount == firstBlock.Statements.Count);
                
                currentIndex++;
                
                Contract.Assert(currentIndex <= firstBlock.Statements.Count);
            }

            return currentIndex;
        }

        [Pure]
        public static bool IsClosureCreation(Method containingMethod, Statement s, out Local closureLocal)
        {
            var closureType = IsClosureCreation(containingMethod, s);
            if (closureType != null)
            {
                // it's a closure alright
                var assign = s as AssignmentStatement;
                if (assign != null)
                {
                    closureLocal = assign.Target as Local;
                }
                else
                {
                    closureLocal = null;
                }

                return true;
            }
            
            closureLocal = null;
            return false;
        }

        /// <summary>
        /// Returns the type of the closure class if this is a closure creation statement
        /// </summary>
        [Pure]
        internal static TypeNode IsClosureCreation(Method containingMethod, Statement s)
        {
            AssignmentStatement assign = s as AssignmentStatement;
            if (assign == null)
            {
                goto MaybePush;
            }

            Construct ctorCall = assign.Source as Construct;
            if (ctorCall == null)
            {
                return null;
            }
            
            if (IsClosureType(containingMethod.DeclaringType, assign.Target.Type))
            {
                return assign.Target.Type;
            }

        MaybePush:

            ExpressionStatement expr = s as ExpressionStatement;
            if (expr == null)
            {
                return null;
            }

            var construct = expr.Expression as Construct;
            if (construct == null) return null;
            
            if (IsClosureType(containingMethod.DeclaringType, construct.Type))
            {
                return construct.Type;
            }

            return null;
        }

        [Pure]
        public static bool IsClosurePart(TypeNode container, Member member)
        {
            Contract.Requires(container != null);
            Contract.Requires(member != null);

            TypeNode type = member as TypeNode;
            if (type != null) return IsClosureType(container, type);

            Method method = member as Method;
            if (method != null) return IsClosureMethod(container, method);

            Field field = member as Field;
            if (field != null) return IsClosureField(container, field);

            return false;
        }

        [Pure]
        public static bool IsClosureType(TypeNode parent, TypeNode type)
        {
            Contract.Requires(parent != null);
            Contract.Requires(type != null);

            type = Unspecialize(type);

            if (type.DeclaringType == null) return false;
            
            if (type.IsVisibleOutsideAssembly) return false;
            
            if (!IsInsideOf(type, parent)) return false;
            
            if (!IsCompilerGenerated(type)) return false;

            return true;
        }

        [Pure]
        public static bool IsClosureMethod(TypeNode container, Method method)
        {
            Contract.Requires(container != null);
            Contract.Requires(method != null);
            
            method = Unspecialize(method);
            
            var type = method.DeclaringType;
            
            Debug.Assert(TypeNode.IsCompleteTemplate(type));
            
            if (IsClosureType(container, type)) return true;
            
            // can be a direct member of a type inside container
            
            if (!method.IsPrivate) return false;
            
            if (method.DeclaringMember != null) return false; // property or event helpers
            
            if (!IsInsideOf(method, container)) return false;
            
            if (!IsCompilerGenerated(method)) return false;
            
            return true;
        }

        [Pure]
        public static bool IsClosureField(TypeNode container, Field field)
        {
            Contract.Requires(container != null);
            Contract.Requires(field != null);

            var type = Unspecialize(field.DeclaringType);

            if (IsClosureType(container, type)) return true;
            
            // can be a direct member of a type inside container for caching delegates
            
            if (!field.IsPrivate) return false;
            
            if (!(field.Type.IsDelegateType())) return false;
            
            if (!IsInsideOf(field, container)) return false;
            
            if (!IsCompilerGenerated(field)) return false;
            
            return true;
        }

#if false
    internal static bool IsClosureType(TypeNode containingMethodDeclaringType, TypeNode typeNode)
    {
      if (!IsCompilerGenerated(typeNode)) return false;
      // same declaration level
      var closureDT = typeNode.DeclaringType;
      if (closureDT == null) return false;
      closureDT = Unspecialize(closureDT);
      var methodDT = Unspecialize(containingMethodDeclaringType);

      if (IsCompilerGenerated(methodDT) && methodDT.DeclaringType != null)
      {
        methodDT = Unspecialize(methodDT.DeclaringType);
      }
      if (methodDT != closureDT) return false;
      return true;
    }
#endif

#if false
    /// <summary>
    /// Gets the type T specified in [typeOfAttribute(typeof(T))] if specified.
    /// </summary>
    /// <param name="type">Type possibly having the attribute named "typeofAttribute".</param>
    /// <param name="typeOfAttribute">The type of the attribute that has the type T as its argument.</param>
    /// <returns>TypeNode for the type specified in the attribute, or null if the attribute is not present.</returns>
    internal static TypeNode/*?*/ GetTypeFromAttribute(TypeNode type, TypeNode typeOfAttribute) {
      if (type == null) return null;
      AttributeNode contractClass = type.GetAttribute(typeOfAttribute);
      if (contractClass == null)
        return null;
      Expression typeExpr = contractClass.GetPositionalArgument(0);
      if (typeExpr == null)
        return null;
      Literal typeLiteral = typeExpr as Literal;
      if (typeLiteral == null)
        return null;
      TypeNode typeNode = typeLiteral.Value as TypeNode;
      if (typeNode == null)
        return null;
      return typeNode;
    }

#endif

        [Pure]
        public static AttributeNode GetAttribute(this AttributeList attributes, Identifier attributeName)
        {
            if (attributes == null) return null;

            for (int i = 0; i < attributes.Count; i++)
            {
                var attr = attributes[i];
                
                if (attr == null) continue;
                
                if (attr.Type == null) continue;
                
                if (attributeName.Matches(attr.Type.Name)) return attr;
            }

            return null;
        }

        /// <summary>
        /// Gets the type T specified in [typeOfAttribute(typeof(T))] if specified.
        /// </summary>
        /// <param name="type">Type possibly having the attribute named "typeofAttribute".</param>
        /// <param name="typeOfAttribute">The type of the attribute that has the type T as its argument.</param>
        /// <returns>TypeNode for the type specified in the attribute, or null if the attribute is not present.</returns>
        /// 
        [Pure]
        internal static TypeNode /*?*/ GetTypeFromAttribute(Member member, Identifier attributeName)
        {
            return GetAttributeArgument(member, attributeName, 0) as TypeNode;
        }

        [Pure]
        public static string GetStringFromAttribute(Member member, Identifier attributeName)
        {
            return GetAttributeArgument(member, attributeName, 0) as string;
        }

        /// <summary>
        /// Get argument from attribute
        /// </summary>
        [Pure]
        internal static object GetAttributeArgument(Member member, Identifier attributeName, int position)
        {
            if (member == null) return null;

            AttributeNode contractClass = member.Attributes.GetAttribute(attributeName);
            
            if (contractClass == null)
                return null;
            
            if (contractClass.Expressions == null) return null;
            
            if (contractClass.Expressions.Count <= position) return null;
            
            Expression typeExpr = contractClass.GetPositionalArgument(position);
            
            if (typeExpr == null)
                return null;
            
            Literal literal = typeExpr as Literal;
            
            if (literal == null)
                return null;
            
            return literal.Value;
        }

        private static void FindClosureInitialization(Method containing, Block b, out Local closureLocal)
        {
            Contract.Requires(containing != null);

            closureLocal = null;
            if (b == null || b.Statements == null || !(b.Statements.Count > 0))
            {
                return;
            }

            for (int i = 0; i < b.Statements.Count; i++)
            {
                Statement s = b.Statements[i];
                if (s == null || s.NodeType == NodeType.Nop) continue;
                
                if (IsClosureCreation(containing, s, out closureLocal))
                {
                    if (closureLocal != null)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// TODO: figure out if there can be more than one closure initialization in a closureInitializer.
        /// Or other stuff.
        /// </summary>
        private static void DeleteClosureInitialization(Method containing, MethodContract mc, Local closureLocal)
        {
            Contract.Requires(containing != null);
            Contract.Requires(mc != null);
            Contract.Requires(closureLocal != null);

            var b = mc.ContractInitializer;
            mc.PostPreamble = null;

            bool foundStart = false;
            if (b == null || b.Statements == null || !(b.Statements.Count > 0))
            {
                return;
            }

            for (int i = 0; i < b.Statements.Count; i++)
            {
                Statement s = b.Statements[i];
                if (s == null || s.NodeType == NodeType.Nop) continue;

                if (s is Block)
                {
                    // indicates code from an abbreviator call
                    if (foundStart)
                    {
                        // this is the end
                        return;
                    }

                    // skip the block
                    continue;
                }

                Local otherLocal;
                if (IsClosureCreation(containing, s, out otherLocal))
                {
                    if (foundStart)
                    {
                        // this must be the next one
                        return;
                    }

                    if (closureLocal == otherLocal)
                    {
                        foundStart = true;
                    }
                }

                if (foundStart)
                {
                    // delete
                    b.Statements[i] = null; // delete
                }
            }
        }

        private static void ForceSideEffect(object anything)
        {
        }

        public static MethodContract DuplicateContractAndClosureParts(Method targetMethod, Method sourceMethod,
            ContractNodes contractNodes, bool copyValidations)
        {
            Module targetModule = targetMethod.DeclaringType.DeclaringModule;

            var dup = new DuplicatorForContractsAndClosures(targetModule, sourceMethod, targetMethod, contractNodes);

            if (sourceMethod.Contract.AsyncEnsuresCount != 0)
            {
                // Removing the name lead to NRE for async method that uses Contract.Ensures(Contract.ForAll)
                // with a capturing lambda.
                // To preserve as much of backward compatible behavior as possible the fix will affect
                // only method contracts with async closures. It still possible that this fix will break existing code
                // and it is possible that there is no any issues by preserving name of the locals.
                // This fix could be enhanced in the future when this issue would be clear.
                dup.RemoveNameForLocals = false;
            }

            return DuplicateContractAndClosureParts(dup, targetMethod, sourceMethod, contractNodes, copyValidations);
        }

        internal static MethodContract DuplicateContractAndClosureParts(
            DuplicatorForContractsAndClosures dup, Method targetMethod, Method sourceMethod,
            ContractNodes contractNodes, bool copyValidations)
        {
            Contract.Ensures(Contract.Result<MethodContract>() != null);

            //System.Console.WriteLine(">>>" + sourceMethod.FullName);

            // materialize source contract:
            var sourceContract = sourceMethod.Contract;
            ForceSideEffect(sourceContract.ContractInitializer);
            TypeNode targetType = targetMethod.DeclaringType;
            TypeNode sourceType = sourceMethod.DeclaringType;

            // need to null out ProvideNestedTypes so the NestedTypes property doesn't use
            // metadata to fill in the list of nested types. Because maybe sourceType has
            // had nested types added to it in memory and those nested types don't exist
            // in the assembly that sourceType is defined in.

            // The source type itself shouldn't be duplicated, because any references to its
            // members (e.g., method calls) in a contract should remain as references to that
            // member. I.e., a contract on virtual method B.M might contain a call to "this.P()". When copying
            // the contract from B.M to an override C.M, "this" of type B should become "this" of type C (which
            // is why the self parameter is mapped below), but the member B.P() should remain B.P().
            // However, any nested types within the source type, such as any closure classes, *should* be
            // duplicated.
            sourceType.ProvideNestedTypes = null;
            targetType.ProvideNestedTypes = null;

            // HACK

            // For some reason, it is important to materialize the name of the targetType here!!!
            // Otherwise the duplicator will fail.
            ForceSideEffect(targetType.Name.Name);

            Local closureLocal;
            FindClosureInitialization(sourceMethod, sourceContract.ContractInitializer, out closureLocal);

            // Duplicate anonymous delegates that turn into static methods (and their caching fields)
            FindClosurePartsToDuplicate fmd = new FindClosurePartsToDuplicate(sourceType, sourceMethod);
            fmd.VisitMethodContract(sourceContract);

            // special handling of closures that are not used.
            if (closureLocal != null)
            {
                var closureType = Unspecialize(closureLocal.Type);
                if (!fmd.MembersToDuplicate.Contains(closureType))
                {
                    // contracts do not depend on closure and won't copy it, so remove the initialization, otherwise debugger gets confused
                    DeleteClosureInitialization(sourceMethod, sourceContract, closureLocal);
                }
            }

            // !Very important!
            // To get appropriate types mapping for duplicated members we have to have two steps approach.
            // First we need to copy all static closures (they're exists only for roslyn-based compiler)
            // Second we need to copy all other members (including closures).
            // The reason why the members are traversed backwards is following:
            // Nested scope can introduced nested closures. In this case, top level closures can reference
            // low level closures. Traversing them in backward order will allow to fill type mapping appropriately.

            for (var memindex = fmd.MembersToDuplicate.Count - 1; memindex >= 0; memindex--)
            {
                Member member = fmd.MembersToDuplicate[memindex];

                if (IsRoslynBasedStaticClosure(member))
                {
                    DuplicateMember(dup, fmd, memindex, targetType);
                }
            }

            for (var memindex = fmd.MembersToDuplicate.Count - 1; memindex >= 0; memindex--)
            {
                var member = fmd.MembersToDuplicate[memindex];
                if (IsRoslynBasedStaticClosure(member))
                {
                    continue;
                }

                DuplicateMember(dup, fmd, memindex, targetType);
            }

            var duplicateContract = dup.VisitMethodContract(sourceContract);
            if (copyValidations)
            {
                duplicateContract.Validations = dup.VisitRequiresList(sourceContract.Validations);
            }

            foreach (Member mem in sourceType.Members)
            {
                if (mem == null) continue;
                Member newMember = (Member) dup.DuplicateFor[mem.UniqueKey];
                if (newMember != null && mem != (Member) targetType && newMember.DeclaringType == targetType &&
                    !targetType.Members.Contains(newMember))
                {
                    TypeNode nestedType = mem as TypeNode;
                    if (nestedType != null && nestedType.Template != null)
                    {
                        // don't add instantiations
                        continue;
                    }

                    Method nestedMethod = mem as Method;
                    if (nestedMethod != null && nestedMethod.Template != null)
                    {
                        // don't add instantiations
                        continue;
                    }
                    
                    // second conjunct is to make sure we don't recursively add a nested type to itself
                    // e.g., ArrayList+IListWrapper extends ArrayList and so inherits contracts from it
                    // so this method could be called with sourceMethod "ArrayList.M" and targetMethod "ArrayList+IListWrapper.M"

                    // But need to make sure that there isn't already a type with the same name!!!
                    TypeNode possibleClash = targetType.GetNestedType(newMember.Name);
                    if (possibleClash != null)
                    {
                        newMember.Name = Identifier.For(newMember.Name.Name + "_1");
                    }

                    // System.Console.WriteLine("found nested member that wasn't there before closure and contract duplication: {0}", newMember.FullName);
                    dup.SafeAddMember(targetType, newMember, mem);
                    //targetType.Members.Add(newMember);
                }
            }

            return duplicateContract;
        }

        private static void DuplicateMember(DuplicatorForContractsAndClosures dup, FindClosurePartsToDuplicate fmd,
            int memindex, TypeNode targetType)
        {
            Member mem = fmd.MembersToDuplicate[memindex];
            
            TypeNode nestedType = mem as TypeNode;
            Method closureInstanceMethod = mem as Method;
            Method closureMethodTemplate = closureInstanceMethod;
            
            while (closureMethodTemplate != null && closureMethodTemplate.Template != null)
            {
                closureMethodTemplate = closureMethodTemplate.Template;
            }

            if (nestedType != null)
            {
                // if nested type is nested inside another type to be duplicated, skip it
                if (nestedType.DeclaringType != null && fmd.MembersToDuplicate.Contains(nestedType.DeclaringType))
                    return;
                
                // For call-site wrappers, we end up having multiple methods from the same original contract method
                // thus we have to avoid duplicating the same closure type multiple times.
                var duplicatedNestedType = FindExistingClosureType(targetType, nestedType);
                if (duplicatedNestedType == null)
                {
                    dup.FindTypesToBeDuplicated(new TypeNodeList(nestedType));

                    // if parent type is generic and different from the target type, then we may have to include all the
                    // consolidated type parameters excluding the ones from the target type.
                    TypeNodeList originalTemplateParameters = FindNonStandardTypeParametersToBeDuplicated(fmd,
                        nestedType, targetType);

                    duplicatedNestedType = dup.Visit(mem) as TypeNode;
                    if (originalTemplateParameters != null)
                    {
                        int parentParameters = (targetType.ConsolidatedTemplateParameters == null)
                            ? 0
                            : targetType.ConsolidatedTemplateParameters.Count;

                        if (parentParameters > 0 &&
                            (nestedType.DeclaringType.ConsolidatedTemplateParameters == null ||
                             nestedType.DeclaringType.ConsolidatedTemplateParameters.Count == 0))
                        {
                            // type is turning from non-generic to generic
                            Debug.Assert(false);
                        }

                        TypeNodeList dupTPs = DuplicateTypeParameterList(originalTemplateParameters, parentParameters, duplicatedNestedType);

                        duplicatedNestedType.TemplateParameters = dupTPs;
                        dup.SafeAddMember(targetType, duplicatedNestedType, mem);


                        // populate the self specialization forwarding
                        //  OriginalDecl<X,Y,Z>.NestedType<A,B,C> -> WrapperType<U,V,W>.NewNestedType<X,Y,Z,A,B,C>
                        //var oldSelfInstanceType = nestedType.GetGenericTemplateInstance(targetModule, nestedType.ConsolidatedTemplateParameters);
                        //var newSelfInstanceType = duplicatedNestedType.GetGenericTemplateInstance(targetModule, duplicatedNestedType.ConsolidatedTemplateParameters);
                        //dup.DuplicateFor[oldSelfInstanceType.UniqueKey] = newSelfInstanceType;
                        // specialize duplicated type
                        var specializer = new Specializer(targetType.DeclaringModule, originalTemplateParameters, dupTPs);
                        
                        specializer.VisitTypeParameterList(dupTPs); // for constraints etc.
                        specializer.VisitTypeNode(duplicatedNestedType);

                        var bodySpecializer = new MethodBodySpecializer(targetType.DeclaringModule,
                            originalTemplateParameters, dupTPs);

                        bodySpecializer.Visit(duplicatedNestedType);
                        
                        // after copying the closure class, clear the self specialization forwarding
                        //  OriginalDecl<X,Y,Z>.NestedType<A,B,C> -> WrapperType<U,V,W>.NewNestedType<X,Y,Z,A,B,C>
                        //dup.DuplicateFor[oldSelfInstanceType.UniqueKey] = null;
                    }
                    else
                    {
                        dup.SafeAddMember(targetType, duplicatedNestedType, mem);
                    }
                }
                else
                {
                    // already copied type previously
                    dup.DuplicateFor[nestedType.UniqueKey] = duplicatedNestedType;
#if false
        if (nestedType.ConsolidatedTemplateArguments != null)
        {
            // populate the self specialization forwarding
            //  NestedType<Self1,Self2> -> NewNestedType<NewSelf1,NewSelf2>
            var origSelfInstantiation = nestedType.DeclaringType.GetTemplateInstance(nestedType, nestedType.DeclaringType.TemplateParameters).GetNestedType(nestedType.Name);
            var newSelfInstantiation = duplicatedNestedType.GetGenericTemplateInstance(targetModule, duplicatedNestedType.ConsolidatedTemplateParameters);
            dup.DuplicateFor[origSelfInstantiation.UniqueKey] = newSelfInstantiation;
            // Also forward ContractType<A,B>.NestedType instantiated at target ContractType<X,Y>.NestedType to
            // TargetType<X,Y,Z>.NewNestedType<X,Y>, since this reference may appear in the contract itself.
            var consolidatedContractTemplateArguments = sourceMethod.DeclaringType.ConsolidatedTemplateArguments;
            var instantiatedNestedOriginal = nestedType.DeclaringType.GetGenericTemplateInstance(targetModule, consolidatedContractTemplateArguments).GetNestedType(nestedType.Name);
            dup.DuplicateFor[instantiatedNestedOriginal.UniqueKey] = duplicatedNestedType.GetTemplateInstance(targetType, consolidatedContractTemplateArguments);
        }
        else
        {
            Debugger.Break();
        }
#endif
                }
            }
            else if (closureInstanceMethod != null && closureMethodTemplate != null &&
                     !fmd.MembersToDuplicate.Contains(closureMethodTemplate.DeclaringType))
            {
                Method closureMethod = closureMethodTemplate;

                Debug.Assert(closureMethod.Template == null);
                
                //var m = FindExistingClosureMethod(targetType, closureMethod);
                Method m = null;
                
                // why did we ever try to find an existing one? This can capture a completely unrelated closure that happens to match by name.
                if (m == null)
                {
                    Method dupMethod = dup.Visit(closureMethod) as Method;
                    TypeNodeList actuals;

                    TransformOriginalConsolidatedTypeFormalsIntoMethodFormals(dupMethod, closureMethod,
                        closureInstanceMethod, out actuals);

                    // now setup a forwarding from the closureInstanceMethod to the new instance
                    Method newInstance = dupMethod.GetTemplateInstance(dupMethod.DeclaringType, actuals);
                    newInstance.Name = dupMethod.Name;

                    dup.DuplicateFor[closureInstanceMethod.UniqueKey] = newInstance;

                    dup.SafeAddMember(targetType, dupMethod, closureMethod);

                    // special case when resulting method is generic and instance, then we need to make "this" a parameter
                    // and the method static, otherwise, there's a type mismatch between the "this" and the explicitly generic parameter types used
                    // in arguments.
                    var originalParentTemplateParameters = closureMethod.DeclaringType.ConsolidatedTemplateParameters;
                    if (!dupMethod.IsStatic && originalParentTemplateParameters != null && originalParentTemplateParameters.Count > 0)
                    {
                        var oldThis = dupMethod.ThisParameter;
                        oldThis.Type = dup.PossiblyRemapContractClassToInterface(oldThis.Type);
                        
                        dupMethod.Flags |= MethodFlags.Static;
                        dupMethod.CallingConvention &= ~CallingConventionFlags.HasThis;
                        
                        var oldParameters = dupMethod.Parameters;
                        
                        dupMethod.Parameters = new ParameterList(oldParameters.Count + 1);
                        dupMethod.Parameters.Add(oldThis); // make explicit
                        
                        for (int i = 0; i < oldParameters.Count; i++)
                        {
                            dupMethod.Parameters.Add(oldParameters[i]);
                        }

                        // now need to specialize transforming original parameters into first n method template parameters
                        var targetTypeParameters = new TypeNodeList(originalParentTemplateParameters.Count);
                        for (int i = 0; i < originalParentTemplateParameters.Count; i++)
                        {
                            targetTypeParameters.Add(dupMethod.TemplateParameters[i]);
                        }

                        var specializer = new Specializer(targetType.DeclaringModule, originalParentTemplateParameters, targetTypeParameters);
                        specializer.VisitMethod(dupMethod);

                        var bodySpecializer = new MethodBodySpecializer(targetType.DeclaringModule,
                            originalParentTemplateParameters, targetTypeParameters);

                        bodySpecializer.VisitMethod(dupMethod);
                    }
                }
            }
            else if (closureInstanceMethod != null)
            {
                var m = FindExistingClosureMethod(targetType, closureInstanceMethod);
                if (m == null)
                {
                    Member duplicatedMember = dup.Visit(mem) as Member;
                    dup.SafeAddMember(targetType, duplicatedMember, mem);
                }
            }
            else
            {
                Member duplicatedMember = dup.Visit(mem) as Member;
                dup.SafeAddMember(targetType, duplicatedMember, mem);
            }
        }

        /// <summary>
        /// If source type is insided target type, only grab the type parameters up to the target type. Otherwise, grab consolidated.
        /// </summary>
        private static TypeNodeList FindNonStandardTypeParametersToBeDuplicated(FindClosurePartsToDuplicate fmd, TypeNode sourceType, TypeNode targetType)
        {
            Debug.Assert(TypeNode.IsCompleteTemplate(sourceType));
            Debug.Assert(TypeNode.IsCompleteTemplate(targetType));

            TypeNodeList result = null;
            if (sourceType.DeclaringType != null)
            {
                if (fmd.MembersToDuplicate.Contains(sourceType.DeclaringType))
                {
                    Debug.Assert(false);
                    return null;
                }

                if (sourceType.DeclaringType == targetType) return null;

                if (IsInsideOf(sourceType, targetType))
                {
                    // difficult case. Grab consolidated type parameters, except the ones up from the target type
                    var sourceConsolidated = sourceType.ConsolidatedTemplateParameters;
                    if (sourceConsolidated == null || sourceConsolidated.Count == 0) return null;

                    var targetConsolidated = targetType.ConsolidatedTemplateParameters;
                    if (targetConsolidated == null || targetConsolidated.Count == 0) return sourceConsolidated;
                    
                    if (sourceConsolidated.Count == targetConsolidated.Count) return null; // no extra type parameters

                    result = new TypeNodeList(sourceConsolidated.Count - targetConsolidated.Count);
                    for (int i = 0; i < sourceConsolidated.Count; i++)
                    {
                        if (i < targetConsolidated.Count) continue;
                        result.Add(sourceConsolidated[i]);
                        
                        return result;
                    }
                }
                else
                {
                    // For Roslyn-based closures we need to combine all the types from source and target types together.
                    if (sourceType.IsRoslynBasedStaticClosure())
                    {
                        var sourceConsolidated = sourceType.ConsolidatedTemplateParameters;
                        if (sourceConsolidated == null || sourceConsolidated.Count == 0) return null;
                        
                        var targetConsolidated = targetType.ConsolidatedTemplateParameters;
                        
                        if (targetConsolidated == null || targetConsolidated.Count == 0) return sourceConsolidated;
                        
                        if (sourceConsolidated.Count == targetConsolidated.Count)
                            return null; // no extra type parameters

                        result = new TypeNodeList(sourceConsolidated.Count + targetConsolidated.Count);

                        for (int i = 0; i < targetConsolidated.Count; i++)
                        {
                            result.Add(targetConsolidated[i]);
                        }

                        for (int i = 0; i < sourceConsolidated.Count; i++)
                        {
                            result.Add(sourceConsolidated[i]);
                        }

                        return result;
                    }

                    result = sourceType.ConsolidatedTemplateParameters;
                }
            }

            return result;
        }

        private static bool GenericityAgrees(TypeNode t1, TypeNode t2)
        {
            if (t1.IsGeneric != t2.IsGeneric) return false;

            var tcount1 = t1.TemplateParameters == null ? 0 : t1.TemplateParameters.Count;
            var tcount2 = t2.TemplateParameters == null ? 0 : t2.TemplateParameters.Count;
            
            return tcount1 == tcount2;
        }

        private static TypeNode FindExistingClosureType(TypeNode targetType, TypeNode nestedType)
        {
            var name = CopiedMemberName(nestedType);

            for (int i = 0; i < targetType.Members.Count; i++)
            {
                TypeNode candidate = targetType.Members[i] as TypeNode;
                if (candidate == null) continue;

                var candidateName = NameWithoutGenericParameterCountSuffix(candidate);
                if (candidateName == name && GenericityAgrees(nestedType, candidate))
                {
                    return candidate;
                }
            }

            return null;
        }

        private static Method FindExistingClosureMethod(TypeNode targetType, Method method)
        {
            var m = targetType.GetExactMatchingMethod(method);

            return m;
        }

        private static void TransformOriginalConsolidatedTypeFormalsIntoMethodFormals(Method dupMethod,
            Method closureMethod, Method closureInstanceMethod, out TypeNodeList actuals)
        {
            // make sure that if we copy it from a generic context, the method becomes generic in the target context
            // if we are copying from the same declaring type (not instantiated), then don't add enclosing type parameters.
            var originals = closureInstanceMethod.DeclaringType.ConsolidatedTemplateArguments == null
                ? null
                : closureMethod.DeclaringType.ConsolidatedTemplateParameters;

            if (originals != null)
            {
                originals = originals.Clone();
            }
            
            if (closureMethod.TemplateParameters != null && closureMethod.TemplateParameters.Count > 0)
            {
                if (originals == null)
                {
                    originals = closureMethod.TemplateParameters.Clone();
                }
                else
                {
                    foreach (var tp in closureMethod.TemplateParameters)
                    {
                        originals.Add(tp);
                    }
                }
            }

            if (originals == null)
            {
                actuals = null;
                return;
            }

            actuals = closureInstanceMethod.DeclaringType.ConsolidatedTemplateArguments == null
                ? null
                : closureInstanceMethod.DeclaringType.ConsolidatedTemplateArguments.Clone();
            
            if (closureInstanceMethod.TemplateArguments != null && closureInstanceMethod.TemplateArguments.Count > 0)
            {
                if (actuals == null)
                {
                    actuals = closureInstanceMethod.TemplateArguments.Clone();
                }
                else
                {
                    foreach (var tp in closureInstanceMethod.TemplateArguments)
                    {
                        actuals.Add(tp);
                    }
                }
            }

            var declaringModule = dupMethod.DeclaringType.DeclaringModule;

            // new method formals
            var tparams = originals.Clone();

            for (int i = 0; i < originals.Count; i++)
            {
                // setup forwarding of tparams to method params
                ITypeParameter tp = originals[i] as ITypeParameter;
                
                TypeNode mtp = NewEqualMethodTypeParameter(tp, dupMethod, i);
                
                tparams[i] = mtp;
            }

            var specializer = new Specializer(declaringModule, originals, tparams);
            
            // System.Console.WriteLine("Made {0} a generic method", dupMethod.FullName);
            dupMethod.TemplateParameters = tparams;
            dupMethod.IsGeneric = true;

            specializer.VisitMethod(dupMethod);
            
            var bodySpecializer = new MethodBodySpecializer(declaringModule, originals, tparams);
            
            bodySpecializer.CurrentType = dupMethod.DeclaringType;
            bodySpecializer.CurrentMethod = dupMethod;
            bodySpecializer.VisitBlock(dupMethod.Body);
        }

        private static TypeNodeList DuplicateTypeParameterList(TypeNodeList typeParameters, int offsetIndex, TypeNode declaringType)
        {
            if (typeParameters == null) return null;

            Duplicator dup = new Duplicator(declaringType.DeclaringModule, null);
            dup.FindTypesToBeDuplicated(typeParameters);
            
            TypeNodeList result = dup.VisitTypeParameterList(typeParameters);
            
            for (int i = 0; i < result.Count; i++)
            {
                TypeNode tn = result[i];
                
                tn.DeclaringType = declaringType;
                tn.DeclaringModule = declaringType.DeclaringModule;

                ITypeParameter tp = (ITypeParameter) tn;
                
                tp.ParameterListIndex = offsetIndex + i;
                tp.DeclaringMember = declaringType;
            }

            return result;
        }

        private static TypeNode NewEqualMethodTypeParameter(ITypeParameter itp, Method declaringMethod, int index)
        {
            TypeNode declaringType = declaringMethod.DeclaringType;
            ClassParameter cp = itp as ClassParameter;
            if (cp != null)
            {
                MethodClassParameter mcp = new MethodClassParameter();

                mcp.Interfaces = cp.Interfaces.Clone();
                mcp.BaseClass = cp.BaseClass;
                
                mcp.TypeParameterFlags = cp.TypeParameterFlags & ~TypeParameterFlags.VarianceMask;
                mcp.DeclaringType = declaringType;
                mcp.DeclaringModule = declaringType.DeclaringModule;
                
                mcp.Name = cp.Name;
                mcp.ParameterListIndex = index;
                mcp.DeclaringMember = declaringMethod;

                return mcp;
            }

            TypeParameter tp = itp as TypeParameter;
            if (tp != null)
            {
                MethodTypeParameter mp = new MethodTypeParameter();

                mp.Interfaces = tp.Interfaces.Clone();
                
                mp.TypeParameterFlags = tp.TypeParameterFlags & ~TypeParameterFlags.VarianceMask;
                
                mp.DeclaringType = declaringType;
                mp.DeclaringModule = declaringType.DeclaringModule;
                
                mp.Name = tp.Name;
                mp.ParameterListIndex = index;
                mp.DeclaringMember = declaringMethod;

                return mp;
            }

            throw new NotImplementedException("unexpected type parameter kind");
        }

        public static TypeNode NewEqualTypeParameter(Duplicator dup, ITypeParameter itp, TypeNode declaringType, int index)
        {
            ClassParameter cp = itp as ClassParameter;
            if (cp != null)
            {
                ClassParameter mcp = new ClassParameter();
                
                mcp.Interfaces = dup.VisitInterfaceReferenceList(cp.Interfaces);
                mcp.BaseClass = cp.BaseClass;
                
                mcp.TypeParameterFlags = cp.TypeParameterFlags & ~TypeParameterFlags.VarianceMask;
                mcp.DeclaringType = declaringType;
                mcp.DeclaringModule = declaringType.DeclaringModule;
                
                mcp.Name = cp.Name;
                mcp.ParameterListIndex = index;
                mcp.DeclaringMember = declaringType;

                return mcp;
            }

            TypeParameter tp = itp as TypeParameter;
            if (tp != null)
            {
                TypeParameter mp = new TypeParameter();

                mp.Interfaces = dup.VisitInterfaceReferenceList(tp.Interfaces);
                mp.TypeParameterFlags = tp.TypeParameterFlags & ~TypeParameterFlags.VarianceMask;
                
                mp.DeclaringType = declaringType;
                mp.DeclaringModule = declaringType.DeclaringModule;
                
                mp.Name = tp.Name;
                mp.ParameterListIndex = index;
                mp.DeclaringMember = declaringType;

                return mp;
            }

            throw new NotImplementedException("unexpected type parameter kind");
        }

        /// <summary>
        /// Renames duplicatedMember name if clashing (and it is a type)
        /// </summary>
        internal static void SafeAddMember(TypeNode targetType, Member duplicatedMember, Member original)
        {
            Debug.Assert(targetType.Template == null);
            
            if (targetType.Members.Contains(duplicatedMember)) return;

            // System.Console.WriteLine("Adding duped member {0} from {1}", duplicatedMember.FullName, original.FullName);
            var newName = CopiedMemberName(original);
            var newVersionedName = NextUnusedMemberName(targetType, newName);
            
            duplicatedMember.Name = Identifier.For(newVersionedName);
            duplicatedMember.EnsureMangledName();
            
            targetType.Members.Add(duplicatedMember);
        }

        public static string NextUnusedMemberName(TypeNode targetType, string memberName)
        {
            var version = 0;
            var members = targetType.Members;
            for (int i = 0; i < members.Count; i++)
            {
                var mem = members[i];
                if (mem == null) continue;

                if (mem.Name.Name.StartsWith(memberName))
                {
                    var candidateName = NameWithoutGenericParameterCountSuffix(mem);
                    var lastIndexOfUnder = candidateName.LastIndexOf('_');

                    if (lastIndexOfUnder == memberName.Length)
                    {
                        try
                        {
                            int used;
                            if (Int32.TryParse(candidateName.Substring(memberName.Length + 1), out used))
                            {
                                if (used >= version)
                                {
                                    version = used + 1;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return memberName + "_" + version.ToString();
        }

        public static string NameWithoutGenericParameterCountSuffix(Member member)
        {
            var type = member as TypeNode;
            var name = member.Name.Name;
            if (type == null) return name;

            if (type.IsGeneric && type.TemplateParameters != null)
            {
                var tcount = type.TemplateParameters.Count;
                
                if (tcount == 0) return name;

                var lastTick = name.LastIndexOf(TargetPlatform.GenericTypeNamesMangleChar);
                if (lastTick <= 0)
                {
                    return name; // something is wrong?
                }

                int acount;
                if (Int32.TryParse(name.Substring(lastTick + 1), out acount))
                {
                    if (acount == tcount)
                    {
                        return name.Substring(0, lastTick);
                    }
                }

                // something is wrong?
            }

            return name;
        }

        public static string CopiedMemberName(Member original)
        {
            var name = NameWithoutGenericParameterCountSuffix(original);

            return original.DeclaringType.Name.Name.Replace('`', '_') + "_" + name.Replace('`', '_');
        }

        /// <summary>
        /// Returns the method implemented or overridden by the contractClass corresponding to the abstract method.
        ///
        /// Can't use TypeNode.GetImplementingMethod because the Reader creates the names of explicit
        /// interface methods such that it doesn't return them: it returns only implicit interface
        /// implementations. A contract class should never have both implicit and explicit
        /// implementations, so this just looks for an implicit first, then an explicit if that fails.
        /// </summary>
        /// <param name="contractClass">A class holding the contracts for an interface.</param>
        /// <param name="interfaceMethod">A method from that interface.</param>
        /// <returns>The method in the class that implements the interface method.  </returns>
        internal static Method GetImplementation(TypeNode contractClass, Method abstractMethod)
        {
            Method m = contractClass.FindShadow(abstractMethod);
            
            if (m != null) // implicit implementation
                return m;

            if (abstractMethod.DeclaringType is Interface)
            {
                foreach (Member mem in contractClass.Members)
                {
                    m = mem as Method;
                    if (m == null || m.ImplementedInterfaceMethods == null) continue;

                    foreach (Method ifaceMethod in m.ImplementedInterfaceMethods)
                    {
                        if (ifaceMethod == null) continue;

                        if (ifaceMethod == abstractMethod || ifaceMethod.Template == abstractMethod)
                            return m; // explicit implementation
                        
                        if (ifaceMethod.Template != null && ifaceMethod.Template == abstractMethod.Template)
                        {
                            return m;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Searches a list of Blocks (actually a clump) for the first statement
        /// that satisfies the supplied predicate.
        /// </summary>
        /// <param name="clump">The list of basic blocks to search</param>
        /// <param name="test">A predicate that is to be applied to each statement</param>
        /// <param name="blockIndex">Negative one if false is returned, the index of the block
        /// containing the first statement to satisfy the predicate
        /// if true is returned
        /// </param>
        /// <param name="statementIndex">Negative one if false is returned, the index of the
        /// block containing the first statement to satisfy the predicate
        /// if true is returned
        /// </param>
        /// <returns>true iff test succeeds on a statement</returns>
        internal static bool SearchClump(StatementList clump, System.Predicate<Statement> test, out int blockIndex, out int statementIndex)
        {
            blockIndex = -1;
            statementIndex = -1;

            if (!IsClump(clump)) return false;
            
            for (int i = 0, n = clump.Count; i < n; i++)
            {
                Block b = clump[i] as Block;
                if (b == null) continue;

                for (int j = 0, m = b.Statements == null ? 0 : b.Statements.Count; j < m; j++)
                {
                    Statement s = b.Statements[j];
                    
                    if (test(s))
                    {
                        blockIndex = i;
                        statementIndex = j;
                        
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Extracts a continuous set of statements and blocks from a set of blocks.
        /// 
        /// The first block in the result is always fresh as the original may be the branch target of unextracted code and
        /// needs to remain in the original blocks.
        /// 
        /// The last block is possibly fresh in the result, unless it is extracted whole.
        /// All blocks in between are just re-used and their references in the original block list are nulled.
        /// 
        /// Any branches to the last block are adjusted to the new last block if it becomes fresh.
        /// </summary>
        /// <param name="blocks">A list of blocks such as a method body to extract from. This list is modified by this method: all
        /// extracted blocks are nulled out.</param>
        /// <param name="firstBlockIndex">First block index.</param>
        /// <param name="firstStmtIndex">First statement index.</param>
        /// <param name="lastBlockIndex">Last block index.</param>
        /// <param name="lastStmtIndex">Last statement index.</param>
        /// <returns>A set of statements and blocks that were extracted.</returns>
        [ContractVerification(false)]
        internal static StatementList ExtractClump(StatementList blocks, int firstBlockIndex, int firstStmtIndex,
            int lastBlockIndex, int lastStmtIndex, AssumeBlock assumeBlock = null)
        {
            Contract.Requires(blocks != null);
            Contract.Requires(firstBlockIndex >= 0);
            Contract.Requires(firstStmtIndex >= 0);

            Contract.Ensures(Contract.Result<StatementList>().Count == lastBlockIndex - firstBlockIndex + 1);
            Contract.Ensures(blocks[firstBlockIndex] == Contract.OldValue(blocks[firstBlockIndex]));

            StatementList clump = new StatementList();

            // first extract the tail of the first block into a new block.
            Block oldFirstBlock = (Block) blocks[firstBlockIndex];
            Block newFirstBlock = new Block(new StatementList());
            
            if (oldFirstBlock != null)
            {
                var count = firstBlockIndex == lastBlockIndex ? lastStmtIndex + 1 : oldFirstBlock.Statements.Count;
                for (int stmtIndex = firstStmtIndex; stmtIndex < count; stmtIndex++)
                {
                    var stmt = oldFirstBlock.Statements[stmtIndex];
                    newFirstBlock.Statements.Add(stmt);
                    
                    if (stmt == null) continue;

                    oldFirstBlock.Statements[stmtIndex] = assumeBlock;
                    assumeBlock = null;
                }
            }

            clump.Add(newFirstBlock);
            
            var currentBlockIndex = firstBlockIndex + 1;
            
            if (currentBlockIndex > lastBlockIndex) return clump;

            // setup info about forwarding branches to new last full block
            Block newLastBlock = null;

            var lastFullBlock = lastBlockIndex - 1;
            var oldLastBlock = (Block) blocks[lastBlockIndex];
            
            if (oldLastBlock != null && lastStmtIndex == oldLastBlock.Statements.Count - 1)
            {
                // last block is also fully used.
                lastFullBlock = lastBlockIndex;
            }
            else
            {
                newLastBlock = new Block(new StatementList());

                // check if first block had a branch
                if (newFirstBlock != null && newFirstBlock.Statements != null && newFirstBlock.Statements.Count > 0)
                {
                    // check if we need to adjust branch to last block
                    var branch = newFirstBlock.Statements[newFirstBlock.Statements.Count - 1] as Branch;
                    if (branch != null && branch.Target != null && branch.Target.UniqueKey == oldLastBlock.UniqueKey)
                    {
                        branch.Target = newLastBlock;
                    }
                }
            }

            // Next extract full blocks between currentBlockIndex and including lastFullBlock
            for (; currentBlockIndex <= lastFullBlock; currentBlockIndex++)
            {
                var block = (Block) blocks[currentBlockIndex];

                // don't skip null blocks since context relies on number
                clump.Add(block);

                if (block == null) continue;
                
                blocks[currentBlockIndex] = assumeBlock;
                assumeBlock = null;
                
                if (newLastBlock != null && block.Statements != null && block.Statements.Count > 0)
                {
                    // check if we need to adjust branch to last block
                    var branch = block.Statements[block.Statements.Count - 1] as Branch;
                    if (branch != null && branch.Target != null && branch.Target.UniqueKey == oldLastBlock.UniqueKey)
                    {
                        branch.Target = newLastBlock;
                    }
                }
            }

            // next, if last block wasn't full, we have a new last block, extract the prefix
            if (newLastBlock != null)
            {
                for (int i = 0; i < lastStmtIndex + 1; i++)
                {
                    newLastBlock.Statements.Add(oldLastBlock.Statements[i]);
                    oldLastBlock.Statements[i] = assumeBlock;
                    assumeBlock = null;
                }

                clump.Add(newLastBlock);
            }

            return clump;
        }

        /// <summary>
        /// Tries to find the last statement in the clump that has a valid source context.
        /// </summary>
        /// <param name="clump">The clump to search in</param>
        /// <param name="sctx">The last source context in clump</param>
        /// <returns>true iff there exists a non-zero source context in clump</returns>
        internal static bool GetLastSourceContext(StatementList clump, out SourceContext sctx)
        {
            sctx = new SourceContext();
            
            bool found = false;
            
            if (clump == null || (!(0 < clump.Count))) return found;
            
            for (int i = clump.Count - 1; 0 <= i && !found; i--)
            {
                Block b = clump[i] as Block;
                if (b == null) continue;
                
                for (int j = b.Statements == null ? 0 : b.Statements.Count - 1; 0 <= j; j--)
                {
                    Statement s = b.Statements[j];
                    if (s == null) continue;
                    
                    if (s.NodeType == NodeType.Nop) continue; // skip curlys
                    
                    sctx = s.SourceContext;
                    
                    if (sctx.IsValid)
                    {
                        found = true;
                        break;
                    }
                }
            }

            return found;
        }

        public static TypeNode IsContractTypeForSomeOtherTypeUnspecialized(TypeNode t, ContractNodes contractNodes)
        {
            TypeNode unspecializedType = GetTypeFromAttribute(t, ContractNodes.ContractClassForAttributeName);
            
            return unspecializedType;
        }

        public static TypeNode IsContractTypeForSomeOtherType(TypeNode t, ContractNodes contractNodes)
        {
            var unspecializedType = IsContractTypeForSomeOtherTypeUnspecialized(t, contractNodes);

            if (unspecializedType == null) return null;
            
            // else go search t's list of interfaces/base classes to find the specialized type
            // (which might be specialized only with a type parameter, but that's better than
            // returning the unspecialized one from the attribute, because the unspecialized one
            // will never be pointer equal to any specialized type.
            for (int i = 0, n = t.Interfaces == null ? 0 : t.Interfaces.Count; i < n; i++)
            {
                if (unspecializedType == t.Interfaces[i] || unspecializedType == t.Interfaces[i].Template)
                    return t.Interfaces[i];
            }

            if (unspecializedType == t.BaseType || unspecializedType == t.BaseType.Template) return t.BaseType;
            
            return null;
        }

        public static bool IsContractTypeForSomeOtherType(Class Class)
        {
            if (Class == null) return false;

            if (Class.Attributes == null) return false;
            
            for (int i = 0; i < Class.Attributes.Count; i++)
            {
                var attr = Class.Attributes[i];
                
                if (attr == null) continue;
                
                if (attr.Type == null) continue;
                
                if (attr.Type.Name == null) continue;
                
                if (attr.Type.Name.Name == "ContractClassForAttribute") return true;
            }

            return false;
        }

        /// <summary>
        /// Should only be called on setters of auto properties
        /// To find the corresponding field, we should go to the template (if any) of the setter,
        /// look at its body and find the field being stored into. Then we have to find the corresponding instance
        /// on the Setter's declaring type by name. 
        /// </summary>
        /// <param name="setter">Instantiated method for which we are looking for the corresponding field instance</param>
        internal static Member GetBackingField(Method setter)
        {
            var template = setter;
            if (template.Template != null) template = template.Template;
            
            var f = BackingFieldFinder.BackingField(template);
            
            if (f == null) throw new Exception("backing field not found");
            
            return setter.DeclaringType.GetField(f.Name);
        }

        public static Field TryGetBackingField(Method setter)
        {
            var template = setter;
            if (template.Template != null) template = template.Template;
            
            var f = BackingFieldFinder.BackingField(template);
            
            if (f == null) return null;
            
            return setter.DeclaringType.GetField(f.Name);
        }

        private class BackingFieldFinder : Inspector
        {
            private Field Found;

            public static Field BackingField(Method setter)
            {
                var v = new BackingFieldFinder();
                
                v.Visit(setter.Body);
                
                return v.Found;
            }

            public override void VisitMemberBinding(MemberBinding memberBinding)
            {
                Field f = memberBinding.BoundMember as Field;
                if (f != null)
                {
                    this.Found = f;
                    return;
                }

                base.VisitMemberBinding(memberBinding);
            }
        }

        public static TypeNode Unspecialize(TypeNode type)
        {
            Contract.Requires(type != null);
            Contract.Ensures(Contract.Result<TypeNode>() != null);

            while (type.Template != null)
            {
                type = type.Template;
            }

            return type;
        }

        public static Method Unspecialize(Method method)
        {
            // F:
            Contract.Ensures(method == null || Contract.Result<Method>() != null);

            if (method == null) return null;

            while (method.Template != null)
            {
                method = method.Template;
            }

            return method;
        }

        [ContractVerification(true)]
        [Pure]
        public static Member Unspecialize(Member member)
        {
            Contract.Requires(member != null);
            Contract.Ensures(Contract.Result<Member>() != null);

            if (member.DeclaringType == null)
            {
                return Unspecialize((TypeNode) member);
            }

            var template = member.DeclaringType.Template;
            if (template == null) return member;

            while (template.Template != null) template = template.Template;
            
            MemberList specializedMembers = member.DeclaringType.Members;
            MemberList unspecializedMembers = template.Members;
            
            for (int i = 0, n = specializedMembers.Count; i < n; i++)
            {
                if (specializedMembers[i] != member) continue;
                
                var unspecializedMember = unspecializedMembers[i];
                
                if (unspecializedMember == null)
                {
                    Contract.Assume(false, "Should find unspeced member here");
                    unspecializedMember = member;
                }

                return unspecializedMember;
            }

            Contract.Assume(false, "Couldn't find unspecialized member");
            
            return member;
        }

        internal static bool IsAnonymousDelegate(Method method, TypeNode referringMethodDeclaringType)
        {
            if (!IsCompilerGenerated(method)) return false;

            if (IsClosureType(referringMethodDeclaringType, method.DeclaringType)) return true;
            
            // if static/instance there might be no closure
            
            if (method.DeclaringType == referringMethodDeclaringType) return true;
            
            return false;
        }

        /// <summary>
        /// Use this instead of SystemTypes.Void in case we are dealing with alternative corlibs
        /// </summary>
        public static bool IsVoidType(TypeNode type)
        {
            return IsType(type, SystemTypes.Void);
        }

        /// <summary>
        /// Use this instead of SystemTypes.Void in case we are dealing with alternative corlibs
        /// </summary>
        internal static bool IsExceptionType(TypeNode type)
        {
            return IsType(type, SystemTypes.Exception);
        }

        internal static bool DerivesFromException(TypeNode type)
        {
            var current = type;
            while (current != null)
            {
                while (current.Template != null) current = current.Template;
                
                if (IsExceptionType(current)) return true;
                
                current = current.BaseType;
            }

            return false;
        }

        internal static bool IsType(TypeNode type, TypeNode expected)
        {
            if (type == expected) return true;

            if (type == null) return false;
            
            if (expected == null) return false;

            if (TemplateParameterCount(type) == TemplateParameterCount(expected) &&
                type.Namespace.Matches(expected.Namespace) && type.Name.Matches(expected.Name))
            {
                return true;
            }
            
            return false;
        }

        private static int TemplateParameterCount(TypeNode type)
        {
            if (type == null) return 0;
            
            if (type.TemplateParameters == null) return 0;
            
            return type.TemplateParameters.Count;
        }

        [Pure]
        internal static bool IsNonTrivial(Block block)
        {
            if (block == null) return false;

            if (block.Statements == null) return false;
            
            foreach (var s in block.Statements)
            {
                if (IsNonTrivial(s)) return true;
            }
            
            return false;
        }

        [Pure]
        private static bool IsNonTrivial(Statement s)
        {
            if (s == null) return false;

            Block b = s as Block;
            
            if (b != null) return (IsNonTrivial(b));
            
            if (s.NodeType == NodeType.Nop) return false;
            
            return true;
        }

        /// <summary>
        /// Returns the contract class for the given interface/abstract type with the proper
        /// instantiation.
        /// </summary>
        private static TypeNode GetContractClass(TypeNode instantiatedType)
        {
            // F:
            Contract.Requires(instantiatedType != null);

            if (instantiatedType.Template != null)
            {
                var genericType = instantiatedType.Template;
                var contractClass = GetTypeFromAttribute(genericType, ContractNodes.ContractClassAttributeName) as Class;
                
                if (contractClass == null) return null;

                // instantiate
                Debug.Assert(contractClass.ConsolidatedTemplateParameters != null &&
                             contractClass.ConsolidatedTemplateParameters.Count ==
                             instantiatedType.ConsolidatedTemplateArguments.Count);
                
                TypeNode instantiatedContractClass =
                    contractClass.GetConsolidatedTemplateInstance(instantiatedType.DeclaringModule, null,
                        contractClass.DeclaringType, instantiatedType.TemplateArguments,
                        instantiatedType.ConsolidatedTemplateArguments);
                
                return instantiatedContractClass;
            }
            else
            {
                var contractClass =
                    GetTypeFromAttribute(instantiatedType, ContractNodes.ContractClassAttributeName) as Class;
                
                if (contractClass == null) return null;

                return contractClass;
            }
        }

        /// <summary>
        /// Returns the properly instantiated method with contract corresponding to the given method.
        /// If the method is an interface method or abstract method, we look for the contract on an out-of band method
        /// Otherwise, we return the method itself if it has contracts.
        /// In all cases, if there are no contracts, we return null.
        /// </summary>
        public static Method GetContractMethod(Method instantiatedMethod)
        {
            if (instantiatedMethod == null) return null;
            
            Method candidate = instantiatedMethod;
            
            if (candidate.IsAbstract)
            {
                TypeNode contractClass = GetContractClass(instantiatedMethod.DeclaringType);
                if (contractClass != null)
                {
                    candidate = GetImplementation(contractClass, instantiatedMethod);
                }
            }
            
            if (candidate != null)
            {
                if (candidate.Contract != null && candidate.Contract.HasAssertions)
                {
                    return candidate;
                }
            }

            return null;
        }
    }
}
