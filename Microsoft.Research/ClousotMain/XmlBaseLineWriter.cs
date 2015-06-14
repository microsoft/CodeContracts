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
using System.Xml;
using Microsoft.Research.DataStructures;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Microsoft.Research.CodeAnalysis
{
  class XmlBaseLineWriter<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    : XmlBaseLine<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>, IOutputFullResults<Method, Assembly>
    where Type : IEquatable<Type>
  {
    XmlWriter xmlWriter;
    string targetFileName;
    CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue> cacheManager;

    public XmlBaseLineWriter(IOutputFullResults<Method, Assembly> output, GeneralOptions options, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
                             CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC,SymbolicValue>, SymbolicValue> cacheManager, string targetFileName)
                : base(output, options, mdDecoder) 
              
    {
      this.targetFileName = targetFileName;
      this.cacheManager = cacheManager;
      var settings = new XmlWriterSettings();
      settings.Indent = true;
      this.xmlWriter = XmlWriter.Create(targetFileName, settings);
      this.xmlWriter.WriteStartElement("Assembly");
    }


    #region IOutputResults Members

    /// <summary>
    /// This output does not swallow anything
    /// </summary>
    /// <param name="outcome"></param>
    /// <returns></returns>
    public int SwallowedMessagesCount(ProofOutcome outcome)
    {
      return 0; 
    }

    public string Name { get { return this.targetFileName; } }

    Method currentMethod;
    bool currentMethodHasFailures; // set to true on first failure
    bool insideMethod; // true during methods

    public void StartMethod(Method method)
    {
      this.currentMethod = method;
      this.insideMethod = true;
      this.currentMethodHasFailures = false;
      output.StartMethod(method);
    }

    private void EnsureMethodStart()
    {
      if (!currentMethodHasFailures)
      {
        currentMethodHasFailures = true;
        xmlWriter.WriteStartElement("Method");
        xmlWriter.WriteAttributeString("Name", XmlBaseLine<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>.CanonicalMethodName(this.currentMethod, this.mdDecoder));
        // can only get method hash if we have a cache manager to compute it with...
        if (cacheManager != null) xmlWriter.WriteAttributeString("Hash", cacheManager.MethodHashAsString());
      }
    }

    private void EnsureMethodEnd()
    {
      if (currentMethodHasFailures)
      {
        xmlWriter.WriteEndElement();
      }
    }


    public void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC=false)
    {
      output.EndMethod(methodEntry, ignoreMethodEntryAPC);
      EnsureMethodEnd();
      this.insideMethod = false;
    }

    public void StartAssembly(Assembly assembly)
    {
      output.StartAssembly(assembly);
    }

    public void EndAssembly()
    {
      output.EndAssembly();
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
      EnsureMethodStart();

      // show suggestion
      output.Suggestion(type, kind, pc, suggestion, causes, extraInfo);

      // put it into baseline
      EmitOutcomeToBaseLine(WarningKind.Informational, ProofOutcome.Bottom, pc, suggestion);
    }

    
    public bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      EnsureMethodStart();

      // show output
      var result = output.EmitOutcome(witness, format, args);

      // put it into baseline
      EmitOutcomeToBaseLine(witness.WarningKind, witness.Outcome, witness.PC, format, args);

      return result;
    }

    public bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      EnsureMethodStart();

      // show output
      var result = output.EmitOutcomeAndRelated(witness, format, args);

      // put it into baseline (no related locations)
      EmitOutcomeToBaseLine(witness.WarningKind, witness.Outcome, witness.PC, format, args);

      return result;
    }

    private void EmitOutcomeToBaseLine(WarningKind kind, ProofOutcome outcome, APC pc, string format, object[] args)
    {
      var message = String.Format(format, args);
      EmitOutcomeToBaseLine(kind, outcome, pc, message);
    }
    private void EmitOutcomeToBaseLine(WarningKind kind, ProofOutcome outcome, APC pc, string message)
    {
      int primaryILOffset, methodILOffset;
      GetILOffsets(pc, this.currentMethod, out primaryILOffset, out methodILOffset);
      xmlWriter.WriteStartElement("Outcome");
      xmlWriter.WriteAttributeString("Kind", kind.ToString());
      xmlWriter.WriteAttributeString("Outcome", outcome.ToString());
      xmlWriter.WriteAttributeString("Message", message);
      xmlWriter.WriteAttributeString("PrimaryILOffset", primaryILOffset.ToString("x"));
      xmlWriter.WriteAttributeString("MethodILOffset", methodILOffset.ToString("x"));
      xmlWriter.WriteEndElement();
    }
    private void EmitOutcomeToBaseLine(CompilerError error)
    {
      xmlWriter.WriteStartElement("Outcome");
      xmlWriter.WriteAttributeString("Message", error.ErrorText);
      xmlWriter.WriteEndElement();
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
      output.EmitError(error);
      if (insideMethod)
      {
        EnsureMethodStart();
      }
      EmitOutcomeToBaseLine(error);
    }

    public bool IsMasked(Witness witness)
    {
      return this.output.IsMasked(witness);
    }


    public void Close()
    {
      output.Close();
      this.xmlWriter.WriteEndElement();
      this.xmlWriter.Close();
    }

    public int RegressionErrors()
    {
      // baseline writer absorbs all errors, so don't return intrinsic ones
      return 0;
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



  }


}
