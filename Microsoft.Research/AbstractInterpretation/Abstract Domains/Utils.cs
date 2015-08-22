// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using System.Linq;

namespace Microsoft.Research.AbstractDomains
{
    /// <summary>
    /// Some helper method for the abstract domains
    /// </summary>
    public static class AbstractDomainsHelper
    {
        [Pure]
        public static bool TryTrivialLessEqual<This>(This left, This right, out bool result)
          where This : IAbstractDomain
        {
            Contract.Ensures(Contract.Result<bool>() || !left.IsTop);
            Contract.Ensures(Contract.Result<bool>() || !right.IsTop);

            if (object.ReferenceEquals(left, right))
            {
                result = true;
                return true;
            }

            if (left.IsBottom)
            {
                result = true;
                return true;
            }
            else if (left.IsTop)
            {
                result = right.IsTop;
                return true;
            }
            else if (right.IsBottom)
            {
                result = false;                   // At this point we already know that it is false
                return true;
            }
            else if (right.IsTop)
            {
                result = true;
                return true;
            }
            else
            {
                result = false;   // no matter what
                return false;
            }
        }

        [Pure]
        public static bool TryTrivialJoin<This>(This left, This right, out This result)
          where This : class, IAbstractDomain
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out result) != null));

            if (object.ReferenceEquals(left, right))
            {
                result = left;
                return true;
            }

            if (left.IsBottom)
            {
                result = right;
                return true;
            }
            else if (left.IsTop)
            {
                result = left;
                return true;
            }
            else if (right.IsBottom)
            {
                result = left;
                return true;
            }
            else if (right.IsTop)
            {
                result = right;
                return true;
            }
            else
            {
                result = default(This);
                return false;
            }
        }

        [Pure]
        public static bool TryTrivialJoinRefinedWithEmptyArrays<AD, Variable, Expression>(
          ArraySegmentation<AD, Variable, Expression> left,
          ArraySegmentation<AD, Variable, Expression> right,
          out ArraySegmentation<AD, Variable, Expression> result)
          where AD : class, IAbstractDomainForArraySegmentationAbstraction<AD, Variable>
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out result) != null));

            // IsEmptyArray may determine that a segmentation is bottom, so it is more precise if we perform this check before
            if (left.IsEmptyArray)
            {
                result = right;
                return true;
            }

            if (right.IsEmptyArray)
            {
                result = left;
                return true;
            }

            if (TryTrivialJoin(left, right, out result))
            {
                return true;
            }

            result = default(ArraySegmentation<AD, Variable, Expression>);
            return false;
        }



        public static bool TryTrivialMeet<This>(This left, This right, out This result)
          where This : class, IAbstractDomain
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out result) != null));

            if (object.ReferenceEquals(left, right))
            {
                result = left;
                return true;
            }

            if (left.IsBottom)
            {
                result = left;
                return true;
            }
            else if (left.IsTop)
            {
                result = right;
                return true;
            }
            else if (right.IsBottom)
            {
                result = right;
                return true;
            }
            else if (right.IsTop)
            {
                result = left;
                return true;
            }
            else
            {
                result = default(This);
                return false;
            }
        }

        /// <summary>
        /// Tries to get a threshod from the booleand guard <code>guard</code>
        /// </summary>
        public static bool TryToGetAThreshold<Variable, Expression>(Expression exp, out List<int> thresholds, IExpressionDecoder<Variable, Expression> decoder)
        {
            var searchForAThreshold = new GetAThresholdVisitor<Variable, Expression>(decoder);
            if (searchForAThreshold.Visit(exp, Void.Value) && searchForAThreshold.Thresholds.Count == 1)
            { // We do not want the thresholds to be too much, as it may slow down the analysis, 
              // so we consider them relevant when they are only one
                var t = searchForAThreshold.Thresholds[0];
                thresholds = new List<int>() { t, t + 1, t - 1 };
                return t != 0;
            }
            else
            {
                thresholds = null;
                return false;
            }
        }
    }

    public static class CommonChecks
    {
        /// <summary>
        /// Checks if <code>e1 \leq e2</code> is always true.
        /// It can use some bound information from the oracledomain
        /// </summary>
        public static FlatAbstractDomain<bool> CheckLessEqualThan<Variable, Expression>(
          Expression e1, Expression e2, INumericalAbstractDomainQuery<Variable, Expression> oracleDomain, IExpressionDecoder<Variable, Expression> decoder)
        {
            Polynomial<Variable, Expression> leq;
            // Try to see if it holds using the oracleDomain
            if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, e1, e2, decoder, out leq))
            {
                if (leq.Degree == 0)
                { // If it is a constant
                    var leftConstant = leq.Left[0].K;
                    var rightConstant = leq.Right[0].K;

                    if (leftConstant <= rightConstant)
                    {
                        return CheckOutcome.True;
                    }
                    else if (leftConstant > rightConstant)
                    {
                        return CheckOutcome.False;
                    }
                    else
                    {
                        return CheckOutcome.Top;
                    }
                }
                else if (leq.Degree == 1)
                {
                    Variable x;
                    Rational k1, k2;
                    if (leq.TryMatch_k1XLessThank2(out k1, out x, out k2))
                    {
                        // Very easy for the moment
                        if (k1 == -1)
                        {  // -1 * x <= k2
                            var b = oracleDomain.BoundsFor(x);
                            if (!b.IsTop && -b.LowerBound <= k2)
                            {
                                return CheckOutcome.True;
                            }
                        }
                    }
                    /*  We do not care at the moment, it can be refined if we want */
                    /*
                      else if (PolynomialHelper.Match_k1XLessEqualThank2(leq, out k1, out x, out k2))
                      {
                        return  CheckOutcome.Top; 
                      }
                     */
                }
            }

            return CheckOutcome.Top;
        }

        /// <summary>
        /// Checks if <code>e1 \lt e2 </code> is always true.
        /// </summary>
        /// It can use some bound information from the oracledomain
#if SUBPOLY_ONLY
        internal
#else
        public
#endif
 static FlatAbstractDomain<bool> CheckLessThan<Variable, Expression>(Expression e1, Expression e2,
                INumericalAbstractDomainQuery<Variable, Expression> oracleDomain, IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            Polynomial<Variable, Expression> ltPol;

            if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessThan, e1, e2, decoder, out ltPol))
            {
                if (ltPol.Degree == 0)
                { // If it is a constant
                    var leftConstant = ltPol.Left[0].K;
                    var rightConstant = ltPol.Right[0].K;

                    if (leftConstant < rightConstant)
                    {
                        return CheckOutcome.True;
                    }
                    else if (leftConstant >= rightConstant)
                    {
                        return CheckOutcome.False;
                    }
                }
                else if (ltPol.Degree == 1)
                {
                    Variable x;
                    Rational k1, k2;

                    if (ltPol.TryMatch_k1XLessThank2(out k1, out x, out k2) && k1 == -1)
                    {
                        // -1 * x < k2
                        var b = oracleDomain.BoundsFor(x);
                        if (!b.IsTop && -b.LowerBound < k2)
                        {
                            return CheckOutcome.True;
                        }
                    }
                }
            }

            return CheckOutcome.Top;
        }

        static public FlatAbstractDomain<bool> CheckGreaterEqualThanZero<Variable, Expression>(Expression e, IExpressionDecoder<Variable, Expression> decoder)
        {
            var anIntervalDomain = new IntervalEnvironment<Variable, Expression>(decoder, VoidLogger.Log);

            return anIntervalDomain.CheckIfGreaterEqualThanZero(e);
        }

        /// <summary>
        /// If the expression is an equality or disequality, see it the two operands are the same symbolic value
        /// </summary>
        static public bool CheckTrivialEquality<Variable, Expression>(Expression exp, IExpressionDecoder<Variable, Expression> decoder)
        {
            var op = decoder.OperatorFor(exp);

            switch (op)
            {
                case ExpressionOperator.Equal:
                case ExpressionOperator.Equal_Obj:
                    {
                        // Is the case that left == right ?
                        var left = decoder.LeftExpressionFor(exp);
                        var right = decoder.RightExpressionFor(exp);

                        if (op == ExpressionOperator.Equal &&
                          (decoder.IsNaN(left) || decoder.IsNaN(right)))
                        {
                            return false;
                        }

                        if (left.Equals(right) || decoder.UnderlyingVariable(left).Equals(decoder.UnderlyingVariable(right)))
                        { // left == right
                            return true;
                        }
                        else
                        {
                            var strippedLeft = decoder.Stripped(left);
                            var strippedRight = decoder.Stripped(right);

                            return strippedLeft.Equals(strippedRight) || decoder.UnderlyingVariable(strippedLeft).Equals(decoder.UnderlyingVariable(strippedRight));
                        }
                    }

                case ExpressionOperator.NotEqual:
                    {
                        // Is the case that (x == x) != 0 ?
                        int value;
                        var left = decoder.LeftExpressionFor(exp);
                        if (decoder.OperatorFor(left) == ExpressionOperator.Equal
                          && decoder.IsConstantInt(decoder.RightExpressionFor(exp), out value)
                          && value == 0)
                        {
                            return decoder.LeftExpressionFor(left).Equals(decoder.RightExpressionFor(left));
                        }
                        return false;
                    }

                default:
                    return false;
            }
        }

        static public bool CheckTrivialEquality<Variable, Expression>(Expression e1, Expression e2,
          IExpressionDecoder<Variable, Expression> decoder, out FlatAbstractDomain<bool> outcome)
        {
            if (decoder.UnderlyingVariable(e1).Equals(decoder.UnderlyingVariable(e2)))
            {
                outcome = CheckOutcome.True;
                return true;
            }

            int v1, v2;
            if (decoder.IsConstantInt(e1, out v1) && decoder.IsConstantInt(e2, out v2))
            {
                outcome = v1 == v2 ? CheckOutcome.True : CheckOutcome.False;
                return true;
            }

            outcome = CheckOutcome.Top;
            return false;
        }
    }

    #region Visitors for the expressions

    abstract public class GenericExpressionVisitor<In, Out, Variable, Expression>
    {
        #region Object invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(decoder != null);
        }

        #endregion

        #region Private state
        private IExpressionDecoder<Variable, Expression> decoder;
        #endregion

        #region Protected state
        protected IExpressionDecoder<Variable, Expression> Decoder
        {
            get
            {
                Contract.Ensures(Contract.Result<IExpressionDecoder<Variable, Expression>>() != null);

                return decoder;
            }
        }
        #endregion

        #region Constructor
        public GenericExpressionVisitor(IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Requires(decoder != null);

            this.decoder = decoder;
        }
        #endregion

        abstract protected Out Default(In data);

        virtual public Out Visit(Expression exp, In data)
        {
            Contract.Requires(exp != null);

            Contract.Ensures(Contract.Result<Out>() != null);

            var op = decoder.OperatorFor(exp);
            switch (op)
            {
                #region All the cases
                case ExpressionOperator.Addition_Overflow:
                    return VisitAddition_Overflow(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.Addition:
                    return VisitAddition(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.And:
                    return VisitAnd(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.Constant:
                    return VisitConstant(exp, data);

                case ExpressionOperator.ConvertToInt8:
                    return VisitConvertToInt8(decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.ConvertToInt32:
                    return VisitConvertToInt32(decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.ConvertToUInt8:
                    return VisitConvertToUInt8(decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.ConvertToUInt16:
                    return VisitConvertToUInt16(decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.ConvertToFloat32:
                    return VisitConvertToFloat32(decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.ConvertToFloat64:
                    return VisitConvertToFloat64(decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.ConvertToUInt32:
                    return VisitConvertToUInt32(decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.Division:
                    return VisitDivision(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.Equal:
                case ExpressionOperator.Equal_Obj:
                    // we want to handle cases as "(a <= b) == 0" rewritten to "a > b"
                    return DispatchVisitEqual(op, decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.GreaterEqualThan:
                    return VisitGreaterEqualThan(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.GreaterEqualThan_Un:
                    return VisitGreaterEqualThan_Un(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.GreaterThan:
                    return VisitGreaterThan(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.GreaterThan_Un:
                    return VisitGreaterThan_Un(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.LessEqualThan:
                    return DispatchCompare(VisitLessEqualThan, decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.LessEqualThan_Un:
                    return DispatchCompare(VisitLessEqualThan_Un, decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.LessThan:
                    return DispatchCompare(VisitLessThan, decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.LessThan_Un:
                    return DispatchCompare(VisitLessThan_Un, decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.LogicalAnd:
                    return VisitLogicalAnd(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.LogicalNot:
                    return VisitLogicalNot(decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.LogicalOr:
                    return VisitLogicalOr(decoder.Disjunctions(exp), exp, data);

                case ExpressionOperator.Modulus:
                    return VisitModulus(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.Multiplication:
                    return VisitMultiplication(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.Multiplication_Overflow:
                    return VisitMultiplication_Overflow(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.Not:
                    return DispatchVisitNot(decoder.LeftExpressionFor(exp), data);

                case ExpressionOperator.NotEqual:
                    return VisitNotEqual(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.Or:
                    return VisitOr(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.ShiftLeft:
                    return VisitShiftLeft(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.ShiftRight:
                    return VisitShiftRight(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.SizeOf:
                    return VisitSizeOf(exp, data);

                case ExpressionOperator.Subtraction:
                    return VisitSubtraction(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.Subtraction_Overflow:
                    return VisitSubtraction_Overflow(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.UnaryMinus:
                    return VisitUnaryMinus(decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.Unknown:
                    return VisitUnknown(exp, data);

                case ExpressionOperator.WritableBytes:
                    return VisitWritableBytes(decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.Variable:
                    return VisitVariable(decoder.UnderlyingVariable(exp), exp, data);

                case ExpressionOperator.Xor:
                    return VisitXor(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                default:
                    Contract.Assert(false);
                    throw new AbstractInterpretationException("I do not know this expression symbol " + decoder.OperatorFor(exp));
                    #endregion
            }
        }

        private Out DispatchVisitEqual(ExpressionOperator eqKind, Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(eqKind == ExpressionOperator.Equal || eqKind == ExpressionOperator.Equal_Obj);

            Contract.Requires(left != null);
            Contract.Requires(right != null);

            // Try matching ==(RelOp(l1, l2), right), RelOp is a relation operator and right is a constant

            var op = decoder.OperatorFor(left);
            if (op == ExpressionOperator.Equal || op == ExpressionOperator.Equal_Obj
                      || op.IsGreaterEqualThan() || op.IsGreaterThan()
                      || op.IsLessEqualThan() || op.IsLessThan())
            {
                bool shouldnegate;
                if (this.TryPolarity(right, data, out shouldnegate))
                {
                    if (shouldnegate)
                    {
                        return DispatchVisitNot(left, data);
                    }
                    else
                    {
                        return Visit(left, data);
                    }
                }
                if (this.TryPolarity(left, data, out shouldnegate))
                {
                    if (shouldnegate)
                    {
                        return DispatchVisitNot(right, data);
                    }
                    else
                    {
                        return Visit(right, data);
                    }
                }
            }

            // visit "left == right"
            if (eqKind == ExpressionOperator.Equal)
            {
                return VisitEqual(left, right, original, data);
            }
            else
            {
                Contract.Assert(eqKind == ExpressionOperator.Equal_Obj);

                return VisitEqual_Obj(left, right, original, data);
            }
        }

        virtual protected bool TryPolarity(Expression exp, In data, out bool shouldNegate)
        {
            Contract.Requires(exp != null);

            if (decoder.IsConstant(exp))
            {
                int intValue;
                if (decoder.TryValueOf<Int32>(exp, ExpressionType.Int32, out intValue))
                {
                    shouldNegate = intValue == 0;
                    return true;
                }

                bool boolValue;
                if (decoder.TryValueOf<bool>(exp, ExpressionType.Bool, out boolValue))
                {
                    shouldNegate = boolValue;
                    return true;
                }
            }

            shouldNegate = default(bool);
            return false;
        }


        private Out DispatchVisitNot(Expression exp, In data)
        {
            Contract.Requires(exp != null);

            switch (decoder.OperatorFor(exp))
            {
                case ExpressionOperator.GreaterEqualThan:
                case ExpressionOperator.GreaterEqualThan_Un:
                    // !(a >= b) = a < b
                    return DispatchCompare(VisitLessThan, decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.GreaterThan:
                    // !(a > b) = a <= b
                    return DispatchCompare(VisitLessEqualThan, decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.GreaterThan_Un:
                    // !(a > b) = a <= b
                    return DispatchCompare(VisitLessEqualThan_Un, decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                case ExpressionOperator.LessEqualThan:
                    // !(a <= b) = b < a
                    return DispatchCompare(VisitLessThan, decoder.RightExpressionFor(exp), decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.LessEqualThan_Un:
                    // !(a <= b) = b < a
                    return DispatchCompare(VisitLessThan_Un, decoder.RightExpressionFor(exp), decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.LessThan:
                    // !(a < b) = b <= a
                    return DispatchCompare(VisitLessEqualThan, decoder.RightExpressionFor(exp), decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.LessThan_Un:
                    // !(a < b) = b <= a
                    return DispatchCompare(VisitLessEqualThan_Un, decoder.RightExpressionFor(exp), decoder.LeftExpressionFor(exp), exp, data);

                case ExpressionOperator.Equal:
                case ExpressionOperator.Equal_Obj:
                    // !(a == b) = a != b
                    return VisitNotEqual(decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), exp, data);

                default:
                    return VisitNot(exp, data);
            }
        }

        protected delegate Out CompareVisitor(Expression left, Expression right, Expression original, In data);

        /// <summary>
        /// Factors out some code so that for instance a - b \leq 0, becomes a \leq b
        /// </summary>
        virtual protected Out DispatchCompare(CompareVisitor cmp, Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(cmp != null);

            if (decoder.IsConstant(left) && decoder.OperatorFor(right) == ExpressionOperator.Subtraction)
            {
                Int32 value;
                if (decoder.TryValueOf<Int32>(left, ExpressionType.Int32, out value) && value == 0)
                {
                    return cmp(decoder.RightExpressionFor(right), decoder.LeftExpressionFor(right), right, data);
                }
                else
                {
                    return cmp(left, right, original, data);
                }
            }

            if (decoder.IsConstant(right) && decoder.OperatorFor(left) == ExpressionOperator.Subtraction)
            {
                Int32 value;
                if (decoder.TryValueOf<Int32>(right, ExpressionType.Int32, out value) && value == 0)
                {
                    return cmp(decoder.LeftExpressionFor(left), decoder.RightExpressionFor(left), left, data);
                }
                else
                {
                    return cmp(left, right, original, data);
                }
            }

            return cmp(left, right, original, data);
        }

        #region The default Visitors
        virtual public Out VisitAddition(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitAddition_Overflow(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return VisitAddition(left, right, original, data);
        }

        virtual public Out VisitAnd(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitConstant(Expression left, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitConvertToInt8(Expression left, Expression original, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitConvertToInt32(Expression left, Expression original, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitConvertToUInt8(Expression left, Expression original, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitConvertToUInt16(Expression left, Expression original, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitConvertToUInt32(Expression left, Expression original, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitConvertToFloat32(Expression left, Expression original, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitConvertToFloat64(Expression left, Expression original, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitDivision(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitEqual(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitEqual_Obj(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitGreaterEqualThan(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return DispatchCompare(VisitLessEqualThan, right, left, original, data);
        }

        virtual public Out VisitGreaterEqualThan_Un(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return DispatchCompare(VisitLessEqualThan_Un, right, left, original, data);
        }

        virtual public Out VisitGreaterThan(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return DispatchCompare(VisitLessThan, right, left, original, data);
        }

        virtual public Out VisitGreaterThan_Un(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            // the Roslyn compiler uses cgt.un for comparing references to null; while producing correct
            // results at runtime, it doesn't match implicit non-null assumptions made by the static checker,
            // so we detect it here and convert it to an old-style equality comparison
            if (decoder.IsNull(right))
                return VisitNotEqual(left, right, original, data);

            return DispatchCompare(VisitLessThan_Un, right, left, original, data);
        }

        virtual public Out VisitLessEqualThan(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        /// <summary>
        /// Default: dispatch to VisitLessEqualThan
        /// </summary>
        virtual public Out VisitLessEqualThan_Un(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return VisitLessEqualThan(left, right, original, data);
        }

        virtual public Out VisitLessThan(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        /// <summary>
        /// Default: dispatch to VisistLessThan
        /// </summary>
        virtual public Out VisitLessThan_Un(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return VisitLessThan(left, right, original, data);
        }

        virtual public Out VisitLogicalAnd(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitLogicalNot(Expression left, Expression original, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitLogicalOr(IEnumerable<Expression> disjunctions, Expression original, In data)
        {
            Contract.Requires(disjunctions != null);

            return Default(data);
        }

        virtual public Out VisitModulus(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitMultiplication(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitMultiplication_Overflow(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return VisitMultiplication(left, right, original, data);
        }

        virtual public Out VisitNot(Expression left, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitNotEqual(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitOr(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitShiftLeft(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitShiftRight(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitSizeOf(Expression left, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitSubtraction(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }

        virtual public Out VisitSubtraction_Overflow(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return VisitSubtraction(left, right, original, data);
        }

        virtual public Out VisitUnaryMinus(Expression left, Expression original, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitUnknown(Expression left, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitVariable(Variable variable, Expression original, In data)
        {
            return Default(data);
        }

        virtual public Out VisitWritableBytes(Expression left, Expression wholeExpression, In data)
        {
            Contract.Requires(left != null);

            return Default(data);
        }

        virtual public Out VisitXor(Expression left, Expression right, Expression original, In data)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            return Default(data);
        }
        #endregion
    }

    abstract public class GenericNormalizingExpressionVisitor<Data, Variable, Expression>
      : GenericExpressionVisitor<Data, Data, Variable, Expression>
    {
        public GenericNormalizingExpressionVisitor(IExpressionDecoder<Variable, Expression> decoder)
          : base(decoder)
        {
        }

        /// <summary>
        /// a \geq b ==> b \leq a
        /// </summary>
        sealed public override Data VisitGreaterEqualThan(Expression left, Expression right, Expression original, Data data)
        {
            return VisitLessEqualThan(right, left, original, data);
        }

        /// <summary>
        /// a \gt b ==> b \lt a
        /// </summary>
        sealed public override Data VisitGreaterThan(Expression left, Expression right, Expression original, Data data)
        {
            return VisitLessThan(right, left, original, data);
        }
    }

    abstract public class GenericTypeExpressionVisitor<Variable, Expression, In, Out>
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(decoder != null);
        }


        private IExpressionDecoder<Variable, Expression> decoder;

        protected IExpressionDecoder<Variable, Expression> Decoder
        {
            get
            {
                Contract.Ensures(Contract.Result<IExpressionDecoder<Variable, Expression>>() != null);

                return decoder;
            }
        }

        public GenericTypeExpressionVisitor(IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Requires(decoder != null);

            this.decoder = decoder;
        }

        virtual public Out Visit(Expression exp, In input)
        {
            Contract.Requires(exp != null);

            switch (decoder.TypeOf(exp))
            {
                case ExpressionType.Bool:
                    return VisitBool(exp, input);

                case ExpressionType.Float32:
                    return VisitFloat32(exp, input);

                case ExpressionType.Float64:
                    return VisitFloat64(exp, input);

                case ExpressionType.Int8:
                    return VisitInt8(exp, input);

                case ExpressionType.Int16:
                    return VisitInt16(exp, input);

                case ExpressionType.Int32:
                    return VisitInt32(exp, input);

                case ExpressionType.Int64:
                    return VisitInt64(exp, input);

                case ExpressionType.String:
                    return VisitString(exp, input);

                case ExpressionType.UInt8:
                    return VisitUInt8(exp, input);

                case ExpressionType.UInt16:
                    return VisitUInt16(exp, input);

                case ExpressionType.UInt32:
                    return VisitUInt32(exp, input);

                case ExpressionType.Unknown:
                    return Default(exp);

                default:
                    throw new AbstractInterpretationException("Unknown type for expressions " + decoder.TypeOf(exp));
            }
        }

        virtual public Out VisitFloat64(Expression exp, In input)
        {
            return Default(exp);
        }

        virtual public Out VisitFloat32(Expression exp, In input)
        {
            return Default(exp);
        }

        virtual public Out VisitBool(Expression/*!*/ exp, In/*!*/ input)
        {
            return Default(exp);
        }

        virtual public Out VisitInt8(Expression/*!*/ exp, In/*!*/ input)
        {
            return Default(exp);
        }

        virtual public Out VisitInt16(Expression/*!*/ exp, In/*!*/ input)
        {
            return Default(exp);
        }

        virtual public Out VisitInt32(Expression/*!*/ exp, In/*!*/ input)
        {
            return Default(exp);
        }

        virtual public Out VisitInt64(Expression/*!*/ exp, In/*!*/ input)
        {
            return Default(exp);
        }

        virtual public Out VisitString(Expression/*!*/ exp, In/*!*/ input)
        {
            return Default(exp);
        }

        virtual public Out VisitUInt8(Expression/*!*/ exp, In/*!*/ input)
        {
            return Default(exp);
        }

        virtual public Out VisitUInt16(Expression/*!*/ exp, In/*!*/ input)
        {
            return Default(exp);
        }

        virtual public Out VisitUInt32(Expression/*!*/ exp, In/*!*/ input)
        {
            return Default(exp);
        }

        public abstract Out Default(Expression exp);
    }

    abstract public class TestTrueVisitor<AbstractDomain, Variable, Expression> :
      GenericNormalizingExpressionVisitor<AbstractDomain, Variable, Expression>
      where AbstractDomain : IAbstractDomainForEnvironments<Variable, Expression>
    {
        #region Constants

        private const int MAXDISJUNCTS = 4;

        #endregion

        #region Private state

        private TestFalseVisitor<AbstractDomain, Variable, Expression> falseVisitor;
        private AbstractDomain result;

        #endregion

        protected TestTrueVisitor(IExpressionDecoder<Variable, Expression> decoder)
          : base(decoder)
        { }

        #region Protected
        internal TestFalseVisitor<AbstractDomain, Variable, Expression> FalseVisitor
        {
            get
            {
                return falseVisitor;
            }
            set
            { // this.trueVisitor == null
                Contract.Requires(value != null);

                Contract.Assume(falseVisitor == null, "Why are you setting twice the false visitor?"); // let's leave it as a runtime check

                falseVisitor = value;
            }
        }
        #endregion


        #region TO BE OVERRIDDEN!

        abstract override public AbstractDomain VisitEqual(Expression left, Expression right, Expression original, AbstractDomain data);

        abstract public override AbstractDomain VisitLessEqualThan(Expression left, Expression right, Expression original, AbstractDomain data);

        abstract public override AbstractDomain VisitLessThan(Expression left, Expression right, Expression original, AbstractDomain data);

        abstract public override AbstractDomain VisitNotEqual(Expression left, Expression right, Expression original, AbstractDomain data);

        abstract public override AbstractDomain VisitVariable(Variable var, Expression original, AbstractDomain data);

        #endregion

        protected override AbstractDomain Default(AbstractDomain data)
        {
            return data;
        }

        public override AbstractDomain VisitLogicalAnd(Expression left, Expression right, Expression original, AbstractDomain data)
        {
            var isLeftAVar = this.Decoder.IsVariable(left);
            var isLeftAConst = this.Decoder.IsConstant(left);
            var isRightAVar = this.Decoder.IsVariable(right);
            var isRightAConst = this.Decoder.IsConstant(right);

            if ((isLeftAVar && isRightAConst) || (isLeftAConst && isRightAVar))
            {
                result = data;
            }
            else
            {
                var cloned = (AbstractDomain)data.Clone();

                cloned = (AbstractDomain)cloned.TestTrue(left);
                cloned = (AbstractDomain)cloned.TestTrue(right);

                result = cloned;
            }
            return result;
        }

        public override AbstractDomain VisitConstant(Expression left, AbstractDomain data)
        {
            bool success;
            Int32 v;

            #region [[0]] == \bot, [[1]] == state
            if (this.Decoder.TryValueOf<bool>(left, ExpressionType.Bool, out success))
            {
                if (success == false)
                {
                    result = (AbstractDomain)data.Bottom;
                }
                else
                {
                    result = data;
                }
            }
            else if (this.Decoder.TryValueOf<Int32>(left, ExpressionType.Int32, out v))
            {
                if (v != 0)
                {
                    result = data;
                }
                else
                {
                    result = (AbstractDomain)data.Bottom;
                }
            }
            else
            {
                result = data;
            }
            #endregion
            return result;
        }

        public override AbstractDomain VisitConvertToInt32(Expression left, Expression original, AbstractDomain data)
        {
            return Visit(left, data);
        }

        public override AbstractDomain VisitConvertToUInt8(Expression left, Expression original, AbstractDomain data)
        {
            return Visit(left, data);
        }

        public override AbstractDomain VisitConvertToUInt16(Expression left, Expression original, AbstractDomain data)
        {
            return Visit(left, data);
        }

        public override AbstractDomain VisitConvertToUInt32(Expression left, Expression original, AbstractDomain data)
        {
            return Visit(left, data);
        }

        public override AbstractDomain VisitNot(Expression left, AbstractDomain data)
        {
            return this.FalseVisitor.Visit(left, data);
        }

        public override AbstractDomain VisitLogicalOr(IEnumerable<Expression> disjuncts, Expression original, AbstractDomain data)
        {
            if (disjuncts.Count() > MAXDISJUNCTS)
            {
                return data;
            }

            var immutable = (AbstractDomain)data.Clone();
            var r = data;
            var first = true;
            foreach (var dis in disjuncts)
            {
                if (first)
                {
                    r = (AbstractDomain)data.TestTrue(dis);
                    first = false;
                }
                else
                {
                    var tmp = ((AbstractDomain)immutable.Clone()).TestTrue(dis);
                    r = (AbstractDomain)r.Join(tmp);
                }

                if (r.IsTop)
                {
                    break;
                }
            }
            return (result = r);
        }

        protected override bool TryPolarity(Expression exp, AbstractDomain data, out bool shouldNegate)
        {
            if (base.TryPolarity(exp, data, out shouldNegate))
            {
                return true;
            }

            var tryTautology = data.CheckIfHolds(exp);

            if (tryTautology.IsNormal())
            {
                shouldNegate = tryTautology.IsFalse();

                return true;
            }

            // shouldNegate already set by base.TryPolarity
            return false;
        }
    }

    abstract internal class TestFalseVisitor<AbstractDomain, Variable, Expression> :
      GenericNormalizingExpressionVisitor<AbstractDomain, Variable, Expression>
      where AbstractDomain : IAbstractDomainForEnvironments<Variable, Expression>
    {
        #region protected state
        private TestTrueVisitor<AbstractDomain, Variable, Expression> trueVisitor;
        #endregion

        #region Constructor
        protected TestFalseVisitor(IExpressionDecoder<Variable, Expression> decoder)
          : base(decoder)
        { }
        #endregion

        /// <summary>
        /// The visitor for the positive case 
        /// </summary>
        internal TestTrueVisitor<AbstractDomain, Variable, Expression> TrueVisitor
        {
            get
            {
                Contract.Ensures(Contract.Result<TestTrueVisitor<AbstractDomain, Variable, Expression>>() != null);

                Contract.Assume(trueVisitor != null);  // F: When calling this method, the visitor should have already been set
                return trueVisitor;
            }
            set
            {
                Contract.Assume(trueVisitor == null, "Why are you setting twice the true visitor?");

                trueVisitor = value;
            }
        }

        #region TO BE OVERRIDDEN!

        abstract public override AbstractDomain VisitVariable(Variable variable, Expression original, AbstractDomain data);

        #endregion

        protected override AbstractDomain Default(AbstractDomain data)
        {
            return data;
        }

        public override AbstractDomain VisitEqual(Expression left, Expression right, Expression original, AbstractDomain data)
        {
            Int32 value;
            // !((a relop b) == 0) => a reolpb
            if (this.Decoder.TryValueOf<Int32>(right, ExpressionType.Int32, out value) && value == 0)
                if (!this.Decoder.IsConstant(left) && !this.Decoder.IsVariable(left) && !this.Decoder.IsUnaryExpression(left))
                {
                    return this.TrueVisitor.Visit(left, data);
                }

            // !(a == b) -> a != b
            return this.TrueVisitor.VisitNotEqual(left, right, original, data);
        }

        public override AbstractDomain VisitLessEqualThan(Expression left, Expression right, Expression original, AbstractDomain data)
        { // !(a <= b) -> a > b -> b < a
            return this.TrueVisitor.VisitLessThan(right, left, original, data);
        }

        public override AbstractDomain VisitLessEqualThan_Un(Expression left, Expression right, Expression original, AbstractDomain data)
        {
            return this.TrueVisitor.VisitLessThan_Un(right, left, original, data);
        }

        public override AbstractDomain VisitLessThan(Expression left, Expression right, Expression original, AbstractDomain data)
        { // !(a < b) -> a >= b -> b <= a
            return this.TrueVisitor.VisitLessEqualThan(right, left, original, data);
        }

        public override AbstractDomain VisitLessThan_Un(Expression left, Expression right, Expression original, AbstractDomain data)
        {
            return this.TrueVisitor.VisitLessEqualThan_Un(right, left, original, data);
        }

        public override AbstractDomain VisitNot(Expression left, AbstractDomain data)
        { // !(!a) -> a
            return this.TrueVisitor.Visit(left, data);
        }

        public override AbstractDomain VisitNotEqual(Expression left, Expression right, Expression original, AbstractDomain data)
        { // ! (a != b) -> a == b
            return this.TrueVisitor.VisitEqual(left, right, original, data);
        }

        public override AbstractDomain VisitLogicalAnd(Expression leftGuard, Expression rightGuard, Expression original, AbstractDomain data)
        { // ! (a && b) -> !a || !b
            IAbstractDomainForEnvironments<Variable, Expression>/*!*/ leftDomain, rightDomain;
            AbstractDomain/*!*/ result;

            var isLeftAVar = this.Decoder.IsVariable(leftGuard);
            var isLeftAConst = this.Decoder.IsConstant(leftGuard);
            var isRightAVar = this.Decoder.IsVariable(rightGuard);
            var isRightAConst = this.Decoder.IsConstant(rightGuard);

            if ((isLeftAVar && isRightAConst) || (isLeftAConst && isRightAVar))
            {
                result = data;
            }
            else if (this.Decoder.TypeOf(leftGuard) == ExpressionType.Bool && this.Decoder.TypeOf(rightGuard) == ExpressionType.Bool)
            {
                // Use de morgan laws
                leftDomain = data.TestFalse(leftGuard);
                rightDomain = data.TestFalse(rightGuard);

                result = (AbstractDomain)leftDomain.Join(rightDomain);
            }
            else
            {
                result = data;
            }

            return result;
        }

        public override AbstractDomain VisitConstant(Expression guard, AbstractDomain data)
        {
            AbstractDomain result;
            bool b; Int32 v;

            if (this.Decoder.TryValueOf<bool>(guard, ExpressionType.Bool, out b))
            {
                if (!b)
                {
                    result = data;
                }
                else
                {
                    result = (AbstractDomain)data.Bottom;
                }
            }
            else if (this.Decoder.TryValueOf<Int32>(guard, ExpressionType.Int32, out v))
            {
                if (v == 0)
                {
                    result = data;
                }
                else
                {
                    result = (AbstractDomain)data.Bottom;
                }
            }
            else
            {
                result = data;
            }

            return result;
        }

        public override AbstractDomain VisitConvertToInt32(Expression left, Expression original, AbstractDomain data)
        {
            return this.Visit(left, data);
        }

        public override AbstractDomain VisitConvertToUInt8(Expression left, Expression original, AbstractDomain data)
        {
            return this.Visit(left, data);
        }

        public override AbstractDomain VisitConvertToUInt16(Expression left, Expression original, AbstractDomain data)
        {
            return this.Visit(left, data);
        }

        public override AbstractDomain VisitConvertToUInt32(Expression left, Expression original, AbstractDomain data)
        {
            return this.Visit(left, data);
        }

        public override AbstractDomain VisitLogicalOr(IEnumerable<Expression> disjuncts, Expression original, AbstractDomain data)
        {
            // ! (a || b) -> !a && !b
            var result = (AbstractDomain)data.Clone();

            foreach (var dis in disjuncts)
            {
                result = (AbstractDomain)result.TestFalse(dis);
            }

            return result;
        }
    }

    /// <summary>
    /// The base class to be used as starting point in the implementation of CheckIfHolds in the abstract domains
    /// </summary>
    abstract public class CheckIfHoldsVisitor<AbstractDomain, Variable, Expression> :
      GenericNormalizingExpressionVisitor<FlatAbstractDomain<bool>, Variable, Expression>
      where AbstractDomain : INumericalAbstractDomain<Variable, Expression>
    {
        private AbstractDomain domain;

        protected AbstractDomain Domain
        {
            get
            {
                Contract.Ensures(Contract.Result<AbstractDomain>() != null);

                return domain;
            }
        }

        public CheckIfHoldsVisitor(IExpressionDecoder<Variable, Expression> decoder)
          : base(decoder)
        {
            Contract.Requires(decoder != null);
        }

        /// <summary>
        /// The entry point for checking if the expression <code>exp</code> holds in the state <code>domain</code>
        /// </summary>
        public FlatAbstractDomain<bool> Visit(Expression exp, AbstractDomain domain)
        {
            Contract.Requires(exp != null);
            Contract.Requires(domain != null);

            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            this.domain = domain;

            var result = base.Visit(exp, CheckOutcome.Top);

            return result;
        }

        #region To be implemented by the client
        public override FlatAbstractDomain<bool> VisitEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
        {
            var leftType = this.Decoder.TypeOf(left);
            var rightType = this.Decoder.TypeOf(right);

            // if they are not floats, and Var(left).Equals(Var(right)) 
            if (!leftType.IsFloatingPointType() && !rightType.IsFloatingPointType())
            {
                if (left.Equals(right)      // We check for expression equality as the WP may have generated the same expression
                ||
                  this.Decoder.UnderlyingVariable(left).Equals(this.Decoder.UnderlyingVariable(right)))
                {
                    return CheckOutcome.True;
                }
            }

            return CheckOutcome.Top;
        }

        public abstract override FlatAbstractDomain<bool> VisitEqual_Obj(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data);

        public abstract override FlatAbstractDomain<bool> VisitLessEqualThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data);

        public abstract override FlatAbstractDomain<bool> VisitLessThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data);
        #endregion

        #region Some standard implementations

        public override FlatAbstractDomain<bool> VisitAddition(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
        {
            return this.Domain.CheckIfNonZero(original);
        }

        public override FlatAbstractDomain<bool> VisitLogicalAnd(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
        {
            var resultLeft = Visit(left, data);
            // false && X == false
            if (resultLeft.IsFalse())
            {
                return resultLeft;
            }

            var resultRight = Visit(right, data);
            // top && false == resultRight == true && resultRight
            if (resultRight.IsFalse() || (resultLeft.IsTrue() && resultRight.IsNormal()))
            {
                return resultRight;
            }

            return CheckOutcome.Top;
        }

        public override FlatAbstractDomain<bool> VisitLogicalOr(IEnumerable<Expression> disjuncts, Expression original, FlatAbstractDomain<bool> data)
        {
            var allBottoms = true;
            foreach (var dis in disjuncts)
            {
                var outcome = Visit(dis, data);

                if (outcome.IsTrue())
                {
                    return outcome;
                }
                if (outcome.IsFalse() || outcome.IsTop)
                {
                    allBottoms = false;
                    continue;
                }
            }

            return allBottoms ? CheckOutcome.Bottom : CheckOutcome.Top;

            /*
            var resultLeft = Visit(left, data);

            if (resultLeft.IsTrue())
            {
              return resultLeft;
            }

            var resultRight = Visit(right, data);

            if (resultLeft.IsFalse())
            {
              return resultRight;
            }

            if (resultLeft.IsBottom)
            {
              return resultRight;
            }
            if (resultRight.IsBottom)
            {
              return resultLeft;
            }
            if (resultLeft.IsTop)
            {
              return resultRight;
            }
            if (resultRight.IsTop)
            {
              return resultLeft;
            }

            return new FlatAbstractDomain<bool>(resultLeft.BoxedElement || resultRight.BoxedElement);
             */
        }

        public override FlatAbstractDomain<bool> VisitConstant(Expression left, FlatAbstractDomain<bool> data)
        {
            var value = new IntervalEnvironment<Variable, Expression>.EvalConstantVisitor(this.Decoder).Visit(left, new IntervalEnvironment<Variable, Expression>(this.Decoder, VoidLogger.Log));

            Contract.Assert(!value.IsBottom);

            if (value.IsTop)
            {
                return CheckOutcome.Top;
            }

            Rational v;
            if (value.TryGetSingletonValue(out v))
            {
                if (v.IsNotZero)
                {
                    return CheckOutcome.True;
                }
                else
                {
                    return CheckOutcome.False;
                }
            }

            // We can return Top if for instance left is -oo or +oo (doubles)
            return CheckOutcome.Top;
        }

        public override FlatAbstractDomain<bool> VisitDivision(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
        {
            return this.Visit(left, data);
        }

        public override FlatAbstractDomain<bool> VisitModulus(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
        {
            return this.Domain.CheckIfNonZero(original);
        }

        public override FlatAbstractDomain<bool> VisitMultiplication(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
        {
            return this.Domain.CheckIfNonZero(original);
        }

        public override FlatAbstractDomain<bool> VisitSubtraction(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
        {
            return this.Domain.CheckIfNonZero(original);
        }

        public override FlatAbstractDomain<bool> VisitNot(Expression left, FlatAbstractDomain<bool> data)
        {
            var leftHolds = this.Visit(left, data);

            if (leftHolds.IsNormal())
            {
                return new FlatAbstractDomain<bool>(!leftHolds.BoxedElement);
            }
            else
            {
                return leftHolds;
            }
        }

        public override FlatAbstractDomain<bool> VisitUnaryMinus(Expression left, Expression original, FlatAbstractDomain<bool> data)
        {
            return this.Domain.CheckIfNonZero(left);
        }

        public override FlatAbstractDomain<bool> VisitXor(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
        {
            return this.Domain.CheckIfNonZero(original);
        }

        protected override FlatAbstractDomain<bool> Default(FlatAbstractDomain<bool> data)
        {
            return CheckOutcome.Top;
        }
        #endregion
    }

    internal class Void
    {
        static public Void Value { get { return new Void(); } }
    }

    internal class GetAThresholdVisitor<Variable, Expression>
      : GenericExpressionVisitor<Void, bool, Variable, Expression>
    {
        private List<int> thresholds;

        public List<int> Thresholds
        {
            get
            {
                return thresholds;
            }
        }

        public GetAThresholdVisitor(IExpressionDecoder<Variable, Expression> decoder)
          : base(decoder)
        {
            thresholds = new List<int>();
        }

        public override bool VisitConstant(Expression left, Void data)
        {
            int v;

            if (this.Decoder.IsConstantInt(left, out v))
            {
                thresholds.Add(v);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool VisitConvertToInt32(Expression left, Expression original, Void data)
        {
            return this.Visit(left, data);
        }

        public override bool VisitConvertToUInt16(Expression left, Expression original, Void data)
        {
            return this.Visit(left, data);
        }

        public override bool VisitConvertToUInt32(Expression left, Expression original, Void data)
        {
            return this.Visit(left, data);
        }

        public override bool VisitConvertToUInt8(Expression left, Expression original, Void data)
        {
            return this.Visit(left, data);
        }

        public override bool VisitEqual(Expression left, Expression right, Expression original, Void data)
        {
            return this.VisitBinary(left, right, data);
        }

        public override bool VisitNotEqual(Expression left, Expression right, Expression original, Void data)
        {
            return this.VisitBinary(left, right, data);
        }

        public override bool VisitGreaterEqualThan(Expression left, Expression right, Expression original, Void data)
        {
            return this.VisitBinary(left, right, data);
        }

        public override bool VisitGreaterThan(Expression left, Expression right, Expression original, Void data)
        {
            return this.VisitBinary(left, right, data);
        }

        public override bool VisitLessEqualThan(Expression left, Expression right, Expression original, Void data)
        {
            return this.VisitBinary(left, right, data);
        }

        public override bool VisitLessThan(Expression left, Expression right, Expression original, Void data)
        {
            return this.VisitBinary(left, right, data);
        }

        protected override bool Default(Void data)
        {
            return false;
        }

        private bool VisitBinary(Expression left, Expression right, Void data)
        {
            bool b1 = this.Visit(left, data);
            bool b2 = this.Visit(right, data);

            return b1 || b2;
        }
    }

    #endregion
}
