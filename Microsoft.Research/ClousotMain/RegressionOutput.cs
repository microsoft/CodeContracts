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
using Microsoft.Research.DataStructures;
using Microsoft.Research.Slicing;
using CompilerError = System.CodeDom.Compiler.CompilerError;

namespace Microsoft.Research.CodeAnalysis
{
  class RegressionOutput<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : DerivableOutputFullResults<Method, Assembly>
  {
    readonly Dictionary<Method, int> methodAnalysisCount;

    GeneralOptions options;
    IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
    int errorCount;

    public RegressionOutput(IOutputFullResults<Method, Assembly> output, GeneralOptions options, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
      : base(output)
    {
      this.options = options;
      this.mdDecoder = mdDecoder;
      this.methodAnalysisCount = new Dictionary<Method, int>();
    }

    #region IOutputFullResults<Method> Members

    AssemblyRegressionOutcomes expectedAssemblyOutcomes;
    MethodRegressionOutcomes expectedOutcomes;
    Method currentMethod;
    bool skipCurrentMethod = false;
    Assembly currentAssembly;

    
    /// <summary>
    /// Holds errors that are emitted prior to any assembly being seen, so we need to wait.
    /// </summary>
    List<CompilerError> delayedErrors;

    public override void StartAssembly(Assembly assembly)
    {
      base.StartAssembly(assembly);
      expectedAssemblyOutcomes = AssemblyRegressionOutcomes.GetOutcomes(assembly, this.mdDecoder);
      this.currentAssembly = assembly;
      if (this.delayedErrors != null)
      {
        foreach (var error in delayedErrors)
        {
          this.EmitError(error);
        }
        this.delayedErrors = null;
      }
    }

    public override void EndAssembly()
    {
      bool isCCI2 = typeof(Type).ToString() != "System.Compiler.TypeNode";

      if (!expectedAssemblyOutcomes.Passed(isCCI2))
      {
        this.errorCount += expectedAssemblyOutcomes.Errors;
        base.WriteLine("Assembly {0}", mdDecoder.Name(this.currentAssembly));
        expectedAssemblyOutcomes.ReportRegressions(this.UnderlyingOutput);
      }
      base.EndAssembly();
      expectedAssemblyOutcomes = null;
    }

    public override void StartMethod(Method method)
    {
      base.StartMethod(method);
      this.skipCurrentMethod = false;
      expectedOutcomes = MethodRegressionOutcomes.GetOutcomes(method, this.mdDecoder);

      int count;
      // It's the first time we saw the method?
      if(!this.methodAnalysisCount.TryGetValue(method, out count))
      {
        count = MethodRegressionOutcomes.GetReanalyisCountIfAny(method, this.mdDecoder);
      }
      else
      {
        count--; // we are re-analyzing the method
      }
      
      if(count > 0)
      {
        this.skipCurrentMethod = true;
      }

      this.methodAnalysisCount[method] = count;
      this.currentMethod = method;
    }

    public override void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC)
    {
      // this is the case when EndMethod was already called
      if (expectedOutcomes == null)
        return;

      if (!this.skipCurrentMethod && !expectedOutcomes.Passed(false))
      {
        this.errorCount += expectedOutcomes.Errors;
        base.WriteLine("Method {0}", mdDecoder.FullName(this.currentMethod));
        expectedOutcomes.ReportRegressions(this.UnderlyingOutput);
      }
      base.EndMethod(methodEntry, ignoreMethodEntryAPC);
      expectedOutcomes = null;
    }

    public override void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      if (this.skipCurrentMethod) return;

      if (this.options.IncludeSuggestionMessagesInRegression && !this.expectedOutcomes.CheckOutcome(suggestion))
      {
        base.Suggestion(type, kind, pc, suggestion, causes, extraInfo);
      }
    }

    public override bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      if (this.skipCurrentMethod) return true;
      
      string message = String.Format(format, args);
      int primaryILOffset, methodILOffset;
      GetILOffsets(witness.PC, this.currentMethod, out primaryILOffset, out methodILOffset);
      if (!this.expectedOutcomes.CheckOutcome(this.UnderlyingOutput, this.currentMethod, witness.Outcome, message, primaryILOffset, methodILOffset, this.mdDecoder))
      {
        // show outcome
        return base.EmitOutcome(witness, format, args);
      }

      return true;
    }

    public override bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      if (this.skipCurrentMethod) return true;

      string message = String.Format(format, args);
      int primaryILOffset, methodILOffset;
      GetILOffsets(witness.PC, this.currentMethod, out primaryILOffset, out methodILOffset);
      if (!this.expectedOutcomes.CheckOutcome(this.UnderlyingOutput, this.currentMethod, witness.Outcome, message, primaryILOffset, methodILOffset, this.mdDecoder))
      {
        // show outcome
        return base.EmitOutcomeAndRelated(witness, format, args);
      }

      return true;
    } 

    private static void GetILOffsets(APC pc, Method method, out int primaryILOffset, out int methodILOffset)
    {
      primaryILOffset = pc.ILOffset;
      if (pc.Block.Subroutine.IsMethod)
      {
        methodILOffset = 0;
        return;
      }
      // look through context
      FList<STuple<CFGBlock, CFGBlock, string>> context = pc.SubroutineContext;
      while (context != null)
      {
        if (context.Head.One.Subroutine.IsMethod)
        {
          string topLabel = context.Head.Three;
          if (topLabel == "exit" || topLabel == "entry" || topLabel.StartsWith("after")) {
            CFGBlock fromBlock = context.Head.One;
            methodILOffset = fromBlock.ILOffset(fromBlock.PriorLast);
          } else {
            CFGBlock toBlock = context.Head.Two;
            methodILOffset = toBlock.ILOffset(toBlock.First);
          }
          return;
        }
        context = context.Tail;
      }
      methodILOffset = primaryILOffset;
    }


    public override void EmitError(CompilerError error)
    {
      if (this.expectedAssemblyOutcomes == null)
      {
        if (this.delayedErrors == null)
        {
          this.delayedErrors = new List<CompilerError>();
        }
        this.delayedErrors.Add(error);
      }
      else
      {
        if (this.expectedOutcomes != null)
        {
          // we are inside a method
          if (!this.expectedOutcomes.CheckOutcome(this.UnderlyingOutput, this.currentMethod, error, this.mdDecoder))
          {
            // show outcome
            base.EmitError(error);
          }
        }
        else
        {
          if (!this.expectedAssemblyOutcomes.CheckOutcome(this.UnderlyingOutput, this.currentAssembly, error, this.mdDecoder))
          {
            // show outcome
            base.EmitError(error);
          }
        }
      }
    }

    public override int RegressionErrors()
    {
      int total = errorCount;
      if (total == 0)
      {
        base.WriteLine("Regression OK.");
      }
      else
      {
        base.WriteLine("Regression has {0} failures.", total.ToString());
      }
      return total;
    }

    public override ILogOptions LogOptions
    {
      get { return this.options; }
    }

    #endregion
  }

  public class BaseRegressionOutcomes
  {
    protected Set<string> expectedOutcomes;
    protected Set<string> actualOutcomes = new Set<string>();
    protected int errorCount = 0;

    protected BaseRegressionOutcomes(Set<string> outcomes)
    {
      this.expectedOutcomes = outcomes;
    }


#if INCLUDE_PEXINTEGRATION
    protected static ClousotRegression.ProofOutcome Translate(ProofOutcome outcome)
    {
      switch (outcome)
      {
        case ProofOutcome.Bottom: return Microsoft.Research.ClousotRegression.ProofOutcome.Bottom;
        case ProofOutcome.False: return Microsoft.Research.ClousotRegression.ProofOutcome.False;
        case ProofOutcome.Top: return Microsoft.Research.ClousotRegression.ProofOutcome.Top;
        case ProofOutcome.True: return Microsoft.Research.ClousotRegression.ProofOutcome.True;
        default: throw new NotImplementedException();
      }
    }
#endif

    public virtual bool Passed(bool ignoreExtra)
    {
      if (this.errorCount == 0 && !ignoreExtra)
      {
        // make sure we don't have extra expected outcomes

        if (this.actualOutcomes.Count < this.expectedOutcomes.Count)
        {
          this.errorCount++;
        }
      }
      return this.errorCount == 0;
    }


    public void ReportRegressions(IOutput output)
    {
      Set<string> expectedDiff = this.expectedOutcomes.Difference(this.actualOutcomes);
      Set<string> actualDiff = this.actualOutcomes.Difference(this.expectedOutcomes);
      output.WriteLine("---Actual   outcomes---");
      foreach (string actual in actualDiff)
      {
        output.WriteLine(actual);
      }
      output.WriteLine("---Expected outcomes---");
      foreach (string expected in expectedDiff)
      {
        output.WriteLine(expected);
      }
      output.WriteLine("-----------------------");
    }

    public int Errors { get { return this.errorCount; } }
  }

  public class MethodRegressionOutcomes : BaseRegressionOutcomes
  {
    protected MethodRegressionOutcomes(Set<string> outcomes) : base(outcomes)
    {
    }

    public static MethodRegressionOutcomes GetOutcomes<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Method method, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      if (mdDecoder.GetMethodHashAttributeFlags(method).HasFlag(MethodHashAttributeFlags.IgnoreRegression))
        return new IgnoreRegressionOutcomes();

      var outcomes = new Set<string>();

      foreach (Attribute attr in mdDecoder.GetAttributes(method))
      {
        var attrType = mdDecoder.AttributeType(attr);
        if (mdDecoder.Name(attrType) != "RegressionOutcomeAttribute")
        {
          continue;
        }
        var posArgs = mdDecoder.PositionalArguments(attr);
        if (posArgs.Count == 0)
        {
          var outcome = (ProofOutcome)(byte)GetNamedArgOrDefault<int, Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(mdDecoder, "Outcome", attr);

          string msg = (string)mdDecoder.NamedArgument("Message", attr);
          int primaryOffset = GetNamedArgOrDefault<int, Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(mdDecoder, "PrimaryILOffset", attr);
          int methodOffset = GetNamedArgOrDefault<int, Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(mdDecoder, "MethodILOffset", attr);

          outcomes.Add(CanonicalFormat(outcome, msg, primaryOffset, methodOffset));
        }
        else
        {
          outcomes.Add((string)posArgs[0]);
        }
      }
      return new MethodRegressionOutcomes(outcomes);
    }

    public static int GetReanalyisCountIfAny<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Method method, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      if (mdDecoder.GetMethodHashAttributeFlags(method).HasFlag(MethodHashAttributeFlags.IgnoreRegression))
      {
        return 0;
      }

      foreach (Attribute attr in mdDecoder.GetAttributes(method))
      {
        Type attrType = mdDecoder.AttributeType(attr);
        if (mdDecoder.Name(attrType) != "RegressionReanalysisCountAttribute")
        {
          continue;
        }
        var posArgs = mdDecoder.PositionalArguments(attr);
        if (posArgs.Count != 0)
        {
          return (int)posArgs[0];
        }
      }

      return 0;
    }

    private static TargetType GetNamedArgOrDefault<TargetType, Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, string name, Attribute attr)
    {
      var outcome = mdDecoder.NamedArgument(name, attr);
      if (outcome != null) return (TargetType)outcome;
      return default(TargetType);
    }

    static string CanonicalFormat(ProofOutcome outcome, string message, int primaryILOffset, int methodILOffset)
    {
      return String.Format("Outcome=ProofOutcome.{0},Message=\"{1}\",PrimaryILOffset={2},MethodILOffset={3}", outcome.ToString(), message, primaryILOffset, methodILOffset);
    }

    public virtual bool CheckOutcome<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(IOutput output, Method method, ProofOutcome outcome, string message, int primaryILOffset, int methodILOffset, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      string canonical = CanonicalFormat(outcome, message, primaryILOffset, methodILOffset);
      this.actualOutcomes.Add(canonical);
      if (!this.expectedOutcomes.Contains(canonical))
      {
        // CCI1 hack (some offsets are off at method entries and exits) REMOVE ME ONCE WE MOVE OFF CCI1
        string canonical2 = CanonicalFormat(outcome, message, primaryILOffset, methodILOffset - 1);
        if (!this.expectedOutcomes.Contains(canonical2))
        {
          errorCount++;
          return false;
        }
      }
      return true;
    }

    public virtual bool CheckOutcome(string suggestion)
    {
      this.actualOutcomes.Add(suggestion);
      if (!this.expectedOutcomes.Contains(suggestion))
      {
        errorCount++;
        return false;
      }

      return true;
    }

    internal virtual bool CheckOutcome<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(IOutput output, Method method, System.CodeDom.Compiler.CompilerError error, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      string canonical = error.ErrorText;
      this.actualOutcomes.Add(canonical);
      if (!this.expectedOutcomes.Contains(canonical))
      {
        errorCount++;
        return false;
      }
      return true;
    }

  }

  class IgnoreRegressionOutcomes : MethodRegressionOutcomes
  {
    public IgnoreRegressionOutcomes() : base(Set<string>.Empty) { }

    public override bool Passed(bool ignoreExtra)
    {
      return true;
    }

    public override bool CheckOutcome<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(IOutput output, Method method, ProofOutcome outcome, string message, int primaryILOffset, int methodILOffset, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      return true;
    }

    public override bool CheckOutcome(string suggestion)
    {
      return true;
    }

    internal override bool CheckOutcome<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(IOutput output, Method method, System.CodeDom.Compiler.CompilerError error, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      return true;
    }
  }

  class AssemblyRegressionOutcomes : BaseRegressionOutcomes
  {
    private AssemblyRegressionOutcomes(Set<string> outcomes) : base(outcomes)
    {
    }

    public static AssemblyRegressionOutcomes GetOutcomes<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Assembly assembly, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      Set<string> outcomes = new Set<string>();

      foreach (Attribute attr in mdDecoder.GetAttributes(assembly))
      {
        Type attrType = mdDecoder.AttributeType(attr);
        if (mdDecoder.Name(attrType) != "RegressionOutcomeAttribute")
        {
          continue;
        }
        string expectedString = (string)mdDecoder.PositionalArguments(attr)[0];
        outcomes.Add(expectedString);
      }
      return new AssemblyRegressionOutcomes(outcomes);
    }


    internal bool CheckOutcome<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(IOutput output, Assembly assembly, System.CodeDom.Compiler.CompilerError error, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      string canonical = error.ErrorText;
      this.actualOutcomes.Add(canonical);
      if (!this.expectedOutcomes.Contains(canonical))
      {
        errorCount++;
        return false;
      }
      return true;
    }
  }

}
