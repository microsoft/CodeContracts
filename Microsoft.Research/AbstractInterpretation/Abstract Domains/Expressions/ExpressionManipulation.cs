// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// Few classes for helping manipulating expressions
// 

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Text;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Expressions
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// The default comparer for variables
    /// </summary>
    internal class ExpressionComparer<Variable> : IComparer<Variable>
    {
        /// <summary>
        /// Uses the <code>CompareTo</code> for strings to get the total order over strings
        /// </summary>
        public int Compare(Variable left, Variable right)
        {
            Contract.Assume(left != null); // cannot strengthen the interface contract
            Contract.Assume(right != null);

            return left.ToString().CompareTo(right.ToString());
        }
    }


    /// <summary>
    /// Pretty print the expressions
    /// </summary>
    public static class ExpressionPrinter
    {
        private const int MaxHeight = 16;
        private const string TRUE = "1";
        [ThreadStatic]
        private static bool ToStringInvokedFromTheOutside;

        public static string ToGeqZero<Variable, Expression>(Expression exp, IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            return "0 <= " + ToString(exp, decoder);
        }

        public static string ToLessThan<Variable, Expression>(Expression left, Expression right, IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            return ToString(left, decoder) + " < " + ToString(right, decoder);
        }

        /// <returns>A string representation of the expression exp</returns>
        public static string ToString<Variable, Expression>(Expression exp, IExpressionDecoder<Variable, Expression> decoder)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            Contract.Assume(!ToStringInvokedFromTheOutside); // This method can be invoked just from the outside of the class

            ToStringInvokedFromTheOutside = true;

            var result = ToString(exp, decoder, 0);

            ToStringInvokedFromTheOutside = false;

            return result;
        }

        /// <returns>A string representation of the expression exp</returns>
        public static string ToString<Variable, Expression>(Variable exp, IExpressionDecoder<Variable, Expression> decoder)
        {
            return decoder.NameOf(exp);
        }

        /// <returns>
        /// A (textual representation of the) C# operator for <code>op</code>
        /// </returns>
        public static string ToString(ExpressionOperator op)
        {
            switch (op)
            {
                case ExpressionOperator.Addition:
                    return "+";
                case ExpressionOperator.And:
                    return "&";
                case ExpressionOperator.Constant:
                    return "<CONST>";
                case ExpressionOperator.ConvertToInt32:
                    return "(Int32)";
                case ExpressionOperator.ConvertToUInt16:
                    return "(UInt16)";
                case ExpressionOperator.ConvertToUInt32:
                    return "(UInt32)";
                case ExpressionOperator.ConvertToUInt8:
                    return "(UInt8)";
                case ExpressionOperator.ConvertToFloat32:
                    return "(Single)";
                case ExpressionOperator.ConvertToFloat64:
                    return "(Double)";
                case ExpressionOperator.Division:
                    return "/";
                case ExpressionOperator.Equal:
                    return "==";
                case ExpressionOperator.Equal_Obj:
                    return "=Equals=";
                case ExpressionOperator.GreaterEqualThan:
                    return ">=";
                case ExpressionOperator.GreaterEqualThan_Un:
                    return ">=_un";
                case ExpressionOperator.GreaterThan:
                    return ">";
                case ExpressionOperator.GreaterThan_Un:
                    return ">_un";
                case ExpressionOperator.LessEqualThan:
                    return "<=";
                case ExpressionOperator.LessEqualThan_Un:
                    return "<=_un";
                case ExpressionOperator.LessThan:
                    return "<";
                case ExpressionOperator.LessThan_Un:
                    return "<_un";
                case ExpressionOperator.LogicalAnd:
                    return "&&";
                case ExpressionOperator.LogicalNot:
                    return "!";
                case ExpressionOperator.LogicalOr:
                    return "||";
                case ExpressionOperator.Modulus:
                    return "%";
                case ExpressionOperator.Multiplication:
                    return "*";
                case ExpressionOperator.Not:
                    return "!";
                case ExpressionOperator.NotEqual:
                    return "!=";
                case ExpressionOperator.Or:
                    return "|";
                case ExpressionOperator.Xor:
                    return "^";
                case ExpressionOperator.ShiftLeft:
                    return "<<";
                case ExpressionOperator.ShiftRight:
                    return ">>";
                case ExpressionOperator.SizeOf:
                    return "sizeof";
                case ExpressionOperator.Subtraction:
                    return "-";
                case ExpressionOperator.UnaryMinus:
                    return "-";
                case ExpressionOperator.Unknown:
                    return "<???>";
                case ExpressionOperator.Variable:
                    return "<VAR>";
                case ExpressionOperator.WritableBytes:
                    return "<WritableBytes>";
                default:
                    return "<????>";
            }
        }

        private static string ToString<Variable, Expression>(Expression exp, IExpressionDecoder<Variable, Expression> decoder, int height)
        {
            Contract.Requires(decoder != null);
            Contract.Requires(height >= 0);
            Contract.Ensures(Contract.Result<string>() != null);

            if (height > MaxHeight)
            {
                return "<too deep in the exp>";
            }
            else
            {
                switch (decoder.OperatorFor(exp))
                {
                    #region All the cases...
                    case ExpressionOperator.Addition:
                        return BinaryPrint("+", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.And:
                        return BinaryPrint("&", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Constant:
                        return ConstantPrint(exp, decoder, height + 1);
                    case ExpressionOperator.ConvertToInt32:
                        return UnaryPrint("(int32)", decoder.LeftExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.ConvertToUInt8:
                        return UnaryPrint("(uint8)", decoder.LeftExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.ConvertToUInt16:
                        return UnaryPrint("(uint16)", decoder.LeftExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.ConvertToUInt32:
                        return UnaryPrint("(uint32)", decoder.LeftExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.ConvertToFloat32:
                        return UnaryPrint("(Single)", decoder.LeftExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.ConvertToFloat64:
                        return UnaryPrint("(Double)", decoder.LeftExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Division:
                        return BinaryPrint("/", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Equal:
                        return BinaryPrint("==", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Equal_Obj:
                        return BinaryPrint("=Equals=", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.GreaterEqualThan:
                        return BinaryPrint(">=", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.GreaterEqualThan_Un:
                        return BinaryPrint(">=_un", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.GreaterThan:
                        return BinaryPrint(">", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.GreaterThan_Un:
                        return BinaryPrint(">_un", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.LessEqualThan:
                        return BinaryPrint("<=", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.LessEqualThan_Un:
                        return BinaryPrint("<=_un", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.LessThan:
                        return BinaryPrint("<", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.LessThan_Un:
                        return BinaryPrint("<_un", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.LogicalAnd:
                        return BinaryPrint("&&", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.LogicalNot:
                        return UnaryPrint("!", decoder.LeftExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.LogicalOr:
                        return BinaryPrint("||", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Modulus:
                        return BinaryPrint("%", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Multiplication:
                        return BinaryPrint("*", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Not:
                        return UnaryPrint("!", decoder.LeftExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.NotEqual:
                        return BinaryPrint("!=", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Or:
                        return BinaryPrint("|", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Xor:
                        return BinaryPrint("^", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.ShiftLeft:
                        return BinaryPrint("<<", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.ShiftRight:
                        return BinaryPrint(">>", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.SizeOf:
                        return SizeOfPrint(exp, decoder, height + 1);
                    case ExpressionOperator.Subtraction:
                        return BinaryPrint("-", decoder.LeftExpressionFor(exp), decoder.RightExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.UnaryMinus:
                        return UnaryPrint("-", decoder.LeftExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Variable:
                        return VariablePrint(decoder.UnderlyingVariable(exp), decoder, height + 1);
                    case ExpressionOperator.WritableBytes:
                        return exp.ToString();
                    // return UnaryPrint("WritableBytes", decoder.LeftExpressionFor(exp), decoder, height + 1);
                    case ExpressionOperator.Unknown:
                        return "<Unknown expression>";
                    default:
                        throw new AbstractInterpretationException("Error, unknown case!!!!" + decoder.OperatorFor(exp));
                        #endregion
                }
            }
        }

        private static string UnaryPrint<Variable, Expression>(string p, Expression p_2, IExpressionDecoder<Variable, Expression> decoder, int height)
        {
            Contract.Requires(p != null);
            Contract.Ensures(Contract.Result<string>() != null);

            string s = ToString(p_2, decoder, height + 1);
            return p + "( " + s + " )";
        }

        private static string SizeOfPrint<Variable, Expression>(Expression sizeOfExp, IExpressionDecoder<Variable, Expression> decoder, int height)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            int size;
            if (decoder.TrySizeOf(sizeOfExp, out size))
            {
                return size.ToString() + "(sizeof)";
            }
            else return "sizeof( ? )";
        }

        private static string VariablePrint<Variable, Expression>(Variable var, IExpressionDecoder<Variable, Expression> decoder, int height)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            return decoder.NameOf(var);
        }

        private static string ConstantPrint<Variable, Expression>(Expression exp, IExpressionDecoder<Variable, Expression> decoder, int height)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            var tmp = new VisitorForConstantPrint<Variable, Expression>(decoder).Visit(exp, Unit.Value);

            Contract.Assume(tmp != null); // F: to shut off Clousot for now

            return tmp;
        }

        private static string BinaryPrint<Variable, Expression>(string p, Expression p_2, Expression p_3, IExpressionDecoder<Variable, Expression> decoder, int height)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            string s1 = ToString(p_2, decoder, height + 1);
            string s2 = ToString(p_3, decoder, height + 1);

            return p + "( " + s1 + ", " + s2 + " )";
        }

        private class VisitorForConstantPrint<Variable, Expression>
          : GenericTypeExpressionVisitor<Variable, Expression, Unit, string>
        {
            private const string FAILED = "<cannot convert the value to a string>";

            public VisitorForConstantPrint(IExpressionDecoder<Variable, Expression> decoder)
              : base(decoder)
            {
            }

            public override string Default(Expression exp)
            {
                return exp.ToString();
            }

            public override string VisitBool(Expression exp, Unit input)
            {
                Boolean value;
                return Decoder.TryValueOf<Boolean>(exp, ExpressionType.Bool, out value) ? value.ToString() : FAILED;
            }

            public override string VisitInt8(Expression exp, Unit input)
            {
                SByte value;
                return Decoder.TryValueOf<SByte>(exp, ExpressionType.Int8, out value) ? value.ToString() : FAILED;
            }

            public override string VisitFloat32(Expression exp, Unit input)
            {
                Single value;
                return Decoder.TryValueOf<Single>(exp, ExpressionType.Float32, out value) ? value.ToString() : FAILED;
            }

            public override string VisitFloat64(Expression exp, Unit input)
            {
                Double value;
                return Decoder.TryValueOf<Double>(exp, ExpressionType.Float64, out value) ? value.ToString() : FAILED;
            }

            public override string VisitInt16(Expression exp, Unit input)
            {
                Int16 value;
                return Decoder.TryValueOf<Int16>(exp, ExpressionType.Int16, out value) ? value.ToString() : FAILED;
            }

            public override string VisitInt32(Expression exp, Unit input)
            {
                Int32 value;
                return Decoder.TryValueOf<Int32>(exp, ExpressionType.Int32, out value) ? value.ToString() : FAILED;
            }

            public override string VisitInt64(Expression exp, Unit input)
            {
                Int64 value;
                return Decoder.TryValueOf<Int64>(exp, ExpressionType.Int64, out value) ? value.ToString() : FAILED;
            }

            public override string VisitString(Expression exp, Unit input)
            {
                String value;
                return Decoder.TryValueOf<String>(exp, ExpressionType.String, out value) ? "\"" + value + "\"" : FAILED;
            }

            public override string VisitUInt8(Expression exp, Unit input)
            {
                Byte value;
                return Decoder.TryValueOf<Byte>(exp, ExpressionType.UInt8, out value) ? value.ToString() : FAILED;
            }

            public override string VisitUInt16(Expression exp, Unit input)
            {
                UInt16 value;
                return Decoder.TryValueOf<UInt16>(exp, ExpressionType.UInt16, out value) ? value.ToString() : FAILED;
            }

            public override string VisitUInt32(Expression exp, Unit input)
            {
                UInt32 value;
                return Decoder.TryValueOf<UInt32>(exp, ExpressionType.UInt32, out value) ? value.ToString() : FAILED;
            }
        }

        /// <returns>
        /// a string representing the conjunction of this value
        /// </returns>
        internal static string ToLogicalFormula(ExpressionOperator op, List<string> list)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            if (list.Count == 0)
            {
                return TRUE;
            }
            else if (list.Count == 1)
            {
                if (list[0].EndsWith("-> 1"))
                    return list[0];
                else
                    return string.Format("{0} -> {1}", list[0], TRUE);
            }
            else if (list.Count == 2)
            {
                return ToStringBinary(op, list[0], list[1]);
            }
            else
            {
                int halfList = list.Count / 2;
                string s1 = ToLogicalFormula(op, list.GetRange(0, halfList - 1));
                string s2 = ToLogicalFormula(op, list.GetRange(halfList, list.Count - halfList));

                return ToStringBinary(op, s1, s2);
            }
        }

        internal static string ToStringBinary(ExpressionOperator expressionOperator, string left, string right)
        {
            if (expressionOperator != ExpressionOperator.And)
            {
                return string.Format("{0}({1}, {2})", ConvertToWords(expressionOperator), left, right);
            }
            else // Simplify the and to make Aprove happier
            {
                if (left.Equals(TRUE))
                    return right;
                if (right.Equals(TRUE))
                    return left;
                return string.Format("{0}, {1}", left, right, TRUE);
            }
        }

        private static string ConvertToWords(ExpressionOperator op)
        {
            switch (op)
            {
                case ExpressionOperator.Addition:
                    return "Add";
                case ExpressionOperator.And:
                    return "And";
                case ExpressionOperator.Constant:
                    return "Constant";
                case ExpressionOperator.ConvertToInt32:
                    return "Int32";
                case ExpressionOperator.ConvertToFloat32:
                    return "Single";
                case ExpressionOperator.ConvertToFloat64:
                    return "Double";
                case ExpressionOperator.ConvertToUInt16:
                    return "UInt16";
                case ExpressionOperator.ConvertToUInt32:
                    return "UInt32";
                case ExpressionOperator.ConvertToUInt8:
                    return "UInt8";
                case ExpressionOperator.Division:
                    return "Div";
                case ExpressionOperator.Equal:
                    return "Eq";
                case ExpressionOperator.Equal_Obj:
                    return "Eq_obj";
                case ExpressionOperator.GreaterEqualThan:
                    return "Cge";
                case ExpressionOperator.GreaterEqualThan_Un:
                    return "Cge_un";
                case ExpressionOperator.GreaterThan:
                    return "Cgt";
                case ExpressionOperator.GreaterThan_Un:
                    return "Cgt_un";
                case ExpressionOperator.LessEqualThan:
                    return "Cle";
                case ExpressionOperator.LessEqualThan_Un:
                    return "Cle_un";
                case ExpressionOperator.LessThan:
                    return "Clt";
                case ExpressionOperator.LessThan_Un:
                    return "Clt_un";
                case ExpressionOperator.LogicalAnd:
                    return "And";
                case ExpressionOperator.LogicalNot:
                    return "Not";
                case ExpressionOperator.LogicalOr:
                    return "Or";
                case ExpressionOperator.Modulus:
                    return "Rem";
                case ExpressionOperator.Multiplication:
                    return "Mul";
                case ExpressionOperator.Not:
                    return "Not";
                case ExpressionOperator.NotEqual:
                    return "Neq";
                case ExpressionOperator.Or:
                    return "Or";
                case ExpressionOperator.Xor:
                    return "Xor";
                case ExpressionOperator.ShiftLeft:
                    return "Shl";
                case ExpressionOperator.ShiftRight:
                    return "Shr";
                case ExpressionOperator.SizeOf:
                    return "Sizeof";
                case ExpressionOperator.Subtraction:
                    return "Sub";
                case ExpressionOperator.UnaryMinus:
                    return "Minus";
                case ExpressionOperator.Unknown:
                    return "???";
                case ExpressionOperator.Variable:
                    return "Var";
                case ExpressionOperator.WritableBytes:
                    return "WB";
                default:
                    return "???";
            }
        }
    }

    /// <summary>
    /// Convert an expression <code>Expression</code> into a Polynomial, and back
    /// </summary>
    public static class ArithmeticExpressionsConverter
    {
    }
}