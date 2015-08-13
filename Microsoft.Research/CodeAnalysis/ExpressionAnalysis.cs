// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Research.Graphs;
using Microsoft.Research.DataStructures;
using System.Linq;

namespace Microsoft.Research.CodeAnalysis
{
    using SubroutineContext = FList<Microsoft.Research.DataStructures.STuple<CFGBlock, CFGBlock, string>>;
    using System.Diagnostics.Contracts;

    public class ExpressionAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, Context, EdgeData>
      where Context : IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>
      where SymbolicValue : IEquatable<SymbolicValue>
      where Type : IEquatable<Type>
      where EdgeData : IFunctionalMap<SymbolicValue, FList<SymbolicValue>>
    {
        #region Privates
        private IFixpointInfo<APC, Domain> fixpointInfo;
        private ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, SymbolicValue, Context, EdgeData> valueLayer;

        private bool PreStateLookup(APC label, out Domain ifFound) { return fixpointInfo.PreState(label, out ifFound); }
        private bool PostStateLookup(APC label, out Domain ifFound) { return fixpointInfo.PostState(label, out ifFound); }

        private IFrameworkLogOptions options;
        private Predicate<APC> isUnreachable;
        #endregion

        public ExpressionAnalysis(
          ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, SymbolicValue, Context, EdgeData> valueLayer,
          IFrameworkLogOptions options, Predicate<APC> isUnreachable)
        {
            this.valueLayer = valueLayer;
            this.options = options;
            this.isUnreachable = isUnreachable;
        }

        public bool Debug;

        #region internal Expressions
        public abstract class Expression : IEquatable<Expression>
        {
            #region Internals

            internal abstract Result Decode<Data, Result, Visitor>(APC at, SymbolicValue dest, Visitor visitor, Data data)
              where Visitor : IVisitExprIL<APC, Type, SymbolicValue, SymbolicValue, Data, Result>;

            /// <summary>
            /// Return an expression with the given substitution applied
            /// </summary>
            internal abstract Expression Substitute(IFunctionalMap<SymbolicValue, FList<SymbolicValue>> subst);

            /// <summary>
            /// Returns true if the expression contains one of the variables in the candidate set
            /// </summary>
            internal abstract bool Contains(IFunctionalSet<SymbolicValue> candidates);

            /// <summary>
            /// Returns true if the expression contains the symbolic value (one level deep)
            /// </summary>
            internal abstract bool Contains(SymbolicValue symbol);

            internal abstract IEnumerable<SymbolicValue> Variables
            {
                get;
            }

            public abstract override string ToString();

            internal class Unary : Expression
            {
                // symbol being operand of unary op
                internal SymbolicValue source;
                internal UnaryOperator op;
                internal bool overflow;
                internal bool unsigned;

                internal Unary(SymbolicValue source, UnaryOperator op, bool overflow, bool unsigned)
                {
                    this.source = source;
                    this.op = op;
                    this.overflow = overflow;
                    this.unsigned = unsigned;
                }

                public override bool Equals(Expression e)
                {
                    Unary other = e as Unary;
                    if (other == null) { return false; }
                    return other.op == this.op && other.overflow == this.overflow && other.unsigned == this.unsigned && other.source.Equals(this.source);
                }


                internal override Result Decode<Data, Result, Visitor>(APC at, SymbolicValue dest, Visitor visitor, Data data)
                {
                    return visitor.Unary(at, op, overflow, unsigned, dest, source, data);
                }

                internal override Expression Substitute(IFunctionalMap<SymbolicValue, FList<SymbolicValue>> subst)
                {
                    if (subst.Contains(source))
                    {
                        return new Unary(subst[source].Head, op, overflow, unsigned);
                    }
                    return null;
                }

                internal override bool Contains(IFunctionalSet<SymbolicValue> candidates)
                {
                    if (candidates.Contains(source)) return true;
                    return false;
                }

                internal override bool Contains(SymbolicValue symbol)
                {
                    return symbol.Equals(this.source);
                }


                public override string ToString()
                {
                    return String.Format("Unary({0} {1})", op, source);
                }

                internal override IEnumerable<SymbolicValue> Variables
                {
                    get { yield return this.source; }
                }
            }

            internal class Binary : Expression
            {
                internal SymbolicValue left;
                internal SymbolicValue right;
                internal BinaryOperator op;

                internal Binary(SymbolicValue left, SymbolicValue right, BinaryOperator op)
                {
                    this.left = left;
                    this.right = right;
                    this.op = op;
                }

                public override bool Equals(Expression e)
                {
                    Binary other = e as Binary;
                    if (other == null) { return false; }
                    return other.op == this.op && other.left.Equals(this.left) && other.right.Equals(this.right);
                }


                internal override Result Decode<Data, Result, Visitor>(APC at, SymbolicValue dest, Visitor visitor, Data data)
                {
                    return visitor.Binary(at, op, dest, left, right, data);
                }

                internal override Expression Substitute(IFunctionalMap<SymbolicValue, FList<SymbolicValue>> subst)
                {
                    bool hasLeft = subst.Contains(left);
                    bool hasRight = subst.Contains(right);

                    if (hasLeft && hasRight)
                    {
                        return new Binary(subst[left].Head, subst[right].Head, op);
                    }
                    else
                    {
                        return null;
                    }
                }

                internal override bool Contains(IFunctionalSet<SymbolicValue> candidates)
                {
                    if (candidates.Contains(left)) return true;
                    if (candidates.Contains(right)) return true;
                    return false;
                }

                internal override bool Contains(SymbolicValue symbol)
                {
                    return this.left.Equals(symbol) || this.right.Equals(symbol);
                }


                public override string ToString()
                {
                    return String.Format("Binary({0} {1} {2})", left, op, right);
                }

                internal override IEnumerable<SymbolicValue> Variables
                {
                    get
                    {
                        yield return this.left;
                        yield return this.right;
                    }
                }
            }

            internal class IsInst : Expression
            {
                internal readonly Type type;
                internal readonly SymbolicValue argument;

                public IsInst(Type type, SymbolicValue argument)
                {
                    this.type = type;
                    this.argument = argument;
                }

                public override bool Equals(Expression e)
                {
                    IsInst other = e as IsInst;
                    if (other == null) { return false; }
                    return other.argument.Equals(this.argument) && other.type.Equals(this.type);
                }


                internal override Result Decode<Data, Result, Visitor>(APC at, SymbolicValue dest, Visitor visitor, Data data)
                {
                    return visitor.Isinst(at, type, dest, argument, data);
                }

                internal override Expression Substitute(IFunctionalMap<SymbolicValue, FList<SymbolicValue>> subst)
                {
                    if (subst.Contains(argument))
                    {
                        return new IsInst(type, subst[argument].Head);
                    }
                    else
                    {
                        return null;
                    }
                }

                internal override bool Contains(IFunctionalSet<SymbolicValue> candidates)
                {
                    return candidates.Contains(argument);
                }

                internal override bool Contains(SymbolicValue symbol)
                {
                    return this.argument.Equals(symbol);
                }

                public override string ToString()
                {
                    return String.Format("IsInst({0} {1})", type.ToString(), argument.ToString());
                }



                internal override IEnumerable<SymbolicValue> Variables
                {
                    get { yield return this.argument; }
                }
            }

            internal class Sizeof : Expression
            {
                internal readonly Type type;

                public Sizeof(Type type)
                {
                    this.type = type;
                }

                public override bool Equals(Expression e)
                {
                    Sizeof other = e as Sizeof;
                    if (other == null) { return false; }
                    return other.type.Equals(this.type);
                }

                internal override Result Decode<Data, Result, Visitor>(APC at, SymbolicValue dest, Visitor visitor, Data data)
                {
                    return visitor.Sizeof(at, type, dest, data);
                }

                internal override Expression Substitute(IFunctionalMap<SymbolicValue, FList<SymbolicValue>> subst)
                {
                    return this;
                }

                internal override bool Contains(IFunctionalSet<SymbolicValue> candidates)
                {
                    return false;
                }

                internal override bool Contains(SymbolicValue symbol)
                {
                    return false;
                }

                public override string ToString()
                {
                    return String.Format("Sizeof({0})", type.ToString());
                }

                internal override IEnumerable<SymbolicValue> Variables
                {
                    get { yield break; }
                }
            }

            internal class Const : Expression
            {
                internal readonly Type type;
                internal readonly object value;

                public Const(Type type, object value)
                {
                    this.type = type;
                    this.value = value;
                }

                public override bool Equals(Expression e)
                {
                    Const other = e as Const;
                    if (other == null) { return false; }
                    return (other.value.Equals(this.value) && other.type.Equals(this.type));
                }

                internal override Result Decode<Data, Result, Visitor>(APC at, SymbolicValue dest, Visitor visitor, Data data)
                {
                    return visitor.Ldconst(at, value, type, dest, data);
                }

                internal override Expression Substitute(IFunctionalMap<SymbolicValue, FList<SymbolicValue>> subst)
                {
                    return this;
                }

                internal override bool Contains(IFunctionalSet<SymbolicValue> candidates)
                {
                    return false;
                }

                internal override bool Contains(SymbolicValue symbol)
                {
                    return false;
                }

                public override string ToString()
                {
                    return String.Format("Const({0} {1})", type.ToString(), value.ToString());
                }

                internal override IEnumerable<SymbolicValue> Variables
                {
                    get { yield break; }
                }
            }

            internal class Null : Expression
            {
                public override bool Equals(Expression e)
                {
                    return e == this;
                }

                internal override Result Decode<Data, Result, Visitor>(APC at, SymbolicValue dest, Visitor visitor, Data data)
                {
                    return visitor.Ldnull(at, dest, data);
                }

                internal override Expression Substitute(IFunctionalMap<SymbolicValue, FList<SymbolicValue>> subst)
                {
                    return this;
                }

                internal override bool Contains(IFunctionalSet<SymbolicValue> candidates)
                {
                    return false;
                }
                internal override bool Contains(SymbolicValue symbol)
                {
                    return false;
                }
                public override string ToString()
                {
                    return "Null()";
                }

                internal override IEnumerable<SymbolicValue> Variables
                {
                    get { yield break; }
                }
            }
            #endregion

            public static readonly Expression NullValue = new Null(); // Thread-safe

            public abstract bool Equals(Expression e);
        }
        #endregion

        public Domain InitialValue(Converter<SymbolicValue/*!*/, int> keyNumber)
        {
            return Domain.TopValue(keyNumber);
        }

        public IAnalysis<APC, Domain, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, Domain, Domain>, EdgeData>
          CreateExpressionAnalyzer()
        {
            return new ValueAnalysis(this);
        }

        public IEnumerable<SubroutineContext> GetContexts(CFGBlock block)
        {
            return fixpointInfo.CachedContexts(block);
        }

        #region Value analysis interface
        private class ValueAnalysis
          : IAnalysis<APC, Domain, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, Domain, Domain>, EdgeData>
        {
            internal ExpressionAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, Context, EdgeData> parent;
            public ValueAnalysis(ExpressionAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, Context, EdgeData> parent)
            {
                this.parent = parent;
            }
            #region IAnalysis<Label,EnvironmentDomain<SymbolicValue,Label>,IVisitMSIL<Label,Local,Parameter,Method,Field,Type,SymbolicValue,SymbolicValue,EnvironmentDomain<SymbolicValue,Label>,EnvironmentDomain<SymbolicValue,Label>>,SymbolicValue> Members

            /// <summary>
            /// Note: we keep some mappings, even though the source key is not appearing anywhere in the renaming. We do this to 
            /// keep track of expressions such as sizeof(...) and others that are not represented in the Egraph and thus become
            /// unreachable. In principle, if the egraph kept all expressions so we could hash cons them, then this problem would
            /// go away and we could build a map from scratch, rather than removing things that are killed.
            /// </summary>
            public Domain EdgeConversion(APC from, APC to, bool joinPoint, EdgeData sourceTargetMap, Domain originalState)
            {
                if (sourceTargetMap == null) return originalState;

                if (this.parent.Debug)
                {
                    Console.WriteLine("====Expression analysis Parallel Assign===");
                    DumpMap(sourceTargetMap);
                    DumpExpressions("original exprs", originalState);
                }

                Domain newState = originalState.Empty();

                Domain renamedState = originalState.Empty();
                // apply the substitution (to the first target) to all our expressions in our domain, provided, they are not killed
                foreach (SymbolicValue key in originalState.Keys)
                {
                    Expression expr = originalState[key].Value;
                    Contract.Assume(expr != null);
                    var subst = expr.Substitute(sourceTargetMap);
                    if (subst != null)
                    {
                        renamedState = renamedState.Add(key, subst);
                    }
                }

                // constrain all targets
                foreach (SymbolicValue source in sourceTargetMap.Keys)
                {
                    //^ assume targets != null
                    FlatDomain<Expression> renamedExpression = renamedState[source];
                    if (renamedExpression.IsNormal)
                    {
                        for (FList<SymbolicValue> targets = sourceTargetMap[source]; targets != null; targets = targets.Tail)
                        {
                            SymbolicValue target = targets.Head;
                            newState = newState.Add(target, renamedExpression.Value);
                        }
                    }
                }

                if (this.parent.Debug)
                {
                    DumpExpressions("new exprs", newState);
                }
                return newState;
            }

            private void DumpExpressions(string header, Domain originalState)
            {
                Console.WriteLine("---  {0}  ---", header);
                foreach (var key in originalState.Keys)
                {
                    var target = originalState[key];
                    if (target.IsNormal)
                    {
                        Console.WriteLine("{0} -> {1}", key, target.Value.ToString());
                    }
                    else if (target.IsTop)
                    {
                        Console.WriteLine("{0} -> (Top)", key);
                    }
                    else if (target.IsBottom)
                    {
                        Console.WriteLine("{0} -> (Bot)", key);
                    }
                }
            }

            private void DumpMap(IFunctionalMap<SymbolicValue, FList<SymbolicValue>> sourceTargetMap)
            {
                Console.WriteLine("Source-target assignment");
                foreach (var key in sourceTargetMap.Keys)
                {
                    var targets = sourceTargetMap[key];
                    for (var t = targets; t != null; t = t.Tail)
                    {
                        Console.Write("{0} ", t.Head);
                    }
                    Console.WriteLine(" := {0}", key);
                }
            }

            public Domain Join(Pair<APC, APC> edge, Domain newState, Domain prevState, out bool weaker, bool widen)
            {
                return prevState.Join(newState, out weaker, widen);
            }

            public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, Domain, Domain>
              Visitor()
            {
                return new AnalysisDecoder(this);
            }

            public Predicate<APC> CacheStates(IFixpointInfo<APC, Domain> fixpointInfo)
            {
                this.parent.fixpointInfo = fixpointInfo;
                return delegate (APC pc) { return true; };
            }

            public bool IsBottom(APC pc, Domain state)
            {
                return state.IsBottom;
            }

            public bool IsTop(APC pc, Domain state)
            {
                return false;
            }

            public Domain ImmutableVersion(Domain state)
            {
                return state;
            }

            public Domain MutableVersion(Domain state)
            {
                // our domain is functional, so no clone needed.
                return state;
            }

            public void Dump(Pair<Domain, TextWriter> statetw)
            {
                statetw.One.Dump(statetw.Two);
            }

            #endregion
        }
        #endregion

        #region Expression analysis instruction visitor
        /// <summary>
        /// Used to compute the expression mapping when running the expression analysis.
        /// </summary>
        public struct Domain : IGraph<SymbolicValue, Unit>
        {
            private EnvironmentDomain<SymbolicValue, FlatDomain<Expression>> expressions;

            private Domain(
              EnvironmentDomain<SymbolicValue, FlatDomain<Expression>> expressions
            )
            {
                this.expressions = expressions;
            }

            internal static Domain TopValue(Converter<SymbolicValue, int> keyNumber)
            {
                return new Domain(EnvironmentDomain<SymbolicValue, FlatDomain<Expression>>.TopValue(keyNumber));
            }

            internal IEnumerable<SymbolicValue> Keys { get { return expressions.Keys; } }
            internal Domain Add(SymbolicValue sv, Expression exp)
            {
                return new Domain(expressions.Add(sv, exp));
            }

            internal bool HasRefinement(SymbolicValue key)
            {
                return expressions.Contains(key);
            }

            internal Domain Remove(SymbolicValue key)
            {
                return new Domain(expressions.Remove(key));
            }

            internal FlatDomain<Expression> this[SymbolicValue key] { get { return expressions[key]; } }

            internal bool IsBottom { get { return expressions.IsBottom; } }

            internal Domain Join(Domain that, out bool weaker, bool widen)
            {
                return new Domain(expressions.Join(that.expressions, out weaker, widen));
            }

            internal SymbolicValue Find(SymbolicValue sv)
            {
                return sv;
            }

            internal void Dump(TextWriter tw)
            {
                expressions.Dump(tw);
            }



            internal Domain Empty()
            {
                return new Domain(expressions.Empty());
            }

            #region IGraph<SymbolicValue,Unit> Members

            IEnumerable<SymbolicValue> IGraph<SymbolicValue, Unit>.Nodes
            {
                get { return expressions.Keys; }
            }

            bool IGraph<SymbolicValue, Unit>.Contains(SymbolicValue node)
            {
                return expressions.Contains(node);
            }

            IEnumerable<Pair<Unit, SymbolicValue>> IGraph<SymbolicValue, Unit>.Successors(SymbolicValue node)
            {
                var expr = expressions[node];
                if (!expr.IsNormal) { yield break; }
                foreach (var sub in expr.Value.Variables)
                {
                    yield return new Pair<Unit, SymbolicValue>(Unit.Value, sub);
                }
            }

            #endregion

            /// <summary>
            /// Checks if target is reachable from root in graph represented by the sub-expression relation
            /// </summary>
            internal bool IsReachableFrom(SymbolicValue root, SymbolicValue target)
            {
                bool reachable = false;
                DepthFirst.Visit(this, root, delegate (SymbolicValue found) { if (found.Equals(target)) reachable = true; return true; }, null);
                return reachable;
            }

            internal bool Occurs(SymbolicValue var, SymbolicValue tree)
            {
                var checkedVars = new Set<SymbolicValue>();
                return Occurs(var, tree, checkedVars);
            }

            private bool Occurs(SymbolicValue var, SymbolicValue tree, Set<SymbolicValue> checkedVars)
            {
                if (!checkedVars.Add(tree)) return false;

                var exp = expressions[tree];
                if (!exp.IsNormal) return false;
                foreach (var contained in exp.Value.Variables.AssumeNotNull())
                {
                    if (var.Equals(contained) || Occurs(var, contained, checkedVars)) return true;
                }
                return false;
            }
        }

        private class AnalysisDecoder :
          MSILVisitor<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, Domain, Domain>
        {
            private ValueAnalysis valueAnalysis;

            public AnalysisDecoder(ValueAnalysis valueAnalysis)
            {
                this.valueAnalysis = valueAnalysis;
            }

            protected override Domain Default(APC pc, Domain data)
            {
                return data;
            }

            private struct AssumeDecoder : IVisitExprIL<APC, Type, SymbolicValue, SymbolicValue, Domain, Domain>
            {
                private bool truth;
                public AssumeDecoder(bool truth)
                {
                    this.truth = truth;
                }

                #region IVisitExprIL<Label,Type,SymbolicValue,SymbolicValue,Domain,Domain> Members

                private bool IsEqOperator(BinaryOperator op)
                {
                    return op == BinaryOperator.Ceq || op == BinaryOperator.Cobjeq;
                }
                /// <summary>
                /// Important. This needs an occurs check before making something equal to something else, for otherwise
                /// we build recursive terms, such as (x == (x & mask)), will generate a recursive term.
                /// </summary>
                public Domain Binary(APC pc, BinaryOperator op, SymbolicValue dest, SymbolicValue s1, SymbolicValue s2, Domain data)
                {
                    if (truth && IsEqOperator(op))
                    {
                        if (!data.HasRefinement(s1))
                        {
                            var refinement = data[s2];
                            if (refinement.IsNormal && !data.IsReachableFrom(s2, s1))
                            {
                                return data.Add(s1, refinement.Value);
                            }
                        }
                        else if (!data.HasRefinement(s2))
                        {
                            var refinement = data[s1];
                            if (refinement.IsNormal && !data.IsReachableFrom(s1, s2))
                            {
                                return data.Add(s2, refinement.Value);
                            }
                        }
                    }
                    if (!truth && op == BinaryOperator.Cne_Un)
                    {
                        if (!data.HasRefinement(s1))
                        {
                            var refinement = data[s2];
                            if (refinement.IsNormal && !data.IsReachableFrom(s2, s1))
                            {
                                return data.Add(s1, refinement.Value);
                            }
                        }
                        else if (!data.HasRefinement(s2))
                        {
                            var refinement = data[s1];
                            if (refinement.IsNormal && !data.IsReachableFrom(s1, s2))
                            {
                                return data.Add(s2, refinement.Value);
                            }
                        }
                    }
                    return data;
                }

                public Domain Isinst(APC pc, Type type, SymbolicValue dest, SymbolicValue obj, Domain data)
                {
                    return data;
                }

                public Domain Ldconst(APC pc, object constant, Type type, SymbolicValue dest, Domain data)
                {
                    return data;
                }

                public Domain Ldnull(APC pc, SymbolicValue dest, Domain data)
                {
                    return data;
                }

                public Domain Sizeof(APC pc, Type type, SymbolicValue dest, Domain data)
                {
                    return data;
                }

                public Domain Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, SymbolicValue dest, SymbolicValue source, Domain data)
                {
                    return data;
                }

                public Domain Box(APC pc, Type type, SymbolicValue dest, SymbolicValue source, Domain data)
                {
                    return data;
                }

                #endregion
            }

            public override Domain Assume(APC pc, string tag, SymbolicValue condition, object provenance, Domain data)
            {
                var conditionRefinement = data[condition];
                if (conditionRefinement.IsNormal)
                {
                    bool truth = (tag != "false");
                    data = conditionRefinement.Value.Decode<Domain, Domain, AssumeDecoder>(pc, condition, new AssumeDecoder(truth), data);
                }
                return data;
            }

            public override Domain Assert(APC pc, string tag, SymbolicValue condition, object provenance, Domain data)
            {
                var conditionRefinement = data[condition];
                if (conditionRefinement.IsNormal)
                {
                    data = conditionRefinement.Value.Decode<Domain, Domain, AssumeDecoder>(pc, condition, new AssumeDecoder(true), data);
                }
                return data;
            }

            public override Domain Binary(APC pc, BinaryOperator op, SymbolicValue dest, SymbolicValue s1, SymbolicValue s2, Domain data)
            {
                if (valueAnalysis.parent.valueLayer.NewerThan(dest, s1) && valueAnalysis.parent.valueLayer.NewerThan(dest, s2))
                { // avoid recursive terms
                    return data.Add(dest, new Expression.Binary(s1, s2, op));
                }
                else
                {
                    // do a full occurs check
                    if (!data.Occurs(dest, s1) && !data.Occurs(dest, s2))
                    {
                        return data.Add(dest, new Expression.Binary(s1, s2, op));
                    }
                }
                return data;
            }

            public override Domain Isinst(APC pc, Type type, SymbolicValue dest, SymbolicValue obj, Domain data)
            {
                if (dest.Equals(obj)) return data;
                return data.Add(dest, new Expression.IsInst(type, obj));
            }

            public override Domain Ldconst(APC pc, object constant, Type type, SymbolicValue dest, Domain data)
            {
                return data.Add(dest, new Expression.Const(type, constant));
            }

            public override Domain Ldnull(APC pc, SymbolicValue dest, Domain data)
            {
                return data.Add(dest, Expression.NullValue);
            }

            public override Domain Sizeof(APC pc, Type type, SymbolicValue dest, Domain data)
            {
                return data.Add(dest, new Expression.Sizeof(type));
            }

            public override Domain Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, SymbolicValue dest, SymbolicValue source, Domain data)
            {
                if (!dest.Equals(source))
                { // avoid recursive terms
                    return data.Add(dest, new Expression.Unary(source, op, overflow, unsigned));
                }
                else
                {
                    var sourceExp = data[source];
                    if (sourceExp.IsNormal)
                    {
                        return data.Add(dest, sourceExp.Value);
                    }
                    else
                    {
                        return data.Remove(dest);
                    }
                }
            }
        }
        #endregion

#if false
        public IAnalysis<APC, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, AState, AState>, EdgeData>
          GetExpressionDriver<AState>(
            IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, EdgeData> decoder,
            IValueAnalysis<APC, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, AState, AState>, SymbolicValue, IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue>> exprdriver
          )
        {
            return new ExpressionDriver<AState>(decoder, exprdriver, this);
        }
#endif

        public IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue, IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue>, EdgeData>
          GetDecoder(
            IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, EdgeData> ilDecoder
          )
        {
            return new ExprDecoder(ilDecoder, this);
        }

        #region MSIL Decoder providing Expressions
        private class ExprDecoder :
          IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue, IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue>, EdgeData>,
          IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue>,
          IExpressionContextData<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue>
        {
            #region Privates
            private IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, EdgeData> ilDecoder;
            private ExpressionAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, Context, EdgeData> parent;
            private IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue> underlying;
            #endregion

            public ExprDecoder(
              IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, EdgeData> ilDecoder,
              ExpressionAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, Context, EdgeData> parent
            )
            {
                this.ilDecoder = ilDecoder;
                this.parent = parent;
                underlying = ilDecoder.Context;
            }

            #region IMethodContext<Method> IStackContext<...> IValueContext<Label,Method,Type,SymbolicValue> Members

            public IExpressionContextData<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue> ExpressionContext { get { return this; } }
            public IValueContextData<Local, Parameter, Method, Field, Type, SymbolicValue> ValueContext { get { return underlying.ValueContext; } }
            public IStackContextData<Field, Method> StackContext { get { return underlying.StackContext; } }
            public IMethodContextData<Field, Method> MethodContext { get { return underlying.MethodContext; } }


            #endregion


            #region IExpressionContextData<Label,Method,Type,Expression,SymbolicValue> Members

            private struct ExpressionDecoderAdapter<Data, Result, Visitor> :
              IVisitExprIL<APC, Type, SymbolicValue, SymbolicValue, Data, Result>
              where Visitor : IVisitValueExprIL<ExternalExpression<APC, SymbolicValue>, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue, Data, Result>
            {
                private Visitor visitor;
                private ExpressionAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, Context, EdgeData> parent;

                public ExpressionDecoderAdapter(
                  Visitor visitor,
                  ExpressionAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, Context, EdgeData> parent
                )
                {
                    this.visitor = visitor;
                    this.parent = parent;
                }

                #region IVisitExprIL<Label,Type,SymbolicValue,SymbolicValue,Data,Result> Members

                private ExternalExpression<APC, SymbolicValue> Convert(APC at, SymbolicValue symbol)
                {
                    return new ExternalExpression<APC, SymbolicValue>(at, symbol);
                }

                public Result Binary(APC at, BinaryOperator op, SymbolicValue dest, SymbolicValue s1, SymbolicValue s2, Data data)
                {
                    return visitor.Binary(Convert(at, dest), op, dest, Convert(at, s1), Convert(at, s2), data);
                }

                public Result Isinst(APC at, Type type, SymbolicValue dest, SymbolicValue obj, Data data)
                {
                    return visitor.Isinst(Convert(at, dest), type, dest, Convert(at, obj), data);
                }

                public Result Ldconst(APC at, object constant, Type type, SymbolicValue dest, Data data)
                {
                    return visitor.Ldconst(Convert(at, dest), constant, type, dest, data);
                }

                public Result Ldnull(APC at, SymbolicValue dest, Data data)
                {
                    return visitor.Ldnull(Convert(at, dest), dest, data);
                }

                public Result Sizeof(APC at, Type type, SymbolicValue dest, Data data)
                {
                    return visitor.Sizeof(Convert(at, dest), type, dest, data);
                }

                public Result Unary(APC at, UnaryOperator op, bool overflow, bool unsigned, SymbolicValue dest, SymbolicValue source, Data data)
                {
                    return visitor.Unary(Convert(at, dest), op, overflow, unsigned, dest, Convert(at, source), data);
                }

                public Result Box(APC pc, Type type, SymbolicValue dest, SymbolicValue source, Data data)
                {
                    return visitor.Box(Convert(pc, dest), type, dest, Convert(pc, source), data);
                }

                #endregion
            }

            public Result Decode<Data, Result, Visitor>(ExternalExpression<APC, SymbolicValue> expr, Visitor visitor, Data data)
              where Visitor : IVisitValueExprIL<ExternalExpression<APC, SymbolicValue>, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue, Data, Result>
            {
                Domain d;
                if (!parent.PreStateLookup(expr.readAt, out d) || d.IsBottom)
                {
                    return visitor.SymbolicConstant(expr, expr.symbol, data);
                }
                FlatDomain<Expression> fexpr = d[expr.symbol];
                if (fexpr.IsNormal)
                {
                    return fexpr.Value.Decode<Data, Result, ExpressionDecoderAdapter<Data, Result, Visitor>>(expr.readAt, expr.symbol,
                                                                                                     new ExpressionDecoderAdapter<Data, Result, Visitor>(visitor, parent),
                                                                                                     data);
                }
                else
                {
                    Type type;
                    object value;
                    if (parent.valueLayer.Decoder.Context.ValueContext.IsConstant(expr.readAt, expr.symbol, out type, out value))
                    {
                        return visitor.Ldconst(expr, value, type, expr.symbol, data);
                    }
                    return visitor.SymbolicConstant(expr, expr.symbol, data);
                }
            }

            public FlatDomain<Type> GetType(ExternalExpression<APC, SymbolicValue> expr)
            {
                return underlying.ValueContext.GetType(expr.readAt, expr.symbol);
            }

            public ExternalExpression<APC, SymbolicValue> Refine(APC pc, SymbolicValue symbol)
            {
                return new ExternalExpression<APC, SymbolicValue>(pc, symbol);
            }

            public SymbolicValue Unrefine(ExternalExpression<APC, SymbolicValue> expr)
            {
                return expr.symbol;
            }

            public APC GetPC(ExternalExpression<APC, SymbolicValue> expr)
            {
                return expr.readAt;
            }

            public ExternalExpression<APC, SymbolicValue> For(SymbolicValue value)
            {
                // We use the entry point as a label.
                return new ExternalExpression<APC, SymbolicValue>(this.MethodContext.CFG.Entry, value);
            }

            public bool IsZero(ExternalExpression<APC, SymbolicValue> expr)
            {
                return this.ValueContext.IsZero(expr.readAt, expr.symbol);
            }

            public bool TryGetArrayLength(ExternalExpression<APC, SymbolicValue> array, out ExternalExpression<APC, SymbolicValue> length)
            {
                SymbolicValue lengthval;
                if (underlying.ValueContext.TryGetArrayLength(array.readAt, array.symbol, out lengthval))
                {
                    length = new ExternalExpression<APC, SymbolicValue>(array.readAt, lengthval);
                    return true;
                }
                length = new ExternalExpression<APC, SymbolicValue>();
                return false;
            }

            public bool TryGetWritableBytes(ExternalExpression<APC, SymbolicValue> pointer, out ExternalExpression<APC, SymbolicValue> writableBytes)
            {
                SymbolicValue writableBytesVal;
                if (underlying.ValueContext.TryGetWritableBytes(pointer.readAt, pointer.symbol, out writableBytesVal))
                {
                    writableBytes = new ExternalExpression<APC, SymbolicValue>(pointer.readAt, writableBytesVal);
                    return true;
                }
                writableBytes = new ExternalExpression<APC, SymbolicValue>();
                return false;
            }

            #endregion

            #region IDecodeMSIL<Label,Local,Parmeter,Method,Field,Type,Expression,SymbolicValue,IExpressionContext<Label,Method,Type,Expression,SymbolicValue>> Members

            public struct MSILDecoderAdapter<Data, Result, Visitor> :
                IVisitMSIL<APC, Local, Parameter, Method, Field, Type, SymbolicValue, SymbolicValue, Data, Result>
                where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue, Data, Result>
            {
                private ExpressionAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, Context, EdgeData> parent;
                private Visitor visitor;

                public MSILDecoderAdapter(
                  ExpressionAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, Context, EdgeData> parent,
                  Visitor visitor
                )
                {
                    this.parent = parent;
                    this.visitor = visitor;
                }

                #region IVisitMSIL<Label,Local,Parameter,Method,Field,Type,SymbolicValue,SymbolicValue,Data,Result> Members

                private struct ArgumentWrapper<ArgList> : IIndexable<ExternalExpression<APC, SymbolicValue>>
                  where ArgList : IIndexable<SymbolicValue>
                {
                    private ArgList underlying;
                    private APC readAt;

                    public ArgumentWrapper(ArgList underlying, APC readAt)
                    {
                        this.underlying = underlying;
                        this.readAt = readAt;
                    }

                    #region IIndexable<Expression> Members

                    public int Count
                    {
                        get
                        {
                            Contract.Ensures(Contract.Result<int>() == underlying.Count);
                            return underlying.Count;
                        }
                    }

                    public ExternalExpression<APC, SymbolicValue> this[int index]
                    {
                        get
                        {
                            Contract.Assert(index < this.Count);
                            Contract.Assert(this.Count == underlying.Count);
                            Contract.Assert(index < underlying.Count);

                            return Convert(underlying[index], readAt);
                        }
                    }

                    #endregion
                }

                private static ExternalExpression<APC, SymbolicValue> Convert(SymbolicValue source, APC pc)
                {
                    return new ExternalExpression<APC, SymbolicValue>(pc, source);
                }

                private ArgumentWrapper<ArgList> Convert<ArgList>(APC pc, ArgList sources)
                  where ArgList : IIndexable<SymbolicValue>
                {
                    return new ArgumentWrapper<ArgList>(sources, pc);
                }

                public Result Arglist(APC pc, SymbolicValue dest, Data data)
                {
                    return visitor.Arglist(pc, dest, data);
                }

                public Result BranchCond(APC pc, APC target, BranchOperator bop, SymbolicValue value1, SymbolicValue value2, Data data)
                {
                    throw new Exception("Should not get branches at this level of abstraction.");
                }

                public Result BranchTrue(APC pc, APC target, SymbolicValue cond, Data data)
                {
                    throw new Exception("Should not get branches at this level of abstraction.");
                }

                public Result BranchFalse(APC pc, APC target, SymbolicValue cond, Data data)
                {
                    throw new Exception("Should not get branches at this level of abstraction.");
                }

                public Result Branch(APC pc, APC target, bool leave, Data data)
                {
                    throw new Exception("Should not get branches at this leverl of abstraction.");
                }

                public Result Break(APC pc, Data data)
                {
                    return visitor.Break(pc, data);
                }

                public Result Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, SymbolicValue dest, ArgList args, Data data)
                  where TypeList : IIndexable<Type>
                  where ArgList : IIndexable<SymbolicValue>
                {
                    return visitor.Call(pc, method, tail, virt, extraVarargs, dest, Convert(pc, args), data);
                }

                public Result Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, SymbolicValue dest, SymbolicValue fp, ArgList args, Data data)
                  where TypeList : IIndexable<Type>
                  where ArgList : IIndexable<SymbolicValue>
                {
                    return visitor.Calli(pc, returnType, argTypes, tail, isInstance, dest, Convert(fp, pc), Convert(pc, args), data);
                }

                public Result Ckfinite(APC pc, SymbolicValue dest, SymbolicValue source, Data data)
                {
                    return visitor.Ckfinite(pc, dest, Convert(source, pc), data);
                }

                public Result Cpblk(APC pc, bool @volatile, SymbolicValue destaddr, SymbolicValue srcaddr, SymbolicValue len, Data data)
                {
                    return visitor.Cpblk(pc, @volatile, Convert(destaddr, pc), Convert(srcaddr, pc), Convert(len, pc), data);
                }

                public Result Endfilter(APC pc, SymbolicValue decision, Data data)
                {
                    return visitor.Endfilter(pc, Convert(decision, pc), data);
                }

                public Result Endfinally(APC pc, Data data)
                {
                    return visitor.Endfinally(pc, data);
                }

                public Result Initblk(APC pc, bool @volatile, SymbolicValue destaddr, SymbolicValue value, SymbolicValue len, Data data)
                {
                    return visitor.Initblk(pc, @volatile, Convert(destaddr, pc), Convert(value, pc), Convert(len, pc), data);
                }

                public Result Jmp(APC pc, Method method, Data data)
                {
                    return visitor.Jmp(pc, method, data);
                }

                public Result Ldarg(APC pc, Parameter argument, bool isOld, SymbolicValue dest, Data data)
                {
                    return visitor.Ldarg(pc, argument, isOld, dest, data);
                }

                public Result Ldarga(APC pc, Parameter argument, bool isOld, SymbolicValue dest, Data data)
                {
                    return visitor.Ldarga(pc, argument, isOld, dest, data);
                }

                public Result Ldftn(APC pc, Method method, SymbolicValue dest, Data data)
                {
                    return visitor.Ldftn(pc, method, dest, data);
                }

                public Result Ldind(APC pc, Type type, bool @volatile, SymbolicValue dest, SymbolicValue ptr, Data data)
                {
                    return visitor.Ldind(pc, type, @volatile, dest, Convert(ptr, pc), data);
                }

                public Result Ldloc(APC pc, Local local, SymbolicValue dest, Data data)
                {
                    return visitor.Ldloc(pc, local, dest, data);
                }

                public Result Ldloca(APC pc, Local local, SymbolicValue dest, Data data)
                {
                    return visitor.Ldloca(pc, local, dest, data);
                }

                public Result Localloc(APC pc, SymbolicValue dest, SymbolicValue size, Data data)
                {
                    return visitor.Localloc(pc, dest, Convert(size, pc), data);
                }

                public Result Nop(APC pc, Data data)
                {
                    return visitor.Nop(pc, data);
                }

                public Result Pop(APC pc, SymbolicValue source, Data data)
                {
                    return visitor.Pop(pc, Convert(source, pc), data);
                }

                public Result Return(APC pc, SymbolicValue source, Data data)
                {
                    return visitor.Return(pc, Convert(source, pc), data);
                }

                public Result Starg(APC pc, Parameter argument, SymbolicValue source, Data data)
                {
                    return visitor.Starg(pc, argument, Convert(source, pc), data);
                }

                public Result Stind(APC pc, Type type, bool @volatile, SymbolicValue ptr, SymbolicValue value, Data data)
                {
                    return visitor.Stind(pc, type, @volatile, Convert(ptr, pc), Convert(value, pc), data);
                }

                public Result Stloc(APC pc, Local local, SymbolicValue source, Data data)
                {
                    return visitor.Stloc(pc, local, Convert(source, pc), data);
                }

                public Result Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, SymbolicValue value, Data data)
                {
                    throw new Exception("Should not see Switch at this level of abstraction (assumes).");
                }

                public Result Box(APC pc, Type type, SymbolicValue dest, SymbolicValue source, Data data)
                {
                    return visitor.Box(pc, type, dest, Convert(source, pc), data);
                }

                public Result ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, SymbolicValue dest, ArgList args, Data data)
                  where TypeList : IIndexable<Type>
                  where ArgList : IIndexable<SymbolicValue>
                {
                    return visitor.ConstrainedCallvirt(pc, method, tail, constraint, extraVarargs, dest, Convert(pc, args), data);
                }

                public Result Castclass(APC pc, Type type, SymbolicValue dest, SymbolicValue obj, Data data)
                {
                    return visitor.Castclass(pc, type, dest, Convert(obj, pc), data);
                }

                public Result Cpobj(APC pc, Type type, SymbolicValue destptr, SymbolicValue srcptr, Data data)
                {
                    return visitor.Cpobj(pc, type, Convert(destptr, pc), Convert(srcptr, pc), data);
                }

                public Result Initobj(APC pc, Type type, SymbolicValue ptr, Data data)
                {
                    return visitor.Initobj(pc, type, Convert(ptr, pc), data);
                }

                public Result Ldelem(APC pc, Type type, SymbolicValue dest, SymbolicValue array, SymbolicValue index, Data data)
                {
                    return visitor.Ldelem(pc, type, dest, Convert(array, pc), Convert(index, pc), data);
                }

                public Result Ldelema(APC pc, Type type, bool @readonly, SymbolicValue dest, SymbolicValue array, SymbolicValue index, Data data)
                {
                    return visitor.Ldelema(pc, type, @readonly, dest, Convert(array, pc), Convert(index, pc), data);
                }

                public Result Ldfld(APC pc, Field field, bool @volatile, SymbolicValue dest, SymbolicValue obj, Data data)
                {
                    return visitor.Ldfld(pc, field, @volatile, dest, Convert(obj, pc), data);
                }

                public Result Ldflda(APC pc, Field field, SymbolicValue dest, SymbolicValue obj, Data data)
                {
                    return visitor.Ldflda(pc, field, dest, Convert(obj, pc), data);
                }

                public Result Ldlen(APC pc, SymbolicValue dest, SymbolicValue array, Data data)
                {
                    return visitor.Ldlen(pc, dest, Convert(array, pc), data);
                }

                public Result Ldsfld(APC pc, Field field, bool @volatile, SymbolicValue dest, Data data)
                {
                    return visitor.Ldsfld(pc, field, @volatile, dest, data);
                }

                public Result Ldsflda(APC pc, Field field, SymbolicValue dest, Data data)
                {
                    return visitor.Ldsflda(pc, field, dest, data);
                }

                public Result Ldtypetoken(APC pc, Type type, SymbolicValue dest, Data data)
                {
                    return visitor.Ldtypetoken(pc, type, dest, data);
                }

                public Result Ldfieldtoken(APC pc, Field field, SymbolicValue dest, Data data)
                {
                    return visitor.Ldfieldtoken(pc, field, dest, data);
                }

                public Result Ldmethodtoken(APC pc, Method method, SymbolicValue dest, Data data)
                {
                    return visitor.Ldmethodtoken(pc, method, dest, data);
                }

                public Result Ldvirtftn(APC pc, Method method, SymbolicValue dest, SymbolicValue obj, Data data)
                {
                    return visitor.Ldvirtftn(pc, method, dest, Convert(obj, pc), data);
                }

                public Result Mkrefany(APC pc, Type type, SymbolicValue dest, SymbolicValue obj, Data data)
                {
                    return visitor.Mkrefany(pc, type, dest, Convert(obj, pc), data);
                }

                public Result Newarray<ArgList>(APC pc, Type type, SymbolicValue dest, ArgList lengths, Data data)
                  where ArgList : IIndexable<SymbolicValue>
                {
                    return visitor.Newarray(pc, type, dest, Convert(pc, lengths), data);
                }

                public Result Newobj<ArgList>(APC pc, Method ctor, SymbolicValue dest, ArgList args, Data data)
                  where ArgList : IIndexable<SymbolicValue>
                {
                    return visitor.Newobj(pc, ctor, dest, Convert(pc, args), data);
                }

                public Result Refanytype(APC pc, SymbolicValue dest, SymbolicValue source, Data data)
                {
                    return visitor.Refanytype(pc, dest, Convert(source, pc), data);
                }

                public Result Refanyval(APC pc, Type type, SymbolicValue dest, SymbolicValue source, Data data)
                {
                    return visitor.Refanyval(pc, type, dest, Convert(source, pc), data);
                }

                public Result Rethrow(APC pc, Data data)
                {
                    return visitor.Rethrow(pc, data);
                }

                public Result Stelem(APC pc, Type type, SymbolicValue array, SymbolicValue index, SymbolicValue value, Data data)
                {
                    return visitor.Stelem(pc, type, Convert(array, pc), Convert(index, pc), Convert(value, pc), data);
                }

                public Result Stfld(APC pc, Field field, bool @volatile, SymbolicValue obj, SymbolicValue value, Data data)
                {
                    return visitor.Stfld(pc, field, @volatile, Convert(obj, pc), Convert(value, pc), data);
                }

                public Result Stsfld(APC pc, Field field, bool @volatile, SymbolicValue value, Data data)
                {
                    return visitor.Stsfld(pc, field, @volatile, Convert(value, pc), data);
                }

                public Result Throw(APC pc, SymbolicValue exn, Data data)
                {
                    return visitor.Throw(pc, Convert(exn, pc), data);
                }

                public Result Unbox(APC pc, Type type, SymbolicValue dest, SymbolicValue obj, Data data)
                {
                    return visitor.Unbox(pc, type, dest, Convert(obj, pc), data);
                }

                public Result Unboxany(APC pc, Type type, SymbolicValue dest, SymbolicValue obj, Data data)
                {
                    return visitor.Unboxany(pc, type, dest, Convert(obj, pc), data);
                }

                #endregion

                #region IVisitSynthIL<Label,SymbolicValue,Data,Result> Members

                public Result Assume(APC pc, string tag, SymbolicValue source, object provenance, Data data)
                {
                    return visitor.Assume(pc, tag, Convert(source, pc), provenance, data);
                }

                public Result Assert(APC pc, string tag, SymbolicValue condition, object provenance, Data data)
                {
                    return visitor.Assert(pc, tag, Convert(condition, pc), provenance, data);
                }

                public Result Entry(APC pc, Method method, Data data)
                {
                    return visitor.Entry(pc, method, data);
                }

                public Result Ldstack(APC pc, int offset, SymbolicValue dest, SymbolicValue source, bool isOld, Data data)
                {
                    return visitor.Ldstack(pc, offset, dest, Convert(source, pc), isOld, data);
                }

                public Result Ldstacka(APC pc, int offset, SymbolicValue dest, SymbolicValue source, Type type, bool isOld, Data data)
                {
                    return visitor.Ldstacka(pc, offset, dest, Convert(source, pc), type, isOld, data);
                }

                public Result BeginOld(APC pc, APC matchingEnd, Data data)
                {
                    return visitor.BeginOld(pc, matchingEnd, data);
                }

                public Result EndOld(APC pc, APC matchingBegin, Type type, SymbolicValue dest, SymbolicValue source, Data data)
                {
                    return visitor.EndOld(pc, matchingBegin, type, dest, Convert(source, pc), data);
                }

                public Result Ldresult(APC pc, Type type, SymbolicValue dest, SymbolicValue source, Data data)
                {
                    return visitor.Ldresult(pc, type, dest, Convert(source, pc), data);
                }

                #endregion        #endregion

                #region IVisitExprIL<Label,Type,SymbolicValue,SymbolicValue,Data,Result> Members

                public Result Binary(APC pc, BinaryOperator op, SymbolicValue dest, SymbolicValue s1, SymbolicValue s2, Data data)
                {
                    return visitor.Binary(pc, op, dest, Convert(s1, pc), Convert(s2, pc), data);
                }

                public Result Isinst(APC pc, Type type, SymbolicValue dest, SymbolicValue obj, Data data)
                {
                    return visitor.Isinst(pc, type, dest, Convert(obj, pc), data);
                }

                public Result Ldconst(APC pc, object constant, Type type, SymbolicValue dest, Data data)
                {
                    return visitor.Ldconst(pc, constant, type, dest, data);
                }

                public Result Ldnull(APC pc, SymbolicValue dest, Data data)
                {
                    return visitor.Ldnull(pc, dest, data);
                }

                public Result Sizeof(APC pc, Type type, SymbolicValue dest, Data data)
                {
                    return visitor.Sizeof(pc, type, dest, data);
                }

                public Result Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, SymbolicValue dest, SymbolicValue source, Data data)
                {
                    return visitor.Unary(pc, op, overflow, unsigned, dest, Convert(source, pc), data);
                }

                #endregion
            }

            public Result ForwardDecode<Data, Result, Visitor>(APC lab, Visitor visitor, Data data)
              where Visitor : IVisitMSIL<APC, Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue, Data, Result>
            {
                return ilDecoder.ForwardDecode<Data, Result, MSILDecoderAdapter<Data, Result, Visitor>>(lab, new MSILDecoderAdapter<Data, Result, Visitor>(parent, visitor), data);
            }

            public IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue> Context
            {
                get { return this; }
            }

            public bool IsUnreachable(APC pc)
            {
                return parent.isUnreachable(pc);
            }

            public EdgeData EdgeData(APC from, APC to)
            {
                return parent.valueLayer.Decoder.EdgeData(from, to);
            }

            public void Display(TextWriter tw, string prefix, EdgeData edgeData)
            {
                edgeData.Visit(delegate (SymbolicValue key, FList<SymbolicValue> targets)
                {
                    tw.Write("  {0} -> ", key);
                    foreach (var target in targets.GetEnumerable())
                    {
                        tw.Write("{0} ", target);
                    }
                    tw.WriteLine();
                    return VisitStatus.ContinueVisit;
                });
            }

            #endregion
        }
        #endregion

        public void BlockInfoPrinter(APC blockend, string prefix, TextWriter tw, Converter<ExternalExpression<APC, SymbolicValue>, string> source2string)
        {
            Contract.Requires(tw != null);
            Contract.Requires(source2string != null);

            foreach (var succ in valueLayer.Decoder.Context.MethodContext.CFG.Successors(blockend))
            {
                var rebindings = valueLayer.Decoder.EdgeData(blockend, succ);
                if (rebindings != null)
                {
                    bool firstTime = true;
                    foreach (SymbolicValue source in rebindings.Keys.OrderBy(s => s))
                    {
                        if (firstTime)
                        {
                            tw.WriteLine("{0}Rebinding on ({1} -> {2})", prefix, blockend, succ);
                            firstTime = false;
                        }
                        FList<SymbolicValue> targets = rebindings[source];
                        while (targets != null)
                        {
                            SymbolicValue target = targets.Head;
                            if (source == null)
                            {
                                tw.WriteLine("{0}  havoc {1}", prefix, target);
                            }
                            else
                            {
                                ExternalExpression<APC, SymbolicValue> expr = new ExternalExpression<APC, SymbolicValue>(blockend, source);
                                tw.WriteLine("{0}  {1} := {2} {3}", prefix, target, source2string(expr), valueLayer.Decoder.Context.ValueContext.GetType(blockend, source));
                            }
                            targets = targets.Tail;
                        }
                    }
                }
            }
        }

        public BlockInfoPrinter<APC> GetBlockPrinter(Converter<ExternalExpression<APC, SymbolicValue>, string> source2string)
        {
            return delegate (APC blockend, string prefix, TextWriter tw)
            {
                BlockInfoPrinter(blockend, prefix, tw, source2string);
            };
        }
#if false

        /// <returns>
        /// A map of assignments "target := source"
        /// </returns>
        public IFunctionalMap<string, string> RenamingsFor(Converter<ExternalExpression<APC, SymbolicValue>, string> converter, APC blockend, APC blockTarget)
        {
            foreach (APC l in this.rebindings.Keys2(blockend))
            {
                if (l.Equals(blockTarget))
                {
                    IFunctionalMap<string, string> result = FunctionalMap<string, string>.Empty;

                    IFunctionalMap<SymbolicValue, FList<SymbolicValue>> rebindings = this.rebindings[blockend, blockTarget];

                    Pair<APC, APC> edge = new Pair<APC, APC>(blockend, blockTarget);
                    foreach (SymbolicValue source in rebindings.Keys)
                    {
                        FList<SymbolicValue> targets = rebindings[source];
                        while (targets != null)
                        {
                            SymbolicValue target = targets.Head;
                            if (source == null)
                            {
                                result = result.Add(target.ToString(), "...havoc..."); // Empty list means that the value is havoc-ed
                            }
                            else
                            {
                                ExternalExpression<APC, SymbolicValue> expr = new ExternalExpression<APC, SymbolicValue>(edge.One, source);

                                // Console.WriteLine("{0} := {1}", target, converter(expr));
                                result = result.Add(target.ToString(), converter(expr));
                            }
                            targets = targets.Tail;
                        }
                    }
                    return result;
                }
            }
            return FunctionalMap<string, string>.Empty;
        }

        public CrossBlockRenamings<APC> GetBlockRenamings(Converter<ExternalExpression<APC, SymbolicValue>, string> converter)
        {
            return delegate (APC blockend, APC blockstart)
            {
                return this.RenamingsFor(converter, blockend, blockstart);
            };
        }

        public Set<string> RenamingsFor(APC blockend, APC blockTarget, Converter<ExternalExpression<APC, SymbolicValue>, Set<string>> converter)
        {
            foreach (APC l in this.rebindings.Keys2(blockend))
            {
                if (l.Equals(blockTarget))
                {
                    Set<string> result = new Set<String>();

                    IFunctionalMap<SymbolicValue, FList<SymbolicValue>> rebindings = this.rebindings[blockend, blockTarget];

                    Pair<APC, APC> edge = new Pair<APC, APC>(blockend, blockTarget);
                    foreach (SymbolicValue source in rebindings.Keys)
                    {
                        FList<SymbolicValue> targets = rebindings[source];

                        while (targets != null)
                        {
                            SymbolicValue target = targets.Head;
                            if (source != null)
                            {
                                ExternalExpression<APC, SymbolicValue> expr = new ExternalExpression<APC, SymbolicValue>(edge.One, source);

                                // Console.WriteLine("{1}", target, converter(expr));
                                result.AddRange(converter(expr));
                            }
                            targets = targets.Tail;
                        }
                    }
                    return result;
                }
            }
            return new Set<string>();
        }

        public RenamedVariables<APC> GetRenamedVariables(Converter<ExternalExpression<APC, SymbolicValue>, Set<string>> converter)
        {
            return delegate (APC blockend, APC blockstart)
            {
                return this.RenamingsFor(blockend, blockstart, converter);
            };
        }
#endif
    }

    public struct ExternalExpression<Label, SymbolicValue> : IEquatable<ExternalExpression<Label, SymbolicValue>>
      where SymbolicValue : IEquatable<SymbolicValue>
    {
        internal readonly Label readAt;
        internal readonly SymbolicValue symbol;

        internal ExternalExpression(Label readAt, SymbolicValue symbol)
        {
            this.readAt = readAt;
            this.symbol = symbol;
        }

        public override bool Equals(object obj)
        {
            if (obj is ExternalExpression<Label, SymbolicValue>)
            {
                ExternalExpression<Label, SymbolicValue> other = (ExternalExpression<Label, SymbolicValue>)obj;  // unbox
                                                                                                                 // HACK! for reasons in the AI handling of expressions as variables, we consider expressions equal if they refer to the same symbolic name
                return this.symbol.Equals(other.symbol); // && this.readAt.Equals(other.readAt);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.symbol.GetHashCode();
        }

        public override string ToString()
        {
            return symbol.ToString() + "@" + readAt;
        }

        #region IEquatable<ExternalExpression<Label,SymbolicValue>> Members

        public bool Equals(ExternalExpression<Label, SymbolicValue> other)
        {
            // HACK! for reasons in the AI handling of expressions as variables, we consider expressions equal if they refer to the same symbolic name
            return this.symbol.Equals(other.symbol); // && this.readAt.Equals(other.readAt);
        }

        #endregion
    }

    public class ExprPrinter
    {
        private class Decoder<Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Attribute, Assembly, LogOptions>
          : IVisitValueExprIL<ExternalExpression<APC, SymbolicValue>, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue, StringBuilder, Unit>
          where SymbolicValue : IEquatable<SymbolicValue>
          where Type : IEquatable<Type>
          where LogOptions : IFrameworkLogOptions
        {
            private IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue> decoder;
            private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, LogOptions> mDriver;

            public Decoder(
              IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue> decoder,
              IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, LogOptions> mDriver
            )
            {
                this.decoder = decoder;
                this.mDriver = mDriver;
            }

            public string ToString(ExternalExpression<APC, SymbolicValue> expr)
            {
                StringBuilder sb = new StringBuilder();
                Recurse(sb, expr);
                return sb.ToString();
            }

            #region IVisitExprIL<Type,ExternalExpression,SymbolicValue,StringBuilder,Unit> Members

            private void Recurse(StringBuilder tw, ExternalExpression<APC, SymbolicValue> expr)
            {
                if (expr.symbol.Equals(default(SymbolicValue))) { tw.Append("<null>"); return; }
                decoder.ExpressionContext.Decode<StringBuilder, Unit, Decoder<Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Attribute, Assembly, LogOptions>>(expr, this, tw);
            }


            public Unit Binary(ExternalExpression<APC, SymbolicValue> pc, BinaryOperator op, SymbolicValue dest, ExternalExpression<APC, SymbolicValue> s1, ExternalExpression<APC, SymbolicValue> s2, StringBuilder data)
            {
                data.Append('(');
                Recurse(data, s1);
                data.AppendFormat(" {0} ", op.ToString());
                Recurse(data, s2);
                data.Append(')');
                return Unit.Value;
            }

            public Unit Isinst(ExternalExpression<APC, SymbolicValue> pc, Type type, SymbolicValue dest, ExternalExpression<APC, SymbolicValue> obj, StringBuilder data)
            {
                data.AppendFormat("IsInst({0}) ", mDriver.MetaDataDecoder.FullName(type));
                Recurse(data, obj);
                return Unit.Value;
            }

            public Unit Ldconst(ExternalExpression<APC, SymbolicValue> pc, object constant, Type type, SymbolicValue dest, StringBuilder data)
            {
                data.Append(constant.ToString());
                return Unit.Value;
            }

            public Unit Ldnull(ExternalExpression<APC, SymbolicValue> pc, SymbolicValue dest, StringBuilder data)
            {
                data.Append("null");
                return Unit.Value;
            }

            public Unit Sizeof(ExternalExpression<APC, SymbolicValue> pc, Type type, SymbolicValue dest, StringBuilder data)
            {
                data.AppendFormat("sizeof({0})", mDriver.MetaDataDecoder.FullName(type));
                return Unit.Value;
            }

            public Unit Unary(ExternalExpression<APC, SymbolicValue> pc, UnaryOperator op, bool overflow, bool unsigned, SymbolicValue dest, ExternalExpression<APC, SymbolicValue> source, StringBuilder data)
            {
                data.AppendFormat("{0} ", op.ToString());
                Recurse(data, source);
                return Unit.Value;
            }

            public Unit SymbolicConstant(ExternalExpression<APC, SymbolicValue> pc, SymbolicValue symbol, StringBuilder data)
            {
                // try forall decoding
                var forall = mDriver.AsForAllIndexed(pc.readAt, symbol);
                if (forall != null)
                {
                    data.Append(forall.ToString());

                    return Unit.Value;
                }
                // try exists decoding
                var exists = mDriver.AsExistsIndexed(pc.readAt, symbol);
                if (exists != null)
                {
                    data.Append(exists.ToString());
                }
                else
                {
                    data.Append(symbol.ToString());
                }
                return Unit.Value;
            }

            public Unit Box(ExternalExpression<APC, SymbolicValue> pc, Type type, SymbolicValue dest, ExternalExpression<APC, SymbolicValue> source, StringBuilder data)
            {
                data.AppendFormat("Box({0}) ", mDriver.MetaDataDecoder.FullName(type));
                Recurse(data, source);
                return Unit.Value;
            }

            #endregion
        }

        public static Converter<ExternalExpression<APC, SymbolicValue>, string> Printer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, LogOptions>(
          IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue> decoder,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, LogOptions> mDriver)
          where SymbolicValue : IEquatable<SymbolicValue>
          where Type : IEquatable<Type>
          where LogOptions : IFrameworkLogOptions
        {
            var exprPrinter = new Decoder<Local, Parameter, Method, Field, Property, Event, Type, SymbolicValue, Attribute, Assembly, LogOptions>(decoder, mDriver);
            return exprPrinter.ToString;
        }
    }
}
