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
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{

  /// <summary>
  /// Precondition inference by all path checking: Read the precondition in the prestate, and then makes sure it is checked on all the paths (ignoring tests) 
  /// </summary>
  public class PreconditionInferenceAllOverThePaths<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    : AggressivePreconditionInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    where Variable : IEquatable<Variable>
    where Expression : IEquatable<Expression>
    where Type : IEquatable<Type>
    where LogOptions : IFrameworkLogOptions
  {
    #region Invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(obligations != null);
    }

    #endregion

    #region State

    private readonly List<IProofObligations<Variable, BoxedExpression>> obligations;

    #endregion

    #region Constructor

    public PreconditionInferenceAllOverThePaths(
      List<IProofObligations<Variable, BoxedExpression>> obligations,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> MethodDriver)
      : base(MethodDriver)
    {
      Contract.Requires(obligations != null);
      Contract.Requires(MethodDriver != null);

      this.obligations = obligations;
    }

    #endregion

    #region Overridden

    public override bool TryInferConditions(ProofObligation obl, ICodeFixesManager codefixesManager, out InferredConditions preConditions)
    {
      // First we try to read exp in the prestate
      
      if (base.TryInferConditions(obl, codefixesManager, out preConditions))
      {
        // if we succeed, now we should prove it is a precondition according to Sect. 4 of [CCL-VMCAI11]
        var necessarypreConditions = preConditions.Where(pre => new PreconditionCheckedAllOverThePathsAnalysis(this.obligations, this.MethodDriver).CheckPreconditionAdmissibility(pre.Expr)).AsInferredPreconditions();
        obl.NotifySufficientYetNotNecessaryPreconditions(preConditions.Except(necessarypreConditions));

        preConditions = necessarypreConditions;
        return necessarypreConditions.Any();
      }

      return false;
    }
    
    #endregion

    #region PreconditionCheckedAllOverThePaths analysis

    struct AbstractValue
    {
      #region State

      public readonly Set<BoxedExpression> Expressions;

      public bool IsChecked { get { return this.Expressions == null; } }

      #endregion

      #region Constructors

      public AbstractValue(Set<BoxedExpression> expressions)
      {
        this.Expressions = expressions;
      }

      public AbstractValue(BoxedExpression expression)
      {
        if (expression == null)
        {
          this.Expressions = null;
        }
        else
        {
          this.Expressions = new Set<BoxedExpression>() { expression };
        }
      }

      #endregion

      #region Overridden

      public override bool Equals(object obj)
      {
        if (obj is AbstractValue)
        {
          var that = (AbstractValue)obj;

          if ((this.Expressions == null) && (that.Expressions == null))
          {
            return true;
          }

          return this.Expressions.Intersection(that.Expressions).Count == this.Expressions.Count;
        }

        return false;
      }

      public override int GetHashCode()
      {
        return this.Expressions == null ? 0 : this.Expressions.GetHashCode();
      }

      public override string ToString()
      {
        if (this.IsChecked)
        {
          return "Checked";
        }
        else
        {
          return Expressions.ToString();
        }
      }
      #endregion
    }

    class PreconditionCheckedAllOverThePathsAnalysis :
      MSILVisitor<APC, Local, Parameter, Method, Field, Type, Variable, Variable, AbstractValue, AbstractValue>,
      IAnalysis<APC,
                AbstractValue,
                IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, AbstractValue, AbstractValue>,
                IFunctionalMap<Variable, FList<Variable>>>
    {
      #region Object invariant

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.MethodDriver != null);
        Contract.Invariant(this.obligations != null);
      }

      #endregion

      #region State

      private readonly IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> MethodDriver;
      private IFixpointInfo<APC, AbstractValue> fixpointInfo;
      private readonly List<IProofObligations<Variable, BoxedExpression>> obligations;

      public bool FoundLoop { get; private set; }

      #endregion

      #region Constructor

      public PreconditionCheckedAllOverThePathsAnalysis(
        List<IProofObligations<Variable, BoxedExpression>> obligations,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> MethodDriver)
      {
        Contract.Requires(MethodDriver != null);
        Contract.Requires(obligations != null);

        this.MethodDriver = MethodDriver;
        this.obligations = obligations;
      }

      #endregion

      #region Analysis Entry point

      public bool CheckPreconditionAdmissibility(BoxedExpression candidate)
      {
        Contract.Requires(candidate != null);

        var closure = this.MethodDriver.HybridLayer.CreateForward(this, new DFAOptions() { Trace = MethodDriver.Options.TraceDFA });
        closure(new AbstractValue(candidate));

        AbstractValue exitValue;

        var result =  !FoundLoop   // No loop with an unchecked condition was found
          && 
          (!this.fixpointInfo.PreState(this.MethodDriver.Context.MethodContext.CFG.NormalExit, out exitValue) // The method exit is unreachable
            ||
            exitValue.IsChecked  // or, if it is reachable it is checked at the end
            );

        return result;
      }
      
      #endregion

      #region IAnalysis<APC,AbstractValue,IVisitMSIL<APC,Local,Parameter,Method,Field,Type,Variable,Variable,AbstractValue,AbstractValue>,IFunctionalMap<Variable,FList<Variable>>> Members

      public AbstractValue EdgeConversion(APC from, APC next, bool joinPoint, IFunctionalMap<Variable, FList<Variable>> edgeData, AbstractValue newState)
      {
        // Wrong, just for the moment
        return newState;
      }

      public AbstractValue Join(Pair<APC, APC> edge, AbstractValue newState, AbstractValue prevState, out bool weaker, bool widen)
      {
        if (widen)
        {
          // If we come back after a loop, it means that there is a path where the condition is not checked, so we are done
          this.FoundLoop = true;
          weaker = false; // stop the computation

          return prevState;
        }
        else
        {
          // we are at a join, and in both branches the precondition was not checked, so we continue

          weaker = true;

          if (newState.IsChecked)
          {
            return prevState;
          }

          if (prevState.IsChecked)
          {
            return newState;
          }

          var union = new Set<BoxedExpression>(newState.Expressions);
          union.AddRange(prevState.Expressions);
         
          return new AbstractValue(union);
        }
      }

      public AbstractValue MutableVersion(AbstractValue state)
      {
        return state;
      }

      public AbstractValue ImmutableVersion(AbstractValue state)
      {
        return state;
      }

      public void Dump(Pair<AbstractValue, System.IO.TextWriter> pair)
      {
        pair.Two.WriteLine(pair.One.ToString());
      }

      public bool IsBottom(APC pc, AbstractValue state)
      {
        return state.IsChecked;
      }

      public bool IsTop(APC pc, AbstractValue state)
      {
        return !state.IsChecked;
      }

      public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, AbstractValue, AbstractValue> Visitor()
      {
        return this;
      }

      public Predicate<APC> CacheStates(IFixpointInfo<APC, AbstractValue> fixpointInfo)
      {
        this.fixpointInfo = fixpointInfo;

        return (APC pc) => false;
      }

      #endregion

      #region Transfer function

      protected override AbstractValue Default(APC pc, AbstractValue data)
      {
        if (data.IsChecked)
        {
          if (this.MethodDriver.Options.TraceDFA)
          {
            Console.WriteLine("Condition checked: Path killed!");
          }

          return data;
        }

        foreach (var exp in data.Expressions)
        {
          if (this.IsExpressionCheckedAt(pc, exp))
          {
            if (this.MethodDriver.Options.TraceDFA)
            {
              Console.WriteLine("Condition checked: Path killed!");
            }

            return new AbstractValue(); // ==> IsChecked == true
          }
        }

        return data;
      }

      #endregion

      #region Private, helper methods

      // F: not the most efficient way of doing it - should replace with a lookup table
      private bool IsExpressionCheckedAt(APC pc, BoxedExpression exp)
      {
        Contract.Requires(exp != null);

        var obligationsAtPC = new List<ProofObligationBase<BoxedExpression, Variable>>();
        foreach (var obl in obligations)
        {
          if (obl.PCWithProofObligations(pc, obligationsAtPC))
          {
            foreach (var particularObl in obligationsAtPC)
            {
              if (exp.Equals(particularObl.Condition))
              {
                return true;
              }
            }
          }
        }

        return false;
      }

      #endregion

      #region ToString

      public override string ToString()
      {
        return "Precondition inference all over the paths";
      }

      #endregion
    }
  }
    #endregion
}