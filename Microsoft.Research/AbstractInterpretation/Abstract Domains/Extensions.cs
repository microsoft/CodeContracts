// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using Microsoft.Research.DataStructures;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains
{
    [ContractVerification(true)]
    public static class INumericalAbstractDomainExtensions
    {
        public static INumericalAbstractDomain<Variable, Expression> AssumeInInterval<Variable, Expression>(
          this INumericalAbstractDomain<Variable, Expression> aState,
          Expression exp, Interval intv, IExpressionEncoder<Variable, Expression> encoder)
        {
            Contract.Requires(aState != null);
            Contract.Requires(exp != null);
            Contract.Requires(intv != null);
            Contract.Requires(encoder != null);

            if (aState.IsBottom || intv.IsTop)
            {
                return aState;
            }

            if (intv.IsBottom)
            {
                return aState.Bottom as INumericalAbstractDomain<Variable, Expression>;
            }

            // a <= exp
            if (!intv.LowerBound.IsInfinity)
            {
                aState = aState.TestTrueLessEqualThan(intv.LowerBound.ToExpression(encoder), exp);
            }
            // b <= exp
            if (!intv.UpperBound.IsInfinity)
            {
                aState = aState.TestTrueLessEqualThan(exp, intv.UpperBound.ToExpression(encoder));
            }

            return aState;
        }

        public static INumericalAbstractDomain<Variable, Expression> TestTrueEqual<Variable, Expression>(
         this INumericalAbstractDomain<Variable, Expression> aState,
         Expression exp, IEnumerable<Expression> uppBounds)
        {
            Contract.Requires(aState != null);
            Contract.Requires(exp != null);
            Contract.Requires(uppBounds != null);

            Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

            if (aState.IsBottom)
            {
                return aState;
            }

            var result = aState;
            foreach (var upp in uppBounds)
            {
                Contract.Assume(upp != null);
                result = result.TestTrueEqual(exp, upp);
            }

            return result;
        }

        public static INumericalAbstractDomain<Variable, Expression> TestTrueEqual<Variable, Expression>(
          this INumericalAbstractDomain<Variable, Expression> aState,
          Expression exp, SetOfConstraints<Variable> equalities,
          IExpressionEncoder<Variable, Expression> encoder)
        {
            Contract.Requires(aState != null);
            Contract.Requires(exp != null);
            Contract.Requires(equalities != null);
            Contract.Requires(encoder != null);

            Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

            if (equalities.IsNormal())
            {
                var newSet = new Set<Expression>();
                foreach (var v in equalities.Values)
                {
                    newSet.Add(encoder.VariableFor(v));
                }

                return aState.TestTrueEqual(exp, newSet);
            }
            else
            {
                return aState;
            }
        }

        public static INumericalAbstractDomain<Variable, Expression> TestTrueLessThan<Variable, Expression>(
          this INumericalAbstractDomain<Variable, Expression> aState,
          Expression exp, IEnumerable<Expression> uppBounds)
        {
            Contract.Requires(aState != null);
            Contract.Requires(exp != null);
            Contract.Requires(uppBounds != null);

            Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

            if (aState.IsBottom)
            {
                return aState;
            }

            var result = aState;
            foreach (var upp in uppBounds)
            {
                Contract.Assume(upp != null);
                result = result.TestTrueLessThan(exp, upp);
            }

            return result;
        }

        public static INumericalAbstractDomain<Variable, Expression> TestTrueLessThan<Variable, Expression>(
          this INumericalAbstractDomain<Variable, Expression> aState,
          Expression exp, Set<Variable> uppBounds,
          IExpressionEncoder<Variable, Expression> encoder)
        {
            Contract.Requires(aState != null);
            Contract.Requires(exp != null);
            Contract.Requires(uppBounds != null);
            Contract.Requires(encoder != null);

            Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

            return aState.TestTrueLessThan(exp, uppBounds.ConvertAll(v => encoder.VariableFor(v)));
        }

        public static INumericalAbstractDomain<Variable, Expression> TestTrueLessThan<Variable, Expression>(
        this INumericalAbstractDomain<Variable, Expression> aState,
        Expression exp, SetOfConstraints<Variable> uppBounds,
        IExpressionEncoder<Variable, Expression> encoder)
        {
            Contract.Requires(aState != null);
            Contract.Requires(exp != null);
            Contract.Requires(uppBounds != null);
            Contract.Requires(encoder != null);

            Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

            if (uppBounds.IsNormal())
            {
                var newSet = new Set<Expression>();
                foreach (var v in uppBounds.Values)
                {
                    newSet.Add(encoder.VariableFor(v));
                }

                return aState.TestTrueLessThan(exp, newSet);
            }
            else
            {
                return aState;
            }
        }

        public static INumericalAbstractDomain<Variable, Expression> TestTrueLessEqualThan<Variable, Expression>(
      this INumericalAbstractDomain<Variable, Expression> aState,
      Expression exp, IEnumerable<Expression> uppBounds)
        {
            Contract.Requires(aState != null);
            Contract.Requires(exp != null);
            Contract.Requires(uppBounds != null);

            Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

            if (aState.IsBottom)
            {
                return aState;
            }

            var result = aState;
            foreach (var upp in uppBounds)
            {
                Contract.Assume(upp != null);
                result = result.TestTrueLessEqualThan(exp, upp);
            }

            return result;
        }

        public static INumericalAbstractDomain<Variable, Expression> TestTrueLessEqualThan<Variable, Expression>(
        this INumericalAbstractDomain<Variable, Expression> aState,
        Expression exp, SetOfConstraints<Variable> uppBounds,
        IExpressionEncoder<Variable, Expression> encoder)
        {
            Contract.Requires(aState != null);
            Contract.Requires(exp != null);
            Contract.Requires(uppBounds != null);
            Contract.Requires(encoder != null);

            Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

            if (uppBounds.IsNormal())
            {
                var newSet = new Set<Expression>();
                foreach (var v in uppBounds.Values)
                {
                    newSet.Add(encoder.VariableFor(v));
                }

                return aState.TestTrueLessEqualThan(exp, newSet);
            }
            else
            {
                return aState;
            }
        }

        public static INumericalAbstractDomain<Variable, Expression> TestTrue<Variable, Expression>(
          this INumericalAbstractDomain<Variable, Expression> aState,
          Variable v, NonRelationalValueAbstraction<Variable, Expression> nonRelationalValue,
          IExpressionEncoder<Variable, Expression> encoder)
        {
            Contract.Requires(v != null);
            Contract.Requires(aState != null);
            Contract.Requires(nonRelationalValue != null);
            Contract.Requires(encoder != null);

            Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

            if (nonRelationalValue.IsBottom)
            {
                Contract.Assume(aState.Bottom as INumericalAbstractDomain<Variable, Expression> != null); // F: Adding the assumption as we do not track types

                return aState.Bottom as INumericalAbstractDomain<Variable, Expression>;
            }

            if (nonRelationalValue.IsTop)
                return aState;

            var result = aState;

            // Assume the interval
            if (nonRelationalValue.Interval.IsNormal())
            {
                result.AssumeInDisInterval(v, nonRelationalValue.Interval);
            }

            var vExp = encoder.VariableFor(v);

            // Assume the equalities
            if (nonRelationalValue.Equalities.IsNormal())
            {
                result = result.TestTrueEqual(vExp, nonRelationalValue.Equalities, encoder);
            }

            // Assume the strict upper bounds
            if (nonRelationalValue.StrictUpperBounds.IsNormal())
            {
                result = result.TestTrueLessThan(vExp, nonRelationalValue.StrictUpperBounds, encoder);
            }

            // Assume the weak upper bounds
            if (nonRelationalValue.WeakUpperBounds.IsNormal())
            {
                result = result.TestTrueLessEqualThan(vExp, nonRelationalValue.WeakUpperBounds, encoder);
            }

            return result;
        }
    }

    [ContractVerification(true)]
    public static class INumericalAbstractDomainQueryExtensions
    {
        /// <summary>
        /// Checks if <code>e1 leq e2</code> using CheckLessThan of <code>dom</code>
        /// </summary>
        public static FlatAbstractDomain<bool> HelperForCheckLessEqualThan<Variable, Expression>(
          this INumericalAbstractDomainQuery<Variable, Expression> dom, Expression e1, Expression e2)
        {
            Contract.Requires(dom != null);
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            var result = dom.CheckIfLessThan(e1, e2);

            if (!result.IsNormal() || result.BoxedElement)
            {
                return result;
            }
            else // if (result.BoxedElement == false)
            {
                return CheckOutcome.Top;
            }
        }
    }

    [ContractVerification(true)]
    public static class SetOfConstraintsExtensions
    {
        [Pure]
        [SuppressMessage("Microsoft.Contracts", "Requires-30-36")]
        public static bool IsSingleton<El>(this SetOfConstraints<El> constraints, out El value)
        {
            if (constraints == null || !constraints.IsNormal() || constraints.Count < 1)
            {
                value = default(El);
                return false;
            }

            value = constraints.Values.First();

            return true;
        }
    }

    [ContractVerification(true)]
    public static class ArrayExtension
    {
        public static T[] Duplicate<T>(this T[] original)
        {
            if (original == null)
                return original;

            var result = new T[original.Length];
            Array.Copy(original, result, original.Length);

            return result;
        }

        public static int CountNotNull<T>(this T[] original)
          where T : class
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(original == null || Contract.Result<int>() <= original.Length);

            if (original == null)
            {
                return 0;
            }
            int count = 0;
            foreach (var x in original)
            {
                if (x != null) count++;
            }

            return count;
        }

        public static bool TryGetValue<Key, Value>(this Tuple<Key, Value>[] original, Key k, out Value v)
        {
            if (original == null)
            {
                v = default(Value);
                return false;
            }

            foreach (var x in original)
            {
                if (x != null)
                {
                    if (x.Item1.Equals(k))
                    {
                        v = x.Item2;
                        return true;
                    }
                }
            }

            v = default(Value);
            return false;
        }

        public static bool ContainsKey<Key, Value>(this Tuple<Key, Value>[] original, Key k)
        {
            if (original == null)
                return false;
            foreach (var x in original)
            {
                if (x != null)
                {
                    if (x.Item1.Equals(k))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool Remove<Key, Value>(this Tuple<Key, Value>[] original, Key k)
        {
            if (original == null)
                return false;
            for (var i = 0; i < original.Length; i++)
            {
                var item = original[i];
                if (item != null)
                {
                    if (item.Item1.Equals(k))
                    {
                        original[i] = null;
                        return true;
                    }
                }
            }

            return false;
        }

        public static Tuple<Key, Value>[] Add<Key, Value>(this Tuple<Key, Value>[] original, Key key, Value v)
        {
            var newValue = new Tuple<Key, Value>(key, v);

            if (original == null)
            {
                return new Tuple<Key, Value>[] { newValue };
            }

            var firstNullIndex = -1;
            for (var i = 0; i < original.Length; i++)
            {
                var item = original[i];
                if (item != null)
                {
                    if (item.Item1.Equals(key))
                    {
                        original[i] = newValue;

                        return original;
                    }
                }
                else if (firstNullIndex < 0)
                {
                    firstNullIndex = i;
                }
            }

            if (firstNullIndex >= 0)
            {
                original[firstNullIndex] = newValue;
                return original;
            }

            var result = original.Resize(1);
            result[result.Length - 1] = newValue;

            return result;
        }

        public static Tuple<Key, Value>[] Resize<Key, Value>(this Tuple<Key, Value>[] original, int length)
        {
            Contract.Requires(length >= 0);
            Contract.Ensures(Contract.Result<Tuple<Key, Value>[]>().Length >= length);

            if (original == null)
            {
                return new Tuple<Key, Value>[length];
            }

            Array.Resize(ref original, original.Length + length);

            return original;
        }

        public static string ToStringInDepth<T>(this T[] arr)
        {
            Contract.Requires(arr != null);
            Contract.Ensures(Contract.Result<string>() != null);

            var result = new StringBuilder();

            result.Append("[");
            for (var i = 0; i < arr.Length; i++)
            {
                var el = arr[i];
                result.Append((el == null ? "<null>" : el.ToString()) + " ");
            }
            result.Append("]");

            return result.ToString();
        }
    }

    [ContractVerification(true)]
    public static class DictionaryExtensions
    {
        public static bool IsIdentityMap<Variable>(this Dictionary<Variable, FList<Variable>> map)
        {
            if (map == null || map.Count == 0)
                return true;

            foreach (var pair in map)
            {
                Contract.Assume(pair.Value != null);
                if (pair.Value.Length() != 1)
                {
                    return false;
                }
                var val = pair.Value.Head;
                if (!pair.Key.Equals(val))
                {
                    return false;
                }
            }

            return true;
        }

        [ContractVerification(false)]
        public static void Add<T, V>(this Dictionary<T, List<V>> dictionary, T key, V value)
        {
            Contract.Requires(dictionary != null);

            List<V> list;
            if (!dictionary.TryGetValue(key, out list))
            {
                list = new List<V>();
            }
            else
            {
                Contract.Assume(list != null);
            }
            list.Add(value);
            dictionary[key] = list;
        }
    }

    [ContractVerification(true)]
    public static class ListExtensions
    {
        public static List<T> SetUnion<T>(this List<T> me, List<T> other)
        {
            Contract.Requires(me != null);
            Contract.Requires(other != null);

            Contract.Ensures(Contract.Result<List<T>>() != null);

            if (me.Count == 0)
                return other;

            if (other.Count == 0)
                return me;

            var tmp = new Set<T>(me);
            tmp.AddRange(other);

            return tmp.ToList();
        }

        public static List<T> SetIntersection<T>(this List<T> me, List<T> other)
        {
            Contract.Requires(me != null);
            Contract.Requires(other != null);

            Contract.Ensures(Contract.Result<List<T>>() != null);

            if (me.Count == 0)
            {
                return me;
            }

            if (other.Count == 0)
            {
                return other;
            }

            var tmp = new Set<T>(me);
            tmp = tmp.Intersection(other);

            return tmp.ToList();
        }

        public static List<T> SetDifference<T>(this List<T> me, List<T> other)
        {
            Contract.Requires(me != null);
            Contract.Requires(other != null);

            Contract.Ensures(Contract.Result<List<T>>() != null);

            if (me.Count == 0 || other.Count == 0)
            {
                return me;
            }

            var tmp = new Set<T>(me);
            tmp = tmp.Difference(new Set<T>(other));

            return tmp.ToList();
        }

        static public void Add<A, B>(this List<Pair<A, B>> list, A a, B b)
        {
            Contract.Requires(list != null);

            Contract.Ensures(list.Count == Contract.OldValue(list.Count) + 1);

            list.Add(new Pair<A, B>(a, b));
        }

        static public void AddIfNotNull<A, B>(this List<PairNonNull<A, B>> list, A a, B b)
          where A : class
          where B : class
        {
            Contract.Requires(list != null);

            Contract.Ensures(list.Count <= Contract.OldValue(list.Count) + 1);
            Contract.Ensures(list.Count >= Contract.OldValue(list.Count));

            if (a != null && b != null)
            {
                list.Add(new PairNonNull<A, B>(a, b));
            }
        }

        static public bool SearchAndRemoveIfFound<A, B>(this List<Pair<A, B>> values, A key, out B value)
        {
            Contract.Requires(values != null);

            for (int i = 0; i < values.Count; i++)
            {
                var currVal = values[i];
                if (currVal.One.Equals(key))
                {
                    value = currVal.Two;
                    values.RemoveAt(i);

                    return true;
                }
            }

            value = default(B);
            return false;
        }

        static public List<List<T>> DeepCopy<T>(this List<List<T>> elements)
        {
            Contract.Requires(elements != null);

            var result = new List<List<T>>(elements.Count);

            foreach (var bucket in elements)
            {
                result.Add(new List<T>(bucket));
            }

            return result;
        }
    }

    [ContractVerification(true)]
    public static class IExpressionDecoderExtensions
    {
        [Pure]
        public static bool IsInfinity<Variable, Expression>(this IExpressionDecoder<Variable, Expression> decoder, Expression exp)
        {
            Contract.Requires(decoder != null);
            Contract.Requires(exp != null);

            if (decoder.IsConstant(exp))
            {
                var value = decoder.Constant(exp);
                if (value is Double)
                {
                    return Double.IsInfinity((Double)value);
                }
                if (value is Single)
                {
                    return Single.IsInfinity((Single)value);
                }
            }

            return false;
        }

        [Pure]
        public static bool IsPositiveInfinity<Variable, Expression>(this IExpressionDecoder<Variable, Expression> decoder, Expression exp)
        {
            Contract.Requires(decoder != null);
            Contract.Requires(exp != null);

            if (decoder.IsConstant(exp))
            {
                var value = decoder.Constant(exp);
                if (value is Double)
                {
                    return Double.IsPositiveInfinity((Double)value);
                }
                if (value is Single)
                {
                    return Single.IsPositiveInfinity((Single)value);
                }
            }

            return false;
        }

        [Pure]
        public static bool IsNegativeInfinity<Variable, Expression>(this IExpressionDecoder<Variable, Expression> decoder, Expression exp)
        {
            Contract.Requires(decoder != null);
            Contract.Requires(exp != null);

            if (decoder.IsConstant(exp))
            {
                var value = decoder.Constant(exp);
                if (value is Double)
                {
                    return Double.IsNegativeInfinity((Double)value);
                }
                if (value is Single)
                {
                    return Single.IsNegativeInfinity((Single)value);
                }
            }

            return false;
        }

        [Pure]
        public static bool TryMatchVarPlusConst<Variable, Expression>(this IExpressionDecoder<Variable, Expression> decoder,
          Expression exp, out Variable var, out int k)
        {
            Contract.Requires(decoder != null);

            if (decoder.IsBinaryExpression(exp) && decoder.OperatorFor(exp) == ExpressionOperator.Addition)
            {
                var left = decoder.LeftExpressionFor(exp);
                var right = decoder.RightExpressionFor(exp);

                if (decoder.IsConstantInt(left, out k) && decoder.IsVariable(right))
                {
                    var = decoder.UnderlyingVariable(right);

                    return true;
                }

                if (decoder.IsConstantInt(right, out k) && decoder.IsVariable(left))
                {
                    var = decoder.UnderlyingVariable(left);

                    return true;
                }
            }

            var = default(Variable);
            k = default(int);
            return false;
        }

        public static bool IsAtomicExpression<Variable, Expression>(this IExpressionDecoder<Variable, Expression> decoder, Expression exp)
        {
            Contract.Requires(decoder != null);

            return decoder.IsConstant(exp) || decoder.IsVariable(exp);
        }

        /// <summary>
        /// See if one can match the expression "left == right" with the expression "(e11 op e12) == 0", where op is a relational operator
        /// </summary>
        public static bool Match_E1relopE2eq0<Variable, Expression>(
          this IExpressionDecoder<Variable, Expression> decoder,
          Expression e1, Expression e2,
          out ExpressionOperator op, out Expression e11, out Expression e12)
        {
            Contract.Requires(decoder != null);

            Int32 value;

            if (decoder.IsConstantInt(e2, out value) && value == 0)
            {
                op = decoder.OperatorFor(e1);
                if (op.IsRelationalOperator())
                {
                    e11 = decoder.LeftExpressionFor(e1);
                    e12 = decoder.RightExpressionFor(e1);
                    return true;
                }
            }

            op = default(ExpressionOperator);
            e11 = e12 = default(Expression);

            return false;
        }

        public static bool Match_E1aritopE2eq0<Variable, Expression>(
          this IExpressionDecoder<Variable, Expression> decoder,
          Expression e1, Expression e2,
          out ExpressionOperator op, out Expression e11, out Expression e12)
        {
            Contract.Requires(decoder != null);

            Int32 value;
            if (decoder.IsConstantInt(e2, out value) && value == 0)
            {
                op = decoder.OperatorFor(e1);
                if (op.Equals(ExpressionOperator.Addition) || op.Equals(ExpressionOperator.Subtraction))
                {
                    e11 = decoder.LeftExpressionFor(e1);
                    e12 = decoder.RightExpressionFor(e1);
                    return true;
                }
            }

            op = default(ExpressionOperator);
            e11 = e12 = default(Expression);

            return false;
        }
    }

    [ContractVerification(true)]
    public static class IExpressionEncoderExtensions
    {
        /// <summary>
        /// Construct left - right, or left iff right == 0
        /// </summary>
        public static Expression SmartSubtraction<Variable, Expression>(this IExpressionEncoder<Variable, Expression> encoder,
          Expression left, Expression right, IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Requires(decoder != null);
            Contract.Requires(encoder != null);

            int value;
            if (decoder.IsConstantInt(right, out value) && value == 0)
            {
                return left;
            }

            return encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction, left, right);
        }

        /// <summary>
        /// Construct left - right, or left iff right == 0
        /// </summary>
        public static Expression SmartSubtraction<Variable, Expression>(this IExpressionEncoder<Variable, Expression> encoder,
          Variable leftVar, Variable rightVar, IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Requires(decoder != null);
            Contract.Requires(encoder != null);

            var right = encoder.VariableFor(rightVar);
            var left = encoder.VariableFor(leftVar);

            int value;
            if (decoder.IsConstantInt(right, out value) && value == 0)
            {
                return left;
            }

            return encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction, left, right);
        }
    }

    [ContractVerification(true)]
    public static class NormalizedExpressionsExtensions
    {
        [Pure]
        public static string PrettyPrint<Variable>(this NormalizedExpression<Variable> exp, Func<Variable, string> variableNamePrettyPrinter)
        {
            Contract.Requires(exp != null);
            Contract.Requires(variableNamePrettyPrinter != null);

            Variable x;
            int k;
            if (exp.IsConstant(out k))
            {
                return k.ToString();
            }
            else if (exp.IsVariable(out x))
            {
                return variableNamePrettyPrinter(x);
            }
            else if (exp.IsAddition(out x, out k))
            {
                return variableNamePrettyPrinter(x) + " + " + k.ToString();
            }
            return null;
        }
    }

    [ContractVerification(true)]
    public static class ExpressionOperatorExtensions
    {
        private readonly static Set<ExpressionOperator> relationalOperators;

        static ExpressionOperatorExtensions()
        {
            relationalOperators = new Set<ExpressionOperator>{
          ExpressionOperator.Equal,
          ExpressionOperator.Equal_Obj,
          ExpressionOperator.GreaterEqualThan, ExpressionOperator.GreaterEqualThan_Un,
          ExpressionOperator.GreaterThan, ExpressionOperator.GreaterThan_Un,
          ExpressionOperator.LessEqualThan, ExpressionOperator.LessEqualThan_Un,
          ExpressionOperator.LessThan, ExpressionOperator.LessThan_Un,
          ExpressionOperator.NotEqual };
        }

        /// <returns>true iff <code>op</code> is a constant or a variable</returns>
        [Pure]
        public static bool IsZerary(this ExpressionOperator op)
        {
            Contract.Ensures(Contract.Result<bool>() == ((op == ExpressionOperator.Constant) || (op == ExpressionOperator.Variable)));

            return (op == ExpressionOperator.Constant) || (op == ExpressionOperator.Variable);
        }

        /// <returns>true iff <code>op</code> is an unary operator</returns>
        [Pure]
        public static bool IsUnary(this ExpressionOperator op)
        {
            Contract.Ensures(Contract.Result<bool>() == ((op == ExpressionOperator.UnaryMinus) || (op == ExpressionOperator.Not)));

            return (op == ExpressionOperator.UnaryMinus) || (op == ExpressionOperator.Not);
        }

        /// <returns>true iff op is a Binary operator</returns>
        [Pure]
        public static bool IsBinary(this ExpressionOperator op)
        {
            Contract.Ensures(Contract.Result<bool>() == (!IsUnary(op) && !IsZerary(op)));

            return !IsUnary(op) && !IsZerary(op);
        }

        /// <returns>true iff <code>is a relational operator</code></returns>
        [Pure]
        public static bool IsRelationalOperator(this ExpressionOperator op)
        {
            return relationalOperators.Contains(op);
        }

        /// <returns>true iff <code>e</code> is the constant true</returns>
        [Pure]
        public static bool IsConstantTrue<Variable, Expression>(Expression e, IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Requires(decoder != null);

            return IsConstantTrueOrFalse(e, true, decoder);
        }

        /// <returns>true iff <code>e</code> is the constant true</returns>
        [Pure]
        public static bool IsConstantFalse<Variable, Expression>(Expression e, IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Requires(decoder != null);

            return IsConstantTrueOrFalse(e, false, decoder);
        }

        [Pure]
        public static bool IsConversionOperator(this ExpressionOperator op)
        {
            return (op == ExpressionOperator.ConvertToInt32 || op == ExpressionOperator.ConvertToUInt8
              || op == ExpressionOperator.ConvertToUInt16 || op == ExpressionOperator.ConvertToUInt32
              || op == ExpressionOperator.ConvertToFloat32 || op == ExpressionOperator.ConvertToFloat64);
        }

        /// <returns>op == ExpressionOperator.LessThan || op == ExpressionOperator.LessThan_Un</returns>
        [Pure]
        public static bool IsLessThan(this ExpressionOperator op)
        {
            return op == ExpressionOperator.LessThan || op == ExpressionOperator.LessThan_Un;
        }

        /// <returns>op == ExpressionOperator.LessEqualThan || op == ExpressionOperator.LessEqualThan_Un</returns>
        [Pure]
        public static bool IsLessEqualThan(this ExpressionOperator op)
        {
            return op == ExpressionOperator.LessEqualThan || op == ExpressionOperator.LessEqualThan_Un;
        }

        /// <returns>op == ExpressionOperator.GreaterThan || op == ExpressionOperator.GreaterThan_Un</returns>
        [Pure]
        public static bool IsGreaterThan(this ExpressionOperator op)
        {
            return op == ExpressionOperator.GreaterThan || op == ExpressionOperator.GreaterThan_Un;
        }

        /// <returns>op == ExpressionOperator.GreaterEqualThan || op == ExpressionOperator.GreaterEqualThan_Un</returns>
        [Pure]
        public static bool IsGreaterEqualThan(this ExpressionOperator op)
        {
            return op == ExpressionOperator.GreaterEqualThan || op == ExpressionOperator.GreaterEqualThan_Un;
        }

        #region Private methods

        private static bool IsConstantTrueOrFalse<Variable, Expression>(
          Expression e, bool t, IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Requires(decoder != null);

            bool boolValue;
            Int32 intValue;

            if (decoder.IsConstant(e))
            {
                if (decoder.TryValueOf<bool>(e, ExpressionType.Bool, out boolValue))
                {
                    return boolValue == t;
                }
                else if (decoder.IsConstantInt(e, out intValue))
                {
                    return (intValue != 0) == t;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
    }

    [ContractVerification(true)]
    public static class SparseRationalArrayExtensions
    {
        [Pure]
        static public bool IsConstantRow(this SparseRationalArray row)
        {
            Contract.Requires(row != null);

            int count = row.Count;
            switch (count)
            {
                case 0:
                case 1:
                    return true;

                case 2:
                    Contract.Assume(row.Length >= 1); // There is an invariant on sparse arrays: Length >= Count
                    return row.IsIndexOfNonDefaultElement(row.Length - 1);

                default:
                    return false;
            }
        }
    }

    [ContractVerification(true)]
    public static class DoubleExtensions
    {
        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        public static Interval ConvertFromDouble(this Double d)
        {
            Contract.Ensures(Contract.Result<Interval>() != null);

            if (d.Equals(Double.NaN) || Double.IsInfinity(d) || d < Int64.MinValue || d > Int64.MaxValue)
            {
                return Interval.UnknownInterval;
            }


            var trunc = Math.Truncate(d);

            var l = (long)trunc;

            if (d == trunc)
            {
                return Interval.For(Rational.For(l, 1));
            }
            else
            {
                Interval candidate;
                if (d > 0)
                {
                    candidate = Interval.For(Rational.For(l, 1), Rational.For(l + 1, 1));
                }
                else
                {
                    candidate = Interval.For(Rational.For(l - 1, 1), Rational.For(l, 1));
                }

                // Check for conversion errors
                if (candidate.LowerBound.Ceiling > d || candidate.UpperBound.Floor < d)
                {
                    return Interval.UnknownInterval;
                }

                return candidate;
            }
        }

        public static bool IsNormal(this Double d)
        {
            return !Double.IsNaN(d) && !Double.IsInfinity(d);
        }
    }

    [ContractVerification(true)]
    public static class SingleExtensions
    {
        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        public static Interval ConvertFromDouble(this Single d)
        {
            Contract.Ensures(Contract.Result<Interval>() != null);

            if (d.Equals(Single.NaN) || d < Int64.MinValue || d > Int64.MaxValue)
            {
                return Interval.UnknownInterval;
            }

            var trunc = Math.Truncate(d);
            var l = (long)trunc;

            if (d == trunc)
            {
                return Interval.For(Rational.For(l, 1));
            }
            else
            {
                Interval candidate;
                if (d > 0)
                {
                    candidate = Interval.For(Rational.For(l, 1), Rational.For(l + 1, 1));
                }
                else
                {
                    candidate = Interval.For(Rational.For(l - 1, 1), Rational.For(l, 1));
                }

                // Check for conversion errors
                if (candidate.LowerBound.Ceiling > d || candidate.UpperBound.Floor < d)
                {
                    return Interval.UnknownInterval;
                }

                return candidate;
            }
        }

        public static bool IsNormal(this Single d)
        {
            return !Single.IsNaN(d) && !Single.IsInfinity(d);
        }
    }
}