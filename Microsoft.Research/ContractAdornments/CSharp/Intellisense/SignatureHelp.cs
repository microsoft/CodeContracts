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
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using Microsoft.Cci.Contracts;
using Microsoft.VisualStudio.TextManager.Interop;
using ContractAdornments.Interfaces;

namespace ContractAdornments {
  class SignatureHelpSource : ISignatureHelpSource {
    bool _disposed = false;
    readonly ITextBuffer _textBuffer;
    readonly TextViewTracker _textViewTracker;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_textBuffer != null);
      Contract.Invariant(_textViewTracker != null);
    }

    public SignatureHelpSource(ITextBuffer textBuffer, ITextViewTracker textViewTracker) {
      Contract.Requires(textBuffer != null);
      Contract.Requires(textViewTracker != null);

      _textBuffer = textBuffer;
      _textViewTracker = (TextViewTracker)textViewTracker;
    }

    public void AugmentSignatureHelpSession(ISignatureHelpSession session, IList<ISignature> signatures) {
      ContractsPackageAccessor.Current.Logger.PublicEntry(() => {

        //Record our start time for preformance considerations
        var startTime = DateTime.Now;

        //Do we have signatures?
        if (signatures == null || signatures.Count < 1)
          return;

        //Do we have a well-formed session?
        if (session == null || session.TextView == null || session.TextView.TextBuffer == null)
          return;
        
        //Can we get our trigger point?
        var triggerPoint = session.GetTriggerPoint(_textBuffer);
        if (triggerPoint == null)
          return;

        //Can we get our snapshot?
        var workingSnapshot = _textBuffer.CurrentSnapshot;
        if (workingSnapshot == null)
          return;

        //Can we get our SourceFile?
        var sourceFile = _textViewTracker.LatestSourceFile;
        if (sourceFile == null)
          return;

        //Can we get our ParseTree?
        var parseTree = sourceFile.GetParseTree();
        if (parseTree == null)
          return;

        //Can we get our compilation?
        var comp = _textViewTracker.LatestCompilation;
        if (comp == null)
          return;

        //Is the model ready?
        if (!parseTree.IsModelReady() || _textViewTracker.IsLatestCompilationStale || _textViewTracker.IsLatestSourceFileStale) {

          //Ask for a new model
          ContractsPackageAccessor.Current.AskForNewVSModel(_textBuffer);

          //Return a message saying we aren't ready yet
          ContractsPackageAccessor.Current.Logger.WriteToLog("The VS model is out of date! Aborting contract lookup.");
          return;//"(VS isn't ready for possible contract lookup yet. Please try again in a few seconds.)";
        }

        string[] contractContents = null;

        //Proceed cautiously
        try
        {

            //Can we get a call node?
            var callNode = IntellisenseContractsHelper.GetAnyCallNodeAboveTriggerPoint(triggerPoint, workingSnapshot, parseTree);
            if (callNode == null || comp == null || sourceFile == null)
                return;

            //Can we get our semantic member?
            var semanticMember = IntellisenseContractsHelper.GetSemanticMember(callNode, comp, sourceFile);
            if (semanticMember == null)
                return;

            var declType = semanticMember.ContainingType;
            // find all members of the same name (virt/non-virt)

            var overloads = GetSignatureOverloads(declType, signatures, semanticMember);

            contractContents = GetContractsForOverloads(overloads);


            //Give up on our contracts if we get an exception
        }
        catch (IllFormedSemanticModelException)
        {
            return;
        }
        catch (InvalidOperationException e)
        {
            if (!e.Message.Contains(ContractsPackageAccessor.InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate))
                throw e;
            else
            {
                this._textViewTracker.IsLatestCompilationStale = true;
                return;
            }
        }
        catch (System.Runtime.InteropServices.COMException)
        {
          // various reasons for COMException
          // - binding failed
          // - project unavailable
          // - ...
          return;
        }

        //Enumerate the signatures and append our custom content
        for (int i = 0; i < signatures.Count; i++) {
          var sig = signatures[i];

          if (contractContents != null && !String.IsNullOrEmpty(contractContents[i]))
          {
              signatures[i] = new SignatureWithContracts(sig, contractContents[i]);
          }
        }

        //Print our elapsed time for preformance considerations
        var elapseTime = DateTime.Now - startTime;
        ContractsPackageAccessor.Current.Logger.WriteToLog("Time to compute quickinfo: " + elapseTime.Milliseconds + "ms");
      
      }, "AugmentSignatureHelpSession");
    }

    private string[] GetContractsForOverloads(Microsoft.RestrictedUsage.CSharp.Semantics.CSharpMember[] overloads)
    {
        Contract.Requires(overloads != null);
        Contract.Requires(Contract.ForAll(overloads, o => o == null || o.IsConstructor || o.IsMethod || o.IsProperty || o.IsIndexer));

        var result = new string[overloads.Length];

        for (int i = 0; i < overloads.Length; i++)
        {
            var mem = overloads[i];
            //Can we get our contracts?
            if (mem == null) continue;

            if (mem.IsConstructor || mem.IsMethod)
            {
                IMethodContract methodContracts;
                if (!((ContractsProvider)_textViewTracker.ProjectTracker.ContractsProvider).TryGetMethodContract(mem, out methodContracts))
                    continue;

                result[i] = IntellisenseContractsHelper.FormatContracts(methodContracts);
            }
            else if (mem.IsProperty || mem.IsIndexer)
            {
                IMethodContract getter, setter;

                if (!((ContractsProvider)_textViewTracker.ProjectTracker.ContractsProvider).TryGetPropertyContract(mem, out getter, out setter))
                    continue;

                result[i] = IntellisenseContractsHelper.FormatPropertyContracts(getter, setter);
            }
        }
        return result;
    }

    private Microsoft.RestrictedUsage.CSharp.Semantics.CSharpMember[] GetSignatureOverloads(Microsoft.RestrictedUsage.CSharp.Semantics.CSharpType declType, IList<ISignature> signatures, Microsoft.RestrictedUsage.CSharp.Semantics.CSharpMember semanticMember)
    {
        Contract.Requires(signatures != null);
        Contract.Requires(declType != null);
        Contract.Requires(semanticMember != null);

        if (signatures.Count == 1 || declType.Members == null)
        {
            return new[] { semanticMember };
        }
        var result = new Microsoft.RestrictedUsage.CSharp.Semantics.CSharpMember[signatures.Count];
        for (int i = 0; i < declType.Members.Count; i++)
        {
            var mem = declType.Members[i];
            if (mem == null || !(mem.IsMethod || mem.IsConstructor || mem.IsIndexer)) continue;
            if (mem.Name != semanticMember.Name) continue;
            if (mem.IsStatic != semanticMember.IsStatic) continue;

            FuzzyIdentifySignature(mem, result, signatures);
        }

        return result;
    }

    private void FuzzyIdentifySignature(Microsoft.RestrictedUsage.CSharp.Semantics.CSharpMember mem, Microsoft.RestrictedUsage.CSharp.Semantics.CSharpMember[] result, [Pure] IList<ISignature> signatures)
    {
        Contract.Requires(signatures != null);
        Contract.Requires(result != null);
        Contract.Requires(result.Length >= signatures.Count);
        Contract.Requires(mem != null);

        for (int i = 0; i < signatures.Count; i++)
        {
            if (result[i] != null) continue;
            var sig = signatures[i];
            if (sig == null) continue;
            if (sig.Parameters == null) continue;
            if (mem.Parameters == null) continue;
            if (sig.Parameters.Count != mem.Parameters.Count) continue;

            var match = true;
            for (int j = 0; j < mem.Parameters.Count; j++)
            {
                var p = mem.Parameters[j];
                var s = sig.Parameters[j];
                if (p == null || p.Name == null || s == null || p.Name.Text != s.Name) { match = false; break; }
                // need to compare types, but how. Sig doesn't seem to give it to us
            }
            if (!match) continue;

            result[i] = mem;
            break;
        }
    }

    /// <summary>
    /// Ideally, we wouldn't have to implement this: the underlying signature help provider would do this.
    /// But because we replace the signatures in the session with our own type, the underlying provider
    /// tries to downcast them to its internal type. That fails (of course) and then it returns null. So
    /// we have to mimic its behavior which is done here by reaching into the session's properties to get
    /// the same data as the underlying signature help provider sees. So we ask for the current method
    /// that is selected and return our signature for the corresponding method.
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    public ISignature GetBestMatch(ISignatureHelpSession session) {
      return ContractsPackageAccessor.Current.Logger.PublicEntry<ISignature>(() => {
        IVsMethodData methodData = null;
        IVsMethodTipWindow3 tipWindow;
        int hr;
        if (session.Properties.TryGetProperty<IVsMethodTipWindow3>(typeof(IVsMethodTipWindow3), out tipWindow)) {
          hr = tipWindow.GetMethodData(out methodData);
          if (hr != 0 || methodData == null) {
            return null; // failed
          }
        } else {
          return null; // failed
        }
        int sigIndex = methodData.GetCurMethod();

        if ((sigIndex >= 0) && (sigIndex < session.Signatures.Count)) {
          return (session.Signatures[sigIndex]);
        }
        return null;
      }, "GetBestMatch");
    }

    public void Dispose() {
      //Public Entry
      if (!_disposed) {
        /* Not sure why we do this, this is taken from a walkthrough on MSDN
         * found here: http://msdn.microsoft.com/en-us/library/ee334194(v=VS.100).aspx
         */
        GC.SuppressFinalize(this);
        _disposed = true;
      }
    }
  }


  class SignatureWithContracts : ISignature
  {
      readonly ISignature underlying;
      readonly string contracts;

      [ContractInvariantMethod]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.underlying != null);
        Contract.Invariant(!String.IsNullOrEmpty(this.contracts));
      }

      public SignatureWithContracts(ISignature original, string contracts)
      {
          Contract.Requires(original != null);
          Contract.Requires(!String.IsNullOrEmpty(contracts));

          this.underlying = original;
          this.contracts = contracts;
      }

      public ITrackingSpan ApplicableToSpan
      {
          get { return this.underlying.ApplicableToSpan; }
      }

      public string Content
      {
          get { return underlying.Content; }
      }

      public IParameter CurrentParameter
      {
          get { return underlying.CurrentParameter; }
      }

      public event EventHandler<CurrentParameterChangedEventArgs> CurrentParameterChanged
      {
          add { this.underlying.CurrentParameterChanged += value; }
          remove { this.underlying.CurrentParameterChanged -= value; }
      }

      public string Documentation
      {
          get { return this.underlying.Documentation + "\n" + this.contracts; }
      }

      public ReadOnlyCollection<IParameter> Parameters
      {
          get { return this.underlying.Parameters; }
      }

      public string PrettyPrintedContent
      {
          get { return this.underlying.PrettyPrintedContent; }
      }
  }
}
