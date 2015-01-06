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
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains
{
  [ContractVerification(true)]
  public class ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>
    : ReducedCartesianAbstractDomain<NumericalAbstractDomain, ArrayAbstractDomain>,
    IAbstractDomainForEnvironments<Variable, Expression>
    where NumericalAbstractDomain : class, INumericalAbstractDomain<Variable, Expression>
    where ArrayAbstractDomain : class, IAbstractDomainForEnvironments<Variable, Expression>
  {
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.expManager != null);
    }

    #region Private state

    private readonly ExpressionManager<Variable, Expression> expManager;

    private readonly Optional additionalState;

    #endregion

    public ArrayStateAD(
      NumericalAbstractDomain numericalAbstractDomain, ArrayAbstractDomain arrayAbstractDomain,
       ExpressionManager<Variable, Expression> expManager)
      : base(numericalAbstractDomain, arrayAbstractDomain)
    {
      Contract.Requires(numericalAbstractDomain != null);
      Contract.Requires(arrayAbstractDomain != null);
      Contract.Requires(expManager != null);

      this.expManager = expManager;

      this.additionalState = new Optional();
    }

    public ArrayStateAD(
      NumericalAbstractDomain numericalAbstractDomain, ArrayAbstractDomain arrayAbstractDomain,
      object additionalState,
       ExpressionManager<Variable, Expression> expManager)
      : this(numericalAbstractDomain, arrayAbstractDomain, expManager)
    {
      Contract.Requires(arrayAbstractDomain != null);
      Contract.Requires(numericalAbstractDomain != null);
      Contract.Requires(expManager != null);
      Contract.Requires(!(additionalState is Optional));

      this.additionalState = new Optional(additionalState);
    }

    [System.Diagnostics.DebuggerHidden]
    public NumericalAbstractDomain Numerical
    {
      get
      {
        Contract.Ensures(Contract.Result<NumericalAbstractDomain>() != null);
        

        return this.Left;
      }
    }

    public ArrayAbstractDomain Array
    {
      get
      {
        Contract.Ensures(Contract.Result<ArrayAbstractDomain>() != null);

        return this.Right;
      }
    }

    public object OtherAbstractStates
    {
      get
      {
        object value;
        this.additionalState.HasValue(out value);

        return value;
      }
    }

    public ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> 
      MakeNewWithUpdatedArrayState(ArrayAbstractDomain newState)
    {
      Contract.Requires(newState != null);

      return new ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>
        (this.Numerical, newState, this.expManager);
    }


    #region Implementation of abstract methods

    public override ReducedCartesianAbstractDomain<NumericalAbstractDomain, ArrayAbstractDomain> Reduce(NumericalAbstractDomain left, ArrayAbstractDomain right)
    {
      return this.Factory(left, right);
    }

    protected override ReducedCartesianAbstractDomain<NumericalAbstractDomain, ArrayAbstractDomain> Factory(NumericalAbstractDomain left, ArrayAbstractDomain right)
    {
      Contract.Ensures(Contract.Result<ReducedCartesianAbstractDomain<NumericalAbstractDomain, ArrayAbstractDomain>>() != null);      

      return new ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>(left, right, this.expManager);
    }

    static public ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> For(
      NumericalAbstractDomain left, ArrayAbstractDomain right, 
       ExpressionManager<Variable, Expression> expManager)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Requires(expManager != null);

      Contract.Ensures(Contract.Result<ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>>() != null);
      
      return new ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>(left, right, expManager);
    }

    [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-!(other is Optional)", Justification="We do not track dynamic types")]
    static public ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> For<OtherAbstractDomain>(
      NumericalAbstractDomain left, ArrayAbstractDomain right, OtherAbstractDomain other,
      ExpressionManager<Variable, Expression> expManager)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Requires(other != null);
      Contract.Requires(expManager != null);

      Contract.Requires(!(other is Optional));

      Contract.Ensures(Contract.Result<ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>>() != null);      

      return new ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>(left, right, other, expManager);
    }

    #endregion

    #region IAbstractDomain Members

    bool IAbstractDomain.LessEqual(IAbstractDomain a)
    {
      return base.LessEqual(a);
    }

    public bool LessEqual(ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> a)
    {
      Contract.Requires(a  != null);

      return base.LessEqual(a);
    }

    new public ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> Bottom
    {
      get
      {
        return  base.Bottom as ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>;
      }
    }

    new public  ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> Top
    {
      get
      {
        return base.Top as ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>;
      }
    }

    IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
    {
      var other = a as ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>;

      Contract.Assume(other != null);

      return this.Join(other);
    }

    public ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> Join(ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> other)
    {
      Contract.Requires(other != null);

      return (ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>) base.Join(other);
    }

    IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
    {
      var other = a as ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>;

      Contract.Assume(other != null);

      return this.Meet(other);
    }

    public ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> Meet(ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> other)
    {
      Contract.Requires(other != null);

      return (ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>)base.Meet(other);
    }

    public override IAbstractDomain Widening(IAbstractDomain prev)
    {
      var other = prev as ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>;

      Contract.Assume(other != null);

      return this.Widening(other);
    }

    public ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> Widening(ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> other)
    {
      Contract.Requires(other != null);

      Contract.Ensures(Contract.Result<ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>>() != null);      

      var leftWiden = (NumericalAbstractDomain) this.Left.Widening(other.Left);
      var rightWiden = (ArrayAbstractDomain)this.Right.Widening(other.Right);

      var f =  Factory(leftWiden, rightWiden) as ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>;

      Contract.Assume(f != null);

      return f;
    }

    #endregion

    #region Duplicate

    public override object Clone()
    {
      return this.Duplicate();
    }

    public ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression> Duplicate()
    {
      Contract.Ensures(Contract.Result<ArrayStateAD<NumericalAbstractDomain, ArrayAbstractDomain, Variable, Expression>>() != null);
      
      var leftDup = (NumericalAbstractDomain) this.Left.Clone();
      var rightDup = (ArrayAbstractDomain)this.Right.Clone();

      object value;
      if (this.additionalState.HasValue(out value) /*&& value != null*/)
      {
        Contract.Assume(!(value is Optional));

        var dup = value;  // F: !!! Should call clone?

        return For(leftDup, rightDup, dup, this.expManager);
      }

      return For(leftDup, rightDup, this.expManager);
    }
    #endregion 

  
    #region IAbstractDomainForEnvironments<Variable,Expression> Members

    public string ToString(Expression exp)
    {
      return this.Left.ToString(exp);
    }

    #endregion

    #region ToString
    public override string ToString()
    {
      var baseStr = base.ToString();
      object value;
      if (this.additionalState.HasValue(out value) /*&& value != null*/)
      {
        baseStr += "\n" + value.ToString();
      }

      return baseStr.ToString();
    }
    #endregion

    #region IPureExpressionAssignments<Variable,Expression> Members

    public List<Variable> Variables
    {
      get { return this.Left.Variables.SetUnion(this.Right.Variables); }
    }

    public void AddVariable(Variable var)
    {
      this.Left.AddVariable(var);
      this.Right.AddVariable(var);
    }

    public void Assign(Expression x, Expression exp)
    {
      this.Left.Assign(x, exp);
      this.Right.Assign(x, exp);
    }

    public void ProjectVariable(Variable var)
    {
      this.Left.ProjectVariable(var);
      this.Right.ProjectVariable(var);
    }

    public void RemoveVariable(Variable var)
    {
      this.Left.RemoveVariable(var);
      this.Right.RemoveVariable(var);
    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {
      this.Left.RenameVariable(OldName, NewName);
      this.Right.RenameVariable(OldName, NewName);
    }

    #endregion

    #region IPureExpressionTest<Variable,Expression> Members

    public IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {
      var leftRes = (NumericalAbstractDomain)this.Left.TestTrue(guard);
      var rightRes = (ArrayAbstractDomain)this.Right.TestTrue(guard);

      return For(leftRes, rightRes, this.expManager);
    }

    public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      var leftRes = (NumericalAbstractDomain)this.Left.TestFalse(guard);
      var rightRes = (ArrayAbstractDomain)this.Right.TestFalse(guard);

      return For(leftRes, rightRes, this.expManager);
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      // TODO : right!!!
      return this.Left.CheckIfHolds(exp);
    }

    void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
    {
      this.AssumeDomainSpecificFact(fact);
    }

    #endregion

    #region IAssignInParallel<Variable,Expression> Members

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      this.Left.AssignInParallel(sourcesToTargets, convert);
      this.Right.AssignInParallel(sourcesToTargets, convert);
    }

    #endregion

    private struct Optional
    {
      readonly private object value;

      public Optional(object value)
      {
        this.value = value;
      }

      public bool HasValue(out object value)
      {
        Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
        value = this.value;
        return this.value != null;
      }

      public override string ToString()
      {
        if (this.value == null)
          return "";
        else
          return this.value.ToString();
      }
    }
  }
}
