// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
    using EdgeData = IFunctionalMap<SymbolicValue, FList<SymbolicValue>>;

    public class OldAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions, MethodResult>
      : AnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, LogOptions, MethodResult>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        public OldAnalysisDriver(
          IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> basicDriver
        ) : base(basicDriver)
        {
        }

        public override IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, LogOptions> MethodDriver(
          Method method,
          IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, LogOptions, MethodResult> classDriver,
          bool removeInferredPrecondition = false)
        {
            return new MDriver(method, this, classDriver, removeInferredPrecondition);
        }

        private class MDriver
          : BasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
          , IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, LogOptions>
          , IFactBase<SymbolicValue>
        {
            private OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> optimisticHeapAnalysis;
            private Converter<ExternalExpression<APC, SymbolicValue>, string> expr2String;
            private IFullExpressionDecoder<Type, SymbolicValue, ExternalExpression<APC, SymbolicValue>> expressionDecoder;
            private readonly IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, LogOptions, MethodResult> classDriver;
            private readonly AnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, LogOptions, MethodResult> specializedParent;
            private readonly bool RemoveInferredPreconditions;

            public IDisjunctiveExpressionRefiner<SymbolicValue, BoxedExpression> DisjunctiveExpressionRefiner { get; set; }

            public SyntacticInformation<Method, Field, SymbolicValue> AdditionalSyntacticInformation { get; set; }

            public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, EdgeData>
              ValueLayer
            { get; private set; }

            public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue>, EdgeData>
              ExpressionLayer
            { get; private set; }

            public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, EdgeData>
              HybridLayer
            { get; protected set; }

            public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder
            { get { return this.RawLayer.MetaDataDecoder; } }

            public MDriver(
              Method method,
              OldAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions, MethodResult> parent,
              IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, LogOptions, MethodResult> classDriver,
              bool removeInferredPrecondition
            )
              : base(method, parent)
            {
                specializedParent = parent;
                // Here we build our stack of adapters.
                //
                //  Expr          (expressions)
                //  Heap          (symbolic values)
                //  StackDepth    (Temp decoder for APC)
                //  CFG           (Unit decoder for APC, including Assume)
                //  cciilprovider (Unit decoder for PC)
                //
                // The base class already built the first 3
                // The last two are built on demand

                this.classDriver = classDriver;
                RemoveInferredPreconditions = removeInferredPrecondition;
            }

            public IExpressionContext<Local, Parameter, Method, Field, Type, ExternalExpression<APC, SymbolicValue>, SymbolicValue> Context { get { return this.ExpressionLayer.Decoder.AssumeNotNull().Context; } }

            public Converter<SymbolicValue, int> KeyNumber { get { return SymbolicValue.GetUniqueKey; } }
            public Comparison<SymbolicValue> VariableComparer { get { return SymbolicValue.Compare; } }

            public void RunHeapAndExpressionAnalyses()
            {
                if (optimisticHeapAnalysis != null) return;

                optimisticHeapAnalysis = new OptimisticHeapAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(this.StackLayer, this.Options.TraceEGraph);
                optimisticHeapAnalysis.TurnArgumentExceptionThrowsIntoAssertFalse = this.Options.TurnArgumentExceptionThrowsIntoAssertFalse;
                optimisticHeapAnalysis.IgnoreExplicitAssumptions = this.Options.IgnoreExplicitAssumptions;
                optimisticHeapAnalysis.TraceAssumptions = this.Options.TraceAssumptions;
                var heapsolver = this.StackLayer.CreateForward(optimisticHeapAnalysis,
                                    new DFAOptions { Trace = this.Options.TraceHeapAnalysis });

                heapsolver(optimisticHeapAnalysis.InitialValue());

                this.ValueLayer = CodeLayerFactory.Create(optimisticHeapAnalysis.GetDecoder(this.StackLayer.Decoder),
                                                          this.StackLayer.MetaDataDecoder,
                                                          this.StackLayer.ContractDecoder,
                                                          delegate (SymbolicValue source) { return source.ToString(); },
                                                          delegate (SymbolicValue dest) { return dest.ToString(); },
                                                          (v1, v2) => v1.symbol.GlobalId > v2.symbol.GlobalId
                );

                if (this.PrintIL)
                {
                    Console.WriteLine("-----------------Value based CFG---------------------");
                    this.ValueLayer.Decoder.Context.MethodContext.CFG.Print(Console.Out, this.ValueLayer.Printer, optimisticHeapAnalysis.GetEdgePrinter(),
                              (block) => optimisticHeapAnalysis.GetContexts(block),
                              null);
                }
                var exprAnalysis = new ExpressionAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, SymbolicValue, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, EdgeData>(
                  this.ValueLayer,
                  this.Options,
                  optimisticHeapAnalysis.IsUnreachable
                  );
                var exprsolver = this.ValueLayer.CreateForward(exprAnalysis.CreateExpressionAnalyzer(),
                                                               new DFAOptions { Trace = this.Options.TraceExpressionAnalysis });

                exprsolver(exprAnalysis.InitialValue(SymbolicValue.GetUniqueKey));

                var exprDecoder = exprAnalysis.GetDecoder(this.ValueLayer.Decoder);
                expr2String = ExprPrinter.Printer(exprDecoder.Context, this);

                this.ExpressionLayer = CodeLayerFactory.Create(
                  exprDecoder,
                  this.ValueLayer.MetaDataDecoder,
                  this.ValueLayer.ContractDecoder,
                  expr2String,
                  this.ValueLayer.VariableToString,
                  (v1, v2) => v1.symbol.GlobalId > v2.symbol.GlobalId
                  );


                if (this.PrintIL)
                {
                    Console.WriteLine("-----------------Expression based CFG---------------------");
                    this.ExpressionLayer.Decoder.Context.MethodContext.CFG.Print(Console.Out, this.ExpressionLayer.Printer, exprAnalysis.GetBlockPrinter(expr2String),
                              (block) => exprAnalysis.GetContexts(block),
                              null);
                }

                this.HybridLayer = CodeLayerFactory.Create(
                  this.ValueLayer.Decoder,
                  this.ValueLayer.MetaDataDecoder,
                  this.ValueLayer.ContractDecoder,
                  this.ValueLayer.ExpressionToString,
                  this.ValueLayer.VariableToString,
                  this.ExpressionLayer.Printer,
                  this.ExpressionLayer.NewerThan
                  );

                if (Options.TraceAssumptions)
                {
                    #region Produce trace output to extract implicit assumptions. Please don't remove this code!

                    Console.WriteLine("<assumptions>");
                    Console.WriteLine(@"<subroutine id=""{0}"" methodName=""{1}"">", HybridLayer.Decoder.Context.MethodContext.CFG.Subroutine.Id, HybridLayer.Decoder.Context.MethodContext.CFG.Subroutine.Name);

                    foreach (var b in HybridLayer.Decoder.Context.MethodContext.CFG.Subroutine.Blocks)
                    {
                        Console.WriteLine(@"<block index=""{0}"">", b.Index);

                        foreach (var apc in b.APCs())
                        {
                            Console.WriteLine(@"<apc name=""{0}"" index=""{1}"" ilOffset=""{2}"" primarySourceContext=""{3}"" sourceContext=""{4}"">",
                              apc.ToString(), apc.Index, apc.ILOffset, apc.PrimarySourceContext(), HybridLayer.Decoder.Context.MethodContext.SourceContext(apc));

                            Console.WriteLine("<code>");
                            HybridLayer.Printer(apc, "", Console.Out);
                            Console.WriteLine("</code>");

                            optimisticHeapAnalysis.Dump(apc);

                            Console.WriteLine("</apc>");
                        }

                        Console.WriteLine("</block>");
                    }

                    Console.WriteLine("</subroutine>");
                    Console.WriteLine("</assumptions>");

                    #endregion
                }
            }

            public Converter<ExternalExpression<APC, SymbolicValue>, string> ExpressionToString { get { return expr2String; } }

            public State BackwardTransfer<State, Visitor>(APC from, APC to, State state, Visitor visitor)
              where Visitor : IEdgeVisit<APC, Local, Parameter, Method, Field, Type, SymbolicValue, State>
            {
                if (this.IsUnreachable(to)) { throw new InvalidOperationException("Can not transfer to unreachable point"); }

                if (from.Block != to.Block)
                {
                    var renaming = optimisticHeapAnalysis.BackwardEdgeRenaming(from, to);
                    if (renaming != null)
                    {
                        return visitor.Rename(from, to, state, renaming);
                    }
                    else
                    {
                        return state;
                    }
                }
                return this.ValueLayer.Decoder.ForwardDecode<State, State, Visitor>(to, visitor, state);
            }


            #region IMethodDriver<APC,Local,Parameter,Method,Field,Type,Attribute,Assembly,ExternalExpression<APC,SymbolicValue>,SymbolicValue,LogOptions> Members


            public IFullExpressionDecoder<Type, SymbolicValue, ExternalExpression<APC, SymbolicValue>> ExpressionDecoder
            {
                get
                {
                    if (expressionDecoder == null)
                    {
                        Contract.Assert(this.Context != null);
                        expressionDecoder = ExternalExpressionDecoder.Create(this.MetaDataDecoder, this.Context);
                    }
                    return expressionDecoder;
                }
            }

            public IFactBase<SymbolicValue> BasicFacts { get { return this; } }

            public override bool CanAddRequires()
            {
                return this.CanAddContracts();
            }

            public override bool CanAddEnsures()
            {
                return !this.MetaDataDecoder.IsVirtual(this.Context.MethodContext.CurrentMethod) && this.CanAddContracts();
            }

            protected override bool CanAddInvariants()
            {
                if (classDriver == null)
                    return false;
                return classDriver.CanAddInvariants();
            }

            protected override bool CanAddAssumes()
            {
                return this.Options.InferAssumesForBaseLining;
            }

            private bool CanAddContracts()
            {
                return !this.MetaDataDecoder.IsCompilerGenerated(this.Context.MethodContext.CurrentMethod) &&
                       (!this.MetaDataDecoder.IsVirtual(this.Context.MethodContext.CurrentMethod) ||
                        this.MetaDataDecoder.OverriddenAndImplementedMethods(this.method).AsIndexable(1).Count == 0);
            }

            public void EndAnalysis()
            {
                if (RemoveInferredPreconditions)
                {
                    // we are done validating the inferred preconditions, so we can remove them from the method cache
                    specializedParent.RemoveInferredPrecondition(this.method);
                }
                else
                {
                    specializedParent.InstallPreconditions(this.inferredPreconditions, this.method);
                }
                specializedParent.InstallPostconditions(this.inferredPostconditions, this.method);

                // Let's add the witness
                specializedParent.MethodCache.AddWitness(this.method, this.MayReturnNull);

                // Install postconditons for other methods
                if (otherPostconditions != null)
                {
                    foreach (var tuple in otherPostconditions.GroupBy(t => t.Item1))
                    {
                        Contract.Assume(tuple.Any());
                        specializedParent.InstallPostconditions(tuple.Select(t => t.Item2).ToList(), tuple.First().Item1);
                    }
                }

                if (classDriver != null)
                {
                    var methodThis = this.MetaDataDecoder.This(this.method);
                    classDriver.InstallInvariantsAsConstructorPostconditions(methodThis, this.inferredObjectInvariants, this.method);

                    // Really install object invariants. TODO: readonly fields only
                    // Missing something... how do I build a Contract.Invariant(...)?

                    var asAssume = this.inferredObjectInvariants.Select(be => (new BoxedExpression.AssumeExpression(be.One, "invariant", this.CFG.EntryAfterRequires, be.Two, be.One.ToString<Type>(type => OutputPrettyCS.TypeHelper.TypeFullName(this.MetaDataDecoder, type))))).Cast<BoxedExpression>();
                    specializedParent.InstallObjectInvariants(asAssume.ToList(), classDriver.ClassType);
                }
            }

            #endregion


            #region IFactBase<SymbolicValue> Members

            public ProofOutcome IsNull(APC pc, SymbolicValue value)
            {
                return ProofOutcome.Top;
                //this.ValueLayer.Decoder.Context.ValueContext.IsZero(pc, value);
            }

            public ProofOutcome IsNonNull(APC pc, SymbolicValue value)
            {
                return ProofOutcome.Top;
                //this.ValueLayer.Decoder.Context.ValueContext.IsZero(pc, value);
            }

            public FList<BoxedExpression> InvariantAt(APC pc, FList<SymbolicValue> filter, bool replaceVarsWithAccessPath = true)
            {
                return FList<BoxedExpression>.Empty;
            }

            public bool IsUnreachable(APC pc)
            {
                return optimisticHeapAnalysis.IsUnreachable(pc);
            }
            #endregion
        }
    }
}
