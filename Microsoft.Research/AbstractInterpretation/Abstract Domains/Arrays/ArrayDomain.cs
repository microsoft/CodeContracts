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
using System.Collections.Generic;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains
{
  public interface IArrayAbstraction<Variable, Expression> : IAbstractDomain, IAssignInParallel<Variable, Expression>
  {
    IArrayAbstraction<Variable, Expression> ArrayUnknown(Set<int> dimensions, ExpressionType type);
    IArrayAbstraction<Variable, Expression> TestTrue(int dimension, Expression constraint);
    IArrayAbstraction<Variable, Expression> Transform(Dictionary<int, Set<int>> transformationMap);
    IArrayAbstraction<Variable, Expression> Assign(int dimension, TouchKind kind, Variable dest, Variable value);
    //IArrayAbstraction<Variable, Expression> Simplify(Expression exp);
  }

  public class FakeAbstractDomain<Variable, Expression> 
    : FlatAbstractDomain<Expression>, IAbstractDomainForEnvironments<Variable, Expression>
  {

    private IExpressionDecoder<Variable, Expression> decoder;

    public FakeAbstractDomain(IExpressionDecoder<Variable, Expression> decoder)
      : base (State.Top)
    {
      this.decoder = decoder;
    }

    public FakeAbstractDomain(FakeAbstractDomain<Variable, Expression> source)
    {
      this.decoder = source.decoder;
      this.state = source.state;
    }

    public override object Clone()
    {
      return new FakeAbstractDomain<Variable, Expression>(this);
    }

    #region IAbstractDomainForEnvironments<Variable, Expression> Members

    public string ToString(Expression exp)
    {
      return "FAKE";
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public Set<Variable> Variables
    {
      get {
        return new Set<Variable>();
      }
    }

    public void AddVariable(Variable var)
    {
      return;
    }

    public void Assign(Expression x, Expression exp)
    {
      return;
    }

    public void ProjectVariable(Variable var)
    {
      return;
    }

    public void RemoveVariable(Variable var)
    {
      return;
    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {
      return;
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    public IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {
      // TODO: if guard is unsat
      
      if(this.decoder.IsConstant(guard) && (this.decoder.TypeOf(guard) == ExpressionType.Bool))
      {
        var b = (Boolean)this.decoder.Constant(guard);
        if (!b)
        {
          this.state = State.Bottom;
        }
      }

      return this;
    }

    public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      return this;
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      return new FlatAbstractDomain<bool>(true).Top;
    }

    #endregion

    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> converter)
    {
      return;
    }

    #endregion
  }
  
  public class SimpleArrayPropertiesAbstractDomain<Variable, Expression> :
    FunctionalAbstractDomain<SimpleArrayPropertiesAbstractDomain<Variable, Expression>, Variable, IArrayAbstraction<Variable, Expression>>,
    IAbstractDomainForEnvironments<Variable, Expression>
  {
    #region Private State

    IArrayAbstraction<Variable, Expression> arrayAbstractor;
    IExpressionDecoder<Variable, Expression> decoder;
    IExpressionEncoder<Variable, Expression> encoder;

    #endregion

    #region Constructors

    public SimpleArrayPropertiesAbstractDomain(IExpressionEncoder<Variable, Expression> encoder, IExpressionDecoder<Variable, Expression> decoder)
      : base()
    {
      this.arrayAbstractor = new SimpleArrayAbstraction<Variable, Expression>(encoder, decoder);
      this.encoder = encoder;
      this.decoder = decoder;
    }

    public SimpleArrayPropertiesAbstractDomain(SimpleArrayPropertiesAbstractDomain<Variable, Expression> source)
      : base(source)
    {
      this.arrayAbstractor = source.arrayAbstractor;
      this.encoder = source.encoder;
      this.decoder = source.decoder;
    }

    #endregion

    #region From FunctionalAbstractDomain

    public override object Clone()
    {
      return new SimpleArrayPropertiesAbstractDomain<Variable, Expression>(this);
    }

    protected override SimpleArrayPropertiesAbstractDomain<Variable, Expression> Factory()
    {
      return new SimpleArrayPropertiesAbstractDomain<Variable, Expression>(this.encoder, this.decoder);
    }

    protected override string ToLogicalFormula(Variable d, IArrayAbstraction<Variable, Expression> c)
    {
      // TODO
      return null;
    }

    protected override T To<T>(Variable d, IArrayAbstraction<Variable, Expression> c, IFactory<T> factory)
    {
      // TODO
      return factory.Constant(true);
    }

    public override SimpleArrayPropertiesAbstractDomain<Variable, Expression> Join(SimpleArrayPropertiesAbstractDomain<Variable, Expression> right)
    {
      //// Here we do not have trivial joins as we want to join maps of different cardinality
      //if (this.IsBottom)
      //  return right;
      //if (right.IsBottom)
      //  return (SimpleArrayPropertiesAbstractDomain<Variable, Expression>)this;

      //SimpleArrayPropertiesAbstractDomain<Variable, Expression> result = this.Factory();

      //foreach (var x in this.Keys)       // For all the elements in the intersection do the point-wise join
      //{
      //  IArrayAbstraction<Variable, Expression> right_x;

      //  if (right.TryGetValue(x, out right_x))
      //  {
      //    var join = this[x].Join(right_x);

      //    //if (!join.IsTop)
      //    { // We keep in the map only the elements that are != top
      //      //result[x] = (Codomain)join;
      //      result.AddElement(x, (IArrayAbstraction<Variable, Expression>)join);
      //    }
      //  }
      //}

      //return result;
      return this.Join(right, new Set<Variable>());
    }

    public SimpleArrayPropertiesAbstractDomain<Variable, Expression> Join(SimpleArrayPropertiesAbstractDomain<Variable, Expression> right, Set<Variable> keep)
    {
      // Here we do not have trivial joins as we want to join maps of different cardinality
      if (this.IsBottom)
        return right;
      if (right.IsBottom)
        return this;

      var result = this.Factory();

      foreach (var x in this.Keys)       // For all the elements in the intersection do the point-wise join
      {
        IArrayAbstraction<Variable, Expression> right_x;

        if (right.TryGetValue(x, out right_x))
        {
          var join = this[x].Join(right_x);

          //if (!join.IsTop)
          { // We keep in the map only the elements that are != top
            //result[x] = (Codomain)join;
            result.AddElement(x, (IArrayAbstraction<Variable, Expression>)join);
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
      // do nothing // TODO
    }

    public void ProjectVariable(Variable var)
    {
      // TODO: project also in other arrays the corresponding slice variables
      this.RemoveVariable(var);
    }

    public void RemoveVariable(Variable var)
    {
      // TODO: see above
      this.RemoveElement(var);
    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {
      // TODO: see above
      this[NewName] = this[OldName];
      this.RemoveVariable(OldName);
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    public IAbstractDomainForEnvironments<Variable, Expression>/*!*/ TestTrue(Expression/*!*/ guard)
    {
      return this; //TODO!!
    }

    public IAbstractDomainForEnvironments<Variable, Expression>/*!*/ TestFalse(Expression/*!*/ guard)
    {
      return this; //TODO!!
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression/*!*/ exp)
    {
      return new FlatAbstractDomain<bool>(true).Top; //TODO!!
    }

    #endregion

    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> converter)
    {
      // TODO : this code can be factored with the one in SimplePartitionAbstractDomain.AssignInParallel
      if (this.IsBottom)
      {
        return;
      }

      //make easier the in-place computation
      var result = new Dictionary<Variable, IArrayAbstraction<Variable, Expression>>();

      var multipleAssignedVariables = new Dictionary<Variable, FList<Variable>>();

      // consider the functional mapping renamings
      foreach (var source in this.Variables)
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

        IArrayAbstraction<Variable, Expression> value;
        if (this.TryGetValue(source, out value))
        {
          var target = targetsList.Head;

          if (result.ContainsKey(target))
          {
            throw new AbstractInterpretationException("Assign in parallel not consistent");
          }

          result[target] = (IArrayAbstraction<Variable, Expression>)value.Clone();
        }
        else
        {
          throw new AbstractInterpretationException("elements changed during foreach");
        }

      }

      // consider the inside array properties renaming
      foreach (var p in result)
      {
        (p.Value).AssignInParallel(sourcesToTargets, converter);
      }

      // handling the multiple assignements: copy
      foreach (var e in multipleAssignedVariables)
      {
        foreach (var target in e.Value.GetEnumerable())
        {
          result[target] = (IArrayAbstraction<Variable, Expression>)result[e.Key].Clone();
        }
      }

      this.SetElements(result);

      return;
    }

    #endregion

    #region IAbstractDomainForEnvironments

    public string ToString(Expression exp)
    {
      //if (this.decoder != null)
      //{
      //  return ExpressionPrinter.ToString(exp, this.decoder);
      //}
      //else
      //{
      //  return "< missing expression decoder >";
      //}
      throw new NotImplementedException();
    }

    #endregion

    public void TransformArray(Variable array, Dictionary<int, Set<int>> transformationMap)
    {
      if (!ContainsKey(array))
      {
        throw new AbstractInterpretationException();
      }

      var arrayAbstraction = this[array];

      arrayAbstraction = arrayAbstraction.Transform(transformationMap);

      this[array] = arrayAbstraction;

      return;
    }

    public void SimplifyArray(Variable array, Set<int> emptyDimensions)
    {
      if (!ContainsKey(array))
      {
        throw new AbstractInterpretationException();
      }

      var arrayAbstraction = this[array];

      var falseConstraint = this.encoder.ConstantFor(false);
      //var falseConstraint = this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, this.encoder.ConstantFor(0), this.encoder.ConstantFor(1));

      foreach (var dimension in emptyDimensions)
      {
        arrayAbstraction = arrayAbstraction.TestTrue(dimension, falseConstraint);
      }

      this[array] = arrayAbstraction;

      return;
    }

    public void CreateArray(Variable array, Set<int> dimensions, int bodyDimension, Expression defaultValue)
    {
      if (ContainsKey(array))
      {
        throw new AbstractInterpretationException();
      }

      IArrayAbstraction<Variable, Expression> arrayAbstraction;

      if (defaultValue != null)
      {
        arrayAbstraction = this.arrayAbstractor.ArrayUnknown(dimensions, this.decoder.TypeOf(defaultValue));
        var bodyConstraint = this.encoder.CompoundExpressionFor(ExpressionType.Bool, 
          ExpressionOperator.Equal, this.encoder.VariableFor(array), defaultValue);

        arrayAbstraction = arrayAbstraction.TestTrue(bodyDimension, bodyConstraint);
      }
      else
      {
        arrayAbstraction = this.arrayAbstractor.ArrayUnknown(dimensions, ExpressionType.Unknown);
      }

      var falseConstraint = this.encoder.ConstantFor(false);
      //var falseConstraint = this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, this.encoder.ConstantFor(0), this.encoder.ConstantFor(1));
      
      var b = dimensions.Remove(bodyDimension);
      foreach (var dim in dimensions)
      {
          arrayAbstraction = arrayAbstraction.TestTrue(dim, falseConstraint);
          //var WORKAROUNDTOMAKEEMPTYOCTAGONSBOTTOMOCTAGONS = arrayAbstraction.ToString();
      }

      this.AddElement(array, arrayAbstraction);

      return;
    }

    public void HandleArrayAssignment(Variable array, Set<Pair<int,TouchKind>> touchedDimensions, Variable value)
    {
      if (!ContainsKey(array))
      {
        throw new AbstractInterpretationException();
      }

      foreach (var e in touchedDimensions)
      {
        this[array].Assign(e.One, e.Two, array, value);
      }

      return;
    }
  }

    //public class SimpleArrayAbstractDomain<Variable, Expression> : //t-maper@56: ,AbstractPartitionDomain<Expression>
    //ReducedCartesianAbstractDomain<INumericalAbstractDomain<Variable, Expression>, AbstractPartitionDomain>,
    //ReducedCartesianAbstractDomain<INumericalAbstractDomain<Variable, Expression>, FunctionalAbstractDomain<,Expression,<FunctionalAbstractDomain<AbstractPartitionDomain, IAbstractDomain>>>,
    //IAbstractDomain //t-maper@54: Do I need to introduce a particular type of abstract domain?IArrayAbstraction<IPartitionAbstractDomain, Expression>
  public class SimpleArrayAbstractDomain<Variable, Expression> :
    ReducedCartesianAbstractDomain<INumericalAbstractDomain<Variable, Expression>, SimpleArrayPropertiesAbstractDomain<Variable, Expression>>,
    IAbstractDomainForEnvironments<Variable, Expression>
  {
    #region Private State

    IExpressionDecoder<Variable, Expression>/*!*/ decoder;
    IExpressionEncoder<Variable, Expression>/*!*/ encoder;

    #endregion

    #region Constructors

    public SimpleArrayAbstractDomain(INumericalAbstractDomain<Variable, Expression> indexes, SimpleArrayPropertiesAbstractDomain<Variable, Expression> contents)
      : base(indexes, contents)
    {
    }

    public SimpleArrayAbstractDomain(INumericalAbstractDomain<Variable, Expression> indexes, SimpleArrayPropertiesAbstractDomain<Variable, Expression> contents, IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder)
      : base(indexes, contents)
    {
      this.encoder = encoder;
      this.decoder = decoder;
    }

    #endregion

    #region ReducedCartesianAbstractDomain

    protected override ReducedCartesianAbstractDomain<INumericalAbstractDomain<Variable, Expression>, SimpleArrayPropertiesAbstractDomain<Variable, Expression>> Factory(INumericalAbstractDomain<Variable, Expression> left, SimpleArrayPropertiesAbstractDomain<Variable, Expression> right)
    {
      // TODO
      return new SimpleArrayAbstractDomain<Variable, Expression>(left, right, this.decoder, this.encoder);
    }

    public override ReducedCartesianAbstractDomain<INumericalAbstractDomain<Variable, Expression>, SimpleArrayPropertiesAbstractDomain<Variable, Expression>> Reduce(INumericalAbstractDomain<Variable, Expression> left, SimpleArrayPropertiesAbstractDomain<Variable, Expression> right)
    {

      if (left.IsBottom)
      {
        return (SimpleArrayAbstractDomain<Variable, Expression>)this.Bottom;
      }

      return new SimpleArrayAbstractDomain<Variable, Expression>(left, right, this.decoder, this.encoder);
    }

    public override IAbstractDomain Widening(IAbstractDomain prev)
    {
      var previous = (SimpleArrayAbstractDomain<Variable, Expression>)prev;
      var leftWidened = (INumericalAbstractDomain<Variable, Expression>)this.Left.Widening(previous.Left);
      var rightWidened = (SimpleArrayPropertiesAbstractDomain<Variable, Expression>)this.Right.Widening(previous.Right);

      var widened = new SimpleArrayAbstractDomain<Variable, Expression>(leftWidened, rightWidened);

      return (IAbstractDomain)widened;
    }

    #endregion

    #region IPureExpressionAssignmentsWithForward

    public void Assign(Expression x, Expression exp)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IPureExpressionAssignments

    public Set<Variable> Variables
    {
      get
      {
        var leftVariables = new Set<Variable>(this.Left.Variables);

        return leftVariables.Union(this.Right.Variables);
      }
    }

    public void AddVariable(Variable var)
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
      // F: Why it is a nop????
    }

    #endregion

    #region IPureExpressionTest

    public IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {
      var leftTested = (INumericalAbstractDomain<Variable, Expression>)this.Left.TestTrue(guard);
      var rightTested = (SimpleArrayPropertiesAbstractDomain<Variable, Expression>)this.Right.TestTrue(guard);
      return (IAbstractDomainForEnvironments<Variable, Expression>)this.Reduce(leftTested, rightTested);
    }

    public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      var leftTested = (INumericalAbstractDomain<Variable, Expression>)this.Left.TestFalse(guard);
      var rightTested = (SimpleArrayPropertiesAbstractDomain<Variable, Expression>)this.Right.TestFalse(guard);
      return (IAbstractDomainForEnvironments<Variable, Expression>)this.Reduce(leftTested, rightTested);
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      return new FlatAbstractDomain<bool>(true).Top;
      //throw new NotImplementedException();
    }

    #endregion

    public SimpleArrayAbstractDomain<Variable, Expression> Join(SimpleArrayAbstractDomain<Variable, Expression> a, Set<Variable> keep)
    {
      SimpleArrayAbstractDomain<Variable, Expression> result;
      if (AbstractDomainsHelper.TryTrivialJoin(this, a, out result))
      {
        return result;
      }

      var joinLeftPart = (INumericalAbstractDomain<Variable, Expression>)this.Left.Join(a.Left);
      var joinRightPart = this.Right.Join(a.Right, keep);

      return (SimpleArrayAbstractDomain<Variable, Expression>)this.Reduce(joinLeftPart, joinRightPart);
    }

    #region IAssignInParallel

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      // NOTE : not called

      if (this.IsBottom)
        return;

      this.Left.AssignInParallel(sourcesToTargets, convert);
      this.Right.AssignInParallel(sourcesToTargets, convert);

      return;
    }

    #endregion

    #region ToString

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
  }

  public class SimpleArrayAbstraction<Variable, Expression> :
    IArrayAbstraction<Variable, Expression>
  {
    #region Private Variables

    protected Dictionary<int, IAbstractDomainForEnvironments<Variable, Expression>> content;
    protected IExpressionEncoder<Variable, Expression> encoder;
    protected IExpressionDecoder<Variable, Expression> decoder;

    #endregion

    #region Constructors

    public SimpleArrayAbstraction(IExpressionEncoder<Variable, Expression> encoder, IExpressionDecoder<Variable, Expression> decoder)
    {
      this.encoder = encoder;
      this.decoder = decoder;
      this.content = new Dictionary<int, IAbstractDomainForEnvironments<Variable, Expression>>();
    }

    public SimpleArrayAbstraction(SimpleArrayAbstraction<Variable, Expression> source)
    {
      this.encoder = source.encoder;
      this.decoder = source.decoder;
      this.content = new Dictionary<int, IAbstractDomainForEnvironments<Variable, Expression>>(source.content);
    }

    #endregion

    public IArrayAbstraction<Variable, Expression> ArrayUnknown(Set<int> dimensions, ExpressionType type)
    {
      var result = new SimpleArrayAbstraction<Variable, Expression>(this.encoder,this.decoder);
      IAbstractDomainForEnvironments<Variable, Expression> topProperty;

      switch (type)
      {
        case ExpressionType.Int8:
        case ExpressionType.Int16:
        case ExpressionType.Int32:
        case ExpressionType.Int64:
        case ExpressionType.UInt8:
        case ExpressionType.UInt16:
        case ExpressionType.UInt32:
          topProperty = new OctagonEnvironment<Variable, Expression>(this.decoder, this.encoder, OctagonEnvironment<Variable, Expression>.OctagonPrecision.FullPrecision);
          break;
        default:
          topProperty = new FakeAbstractDomain<Variable, Expression>(this.decoder);
          break;
      }

      foreach (var dim in dimensions)
      {
        result.content.Add(dim, (IAbstractDomainForEnvironments<Variable, Expression>)topProperty.Clone());
      }

      return (IArrayAbstraction<Variable, Expression>)result;
    }

    public IArrayAbstraction<Variable, Expression> TestTrue(int dimension, Expression constraint)
    {
      //var result = new SimpleArrayAbstraction<Variable, Expression>(this);

      this.content[dimension] = this.content[dimension].TestTrue(constraint);

      return this;
    }

    public IArrayAbstraction<Variable, Expression> Transform(Dictionary<int, Set<int>> transformationMap)
    {
      var result = new SimpleArrayAbstraction<Variable, Expression>(this.encoder, this.decoder);

      foreach (var e in transformationMap)
      {
        var dimensionsEnumerator = e.Value.GetEnumerator();

        if (dimensionsEnumerator.MoveNext())
        {
          IAbstractDomainForEnvironments<Variable, Expression> aProperty;

          if (this.content.TryGetValue(dimensionsEnumerator.Current, out aProperty))
          {
            var property = (IAbstractDomainForEnvironments<Variable, Expression>)aProperty.Clone();

            while (dimensionsEnumerator.MoveNext())
            {
              if (this.content.TryGetValue(dimensionsEnumerator.Current, out aProperty))
              {
                property = (IAbstractDomainForEnvironments<Variable, Expression>)property.Join(aProperty);
              }
              else
              {
                throw new AbstractInterpretationException("incorrect transformation map");
              }
            }

            result.content.Add(e.Key, property);
          }
          else
          {
            throw new AbstractInterpretationException("incorrect transformation map");
          }
        }
        else
        {
          throw new AbstractInterpretationException("incorrect transformation map");
        }
      }

      return result;
    }


    #region Lattice elements and operators

    public SimpleArrayAbstraction<Variable, Expression> Bottom
    {
      get
      {
        //This bottom = this.Factory();
        //var topSlice = this.oneSlice.Top;
        //bottom.content.Add((SliceAbstractDomain)topSlice.Clone());

        //return bottom;
        throw new NotImplementedException();
      }
    }

    public SimpleArrayAbstraction<Variable, Expression> Top
    {
      get
      {
        return new SimpleArrayAbstraction<Variable, Expression>(this.encoder, this.decoder);
      }
    }

    /// <summary>
    /// Order ...
    /// </summary>
    /// 
    public bool LessEqual(SimpleArrayAbstraction<Variable, Expression> right)
    {
      bool result;
      if (AbstractDomainsHelper.TryTrivialLessEqual(this, right, out result))
      {
        return result;
      }

      foreach (var e in this.content)
      {
        IAbstractDomainForEnvironments<Variable, Expression> rightProperty;
        var leftProperty = e.Value;

        if (right.content.TryGetValue(e.Key, out rightProperty))
        {
          if (!leftProperty.LessEqual(rightProperty))
          {
            return false;
          }
        }
        else
        {
          throw new AbstractInterpretationException();
        }
      }

      return true;
    }

    public SimpleArrayAbstraction<Variable, Expression> Join(SimpleArrayAbstraction<Variable, Expression> right)
    {
      SimpleArrayAbstraction<Variable, Expression> trivialResult;
      if (AbstractDomainsHelper.TryTrivialJoin(this, right, out trivialResult))
      {
        return trivialResult;
      }

      SimpleArrayAbstraction<Variable, Expression> result = new SimpleArrayAbstraction<Variable, Expression>(this.encoder, this.decoder);

      foreach (var e in this.content)
      {
        IAbstractDomainForEnvironments<Variable, Expression> rightProperty;
        var leftProperty = e.Value;

        if (right.content.TryGetValue(e.Key, out rightProperty))
        {
          result.content.Add(e.Key, (IAbstractDomainForEnvironments<Variable, Expression>)leftProperty.Join(rightProperty));
        }
        else
        {
          throw new AbstractInterpretationException();
        }
      }

      return result;
    }

    public SimpleArrayAbstraction<Variable, Expression> Meet(SimpleArrayAbstraction<Variable, Expression> right)
    {
      SimpleArrayAbstraction<Variable, Expression> trivialResult;
      if (AbstractDomainsHelper.TryTrivialMeet(this, right, out trivialResult))
      {
        return trivialResult;
      }

      var result = new SimpleArrayAbstraction<Variable, Expression>(this.encoder, this.decoder);

      foreach (var e in this.content)
      {
        IAbstractDomainForEnvironments<Variable, Expression> rightProperty;
        var leftProperty = e.Value;

        if (right.content.TryGetValue(e.Key, out rightProperty))
        {
          result.content.Add(e.Key, (IAbstractDomainForEnvironments<Variable, Expression>)leftProperty.Meet(rightProperty));
        }
        else
        {
          throw new AbstractInterpretationException();
        }
      }

      return result;
    }

    #endregion

    #region IAbstractDomain Members

    /// <summary>
    /// TODO
    /// </summary>
    public bool IsBottom
    {
      get
      {
        // TODO
        return false;
      }
    }

    /// <summary>
    /// TODO
    /// </summary>
    public bool IsTop
    {
      get
      {
        foreach (var property in this.content.Values)
        {
          if (!property.IsTop)
          {
            return false;
          }
        }

        return true;
      }
    }

    public bool LessEqual(IAbstractDomain a)
    {
      return this.LessEqual((SimpleArrayAbstraction<Variable, Expression>)a);
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
      return this.Join((SimpleArrayAbstraction<Variable, Expression>)a);
    }

    IAbstractDomain/*!*/ IAbstractDomain.Meet(IAbstractDomain/*!*/ a)
    {
      return this.Meet((SimpleArrayAbstraction<Variable, Expression>)a);
    }

    IAbstractDomain/*!*/ IAbstractDomain.Widening(IAbstractDomain/*!*/ prev)
    {
      return this.Join((SimpleArrayAbstraction<Variable, Expression>)prev);
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

    #region ICloneable Members

    public object Clone()
    {
      return new SimpleArrayAbstraction<Variable, Expression>(this);
    }

    #endregion

    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      // TODO: this code can be factored with SimplePartitionAbstractionFactory.PartialAssignInParallel(...)

      var multipleAssignedVariables = new Dictionary<Variable, FList<Variable>>();

      foreach (var e in sourcesToTargets)
      {
        if (e.Value.Length() > 1)
        {
          multipleAssignedVariables.Add(e.Key, e.Value);
        }
      }

      foreach (var arrayProperty in this.content.Values)
      {
        if (!arrayProperty.IsBottom)
        {
           var originalVariables = arrayProperty.Variables;

           arrayProperty.AssignInParallel(sourcesToTargets, convert);

           // removing variables introduced by the treatment of assignment to multiple variables
           foreach (var e in multipleAssignedVariables)
           {
             if (!originalVariables.Contains(e.Key))
             {
               foreach (var v in e.Value.GetEnumerable())
               {
                 arrayProperty.RemoveVariable(v);
               }
             }
             else
             {
               // TODO?
             }
           }
        }
      }

      return;
    }

    #endregion

    public IArrayAbstraction<Variable, Expression> Assign(int dimension, TouchKind kind, Variable dest, Variable value)
    {
      if (this.content.ContainsKey(dimension))
      {
        Expression destExp, valueExp;
        switch (kind)
        {
          case TouchKind.Strong:
            destExp = this.encoder.VariableFor(dest);
            valueExp = this.encoder.VariableFor(value);

            this.content[dimension].Assign(destExp, valueExp);
            break;

          case TouchKind.Weak:
            var result = (IAbstractDomainForEnvironments<Variable, Expression>)this.content[dimension].Top.Clone();

            destExp = this.encoder.VariableFor(dest);
            valueExp = this.encoder.VariableFor(value);
            
            result.Assign(destExp, valueExp);
            
            this.content[dimension] = (IAbstractDomainForEnvironments<Variable, Expression>)this.content[dimension].Join(result);
            break;
        }
      }
      else
      {
        throw new AbstractInterpretationException();
      }

      return (IArrayAbstraction<Variable, Expression>)this;
    }

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
        StringBuilder tempStr = new StringBuilder();

        foreach (var e in this.content)
        {
          //string xAsString = this.decoder != null ? this.decoder.NameOf(x) : x.ToString();
          tempStr.Append(e.Key + ":" + e.Value + ";");
        }

        result = tempStr.ToString();
        int indexOfLastComma = result.LastIndexOf(";");
        if (indexOfLastComma > 0)
        {
          result = result.Remove(indexOfLastComma);
        }
      }

      return result;
    }

    #endregion

  }
}
#endif