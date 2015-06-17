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

#if INCLUDE_UNSAFE_ANALYSIS
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains;
using ADomainKind = Microsoft.Research.CodeAnalysis.Analyzers.DomainKind;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    /// <summary>
    /// Entry point to run the Buffer analysis
    /// </summary>
    public static IMethodResult<Variable> RunBufferAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
    (
      string methodName,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
      List<Analyzers.Buffers.Options> options,
      Predicate<APC> cachePCs
    )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      var analysis =
       new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.BufferAnalysis(methodName, driver, options, cachePCs);

      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.RunTheAnalysis(methodName, driver, analysis); 
    }

    /// <summary>
    /// This class is just for binding types for the internal clases
    /// </summary>
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      #region Facility to forward operations on the abstract domain (implementation of IAbstractDomainOperations)
      public class AbstractOperationsImplementationBuffer : IAbstractDomainOperations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression,Variable, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>
      {
        public bool LookupState(IMethodResult<Variable> mr, APC pc, out INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate)
        {
          astate = null;
          BufferAnalysis an = mr as BufferAnalysis;
          if (an == null)
            return false;

          return an.PreStateLookup(pc, out astate);
        }

        public INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Join(IMethodResult<Variable> mr, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate1, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate2)
        {
          BufferAnalysis an = mr as BufferAnalysis;
          if (an == null)
            return null;

          bool bWeaker;
          return an.Join(new Pair<APC, APC>(), astate1, astate2, out bWeaker, false);
        }

        public List<BoxedExpression> ExtractAssertions(
          IMethodResult<Variable> mr,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder)
        {
          BufferAnalysis an = mr as BufferAnalysis;
          if (an == null)
            return null;

          BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly> br = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(context, metaDataDecoder);

          return an.ToListOfBoxedExpressions(astate, br);
        }

        public bool AssignInParallel(IMethodResult<Variable> mr, ref INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> mapping, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          BufferAnalysis an = mr as BufferAnalysis;
          if (an == null)
            return false;

          astate.AssignInParallel(mapping, convert);
          return true;
        }
      }
      #endregion

      public class BufferAnalysis :
        GenericNumericalAnalysis<Analyzers.Buffers.Options>
        //NumericalAnalysis<Analyzers.Buffers.BufferOptions>
      {
        #region private state

        readonly private List<Analyzers.Buffers.Options> specificoptions;

        #endregion
        
        internal BufferAnalysis(
          string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          List<Analyzers.Buffers.Options> optionsList,
          Predicate<APC> cachePCs
        )
          : base(methodName, mdriver, optionsList[0], cachePCs)
          // : base(methodName, mdriver, optionsList)
        {
          Contract.Requires(mdriver != null);
          Contract.Requires(optionsList.Count > 0);

          this.specificoptions = optionsList;

        }

        #region Transfer functions for instructions
        /// <summary>
        /// WB(dest) == size
        /// </summary>
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Localloc(APC pc, Variable dest, Variable size, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          Variable WB_dest;
          if (this.Context.ValueContext.TryGetWritableBytes(this.Context.MethodContext.CFG.Post(pc), dest, out WB_dest))
          {
            var sizeExp = ToBoxedExpression(pc, size);

            var tryConst = data.BoundsFor(sizeExp);
            if (tryConst.IsSingleton)
            {
              data.AssumeInDisInterval(new BoxedVariable<Variable>(WB_dest), tryConst);
            }

            data = data.TestTrueGeqZero(sizeExp);
            data = data.TestTrueEqual(ToBoxedExpression(pc, WB_dest), sizeExp);
          }

          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldflda(APC pc, Field field, Variable dest, Variable obj, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {         
          return HandleFldInstruction(pc, obj, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          return HandleFldInstruction(pc, obj, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          return HandleFldInstruction(pc, obj, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          return HandleIndInstruction(pc, type, ptr, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          return HandleIndInstruction(pc, type, ptr, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldarga(APC pc, Parameter argument, bool isOld, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          return HandleLoadAddrInstruction(pc, dest, this.DecoderForMetaData.ParameterType(argument), data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldloca(APC pc, Local local, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          return HandleLoadAddrInstruction(pc, dest, this.DecoderForMetaData.LocalType(local), data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          // WB(dest) == (array.Length -index) * typesize 

          var postPC = this.Context.MethodContext.CFG.Post(pc);
          var arrType = this.Context.ValueContext.GetType(postPC, array);

          Variable len_arr, wb_dest;
          if (arrType.IsNormal
            && this.Context.ValueContext.TryGetWritableBytes(postPC, dest, out wb_dest)
            && this.Context.ValueContext.TryGetArrayLength(postPC, array, out len_arr))
          {
            var typesize = this.DecoderForMetaData.TypeSize(this.DecoderForMetaData.ElementType(arrType.Value));

            if (typesize != -1)
            {
              var elements = BoxedExpression.Binary(BinaryOperator.Sub, ToBoxedExpression(pc, len_arr), ToBoxedExpression(pc, index));
              var bytes = BoxedExpression.Binary(BinaryOperator.Mul, elements, BoxedExpression.Const(typesize, this.DecoderForMetaData.System_UInt32, this.DecoderForMetaData));

              data = data.TestTrueEqual(ToBoxedExpression(postPC, wb_dest), bytes);
            }
          }

          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList lengths, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var result = base.Newarray<ArgList>(pc, type, dest, lengths, data);

          // F: HACK HACK HACK to cut some paths in fixed statements
          result = result.TestTrueEqual(ToBoxedExpression(pc, dest), BoxedExpression.Const(1, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));

          return result;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = base.Unary(pc, op, overflow, unsigned, dest, source, data);
          switch (op)
          {
            case UnaryOperator.Conv_i:
              {
              }
              break;

            default:
              break;
          }

          return data;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (!this.Options.FastCheck)
          {
            // This will introduce a lot more facts, 
            data = base.Binary(pc, op, dest, s1, s2, data);
          }

          var postPC = this.Context.MethodContext.CFG.Post(pc);
          var destType = this.Context.ValueContext.GetType(postPC, dest);
          if (destType.IsNormal && this.DecoderForMetaData.IsUnmanagedPointer(destType.Value))
          {
            Variable wb_dest;
            if (this.Context.ValueContext.TryGetWritableBytes(this.Context.MethodContext.CFG.Post(pc), dest, out wb_dest))
            {
              // Two cases, depending if it is a pointer arithmetic, or a string
              var sourcetype = this.Context.ValueContext.GetType(postPC, s1);

              if (sourcetype.IsNormal && sourcetype.Value.Equals(this.DecoderForMetaData.System_String))
              {
                Variable source_Len;
                if (this.Context.ValueContext.TryGetArrayLength(postPC, s1, out source_Len))
                {
                  data = data.TestTrueEqual(
                    ToBoxedExpression(postPC, wb_dest),
                    BoxedExpression.Binary(BinaryOperator.Mul,
                      BoxedExpression.Const(2, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData), ToBoxedExpression(postPC, source_Len)
                      )
                      );
                }
              }
              else
              {
                Variable left, right;

                if (!this.Context.ValueContext.TryGetWritableBytes(pc, s1, out left))
                {
                  left = s1;
                }

                if (!this.Context.ValueContext.TryGetWritableBytes(pc, s2, out right))
                {
                  right = s2;
                }

                var wb_destExp = ToBoxedExpression(pc, wb_dest);
                var leftExp = ToBoxedExpression(pc, left);
                var rightExp = ToBoxedExpression(pc, right);

                // We should be careful that wb_destExp can be negative!
                // we van have wb_destExp == wb + k, with k > 0
                // if we do not know anything about wb, it is unsound to assume that wb_destExp >= 0, because this will imply that wb >= k, which is wrong (in general)
                BoxedExpression newConstraint;
                if (TryBuildNewWB(op, wb_destExp, leftExp, rightExp, out newConstraint))
                {
                  data = data.TestTrue(newConstraint) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
                }
              }
            }
          }

          return data;
        }

        private bool TryBuildNewWB(BinaryOperator op, BoxedExpression wb_destExp, BoxedExpression leftExp, BoxedExpression rightExp, out BoxedExpression newConstraint)
        {
          BinaryOperator flippedOp;
          switch (op)
          {
            case BinaryOperator.Add:
              flippedOp = BinaryOperator.Sub;
              break;

            case BinaryOperator.Sub:
              flippedOp = BinaryOperator.Add;
              break;

            default:
              newConstraint = default(BoxedExpression);
              return false;
          }

          newConstraint = BoxedExpression.Binary(
            BinaryOperator.Ceq,
            wb_destExp,
            BoxedExpression.Binary(flippedOp, leftExp, rightExp));

          return true;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleFldInstruction(APC pc, Variable obj, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var type = this.Context.ValueContext.GetType(pc, obj);
          if (type.IsNormal)
          {
            data = HandleIndInstruction(pc, type.Value, obj, data);
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleIndInstruction(APC pc, Type type, Variable ptr, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          BoxedExpression lowerbound, upperbound;
          if (this.TryInferSafeBufferAccessConstraints(pc, type, ptr, out lowerbound, out upperbound))
          {
            data = (INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>)data.TestTrue(lowerbound).TestTrue(upperbound);
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleLoadAddrInstruction(APC pc, Variable dest, FlatDomain<Type> t, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (!t.IsNormal)
            return data;

          return HandleLoadAddrInstruction(pc, dest, t.Value, data); 
        }

        /// <summary>
        /// wb(ptr) == sizeof(type)
        /// </summary>
        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleLoadAddrInstruction(APC pc, Variable ptr, Type type, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          Variable wb_ptr;
          var typeSize = this.DecoderForMetaData.TypeSize(type);
          if (typeSize != -1 && this.Context.ValueContext.TryGetWritableBytes(this.Context.MethodContext.CFG.Post(pc), ptr, out wb_ptr))
          {
            data = data.TestTrueEqual(ToBoxedExpression(pc, wb_ptr), BoxedExpression.Const(typeSize, this.DecoderForMetaData.System_UInt32, this.DecoderForMetaData));
          }

          return data;
        }
        #endregion

        #region refined ParallelAssign
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>
          HelperForAssignInParallel(INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> state,
          Pair<APC, APC> edge, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap,
          Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          state = base.HelperForAssignInParallel(state, edge, refinedMap, convert);

          foreach (var pair in refinedMap)
          {
            Variable fromVar, strLenVar;
            if (pair.Key.TryUnpackVariable(out fromVar))
            {
              if (this.Context.ValueContext.IsZero(edge.One, fromVar))
              {
                var t = this.Context.ValueContext.GetType(edge.One, fromVar);
                if (t.IsNormal)
                {
                  // If we know that it is a null pointer, then we add the information that the WB(...) == 0
                  if (CanBeAPointer(t.Value))
                  {
                    foreach (var toVarBoxed in pair.Value.GetEnumerable())
                    {
                      Variable toVar, toVar_WB;
                      if (toVarBoxed.TryUnpackVariable(out toVar) && this.Context.ValueContext.TryGetWritableBytes(edge.Two, toVar, out toVar_WB))
                      {
                        // Add the constraint WB(toVar) == 0
                        state = state.TestTrueEqual(ToBoxedExpression(edge.Two, toVar_WB), BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
                      }
                    }
                  }
                  // If we know that it is a null array, then we add the information that ArrayLength(...) == 0
                  else if (IsAnArray(t.Value))
                  {
                    foreach (var toVarBoxed in pair.Value.GetEnumerable())
                    {
                      Variable toVar, toVar_Len;
                      if (toVarBoxed.TryUnpackVariable(out toVar) && this.Context.ValueContext.TryGetArrayLength(edge.Two, toVar, out toVar_Len))
                      {
                        // Add the constraint toVar.Length == 0
                        state = state.TestTrueEqual(ToBoxedExpression(edge.Two, toVar_Len), BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
                      }
                    }
                  }
                }
              }
              // This is a special case to handle fixed(char* charPtr = str), with str a string)
                // We look for assignments "charPtr = (conv_i str) + string_offset" or "charPtr = (conv_i str)"
              if (TryFindStringExpression(edge, fromVar, out strLenVar))
              {
                foreach(var charPtrCandidateBoxed in pair.Value.GetEnumerable())
                {
                  Variable charPtrCandidate, charPtrCandidate_WB;

                  if (charPtrCandidateBoxed.TryUnpackVariable(out charPtrCandidate)
                    && this.Context.ValueContext.TryGetWritableBytes(edge.Two, charPtrCandidate, out charPtrCandidate_WB))
                  {
                    // WB(charPtr) = 2 * str.Len
                    state = state.TestTrueEqual(ToBoxedExpression(edge.Two, charPtrCandidate_WB),
                      BoxedExpression.Binary(BinaryOperator.Mul, ToBoxedExpression(edge.Two, strLenVar), BoxedExpression.Const(2, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData))); 
                  }
                }
              }
            }
          }

          return state;
        }


        #endregion

        #region  Common methods
        /// <summary>
        /// Because of expression reconstruction, ptr is refined to exp[base]
        /// We generate the two proof obligations:
        /// offset >= 0 (lower bound)
        /// offset + sizeof(type) \leq WB(base)
        /// </summary>
        /// <returns>false if no proof obligation could be inferred</returns>
        public bool TryInferSafeBufferAccessConstraints(APC pc, Type type, Variable ptr, out BoxedExpression lowerBound, out BoxedExpression upperBound)
        {
          var ptrType = this.Context.ValueContext.GetType(pc, ptr);
          
          // If we do not have a type, or it is a managed pointer, we are done
          if (!ptrType.IsNormal || this.DecoderForMetaData.IsManagedPointer(ptrType.Value))
          {
            return FailedToInferObligation(out lowerBound, out upperBound);
          }

          // F: need to consider when sizeof is there?

         Polynomial<BoxedVariable<Variable>, BoxedExpression> pol;
         if (Polynomial<BoxedVariable<Variable>, BoxedExpression>.TryToPolynomialForm(ToBoxedExpression(pc, ptr), this.Decoder, out pol))
          {
            Contract.Assume(!object.ReferenceEquals(pol, null));

            BoxedExpression basePtr, wbPtr, offset;

            if (!TrySplitBaseWBAndOffset(pc, pol, out basePtr, out wbPtr, out offset))
            {
              return FailedToInferObligation(out lowerBound, out upperBound);
            }

            // 0 <= offset
            lowerBound = BoxedExpression.Binary(BinaryOperator.Cle, BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData), offset);

            // offset + sizeof(T) <= WB
            var size = this.DecoderForMetaData.TypeSize(type);

           if(size >= 0)
            {
              //var neededbytes = BoxedExpression.Binary(BinaryOperator.Add, offset, BoxedExpression.SizeOf(type, size));
              var neededbytes = BoxedExpression.Binary(BinaryOperator.Add, 
                offset, BoxedExpression.Const(size, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));

              upperBound = BoxedExpression.Binary(BinaryOperator.Cle, neededbytes, wbPtr);
            }
            else // We cannot get the size statically, and we create an expression with the size expressed symbolically
            {
              var neededbytes = BoxedExpression.Binary(BinaryOperator.Add, offset, BoxedExpression.SizeOf(type, size));

              upperBound = BoxedExpression.Binary(BinaryOperator.Cle, neededbytes, wbPtr);
            }

            return true;
          }
          else
          {
            // TODO: Consider the non-polynomial case
            // F: for instance "*(p + a/b)" we do not infer any proof obligation.
            return FailedToInferObligation(out lowerBound, out upperBound);
          }
        }

        private bool FailedToInferObligation(out BoxedExpression a, out BoxedExpression b)
        {
          a = b = default(BoxedExpression);
          return false;
        }

        private bool TrySplitBaseWBAndOffset(APC pc, Polynomial<BoxedVariable<Variable>, BoxedExpression> pol, out BoxedExpression basePtr, out BoxedExpression wbPtr, out BoxedExpression offset)
        {
          var foundAPointer = false;
          var offsets = new List<Monomial<BoxedVariable<Variable>>>(pol.Left.Length);
          var basePtrVar = default(Variable);

          // 1. Fetch the pointer
          foreach (var m in pol.Left)
          { 
            BoxedVariable<Variable> tryVar;
            Variable v;
            if (m.IsVariable(out tryVar) && tryVar.TryUnpackVariable(out v))
            {
              var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), v);

              if(type.IsNormal && (this.DecoderForMetaData.IsUnmanagedPointer(type.Value) || this.DecoderForMetaData.IsReferenceType(type.Value)))
              {
                basePtrVar = v;

                Contract.Assume(foundAPointer == false);
                foundAPointer = true;

                continue;
              }
            }
            offsets.Add(m);
          }

          if (!foundAPointer)
          {
            basePtr = offset = wbPtr = default(BoxedExpression);

            return false;
          }

          // 2. Get the WB
          
          Variable varForWB;
          if (!this.Context.ValueContext.TryGetWritableBytes(this.Context.MethodContext.CFG.Post(pc), basePtrVar, out varForWB))
          {
            basePtr = offset = wbPtr = default(BoxedExpression);

            return false;
          }

          // 3. Construct the offset
          Polynomial<BoxedVariable<Variable>, BoxedExpression> tmpPol;
          if (!Polynomial<BoxedVariable<Variable>, BoxedExpression>.TryToPolynomialForm(offsets.ToArray(), out tmpPol))
          {
            throw new AbstractInterpretationException("Impossible case?");
          }

          Contract.Assert(this.Encoder != null);

          basePtr = BoxedExpression.Var(basePtrVar);
          wbPtr = BoxedExpression.Var(varForWB);
          offset = tmpPol.ToPureExpression(this.Encoder);

          return true;
        }

        private bool TryFindStringExpression(Pair<APC, APC> edge, Variable var, out Variable strLenVar)
        {
          var pc = edge.One;
          var be = ToBoxedExpression(pc, var);

          strLenVar = default(Variable);

          return TryFindStringExpressionInternal(pc, be, ref strLenVar);
        }

        private bool TryFindStringExpressionInternal(APC pc, BoxedExpression be, ref Variable strLenVar)
        {
          if (be.IsUnary)
          {
            return TryFindStringExpressionInternal(pc, be.UnaryArgument, ref strLenVar);
          }
          if (be.IsBinary)
          {
            if(be.BinaryOp != BinaryOperator.Add)
            {
              return false;
            }
            if (!TryFindStringExpressionInternal(pc, be.BinaryLeft, ref strLenVar))
            {
              return TryFindStringExpressionInternal(pc, be.BinaryRight, ref strLenVar);
            }

            return true;
          }
          if(be.IsVariable)
          {
            Variable v;
            if (be.TryGetFrameworkVariable(out v))
            {
              var vType = this.Context.ValueContext.GetType(pc, v);
              if (vType.IsNormal 
                && vType.Value.Equals(this.DecoderForMetaData.System_String)
                && this.Context.ValueContext.TryGetArrayLength(pc, v, out strLenVar))
              {
                return true;
              }
            }
          }

          return false;
        }

        private bool CanBeAPointer(Type t)
        {
          return this.DecoderForMetaData.IsUnmanagedPointer(t)
            || this.DecoderForMetaData.IsManagedPointer(t)
            || this.DecoderForMetaData.System_UIntPtr.Equals(t)
            || this.DecoderForMetaData.System_IntPtr.Equals(t);
        }

        private bool IsAnArray(Type p)
        {
          return this.DecoderForMetaData.IsArray(p);
        }

        #endregion

        #region Interface for the dataflow analysis
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> GetTopValue()
        {
          switch (this.specificoptions[0].type)
          {
            case Analyzers.DomainKind.SubPolyhedra:
              {
                var intervals = new IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                var karr = new LinearEqualitiesForSubpolyhedraEnvironment<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
                var subpolyhedra = new SubPolyhedra<BoxedVariable<Variable>, BoxedExpression>(karr, intervals, this.ExpressionManager);

                return subpolyhedra;
              }

            default:
              throw new AbstractInterpretationException("Abstract domain not supported");
          }
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpoint)
        {
          return new AILogicInference<INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>(this.Decoder,
              this.Options, fixpoint, this.Context, this.DecoderForMetaData, this.Options.TraceNumericalAnalysis);
        }

        #endregion
      }
     
    }
  }
}
#endif