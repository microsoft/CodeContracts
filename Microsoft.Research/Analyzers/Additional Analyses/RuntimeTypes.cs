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

using System.Diagnostics.Contracts;
using Microsoft.Research.AbstractDomains;
using System.Collections.Generic;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains.Numerical;


namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// Analysis that determines how an expressions refines to an array
      /// </summary>
      [ContractVerification(true)]
      public class RuntimeTypesPlugIn :
        GenericPlugInAnalysisForComposedAnalysis
      {

        #region Constructor

        public RuntimeTypesPlugIn(int id,
          string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          ILogOptions options,
          Predicate<APC> cachePCs
        )
          : base(id, methodName, mdriver, new PlugInAnalysisOptions(options), cachePCs)
        {
          Contract.Requires(id >= 0);
          Contract.Requires(mdriver != null);
        }
        
        #endregion

        #region Overridden
        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> InitialState
        {
          get { return new ADictionary(this.ExpressionManager); }
        }

        public override IFactQuery<BoxedExpression,Variable> FactQuery(IFixpointInfo<APC,ArrayState> fixpoint)
        {
          return new RuntimeTypesFactQuery(this, fixpoint);
        }

        public override ArrayState.AdditionalStates Kind
        {
          get { return ArrayState.AdditionalStates.RuntimeTypes; }
        }

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> AssignInParallel(
          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, 
          Converter<BoxedVariable<Variable>, BoxedExpression> convert, List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>> equalities, 
          ArrayState state)
        {
          Contract.Assume(state != null, "Problem with the extractor of CC");
          Contract.Assume(refinedMap != null, "Problem with the extractor of CC");
          Contract.Assume(convert != null, "Problem with the extractor of CC");

          var renamed = Select(state);

          renamed.AssignInParallel(refinedMap, convert);

          return renamed;
        }

        #endregion

        #region Transfer functions

        public override ArrayState Assume(APC pc, string tag, Variable source, object provenance, ArrayState data)
        {
          var md = this.MethodDriver;
          var cond = BoxedExpression.Convert(md.Context.ExpressionContext.Refine(pc, source), md.ExpressionDecoder).Simplify(md.MetaDataDecoder);

          Type type;
          Variable var;
          if (cond != null)
          {
            // normalize assume(false) !(...)
            if (tag == "false" && cond.IsUnary && cond.UnaryOp == UnaryOperator.Not)
            {
              cond = cond.UnaryArgument;
            }

            if (cond.IsAssumeIsAType(out var, out type))
            {
              return data.UpdatePluginAt(this.Id, Select(data).Update(var, type));
            }

            Variable left, right;
            if(cond.IsBinary && cond.BinaryOp == BinaryOperator.Cobjeq & cond.BinaryLeft.TryGetFrameworkVariable(out left) && cond.BinaryRight.TryGetFrameworkVariable(out right))
            {
              var runtimeTypes = Select(data);
              var boxedLeft = new BoxedVariable<Variable>(left);
              var boxedRight = new BoxedVariable<Variable>(right);
              Type t;
              if(runtimeTypes.TryGetValue(left, out t))
              {
                runtimeTypes[boxedRight] = t;
              }
              if(runtimeTypes.TryGetValue(right, out t))
              {
                runtimeTypes[boxedLeft] = t;
              }

              return data.UpdatePluginAt(this.Id, runtimeTypes);
            }
          }

          return base.Assume(pc, tag, source, provenance, data);
        }
     
        public override ArrayState Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, 
          TypeList extraVarargs, Variable dest, ArgList args, ArrayState data)
        {
          Contract.Assume(data != null);

          var mdd = this.DecoderForMetaData;

          var methodname = mdd.Name(method);
          Contract.Assert(methodname != null);
          var containingType = mdd.DeclaringType(method);
          var containingTypeName = mdd.Name(containingType);
          var nsName = mdd.Namespace(containingType);

          if (nsName != null /* && methodname != null */
            && nsName.Equals("System") && methodname.Equals("GetTypeFromHandle"))
          {
            Type runtimeType;
            if (Context.ValueContext.IsRuntimeType(this.Context.MethodContext.CFG.Post(pc), dest, out runtimeType))  // Get the type
            {
              return data.UpdatePluginAt(this.Id, Select(data).Update(dest, runtimeType));
            }

            return data;
          }

          if(nsName != null && nsName.Equals("System") && methodname.Equals("get_GenericTypeArguments"))
          {
            var runtimeTypes = Select(data);
            Contract.Assume(args.Count > 0);
            var key = args[0];

            Type t;
            if(runtimeTypes.TryGetValue(key, out t))
            {
              IIndexable<Type> typeList; Variable len;
              if (this.DecoderForMetaData.IsGeneric(t, out typeList, false) && this.MethodDriver.Context.ValueContext.TryGetArrayLength(pc.Post(), dest, out len))
              {
                // assume lenght == typeList.Count + 1                
                var range = DisInterval.For(typeList.Count+1);
                data.Numerical.AssumeInDisInterval(new BoxedVariable<Variable>(len), range); // unfortunately with side-effects ...
              }
            }

            return data;
          }

          if (nsName != null && containingTypeName != null
            && nsName.Equals("System") && containingTypeName.Equals("Enum"))
          {
            Type type;
            if (args.Count == 2 && methodname == "IsDefined" && Select(data).TryGetValue(args[0], out type))
            {
              var enumvalues = new List<int>();
              if (mdd.TryGetEnumValues(type, out enumvalues))
              {
                Variable unboxed;

                // We check if it is the right integer
                if (this.Context.ValueContext.TryUnbox(pc, args[1], out unboxed))
                {
                  var bounds = data.Numerical.BoundsFor(ToBoxedExpression(pc, unboxed));
                  var enumValues = DisInterval.For(enumvalues.ConvertAll(y => Interval.For(y)));

                  if (bounds.LessEqual(enumValues))
                  {
                    var guard = ToBoxedExpression(pc, dest);
                    if (guard == null) { return data; }
                    var newNumerical = data.Numerical.TestTrue(guard) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

                    Contract.Assume(newNumerical != null);
                    
                    return data.UpdateNumerical(newNumerical);
                  }
                  else if (bounds.Meet(enumValues).IsBottom)
                  {
                    var exp = ToBoxedExpression(pc, dest);
                    if (exp == null) return data;

                    var newNumerical = data.Numerical.TestFalse(exp) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

                    Contract.Assume(newNumerical != null);

                    return data.UpdateNumerical(newNumerical);
                  }
                }
                // Otherwise we check if it is used by name
                object value;
                Type typeValue;
                if (this.Context.ValueContext.IsConstant(pc, args[1], out typeValue, out value) && this.DecoderForMetaData.System_String.Equals(typeValue))
                {
                  List<string> enumFields;
                  if (this.DecoderForMetaData.TryGetEnumFields(type, out enumFields))
                  {
                    var enumFieldName = value as string;
                    Contract.Assume(enumFieldName != null);
                    if (enumFields.Contains(enumFieldName))
                    {
                      var exp = ToBoxedExpression(pc, dest);
                      if (exp == null) return data;
                      var newNumerical = data.Numerical.TestTrue(exp) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

                      Contract.Assume(newNumerical != null);

                      return data.UpdateNumerical(newNumerical);
                    }
                    else
                    {
                      var exp = ToBoxedExpression(pc, dest);
                      if(exp == null) return data;
                      var newNumerical = data.Numerical.TestFalse(exp) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

                      Contract.Assume(newNumerical != null);

                      return data.UpdateNumerical(newNumerical);
                    }
                  }
                }
              }
            }
          }
          return data;
        }

        #endregion

        #region Select 

        private ADictionary Select(ArrayState state)
        {
          Contract.Requires(state != null);
          Contract.Ensures(Contract.Result<ADictionary>() != null);

          Contract.Assume(-1 <= this.Id , "assuming the global invariant");
          Contract.Assume(this.Id < state.PluginsCount, "assuming the global invariant");

          var result = state.PluginAbstractStateAt(this.Id) as ADictionary;

          Contract.Assume(result != null, "Wrong type!!!");

          return result;
        }

        #endregion

        #region Abstract domain

        [ContractVerification(true)]
        class ADictionary 
          : FunctionalAbstractDomainEnvironment<ADictionary, BoxedVariable<Variable>, FlatAbstractDomain<Type>, BoxedVariable<Variable>, BoxedExpression>
        {
          #region Constructors

          public ADictionary(
            ExpressionManager<BoxedVariable<Variable>, BoxedExpression> expManager)
            : base(expManager)
          {
            Contract.Requires(expManager != null);
          }

          private ADictionary(ADictionary other)
            : base(other)
          {
            Contract.Requires(other != null);
          }

          #endregion

          #region Updating

          public ADictionary Update(Variable v, Type t)
          {
            var result = this.Clone() as ADictionary;

            Contract.Assume(result != null, "We miss the dynamic type static analysis");

            result[new BoxedVariable<Variable>(v)] = new FlatAbstractDomain<Type>(t);

            return result;
          }

          #endregion

          #region TryGetValue

          public bool TryGetValue(Variable key, out Type type)
          {
            FlatAbstractDomain<Type> t;
            if (this.TryGetValue(new BoxedVariable<Variable>(key), out t))
            {
              type = t.BoxedElement;
              return true;
            }

            type = default(Type);
            return false;
          }
          #endregion

          #region Overridden
          public override object Clone()
          {
            return new ADictionary(this);
          }

          protected override ADictionary Factory()
          {
            return new ADictionary(this.ExpressionManager);
          }

          public override List<BoxedVariable<Variable>> Variables
          {
            get 
            {
              return new List<BoxedVariable<Variable>>(this.Keys); 
            }
          }

          public override void Assign(BoxedExpression x, BoxedExpression exp)
          {
            //
          }

          public override void ProjectVariable(BoxedVariable<Variable> var)
          {
            //
          }

          public override void RemoveVariable(BoxedVariable<Variable> var)
          {
            //
          }

          public override void RenameVariable(BoxedVariable<Variable> OldName, BoxedVariable<Variable> NewName)
          {
            //
          }

          public override ADictionary TestTrue(BoxedExpression guard)
          {
            return this;
          }

          public override ADictionary TestFalse(BoxedExpression guard)
          {
            return this;
          }

          public override FlatAbstractDomain<bool> CheckIfHolds(BoxedExpression exp)
          {
            return CheckOutcome.Top;
          }

          public override void AssignInParallel(
            Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> sourcesToTargets, 
            Converter<BoxedVariable<Variable>, BoxedExpression> convert)
          {
            if (this.Count != 0)
            {
              var renamed = this.Factory();

              foreach (var pair in this.Elements)
              {
                FList<BoxedVariable<Variable>> renamings;
                if (pair.Value.IsNormal() && sourcesToTargets.TryGetValue(pair.Key, out renamings))
                {
                  foreach (var newName in renamings.GetEnumerable())
                  {
                    renamed[newName] = pair.Value;
                  }
                }
              }

              this.CopyAndTransferOwnership(renamed);
            }
          }

          public override string ToString()
          {
            if (this.IsTop)
            {
              return "empty (runtime types)";
            }

            return base.ToString();
          }

          #endregion
        }

        #endregion

        #region FactQuery

        public class RuntimeTypesFactQuery
           : IFactQuery<BoxedExpression, Variable>
        {
          #region Object invariant
          [ContractInvariantMethod]
          [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
          private void ObjectInvariant()
          {
            Contract.Invariant(this.analysis != null);
            Contract.Invariant(this.fixpoint != null);
          }
          #endregion

          #region State

          private readonly RuntimeTypesPlugIn analysis;
          private readonly IFixpointInfo<APC, ArrayState> fixpoint;
          
          #endregion

          public RuntimeTypesFactQuery(RuntimeTypesPlugIn analysis, IFixpointInfo<APC, ArrayState> fixpoint)
          {
            Contract.Requires(analysis != null);
            Contract.Requires(fixpoint != null);

            this.analysis = analysis;
            this.fixpoint = fixpoint;
          }

          public ProofOutcome IsNull(APC pc, BoxedExpression expr)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsTrue(APC pc, BoxedExpression condition)
          {
            Variable v;
            Type t;
            if (condition.IsAssumeIsAType(out v, out t))
            {
              ArrayState preState;
              if (this.fixpoint.PreState(pc, out preState))
              {
                var state = preState.PluginAbstractStateAt(analysis.Id) as ADictionary;
                Contract.Assume(state != null);
                Type knownType;
                if (state.TryGetValue(v, out knownType) && knownType.Equals(t))
                {
                  return ProofOutcome.True;
                }
              }
              else
              {
                return ProofOutcome.Bottom;
              }
            }
            return ProofOutcome.Top;
          }            

          public ProofOutcome IsTrue(APC pc, Question question)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsTrueImply(APC pc, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
          {
            return ProofOutcome.Top;
          }

          public ProofOutcome HaveSameFloatType(APC pc, BoxedExpression left, BoxedExpression right)
          {
            return ProofOutcome.Top;
          }

          public bool TryGetFloatType(APC pc, BoxedExpression exp, out ConcreteFloat type)
          {
            type = default(ConcreteFloat);
            return false;
          }

          public IEnumerable<Variable> LowerBounds(APC pc, BoxedExpression exp, bool strict)
          {
            yield break;
          }

          public IEnumerable<Variable> UpperBounds(APC pc, BoxedExpression exp, bool strict)
          {
            yield break;
          }

          public IEnumerable<BoxedExpression> LowerBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
          {
            yield break;
          }

          public IEnumerable<BoxedExpression> UpperBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
          {
            yield break;
          }

          public Pair<long, long> BoundsFor(APC pc, BoxedExpression exp)
          {
            return new Pair<long, long>(long.MinValue, long.MaxValue);
          }

          public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object type)
          {
            return ProofOutcome.Top;
          }

          public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables = null, bool replaceVarsWithAccessPaths=true)
          {
            return FList<BoxedExpression>.Empty;
          }

          public ProofOutcome IsNull(APC pc, Variable value)
          {
            return ProofOutcome.Top;          
          }

          public ProofOutcome IsNonNull(APC pc, Variable value)
          {
            return ProofOutcome.Top;
          }

          public bool IsUnreachable(APC pc)
          {
            return false;

          }
        }
        #endregion
      }
    }
  }
}