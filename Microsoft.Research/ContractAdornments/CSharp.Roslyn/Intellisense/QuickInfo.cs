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
using ContractAdornments.Interfaces;
using Microsoft.Cci.Contracts;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;

namespace ContractAdornments {
  class QuickInfoSource : IQuickInfoSource {
    readonly ITextBuffer _textBuffer;
    readonly TextViewTracker _textViewTracker;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_textBuffer != null);
      Contract.Invariant(_textViewTracker != null);
    }

    public QuickInfoSource(ITextBuffer textBuffer, ITextViewTracker textViewTracker) {
      Contract.Requires(textBuffer != null);
      Contract.Requires(textViewTracker != null);

      _textBuffer = textBuffer;
      _textViewTracker = (TextViewTracker)textViewTracker;
    }

    public void AugmentQuickInfoSession(IQuickInfoSession session, IList<object> quickInfoContent, out ITrackingSpan applicableToSpan) {

      if (session == null)
      {
        applicableToSpan = null;
        return;
      }

      var span = applicableToSpan = session.ApplicableToSpan;
      if (quickInfoContent == null) return;

      //Wrap our method body in a safty net that checks for exceptions
      ContractsPackageAccessor.Current.Logger.PublicEntry(() => {

        //Record our start time for preformance considerations
        var startTime = DateTime.Now;

        //Is our session valid?
        if (session == null)
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
        SyntaxTree parseTree;
        if (!sourceFile.TryGetSyntaxTree(out parseTree))
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

        //Proceed cautiously
        string formattedContracts;
        try {

          //Can we get a call node?
          var targetNode = IntellisenseContractsHelper.GetTargetAtTriggerPoint(triggerPoint, workingSnapshot, parseTree);
          if (targetNode == null)
            return;

          //Can we get our semantic member?
          var semanticMember = IntellisenseContractsHelper.GetSemanticMember(targetNode, comp, sourceFile);
          if (semanticMember == null)
            return;

          //Can we get our contracts?
          formattedContracts = GetFormattedContracts(semanticMember);
          if (formattedContracts == null)
            return;

          if (span == null)
          {
              span = workingSnapshot.CreateTrackingSpan(triggerPoint.GetPosition(workingSnapshot), 1, SpanTrackingMode.EdgeInclusive);
          }
          //Give up on our contracts if we get an exception
        } catch (IllFormedSemanticModelException) {
          return;
        } catch (InvalidOperationException e) {
            if (!e.Message.Contains(ContractsPackageAccessor.InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate))
                throw e;
            else
            {
                this._textViewTracker.IsLatestCompilationStale = true;
                return;
            }
        } catch (System.Runtime.InteropServices.COMException e) {
          // various reasons for ComExceptions:
          //  - binding failed
          //  - project unavailable
          if (e.Message.EndsWith("out of date"))
          {
            this._textViewTracker.IsLatestCompilationStale = true;
          }
          return;
        }

        //Append our formatted contract info
        quickInfoContent.Add(formattedContracts);
        //Print our elapsed time for preformance considerations
        var elapseTime = DateTime.Now - startTime;
        ContractsPackageAccessor.Current.Logger.WriteToLog("Time to compute quickinfo: " + elapseTime.Milliseconds + "ms");
      }, "AugmentQuickInfoSession");

      if (span != null) { applicableToSpan = span; }
    }

    private string GetFormattedContracts(ISymbol semanticMember)
    {
        Contract.Requires(semanticMember != null);
        Contract.Requires(semanticMember.Kind == SymbolKind.Method || semanticMember.Kind == SymbolKind.Property);

        if (semanticMember.Kind == SymbolKind.Property)
        {
            IMethodContract setter, getter;
            if (!((ContractsProvider)_textViewTracker.ProjectTracker.ContractsProvider).TryGetPropertyContract(semanticMember, out getter, out setter))
            {
                return null;
            }
            return IntellisenseContractsHelper.FormatPropertyContracts(getter, setter);
        }

        //Can we get our contracts?
        IMethodContract methodContracts;
        if (!((ContractsProvider)_textViewTracker.ProjectTracker.ContractsProvider).TryGetMethodContract(semanticMember, out methodContracts))
            return null;

        //Can we get our formatted contracts?
        return IntellisenseContractsHelper.FormatContracts(methodContracts);
    }
    public void Dispose() {
      //TODO: Dispose?
    }
  }
}