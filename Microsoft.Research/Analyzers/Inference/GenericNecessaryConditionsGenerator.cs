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
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  abstract internal class GenericNecessaryConditionsGenerator<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, AElement>
  : IEdgeVisit<APC, Local, Parameter, Method, Field, Type, Variable, AElement>
    where Variable : IEquatable<Variable>
    where Expression : IEquatable<Expression>
    where Type : IEquatable<Type>
    where LogOptions : IFrameworkLogOptions
  {
    #region object invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.Mdriver != null);
      Contract.Invariant(this.CFG != null);
      Contract.Invariant(this.underVisit != null);
      Contract.Invariant(this.timeout != null);
      Contract.Invariant(this.mutator != null);
    }

    #endregion

    #region State

    protected const int MaxDepth = 400;

    readonly protected APC pcCondition;
    readonly protected IFactQuery<BoxedExpression, Variable> facts;
    readonly protected IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> Mdriver;
    readonly protected ExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable> ExpressionReader;
    readonly protected SimpleSatisfyProcedure<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> SatisfyProcedure;
    readonly protected ICFG CFG;
    readonly protected Set<APC> underVisit;
    readonly protected TimeOutChecker timeout;

    private BoxedExpression ___falseExp;
    protected BoxedExpression False
    {
      get
      {
        Contract.Ensures(Contract.Result<BoxedExpression>() != null);

        if (this.___falseExp == null)
        {
          this.___falseExp = BoxedExpression.ConstBool(false, this.Mdriver.MetaDataDecoder);
        }

        return this.___falseExp;
      }
    }

    protected ReplaceSymbolicValueForAccessPath<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly> mutator;

    public bool LoopHit { protected set; get; }

    #endregion

    #region Constructor

    public GenericNecessaryConditionsGenerator(
      APC pcCondition,
      IFactQuery<BoxedExpression, Variable> facts,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      TimeOutChecker timeout)
    {
      Contract.Requires(mdriver != null);

      this.pcCondition = pcCondition;
      this.facts = facts;
      this.Mdriver = mdriver;
      this.CFG = this.Mdriver.StackLayer.Decoder.Context.MethodContext.CFG;
      this.underVisit = new Set<APC>();
      this.timeout = timeout;
      this.ExpressionReader = new ExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>();
      this.SatisfyProcedure = new SimpleSatisfyProcedure<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(mdriver.MetaDataDecoder);
      this.LoopHit = false;
      this.mutator = new ReplaceSymbolicValueForAccessPath<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(mdriver.Context, mdriver.MetaDataDecoder);
    }

    #endregion

    #region Properties

    protected bool TraceInference { get { return this.Mdriver.Options.TraceInference; } }

    protected ExpressionInPreState Converter(APC pc, BoxedExpression exp)
    {
      return PreconditionSuggestion.ExpressionInPreState(exp, this.Mdriver.Context, this.Mdriver.MetaDataDecoder, pc, allowedKinds: ExpressionInPreStateKind.All);
    }

    #endregion

    #region Abstract members

    abstract protected AElement NoCondition { get; }

    abstract protected bool IsNoCondition(AElement el);
    abstract protected bool ShoudStopTheVisit(APC pc, AElement pre, int depth, out APC nextPC, out AElement nextCondition);

    #endregion

    #region Visit

    protected void Visit(APC pc, AElement pre, int depth)
    {
      APC lastPC;
      AElement preConditionAtTheBlockEntry;
      if(ShoudStopTheVisit(pc, pre, depth, out lastPC, out preConditionAtTheBlockEntry))
      {
        return;
      }

      pre = preConditionAtTheBlockEntry;
      pc = lastPC;

      var md = this.Mdriver;

      foreach (var prePC in CFG.Predecessors(lastPC))
      {
        // nothing to do
        if (md.BasicFacts.IsUnreachable(prePC))
        {
          continue;
        }

        // Widening: stop after one iteration
        if (this.underVisit.Contains(lastPC))
        {
          this.LoopHit = true;

          Trace("Applying simple 1-iteration widening", pre);

          return;
        }

        if (CFG.IsForwardBackEdgeTarget(pc))
        {
          Trace("Reached the target of a forward backedge", pre);

          this.LoopHit = true;

          this.underVisit.Add(lastPC);

          Visit(prePC, md.BackwardTransfer(pc, prePC, pre, this), depth + 1);

          this.underVisit.Remove(lastPC);
        }
        else
        {
          var newPre = md.BackwardTransfer(pc, prePC, pre, this);

          Visit(prePC, newPre, depth + 1);
        }
      }
    }



    protected APC VisitBlock(APC pc, AElement preCondition, out AElement newPrecondition)
    {
      var pcHead = pc.FirstInBlock();

      newPrecondition = preCondition;

      APC prePC;
      while (CFG.HasSinglePredecessor(pc, out prePC))
      {
        // We found the head!
        if (pc.Equals(pcHead))
        {
          return pcHead;
        }

        // nothing to do, kill search
        if (this.Mdriver.BasicFacts.IsUnreachable(pc))
        {
          Trace("Killing path as pc is unreachable", newPrecondition);

          newPrecondition = this.NoCondition;
          return pc;
        }

        newPrecondition = this.Mdriver.BackwardTransfer(pc, prePC, newPrecondition, this);

        if(this.IsNoCondition(newPrecondition))
        {
          Trace("Killing path as we the underlyng analysis returned bottom", newPrecondition);
          return pc;
        }

        pc = prePC;
      }

      return pc;
    }

    /// <summary>
    /// Helper to be called from implementation of ShouldStopTheVisit
    /// </summary>
    /// <param name="depth"></param>
    /// <param name="pre"></param>
    /// <returns></returns>
    protected bool WentTooFar(int depth, AElement pre)
    {
      this.timeout.CheckTimeOut("Precondition (Backwards) computation");

      if (depth >= MaxDepth)
      {
        Trace("Killing the path: Too deep", pre);
        return true;
      }

      return false;
    }

    #endregion

    #region Helpers

    protected BoxedExpression RemoveVacuousPremiseAndCheckForContradiction(APC to, BoxedExpression exp, ref bool contradiction)
    {
      var outcome = this.facts.IsTrue(to, exp);
      switch (outcome)
      {
        case ProofOutcome.Top:
          return exp;
        case ProofOutcome.True:
          return null; // vacuous premise
        case ProofOutcome.Bottom:
        case ProofOutcome.False:
          contradiction = true;
          return null;
        default:
          Contract.Assert(false, "Impossible");
          return null;
      }
    }

    /// <summary>
    /// if bop in {\gt, \geq} then make bop { \lt, \leq } and swap left with right
    /// </summary>
    static protected void Normalize(ref BinaryOperator bop, ref BoxedExpression left, ref BoxedExpression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      Contract.Ensures(Contract.ValueAtReturn(out left) != null);
      Contract.Ensures(Contract.ValueAtReturn(out right) != null);

      if (bop == BinaryOperator.Cgt || bop == BinaryOperator.Cge)
      {
        bop = bop == BinaryOperator.Cgt ? BinaryOperator.Clt : BinaryOperator.Cle;

        var tmp = right;
        right = left;
        left = tmp;
      }
    }

    #endregion

    #region Transfer functions
    abstract public AElement Rename(APC from, APC to, AElement pre, IFunctionalMap<Variable, Variable> renaming);

    virtual public AElement Arglist(APC pc, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement BranchCond(APC pc, APC target, BranchOperator bop, Variable value1, Variable value2, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement BranchTrue(APC pc, APC target, Variable cond, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement BranchFalse(APC pc, APC target, Variable cond, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Branch(APC pc, APC target, bool leave, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Break(APC pc, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, AElement pre)
      where TypeList : IIndexable<Type>
      where ArgList : IIndexable<Variable>
    {
      BreakHere(pc, pre);

      return pre;
    }

    virtual public AElement Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool instance, Variable dest, Variable fp, ArgList args, AElement pre)
      where TypeList : IIndexable<Type>
      where ArgList : IIndexable<Variable>
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ckfinite(APC pc, Variable dest, Variable source, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Cpblk(APC pc, bool @volatile, Variable destaddr, Variable srcaddr, Variable len, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Endfilter(APC pc, Variable decision, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Endfinally(APC pc, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Initblk(APC pc, bool @volatile, Variable destaddr, Variable value, Variable len, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Jmp(APC pc, Method method, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldarga(APC pc, Parameter argument, bool isOld, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldftn(APC pc, Method method, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldloc(APC pc, Local local, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldloca(APC pc, Local local, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Localloc(APC pc, Variable dest, Variable size, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Nop(APC pc, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Pop(APC pc, Variable source, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Return(APC pc, Variable source, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Starg(APC pc, Parameter argument, Variable source, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    public AElement Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Stloc(APC pc, Local local, Variable source, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, Variable value, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Box(APC pc, Type type, Variable dest, Variable source, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Variable dest, ArgList args, AElement pre)
      where TypeList : IIndexable<Type>
      where ArgList : IIndexable<Variable>
    {
      BreakHere(pc, pre);

      return pre;
    }

    virtual public AElement Castclass(APC pc, Type type, Variable dest, Variable obj, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Cpobj(APC pc, Type type, Variable destptr, Variable srcptr, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Initobj(APC pc, Type type, Variable ptr, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldflda(APC pc, Field field, Variable dest, Variable obj, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldlen(APC pc, Variable dest, Variable array, AElement pre)
    {
      BreakHere(pc, pre);

      return pre;
    }

    virtual public AElement Ldsfld(APC pc, Field field, bool @volatile, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldsflda(APC pc, Field field, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldtypetoken(APC pc, Type type, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldfieldtoken(APC pc, Field field, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldmethodtoken(APC pc, Method method, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldvirtftn(APC pc, Method method, Variable dest, Variable obj, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Mkrefany(APC pc, Type type, Variable dest, Variable obj, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList len, AElement pre) where ArgList : IIndexable<Variable>
    {
      BreakHere(pc, pre);

      return pre;
    }

    virtual public AElement Newobj<ArgList>(APC pc, Method ctor, Variable dest, ArgList args, AElement pre) where ArgList : IIndexable<Variable>
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Refanytype(APC pc, Variable dest, Variable source, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Refanyval(APC pc, Type type, Variable dest, Variable source, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Rethrow(APC pc, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Stelem(APC pc, Type type, Variable array, Variable index, Variable value, AElement pre)
    {
      BreakHere(pc, pre);

      return pre;
    }

    virtual public AElement Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Stsfld(APC pc, Field field, bool @volatile, Variable value, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Throw(APC pc, Variable exn, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Unbox(APC pc, Type type, Variable dest, Variable obj, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Unboxany(APC pc, Type type, Variable dest, Variable obj, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Entry(APC pc, Method method, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    [ContractVerification(true)]
    virtual public AElement Assume(APC pc, string tag, Variable condition, object provenance, AElement pre)
    {
      BreakHere(pc, pre, "assume " + tag);


      return pre;
    }

    virtual public AElement Assert(APC pc, string tag, Variable condition, object provenance, AElement pre)
    {
      BreakHere(pc, pre, "assert");

      return pre;
    }

    virtual public AElement Ldstack(APC pc, int offset, Variable dest, Variable source, bool isOld, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldstacka(APC pc, int offset, Variable dest, Variable source, Type origParamType, bool isOld, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldresult(APC pc, Type type, Variable dest, Variable source, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement BeginOld(APC pc, APC matchingEnd, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement EndOld(APC pc, APC matchingBegin, Type type, Variable dest, Variable source, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, AElement pre)
    {
      BreakHere(pc, pre);

      return pre;
    }

    virtual public AElement Isinst(APC pc, Type type, Variable dest, Variable obj, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldconst(APC pc, object constant, Type type, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Ldnull(APC pc, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }

    virtual public AElement Sizeof(APC pc, Type type, Variable dest, AElement pre)
    {
      BreakHere(pc, pre); return pre;
    }


    virtual public AElement Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, AElement pre)
    {
      BreakHere(pc, pre);


      return pre;
    }

    #region Helpers

    protected bool AtLeastOnePremisIsNotFalse(APC pc, List<BoxedExpression> list)
    {
      if (list != null && list.Count > 0)
      {
        var facts = this.facts;
        foreach (var p in list)
        {
          if (facts.IsTrue(pc, p) != ProofOutcome.False)
          {
            return true;
          }
        }

        return false;
      }

      return true;
    }

    protected bool AreTheSame(APC pc, Variable dest, BoxedExpression right)
    {
      Contract.Requires(right != null);

      if (right.UnderlyingVariable != null)
      {
        if (dest.Equals(right.UnderlyingVariable))
        {
          return true;
        }

        // give a shot to  dest == (conv) right.Variable
        // this is essentially a special case for the IL instruction (convi4)arr.Length
        var refined = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(this.Mdriver.CFG.Post(pc), dest), this.Mdriver.ExpressionDecoder);

        UnaryOperator uop;
        BoxedExpression left;
        if (refined != null && refined.UnderlyingVariable != null &&
          (refined.UnderlyingVariable.Equals(right.UnderlyingVariable))
          || (refined.IsUnaryExpression(out uop, out left) && uop.IsConversionOperator() && right.UnderlyingVariable.Equals(left.UnderlyingVariable)
          ))
        {
          return true;
        }
      }
      return false;
    }

    protected BoxedExpression.BinaryExpressionMethodCall MakeMethodCallExpression(Method method, Variable var, FList<PathElement> accessPath)
    {
      return new BoxedExpression.BinaryExpressionMethodCall(/* dummy? */ BinaryOperator.Add,
      BoxedExpression.Var(var, accessPath, this.Mdriver.MetaDataDecoder.System_Object), this.Mdriver.MetaDataDecoder.Name(method));
    }

    protected BoxedExpression MakeZeroExp(APC pc, Variable dest)
    {
      var t = this.Mdriver.Context.ValueContext.GetType(this.Mdriver.Context.MethodContext.CFG.Post(pc), dest);

      BoxedExpression zeroExp;
      if (t.IsNormal)
      {
        object value;
        if (this.Mdriver.MetaDataDecoder.IsStruct(t.Value))
        {
          value = this.Mdriver.MetaDataDecoder.System_Boolean.Equals(t.Value) ? (object)false : (object)0;
        }
        else
        {
          value = null;
        }
        zeroExp = BoxedExpression.Const(value, t.Value, this.Mdriver.MetaDataDecoder);
      }
      else
      {
        zeroExp = BoxedExpression.Const(null, this.Mdriver.MetaDataDecoder.System_Object, this.Mdriver.MetaDataDecoder);
      }

      return zeroExp;
    }

    [Conditional("DEBUG")]
    protected void BreakHere(APC pc, AElement pre, string instr = null)
    {
      if (this.TraceInference)
      {
        Console.WriteLine("Visiting: {0}-{1} with {2}", pc.ToString(), instr, pre.ToString());
      }
    }

    [Conditional("DEBUG")]
    protected void Trace(string s)
    {
      if (this.TraceInference)
      {
        Console.WriteLine("[Backwards] " + s);
      }
    }

    [Conditional("DEBUG")]
    protected void Trace(string s, AElement pre)
    {
      if (this.TraceInference)
      {
        Console.WriteLine("[Backwards] " + s + ": " + pre.ToString());
      }
    }

    #endregion

    #endregion
  }
}
