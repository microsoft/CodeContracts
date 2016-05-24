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
using Microsoft.Research.CodeAnalysis.Inference;
using Microsoft.Research.CodeAnalysis.Expressions;

namespace Microsoft.Research.CodeAnalysis
{
  public class CodeFixesManager : ICodeFixesManager
  {
    private readonly bool shortCodeFixes;
    private readonly List<ICodeFix> fixes;
    private readonly IOutput output;
    private readonly LazyEval<BoxedExpression> ZeroExp;
    private readonly LazyEval<BoxedExpression> OneExp;
    private readonly Func<BoxedExpression, BoxedExpression> Simplify;
    private readonly Func<BoxedExpression, BoxedExpression, BoxedExpression> ConstraintSolver;
    private readonly bool traceBaseline;

    public CodeFixesManager(bool shortCodeFixes, IOutput output, bool traceBaseline,
      Func<BoxedExpression, BoxedExpression> ExpressionSimplifier,  Func<BoxedExpression, BoxedExpression, BoxedExpression> ConstraintSolver,
      LazyEval<BoxedExpression> ZeroExp, LazyEval<BoxedExpression> OneExp)
    {
      Contract.Requires(output != null);
      Contract.Requires(ExpressionSimplifier != null);
      Contract.Requires(ConstraintSolver != null);
      Contract.Requires(ZeroExp != null);
      Contract.Requires(OneExp != null);

      this.traceBaseline = traceBaseline;
      this.shortCodeFixes = shortCodeFixes;
      this.output = output;
      this.Simplify = ExpressionSimplifier;
      this.ConstraintSolver = ConstraintSolver;
      this.ZeroExp = ZeroExp;
      this.OneExp = OneExp;

      this.fixes = new List<ICodeFix>();
    }

    internal void Add(ProofObligation obl, ICodeFix fix)
    {
      if (obl != null)
      {
        obl.AddCodeFix(fix); // for each obligation, we want to remember its fixes
      }
      this.fixes.Add(fix);
    }

    internal IEnumerable<ICodeFix> GenerateCodeFixes()
    {
      // TODO: remove duplicated
      return fixes;
    }

    private static bool WantSuggestionAtPC(APC aPC)
    {
      // never suggest fixes for instantiations of contracts
      if (aPC.InsideContractAtCall) return false;
      return true;
    }


    public int SuggestCodeFixes<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Exp, Variable, ILogOptions>(
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions> mdriver)
      where Variable : IEquatable<Variable>
      where Type : IEquatable<Type>
      where ILogOptions : IFrameworkLogOptions
    {
      var i = 0;
      foreach (var fix in this.GenerateCodeFixes())
      {
        i++;

#if false
        // this is pretty hack-y, but we need a handle on mdriver and the CodeFix, not sure how else to do
        var baselineFix = fix as CodeFix.BaselineAssumeAtCallReturn;
        if (baselineFix != null)
        {
          mdriver.AddCalleeAssume(baselineFix.Assumption, baselineFix.MethodInfo, baselineFix.PC, new ProofObligation[]{ baselineFix.Obligation });
          if (this.traceBaseline)
          {
            this.output.Suggestion("Baseline", baselineFix.PC, baselineFix.Suggest(), fix.Obligation != null ? new List<uint>() { fix.Obligation.ID } : null);
          }
          continue; // don't emit normal suggestion for this
        }
#endif

        string codeFix;
        if (this.shortCodeFixes)
        {
          codeFix = fix.SuggestCode();
        }
        else
        {
          codeFix = fix.Suggest();
        }

        if (codeFix != null)
        {
          this.output.Suggestion(ClousotSuggestion.Kind.CodeFix, ClousotSuggestion.Kind.CodeFix.Message(),
            fix.PC, codeFix, fix.Obligation != null ? new List<uint>() { fix.Obligation.ID } : null, ClousotSuggestion.ExtraSuggestionInfo.None);
        }
      }

      return i;
    }

    #region Try Suggest larger allocations

    public bool TrySuggestLargerAllocation<Variable>(ProofObligation obl, Func<APC> pc, APC failingConditionPC, BoxedExpression failingCondition, Variable array, Variable length, Func<Variable, BoxedExpression> Converter, IFactQuery<BoxedExpression, Variable> factQuery)
    {      
      if (failingCondition == null)
      {
        return false;
      }

      BinaryOperator bop;
      BoxedExpression left, right;
      // is it in the form exp < length?

      if (failingCondition.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Clt)
      {
        var tryLeq = BoxedExpression.Binary(BinaryOperator.Cle, left, right);
        if (factQuery.IsTrue(failingConditionPC, tryLeq) == ProofOutcome.True)
        {
          var apc = pc();
          if (!WantSuggestionAtPC(apc)) return false;
          var lengthExp = Converter(length);
          this.fixes.Add(new CodeFix.ArrayInitializationFix(apc, obl, Converter(array), lengthExp, Simplify(BoxedExpression.Binary(BinaryOperator.Add, lengthExp, this.OneExp.Value))));
        }
        else if(length.Equals(right.UnderlyingVariable))
        {
          var apc = pc();
          if (!WantSuggestionAtPC(apc)) return false;
          this.fixes.Add(new CodeFix.ArrayInitializationFix(apc, obl, Converter(array), left, Simplify(BoxedExpression.Binary(BinaryOperator.Add, left, this.OneExp.Value))));
        }
        return true;
      }

      return false;
    }

    #endregion

    #region TrySuggest Test fixes

    public bool TrySuggestTestStrengthening(ProofObligation obl, APC pc, BoxedExpression additionalGuard, bool strengthenNullCheck)
    {
      if (additionalGuard == null)
      {
        return false;
      }

      if (!WantSuggestionAtPC(pc)) return false;

      this.Add(obl, new CodeFix.StrengthenTestFix(pc, obl, Simplify(additionalGuard), strengthenNullCheck));

      return true;
    }

    public bool TrySuggestTestFix<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          ProofObligation obl, Func<APC> pc, BoxedExpression guard, BoxedExpression failingCondition,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Func<BoxedExpression, bool> IsArrayLength)
    {
      if (failingCondition == null || guard == null)
      {
        return false;
      }

      var negatedGuard = guard.Negate().Simplify(metaDataDecoder, IsArrayLength);

      // if failingCondition == !guard, then we should suggest to negate guard
      if (BoxedExpression.SimpleSyntacticEquality(failingCondition, negatedGuard))
      {
        var apc = pc();
        if (!WantSuggestionAtPC(apc)) return false;
        this.Add(obl, new CodeFix.TestFix(apc, obl, guard.Simplify(metaDataDecoder, IsArrayLength), failingCondition));
        return true;
      }
      // special case as with /optimize the C# compiler generates "x" instead of "(x == null) == 0"
      // we do not need it with cci2        
      if (negatedGuard.IsVariable)
      {
        var notZero = negatedGuard.NotK(0, metaDataDecoder);
        if (failingCondition.Equals(notZero))
        {
          var apc = pc();
          if (!WantSuggestionAtPC(apc)) return false;
          this.Add(obl, new CodeFix.TestFix(apc, obl, negatedGuard.EqualK(0, metaDataDecoder), failingCondition));
          return true;
        }

        var notNull = negatedGuard.NotK(null, metaDataDecoder);
        if (failingCondition.Equals(notNull))
        {
          var apc = pc();
          if (!WantSuggestionAtPC(apc)) return false;
          this.Add(obl, new CodeFix.TestFix(apc, obl, negatedGuard.EqualK(null, metaDataDecoder), failingCondition));
          return true;
        }
      }

      // simplify things like (int)arr.Length to arr.Length
      var simplifiedGuard = guard.Simplify(metaDataDecoder, IsArrayLength);

      BoxedExpression fix;
      if (TrySuggestStrongerTest(obl, simplifiedGuard, failingCondition, 
        x => BoxedExpression.Const(x, metaDataDecoder.System_Int32, metaDataDecoder),
        out fix))
      {
        var apc = pc();
        if (!WantSuggestionAtPC(apc)) return false;
        this.Add(obl, new CodeFix.TestFix(apc, obl, simplifiedGuard, fix));
        return true;
      }

      return false; // no fix
    }

    private bool TrySuggestStrongerTest(ProofObligation obl, BoxedExpression simplifiedGuard, BoxedExpression failingCondition, 
      Func<int, BoxedExpression> ConstMaker,
      out BoxedExpression fix)
    {
      Contract.Requires(simplifiedGuard != null);
      Contract.Requires(failingCondition != null);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out fix) != null);

      fix = failingCondition;

      BinaryOperator bop1, bop2;
      BoxedExpression l1, l2, r1, r2;
      if (simplifiedGuard.IsBinaryExpression(out bop1, out l1, out r1) && failingCondition.IsBinaryExpression(out bop2, out l2, out r2))
      {
        // "l1 bop1 r1" is weaker than "l2 bop r2"?
        var leftsAreEqual = l1.Equals(l2);
        var rightsAreEqual = r1.Equals(r2);
        if (leftsAreEqual && rightsAreEqual)
        {
          return IsWeakerOperator(bop1, bop2);
        }

        // if they are comparisons
        if (bop1.IsComparisonBinaryOperator() && bop2.IsComparisonBinaryOperator())
        {
          // We search for constant fixes
          int l1Const, l2Const, r1Const, r2Const;

          var l1IsConst = l1.IsConstantInt(out l1Const);
          var l2IsConst = l2.IsConstantInt(out l2Const);

          var r1IsConst = r1.IsConstantInt(out r1Const);
          var r2IsConst = r2.IsConstantInt(out r2Const);


          if (bop1 == bop2)
          {
            // "left bop k1" and "left bop k2"
            if (leftsAreEqual && r1IsConst && r2IsConst)
            {
              if (this.TrySuggestStrongerTestForConstants(bop1, simplifiedGuard, failingCondition, r1Const, r2Const, ConstMaker, out fix))
              {
                return true;
              }
            }

            BinaryOperator newBop;
            if (bop1.TryInvert(out newBop))
            {
              // "k1 bop right" and "k2 bop right"
              if (rightsAreEqual && l1IsConst && l2IsConst)
              {
                var newSimplifiedGuard = BoxedExpression.Binary(newBop, r1, l1);
                var newFailingCondition = BoxedExpression.Binary(newBop, r2, l2);

                if (this.TrySuggestStrongerTestForConstants(newBop, newSimplifiedGuard, newFailingCondition, l1Const, l2Const, ConstMaker, out fix))
                {
                  return true;
                }
              }
            }
          }

          BinaryOperator bop1Neg;
          if (bop1.TryInvert(out bop1Neg) && bop2 == bop1Neg)
          {
            if (l1IsConst && r2IsConst && l2.Equals(r1))
            {
              var simplifiedGuardInverted = BoxedExpression.Binary(bop1Neg, r1, l1);
              if (this.TrySuggestStrongerTestForConstants(bop2, simplifiedGuardInverted, failingCondition, l1Const, r2Const, ConstMaker, out fix))
              {
                return true;
              }
            }
            if (r1IsConst && l2IsConst && l1.Equals(r2))
            {
              var failingConditionInverted = BoxedExpression.Binary(bop1, r2, l2); // we are inverting the failingCondition
              if (this.TrySuggestStrongerTestForConstants(bop1, simplifiedGuard, failingConditionInverted, r1Const, l2Const, ConstMaker, out fix))
              {
                return true;
              }
            }
          }
        }

        // "l1 bop1 r2" is weaker than "r2 bop2Inverted l2"?
        BinaryOperator bop2Inverted;
        if (l1.Equals(r2) && r1.Equals(l2) && bop2.TryInvert(out bop2Inverted))
        {
          return IsWeakerOperator(bop1, bop2Inverted);
        }
      }

      fix = null;
      return false;
    }

    private static bool IsWeakerOperator(BinaryOperator bop1, BinaryOperator bop2)
    {
      // l <= r 
      if (bop1 == BinaryOperator.Cle)
      {
        // should we suggest l < r ? 
        return bop2 == BinaryOperator.Clt;
      }
      // l >= r
      if (bop1 == BinaryOperator.Cge)
      {
        // should we suggest l > r ?
        return bop2 == BinaryOperator.Cgt;
      }
      // l <= r 
      if (bop1 == BinaryOperator.Cle_Un)
      {
        // should we suggest l < r ? 
        return bop2 == BinaryOperator.Clt_Un;
      }
      // l >= r
      if (bop1 == BinaryOperator.Cge_Un)
      {
        // should we suggest l > r ?
        return bop2 == BinaryOperator.Cgt_Un;
      }
      return false;
    }

    private bool TrySuggestStrongerTestForConstants(BinaryOperator bop, BoxedExpression simplifiedGuard, BoxedExpression failingCondition, 
      int r1Const, int r2Const,
      Func<int, BoxedExpression> MakeConst,
      out BoxedExpression fix)
    {
      Contract.Requires(bop == simplifiedGuard.BinaryOp);
      Contract.Requires(bop == failingCondition.BinaryOp);

      #region All the cases
      switch (bop)
      {
        case BinaryOperator.Ceq:
          {
            // x == k but need x == k' , so suggest x == k'
            fix = failingCondition;
            return true;
          }
        case BinaryOperator.Cne_Un:
          {
            // x != k but need x != k2, so suggest x != k1 && x != k2 
            fix = BoxedExpression.Binary(BinaryOperator.LogicalAnd, simplifiedGuard, failingCondition);
            return true;
          }
        case BinaryOperator.Cge:
        case BinaryOperator.Cgt:
          {
            // x {>=, >} k1 but need x {>=, >} k2, so if k2 > k1, we suggest x {>=, >} k2

            if (r2Const > r1Const)
            {
              fix = failingCondition;
              return true;
            }
            else
            {
              fix = null;
              return false;
            }
          }

        case BinaryOperator.Cle:
        case BinaryOperator.Clt:
          {
            // x {<=, <} k1 but need x {<=, <} k2, so if k2 < k1, we suggest x {<=, <} k2
            if (r2Const < r1Const)
            {
              fix = failingCondition;
              return true;
            }
            else
            {
              fix = null;
              return false;
            }
          }

        default:
          {
            fix = null;
            return false;
          }
      }
      #endregion
    }

    #endregion
     
    #region TrySuggest Initialization fixes
    
    public bool TrySuggestInitializationFix<Variable>(ref InitializationFix<Variable> parameters)
    {
      if (!WantSuggestionAtPC(parameters.pc)) return false;

      if (parameters.SourceExp != null && parameters.varsInSourceExp.Contains(parameters.dest))
      {
        this.Add(parameters.obl, new CodeFix.Assume(parameters.pc, parameters.obl, this.Simplify(parameters.SourceExp)));

        return true;
      }

      return false;
    }

    public bool TrySuggestConstantInititalizationFix(ProofObligation obl, APC pc, BoxedExpression dest, BoxedExpression oldInitialization, BoxedExpression newInitialization, BoxedExpression constraint)
    {
      if (!WantSuggestionAtPC(pc)) return false;
      this.Add(obl, new CodeFix.ExpressionInitializationFix(pc, obl, dest, oldInitialization, constraint, newInitialization));

      return true;
    }

    public bool TrySuggestConstantInitializationFix<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(
    ProofObligation obl, 
      Func<APC> pc, BoxedExpression failingCondition, BoxedExpression falseCondition,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder,
      Func<Variable, BoxedExpression> VariableValueBeforeRenaming, Func<Variable, BoxedExpression> VariableName)
    {
      if (VariableValueBeforeRenaming != null)
      {
        var failingConditionVars = failingCondition.Variables<Variable>();

        // We only do the case when only one variable was lost in the renaming
        if (failingConditionVars.Count == 1)
        {
          var exp = VariableValueBeforeRenaming(failingConditionVars.PickAnElement());
          if (exp.IsConstant)
          {
            var initVar = VariableName(failingConditionVars.PickAnElement());

            if (initVar != null)
            {
              failingCondition = failingCondition.Substitute(BoxedExpression.Var(failingConditionVars.PickAnElement()), initVar);
            }
            else // give another shot - sometimes VariableName returns null, but we already have the access path in the failing condition
            {
              initVar = failingCondition.Variables().PickAnElement();
            }
            if (initVar != null && failingCondition != null)
            {
              var constraint = failingCondition.Simplify(metaDataDecoder);
              var newInit = this.ConstraintSolver(constraint, initVar);
              var apc = pc();
              if (!WantSuggestionAtPC(apc)) return false;

              this.Add(obl, new CodeFix.ConstantInitializationFix(apc, obl, initVar, exp.Constant, constraint, newInit));

              return true;
            }
          }
        }
        else
        {
          var failingConditionVarsAsExps = failingCondition.Variables();
          var falseConditionVarsAsExps = falseCondition.Variables();
          var removedVarsAsExp = failingConditionVarsAsExps.Difference(falseConditionVarsAsExps); // the variables removed by the renaming

          // We only do the case when only one variable was lost in the renaming
          if (removedVarsAsExp.Count == 1)
          {
            var oldExp = removedVarsAsExp.PickAnElement();
            Variable v;
            if (oldExp.TryGetFrameworkVariable(out v))
            {
              var variable = VariableValueBeforeRenaming(v);
              var constraint = failingCondition.Simplify(metaDataDecoder);
              var newInit = this.ConstraintSolver(constraint, oldExp);
              var apc = pc();
              if (!WantSuggestionAtPC(apc)) return false;
              this.Add(obl, new CodeFix.ExpressionInitializationFix(apc, obl, oldExp, variable, constraint, newInit));
              return true;
            }
          }
        }
      }
      return false;
    }

    #endregion

    #region Try suggest an assumption of a postcondition starting from a result value

    public bool TrySuggestFixForMethodCallReturnValue<Variable, ArgList, Method>(ref ParametersFixMethodCallReturnValue<Variable, ArgList, Method> context)
     where ArgList : IIndexable<Variable>
    {
      if (context.condition != null)
      {
        var pcWithSourceContext = context.pcWithSourceContext();

#if false
        #region baseline assume is fully general
        var postPC = context.cfg.Post(pcWithSourceContext);
        var baseLineFixExpr = context.ReadAt(postPC, context.condition, true);
        if (baseLineFixExpr != null)
        {
          this.Add(context.obl, new CodeFix.BaselineAssumeAtCallReturn(postPC, context.obl, baseLineFixExpr, context.method));
        }
        #endregion
#endif

        #region If we have no real source context, we just avoid the suggestion

        if (!pcWithSourceContext.HasRealSourceContext)
        {
          return false;
        }

        #endregion

        #region Try to suggest [Pure]

        if (!context.CalleeIsProperty())
        {
          var varsInCondition = context.condition.Variables<Variable>();

          if (varsInCondition.Any())
          {
            // See if a variable in the condition has been havoced in the call
            foreach (var v in varsInCondition)
            {
              var vBeforeCall = context.AccessPath(context.pc, v);
              FList<PathElement> vAfterCall;
              if (vBeforeCall == null && (vAfterCall = context.AccessPath(context.obl.PC, v)) != null && context.IsRootedInParameter(vAfterCall))
              {
                var havocedExp = BoxedExpression.Var(v, vAfterCall);
                this.fixes.Add(new CodeFix.MethodShouldbePure(pcWithSourceContext, context.obl, context.MethodFullName(), havocedExp));

              }
            }
          }
        }
        #endregion

        #region try to infer an assume != null, case dest != {0, null}
        BinaryOperator dummyBop;
        BoxedExpression left, right;

        if (context.dest.Equals(context.condition.UnderlyingVariable))
        {
          if (context.condition.AccessPath != null)
          {
            this.Add(context.obl, new CodeFix.MethodCallResult(pcWithSourceContext, context.obl, 
              BoxedExpression.Binary(BinaryOperator.Cne_Un, context.condition, context.MakeEqualZero(context.dest)), 
              context.condition, context.method, context.MethodName()));
            return true;
          }
          else
          {
            if (context.CalleeIsStaticMethod())
            {
              // do nothing
            }
            else if (context.args.Count == 1)
            {
              var self = context.AccessPath(context.pc, context.args[0]);
              if (self != null)
              {
                var exp = BoxedExpression.Binary(BinaryOperator.Cne_Un, context.MakeMethodCall(context.dest, self), context.MakeEqualZero(context.dest));

                this.Add(context.obl, new CodeFix.MethodCallResult(pcWithSourceContext, context.obl, exp, null, context.method, context.MethodFullName()));

                return true;
              }
            }
          }
        }
        #endregion

        #region try to infer dest op constant (or constant op dest)
        else if (context.condition.IsBinaryExpression(out dummyBop, out left, out right) && ((IsVariableOpConstant(context.dest, left, right) || IsVariableOpConstant(context.dest, right, left))))
        {
          // TODO: fix fix. Need to use context.ReadAfter to see where the expression is value (walk to successors)
          this.Add(context.obl, new CodeFix.MethodCallResult(pcWithSourceContext, context.obl, context.condition, context.condition, context.method, context.MethodName()));

          return true;
        }
        #endregion

        #region try to infer a condition where there is only one free variable, and this free variable is dest
        else
        {
          var vars = context.condition.Variables<Variable>();
          if (vars.Count == 1)
          {
            var onlyVar = vars.PickAnElement();
            if (context.dest.Equals(onlyVar))
            {
              var accessPath = context.AccessPath(context.pc, context.dest);
              if (accessPath != null)
              {
                var goalExp = context.condition.Substitute(BoxedExpression.Var(context.dest), BoxedExpression.Var(context.dest, accessPath));
                if (goalExp != null)
                {
                  this.Add(context.obl, new CodeFix.MethodCallResult(pcWithSourceContext, context.obl, goalExp, null, context.method, context.MethodFullName()));

                  return true;
                }
              }
              else if (!context.CalleeIsStaticMethod())
              {
                if (context.args.Count == 1)
                {
                  var self = context.AccessPath(context.pc, context.args[0]);
                  if (self != null)
                  {
                    var goalExp = context.condition.Substitute(BoxedExpression.Var(context.dest), context.MakeMethodCall(context.args[0], self));
                    if (goalExp != null)
                    {
                      this.Add(context.obl, new CodeFix.MethodCallResult(pcWithSourceContext, context.obl, goalExp, null, context.method, context.MethodFullName()));

                      return true;
                    }
                  }
                }
              }

              // if we reach this point, we know that dest == onlyVar
              // Let us try to propose an assume
              BinaryOperator bop;
              // Condition == "left != null"
              if (context.condition.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Cne_Un && right.IsNull)
              {
                this.Add(context.obl, new CodeFix.MethodCallResultNoCode(pcWithSourceContext, context.obl, context.MethodFullName()));

                return true;
              }
            }
          }
	  else if (vars.Contains(context.dest))
          {
            var contextLoc = context; // to please the compiler
            var sourceReadableExpression = new SourceLevelReadableExpression<Variable>(exp => contextLoc.AccessPath(contextLoc.pc, exp));
            var fix = sourceReadableExpression.Visit(contextLoc.condition, new Void());
            if (fix != null)
            {
              this.Add(context.obl, new CodeFix.MethodCallResult(pcWithSourceContext, context.obl, fix, contextLoc.condition, context.method, context.MethodFullName()));
            }
          }

        }
        #endregion
      }

#if TODO // try to infer an assume when dest = f(...) and the goal is ".... dest ... ==> condition"
      if (context.premises != null && context.premises.Count > 0)
      {
        #region try to infer an assume when dest = f(...) and the goal is ".... dest ... ==> condition"

        var dest = context.dest;
        if (context.premises.Any(exp => exp.Variables<Variable>().Contains(dest)))
        {

        }
        
        #endregion
      }
#endif
      return false;

    }


    private static bool IsVariableOpConstant<Variable>(Variable dest, BoxedExpression left, BoxedExpression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      return (left.UnderlyingVariable != null && left.AccessPath != null && left.UnderlyingVariable.Equals(dest) && right.IsConstant);
    }

    #endregion

    #region Try suggest casts for floating point comparisons

    public bool TrySuggestFloatingPointComparisonFix(ProofObligation obl, APC pc, BoxedExpression left, BoxedExpression right, ConcreteFloat leftType, ConcreteFloat rightType)
    {
      if (!WantSuggestionAtPC(pc)) return false;

      // Should not happen, but we make sure about it
      if (leftType == rightType)
      {
        return false;
      }

      if (leftType == ConcreteFloat.Uncompatible || rightType == ConcreteFloat.Uncompatible)
      {
        return false;
      }

      if (rightType == ConcreteFloat.Float80)
      {
        this.Add(obl, new CodeFix.FloatingPointCastFix(pc, obl, left, right, leftType));

        return true;
      }
      if (leftType == ConcreteFloat.Float80)
      {
        this.Add(obl, new CodeFix.FloatingPointCastFix(pc, obl, right, left, rightType));

        return true;
      }

      return false;
    }

    #endregion

    #region Try Suggesting the removal of a constructor

    public bool TrySuggestFixingConstructor(APC pc, string name, bool isConstructor, IEnumerable<MinimalProofObligation> obligations)
    {
      if (!WantSuggestionAtPC(pc)) return false;

      if (isConstructor && obligations.Any()) 
      {
        this.Add(null, new CodeFix.RemoveConstructorFix(pc, name, obligations));
        return true;
      }
      return false;
    }

    #endregion

    #region Try suggesting an equivalent yet not overflowing/underflowing expression

    public bool TrySuggestNonOverflowingExpression<Variable>(ref ParametersSuggestNonOverflowingExpression<Variable> context)
    {
      if (!WantSuggestionAtPC(context.pc)) return false;

      if (context.exp != null && context.pcWithSourceContext.HasRealSourceContext)
      {
        var exp = context.Simplificator(context.pc, context.exp, true);

        if (exp.IsBinary && exp.BinaryOp.IsComparisonBinaryOperator())
        {
          if (InTypeRange(context.pc.Block.First, exp.BinaryLeft, context.factQuery, context.TypeRange) && InTypeRange(context.pc.Block.First, exp.BinaryRight, context.factQuery, context.TypeRange))
          {
            return false;
          }
        }
        else if (InTypeRange(context.pc.Block.First, exp, context.factQuery, context.TypeRange))
        {
          return false;
        }
        var expressionFixer = new CodeFixesForOverflowingExpression<Variable>(context.pcWithSourceContext, context.factQuery, this.ZeroExp);
        BoxedExpression expFixed;
        if (expressionFixer.TryRefactorExpression(exp, out expFixed))
        {
          this.Add(null, new CodeFix.NonOverflowingExpressionFix(context.pcWithSourceContext, context.Simplificator(context.pc, context.exp, false), expFixed));
          return true;
        }
      }

      return false;
    }

    private bool InTypeRange<Variable>(APC pc, BoxedExpression exp, IFactQuery<BoxedExpression, Variable> factQuery, Func<Variable, IntervalStruct> TypeRange)
    {
      Variable v;
      if (exp.TryGetFrameworkVariable(out v))
      {
        var range = TypeRange(v);
        if (range.IsValid)
        {
          if (factQuery.IsTrue(pc, BoxedExpression.Binary(BinaryOperator.Cle, range.MinValue, exp)) == ProofOutcome.True
            && factQuery.IsTrue(pc, BoxedExpression.Binary(BinaryOperator.Cle, exp, range.MaxValue)) == ProofOutcome.True)
          {
            return true;
          }
        }
      }

      return false;
    }

    #endregion

    #region Try suggesting an off-by one fix

    public bool TrySuggestOffByOneFix<Variable>(ref ParametersSuggestOffByOneFix<Variable> context)
    {
      if (!WantSuggestionAtPC(context.pc)) return false;
      if (context.pcWithSourceContext.HasRealSourceContext)
      {
        BinaryOperator bop;
        BoxedExpression left, right;
        if (context.exp.IsBinaryExpression(out bop, out left, out right))
        {
          switch (bop)
          {
            case BinaryOperator.Clt:
              return TrySuggestOffByOneFixInternal(context.obl, context.pc, context.pcWithSourceContext, context.isArrayAccess, left, right, context.exp, context.AccessPath, context.IsArrayLength, context.factQuery);

            case BinaryOperator.Cgt:
              return TrySuggestOffByOneFixInternal(context.obl, context.pc, context.pcWithSourceContext, context.isArrayAccess, right, left, context.exp, context.AccessPath, context.IsArrayLength, context.factQuery);

            default:
              // do nothing;
              return false;

          }
        }
      }
      return false;
    }

    private bool TrySuggestOffByOneFixInternal<Variable>(ProofObligation obl, APC pc, APC pcWithSourceContext, bool isArrayAccess, BoxedExpression left, BoxedExpression right, BoxedExpression original, Func<Variable, FList<PathElement>> AccessPath, Func<BoxedExpression, bool> IsArrayLength, IFactQuery<BoxedExpression, Variable> factQuery)
    {
      Contract.Requires(pcWithSourceContext.HasRealSourceContext);
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Requires(original != null);
      Contract.Requires(AccessPath != null);
      Contract.Requires(IsArrayLength != null);
      Contract.Requires(factQuery != null);


      var checkIsSourceLevelReadable = new SourceLevelReadableExpression<Variable>(AccessPath);
      if (checkIsSourceLevelReadable.Visit(left, new Void()) == null || checkIsSourceLevelReadable.Visit(right, new Void()) == null)
      {
        return false;
      }
  
      var LeqExp = BoxedExpression.Binary(BinaryOperator.Cle, left, right);
      if (factQuery.IsTrue(pc, LeqExp) == ProofOutcome.True)
      {
        // we know that left < right is top, but left <= right is true

        var leftMinusOne = BoxedExpression.Binary(BinaryOperator.Sub, left, this.OneExp.Value);

        //if right is an array length
        if (isArrayAccess || IsArrayLength(right))
        {
          // We do not want to introduce a buffer underflow
          if (factQuery.IsGreaterEqualToZero(pc, leftMinusOne) != ProofOutcome.True)
          {
            return false;
          }

          // avoid the silly suggestion 0-1 <= array.Length
          int value;
          if (left.IsConstantIntOrNull(out value) && value == 0)
          {
            return false;
          }

          this.Add(obl, new CodeFix.ArrayOffByOneFix(pcWithSourceContext, obl, left, Simplify(leftMinusOne)));
        }
        else
        {
          var suggestion = left.Equals(right) ? 
            BoxedExpression.Binary(BinaryOperator.Clt, leftMinusOne, right)
            : LeqExp;
          this.Add(obl, new CodeFix.OffByOneFix(pcWithSourceContext, obl, original, Simplify(suggestion)));
        }
      }
      
      return true;
    }
    #endregion


    public bool IsEnabled
    {
      get { return true; }
    }
  }

  internal abstract class CodeFix : ICodeFix
  {
    private readonly APC pc;

    private readonly ProofObligation obligation;

    CodeFix(APC pc, ProofObligation obligation)
    {
      this.pc = pc;
      this.obligation = obligation;
    }

    public APC PC { get { return this.pc; } }

    public ProofObligation Obligation { get { return this.obligation; } }

    public string Suggest()
    {
      var action = this.Action != null? this.Action + " " : null;
      return string.Format("{0}. Fix: {1}{2}", this.SuggestMessage(), action, this.SuggestCode());
    }

    public abstract string SuggestMessage();

    public abstract string SuggestCode();

    public abstract string GetMessageForSourceObligation();

    public abstract CodeFixKind Kind { get; }

    public virtual bool IsAGoodConstraint(BoxedExpression exp)
    {
      return exp != null && !exp.IsConstantFalse() && !exp.IsConstantTrue();
    }

    public virtual string Action { get { return null; } }

    public override string ToString()
    {
      return string.Format("Fix for {0}", pc.PrimarySourceContext());
    }

    #region Particular fixes

    public class TestFix : CodeFix
    {
      public readonly BoxedExpression WarningCondition;
      public readonly BoxedExpression Fix;

      public TestFix(APC pc, ProofObligation obl,  BoxedExpression WarningCondition, BoxedExpression Fix)
        : base(pc, obl)
      {
        Contract.Requires(obl != null);
        Contract.Requires(WarningCondition != null);
        Contract.Requires(Fix != null);

        this.WarningCondition = WarningCondition;
        this.Fix = Fix;
      }

      override public string SuggestMessage()
      {
        return string.Format("Consider replacing {0}", this.WarningCondition);
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.Test; }
      }

      public override string SuggestCode()
      {
        return this.Fix.ToString();
      }
      public override string GetMessageForSourceObligation()
      {
        if(this.WarningCondition.CanPrintAsSourceCode())
        {
          return string.Format("Maybe the guard {0} is too weak?", this.WarningCondition);
        }
        else
        {
          return "Maybe a guard is too weak?";
        }
      }

    }

    public class StrengthenTestFix : CodeFix
    {
      public readonly BoxedExpression NewTest;
      public readonly bool StrengthenNullCheck;

      public StrengthenTestFix(APC pc, ProofObligation obl, BoxedExpression newTest, bool strengthenNullCheck)
        : base(pc, obl)
      {
        Contract.Requires(newTest != null);
        Contract.Requires(obl != null);

        this.NewTest = newTest;
        this.StrengthenNullCheck = strengthenNullCheck;
      }

      public override string SuggestMessage()
      {
        return this.StrengthenNullCheck ?
          "Consider strengthening the null check" :
          "Consider strengthening the guard";
      }

      public override string Action
      {
        get
        {
          return "Add";
        }
      }

      public override string SuggestCode()
      {
        return string.Format("&& {0}", this.NewTest.ToString());
      }

      public override string GetMessageForSourceObligation()
      {
        if (this.StrengthenNullCheck)
        {
          return "Should you strengthen a check for null?";
        }
        else
        {
          return "Consider strengthening some guard?";
        }
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.StrengthenTest; }
      }
    }

    public class ConstantInitializationFix : CodeFix
    {
      public readonly BoxedExpression Variable;
      public readonly object constant;
      public readonly BoxedExpression Constraint;// can be null
      public readonly BoxedExpression Fix; // can be null

      public ConstantInitializationFix(APC pc, ProofObligation obl, BoxedExpression Variable, object constant, BoxedExpression constraint, BoxedExpression fix)
        : base(pc, obl)
      {
        Contract.Requires(obl != null);
        Contract.Requires(Variable != null);
        // the constant can be itself null!

        this.Variable = Variable;
        this.constant = constant;
        this.Constraint = constraint;
        this.Fix = fix;
      }

      public override string SuggestMessage()
      {
        var suggestedFix = this.IsAGoodConstraint(this.Constraint) ? string.Format("(e.g., satisfying {0})", this.Constraint.ToString()) : "";
        return string.Format("Consider initializing {0} with a value other than {1} {2}", this.Variable, this.constant ?? "null", suggestedFix);
      }

      public override string SuggestCode()
      {
        return this.Fix != null ? string.Format("{0};", this.Fix.ToString()) : "<none>";
      }

      public override string GetMessageForSourceObligation()
      {
        return String.Format("The error may be caused by the initialization of {0}", this.Variable);
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.ConstantInitialization; }
      }

    }

    public class ExpressionInitializationFix : CodeFix
    {
      public readonly BoxedExpression Variable;
      public readonly BoxedExpression NewExpression;
      public readonly BoxedExpression Constraint;
      public readonly BoxedExpression Fix;

      public ExpressionInitializationFix(APC pc, ProofObligation obl, BoxedExpression Variable, BoxedExpression NewExpression, BoxedExpression constraint, BoxedExpression fix)
        : base(pc, obl)
      {
        Contract.Requires(Variable != null);
        Contract.Requires(obl != null);

        this.Variable = Variable;
        this.NewExpression = NewExpression;
        this.Constraint = constraint;
        this.Fix = fix;
      }

      public override string SuggestMessage()
      {
        var suggestedFix = this.IsAGoodConstraint(this.Constraint) ? string.Format("(e.g., satisfying {0})", this.Constraint.ToString()) : "";
        return string.Format("Consider initializing {0} with a value other than {1} {2}", this.Variable, this.NewExpression, suggestedFix);
      }

      public override string SuggestCode()
      {
        return this.Fix!= null ? string.Format("{0};", this.Fix.ToString()) : "<none>";
      }

      public override string GetMessageForSourceObligation()
      {
        return String.Format("The error may be caused by the initialization of {0}", this.Variable);
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.ExpressionInitialization; }
      }

    }

    public abstract class BaselineAssume : ICodeFix
    {
      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.assume != null);
        Contract.Invariant(this.obl != null);
      }

      private readonly BoxedExpression assume;
      private APC pc;
      private ProofObligation obl;

      public BaselineAssume(APC pc, ProofObligation obl, BoxedExpression assume)
      {
        Contract.Requires(obl != null);
        Contract.Requires(assume != null);

        this.pc = pc;
        this.assume = assume;
        this.obl = obl;
      }

      public string Suggest()
      {
        return string.Format("Baseline assumption {0}", this.assume.ToString());
      }

      public string SuggestCode()
      {
        return string.Format("Contract.Assume({0});", this.assume);
      }

      public string GetMessageForSourceObligation()
      {
        return null;
      }

      public CodeFixKind Kind
      {
        get { return CodeFixKind.BaselineAssume; }
      }

      public BoxedExpression Assumption { get { return this.assume; } }

      public APC PC { get { return this.pc; } }
      public ProofObligation Obligation { get { return this.obl; } }
    }

    public class BaselineAssumeAtCallReturn : BaselineAssume
    {
      private object methodCalled;

      public BaselineAssumeAtCallReturn(APC pc, ProofObligation obl, BoxedExpression assume, object methodCalled)
        : base(pc, obl, assume)
      {
        Contract.Requires(obl != null);
        Contract.Requires(assume != null);
        Contract.Requires(methodCalled != null);

        this.methodCalled = methodCalled;
      }

      public object MethodInfo { get { return this.methodCalled; } }
    }

    public class Assume : CodeFix
    {
      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.what != null);
      }

      private readonly BoxedExpression what;

      public Assume(APC pc, ProofObligation obl, BoxedExpression what)
        : base(pc, obl)
      {
        Contract.Requires(obl != null);
        Contract.Requires(what != null);

        this.what = what;
      }

      public override string SuggestMessage()
      {
        return string.Format("Consider adding the assumption {0}", this.what.ToString());
      }

      public override string Action
      {
        get
        {
          return "Add";
        }
      }

      public override string SuggestCode()
      {
        return string.Format("Contract.Assume({0});", this.what);
      }

      public override string GetMessageForSourceObligation()
      {
        return "Are you making some assumption on some callee that it is unknown to the static analyzer?";
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.AssumeLocalInitialization; }
      }
    }

    public class ArrayInitializationFix : CodeFix
    {
      public readonly BoxedExpression Array;
      public readonly BoxedExpression Constraint;
      public readonly BoxedExpression SuggestedExpression;

      public ArrayInitializationFix(APC pc, ProofObligation obl, BoxedExpression array, BoxedExpression constraint, BoxedExpression suggestedExpression)
        : base(pc, obl)
      {
        Contract.Requires(obl != null);
        // array can be null, if we have no access path 
        Contract.Requires(constraint != null);
        Contract.Requires(suggestedExpression != null);

        this.Array = array;
        this.Constraint = constraint;
        this.SuggestedExpression = suggestedExpression;
      }

      public override string SuggestMessage()
      {
        return string.Format("Consider initializing the array{0} with a value larger than {1}", this.GetArrayName(), this.Constraint.ToString());
      }

      public override string SuggestCode()
      {
        return this.SuggestedExpression.ToString();
      }

      public override string GetMessageForSourceObligation()
      {
        var arrayName = this.GetArrayName();
        if(!string.IsNullOrEmpty(arrayName))
        {
          return string.Format("The error may be caused by the initialization of {0}", arrayName);
        }
        else
        {
          return string.Format("Should you consider allocating a larger array?");
        }
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.ArrayInitialization; }
      }

      private string GetArrayName()
      {
        // TODO TODO : We are not writing this.Array, as for some reason we get a symbolic value instead of the access path!
        return (this.Array != null && this.Array.AccessPath != null) ? this.Array.ToString() : "";
      }

    }

    // different version of method class result that is aware of which vars are havoc'd by the function and which are not
    public class AssumeReturnValueResult : CodeFix
    {
      public readonly BoxedExpression Fix;
      public readonly string CalleeName; // can be null
      public readonly object CalleeObj;

      public AssumeReturnValueResult(APC pc, ProofObligation obl, BoxedExpression fix, object CalleeObj, string Callee = null)
        : base(pc, obl)
      {
        Contract.Requires(fix != null);
        this.Fix = fix;
        this.CalleeName = Callee;
        this.CalleeObj = CalleeObj;
      }

      public override string SuggestMessage()
      {
        var callee = this.CalleeName != null ?
          string.Format(" to method {0}", this.CalleeName) : null;
        return string.Format("This condition should hold: {0}. Add an assume, a postcondition{1}, or consider a different initialization", this.Fix, callee);
      }

      public override string Action
      {
        get
        {
          return "Add";
        }
      }

      public override string SuggestCode()
      {
        return string.Format("Contract.Assume({0});", this.Fix);
      }

      public override string GetMessageForSourceObligation()
      {
        string methodName = this.CalleeName ?? "a callee";
        return string.Format("Are you making some assumption on {0} that the static checker is unaware of?", methodName);
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.MethodCallResult; }
      }
    }

    public class MethodCallResult : CodeFix
    {
      public readonly BoxedExpression Fix;
      public readonly BoxedExpression RawFix; // can be null
      public readonly string CalleeName; // can be null
      public readonly object CalleeObj;

      public MethodCallResult(APC pc, ProofObligation obl, BoxedExpression fix, BoxedExpression/*?*/ rawFix, object CalleeObj, string Callee = null)
        : base(pc, obl)
      {
        Contract.Requires(fix != null);
        
        this.Fix = fix;
        this.RawFix = rawFix;
        this.CalleeName = Callee;
        this.CalleeObj = CalleeObj;
      }

      public override string SuggestMessage()
      {
        var callee = this.CalleeName != null ?
          string.Format(" to method {0}", this.CalleeName) : null;
        return string.Format("This condition should hold: {0}. Add an assume, a postcondition{1}, or consider a different initialization", this.Fix, callee);
      }

      public override string Action
      {
        get
        {
          return "Add (after)";
        }
      }

      public override string SuggestCode()
      {
        return string.Format("Contract.Assume({0});", this.Fix);
      }

      public override string GetMessageForSourceObligation()
      {
        string methodName = this.CalleeName ?? "a callee";
        return string.Format("Are you making some assumption on {0} that the static checker is unaware of?", methodName);
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.MethodCallResult; }
      }

    }

    // Sometimes we have no code available, so we simply report the warning message
    public class MethodCallResultNoCode : CodeFix
    {
      private readonly string MethodName;

      public MethodCallResultNoCode(APC pc, ProofObligation obl, string methodname)
        : base(pc, obl)
      {
        this.MethodName = methodname;
      }

      public override string SuggestMessage()
      {
        return string.Format("The value returned{0} should be non-null. Consider adding a postcondition or an assumption",
          this.MethodName != null ? " by " + this.MethodName : "");
      }

      public override string SuggestCode()
      {
        return "<none>";
      }
      public override string GetMessageForSourceObligation()
      {
        string methodName = this.MethodName ?? "a callee";
        return string.Format("Do you expect that {0} returns non-null?", methodName);
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.MethodCallResultNoCode; }
      }
    }

    public class MethodShouldbePure : CodeFix
    {
      private readonly string MethodName;
      private readonly BoxedExpression HavocedExpression;

      public MethodShouldbePure(APC pc, ProofObligation obl, string methodName, BoxedExpression expression)
        : base(pc, obl)
      {
        Contract.Requires(methodName != null);
        Contract.Requires(expression != null);

        this.MethodName = methodName;
        this.HavocedExpression = expression;
      }

      public override string SuggestMessage()
      {
        return string.Format("The value of {0} may be modified by the invocation to {1}. Consider adding the attribute [Pure] to {1} or a postcondition",
          this.HavocedExpression, this.MethodName);
      }

      public override string SuggestCode()
      {
        return string.Format("Add attribute [Pure] to {0}", this.MethodName);
      }

      public override string GetMessageForSourceObligation()
      {
        return string.Format("Did the static checker lost some information because {0} is not marked as [Pure]?", this.MethodName);
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.MethodShouldBePure; }
      }
    }

    public class FloatingPointCastFix : CodeFix
    {
      public readonly BoxedExpression NarrowerExp, WiderExp;
      public readonly ConcreteFloat cast;

      public FloatingPointCastFix(APC pc, ProofObligation obl, BoxedExpression narrowerExp, BoxedExpression widerExp, ConcreteFloat cast)
        : base(pc, obl)
      {
        Contract.Requires(obl != null);
        Contract.Requires(cast == ConcreteFloat.Float32 || cast == ConcreteFloat.Float64);

        this.NarrowerExp = narrowerExp;
        this.WiderExp = widerExp;
        this.cast = cast;
      }

      public override string SuggestMessage()
      {
        return "Consider adding an explicit cast to force truncation";
      }

      public override string SuggestCode()
      {
        return string.Format("{0} == {1}{2}", this.NarrowerExp, this.cast.ToCastString(), this.WiderExp);
      }
      public override string GetMessageForSourceObligation()
      {
        return string.Format("Do you need to narrow the expression {0}?", this.WiderExp);
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.FloatingPointCast; }
      }

    }

    public class RemoveConstructorFix : CodeFix
    {
      public readonly string ConstructorName;
      public readonly IEnumerable<MinimalProofObligation> FalseObligations;

      public RemoveConstructorFix(APC pc, string name, IEnumerable<MinimalProofObligation> falseobligations)
        : base(pc, null)
      {
        Contract.Requires(name != null);
        Contract.Requires(falseobligations != null);

        this.ConstructorName = name;
        this.FalseObligations = falseobligations;
      }

      public override string SuggestMessage()
      {
        
        //return string.Format("Consider removing the constructor {0} (it violates an inferred object invariant)", this.ConstructorName);
        var provenance = this.FalseObligations.First().Provenance;

        if (provenance.Any())
        {
          var condition = provenance.First().Condition;
          var weakerInvariant = BuildWeakerInvariant(condition);

          return string.Format("The constructor {0} violates the inferred object invariant {1}. Consider removing it or weakening the object invariant to {2}",
            this.ConstructorName, condition, weakerInvariant);
        }
        else
        {
          return string.Format("The constructor {0} violates an inferred object invariant. Consider removing the constructor or weakening the object invariant by adding a typestate", this.ConstructorName);
        }
      }

      private string BuildWeakerInvariant(BoxedExpression invariant)
      {
        // assume ConstructorName is A.B.C.ctor(...)
        var constructornameSplit = this.ConstructorName.Substring(0, this.ConstructorName.IndexOf('(')).Split('.');
        var className = constructornameSplit[constructornameSplit.Length-2];

        return string.Format("Is{0}Invoked || {1}", className, invariant);
      }

      public override string Action
      {
        get
        {
          return "Add";
        }
      }

      public override string SuggestCode()
      {
        var provenance = this.FalseObligations.First().Provenance;
        if (provenance.Any())
        {
          return BuildWeakerInvariant(provenance.First().Condition);
        }
        else
        {
          return String.Empty;
        }
      }

      public override string GetMessageForSourceObligation()
      {
        return null;
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.RemoveConstructor; }
      }

    }

    public class NonOverflowingExpressionFix : CodeFix
    {
      public readonly BoxedExpression OriginalExp;
      public readonly BoxedExpression FixedExp;

      public NonOverflowingExpressionFix(APC pc, BoxedExpression originalExp, BoxedExpression fixedExp)
      : base(pc, null)
      {
        Contract.Requires(originalExp != null);
        Contract.Requires(fixedExp != null);

        this.OriginalExp = originalExp;
        this.FixedExp = fixedExp;
      }

      public override string SuggestMessage()
      {
        return string.Format("Consider replacing the expression {0} with an equivalent, yet not overflowing expression", this.OriginalExp, this.FixedExp);
      }

      public override string SuggestCode()
      {
        return this.FixedExp.ToString();
      }

      public override string GetMessageForSourceObligation()
      {
        return null;
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.NonOverflowingExpression; }
      }
    }

    abstract public class OffByOneFixBase : CodeFix
    {
      public readonly BoxedExpression OriginalExp;
      public readonly BoxedExpression FixedExp;

      public OffByOneFixBase(APC pc, ProofObligation obl, BoxedExpression originalExp, BoxedExpression fixedExp)
        : base(pc, obl)
      {
        Contract.Requires(originalExp != null);
        Contract.Requires(fixedExp != null);

        this.OriginalExp = originalExp;
        this.FixedExp = fixedExp;
      }

      public abstract override string SuggestMessage();

      public abstract override string SuggestCode();

      public abstract override CodeFixKind Kind { get; }
    }

    public class OffByOneFix : OffByOneFixBase
    {
      public OffByOneFix(APC pc, ProofObligation obl, BoxedExpression originalExp, BoxedExpression fixedExp)
        : base(pc, obl, originalExp, fixedExp)
      {
        Contract.Requires(originalExp != null);
        Contract.Requires(fixedExp != null);
      }

      public override string SuggestMessage()
      {
        return string.Format("Did you mean {1} instead of {0}?", this.OriginalExp, this.FixedExp);
      }

      public override string SuggestCode()
      {
        return this.FixedExp.ToString();
      }

      public override string GetMessageForSourceObligation()
      {
        return "Is it an off-by-one?";
      }

      public override CodeFixKind Kind
      {
        get { return CodeFixKind.OffByOne; }
      }
    }

    public class ArrayOffByOneFix : OffByOneFixBase
    {
      public ArrayOffByOneFix(APC pc, ProofObligation obl, BoxedExpression originalExp, BoxedExpression fixedExp)
        : base(pc, obl, originalExp, fixedExp)
      {
        Contract.Requires(originalExp != null);
        Contract.Requires(fixedExp != null);
        Contract.Requires(obl != null);
      }

      public override string SuggestMessage()
      {
        return string.Format("Possible off-by one: did you mean indexing with {1} instead of {0}?", this.OriginalExp, this.FixedExp);
      }

      public override string SuggestCode()
      {
        return this.FixedExp.ToString();
      }

      public override string GetMessageForSourceObligation()
      {
        return string.Format("Did you mean {1} instead of {0}?", this.OriginalExp, this.FixedExp);
      }
      
      public override CodeFixKind Kind
      {
        get { return CodeFixKind.ArrayOffByOne; }
      }

    }

    #endregion
  }
}
