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
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  using Provenance = IEnumerable<ProofObligation>;
  using StackTemp = System.Int32;

  class BasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
    : IBasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
    where LogOptions : IFrameworkLogOptions
    where Type : IEquatable<Type>
  {

    #region Object invariant
    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.parent != null);
    }

    #endregion

    private readonly ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit> rawLayer;
    private readonly ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit> stackLayer;
    protected readonly IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> parent;
    protected readonly Method method;

    private ICFG contractFreeCFG; // computed on demand

    protected BasicMethodDriver(
      Method method,
      IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> parent
    )
    {
      Contract.Requires(parent != null);

      this.method = method;
      this.parent = parent;
      var rawCFG = this.parent.MethodCache.GetCFG(method);

      // Here we build our stack of adapters.
      //
      //  StackDepth    (Temp decoder for APC)
      //  CFG           (Unit decoder for APC, including Assume)
      //  cciilprovider (Unit decoder for PC)
      //

      this.rawLayer = CodeLayerFactory.Create(
        rawCFG.GetDecoder(parent.MetaDataDecoder),
        parent.MetaDataDecoder,
        parent.ContractDecoder,
        (unit) => "",
        (unit) => "",
        (v1, v2) => false);

      if (PrintIL)
      {
        Console.WriteLine("-----------------APC based CFG---------------------");
        this.RawLayer.Decoder.AssumeNotNull().Context.MethodContext.CFG.Print(Console.Out, this.RawLayer.Printer, null, null, null);
      }

      this.stackLayer = CodeLayerFactory.Create(
          StackDepthFactory.Create(
            this.RawLayer.Decoder,
            this.RawLayer.MetaDataDecoder),
          this.RawLayer.MetaDataDecoder,
          this.RawLayer.ContractDecoder,
          delegate(int i) { return "s" + i.ToString(); },
          delegate(int i) { return "s" + i.ToString(); },
          (s1, s2) => false
          );

      if (PrintIL)
      {
        Console.WriteLine("-----------------Stack based CFG---------------------");
        this.StackLayer.Decoder.AssumeNotNull().Context.MethodContext.CFG.Print(Console.Out, StackLayer.Printer, null, null, null);
      }

      // set static wp tracing option
      WeakestPreconditionProver.Trace = parent.Options.TraceWP;
      WeakestPreconditionProver.EmitSMT2Formula = parent.Options.EmitSMT2Formula;
    }

    public SyntacticComplexity SyntacticComplexity { get; set; }
    public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit> RawLayer { get { return this.rawLayer; } }
    public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit> StackLayer { get { return this.stackLayer; } }

    private ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit> contractFreeRawLayer;

    public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Unit, Unit, IMethodContext<Field, Method>, Unit> ContractFreeRawLayer
    {
      get
      {
        if (this.contractFreeRawLayer == null)
        {
          this.contractFreeRawLayer =
            CodeLayerFactory.Create(
              ContractFreeCFG.GetDecoder(parent.MetaDataDecoder),
              RawLayer.MetaDataDecoder,
              RawLayer.ContractDecoder,
              RawLayer.ExpressionToString,
              RawLayer.VariableToString,
              RawLayer.Printer,
              RawLayer.NewerThan);
        }
        return this.contractFreeRawLayer;
      }

    }

    private ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit> contractFreeStackLayer;

    public ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, StackTemp, StackTemp, IStackContext<Field, Method>, Unit> ContractFreeStackLayer
    {
      get
      {
        if (this.contractFreeStackLayer == null)
        {
          this.contractFreeStackLayer = CodeLayerFactory.Create(
            StackDepthFactory.Create(
              this.ContractFreeRawLayer.Decoder,
              this.ContractFreeRawLayer.MetaDataDecoder),
            this.ContractFreeRawLayer.MetaDataDecoder,
            this.ContractFreeRawLayer.ContractDecoder,
            this.StackLayer.ExpressionToString,
            this.StackLayer.VariableToString,
            this.StackLayer.Printer,
            this.StackLayer.NewerThan);
        }
        return this.contractFreeStackLayer;
      }
    }

    protected bool PrintIL { get { return this.parent.Options.PrintIL; } }

#if false
      public IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, StackTemp, StackTemp, IStackContext<Field, Method>> StackDecoder
      {
        get { return stackDecoder; }
      }

      public Func<AState, IFixpointInfo<APC,AState>> CreateForward<AState>(IAnalysisDriver<APC, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, AState, AState>, IMethodContext<Field, Method>> analysis)
      {
        var solver = ForwardAnalysisSolver<AState, Type>.Make(apcDecoder, analysis, apcRawPrinter);
        solver.Trace = this.parent.Options.TraceDFA;
        solver.TraceTimePerInstruction = this.parent.Options.TraceTimings;

        return (initialState) => { solver.Run(initialState); return solver; };
      }

      public Func<AState, IFixpointInfo<APC,AState>> CreateForward<AState>(IAnalysisDriver<APC, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, int, int, AState, AState>, IStackContext<Field, Method>> analysis)
      {
        var solver = ForwardAnalysisSolver<AState, Type>.Make(stackDecoder, analysis, apcStackPrinter);
        solver.Trace = this.parent.Options.TraceDFA;
        return (initialState) => { solver.Run(initialState); return solver; };
      }

      public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get { return this.parent.MetaDataDecoder; } }
#endif

    public LogOptions Options { get { return this.parent.Options; } }

    public ICFG ContractFreeCFG
    {
      get
      {

        if (this.contractFreeCFG == null)
        {
          var decoder = this.RawLayer.Decoder;
          Contract.Assert(decoder != null);
          this.contractFreeCFG = new ContractFilteredCFG(decoder.Context.MethodContext.CFG);
          if (PrintIL)
          {
            Console.WriteLine("-----------------raw contract-free CFG---------------------");
            this.contractFreeCFG.Print(Console.Out, this.RawLayer.Printer, null, null, null);

            var loopInfo = LoopAnalysis.ComputeContainingLoopMap(this.contractFreeCFG);
            Console.WriteLine("  --- loop info ---");
            foreach (var block in loopInfo.Keys)
            {
              Console.Write("  Block {0}.{1} is inside loop heads ", block.Subroutine.Id, block.Index);
              var loops = loopInfo[block].AssumeNotNull();
              foreach (var loophead in loops)
              {
                Console.Write("{0}.{1} ", loophead.Subroutine.Id, loophead.Index);
              }
              Console.WriteLine();
            }
          }
        }
        return this.contractFreeCFG;
      }
    }

    public IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> AnalysisDriver
    {
      get
      {
        return this.parent;
      }
    }

    public ICFG CFG { 
      [ContractVerification(false)]
      get { return this.StackLayer.Decoder.Context.AssumeNotNull().MethodContext.CFG; } }

    public Method CurrentMethod { get { return this.method; } }
  }

  abstract class BasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
    : BasicMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>,
      IBasicMethodDriverWithInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions>
    where LogOptions : IFrameworkLogOptions
    where Type : IEquatable<Type>
  {
    protected BasicMethodDriverWithInference(
        Method method,
        IBasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, LogOptions> parent
      )
      : base(method, parent)
    { }

    public abstract bool CanAddRequires();
    public abstract bool CanAddEnsures();
    protected abstract bool CanAddInvariants();
    protected abstract bool CanAddAssumes();

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(inferredPreconditions != null);
      Contract.Invariant(inferredPostconditions != null);
      Contract.Invariant(inferredObjectInvariants != null);
      Contract.Invariant(inferredCalleeAssumes != null);
      Contract.Invariant(inferredEntryAssumes != null);
    }

    protected readonly List<BoxedExpression.AssumeExpression> inferredPreconditions = new List<BoxedExpression.AssumeExpression>();

    public IEnumerable<Pair<BoxedExpression, Provenance>> InferredPreConditions { get { return this.inferredPreconditions.Select(assert => Pair.For(assert.Condition, assert.Provenance)); } }

    public bool AddPreCondition(BoxedExpression boxedExpression, APC apc, Provenance provenance)
    {
      if (!this.CanAddRequires())
      {
        this.AddEntryAssume(boxedExpression, provenance); // record it as assume instead
        return false;
      }
      var pre = new BoxedExpression.AssumeExpression(boxedExpression, "requires", apc, provenance, boxedExpression.ToString<Type>(type => OutputPrettyCS.TypeHelper.TypeFullName(this.AnalysisDriver.MetaDataDecoder, type)));
      this.inferredPreconditions.Add(pre);
      return true;
    }

    protected readonly List<BoxedExpression.AssertExpression> inferredPostconditions = new List<BoxedExpression.AssertExpression>();

    public IEnumerable<Pair<BoxedExpression, Provenance>> InferredPostConditions { get { return this.inferredPostconditions.Select(assert => Pair.For(assert.Condition, assert.Provenance)); } }

    public bool AddPostCondition(BoxedExpression postcondition, APC pc, Provenance provenance)
    {
      if (!this.CanAddEnsures())
      {
        return false;
      }
      var post = new BoxedExpression.AssertExpression(postcondition, "ensures", pc, provenance, null);
      this.inferredPostconditions.Add(post);
      return true;
    }

    protected List<Tuple<Method, BoxedExpression.AssertExpression, Provenance>> otherPostconditions;
    public bool AddPostConditionsForAutoProperties(List<Tuple<Method, BoxedExpression.AssertExpression, Provenance>> postconditions)
    {
      if(postconditions == null)
      {
        return false;
      }

      if (otherPostconditions == null)
      {
        otherPostconditions = postconditions;
      }
      else
      {
        otherPostconditions.AddRange(postconditions);
      }

      return true;
    }

    public bool MayReturnNull { get; set; }

    // We do not box object invariants into Contract.Invariant because we want to use them as postconditions of constructors
    protected readonly List<Pair<BoxedExpression, Provenance>> inferredEntryAssumes = new List<Pair<BoxedExpression, Provenance>>();

    public IEnumerable<Pair<BoxedExpression, Provenance>> InferredEntryAssumes { get { return this.inferredEntryAssumes; } }

    // We do not box object invariants into Contract.Invariant because we want to use them as postconditions of constructors
    protected readonly List<Pair<BoxedExpression, Provenance>> inferredObjectInvariants = new List<Pair<BoxedExpression, Provenance>>();

    public IEnumerable<Pair<BoxedExpression, Provenance>> InferredObjectInvariants { get { return this.inferredObjectInvariants; } }

    public bool AddObjectInvariant(BoxedExpression objinv, Provenance provenance)
    {
      if (!this.CanAddInvariants())
      {
        this.AddEntryAssume(objinv, provenance); // record as assume instead
        return false;
      }
      this.inferredObjectInvariants.Add(new Pair<BoxedExpression, Provenance>(objinv, provenance));
      return true;
    }

    protected readonly List<Pair<BoxedExpression.AssumeExpression, Method>> inferredCalleeAssumes = new List<Pair<BoxedExpression.AssumeExpression, Method>>();

    public IEnumerable<STuple<BoxedExpression, Provenance, Method>> InferredCalleeAssumes { get { return this.inferredCalleeAssumes.Select(p => STuple.For(p.One.Condition, p.One.Provenance, p.Two)); } }

    public bool AddCalleeAssume(BoxedExpression assumeExpression, object callee, APC apc, Provenance provenance)
    {
      Contract.Assume(callee != null);

      if (!this.CanAddAssumes())
      {
        return false;
      }
      // TODO: collect context info
      var assume = new BoxedExpression.AssumeExpression(assumeExpression, "assume", apc, provenance, assumeExpression.ToString());
      //var assumePostCond = new BoxedExpression.AssumeAsPostConditionExpression(assumeExpression, null, callee, calleeName, "assume", apc, provenance);
      //var assume = new BoxedExpression.AssumeExpression(boxedExpression, "assume", apc, provenance);
      this.inferredCalleeAssumes.Add(Pair.For(assume, (Method)callee));
      return true;
    }

    public bool AddEntryAssume(BoxedExpression assumeExpression, Provenance provenance)
    {
      if (!this.CanAddAssumes())
      {
        return false;
      }
      this.inferredEntryAssumes.Add(new Pair<BoxedExpression, Provenance>(assumeExpression, provenance));
      return true;
    }

  }
}
