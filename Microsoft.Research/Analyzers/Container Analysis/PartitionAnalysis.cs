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
using System.Collections.Generic;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    /// <summary>
    /// Entry point to run the partition analysis
    /// </summary>
    public static
      TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>.PartitionAnalysis
      RunPartitionAnalysis<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
    (
      string methodName,
      IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> driver,
      List<Analyzers.Containers.ContainerOptions> options,
      IFactQuery<BoxedExpression, Variable> factQuery)
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {
      //var analysis =
      // new TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>.PartitionAnalysis(methodName, driver, options);

      return TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>.HelperForPartitionAnalysis(methodName, driver, options, factQuery);
    }

    /// <summary>
    /// This class is just for binding types for the internal classes
    /// </summary>
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {

      /// <summary>
      /// It runs the analysis. 
      /// It is there because so we can use the typebinding, and make the code less verbose.
      /// </summary>
      internal static PartitionAnalysis 
        HelperForPartitionAnalysis(string methodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> driver,
          List<Analyzers.Containers.ContainerOptions> optionsList,
          IFactQuery<BoxedExpression, Variable> factQuery)
      {
        PartitionAnalysis analysis;
        analysis = new PartitionAnalysis(methodName, driver, optionsList, factQuery);

        // *** The next lines must be strictly sequential ***
        var closure = driver.CreateForward<SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>(analysis);

        // At this point, CreateForward has called the Visitor, so the context has been created, so that now we can call initValue

        SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression>.Trace = optionsList[0].TracePartitionAnalysis;

        var initValue = analysis.InitialValue;

        closure(initValue); // Do the analysis 

        return analysis;
      }

      #region Facility to forward operations on the abstract domain (implementation of IAbstractDomainOperations)
      public class AbstractOperationsImplementationPartition : IAbstractDomainOperations<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>
      {
        public bool LookupState(IMethodResult<Variable> mr, APC pc, out SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate)
        {
          astate = null;
          PartitionAnalysis an = mr as PartitionAnalysis;
          if (an == null)
            return false;

          return an.PreStateLookup(pc, out astate);
        }

        public SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Join(IMethodResult<Variable> mr, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate1, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate2)
        {
          PartitionAnalysis an = mr as PartitionAnalysis;
          if (an == null)
            return null;

          bool bWeaker;
          return an.Join(new Pair<APC, APC>(), astate1, astate2, out bWeaker, false);
        }

        public List<BoxedExpression> ExtractAssertions(
          IMethodResult<Variable> mr,
          SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate,
          IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> metaDataDecoder)
        {
          PartitionAnalysis an = mr as PartitionAnalysis;
          if (an == null)
            return null;

          BoxedExpressionReader<APC, Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly> br = new BoxedExpressionReader<APC, Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly>(context, metaDataDecoder);

          return an.ToListOfBoxedExpressions(astate, br);
        }

        public bool AssignInParallel(IMethodResult<Variable> mr, ref SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> mapping, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          PartitionAnalysis an = mr as PartitionAnalysis;
          if (an == null)
            return false;

          astate.AssignInParallel(mapping, convert);
          return true;
        }
      }
      #endregion

      /// <summary>
      /// The analysis for the partitions of the containers : a generic value analysis
      /// </summary>
      public class PartitionAnalysis :
        GenericValueAnalysis<SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, Analyzers.Containers.ContainerOptions>
      {

        //protected readonly List<Analyzers.Containers.ContainerOptions> optionsList;
        readonly PartitionsObligations obligations;
        //internal IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> mdriver;
        protected readonly IFactQuery<BoxedExpression, Variable> factQuery;

        #region Constructors

        internal PartitionAnalysis(
          string methodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> mdriver,
          List<Analyzers.Containers.ContainerOptions> optionsList,
          IFactQuery<BoxedExpression, Variable> factQuery)
          : base(methodName, mdriver, optionsList[0])
        {
          //this.mdriver = mdriver;
          //this.optionsList = optionsList;
          this.factQuery = factQuery;
          this.obligations = new PartitionsObligations(mdriver, optionsList[0]);
        }

        #endregion

        #region Extract information

        public bool TryPartitionAt(APC pc, out SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> result)
        {
          if (this.fixpointInfo.PreState(pc, out result))
          {
            return true;
          }

          result = default(SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression>);
          return false;
        }

        #endregion

        #region Implementation of the abstract interface

        protected internal override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> InitialValue
        {
          get
          {
            // TODO: implement an option for choosing partition representation
            // IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> partition = new ...
            // return new SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(this.Encoder, this.Decoder, partition);

            // Return an empty environment of partitions based on Octagons.
            return new SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression>(this.Encoder, this.Decoder);
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
            // Result of a partition analysis will not be translated into a factQuery
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



        protected override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HelperForJoin(SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> newState, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> prevState, Pair<APC, APC> edge)
        {
          /// - that an array can be null, and in his case not appearing into the environment

          var keep = new Set<BoxedVariable<Variable>>();

          foreach (var bv in prevState.Variables)
          {
            Variable v;
            if (bv.TryUnpackVariable(out v))
            {
              var atJoinPoint = this.factQuery.IsNull(edge.Two, v);
              var atJoinedPoint = this.factQuery.IsNull(edge.One, v);

              if (atJoinPoint == ProofOutcome.Top)
              {
                //if (atJoinedPoint == ProofOutcome.True)
                //{
                //  // do nothing
                //}
                //else if (atJoinedPoint == ProofOutcome.False)
                {
                  // for some paths exp is null : we want to keep it in the environment
                  keep.Add(bv);
                }

                // TODO: could be also TOP if in every branch a (non-identity) rebinding appeared
              }
            }
          }
          
          var result = newState.Join(prevState, keep);

          //return base.HelperForJoin(newState, prevState, edge);
          return result;
        }

        #region OfInterest1

        /// <summary>
        /// Retrieve the lower bounds and the upper bounds of an <typeparam name="exp">expression</typeparam> at some <typeparam name="pc">control point</typeparam> and return
        /// the conjonction of the corresponding constraint on the <typeparam name="exp">expression</typeparam>.
        /// </summary>
        private BoxedExpression gatherKnowledge(APC pc, BoxedExpression exp)
        {
          var lowerBounds = this.factQuery.LowerBoundsAsExpressions(pc, exp, false);
          var strictLowerBounds = this.factQuery.LowerBoundsAsExpressions(pc, exp, true);
          var upperBounds = this.factQuery.UpperBoundsAsExpressions(pc, exp, false);
          var strictUpperBounds = this.factQuery.UpperBoundsAsExpressions(pc, exp, true);

          var result = this.Encoder.ConstantFor(true);
          
          foreach(var lowerBound in lowerBounds)
          {
            if (!(exp.IsConstant && lowerBound.IsConstant))
            {
              var comparison = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.GreaterEqualThan, exp, lowerBound);
              result = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.And, comparison, result);
            }
          }
          
          foreach (var strictLowerBound in strictLowerBounds)
          {
            if (!(exp.IsConstant && strictLowerBound.IsConstant))
            {
              var comparison = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.GreaterThan, exp, strictLowerBound);
              result = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.And, comparison, result);
            }
          }
          
          foreach (var upperBound in upperBounds)
          {
            if (!(exp.IsConstant && upperBound.IsConstant))
            {
              var comparison = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, exp, upperBound);
              result = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.And, comparison, result);
            }
          }
          
          foreach (var strictUpperBound in strictUpperBounds)
          {
            if (!(exp.IsConstant && strictUpperBound.IsConstant))
            {
              var comparison = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, exp, strictUpperBound);
              result = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.And, comparison, result);
            }
          }
          
          return result;
        }

        /// <summary>
        /// Infer the tightest lower bound and upper bound of an <typeparam name="exp">expression</typeparam> at some <typeparam name="pc">control point</typeparam>
        /// TODO : ... if possible based on some <typeparam name="vars">specific variables</typeparam>.
        /// </summary>
        /// <remarks>
        /// Inference based on syntaxic comparisons for performance reasons.
        /// </remarks>
        private Pair<BoxedExpression,BoxedExpression> guessRange(APC pc, BoxedExpression exp)
        {
          if (exp.IsConstant)
          {
            return new Pair<BoxedExpression, BoxedExpression>(exp, exp);
          }

          var expVar = this.Decoder.UnderlyingVariable(exp);

          // lower bound
          var lowerBounds = this.factQuery.LowerBoundsAsExpressions(pc, exp, false);
          var lowerCandidates = new Dictionary<BoxedExpression, IEnumerable<BoxedExpression>>();

          foreach (var lowerBound in lowerBounds)
          {
            var lowerBoundVar = this.Decoder.UnderlyingVariable(lowerBound);

            // we need a non-trivial lower bound
            if (lowerBound.Equals(exp) 
              || (this.Decoder.VariablesIn(lowerBound).Contains(expVar)) 
              || (this.Decoder.VariablesIn(exp).Contains(lowerBoundVar)))
            {
              continue;
            }
            
            // initialization
            if (lowerCandidates.Count == 0)
            {
              lowerCandidates.Add(lowerBound, this.factQuery.LowerBoundsAsExpressions(pc, lowerBound, false));
              continue;
            }

            // Is there already a tighter candidate ?
            var uninteresting = false;
            var equalToCandidate = false;
            foreach (var lowerCandidate in lowerCandidates)
            {
              // if they (lowerBound and lowerCandidate) are equals, keep both, we will choose later.
              // TODO: Or choose now ?
              if (lowerBound.Equals(lowerCandidate.Key))
              {
                equalToCandidate = true;
                break;
              }

              foreach (var uninterestingBound in lowerCandidate.Value)
              {
                if (lowerBound.Equals(uninterestingBound))
                {
                  
                  uninteresting = true;
                  break;
                }
              }
              if (uninteresting)
              {
                break;
              }
            }

            if (uninteresting)
            {
              continue;
            }

            var lowerLowerBounds = this.factQuery.LowerBoundsAsExpressions(pc, lowerBound, false);

            if (!equalToCandidate)
            {
              // Is it tighter than an existing candidate ?
              foreach (var lowerLowerBound in lowerLowerBounds)
              {
                if (lowerCandidates.ContainsKey(lowerLowerBound))
                {
                  lowerCandidates.Remove(lowerLowerBound);
                }
              }
            }

            lowerCandidates.Add(lowerBound, lowerLowerBounds);
          }

          // upper bound
          var upperBounds = this.factQuery.UpperBoundsAsExpressions(pc, exp, false);
          var upperCandidates = new Dictionary<BoxedExpression, IEnumerable<BoxedExpression>>();

          foreach (var upperBound in upperBounds)
          {
            var upperBoundVar = this.Decoder.UnderlyingVariable(upperBound);

            // we need a non-trivial upper bound
            if (upperBound.Equals(exp) 
              || (this.Decoder.VariablesIn(upperBound).Contains(expVar)) 
              || (this.Decoder.VariablesIn(exp).Contains(upperBoundVar)))
            {
              continue;
            }
            
            // initialization
            if (upperCandidates.Count == 0)
            {
              upperCandidates.Add(upperBound, this.factQuery.UpperBoundsAsExpressions(pc, upperBound, false));
              continue;
            }
            
            // Is there already a tighter candidate ?
            bool uninteresting = false;
            bool equalToCandidate = false;
            foreach (var upperCandidate in upperCandidates)
            {
              // if they (lowerBound and lowerCandidate) are equals, keep both, we will choose later.
              // TODO: Or choose now ?
              if (upperBound.Equals(upperCandidate.Key))
              {
                equalToCandidate = true;
                break;
              }

              foreach (var uninterestingBound in upperCandidate.Value)
              {
                if (upperBound.Equals(uninterestingBound))
                {
                  uninteresting = true;
                  break;
                }
              }
              if (uninteresting)
              {
                break;
              }
            }

            if (uninteresting)
            {
              continue;
            }

            var upperUpperBounds = this.factQuery.UpperBoundsAsExpressions(pc, upperBound, false);

            if (!equalToCandidate)
            {
              // Is it tighter than an existing candidate ?
              foreach (var upperUpperBound in upperUpperBounds)
              {
                if (upperCandidates.ContainsKey(upperUpperBound))
                {
                  upperCandidates.Remove(upperUpperBound);
                }
              }
            } 
            
            upperCandidates.Add(upperBound, upperUpperBounds);
          }

          BoxedExpression choosedLowerBound;
          BoxedExpression choosedUpperBound;
          // mscorlid method 473, SR516: no lower bound because exp (the index) come from an array  ...
          // if (lowerCandidates.Count == 0 || upperCandidates.Count == 0) { throw ... }
          
          // Choose one between possible bounds
          // TODO: prefer the ones based on variables already present in the partition
          // TODO: if not unique, prefer constants
          var enumLowerBounds = lowerCandidates.Keys.GetEnumerator();
          var b = enumLowerBounds.MoveNext();

          if (b)
          {
            choosedLowerBound = enumLowerBounds.Current;
          }
          else
          {
            choosedLowerBound = null;
          }

          var enumUpperBounds = upperCandidates.Keys.GetEnumerator();
          b = enumUpperBounds.MoveNext();

          if (b)
          {
            choosedUpperBound = enumUpperBounds.Current;
          }
          else
          {
            choosedUpperBound = null;
          }

          var result = new Pair<BoxedExpression, BoxedExpression>(choosedLowerBound, choosedUpperBound);

          return result;
        }

        #endregion

        #region OfInterest2

        /// <summary>
        /// Hypothesis: Bound Analysis assumed that <code>0 &le; len </code> and 
        /// </summary>
        /// <param name="dest"></param>
        private SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleAllocations(APC pc, Variable dest, Variable len, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var lenAsExp = BoxedExpression.For(this.Context.Refine(pc, len), this.Decoder.Outdecoder);
          lenAsExp = this.Decoder.Stripped(lenAsExp);
          var destAsExp = BoxedExpression.For(this.Context.Refine(pc, dest), this.Decoder.Outdecoder);

          ALog.BeginTransferFunction(StringClosure.For("[P]Allocation"),
            ExpressionPrinter.ToStringClosure(lenAsExp, this.Decoder), PrettyPrintPC(pc), StringClosure.For(data));

          var refinedDomain = data.Allocation(new BoxedVariable<Variable>(dest), lenAsExp);

          ALog.EndTransferFunction(StringClosure.For(data));

          return refinedDomain;
        }

        /// <summary>
        /// Hypothesis: Bound Analysis assumed.
        /// </summary>
        /// <param name="dest"></param>
        private SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleArrayAssignment(
          string name, APC pc, 
          Variable array, Variable index, 
          SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var arrayAsExp = BoxedExpression.For(this.Context.Refine(pc, array), this.Decoder.Outdecoder);
          var indexAsExp = BoxedExpression.For(this.Context.Refine(pc, index), this.Decoder.Outdecoder);

          ALog.BeginTransferFunction(StringClosure.For("[P]ArrayAssignement"),
            ExpressionPrinter.ToStringClosure(arrayAsExp, this.Decoder), PrettyPrintPC(pc), StringClosure.For(data));

          var baseVariables = this.Decoder.VariablesIn(indexAsExp);

          Log("VARIABLESIN={0}", baseVariables);
          // TODO: decide wich variables you want in the partition : probably here a variable inside. You must gatherKnowledge for each variable inside, and reconstruct the bounds of interest.
          // eg. index: sv1; index is (sv2+1); indexKnowledge is (0,sv3); so (sv2+1) go from 1 to sv3+1 (but could be unnecessary if sv2 is already into the partition ...;
          var notWanted = new Set<BoxedVariable<Variable>>();

          if (baseVariables.Count > 1 || !baseVariables.Contains(ToBoxedVariable(index)))
          {
            notWanted.Add(ToBoxedVariable(index));
          }

          var knowledge = this.Encoder.ConstantFor(true);
          var equalityKnowledge = this.Encoder.ConstantFor(true);

          foreach (var e in baseVariables)
          {
            knowledge = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, 
              ExpressionOperator.And, 
              knowledge, 
              gatherKnowledge(this.Context.Post(pc), ToBoxedExpression(this.Context.Post(pc), e)));
          }

          if (!notWanted.IsEmpty)
          {
            // NOTE : even if notWanted will not be part of the split partition used, it can already be present in the current partition.
            // This is why we provide the information, to be used in simplify.
            PolynomialOfInt<BoxedVariable<Variable>, BoxedExpression> indexAsPoly;
            if (PolynomialOfInt<BoxedVariable<Variable>, BoxedExpression>.TryToPolynomialForm(indexAsExp, this.Decoder, out indexAsPoly))
            {
              equalityKnowledge = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, 
                ExpressionOperator.Equal, 
                indexAsExp, 
                PolynomialOfInt<BoxedVariable<Variable>, BoxedExpression>.ToPureExpressionForm(indexAsPoly, this.Encoder));

              knowledge = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.And, knowledge, equalityKnowledge);
            }
          }

          //var indexKnowledge = gatherKnowledge(pc, indexAsExp);

          foreach (var e in baseVariables)
          {
            var indexRange = guessRange(pc, ToBoxedExpression(pc, e)); // carefull : One or Two can be null.
            Log("LB({1})={0}", indexRange.One, e);
            Log("UB({1})={0}", indexRange.Two, e);
          }
          var indexRange2 = guessRange(pc, indexAsExp); // carefull : One or Two can be null.
          Log("LB({1})={0}", indexRange2.One, indexAsExp);
          Log("UB({1})={0}", indexRange2.Two, indexAsExp);

          var refinedDomain = data.ArrayAssignment(ToBoxedVariable(array), indexAsExp, knowledge, notWanted); //,notWanted

          ALog.EndTransferFunction(StringClosure.For(data));

          return refinedDomain;
        }


        private SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> HandleIndexAssignment(string name, APC pc, 
          BoxedVariable<Variable> array, BoxedVariable<Variable> index, 
          BoxedExpression assignedAsExp, BoxedExpression assignmentKnowledge, 
          SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          ALog.BeginTransferFunction(StringClosure.For("[P]IndexAssignement"),
            StringClosure.For(""), PrettyPrintPC(pc), StringClosure.For(data));

          //var baseVariables = this.Decoder.VariablesIn(indexAsExp);
          var baseVariables = this.Decoder.VariablesIn(assignedAsExp);
          
          Log("VARIABLESIN={0}", baseVariables);

          var indexAsExp = ToBoxedExpression(pc, index);

          var indexKnowledge = gatherKnowledge(this.Context.Post(pc), indexAsExp); //TODO: post is not useful here
          
          var knowledge = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, 
            ExpressionOperator.And, 
            indexKnowledge, 
            assignmentKnowledge);

          // TODO : useless if indexAsExp is based on variables already in the partition (which space is delimited)
          //var indexRange = guessRange(pc, indexAsExp);
          //Log("LB={0}", indexRange.One);
          //Log("UB={0}", indexRange.Two);

          IPartitionAbstraction<BoxedVariable<Variable>, BoxedExpression> arrayPartition;
          var refinedDomain = data;

          if (data.TryGetValue(array, out arrayPartition))
          {
            var singles = arrayPartition.Singles();
            Log("SINGLES={0}",singles);

            var b = baseVariables.Remove(index); 
            
            foreach (var single in singles)
            {
              if (!this.Decoder.IsConstant(single))
              {
                var variablesInSingle = this.Decoder.VariablesIn(single);

                if (baseVariables.IsSubset(variablesInSingle))
                {
                  // The general case is very complicated. Here we assume that single has the form of an octagon contraint (+/- y + c)
                  if (baseVariables.Count == 1) 
                  {
                    var indexSubstitute = this.Encoder.Substitute(single, ToBoxedExpression(pc, baseVariables.PickAnElement()), indexAsExp);
                    
                    var notWanted = new Set<BoxedVariable<Variable>>(baseVariables);
                  
                    if (!indexSubstitute.IsVariable) // HACK due to handling of expression
                    {
                      notWanted.Add(this.Decoder.UnderlyingVariable(indexSubstitute));
                    }
                    
                    refinedDomain = refinedDomain.ArrayAssignment(array, indexSubstitute, knowledge, notWanted);
                  } 
                  else // Otherwise we try to partition wrt indexAsExp
                  {
                    refinedDomain = refinedDomain.ArrayAssignment(array, indexAsExp, knowledge, baseVariables);
                  }
                }
              }
            }
          }
          else
          {
            throw new AbstractInterpretationException();
          }

          ALog.EndTransferFunction(StringClosure.For(data));

          return refinedDomain;
        }

        #endregion

        #region MSILVisitor Members

        // TODO : useless 
        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldnull(APC pc, Variable dest, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }
        
        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }

        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          // TODO: what is isOld for ?
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }

        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldsfld(APC pc, Field field, bool @volatile, Variable dest, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }

        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }

        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Castclass(APC pc, Type type, Variable dest, Variable obj, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }

          return data;
        }

        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            var fullNameForMethod = this.DecoderForMetaData.FullName(method);
            Variable arrayLengthVar;

            if (fullNameForMethod.StartsWith("System.Runtime.CompilerServices.RuntimeHelpers.InitializeArray"))
            {
              if (this.Context.TryGetArrayLength(this.Context.Post(pc), args[0], out arrayLengthVar))
              {
                var arrayLengthExp = BoxedExpression.For(this.Context.Refine(pc, arrayLengthVar), this.Decoder.Outdecoder);
                arrayLengthExp = this.Decoder.Stripped(arrayLengthExp);
                int n;

                if (arrayLengthExp.IsConstantInt(out n))
                {
                  var refinedDomain = data;
                  // var arrayAsExp = BoxedExpression.For(this.Context.Refine(pc, args[0]), this.Decoder.Outdecoder);

                  var array = ToBoxedVariable(args[0]);

                  var knowledge = this.Encoder.ConstantFor(true);

                  for (int i = 0; i < n; i++)
                  {
                    //refinedDomain = HandleArrayAssignment("InitializeArray", pc, args[0], i, refinedDomain);
                    refinedDomain = refinedDomain.ArrayAssignment(array, this.Encoder.ConstantFor(i), knowledge, new Set<BoxedVariable<Variable>>());
                  }

                  return refinedDomain;
                }
                else
                {
                  // TODO: probably nothing
                }
              }

              Debug.Assert(false, "ArrayLength of an array must be known");
            }

            //if (isArray(pc, dest))
            //{
            return GenericArrayHandling(pc, dest, data);
            //}

          }

          return data;
        }

        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Isinst(APC pc, Type type, Variable dest, Variable obj, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, dest))
          {
            return GenericArrayHandling(pc, dest, data);
          }
          
          return data;
        }

        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          // TODO: case Neg and Not
          Log("Unary:{0}",op);
 	        return data;
        }

        private BoxedExpression RecursiveStripped(BoxedExpression exp)
        {
          while (exp.IsUnary)
          {
            exp = this.Decoder.Stripped(exp);
          }
          
          if (exp.IsBinary)
          {
            var left = RecursiveStripped(exp.BinaryLeft);
            var right = RecursiveStripped(exp.BinaryRight);
            
            BoxedExpression expStripped;
            if (TryRepackAssignment(exp.BinaryOp, left, right, out expStripped))
            {
              return expStripped;
            }
            //else
            //{
            //}
          }
          
          return exp;
        } 

        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          Log("Binary:{0}", op);
          if (OperatorExtensions.IsComparisonBinaryOperator(op))
          {
            // TODO: if s1 or s2 includes an array term, partitioning must be done.
            return data;
          }
          else if (OperatorExtensions.IsBooleanBinaryOperator(op))
          {
            // TODO: see above
            return data;
          }
          else
          {
            var destAsExp = ToBoxedExpression(pc, dest);
            var leftAsExp = ToBoxedExpressionWithConstantRefinement(pc, s1);
            var rightAsExp = ToBoxedExpressionWithConstantRefinement(pc, s2);

            ALog.BeginTransferFunction(StringClosure.For("Assign"),
              StringClosure.For("{0} := {1}({2}, {3})",
                ExpressionPrinter.ToStringClosure(destAsExp, this.Decoder), StringClosure.For(op),
                ExpressionPrinter.ToStringClosure(leftAsExp, this.Decoder), ExpressionPrinter.ToStringClosure(rightAsExp, this.Decoder)),
                PrettyPrintPC(pc), StringClosure.For(data));

            // Hypothesis : if it is a reminder or division operation, we assume that the dividend is not zero
            var refinedDomain = data;
            BoxedExpression rightOfAssignment;

            Log("REPACK:{1} {0} {2}", op, s1, s2);

            if (TryRepackAssignment(op, leftAsExp, rightAsExp, out rightOfAssignment))
            {
              var arraysToRefine = data.arraysWithPartitionDefinedOnASubsetExpressionOf(rightOfAssignment);

              if (!arraysToRefine.IsEmpty)
              {
                var assignmentKnowledge = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, destAsExp, rightOfAssignment);
                var assignmentStrippedKnowledge = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, destAsExp, RecursiveStripped(rightOfAssignment));
                assignmentKnowledge = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.And, assignmentKnowledge, assignmentStrippedKnowledge);

                //Log("KNOWLEDGEonREPACK:{0}", rightOfAssignment);

                foreach (var array in arraysToRefine)
                {
                  refinedDomain = HandleIndexAssignment("", pc, 
                    array, 
                    ToBoxedVariable(dest), 
                    rightOfAssignment, 
                    assignmentKnowledge, refinedDomain);
                }
              }
            }
            //else
            //{
            //  throw new AbstractInterpretationException(); //TODO : leave the else.
            //}

            ALog.EndTransferFunction(StringClosure.For(refinedDomain));

            return refinedDomain;
          }

        }

        #region Arrays

        /// <summary>
        /// 
        /// </summary>
        private SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> GenericArrayHandling(APC pc, Variable array, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          Variable arrayLengthVar;

          if (this.Context.TryGetArrayLength(this.Context.Post(pc), array, out arrayLengthVar))
          {
            // if a symbolic value is assigned twice, thanks to symbolic values semantics, its equivalent to nop. 
            // So we keep the partition such as it is.
            if (!data.ContainsKey(ToBoxedVariable(array)))
            {
              return HandleAllocations(pc, array, arrayLengthVar, data);
            }

            return data;
          }

          Debug.Assert(false, "ArrayLength of an array must be known");

          return data;
        }
        
        /// <summary>
        /// We allocate a new partition to the array
        /// </summary>
        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList lengths, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
         if (lengths.Count == 1)
          {
            return HandleAllocations(pc, dest, lengths[0], data);
          }
          else
          {
            return data; // TODO multidimensional array
          }
        }

        public override SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Stelem(APC pc, Type type, Variable array, Variable index, Variable value, SimplePartitionAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (isOneDimensionalArray(pc, array))
          {
            bool wasTop = data.IsTop;

            Log("STELEM={0}[{1}]", array, index);
            var result = HandleArrayAssignment("Stelem", pc, array, index, data);

            Debug.Assert(wasTop || !result.IsTop);

            return result;
          }

          return data;
        }

        #endregion

        #region Containers

        #endregion

        #region Iterators
        
        #endregion

        #endregion

        private void Log(string format, params object[] args)
        {
          if(this.Options.LogOptions.TracePartitionAnalysis)
          {
            Console.WriteLine(format, args);
          }
        }
      }

    }
  }
}
#endif