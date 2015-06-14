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
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    /// <summary>
    /// Entry point to run the container analysis
    /// </summary>
    public static IMethodResult<Variable> RunContainerAnalysis<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
    (
      string methodName,
      IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> driver,
      TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>.PartitionAnalysis partitions,
      List<Analyzers.Containers.ContainerOptions> options)
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {
      var analysis =
       new TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>.ContainerAnalysis(methodName, driver, partitions, options);

      return TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>.RunTheAnalysis(methodName, driver, analysis);
    }


    /// <summary>
    /// This class is just for binding types for the internal classes
    /// </summary>
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {
      #region Facility to forward operations on the abstract domain (implementation of IAbstractDomainOperations)
      public class AbstractOperationsImplementationContainer : IAbstractDomainOperations<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>
      {
        public bool LookupState(IMethodResult<Variable> mr, APC pc, out SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate)
        {
          astate = null;
          ContainerAnalysis an = mr as ContainerAnalysis;
          if (an == null)
            return false;

          return an.PreStateLookup(pc, out astate);
        }

        public SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Join(IMethodResult<Variable> mr, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate1, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate2)
        {
          ContainerAnalysis an = mr as ContainerAnalysis;
          if (an == null)
            return null;

          bool bWeaker;
          return an.Join(new Pair<APC, APC>(), astate1, astate2, out bWeaker, false);
        }

        public List<BoxedExpression> ExtractAssertions(
          IMethodResult<Variable> mr,
          SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate,
          IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> metaDataDecoder)
        {
          ContainerAnalysis an = mr as ContainerAnalysis;
          if (an == null)
            return null;

          BoxedExpressionReader<APC, Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly> br = new BoxedExpressionReader<APC, Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly>(context, metaDataDecoder);

          return an.ToListOfBoxedExpressions(astate, br);
        }

        public bool AssignInParallel(IMethodResult<Variable> mr, ref SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> mapping, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          ContainerAnalysis an = mr as ContainerAnalysis;
          if (an == null)
            return false;

          astate.AssignInParallel(mapping, convert);
          return true;
        }
      }
      #endregion


      /// <summary>
      /// The analysis for the contents of containers
      /// </summary>
      public class ContainerAnalysis :
        GenericValueAnalysis<SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, Analyzers.Containers.ContainerOptions>
      {
        
        #region Private state

        private NumericalAnalysis<Analyzers.Containers.ContainerOptions> indexesAnalysis;
        readonly private PartitionAnalysis partitions;
        readonly ContainersObligations obligations;

        #endregion
        
        #region Constructor

        internal ContainerAnalysis(
          string methodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> mdriver,
          PartitionAnalysis partitions,
          List<Analyzers.Containers.ContainerOptions> optionsList)
            : base(methodName, mdriver, optionsList[0])
        {
          // TODO : we can imagine that the analysis used to handle indexes is choose wrt optionsList.
          //this.indexesAnalysis = new NumericalAnalysis<Options>(this.MethodName, this.MethodDriver, new List<Analyzers.Bounds.BoundsOptions>());
          this.indexesAnalysis = new NumericalAnalysis<Analyzers.Containers.ContainerOptions>(this.MethodName, this.MethodDriver, optionsList);
          this.partitions = partitions;
          this.obligations = new ContainersObligations(mdriver, optionsList[0]);

        }

        #endregion

        #region Implementation of the abstract interface

        protected internal override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> InitialValue
        {
          get
          {
            var indexes = this.indexesAnalysis.InitialValue;
            var contents = new SimpleArrayPropertiesAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(this.Encoder, this.Decoder);

            return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents, this.Decoder, this.Encoder);
          }
        }

        protected override ProofObligations<Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>> Obligations
        {
          get
          {
            // TODO
            return this.obligations;
          }
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery
        {
          get 
          {
            // TODO
            return new ComposedFactQuery<Variable>(this.MethodDriver.IsUnreachable); 
          }
        }

        protected override void SuggestAnalysisSpecificPostconditions(List<BoxedExpression> postconditions)
        {
          return;
        }

        protected override bool TrySuggestPostconditionForOutParameters(List<BoxedExpression> postconditions, Variable p, FList<PathElement> path)
        {
          return false;
        }

        #endregion

        private Boolean TryDefaultValue(Type type, out BoxedExpression defaultExp)
        {
          //this.DecoderForMetaData.
          if (this.DecoderForMetaData.System_Boolean.Equals(type))
          {
            defaultExp = this.Encoder.ConstantFor(default(System.Boolean));
            return true;
          }
          else if (this.DecoderForMetaData.System_Int16.Equals(type))
          {
            defaultExp = this.Encoder.ConstantFor(default(System.Int16));
            return true;
          }
          else if (this.DecoderForMetaData.System_Int32.Equals(type))
          {
            defaultExp = this.Encoder.ConstantFor(default(System.Int32));
            return true;
          }
          else if (this.DecoderForMetaData.System_Int8.Equals(type) || this.DecoderForMetaData.System_UInt8.Equals(type))
          {
            defaultExp = this.Encoder.ConstantFor(default(System.SByte));
            return true;
          } 
          else if (this.DecoderForMetaData.System_Char.Equals(type) || this.DecoderForMetaData.System_UInt16.Equals(type))
          {
            defaultExp = this.Encoder.ConstantFor(default(System.Int16));
            return true;
          }
          else if (this.DecoderForMetaData.System_UInt32.Equals(type))
          {
            defaultExp = this.Encoder.ConstantFor(default(System.Int32));
            return true;
          }
          else if (this.DecoderForMetaData.System_UInt64.Equals(type))
          {
            defaultExp = this.Encoder.ConstantFor(default(System.Int64));
            return true;
          }
          //else if (this.DecoderForMetaData.System_Object.Equals(type))
          //{
          //  defaultExp = this.Encoder.ConstantFor(default(System.Object));
          //  return true;
          //}
          else 
          {
            defaultExp = null;
            return false;
          }

        }

        protected override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HelperForJoin(SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> newState, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> prevState, Pair<APC, APC> edge)
        {
          /// - that an array can be null, and in his case not appearing into the environment

          var keep = new Set<BoxedVariable<Variable>>();

          foreach (var array in prevState.Right.Variables)
          {
            SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> postPartitions;

            if (this.partitions.TryPartitionAt(edge.Two, out postPartitions))
            {
              IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> postPartition;

              if (postPartitions.TryGetValue(array, out postPartition))
              {
                keep.Add(array);
              }
            }
            else
            {
              throw new AbstractInterpretationException();
            }
          }

          var result = newState.Join(prevState, keep);

          //return base.HelperForJoin(newState, prevState, edge);
          return result;
        }

        public override void HelperForAssignInParallel(SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> state, Pair<APC, APC> edge, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          //state.Left.AssignInParallel(refinedMap);

          // Warning : multiple assigned vriables can have different post partitions, because they are the result
          // of the whole analysis, and some merge could have happenned for only one of the multiple assigned.
          // tht is why you assign in pralle before adapt arrayProperties

          var originalArrays = new List<BoxedVariable<Variable>>(state.Right.Keys);

          base.HelperForAssignInParallel(state, edge, refinedMap, convert); 

          SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> prePartitions;

          if (this.partitions.TryPartitionAt(edge.One, out prePartitions))
          {
            var contents = state.Right;

            foreach (var array in originalArrays)
            {
              // if the array is kept
              if (refinedMap.ContainsKey(array))
              {
                IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> prePartition;
                if (prePartitions.TryGetValue(array, out prePartition))
                {
                  var prePartitionRebinded = prePartition.PartialAssignInParallel(refinedMap, convert);

                  SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> postPartitions;

                  if (this.partitions.TryPartitionAt(edge.Two, out postPartitions))
                  {
                    IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> postPartition;
                    //arrayRebinded = refinedMap[array].Head;
                    foreach (var arrayRebinded in refinedMap[array].GetEnumerable())
                    {
                      if (postPartitions.TryGetValue(arrayRebinded, out postPartition))
                      {
                        var transformMap = prePartitionRebinded.TransformationMapFor(postPartition);

                        contents.TransformArray(arrayRebinded, transformMap);
                        contents = Normalize(arrayRebinded, contents, state.Left, postPartition);
                      }
                      else
                      {
                        // no partition in postState ... 
                        // TODO : make the arrayproperties attached to the array disappear.
                        contents.RemoveElement(arrayRebinded);
                        //throw new AbstractInterpretationException();
                      }
                    }
                  }
                  else
                  {
                    throw new AbstractInterpretationException();
                  }
                }
              }
            }
            //state = new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(
          }
          else
          {
            throw new AbstractInterpretationException();
          }

          //base.HelperForAssignInParallel(state, edge, refinedMap); 
          //state.Right.AssignInParallel(refinedMap);
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Assume(APC pc, string tag, Variable source, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var result =  base.Assume(pc, tag, source, data);
          var contents = result.Right;

          // TODO : before calling normalize see if invovled variables are into the partition
          //var refinedExp = ToBoxedExpression(pc, source);

          SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> postPartitions;

          if (this.partitions.TryPartitionAt(this.Context.Post(pc), out postPartitions))
          {
            IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> postPartition;

            var arrays = new List<BoxedVariable<Variable>>(contents.Keys);

            foreach (var array in arrays)
            {
              if (postPartitions.TryGetValue(array, out postPartition))
              {
                contents = Normalize(array, contents, result.Left, postPartition);
              }
            }
          }

          return result;
        }

        private SimpleArrayPropertiesAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Normalize(
          BoxedVariable<Variable> array, 
          SimpleArrayPropertiesAbstractDomain<BoxedVariable<Variable>, 
          BoxedExpression> dataContents, 
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> dataIndexes, 
          IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> partition)
        {
          if (dataIndexes.IsBottom)
          {
            // TODO
          }

          var knowledge = dataIndexes.To<BoxedExpression>(new BoxedExpressionFactory<Local,Parameter,Method,Field,Property,Type,Attribute,Assembly>(this.DecoderForMetaData));

          var emptyDimensions = partition.EmptyDimensionsInContext(knowledge);

          dataContents.SimplifyArray(array, emptyDimensions);

          return dataContents;
        }

        private SimpleArrayPropertiesAbstractDomain<BoxedVariable<Variable>, BoxedExpression> AdaptPartition(
          BoxedVariable<Variable> array, 
          SimpleArrayPropertiesAbstractDomain<BoxedVariable<Variable>, BoxedExpression> dataContents, 
          IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> fromPartition, 
          IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> toPartition)
        {
          // Assumption: fromPartition is not top
          if (fromPartition.IsTop)
          {
            throw new AbstractInterpretationException();
          }

          var transformMap = fromPartition.TransformationMapFor(toPartition);

          dataContents.TransformArray(array, transformMap);

          return dataContents;
        }

        private SimpleArrayPropertiesAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleAllocations(APC pc, Type type, Variable dest, Variable len, SimpleArrayPropertiesAbstractDomain<BoxedVariable<Variable>, BoxedExpression> contentsData)
        {
          
            //SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> prevPartitions;

            //if (this.partitions.TryPartitionAt(pc, out prevPartitions)) 
            //{
            //  var destAsExp = BoxedExpression.For(this.Context.Refine(pc, dest), this.Decoder.Outdecoder);
            //  IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> prevPartition;

            //  if (prevPartitions.TryGetValue(destAsExp, out prevPartition))
            //  {
            //    throw new AbstractInterpretationException(); // TODO : avoid above code, assuming that there is no partition before
            //  }
            //  else
            //  {

            SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> postPartitions;

            if (this.partitions.TryPartitionAt(this.Context.Post(pc), out postPartitions))
            {
              // var destAsExp = BoxedExpression.For(this.Context.Refine(pc, dest), this.Decoder.Outdecoder);

              var destB = new BoxedVariable<Variable>(dest);

              IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> postPartition;

              if (postPartitions.TryGetValue(destB, out postPartition))
              {
                var lenAsExp = BoxedExpression.For(this.Context.Refine(pc, len), this.Decoder.Outdecoder);
                lenAsExp = this.Decoder.Stripped(lenAsExp);

                var sliceRep = postPartition.Representative();
                
                var lowerConstraint = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, 
                  ExpressionOperator.GreaterEqualThan, 
                  this.Encoder.VariableFor(sliceRep), 
                  this.Encoder.ConstantFor(0));

                var greaterConstraint = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, 
                  ExpressionOperator.LessThan, 
                  this.Encoder.VariableFor(sliceRep), 
                  lenAsExp);
                
                var bodyConstraint = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.And, lowerConstraint, greaterConstraint);

                var touchedDimensions = postPartition.DimensionsTouchedBy(bodyConstraint, new Set<BoxedVariable<Variable>>());

                touchedDimensions = touchedDimensions.FindAll(e => e.Two == TouchKind.Strong);

                if (touchedDimensions.Count == 1)
                {
                  var touchedDimension = touchedDimensions.PickAnElement();
                  var bodyDimension = touchedDimension.One;
                  BoxedExpression defaultValue;

                  var b = TryDefaultValue(type, out defaultValue);
                  contentsData.CreateArray(destB, postPartition.Dimensions(), bodyDimension, defaultValue);
                }
                else
                {
                  throw new AbstractInterpretationException();
                }
              }
              else
              {
                throw new AbstractInterpretationException();
              }
              //}
              //else
              //{
              //  throw new AbstractInterpretationException();
              //}
            }
          
          return contentsData;

        }

        /// <summary>
        /// 
        /// </summary>
        private SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> GenericArrayHandling(APC pc, Variable array, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          Variable arrayLengthVar;
          //var type = this.Context.GetType(pc, dest);
          if (this.Context.TryGetArrayLength(this.Context.Post(pc), array, out arrayLengthVar))
          {
            var arrayB = new BoxedVariable<Variable>(array);

            // if a symbolic value is assigned twice, thanks to symbolic values semantics, its equivalent to nop. 
            // So we keep the arraysProperties such as they are.
            if (!data.Right.ContainsKey(arrayB))
            {
              var arrayType = this.Context.GetType(this.Context.Post(pc), array);
              if (arrayType.IsNormal)
              {
                var type = this.DecoderForMetaData.ElementType(arrayType.Value);
                var contents = data.Right;
                contents = HandleAllocations(pc, type, array, arrayLengthVar, contents);
                return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(data.Left, contents, this.Decoder, this.Encoder);
              }
              else
              {
                throw new AbstractInterpretationException();
              }
            }

            return data;
          }

          Debug.Assert(false, "ArrayLength of an array must be known");

          return data;
        }

        #region MSILVisitor Members

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          // TODO: what is isOld for ?
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }


        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldsfld(APC pc, Field field, bool @volatile, Variable dest, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Castclass(APC pc, Type type, Variable dest, Variable obj, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var indexes = this.indexesAnalysis.Binary(pc, op, dest, s1, s2, data.Left);
          var contents = data.Right;

          if (OperatorExtensions.IsComparisonBinaryOperator(op))
          {
            // TODO: if s1 or s2 includes an array term, gaurd must be take into account
            return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents);
          }
          else if (OperatorExtensions.IsBooleanBinaryOperator(op))
          {
            // TODO: see above
            return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents);
          }
          else
          {
            var leftAsExp = ToBoxedExpressionWithConstantRefinement(pc, s1);
            var rightAsExp = ToBoxedExpressionWithConstantRefinement(pc, s2);

            // adapt
            BoxedExpression rightOfAssignment;
            if (TryRepackAssignment(op, leftAsExp, rightAsExp, out rightOfAssignment))
            {
              SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> prePartitions;
              SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> postPartitions;

              if (this.partitions.TryPartitionAt(pc, out prePartitions))
              {
                var arraysToRefine = prePartitions.arraysWithPartitionDefinedOnASubsetExpressionOf(rightOfAssignment);

                // NOTE: this does not means that each array in arraysToRefine are in the current state, BUT they should be

                foreach (var arrayAsExp in arraysToRefine)
                {
                  if (this.partitions.TryPartitionAt(this.Context.Post(pc), out postPartitions))
                  {
                    IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> prePartition;
                    IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> postPartition;
                    if (prePartitions.TryGetValue(arrayAsExp, out prePartition))
                    {
                      if (postPartitions.TryGetValue(arrayAsExp, out postPartition))
                      {
                        contents = AdaptPartition(arrayAsExp, contents, prePartition, postPartition);
                      }
                    }
                    //refinedDomain = HandleIndexAssignment("", pc, arrayAsExp, destAsExp, rightOfAssignment, assignmentKnowledge, refinedDomain);
                  }
                }
              }
            }
          }
          
          return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents);
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList lengths, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var indexes = this.indexesAnalysis.Newarray<ArgList>(pc, type, dest, lengths, data.Left);
          var contents = data.Right;

          if (lengths.Count > 1)
          {
            return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents);
          }
          else
          {
            contents = HandleAllocations(pc, type, dest, lengths[0], contents);
          }
          
          return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents);
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var indexes = this.indexesAnalysis.Ldelem(pc, type, dest, array, index, data.Left);
          var contents = data.Right;

          return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents);
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var indexes = this.indexesAnalysis.Ldelema(pc, type, @readonly, dest, array, index, data.Left);
          var contents = data.Right;

          return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents);
        }

        public bool TryRetrievePreAndPostPartitions(
          APC pc, 
          BoxedVariable<Variable> array, 
          out Pair<IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression>, IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression>> partitions)
        {
          SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> prePartitions;
          SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> postPartitions;

          if (this.partitions.TryPartitionAt(pc, out prePartitions))
          {
            IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> prePartition;
            if (prePartitions.TryGetValue(array, out prePartition))
            {
              if (this.partitions.TryPartitionAt(this.Context.Post(pc), out postPartitions))
              {
                IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> postPartition;

                if (postPartitions.TryGetValue(array, out postPartition))
                {
                  partitions = new Pair<IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression>, IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression>>(prePartition, postPartition);
                  return true;
                }
              }
            }
          }

          partitions = default(Pair<IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression>, IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression>>);
          return false;
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Stelem(APC pc, Type type, Variable array, Variable index, Variable value, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var indexes = this.indexesAnalysis.Stelem(pc, type, array, index, value, data.Left);
          var contents = data.Right;

          if (isOneDimensionalArray(pc, array))
          {
            //var valueAsExp = BoxedExpression.For(this.Context.Refine(pc, value), this.Decoder.Outdecoder);

            var arrayBoxed = ToBoxedVariable(array);

            // adapt the abstract value attached to the array
            Pair<IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression>, IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression>> partitions;

            if (TryRetrievePreAndPostPartitions(pc, arrayBoxed, out partitions))
            {
              contents = AdaptPartition(arrayBoxed, contents, partitions.One, partitions.Two);

              contents = Normalize(arrayBoxed, contents, indexes, partitions.Two);

            }
            else
            {
              throw new AbstractInterpretationException();
            }

            var indexAsExp = ToBoxedExpression(pc, index);
            var indexBoxed = ToBoxedVariable(index);

            // assign
            var baseVariables = this.Decoder.VariablesIn(indexAsExp);
            var notWanted = new Set<BoxedVariable<Variable>>();

            if (baseVariables.Count > 1 || !baseVariables.Contains(indexBoxed))
            {
              notWanted.Add(indexBoxed);
            }

            var constraintAssignement = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, 
              ExpressionOperator.Equal, 
              ToBoxedExpression(pc, partitions.Two.Representative()), 
              indexAsExp);
            
            //TODO: improve by var knowledgeIndex = ...
            var assignedDimensions = partitions.Two.DimensionsTouchedBy(constraintAssignement, notWanted);

            contents.HandleArrayAssignment(
              arrayBoxed, 
              assignedDimensions, 
              ToBoxedVariable(value));

            // be aware of changes of multiple abstract values attached to different arrays where the array appears
            // project on other arrays (TODO)
          }

          return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents);
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var indexes = this.indexesAnalysis.Call<TypeList, ArgList>(pc, method, tail, virt, extraVarargs, dest, args, data.Left);
          var contents = data.Right;

          if (isOneDimensionalArray(pc, dest))
          {
            //string fullNameForMethod = this.DecoderForMetaData.FullName(method);

            //if (fullNameForMethod.StartsWith("System.Runtime.CompilerServices.RuntimeHelpers.InitializeArray"))
            //{
            //  Variable arrayLengthVar;
            //  if (this.Context.TryGetArrayLength(this.Context.Post(pc), args[0], out arrayLengthVar))
            //  {
            //    var arrayLengthExp = BoxedExpression.For(this.Context.Refine(pc, arrayLengthVar), this.Decoder.Outdecoder);
            //    int n;

            //    if (arrayLengthExp.IsConstantInt(out n))
            //    {
            //      var refinedDomain = data;
            //      var arrayAsExp = BoxedExpression.For(this.Context.Refine(pc, args[0]), this.Decoder.Outdecoder);

            //      BoxedExpression knowledge = this.Encoder.ConstantFor(true);

            //      for (int i = 0; i < n; i++)
            //      {
            //        var touchedDimensions = ...  this.Encoder.ConstantFor(i) ... ;
            //        contents =  contents.HandleArrayAssignment(arrayAsExp, touchedDimensions, ???);
            //      }

            //      return refinedDomain;
            //    }
            //    else
            //    {
            //      // TODO: probably nothing
            //    }
            //  }

            //  Debug.Assert(false, "ArrayLength of an array must be known");
            //}

            return GenericArrayHandling(pc, dest, new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents));

          }

          return new SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(indexes, contents);
        }

        public override SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression>  Isinst(APC pc, Type type, Variable dest, Variable obj, SimpleArrayAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          // TODO: what is isOld for ?
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }

        #endregion

      }

    }
  }
}
#endif
