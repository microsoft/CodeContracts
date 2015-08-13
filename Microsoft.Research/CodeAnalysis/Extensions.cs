// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.CodeAnalysis
{
    public static class BoxedExpressionExtensions
    {
        [Pure]
        static public bool IsConstantFalse(this BoxedExpression be)
        {
            Contract.Requires(be != null);

            if (!be.IsConstant)
            {
                return false;
            }

            int k;
            if (be.IsConstantInt(out k) && k == 0)
            {
                return true;
            }

            if (be.Constant is bool)
            {
                return ((bool)be.Constant) == false;
            }

            return false;
        }

        [Pure]
        public static bool IsConstantInt(this BoxedExpression b, out int value)
        {
            Contract.Requires(b != null);

            if (b.IsConstant)
            {
                var k = b.Constant;

                if (k is Int32)
                {
                    value = (Int32)k;
                    return true;
                }
                else if (k is System.Double || k is System.Single)
                {
                    double kAsDouble = k is System.Single ? (Single)k : (Double)k;

                    if (!Double.IsInfinity(kAsDouble) && !Double.IsNaN(kAsDouble))
                    {
                        var floor = Math.Floor(kAsDouble);
                        var ceiling = Math.Ceiling(kAsDouble);
                        if (floor == ceiling) // here it is ok to use == on doubles
                        {
                            var kAsInt64 = (Int64)Math.Floor(kAsDouble);

                            if (Int32.MinValue <= kAsInt64 && kAsInt64 <= Int32.MaxValue)
                            {
                                value = (Int32)kAsInt64;
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    try
                    {
                        var ic = k as IConvertible;
                        if (ic == null || k is string)
                        {
                            value = 0;
                            return false;
                        }

                        value = ic.ToInt32(null);
                        return true;
                    }
                    catch (Exception)
                    {
                        // to nothing
                    }
                }
            }

            value = 0;
            return false;
        }

        [Pure]
        public static bool IsConstantInt64(this BoxedExpression b, out long value)
        {
            Contract.Requires(b != null);

            if (b.IsConstant)
            {
                var k = b.Constant;

                if (k is Int64)
                {
                    value = (Int64)k;
                    return true;
                }
                else
                {
                    try
                    {
                        var ic = k as IConvertible;
                        if (ic == null)
                        {
                            value = 0;
                            return false;
                        }
                        value = ic.ToInt64(null);
                        return true;
                    }
                    catch (Exception)
                    {
                        // to nothing
                    }
                }
            }

            value = 0;
            return false;
        }

        /// <summary>
        /// Matches the boxed expression *exactly* with a Float64
        /// </summary>
        [Pure]
        public static bool IsConstantFloat64(this BoxedExpression b, out double value)
        {
            Contract.Requires(b != null);

            if (b.IsConstant)
            {
                var k = b.Constant;

                if (k is Double)
                {
                    value = (Double)k;
                    return true;
                }
            }

            value = 0.0d;
            return false;
        }

        [Pure]
        public static bool IsConstantFloat32(this BoxedExpression b, out float value)
        {
            Contract.Requires(b != null);

            if (b.IsConstant)
            {
                var k = b.Constant;

                if (k is float)
                {
                    value = (float)k;
                    return true;
                }
            }

            value = 0.0f;
            return false;
        }

        [Pure]
        public static bool IsConstantIntOrNull(this BoxedExpression b, out int value)
        {
            Contract.Requires(b != null);

            if (b.IsConstant)
            {
                var k = b.Constant;

                if (k is Int32)
                {
                    value = (Int32)k;
                    return true;
                }
                if (k == null)
                {
                    value = 0; // treat null as 0
                    return true;
                }
            }
            value = 0;
            return false;
        }

        [Pure]
        public static bool IsConstantBool(this BoxedExpression b, out bool value)
        {
            Contract.Requires(b != null);

            if (b.IsConstant)
            {
                if (b.Constant is Boolean)
                {
                    value = (Boolean)b.Constant;

                    return true;
                }
                if (b.Constant is Int32)
                {
                    value = ((Int32)b.Constant) != 0;

                    return true;
                }
            }
            value = false;
            return false;
        }

        [Pure]
        public static bool IsNaN(this BoxedExpression b)
        {
            Contract.Requires(b != null);

            if (b.IsConstant)
            {
                var k = b.Constant;

                if (k is Single)
                {
                    return Single.IsNaN((Single)k);
                }

                if (k is Double)
                {
                    return Double.IsNaN((Double)k);
                }
            }

            return false;
        }

        [Pure]
        public static bool IsConstantString(this BoxedExpression b, out string value)
        {
            Contract.Requires(b != null);

            if (b.IsConstant && b.Constant is string)
            {
                value = b.Constant as string; // value can be null, as null is an admissible string constant
                return true;
            }

            // HACKHACKHACK for string.Empty
            if (b.AccessPath != null && b.AccessPath.Length == 2 && b.AccessPath[0].ToString() == "string.Empty")
            {
                value = "";
                return true;
            }

            value = null;
            return false;
        }

        [Pure]
        static public bool IsAssumeIsAType<Variable, Type>(this BoxedExpression cond, out Variable var, out Type type)
        {
            Contract.Requires(cond != null);

            // We do pattern matching for the C# compiler generated condition
            BinaryOperator bop;
            BoxedExpression left, right;
            if (cond.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Cle_Un && right.IsNull)
            {
                object type_obj;
                BoxedExpression exp;
                if (left.IsIsInstExpression(out exp, out type_obj) && type_obj is Type && exp.TryGetFrameworkVariable(out var))
                {
                    type = (Type)type_obj;

                    return true;
                }
            }

            var = default(Variable);
            type = default(Type);
            return false;
        }


        public static bool IsArrayLengthOrThisDotCount<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
          this BoxedExpression exp, APC pc, IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder)
          where Type : IEquatable<Type>
        {
            Contract.Requires(exp != null);
            Contract.Requires(Context != null);

            return exp.IsArrayLength(pc, Context, MetaDataDecoder) || exp.IsThisDotCount(pc, Context, MetaDataDecoder);
        }

        /// <summary>
        /// Syntactic pattern matching to detect if exp is "this.Count".
        /// To be used only in heuristics to filter stupid preconditions
        /// </summary>
        [Pure]
        public static bool IsThisDotCount<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
          this BoxedExpression exp, APC pc, IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder)
          where Type : IEquatable<Type>
        {
            Contract.Requires(exp != null);

            Variable var;
            if (exp.IsVariable && exp.TryGetFrameworkVariable(out var))
            {
                var accessPath = Context.ValueContext.AccessPathList(pc, var, false, false);

                // simple syntactic pattern matching
                if (accessPath != null && accessPath.Length() == 2)
                {
                    if (Context.ValueContext.IsRootedInParameter(accessPath) &&
                      accessPath.Head.ToString() == "this" && accessPath.Tail.Head.IsGetter &&
                      accessPath.Tail.Head.ToString() == "Count")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [Pure]
        public static bool IsExpressionNotEqualToNull(this BoxedExpression exp, out BoxedExpression innerExp)
        {
            innerExp = null;

            if (exp == null)
            {
                return false;
            }

            BinaryOperator bop;
            BoxedExpression left, right;
            if (exp.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Cne_Un)
            {
                if (left.IsNull)
                {
                    innerExp = right;
                    return true;
                }
                if (right.IsNull)
                {
                    innerExp = left;
                    return true;
                }
            }

            return false;
        }

        [Pure]
        public static bool IsConstantLimitValue(this BoxedExpression be)
        {
            Contract.Requires(be != null);
            if (be.IsConstant)
            {
                int i;
                if (be.IsConstantInt(out i) && (Int32.MinValue == i || Int32.MaxValue == i))
                {
                    return true;
                }
                long l;
                if (be.IsConstantInt64(out l) && (Int64.MinValue == l || Int64.MaxValue == l))
                {
                    return true;
                }
            }

            return false;
        }

        [Pure]
        public static bool IsBinaryExpressionExpOpConst(this BoxedExpression exp, out BinaryOperator bop, out BoxedExpression left, out int value, bool preserveOrder = true)
        {
            Contract.Requires(exp != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out left) != null);

            BoxedExpression l, r;
            if (exp.IsBinaryExpression(out bop, out l, out r))
            {
                if (r.IsConstantInt(out value))
                {
                    left = l;
                    return true;
                }

                if (!preserveOrder)
                {
                    if (l.IsConstantInt(out value))
                    {
                        left = r;
                        return true;
                    }
                }
            }

            left = default(BoxedExpression);
            value = default(int);
            return false;
        }

        [Pure]
        public static bool IsArrayLength<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
          this BoxedExpression exp, APC pc, IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder)
          where Type : IEquatable<Type>
        {
            Contract.Requires(exp != null);
            Contract.Requires(Context != null);

            Variable var;
            if (exp.IsVariable && exp.TryGetFrameworkVariable(out var))
            {
                var accessPath = Context.ValueContext.AccessPathList(pc, var, false, false);
                // pattern match for the access path *.a.Length for some a
                if (accessPath != null)
                {
                    var accessPathArray = accessPath.ToArray();

                    Contract.Assume(Contract.ForAll(accessPathArray, p => p != null));

                    // a.Length?
                    if (accessPathArray.Length == 2)
                    {
                        if (IsArrayDotLength(accessPathArray[accessPathArray.Length - 2], accessPathArray[accessPathArray.Length - 1], MetaDataDecoder))
                            return true;
                    }

                    // *.a.Length? there is a deref
                    if (accessPathArray.Length >= 3 && accessPathArray[accessPathArray.Length - 2].IsDeref)
                    {
                        if (IsArrayDotLength(accessPathArray[accessPathArray.Length - 3], accessPathArray[accessPathArray.Length - 1], MetaDataDecoder))
                            return true;
                    }

                    return false;
                }
            }

            return false;
        }

        [Pure]
        public static BoxedExpression Stripped(this BoxedExpression exp)
        {
            Contract.Ensures(exp == null || Contract.Result<BoxedExpression>() != null);

            UnaryOperator uop;
            BoxedExpression inner;
            if (exp != null && exp.IsUnaryExpression(out uop, out inner) && uop.IsConversionOperator())
            {
                return inner;
            }

            return exp;
        }

        /// <summary>
        /// If exp == (int)a.Length, for some a, return a.Length
        /// </summary>
        [Pure]
        public static BoxedExpression StripIfCastOfArrayLength(this BoxedExpression exp)
        {
            Contract.Requires(exp != null);

            UnaryOperator uop;
            BoxedExpression inner;
            if (exp.IsCastExpression(out uop, out inner) && uop == UnaryOperator.Conv_i4 && inner.AccessPath != null)
            {
                var accessPathArray = inner.AccessPath.ToArray();

                if (accessPathArray.Length >= 2 && accessPathArray[accessPathArray.Length - 1].AssumeNotNull().ToString() == "Length")
                {
                    return inner;
                }
            }

            return exp;
        }

        [Pure]
        static private bool IsArrayDotLength<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          PathElement candidateArray, PathElement candidateLength,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder)
        {
            try
            {
                Type t;
                return candidateLength.ToString() == "Length" && candidateArray.TryGetResultType(out t) &&
                  !MetaDataDecoder.IsPrimitive(t) && (MetaDataDecoder.IsArray(t) || MetaDataDecoder.IsArray(MetaDataDecoder.ElementType(t)));
            }
            catch (NotImplementedException) // Our heuristic is partially pattern-based, so we may sometime fail
            {
                return false;
            }
        }

        [Pure]
        static public bool IsCastExpression(this BoxedExpression exp, out UnaryOperator cast, out BoxedExpression casted)
        {
            Contract.Requires(exp != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out casted) != null);

            if (exp.IsUnaryExpression(out cast, out casted) && cast.IsConversionOperator())
            {
                return true;
            }

            return false;
        }

        [Pure]
        static public bool IsCheckOfEnumValue<Variable>(this BoxedExpression exp, Predicate<Variable> IsEnumTyped)
        {
            Contract.Requires(IsEnumTyped != null);

            if (exp == null)
            {
                return false;
            }

            BinaryOperator bop;
            BoxedExpression left, right;
            if (exp.IsBinaryExpression(out bop, out left, out right) && (left.IsConstant || right.IsConstant))
            {
                if (right.IsConstant)
                {
                    return left.IsVariable && left.UnderlyingVariable is Variable && IsEnumTyped((Variable)left.UnderlyingVariable);
                }
                else
                {
                    Contract.Assert(left.IsConstant);
                    return right.IsVariable && right.UnderlyingVariable is Variable && IsEnumTyped((Variable)right.UnderlyingVariable);
                }
            }

            return false;
        }

        [Pure]
        static public bool IsTrivialCondition(this BoxedExpression exp, out bool result)
        {
            Contract.Requires(exp != null);

            // "constant"
            int value;
            if (exp.IsConstantIntOrNull(out value))
            {
                result = value != 0;

                return true;
            }

            if (exp.IsConstantBool(out result))
            {
                return true;
            }

            UnaryOperator uop;
            BoxedExpression unaryExp;
            if (exp.IsUnaryExpression(out uop, out unaryExp) && uop == UnaryOperator.Not)
            {
                bool innerResult;
                if (unaryExp.IsTrivialCondition(out innerResult))
                {
                    result = !innerResult;
                    return true;
                }
            }

            BinaryOperator bop;
            BoxedExpression left, right;
            if (exp.IsBinaryExpression(out bop, out left, out right))
            {
                int x, y;

                var leftConst = left.IsConstantIntOrNull(out x);
                var rightConst = right.IsConstantIntOrNull(out y);

                if (bop == BinaryOperator.Ceq)
                {
                    #region Equalities
                    // "assume NaN == NaN"
                    if (left.IsNaN() || right.IsNaN())
                    {
                        result = false;
                        return true;
                    }

                    if (leftConst && rightConst)
                    {
                        result = x == y;
                        return true;
                    }

                    if (rightConst && y == 0)
                    {
                        bool recursiveResult;
                        if (IsTrivialCondition(left, out recursiveResult))
                        {
                            result = !recursiveResult;
                            return true;
                        }
                    }

                    if (leftConst && x == 0)
                    {
                        bool recursiveResult;
                        if (IsTrivialCondition(right, out recursiveResult))
                        {
                            result = !recursiveResult;
                            return true;
                        }
                    }
                    #endregion
                }
                // simplify things like x < x
                if (bop == BinaryOperator.Clt || bop == BinaryOperator.Cgt || bop == BinaryOperator.Cne_Un)
                {
                    if (left.UnderlyingVariable != null && left.UnderlyingVariable.Equals(right.UnderlyingVariable))
                    {
                        return IsTrue(false, out result);
                    }
                }

                if (leftConst && rightConst)
                {
                    #region All the cases
                    switch (bop)
                    {
                        case BinaryOperator.Add:
                        case BinaryOperator.Add_Ovf:
                            return IsTrue(x + y != 0, out result);

                        case BinaryOperator.Add_Ovf_Un:
                            return IsTrue((uint)x + (uint)y != 0, out result);

                        case BinaryOperator.And:
                            return IsTrue((x & y) != 0, out result);

                        case BinaryOperator.Cge:
                            return IsTrue(x >= y, out result);

                        case BinaryOperator.Cge_Un:
                            return IsTrue((uint)x >= (uint)y, out result);

                        case BinaryOperator.Cgt:
                            return IsTrue(x > y, out result);

                        case BinaryOperator.Cgt_Un:
                            return IsTrue((uint)x > (uint)y, out result);

                        case BinaryOperator.Cle:
                            return IsTrue(x <= y, out result);

                        case BinaryOperator.Cle_Un:
                            return IsTrue((uint)x <= (uint)y, out result);

                        case BinaryOperator.Clt:
                            return IsTrue(x < y, out result);

                        case BinaryOperator.Clt_Un:
                            return IsTrue((uint)x < (uint)y, out result);

                        case BinaryOperator.Cne_Un:
                            return IsTrue((uint)x != (uint)y, out result);

                        case BinaryOperator.Div:
                            {
                                result = false;
                                return y != 0 ? IsTrue(x / y != 0, out result) : false;
                            }
                        case BinaryOperator.Div_Un:
                            {
                                result = false;
                                return y != 0 ? IsTrue((uint)x / (uint)y != 0, out result) : false;
                            }

                        case BinaryOperator.LogicalAnd:
                            return IsTrue(x != 0 && y != 0, out result);

                        case BinaryOperator.LogicalOr:
                            return IsTrue(x != 0 || y != 0, out result);

                        case BinaryOperator.Mul:
                            return IsTrue(x * y != 0, out result);

                        case BinaryOperator.Mul_Ovf_Un:
                            return IsTrue((uint)x * (uint)y != 0, out result);

                        case BinaryOperator.Or:
                            return IsTrue((x | y) != 0, out result);

                        case BinaryOperator.Rem:
                            return y != 0 ? IsTrue(x % y != 0, out result) : false;
                        case BinaryOperator.Rem_Un:
                            return IsTrue((uint)x % (uint)y != 0, out result);

                        case BinaryOperator.Shl:
                            return IsTrue((x << y) != 0, out result);

                        case BinaryOperator.Shr:
                            return IsTrue((x >> y) != 0, out result);

                        case BinaryOperator.Shr_Un:
                            return IsTrue(((uint)x >> y) != 0, out result);

                        case BinaryOperator.Sub:
                            return IsTrue(x - y != 0, out result);

                        case BinaryOperator.Sub_Ovf_Un:
                            return IsTrue(((uint)x - (uint)y) != 0, out result);

                        case BinaryOperator.Xor:
                            return IsTrue((x ^ y) != 0, out result);

                        default:
                            result = false;
                            return false;
                    }
                    #endregion
                }
            }

            result = default(bool);
            return false;
        }

        /// <summary>
        /// Try match the boxed expresion to (checkedExp == null) == 0
        /// </summary>
        [Pure]
        static public bool IsCheckExpNotNotNull(this BoxedExpression exp, out BoxedExpression checkedExp)
        {
            Contract.Requires(exp != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out checkedExp) != null);

            int val;
            BinaryOperator bop;
            BoxedExpression left, right;

            if (exp.IsBinaryExpression(out bop, out checkedExp, out right) && bop == BinaryOperator.Cne_Un && right.IsNull)
            {
                return true;
            }

            if (exp.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Ceq
              && right.IsConstantIntOrNull(out val) && val == 0)
            {
                if (left.IsBinaryExpression(out bop, out checkedExp, out right) && bop == BinaryOperator.Ceq && right.IsNull)
                {
                    return true;
                }
            }


            checkedExp = default(BoxedExpression);
            return false;
        }

        /// <summary>
        /// Try match the boxed expression to (exp1 == k)
        /// </summary>
        [Pure]
        static public bool IsCheckExp1EqConst(this BoxedExpression exp, out BoxedExpression exp1, out int k)
        {
            Contract.Requires(exp != null);
            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out exp1) != null));

            BinaryOperator bop;
            return exp.IsCheckExpOpConst(out bop, out exp1, out k) && bop == BinaryOperator.Ceq;
        }

        /// <summary>
        /// Try match the boxed expression to (var == k)
        /// </summary>
        [Pure]
        static public bool IsCheckExp1EqConst<Variable>(this BoxedExpression exp, out Variable var, out int k)
        {
            Contract.Requires(exp != null);

            BoxedExpression tmp;
            var = default(Variable);

            return exp.IsCheckExp1EqConst(out tmp, out k) && tmp.TryGetFrameworkVariable(out var);
        }

        /// <summary>
        /// Mathches (exp == k) or (!exp), in which case it returns (exp != 0)
        /// </summary>
        [Pure]
        static public bool IsCheckExpOpConst(this BoxedExpression exp, out BinaryOperator bop, out BoxedExpression exp1, out int k)
        {
            Contract.Requires(exp != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out exp1) != null);

            BoxedExpression exp2;
            k = default(int);
            if (exp.IsBinaryExpression(out bop, out exp1, out exp2) && exp2.IsConstantInt(out k))
            {
                return true;
            }

            UnaryOperator uop;
            if (exp.IsUnaryExpression(out uop, out exp1) && uop == UnaryOperator.Not)
            {
                bop = BinaryOperator.Ceq;
                k = 0;
                return true;
            }

            return false;
        }

        [Pure]
        static public bool IsCheckExpOpConst<Variable>(this BoxedExpression exp, out BinaryOperator bop, out Variable var, out int k)
        {
            BoxedExpression tmp;

            bop = default(BinaryOperator);
            var = default(Variable);
            k = default(int);

            return exp != null && exp.IsCheckExpOpConst(out bop, out tmp, out k) && tmp.TryGetFrameworkVariable(out var);
        }

        /// <summary>
        /// Try to match the boxed expression to (exp1 == exp2)
        /// </summary>
        [Pure]
        static public bool IsCheckExp1EqExp2(this BoxedExpression exp, out BoxedExpression exp1, out BoxedExpression exp2)
        {
            Contract.Requires(exp != null);
            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out exp1) != null));
            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out exp2) != null));

            BinaryOperator bop;

            if (exp.IsBinaryExpression(out bop, out exp1, out exp2))
            {
                switch (bop)
                {
                    // exp1 == exp2
                    case BinaryOperator.Ceq:
                        return true;

                    default:
                        return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Try to match the boxed expression to (v1 == v2)
        /// </summary>
        [Pure]
        static public bool IsCheckExp1EqExp2<Variable>(this BoxedExpression exp, out Variable v1, out Variable v2)
        {
            BoxedExpression e1, e2;
            v1 = v2 = default(Variable);
            return exp != null && exp.IsCheckExp1EqExp2(out e1, out e2) && e1.TryGetFrameworkVariable(out v1) && e2.TryGetFrameworkVariable(out v2);
        }

        /// <summary>
        /// Try to match the boxed expression to (exp1 != exp2)
        /// </summary>
        [Pure]
        static public bool IsCheckExp1NotEqExp2(this BoxedExpression exp, out BoxedExpression exp1, out BoxedExpression exp2)
        {
            Contract.Requires(exp != null);
            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out exp1) != null));
            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out exp2) != null));

            BinaryOperator bop;

            if (exp.IsBinaryExpression(out bop, out exp1, out exp2))
            {
                switch (bop)
                {
                    case BinaryOperator.Cne_Un:
                        {
                            return true;
                        }

                    case BinaryOperator.Ceq:
                        {
                            int value;
                            if (exp2.IsConstantIntOrNull(out value) && value == 0)
                            {
                                return exp1.IsCheckExp1EqExp2(out exp1, out exp2);
                            }
                            else
                            {
                                return false;
                            }
                        }
                }
            }

            return false;
        }

        /// <summary>
        /// Try to match the boxed expression to (v1 != v2)
        /// </summary>
        [Pure]
        static public bool IsCheckExp1NotEqExp2<Variable>(this BoxedExpression exp, out Variable v1, out Variable v2)
        {
            Contract.Requires(exp != null);

            BoxedExpression exp1, exp2;

            v1 = v2 = default(Variable);
            return exp.IsCheckExp1NotEqExp2(out exp1, out exp2) && exp1.TryGetFrameworkVariable(out v1) && exp2.TryGetFrameworkVariable(out v2);
        }

        /// <summary>
        /// Try to match the boxed expression to (left \leq right).
        /// It is ok to pass null. The postcondition ensures that if we return true, then be != null
        /// </summary>
        [Pure]
        static public bool IsCheckExp1LEQExp2(this BoxedExpression be, out BoxedExpression exp1, out BoxedExpression exp2)
        {
            Contract.Ensures(!Contract.Result<bool>() || be != null);
            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out exp1) != null));
            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out exp2) != null));

            BinaryOperator bop;
            if (be != null && be.IsBinaryExpression(out bop, out exp1, out exp2))
            {
                switch (bop)
                {
                    case BinaryOperator.Ceq:
                        {
                            int value;
                            if (exp2.IsConstantIntOrNull(out value) && value == 0)
                            {
                                return exp1.IsCheckExp1LTExp2(out exp2, out exp1);
                            }
                            break;
                        }

                    case BinaryOperator.Cle:
                        {
                            return true;
                        }

                    default:
                        return false;
                }
            }

            exp1 = exp2 = default(BoxedExpression);

            return false;
        }

        /// <summary>
        /// Try to match the boxed expression to (left \lt right).
        /// It is ok to pass null as be. The contract ensures that if true is returned then be != null
        /// </summary>
        [Pure]
        static public bool IsCheckExp1LTExp2(this BoxedExpression be, out BoxedExpression exp1, out BoxedExpression exp2)
        {
            Contract.Ensures(!Contract.Result<bool>() || be != null);
            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out exp1) != null));
            Contract.Ensures(!Contract.Result<bool>() || (Contract.ValueAtReturn(out exp2) != null));

            BinaryOperator bop;
            if (be != null && be.IsBinaryExpression(out bop, out exp1, out exp2))
            {
                switch (bop)
                {
                    case BinaryOperator.Ceq:
                        {
                            int value;
                            if (exp2.IsConstantIntOrNull(out value) && value == 0)
                            {
                                return exp1.IsCheckExp1LEQExp2(out exp2, out exp1);
                            }
                            break;
                        }

                    case BinaryOperator.Clt:
                        {
                            return true;
                        }

                    default:
                        return false;
                }
            }

            exp1 = exp2 = default(BoxedExpression);

            return false;
        }

        private static bool IsTrue(bool condition, out bool result)
        {
            result = condition;
            return true;
        }

        [Pure]
        public static bool TryGetFrameworkVariable<Variable>(this BoxedExpression be, out Variable var)
        {
            Contract.Requires(be != null);

            var loc = be.UnderlyingVariable;
            var asBoxedVar = loc as BoxedVariable<Variable>;

            if (asBoxedVar != null && asBoxedVar.TryUnpackVariable(out var))
            {
                return true;
            }

            if (loc is Variable)
            {
                var = (Variable)loc;
                return true;
            }

            var = default(Variable);
            return false;
        }

        /// <summary>
        /// Searches an ArrayIndexExpression in exp with index == index.
        /// If there are more than one, it returns the first one.
        /// </summary>    
        public static bool TryFindArrayExp<Typ>(this BoxedExpression exp, BoxedExpression index,
          out BoxedExpression.ArrayIndexExpression<Typ> res)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out res) != null);

            if (exp == null)
            {
                res = default(BoxedExpression.ArrayIndexExpression<Typ>);
                return false;
            }

            res = exp as BoxedExpression.ArrayIndexExpression<Typ>;
            if (res != null)
            {
                // Looking for a[index]
                // TODO: Extend to more complex indexing
                return index == res.Index;
            }

            UnaryOperator unaryDummy;
            BoxedExpression unaryExp;
            if (exp.IsUnaryExpression(out unaryDummy, out unaryExp))
            {
                return unaryExp.TryFindArrayExp(index, out res);
            }

            BinaryOperator dummy;
            BoxedExpression left, right;
            if (exp.IsBinaryExpression(out dummy, out left, out right))
            {
                return left.TryFindArrayExp(index, out res) || right.TryFindArrayExp(index, out res);
            }

            res = null;
            return false;
        }

        public static bool TryFindArrayExpBinOpArrayExp<Typ>(this BoxedExpression exp,
          BoxedExpression index,
          out BinaryOperator bop,
          out BoxedExpression.ArrayIndexExpression<Typ> left,
          out BoxedExpression.ArrayIndexExpression<Typ> right)
        {
            Contract.Requires(exp != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out left) != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out right) != null);

            BoxedExpression leftExp, rightExp;
            if (exp.IsBinaryExpression(out bop, out leftExp, out rightExp) && leftExp.TryFindArrayExp(index, out left))
            {
                return rightExp.TryFindArrayExp(index, out right);
            }

            left = right = default(BoxedExpression.ArrayIndexExpression<Typ>);
            return false;
        }

        public static IEnumerable<BoxedExpression> SyntacticReductionRemoval(this IEnumerable<BoxedExpression> input,
          bool removeChecksWithMinValue = false, bool keepStronger = true)
        {
            if (input == null)
            {
                return input;
            }

            var tmp = new List<BoxedExpression>();

            // 1. Remove doubles
            // 2. Put all the binary expressions in the form a {<, <= } b
            foreach (var b in input.Distinct())
            {
                var toAdd = b;
                BinaryOperator bop;
                BoxedExpression left, right;
                if (b.IsBinaryExpression(out bop, out left, out right) &&
                  (bop == BinaryOperator.Cge || bop == BinaryOperator.Cge_Un || bop == BinaryOperator.Cgt || bop == BinaryOperator.Cgt_Un))
                {
                    if (bop.TryInvert(out bop))
                    {
                        toAdd = BoxedExpression.Binary(bop, right, left);
                    }
                    else
                    {
                        Contract.Assume(false, "impossible?");
                    }
                }

                tmp.Add(toAdd);
            }

            var result = new List<BoxedExpression>(tmp.Count);

            // 3. if a < b and a <= b are in input keep a < b if keepStronger==true, otherwise a <= b
            // 4. if a <= b and b is MaxValue or a is MinValue then drop them -- TODO use types
            foreach (var b in tmp)
            {
                BinaryOperator bop;
                BoxedExpression left, right;
                if (b.IsBinaryExpression(out bop, out left, out right))
                {
                    if (keepStronger)
                    {            // if b is a <= b, then do not put it if there is a a < b in the list
                        if (bop == BinaryOperator.Cle)
                        {
                            if (tmp.Contains(BoxedExpression.Binary(BinaryOperator.Clt, left, right)))
                            {
                                continue;
                            }
                            if (removeChecksWithMinValue)
                            {
                                int k;
                                if (left.IsConstantInt(out k) && k == Int32.MinValue)
                                {
                                    continue;
                                }
                                if (right.IsConstantInt(out k) && k == Int32.MaxValue)
                                {
                                    continue;
                                }
                            }
                        }
                        if (bop == BinaryOperator.Cle_Un)
                        {
                            if (tmp.Contains(BoxedExpression.Binary(BinaryOperator.Clt_Un, left, right)))
                            {
                                continue;
                            }
                            if (removeChecksWithMinValue)
                            {
                                int k;
                                if (left.IsConstantInt(out k) && k == Int32.MinValue)
                                {
                                    continue;
                                }
                                if (right.IsConstantInt(out k) && k == Int32.MaxValue)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (bop == BinaryOperator.Clt && tmp.Contains(BoxedExpression.Binary(BinaryOperator.Cle, left, right)))
                        {
                            continue;
                        }
                        if (bop == BinaryOperator.Clt_Un && tmp.Contains(BoxedExpression.Binary(BinaryOperator.Cle_Un, left, right)))
                        {
                            continue;
                        }
                    }
                }
                result.Add(b);
            }

            return result;
        }

        static public ExpressionInPreStateKind GuessExpressionInPreStateKind<Local, Parameter, Method, Field, Type, Variable>
          (this BoxedExpression condition, APC pc, IValueContextData<Local, Parameter, Method, Field, Type, Variable> context, Func<FList<PathElement>, bool> IsReadonlyField)
        where Type : IEquatable<Type>
        {
            Contract.Requires(condition != null);
            Contract.Requires(IsReadonlyField != null);

            var inferKinds = condition.GuessExpressionInPreStateKind_Internal(pc, context, IsReadonlyField);
            switch (inferKinds)
            {
                case EnumType.Assume:
                    return ExpressionInPreStateKind.Assume;

                case EnumType.Neutral:
                case EnumType.Parameter:
                case EnumType.PublicField:
                    return ExpressionInPreStateKind.MethodPrecondition;

                case EnumType.PrivateField:
                    return ExpressionInPreStateKind.ObjectInvariant;

                default:
                    Contract.Assert(false);
                    break;
            }

            // Should be unreachable
            return ExpressionInPreStateKind.Any;
        }

        private enum EnumType { Neutral, Parameter, PublicField, PrivateField, Assume }

        static private EnumType GuessExpressionInPreStateKind_Internal<Local, Parameter, Method, Field, Type, Variable>
        (this BoxedExpression condition, APC pc, IValueContextData<Local, Parameter, Method, Field, Type, Variable> context, Func<FList<PathElement>, bool> IsReadonlyField)
          where Type : IEquatable<Type>
        {
            Contract.Requires(IsReadonlyField != null);

            #region All the cases

            if (condition.IsVariable)
            {
                // Special case as we may have some slack variable
                Variable var;
                if (!condition.TryGetFrameworkVariable(out var))
                {
                    return EnumType.Neutral;
                }

                // First check if we've got a constant
                Type type;
                object value;
                if (context.IsConstant(pc, var, out type, out value))
                {
                    return EnumType.Neutral;
                }

                FList<PathElement> accessPath;
                accessPath = context.VisibleAccessPathListFromPre(pc, var);

                if (accessPath != null)
                {
                    if (accessPath.Head.ToString() == "this")
                    {
                        return EnumType.PublicField;
                    }
                    else
                    {
                        return EnumType.Parameter;
                    }
                }

                accessPath = context.AccessPathList(pc, var, false, false);
                if (accessPath != null && context.PathSuitableInRequires(pc, accessPath) && accessPath.Head.AssumeNotNull().IsParameter)
                {
                    if (accessPath.Head.ToString() == "this")
                    {
                        return IsReadonlyField(accessPath) ? EnumType.PrivateField : EnumType.Assume;
                    }
                    else
                    {
                        return EnumType.Parameter;
                    }
                }

                // F: HackHack: sometimes the two tests above return null, but condition has an access path. I do not know why
                if (condition.AccessPath != null && condition.AccessPath.Length > 0 && condition.AccessPath[0].AssumeNotNull().ToString() == "this")
                {
                    return IsReadonlyField(accessPath) ? EnumType.PrivateField : EnumType.Assume;
                }

                return EnumType.Neutral;
            }

            if (condition.IsConstant || condition.IsNull || condition.IsSizeOf)
            {
                return EnumType.Neutral;
            }
            if (condition.IsUnary)
            {
                return condition.UnaryArgument.GuessExpressionInPreStateKind_Internal(pc, context, IsReadonlyField);
            }
            if (condition.IsBinary)
            {
                var left = condition.BinaryLeft.GuessExpressionInPreStateKind_Internal(pc, context, IsReadonlyField);
                var right = condition.BinaryRight.GuessExpressionInPreStateKind_Internal(pc, context, IsReadonlyField);

                return Join(left, right);
            }

            BoxedExpression arrayExp, indexExp;
            object t;
            if (condition.IsArrayIndexExpression(out arrayExp, out indexExp, out t) && t is Type)
            {
                var left = arrayExp.GuessExpressionInPreStateKind_Internal(pc, context, IsReadonlyField);
                var right = indexExp.GuessExpressionInPreStateKind_Internal(pc, context, IsReadonlyField);

                return Join(left, right);
            }

            bool isForAll;
            BoxedExpression boundExp, lowerBound, upperBound, body;
            if (condition.IsQuantifiedExpression(out isForAll, out boundExp, out lowerBound, out upperBound, out body))
            {
                var newLowerBound = lowerBound.GuessExpressionInPreStateKind_Internal(pc, context, IsReadonlyField);
                var newUpperBound = upperBound.GuessExpressionInPreStateKind_Internal(pc, context, IsReadonlyField);
                var newBody = body.GuessExpressionInPreStateKind_Internal(pc, context, IsReadonlyField);

                return Join(newLowerBound, Join(newUpperBound, newBody));
            }

            return EnumType.Parameter;

            #endregion
        }

        private static EnumType Join(EnumType left, EnumType right)
        {
            switch (left)
            {
                case EnumType.Neutral:
                    {
                        return right;
                    }

                case EnumType.Parameter:
                    switch (right)
                    {
                        case EnumType.Neutral:
                            return EnumType.Parameter;

                        case EnumType.Parameter:
                            return EnumType.Parameter;

                        case EnumType.PrivateField:
                            return EnumType.Assume;

                        case EnumType.PublicField:
                            return EnumType.Parameter;

                        case EnumType.Assume:
                            return EnumType.Assume;

                        default:
                            throw new InvalidOperationException();
                    }

                case EnumType.PublicField:
                    switch (right)
                    {
                        case EnumType.Neutral:
                            return EnumType.PublicField;

                        case EnumType.Parameter:
                            return EnumType.Parameter;

                        case EnumType.PublicField:
                            return EnumType.PublicField;

                        case EnumType.PrivateField:
                            return EnumType.PrivateField;

                        case EnumType.Assume:
                            return EnumType.Assume;

                        default:
                            throw new InvalidOperationException();
                    }


                case EnumType.PrivateField:
                    switch (right)
                    {
                        case EnumType.Neutral:
                            return EnumType.PrivateField;

                        case EnumType.Parameter:
                            return EnumType.Assume;

                        case EnumType.PrivateField:
                            return EnumType.PrivateField;

                        case EnumType.PublicField:
                            return EnumType.PrivateField;

                        case EnumType.Assume:
                            return EnumType.Assume;

                        default:
                            throw new InvalidOperationException();
                    }

                case EnumType.Assume:
                    return EnumType.Assume;

                default:
                    throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Constructs  the expression "condition != k"
        /// </summary>
        public static BoxedExpression NotK<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          this BoxedExpression condition, object K, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder)
        {
            Contract.Requires(condition != null);
            Contract.Requires(metaDataDecoder != null);

            return BoxedExpression.Binary(BinaryOperator.Cne_Un, condition, BoxedExpression.Const(K, metaDataDecoder.System_Object, metaDataDecoder));
        }

        /// <summary>
        /// Constructs  the expression "condition == k"
        /// </summary>
        public static BoxedExpression EqualK<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
           this BoxedExpression condition, object K, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder)
        {
            Contract.Requires(condition != null);
            Contract.Requires(metaDataDecoder != null);

            return BoxedExpression.Binary(BinaryOperator.Ceq, condition, BoxedExpression.Const(K, metaDataDecoder.System_Object, metaDataDecoder));
        }

        /// <summary>
        /// Constructs "original + exps[0] + ... + exps[n]
        /// </summary>
        public static BoxedExpression Add(this BoxedExpression original, IEnumerable<BoxedExpression> exps)
        {
            Contract.Requires(original != null);

            var result = original;
            if (exps != null)
            {
                foreach (var exp in exps)
                {
                    result = BoxedExpression.Binary(BinaryOperator.Add, result, exp);
                }
            }
            return result;
        }

        /// <summary>
        /// Constructs "original - exps[0] - ... - exps[n]
        /// </summary>
        public static BoxedExpression Sub(this BoxedExpression original, IEnumerable<BoxedExpression> exps)
        {
            Contract.Requires(original != null);

            var result = original;
            if (exps != null)
            {
                foreach (var exp in exps)
                {
                    result = BoxedExpression.Binary(BinaryOperator.Sub, result, exp);
                }
            }
            return result;
        }

        static public Set<BoxedExpression> Constants(this BoxedExpression be)
        {
            Contract.Requires(be != null);
            Contract.Ensures(Contract.Result<Set<BoxedExpression>>() != null);

            var visitor = new ConstantsInBoxedExpression();

            be.Dispatch(visitor);

            return visitor.Constants;
        }

        static public bool ContainsReturnValue(this BoxedExpression be)
        {
            Contract.Requires(be != null);

            var visitor = new HasReturnValue();
            be.Dispatch(visitor);

            return visitor.Found;
        }

        static public Set<Variable> Variables<Variable>(this BoxedExpression be)
        {
            Contract.Requires(be != null);
            Contract.Ensures(Contract.Result<Set<Variable>>() != null);

            var visitor = new VariablesInBoxedExpression();

            be.Dispatch(visitor);

            return visitor.Variables.ConvertIf(obj => obj is Variable, obj => (Variable)obj) ?? new Set<Variable>();
        }

        static public Set<BoxedExpression> Variables(this BoxedExpression be)
        {
            Contract.Requires(be != null);
            Contract.Ensures(Contract.Result<Set<BoxedExpression>>() != null);

            var visitor = new VariablesInBoxedExpression();

            be.Dispatch(visitor);

            return visitor.VariablesAsBoxedExpressions ?? new Set<BoxedExpression>();
        }

        static public void Variables(this BoxedExpression be, out Set<object> frameworkVariables, out Set<BoxedExpression> beVariables, out bool onlyConstants)
        {
            Contract.Requires(be != null);
            Contract.Ensures(Contract.ValueAtReturn(out frameworkVariables) != null);
            Contract.Ensures(Contract.ValueAtReturn(out beVariables) != null);

            var visitor = new VariablesInBoxedExpression();

            be.Dispatch(visitor);

            frameworkVariables = visitor.Variables ?? new Set<object>();
            beVariables = visitor.VariablesAsBoxedExpressions ?? new Set<BoxedExpression>();
            onlyConstants = visitor.OnlyConstants;
        }


        static public Set<Variable> VariablesInnerNodes<Variable>(this BoxedExpression be)
        {
            Contract.Requires(be != null);
            Contract.Ensures(Contract.Result<Set<Variable>>() != null);

            var visitor = new FrameworkVariablesInBoxedExpression();
            be.Dispatch(visitor);

            return visitor.Variables.ConvertIf(obj => obj is Variable, obj => (Variable)obj) ?? new Set<Variable>();
        }

        static public bool CanPrintAsSourceCode(this BoxedExpression be)
        {
            if (be == null)
            {
                return false;
            }

            if (be.IsConstant)
            {
                return true;
            }

            Set<BoxedExpression> beVariables;
            Set<object> frameworkVariables;
            bool onlyConstants;

            be.Variables(out frameworkVariables, out beVariables, out onlyConstants);

            return onlyConstants || (frameworkVariables.Count == beVariables.Count && frameworkVariables.Count > 0 && beVariables.All(exp => exp.AccessPath != null));
        }

        public static BoxedExpression EvaluateConstants<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
        (this BoxedExpression be, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
            Contract.Requires(be != null);
            Contract.Requires(mdDecoder != null);

            var eval = new ConstantEvaluator<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(mdDecoder);

            return eval.Visit(be, new Void());
        }

#if bad
        public static BoxedExpression Rename<Variable>(
          this BoxedExpression original,
          IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
        {
            Contract.Requires(renaming != null);

            if (original == null)
            {
                return null;
            }

            UnaryOperator uop;
            BinaryOperator bop;
            BoxedExpression left, right;

            if (original.IsBinaryExpression(out bop, out left, out right))
            {
                var exp1 = left.Rename(renaming, refiner);

                if (exp1 == null)
                    return null;

                var exp2 = right.Rename(renaming, refiner);

                if (exp2 == null)
                    return null;

                return BoxedExpression.Binary(bop, exp1, exp2);
            }

            if (original.IsUnaryExpression(out uop, out left))
            {
                var exp1 = Rename(left, renaming, refiner);

                if (exp1 == null)
                    return null;

                return BoxedExpression.Unary(uop, exp1);
            }

            if (original.IsVariable)
            {
                Variable v;
                if (original.TryGetFrameworkVariable(out v))
                {
                    if (renaming.Contains(v))
                    {
                        if (refiner != null)
                        {
                            return refiner(renaming[v]);
                        }

                        return BoxedExpression.Var(renaming[v]);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return original;
                }
            }

            bool isForAll;
            BoxedExpression boundVar, inf, upp, body;
            if (original.IsQuantifiedExpression(out isForAll, out boundVar, out inf, out upp, out body))
            {
                if (boundVar == null)
                    return null;

                var infRenamed = inf.Rename(renaming);
                if (infRenamed != null)
                {
                    var uppRenamed = upp.Rename(renaming);
                    if (uppRenamed != null)
                    {
                        var bodyRenamed = body.Rename(renaming);
                        if (bodyRenamed != null)
                        {
                            if (isForAll)
                            {
                                return new ForAllIndexedExpression(null, boundVar, infRenamed, uppRenamed, bodyRenamed);
                            }
                            else
                            {
                                return new ExistsIndexedExpression(null, boundVar, infRenamed, uppRenamed, bodyRenamed);
                            }
                        }
                    }
                }

                return null;
            }
            if (original.IsArrayIndex)
            {
                var arrayIndexExp = (original as BoxedExpression.ArrayIndexExpression);
                Contract.Assume(arrayIndexExp != null);
                return arrayIndexExp.Rename<Variable>(renaming, refiner);
            }

            return original;
        }
#endif


        public static BoxedExpression Simplify<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
        (this BoxedExpression be, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          Func<BoxedExpression, bool> IsArrayLength = null,
          Func<BoxedExpression, BoxedExpression> ExtraSimplification = null,
          bool replaceIntConstantsByBooleans = true)
        {
            Contract.Requires(mdDecoder != null);

            // propagate null
            if (be == null)
            {
                return null;
            }

            bool result;
            if (replaceIntConstantsByBooleans && be.IsTrivialCondition(out result))
            {
                return result
                  ? BoxedExpression.ConstBool(true, mdDecoder)
                  : BoxedExpression.ConstBool(false, mdDecoder);
            }

            var simplified = be.SimplifyInternal(mdDecoder, IsArrayLength);

            return ExtraSimplification != null ? ExtraSimplification(simplified) : simplified;
        }

        private static BoxedExpression SimplifyInternal<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          this BoxedExpression be,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          Func<BoxedExpression, bool> IsArrayLength)
        {
            Contract.Requires(be != null);
            Contract.Requires(mdDecoder != null);

            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            BinaryOperator bop, negatedBop;
            BoxedExpression left, right;

            if (be.IsUnary)
            {
                var uop = be.UnaryOp;
                var uarg = be.UnaryArgument;

                var rec = uarg.SimplifyInternal(mdDecoder, IsArrayLength);

                if (IsArrayLength != null && IsArrayLength(rec))
                {
                    return rec;
                }

                // !(a == b) --> (a != n) and similar cases...
                // !(a %b) --> (a %b) == 0
                if (uop == UnaryOperator.Not && rec.IsBinaryExpression(out bop, out left, out right))
                {
                    if (bop.TryNegate(out negatedBop))
                    {
                        return BoxedExpression.Binary(negatedBop, left, right);
                    }
                    else if (bop.IsArithmeticBinaryOperator() || bop.IsBitwiseBinaryOperator())
                    /*          {
                                return BoxedExpression.Binary(BinaryOperator.Cne_Un, rec, BoxedExpression.Const(0, mdDecoder.System_Int32, mdDecoder));
                              }
                              else if (bop.IsBitwiseBinaryOperator())

                     */
                    {
                        return BoxedExpression.Binary(BinaryOperator.Ceq, rec, BoxedExpression.Const(0, mdDecoder.System_Int32, mdDecoder));
                    }
                }

                return uarg.Equals(rec) ? be : BoxedExpression.Unary(uop, rec);
            }

            if (be.IsBinaryExpression(out bop, out left, out right))
            {
                var recLeft = left.SimplifyInternal(mdDecoder, IsArrayLength);
                /*
                if (recLeft == null)
                {
                  return be;
                }
                */
                var recRight = right.SimplifyInternal(mdDecoder, IsArrayLength);
                /*
                if (recRight == null)
                {
                  return be;
                }
                */
                // Handle cases as (x + 1) + 1 or (x -1) -1
                ApplySimpleConstantPropagation(bop, ref recLeft, ref recRight, mdDecoder);

                if (ApplyIdentities(mdDecoder, bop, ref recLeft, ref recRight))
                {
                    #region Cases
                    if (recRight == null)
                    {
                        if (recLeft != null)
                        {
                            return recLeft;
                        }
                        // else we simplified both branches, and we give up by returning the original expression
                        else
                        {
                            return be;
                        }
                    }
                    else if (recLeft == null)
                    {
                        return recRight;
                    }

                    Contract.Assert(recLeft != null);
                    Contract.Assert(recRight != null);
                    #endregion
                }
                else
                {
                    // Apply identities may modify one of the two to null
                    // We make sure this will not be the case
                    // This bug was found by Clousot
                    if (recLeft == null || recRight == null)
                    {
                        return be;
                    }

                    int k1, k2;
                    if (recLeft.IsConstantInt(out k1) && recRight.IsConstantInt(out k2))
                    {
                        #region All the cases
                        bool? truthValue = null;

                        // we do not do the *_un version because we should make the difference between unsigned and floats
                        switch (bop)
                        {
                            case BinaryOperator.Ceq:
                                truthValue = k1 == k2;
                                break;

                            case BinaryOperator.Cge:
                                truthValue = k1 >= k2;
                                break;

                            case BinaryOperator.Cgt:
                                truthValue = k1 > k2;
                                break;

                            case BinaryOperator.Cle:
                                truthValue = k1 <= k2;
                                break;

                            case BinaryOperator.Clt:
                                truthValue = k1 < k2;
                                break;

                            case BinaryOperator.Cne_Un:
                                truthValue = k1 != k2;
                                break;
                        }

                        if (truthValue.HasValue)
                        {
                            return BoxedExpression.ConstBool(truthValue.Value, mdDecoder);
                        }
                        #endregion
                    }

                    if (bop == BinaryOperator.Ceq || bop == BinaryOperator.Cne_Un)
                    {
                        #region The cases
                        // recLeft {==, !=} recRight ==> {"true", "false"} 
                        if (recLeft.Equals(recRight))
                        {
                            return BoxedExpression.ConstBool(bop == BinaryOperator.Ceq ? true : false, mdDecoder);
                        }

                        // Pattern matches (recLeftLeft { relOp } recRightRight) == {false, true}
                        BinaryOperator internalBop;
                        BoxedExpression recLeftLeft, recRightRight;
                        int k;
                        if (recLeft.IsBinaryExpression(out internalBop, out recLeftLeft, out recRightRight)
                          && internalBop.IsComparisonBinaryOperator()
                          // && recRight != null 
                          && recRight.IsConstantIntOrNull(out k))
                        {
                            if ((bop == BinaryOperator.Ceq && k != 0) || (bop == BinaryOperator.Cne_Un && k == 0))// true
                            {
                                return recLeft;
                            }
                            else  // false
                            {
                                BinaryOperator negatedOp;
                                if (internalBop.TryNegate(out negatedOp))
                                {
                                    return BoxedExpression.Binary(negatedOp, recLeftLeft, recRightRight);
                                }
                            }
                        }
                        #endregion
                    }

                    // a / k < a ==> a != 0
                    if (bop == BinaryOperator.Clt || bop == BinaryOperator.Cne_Un)
                    {
                        if (recLeft.IsBinary && recLeft.BinaryOp == BinaryOperator.Div && recLeft.BinaryRight.IsConstant && recLeft.BinaryLeft.Equals(recRight))
                        {
                            return BoxedExpression.Binary(BinaryOperator.Cne_Un, recLeft.BinaryLeft, BoxedExpression.Const(0, mdDecoder.System_Int32, mdDecoder));
                        }
                    }

#if false
                    if (bop.IsComparisonBinaryOperator())
                    {
                        var tmpLeft = recLeft;
                        var tmpRight = recRight;
                        if (ApplySimplifications(mdDecoder, ref recLeft, ref recRight))
                        {
                            //             recLeft = tmpLeft;
                            //              recRight = tmpRight;
                        }
                    }
#endif
                }

                Contract.Assert(recLeft != null); // Let us make sure when we reach this point the two are not null
                Contract.Assert(recRight != null);

                return left.Equals(recLeft) && right.Equals(recRight) ? be : BoxedExpression.Binary(bop, recLeft, recRight);
            }

            return be;
        }

        private static bool ApplySimpleConstantPropagation<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(BinaryOperator bop, ref BoxedExpression recLeft, ref BoxedExpression recRight, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
            Contract.Requires(recLeft != null);
            Contract.Ensures(recLeft != null);
            Contract.Ensures(recRight != null);

            int kRight;
            if ((bop == BinaryOperator.Add || bop == BinaryOperator.Add_Ovf || bop == BinaryOperator.Sub || bop == BinaryOperator.Sub_Ovf)
              && recRight.IsConstantInt(out kRight))
            {
                BinaryOperator leftOp;
                BoxedExpression leftleft;
                int kLeft;

                // examples: (x + 1) + 1 or (x - 1) - 1 or (x + 1) - 1 or (x - 1) + 1
                if (recLeft.IsBinaryExpressionExpOpConst(out leftOp, out leftleft, out kLeft)
                  && (leftOp == BinaryOperator.Add || leftOp == BinaryOperator.Sub))
                {
                    if (kLeft == Int32.MinValue)
                    {
                        return false;
                    }

                    switch (bop)
                    {
                        case BinaryOperator.Add:
                        case BinaryOperator.Add_Ovf:
                        case BinaryOperator.Sub:
                        case BinaryOperator.Sub_Ovf:
                            {
                                if (leftOp == BinaryOperator.Sub)
                                {
                                    kLeft = -kLeft;
                                }

                                Contract.Assert(leftleft != null);
                                recLeft = leftleft;
                                recRight = BoxedExpression.Const(
                                  bop == BinaryOperator.Add ? kRight + kLeft : kRight - kLeft,
                                  mdDecoder.System_Int32, mdDecoder);
                            }
                            return true;


                        default:
                            {
                            }
                            break;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// If a parameter is null, then it means we applied some cancellation rule
        /// </summary>
        private static bool ApplyIdentities<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          BinaryOperator bop, ref BoxedExpression recLeft, ref BoxedExpression recRight)
        {
            Contract.Requires(recLeft != null);
            Contract.Requires(recRight != null);

            int value;

            // We simplify identities
            switch (bop)
            {
                // a +0 == 0 + a == a
                case BinaryOperator.Add:
                case BinaryOperator.Add_Ovf:
                case BinaryOperator.Add_Ovf_Un:
                    {
                        recLeft = recLeft.IsConstantIntOrNull(out value) && value == 0 ? null : recLeft;
                        recRight = recRight.IsConstantIntOrNull(out value) && value == 0 ? null : recRight;

                        return true;
                    }

                // a - 0 == a
                case BinaryOperator.Sub:
                case BinaryOperator.Sub_Ovf:
                case BinaryOperator.Sub_Ovf_Un:
                    {
                        recRight = recRight.IsConstantIntOrNull(out value) && value == 0 ? null : recRight;

                        return true;
                    }

                // a / 1 == a
                case BinaryOperator.Div:
                case BinaryOperator.Div_Un:
                    {
                        recRight = recRight.IsConstantIntOrNull(out value) && value == 1 ? null : recRight;

                        return true;
                    }

                // a * 1 = a
                case BinaryOperator.Mul:
                case BinaryOperator.Mul_Ovf:
                case BinaryOperator.Mul_Ovf_Un:
                    {
                        recRight = recRight.IsConstantIntOrNull(out value) && value == 1 ? null : recRight;

                        return true;
                    }

                case BinaryOperator.Ceq:
                case BinaryOperator.Cne_Un:
                    {
                        object type;
                        long k;
                        bool b1 = false, b2 = false;
                        if ((recLeft.TryGetType(out type) && recRight.IsConstantInt64(out k) && (b1 = true)) ||
                          (recRight.TryGetType(out type) && recLeft.IsConstantInt64(out k) && (b2 = true)))
                        {
                            if (mdDecoder.System_UInt16.Equals(type))
                            {
                                if (b1)
                                {
                                    recRight = BoxedExpression.Const((UInt16)k, mdDecoder.System_UInt16, mdDecoder);
                                }
                                else
                                {
                                    Contract.Assert(b2);
                                    recLeft = BoxedExpression.Const((UInt16)k, mdDecoder.System_UInt16, mdDecoder);
                                }

                                return true;
                            }

                            if (mdDecoder.System_UInt32.Equals(type))
                            {
                                if (b1)
                                {
                                    recRight = BoxedExpression.Const((UInt32)k, mdDecoder.System_UInt32, mdDecoder);
                                }
                                else
                                {
                                    Contract.Assert(b2);
                                    recLeft = BoxedExpression.Const((UInt32)k, mdDecoder.System_UInt32, mdDecoder);
                                }

                                return true;
                            }
                        }
                    }
                    break;

                case BinaryOperator.LogicalOr:
                    {
                        // false || e == e
                        bool b;
                        recLeft = recLeft.IsConstantBool(out b) && !b ? null : recLeft;
                        recRight = recRight.IsConstantBool(out b) && !b ? null : recRight;

                        return recLeft == null || recRight == null;
                    }

                case BinaryOperator.LogicalAnd:
                    {
                        // true && e == e
                        bool b;
                        recLeft = recLeft.IsConstantBool(out b) && b ? null : recLeft;
                        recRight = recRight.IsConstantBool(out b) && b ? null : recRight;

                        return recLeft == null || recRight == null;
                    }
            }

            return false;
        }

        private static bool ApplySimplifications<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          ref BoxedExpression left, ref BoxedExpression right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            if (ApplySimplificationsInternal(mdDecoder, ref left, ref right))
            {
                return true;
            }
            if (ApplySimplificationsInternal(mdDecoder, ref right, ref left))
            {
                // We swapped the operands to simplify them. We should correct it
                var tmp = left;
                left = right;
                right = tmp;

                return true;
            }

            return false;
        }

        private static bool ApplySimplificationsInternal<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          ref BoxedExpression left, ref BoxedExpression right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(left != null);
            Contract.Ensures(right != null);

            BinaryOperator bop;
            BoxedExpression l;
            int k;
            if (left.IsBinaryExpressionExpOpConst(out bop, out l, out k, false))
            {
                switch (bop)
                {
                    case BinaryOperator.Mul:
                        {
                            if (/*l != null && */l.Equals(right))
                            {
                                right = BoxedExpression.Const(0, mdDecoder.System_Int32, mdDecoder);
                                if (k == 1)
                                {
                                    left = right; // i.e., zero
                                }
                                else if (k == 2)
                                {
                                    left = l;
                                }
                                else
                                {
                                    left = BoxedExpression.Binary(BinaryOperator.Mul, BoxedExpression.Const(k - 1, mdDecoder.System_Int32, mdDecoder), l);
                                }

                                return true;
                            }
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }

            return false;
        }

        private class FrameworkVariablesInBoxedExpression : IBoxedExpressionVisitor
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(variablesMaybeWithRepetitions != null);
            }

            readonly private List<object> variablesMaybeWithRepetitions;

            private Set<object> variables;

            public Set<object> Variables
            {
                get
                {
                    Contract.Ensures(Contract.Result<object>() != null);

                    if (variables == null)
                    {
                        variables = new Set<object>();
                        variables.AddRange(variablesMaybeWithRepetitions);
                    }

                    return variables;
                }
            }

            public FrameworkVariablesInBoxedExpression()
            {
                variablesMaybeWithRepetitions = new List<object>();
                variables = null;
            }

            #region IBoxedExpressionVisitor Members

            public void Variable(object var, PathElement[] path, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(var);
            }

            public void Constant<Type>(Type type, object value, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);
            }

            public void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);

                left.Dispatch(this);
                right.Dispatch(this);
            }

            public void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);

                argument.Dispatch(this);
            }

            public void SizeOf<Type>(Type type, int sizeAsConstant, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);
            }

            public void IsInst<Type>(Type type, BoxedExpression argument, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);

                argument.Dispatch(this);
            }

            public void ArrayIndex<Type>(Type type, BoxedExpression array, BoxedExpression index, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);

                array.Dispatch(this);
                index.Dispatch(this);
            }

            public void Result<Type>(Type type, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);
            }

            public void Old<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);

                expression.Dispatch(this);
            }

            public void ValueAtReturn<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);

                expression.Dispatch(this);
            }

            public void Assert(BoxedExpression condition, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);

                condition.Dispatch(this);
            }

            public void Assume(BoxedExpression condition, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);

                condition.Dispatch(this);
            }

            public void StatementSequence(IIndexable<BoxedExpression> statements, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);

                foreach (var statement in statements.Enumerate())
                {
                    statement.Dispatch(this);
                }
            }

            public void ForAll(BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body, BoxedExpression parent)
            {
                variablesMaybeWithRepetitions.Add(parent.UnderlyingVariable);

                boundVariable.Dispatch(this);
                lower.Dispatch(this);
                upper.Dispatch(this);
                body.Dispatch(this);
            }

            #endregion
        }

        private class VariablesInBoxedExpression : IBoxedExpressionVisitor
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.Variables != null);
            }

            readonly public Set<object> Variables;
            readonly public Set<BoxedExpression> VariablesAsBoxedExpressions;
            public bool OnlyConstants { get; private set; }

            public VariablesInBoxedExpression()
            {
                this.Variables = new Set<object>();
                this.VariablesAsBoxedExpressions = new Set<BoxedExpression>();
                this.OnlyConstants = true;
            }

            #region IBoxedExpressionVisitor Members

            public void Variable(object var, PathElement[] path, BoxedExpression parent)
            {
                this.OnlyConstants = false;
                Variables.Add(var);
                if (path != null)
                {
                    VariablesAsBoxedExpressions.Add(BoxedExpression.Var(var, path));
                }
            }

            public void Constant<Type>(Type type, object value, BoxedExpression parent)
            {
            }

            public void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression parent)
            {
                left.Dispatch(this);
                right.Dispatch(this);
            }

            public void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression parent)
            {
                argument.Dispatch(this);
            }

            public void SizeOf<Type>(Type type, int sizeAsConstant, BoxedExpression parent)
            {
            }

            public void IsInst<Type>(Type type, BoxedExpression argument, BoxedExpression parent)
            {
                argument.Dispatch(this);
            }

            public void ArrayIndex<Type>(Type type, BoxedExpression array, BoxedExpression index, BoxedExpression parent)
            {
                this.OnlyConstants = false;

                array.Dispatch(this);
                index.Dispatch(this);
            }

            public void Result<Type>(Type type, BoxedExpression parent)
            {
                this.OnlyConstants = false;
            }

            public void Old<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
            {
                this.OnlyConstants = false;
                expression.Dispatch(this);
            }

            public void ValueAtReturn<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
            {
                this.OnlyConstants = false;
                expression.Dispatch(this);
            }

            public void Assert(BoxedExpression condition, BoxedExpression parent)
            {
                this.OnlyConstants = false;
                condition.Dispatch(this);
            }

            public void Assume(BoxedExpression condition, BoxedExpression parent)
            {
                condition.Dispatch(this);
            }

            public void StatementSequence(IIndexable<BoxedExpression> statements, BoxedExpression parent)
            {
                foreach (var statement in statements.Enumerate())
                {
                    statement.Dispatch(this);
                }
            }

            public void ForAll(BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body, BoxedExpression parent)
            {
                this.OnlyConstants = false;

                boundVariable.Dispatch(this);
                lower.Dispatch(this);
                upper.Dispatch(this);
                body.Dispatch(this);
            }

            #endregion
        }

        // 

        private class ConstantsInBoxedExpression : IBoxedExpressionVisitor
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.Constants != null);
            }

            readonly public Set<BoxedExpression> Constants;

            public ConstantsInBoxedExpression()
            {
                this.Constants = new Set<BoxedExpression>();
            }

            #region IBoxedExpressionVisitor Members

            public void Variable(object var, PathElement[] path, BoxedExpression parent)
            {
            }

            public void Constant<Type>(Type type, object value, BoxedExpression parent)
            {
                this.Constants.Add(parent);
            }

            public void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression parent)
            {
                left.Dispatch(this);
                right.Dispatch(this);
            }

            public void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression parent)
            {
                argument.Dispatch(this);
            }

            public void SizeOf<Type>(Type type, int sizeAsConstant, BoxedExpression parent)
            {
            }

            public void IsInst<Type>(Type type, BoxedExpression argument, BoxedExpression parent)
            {
                argument.Dispatch(this);
            }

            public void ArrayIndex<Type>(Type type, BoxedExpression array, BoxedExpression index, BoxedExpression parent)
            {
                array.Dispatch(this);
                index.Dispatch(this);
            }

            public void Result<Type>(Type type, BoxedExpression parent)
            {
            }

            public void Old<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
            {
                expression.Dispatch(this);
            }

            public void ValueAtReturn<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
            {
                expression.Dispatch(this);
            }

            public void Assert(BoxedExpression condition, BoxedExpression parent)
            {
                condition.Dispatch(this);
            }

            public void Assume(BoxedExpression condition, BoxedExpression parent)
            {
                condition.Dispatch(this);
            }

            public void StatementSequence(IIndexable<BoxedExpression> statements, BoxedExpression parent)
            {
                foreach (var statement in statements.Enumerate())
                {
                    statement.Dispatch(this);
                }
            }

            public void ForAll(BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body, BoxedExpression parent)
            {
                boundVariable.Dispatch(this);
                lower.Dispatch(this);
                upper.Dispatch(this);
                body.Dispatch(this);
            }

            #endregion
        }


        private class HasReturnValue : IBoxedExpressionVisitor
        {
            public bool Found { get; private set; }
            public HasReturnValue()
            {
                this.Found = false;
            }

            public void Variable(object var, PathElement[] path, BoxedExpression original)
            {
            }

            public void Constant<Type>(Type type, object value, BoxedExpression original)
            {
            }

            public void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression original)
            {
                left.Dispatch(this);
                right.Dispatch(this);
            }

            public void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression original)
            {
                argument.Dispatch(this);
            }

            public void SizeOf<Type>(Type type, int sizeAsConstant, BoxedExpression original)
            {
            }

            public void IsInst<Type>(Type type, BoxedExpression argument, BoxedExpression original)
            {
                argument.Dispatch(this);
            }

            public void ArrayIndex<Type>(Type type, BoxedExpression array, BoxedExpression index, BoxedExpression original)
            {
                array.Dispatch(this);
                index.Dispatch(this);
            }

            public void Result<Type>(Type type, BoxedExpression original)
            {
                this.Found = true;
            }

            public void Old<Type>(Type type, BoxedExpression expression, BoxedExpression original)
            {
                expression.Dispatch(this);
            }

            public void ValueAtReturn<Type>(Type type, BoxedExpression expression, BoxedExpression original)
            {
                expression.Dispatch(this);
            }

            public void Assert(BoxedExpression condition, BoxedExpression original)
            {
            }

            public void Assume(BoxedExpression condition, BoxedExpression original)
            {
            }

            public void StatementSequence(IIndexable<BoxedExpression> statements, BoxedExpression original)
            {
                for (var i = 0; i < statements.Count; i++)
                {
                    statements[i].AssumeNotNull().Dispatch(this);
                }
            }

            public void ForAll(BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body, BoxedExpression original)
            {
                boundVariable.Dispatch(this);
                lower.Dispatch(this);
                upper.Dispatch(this);
                body.Dispatch(this);
            }
        }
    }

    [ContractVerification(true)]
    public static class ListOfBoxedExpressionsExtensions
    {
        public static BoxedExpression ToDisjunction(this List<BoxedExpression> disjuncts)
        {
            if (disjuncts == null || disjuncts.Count == 0)
            {
                return null;
            }

            var result = disjuncts[0];
            Contract.Assume(result != null);

            for (var i = 1; i < disjuncts.Count; i++)
            {
                var disjunct = disjuncts[i];
                if (disjunct != null)
                {
                    result = BoxedExpression.Binary(BinaryOperator.LogicalOr, result, disjunct);
                }
            }

            return result;
        }
    }

    public static class IDecodeMetaDataExtensions
    {
        public static bool IsReadOnly<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          this IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          FList<PathElement> accessPath)
          where Type : IEquatable<Type>
        {
            Contract.Requires(mdDecoder != null);

            var accessPathArray = accessPath.ToArray();

            Contract.Assume(Contract.ForAll(accessPathArray, p => p != null));

            if (accessPathArray.Length < 2)
            {
                return false;
            }

            Field f;

            if (accessPathArray.Length == 2)
            {
                return accessPathArray[1].TryField(out f) && mdDecoder.IsReadonly(f);
            }

            Contract.Assert(accessPathArray.Length > 2, "trivial, but make it sure");

            // F: HACK - We need to pass the value context to check if the field is an array!
            return
              accessPathArray[accessPathArray.Length - 1].IsDeref && accessPathArray[accessPathArray.Length - 2].TryField(out f) && mdDecoder.IsReadonly(f) ||
              accessPathArray[accessPathArray.Length - 3].TryField(out f) && mdDecoder.IsReadonly(f) && accessPathArray[accessPathArray.Length - 1].ToString() == "Length";
        }
    }

    [ContractVerification(true)]
    public static class PathElementExtensions
    {
        public static bool ContainsStaticFieldAccess<Field>(this PathElement[] pathElement)
        {
            Contract.Requires(pathElement != null);

            for (var i = 0; i < pathElement.Length; i++)
            {
                var element = pathElement[i];
                Contract.Assume(element != null);

                Field f;
                if (element.IsStatic && element.TryField(out f))
                {
                    return true;
                }
            }

            return false;
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        public static bool IsNaNCall(this PathElement[] path, out bool isFloat64)
        {
            isFloat64 = false;

            if (path == null || path.Length != 2)
            {
                return false;
            }

            var candidate = path[1];

            if (candidate == null)
            {
                return false;
            }

            if (candidate.IsMethodCall && candidate.IsStatic)
            {
                var str = candidate.ToString();
                if (str.EndsWith("IsNaN"))
                {
                    var strLowerCase = str.Split('.')[0].ToLower();
                    switch (strLowerCase)
                    {
                        case "float":
                        case "float32":
                        case "system.float32":
                        case "system.float":
                            {
                                isFloat64 = false;
                                return true;
                            }

                        case "double":
                        case "float64":
                        case "system.float64":
                        case "system.double":
                            {
                                isFloat64 = true;
                                return true;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }

            return false;
        }

        public static bool IsInfinityCall(this PathElement[] path, out bool isFloat64, out bool isPlusInfinty, out bool isMinusInfinity)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out isPlusInfinty) || Contract.ValueAtReturn(out isMinusInfinity));

            isFloat64 = isPlusInfinty = isMinusInfinity = false;
            if (path != null && path.Length == 2)
            {
                var candidate = path[1];

                if (candidate == null)
                {
                    return false;
                }

                if (candidate.IsStatic && candidate.IsMethodCall)
                {
                    var str = candidate.ToString();
                    if (str.EndsWith("Infinity"))
                    {
                        var split = str.ToLower().Split('.');
                        if (split.Length == 2)
                        {
                            switch (split[0])
                            {
                                case "float":
                                case "float32":
                                case "system.float32":
                                case "system.float":
                                    {
                                        isFloat64 = false;
                                        break;
                                    }

                                case "double":
                                case "float64":
                                case "system.float64":
                                case "system.double":
                                    {
                                        isFloat64 = true;
                                        break;
                                    }

                                default:
                                    {
                                        return false;
                                    }
                            }

                            switch (split[1])
                            {
                                case "isinfinity":
                                    {
                                        isPlusInfinty = true;
                                        isMinusInfinity = true;
                                        break;
                                    }

                                case "isminusinfinity":
                                    {
                                        isPlusInfinty = false;
                                        isMinusInfinity = true;
                                        break;
                                    }

                                case "isplusinfinity":
                                    {
                                        isPlusInfinty = true;
                                        isMinusInfinity = false;
                                        break;
                                    }

                                default:
                                    {
                                        return false;
                                    }
                            }

                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }

    public static class APCExtensions
    {
        public static APC GetFirstSuccessorWithSourceContext(this APC pc, ICFG cfg)
        {
            Contract.Requires(cfg != null);

            if (pc.HasRealSourceContext)
            {
                return pc;
            }

            var oldPC = pc;

            for (var i = 0; i < 10; i++)
            {
                pc = cfg.Post(pc);
                if (pc.HasRealSourceContext)
                {
                    return pc;
                }
            }

            return pc; // We failed. This shoulnd never happen
        }

        /*
        // this recursive implementation caused stack overflow in mscorlib analysis
        public static APC GetFirstPredecessorWithSourceContext(this APC pc, ICFG cfg)
        {
          Contract.Requires(cfg != null);

          if (pc.HasRealSourceContext || cfg.Predecessors(pc).Count() == 0)
          {
            return pc;
          }

          var pre = cfg.Predecessors(pc).First();

          if (pre.HasRealSourceContext)
          {
            return pre;
          }
          else
          {
            return pre.GetFirstPredecessorWithSourceContext(cfg);
          }
        }
         */

        public static APC GetFirstPredecessorWithSourceContext(this APC pc, ICFG cfg)
        {
            Contract.Requires(cfg != null);

            var currPC = pc;

            while (!currPC.HasRealSourceContext && cfg.Predecessors(currPC).Any())
            {
                currPC = cfg.Predecessors(currPC).First();
            }

            return currPC;
        }
    }
}