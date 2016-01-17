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

 // #define EXPERIMENTAL

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis.Inference;

namespace Microsoft.Research.CodeAnalysis
{
  using Provenance = IEnumerable<ProofObligation>;
using System.IO;

  public static partial class Analyzers
  {
    public class NonNull : IMethodAnalysis
    {
      class Options : OptionParsing
      {
        ILogOptions frameworkOptions;

        public Options(ILogOptions frameworkOptions)
        {
          this.frameworkOptions = frameworkOptions;
        }

        #region Overrides
        protected override bool ParseUnknown(string option, string[] args, ref int index, string optionEqualsArgument)
        {
          return false;
        }
        protected override bool UseDashOptionPrefix
        {
          get
          {
            return false;
          }
        }
        #endregion

        #region Options
        [OptionDescription("Don't generate proof obligations")]
        public bool noObl = false;


        public bool NoImplicitProofObligations
        {
          get { return this.noObl; }
        }

        #endregion
      }

      Options options;
      IEnumerable<OptionParsing> IMethodAnalysis.Options { get { return options == null ? Enumerable.Empty<OptionParsing>() : new OptionParsing[] { options }; } }

      /// <summary>
      /// We should write our analyses to be generic over the underlying representation of code. This nested static class
      /// simply binds the type parameters common to all interior classes.
      /// </summary>
      public /* Made public so that it can be used in iterator analysis. */
      static class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
      {
        /// <summary>
        /// Defines the type of abstract state we work over. We use a struct wrapper here to abbreviate the underlying type instance
        ///
        /// [sv -> SV] means sv is strictly less numerically than sv' for all sv' in SV
        /// </summary>
        public struct Domain
        {
          public readonly SetDomain<Variable> NonNulls; // the set of definitely non-null variables
          public readonly SetDomain<Variable> Nulls; // the set of definitely null variables
          public readonly SetDomain<Variable> NonNullIfBoxed; // set of variables that if boxed produce non-null

          public Domain(SetDomain<Variable> nonNulls, SetDomain<Variable> nulls, SetDomain<Variable> nonnullIfBoxed)
          {
            this.NonNulls = nonNulls;
            this.Nulls = nulls;
            this.NonNullIfBoxed = nonnullIfBoxed;
          }

          public static readonly Domain Bottom = new Domain(SetDomain<Variable>.BottomValue, SetDomain<Variable>.BottomValue, SetDomain<Variable>.BottomValue); // Thread-safe

          public bool IsBottom { get { return this.NonNulls.IsBottom || this.Nulls.IsBottom; } }

          public bool IsNonNull(Variable v)
          {
            if (this.NonNulls.IsBottom) return true;
            return this.NonNulls.Contains(v);
          }
          public bool IsNull(Variable v)
          {
            if (this.Nulls.IsBottom) return true;
            return this.Nulls.Contains(v);
          }
          public bool IsNonNullIfBoxed(Variable v)
          {
            if (this.NonNullIfBoxed.IsBottom) return true;
            return this.NonNullIfBoxed.Contains(v);
          }

          override public string ToString()
          {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            sw.WriteLine("Nulls: ");
            // sw.WriteLine("<");
            this.Nulls.Dump(sw);
            // sw.WriteLine(">");
            sw.WriteLine("Non-Nulls: ");
            //sw.WriteLine("<");
            this.NonNulls.Dump(sw);
            //sw.WriteLine(">");
            sw.WriteLine("Non-Null-If-Boxed: ");
            //sw.WriteLine("<");
            this.NonNullIfBoxed.Dump(sw);
            // sw.WriteLine(">");
            return sw.ToString();
          }
        }

        public class AbstractOperationsImplementationNonNull : IAbstractDomainOperations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Domain>
        {
          #region IAbstractDomainOperations<Variable,Domain> Members

          public bool LookupState(IMethodResult<Variable> mr, APC pc, out Domain astate)
          {
            astate = Bottom;
            Analysis an = mr as Analysis;
            if (an == null)
              return false;

            return an.PreStateLookup(pc, out astate);
          }

          public Domain Join(IMethodResult<Variable> mr, Domain astate1, Domain astate2)
          {
            Analysis an = mr as Analysis;
            if (an == null)
              return Bottom;

            bool bWeaker;
            return an.Join(new Pair<APC, APC>(), astate1, astate2, out bWeaker, false);
          }

          public List<BoxedExpression> ExtractAssertions(
            IMethodResult<Variable> mr,
            Domain astate,
            IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
            IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder)
          {
            Analysis an = mr as Analysis;
            if (an == null)
              return null;

            var exprs = new Set<BoxedExpression>();
            an.ExtractBoxedExpressionFromAbstractDomain(context, astate, exprs, new Dictionary<BoxedExpression, Variable>(), new Set<Tuple<Field, BoxedExpression>>());
            List<BoxedExpression> exprs_list = new List<BoxedExpression>();
            exprs_list.AddRange(exprs);
            return exprs_list;
          }

          private class ErrorUnpackVariable : Exception { }

          public bool AssignInParallel(IMethodResult<Variable> mr, ref Domain astate, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> mapping, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
          {
            Analysis an = mr as Analysis;
            if (an == null)
              return false;

            // Convert to the right type for the dictionnary
            IFunctionalMap<Variable, FList<Variable>> mapping_cast = FunctionalMap<Variable, FList<Variable>>.Empty;
            FList<Variable> unboxed;
            foreach (var row in mapping)
            {
              Variable first;
              try
              {
                if (!row.Key.TryUnpackVariable(out first))
                  throw new ErrorUnpackVariable();

                unboxed = row.Value.Map(boxed =>
                {
                  Variable v = default(Variable);
                  if (boxed.TryUnpackVariable(out v))
                    return v;
                  else
                    throw new ErrorUnpackVariable();
                });
              }
              catch (ErrorUnpackVariable)
              {
                continue; // move to the next row on error
              }
              //Console.WriteLine("Mapping : {0} -> {1}", first, unboxed); // Mic: DEBUG
              mapping_cast = mapping_cast.Add(first, unboxed);
            }
            var dummyEdge = new Pair<APC, APC>();
            astate = an.EdgeConversion(dummyEdge.One, dummyEdge.Two, true, mapping_cast, astate);
            return true;
          }

          public Domain Bottom { get { return Domain.Bottom; } }

          #endregion
        }

        class FactBase : IFactBase<Variable>
        {

          IFixpointInfo<APC, Domain> fixpoint;
          /// <summary>
          /// An analysis likely needs access to a meta data decoder to make sense of types etc.
          /// </summary>
          internal IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;

          public FactBase(
            IFixpointInfo<APC, Domain> fixpoint,
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
          )
          {
            Contract.Requires(fixpoint != null);
            Contract.Requires(mdriver != null);

            this.mdriver = mdriver;
            this.fixpoint = fixpoint;
          }

          #region IFactBase<Variable> Members

          public ProofOutcome ValidateExplicitAssertion(APC pc, Variable value)
          {
            // first check if reachable
            Domain d;
            if (fixpoint.PreState(pc, out d))
            {
              if (d.NonNulls.IsBottom) { return ProofOutcome.Bottom; }
              // refine expression
              Expression expr = this.mdriver.Context.ExpressionContext.Refine(pc, value);

              return this.mdriver.Context.ExpressionContext.Decode<bool, ProofOutcome, ExpressionAssertDischarger>(expr, new ExpressionAssertDischarger(this, pc), true);
            }
            else
            {
              // unreachable
              return ProofOutcome.Bottom;
            }
          }

          public ProofOutcome IsNull(APC pc, Variable variable)
          {
            if (mdriver.Context.ValueContext.IsZero(pc, variable)) return ProofOutcome.True;
            Domain atPC;
            if (!fixpoint.PreState(pc, out atPC) || atPC.NonNulls.IsBottom)
            {
              return ProofOutcome.Bottom;
            }

            if (atPC.IsNonNull(variable)) return ProofOutcome.False;
            if (mdriver.Context.ValueContext.IsZero(pc, variable) || atPC.IsNull(variable)) return ProofOutcome.True;

            return ProofOutcome.Top;
#if false
            Domain value;
            if (fixpoint.PreState(pc, out value) && !value.IsBottom)
            {
              if (value.IsNull(variable)) return ProofOutcome.True;
              if (value.IsNonNull(variable)) return ProofOutcome.False;
              return ProofOutcome.Top;
            }
            return ProofOutcome.Bottom;
#endif
          }

          ProofOutcome IFactBase<Variable>.IsNonNull(APC pc, Variable value)
          {
            return this.ValidateExplicitAssertion(pc, value);
          }

          internal ProofOutcome IsNonNull(APC pc, Variable value)
          {
            Domain atPC;

            if (!fixpoint.PreState(pc, out atPC) || atPC.NonNulls.IsBottom)
            {
              return ProofOutcome.Bottom;
            }

            if (atPC.IsNonNull(value)) return ProofOutcome.True;

            if (mdriver.Context.ValueContext.IsZero(pc, value) || atPC.IsNull(value)) return ProofOutcome.False;

            var type = mdriver.Context.ValueContext.GetType(pc, value);
            if (type.IsNormal && this.mdriver.MetaDataDecoder.IsManagedPointer(type.Value))
            {
              return ProofOutcome.True;
            }
            return ProofOutcome.Top;
          }


          public bool IsUnreachable(APC pc)
          {
            Domain value;
            if (fixpoint.PreState(pc, out value) && !value.IsBottom)
            {
              return false;
            }
            return true;
          }

          private ProofOutcome IsNonNullIfBoxed(APC pc, Variable value)
          {
            Domain atPC;

            if (!fixpoint.PreState(pc, out atPC) || atPC.NonNullIfBoxed.IsBottom)
            {
              return ProofOutcome.Bottom;
            }

            if (atPC.IsNonNullIfBoxed(value)) return ProofOutcome.True;

            return ProofOutcome.Top;
          }



          public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> variables, bool replaceVarsWithAccessPaths = true)
          {
            var result = FList<BoxedExpression>.Empty;

            Domain astate;
            if (variables != null)
            {
              if (this.fixpoint.PreState(pc, out astate))
              {
                foreach (var x in variables.GetEnumerable())
                {
                  var accessPath = this.mdriver.Context.ValueContext.AccessPathList(pc, x, true, true);
                  if (accessPath != null)
                  {
                    if (astate.IsNonNull(x))
                    {
                      result = result.Cons(MakeNotEqualsToNull(BoxedExpression.Var(x, accessPath)));
                    }
                    if (astate.IsNull(x))
                    {
                      result = result.Cons(MakeEqualsToNull(BoxedExpression.Var(x, accessPath)));
                    }
                  }
                }
              }
            }
            else
            {
              return GetPostconditionAsExpression().ToFList();
            }

            return result;
          }


          public IEnumerable<BoxedExpression> GetPostconditionAsExpression()
          {
            if (this.fixpoint != null)
            {
              Domain exitState;
              if (this.fixpoint.PostState(this.mdriver.CFG.NormalExit, out exitState))
              {
                var result = new Set<BoxedExpression>();
                var dummy = new Dictionary<BoxedExpression, Variable>();

                ExtractBoxedExpressionFromAbstractDomain(this.mdriver.Context, exitState, result, dummy);

                foreach (var p in result)
                {
                  yield return p;
                }
              }
            }
          }

          public 
            void ExtractBoxedExpressionFromAbstractDomain(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
            Domain astate, Set<BoxedExpression> postconditions, Dictionary<BoxedExpression, Variable> map)
          {
            var expInPostState
                = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(context, this.mdriver.MetaDataDecoder);

            var dec = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.ValueExpDecoder(context, this.mdriver.MetaDataDecoder);

            if (!astate.NonNulls.IsBottom && !astate.NonNulls.IsTop)
            {
              foreach (var v in astate.NonNulls.Elements)
              {
                // skip vars that are managed pointers
                var vtype = context.ValueContext.GetType(context.MethodContext.CFG.NormalExit, v);
                if (!vtype.IsNormal) continue;
                if (this.mdriver.MetaDataDecoder.IsManagedPointer(vtype.Value)) continue;

                // skip vars that are type parameters unless reference constrained
                if (!this.mdriver.MetaDataDecoder.IsReferenceType(vtype.Value))
                {
                  continue;
                }

                foreach (var ap in context.ValueContext.AccessPaths(context.MethodContext.CFG.NormalExit, v, AccessPathFilter<Method, Type>.FromPostcondition(context.MethodContext.CurrentMethod, this.mdriver.MetaDataDecoder.ReturnType(context.MethodContext.CurrentMethod))))
                {
                  if (ap == null)
                    continue;

                  if (ap.Length() > 2)
                  {
                    var be = MakeNotEqualsToNull(BoxedExpression.Var(ap, ap, vtype.Value));
                    postconditions.Add(be);
                    //map[v] = be;
                    map[be] = v;
                  }
                }

              }
            }

            if (!astate.Nulls.IsBottom && !astate.Nulls.IsTop)
            {
              foreach (var v in astate.Nulls.Elements)
              {
                // skip vars that are managed pointers
                var vtype = context.ValueContext.GetType(this.mdriver.Context.MethodContext.CFG.NormalExit, v);
                if (!vtype.IsNormal) continue;
                if (this.mdriver.MetaDataDecoder.IsManagedPointer(vtype.Value)) continue;

                // skip vars that are type parameters unless reference constrained
                if (!this.mdriver.MetaDataDecoder.IsReferenceType(vtype.Value))
                {
                  continue;
                }

                foreach (var ap in context.ValueContext.AccessPaths(context.MethodContext.CFG.NormalExit, v, AccessPathFilter<Method, Type>.FromPostcondition(context.MethodContext.CurrentMethod, this.mdriver.MetaDataDecoder.ReturnType(context.MethodContext.CurrentMethod))))
                {
                  if (ap == null)
                    continue;

                  if (ap.Length() > 2)
                  {
                    var be = MakeEqualsToNull(BoxedExpression.Var(ap, ap, vtype.Value));
                    postconditions.Add(be);
                    //map[v] = be;
                    map[be] = v;
                  }
                }
              }
            }
          }



          #endregion

          private BoxedExpression MakeNotEqualsToNull(BoxedExpression be)
          {
            return MakeNotEqualsToNull(be, this.mdriver.MetaDataDecoder.System_Object);
          }

          private BoxedExpression MakeNotEqualsToNull(BoxedExpression be, Type type)
          {
            return BoxedExpression.Binary(
                  BinaryOperator.Cne_Un, be, BoxedExpression.Const(null, type, this.mdriver.MetaDataDecoder));
          }

          private BoxedExpression MakeEqualsToNull(BoxedExpression be)
          {
            return MakeEqualsToNull(be, this.mdriver.MetaDataDecoder.System_Object);
          }

          private BoxedExpression MakeEqualsToNull(BoxedExpression be, Type type)
          {
            return BoxedExpression.Binary(BinaryOperator.Ceq, be, BoxedExpression.Const(null, type, this.mdriver.MetaDataDecoder));
          }

          /// <summary>
          /// Given (true, domain), decoding symbol s, means s is non-zero/non-null in domain.
          /// Given (false, domain) decoding symbol s, means s is zero/null in domain
          /// </summary>
          struct ExpressionAssertDischarger : IVisitValueExprIL<Expression, Type, Expression, Variable, bool, ProofOutcome>
          {
            FactBase parent;
            APC pc;

            public ExpressionAssertDischarger(FactBase parent, APC pc)
            {
              this.parent = parent;
              this.pc = pc;
            }

            #region IVisitValueExprIL<APC,Type,Expression,Variable,bool,Unit> Members

            private IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context { get { return this.parent.mdriver.Context; } }

            private ProofOutcome Recurse(bool polarity, Expression exp)
            {
              return Context.ExpressionContext.Decode<bool, ProofOutcome, ExpressionAssertDischarger>(exp, this, polarity);
            }

            public ProofOutcome Binary(Expression orig, BinaryOperator op, Variable dest, Expression s1, Expression s2, bool polarity)
            {
              // Comparison is true/false depending on data.One
              switch (op)
              {
                case BinaryOperator.Cne_Un:
                  // if this operator is true and one of the sides is null, then the other is non-null/true
                  // if this operator is false and one of the sides is null, then the other is also null/false
                  if (Context.ExpressionContext.IsZero(s2))
                  {
                    return Recurse(polarity, s1);
                  }
                  if (Context.ExpressionContext.IsZero(s1))
                  {
                    return Recurse(polarity, s2);
                  }
                  return ProofOutcome.Top;

                case BinaryOperator.Ceq:
                case BinaryOperator.Cobjeq:
                  // if this operator is true and one of the sides is null/zero, the other is also null/zero/false
                  // if this operator is false and one of the sides is null/zero, the other is non-null/non-zero/true
                  if (Context.ExpressionContext.IsZero(s2))
                  {
                    // if s2 is zero, then s1 is also zero, given that data.One is true (equality holds)
                    return Recurse(!polarity, s1);
                  }
                  if (Context.ExpressionContext.IsZero(s1))
                  {
                    // if s1 is zero, then s2 is also zero, given that data.One is true (equality holds)
                    return Recurse(!polarity, s2);
                  }
                  // no info
                  return ProofOutcome.Top;

                default:
                  return this.SymbolicConstant(orig, Context.ExpressionContext.Unrefine(orig), polarity);
              }
            }

            public ProofOutcome Isinst(Expression orig, Type type, Variable dest, Expression obj, bool polarity)
            {
              if (polarity)
              {
                // means that dest must be non-null
                ProofOutcome temp = this.parent.IsNonNull(this.pc, dest);

                if (temp != ProofOutcome.True) return temp;

                // and also that obj is non-null
                return Recurse(true, obj);
              }
              else
              {
                // means that dest must be null, but nothing is known about obj
                return this.parent.IsNull(this.pc, dest);
              }
            }

            private ProofOutcome NotEqualToDefault<T>(object obj)
              where T : IEquatable<T>
            {
              T value = (T)obj;
              if (value.Equals(default(T))) return ProofOutcome.False;
              return ProofOutcome.True;
            }

            private ProofOutcome EqualToDefault<T>(object obj)
              where T : IEquatable<T>
            {
              T value = (T)obj;
              if (value.Equals(default(T))) return ProofOutcome.True;
              return ProofOutcome.False;
            }

            public ProofOutcome Ldconst(Expression orig, object constant, Type type, Variable dest, bool polarity)
            {
              if (polarity)
              {
                // symbol must be true/non-zero/non-null
                if (constant is int) return NotEqualToDefault<int>(constant);
                if (constant is long) return NotEqualToDefault<long>(constant);
                if (constant is float) return NotEqualToDefault<float>(constant);
                if (constant is double) return NotEqualToDefault<double>(constant);
                if (constant is string) return (constant != null) ? ProofOutcome.True : ProofOutcome.False;
                return ProofOutcome.Top;
              }
              else
              {
                // symbol must be false/zero/null
                if (constant is int) return EqualToDefault<int>(constant);
                if (constant is long) return EqualToDefault<long>(constant);
                if (constant is float) return EqualToDefault<float>(constant);
                if (constant is double) return EqualToDefault<double>(constant);
                if (constant is string) return (constant != null) ? ProofOutcome.False : ProofOutcome.True;
                return ProofOutcome.Top;
              }
            }

            public ProofOutcome Ldnull(Expression orig, Variable dest, bool polarity)
            {
              if (polarity)
              {
                // must be true/non-zero/non-null
                return ProofOutcome.False;
              }
              // must be false/zero/null
              return ProofOutcome.True;
            }

            public ProofOutcome Sizeof(Expression orig, Type type, Variable dest, bool polarity)
            {
              // never zero
              if (polarity)
              {
                // must be true/non-zero/non-null
                return ProofOutcome.True;
              }
              // symbol must be false/zero/null
              return ProofOutcome.False;
            }

            public ProofOutcome Unary(Expression orig, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, bool polarity)
            {
              switch (op)
              {
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
                  return Recurse(polarity, source);

                case UnaryOperator.Neg:
                  return Recurse(polarity, source); // polarity does not change with respect to null when negating

                case UnaryOperator.Not:
                  // recurse but flip context truthiness
                  return Recurse(!polarity, source);

                default:
                  // extracting no info
                  return SymbolicConstant(orig, Context.ExpressionContext.Unrefine(orig), polarity);
              }
            }

            public ProofOutcome SymbolicConstant(Expression orig, Variable symbol, bool polarity)
            {
              if (polarity)
              {
                // symbol must be true/non-zero/non-null
                return this.parent.IsNonNull(pc, symbol);
              }
              else
              {
                // symbol must be null
                return this.parent.IsNull(pc, symbol);
              }
            }

            public ProofOutcome Box(Expression orig, Type type, Variable dest, Expression source, bool polarity)
            {
              var sourceVar = Context.ExpressionContext.Unrefine(source);
              if (polarity)
              {
                // must be true/non-zero/non-null. Happens if source is NonNullIfBoxed
                return this.parent.IsNonNullIfBoxed(pc, sourceVar);
              }
              // must be false/zero/null
              return this.parent.IsNonNullIfBoxed(pc, sourceVar).Negate();
            }

            #endregion
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
        public class Analysis :
          MSILVisitor<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Domain, Domain>,
          //IValueAnalysis<APC, Domain, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Domain, Domain>,
          //               Variable,
          //               IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>>,
          IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, Domain, Variable>,
          IMethodResult<Variable>
          //,
          //IFactBase<Variable>
        {

          #region Privates
          /// <summary>
          /// Here we hold the state lookup functions for future reference.
          /// </summary>
          protected IFixpointInfo<APC, Domain> fixpointInfo;

          // Mic: public to be accessible from AbstractOperationsImplementation
          public bool PreStateLookup(APC label, out Domain ifFound) { return this.fixpointInfo.PreState(label, out ifFound); }

          /// <summary>
          /// Context we got from the upcall to Visitor. This lets us find out things about Variables and Expressions
          /// </summary>
          internal IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context { get { return this.mdriver.Context; } }

          /// <summary>
          /// An analysis likely needs access to a meta data decoder to make sense of types etc.
          /// </summary>
          internal IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;

          internal NonNull parent;


          #endregion

          public Analysis(
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable,
            ILogOptions> mdriver,
            NonNull parent,
            Predicate<APC> cachePCs
          )
          {
            this.parent = parent;
            this.mdriver = mdriver;
            this.cachePCs = cachePCs;
          }

          #region IAbstractAnalysis members

          public Domain GetTopValue()
          {
            return new Domain(new SetDomain<Variable>(this.mdriver.KeyNumber), new SetDomain<Variable>(this.mdriver.KeyNumber), new SetDomain<Variable>(this.mdriver.KeyNumber));
          }
          public Domain GetBottomValue()
          {
            return Domain.Bottom;
          }

          public Domain LookupState(APC pc)
          {
            Domain dom;
            if (this.PreStateLookup(pc, out dom))
            {
              return dom;
            }
            return default(Domain);
          }


          public IMethodResult<Variable> ExtractResult()
          {
            return this;
          }

          public IFactQuery<BoxedExpression, Variable> FactQuery()
          {
            return FactQuery(this.fixpointInfo);                   
          }

          public IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, Domain> fixpoint)
          {
            return new SimpleLogicInference<Local, Parameter, Method, Field, Type, Expression, Variable>(
              this.mdriver.ExpressionLayer.Decoder.Context, GetFactBase(fixpoint),
              this.MakeNotEqualsToNull, this.MakeEqualsToNull,
              this.mdriver.ValueLayer.Decoder.IsUnreachable, 
              this.mdriver.MetaDataDecoder.IsReferenceType);
          }

          virtual protected IFactBase<Variable> GetFactBase(IFixpointInfo<APC, Domain> fixpoint)
          {
            return new FactBase(fixpoint, this.mdriver);
          }
          #endregion

          //public int ProofObligationCount { get { return this.proofObligations.Count; } }

          #region IValueAnalysis<APC,Domain,IVisitMSIL<APC,Local,Parameter,Method,Field,Type,Expression,Variable,Domain,Domain>,Variable,Expression,IExpressionContext<APC,Method,Type,Expression,Variable>> Members

          /// <summary>
          /// Here, we return the transfer function. Since we implement this via MSILVisitor, we just return this.
          ///
          /// </summary>
          /// <param name="context">The expression context is an interface we can use to find out more about expressions, such as their type etc.</param>
          /// <returns>The transfer function.</returns>
          public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Domain, Domain> Visitor()
          {
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
          public Domain Join(DataStructures.Pair<APC, APC> edge, Domain newState, Domain prevState, out bool weaker, bool widen)
          {
            bool weaker1, weaker2, weaker3;
            var joinedNonNulls = prevState.NonNulls.Join(newState.NonNulls, out weaker1, widen);
            var joinedNulls = prevState.Nulls.Join(newState.Nulls, out weaker2, widen);
            var joinedNonNullIfBoxed = prevState.NonNullIfBoxed.Join(newState.NonNullIfBoxed, out weaker3, widen);
            weaker = weaker1 || weaker2 || weaker3;
            return new Domain(joinedNonNulls, joinedNulls, joinedNonNullIfBoxed);
          }

          public bool IsBottom(APC pc, Domain state)
          {
            return state.NonNulls.IsBottom;
          }

          public bool IsTop(APC pc, Domain state)
          {
            return state.NonNulls.IsTop;
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
            var tw = stateAndWriter.Two;
            tw.WriteLine("NonNulls: ");
            stateAndWriter.One.NonNulls.Dump(tw);
            tw.WriteLine("Nulls: ");
            stateAndWriter.One.Nulls.Dump(tw);
          }

          /// <summary>
          /// Here's where the actual work is. We get passed a list of pairs (source,targets) representing
          /// the assignments t = source for each t in targets.
          ///
          /// For our domain, if the source is in the non-null set, then we add all the targets to the non-null set.
          /// </summary>
          public Domain EdgeConversion(APC from, APC to, bool isJoin, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap, Domain state)
          {
            if (sourceTargetMap == null) return state;

            if (state.NonNulls.IsBottom) return state;
            if (state.Nulls.IsBottom) return state;

            SetDomain<Variable> originalNonNulls = state.NonNulls;
            SetDomain<Variable> newNonNulls = SetDomain<Variable>.TopValue;
            SetDomain<Variable> originalNulls = state.Nulls;
            SetDomain<Variable> newNulls = SetDomain<Variable>.TopValue;
            SetDomain<Variable> originalNonNullIfBoxed = state.NonNullIfBoxed;
            SetDomain<Variable> newNonNullIfBoxed = SetDomain<Variable>.TopValue;

            foreach (var nonNull in originalNonNulls.Elements)
            {
              var targets = sourceTargetMap[nonNull];
              while (targets != null)
              {
                newNonNulls = newNonNulls.Add(targets.Head);
                targets = targets.Tail;
              }
            }

            foreach (var defNull in originalNulls.Elements)
            {
              var targets = sourceTargetMap[defNull];
              while (targets != null)
              {
                newNulls = newNulls.Add(targets.Head);
                targets = targets.Tail;
              }
            }

            foreach (var nnIB in originalNonNullIfBoxed.Elements)
            {
              var targets = sourceTargetMap[nnIB];
              while (targets != null)
              {
                newNonNullIfBoxed = newNonNullIfBoxed.Add(targets.Head);
                targets = targets.Tail;
              }
            }

            return new Domain(newNonNulls, newNulls, newNonNullIfBoxed);
          }


          /// <summary>
          /// This method is called by the underlying driver of the fixpoint computation. It provides delegates for future lookup
          /// of the abstract state at given pcs.
          /// </summary>
          /// <returns>Return true only if you want the fixpoint computation to eagerly cache each pc state.</returns>
          public Predicate<APC> CacheStates(IFixpointInfo<APC, Domain> fixpointInfo)
          {
            this.fixpointInfo = fixpointInfo;
            return this.cachePCs;
            //return this.proofObligations.PCWithProofObligation;
          }

          #endregion

          internal IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get { return this.mdriver.MetaDataDecoder; } }

          /// <summary>
          /// Our default transfer function is do nothing.
          /// </summary>
          protected override Domain Default(APC pc, Domain data)
          {
            return data;
          }

          internal static Domain AssumeNonNull(Variable dest, Domain domain)
          {
            try
            {
              if (domain.IsBottom) return domain;
              if (domain.Nulls.Contains(dest)) return Domain.Bottom;
              if (!domain.NonNulls.Contains(dest))
              {
                return new Domain(domain.NonNulls.Add(dest), domain.Nulls, domain.NonNullIfBoxed);
              }
              return domain;
            }
            catch(NullReferenceException 
#if DEBUG
              e
#endif
              )
            {
#if DEBUG
              Console.WriteLine("[NonNullAnalysis] Null pointer derenference in AssumeNotNull. Variable: {0}, Domain = {1}, stack trace {2}", dest, domain, e);
#endif
              return domain;
            }
          }

          internal static Domain AssumeNull(Variable dest, Domain domain)
          {
            try
            {
              if (domain.IsBottom) return domain;
              if (domain.NonNulls.Contains(dest)) return Domain.Bottom;
              if (!domain.Nulls.Contains(dest))
              {
                return new Domain(domain.NonNulls, domain.Nulls.Add(dest), domain.NonNullIfBoxed);
              }
              return domain;
            }
            catch (NullReferenceException
#if DEBUG
              e
#endif
)
            {
#if DEBUG
              Console.WriteLine("[NonNullAnalysis] Null pointer derenference in AssumeNull. Variable: {0}, Domain = {1}, stack trace {2}", dest, domain, e);
#endif
              return domain;
            }
          }

          internal static Domain AssumeNonNullIfBoxed(Variable dest, Domain domain)
          {
            try
            {
              if (domain.IsBottom) return domain;
              if (!domain.NonNullIfBoxed.Contains(dest))
              {
                return new Domain(domain.NonNulls, domain.Nulls, domain.NonNullIfBoxed.Add(dest));
              }
              return domain;
            }
            catch (NullReferenceException
#if DEBUG
              e
#endif
)
            {
#if DEBUG
              Console.WriteLine("[NonNullAnalysis] Null pointer derenference in AssumeNonNullIfBoxed. Variable: {0}, Domain = {1}, stack trace {2}", dest, domain, e);
#endif
              return domain;
            }
          }

          /// <summary>
          /// Just assume the expression to extract non-null info. Assertion obligations are handled outside
          /// </summary>
          public override Domain Assert(APC pc, string tag, Variable condition, object objProvenance, Domain data)
          {
            // refine source boolean to an expression
            Expression exp = this.context.ExpressionContext.Refine(pc, condition);
            //var forall = this.mdriver.AsForAllIndexed(pc, condition);

#if EXPERIMENTAL // just to get numbers for the forward object invariants inference, We avoid assuming inferred invariants
            if (tag == "invariant")
            {
              return data;
            }
#endif

            // finally assume the assertion in the post state
            return this.context.ExpressionContext.Decode<Pair<bool, Domain>, Domain, ExpressionAssumeDecoder>(exp, new ExpressionAssumeDecoder(this.context, this.MetaDataDecoder.System_Boolean), new Pair<bool, Domain>(true, data));

          }

          /// <summary>
          /// Try to interpret the assumed expression to extract non-null assumptions
          /// </summary>
          public override Domain Assume(APC pc, string tag, Variable source, object objProvenance, Domain data)
          {
            // refine source boolean to an expression
            var exp = this.context.ExpressionContext.Refine(pc, source);
            //var forall = this.mdriver.AsForAllIndexed(pc, source);

            // F: This below is some code I wanted to use to add a.x == b.x if a == b
#if false
            var be = BoxedExpression.Convert(this.mdriver.Context.ExpressionContext.Refine(pc, source), this.mdriver.ExpressionDecoder, MAXDEPTH: 16);
            BinaryOperator bop;
            BoxedExpression l, r;
            if (be.IsBinaryExpression(out bop, out l, out r) && bop == BinaryOperator.Ceq)
            {
              data = AddFieldEqualities(pc, (Variable)l.UnderlyingVariable, (Variable)r.UnderlyingVariable, data);
            } 
#endif
            
            switch (tag)
            {
              case "false":
                return this.context.ExpressionContext.Decode<Pair<bool, Domain>, Domain, ExpressionAssumeDecoder>(exp, new ExpressionAssumeDecoder(this.context, this.MetaDataDecoder.System_Boolean), new Pair<bool, Domain>(false, data));

              default:
                return this.context.ExpressionContext.Decode<Pair<bool, Domain>, Domain, ExpressionAssumeDecoder>(exp, new ExpressionAssumeDecoder(this.context, this.MetaDataDecoder.System_Boolean), new Pair<bool, Domain>(true, data));
            }
          }

          private Domain AddFieldEqualities(APC pc, Variable left, Variable right, Domain data)
          {
            var md = this.mdriver;
            var mdd = md.MetaDataDecoder;
            var vc = md.Context.ValueContext;
            var type = vc.GetType(pc.Post(), left);
            if (type.IsNormal && mdd.IsReferenceType(type.Value))
            {
              foreach (var field in mdd.Fields(type.Value).AsEnumerable())
              {
                Variable varFieldLeft, varFieldRight;
                if (vc.TryFieldAddress(pc.Post(), left, field, out varFieldLeft) && vc.TryFieldAddress(pc.Post(), right, field, out varFieldRight))
                {
                  Console.WriteLine("should add {0} == {1}", varFieldLeft, varFieldRight);

                  if (data.IsNull(varFieldLeft))
                    return AssumeNull(varFieldRight, data);

                  if (data.IsNull(varFieldRight))
                    return AssumeNull(varFieldLeft, data);

                  if (data.IsNonNull(varFieldLeft))
                    return AssumeNonNull(varFieldRight, data);

                  if (data.IsNonNull(varFieldRight))
                    return AssumeNonNull(varFieldLeft, data);

                }
              }
            }

            return data;
          }

          public override Domain Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, Domain data)
          {
            switch (op)
            {
              // for pointer arithmetic
              case BinaryOperator.Add:
              case BinaryOperator.Add_Ovf:
              case BinaryOperator.Add_Ovf_Un:
              case BinaryOperator.Sub:
              case BinaryOperator.Sub_Ovf:
              case BinaryOperator.Sub_Ovf_Un:
                var s1type = this.context.ValueContext.GetType(pc, s1);
                if (s1type.IsNormal && this.MetaDataDecoder.IsUnmanagedPointer(s1type.Value) && data.IsNonNull(s1))
                {
                  return AssumeNonNull(dest, data);
                }
                else
                {
                  var s2type = this.context.ValueContext.GetType(pc, s2);
                  if (s2type.IsNormal && this.MetaDataDecoder.IsUnmanagedPointer(s2type.Value) && data.IsNonNull(s2))
                  {
                    return AssumeNonNull(dest, data);
                  }
                }
                break;

              // for const prop
              case BinaryOperator.Ceq:
              case BinaryOperator.Cobjeq:
                if (data.IsNull(s1) && data.IsNull(s2))
                {
                  return AssumeNonNull(dest, data);
                }
                if (data.IsNull(s1) && data.IsNonNull(s2) || data.IsNonNull(s1) && data.IsNull(s2))
                {
                  return AssumeNull(dest, data);
                }
                break;

              case BinaryOperator.Cne_Un:
                if (data.IsNull(s1) && data.IsNull(s2))
                {
                  return AssumeNull(dest, data);
                }
                if (data.IsNull(s1) && data.IsNonNull(s2) || data.IsNonNull(s1) && data.IsNull(s2))
                {
                  return AssumeNonNull(dest, data);
                }
                break;

            }
            return data;
          }

          public override Domain Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, Domain data)
          {
            // for pointer casting
            switch (op)
            {
              case UnaryOperator.Conv_i:
              case UnaryOperator.Conv_u:
                if (data.IsNonNull(source))
                {
                  return AssumeNonNull(dest, data);
                }
                break;
            }
            return data;
          }

          bool IsTypeVariable(Type type)
          {
            return this.mdriver.MetaDataDecoder.IsFormalTypeParameter(type) || this.mdriver.MetaDataDecoder.IsMethodFormalTypeParameter(type);
          }

          public override Domain Box(APC pc, Type type, Variable dest, Variable source, Domain data)
          {
            var md = this.mdriver.MetaDataDecoder;
            if (this.IsTypeVariable(type))
            {
              // if we have specialized type info for the argument that indicates it is a reference type
              // then we can use the null status of the argument
              if (md.IsReferenceConstrained(type))
              {
                if (data.IsNonNull(source))
                {
                  var argtype = this.mdriver.Context.ValueContext.GetType(pc, source);
                  if (argtype.IsNormal)
                  {
                    if (this.mdriver.MetaDataDecoder.IsReferenceType(argtype.Value))
                    {
                      return AssumeNonNull(dest, data);
                    }
                  }
                }
                return data; // box !!T may be a no-op if it is instantiated to a reference type
              }
             
              // HACKHACK: Patch for the cases where we have a contract inherited by an interface with generics
              Variable retVar;
              if (pc.InsideEnsuresInMethod && 
                this.mdriver.Context.ValueContext.TryResultValue(pc, out retVar) &&
                source.Equals(retVar) &&
                md.IsReferenceType(md.ReturnType(this.mdriver.CurrentMethod)))
              {
                if (data.IsNonNull(source))
                {
                  return AssumeNonNull(dest, data);
                }
                if (data.IsNull(source))
                {
                  return AssumeNull(dest, data);
                }
              }
            }

            if (this.mdriver.MetaDataDecoder.IsValueConstrained(type))
            {
              // actually, there's an exception if the type is a Nullable and the value is nulled.
              return AssumeNonNull(dest, data);
            }
            // no constraint
            if (data.IsNonNullIfBoxed(source))
            {
              return AssumeNonNull(dest, data);
            }
            return data;
          }

          public override Domain Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, Domain data)
          {
            callSiteCache[pc] = data; // cache for potential old state lookup. (override due to fixpoint)
            if (!MetaDataDecoder.IsStatic(method))
            {
              Variable receiver = args[0];
              return AssumeNonNull(receiver, data);
            }
            else
            {
              if (MetaDataDecoder.IsReferenceType(MetaDataDecoder.ReturnType(method)))
              {
                // handle static properties of type string that are autogenerated.
                if (this.MetaDataDecoder.IsPropertyGetter(method))
                {
                  Type declaringType = this.MetaDataDecoder.DeclaringType(method);
                  foreach (var attr in this.MetaDataDecoder.GetAttributes(declaringType))
                  {
                    var attrType = MetaDataDecoder.AttributeType(attr);
                    var attrName = MetaDataDecoder.Name(attrType);
                    if (attrName == "GeneratedCodeAttribute")
                    {
                      var attrArgs = MetaDataDecoder.PositionalArguments(attr);
                      if (attrArgs.Count > 0)
                      {
                        var str = attrArgs[0] as string;
                        if (str == "System.Resources.Tools.StronglyTypedResourceBuilder" ||
                            str == "Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator" ||
                            str == "MyTemplate"
                           )
                        {
                          // assume non-null return value
                          return AssumeNonNull(dest, data);
                        }
                      }
                    }
                    else if (attrName == "StandardModuleAttribute")
                    {
                      Property property = this.MetaDataDecoder.GetPropertyFromAccessor(method);
                      if (MetaDataDecoder.Name(property) == "Settings")
                      {
                        // VB settings has no special attribute arg on GeneratedCode
                        return AssumeNonNull(dest, data);
                      }
                    }
                  }
                }
              }
            }
            return data;
          }

          public override Domain Castclass(APC pc, Type type, Variable dest, Variable obj, Domain data)
          {
            // if arg is non-null, result is non-null
            if (data.NonNulls.Contains(obj))
            {
              return AssumeNonNull(dest, data);
            }
            return data;
          }

          public override Domain ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Variable dest, ArgList args, Domain data)
          {
            return Call(pc, method, tail, true, extraVarargs, dest, args, data);
          }

          /// <summary>
          /// Note, underlying heap domain does not have any data prior to Entry. Thus
          /// we use the Post pc to lookup info about parameters.
          /// </summary>
          public override Domain Entry(APC pc, Method method, Domain data)
          {
            APC postPC = this.context.MethodContext.CFG.Post(pc);
            Domain result = data;
            IIndexable<Parameter> parameters = this.MetaDataDecoder.Parameters(method);
            Type eventArgsType;
            bool hasEventArgsType = MetaDataDecoder.TryGetSystemType("System.EventArgs", out eventArgsType);
            for (int i = 0; i < parameters.Count; i++)
            {
              Parameter p = parameters[i];
              Type parameterType = this.MetaDataDecoder.ParameterType(p);
              // by ref/out are non-null
              if (this.MetaDataDecoder.IsManagedPointer(parameterType))
              {
                Variable paramValue;
                if (this.context.ValueContext.TryParameterValue(postPC, p, out paramValue))
                {
                  result = AssumeNonNull(paramValue, result);
                }
              }
              else if (i == 0 && parameters.Count == 1 && MetaDataDecoder.IsArray(parameterType) && MetaDataDecoder.IsMain(method) && MetaDataDecoder.IsStatic(method))
              {
                // main method
                Variable paramValue;
                if (this.context.ValueContext.TryParameterValue(postPC, p, out paramValue))
                {
                  result = AssumeNonNull(paramValue, result);
                }
              }
            }
            // special recognition of event handlers
            if (hasEventArgsType && parameters.Count == 2 &&
                MetaDataDecoder.Equal(MetaDataDecoder.System_Object, MetaDataDecoder.ParameterType(parameters[0])) && // sender
                MetaDataDecoder.DerivesFrom(MetaDataDecoder.ParameterType(parameters[1]), eventArgsType)) // event args
            {
              // assume event args non-null
              Variable eventArgsValue;
              if (this.context.ValueContext.TryParameterValue(postPC, parameters[1], out eventArgsValue))
              {
                result = AssumeNonNull(eventArgsValue, result);
              }

            }
            // this parameter
            if (!MetaDataDecoder.IsStatic(method))
            {
              Variable thisValue;
              if (this.context.ValueContext.TryParameterValue(postPC, this.MetaDataDecoder.This(method), out thisValue))
              {
                result = AssumeNonNull(thisValue, result);
              }
            }
            Variable zero;
            if (this.context.ValueContext.TryZero(postPC, out zero))
            {
              result = AssumeNull(zero, result);
            }
            if (this.context.ValueContext.TryNull(postPC, out zero))
            {
              result = AssumeNull(zero, result);
            }
            return result;
          }

          OnDemandMap<APC, Domain> callSiteCache;
          private Predicate<APC> cachePCs;

          public bool TryFindOldState(APC pc, out Domain old)
          {
            // walk the subroutine context to determine if we are in an ensures of a method exit, or at a call-return
            // and grab the correct context pc for that state.
            var scontext = pc.SubroutineContext;
            while (scontext != null)
            {
              var head = scontext.Head;
              if (head.Three.StartsWith("after"))
              {
                // after call or after newObj
                // we are in an ensures around a method call. Find the calling context and the from block, which is the method call
                return callSiteCache.TryGetValue(new APC(head.One, 0, scontext.Tail), out old);
              }
              scontext = scontext.Tail;
            }
            old = default(Domain);
            return false;
          }

          public override Domain Ldstack(APC pc, int offset, Variable dest, Variable source, bool isOld, Domain data)
          {
            if (isOld)
            {
              Domain oldState;
              if (TryFindOldState(pc, out oldState))
              {
                if (oldState.IsNonNull(source))
                {
                  return AssumeNonNull(dest, data);
                }
                if (oldState.IsNull(source))
                {
                  return AssumeNull(dest, data);
                }
              }
            }
            return data;
          }

          public override Domain EndOld(APC pc, APC matchingBegin, Type type, Variable dest, Variable source, Domain data)
          {
            Domain oldState;
            if (TryFindOldState(pc, out oldState))
            {
              if (oldState.IsNonNull(source))
              {
                return AssumeNonNull(dest, data);
              }
              if (oldState.IsNull(source))
              {
                return AssumeNull(dest, data);
              }
            }
            return data;
          }

          public override Domain Isinst(APC pc, Type type, Variable dest, Variable obj, Domain data)
          {
            if (data.IsNonNull(obj))
            {
              var actual = this.context.ValueContext.GetType(pc, obj);
              if (actual.IsNormal && this.mdriver.MetaDataDecoder.DerivesFrom(actual.Value, type))
              {
                return AssumeNonNull(dest, data);
              }
            }
            return data;
          }

          public override Domain Initobj(APC pc, Type type, Variable ptr, Domain data)
          {
              if (this.MetaDataDecoder.IsReferenceConstrained(type))
              {
                  Variable value;
                  if (this.context.ValueContext.TryLoadIndirect(pc, ptr, out value))
                  {
                      return AssumeNull(value, data);
                  }
              }
              return data;
          }

          public override Domain Ldarga(APC pc, Parameter argument, bool isOld, Variable dest, Domain data)
          {
            return AssumeNonNull(dest, data);
          }

          public override Domain Ldconst(APC pc, object constant, Type type, Variable dest, Domain data)
          {
            if (constant is string)
            {
              return AssumeNonNull(dest, data);
            }
            return data;
          }

          /// <summary>
          /// </summary>
          public override Domain Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, Domain data)
          {
            return AssumeNonNull(array, data);
          }

          public override Domain Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, Domain data)
          {
            Domain result = data;
            // from now on, assume it is non-null
            result = AssumeNonNull(array, result);
            // also, dest is non-null
            return AssumeNonNull(dest, result);
          }

          /// <summary>
          /// Whether a method is the movenext method of an enumerator class. This method is typically called
          /// when a special case involving movenext method should be handled. 
          /// </summary>
          /// <param name="method"></param>
          /// <returns></returns>
          bool IsIteratorMethod(Method method)
          {
            string mname = this.mdriver.MetaDataDecoder.Name(method);
            if (mname != "MoveNext") return false;
            return true;
          }

          /// <summary>
          /// Determine whether v.field is this.<>?_this. We need to know this to correctly reason about
          /// the non-nullness of this parameter in an IEnumerable. The translation of the iterator into 
          /// a closure class guarantees <>_?this always holds the this value in the original method, thus
          /// is always non-null. 
          /// </summary>
          /// <param name="pc"></param>
          /// <param name="possibleThis"></param>
          /// <param name="field"></param>
          /// <returns></returns>
          private bool IsAccessToClosureThisField(APC pc, Variable possibleThis, Field field)
          {
            string fieldName = this.mdriver.MetaDataDecoder.Name(field);
            if (fieldName.StartsWith("<>") && fieldName.EndsWith("this") ||
                fieldName == "$VB$Me")
            {
              return true;
            }
            return false;
          }

          /// <summary>
          /// Determine if this field refers to an outer closure
          /// </summary>
          private bool IsOuterClosure(Field f)
          {
            Type fieldType = MetaDataDecoder.FieldType(f);

            if (MetaDataDecoder.IsReferenceType(fieldType) &&
                MetaDataDecoder.IsCompilerGenerated(fieldType))
            {
              return true;
            }
            return false;
          }

          public override Domain Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, Domain data)
          {
            Domain result = data;
            result = AssumeNonNull(obj, result);
            // if the field is a struct and we loaded the entire thing, the result at this level of abstraction
            // is actually the address of the temporary copy. It is thus non-null.
            // We determine this by seeing if the result of a ldfld is a managed reference.
            FlatDomain<Type> type = context.ValueContext.GetType(context.MethodContext.CFG.Post(pc), dest);
            if (type.IsNormal && MetaDataDecoder.IsManagedPointer(type.Value))
            {
              result = AssumeNonNull(dest, result);
            }
            // Hack for the case when we refer to "this" in a closure
            if (IsAccessToClosureThisField(pc, obj, field))
            {
              result = AssumeNonNull(dest, result);
            }
            // Hack for chained closures. The accessed outer closure should be non-null
            if (IsOuterClosure(field))
            {
              result = AssumeNonNull(dest, result);
            }
            if (IsMEFNonNullImportField(field))
            {
              result = AssumeNonNull(dest, result);
            }
            return result;
          }

          private bool IsMEFNonNullImportField(Field field)
          {
            field = this.MetaDataDecoder.Unspecialized(field);
            foreach (var attr in this.MetaDataDecoder.GetAttributes(field))
            {
              var type = this.MetaDataDecoder.AttributeType(attr);
              var name = this.MetaDataDecoder.Name(type);
              if (name == "ImportManyAttribute") return true;
              if (name != "ImportAttribute") continue;
              var arg = this.MetaDataDecoder.NamedArgument("AllowDefault", attr);
              if (arg != null && arg is bool && (bool)arg)
              {
                return false;
              }
              return true;
            }
            return false;
          }

          public override Domain Ldsfld(APC pc, Field field, bool @volatile, Variable dest, Domain data)
          {
            Domain result = data;
            // if the field is a struct and we loaded the entire thing, the result at this level of abstraction
            // is actually the address of the temporary copy. It is thus non-null.
            // We determine this by seeing if the result of a ldfld is a managed reference.
            if (mdriver.MetaDataDecoder.IsStruct(mdriver.MetaDataDecoder.FieldType(field)))
            {
              FlatDomain<Type> type = context.ValueContext.GetType(context.MethodContext.CFG.Post(pc), dest);
              if (type.IsNormal)
              {
                if (MetaDataDecoder.IsManagedPointer(type.Value))
                {
                  result = AssumeNonNull(dest, result);
                }
                else if (mdriver.MetaDataDecoder.IsPrimitive(type.Value) && mdriver.MetaDataDecoder.Name(field) == "Zero")
                {
                  result = AssumeNull(dest, result);
                }
              }
            }
            else
            {
              // if the field is a cached delegate field or other thing we know to be null, don't make assumptions as heap analysis constructs initializing path
              if (mdriver.Context.ValueContext.IsZero(mdriver.CFG.Post(pc), dest))
              {
              }
              else
              {
                // assume all static fields are non-null
                result = AssumeNonNull(dest, result);
              }
            }
            return result;

          }

          public override Domain Ldflda(APC pc, Field field, Variable dest, Variable obj, Domain data)
          {
            Domain result = data;
            // from now on, assume it is non-null
            result = AssumeNonNull(obj, result);
            // dest non-null
            return AssumeNonNull(dest, result);
          }

          public override Domain Ldsflda(APC pc, Field field, Variable dest, Domain data)
          {
            Domain result = data;
            // dest non-null
            return AssumeNonNull(dest, result);
          }

          public override Domain Ldlen(APC pc, Variable dest, Variable array, Domain data)
          {
            Domain result = data;
            // from now on, assume it is non-null
            result = AssumeNonNull(array, result);
            return result;
          }

          public override Domain Ldloca(APC pc, Local local, Variable dest, Domain data)
          {
            return AssumeNonNull(dest, data);
          }

          public override Domain Ldvirtftn(APC pc, Method method, Variable dest, Variable obj, Domain data)
          {
            Domain result = data;
            // from now on, assume it is non-null
            result = AssumeNonNull(obj, result);
            // dest non-null
            return AssumeNonNull(dest, result);
          }

          public override Domain Localloc(APC pc, Variable dest, Variable size, Domain data)
          {
            return AssumeNonNull(dest, data);
          }

          public override Domain Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList lengths, Domain data)
          {
            return AssumeNonNull(dest, data);
          }

          public override Domain Newobj<ArgList>(APC pc, Method ctor, Variable dest, ArgList args, Domain data)
          {
            return AssumeNonNull(dest, data);
          }

          public override Domain Stelem(APC pc, Type type, Variable array, Variable index, Variable value, Domain data)
          {
            Domain result = data;
            // from now on, assume it is non-null
            result = AssumeNonNull(array, result);
            return result;
          }


          public override Domain Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, Domain data)
          {
            Domain result = data;
            // from now on, assume it is non-null
            result = AssumeNonNull(obj, result);
            return result;
          }

          public override Domain Unbox(APC pc, Type type, Variable dest, Variable obj, Domain data)
          {
            Domain result = data;
            // from now on, assume object is non-null
            result = AssumeNonNull(obj, result);
            // result is also non-null (reference into box)
            result = AssumeNonNull(dest, result);
            return result;
          }

          public override Domain Unboxany(APC pc, Type type, Variable dest, Variable obj, Domain data)
          {
              if (this.MetaDataDecoder.IsReferenceType(type))
              {
                  // same as castclass
                  return this.Castclass(pc, type, dest, obj, data);
              }

              Domain result = data;

              if (this.MetaDataDecoder.IsValueConstrained(type))
              {
                  // from now on, assume object is non-null.  
                  result = AssumeNonNull(obj, result);
              }
              result = AssumeNonNullIfBoxed(dest, result);
              return result;
          }

          /// <summary>
          /// Given (true, domain), decoding symbol s, means s is non-zero/non-null in domain.
          /// Given (false, domain) decoding symbol s, means s is zero/null in domain
          /// </summary>
          struct ExpressionAssumeDecoder : IVisitValueExprIL<Expression, Type, Expression, Variable, Pair<bool, Domain>, Domain>
          {
            readonly IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context;
            readonly Type BooleanType;
            public ExpressionAssumeDecoder(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context, Type Boolean)
            {
              this.context = context;
              this.BooleanType = Boolean;
            }

            #region IVisitValueExprIL<APC,Type,Expression,Variable,CheckPointClosure<bool,Domain>,Domain> Members

            private Domain Recurse(Pair<bool, Domain> pair, Expression exp)
            {
              return context.ExpressionContext.Decode<Pair<bool, Domain>, Domain, ExpressionAssumeDecoder>(exp, this, pair);
            }

            public Domain Binary(Expression orig, BinaryOperator op, Variable dest, Expression s1, Expression s2, Pair<bool, Domain> data)
            {
              // Comparison is true/false depending on data.One
              switch (op)
              {
                case BinaryOperator.Cne_Un:
                  if (data.One)
                  {
                    // positive polarity, so !=
                    // if this operator is true and one of the sides is null, then the other is non-null/true
                    if (data.Two.IsNull(this.context.ExpressionContext.Unrefine(s2)) || this.context.ExpressionContext.IsZero(s2))
                    {
                      return Recurse(data, s1);
                    }
                    if (data.Two.IsNull(this.context.ExpressionContext.Unrefine(s1)) || this.context.ExpressionContext.IsZero(s1))
                    {
                      return Recurse(data, s2);
                    }
                  }
                  else
                  {
                    // negative polarity, so ==
                    // if this operator is false and one of the sides is null, then the other is also null/false
                    if (data.Two.IsNull(this.context.ExpressionContext.Unrefine(s2)) || this.context.ExpressionContext.IsZero(s2))
                    {
                      return Recurse(data, s1);
                    }
                    if (data.Two.IsNull(this.context.ExpressionContext.Unrefine(s1)) || this.context.ExpressionContext.IsZero(s1))
                    {
                      return Recurse(data, s2);
                    }
                  }
                  return data.Two;

                case BinaryOperator.Cgt_Un:
                  // if this operator is true and one of the sides is null, then the other is non-null/true
                  if (data.One)
                  { // positive polarity only
                    if (data.Two.IsNull(this.context.ExpressionContext.Unrefine(s2)) || this.context.ExpressionContext.IsZero(s2))
                    {
                      return Recurse(data, s1);
                    }
                    if (data.Two.IsNull(this.context.ExpressionContext.Unrefine(s1)) || this.context.ExpressionContext.IsZero(s1))
                    {
                      return Recurse(data, s2);
                    }
                  }
                  return data.Two;

                case BinaryOperator.Ceq:
                case BinaryOperator.Cobjeq:
                  if (data.One)
                  {
                    // if this operator is true and one of the sides is null/zero, the other is also null/zero/false
                    if (data.Two.IsNull(this.context.ExpressionContext.Unrefine(s2)) || this.context.ExpressionContext.IsZero(s2))
                    {
                      // if s2 is zero, then s1 is also zero, given that data.One is true (equality holds)
                      return Recurse(new Pair<bool, Domain>(false, data.Two), s1);
                    }
                    if (data.Two.IsNull(this.context.ExpressionContext.Unrefine(s1)) || this.context.ExpressionContext.IsZero(s1))
                    {
                      // if s1 is zero, then s2 is also zero, given that data.One is true (equality holds)
                      return Recurse(new Pair<bool, Domain>(false, data.Two), s2);
                    }
                    // if one is non-null and the equality holds, then the other must also be non-null
                    if (data.Two.IsNonNull(this.context.ExpressionContext.Unrefine(s1)))
                    {
                      return AssumeNonNull(this.context.ExpressionContext.Unrefine(s2), data.Two);
                    }
                    // if one is non-null and the equality holds, then the other must also be non-null
                    if (data.Two.IsNonNull(this.context.ExpressionContext.Unrefine(s2)))
                    {
                      return AssumeNonNull(this.context.ExpressionContext.Unrefine(s1), data.Two);
                    }
                  }
                  else
                  {
                    // if this operator is false and one of the sides is null/zero, the other is non-null/non-zero/true
                    if (data.Two.IsNull(this.context.ExpressionContext.Unrefine(s2)) || this.context.ExpressionContext.IsZero(s2))
                    {
                      // if s2 is zero, then s1 is non-zero, given that data.One is false (in-equality holds)
                      return Recurse(new Pair<bool, Domain>(true, data.Two), s1);
                    }
                    if (data.Two.IsNull(this.context.ExpressionContext.Unrefine(s1)) || this.context.ExpressionContext.IsZero(s1))
                    {
                      // if s1 is zero, then s2 is non-zero, given that data.One is false (in-equality holds)
                      return Recurse(new Pair<bool, Domain>(true, data.Two), s2);
                    }
                  }
                  // no info
                  return data.Two;

                case BinaryOperator.And:
                  {
                    if(data.One)
                    {
                      // it is s1 & s2
                      var pc = this.context.ExpressionContext.GetPC(orig);
                      var typeS1 = this.context.ValueContext.GetType(pc, this.context.ExpressionContext.Unrefine(s1));
                      if (typeS1.IsNormal)
                      {
                        var typeS2 = this.context.ValueContext.GetType(pc, this.context.ExpressionContext.Unrefine(s2));
                        if (typeS2.IsNormal && (typeS1.Value.Equals(BooleanType) || typeS2.Value.Equals(BooleanType)))
                        {
                          var left = Recurse(new Pair<bool, Domain>(true, data.Two), s1);
                          return Recurse(new Pair<bool, Domain>(true, left), s2);
                        }
                      }
                    }
                    return data.Two;
                  }


                default:
                  return data.Two;
              }
            }

            public Domain Isinst(Expression orig, Type type, Variable dest, Expression obj, Pair<bool, Domain> data)
            {
              if (data.One)
              {
                // means that dest is non-null
                Domain domain = AssumeNonNull(dest, data.Two);
                // and also that obj is non-null
                return Recurse(new Pair<bool, Domain>(true, domain), obj);
              }
              // no info
              return data.Two;
            }


            public Domain Ldconst(Expression orig, object constant, Type type, Variable dest, Pair<bool, Domain> data)
            {
              // figure out if constant is 0-equivalent
              if (constant is string)
              {
                if(data.One)
                {
                  return AssumeNonNull(dest, data.Two);
                }

                return data.Two; // no info
              }
              IConvertible ic = constant as IConvertible;
              bool isZero = false;
              if (ic != null)
              {
                try
                {
                  int intval = ic.ToInt32(null);
                  isZero = (intval == 0);
                }
                catch
                {
                  return data.Two; // no info
                }
              }
              if (data.One && isZero)
              {
                // supposed to be true/non-zero, but isn't
                // contradiction
                return Domain.Bottom;
              }
              if (!data.One && !isZero)
              {
                // supposed to be false/zero, but isn't
                // contradiction
                return Domain.Bottom;
              }
              // no info
              return data.Two;
            }

            public Domain Ldnull(Expression orig, Variable dest, Pair<bool, Domain> data)
            {
              if (data.One)
              {
                // is true/non-zero/non-null
                // contradiction
                return Domain.Bottom;
              }
              return data.Two;
            }

            public Domain Sizeof(Expression orig, Type type, Variable dest, Pair<bool, Domain> data)
            {
              // no info
              return data.Two;
            }

            public Domain Unary(Expression orig, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Expression source, Pair<bool, Domain> data)
            {
              switch (op)
              {
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
                  return Recurse(data, source);

                case UnaryOperator.Neg:
                  // recurse but dont flip context truthiness
                  return Recurse(data, source);

                case UnaryOperator.Not:
                  // recurse but flip context truthiness
                  return Recurse(new Pair<bool, Domain>(!data.One, data.Two), source);

                default:
                  // extracting no info
                  return data.Two;
              }
            }

            public Domain SymbolicConstant(Expression orig, Variable symbol, Pair<bool, Domain> data)
            {
              if (data.One)
              {
                // symbol is true/non-zero/non-null
                if (this.context.ExpressionContext.IsZero(orig))
                {
                  // contradiction
                  return Domain.Bottom;
                }

                return AssumeNonNull(symbol, data.Two);
              }
              else
              {
                // symbol is false/zero/null
                if (data.Two.NonNulls.Contains(symbol))
                {
                  // contradiction
                  return Domain.Bottom;
                }
                Variable extraWitness;
                if (GetExtraNonNullAssumptionWitness(orig, symbol, out extraWitness))
                {
                  return AssumeNonNull(extraWitness, AssumeNull(symbol, data.Two));
                }

                return AssumeNull(symbol, data.Two);
              }
            }

            private bool GetExtraNonNullAssumptionWitness(Expression orig, Variable symbol, out Variable extraWitness)
            {
              FList<Variable> witness;
              var accessPath = this.context.ValueContext.AccessPathListAndWitness(this.context.ExpressionContext.GetPC(orig), symbol, true, true, out witness);
              if (accessPath != null)
              {
                var last = accessPath.Last();
                if (last.IsMethodCall)
                {
                  if (last.ToString() == "string.IsNullOrEmpty")
                  {
                    witness = witness.Reverse();
                    extraWitness = witness.Tail.Head;
                    return true;
                  }
                  if (last.ToString() == "string.IsNullOrWhiteSpace")
                  {
                    witness = witness.Reverse();
                    extraWitness = witness.Tail.Head;
                    return true;
                  }
                }
              }
              extraWitness = default(Variable);
              return false;
            }

            public Domain Box(Expression pc, Type type, Variable dest, Expression source, Pair<bool, Domain> data)
            {
              var sourceVariable = this.context.ExpressionContext.Unrefine(source);
              if (data.One)
              {
                // box is true/non-zero/non-null. Make source be NonNullIfBoxed
                return AssumeNonNullIfBoxed(sourceVariable, data.Two);
              }
              else
              {
                // box must be null, meaning the sourceVariable cannot be in NonNullIfBoxed
                if (data.Two.IsNonNullIfBoxed(sourceVariable))
                {
                  // contradiction
                  return Domain.Bottom;
                }
                return data.Two;
              }
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
              switch (op)
              {
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

            public Expression Box(Expression orig, Type type, Variable dest, Expression source, Unit data)
            {
              // nothing to skip
              return orig;
            }

            #endregion
          }


          #region IMethodResult<Variable> Members

#if false
          public void ValidateImplicitAssertions(IFactQuery<BoxedExpression, Variable> facts, IOutputResults output) {
            if (this.parent.options.NoImplicitProofObligations) return;
            this.proofObligations.Validate(output, facts);
          }
#endif

          IFactQuery<BoxedExpression, Variable> IMethodResult<Variable>.FactQuery
          {
            get
            {
              return new SimpleLogicInference<Local, Parameter, Method, Field, Type, Expression, Variable>(
                this.context, this.GetFactBase(this.fixpointInfo), this.MakeNotEqualsToNull, this.MakeEqualsToNull,
                this.mdriver.BasicFacts.IsUnreachable,
                this.mdriver.MetaDataDecoder.IsReferenceType);
            }
          }

#if false
          public ProofOutcome ValidateExplicitAssertion(APC pc, Variable value)
          {
            // first check if reachable
            Domain d;
            if (this.PreStateLookup(pc, out d))
            {
              if (d.NonNulls.IsBottom) { return ProofOutcome.Bottom; }
              // refine expression
              Expression expr = this.context.ExpressionContext.Refine(pc, value);

              return this.context.ExpressionContext.Decode<bool, ProofOutcome, ExpressionAssertDischarger>(expr, new ExpressionAssertDischarger(this, pc), true);
            }
            else
            {
              // unreachable
              return ProofOutcome.Bottom;
            }
          }
#endif

          #region Suggest Precondition

          public void SuggestPrecondition(ContractInferenceManager inferenceManager)
          {
            // nothing to suggest at the moment
          }

          #endregion

          #region Suggest Postcondition

          public bool SuggestPostcondition(ContractInferenceManager inferenceManager)
          {
            if (this.fixpointInfo != null)
            {
              return SuggestPostcondition(inferenceManager, this.fixpointInfo);
            }
            return false;
          }

          /// <summary>
          /// We suggest the postcondition only for the returned value
          /// </summary>
          public bool SuggestPostcondition(ContractInferenceManager inferenceManager, IFixpointInfo<APC, Domain> fixpointInfo)
          {
            Contract.Requires(fixpointInfo != null);

            var postconditions = new Set<BoxedExpression>();
            var map = new Dictionary<BoxedExpression, Variable>();
            var nnFields = new Set<Tuple<Field, BoxedExpression>>();
            var isCurrentMethodAProperty = this.MetaDataDecoder.IsPropertyGetterOrSetter(this.context.MethodContext.CurrentMethod);

            var wantWitness = SuggestPostconditionFromReturnValue(fixpointInfo, postconditions, map);

            SuggestPostconditionFromReturnState(fixpointInfo, postconditions, map, nnFields);          

            var reduced = ReducePostConditions(fixpointInfo, postconditions, map, nnFields);

            postconditions = reduced.Item1;
            var reducedNonNullFields = reduced.Item2;

            inferenceManager.PostCondition.AddPostconditions(postconditions);
            inferenceManager.PostCondition.AddNonNullFields(this.mdriver.CurrentMethod, reducedNonNullFields.Select(pair => (object)pair)); // kind of ugly we should manually cast it to objects and then down in the stack re-read them...

            return wantWitness;
          }

          private bool SuggestPostconditionFromReturnValue(
            IFixpointInfo<APC, Domain> fixpointInfo,
            Set<BoxedExpression> postconditions, Dictionary<BoxedExpression, Variable> map)
          {
            Contract.Requires(fixpointInfo != null);

            // saving the existing fixpointinfo. The existing can be null
            // We save it to avoid vagin the IsNonNull & co. method below taks a fixpointInfo
            var savedFixpointInfo = this.fixpointInfo;
            this.fixpointInfo = fixpointInfo;

            var witnessMayBeNeeded = false;

            var md = this.MetaDataDecoder;
            var retType = md.ReturnType(this.context.MethodContext.CurrentMethod);
            Variable retVar;
            if (!md.IsVoid(retType)
              && (md.System_String.Equals(retType) || !md.IsPrimitive(retType))
              && !md.IsStruct(retType)
              && !md.IsVirtual(this.context.MethodContext.CurrentMethod)
              && this.context.ValueContext.TryResultValue(this.context.MethodContext.CFG.NormalExit, out retVar))
            {
              if (this.IsNonNull(this.context.MethodContext.CFG.NormalExit, retVar) == ProofOutcome.True)
              {
                var notNull = MakeNotEqualsToNull(BoxedExpression.Result(retType), retType);

                postconditions.Add(notNull);

                map[notNull] = retVar;

              }
              else if (this.IsNull(this.context.MethodContext.CFG.NormalExit, retVar) == ProofOutcome.True)
              {
                var isNull = MakeEqualsToNull(BoxedExpression.Result(retType), retType);

                postconditions.Add(isNull);

                map[isNull] = retVar;

              }
              else
              {
                witnessMayBeNeeded = true;
              }
            }

            this.fixpointInfo = savedFixpointInfo;

            return witnessMayBeNeeded;
          }


          private void SuggestPostconditionFromReturnState(
            IFixpointInfo<APC, Domain> fixpointInfo, Set<BoxedExpression> postconditions, Dictionary<BoxedExpression, Variable> map, Set<Tuple<Field, BoxedExpression>> nnFields)
          {
            Contract.Requires(fixpointInfo != null);
            Contract.Requires(nnFields != null);

            Domain exitState;

            if (fixpointInfo.PreState(this.context.MethodContext.CFG.NormalExit, out exitState))
            {
              ExtractBoxedExpressionFromAbstractDomain(this.context, exitState, postconditions, map, nnFields);
            }
          }

          private Tuple<Set<BoxedExpression>, IEnumerable<Tuple<Field, BoxedExpression>>> ReducePostConditions(
            IFixpointInfo<APC, Domain> fixpointInfo,
            Set<BoxedExpression> candidatePostconditions,
            Dictionary<BoxedExpression, Variable> map,
            Set<Tuple<Field,BoxedExpression>> nnFields)
          {
            Contract.Requires(fixpointInfo != null);

            Domain top = GetTopValue();
            Domain post;
            if (!fixpointInfo.TryAStateForPostCondition(top, out post))
            {
              post = top;
            }

            var postConditions = new Set<BoxedExpression>();

            IEnumerable<Tuple<Field,BoxedExpression>> resultNonNullFields = nnFields;

            nnFields = null; // to avoid reusing it!

            foreach (var pair in map)
            {
              if (post.IsNull(pair.Value))
              {
                continue;
              }
              if(post.IsNonNull(pair.Value))
              {
                // Try to remove all the nn fields we can prove because postconditions or object invariants
                foreach (var ap in context.ValueContext.AccessPaths(context.MethodContext.CFG.NormalExit, pair.Value, AccessPathFilter<Method, Type>.FromPostcondition(context.MethodContext.CurrentMethod, this.MetaDataDecoder.ReturnType(context.MethodContext.CurrentMethod))))
                {
//                  nnFields = nnFields.Difference(ap.FieldsIn<Field>());
                  var fiedlsInAccessPath = ap.FieldsIn<Field>();
                  resultNonNullFields = resultNonNullFields.Where(tuple => !fiedlsInAccessPath.Contains(tuple.Item1));

                  if(!resultNonNullFields.Any())
                  {
                    break; // no need to continue
                  }
                }

                // Over-approximation: never skip the return value
                if (this.mdriver.AdditionalSyntacticInformation.AssertedPostconditions.Any() || !pair.Key.ContainsReturnValue())
                {
                  continue;
                }
              }
              postConditions.Add(pair.Key);
            }

            return new Tuple<Set<BoxedExpression>, IEnumerable<Tuple<Field, BoxedExpression>>>(postConditions, resultNonNullFields);
          }

          #endregion

          #region Postconditions for backwards propagation

          public IEnumerable<BoxedExpression> GetPostconditionAsExpression()
          {
            if (this.fixpointInfo != null)
            {
              Domain exitState;
              if (this.fixpointInfo.PostState(this.mdriver.CFG.NormalExit, out exitState))
              {
                var result = new Set<BoxedExpression>();
                var dummy = new Dictionary<BoxedExpression, Variable>();
                var dummy2 = new Set<Tuple<Field, BoxedExpression>>();

                ExtractBoxedExpressionFromAbstractDomain(this.context, exitState, result, dummy, dummy2);

                foreach (var p in result)
                {
                  yield return p;
                }
              }
            }
          }

          public /* public to be accessible from class analyses */
          void ExtractBoxedExpressionFromAbstractDomain(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
            Domain astate, Set<BoxedExpression> postconditions, Dictionary<BoxedExpression, Variable> map, Set<Tuple<Field, BoxedExpression>> nonNullFields)
          {
            Contract.Requires(nonNullFields != null);

            /*            var expInPostState
                            = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(context, this.mdriver.MetaDataDecoder);
             */
            var dec = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.ValueExpDecoder(context, this.MetaDataDecoder);
            var md = this.MetaDataDecoder;
            var valueContext = context.ValueContext;
            var normalExit = context.MethodContext.CFG.NormalExit;
            var currMethod = context.MethodContext.CurrentMethod;

            if (!astate.NonNulls.IsBottom && !astate.NonNulls.IsTop)
            {
              foreach (var v in astate.NonNulls.Elements)
              {
                // skip vars that are managed pointers
                var vtype = valueContext.GetType(context.MethodContext.CFG.NormalExit, v);
                if (!vtype.IsNormal) continue;
                if (md.IsManagedPointer(vtype.Value)) continue;

                // skip vars that are type parameters unless reference constrained
                if (!md.IsReferenceType(vtype.Value))
                {
                  continue;
                }

                // skip static variables in System Runtime, as very likely thery are byproducts of 'dynamic'
                if (md.IsInNamespace("System.Runtime.CompilerServices", vtype.Value))
                {
                  continue;
                }

                foreach (var ap in valueContext.AccessPaths(normalExit, v, AccessPathFilter<Method, Type>.FromPostcondition(currMethod, md.ReturnType(currMethod))))
                {
                  if (ap == null)
                    continue;

                  if (ap.Length() > 2 || (ap.Length() == 2 && ap.Tail.Head.IsMethodCall))
                  {
                    var be = MakeNotEqualsToNull(BoxedExpression.Var(ap, ap, vtype.Value));
                    postconditions.Add(be);
                    map[be] = v;

                    if (ap.Head.ToString() == "this")
                    {
                      var f = ap.FieldsIn<Field>();
                      if (f.Count == 1)
                      {
                        nonNullFields.Add(new Tuple<Field,BoxedExpression>(f.PickAnElement(), be));
                      }
                    }
                  }
                }

              }
            }

            if (!astate.Nulls.IsBottom && !astate.Nulls.IsTop)
            {
              foreach (var v in astate.Nulls.Elements)
              {
                // skip vars that are managed pointers
                var vtype = valueContext.GetType(this.context.MethodContext.CFG.NormalExit, v);
                // if (!vtype.IsNormal) continue;

                if(vtype.IsTop)
                {
                  continue;
                }

                if (vtype.IsNormal)
                {
                  if (md.IsManagedPointer(vtype.Value))
                  {
                    continue;
                  }
                  // skip vars that are type parameters unless reference constrained
                  if (!md.IsReferenceType(vtype.Value))
                  {
                    continue;
                  }

                  // skip static variables in System Runtime, as very likely thery are byproducts of dynamic
                  if (md.IsInNamespace("System.Runtime.CompilerServices", vtype.Value))
                  {
                    continue;
                  }
                }

                Contract.Assume(vtype.IsBottom || vtype.IsNormal);

                foreach (var ap in valueContext.AccessPaths(normalExit, v, AccessPathFilter<Method, Type>.FromPostcondition(currMethod, md.ReturnType(currMethod))))
                {
                  if (ap == null)
                    continue;

                  if (ap.Length() > 2)
                  {
                    var be = MakeEqualsToNull(BoxedExpression.Var(ap, ap, vtype.Value));
                    postconditions.Add(be);
                    map[be] = v;
                  }
                }
              }
            }

            #if false // code to suggest e.g. this.x == null, not working yet. I should forge an access path for this.x if it is not materialized
            // experimental
            var mc = this.mdriver.Context.MethodContext;
            var vc = this.mdriver.Context.ValueContext;
            var exitPC = mc.CFG.NormalExit;

            Variable varForThis;
            if (vc.TryParameterAddress(exitPC, md.This(mc.CurrentMethod), out varForThis))
            {
              foreach (var field in md.Fields(md.DeclaringType(this.mdriver.CurrentMethod)))
              {
                Variable varForField;
                if (vc.TryFieldAddress(exitPC, varForThis, field, out varForField))
                {

                }
                else
                {
                  // it can be null???
                }
                /*
                var pathForp = vc.AccessPathList(mc.CFG.NormalExit, varForp, false, false);

                if (!vc.PathUnmodifiedSinceEntry(mc.CFG.NormalExit, pathForp))
                {
                  continue;
                }*/
              }
            }
            #endif
          }

          private BoxedExpression MakeNotEqualsToNull(BoxedExpression be)
          {
            return MakeNotEqualsToNull(be, this.MetaDataDecoder.System_Object);
          }

          private BoxedExpression MakeNotEqualsToNull(BoxedExpression be, Type type)
          {
            return BoxedExpression.Binary(
                  BinaryOperator.Cne_Un, be, BoxedExpression.Const(null, type, MetaDataDecoder));
          }

          private BoxedExpression MakeEqualsToNull(BoxedExpression be)
          {
            return MakeEqualsToNull(be, this.MetaDataDecoder.System_Object);
          }

          private BoxedExpression MakeEqualsToNull(BoxedExpression be, Type type)
          {
            return BoxedExpression.Binary(BinaryOperator.Ceq, be, BoxedExpression.Const(null, type, this.MetaDataDecoder));
          }

          #endregion

          #region Invariant at

          public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> variables, bool replaceVarsWithAccessPaths = true)
          {
            var result = FList<BoxedExpression>.Empty;

            Domain astate;
            if (variables != null)
            {
              if (this.fixpointInfo.PreState(pc, out astate))
              {
                foreach (var x in variables.GetEnumerable())
                {
                  var accessPath = this.context.ValueContext.AccessPathList(pc, x, true, true);
                  if (accessPath != null)
                  {
                    if (astate.IsNonNull(x))
                    {
                      result = result.Cons(MakeNotEqualsToNull(BoxedExpression.Var(x, accessPath)));
                    }
                    if (astate.IsNull(x))
                    {
                      result = result.Cons(MakeEqualsToNull(BoxedExpression.Var(x, accessPath)));
                    }
                  }
                }
              }
            }
            else
            {
              return GetPostconditionAsExpression().ToFList();
            }

            return result;
          }

          #endregion


#if false
          public AnalysisStatistics Statistics() {
            return this.proofObligations.Statistics;
          }
#endif

          #endregion

          #region IFactBase<Label> Members

#if false
          ProofOutcome IFactBase<Variable>.IsNonNull(APC pc, Variable value)
          {
            return this.ValidateExplicitAssertion(pc, value);
          }

#endif

          internal ProofOutcome IsNonNull(APC pc, Variable value)
          {
            Domain atPC;

            if (!PreStateLookup(pc, out atPC) || atPC.NonNulls.IsBottom)
            {
              return ProofOutcome.Bottom;
            }

            if (atPC.IsNonNull(value)) return ProofOutcome.True;

            if (context.ValueContext.IsZero(pc, value) || atPC.IsNull(value)) return ProofOutcome.False;

            var type = context.ValueContext.GetType(pc, value);
            if (type.IsNormal && this.MetaDataDecoder.IsManagedPointer(type.Value))
            {
              return ProofOutcome.True;
            }
            return ProofOutcome.Top;
          }
#if false
          private ProofOutcome IsNonNullIfBoxed(APC pc, Variable value)
          {
            Domain atPC;

            if (!PreStateLookup(pc, out atPC) || atPC.NonNullIfBoxed.IsBottom)
            {
              return ProofOutcome.Bottom;
            }

            if (atPC.IsNonNullIfBoxed(value)) return ProofOutcome.True;

            return ProofOutcome.Top;
          }
#endif
          public ProofOutcome IsNull(APC pc, Variable value)
          {
            if (context.ValueContext.IsZero(pc, value)) return ProofOutcome.True;
            Domain atPC;
            if (!PreStateLookup(pc, out atPC) || atPC.NonNulls.IsBottom)
            {
              return ProofOutcome.Bottom;
            }

            if (atPC.IsNonNull(value)) return ProofOutcome.False;
            if (context.ValueContext.IsZero(pc, value) || atPC.IsNull(value)) return ProofOutcome.True;

            return ProofOutcome.Top;
          }

#if false
          public bool IsUnreachable(APC pc)
          {
            Domain atPC;
            if (!PreStateLookup(pc, out atPC) || atPC.NonNulls.IsBottom)
            {
              return true;
            }
            return false;
          }
#endif
          #endregion

        }


        public class AnalysisForArrays : Analysis
        {
          public AnalysisForArrays(
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
            NonNull parent,
            Predicate<APC> cachePCs
          )
            : base(mdriver, parent, cachePCs)
          {
          }

#if false
          public void ValidateImplicitAssertions(IFactQuery<BoxedExpression,Variable> facts, IOutputResults output, IFixpointInfo<APC, Domain> fixpointInfo)
          {
            var oldfixpoint = this.fixpointInfo;
            this.fixpointInfo = fixpointInfo;

            base.ValidateImplicitAssertions(facts, output);

            this.fixpointInfo = oldfixpoint;
          }

          public ProofOutcome ValidateExplicitAssertion(APC pc, Variable value, IFixpointInfo<APC, Domain> fixpoint)
          {
            var oldfixpoint = this.fixpointInfo;
            this.fixpointInfo = fixpointInfo;

            var result = base.ValidateExplicitAssertion(pc, value);

            this.fixpointInfo = oldfixpoint;

            return result;
          }
#endif

#if true
          protected override IFactBase<Variable> GetFactBase(IFixpointInfo<APC, Domain> fixpointInfo)
          {
            this.fixpointInfo = fixpointInfo;

            return base.GetFactBase(fixpointInfo);
          }
#endif
        }

        internal abstract class ProofObligationBase : ProofObligationBase<BoxedExpression, Variable>
        {
          protected readonly Variable Value;
          protected readonly IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;

          public ProofObligationBase(
            APC pc, Variable var,
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
            Provenance provenance
          )
            : base(pc, mdriver.MetaDataDecoder.Name(mdriver.CurrentMethod), provenance)
          {
            this.Value = var;
            this.mdriver = mdriver;
          }
        }

        internal enum NullOperation { Call, Field, Array, Unbox }

        internal class NonNullProofObligation : ProofObligationBase
        {
          public readonly NullOperation Operation;
          private WeakestPreconditionProver.Path path;
          public NonNullEquivalentObligationsManager EquivalentObligationsManager { get; set; }

          public NonNullProofObligation(
            APC pc, Variable var, NullOperation operation,
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
          )
            : base(pc, var, mdriver, null)
          {
            this.Operation = operation;
            this.EquivalentObligationsManager = new NonNullEquivalentObligationsManager(new List<NonNullProofObligation>() { this });
          }

          public override BoxedExpression Condition
          {
            get
            {
              var pc = this.PCForValidation;
              var exp = mdriver.Context.ExpressionContext.Refine(pc, this.Value);

              // It is importat to internalize the expression, to have an uniform treatement
              var boxedExp = BoxedExpression.Convert(exp, this.mdriver.ExpressionDecoder,
              replaceConstants: false,  // We do not want svX to be replaced by the constant null, as we need it for inference
              accessPaths: (Variable v) => this.mdriver.Context.ValueContext.AccessPathList(this.PC, v, true, false) );

              var condition = BoxedExpression.Binary(
                BinaryOperator.Cne_Un, boxedExp, BoxedExpression.Const(null, default(Type), this.mdriver.MetaDataDecoder));

              return condition;
            }
          }

          public override ProofOutcome? Outcome
          {
            get
            {
              return base.Outcome;
            }
            protected set
            {
              this.EquivalentObligationsManager.NotifyOutCome(value.Value);
              base.Outcome = value;
            }
          }

          protected override ProofOutcome ValidateInternal(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
          {
            var options = output.LogOptions;            
            var pc = this.PCForValidation;

            if (options.TraceChecks)
            {
              Console.WriteLine("Checking {0} @{1}", this.Condition, pc);
            }

            var outcome = query.IsNonNull(pc, this.Value);
            if (outcome == ProofOutcome.Top)
            {
              if (options.UseWeakestPreconditions)
              {
                bool messageAlreadyPrinted;
                if (mdriver.SyntacticComplexity.ShouldAvoidWPComputation(out messageAlreadyPrinted))
                {
                  if (!messageAlreadyPrinted)
                  {
                    output.WriteLine("Skipping backwards computation for this method ({0}) as cccheck thinks it will cause a timeout", mdriver.MetaDataDecoder.Name(mdriver.CurrentMethod));
                  }
                  this.AdditionalInformationOnTheWarning.Add(new WarningContext(WarningContext.ContextType.WPSkippedBecauseAdaptiveAnalysis));
                }
                else
                {
                  WeakestPreconditionProver.AdditionalInfo why;

                  var path = WeakestPreconditionProver.Discharge(pc, this.Value, options.MaxPathSize, this.mdriver, query, inferenceManager, out why);
                  if (path == null)
                  {
                    outcome = ProofOutcome.True;
                  }
                  else
                  {
                    this.path = path;
                    this.AdditionalInformationOnTheWarning.AddRange(why.GetWarningContexts());
                  }
                }
              }
            }

            return outcome;
          }

          private string Message(bool possible, FList<PathElement> accessPath, string additionalMessageAtTheEnd)
          {
            var message = Message(possible, accessPath);

            return String.IsNullOrEmpty(additionalMessageAtTheEnd)
              ? message
              : message + " " + additionalMessageAtTheEnd;
          }

          private string Message(bool possible, FList<PathElement> accessPath)
          {
            Contract.Ensures(Contract.Result<string>() != null);

            string accessPathString = (accessPath != null) ? accessPath.ToCodeString() : null;

            if (accessPathString != null && accessPathString.StartsWith("this.<"))
            {
              var endName = accessPathString.IndexOf('>');
              if (endName > 0)
              {
                // skip numbers and __ and numbers
                var skip = endName + 1;
                var len = accessPathString.Length;
                for (; skip < len; skip++)
                {
                  if (Char.IsDigit(accessPathString[skip])) continue;
                  if (accessPathString[skip] == '_') continue;
                  break;
                }
                accessPathString = accessPathString.Substring(6, endName - 6) + accessPathString.Substring(skip);
              }
            }

            switch (this.Operation)
            {
              case NullOperation.Call:
                if (possible)
                {
                  if (accessPathString != null)
                  {
                    return String.Format("Possibly calling a method on a null reference '{0}'", accessPathString);
                  }
                  else
                  {
                    return "Possibly calling a method on a null reference";
                  }
                }
                else
                {
                  if (accessPathString != null)
                  {
                    return String.Format("Calling a method on a null reference '{0}'", accessPathString);
                  }
                  else
                  {
                    return "Calling a method on a null reference";
                  }
                }

              case NullOperation.Field:
                if (possible)
                {
                  if (accessPathString != null)
                  {
                    return String.Format("Possibly accessing a field on a null reference '{0}'", accessPathString);
                  }
                  else
                  {
                    return "Possibly accessing a field on a null reference";
                  }
                }
                else
                {
                  if (accessPathString != null)
                  {
                    return String.Format("Accessing a field on a null reference '{0}'", accessPathString);
                  }
                  else
                  {
                    return "Accessing a field on a null reference";
                  }
                }

              case NullOperation.Array:
                if (possible)
                {
                  if (accessPathString != null)
                  {
                    return String.Format("Possible use of a null array '{0}'", accessPathString);
                  }
                  else
                  {
                    return "Possible use of a null array";
                  }
                }
                else
                {
                  if (accessPathString != null)
                  {
                    return String.Format("Use of a null array '{0}'", accessPathString);
                  }
                  else
                  {
                    return "Use of a null array";
                  }
                }

              case NullOperation.Unbox:
                if (possible)
                {
                  if (accessPathString != null)
                  {
                    return String.Format("Possibly unboxing a null reference '{0}'", accessPathString);
                  }
                  else
                  {
                    return "Possibly unboxing a null reference";
                  }
                }
                else
                {
                  if (accessPathString != null)
                  {
                    return String.Format("Unboxing a null reference '{0}'", accessPathString);
                  }
                  else
                  {
                    return "Unboxing a null reference";
                  }
                }

              default:
                throw new InvalidOperationException();
            }
          }

          public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
          {
            var additionalMessage = "";
            if (EquivalentObligationsManager.ShouldEmitAdditionalMessage && !EquivalentObligationsManager.AdditionalMessageAlreadyEmitted)
            {
              // Enable it if you want to avoid printing other proof obligations in the equivalence class
#if false
              if (EquivalentObligationsManager.AdditionalMessageAlreadyEmitted)
              {
                return;
              }
#endif
              var others = EquivalentObligationsManager.Count - 1;

              Contract.Assert(others >= 1);
              string post;
              if (others == 1)
              {
                post = "one additional issue in the code)";
              }
              else
              {
                post = string.Format("{0} additional issues in the code)", others);
              }
              additionalMessage = "(Fixing this warning may solve " + post;
            }

            EmitOutcomeInternal(outcome, output, additionalMessage);

            this.EquivalentObligationsManager.NotifyAdditionalMessageEmitted();
          }

          private void EmitOutcomeInternal(ProofOutcome outcome, IOutputResults output, string additionalMessageWhenTop)
          {
            var witness = GetWitness(outcome);
            switch (outcome)
            {
              case ProofOutcome.Top:
                {
                  FList<PathElement> accessPath = this.mdriver.Context.ValueContext.AccessPathList(this.PCForValidation, this.Value, true, true);

                  output.EmitOutcome(witness, AddHintsForTheUser(outcome, Message(true, accessPath, additionalMessageWhenTop)));

                  if (path != null)
                  {
                    var options = output.LogOptions;
                    if (options.ShowPaths)
                    {
                      path.PrintPathInMethod(output, this.PCForValidation.PrimarySourceContext(), this.mdriver);
                    }
                    if (options.ShowUnprovenObligations)
                    {
                      APC pathPC = path.FirstUsablePC;
                      output.WriteLine("{0}: unproven condition: {1}", pathPC.PrimarySourceContext(), path.ObligationStringAtPC(pathPC, this.mdriver.Context, this.mdriver.MetaDataDecoder));
                    }
                  }

                  break;
                }
              case ProofOutcome.False:
                {
                  FList<PathElement> accessPath = this.mdriver.Context.ValueContext.AccessPathList(this.PCForValidation, this.Value, true, true);

                  output.EmitOutcome(witness, AddHintsForTheUser(outcome, Message(false, accessPath)));
                  break;
                }
              case ProofOutcome.Bottom:
                output.EmitOutcomeAndRelated(witness, AddHintsForTheUser(outcome, "reference use unreached"));
                break;

              case ProofOutcome.True:
                output.EmitOutcome(witness, "valid non-null reference ({0})", OperationAsValidString(this.Operation));
                break;
            }
          }

          protected override void PopulateWarningContextInternal(ProofOutcome outcome)
          {
            if (outcome == ProofOutcome.Top)
            {
              var md = this.mdriver;
              var context = md.Context;
              var dec = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.ValueExpDecoder(context, md.MetaDataDecoder);
              var exp = BoxedExpression.For(context.ExpressionContext.Refine(this.PCForValidation, this.Value), dec);

              this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PCForValidation, exp, context, md.MetaDataDecoder.IsBoolean));
            }
          }

          public override Witness GetWitness(ProofOutcome outcome)
          {
            this.PopulateWarningContext(outcome);

            var witness = new Witness(this.ID, WarningType, outcome, this.PC, this.AdditionalInformationOnTheWarning);

            switch (outcome)
            {
              case ProofOutcome.Top:
                {
                  var accessPath = this.mdriver.Context.ValueContext.AccessPathList(this.PCForValidation, this.Value, true, true);

                  witness.AddWarningContext(new WarningContext(WarningContext.ContextType.NonNullAccessPath, accessPath.Length()));
                  break;
                }

              case ProofOutcome.False:
                {
                  var accessPath = this.mdriver.Context.ValueContext.AccessPathList(this.PCForValidation, this.Value, true, true);

                  witness.AddWarningContext(new WarningContext(WarningContext.ContextType.NonNullAccessPath, accessPath.Length()));

                  break;
                }
              case ProofOutcome.Bottom:
              case ProofOutcome.True:
                break;

              default:
                Contract.Assert(false);
                break;
            }

            return witness;
          }

          public override string ObligationName
          {
            get { return "NonNull"; }
          }

          private WarningType WarningType
          {
            get
            {
              switch (this.Operation)
              {
                case NullOperation.Array:
                  return WarningType.NonnullArray;

                case NullOperation.Call:
                  return WarningType.NonnullCall;

                case NullOperation.Field:
                  return WarningType.NonnullField;

                case NullOperation.Unbox:
                default:
                  return WarningType.NonnullUnbox;
              }
            }
          }

          public int Encode()
          {
            return this.Value.GetHashCode();
          }
        }

        internal class NonNullEquivalentObligationsManager
        {
          private List<NonNullProofObligation> equivalentObligations;
          private int topCount;
          private bool additionalMessageEmitted;

          public bool AreAllOutcomesTop { get { return this.topCount == this.equivalentObligations.Count; } }
          public int Count { get { return this.equivalentObligations.Count; } }
          public bool ShouldEmitAdditionalMessage { get { return this.equivalentObligations.Count > 1 && AreAllOutcomesTop; } }
          public bool AdditionalMessageAlreadyEmitted { get { return this.additionalMessageEmitted; } }

          public NonNullEquivalentObligationsManager(List<NonNullProofObligation> equivalentObligations)
          {
            Contract.Requires(equivalentObligations != null);

            this.topCount = 0;
            this.additionalMessageEmitted = false;
            this.equivalentObligations = equivalentObligations;
          }

          public void NotifyOutCome(ProofOutcome outcome)
          {
            if (outcome == ProofOutcome.Top)
              this.topCount++;
          }

          public void NotifyAdditionalMessageEmitted()
          {
            this.additionalMessageEmitted = true;
          }

        }

        private class NonNullCallProofObligation : NonNullProofObligation
        {
          APC prePrecondition;

          public NonNullCallProofObligation(APC pc, APC prePrecondition, Variable pointer,
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
          ) :
            base(pc, pointer, NullOperation.Call, mdriver)
          {
            this.prePrecondition = prePrecondition;
          }

          public override APC PCForValidation
          {
            get
            {
              return prePrecondition;
            }
          }
        }

        static string OperationAsValidString(NullOperation operation)
        {
          switch (operation)
          {
            case NullOperation.Array: return "as array";
            case NullOperation.Unbox: return "in unbox";
            case NullOperation.Call: return "as receiver";
            case NullOperation.Field: return "as field receiver";
            default:
              throw new NotImplementedException();
          }
        }

        internal class NonNullProofObligations : ProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, BoxedExpression, NonNullProofObligation>
        {

          private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;

          public NonNullProofObligations(
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
            bool noImplicitProofObligations
            )
          {
            this.mdriver = mdriver;
            if (noImplicitProofObligations) return;
            this.Run(mdriver.ValueLayer, null);
            this.GroupSimilarProofObligations();
          }

          public override string Name
          {
            get { return "Non-null"; }
          }

          private void AddProofObligation(APC pc, Variable pointer, NullOperation operation)
          {
            if (!this.IgnoreProofObligationAtPC(pc))
            {
              Add(new NonNullProofObligation(pc, pointer, operation, this.mdriver));
            }
          }

          private void AddCallInstProofObligation(APC pc, APC prePreconditions, Variable pointer)
          {
            if (!this.IgnoreProofObligationAtPC(pc))
            {
              Add(new NonNullCallProofObligation(pc, prePreconditions, pointer, this.mdriver));
            }
          }

          private void GroupSimilarProofObligations()
          {
            var buckets = new Dictionary<int, List<NonNullProofObligation>>();
            foreach (var po in this.obligations)
            {
              buckets.Add(po.Encode(), po);
            }

            if (buckets.Count != this.obligations.Count)
            {
              foreach (var pair in buckets)
              {
                if (pair.Value.Count > 1)
                {
                  var manager = new NonNullEquivalentObligationsManager(pair.Value);
                  foreach (var po in pair.Value)
                  {
                    po.EquivalentObligationsManager = manager;
                  }
                }
              }
            }
          }

          /// <summary>
          /// Non-null proof obligation for instance methods are a bit tricky. Because the pre-condition is inlined
          /// just prior to the call instruction, we need to check the non-nullness prior to the pre-conditions. Otherwise
          /// the pre-condition might assume "this" is non-null and no check ensues.
          /// However, it isn't enough to just use the APC in the previous block. We also need the symbol corresponding
          /// to the receiver that is valid at the previous block. Due to renamings, that might not be the same symbol.
          /// To get it, we need to find the stack depth of the receiver, and ask for that on the previous block.
          /// </summary>
          public override bool Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, bool data)
          {
            if (!this.MetaDataDecoder.IsStatic(method))
            {
              Debug.Assert(pc.Index == 0);
              int topOfStack = this.Context.StackContext.StackDepth(pc);
              int receiverIndex = topOfStack - args.Count;

              APC priorToRequires = this.mdriver.CFG.PredecessorPCPriorToRequires(pc);
              Variable receiver;
              if (this.Context.ValueContext.TryStackValue(priorToRequires, receiverIndex, out receiver))
              {
                this.AddCallInstProofObligation(pc, priorToRequires, receiver);
              }
              else
              {
                // can't find proof obligation pc
                Debug.Assert(false);
              }
            }
            return data;
          }

          public override bool Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, bool data)
          {
            this.AddProofObligation(pc, array, NullOperation.Array);
            return data;
          }

          public override bool Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, bool data)
          {
            this.AddProofObligation(pc, array, NullOperation.Array);
            return data;
          }

          public override bool Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, bool data)
          {
            // filter out ldfld obligations if the value on the stack is a struct address
            var type = this.Context.ValueContext.GetType(pc, obj);
            if (type.IsNormal && this.MetaDataDecoder.IsManagedPointer(type.Value)) return data;

            if (type.IsNormal && this.MetaDataDecoder.IsStruct(type.Value)) return data;

            this.AddProofObligation(pc, obj, NullOperation.Field);
            return data;
          }

          public override bool Ldflda(APC pc, Field field, Variable dest, Variable obj, bool data)
          {
            //try
            {
              // filter out ldflda obligations if the value on the stack is a struct address
              var type = this.Context.ValueContext.GetType(pc, obj);
              if (type.IsNormal && this.MetaDataDecoder.IsManagedPointer(type.Value)) return data;

              this.AddProofObligation(pc, obj, NullOperation.Field);
            }
              /*
            catch(Exception e)
            {
              Console.WriteLine("Exception {0}", e.Message);
              Console.WriteLine("Info: PC ={0}, obj = {1}", pc, obj);
            }*/
            return data;
          }

          public override bool Ldlen(APC pc, Variable dest, Variable array, bool data)
          {
            this.AddProofObligation(pc, array, NullOperation.Array);
            return data;
          }

          public override bool Ldvirtftn(APC pc, Method method, Variable dest, Variable obj, bool data)
          {
            this.AddProofObligation(pc, obj, NullOperation.Call);
            return data;
          }

          public override bool Stelem(APC pc, Type type, Variable array, Variable index, Variable value, bool data)
          {
            this.AddProofObligation(pc, array, NullOperation.Array);
            return data;
          }

          public override bool Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, bool data)
          {
            this.AddProofObligation(pc, obj, NullOperation.Field);
            return data;
          }

          public override bool Unbox(APC pc, Type type, Variable dest, Variable obj, bool data)
          {
            this.AddProofObligation(pc, obj, NullOperation.Unbox);
            return data;
          }

          public override bool Unboxany(APC pc, Type type, Variable dest, Variable obj, bool data)
          {
            if (!this.MetaDataDecoder.IsReferenceType(type) && !this.IsNullable(type))
            {
              this.AddProofObligation(pc, obj, NullOperation.Unbox);
            }
            return data;
          }

          private bool IsNullable(Type type)
          {
            type = this.MetaDataDecoder.Unspecialized(type);
            if (this.MetaDataDecoder.FullName(type).StartsWith("System.Nullable`1"))
            {
              return true;
            }
            return false;
          }
        }
      }

      #region IMethodAnalysis Members

      public string Name { get { return "Non-null"; } }

      public bool ObligationsEnabled { get { return !options.noObl; } }

      public IProofObligations<Variable, BoxedExpression> GetProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      (
        string fullMethodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver
      )
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
      {
        // computes proof obligations
        return new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.NonNullProofObligations(driver, options.NoImplicitProofObligations);
      }

#if false // new interface, not implemented yet
      public Result Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result>(
        string fullMethodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        IFactQuery<BoxedExpression, Variable> factQuery,
        IAnalysisConsumer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result> consumer
      )
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
      {
        TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Analysis analysis =
          new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Analysis(driver, this);

        return consumer.CallBack(analysis);
        // driver.HybridLayer.CreateForward(analysis, new DFAOptions { Trace = driver.Options.TraceDFA })(analysis.GetInitialValue(driver.KeyNumber));
      }
#endif
      public IMethodResult<Variable> Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
        string fullMethodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        Predicate<APC> cachePCs,
        IFactQuery<BoxedExpression, Variable> factQuery, DFAController controller
      )
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
      {
        TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Analysis analysis =
          new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Analysis(driver, this, cachePCs);

        driver.HybridLayer.CreateForward(analysis, new DFAOptions { Trace = driver.Options.TraceDFA }, controller)(analysis.GetTopValue());
        return analysis;
      }

      public T Instantiate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T>(
        string fullMethodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        Predicate<APC> cachePCs,
        IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T> factory, DFAController controller
      )
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
      {
        var analysis =
          new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Analysis(driver, this, cachePCs);

        return factory.Create(analysis, controller);
      }

      public void PrintAnalysisSpecificStatistics(IOutput output)
      {
      }

      public bool Initialize(ILogOptions logoptions, string[] args)
      {
        // Check to see if it has already been initialized (e.g. because of multiple occurrences on the command line)
        // If it is, then we skip parsing the new options
        if (this.options == null)
        {
          this.options = new Options(logoptions);
          this.options.Parse(args);
        }

        return !this.options.HasErrors;
      }

      public void PrintOptions(string indent, TextWriter output)
      {
        Options defaultOptions = new Options(null);
        defaultOptions.PrintOptions(indent, output);
        defaultOptions.PrintDerivedOptions(indent, output);
      }

      #endregion


      #region IMethodAnalysis Members

      virtual public void SetDependency(IMethodAnalysis analysis)
      {
        // do nothing
      }

      public bool ExecuteAbstractDomainFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options, Result, Data>(
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options, IMethodResult<Variable>> cdriver,
        IResultsFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result, Data> functor,
        Data data,
        out Result result)
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
        where Options : IFrameworkLogOptions
      {
        var aoi = new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.AbstractOperationsImplementationNonNull();
        return functor.Execute(aoi, data, out result);
      }

      #endregion
    }
  }
}