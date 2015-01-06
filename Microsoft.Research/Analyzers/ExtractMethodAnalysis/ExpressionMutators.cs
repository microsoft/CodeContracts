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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  [ContractVerification(true)]
  public class ReplaceSymbolicValueForAccessPath<Local, Parameter, Method, Field, Property, Event, Typ, Variable, Expression, Attribute, Assembly>
    where Typ : IEquatable<Typ>
  {
    #region Object invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.context != null);
      Contract.Invariant(this.metaDataDecoder != null);
      Contract.Invariant(this.boundVariables != null);
    }

    #endregion

    #region State

    private readonly IExpressionContext<Local, Parameter, Method, Field, Typ, Expression, Variable> context;
    private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly> metaDataDecoder;
    private readonly Set<BoxedExpression> boundVariables;

    #endregion

    public ReplaceSymbolicValueForAccessPath(
      IExpressionContext<Local, Parameter, Method, Field, Typ, Expression, Variable> context,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly> metaDataDecoder)
    {
      Contract.Requires(context != null);
      Contract.Requires(metaDataDecoder != null);

      this.context = context;
      this.metaDataDecoder = metaDataDecoder;
      this.boundVariables = new Set<BoxedExpression>();
    }

    /// <summary>
    /// If failIfCannotReplaceVarsWithAccessPaths is true, whenever we cannot convert a variable into an expression, we return null.
    /// Otherwise (when failIfCannotReplaceVarsWithAccessPaths is false), then we return the same expression
    /// </summary>
    public BoxedExpression ReadAt(APC pc, BoxedExpression exp, bool failIfCannotReplaceVarsWithAccessPaths = true, Typ allowReturnValue = default(Typ))
    {
      Contract.Requires(exp != null);

      if(this.boundVariables.Contains(exp))
      {
        return exp;
      }

      if (exp.IsVariable)
      {
        Variable var;
        if (exp.TryGetFrameworkVariable(out var))
        {
          var accessPath = context.ValueContext.AccessPathList(pc, var, true, true, allowReturnValue);

          if (accessPath != null)
          {
            return BoxedExpression.Var(var, accessPath);
          }
          
          Contract.Assert(accessPath == null, "Just for readibility: we are here if we do not have an access path for the variable");

          return failIfCannotReplaceVarsWithAccessPaths ? null : exp;
        }

        return null;
      }

      BinaryOperator bop;
      BoxedExpression left, right;
      if (exp.IsBinaryExpression(out bop, out left, out right))
      {
        Variable leftVar, rightVar;
        if (left.TryGetFrameworkVariable(out leftVar) && right.IsConstant)
        {
          var typeLeft = this.context.ValueContext.GetType(pc, leftVar);
          if (typeLeft.IsNormal && IsInterestingBoundFromTheTypeRange(right, typeLeft.Value))
          {
            var recurse = ReadAt(pc, left, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);
            if (recurse != null)
              return BoxedExpression.Binary(bop, recurse, right);
          }

          return null;
        }

        if (left.IsConstant && right.TryGetFrameworkVariable(out rightVar))
        {
          var typeRight = this.context.ValueContext.GetType(pc, rightVar);
          if (typeRight.IsNormal && IsInterestingBoundFromTheTypeRange(left, typeRight.Value))
          {

            var recurse = ReadAt(pc, right, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);
            if (recurse != null)
              return BoxedExpression.Binary(bop, left, recurse);
          }

          return null;
        }

        var recurseLeft = ReadAt(pc, left, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);
        var recurseRight = ReadAt(pc, right, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);

        if (recurseLeft != null && recurseRight != null)
          return BoxedExpression.Binary(bop, recurseLeft, recurseRight);
        else
          return null;
      }

      UnaryOperator uop;
      if (exp.IsUnaryExpression(out uop, out left))
      {
        var recurse = ReadAt(pc, left, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);
        if (recurse != null)
          return BoxedExpression.Unary(uop, recurse);
        return null;
      }

      object type;
      if (exp.IsIsInstExpression(out left, out type)) {
        var recurse = ReadAt(pc, left, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);
        if (recurse != null)
        {
          return ClousotExpression<Type>.MakeIsInst((Type)type, recurse);
        }
        return null;
      }
      bool isForAll;
      BoxedExpression boundVar, inf, sup, body;
      if (exp.IsQuantifiedExpression(out isForAll, out boundVar, out inf, out sup, out body))
      {
        Contract.Assert(boundVar != null);
        Contract.Assert(inf != null);
        Contract.Assert(sup != null);
        Contract.Assert(body != null);

        this.boundVariables.Add(boundVar);

        var infRec = ReadAt(pc, inf, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);
        if (infRec != null)
        {
          var supRec = ReadAt(pc, sup, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);
          if (supRec != null)
          {
            var bodyRec = ReadAt(pc, body, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);
            if (bodyRec != null)
            {
              this.boundVariables.Remove(boundVar);

              if (isForAll)
                return new ForAllIndexedExpression(null, boundVar, infRec, supRec, bodyRec);
              else
                return new ExistsIndexedExpression(null, boundVar, infRec, supRec, bodyRec);
            }
          }
        }

        return null;
      }

      object t;
      BoxedExpression array, index;
      if (exp.IsArrayIndexExpression(out array, out index, out t) && t is Typ)
      {
        var arrayRec = ReadAt(pc, array, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);
        if (arrayRec != null)
        {
          var indexRec = ReadAt(pc, index, failIfCannotReplaceVarsWithAccessPaths, allowReturnValue);
          if (indexRec != null)
          {
            return new BoxedExpression.ArrayIndexExpression<Typ>(arrayRec, indexRec, (Typ)t);
          }
        }
      }

      return exp;
    }

    public bool IsInterestingBoundFromTheTypeRange(BoxedExpression left, Typ t)
    {
      Contract.Requires(left != null);

      int k;
      if (left.IsConstantInt(out k))
      {
        if ((k == SByte.MaxValue || k == SByte.MinValue))
          return !t.Equals(metaDataDecoder.System_Int8);

        if ((k == Int16.MaxValue || k == Int16.MinValue))
          return !t.Equals(metaDataDecoder.System_Int8) && !t.Equals(metaDataDecoder.System_Int16)
            && !t.Equals(metaDataDecoder.System_UInt8);

        if ((k == Int32.MaxValue || k == Int32.MinValue))
          return !t.Equals(metaDataDecoder.System_Int8) && !t.Equals(metaDataDecoder.System_Int16) && !t.Equals(metaDataDecoder.System_Int32)
            && !t.Equals(metaDataDecoder.System_UInt8) && !t.Equals(metaDataDecoder.System_UInt16);
      }

      long l;
      if (left.IsConstantInt64(out l))
      {
        if ((l == SByte.MaxValue || l == SByte.MinValue))
          return !t.Equals(metaDataDecoder.System_Int8);

        if ((l == Int16.MaxValue || l == Int16.MinValue))
          return !t.Equals(metaDataDecoder.System_Int8) && !t.Equals(metaDataDecoder.System_Int16)
            && !t.Equals(metaDataDecoder.System_UInt8);

        if ((l == Int32.MaxValue || l == Int32.MinValue))
          return !t.Equals(metaDataDecoder.System_Int8) && !t.Equals(metaDataDecoder.System_Int16) && !t.Equals(metaDataDecoder.System_Int32)
            && !t.Equals(metaDataDecoder.System_UInt8) && !t.Equals(metaDataDecoder.System_UInt16);

        if ((l == Int64.MaxValue || l == Int64.MinValue))
          return !t.Equals(metaDataDecoder.System_Int8) && !t.Equals(metaDataDecoder.System_Int16) && !t.Equals(metaDataDecoder.System_Int32) && !t.Equals(metaDataDecoder.System_Int64)
            && !t.Equals(metaDataDecoder.System_UInt8) && !t.Equals(metaDataDecoder.System_UInt16);
      }

      float f;
      if (left.IsConstantFloat32(out f))
      {
        if ((float)f == Single.MaxValue || (float)f == Single.MinValue)
          return !t.Equals(metaDataDecoder.System_Single);
      }

      double d;
      if (left.IsConstantFloat64(out d))
      {
        if ((double)d == Double.MaxValue || (double)d == Double.MinValue)
          return false;
      }

      return true;
    }
  }
}

