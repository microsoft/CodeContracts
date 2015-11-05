// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#define TRACEPERFORMANCE

using System;
using Microsoft.Research.DataStructures;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.CodeAnalysis
{
    public static class WeakestPreconditionProver
    {
        public struct AdditionalInfo
        {
            public static AdditionalInfo None
            {
                get
                {
                    return new AdditionalInfo(true);
                }
            }

            public readonly bool IsNone;
            public bool ReachedMethodEntry;
            public bool ReachedMaximumSize;
            public readonly bool ReachedLoop;

            private AdditionalInfo(bool isNone)
            {
                Contract.Requires(isNone == true);
                this.IsNone = isNone;
                this.ReachedMethodEntry = this.ReachedMaximumSize = this.ReachedLoop = false;
            }

            public override string ToString()
            {
                if (this.IsNone)
                    return "none";
                else
                    return "" +
                      (ReachedMethodEntry ? "ReachedMethodEntry " : null) +
                      (ReachedLoop ? "ReachedLoop" : null) +
                      (ReachedMaximumSize ? "ReachedMaximumSize" : null);
            }

            public IEnumerable<WarningContext> GetWarningContexts()
            {
                if (this.IsNone)
                    yield break;

                if (this.ReachedMethodEntry)
                    yield return new WarningContext(WarningContext.ContextType.WPReachedMethodEntry);

                if (this.ReachedLoop)
                    yield return new WarningContext(WarningContext.ContextType.WPReachedLoop);

                if (this.ReachedMaximumSize)
                    yield return new WarningContext(WarningContext.ContextType.WPReachedMaxPathSize);
            }
        }

        [ThreadStatic]
        public static bool Trace;
        [ThreadStatic]
        public static bool EmitSMT2Formula;

        #region TimeOut

        [ThreadStatic]
        private static TimeOutChecker timeout;
        public static TimeOutChecker Timeout
        {
            get
            {
                Contract.Ensures(Contract.Result<TimeOutChecker>() != null);

                // We want the timeout to be always set before, but if it is not the case, we do not want to crash, and just get a dummy Timeout
                if (timeout == null)
                {
                    timeout = new TimeOutChecker(180, 7, new CancellationToken()); // todo(mchri): Decide which value makes sense for the symbolic timeout
                }

                return timeout;
            }
            set
            {
                Contract.Requires(value != null);

                timeout = value;
            }
        }

        #endregion

        /// <summary>
        /// returns null if successfully discharged obligation, otherwise a path that cannot be discharged
        /// </summary>
        [Pure]
        public static Path Discharge<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
        (
          APC pc,
          Variable goal,
          int maxPathSize,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
          IFactQuery<BoxedExpression, Variable> facts,
          ContractInferenceManager inferenceManager,
          out AdditionalInfo why
        )
          where Expression : IEquatable<Expression>
          where Variable : IEquatable<Variable>
          where LogOptions : IFrameworkLogOptions
          where Type : IEquatable<Type>
        {
            Contract.Requires(mdriver != null);
            Contract.Requires(facts != null);
#if TRACEPERFORMANCE
            AdditionalInfo whyInternal = default(AdditionalInfo);

            var result =
              PerformanceMeasure.Measure(PerformanceMeasure.ActionTags.WP, () =>
                TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>.DischargeObligation(mdriver, facts, inferenceManager, maxPathSize, pc, goal, out whyInternal), true);

            why = whyInternal;

            return result;
#else
            return TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>.DischargeObligation(mdriver, facts, inferenceManager, maxPathSize, pc, goal, out why);
#endif
        }

        [Pure]
        public static Path Discharge<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
       (
         APC pc,
        BoxedExpression goal,
        int maxPathSize,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
        IFactQuery<BoxedExpression, Variable> facts,
          ContractInferenceManager inferenceManager,
          out AdditionalInfo why
         )
          where Expression : IEquatable<Expression>
          where Variable : IEquatable<Variable>
          where LogOptions : IFrameworkLogOptions
          where Type : IEquatable<Type>
        {
            Contract.Requires(facts != null);
#if TRACEPERFORMANCE
            AdditionalInfo whyInternal = default(AdditionalInfo);

            var result = PerformanceMeasure.Measure(PerformanceMeasure.ActionTags.WP,
              () => TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>.DischargeObligation(mdriver, facts, inferenceManager, maxPathSize, pc, goal, out whyInternal), true);

            why = whyInternal;

            return result;
#else
            return TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>.DischargeObligation(mdriver, facts, inferenceManager, maxPathSize, pc, goal, out why);
#endif
        }

        public class Path
        {
            private readonly IFunctionalSet<APC> loopHeads;
            private readonly FList<APC> codePath;
            private readonly BoxedExpression goal; // maybe null, meaning false. Must prove by contradiction
            private readonly bool goalIsNegated;
            private FList<BoxedExpression> posAssumptions;
            private FList<BoxedExpression> negAssumptions;

            public int Size { get { return posAssumptions.Length() + negAssumptions.Length(); } }

            public Path(APC initial, BoxedExpression initialGoal)
            {
                loopHeads = FunctionalSet<APC>.Empty();
                codePath = FList<APC>.Cons(initial, null);
                posAssumptions = FList<BoxedExpression>.Empty;
                negAssumptions = FList<BoxedExpression>.Empty;
                goal = NormalizePolarity(initialGoal, false, out goalIsNegated, 0);
            }

            private Path(FList<APC> path, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal, bool negatedGoal, IFunctionalSet<APC> loopHeads)
            {
                this.loopHeads = loopHeads;
                codePath = path;
                this.posAssumptions = posAssumptions;
                this.negAssumptions = negAssumptions;
                this.goal = goal;
                goalIsNegated = negatedGoal;
            }

            public Path ExpandBy(APC pc, bool loopHead)
            {
                IFunctionalSet<APC> newLoopHeads = (loopHead) ? loopHeads.Add(pc) : loopHeads;
                return new Path(codePath.Cons(pc), posAssumptions, negAssumptions, goal, goalIsNegated, newLoopHeads);
            }

            public Path WithGoal(BoxedExpression newGoal)
            {
                return new Path(codePath, posAssumptions, negAssumptions, newGoal, goalIsNegated, loopHeads);
            }

            public Path Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> converter)
            {
                bool newlyNegated = false;
                BoxedExpression newGoal = goal == null ? null : NormalizePolarity(goal.Substitute(converter), false, out newlyNegated, 0);

                bool negatedGoal = newlyNegated ? !goalIsNegated : goalIsNegated;

                FList<BoxedExpression> tmpPosAssumptions = null;
                FList<BoxedExpression> tmpNegAssumptions = null;
                bool contradiction = false;

                posAssumptions.Apply(delegate (BoxedExpression b)
                                          {
                                              bool negated;
                                              var b2 = NormalizePolarity(b.Substitute(converter), false, out negated, 0);
                                              if (b2 == null) return;
                                              if (negated)
                                              {
                                                  tmpNegAssumptions = tmpNegAssumptions.Cons(b2);
                                                  CheckForContradiction(tmpPosAssumptions, b2, ref contradiction);
                                              }
                                              else
                                              {
                                                  tmpPosAssumptions = tmpPosAssumptions.Cons(b2);
                                                  CheckForContradiction(tmpNegAssumptions, b2, ref contradiction);
                                              }
                                          });

                if (contradiction) return null; // kill this path

                negAssumptions.Apply(delegate (BoxedExpression b)
                                          {
                                              bool negated;
                                              var b2 = NormalizePolarity(b.Substitute(converter), false, out negated, 0);
                                              if (b2 == null) return;
                                              if (negated)
                                              {
                                                  tmpPosAssumptions = tmpPosAssumptions.Cons(b2);
                                                  CheckForContradiction(tmpNegAssumptions, b2, ref contradiction);
                                              }
                                              else
                                              {
                                                  tmpNegAssumptions = tmpNegAssumptions.Cons(b2);
                                                  CheckForContradiction(tmpPosAssumptions, b2, ref contradiction);
                                              }
                                          });

                if (contradiction) return null; // kill this path

                // Check if goal is now implied by any of the assumptions
                if (newGoal != null)
                {
                    if (negatedGoal)
                    {
                        for (var list = tmpNegAssumptions; list != null; list = list.Tail)
                        {
                            if (list.Head.Equals(newGoal)) return null; // kills this path
                        }
                    }
                    else
                    {
                        for (var list = tmpPosAssumptions; list != null; list = list.Tail)
                        {
                            if (list.Head.Equals(newGoal)) return null; // kills this path
                            BinaryOperator bop;
                            BoxedExpression left, right;
                            if (list.Head.IsBinaryExpression(out bop, out left, out right))
                            {
                                int rightval;
                                if (bop == BinaryOperator.Ceq && left.Equals(newGoal) && right.IsConstantInt(out rightval) && rightval != 0)
                                {
                                    // we know left != 0, and if left is our goal, then we are done.
                                    return null;
                                }
                            }
                        }
                    }
                }
                return new Path(codePath, tmpPosAssumptions, tmpNegAssumptions, newGoal, negatedGoal, loopHeads);
            }

            private bool CheckForContradiction(FList<BoxedExpression> assumptions, BoxedExpression toFind)
            {
                bool contradiction = false;
                CheckForContradiction(assumptions, toFind, ref contradiction);
                return contradiction;
            }

            private void CheckForContradiction(FList<BoxedExpression> assumptions, BoxedExpression toFind, ref bool contradiction)
            {
                if (contradiction) return; // optimize already contradiction

                while (assumptions != null)
                {
                    if (assumptions.Head.Equals(toFind))
                    {
                        contradiction = true;
                        return;
                    }
                    assumptions = assumptions.Tail;
                }
            }

            public FList<BoxedExpression> PosAssumptions { get { return posAssumptions; } }
            public FList<BoxedExpression> NegAssumptions { get { return negAssumptions; } }
            public FList<APC> CodePath { get { return codePath; } }
            public APC PC { get { return codePath.Head; } }
            public APC FirstUsablePC
            {
                get
                {
                    var path = codePath;
                    for (; path != null; path = path.Tail)
                    {
                        if (path.Head.ILOffset == 0) continue;
                        if (!path.Head.Block.Subroutine.IsMethod) continue;
                        return path.Head;
                    }
                    return PC; // no good pc
                }
            }
            public BoxedExpression Goal { get { return goal; } }
            public bool GoalIsNegated { get { return goalIsNegated; } }

            public void PrintPathInMethod<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>(
              IOutput output,
              string lastSourceContext, // already emitted
              IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver
            )
              where LogOptions : IFrameworkLogOptions
              where Type : IEquatable<Type>
            {
                var path = codePath.Reverse().Tail; //skip assert point
                                                    // avoid duplicates
                var seen = new System.Collections.Generic.HashSet<string>();
                for (; path != null; path = path.Tail)
                {
                    APC apc = path.Head;
                    if (!apc.Block.Subroutine.IsMethod) continue;
                    if (!apc.HasRealSourceContext) continue;
                    string sourceContext = apc.PrimarySourceContext();
                    if (sourceContext == lastSourceContext) continue;

                    if (seen.Contains(sourceContext)) return; // reached same point twice

                    seen.Add(sourceContext);
                    output.WriteLine("{0}:   warning : point on path related to warning", sourceContext);
                    lastSourceContext = sourceContext;
                }
            }


            /// <summary>
            /// Puts the expression into a normal form, where comparisons
            /// are
            ///  lt, lte, = 
            /// and their negations are expressed using a toplevel not.
            /// This way, we can spot contraditions faster.
            /// </summary>
            /// <param name="expr"></param>
            /// <param name="negated">returns true if returned expression should be considered negated.</param>
            /// <returns></returns>
            private static BoxedExpression NormalizePolarity(BoxedExpression expr, bool inNegativeContext, out bool negated, int depth)
            {
                Contract.Ensures(expr == null || Contract.Result<BoxedExpression>() != null);

                if (expr == null) { negated = false; return null; }
                if (Trace)
                {
                    WriteSpaces(depth);
                    Console.WriteLine("Prior to normalize: {0}", expr);
                }
                expr = NormalizePolarityInternal(expr, inNegativeContext, out negated, depth + 1);

                if (Trace)
                {
                    if (negated)
                    {
                        WriteSpaces(depth);
                        Console.WriteLine("After normalize: !({0})", expr);
                    }
                    else
                    {
                        WriteSpaces(depth);
                        Console.WriteLine("After normalize: {0}", expr);
                    }
                }
                return expr;
            }

            private static void WriteSpaces(int depth)
            {
                for (var i = 0; i < depth; i++)
                {
                    Console.Write(' ');
                }
            }

            /// <summary>
            /// Normalize to syntactic same form:
            ///   a geq b  becomes b leq a
            ///   a gt b   becomes !(a leq b)
            ///   a leq b  stays 
            ///   a leq_un stays
            ///   a lt b   becomes !(b leq a)
            ///
            /// </summary>
            /// <param name="expr"></param>
            /// <param name="negated"></param>
            /// <returns></returns>
            [Pure]
            private static BoxedExpression NormalizePolarityInternal(BoxedExpression expr, bool inNegativeContext, out bool negated, int depth)
            {
                Contract.Requires(expr != null);
                Contract.Ensures(Contract.Result<BoxedExpression>() != null);

                UnaryOperator uop;
                BinaryOperator bop;
                BoxedExpression left, right;
                if (expr.IsBinaryExpression(out bop, out left, out right))
                {
                    #region All the cases
                    switch (bop)
                    {
                        case BinaryOperator.Cge:
                            negated = false;
                            return BoxedExpression.Binary(BinaryOperator.Cle, right, left);
                        case BinaryOperator.Cge_Un:
                            negated = false;
                            return BoxedExpression.Binary(BinaryOperator.Cle_Un, right, left);
                        case BinaryOperator.Cgt:
                            negated = true;
                            return BoxedExpression.Binary(BinaryOperator.Cle, left, right);
                        case BinaryOperator.Cgt_Un:
                            // Case 1: Particular case for (a is x) >_un null - The boxed expressions are already doing it in the ToString() method
                            // Case 2: Also, it seems that Roslyn is already generating some slightly different code
                            if ((left.IsIsInst && right.IsNull) /* case 1*/
                              || (right.IsNull && right.Constant == null) /* case 2 */)
                            {
                                negated = false;
                                return left;
                            }
                            else
                            {
                                negated = true;
                                return BoxedExpression.Binary(BinaryOperator.Cle_Un, left, right);
                            }
                        case BinaryOperator.Clt:
                            {
                                negated = true;
                                return BoxedExpression.Binary(BinaryOperator.Cle, right, left);
                            }
                        case BinaryOperator.Clt_Un:
                            {
                                negated = true;
                                return BoxedExpression.Binary(BinaryOperator.Cle_Un, right, left);
                            }
                        case BinaryOperator.Cle:
                            {
                                if (inNegativeContext)
                                {
                                    negated = true;
                                    return BoxedExpression.Binary(BinaryOperator.Clt, right, left);
                                }
                                else
                                {
                                    negated = false;
                                    return expr;
                                }
                            }
                        case BinaryOperator.Cle_Un:
                            {
                                if (inNegativeContext)
                                {
                                    negated = true;
                                    return BoxedExpression.Binary(BinaryOperator.Clt_Un, right, left);
                                }
                                else
                                {
                                    negated = false;
                                    return expr;
                                }
                            }
                        case BinaryOperator.Cne_Un:
                            {
                                bool nestedNegated;
                                BoxedExpression result = NormalizePolarity(BoxedExpression.Binary(BinaryOperator.Ceq, left, right), inNegativeContext, out nestedNegated, depth + 1);
                                negated = !nestedNegated;
                                return result;
                            }
                        case BinaryOperator.Ceq:
                        case BinaryOperator.Cobjeq:
                            {
                                int rightVal;
                                if (right.IsConstantIntOrNull(out rightVal))
                                {
                                    if (rightVal == 0)
                                    {
                                        // negate left
                                        bool nestedNegated;

                                        var left2 = NormalizePolarity(left, inNegativeContext, out nestedNegated, depth + 1);
                                        negated = !nestedNegated;
                                        return left2;
                                    }
                                    int leftValue;
                                    if (rightVal == 1 && left.IsConstantInt(out leftValue) && leftValue == 1)
                                    {
                                        // (true == true) == true
                                        negated = false;
                                        return left;
                                    }
                                }
                                else
                                {
                                    int leftValue;
                                    if (left.IsConstantIntOrNull(out leftValue))
                                    {
                                        if (leftValue == 1)
                                        {
                                            // N(1 == (a bop b)) ===> N((a bop b))
                                            if (right.IsBinary && right.BinaryOp.IsComparisonBinaryOperator())
                                            {
                                                return NormalizePolarity(right, inNegativeContext, out negated, depth + 1);
                                            }

                                            // check if right value is the same after normalization
                                            bool nestedNegated;
                                            var right2 = NormalizePolarity(right, inNegativeContext, out nestedNegated, depth + 1);
                                            int rightValue;
                                            // if (right2 != null)
                                            {
                                                if (nestedNegated)
                                                {
                                                    // negate right
                                                    negated = true; // remeber we should negate it
                                                    return right2;
                                                }
                                                else if (right2.IsConstantInt(out rightValue) && rightValue == 1)
                                                {
                                                    Contract.Assert(!nestedNegated); // just for code readibility
                                                                                     // (true == true) == true
                                                    negated = false;
                                                    return left;
                                                }
                                            }
                                        }
                                        else if (leftValue == 0)
                                        {
                                            // negate right
                                            bool nestedNegated;
                                            var right2 = NormalizePolarity(right, inNegativeContext, out nestedNegated, depth + 1);
                                            negated = !nestedNegated;
                                            return right2;
                                        }
                                    }
                                }
                                if (bop == BinaryOperator.Ceq)
                                {
                                    goto default;
                                }
                                // normalize to ceq
                                expr = BoxedExpression.Binary(BinaryOperator.Ceq, left, right);
                                goto default;
                            }

                        case BinaryOperator.Xor:
                            {
                                // !(a ^ b) is (a==b)
                                if (inNegativeContext)
                                {
                                    negated = true; // We swallow the negation

                                    return BoxedExpression.Binary(BinaryOperator.Ceq, left, right);
                                }
                                else // (a ^ b) is (a != b)
                                {
                                    negated = false;
                                    return BoxedExpression.Binary(BinaryOperator.Cne_Un, left, right);
                                }
                            }

                        default:
                            negated = false;
                            return expr;
                    }
                    #endregion
                }
                if (expr.IsUnary)
                {
                    uop = expr.UnaryOp;
                    if (uop == UnaryOperator.Not)
                    {
                        bool nestedNegated;
                        BoxedExpression result = NormalizePolarity(expr.UnaryArgument, !inNegativeContext, out nestedNegated, depth + 1);
                        negated = !nestedNegated;
                        return result;
                    }
                    negated = false;
                    return expr;
                }
                bool isForAll;
                BoxedExpression boundVar, low, upp, body;
                if (expr.IsQuantifiedExpression(out isForAll, out boundVar, out low, out upp, out body))
                {
                    if (!inNegativeContext)
                    {
                        BoxedExpression unaryExp;
                        // !(a bop b)
                        if (body.IsUnaryExpression(out uop, out unaryExp) && uop == UnaryOperator.Not)
                        {
                            BinaryOperator negatedBop;
                            if (unaryExp.IsBinaryExpression(out bop, out left, out right) && bop.TryNegate(out negatedBop))
                            {
                                var newBody = BoxedExpression.Binary(negatedBop, left, right);
                                negated = false;
                                return isForAll ?
                                  new ForAllIndexedExpression(expr.UnderlyingVariable, boundVar, low, upp, newBody) as BoxedExpression :
                                  new ExistsIndexedExpression(expr.UnderlyingVariable, boundVar, low, upp, newBody) as BoxedExpression;
                            }
                        }
                        else if (body.IsBinaryExpression(out bop, out left, out right))
                        {
                            int k;
                            if (bop == BinaryOperator.Ceq &&
                                (left.IsBinary && left.BinaryOp == BinaryOperator.Ceq) &&
                                (right.IsConstantIntOrNull(out k) && k == 0))
                            {
                                var newBody = BoxedExpression.Binary(BinaryOperator.Cne_Un, left.BinaryLeft, left.BinaryRight);
                                negated = false;

                                return isForAll ?
                                  new ForAllIndexedExpression(null, boundVar, low, upp, newBody) as BoxedExpression :
                                  new ExistsIndexedExpression(null, boundVar, low, upp, newBody) as BoxedExpression;
                            }
                        }
                    }
                }
                negated = false;
                return expr;
            }

            [Pure]
            private static bool IsRelation(BoxedExpression b)
            {
                if (b.IsBinary)
                {
                    switch (b.BinaryOp)
                    {
                        case BinaryOperator.Ceq:
                        case BinaryOperator.Cobjeq:
                        case BinaryOperator.Cge:
                        case BinaryOperator.Cge_Un:
                        case BinaryOperator.Cgt:
                        case BinaryOperator.Cgt_Un:
                        case BinaryOperator.Cle:
                        case BinaryOperator.Cle_Un:
                        case BinaryOperator.Clt:
                        case BinaryOperator.Clt_Un:
                        case BinaryOperator.Cne_Un:
                            return true;
                        default:
                            return false;
                    }
                }
                return false;
            }

            private IList<BoxedExpression> SplitOnOperator(BoxedExpression expr, BinaryOperator logicalOp)
            {
                var list = new List<BoxedExpression>();
                AddParts(expr, list, logicalOp);
                return list;
            }

            private void AddParts(BoxedExpression expr, IList<BoxedExpression> list, BinaryOperator logicalOp)
            {
                BoxedExpression left, right;
                BinaryOperator bop;
                if (expr.IsBinaryExpression(out bop, out left, out right) && bop == logicalOp)
                {
                    AddParts(left, list, logicalOp);
                    AddParts(right, list, logicalOp);
                }
                else
                {
                    list.Add(expr);
                }
            }
            /// <summary>
            /// Both, add the substituted assumption, which is normalized to the proper truth direction
            /// as well as the original variable. This is needed if at some point on the path, we know that sv1 == 1
            /// and later we don't know it and we see assume(true) sv1 and later assume(false) sv1. We need to
            /// discharge that path, but if we only use substitutions, it may look as if we assume(true) true and later
            /// assume(false) sv1.
            /// </summary>
            internal Path AddAssumption<Variable>(BoxedExpression assumption, Variable original, bool originalNegated, Comparison<Variable> comparer)
            {
                BinaryOperator bop;
                BoxedExpression left, right;
                if (assumption.IsBinaryExpression(out bop, out left, out right))
                {
                    if (bop == BinaryOperator.LogicalAnd)
                    {
                        var conjuncts = SplitOnOperator(assumption, BinaryOperator.LogicalAnd);
                        // assume a && b
                        foreach (var conjunct in conjuncts)
                        {
                            Variable var;
                            if (!conjunct.TryGetFrameworkVariable(out var)) continue;
                            if (AddAssumption(conjunct, var, false, comparer) == null) return null; // contradiction
                        }
                        return this;
                    }
                    else if (bop == BinaryOperator.LogicalOr)
                    {
                        var disjuncts = SplitOnOperator(assumption, BinaryOperator.LogicalOr);
                        // (a || b) is ~(~a && ~b)
                        // look for ~a in the existing assumptions => contradiction, thus b can be assumed. Vice versa for ~b
                        for (int i = 0; i < disjuncts.Count; i++)
                        {
                            var disjunct = disjuncts[i];
                            Variable var;
                            if (!disjunct.TryGetFrameworkVariable(out var)) return this; // can't do much
                            if (DoesAtomicAssumptionContradict(disjunct, var, false, comparer))
                            {
                                disjuncts[i] = null;
                            }
                        }
                        // now see if we have any disjuncts left.
                        var remaining = disjuncts.Where(elem => elem != null);
                        if (remaining.Any())
                        {
                            var first = remaining.First();
                            if (remaining.Skip(1).Any())
                            {
                                // still have some others.
                                return this; // can't do anything with the remaining disjunction
                            }
                            // sole remainder can be added
                            Variable firstVar;
                            if (!first.TryGetFrameworkVariable(out firstVar)) return this; // can't do much
                            return AddAssumption(first, firstVar, false, comparer);
                        }
                        else
                        {
                            return null; // contradiction
                        }
                    }
                    // a != b, and we know both a and b, then we found a contraddiction
                    else if (bop == BinaryOperator.Cne_Un)
                    {
                        Variable leftVar, rightVar;
                        if (left.TryGetFrameworkVariable(out leftVar) && right.TryGetFrameworkVariable(out rightVar))
                        {
                            if (this.ContainsVariables(posAssumptions, leftVar, rightVar))
                            {
                                if (Trace)
                                {
                                    Console.WriteLine("   Path discharged as we encountered {0} with both arguments true...", assumption);
                                }

                                return null;
                            }
                        }
                    }
                }
                return AddAtomicAssumption(assumption, original, originalNegated, comparer);
            }

            [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
            internal Path AddAtomicAssumption<Variable>(BoxedExpression assumption, Variable original, bool originalNegated, Comparison<Variable> comparer)
            {
                if (assumption == null)
                {
                    return this;
                }

                bool negated;

                // check for redundant assumption
                if (originalNegated)
                {
                    if (ContainsVariable(negAssumptions, original)) return this;
                }
                else
                {
                    if (ContainsVariable(posAssumptions, original)) return this;
                }
                var normalized = NormalizePolarity(assumption, false, out negated, 0);

                if (normalized.IsVariable && normalized.UnderlyingVariable.Equals(original))
                {
                    // don't add the variable again.
                }
                else
                {
                    var originalExp = BoxedExpression.Var(original);
                    if (originalNegated)
                    {
                        if (CheckForContradiction(posAssumptions, originalExp))
                        {
                            if (Trace)
                            {
                                Console.WriteLine("Discharged Path due to negated originalExp {0} appearing in posAssumptions", original);
                            }
                            return null;
                        }
                        negAssumptions = negAssumptions.Cons(originalExp);
                    }
                    else
                    {
                        if (CheckForContradiction(negAssumptions, originalExp))
                        {
                            if (Trace)
                            {
                                Console.WriteLine("Discharged Path due to positive originalExp {0} appearing in negAssumptions", original);
                            }
                            return null;
                        }
                        posAssumptions = posAssumptions.Cons(originalExp);
                    }
                }

                if (negated)
                {
                    if (normalized.IsConstant)
                    {
                        var c = normalized.Constant;
                        if (!(c is string))
                        {
                            if (c == null)
                            {
                                // negated null constant as assumption is trivial true, don't add it
                                return this;
                            }
                            IConvertible ic = c as IConvertible;
                            if (ic != null)
                            {
                                try
                                {
                                    int value = ic.ToInt32(null);
                                    if (value == 0)
                                    {
                                        return this; // negated 0 is trivial true, don't add it
                                    }
                                    else
                                    {
                                        if (Trace)
                                        {
                                            Console.WriteLine("Discharged Path due to normalized expression {0} = !{1} being null constant", assumption, normalized);
                                        }
                                        return null; // negated non-zero is contradiction
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }

                    if (goal != null && goalIsNegated && goal.Equals(normalized))
                    {
                        if (Trace)
                        {
                            Console.WriteLine("Discharged Path due to exact match of negative assumption {0} and negative goal {1}", normalized, goal);
                        }
                        return null;
                    }
                    if (CheckForContradiction(posAssumptions, normalized))
                    {
                        if (Trace)
                        {
                            Console.WriteLine("Discharged Path due to negated normalized {0} appearing in posAssumptions", normalized);
                        }
                        return null;
                    }
                    negAssumptions = negAssumptions.Cons(normalized);
                }
                else
                {
                    if (normalized.IsConstant)
                    {
                        var c = normalized.Constant;
                        if (!(c is string))
                        {
                            if (c == null)
                            { // null constant as assumption is contradiction
                                if (Trace)
                                {
                                    Console.WriteLine("Discharged Path due to null constant added to assumptions");
                                }
                                return null;
                            }
                            var ic = c as IConvertible;
                            if (ic != null)
                            {
                                try
                                {
                                    int value = ic.ToInt32(null);
                                    if (value == 0)
                                    {
                                        if (Trace)
                                        {
                                            Console.WriteLine("Discharged Path due to 0 constant added to assumptions");
                                        }
                                        return null; // contradiction
                                    }
                                    else
                                    {
                                        return this; // don't add trivial "true" to assumptions
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    if (goal != null && !goalIsNegated && goal.Equals(normalized))
                    {
                        if (Trace)
                        {
                            Console.WriteLine("Discharged Path due to assumption {0} matching goal {1} exactly", normalized, goal);
                        }
                        return null;
                    }

                    if (CheckForContradiction(negAssumptions, normalized))
                    {
                        if (Trace)
                        {
                            Console.WriteLine("Discharged Path due to positive normalized {0} appearing in negAssumptions", normalized);
                        }
                        return null;
                    }
                    #region if equality, substitute in existing, but keep original equality
                    BinaryOperator bop;
                    BoxedExpression left, right;
                    Variable leftVar, rightVar;
                    var result = this;
                    if (normalized.IsBinaryExpression(out bop, out left, out right) && (bop == BinaryOperator.Ceq || bop == BinaryOperator.Cobjeq) &&
                        left.TryGetFrameworkVariable(out leftVar) && right.TryGetFrameworkVariable(out rightVar))
                    {
                        var diff = comparer(leftVar, rightVar);
                        if (diff < 0)
                        {
                            // left var is older
                            result = this.Substitute(delegate (Variable var, BoxedExpression orig) { if (comparer(var, rightVar) == 0) return left; else return orig; });
                        }
                        else if (diff > 1)
                        {
                            // right var is older
                            result = this.Substitute(delegate (Variable var, BoxedExpression orig) { if (comparer(var, leftVar) == 0) return right; else return orig; });
                        }
                    }
                    #endregion
                    if (result != null)
                    {
                        result.posAssumptions = result.posAssumptions.Cons(normalized);
                        result = SaturateNewPosAssumption(result, normalized, comparer);
                    }
                    return result;
                }
                return this;
            }

            private static Path SaturateNewPosAssumption<Variable>(Path result, BoxedExpression pos, Comparison<Variable> comparer)
            {
                if (result == null) return result;
                if (pos.IsIsInst)
                {
                    Variable var;
                    var arg = pos.UnaryArgument;
                    if (!arg.TryGetFrameworkVariable(out var)) return result;

                    // if a as T is true, then a has to be nonnull (true)
                    return result.AddAtomicAssumption(pos.UnaryArgument, var, false, comparer);
                }
                return result;
            }

            internal bool DoesAtomicAssumptionContradict<Variable>(BoxedExpression assumption, Variable original, bool originalNegated, Comparison<Variable> comparer)
            {
                Contract.Requires(assumption != null);

                bool negated;

                // check for redundant assumption
                if (originalNegated)
                {
                    if (ContainsVariable(negAssumptions, original)) return false;
                }
                else
                {
                    if (ContainsVariable(posAssumptions, original)) return false;
                }
                var normalized = NormalizePolarity(assumption, false, out negated, 0);

                if (normalized.IsVariable && normalized.UnderlyingVariable.Equals(original))
                {
                    // don't add the variable again.
                }
                else
                {
                    var originalExp = BoxedExpression.Var(original);
                    if (originalNegated)
                    {
                        if (CheckForContradiction(posAssumptions, originalExp))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (CheckForContradiction(negAssumptions, originalExp))
                        {
                            return true;
                        }
                    }
                }

                if (negated)
                {
                    if (normalized.IsConstant)
                    {
                        var c = normalized.Constant;
                        if (!(c is string))
                        {
                            if (c == null)
                            {
                                // negated null constant as assumption is trivial true, don't add it
                                return false;
                            }
                            var ic = c as IConvertible;
                            if (ic != null)
                            {
                                try
                                {
                                    int value = ic.ToInt32(null);
                                    if (value == 0)
                                    {
                                        return false; // negated 0 is trivial true, don't add it
                                    }
                                    else
                                    {
                                        return true; // negated non-zero is contradiction
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }

                    if (goal != null && goalIsNegated && goal.Equals(normalized))
                    {
                        return true;
                    }
                    if (CheckForContradiction(posAssumptions, normalized))
                    {
                        return true;
                    }
                }
                else
                {
                    if (normalized.IsConstant)
                    {
                        var c = normalized.Constant;
                        if (!(c is string))
                        {
                            if (c == null)
                            { // null constant as assumption is contradiction
                                return true;
                            }
                            var ic = c as IConvertible;
                            if (ic != null)
                            {
                                try
                                {
                                    int value = ic.ToInt32(null);
                                    if (value == 0)
                                    {
                                        return true; // contradiction
                                    }
                                    else
                                    {
                                        return false; // don't add trivial "true" to assumptions
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    if (goal != null && !goalIsNegated && goal.Equals(normalized))
                    {
                        return true;
                    }

                    if (CheckForContradiction(negAssumptions, normalized))
                    {
                        return true;
                    }
                }
                return false;
            }

            private bool ContainsVariable<Variable>(FList<BoxedExpression> assumptions, Variable original)
            {
                while (assumptions != null)
                {
                    var boxed = assumptions.Head;
                    if (boxed.UnderlyingVariable != null && boxed.UnderlyingVariable.Equals(original)) return true;
                    assumptions = assumptions.Tail;
                }
                return false;
            }
            private bool ContainsVariables<Variable>(FList<BoxedExpression> assumptions, Variable left, Variable right)
            {
                if (left.Equals(right))
                {
                    return ContainsVariable(assumptions, left);
                }

                while (assumptions != null)
                {
                    Variable assumptionVar;
                    if (assumptions.Head.TryGetFrameworkVariable(out assumptionVar))
                    {
                        if (assumptionVar.Equals(left))
                        {
                            // search the other
                            return ContainsVariable(assumptions, right);
                        }
                        if (assumptionVar.Equals(right))
                        {
                            return ContainsVariable(assumptions, left);
                        }
                    }
                    assumptions = assumptions.Tail;
                }
                return false;
            }

            internal static string ConditionStringAtPC<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
              APC pc,
              BoxedExpression condition,
              IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
              IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder
            )
              where Type : IEquatable<Type>
            {
                return ConditionStringAtPC(pc, condition, context, mdDecoder, false);
            }

            internal static string ConditionStringAtPC<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
              APC pc,
              BoxedExpression condition,
              IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
              IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
              bool omitPath
            )
              where Type : IEquatable<Type>
            {
                if (condition == null) return "<null>";
                // simply substitute variable nodes with their access paths
                condition = condition.Substitute(delegate (Variable variable, BoxedExpression original)
                {
                    if (omitPath) return BoxedExpression.Var(variable);
                    string accessPath = context.ValueContext.AccessPath(pc, variable);
                    if (accessPath != null) return BoxedExpression.Var(accessPath);
                    return original;
                });

                if (condition == null) return "<null>";

                return condition.ToString();
            }

            public string ObligationStringAtPC<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
              APC pc,
              IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
              IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder
            )
              where Type : IEquatable<Type>
            {
                return ObligationStringAtPC(pc, context, mdDecoder, false);
            }

            public string ObligationStringAtPC<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
              APC pc,
              IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
              IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
              bool omitPath
            )
              where Type : IEquatable<Type>
            {
                StringBuilder sb = new StringBuilder();
                if (this.Size > 0)
                {
                    bool first = true;
                    foreach (var posAssumption in posAssumptions.GetEnumerable())
                    {
                        if (!first)
                        {
                            sb.Append("&& ");
                        }
                        else
                        {
                            first = false;
                        }
                        string aString = ConditionStringAtPC(pc, posAssumption, context, mdDecoder, omitPath);
                        sb.Append(aString);
                        sb.Append(' ');
                    }
                    foreach (var negAssumption in negAssumptions.GetEnumerable())
                    {
                        if (!first)
                        {
                            sb.Append("&& ");
                        }
                        else
                        {
                            first = false;
                        }
                        string aString = ConditionStringAtPC(pc, negAssumption, context, mdDecoder, omitPath);
                        sb.Append('!');
                        sb.Append(aString);
                        sb.Append(' ');
                    }
                    sb.Append("=> ");
                }
                string cond = ConditionStringAtPC(pc, goal, context, mdDecoder, omitPath);
                if (goalIsNegated)
                {
                    sb.AppendFormat("!({0})", cond);
                }
                else
                {
                    sb.Append(cond);
                }
                return sb.ToString();
            }


            internal bool VisitedLoop(APC aPC)
            {
                return loopHeads.Contains(aPC);
            }

            internal Path RemoveTautologiesAndDuplicatedFacts()
            {
                var posAssumptionsWithoutTrue = FilterTrivialitiesAndDuplicatedFacts(posAssumptions, true);
                var negAssumptionsWithoutFalse = FilterTrivialitiesAndDuplicatedFacts(negAssumptions, false);

                return new Path(codePath, posAssumptionsWithoutTrue, negAssumptionsWithoutFalse, goal, goalIsNegated, loopHeads); ;
            }

            private FList<BoxedExpression> FilterTrivialitiesAndDuplicatedFacts(FList<BoxedExpression> original, bool posPolarity)
            {
                var constant = posPolarity ? 1 : 0;

                var result = FList<BoxedExpression>.Empty;

                var removed = 0;

                var dict = new Set<BoxedExpression>();

                foreach (var be in original.GetEnumerable())
                {
                    // Remove true (resp. false) constants
                    int value;
                    if (be.IsConstantIntOrNull(out value) && value == constant)
                    {
                        removed++;
                        continue;
                    }

                    BinaryOperator bop;
                    BoxedExpression left, right;
                    int k1, k2;
                    if (be.IsBinaryExpression(out bop, out left, out right))
                    {
                        // Remove simple tautologies in the form k1 bop k2, with k1, k2 constants
                        if (left.IsConstantIntOrNull(out k1) && right.IsConstantIntOrNull(out k2))
                        {
                            switch (bop)
                            {
                                case BinaryOperator.Ceq:
                                    if (IsTriviality(posPolarity, k1 == k2))
                                    {
                                        removed++;
                                        continue;
                                    }
                                    break;

                                case BinaryOperator.Cge:
                                    if (IsTriviality(posPolarity, k1 >= k2))
                                    {
                                        removed++;
                                        continue;
                                    }
                                    break;

                                case BinaryOperator.Cgt:
                                    if (IsTriviality(posPolarity, k1 > k2))
                                    {
                                        removed++;
                                        continue;
                                    }
                                    break;

                                case BinaryOperator.Cle:
                                    if (IsTriviality(posPolarity, k1 <= k2))
                                    {
                                        removed++;
                                        continue;
                                    }
                                    break;

                                case BinaryOperator.Clt:
                                    if (IsTriviality(posPolarity, k1 < k2))
                                    {
                                        removed++;
                                        continue;
                                    }
                                    break;

                                case BinaryOperator.Cne_Un:
                                    if (IsTriviality(posPolarity, k1 != k2))
                                    {
                                        removed++;
                                        continue;
                                    }
                                    break;

                                // We do nothing
                                default:
                                    break;
                            }
                        }

                        // Remove duplicated binary expressions
                        if (dict.Contains(be))
                        {
                            removed++;
                            continue;
                        }
                        else
                        {
                            dict.Add(be);
                        }
                    }

                    result = FList<BoxedExpression>.Cons(be, result);
                }

                if (Trace && removed > 0 && result != null)
                {
                    Console.WriteLine("Removed {0} trivialites ({1}).\nWas:\n {2},\nIs:\n {3}", removed, posPolarity, original, result);
                }

                return result;
            }

            private bool IsTriviality(bool polarity, bool condition)
            {
                return (polarity && condition) || (!polarity && !condition);
            }
        }

        public static class TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
          where Expression : IEquatable<Expression>
          where Variable : IEquatable<Variable>
          where LogOptions : IFrameworkLogOptions
          where Type : IEquatable<Type>
        {
            public static Path DischargeObligation(
              IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
              IFactQuery<BoxedExpression, Variable> facts,
              ContractInferenceManager inferenceManager,
              int maxPathSize,
              APC pc, Variable goal, out AdditionalInfo why)
            {
                Contract.Requires(mdriver != null);
                Contract.Requires(facts != null);

                var goalExpression = BoxedExpression.Convert(mdriver.Context.ExpressionContext.Refine(pc, goal), mdriver.ExpressionDecoder);

                return DischargeObligation(mdriver, facts, inferenceManager, maxPathSize, pc, goalExpression, out why);
            }

            public static Path DischargeObligation(
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
             IFactQuery<BoxedExpression, Variable> facts,
                     ContractInferenceManager inferenceManager,
            int maxPathSize,
            APC pc, BoxedExpression goalExpression, out AdditionalInfo why)
            {
                Contract.Requires(facts != null);

                if (Trace)
                {
                    Console.WriteLine("[WP] Trying to prove the condition {0}", goalExpression);
                }

                if (facts.IsUnreachable(pc))
                {
                    why = AdditionalInfo.None;
                    return null;
                }

                switch (facts.IsTrue(pc, goalExpression))
                {
                    case ProofOutcome.True:
                    case ProofOutcome.Bottom:
                        {
                            why = AdditionalInfo.None;
                            return null;
                        }

                    default:
                        {
                            break;
                        }
                }

                var paths = FList<Path>.Cons(new Path(pc, goalExpression), null);
                var exploration = new Exploration(mdriver, facts, inferenceManager, maxPathSize);

                var result = exploration.DischargePaths(paths, out why);

                if (Trace)
                {
                    Console.WriteLine("  [WP] Outcome: {0}", result == null ? "true" : "top");
                }

                return result;
            }

            private class Exploration : IEdgeVisit<APC, Local, Parameter, Method, Field, Type, Variable, Path>
            {
                private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver;
                private IFactQuery<BoxedExpression, Variable> facts;

                private readonly ContractInferenceManager inferenceManager;
                private readonly int MaxPathSize;

                public Exploration(
                  IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
                  IFactQuery<BoxedExpression, Variable> facts,
                            ContractInferenceManager inferenceManager,
                  int maxPathSize
                )
                {
                    this.mdriver = mdriver;
                    this.facts = facts;
                    this.inferenceManager = inferenceManager;
                    MaxPathSize = maxPathSize;
                }

                /// <summary>
                /// Returns a path that it cannot prove or null if all paths are provable
                /// Assumes that the paths in the list at their current program point are unprovable
                /// (thus starts by expanding paths, rather than proving current point).
                /// </summary>
                public Path DischargePaths(FList<Path> paths, out AdditionalInfo why)
                {
                    why = AdditionalInfo.None;
                    while (paths != null)
                    {
                        Timeout.CheckTimeOut("wp");

                        // pick first path and current PC
                        var head = paths.Head;
                        paths = paths.Tail;

                        // expand path
                        if (ExpandPath(head, ref paths, out why))
                        {
                            continue;
                        }
                        // found a path we can't prove
                        if (Trace)
                        {
                            Console.WriteLine("Can't prove the following {0}:", head.ObligationStringAtPC(head.PC, mdriver.Context, mdriver.MetaDataDecoder));
                            foreach (var pathpc in head.CodePath.GetEnumerable())
                            {
                                Console.WriteLine("   {0}", pathpc.ToString());
                            }
                        }
                        return head;
                    }
                    return null; // success
                }

                private bool ExpandPath(Path head, ref FList<Path> paths, out AdditionalInfo why)
                {
                    if (Trace)
                    {
                        Console.WriteLine("{0}: ExpandPath {1}", head.PC.ToString(), head.ObligationStringAtPC(head.PC, mdriver.Context, mdriver.MetaDataDecoder));
                    }
                    var CFG = mdriver.StackLayer.Decoder.Context.MethodContext.CFG;
                    var hasPred = false;
                    foreach (var pred in CFG.Predecessors(head.PC))
                    {
                        Timeout.CheckTimeOut("wp");

                        if (Trace)
                        {
                            Console.WriteLine("-Visiting {0}", pred);
                        }

                        if (facts.IsUnreachable(pred))
                        {
                            if (Trace)
                            {
                                Console.WriteLine("-- {0} is unreachable", pred);
                            }

                            hasPred = true;
                        }
                        else
                        {
                            var loopHead = CFG.IsForwardBackEdgeTarget(pred);

                            if (loopHead && head.VisitedLoop(pred) || head.Size > MaxPathSize)
                            {
                                var outcome = TryProvingTheImplication(head);

                                if (outcome)
                                {
                                    why = AdditionalInfo.None;
                                }
                                else
                                {
                                    why = new AdditionalInfo()
                                    {
                                        ReachedMethodEntry = loopHead /*&& head != null*/ && head.VisitedLoop(pred),
                                        ReachedMaximumSize = /*head != null && */head.Size > MaxPathSize
                                    };
                                }

                                if (Trace)
                                {
                                    Console.WriteLine("-- Aborting the visit at {0}. Either we reached a loophead and we already visited it, or the headsize is too large", pred);
                                }

                                return outcome;
                            }

                            var nextPath = mdriver.BackwardTransfer(head.PC, pred, head.ExpandBy(pred, loopHead), this);

                            // only try to prove things at source of a join edge rather than each program point
                            if (nextPath != null && CFG.IsJoinPoint(head.PC))
                            {
                                if (Trace)
                                {
                                    Console.WriteLine("-- After backwards transfer, we reached a join point. We try to discharge the proof obligation", pred);
                                }

                                if (DischargePath(nextPath))
                                {
                                    nextPath = null; // kill this path

                                    if (Trace)
                                    {
                                        Console.WriteLine("--- We were able to discharge the Path condition");
                                    }
                                }
                            }

                            // transfer may kill a path
                            if (nextPath != null)
                            {
                                paths = paths.Cons(nextPath);
                                if (Trace)
                                {
                                    Console.WriteLine(" --- adding the path to the list of paths. Current path length {0}", paths.Length());
                                }
                            }
                            else if (Trace)
                            {
                                Console.WriteLine("Path discharged.");
                            }

                            hasPred = true;
                        }
                    }
                    if (hasPred)
                    {
                        // not stuck
                        why = AdditionalInfo.None;
                        return true;
                    }

                    var direct = TryProvingTheImplication(head);

                    if (!hasPred && !direct)
                    {
                        if (Trace)
                        {
                            Console.WriteLine("Hit a PC with no predecessors");
                            Console.WriteLine("-- We were not able to prove the implication");
                        }

                        if (EmitSMT2Formula)
                        {
                            EmitImplicationInSMTLibFormat(head);
                        }
                    }

                    why = new AdditionalInfo() { ReachedMethodEntry = true };

                    return direct;
                }

                private bool TryProvingTheImplication(Path head)
                {
                    // If it is a too complex CFG, we save time and do not run the relatively expensive implication checking
                    if (mdriver.SyntacticComplexity.TooManyJoins)
                    {
                        if (Trace)
                        {
                            Console.WriteLine("Skipping TryProvingImplication as the Clousot thinks there are too many joins in this method");
                        }
                        return false;
                    }

                    if (head.Goal != null)
                    {  // if goal != null, we should prove that PosAssumptions ==> Goal
                        var goal = head.GoalIsNegated ? BoxedExpression.Unary(UnaryOperator.Not, head.Goal) : head.Goal;

                        var posAssumptions = head.PosAssumptions.Append(SaturatePremises(head.PosAssumptions.ToArray()).ToFList());

                        var outcome = facts.IsTrueImply(head.PC, posAssumptions, head.NegAssumptions, goal);

                        if (outcome == ProofOutcome.True)
                        {
                            if (Trace)
                            {
                                Console.WriteLine("Goal implied by assumptions and facts");
                            }
                            return true;
                        }

                        return false;
                    }
                    else
                    { // if the goal is null, then we should prove !(PosAssumptions)
                        if (head.PosAssumptions != null)
                        {
                            foreach (var assumption in head.PosAssumptions.GetEnumerable())
                            {
                                if (facts.IsTrue(head.PC, assumption) == ProofOutcome.False)
                                {
                                    if (Trace)
                                    {
                                        Console.WriteLine("Assumption {0} is false according to the facts", assumption);
                                    }
                                    return true;
                                }
                            }
                        }

                        return false;
                    }
                }

                /// <summary>
                /// Only called on pos assumption
                /// </summary>
                private List<BoxedExpression> SaturatePremises(BoxedExpression[] assumptions)
                {
                    Contract.Requires(assumptions != null);
                    Contract.Requires(Contract.ForAll(assumptions, assume => assume != null));
                    Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

                    var toAdd = new List<BoxedExpression>();

                    for (var i = 0; i < assumptions.Length; i++)
                    {
                        var exp = assumptions[i];
                        if (exp.IsVariable)
                        {
                            for (var j = 0; j < assumptions.Length; j++)
                            {
                                if (i != j)
                                {
                                    var otherExp = assumptions[j];
                                    BinaryOperator bop;
                                    BoxedExpression left, right;
                                    if (otherExp.IsBinaryExpression(out bop, out left, out right) && (bop == BinaryOperator.Cobjeq || bop == BinaryOperator.Ceq))
                                    {
                                        if (left.Equals(exp))
                                        {
                                            toAdd.Add(right);
                                        }
                                        else if (right.Equals(exp))
                                        {
                                            toAdd.Add(left);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (Trace)
                    {
                        if (toAdd.Count > 0)
                        {
                            Console.WriteLine("Saturation inferred new premises to use in proving the implication: {0}", toAdd);
                        }
                        else
                        {
                            Console.WriteLine("No new premise inferred by saturation");
                        }
                    }

                    return toAdd;
                }

                private bool DischargePath(Path head)
                {
                    var decoder = mdriver.MetaDataDecoder;
                    if (Trace)
                    {
                        Console.WriteLine("{1}: Trying to prove goal: {0}", head.ObligationStringAtPC(head.PC, mdriver.Context, decoder), head.PC.ToString());
                        Console.WriteLine("{1}: Trying to prove goal: {0}", head.ObligationStringAtPC(head.PC, mdriver.Context, decoder, true), head.PC.ToString());
                    }
                    if (head.Goal != null)
                    {
                        var outcome = facts.IsTrue(head.PC, head.Goal);
                        if (!head.GoalIsNegated && outcome == ProofOutcome.True ||
                            head.GoalIsNegated && outcome == ProofOutcome.False)
                        {
                            if (Trace)
                            {
                                Console.WriteLine("Goal implied by facts");
                            }
                            return true;
                        }

                        if (TryProvingTheImplication(head))
                        {
                            return true;
                        }
                    }
                    else // We look for a contraddiction
                    {
                        if (mdriver.SyntacticComplexity.TooManyJoinsForBackwardsChecking)
                        {
                            // Do nothing

                            if (Trace)
                            {
                                Console.WriteLine("Skipping the relative costly operation facts.TryProveImplication");
                            }
                        }
                        else
                        {
                            var ff = BoxedExpression.Const(0, decoder.System_Int32, decoder);
                            if (facts.IsTrueImply(head.PC, head.PosAssumptions, head.NegAssumptions, ff) == ProofOutcome.True)
                            {
                                if (Trace)
                                {
                                    Console.WriteLine("  Found a contraddiction at program point {0}", head.PC);
                                }
                                return true;
                            }
                        }
                    }

                    // try to see if any of the assumptions are contradicted in current state
                    foreach (var posAssumption in head.PosAssumptions.GetEnumerable())
                    {
                        if (facts.IsTrue(head.PC, posAssumption) == ProofOutcome.False)
                        {
                            if (Trace)
                            {
                                Console.WriteLine("   Assumption {0} is false", Path.ConditionStringAtPC(head.PC, posAssumption, mdriver.Context, decoder));
                            }
                            return true;
                        }
                    }
                    foreach (var negAssumption in head.NegAssumptions.GetEnumerable())
                    {
                        if (facts.IsTrue(head.PC, negAssumption) == ProofOutcome.True)
                        {
                            if (Trace)
                            {
                                Console.WriteLine("   Assumption !({0}) is false", Path.ConditionStringAtPC(head.PC, negAssumption, mdriver.Context, decoder));
                            }
                            return true;
                        }
                    }

                    return false;
                }

                #region IEdgeVisit<APC,Local,Parameter,Method,Field,Type,Variable,Path> Members

                private BoxedExpression Rename(APC pc, Variable v, BoxedExpression original, IFunctionalMap<Variable, Variable> renaming)
                {
                    if (renaming.Contains(v))
                    {
                        Variable v2 = renaming[v];
                        //if (v2.Equals(v)) return original;
                        var candidate = BoxedExpression.Convert(mdriver.Context.ExpressionContext.Refine(pc, v2), mdriver.ExpressionDecoder);

                        if (candidate == null)
                        {
                            return null;
                        }

                        if (candidate.IsVariable && v.Equals(v2)) return original;
                        return candidate;
                    }
                    return null;
                }

                [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant", Justification = "Bug in Clousot that thinks Substitute returns != null, because it does not take into account a parameter havoc")]
                public Path Rename(APC from, APC to, Path path, IFunctionalMap<Variable, Variable> renaming)
                {
                    if (Trace)
                    {
                        Console.WriteLine("Rename from {0} to {1}", from, to);
                        Console.WriteLine("  Path prior to substitution: {0}", path.ObligationStringAtPC(from, mdriver.Context, mdriver.MetaDataDecoder));
                        Console.WriteLine("  Path prior to substitution (as symbolic variables) : {0}", path.ObligationStringAtPC(from, mdriver.Context, mdriver.MetaDataDecoder, true));
                        Console.WriteLine(" Substitution:");
                        renaming.Visit(delegate (Variable key, Variable value) { Console.WriteLine("  {0} -> {1}", key, value); return VisitStatus.ContinueVisit; });

                        renaming.Visit(delegate (Variable key, Variable value) { Console.WriteLine("  {0} -> Expr {1}", key, BoxedExpression.Convert(mdriver.Context.ExpressionContext.Refine(to, value), mdriver.ExpressionDecoder)); return VisitStatus.ContinueVisit; });
                    }
                    Func<Variable, BoxedExpression, BoxedExpression> converter =
                      (variable, original) =>
                      {
                          var newName = Rename(to, variable, original, renaming);
                          Variable newVarName;
                          if (newName != null && newName.TryGetFrameworkVariable(out newVarName))
                          {
                              var tryForAll = mdriver.AsForAllIndexed(to, newVarName);
                              if (tryForAll != null)
                                  newName = tryForAll;
                          }

                          return newName;
                      };

                    var result = path.Substitute(converter);

                    result = result != null ? result.RemoveTautologiesAndDuplicatedFacts() : null;

                    if (Trace)
                    {
                        if (result != null)
                        {
                            Console.WriteLine("  Path after substitution: {0}", result.ObligationStringAtPC(to, mdriver.Context, mdriver.MetaDataDecoder));
                            Console.WriteLine("  Path after substitution: {0}", result.ObligationStringAtPC(to, mdriver.Context, mdriver.MetaDataDecoder, true));
                        }
                        else
                        {
                            Console.WriteLine("Substitution discharged path");
                        }
                    }
                    return result;
                }

                #endregion

                #region IVisitMSIL<APC,Local,Parameter,Method,Field,Type,Variable,Variable,Path,Path> Members

                public Path Arglist(APC pc, Variable dest, Path data)
                {
                    return data;
                }

                public Path BranchCond(APC pc, APC target, BranchOperator bop, Variable value1, Variable value2, Path data)
                {
                    return data;
                }

                public Path BranchTrue(APC pc, APC target, Variable cond, Path data)
                {
                    return data;
                }

                public Path BranchFalse(APC pc, APC target, Variable cond, Path data)
                {
                    return data;
                }

                public Path Branch(APC pc, APC target, bool leave, Path data)
                {
                    return data;
                }

                public Path Break(APC pc, Path data)
                {
                    return data;
                }

                [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
                public Path Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, Path data)
                  where TypeList : IIndexable<Type>
                  where ArgList : IIndexable<Variable>
                {
                    // We'd love to have it, but then we should also take care of the changes of array elements in stelem, non-pure method calls, etc.
                    // The only point were we do the ForAll expansion is in the renaming
                    // The code is here only to help the debugging
#if false
                    var tryForAll = mdriver.AsForAllIndexed(pc.Post(), dest);
                    if (tryForAll != null)
                    {
                        if (Trace)
                        {
                            Console.WriteLine("Hit a call to a ForAll. Replacing {0} with {1}", dest, tryForAll);
                        }

                        Func<Variable, BoxedExpression, BoxedExpression> substituition = (v, original) => v.Equals(dest) ? tryForAll : original;

                        return data.Substitute(substituition);
                    }
#endif
                    if (Trace)
                    {
                        Console.WriteLine("  Hit a method call: {0}", method);
                    }

                    // We do some magic for Equality and Inequality, to be used for proving properties of structs
                    if (data.Goal != null && data.Goal.Variables<Variable>().Contains(dest))
                    {
                        var mdd = mdriver.MetaDataDecoder;
                        var methodName = mdd.Name(method);
                        var context = mdriver.Context.ExpressionContext;

                        switch (methodName)
                        {
                            case "op_Equality":
                                {
                                    return BuildExpression<ArgList>(BinaryOperator.Ceq, ref pc, dest, args, data, context);
                                }

                            case "op_Inequality":
                                {
                                    return BuildExpression<ArgList>(BinaryOperator.Cne_Un, ref pc, dest, args, data, context);
                                }
                        }
                    }
                    return data;
                }

                private Path BuildExpression<ArgList>(BinaryOperator bop, ref APC pc, Variable var, ArgList args, Path data, IExpressionContextData<Local, Parameter, Method, Field, Type, Expression, Variable> context) where ArgList : IIndexable<Variable>
                {
                    Contract.Requires(data.Goal != null);

                    var left = BoxedExpression.Convert(context.Refine(pc, args[0]), mdriver.ExpressionDecoder); if (left == null) return data;
                    var right = BoxedExpression.Convert(context.Refine(pc, args[1]), mdriver.ExpressionDecoder); if (right == null) return data;
                    var exp = BoxedExpression.Binary(bop, left, right, var);

                    var newGoal = data.Goal.Substitute(
                      delegate (Variable v, BoxedExpression original)
                      {
                          if (var.Equals(v)) return exp;
                          else return original;
                      }
                    );

                    if (Trace)
                    {
                        Console.WriteLine("  Replace a sub-goal with the binary exp {0}", exp);
                        Console.WriteLine("  New goal {0}", newGoal);
                    }

                    return newGoal != null ? data.WithGoal(newGoal) : data;
                }

                public Path Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Variable dest, Variable fp, ArgList args, Path data)
                  where TypeList : IIndexable<Type>
                  where ArgList : IIndexable<Variable>
                {
                    return data;
                }

                public Path Ckfinite(APC pc, Variable dest, Variable source, Path data)
                {
                    return data;
                }

                public Path Cpblk(APC pc, bool @volatile, Variable destaddr, Variable srcaddr, Variable len, Path data)
                {
                    return data;
                }

                public Path Endfilter(APC pc, Variable decision, Path data)
                {
                    return data;
                }

                public Path Endfinally(APC pc, Path data)
                {
                    return data;
                }

                public Path Initblk(APC pc, bool @volatile, Variable destaddr, Variable value, Variable len, Path data)
                {
                    return data;
                }

                public Path Jmp(APC pc, Method method, Path data)
                {
                    return data;
                }

                public Path Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, Path data)
                {
                    return data;
                }

                public Path Ldarga(APC pc, Parameter argument, bool isOld, Variable dest, Path data)
                {
                    return data;
                }

                public Path Ldftn(APC pc, Method method, Variable dest, Path data)
                {
                    return data;
                }

                public Path Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, Path data)
                {
                    return data;
                }

                public Path Ldloc(APC pc, Local local, Variable dest, Path data)
                {
                    return data;
                }

                public Path Ldloca(APC pc, Local local, Variable dest, Path data)
                {
                    return data;
                }

                public Path Localloc(APC pc, Variable dest, Variable size, Path data)
                {
                    return data;
                }

                public Path Nop(APC pc, Path data)
                {
                    return data;
                }

                public Path Pop(APC pc, Variable source, Path data)
                {
                    return data;
                }

                public Path Return(APC pc, Variable source, Path data)
                {
                    return data;
                }

                public Path Starg(APC pc, Parameter argument, Variable source, Path data)
                {
                    return data;
                }

                public Path Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, Path data)
                {
                    return data;
                }

                public Path Stloc(APC pc, Local local, Variable source, Path data)
                {
                    return data;
                }

                public Path Switch(APC pc, Type type, System.Collections.Generic.IEnumerable<Pair<object, APC>> cases, Variable value, Path data)
                {
                    return data;
                }

                public Path Box(APC pc, Type type, Variable dest, Variable source, Path data)
                {
                    return data;
                }

                public Path ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Variable dest, ArgList args, Path data)
                  where TypeList : IIndexable<Type>
                  where ArgList : IIndexable<Variable>
                {
                    return data;
                }

                public Path Castclass(APC pc, Type type, Variable dest, Variable obj, Path data)
                {
                    return data;
                }

                public Path Cpobj(APC pc, Type type, Variable destptr, Variable srcptr, Path data)
                {
                    return data;
                }

                public Path Initobj(APC pc, Type type, Variable ptr, Path data)
                {
                    return data;
                }

                public Path Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, Path data)
                {
                    return data;
                }

                public Path Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, Path data)
                {
                    return data;
                }

                public Path Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, Path data)
                {
                    return data;
                }

                public Path Ldflda(APC pc, Field field, Variable dest, Variable obj, Path data)
                {
                    return data;
                }

                public Path Ldlen(APC pc, Variable dest, Variable array, Path data)
                {
                    return data;
                }

                public Path Ldsfld(APC pc, Field field, bool @volatile, Variable dest, Path data)
                {
                    return data;
                }

                public Path Ldsflda(APC pc, Field field, Variable dest, Path data)
                {
                    return data;
                }

                public Path Ldtypetoken(APC pc, Type type, Variable dest, Path data)
                {
                    return data;
                }

                public Path Ldfieldtoken(APC pc, Field field, Variable dest, Path data)
                {
                    return data;
                }

                public Path Ldmethodtoken(APC pc, Method method, Variable dest, Path data)
                {
                    return data;
                }

                public Path Ldvirtftn(APC pc, Method method, Variable dest, Variable obj, Path data)
                {
                    return data;
                }

                public Path Mkrefany(APC pc, Type type, Variable dest, Variable obj, Path data)
                {
                    return data;
                }

                public Path Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList len, Path data) where ArgList : IIndexable<Variable>
                {
                    return data;
                }

                public Path Newobj<ArgList>(APC pc, Method ctor, Variable dest, ArgList args, Path data) where ArgList : IIndexable<Variable>
                {
                    return data;
                }

                public Path Refanytype(APC pc, Variable dest, Variable source, Path data)
                {
                    return data;
                }

                public Path Refanyval(APC pc, Type type, Variable dest, Variable source, Path data)
                {
                    return data;
                }

                public Path Rethrow(APC pc, Path data)
                {
                    return data;
                }

                public Path Stelem(APC pc, Type type, Variable array, Variable index, Variable value, Path data)
                {
                    return data;
                }

                public Path Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, Path data)
                {
                    return data;
                }

                public Path Stsfld(APC pc, Field field, bool @volatile, Variable value, Path data)
                {
                    return data;
                }

                public Path Throw(APC pc, Variable exn, Path data)
                {
                    return data;
                }

                public Path Unbox(APC pc, Type type, Variable dest, Variable obj, Path data)
                {
                    return data;
                }

                public Path Unboxany(APC pc, Type type, Variable dest, Variable obj, Path data)
                {
                    return data;
                }

                #endregion

                #region IVisitSynthIL<APC,Method,Type,Variable,Variable,Path,Path> Members

                public Path Entry(APC pc, Method method, Path data)
                {
                    return data;
                }

                public Path Assume(APC pc, string tag, Variable condition, object provenance, Path data)
                {
                    var expression = BoxedExpression.Convert(mdriver.Context.ExpressionContext.Refine(pc, condition), mdriver.ExpressionDecoder);

                    if (Trace)
                    {
                        Console.WriteLine("  Assume({2}) reached: non-normalized {0} {1}", condition, expression, tag);
                    }

                    if (expression == null)
                    {
                        // abstract the assumption, if too big

                        if (Trace)
                        {
                            Console.WriteLine("       We ignore the condition, as it is too big");
                        }

                        return data;
                    }

                    var originalNegated = false;
                    if (tag == "false")
                    {
                        // Try to avoid double negation
                        if (expression.IsUnary && expression.UnaryOp == UnaryOperator.Not)
                        {
                            expression = expression.UnaryArgument;
                            originalNegated = true; // check check? 
                        }
                        else
                        {
                            // negate
                            expression = BoxedExpression.Unary(UnaryOperator.Not, expression);
                            originalNegated = true;
                        }
                    }


#if false
                    else if (tag == "assume" && pc.InsideRequiresAtCall)
                    {
                        // special hack: ThrowOtherwise are extracted with an extra assume(false) on the
                        // bad branch so the forward analysis actually gets some state out of it.
                        // On the backward proofs at call sites, we need to ignore that assume
                        if (expression.IsConstant)
                        {
                            object c = expression.Constant;
                            if (c is int)
                            {
                                int ic = (int)c;
                                if (ic == 0)
                                { // ignore this assume
                                    return data;
                                }
                            }
                        }
                    }
#endif
                    var simplified = expression.Simplify(mdriver.MetaDataDecoder);

                    // We try to simplify the goal -- we know that the assumption holds, so we can rewrite the goal with "true"
                    if (data.Goal != null)
                    {
                        if (IsSuitableForReplacing(pc.Post(), simplified))
                        {
                            var newGoal = data.Goal.Substitute(simplified, BoxedExpression.ConstBool(true, mdriver.MetaDataDecoder));

                            // nothing changed...
                            if (data.Goal.Equals(newGoal))
                            {
                                // if we know !b, then we want to replace b with "false"
                                if (simplified.IsUnary && simplified.UnaryOp == UnaryOperator.Not)
                                {
                                    newGoal = newGoal.Substitute(simplified.UnaryArgument, BoxedExpression.ConstBool(false, mdriver.MetaDataDecoder));
                                }
                            }

                            data = data.WithGoal(newGoal);
                        }
                    }

                    var newAssumptions = data.AddAssumption(simplified, condition, originalNegated, mdriver.VariableComparer);

                    if (newAssumptions != null)
                    {
                        if (TryProvingTheImplication(newAssumptions))
                        {
                            if (Trace)
                            {
                                Console.WriteLine("  Path discharged thanks to new facts from the assumption");
                            }
                            return null;
                        }
                    }

                    return newAssumptions;
                }

                private bool IsSuitableForReplacing(APC pc, BoxedExpression simplified)
                {
                    Contract.Ensures(!Contract.Result<bool>() || simplified != null);

                    if (simplified == null)
                    {
                        return false;
                    }

                    Variable v;
                    if (simplified.TryGetFrameworkVariable(out v))
                    {
                        var t = mdriver.Context.ValueContext.GetType(pc, v);
                        return t.IsNormal && mdriver.MetaDataDecoder.System_Boolean.Equals(t.Value);
                    }

                    return false;
                }

                public Path Assert(APC pc, string tag, Variable condition, object provenance, Path data)
                {
                    var expression = BoxedExpression.Convert(mdriver.Context.ExpressionContext.Refine(pc, condition), mdriver.ExpressionDecoder);

                    if (expression == null)
                    {
                        return data; // Let's skip the assumption
                    }

                    var newAssumptions = data.AddAssumption(expression, condition, false, mdriver.VariableComparer);
                    if (newAssumptions != null)
                    {
                        if (TryProvingTheImplication(newAssumptions))
                        {
                            if (Trace)
                            {
                                Console.WriteLine("  Path discharged thanks to new facts from the assertion");
                            }
                            return null;
                        }
                    }

                    return newAssumptions;
                }

                public Path Ldstack(APC pc, int offset, Variable dest, Variable source, bool isOld, Path data)
                {
                    return data;
                }

                public Path Ldstacka(APC pc, int offset, Variable dest, Variable source, Type type, bool isOld, Path data)
                {
                    return data;
                }

                public Path Ldresult(APC pc, Type type, Variable dest, Variable source, Path data)
                {
                    return data;
                }

                public Path BeginOld(APC pc, APC matchingEnd, Path data)
                {
                    return data;
                }

                public Path EndOld(APC pc, APC matchingBegin, Type type, Variable dest, Variable source, Path data)
                {
                    return data;
                }

                #endregion

                #region IVisitExprIL<APC,Type,Variable,Variable,Path,Path> Members

                public Path Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, Path data)
                {
                    return data;
                }

                public Path Isinst(APC pc, Type type, Variable dest, Variable obj, Path data)
                {
                    return data;
                }

                public Path Ldconst(APC pc, object constant, Type type, Variable dest, Path data)
                {
                    return data;
                }

                public Path Ldnull(APC pc, Variable dest, Path data)
                {
                    return data;
                }

                public Path Sizeof(APC pc, Type type, Variable dest, Path data)
                {
                    return data;
                }

                public Path Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, Path data)
                {
                    return data;
                }

                #endregion

                #region SMT-LIB Related

                private void EmitImplicationInSMTLibFormat(Path head)
                {
                    var reader = new BoxedExpressionsToSMT2(obj => TypeFor(head.PC, obj));

                    var definitions = new SMTFunctionDefinitions();

                    var facts = "";
                    var not_goal = "";
                    var first = true;

                    if (head.PosAssumptions != null)
                    {
                        foreach (var pos in head.PosAssumptions.GetEnumerable())
                        {
                            if (reader.Visit(pos, definitions) != null)
                            {
                                facts = And(ref first, facts, reader.ResultValue);
                            }
                        }
                    }
                    if (head.NegAssumptions != null)
                    {
                        foreach (var neg in head.NegAssumptions.GetEnumerable())
                        {
                            if (reader.Visit(BoxedExpression.Unary(UnaryOperator.Not, neg), definitions) != null)
                            {
                                facts = And(ref first, facts, reader.ResultValue);
                            }
                        }
                    }

                    if (head.Goal != null)
                    {
                        var what = head.GoalIsNegated ? head.Goal : BoxedExpression.Unary(UnaryOperator.Not, head.Goal);
                        if (reader.Visit(what, definitions) != null)
                        {
                            not_goal = reader.ResultValue;
                        }
                    }
                    else
                    {
                        not_goal = "true";
                    }

                    Console.WriteLine("SMT-Output");
                    Console.WriteLine("(set-logic QF_FPA)");
                    foreach (var def in definitions.Declarations)
                    {
                        Console.WriteLine(def);
                    }
                    Console.WriteLine(string.Format("(assert (and {0} {1}))", facts, not_goal));
                    Console.WriteLine("(check-sat)");
                    Console.WriteLine("(get-model)");
                }

                private string And(ref bool isFirst, string facts, string fact)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        return fact;
                    }

                    return string.Format("(and {0} {1})", facts, fact);
                }

                private System.Type TypeFor(APC where, object obj)
                {
                    if (obj is Variable)
                    {
                        var v = (Variable)obj;
                        var t = mdriver.Context.ValueContext.GetType(where.Post(), v);
                        if (t.IsNormal)
                        {
                            var md = mdriver.MetaDataDecoder;
                            if (t.Equals(md.System_Int32))
                            {
                                return typeof(System.Int32);
                            }
                            if (t.Equals(md.System_Single))
                            {
                                return typeof(System.Single);
                            }
                            if (t.Equals(md.System_Double))
                            {
                                return typeof(System.Double);
                            }
                            // TODO

                        }
                    }

                    return null;
                }

                #endregion
            }
        }
    }
}
