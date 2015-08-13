// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    [ContractVerification(false)]
    public class BoxedExpressionsToSMT2 : BoxedExpressionTransformer<SMTFunctionDefinitions>
    {
        private const string FLOAT32Bits = "11 53"; // to change!!!
        private const string FLOAT64Bits = "11 53";

        private const string FLOAT32 = "(_ FP 11 53)"; // To change!!!
        private const string FLOAT64 = "(_ FP 11 53)";

        private const string INT = "Int"; // To change !!!
        private const string DEFAULTROUNDING = "roundTowardZero"; // .NET has some default rounding

        public string ResultValue { get; private set; }
        readonly private Func<Object, Type> VariableType;

        private enum FloatType { Float32, Float64, Other };

        private Dictionary<BoxedExpression, FloatType> types;

        public BoxedExpressionsToSMT2(Func<object, Type> VariableType)
        {
            Contract.Requires(VariableType != null);

            this.ResultValue = null;
            this.VariableType = VariableType;
        }

        public override BoxedExpression Visit(BoxedExpression exp, SMTFunctionDefinitions info, Func<object, DataStructures.FList<PathElement>> PathFetcher = null)
        {
            this.ResultValue = null;
            types = new Dictionary<BoxedExpression, FloatType>();

            return base.Visit(exp, info, PathFetcher);
        }

        protected override BoxedExpression Binary(BoxedExpression original, BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right)
        {
            var tmp = this.Visit(left);
            if (tmp != null)
            {
                var l = this.ResultValue;
                Contract.Assume(l != null);
                tmp = this.Visit(right);
                if (tmp != null)
                {
                    var r = this.ResultValue;
                    Contract.Assume(r != null);

                    FloatType typeOp;
                    if (!(types.TryGetValue(left, out typeOp) && typeOp != FloatType.Other) && !types.TryGetValue(right, out typeOp))
                    {
                        typeOp = FloatType.Other;
                    }
                    this.ResultValue = Combine(binaryOperator, typeOp, l, r);

                    if (this.ResultValue != null)
                    {
                        types[original] = typeOp;

                        return original;
                    }
                }
            }

            this.ResultValue = null;
            return null;
        }

        protected override BoxedExpression Unary(BoxedExpression original, UnaryOperator unaryOperator, BoxedExpression argument)
        {
            var tmp = this.Visit(argument);
            if (tmp != null)
            {
                var arg = this.ResultValue;
                this.ResultValue = Combine(unaryOperator, arg);
                if (this.ResultValue != null)
                {
                    return original;
                }
            }

            this.ResultValue = null;
            return null;
        }

        protected override BoxedExpression Variable(BoxedExpression original, object var, PathElement[] path)
        {
            bool isDouble, isPlusInfinity, isMinusInfinity;
            var type = VariableType(var);
            if (type != null)
            {
                if (type.Equals(typeof(System.Single)))
                {
                    return DeclareVariable(original, FLOAT32); // TODO: change it
                }
                if (type.Equals(typeof(System.Double)))
                {
                    return DeclareVariable(original, FLOAT64);
                }
                if (type.Equals(typeof(System.Int32)))
                {
                    return DeclareVariable(original, INT);
                }
            }
            // IsNaN
            else if (path.IsNaNCall(out isDouble))
            {
                return NaNCheck(original, path, isDouble);
            }
            // IsInfinity, IsPlusInfinity, IsMinusInfinity
            else if (path.IsInfinityCall(out isDouble, out isPlusInfinity, out isMinusInfinity))
            {
                return InfinityCheck(original, path, isDouble, isPlusInfinity, isMinusInfinity);
            }
            this.ResultValue = null;
            return null;
        }


        protected override BoxedExpression Constant(BoxedExpression original, object type, object value)
        {
            if (value is System.Single)
            {
                types[original] = FloatType.Float32;
                this.ResultValue = string.Format("((_ asFloat {0}) {1} {2:0.0})", FLOAT32Bits, DEFAULTROUNDING, value);
            }
            else if (value is System.Double)
            {
                types[original] = FloatType.Float64;
                this.ResultValue = string.Format("((_ asFloat {0}) {1} {2:0.0})", FLOAT64Bits, DEFAULTROUNDING, value);
            }
            else
            {
                this.ResultValue = value.ToString();
            }
            return original;
        }

        protected override BoxedExpression Null(BoxedExpression original)
        {
            return this.Constant(original, original.ConstantType, original.Constant);
        }

        #region TODO

        protected override BoxedExpression Exists(BoxedExpression original, BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body)
        {
            this.ResultValue = null;
            return null;
        }

        protected override BoxedExpression IsInst(BoxedExpression original, object type, BoxedExpression argument)
        {
            this.ResultValue = null;
            return null;
        }

        protected override BoxedExpression ForAll(BoxedExpression original, BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body)
        {
            this.ResultValue = null;
            return null;
        }

        protected override BoxedExpression ArrayIndex<Typ>(BoxedExpression original, Typ type, BoxedExpression array, BoxedExpression index)
        {
            this.ResultValue = null;
            return null; // TODO
        }

        protected override BoxedExpression Result(BoxedExpression original)
        {
            this.ResultValue = null;
            return null;
        }

        protected override BoxedExpression SizeOf(BoxedExpression original)
        {
            this.ResultValue = null;
            return null;
        }

        #endregion

        #region privates

        private BoxedExpression DeclareVariable(BoxedExpression original, string type)
        {
            var tmp = original.ToString().Replace(' ', '_');
            this.Info.AddDeclaration(string.Format("(declare-fun {0} () {1})", tmp, type));
            this.ResultValue = tmp;

            if (type == FLOAT32)
            {
                types[original] = FloatType.Float32;
            }
            else if (type == FLOAT64)
            {
                types[original] = FloatType.Float64;
            }

            return original;
        }

        private BoxedExpression NaNCheck(BoxedExpression original, PathElement[] path, bool isDouble)
        {
            Contract.Requires(path.Length == 2);

            var arg = path[0].ToString();
            this.Info.AddDeclaration(string.Format("(declare-fun {0} () {1})", arg, isDouble ? FLOAT64 : FLOAT32));
            this.ResultValue = string.Format("(= {0} {1})", arg, GetNaN(isDouble)); // emitting real equality "=" instead of FPA equality "==" which returns false when one of the arguments is NaN

            return original;
        }

        private BoxedExpression InfinityCheck(BoxedExpression original, PathElement[] path, bool isDouble, bool isPlusInfinity, bool isMinusInfinity)
        {
            Contract.Requires(path.Length == 2);
            Contract.Requires(isPlusInfinity || isMinusInfinity);

            var arg = path[0].ToString();
            this.Info.AddDeclaration(string.Format("(declare-fun {0} () {1})", arg, isDouble ? FLOAT64 : FLOAT32));

            string plusInfinity = null;
            string minusInfinity = null;

            if (isPlusInfinity)
            {
                plusInfinity = string.Format("(= {0} {1})", arg, GetInfinity(isDouble, true));
            }
            if (isMinusInfinity)
            {
                minusInfinity = string.Format("(= {0} {1})", arg, GetInfinity(isDouble, false));
            }

            if (isPlusInfinity && isMinusInfinity)
            {
                this.ResultValue = string.Format("(or {0} {1})", plusInfinity, minusInfinity);
            }
            else if (isPlusInfinity)
            {
                this.ResultValue = plusInfinity;
            }
            else
            {
                this.ResultValue = minusInfinity;
            }

            return original;
        }


        private string GetNaN(bool IsDouble)
        {
            return string.Format("(as NaN {0})", IsDouble ? FLOAT64 : FLOAT32);
        }

        private string GetInfinity(bool IsDouble, bool isPlusInfinity)
        {
            return string.Format("(as {0} {1})",
              isPlusInfinity ? "plusInfinity" : "minusInfinity",
              IsDouble ? FLOAT64 : FLOAT32);
        }

        private string Combine(BinaryOperator binaryOperator, FloatType type, string left, string right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            var format = "({0} {1} {2} {3})";

            string bop = null;
            string bopType = null;

            switch (type)
            {
                case FloatType.Float32:
                case FloatType.Float64:
                    {
                        bopType = DEFAULTROUNDING;
                    }
                    break;

                default:
                    {
                        bopType = "";
                    }
                    break;
            }

            switch (binaryOperator)
            {
                case BinaryOperator.Add:
                case BinaryOperator.Add_Ovf:
                case BinaryOperator.Add_Ovf_Un:
                    {
                        bop = "+";
                    }
                    break;

                case BinaryOperator.And:
                    {
                        bopType = null;
                        bop = "&";
                    }
                    break;

                case BinaryOperator.Ceq:
                case BinaryOperator.Cobjeq:
                    {
                        bopType = null;
                        bop = "=";
                    }
                    break;

                case BinaryOperator.Cge:
                case BinaryOperator.Cge_Un:
                    {
                        bopType = null;

                        bop = ">=";
                    }
                    break;

                case BinaryOperator.Cgt:
                case BinaryOperator.Cgt_Un:
                    {
                        bopType = null;

                        bop = ">";
                    }
                    break;

                case BinaryOperator.Cle:
                case BinaryOperator.Cle_Un:
                    {
                        bopType = null;
                        bop = "<=";
                    }
                    break;

                case BinaryOperator.Clt:
                case BinaryOperator.Clt_Un:
                    {
                        bopType = null;
                        bop = "<";
                    }
                    break;

                case BinaryOperator.Cne_Un:
                    {
                        bopType = null;
                        bop = "!=";
                    }
                    break;

                case BinaryOperator.Div:
                case BinaryOperator.Div_Un:
                    {
                        bop = "/";
                    }
                    break;

                case BinaryOperator.LogicalAnd:
                    {
                        bopType = null;
                        bop = "and";
                    }
                    break;

                case BinaryOperator.LogicalOr:
                    {
                        bopType = null;
                        bop = "or";
                    }
                    break;

                case BinaryOperator.Mul:
                case BinaryOperator.Mul_Ovf:
                case BinaryOperator.Mul_Ovf_Un:
                    {
                        bop = "*";
                    }
                    break;

                case BinaryOperator.Or:
                    {
                        bopType = null;
                        bop = "|";
                    }
                    break;


                case BinaryOperator.Rem:
                case BinaryOperator.Rem_Un:
                    {
                        bop = "%";
                    }
                    break;

                case BinaryOperator.Sub:
                case BinaryOperator.Sub_Ovf:
                case BinaryOperator.Sub_Ovf_Un:
                    {
                        bop = "-";
                    }
                    break;

                case BinaryOperator.Xor:
                    {
                        bopType = null;
                        bop = "^";
                    }
                    break;

                case BinaryOperator.Shl:
                case BinaryOperator.Shr:
                case BinaryOperator.Shr_Un:
                // TODO!!!!

                default:
                    {
                        bopType = null;

                        return null;
                    }
            }

            Contract.Assert(bop != null);


            return string.Format(format, bop, bopType, left, right);
        }


        private string Combine(UnaryOperator unaryOperator, string arg)
        {
            Contract.Requires(arg != null);

            var format = "({0} {1})";
            string op = null;

            switch (unaryOperator)
            {
                case UnaryOperator.Neg:
                case UnaryOperator.Not:
                    {
                        op = "not";
                    }
                    break;

                case UnaryOperator.WritableBytes:
                case UnaryOperator.Conv_i:
                case UnaryOperator.Conv_i1:
                case UnaryOperator.Conv_i2:
                case UnaryOperator.Conv_i4:
                case UnaryOperator.Conv_i8:
                case UnaryOperator.Conv_r_un:
                case UnaryOperator.Conv_r4:
                case UnaryOperator.Conv_r8:
                case UnaryOperator.Conv_u:
                case UnaryOperator.Conv_u1:
                case UnaryOperator.Conv_u2:
                case UnaryOperator.Conv_u4:
                case UnaryOperator.Conv_u8:
                    {
                        return null;
                    }
            }

            return string.Format(format, op, arg);
        }

        #endregion
    }

    public class SMTFunctionDefinitions
    {
        readonly private List<string> declarations;

        public SMTFunctionDefinitions()
        {
            declarations = new List<string>();
        }

        public void AddDeclaration(string s)
        {
            Contract.Requires(s != null);

            declarations.Add(s);
        }

        public IEnumerable<string> Declarations
        {
            get
            {
                return declarations.Distinct();
            }
        }
    }
}
