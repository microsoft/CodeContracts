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
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  [ContractVerification(true)]
  public class SimplePreconditionDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    : IPreconditionDispatcher
    where LogOptions : IFrameworkLogOptions
    where Variable : IEquatable<Variable>
    where Expression : IEquatable<Expression>
    where Type : IEquatable<Type>
  {
    #region Invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.mdriver != null);
      Contract.Invariant(this.output != null);
      Contract.Invariant(this.preconditions != null);
    }

    #endregion

    #region State

    private const string ContractPreconditionTemplate = "Contract.Requires({0});";

    readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver;
    readonly private IOutputResults output;
    readonly private InferredPreconditionDB preconditions;

    #endregion

    #region Constructor

    public SimplePreconditionDispatcher(
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      IOutputResults output)
    {
      Contract.Requires(mdriver != null);

      this.mdriver = mdriver;
      this.output = output;
      this.preconditions = new InferredPreconditionDB();
    }

    #endregion

    #region IPreconditionDispatcher Members

    public ProofOutcome AddPrecondition(APC pc, BoxedExpression precondition)
    {      
      ProofOutcome outcome;

      // 1. Have we already seen this precondition?
      if (this.preconditions.TryLookUp(precondition, out outcome))
      {
        return outcome;
      }
      else
      {
        outcome = ProofOutcome.Top;
      }

      // 2. Simplify the expression
      precondition = SimplifyAndFix(pc, precondition);

      // 3. See if it is valid
      if (precondition != null && this.mdriver.CanAddRequires())
      {
        if (mdriver.IsExistingPrecondition(precondition.ToString())
          || 
          output.LogOptions.PropagateInferredRequires(mdriver.MetaDataDecoder.IsPropertyGetterOrSetter(mdriver.CurrentMethod)))
        {
          outcome = ProofOutcome.True;
        }

        this.preconditions.Add(precondition, outcome);
      }

      return outcome;
    }

    public List<BoxedExpression> GeneratePreconditions()
    {
      return preconditions.ComputeMinimal();
    }

    public void SuggestPreconditions()
    {
      var pc = this.GetPCForMethodEntry();

      foreach(var precondition in this.GeneratePreconditions())
      {
        output.Suggestion("requires", pc, MakePreconditionString(precondition.ToString()));
      }
    }

    public void PropagatePreconditions()
    {
      var pc = this.GetPCForMethodEntry();

      foreach (var precondition in this.GeneratePreconditions())
      {
        this.mdriver.AddPreCondition(precondition, precondition.ToString(), pc);
      }
    }

    #endregion

    #region Private

    private BoxedExpression SimplifyAndFix(APC pc, BoxedExpression precondition)
    {
      Contract.Requires(precondition != null);

      var decompiler = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.BooleanExpressionsDecompiler<LogOptions>(mdriver);

      BoxedExpression result;
      if (decompiler.FixIt(pc, precondition, out result))
      {
        return result;
      }
      else
      {
        return null; // doesn't type check and is not fixable, ignore
      }
    }

    private APC GetPCForMethodEntry()
    {
      var entry = this.mdriver.Context.MethodContext.CFG.EntryAfterRequires;
      for (int count = 0; count < 10; count++)
      {
        if (entry.PrimaryMethodLocation().HasRealSourceContext) return entry.PrimaryMethodLocation();
        entry = this.mdriver.Context.MethodContext.CFG.Post(entry);
      }
      return this.mdriver.Context.MethodContext.CFG.Entry;
    }

    private static string MakePreconditionString(string condition)
    {
      return string.Format(ContractPreconditionTemplate, condition);
    }

    #endregion

    #region State for the inferred preconditions

    class InferredPreconditionDB
    {
      #region Invariant
      
      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.inferred != null);
      }
      
      #endregion

      #region State
      
      // we keep the string for optimization
      readonly List<Tuple<BoxedExpression, string, ProofOutcome>> inferred;
      List<BoxedExpression> preconditions;

      #endregion

      #region Constructor

      public InferredPreconditionDB()
      {
        this.inferred = new List<Tuple<BoxedExpression, string, ProofOutcome>>();
      }

      #endregion

      #region Instance methods

      public bool TryLookUp(BoxedExpression exp, out ProofOutcome outcome)
      {
        Contract.Requires(exp != null);

        var expStr = exp.ToString();
        foreach (var triple in this.inferred)
        {
          if (triple.Item2 == expStr)
          {
            outcome = triple.Item3;
            return true;
          }
        }
        outcome = ProofOutcome.Top;

        return false;
      }

      public void Add(BoxedExpression exp, ProofOutcome outcome)
      {
        this.inferred.Add(new Tuple<BoxedExpression, string, ProofOutcome>(exp, exp.ToString(), outcome));
      }

      public List<BoxedExpression> ComputeMinimal()
      {
        Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

        if (this.preconditions != null)
          return preconditions;

        var buckets = new Dictionary<Tuple<BinaryOperator, BoxedExpression>, List<Tuple<BoxedExpression, BoxedExpression>>>();
        var result = new List<BoxedExpression>(this.inferred.Count);

        // First we group expressions by upper bounds. 
        // If not lower bound is found, we just continue
        foreach (var triple in this.inferred)
        {
          BoxedExpression e1, e2;
          if (triple.Item1.IsCheckExp1LEQExp2(out e1, out e2))
          {
            Add(buckets, 
              new Tuple<BinaryOperator, BoxedExpression>(BinaryOperator.Cle, e2), 
              e1, triple.Item1);
          }
          else if (triple.Item1.IsCheckExp1LTExp2(out e1, out e2))
          {
            Add(buckets, 
              new Tuple<BinaryOperator, BoxedExpression>(BinaryOperator.Cle, e2), 
              e1, triple.Item1);
          }
          else
          {
            result.Add(triple.Item1);
          }
        }

        // Now we filter redundant expressions, according to their upper bound
        foreach (var pair in buckets)
        {
          Contract.Assume(pair.Value != null);
          Contract.Assume(pair.Value.Count  > 0);

          if (pair.Value.Count == 1)
          {          
            // no doubt, just emit it
            result.Add(pair.Value[0].Item2);
          }
          else
          {
            // the same variable is an upper bound for two or more expressions. Let's try to filter them
            int minIndex;
            if (TryGetSubsumingExpression(pair.Value, out minIndex))
            {
              result.Add(pair.Value[minIndex].Item2);
            }
            else
            {
              foreach (var innerPair in pair.Value)
              {
                result.Add(innerPair.Item2);
              }
            }
          }
        }

        return (this.preconditions = result);
      }


      #endregion

      #region Privates

      private void Add(
        Dictionary<Tuple<BinaryOperator, BoxedExpression>, List<Tuple<BoxedExpression, BoxedExpression>>> where,
        Tuple<BinaryOperator, BoxedExpression> key, BoxedExpression value, BoxedExpression precondition)
      {
        Contract.Requires(where != null);
        Contract.Requires(key != null);
        Contract.Requires(value != null);
        Contract.Requires(precondition != null);

        List<Tuple<BoxedExpression, BoxedExpression>> values;
        var pair = new Tuple<BoxedExpression, BoxedExpression>(value, precondition);
        if (where.TryGetValue(key, out values))
        {
          // use aliasing here to save one lookup
          values.Add(pair);
        }
        else
        {
          values = new List<Tuple<BoxedExpression, BoxedExpression>>() { pair };
          where[key] = values;
        }
      }

      private bool TryGetSubsumingExpression(List<Tuple<BoxedExpression, BoxedExpression>> list, out int minIndex)
      {
        Contract.Requires(list != null);
        Contract.Requires(list.Count > 1);
        Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out minIndex) >= 0);
        Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out minIndex) < list.Count);

        var max = list[0].Item1;
        minIndex = 0;
        for (var i = 1; i < list.Count; i++)
        {
          var curr = list[i].Item1;
          Contract.Assume(curr != null);
          var compare = SyntacticComparison(curr, max);

          switch (compare)
          {
            case SyntacticComparisonOutcome.LessEqual:
            case SyntacticComparisonOutcome.Equal:
              {
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

    #endregion


  }
}
