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
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.CodeAnalysis
{

  abstract class SuppressWarningsManager
  {
    #region Constants
    public const string REQUIRESATCALL = "RequiresAtCall";
    public const string ENSURES = "Ensures";
    public const string ENSURESINMETHOD = "EnsuresInMethod";
    public const string INVARIANTINMETHOD = "InvariantInMethod";
    #endregion
  }

  [ContractVerification(true)]
  class SuppressWarningsManager<Method>
    : SuppressWarningsManager
  {
    public SuppressWarningsManager()
    {
    }

    [Pure]
    public string ComputeSuppressMessage(Witness witness)
    {
      return this.ComputeSuppressMessage(this.ComputeWarningMask(witness));
    }

    [Pure]
    public string ComputeSuppressMessage(string encode)
    {
      return string.Format(@"[SuppressMessage(""Microsoft.Contracts"", ""{0}"")]", encode);
    }

    [Pure]
    public string ComputeWarningMask(Witness witness)
    {
      Contract.Requires(witness != null);
      Contract.Ensures(Contract.Result<string>() != null);

      if (witness.Warning == WarningType.TestAlwaysEvaluatingToAConstant)
      {
        return "TestAlwaysEvaluatingToAConstant";
      }
      else
      {
        int offsetIL, offsetMethod;
        GetILOffsets(witness.PC, out offsetIL, out offsetMethod);

        return string.Format("{0}-{1}-{2}", witness.WarningKind, offsetIL, offsetMethod);
      }
    }

    [Pure]
    public string ComputeWarningMaskAtCallee(Witness witness)
    {
      Contract.Requires(witness != null);

      return ComputeWarningMaskInternal(witness, REQUIRESATCALL);
    }

    [Pure]
    public string ComputeWarningMaskAtBaseMethod(Witness witness)
    {
      Contract.Requires(witness != null);

      return ComputeWarningMaskInternal(witness, ENSURESINMETHOD);
    }

    [Pure]
    public string ComputeWarningMaskForInvariant(Witness witness)
    {
      Contract.Requires(witness != null);

      return ComputeWarningMaskInternal(witness, INVARIANTINMETHOD);
    }

    [Pure]
    public string ComputeWarningMaskForEnsures(Witness witness)
    {
      Contract.Requires(witness != null);

      return ComputeWarningMaskInternal(witness, ENSURES);
    }


    [Pure]
    private string ComputeWarningMaskInternal(Witness witness, string s)
    {
      Contract.Requires(witness != null);

      Method callee;
      if (witness.PC.TryGetContainingMethod(out callee))
      {
        var strEncoding = witness.PC.ExtractAssertionCondition();
        return string.Format("{0}-{1}", s, strEncoding);
      }

      return null;
    }

    [Pure]
    private void GetILOffsets(APC pc, out int primaryILOffset, out int methodILOffset)
    {
      primaryILOffset = pc.ILOffset;
      if (pc.Block == null || pc.Block.Subroutine.IsMethod)
      {
        methodILOffset = 0;
        return;
      }
      // look through context
      var context = pc.SubroutineContext;
      while (context != null)
      {
        Contract.Assume(context.Head.One != null);
        Contract.Assert(context.Head.One.Subroutine != null);

        if (context.Head.One.Subroutine.IsMethod)
        {
          string topLabel = context.Head.Three;
          if (topLabel != null && (topLabel == "exit" || topLabel == "entry" || topLabel.StartsWith("after")))
          {
            CFGBlock fromBlock = context.Head.One;
            methodILOffset = fromBlock.ILOffset(fromBlock.PriorLast);
          }
          else
          {
            CFGBlock toBlock = context.Head.Two;
            Contract.Assume(toBlock != null);
            methodILOffset = toBlock.ILOffset(toBlock.First);
          }
          return;
        }
        context = context.Tail;
      }
      methodILOffset = primaryILOffset;
    }

  }

  [ContractVerification(true)]
  class MaskingOutput<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : DerivableOutputFullResults<Method, Assembly>
  {
    #region Object invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.options != null);
      Contract.Invariant(this.mdDecoder != null);
      Contract.Invariant(this.swallowedCount != null);
      Contract.Invariant(this.suppressWarningsManager != null);
    }

    #endregion

    #region Private state

    private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
    private readonly SuppressWarningsManager<Method> suppressWarningsManager;
    private readonly GeneralOptions options;
    
    private readonly SwallowedBuckets swallowedCount;
    private bool errorsWereEmitted;
    Method currMethod;
    MaskedWarnings methodmask;
    Set<String> assemblymask;

    #endregion

    public MaskingOutput(IOutputFullResults<Method, Assembly> output, GeneralOptions options, 
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
      : base(output)
    {
      Contract.Requires(output != null);
      Contract.Requires(options != null);
      Contract.Requires(mdDecoder != null);

      this.options = options;
      this.mdDecoder = mdDecoder;
      this.currMethod = default(Method);
      this.methodmask = null;
      this.assemblymask = null;
      this.swallowedCount = new SwallowedBuckets();
      this.suppressWarningsManager = new SuppressWarningsManager<Method>();
      this.errorsWereEmitted = false;
    }

    #region IOutputFullResults<Method,Assembly> Members

    /// <summary>
    /// Holds errors that are emitted prior to any assembly being seen, so we need to wait.
    /// </summary>
    List<System.CodeDom.Compiler.CompilerError> delayedErrors;

    public override void StartAssembly(Assembly assembly)
    {
      this.assemblymask = MaskedWarnings.GetMaskedWarningsFor(assembly, mdDecoder);
      base.StartAssembly(assembly);

      if (this.delayedErrors != null)
      {
        foreach (var error in delayedErrors)
        {
          if (error == null) continue;
          this.EmitError(error);
        }
        this.delayedErrors = null;
      }
    }

    public override void EndAssembly()
    {
      base.EndAssembly();
      this.assemblymask = null;
    }

    public override void StartMethod(Method method)
    {
      this.currMethod = method;
      this.methodmask = MaskedWarnings.GetMaskedWarningsFor(
        this.assemblymask != null? new Set<string>(this.assemblymask) : new Set<string>(), 
        method, this.mdDecoder);
      base.StartMethod(method);
    }

    public override void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC)
    {
      // Collect the obsolete suppressions for warning messages
      List<string> unusedMethodMaskedWarnings;
      if(this.methodmask != null && this.methodmask.TryGetUnusedMethodMaskedWarnings(out unusedMethodMaskedWarnings))
      {
        Contract.Assert(unusedMethodMaskedWarnings != null);
        foreach (var s in unusedMethodMaskedWarnings)
        {
          Contract.Assume(s != null);

          var methodName = this.mdDecoder.Name(this.currMethod);
          var dt = this.mdDecoder.DeclaringType(this.currMethod);
          var dtName = this.mdDecoder.Name(dt);
          var nsName = this.mdDecoder.Namespace(dt);
          methodName = String.Format("{0}.{1}.{2}", nsName, dtName, methodName);

#if false
          this.WriteLine("{0}Unused suppression of a warning message: {1}",
            methodName != null? ("Method " + methodName + ": ") : "",
   ComputeSuppressMessage(s));          
#endif
          var suggestion = string.Format("{0}Unused suppression of a warning message: {1}",
            "Method " + methodName + ": ",
            this.suppressWarningsManager.ComputeSuppressMessage(s));
            this.Suggestion(ClousotSuggestion.Kind.UnusedSuppressWarning, ClousotSuggestion.Kind.UnusedSuppressWarning.Message(),
              methodEntry, suggestion, null, ClousotSuggestion.ExtraSuggestionInfo.None);
        }
      }

      this.methodmask = null;
      this.currMethod = default(Method);
      base.EndMethod(methodEntry, ignoreMethodEntryAPC);
    }

    public override int SwallowedMessagesCount(ProofOutcome outcome)
    { 
      return this.swallowedCount.GetCounter(outcome);  
    }

    #endregion

    #region IOutputResults Members

    public override bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      return EmitOutcome_Internal(false, witness, format, args);
    }

    public override bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      return EmitOutcome_Internal(true, witness, format, args);
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    private bool EmitOutcome_Internal(bool emitRelatedToo, Witness witness, string format, params object[] args)
    {
      Contract.Requires(witness != null);
      Contract.Requires(format != null);

      string encode = null;
      string encodeRequiresAtCall = null;     
      string methodNameContainingRequires = null;

      string encodeEnsures = null;
      string encodeEnsuresAtCall = null;
      string methodNameContainingEnsures = null;

      string encodeInvariant = null;
      string typeNameContainingInvariant = null;

      if (witness.Outcome != ProofOutcome.True && this.options.MaskedWarnings 
        && this.methodmask != null // We add this check as we are not enforcing the typestate
        )
      {
        encode = this.suppressWarningsManager.ComputeWarningMask(witness);

        // Is it masked?
        if (this.methodmask.IsMasked(encode))
        {
          this.methodmask.SignalMasked(encode);
          this.swallowedCount.UpdateCounter(witness.Outcome);

          return false;
        }

        // Is it a masked precondition ?
        Method m; Type t;
        if (witness.PC.InsideRequiresAtCall && witness.PC.TryGetContainingMethod(out m))
        {
          encodeRequiresAtCall = this.suppressWarningsManager.ComputeWarningMaskAtCallee(witness);
          methodNameContainingRequires = this.mdDecoder.FullName(m);
          if (MaskedWarnings.IsMaskedInTheOriginalMethod(m, this.mdDecoder, encodeRequiresAtCall))
          {
            this.methodmask.SignalMasked(encodeRequiresAtCall);
            this.swallowedCount.UpdateCounter(witness.Outcome);

            return false;
          }
        }
        // Is it a masked postcondition ?
        else if (witness.PC.InsideEnsuresInMethod) 
        {
          encodeEnsures = this.suppressWarningsManager.ComputeWarningMaskForEnsures(witness);

          if (encodeEnsures != null)
          {
            if (this.methodmask.IsMasked(encodeEnsures))
            {
              this.methodmask.SignalMasked(encodeEnsures);
              this.swallowedCount.UpdateCounter(witness.Outcome);

              return false;
            }
          }

          if (witness.PC.TryGetContainingMethod(out m))
          {
            encodeEnsuresAtCall = this.suppressWarningsManager.ComputeWarningMaskAtBaseMethod(witness);
            methodNameContainingEnsures = this.mdDecoder.FullName(m);
            if (MaskedWarnings.IsMaskedInTheOriginalMethod(m, this.mdDecoder, encodeEnsuresAtCall))
            {
              this.methodmask.SignalMasked(encodeEnsuresAtCall);
              this.swallowedCount.UpdateCounter(witness.Outcome);

              return false;
            }
          }
        }
        // Is it a masked invariant?
        else if (witness.PC.InsideInvariantInMethod &&
          witness.PC.TryGetContainingType(out t))
        {
          encodeInvariant = this.suppressWarningsManager.ComputeWarningMaskForInvariant(witness);
          typeNameContainingInvariant = this.mdDecoder.FullName(t);
          if (MaskedWarnings.IsMaskedInType(t, this.mdDecoder, encodeInvariant))
          {
            this.swallowedCount.UpdateCounter(witness.Outcome);

            return false;
          }
        }

      }

      bool result;

      if (emitRelatedToo)
      {
        result = base.EmitOutcomeAndRelated(witness, format, args);
      }
      else
      {
        result = base.EmitOutcome(witness, format, args);
      }

      if (witness.Outcome != ProofOutcome.True && this.options.OutputWarningMasks)
      {
        this.WriteLine("Method {0}: To mask the warning {1}{2}       add the attribute: {3}", 
          this.currMethod.ToString(),
          witness.PC.HasRealSourceContext? 
            "at " + witness.PC.PrimaryMethodLocation().PrimarySourceContext(): 
            "",
          "\n", // f: using "\n" instead of Environment.NewLine, because the last one caused '\n' to be printed in the output window of VS

          encodeEnsures != null?
          this.suppressWarningsManager.ComputeSuppressMessage(encodeEnsures) :
          this.suppressWarningsManager.ComputeSuppressMessage(encode));
        
        if (encodeRequiresAtCall != null && methodNameContainingRequires != null)
        {
          this.WriteLine("Method {0}: To mask *all* warnings issued like the precondition {1}{2}        add the attribute: {3} to the method {4}",
            this.currMethod.ToString(),
            witness.PC.HasRealSourceContext ?
              "at " + witness.PC.PrimaryMethodLocation().PrimarySourceContext() :
              "",
            "\n",// f: using "\n" instead of Environment.NewLine, because the last one caused '\n' to be printed in the output window of VS
            this.suppressWarningsManager.ComputeSuppressMessage(encodeRequiresAtCall), 
            methodNameContainingRequires);
        }

        if (encodeEnsuresAtCall != null && methodNameContainingEnsures != null)
        {
          this.WriteLine("Method {0}: To mask *all* warnings issued like the postcondition {1}{2}        add the attribute: {3} to the method {4}",
            this.currMethod.ToString(),
            witness.PC.HasRealSourceContext ?
              "at " + witness.PC.PrimaryMethodLocation().PrimarySourceContext() :
              "",
            "\n",  // f: using "\n" instead of Environment.NewLine, because the last one caused '\n' to be printed in the output window of VS          
            this.suppressWarningsManager.ComputeSuppressMessage(encodeEnsuresAtCall), 
            methodNameContainingEnsures);
        }

        if (encodeInvariant != null && typeNameContainingInvariant != null)
        {
          this.WriteLine("Method {0}: To mask *all* warnings issued like the invariant {1}{2}        add the attribute: {3} to the type {4}",
          this.currMethod.ToString(),
          witness.PC.HasRealSourceContext ?
            "at " + witness.PC.PrimaryMethodLocation().PrimarySourceContext() :
            "",
          "\n",// f: using "\n" instead of Environment.NewLine, because the last one caused '\n' to be printed in the output window of VS
          this.suppressWarningsManager.ComputeSuppressMessage(encodeInvariant),
          typeNameContainingInvariant);
        
        }
      }

      return result;
    }

    public override bool IsMasked(Witness witness)
    {
      if (this.options.MaskedWarnings && this.methodmask != null)
      {
        var encode = this.suppressWarningsManager.ComputeWarningMask(witness);

        // Is it masked?
        if (this.methodmask.IsMasked(encode))
        {
          return true;
        }

        // Is it a masked precondition ?
        Method m; Type t;
        if (witness.PC.InsideRequiresAtCall &&
          witness.PC.TryGetContainingMethod(out m))
        {
          var encodeRequiresAtCall = this.suppressWarningsManager.ComputeWarningMaskAtCallee(witness);
          var methodNameContainingRequires = this.mdDecoder.FullName(m);
          if (MaskedWarnings.IsMaskedInTheOriginalMethod(m, this.mdDecoder, encodeRequiresAtCall))
          {
            return true;
          }
        }
        // Is it a masked postcondition ?
        else if (witness.PC.InsideEnsuresInMethod)
        {
          var encodeEnsures = this.suppressWarningsManager.ComputeWarningMaskForEnsures(witness);

          if (encodeEnsures != null)
          {
            if (this.methodmask.IsMasked(encodeEnsures))
            {
              return true;
            }
          }

          if (witness.PC.TryGetContainingMethod(out m))
          {
            var encodeEnsuresAtCall = this.suppressWarningsManager.ComputeWarningMaskAtBaseMethod(witness);
            var methodNameContainingEnsures = this.mdDecoder.FullName(m);
            if (MaskedWarnings.IsMaskedInTheOriginalMethod(m, this.mdDecoder, encodeEnsuresAtCall))
            {
              return true;
            }
          }
        }
        // Is it a masked invariant?
        else if (witness.PC.InsideInvariantInMethod &&
          witness.PC.TryGetContainingType(out t))
        {
          var encodeInvariant = this.suppressWarningsManager.ComputeWarningMaskForInvariant(witness);
          var typeNameContainingInvariant = this.mdDecoder.FullName(t);
          if (MaskedWarnings.IsMaskedInType(t, this.mdDecoder, encodeInvariant))
          {
            return true;
          }
        }
      }

      return false;
    }

    public override void Close()
    {
      FlushErrors();
      base.Close();
    }

    public override int RegressionErrors()
    {
      FlushErrors();
      return base.RegressionErrors();
    }

    private void FlushErrors()
    {
      if (this.delayedErrors == null) return;
      // means we never got started so delayed all errors
      foreach (var e in this.delayedErrors)
      {
        if (e == null) continue;
        base.EmitError(e);
      }
    }

    #endregion

    #region IOutput Members

    public override void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      if (this.methodmask != null)
      {
        if (this.methodmask.IsMasked(suggestion))
        {
          this.methodmask.SignalMasked(suggestion);

          return;
        }
      }

      base.Suggestion(type, kind, pc, suggestion, causes, extraInfo);
    }

    public override void EmitError(System.CodeDom.Compiler.CompilerError error)
    {
      this.errorsWereEmitted = true;

      if (this.assemblymask == null)
      {
        if (this.delayedErrors == null)
        {
          this.delayedErrors = new List<System.CodeDom.Compiler.CompilerError>();
        }
        this.delayedErrors.Add(error);
      }
      else
      {
        if (this.methodmask != null)
        {
          // we are inside a method
          if (this.methodmask.IsMasked(error.ErrorText))
          {
            Contract.Assert(error.ErrorText != null);
            this.methodmask.SignalMasked(error.ErrorText);
          }
          else
          {
            // show outcome
            base.EmitError(error);
          }
        }
        else
        {
          if (this.assemblymask.Contains(error.ErrorText))
          {
            // do nothing as we do not have a valid methodmask object here...
          }
          else
          {
            // show outcome
            base.EmitError(error);
          }
        }
      }
    }

    public override bool ErrorsWereEmitted
    {
      get
      {
        return this.errorsWereEmitted || base.ErrorsWereEmitted;
      }
    }

    #endregion

    class MaskedWarnings
    {
      #region Object Invariant
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(this.mask != null);
        Contract.Invariant(this.methodMask != null);
        Contract.Invariant(this.effectivelyMasked != null);

        Contract.Invariant(this.methodMask.Count <= this.mask.Count);
      }
      #endregion

      #region Fields
      private readonly Set<string> mask;                 // The mask for the current method as they appear in the source
      private readonly Set<string> methodMask;           // The mask for the current method only. We use it to compute the unused warnings 
      private readonly Set<string> effectivelyMasked;    // The warnings effectively masked by the analysis
      #endregion

      #region Constructor
      private MaskedWarnings(Set<string> mask, Set<string> methodMask)
      {
        Contract.Requires(mask != null);
        Contract.Requires(methodMask != null);
        Contract.Requires(methodMask.Count <= mask.Count);

        this.mask = mask;
        this.methodMask = methodMask;
        this.effectivelyMasked = new Set<string>();
      }
      #endregion

      #region Instance methods

      [Pure]
      public bool IsMasked(string s)
      {
        Contract.Ensures(!Contract.Result<bool>() || s != null);

        return s != null && (this.mask.Contains(s) || this.mask.Contains(Prefix(s)));
      }

      public void SignalMasked(string s)
      {
        Contract.Requires(s != null);

        this.effectivelyMasked.Add(s);
        this.effectivelyMasked.Add(Prefix(s));
      }

      [Pure]
      public bool TryGetUnusedMethodMaskedWarnings(out List<string> warnings)
      {
        Contract.Ensures(
          !Contract.Result<bool>() || Contract.ValueAtReturn(out warnings) != null);

        var difference = this.methodMask.Difference(this.effectivelyMasked);
        if (difference.Count > 0)
        {
          warnings = new List<String>(difference.Count);
          foreach (var s in difference)
          {
            Contract.Assume(s != null);

            if (s.Contains(SuppressWarningsManager.REQUIRESATCALL) || s.Contains(SuppressWarningsManager.ENSURESINMETHOD))
            {
              // We do not track inter-method
              continue;
            }

            warnings.Add(s);
          }

          return true;
        }

        warnings = default(List<string>);
        return false;
      }

      #endregion

      #region Static methods

      [Pure]
      internal static bool IsMaskedInTheOriginalMethod(Method m,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
        string maskingString)
      {
        Contract.Requires(mdDecoder != null);
        Contract.Ensures(!Contract.Result<bool>() || maskingString != null);

        if (maskingString == null)
        {
          return false;
        }

        foreach (var attrib in mdDecoder.GetAttributes(m))
        {
          var attribType = mdDecoder.AttributeType(attrib);
          if (mdDecoder.Name(attribType) == "SuppressMessageAttribute")
          {
            var args = mdDecoder.PositionalArguments(attrib);

            Contract.Assume(args != null);
            
            if (args.Count < 2)
              return false;

            var name = (string)args[0];

            if (name == "Microsoft.Contracts" && ((args[1] as string) == maskingString))
            {
              return true;
            }
          }
        }

        return false;
      }

      [Pure]
      internal static bool IsMaskedInType(Type t,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
        string maskingString)
      {
        Contract.Requires(mdDecoder != null);
        Contract.Ensures(!Contract.Result<bool>() || maskingString != null);

        if (maskingString == null)
        {
          return false;
        }

        foreach (var attrib in mdDecoder.GetAttributes(t))
        {
          var attribType = mdDecoder.AttributeType(attrib);
          if (mdDecoder.Name(attribType) == "SuppressMessageAttribute")
          {
            var args = mdDecoder.PositionalArguments(attrib);

            Contract.Assume(args != null);

            if (args.Count < 2)
              return false;

            var name = (string)args[0];

            if (name == "Microsoft.Contracts" && ((args[1] as string) == maskingString))
            {
              return true;
            }
          }
        }

        return false;
      }

      [Pure]
      internal static MaskedWarnings GetMaskedWarningsFor(Set<string> mask, Method method, 
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
      {
        Contract.Requires(mask != null);
        Contract.Requires(mdDecoder != null);

        Contract.Ensures(Contract.Result<MaskedWarnings>() != null);

        // Add Type attributes
        var typeAttrib = mdDecoder.GetAttributes(mdDecoder.DeclaringType(method));
        Contract.Assume(typeAttrib != null);
        foreach (var attrib in typeAttrib)
        {
          string dummy;

          TryAddAttribute(mask, attrib, mdDecoder, out dummy);
        }

        // Save the mask for the warnings as we use it later
        var methodMask = new Set<string>();

        // Add Method attributes
        var methodAttrib = mdDecoder.GetAttributes(method);
        Contract.Assume(methodAttrib != null);
        foreach (var attrib in methodAttrib)
        {
          string attribName;
          if (TryAddAttribute(mask, attrib, mdDecoder, out attribName))
          {
            methodMask.Add(attribName);
          }
        }

        Contract.Assume(methodMask.Count <= mask.Count);
        return new MaskedWarnings(mask, methodMask);
      }

      [Pure]
      internal static Set<string> GetMaskedWarningsFor(Assembly assembly, 
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
      {
        Contract.Requires(mdDecoder != null);
        Contract.Ensures(Contract.Result<Set<string>>() != null);

        var mask = new Set<String>();
        var attribMask = mdDecoder.GetAttributes(assembly);
        Contract.Assume(attribMask != null);
        foreach (var attrib in attribMask)
        {
          string dummy;
          TryAddAttribute(mask, attrib, mdDecoder, out dummy);
        }

        return mask;
      }

      [Pure]
      private static bool TryAddAttribute(Set<String> mask, Attribute attrib, 
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
        out string attribName)
      {
        Contract.Requires(mask != null);
        Contract.Requires(mdDecoder != null);

        attribName = null;

        var attribType = mdDecoder.AttributeType(attrib);
        if (mdDecoder.Name(attribType) == "SuppressMessageAttribute")
        {
          var args = mdDecoder.PositionalArguments(attrib);

          Contract.Assume(args != null);

          if (args.Count < 2)
            return false;

          var name = (string)args[0];

          if (name == "Microsoft.Contracts")
          {
            attribName = (string)args[1];
            mask.Add(attribName);

            return true;
          }
        }

        return false;
      }

      [Pure]
      private static string Prefix(string s)
      {
        if (string.IsNullOrEmpty(s))
          return s;

        var prefixEnd = s.IndexOf('-');
        if (prefixEnd <= 0) return s;

        return s.Substring(0, s.IndexOf('-'));
      }
      #endregion
    }
  }
}
