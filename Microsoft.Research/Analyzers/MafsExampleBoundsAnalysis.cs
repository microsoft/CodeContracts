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
using System.Text;

using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{

  /// <summary>
  /// This example analysis shows how to use the value abstraction provided by the CodeAnalysis infrastructure to 
  /// perform an analysis without having to concern yourself with the intricacies of the MSIL memory model.
  /// At this level, almost all instructions are turned into assumptions that are there for informational purposes.
  /// Branches are not visible, instead Assume statements are visited that constrain the branch condition.
  ///
  /// At edges leading to join points, actual assignments are present as parallel assignments. These are the only state changing
  /// operations from the value analysis point of view.
  ///
  /// This example analysis computes symbolic upper bounds for values to try to discharge the upper bound limit proof obligation
  /// in array accesses. As our abstract domain, we use an EnvironmentDomain mapping Symbolic variables to a set of Symbolic variables representing
  /// upper bounds. The meaning is that if sv -> SV, then sv is less than all sv' in SV.
  /// 
  /// This outermost class should be non-generic and implement the interface IValueAnalysis&lt;AState&gt; to obtain an instance of the analysis.
  /// </summary>
  public class SimpleBoundsAnalysis
  {
    public static void Analyze<Label, Local, Parameter, Method, Field, Type, Expression, Variable>(
      IMethodDriver<Label, Local, Parameter, Method, Field, Type, Expression, Variable> driver,
      bool doDebug
    )
      where Variable : IEquatable<Variable>
    {
      TypeBindings<Label, Local, Parameter, Method, Field, Type, Expression, Variable>.Analysis analysis = 
        new TypeBindings<Label, Local, Parameter, Method, Field, Type, Expression, Variable>.Analysis(driver.MetaDataDecoder);
      analysis.Debug = doDebug;
      driver.CreateForward(analysis, doDebug)(analysis.InitialValue(driver.KeyNumber));
    }

    /// <summary>
    /// We should write our analyses to be generic over the underlying representation of code. This nested static class
    /// simply binds the type parameters common to all interior classes.
    /// </summary>
    static class TypeBindings<Label, Local, Parameter, Method, Field, Type, Expression, Variable>
        where Variable : IEquatable<Variable>
    {
      /// <summary>
      /// Defines the type of abstract state we work over. We use a struct wrapper here to abbreviate the underlying type instance
      ///
      /// [sv -> SV] means sv is strictly less numerically than sv' for all sv' in SV
      /// </summary>
      public struct Domain
      {
        public readonly EnvironmentDomain<Variable, SetDomain<Variable>> Value;
        public Domain(EnvironmentDomain<Variable, SetDomain<Variable>> value)
        {
          this.Value = value;
        }
      }

      /// <summary>
      /// To be a client of a value analysis, implement the IValueAnalysis interface.
      /// We do this here in a nested class instead of the top-level class to take advantage of name abbreviations and to not
      /// expose things at the higher level.
      ///
      /// We inherit the MSILVisitor class to get a default visitor for MSIL where we can just override the few instructions
      /// that we find interesting.
      /// </summary>
      public class Analysis : MSILVisitor<Label, Local, Parameter, Method, Field, Type, Variable, Variable, Domain, Domain>,
        IValueAnalysis<Label, Domain, IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Variable, Variable, Domain, Domain>,
                       Variable,
                       IExpressionContext<Label, Local, Parameter, Method, Type, Expression, Variable>>
      {

        #region Privates
        /// <summary>
        /// Here we hold the state lookup functions for future reference.
        /// </summary>
        StateLookup<Label, Domain> preStateLookup;
        StateLookup<Label, Domain> postStateLookup;

        /// <summary>
        /// Context we got from the upcall to Visitor. This lets us find out things about Variables and Expressions
        /// </summary>
        IExpressionContext<Label, Local, Parameter, Method, Type, Expression, Variable> context;

        /// <summary>
        /// An analysis likely needs access to a meta data decoder to make sense of types etc.
        /// </summary>
        IDecodeMetaData<Local, Parameter, Method, Field, Type> mdDecoder;

        #endregion

        public bool Debug = false;

        public Analysis(IDecodeMetaData<Local, Parameter, Method, Field, Type> mdDecoder)
        {
          this.mdDecoder = mdDecoder;
        }

        public Domain InitialValue(Converter<Variable, int> keyNumber)
        {
          return new Domain(EnvironmentDomain<Variable, SetDomain<Variable>>.TopValue(keyNumber));
        }

        #region IValueAnalysis<Label,Domain,IVisitMSIL<Label,Local,Parameter,Method,Field,Type,Expression,Variable,Domain,Domain>,Variable,Expression,IExpressionContext<Label,Method,Type,Expression,Variable>> Members

        /// <summary>
        /// Here, we return the transfer function. Since we implement this via MSILVisitor, we just return this.
        ///
        /// </summary>
        /// <param name="context">The expression context is an interface we can use to find out more about expressions, such as their type etc.</param>
        /// <returns>The transfer function.</returns>
        public IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Variable, Variable, Domain, Domain> Visitor(IExpressionContext<Label, Local, Parameter, Method, Type, Expression, Variable> context)
        {
          // store away the context for future reference
          this.context = context;
          return this;
        }

        /// <summary>
        /// Must implement the join/widen operation
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="newState"></param>
        /// <param name="prevState"></param>
        /// <param name="weaker">should return false if result is less than or equal prevState.</param>
        /// <param name="widen">true if this is a widen operation. For our domain, this makes no difference</param>
        public Domain Join(DataStructures.Pair<Label, Label> edge, Domain newState, Domain prevState, out bool weaker, bool widen)
        {
          return new Domain(prevState.Value.Join(newState.Value, out weaker, widen));
        }

        public bool IsBottom(Domain state)
        {
          return state.Value.IsBottom;
        }

        public Domain ImmutableVersion(Domain state)
        {
          // our domain is pure
          return state;
        }

        public Domain MutableVersion(Domain state)
        {
          // our domain is pure
          return state;
        }

        public void Dump(Pair<Domain, System.IO.TextWriter> stateAndWriter)
        {
          stateAndWriter.One.Value.Dump(stateAndWriter.Two);
        }

        /// <summary>
        /// Here's where the actual work is. We get passed a list of pairs (source,targets) representing
        /// the assignments t = source for each t in targets.
        ///
        /// For our domain, we thus add new mappings for all targets by looking up the bounds of the source and map the source bounds to new target bounds.
        /// </summary>
        public Domain ParallelAssign(Pair<Label, Label> edge, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap, Domain state)
        {
          EnvironmentDomain<Variable, SetDomain<Variable>> originalState = state.Value;
          EnvironmentDomain<Variable, SetDomain<Variable>> newState = originalState;

          foreach (Variable source in sourceTargetMap.Keys) {

            FList<Variable> targets = sourceTargetMap[source];

            // 1) for each target in this assignment, assign it the same bounds as source.
            // 2) since we also have to map the source bounds, we assign it actually the union of all targets of all bounds of the source.
            SetDomain<Variable> targetBounds = SetDomain<Variable>.TopValue;
            if (originalState.Contains(source)) {
              SetDomain<Variable> originalBounds = originalState[source];
              foreach (Variable origBound in originalBounds.Elements) {
                FList<Variable> targetBoundNames = sourceTargetMap[origBound];
                while (targetBoundNames != null) {
                  targetBounds = targetBounds.Add(targetBoundNames.Head);
                  targetBoundNames = targetBoundNames.Tail;
                }
              }
            }
            if (targetBounds.IsTop) {
              // have no bounds, so havoc all targets
              while (targets != null) {
                newState = newState.Remove(targets.Head);
                targets = targets.Tail;
              }
            }
            else {
              while (targets != null) {
                newState = newState.Add(targets.Head, targetBounds);
                targets = targets.Tail;
              }
            }
          }
          return new Domain(newState);
        }


        /// <summary>
        /// This method is called by the underlying driver of the fixpoint computation. It provides delegates for future lookup
        /// of the abstract state at given pcs.
        /// </summary>
        /// <returns>Return true only if you want the fixpoint computation to eagerly cache each pc state.</returns>
        public bool CacheStates(StateLookup<Label, Domain> preState, StateLookup<Label, Domain> postState)
        {
          this.preStateLookup = preState;
          this.postStateLookup = postState;
          return false;
        }

        #endregion

        /// <summary>
        /// Our default transfer function is do nothing.
        /// </summary>
        protected override Domain Default(Label pc, Domain data)
        {
          return data;
        }

        /// <summary>
        /// The only other interesting case is when we have a constraining assumption.
        /// </summary>
        public override Domain Assume(Label pc, string tag, Variable source, Domain data)
        {
          // refine source boolean to an expression
          Expression exp = this.context.Refine(pc, source);

          switch (tag) {
            case "true":
              return this.context.Decode<Pair<bool, Domain>, Domain, ExpressionAssumeDecoder>(exp, new ExpressionAssumeDecoder(this.context), new Pair<bool, Domain>(true, data));

            case "false":
              return this.context.Decode<Pair<bool, Domain>, Domain, ExpressionAssumeDecoder>(exp, new ExpressionAssumeDecoder(this.context), new Pair<bool, Domain>(false, data));

            default:
              // no refinement
              return data;
          }
        }

        /// <summary>
        /// Here's one place where we want to check the bound. In principle, we want to only record the position and relevant
        /// parameters for a proof obligation and check it only after the fixpoint is computed.
        /// I'm lazy and checking it right away.
        /// </summary>
        public override Domain Ldelem(Label pc, Type type, Variable dest, Variable array, Variable index, Domain data)
        {
          if (data.Value.Contains(index)) {
            SetDomain<Variable> bounds = data.Value[index];
            // lookup upper bound of index and array bound
            Variable bound;
            if (this.context.TryGetArrayLength(pc, array, out bound)) {
              if (Debug) {
                Console.WriteLine("bound for array {0} is {1}", array.ToString(), bound.ToString());
              }
              // check that bound is in bounds of index
              if (!bounds.Contains(bound)) {
                Console.WriteLine("{0}: WARNING: Can't prove that array index is okay", this.context.SourceContext(pc));
              }
              else {
                Console.WriteLine("{0}: INFO: Array index looks good.", this.context.SourceContext(pc));
              }
            }
            else {
              Console.WriteLine("{0}: WARNING: Can't prove that array index is okay", this.context.SourceContext(pc));
            }
          }
          else {
            Console.WriteLine("{0}: WARNING: Can't prove that array index is okay", this.context.SourceContext(pc));
          }
          
          // TODO: might want to constrain index by bound of array to eliminate subsequent errors
          
          return data;
        }
        /// <summary>
        /// Given an expression in an assume, this decoder tries to find if it is a less-than constraint and transforms the
        /// state accordingly.
        /// </summary>
        struct ExpressionAssumeDecoder : IVisitValueExprIL<Expression, Type, Expression, Variable, Pair<bool, Domain>, Domain>
        {
          IExpressionContext<Label, Local, Parameter, Method, Type, Expression, Variable> context;
          public ExpressionAssumeDecoder(IExpressionContext<Label, Local, Parameter, Method, Type, Expression, Variable> context)
          {
            this.context = context;
          }

          /// <summary>
          /// Here we actually add constraints to our state.
          /// </summary>
          private Domain ConstrainLessThan(Expression var, Expression bound, Domain domain)
          {
            // First, skip unary coercions on var and bound
            SkipConversionDecoder skipper = new SkipConversionDecoder();
            var = this.context.Decode<Unit,Expression,SkipConversionDecoder>(var, skipper, Unit.Value);
            bound = this.context.Decode<Unit,Expression,SkipConversionDecoder>(bound, skipper, Unit.Value);

            Variable left = this.context.Unrefine(var);
            Variable right = this.context.Unrefine(bound);
            // add the bound to the set of bounds known
            SetDomain<Variable> currentBounds = 
              (domain.Value.Contains(left)) ? domain.Value[left] : SetDomain<Variable>.TopValue;

            return new Domain(domain.Value.Add(left, currentBounds.Add(right)));
          }

          #region IVisitValueExprIL<Label,Type,Expression,Variable,CheckPointClosure<bool,Domain>,Domain> Members

          public Domain Binary(Expression orig, BinaryOperator op, Variable dest, Expression s1, Expression s2, Pair<bool, Domain> data)
          {
            switch (op) {
              case BinaryOperator.Cgt:
              case BinaryOperator.Cgt_Un:
                // s1 is a bound on s2 provided data.One is true, otherwise we don't have a strict inclusion.
                if (data.One) {
                  return ConstrainLessThan(s2, s1, data.Two);
                }
                return data.Two; // no constraint

              case BinaryOperator.Clt:
              case BinaryOperator.Clt_Un:
                // s2 is a bound on s1, provided that data.One is true, otherwise no strict inclusion
                if (data.One) {
                  return ConstrainLessThan(s1, s2, data.Two);
                }
                return data.Two; // no constraint

              default:
                // no info
                return data.Two;
            }
          }

          public Domain Isinst(Expression orig, Type type, Variable dest, Expression obj, Pair<bool, Domain> data)
          {
            // no info
            return data.Two;
          }

          public Domain Ldconst(Expression orig, object constant, Type type, Variable dest, Pair<bool, Domain> data)
          {
            // no info
            return data.Two;
          }

          public Domain Ldnull(Expression orig, Variable dest, Pair<bool, Domain> data)
          {
            // no info
            return data.Two;
          }

          public Domain Sizeof(Expression orig, Type type, Variable dest, Pair<bool, Domain> data)
          {
            // no info
            return data.Two;
          }

          public Domain Unary(Expression orig, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Pair<bool, Domain> data)
          {
            switch (op) {
              // ignore all coercions
              case UnaryOperator.Conv_i:
              case UnaryOperator.Conv_i1:
              case UnaryOperator.Conv_i2:
              case UnaryOperator.Conv_i4:
              case UnaryOperator.Conv_i8:
              case UnaryOperator.Conv_u:
              case UnaryOperator.Conv_u1:
              case UnaryOperator.Conv_u2:
              case UnaryOperator.Conv_u4:
              case UnaryOperator.Conv_u8:
                // recurse
                return this.context.Decode<Pair<bool, Domain>, Domain, ExpressionAssumeDecoder>(source, this, data);

              case UnaryOperator.Neg:
              case UnaryOperator.Not:
                // recurse but flip context truthiness
                return this.context.Decode<Pair<bool, Domain>, Domain, ExpressionAssumeDecoder>(source, this, new Pair<bool, Domain>(!data.One, data.Two));

              default:
                // extracting no info
                return data.Two;
            }
          }

          public Domain SymbolicConstant(Expression orig, Variable symbol, Pair<bool, Domain> data)
          {
            // no more refined info
            return data.Two;
          }

          #endregion
        }

        /// <summary>
        /// Skips any Unary conversion operators and returns the underlying value, e.g. [conv_u4 exp], returns [exp]
        /// </summary>
        struct SkipConversionDecoder : IVisitValueExprIL<Expression, Type, Expression, Variable, Unit, Expression>
        {
          #region IVisitValueExprIL<Expression,Type,Expression,Variable,Unit,Expression> Members

          public Expression SymbolicConstant(Expression orig, Variable symbol, Unit data)
          {
            // nothing to skip
            return orig;
          }

          public Expression Binary(Expression orig, BinaryOperator op, Variable dest, Expression s1, Expression s2, Unit data)
          {
            // nothing to skip
            return orig;
          }

          public Expression Isinst(Expression orig, Type type, Variable dest, Expression obj, Unit data)
          {
            // nothing to skip
            return orig;
          }

          public Expression Ldconst(Expression orig, object constant, Type type, Variable dest, Unit data)
          {
            // nothing to skip
            return orig;
          }

          public Expression Ldnull(Expression orig, Variable dest, Unit data)
          {
            // nothing to skip
            return orig;
          }

          public Expression Sizeof(Expression orig, Type type, Variable dest, Unit data)
          {
            // nothing to skip
            return orig;
          }

          public Expression Unary(Expression orig, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Unit data)
          {
            switch (op) {
              // ignore all coercions
              case UnaryOperator.Conv_i:
              case UnaryOperator.Conv_i1:
              case UnaryOperator.Conv_i2:
              case UnaryOperator.Conv_i4:
              case UnaryOperator.Conv_i8:
              case UnaryOperator.Conv_u:
              case UnaryOperator.Conv_u1:
              case UnaryOperator.Conv_u2:
              case UnaryOperator.Conv_u4:
              case UnaryOperator.Conv_u8:
                return source; // should we recurse here?

              default:
                return orig;
            }
          }

          #endregion
        }

      }
    }

  }
}
