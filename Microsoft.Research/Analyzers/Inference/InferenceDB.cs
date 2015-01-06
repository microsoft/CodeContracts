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

namespace Microsoft.Research.CodeAnalysis.Inference
{
  public class InferenceDB
  {
    #region Invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.inferred != null);
      Contract.Invariant(this.SimplifyExpression != null);
    }

    #endregion

    #region State

    // we keep the string for optimization
    private readonly List<Tuple<BoxedExpression, string, ProofOutcome, List<ProofObligation>>> inferred;
    private Dictionary<BoxedExpression, List<ProofObligation>> preconditions;
    private Func<BoxedExpression, BoxedExpression> SimplifyExpression;
    private Predicate<BoxedExpression> IsSuitableInRequires;

    public readonly object ExtraInfo; // Some extra piece of information we want to keep. For instance (a part of) the warning context for the corresponding obligation

    #endregion

    #region Constructor

    public InferenceDB(Func<BoxedExpression, BoxedExpression> SimplifyExpression, Predicate<BoxedExpression> IsSuitableInRequires, object extraInfo = null)
    {
      Contract.Requires(SimplifyExpression != null);

      this.inferred = new List<Tuple<BoxedExpression, string, ProofOutcome, List<ProofObligation>>>();
      this.SimplifyExpression = SimplifyExpression;
      this.IsSuitableInRequires = IsSuitableInRequires;
      this.ExtraInfo = extraInfo;
    }

    #endregion

    #region Instance methods

    public bool TryLookUp(ProofObligation obl, BoxedExpression exp, out ProofOutcome outcome)
    {
      Contract.Requires(obl != null);
      Contract.Requires(exp != null);

      var expStr = exp.ToString();
      foreach (var quad in this.inferred)
      {
        if (quad.Item2 == expStr)
        {
          quad.Item4.Add(obl);
          outcome = quad.Item3;
          return true;
        }
      }
      outcome = ProofOutcome.Top;

      return false;
    }

    public List<uint> CausesFor(BoxedExpression exp)
    {
      if (this.preconditions == null || exp == null)
      {
        return null;
      }

      List<ProofObligation> causes;
      if (this.preconditions.TryGetValue(exp, out causes))
      {
        return causes.ConvertAll(obl => obl.ID).Distinct().ToList();
      }

      return null;
    }

    public void Add(ProofObligation obl, BoxedExpression exp, ProofOutcome outcome)
    {
      Contract.Requires(obl != null);
      Contract.Requires(exp != null);

      this.inferred.Add(new Tuple<BoxedExpression, string, ProofOutcome, List<ProofObligation>>(exp, exp.ToString(), outcome, new List<ProofObligation>() { obl }));

      // we added a new condition, let us discare the previous ones
      this.preconditions = null; 
    }

    public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GenerateConditions()
    {
      Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>>>() != null);

      if (this.preconditions == null)
      {
        var asSimpleList = ProjectExpressions(this.inferred);
        var removedStrongerPremises = RemoveStrongerPremises(asSimpleList);
        var removeSubsumed = ComputeMinimalWithSameUpperBound(removedStrongerPremises);
        var removedTrivialities = RemoveCoveringPremises(removeSubsumed);
        var removedContraddictions = RemoveEvidentContraddictions(removedTrivialities);

        this.preconditions = removedContraddictions;
      }

      Contract.Assert(this.preconditions != null);

      return this.preconditions
        .Select(p => new KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>(SimplifyExpression(p.Key),p.Value.Select(obl => obl.MinimalProofObligation)))
        .Where(pair => !pair.Key.IsConstantTrue() && this.IsSuitableInRequires(pair.Key)) // Avoid suggesting "requires true" and malformed preconditions
        .Distinct(BoxedExpression.EqualityPairComparer);
  }

    #region Simplification Logic

    static private Dictionary<BoxedExpression, List<ProofObligation>> ProjectExpressions(List<Tuple<BoxedExpression, string, ProofOutcome, List<ProofObligation>>> elements)
    {
      var result = new Dictionary<BoxedExpression, List<ProofObligation>>();

      foreach (var el in elements)
      {
        result[el.Item1] = new List<ProofObligation>(el.Item4);
      }

      return result;
    }

    static private Dictionary<BoxedExpression, List<ProofObligation>> RemoveStrongerPremises(Dictionary<BoxedExpression, List<ProofObligation>> inferred)
    {
      var result = new Dictionary<BoxedExpression, List<ProofObligation>>();

      foreach (var pair in inferred)
      {
        BoxedExpression premise, condition;
        BinaryOperator bop;

        if (pair.Key.IsBinaryExpression(out bop, out premise, out condition) && bop == BinaryOperator.LogicalOr)
        {
          // premise || conddition, but we have already condition without premise
          if (inferred.ContainsKey(condition))
          {
            List<ProofObligation> obligations;

            // we already processed  "condition", so we update the proof obligation
            if (result.TryGetValue(condition, out obligations) || inferred.TryGetValue(condition, out obligations))
            {
              obligations.AddRange(pair.Value);
            }
            else
            {
              Contract.Assume(false, "Impossible case!");
            }


            // was: continue
            continue;
          }

          // premise || premise
          if (BoxedExpression.SimpleSyntacticEquality(premise, condition))
          {
            result.AddOrUpdate(premise, pair.Value);

            // was: result.Add(premise);
            continue;
          }

          // if cond ==> premise, remove the premise
          if (Implies(condition, premise))
          {
            result.AddOrUpdate(condition, pair.Value);
            // was: result.Add(condition);
            continue;
          }

          // a < b || a == b, becomes a <= b
          BoxedExpression joined;
          if (CanMergeTogether(condition, premise, out joined))
          {
            result.AddOrUpdate(joined, pair.Value);
            // result.Add(joined);
            continue;
          }

          // F: We depend on the order of the facts!!! We should iterate to a fixpoint or we should pre-sort the list of inferred facts

          // "premise && !premise || condition" becomes "premise && condition"
          BoxedExpression newPre;
          if(PremiseIsImpliedByOtherPreconditions(premise, condition, result.Keys, out newPre))
          {
            result.AddOrUpdate(newPre, pair.Value);
            continue;
          }
        }
        result.AddOrUpdate(pair.Key, pair.Value);
        // result.Add(pair);
      }

      return result;
    }

    static private bool CanMergeTogether(BoxedExpression condition, BoxedExpression premise, out BoxedExpression joined)
    {
      Contract.Requires(condition != null);
      Contract.Requires(premise != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out joined) != null);

      BinaryOperator bopCondition, bopPremise;
      BoxedExpression condition1, condition2, premise1, premise2;
      if (condition.IsBinaryExpression(out bopCondition, out condition1, out condition2) && premise.IsBinaryExpression(out bopPremise, out premise1, out premise2))
      {
        condition = premise = null; // kill the parameters, just to make sure we do not do errors afterwards

        // remove the (int)a.Length, if any
        condition1 = condition1.StripIfCastOfArrayLength();
        condition2 = condition2.StripIfCastOfArrayLength();
        premise1 = premise1.StripIfCastOfArrayLength();
        premise2 = premise2.StripIfCastOfArrayLength();

        // (< || ==) or ( == || <)
        if ((bopCondition == BinaryOperator.Clt && bopPremise == BinaryOperator.Ceq) 
          ||(bopCondition == BinaryOperator.Ceq && bopPremise == BinaryOperator.Clt))
        {
          if (condition1.Equals(premise1) && condition2.Equals(premise2))
          {
            joined = BoxedExpression.Binary(BinaryOperator.Cle, condition1, condition2);
            return true;
          }

          if (condition1.Equals(premise2) && condition2.Equals(premise1))
          {
            joined = BoxedExpression.Binary(
              bopCondition == BinaryOperator.Ceq
              ? BinaryOperator.Cge   // a == b || b < a   ==> a >= b
              : BinaryOperator.Cle,  // a < b  || b == a  ==> a <= b
              condition1, condition2);
            
            return true;
          }

        }

        // (> || ==) or ( == || >)
        if ((bopCondition == BinaryOperator.Cgt && bopPremise == BinaryOperator.Ceq)
          || (bopCondition == BinaryOperator.Ceq && bopPremise == BinaryOperator.Cgt))
        {
          if (condition1.Equals(premise1) && condition2.Equals(premise2))
          {
            joined = BoxedExpression.Binary(BinaryOperator.Cge, condition1, condition2);
            return true;
          }

          if (condition1.Equals(premise2) && condition2.Equals(premise1))
          {
            joined = BoxedExpression.Binary(
              bopCondition == BinaryOperator.Ceq
              ? BinaryOperator.Cle   // a == b || b > a   ==> a <= b
              : BinaryOperator.Cge,  // a > b  || b == a  ==> a >= b
              condition1, condition2);

            return true;
          }
        }

        // a  < b ||  b < a -- > a != b 
        if(bopCondition == BinaryOperator.Clt && bopPremise == BinaryOperator.Clt)
        {
          // a  < b ||  b < a -- > a != b 
          if(condition1.Equals(premise2) && condition2.Equals(premise1))
          {
            joined = BoxedExpression.Binary(BinaryOperator.Cne_Un, condition1, condition2);
            return true;
          }
        }

      }

      joined = null;
      return false;
    }

    static private bool PremiseIsImpliedByOtherPreconditions(BoxedExpression premises, BoxedExpression condition, IEnumerable<BoxedExpression> knownPreconditions, out BoxedExpression newPrecondition)
    {
      Contract.Requires(premises != null);
      Contract.Requires(knownPreconditions != null);
      Contract.Requires(condition != null);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out newPrecondition) != null);

      if(!knownPreconditions.Any())
      {
        newPrecondition = null;
        return false;
      }

      var oldPremises = premises.SplitDisjunctions();
      var newPremises = new List<BoxedExpression>();

      foreach (var disjunct in oldPremises)
      {
        if (knownPreconditions.Contains(disjunct.Negate()))
        {
          continue;
        }
        else
        {
          newPremises.Add(disjunct);
        }
      }

      if (oldPremises.Count == newPremises.Count)
      {
        newPrecondition = null;
        return false;
      }
      else
      {
        if (newPremises.Count == 0)
        {
          newPrecondition = condition;
        }
        else
        {
          var newPremisesAsExp = newPremises.ToDisjunction();
          if (newPremisesAsExp != null)
          {
            newPrecondition = BoxedExpression.Binary(BinaryOperator.LogicalOr, newPremisesAsExp, condition);
          }
          else
          {
            newPrecondition = condition;
          }
        }
        return true;
      }
    }

    private Dictionary<BoxedExpression, List<ProofObligation>> ComputeMinimalWithSameUpperBound(Dictionary<BoxedExpression, List<ProofObligation>> inferred)
    {
      Contract.Requires(inferred != null);
      Contract.Ensures(Contract.Result<Dictionary<BoxedExpression, List<ProofObligation>>>() != null);


      var buckets = new Dictionary<Tuple<BinaryOperator, BoxedExpression>, List<Tuple<BoxedExpression, BoxedExpression, List<ProofObligation>>>>();
      var result = new Dictionary<BoxedExpression, List<ProofObligation>>();

      // First we group expressions by upper bounds. 
      // If no lower bound is found, we just continue
      foreach (var pair in inferred)
      {
        BoxedExpression e1, e2;
        if (pair.Key.IsCheckExp1LEQExp2(out e1, out e2))
        {
          Add(buckets,
            new Tuple<BinaryOperator, BoxedExpression>(BinaryOperator.Cle, e2),
            e1, pair.Key, pair.Value);
        }
        else if (pair.Key.IsCheckExp1LTExp2(out e1, out e2))
        {
          Add(buckets,
            new Tuple<BinaryOperator, BoxedExpression>(BinaryOperator.Cle, e2),
            e1, pair.Key, pair.Value);
        }
        else
        {
          result.Add(pair.Key, pair.Value);
        }
      }

      // Now we filter redundant expressions, according to their upper bound
      foreach (var pair in buckets)
      {
        var value = pair.Value;

        Contract.Assume(value != null);
        Contract.Assume(value.Count > 0);
        Contract.Assume(Contract.ForAll(value, x => x != null));

        if (value.Count == 1)
        {
          Contract.Assume(value[0] != null, "missing quantified invariant");
          // no doubt, just emit it
          result.Add(value[0].Item2, value[0].Item3);
        }
        else
        {
          // the same variable is an upper bound for two or more expressions. Let's try to filter them
          int minIndex;
          if (TryGetSubsumingExpression(value, out minIndex))
          {
            Contract.Assume(value[minIndex] != null);
            Contract.Assume(value[minIndex].Item2 != null);
            result.Add(value[minIndex].Item2, JoinAllProofObligation(pair.Value));
          }
          else
          {
            foreach (var innerPair in value)
            {
              result.Add(innerPair.Item2, innerPair.Item3);
            }
          }
        }
      }

      return result;
    }

    static private List<ProofObligation> JoinAllProofObligation(List<Tuple<BoxedExpression, BoxedExpression, List<ProofObligation>>> list)
    {
      Contract.Requires(list != null);

      var result = new List<ProofObligation>();
      foreach(var triplet in list)
      {
        result.AddRange(triplet.Item3);
      }

      Contract.Assume(result.Count >= 1);
      return result;
    }

    static private Dictionary<BoxedExpression, List<ProofObligation>> RemoveCoveringPremises(Dictionary<BoxedExpression, List<ProofObligation>> original)
    {
      Contract.Requires(original != null);
      Contract.Ensures(Contract.Result<Dictionary<BoxedExpression, List<ProofObligation>>>() != null);

      var result = new Dictionary<BoxedExpression, List<ProofObligation>>();

      // we look for premise ==> cond
      // so the dictionary contains cond --> (premise ==> cond)
      var cached = new Dictionary<BoxedExpression, Tuple<BoxedExpression, List<ProofObligation>>>(original.Count);

      foreach (var pair in original)
      {
        BinaryOperator bop;
        BoxedExpression premise, cond;
        if (pair.Key.IsBinaryExpression(out bop, out premise, out cond) && bop == BinaryOperator.LogicalOr)
        {
          Tuple<BoxedExpression, List<ProofObligation>> prev;
          if (cached.TryGetValue(cond, out prev))
          {
            Contract.Assume(prev != null);
            BoxedExpression prevPremise, prevCond;
            if (prev.Item1.IsBinaryExpression(out bop, out prevPremise, out prevCond) && bop == BinaryOperator.LogicalOr)
            {
              Contract.Assume(prevCond.Equals(cond));

              cached.Remove(cond);

              if (premise.Negate().Equals(prevPremise))
              {
                result.AddOrUpdate(cond, pair.Value);
              }
              else
              {
                result.AddOrUpdate(pair.Key, pair.Value);
                result.AddOrUpdate(prev.Item1, prev.Item2);
              }
            }
            else
            {
              Contract.Assume(false);
            }
          }
          else
          {
            cached[cond] = new Tuple<BoxedExpression,List<ProofObligation>>(pair.Key, pair.Value);
          }
        }
        else
        {
          result.AddOrUpdate(pair.Key, pair.Value);
        }
      }

      foreach (var pairs in cached)
      {
        result.AddOrUpdate(pairs.Value.Item1, pairs.Value.Item2);
      }

      return result;
    }

      /// <summary>
      ///  left ==> right ?
      /// </summary>
    static private bool Implies(BoxedExpression left, BoxedExpression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      BinaryOperator leftOp, rightOp;
      BoxedExpression leftLeft, leftRight, rightLeft, rightRight;

      if (left.IsBinaryExpression(out leftOp, out leftLeft, out leftRight) && right.IsBinaryExpression(out rightOp, out rightLeft, out rightRight))
      {
        #region Equality
        switch (rightOp)
        {
          case BinaryOperator.Ceq:
            {
              if ((leftLeft.Equals(rightLeft) && leftRight.Equals(rightRight)) || (leftLeft.Equals(rightRight) && leftRight.Equals(rightLeft)))
              {
                return new BinaryOperator[] { BinaryOperator.Ceq, BinaryOperator.Cge, BinaryOperator.Cge_Un, BinaryOperator.Cle, BinaryOperator.Cle_Un }.Contains(leftOp);
              }

              return false;
            }

          default:
            {
              break;
            }
        }
        #endregion

        #region Other cases

        // k1 op1 exp and k2 op2 exp
        int k1, k2;
        if (leftRight.Equals(rightRight) && leftLeft.IsConstantInt(out k1) && rightLeft.IsConstantInt(out k2))
        {
          // k1 <= exp and k2 < exp
          if (leftOp == BinaryOperator.Cle && rightOp == BinaryOperator.Clt)
          {
            return k1 < k2;
          }
        }

        // todo: other cases

        #endregion

      }

      return false;
    }

    /// <summary>
    /// Make sure that if we have trivial contraddictions, we will not suggest them
    /// </summary>
    /// <param name="removedTrivialities"></param>
    /// <returns></returns>
    static private Dictionary<BoxedExpression, List<ProofObligation>> RemoveEvidentContraddictions(Dictionary<BoxedExpression, List<ProofObligation>> removedTrivialities)
    {
      Contract.Requires(removedTrivialities != null);

      var inequalities = removedTrivialities.Where(pair => (pair.Key.IsBinary && pair.Key.BinaryOp.IsComparisonBinaryOperator()));

      var toSkip = new Set<BoxedExpression>(); // We accumulate the expressions to skip

      // TODO: this is quadratic, we hope we have few constraints at this point, but we should improve it
      foreach (var pair in inequalities)
      {
        BinaryOperator bop;
        BoxedExpression left, right;

        if (toSkip.Contains(pair.Key))
        {
          continue;
        }

        if(pair.Key.IsBinaryExpression(out bop, out left, out right))
        {
          foreach (var other in inequalities)
          {
            BinaryOperator bopOther;
            BoxedExpression leftOther, rightOther;

            // search for things like left < right && left >= right
            if (other.Key.IsBinaryExpression(out bopOther, out leftOther, out rightOther) 
              && bop == NegateComparison(bopOther)
              && left.Equals(leftOther) && right.Equals(rightOther) )
            {
              toSkip.Add(pair.Key);
              toSkip.Add(other.Key);
            }
          }
        }
        else
        {
          Contract.Assume(false, "Should never be the case, because we filtered them before");
        }
      }

      return removedTrivialities.Where(pair => !toSkip.Contains(pair.Key)).ToDictionary(); // This cast to ToDictionary should be removed, and we should use IEnumerable<> everywhere, but lazyness is so hard to debug
    }

    static private BinaryOperator NegateComparison(BinaryOperator bop)
    {
      Contract.Requires(bop.IsComparisonBinaryOperator());

      BinaryOperator bopResult;
      bop.TryNegate(out bopResult);

      return bopResult;
    }

    #endregion



    #endregion

    #region Privates

    private void Add(
      Dictionary<Tuple<BinaryOperator, BoxedExpression>, List<Tuple<BoxedExpression, BoxedExpression, List<ProofObligation>>>> where,
      Tuple<BinaryOperator, BoxedExpression> key, BoxedExpression value, BoxedExpression precondition, List<ProofObligation> obl)
    {
      Contract.Requires(where != null);
      Contract.Requires(key != null);
      Contract.Requires(value != null);
      Contract.Requires(precondition != null);
      Contract.Requires(obl != null);

      List<Tuple<BoxedExpression, BoxedExpression, List<ProofObligation>>> values;
      var triplet = new Tuple<BoxedExpression, BoxedExpression, List<ProofObligation>>(value, precondition, obl);
      if (where.TryGetValue(key, out values))
      {
        Contract.Assume(values != null);
        // use aliasing here to save one lookup
        values.Add(triplet);
      }
      else
      {
        values = new List<Tuple<BoxedExpression, BoxedExpression, List<ProofObligation>>>() { triplet };
        where[key] = values;
      }
    }

    private bool TryGetSubsumingExpression(List<Tuple<BoxedExpression, BoxedExpression, List<ProofObligation>>> list, out int minIndex)
    {
      Contract.Requires(list != null);
      Contract.Requires(list.Count > 1);
      Contract.Requires(Contract.ForAll(list, x => x.Item1 != null));
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out minIndex) >= 0);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out minIndex) < list.Count);

      Contract.Assume(list[0] != null, "weakness of the checker");
      var max = list[0].Item1;

      Contract.Assume(max != null, "requires quantification over inner field");

      minIndex = 0;
      for (var i = 1; i < list.Count; i++)
      {
        var item = list[i];
        Contract.Assume(item != null, "weakness of the checker");
        var curr = item.Item1;
        Contract.Assume(curr != null);
        var compare = SyntacticComparison(curr, max);

        switch (compare)
        {
          case SyntacticComparisonOutcome.LessEqual:
          case SyntacticComparisonOutcome.Equal:
            {
              if(item.Item2.IsBinary && item.Item2.BinaryOp == BinaryOperator.Clt)
              {
                minIndex = i;
              }

              continue;
            }

          case SyntacticComparisonOutcome.GreaterEqual:
            {
              max = curr;
              minIndex = i;

              continue;
            }

          case SyntacticComparisonOutcome.Top:
            {
              minIndex = -1;
              return false;
            }
        }
      }

      return true;
    }

    private SyntacticComparisonOutcome SyntacticComparison(BoxedExpression left, BoxedExpression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      if (left.Equals(right))
      {
        return SyntacticComparisonOutcome.Equal;
      }

      int leftK, rightK;
      if (left.IsConstantInt(out leftK) && right.IsConstantInt(out rightK))
      {
        if (leftK == rightK)
          return SyntacticComparisonOutcome.Equal;
        if (leftK < rightK)
          return SyntacticComparisonOutcome.LessEqual;
        if (leftK > rightK)
          return SyntacticComparisonOutcome.GreaterEqual;
      }

      BinaryOperator bopLeft, bopRight;
      BoxedExpression bLeftLeft, bLeftRight, bRightLeft, bRightRight;

      var isLeftBin = left.IsBinaryExpression(out bopLeft, out bLeftLeft, out bLeftRight);
      var isRightBin = right.IsBinaryExpression(out bopRight, out bRightLeft, out bRightRight);

      // case "a + b" compared with "a' + b'"
      if (isLeftBin && isRightBin && bopLeft == bopRight)
      {
        switch (bopLeft)
        {
          case BinaryOperator.Add:
            {
              var compareLeft = SyntacticComparison(bLeftLeft, bRightLeft);
              var compareRight = SyntacticComparison(bLeftRight, bRightRight);

              return Join(compareLeft, compareRight);
            }

          // TODO: other cases?
        }
        return SyntacticComparisonOutcome.Top;
      }

      // case "a + b" compared with "a"
      if (isLeftBin && bLeftLeft.Equals(right))
      {
        switch (bopLeft)
        {
          case BinaryOperator.Add:
            {
              if (bLeftRight.IsConstantInt(out leftK))
              {
                if (leftK == 0)
                  return SyntacticComparisonOutcome.Equal;
                if (leftK > 0)
                  return SyntacticComparisonOutcome.GreaterEqual;
                if (leftK < 0)
                  return SyntacticComparisonOutcome.LessEqual;
              }
              break;
            }
        }
        return SyntacticComparisonOutcome.Top;
      }

      // case "a" compared with "a+b"
      if (isRightBin && bRightLeft.Equals(left))
      {
        switch (bopRight)
        {
          case BinaryOperator.Add:
            {
              if (bRightRight.IsConstantInt(out rightK))
              {
                if (rightK == 0)
                  return SyntacticComparisonOutcome.Equal;
                if (rightK > 0)
                  return SyntacticComparisonOutcome.GreaterEqual;
                if (rightK < 0)
                  return SyntacticComparisonOutcome.LessEqual;
              }
              break;
            }
        }
        return SyntacticComparisonOutcome.Top;
      }



      return SyntacticComparisonOutcome.Top;
    }

    private SyntacticComparisonOutcome Join(SyntacticComparisonOutcome compareLeft, SyntacticComparisonOutcome compareRight)
    {
      if (compareLeft == compareRight)
      {
        return compareLeft;
      }
      if (compareLeft == SyntacticComparisonOutcome.Equal)
      {
        return compareRight;
      }
      if (compareRight == SyntacticComparisonOutcome.Equal)
      {
        return compareLeft;
      }

      return SyntacticComparisonOutcome.Top;
    }

    enum SyntacticComparisonOutcome { Top, LessEqual, Equal, GreaterEqual }

    #endregion

  }

}
