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
using System.Diagnostics; // needed for Debug.Assert (etc.)
using System.Runtime.Serialization; // needed for defining exception .ctors
using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    public static class RewriteHelper
    {
        internal static readonly TypeNode flagsAttributeNode = TypeNode.GetTypeNode(typeof(System.FlagsAttribute));


        internal static Block CreateTryFinallyBlock(Method method, Block tryBody, Block finallyBody)
        {
          Contract.Requires(method != null);
          Contract.Requires(tryBody != null);
          Contract.Requires(finallyBody != null);

            if (method.ExceptionHandlers == null) method.ExceptionHandlers = new ExceptionHandlerList();

            Block result = new Block(new StatementList());
            Block afterFinally = new Block(new StatementList());

            tryBody.Statements.Add(new Branch(null, afterFinally, false, true, true));
            finallyBody.Statements.Add(new EndFinally());

            result.Statements.Add(tryBody);
            result.Statements.Add(finallyBody);
            result.Statements.Add(afterFinally);

            ExceptionHandler fb = new ExceptionHandler();
            fb.TryStartBlock = tryBody;
            fb.BlockAfterTryEnd = finallyBody;
            fb.HandlerStartBlock = finallyBody;
            fb.BlockAfterHandlerEnd = afterFinally;
            fb.HandlerType = NodeType.Finally;
            method.ExceptionHandlers.Add(fb);

            return result;
        }

        internal static Statement GenerateForLoop(Variable loopVariable, Expression lowerBound, Expression upperBound, Statement body)
        {
            Block bodyAsBlock = body as Block ?? new Block(new StatementList(body));
            Block init = new Block(new StatementList(2));
            Block increment = new Block(new StatementList(1));
            Block test = new Block(new StatementList(new Branch(
              new BinaryExpression(loopVariable, upperBound, NodeType.Lt), bodyAsBlock))); //, false, true, false)));

            init.Statements.Add(new AssignmentStatement(loopVariable, lowerBound));
            init.Statements.Add(new Branch(null, test));

            increment.Statements.Add(new AssignmentStatement(loopVariable, new BinaryExpression(loopVariable, Literal.Int32One, NodeType.Add)));

            Block forLoop = new Block(new StatementList(4));
            forLoop.Statements.Add(init);
            forLoop.Statements.Add(bodyAsBlock);
            forLoop.Statements.Add(increment);
            forLoop.Statements.Add(test);
            return forLoop;
        }


        internal static bool Implements(Method concreteMethod, Method abstractMethod)
        {
          Contract.Requires(concreteMethod != null);

            MethodList ml = concreteMethod.ImplicitlyImplementedInterfaceMethods;
            for (int i = 0, n = ml == null ? 0 : ml.Count; i < n; i++)
            {
                Method ifaceMethod = ml[i];
                if (ifaceMethod == null) continue;
                if (ifaceMethod == abstractMethod) return true;
            }
            ml = concreteMethod.ImplementedInterfaceMethods;
            for (int i = 0, n = ml == null ? 0 : ml.Count; i < n; i++)
            {
                Method ifaceMethod = ml[i];
                if (ifaceMethod == null) continue;
                if (ifaceMethod == abstractMethod) return true;
            }
            return false;
        }
        internal static bool IsFinalizer(Method method)
        {
          Contract.Requires(method != null);

            return method.Name.Name == "Finalize" && method.Parameters.Count == 0 && HelperMethods.IsVoidType(method.ReturnType)
              && method.IsVirtualAndNotDeclaredInStruct
              && ((method.Flags & MethodFlags.NewSlot) == 0);
        }


#if DEBUG
        /// <summary>
        /// Used to determine if a method body is just a sequence of non-nested blocks. Ignores any PreambleBlocks at the beginning of the body
        /// </summary>
        /// <param name="method">Any method</param>
        /// <returns>method != null &amp;&amp; method.Body != null ==> IsClump(method.Body.Statements) (except for PreambleBlocks)
        /// </returns>
        internal static bool PostNormalizedFormat(Method method)
        {
            if (method == null) return true;
            if (method.Body == null) return true;
            // skip over any PreambleBlocks, they don't need to be in post-normalized format
            int i = 0;
            StatementList sl = method.Body.Statements;
            while (i < sl.Count)
            {
                PreambleBlock pb = sl[i] as PreambleBlock;
                if (pb == null) break;
                i++;
            }
            while (i < sl.Count)
            {
                if (!HelperMethods.IsBasicBlock(sl[i] as Block)) return false;
                i++;
            }
            return true;
        }
#endif

        /// <summary>
        /// Scans the entire block to find 0 or more closure initializations. For each, it adds an entry to the dictionary
        /// mapping the closure type to the closure local.
        /// </summary>
        internal static void RecordClosureInitialization(Method containing, Block b, Dictionary<TypeNode, Local> closureLocalMap)
        {
            Contract.Requires(closureLocalMap != null);

            Local closureLocal = null;
            if (b == null || b.Statements == null || !(b.Statements.Count > 0))
            {
                return;
            }
            for (int i = 0; i < b.Statements.Count; i++)
            {
                Statement s = b.Statements[i];
                if (s == null || s.NodeType == NodeType.Nop) continue;
                if (HelperMethods.IsClosureCreation(containing, s, out closureLocal))
                {
                  if (closureLocal != null)
                  {
                    closureLocalMap.Add(closureLocal.Type, closureLocal);
                  }
                }
            }
        }

        /// <summary>
        /// Replaces any MemberBindings of the form (this,x) in the methodContract with a method call where the method
        /// being called is P where x is a private field of the source type that has been marked as [ContractPublicPropertyName("P")].
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="sourceType"></param>
        /// <param name="methodContract"></param>
        internal static void ReplacePrivateFieldsThatHavePublicProperties(TypeNode targetType,
          TypeNode sourceType,
          MethodContract methodContract,
          ContractNodes contractNodes
          )
        {
          Contract.Requires(sourceType != null);

            Dictionary<Field, Method> field2Getter = new Dictionary<Field, Method>();

            for (int i = 0, n = /*sourceType.Members == null ? 0 : */ sourceType.Members.Count; i < n; i++)
            {
                Field f = sourceType.Members[i] as Field;
                if (f == null) continue;
                string propertyName = HelperMethods.GetStringFromAttribute(f, ContractNodes.SpecPublicAttributeName);
                if (propertyName != null)
                {
                    Property p = sourceType.GetProperty(Identifier.For(propertyName));
                    if (p != null)
                    {
                        field2Getter.Add(f, p.Getter);
                    }
                }
            }
            if (0 < field2Getter.Count)
            {
                SubstitutePropertyGetterForField s = new SubstitutePropertyGetterForField(field2Getter);
                s.Visit(methodContract);
            }
            return;
        }

        private class SubstitutePropertyGetterForField : StandardVisitor
        {
            readonly Dictionary<Field, Method> substitution;
            public SubstitutePropertyGetterForField(Dictionary<Field, Method> substitutionTable)
            {
                substitution = substitutionTable;
            }
            public override Expression VisitMemberBinding(MemberBinding memberBinding)
            {
                if (memberBinding == null) return null;
                Field f = memberBinding.BoundMember as Field;
                if (f == null || !this.substitution.ContainsKey(f))
                    return base.VisitMemberBinding(memberBinding);
                Method m = this.substitution[f];
                return new MethodCall(
                  new MemberBinding(memberBinding.TargetObject, m),
                  null,
                  m.IsVirtual ? NodeType.Callvirt : NodeType.Call,
                  f.Type
                  );
            }
        }

        internal static void TryAddCompilerGeneratedAttribute(Class Class)
        {
          Contract.Requires(Class != null);

            TryAddCompilerGeneratedAttribute(Class, System.AttributeTargets.Class);
        }
        internal static void TryAddCompilerGeneratedAttribute(EnumNode Enum)
        {
          Contract.Requires(Enum != null);
            TryAddCompilerGeneratedAttribute(Enum, System.AttributeTargets.Enum);
        }
        internal static void TryAddCompilerGeneratedAttribute(Method method)
        {
          Contract.Requires(method != null);

            TryAddCompilerGeneratedAttribute(method, System.AttributeTargets.Method);
        }
        internal static void TryAddCompilerGeneratedAttribute(Field field)
        {
          Contract.Requires(field != null);
            TryAddCompilerGeneratedAttribute(field, System.AttributeTargets.Field);
        }
        internal static void TryAddDebuggerBrowsableNeverAttribute(Field field)
        {
            Contract.Requires(field != null);
            TryAddDebuggerBrowsableNeverAttribute(field, System.AttributeTargets.Field);
        }

        internal static void TryAddCompilerGeneratedAttribute(Member member, System.AttributeTargets targets)
        {
          Contract.Requires(member != null);

            if (HelperMethods.compilerGeneratedAttributeNode == null) return;
            Member compilerGenerated = HelperMethods.compilerGeneratedAttributeNode.GetConstructor();
            AttributeNode compilerGeneratedAttribute = new AttributeNode(new MemberBinding(null, compilerGenerated), null, targets);
            member.Attributes.Add(compilerGeneratedAttribute);
        }

        internal static void TryAddDebuggerBrowsableNeverAttribute(Member member, System.AttributeTargets targets)
        {
            Contract.Requires(member != null);

            try
            {
                if (HelperMethods.debuggerBrowsableAttributeNode == null) return;
                if (HelperMethods.debuggerBrowsableStateType == null) return;
                var ctor = HelperMethods.debuggerBrowsableAttributeNode.GetConstructor(HelperMethods.debuggerBrowsableStateType);
                var args = new ExpressionList(Literal.Int32Zero);
                var attribute = new AttributeNode(new MemberBinding(null, ctor), args, targets);
                member.Attributes.Add(attribute);
            }
            catch
            { }
        }


    }


}
