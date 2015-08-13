// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Generics = System.Collections.Generic;
using Microsoft.Research.DataStructures;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Research.CodeAnalysis;
using System.IO;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    using Provenance = IEnumerable<ProofObligation>;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Used to associated other info with an expression
    /// </summary>
    public enum AssociatedInfo
    {
        /// <summary>
        /// Writable byte extent for pointers
        /// </summary>
        WritableBytes,

        /// <summary>
        /// Element count of an array
        /// </summary>
        ArrayLength
    }

    /// <summary>
    /// An expression is either
    ///   IsNull
    ///   IsVariable
    ///   IsConstant
    ///   IsSizeOf
    ///   IsUnary
    ///   IsBinary
    ///   IsIsInst
    ///
    /// In each case, further access to data stored in that case is possible.
    /// </summary>
    [ContractClass(typeof(BoxedExpressionContracts))]
    [Serializable]
    public abstract class BoxedExpression
    {
        #region Protected fields
        protected const int HashCodeUnSet = 0x5f5f5f5f;
        private int hashCodeCache = HashCodeUnSet;

        protected abstract int ComputeHashCode();

        #endregion

        #region Constants

        public const int MaxDepthInConverting = Int32.MaxValue;

        public const int DefaultDepthInConverting = 16; // some small number

        #endregion

        #region Statics 

        // We use True/False too often, and creating them each time requires bringing around the metadatadecoder, and hence all the templates..
        [ThreadStatic]
        private static BoxedExpression constTrue, constFalse;

        public static BoxedExpression ConstTrue
        {
            get
            {
                return constTrue;
            }
            set
            {
                constTrue = value;
            }
        }

        public static BoxedExpression ConstFalse
        {
            get
            {
                return constFalse;
            }
            set
            {
                constFalse = value;
            }
        }

        #endregion

        #region Factory methods

        private static IEqualityComparer<BoxedExpression> equalityComparerCache; // Thread-safe
        public static IEqualityComparer<BoxedExpression> EqualityComparer
        {
            get
            {
                Contract.Ensures(Contract.Result<IEqualityComparer<BoxedExpression>>() != null);

                if (equalityComparerCache == null)
                {
                    equalityComparerCache = new BoxedExpressionsEqualityComparer();
                }
                return equalityComparerCache;
            }
        }

        private static IEqualityComparer<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> equalityPairComparerCache; // Thread-safe
        public static IEqualityComparer<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> EqualityPairComparer
        {
            get
            {
                Contract.Ensures(Contract.Result<IEqualityComparer<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>>>() != null);

                if (equalityPairComparerCache == null)
                {
                    equalityPairComparerCache = new BoxedExpressionKeyEqualityComparer();
                }
                return equalityPairComparerCache;
            }
        }


        /// <summary>
        /// Create an External box, i.e., a subtree holding on to an ExternalExpression but that can be viewed as
        /// an ordinary Expression
        /// </summary>
        public static BoxedExpression For<Type, Variable, Expression>(Expression external, IFullExpressionDecoder<Type, Variable, Expression> decoder)
          where Expression : IEquatable<Expression>
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.ExternalBox<Variable, Expression>(external, decoder);
        }

        /// <summary>
        /// Create an ordinary Expression from the given external expression by converting it completely.
        /// </summary>
        /// <returns>It may return null, if the expression is too big!</returns>
        public static BoxedExpression Convert<Type, V, T>(T exp, IFullExpressionDecoder<Type, V, T> decoder,
          int MAXDEPTH = DefaultDepthInConverting, bool trySimplify = true, bool replaceConstants = true, bool replaceNull = true,
          Func<V, FList<PathElement>> accessPaths = null)
          where T : IEquatable<T>
        {
            BoxedExpression result;

            if (ClousotExpression<Type>.TryConvert(exp, decoder, MAXDEPTH, trySimplify, replaceConstants, replaceNull, accessPaths, out result))
            {
                return result;
            }
            return null;
        }

        public static BoxedExpression Var(object var)
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new VariableExpression(var);
        }

        public static BoxedExpression Var(object var, FList<PathElement> path)
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new VariableExpression(var, path);
        }


        public static BoxedExpression Var(object var, PathElement[] path)
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new VariableExpression(var, path);
        }

        public static BoxedExpression Var(object var, FList<PathElement> path, object type)
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new VariableExpression(var, path, type);
        }

        public static BoxedExpression VarBoundedInQuantifier(string name)
        {
            Contract.Requires(name != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new VariableExpression(name, true);
        }

        public static BoxedExpression VarCast<Type>(object var, PathElement[] path, Type type, Type CastTo)
          where Type : IEquatable<Type>
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.CastExpression(new VariableExpression(var, path, (object)type), null, CastTo, false);
        }

        public static BoxedExpression VarCastUnchecked<Type>(object var, PathElement[] path, Type type, Type CastTo)
          where Type : IEquatable<Type>
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.CastExpression(new VariableExpression(var, path, (object)type), null, CastTo, true);
        }

        public static BoxedExpression Binary(BinaryOperator op, BoxedExpression left, BoxedExpression right, object frameworkVar = null)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new BinaryExpression(op, left, right, frameworkVar);
        }

        public static BoxedExpression BinaryLogicalAnd(BoxedExpression left, BoxedExpression right)
        {
            if (left == null || right == null)
                return null;

            if (Object.ReferenceEquals(left, right))
                return left;

            int k;
            if (left.IsConstantInt(out k))
            {
                return k == 0 ? left : right;
            }
            if (right.IsConstantInt(out k))
            {
                return k == 0 ? right : left;
            }

            return BoxedExpression.Binary(BinaryOperator.LogicalAnd, left, right);
        }

        public static BoxedExpression BinaryLogicalOr(BoxedExpression left, BoxedExpression right)
        {
            if (left == null || right == null)
                return null;

            if (Object.ReferenceEquals(left, right))
                return left;

            int k;
            if (left.IsConstantInt(out k))
            {
                return k != 0 ? left : right;
            }
            if (right.IsConstantInt(out k) && k == 0)
            {
                return k != 0 ? right : left;
            }

            // F: Should use LogicalOr
            return BoxedExpression.Binary(BinaryOperator.LogicalOr, left, right);
        }

        public static BoxedExpression BinaryCast<Type>(BinaryOperator op, BoxedExpression left, BoxedExpression right, Type CastTo)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.CastExpression(new BinaryExpression(op, left, right), null, CastTo, false);
        }

        public static BoxedExpression BinaryMethodToCall(BinaryOperator op, BoxedExpression left, BoxedExpression right, string method_to_call)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new BinaryExpressionMethodCall(op, left, right, method_to_call);
        }

        public static BoxedExpression Unary(UnaryOperator op, BoxedExpression arg)
        {
            Contract.Requires(arg != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            // normalize negations

            BinaryOperator bop;
            BoxedExpression left, right;
            if (arg.IsBinaryExpression(out bop, out left, out right))
            {
                switch (bop)
                {
                    case BinaryOperator.Ceq:
                    case BinaryOperator.Cobjeq:
                        return new BinaryExpression(BinaryOperator.Cne_Un, left, right);
                    case BinaryOperator.Cne_Un:
                        return new BinaryExpression(BinaryOperator.Ceq, left, right);
                    default:
                        break;
                }
            }
            return new UnaryExpression(op, arg, null);
        }

        public static BoxedExpression UnaryLogicalNot(BoxedExpression arg)
        {
            Contract.Requires(arg != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new UnaryExpression(UnaryOperator.Not, arg, null);
        }

        public static BoxedExpression UnaryCast<Type>(UnaryOperator op, BoxedExpression arg, Type CastTo)
        {
            Contract.Requires(arg != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.CastExpression(new UnaryExpression(op, arg, null), null, CastTo, false);
        }

        public static BoxedExpression UnaryCastUnchecked<Type>(UnaryOperator op, BoxedExpression arg, Type CastTo)
        {
            Contract.Requires(arg != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.CastExpression(new UnaryExpression(op, arg, null), null, CastTo, true);
        }

        public static BoxedExpression Const<Local, Parameter, Field, Property, Event, Method, Type, Attribute, Assembly>(object obj, Type type, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
            Contract.Requires(mdDecoder != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            Contract.Assume(!mdDecoder.Equal(type, mdDecoder.System_Int32) || obj is Int32);
            return new ClousotExpression<Type>.ConstantExpression(obj, type);
        }

        public static BoxedExpression ConstCast<Local, Parameter, Field, Property, Event, Method, Type, Attribute, Assembly>(object obj, string name, Type type, Type type_cast, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
            Contract.Requires(mdDecoder != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.CastExpression(Const(obj, type, mdDecoder), name, type_cast, false);
        }

        public static BoxedExpression ConstCastUnchecked<Local, Parameter, Field, Property, Event, Method, Type, Attribute, Assembly>(object obj, Type type, Type type_cast, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
            Contract.Requires(mdDecoder != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.CastExpression(Const(obj, type, mdDecoder), null, type_cast, true);
        }

        public static BoxedExpression ConstBool<Local, Parameter, Field, Property, Event, Method, Type, Attribute, Assembly>(object obj, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
        {
            Contract.Requires(mdDecoder != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.ConstantExpression(obj, mdDecoder.System_Boolean, true);
        }

        public static BoxedExpression Result<Type>(Type type)
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.ResultExpression(type);
        }

        public static BoxedExpression Old<Type>(BoxedExpression oldExpr, Type type)
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.OldExpression(oldExpr, type);
        }

        public static BoxedExpression ValueAtReturn<Type>(BoxedExpression oldExpr, Type type)
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.ValueAtReturnExpression(oldExpr, type);
        }

        #endregion

        #region Public methods
        public virtual bool IsVariable
        {
            get { return false; }
        }

        public virtual bool IsVariableBoundedInQuantifier
        {
            get { return false; }
        }

        public virtual bool IsBooleanTyped
        {
            get { return false; }
        }

        public virtual object UnderlyingVariable
        {
            get { return null; }
        }

        public virtual PathElement[] AccessPath
        {
            get
            {
                Contract.Ensures(Contract.Result<PathElement[]>() == null || Contract.ForAll(Contract.Result<PathElement[]>(), p => p != null));
                return null;
            }
        }

        public virtual bool IsConstant
        {
            get { return false; }
        }
        public virtual object Constant { get { throw new InvalidOperationException(); } }
        public virtual object ConstantType { get { throw new InvalidOperationException(); } }
        public virtual bool IsSizeOf
        {
            get { return false; }
        }
        public virtual bool SizeOf(out int size) { size = 0; return false; }
        public virtual bool SizeOf(out object type, out int size) { type = null; size = 0; return false; }

        public virtual bool IsUnary
        {
            get { return false; }
        }
        public virtual UnaryOperator UnaryOp { get { throw new InvalidOperationException(); } }
        public virtual BoxedExpression UnaryArgument
        {
            get
            {
                Contract.Ensures(Contract.Result<BoxedExpression>() != null);

                throw new InvalidOperationException();
            }
        }

        public virtual bool IsBinary
        {
            get { return false; }
        }
        public virtual bool IsBinaryExpression(out BinaryOperator bop, out BoxedExpression left, out BoxedExpression right)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out left) != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out right) != null);

            bop = BinaryOperator.Add; // dummy
            left = null;
            right = null;
            return false;
        }

        public virtual bool IsUnaryExpression(out UnaryOperator uop, out BoxedExpression left)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out left) != null);

            uop = default(UnaryOperator); // dummy
            left = default(BoxedExpression);
            return false;
        }

        public virtual bool IsIsInstExpression(out BoxedExpression exp, out object type)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out exp) != null);

            exp = default(BoxedExpression);
            type = default(object);
            return false;
        }

        public virtual BinaryOperator BinaryOp { get { throw new InvalidOperationException(); } }

        public virtual BoxedExpression BinaryLeft
        {
            get
            {
                Contract.Ensures(Contract.Result<BoxedExpression>() != null);

                throw new InvalidOperationException();
            }
        }

        public virtual BoxedExpression BinaryRight
        {
            get
            {
                Contract.Ensures(Contract.Result<BoxedExpression>() != null);

                throw new InvalidOperationException();
            }
        }

        public virtual bool IsIsInst
        {
            get { return false; }
        }
        public virtual bool IsNull
        {
            get { return false; }
        }

        public virtual bool IsCast
        {
            get { return false; }
        }

        public virtual bool IsResult
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsParameterRef
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsArrayIndex
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsArrayIndexExpression(out BoxedExpression array, out BoxedExpression index, out object type)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out array) != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out index) != null);

            array = index = null;
            type = null;
            return false;
        }

        public virtual bool IsQuantified
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsQuantifiedExpression(out bool isForAll, out BoxedExpression boundVar, out BoxedExpression lower, out BoxedExpression upper, out BoxedExpression body)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out boundVar) != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out lower) != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out upper) != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out body) != null);

            isForAll = false;

            boundVar = lower = upper = body = null;

            return false;
        }

        abstract public BoxedExpression Negate();

        public abstract void AddFreeVariables(Set<BoxedExpression> set);

        /// <summary>
        /// If expression has associated info, such as a pointer with associated writable byte information, then this operation
        /// returns the expression representing the info.
        /// </summary>
        public virtual bool TryGetAssociatedInfo(AssociatedInfo infoKind, out BoxedExpression info)
        {
            info = null;
            return false;
        }

        /// <summary>
        /// If expression has associated info, such as a pointer with associated writable byte information, then this operation
        /// returns the expression representing the info.
        /// The extra PC provides information about where the associated info should hold in case of a variable.
        /// </summary>
        public virtual bool TryGetAssociatedInfo(APC atPC, AssociatedInfo infoKind, out BoxedExpression info)
        {
            info = null; return false;
        }

        /// <summary>
        /// Replaces the occurrences of what with newExp, converting external subtrees as they are found.
        /// </summary>
        public virtual BoxedExpression Substitute(BoxedExpression what, BoxedExpression newExp)
        {
            Contract.Requires(what != null);
            Contract.Requires(newExp != null);

            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            if (this == what || this.Equals(what)) return newExp;

            return this.RecursiveSubstitute(what, newExp);
        }

        /// <summary>
        /// Performs the given substitution of variables.
        /// Returns null if the converter returns null
        /// </summary>
        public abstract BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map);

        public abstract BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null);

        protected virtual BoxedExpression RecursiveSubstitute(BoxedExpression what, BoxedExpression newExp)
        {
            Contract.Requires(what != null);
            Contract.Requires(newExp != null);

            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            // default
            return this;
        }

        public abstract void Dispatch(IBoxedExpressionVisitor visitor);

        public virtual bool TryGetType(out object type)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out type) != null);

            type = null;
            return false;
        }


        internal abstract Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(BoxedExpression.PC pc, Visitor visitor, Data data)
        where Visitor : IVisitMSIL<BoxedExpression.PC, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result>;

        #endregion

        #region System.Object overrides
        public abstract override bool Equals(object obj);

        public sealed override int GetHashCode()
        {
            if (hashCodeCache == HashCodeUnSet)
            {
                hashCodeCache = ComputeHashCode();
            }
            return hashCodeCache;
        }

        public abstract override string ToString();
        public virtual string ToString<Typ2>(Converter<Typ2, string> converter)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            return ToString(); // Default behavior
        }

        #endregion

        #region SimpleSyntacticEquals

        public static bool SimpleSyntacticEquality(BoxedExpression left, BoxedExpression right)
        {
            if (left == null || right == null)
            {
                return left == right;
            }

            if (left.Equals(right))
            {
                return true;
            }

            BinaryOperator bopLeft, bopRight;
            BoxedExpression left1, left2, right1, right2;
            if (left.IsBinaryExpression(out bopLeft, out left1, out left2) && bopLeft.IsComparisonBinaryOperator()
              && right.IsBinaryExpression(out bopRight, out right1, out right2) && bopRight.IsComparisonBinaryOperator())
            {
                // left1 bopLeft left2 && right2 bopleft right 1
                BinaryOperator bopRightInverted;
                if (bopRight.TryInvert(out bopRightInverted) && bopLeft == bopRightInverted)
                {
                    return BoxedExpression.SimpleSyntacticEquality(left1, right2) && BoxedExpression.SimpleSyntacticEquality(left2, right1);
                }
            }

            return false;
        }

        #endregion

        #region Actual Expression implementations

#pragma warning disable 659
        [Serializable]
        internal class VariableExpression : BoxedExpression
        {
            public readonly PathElement[] Path;
            new public object Var { get; private set; } // not readonly because we need to assign it during serialization
            [NonSerialized]
            private object _savedVar; // used in serialization only
            public readonly object mType;
            public readonly BoxedExpression WritableBytes/*?*/;
            private readonly bool isBoundedVarInQuantifier = false;

            // Just before the serialization, save the var, if needed            
            [OnSerializing]
            private void OnSerializing(StreamingContext context)
            {
                // keep Var in the serialized object only if it is a string
                // use _savedVar to save Var during serialization and restore it afterwards
                _savedVar = this.Var;
                if (!(this.Var is string))
                {
                    if (this.Path == null)
                    {
                        // In this case the deserialized VariableExpression would have no sense
                        throw new NotSupportedException(String.Format("Trying to serialize a variable with Path == null and Var not string: {0}", this.ToString()));
                    }
                    this.Var = null;
                }
            }

            // Just after the serialization, restore the value
            [OnSerialized]
            private void OnSerialized(StreamingContext context)
            {
                this.Var = _savedVar;
            }

            public VariableExpression(object var)
              : this(var, (PathElement[])null, (BoxedExpression)null, null)
            { }

            public VariableExpression(object var, PathElement[] path)
              : this(var, path, (BoxedExpression)null, null)
            { }

            public VariableExpression(object var, FList<PathElement> path)
              : this(var, path, (BoxedExpression)null, null)
            { }

            public VariableExpression(object var, PathElement[] path, object type)
              : this(var, path, (BoxedExpression)null, type)
            { }

            public VariableExpression(object var, FList<PathElement> path, BoxedExpression wb)
              : this(var, path, wb, null)
            { }

            public VariableExpression(object var, PathElement[] path, BoxedExpression wb)
              : this(var, path, wb, null)
            { }

            public VariableExpression(object var, FList<PathElement> path, object type)
              : this(var, path, (BoxedExpression)null, type)
            { }

            public VariableExpression(object var, FList<PathElement> path, BoxedExpression wb, object type)
              : this(var, path == null ? null : path.ToArray(), wb, type)
            { }

            public VariableExpression(object var, PathElement[] path, BoxedExpression wb, object type)
            {
                this.Var = var;
                this.Path = path;
                this.WritableBytes = wb;
                this.mType = type;
            }

            public VariableExpression(string name, bool p)
            {
                this.Var = name;
                isBoundedVarInQuantifier = p;
            }

            public override bool TryGetType(out object type)
            {
                if (mType != null)
                {
                    type = mType;
                    return true;
                }
                type = null;
                return false;
            }

            public override bool IsVariable { get { return true; } }
            //public override object Variable { get { return Var; } }

            public override object UnderlyingVariable
            {
                get
                {
                    return Var;
                }
            }

            public override bool IsVariableBoundedInQuantifier
            {
                get
                {
                    return isBoundedVarInQuantifier;
                }
            }


            public override PathElement[] AccessPath
            {
                get
                {
                    return this.Path;
                }
            }

            public override bool IsParameterRef
            {
                get
                {
                    return this.Path != null && this.Path.Length > 0 && this.Path[0].AssumeNotNull().IsParameterRef;
                }
            }

            public override bool IsBooleanTyped
            {
                get
                {
                    return (Path != null && Path[Path.Length - 1].AssumeNotNull().IsBooleanTyped);
                }
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                set.Add(this);
                if (this.WritableBytes != null) this.WritableBytes.AddFreeVariables(set);
            }

            public override bool TryGetAssociatedInfo(AssociatedInfo infoKind, out BoxedExpression info)
            {
                switch (infoKind)
                {
                    case AssociatedInfo.WritableBytes:
                        if (this.WritableBytes != null) { info = this.WritableBytes; return true; }
                        break;
                }
                info = null;
                return false;
            }

            public override bool TryGetAssociatedInfo(APC atPC, AssociatedInfo infoKind, out BoxedExpression info)
            {
                // We don't have pc specific info here
                return this.TryGetAssociatedInfo(infoKind, out info);
            }

            protected override BoxedExpression RecursiveSubstitute(BoxedExpression what, BoxedExpression newExp)
            {
                // F: this causes a stack overflow has when we visit WB(s), we visit the argument s, and then s.Wb, i.e. WB(s) ...
                /*
                if (this.WritableBytes != null)
                {
                  BoxedExpression subst = this.WritableBytes.Substitute(what, newExp);
                  if (subst == this.WritableBytes) return this;
                  return new VariableExpression(this.Var, this.Path, subst);
                }
                */

                var whatAsVariableExpression = what as VariableExpression;
                if (whatAsVariableExpression != null)
                {
                    if (whatAsVariableExpression.Var.Equals(this.Var))
                    {
                        return newExp;
                    }
                }

                return this;
            }

            public override BoxedExpression Negate()
            {
                return BoxedExpression.UnaryLogicalNot(this);
            }

            #region System.Object overrides
            public override bool Equals(object obj)
            {
                if (this == obj) return true;
                BoxedExpression that = obj as BoxedExpression;
                if (that != null)
                {
                    if (this.Var != null && that.IsVariable) { return this.Var.Equals(that.UnderlyingVariable); }

                    // compare the accessPaths
                    if (this.Var == null && that.UnderlyingVariable == null)
                    {
                        var thisPath = this.AccessPath;
                        var thatPath = that.AccessPath;
                        if (thisPath != null && thatPath != null && thisPath.Length == thatPath.Length)
                        {
                            Contract.Assume(Contract.ForAll(thisPath, p => p != null));

                            for (var i = 0; i < thisPath.Length; i++)
                            {
                                // ToString is brittle or too expensive? We should use something else?
                                if (thisPath[i].ToString() != thatPath[i].ToString())
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                    }
                }
                return false;
            }

            protected override int ComputeHashCode()
            {
                if (this.Var != null) return this.Var.GetHashCode();
                if (this.Path != null) return this.Path.GetHashCode();

                //Contract.Assert(false, "Var && Paths are null!!!");

                // Some of michael's code creates a variable where Var and Paths are both null
                return -1;
            }

            public override string ToString()
            {
                if (this.Path != null)
                {
                    return this.Path.ToCodeString();
                }
                if (this.Var != null)
                {
#if DEBUG
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        return Var.ToString();
                    }
#endif
                    var fullName = Var.ToString();
                    var length = fullName.IndexOf('(');
                    return fullName.Substring(0, length < 0 ? fullName.Length : length);
                }
                return "Path & Var are null!!"; // should not happen
            }

            #endregion

            /// <summary>
            /// For variables, the access path is decoded prior to getting here. The final op is a nop.
            /// </summary>
            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(BoxedExpression.PC pc, Visitor visitor, Data data)
            {
                return visitor.Nop(pc, data);
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                Variable v;
                if (this.TryGetFrameworkVariable(out v))
                {
                    var result = map(v, this);
                    return result;
                }
                return this;
            }

            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                Variable v;
                if (this.TryGetFrameworkVariable(out v))
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
                return this; // REVIEW: if variable is null or not the right type, we leave it.
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.Variable(this.Var, this.Path, this);
            }
        }

        [Serializable]
        public /* Mic: public for convenience of use in OutputPrettyCs, temporary until BoxedExpression are cleaned up to include type information */
          class BinaryExpression : BoxedExpression
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.Left != null);
                Contract.Invariant(this.Right != null);
            }


            public readonly BinaryOperator Op;
            public readonly BoxedExpression Left;
            public readonly BoxedExpression Right;

            [NonSerialized]
            private readonly object frameworkVar;
            public override object UnderlyingVariable
            {
                get
                {
                    return frameworkVar;
                }
            }

            public BinaryExpression(BinaryOperator op, BoxedExpression left, BoxedExpression right, object frameworkVar = null)
            {
                Contract.Requires(left != null);
                Contract.Requires(right != null);

                this.Op = op;
                this.Left = left;
                this.Right = right;
                this.frameworkVar = frameworkVar;
            }

            public override bool IsBinary
            {
                get { return true; }
            }

            [ContractVerification(false)]
            public override bool IsBinaryExpression(out BinaryOperator bop, out BoxedExpression left, out BoxedExpression right)
            {
                bop = this.Op;
                left = this.Left;
                right = this.Right;
                return true;
            }

            public override BoxedExpression BinaryLeft
            {
                get { return Left; }
            }

            public override BoxedExpression BinaryRight
            {
                get { return Right; }
            }

            public override BinaryOperator BinaryOp
            {
                get { return Op; }
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                this.Left.AddFreeVariables(set);
                this.Right.AddFreeVariables(set);
            }

            protected override BoxedExpression RecursiveSubstitute(BoxedExpression what, BoxedExpression newExp)
            {
                BoxedExpression left = this.Left.Substitute(what, newExp);
                BoxedExpression right = this.Right.Substitute(what, newExp);

                if (left == this.Left && right == this.Right) return this;
                return new BinaryExpression(this.Op, left, right);
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(BoxedExpression.PC pc, Visitor visitor, Data data)
            {
                if (this.BinaryOp == BinaryOperator.LogicalOr || this.BinaryOp == BinaryOperator.LogicalAnd)
                {
                    // is decoded as shortcut evaluation and final operation is a nop.
                    return visitor.Nop(pc, data);
                }
                return visitor.Binary(pc, this.BinaryOp, Unit.Value, Unit.Value, Unit.Value, data);
            }

            private bool IsEqualityOp(BinaryOperator op)
            {
                return op == BinaryOperator.Ceq || op == BinaryOperator.Cobjeq;
            }

            #region System.Object overrides

            public override bool Equals(object obj)
            {
                if (this == obj) return true;
                BinaryExpression that = obj as BinaryExpression;
                if (that != null)
                {
                    return this.Op == that.Op && this.Left.Equals(that.Left) && this.Right.Equals(that.Right);
                }
                // TODO: equality with external expressions?
                return false;
            }

            protected override int ComputeHashCode()
            {
                return (int)Op + this.Left.GetHashCode() + this.Right.GetHashCode();
            }

            private string GetFormatString()
            {
                // TODO: improve
                // Do some pretty printing
                string format = null;
                string formatLeft, formatRight;

                switch (this.Op)
                {
                    case BinaryOperator.Add:
                    case BinaryOperator.Add_Ovf:
                    case BinaryOperator.Add_Ovf_Un:
                        format = "{0} {1} {2}";
                        break;

                    case BinaryOperator.Sub:
                    case BinaryOperator.Sub_Ovf:
                    case BinaryOperator.Sub_Ovf_Un:
                        if (!this.Right.IsBinary)
                            format = "{0} {1} {2}";
                        else
                            format = "({0} {1} {2})";
                        break;

                    case BinaryOperator.And:
                    case BinaryOperator.Or:
                    case BinaryOperator.Rem:
                    case BinaryOperator.Rem_Un:
                    case BinaryOperator.Shl:
                    case BinaryOperator.Shr:
                    case BinaryOperator.Shr_Un:
                    case BinaryOperator.LogicalAnd:
                    case BinaryOperator.LogicalOr:
                    case BinaryOperator.Xor:
                        format = "({0} {1} {2})";
                        break;

                    case BinaryOperator.Ceq:
                        {
                            int cval;

                            if (Left.IsBooleanTyped && Right.IsConstantInt(out cval) && cval == 0)
                            {
                                format = this.Left.IsBinary ? "!({0})" : "!{0}";
                            }
                            else
                            {
                                formatLeft = this.Left.IsBinary && !CanAvoidParenthesesInPrettyPrintingBinary(this.Left.BinaryOp) ? "({0})" : "{0}";
                                formatRight = this.Right.IsBinary && !CanAvoidParenthesesInPrettyPrintingBinary(this.Right.BinaryOp) ? "({2})" : "{2}";

                                format = formatLeft + " {1} " + formatRight;
                            }
                            break;
                        }
                    case BinaryOperator.Cne_Un:
                        {
                            int cval;

                            if (Left.IsBooleanTyped && Right.IsConstantInt(out cval) && cval == 0)
                            {
                                format = "{0}";
                            }
                            else
                            {
                                formatLeft = this.Left.IsBinary && !CanAvoidParenthesesInPrettyPrintingBinary(this.Left.BinaryOp) ? "({0})" : "{0}";
                                formatRight = this.Right.IsBinary && !CanAvoidParenthesesInPrettyPrintingBinary(this.Right.BinaryOp) ? "({2})" : "{2}";

                                format = formatLeft + " {1} " + formatRight;
                            }
                            break;
                        }

                    case BinaryOperator.Mul:
                    case BinaryOperator.Mul_Ovf:
                    case BinaryOperator.Mul_Ovf_Un:
                    case BinaryOperator.Div:
                    case BinaryOperator.Div_Un:

                    case BinaryOperator.Cge:
                    case BinaryOperator.Cge_Un:
                    case BinaryOperator.Cgt:
                    case BinaryOperator.Cgt_Un:
                    case BinaryOperator.Cle:
                    case BinaryOperator.Cle_Un:
                    case BinaryOperator.Clt:
                    case BinaryOperator.Clt_Un:
                        formatLeft = this.Left.IsBinary ? "({0})" : "{0}";
                        formatRight = this.Right.IsBinary ? "({2})" : "{2}";

                        format = formatLeft + " {1} " + formatRight;
                        break;

                    case BinaryOperator.Cobjeq:
                        formatLeft = this.Left.IsBinary ? "({0})" : "{0}";
                        format = formatLeft + ".Equals({2})";
                        break;
                }

                return format;
            }

            private bool CanAvoidParenthesesInPrettyPrintingBinary(BinaryOperator binaryOperator)
            {
                switch (binaryOperator)
                {
                    case BinaryOperator.Add:
                    case BinaryOperator.Add_Ovf:
                    case BinaryOperator.Add_Ovf_Un:

                    case BinaryOperator.Div:
                    case BinaryOperator.Div_Un:
                    case BinaryOperator.Mul:
                    case BinaryOperator.Mul_Ovf:
                    case BinaryOperator.Mul_Ovf_Un:
                    case BinaryOperator.Rem:
                    case BinaryOperator.Rem_Un:
                    case BinaryOperator.Sub:
                    case BinaryOperator.Sub_Ovf:
                    case BinaryOperator.Sub_Ovf_Un:
                        return true;

                    default:
                        return false;
                }
            }

            public override string ToString()
            {
                string opString;
                // Special case to handle the code generated by the C# compiler for "o is A", when A is a interface
                // The compiler generates: "(o as A) >_un null"
                if (this.Op == BinaryOperator.Cgt_Un && this.Right.IsNull)
                {
                    opString = BinaryOperator.Cne_Un.ToCodeString();
                }
                else
                {
                    opString = this.Op.ToCodeString();
                }

                return string.Format(GetFormatString(), this.Left.ToString(), opString, this.Right.ToString());
            }

            public override string ToString<Typ2>(Converter<Typ2, string> converter)
            {
                return string.Format(GetFormatString(), this.Left.ToString(converter), this.Op.ToCodeString(), this.Right.ToString(converter));
            }



            #endregion

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                var left = this.Left.Substitute(map);
                if (left == null) return null;
                var right = this.Right.Substitute(map);
                if (right == null) return null;
                if (this.Left == left && this.Right == right) return this;
                return new BinaryExpression(this.Op, left, right);
            }

            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                var left = this.Left.Rename(renaming, refiner);
                if (left == null) return null;
                var right = this.Right.Rename(renaming, refiner);
                if (right == null) return null;
                if (this.Left == left && this.Right == right) return this;
                return new BinaryExpression(this.Op, left, right);
            }

            public override BoxedExpression Negate()
            {
                switch (this.Op)
                {
                    case BinaryOperator.Ceq:
                        return new BinaryExpression(BinaryOperator.Cne_Un, this.Left, this.Right);

                    case BinaryOperator.Cge:
                        return new BinaryExpression(BinaryOperator.Clt, this.Left, this.Right);

                    case BinaryOperator.Cge_Un:
                        return new BinaryExpression(BinaryOperator.Clt_Un, this.Left, this.Right);

                    case BinaryOperator.Cgt:
                        return new BinaryExpression(BinaryOperator.Cle, this.Left, this.Right);

                    case BinaryOperator.Cgt_Un:
                        return new BinaryExpression(BinaryOperator.Cle_Un, this.Left, this.Right);

                    case BinaryOperator.Cle:
                        return new BinaryExpression(BinaryOperator.Cgt, this.Left, this.Right);

                    case BinaryOperator.Cle_Un:
                        return new BinaryExpression(BinaryOperator.Cgt_Un, this.Left, this.Right);

                    case BinaryOperator.Clt:
                        return new BinaryExpression(BinaryOperator.Cge, this.Left, this.Right);

                    case BinaryOperator.Clt_Un:
                        return new BinaryExpression(BinaryOperator.Cge_Un, this.Left, this.Right);

                    case BinaryOperator.Cne_Un:
                        return new BinaryExpression(BinaryOperator.Ceq, this.Left, this.Right);

                    case BinaryOperator.Cobjeq:
                        return new BinaryExpression(BinaryOperator.Cne_Un, this.Left, this.Right);

                    default:
                        return BoxedExpression.UnaryLogicalNot(this);
                }
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.Binary(this.Op, this.Left, this.Right, this);
            }
        }

        /// <summary>
        /// Allows to output expressions such as "Left.method_to_call (Right)"
        /// </summary>
        [Serializable]
        public /* Mic: idem */ class BinaryExpressionMethodCall : BinaryExpression
        {
            /// <summary>
            /// create Left."methodCall"()
            /// </summary>
            public BinaryExpressionMethodCall(BinaryOperator dummy, BoxedExpression left, string methodToCall, object frameworkVar = null)
              : this(dummy, left, BoxedExpression.Var(null), methodToCall, frameworkVar)
            {
                Contract.Requires(left != null);
            }

            public BinaryExpressionMethodCall(BinaryOperator op, BoxedExpression left, BoxedExpression right, string method_to_call, object frameworkVar = null)
              : base(op, left, right, frameworkVar)
            {
                Contract.Requires(left != null);
                Contract.Requires(right != null);

                mMethodToCall = method_to_call;
            }

            public override string ToString()
            {
                var right = "";
                if (Right.IsVariable && (Right.UnderlyingVariable != null || Right.AccessPath != null))
                {
                    right = Right.ToString();
                }
                return string.Format("{0}.{1}({2})", Left.ToString(), mMethodToCall, right);
            }

            public override string ToString<Typ2>(Converter<Typ2, string> converter)
            {
                return string.Format("{0}.{1}({2})", Left.ToString(converter), mMethodToCall, Right.ToString(converter));
            }

            readonly private string mMethodToCall;
            public string MethodToCall { get { return mMethodToCall; } }
        }

        [Serializable]
        internal class UnaryExpression : BoxedExpression
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(this.Argument != null);
            }

            public readonly UnaryOperator Op;
            public readonly BoxedExpression Argument;

            [NonSerialized]
            private readonly object frameworkVar;
            public override object UnderlyingVariable
            {
                get
                {
                    return frameworkVar;
                }
            }

            public UnaryExpression(UnaryOperator op, BoxedExpression arg, object frameworkVar)
            {
                Contract.Requires(arg != null);

                this.Op = op;
                this.Argument = arg;
                this.frameworkVar = frameworkVar;
            }

            public override bool IsUnary { get { return true; } }
            public override BoxedExpression UnaryArgument { get { return Argument; } }
            public override UnaryOperator UnaryOp { get { return Op; } }

            public override bool IsUnaryExpression(out UnaryOperator uop, out BoxedExpression left)
            {
                uop = this.Op;
                left = this.Argument;

                return true;
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                this.Argument.AddFreeVariables(set);
            }

            protected override BoxedExpression RecursiveSubstitute(BoxedExpression what, BoxedExpression newExp)
            {
                BoxedExpression nested = this.Argument.Substitute(what, newExp);
                if (nested == this.Argument)
                {
                    return this;
                }
                return new UnaryExpression(this.Op, nested, null);
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(BoxedExpression.PC pc, Visitor visitor, Data data)
            {
                return visitor.Unary(pc, this.UnaryOp, false, false, Unit.Value, Unit.Value, data);
            }

            #region System.Object overrides

            public override bool Equals(object obj)
            {
                if (this == obj) return true;
                UnaryExpression that = obj as UnaryExpression;
                if (that != null)
                {
                    return this.Op == that.Op && this.Argument.Equals(that.Argument);
                }
                // TODO: handle equality with external expressions
                return false;
            }

            protected override int ComputeHashCode()
            {
                return (int)Op + this.Argument.GetHashCode();
            }

            virtual protected string OpCodeString
            {
                get
                {
                    return this.Op.ToCodeString();
                }
            }

            public override string ToString()
            {
                // Mic: we don't want to make some "(UIntPtr)" casts, because they interfere
                // with contracts output. The solution we came up with is to simply ignore the Conv_u casts.
                if (this.UnaryOp == UnaryOperator.Conv_u)
                    return this.Argument.ToString();

                string format = "{0}" + /*(this.Argument.IsConstant ? "{1}" : */"({1})"/*)*/; // Mic: always parentheses, for example conv with negative constants
                return string.Format(format, this.OpCodeString, this.Argument.ToString());
            }

            public override string ToString<Typ2>(Converter<Typ2, string> converter)
            {
                // Mic: we don't want to make some "(UIntPtr)" casts, because they interfere
                // with contracts output. The solution we came up with is to simply ignore the Conv_u casts.
                if (this.UnaryOp == UnaryOperator.Conv_u)
                    return this.Argument.ToString(converter);

                string format = "{0}" + /*(this.Argument.IsConstant ? "{1}" : */"({1})"/*)*/; // Mic: always parentheses, for example conv with negative constants
                return string.Format(format, this.OpCodeString, this.Argument.ToString(converter));
            }

            #endregion

            public override BoxedExpression Negate()
            {
                switch (this.Op)
                {
                    case UnaryOperator.Not:
                        return this.Argument;

                    default:
                        return BoxedExpression.UnaryLogicalNot(this);
                }
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                var arg = this.Argument.Substitute(map);
                if (arg == this.Argument) { return this; }
                if (arg == null) return null;
                return new UnaryExpression(this.Op, arg, null);
            }

            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                var arg = this.Argument.Rename(renaming, refiner);
                if (arg == this.Argument) { return this; }
                if (arg == null) return null;
                return new UnaryExpression(this.Op, arg, null);
            }
            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.Unary(this.Op, this.Argument, this);
            }
        }

        [Serializable]
        public abstract class ContractExpression : BoxedExpression
        {
            public readonly string Tag;
            public readonly BoxedExpression Condition;
            public readonly APC Apc;
            /// <summary>
            /// An object encoding where this contract comes from. Can be null.
            /// </summary>
            public readonly Provenance Provenance;
            public readonly string SourceCondition;

            public ContractExpression(BoxedExpression cond, string tag, APC apc, Provenance provenance, string sourceCondition)
            {
                // Contract.Assume(apc.Block != null, "It should be a precondition");

                this.Tag = tag;
                this.Condition = cond;
                this.Apc = apc;
                this.Provenance = provenance;
                this.SourceCondition = sourceCondition;
            }

            protected override int ComputeHashCode()
            {
                return Condition.GetHashCode();
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                this.Condition.AddFreeVariables(set);
            }

            internal abstract override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(PC pc, Visitor visitor, Data data);

            public abstract override bool Equals(object obj);

            public abstract override string ToString();

            public bool HasSourceContext
            {
                get
                {
                    var block = this.Apc.Block;
                    return block != null && block.SourceDocument(this.Apc) != null;
                }
            }
            public string SourceAssertionCondition
            {
                get
                {
                    if (this.SourceCondition != null) return this.SourceCondition;
                    Contract.Assume(this.Apc.Block != null);
                    return this.Apc.Block.SourceAssertionCondition(this.Apc);
                }
            }
            public string SourceDocument { get { Contract.Assume(this.Apc.Block != null); return this.Apc.Block.SourceDocument(this.Apc); } }
            public int SourceStartLine { get { Contract.Assume(this.Apc.Block != null); return this.Apc.Block.SourceStartLine(this.Apc); } }
            public int SourceStartColumn { get { Contract.Assume(this.Apc.Block != null); return this.Apc.Block.SourceStartColumn(this.Apc); } }
            public int SourceEndLine { get { Contract.Assume(this.Apc.Block != null); return this.Apc.Block.SourceEndLine(this.Apc); } }
            public int SourceEndColumn { get { Contract.Assume(this.Apc.Block != null); return this.Apc.Block.SourceEndColumn(this.Apc); } }
            public int SourceStartIndex { get { Contract.Assume(this.Apc.Block != null); return this.Apc.Block.SourceStartIndex(this.Apc); } }
            public int SourceLength { get { Contract.Assume(this.Apc.Block != null); return this.Apc.Block.SourceLength(this.Apc); } }
            public int ILOffset { get { Contract.Assume(this.Apc.Block != null); return this.Apc.Block.ILOffset(this.Apc); } }
            public abstract override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map);
        }

        [Serializable]
        public class AssertExpression : ContractExpression
        {
            public AssertExpression(BoxedExpression cond, string tag, APC apc, Provenance provenance, string sourceCondition)
              : base(cond, tag, apc, provenance, sourceCondition)
            {
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(PC pc, Visitor visitor, Data data)
            {
                return visitor.Assert(pc, this.Tag, Unit.Value, this.Provenance, data);
            }

            public override bool Equals(object obj)
            {
                var that = obj as AssertExpression;
                if (that == null) return false;
                return this.Condition.Equals(that.Condition);
            }

            public override string ToString()
            {
                return "assert(" + this.Tag + ") " + Condition.ToString();
            }

            public override string ToString<Typ2>(Converter<Typ2, string> converter)
            {
                return "assert(" + this.Tag + ") " + Condition.ToString(converter);
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                var cond = this.Condition.Substitute(map);
                if (cond == this.Condition) return this;
                if (cond == null) return null;
                return new AssertExpression(cond, this.Tag, this.Apc, this.Provenance, this.SourceCondition);
            }

            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                var cond = this.Condition.Rename(renaming, refiner);
                if (cond == this.Condition) return this;
                if (cond == null) return null;
                return new AssertExpression(cond, this.Tag, this.Apc, this.Provenance, this.SourceCondition);
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.Assert(this.Condition, this);
            }

            public override BoxedExpression Negate()
            {
                return new AssertExpression(this.Condition.Negate(), this.Tag, this.Apc, this.Provenance, this.SourceCondition);
            }
        }

#if false
        // special kind of assume used to add context- and path-sensitive postconditions to a method (needed for semantic baselining)
        [Serializable]
        public class AssumeAsPostConditionExpression : ContractExpression
        {
            // holds information about the path on which this assume is to be placed
            public readonly Set<BoxedExpression> PathContext;
            public readonly string CalleeName;
            public object Callee;

            // for the code:
            // if (x > 10) foo(); assume e
            // we want assume (x > 10 => e); we write store this as cond = e, pathContext = [x > 10], calleeName = foo

            // TODO: actually, just want return value instruction?
            public AssumeAsPostConditionExpression(BoxedExpression cond, Set<BoxedExpression> pathContext, object callee, string calleeName, string tag, APC apc, Provenance provenance)
              : base(cond, tag, apc, provenance, null)
            {
                Contract.Requires(cond != null);
                this.PathContext = pathContext;
                this.Callee = callee;
                this.CalleeName = calleeName;
            }

            // return pathContext => cond
            private BoxedExpression GetImplicationForm()
            {
                // will this use the first element as the seed as desired?
                var left = PathContext.Aggregate((acc, cur) => BoxedExpression.BinaryLogicalAnd(acc, cur));
                return BoxedExpression.BinaryLogicalOr(left.Negate(), Condition);
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(PC pc, Visitor visitor, Data data)
            {
                //  SHB: just copied this over from ordinary assume, but Unit.Value seems a bit odd (depending on what ForwardDecode is used for I suppose...)
                return visitor.Assume(pc, this.Tag, Unit.Value, this.Provenance, data);
            }

            #region System.Object overrides

            public override bool Equals(object obj)
            {
                if (this == obj) return true;
                AssumeAsPostConditionExpression that = obj as AssumeAsPostConditionExpression;
                if (that != null)
                {
                    return this.GetImplicationForm().Equals(that.GetImplicationForm()) && this.CalleeName.Equals(that.CalleeName);
                }
                return false;
            }

            protected override int ComputeHashCode()
            {
                return base.ComputeHashCode() + this.CalleeName.GetHashCode();
            }

            public override string ToString()
            {
                return "assume(" + this.Tag + ") !(" + GetImplicationForm().ToString() + ") || " + Condition.ToString();
            }

            public override string ToString<Typ2>(Converter<Typ2, string> converter)
            {
                return "assume(" + this.Tag + ") !(" + GetImplicationForm().ToString() + ") || " + Condition.ToString(converter);
            }
            #endregion

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                Contract.Assume(false, "untested");
                var cond = this.Condition.Substitute(map);
                if (cond == this.Condition) return this;
                if (cond == null) return null;
                // TODO: is this ok?
                var substituted = PathContext.Select(x => Substitute(map)) as Set<BoxedExpression>;
                return new AssumeAsPostConditionExpression(cond, substituted, this.Callee, this.CalleeName, this.Tag, this.Apc, this.Provenance);
            }

            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                Contract.Assume(false, "untested");
                var cond = this.Condition.Rename(renaming, refiner);
                if (cond == this.Condition) return this;
                if (cond == null) return null;
                // TODO: is this ok?
                return new AssumeAsPostConditionExpression(cond, substituted, this.Callee, this.CalleeName, this.Tag, this.Apc, this.Provenance);
            }
            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.Assume(GetImplicationForm(), this);
            }

            public override BoxedExpression Negate()
            {
                return new AssumeExpression(GetImplicationForm().Negate(), this.Tag, this.Apc, this.Provenance, null);
            }
        }
#endif

        [Serializable]
        public class AssumeExpression : ContractExpression
        {
            public AssumeExpression(BoxedExpression cond, string tag, APC apc, Provenance provenance, string sourceCondition)
              : base(cond, tag, apc, provenance, sourceCondition)
            {
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(PC pc, Visitor visitor, Data data)
            {
                return visitor.Assume(pc, this.Tag, Unit.Value, this.Provenance, data);
            }

            public override bool Equals(object obj)
            {
                var that = obj as AssumeExpression;
                if (that == null) return false;
                return this.Condition.Equals(that.Condition);
            }

            public override string ToString()
            {
                return "assume(" + this.Tag + ") " + Condition.ToString();
            }

            public override string ToString<Typ2>(Converter<Typ2, string> converter)
            {
                return "assume(" + this.Tag + ") " + Condition.ToString(converter);
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                var cond = this.Condition.Substitute(map);
                if (cond == this.Condition) return this;
                if (cond == null) return null;
                return new AssumeExpression(cond, this.Tag, this.Apc, this.Provenance, this.SourceCondition);
            }
            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                var cond = this.Condition.Rename(renaming, refiner);
                if (cond == this.Condition) return this;
                if (cond == null) return null;
                return new AssumeExpression(cond, this.Tag, this.Apc, this.Provenance, this.SourceCondition);
            }
            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.Assume(this.Condition, this);
            }

            public override BoxedExpression Negate()
            {
                return new AssumeExpression(this.Condition.Negate(), this.Tag, this.Apc, this.Provenance, null);
            }
        }

        [Serializable]
        public class ArrayIndexExpression<Typ> : BoxedExpression
        {
            public BoxedExpression Array { get; private set; }
            public BoxedExpression Index { get; private set; }
            public readonly Typ Type;

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(Array != null);
                Contract.Invariant(Index != null);
            }

            public ArrayIndexExpression(BoxedExpression array, BoxedExpression index, Typ type)
            {
                Contract.Requires(array != null);
                Contract.Requires(index != null);

                this.Array = array;
                this.Index = index;
                this.Type = type;
            }

            #region System.Object overrides

            public override bool Equals(object obj)
            {
                if (this == obj) return true;
                ArrayIndexExpression<Typ> that = obj as ArrayIndexExpression<Typ>;
                if (that != null)
                {
                    return this.Array.Equals(that.Array) && this.Index.Equals(that.Index);
                }
                return false;
            }

            protected override int ComputeHashCode()
            {
                return this.Array.GetHashCode() + this.Index.GetHashCode();
            }

            public override string ToString()
            {
                return String.Format("{0}[{1}]", Array, Index);
            }
            #endregion

            public override bool IsArrayIndex
            {
                get
                {
                    return true;
                }
            }

            public override bool IsArrayIndexExpression(out BoxedExpression array, out BoxedExpression index, out object type)
            {
                array = this.Array;
                index = this.Index;
                type = this.Type;

                return true;
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                // TODO
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                var array = this.Array.Substitute(map);
                if (array == null) return null;
                var index = this.Index.Substitute(map);
                if (index == null) return null;
                if (this.Array == array && this.Index == index) return this;
                return new ArrayIndexExpression<Typ>(array, index, this.Type);
            }

            override public BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                var array = this.Array.Rename(renaming, refiner);
                if (array == null) return null;
                var index = this.Index.Rename(renaming, refiner);
                if (index == null) return null;
                if (this.Array == array && this.Index == index) return this;
                return new ArrayIndexExpression<Typ>(array, index, this.Type);
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(PC pc, Visitor visitor, Data data)
            {
                if (this.Type is Type)
                {
                    return visitor.Ldelem(pc, (Type)(object)this.Type, Unit.Value, Unit.Value, Unit.Value, data);
                }
                return visitor.Nop(pc, data);
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.ArrayIndex(this.Type, this.Array, this.Index, this);
            }

            public override BoxedExpression Negate()
            {
                return BoxedExpression.UnaryLogicalNot(this);
            }
        }

        [Serializable]
        public class StatementSequence : BoxedExpression
        {
            private readonly IIndexable<BoxedExpression> seq;

            public int Count { get { return seq.Count; } }
            public BoxedExpression this[int index]
            {
                get
                {
                    Contract.Requires(index >= 0);
                    Contract.Requires(index < Count);
                    return seq[index];
                }
            }

            public StatementSequence(IEnumerable<BoxedExpression> seq)
            {
                this.seq = seq.AsIndexable();
            }

            protected override int ComputeHashCode()
            {
                return seq.Count;
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                foreach (var b in seq.Enumerate()) { b.AddFreeVariables(set); }
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                return new StatementSequence(seq.Enumerate().AsIndexable(b => b.Substitute(map)));
            }

            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                var distinct = false;
                var belist = new List<BoxedExpression>();
                for (int i = 0; i < seq.Count; i++)
                {
                    var elem = seq[i];
                    Contract.Assume(elem != null);
                    var newElem = elem.Rename(renaming, refiner);
                    if (newElem == null) return null;
                    if (newElem != elem) { distinct = true; }
                    belist.Add(newElem);
                }
                if (!distinct) return this;
                return new StatementSequence(belist);
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(PC pc, Visitor visitor, Data data)
            {
                return visitor.Nop(pc, data);
            }

            public override bool Equals(object obj)
            {
                StatementSequence other = obj as StatementSequence;
                if (other == null) return false;
                return seq.Equals(other.seq);
            }

            public override string ToString()
            {
                return "StatementSequence";
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.StatementSequence(seq, this);
            }

            public override BoxedExpression Negate()
            {
                return new StatementSequence(seq.Enumerate().AsIndexable(b => b.Negate()));
            }
        }
        #endregion

        #region BoxedExpression proxy for deserialization
        [Serializable]
        protected class BoxedExpressionProxy : ISerializable, IObjectReference
        {
            private readonly BoxedExpression be;

            private BoxedExpressionProxy(SerializationInfo info, StreamingContext context)
            {
                be = (BoxedExpression)info.GetValue("BE", typeof(BoxedExpression));
            }

            object IObjectReference.GetRealObject(StreamingContext context)
            {
                return be;
            }

            void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
            {
                throw new NotSupportedException("Trying to serialize a BoxedExpressionProxy");
            }
        }
        #endregion

        public struct PC
        {
            public readonly BoxedExpression Node;
            public readonly int Index;

            public PC(BoxedExpression exp, int index)
            {
                Contract.Requires(exp != null);

                this.Node = exp;
                this.Index = index;
            }

            public override string ToString()
            {
                Contract.Assume(this.Node != null);
                return string.Format("PC({0}, {1})", this.Node.ToString(), Index);
            }
        }

        public static BoxedExpression SizeOf<Type>(Type type, int sizeAsConstant)
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ClousotExpression<Type>.SizeOfExpression(type, sizeAsConstant);
        }

        internal static BoxedExpression Sequence(IEnumerable<BoxedExpression> list)
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new BoxedExpression.StatementSequence(list);
        }

        internal static BoxedExpression ArrayIndex<Typ>(BoxedExpression arrayExpr, BoxedExpression indexExpr, Typ type)
        {
            Contract.Requires(arrayExpr != null);
            Contract.Requires(indexExpr != null);

            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return new ArrayIndexExpression<Typ>(arrayExpr, indexExpr, type);
        }


        private class BoxedExpressionsEqualityComparer : IEqualityComparer<BoxedExpression>
        {
            public bool Equals(BoxedExpression x, BoxedExpression y)
            {
                if (x == null)
                {
                    return y == null;
                }
                if (y == null)
                {
                    return x == null;
                }

                return x.Equals(y);
            }

            public int GetHashCode(BoxedExpression obj)
            {
                if (obj != null)
                    return obj.GetHashCode();

                return Int32.MinValue;
            }
        }

        private class BoxedExpressionKeyEqualityComparer : IEqualityComparer<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>>
        {
            public bool Equals(KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>> px, KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>> py)
            {
                var x = px.Key;
                var y = py.Key;

                if (x == null)
                {
                    return y == null;
                }
                if (y == null)
                {
                    return x == null;
                }

                return x.Equals(y);
            }

            public int GetHashCode(KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>> pobj)
            {
                var obj = pobj.Key;
                if (obj != null)
                    return obj.GetHashCode();

                return Int32.MinValue;
            }
        }
    }

    [ContractClassFor(typeof(BoxedExpression))]
    internal abstract class BoxedExpressionContracts
      : BoxedExpression
    {
        #region Contracts
        public override BoxedExpression Negate()
        {
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return null;
        }
        #endregion

        #region

        protected override int ComputeHashCode()
        {
            throw new NotImplementedException();
        }


        public override void AddFreeVariables(Set<BoxedExpression> set)
        {
            throw new NotImplementedException();
        }

        public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
        {
            throw new NotImplementedException();
        }

        public override void Dispatch(IBoxedExpressionVisitor visitor)
        {
            throw new NotImplementedException();
        }

        internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(BoxedExpression.PC pc, Visitor visitor, Data data)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    /// <summary>
    /// The bound variable is represented by a variable node whose variable is the contant int32 0.
    /// </summary>
    [ContractVerification(true)]
    [ContractClass(typeof(QuantifiedIndexedExpressionContracts))]
    [Serializable]
    public abstract class QuantifiedIndexedExpression
      : BoxedExpression
    {
        #region To be Overridden

        abstract protected string Name { get; }
        abstract protected BoxedExpression Factory(BoxedExpression boundVar, BoxedExpression lowerBound, BoxedExpression upperBound, BoxedExpression newBody, object var = null);

        #endregion

        #region Object invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.BoundVariable != null);
            Contract.Invariant(this.LowerBound != null);
            Contract.Invariant(this.UpperBound != null);
            Contract.Invariant(this.Body != null);
        }

        #endregion

        #region State

        public readonly BoxedExpression BoundVariable;
        public readonly BoxedExpression LowerBound;
        public readonly BoxedExpression UpperBound;
        public readonly BoxedExpression Body;

        private readonly object var;

        #endregion

        #region Constructor

        protected QuantifiedIndexedExpression(object value, BoxedExpression boundVariable, BoxedExpression lowerBound, BoxedExpression upperBound, BoxedExpression bodyDecoded)
        {
            Contract.Requires(lowerBound != null);
            Contract.Requires(upperBound != null);
            Contract.Requires(bodyDecoded != null);
            Contract.Requires(boundVariable != null);

            var = value;
            this.BoundVariable = boundVariable;
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
            this.Body = bodyDecoded;
        }

        #endregion

        #region Implementation of abstract methods

        public override string ToString()
        {
            return String.Format("Contract.{0}({1}, {2}, {3} => {4})", Name, LowerBound, UpperBound, BoundVariable, Body);
        }

        protected override int ComputeHashCode()
        {
            return BoundVariable.GetHashCode() + LowerBound.GetHashCode() + UpperBound.GetHashCode() + Body.GetHashCode();
        }

        public override void AddFreeVariables(Set<BoxedExpression> set)
        {
            // REVIEW: this looks wrong. It should compute the free variables and then remove the bound variable.
        }

        public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
        {
            var newLowerBound = this.LowerBound.Substitute(map);
            var newUpperBound = this.UpperBound.Substitute(map);
            // bound variable is not substituted, so modify the map for the recursion body

            Func<Variable, BoxedExpression, BoxedExpression> mapModified = (var, orig) => (orig == this.BoundVariable) ? orig : map(var, orig);

            var newBody = this.Body.Substitute(mapModified);

            if (newLowerBound == null || newUpperBound == null || newBody == null)
                return null;

            if (newLowerBound == this.LowerBound
              && newUpperBound == this.UpperBound && newBody == this.Body)
                return this;

            return this.Factory(this.BoundVariable, newLowerBound, newUpperBound, newBody);
        }

        public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
        {
            // if (this.BoundVariable == null) return null;
            // don't rename bound variables
            var newLowerBound = this.LowerBound.Rename(renaming, refiner);
            var newUpperBound = this.UpperBound.Rename(renaming, refiner);

            BoxedExpression newBody;

            Variable underlyingBound;
            if (this.BoundVariable.TryGetFrameworkVariable(out underlyingBound))
            {
                // have to remove it.
                var modifiedMap = renaming.Remove(underlyingBound);
                newBody = this.Body.Rename(modifiedMap, refiner);
            }
            else
            {
                newBody = this.Body.Rename(renaming, refiner);
            }
            if (newLowerBound == null || newUpperBound == null || newBody == null)
                return null;

            if (newLowerBound == this.LowerBound
              && newUpperBound == this.UpperBound && newBody == this.Body)
                return this;

            return this.Factory(this.BoundVariable, newLowerBound, newUpperBound, newBody);
        }
        public override BoxedExpression Substitute(BoxedExpression what, BoxedExpression newExp)
        {
            var boundVarSub = this.BoundVariable.Substitute(what, newExp);
            //     if (boundVarSub != null)
            {
                var lowerBoundSub = this.LowerBound.Substitute(what, newExp);
                //       if (lowerBoundSub != null)
                {
                    var upperBoundSub = this.UpperBound.Substitute(what, newExp);
                    //         if (upperBoundSub != null)
                    {
                        var bodySub = this.Body.Substitute(what, newExp);
                        return this.Factory(boundVarSub, lowerBoundSub, upperBoundSub, bodySub);
                    }
                }
            }
            //     return null;
        }

        public override void Dispatch(IBoxedExpressionVisitor visitor)
        {
            visitor.ForAll(this.BoundVariable, this.LowerBound, this.UpperBound, this.Body, this);
        }

        internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(BoxedExpression.PC pc, Visitor visitor, Data data)
        {
            return visitor.Nop(pc, data);
        }

        public override bool Equals(object obj)
        {
            var that = obj as QuantifiedIndexedExpression;
            if (that == null)
                return false;

            return this.Name.Equals(that.Name) &&
              this.BoundVariable.Equals(that.BoundVariable) && this.LowerBound.Equals(that.LowerBound)
              && this.UpperBound.Equals(that.UpperBound) && this.Body.Equals(that.Body);
        }

        #endregion

        #region Implementation of virtuals 

        public override bool IsQuantified
        {
            get
            {
                return true;
            }
        }

        public override bool IsQuantifiedExpression(out bool isForAll, out BoxedExpression boundVar, out BoxedExpression lower, out BoxedExpression upper, out BoxedExpression body)
        {
            isForAll = this is ForAllIndexedExpression;
            boundVar = this.BoundVariable;
            lower = this.LowerBound;
            upper = this.UpperBound;
            body = this.Body;

            return true;
        }

        public override object UnderlyingVariable
        {
            get
            {
                return var;
            }
        }

        #endregion
    }

    [ContractVerification(true)]
    [Serializable]
    public class ForAllIndexedExpression : QuantifiedIndexedExpression
    {
        public ForAllIndexedExpression(object value, BoxedExpression boundVariable, BoxedExpression lowerBound, BoxedExpression upperBound, BoxedExpression bodyDecoded)
          : base(value, boundVariable, lowerBound, upperBound, bodyDecoded)
        {
            Contract.Requires(boundVariable != null);
            Contract.Requires(lowerBound != null);
            Contract.Requires(upperBound != null);
            Contract.Requires(bodyDecoded != null);
        }

        protected override string Name
        {
            get { return "ForAll"; }
        }

        protected override BoxedExpression Factory(BoxedExpression boundVar, BoxedExpression lowerBound, BoxedExpression upperBound, BoxedExpression newBody, object variable = null)
        {
            return new ForAllIndexedExpression(variable, boundVar, lowerBound, upperBound, newBody);
        }

        public override BoxedExpression Negate()
        {
            return new ExistsIndexedExpression(null, this.BoundVariable, this.LowerBound, this.UpperBound, this.Body.Negate());
        }
    }

    [ContractVerification(true)]
    [Serializable]
    public class ExistsIndexedExpression : QuantifiedIndexedExpression
    {
        public ExistsIndexedExpression(object value, BoxedExpression boundVariable, BoxedExpression lowerBound, BoxedExpression upperBound, BoxedExpression bodyDecoded)
          : base(value, boundVariable, lowerBound, upperBound, bodyDecoded)
        {
            Contract.Requires(boundVariable != null);
            Contract.Requires(lowerBound != null);
            Contract.Requires(upperBound != null);
            Contract.Requires(bodyDecoded != null);
        }

        protected override string Name
        {
            get { return "Exists"; }
        }

        protected override BoxedExpression Factory(BoxedExpression boundVar, BoxedExpression lowerBound, BoxedExpression upperBound, BoxedExpression newBody, object variable = null)
        {
            return new ExistsIndexedExpression(variable, boundVar, lowerBound, upperBound, newBody);
        }

        public override BoxedExpression Negate()
        {
            return new ForAllIndexedExpression(null, this.BoundVariable, this.LowerBound, this.UpperBound, this.Body.Negate());
        }
    }

    #region Contracts for QuantifiedIndexedExpression
    [ContractClassFor(typeof(QuantifiedIndexedExpression))]
    internal abstract class QuantifiedIndexedExpressionContracts : QuantifiedIndexedExpression
    {
        protected override string Name
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);

                return default(string);
            }
        }

        protected override BoxedExpression Factory(BoxedExpression boundVar, BoxedExpression lowerBound, BoxedExpression upperBound, BoxedExpression newBody, object var = null)
        {
            Contract.Requires(boundVar != null);
            Contract.Requires(lowerBound != null);
            Contract.Requires(upperBound != null);
            Contract.Requires(newBody != null);

            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            return default(BoxedExpression);
        }

        // To make the complier happy
        private QuantifiedIndexedExpressionContracts(BoxedExpression boundVariable, BoxedExpression lowerBound, BoxedExpression upperBound, BoxedExpression bodyDecoded)
          : base(null, boundVariable, lowerBound, upperBound, bodyDecoded)
        {
        }
    }
    #endregion

    /// <summary>
    /// A ClousotExpression contains OR an expression created outside the analyzer (i.e. an ExternalExpression) OR an expression created by the analyzer (i.e. an InternalExpression)
    /// </summary>
    [Serializable]
    public abstract class ClousotExpression<Typ> : BoxedExpression
    {
        #region Factories

        public static BoxedExpression MakeIsInst(Typ type, BoxedExpression arg)
        {
            Contract.Requires(arg != null);

            return new IsInstExpression(arg, type, null);
        }

        public static BoxedExpression MakeSizeOf(Typ type, int sizeAsConst)
        {
            return new SizeOfExpression(type, sizeAsConst);
        }

        public static BoxedExpression MakeUnary(UnaryOperator op, BoxedExpression arg)
        {
            Contract.Requires(arg != null);

            return new UnaryExpression(op, arg, null);
        }

        internal static bool TryConvert<Variable, ExternalExpression>(ExternalExpression exp, IFullExpressionDecoder<Typ, Variable, ExternalExpression> decoder,
          int maxdepth, bool trysimplify, bool replaceConstants, bool replaceNull, Func<Variable, FList<PathElement>> accessPaths, out BoxedExpression result)
          where ExternalExpression : IEquatable<ExternalExpression>
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);

            if (maxdepth <= 0)
            {
                result = null;
                return false;
            }

            try
            {
                object constant, variable;
                Typ type;
                UnaryOperator uop;
                BinaryOperator bop;
                BoxedExpression convertedLeft, convertedRight;
                ExternalExpression uarg, left, right, associatedInfo;

                var frameworkVar = decoder.UnderlyingVariable(exp);

                if (decoder.IsConstant(exp, out constant, out type))
                {
                    result = replaceConstants ? new ConstantExpression(constant, type, frameworkVar) :
                      BoxedExpression.Var(frameworkVar, accessPaths != null ? accessPaths(frameworkVar) : null);
                    return true;
                }

                // We want to check if it is a variable before checking if it is null, to avoid the too smartness of the heap analysis
                if (decoder.IsVariable(exp, out variable))
                {
                    var path = decoder.GetVariableAccessPath(exp);
                    if (decoder.TryGetAssociatedExpression(exp, AssociatedInfo.WritableBytes, out associatedInfo))
                    {
                        result = new VariableExpression(variable, path, BoxedExpression.For(associatedInfo, decoder));
                    }
                    else
                    {
                        object expType;
                        if (decoder.TryGetType(exp, out expType))
                        {
                            // we are happy
                        }
                        else
                        {
                            expType = null; // make sure we have no garbage
                        }
                        result = new VariableExpression(variable, path, expType);
                    }
                    return true;
                }
                if (replaceNull && decoder.IsNull(exp))
                {
                    result = new ConstantExpression(null, default(Typ));
                    return true;
                }
                if (decoder.IsSizeOf(exp, out type))
                {
                    int value;
                    if (decoder.TrySizeOfAsConstant(exp, out value))
                    {
                        result = new SizeOfExpression(type, value, frameworkVar);
                    }
                    else
                    {
                        result = new SizeOfExpression(type, frameworkVar);
                    }
                    return true;
                }
                else if (decoder.IsInst(exp, out uarg, out type))
                {
                    if (TryConvert(uarg, decoder, maxdepth - 1, trysimplify, replaceConstants, replaceNull, accessPaths, out convertedLeft))
                    {
                        result = new IsInstExpression(convertedLeft, type, frameworkVar);
                        return true;
                    }
                    else
                    {
                        result = null;
                        return false;
                    }
                }
                else if (decoder.IsUnaryOperator(exp, out uop, out uarg))
                {
                    if (TryConvert(uarg, decoder, maxdepth - 1, trysimplify, replaceConstants, replaceNull, accessPaths, out convertedLeft))
                    {
                        // Get rid of the annoying cast "(int32)uarg"  when we know that uarg is of type int32
                        object uargType;
                        if (uop == UnaryOperator.Conv_i4
                          && convertedLeft.IsVariable  // HACKHACK!!! if converted left is an expression of wider type, the framework believes it is of the type of the cast. Example (int) ((long)y + (long) t)
                          && decoder.TryGetType(uarg, out uargType) && decoder.System_Int32.Equals(uargType))
                        {
                            result = convertedLeft;
                        }
                        else if (uop == UnaryOperator.Not && decoder.TryGetType(uarg, out uargType) && decoder.IsReferenceType((Typ)uargType))
                        {
                            // turn into == null unless it is (x as ValueType)
                            BoxedExpression isinstArg;
                            object isinstType;
                            if (convertedLeft.IsIsInstExpression(out isinstArg, out isinstType))
                            {
                                if (isinstType is Typ && !decoder.IsReferenceType((Typ)isinstType))
                                {
                                    result = new UnaryExpression(uop, convertedLeft, frameworkVar);
                                    return true;
                                }
                            }
                            result = new BinaryExpression(BinaryOperator.Ceq, convertedLeft, new ConstantExpression(null, (Typ)uargType));
                        }
                        else
                        {
                            // HACK HACK: We cannot apply conversion to a null paramameter
                            if (uop.IsConversionOperator() && convertedLeft.IsConstant)
                            {
                                if (convertedLeft.Constant == null)
                                {
                                    result = null;
                                    return false;
                                }
                            }

                            result = new UnaryExpression(uop, convertedLeft, frameworkVar);
                        }
                        return true;
                    }
                    else
                    {
                        result = null;
                        return false;
                    }
                }
                else if (decoder.IsBinaryOperator(exp, out bop, out left, out right))
                {
                    if (trysimplify && (bop == BinaryOperator.Ceq || bop == BinaryOperator.Cobjeq) && decoder.IsNull(left) && decoder.IsNull(right))
                    {
                        result = new ConstantExpression(1, decoder.System_Int32);
                        return true;
                    }

                    if (TryConvert(left, decoder, maxdepth - 1, trysimplify, replaceConstants, replaceNull, accessPaths, out convertedLeft)
                      && TryConvert(right, decoder, maxdepth - 1, trysimplify, replaceConstants, replaceNull, accessPaths, out convertedRight))
                    {
                        result = new BinaryExpression(bop, convertedLeft, convertedRight, frameworkVar);
                        return true;
                    }

                    result = null;
                    return false;
                }
                else
                {
                    Contract.Assume(false, "Type of expression unknown!");

                    throw new NotImplementedException();
                }
            }
            catch (Exception)
            {
                // something can go wrong while trying to convert. In this case we just give up
                result = null;
                return false;
            }
        }
        #endregion

        #region CastExpression (embedding the real expression)
        [Serializable]
        internal class CastExpression : ClousotExpression<Typ>
        {
            public readonly Typ mTypeCasting;
            public readonly BoxedExpression mEmbeddedExpression;
            public readonly bool mbUnchecked;
            private readonly string expressionName;

            public override bool IsCast
            {
                get
                {
                    return true;
                }
            }

            public CastExpression(BoxedExpression embedded_exp, string/*?*/ expname, Typ type_casting, bool mbUnchecked)
            {
                this.mEmbeddedExpression = embedded_exp;
                expressionName = expname;
                this.mTypeCasting = type_casting;
                this.mbUnchecked = mbUnchecked;
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                mEmbeddedExpression.AddFreeVariables(set);
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                return mEmbeddedExpression.Substitute(map); // the cast is lost?? REVIEW: this looks bad
            }

            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                var newEmbedded = mEmbeddedExpression.Rename(renaming, refiner);
                if (newEmbedded == null) return null;
                if (newEmbedded == this.mEmbeddedExpression) return this;
                return new CastExpression(newEmbedded, expressionName, this.mTypeCasting, this.mbUnchecked);
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(PC pc, Visitor visitor, Data data)
            {
                return mEmbeddedExpression.ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(pc, visitor, data);
            }

            public override string ToString()
            {
                string format;

                if (!mbUnchecked && expressionName != null)
                {
                    return string.Format("{0}.{1}", mTypeCasting.ToString().Replace('+', '.'), expressionName);
                }

                if (!mbUnchecked)
                    format = "(({0})({1}))";
                else
                    format = "(unchecked({0})({1}))";

                return string.Format(format, mTypeCasting.ToString().Replace('+', '.'), mEmbeddedExpression.ToString());
            }

            public override string ToString<Typ2>(Converter<Typ2, string> converter)
            {
                Contract.Assume(converter != null);

                if (mTypeCasting is Typ2)
                {
                    string format;
                    if (!mbUnchecked)
                        format = "(({0})({1}))";
                    else
                        format = "(unchecked({0})({1}))";
                    return string.Format(format, converter((Typ2)(object)mTypeCasting), mEmbeddedExpression.ToString(converter));
                }
                else
                    return ToString();
            }

            public override PathElement[] AccessPath
            {
                get
                {
                    return mEmbeddedExpression.AccessPath;
                }
            }

            public override BoxedExpression BinaryLeft
            {
                get
                {
                    return mEmbeddedExpression.BinaryLeft;
                }
            }

            public override BinaryOperator BinaryOp
            {
                get
                {
                    return mEmbeddedExpression.BinaryOp;
                }
            }

            public override BoxedExpression BinaryRight
            {
                get
                {
                    return mEmbeddedExpression.BinaryRight;
                }
            }

            public override object Constant
            {
                get
                {
                    return mEmbeddedExpression.Constant;
                }
            }

            public override object ConstantType
            {
                get
                {
                    return mEmbeddedExpression.ConstantType;
                }
            }

            public override bool IsBinary
            {
                get
                {
                    return mEmbeddedExpression.IsBinary;
                }
            }

            public override bool IsBinaryExpression(out BinaryOperator bop, out BoxedExpression left, out BoxedExpression right)
            {
                return mEmbeddedExpression.IsBinaryExpression(out bop, out left, out right);
            }

            public override bool IsBooleanTyped
            {
                get
                {
                    return mEmbeddedExpression.IsBooleanTyped;
                }
            }

            public override bool IsConstant
            {
                get
                {
                    return mEmbeddedExpression.IsConstant;
                }
            }

            public override bool IsIsInst
            {
                get
                {
                    return mEmbeddedExpression.IsIsInst;
                }
            }

            public override bool IsIsInstExpression(out BoxedExpression exp, out object type)
            {
                return mEmbeddedExpression.IsIsInstExpression(out exp, out type);
            }

            public override bool IsNull
            {
                get
                {
                    return mEmbeddedExpression.IsNull;
                }
            }

            public override bool IsResult
            {
                get
                {
                    return mEmbeddedExpression.IsResult;
                }
            }

            public override bool IsSizeOf
            {
                get
                {
                    return mEmbeddedExpression.IsSizeOf;
                }
            }

            public override bool IsUnary
            {
                get
                {
                    return mEmbeddedExpression.IsUnary;
                }
            }

            public override bool IsVariable
            {
                get
                {
                    return mEmbeddedExpression.IsVariable;
                }
            }

            protected override BoxedExpression RecursiveSubstitute(BoxedExpression what, BoxedExpression newExp)
            {
                return ((ClousotExpression<Typ>)mEmbeddedExpression).RecursiveSubstitute(what, newExp);
            }

            public override bool SizeOf(out int size)
            {
                return mEmbeddedExpression.SizeOf(out size);
            }

            public override bool SizeOf(out object type, out int size)
            {
                return mEmbeddedExpression.SizeOf(out type, out size);
            }

            public override BoxedExpression Substitute(BoxedExpression what, BoxedExpression newExp)
            {
                return mEmbeddedExpression.Substitute(what, newExp);
            }

            public override bool TryGetAssociatedInfo(APC atPC, AssociatedInfo infoKind, out BoxedExpression info)
            {
                return mEmbeddedExpression.TryGetAssociatedInfo(atPC, infoKind, out info);
            }

            public override bool TryGetAssociatedInfo(AssociatedInfo infoKind, out BoxedExpression info)
            {
                return mEmbeddedExpression.TryGetAssociatedInfo(infoKind, out info);
            }

            public override bool TryGetType(out object type)
            {
                return mEmbeddedExpression.TryGetType(out type);
            }

            public override BoxedExpression UnaryArgument
            {
                get
                {
                    return mEmbeddedExpression.UnaryArgument;
                }
            }

            public override UnaryOperator UnaryOp
            {
                get
                {
                    return mEmbeddedExpression.UnaryOp;
                }
            }

            public override object UnderlyingVariable
            {
                get
                {
                    return mEmbeddedExpression.UnderlyingVariable;
                }
            }

            protected override int ComputeHashCode()
            {
                if (this.mEmbeddedExpression == null) return 1;
                return this.mEmbeddedExpression.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is CastExpression)
                {
                    CastExpression ce = obj as CastExpression;
                    return this.mEmbeddedExpression == ce.mEmbeddedExpression
                      && this.mbUnchecked == ce.mbUnchecked
                      && this.mTypeCasting.Equals(ce.mTypeCasting);
                }
                return false;
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                if (this.mEmbeddedExpression == null) return;
                this.mEmbeddedExpression.Dispatch(visitor);
            }

            public override BoxedExpression Negate()
            {
                return BoxedExpression.Unary(UnaryOperator.Not, this);
            }
        }
        #endregion

        #region Actual expression types

        [Serializable]
        internal class ConstantExpression : ClousotExpression<Typ>
        {
            public readonly object Value;
            public readonly Typ Type;
            private readonly bool isBoolean;     // True if we are sure that we have a int value that represents a Boolean 
                                                 // False means that we do not know

            [NonSerialized]
            private readonly object frameworkVar;

            public ConstantExpression(object value, Typ type, object frameworkVar = null)
            {
                this.Value = value;
                this.Type = type;
                isBoolean = false;
                this.frameworkVar = frameworkVar;
            }

            public ConstantExpression(object value, Typ type, bool isBoolean, object frameworkVar = null)
            {
                this.Value = value;
                this.Type = type;
                this.isBoolean = isBoolean;
                this.frameworkVar = frameworkVar;
            }

            public override bool IsConstant { get { return true; } }
            public override object Constant { get { return this.Value; } }
            public override object UnderlyingVariable
            {
                get
                {
                    return frameworkVar;
                }
            }

            public override object ConstantType
            {
                get
                {
                    return this.Type;
                }
            }

            public override bool IsNull
            {
                get
                {
                    if (Value == null) return true;
                    IConvertible ic = this.Value as IConvertible;
                    if (ic != null && !(this.Value is string))
                    {
                        try
                        {
                            if (ic.ToInt32(null) == 0) return true;
                        }
                        catch
                        { // this is the case if Value is a double, or a Int64, etc
                            return false;
                        }
                    }
                    return false;
                }
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type2, Visitor>(BoxedExpression.PC pc, Visitor visitor, Data data)
            {
                if (this.Value == null)
                {
                    return visitor.Ldnull(pc, Unit.Value, data);
                }
                var that = (ClousotExpression<Type2>.ConstantExpression)(object)this;

                return visitor.Ldconst(pc, Value, that.Type, Unit.Value, data);
            }

            #region System.Object overrides
            public override bool Equals(object obj)
            {
                if (this == obj) return true;
                BoxedExpression that = obj as BoxedExpression;
                if (that != null && that.IsConstant)
                {
                    if (this.Value == that.Constant) return true;
                    if (this.Value == null) return false;
                    return this.Value.Equals(that.Constant);
                }

                return false;
            }

            protected override int ComputeHashCode()
            {
                if (Value == null) return 1;
                return this.Value.GetHashCode();
            }

            public override string ToString()
            {
                return this.ToStringInternal();
            }

            private string ToStringInternal()
            {
                if (this.Value == null)
                {
                    return "null";
                }
                // Special case here to return legal C# constants
                else if (this.Value is bool)
                {
                    return (bool)this.Value ? "true" : "false";
                }
                else if (isBoolean && this.Value is Int32)
                {
                    var v = (Int32)this.Value;
                    return v != 0 ? "true" : "false";
                }
                // Pretty printing for MinInt and MaxInt
                else if (this.Value is Int32)
                {
                    switch ((Int32)this.Value)
                    {
                        case Int32.MaxValue:
                            return "Int32.MaxValue";
                        case Int32.MinValue:
                            return "Int32.MinValue";
                        default:
                            return this.Value.ToString();
                    }
                }
                else if (this.Value is string)
                {
                    string str = this.Value as string;
                    return "@\"" + str.Replace("\"", "\"\"") + "\""; // correctly escape the characters
                }
                else
                {
                    return this.Value.ToString();
                }
            }
            #endregion

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                return this;
            }
            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                return this;
            }
            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.Constant<Typ>(this.Type, this.Value, this);
            }

            public override BoxedExpression Negate()
            {
                if (Value is Int32)
                {
                    int v = (Int32)Value;

                    if (v == 0)
                        return new ClousotExpression<Typ>.ConstantExpression(1, this.Type);
                    else
                        return new ClousotExpression<Typ>.ConstantExpression(0, this.Type);
                }

                return BoxedExpression.UnaryLogicalNot(this);
            }
        }

        [Serializable]
        internal class SizeOfExpression : ClousotExpression<Typ>
        {
            public readonly Typ Type;
            public readonly int sizeAsConstant;

            [NonSerialized]
            private readonly object frameworkVar;
            public override object UnderlyingVariable
            {
                get
                {
                    return frameworkVar;
                }
            }

            public SizeOfExpression(Typ type, int sizeAsConstant, object frameworkVar)
            {
                this.Type = type;
                this.sizeAsConstant = sizeAsConstant;
                this.frameworkVar = frameworkVar;
            }

            public SizeOfExpression(Typ type, object frameworkVar) : this(type, -1, frameworkVar) { }

            public override bool IsSizeOf { get { return true; } }

            public override bool SizeOf(out int size)
            {
                size = this.sizeAsConstant;
                return (size >= 0);
            }

            public override bool SizeOf(out object type, out int size)
            {
                type = this.Type;
                size = this.sizeAsConstant;
                return (size >= 0);
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
            }


            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type2, Visitor>(BoxedExpression.PC pc, Visitor visitor, Data data)
            {
                ClousotExpression<Type2>.SizeOfExpression that = (ClousotExpression<Type2>.SizeOfExpression)(object)this;
                return visitor.Sizeof(pc, that.Type, Unit.Value, data);
            }


            #region System.Object overrides
            public override bool Equals(object obj)
            {
                if (this == obj) return true;

                ClousotExpression<Typ>.SizeOfExpression that = obj as ClousotExpression<Typ>.SizeOfExpression;
                if (that == null) return false;

                return this.Type.Equals(that.Type);
            }

            protected override int ComputeHashCode()
            {
                return this.Type.GetHashCode();
            }

            public override string ToString()
            {
                return String.Format("sizeof({0})", this.Type.ToString());
            }

            public override string ToString<Typ2>(Converter<Typ2, string> converter)
            {
                Contract.Assume(converter != null);

                if (this.Type is Typ2)
                    return String.Format("sizeof({0})", converter((Typ2)(object)this.Type));
                return ToString();
            }
            #endregion


            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                return this;
            }
            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                return this;
            }
            public override BoxedExpression Negate()
            {
                return BoxedExpression.UnaryLogicalNot(this);
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.SizeOf<Typ>(this.Type, this.sizeAsConstant, this);
            }
        }

        [Serializable]
        public class IsInstExpression : ClousotExpression<Typ>
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(arg != null);
            }


            private readonly BoxedExpression arg;
            private readonly Typ type;

            [NonSerialized]
            private readonly object frameworkVar;
            public override object UnderlyingVariable
            {
                get
                {
                    return frameworkVar;
                }
            }

            public IsInstExpression(BoxedExpression arg, Typ type, object frameworkVar)
            {
                Contract.Requires(arg != null);

                this.arg = arg;
                this.type = type;
                this.frameworkVar = frameworkVar;
            }

            public override bool IsIsInst
            {
                get
                {
                    return true;
                }
            }

            public override bool IsIsInstExpression(out BoxedExpression exp, out object type)
            {
                exp = arg;
                type = this.type;

                return true;
            }

            public override BoxedExpression UnaryArgument
            {
                get
                {
                    return arg;
                }
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type2, Visitor>(BoxedExpression.PC pc, Visitor visitor, Data data)
            {
                ClousotExpression<Type2>.IsInstExpression that = (ClousotExpression<Type2>.IsInstExpression)(object)this;

                return visitor.Isinst(pc, that.type, Unit.Value, Unit.Value, data);
            }

            protected override int ComputeHashCode()
            {
                return arg.GetHashCode();
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                arg.AddFreeVariables(set);
            }

            public override bool Equals(object obj)
            {
                ClousotExpression<Typ>.IsInstExpression that = obj as ClousotExpression<Typ>.IsInstExpression;
                if (that == null) return false;
                return arg.Equals(that.arg) && type.Equals(that.type);
            }

            public override string ToString()
            {
                return String.Format("({0} as {1})", arg.ToString(), type.ToString());
            }

            public override string ToString<Typ2>(Converter<Typ2, string> converter)
            {
                Contract.Assume(converter != null);

                if (type is Typ2)
                    return String.Format("({0} as {1})", arg.ToString(), converter((Typ2)(object)type));
                else
                    return ToString();
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                var arg = this.arg.Substitute(map);
                if (arg == this.arg) return this;
                if (arg == null) return null;
                return new IsInstExpression(arg, type, null);
            }

            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                var arg = this.arg.Rename(renaming, refiner);
                if (arg == this.arg) return this;
                if (arg == null) return null;
                return new IsInstExpression(arg, type, null);
            }
            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.IsInst<Typ>(type, arg, this);
            }

            public override BoxedExpression Negate()
            {
                return BoxedExpression.UnaryLogicalNot(this);
            }
        }

        [Serializable]
        internal class ResultExpression : ClousotExpression<Typ>
        {
            private const string ContractResultTemplate = "Contract.Result<{0}>()";

            public readonly Typ type;

            public ResultExpression(Typ type)
            {
                this.type = type;
            }

            public override bool IsResult
            {
                get
                {
                    return true;
                }
            }

            public override string ToString()
            {
                return string.Format(ContractResultTemplate, type.ToString());
                //return string.Format(ContractResultTemplate, type.ToString());
                //return string.Format(ContractResultTemplate, OutputPrettyCS.TypeHelper.TypeFullName());
            }

            public override string ToString<Typ2>(Converter<Typ2, string> converter)
            {
                Contract.Assume(converter != null);

                if (type is Typ2)
                    return string.Format(ContractResultTemplate, converter((Typ2)(object)type));
                else
                    return ToString();
            }

            protected override int ComputeHashCode()
            {
                return type.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                ResultExpression other = obj as ResultExpression;
                if (other == null) return false;
                return this.type.Equals(other.type);
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                return this;
            }
            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                return this;
            }
            [ContractVerification(false)]
            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(PC pc, Visitor visitor, Data data)
            {
                return visitor.Ldresult(pc, (Type)(object)this.type, Unit.Value, Unit.Value, data);
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.Result<Typ>(this.type, this);
            }

            public override BoxedExpression Negate()
            {
                return BoxedExpression.UnaryLogicalNot(this);
            }
        }

        [Serializable]
        internal class OldExpression : ClousotExpression<Typ>
        {
            private const string FoxTrotOld = "Contract.OldValue({0})";

            public readonly BoxedExpression Old;
            public readonly Typ Type;

            public OldExpression(BoxedExpression old, Typ typ)
            {
                this.Old = old;
                this.Type = typ;
            }

            public OldExpression(BoxedExpression old, Typ typ, bool mbOut)
            {
                this.Old = old;
                this.Type = typ;
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                this.Old.AddFreeVariables(set);
            }

            protected override int ComputeHashCode()
            {
                return Old.GetHashCode() * 2 + 1;
            }

            public override bool Equals(object obj)
            {
                OldExpression that = obj as OldExpression;
                if (that == null) return false;
                return this.Old.Equals(that.Old);
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                var old = this.Old.Substitute(map);
                if (old == this.Old) return this;
                if (old == null) return null;
                return new OldExpression(old, this.Type);
            }

            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                var old = this.Old.Rename(renaming, refiner);
                if (old == this.Old) return this;
                if (old == null) return null;
                return new OldExpression(old, this.Type);
            }

            #region The Old expression is "transparent", so everything gets forwarded to the embedded expression
            public override PathElement[] AccessPath
            {
                get
                {
                    return this.Old.AccessPath;
                }
            }

            public override BoxedExpression BinaryLeft
            {
                get
                {
                    return this.Old.BinaryLeft;
                }
            }

            public override BinaryOperator BinaryOp
            {
                get
                {
                    return this.Old.BinaryOp;
                }
            }
            public override BoxedExpression BinaryRight
            {
                get
                {
                    return this.Old.BinaryRight;
                }
            }

            public override object Constant
            {
                get
                {
                    return this.Old.Constant;
                }
            }

            public override object ConstantType
            {
                get
                {
                    return this.Old.ConstantType;
                }
            }

            public override bool IsBinary
            {
                get
                {
                    return this.Old.IsBinary;
                }
            }

            public override bool IsBinaryExpression(out BinaryOperator bop, out BoxedExpression left, out BoxedExpression right)
            {
                return this.Old.IsBinaryExpression(out bop, out left, out right);
            }


            public override bool IsConstant
            {
                get
                {
                    return this.Old.IsConstant;
                }
            }

            public override bool IsIsInst
            {
                get
                {
                    return this.Old.IsIsInst;
                }
            }

            public override bool IsIsInstExpression(out BoxedExpression exp, out object type)
            {
                return this.Old.IsIsInstExpression(out exp, out type);
            }

            public override bool IsNull
            {
                get
                {
                    return this.Old.IsNull;
                }
            }

            public override bool IsSizeOf
            {
                get
                {
                    return this.Old.IsSizeOf;
                }
            }

            public override bool IsUnary
            {
                get
                {
                    return this.Old.IsUnary;
                }
            }

            public override bool IsVariable
            {
                get
                {
                    return this.Old.IsVariable;
                }
            }

            public override bool SizeOf(out int size)
            {
                return this.Old.SizeOf(out size);
            }


            public override BoxedExpression UnaryArgument
            {
                get
                {
                    return this.Old.UnaryArgument;
                }
            }
            public override UnaryOperator UnaryOp
            {
                get
                {
                    return this.Old.UnaryOp;
                }
            }

            //public override object Variable
            //{
            //  get
            //  {
            //    return this.Old.Variable;
            //  }
            //}

            public override object UnderlyingVariable
            {
                get
                {
                    return this.Old.UnderlyingVariable;
                }
            }

            public override bool SizeOf(out object type, out int size)
            {
                return this.Old.SizeOf(out type, out size);
            }

            #endregion

            public override string ToString()
            {
                return String.Format(FoxTrotOld, Old.ToString());
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(PC pc, Visitor visitor, Data data)
            {
                ClousotExpression<Type>.OldExpression that = (ClousotExpression<Type>.OldExpression)(object)this;
                // this represents EndOld
                return visitor.EndOld(pc, new PC(pc.Node, 0), that.Type, Unit.Value, Unit.Value, data);
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.Old<Typ>(this.Type, this.Old, this);
            }

            public override BoxedExpression Negate()
            {
                return BoxedExpression.UnaryLogicalNot(this);
            }
        }

        [Serializable]
        internal class ValueAtReturnExpression : ClousotExpression<Typ>
        {
            private const string FoxTrotValueAtReturn = "Contract.ValueAtReturn(out {0})";

            public readonly BoxedExpression Value;
            public readonly Typ Type;

            public ValueAtReturnExpression(BoxedExpression value, Typ typ)
            {
                this.Value = value;
                this.Type = typ;
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                this.Value.AddFreeVariables(set);
            }

            protected override int ComputeHashCode()
            {
                return Value.GetHashCode() * 2 + 1;
            }

            public override bool Equals(object obj)
            {
                OldExpression that = obj as OldExpression;
                if (that == null) return false;
                return this.Value.Equals(that.Old);
            }

            public override BoxedExpression Substitute<Variable>(Func<Variable, BoxedExpression, BoxedExpression> map)
            {
                var val = this.Value.Substitute(map);
                if (val == this.Value) return this;
                if (val == null) return null;
                return new ValueAtReturnExpression(val, this.Type);
            }

            public override BoxedExpression Rename<Variable>(IFunctionalMap<Variable, Variable> renaming, Func<Variable, BoxedExpression> refiner = null)
            {
                var val = this.Value.Rename(renaming, refiner);
                if (val == this.Value) return this;
                if (val == null) return null;
                return new ValueAtReturnExpression(val, this.Type);
            }
            #region The Old expression is "transparent", so everything gets forwarded to the embedded expression
            public override PathElement[] AccessPath
            {
                get
                {
                    return this.Value.AccessPath;
                }
            }

            public override BoxedExpression BinaryLeft
            {
                get
                {
                    return this.Value.BinaryLeft;
                }
            }

            public override BinaryOperator BinaryOp
            {
                get
                {
                    return this.Value.BinaryOp;
                }
            }
            public override BoxedExpression BinaryRight
            {
                get
                {
                    return this.Value.BinaryRight;
                }
            }

            public override object Constant
            {
                get
                {
                    return this.Value.Constant;
                }
            }

            public override object ConstantType
            {
                get
                {
                    return this.Value.ConstantType;
                }
            }

            public override bool IsBinary
            {
                get
                {
                    return this.Value.IsBinary;
                }
            }

            public override bool IsBinaryExpression(out BinaryOperator bop, out BoxedExpression left, out BoxedExpression right)
            {
                return this.Value.IsBinaryExpression(out bop, out left, out right);
            }


            public override bool IsConstant
            {
                get
                {
                    return this.Value.IsConstant;
                }
            }

            public override bool IsIsInst
            {
                get
                {
                    return this.Value.IsIsInst;
                }
            }

            public override bool IsIsInstExpression(out BoxedExpression exp, out object type)
            {
                return this.Value.IsIsInstExpression(out exp, out type);
            }

            public override bool IsNull
            {
                get
                {
                    return this.Value.IsNull;
                }
            }

            public override bool IsSizeOf
            {
                get
                {
                    return this.Value.IsSizeOf;
                }
            }

            public override bool IsUnary
            {
                get
                {
                    return this.Value.IsUnary;
                }
            }

            public override bool IsVariable
            {
                get
                {
                    return this.Value.IsVariable;
                }
            }

            public override bool SizeOf(out int size)
            {
                return this.Value.SizeOf(out size);
            }


            public override BoxedExpression UnaryArgument
            {
                get
                {
                    return this.Value.UnaryArgument;
                }
            }
            public override UnaryOperator UnaryOp
            {
                get
                {
                    return this.Value.UnaryOp;
                }
            }

            //public override object Variable
            //{
            //  get
            //  {
            //    return this.Value.Variable;
            //  }
            //}

            public override object UnderlyingVariable
            {
                get
                {
                    return this.Value.UnderlyingVariable;
                }
            }

            public override bool SizeOf(out object type, out int size)
            {
                return this.Value.SizeOf(out type, out size);
            }

            #endregion

            public override string ToString()
            {
                return String.Format(FoxTrotValueAtReturn, Value.ToString());
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(PC pc, Visitor visitor, Data data)
            {
                ClousotExpression<Type>.ValueAtReturnExpression that = (ClousotExpression<Type>.ValueAtReturnExpression)(object)this;
                // this represents ldind
                return visitor.Ldind(pc, that.Type, false, Unit.Value, Unit.Value, data);
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                visitor.ValueAtReturn<Typ>(this.Type, this.Value, this);
            }

            public override BoxedExpression Negate()
            {
                return BoxedExpression.UnaryLogicalNot(this);
            }
        }

        [Serializable]
        internal class ExternalBox<Variable, ExternalExpression> : ClousotExpression<Typ>, ISerializable
          where ExternalExpression : IEquatable<ExternalExpression>
        {
            private ExternalExpression exp;
            private IFullExpressionDecoder<Typ, Variable, ExternalExpression> decoder;

            #region Caching

            private Optional<object> var;
            private Optional<Pair<bool, object>> type;
            private Optional<Pair<bool, Object>> isVar;
            private Optional<Microsoft.Research.DataStructures.STuple<bool, object, Typ>> constant;
            private Optional<Microsoft.Research.DataStructures.STuple<bool, BoxedExpression, Typ>> isInst;
            private Optional<Microsoft.Research.DataStructures.STuple<bool, UnaryOperator, BoxedExpression>> unary;
            private Optional<Microsoft.Research.DataStructures.STuple<bool, BinaryOperator, BoxedExpression, BoxedExpression>> binary;

            #endregion

            #region ISerializable members
            void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.SetType(typeof(BoxedExpressionProxy));
                info.AddValue("BE", BoxedExpression.Convert(exp, decoder, Int32.MaxValue));
            }
            #endregion

            public ExternalBox(ExternalExpression exp, IFullExpressionDecoder<Typ, Variable, ExternalExpression> decoder)
            {
                this.exp = exp;
                this.decoder = decoder;
            }

            public override bool IsBinary
            {
                get
                {
                    if (binary.IsValid)
                    {
                        return binary.Value.One;
                    }

                    BinaryOperator op;
                    ExternalExpression left, right;
                    var res = decoder.IsBinaryOperator(exp, out op, out left, out right);

                    binary = new Microsoft.Research.DataStructures.STuple<bool, BinaryOperator, BoxedExpression, BoxedExpression>
                       (res, op, BoxedExpression.For(left, decoder), BoxedExpression.For(right, decoder));

                    return res;
                }
            }

            [ContractVerification(false)]
            public override bool IsBinaryExpression(out BinaryOperator bop, out BoxedExpression left, out BoxedExpression right)
            {
                if (binary.IsValid)
                {
                    bop = binary.Value.Two;
                    left = binary.Value.Three;
                    right = binary.Value.Four;

                    return binary.Value.One;
                }

                ExternalExpression eleft, eright;
                if (decoder.IsBinaryOperator(exp, out bop, out eleft, out eright))
                {
                    left = BoxedExpression.For(eleft, decoder);
                    right = BoxedExpression.For(eright, decoder);

                    binary = new Microsoft.Research.DataStructures.STuple<bool, BinaryOperator, BoxedExpression, BoxedExpression>(true, bop, left, right);

                    return true;
                }


                left = null;
                right = null;

                binary = new Microsoft.Research.DataStructures.STuple<bool, BinaryOperator, BoxedExpression, BoxedExpression>(false, bop, left, right);
                return false;
            }

            public override BinaryOperator BinaryOp
            {
                get
                {
                    if (binary.IsValid)
                    {
                        return binary.Value.Two;
                    }

                    BinaryOperator bop;
                    ExternalExpression left, right;
                    if (decoder.IsBinaryOperator(exp, out bop, out left, out right))
                    {
                        binary = new Microsoft.Research.DataStructures.STuple<bool, BinaryOperator, BoxedExpression, BoxedExpression>
                          (true, bop, BoxedExpression.For(left, decoder), BoxedExpression.For(right, decoder));

                        return bop;
                    }
                    throw new InvalidOperationException();
                }
            }

            public override BoxedExpression BinaryLeft
            {
                get
                {
                    if (binary.IsValid)
                    {
                        Contract.Assume(binary.Value.Three != null);
                        return binary.Value.Three;
                    }

                    BinaryOperator bop;
                    ExternalExpression left, right;
                    if (decoder.IsBinaryOperator(exp, out bop, out left, out right))
                    {
                        return BoxedExpression.For(left, decoder);
                    }
                    throw new InvalidOperationException();
                }
            }

            public override BoxedExpression BinaryRight
            {
                get
                {
                    if (binary.IsValid)
                    {
                        Contract.Assume(binary.Value.Four != null);

                        return binary.Value.Four;
                    }

                    BinaryOperator bop;
                    ExternalExpression left, right;
                    if (decoder.IsBinaryOperator(exp, out bop, out left, out right))
                    {
                        return BoxedExpression.For(right, decoder);
                    }
                    throw new InvalidOperationException();
                }
            }

            public override bool IsConstant
            {
                get
                {
                    if (constant.IsValid)
                    {
                        return constant.Value.One;
                    }

                    try // We are catching it because the underlying decoder may fail in some weird cases when this.exp is not a constant
                    {
                        object value;
                        Typ type;
                        var r = decoder.IsConstant(exp, out value, out type);

                        constant = new Microsoft.Research.DataStructures.STuple<bool, object, Typ>(r, value, type);

                        return r;
                    }
                    catch (NullReferenceException)
                    {
                        return false;
                    }
                }
            }
            public override object Constant
            {
                get
                {
                    if (constant.IsValid)
                    {
                        return constant.Value.Two;
                    }

                    object value;
                    Typ type;
                    if (decoder.IsConstant(exp, out value, out type))
                    {
                        constant = new Microsoft.Research.DataStructures.STuple<bool, object, Typ>(false, value, type);
                        return value;
                    }

                    constant = new Microsoft.Research.DataStructures.STuple<bool, object, Typ>(false, value, type);

                    return null;
                }
            }

            public override object ConstantType
            {
                get
                {
                    if (constant.IsValid)
                    {
                        return constant.Value.Three;
                    }

                    object value;
                    Typ type;
                    if (decoder.IsConstant(exp, out value, out type))
                    {
                        constant = new Microsoft.Research.DataStructures.STuple<bool, object, Typ>(false, value, type);

                        return type;
                    }

                    constant = new Microsoft.Research.DataStructures.STuple<bool, object, Typ>(false, value, type);
                    return null;
                }
            }

            public override bool IsSizeOf
            {
                get
                {
                    Typ type;
                    return decoder.IsSizeOf(exp, out type);
                }
            }

            public override bool SizeOf(out object type, out int size)
            {
                Typ ttype;
                if (decoder.IsSizeOf(exp, out ttype))
                {
                    decoder.TrySizeOfAsConstant(exp, out size);
                    type = ttype;
                    return size >= 0;
                }
                type = null; size = -1;
                return false;
            }

            public override bool SizeOf(out int size)
            {
                return decoder.TrySizeOfAsConstant(exp, out size);
            }

            public override bool IsIsInstExpression(out BoxedExpression exp, out object type)
            {
                if (this.IsIsInst)
                {
                    Contract.Assume(isInst.Value.Two != null);

                    exp = isInst.Value.Two;
                    type = isInst.Value.Three;

                    return true;
                }

                exp = default(BoxedExpression);
                type = default(object);
                return false;
            }

            public override bool IsIsInst
            {
                get
                {
                    if (!isInst.IsValid)
                    {
                        ExternalExpression arg;
                        Typ type;
                        var outcome = decoder.IsInst(exp, out arg, out type);

                        isInst = new Microsoft.Research.DataStructures.STuple<bool, BoxedExpression, Typ>(outcome, BoxedExpression.For(arg, decoder), type);
                    }

                    return isInst.Value.One;
                }
            }

            public override bool IsUnary
            {
                get
                {
                    if (unary.IsValid)
                    {
                        return unary.Value.One;
                    }

                    UnaryOperator op;
                    ExternalExpression arg;
                    var res = decoder.IsUnaryOperator(exp, out op, out arg);

                    unary = new Microsoft.Research.DataStructures.STuple<bool, UnaryOperator, BoxedExpression>(res, op, BoxedExpression.For(arg, decoder));

                    return res;
                }
            }

            public override bool IsUnaryExpression(out UnaryOperator uop, out BoxedExpression left)
            {
                if (unary.IsValid)
                {
                    Contract.Assume(unary.Value.Three != null, "F: Lazy, should be an object invariant");

                    uop = unary.Value.Two;
                    left = unary.Value.Three;
                    return unary.Value.One;
                }

                ExternalExpression arg;
                var res = decoder.IsUnaryOperator(exp, out uop, out arg);
                left = BoxedExpression.For(arg, decoder);
                unary = new Microsoft.Research.DataStructures.STuple<bool, UnaryOperator, BoxedExpression>(res, uop, left);

                return res;
            }

            public override BoxedExpression UnaryArgument
            {
                get
                {
                    if (unary.IsValid)
                    {
                        Contract.Assume(unary.Value.Three != null);
                        return unary.Value.Three;
                    }

                    UnaryOperator op;
                    ExternalExpression arg;
                    if (decoder.IsUnaryOperator(exp, out op, out arg))
                    {
                        var exp = BoxedExpression.For(arg, decoder);

                        unary = new Microsoft.Research.DataStructures.STuple<bool, UnaryOperator, BoxedExpression>(true, op, exp);

                        return exp;
                    }

                    unary = new Microsoft.Research.DataStructures.STuple<bool, UnaryOperator, BoxedExpression>(false, op, null);

                    throw new InvalidOperationException();
                }
            }
            public override UnaryOperator UnaryOp
            {
                get
                {
                    if (unary.IsValid)
                    {
                        return unary.Value.Two;
                    }

                    UnaryOperator op;
                    ExternalExpression arg;
                    if (decoder.IsUnaryOperator(exp, out op, out arg))
                    {
                        unary = new Microsoft.Research.DataStructures.STuple<bool, UnaryOperator, BoxedExpression>(true, op, BoxedExpression.For(arg, decoder));

                        return op;
                    }
                    throw new InvalidOperationException();
                }
            }

            public override bool IsVariable
            {
                get
                {
                    if (isVar.IsValid)
                    {
                        return isVar.Value.One;
                    }

                    object variable;
                    var res = decoder.IsVariable(exp, out variable);

                    isVar = new Pair<bool, object>(res, variable);

                    return res;
                }
            }

            public override object UnderlyingVariable
            {
                get
                {
                    if (var.IsValid)
                    {
                        return var.Value;
                    }

                    var v = decoder.UnderlyingVariable(exp);

                    var = v;

                    return v;
                }
            }


            public override bool TryGetAssociatedInfo(AssociatedInfo infoKind, out BoxedExpression info)
            {
                ExternalExpression tempInfo;
                bool success = decoder.TryGetAssociatedExpression(exp, infoKind, out tempInfo);
                if (success) { info = BoxedExpression.For(tempInfo, decoder); return true; }
                info = null;
                return false;
            }
            public override bool TryGetAssociatedInfo(APC pc, AssociatedInfo infoKind, out BoxedExpression info)
            {
                ExternalExpression tempInfo;
                bool success = decoder.TryGetAssociatedExpression(pc, exp, infoKind, out tempInfo);
                if (success) { info = BoxedExpression.For(tempInfo, decoder); return true; }
                info = null;
                return false;
            }

            public override bool TryGetType(out object type)
            {
                if (this.type.IsValid)
                {
                    type = this.type.Value.Two;
                    return this.type.Value.One;
                }
                var outcome = decoder.TryGetType(exp, out type);

                this.type = new Pair<bool, object>(outcome, type);

                return outcome;
            }

            private struct ISetWrapper : Microsoft.Research.DataStructures.IMutableSet<ExternalExpression>
            {
                private Set<BoxedExpression> set;
                private IFullExpressionDecoder<Typ, Variable, ExternalExpression> decoder;
                public ISetWrapper(Set<BoxedExpression> set, IFullExpressionDecoder<Typ, Variable, ExternalExpression> decoder)
                {
                    this.set = set;
                    this.decoder = decoder;
                }


                #region Set<ExternalExpression> Members

                public void AddRange(IEnumerable<ExternalExpression> range)
                {
                    throw new NotImplementedException();
                }

                public Microsoft.Research.DataStructures.IMutableSet<U> ConvertAll<U>(Converter<ExternalExpression, U> converter)
                {
                    throw new NotImplementedException();
                }

                public Microsoft.Research.DataStructures.IMutableSet<ExternalExpression> Difference(IEnumerable<ExternalExpression> b)
                {
                    throw new NotImplementedException();
                }

                public Microsoft.Research.DataStructures.IMutableSet<ExternalExpression> FindAll(Predicate<ExternalExpression> predicate)
                {
                    throw new NotImplementedException();
                }

                public void ForEach(Action<ExternalExpression> action)
                {
                    throw new NotImplementedException();
                }

                public bool Exists(Predicate<ExternalExpression> predicate)
                {
                    throw new NotImplementedException();
                }

                public Microsoft.Research.DataStructures.IMutableSet<ExternalExpression> Intersection(IReadonlySet<ExternalExpression> b)
                {
                    throw new NotImplementedException();
                }

                public bool IsSubset(IReadonlySet<ExternalExpression> s)
                {
                    throw new NotImplementedException();
                }

                public bool IsEmpty
                {
                    get { throw new NotImplementedException(); }
                }

                public bool IsSingleton
                {
                    get { throw new NotImplementedException(); }
                }

                public ExternalExpression PickAnElement()
                {
                    throw new NotImplementedException();
                }

                public bool TrueForAll(Predicate<ExternalExpression> predicate)
                {
                    throw new NotImplementedException();
                }

                public Microsoft.Research.DataStructures.IMutableSet<ExternalExpression> Union(IReadonlySet<ExternalExpression> b)
                {
                    throw new NotImplementedException();
                }

                #endregion

                #region ISet<ExternalExpression> Members

                public bool Add(ExternalExpression item)
                {
                    return set.Add(BoxedExpression.For(item, decoder));
                }

                public bool Contains(ExternalExpression item)
                {
                    throw new NotImplementedException();
                }

                public int Count
                {
                    get { throw new NotImplementedException(); }
                }

                #endregion

                #region IEnumerable<ExternalExpression> Members

                public IEnumerator<ExternalExpression> GetEnumerator()
                {
                    throw new NotImplementedException();
                }

                #endregion

                #region IEnumerable Members

                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    throw new NotImplementedException();
                }

                #endregion
            }

            public override void AddFreeVariables(Set<BoxedExpression> set)
            {
                decoder.AddFreeVariables(exp, new ISetWrapper(set, decoder));
            }

            public override bool IsNull { get { return decoder.IsNull(exp); } }

            [ContractVerification(false)]
            protected override BoxedExpression RecursiveSubstitute(BoxedExpression what, BoxedExpression newExp)
            {
                // first internalize
                var internalized = BoxedExpression.Convert(exp, decoder, Int32.MaxValue);
                // now substitute
                return internalized != null ? internalized.Substitute(what, newExp) : null;
            }

            [ContractVerification(false)]
            public override BoxedExpression Negate()
            {
                var internalConvert = BoxedExpression.Convert(exp, decoder, Int32.MaxValue);
                return internalConvert != null ? internalConvert.Negate() : null;
            }

            internal override Result ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type2, Visitor>(BoxedExpression.PC pc, Visitor visitor, Data data)
            {
                IFullExpressionDecoder<Type2, Variable, ExternalExpression> decoder = (IFullExpressionDecoder<Type2, Variable, ExternalExpression>)(object)this.decoder;
                object value;
                Type2 type;
                if (decoder.IsConstant(exp, out value, out type))
                {
                    if (value == null)
                    {
                        return visitor.Ldnull(pc, Unit.Value, data);
                    }
                    return visitor.Ldconst(pc, value, type, Unit.Value, data);
                }
                UnaryOperator uop;
                ExternalExpression uarg;
                if (decoder.IsUnaryOperator(exp, out uop, out uarg))
                {
                    return visitor.Unary(pc, uop, false, false, Unit.Value, Unit.Value, data);
                }
                BinaryOperator bop;
                ExternalExpression left, right;
                if (decoder.IsBinaryOperator(exp, out bop, out left, out right))
                {
                    return visitor.Binary(pc, bop, Unit.Value, Unit.Value, Unit.Value, data);
                }
                if (decoder.IsInst(exp, out uarg, out type))
                {
                    return visitor.Isinst(pc, type, Unit.Value, Unit.Value, data);
                }
                if (decoder.IsNull(exp))
                {
                    return visitor.Ldnull(pc, Unit.Value, data);
                }
                if (decoder.IsSizeOf(exp, out type))
                {
                    return visitor.Sizeof(pc, type, Unit.Value, data);
                }
                Contract.Assume(this.decoder.IsVariable(exp, out value));
                throw new NotImplementedException("TODO");
            }


            #region Object overrides
            public override bool Equals(object obj)
            {
                if (this == obj) return true;
                ExternalBox<Variable, ExternalExpression> that = obj as ExternalBox<Variable, ExternalExpression>;
                if (that != null)
                {
                    return exp.Equals(that.exp);
                }
                BoxedExpression other = obj as BoxedExpression;
                if (other != null)
                {
                    // mixed case dispatch in reverse
                    return other.Equals(this);
                }
                return false;
            }

            protected override int ComputeHashCode()
            {
                return exp.GetHashCode();
            }

            public override string ToString()
            {
                // Special case for constants 
                if (this.IsConstant)
                {
                    if (this.Constant is string)
                    {
                        string str = this.Constant as string;
                        return "@\"" + str.Replace("\"", "\"\"") + "\""; // correctly escape the characters
                    }
                    else
                        return this.Constant != null ? this.Constant.ToString() : "null";
                }

                return exp.ToString();
            }
            #endregion

            public override BoxedExpression Substitute<V>(Func<V, BoxedExpression, BoxedExpression> map)
            {
                // first internalize
                var internalized = BoxedExpression.Convert(exp, decoder, Int32.MaxValue);
                // now substitute
                return internalized != null ? internalized.Substitute(map) : internalized;
            }

            public override BoxedExpression Rename<V>(IFunctionalMap<V, V> renaming, Func<V, BoxedExpression> refiner = null)
            {
                // first internalize
                var internalized = BoxedExpression.Convert(exp, decoder, Int32.MaxValue);
                // now substitute
                return internalized != null ? internalized.Rename(renaming, refiner) : null;
            }

            public override void Dispatch(IBoxedExpressionVisitor visitor)
            {
                decoder.Dispatch(exp, visitor);
            }
        }

        #endregion
    }

    public interface IFullExpressionDecoder<Type, Variable, Expression>
    {
        /// <summary>
        /// Returns true if exp is a variable
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        bool IsVariable(Expression exp, out object variable);

        Variable UnderlyingVariable(Expression exp);

        /// <summary>
        /// Returns true if exp is the null constant
        /// </summary>
        bool IsNull(Expression exp);

        /// <summary>
        /// If the expression is a constant, this provides the value and the type
        /// </summary>
        bool IsConstant(Expression exp, out object value, out Type type);

        /// <summary>
        /// If the expression is sizeof(T), this provides the type T
        /// </summary>
        bool IsSizeOf(Expression exp, out Type type);


        /// <summary>
        /// If the expression is isInst(e, T), this provides e and T
        /// </summary>
        bool IsInst(Expression exp, out Expression arg, out Type type);

        /// <summary>
        /// If exp is a Unary operation (according to MSIL!), this returns the operator
        /// and operand
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        bool IsUnaryOperator(Expression exp, out UnaryOperator op, out Expression arg);

        /// <summary>
        /// If exp is a binary operation (according to MSIL!), this returns the operator
        /// and operands
        /// </summary>
        bool IsBinaryOperator(Expression exp, out BinaryOperator op, out Expression left, out Expression right);

        /// <summary>
        /// If the expression has the associated info, it is returned.
        /// </summary>
        /// <returns>true if success, false, if no information</returns>
        bool TryGetAssociatedExpression(Expression exp, AssociatedInfo infoKind, out Expression info);

        /// <summary>
        /// If the expression has the associated info, it is returned.
        /// </summary>
        /// <param name="pc">Try to find information associated at given pc (e.g., for variables)</param>
        /// <returns>true if success, false, if no information</returns>
        bool TryGetAssociatedExpression(APC pc, Expression exp, AssociatedInfo infoKind, out Expression info);

        /// <summary>
        /// Used to gather the free variables in exp and add them to the set
        /// </summary>
        void AddFreeVariables(Expression exp, Microsoft.Research.DataStructures.IMutableSet<Expression> set);

        /// <summary>
        /// Returns an access path to the expression (which is classified as a variable)
        /// </summary>
        FList<PathElement> GetVariableAccessPath(Expression exp);

        /// <summary>
        /// If exp is sizeof this tries to get the size of the type as a constant
        /// </summary>
        bool TrySizeOfAsConstant(Expression exp, out int value);

        bool TryGetType(Expression exp, out object type);

        Type System_Int32 { get; }

        bool IsReferenceType(Type type);

        void Dispatch(Expression exp, IBoxedExpressionVisitor visitor);
    }

    #region CodeProviders and MSIL decoders

    public class ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type> :
      ICodeProvider<BoxedExpression.PC, Local, Parameter, Method, Field, Type>
    {
        public static readonly ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type> Decoder = new ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type>(); // Thread-safe

        #region ICodeProvider<PC,Method,Type> Members

        /// <summary>
        /// To avoid redundantly encoding the logic for decoding the next and nested ops,
        /// we return the next flat aggregate label as well as the possible nested aggregate op at the
        /// current point.
        ///
        /// There are a number of possible outcomes
        /// 1) the current program point refers to a sub-expression
        ///   1a) and there is a syntactic next program point
        ///   1b) and there is no syntactic next program point
        /// 2) the current program point refers to a final operation (e.g. add elements on stack)
        ///    and there is no syntactic next program point
        /// 3) the current program point is a no-op and there is no syntactic next program point
        /// 4) the current node is a variable and we are stepping through the access path! In this case
        ///    the final operation is a no-op and there are no nested expressions that representable as ClousotExpressions.
        ///
        /// We encode these outcomes as follows:
        /// If the result is null, it means there is a next program point, otherwise, it is the final operation
        /// of the current PC (possibly no-ops, e.g. Block, or ExpressionStatement).
        /// The out parameter nestedStart is non-null if the current PC labels that particular sub-expression
        /// and it is null, if the current program point does not have a sub-expression to be evaluated.
        /// If both the result and the nexted start are null, then this is a special encoding, e.g., for Begin_Old
        /// In the case of stepping through a variable access path, the nested expression is the same as the original,
        /// and the context must use the index to access the path element.
        /// </summary>
        /// <param name="nestedStart">Returns the sub-tree at this PC or null.</param>
        /// <returns>The final operation referred to by this PC or null if there is a syntactic next pc.
        /// </returns>
        private static BoxedExpression FinalOpAndNested(BoxedExpression.PC pc, out BoxedExpression nestedStart)
        {
            ClousotExpression<Type>.OldExpression oldExp = pc.Node as ClousotExpression<Type>.OldExpression;
            if (oldExp != null)
            {
                // 0 is begin old
                // 1 is nested aggregate
                // 2 is end old
                if (pc.Index == 0)
                {
                    nestedStart = null;
                    return null;
                }
                if (pc.Index == 1)
                {
                    nestedStart = oldExp.Old;
                    return null;
                }
                nestedStart = null;
                return oldExp;
            }

            if (pc.Node.IsConstant || pc.Node.IsSizeOf)
            {
                nestedStart = null;
                return pc.Node;
            }
            if (pc.Node.IsUnary)
            {
                if (pc.Index == 0)
                {
                    nestedStart = pc.Node.UnaryArgument;
                    return null;
                }
                nestedStart = null;
                return pc.Node;
            }
            if (pc.Node.IsBinary)
            {
                var bop = pc.Node.BinaryOp;
                if (bop == BinaryOperator.LogicalAnd || bop == BinaryOperator.LogicalOr)
                {
                    // lazy eval producing the following code:
                    //  a
                    //  dup
                    //  br.true pc.Node,5 (br.false pc.Node,5 for logical &&)
                    //  pop
                    //  b
                    // pc,5:
                    //  nop
                    switch (pc.Index)
                    {
                        case 0:
                            nestedStart = pc.Node.BinaryLeft;
                            return null;

                        case 1:
                        case 2:
                        case 3:
                            nestedStart = null;
                            return null;

                        case 4:
                            nestedStart = pc.Node.BinaryRight;
                            return null;

                        case 5:
                            nestedStart = null;
                            return pc.Node;
                    }
                }
                if (pc.Index == 0)
                {
                    nestedStart = pc.Node.BinaryLeft;
                    return null;
                }
                if (pc.Index == 1)
                {
                    nestedStart = pc.Node.BinaryRight;
                    return null;
                }
                nestedStart = null;
                return pc.Node;
            }
            if (pc.Node.IsVariable)
            {
                if (pc.Node.AccessPath != null // the access path can be null when we habe a bounded variable for quantifiers
                  && pc.Node.AccessPath.Length > pc.Index)
                {
                    nestedStart = pc.Node;
                    return null;
                }
                // End of variable sub expressions.
                nestedStart = null;
                return pc.Node;
            }
            if (pc.Node.IsIsInst)
            {
                if (pc.Index == 0)
                {
                    nestedStart = pc.Node.UnaryArgument;
                    return null;
                }
                nestedStart = null;
                return pc.Node;
            }
            // F: Added this case for the array index expressions
            BoxedExpression array, index;
            object type;
            if (pc.Node.IsArrayIndexExpression(out array, out index, out type))
            {
                if (pc.Index == 0)
                {
                    nestedStart = array;
                    return null;
                }
                if (pc.Index == 1)
                {
                    nestedStart = index;
                    return null;
                }
                nestedStart = null;
                return pc.Node;
            }
            BoxedExpression.ContractExpression assertExp = pc.Node as BoxedExpression.ContractExpression;
            if (assertExp != null)
            {
                if (pc.Index == 0)
                {
                    nestedStart = assertExp.Condition;
                    return null;
                }
                nestedStart = null;
                return assertExp;
            }
            ClousotExpression<Type>.ResultExpression resExp = pc.Node as ClousotExpression<Type>.ResultExpression;
            if (resExp != null)
            {
                nestedStart = null;
                return pc.Node;
            }
            BoxedExpression.StatementSequence seq = pc.Node as BoxedExpression.StatementSequence;
            if (seq != null)
            {
                if (pc.Index >= seq.Count)
                {
                    nestedStart = null;
                    return pc.Node;
                }
                Contract.Assume(pc.Index >= 0, "limitation of invariants on readonly fields");
                Contract.Assert(pc.Index < seq.Count, "Complicated invariant on pc's");
                nestedStart = seq[pc.Index];
                return null;
            }

            // F: Added quantified expressions
            bool isForAll;
            BoxedExpression boundVar, lower, upper, body;
            if (pc.Node.IsQuantifiedExpression(out isForAll, out boundVar, out lower, out upper, out body))
            {
                switch (pc.Index)
                {
                    case 0:
                        {
                            nestedStart = boundVar;
                            return null;
                        }
                    case 1:
                        {
                            nestedStart = lower;
                            return null;
                        }
                    case 2:
                        {
                            nestedStart = upper;
                            return null;
                        }
                    case 3:
                        {
                            nestedStart = body;
                            return null;
                        }
                }
                nestedStart = null;
                return pc.Node;
            }

            throw new NotImplementedException();
        }

#if false
        public R Decode<Visitor, T, R>(BoxedExpression.PC label, Visitor query, T data)
          where Visitor : ICodeQuery<BoxedExpression.PC, Local, Parameter, Method, Field, Type, T, R>
        {
            BoxedExpression nested;
            BoxedExpression finalOp = FinalOpAndNested(label, out nested);
            if (nested != null)
            {
                if (nested == label.Node)
                {
                    // variable access path case
                    Debug.Assert(nested.IsVariable);
                    PathElement[] path = nested.AccessPath;
                    return path[label.Index].Decode<T, R, Visitor, BoxedExpression.PC, Local, Parameter, Method, Field, Type>(label, query, data);
                }
                return query.Aggregate(label, new BoxedExpression.PC(nested, 0), false, data);
            }
            if (finalOp == null)
            {
                // begin old
                return query.BeginOld(label, new BoxedExpression.PC(label.Node, 2), data);
            }
            ClousotExpression<Type>.OldExpression oldExp = finalOp as ClousotExpression<Type>.OldExpression;
            if (oldExp != null)
            {
                return query.EndOld(label, new BoxedExpression.PC(label.Node, 0), oldExp.Type, Unit.Value, Unit.Value, data);
            }
            return query.Atomic(data, label);
        }
#endif

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant", Justification = "Probably Clousot is right that this current.Node != null but we want to leave the check anyway")]
        public bool Next(BoxedExpression.PC current, out BoxedExpression.PC nextLabel)
        {
            BoxedExpression nested;
            BoxedExpression finalOp = FinalOpAndNested(current, out nested);
            if (finalOp == null && current.Node != null)
            {
                // has next
                nextLabel = new BoxedExpression.PC(current.Node, current.Index + 1);
                return true;
            }

            nextLabel = default(BoxedExpression.PC);
            return false;
        }

        public void PrintCodeAt(BoxedExpression.PC pc, string indent, TextWriter tw)
        {
            Contract.Requires(tw != null);

            Contract.Assume(pc.Node != null);
            tw.WriteLine("[{0},{1}]", pc.Index, pc.Node.ToString());
        }

        public bool HasSourceContext(BoxedExpression.PC pc)
        {
            BoxedExpression.ContractExpression assert = pc.Node as BoxedExpression.ContractExpression;
            if (assert != null)
            {
                return assert.HasSourceContext;
            }
            return false;
        }

        public string SourceAssertionCondition(BoxedExpression.PC pc)
        {
            BoxedExpression.ContractExpression assert = pc.Node as BoxedExpression.ContractExpression;
            if (assert != null)
            {
                return assert.SourceAssertionCondition;
            }
            return null;
        }

        public string SourceDocument(BoxedExpression.PC pc)
        {
            BoxedExpression.ContractExpression assert = pc.Node as BoxedExpression.ContractExpression;
            if (assert != null)
            {
                return assert.SourceDocument;
            }
            throw new NotImplementedException();
        }

        public int SourceStartLine(BoxedExpression.PC pc)
        {
            BoxedExpression.ContractExpression assert = pc.Node as BoxedExpression.ContractExpression;
            if (assert != null)
            {
                return assert.SourceStartLine;
            }
            throw new NotImplementedException();
        }

        public int SourceEndLine(BoxedExpression.PC pc)
        {
            BoxedExpression.ContractExpression assert = pc.Node as BoxedExpression.ContractExpression;
            if (assert != null)
            {
                return assert.SourceEndLine;
            }
            throw new NotImplementedException();
        }

        public int SourceStartColumn(BoxedExpression.PC pc)
        {
            BoxedExpression.ContractExpression assert = pc.Node as BoxedExpression.ContractExpression;
            if (assert != null)
            {
                return assert.SourceStartColumn;
            }
            throw new NotImplementedException();
        }

        public int SourceEndColumn(BoxedExpression.PC pc)
        {
            BoxedExpression.ContractExpression assert = pc.Node as BoxedExpression.ContractExpression;
            if (assert != null)
            {
                return assert.SourceEndColumn;
            }
            throw new NotImplementedException();
        }

        public int SourceStartIndex(BoxedExpression.PC pc)
        {
            BoxedExpression.ContractExpression assert = pc.Node as BoxedExpression.ContractExpression;
            if (assert != null)
            {
                return assert.SourceStartIndex;
            }
            throw new NotImplementedException();
        }

        public int SourceLength(BoxedExpression.PC pc)
        {
            BoxedExpression.ContractExpression assert = pc.Node as BoxedExpression.ContractExpression;
            if (assert != null)
            {
                return assert.SourceLength;
            }
            throw new NotImplementedException();
        }

        public int ILOffset(BoxedExpression.PC pc)
        {
            BoxedExpression.ContractExpression assert = pc.Node as BoxedExpression.ContractExpression;
            if (assert != null && assert.Apc.Block != null)
            {
                return assert.ILOffset;
            }
            return 0;
        }

        #endregion

        #region IDecodeMSIL<PC,Local,Parameter,Method,Field,Type,Unit,Unit,Unit> Members

        /// <summary>
        /// NOTE: Variable is not atomic, as it is decoded into an access path!!!
        /// </summary>
        private bool IsAtomic(BoxedExpression exp)
        {
            return exp.IsConstant || exp.IsSizeOf;
        }

        public Result Decode<Visitor, Data, Result>(BoxedExpression.PC pc, Visitor visitor, Data data)
          where Visitor : ICodeQuery<BoxedExpression.PC, Local, Parameter, Method, Field, Type, Data, Result>
        {
            BoxedExpression nested;
            BoxedExpression currentOp = FinalOpAndNested(pc, out nested);
            if (nested != null)
            {
                if (nested == pc.Node)
                {
                    // variable access path case
                    Contract.Assume(nested.IsVariable);
                    var path = nested.AccessPath;

                    Contract.Assume(pc.Index < path.Length);
                    Contract.Assume(path[pc.Index] != null);

                    return path[pc.Index].Decode<Data, Result, Visitor, BoxedExpression.PC, Local, Parameter, Method, Field, Type>(pc, visitor, data);
                }
                return visitor.Aggregate(pc, new BoxedExpression.PC(nested, 0), false, data);
#if false
                if (IsAtomic(nested))
                {
                    currentOp = nested;
                }
                else
                {
                    return visitor.Nop(pc, data);
                }
#endif
            }
            if (currentOp == null)
            {
                if (pc.Node is ClousotExpression<Type>.OldExpression)
                {
                    // begin old
                    return visitor.BeginOld(pc, new BoxedExpression.PC(pc.Node, 2), data);
                }
                if (pc.Node.IsBinary)
                {
                    return DecodeBinary<Visitor, Data, Result>(pc, visitor, data);
                }
                return visitor.Nop(pc, data);
            }
            // final op is decoded by the expression itself
            return currentOp.ForwardDecode<Data, Result, Local, Parameter, Field, Method, Type, Visitor>(pc, visitor, data);
        }

        private Result DecodeBinary<Visitor, Data, Result>(BoxedExpression.PC pc, Visitor visitor, Data data)
          where Visitor : ICodeQuery<BoxedExpression.PC, Local, Parameter, Method, Field, Type, Data, Result>
        {
            switch (pc.Index)
            {
                case 1:
                    return visitor.Ldstack(pc, 0, Unit.Value, Unit.Value, false, data);
                case 2:
                    if (pc.Node.BinaryOp == BinaryOperator.LogicalAnd)
                    {
                        return visitor.BranchFalse(pc, new BoxedExpression.PC(pc.Node, 5), Unit.Value, data);
                    }
                    else
                    {
                        return visitor.BranchTrue(pc, new BoxedExpression.PC(pc.Node, 5), Unit.Value, data);
                    }
                case 3:
                    return visitor.Pop(pc, Unit.Value, data); // pop the extra argument from the first evaluation
                                                              //case 0:
                                                              //case 4:
                                                              //case 5:
                default:
                    return visitor.Nop(pc, data);
            }
        }

#if false
        public Transformer<BoxedExpression.PC, Data, Result> CacheForwardDecoder<Data, Result>(IVisitMSIL<BoxedExpression.PC, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result> visitor)
        {
            return (label, data) => ForwardDecode<Data, Result, IVisitMSIL<BoxedExpression.PC, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result>>(label, visitor, data);
        }
#endif

        public Unit GetContext
        {
            get { return Unit.Value; }
        }

        #endregion
    }

    #endregion
}
