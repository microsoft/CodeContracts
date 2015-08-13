// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// The decoder (implemented as visitors for the IL)

using System;
using Generics = System.Collections.Generic;
using Microsoft.Research.DataStructures;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    /// <summary>
    /// The wrapper for the analysis, so to share some code (in particular the decoder)
    /// </summary>
    public static class ExternalExpressionDecoder
    {
        public static IFullExpressionDecoder<Type, Variable, Expression> Create<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context
        )
          where Expression : IEquatable<Expression>
          where Variable : IEquatable<Variable>
          where Type : IEquatable<Type>
        {
            Contract.Requires(context != null);

            return new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Decoder(context, mdDecoder);
        }

        // This class is just to provide bindings to type parameters
        public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
          where Variable : IEquatable<Variable>
          where Expression : IEquatable<Expression>
          where Type : IEquatable<Type>
        {
            #region Expression decoder, implementation and helper visitors
            /// <summary>
            /// The decoder for the expressions
            /// </summary>
            public class Decoder : IFullExpressionDecoder<Type, Variable, Expression>
            {
                #region Private environment
                protected IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context;
                protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoderForMetaData;

                private VisitorForIsBinaryExpression visitorForIsBinaryExpression;
                private VisitorForIsUnaryExpression visitorForIsUnaryExpression;
                private VisitorForVariablesIn visitorForVariablesIn;
                private VisitorForValueOf visitorForValueOf;
                private VisitorForIsNull visitorForIsNull;
                private VisitorForSizeOf visitorForSizeOf;
                private VisitorForIsInst visitorForIsInst;
                private VisitorForVariable visitorForVariable;
                private VisitorForUnderlyingVariable visitorForUnderlyingVariable;
                private VisitorForDispatch visitorForDispatch;
                #endregion

                #region Getters

                public IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context
                {
                    get
                    {
                        Contract.Ensures(Contract.Result<IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>>() != null);

                        return this.context;
                    }
                }

                #endregion

                public Decoder(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
                  IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoderForMetaData)
                {
                    Contract.Requires(context != null);

                    this.context = context;
                    this.decoderForMetaData = decoderForMetaData;

                    visitorForIsUnaryExpression = new VisitorForIsUnaryExpression();
                    visitorForIsBinaryExpression = new VisitorForIsBinaryExpression();
                    visitorForVariablesIn = new VisitorForVariablesIn(context);
                    visitorForValueOf = new VisitorForValueOf();
                    visitorForSizeOf = new VisitorForSizeOf();
                    visitorForIsNull = new VisitorForIsNull();
                    visitorForVariable = new VisitorForVariable();
                    visitorForIsInst = new VisitorForIsInst();
                    visitorForUnderlyingVariable = new VisitorForUnderlyingVariable();
                    visitorForDispatch = new VisitorForDispatch(this);
                }

                #region IFullExpressionDecoder<Type,Field,Expression> Members

                public bool IsVariable(Expression exp, out object variable)
                {
                    Variable var;
                    bool result = VisitorForVariable.IsVariable(exp, out var, this);
                    variable = var;
                    return result;
                }

                public Variable UnderlyingVariable(Expression exp)
                {
                    return VisitorForUnderlyingVariable.UnderlyingVariable(exp, this);
                }

                public bool IsConstant(Expression exp, out object value, out Type type)
                {
                    return VisitorForValueOf.IsConstant(exp, out value, out type, this);
                }

                public bool IsSizeOf(Expression exp, out Type type)
                {
                    return VisitorForSizeOf.IsSizeOf(exp, out type, this);
                }

                public bool IsInst(Expression exp, out Expression arg, out Type type)
                {
                    return VisitorForIsInst.IsIsInst(exp, out type, out arg, this);
                }

                public bool IsUnaryOperator(Expression exp, out UnaryOperator op, out Expression arg)
                {
                    return VisitorForIsUnaryExpression.IsUnary(exp, out op, out arg, this);
                }

                public bool IsBinaryOperator(Expression exp, out BinaryOperator op, out Expression left, out Expression right)
                {
                    return VisitorForIsBinaryExpression.IsBinary(exp, out op, out left, out right, this);
                }

                public FList<PathElement> GetVariableAccessPath(Expression exp)
                {
                    return this.context.ValueContext.AccessPathList(this.context.ExpressionContext.GetPC(exp), this.context.ExpressionContext.Unrefine(exp), true, false);
                }

                public void AddFreeVariables(Expression exp, Microsoft.Research.DataStructures.IMutableSet<Expression> set)
                {
                    VisitorForVariablesIn.AddFreeVariables(exp, set, this);
                }

                public bool TryGetType(Expression exp, out object type)
                {
                    var liftedType = this.context.ExpressionContext.GetType(exp);
                    if (liftedType.IsNormal)
                    {
                        type = liftedType.Value;
                        return true;
                    }

                    type = default(object);
                    return false;
                }

                #endregion

                #region The visitors

                protected abstract class QueryVisitor
                    : IVisitValueExprIL<Expression, Type, Expression, Variable, Unit, bool>
                {
                    protected static bool Decode<Visitor>(Expression exp, Visitor v, Decoder decoder) where Visitor : QueryVisitor
                    {
                        Contract.Requires(decoder != null);

                        return decoder.Context.ExpressionContext.Decode<Unit, bool, Visitor>(exp, v, Unit.Value);
                    }

                    #region IVisitValueExprIL<Expression,Type,ExternalExpression,Variable,Unit,bool> Members

                    public virtual bool SymbolicConstant(Expression pc, Variable symbol, Unit data)
                    {
                        return false;
                    }

                    #endregion

                    #region IVisitExprIL<Expression,Type,ExternalExpression,Variable,Unit,bool> Members

                    public virtual bool Binary(Expression pc, BinaryOperator op, Variable dest, Expression s1, Expression s2, Unit data)
                    {
                        return false;
                    }

                    public virtual bool Isinst(Expression pc, Type type, Variable dest, Expression obj, Unit data)
                    {
                        return false;
                    }

                    public virtual bool Ldconst(Expression pc, object constant, Type type, Variable dest, Unit data)
                    {
                        return false;
                    }

                    public virtual bool Ldnull(Expression pc, Variable dest, Unit data)
                    {
                        return false;
                    }

                    public virtual bool Sizeof(Expression pc, Type type, Variable dest, Unit data)
                    {
                        return false;
                    }

                    public virtual bool Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Unit data)
                    {
                        return false;
                    }

                    public virtual bool Box(Expression pc, Type type, Variable dest, Expression source, Unit data)
                    {
                        return false;
                    }

                    #endregion
                }

                private class VisitorForVariable : QueryVisitor
                {
                    private Variable Variable;

                    public static bool IsVariable(Expression exp, out Variable var, Decoder decoder)
                    {
                        VisitorForVariable visitor = decoder.visitorForVariable;
                        bool result = Decode(exp, visitor, decoder);
                        var = visitor.Variable;
                        return result;
                    }

                    public override bool SymbolicConstant(Expression pc, Variable symbol, Unit data)
                    {
                        Variable = symbol;
                        return true;
                    }
                }

                private class VisitorForUnderlyingVariable : QueryVisitor
                {
                    private Variable Variable;

                    public static Variable UnderlyingVariable(Expression exp, Decoder decoder)
                    {
                        var visitor = decoder.visitorForUnderlyingVariable;
                        var dummy = Decode(exp, visitor, decoder);

                        return visitor.Variable;
                    }

                    public override bool Binary(Expression pc, BinaryOperator op, Variable dest, Expression s1, Expression s2, Unit data)
                    {
                        Variable = dest;
                        return true;
                    }

                    public override bool Isinst(Expression pc, Type type, Variable dest, Expression obj, Unit data)
                    {
                        Variable = dest;
                        return true;
                    }

                    public override bool Ldconst(Expression pc, object constant, Type type, Variable dest, Unit data)
                    {
                        Variable = dest;
                        return true;
                    }

                    public override bool Ldnull(Expression pc, Variable dest, Unit data)
                    {
                        Variable = dest;
                        return true;
                    }

                    public override bool Sizeof(Expression pc, Type type, Variable dest, Unit data)
                    {
                        Variable = dest;
                        return true;
                    }

                    public override bool SymbolicConstant(Expression pc, Variable symbol, Unit data)
                    {
                        Variable = symbol;
                        return true;
                    }

                    public override bool Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Unit data)
                    {
                        Variable = dest;
                        return true;
                    }
                }


                private class VisitorForIsUnaryExpression : QueryVisitor
                {
                    private UnaryOperator Operator;
                    private Expression Argument;

                    public static bool IsUnary(Expression exp, out UnaryOperator op, out Expression arg,
                      Decoder decoder)
                    {
                        VisitorForIsUnaryExpression visitor = decoder.visitorForIsUnaryExpression;
                        bool result = Decode(exp, visitor, decoder);
                        op = visitor.Operator;
                        arg = visitor.Argument;
                        return result;
                    }

                    public override bool Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Unit data)
                    {
                        Argument = source;
                        Operator = op;
                        return true;
                    }
                }

                private class VisitorForIsBinaryExpression : QueryVisitor
                {
                    private BinaryOperator Operator;
                    private Expression Left;
                    private Expression Right;

                    public static bool IsBinary(Expression exp, out BinaryOperator op, out Expression left, out Expression right, Decoder decoder)
                    {
                        VisitorForIsBinaryExpression visitor = decoder.visitorForIsBinaryExpression;
                        bool result = Decode(exp, visitor, decoder);
                        op = visitor.Operator;
                        left = visitor.Left;
                        right = visitor.Right;
                        return result;
                    }

                    public override bool Binary(Expression pc, BinaryOperator op, Variable dest, Expression s1, Expression s2, Unit data)
                    {
                        Operator = op;
                        Left = s1;
                        Right = s2;
                        return true;
                    }
                }

                private class VisitorForVariablesIn
                  : IVisitValueExprIL<Expression, Type, Expression, Variable, Microsoft.Research.DataStructures.IMutableSet<Expression>, Unit>
                {
                    #region Private state
                    private IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context;
                    #endregion

                    public VisitorForVariablesIn(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>/*!*/ context)
                    {
                        this.context = context;
                    }

                    public static void AddFreeVariables(Expression exp,
                                                        Microsoft.Research.DataStructures.IMutableSet<Expression> set, Decoder decoder)
                    {
                        decoder.visitorForVariablesIn.Recurse(exp, set);
                    }

                    private void Recurse(Expression exp, Microsoft.Research.DataStructures.IMutableSet<Expression> set)
                    {
                        context.ExpressionContext.Decode<Microsoft.Research.DataStructures.IMutableSet<Expression>, Unit, VisitorForVariablesIn>(exp, this, set);
                    }

                    #region IVisitValueExprIL<Expression,Type,Expression,Variable,Generics.Set<Variable>,Generics.Set<Variable>> Members

                    public Unit SymbolicConstant(Expression pc, Variable symbol, Microsoft.Research.DataStructures.IMutableSet<Expression> data)
                    {
                        data.Add(pc);
                        return Unit.Value;
                    }

                    #endregion

                    #region IVisitExprIL<Expression,Type,Expression,Variable,Generics.Set<Expression>,Microsoft.Research.DataStructures.ISet<Expression>> Members

                    public Unit Binary(Expression pc, BinaryOperator op, Variable dest, Expression s1, Expression s2, Microsoft.Research.DataStructures.IMutableSet<Expression> data)
                    {
                        Recurse(s1, data);
                        Recurse(s2, data);

                        return Unit.Value;
                    }

                    public Unit Isinst(Expression pc, Type type, Variable dest, Expression obj, Microsoft.Research.DataStructures.IMutableSet<Expression> data)
                    {
                        data.Add(pc); // TODO: figure out who depends on this! This should not be treated as a free variable!

                        return Unit.Value;
                    }

                    public Unit Ldconst(Expression pc, object constant, Type type, Variable dest, Microsoft.Research.DataStructures.IMutableSet<Expression> data)
                    {
                        return Unit.Value;
                    }

                    public Unit Ldnull(Expression pc, Variable dest, Microsoft.Research.DataStructures.IMutableSet<Expression> data)
                    {
                        return Unit.Value;
                    }

                    public Unit Sizeof(Expression pc, Type type, Variable dest, Microsoft.Research.DataStructures.IMutableSet<Expression> data)
                    {
                        // Why is this used here? TODO: figure out what depends on this!
                        data.Add(pc);
                        return Unit.Value;
                    }

                    public Unit Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Microsoft.Research.DataStructures.IMutableSet<Expression> data)
                    {
                        Recurse(source, data);
                        return Unit.Value;
                    }

                    public Unit Box(Expression pc, Type type, Variable dest, Expression source, IMutableSet<Expression> data)
                    {
                        Recurse(source, data);
                        return Unit.Value;
                    }

                    #endregion
                }

                private class VisitorForValueOf : QueryVisitor
                {
                    #region Private state
                    private Type Type;
                    private object Value;
                    #endregion

                    public static bool IsConstant(Expression exp, out object value, out Type type, Decoder decoder)
                    {
                        VisitorForValueOf visitor = decoder.visitorForValueOf;
                        bool result = Decode(exp, visitor, decoder);
                        value = visitor.Value;
                        type = visitor.Type;
                        return result;
                    }

                    public override bool Ldconst(Expression pc, object constant, Type type, Variable dest, Unit data)
                    {
                        Type = type;
                        Value = constant;
                        return true;
                    }
                    public override bool Ldnull(Expression pc, Variable dest, Unit data)
                    {
                        Type = default(Type);
                        Value = null;
                        return true;
                    }
                }

                private class VisitorForSizeOf : QueryVisitor
                {
                    #region Constructor
                    #endregion

                    private Type Type;

                    public static bool IsSizeOf(Expression exp, out Type type, Decoder decoder)
                    {
                        VisitorForSizeOf visitor = decoder.visitorForSizeOf;
                        bool result = Decode(exp, visitor, decoder);
                        type = visitor.Type;
                        return result;
                    }

                    public override bool Sizeof(Expression pc, Type type, Variable dest, Unit data)
                    {
                        Type = type;
                        return true;
                    }
                }

                private class VisitorForIsInst : QueryVisitor
                {
                    #region Constructor
                    #endregion

                    private Type Type;
                    private Expression Argument;

                    public static bool IsIsInst(Expression exp, out Type type, out Expression arg, Decoder decoder)
                    {
                        VisitorForIsInst visitor = decoder.visitorForIsInst;
                        bool result = Decode(exp, visitor, decoder);
                        type = visitor.Type;
                        arg = visitor.Argument;
                        return result;
                    }

                    public override bool Isinst(Expression pc, Type type, Variable dest, Expression obj, Unit data)
                    {
                        Type = type;
                        Argument = obj;
                        return true;
                    }
                }

#if false
                private class VisitorForNameOf : IVisitValueExprIL<Expression, Type, Expression, Variable, Unit, string>
                {
                    #region Private state
                    private IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context;
                    #endregion

                    #region Constructor
                    public VisitorForNameOf(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context)
                    {
                        this.context = context;
                    }
                    #endregion

                    #region IVisitValueExprIL<Expression,Type,Expression,Variable,Unit,string> Members

                    public string SymbolicConstant(Expression aPC, Variable symbol, Unit data)
                    {
                        return symbol.ToString();
                    }

                    #endregion

                    #region IVisitExprIL<Expression,Type,Expression,Variable,Unit,string> Members

                    public string Binary(Expression pc, BinaryOperator op, Variable dest, Expression s1, Expression s2, Unit data)
                    {
                        return null;
                    }

                    public string Isinst(Expression pc, Type type, Variable dest, Expression obj, Unit data)
                    {
                        return null;
                    }

                    public string Ldconst(Expression pc, object constant, Type type, Variable dest, Unit data)
                    {
                        return "Constant(" + constant.ToString() + ")";
                    }

                    public string Ldnull(Expression pc, Variable dest, Unit data)
                    {
                        return null;
                    }

                    public string Sizeof(Expression pc, Type type, Variable dest, Unit data)
                    {
                        return null;
                    }

                    public string Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Unit data)
                    {
                        return null;
                    }

                    #endregion
                }
#endif
                private class VisitorForIsNull : QueryVisitor
                {
                    public static bool IsNull(Expression exp, Decoder decoder)
                    {
                        return Decode(exp, decoder.visitorForIsNull, decoder);
                    }

                    public override bool Ldnull(Expression pc, Variable dest, Unit data)
                    {
                        return true;
                    }
                }

                private class VisitorForDispatch : IVisitValueExprIL<Expression, Type, Expression, Variable, IBoxedExpressionVisitor, Unit>
                {
                    private Decoder decoder;

                    public VisitorForDispatch(Decoder decoder)
                    {
                        Contract.Requires(decoder != null);

                        this.decoder = decoder;
                    }

                    public Unit SymbolicConstant(Expression pc, Variable symbol, IBoxedExpressionVisitor data)
                    {
                        data.Variable(symbol, null, null);
                        return Unit.Value;
                    }

                    public Unit Binary(Expression pc, BinaryOperator op, Variable dest, Expression s1, Expression s2, IBoxedExpressionVisitor data)
                    {
                        data.Binary(op, BoxedExpression.For(s1, decoder), BoxedExpression.For(s2, decoder), null);
                        return Unit.Value;
                    }

                    public Unit Isinst(Expression pc, Type type, Variable dest, Expression obj, IBoxedExpressionVisitor data)
                    {
                        data.IsInst(type, BoxedExpression.For(obj, decoder), null);
                        return Unit.Value;
                    }

                    public Unit Ldconst(Expression pc, object constant, Type type, Variable dest, IBoxedExpressionVisitor data)
                    {
                        data.Constant(type, constant, null);
                        return Unit.Value;
                    }

                    public Unit Ldnull(Expression pc, Variable dest, IBoxedExpressionVisitor data)
                    {
                        data.Constant<Type>(default(Type), null, null);
                        return Unit.Value;
                    }

                    public Unit Sizeof(Expression pc, Type type, Variable dest, IBoxedExpressionVisitor data)
                    {
                        int size;
                        if (!decoder.TrySizeOfAsConstant(pc, out size))
                        {
                            size = 0;
                        }
                        data.SizeOf(type, size, null);
                        return Unit.Value;
                    }

                    public Unit Unary(Expression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, IBoxedExpressionVisitor data)
                    {
                        data.Unary(op, BoxedExpression.For(source, decoder), null);
                        return Unit.Value;
                    }

                    public Unit Box(Expression pc, Type type, Variable dest, Expression source, IBoxedExpressionVisitor data)
                    {
                        // TODO: dispatch to box, once we have BOX in BoxedExpressions
                        return Unit.Value;
                    }

                    internal void Dispatch(Expression exp, IBoxedExpressionVisitor visitor)
                    {
                        var expContext = decoder.Context.ExpressionContext;

                        expContext.Decode<IBoxedExpressionVisitor, Unit, VisitorForDispatch>(exp, this, visitor);
                    }
                }
                #endregion

                #region IFullExpressionDecoder<Type,Field,Expression> Members

                public Type System_Int32 { get { return this.decoderForMetaData.System_Int32; } }

                public bool IsReferenceType(Type type)
                {
                    return this.decoderForMetaData.IsReferenceType(type);
                }

                public bool TrySizeOfAsConstant(Expression exp, out int value)
                {
                    return TrySizeOf(exp, out value);
                }

                public bool TrySizeOf(Expression exp, out int value)
                {
                    Type type;
                    if (VisitorForSizeOf.IsSizeOf(exp, out type, this))
                    {
                        int size = this.decoderForMetaData.TypeSize(type);
                        if (size != -1)
                        {
                            value = size;
                            return true;
                        }
                        else
                        {
                            value = -1;
                            return false;
                        }
                    }
                    else
                    {
                        value = -1;
                        return false;
                    }
                }

                public bool IsNull(Expression exp)
                {
                    return this.context.ExpressionContext.IsZero(exp);
                    // return this.context.Decode<Unit, bool, VisitorForIsNull>(exp, this.visitorForIsNull, Unit.Value);
                }

                public bool TryGetAssociatedExpression(APC pc, Expression e, AssociatedInfo infoKind, out Expression info)
                {
                    Variable var = this.context.ExpressionContext.Unrefine(e);
                    return TryGetAssociatedExpression(this.context.ExpressionContext.Refine(pc, var), infoKind, out info);
                }

                public bool TryGetAssociatedExpression(Expression e, AssociatedInfo infoKind, out Expression info)
                {
                    switch (infoKind)
                    {
                        case AssociatedInfo.WritableBytes:
                            return this.context.ExpressionContext.TryGetWritableBytes(e, out info);
                        case AssociatedInfo.ArrayLength:
                            return this.context.ExpressionContext.TryGetArrayLength(e, out info);
                        default:
                            info = default(Expression);
                            return false;
                    }
                }

                public void Dispatch(Expression exp, IBoxedExpressionVisitor visitor)
                {
                    visitorForDispatch.Dispatch(exp, visitor);
                }
                #endregion
            }

            #endregion
        }
    }
}