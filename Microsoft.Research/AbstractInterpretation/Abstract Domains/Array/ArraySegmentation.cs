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
using System.Text;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

[assembly: ContractVerification(false)] // Temp, should go in some Assembly.cs

namespace Microsoft.Research.AbstractDomains
{
  public static class ArrayOptions
  {
    [ThreadStatic]
    static public bool Trace;
  }

  [ContractVerification(true)]
  [SuppressMessage("Microsoft.Contracts", "InvariantInMethod-!this.IsNormal || !this.limits.GetLastElement().IsEmpty")]
  public class ArraySegmentation<AbstractDomain, Variable, Expression>
    : IAbstractDomainForEnvironments<Variable, Expression>
    where AbstractDomain : class, IAbstractDomainForArraySegmentationAbstraction<AbstractDomain, Variable>
  {
    #region Object Identity

    private static int NextId = 0;
    private static int UniformCount = 0;
    
    #endregion

    #region State

    private enum State { BOTTOM, NORMAL, TOP }

    readonly private ExpressionManager<Variable, Expression> expManager;

    readonly private ArraySegmentationTestTrueVisitor testTrue;
    readonly private ArraySegmentationTestFalseVisitor testFalse;

    readonly private NonNullList<SegmentLimit<Variable>> limits;
    readonly private NonNullList<AbstractDomain> elements;                       // F: We assume non-relational domains for the moment

    readonly private int Id = NextId++;

    readonly private AbstractDomain bottomElement;

    private State state;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.limits != null);
      Contract.Invariant(this.elements != null);
      Contract.Invariant(this.expManager != null);
      Contract.Invariant(this.testTrue != null);
      Contract.Invariant(this.testFalse != null);
      Contract.Invariant(this.bottomElement != null);
      Contract.Invariant(!this.IsNormal || this.elements.Count +1 == this.limits.Count);
      Contract.Invariant(!this.IsNormal || !this.limits.GetLastElement().IsEmpty); // We only check it at runtime
    }

    #endregion

    #region Constructor

    public ArraySegmentation(
      SegmentLimit<Variable> lowerBounds, AbstractDomain value, SegmentLimit<Variable> upperBounds,
      AbstractDomain bottomElement,
       ExpressionManager<Variable, Expression> expManager)
      : this(bottomElement, expManager)
    {
      Contract.Requires(lowerBounds != null);
      Contract.Requires(value != null);
      Contract.Requires(upperBounds != null);
      Contract.Requires(bottomElement != null);
      Contract.Requires(expManager != null);

      this.limits = new NonNullList<SegmentLimit<Variable>>() { lowerBounds, upperBounds };
      this.elements = new NonNullList<AbstractDomain>() { value };
      this.state = State.NORMAL;

      this.testTrue = new ArraySegmentation<AbstractDomain, Variable, Expression>.ArraySegmentationTestTrueVisitor(this.expManager.Decoder);
      this.testFalse = new ArraySegmentation<AbstractDomain, Variable, Expression>.ArraySegmentationTestFalseVisitor(this.expManager.Decoder);

      this.testTrue.FalseVisitor = this.testFalse;
      this.testFalse.TrueVisitor = this.testTrue;
    }
     
    public ArraySegmentation(NonNullList<SegmentLimit<Variable>> limits, NonNullList<AbstractDomain> elements, 
      AbstractDomain bottomElement, ExpressionManager<Variable, Expression> expManager)
      : this(true, limits, elements, bottomElement, expManager)
    {
      Contract.Requires(limits != null);
      Contract.Requires(elements != null);
      Contract.Requires(bottomElement != null);
      Contract.Requires(expManager != null);
      Contract.Requires(limits.Count == elements.Count + 1);

      // F: those are needed because of a weakness of the equality
      Contract.Ensures(this.limits.Count <= limits.Count);
      Contract.Ensures(this.elements.Count <= elements.Count);

    }
     
    private ArraySegmentation(AbstractDomain bottomElement, ExpressionManager<Variable, Expression> expManager)
    {
      Contract.Requires(bottomElement != null);
      Contract.Requires(expManager!= null);

      this.expManager = expManager;

      this.limits = new NonNullList<SegmentLimit<Variable>>();
      this.elements = new NonNullList<AbstractDomain>();

      this.testTrue = new ArraySegmentation<AbstractDomain, Variable, Expression>.ArraySegmentationTestTrueVisitor(this.expManager.Decoder);
      this.testFalse = new ArraySegmentation<AbstractDomain, Variable, Expression>.ArraySegmentationTestFalseVisitor(this.expManager.Decoder);

      this.testTrue.FalseVisitor = this.testFalse;
      this.testFalse.TrueVisitor = this.testTrue;

      this.bottomElement = bottomElement;

      this.state = State.TOP;
    }

    private ArraySegmentation(bool forceSimplification, NonNullList<SegmentLimit<Variable>> limits, NonNullList<AbstractDomain> elements,
      AbstractDomain bottomElement, ExpressionManager<Variable, Expression> expManager)
      : this(bottomElement, expManager)
    {
      Contract.Requires(limits != null);
      Contract.Requires(elements != null);
      Contract.Requires(bottomElement != null);
      Contract.Requires(expManager!= null);
      Contract.Requires(limits.Count == elements.Count + 1);

      // F: those are needed because of a weakness of the equality
      Contract.Ensures(this.limits.Count <= limits.Count);
      Contract.Ensures(this.elements.Count <= elements.Count);

      if (forceSimplification)
      {
        bool isBottom;
        RemoveTriviallyEmptySegments(limits, elements, out this.limits, out this.elements, out isBottom);

        this.state = isBottom ? State.BOTTOM : State.NORMAL;
      }
      else
      {
        this.limits = limits;
        this.elements = elements;
        this.state = State.NORMAL;
      }
      this.testTrue = new ArraySegmentation<AbstractDomain, Variable, Expression>.ArraySegmentationTestTrueVisitor(this.expManager.Decoder);
      this.testFalse = new ArraySegmentation<AbstractDomain, Variable, Expression>.ArraySegmentationTestFalseVisitor(this.expManager.Decoder);

      this.testTrue.FalseVisitor = this.testFalse;
      this.testFalse.TrueVisitor = this.testTrue;
    }

    /// <param name="emptySegment">If true, constructs the empty segment, if false the bottom one</param>
    private ArraySegmentation(bool emptySegment, 
      AbstractDomain bottomElement,  ExpressionManager<Variable, Expression> expManager)
      : this(bottomElement, expManager)
    {
      Contract.Requires(bottomElement != null);
      Contract.Requires(expManager != null);

      var lowerBound = new SegmentLimit<Variable>(NormalizedExpression<Variable>.For(0), false);
      var upperBound = new SegmentLimit<Variable>(NormalizedExpression<Variable>.For(0), emptySegment);

      this.limits = new NonNullList<SegmentLimit<Variable>>() { lowerBound, upperBound };
      this.elements = new NonNullList<AbstractDomain>() {  };
      this.state = emptySegment? State.NORMAL: State.BOTTOM;

      this.testTrue = new ArraySegmentation<AbstractDomain, Variable, Expression>.ArraySegmentationTestTrueVisitor(this.expManager.Decoder);
      this.testFalse = new ArraySegmentation<AbstractDomain, Variable, Expression>.ArraySegmentationTestFalseVisitor(this.expManager.Decoder);

      this.testTrue.FalseVisitor = this.testFalse;
      this.testFalse.TrueVisitor = this.testTrue;
    }
 
    private ArraySegmentation(ArraySegmentation<AbstractDomain, Variable, Expression> other)
    {
      Contract.Requires(other != null);

      Contract.Ensures(this.expManager == other.expManager);
      Contract.Ensures(this.testTrue == other.testTrue);
      Contract.Ensures(this.testFalse == other.testFalse);
      Contract.Ensures(this.state == other.state);

      Contract.Ensures(this.limits != null);
      Contract.Ensures(this.elements != null);
      Contract.Ensures(this.limits.Count == other.limits.Count);
      Contract.Ensures(this.elements.Count == other.elements.Count);

      Contract.Assume(other.limits != null);  // F: we are not assuming the object invariant
      Contract.Assume(other.elements != null);

      this.limits = new NonNullList<SegmentLimit<Variable>>(other.limits);
      this.elements = new NonNullList<AbstractDomain>(other.elements);
  
      Contract.Assert(this.limits.Count == other.limits.Count);
      Contract.Assert(this.elements.Count == other.elements.Count);

      // We assume the other object invariant
      Contract.Assume(other.expManager!= null);

      Contract.Assume(other.testTrue != null);
      Contract.Assume(other.testFalse != null);

      Contract.Assume(Contract.ForAll(this.limits, l => l != null)); 

      this.expManager = other.expManager;

      this.testTrue = other.testTrue;
      this.testFalse = other.testFalse;

      Contract.Assume(other.IsTop || other.elements.Count > 0);
      this.state = other.state;

      Contract.Assume(other.bottomElement != null);

      this.bottomElement = other.bottomElement;
    }

    #endregion

    #region IAbstractDomainForEnvironments<Variable,Expression> Members

    public string ToString(Expression exp)
    {
      return ExpressionPrinter.ToString(exp, this.expManager.Decoder);
    }

    #endregion

    #region IAbstractDomain Members

    public bool IsBottom
    {
      get {
        Contract.Ensures(Contract.Result<bool>() == (this.state == State.BOTTOM));
        return this.state == State.BOTTOM; }
    }

     
    public bool IsTop
    {
      get {
        Contract.Ensures(Contract.Result<bool>() == (this.state == State.TOP)); 
 
        return this.state == State.TOP; 
      }
    }

    public bool IsEmptyArray
    {
      get
      {
        Contract.Ensures(!Contract.Result<bool>() || (this.state == State.NORMAL));

        if (this.IsTop || this.IsBottom)
        {
          return false;
        }

        if (this.elements.Count == 0)
        {
          this.state = State.NORMAL;
          return true;
        }

        if (this.limits.Count > 1)
        {
          Contract.Assert(!this.limits.IsEmpty);
          // If there is a common variable in the first and last set of bounds
          if (!this.limits[0].Intersection(this.limits.GetLastElement()).IsEmpty)
          {
            if (this.limits.GetLastElement().IsConditional)
            {
              this.state = State.NORMAL;
              return true;
            }
            else
            {
              this.state = State.BOTTOM;
              return false;
            }
          }
        }

        return false;
      }
    }

    public bool IsNormal
    {
      [SuppressMessage("Microsoft.Contracts", "EnsuresInMethod-!Contract.Result<bool>() || this.limits.Count >= 2")]
      [SuppressMessage("Microsoft.Contracts", "EnsuresInMethod-!Contract.Result<bool>() || this.elements.Count +1 == this.limits.Count")]
      get
      {
        Contract.Ensures(!Contract.Result<bool>() || this.limits.Count >= 2);
        Contract.Ensures(!Contract.Result<bool>() || this.elements.Count +1 == this.limits.Count );

        return !(this.IsEmptyArray || this.IsBottom || this.IsTop);
      }
    }

    bool IAbstractDomain.LessEqual(IAbstractDomain a)
    {
      var other = a as ArraySegmentation<AbstractDomain, Variable, Expression>;

      Contract.Assume(other != null);

      return this.LessEqual(other);
    }
     
    public bool LessEqual(ArraySegmentation<AbstractDomain, Variable, Expression> other)
    {
      Contract.Requires(other != null);

      bool result;
      if (AbstractDomainsHelper.TryTrivialLessEqual(this, other, out result))
      {
        return result;
      }

      if (this.IsEmptyArray)
      {
        return other.IsEmptyArray;
      }

      // F: Warnings for those are from the handling of constrained calls
     // Contract.Assert(!this.IsTop);
     // Contract.Assert(!other.IsTop);

      Contract.Assume(this.elements.Count > 0);

      Contract.Assume(other.limits != null);
      Contract.Assume(other.elements != null);
      Contract.Assume(other.elements.Count > 0);

      Contract.Assume(this.limits.Count == this.elements.Count + 1);
      Contract.Assume(other.limits.Count == other.elements.Count + 1);

      ArraySegmentation<AbstractDomain, Variable, Expression> leftReduced, rightReduced;
      bool removedAtLeastOneLimitOnRight;

      if (TryUnifySegments(SegmentUnificationOperation.LessEqual, this.DuplicateMe(), other.DuplicateMe(),
        (AbstractDomain)this.elements[0].Bottom, (AbstractDomain)other.elements[0].Top,
        out leftReduced, out rightReduced, out removedAtLeastOneLimitOnRight))
      {

        Contract.Assert(leftReduced.elements.Count == rightReduced.elements.Count);
        Contract.Assert(leftReduced.limits.Count == rightReduced.limits.Count);

        for (int i = 0; i < leftReduced.limits.Count; i++)
        {
          if (leftReduced.limits[i].IsConditional && !rightReduced.limits[i].IsConditional)
          {
            return false;
          }
        }

        for (int i = 0; i < leftReduced.elements.Count; i++)
        {
          var tmpElements = rightReduced.elements[i];

          Contract.Assert(tmpElements != null);

          if (!leftReduced.elements[i].LessEqual(tmpElements))
            return false;
        }

        return !removedAtLeastOneLimitOnRight;
      }
      else
      {
        return false;
      }
    }



    IAbstractDomain IAbstractDomain.Bottom
    {
      get { return this.Bottom; }
    }

    public ArraySegmentation<AbstractDomain, Variable, Expression> Bottom
    {
      get
      {
        Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);        

        var tmp = new ArraySegmentation<AbstractDomain, Variable, Expression>(this.bottomElement, this.expManager);
        tmp.state = State.BOTTOM;

        return tmp;
      }
    }

    IAbstractDomain IAbstractDomain.Top
    {
      get { return this.Top; }
    }

     
    public ArraySegmentation<AbstractDomain, Variable, Expression> Top
    {
      get
      {
        Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);
        
        return new ArraySegmentation<AbstractDomain, Variable, Expression>(this.bottomElement, this.expManager);
      }
    }

    public ArraySegmentation<AbstractDomain, Variable, Expression> EmptyArray
    {
      get
      {
        Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);

        return new ArraySegmentation<AbstractDomain, Variable, Expression>(true, this.bottomElement, this.expManager);
      }
    }

    IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
    {
      var other = a as ArraySegmentation<AbstractDomain, Variable, Expression>;

      Contract.Assume(other != null);

      return this.Join(other);
    }

     
    public ArraySegmentation<AbstractDomain, Variable, Expression> Join(ArraySegmentation<AbstractDomain, Variable, Expression> other)
    {
      Contract.Requires(other != null);

      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);
      
      ArraySegmentation<AbstractDomain, Variable, Expression> left, right, trivialResult;

      if (ArrayOptions.Trace)
      {
        Console.WriteLine("Join of:\n{0}\n{1}", this.ToString(), other.ToString());
      }

      if (AbstractDomainsHelper.TryTrivialJoinRefinedWithEmptyArrays(this, other, out trivialResult))
      {
        if (ArrayOptions.Trace)
        {
          Console.WriteLine("Result {0}", trivialResult.ToString());
        }

        return trivialResult;
      }


      Contract.Assume(other.elements != null);
      Contract.Assume(other.limits != null);

      Contract.Assume(this.elements.Count > 0);
      Contract.Assume(other.elements.Count > 0);

      Contract.Assume(this.limits.Count == this.elements.Count +1);
      Contract.Assume(other.limits.Count == other.elements.Count +1);

      // Different upper bounds
      if (this.limits[this.limits.Count - 1].Join(other.limits[other.limits.Count - 1]).IsEmpty)
      {
        return this.Top;
      }

      var bott = (AbstractDomain)this.elements[0].Bottom;

      var dup_this = this.DuplicateMe();
      var dup_other = other.DuplicateMe();
      bool dummy;
      if (TryUnifySegments(SegmentUnificationOperation.Join, dup_this, dup_other, bott, bott, out left, out right, out dummy))
      {

        Contract.Assert(left.limits.Count > 0);
        Contract.Assert(right.limits.Count > 0);
        Contract.Assert(left.limits.Count == right.limits.Count);
        Contract.Assert(left.elements.Count == right.elements.Count);
        Contract.Assert(left.limits.Count == left.elements.Count + 1);

        var resultLimits = new NonNullList<SegmentLimit<Variable>>();
        var resultElements = new NonNullList<AbstractDomain>();

        var last = left.limits.Count - 1;

        for (var i = 0; i < last; i++)
        {
          var segment = left.limits[i].Join(right.limits[i]);
          var l = left.elements[i];
          var r = right.elements[i];
          var element = (AbstractDomain)l.Join(r);
          
          resultLimits.Add(segment);
          resultElements.Add(element);
        }

        resultLimits.Add(left.limits[last].Join(right.limits[last]));

        Contract.Assert(resultLimits.Count == resultElements.Count + 1);

        var result = new ArraySegmentation<AbstractDomain, Variable, Expression>(resultLimits, resultElements, this.bottomElement, this.expManager);

        if (ArrayOptions.Trace)
        {
          Console.WriteLine("Join result:\n{0}:", result.ToString());
        }

        return result;
      }
      else
      {
        return this.Top;
      }
    }
     
    IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
    {
      var other = a as ArraySegmentation<AbstractDomain, Variable, Expression>;

      Contract.Assume(other != null);

      return this.Meet(other);
    }

    [Pure] 
    public ArraySegmentation<AbstractDomain, Variable, Expression> Meet(ArraySegmentation<AbstractDomain, Variable, Expression> other)
    {
      Contract.Requires(other != null);

      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);
      
      ArraySegmentation<AbstractDomain, Variable, Expression> left, right, trivialResult;

      if (ArrayOptions.Trace)
      {
        Console.WriteLine("Meet of:\n{0}\n{1}", this.ToString(), other.ToString());
      }

      if (AbstractDomainsHelper.TryTrivialMeet(this, other, out trivialResult))
      {
        if (ArrayOptions.Trace)
        {
          Console.WriteLine("Result {0}", trivialResult.ToString());
        }

        return trivialResult;
      }

      if (this.IsEmptyArray)        
      {
        if (other.IsEmptyArray)
        {
          return this;
        }
        return this.Bottom;
      }
      else if (other.IsEmptyArray)
      {
        return this.Bottom;
      }

      if (this.IsIncludedIn(other))
      {
        return this;
      }
      if (other.IsIncludedIn(this))
      {
        return other;
      }

     // Contract.Assert(!this.IsTop);
     // Contract.Assert(!other.IsTop);

      // F: those assumptions should go away once we will have the assertion above proven and the right object invariant
      Contract.Assume(this.elements.Count > 0);
      Contract.Assume(this.limits.Count == this.elements.Count +1);

      Contract.Assert(other.elements != null);
      Contract.Assert(other.limits != null);

      Contract.Assume(other.elements.Count > 0);
      Contract.Assume(other.limits.Count == other.elements.Count + 1);

      var bott = (AbstractDomain)this.elements[0].Bottom;

      var dup_this = this.DuplicateMe();
      var dup_other = other.DuplicateMe();

      Contract.Assert(dup_this.limits.Count > 0);

      // If different upper bounds, then merge them together (we know they should be the same)
      var upperBounds = dup_this.limits[dup_this.limits.Count - 1].Join(dup_other.limits[dup_other.limits.Count - 1]);
      if (upperBounds.IsEmpty)
      {
        var union = dup_this.limits[dup_this.limits.Count - 1].Meet(dup_other.limits[dup_other.limits.Count - 1]);

        dup_this.limits[dup_this.limits.Count - 1] = union;
        dup_other.limits[dup_other.limits.Count - 1] = union;
      }

      bool dummy;
      if (TryUnifySegments(SegmentUnificationOperation.Meet, dup_this, dup_other, bott, bott, out left, out right, out dummy))
      {

        Contract.Assert(left.limits.Count == right.limits.Count);
        Contract.Assert(left.elements.Count == right.elements.Count);
        Contract.Assert(left.limits.Count == left.elements.Count +1);

        var resultLimits = new NonNullList<SegmentLimit<Variable>>();
        var resultElements = new NonNullList<AbstractDomain>();

        var last = left.limits.Count - 1;

        for (var i = 0; i < last; i++)
        {
          var segment = left.limits[i].Meet(right.limits[i]);
          var element = (AbstractDomain)left.elements[i].Meet(right.elements[i]);

          // refinement
          if (i < last - 1)
          {
            IAbstractDomain oldValueLeft = null;
            if (this.TryMeetAllValuesInTheLimits(left.limits[i], left.limits[i + 1], TopNumericalDomain<Variable, Expression>.Singleton, ref oldValueLeft)
              && !oldValueLeft.IsTop)
            {
              element = (AbstractDomain)element.Meet(oldValueLeft);
            }
            IAbstractDomain oldValueRight = null;
            if (other.TryMeetAllValuesInTheLimits(right.limits[i], right.limits[i + 1], TopNumericalDomain<Variable, Expression>.Singleton, ref oldValueRight)
              && !oldValueRight.IsTop)
            {
              element = (AbstractDomain)element.Meet(oldValueRight);
            }
          }

          resultLimits.Add(segment);
          resultElements.Add(element);
        }

        resultLimits.Add(left.limits[last].Join(right.limits[last]));

        Contract.Assert(resultLimits.Count == resultElements.Count + 1);

        var result = new ArraySegmentation<AbstractDomain, Variable, Expression>(resultLimits, resultElements, this.bottomElement, this.expManager);

        if (ArrayOptions.Trace)
        {
          Console.WriteLine("Meet result:\n{0}:", result.ToString());
        }

        return result;
      }
      else
      {
        if (ArrayOptions.Trace)
        {
          Console.WriteLine("Unification failed. Meet returns top");
        }

        return this.Top;
      }
    }

     
    IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
    {
      var other = prev as ArraySegmentation<AbstractDomain, Variable, Expression>;

      Contract.Assume(other != null);

      return this.Widening(other);
    }

     
    [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-other != null")]
    public ArraySegmentation<AbstractDomain, Variable, Expression> Widening(ArraySegmentation<AbstractDomain, Variable, Expression> other)
    {
      Contract.Requires(other != null);

      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);      

      ArraySegmentation<AbstractDomain, Variable, Expression> left, right;

      if (this.IsTop)
      {
        return this;
      }

      if (this.IsEmptyArray)
      {
        if (other.IsEmptyArray)
        {
          return this;
        }

        return this.Top;
      }

      Contract.Assert(!this.IsTop);

      Contract.Assume(this.elements.Count > 0);
      Contract.Assume(this.limits.Count == this.elements.Count + 1);
      
      Contract.Assume(other.elements != null);
      Contract.Assume(other.limits != null);

      Contract.Assume(other.elements.Count > 0);
      Contract.Assume(other.limits.Count == other.elements.Count + 1);

      var bott = (AbstractDomain)this.elements[0].Bottom;
      bool dummy;
      if (TryUnifySegments(SegmentUnificationOperation.Widening, this, other, bott, bott, out left, out right, out dummy))
      {
        Contract.Assert(left.limits.Count == right.limits.Count);
        Contract.Assert(left.elements.Count == right.elements.Count);
        Contract.Assert(left.limits.Count == left.elements.Count + 1);

        var resultLimits = new NonNullList<SegmentLimit<Variable>>();
        var resultElements = new NonNullList<AbstractDomain>();

        var last = left.limits.Count - 1;

        for (var i = 0; i < last; i++)
        {
          var segment = left.limits[i].Widening(right.limits[i]);
          var element = (AbstractDomain)left.elements[i].Widening(right.elements[i]);

          resultLimits.Add(segment);
          resultElements.Add(element);
        }

        resultLimits.Add(left.limits[last].Widening(right.limits[last]));

        Contract.Assert(resultLimits.Count == resultElements.Count + 1);

        return new ArraySegmentation<AbstractDomain, Variable, Expression>(resultLimits, resultElements, this.bottomElement, this.expManager);
      }
      else
      {
        return this.Top;
      }
    }

    #endregion

    #region ICloneable Members

    public object Clone()
    {
      return DuplicateMe();
    }

    [Pure]
    public ArraySegmentation<AbstractDomain, Variable, Expression> DuplicateMe()
    {
      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);

      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>().state == this.state);
      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>().limits.Count == this.limits.Count);
      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>().elements.Count == this.elements.Count);

      // F: TODO - one day have a better infernce for the fields assigned in the constructor
      return new ArraySegmentation<AbstractDomain, Variable, Expression>(this);
    }

    #endregion

    #region IPureExpressionAssignments<Variable,Expression> Members

    [Pure]
    public List<Variable> Variables
    {
      get 
      {
        var result = new List<Variable>();
        if (this.IsBottom || this.IsTop)
          return result;

        foreach (var limit in this.limits)
        {
          Contract.Assume(limit != null);

          foreach (var exp in limit)
          {
            Contract.Assume(exp != null);
            int dummy;
            Variable v;
            if (exp.IsVariable(out v) || exp.IsAddition(out v, out dummy))
            {
              result.Add(v);
            }
          }
        }

        return result;
      }
    }

    public void AddVariable(Variable var)
    {
      // do nothing
    }

    public void Assign(Expression x, Expression exp)
    {

    }

    public void ProjectVariable(Variable var)
    {

    }

    public void RemoveVariable(Variable var)
    {

    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {

    }

    public void AssumeDomainSpecificFact(DomainSpecificFact fact)
    {

    }

    #endregion

    #region IPureExpressionTest<Variable,Expression> Members

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestTrue(Expression guard)
    {
      return this.TestTrue(guard);
    }

    public ArraySegmentation<AbstractDomain, Variable, Expression> TestTrue(Expression guard)
    {
      Contract.Requires(guard != null);
      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);

      return this.testTrue.Visit(guard, this);
    }

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestFalse(Expression guard)
    {
      return this.TestFalse(guard);
    }

    public ArraySegmentation<AbstractDomain, Variable, Expression> TestFalse(Expression guard)
    {
      Contract.Requires(guard != null);
      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);

      return this.testFalse.Visit(guard, this);
    }

     
    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() == CheckOutcome.Top);

      return CheckOutcome.Top;
    }

    #endregion

    #region IAssignInParallel<Variable,Expression> Members

    [SuppressMessage("Microsoft.Contracts", "Assert-1-0")]
    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      // It should not be called
      Contract.Assert(false);
    }

    
    public bool TryAssignInParallelFunctional(
      Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert, 
      out ArraySegmentation<AbstractDomain, Variable, Expression> result)
    {
      #region Contracts
      Contract.Requires(sourcesToTargets != null);
      Contract.Requires(convert != null);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);

      #endregion
      
      if (this.IsEmptyArray || this.IsBottom || this.IsTop)
      {
        result = this;
        return true;
      }

      Contract.Assume(this.elements.Count > 0);
      Contract.Assume(this.limits.Count == this.elements.Count + 1);

      var newLimits = new NonNullList<SegmentLimit<Variable>>();
      var newElements = new NonNullList<AbstractDomain>();

      for (var i = 0; i < this.limits.Count - 1; i++)
      {
        var currLimits = this.limits[i];

        foreach (var x in this.limits[i + 1])
        {
          if (x != null)
          {
            Variable v;
            int k;
            if (x.IsAddition(out v, out k) && k == -1 && currLimits.Contains(NormalizedExpression<Variable>.For(v)))
            {
              currLimits = currLimits.Add(NormalizedExpression<Variable>.For(v));
            }
          }
        }

        var newLimit = currLimits.AssignInParallel(sourcesToTargets, convert, this.expManager.Decoder);

        if (!newLimit.IsEmpty)
        {
          newLimits.Add(newLimit);
          newElements.Add(this.elements[i]);        // F TODO: This should be changed when we'll go relational
        }
        else
        {  //  We are abstracting this segment limit, so we have to join the element values

          Contract.Assume(newElements.Count > 0);   // The reason why that holds is because the first segment *always* contains 0, therefore we always have at least one element

          var last = newElements.Count - 1;
          newElements[last] = (AbstractDomain)newElements[last].Join(this.elements[i]);
        }
      }

      Contract.Assert(newLimits.Count > 0);   // It holds because the first segment always contains 0 
      Contract.Assume(!newLimits[0].IsBottom);

      var lastLimit = this.limits[this.limits.Count - 1].AssignInParallel(sourcesToTargets, convert, this.expManager.Decoder);

      if (lastLimit.IsEmpty)
      {
        // We reach this point when, for some reason, we cannot get a name for the length of the array
        result = default(ArraySegmentation<AbstractDomain, Variable, Expression>);
        return false;
      }

      newLimits.Add(lastLimit);

      Contract.Assert(newLimits.Count == newElements.Count +1); // the List.Add contract is not strong enough

      var renamedNewElements = newElements.ConvertAll(x => x.Rename(sourcesToTargets));

      result = new ArraySegmentation<AbstractDomain, Variable, Expression>(newLimits, renamedNewElements, this.bottomElement, this.expManager);
      return true;
    }

    #endregion

    #region Helper checking functions

    static public FlatAbstractDomain<bool> IsEqual(NormalizedExpression<Variable> exp, SegmentLimit<Variable> segment, INumericalAbstractDomainQuery<Variable, Expression> oracle)
    {
      Contract.Requires(segment != null);

      // 1. Try in the Segment
      if (segment.Contains(exp))
        return CheckOutcome.True;

      // 2. Try in the ArraySegmentation (TODO)
      return CheckOutcome.Top;
    }

    #endregion

    #region GetAbstractValue
     
    public bool TryGetAbstractValue(NormalizedExpression<Variable> index, 
      INumericalAbstractDomainQuery<Variable, Expression> oracle, out IAbstractDomain result)
    {
      #region Contracts

      Contract.Requires(index != null);
      Contract.Requires(oracle != null);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);
      
      #endregion

      var info = new SearchInfo();

      var bounds = oracle.BoundsFor(ToExp(index)).AsInterval;

      if (bounds.IsBottom)
      {
        result = Interval.UnreachedInterval;

        return true;
      }

      IAbstractDomain result1 = null, result2 = null;

      int low, upp;
      if (bounds.IsFiniteAndInt32(out low, out upp) && TrySearchBounds(ToNormExp(low), ToNormExp(upp+1), oracle, ref info))
      {
        Contract.Assert(info.Low >= 0);
        result1 = JoinValuesInASubrange(info.Low, info.UppMerge);
      }

      if (TrySearchBounds(index, index, oracle, ref info))
      {
        Contract.Assert(info.Low >= 0);
        result2 = JoinValuesInASubrange(info.Low, info.UppMerge);
      }

      if (result1 != null)
      {
        if (result2 != null)
        {
          result = result1.Meet(result2);
          return true;
        }

        result = result1;
        return true;
      }
      else if (result2 != null)
      {
        result = result2;
        return true;
      }
      else
      {
        result = default(IAbstractDomain);
        return false;
      }
    }

    public bool TryGetAbstractValue(NormalizedExpression<Variable> lower, NormalizedExpression<Variable> upper,
      INumericalAbstractDomain<Variable, Expression> oracle, out IAbstractDomain result)
    {
      #region Contracts
      Contract.Requires(lower != null);
      Contract.Requires(upper != null);
      Contract.Requires(oracle != null);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result)!= null);
      #endregion

      var si = default(SearchInfo);
      if(this.TrySearchBounds(lower, upper, oracle, ref si))
      {
        result = JoinValuesInASubrange(si.Low, si.UppMerge, this.bottomElement);
        return true;
      }

      result = default(IAbstractDomain);
      return false;
    }

    public bool TryMeetAllValuesInTheLimits(SegmentLimit<Variable> lower, SegmentLimit<Variable> upper,
      INumericalAbstractDomain<Variable, Expression> oracle, ref IAbstractDomain result)
    {
      Contract.Requires(lower != null);
      Contract.Requires(upper != null);
      Contract.Requires(oracle != null);
      Contract.Requires(result == null);

      Contract.Ensures(!Contract.Result<bool>() || result != null);

      foreach (var low in lower)
      {
        if (low == null)
          continue;

        foreach (var upp in upper)
        {
          if (upp == null)
            continue;

          IAbstractDomain tmpValue;
          if (TryGetAbstractValue(low, upp, oracle, out tmpValue))
          {
            result = result != null? result.Meet(tmpValue) : tmpValue;
          }
          else
          {
            result = null;
            return false;
          }
        }
      }

      return result != null;
    }

    #endregion

    #region AssumeAbstractValue
    
    /// <summary>
    /// The segmentation is update only if the abstract value does not overwrite some information we already had.
    /// Differently stated, this methos succeeds only if it guarantees a refinement of the segmentation
    /// </summary>
    public bool TryAssumeAbstractValue(
      NormalizedExpression<Variable> index, 
      AbstractDomain value,
      INumericalAbstractDomainQuery<Variable, Expression> oracle,
      out ArraySegmentation<AbstractDomain, Variable, Expression> result)
    {
      #region Contracts

      Contract.Requires(index != null);
      Contract.Requires(value != null);
      Contract.Requires(oracle != null);

      #endregion

      Contract.Assume(this.limits.Count == this.elements.Count + 1);

      var info = new SearchInfo();
      if (this.IsNormal && TrySearchBoundsForAddingASegment(index, oracle, ref info))
      {
        return TrySetAbstractValueInternal(false, index, value, oracle, info, out result);
      }

      result = default(ArraySegmentation<AbstractDomain, Variable, Expression>);
      return false;
    }

    private bool TrySetAbstractValueInternal(
      bool allowOverwrite,
      NormalizedExpression<Variable> index, 
      AbstractDomain value, INumericalAbstractDomainQuery<Variable, Expression> oracle, SearchInfo info,
      out ArraySegmentation<AbstractDomain, Variable, Expression> result)
    {
      #region Contracts 
      Contract.Requires(index != null);
      Contract.Requires(value != null);
      Contract.Requires(oracle != null);

      Contract.Requires(0 <= info.Low);
      Contract.Requires(0 <= info.Upp);
      Contract.Requires(info.Low < this.elements.Count + 1);
      Contract.Requires(info.Upp <= info.UppMerge);
      Contract.Requires(info.Upp <= this.elements.Count);
      Contract.Requires(info.UppMerge < this.limits.Count);
      Contract.Requires(this.limits.Count == this.elements.Count + 1);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);
      #endregion

      // The new values
      NonNullList<SegmentLimit<Variable>> newLimits;
      NonNullList<AbstractDomain> newElements;

      CopyPrefix(info.Low, out newLimits, out newElements);

      result = default(ArraySegmentation<AbstractDomain, Variable, Expression>);

      #region Set the prefix

      // Case 1 : { L } val ...   && L == index so we generate { L, index } val ...
      if (info.IsLowerBoundEqual)
      {
        newLimits.Add(this.limits[info.Low].Add(index));
        newElements.Add(value);

        Contract.Assert(newLimits.Count == newElements.Count);
      }
      else // Case 2 : { L } val  { L1 } val1 { L2 } ... && L < index so we generate { L } Join(val) { index } ...
        if (info.IsLowerBoundStrict)
        {
          // { L }
          newLimits.Add(this.limits[info.Low]);

          if (!allowOverwrite)
            return false;

          // Join(val)
          var joinValues = JoinValuesInASubrange(info.Low, info.Upp, value.Bottom);
          newElements.Add(joinValues);

          // { index }
          newLimits.Add(new SegmentLimit<Variable>(index, false));

          // value
          newElements.Add(value);

          Contract.Assert(newLimits.Count == newElements.Count);
        }
        else if (info.IsUpperBoundEqual)
        {
          if (!allowOverwrite)
            return false;

          // { L }
          newLimits.Add(this.limits[info.Low]);

          // Join(val)
          var joinValues = JoinValuesInASubrange(info.Low, info.Upp, value.Bottom);
          newElements.Add(joinValues);

          // { index }
          newLimits.Add(new SegmentLimit<Variable>(index, true));

          // value
          newElements.Add(value);

          Contract.Assert(newLimits.Count == newElements.Count);

        }
        else if (info.IsLowerBoundStrictlySmallerThanUpp)
        {    // Case 3 : { L } val { L1 } &&  L <= index < L1 so we generate {L} prevVal { index }? val ...
          if (!allowOverwrite)
            return false;

          // { L }
          newLimits.Add(this.limits[info.Low]);

          // Join(val)
          var joinValues = JoinValuesInASubrange(info.Low, info.Upp, value.Bottom);
          newElements.Add(joinValues);

          // { index }
          newLimits.Add(new SegmentLimit<Variable>(index, true));

          // { value }
          newElements.Add(value);

          Contract.Assert(newLimits.Count == newElements.Count);
        }
        else // Case 4 : { L } val  { L1 } val1 { L2 } ... && L <= index so we generate { L } Join(val)  ...
        // So we abstract away the index  
        {
          if (!allowOverwrite)
            return false;

          // { L }
          newLimits.Add(this.limits[info.Low]);

          // Join(val)
          var joinVal = JoinValuesInASubrange(info.Low, info.Upp, value.Bottom);

          joinVal = (AbstractDomain)joinVal.Join(value);

          // Join(val) join value
          newElements.Add(joinVal);

          Contract.Assert(newLimits.Count == newElements.Count);
        }

      #endregion

      // F: here it seems that the WP do not go back enough
      Contract.Assert(info.Upp < this.limits.Count);

      #region Set the upper limit

      int upp;

      // Case 1 : ...  { U } ... && index + 1 < U so we generate {index+1} \join(val) { U } ...  
      if (info.IsUpperBoundStrict)
      {
        if (!allowOverwrite)
          return false;

        // { index + 1 }
        newLimits.Add(new SegmentLimit<Variable>(index.PlusOne(), false));

        // Join(Val)
        newElements.Add(JoinValuesInASubrange(info.Low, info.Upp, value.Bottom));

        // { U }
        var uppLimit = this.limits[info.Upp];

        Contract.Assert(uppLimit != null);

        newLimits.Add(new SegmentLimit<Variable>(uppLimit, false));

        upp = info.Upp + 1;

        Contract.Assert(newLimits.Count == newElements.Count + 1);
      }
      else  // Case 2 : .... { U } ... && index + 1 == U so we generate { U, index+1 } ...
        if (info.IsUpperBoundEqual)
        {
          // update the previous bound with the info that it is -1
          Contract.Assume(newLimits.GetLastElement().Contains(index));
          newLimits[newLimits.Count - 1] = newLimits.GetLastElement().Add(this.limits[info.Upp].MinusOne());

          // { index + 1, U }
          newLimits.Add(this.limits[info.Upp].Add(index.PlusOne()));

          upp = info.Upp + 1;

          Contract.Assert(newLimits.Count == newElements.Count + 1);
        }
        else // Case 3 : ...  { U }[?] ... && index + 1 <= U so we generate {index+1} \join(val) { U }[?] ...  
        {
          var nextLimits = new Set<NormalizedExpression<Variable>>(index.PlusOne());
          if (newLimits[newLimits.Count - 1].Contains(index))
          {
            foreach (var exp in newLimits[newLimits.Count - 1])
            {
              // a limit can be null (with no semantic meaning)
              if (exp != null)
              {
                nextLimits.Add(exp.PlusOne());
              }
            }
          }

          // { index + 1 }
          newLimits.Add(new SegmentLimit<Variable>(nextLimits, false));

          // Join(Val)
          newElements.Add(JoinValuesInASubrange(info.Low, info.Upp, value.Bottom));

          Contract.Assert(newElements.Count > 0); // F: Weakness of List.Add

          // { U }
          bool a, b;

          // If we cannot prove that index +1 <= U, then we abstract away U
          if (!this.LessEqual(index.PlusOne(), this.limits[info.UppMerge], oracle, out a, out b))
          {
            if (!allowOverwrite)
              return false;

            if (info.UppMerge < this.elements.Count)
            {
              var prev = this.elements[info.UppMerge];

              newElements[newElements.Count - 1] = (AbstractDomain)newElements[newElements.Count - 1].Join(prev);
              info.UppMerge++;
            }
            else  // Special case for the last element
            {
              Contract.Assume(info.UppMerge > 0);   // F: At the moment, I am not 100% sure why it is the case.
              var prev = this.elements[info.UppMerge - 1];
              newElements[newElements.Count - 1] = (AbstractDomain)newElements[newElements.Count - 1].Join(prev);
            }

            var uppMergeLimits = this.limits[info.UppMerge];

            Contract.Assert(uppMergeLimits != null);

            newLimits.Add(new SegmentLimit<Variable>(uppMergeLimits, true));

            upp = info.UppMerge + 1;
          }
          else
          {

            var uppMergeLimits = this.limits[info.UppMerge];

            Contract.Assert(uppMergeLimits != null);

            newLimits.Add(new SegmentLimit<Variable>(uppMergeLimits, true));

            upp = info.UppMerge + 1;
          }
          Contract.Assert(newLimits.Count == newElements.Count + 1);
        }

      #endregion

      // Set the remaining
      CopySuffix(Math.Min(this.limits.Count, upp), newLimits, newElements);

      result = new ArraySegmentation<AbstractDomain, Variable, Expression>(newLimits, newElements, this.bottomElement, this.expManager);
      return true;
    }
   
    #endregion

    #region SetAbstractValue

    public bool TrySetAbstractValue(NormalizedExpression<Variable> index, 
      AbstractDomain value,
      INumericalAbstractDomainQuery<Variable, Expression> oracle,
      out ArraySegmentation<AbstractDomain, Variable, Expression> result)
    {
      #region Contracts
      
      Contract.Requires(index != null);
      Contract.Requires(value != null);
      Contract.Requires(oracle != null);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);

      #endregion

      Contract.Assume(this.limits.Count == this.elements.Count +1);

      var info = new SearchInfo();
      if (this.IsNormal)
      {
        if (TrySearchBoundsForAddingASegment(index, oracle, ref info))
        {
          return TrySetAbstractValueInternal(true, index, value, oracle, info, out result);
        }
      }

      result = default(ArraySegmentation<AbstractDomain, Variable, Expression>);
      return false;
    }

     
    public bool TrySetAbstractValue(NormalizedExpression<Variable> lowIndex, NormalizedExpression<Variable> uppIndex,
      AbstractDomain value,
      INumericalAbstractDomainQuery<Variable, Expression> oracle,
      out ArraySegmentation<AbstractDomain, Variable, Expression> result)
    {
      #region Contracts
      Contract.Requires(lowIndex != null);
      Contract.Requires(uppIndex != null);
      Contract.Requires(oracle != null);

      Contract.Requires(value != null);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out result) != null);
      #endregion

      // F: we need some more checking code above to determine that we call this method only on normal abstract states
      Contract.Assume(this.limits.Count == this.elements.Count + 1);
      
      var info = new SearchInfo();
      if (TrySearchBounds(lowIndex, uppIndex, oracle, ref info))
      {
        // F: We should put it here as otherwise the WP do not go back enough
        Contract.Assert(info.Low >= 0);
        Contract.Assert(info.Upp > 0); 
        Contract.Assert(info.Low < this.elements.Count);
        Contract.Assert(info.Upp <= this.elements.Count);

        NonNullList<SegmentLimit<Variable>> newLimits;
        NonNullList<AbstractDomain> newElements;

        this.CopyPrefix(info.Low, out newLimits, out newElements);

        newLimits.Add(this.limits[info.Low]);

        Contract.Assert(info.Low < this.elements.Count);

        var smash = this.JoinValuesInASubrange(info.Low, info.Upp);

        smash = (AbstractDomain) smash.Join(value);

        this.CopySuffix(info.Upp, newLimits, newElements);

        Contract.Assert(info.Upp > 0);

        // The reason why this is true is because either CopyPrefix or CopySuffix added at least one element to newElements
        Contract.Assume(newElements.Count > 0); 

        newElements[Math.Min(info.Upp - 1, newElements.Count-1)] = smash;

        Contract.Assert(newLimits.Count == newElements.Count + 1);

        result = new ArraySegmentation<AbstractDomain, Variable, Expression>(newLimits, newElements, this.bottomElement, this.expManager);
        return true;
      }

      result = default(ArraySegmentation<AbstractDomain, Variable, Expression>);
      return false;
    }

     
    private AbstractDomain JoinValuesInASubrange(int low, int upp, IAbstractDomain bottom)
    {
      #region Contracts
      Contract.Requires(bottom != null);
      Contract.Requires(upp <= this.elements.Count);

      Contract.Ensures(Contract.Result<AbstractDomain>() != null);
      #endregion

      var joinVal = bottom;
      for (var i = Math.Max(0, low); i < upp; i++)
      {
        joinVal = joinVal.Join(this.elements[i]);
      }

      var tmp = (AbstractDomain)joinVal;
      Contract.Assert(tmp != null);

      return tmp;
    }
     
    [Pure]
    private AbstractDomain JoinValuesInASubrange(int low, int upp)
    {
      #region Contracts
      Contract.Requires(low >= 0);
      
      Contract.Requires(low <= upp);

      Contract.Requires(low < this.elements.Count);
      Contract.Requires(upp <= this.elements.Count);

      Contract.Ensures(Contract.Result<AbstractDomain>() != null);
      #endregion

      IAbstractDomain joinVal = this.elements[low];

      Contract.Assert(joinVal != null); 

      for (var i = low + 1; i < upp; i++)
      {
        joinVal = joinVal.Join(this.elements[i]);
        Contract.Assert(joinVal != null);
      }

      // F: Sticking the assumption here until NN analysis will track "if reference then NN"
      var tmp = (AbstractDomain)joinVal;
      Contract.Assert( tmp != null);

      return tmp;
    }

    [Pure]     
    private void CopyPrefix(int limit, out NonNullList<SegmentLimit<Variable>> newLimits, out NonNullList<AbstractDomain> newElements)
    {
      #region Contracts

      Contract.Requires(limit >= -1);
      Contract.Requires(limit < this.limits.Count);
      Contract.Requires(this.limits.Count == this.elements.Count + 1);

      Contract.Ensures(Contract.ValueAtReturn(out newLimits) != null);
      Contract.Ensures(Contract.ValueAtReturn(out newElements) != null);
      Contract.Ensures(Contract.ValueAtReturn(out newLimits).Count == Contract.ValueAtReturn(out newElements).Count);

      #endregion

      newLimits = new NonNullList<SegmentLimit<Variable>>();
      newElements = new NonNullList<AbstractDomain>();

      for (var i = 0; i < limit; i++)
      {
        newLimits.Add(this.limits[i]);
        newElements.Add(this.elements[i]);

        Contract.Assert(newLimits.Count == newElements.Count); // F: because of Add
      }
    }

    [Pure]
     
    private void CopySuffix(int start, NonNullList<SegmentLimit<Variable>> newLimits, NonNullList<AbstractDomain> newElements)
    {
      Contract.Requires(newLimits != null);
      Contract.Requires(newElements != null);

      Contract.Requires(start >= 0);
      Contract.Requires(start <= this.limits.Count);

      for (var i = start; i < this.limits.Count; i++)
      {
        newLimits.Add(this.limits[i]);
      }

      for (var i = Math.Max(start - 1, 0); i < this.elements.Count; i++)
      {
        newElements.Add(this.elements[i]);
      }
    }

    #endregion

    #region Limits

    public IEnumerable<SegmentLimit<Variable>> Limits
    {
      get
      {
        return this.limits;
      }
    }

    public SegmentLimit<Variable> LastLimit
    {
      get
      {
        Contract.Requires(this.IsNormal && !this.IsEmptyArray);
        Contract.Ensures(Contract.Result<SegmentLimit<Variable>>() != null);

        return this.limits[this.limits.Count - 1];
      }
    }

    #endregion

    #region Elements
    public IEnumerable<AbstractDomain> Elements
    {
      [ContractVerification(false)]
      get
      {
        Contract.Ensures(this.elements != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<AbstractDomain>>(), x => x != null));

        return this.elements;
      }
    }
    #endregion

    #region Search bounds (TrySearchBounds* methods)

    /// <summary>
    /// Return the largest index of a segment such that limits[index] \leq exp
    /// </summary>
     
    public bool TrySearchBoundsForAddingASegment(
      NormalizedExpression<Variable> exp, INumericalAbstractDomainQuery<Variable, Expression> oracle, 
      ref SearchInfo info)
    {
      #region Contracts

      Contract.Requires(exp != null);
      Contract.Requires(oracle != null);

      Contract.Ensures(-1 <= info.Low);
      Contract.Ensures(0 <= info.Upp);
      Contract.Ensures(0 <= info.UppMerge);

      Contract.Ensures(info.Upp <= info.UppMerge);

      Contract.Ensures(info.Upp <= this.limits.Count);
      Contract.Ensures(info.UppMerge <= this.limits.Count);      
      Contract.Ensures(info.Low < this.limits.Count);

      // If we find the bounds, then they are within the bounds of limits
      Contract.Ensures(!Contract.Result<bool>() || info.Low >= 0);

      Contract.Ensures(!Contract.Result<bool>() || info.Upp < this.limits.Count);
      Contract.Ensures(!Contract.Result<bool>() || info.UppMerge < this.limits.Count);

      Contract.Ensures(!Contract.Result<bool>() || info.Upp <= this.elements.Count);
      Contract.Ensures(!Contract.Result<bool>() || info.UppMerge <= this.elements.Count);

      #endregion

      info.Low = -1;
      info.Upp = this.limits.Count;
      info.UppMerge = this.limits.Count;
      info.IsLowerBoundEqual = false;
      info.IsUpperBoundStrict = false;

      // Search the lower bound
      for (var i = 0; i < this.limits.Count; i++)
      {
        if (!GreaterEqualThan(exp, this.limits[i], oracle, out info.IsLowerBoundEqual, out info.IsLowerBoundStrict) || info.IsLowerBoundEqual)
        {
          info.Low = info.IsLowerBoundEqual ? i : i - 1;

          if (!info.IsLowerBoundEqual && info.Low >= 0)
          {
            info.IsLowerBoundStrict = ExistsLessThan(this.limits[info.Low], exp, oracle);
          }

          break;
        }
      }
      if (info.Low < 0)
      {
        return false;
      }

      info.Upp =  info.Low + 1;

      while (info.Upp < this.limits.Count && !ExistsLessThan(exp, this.limits[info.Upp], oracle))
      {
        info.Upp++;
      }

      if (info.Upp >= this.limits.Count)
      {
        return false;
      }

      info.UppMerge = info.Upp;

      var expPlusOne = exp.PlusOne();

      // \exists l. exp+1 == l
      info.IsUpperBoundEqual = Equal(expPlusOne, this.limits[info.Upp], oracle);

      // \exists l. exp+1 < l
      info.IsUpperBoundStrict = LessThan(expPlusOne, this.limits[info.Upp], oracle);


      for (var i = info.Upp; i < this.limits.Count; i++)
      {
        if (this.limits[i].IsConditional)
        {
          info.UppMerge = i;
        }
        else
        {
          break;
        }
      }

      info.IsLowerBoundStrictlySmallerThanUpp = ExistsLessThan(exp, this.limits[info.Upp], oracle);

      Contract.Assume(this.limits.Count == this.elements.Count + 1);

      return true;
    }
     
    public bool TrySearchBounds(NormalizedExpression<Variable> lowExp, NormalizedExpression<Variable> uppExp,
      INumericalAbstractDomainQuery<Variable, Expression> oracle, ref SearchInfo info)
    {
      #region Contracts
      Contract.Requires(lowExp != null);
      Contract.Requires(uppExp != null);
      Contract.Requires(oracle != null);

      Contract.Ensures(info.Low >= -1);
      Contract.Ensures(info.Upp >= 0);
      //Contract.Ensures(info.UppMerge >= 0);

      Contract.Ensures(info.Low <= info.Upp);
      Contract.Ensures(info.Low <= this.elements.Count);

      Contract.Ensures(info.Upp == info.UppMerge);

      Contract.Ensures(info.Upp <= this.limits.Count);

      Contract.Ensures(!Contract.Result<bool>() || info.Low >= 0);
      Contract.Ensures(!Contract.Result<bool>() || info.Upp > 0);

      Contract.Ensures(!Contract.Result<bool>() || info.Low < this.elements.Count);

      Contract.Ensures(!Contract.Result<bool>() || info.Low <= info.Upp);
      
      Contract.Ensures(!Contract.Result<bool>() || info.Upp < this.limits.Count);
      Contract.Ensures(!Contract.Result<bool>() || info.Upp <= this.elements.Count);
      
      #endregion

      // Let's search the lower bound
      info.Low = -1;
      info.Upp = info.UppMerge = 0;

      if (this.IsEmptyArray || !this.IsNormal)
      {
        return false;
      }

      Contract.Assert(this.elements.Count >= 1);
      Contract.Assert(this.limits.Count == this.elements.Count + 1);

      // Fast, syntactic search for the lower bound
      var found = false;
      for (var i = 0; i < this.limits.Count-1; i++)
      {
        if (this.limits[i].Contains(lowExp))
        {
          info.Low = i;
          Contract.Assert(info.Low < this.elements.Count);
          
          info.IsLowerBoundEqual = true;
          info.IsLowerBoundStrict = false;
          found = true;
          break;
        }
      }

      Contract.Assert(this.limits.Count == this.elements.Count + 1);

      // Semantic search
      if (!found)
      {
        Contract.Assert(this.elements.Count < this.limits.Count); // ok we can prove it
        for (var i = 0; i < this.limits.Count; i++)
        {
          if (!GreaterEqualThan(lowExp, this.limits[i], oracle, out info.IsLowerBoundEqual, out info.IsLowerBoundStrict) || info.IsLowerBoundEqual) // We found our index!
          {
            info.Low = info.IsLowerBoundEqual ? i : i - 1;
            
            Contract.Assume(info.Low < this.elements.Count, "we cannot prove it, because i <= this.elements.Count, maybe there is a reason why it holds, or my code is buggy");

            break;
          }
        }


        if (info.Low < 0 || info.Low == this.elements.Count)  // We did not find any lower bound
        {
          info.Low = -1;  // This is to make sure that 

          return false;
        }
      }

      Contract.Assert(info.Low >= 0); // If we reach that point, we found a lower bound. 
      Contract.Assert(info.Low < this.elements.Count); 

      // Let's search the upper bound 

      var uppExpAsBoxedExpression = ToExp(uppExp);
      Func<NormalizedExpression<Variable>, bool> semanticEquality = (NormalizedExpression<Variable> limit) => { return oracle.CheckIfEqual(ToExp(limit), uppExpAsBoxedExpression).IsTrue();};

      // Syntactic search
      for (var i = info.Low+1; i < this.limits.Count; i++)
      {
        // Found?
        var limits = this.limits[i];
        if (limits.Contains(uppExp) || (uppExpAsBoxedExpression != null && limits.Any(semanticEquality)))
        {
          info.Upp = info.UppMerge = i;
          info.IsUpperBoundEqual = true;
          info.IsUpperBoundStrict = false;

          info.IsLowerBoundStrictlySmallerThanUpp = ExistsLessThan(lowExp, this.limits[info.Upp], oracle);

          return true;
        }
      }
      
      // Semantic search
      info.Upp = this.limits.Count;

      for (var i = this.limits.Count - 1; i >= 0; i--)
      {
        if (!LessEqual(uppExp, this.limits[i], oracle, out info.IsUpperBoundEqual, out info.IsUpperBoundStrict) || 
          info.IsUpperBoundEqual || !info.IsUpperBoundStrict)
        {
          // There are two reasons why we are here.
          // The first is that the uppExp is to the left of the bound (i.e. limits[i] is no more a lower bound for uppEx)
          // In this case, we know that it was the case for limits[i+1], so we set info.Upp = i+1
          // The second is because we hit the exact bound for uppExp.
          // In this case, we know that we have to consider the next element as well

          info.Upp = Math.Min(i+1, this.limits.Count-1);

          // The reason why this is true is because of the global invariant of the segmentation, that is we cannot have malformed segmentations { a } el { b } where a > b
          // This is definitively out-of-reach of what Clousot can prove today
          Contract.Assume(info.Low <= info.Upp);
          break;
        }
      }

      info.UppMerge = info.Upp;

      if (info.Upp == this.limits.Count)
      {
        // Make it not meaningful
        info.Low = -1;  
      
        return false;
      }

      Contract.Assert(info.Upp < this.limits.Count);

      info.IsLowerBoundStrictlySmallerThanUpp = ExistsLessThan(lowExp, this.limits[info.Upp], oracle);

      return true;
    }


    #endregion

    #region SegmentLimitsQuery
     
    public bool Equal(NormalizedExpression<Variable> exp, SegmentLimit<Variable> segment, INumericalAbstractDomainQuery<Variable, Expression> oracle)
    {
      Contract.Requires(exp != null);
      Contract.Requires(segment != null);
      Contract.Requires(oracle != null);

      if (segment.Contains(exp))
        return true;

      foreach (var limit in segment)
      {
        if (limit != null && oracle.CheckIfEqual(ToExp(exp), ToExp(limit)).IsTrue())
        {
          return true;
        }
      }

      return false;
    }
     
    public bool LessEqual(NormalizedExpression<Variable> exp, SegmentLimit<Variable> segment, INumericalAbstractDomainQuery<Variable, Expression> oracle, 
      out bool equal, out bool strict)
    {
      Contract.Requires(exp != null);
      Contract.Requires(segment != null);
      Contract.Requires(oracle != null);

      if (segment.Contains(exp))
      {
        equal = true;
        strict = false;
        return true;
      }

      var expExp = ToExp(exp);

      foreach (var limit in segment)
      {
        if(limit == null) continue;

        var limitExp = ToExp(limit);
        var res = oracle.CheckIfLessEqualThan(expExp, limitExp);
        if (res.IsTrue())
        {
          equal = oracle.CheckIfEqual(expExp, limitExp).IsTrue();
          strict = oracle.CheckIfLessThan(expExp, limitExp).IsTrue();
          return true;
        }
      }

      equal = false;
      strict = false;
      return false;
    }

    public bool LessEqual(SegmentLimit<Variable> segment, NormalizedExpression<Variable> exp,  
      INumericalAbstractDomainQuery<Variable, Expression> oracle,
     out bool equal, out bool strict)
    {
      #region Contracts
      Contract.Requires(exp != null);
      Contract.Requires(segment != null);
      Contract.Requires(oracle != null);
      #endregion

      if (segment.Contains(exp))
      {
        equal = true;
        strict = false;
        return true;
      }

      var expExp = ToExp(exp);

      foreach (var limit in segment)
      {
        if (limit == null) continue;

        var limitExp = ToExp(limit);
        var res = oracle.CheckIfLessEqualThan(limitExp, expExp);
        if (res.IsTrue())
        {
          equal = oracle.CheckIfEqual(limitExp, expExp).IsTrue();
          strict = oracle.CheckIfLessThan(limitExp, expExp).IsTrue();
          return true;
        }
      }

      equal = false;
      strict = false;
      return false;
    }
  
    [Pure]
    public bool GreaterEqualThan(
      NormalizedExpression<Variable> exp, SegmentLimit<Variable> segment, INumericalAbstractDomainQuery<Variable, Expression> oracle, 
      out bool equal, out bool strict)
    {
      Contract.Requires(exp != null);
      Contract.Requires(segment != null);
      Contract.Requires(oracle != null);

      if (segment.Contains(exp))
      {
        equal = true;
        strict = false;
        return true;
      }

      var expExp = ToExp(exp);

      foreach (var limit in segment)
      {
        if (limit == null)
          continue;

        var limitExp = ToExp(limit);
        var res = oracle.CheckIfLessEqualThan(limitExp, expExp);
        if (res.IsTrue())
        {
          equal = oracle.CheckIfEqual(expExp, limitExp).IsTrue();
          strict = oracle.CheckIfLessThan(limitExp, expExp).IsTrue();
          return true;
        }
        if (res.IsFalse())
        {
          equal = false;
          strict = true;
          return false;
        }
      }

      equal = false;
      strict = false;
      return false;
    }    
     
    public bool LessThan(NormalizedExpression<Variable> exp, SegmentLimit<Variable> segment, INumericalAbstractDomainQuery<Variable, Expression> oracle)
    {
      Contract.Requires(exp != null);
      Contract.Requires(segment != null);
      Contract.Requires(oracle != null);

      foreach (var limit in segment)
      {
        if (limit != null && oracle.CheckIfLessThan(ToExp(exp), ToExp(limit)).IsTrue())
        {
          return true;
        }
      }

      return false;
    }

    public bool LessThan(SegmentLimit<Variable> segment, NormalizedExpression<Variable> exp, INumericalAbstractDomainQuery<Variable, Expression> oracle)
    {
      Contract.Requires(exp != null);
      Contract.Requires(segment != null);
      Contract.Requires(oracle != null);

      foreach (var limit in segment)
      {

        if (limit != null && oracle.CheckIfLessThan(ToExp(limit), ToExp(exp)).IsTrue())
        {
          return true;
        }
      }

      return false;

    }
     
    public bool ExistsLessThan(SegmentLimit<Variable> segment, 
      NormalizedExpression<Variable> exp, INumericalAbstractDomainQuery<Variable, Expression> oracle)
    {
      Contract.Requires(exp != null);
      Contract.Requires(segment != null);
      Contract.Requires(oracle != null);

      foreach (var limit in segment)
      {
        if (limit != null && oracle.CheckIfLessThan(ToExp(limit), ToExp(exp)).IsTrue())
        {
          return true;
        }
      }

      return false;
    }

    public bool ExistsLessThan(NormalizedExpression<Variable> exp, SegmentLimit<Variable> segment,
      INumericalAbstractDomainQuery<Variable, Expression> oracle)
    {
      Contract.Requires(exp != null);
      Contract.Requires(segment != null);
      Contract.Requires(oracle != null);

      foreach (var limit in segment)
      {
        if (limit != null && oracle.CheckIfLessThan(ToExp(exp), ToExp(limit)).IsTrue())
        {
          return true;
        }
      }

      return false;
    }

    #endregion

    #region TestTrueEqual
     
    public ArraySegmentation<AbstractDomain, Variable, Expression>
      TestTrueEqualAsymmetric(NormalizedExpression<Variable> v, NormalizedExpression<Variable> normExpression)
    {
      Contract.Requires(v != null);
      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);

      if (!this.IsNormal)
      {
        return this;
      }

      Contract.Assert(this.elements.Count + 1== this.limits.Count); // assertion needed to prove the precondition later

      var newElements = new NonNullList<AbstractDomain>(this.elements);
      var newLimits = new NonNullList<SegmentLimit<Variable>>(this.limits.Count);

      for (var i = 0; i < this.limits.Count; i++)
      {
        var limit = this.limits[i];
        if (limit.Contains(normExpression))
        {
          var updatedLimits = limit.Add(v);

          Contract.Assert(updatedLimits != null);

          newLimits.InsertOrAdd(i , updatedLimits);

          if (i < this.limits.Count - 1) 
          {
            var nextLimit = this.limits[i + 1];
            if (nextLimit.IsSuccessorOf(updatedLimits))
            {
              newLimits.InsertOrAdd(i + 1, nextLimit.Add(v.PlusOne()));
              i++; // Skip one iteration
            }
          }
        }
        else
        {
          newLimits.InsertOrAdd(i, limit);
        }
      }
      Contract.Assume(this.limits.Count == this.elements.Count + 1, "for some reason we lose the assertion above");

      return new ArraySegmentation<AbstractDomain, Variable, Expression>(newLimits, newElements, this.bottomElement, this.expManager);
    }

     
    public ArraySegmentation<AbstractDomain, Variable, Expression>
      TestTrueEqualAsymmetric(Set<NormalizedExpression<Variable>> vars, NormalizedExpression<Variable> normExpression)
    {
      Contract.Requires(vars != null);
      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);

      if (!this.IsNormal)
      {
        return this;
      }

      Contract.Assert(this.elements.Count + 1 == this.limits.Count); // assertion needed to prove the precondition later

      var newElements = new NonNullList<AbstractDomain>(this.elements);
      var newLimits = new NonNullList<SegmentLimit<Variable>>(this.limits.Count);

      int i;
      for (i = 0; i < this.limits.Count; i++)
      {
        var limit = this.limits[i];
        if (limit.Contains(normExpression))
        {
          newLimits.InsertOrAdd(i, limit.Add(vars));
        }
        else
        {
          newLimits.InsertOrAdd(i, limit);
        }
      }
      Contract.Assume(this.limits.Count == this.elements.Count + 1, "for some reason we lose the assertion above");
      Contract.Assert(newLimits.Count == newElements.Count + 1);
      return new ArraySegmentation<AbstractDomain,Variable,Expression>(newLimits, newElements, this.bottomElement, this.expManager);
    }

    #endregion

    #region TestTrueLessThan
     
    public ArraySegmentation<AbstractDomain, Variable, Expression> TestTrueLessThan(NormalizedExpression<Variable> left, NormalizedExpression<Variable> right)
    {
      for (int i = 0; i < this.limits.Count - 1; i++)
      {
        var leftLimit = this.limits[i];
        var rightLimit = this.limits[i + 1];

        Contract.Assert(leftLimit != null);
        Contract.Assert(rightLimit != null);

        if (leftLimit.Contains(left) && rightLimit.Contains(right))
        {
          if (rightLimit.IsConditional)
          {
            var result = this.DuplicateMe();
            result.limits[i + 1] = new SegmentLimit<Variable>(rightLimit, false);

            return result;
          }
          else
          {
            return this;
          }
        }
      }

      return this;
    }

    #endregion

    #region TestTrueWithIntConstants
     
    public ArraySegmentation<AbstractDomain, Variable, Expression> 
      TestTrueWithIntConstants(List<Pair<NormalizedExpression<Variable>, NormalizedExpression<Variable>>> intConstants)
    {
      Contract.Requires(intConstants != null);
      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);

      if (intConstants.Count == 0 || this.IsBottom || this.IsTop)
      {
        return this;
      }

      var newLimits = new NonNullList<SegmentLimit<Variable>>(this.limits);
      var newElements = new NonNullList<AbstractDomain>(this.elements);

      foreach (var pair in intConstants)
      {
        int i = 0;
        for (; i < newLimits.Count; i++)
        {
          var newLimits_i = newLimits[i];
          if (newLimits_i.Contains(pair.One))
          {
            newLimits[i] = newLimits_i.Add(pair.Two);
            break;
          }

          if (newLimits_i.Contains(pair.Two))
          {
            newLimits[i] = newLimits_i.Add(pair.One);
            break;
          }
        }
      }

      Contract.Assume(newLimits.Count == newElements.Count+1);
      return new ArraySegmentation<AbstractDomain, Variable, Expression>(newLimits, newElements, this.bottomElement, this.expManager);
    }

    #endregion

    #region TestTrueFromNumericalstate


    #region
    internal ArraySegmentation<AbstractDomain, Variable, Expression> TestTrueInformationForTheSegments(INumericalAbstractDomainQuery<Variable, Expression> oracle)
    {
      #region Contracts

      Contract.Requires(oracle != null);

      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);

      #endregion

      if (!this.IsNormal)
      {
        return this;
      }

      Contract.Assert(this.limits.Count >= 2, "should become an assertion");

      Contract.Assert(this.elements.Count + 1 == this.limits.Count); // assertion needed to prove the precondition later

      var resultLimits = new NonNullList<SegmentLimit<Variable>>(this.limits);

      Contract.Assert(resultLimits.Count == this.limits.Count);

      var resultElements = new NonNullList<AbstractDomain>(this.elements.Count);

      for (var i = 0; i < this.elements.Count; i++)
      {
        var newSegment = this.elements[i].AssumeInformationFrom(oracle);

        resultElements.Add(newSegment);
      }

      Contract.Assert(resultElements.Count == this.elements.Count);

      Contract.Assume(this.elements.Count + 1 == this.limits.Count, "for some reason we lose the assertion above"); // assertion needed to prove the precondition later

      return new ArraySegmentation<AbstractDomain, Variable, Expression>(resultLimits, resultElements, this.bottomElement, this.expManager);
    }
    #endregion

    #endregion

    #region Reductions
     
    public ArraySegmentation<AbstractDomain, Variable, Expression> ReduceWith(INumericalAbstractDomainQuery<Variable, Expression> oracle)
    {
      #region Contracts

      Contract.Requires(oracle != null);

      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);

      #endregion

      if (this.IsEmptyArray || this.IsBottom || this.IsTop)
      {
        return this;
      }

      // We need the expressions encoder to push the information.
      // If we do not have one, we just gave up

      IExpressionEncoder<Variable, Expression> encoder;
      if (!this.expManager.TryGetEncoder(out encoder))
      {
        return this;
      }

      Contract.Assert(encoder != null, "to help wp");

      // F: Should become an assertion
      Contract.Assume(this.limits.Count >= 2);

      var resultLimits = new NonNullList<SegmentLimit<Variable>>();
      var resultElements = new NonNullList<AbstractDomain>();

      var refined = false;

      resultLimits.Add(this.limits[0]);

      Contract.Assert(resultLimits.Count == 1);

      Contract.Assume(this.limits.Count == this.elements.Count + 1);

      for (var i = 1; i < this.limits.Count; i++)
      {               
        Contract.Assert(i <= this.elements.Count); 

        var curr = this.limits[i];

        Contract.Assert(curr != null);

        if (curr.IsConditional)
        {
          var prev = resultLimits[resultLimits.Count - 1];

          Contract.Assert(prev != null);

          foreach (var p in prev)
          {
            Contract.Assume(p != null);

            foreach (var c in curr)
            {
              #region Body
              Contract.Assume(c != null);

              var pExp = p.Convert(encoder);
              var cExp = c.Convert(encoder);
//              var isEq = PerformanceMeasure.Measure(PerformanceMeasure.ActionTags.CheckIfEqual, () => oracle.CheckIfEqual(pExp, cExp));
              var isEq = PerformanceMeasure.Measure(PerformanceMeasure.ActionTags.CheckIfEqual, () => oracle.CheckIfEqualIncomplete(pExp, cExp));

              if (isEq.IsTrue())    // The segment is empty, so we remove it
              {
                // { 0, a } [0, 0] { a }? 
                if (this.limits.Count == 2)
                {
                  return this.limits[1].IsConditional? this : this.Bottom;
                }

                resultLimits[resultLimits.Count - 1] = new SegmentLimit<Variable>(prev.Meet(curr), prev.IsConditional);

                refined = true;

                goto nextLoopIteration;
              }

              // The ? can go away
              if (isEq.IsFalse() || oracle.CheckIfLessThanIncomplete(pExp, cExp).IsTrue())                   
              {
                resultElements.Add(this.elements[i - 1]);
                resultLimits.Add(new SegmentLimit<Variable>(curr, false));

                Contract.Assert(resultLimits.Count > 0);
                Contract.Assert(resultElements.Count > 0);

                refined = true;

                goto nextLoopIteration;
              }
              #endregion
            }
          }
        }

        resultElements.Add(this.elements[i - 1]);
        resultLimits.Add(curr);

      nextLoopIteration:
        
        ;
      }

      Contract.Assert(resultLimits.Count == resultElements.Count +1 );

      // It may be the case that the reduction has smashed together all the limits, by proving all be the same
      if (resultLimits.Count <= 1)
      {
       return EmptyArray;
      }

      return refined ?
        new ArraySegmentation<AbstractDomain, Variable, Expression>(resultLimits, resultElements, this.bottomElement, this.expManager) :
        this;
    }

    #endregion    

    #region Normal Form
    static private void RemoveTriviallyEmptySegments(NonNullList<SegmentLimit<Variable>> limits, NonNullList<AbstractDomain> elements,
      out NonNullList<SegmentLimit<Variable>> newLimits, out NonNullList<AbstractDomain> newElements, out bool isBottom)
    {
      Contract.Requires(limits != null);
      Contract.Requires(elements != null);
      Contract.Requires(limits.Count == elements.Count + 1);
      
      Contract.Ensures(Contract.ValueAtReturn(out newLimits) != null);
      Contract.Ensures(Contract.ValueAtReturn(out newElements) != null);

      Contract.Ensures(Contract.ValueAtReturn(out newLimits).Count <= limits.Count);
      Contract.Ensures(Contract.ValueAtReturn(out newElements).Count <= elements.Count);

      newLimits = new NonNullList<SegmentLimit<Variable>>();
      newElements = new NonNullList<AbstractDomain>();
      isBottom = false;

      Contract.Assert(newLimits.Count <= limits.Count);
      Contract.Assert(newElements.Count <= elements.Count);

      for (var i = 0; i < limits.Count; i++)
      {
        // F: If we reach this point, it means that we still have some limit to consider, not added to the newLimits
        Contract.Assume(newLimits.Count < limits.Count);
        var prevLimit = limits[i];

        if (i == limits.Count - 1)
        {
          newLimits.Add(prevLimit);
        }
        else
        {
          // F: If we reach this point, it means that we still have some element to consider, not added to the newElements
          Contract.Assume(newElements.Count < elements.Count);

          var nextLimit = limits[i + 1];

          if (nextLimit.IsConditional)
          { 
            // Case 1: So common that we make it a special case
            // We have {a, x } {a, y}?
            if (HaveASameBound(prevLimit, nextLimit))
            {
              newLimits.Add(new SegmentLimit<Variable>(nextLimit.Meet(prevLimit), false));
              if (
                /* i < elements.Count && */               // REDUNDANT inferred by CC
                i + 1 < limits.Count - 1 // it is not the last limit
                )
              {
                newElements.Add(elements[i]);

                Contract.Assert(newElements.Count <= elements.Count);
              }
              i++; // skip the next
              goto nextLoopIteration;
            }

            // Case 2:
            // We have a sequence of conditionals { a, x } { b }? { a, y }? ==> {a, b, x, y }            
            // Repro: method 2937 of System.Design.dll, v.2.0 with -bounds -arrays -nonnull
            for (int lastConditionalIndex = i + 2; lastConditionalIndex < limits.Count; lastConditionalIndex++)
            {
              var upperLimit = limits[lastConditionalIndex];
              if (upperLimit.IsConditional)
              {
                // Ok we found a match!
                if (HaveASameBound(prevLimit, upperLimit))
                {
                  var unionLimit = prevLimit;
                  for (var j = i; j <= lastConditionalIndex; j++)
                  {
                    unionLimit = unionLimit.Meet(limits[j]);
                  }
                  newLimits.Add(unionLimit);    // The new limits are the union of all the limits

                  // Add this only if it is not the last limit
                  if (/*i < elements.Count && */ // REDUNDANT inferred by CC
                    lastConditionalIndex < limits.Count - 1)
                  {
                    newElements.Add(elements[i]); // The new elements are simply the one of the first limit
                    Contract.Assert(newElements.Count <= elements.Count);

                  }

                  i = lastConditionalIndex;

                  goto nextLoopIteration;
                }
              }
              else
              {
                break;
              }
            }

            Contract.Assert(newElements.Count <= elements.Count);

            int leftConst, rightConst;
            if (prevLimit.TryFindConstant(out leftConst) && nextLimit.TryFindConstant(out rightConst) && leftConst < rightConst)
            {
              // remove the conditional on the next limit
              limits[i + 1] = new SegmentLimit<Variable>(nextLimit, false);
            }

          }
          else
          {
            // Let's search for a contraddiction
            if (HaveASameBound(prevLimit, nextLimit))
            {
              isBottom = true;
            }
          }

          newLimits.Add(prevLimit);
          newElements.Add(elements[i]);

          Contract.Assert(newElements.Count <= elements.Count);
        }

      nextLoopIteration:
        ;
      }

    }
    #endregion

    #region Simplification
    /// <summary>
    /// The AssignInParallel may have produced a state as ... { a, b } e { a }? ...
    /// that we can can simplify to { a, b}
    /// </summary>         
    [Pure]
    public ArraySegmentation<AbstractDomain, Variable, Expression> Simplify()
    {
      if (this.IsEmptyArray || this.IsBottom || this.IsTop)
      {
        return this;
      }

      Contract.Assume(this.limits.Count == this.elements.Count + 1);

      NonNullList<SegmentLimit<Variable>> newLimits;
      NonNullList<AbstractDomain> newElements;

      bool isBottom;
      RemoveTriviallyEmptySegments(this.limits, this.elements, out newLimits, out newElements, out isBottom);
 
      Contract.Assume(newLimits.Count == newElements.Count + 1);
      return new ArraySegmentation<AbstractDomain, Variable, Expression>(newLimits, newElements, this.bottomElement, this.expManager);
    }

    [Pure]
    private static bool HaveASameBound(SegmentLimit<Variable> left, SegmentLimit<Variable> right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      // F: TODO implement a more efficient algorithm
      return left.Intersection(right).Count != 0;
    }
    #endregion

    #region Purification
    [Pure]
    public ArraySegmentation<AbstractDomain, Variable, Expression> Purify(List<Variable> vars)
    {
      Contract.Requires(vars != null);

      if (!this.IsNormal)
      {
        return this;
      }

      Contract.Assert(this.limits.Count > 1);
      Contract.Assert(this.elements.Count >= 1);


      if (vars.Count == 0)
      {
        return this.Bottom;
      }

      var newLimits = new NonNullList<SegmentLimit<Variable>>();
      var newElements = new NonNullList<AbstractDomain>();

      int i = 0, j = 0;

      #region First Limit
      var currLimit = this.limits[i];
      var newLimit = new Set<NormalizedExpression<Variable>>();
      
      int dummyInt;
      Variable v;
      foreach (var exp in currLimit)
      {
        Contract.Assume(exp != null);
        if (exp.IsConstant(out dummyInt))
        {
          newLimit.Add(exp);
        }
        else if (exp.IsVariable(out v) || exp.IsAddition(out v, out dummyInt))
        {
          if (vars.Contains(v))
          {
            newLimit.Add(exp);
          }
        }
      }

      newLimits.Add(new SegmentLimit<Variable>(newLimit, currLimit.IsConditional));
      newElements.Add(this.elements[i]);
      
      #endregion

      #region All the othe cases
      for (i = 1, j = 1; i < this.limits.Count; i++)
      {
        Contract.Assert(j <= i);

        currLimit = this.limits[i];
        newLimit = new Set<NormalizedExpression<Variable>>();
        foreach (var exp in currLimit)
        {
          Contract.Assume(exp != null);
          if (exp.IsConstant(out dummyInt))
          {
            newLimit.Add(exp);
          }
          else 
            if (exp.IsVariable(out v) || exp.IsAddition(out v, out dummyInt))
          {
            // this
            if (vars.Contains(v))
            {
              newLimit.Add(exp);
            }
          }
        }

        if (!newLimit.IsEmpty)
        {
          // this
          newLimits.Add(new SegmentLimit<Variable>(newLimit, currLimit.IsConditional));
          if (i < this.elements.Count) // Remember: there is one more limit than elements
          {
            newElements.Add(this.elements[i]);
          }
          j++;
        }
          // this
        else
        {
          Contract.Assert(j > 0); // This should always be true, as the first segment cannot be empty
          Contract.Assert(newElements.Count >= 1);

          if (i < this.elements.Count) // Remember: there is one more limit than elements
          {
            Contract.Assume(j -1 < newElements.Count);
            newElements[j - 1] = (AbstractDomain)newElements[j - 1].Join(this.elements[i]);
          }

        }
      }
      #endregion

      Contract.Assume(newLimits.Count == newElements.Count + 1);// F: Hard to prove because it depends onthe algorithmical knowledge that we insert in newLimits only once more that newElements
      return new ArraySegmentation<AbstractDomain, Variable, Expression>(newLimits, newElements, this.bottomElement, this.expManager);
    }

    #endregion

    #region Conversion

    [Pure]
    public Expression ToExp(NormalizedExpression<Variable> exp)
    {
      Contract.Requires(exp != null);

      IExpressionEncoder<Variable, Expression> encoder;
      if (this.expManager.TryGetEncoder(out encoder))
      {
        return exp.Convert(encoder);
      }
      else
      {
        return default(Expression);
      }
    }

    // Just a shortcut
    [Pure]
    public static NormalizedExpression<Variable> ToNormExp(int x)
    {
      Contract.Ensures(Contract.Result<NormalizedExpression<Variable>>() != null);
      
      return NormalizedExpression<Variable>.For(x);
    }

    #endregion

    #region Segment Unification

    private enum SegmentUnificationOperation { LessEqual, Join, Widening, Meet }

    [ContractVerification(false)]
    [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-!left.limits[0].Join(right.limits[0]).IsEmpty (Malformed segments: Both should contain at least 0)")] 
    [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-!left.limits[left.limits.Count - 1].Join(right.limits[right.limits.Count - 1]).IsEmpty (Malformed segments: Both should contain the array length)")] 
    private bool TryUnifySegments(SegmentUnificationOperation operation, ArraySegmentation<AbstractDomain, Variable, Expression> left, ArraySegmentation<AbstractDomain, Variable, Expression> right,
      AbstractDomain neutralLeft, AbstractDomain neutralRight,
      out ArraySegmentation<AbstractDomain, Variable, Expression> leftReduced, out ArraySegmentation<AbstractDomain, Variable, Expression> rightReduced,
      out bool removedAtLeastOneLimitForRight)
    {
      #region Contracts
      Contract.Requires(left.elements.Count > 0);
      Contract.Requires(right.elements.Count > 0);
      
      Contract.Requires(left.limits.Count  == left.elements.Count + 1);
      Contract.Requires(right.limits.Count == right.elements.Count +1);

      Contract.Requires(neutralLeft != null);
      Contract.Requires(neutralRight != null);

      // Those two preconditions are two complex to be checked at static time, so we mask them at all the call sites
      Contract.Requires(!left.limits[0].Join(right.limits[0]).IsEmpty, "Malformed segments: Both should contain at least 0");
      Contract.Requires(!left.limits[left.limits.Count - 1].Join(right.limits[right.limits.Count - 1]).IsEmpty, "Malformed segments: Both should contain the array length");
      
        // At least one element
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out leftReduced).elements.Count > 0); 
        
        // Unification worked          
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out leftReduced).limits.Count == Contract.ValueAtReturn(out rightReduced).limits.Count);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out leftReduced).elements.Count == Contract.ValueAtReturn(out rightReduced).elements.Count);
      
        // Consistency      
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out leftReduced).limits.Count == Contract.ValueAtReturn(out leftReduced).elements.Count + 1);

      #endregion

      if (ArrayOptions.Trace)
      {
        Console.WriteLine("#{0}: Uniforming\n{1}\n{2}", UniformCount++, left.ToString(), right.ToString());
      }

      removedAtLeastOneLimitForRight = false;
      var unificationIteration = 0;
      
      var newLeftLimits = new NonNullList<SegmentLimit<Variable>>();
      var newRightLimits = new NonNullList<SegmentLimit<Variable>>();

      var newLeftElements = new NonNullList<AbstractDomain>();
      var newRightElements = new NonNullList<AbstractDomain>();

      // Initialization
      var leftCount = 0;
      var rightCount = 0;

      // 
      var old_leftCount = -1;
      var old_rightCount = -1;

      // Step 1: Purify the abstract elements. This is a new introduction from our technical report
      // The idea is to remove the extra-variables appearing only in one segmentation, to improve the precision of the step 2 below
      // Essentially extra temp variables can be introduce lose of precision in the step 2 because we use a lookahead of 1 
      // 
      // ** Example 
      // Input:
      // {0}      a { sv4    } b { sv5 }  
      // {0, sv2} c { sv4 -1 } d { sv4 }
      // We get (after 2 iterations)
      // {0} a       {sv4}  b { sv5  }
      // {0} neutral {sv2}? c {sv4-1 } d { sv4}
      // And as we do not see any relation between sv4 and sv2, and we cannot see that sv4 will appear later, we abstract sv4
      // The purification algorithm gets rid of the unnecessary sv2 variable

      if (operation == SegmentUnificationOperation.Join || operation == SegmentUnificationOperation.Widening)
      {
        left = left.Purify(right.Variables);
        right = right.Purify(left.Variables);
      }
      if (ArrayOptions.Trace)
      {
        Console.WriteLine("After purification \n{0}\n{1}", left.ToString(), right.ToString());
      }

      // Step 2: Unify the segments      
      // TODO: do we need a wider lookahead? Purification seems ok, and larger lookahead may slow down things (quadratically)
      while (leftCount < left.limits.Count || rightCount <= right.limits.Count)
      {                
        // Loop invariants
          // Consistency: "The elements and the limits are in sync"  
        Contract.Assert(newLeftElements.Count == newRightElements.Count); 
        Contract.Assert(newLeftLimits.Count == newRightLimits.Count);
       
          // Progress: "We add at least one <segment, element> at each iteration"
        //Contract.Assert(leftCount + rightCount > old_leftCount + old_rightCount);

        old_leftCount = leftCount;
        old_rightCount = rightCount;

        Trace(unificationIteration, newLeftLimits, newLeftElements);
        Trace(unificationIteration, newRightLimits, newRightElements);

        unificationIteration++;

        // Case 1: Last element in both the lists
        if (leftCount == left.limits.Count - 1 && rightCount == right.limits.Count - 1)
        {
          Contract.Assume(leftCount >= 1);
          Contract.Assume(rightCount >= 1);

          // Simply add the elements
          newLeftLimits.Add(left.limits.GetLastElement());
          newRightLimits.Add(right.limits.GetLastElement());

          break;  // we are done!
        }

        // Case 2 : Last element of left, but still elements in right: we should refine left!
        if (leftCount == left.limits.Count - 1 && rightCount < right.limits.Count - 1)
        {
          Contract.Assume(leftCount >= 1);
          Contract.Assume(rightCount >= 1);

          // Try to split
          var leftLimit = left.limits[leftCount];
          var rightLimit = right.limits[rightCount];
          if (rightLimit.IsSubSetOf(leftLimit))
          {
            SplitSegment(right, left, neutralLeft, newRightLimits, newLeftLimits, newRightElements, newLeftElements, ref rightCount, ref leftCount);
          }
          else
          {
            bool limitRemoved;

            RefineSegmentation(operation == SegmentUnificationOperation.Widening, right, left,
              neutralLeft, newRightLimits, newLeftLimits, newRightElements, newLeftElements, ref rightCount, out limitRemoved);

            removedAtLeastOneLimitForRight = removedAtLeastOneLimitForRight || limitRemoved;
          }
          Contract.Assert(newLeftElements.Count == newRightElements.Count);
          Contract.Assert(newLeftLimits.Count == newRightLimits.Count);

          continue;
        }

        // Case 3: "Almost" the symmetric of 2
        if (leftCount < left.limits.Count - 1 && rightCount == right.limits.Count - 1)
        {
          Contract.Assume(leftCount >= 1);
          Contract.Assume(rightCount >= 1);

          // Try to split
          var leftLimit = left.limits[leftCount];
          var rightLimit = right.limits[rightCount];
          if (leftLimit.IsSubSetOf(rightLimit))
          {
            SplitSegment(left, right, neutralRight, newLeftLimits, newRightLimits, newLeftElements, newRightElements, ref leftCount, ref rightCount);
          }
          else if (!leftLimit.Intersection(rightLimit).IsEmpty)
          {
            var intersection = leftLimit.Intersection(rightLimit);
            left.limits[leftCount] = new SegmentLimit<Variable>(intersection, leftLimit.IsEmpty);
            right.limits[rightCount] = new SegmentLimit<Variable>(intersection, rightLimit.IsEmpty);

            left.limits.InsertOrAdd(leftCount+1, new SegmentLimit<Variable>(leftLimit.Difference(intersection), true));
            right.limits.InsertOrAdd(rightCount+1, new SegmentLimit<Variable>(rightLimit.Difference(intersection), true));

            if(leftCount <= left.elements.Count)
              left.elements.InsertOrAdd(leftCount, neutralLeft);

             if (rightCount <= right.elements.Count) 
              right.elements.InsertOrAdd(rightCount, neutralRight);

            newLeftLimits.Add(left.limits[leftCount]);
            newRightLimits.Add(right.limits[rightCount]);

            newLeftElements.Add(neutralLeft);
            newRightElements.Add(neutralRight);

            leftCount++;
            rightCount++;
          }
          else
          {
            bool removedALimitOnTheLeft;
            RefineSegmentation(operation == SegmentUnificationOperation.Widening, left, right,
              neutralRight, newLeftLimits, newRightLimits, newLeftElements, newRightElements, ref leftCount, out removedALimitOnTheLeft);
          }

          Contract.Assert(newLeftElements.Count == newRightElements.Count);
          Contract.Assert(newLeftLimits.Count == newRightLimits.Count);

          continue;
        }

        // If something goes wrong, we silently swallow it and continue
        if (leftCount >= left.limits.Count || rightCount >= right.limits.Count)
        {
          leftCount++;
          rightCount++;
          continue;
        }

        // Caching useful dictionary accesses
        var currentLeftLimits = left.limits[leftCount];
        var currentRightLimits = right.limits[rightCount];

        // F: Those assumptions may not be true, so we want to check at runtime
        Contract.Assume(leftCount < left.elements.Count);
        Contract.Assume(rightCount < right.elements.Count);

        var currentLeftElements = left.elements[leftCount];
        var currentRightElements = right.elements[rightCount];

        Contract.Assert(currentLeftLimits != null);
        Contract.Assert(currentRightLimits != null);

        // Case 4: 
        // Case 4.a  left.limits[leftCount] == right.limits[rightCount]
        if (SegmentLimit<Variable>.HaveTheSameElements(currentLeftLimits, currentRightLimits))
        {
          newLeftLimits.Add(currentLeftLimits);
          newRightLimits.Add(currentRightLimits);

          newLeftElements.Add(currentLeftElements);
          newRightElements.Add(currentRightElements);

          leftCount++;
          rightCount++;

          continue;
        }

        // Case 4.b left.limits[leftCount] < right.limits[rightCount] ==> We should split right
        if (currentLeftLimits.IsSubSetOf(currentRightLimits))
        {
          // < { a } P; { a, b } P'> --> < { a } P; { a } neutral { b }? P'>
          SplitSegment(left, right, neutralRight, newLeftLimits, newRightLimits,
            newLeftElements, newRightElements, ref leftCount, ref rightCount);

          Contract.Assert(newLeftElements.Count == newRightElements.Count);
          Contract.Assert(newLeftLimits.Count == newRightLimits.Count);
          Contract.Assert(right.limits.Count == right.elements.Count + 1);

          continue;
        }

        // Case 4.c left.limits[leftCount] > right.limts[rightCount] ==> We should split left
        if (currentRightLimits.IsSubSetOf(currentLeftLimits))
        {
          // < { a, b} P; { a } P' > --> < {a} neutral {b}? P; { a } P'>
          SplitSegment(right, left, neutralLeft, newRightLimits, newLeftLimits, 
            newRightElements, newLeftElements, ref rightCount, ref leftCount);

          Contract.Assert(newLeftElements.Count == newRightElements.Count);
          Contract.Assert(newLeftLimits.Count == newRightLimits.Count);
          Contract.Assert(right.limits.Count == right.elements.Count + 1);

          continue;
        }

        // Case 5 : We cannot unify, and so we have to abstract some of the limits

        // 5.a Keep only the intersection
        var commonLimits = currentLeftLimits.Intersection(currentRightLimits);
     
        if (!commonLimits.IsEmpty)
        {
          KeepOnlyIntersection(left, right, neutralLeft, neutralRight, newLeftLimits, newRightLimits, newLeftElements, newRightElements, 
            ref leftCount, ref rightCount, currentLeftLimits, currentRightLimits, currentLeftElements, currentRightElements, commonLimits);         

          continue;
        }

        Contract.Assume(leftCount >= 1);
        Contract.Assume(rightCount >= 1);

        // 5.b Try to keep some bounds
        if (rightCount < right.limits.Count - 1 && !(currentLeftLimits.Join(right.limits[rightCount + 1]).IsEmpty))
        {
          Contract.Assume(newRightElements.Count >= 1);

          JoinElementsAndMoveForward(newRightElements, ref rightCount, currentRightElements);
        }        
        else if (leftCount < left.limits.Count - 1 && !(left.limits[leftCount + 1].Join(currentRightLimits).IsEmpty))
        {
          Contract.Assume(newLeftElements.Count >= 1);

          JoinElementsAndMoveForward(newLeftElements, ref leftCount, currentLeftElements);
        }
        else 
        { // This the case in which we may have to drop both limits.
          // We try to refine precision by performing some lookahead
          
          // 1-lookahed
          if (currentLeftLimits.IsAtLeftOf(currentRightLimits))
          {
            Contract.Assume(newLeftElements.Count >= 1);

            JoinElementsAndMoveForward(newLeftElements, ref leftCount, currentLeftElements);

            continue;
          }

          // 1-lookahed
          if (currentRightLimits.IsAtLeftOf(currentLeftLimits))
          {
            Contract.Assume(newRightElements.Count >= 1);

            JoinElementsAndMoveForward(newRightElements, ref rightCount, currentRightElements);

            continue;
          }

          // If one of the two contains a constant, we try to keep it
          if (currentLeftLimits.ContainsConstant)
          {
            Contract.Assume(newRightElements.Count >= 1);

            JoinElementsAndMoveForward(newRightElements, ref rightCount, currentRightElements);

            continue;
          }

          if (currentRightLimits.ContainsConstant)
          {
            Contract.Assume(newLeftElements.Count >= 1);

            JoinElementsAndMoveForward(newLeftElements, ref leftCount, currentLeftElements);

            continue;
          }

          // else - no hope, let's just abstract the two limits
          Contract.Assume(newLeftElements.Count > 0);

          JoinElementsAndMoveForward(newLeftElements, ref leftCount, currentLeftElements);

          Contract.Assume(newRightElements.Count > 0);

          JoinElementsAndMoveForward(newRightElements, ref rightCount, currentRightElements);
        }
        continue;
      }

      if ((newLeftLimits.Count > 0 && newLeftLimits.GetLastElement().IsEmpty) || (newRightLimits.Count > 0 && newRightLimits.GetLastElement().IsEmpty))
      {
        // We want this program point to be unreached, but in order to make the implemetation robust, 
        // if we get a malformed list of limits, were the last limit is empty we just return fail
        if (ArrayOptions.Trace)
        {
          Console.WriteLine("*** Internal error in Unification. Giving up and returning top");
        }

        leftReduced = rightReduced = null;

        return false;
      }
      else
      {
        Contract.Assume(newLeftLimits.Count == newLeftElements.Count + 1);
        Contract.Assume(newRightLimits.Count == newRightElements.Count + 1);

        leftReduced = new ArraySegmentation<AbstractDomain, Variable, Expression>(false,
          newLeftLimits, newLeftElements, this.bottomElement, this.expManager);
        rightReduced = new ArraySegmentation<AbstractDomain, Variable, Expression>(false,
          newRightLimits, newRightElements, this.bottomElement, this.expManager);

        Contract.Assume(leftReduced.elements.Count > 0);
        Contract.Assert(leftReduced.limits.Count == rightReduced.limits.Count);
      }

      if (ArrayOptions.Trace)
      {
        Console.WriteLine("After Unification:\n{0}\n{1}", leftReduced.ToString(), rightReduced.ToString());
      }

      return true;
    }

    private static void RefineSegmentation(bool isWidening,
      ArraySegmentation<AbstractDomain, Variable, Expression> refiningSegmentation, 
      ArraySegmentation<AbstractDomain, Variable, Expression> refineeSegmentation,
      AbstractDomain neutralElement, 
      NonNullList<SegmentLimit<Variable>> newLimitsForRefinee, NonNullList<SegmentLimit<Variable>> newLimitsForRefining, 
      NonNullList<AbstractDomain> newElementsForRefinee, NonNullList<AbstractDomain> newElementsForRefining, 
      ref int count, out bool removedAtLeastOneLimit)
    {
      #region Contract

      Contract.Requires(neutralElement != null);
      Contract.Requires(refiningSegmentation != null);
      Contract.Requires(refineeSegmentation != null);
      Contract.Requires(newLimitsForRefinee != null);
      Contract.Requires(newLimitsForRefining != null);
      Contract.Requires(newElementsForRefinee != null);
      Contract.Requires(newElementsForRefining != null);

      Contract.Requires(newElementsForRefinee.Count == newElementsForRefining.Count);
      Contract.Requires(newLimitsForRefinee.Count == newLimitsForRefining.Count);

      Contract.Requires(count >= 1);

      Contract.Requires(newElementsForRefinee.Count >= 1);

      Contract.Ensures(count >= Contract.OldValue(count));
      Contract.Ensures(newLimitsForRefinee.Count == newLimitsForRefining.Count);

      #endregion

      removedAtLeastOneLimit = false;

      // REDUNDANT inferred by CC
      /*
      var lastElementForRefining = newElementsForRefining.Count > 0
        ? newElementsForRefining.GetLastElement()
        : neutralElement;
      */

      var lastElementForRefining = newElementsForRefining.GetLastElement();

      var old_count = count;

      // F: this is the object invariant of refininingSegmentation, that we do not assume here
      Contract.Assume(refiningSegmentation.elements != null);
      Contract.Assume(refiningSegmentation.limits != null);
      Contract.Assume(refineeSegmentation.limits != null);
      Contract.Assume(refiningSegmentation.elements.Count  == refiningSegmentation.limits.Count -1);

      for (int i = count; i < refiningSegmentation.limits.Count - 1; i++)
      {
        var limit = refiningSegmentation.limits[i];

        if (isWidening)
        {
          limit = limit.KeepOnlyConstantExpressionIfAny();
        }

        if (!limit.IsEmpty && !limit.IsConditional && !isWidening)
        {          
          // Update the refining
          newLimitsForRefining.Add(limit);
          newElementsForRefining.Add(refiningSegmentation.elements[i]);
          // Update the refinee
          if (count <= refineeSegmentation.limits.Count - 1 && !limit.Intersection(refineeSegmentation.limits[count]).IsEmpty)
          {
            newLimitsForRefinee.Add(new SegmentLimit<Variable>(limit, true));
            newElementsForRefinee.Add(neutralElement);

            var newPostLimit = refineeSegmentation.limits[count].Difference(limit);
            refineeSegmentation.limits[count] = new SegmentLimit<Variable>(newPostLimit, true);
          }
          else
          {
            newLimitsForRefinee.Add(limit);
            newElementsForRefinee.Add(lastElementForRefining);
          }
        }
        else
        {
          // If we reached this branch, it means that we abstracted away the limit
          removedAtLeastOneLimit = true;

          var lastElementIndex = Math.Max(0, newElementsForRefinee.Count - 1);
          newElementsForRefinee[lastElementIndex] = (AbstractDomain)newElementsForRefinee[lastElementIndex].Join(refiningSegmentation.elements[i]);
        }
        count++;
      }

      Contract.Assert(old_count <= count); // F: we need the assert here to help Clousot proving the postcondition
    }

    [Pure]
    private static void JoinElementsAndMoveForward(
      NonNullList<AbstractDomain> newElements, ref int count, AbstractDomain currentElements)
    {
      Contract.Requires(count >= 1);
      Contract.Requires(newElements != null);
      Contract.Requires(newElements.Count >= 1);

      Contract.Ensures(count == Contract.OldValue(count) +1);

      var pos = Math.Min(count - 1, newElements.Count - 1); // We take the min to handle the case for the last element

      newElements[pos] = (AbstractDomain)currentElements.Join(newElements[pos]);

      count++;
    }

    private static void KeepOnlyIntersection(
      ArraySegmentation<AbstractDomain, Variable, Expression> left, ArraySegmentation<AbstractDomain, Variable, Expression> right, 
      AbstractDomain neutralLeft, AbstractDomain neutralRight, 
      NonNullList<SegmentLimit<Variable>> newLeftLimits, NonNullList<SegmentLimit<Variable>> newRightLimits, 
      NonNullList<AbstractDomain> newLeftElements, NonNullList<AbstractDomain> newRightElements, 
      ref int leftCount, ref int rightCount, 
      SegmentLimit<Variable> currentLeftLimits, SegmentLimit<Variable> currentRightLimits, 
      AbstractDomain currentLeftElements, AbstractDomain currentRightElements, 
      Set<NormalizedExpression<Variable>> commonLimits)
    {
      #region Contracts 
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      Contract.Requires(neutralLeft != null);
      Contract.Requires(neutralRight != null);
      Contract.Requires(commonLimits != null);
      Contract.Requires(currentLeftLimits != null);
      Contract.Requires(currentRightLimits != null);
      Contract.Requires(currentLeftElements != null);
      Contract.Requires(currentRightElements != null);
      Contract.Requires(newLeftLimits != null);
      Contract.Requires(newRightLimits != null);
      Contract.Requires(newLeftElements != null);
      Contract.Requires(newRightElements != null);

      Contract.Requires(leftCount >= 0);
      Contract.Requires(rightCount >= 0);

      // F: Those two are because we do not assume invariants for parameters
      Contract.Requires(left.elements != null);
      Contract.Requires(right.elements != null);

      Contract.Requires(leftCount < left.elements.Count);
      Contract.Requires(rightCount < right.elements.Count);

      Contract.Ensures(leftCount == Contract.OldValue(leftCount) + 1);
      Contract.Ensures(rightCount == Contract.OldValue(rightCount) + 1);

      #endregion

      newLeftLimits.Add(new SegmentLimit<Variable>(commonLimits, currentLeftLimits.IsConditional));
      newRightLimits.Add(new SegmentLimit<Variable>(commonLimits, currentRightLimits.IsConditional));

      newLeftElements.Add(neutralLeft);
      newRightElements.Add(neutralRight);

      var diffLeft = currentLeftLimits.Difference(commonLimits);
      var diffRight = currentRightLimits.Difference(commonLimits);

      // Small optimization: we increment here the pointers 
      // instead that at the end of the method, as we need to insert a new pair <limit, element>
      leftCount++;
      rightCount++;

      // F: Adding those two assumptions, as we do not materialize the object invariants for parameters
      Contract.Assume(left.limits != null);
      Contract.Assume(right.limits != null);
      Contract.Assume(left.limits.Count == left.elements.Count + 1);
      Contract.Assume(right.limits.Count == right.elements.Count + 1);

      Contract.Assert(leftCount <= left.elements.Count);

      left.limits.InsertOrAdd(leftCount, new SegmentLimit<Variable>(diffLeft, true));

      Contract.Assert(leftCount <= left.elements.Count);

      left.elements.InsertOrAdd(leftCount, currentLeftElements);

      right.limits.InsertOrAdd(rightCount, new SegmentLimit<Variable>(diffRight, true));
      right.elements.InsertOrAdd(rightCount, currentRightElements);
    }

     
    private static void SplitSegment(
      ArraySegmentation<AbstractDomain, Variable, Expression> splitter, ArraySegmentation<AbstractDomain, Variable, Expression> splittee, 
      AbstractDomain splitteeNeutralElement,
      NonNullList<SegmentLimit<Variable>> newSplitterLimits, NonNullList<SegmentLimit<Variable>> newSplitteeLimits, 
      NonNullList<AbstractDomain> newSplitterElements, NonNullList<AbstractDomain> newSplitteeElements, 
      ref int splitterCount, ref int splitteeCount)
    {
      #region Contracts

      Contract.Requires(splitter != null);
      Contract.Requires(splittee != null);

      Contract.Requires(splitter.limits != null);
      Contract.Requires(splittee.limits != null);

      Contract.Requires(splitter.elements != null);
      Contract.Requires(splittee.elements != null);
      
      Contract.Requires(splitteeNeutralElement != null);
      
      Contract.Requires(newSplitterLimits != null);
      Contract.Requires(newSplitteeLimits != null);
      Contract.Requires(newSplitterElements != null);
      Contract.Requires(newSplitteeElements != null);

      Contract.Requires(splitterCount >= 0);
      Contract.Requires(splitterCount < splitter.elements.Count);

      Contract.Requires(splitteeCount >= 0);
      
      Contract.Requires(splitteeCount < splittee.limits.Count);

      Contract.Requires(newSplitterLimits.Count == newSplitteeLimits.Count);
      Contract.Requires(newSplitterElements.Count == newSplitteeElements.Count);

      Contract.Requires(splitter.limits.Count == splitter.elements.Count + 1);

      Contract.Ensures(newSplitterLimits.Count == newSplitteeLimits.Count);
      Contract.Ensures(newSplitterElements.Count == newSplitteeElements.Count);

      Contract.Ensures(Contract.ValueAtReturn(out splitterCount) == Contract.OldValue(splitterCount) + 1);
      Contract.Ensures(Contract.ValueAtReturn(out splitteeCount) == Contract.OldValue(splitteeCount) + 1);

      Contract.Ensures(splittee.limits.Count == Contract.OldValue(splittee.limits.Count) + 1);
      Contract.Ensures(splittee.elements.Count <= Contract.OldValue(splittee.elements.Count) + 1);

      #endregion      
      
      var common = splitter.limits[splitterCount];

      Contract.Assert(common != null);

      var reminder = splittee.limits[splitteeCount].Difference(common);

      // 1. Add the common limits
      newSplitterLimits.Add(common);
      newSplitteeLimits.Add(new SegmentLimit<Variable>(common, splitterCount != 0 ? true : false));

      // 2. Add the elements
      newSplitterElements.Add(splitter.elements[splitterCount]);

      newSplitteeElements.Add(splitteeNeutralElement);

      // 3. Increase the pointers (small optimization)
      splitterCount++;
      splitteeCount++;

      //Contract.Assert(splitteeCount < splittee.elements.Count);

      // 4. Push the reminder limit and the element onto the splittee
      splittee.limits.InsertOrAdd(splitteeCount, new SegmentLimit<Variable>(reminder, true));

        // We check it because it can be the last element in the sequence, so there is nothing to do then
      if (splitteeCount - 1 < splittee.elements.Count)
      {
        splittee.elements.InsertOrAdd(splitteeCount, splittee.elements[splitteeCount - 1]);
      }
    }

    #endregion

    #region Segmentation inclusions

    /// <summary>
    /// Check if this segmentation is included in the other.
    /// This is useful for the meet (and LessEqual) because the segment unification can be too rough, and throw away segments in the meet
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    [Pure]
    private bool IsIncludedIn(ArraySegmentation<AbstractDomain, Variable, Expression> other)
    {
      Contract.Requires(other != null);

      if (this.IsBottom || other.IsTop)
      {
        return true;
      }

      if (other.IsBottom)
      {
        return false;
      }

      Contract.Assume(this.IsNormal);

      // We can start from 1, as we know that '0' is in the first limit of all the segmentations
      var lastI = 0;
      var lastJ = 0;

      Contract.Assume(other.limits != null, "assuming object invariant");
      Contract.Assume(other.IsNormal, "assuming object invariant");

      for (var j = 1; j < other.limits.Count; j++)
      {
        // the limit other.limits[i] should be contained in one of this limits
        for (var i = lastI+1; i < this.limits.Count; i++)
        {
          Contract.Assert(this.limits.Count == this.elements.Count + 1);
          Contract.Assert(other.limits.Count == other.elements.Count +1);

          var intersection = this.limits[i].Intersect(other.limits[j]);
          if (intersection.Any())
          {
            var thisElements = this.JoinValuesInASubrange(lastI, i);
            var otherElements = other.JoinValuesInASubrange(lastJ, j);

            if (thisElements.LessEqual(otherElements))
            {
              lastJ = j;
              lastI = i;
              goto NextLimit;
            }
            else
            {
              return false;
            }
          }
        }

        return false;
      NextLimit: ;
      }

      return true;
    }

    #endregion

    #region Smash all the values in the segment
    [Pure]     
    public ArraySegmentation<AbstractDomain, Variable, Expression> SmashAndWeakUpdateWith(AbstractDomain valueIntv)
    {
      Contract.Requires(valueIntv != null);

      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);

      if (this.limits.Count <= 1)
      {
        return this.Top;
      }

      var join = valueIntv;

      foreach (var x in this.elements)
      {
        Contract.Assume(x != null); // F: TODO the contract on the NonNullist GetEnumerator

        join = (AbstractDomain)join.Join(x);

        Contract.Assert(join != null);
      }

      return new ArraySegmentation<AbstractDomain, Variable, Expression>(
        this.limits[0], join, this.limits[this.limits.Count-1],
        this.bottomElement, this.expManager);
    }
    #endregion

    #region JoinAllTheValues

    public AbstractDomain JoinAll(AbstractDomain bottom)
    {
      #region Contract

      Contract.Requires(bottom != null);

      Contract.Ensures(Contract.Result<AbstractDomain>() != null);

      #endregion

      if (this.IsEmptyArray || this.IsTop)
      {
        return (AbstractDomain) bottom.Top;
      }
      if (this.IsBottom)
      {
        return bottom;
      }

      var result = bottom;
      foreach (var element in this.elements)
      {
        result = (AbstractDomain) result.Join(element);
      }

      return result;
    }

    #endregion

    #region ToString

    public override string ToString()
    {
      if (this.IsEmptyArray)
        return "Empty array";
      if (this.IsBottom)
        return "Bottom (_|_)";
      if (this.IsTop)
        return "Top";

      // F: making it an assume until we have a better handling of disjunctions in object-invariants
      Contract.Assume(this.limits.Count == this.elements.Count + 1);

      var result = new StringBuilder();

      if (/*ArrayOptions.Trace*/ true)
      {
        result.AppendFormat("#{0}", this.Id);
      }

      result.AppendFormat("{0} ", ToString(limits, elements));

      return result.ToString();
    }
     
    static private string ToString(NonNullList<SegmentLimit<Variable>> limits, NonNullList<AbstractDomain> elements)
    {
      Contract.Requires(limits != null);
      Contract.Requires(elements != null);
      Contract.Requires(limits.Count - elements.Count >= 0);
      Contract.Requires(1 >= limits.Count - elements.Count);

      if (limits.Count == 0)
      {
        return "Empty";
      }

      var result = new StringBuilder();
      result.AppendFormat("{0} ", limits[0].ToString());

      for (var i = 1; i < limits.Count; i++)
      {
        Contract.Assert(i - 1 < limits.Count);
        Contract.Assert(elements.Count <= limits.Count);
        Contract.Assert(i - 1 < elements.Count); 

        result.AppendFormat("{0} ", elements[i - 1]);
        result.AppendFormat("{0} ", limits[i]);
      }

      return result.ToString();
    }

    [Pure]
    [ContractVerification(false)]
    static private void Trace(int iteration, NonNullList<SegmentLimit<Variable>> limits, NonNullList<AbstractDomain> elements)
    {
      Contract.Requires(limits != null);
      Contract.Requires(elements != null);

      if (ArrayOptions.Trace)
      {
        Console.WriteLine(iteration+ " : " + ToString(limits, elements));
      }
    }
    #endregion

    #region TestTue and TestFalse

    private class ArraySegmentationTestTrueVisitor
      : TestTrueVisitor<ArraySegmentation<AbstractDomain, Variable, Expression>, Variable, Expression>
    {

      public ArraySegmentationTestTrueVisitor(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
      }

      #region Implementation of overridden
      public override ArraySegmentation<AbstractDomain, Variable, Expression> VisitEqual(Expression left, Expression right, Expression original, ArraySegmentation<AbstractDomain, Variable, Expression> data)
      {
        return data;
      }

      public override ArraySegmentation<AbstractDomain, Variable, Expression> VisitLessEqualThan(Expression left, Expression right, Expression original, ArraySegmentation<AbstractDomain, Variable, Expression> data)
      {
        return data;
      }

      public override ArraySegmentation<AbstractDomain, Variable, Expression> VisitLessThan(Expression left, Expression right, Expression original, ArraySegmentation<AbstractDomain, Variable, Expression> data)
      {
        if (data != null)
        {
          NormalizedExpression<Variable> leftNorm, rightNorm;
          var isLeftOk = NormalizedExpression<Variable>.TryConvertFrom(left, this.Decoder, out leftNorm);
          var isRightOk = NormalizedExpression<Variable>.TryConvertFrom(right, this.Decoder, out rightNorm);

          if (isLeftOk && isRightOk)
          {
            return data.TestTrueLessThan(leftNorm, rightNorm);
          }
        }

        return data;
      }

      public override ArraySegmentation<AbstractDomain, Variable, Expression> VisitNotEqual(Expression left, Expression right, Expression original, ArraySegmentation<AbstractDomain, Variable, Expression> data)
      {
        return data;
      }

      public override ArraySegmentation<AbstractDomain, Variable, Expression> VisitVariable(Variable var, Expression original, ArraySegmentation<AbstractDomain, Variable, Expression> data)
      {
        return data;
      }
      #endregion
    }

    private class ArraySegmentationTestFalseVisitor
      : TestFalseVisitor<ArraySegmentation<AbstractDomain, Variable, Expression>, Variable, Expression>
    {

      public ArraySegmentationTestFalseVisitor(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
      }

      public override ArraySegmentation<AbstractDomain, Variable, Expression> VisitVariable(Variable variable, Expression original, ArraySegmentation<AbstractDomain, Variable, Expression> data)
      {
        return data;
      }
    }

    #endregion

    #region Atomic handling for Array.Copy
    [Pure]
    public ArraySegmentation<AbstractDomain, Variable, Expression>
      CopyFrom
      (ArraySegmentation<AbstractDomain, Variable, Expression> sourceSegmentation, Expression length,
      INumericalAbstractDomainQuery<Variable, Expression> oracle)
    {
      #region Contracts
      Contract.Requires(sourceSegmentation != null);
      Contract.Requires(oracle != null);
      Contract.Ensures(Contract.Result<ArraySegmentation<AbstractDomain, Variable, Expression>>() != null);
      #endregion

      if (this.IsEmptyArray || this.IsBottom)
      {
        return this;
      }

      if (this.IsTop)
      {
        return new ArraySegmentation<AbstractDomain, Variable, Expression>(sourceSegmentation);
      }

      // Let's search for the upper bound

      var lengthNormalized = NormalizedExpression<Variable>.For(this.expManager.Decoder.UnderlyingVariable(length));

      var i = 0;
      bool strict = false, equal = false;
      for (; i < this.limits.Count; i++)
      {
        if (LessEqual(this.limits[i], lengthNormalized, oracle, out equal, out strict))
        {
          // ok
        }
        else
        {
          break;
        }
      }

      if (i == 0)
      {
        return this.Top;
      }

      Contract.Assume(sourceSegmentation.limits != null);
      Contract.Assume(sourceSegmentation.elements != null);
      Contract.Assume(sourceSegmentation.limits.Count == sourceSegmentation.elements.Count + 1);

      var newLimits = new NonNullList<SegmentLimit<Variable>>();
      var newElements = new NonNullList<AbstractDomain>();

      // Add all the limits
      var count = 0;
      foreach (var limit in sourceSegmentation.limits)
      {
        Contract.Assume(limit != null);
        newLimits.Add(limit);
        count++;
      }
      Contract.Assert(count == newLimits.Count);
      Contract.Assume(newLimits.Count == sourceSegmentation.limits.Count); // F: cannot prove because we miss the relation between the iterator and the original # of elements

      // Add all the abstract values
      count = 0;
      foreach (var el in sourceSegmentation.elements)
      {
        Contract.Assume(el != null);
        newElements.Add(el);
        count++;
      }
      Contract.Assert(count == newElements.Count);
      Contract.Assume(newElements.Count== sourceSegmentation.elements.Count);

      Contract.Assert(newElements.Count + 1== newLimits.Count);
      if (!equal)
      {
        // Add whatever remains here
        for (var j = i; j < this.limits.Count; j++)
        {
          newLimits.Add(this.limits[j]);
        }
        for (var j = Math.Max(0, i-1); j < this.elements.Count; j++)
        {
          newElements.Add(this.elements[j]);
        }
        Contract.Assume(newLimits.Count == newElements.Count + 1);
      }
      return new ArraySegmentation<AbstractDomain, Variable, Expression>(newLimits, newElements, this.bottomElement, this.expManager);
    }
    #endregion

    #region PrettyPrinting

    private const string ForAllTemplate = "Contract.Forall({0}, {1}, {2})";

    [ContractVerification(false)]
    public List<string> PrettyPrint(string arrayName, 
      Func<Variable, string> variableNamePrettyPrinter,
      Func<AbstractDomain, string> abstractElementPrettyPrinter)
    {
      Contract.Requires(arrayName != null);
      Contract.Requires(variableNamePrettyPrinter != null);
      Contract.Requires(abstractElementPrettyPrinter != null);

      Contract.Ensures(Contract.Result<List<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<List<string>>(), x => x != null));

      var result = new List<string>();

      for (var i = 0; i < this.limits.Count-1; i++)
      {
        string inf = null, sup = null;

        foreach (var l in this.limits[i])
        {
          inf = l.PrettyPrint(variableNamePrettyPrinter);
          if (inf != null)
            break;
        }

        foreach (var u in this.limits[i + 1])
        {
          sup = u.PrettyPrint(variableNamePrettyPrinter);
          if (sup != null)
            break;
        }

        if (inf != null && sup != null)
        {
          var arrayElement = abstractElementPrettyPrinter(this.elements[i]);
          if (arrayElement != null)
          {
            result.Add(String.Format(ForAllTemplate, inf, sup, arrayElement));
          }
        }
      }
      return result;
    }

    #endregion

    #region To
    public T To<T>(IFactory<T> factory)
    {
      if (this.IsEmptyArray || !this.IsNormal)
      {
        return factory.IdentityForAnd;
      }

      var result = new List<T>();

      for (var i = 0; i < this.limits.Count - 1; i++)
      {
        Contract.Assert(this.limits.Count == this.elements.Count + 1);

        T inf = default(T), sup = default(T);
        bool infOk = false, supOk = false;

        foreach (var l in this.limits[i])
        {
          if (l == null)
          {
            return factory.IdentityForAnd;
          }
          if (l.TryPrettyPrint(factory, out inf))
          {
            infOk = true;
            break;
          }
        }

        if (!infOk)
        {
          continue;
        }

        foreach (var u in this.limits[i + 1])
        {
          Contract.Assume(u != null);
          if (u.TryPrettyPrint(factory, out sup))
          {
            // REDUNDANT inferred by CC
            /*
            if (u == null)
            {
              return factory.IdentityForAnd;
            }
            */
            supOk = true;
            break;
          }
        }

        Contract.Assert(infOk);

        if (supOk)
        {
          if (sup == null || inf == null)
          {
            // "invariant: supOk ==> sup != null"

            return factory.IdentityForAnd;
          }

          var body = this.elements[i].To(factory);
          if (!object.Equals(body, default(T)) && !object.Equals(body, factory.IdentityForAnd))
          {
            foreach (var splitted in factory.SplitAnd(body))
            {
              if (splitted == null)
              {
                return factory.IdentityForAnd;
              }

              var forall = factory.ForAll(inf, sup, splitted);
              result.Add(forall);
            }
          }
        }
      }

      var formula = default(T);
      var first = true;
      foreach (var f in result)
      {
        if (first)
        {
          formula = f;
          first = false;
        }
        else
        {
          formula = factory.And(formula, f);
        }
      }

      return formula;
    }

    #endregion    
  }

  #region Search Info structure
  public struct SearchInfo
  {
    public int Low;
    public int Upp;
    public int UppMerge;
    public bool IsLowerBoundEqual;
    public bool IsLowerBoundStrict;
    public bool IsLowerBoundStrictlySmallerThanUpp;
    public bool IsUpperBoundEqual;
    public bool IsUpperBoundStrict;

    public SearchInfo(int low, int upp, int uppMerge, 
      bool isLowerBoundEqual, bool isLowerBoundStrict, bool isLowerBoundStrictlySmallerThanUpp,
      bool isUpperBoundEqual, bool isUpperBoundStrict)
    {
      this.Low = low;
      this.Upp = upp;
      this.UppMerge = uppMerge;
      this.IsLowerBoundEqual = isLowerBoundEqual;
      this.IsLowerBoundStrict = isLowerBoundStrict;
      this.IsLowerBoundStrictlySmallerThanUpp = isLowerBoundStrictlySmallerThanUpp;
      this.IsUpperBoundEqual = isUpperBoundEqual;
      this.IsUpperBoundStrict = isUpperBoundStrict;
    }

    public override string ToString()
    {
      var result = new StringBuilder();

      result.AppendFormat("Low : {0} ", Low);
      result.AppendFormat("Upp : {0} ", Upp);
      result.AppendFormat("UppMerge : {0} ", UppMerge);
      result.AppendFormat("IsLowerBoundEqual: {0} ", IsLowerBoundEqual);
      result.AppendFormat("IsLowerBoundStrictlySmallerThanUpp: {0} ", IsLowerBoundStrictlySmallerThanUpp);
      result.AppendFormat("IsUpperBoundStrict: {0} ", IsUpperBoundStrict);
      result.AppendFormat("IsUpperBoundEqual: {0} ", IsUpperBoundEqual);
      result.AppendFormat("IsUpperBoundStrict: {0}\n", IsUpperBoundStrict);

      return result.ToString();
    }
  }
  #endregion
}

