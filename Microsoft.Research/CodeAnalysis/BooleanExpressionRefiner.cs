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
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace Microsoft.Research.CodeAnalysis
{

  public static class BooleanExpressionRefiner
  {
    public static bool TryRefine<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      (APC pc, Variable goal, bool tryRefineToMethodEntry,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      out BoxedExpression result)
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
      Contract.Requires(mdriver != null);

      var goalExp = BoxedExpression.Convert(mdriver.ExpressionLayer.Decoder.Context.AssumeNotNull().ExpressionContext.Refine(pc, goal), mdriver.ExpressionDecoder, Int32.MaxValue);

      return TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>.TryRefineInternal(pc, goalExp, tryRefineToMethodEntry, mdriver, out result);
    }

    public static class TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
      [ThreadStatic]
      static private bool Trace = false;

      private const int MaxVisitDepth = 100;

      static internal bool TryRefineInternal(APC pc, BoxedExpression goal, bool tryRefineToMethodEntry, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver, out BoxedExpression result)
      {
        var refiner = new Refiner(pc, mdriver);

        var visitDept = 0;

        bool isVisitAborted;
        result = refiner.Refine(pc, goal, tryRefineToMethodEntry, ref visitDept, out isVisitAborted);

        Log("Result of the refinement {0}", result != null ? result.ToString() : "<null>");

        return result != null;
      }


      class Refiner : IEdgeVisit<APC, Local, Parameter, Method, Field, Type, Variable, BoxedExpression>
      {

        #region Object invariant
        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.mdriver != null);
        }
        
        #endregion

        readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver;
        readonly private Subroutine subroutine;
        readonly private BoxedExpression False;


        public Refiner(APC pc, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver)
        {
          Contract.Requires(mdriver != null);

          this.mdriver = mdriver;
          this.subroutine = pc.Block.Subroutine;
          Contract.Assert(mdriver.MetaDataDecoder != null);
          this.False = BoxedExpression.ConstBool(0, mdriver.MetaDataDecoder);
        }

        public BoxedExpression Refine(APC pc, BoxedExpression goal, bool tryRefineToMethodEntry, ref int visitDepth, out bool isVisitAborted)
        {
          Log("Visiting {0} with {1}", pc.ToString(), goal != null ? goal.ToString() : "<null>");

          if (visitDepth > MaxVisitDepth)
          {
            Log("Too depth in the visit. Aborting");

            isVisitAborted = true;
            return null;
          }

          isVisitAborted = false;
          if (goal == null)
          {
            Log("Null goal ({0})", pc.ToString());

            return null;
          }

          if (!tryRefineToMethodEntry && pc.Block.Subroutine != this.subroutine)
          {
            Log("Hit Subroutine boundaries ({0}) with {1}", pc.ToString(), goal.ToString());

            // We give one last shot to try to decompile a ForAll/Exists.
            // This may happen as we cannot decompile it at this point

            Variable var;
            if (goal.IsVariable && goal.TryGetFrameworkVariable(out var))
            {
              var tryForall = this.mdriver.AsForAllIndexed(pc, var);
              if (tryForall != null)
              {
                Log("We were able to refine {0} into {1}", goal.ToString(), tryForall.ToString());
                return tryForall;
              }
            }
            return goal;
          }

          var result = (BoxedExpression)null;
          var currExp = goal;
          var md = this.mdriver;

          if (md.CFG.IsForwardBackEdgeTarget(pc))
          {
            Log("Hit target of a backedge: we give up");
            return null;
          }

          foreach (var pred in md.StackLayer.Decoder.Context.MethodContext.CFG.Predecessors(pc))
          {
            if (md.BasicFacts.IsUnreachable(pred))
            {
              continue;
            }

            var refined = md.BackwardTransfer(pc, pred, currExp, this);

            visitDepth++;

            var r = Refine(pred, refined, tryRefineToMethodEntry, ref visitDepth, out isVisitAborted);

            visitDepth--;

            if (isVisitAborted)
            {
              return null;
            }

            if (r != null)
            {
              if (r.UnderlyingVariable is Variable) // just a sanity check
              {
                var asForAll = md.AsForAllIndexed(pc.Post(), (Variable)r.UnderlyingVariable);
                if (asForAll != null)
                {
                  r = asForAll;
                }
              }

              if (result == null)
              {
                result = r;
              }
              else
              {
                result = BoxedExpression.BinaryLogicalOr(r, result);
              }
            }
            else
            {
              result = result == null ? goal : result;
            }
          }

          return result;
        }

        #region IEdgeVisit<APC,Local,Parameter,Method,Field,Type,Variable,BoxedExpression> Members

        public BoxedExpression Rename(APC from, APC to, BoxedExpression state, IFunctionalMap<Variable, Variable> renaming)
        {
          if (state != null)
          {
            Variable v;
            if (state.TryGetFrameworkVariable(out v))
            {
              if (renaming.Contains(v))
              {
                var newVar = renaming[v];

                var result = BoxedExpression.Convert(this.mdriver.Context.ExpressionContext.Refine(to, newVar), this.mdriver.ExpressionDecoder);

                Log("Renaming {0} to {1}", state.ToString(), result != null? result.ToString(): "<null>");

                // We use the heuristic of reading variables in the prestate, to get their name at the point we are interested in.
                // We may lose information because of it, and unable to decompile more complex expressions
                if (result != null && !result.IsVariable)
                {
                  return ReadInPostState(result, renaming);
                }

                return result;
              }
            }
            int k;
            if (state.IsConstantInt(out k))
            {
              return state;
            }
          }

          return null;
        }

        public BoxedExpression Assume(APC pc, string tag, Variable condition, object provenance, BoxedExpression data)
        {
          var mdd = mdriver.MetaDataDecoder;
          var cond = BoxedExpression.Convert(this.mdriver.Context.ExpressionContext.Refine(pc, condition), this.mdriver.ExpressionDecoder);

          Log("Hit Assume ({0}) : {1}", tag.ToString(), cond != null ? cond.ToString() : "<null>");

          if (cond == null)
          {
            Log("Aborting as we found a null condition for assume");
            return null;
          }

          // We do not add the assumptions already checked somewhere else
          if (tag == "requires" || tag == "invariant")
          {
            return data;
          }

          if (tag == "true")
          {
            if (cond.IsVariable)
            {
              var liftedtype = mdriver.Context.ValueContext.GetType(pc, condition);
              if (liftedtype.IsNormal)
              {
                var type = liftedtype.Value;
                if (mdd.System_Boolean.Equals(type))
                {
                  // do nothing
                }
                else if (mdd.IsReferenceType(type))
                {
                  // build cond != null
                  cond = BoxedExpression.Binary(BinaryOperator.Cne_Un, cond, BoxedExpression.Const(null, type, mdd));
                }
                else
                {
                  // build cond != 0
                  cond = BoxedExpression.Binary(BinaryOperator.Cne_Un, cond, BoxedExpression.Const(0, type, mdd));
                }
              }
            }
          }
          else if (tag == "false")
          {
            UnaryOperator uop;
            BoxedExpression inner;

            // !(!inner) ? ---> inner != null
            if (cond.IsUnaryExpression(out uop, out inner) && uop == UnaryOperator.Not)
            {
              cond = BoxedExpression.Binary(BinaryOperator.Cne_Un, inner, BoxedExpression.Const(null, mdd.System_Object, mdd));
            }
            else
            {
              cond = BoxedExpression.Unary(UnaryOperator.Not, cond);
            }
          }

          bool B;
          if(cond.IsTrivialCondition(out B))
          {
            return B ? data : BoxedExpression.ConstBool(false, this.mdriver.MetaDataDecoder);
          }


          return BoxedExpression.BinaryLogicalAnd(cond, data);
        }

        protected BoxedExpression ReadInPostState(BoxedExpression candidate, IFunctionalMap<Variable, Variable> renaming)
        {
          Contract.Requires(candidate != null);

          Contract.Ensures(Contract.Result<BoxedExpression>() != null);

          if (candidate.IsVariable)
          {
            Variable v;
            if (candidate.TryGetFrameworkVariable(out v))
            {
              // F: bad bad way of doing reverse lookup. This should be changed (by now it is balanced by the fact that we cache the reconstructed expressions)
              foreach (var key in renaming.Keys)
              {
                var val = renaming[key];
                if (v.Equals(val))
                {
                  return BoxedExpression.Var(key);
                }
              }
            }

            return candidate;
          }
          if (candidate.IsUnary)
          {
            return BoxedExpression.Unary(candidate.UnaryOp, ReadInPostState(candidate.UnaryArgument, renaming));
          }

          BinaryOperator bop;
          BoxedExpression left, right;
          if (candidate.IsBinaryExpression(out bop, out left, out right))
          {
            return BoxedExpression.Binary(bop, ReadInPostState(left, renaming), ReadInPostState(right, renaming));
          }
          return candidate;
        }

        #endregion

        #region Utils to simplify expressions


        #endregion

        #region IVisitMSIL<APC,Local,Parameter,Method,Field,Type,Variable,Variable,BoxedExpression,BoxedExpression> Members

        public BoxedExpression Arglist(APC pc, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression BranchCond(APC pc, APC target, BranchOperator bop, Variable value1, Variable value2, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression BranchTrue(APC pc, APC target, Variable cond, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression BranchFalse(APC pc, APC target, Variable cond, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Branch(APC pc, APC target, bool leave, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Break(APC pc, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, BoxedExpression data)
          where TypeList : Microsoft.Research.DataStructures.IIndexable<Type>
          where ArgList : Microsoft.Research.DataStructures.IIndexable<Variable>
        {
          return data;
        }

        public BoxedExpression Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Variable dest, Variable fp, ArgList args, BoxedExpression data)
          where TypeList : Microsoft.Research.DataStructures.IIndexable<Type>
          where ArgList : Microsoft.Research.DataStructures.IIndexable<Variable>
        {
          return data;
        }

        public BoxedExpression Ckfinite(APC pc, Variable dest, Variable source, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Cpblk(APC pc, bool @volatile, Variable destaddr, Variable srcaddr, Variable len, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Endfilter(APC pc, Variable decision, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Endfinally(APC pc, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Initblk(APC pc, bool @volatile, Variable destaddr, Variable value, Variable len, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Jmp(APC pc, Method method, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldarga(APC pc, Parameter argument, bool isOld, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldftn(APC pc, Method method, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldloc(APC pc, Local local, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldloca(APC pc, Local local, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Localloc(APC pc, Variable dest, Variable size, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Nop(APC pc, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Pop(APC pc, Variable source, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Return(APC pc, Variable source, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Starg(APC pc, Parameter argument, Variable source, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Stloc(APC pc, Local local, Variable source, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Switch(APC pc, Type type, IEnumerable<Microsoft.Research.DataStructures.Pair<object, APC>> cases, Variable value, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Box(APC pc, Type type, Variable dest, Variable source, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Variable dest, ArgList args, BoxedExpression data)
          where TypeList : Microsoft.Research.DataStructures.IIndexable<Type>
          where ArgList : Microsoft.Research.DataStructures.IIndexable<Variable>
        {
          return data;
        }

        public BoxedExpression Castclass(APC pc, Type type, Variable dest, Variable obj, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Cpobj(APC pc, Type type, Variable destptr, Variable srcptr, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Initobj(APC pc, Type type, Variable ptr, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, BoxedExpression data)
        {
          Log("Hit ldelem {0} = {1}[{2}] with {3}", dest, array, index, data);

          if (data != null)
          {
            return data.Substitute(delegate(Variable v, BoxedExpression be)
            {
              if (dest.Equals(v))
              {
                var context = this.mdriver.Context.ExpressionContext;
                var arrayExp = BoxedExpression.Convert(this.mdriver.Context.ExpressionContext.Refine(pc, array), this.mdriver.ExpressionDecoder);
                var indexExp = BoxedExpression.Convert(this.mdriver.Context.ExpressionContext.Refine(pc, index), this.mdriver.ExpressionDecoder);
                if (arrayExp != null && indexExp != null)
                {
                  return new BoxedExpression.ArrayIndexExpression<Type>(arrayExp, indexExp, type);
                }
              }
              return be;
            });
          }

          return data;
        }

        public BoxedExpression Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldflda(APC pc, Field field, Variable dest, Variable obj, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldlen(APC pc, Variable dest, Variable array, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldsfld(APC pc, Field field, bool @volatile, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldsflda(APC pc, Field field, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldtypetoken(APC pc, Type type, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldfieldtoken(APC pc, Field field, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldmethodtoken(APC pc, Method method, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldvirtftn(APC pc, Method method, Variable dest, Variable obj, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Mkrefany(APC pc, Type type, Variable dest, Variable obj, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList len, BoxedExpression data) where ArgList : Microsoft.Research.DataStructures.IIndexable<Variable>
        {
          return data;
        }

        public BoxedExpression Newobj<ArgList>(APC pc, Method ctor, Variable dest, ArgList args, BoxedExpression data) where ArgList : Microsoft.Research.DataStructures.IIndexable<Variable>
        {
          return data;
        }

        public BoxedExpression Refanytype(APC pc, Variable dest, Variable source, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Refanyval(APC pc, Type type, Variable dest, Variable source, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Rethrow(APC pc, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Stelem(APC pc, Type type, Variable array, Variable index, Variable value, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Stsfld(APC pc, Field field, bool @volatile, Variable value, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Throw(APC pc, Variable exn, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Unbox(APC pc, Type type, Variable dest, Variable obj, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Unboxany(APC pc, Type type, Variable dest, Variable obj, BoxedExpression data)
        {
          return data;
        }

        #endregion

        #region IVisitSynthIL<APC,Method,Type,Variable,Variable,BoxedExpression,BoxedExpression> Members

        public BoxedExpression Entry(APC pc, Method method, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Assert(APC pc, string tag, Variable condition, object provenance, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldstack(APC pc, int offset, Variable dest, Variable source, bool isOld, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldstacka(APC pc, int offset, Variable dest, Variable source, Type origParamType, bool isOld, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldresult(APC pc, Type type, Variable dest, Variable source, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression BeginOld(APC pc, APC matchingEnd, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression EndOld(APC pc, APC matchingBegin, Type type, Variable dest, Variable source, BoxedExpression data)
        {
          return data;
        }

        #endregion

        #region IVisitExprIL<APC,Type,Variable,Variable,BoxedExpression,BoxedExpression> Members

        public BoxedExpression Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Isinst(APC pc, Type type, Variable dest, Variable obj, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldconst(APC pc, object constant, Type type, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Ldnull(APC pc, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Sizeof(APC pc, Type type, Variable dest, BoxedExpression data)
        {
          return data;
        }

        public BoxedExpression Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, BoxedExpression data)
        {
          return data;
        }

        #endregion
      }

      #region Logging
      [Conditional("DEBUG")]
      static private void Log(string format, params object[] args)
      {
        if (Trace)
        {
          Console.WriteLine(format, args);
        }
      }
      #endregion
    }
  }
}
