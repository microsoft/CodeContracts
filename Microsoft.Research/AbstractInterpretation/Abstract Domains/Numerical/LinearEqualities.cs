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

// This file contains the classes and the abstract domains for implementing the abstract domain of Linear equalities

#if DEBUG
//#define CHECKINVARIANTS
//#define TRACEPERFORMANCE
//#define LOGINFO
#endif

#define TRACEPERFORMANCE2
//#define USESOLVERFOUNDATION

using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Glee;
using Microsoft.Glee.Optimization;
using System.Diagnostics.Contracts;
using System.Collections;
using System.Diagnostics.CodeAnalysis;


#if USESOLVERFOUNDATION
using Microsoft.SolverFoundation.Solvers;
using SolverRational = Microsoft.SolverFoundation.Common.Rational;
using Microsoft.SolverFoundation.Services;
#endif 

namespace Microsoft.Research.AbstractDomains.Numerical
{
  using System.Diagnostics.Contracts;

  [ContractVerification(true)]
  public class LinearEqualitiesEnvironment
  {
    #region Constants

    protected enum LinearEqualitiesState { Normal, StrongRowEchelon, Bottom };

    protected const int INITIAL_VARS_FOR_EQUATION_COUNT = 3;
    protected const int INITIAL_EQUATIONS_COUNT = 10;
    protected const int MAX_VARS_FOR_CLOSURE = 2;

    // The ratio and the maximum absolute number above which we give up having a precise projection, and we simply throw away variables
    protected const int MAX_RATIO_FOR_USING_WEAK_PROJECTION = 10;
    protected const int MAX_VARIABLES_TO_REMOVE_FOR_USING_WEAK_PROJECTION = 1000;

    readonly protected int HUGE_RENAMING = 1000;

    #endregion

#if TRACEPERFORMANCE
    private static int maxColumns;
    private static int maxRows;
    private static int maxVars;
    private static int maxUnused;
    private static int maxPairwiseEq;
#endif

    readonly protected bool PrintWarnsForLargeTargets =
#if DEBUG
      true;
#else
     false;
#endif

    readonly private static bool verbose;


    static LinearEqualitiesEnvironment()
    {
      verbose = false;
    }

    protected LinearEqualitiesEnvironment()
    {
    }

    /// <summary>
    /// Get the statics for the usage of the LinearEqualitiesEnvironment
    /// </summary>
    static public string Statistics
    {
      get
      {
#if TRACEPERFORMANCE
        var maxCol = "Maximum number of Columns : " + MaxColumns + Environment.NewLine;
        var maxRows = "Maximum number of Rows : " + MaxRows + Environment.NewLine;
        var maxUnused = "Maximum number of Unused columns " + MaxUnusedDim + Environment.NewLine;
        var maxPairWiseEq = "Maximum number of Pairwise Equalities " + MaxPairwiseEq + Environment.NewLine;

        return maxCol + maxRows + maxUnused + maxPairWiseEq;
#else
        return "Performance tracing is off";
#endif
      }
    }

    public static int AdaptiveRenamingInSubpolyhedraThreshold { set; get; }

#if TRACEPERFORMANCE
    public static int MaxColumns
    {
      get
      {
        return maxColumns;
      }
    }

    public static int MaxRows
    {
      get
      {
        return maxRows;
      }
    }

    public static int MaxVars
    {
      get
      {
        return maxVars;
      }
    }

    public static int MaxUnusedDim
    {
      get
      {
        return maxUnused;
      }
    }

    public static int MaxPairwiseEq
    {
      get
      {
        return maxPairwiseEq;
      }
    }
#endif

    [Conditional("TRACEPERFORMANCE")]
    public static void UpdateMaxColumns(int newVal)
    {
#if TRACEPERFORMANCE
      Update(ref maxColumns, newVal, "Max columns so far {0}");
#endif
    }

    [Conditional("TRACEPERFORMANCE")]
    public static void UpdateMaxRows(int newVal)
    {
#if TRACEPERFORMANCE
      Update(ref maxRows, newVal, "Max rows so far {0}");
#endif
    }

    public static void UpdateMaxVariables(int newVal)
    {
#if TRACEPERFORMANCE
      Update(ref maxVars, newVal, "Max vars so far {0}");
#endif
    }

    [Conditional("TRACEPERFORMANCE")]
    public static void UpdateMaxUnusedVariables(int newVal)
    {
#if TRACEPERFORMANCE
      Update(ref maxUnused, newVal, "Max unused dimensions so far {0}");
#endif
    }

    [Conditional("TRACEPERFORMANCE")]
    public static void UpdateMaxPairwiseEq(int newVal)
    {
#if TRACEPERFORMANCE
      Update(ref maxPairwiseEq, newVal, "Max pairwise eq so far {0}");
#endif
    }

    [Conditional("TRACEPERFORMANCE")]
    private static void Update(ref int what, int newVal, string msg)
    {
      if (newVal > what)
      {
        what = newVal;
        if (verbose)
        {
          Console.WriteLine(msg, newVal);
        }
      }
    }

    [Conditional("TRACEPERFORMANCE")]
    protected static void LogPerformance(string format, params string[] args)
    {
      Console.WriteLine(format, args);
    }

    [Conditional("TRACEPERFORMANCE")]
    protected static void LogPerformanceWithTreshold(int treshold, string format, int value)
    {
      if (value > treshold)
      {
        Console.WriteLine(format, value.ToString());
      }
    }

    [Conditional("TRACEPERFORMANCE")]
    protected static void LogPerformanceWithTreshold(TimeSpan treshold, string format, TimeSpan value)
    {
      if (value > treshold)
      {
        Console.WriteLine(format, value.ToString());
      }
    }
  }

  /// <summary>
  /// The abstract numerical domain of linear equalities
  /// </summary> 
  [ContractVerification(false)]
  [SuppressMessage("Microsoft.Contracts", "InvariantInMethod-this.varsToDimensions.LessThan(this.NumberOfColumns - 1)")]
  public class LinearEqualitiesEnvironment<Variable, Expression> :
      LinearEqualitiesEnvironment, // just for the statistics
      INumericalAbstractDomain<Variable, Expression>
  {

    #region Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.equations != null);
      Contract.Invariant(this.varsToDimensions != null);
      Contract.Invariant(this.expManager != null);
      Contract.Invariant(this.testTrue != null);
      Contract.Invariant(this.testFalse != null);
      Contract.Invariant(this.checkIfHolds != null);

      Contract.Invariant(this.varsToDimensions.LessThan(this.NumberOfColumns - 1));
    }

    #endregion

    #region State
    protected LinearEqualitiesState state;                              // The state of this linear equality
    protected LinearEquations<Variable, Expression> equations;          // The equations of the environment

    protected int numberOfRows;                 // The next free slot for a linear constraint
    protected int numberOfCols;                 // The number of columns

    protected VarsToDimensions varsToDimensions;        // The map from variables to dimensions

    protected ExpressionManagerWithEncoder<Variable, Expression> expManager;

    internal VisitorForTestTrue testTrue;
    internal VisitorForTestFalse testFalse;
    internal VisitorForCheckIfHolds checkIfHolds;

    #endregion

    #region The properties

    public ExpressionManager<Variable, Expression> ExpressionManager
    {
      get
      {
        return this.expManager;
      }
    }

    public bool ContainsOnlyConstants
    {
      get
      {
        for (int row = 0; row < this.NumberOfRows; row++)
        {
          if (!this.equations.IsConstantRow(row))
            return false;
        }
        return true;
      }
    }

    protected int NumberOfColumns
    {
      get
      {
        UpdateMaxColumns(this.numberOfCols);

        return this.numberOfCols;
      }
      set
      {
        UpdateMaxColumns(value);

        this.numberOfCols = value;
      }
    }

    public int NumberOfVariables
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        UpdateMaxVariables(this.varsToDimensions.Count);

        return this.varsToDimensions.Count;
      }
    }

    public int NumberOfRows
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() == this.numberOfRows);

        UpdateMaxRows(this.numberOfRows);

        return this.numberOfRows;
      }
    }

    protected int GrowSizeForColumns
    {
      get
      {
        return 4;
      }
    }

    public int UnusedColumns
    {
      get
      {
        return this.NumberOfColumns - (this.varsToDimensions.Count + 1);
      }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// How many linear equalities?
    /// </summary>
    /// <param name="decoder">The decoder for expressions</param>
    [ContractVerification(true)]
    public LinearEqualitiesEnvironment(ExpressionManagerWithEncoder<Variable, Expression> expManager)
      : this(INITIAL_VARS_FOR_EQUATION_COUNT, INITIAL_EQUATIONS_COUNT, expManager)
    {
      Contract.Requires(expManager != null);
    }

    /// <summary>
    /// Build a Linear equalities environment with <code>n</code> dimensions
    /// </summary>
    /// <param name="var">The initial number of variables</param>
    /// <param name="equationsCount">The initial number of equations</param>
    [ContractVerification(true)]
    public LinearEqualitiesEnvironment(int var, int equationsCount, ExpressionManagerWithEncoder<Variable, Expression> expManager)
    {
      Contract.Requires(expManager != null);

      Contract.Requires(var > 0);
      Contract.Requires(equationsCount > 0);

      this.equations = new LinearEquations<Variable, Expression>(this, equationsCount);

      this.numberOfRows = 0;
      this.NumberOfColumns = var + 1;

      this.state = LinearEqualitiesState.Normal;
      this.varsToDimensions = new VarsToDimensions();

      this.expManager = expManager;
      SetUpVisitors();
    }

    /// <summary>
    /// A cosntructor used mainly for the internal operations of the abstract domain
    /// </summary>
    [ContractVerification(true)]
    protected LinearEqualitiesEnvironment(LinearEquations<Variable, Expression> equations, int numOfRows, int numOfCols,
      VarsToDimensions varsToDimensions,
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
    {
      Contract.Requires(equations != null);
      Contract.Requires(varsToDimensions != null);
      Contract.Requires(expManager != null);

      this.equations = equations;
      this.numberOfRows = numOfRows;
      this.NumberOfColumns = numOfCols;

      this.state = LinearEqualitiesState.Normal;

      if (numOfRows > 0 && !this.equations.IsNullRow(0))
      {
        this.varsToDimensions = varsToDimensions;
      }
      else // The matrix is empty, so all the varsToDimensions are unusefull
      {
        this.varsToDimensions = new VarsToDimensions();
      }

      this.expManager = expManager;

      SetUpVisitors();
    }

    [ContractVerification(true)]
    protected LinearEqualitiesEnvironment(SparseRationalArray[] equations, int numOfRows, int numOfCols, VarsToDimensions varsToDimensions,
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
      : this(new LinearEquations<Variable, Expression>(null, equations), numOfRows, numOfCols, varsToDimensions, expManager)
    {
      Contract.Requires(equations != null);
      Contract.Requires(varsToDimensions != null);
      Contract.Requires(expManager != null);

      this.equations = new LinearEquations<Variable, Expression>(this, equations);
    }

    [ContractVerification(true)]
    protected LinearEqualitiesEnvironment(LinearEqualitiesEnvironment<Variable, Expression> l)
    {
      Contract.Requires(l != null);

      Contract.Assume(l.equations != null);
      Contract.Assume(l.varsToDimensions != null);
      Contract.Assume(l.expManager != null);
      Contract.Assume(l.testTrue != null);
      Contract.Assume(l.testFalse != null);
      Contract.Assume(l.checkIfHolds != null);

      this.equations = l.equations.AddSharing(this);
      this.numberOfRows = l.numberOfRows;
      this.numberOfCols = l.numberOfCols;
      this.state = l.state;
      this.varsToDimensions = new VarsToDimensions(l.varsToDimensions);
      this.expManager = l.expManager;
      this.testTrue = l.testTrue;
      this.testFalse = l.testFalse;
      this.checkIfHolds = l.checkIfHolds;
    }

    [ContractVerification(true)]
    protected void CloneThisFrom(LinearEqualitiesEnvironment<Variable, Expression> orig)
    {
      Contract.Requires(orig != null);

      Contract.Assume(orig.equations != null);
      Contract.Assume(orig.varsToDimensions != null);
      Contract.Assume(orig.expManager != null);
      Contract.Assume(orig.testTrue != null);
      Contract.Assume(orig.testFalse != null);
      Contract.Assume(orig.checkIfHolds != null);

      this.equations = orig.equations;
      this.numberOfCols = orig.numberOfCols;
      this.numberOfRows = orig.numberOfRows;
      this.state = orig.state;
      this.varsToDimensions = orig.varsToDimensions;
      this.expManager = orig.expManager;
      this.testTrue = orig.testTrue;
      this.testFalse = orig.testFalse;
      this.checkIfHolds = orig.checkIfHolds;

      // destroy the state of orig, to prevent bugs
      orig.equations = null;
      orig.varsToDimensions = null;

    }

    /// <summary>
    /// Set up the visitors
    /// </summary>
    [ContractVerification(true)]
    private void SetUpVisitors()
    {
      this.testTrue = new VisitorForTestTrue(this.expManager.Decoder);
      this.testFalse = new VisitorForTestFalse(this.expManager.Decoder);

      this.testTrue.FalseVisitor = this.testFalse;
      this.testFalse.TrueVisitor = this.testTrue;

      this.checkIfHolds = new VisitorForCheckIfHolds(this.expManager);
    }

    [ContractVerification(true)]
    protected virtual LinearEqualitiesEnvironment<Variable, Expression> New(
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
    {
      Contract.Requires(expManager != null);

      return new LinearEqualitiesEnvironment<Variable, Expression>(expManager);
    }

    [ContractVerification(true)]
    protected virtual LinearEqualitiesEnvironment<Variable, Expression> New(int var, int eqs,
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
    {
      Contract.Requires(var > 0);
      Contract.Requires(eqs > 0);
      Contract.Requires(expManager != null);

      return new LinearEqualitiesEnvironment<Variable, Expression>(var, eqs, this.expManager);
    }

    [ContractVerification(true)]
    protected virtual LinearEqualitiesEnvironment<Variable, Expression> New(LinearEqualitiesEnvironment<Variable, Expression> other)
    {
      Contract.Requires(other != null);

      return new LinearEqualitiesEnvironment<Variable, Expression>(other);
    }

    [ContractVerification(true)]
    protected virtual LinearEqualitiesEnvironment<Variable, Expression> New(SparseRationalArray[] JoinedMatrix,
      int numberOfRowsInResult, int p, VarsToDimensions freshMap,
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
    {
      Contract.Requires(JoinedMatrix != null);
      Contract.Requires(freshMap != null);
      Contract.Requires(expManager != null);

      return new LinearEqualitiesEnvironment<Variable, Expression>(JoinedMatrix, numberOfRowsInResult, p, freshMap, expManager);
    }

    #endregion

    #region ToString

    [ContractVerification(false)]
    public T To<T>(IFactory<T> factory)
    {
      if (this.IsBottom)
        return factory.Constant(false);
      if (this.IsTop)
        return factory.Constant(true);

      var result = factory.IdentityForAnd;

      for (int i = 0; i < this.numberOfRows; i++)
      {
        result = factory.And(result, To(i, factory));
      }

      return result;
    }

    [ContractVerification(false)]
    private T To<T>(int i, IFactory<T> factory)
    {
      var result = factory.IdentityForAdd;

      var coefficients = new List<Rational>();
      var variables = new List<Variable>();

      for (int j = 0; j < this.NumberOfColumns - 1; j++)
      {

        Variable var;

#if false
        if (this.varsToDimensions.TryGetValue(j, out var) && this.equations.At(i, j).IsNotZero)
        {
          var k = factory.Constant(this.equations.At(i, j));
          var x = factory.Variable(var);

          result = factory.Add(result, factory.Mul(k, x));
        }
#endif
        if (this.varsToDimensions.TryGetValue(j, out var))
        {
          var k = this.equations.At(i, j);
          if(k.IsNotZero)
          {
            coefficients.Add(k);
            variables.Add(var);
          }
        }
      }

      Contract.Assert(coefficients.Count == variables.Count);

      var knownValue = this.equations.At(i, this.NumberOfColumns - 1);

      coefficients.Add(knownValue);

      var normalizedCoefficients = NormalizeCoeffiecients(coefficients);
      if (normalizedCoefficients != null)
      {
        for (var j = 0; j < normalizedCoefficients.Count - 1 /* the last element is the known value */ ; j++)
        {
          var coeff = normalizedCoefficients[j];
          if (coeff > 0)
          {
            result = factory.Add(result, factory.Mul(factory.Constant(coeff), factory.Variable(variables[j])));
          }
          else
          {
            Contract.Assume(coeff < 0);
            result = factory.Sub(result, factory.Mul(factory.Constant(-coeff), factory.Variable(variables[j])));
          }
        }
        
        result = factory.EqualTo(result, factory.Constant(normalizedCoefficients.Last()));
      }
      else // if something went wrong, we simply return the raw equation
      {
        for (int j = 0; j < this.NumberOfColumns - 1; j++)
        {

          Variable var;
          if (this.varsToDimensions.TryGetValue(j, out var) && this.equations.At(i, j).IsNotZero)
          {
            var k = factory.Constant(this.equations.At(i, j));
            var x = factory.Variable(var);

            result = factory.Add(result, factory.Mul(k, x));
          }
        }

        result = factory.EqualTo(result, factory.Constant(knownValue));
      }
      return result;
    }

    [ContractVerification(false)]
    private List<Rational> NormalizeCoeffiecients(List<Rational> coefficients)
    {
      Contract.Requires(coefficients != null);
      Contract.Ensures(Contract.Result<List<Rational>>() == null || Contract.Result<List<Rational>>().Count == coefficients.Count);

      try
      {
        var mul = 1L;
        foreach (var c in coefficients)
        {
          var down = c.Down;
          long min, max;
          if (down < mul)
          {
            min = down;
            max = mul;
          }
          else
          {
            min = mul;
            max = down;
          }
          if (min % max != 0)
          {
            mul *= c.Down;
          }
        }

        Contract.Assume(mul != 0);

        return coefficients.ApplyToAll<Rational, Rational>(c => c * mul).ToList();
      }
      catch (ArithmeticExceptionRational)
      {
        return null;
      }
    }

    public override string ToString()
    {
      if (this.IsTop)
      {
        return "Top (Karr) ";
      }

      // Pretty print the constraints
      var result = new StringBuilder();

      result.AppendLine("Linear Equalities:");
#if VERBOSEOUTPUT
      result.AppendLine(string.Format("Vars -> Dimensions map" + Environment.NewLine + "{0}", this.varsToDimensions));

      result.AppendLine(this.IsConsistent ? "Consistent" : "Warning: The representation is NOT consistent");
#endif

      if (this.state == LinearEqualitiesState.Bottom)
      {
        result.AppendLine("_|_");
        return result.ToString();
      }

      // We want to sort the lines, to obtain a more deterministic debugging output
      var lines = new List<List<string>>();

      for (int i = 0; i < this.numberOfRows; i++)
      {
        var equation = new List<string>();

        for (var j = 0; j < this.NumberOfColumns - 1; j++)
        {
          if(this.varsToDimensions.ContainsValue(j))
          {
            var variable = this.varsToDimensions.KeyForValue(j);
            var value = this.equations.At(i, j);
            var monomial = new StringBuilder();

            if (value == 0)
            {
              monomial.Append(" ");
            }
            else
            {
              if (value != 1)
              {
                monomial.Append(value == -1 ? "-" : value.ToString() + " ");
              }
              var asString = this.ToString(variable);
              monomial.Append(asString);
            }
            equation.Add(monomial.ToString());
          }
        }

        var constant = this.equations.At(i, NumberOfColumns-1);

        equation.Add(" = ");
        equation.Add(constant.ToString());

        lines.Add(equation);
      }

      // Compute the column widths
      var widths = new List<int>();
      foreach (var line in lines)
      {
        var i = 0;
        foreach (var monom in line)
        {
          var max = Math.Max(monom.Length, widths.ElementAtOrDefault(i));
          if (i < widths.Count)
          {
            widths[i] = max;
          }
          else
          {
            widths.Add(max);
          }
          i++;
        }
      }

      var formattedlines = new List<string>();

      foreach (var line in lines)
      {
        var formattedLine = new StringBuilder();
        for (var i = 0; i < line.Count; i++)
        {
          var mask = "{0," + widths[i] +"}";
          var formattedString = string.Format(mask, line[i]);
          formattedLine.Append(formattedString);
        }
        formattedlines.Add(formattedLine.ToString());
      }
      
      return Print(this.state, formattedlines);
    }

    static private string Print(LinearEqualitiesState state, List<string> strs)
    {
      var sb = new StringBuilder();
      sb.AppendFormat("State : {0}\n", state);
      foreach (var s in strs)
      {
        sb.AppendLine(s);
      }

      return sb.ToString();
    }

    public string ToString(Expression exp)
    {
      if (this.expManager.Decoder!= null)
      {
        return ExpressionPrinter.ToString(exp, this.expManager.Decoder);
      }
      else
      {
        return "< missing expression decoder >";
      }
    }

    public string ToString(Variable v)
    {
      return this.expManager.Decoder.NameOf(v);
    }

    #endregion

    #region IAbstractDomain Members
    /// <summary>
    /// Return true iff this set of constraints is contradictory
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsBottom
    {
      get
      {
        return PerformanceMeasure.Measure<bool>(
          PerformanceMeasure.ActionTags.KarrIsBottom,
        () =>
        {
          return this.IsEmpty();
        }
          );
      }
    }

    /// <summary>
    /// Return true iff this set set of constraints is empty
    /// </summary>
    public bool IsTop
    {
      get
      {
        return this.numberOfRows == 0;      // No contstraint => top element
      }
    }

    /// <summary>
    /// In order to check if <code>this \leq a</code>, we do <code>this.Meet(a)</code>, and check if the matrix is this one
    /// </summary>
    public bool LessEqual(IAbstractDomain a)
    {
      bool result;
      if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out result))
      {
        return result;
      }

      var right = a as LinearEqualitiesEnvironment<Variable, Expression>;

      Debug.Assert(a != null, "I was expecting an instance of " + this.GetType().ToString() + ". I found " + a.GetType().ToString());

      var meet = this.Meet(right);

      if (meet.IsBottom)
      {
        return false;     // It means that the two elements are not comparable
      }

      Debug.Assert(!meet.IsTop, "It is strange that the meet, at this point is top...");

      var meetAsLE = meet as LinearEqualitiesEnvironment<Variable, Expression>;
      Debug.Assert(meetAsLE != null, "I was expecting an instance of " + this.GetType().ToString() + ". I found " + meet.GetType().ToString());


      // Now we check that the meet == this;

      if (this.NumberOfRows != meetAsLE.NumberOfRows)
      {
        return false;
      }

      if (!this.equations.IsNullRow(0) && !meetAsLE.equations.IsNullRow(0))
      {
        if (this.equations.RowLength(0) != meetAsLE.equations.RowLength(0))
        {
          return false;
        }
        else
        {
          UniformEnvironments(this, meetAsLE);
          this.PutIntoWeakRowEchelonForm();
          meetAsLE.PutIntoWeakRowEchelonForm();

          for (int row = 0; row < this.numberOfRows; row++)
          {
            for (int col = 0; col < this.NumberOfColumns; col++)
            {
              Rational l, r;

              var bl = this.equations.IsIndexOfNonDefaultElement(row, col, out l);
              var br = meetAsLE.equations.IsIndexOfNonDefaultElement(row, col, out r);

              if (bl && br && l != r)
              {
                return false;
              }
            }
          }
          return true;
        }
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Returns a contradictory system
    /// </summary>
    public IAbstractDomain Bottom
    {
      get
      {
        var bott = New(1, 1, this.expManager);

        var b = SparseRationalArray.New(2, 0);
        b[0] = Rational.For(0);
        b[1] = Rational.For(1);

        bott.AddConstraint(b);

        return bott;
      }
    }

    /// <summary>
    /// Returns a system without constraints
    /// </summary>
    public IAbstractDomain Top
    {
      get
      {
        return New(1, 3, this.expManager);
      }
    }

    /// <summary>
    /// The join follows the [Kar76] algorithm.
    /// The algorithm works in place, and it modifies the receiver <b>AND</b> <code>a</code>
    /// </summary>
    /// <returns>The disjunction of the two</returns>
    IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
    {
      var other = a as LinearEqualitiesEnvironment<Variable, Expression>;
      Contract.Assume(other != null);
      return this.Join(other);
    }

    public LinearEqualitiesEnvironment<Variable, Expression> Join(LinearEqualitiesEnvironment<Variable, Expression> a)
    {
      Contract.Requires(a != null);
      Contract.Ensures(Contract.Result<LinearEqualitiesEnvironment<Variable, Expression>>() != null);

      var obj = new List<Polynomial<Variable, Expression>>();
      return Join(a, ref obj, ref obj);
    }

    virtual internal LinearEqualitiesEnvironment<Variable, Expression> Join(IAbstractDomain a, ref List<Polynomial<Variable, Expression>> deletedLeft, ref List<Polynomial<Variable, Expression>> deletedRight)
    {
      IAbstractDomain/*!*/ trivialresult;
      if (AbstractDomainsHelper.TryTrivialJoin(this, a, out trivialresult))
      {
        if (trivialresult.IsTop)
        {
          deletedLeft.AddRange(this.ToPolynomial(false));
          deletedRight.AddRange((a as LinearEqualitiesEnvironment<Variable, Expression>).ToPolynomial(false));
        }
        return trivialresult as LinearEqualitiesEnvironment<Variable, Expression>;
      }

      var right = a as LinearEqualitiesEnvironment<Variable, Expression>;

      Contract.Assert(a != null, "I was expecting an instance of " + this.GetType().ToString() + ". I found " + a.GetType().ToString());

      if (this.equations.AreSharingTheEquations(this, right))
      {
        return New(this);
      }

      LogPerformance("Equalities x == y at join point (left arg) : {0}/{1}", this.CountPairwiseEqualities().ToString(), this.NumberOfRows.ToString());
      LogPerformance("Equalities x == y at join point (rightarg) : {0}/{1}", right.CountPairwiseEqualities().ToString(), right.NumberOfRows.ToString());

      // 1. Make the domains the same, by uniforming the maps, and swapping columns
      this.PutIntoRowEchelonForm();
      this.RecoverUnusedColumns(8);

      right.PutIntoRowEchelonForm();
      right.RecoverUnusedColumns(8);

      UniformEnvironments(this, right);

      this.PutIntoWeakRowEchelonForm();
      right.PutIntoWeakRowEchelonForm();

      LogPerformance("Unused variables at join point (left arg of join) : {0}", this.UnusedColumns.ToString());
      LogPerformance("Unused variables at join point (right arg of join) : {0}", right.UnusedColumns.ToString());

      if (this.state == LinearEqualitiesState.Bottom)
      {
        return New(right);
      }

      if (right.state == LinearEqualitiesState.Bottom)
      {
        return New(this);
      }

      Contract.Assert(this.NumberOfColumns == right.NumberOfColumns);

      // 2. The join is computed according to Karr 1976 paper
      var A = this.equations.GetClonedEquations();      // We clone the matrixes as Karr works with side effects
      var B = right.equations.GetClonedEquations();

      int numberOfRowsInResult;
      var JoinedMatrix = DisjunctionOfLinearSpaces(A, B, this.NumberOfRows, right.numberOfRows, out numberOfRowsInResult, ref deletedLeft, ref deletedRight);

      // 3. Create a clone of the current private state
      var freshMap = new VarsToDimensions(this.varsToDimensions);

      var result = New(JoinedMatrix, numberOfRowsInResult, this.NumberOfColumns, freshMap, this.expManager);
      result.state = LinearEqualitiesState.Normal; // the condition implied by Karr's algorithm is weaker than what we call now row echelon form

      LogPerformance("Unused variables after join point : {0}", result.UnusedColumns.ToString());
      UpdateMaxUnusedVariables(result.UnusedColumns);

      LogPerformance("Equalities x == y at return from join : {0}/{1}", result.CountPairwiseEqualities().ToString(), result.NumberOfRows.ToString());

      result.RecoverUnusedColumns();

      return result;
    }

    /// <summary>
    /// The matrix can grow very large, and get many unused entries. 
    /// This method recover the unused space if there are more than 2 * GrowSizeForColumns unused columns 
    /// </summary>
    protected void RecoverUnusedColumns()
    {
      RecoverUnusedColumns(2 * GrowSizeForColumns);
    }

    /// <summary>
    /// The matrix can grow very large, and get many unused entries. 
    /// This method recover the unused space.
    /// </summary>
    protected void RecoverUnusedColumns(int treshold)
    {
      var isConsistent = this.IsConsistent;

      var matrix = this.equations.Unshare(this, ref this.equations);

      Contract.Assert(matrix.Length >= this.NumberOfRows);

      if (this.NumberOfColumns - this.varsToDimensions.Count > treshold)
      {
        LogPerformance("Performing column recovering. Unused dims : {0}", this.UnusedColumns.ToString());

        var newMatrix = new SparseRationalArray[this.NumberOfRows];
        var newMappings = new VarsToDimensions();

        var newNumberOfColumns = this.varsToDimensions.Count + 1;
        for (int row = 0; row < this.NumberOfRows; row++)
        {
          newMatrix[row] = FreshRow(newNumberOfColumns);
        }

        var nextFreePos = 0;

        for (int i = 0; i < this.NumberOfColumns - 1; i++)
        {
          if (!IsZeroCol(matrix, this.NumberOfRows, i))
          {
            // 1. Update the mapping var <-> dim
            newMappings[this.varsToDimensions.KeyForValue(i)] = nextFreePos;

            // 2. Copy the column
            for (int row = 0; row < this.NumberOfRows; row++)
            {
              newMatrix[row][nextFreePos] = matrix[row][i];
            }

            nextFreePos++;
          }
        }

        // 3. Update the column of the coefficients
        for (int row = 0; row < this.NumberOfRows; row++)
        {
          newMatrix[row][newNumberOfColumns - 1] = matrix[row][NumberOfColumns - 1];
        }

        this.equations = new LinearEquations<Variable, Expression>(this, newMatrix);
        this.varsToDimensions = newMappings;
        this.numberOfCols = newNumberOfColumns;
      }
      else
      {
        LogPerformance("No columns to recover, treshold not reached. Unused columns: {0}; Treshold: {1} > {2}",
          this.UnusedColumns.ToString(), (this.NumberOfColumns - this.varsToDimensions.Count).ToString(), treshold.ToString());
      }

      CheckConsistency(isConsistent);
    }

    /// <summary>
    /// The meet of two system of equations just puts the two system one beneath the other.
    /// It modifies <b>BOTH</b>the receiver and <code>a</code>
    /// </summary>
    /// <returns></returns>
    public IAbstractDomain Meet(IAbstractDomain a)
    {
      IAbstractDomain trivialMeet;
      if (AbstractDomainsHelper.TryTrivialMeet(this, a, out trivialMeet))
      {
        return trivialMeet;
      }

      var right = a as LinearEqualitiesEnvironment<Variable, Expression>;
      Debug.Assert(a != null, "I was expecting an instance of " + this.GetType().ToString() + ", I found a " + a.GetType().ToString());

      if (this.equations.AreSharingTheEquations(this, right))
      {
        return New(this);
      }

      // 1. Make the domains the same, by uniforming the maps, and swapping columns
      UniformEnvironments(this, right);

      Debug.Assert(this.NumberOfColumns == right.NumberOfColumns);

      // 2. The meet is just putting one matrix beneath the other
      var result = new SparseRationalArray[this.numberOfRows + right.numberOfRows];

      int numberOfCols = this.NumberOfColumns;

      // Copy all the constraints
      for (int i = 0; i < this.numberOfRows; i++)
      {
        result[i] = SparseRationalArray.New(numberOfCols, 0);

        this.equations.CopyRowTo(i, result[i], numberOfCols);
      }
      for (int i = 0; i < right.numberOfRows; i++)
      {
        int index = i + this.numberOfRows;
        result[index] = SparseRationalArray.New(numberOfCols, 0);

        right.equations.CopyRowTo(i, result[index], numberOfCols);
      }

      var vars2Dim = new VarsToDimensions(this.varsToDimensions);

      return New(result, result.Length, numberOfCols, vars2Dim, this.expManager);
    }

    /// <summary>
    /// As the abstract domain of linear equalities satisfies the ACC, the widening is just the join
    /// </summary>
    /// <param name="prev">The previous value</param>
    /// <returns>The join of this with <code>prev</code></returns>
    IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
    {
      return this.Join((LinearEqualitiesEnvironment<Variable, Expression>)prev);
    }

    public LinearEqualitiesEnvironment<Variable, Expression> Widening(LinearEqualitiesEnvironment<Variable, Expression> prev)
    {
      return this.Join((LinearEqualitiesEnvironment<Variable, Expression>)prev);
    }

    #endregion

    #region INumericalAbstractDomain<Rational,Expression> Members

    /// <returns>
    /// An interval for the expression <code>v</code>.
    /// The interval is one of the two: a singleton or top.
    /// </returns>
    public DisInterval BoundsFor(Expression v)
    {
      var directTry = this.BoundsForQuick(v);

      // Give another chance to fins a bound for
      IExpressionEncoder<Variable, Expression> encoder;
      if (this.expManager.TryGetEncoder(out encoder) && !directTry.IsNormal() && !this.expManager.Decoder.IsVariable(v) )
      {
        // Fast path
        Polynomial<Variable, Expression> polFast;
        if (Polynomial<Variable, Expression>.TryToPolynomialForm(v, this.expManager.Decoder, out polFast)
          && polFast.IsLinear && !polFast.IsTautology)
        {
          var result = Interval.For(0);

          foreach (var mon in polFast.Left)
          {
            if (mon.IsConstant)
            {
              result = result + mon.K;
            }
            else
            {
              var boundForX = BoundsFor(mon.VariableAt(0)).AsInterval;
              if (boundForX.IsTop)
              {
                goto slowPath;
              }

              if (mon.K > 0)
              {
                result = result + boundForX;
              }
              else
              {
                result = result - boundForX;
              }
            }
          }

          return result.AsDisInterval;
        }


        // Slow path
      slowPath:
        var tmpVar = encoder.FreshVariable<int>();

        Polynomial<Variable, Expression> pol;
        if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.Equal, tmpVar, v, this.expManager.Decoder, out pol))
        {
          if (pol.IsLinear && !pol.IsTautology)
          {
            var dup = this.Duplicate();

            dup = dup.TestTrue(pol);

            return dup.BoundsForQuick_Internal(tmpVar);
          }
        }
      }

      return directTry;
    }

    public DisInterval BoundsFor(Variable v)
    {
      return this.BoundsForQuick_Internal(v);
    }

    private DisInterval BoundsForQuick(Expression v)
    {
      var decoder = this.expManager.Decoder;
      int k;
      if (decoder.IsConstantInt(v, out k))
      {
        return DisInterval.For(k);
      }

      return BoundsForQuick_Internal(decoder.UnderlyingVariable(v));
    }

    private DisInterval BoundsForQuick_Internal(Variable v)
    {
      Contract.Ensures(Contract.Result<DisInterval>() != null);

      if (!this.varsToDimensions.ContainsKey(v))
      {
        return DisInterval.UnknownInterval;
      }

      this.PutIntoRowEchelonForm();
      var colForv = this.varsToDimensions[v];

      Rational r;

      for (int row = 0; row < this.numberOfRows; row++)
      {
        if (this.equations.At(row, colForv).IsNotZero)
        {
          // Check if there is some other nonzero variable
          foreach (var pair in this.equations.GetElementsForRow(row))
          {
            int nextCol = pair.Key;
            if (nextCol == colForv || nextCol == this.NumberOfColumns - 1)
              continue;

            goto exit;
          }

          r = this.equations.At(row, this.NumberOfColumns - 1);

          return DisInterval.For(r);
        }
      }

    exit:
      return DisInterval.UnknownInterval;
    }

    public List<Pair<Variable, Int32>> IntConstants
    {
      get
      {
        return new List<Pair<Variable, Int32>>();
      }
    }

    public IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
    {
      yield break; //not implemented
    }

    public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
    {
      yield break; //not implemented
    }

    public IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict)
    {
      yield break; //not implemented
    }

    public IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict)
    {
      yield break; //not implemented
    }

    public IEnumerable<Variable> EqualitiesFor(Variable v)
    {
      yield break; //not implemented -- too expensive
    }

    #region IPureExpressionAssignmentsWithForward<Expression> Members

    /// <summary>
    /// The assignment for the domain. 
    /// If <code>exp</code> is a linear equaality, then we are done.
    /// Otherwise, we project the variable <code>x</code>
    /// </summary>
    /// <param name="x">The variable to be assigned</param>
    /// <param name="exp">The value for the variable</param>
    public void Assign(Expression x, Expression exp)
    {
      this.Assign(x, exp, TopNumericalDomain<Variable, Expression>.Singleton);
    }

    public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      Contract.Assert(this.IsConsistent, "LinEq inconsistent @ Assign");

      var xVar = this.ExpressionManager.Decoder.UnderlyingVariable(x);

      Polynomial<Variable, Expression> pol;

      if (!Polynomial<Variable, Expression>.TryToPolynomialForm(exp, this.ExpressionManager.Decoder, out pol))
      {
        return;
      }

      if (pol.Relation != null)
      {
        return;
      }

      if (pol.IsLinear)
      {
        var colForX = GetDimensionFor(xVar);
        var polAsVector = AsVector(pol);
        var coeffForX = polAsVector[colForX];

        #region Case 1. It is an invertible assignment
        if (pol.Variables.Contains(xVar))
        {
          var matrix = this.equations.Unshare(this, ref this.equations);

          for (int row = 0; row < this.NumberOfRows; row++)
          {
            var k = (matrix[row][colForX] / coeffForX);

            // Process the coefficents of the variables
            for (int col = 0; col < this.NumberOfColumns - 1; col++)
            {
              if (col != colForX)
              {
                matrix[row][col] = matrix[row][col] - k * polAsVector[col];
              }
              else
              {
                matrix[row][col] = k;
              }
            }
            // Process the constant
            matrix[row][this.NumberOfColumns - 1] = matrix[row][this.NumberOfColumns - 1] - k * polAsVector[this.NumberOfColumns - 1];
          }

          this.equations = new LinearEquations<Variable, Expression>(this, matrix);
        }
        #endregion

        #region Case 2. It is not invertible
        else
        {
          this.ProjectVariable(xVar);
          var newConstraint = ConstraintFor(xVar, pol);

          Debug.Assert(newConstraint.Length == this.NumberOfColumns);

          this.AddConstraint(newConstraint);
        }
        #endregion
      }
      else
      { // Nothing to do, so we project it
        this.ProjectVariable(xVar);
      }
    }

    public void AssumeInDisInterval(Variable x, DisInterval value)
    {
      if (this.varsToDimensions.ContainsKey(x))
        this.ProjectVariable(x);
    }

    virtual public void AssumeDomainSpecificFact(DomainSpecificFact fact)
    {
    }

    private int GetDimensionFor(Variable x)
    {
      if (!this.varsToDimensions.ContainsKey(x))
        this.AddVariable(x);

      return this.varsToDimensions[x];
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    /// <summary>
    /// The variables defined in this linear equality
    /// </summary>
    public List<Variable> Variables
    {
      get
      {
        return new List<Variable>(this.varsToDimensions.Keys);
      }
    }

    protected IEnumerable<Variable> Variables_Internal
    {
      get
      {
        return this.varsToDimensions.Keys;
      }
    }

    public bool ContainsKey(Variable var)
    {
      return this.varsToDimensions.ContainsKey(var);
    }

    /// <summary>
    /// Add a variable to this set of linear equalities
    /// </summary>
    /// <param name="var">The name of the variable to add</param>
    public void AddVariable(Variable var)
    {
      int dummy;
      AddVariable(var, out dummy);
    }

    public void AddVariable(Variable var, out int dim)
    {
      if (this.varsToDimensions.TryGetValue(var, out dim))
      {
        return;
      }
      else
      {
        this.AddVariable_Internal(var, out dim);
      }
    }

    private void AddVariable_Internal(Variable var, out int freshCol)
    {
      if (this.TryGetAFreshDimension(out freshCol))
      {
        this.equations.ZeroCol(this, freshCol, ref this.equations);
        this.varsToDimensions[var] = freshCol;
      }
      else
      {
        this.AddColumnsToTheEnvironment(GrowSizeForColumns);
        this.AddVariable_Internal(var, out freshCol);
      }
    }

    /// <summary>
    /// Project the value of the variable from this set of linear equalities
    /// </summary>
    /// <param name="var">The variable to be projected</param>
    public void ProjectVariable(Variable var)
    {
      if (!this.varsToDimensions.ContainsKey(var))
      {
        //The variable to project is not in this linear equalities
        return;
      }
      else
      {
        this.PutIntoRowEchelonForm();
        this.ProjectDimension(this.varsToDimensions[var]);
      }
    }

    public void RemoveVariable(Variable var)
    {
      RemoveVariable(var, false);
    }

    /// <summary>
    /// Remove the variable from the constraints.
    /// The mapping from variables to dimensions may be changed if and only if <paramref name="preserveVariableMappings"/> is false
    /// </summary>
    /// <param name="var">The variable to be removed</param>
    /// <param name="preserveVariableMappings">Make it true if you rely on the order of the variables</param>
    public void RemoveVariable(Variable var, bool preserveVariableMappings)
    {
      Polynomial<Variable, Expression> row;
      int dim;
      RemoveVariable(var, preserveVariableMappings, out row, out dim);
    }

    public void RemoveVariable(Variable var, bool preserveVariableMappings, out Polynomial<Variable, Expression> deleted, out int dim)
#if TRACEPERFORMANCE
    {
      Polynomial<Variable, Expression> deletedTMP = default(Polynomial<Variable, Expression>);
      int dimTMP = -1 ;

      var dummy = PerformanceMeasure.Measure<int>(PerformanceMeasure.ActionTags.KarrRemoveVars, () => { RemoveVariableInternal(var, preserveVariableMappings, out deletedTMP, out dimTMP); return -1; });

      deleted = deletedTMP;
      dim = dimTMP;
    }
    public void RemoveVariableInternal(Variable var, bool preserveVariableMappings, out Polynomial<Variable, Expression> deleted, out int dim)
#endif
    {
      var b = this.IsConsistent;

      if (!this.varsToDimensions.ContainsKey(var))
      {
        dim = 0;
        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(new Monomial<Variable>[0], out deleted))
        {
          throw new AbstractInterpretationException("Impossible case");
        }

        return;
      }

      if (!preserveVariableMappings)
      {
        this.PutIntoRowEchelonForm();
      }

      // Cannot use this.varsToDimensions.TryGetValue(...) as PutIntoRowEchelonForm could have changed the mapping
      // Cannot have dim = this.varsToDimensions[var] as ProjectDimension can change the mappings  

      this.ProjectDimension(this.varsToDimensions[var], preserveVariableMappings, out deleted);              // Project the dimensions

      dim = this.varsToDimensions[var];

      this.varsToDimensions.Remove(var);                                              // Remove the variable from the mapping

      Contract.Assert(b ? this.IsConsistent : true, "LinEq not consistent @ ExitPoint of RemoveVariable");    // We want to check it only if at the entry point it was in a valid state
    }

    public void RemoveVariables(List<Variable> var, bool preserveVariableMappings)
    {
      if (var.Count == 0)
      {
        return;
      }

      if (this.varsToDimensions.Count == var.Count)
      {
        this.varsToDimensions = new VarsToDimensions();
        this.equations = new LinearEquations<Variable, Expression>(this, 0);
        this.numberOfRows = 0;

        return;
      }

      var ratio = var.Count / (this.varsToDimensions.Count - var.Count);
      if (ratio >= MAX_RATIO_FOR_USING_WEAK_PROJECTION || var.Count > MAX_VARIABLES_TO_REMOVE_FOR_USING_WEAK_PROJECTION)
      {
        this.expManager.Log(string.Format("Too many variables to remove (variables = {0}, ratio = {1}), giving up a precise projection", var.Count, ratio));

        this.PutIntoRowEchelonForm();

        var indexes = new bool[this.NumberOfColumns];
        foreach (var v in var)
        {
          int dim;
          if (this.varsToDimensions.TryGetValue(v, out dim))
          {
            indexes[dim] = true;
            this.varsToDimensions.Remove(v);
          }
        }

        var matrix = this.equations.Unshare(this, ref this.equations);
        for (var rowCount = 0; rowCount < this.NumberOfRows; rowCount++)
        {
          foreach (var pair in matrix[rowCount].GetElements())
          {
            if (indexes[pair.Key])
            {
              RemoveRow(rowCount);
              rowCount--;
              break;
            }
          }
        }
      }
      else
      {
        foreach (var x in var)
        {
          this.RemoveVariable(x, preserveVariableMappings);
        }
      }
    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {
      if (OldName.Equals(NewName))
      {
        return;
      }

      int dim;
      if (varsToDimensions.TryGetValue(OldName, out dim))
      {
        this.varsToDimensions[NewName] = dim;
        this.varsToDimensions.Remove(OldName);
      }
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

     public IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {

      return this.testTrue.Visit(guard, this);
    }

    public LinearEqualitiesEnvironment<Variable, Expression> TestTrue(Polynomial<Variable, Expression> pol)
    {
      if (pol.IsLinear && !pol.IsTautology)
      {
        var asVector = this.AsVector(pol.LeftAsPolynomial);
        asVector[asVector.Length - 1] = pol.Right[0].K;

        this.AddConstraint(asVector);
      }
      return this;
    }

    public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      return this.testFalse.Visit(guard, this);
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression exp)
    {
      return this;
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
    {
      return this;
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueLessEqualThan(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      return this;
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueEqual(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      return this.TestTrueEqual(exp1, exp2);
    }

    public LinearEqualitiesEnvironment<Variable, Expression> TestTrueEqual(Expression left, Expression right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      Contract.Ensures(Contract.Result<LinearEqualitiesEnvironment<Variable, Expression>>() != null);

      CheckStateInvariant();

      var decoder = this.ExpressionManager.Decoder;
      var varLeft = decoder.UnderlyingVariable(left);
      var varRight = decoder.UnderlyingVariable(right);

      if (varLeft.Equals(varRight))
      {
        return this;
      }

      if (decoder.IsSlackOrFrameworkVariable(varLeft) &&
              decoder.IsSlackOrFrameworkVariable(varRight))
      // 1. Add left == right
      {
        int leftConst, rightConst;
        if (decoder.IsConstantInt(left, out leftConst) && decoder.IsVariable(right))
        {
          this.AddConstraintVariableEqConst(varRight, Rational.For(leftConst));

          return this;
        }

        if (decoder.IsConstantInt(right, out rightConst) && decoder.IsVariable(left))
        {
          this.AddConstraintVariableEqConst(varLeft, Rational.For(rightConst));

          return this;
        }

        this.AddConstraintVariableEqVariable(varLeft, varRight);

        if (decoder.IsAtomicExpression(left) && decoder.IsAtomicExpression(right))
        {
          return this;
        }
      }
      // 2. add left == right by going inside them
      Polynomial<Variable, Expression> tmpPoly;
      if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.Equal, left, right, this.ExpressionManager.Decoder, out tmpPoly))
      {
        if (tmpPoly.IsLinear && !tmpPoly.IsTautology)
        {
          AddConstraintPolynomialEqConstant(tmpPoly.LeftAsPolynomial, tmpPoly.Right[0].K);
        }
      }

      return this;
    }

    public LinearEqualitiesEnvironment<Variable, Expression> TestTrueEqual(Variable left, Expression right)
    {
      CheckStateInvariant();

      if (this.ExpressionManager.Decoder.IsVariable(right))
      {
        var rightVar = this.ExpressionManager.Decoder.UnderlyingVariable(right);
        this.AddConstraintVariableEqVariable(left, rightVar);

        return this;
      }

      int value;
      if (this.ExpressionManager.Decoder.IsConstantInt(right, out value))
      {
        this.AddConstraintVariableEqConst(left, Rational.For(value));

        return this;
      }

      Polynomial<Variable, Expression> tmpPoly;

      if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.Equal, left, right, this.ExpressionManager.Decoder, out tmpPoly))
      {
        if (tmpPoly.IsLinear && !tmpPoly.IsTautology)
        {
          var asVector = this.AsVector(tmpPoly.LeftAsPolynomial);
          asVector[asVector.Length - 1] = tmpPoly.Right[0].K;

          this.AddConstraint(asVector);
        }
      }
      return this;
    }

    #endregion

    #region Checking
    /// <summary>
    /// Check if the condition <code>exp</code> holds for this set of linear equalities
    /// </summary>
    /// <param name="exp"></param>
    /// <returns></returns>
    public FlatAbstractDomain<bool> CheckIfHolds(Expression/*!*/ exp)
    {
      return this.checkIfHolds.Visit(exp, this);
    }

    /// <summary>
    /// Check if the expression <code>exp</code> is greater than zero
    /// </summary>
    public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      var boundForExp = this.BoundsFor(exp);

      if (!boundForExp.IsNormal())
        return CheckOutcome.Top;

      if (boundForExp.LowerBound >= 0)
        return CheckOutcome.True;

      return CheckOutcome.Top;
    }

    /// <summary>
    /// Check if the expression <code>e1</code> is strictly smaller than <code>e2</code>
    /// </summary>
    public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      if (e1.Equals(e2))
      {
        return CheckOutcome.False;
      }

      IExpressionEncoder<Variable, Expression> encoder;
      if(this.ExpressionManager.TryGetEncoder(out encoder))
      {
        // e2 - e1
        var e2SubE1 = this.BoundsFor(encoder.SmartSubtraction(e2, e1, this.ExpressionManager.Decoder));

        if (!e2SubE1.IsNormal())
          return CheckOutcome.Top;

        if (e2SubE1.LowerBound > 0)
          return CheckOutcome.True;

        if (e2SubE1.UpperBound <= 0)
          return CheckOutcome.False;
      }

      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    /// <summary>
    /// Check if the expression <code>e1</code> is strictly smaller than <code>e2</code>
    /// </summary>
    public FlatAbstractDomain<bool> CheckIfLessThan(Variable e1, Variable e2)
    {
      if (e1.Equals(e2))
      {
        return CheckOutcome.False;
      }

      IExpressionEncoder<Variable, Expression> encoder;
      if (this.ExpressionManager.TryGetEncoder(out encoder))

      {
        // e2 - e1
        var e2SubE1 = this.BoundsFor(encoder.SmartSubtraction(e2, e1, this.ExpressionManager.Decoder));

        if (!e2SubE1.IsNormal())
          return CheckOutcome.Top;

        if (e2SubE1.LowerBound > 0)
          return CheckOutcome.True;

        if (e2SubE1.UpperBound <= 0)
          return CheckOutcome.False;
      }

      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
    {
      return this.CheckIfLessThan(e1, e2);
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      if (e1.Equals(e2))
      {
        return CheckOutcome.True;
      }

      IExpressionEncoder<Variable, Expression> encoder;
      if (this.ExpressionManager.TryGetEncoder(out encoder))
      {
        // e2 - e1
        var e2SubE1 = this.BoundsFor(encoder.SmartSubtraction(e2, e1, this.ExpressionManager.Decoder));

        if (!e2SubE1.IsNormal())
          return CheckOutcome.Top;

        if (e2SubE1.LowerBound >= 0)
          return CheckOutcome.True;

        if (e2SubE1.UpperBound < 0)
          return CheckOutcome.False;
      }

      return CheckOutcome.Top;
    }


    /// <summary>
    /// For the moment we return Unknown. We want to refine it
    /// </summary>
    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable e1, Variable e2)
    {
      if (e1.Equals(e2))
      {
        return CheckOutcome.True;
      }
 
      IExpressionEncoder<Variable, Expression> encoder;
      if (this.ExpressionManager.TryGetEncoder(out encoder))
      {
        // e2 - e1
        var e2SubE1 = this.BoundsFor(encoder.SmartSubtraction(e2, e1, this.ExpressionManager.Decoder));

        if (!e2SubE1.IsNormal())
          return CheckOutcome.Top;

        if (e2SubE1.LowerBound >= 0)
          return CheckOutcome.True;

        if (e2SubE1.UpperBound < 0)
          return CheckOutcome.False;
      }

      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
    {
      return this.CheckIfLessEqualThan(e1, e2);
    }

    public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
    {
      IExpressionEncoder<Variable, Expression> encoder;
      if (this.ExpressionManager.TryGetEncoder(out encoder))
      {
        // e2 - e1
        var e2SubE1 = this.BoundsFor(encoder.SmartSubtraction(e2, e1, this.ExpressionManager.Decoder));

        if (!e2SubE1.IsNormal())
          return CheckOutcome.Top;

        if (e2SubE1.IsSingleton)
        {
          return e2SubE1.LowerBound.IsZero ? CheckOutcome.True : CheckOutcome.False;
        }
      }

      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
    {
      IExpressionEncoder<Variable, Expression> encoder;
      if (this.ExpressionManager.TryGetEncoder(out encoder))
      {
        // e2 - e1
        var e2SubE1 = this.BoundsForQuick(encoder.SmartSubtraction(e2, e1, this.ExpressionManager.Decoder));

        if (!e2SubE1.IsNormal())
          return CheckOutcome.Top;

        if (e2SubE1.IsSingleton)
        {
          return e2SubE1.LowerBound.IsZero ? CheckOutcome.True : CheckOutcome.False;
        }
      }

      return CheckOutcome.Top;
    }

    /// <summary>
    /// Check if exp != 0
    /// </summary>
    public FlatAbstractDomain<bool> CheckIfNonZero(Expression exp)
    {
      IExpressionEncoder<Variable, Expression> encoder;
      if (this.ExpressionManager.TryGetEncoder(out encoder))
      {
        var zero = encoder.ConstantFor(0);
        var nonZeroExp = encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.NotEqual, exp, zero);

        return this.CheckIfHolds(nonZeroExp);
      }
      else
      {
        return CheckOutcome.Top;
      }
    }

    #endregion

    #region Basic Operations

    protected bool TryGetAFreshDimension(out int freshcol)
    {
      if (this.varsToDimensions.TryGetFirstAvailableDimension(out freshcol) 
        && freshcol < this.NumberOfColumns - 1)
      {
        return true;
      }
      return false;
    }

    public SparseRationalArray AddConstraintEqual(Variable x, Rational v)
    {
      this.AddVariable(x);
      var newRow = FreshRow(this.NumberOfColumns);
      newRow[this.varsToDimensions[x]] = Rational.For(1);
      newRow[this.NumberOfColumns - 1] = v;

      this.AddConstraint(newRow, true);

      return newRow;
    }

    private void AddConstraintPolynomialEqConstant(Polynomial<Variable, Expression> pol, Rational k)
    {
      if (pol.ContainsDummyVariables(this.ExpressionManager.Decoder))
      {
        return;
      }

      var asVector = this.AsVector(pol);
      asVector[asVector.Length - 1] = k;

      this.AddConstraint(asVector);
    }

    private void AddConstraintVariableEqVariable(Variable left, Variable right)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);

      if (left.Equals(right))
      {
        return;
      }

      // Adding the two variables
      this.AddVariable(left);
      this.AddVariable(right);

      var freshRow = FreshRow(this.NumberOfColumns);
      freshRow[this.varsToDimensions[left]] = Rational.For(1);
      freshRow[this.varsToDimensions[right]] = Rational.For(-1);
      freshRow[this.numberOfCols - 1] = Rational.For(0);

      this.AddConstraint(freshRow, true);
    }

    private void AddConstraintVariableEqConst(Variable left, Rational k)
    {
      Contract.Requires(left != null);
      Contract.Requires(!object.ReferenceEquals(k, null));

      this.AddVariable(left);
      var freshRow = FreshRow(this.NumberOfColumns);
      freshRow[this.varsToDimensions[left]] = Rational.For(1);
      freshRow[this.numberOfCols - 1] = k;

      this.AddConstraint(freshRow, true);
    }

    public void AddConstraint(SparseRationalArray newConstraint)
    {
      AddConstraint(newConstraint, false);
    }

    public void AddConstraint(Polynomial<Variable, Expression> pol)
    {
      var newRow = this.AsVector(pol);
      this.AddConstraint(newRow);
    }

    /// <summary>
    /// Add the new constraint to this set of linear equalities.
    /// We copy the pointer of the vector, so you do not want to modify it!
    /// </summary>
    /// <param name="newConstraint">A vector of size <code>this.NumberOfVariables</code></param>    
    public void AddConstraint(SparseRationalArray newConstraint, bool isNormalized)
    {
      Contract.Assert(this.NumberOfColumns == 0 || newConstraint.Length == this.NumberOfColumns,
        "You must add to the LE a vector of the same size");

      // Here there used to be some checks for redundancy. It seems it works without, deserve some testing before refactoring
      CheckStateInvariant();

      AddConstraintInternal(newConstraint, isNormalized);

      CheckStateInvariant();
    }

    private void AddConstraintInternal(SparseRationalArray newConstraint, bool isNormalized)
    {
      var matrix = this.equations.Unshare(this, ref this.equations);

      if (this.NumberOfRows == matrix.Length)
      { // We have no more rows for adding a constraint, so we enlarge it
        Array.Resize(ref matrix, matrix.Length * 2 + 1);

        this.equations = new LinearEquations<Variable, Expression>(this, matrix);

        AddConstraintInternal(newConstraint, isNormalized);
      }
      else
      {
        var normalizedNewConstraint = isNormalized ? newConstraint : NormalizeConstraint(newConstraint);

        matrix[numberOfRows] = normalizedNewConstraint;
        this.numberOfRows++;

        if (this.NumberOfRows == 1)
        {
          this.state = LinearEqualitiesState.Normal;
          this.equations = new LinearEquations<Variable, Expression>(this, matrix);

          this.PutIntoRowEchelonForm();

          CheckStateInvariant();

          return;
        }

        Rational val;
        if (this.state == LinearEqualitiesState.StrongRowEchelon
          && this.NumberOfRows <= this.NumberOfColumns
          && normalizedNewConstraint.IsIndexOfNonDefaultElement(this.numberOfRows - 1, out val)
          && val == 1)
        {
          // We want to see if the new constraint preserves the Row echelon form

          if (normalizedNewConstraint.SmallestIndexOfNonDefaultElement < this.numberOfRows - 1)
          {
            goto Normal;
          }

          for (int row = 0; row < this.numberOfRows - 2; row++)
          {
            if (matrix[row].IsIndexOfNonDefaultElement(this.numberOfRows - 1))
            {
              goto Normal;
            }
          }

          this.state = LinearEqualitiesState.StrongRowEchelon;
          this.equations = new LinearEquations<Variable, Expression>(this, matrix);

          CheckStateInvariant();
          return;
        }

      Normal:
        // If we reach this point, the constraint breaks the row echelon form
        this.state = LinearEqualitiesState.Normal;
        this.equations = new LinearEquations<Variable, Expression>(this, matrix);

        CheckStateInvariant();
      }
    }

    private static SparseRationalArray NormalizeConstraint(SparseRationalArray newConstraint)
    {
      var result = SparseRationalArray.New(newConstraint.Length, 0);

      int pivotKey = newConstraint.SmallestIndexOfNonDefaultElement;
      if (pivotKey >= 0)
      {
        var pivot = newConstraint[pivotKey];

        Contract.Assert(pivot.IsNotZero);

        // Already normalized?
        if (pivot == 1)
        {
          return newConstraint;
        }

        foreach (var pair in newConstraint.GetElements())
        {
          // result[pair.Key] = pair.Value / pivot;
          Rational newVal;
          if (!Rational.TryDiv(pair.Value, pivot, out newVal))
          {
            return newConstraint;
          }
          result[pair.Key] = newVal;
        }

        return result;
      }
      else
      {
        return newConstraint;
      }
    }

    [Conditional("CHECKINVARIANTS")]
    internal void CheckStateInvariant()
    {
      if (this.state != LinearEqualitiesState.StrongRowEchelon)
        return;

      for (int row = 0; row < this.NumberOfRows; row++)
      {
        Debug.Assert(this.equations.At(row, row) == 1);

        for (int col = 0; col < row; col++)
        {
          Debug.Assert(this.equations.At(row, col).IsZero);
        }
      }
    }

    [Pure]
    static internal void CheckHaveUniformEnvironment(LinearEqualitiesEnvironment<Variable, Expression> left, LinearEqualitiesEnvironment<Variable, Expression> right)
    {
      Debug.Assert(left.Variables.Count == right.Variables.Count);

      foreach (var x in left.Variables)
      {
        Debug.Assert(right.Variables.Contains(x));

        var xDim = left.varsToDimensions[x];
        var yDim = right.varsToDimensions[x];

        Debug.Assert(yDim == xDim);
      }
    }

    [Conditional("CHECKINVARIANTS")]
    [Pure]
    internal void CheckConsistency(bool expected)
    {
      if (!expected)
      {
        return;
      }

      Contract.Assert(this.IsConsistent);
    }

    [Conditional("CHECKINVARIANTS")]
    [Pure]
    protected void CheckAreAllZeroAt(SparseRationalArray[] matrix, Variable var, int row, bool shouldBreak)
    {
      if (!AreAllZeroAt(matrix, var, row))
      {
        if (shouldBreak)
        {
          System.Diagnostics.Debugger.Break();
        }
        else
        {
          Contract.Assert(false);
        }
      }
    }

    [Pure]
    protected bool AreAllZeroAt(SparseRationalArray[] matrix, Variable var, int row)
    {
      var oldCol = this.varsToDimensions[var];
      for (int i = 0; i < this.NumberOfRows; i++)
      {
        if (matrix[i] == null)
          continue;   // this can be a bug, but we abstract away from it

        if (i != (row) && matrix[i][oldCol].IsNotZero)
        {
          return false;
        }
      }
      return true;
    }

    /// <summary>
    /// Put the current set of equalities into a row echelon form. 
    /// This is a stronger condition than the one in Karr 1976, because the first <code>NumberOfRows</code> columns 
    /// will represent the identity matrix
    /// The <code>varsToDimensions</code> map may be modified.
    /// </summary>
   
    public void PutIntoRowEchelonForm()
#if TRACEPERFORMANCE2
    {
      PerformanceMeasure.Measure<int>(PerformanceMeasure.ActionTags.KarrPutIntoRowEchelonForm, () => {PutIntoRowEchelonFormInternal(); return 0; }, true);
    }
    
    public void PutIntoRowEchelonFormInternal()
#else
#endif
    {
      CheckStateInvariant();
      var expectedConsistency = this.IsConsistent;

      if (this.state == LinearEqualitiesState.StrongRowEchelon || this.state == LinearEqualitiesState.Bottom)
      {
        return;
      }

      var matrix = this.equations.Unshare(this, ref this.equations);

      try
      {
        int i, j;
        i = 0;    // row
        j = 0;    // col
        var timeout = this.expManager.TimeOut;

        while (i < this.NumberOfRows && j < this.NumberOfColumns - 1)
        {
          timeout.CheckTimeOut("Row Echelon mode computation");

          if (matrix[i][j].IsZero)
          {
            for (int r = i + 1; r < this.NumberOfRows; r++)
            {
              if (matrix[r][j].IsNotZero)
              { // We swap the rows
                Swap(ref matrix[i], ref matrix[r]);

                goto continueNormally;
              }
            }

            if (j < this.NumberOfColumns - 2)
            {
              j++;

              continue;
            }
            else
            { // Here we know that all remaining rows are zeros : checking consistency, then deleting them
              this.state = LinearEqualitiesEnvironment<Variable, Expression>.LinearEqualitiesState.StrongRowEchelon;

              for (int u = i; u < this.NumberOfRows; u++)
              {
                //if this.Matrix[u][this.NumberOfColumns - 1] != 0
                if (matrix[u].IsIndexOfNonDefaultElement(this.NumberOfColumns - 1))
                {
                  this.state = LinearEqualitiesEnvironment<Variable, Expression>.LinearEqualitiesState.Bottom;
                  return;
                }

              }
              numberOfRows = i;

              return;
            }
          }

          CheckConsistency(expectedConsistency);

        continueNormally:

          int v;
          var this_matrix_i_ = matrix[i];
          var mij = this_matrix_i_[j];
          var caughtException = false;

          // divide each entry of the vector this.matrix[i,*] by this.matrix[i, j];
          if (mij != 1)
          {
            // for (k = 0; k < this.NumberOfColumns; k++)
            var indexes = new Set<int>(this_matrix_i_.IndexesOfNonDefaultElements);

            foreach (int t in indexes)
            {
              // Optimization: locality
              Rational div;
              if (!Rational.TryDiv(this_matrix_i_[t], mij, out div) || div.IsInfinity)
              {
                this.RemoveRow(i);
                caughtException = true;

                break;
              }
              this_matrix_i_[t] = div;
            }
          }

          if (caughtException)
          {
            continue;
          }

          CheckConsistency(expectedConsistency);

          // apply the linear combination to all the other vectors
          for (v = 0; v < this.NumberOfRows; v++)
          {
            // Rational initial = this.matrix[v][i];
            var initial = matrix[v][j];

            if (v == i || initial.IsZero)
            {
              continue;
            }

            var this_matrix_v_ = matrix[v];

            for (int k = 0; k < this.NumberOfColumns; k++)
            {
              Rational comb;
              Rational this_matrix_v_k, this_matrix_i_k;

              var b1 = this_matrix_v_.IsIndexOfNonDefaultElement(k, out this_matrix_v_k);
              var b2 = this_matrix_i_.IsIndexOfNonDefaultElement(k, out this_matrix_i_k);

              if (!b1 && !b2)
              {
                continue;
              }

              try
              {
                if (b1)
                {
                  if (b2)
                  {
                    comb = this_matrix_v_k - this_matrix_i_k * initial;
                  }
                  else
                  {
                    comb = this_matrix_v_k;
                  }
                }
                else
                { // We know b2 is true here
                  comb = -this_matrix_i_k * initial;
                }
              }
              catch (ArithmeticExceptionRational)
              {
                CleanUp(ref i, ref v);
                break;
              }

              if (comb.IsInfinity)
              {
                CleanUp(ref i, ref v);
                break;
              }
              else
              {
                matrix[v][k] = comb;
              }
            }
          }

          CheckConsistency(expectedConsistency);

          SwapColumnsAndVariables(i, j);

          CheckConsistency(expectedConsistency);

          i++;
          j++;
        }

        this.state = LinearEqualitiesState.StrongRowEchelon;
        for (int u = i; u < this.NumberOfRows; u++)
        {
          // if this.Matrix[u][this.NumberOfColumns - 1] != 0
          if (matrix[u].IsIndexOfNonDefaultElement(this.NumberOfColumns - 1))
          {
            this.state = LinearEqualitiesEnvironment<Variable, Expression>.LinearEqualitiesState.Bottom;
            return;
          }
        }

        numberOfRows = i;

        CheckConsistency(expectedConsistency);
      }
      catch (ArithmeticException)
      {
        Contract.Assert(false, "Exception should have been caught earlier");
      }


      CheckStateInvariant();
    }

    private void CleanUp(ref int i, ref int v)
    {
      Contract.Requires(i >= 0);

      if (v < i)
      {
        for (int z = v; z < i; z++)
        {
          SwapColumnsAndVariables(z, z + 1);
        }
        i--;
      }
      this.RemoveRow(v);
      v--;
    }

    /// <summary>
    /// Put the current set of equalities into a row echelon form. This puts the matrix in the form used in Karr's algorithm ; the state is not changed to StrongRowEchelon.
    /// This method ensures that the <code>varsToDimensions</code> map is not changed.
    /// </summary>
    /* private */
    public void PutIntoWeakRowEchelonForm()
    {
      if (this.state == LinearEqualitiesState.StrongRowEchelon || this.state == LinearEqualitiesState.Bottom)
      {
        return;
      }
  
      int i, j;
      i = 0;    // row
      j = 0;    // col

      var matrix = this.equations.Unshare(this, ref this.equations);

      while (i < this.NumberOfRows && j < this.NumberOfColumns - 1)
      {
        this.expManager.TimeOut.CheckTimeOut("Row Echelon mode");

        if (matrix[i][j].IsZero)
        {
          for (int r = i + 1; r < this.NumberOfRows; r++)
          {
            if (matrix[r][j].IsNotZero)
            { // We swap the rows
              Swap(ref matrix[i], ref matrix[r]);

              goto continueNormally;
            }
          }

          if (j < this.NumberOfColumns - 2)
          {
            j++;
            continue;
          }
          else
          { // Here we now that all remaining rows are zeros : checking consistency, then deleting them
            this.state = LinearEqualitiesEnvironment<Variable, Expression>.LinearEqualitiesState.StrongRowEchelon;
            for (int u = i; u < this.NumberOfRows; u++)
            {
              if (matrix[u].IsIndexOfNonDefaultElement(this.NumberOfColumns - 1))
              {
                this.state = LinearEqualitiesEnvironment<Variable, Expression>.LinearEqualitiesState.Bottom;
                return;
              }
            }
            numberOfRows = i;
            return;
          }
        }

      continueNormally:

        int k, v;
        var mij = matrix[i][j];
        bool caughtException = false;

        // divide each entry of the vector this.matrix[i,*] by this.matrix[i, j];
        if (mij != 1)
        {

          //for (k = 0; k < this.NumberOfColumns; k++)
          var indexes = new Set<int>(matrix[i].IndexesOfNonDefaultElements);
          foreach (var t in indexes)
          {
            // this.matrix[i][t] = this.matrix[i][t] / mij;
            Rational value;

            if (!Rational.TryDiv(matrix[i][t], mij, out value) || value.IsInfinity)
            {
              this.RemoveRow(i);
              caughtException = true;

              break;
            }

            matrix[i][t] = value;
          }
        }

        if (caughtException)
        {
          continue;
        }

        // apply the linear combination to all the other vectors
        for (v = 0; v < this.NumberOfRows; v++)
        {
          // Rational initial = this.matrix[u][i];
          var initial = matrix[v][j];

          if (v == i || initial.IsZero)
          {
            continue;
          }

          for (k = 0; k < this.NumberOfColumns; k++)
          {

            Rational r1, r2;
            var b1 = matrix[v].IsIndexOfNonDefaultElement(k, out r1);
            var b2 = matrix[i].IsIndexOfNonDefaultElement(k, out r2);

            if (!b1 && !b2)
            {
              continue;
            }

            try
            {
              // this.matrix[v][k] = this.matrix[v][k] - this.matrix[i][k] * initial;

              matrix[v][k] = r1 - r2 * initial;

              if (matrix[v][k].IsInfinity)
              {
                CleanUp_PutIntoWeakEchelonForm(ref i, ref v);
              }
            }
            catch (ArithmeticException)
            {
              // ALog.WriteStatistics("Arithmetic Exception", "in row combination"); // used to find occurrences using VERBOSEOUTPUT, loglevel 0
              CleanUp_PutIntoWeakEchelonForm(ref i, ref v);

              break;
            }
          }
        }
        i++;
        j++;
      }

      if (this.numberOfRows > this.numberOfCols - 1)
      {
        this.numberOfRows = this.numberOfCols - 1;
      }

    }

    private void CleanUp_PutIntoWeakEchelonForm(ref int i, ref int v)
    {
      Contract.Requires(v >= 0);

      this.RemoveRow(v);
      v--;
      if (v < i)
      {
        i--;
      }
    }

    /// <summary>
    /// Check if the concretization of the current set of linear inequalities is empty or not
    /// </summary>
    private bool IsEmpty()
    {
      if (this.state == LinearEqualitiesState.Bottom)
      {
        return true;
      }

      if (this.state != LinearEqualitiesState.StrongRowEchelon)
      {
        this.PutIntoRowEchelonForm();
      }

      if (this.state == LinearEqualitiesState.Bottom)
      {
        return true;
      }

#if false
      var rows = this.NumberOfRows;
      var cols = this.NumberOfColumns;
      var eqs = this.equations;
      for (int i = 0; i < rows; i++)
      {
#if false
        // for (int j = 0; j < this.NumberOfColumns - 1; j++)
        foreach (KeyValuePair<int, Rational> pair in this.matrix[i].GetElements())
        {
          if (pair.Key < this.NumberOfColumns - 1)
          {
            // skip this vector
            goto Skip;
          }
        }
        // If we reach this point, all the this.matrix[i, 0..this.NumbersOfColums-1) == 0. 
        // So we check if this.matrix[i, this.NumbersOfColums-1] != 0, which implies the contraddiction
        if (this.matrix[i][this.NumberOfColumns - 1].IsNotZero)
        {
          this.state = LinearEqualitiesState.Bottom;
          return true;
        }
      Skip:
        ;
#else
        if (eqs.RowCount(i) <= 1 && eqs.At(i, cols - 1).IsNotZero)
        {
          this.state = LinearEqualitiesState.Bottom;
          return true;
        }
#endif

      }
#endif
      return false;
    }

    /// <summary>
    /// Uniform the two maps, so that \forall x \in dom(left.varsToDimensions) \cap dom(right.varsToDimensions). left.varsToDimensions(x) == right.varsToDimensions(x).
    /// It works with side effects
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    protected static void UniformEnvironments(LinearEqualitiesEnvironment<Variable, Expression> left, LinearEqualitiesEnvironment<Variable, Expression> right)
    {
      Contract.Assert(left.IsConsistent);
      Contract.Assert(right.IsConsistent);

      var leftMap = left.varsToDimensions;
      var rightMap = right.varsToDimensions;

      var allvariables = new Set<Variable>(leftMap.Keys);
      allvariables.AddRange(rightMap.Keys);

      left.expManager.Log("Uniforming the two environments");
      left.expManager.Log(string.Format("Left argument contains {0} variables", left.Variables.Count));
      left.expManager.Log(string.Format("Right argument contains {0} variables", right.Variables.Count));


      // We can use lists as the code below will guarantee that the three data structures are disjoints
      var toRemoveLeft = new List<Variable>();
      var toRemoveRight = new List<Variable>();
      var intersection = new List<Variable>();

      foreach (var x in allvariables)
      {
        var inLeft = leftMap.ContainsKey(x);
        var inRight = rightMap.ContainsKey(x);

        if (inLeft && inRight)
        { // If both environments have the variable x, we want it to point to the same dimension
          intersection.Add(x);
        }
        else
        { // It is a variable that we want to project
          if (inLeft)
            toRemoveLeft.Add(x);
          else if (inRight)
            toRemoveRight.Add(x);
          else
            Contract.Assert(false, "Impossible case?");
        }
      }

      Contract.Assert(allvariables.Count == intersection.Count + toRemoveLeft.Count + toRemoveRight.Count);

      left.expManager.Log(string.Format("Intersection contains {0} variables", intersection.Count));

      left.RemoveVariables(toRemoveLeft, false);
      left.RecoverUnusedColumns();

      right.RemoveVariables(toRemoveRight, false);
      right.RecoverUnusedColumns();

      foreach (var toUniform in intersection)
      {
        int leftDim, rightDim;

        var leftMapContainsToUniform = left.varsToDimensions.TryGetValue(toUniform, out leftDim);
        var rightMapContainsToUniform = right.varsToDimensions.TryGetValue(toUniform, out rightDim);

        if (leftMapContainsToUniform && rightMapContainsToUniform)
        {
          // this can be false for slack variables removed by dependencies.
          right.SwapColumnsAndVariables(leftDim, rightDim);
        }
        else
        {
          // Removing the variables above can also have removed some variable in the intersection

          if (leftMapContainsToUniform)
            left.RemoveVariable(toUniform, true);

          if (rightMapContainsToUniform)
            right.RemoveVariable(toUniform, true);
        }
      }

      Contract.Assert(left.IsConsistent);
      Contract.Assert(right.IsConsistent);

      if (left.NumberOfColumns != right.NumberOfColumns)
      {
        int newSize = Math.Min(left.NumberOfColumns, right.NumberOfColumns);

        left.RemoveColumnsTo(newSize);
        right.RemoveColumnsTo(newSize);
      }

      left.expManager.Log(string.Format("After uniforming, left contains {0}", left.Variables.Count));
      left.expManager.Log(string.Format("After uniforming, right contains {0}", right.Variables.Count));

      Contract.Assert(left.IsConsistent);
      Contract.Assert(right.IsConsistent);
    }

    public void RemoveColumnsTo(int newSize)
    {
      Contract.Requires(newSize >= 0);

      if (newSize == this.NumberOfColumns)
      {
        return;
      }

      var matrix = this.equations.Unshare(this, ref this.equations);

      for (int row = 0; row < matrix.Length; row++)
      {
        if (matrix[row] != null)
        {
          var k = matrix[row][matrix[row].Length - 1];
          var shorterRow = FreshRow(newSize);

          shorterRow.CopyFrom(matrix[row], newSize);

          shorterRow[newSize - 1] = k;

          matrix[row] = shorterRow;
        }
      }

      this.NumberOfColumns = newSize;

      Contract.Assert(this.IsConsistent);
    }

    /// <summary>
    /// Project the dimension <paramref name="d"/>, with the option to preserve the variable mappings, which corresponds to not assuming strong row echelon form.
    /// </summary>
    /// <param name="d">The dimension to project</param>
    /// <param name="preserveVariableMappings">set it to true if you may not assume strong row echelon form</param>
    private void ProjectDimension(int d, bool preserveVariableMappings)
    {
      Polynomial<Variable, Expression> row;
      ProjectDimension(d, preserveVariableMappings, out row);
    }

    private void ProjectDimension(int d, bool preserveVariableMappings, out Polynomial<Variable, Expression> deleted)
    {
      if (preserveVariableMappings)
      {
        if (Polynomial<Variable, Expression>.TryToPolynomialForm(new Monomial<Variable>[0], out deleted))
        {
          SwapColumnsAndVariables(0, d);
          PutIntoWeakRowEchelonForm();

          var matrix = this.equations.Unshare(this, ref this.equations);

          for (int row = 0; row < this.numberOfRows; row++)
          {
            var currRow = matrix[row];
            if (currRow[0].IsNotZero)
            {
              deleted = ToPolynomial(currRow);
              this.RemoveRow(row);
              row--;
              this.state = LinearEqualitiesEnvironment<Variable, Expression>.LinearEqualitiesState.Normal;
            }
          }
        }
        SwapColumnsAndVariables(0, d);
      }
      else
      {
        ProjectDimension(d, out deleted);
      }
    }

    /// <summary>
    /// Project the dimension <code>d</code> from this system of equalities
    /// </summary>
    /// <param name="d">The dimension to be projected</param>
    private void ProjectDimension(int d)
    {
      Polynomial<Variable, Expression> row;
      ProjectDimension(d, out row);
    }

    private void ProjectDimension(int d, out Polynomial<Variable, Expression> deleted)
    {
      Contract.Requires(d >= 0);

      Contract.Requires(d < this.NumberOfColumns - 1);

      // Requires the state is RowEchelonForm ; otherwise putting into row echelon form might give a different d
      Contract.Requires(this.state != LinearEqualitiesEnvironment<Variable, Expression>.LinearEqualitiesState.Normal, "Call PutIntoRowEchelonForm before projecting");


      // 2. Find the row that will enable us to put d in the basis
      var allZero = true;
      var nonZeroRow = -1;
      for (var row = 0; row < this.numberOfRows; row++)
      {
        if (this.equations.At(row, d).IsNotZero)
        {
          allZero = false;
          nonZeroRow = row;

          break;
        }
      }
      if (allZero)
      {
        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(new Monomial<Variable>[0], out deleted))
        {
          throw new AbstractInterpretationException("Impossible case");
        }

        return;
      }

      // 3. Put the dimension in basis
      int exceptionRow;

      if (!this.TrySwapColumnsInRowEchelonForm(nonZeroRow, d, out exceptionRow))
      {
        this.RemoveRow(exceptionRow);
      }

      // 4. Remove all the constraints that contain a non-zero entry for the dimension d
      // (should be only one if b is true)
      Polynomial<Variable, Expression>.TryToPolynomialForm(new Monomial<Variable>[0], out deleted);

      var matrix = this.equations.Unshare(this, ref this.equations);

      for (int row = 0; row < this.numberOfRows; row++)
      {
        if (matrix[row][nonZeroRow].IsNotZero)
        {
          deleted = ToPolynomial(matrix[row]);
          this.RemoveRow(row);
          row--;
          this.state = LinearEqualitiesEnvironment<Variable, Expression>.LinearEqualitiesState.Normal;
        }
      }
    }

    public SparseRationalArray[] DisjunctionOfLinearSpaces(SparseRationalArray[] A, SparseRationalArray[] B, out Int32 nRows)
    {
      var delLeft = new List<Polynomial<Variable, Expression>>();
      var delRight = new List<Polynomial<Variable, Expression>>();

      return DisjunctionOfLinearSpaces(A, B, A.Length, B.Length, out nRows, ref delLeft, ref delRight);
    }

    /// <summary>
    /// The union of linear spaces according to the Karr's algorithm.
    /// Please note that the Karr's algorithm works with side effects on <code>A</code> and <code>B</code>
    /// </summary>
    /// <param name="A">A matrix in row echelon form</param>
    /// <param name="B">A matrix in row echelon form</param>
    public SparseRationalArray[] DisjunctionOfLinearSpaces(SparseRationalArray[] A, SparseRationalArray[] B,
      int ALength, int BLength,
      out Int32 nRows,
      ref List<Polynomial<Variable, Expression>> deletedInLeft, ref List<Polynomial<Variable, Expression>> deletedInRight)
    {
      Contract.Requires(A != null);
      Contract.Requires(B != null);

      Contract.Ensures(Contract.Result<SparseRationalArray[]>() != null);

      bool swapped = false;
      int nDeletedLeft = 0;
      int nDeletedRight = 0;

      // If one of the two matrixes has no constraints, then we return it
      if (A.Length == 0 || A[0] == null || IsZeroRow(A[0]))
      {
        nRows = 0;
        for (int i = 0; i < B.Length && B[i] != null && !IsZeroRow(B[i]); i++)
        {
          deletedInRight.Add(ToPolynomial(B[i]));
        }

        if (swapped)
        {
          Swap(ref deletedInLeft, ref deletedInRight);
        }

        return A;
      }

      if (B.Length == 0 || B[0] == null || IsZeroRow(B[0]))
      {
        nRows = 0;
        for (int i = 0; i < A.Length && A[i] != null && !IsZeroRow(A[i]); i++)
        {
          deletedInLeft.Add(ToPolynomial(A[i]));
        }

        if (swapped)
        {
          Swap(ref deletedInLeft, ref deletedInRight);
        }

        return B;
      }

      // The number of columns
      int nCols = A[0].Length;

      // We just use it, and we do not create the C matrix, unlike the Karr's algorithm

      int rowsInC = -1;

      for (int s = 0; s < nCols; s++)
      {
        int r = rowsInC + 1;

        #region Case 0 A is empty or B is empty
        if (A[0] == null || IsZeroRow(A[0]))
        {
          nRows = 0;

          for (int i = 0; i < B.Length && B[i] != null && !IsZeroRow(B[i]); i++)
          {
            deletedInRight.Add(ToPolynomial(B[i]));
          }

          if (swapped)
          {
            Swap(ref deletedInLeft, ref deletedInRight);
          }
          return A;
        }

        if (B[0] == null || IsZeroRow(B[0]))
        {
          nRows = 0;

          for (int i = 0; i < A.Length && A[i] != null && !IsZeroRow(A[i]); i++)
          {
            deletedInLeft.Add(ToPolynomial(A[i]));
          }

          if (swapped)
          {
            Swap(ref deletedInLeft, ref deletedInRight);
          }

          return B;
        }
        #endregion

        bool ArsIsZero = r == ALength || A[r] == null || A[r][s].IsZero;
        bool BrsIsZero = r == BLength || B[r] == null || B[r][s].IsZero;
        bool ArsIsOne = r < ALength && A[r] != null && A[r][s] == 1;
        bool BrsIsOne = r < BLength && B[r] != null && B[r][s] == 1;

        #region Case 1
        if (ArsIsOne && BrsIsOne)
        {
          this.expManager.Log("Applied case 1");

          // C[r][s] = 1;

          rowsInC++;
        }
        #endregion

        #region Case 2
        else if ((ArsIsOne && BrsIsZero) || (ArsIsZero && BrsIsOne))
        {
          this.expManager.Log("Applied case 2");

          // Swap the matrixes so to make the two cases symmetrical
          if (BrsIsOne)
          {
            swapped = !swapped;

            // Swap the matrixes
            Swap(ref A, ref B);

            // Swap the lengths
            Swap(ref ALength, ref BLength);

            // Swap the deleted values
            Swap(ref deletedInLeft, ref deletedInRight);
            Swap(ref nDeletedLeft, ref nDeletedRight);
          }

          // Do the linear combination for all the rows over it

          for (int i = 0; i < r; i++)
          {
            var bi = B[i][s];

            var newRow = SparseRationalArray.New(nCols, 0);

            if (bi.IsNotZero)
            {
              try
              {
                for (int j = 0; j < nCols; j++)
                {
                  //   newRow[j] = A[r][j] * bi + A[i][j];
                  // We optimize it with some inling
                  Rational A_r_j, A_i_j;
                  if (A[r].IsIndexOfNonDefaultElement(j, out A_r_j))
                  {
                    if (A[i].IsIndexOfNonDefaultElement(j, out A_i_j))
                    {
                      newRow[j] = A_r_j * bi + A_i_j;
                    }
                    else
                    {
                      newRow[j] = A_r_j * bi;
                    }
                  }
                  else
                  {
                    if (A[i].IsIndexOfNonDefaultElement(j, out A_i_j))
                    {
                      newRow[j] = A_i_j;
                    }
                    else
                    {
                      continue;
                    }
                  }
                }

                A[i] = newRow;
              }
              catch (ArithmeticExceptionRational)
              {
                // Deleting row i in A and B
                RemoveRow(A, i);
                RemoveRow(B, i);

                ALength--;
                BLength--;
                r--;
                i--;
                rowsInC--;
              }
            }
          }

          // Delete the row A[r]
          deletedInLeft.Add(ToPolynomial(A[r]));
          nDeletedLeft++;

          RemoveRow(A, r);

        }
        #endregion

        #region Case 3
        else if (ArsIsZero && BrsIsZero)
        {
          this.expManager.Log("Applied case 3");

          bool simpleCase = true;
          int t = -1;
          for (int k = 0; k < r; k++)
          {
            if (A[k][s] != B[k][s])
            {
              try
              {
                // we try to catch the exception caused by A[t][s] - B[t][s] here because it would be harder to remove the right row later
                var diff = A[k][s] - B[k][s];
                simpleCase = false;
                t = k;      // we need this information!
              }
              catch (ArithmeticExceptionRational)
              { // deleting row k in A and B

                RemoveRow(A, k);
                RemoveRow(B, k);

                ALength--;
                BLength--;
                r--;
                k--;
                rowsInC--;
              }
            }
          }

          if (!simpleCase)
          {
            // Do some linear combinations
            for (int i = 0; i < t; i++)
            {
              var newRowInA = SparseRationalArray.New(nCols, 0);
              var newRowInB = SparseRationalArray.New(nCols, 0);
              try
              {
                var coeff = (A[i][s] - B[i][s]) / (A[t][s] - B[t][s]);

                if (coeff.IsNotZero)
                {
                  for (int j = 0; j < nCols; j++)
                  {
                    // newRowInA[j] = A[i][j] - coeff * A[t][j];
                    ApplyLinearCombinationInDisjunctionOfLinearSpaces(A, t, i, newRowInA, coeff, j);

                    // newRowInB[j] = B[i][j] - coeff * B[t][j];
                    ApplyLinearCombinationInDisjunctionOfLinearSpaces(B, t, i, newRowInB, coeff, j);
                  }

                  A[i] = newRowInA;
                  B[i] = newRowInB;
                }
              }
              catch (ArithmeticExceptionRational)
              {
                // deleting row i in A and B
                RemoveRow(A, i);
                RemoveRow(B, i);

                ALength--;
                BLength--;
                r--;
                i--;
                t--;
                rowsInC--;
              }
            }

            // Remove the row t from both matrixes
            deletedInLeft.Add(ToPolynomial(A[t]));
            nDeletedLeft++;
            deletedInRight.Add(ToPolynomial(B[t]));
            nDeletedRight++;

            RemoveRow(A, t);

            RemoveRow(B, t);

            A[ALength - 1] = FreshRow(nCols);
            B[BLength - 1] = FreshRow(nCols);

            rowsInC--;    // Check it!!!
          }
        }
        #endregion

        else if (A[r] != null && B[r] != null & A[r][s] != B[r][s])
        {
          // Remove the row r from both matrixes
          RemoveRow(A, r);
          RemoveRow(B, r);

          A[ALength - 1] = FreshRow(nCols);
          B[BLength - 1] = FreshRow(nCols);

          rowsInC--;    // Check it!!!
        }

        else
        {
          throw new AbstractInterpretationException("Unknown case in the join of two system of linear equations");
        }
      }

      nRows = rowsInC + 1;

      for (int i = nRows; i < ALength; i++)
      {
        A[i] = FreshRow(nCols);
      }

      Contract.Assert(nRows >= 0);
      Contract.Assert(nRows <= ALength);

      if (swapped)
      {
        Swap(ref deletedInLeft, ref deletedInRight);
      }

      return A;
    }

    private static void ApplyLinearCombinationInDisjunctionOfLinearSpaces(SparseRationalArray[] row, int t, int i, SparseRationalArray newRowInA, Rational coeff, int j)
    {
      Rational row_i_j, row_t_j;

      var b1 = row[i].IsIndexOfNonDefaultElement(j, out row_i_j);
      var b2 = row[t].IsIndexOfNonDefaultElement(j, out row_t_j);

      if (!b1 && !b2)
      {
        return;
      }

      if (b1)
      {
        if (b2)
        {
          newRowInA[j] = row_i_j - coeff * row_t_j;
        }
        else
        {
          newRowInA[j] = row_i_j;
        }
      }
      else if (b2)
      {
        newRowInA[j] = -coeff * row_t_j;
      }
      else
      {
        // unreachable code
      }
    }

    private static void Swap<T>(ref T a, ref T b)
    {
      T tmp = a;
      a = b;
      b = tmp;
    }

    /// <summary>
    /// Generate a row to be added to the matrix
    /// </summary>
    /// <param name="x"></param>
    /// <param name="pol">A linear polynomial</param>
    /// <returns></returns>
    private SparseRationalArray ConstraintFor(Variable x, Polynomial<Variable, Expression> pol)
    {
      Contract.Requires(pol.IsLinear);

      var result = FreshRow(this.NumberOfColumns);

      var colForX = this.varsToDimensions[x];

      result[colForX] = Rational.For(1);

      foreach (var m in pol.Left)
      {
        if (!m.IsConstant)
        {
          int col = this.varsToDimensions[m.VariableAt(0)];

          if (colForX != col)
          {
            Contract.Assert(result[col].IsZero, "You already have assigned this???");

            result[col] = -m.K;
          }
          else
          {
            Contract.Assert(result[col].IsZero, "You already have assigned this???");

            result[col] = m.K;
          }
        }
        else
        {
          Contract.Assert(result[this.NumberOfColumns - 1].IsZero, "You already have assigned this???");

          result[this.NumberOfColumns - 1] = m.K;
        }
      }

      return result;
    }

    /// <returns>
    /// A representation of <code>pol</code> in the form of a vector
    /// </returns>
    /// <param name="pol">A polynomial that must be already linear and in canonical form</param>
    internal SparseRationalArray AsVector(Polynomial<Variable, Expression> pol)
    {
      Contract.Requires(pol.IsLinear, "Expecting a linear polynomial");

      // It is important that we first add the variables, so that if there are not enough columns, the matrix can be enlarged
      foreach (var var in pol.Variables)
      {
        this.AddVariable(var);
      }

      var result = FreshRow(this.NumberOfColumns);

      switch (pol.Relation)
      {
        case null:
          foreach (var m in pol.Left)
          {
            if (!m.IsConstant)
            {
              var var = m.VariableAt(0);
              var col = this.varsToDimensions[var];
              Contract.Assert(result[col].IsZero, "You've already written the position " + col + " ?");

              result[col] = m.K;
            }
            else
            {
              var col = this.NumberOfColumns - 1;
              Contract.Assert(result[col].IsZero, "You've already written the position " + col + " ?");

              result[col] = -m.K;
            }
          }
          break;

        case ExpressionOperator.Equal:
        case ExpressionOperator.Equal_Obj:
          foreach (var m in pol.Left)
          {
            if (!m.IsConstant)
            {
              var var = m.VariableAt(0);
              var col = this.varsToDimensions[var];
              Contract.Assert(result[col].IsZero, "You've already written the position " + col + " ?");

              result[col] = m.K;
            }
            else
            {
              var col = this.NumberOfColumns - 1;
              Contract.Assert(result[col].IsZero, "You've already written the position " + col + " ?");

              result[col] = -m.K;
            }
          }

          var constant = pol.Right[0].K;
          int lastCol = this.NumberOfColumns - 1;

          Contract.Assert(result[lastCol].IsZero, "You've already written the position " + lastCol + " ?");

          result[lastCol] = constant;
          break;

        default:
          throw new AbstractInterpretationException("trying to convert a polynomial that doesn't correspond to an equality");
      }

      return result;
    }

    /// <summary>
    /// Add <code>howManyNewColumns</code> new columns to the matrix.
    /// It adds also the new columns to the available dimensions
    /// </summary>
    protected void AddColumnsToTheEnvironment(int howManyNewColumns)
    {
      Contract.Requires(howManyNewColumns >= 0);

      var columnsBefore = this.NumberOfColumns;
      var columnsAfter = columnsBefore + howManyNewColumns;

      var matrix = this.equations.Unshare(this, ref this.equations);

      for (int row = 0; row < matrix.Length; row++)
      {
        var theRow = matrix[row];

        if (theRow != null)
        {
          var k = theRow[this.NumberOfColumns - 1];

          theRow.ExpandBy(howManyNewColumns);

          theRow.ResetToDefaultElement(this.NumberOfColumns - 1);

          theRow[columnsAfter - 1] = k;
        }
      }

      this.NumberOfColumns = columnsAfter;
    }

    #endregion

    #region Helper functions for matrixes

    /// <summary>
    /// Swap the column x with y
    /// </summary>
    private void SwapColums(int x, int y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y >= 0);

      if (y > this.NumberOfColumns - 2)
      {
        this.AddColumnsToTheEnvironment(y - this.NumberOfColumns + 2);
      }

      if (x > this.NumberOfColumns - 2)
      {
        this.AddColumnsToTheEnvironment(x - this.NumberOfColumns + 2);
      }

      var matrix = this.equations.Unshare(this, ref this.equations);

      for (int i = 0; i < this.numberOfRows; i++)
      {
        Contract.Assume(matrix[i] != null);
        matrix[i].Swap(x, y);
      }

      this.state = LinearEqualitiesState.Normal;      // At this point it is no more in the row echelon form
    }

    private void SwapColumnsAndVariables(int x, int y)
    {
      if (x == y)
      {
        return;
      }

      Variable varX, varY;
      bool bX = varsToDimensions.TryGetValue(x, out varX);
      bool bY = varsToDimensions.TryGetValue(y, out varY);

      if (bX)
      {
        if (bY)
        {
          varsToDimensions[varX] = y;
          varsToDimensions[varY] = x;
        }
        else
        {
          varsToDimensions[varX] = y;
        }
      }
      else
      {
        if (bY)
        {
          varsToDimensions[varY] = x;
        }
      }

      SwapColums(x, y);
    }

    /// <summary>
    /// Swap the columns <paramref name="x"/> and <paramref name="y"/>, if possible, assuming that the state is RowEchelonForm, then puts the result in RowEchelonForm
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="exceptionRow">if an arithmetic exception occurred, it correspond to the row which threw the exception</param>
    /// <returns>true iff the swap was possible</returns>
    protected bool TrySwapColumnsInRowEchelonForm(int x, int y, out int exceptionRow)
    {
      exceptionRow = -1;

      if (x == y)
      {
        return true;
      }

      Contract.Assert(x >= 0);
      Contract.Assert(y >= 0);
      Contract.Assert(this.state != LinearEqualitiesEnvironment<Variable, Expression>.LinearEqualitiesState.Normal);

      var matrix = this.equations.Unshare(this, ref this.equations);

      if (x < this.NumberOfRows)
      {
        if (y < this.NumberOfRows)
        {
          Swap(ref matrix[x], ref matrix[y]);
          SwapColumnsAndVariables(x, y);
        }
        else
        {
          SwapColumnsAndVariables(x, y);
          var k = matrix[x][x];

          if (k.IsZero)
          {
            return false;
          }

          if (k != 1)
          {
            try
            {
              var row = matrix[x];
              var indexes = new List<int>(row.IndexesOfNonDefaultElements);

              foreach (int j in indexes)
              {
                // Optimization: locality
                var row_j = row[j] / k;

                if (row_j.IsInfinity)
                {
                  exceptionRow = x;
                  return false;
                }
                else
                {
                  row[j] = row_j;
                }
              }
            }
            catch (ArithmeticExceptionRational)
            {
              exceptionRow = x;
              return false;
            }
          }

          for (int i = 0; i < NumberOfRows; i++)
          {
            var row = matrix[i];

            var initial = row[x];

            if (i == x || initial.IsZero)
            {
              continue;
            }

            for (int l = 0; l < NumberOfColumns; l++)
            {
              if (!row.IsIndexOfNonDefaultElement(l) && !matrix[x].IsIndexOfNonDefaultElement(l))
              {
                continue;
              }

              try
              {
                // Optimization: locality
                var Matrix_i_l = row[l] - initial * matrix[x][l];

                if (Matrix_i_l.IsInfinity)
                {
                  exceptionRow = i;
                  return false;
                }
                else
                {
                  row[l] = Matrix_i_l;
                }
              }
              catch (ArithmeticExceptionRational)
              {
                exceptionRow = i;
                return false;
              }
            }

          }
        }
      }
      else
      {
        if (y < this.NumberOfRows)
        {
          return TrySwapColumnsInRowEchelonForm(y, x, out exceptionRow);
        }
        else
        {
          SwapColumnsAndVariables(x, y);
        }
      }

      this.state = LinearEqualitiesEnvironment<Variable, Expression>.LinearEqualitiesState.StrongRowEchelon;
      return true;
    }

    /// <summary>
    /// Remove the row r;
    /// </summary>
    protected void RemoveRow(int rowToDelete)
    {
      Contract.Requires(0 <= rowToDelete);
      Contract.Requires(rowToDelete < this.NumberOfRows);

      var matrix = this.equations.Unshare(this, ref this.equations);

      RemoveRow(matrix, rowToDelete);

      this.numberOfRows--;
    }

    protected static void RemoveRow(SparseRationalArray[] matrix, int rowToDelete)
    {
      Contract.Requires(matrix != null);
      Contract.Requires(0 <= rowToDelete);
      Contract.Requires(rowToDelete < matrix.Length);

      Array.Copy(matrix, rowToDelete + 1, matrix, rowToDelete, matrix.Length - rowToDelete - 1);
      matrix[matrix.Length - 1] = null;
    }

    /// <summary>
    /// A fresh row with <code>col</code> columns
    /// </summary>
    /// <param name="col">The number of columns of the vector</param>
    static protected SparseRationalArray FreshRow(int col)
    {
      return SparseRationalArray.New(col, 0);
    }

    /// <returns>true iff all the column <code>col</code> is zero</returns>
    [ContractVerification(true)]
    [Pure]
    static protected bool IsZeroCol(SparseRationalArray[] matrix, int rows, int col)
    {
      Contract.Requires(matrix != null);
      Contract.Requires(rows <= matrix.Length);
      Contract.Requires(col >= 0);
      Contract.Requires(Contract.ForAll(0, rows, i => matrix[i] != null));

      for (int row = 0; row < rows; row++)
      {
        var matrix_row = matrix[row];

        // Implied by the precondition
        /*
        if (matrix_row == null)
        {
          continue;
        }
        */
        if (matrix_row.IsIndexOfNonDefaultElement(col))
        {
          return false;
        }
      }

      return true;
    }

    [Pure]
    static protected bool IsZeroRow(SparseRationalArray row)
    {
      return row.NonDefaultElementsCount == 0;
    }

    [Pure]
    static protected bool IsConstantRow(SparseRationalArray row, out int dim)
    {
      dim = -1;

      foreach (var pair in row.GetElements())
      {
        if (pair.Key < row.Length - 1)
        {
          if (dim >= 0)
          { // We already saw a coefficient
            return false;
          }
          else
          {
            dim = pair.Key;
          }
        }
      }

      return dim >= 0;
    }



    /// <summary>
    /// Finds the first row of <code>m</code> filled with all zeros
    /// </summary>
    static protected int FirstZeroRow(SparseRationalArray[] m)
    {
      int r = m.Length - 1;
      for (int row = r - 1; row >= 0; row--)
      {
        if (m[row] == null)
        {
          r = row;
        }
        else if (!IsZeroRow(m[row]))
        {
          return r;
        }
        else
        {
          r = row;
        }
      }
      return r;
    }

    #endregion

    #region ICloneable Members

    /// <summary>
    /// Make a deep copy of this LinearEqualities object
    /// </summary>
    /// <returns></returns>
    public virtual object Clone()
    {
      return this.Duplicate();
    }

    public LinearEqualitiesEnvironment<Variable, Expression> DuplicateMe()
    {
      return this.Duplicate();
    }

    /// <summary>
    /// A type safe version of <code>Clone</code>
    /// </summary>
    virtual protected LinearEqualitiesEnvironment<Variable, Expression> Duplicate()
    {
      return new LinearEqualitiesEnvironment<Variable, Expression>(this);
    }

    #endregion

    #endregion

    #region INumericalAbstractDomain<int,Expression> Members

    public virtual void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargetsInitial, Converter<Variable, Expression> convert)
    {
      Contract.Assert(this.IsConsistent, "LinEq inconstistent @ AssignInParallel entry");

      LogPerformance("Equalities x == y before assign in parallel : {0}/{1}", this.CountPairwiseEqualities().ToString(), this.NumberOfRows.ToString());
      LogPerformance("#of cols/rows on entry {0}/{1}", this.numberOfCols.ToString(), this.NumberOfRows.ToString());

      var tmp = new LinearEqualitiesEnvironment<Variable, Expression>(this);
      var decoder = this.ExpressionManager.Decoder;

      // adding the domain-generated variables to the map as identity
      var sourcesToTargets = new Dictionary<Variable, FList<Variable>>(sourcesToTargetsInitial);
      var toSkip = new Set<Variable>();

      foreach (var x in this.varsToDimensions.Keys)
      {
        if (!sourcesToTargets.ContainsKey(x))
        {
          if (!toSkip.Contains(x)) // otherwise, it already represents a target variable and should not be removed
          {
            tmp.RemoveVariable(x, false);
          }

          continue;
        }

        // choosing the canonical element
        var xTargets = sourcesToTargets[x];
        var target = (xTargets.Length() > 1 && x.Equals(xTargets.Head))
          ? xTargets.Tail.Head
          : xTargets.Head;

        if (x.Equals(target))
        { // If it is the identity there is nothing to do...
          continue;
        }

        if (tmp.varsToDimensions.ContainsKey(target))
        {
          tmp.RemoveVariable(target, false);
          toSkip.Add(target);
        }

        int dimX = tmp.varsToDimensions[x];
        tmp.varsToDimensions.Remove(x);
        tmp.varsToDimensions[target] = dimX;

        Contract.Assert(tmp.IsConsistent);
      }

      // now adding equalities between targets of a same source

      // We first count how many x -> { y, z} we have. This is to avoid, in the presence of huge renamings to allocate a couple of columns, 
      // then fill them, then copy, then allocate another bunch, etc.

      #region Enlarge the matrix
      var equalities = 0;
      var filtered = new Dictionary<Variable, FList<Variable>>();
      foreach (var pair in sourcesToTargets)
      {
        var sourceConverted = convert(pair.Key);

        // f: Hack to have a better handling of constants (in particular with Int64)
        if (decoder.IsConstant(decoder.Stripped(sourceConverted)))
        {
          foreach (var x in pair.Value.GetEnumerable())
          {
            tmp = tmp.TestTrueEqual(x, sourceConverted);
          }
        }
        else
        {
          var length = pair.Value.Length();
          if (length >= 2)
          {

            if (base.PrintWarnsForLargeTargets && length > 10)
            {
              Console.WriteLine("[KARR] Found more than 10 pairwise equalities!!! (exactly {0})", length);
            }

            equalities++;
            filtered.Add(pair.Key, pair.Value);
          }
        }
      }

      // more columns
      tmp.AddColumnsToTheEnvironment(equalities * 2);

      // more rows
      LinearEquations<Variable, Expression> equations = null;
      var matrix = tmp.equations.Unshare(tmp, ref equations);
      Array.Resize(ref matrix, Math.Max(matrix.Length, equalities));
      tmp.equations = new LinearEquations<Variable, Expression>(tmp, matrix);
      #endregion


      // for huge renamings we Bypass TestTrue, and simply add the equalites sv1 == sv2
      if (sourcesToTargets.Count > HUGE_RENAMING)
      {
        this.expManager.Log("[KARR] Found a huge renaming ({0} renamings, {1} after filtering). Pre-allocating the matrix", 
          () => sourcesToTargets.Count.ToString(), 
          () => filtered.Count.ToString());

        foreach (var pair in filtered)
        {
          if (pair.Value.Length() <= 1)
          {
            continue;
          }
          var target = pair.Key.Equals(pair.Value.Head)
            ? pair.Value.Tail.Head
            : pair.Value.Head;

          foreach (var otherTarget in pair.Value.GetEnumerable())
          {
            if (otherTarget.Equals(target))
            {
              continue;
            }

            tmp.AddConstraintVariableEqVariable(target, otherTarget);
          }
        }
      }
      else
      {

        foreach (var pair in filtered)
        {
          Contract.Assert(pair.Value.Length() >= 2);
          var target = pair.Key.Equals(pair.Value.Head)
            ? pair.Value.Tail.Head
            : pair.Value.Head;

          var targetExp = convert(target);

          foreach (var otherTarget in pair.Value.GetEnumerable())
          {
            if (otherTarget.Equals(target))
            {
              continue;
            }

            var otherTargetExp = convert(otherTarget);

            tmp = tmp.TestTrueEqual(targetExp, otherTargetExp);
          }
        }
#if old
        foreach (var pair in sourcesToTargets)
        {
          var sourceConverted = convert(pair.Key);
 
          // f: Hack to have a better handling of constants (in particular with Int64)
          if (this.ExpressionManager.Decoder.IsConstant(this.ExpressionManager.Decoder.Stripped(sourceConverted)))
          {
            foreach (var x in pair.Value.GetEnumerable())
            {
              tmp = tmp.TestTrueEqual(x, sourceConverted);
            }
          }
          else
          {
            if (pair.Value.Length() >= 2)
            {

              var target = pair.Key.Equals(pair.Value.Head)
                ? pair.Value.Tail.Head
                : pair.Value.Head;

              var targetExp = convert(target);

              foreach (var otherTarget in pair.Value.GetEnumerable())
              {
                if (otherTarget.Equals(target))
                {
                  continue;
                }

                var otherTargetExp = convert(otherTarget);

                tmp = tmp.TestTrueEqual(targetExp, otherTargetExp);
              }
            }
          }
        }
#endif
      }
      // We change the state of this, using tmp
      CloneThisFrom(tmp);

      LogPerformance("Equalities x == y after assign in parallel : {0}/{1}", this.CountPairwiseEqualities().ToString(), this.NumberOfRows.ToString());
      LogPerformance("#of cols/rows on exit {0}/{1}", this.numberOfCols.ToString(), this.NumberOfRows.ToString());

      Contract.Assert(this.IsConsistent, "LinEq inconstistent @ AssignInParallel exit");
    }
    #endregion

    public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
    {
      var result = New(this.expManager);

      IExpressionEncoder<Variable, Expression> encoder;
      if (this.ExpressionManager.TryGetEncoder(out encoder))
      {
        for (int i = 0; i < this.NumberOfRows; i++)
        {
          var exp = this.ToPolynomial(i).ToPureExpression(encoder);

          var redundant = oracle.CheckIfHolds(exp);

          if (!redundant.IsNormal() || !redundant.BoxedElement)
          { // If it is not implied by the oracle, we add it in the result
            result = result.TestTrue(exp) as LinearEqualitiesEnvironment<Variable, Expression>;
          }
        }
      }

      return result;
    }

    /// <summary>
    /// Gets the polynomials that are equals to the expression <code>x</code>
    /// </summary>
    public Set<Polynomial<Variable, Expression>> EqualsTo(Variable x)
    {
      if (!this.varsToDimensions.ContainsKey(x))
      {
        return new Set<Polynomial<Variable, Expression>>();
      }

      var tmp = this.Duplicate();
      int initialRows = tmp.NumberOfRows;

      foreach (var var in tmp.Variables)
      {
        tmp.Close(tmp.varsToDimensions[var], MAX_VARS_FOR_CLOSURE, initialRows);
      }

      return tmp.LookForEqualities(x);
    }

    /// <returns>
    /// A set of pairs <code>(exp, val)</code> 
    /// </returns>
    public Set<Pair<Variable, Rational>> ConstantValues()
    {
      var result = new Set<Pair<Variable, Rational>>();

      var tmp = this.Duplicate();
      tmp.PutIntoRowEchelonForm();

      int dim;

      for (int i = 0; i < tmp.numberOfRows; i++)
      {
        Variable var;
        if (tmp.equations.IsConstantRow(i, out dim) && this.varsToDimensions.TryGetValue(dim, out var))
        {
          result.Add(new Pair<Variable, Rational>(var, tmp.equations.At(i, tmp.NumberOfColumns - 1)));
        }
      }

      return result;
    }

    //static int __count;

    /// <returns>
    /// A set of equalities (e1, e2) implied by the current state.
    /// </returns>
    public IEnumerable<Pair<Expression, Expression>> PairWiseEqualities(int max)
    {
      Contract.Requires(max >= 0);
      Contract.Ensures(Contract.Result<IEnumerable<Pair<Expression, Expression>>>() != null);

      var matrix = this.equations.Unshare(this, ref this.equations);

      //__count++;
      //Console.WriteLine("#{0}: Rows = {1}", __count, this.numberOfRows); 

      int count = 0;

      for (int i = 0; i < this.NumberOfRows && count < max; i++)
      {
        var Matrix_i = matrix[i];

        if (this.equations.IsConstantRow(i))
        {
          continue;
        }

        for (int j = i; j < this.NumberOfRows && count < max; j++)
        {
          SparseRationalArray tmpVector;

          if (i == j)
          {
            tmpVector = Matrix_i;
          }
          else
          {
            var Matrix_j = matrix[j];

            tmpVector = FreshRow(this.NumberOfColumns);

            // Set union seems to allocate too much space. 
            // So we simply go for two disjoint cycles and manually track if we've already seen some element
            var visited = new bool[this.NumberOfColumns];

            foreach (var k in Matrix_i.IndexesOfNonDefaultElements)
            {
              // Pairwise sum
              // tmpVector[k] = Matrix_i[k] + Matrix_j[k];

              Rational add;
              if (Rational.TryAdd(Matrix_i[k], Matrix_j[k], out add))
              {
                visited[k] = true;
                tmpVector[k] = add;
              }
              else
              {
                goto nextIteration;
              }
            }

            foreach (var k in Matrix_j.IndexesOfNonDefaultElements)
            {
              // Pairwise sum
              // tmpVector[k] = Matrix_i[k] + Matrix_j[k];

              if (visited[k])
              {
                continue;
              }

              Rational add;
              if (Rational.TryAdd(Matrix_i[k], Matrix_j[k], out add))
              {
                tmpVector[k] = add;
              }
              else
              {
                goto nextIteration;
              }
            }
          }

          Variable x, y, z;

          var encoder = this.expManager.Encoder;

          if (this.MatchXEqY(tmpVector, out x, out  y))
          {
            var xExp = encoder.VariableFor(x);
            var yExp = encoder.VariableFor(y);

            yield return new Pair<Expression, Expression>(xExp, yExp);

            count++;
            continue;
          }

          if (this.MatchXPlusYEqZ(tmpVector, out x, out y, out z))
          {
            var xExp = encoder.VariableFor(x);
            var yExp = encoder.VariableFor(y);
            var zExp = encoder.VariableFor(z);

            yield return new Pair<Expression, Expression>(encoder.CompoundExpressionFor(ExpressionType.Int32,
              ExpressionOperator.Addition, xExp, yExp),
              zExp);

            count++;
            continue;
          }

        nextIteration: ;
        }
      }

      LogPerformanceWithTreshold(50, "Size of equalities : {0}", count);

      yield break;
    }

    private int CountPairwiseEqualities()
    {
      #region Contracts
      Contract.Ensures(Contract.Result<int>() >= 0);
      #endregion

      var count = 0;

      for (var row = 0; row < this.NumberOfRows; row++)
      {
        if (this.equations.IsXeqYRow(row))
          count++;
      }

      return count;
    }

    #region Helpers
    private bool MatchXEqY(SparseRationalArray vector, out Variable x, out Variable y)
    {
      Contract.Requires(vector.Length == this.NumberOfColumns);

      x = default(Variable);
      y = default(Variable);

      if (vector[this.NumberOfColumns - 1].IsNotZero)
      {
        return false;
      }

      int indexForX = -1;
      int indexForY = -1;

      foreach (var pair in vector.GetElements())
      {
        if (pair.Value.IsNotZero)
        {// We need to check that the coefficients are all unary
          if (pair.Value != -1 && pair.Value != 1)
          {
            return false;
          }

          if (indexForX < 0)
            indexForX = pair.Key;

          else if (indexForY < 0)
            indexForY = pair.Key;
          else
            return false; // That is, there are at least three non-zero variables 
        }
      }

      if (indexForX < 0 || indexForY < 0)
      {
        return false;
      }

      // If we reach this point, then we have exactly two variables in the lineaer equation, and we want to check thay have opposite signs
      var valueForX = vector[indexForX];
      var valueForY = vector[indexForY];

      if (valueForX.Sign != valueForY.Sign)
      {
        x = this.varsToDimensions[indexForX];
        y = this.varsToDimensions[indexForY];
        return true;
      }
      else
      {
        return false;
      }

    }

    private bool MatchXPlusYEqZ(SparseRationalArray vector, out Variable x, out Variable y, out Variable z)
    {
      Contract.Requires(vector.Length == this.NumberOfColumns);

      x = default(Variable);
      y = default(Variable);
      z = default(Variable);

      if (vector[this.NumberOfColumns - 1].IsNotZero)
      {
        return false;
      }

      int indexForX = -1;
      int indexForY = -1;
      int indexForZ = -1;

      foreach (var pair in vector.GetElements())
      {
        if (pair.Value.IsNotZero)
        {// We need to check that the coefficients are all unary
          if (pair.Value != -1 && pair.Value != 1)
            return false;
          if (indexForX < 0)
            indexForX = pair.Key;
          else if (indexForY < 0)
            indexForY = pair.Key;
          else if (indexForZ < 0)
            indexForZ = pair.Key;
          else
            return false; // That is, there are at least four non-zero variables 
        }
      }

      if (indexForX < 0 || indexForY < 0 || indexForZ < 0)
      {
        return false;
      }

      // If we reach this point, then we have exactly three variables in the linear equation, and we want to check that their signs match
      var valueForX = vector[indexForX];
      var valueForY = vector[indexForY];
      var valueForZ = vector[indexForZ];

      if (valueForX.Sign + valueForY.Sign + valueForZ.Sign == 1)
      {
        if (valueForX.Sign == -1)
        {
          z = this.varsToDimensions[indexForX];
          y = this.varsToDimensions[indexForY];
          x = this.varsToDimensions[indexForZ];
          return true;
        }
        if (valueForY.Sign == -1)
        {
          y = this.varsToDimensions[indexForX];
          z = this.varsToDimensions[indexForY];
          x = this.varsToDimensions[indexForZ];
          return true;
        }
        if (valueForZ.Sign == -1)
        {
          x = this.varsToDimensions[indexForX];
          y = this.varsToDimensions[indexForY];
          z = this.varsToDimensions[indexForZ];
          return true;
        }
      }
      else if (valueForX.Sign + valueForY.Sign + valueForZ.Sign == -1)
      {
        if (valueForX.Sign == 1)
        {
          z = this.varsToDimensions[indexForX];
          y = this.varsToDimensions[indexForY];
          x = this.varsToDimensions[indexForZ];
          return true;
        }
        if (valueForY.Sign == 1)
        {
          y = this.varsToDimensions[indexForX];
          z = this.varsToDimensions[indexForY];
          x = this.varsToDimensions[indexForZ];
          return true;
        }
        if (valueForZ.Sign == 1)
        {
          x = this.varsToDimensions[indexForX];
          y = this.varsToDimensions[indexForY];
          z = this.varsToDimensions[indexForZ];
          return true;
        }
      }
      return false;

    }

    /// <summary>
    /// A set of polynomial equalities implied by this state
    /// </summary>
    /// <param name="complete">true for polynomials obtained by combining one or two rows, false for only one polynomial per row</param>
    /// <returns>A set of polynomials</returns>
    public Set<Polynomial<Variable, Expression>>/*!*/ ToPolynomial(bool complete)
    {
      Contract.Ensures(Contract.Result<Set<Polynomial<Variable, Expression>>>() != null);

      var result = new Set<Polynomial<Variable, Expression>>();

      for (var i = 0; i < numberOfRows; i++)
      {
        var poli = ToPolynomial(i);

        result.Add(poli);

        if (complete)
        {
          for (int j = i + 1; j < numberOfRows; j++)
          {
            var polj = ToPolynomial(j);
            var commonVars = poli.Variables.Intersection(polj.Variables);
            foreach (var e in commonVars)
            {
              try
              {
                Polynomial<Variable, Expression> pol;
                if (TryCombine(poli, polj, e, out pol))
                {
                  result.Add(pol);
                }
              }
              catch (ArithmeticExceptionRational)
              {
                // We ignore it
              }
              break; // only one addition as others are likely to give the same polynomial
            }
          }
        }
      }
      return result;
    }

    /// <summary>
    /// Returns a set of polynomials obtained by linear combination of <paramref name="template"/> and a row of this state
    /// </summary>
    /// <param name="template">Will be used in the combination</param>
    /// <returns>A set of polynomials that all contain exactly one slack variable</returns>
    internal Set<Polynomial<Variable, Expression>> Combine(
      Polynomial<Variable, Expression> template, int numberOfSlackInTemplate, int maxNumberOfSlackInResult, int depth)
    {
      Contract.Requires(depth >= 0);

      Contract.Ensures(Contract.Result<Set<Polynomial<Variable, Expression>>>() != null);

      var result = new Set<Polynomial<Variable, Expression>>();

      if (depth == 0)
      {
        return result;
      }

      if (numberOfSlackInTemplate <= maxNumberOfSlackInResult)
      {
        if (!template.IsTautology && !(template.Relation == null && template.Left.Length == 1 && template.Left[0].IsConstant))
        {
          result.Add(template);
        }

        return result;
      }

      var polynomials = this.ToPolynomial(false);
      foreach (var pol in polynomials)
      {
        var commonvars = template.Variables.Intersection(pol.Variables);

        foreach (var e in commonvars)
        {
          if (!this.expManager.Decoder.IsSlackVariable(e))
          {
            continue;
          }

          Polynomial<Variable, Expression> temp;
          if (!TryCombine(template, pol, e, out temp))
          {
            continue;
          }
          Variable v;
          int k = SubPolyhedra<Variable, Expression>.IsBetaDefinition(temp, out v, this.expManager.Decoder);

          if (k >= numberOfSlackInTemplate)
          {
            continue;
          }

          if (k <= maxNumberOfSlackInResult)
          {
            if (!temp.IsTautology)
            {
              result.Add(temp);
            }
          }
          else
          {
            var newPolynomials = Combine(temp, k, maxNumberOfSlackInResult, depth - 1);
            result.AddRange(newPolynomials);
          }
        }
      }
      return result;
    }

    static internal bool TryCombine(
      Polynomial<Variable, Expression> left, Polynomial<Variable, Expression> right, Variable pivot,
      out Polynomial<Variable, Expression> result)
    {
      if (left.Relation == null)
      { // transform p into p == 0
        Polynomial<Variable, Expression>.TryToPolynomialForm(true, ExpressionOperator.Equal,
          (new List<Monomial<Variable>>(left.Left)).ToArray(),
          new Monomial<Variable>[] { new Monomial<Variable>(0) }, out left);
      }
      if (right.Relation == null)
      { // transform p into p == 0
        Polynomial<Variable, Expression>.TryToPolynomialForm(true, ExpressionOperator.Equal,
          (new List<Monomial<Variable>>(right.Left)).ToArray(),
          new Monomial<Variable>[] { new Monomial<Variable>(0) }, out right);
      }

      var leftK = Rational.For(0);
      var rightK = Rational.For(0);
      foreach (var m in left.Left)
      {
        if (m.VariableAt(0).Equals(pivot))
        {
          leftK = m.K;

          break;
        }
      }

      foreach (var m in right.Left)
      {
        if (m.VariableAt(0).Equals(pivot))
        {
          rightK = m.K;

          break;
        }
      }

      var res = new List<Monomial<Variable>>();
      foreach (var m in left.Left)
      {
        res.Add(new Monomial<Variable>(rightK * m.K, m.VariableAt(0)));
      }

      foreach (var m in right.Left)
      {
        res.Add(new Monomial<Variable>(-leftK * m.K, m.VariableAt(0)));
      }
      res.Add(new Monomial<Variable>(leftK * right.Right[0].K - rightK * left.Right[0].K));


      return
        Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.Equal, res.ToArray(), new Monomial<Variable>[0], out result);
    }

    /// <summary>
    /// This is a potential *very* expensive operation, so we'd like to avoid using it if possible
    /// </summary>
    /// <returns></returns>
    public Set<Polynomial<Variable, Expression>>/*!*/ ToPolynomial()
    {
      var tmp = this.Duplicate();

      int initalRows = tmp.NumberOfRows;


      foreach (var var in tmp.Variables)
      {
        tmp.Close(tmp.varsToDimensions[var], MAX_VARS_FOR_CLOSURE, initalRows);
      }

      var result = new Set<Polynomial<Variable, Expression>>(tmp.NumberOfRows);

      for (int row = 0; row < tmp.NumberOfRows; row++)
      {
        var newPolynomial = tmp.ToPolynomial(row);

        result.Add(newPolynomial);
      }

      return result;
    }

    /// <returns>
    /// A set of expression representing the equations in this state
    /// </returns>
    public Set<Expression> ToExpressions()
    {
      var result = new Set<Expression>();

      if (this.IsBottom)
      {
        return result;
      }

      foreach (var p in this.ToPolynomial())
      {
        result.Add(p.ToPureExpression(this.expManager.Encoder));
      }

      return result;
    }

    /// <returns>
    /// A view as polynomial of the row <code>row</code>
    /// </returns>
    protected Polynomial<Variable, Expression>/*!*/ ToPolynomial(int row)
    {
      Contract.Requires(row >= 0);
      Contract.Requires(row < this.numberOfRows);

      var matrix = this.equations.Unshare(this, ref this.equations);

      return ToPolynomial(matrix[row]);
    }

    protected Polynomial<Variable, Expression> ToPolynomial(SparseRationalArray vector)
    {
      Contract.Requires(vector != null);

      if (vector.Length != this.NumberOfColumns)
      {
        throw new AbstractInterpretationException("Trying to convert a vector with the wrong length");
      }

      var left = new List<Monomial<Variable>>();
      var right = new List<Monomial<Variable>>();

      foreach (var pair in vector.GetElements())
      {
        if (pair.Key == this.NumberOfColumns - 1)
        {
          continue;
        }

        var k = pair.Value;
        var x = this.varsToDimensions[pair.Key];
        left.Add(new Monomial<Variable>(k, x));
      }

      if (left.Count == 0)
      {
        left.Add(new Monomial<Variable>(0));
      }

      right.Add(new Monomial<Variable>(vector[this.NumberOfColumns - 1]));

      Polynomial<Variable, Expression> result;
      if (!Polynomial<Variable, Expression>.TryToPolynomialForm(true, ExpressionOperator.Equal, left.ToArray(), right.ToArray(), out result))
      {
        throw new AbstractInterpretationException("Converting two monomials into a polynomial should never rise an exception");
      }

      return result;
    }

    /// <summary>
    /// Close the matrix w.r.t. the <code>dimension</code>
    /// </summary>
    /// <param name="dim">The dimension to close</param>
    /// <param name="n">The maximum number of variables that we want to keep in a constraint</param>
    private void Close(int dim, int n, int initialRows)
    {
      var matrix = this.equations.Unshare(this, ref this.equations);

      for (int row = 0; row < initialRows; row++)
      {
        for (int internalRow = row + 1; internalRow < initialRows; internalRow++)
        {
          // If combining the two rows gives zero for the dimension, then we want to keep this row
          if ((matrix[row][dim] + matrix[internalRow][dim]).IsZero)
          {
            var newRow = FreshRow(this.NumberOfColumns);

            // F: I perform two loops because I hope that this is less expensive than allocating a set, put the elements in it and then iterate over
            foreach (var i in matrix[row].IndexesOfNonDefaultElements)
            {
              try
              {
                newRow[i] = matrix[row][i] + matrix[internalRow][i];
              }
              catch (ArithmeticExceptionRational)
              {
                // Abstraction: If we overflow, we forget the constraints
                goto next;
              }
            }

            foreach (var i in matrix[internalRow].IndexesOfNonDefaultElements)
            {
              try
              {
                newRow[i] = matrix[row][i] + matrix[internalRow][i];
              }
              catch (ArithmeticExceptionRational)
              {
                // Abstraction: If we overflow, we forget the constraints
                goto next;
              }
            }

            if (ContainsAtMostNVariables(newRow, n + 1))
            {
              this.AddConstraint(newRow);
            }
          }

        next:
          ;
        }
      }
    }

    static private bool ContainsAtMostNVariables(SparseRationalArray row, int maxVariables)
    {
      //Debug.Assert(maxVariables >= 0);
      //Debug.Assert(maxVariables < row.Length - 1);

      // return row.Count <= maxVariables;

      return maxVariables >= 0 && maxVariables < row.Length - 1 && row.Count <= maxVariables;
    }

    /// <returns>
    /// A set of poynomials such that for each polynomial <code>p \in \result</code> <code>x == p</code> holds in this abstract domain
    /// </returns>
    private Set<Polynomial<Variable, Expression>> LookForEqualities(Variable/*!*/ x)
    {
      var result = new Set<Polynomial<Variable, Expression>>();
      Polynomial<Variable, Expression> tmpPol;

      int dim = this.varsToDimensions[x];

      for (int row = 0; row < this.NumberOfRows; row++)
      {
        var k = this.equations.At(row, dim);
        if (k.IsNotZero)
        {
          var monomials = new List<Monomial<Variable>>();
          Monomial<Variable> newMonomial;

          foreach (var pair in this.equations.GetElementsForRow(row))
          {
            var i = pair.Key;
            var k1 = -pair.Value / k;
            if (i != dim && k1.IsNotZero && i < this.NumberOfColumns - 1)
            {
              var v = this.varsToDimensions[i];
              newMonomial = new Monomial<Variable>(k1, v);
              monomials.Add(newMonomial);
            }
          }

          monomials.Add(new Monomial<Variable>(this.equations.At(row, this.numberOfCols - 1) / k));

          if (!Polynomial<Variable, Expression>.TryToPolynomialForm(true, monomials.ToArray(), out tmpPol))
          {
            throw new AbstractInterpretationException("It can never be the case that a list of monomials fails to be converted into a polynomial");
          }

          result.Add(tmpPol);
        }
      }

      return result;
    }
    #endregion

    #region Class Invariants

    // We make the body conditionally compiled as the checking is quite expensive and we use the attribute "Conditional" (it is a getter)
    internal bool IsConsistent
    {
      get
      {
#if CHECKINVARIANTS
        foreach (var x in this.varsToDimensions.DirectMap.Keys)
        {
          if (this.varsToDimensions.DirectMap[x] >= this.NumberOfColumns)
          {
            return false;
          }
        }


        // As this is for debugging only, we iterate all over the columns explictly
        for (int row = 0; row < this.NumberOfRows; row++)
        {
          for (int col = 0; col < this.NumberOfColumns - 1; col++)
          {
            if (this.equations.At(row,col).IsNotZero)
            {
              if (!this.varsToDimensions.InverseMap.ContainsKey(col))
              { // We have a nonzero element in a col that is suppoed not to be used
                ALog.Message(StringClosure.For("There is no variable associated with the matrix element ({0},{1})", StringClosure.For(row), StringClosure.For(col)));

                // We want to stop the execution for debugger...
                // System.Diagnostics.Debugger.Break();

                return false;
              }
            }
          }
        }
        return true;
#else
        if (this.equations == null)
          return false;

        return true;
#endif
      }
    }

    private bool IsFinite
    {
      get
      {
#if VERBOSEOUTPUT
        for (int i = 0; i < this.numberOfRows; i++)
          if (!this.equations.IsNullRow(i))
            for (int j = 0; j < this.equations.RowLength(i); j++)
              if (this.equations.At(i, j).IsInfinity) return false;
#endif
        return true;
      }
    }

    private bool AreAllConstants
    {
      get
      {
        for (int i = 0; i < this.NumberOfRows; i++)
        {
          // If it is not constant
          if (this.equations.RowCount(i) >= 3)
          {
            return false;
          }
        }

        return true;
      }
    }
    #endregion

    #region Visitors

    internal class VisitorForTestTrue
      : TestTrueVisitor<LinearEqualitiesEnvironment<Variable, Expression>, Variable, Expression>
    {
      public VisitorForTestTrue(IExpressionDecoder<Variable, Expression>/*!*/ decoder)
        : base(decoder)
      {
      }

      public override LinearEqualitiesEnvironment<Variable, Expression> VisitEqual_Obj(Expression left, Expression right, Expression original, LinearEqualitiesEnvironment<Variable, Expression> data)
      {
        return this.VisitEqual(left, right, original, data);
      }

      public override LinearEqualitiesEnvironment<Variable, Expression> VisitEqual(Expression left, Expression right, Expression original, LinearEqualitiesEnvironment<Variable, Expression> data)
      {
        return data.TestTrueEqual(left, right);
      }

      public override LinearEqualitiesEnvironment<Variable, Expression> VisitVariable(Variable variable, Expression original, LinearEqualitiesEnvironment<Variable, Expression> data)
      {
        return Default(data);
      }

      public override LinearEqualitiesEnvironment<Variable, Expression> VisitLessEqualThan(Expression left, Expression right, Expression original, LinearEqualitiesEnvironment<Variable, Expression> data)
      {
        return Default(data);
      }

      public override LinearEqualitiesEnvironment<Variable, Expression> VisitLessThan(Expression left, Expression right, Expression original, LinearEqualitiesEnvironment<Variable, Expression> data)
      {
        return Default(data);
      }

      public override LinearEqualitiesEnvironment<Variable, Expression> VisitNotEqual(Expression left, Expression right, Expression original, LinearEqualitiesEnvironment<Variable, Expression> data)
      {
        return Default(data);
      }
    }

    internal class VisitorForTestFalse
      : TestFalseVisitor<LinearEqualitiesEnvironment<Variable, Expression>, Variable, Expression>
    {
      public VisitorForTestFalse(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
        Contract.Requires(decoder != null);
      }

      public override LinearEqualitiesEnvironment<Variable, Expression> VisitVariable(Variable variable, Expression original, LinearEqualitiesEnvironment<Variable, Expression> data)
      {
        return Default(data);
      }
    }

    internal class VisitorForCheckIfHolds
      : CheckIfHoldsVisitor<LinearEqualitiesEnvironment<Variable, Expression>, Variable, Expression>
    {
      private IExpressionEncoder<Variable, Expression> encoder;

      public VisitorForCheckIfHolds(ExpressionManagerWithEncoder<Variable, Expression> expManager)
        : base(expManager.Decoder)
      {
        Contract.Requires(expManager != null); 

        this.encoder = expManager.Encoder;
      }

      public override FlatAbstractDomain<bool> VisitEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        Polynomial<Variable, Expression> pol;

        if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.Equal, left, right, Decoder, out pol))
        {
          // We check if it is a trivial equality...
          if (pol.IsTautology)
          {
            return CheckOutcome.True;
          }

          if (pol.IsInconsistent)
          {
            return CheckOutcome.False;
          }

          if (pol.IsLinear)
          {
            var asVector = Domain.AsVector(pol.LeftAsPolynomial);
            asVector[asVector.Length - 1] = pol.Right[0].K;

            var duplicate = Domain.Duplicate();
            duplicate.PutIntoRowEchelonForm();

            int prevRowsCount = duplicate.NumberOfRows;

            duplicate.AddConstraint(asVector);

            Debug.Assert(prevRowsCount <= duplicate.NumberOfRows);  // we should not have more constraints here!

            duplicate.PutIntoRowEchelonForm();

            // Is the new constraint a linear combination of the previous one? 
            if (duplicate.NumberOfRows == prevRowsCount)
            {
              return CheckOutcome.True;
            }
            else if (duplicate.IsBottom)
            {
              return CheckOutcome.False;
            }
            else if (duplicate.IsEmpty())
            {
              return CheckOutcome.True;
            }
            else
            {
              // Checking all the constants can be 
              if (!duplicate.AreAllConstants)
              {
                var asPolynomial = Domain.ToPolynomial();
                if (asPolynomial.Exists(delegate(Polynomial<Variable, Expression> other) { return pol.IsEquivalentTo(other); }))
                {
                  return CheckOutcome.True;
                }
              }
              return CheckOutcome.Top;
            }
          }
        }
        return CheckOutcome.Top;
      }

      public override FlatAbstractDomain<bool> VisitEqual_Obj(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        return this.VisitEqual(left, right, original, data);
      }

      public override FlatAbstractDomain<bool> VisitNotEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        if (this.encoder == null)
        {
          return base.VisitNotEqual(left, right, original, data);
        }

        var eq = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction, left, right);
        var range = this.Domain.BoundsFor(eq);

        if (range.IsNormal() && range.IsSingleton)
        {
          return range.LowerBound.IsZero ? CheckOutcome.False : CheckOutcome.True;
        }

        return CheckOutcome.Top;
      }

      public override FlatAbstractDomain<bool> VisitLessEqualThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        var result = VisitEqual(left, right, original, data);

        return result.IsTrue() ? result : CheckOutcome.Top;
      }

      public override FlatAbstractDomain<bool> VisitLessThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        return Default(data);
      }

    }

    #endregion

    #region Floating point types

    public void SetFloatType(Variable v, ConcreteFloat f)
    {
      // does nothing
    }

    public FlatAbstractDomain<ConcreteFloat> GetFloatType(Variable v)
    {
      return FloatTypes<Variable, Expression>.Unknown;
    }

    #endregion

    internal void RemoveUnusedVariables()
    {
      this.PutIntoRowEchelonForm();

      var matrix = this.equations.Unshare(this, ref this.equations);

      var toRemove = new List<Variable>();

      foreach (var pair in this.varsToDimensions)
      {
        if (IsZeroCol(matrix, this.NumberOfRows, pair.Value))
        {
          toRemove.Add(pair.Key);
        }
      }

      //Console.WriteLine("Candidate to be removed {0}", toRemove.Count);

      var count = this.NumberOfColumns - this.varsToDimensions.Count;

      //Console.WriteLine("Unused before {0}", count);

      foreach (var x in toRemove)
      {
        this.RemoveVariable(x);
      }

      if (count > 2 * GrowSizeForColumns)
      {
        // We move from the right to the left
        int j;

        #region Special case for the first segment (we should move the coefficient too)
        for (j = NumberOfColumns - 2; j >= 0 && !this.varsToDimensions.ContainsValue(j); j--)
        {
          if (!IsZeroCol(matrix, this.NumberOfRows, j))
          {
            break;
          }
        }

        if (j >= 0)
        {
          var OldNumberOfColumns = NumberOfColumns;
          NumberOfColumns = j + 2;
          for (int i = 0; i < this.NumberOfRows; i++)
          {
            var tmp = matrix[i][OldNumberOfColumns - 1];
            matrix[i].ShrinkTo(NumberOfColumns);
            matrix[i][NumberOfColumns - 1] = tmp;
          }
        }
        #endregion

        // TODO: Add the intervals
      }

      //Console.WriteLine("Unused after {0}", this.NumberOfColumns - this.varsToDimensions.Keys.Count);
    }

    #region INumericalAbstractDomainQuery<Variable,Expression> Members

    public Variable ToVariable(Expression exp)
    {
      return this.ExpressionManager.Decoder.UnderlyingVariable(exp);
    }

    #endregion

    #region Statistics

    new public string Statistics()
    {
      if (this.state == LinearEqualitiesState.Normal || this.state == LinearEqualitiesState.StrongRowEchelon)
      {
        return string.Format("{0}; Rows: {1}, Cols: {2}, vars2Dim count : {3}", this.equations.Statistics(), this.numberOfRows, this.numberOfCols, this.varsToDimensions.Count);
      }
      else
      {
        return "bottom";
      }
    }
    
    #endregion

    protected class VarsToDimensions
    {
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(this.varsToDimensions != null);
        Contract.Invariant(this.map != null);

        Contract.Invariant(this.IsConsistent());
      }

      [Pure]
      private bool IsConsistent()
      {
        foreach (var pair in this.varsToDimensions)
        {
          if (!this.map[pair.Value])
            return false;
        }

        for (var i = 0; i < this.map.Length; i++)
        {
          var p = map[i];
          var q = this.varsToDimensions.ContainsValue(i);

          if (p != q)
            return false;
        }

        return true;
      }

      readonly private BijectiveMap<Variable, int> varsToDimensions;
      readonly private BitArray map;

      public VarsToDimensions()
      {
        this.varsToDimensions = new BijectiveMap<Variable, int>();
        this.map = new BitArray(256);
      }

      public VarsToDimensions(VarsToDimensions original)
      {
        this.varsToDimensions = new BijectiveMap<Variable, int>(original.varsToDimensions);
        this.map = new BitArray(original.map);
      }

      public int Count
      {
        get
        {
          Contract.Ensures(Contract.Result<int>() >= 0);

          return this.varsToDimensions.Count;
        }
      }

      public IEnumerable<Variable> Keys
      {
        get
        {
          return this.varsToDimensions.Keys;
        }
      }

      public IEnumerator<KeyValuePair<Variable, int>> GetEnumerator()
      {
        return this.varsToDimensions.GetEnumerator();
      }

      public int this[Variable key]
      {
        get
        {
          Contract.Ensures(Contract.Result<int>() >= 0);

          return this.varsToDimensions[key];
        }
        set
        {
          Contract.Requires(value >= 0);

          EnsureMapSize(value);

          int prev;
          if (this.TryGetValue(key, out prev))
          {
            this.map[prev] = false;
          }

          this.map[value] = true;


          this.varsToDimensions[key] = value;
        }
      }

      public Variable this[int dim]
      {
        get
        {
          Contract.Requires(dim >= 0);
 
          return this.varsToDimensions.InverseMap[dim];
        }
      }

      public Dictionary<int, Variable> InverseMap
      {
        get
        {
          return this.varsToDimensions.InverseMap;
        }
      }

      [Pure]
      public bool TryGetValue(Variable v, out int dim)
      {
        return this.varsToDimensions.TryGetValue(v, out dim);
      }

      [Pure]
      public bool TryGetValue(int dim, out Variable v)
      {
        Contract.Requires(dim >= 0);

        v = default(Variable);
        return dim < this.map.Length && this.map[dim] && this.varsToDimensions.InverseMap.TryGetValue(dim, out v);
      }

      [Pure]
      public bool TryGetFirstAvailableDimension(out int dim)
      {
        for (var i = 0; i < this.map.Length; i++)
        {
          if(!this.map[i])
          {
            dim = i;
            return true;
          }
        }

        map.Length++;

        dim = map.Length;

        return true;
      }

      public void Remove(Variable v)
      {
        int dim;
        if (this.TryGetValue(v, out dim))
        {
          this.map[dim] = false;
          this.varsToDimensions.Remove(v);
        }
      }

      [Pure]
      public bool ContainsKey(Variable v)
      {
        return this.varsToDimensions.ContainsKey(v);
      }

      [Pure]
      public bool ContainsValue(int i)
      {
        Contract.Requires(i >= 0);

        return i < this.map.Length && this.map[i];
      }

      [Pure]
      public Variable KeyForValue(int value)
      {
        return this.varsToDimensions.InverseMap[value];
      }

      public override string ToString()
      {
        return varsToDimensions.ToString();
      }

      private void EnsureMapSize(int dim)
      {
        Contract.Requires(dim >= 0);
        Contract.Ensures(dim < this.map.Length);

        if (this.map.Length <= dim)
        {
          this.map.Length = dim+1;
        }
      }

      /// <summary>
      /// Only for debugging
      /// </summary>
      [Pure]
      internal bool LessThan(int max)
      {
        foreach (var dim in this.varsToDimensions.Values)
        {
          if (dim >= max)
            return false;
        }

        return true;
      }
    }
  }
}