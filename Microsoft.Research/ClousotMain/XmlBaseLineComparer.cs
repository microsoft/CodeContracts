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
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Microsoft.Research.DataStructures;
using System.CodeDom.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  /// <summary>
  /// Filters output based on an existing baseline. If an outcome is in the baseline, it is not emitted.
  /// </summary>
  class XmlBaseLineComparer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    : XmlBaseLine<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>, IOutputFullResults<Method, Assembly>
    where Type : IEquatable<Type>
  {
    private readonly SwallowedBuckets swallowedCounter;

    int additionalErrors;
    int methodsUnmatched;
    XElement baseLineRoot;
    CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue> cacheManager;

    public XmlBaseLineComparer(IOutputFullResults<Method, Assembly> output, GeneralOptions options, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
                               CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue> cacheManager, string xmlBaseLineFile)
      : base(output, options, mdDecoder)
    {
      this.cacheManager = cacheManager;
      try
      {
        this.baseLineRoot = XElement.Load(xmlBaseLineFile);
        // gets pool of *all* expected outcomes (currently, does not distinguish by method)
        expectedAssemblyOutcomes = BaseLineOutcomes.GetOutcomes(this.GetAssemblyRoots(), this.options.baseLineStrategy);
      }
      catch (System.Xml.XmlException)
      {
        output.WriteLine("Error: baseline file {0} is corrupted", xmlBaseLineFile);
        Environment.Exit(-1);
      }
      this.swallowedCounter = new SwallowedBuckets();
    }

    #region IOutputFullResults<Method> Members
    public string Name { get { return this.output.Name; } }

    public int SwallowedMessagesCount(ProofOutcome outcome)
    {
      return this.swallowedCounter.GetCounter(outcome);
    }

    Assembly currentAssembly;
    Method currentMethod;
    BaseLineOutcomes expectedMethodOutcomes; // initialied when entering a method
    BaseLineOutcomes expectedAssemblyOutcomes; // initialized during entire run

    public void StartAssembly(Assembly assembly)
    {
      output.StartAssembly(assembly);
      this.currentAssembly = assembly;
    }

    public void EndAssembly()
    {
      this.currentAssembly = default(Assembly);
      output.EndAssembly();
    }

    public void StartMethod(Method method)
    {
      output.StartMethod(method);
      IEnumerable<XElement> methodRoots = this.GetMethodRoots(method);
      if (!methodRoots.Any())
      {
        //output.WriteLine("No matches for method {0} ", mdDecoder.FullName(method));
        methodsUnmatched++;
      }
      else
      {
        //output.WriteLine("Found baseline for method {0} ", mdDecoder.FullName(method));
      }
      expectedMethodOutcomes = BaseLineOutcomes.GetOutcomesForMethod(methodRoots, this.options.baseLineStrategy); //BaseLineOutcomes.GetOutcomes(methodRoots, this.options.baseLineStrategy);
      this.currentMethod = method;
    }

    public void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC = false)
    {
      if (!expectedMethodOutcomes.Passed)
      {
        output.WriteLine("Method {0} has new failures", mdDecoder.FullName(this.currentMethod));
      }
      this.additionalErrors += expectedMethodOutcomes.Errors;
      output.EndMethod(methodEntry, ignoreMethodEntryAPC);
      expectedMethodOutcomes = null;
    }

    public bool IsMasked(Witness witness)
    {
      return this.output.IsMasked(witness);
    }


    public void WriteLine(string format, params object[] args)
    {
      output.WriteLine(format, args);
    }

    public void Statistic(string format, params object[] args)
    {
      output.Statistic(format, args);
    }

    public void FinalStatistic(string assemblyName, string msg)
    {
      output.FinalStatistic(assemblyName, msg);
    }

    public void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      int primaryILOffset, methodILOffset;
      GetILOffsets(pc, this.currentMethod, out primaryILOffset, out methodILOffset);
      if (!this.expectedMethodOutcomes.CheckOutcome(WarningKind.Informational, output, this.currentMethod, ProofOutcome.Bottom, suggestion, primaryILOffset, methodILOffset, this.mdDecoder, this.cacheManager))
      {
        // show suggestion
        output.Suggestion(type, kind, pc, suggestion, causes, extraInfo);
        // don't count as an error
      }
    }

    public bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      string message = String.Format(format, args);
      int primaryILOffset, methodILOffset;
      GetILOffsets(witness.PC, this.currentMethod, out primaryILOffset, out methodILOffset);
      // if (!this.expectedMethodOutcomes.CheckOutcome(witness.WarningKind, output, this.currentMethod, witness.Outcome, message, primaryILOffset, methodILOffset, this.mdDecoder))
      if (!this.expectedMethodOutcomes.CheckOutcome(witness.WarningKind, output, this.currentMethod, witness.Outcome, message, primaryILOffset, methodILOffset, this.mdDecoder, this.cacheManager))
      {
        // show outcome
        var result = output.EmitOutcome(witness, format, args);
        if (!result)
        {
          this.swallowedCounter.UpdateCounter(witness.Outcome);
        }
        return result;
      }
      else
      {
        this.swallowedCounter.UpdateCounter(witness.Outcome);
        return false;
      }
    }

    public bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      string message = String.Format(format, args);
      int primaryILOffset, methodILOffset;
      GetILOffsets(witness.PC, this.currentMethod, out primaryILOffset, out methodILOffset);
      if (!this.expectedMethodOutcomes.CheckOutcome(witness.WarningKind, output, this.currentMethod, witness.Outcome, message, primaryILOffset, methodILOffset, this.mdDecoder, this.cacheManager))
      {
        // show outcome
        var result = output.EmitOutcomeAndRelated(witness, format, args);
        if (!result)
        {
          this.swallowedCounter.UpdateCounter(witness.Outcome);
        }
        return result;

      }
      else
      {
        this.swallowedCounter.UpdateCounter(witness.Outcome);
        return false;
      }
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
          methodILOffset = context.Head.Two.First.ILOffset;
          if (methodILOffset == 0)
          {
            methodILOffset = context.Head.One.PriorLast.ILOffset;
          }
          return;
        }
        context = context.Tail;
      }
      methodILOffset = primaryILOffset;
    }


    public void EmitError(System.CodeDom.Compiler.CompilerError error)
    {
      if (this.expectedAssemblyOutcomes == null)
      {
        throw new ApplicationException("got error but assembly outcomes not initialized");
      }
      else
      {
        if (this.expectedMethodOutcomes != null)
        {
          // we are inside a method
          if (!this.expectedMethodOutcomes.CheckOutcome(error))
          {
            // show outcome
            output.EmitError(error);
          }
        }
        else
        {
          if (!this.expectedAssemblyOutcomes.CheckOutcome(error))
          {
            // show outcome
            output.EmitError(error);
          }
        }
      }
    }

    public void Close()
    {
      output.Close();
    }

    public int RegressionErrors()
    {
      int localAdditionalErrors = additionalErrors + expectedAssemblyOutcomes.Errors;
      int total = localAdditionalErrors;
      if (total == 0)
      {
        output.WriteLine("No failures in addition to baseline.");
      }
      else
      {
        output.WriteLine("Run has {0} additional failures over baseline. New errors in {1}", localAdditionalErrors.ToString(), output.Name);
        output.WriteLine("Failed to match {0} methods", methodsUnmatched);
      }
      return total;
    }

    IFrameworkLogOptions IOutput.LogOptions { get { return this.options; } }
    public ILogOptions LogOptions
    {
      get { return this.options; }
    }

    public bool ErrorsWereEmitted
    {
      get { return this.output.ErrorsWereEmitted; }
    }

    #endregion


    #region Locating all method outcomes

    private IEnumerable<XElement> GetMethodRoots(Method method)
    {
      string canonicalMethodName = CanonicalMethodName(method, this.mdDecoder);

      IEnumerable<XElement> result =
        from elem in baseLineRoot.Descendants("Method")
        where
        (string)elem.Attribute("Name") == canonicalMethodName
        select elem;

      return result;
    }

    #endregion

    #region Locating assembly outcomes
    private IEnumerable<XElement> GetAssemblyRoots()
    {
      yield return baseLineRoot;
    }

    #endregion

    private class BaseLineOutcomes
    {
      Set<string> expectedOutcomes;
      Set<string> actualOutcomes = new Set<string>();

      int errorCount = 0;
      // string representation of the Clousot hash of the current method
      string methodHash;
      // hack to prevent multiple counting of method hash match failures. has to be done because when we construct the baseline outcomes for a method, we haven't computed its hash yet...
      bool checkedHashMatch = false;
      BaseLiningOptions baselineStrategy;

      public BaseLineOutcomes(Set<string> outcomes, BaseLiningOptions baselineStrategy)
      {
        this.expectedOutcomes = outcomes;
        this.baselineStrategy = baselineStrategy;
      }

      public BaseLineOutcomes(Set<string> outcomes, BaseLiningOptions baselineStrategy, string methodHash)
      {
        this.expectedOutcomes = outcomes;
        this.baselineStrategy = baselineStrategy;
        this.methodHash = methodHash;
      }

      /*
      public bool MatchesMethodHash(string otherHash)
      {
          return methodHash == otherHash;
      }
      */

      public static BaseLineOutcomes GetOutcomesForMethod(IEnumerable<XElement> methodElements, BaseLiningOptions baselineStrategy)
      {
        Set<string> outcomes = new Set<string>();
        string hash = null;

        Contract.Assert(methodElements.Count() < 2, string.Format("Not sure what to do with {0} method elements ", methodElements.Count()));

        // TODO: make more LINQ-y and concise
        foreach (XElement methodElement in methodElements)
        {
          // save the hash of this element so we can check if it has changed in the current version
          var hashElem = methodElement.Attribute("Hash");
          hash = hashElem != null ? hashElem.Value : "NONE";
          foreach (XElement outcomeElement in methodElement.Elements("Outcome"))
          {
            outcomes.Add(Canonicalize(methodElement.FirstAttribute.Value, outcomeElement, baselineStrategy));
          }
        }

        return new BaseLineOutcomes(outcomes, baselineStrategy, hash);
      }

      public static BaseLineOutcomes GetOutcomes(IEnumerable<XElement> methodElements, BaseLiningOptions baselineStrategy)
      {
        /*
        IEnumerable<string> outcomeStrings =
            from o in methodElements.Elements("Outcome") select Canonicalize(o, baselineStrategy);
        */
        Set<string> outcomes = new Set<string>();

        // TODO: make more LINQ-y and concise
        foreach (XElement methodElement in methodElements)
        {
          var outcomeElems = methodElement.Elements("Outcome");
          if (methodElement.FirstAttribute != null)
          {
            // get method name if the elements are associated with a particular method, else they are associated with the assembly as a whole
            string methodName = methodElement.FirstAttribute != null ? methodElement.FirstAttribute.Value : "Assembly";
            foreach (XElement outcomeElement in outcomeElems)
            {
              outcomes.Add(Canonicalize(methodName, outcomeElement, baselineStrategy));
            }
          }
        }
        return new BaseLineOutcomes(outcomes, baselineStrategy);
      }

      static string Canonicalize(String methodName, XElement outcomeElement, BaseLiningOptions baselineStrategy)
      {
        var outcomeKind = (string)outcomeElement.Attribute("Outcome");
        if (outcomeKind != null)
        {
          ProofOutcome outcome = (ProofOutcome)Enum.Parse(typeof(ProofOutcome), (string)outcomeElement.Attribute("Outcome"), true);
          WarningKind kind = (WarningKind)Enum.Parse(typeof(WarningKind), (string)outcomeElement.Attribute("Kind"), true);
          string message = (string)outcomeElement.Attribute("Message");
          int primaryILOffset = Int32.Parse((string)outcomeElement.Attribute("PrimaryILOffset"), System.Globalization.NumberStyles.HexNumber);
          int methodILOffset = Int32.Parse((string)outcomeElement.Attribute("MethodILOffset"), System.Globalization.NumberStyles.HexNumber);
          return CanonicalFormat(methodName, kind, outcome, message, primaryILOffset, methodILOffset, baselineStrategy);
        }
        else
        {
          return (string)outcomeElement.Attribute("Message");
        }
      }

      static string CanonicalFormat(string methodName, WarningKind kind, ProofOutcome outcome, string message, int primaryILOffset, int methodILOffset, BaseLiningOptions baselineStrategy)
      {
        switch (baselineStrategy)
        {
          case BaseLiningOptions.ilBased:
            return String.Format("{0}:{1} PrimaryIL={2} MethodIL={3}", methodName, message, primaryILOffset, methodILOffset);
          case BaseLiningOptions.typeBased:
            return String.Format("{0}:{1}", methodName, kind.ToString());
          case BaseLiningOptions.mixed:
          default:
            switch (kind)
            {
              case WarningKind.Invariant:
              case WarningKind.Assert:
              case WarningKind.Requires:
              case WarningKind.Ensures:
                return String.Format("{0} PrimaryIL={1}", message, primaryILOffset);
              default:
                return message;
            }
        }
      }

      public bool CheckOutcome(CompilerError error)
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
      public bool CheckOutcome(WarningKind kind, IOutput output, Method method, ProofOutcome outcome, string message, int primaryILOffset, int methodILOffset,
                               IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
                               CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue> cacheManager)
      {
        string canonicalMethodName = XmlBaseLine<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.CanonicalMethodName(method, mdDecoder);
        string canonical = CanonicalFormat(canonicalMethodName, kind, outcome, message, primaryILOffset, methodILOffset, baselineStrategy);
        this.actualOutcomes.Add(canonical);

        // HACK to avoid modifying tons of Clousot code
        if (cacheManager != null && !checkedHashMatch)
        {
          if (this.methodHash != cacheManager.MethodHashAsString())
          {
            // the method names match, but their hashes do not. this means the programmer has changed the method *without* renaming it
            //TextWriter errorWriter = Console.Error;
            //errorWriter.WriteLine(string.Format("<Hash/Name Discrepancy>{0}</HashName Discrepancy>", method));
            //Console.WriteLine(string.Format("<Hash/Name Discrepancy>{0}</HashName Discrepancy>", method));
          }
          checkedHashMatch = true;
        }
        if (!this.expectedOutcomes.Contains(canonical))
        {
          if (kind != WarningKind.Informational)
          {
            errorCount++;
          }
          return false;
        }
        return true;
      }


      public bool Passed
      {
        get
        {
          if (this.errorCount == 0)
          {
#if CheckForExtraneousOutcomes
            if (this.actualOutcomes.Count < this.expectedOutcomes.Count)
            {
              this.errorCount++;
            }
#endif
          }
          return this.errorCount == 0;
        }
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
  }
}
