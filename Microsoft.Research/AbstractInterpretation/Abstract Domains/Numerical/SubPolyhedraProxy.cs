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
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  public enum Reduction { Simplex, Fast, Complete }
  public enum Hints { Standard, ConvexHull2D, Octagons, CHOct }

  // A proxy for the subpolyhedra
  public class SubPoly<Expression>
  {
    private SubPolyhedra<Expression> subpoly;

    /// <summary>
    /// reduction on "assume" statements ?
    /// </summary>
    public static bool StrongPrecision
    {
      set
      {
        SubPolyhedra<Expression>.StrongPrecision = value;
      }
    }
    
    /// <summary>
    /// The algorithm for inferring tighter intervals
    /// </summary>
    public static Reduction Algorithm
    {
      set
      {
        SubPolyhedra<Expression>.Algorithm = To(value);
      }
    }
    /// <summary>
    ///  what constraints we guess at the join
    /// </summary>
    public static Hints Inference
    {
      set
      {
        SubPolyhedra<Expression>.Inference= To(value);
      }
    }

    public SubPoly(IExpressionDecoder<Expression> decoder, IExpressionEncoder<Expression> encoder)
    {
      IntervalEnvironment<Expression> intv = new IntervalEnvironment<Expression>(decoder, encoder);
      LinearEqualitiesEnvironment<Expression> lineq = new LinearEqualitiesEnvironment<Expression>(decoder, encoder, true, false);

      this.subpoly = new SubPolyhedra<Expression>(lineq, intv, decoder, encoder);
    }

    private SubPoly(IAbstractDomain sp)
    {
      this.subpoly = sp as SubPolyhedra<Expression>;
      Debug.Assert(sp != null);
    }

    public void Assign(Expression x, Expression exp)
    {
      if (this.subpoly.IsBottom)
        return;

      this.subpoly.Assign(x, exp);
    }

    public SubPoly<Expression> TestTrueGeqZero(Expression exp)
    {
      return new SubPoly<Expression>(this.subpoly.TestTrueGeqZero(exp));
    }

    public SubPoly<Expression> TestTrueLessThan(Expression exp1, Expression exp2)
    {
      return new SubPoly<Expression>(this.subpoly.TestTrueLessThan(exp1, exp2));
    }

    public SubPoly<Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2)
    {
      return new SubPoly<Expression>(this.subpoly.TestTrueLessEqualThan(exp1, exp2));
    }
   
    public bool IsBottom
    {
      get 
      {
        return this.subpoly.IsBottom;
      }
    }

    public bool IsTop
    {
      get { return this.subpoly.IsTop; }
    }

    public bool LessEqual(SubPoly<Expression> right)
    {
      return this.subpoly.LessEqual(right.subpoly);
    }

    public SubPoly<Expression> Bottom
    {
      get { return new SubPoly<Expression>(this.subpoly.Bottom); }
    }

    public SubPoly<Expression> Top
    {
      get { return new SubPoly<Expression>(this.subpoly.Top); }
    }

    public SubPoly<Expression> Join(SubPoly<Expression> right)
    {
      return new SubPoly<Expression>(this.subpoly.Join(right.subpoly));
    }

    public SubPoly<Expression> Meet(SubPoly<Expression> right)
    {
      return new SubPoly<Expression>(this.subpoly.Meet(right.subpoly));
    }

    public SubPoly<Expression> Widening(SubPoly<Expression> prev)
    {
      return new SubPoly<Expression>(this.subpoly.Widening(prev.subpoly));
    }

    public T To<T>(IFactory<T> factory)
    {
      return this.subpoly.To(factory);
    }

    public SubPoly<Expression> Duplicate()
    {
      return new SubPoly<Expression>((IAbstractDomain)this.subpoly.Clone());
    }

    public System.Collections.Generic.ISet<Expression> Variables
    {
      get { return this.subpoly.Variables; }
    }

    public void AddVariable(Expression var)
    {
     this.subpoly.AddVariable(var);
    }

    public void ProjectVariable(Expression var)
    {
      this.subpoly.ProjectVariable(var);
    }

    public void RemoveVariable(Expression var)
    {
      this.subpoly.RemoveVariable(var);
    }

    public void RenameVariable(Expression OldName, Expression NewName)
    {
      this.subpoly.RenameVariable(OldName, NewName);
    }

    public SubPoly<Expression> AssumeTrue(Expression guard)
    {
      return new SubPoly<Expression>(this.subpoly.TestTrue(guard));
    }

    public SubPoly<Expression> AssumeFalse(Expression guard)
    {
      return new SubPoly<Expression>(this.subpoly.TestFalse(guard));
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      return this.subpoly.CheckIfHolds(exp);
    }

    public Interval BoundsFor(Expression v)
    {
      return this.subpoly.BoundsFor(v);
    }

    public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      return this.subpoly.CheckIfHolds(exp);
    }

    public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      return this.subpoly.CheckIfLessThan(e1, e2);
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      return this.subpoly.CheckIfLessEqualThan(e1, e2);
    }

    public FlatAbstractDomain<bool> CheckIfNonZero(Expression e)
    {
      return this.subpoly.CheckIfNonZero(e);
    }


    private static SubPolyhedra<Expression>.JoinConstraintInference To(Hints h)
    {
      switch (h)
      {
        case Hints.CHOct:
          return SubPolyhedra<Expression>.JoinConstraintInference.CHOct;

        case Hints.ConvexHull2D:
          return SubPolyhedra<Expression>.JoinConstraintInference.ConvexHull2D;

        case Hints.Octagons:
          return SubPolyhedra<Expression>.JoinConstraintInference.Octagons;

        case Hints.Standard:
          return SubPolyhedra<Expression>.JoinConstraintInference.Standard;

        default:
          throw new InvalidOperationException();
      }
    }

    private static SubPolyhedra<Expression>.ReductionAlgorithm To(Reduction r)
    {
      switch (r)
      {
        case Reduction.Complete:
          return SubPolyhedra<Expression>.ReductionAlgorithm.Complete;

        case Reduction.Fast:
          return SubPolyhedra<Expression>.ReductionAlgorithm.Fast;

        case Reduction.Simplex:
          return SubPolyhedra<Expression>.ReductionAlgorithm.Simplex;

        default:
          throw new InvalidOperationException();
      }
    }

    public override string ToString()
    {
      return this.subpoly.ToString();
    }
  }
}
