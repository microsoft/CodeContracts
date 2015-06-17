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

#if CONTRACTS_EXPERIMENTAL
using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using QuickGraph;
using QuickGraph.Algorithms.ConnectedComponents;

namespace Microsoft.Research.DataStructures
{

  public interface IPartitionAbstraction<Variable, Expression> 
    : IAbstractDomain, IAssignInParallel<Variable, Expression>
  {
    void Simplify();
    void Simplify(Expression knowledge);
    Set<Variable> Indexes();
    Set<Expression> Singles();

    IPartitionAbstraction<Variable, Expression> PartitionRange(Expression lb, Expression ub);
    IPartitionAbstraction<Variable, Expression> PartitionSplit(Expression single);

    void Merge(Variable exp);

    Set<int> Dimensions();

    IPartitionAbstraction<Variable, Expression> Factori();

    Variable Representative();

    Dictionary<int, Set<int>> TransformationMapFor(IPartitionAbstraction<Variable, Expression> toPartition);

    Set<int> EmptyDimensionsInContext(Expression knowledge);

    Set<Pair<int, TouchKind>> DimensionsTouchedBy(Expression constraint, Set<Variable> notWanted);

    IPartitionAbstraction<Variable, Expression> PartialAssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> converter ); // carefull: can return a non consistent partition
  }

}

namespace Microsoft.Research.AbstractDomains
{

  /// <summary>
  /// A simple abstract domain abstracting (environments of) partitions
  /// </summary>
  /// <typeparam name="Expression"></typeparam>
  public class SimplePartitionAbstractDomain<Variable, Expression> :
    FunctionalAbstractDomain<SimplePartitionAbstractDomain<Variable, Expression>, Variable, IPartitionAbstraction<Variable, Expression>>,
    IAbstractDomainForEnvironments<Variable, Expression>
  {

    #region Static

    static SimplePartitionAbstractDomain()
    {
      Trace = true;
    }

    public static bool Trace
    {
      private get;
      set;
    }

    #endregion

    #region Private State

    IPartitionAbstraction<Variable, Expression> onePartition;
    IExpressionDecoder<Variable, Expression>/*!*/ decoder;
    IExpressionEncoder<Variable, Expression>/*!*/ encoder;

    #endregion

    #region Constructor

    public SimplePartitionAbstractDomain(IExpressionEncoder<Variable, Expression>/*!*/ encoder,IExpressionDecoder<Variable, Expression>/*!*/ decoder)
    {
      this.encoder = encoder; 
      this.decoder = decoder;
      this.onePartition = new SimplePartitionAbstractionOctagonBased<Variable, Expression>(encoder, decoder);
    }

    public SimplePartitionAbstractDomain(IExpressionEncoder<Variable, Expression>/*!*/ encoder, IExpressionDecoder<Variable, Expression>/*!*/ decoder, IPartitionAbstraction<Variable, Expression> partition)
    {
      this.encoder = encoder;
      this.decoder = decoder;
      this.onePartition = partition;
    }

    private SimplePartitionAbstractDomain(SimplePartitionAbstractDomain<Variable, Expression>/*!*/ source)
      : base(source)
    {
      this.encoder = source.encoder; 
      this.decoder = source.decoder;
      this.onePartition = (IPartitionAbstraction<Variable, Expression>)source.onePartition.Clone();
    }

    #endregion

    #region From FunctionalAbstractDomain
    
    public override object Clone()
    {
      return new SimplePartitionAbstractDomain<Variable, Expression>(this);
    }

    protected override SimplePartitionAbstractDomain<Variable, Expression> Factory()
    {
      return new SimplePartitionAbstractDomain<Variable, Expression>(this.encoder,this.decoder,this.onePartition);
    }

    protected override string ToLogicalFormula(Variable d, IPartitionAbstraction<Variable, Expression> c)
    {
      return null;
    }

    protected override T To<T>(Variable d, IPartitionAbstraction<Variable, Expression> c, IFactory<T> factory)
    {
      return factory.Constant(true);
    }

    /// <summary>
    /// Join is override to take into account that a bottom partition does not mean unreachability
    /// So here, even if the bottom partition is attached to some expression, this binding is kept.
    /// </summary>
    public override SimplePartitionAbstractDomain<Variable, Expression> Join(SimplePartitionAbstractDomain<Variable, Expression> right)
    {
      //// Here we do not have trivial joins as we want to join maps of different cardinality
      //if (this.IsBottom)
      //  return right;
      //if (right.IsBottom)
      //  return (SimplePartitionAbstractDomain<Variable, Expression>)this;

      //SimplePartitionAbstractDomain<Variable, Expression> result = this.Factory();

      //foreach (Expression x in this.Keys)       // For all the elements in the intersection do the point-wise join
      //{
      //  IPartitionAbstraction<Variable, Expression> right_x;

      //  if (right.TryGetValue(x, out right_x))
      //  {
      //    var join = this[x].Join(right_x);

      //    if (!join.IsTop)
      //    { // We keep in the map only the elements that are != top
      //      result.AddElement(x, (IPartitionAbstraction<Variable, Expression>)join);
      //      //result[x] = (Codomain)join;
      //    }
      //  }
      //}

      //return result;
      return this.Join(right, new Set<Variable>());
    }

    public SimplePartitionAbstractDomain<Variable, Expression> Join(SimplePartitionAbstractDomain<Variable, Expression> right, Set<Variable> keep)
    {
      // Here we do not have trivial joins as we want to join maps of different cardinality
      if (this.IsBottom)
        return right;
      if (right.IsBottom)
        return (SimplePartitionAbstractDomain<Variable, Expression>)this;

      SimplePartitionAbstractDomain<Variable, Expression> result = this.Factory();

      foreach (var x in this.Keys)       // For all the elements in the intersection do the point-wise join
      {
        IPartitionAbstraction<Variable, Expression> right_x;

        if (right.TryGetValue(x, out right_x))
        {
          var join = this[x].Join(right_x);

          if (!join.IsTop)
          { // We keep in the map only the elements that are != top
            result.AddElement(x, (IPartitionAbstraction<Variable, Expression>)join);
            //result[x] = (Codomain)join;
          }
        }
        else
        {
          if (keep.Contains(x))
          {
            result.AddElement(x, this[x]);
          }
          keep.Remove(x);
        }
      }

      foreach (var x in keep)
      {
        if (!result.ContainsKey(x))
        {
          result.AddElement(x, right[x]);
        }
      }

      return result;
    }

    #endregion

    #region IPureExpressionAssignmentsWithForward<Expression> Members

    public void Assign(Expression x, Expression exp)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public Set<Variable> Variables
    {
      get
      {
        return new Set<Variable>(this.Keys);
      }
    }

    public void AddVariable(Variable var)
    {
      // do nothing //t-maper@57: TODO ?
    }

    public void ProjectVariable(Variable var)
    {
      this.RemoveVariable(var);
    }

    public void RemoveVariable(Variable var)
    {
      this.RemoveElement(var);
    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {
      this[NewName] = this[OldName];
      this.RemoveVariable(OldName);
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    public IAbstractDomainForEnvironments<Variable, Expression>/*!*/ TestTrue(Expression/*!*/ guard)
    {
      return this; // TODO: return throw new AbstractInterpretationException();
    }

    public IAbstractDomainForEnvironments<Variable, Expression>/*!*/ TestFalse(Expression/*!*/ guard)
    {
      return this; // TODO: return throw new AbstractInterpretationException();
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression/*!*/ exp)
    {
      return new FlatAbstractDomain<bool>(true).Top; //TODO: return throw new AbstractInterpretationException();
    }

    #endregion

    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> converter)
    {
      ALog.BeginAssignInParallel(StringClosure.For("Partitions"), StringClosure.For(this));
      
      if (this.IsBottom)
        return;

      //make easier the in-place computation
      var result = new Dictionary<Variable, IPartitionAbstraction<Variable, Expression>>();
      
      var multipleAssignedVariables = new Dictionary<Variable, FList<Variable>>();

      // consider the functional mapping renamings
      foreach(var source in this.Variables)
      {
        if (!sourcesToTargets.ContainsKey(source))
        {
          continue;
        }

        var targetsList = sourcesToTargets[source];

        if (targetsList.Length() > 1)
        {
          multipleAssignedVariables.Add(targetsList.Head, targetsList.Tail); 
        }
        
        IPartitionAbstraction<Variable, Expression> value;
        if (this.TryGetValue(source, out value))
        {
          var target = targetsList.Head;

          if (result.ContainsKey(target))
          {
            throw new AbstractInterpretationException("Assign in parallel not consistent");
          }

          result[target] = (IPartitionAbstraction<Variable, Expression>)value.Clone();
        }
        else
        {
          throw new AbstractInterpretationException("elements changed during foreach");
        }
        
      }

      // consider the inside partition renaming
      foreach (var p in result)
      {
        (p.Value).AssignInParallel(sourcesToTargets, converter);
      }
      
      // handling the multiple assignements: copy
      foreach (var e in multipleAssignedVariables)
      {
        foreach (var target in e.Value.GetEnumerable())
        {
          result[target] = (IPartitionAbstraction<Variable, Expression>)result[e.Key].Clone();
        }
      }

      this.SetElements(result);

      return;
    }

    #endregion

    #region Allocations

    public SimplePartitionAbstractDomain<Variable, Expression> Allocation(Variable x, Expression len)
    {
      // every new allocation must generate a new symbolic variable
      if (this.ContainsKey(x))
      {
        throw new AbstractInterpretationException();
        //this.elements.Remove(x);
      }

      this.State = AbstractState.Normal;
      IPartitionAbstraction<Variable, Expression> allocationPartition = (IPartitionAbstraction<Variable, Expression>)onePartition.PartitionRange(encoder.ConstantFor(0),len);

      this.AddElement(x, allocationPartition);

      return this;
    }

    public SimplePartitionAbstractDomain<Variable, Expression> ArrayAssignment(Variable x, Expression index, Expression knowledge, Set<Variable> notWanted)
    {
      // a prior allocation must have taken place
      //if (!this.ContainsKey(x))
      //{
      //  throw new AbstractInterpretationException();
      //}

      this.State = AbstractState.Normal;
      var currentPartition = (IPartitionAbstraction<Variable, Expression>)(this[x]).Clone();
      var splitPartition = (IPartitionAbstraction<Variable, Expression>)onePartition.PartitionSplit(index);
      
      foreach (var e in notWanted)
      {
        splitPartition.Merge(e);
      }

      // the notWanted can make the partition TOP
      if (splitPartition.IsTop)
      {
        // not succeeded to create a suitable partition
        splitPartition = (IPartitionAbstraction<Variable, Expression>)splitPartition.Bottom;
      }
      
      var newPartition = (IPartitionAbstraction<Variable, Expression>)currentPartition.Join((IAbstractDomain)splitPartition);
 
      newPartition.Simplify(knowledge);

      this.RemoveElement(x);
      this.AddElement(x, newPartition);

      return this;
    }

    #endregion

    #region ToString

    public override string ToString()
    {
      string result;

      if (this.IsBottom)
      {
        result = "_|_";
      }
      else if (this.IsTop)
      {
        result = "Top";
      }
      else
      {
        var tempStr = new StringBuilder();

        foreach (var x in this.Keys)
        {
          var xAsString = this.decoder != null ? this.decoder.NameOf(x) : x.ToString();
          tempStr.Append(xAsString + ": " + this[x] + ", ");
        }

        result = tempStr.ToString();
        int indexOfLastComma = result.LastIndexOf(",");
        if (indexOfLastComma > 0)
        {
          result = result.Remove(indexOfLastComma);
        }
      }

      return result;
    }

    public string ToString(Expression exp)
    {
      if (this.decoder != null)
      {
        return ExpressionPrinter.ToString(exp, this.decoder);
      }
      else
      {
        return "< missing expression decoder >";
      }
    }
    
    #endregion


    public Set<Variable> arraysWithPartitionDefinedOnASubsetExpressionOf(Expression exp)
    {
      var result = new Set<Variable>();
      var variables = this.decoder.VariablesIn(exp);

      if (variables.IsEmpty)
      {
        return result;
      }

      foreach (var e in this.Elements)
      {
        if (e.Value.IsNormal())
        {
          var indexes = e.Value.Indexes();
          var intersection = indexes.Intersection(variables);

          if (!intersection.IsEmpty)
          {
            result.Add(e.Key);
          }
        }
      }

      return result;
    }

    private static void Log(string format, params object[] args)
    {
      if (Trace)
      {
        Console.WriteLine(format, args);
      }
    }

  }

  //---------------------------------------------------------------------------------------------------------------------------------------------------------------------

  public enum TouchKind { Strong, Weak }

  /// <summary>
  /// A simple factory of partition abstract domains based on an abstract domain for slices
  /// </summary>
  /// <typeparam name="Expression"></typeparam>
  /// <typeparam name="Slice"></typeparam>
  public abstract class SimplePartitionAbstractionFactory<This, Variable, Expression, SliceAbstractDomain> : 
    IPartitionAbstraction<Variable, Expression>
    where SliceAbstractDomain : INumericalAbstractDomain<Variable, Expression>
    where This : SimplePartitionAbstractionFactory<This, Variable, Expression, SliceAbstractDomain>
  {

    #region Private Variables

    protected Variable sliceRep;
    protected SliceAbstractDomain oneSlice;

    protected List<SliceAbstractDomain> content;

    protected IExpressionDecoder<Variable, Expression> decoder;
    protected IExpressionEncoder<Variable, Expression> encoder;

    #endregion

    public Variable Representative()
    {
      return this.sliceRep;
    }

    public Set<int> Dimensions()
    {
      var dims = new Set<int>();
      for (int i = 0; i < this.content.Count; i++)
      {
        dims.Add(i);
      }

      return dims;
    }

    protected bool TryDimensionOf(SliceAbstractDomain slice, out int dimension)
    {
      // TODO: indexOf ...

      Predicate<SliceAbstractDomain> sliceFinder = s => s.Equals(slice);
        
      try 
      {
        dimension = this.content.FindIndex(sliceFinder);
        return true;
      } 
      catch
      {
        dimension = default(int);
        return false;
      }
    }

    // version for inconsistent partitions
    private bool TryHarderDimensionOf(Set<int> forbidden, SliceAbstractDomain slice, out int dimension)
    {
      Predicate<SliceAbstractDomain> sliceFinder = s => s.Equals(slice);

      try
      {
        dimension = this.content.FindIndex(sliceFinder);
        while (forbidden.Contains(dimension))
        {
          dimension = this.content.FindIndex(dimension+1, sliceFinder);
        }

        return true;
      }
      catch
      {
        dimension = default(int);
        return false;
      }
    }

    public virtual Set<Pair<int,TouchKind>> DimensionsTouchedBy(Expression exp, Set<Variable> notWanted)
    {
      var result = new Set<Pair<int, TouchKind>>();
      int dimension;

      var sliceExpression = (SliceAbstractDomain)this.oneSlice.Top;
      sliceExpression = (SliceAbstractDomain)sliceExpression.TestTrue(exp);

      foreach (var e in notWanted)
      {
        sliceExpression.RemoveVariable(e);
      }

      foreach(var slice in this.content)
      {
        if (sliceExpression.LessEqual(slice))
        {
          if (TryDimensionOf(slice, out dimension))
          {
            if (slice.LessEqual(sliceExpression))
            {
              result.Add(new Pair<int, TouchKind>(dimension, TouchKind.Strong));
            }
            else
            {
              result.Add(new Pair<int, TouchKind>(dimension, TouchKind.Weak));
            }
          }
          else
          {
            throw new AbstractInterpretationException();
          }
        }
        else if (slice.LessEqual(sliceExpression))
        {
          if (TryDimensionOf(slice, out dimension))
          {
            result.Add(new Pair<int, TouchKind>(dimension, TouchKind.Strong));
          }
          else
          {
            throw new AbstractInterpretationException();
          }
        }
      }

      return result;
    }

    public Dictionary<int, Set<int>> TransformationMapFor(IPartitionAbstraction<Variable, Expression> toPartition)
    {
      var right = (This)toPartition;  // VERY BAD ASSUMPTION, the all method should use DimensionsTouchedBy(Expression exp) for each partition
      var result = new Dictionary<int, Set<int>>();

      foreach (var rightSlice in right.content) 
      {
        int dimRightSlice;

        if (right.TryDimensionOf(rightSlice, out dimRightSlice)) //toPartition.Try tototototo
        {
          // handling of non-consistent slices possible at left due to call from ContainerAnalysis.HelperForAssignmentInParallel.
          Set<int> dimLeftVisited = new Set<int>();
          
          foreach (var leftSlice in this.content)
          {
            int dimLeftSlice;

            if (this.TryHarderDimensionOf(dimLeftVisited, leftSlice, out dimLeftSlice))
            {
              dimLeftVisited.Add(dimLeftSlice);

              var leftSlice2 = (SliceAbstractDomain)leftSlice.Clone();
              var rightSlice2 = (SliceAbstractDomain)rightSlice.Clone();
              AbstractDomainsHelper.unifyVariablePacks<SliceAbstractDomain, Variable, Expression>(leftSlice2, rightSlice2);
              
              if (leftSlice2.LessEqual(rightSlice2))
              {
                if (!result.ContainsKey(dimRightSlice))
                {
                  result[dimRightSlice] = new Set<int>();
                }
                result[dimRightSlice].Add(dimLeftSlice);
              }
              else if (rightSlice2.LessEqual(leftSlice2))
              {
                result[dimRightSlice] = new Set<int>();
                result[dimRightSlice].Add(dimLeftSlice);
                break;
              }
            }
            else
            {
              throw new AbstractInterpretationException();
            }
          }
        }
        else
        {
          throw new AbstractInterpretationException();
        }
      }

      return result;
    }

    public virtual Set<int> EmptyDimensionsInContext(Expression knowledge)
    {
      var result = new Set<int>();

      var knowledgeSlice = (SliceAbstractDomain)this.oneSlice.Top.Clone();
      knowledgeSlice = (SliceAbstractDomain)knowledgeSlice.TestTrue(knowledge);

      int dimension;

      // TODO: try to avoid the call to TryDimensionOf, by iterate wrt to the list order.
      foreach (var slice in this.content)
      {
        var slice_cloned = (SliceAbstractDomain)slice.Clone();
        var tmp = (SliceAbstractDomain)slice_cloned.Meet(knowledgeSlice);

        if (!(tmp.IsBottom))
        {
          if (TryDimensionOf(slice, out dimension))
          {
            result.Add(dimension);
          }
          else
          {
            throw new AbstractInterpretationException();
          }
        }
      }

      return result;
    }

    #region Constructors

    public SimplePartitionAbstractionFactory(SliceAbstractDomain slice, Variable rep, IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder)
    {
      this.oneSlice = slice;
      this.sliceRep = rep;
      this.decoder = decoder;

      this.encoder = encoder;

      this.content = new List<SliceAbstractDomain>();

    }

    public SimplePartitionAbstractionFactory(This source)
    {
      this.oneSlice = (SliceAbstractDomain)source.oneSlice.Clone();
      this.sliceRep = source.sliceRep;
      this.decoder = source.decoder;

      this.encoder = source.encoder;

      this.content = new List<SliceAbstractDomain>(source.content.Count);
      foreach (SliceAbstractDomain x in source.content)

      {
        this.content.Add((SliceAbstractDomain)x.Clone());
      }
    }

    private void CopyFromThis(This source)
    {
      this.oneSlice = (SliceAbstractDomain)source.oneSlice;
      this.sliceRep = source.sliceRep;
      this.decoder = source.decoder;
      this.encoder = source.encoder;
      this.content = source.content;
    }

    #endregion

    #region IPartitionAbstraction<Variable, Expression> Members

    public virtual void Simplify()
    {
      this.content = this.content.FindAll((slice => !slice.IsBottom));
      
      return;
    }

    public virtual void Simplify(Expression knowledge)
    {

      var result = new List<SliceAbstractDomain>();

      foreach (var slice in this.content)
      {
        var slice_cloned = (SliceAbstractDomain)slice.Clone();
        var tmp = slice_cloned.TestTrue(knowledge);
        
        if (!(tmp.IsBottom))
        {
          result.Add(slice);
        }
      }

      this.content = result;

      return;
    }

    public virtual Set<Variable> Indexes()
    {
      if (this.content.Count == 0)
      {
        return new Set<Variable>();
      }

      Predicate<SliceAbstractDomain> alwaysTrue = x => true;
      var slice = this.content.Find(alwaysTrue);

      var result = slice.Variables; //TODO : is it optimized if I leave the slack variable?
      
      return result;
    }

    public virtual Set<Expression> Singles()
    {
      // TODO : add to abstract domain interface a method m(exp) which returns all exp' equals to exp according to the abstract element
      // you just have to have ask, for each slice, m(this.sliceRep)
      throw new NotImplementedException();
    }

    public virtual void Merge(Variable exp)
    {
      var topSlices = new Set<SliceAbstractDomain>();

      foreach (var slice in this.content)
      {
        slice.RemoveVariable(exp);

        if (slice.IsTop)
        {
          this.content = new List<SliceAbstractDomain>();

          return;
        }
      }

      //foreach (var slice in topSlices)
      //{
      //  this.content.Remove(slice);
      //}

      return;
    }

    public virtual IPartitionAbstraction<Variable, Expression> PartitionRange(Expression lb, Expression ub)
    {
      This result = this.Factory();
      var success = false;

      var sliceRepExp = this.encoder.VariableFor(this.sliceRep);

      var left = (SliceAbstractDomain)this.oneSlice.Top;
      left = (SliceAbstractDomain)left.TestTrueLessThan(sliceRepExp, lb);
      left = (SliceAbstractDomain)left.TestTrueLessEqualThan(lb, ub);

      if (!left.IsTop)
      {
        SliceAbstractDomain right = (SliceAbstractDomain)this.oneSlice.Top;
        right = (SliceAbstractDomain)right.TestTrueLessEqualThan(ub, sliceRepExp);
        right = (SliceAbstractDomain)right.TestTrueLessEqualThan(lb, ub);

        var partitionIsConsistentHelper = left.Meet(right);

        if (partitionIsConsistentHelper.IsBottom)
        {
          SliceAbstractDomain range = (SliceAbstractDomain)this.oneSlice.Top;
          range = (SliceAbstractDomain)range.TestTrueLessEqualThan(lb, sliceRepExp);
          range = (SliceAbstractDomain)range.TestTrueLessThan(sliceRepExp, ub);

          partitionIsConsistentHelper = range.Meet(right);
          var partitionIsConsistentHelper2 = range.Meet(left);

          if (partitionIsConsistentHelper.IsBottom && partitionIsConsistentHelper2.IsBottom)
          {
            result.content.Add(left);
            result.content.Add(range);
            result.content.Add(right);
            success = true;
          }
        }
      }

      if (success)
      {
        return (IPartitionAbstraction<Variable, Expression>)result;
      }
      else
      {
        // TODO : in case of an allocation, sometimes if the length cannot be represented we can try 0<l,0<l<i,0<=i=l,l>i>=0
        // if the invariants is computed in a traversal from 0 to length, it will work. If it is from length to 0, this partitioning will not be sufficient
        return (IPartitionAbstraction<Variable, Expression>)result.Bottom;
      }
    }

    public virtual IPartitionAbstraction<Variable, Expression> PartitionSplit(Expression x)
    {
      This result = this.Factory();
      var success = false;

      var sliceRepExp = this.encoder.VariableFor(this.sliceRep);

      SliceAbstractDomain left = (SliceAbstractDomain)this.oneSlice.Top;
      left = (SliceAbstractDomain)left.TestTrueLessThan(sliceRepExp, x);

      if (!left.IsTop)
      {
        SliceAbstractDomain right = (SliceAbstractDomain)this.oneSlice.Top;
        right = (SliceAbstractDomain)right.TestTrueLessThan(x, sliceRepExp);

        if ((left.Meet(right)).IsBottom)
        {
          SliceAbstractDomain single = (SliceAbstractDomain)this.oneSlice.Top;
          single = (SliceAbstractDomain)single.TestTrueLessEqualThan(x, sliceRepExp);
          single = (SliceAbstractDomain)single.TestTrueLessEqualThan(sliceRepExp, x);

          if ((single.Meet(right)).IsBottom && (single.Meet(left)).IsBottom)
          {
            result.content.Add(left);
            result.content.Add(single);
            result.content.Add(right);
            success = true;
          }
        }
      }

      if (success)
      {
        return (IPartitionAbstraction<Variable, Expression>)result;
      }
      else
      {
        return (IPartitionAbstraction<Variable, Expression>)result.Bottom;
      }
    }

    public IPartitionAbstraction<Variable, Expression> Factori()
    {
      return (IPartitionAbstraction<Variable, Expression>)Factory();
    }

    #region IAssignInParallel<Expression> Members

    public IPartitionAbstraction<Variable, Expression> PartialAssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)

    {
      var result = (This)this.Clone();

      var multipleAssignedVariables = new Dictionary<Variable, FList<Variable>>();

      foreach (var e in sourcesToTargets)
      {
        if (e.Value.Length() > 1)
        {
          multipleAssignedVariables.Add(e.Key, e.Value);
        }
      }

      // Adding the slack variable of the partition to the assignments (here because each partition can use a different slice representative)
      sourcesToTargets.Add(this.sliceRep, FList<Variable>.Cons(this.sliceRep, FList<Variable>.Empty));

      foreach (var slice in result.content)
      {
        var originalVariables = slice.Variables;

        slice.AssignInParallel(sourcesToTargets, convert);

        // removing variables introduced by the treatment of assignment to multiple variables
        foreach (var e in multipleAssignedVariables)
        {
          if (!originalVariables.Contains(e.Key))
          {
            foreach (var v in e.Value.GetEnumerable())
            {
              slice.RemoveVariable(v);
            }
          }
          else
          {
            // TODO?
          }

        }

      }

      // Removing the slack variable of the partition to the assignments
      sourcesToTargets.Remove(this.sliceRep);

      return result;
    }

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      var partition = (This)PartialAssignInParallel(sourcesToTargets, convert);

      // merge (TODO: try to find some conditions to avoid this treatment each time)
      // TRICK: in case of a merge, the partition is not consistent : some of the slices overlap.
      // so calling Meet operators with this partition as left and right operand will return a sound partition
      var finalPartition = partition.Meet(partition);
      CopyFromThis(finalPartition);      
      
      return;
    }

    #endregion

    #endregion

    #region IAbstractDomain Members

    /// <summary>
    /// A partition is bottom when it is only made of one slice which is the top element of the underlying abstract domain
    /// </summary>
    public bool IsBottom
    {
      get
      {
        var slices = this.content;

        if (slices.Count > 0)
        {
          Predicate<SliceAbstractDomain> alwaysTrue = x => true;
          var aSlice = this.content.Find(alwaysTrue);

          return (this.content.Count == 1 && aSlice.IsTop);
        }
        else
        {
          return false;
        }

      }
    }

    /// <summary>
    /// A partition is top when it does not contains any slice
    /// </summary>
    public bool IsTop
    {
      get
      {
        return (this.content.Count == 0); 
      }
    }

    /// <summary>
    /// Order
    /// </summary>
    /// 
    public bool LessEqual(IAbstractDomain a)
    {
      return this.LessEqual((This)a);
    }

    IAbstractDomain IAbstractDomain.Bottom
    {
      get
      {
        return this.Bottom;
      }
    }

    IAbstractDomain IAbstractDomain.Top
    {
      get
      {
        return this.Top;
      }
    }

    IAbstractDomain/*!*/ IAbstractDomain.Join(IAbstractDomain/*!*/ a)
    {
      return this.Join((This)a);
    }

    IAbstractDomain/*!*/ IAbstractDomain.Meet(IAbstractDomain/*!*/ a)
    {
      return this.Meet((This)a);
    }

    IAbstractDomain/*!*/ IAbstractDomain.Widening(IAbstractDomain/*!*/ prev)
    {
      return this.Join((This)prev);
    }

    public string ToRewritingRule()
    {
      return null;
    }

    public T To<T>(IFactory<T> factory)
    {
      return factory.Constant(true);
    }
    
    #endregion

    public abstract This Factory();
    
    #region Lattice elements and operators

    public This Bottom
    {
      get
      {
        This bottom = this.Factory();
        var topSlice = this.oneSlice.Top;
        bottom.content.Add((SliceAbstractDomain)topSlice.Clone());

        return bottom;
      }
    }

    public This Top
    {
      get
      {
        This top = this.Factory();
        
        return top;
      }
    }

    /// <summary>
    /// Order ...
    /// </summary>
    /// 
    public bool LessEqual(This right)
    {
      bool result;
      if (AbstractDomainsHelper.TryTrivialLessEqual(this, right, out result))
      {
        return result;
      }

      foreach(SliceAbstractDomain y in right.content)
      {
        bool contained = false;
        foreach (SliceAbstractDomain x in this.content)
        {
          if (y.LessEqual(x))
          {
            contained = true;
            break;
          }
        }
        if (!contained)
        {
          return false;
        }
      }
      return true;
    }
    
    public virtual This/*!*/ Join(This right)
    {
      SimplePartitionAbstractionFactory<This, Variable, Expression,SliceAbstractDomain> trivialResult;
      if (AbstractDomainsHelper.TryTrivialJoin(this, right, out trivialResult))
      {
        return (This)trivialResult;
      }

      This result = this.Factory();

      //Log("JOIN");

      foreach (var y in right.content)
      {
        foreach (var x in this.content)
        {
          AbstractDomainsHelper.unifyVariablePacks<SliceAbstractDomain, Variable, Expression>(x, y);

          var intersection = (SliceAbstractDomain)y.Meet(x);

          //Log("Y::{0}", y);
          //Log("X::{0}", x);
          //Log("Intersection::{0}", intersection);
          
          if (!intersection.IsBottom)
          {
            result.content.Add(intersection);
          }
        }
      }

      //Log("ENDJOIN");

      result.Simplify();

      return result;
    }

    public virtual This/*!*/ Meet(This right)
    {
      SimplePartitionAbstractionFactory<This, Variable, Expression, SliceAbstractDomain> trivialResult;
      if (AbstractDomainsHelper.TryTrivialMeet(this, right, out trivialResult))
      {
        return (This)trivialResult;
      }

      This result = this.Factory();
      
      // Construct intersection graph
      var g = new UndirectedGraph<SliceAbstractDomain, Edge<SliceAbstractDomain>>(false);

      foreach (var y in right.content)
      {
        foreach (var x in this.content)
        {
          var intersection = y.Meet(x);

          if (!intersection.IsBottom)
          {
            //var edge = new Edge<SliceAbstractDomain>(x, y);
            //g.AddVerticesAndEdge(edge);

            if (x.Equals(y))
            {
              bool xExists = false;
              var xSubstitue = x;
              foreach (var v in g.Vertices)
              {
                if (v.Equals(x))
                {
                  xExists = true;
                  xSubstitue = v;
                }
              }
              if (!xExists)
              {
                g.AddVertex(x);
              }

              g.AddEdge(new Edge<SliceAbstractDomain>(xSubstitue, xSubstitue));
            }
            else
            {
              bool xExists = false;
              var xSubstitue = x;
              bool yExists = false;
              var ySubstitue = y;
              foreach (var v in g.Vertices)
              {
                if (v.Equals(x))
                {
                  xExists = true;
                  xSubstitue = v;
                }
                if (v.Equals(y))
                {
                  yExists = true;
                  ySubstitue = v;
                }
              }
              if (!xExists)
              {
                g.AddVertex(x);
              }
              if (!yExists)
              {
                g.AddVertex(y);
              }

              g.AddEdge(new Edge<SliceAbstractDomain>(xSubstitue, ySubstitue));
            }
          }
        }
      }

      // Compute connected components
      var cc = new ConnectedComponentsAlgorithm<SliceAbstractDomain, Edge<SliceAbstractDomain>>(g);
      cc.Compute();

      // The result, of the form Vertex -> int, is transformed into the form int -> Set(vertex)
      SliceAbstractDomain[] cc_array = new SliceAbstractDomain[cc.ComponentCount];

      foreach(int k in cc.Components.Values)
      {
        cc_array[k] = (SliceAbstractDomain)this.oneSlice.Bottom;
      }
      
      foreach (KeyValuePair<SliceAbstractDomain,int> e in cc.Components)
      {
        var aggregate = cc_array[e.Value].Join(e.Key);
        cc_array[e.Value] = (SliceAbstractDomain)aggregate;
      }

      foreach (SliceAbstractDomain newSlice in cc_array)
      {
        result.content.Add(newSlice);
      }

      return result;
    }

    #endregion

    #region ICloneable Members

    public abstract object/*!*/ Clone();

    #endregion

    public override string ToString()
    {
      string result;

      if (this.IsBottom)
      {
        result = "(bottom partition)";
      }
      else if (this.IsTop)
      {
        result = "(top partition)";
      }
      else
      {
        var tempStr = new StringBuilder();

        tempStr.AppendFormat(" ({0} partions) {1}", this.content.Count, Environment.NewLine);

        var i = 0;

        foreach (var partition in this.content)
        {
          var p = MakeItPrettier("  ", partition.ToString());
          tempStr.AppendFormat("  Partition {0}: {1}", i++, p);
        }
        
        result = tempStr.ToString();
      }

      return result;
    }

    private string MakeItPrettier(string prefix, string d)
    {
      var split = d.Split('\n', '\r');
      var result = new StringBuilder();
      foreach (string s in split)
      {
        if(!string.IsNullOrEmpty(s))
          result.AppendLine(prefix + s);
      }

      return result.ToString();
    }

  }

  // ------------------------------------------------------------------------------------------------------------------------------------------------

  public class SimplePartitionAbstractionSubPolyhedraBased<Variable, Expression> :
    SimplePartitionAbstractionFactory<SimplePartitionAbstractionSubPolyhedraBased<Variable, Expression>, Variable, Expression, SubPolyhedra<Variable, Expression>>
  {

    #region Constructors
    
    public SimplePartitionAbstractionSubPolyhedraBased(IExpressionEncoder<Variable, Expression> encoder, IExpressionDecoder<Variable, Expression> decoder) :
      base(
        new SubPolyhedra<Variable, Expression>(new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(decoder, encoder), new IntervalEnvironment<Variable, Expression>(decoder), decoder, encoder),
           encoder.FreshVariable<Expression>(), decoder, encoder)
    {
    }

    public SimplePartitionAbstractionSubPolyhedraBased(SimplePartitionAbstractionSubPolyhedraBased<Variable, Expression> source)
      : base(source) 
    {
    }

    public SimplePartitionAbstractionSubPolyhedraBased(SubPolyhedra<Variable, Expression> slice, Variable rep, IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder)
      : base(slice, rep, decoder, encoder)
    {
    }
    
    #endregion


    public override SimplePartitionAbstractionSubPolyhedraBased<Variable, Expression> Factory()
    {
      return new SimplePartitionAbstractionSubPolyhedraBased<Variable, Expression>(this.oneSlice, this.sliceRep, this.decoder, this.encoder);
    }

    #region ICloneable Members

    public override object Clone()
    {
      return new SimplePartitionAbstractionSubPolyhedraBased<Variable, Expression>(this);
    }
    
    #endregion
  }

  // ------------------------------------------------------------------------------------------------------------------------------------------------

  public class SimplePartitionAbstractionOctagonBased<Variable, Expression> :
    SimplePartitionAbstractionFactory<SimplePartitionAbstractionOctagonBased<Variable, Expression>, Variable, Expression, OctagonEnvironment<Variable, Expression>>
  {

    #region Constructors

    public SimplePartitionAbstractionOctagonBased(IExpressionEncoder<Variable, Expression> encoder, IExpressionDecoder<Variable, Expression> decoder) :
      base(new OctagonEnvironment<Variable, Expression>(decoder, encoder, OctagonEnvironment<Variable, Expression>.OctagonPrecision.FullPrecision),
           encoder.FreshVariable<Expression>(),decoder, encoder)
    {
    }

    public SimplePartitionAbstractionOctagonBased(SimplePartitionAbstractionOctagonBased<Variable, Expression> source)
      : base(source)
    {
    }

    public SimplePartitionAbstractionOctagonBased(OctagonEnvironment<Variable, Expression> slice, Variable rep, IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder)
      : base(slice, rep, decoder, encoder)
    {
    }

    #endregion

    public override void Simplify()
    {
      System.Action<OctagonEnvironment<Variable, Expression>> reduce = slice => slice.DoClosure();
      this.content.ForEach(reduce);

      base.Simplify();
      return;
    }

    public override void Simplify(Expression knowledge)
    {

      Console.WriteLine("SIMPLIFY::{0}", knowledge);

      var result = new List<OctagonEnvironment<Variable, Expression>>();


      var knowledgeSlice = (OctagonEnvironment<Variable, Expression>)this.oneSlice.Top.Clone();
      knowledgeSlice = (OctagonEnvironment<Variable, Expression>)knowledgeSlice.TestTrue(knowledge);
      knowledgeSlice = (OctagonEnvironment<Variable, Expression>)knowledgeSlice.TestTrueNotRefining(knowledge);

      foreach (var slice in this.content)
      {
        var slice_cloned = slice.Duplicate();
        //var tmp = ((OctagonEnvironment<Variable, Expression>)slice_cloned.TestTrue(knowledge)); //TODO !!!!!! make a slice representing knowledge one time, and call meet here.
        var tmp = (OctagonEnvironment<Variable, Expression>)slice_cloned.Meet(knowledgeSlice); // done
        tmp.DoClosure();

        if (!(tmp.IsBottom))
        {
          result.Add(slice);
        }
      }

      this.content = result;
  
      return;
    }

    public override Set<int> EmptyDimensionsInContext(Expression knowledge)
    {
      var result = new Set<int>();

      var knowledgeSlice = (OctagonEnvironment<Variable, Expression>)this.oneSlice.Top.Clone();
      knowledgeSlice = (OctagonEnvironment<Variable, Expression>)knowledgeSlice.TestTrue(knowledge);
      knowledgeSlice = (OctagonEnvironment<Variable, Expression>)knowledgeSlice.TestTrueNotRefining(knowledge);

      int dimension;

      // TODO: try to avoid the call to TryDimensionOf, by iterate wrt to the list order.
      foreach (var slice in this.content)
      {
        var slice_cloned = slice.Duplicate();
        var tmp = (OctagonEnvironment<Variable, Expression>)slice_cloned.Meet(knowledgeSlice);
        tmp.DoClosure();

        if (tmp.IsBottom)
        {
          if (TryDimensionOf(slice, out dimension))
          {
            result.Add(dimension);
          }
          else
          {
            throw new AbstractInterpretationException();
          }
        }
      }

      return result;
    }

    public override Set<Expression> Singles()
    {
      var result = new Set<Expression>();

      foreach (var slice in this.content)
      {
        var equalsToSliceRep = slice.EqualsTo(this.encoder.VariableFor(this.sliceRep));

        //if (!equalsToSliceRep.IsEmpty)
        //{
          result = result.Union(equalsToSliceRep);
        //}
      }

      return result;
    }


    // TODO: implement a CLOSED OctagonEnvironment<Variable, Expression> class! to avoid the override
    public override SimplePartitionAbstractionOctagonBased<Variable, Expression> Meet(SimplePartitionAbstractionOctagonBased<Variable, Expression> right)
    {
      SimplePartitionAbstractionOctagonBased<Variable, Expression> trivialResult;
      if (AbstractDomainsHelper.TryTrivialMeet(this, right, out trivialResult))
      {
        return trivialResult;
      }

      var result = this.Factory();

      // Construct intersection graph
      var g = new UndirectedGraph<OctagonEnvironment<Variable, Expression>, Edge<OctagonEnvironment<Variable, Expression>>>(false);

      foreach (var y in right.content)
      {
        foreach (var x in this.content)
        {
          var intersection = y.Meet(x);
          intersection.DoClosure();

          if (!intersection.IsBottom)
          {

            //var edge = new Edge<SliceAbstractDomain>(x, y);
            //g.AddVerticesAndEdge(edge);

            if (x.Equals(y))
            {
              bool xExists = false;
              var xSubstitue = x;
              foreach (var v in g.Vertices)
              {
                if (v.Equals(x))
                {
                  xExists = true;
                  xSubstitue = v;
                }
              }
              if (!xExists)
              {
                g.AddVertex(x);
              }

              g.AddEdge(new Edge<OctagonEnvironment<Variable, Expression>>(xSubstitue, xSubstitue));
            }
            else
            {
              bool xExists = false;
              var xSubstitue = x;
              bool yExists = false;
              var ySubstitue = y;
              foreach (var v in g.Vertices)
              {
                if (v.Equals(x))
                {
                  xExists = true;
                  xSubstitue = v;
                }
                if (v.Equals(y))
                {
                  yExists = true;
                  ySubstitue = v;
                }
              }
              if (!xExists)
              {
                g.AddVertex(x);
              }
              if (!yExists)
              {
                g.AddVertex(y);
              }

              g.AddEdge(new Edge<OctagonEnvironment<Variable, Expression>>(xSubstitue, ySubstitue));
            }
          }
        }
      }

      // Compute connected components
      var cc = new ConnectedComponentsAlgorithm<OctagonEnvironment<Variable, Expression>, Edge<OctagonEnvironment<Variable, Expression>>>(g);
      cc.Compute();

      // The result, of the form Vertex -> int, is transformed into the form int -> Set(vertex)
      OctagonEnvironment<Variable, Expression>[] cc_array = new OctagonEnvironment<Variable, Expression>[cc.ComponentCount];

      foreach (int k in cc.Components.Values)
      {
        cc_array[k] = (OctagonEnvironment<Variable, Expression>)this.oneSlice.Bottom;
      }

      foreach (KeyValuePair<OctagonEnvironment<Variable, Expression>, int> e in cc.Components)
      {
        var aggregate = cc_array[e.Value].Join(e.Key);
        cc_array[e.Value] = (OctagonEnvironment<Variable, Expression>)aggregate;
      }

      foreach (var newSlice in cc_array)
      {
        result.content.Add(newSlice);
      }

      return result;
    }

    // TODO: implement a CLOSED OctagonEnvironment<Variable, Expression> class! to avoid the override
    public override IPartitionAbstraction<Variable, Expression> PartitionRange(Expression lb, Expression ub)
    {
      var result = this.Factory();
      var success = false;
      //t-maper@527 TODO this.decoder.Isconstant or IsVariable ...

      var sliceRepExp = this.encoder.VariableFor(this.sliceRep);

      var left = (OctagonEnvironment<Variable, Expression>)this.oneSlice.Top;
      left = (OctagonEnvironment<Variable, Expression>)left.TestTrueLessThan(sliceRepExp, lb);
      left = (OctagonEnvironment<Variable, Expression>)left.TestTrueLessEqualThan(lb, ub);
      //left.DoClosure();

      if (!left.IsTop)
      {
        var right = (OctagonEnvironment<Variable, Expression>)this.oneSlice.Top;
        right = (OctagonEnvironment<Variable, Expression>)right.TestTrueLessEqualThan(ub, sliceRepExp);
        right = (OctagonEnvironment<Variable, Expression>)right.TestTrueLessEqualThan(lb, ub);
        //right.DoClosure();

        var partitionIsConsistentHelper = left.Meet(right);
        partitionIsConsistentHelper.DoClosure();

        if (partitionIsConsistentHelper.IsBottom)
        {
          var range = (OctagonEnvironment<Variable, Expression>)this.oneSlice.Top;
          range = (OctagonEnvironment<Variable, Expression>)range.TestTrueLessEqualThan(lb, sliceRepExp);
          range = (OctagonEnvironment<Variable, Expression>)range.TestTrueLessThan(sliceRepExp, ub);

          partitionIsConsistentHelper = range.Meet(right);
          partitionIsConsistentHelper.DoClosure();

          var partitionIsConsistentHelper2 = range.Meet(left);
          partitionIsConsistentHelper2.DoClosure();

          if (partitionIsConsistentHelper.IsBottom && partitionIsConsistentHelper2.IsBottom)
          {
            result.content.Add(left);
            result.content.Add(range);
            result.content.Add(right);
            success = true;
          }
        }
      }

      if (success)
      {
        return (IPartitionAbstraction<Variable, Expression>)result;
      }
      else
      {
        return (IPartitionAbstraction<Variable, Expression>)result.Bottom;
      }
    }

    // TODO: implement a CLOSED OctagonEnvironment<Variable, Expression> class! to avoid the override
    public override IPartitionAbstraction<Variable, Expression> PartitionSplit(Expression x)
    {
      var result = this.Factory();
      var success = false;

      var sliceRepExp = this.encoder.VariableFor(this.sliceRep);

      var left = (OctagonEnvironment<Variable, Expression>)this.oneSlice.Top;
      left = (OctagonEnvironment<Variable, Expression>)left.TestTrueLessThan(sliceRepExp, x);
      //left.DoClosure();

      if (!left.IsTop)
      {
        var right = (OctagonEnvironment<Variable, Expression>)this.oneSlice.Top;
        right = (OctagonEnvironment<Variable, Expression>)right.TestTrueLessThan(x, sliceRepExp);
        //right.DoClosure();

        var partitionIsConsistentHelper = left.Meet(right);
        partitionIsConsistentHelper.DoClosure();

        if (partitionIsConsistentHelper.IsBottom)
        {
          var single = (OctagonEnvironment<Variable, Expression>)this.oneSlice.Top;
          single = (OctagonEnvironment<Variable, Expression>)single.TestTrueLessEqualThan(x, sliceRepExp);
          single = (OctagonEnvironment<Variable, Expression>)single.TestTrueLessEqualThan(sliceRepExp, x);
          //single.DoClosure();

          partitionIsConsistentHelper = single.Meet(right);
          partitionIsConsistentHelper.DoClosure();

          var partitionIsConsistentHelper2 = single.Meet(left);
          partitionIsConsistentHelper2.DoClosure();

          if (partitionIsConsistentHelper.IsBottom && partitionIsConsistentHelper2.IsBottom)
          {
            result.content.Add(left);
            result.content.Add(single);
            result.content.Add(right);
            success = true;
          }
        }
      }

      if (success)
      {
        return (IPartitionAbstraction<Variable, Expression>)result;
      }
      else
      {
        return (IPartitionAbstraction<Variable, Expression>)result.Bottom;
      }
    }

    public override Set<Pair<int, TouchKind>> DimensionsTouchedBy(Expression exp, Set<Variable> notWanted)
    {
      var result = new Set<Pair<int, TouchKind>>();
      int dimension;

      var sliceExpression = (OctagonEnvironment<Variable, Expression>)this.oneSlice.Top;
      sliceExpression = (OctagonEnvironment<Variable, Expression>)sliceExpression.TestTrue(exp);
      sliceExpression.DoClosure();

      foreach (var e in notWanted)
      {
        sliceExpression.RemoveVariable(e);
      }

      foreach (var slice in this.content)
      {
        if (sliceExpression.LessEqual(slice))
        {
          if (TryDimensionOf(slice, out dimension))
          {
            if (slice.LessEqual(sliceExpression))
            {
              result.Add(new Pair<int, TouchKind>(dimension, TouchKind.Strong));
            }
            else
            {
              result.Add(new Pair<int, TouchKind>(dimension, TouchKind.Weak));
            }
          }
          else
          {
            throw new AbstractInterpretationException();
          }
        }
        else if (slice.LessEqual(sliceExpression))
        {
          if (TryDimensionOf(slice, out dimension))
          {
            result.Add(new Pair<int, TouchKind>(dimension, TouchKind.Strong));
          }
          else
          {
            throw new AbstractInterpretationException();
          }
        }
      }

      return result;
    }


    public override SimplePartitionAbstractionOctagonBased<Variable, Expression> Factory()
    {
      return new SimplePartitionAbstractionOctagonBased<Variable, Expression>(this.oneSlice, this.sliceRep, this.decoder, this.encoder);
    }

    #region ICloneable Members

    public override object Clone()
    {
      return new SimplePartitionAbstractionOctagonBased<Variable, Expression>(this);
    }

    #endregion
  }

}
#endif