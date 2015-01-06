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

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  [ContractVerification(false)]
  public class BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Var> 
    : IFactory<BoxedExpression>
  {
    #region Object invariant
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.True != null);
      Contract.Invariant(this.False != null);
      Contract.Invariant(this.One != null);
      Contract.Invariant(this.medataDecoder != null);
    }

    #endregion

    #region State

    readonly private BoxedExpression True;
    readonly private BoxedExpression False;
    readonly private BoxedExpression One;    
    readonly private IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> medataDecoder;
    readonly private BoxedExpression name;
    readonly private BoxedExpression boundVariable;

    readonly private Func<Var, BoxedExpression> VariableConverter;
    readonly private Func<Var, BoxedExpression> ArrayLength;

    #endregion
    
    public BoxedExpressionFactory(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadataDecoder, Func<Var, BoxedExpression> ArrayLength = null)
      : this(null, null, null, metadataDecoder, ArrayLength)
    {
      Contract.Requires(metadataDecoder != null);
    }

    public BoxedExpressionFactory(
      BoxedExpression boundVar,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadataDecoder, Func<Var, BoxedExpression> ArrayLength = null)
      : this(boundVar, null, null, metadataDecoder, ArrayLength)
    {
      Contract.Requires(metadataDecoder != null);
    }

    public BoxedExpressionFactory(
      BoxedExpression boundVar, BoxedExpression name,
      Func<Var, BoxedExpression> VariableConverter,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadataDecoder,
      Func<Var, BoxedExpression> ArrayLength)
    {
      Contract.Requires(metadataDecoder != null);
      this.boundVariable = boundVar;
      this.name = name;
      this.VariableConverter = VariableConverter;
      this.medataDecoder = metadataDecoder;
      this.ArrayLength = ArrayLength;
      this.True = BoxedExpression.Const(true, medataDecoder.System_Boolean, metadataDecoder);
      this.False = BoxedExpression.Const(false, metadataDecoder.System_Boolean, metadataDecoder);
      this.One = BoxedExpression.Const(1, metadataDecoder.System_Int32, metadataDecoder);
    }

    #region IFactory<BoxedExpression> Members

    public IFactory<BoxedExpression> FactoryWithName(BoxedExpression name)
    {
      return new BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Var>(name, this.name, this.VariableConverter, this.medataDecoder, this.ArrayLength);
    }

    public BoxedExpression Constant(int constant)
    {
      if (constant == 1)
        return this.IdentityForMul;
      else
        return BoxedExpression.Const(constant, medataDecoder.System_Int32, this.medataDecoder);
    }

    public BoxedExpression Constant(long constant)
    {
      if (constant == 1)
        return this.IdentityForMul;
      else
        return BoxedExpression.Const(constant, medataDecoder.System_Int64, this.medataDecoder);
    }

    public BoxedExpression Constant(Rational constant)
    {
      return constant.To(this);
    }

    public BoxedExpression Constant(bool constant)
    {
      if (constant)
        return this.True;
      else
        return this.False;
    }

    public BoxedExpression Constant(double constant)
    {
      return BoxedExpression.Const(constant, this.medataDecoder.System_Double, this.medataDecoder);
    }

    public BoxedExpression Variable(object variable)
    {
      var variableAsBoxedExp = variable as BoxedExpression;
      if (variableAsBoxedExp != null)
      {
        return variableAsBoxedExp;
      }

      if (VariableConverter != null)
      {
        var variableAsBoxedVar = variable as BoxedVariable<Var>;
        if (variableAsBoxedVar != null)
        {
          Var v;
          if (variableAsBoxedVar.TryUnpackVariable(out v))
          {
            return VariableConverter(v);
          }
        }
        else if (variable is Var)
        {
          return VariableConverter((Var)variable);
        }
      }

      return BoxedExpression.Var(variable);
    }

    public BoxedExpression Add(BoxedExpression left, BoxedExpression right)
    {
      if (left == this.IdentityForAdd)
        return right;
      if (right == this.IdentityForAdd)
        return left;
      return BoxedExpression.Binary(BinaryOperator.Add, left, right);
    }

    public BoxedExpression Sub(BoxedExpression left, BoxedExpression right)
    {
      return BoxedExpression.Binary(BinaryOperator.Sub, left, right);
    }

    public BoxedExpression Mul(BoxedExpression left, BoxedExpression right)
    {
      if (left == this.One)
        return right;
      if (right == this.One)
        return left;
      
      return BoxedExpression.Binary(BinaryOperator.Mul, left, right);
    }

    public BoxedExpression Div(BoxedExpression left, BoxedExpression right)
    {
      return BoxedExpression.Binary(BinaryOperator.Div, left, right);
    }

    public BoxedExpression EqualTo(BoxedExpression left, BoxedExpression right)
    {
      return BoxedExpression.Binary(BinaryOperator.Ceq, left, right);
    }

    public BoxedExpression NotEqualTo(BoxedExpression left, BoxedExpression right)
    {
      return BoxedExpression.Binary(BinaryOperator.Cne_Un, left, right);
    }

    public BoxedExpression LessThan(BoxedExpression left, BoxedExpression right)
    {
      if (left.Equals(right))
        return False;
      else
        return BoxedExpression.Binary(BinaryOperator.Clt, left, right);
    }

    public BoxedExpression LessEqualThan(BoxedExpression left, BoxedExpression right)
    {
      if (left.Equals(right))
        return True;
      else
        return BoxedExpression.Binary(BinaryOperator.Cle, left, right);
    }

    public BoxedExpression And(BoxedExpression left, BoxedExpression right)
    {
      if (left == this.IdentityForAnd)
        return right;
      if (right == this.IdentityForAnd)
        return left;
      else
       return BoxedExpression.Binary(BinaryOperator.LogicalAnd, left, right);
    }

    public BoxedExpression Or(BoxedExpression left, BoxedExpression right)
    {
      if (left == this.IdentityForOr)
        return right;
      if (right == this.IdentityForOr)
        return left;
      else
      return BoxedExpression.Binary(BinaryOperator.LogicalOr, left, right);
    }


    public BoxedExpression IdentityForMul
    {
      get
      {
        return this.One;
      }
    }

    public BoxedExpression Null
    {
      get { return BoxedExpression.Const(null, this.medataDecoder.System_Object, this.medataDecoder); }
    }

    public BoxedExpression IdentityForAnd
    {
      get { return this.True; }
    }

    public BoxedExpression IdentityForOr
    {
      get { return this.False; }
    }

    public BoxedExpression IdentityForAdd
    {
      get { return null; }
    }

    public BoxedExpression ForAll(BoxedExpression inf, BoxedExpression sup, BoxedExpression body)
    {
      return new ForAllIndexedExpression(null, this.boundVariable, inf, sup, body);
    }

    public BoxedExpression Exists(BoxedExpression inf, BoxedExpression sup, BoxedExpression body)
    {
      return new ExistsIndexedExpression(null, this.boundVariable, inf, sup, body);
    }

    public bool TryGetName(out BoxedExpression name)
    {
      name = this.name;
      return name != null;
    }

    public bool TryGetBoundVariable(out BoxedExpression boundVar)
    {
      boundVar = this.boundVariable;
      return boundVar != null;
    }

    public BoxedExpression ArrayIndex(BoxedExpression array, BoxedExpression index)
    {
      return new BoxedExpression.ArrayIndexExpression<Type>(array, index, this.medataDecoder.System_Object);
    }

    public bool TryArrayLengthName(BoxedExpression array, out BoxedExpression name)
    {
      Var v;
      if (ArrayLength != null && array.TryGetFrameworkVariable(out v))
      {
        name = ArrayLength(v);
      }
      else
      {
        name = null;
      }

      return name != null;
    }

    public List<BoxedExpression> SplitAnd(BoxedExpression t)
    {
      var result = new List<BoxedExpression>();

      Recurse(t, result);

      return result;
    }

    private void Recurse(BoxedExpression be, List<BoxedExpression> result)
    {
      BinaryOperator bop;
      BoxedExpression left, right;

      if (be.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.LogicalAnd)
      {
        Recurse(left, result);
        Recurse(right, result);
      }
      else
      {
        result.Add(be);
      }

    }

    #endregion
  }
}
