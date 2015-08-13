// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;

using StackTemp = System.Int32;

namespace Microsoft.Research.CodeAnalysis
{
    using Provenance = IEnumerable<ProofObligation>;
    using SubroutineContext = FList<Microsoft.Research.DataStructures.STuple<CFGBlock, CFGBlock, string>>;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Used to get the renamings at the exit point of a block
    /// </summary>
    public delegate IFunctionalMap<string, string> CrossBlockRenamings<Label>(Label blockEnd, Label blockTarget);

    public delegate Set<string> RenamedVariables<Label>(Label blockEnd, Label blockTarget);

    public delegate string AssumptionFinder<Label>(Label label);

    public delegate string InvariantQuery<Label>(Label label);

    [ContractClass(typeof(IBasicAnalysisDriverContract<,,,,,,,,,>))]
    public interface IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, out LogOptions>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        LogOptions Options { get; }

        MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> MethodCache { get; }

        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get; }

        IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder { get; }

        IOutput Output { get; }
    }

    #region IBasicAnalysisDriver contract binding
    [ContractClassFor(typeof(IBasicAnalysisDriver<,,,,,,,,,>))]
    internal abstract class IBasicAnalysisDriverContract<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
      : IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        #region IBasicAnalysisDriver<Local,Parameter,Method,Field,Property,Event,Type,Attribute,Assembly,LogOptions> Members

        public LogOptions Options
        {
            get
            {
                Contract.Ensures(Contract.Result<LogOptions>() != null);
                throw new NotImplementedException();
            }
        }

        public MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly> MethodCache
        {
            get
            {
                Contract.Ensures(Contract.Result<MethodCache<Local, Parameter, Type, Method, Field, Property, Event, Attribute, Assembly>>() != null);
                throw new NotImplementedException();
            }
        }

        public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder
        {
            get
            {
                Contract.Ensures(Contract.Result<IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>>() != null);
                throw new NotImplementedException();
            }
        }

        public IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder
        {
            get
            {
                Contract.Ensures(Contract.Result<IDecodeContracts<Local, Parameter, Method, Field, Type>>() != null);
                throw new NotImplementedException();
            }
        }

        public IOutput Output
        {
            get
            {
                Contract.Ensures(Contract.Result<IOutput>() != null);
                throw new NotImplementedException();
            }
        }

        #endregion
    }
    #endregion

    public interface IVisitValueExprIL<Label, Type, Expression, SymbolicValue, Data, Result> : IVisitExprIL<Label, Type, Expression, SymbolicValue, Data, Result>
    {
        /// <summary>
        /// Base case for expression visitor when no more information about the symbolic constant is available
        /// </summary>
        Result SymbolicConstant(Label pc, SymbolicValue symbol, Data data);
    }

    /// <summary>
    /// A decoder instantiated to a particular data,result transformation.
    /// </summary>
    /// <typeparam name="Label">Type of underlying code points</typeparam>
    /// <typeparam name="Data">Transformation argument type</typeparam>
    /// <typeparam name="Result">Transformation result</typeparam>
    /// <typeparam name="Visitor">The visitor that is dispatched to.</typeparam>
    /// <param name="label">Dispatch according to code at this label</param>
    /// <param name="data">Data to pass to visitor</param>
    /// <param name="v">visitor being dispatched to</param>
    /// <returns></returns>
    public delegate Result ILDecoder<Label, Data, Result, Visitor>(Label label, Data data, Visitor v);

    /// <summary>
    /// The combination of an ILDecoder and a Visitor
    /// </summary>
    public delegate Result Transformer<Label, Data, Result>(Label label, Data data);


    /// <summary>
    /// A delegate representing a join operation
    /// </summary>
    public delegate AState Joiner<Label, AState>(Pair<Label, Label> edge, AState newState, AState prevState, out bool weaker, bool widen);

    /// <summary>
    /// A delegate representing a converter that gets a chance to convert an abstract state when it is pushed
    /// onto a block. This allows for instance renaming.
    /// </summary>
    public delegate AState EdgeConverter<Label, AState, EdgeData>(Label from, Label next, bool joinPoint, EdgeData data, AState newState);

    /// <summary>
    /// A delegate that can generate a constant of an abstract state
    /// </summary>
    /// <typeparam name="AState"></typeparam>
    /// <returns></returns>
    public delegate AState StateGenerator<AState>();

    [ContractClass(typeof(IFixpointInfoContracts<,>))]
    public interface IFixpointInfo<Label, AState>
    {
        /// <summary>
        /// Returns true if state is found
        /// </summary>
        bool PreState(Label label, out AState ifFound);
        /// <summary>
        /// Returns true if state is found
        /// </summary>
        bool PostState(Label label, out AState ifFound);

        /// <summary>
        /// Try to get an abstract state which captures the postconditions of the method
        /// </summary>
        bool TryAStateForPostCondition(AState initialState, out AState exitState);

        /// <summary>
        /// Returns the set of cached subroutine contexts starting at this block
        /// NOTE: slow, use only for debugging
        /// </summary>
        IEnumerable<SubroutineContext> CachedContexts(CFGBlock block);

        /// <summary>
        /// Push the given state as state on a possible exception edge originating
        /// from the given pc.
        /// </summary>
        void PushExceptionState(Label atThrow, AState exceptionState);
    }

    #region Contracts for IFixpointInfo

    [ContractClassFor(typeof(IFixpointInfo<,>))]
    internal abstract class IFixpointInfoContracts<Label, AState>
      : IFixpointInfo<Label, AState>
    {
        public bool PreState(Label label, out AState ifFound)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out ifFound) != null);

            throw new NotImplementedException();
        }

        public bool PostState(Label label, out AState ifFound)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out ifFound) != null);

            throw new NotImplementedException();
        }

        public bool TryAStateForPostCondition(AState initialState, out AState exitState)
        {
            Contract.Requires(initialState != null);
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out exitState) != null);

            throw new NotImplementedException();
        }

        public IEnumerable<SubroutineContext> CachedContexts(CFGBlock block)
        {
            Contract.Ensures(Contract.Result<IEnumerable<SubroutineContext>>() != null);

            return null;
        }

        public void PushExceptionState(Label atThrow, AState exceptionState)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    public interface IAbstractValue<T>
    {
        T Top { get; }
        T Bottom { get; }

        bool IsTop { get; }
        bool IsBottom { get; }

        /// <summary>
        /// Used by the underlying fixpoint computation to signal that the argument value will not be used
        /// for transfer update any longer.
        /// Useful for debugging. Usually, can be implemented as an identity function. But if the analysis 
        /// does complicated sharing and updating, one might have to do something different.
        /// </summary>
        /// <returns>A value that is guaranteed to not be modified by the analysis.</returns>
        T ImmutableVersion();

        /// <summary>
        /// Return a value equivalent to the argument value that can be updated by the transfer function.
        ///
        /// For purely functional states, this is an identity. For transfer functions that mutate the 
        /// abstract value, this should be a clone.
        /// </summary>
        T Clone();

        /// <summary>
        /// Returns weaker==true if the result of the join is strictly more abstract than existingState (this)
        /// i.e., !LessEqual(result, existingState). This can be true more often than necessary, unless widen
        /// is true.
        ///
        /// If widen is true, operation must widen
        /// </summary>
        T Join(T newState, out bool weaker, bool widen);

        /// <summary>
        /// </summary>
        T Meet(T that);

        bool LessEqual(T that);

        void Dump(TextWriter tw);
    }


    /// <summary>
    /// An analysis driver encapsulates a complete analysis to be used by a fix point engine.
    /// The structure allows generating stacks of drivers, where elements in the stack can 
    /// adjust what higher level drivers see.
    /// </summary>
    /// <typeparam name="Label">Abstract PC type</typeparam>
    /// <typeparam name="AState">Abstract state for analysis</typeparam>
    /// <typeparam name="Interface">The visitor interface used by the analysis</typeparam>
    /// <typeparam name="EdgeConversionData">Additional information on edges for transforming abstract state</typeparam>
    public partial interface IAnalysis<Label, AState, Interface, EdgeConversionData>
    {
        /// <summary>
        /// A converter that gets a chance to convert an abstract state when it is pushed
        /// onto a block. This allows for instance renaming.
        /// 
        /// The edgeData depends on the particular analysis layer and is optional.
        /// </summary>
        AState EdgeConversion(Label from, Label next, bool joinPoint, EdgeConversionData edgeData, AState newState);

        /// <summary>
        /// The join operation
        /// </summary>
        /// <returns></returns>
        AState Join(Pair<Label, Label> edge, AState newState, AState prevState, out bool weaker, bool widen);

        /// <summary>
        /// Obtain a mutable version of an abstract state (usually a clone)
        /// </summary>
        /// <returns></returns>
        AState MutableVersion(AState state);

        /// <summary>
        /// Marks a state as no longer mutable. Mostly for debugging.
        /// </summary>
        /// <returns></returns>
        AState ImmutableVersion(AState state);

        /// <summary>
        /// Dumps the abstract state
        /// </summary>
        /// <returns></returns>
        void Dump(Pair<AState, TextWriter> pair);

        /// <summary>
        /// Evaluates to true on bottom states.
        /// This instructs the fixpoint analysis to stop following paths.
        /// </summary>
        bool IsBottom(Label pc, AState state);

        /// <summary>
        /// Evaluates to true on top states.
        /// </summary>
        bool IsTop(Label pc, AState state);

        /// <summary>
        /// Must return a visitor that is used to construct the transfer function
        /// </summary>
        Interface Visitor();

        /// <summary>
        /// Called by underlying engine to provide a lookup method that can be used to find fixpoint states
        /// after the fix point has been reached.
        /// </summary>
        /// <returns>Can return a predicate that informs the underlying DFA at which pc's to cache the state.
        /// if null, no extra caching is done.</returns>
        Predicate<Label> CacheStates(IFixpointInfo<Label, AState> fixpointInfo);
    }

    #region IAnalysis contract binding
    [ContractClass(typeof(IAnalysisContract<,,,>))]
    public partial interface IAnalysis<Label, AState, Interface, EdgeConversionData> { }

    [ContractClassFor(typeof(IAnalysis<,,,>))]
    internal abstract class IAnalysisContract<Label, AState, Interface, EdgeConversionData> : IAnalysis<Label, AState, Interface, EdgeConversionData>
    {
        #region IAnalysis<Label,AState,Interface,EdgeConversionData> Members

        public AState EdgeConversion(Label from, Label next, bool joinPoint, EdgeConversionData edgeData, AState newState)
        {
            throw new NotImplementedException();
        }

        public AState Join(Pair<Label, Label> edge, AState newState, AState prevState, out bool weaker, bool widen)
        {
            throw new NotImplementedException();
        }

        public AState MutableVersion(AState state)
        {
            throw new NotImplementedException();
        }

        public AState ImmutableVersion(AState state)
        {
            throw new NotImplementedException();
        }

        public void Dump(Pair<AState, TextWriter> pair)
        {
            throw new NotImplementedException();
        }

        public bool IsBottom(Label pc, AState state)
        {
            throw new NotImplementedException();
        }

        public bool IsTop(Label pc, AState state)
        {
            throw new NotImplementedException();
        }

        public Interface Visitor()
        {
            throw new NotImplementedException();
        }

        public Predicate<Label> CacheStates(IFixpointInfo<Label, AState> fixpointInfo)
        {
            Contract.Requires(fixpointInfo != null);
            throw new NotImplementedException();
        }

        #endregion
    }
    #endregion


    public interface IEdgeVisit<Label, Local, Parameter, Method, Field, Type, Variable, State> : IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Variable, Variable, State, State>
    {
        /// <summary>
        /// Called in backward edge visitor if the edge corresponds to a renaming
        /// </summary>
        /// <param name="from">Start of edge (in backward transfer this is the later pc)</param>
        /// <param name="to">Target of edge (in backward transfer this is the earlier pc)</param>
        State Rename(Label from, Label to, State state, IFunctionalMap<Variable, Variable> renaming);
    }


    [ContractClass(typeof(ICodeLayerContracts<,,,,,,,,,,,,>))]
    public interface ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeConversionData>
      where Type : IEquatable<Type>
      where ContextData : IMethodContext<Field, Method>
    {
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get; }
        IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder { get; }

        /// <summary>
        /// Decode the instruction immediately following the label on a forward path.
        /// </summary>
        IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeConversionData> Decoder { get; }

        /// <summary>
        /// A converter from destinations (variables) to strings
        /// </summary>
        Converter<Variable, string> VariableToString { get; }

        /// <summary>
        /// A converter from sources (expressions) to strings
        /// </summary>
        Converter<Expression, string> ExpressionToString { get; }

        /// <summary>
        /// A printer for IL at this abstraction level
        /// </summary>
        ILPrinter<APC> Printer { get; }

        /// <summary>
        /// Returns true if v1 is newer than v2
        /// </summary>
        bool NewerThan(Variable v1, Variable v2);

        /// <summary>
        /// Creates a forward analysis with the given IL visitor.
        /// </summary>
        /// <typeparam name="AnalysisState">Abstract state of analysis</typeparam>
        /// <param name="analysis">A visitor handling il at this layer</param>
        /// <returns>A delegate that starts the fixpoint computation if provided an initial abstract value</returns>
        Func<AnalysisState, IFixpointInfo<APC, AnalysisState>> CreateForward<AnalysisState>(
          IAnalysis<APC, AnalysisState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, AnalysisState, AnalysisState>, EdgeConversionData> analysis,
          DFAOptions options
        );
    }

    #region ICodeLayerContracts

    [ContractClassFor(typeof(ICodeLayer<,,,,,,,,,,,,>))]
    internal abstract class ICodeLayerContracts<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeConversionData>
      : ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeConversionData>
      where Type : IEquatable<Type>
      where ContextData : IMethodContext<Field, Method>
    {
        public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder
        {
            get
            {
                Contract.Ensures(Contract.Result<IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>>() != null);
                throw new NotImplementedException();
            }
        }

        public IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder
        {
            get { throw new NotImplementedException(); }
        }

        public IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeConversionData> Decoder
        {
            get
            {
                Contract.Ensures(Contract.Result<IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeConversionData>>() != null);
                throw new NotImplementedException();
            }
        }

        public Converter<Variable, string> VariableToString
        {
            get { throw new NotImplementedException(); }
        }

        public Converter<Expression, string> ExpressionToString
        {
            get { throw new NotImplementedException(); }
        }

        public ILPrinter<APC> Printer
        {
            get { throw new NotImplementedException(); }
        }

        public Func<AnalysisState, IFixpointInfo<APC, AnalysisState>> CreateForward<AnalysisState>(IAnalysis<APC, AnalysisState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, AnalysisState, AnalysisState>, EdgeConversionData> analysis, DFAOptions options)
        {
            Contract.Ensures(Contract.Result<Func<AnalysisState, IFixpointInfo<APC, AnalysisState>>>() != null);

            throw new NotImplementedException();
        }

        public bool NewerThan(Variable v1, Variable v2)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region IBasicMethodDriver contract binding
    [ContractClass(typeof(IBasicMethodDriverContract<,,,,,,,,,>))]
    public partial interface IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, out LogOptions>
    {
    }

    [ContractClassFor(typeof(IBasicMethodDriver<,,,,,,,,,>))]
    internal abstract class IBasicMethodDriverContract<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> : IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit> IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.RawLayer
        {
            get
            {
                Contract.Ensures(Contract.Result<ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit>>() != null);
                throw new NotImplementedException();
            }
        }

        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit> IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.StackLayer
        {
            get
            {
                Contract.Ensures(Contract.Result<ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit>>() != null);
                throw new NotImplementedException();
            }
        }

        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit> IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.ContractFreeRawLayer
        {
            get
            {
                Contract.Ensures(Contract.Result<ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit>>() != null);
                throw new NotImplementedException();
            }
        }

        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit> IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.ContractFreeStackLayer
        {
            get
            {
                Contract.Ensures(Contract.Result<ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit>>() != null);
                throw new NotImplementedException();
            }
        }

        LogOptions IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.Options
        {
            get
            {
                Contract.Ensures(Contract.Result<LogOptions>() != null);
                throw new NotImplementedException();
            }
        }

        ICFG IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.ContractFreeCFG
        {
            get
            {
                Contract.Ensures(Contract.Result<ICFG>() != null);
                throw new NotImplementedException();
            }
        }

        SyntacticComplexity IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.SyntacticComplexity
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.AnalysisDriver
        {
            get
            {
                Contract.Ensures(Contract.Result<IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>>() != null);
                throw new NotImplementedException();
            }
        }
    }
    #endregion

    public partial interface IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, out LogOptions>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        /// <summary>
        /// The original IL code layer
        /// </summary>
        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit>
          RawLayer
        { get; }

        /// <summary>
        /// The stack code layer with stack temporary numbers and stack depth info
        /// </summary>
        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit>
          StackLayer
        { get; }

        /// <summary>
        /// The original IL code layer
        /// </summary>
        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit>
          ContractFreeRawLayer
        { get; }

        /// <summary>
        /// The stack code layer with stack temporary numbers and stack depth info
        /// </summary>
        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit>
          ContractFreeStackLayer
        { get; }

        LogOptions Options { get; }

        ICFG ContractFreeCFG { get; }

        /// <summary>
        /// The Syntactic complexity of the method
        /// </summary>
        SyntacticComplexity SyntacticComplexity { get; set; }

        IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> AnalysisDriver { get; }
    }

    public interface IDisjunctiveExpressionRefiner<Variable, Expression>
    {
        bool TryRefineExpression(APC pc, Variable toRefine, out Expression refined);

        bool TryApplyModusPonens(APC pc, Expression premise, Predicate<Expression> oracle, out List<Expression> consequences);
    }

    #region IBasicMethodDriverWithInference contract binding
    [ContractClass(typeof(IBasicMethodDriverWithInferenceContract<,,,,,,,,,>))]
    public partial interface IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, out LogOptions>
    {
    }

    [ContractClassFor(typeof(IBasicMethodDriverWithInference<,,,,,,,,,>))]
    internal abstract class IBasicMethodDriverWithInferenceContract<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> : IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        #region IBasicMethodDriverWithInference
        bool IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.AddPreCondition(BoxedExpression boxedExpression, APC pc, Provenance provenance)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Pair<BoxedExpression, Provenance>> IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.InferredPreConditions
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Pair<BoxedExpression, Provenance>>>() != null);
                throw new NotImplementedException();
            }
        }

        bool IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.AddPostCondition(BoxedExpression boxedExpression, APC pc, Provenance provenance)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Pair<BoxedExpression, Provenance>> IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.InferredPostConditions
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Pair<BoxedExpression, Provenance>>>() != null);
                throw new NotImplementedException();
            }
        }

        bool IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.AddObjectInvariant(BoxedExpression boxedExpression, Provenance provenance)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Pair<BoxedExpression, Provenance>> IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.InferredObjectInvariants
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Pair<BoxedExpression, Provenance>>>() != null);
                throw new NotImplementedException();
            }
        }

        bool IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.AddCalleeAssume(BoxedExpression boxedExpression, object callee, APC pc, Provenance provenance)
        {
            throw new NotImplementedException();
        }

        IEnumerable<STuple<BoxedExpression, Provenance, Method>> IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.InferredCalleeAssumes
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<STuple<BoxedExpression, Provenance, Method>>>() != null);
                throw new NotImplementedException();
            }
        }

        bool IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.AddEntryAssume(BoxedExpression boxedExpression, Provenance provenance)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Pair<BoxedExpression, Provenance>> IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.InferredEntryAssumes
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Pair<BoxedExpression, Provenance>>>() != null);
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Inherited
        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit> IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.RawLayer
        {
            get { throw new NotImplementedException(); }
        }

        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit> IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.StackLayer
        {
            get { throw new NotImplementedException(); }
        }

        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit> IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.ContractFreeRawLayer
        {
            get { throw new NotImplementedException(); }
        }

        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit> IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.ContractFreeStackLayer
        {
            get { throw new NotImplementedException(); }
        }

        LogOptions IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.Options
        {
            get { throw new NotImplementedException(); }
        }

        ICFG IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.ContractFreeCFG
        {
            get { throw new NotImplementedException(); }
        }

        SyntacticComplexity IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.SyntacticComplexity
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>.AnalysisDriver
        {
            get { throw new NotImplementedException(); }
        }
        #endregion


        public bool MayReturnNull
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public bool AddPostConditionsForAutoProperties(List<Tuple<Method, BoxedExpression.AssertExpression, Provenance>> postconditions)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    public partial interface IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, out LogOptions>
      : IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        /// <summary>
        /// Returns true if pre-condition was added.
        /// </summary>
        bool AddPreCondition(BoxedExpression boxedExpression, APC pc, Provenance provenance);

        IEnumerable<Pair<BoxedExpression, Provenance>> InferredPreConditions { get; }

        bool AddPostCondition(BoxedExpression boxedExpression, APC pc, Provenance provenance);

        IEnumerable<Pair<BoxedExpression, Provenance>> InferredPostConditions { get; }

        bool AddPostConditionsForAutoProperties(List<Tuple<Method, BoxedExpression.AssertExpression, Provenance>> candidatePostconditions);


        // TODO: make more generic, to collect a set of witnesses for the postconditions
        bool MayReturnNull { get; set; }

        bool AddObjectInvariant(BoxedExpression boxedExpression, Provenance provenance);

        IEnumerable<Pair<BoxedExpression, Provenance>> InferredObjectInvariants { get; }

        bool AddCalleeAssume(BoxedExpression boxedExpression, object callee, APC pc, Provenance provenance);

        IEnumerable<STuple<BoxedExpression, Provenance, Method>> InferredCalleeAssumes { get; }

        bool AddEntryAssume(BoxedExpression boxedExpression, Provenance provenance);

        IEnumerable<Pair<BoxedExpression, Provenance>> InferredEntryAssumes { get; }
    }

    [ContractClass(typeof(IMethodDriverContracts<,,,,,,,,,,,>))]
    public interface IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, out LogOptions>
      : IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        /// <summary>
        /// The Value Abstraction Layer
        /// </summary>
        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable, Variable, IValueContext<Local, Parameter, Method, Field, Type, Variable>, IFunctionalMap<Variable, FList<Variable>>>
          ValueLayer
        { get; }

        /// <summary>
        /// The expression layer
        /// </summary>
        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>, IFunctionalMap<Variable, FList<Variable>>>
          ExpressionLayer
        { get; }

        /// <summary>
        /// Same as the Value Abstraction Layer except that the printer prints expressions (historical)
        /// </summary>
        ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable, Variable, IValueContext<Local, Parameter, Method, Field, Type, Variable>, IFunctionalMap<Variable, FList<Variable>>>
          HybridLayer
        { get; }


        /// <summary>
        /// Same as ExpressionLayer.Decoder.Context
        /// </summary>
        IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context { get; }

        /// <summary>
        /// Same as XXXLayer.MetaDataDecoder (just temporary)
        /// </summary>
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get; }

        /// <summary>
        /// Same as StackLayer.Decoder.Context.MethodContext.CFG
        /// </summary>
        ICFG CFG { get; }

        /// <summary>
        /// Same as StackLayer.Decoder.Context.MethodContext.CurrentMethod
        /// </summary>
        Method CurrentMethod { get; }

        Converter<Variable, int> KeyNumber { get; }
        Comparison<Variable> VariableComparer { get; }

        /// <summary>
        /// Dispatches on the transfer edge. Could be an instruction, or a renaming
        /// </summary>
        /// <param name="from">Starting point of backward edge</param>
        /// <param name="to">Target of backward edge</param>
        State BackwardTransfer<State, Visitor>(APC from, APC to, State state, Visitor visitor)
          where Visitor : IEdgeVisit<APC, Local, Parameter, Method, Field, Type, Variable, State>;

        IFullExpressionDecoder<Type, Variable, Expression> ExpressionDecoder { get; }

        /// <returns>
        /// Returns true if we can add preconditions to the method
        /// </returns>
        bool CanAddRequires();

        /// <returns>
        /// Returns true if we can add postconditions to the method
        /// </returns>
        bool CanAddEnsures();

        /// <summary>
        /// Perform any pending actions on this method before moving on to the next such as pending AddRequires etc.
        /// </summary>
        void EndAnalysis();

        void RunHeapAndExpressionAnalyses();

        /// <summary>
        /// Returns a query interface for truths about variables and reachable pc's independent of higher-level analysis,
        /// just based on the value/expression analysis.
        /// </summary>
        IFactBase<Variable> BasicFacts { get; }

        /// <summary>
        /// Returns a query interface to enable refinement to disjunctive expressions.
        /// It may return null.
        /// </summary>
        IDisjunctiveExpressionRefiner<Variable, BoxedExpression> DisjunctiveExpressionRefiner { get; set; }

        SyntacticInformation<Method, Field, Variable> AdditionalSyntacticInformation { get; set; }

        bool IsUnreachable(APC pc);
    }

    #region Contracts

    [ContractClassFor(typeof(IMethodDriver<,,,,,,,,,,,>))]
    internal abstract class IMethodDriverContracts<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      : IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable, Variable, IValueContext<Local, Parameter, Method, Field, Type, Variable>, IFunctionalMap<Variable, FList<Variable>>>
          ValueLayer
        {
            get
            {
                Contract.Ensures(Contract.Result<ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable, Variable, IValueContext<Local, Parameter, Method, Field, Type, Variable>, IFunctionalMap<Variable, FList<Variable>>>>() != null);
                throw new NotImplementedException();
            }
        }

        public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>, IFunctionalMap<Variable, FList<Variable>>>
          ExpressionLayer
        {
            get
            {
                Contract.Ensures(Contract.Result<ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>, IFunctionalMap<Variable, FList<Variable>>>>() != null);
                throw new NotImplementedException();
            }
        }

        public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable, Variable, IValueContext<Local, Parameter, Method, Field, Type, Variable>, IFunctionalMap<Variable, FList<Variable>>>
          HybridLayer
        {
            get
            {
                Contract.Ensures(Contract.Result<ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable, Variable, IValueContext<Local, Parameter, Method, Field, Type, Variable>, IFunctionalMap<Variable, FList<Variable>>>>() != null);
                throw new NotImplementedException();
            }
        }

        public IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context
        {
            get
            {
                Contract.Ensures(Contract.Result<IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>>() != null);
                throw new NotImplementedException();
            }
        }

        public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder
        {
            get
            {
                Contract.Ensures(Contract.Result<IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>>() != null);
                throw new NotImplementedException();
            }
        }

        public ICFG CFG
        {
            get
            {
                Contract.Ensures(Contract.Result<ICFG>() != null);
                throw new NotImplementedException();
            }
        }

        public Method CurrentMethod
        {
            get { throw new NotImplementedException(); }
        }

        public Converter<Variable, StackTemp> KeyNumber
        {
            get { throw new NotImplementedException(); }
        }

        public Comparison<Variable> VariableComparer
        {
            get { throw new NotImplementedException(); }
        }

        public State BackwardTransfer<State, Visitor>(APC from, APC to, State state, Visitor visitor) where Visitor : IEdgeVisit<APC, Local, Parameter, Method, Field, Type, Variable, State>
        {
            throw new NotImplementedException();
        }

        public IFullExpressionDecoder<Type, Variable, Expression> ExpressionDecoder
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanAddRequires()
        {
            throw new NotImplementedException();
        }

        public bool CanAddEnsures()
        {
            throw new NotImplementedException();
        }

        public void EndAnalysis()
        {
            throw new NotImplementedException();
        }

        public void RunHeapAndExpressionAnalyses()
        {
            throw new NotImplementedException();
        }

        public IFactBase<Variable> BasicFacts
        {
            get
            {
                Contract.Ensures(Contract.Result<IFactBase<Variable>>() != null);

                throw new NotImplementedException();
            }
        }

        public IDisjunctiveExpressionRefiner<Variable, BoxedExpression> DisjunctiveExpressionRefiner
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public SyntacticInformation<Method, Field, Variable> AdditionalSyntacticInformation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool AddPreCondition(BoxedExpression boxedExpression, APC pc, Provenance provenance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pair<BoxedExpression, Provenance>> InferredPreConditions
        {
            get { throw new NotImplementedException(); }
        }

        public bool AddPostCondition(BoxedExpression boxedExpression, APC pc, Provenance provenance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pair<BoxedExpression, Provenance>> InferredPostConditions
        {
            get { throw new NotImplementedException(); }
        }

        public bool AddObjectInvariant(BoxedExpression boxedExpression, Provenance provenance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pair<BoxedExpression, Provenance>> InferredObjectInvariants
        {
            get { throw new NotImplementedException(); }
        }

        public bool AddCalleeAssume(BoxedExpression boxedExpression, object callee, APC pc, Provenance provenance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<STuple<BoxedExpression, Provenance, Method>> InferredCalleeAssumes
        {
            get { throw new NotImplementedException(); }
        }

        public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit> RawLayer
        {
            get { throw new NotImplementedException(); }
        }

        public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit> StackLayer
        {
            get { throw new NotImplementedException(); }
        }

        public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit> ContractFreeRawLayer
        {
            get { throw new NotImplementedException(); }
        }

        public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit> ContractFreeStackLayer
        {
            get { throw new NotImplementedException(); }
        }

        public LogOptions Options
        {
            get { throw new NotImplementedException(); }
        }

        public ICFG ContractFreeCFG
        {
            get { throw new NotImplementedException(); }
        }

        public SyntacticComplexity SyntacticComplexity
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> AnalysisDriver
        {
            get { throw new NotImplementedException(); }
        }


        public bool IsUnreachable(APC pc)
        {
            throw new NotImplementedException();
        }


        public bool AddEntryAssume(BoxedExpression boxedExpression, Provenance provenance)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pair<BoxedExpression, Provenance>> InferredEntryAssumes
        {
            get { throw new NotImplementedException(); }
        }


        public bool MayReturnNull
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public bool AddPostConditionsForAutoProperties(List<Tuple<Method, BoxedExpression.AssertExpression, Provenance>> postconditions)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    public interface IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get; }

        Type ClassType { get; }
        AnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> ParentDriver { get; }

        List<Method> Constructors { get; }
        LogOptions Options { get; }

        IConstructorsAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> ConstructorsStatus { get; }
        int PendingConstructors { get; }

        /// <summary>
        /// Returns true if invariant is implied by existing invariants of the class
        /// </summary>
        bool IsExistingInvariant(string preconditionString);

        /// <summary>
        /// Returns true if invariant was added.
        /// </summary>
        bool AddInvariant(BoxedExpression boxedExpression);

        /// <returns>
        /// Returns true if we can add invariants to the class
        /// </returns>
        bool CanAddInvariants();

        /// <returns>
        /// Returns true if the postcondition was added to all non-static constructors.
        /// </returns>
        bool InstallInvariantsAsConstructorPostconditions(Parameter thisOfInferringMethod, IEnumerable<Pair<BoxedExpression, Provenance>> boxedExpressions, Method method);

        /// <summary>
        /// Informs the class driver that this method has just been analyzed by an analyzer
        /// </summary>
        void MethodHasBeenAnalyzed(
          string analyzer_name,
          MethodResult result,
          Method method
          );

        /// <summary>
        /// Inform the class drivers that all the analyzers are done on this method
        /// </summary>
        void MethodHasBeenFullyAnalyzed(Method method);

        /// <summary>
        /// Returns true if MethodHasBeenFullyAnalyzed has been called at least once for each non-static method of the class
        /// </summary>
        bool IsClassFullyAnalyzed();
    }

    public interface IConstructorsAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        void ConstructorAnalyzed(IMethodAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> mas);

        Type ClassType { get; }
        int Pending { get; } /// number of constructors left to analyze before starting the ObjectInvariant analysis
        int TotalNb { get; } /// total number of constructors for this type
        Dictionary<Method, IMethodAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>> AnalysesResults { get; }
    }

    public interface IMethodAnalysisStatus<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        Method MethodObj { get; }

        /// <summary>
        /// Results from the analysis called "Key"
        /// </summary>
        Dictionary<string, MethodResult> Results { get; }
    }


    #region Context providers for various analysis abstraction layers

    [ContractClass(typeof(IValueContextContracts<,,,,,>))]
    public interface IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue> : IStackContext<Field, Method>
      where Type : IEquatable<Type>
    {
        IValueContextData<Local, Parameter, Method, Field, Type, SymbolicValue> ValueContext { get; }
    }

    #region Contracts for IValueContext

    [ContractClassFor(typeof(IValueContext<,,,,,>))]
    internal abstract class IValueContextContracts<Local, Parameter, Method, Field, Type, SymbolicValue> : IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>
      where Type : IEquatable<Type>
    {
        public IValueContextData<Local, Parameter, Method, Field, Type, SymbolicValue> ValueContext
        {
            get
            {
                Contract.Ensures(Contract.Result<IValueContextData<Local, Parameter, Method, Field, Type, SymbolicValue>>() != null);
                return null;
            }
        }

        public IStackContextData<Field, Method> StackContext
        {
            get { return null; }
        }

        public IMethodContextData<Field, Method> MethodContext
        {
            get { return null; }
        }
    }

    #endregion

    public partial interface IValueContextData<Local, Parameter, Method, Field, Type, SymbolicValue>
      where Type : IEquatable<Type>
    {
        /// <summary>
        /// Returns the value representing null
        /// </summary>
        [Pure]
        bool TryZero(APC at, out SymbolicValue value);

        /// <summary>
        /// Returns the value representing null
        /// </summary>
        [Pure]
        bool TryNull(APC at, out SymbolicValue value);

        /// <summary>
        /// Returns the type of the given symbolic value
        /// </summary>
        [Pure]
        FlatDomain<Type> GetType(APC at, SymbolicValue v);

        /// <summary>
        /// Returns the array length of the given array or buffer value if known
        /// </summary>
        [Pure]
        bool TryGetArrayLength(APC at, SymbolicValue array, out SymbolicValue length);

        /// <summary>
        /// Returns the writable bytes extent of a pointer if known
        /// </summary>
        [Pure]
        bool TryGetWritableBytes(APC at, SymbolicValue pointer, out SymbolicValue writableBytes);

        /// <summary>
        /// Returns true if value is equivalent to 0 in all its forms (int32, int8, null, null pointers, etc).
        /// </summary>
        [Pure]
        bool IsZero(APC at, SymbolicValue value);

        /// <summary>
        /// Returns true if the Symbolic value is not dummy
        /// </summary>
        [Pure]
        bool IsValid(SymbolicValue value);

        /// <summary>
        /// Returns the symbolic value on the evaluation stack with the given index. 
        /// The bottom of the stack is 0, the top given by the current stack depth at the given PC
        /// </summary>
        [Pure]
        bool TryStackValue(APC at, int stackIndex, out SymbolicValue sv);

        /// <summary>
        /// Returns the value of the given local at the given pc
        /// Should not be called if local is of type struct (unless it is a primitive numeric type).
        /// </summary>
        [Pure]
        bool TryLocalValue(APC at, Local local, out SymbolicValue sv);

        /// <summary>
        /// Returns the value representing the address of the given local at the given pc
        /// </summary>
        [Pure]
        bool TryLocalAddress(APC at, Local local, out SymbolicValue sv);

        /// <summary>
        /// Returns the value of the given parameter (which must be a parameter of the current method) at the given pc
        /// Should not be called if parameter is of type struct (unless it is a primitive numeric type).
        /// </summary>
        [Pure]
        bool TryParameterValue(APC at, Parameter parameter, out SymbolicValue sv);

        /// <summary>
        /// Returns the value representing the address of the given parameter 
        /// (which must be a parameter of the current method) at the given pc
        /// </summary>
        [Pure]
        bool TryParameterAddress(APC at, Parameter parameter, out SymbolicValue sv);

        /// <summary>
        /// Returns the symbolic value for the given field address if present.
        /// </summary>
        [Pure]
        bool TryFieldAddress(APC at, SymbolicValue objectValue, Field field, out SymbolicValue fieldAddress);

        /// <summary>
        /// Returns the symbolic value stored in the given address if present.
        /// </summary>
        [Pure]
        bool TryLoadIndirect(APC at, SymbolicValue address, out SymbolicValue value);

        /// <summary>
        /// Returns a symbolic value for the "result" of the method (for non-void methods) at the given PC
        /// </summary>
        [Pure]
        bool TryResultValue(APC pc, out SymbolicValue value);


        /// <summary>
        /// Returns an access path for the symbolic value at the given program point.
        /// The access path returned is preferably rooted in a parameter. If not possible, it is rooted in a local variable.
        /// For symbolic values that are not reachable from locals/parameters, null is returned. 
        ///
        /// The access path may contain dereferences (*), field accesses .f, and Length accesses, as well as array index operations.
        /// </summary>
        /// <param name="at">PC at which access path holds</param>
        /// <param name="value">Value for which we compute an access path</param>
        /// <returns>null if no access path is found.</returns>
        [Pure]
        string AccessPath(APC at, SymbolicValue value);

        /// <summary>
        /// Returns an access path for the symbolic value at the given program point.
        /// The access path returned is preferably rooted in a parameter. If not possible, it is rooted in a local variable.
        /// For symbolic values that are not reachable from locals/parameters, null is returned. 
        ///
        /// The access path may contain dereferences (*), field accesses .f, and Length accesses, as well as array index operations.
        /// </summary>
        /// <param name="at">PC at which access path holds</param>
        /// <param name="value">Value for which we compute an access path</param>
        /// <param name="witness">list of symbolic values corrsponding to path</param>
        /// <returns>null if no access path is found.</returns>
        [Pure]
        FList<PathElement> AccessPathListAndWitness(APC at, SymbolicValue value, bool allowLocal, bool preferLocal, out FList<SymbolicValue> witness, Type allowReturnValueFromCall = default(Type));

        /// <summary>
        /// Returns an access path for the symbolic value at the given program point.
        /// The access path returned is preferably rooted in a parameter. If not possible, it is rooted in a local variable.
        /// For symbolic values that are not reachable from locals/parameters, null is returned. 
        ///
        /// The access path may contain dereferences (*), field accesses .f, and Length accesses, as well as array index operations.
        /// </summary>
        /// <param name="at">PC at which access path holds</param>
        /// <param name="value">Value for which we compute an access path</param>
        /// <returns>null if no access path is found.</returns>
        [Pure]
        FList<PathElement> AccessPathList(APC at, SymbolicValue value, bool allowLocal, bool preferLocal, Type allowReturnValueFromCall = default(Type));

        /// <summary>
        /// Returns all possible access paths at pc to given value.
        /// </summary>
        [Pure]
        IEnumerable<FList<PathElement>> AccessPaths(APC at, SymbolicValue value, AccessPathFilter<Method, Type> filter);

        /// <summary>
        /// Create an access path to the given value, but only one that is visible from outside the
        /// method in the pre state given the current method's visibility.
        /// </summary>
        /// <returns>null if no access path is found</returns>
        [Pure]
        FList<PathElement> VisibleAccessPathListFromPre(APC at, SymbolicValue value);

        /// <summary>
        /// Create an access path to the given value, but only one that is visible from outside the
        /// method in the post state given the current method's visibility.
        /// </summary>
        /// <returns>null if no access path is found</returns>
        [Pure]
        FList<PathElement> VisibleAccessPathListFromPost(APC at, SymbolicValue value, bool allowReturnValue = true);


        /// <summary>
        /// Determines if the given access path at Label at is referring to the same value in the pre-state of the method.
        /// </summary>
        /// <param name="at">Label where path is valid</param>
        /// <param name="path">Path</param>
        /// <returns>true if same path refers to same value in pre-state of method.</returns>
        [Pure]
        bool PathUnmodifiedSinceEntry(APC at, FList<PathElement> path);

        /// <summary>
        /// Determines if the given access path is suitable as a pre conditions. Implies PathUnmodifiedSinceEntry,
        /// and additionally filters out unwanted preconditions involving static getters.
        /// </summary>
        /// <param name="at">Label where path is valid</param>
        /// <param name="path">Path</param>
        [Pure]
        bool PathSuitableInRequires(APC at, FList<PathElement> path);

        /// <summary>
        /// Determines if the given access path is suitable as a post conditions. Implies that if the location is not modified,
        /// and additionally filters out unwanted postconditions involving static getters.
        /// </summary>
        /// <param name="at">Label where path is valid</param>
        /// <param name="path">Path</param>
        [Pure]
        bool PathSuitableInEnsures(APC at, FList<PathElement> path, bool allowReturnValue);

        /// <summary>
        /// If known to be a constant, returns the type and value
        /// </summary>
        [Pure]
        bool IsConstant(APC at, SymbolicValue sv, out Type type, out object value);

        /// <summary>
        /// Is the first element of the list a parameter?
        /// </summary>
        [Pure]
        bool IsRootedInParameter(FList<PathElement> path);

        /// <summary>
        /// Is the first element return value?
        /// </summary>
        [Pure]
        bool IsRootedInReturnValue(FList<PathElement> path);

        /// <summary>
        /// Computes information about how the given symbolic value is accesssible. It computes
        /// whether any access paths exist where the value was reached from the return value of
        /// a method call, or the read of an array element.
        /// </summary>
        /// <returns>All possible ways the value is accessible</returns>
        [Pure]
        PathContextFlags PathContexts(APC at, SymbolicValue value);

        /// <summary>
        /// Succeeds if the value is the result of a pure method call at that program point.
        /// </summary>
        [Pure]
        bool IsPureFunctionCall(APC at, SymbolicValue value, out Method method, out SymbolicValue[] args);

        /// <summary>
        /// Returns true if the given symbolic value is a delegate value and the target function is known
        /// </summary>
        /// <param name="targetObject">Could be dummy in case of a static method. Don't use it in that case.</param>
        /// <param name="targetMethod">The target method of the delegate</param>
        [Pure]
        bool IsDelegateValue(APC at, SymbolicValue delegateValue, out SymbolicValue targetObject, out Method targetMethod);

        /// <summary>
        /// For enumerables, attempt to obtain model array
        /// </summary>
        [Pure]
        bool TryGetModelArray(APC at, SymbolicValue enumerable, out SymbolicValue modelArray);

        /// <summary>
        /// If given an object version id, we want to find the object value, use this method.
        /// </summary>
        [Pure]
        bool TryGetObjectFromObjectVersion(APC at, SymbolicValue objectVersion, out SymbolicValue objectValue);

        /// <summary>
        /// If given address is the element address of an array, returns the corresponding array and index
        /// </summary>
        [Pure]
        bool TryGetArrayFromElementAddress(APC at, SymbolicValue arrayElementAddress, out SymbolicValue arrayValue, out SymbolicValue index);

        /// <summary>
        /// Returns the static type corresponding to the given runtime type object if known.
        /// </summary>
        [Pure]
        bool IsRuntimeType(APC at, SymbolicValue runtimeTypeObject, out Type type);

        /// <summary>
        /// If the given argument is a box, return the contents. This only works if the contents is primitive (not a struct with fields)
        /// </summary>
        [Pure]
        bool TryUnbox(APC at, SymbolicValue box, out SymbolicValue content);
    }

    #region IValueContextData contract binding
    [ContractClass(typeof(IValueContextDataContract<,,,,,>))]
    public partial interface IValueContextData<Local, Parameter, Method, Field, Type, SymbolicValue>
      where Type : IEquatable<Type>
    {
    }

    [ContractClassFor(typeof(IValueContextData<,,,,,>))]
    internal abstract class IValueContextDataContract<Local, Parameter, Method, Field, Type, SymbolicValue> : IValueContextData<Local, Parameter, Method, Field, Type, SymbolicValue>
      where Type : IEquatable<Type>
    {
        #region IValueContextData<Local,Parameter,Method,Field,Type,SymbolicValue> Members

        public bool TryZero(APC at, out SymbolicValue value)
        {
            throw new NotImplementedException();
        }

        public bool TryNull(APC at, out SymbolicValue value)
        {
            throw new NotImplementedException();
        }

        public FlatDomain<Type> GetType(APC at, SymbolicValue v)
        {
            throw new NotImplementedException();
        }

        public bool TryGetArrayLength(APC at, SymbolicValue array, out SymbolicValue length)
        {
            throw new NotImplementedException();
        }

        public bool TryGetWritableBytes(APC at, SymbolicValue pointer, out SymbolicValue writableBytes)
        {
            throw new NotImplementedException();
        }

        public bool IsZero(APC at, SymbolicValue value)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(SymbolicValue value)
        {
            throw new NotImplementedException();
        }

        public bool TryStackValue(APC at, StackTemp stackIndex, out SymbolicValue sv)
        {
            Contract.Requires(stackIndex >= 0);

            throw new NotImplementedException();
        }

        public bool TryLocalValue(APC at, Local local, out SymbolicValue sv)
        {
            throw new NotImplementedException();
        }

        public bool TryLocalAddress(APC at, Local local, out SymbolicValue sv)
        {
            throw new NotImplementedException();
        }

        public bool TryParameterValue(APC at, Parameter parameter, out SymbolicValue sv)
        {
            throw new NotImplementedException();
        }

        public bool TryParameterAddress(APC at, Parameter parameter, out SymbolicValue sv)
        {
            throw new NotImplementedException();
        }

        public bool TryFieldAddress(APC at, SymbolicValue objectValue, Field field, out SymbolicValue fieldAddress)
        {
            throw new NotImplementedException();
        }

        public bool TryLoadIndirect(APC at, SymbolicValue address, out SymbolicValue value)
        {
            throw new NotImplementedException();
        }

        public bool TryResultValue(APC at, out SymbolicValue value)
        {
            throw new NotImplementedException();
        }

        public string AccessPath(APC at, SymbolicValue value)
        {
            throw new NotImplementedException();
        }

        public FList<PathElement> AccessPathList(APC at, SymbolicValue value, bool allowLocal, bool preferLocal, Type allowReturnValueFromCall = default(Type))
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FList<PathElement>> AccessPaths(APC at, SymbolicValue value, AccessPathFilter<Method, Type> filter)
        {
            throw new NotImplementedException();
        }

        public FList<PathElement> VisibleAccessPathListFromPre(APC at, SymbolicValue value)
        {
            throw new NotImplementedException();
        }

        public FList<PathElement> VisibleAccessPathListFromPost(APC at, SymbolicValue value, bool allowReturnValue)
        {
            throw new NotImplementedException();
        }

        public bool PathUnmodifiedSinceEntry(APC at, FList<PathElement> path)
        {
            throw new NotImplementedException();
        }

        public bool PathSuitableInRequires(APC at, FList<PathElement> path)
        {
            throw new NotImplementedException();
        }

        public bool PathSuitableInEnsures(APC at, FList<PathElement> path, bool allowReturnValue)
        {
            throw new NotImplementedException();
        }

        public bool IsConstant(APC at, SymbolicValue sv, out Type type, out object value)
        {
            throw new NotImplementedException();
        }

        public bool IsRootedInParameter(FList<PathElement> path)
        {
            throw new NotImplementedException();
        }

        public bool IsRootedInReturnValue(FList<PathElement> path)
        {
            throw new NotImplementedException();
        }

        public PathContextFlags PathContexts(APC at, SymbolicValue value)
        {
            throw new NotImplementedException();
        }

        public bool IsPureFunctionCall(APC at, SymbolicValue value, out Method method, out SymbolicValue[] args)
        {
            throw new NotImplementedException();
        }

        public bool IsDelegateValue(APC at, SymbolicValue delegateValue, out SymbolicValue targetObject, out Method targetMethod)
        {
            throw new NotImplementedException();
        }

        public bool TryGetModelArray(APC at, SymbolicValue enumerable, out SymbolicValue modelArray)
        {
            throw new NotImplementedException();
        }

        public bool TryGetObjectFromObjectVersion(APC at, SymbolicValue objectVersion, out SymbolicValue objectValue)
        {
            throw new NotImplementedException();
        }

        public bool TryGetArrayFromElementAddress(APC at, SymbolicValue arrayElementAddress, out SymbolicValue arrayValue, out SymbolicValue index)
        {
            throw new NotImplementedException();
        }

        public bool IsRuntimeType(APC at, SymbolicValue runtimeTypeObject, out Type type)
        {
            throw new NotImplementedException();
        }

        public bool TryUnbox(APC at, SymbolicValue box, out SymbolicValue content)
        {
            throw new NotImplementedException();
        }

        #endregion


        public FList<PathElement> AccessPathListAndWitness(APC at, SymbolicValue value, bool allowLocal, bool preferLocal, out FList<SymbolicValue> witness, Type allowReturnValueFromCall = default(Type))
        {
            Contract.Ensures(Contract.Result<FList<PathElement>>() == null || Contract.ValueAtReturn(out witness) != null);
            throw new NotImplementedException();
        }
    }

    #endregion

    [Flags]
    public enum PathContextFlags
    {
        None = 0,
        ViaMethodReturn = 1,
        ViaArray = 2,
        ViaField = 4,
        ViaPureMethodReturn = 8,
        ViaCast = 16,
        ViaOutParameter = 32,
        ViaCallThisHavoc = 64,
        ViaOldValue = 128,

        /// <summary>
        /// Complete bitmask
        /// </summary>
        All = 255,
    }

    [ContractClass(typeof(IExpressionContextContracts<,,,,,,>))]
    public interface IExpressionContext<Local, Parameter, Method, Field, Type, Expression, SymbolicValue>
      : IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>
      where Type : IEquatable<Type>
    {
        IExpressionContextData<Local, Parameter, Method, Field, Type, Expression, SymbolicValue> ExpressionContext { get; }
    }

    #region Contracts

    [ContractClassFor(typeof(IExpressionContext<,,,,,,>))]
    internal abstract class IExpressionContextContracts<Local, Parameter, Method, Field, Type, Expression, SymbolicValue> : IExpressionContext<Local, Parameter, Method, Field, Type, Expression, SymbolicValue>
    where Type : IEquatable<Type>
    {
        public IExpressionContextData<Local, Parameter, Method, Field, Type, Expression, SymbolicValue> ExpressionContext
        {
            get
            {
                Contract.Ensures(Contract.Result<IExpressionContextData<Local, Parameter, Method, Field, Type, Expression, SymbolicValue>>() != null);

                throw new NotImplementedException();
            }
        }

        public IValueContextData<Local, Parameter, Method, Field, Type, SymbolicValue> ValueContext
        {
            get { throw new NotImplementedException(); }
        }

        public IStackContextData<Field, Method> StackContext
        {
            get { throw new NotImplementedException(); }
        }

        public IMethodContextData<Field, Method> MethodContext
        {
            get { throw new NotImplementedException(); }
        }
    }
    #endregion

    public interface IExpressionContextData<Local, Parameter, Method, Field, Type, Expression, SymbolicValue>
      where Type : IEquatable<Type>
    {
        /// <summary>
        /// Refine a symbolic value into an expression at the given pc.
        /// </summary>
        /// <returns>A possible expression representing this value. In the future we might need to return all possible such expressions.</returns>
        Expression Refine(APC pc, SymbolicValue value);

        /// <summary>
        /// Return the symbolic value representing this expression
        /// </summary>
        /// <returns>The name of the expression expr</returns>
        SymbolicValue Unrefine(Expression expr);

        /// <summary>
        /// Decode an expression.
        /// </summary>
        Result Decode<Data, Result, Visitor>(Expression expr, Visitor visitor, Data data)
          where Visitor : IVisitValueExprIL<Expression, Type, Expression, SymbolicValue, Data, Result>;

        /// <summary>
        /// Get the type of an expression
        /// </summary>
        FlatDomain<Type> GetType(Expression expr);

        /// <summary>
        /// Returns the label at which the expression is captured
        /// </summary>
        APC GetPC(Expression expr);

        /// <summary>
        /// Returns an expression for the symbolic value
        /// </summary>
        Expression For(SymbolicValue value);

        /// <summary>
        /// Returns true if an expression is equivalent to 0, null, (int16)0, (int8)0, etc.
        /// </summary>
        bool IsZero(Expression exp);

        /// <summary>
        /// Returns the array length of the given array or buffer value if known
        /// </summary>
        bool TryGetArrayLength(Expression array, out Expression length);

        /// <summary>
        /// Returns the writable bytes extent of a pointer if known
        /// </summary>
        bool TryGetWritableBytes(Expression pointer, out Expression writableBytes);
    }
    #endregion


    /// <summary>
    /// Implements an IAnalysis for fixpoint computation along with some extra operations to manipulate the abstract domain values.
    /// </summary>
    public interface IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, MyDomain, Variable>
      : IAnalysis<APC,
                  MyDomain,
                  IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, MyDomain, MyDomain>,
                  IFunctionalMap<Variable, FList<Variable>>
                 >
      where Type : IEquatable<Type>
    {
        /// <summary>
        /// The initial abstract state.
        /// </summary>
        MyDomain GetTopValue();

        /// <summary>
        /// The bottom abstract state.
        /// </summary>
        MyDomain GetBottomValue();

        /// <summary>
        /// Extract facts from the fixpoint  
        /// </summary>
        /// <returns></returns>
        IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, MyDomain> fixpoint);
    }

    public interface IFactQuery<Expression, Variable> : IFactBase<Variable>
    {
        ProofOutcome IsNull(APC pc, Expression expr);
        ProofOutcome IsNonNull(APC pc, Expression expr);

        ProofOutcome IsTrue(APC pc, Expression condition);

        ProofOutcome IsTrue(APC pc, Question question);

        /// <summary>
        /// Check if "posAssumptions && negAssumptions ==> goal" at program point pc
        /// </summary>
        ProofOutcome IsTrueImply(APC pc, FList<Expression> posAssumptions, FList<Expression> negAssumptions, Expression goal);

        ProofOutcome IsGreaterEqualToZero(APC pc, Expression expr);
        ProofOutcome IsLessThan(APC pc, Expression left, Expression right);
        ProofOutcome IsNonZero(APC pc, Expression condition);

        /// <summary>
        /// Do left and right have the same concrete floating point type?
        /// </summary>
        ProofOutcome HaveSameFloatType(APC pc, Expression left, Expression right);

        /// <summary>
        /// Try to get a definite floating point type for exp
        /// </summary>
        bool TryGetFloatType(APC pc, Expression exp, out ConcreteFloat type);

        /// <summary>
        /// Provide variables that lower-bound the expression numerically
        /// </summary>
        IEnumerable<Variable> LowerBounds(APC pc, Expression exp, bool strict);

        /// <summary>
        /// Provide variables that upper-bound the expression numerically
        /// </summary>
        IEnumerable<Variable> UpperBounds(APC pc, Expression exp, bool strict);

        /// <summary>
        /// Provide expressions that lower-bound the expression numerically
        /// </summary>
        IEnumerable<Expression> LowerBoundsAsExpressions(APC pc, Expression exp, bool strict);

        /// <summary>
        /// Provide expressions that upper-bound the expression numerically
        /// </summary>
        IEnumerable<Expression> UpperBoundsAsExpressions(APC pc, Expression exp, bool strict);

        /// <summary>
        /// An interval for the expression <code>exp</code> at program point pc
        /// </summary>
        Pair<long, long> BoundsFor(APC pc, Expression exp);

        ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object type);
    }

    public interface IFactQueryForOverflow<Expression>
    {
        bool CanOverflow(APC pc, Expression exp);
        bool CanUnderflow(APC pc, Expression exp);
    }

    public delegate IEnumerable<IFixpointInfo<Label, AState>> FixpointEnum<Label, AState>();

    public interface IFact
    {
    }

    public interface IFactBase<Variable> : IFact
    {
        ProofOutcome IsNull(APC pc, Variable value);
        ProofOutcome IsNonNull(APC pc, Variable value);

        /// <summary>
        /// Returns true if the pc is definitely unreachable.
        /// </summary>
        bool IsUnreachable(APC pc);

        FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> variables = null, bool replaceVarsWithAccessPaths = true);
    }

    public class Question
    {
        public class IsPureArray<Variable>
          : Question
        {
            public readonly Variable array;

            public IsPureArray(Variable var)
            {
                this.array = var;
            }
        }
    }

    public class QuestionVisitor<Variable, Expression>
    {
        public ProofOutcome Visit(Question q)
        {
            var pureArray = q as Question.IsPureArray<Variable>;
            if (pureArray != null)
            {
                return VisitIsPureArray(pureArray.array);
            }

            return ProofOutcome.Top;
        }

        virtual protected ProofOutcome VisitIsPureArray(Variable array)
        {
            return ProofOutcome.Top;
        }
    }
}

