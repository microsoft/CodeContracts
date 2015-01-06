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

// This file contains the interfaces and the hierarchies for the abstract domains

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Numerical;
using System.Diagnostics.Contracts;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains
{
  #region Interfaces for the domains

  /// <summary>
  /// The interface for a basic abstract domain (bottom, top, join, meet, widening)
  /// </summary>
  [ContractClass(typeof(IAbstractDomainContracts))]
  public interface IAbstractDomain 
    : ICloneable
  {
    bool IsBottom { get; }
    bool IsTop { get; }

    /// <summary>
    /// The partial order on the abstract domain
    /// </summary>
    bool LessEqual(IAbstractDomain a);

    /// <summary>
    /// Get the bottom element of the abstract domain
    /// </summary>
    IAbstractDomain Bottom { get; }

    /// <summary>
    /// Get the top element of the abstract doman
    /// </summary>
    IAbstractDomain Top { get; }

    /// <summary>
    /// Join of the abstract domain
    /// </summary>
    [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-a != null")]
    IAbstractDomain Join(IAbstractDomain a);

    /// <summary>
    /// Meet of the abstract domain
    /// </summary>
    [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-a != null")]
    IAbstractDomain Meet(IAbstractDomain a);

    /// <summary>
    /// Widening of the abstract domain.
    /// Convention: <code>this</code> is the value of the new iteration. <code>prev</code> is the value of the previous one
    /// </summary>
    [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-prev != null")]
    IAbstractDomain Widening(IAbstractDomain prev);

    /// <summary>
    /// Give a <code>T</code> view of the elements using <code>factory</code>
    /// </summary>
    [Pure]
    T To<T>(IFactory<T> factory);
  }

  #region Contracts for a IAbstractDomain
  [ContractClassFor(typeof(IAbstractDomain))]
  abstract class IAbstractDomainContracts
    : IAbstractDomain
  {
    #region IAbstractDomain Members

    bool IAbstractDomain.IsBottom
    {
      get
      {
        return default(bool);
      }
    }

    bool IAbstractDomain.IsTop
    {
      get
      {
        return default(bool);
      }
    }

    [Pure]
    bool IAbstractDomain.LessEqual(IAbstractDomain a)
    {
      Contract.Requires(a != null);

      return default(bool);
    }

    IAbstractDomain IAbstractDomain.Bottom
    {
      get 
      {
        Contract.Ensures(Contract.Result<IAbstractDomain>() != null);

        return default(IAbstractDomain);
      }
    }

    IAbstractDomain IAbstractDomain.Top
    {
      get
      {
        Contract.Ensures(Contract.Result<IAbstractDomain>() != null);

        return default(IAbstractDomain);
      }
    }

    IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
    {
      Contract.Requires(a != null);

      Contract.Ensures(Contract.Result<IAbstractDomain>() != null);

      return default(IAbstractDomain);
    }

    IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
    {
      Contract.Requires(a != null);

      Contract.Ensures(Contract.Result<IAbstractDomain>() != null);

      return default(IAbstractDomain);
    }

    IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
    {
      Contract.Requires(prev != null);

      Contract.Ensures(Contract.Result<IAbstractDomain>() != null);

      return default(IAbstractDomain);
    }

    T IAbstractDomain.To<T>(IFactory<T> factory)
    {
      Contract.Requires(factory != null);

      return default(T);
    }

    #endregion

    #region ICloneable Members

    object ICloneable.Clone()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
  #endregion

  ///<summary>
  /// The capability of handling assignments
  ///</summary>
  ///<typeparam name="Variable">The type of the variables handled by this abstract domain</typeparam>
  ///<typeparam name="Expression">The type of the expressions handled by this abstract domain</typeparam>

  [ContractClass(typeof(IAbstractDomainWithRenamingContracts<,>))]
  public interface IAbstractDomainWithRenaming<T, Variable>
    : IAbstractDomain
    where T: IAbstractDomainWithRenaming<T, Variable>
  {
    T Rename(Dictionary<Variable, FList<Variable>> renaming);
  }

  #region Contracts for IAbstractDomainWithRenaming
  [ContractClassFor(typeof(IAbstractDomainWithRenaming<,>))]
  abstract class IAbstractDomainWithRenamingContracts<T, Variable>
    : IAbstractDomainWithRenaming<T, Variable>
    where T : IAbstractDomainWithRenaming<T, Variable>
  {
    #region IAbstractDomainWithRenaming<Variable,T> Members

    T IAbstractDomainWithRenaming<T, Variable>.Rename(Dictionary<Variable, FList<Variable>> renaming)
    {
      Contract.Requires(renaming != null);

      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    #endregion

    #region IAbstractDomain Members - not used

    bool IAbstractDomain.IsBottom
    {
      get { throw new NotImplementedException(); }
    }

    bool IAbstractDomain.IsTop
    {
      get { throw new NotImplementedException(); }
    }

    bool IAbstractDomain.LessEqual(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    IAbstractDomain IAbstractDomain.Bottom
    {
      get { throw new NotImplementedException(); }
    }

    IAbstractDomain IAbstractDomain.Top
    {
      get { throw new NotImplementedException(); }
    }

    IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
    {
      throw new NotImplementedException();
    }

    TT IAbstractDomain.To<TT>(IFactory<TT> factory)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region ICloneable Members

    object ICloneable.Clone()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
  #endregion

  /// <summary>
  ///  The interface for the elements abstracting an array segmentation
  /// </summary>
  [ContractClass(typeof(IAbstractDomainForArraySegmentationAbstractionContracts<,>))]
  public interface IAbstractDomainForArraySegmentationAbstraction<T, Variable>
  : IAbstractDomainWithRenaming<T, Variable>
    where T : IAbstractDomainForArraySegmentationAbstraction<T, Variable>
  {
    T AssumeInformationFrom<Expression>(INumericalAbstractDomainQuery<Variable, Expression> oracle);
  }

  #region Contracts for IAbstractDomainForArraySegmentationAbstraction
  [ContractClassFor(typeof(IAbstractDomainForArraySegmentationAbstraction<,>))]
  abstract class IAbstractDomainForArraySegmentationAbstractionContracts<T, Variable>
    : IAbstractDomainForArraySegmentationAbstraction<T, Variable>
      where T : IAbstractDomainForArraySegmentationAbstraction<T, Variable>
  {
    public T AssumeInformationFrom<Expression>(INumericalAbstractDomainQuery<Variable, Expression> oracle)
    {
      Contract.Requires(oracle != null);
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    #region Dummy
    #region IAbstractDomainWithRenaming<T,Variable> Members

    public T Rename(Dictionary<Variable, FList<Variable>> renaming)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IAbstractDomain Members

    public bool IsBottom
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsTop
    {
      get { throw new NotImplementedException(); }
    }

    public bool LessEqual(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    public IAbstractDomain Bottom
    {
      get { throw new NotImplementedException(); }
    }

    public IAbstractDomain Top
    {
      get { throw new NotImplementedException(); }
    }

    public IAbstractDomain Join(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    public IAbstractDomain Meet(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    public IAbstractDomain Widening(IAbstractDomain prev)
    {
      throw new NotImplementedException();
    }

    public Tp To<Tp>(IFactory<Tp> factory)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region ICloneable Members

    public object Clone()
    {
      throw new NotImplementedException();
    }

    #endregion
    #endregion
  }
  #endregion

  [ContractClass(typeof(IPureExpressionAssignmentsContracts<,>))]
  public interface IPureExpressionAssignments<Variable, Expression>
  {
    /// <summary>
    /// The set of variables defined in this environment
    /// </summary>
    List<Variable> Variables { get; }

    /// <summary>
    /// Add a new variable to the environment.
    /// </summary>
    /// <param name="var">must be a variable</param>
    void AddVariable(Variable var);

    /// <summary>
    /// The transfer function corresponding to the assignment <code>x := exp</code>.
    /// </summary>
    /// <param name="x">Must be a variable</param>
    void Assign(Expression x, Expression exp);

    /// <summary>
    /// Project the value of the variable <code>var</code>.
    /// It is as setting its value to top
    /// </summary>
    /// <param name="var">Must be a variable</param>
    void ProjectVariable(Variable var);

    /// <summary>
    /// Remove the variable from the current environment.
    /// Its values is projected, so that the relations are kept.
    /// The variable is removed from the octagon
    /// </summary>
    /// <param name="var">Must be a variable</param>
    void RemoveVariable(Variable var);

    /// <summary>
    /// Rename the variable <code>OldName</code> with <code>NewName</code>
    /// </summary>
    /// <param name="OldName">The old of the variable</param>
    /// <param name="NewName">The new name</param>
    void RenameVariable(Variable OldName, Variable NewName);
  }

  #region Contracts for IPureExpressionAssigments
  [ContractClassFor(typeof(IPureExpressionAssignments<,>))]
  abstract class IPureExpressionAssignmentsContracts<Variable, Expression>
    : IPureExpressionAssignments<Variable, Expression>
  {
    #region IPureExpressionAssignments<Variable,Expression> Members

    List<Variable> IPureExpressionAssignments<Variable, Expression>.Variables
    {
      get 
      {
        Contract.Ensures(Contract.Result<List<Variable>>() != null);

        return default(List<Variable>);
      }
    }

    void IPureExpressionAssignments<Variable, Expression>.AddVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.Assign(Expression x, Expression exp)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.ProjectVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.RemoveVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.RenameVariable(Variable OldName, Variable NewName)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
  #endregion

  /// <summary>
  /// The capability of handling tests
  /// </summary>    
  public partial interface IPureExpressionTest<Variable, Expression>
  {
    /// <summary>
    /// The boolean guard for positive tests.
    /// </summary>
    /// <returns>
    /// An over approximation of { s | s \in \gamma(this) and [[guard]](s) == true }
    /// </returns>
    IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard);

    /// <summary>
    /// The boolean guard for negative tests.
    /// </summary>
    /// <returns>
    /// An over approximation of { s | s \in \gamma(this) and [[! guard]](s) == true }
    /// </returns>
    IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard);

    /// <summary>
    /// Checks whether the expression <code>exp</code> holds or not
    /// </summary>
    /// <param name="exp">The expression to check</param>
    /// <returns>Bottom if the expression is unreached, true if it holds, false it does not hold, Top if it is unknown</returns>
    FlatAbstractDomain<bool> CheckIfHolds(Expression exp);

    /// <summary>
    /// Pushes some abstract domain specific Fact. 
    /// Abstract domains are free to ignore it
    /// </summary>
    void AssumeDomainSpecificFact(DomainSpecificFact fact);

  }

  #region IPureExpressionTest<Variable,Expression> contract binding
  [ContractClass(typeof(IPureExpressionTestContract<,>))]
  public partial interface IPureExpressionTest<Variable,Expression>
  {
  }

  [ContractClassFor(typeof(IPureExpressionTest<,>))]
  abstract class IPureExpressionTestContract<Variable,Expression> : IPureExpressionTest<Variable,Expression>
  {
    #region IPureExpressionTest<Variable,Expression> Members

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestTrue(Expression guard)
    {
      Contract.Requires(guard != null);
      Contract.Ensures(Contract.Result<IAbstractDomainForEnvironments<Variable, Expression>>() != null);

      return default(IAbstractDomainForEnvironments<Variable, Expression>);
    }

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestFalse(Expression guard)
    {
      Contract.Requires(guard != null);
      Contract.Ensures(Contract.Result<IAbstractDomainForEnvironments<Variable, Expression>>() != null);

      return default(IAbstractDomainForEnvironments<Variable, Expression>);
    }

    FlatAbstractDomain<bool> IPureExpressionTest<Variable, Expression>.CheckIfHolds(Expression exp)
    {
      Contract.Requires(exp != null);
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

      return default(FlatAbstractDomain<bool>);
    }

    #endregion

    #region IPureExpressionTest<Variable,Expression> Members


    void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
    {
      Contract.Requires(fact != null);

      throw new NotImplementedException();
    }

    #endregion
  }
  #endregion

  [ContractClass(typeof(IAssignInParallelContracts<,>))]
  public interface IAssignInParallel<Variable, Expression>
  {
    /// <summary>
    /// The parallel assigment
    /// </summary>
    /// <param name="sourcesToTargets">The map from sources to their targets</param>
    void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert);
  }


  #region Contracts for IAssignInParallel<Variable, Expression>
  [ContractClassFor(typeof(IAssignInParallel<,>))]
  abstract class IAssignInParallelContracts<Variable, Expression>
    : IAssignInParallel<Variable, Expression>
  {
    #region IAssignInParallel<Variable,Expression> Members

    void IAssignInParallel<Variable, Expression>.AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      Contract.Requires(sourcesToTargets != null);
      Contract.Requires(convert != null);
    }

    #endregion
  }
  #endregion

  public interface IAbstractDomainForEnvironments<Variable, Expression> :
    IAbstractDomain,
    IPureExpressionAssignments<Variable, Expression>,
    IPureExpressionTest<Variable, Expression>,
    IAssignInParallel<Variable, Expression>
  {
    /// <returns>
    /// A textual representation for the expression <code>exp</code>
    /// </returns>
    string ToString(Expression exp);
  }

  public partial interface INumericalAbstractDomainQuery<Variable, Expression>
  {
    #region Queries
    [Pure]
    DisInterval BoundsFor(Expression v);

    [Pure]
    DisInterval BoundsFor(Variable v);

    [Pure]
    IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict);
    
    [Pure]
    IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict);

    [Pure]
    IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict);

    [Pure]
    IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict);

    [Pure]
    IEnumerable<Variable> EqualitiesFor(Variable v);

    #endregion

    #region Checks
    [Pure]
    FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp);

    [Pure]
    FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2);

    [Pure]
    FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2);

    [Pure]
    FlatAbstractDomain<bool> CheckIfLessThan(Variable v1, Variable v2);
    
    [Pure]
    FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2);

    [Pure]
    FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2);

    [Pure]
    FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable e1, Variable e2);

    [Pure]
    FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2);

    [Pure]
    FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2);

    [Pure]
    FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2);

    [Pure]
    FlatAbstractDomain<bool> CheckIfNonZero(Expression e);

    #endregion

    #region Conversion Exp -> Variable

    [Pure]
    Variable ToVariable(Expression exp);

    #endregion
  }

  #region INumericalAbstractDomainQuery<Variable,Expression> contract binding
  [ContractClass(typeof(INumericalAbstractDomainQueryContract<,>))]
  public partial interface INumericalAbstractDomainQuery<Variable,Expression>
  {

  }

  [ContractClassFor(typeof(INumericalAbstractDomainQuery<,>))]
  abstract class INumericalAbstractDomainQueryContract<Variable,Expression> : INumericalAbstractDomainQuery<Variable, Expression>
  {
    #region INumericalAbstractDomainQuery<Variable,Expression> Members

    DisInterval INumericalAbstractDomainQuery<Variable, Expression>.BoundsFor(Expression v)
    {
      Contract.Ensures(Contract.Result<DisInterval>() != null);

      return default(DisInterval);
    }

    DisInterval INumericalAbstractDomainQuery<Variable, Expression>.BoundsFor(Variable v)
    {
      Contract.Ensures(Contract.Result<DisInterval>() != null);

      return default(DisInterval);
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.LowerBoundsFor(Expression v, bool strict)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Expression>>() != null);      

      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.UpperBoundsFor(Expression v, bool strict)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Expression>>() != null);  

      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.LowerBoundsFor(Variable v, bool strict)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Expression>>() != null);  
     
      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.UpperBoundsFor(Variable v, bool strict)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Expression>>() != null);  

      throw new NotImplementedException();
    }

    IEnumerable<Variable> INumericalAbstractDomainQuery<Variable, Expression>.EqualitiesFor(Variable v)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Variable>>() != null);

      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfGreaterEqualThanZero(Expression exp)
    {
      Contract.Requires(exp != null);

      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);  

      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThan(Expression e1, Expression e2)
    {
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThanIncomplete(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThan(Variable v1, Variable v2)
    {
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThan_Un(Expression v1, Expression v2)
    {
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessEqualThan_Un(Expression e1, Expression e2)
    {
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessEqualThan(Variable e1, Variable e2)
    {
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfEqual(Expression e1, Expression e2)
    {
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
    {
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

      return null;
    }


    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfNonZero(Expression e)
    {
      Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

      throw new NotImplementedException();
    }

    // ok to return default(Variable)
    Variable INumericalAbstractDomainQuery<Variable, Expression>.ToVariable(Expression exp)
    {
      return default(Variable);
    }

    #endregion



  }
  #endregion

  /// <summary>
  ///  Abstractions over the numerical type
  /// </summary>
  public interface INumericalTypeQuery<NType>
  {
    bool IsGreaterEqualThanZero(NType val);
    bool IsGreaterThanZero(NType val);
    bool IsLessThanZero(NType val);
    bool IsLessEqualThanZero(NType val);
    bool IsLessThan(NType val1, NType val2);
    bool IsLessEqualThan(NType val1, NType val2);
    bool IsZero(NType val);
    bool IsNotZero(NType val);
    bool IsPlusInfinity(NType val);
    bool IsMinusInfinity(NType val);

    
    NType PlusInfinity { get; }
    NType MinusInfinty { get; }
    bool TryAdd(NType left, NType right, out NType result);
  }

  public interface ISetOfNumbersAbstraction<Variable, Expression, NType, IType>
    : INumericalTypeQuery<NType>
  {
    bool TryGetValue(Variable var, bool isSigned, out IType intv);

    IType Eval(Expression exp);
    IType Eval(Variable var);

    // Abstractions over the intervals 
    FlatAbstractDomain<bool> IsNotZero(IType intv);
    bool IsGreaterEqualThanZero(IType intv);
    bool IsLessEqualThanZero(IType intv);
    bool AreEqual(IType left, IType right);
    
    IType IntervalUnknown { get; }
    IType IntervalZero { get; }
    IType IntervalOne { get; }
    IType Interval_Positive { get; }
    IType IntervalSingleton(NType val);
    IType IntervalRightOpen(NType inf);
    IType IntervalLeftOpen(NType sup);

    IType Interval_Add(IType left, IType right);
    IType Interval_Div(IType left, IType right);
    IType Interval_Sub(IType left, IType right);
    IType Interval_Mul(IType left, IType right);
    IType Interval_UnaryMinus(IType left);
    IType Interval_Rem(IType left, IType right);

    IType Interval_BitwiseAnd(IType left, IType right);
    IType Interval_BitwiseOr(IType left, IType right);

    IType Interval_ShiftLeft(IType left, IType right);
    IType Interval_ShiftRight(IType left, IType right);

    IType For(Byte v);

    IType For(Double d);
    IType For(Int16 v);
    IType For(Int32 v);
    IType For(Int64 v);
    IType For(SByte s);
    IType For(UInt16 u);
    IType For(UInt32 u);
    IType For(Rational r);
    IType For(NType inf, NType sup);
  }

  [ContractClass(typeof(IIntervalAbstractionContracts<,>))] 
  public interface IIntervalAbstraction<Variable, Expression>
    : 
    IAbstractDomain,
    INumericalAbstractDomainQuery<Variable, Expression>,
    IPureExpressionAssignments<Variable, Expression>,
    IPureExpressionTest<Variable, Expression>,
    IAssignInParallel<Variable, Expression>
  {
    bool LessEqual(IIntervalAbstraction<Variable, Expression> other);
    IIntervalAbstraction<Variable, Expression> Join(IIntervalAbstraction<Variable, Expression> other);
    IIntervalAbstraction<Variable, Expression> Widening(IIntervalAbstraction<Variable, Expression> other);

    List<Pair<Variable, Int32>> IntConstants { get; }

    void AssumeInDisInterval(Variable x, DisInterval value);
    void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState);

    IIntervalAbstraction<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle);

    new IIntervalAbstraction<Variable, Expression> TestTrue(Expression exp);
    new IIntervalAbstraction<Variable, Expression> TestFalse(Expression exp);

    IIntervalAbstraction<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2);
    IIntervalAbstraction<Variable, Expression> TestTrueLessThan(Variable v1, Variable v2);
    IIntervalAbstraction<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2);
    IIntervalAbstraction<Variable, Expression> TestTrueLessEqualThan(Variable v1, Variable v2);
    IIntervalAbstraction<Variable, Expression> TestTrueEqual(Expression exp1, Expression exp2);
    IIntervalAbstraction<Variable, Expression> TestTrueGeqZero(Expression exp);
  }

  #region Contracts for IIntervalAbstraction<Variable, Expression>
  [ContractClassFor(typeof(IIntervalAbstraction<,>))]
  abstract class IIntervalAbstractionContracts<Variable, Expression> : IIntervalAbstraction<Variable, Expression>
  {
    #region IIntervalAbstraction<Variable,Expression> Members

    bool IIntervalAbstraction<Variable, Expression>.LessEqual(IIntervalAbstraction<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      return default(bool);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.Join(IIntervalAbstraction<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);

      return default(IIntervalAbstraction<Variable, Expression>);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.Widening(IIntervalAbstraction<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);

      return default(IIntervalAbstraction<Variable, Expression>);
    }

    List<Pair<Variable, int>> IIntervalAbstraction<Variable, Expression>.IntConstants
    {
      get 
      {
        Contract.Ensures(Contract.Result<List<Pair<Variable, int>>>() != null);

        return default(List<Pair<Variable, int>>);
      }
    }

    void IIntervalAbstraction<Variable, Expression>.AssumeInDisInterval(Variable x, DisInterval value)
    {
      Contract.Requires(x != null);
      Contract.Requires(value != null);
    }

    void IIntervalAbstraction<Variable, Expression>.Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      Contract.Requires(x != null);
      Contract.Requires(exp != null);
      Contract.Requires(preState != null);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
    {
      Contract.Requires(oracle != null);
      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);

      return default(IIntervalAbstraction<Variable, Expression>);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrue(Expression exp)
    {
      Contract.Requires(exp != null);
      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);
      
      return default(IIntervalAbstraction<Variable, Expression>);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestFalse(Expression exp)
    {
      Contract.Requires(exp != null);

      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);

      return default(IIntervalAbstraction<Variable, Expression>);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessThan(Expression exp1, Expression exp2)
    {
      Contract.Requires(exp1 != null);
      Contract.Requires(exp2 != null);

      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);
      
      return default(IIntervalAbstraction<Variable, Expression>);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessThan(Variable v1, Variable v2)
    {
      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);

      return default(IIntervalAbstraction<Variable, Expression>);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessEqualThan(Expression exp1, Expression exp2)
    {
      Contract.Requires(exp1 != null);
      Contract.Requires(exp2 != null);
      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);
      
      return default(IIntervalAbstraction<Variable, Expression>);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessEqualThan(Variable v1, Variable v2)
    {
      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);

      return default(IIntervalAbstraction<Variable, Expression>);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueEqual(Expression exp1, Expression exp2)
    {
      Contract.Requires(exp1 != null);
      Contract.Requires(exp2 != null);

      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);

      return default(IIntervalAbstraction<Variable, Expression>);
    }

    IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueGeqZero(Expression exp)
    {
      Contract.Requires(exp != null);

      Contract.Ensures(Contract.Result<IIntervalAbstraction<Variable, Expression>>() != null);

      return default(IIntervalAbstraction<Variable, Expression>);
    }

    #endregion

    #region IAbstractDomain Members

    bool IAbstractDomain.IsBottom
    {
      get { throw new NotImplementedException(); }
    }

    bool IAbstractDomain.IsTop
    {
      get { throw new NotImplementedException(); }
    }

    bool IAbstractDomain.LessEqual(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    IAbstractDomain IAbstractDomain.Bottom
    {
      get { throw new NotImplementedException(); }
    }

    IAbstractDomain IAbstractDomain.Top
    {
      get { throw new NotImplementedException(); }
    }

    IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
    {
      throw new NotImplementedException();
    }

    T IAbstractDomain.To<T>(IFactory<T> factory)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region ICloneable Members

    object ICloneable.Clone()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region INumericalAbstractDomainQuery<Variable,Expression> Members

    DisInterval INumericalAbstractDomainQuery<Variable, Expression>.BoundsFor(Expression v)
    {
      throw new NotImplementedException();
    }

    DisInterval INumericalAbstractDomainQuery<Variable, Expression>.BoundsFor(Variable v)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.LowerBoundsFor(Expression v, bool strict)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.UpperBoundsFor(Expression v, bool strict)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.LowerBoundsFor(Variable v, bool strict)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.UpperBoundsFor(Variable v, bool strict)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfGreaterEqualThanZero(Expression exp)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThan(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThan(Variable v1, Variable v2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThan_Un(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessEqualThan(Variable e1, Variable e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessEqualThan_Un(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfEqual(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfEqualIncomplete(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }


    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfNonZero(Expression e)
    {
      throw new NotImplementedException();
    }

    Variable INumericalAbstractDomainQuery<Variable, Expression>.ToVariable(Expression exp)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region IPureExpressionAssignments<Variable,Expression> Members

    List<Variable> IPureExpressionAssignments<Variable, Expression>.Variables
    {
      get { throw new NotImplementedException(); }
    }

    void IPureExpressionAssignments<Variable, Expression>.AddVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.Assign(Expression x, Expression exp)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.ProjectVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.RemoveVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.RenameVariable(Variable OldName, Variable NewName)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IPureExpressionTest<Variable,Expression> Members

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestTrue(Expression guard)
    {
      throw new NotImplementedException();
    }

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestFalse(Expression guard)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> IPureExpressionTest<Variable, Expression>.CheckIfHolds(Expression exp)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IAssignInParallel<Variable,Expression> Members

    void IAssignInParallel<Variable, Expression>.AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IPureExpressionTest<Variable,Expression> Members


    void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region INumericalAbstractDomainQuery<Variable,Expression> Members

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThanIncomplete(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }
    IEnumerable<Variable> INumericalAbstractDomainQuery<Variable, Expression>.EqualitiesFor(Variable v)
    {
      throw new NotImplementedException();
    }

    #endregion


  }
  #endregion

  /// <summary>
  /// A numerical abstract domain
  /// </summary>
  /// <typeparam name="Expression">The language of the expression understood by the domain</typeparam>
  [ContractClass(typeof(INumericalAbstractDomainContracts<,>))]
  public interface INumericalAbstractDomain<Variable, Expression> :
      IAbstractDomainForEnvironments<Variable, Expression>,
      INumericalAbstractDomainQuery<Variable, Expression>
  {
    void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState);

    /// <summary>
    /// The variable <code>x</code> ranges in the interval <code>intv</code>
    /// </summary>
    void AssumeInDisInterval(Variable x, DisInterval intv);

    /// <summary>
    /// The variables which have a definite <code>Int32</code> value 
    /// </summary>
    List<Pair<Variable, Int32>> IntConstants { get; }

    /// <summary>
    /// Removes the relations in this domain that are also implied by the oracle
    /// </summary>
    /// <param name="oracle"></param>
    /// <returns>A Fresh domain where the relations implied by <code>oracle</code> have been removed</returns>
    INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle);

    /// <summary>
    /// Add the constraint <code> 0 \leq exp </code> to the abstract domain
    /// </summary>
    INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression exp);

    /// <summary>
    /// Add the constraint <code> exp1 \lt exp2 </code>
    /// </summary>
    INumericalAbstractDomain<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2);

    /// <summary>
    /// Add the constraint <code> exp1 \leq exp2 </code>
    /// </summary>
    INumericalAbstractDomain<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2);

    /// <summary>
    ///  Add the constraint <code>exp1 == exp2</code>
    /// </summary>
    INumericalAbstractDomain<Variable, Expression> TestTrueEqual(Expression exp1, Expression exp2);

    /// <summary>
    /// Set the floating point type for the expression exp
    /// </summary>
    void SetFloatType(Variable v, ConcreteFloat f);
    
    /// <summary>
    /// Get the floating type for exp, if any
    /// </summary>
    FlatAbstractDomain<ConcreteFloat> GetFloatType(Variable v);

  }

  #region Contracts for INumericalAbstractDomain
  [ContractClassFor(typeof(INumericalAbstractDomain<,>))]
  abstract partial class INumericalAbstractDomainContracts<Variable, Expression>
    : INumericalAbstractDomain<Variable, Expression>
  {
    #region INumericalAbstractDomain<Variable,Expression> Members

    void INumericalAbstractDomain<Variable, Expression>.Assign(Expression x, Expression exp, 
      INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      Contract.Requires(x != null);
      Contract.Requires(exp != null);
      Contract.Requires(preState != null);
    }

    void INumericalAbstractDomain<Variable, Expression>.AssumeInDisInterval(Variable x, DisInterval value)
    {
      Contract.Requires(x != null);
      Contract.Requires(value != null) ;
    }

    List<Pair<Variable, int>> INumericalAbstractDomain<Variable, Expression>.IntConstants
    {
      get
      {
        Contract.Ensures(Contract.Result<List<Pair<Variable, int>>>() != null);
        return default(List<Pair<Variable, int>>);
      }
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
    {
      Contract.Requires(oracle != null);

      Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

      return default(INumericalAbstractDomain<Variable, Expression>);
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueGeqZero(Expression exp)
    {
      Contract.Requires(exp != null);
      Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

      return default(INumericalAbstractDomain<Variable, Expression>);
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessThan(Expression exp1, Expression exp2)
    {
      Contract.Requires(exp1 != null);
      Contract.Requires(exp2 != null);
      Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

      return default(INumericalAbstractDomain<Variable, Expression>);
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessEqualThan(Expression exp1, Expression exp2)
    {
      Contract.Requires(exp1 != null);
      Contract.Requires(exp2 != null);
      Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

      return default(INumericalAbstractDomain<Variable, Expression>);
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueEqual(Expression exp1, Expression exp2)
    {
      Contract.Requires(exp1 != null);
      Contract.Requires(exp2 != null);
      Contract.Ensures(Contract.Result<INumericalAbstractDomain<Variable, Expression>>() != null);

      return default(INumericalAbstractDomain<Variable, Expression>);
    }

    void INumericalAbstractDomain<Variable, Expression>.SetFloatType(Variable v, ConcreteFloat f)
    {
      Contract.Requires(v != null);
    }

    FlatAbstractDomain<ConcreteFloat> INumericalAbstractDomain<Variable, Expression>.GetFloatType(Variable v)
    {
      Contract.Requires(v != null);

      Contract.Ensures(Contract.Result<FlatAbstractDomain<ConcreteFloat>>() != null);


      return default(FlatAbstractDomain<ConcreteFloat>);
    }

    Variable INumericalAbstractDomainQuery<Variable, Expression>.ToVariable(Expression exp)
    {
      return default(Variable);
    }
    #endregion


    #region IAbstractDomainForEnvironments<Variable,Expression> Members

    public string ToString(Expression exp)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IAbstractDomain Members

    public bool IsBottom
    {
      get { throw new NotImplementedException(); }
    }

    public bool IsTop
    {
      get { throw new NotImplementedException(); }
    }

    public bool LessEqual(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    public IAbstractDomain Bottom
    {
      get { throw new NotImplementedException(); }
    }

    public IAbstractDomain Top
    {
      get { throw new NotImplementedException(); }
    }

    public IAbstractDomain Join(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    public IAbstractDomain Meet(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    public IAbstractDomain Widening(IAbstractDomain prev)
    {
      throw new NotImplementedException();
    }

    public T To<T>(IFactory<T> factory)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region ICloneable Members

    public object Clone()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IPureExpressionAssignments<Variable,Expression> Members

    public List<Variable> Variables
    {
      get { throw new NotImplementedException(); }
    }

    public void AddVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    public void Assign(Expression x, Expression exp)
    {
      throw new NotImplementedException();
    }

    public void ProjectVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    public void RemoveVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IPureExpressionTest<Variable,Expression> Members

    public IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {
      throw new NotImplementedException();
    }

    public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IAssignInParallel<Variable,Expression> Members

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region INumericalAbstractDomainQuery<Variable,Expression> Members

    public DisInterval BoundsFor(Expression v)
    {
      throw new NotImplementedException();
    }

    public DisInterval BoundsFor(Variable v)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict)
    {
      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfLessThan(Variable v1, Variable v2)
    {
      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable e1, Variable e2)
    {
      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
    {
      return null;
    }

    public FlatAbstractDomain<bool> CheckIfNonZero(Expression e)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IAbstractDomainForEnvironments<Variable,Expression> Members

    string IAbstractDomainForEnvironments<Variable, Expression>.ToString(Expression exp)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IAbstractDomain Members

    bool IAbstractDomain.IsBottom
    {
      get { throw new NotImplementedException(); }
    }

    bool IAbstractDomain.IsTop
    {
      get { throw new NotImplementedException(); }
    }

    bool IAbstractDomain.LessEqual(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    IAbstractDomain IAbstractDomain.Bottom
    {
      get { throw new NotImplementedException(); }
    }

    IAbstractDomain IAbstractDomain.Top
    {
      get { throw new NotImplementedException(); }
    }

    IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
    {
      throw new NotImplementedException();
    }

    IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
    {
      throw new NotImplementedException();
    }

    T IAbstractDomain.To<T>(IFactory<T> factory)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region ICloneable Members

    object ICloneable.Clone()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IPureExpressionAssignments<Variable,Expression> Members

    List<Variable> IPureExpressionAssignments<Variable, Expression>.Variables
    {
      get { throw new NotImplementedException(); }
    }

    void IPureExpressionAssignments<Variable, Expression>.AddVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.Assign(Expression x, Expression exp)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.ProjectVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.RemoveVariable(Variable var)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionAssignments<Variable, Expression>.RenameVariable(Variable OldName, Variable NewName)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IPureExpressionTest<Variable,Expression> Members

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestTrue(Expression guard)
    {
      throw new NotImplementedException();
    }

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestFalse(Expression guard)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> IPureExpressionTest<Variable, Expression>.CheckIfHolds(Expression exp)
    {
      throw new NotImplementedException();
    }

    void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IAssignInParallel<Variable,Expression> Members

    void IAssignInParallel<Variable, Expression>.AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region INumericalAbstractDomainQuery<Variable,Expression> Members

    DisInterval INumericalAbstractDomainQuery<Variable, Expression>.BoundsFor(Expression v)
    {
      throw new NotImplementedException();
    }

    DisInterval INumericalAbstractDomainQuery<Variable, Expression>.BoundsFor(Variable v)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.LowerBoundsFor(Expression v, bool strict)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.UpperBoundsFor(Expression v, bool strict)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.LowerBoundsFor(Variable v, bool strict)
    {
      throw new NotImplementedException();
    }

    IEnumerable<Expression> INumericalAbstractDomainQuery<Variable, Expression>.UpperBoundsFor(Variable v, bool strict)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfGreaterEqualThanZero(Expression exp)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThan(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThan(Variable v1, Variable v2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThan_Un(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessEqualThan(Variable e1, Variable e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessEqualThan_Un(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfEqual(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }

    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfNonZero(Expression e)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region INumericalAbstractDomainQuery<Variable,Expression> Members


    IEnumerable<Variable> INumericalAbstractDomainQuery<Variable, Expression>.EqualitiesFor(Variable v)
    {
      throw new NotImplementedException();
    }
    FlatAbstractDomain<bool> INumericalAbstractDomainQuery<Variable, Expression>.CheckIfLessThanIncomplete(Expression e1, Expression e2)
    {
      throw new NotImplementedException();
    }
    #endregion


  }


  #endregion

  interface IStringAbstractDomain<Elements, Variable, Expression> :
    IAbstractDomainForEnvironments<Variable, Expression>
    where Elements : IStringAbstraction
  {
    /// <summary>
    /// The abstract concatenation
    /// </summary>
    void Concat(Expression/*!*/ target, Expression/*!*/ left, Expression/*!*/ right);
  
    /// <summary>
    /// Insert <code>what</code> at the position <code>where</code> in <code>target</code>
    /// </summary>
    void Insert(Expression/*!*/ target, Expression/*!*/ which, Expression/*!*/ where, Expression/*!*/ what);

    /// <summary>
    /// Remove all the spaces from <code>who</code>
    /// </summary>
    void Trim(Expression/*!*/ who, Expression/*!*/ what);

    FlatAbstractDomain<int> CompareTo(Expression/*!*/ left, Expression/*!*/ right);
    FlatAbstractDomain<bool> Contains(Expression/*!*/ who, Expression/*!*/ what);
    FlatAbstractDomain<bool> StartsWith(Expression/*!*/ who, Expression/*!*/ what);
  }
  #endregion

  #region Abstract Domain-specific facts
  public class DomainSpecificFact
  {
    virtual public bool IsAssumeInFloatInterval<Variable>(out Variable var, out Interval_IEEE754 intv)
    {
      var = default(Variable);
      intv = default(Interval_IEEE754);

      return false;
    }

    public class AssumeInFloatInterval : DomainSpecificFact
    {
      private readonly object var;
      private readonly Interval_IEEE754 intv;

      public AssumeInFloatInterval(object var, Interval_IEEE754 intv)
      {
        this.var = var;
        this.intv = intv;
      }

      public override bool IsAssumeInFloatInterval<Variable>(out Variable v, out Interval_IEEE754 intv)
      {
        if (this.var is Variable)
        {
          v = (Variable)this.var;
          intv = this.intv;

          return true;
        }
        
        return base.IsAssumeInFloatInterval(out v, out intv);
      }

    }

  }


  #endregion

  #region Logging
  public delegate string StringClosure();

  public delegate void Logger(string format, params StringClosure[] args);

  public static class VoidLogger
  {
    public static void Log(string format, params StringClosure[] args)
    {
      // It does nothing
    }
  }

  #endregion
}
