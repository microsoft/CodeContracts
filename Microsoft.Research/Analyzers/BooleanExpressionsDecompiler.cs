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


using System.Collections.Generic;
using System;
using System.Diagnostics.Contracts;
namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    //internal class ErrorInferenceException : Exception { }  // the expression should be rejected
    //internal class SkipInferenceException : Exception { }   // the expression should be accepted even if it does not typecheck

    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      [ContractVerification(true)]
      public class BooleanExpressionsDecompiler<LogOptions>
        where LogOptions : IFrameworkLogOptions
      {

        #region Object invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.MethodDriver != null);
        }
        #endregion

        #region State
        readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> MethodDriver;
        #endregion

        #region Constructor
        public BooleanExpressionsDecompiler(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> methodDriver)
        {
          Contract.Requires(methodDriver != null);

          this.MethodDriver = methodDriver;
        }
        #endregion

        #region Getters
        protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> DecoderForMetaData
        {
          get
          {
            Contract.Ensures(Contract.Result<IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>>() != null);

            return this.MethodDriver.MetaDataDecoder;
          }
        }

        protected IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context
        {
          get
          {
            Contract.Ensures(Contract.Result < IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>>() != null);

            return this.MethodDriver.Context;
          }
        }
        #endregion

        public bool FixIt(APC pc, BoxedExpression input, out BoxedExpression output)
        {
          Contract.Requires(input != null);
          Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out output) != null);

          Dictionary<BoxedExpression, Type> types;

          switch (InferTypes(pc, input, out types))
          {
            case Status.Ok:
              {
                switch (FixTypes(input, types, out output))
                {
                  case Status.Ok:
                    {
                      return true;
                    }
                  case Status.Error:
                    {
                      output = null;
                      return false;
                    }
                  case Status.Skip:
                    {
                      output = input;
                      return true;
                    }
                  default:
                    {
                      output = null;
                      return false;
                    }
                }
              }

            case Status.Error:
              {
                output = null;
                // We saw a case of non-compatibility
                return false;
              }

            case Status.Skip:
              {
                // Mic & Francesco:
                // HACK
                // Authorize expressions which cannot be checked (trust the origin about the content).
                // It can arise in some situations (post-conditions with parameters and fields)
                // so we act as if everything was ok, because we do not have the requested information
                // to test it. The problem comes from some design decision in Clousot{2} which prevents
                // informations to be retrieved by the TryGetFrameworkVariable method.
                // More precisely, when an expression like this.mField = value is seen, what is done now
                // is that the "object" object in the left variable definition is its path instead of the symbolic value.
                // That way, left & right don't have the same symbolic value and the postcondition is not considered
                // as trivial and stays in the output.
                // One solution would be to change quite a bit how things are done, but for the time being, just trust
                // the expression here.
                output = input;
                return true;
              }

            default:
              {
                output = null;
                return false; // reject by default
              }
          }
        }

        #region Type inference

        enum Status { Error, Skip, Ok, False }

        private Status InferTypes(APC pc, BoxedExpression input, out Dictionary<BoxedExpression, Type> types)
        {
          Contract.Requires(input != null);

          Contract.Ensures(Contract.Result<Status>() != Status.Ok || Contract.ValueAtReturn(out types) != null);

          types = new Dictionary<BoxedExpression, Type>();
          return InferTypesInternal(pc, input, new LiftedType(), types);
        }

        private Status InferTypesInternal(APC pc, BoxedExpression exp, LiftedType expected, Dictionary<BoxedExpression, Type> types)
        {
          Contract.Requires(exp != null);
          Contract.Requires(types != null);

          if (exp.IsSizeOf || exp.IsNull || exp.IsIsInst)
          {
            if (exp.IsSizeOf)
            {
              types[exp] = this.DecoderForMetaData.System_Int32;
            }
            else if (exp.IsNull)
            {
              int value; // null, thus value will be 0
              float valueSingle;
              double valueDouble;
              
              if (exp.IsConstantFloat64(out valueDouble))
              {
                types[exp] = this.DecoderForMetaData.System_Double;
              }
              else if (exp.IsConstantFloat32(out valueSingle))
              {
                types[exp] = this.DecoderForMetaData.System_Single;
              }
                // It is important we test for an Int after the test for doubles, because for 0.0 exp.IsConstantInt() returns true, and this will mess up the types
                // This has to do with the fact that constants are polymorphic
              else if (exp.IsConstantInt(out value))
              {
                types[exp] = this.DecoderForMetaData.System_Int32;
              }
              else
              {
                types[exp] = this.DecoderForMetaData.System_Object;
              }
            }
            else // if (exp.IsIsInst)
            {
              Contract.Assert(exp.IsIsInst);
              types[exp] = this.DecoderForMetaData.System_Object;
            }

            return Status.Ok;
          }
          else if (exp.IsConstant)
          {
            int value;
            if (exp.IsConstantInt(out value))
            {
              types[exp] = this.DecoderForMetaData.System_Int32;
            }

            return Status.Ok; // otherwise we do nothing
          }
          else if (exp.IsResult)
          {
            Contract.Assert(this.MethodDriver.Context != null); // was an assume
            Contract.Assert(this.MethodDriver.Context.MethodContext != null); // was an assume

            types[exp] = this.DecoderForMetaData.ReturnType(this.MethodDriver.Context.MethodContext.CurrentMethod);

            return Status.Ok;
          }
          else if (exp.IsVariable)
          {
            Variable var;
            if (!exp.TryGetFrameworkVariable(out var))
            {
              object t;
              if (exp.TryGetType(out t) /*&& t != null*/)
              {
                types[exp] = (Type)t;
              }
              else
              {
                //throw new SkipInferenceException(); // see above comment
                return Status.Skip;
              }
            }
            else
            {
              // Infer the type
              Contract.Assert(this.Context.ValueContext != null);
              var p = this.Context.ValueContext.AccessPathList(pc, var, true, false);
              var last = (PathElement)null;

              for (var e = p; e != null; e = e.Tail)
              {
                last = e.Head;
              }

              Type t;
              if (last != null && last.TryGetResultType(out t))
              {
                types[exp] = t;
              }
              else
              {
                var type = this.Context.ValueContext.GetType(pc, var);
                if (type.IsNormal)
                {
                  types[exp] = type.Value;
                }
              }
            }

            // don't care about ref or out, the useful information is the type we get when computing using the variable

            if (types.ContainsKey(exp))
            {
              if (this.DecoderForMetaData.IsManagedPointer(types[exp]))
              {
                types[exp] = this.DecoderForMetaData.ElementType(types[exp]);
              }
            }
            else  // The type is not there, so we give up
            {
              //throw new SkipInferenceException();
              return Status.Skip;
            }

           // return true;
            return Status.Ok;
          }
          else if (exp.IsUnary)
          {
            var success = InferTypesInternal(pc, exp.UnaryArgument, null, types);
            if (success != Status.Ok)
            {
              return success;
            }
            Type type;
            switch (exp.UnaryOp)
            {
              #region All the UnaryOp cases
              case UnaryOperator.Conv_i:
                type = this.DecoderForMetaData.System_UIntPtr;
                break;
              case UnaryOperator.Conv_i1:
                type = this.DecoderForMetaData.System_Int8;
                break;
              case UnaryOperator.Conv_i2:
                type = this.DecoderForMetaData.System_Int16;
                break;
              case UnaryOperator.Conv_i4:
                type = this.DecoderForMetaData.System_Int32;
                break;
              case UnaryOperator.Conv_i8:
                type = this.DecoderForMetaData.System_Int64;
                break;
              case UnaryOperator.Conv_r_un:
                type = this.DecoderForMetaData.System_Single;
                break;
              case UnaryOperator.Conv_r4:
                type = this.DecoderForMetaData.System_Single;
                break;
              case UnaryOperator.Conv_r8:
                type = this.DecoderForMetaData.System_Double;
                break;
              case UnaryOperator.Conv_u:
                type = this.DecoderForMetaData.System_Int32;
                break;
              case UnaryOperator.Conv_u1:
                type = this.DecoderForMetaData.System_UInt8;
                break;
              case UnaryOperator.Conv_u2:
                type = this.DecoderForMetaData.System_UInt16;
                break;
              case UnaryOperator.Conv_u4:
                type = this.DecoderForMetaData.System_UInt32;
                break;
              case UnaryOperator.Conv_u8:
                type = this.DecoderForMetaData.System_UInt64;
                break;
              case UnaryOperator.Neg:
                return InferTypesInternal(pc, exp.UnaryArgument, new LiftedType(), types);
                
              case UnaryOperator.Not:
                // F: Mic assumes expected can be null
                if (expected == null)
                {
                  //return false;
                  return Status.False;
                }
                if (!expected.TryGetType(out type))
                {
                  type = this.DecoderForMetaData.System_Int32;
                }
                break;

              case UnaryOperator.WritableBytes:
                type = this.DecoderForMetaData.System_UInt64;
                break;

              default:
                // return false;
                return Status.False;
              
              #endregion
            }

            types[exp] = type;
            // return true;
            return Status.Ok;
          }
          else if (exp.IsBinary)
          {
            Type type, typeLeft, typeRight;

            var expectedType =
              exp.BinaryOp == BinaryOperator.LogicalAnd || exp.BinaryOp == BinaryOperator.LogicalOr ?
              new LiftedType(DecoderForMetaData.System_Boolean) : new LiftedType();

            var leftResult = InferTypesInternal(pc, exp.BinaryLeft, expectedType, types);
            if(leftResult != Status.Ok)
            {
              return leftResult;
            }

            var rightResult = InferTypesInternal(pc, exp.BinaryRight, expectedType, types);
            if (rightResult != Status.Ok)
            {
              return rightResult;
            }

//            if (InferTypesInternal(pc, exp.BinaryLeft, expectedType, types)
//              &&
//              InferTypesInternal(pc, exp.BinaryRight, expectedType, types) == Status.Ok)
            {

              if (!types.TryGetValue(exp.BinaryLeft, out typeLeft)
                 || !types.TryGetValue(exp.BinaryRight, out typeRight))
              {
                //throw new SkipInferenceException(); // see above comment
                return Status.Skip;
              }

              if (!AreCompatible(typeLeft, typeRight, exp.BinaryOp))
              {
                //throw new ErrorInferenceException(); // we know it's an invalid operation and it should be rejected
                return Status.Error;
              }
              switch (exp.BinaryOp)
              {
                #region All the binaryOp cases
                case BinaryOperator.Add_Ovf:
                case BinaryOperator.Add_Ovf_Un:
                case BinaryOperator.And:
                case BinaryOperator.Add:
                case BinaryOperator.Div:
                case BinaryOperator.Div_Un:
                case BinaryOperator.Mul:
                case BinaryOperator.Mul_Ovf:
                case BinaryOperator.Mul_Ovf_Un:
                case BinaryOperator.Or:
                case BinaryOperator.Rem:
                case BinaryOperator.Rem_Un:
                case BinaryOperator.Shl:
                case BinaryOperator.Shr:
                case BinaryOperator.Shr_Un:
                case BinaryOperator.Sub:
                case BinaryOperator.Sub_Ovf:
                case BinaryOperator.Sub_Ovf_Un:
                case BinaryOperator.Xor:
                  {
                    //type = DecoderForMetaData.IsEnum(typeRight) ? typeRight : typeLeft; // prefer enums
                    if (DecoderForMetaData.IsEnum(typeLeft) || DecoderForMetaData.IsEnum(typeRight))
                    {
                      if (exp.BinaryOp == BinaryOperator.Add || exp.BinaryOp == BinaryOperator.Add_Ovf || exp.BinaryOp == BinaryOperator.Add_Ovf_Un
                        || exp.BinaryOp == BinaryOperator.Sub || exp.BinaryOp == BinaryOperator.Sub_Ovf || exp.BinaryOp == BinaryOperator.Sub_Ovf_Un
                        || exp.BinaryOp == BinaryOperator.Shl || exp.BinaryOp == BinaryOperator.Shr || exp.BinaryOp == BinaryOperator.Shr_Un)
                      //type = DecoderForMetaData.System_Int32; // arithmetic between flags always return int
                      {
                        //throw new ErrorInferenceException(); // no arithmetic on flags
                        return Status.Error;
                      }
                      else
                      {
                        type = DecoderForMetaData.IsEnum(typeLeft) ? typeLeft : typeRight;
                      }
                    }
                    else
                    {
                      type = IsUnsignedIntegerType(typeRight) ? typeRight : GetWiderType(typeLeft, typeRight); // priority for unsigned operations
                    }
                  }
                  break;

                case BinaryOperator.Ceq:
                case BinaryOperator.Cge:
                case BinaryOperator.Cge_Un:
                case BinaryOperator.Cgt:
                case BinaryOperator.Cgt_Un:
                case BinaryOperator.Cle:
                case BinaryOperator.Cle_Un:
                case BinaryOperator.Clt:
                case BinaryOperator.Clt_Un:
                case BinaryOperator.Cne_Un:
                case BinaryOperator.Cobjeq:
                case BinaryOperator.LogicalAnd:
                case BinaryOperator.LogicalOr:
                  {
                    type = this.DecoderForMetaData.System_Boolean;
                  }
                  break;

                default:
                  //return false;

                  return Status.False;
                #endregion
              }

              types[exp] = type;
              //return true;
              return Status.Ok;
            }
          }
          else if (exp.IsArrayIndex)
          {
            types[exp] = ((BoxedExpression.ArrayIndexExpression<Type>)exp).Type;
            
            //return true;
            return Status.Ok;
          }
          else if (exp.IsQuantified)
          {
            types[exp] = this.DecoderForMetaData.System_Boolean;

            // return true;
            return Status.Ok;
          }
#if DEBUG
          Console.WriteLine("Unknown BoxedExpression kind (in InferTypes): {0}", exp);
#endif
          // no other case?
          // return false;
          return Status.False;
        }

        private bool IsNarrowerType(Type left, Type right)
        {
          if (GetWiderType(left, right).Equals(right))
            return true;
          return false; 
        }

        private Type GetWiderType(Type left, Type right)
        {
          if(left.Equals(right))
          {
            return left;
          }
          
          var int8 = DecoderForMetaData.System_Int8;
          var int16 = DecoderForMetaData.System_Int16;
          var int32 = DecoderForMetaData.System_Int32;
          var int64 = DecoderForMetaData.System_Int64;

          if (left.Equals(int8))
          {
            return right; // right can ve int8, 16, 32, 64
          }
          if (right.Equals(int8))
          {
            return left; // left can be int16, 32, 64
          }
          if (left.Equals(int16))
          {
            return right; // right can be int16, 32, 64
          }
          if (right.Equals(int16))
          {
            return left; // left can be int32, 64
          }
          if (left.Equals(int32))
          {
            return right; // right can be int32, 64
          }
          if (right.Equals(int32))
          {
            return left; // left can be int64
          }


          return left;
        }

        #endregion

        #region Type fixing
        private bool IsUnsignedIntegerType(Type t)
        {
          return
            t.Equals(DecoderForMetaData.System_Char)
            || t.Equals(DecoderForMetaData.System_UInt8)
            || t.Equals(DecoderForMetaData.System_UInt16)
            || t.Equals(DecoderForMetaData.System_UInt32)
            || t.Equals(DecoderForMetaData.System_UInt64);
        }

        private bool IsSignedIntegerType(Type t)
        {
          return
              t.Equals(DecoderForMetaData.System_Int8)
              || t.Equals(DecoderForMetaData.System_Int16)
              || t.Equals(DecoderForMetaData.System_Int32)
              || t.Equals(DecoderForMetaData.System_Int64);
        }

        private Type ToUnsignedType(Type t)
        {
          if (IsUnsignedIntegerType(t))
            return t;
          else if (IsSignedIntegerType(t))
          {
            if (t.Equals(DecoderForMetaData.System_Int8))
              return DecoderForMetaData.System_UInt8;
            else if (t.Equals(DecoderForMetaData.System_Int16))
              return DecoderForMetaData.System_UInt16;
            else if (t.Equals(DecoderForMetaData.System_Int32))
              return DecoderForMetaData.System_UInt32;
            else if (t.Equals(DecoderForMetaData.System_Int64))
              return DecoderForMetaData.System_UInt64;
          }
          return t;
        }

        private Type ToSignedType(Type t)
        {
          if (IsSignedIntegerType(t))
            return t;
          else if (IsUnsignedIntegerType(t))
          {
            if (t.Equals(DecoderForMetaData.System_UInt8))
              return DecoderForMetaData.System_Int8;
            else if (t.Equals(DecoderForMetaData.System_UInt16))
              return DecoderForMetaData.System_Int16;
            else if (t.Equals(DecoderForMetaData.System_UInt32))
              return DecoderForMetaData.System_Int32;
            else if (t.Equals(DecoderForMetaData.System_UInt64))
              return DecoderForMetaData.System_Int64;
          }
          return t;
        }

        private bool IsIntegerType(Type t)
        {
          return IsUnsignedIntegerType(t) || IsSignedIntegerType(t);
        }

        private bool AreCompatible(Type t1, Type t2, BinaryOperator op)
        {
          if (t1 == null || t2 == null)
            return false;

          if (t1.Equals(default(Type)) || t2.Equals(default(Type)))
            return false;

          if (t1.Equals(t2))
            return true;

          if (op == BinaryOperator.Cobjeq
            || op == BinaryOperator.Ceq
            || op == BinaryOperator.Cne_Un
            || op == BinaryOperator.Or
            || op == BinaryOperator.Xor
            || op == BinaryOperator.LogicalAnd
            || op == BinaryOperator.LogicalOr)
          {
            // It will be fixed as "true" or "false" by FixTypes
            if (t1.Equals(DecoderForMetaData.System_Boolean) && IsIntegerType(t2)
              || t2.Equals(DecoderForMetaData.System_Boolean) && IsIntegerType(t1))
              return true;
          }

          if (op == BinaryOperator.LogicalAnd || op == BinaryOperator.LogicalOr)
          {
            if ((IsReferenceType(t1) || IsBool(t1)) && (IsReferenceType(t2) || IsBool(t2))) return true;
          }

          if (op == BinaryOperator.Cobjeq || op == BinaryOperator.Ceq || op == BinaryOperator.Cne_Un)
          {
            // object == another object
            // the check here could be more thorough, like testing whether types are actually
            // convertible, but it didn't prove to be necessary
            if (DecoderForMetaData.IsReferenceType(t1) && DecoderForMetaData.IsReferenceType(t2))
              return true;
            if (DecoderForMetaData.IsFormalTypeParameter(t1) || DecoderForMetaData.IsFormalTypeParameter(t2))
            {
                return true;
            }
          }

          if (op == BinaryOperator.Cobjeq || op == BinaryOperator.Ceq || op == BinaryOperator.Cne_Un)
          {
            // object == 0 (will be fixed to object == null)
            if ((DecoderForMetaData.IsReferenceType(t1) && IsIntegerType(t2))
              || (DecoderForMetaData.IsReferenceType(t2) && IsIntegerType(t1)))
              return true;
          }

          if (IsIntegerType(t1) && IsIntegerType(t2))
          {
            if (op == BinaryOperator.Sub || op == BinaryOperator.Sub_Ovf || op == BinaryOperator.Sub_Ovf_Un)
            {
              if (IsUnsignedIntegerType(t1) || IsUnsignedIntegerType(t2))
                return false; // No substraction on unsigned types
            }
            return true;
          }

          if (DecoderForMetaData.IsEnum(t1) && DecoderForMetaData.IsEnum(t2))
            return t1.Equals(t2);

          // Authorize mixing int and enums (including uint, ...), for "flag & int"
          // Then it's fixed int the end by FixTypesInternal
          if ((DecoderForMetaData.IsEnum(t1) && IsIntegerType(t2))
            || (DecoderForMetaData.IsEnum(t2) && IsIntegerType(t1)))
            return true;

          return false;
        }

        private Status FixTypes(BoxedExpression exp, Dictionary<BoxedExpression, Type> types, out BoxedExpression result)
        {
          Contract.Requires(exp != null);
          Contract.Requires(types != null);

          Contract.Ensures(Contract.Result<Status>() != Status.Ok || Contract.ValueAtReturn(out result) != null);

          return FixTypesInternal(exp, types, new LiftedType(), out result);
        }

        private Status FixTypesInternal(BoxedExpression exp, Dictionary<BoxedExpression, Type> types, LiftedType expectedType, out BoxedExpression result)
        {
          Contract.Requires(exp != null);
          Contract.Requires(types != null);
          Contract.Requires(expectedType != null);

          Contract.Ensures(Contract.Result<Status>() != Status.Ok || Contract.ValueAtReturn(out result) != null);

          Type prevType, expectTyp;
          if (exp.IsVariable && types.TryGetValue(exp, out prevType) && expectedType.TryGetType(out expectTyp) && prevType.Equals(expectTyp))
          {
            result = exp; 
            return Status.Ok;
          }
          
          int value;
          if (exp.IsResult)
          {
            Type restype;
            if (types.TryGetValue(exp, out restype))
            {
              if (this.DecoderForMetaData.IsUnmanagedPointer(restype))
              {
                //throw new ErrorInferenceException(); // unmanaged pointers are prohibited as template arguments
                result = null;
                return Status.Error;                
              }
            }
          }
          else if (exp.IsConstantInt(out value))
          {
            if (IsBool(expectedType))
            {
              result = BoxedExpression.ConstBool(value, this.DecoderForMetaData);
              return Status.Ok;
            }
            Type t;
            if (IsEnum(expectedType, out t))
            {
              string name = null;
              Field f;
              if(this.DecoderForMetaData.TryGetEnumFieldNameFromValue(value, t, out f))
              {
                name = this.DecoderForMetaData.Name(f);
              }

              result = BoxedExpression.ConstCast(value, name, this.DecoderForMetaData.System_Int32, t, this.DecoderForMetaData);
              return Status.Ok;
            }
            if(IsReference(expectedType, out t))
            {
              if (value == 0)
              {
                if (this.MethodDriver.MetaDataDecoder.IsReferenceType(t))
                {
                  result = BoxedExpression.Const(null, t, this.DecoderForMetaData); // no need to cast null to System.Object
                  return Status.Ok;
                }
                else
                {
                  result = BoxedExpression.ConstCast(null, null, t, t, this.DecoderForMetaData);
                  return Status.Ok;
                }
              }
            }
          }
          else if (exp.IsVariable)
          {
            Type t;
            if (types.TryGetValue(exp, out t))
            {
              Type expected_t;
              if (expectedType.TryGetType(out expected_t))
              {
                // The type is not the one expected but we're dealing with integers. Let's do a brutal cast
                if (!t.Equals(expected_t))
                {
                  // Cast the variable as a expected_t
                  Variable var;
                  if (!exp.TryGetFrameworkVariable(out var))
                  {
                    result = exp; // not supposed to happen, the call must have succeeded before in InferTypes
                    return Status.Ok;
                  }

                  if (IsSignedIntegerType(t) && IsUnsignedIntegerType(expected_t)) // bypass signed/unsigned restriction because contracts are right
                  {
                    result = BoxedExpression.VarCastUnchecked(var, exp.AccessPath, t, expected_t);
                    return Status.Ok;
                  }
                  else if (IsReferenceType(t) && IsBool(expected_t))
                  {
                    // insert x != null
                    result = BoxedExpression.Binary(BinaryOperator.Cne_Un, exp, BoxedExpression.Const(null, t, this.DecoderForMetaData));
                    return Status.Ok;
                  }
                  else
                  {
                    // don't cast to System.Object, it's never relevant (it could be generated for null comparisons)
                    //if (expected_t.Equals(this.DecoderForMetaData.System_Object))
                    //  return exp;
                    //else
                    result = IsNarrowerType(t, expected_t) ? // if t can be widened to expected_t, avoid generating the cast, as the c# compiler will do it
                      exp :
                      BoxedExpression.VarCast(var, exp.AccessPath, t, expected_t);

                    return Status.Ok;
                  }
                }
              }
            }
          }
          else if (exp.IsBinary)
          {
            Type typeLeft, typeRight;
            if (types.TryGetValue(exp.BinaryLeft, out typeLeft) && types.TryGetValue(exp.BinaryRight, out typeRight))
            {
              Contract.Assume(typeLeft != null);
              Contract.Assume(typeRight != null);
              var newType = Join(typeLeft, typeRight, exp.BinaryOp);

              if (newType == null)
              {
                result = null;
                return Status.Error;
              }

              if (this.DecoderForMetaData.IsFormalTypeParameter(typeLeft) || this.DecoderForMetaData.IsFormalTypeParameter(typeRight))
              {
                // Case when the two sides are of type coming from a formal parameter
                if (exp.BinaryOp == BinaryOperator.Ceq || exp.BinaryOp == BinaryOperator.Cobjeq)
                {
                  result = BoxedExpression.BinaryMethodToCall(exp.BinaryOp, exp.BinaryLeft, exp.BinaryRight, "Equals");
                  return Status.Ok;
                }
                else
                {
                  //throw new SkipInferenceException(); // trust the contract... ?
                  result = null;
                  return Status.Skip;
                }
              }

              BoxedExpression bLeft, bRight;

              var leftStatus = FixTypesInternal(exp.BinaryLeft, types, newType, out bLeft);

              if (leftStatus != Status.Ok)
              {
                result = null;
                return leftStatus;
              }

              var rightStatus = FixTypesInternal(exp.BinaryRight, types, newType, out bRight);
              if(rightStatus != Status.Ok)
              {
                result = null;
                return rightStatus;
              }

              if (!newType.Equals(expectedType))
              {
                Type t;
                if (expectedType.TryGetType(out t))
                {
                  // Cast to bool only if it's not already the case
                  if (exp.BinaryOp != BinaryOperator.Cobjeq && exp.BinaryOp != BinaryOperator.Cne_Un && exp.BinaryOp != BinaryOperator.Ceq)
                  {
                    // Michael's code
                    // return BoxedExpression.BinaryCast(exp.BinaryOp, bLeft, bRight, t);
                    switch (exp.BinaryOp)
                    {
                      case BinaryOperator.Cge:
                      case BinaryOperator.Cge_Un:
                      case BinaryOperator.Cgt:
                      case BinaryOperator.Cgt_Un:
                      case BinaryOperator.Cle:
                      case BinaryOperator.Cle_Un:
                      case BinaryOperator.Clt:
                      case BinaryOperator.Clt_Un:
                        {
                          result = BoxedExpression.Binary(exp.BinaryOp, bLeft, bRight);
                          return Status.Ok;
                        }
                      default:
                        {
                          result = BoxedExpression.Binary(
                            BinaryOperator.Cne_Un,
                            BoxedExpression.Binary(exp.BinaryOp, bLeft, bRight),
                            BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));

                          return Status.Ok;
                        }
                    }
                  }
                }

                result = BoxedExpression.Binary(exp.BinaryOp, bLeft, bRight);
                return Status.Ok;
              }
              else
              {
                result = BoxedExpression.Binary(exp.BinaryOp, bLeft, bRight);
                return Status.Ok;
              }
            }
          }
          else if (exp.IsUnary)
          {
            Type t_cur = types[exp];
            Type t_expected;
            if (expectedType.TryGetType(out t_expected))
            {
              if (!t_cur.Equals(t_expected))
              {
                if (IsSignedIntegerType(t_cur) && IsUnsignedIntegerType(t_expected)) // bypass signed/unsigned restriction because contracts are right
                {
                  result = BoxedExpression.UnaryCastUnchecked(exp.UnaryOp, exp.UnaryArgument, t_expected);
                  return Status.Ok;
                }
                else
                {
                  result = BoxedExpression.UnaryCast(exp.UnaryOp, exp.UnaryArgument, t_expected);
                  return Status.Ok;
                }
              }
            }
          }
          else if (exp.IsIsInst)
          {
            // return " result != null "
            result = BoxedExpression.Binary(BinaryOperator.Cne_Un, exp, BoxedExpression.Const(null, this.DecoderForMetaData.System_Object, this.DecoderForMetaData));
            return Status.Ok;
          }

          // Nothing to do in all the other cases
          result = exp;
          return Status.Ok;
        }

        private bool IsBool(LiftedType type)
        {
          Contract.Requires(type != null);

          Type t;
          return type.TryGetType(out t) && IsBool(t);
        }

        private bool IsBool(Type type)
        {
          return type.Equals(this.DecoderForMetaData.System_Boolean);
        }

        private bool IsReferenceType(Type type)
        {
          return this.DecoderForMetaData.IsReferenceType(type);
        }

        private bool IsEnum(LiftedType type, out Type t)
        {
          Contract.Requires(type != null);

          return type.TryGetType(out t) && this.DecoderForMetaData.IsEnum(t);
        }

        private bool IsReference(LiftedType type, out Type t)
        {
          Contract.Requires(type != null);

          return type.TryGetType(out t) && this.DecoderForMetaData.IsReferenceType(t);
        }

        private LiftedType Join(Type left, Type right, BinaryOperator bop)
        {
          Contract.Requires(left != null);
          Contract.Requires(right != null);

          if (this.DecoderForMetaData.System_Boolean.Equals(left) || this.DecoderForMetaData.System_Boolean.Equals(right))
          {
            return new LiftedType(this.DecoderForMetaData.System_Boolean);
          }
          // fix type for Flag & int
          else if (this.DecoderForMetaData.IsEnum(left))
          {
            return new LiftedType(left);
          }
          else if (this.DecoderForMetaData.IsEnum(right))
          {
            return new LiftedType(right);
          }
          else if (this.DecoderForMetaData.System_Int32.Equals(left))
          {
            return new LiftedType(right);
          }
          else if (this.DecoderForMetaData.System_Int32.Equals(right))
          {
            return new LiftedType(left);
          }
          else if ((this.DecoderForMetaData.System_Object.Equals(left) && this.DecoderForMetaData.System_String.Equals(right))
            || (this.DecoderForMetaData.System_Object.Equals(right) && this.DecoderForMetaData.System_String.Equals(left)))
          {
            // Special case for object == string, cast the object to string
            return new LiftedType(this.DecoderForMetaData.System_String);
          }
          else if (DecoderForMetaData.IsReferenceType(left) && !DecoderForMetaData.IsReferenceType(right))
          {
            return new LiftedType(left);
          }
          else if (!DecoderForMetaData.IsReferenceType(left) && DecoderForMetaData.IsReferenceType(right))
          {
            return new LiftedType(right);
          }
          else if (DecoderForMetaData.IsReferenceType(left) && DecoderForMetaData.IsReferenceType(right))
          {
            if (bop == BinaryOperator.Ceq || bop == BinaryOperator.Cobjeq || bop == BinaryOperator.Cne_Un)
            {
              // object == another object
              if (DecoderForMetaData.DerivesFrom(left, right))
                return new LiftedType(left); // no need to cast, C# rules will do it for us
              else if (DecoderForMetaData.DerivesFrom(right, left))
                return new LiftedType(right); // no need to cast, C# rules will do it for us
            }
          }
          else if (left.Equals(right))
          {
            return new LiftedType(left);
          }
          //return new LiftedType(); // Top
          // throw new ErrorInferenceException(); // cannot be united
          return null;
        }

        #endregion

        #region ExpectedType
        private class LiftedType
        {
          private readonly Type type;
          private readonly bool set;

          public LiftedType(Type type)
          {
            this.type = type;
            this.set = true;
          }

          public LiftedType()
          {
            this.type = default(Type);
            this.set = false;
          }

          public bool TryGetType(out Type t)
          {
            t = this.type;
            return this.set;
          }

          public override bool Equals(object obj)
          {
            if (obj is LiftedType)
            {
              var lfr = obj as LiftedType;
              if (!this.set)
                return !lfr.set;
              return this.type.Equals(lfr.type);
            }
            return false;
          }

          public override int GetHashCode()
          {
            return this.type.GetHashCode();
          }

          public override string ToString()
          {
            return this.set ? type.ToString() : "Top";
          }
        }
        #endregion
      }
    }
  }
}