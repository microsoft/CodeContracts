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

// This file contains the part of the class OctagonEnvironment<Variable, Expression> that handles the inference of constraints

using System;
using System.Collections.Generic;
using System.Diagnostics;   
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  public sealed partial class OctagonEnvironment<Variable, Expression>
  {

    #region Assignment

    /// <summary>
    /// Perform the assignment in the abstract environment.
    /// First, it simplifies the pure expression <code>exp</code>.
    /// Then, if it is an assignment that can be handled by the domain, it handles it directly (both for invertible and not-invertible assignments).
    /// Otherwise, it uses the abstract domain of Intervals to compute un upper and lower bound for <code>x</code>
    /// </summary>
    public void Assign(Expression x, Expression/*!*/ exp)
    {
      //if (base.Dimensions > 50)
       // Console.Write("{0};", base.Dimensions);

      var decoder = this.expManager.Decoder;

      if (Precision == OctagonPrecision.FullPrecision)
      {
        // 1. Simplify exp
        PolynomialOfInt<Variable, Expression> polyExp;
        if (!PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(exp, decoder, out polyExp))
        {
          AssignWithIntervals(x, exp);

          return;
        }

        // 2. Determine the abstract domain that must handle the assignment

        // 2.1 It is an octagon constraint, so we are happy and we handle the assignment precisely
        if (polyExp.IsLinearOctagon && ! polyExp.Relation.HasValue)
        {
          AssignWithOctagons(x, polyExp);
        }
        // 2.2 It is not an octagon constraint, so we approximate its value
        else
        {
          AssignWithIntervals(x, polyExp);
        }
      }
      else
      {
        var xVar = decoder.UnderlyingVariable(x);
        this.ProjectVariable(xVar);
      }
    }

    public void Assign(Expression x, Expression/*!*/ exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      var watch = new Stopwatch();
      watch.Start();

      CheckConsistency();

      if (Precision == OctagonPrecision.FullPrecision)
      {
        // 1. Infers the "simple" constraints

        var NewConstraints = new Set<Expression>();

        // Discover new constraints
        foreach (var constraintsFetcher in this.constraintsForAssignment)
        {
          NewConstraints.AddRange(constraintsFetcher.InferConstraints(x, exp, preState));
        }

        // Assume them
        foreach (var newConstraint in NewConstraints)
        {
          this.TestTrue(newConstraint);
        }

        // 2. Perform the usual assignment
        this.Assign(x, exp);
      }
      else
      {
        var decoder = this.expManager.Decoder;

        var xVar = decoder.UnderlyingVariable(x);
        this.ProjectVariable(xVar);
      }

      CheckConsistency();

      #if  TRACE_AD_PERFORMANCES
      AddTimeElapsedFromStart("Assign", ref timeSpentInAssignments, watch.Elapsed);
      #endif
    }

    [Conditional("VERBOSE")]
    new void CheckConsistency()
    {
      base.CheckConsistency();

      foreach (var x in this.variables2dimensions.Keys)
      {
        Contract.Assert(!this.availableDimensions.Contains(this.variables2dimensions[x]), string.Format("Incosistency detected for the dimension {0} (i.e. the variable {1})", this.variables2dimensions[x], x));
      }

      foreach (var i in this.AllocatedDimensions)
      {
        Contract.Assert(this.variables2dimensions.InverseMap.ContainsKey(i));
      }
    }

    #endregion

    #region TestTrue and TestFalse
    public IAbstractDomainForEnvironments<Variable, Expression>/*!*/ TestTrue(Expression/*!*/ guard)
    {
      CheckConsistency();

      var result = this.trueTestVisitor.Visit(guard, this);

      CheckConsistency();

      return result;
    }

    public OctagonEnvironment<Variable, Expression>/*!*/ TestTrueNotRefining(Expression/*!*/ guard)
    {
      CheckConsistency();

      var result = this.trueTestNotRefiningVisitor.Visit(guard, this);

      CheckConsistency();

      return result;
    }

    public IAbstractDomainForEnvironments<Variable, Expression>/*!*/ TestFalse(Expression/*!*/ guard)
    {
      CheckConsistency();

      var result = this.falseTestVisitor.Visit(guard, this);

      CheckConsistency();

      return result;
    }
    
#endregion

    #region Helper methods for assign
    /// <summary>
    /// Use the abstract domain of intervals to find lower and upper bounds for <code>x</code>.
    /// Essentually, <code>exp</code> is evaluated with intervals
    /// </summary>

    private void AssignWithIntervals(Expression x, PolynomialOfInt<Variable, Expression> exp)

    {
      var boundsForX = ConstraintsAsIntervalsFor(x, exp);

      var xVar = this.expManager.Decoder.UnderlyingVariable(x);

      this.ProjectVariable(xVar);
      this.AddConstraints(boundsForX);
    }

    /// <summary>
    /// Use this method when you were unable to obtain a polynomial form from the expressions
    /// </summary>
    private void AssignWithIntervals(Expression x, Expression exp)
    {
      var boundsForX = ConstraintsAsIntervalsFor(x, exp);

      var xVar = this.expManager.Decoder.UnderlyingVariable(x);

      this.ProjectVariable(xVar);
      this.AddConstraints(boundsForX);
    }

    /// <summary>
    /// Try to infer some constraints from the linear expression <code>polyExpSimplified</code>, which then uses for the octagon
    /// </summary>
    /// <param name="x">The variable to be assigned</param>
    /// <param name="polyExpSimplified">Must be a linear relation</param>

    private void AssignNonOctagonLinear(Variable x, PolynomialOfInt<Variable, Expression> polyExpSimplified)

    //^ requires polyExpSimplified.IsLinear;
    {
      // We distinguish two cases, if the assigment is invertible or not
      if (polyExpSimplified.Variables.Contains(x))   // It is an assigment in the form of x = x + k, with k a constant
      {
        // Renames "x + k" to "x' + k"

        var xOld = this.expManager.Encoder.FreshVariable<int>();
        this.AddVariable(xOld);

        var equalConstraint = new OctagonConstraintXEqualY(this.DimensionForVar(x), this.DimensionForVar(xOld));
        this.AddConstraint(equalConstraint);

        var renamedExp = polyExpSimplified.Rename(x, xOld);

        var newConstraints = ConstraintsForNonOctagonLinearForm(x, renamedExp);
        this.ProjectDimension(this.DimensionForVar(x));
        this.AddConstraints(newConstraints);

        this.RemoveVariable(xOld);
      }
      else
      {
        var newConstraints = ConstraintsForNonOctagonLinearForm(x, polyExpSimplified);
        this.ProjectVariable(x);            // set the value of x to top, force the closure of the octagon
        this.AddConstraints(newConstraints);
      }
    }
    #endregion

    /// <summary>
    /// The assignment can be handled by octagons
    /// </summary>
    /// <param name="x">Must be a variable</param>

    private void AssignWithOctagons(Expression x, PolynomialOfInt<Variable, Expression> exp)
    {
      Contract.Assert(exp.IsLinearOctagon, "I was expecting an octagon assigment, but I've got " + exp + " instead");

      // We distinguish two cases, if the assigment is invertible or not
      var xVar = this.expManager.Decoder.UnderlyingVariable(x);
      if (exp.Variables.Contains(xVar))   // It is an assigment in the form of x = x + k, with k a constant
      {
        // Renames "x + k" to "x' + k"

        var xOld = this.expManager.Encoder.FreshVariable<int>();
        this.AddVariable(xOld);

        var equalConstraint = new OctagonConstraintXEqualY(this.DimensionForVar(xVar), this.DimensionForVar(xOld));
        this.AddConstraint(equalConstraint);

        var renamedExp = exp.Rename(xVar, xOld);

        var newConstraints = ConstraintsForOctagonForm(xVar, renamedExp);
        this.ProjectDimension(this.DimensionForVar(xVar));
        this.AddConstraints(newConstraints);

        this.RemoveVariable(xOld);
      }
      else
      {
        var newConstraints = ConstraintsForOctagonForm(xVar, exp);
        this.ProjectVariable(xVar);            // set the value of x to top, force the closure of the octagon
        this.AddConstraints(newConstraints);
      }
    }

    #region Constraints inference

    /// <returns>
    /// A set of constraints that stands for the assignment <code>+/- x +/- y \leq k </code>. 
    /// Please note that <code>x</code> must not appear in <code>exp</code> in order to be correct
    /// </returns>            

    private Set<OctagonConstraint> ConstraintsForOctagonForm(Variable x, PolynomialOfInt<Variable, Expression> exp)
    {
      Contract.Assert(exp.IsLinearOctagon, string.Format("{0} is not in Octagonal form", exp));

      var result = new Set<OctagonConstraint>();
      
      if (exp.Left.Count == 1)    // x = y or or x = -y or x = k
      {
        var m = exp.Left[0];
        if (m.IsConstant)       // case x = k, k constant
        {
          result.Add(new OctagonConstraintXEqualConst(this.DimensionForVar(x), Rational.For(m.K)));
        }
        else if (m.K == 1)      // case x = y
        {
          var y = m.Variables[0];

          result.Add(new OctagonConstraintXEqualY(this.DimensionForVar(x), this.DimensionForVar(y)));
        }
        else if (m.K == -1)      // case x = -y
        {
          var y = m.Variables[0];

          result.Add(new OctagonConstraintXY(this.DimensionForVar(x), this.DimensionForVar(y), 0));             //  x + y <= 0
          result.Add(new OctagonConstraintMinusXMinusY(this.DimensionForVar(x), this.DimensionForVar(y), 0));   // -x - y <= 0
        }
        else
        {
          Contract.Assert(false, "Unknown case : " + x + " == " + exp);
           }
      }
      else // x = {+, -} y + k, k constant
      {
        var signOfY = (int)exp.Left[0].K;
        var y = exp.Left[0].Variables[0];
        var k = (int)exp.Left[1].K;

        if (signOfY == 1)               // x = y + k, k constant
        {
          result.Add(new OctagonConstraintXMinusY(this.DimensionForVar(x), this.DimensionForVar(y), (int)k));     // x - y <= k
          result.Add(new OctagonConstraintXMinusY(this.DimensionForVar(y), this.DimensionForVar(x), (int)-k));    // y - x <= -k
        }
        else if (signOfY == -1)         // x = -y + k, k constant
        {
          result.Add(new OctagonConstraintXY(this.DimensionForVar(x), this.DimensionForVar(y), (int)k));           //   x + y <= k
          result.Add(new OctagonConstraintMinusXMinusY(this.DimensionForVar(x), this.DimensionForVar(y), (int)-k)); // - x - y <= -k
        }
        else
        {
          Contract.Assert(false, "Unknown case : " + x + " == " + exp);
        }
      }
      
      return result;
    }

    /// <summary>
    /// Try to infer some constraints from the linear expression <code>x == polyExpSimplified</code>
    /// </summary>
    /// <param name="x">The variable to be assigned</param>
    /// <param name="polyExpSimplified">Must be a linear relation</param>

    private Set<OctagonConstraint> ConstraintsForNonOctagonLinearForm(Variable x, PolynomialOfInt<Variable, Expression> polyExpSimplified)
    {
      Contract.Assert(polyExpSimplified.IsLinear, "Expecting a linear polynomial");

      var newConstraints = new Set<OctagonConstraint>();

      // If polyExpSimplified is in the form of a1 * x1 + a2 * x2 + ... where \forall a_i. a_i \in {-1 , +1}
      if (HasUnaryCoefficients(polyExpSimplified))
      {
        #region 1. Get the bounds for all the variables in polyExpSimplified

        var boundsFor = new Dictionary<Variable, Interval>();     // The interval bounds for the variable
        var signFor = new Dictionary<Variable, int>();                           // The sign for each variable
        int constant = 0;                                                                                       // The constant (if any) in the expression

        foreach (var m in polyExpSimplified.Left)
        {
          Contract.Assert(m.IsLinear); //^ assert m.IsLinear;

          if (!m.IsConstant)
          {
            boundsFor[m.Variables[0]] = this.BoundsFor(m.Variables[0]).AsInterval;
            signFor[m.Variables[0]] = Math.Sign(m.K);
          }
          else
          {
            constant = m.K;
          }
        }

        Contract.Assert(boundsFor.Keys.Count == signFor.Keys.Count, "Something is wrong...");
        //^ assert boundsFor.Keys.Count == signFor.Keys.Count;
        #endregion

        #region 2. Try to reduce the polynomial to ai * xi + I, where ai is in {+1 , -1}I is a closed interval

        foreach (var xi in boundsFor.Keys)       // Try all the expressions
        {
          OctagonConstraint constraintForLowerBound = null;
          OctagonConstraint constraintForUpperBound = null;

          if (!x.Equals(xi))
          {
            #region Try to get the constraints for x = xi + I
            var I = Interval.For(constant);

            // Evaluate all the other variables
            foreach (var y in boundsFor.Keys)
            {
              if (!y.Equals(xi))
              {
                if (signFor[y] == 1)
                {
                  I = I + boundsFor[y];          // I += [y]
                }
                else if (signFor[y] == -1)
                {

                  I = I - boundsFor[y];   // I -= [y]
                }
                else
                {
                  Contract.Assert(false, "Expecting +1 or -1 as coefficent, found " + boundsFor[y]);
                  //^ assert false;
                }
              }
            }
            // At this point we have I = [a,b], and x = xi + [a, b]. We want to generate the two constraints x - xi >= a and x - xi <= b

            // Case x {+, -} xi >= a, a > -oo
            if (!I.IsLowerBoundMinusInfinity)
            {
              constraintForLowerBound = OctagonConstraint.For(-1, this.DimensionForVar(x), +1 * signFor[xi], this.variables2dimensions[xi], -1 * I.LowerBound);
            }

            // Case x {+, -} xi <= b, b < +oo
            if (!I.IsUpperBoundPlusInfinity)
            {
              constraintForUpperBound = OctagonConstraint.For(+1, this.DimensionForVar(x), -1 * signFor[xi], this.variables2dimensions[xi],  I.UpperBound);
            }
            #endregion
          }
          else // x == xi
          {
            #region Get the constraints for x = x + I
            Interval I = Interval.For(constant);
            foreach (var y in boundsFor.Keys)
            {
              if (signFor[y] == 1)
              {
                I = I + boundsFor[y];          // I += [y]
              }
              else if (signFor[y] == -1)
              {

                I = I - boundsFor[y];   // I -= [y]
              }
              else
              {
                Contract.Assert(false, "Expecting +1 or -1 as coefficent, found " + boundsFor[y]);
              }
            }

            // Case x >= a, a > -oo
            if (!I.IsLowerBoundMinusInfinity)
            {
              constraintForLowerBound = OctagonConstraint.For(-1, this.DimensionForVar(x), -1 * I.LowerBound);
            }

            // Case x <= b, b < +oo
            if (!I.IsUpperBoundPlusInfinity)
            {
              constraintForUpperBound = OctagonConstraint.For(-1, this.DimensionForVar(x), -I.UpperBound);
            }
            #endregion
          }

          if (constraintForLowerBound != null)
            newConstraints.Add(constraintForLowerBound);
          if (constraintForUpperBound != null)
            newConstraints.Add(constraintForUpperBound);
        }
        #endregion
  
       

      }  //  If polyExpSimplified is in the form a * x + b, where a, b are arbitary rationals
      else if (IsSimpleLine(polyExpSimplified))
      {
        #region Try to infer something
        Contract.Assert(polyExpSimplified.Left.Count == 2);
 

        var m1 = polyExpSimplified.Left[0];
        var m2 = polyExpSimplified.Left[1];

        Contract.Assert(m2.Variables.Count == 0);

        var a = m1.K;
        var z = m1.Variables[0];
        var b = m2.K;

        if (!z.Equals(x))
        {

          var boundsForZ = this.BoundsFor(z);   // TODO5: Performance, avoid closing the octagon here, or close it just for x

          if (b % (1 - a) != 0) // not an int
          {
            return newConstraints;
          }

          var frontier = b/ (1 - a);    // the point where x == a * z + b

          if (boundsForZ.LowerBound > frontier)
          {
            // At this point we can create the constraint x < z, i.e. x - z <= -1 .
            newConstraints.Add(new OctagonConstraintXMinusY(this.DimensionForVar(x), this.DimensionForVar(z), -1));
          }
          else if (boundsForZ.LowerBound == frontier)
          {
            // At this point we can create the constraint x <= z, i.e. x - z <= 0 .
            newConstraints.Add(new OctagonConstraintXMinusY(this.DimensionForVar(x), this.DimensionForVar(z), 0));
          }

          if (boundsForZ.UpperBound < frontier)
          {
            // At this point we can create the constrain x > z, i.e.  z - x <= -1 . 
            newConstraints.Add(new OctagonConstraintXMinusY(this.DimensionForVar(z), this.DimensionForVar(x), -1));
          }
          else if (boundsForZ.UpperBound == frontier)
          {
            // At this point we can create the constrain x >= z, i.e.  z - x <= 0 . 
            newConstraints.Add(new OctagonConstraintXMinusY(this.DimensionForVar(z), this.DimensionForVar(x), 0));
          }

          // eval = a * z + b
          var boundsForZAsRational = Interval.For(boundsForZ.LowerBound, boundsForZ.UpperBound);
          var eval = (Interval.For(a))* boundsForZAsRational  + (Interval.For(b));

          if (!eval.LowerBound.IsInfinity)
          { // Add the constraints x >= eval.LowerBound
            newConstraints.Add(new OctagonConstraintMinusX(this.DimensionForVar(x), -eval.LowerBound.PreviousInt32));
          }
          if (!eval.UpperBound.IsInfinity)
          { // Add the constraint x <= eval.UpperBound
            newConstraints.Add(new OctagonConstraintX(this.DimensionForVar(x), eval.UpperBound.NextInt32));
          }

          Contract.Assert(newConstraints.Count <= 3, "At this point you can add at most 3 constraints! Found " + newConstraints.Count);
          
        }
        else // x == z
        {
            // Do nothing
        }
        #endregion
      }
      else
      {
        #region Try to find upperbounds for x, if polyExpSimplified is monotonic, otherwise use the intervals
        bool polyIsMonotonic = true;

        // Check if the polynomial is monotonic 

        foreach (var m in polyExpSimplified.Left)
        {
          if (!m.IsConstant && m.K < 0)
          {
            polyIsMonotonic = false;
            break;
          }
        }

        if (polyIsMonotonic && !polyExpSimplified.Variables.Contains(x))
        {
          newConstraints = ConstraintsForNonOctagonLinearFormWithUpperBounds(x, polyExpSimplified);
        }
        else
        {
          var xExp = this.expManager.Encoder.VariableFor(x);
          newConstraints = ConstraintsAsIntervalsFor(xExp, polyExpSimplified);
        }
        #endregion
      }

      return newConstraints;
    }

    /// <returns>
    /// At most two octagon constraints that stands for the Interval in which <code>x</code> lay.
    /// If <code>x</code> is contradictory, then it returns bottom
    /// </returns>
    private Set<OctagonConstraint> ConstraintsAsIntervalsFor(Expression x, PolynomialOfInt<Variable, Expression> exp)
    {
      if (this.expManager.Encoder == null)
      {
        return new Set<OctagonConstraint>();
      }

      var xVar = this.expManager.Decoder.UnderlyingVariable(x);

      // Create an interval environment with just the variables in polyExpSimplified
      Set<OctagonConstraint> boundsForX;
      var varsInExp = exp.Variables;
      int dimensionForX;

      var tmpInterval = new IntervalEnvironment<Variable, Expression>(this.expManager);
      foreach (var var in varsInExp)
      {
        tmpInterval.AssumeInDisInterval(var, this.BoundsFor(var));
      }

      if (!tmpInterval.Variables.Contains(xVar))    //if it is not an assignement like "x = exp[x]" we add x to the environment 
      {
        tmpInterval.AddVariable(xVar);
      }

      tmpInterval.Assign(x, exp.ToPureExpression(this.expManager.Encoder));

      var v = tmpInterval.BoundsFor(x);
      
      dimensionForX = this.DimensionForVar(xVar);
      boundsForX = new Set<OctagonConstraint>(2);

      if (!v.LowerBound.IsInfinity)
      {
        boundsForX.Add(new OctagonConstraintMinusX(dimensionForX, -v.LowerBound));
      }
      if (!v.UpperBound.IsInfinity)
      {
        boundsForX.Add(new OctagonConstraintX(dimensionForX, v.UpperBound));
      }

      if (boundsForX.Count != 0)
      {   // If we have at least one constraint
        return boundsForX;
      }
      else
      {   // No new constraints.. It can be either top or bottom
        if (v.IsTop)
        {
          return boundsForX;
        }
        else
        {   
          Contract.Assert(v.IsBottom);

          // Add a contraddiction
          OctagonConstraint xLTZero = new OctagonConstraintX(dimensionForX, -1);
          OctagonConstraint xGTZero = new OctagonConstraintMinusX(dimensionForX, -1);

          boundsForX.Add(xLTZero);
          boundsForX.Add(xGTZero);

          return boundsForX;
        }
      }
    }

    /// <returns>
    /// At most two octagon constraints that stands for the Interval in which <code>x</code> lay.
    /// If <code>x</code> is contradictory, then it returns bottom
    /// </returns>
    private Set<OctagonConstraint> ConstraintsAsIntervalsFor(Expression x, Expression exp)
    {
      // Create an interval environment with just the variables in polyExpSimplified
      var boundsForX = new Set<OctagonConstraint>();
      var varsInExp = this.expManager.Decoder.VariablesIn(exp);
      Interval v;
      int dimensionForX;

      var xVar = this.expManager.Decoder.UnderlyingVariable(x);

      var tmpInterval = new IntervalEnvironment<Variable, Expression>(this.expManager);
      foreach (var var in varsInExp)
      {
        tmpInterval.AssumeInDisInterval(var, this.BoundsFor(var));
      }

      if (!tmpInterval.ContainsKey(xVar))    //if it is not an assignement like "x = exp[x]" we add x to the environment 
      {
        tmpInterval.AddVariable(xVar);
      }

      tmpInterval.Assign(x, exp);

      v = tmpInterval.BoundsFor(x).AsInterval;

      dimensionForX = this.DimensionForVar(xVar);
      boundsForX = new Set<OctagonConstraint>(2);

      if (!v.LowerBound.IsInfinity)
      {
        boundsForX.Add(new OctagonConstraintMinusX(dimensionForX, -v.LowerBound));
      }
      if (!v.UpperBound.IsInfinity)
      {
        boundsForX.Add(new OctagonConstraintX(dimensionForX, v.UpperBound));
      }

      if (boundsForX.Count != 0)
      {   // If we have at least one constraint
        return boundsForX;
      }
      else
      {   // No new constraints.. It can be either top or bottom
        if (v.IsTop)
        {
          return boundsForX;
        }
        else if (v.IsBottom)
        {

          // Add a contraddiction
          var xLTZero = new OctagonConstraintX(dimensionForX, -1);
          var xGTZero = new OctagonConstraintMinusX(dimensionForX, -1);

          boundsForX.Add(xLTZero);
          boundsForX.Add(xGTZero);

          return boundsForX;
        }
        else
        { // this is the case when v is a very large value 
          return boundsForX;
        }
      }
    }

    /// <summary>
    /// Try to find symbolic upper bounds for the variable x, and then do the assignment.
    /// At the moment, we require <code>polyExpSimplified</code> to have all the coefficents >= 0, i.e. it is a strictly monotonic function
    /// </summary>
    /// <param name="x">The variable to be assigned</param>
    /// <param name="polyExpSimplified">The polynomial, which must be linear and with all the coefficents >= 0</param>

    private Set<OctagonConstraint> ConstraintsForNonOctagonLinearFormWithUpperBounds(Variable x, PolynomialOfInt<Variable, Expression> polyExpSimplified)

    {
      Contract.Assert(polyExpSimplified.IsLinear);
      Contract.Assert(!polyExpSimplified.Variables.Contains(x));

      var variables = new Set<Variable>();
      var newConstraints = new Set<OctagonConstraint>();

      var varsToUpperBounds = new Dictionary<Variable, Set<PolynomialOfInt<Variable, Expression>>>();


      #region 1. Get the upper bounds for the variables in polyExpSimplified
      // Get the variables in the polynomial

      foreach (var m in polyExpSimplified.Left)

      {
        if (!m.IsConstant)
        {
          var v = m.Variables[0];      // We know there is just one variable as the polynomial is linear
          variables.Add(v);
        }
      }

      // Get the symbolic upper bounds for the variables in the monomial
      foreach (var v in variables)
      {
        var symbolicBoundsForV = BoundsInSymbolicFormFor(v);
        varsToUpperBounds[v] = symbolicBoundsForV;
      }
      #endregion

      #region 2. Substitute the upper bounds for the variables in polyExpSimplified, and try to infer an upper bound for x

      var currentSubstitutions = new Dictionary<Variable, PolynomialOfInt<Variable, Expression>>();
      var variablesAsArray = new Variable[varsToUpperBounds.Count];

      int i = 0;
      foreach (var var in varsToUpperBounds.Keys)
      {
        variablesAsArray[i] = var;
        i++;
      }

      // Get the upper bound constraints
      QuestForNewConstraints(0, variablesAsArray, varsToUpperBounds, currentSubstitutions, newConstraints, x, polyExpSimplified);

      // Get the lower bound constraints (as intervals!)
      var xExp = this.expManager.Encoder.VariableFor(x);
      var constraintsFromIntervals = this.ConstraintsAsIntervalsFor(xExp, polyExpSimplified);

      newConstraints.AddRange(constraintsFromIntervals);
      #endregion

      return newConstraints;
    }

    /// <returns>
    /// A constraint that stands for the relation <code>x \leq exp</code>. 
    /// Please note that <code>x</code> must not appear in <code>exp</code> in order to be correct
    /// </returns>            

    OctagonConstraint ConstraintsForUpperBound(Variable x, PolynomialOfInt<Variable, Expression> exp)
    {
      Contract.Assert(!exp.Variables.Contains(x), "The variable " + x + " is not allowed to stay in " + exp);

      OctagonConstraint result;

      if (exp.Left.Count == 1)    // x <= y or or x <= -y or x <= k
      {

        var m = exp.Left[0];

        if (m.IsConstant)       // case x <= k, k constant
        {
          result = new OctagonConstraintX(this.DimensionForVar(x), (int)m.K);
        }
        else if (m.K == 1)      // case x <= y
        {
          var y = m.Variables[0];

          result = new OctagonConstraintXMinusY(this.DimensionForVar(x), this.DimensionForVar(y), 0);
        }
        else if (m.K == -1)      // case x <= -y
        {
          var y = m.Variables[0];

          result = new OctagonConstraintXY(this.DimensionForVar(x), this.DimensionForVar(y), 0);             //  x + y <= 0
        }
        else
        {
          Contract.Assert(false, "Unknown case : " + x + " == " + exp);

          throw new AbstractInterpretationException("Unknown case");
        }
      }
      else // x = {+, -} y + k, k constant
      {
        int signOfY = (int)exp.Left[0].K;
        var y = exp.Left[0].Variables[0];
        int k = (int)exp.Left[1].K;

        if (signOfY == 1)               // x <= y + k, k constant
        {
          result = new OctagonConstraintXMinusY(this.DimensionForVar(x), this.DimensionForVar(y), (int)k);     // x - y <= k
        }
        else if (signOfY == -1)         // x <= -y + k, k constant
        {
          result = new OctagonConstraintXY(this.DimensionForVar(x), this.DimensionForVar(y), (int)k);           //   x + y <= k
        }
        else
        {
          Contract.Assert(false, "Unknown case : " + x + " == " + exp);

          throw new AbstractInterpretationException("Unknown case");
        }
      }

      return result;
    }

    /// <returns>
    /// The symbolic bound corresponding to the variable <code>x</code>
    /// </returns>

    private Set<PolynomialOfInt<Variable, Expression>> BoundsInSymbolicFormFor(Variable x)
    {
      var result = new Set<PolynomialOfInt<Variable, Expression>>();

      MonomialOfInt<Variable> kAsMonomial;
      MonomialOfInt<Variable> yAsMonomial;

      Rational k;

      #region Gets the bounds x <= k -y and x <= y +k
      foreach (var y in this.Variables)
      {
        if (x.Equals(y))
          continue;

        // The constraint -y + x <= k, that is the upper bound x <= k + y
        k = this.ConstantForMinusXY(this.DimensionForVar(y), this.DimensionForVar(x));
        if (!k.IsInfinity && k.IsInteger)
        {
          kAsMonomial = new MonomialOfInt<Variable>((int)k);        //  k * 1
          yAsMonomial = new MonomialOfInt<Variable>(1, y);    //  1 * y
          PolynomialOfInt<Variable, Expression> kMinusy;  // = new PolynomialOfInt<Variable, Expression>(kAsMonomial, yAsMonomial); // k * 1 + 1 * y

          var asList = new List<MonomialOfInt<Variable>>() { kAsMonomial, yAsMonomial };

          if (!PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(asList, out kMinusy))
          {
            throw new AbstractInterpretationException("The conversion of a list of monomials into a polynomial should never fail");
          }

          result.Add(kMinusy);
        }
      }
      #endregion

      #region Gets the bound x <= k
      k = this.ConstantForX(this.DimensionForVar(x));
      if (!k.IsInfinity && k.IsInteger)
      {

        PolynomialOfInt<Variable, Expression> lessThank; // = new PolynomialOfInt<Variable, Expression>(kAsMonomial);     // 1 * k

        if (!PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(new List<MonomialOfInt<Variable>>() { new MonomialOfInt<Variable>((Int32) k)}, out lessThank))
        {
          throw new AbstractInterpretationException("The conversion of a list of monomials into a polynomial should never fail");
        }

        result.Add(lessThank);
      }
      #endregion

      return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p">The current position in the variables order to visit</param>
    /// <param name="variables">The array containing all the variables to be assigned</param>
    /// <param name="varsToUpperBounds">The map between variables and their upper bounds w.r.t. <code>x</code></param>
    /// <param name="currentSubstitutions">The substitution we have computed up to now</param>
    /// <param name="newConstraints">The new inferred constraints</param>
    /// <param name="x">The variable we want to infer constraints for</param>
   /// <param name="polyExpSimplified">The original PolynomialOfInt</param>
    private void QuestForNewConstraints(int p, Variable[] variables, IDictionary<Variable, Set<PolynomialOfInt<Variable, Expression>>> varsToUpperBounds,
      IDictionary<Variable, PolynomialOfInt<Variable, Expression>> currentSubstitutions, Set<OctagonConstraint> newConstraints, Variable x, PolynomialOfInt<Variable, Expression> polyExpSimplified)

    {
      Contract.Assert(p >= 0);

      #region Case 1. We still have to generate a polynomial, to be propagated to the bottom of the tree
      if (p < varsToUpperBounds.Count)
      {

        Variable currentVariable = variables[p];      // The current variable
        var upperbounds = varsToUpperBounds[currentVariable];
        foreach (var upperBoundForCurrentVariable in upperbounds)
        {
          currentSubstitutions[currentVariable] = upperBoundForCurrentVariable;
          QuestForNewConstraints(p + 1, variables, varsToUpperBounds, currentSubstitutions, newConstraints, x, polyExpSimplified);
        }

        return;
      }
      #endregion

      #region Case 2. We have an assignment for all the variables (we are at the bottom of tree)
      if (p == varsToUpperBounds.Count)
      {
        var canonicalOne = polyExpSimplified.Rename(currentSubstitutions);    // Get the polynomial containing the substitutions...
        
        if (canonicalOne.IsLinearOctagon)
        {
          var asConstraint = ConstraintsForUpperBound(x, canonicalOne);
          newConstraints.Add(asConstraint);
        }
        else
        {
          // do nothing as we have any new octagonal constraints...
        }
        return;
      }
      #endregion


      //Unreachable code
      throw new AbstractInterpretationException("Unreachable code?");
    }
    #endregion

    #region Helper for TestTrue

    /// <summary>
    /// Assume <code>exp1 \leq exp2</code>
    /// </summary>
    public OctagonEnvironment<Variable, Expression> TestTrueLessEqualThan(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      this.CheckConsistency();

      // First step: Add exp1 <= exp2 (that is exp1 -exp2 <= 0)
      PolynomialOfInt<Variable, Expression> pol;

      var intvForE1 = this.constantVisitor.Visit(exp1, new IntervalEnvironment<Variable, Expression>(this.expManager));
      var intvForE2 = this.constantVisitor.Visit(exp2, new IntervalEnvironment<Variable, Expression>(this.expManager));

      var exp1Var = this.expManager.Decoder.UnderlyingVariable(exp1);
      var exp2Var = this.expManager.Decoder.UnderlyingVariable(exp2);

      OctagonConstraint directConstraint = null;
      Rational valForE1, valForE2;

      var b1 = intvForE1.TryGetSingletonValue(out valForE1);
      var b2 = intvForE2.TryGetSingletonValue(out valForE2);

      // The four cases...
      if (b1 && !b2)
      { // k - e2 <= 0
        if (valForE1.IsInteger)
        {
          directConstraint = OctagonConstraint.For(-1, this.DimensionForVar(exp2Var), - ((Int32)valForE1));
        }
      }
      else if (!b1 && b2)
      { // e1 - k <= 0
        if (valForE2.IsInteger)
        {
          directConstraint = OctagonConstraint.For(1, this.DimensionForVar(exp1Var), 0 + ((Int32)valForE2));
        }
      }
      else if (b1 && b2)
      {
        if (!(valForE1 <= valForE2))
        {
          return (OctagonEnvironment<Variable, Expression>)this.Bottom;
        }
      }
      else
      {
        directConstraint = OctagonConstraint.For(1, this.DimensionForVar(exp1Var), -1, this.DimensionForVar(exp2Var), Rational.For(0));
      }

      if (directConstraint != null)
      {
        this.AddConstraint(directConstraint);
      }

      TestTrueLessEqualThanSpecialCaseForPartitionAnalysis(exp1, exp2);

      if (! PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessEqualThan, exp1, exp2, this.expManager.Decoder, out pol))
      {
        return this;
      }

      if (pol.IsOctagonForm)
      {
        OctagonConstraint constraintForGuard;

        // We check to see which kind of octagon constraint is
        if (pol.Relation == ExpressionOperator.LessEqualThan)
        {
          if (pol.Left.Count == 2)
          {   // It is a constraint in the form a.x + b.y <= k
            var a = pol.Left[0].K;
            var b = pol.Left[1].K;

            var x = pol.Left[0].Variables[0];
            var y = pol.Left[1].Variables[0];

            var k = pol.Right[0].K;

            constraintForGuard = OctagonConstraint.For(a, this.DimensionForVar(x), b, this.DimensionForVar(y), k);
          }
          else if (pol.Left.Count == 1)
          {   // It is a constraint in the form of a.x <= k or k1 <= k2
            var a = pol.Left[0].K;

            if (pol.Left[0].Variables.Count > 0 && a != 0) // there is at least one variable, so it is not a straignforward constraints
            {
              var x = pol.Left[0].Variables[0];

              int k = (int)pol.Right[0].K;

              constraintForGuard = OctagonConstraint.For(a, this.DimensionForVar(x), k);
            }
            else
            {   // No Variables, so it is a constraint in the form of k1 <= k2, with k1 and k2 int constants

              // We compare the coefficents using rationals, i.e. more precise 
              var aAsRational = pol.Left[0].K;
              var b = pol.Right[0].K;
              if (aAsRational <= b)
              {   // nothing new, is a tautology
                return this;
              }
              else
              {   // is a contraddiction
                return (OctagonEnvironment<Variable, Expression>)this.Bottom;
              }
            }
          }
          else
          {   // We are not supposed to be there....
            Contract.Assert(false, "Error: wrong octogonal constraint: " + pol);

            return null;
          }

          this.CheckConsistency();

          this.AddConstraint(constraintForGuard);     // Add the constraint to the environment

          return this;
        }
        else
        {
          return this;
        }
      }
      else
      {
        return this;
      }
    }

    private OctagonEnvironment<Variable, Expression> TestTrueEqualNotRefininingLeftArgumentsInternal(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      var constraints = new Set<OctagonConstraint>();

      PolynomialOfInt<Variable, Expression> exp2Pol;
      if (PolynomialOfInt<Variable, Expression>.TryToPolynomialForm(exp2, this.expManager.Decoder, out exp2Pol) && exp2Pol.IsLinearOctagon)
      {
        OctagonConstraint c1 = null, c2 = null;
        var dim1 = this.DimensionForVar(this.expManager.Decoder.UnderlyingVariable(exp1));

        if (exp2Pol.Left.Count == 1)
        { // exp1 == m

          var k = exp2Pol.Left[0].K;

          if (exp2Pol.Left[0].IsConstant)
          { // exp1 == k
            c1 = OctagonConstraint.For(1, dim1, k);
            c2 = OctagonConstraint.For(-1, dim1, -k);
          }
          else
          { // exp1 == v
            var dim2 = this.DimensionForVar(exp2Pol.Left[0].Variables[0]);
            c1 = OctagonConstraint.For(1, dim1, -1, dim2, 0);
            c2 = OctagonConstraint.For(-1, dim1, 1, dim2, 0);
          }

        }
        else if (exp2Pol.Left.Count == 2)
        {
          // exp1 == v + k
          var dim2 = this.DimensionForVar(exp2Pol.Left[0].Variables[0]);
          var k = exp2Pol.Left[1].K;

          c1 = OctagonConstraint.For(1, dim1, -1, dim2, k);
          c2 = OctagonConstraint.For(-1, dim1, 1, dim2, -k);
        }

        constraints.Add(c1);
        constraints.Add(c2);

        this.AddConstraints(constraints);
      }

      return this;
    }

    #endregion

    #region Miscellanea helper functions (HasUnaryCoefficients, IsSimpleLine)


    private static readonly Predicate<MonomialOfInt<Variable>> oneOrMinusOne = delegate(MonomialOfInt<Variable> m) { return m.K == 1 || m.K == -1; };


    /// <returns>
    /// true iff polyExpSimplified is in the form of a1 * x1 + a2 * x2 + ... where \forall a_i. a_i \in {-1 , +1}
    /// </returns>

    private static bool HasUnaryCoefficients(PolynomialOfInt<Variable, Expression> polyExpSimplified)

    {
      foreach(var m in polyExpSimplified.Left)
      {
        if (!oneOrMinusOne(m))
          return false;
      }

      return true;
    }

    /// <param name="polyExpSimplified">Must be a linear PolynomialOfInt</param>
    /// <returns>
    /// true iff <code>polyExpSimplified</code> is in the form of a1 * x1 + a2, where a1, a2 are <code>int</code> and <code>x</code> is a variable 
    /// </returns>
    private static bool IsSimpleLine(PolynomialOfInt<Variable, Expression> polyExpSimplified)

    //^ requires polyExpSimplified.IsLinear;
    {
      Contract.Assert(polyExpSimplified.IsLinear, "I was expecting " + polyExpSimplified + " to be linear");

      if (polyExpSimplified.Left.Count == 2)
      { // matches polyExpSimplified with m + k
        var m = polyExpSimplified.Left[0];
        var k = polyExpSimplified.Left[1];

        return (m.Variables.Count == 1 && k.Variables.Count == 0) || (m.Variables.Count == 0 && k.Variables.Count == 1);
      }
      else
      {
        return false;
      }
    }
    #endregion

    #region INumericalAbstractDomainQuery<Variable,Expression> Members

    public Variable ToVariable(Expression exp)
    {
      return this.expManager.Decoder.UnderlyingVariable(exp);
    }

    #endregion
  }

  #region Visitors for the guards (TestTrue, TestFalse, CheckIfHolds)

  class OctagonsTrueTestVisitor<Variable, Expression>
  : TestTrueVisitor<OctagonEnvironment<Variable, Expression>, Variable, Expression>
  {
    public OctagonsTrueTestVisitor(IExpressionDecoder<Variable, Expression> decoder)
      : base(decoder)
    {
    }


    public override OctagonEnvironment<Variable, Expression> VisitEqual(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)

    {
      return data.TestTrueEqual(this.Decoder.Stripped(left), this.Decoder.Stripped(right));
    }

    public override OctagonEnvironment<Variable, Expression> VisitLessEqualThan(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data.TestTrueLessEqualThan(this.Decoder.Stripped(left), this.Decoder.Stripped(right));
    }

    public override OctagonEnvironment<Variable, Expression> VisitLessThan(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data.TestTrueLessThan(this.Decoder.Stripped(left), this.Decoder.Stripped(right));
    }

    public override OctagonEnvironment<Variable, Expression> VisitNotEqual(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data.TestTrueNotEqual(this.Decoder.Stripped(left), this.Decoder.Stripped(right));
    }

    public override OctagonEnvironment<Variable, Expression> VisitVariable(Variable variable, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data;
    }
  }


  class OctagonsTrueTestNotRefiningVisitor<Variable, Expression>
  : TestTrueVisitor<OctagonEnvironment<Variable, Expression>, Variable, Expression>
  {
    public OctagonsTrueTestNotRefiningVisitor(IExpressionDecoder<Variable, Expression> decoder)
      : base(decoder)
    {
    }

    public override OctagonEnvironment<Variable, Expression> VisitAnd(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)
    {     
      var isLeftAVar = this.Decoder.IsVariable(left);
      var isLeftAConst = this.Decoder.IsConstant(left);
      var isRightAVar = this.Decoder.IsVariable(right);
      var isRightAConst = this.Decoder.IsConstant(right);

      if ((isLeftAVar && isRightAConst) || (isLeftAConst && isRightAVar))
      {
        //this.result = data;
        return data;
      }
      else
      {
        var oct = (OctagonEnvironment<Variable, Expression>)data.Clone();

        oct = oct.TestTrueNotRefining(left);
        oct = oct.TestTrueNotRefining(right);

        return oct;
      }
    }

    public override OctagonEnvironment<Variable, Expression> VisitEqual(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data.TestTrueEqualNotRefininingLeftArguments(this.Decoder.Stripped(left), this.Decoder.Stripped(right));
    }

    public override OctagonEnvironment<Variable, Expression> VisitLessEqualThan(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data.TestTrueLessEqualThan(this.Decoder.Stripped(left), this.Decoder.Stripped(right));
    }

    public override OctagonEnvironment<Variable, Expression> VisitLessThan(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data.TestTrueLessThan(this.Decoder.Stripped(left), this.Decoder.Stripped(right));
    }

    public override OctagonEnvironment<Variable, Expression> VisitNotEqual(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data.TestTrueNotEqual(this.Decoder.Stripped(left), this.Decoder.Stripped(right));
    }

    public override OctagonEnvironment<Variable, Expression> VisitVariable(Variable var, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data;
    }
  }

  class OctagonsFalseTestVisitor<Variable, Expression>
    : TestFalseVisitor<OctagonEnvironment<Variable, Expression>, Variable, Expression>
  {
    public OctagonsFalseTestVisitor(IExpressionDecoder<Variable, Expression>/*!*/ decoder)
      : base(decoder)
    { }

    public override OctagonEnvironment<Variable, Expression> VisitEqual(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data.TestTrueNotEqual(this.Decoder.Stripped(left), this.Decoder.Stripped(right));
    }

    public override OctagonEnvironment<Variable, Expression> VisitNotEqual(Expression left, Expression right, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data.TestTrueEqual(this.Decoder.Stripped(left), this.Decoder.Stripped(right));
    }

    public override OctagonEnvironment<Variable, Expression> VisitVariable(Variable variable, Expression original, OctagonEnvironment<Variable, Expression> data)
    {
      return data;
    }
  }

  class OctagonsCheckIfHoldsVisitor<Variable, Expression>
    : CheckIfHoldsVisitor<OctagonEnvironment<Variable, Expression>, Variable, Expression>
  {
    public OctagonsCheckIfHoldsVisitor(IExpressionDecoder<Variable, Expression> decoder)
      : base(decoder)
    {
    }

    public override FlatAbstractDomain<bool> VisitEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
    {
      var resultLeft = Domain.CheckIfLessEqualThan(left, right);
      
      if (!resultLeft.IsBottom && !resultLeft.IsTop)
      {
        var resultRight = Domain.CheckIfLessEqualThan(right, left);

        if (!resultRight.IsBottom && !resultRight.IsTop)
        {
          return resultLeft.Meet(resultRight);
        }
        else
        {
          return resultRight;
        }
      }
      else
      {
        return resultLeft;
      }
    }

    public override FlatAbstractDomain<bool> VisitEqual_Obj(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
    {
      return VisitEqual(left, right, original, data);
    }

    public override FlatAbstractDomain<bool> VisitLessEqualThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
    {
      return Domain.CheckIfLessEqualThan(left, right);
    }

    public override FlatAbstractDomain<bool> VisitLessThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
    {
      return Domain.CheckIfLessThan(left, right);
    }

    public override FlatAbstractDomain<bool> VisitVariable(Variable variable, Expression original, FlatAbstractDomain<bool> data)
    {
      var intv = this.Domain.BoundsFor(variable);

      if (intv.IsBottom)
        return CheckOutcome.Bottom;

      if (intv.IsTop)
        return CheckOutcome.Top;

      Rational v;
      if (intv.TryGetSingletonValue(out v))
      {
        return v.IsZero ? CheckOutcome.False : CheckOutcome.True;
      }

      return CheckOutcome.Top;
    }

  }
  #endregion

  #region Visitor for Inferring Octagon Constraints


  abstract class TryVisitAtMostTwoMonomialsOfInt<Variable, Expression, In, Data>
  {
    /// <summary>
    /// We want it to be used by the subclasses, but not called from clients
    /// </summary>

    protected bool Dispatch(PolynomialOfInt<Variable, Expression> exp, ref Data result)
    {
      Variable x, y;
      int a, b, k;

      if(PolynomialOfIntHelper.Match_k1XLessEqualThank2(exp, out a, out x, out k))
      { // a * x <= k
        if (a < 0)
        {
          return this.VisitMinusXLessEqualThan(a, x, k, ref result);
        }
        else if (a > 0)
        {
          return this.VisitXLessEqualThan(a, x, k, ref result);
        }
        else // a == 0
        {
          return Default(ref result);
        }
      }
      else if(PolynomialOfIntHelper.Match_k1XLessThank2(exp, out a, out x, out k))
      { // "a * x < k " == "a * x  <= k-1"
        if (a < 0)
        {
          return this.VisitMinusXLessEqualThan(a, x, k - 1, ref result);
        }
        else if (a > 0)
        {
          return this.VisitXLessEqualThan(a, x, k - 1, ref result);
        }
        else
        {
          return Default(ref result);
        }
      }
      else if(PolynomialOfIntHelper.Match_k1XPlusk2YLessEqualThanK(exp, out a, out x, out b, out y, out k))
      { // "a * x + b * y <= k" 
        if (a == 1 && b == 1)
        {
          return VisitXYLessEqualThanK(x, y, k, ref result);
        }
        else if (a == 1 && b == -1)
        {
          return this.VisitXMinusYLessEqualThan(x, y, k, ref result);
        }
        else if (a == -1 && b == 1)
        {
          return this.VisitXMinusYLessEqualThan(y, x, k, ref result);
        }
        else if(a== -1 && b == -1)
        {
          return this.VisitMinusXMinusYLessEqualThan(x, y, k, ref result);
        }
        else 
        {
          // Todo: Do we want a visitor for other cases here?
          return Default(ref result);
        }
      }
      else if(PolynomialOfIntHelper.Match_k1XPlusk2YLessThanK(exp, out a, out x, out b, out y, out k))
      { // "a * x + b * y < k" is "a * x + b * y <=  k-1"
        if (a == 1 && b == 1)
        {
          return this.VisitXYLessEqualThanK(x, y, k - 1, ref result);
        }
        else if (a == 1 && b == -1)
        {
          return this.VisitXMinusYLessEqualThan(x, y, k - 1, ref result);
        }
        else if (a == -1 && b == 1)
        {
          return this.VisitXMinusYLessEqualThan(y, x, k - 1, ref result);
        }
        else if (a == -1 && b == -1)
        {
          return this.VisitMinusXMinusYLessEqualThan(x, y, k - 1, ref result);
        }
        else
        {
          // Todo: Do we want a visitor for other cases here?
          return Default(ref result);
        }
      }
      else
      {
        return Default(ref result);
      }
    }

    abstract public bool Visit(PolynomialOfInt<Variable, Expression> exp, In input, ref Data result);

    abstract protected bool VisitMinusXMinusYLessEqualThan(Variable x, Variable y, int k, ref Data result);

    abstract protected bool VisitXMinusYLessEqualThan(Variable x, Variable y, int k, ref Data result);

    abstract protected bool VisitXYLessEqualThanK(Variable x, Variable y, int k, ref Data result);

    abstract protected bool VisitXLessEqualThan(int a, Variable x, int k, ref Data result);

    abstract protected bool VisitMinusXLessEqualThan(int a, Variable x, int k, ref Data result);


    private bool Default(ref Data result)
    {
      return false;
    }
  }


  class InferOctagonConstraints<Variable, Expression>
    : TryVisitAtMostTwoMonomialsOfInt<Variable, Expression, OctagonEnvironment<Variable, Expression>, Set<OctagonConstraint>>

  {
    private OctagonEnvironment<Variable, Expression> octagon;

    public InferOctagonConstraints()
    {
      this.octagon = null;
    }

    public override bool Visit(PolynomialOfInt<Variable, Expression> exp, OctagonEnvironment<Variable, Expression> input, ref Set<OctagonConstraint> result)

    {
      this.octagon = input;

      bool ok = this.Dispatch(exp, ref result);

      this.octagon = null;

      return ok;
    }


    protected override bool VisitMinusXMinusYLessEqualThan(Variable x, Variable y, int k, ref Set<OctagonConstraint> result)
    {
      result.Add(new OctagonConstraintMinusXMinusY(this.octagon.DimensionForVar(x), this.octagon.DimensionForVar(y), k));

      return true;
    }


    protected override bool VisitXMinusYLessEqualThan(Variable x, Variable y, int k, ref Set<OctagonConstraint> result)
    {
      result.Add(new OctagonConstraintXMinusY(this.octagon.DimensionForVar(x), this.octagon.DimensionForVar(y), k));

      return true;
    }

    protected override bool VisitXYLessEqualThanK(Variable x, Variable y, int k, ref Set<OctagonConstraint> result)
    {
      result.Add(new OctagonConstraintXY(this.octagon.DimensionForVar(x), this.octagon.DimensionForVar(y), k));

      return true;
    }

    protected override bool VisitXLessEqualThan(int a, Variable x, int k, ref Set<OctagonConstraint> result)
    {
      Contract.Assert(a > 0);

      if (k % a == 0)
      {
        result.Add(new OctagonConstraintX(this.octagon.DimensionForVar(x), (k / Math.Abs(a))));
        return true;
      }

      return false;
    }


    protected override bool VisitMinusXLessEqualThan(int a, Variable x, int k, ref Set<OctagonConstraint> result)
    {
      Contract.Assert(a < 0);

      if (k % a == 0)
      {
        result.Add(new OctagonConstraintMinusX(this.octagon.DimensionForVar(x), (k / Math.Abs(a))));

        return true;
      }

      return false;
    }
  }

  // This class is just for sharing code, you do not want to use it or give some meaning

  class CheckLessThanOrLessEqualThan<Variable, Expression>
    : TryVisitAtMostTwoMonomialsOfInt<Variable, Expression, OctagonEnvironment<Variable, Expression>, FlatAbstractDomain<bool>>
  {
    private OctagonEnvironment<Variable, Expression> octagon;

    public CheckLessThanOrLessEqualThan()
    {
      this.octagon = null;
    }

    /// <summary>
    /// Tries to see if <code>exp</code> holds in input.
    /// If it can figure it out a definite value (true or false), it returns true, and the value will be in <code>result</code>
    /// </summary>

    public override bool Visit(PolynomialOfInt<Variable, Expression> exp, OctagonEnvironment<Variable, Expression> input, ref FlatAbstractDomain<bool> result)
    {
      this.octagon = input;

      if (this.octagon.IsEmpty)
      {
        result = CheckOutcome.Top; // In theory the client should not access this value
        return false;
      }

      bool success = this.Dispatch(exp, ref result);

      this.octagon = null;

      return success;
    }

    protected override bool VisitMinusXMinusYLessEqualThan(Variable x, Variable y, int k, ref FlatAbstractDomain<bool> result)
    {
      Rational val;
      if (this.octagon.TryConstantForMinusXMinusY(x, y, out val))
      {
        result = val <= k ? CheckOutcome.True: CheckOutcome.Top;
        return true;
      }
      else
      {
        result = CheckOutcome.Top;
        return false;
      }
    }


    protected override bool VisitXMinusYLessEqualThan(Variable x, Variable y, int k, ref FlatAbstractDomain<bool> result)
    {
      Rational val;
      if (this.octagon.TryConstantForXMinusY(x, y, out val))
      {
        result = val <= k ? CheckOutcome.True : CheckOutcome.Top;
        return true;
      }
      else
      {
        result = CheckOutcome.Top;
        return false;
      }
    }

    protected override bool VisitXYLessEqualThanK(Variable x, Variable y, int k, ref FlatAbstractDomain<bool> result)
    {
      Rational val;
      if (this.octagon.TryConstantForXY(x, y, out val))
      {
        result = val <= k ? CheckOutcome.True : CheckOutcome.Top;
        return true;
      }
      else
      {
        result = CheckOutcome.Top;
        return false;
      }
    }


    protected override bool VisitXLessEqualThan(int a, Variable x, int k, ref FlatAbstractDomain<bool> result)
    {
      Rational val;
      if (this.octagon.TryConstantForX(x, out val))
      {
        result = val <= (k/a) ? CheckOutcome.True : CheckOutcome.Top;
        return true;
      }
      else
      {
        result = CheckOutcome.Top;
        return false;
      }
    }

    protected override bool VisitMinusXLessEqualThan(int a, Variable x, int k, ref FlatAbstractDomain<bool> result)
    {
      Rational val;
      if (this.octagon.TryConstantForMinusX(x, out val))
      {
        result = val <= -(k/a)? CheckOutcome.True : CheckOutcome.Top;
        return true;
      }
      else
      {
        result = CheckOutcome.Top;
        return false;
      }
    }
  }
  #endregion
}