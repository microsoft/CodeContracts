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
using System.Diagnostics.Contracts;
using System.Timers;
using Adornments;
using ContractAdornments.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace ContractAdornments {
  internal sealed class TextViewTracker : ITextViewTracker {
    public static readonly object TextViewTrackerKey = TextViewTrackerAccessor.TextViewTrackerKey;
    public const double DelayOnTextViewOpened = 2000d;
    public const double DelayOnTextViewChanged = 5000d;

    public readonly IWpfTextView TextView;
    readonly IProjectTracker _projectTracker;
    public IProjectTracker ProjectTracker
    {
      get
      {
        Contract.Ensures(Contract.Result<IProjectTracker>() != null);
        return this._projectTracker;
      }
    }
    public readonly DocumentId FileName;
    readonly Timer _textBufferChangedTimer;
    public VSTextProperties VSTextProperties;

    public Document LatestSourceFile { get; private set; }
    public bool IsLatestSourceFileStale { get; set; }
    public event EventHandler<LatestSourceFileChangedEventArgs> LatestSourceFileChanged;

    public Workspace LatestCompilation { get; private set; }
    public bool IsLatestCompilationStale { get; set; }
    public event EventHandler<LatestCompilationChangedEventArgs> LatestCompilationChanged;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(TextView != null);
      Contract.Invariant(_projectTracker != null);
      Contract.Invariant(_textBufferChangedTimer != null);
    }

    internal TextViewTracker(IWpfTextView textView, IProjectTracker projectTracker, VSTextProperties vsTextProperties)
      : base() {
      Contract.Requires(textView != null);
      Contract.Requires(projectTracker != null);

      ContractsPackageAccessor.Current.ExtensionFailed += OnFailed;
      this.TextView = textView;
      if (textView.TextBuffer != null)
      {
        textView.TextBuffer.Changed += OnTextBufferChanged;
      }
      TextView.Closed += OnClosed;
      this._projectTracker = projectTracker;
      projectTracker.BuildDone += OnBuildDone;
//      ContractsPackageAccessor.Current.NewSourceFile += OnNewSourceFile;
      ContractsPackageAccessor.Current.NewCompilation += OnNewComilation;
      
      //Timer
      _textBufferChangedTimer = new System.Timers.Timer();
      _textBufferChangedTimer.AutoReset = false;
      _textBufferChangedTimer.Elapsed += OnTextViewSettled;
      _textBufferChangedTimer.Interval = DelayOnTextViewOpened; //Wait two seconds before attempting to fetch any syntactic/semantic information. This gives VS a chance to properly initialize everything.
      _textBufferChangedTimer.Start();

      //Set the text properties
      VSTextProperties = vsTextProperties;
      ContractsPackageAccessor.Current.QueueWorkItem((() => { VSTextProperties.LineHeight = TextView.LineHeight; }));

      //Set the file name
      Document document = TextView.TextSnapshot.GetOpenDocumentInCurrentContextWithChanges();
      FileName = document.Id;
    }

    void OnClosed(object sender, EventArgs e) {
      UnsubscribeFromEvents();
    }
    void OnFailed() {
      UnsubscribeFromEvents();
    }
    void UnsubscribeFromEvents() {
      //Halt the timer
      _textBufferChangedTimer.Enabled = false;
      _textBufferChangedTimer.Elapsed -= OnTextViewSettled;
      _textBufferChangedTimer.Close();

      ProjectTracker.BuildDone -= OnBuildDone;
      if (TextView.TextBuffer != null)
      {
        TextView.TextBuffer.Changed -= OnTextBufferChanged;
      }
      ContractsPackageAccessor.Current.NewCompilation -= OnNewComilation;
//      ContractsPackageAccessor.Current.NewSourceFile -= OnNewSourceFile;
      ContractsPackageAccessor.Current.ExtensionFailed -= OnFailed;
      ContractsPackageAccessor.Current.Logger.WriteToLog("TextViewTracker for '" + FileName + "' unsubscribed from all events.");
    }

    void OnBuildDone() {
        
      IsLatestSourceFileStale = true;
      IsLatestCompilationStale = true;
      ContractsPackageAccessor.Current.AskForNewVSModel(TextView.TextBuffer);
    }

    void OnTextBufferChanged(object sender, TextContentChangedEventArgs e) {
        
      try
        {// PublicEntry
        #region Start timer
        this._textBufferChangedTimer.Interval = DelayOnTextViewChanged;
        this._textBufferChangedTimer.Enabled = true;
        #endregion
      } catch (Exception exn) {
        ContractsPackageAccessor.Current.Logger.PublicEntryException(exn, "OnTextBufferChanged");
      }
    }
    void OnTextViewSettled(object sender, System.Timers.ElapsedEventArgs e) {

      ContractsPackageAccessor.Current.Logger.PublicEntry(() => {

        ContractsPackageAccessor.Current.Logger.WriteToLog("Timer elapsed. Waiting for new syntactic info.");

        ContractsPackageAccessor.Current.AskForNewVSModel(TextView.TextBuffer);

        IsLatestCompilationStale = true;
        IsLatestSourceFileStale = true;
      }, "OnTextViewSettled");
    }

    void OnNewSourceFile(Document sourceFile) {
      Contract.Requires(sourceFile != null);  

      // BUGBUG: Foxtrot1 extractor gets confused and a style-checker error is issued thinking there is code in the contract section.
      //Contract.Requires(sourceFile != null);
      //Contract.Requires(sourceFile.FileName != null);

      //Is this source file relevant to us?
      if (FileName == sourceFile.Id) {
        //Update our latest source file info
        LatestSourceFile = sourceFile;

        //Save and update our staleness
        var wasLatestSourceFileStale = IsLatestSourceFileStale;
        IsLatestSourceFileStale = false;

        //Report our latest source file
        if (LatestSourceFileChanged != null)
          LatestSourceFileChanged(this, new LatestSourceFileChangedEventArgs(wasLatestSourceFileStale, IsLatestSourceFileStale, LatestSourceFile));
      }
    }
    void OnNewComilation(object compilationObject) {
      Contract.Requires(compilationObject != null);
      Workspace comp = (Workspace)compilationObject;

      //Is this compilation relevant to us?
      Document sourceFile = comp.CurrentSolution.GetDocument(FileName);
      if (sourceFile != null)
      {
        // Update source file
        LatestSourceFile = sourceFile;
        //Save and update our staleness
        var wasLatestSourceFileStale = IsLatestSourceFileStale;
        IsLatestSourceFileStale = false;

        //Updated our latest compilation info
        LatestCompilation = comp;

        //Save and update our staleness
        var wasLatestCompilationStale = IsLatestCompilationStale;
        IsLatestCompilationStale = false;

        //Report our latest compilation
        if (LatestCompilationChanged != null)
          LatestCompilationChanged(this, new LatestCompilationChangedEventArgs(wasLatestCompilationStale, IsLatestCompilationStale, LatestCompilation));
      }
    }
    
  }
  public class LatestCompilationChangedEventArgs : EventArgs {
    public bool WasLatestCompilationStale { get; private set; }
    public bool IsLatestCompilationStale { get; private set; }
    public Workspace LatestCompilation { get; private set; }

    public LatestCompilationChangedEventArgs(bool wasLatestCompilationStale, bool isLatestCompilationStale, Workspace latestCompilation) {
      WasLatestCompilationStale = wasLatestCompilationStale;
      IsLatestCompilationStale = isLatestCompilationStale;
      LatestCompilation = latestCompilation;
    }
  }
  public class LatestSourceFileChangedEventArgs : EventArgs {
    public bool WasLatestSourceFileStale { get; private set; }
    public bool IsLatestSourceFileStale { get; private set; }
    public Document LatestSourceFile { get; private set; }

    public LatestSourceFileChangedEventArgs(bool wasLatestSourceFileStale, bool isLatestSourceFileStale, Document latestSourceFile) {
      WasLatestSourceFileStale = wasLatestSourceFileStale;
      IsLatestSourceFileStale = isLatestSourceFileStale;
      LatestSourceFile = latestSourceFile;
    }
  }
}
