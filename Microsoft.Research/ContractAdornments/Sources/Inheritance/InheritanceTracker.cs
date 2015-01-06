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
using System.Text;
using Microsoft.RestrictedUsage.CSharp;
using Microsoft.RestrictedUsage.CSharp.Compiler;
using Microsoft.RestrictedUsage.CSharp.Extensions;
using Microsoft.RestrictedUsage.CSharp.Semantics;
using Microsoft.RestrictedUsage.CSharp.Syntax;
using Microsoft.Cci.Contracts;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Outlining;
using Adornments;
using Microsoft.Cci;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using UtilitiesNamespace;
using System.Collections.ObjectModel;

namespace ContractAdornments {
  internal sealed class InheritanceTracker {
    public const string InheritanceTrackerKey = "InheritanceTracker";
    public const double DelayOnVSModelFailedBeforeTryingAgain = 500d;
    public const double DelayAfterMembersRevalutation = 500d;

    readonly TextViewTracker _textViewTracker;
    readonly AdornmentManager _adornmentManager;

    readonly Queue<KeyValuePair<object, MethodDeclarationNode>> _methodsNeedingContractLookup;
    readonly Queue<KeyValuePair<Tuple<object, object>, PropertyDeclarationNode>> _propertiesNeedingContractLookup;
    readonly ISet<object> _methodKeys;
    readonly ISet<object> _propertyKeys;

    int semanticModelsFetchedCounter = 0;
    bool trackingNumberOfFetchedSemanticModels = false;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_textViewTracker != null);
      Contract.Invariant(_adornmentManager != null);
      Contract.Invariant(_methodsNeedingContractLookup != null);
      Contract.Invariant(_propertiesNeedingContractLookup != null);
      Contract.Invariant(_methodKeys != null);
      Contract.Invariant(_propertyKeys != null);
    }

    #region Static getters
    [ContractVerification(false)]
    public static InheritanceTracker GetOrCreateAdornmentTracker(TextViewTracker textViewTracker) {
      Contract.Requires(textViewTracker != null);
      Contract.Ensures(Contract.Result<InheritanceTracker>() != null);
      return textViewTracker.TextView.Properties.GetOrCreateSingletonProperty<InheritanceTracker>(InheritanceTrackerKey, delegate { return new InheritanceTracker(textViewTracker); });
    }
    #endregion

    private InheritanceTracker(TextViewTracker textViewTracker) {
      Contract.Requires(textViewTracker != null);
      Contract.Requires(textViewTracker.TextView != null);
      if (!AdornmentManager.TryGetAdornmentManager(textViewTracker.TextView, "InheritanceAdornments", out _adornmentManager)) {
        VSServiceProvider.Current.Logger.WriteToLog("Inheritance adornment layer not instantiated.");
        throw new InvalidOperationException("Inheritance adornment layer not instantiated.");
      }

      _methodsNeedingContractLookup = new Queue<KeyValuePair<object, MethodDeclarationNode>>();
      _propertiesNeedingContractLookup = new Queue<KeyValuePair<Tuple<object, object>, PropertyDeclarationNode>>();
      _methodKeys = new HashSet<object>();
      _propertyKeys = new HashSet<object>();

      _textViewTracker = textViewTracker;
      _textViewTracker.LatestCompilationChanged += OnLatestCompilationChanged;
      _textViewTracker.LatestSourceFileChanged += OnLatestSourceFileChanged;
      _textViewTracker.ProjectTracker.BuildDone += OnBuildDone;
      _textViewTracker.TextView.Closed += OnClosed;
    }

    void OnClosed(object sender, EventArgs e) {
      _textViewTracker.ProjectTracker.BuildDone -= OnBuildDone;
      _textViewTracker.LatestSourceFileChanged -= OnLatestSourceFileChanged;
      _textViewTracker.LatestCompilationChanged -= OnLatestCompilationChanged;
    }

    void OnBuildDone() {
      VSServiceProvider.Current.Logger.WriteToLog("Removing all old adornments because of a new build for text file: " + _textViewTracker.FileName);
      _adornmentManager.Adornments.Clear();
      _methodsNeedingContractLookup.Clear();
      _propertiesNeedingContractLookup.Clear();
      _methodKeys.Clear();
      _propertyKeys.Clear();
    }

    void OnLatestSourceFileChanged(object sender, LatestSourceFileChangedEventArgs e) {

      //Is the source file change significant?
      if (e.WasLatestSourceFileStale == true) {

        //Revaluate the inheritance adornments on the text view when we next have focus
        if (VSServiceProvider.Current.VSOptionsPage.InheritanceOnMethods)
          VSServiceProvider.Current.QueueWorkItem(() => RevaluateMethodInheritanceAdornments(this), () => _textViewTracker.TextView.HasAggregateFocus);
        if (VSServiceProvider.Current.VSOptionsPage.InheritanceOnProperties)
          VSServiceProvider.Current.QueueWorkItem(() => RevaluatePropertyInheritanceAdornments(this), () => _textViewTracker.TextView.HasAggregateFocus);
      }
    }
    void OnLatestCompilationChanged(object sender, LatestCompilationChangedEventArgs e) {

      Contract.Requires(e.LatestCompilation != null);

      //Do any methods need their contract information looked up?
      if (VSServiceProvider.Current.VSOptionsPage.InheritanceOnMethods && _methodsNeedingContractLookup.Count > 0) {

        //Recursively look up the needed contract information
        VSServiceProvider.Current.Logger.WriteToLog("Attempting to lookup contracts for " + _methodsNeedingContractLookup.Count + " methods.");
        VSServiceProvider.Current.QueueWorkItem(() => RecursivelyLookupContractsForMethods(this), () => _textViewTracker.TextView.HasAggregateFocus);
      }

      //Do any properties need their contract information looked up?
      if (VSServiceProvider.Current.VSOptionsPage.InheritanceOnProperties && _propertiesNeedingContractLookup.Count > 0) {

        //Recursively look up the needed contract information
        VSServiceProvider.Current.Logger.WriteToLog("Attempting to lookup contracts for " + _propertiesNeedingContractLookup.Count + " properties.");
        VSServiceProvider.Current.QueueWorkItem(() => RecursivelyLookupContractsForProperties(this), () => _textViewTracker.TextView.HasAggregateFocus);
      }
    }

    static void RevaluateMethodInheritanceAdornments(InheritanceTracker @this) {
      Contract.Requires(@this != null);

      var workingSnapshot = @this._textViewTracker.TextView.TextSnapshot;  //Save the current snapshot so it doesn't change while you work, we assume that the snapshot is the same as the source file.

      //Check if model is ready
      var parseTree = @this._textViewTracker.LatestSourceFile == null ? null : @this._textViewTracker.LatestSourceFile.GetParseTree();
      if (parseTree == null || !VSServiceProvider.IsModelReady(parseTree)) {
        @this._textViewTracker.IsLatestSourceFileStale = true;
        Utilities.Delay(() => VSServiceProvider.Current.AskForNewVSModel(), DelayOnVSModelFailedBeforeTryingAgain);
        return;
      }

      //Collect all the methods in this text view
      var methodCollector = new MethodCollector(workingSnapshot);
      methodCollector.Visit(parseTree.RootNode);
      var methods = methodCollector.GetMethods();

      //Calculate which methods are new
      var newKeys = new List<object>(methods.Keys.Where((k) => !@this._methodKeys.Contains(k)));
      VSServiceProvider.Current.Logger.WriteToLog(String.Format("Found {0} new methods.", newKeys.Count));

      //Update our method keys
      @this._methodKeys.Clear();
      @this._methodKeys.AddAll(methods.Keys);

      VSServiceProvider.Current.QueueWorkItem(() => {
        if (@this._textViewTracker.TextView.IsClosed)
          return;
        var adornmentKeys = new List<object>(@this._adornmentManager.Adornments.Keys);
        foreach (var key in adornmentKeys) {
          var keyAsString = key as string;
          if (keyAsString == null) {
            VSServiceProvider.Current.Logger.WriteToLog("Unexpected: A key in the AdornmentManager wasn't a string! key: " + key.ToString());
            continue;
          }
          if (!@this._methodKeys.Contains(key) && keyAsString.EndsWith(MethodCollector.MethodTagSuffix)) {
            @this._adornmentManager.RemoveAdornment(key);
            VSServiceProvider.Current.Logger.WriteToLog("Removing obsolete method adornment with tag: " + keyAsString);
          }
        }
      }, () => @this._textViewTracker.TextView.IsClosed || @this._textViewTracker.TextView.HasAggregateFocus);

      //Create placeholder adornments for our new methods and queue them for contract lookup
      VSServiceProvider.Current.QueueWorkItem(() => {
        foreach (var key in newKeys) {
          MethodDeclarationNode method;
          if (methods.TryGetValue(key, out method)) {
            VSServiceProvider.Current.Logger.WriteToLog("Creating placeholder adornment and enqueueing for future contract lookup for: " + key.ToString());
            #region Create placeholder adornment
            //We add the placeholder adornment here because our workingSnapshot corresponds most closely to the syntactic model's text
            var snapshotSpan = new SnapshotSpan(method.GetSpan().Convert(workingSnapshot).Start, 1);
            var trackingSpan = workingSnapshot.CreateTrackingSpan(snapshotSpan.Span, SpanTrackingMode.EdgeExclusive);
            var ops = AdornmentOptionsHelper.GetAdornmentOptions(VSServiceProvider.Current.VSOptionsPage);
            @this._adornmentManager.AddAdornment(new InheritanceContractAdornment(trackingSpan, @this._textViewTracker.VSTextProperties, VSServiceProvider.Current.Logger, @this._adornmentManager.QueueRefreshLineTransformer, ops), key);
            #endregion
            @this._methodsNeedingContractLookup.Enqueue(new KeyValuePair<object, MethodDeclarationNode>(key, method));
          }
        }
      });

      //Most likely we've changed something (and this is a pretty cheap call), so let's ask for a refresh
      Utilities.Delay(() => VSServiceProvider.Current.QueueWorkItem(@this._adornmentManager.QueueRefreshLineTransformer), DelayAfterMembersRevalutation);

      //Ask for the new VS model so we can look up contracts
      Utilities.Delay(() => VSServiceProvider.Current.QueueWorkItem(VSServiceProvider.Current.AskForNewVSModel), DelayAfterMembersRevalutation);
    }
    static void RevaluatePropertyInheritanceAdornments(InheritanceTracker @this) {
      Contract.Requires(@this != null);

      var workingSnapshot = @this._textViewTracker.TextView.TextSnapshot;  //Save the current snapshot so it doesn't change while you work, we assume that the snapshot is the same as the source file.

      //Check if model is ready
      var parseTree = @this._textViewTracker.LatestSourceFile == null ? null : @this._textViewTracker.LatestSourceFile.GetParseTree();
      if (parseTree == null || !VSServiceProvider.IsModelReady(parseTree)) {
        @this._textViewTracker.IsLatestSourceFileStale = true;
        Utilities.Delay(() => VSServiceProvider.Current.AskForNewVSModel(), DelayOnVSModelFailedBeforeTryingAgain);
        return;
      }

      //Collect all the properties in this text view
      var propertyCollector = new PropertyCollector(workingSnapshot);
      propertyCollector.Visit(parseTree.RootNode);
      var properties = propertyCollector.GetProperties();

      //Get our property keys
      var keys = new List<object>();
      var newTuples = new List<Tuple<object, object>>();
      foreach (var tuple in properties.Keys) {
        if (tuple.Item1 != null)
          keys.Add(tuple.Item1);
        if (tuple.Item2 != null)
          keys.Add(tuple.Item2);
        if (!(@this._propertyKeys.Contains(tuple.Item1) && @this._propertyKeys.Contains(tuple.Item1)))
          newTuples.Add(tuple);
      }

      VSServiceProvider.Current.Logger.WriteToLog(String.Format("Found {0} new properties.", newTuples.Count));

      //Update our property keys
      @this._propertyKeys.Clear();
      @this._propertyKeys.AddAll(keys);

      VSServiceProvider.Current.QueueWorkItem(() => {
        if (@this._textViewTracker.TextView.IsClosed)
          return;
        var adornmentKeys = new List<object>(@this._adornmentManager.Adornments.Keys);
        foreach (var key in adornmentKeys) {
          var keyAsString = key as string;
          if (keyAsString == null) {
            VSServiceProvider.Current.Logger.WriteToLog("Unexpected: A key in the AdornmentManager wasn't a string! key: " + key.ToString());
            continue;
          }
          if (!@this._propertyKeys.Contains(key) && keyAsString.EndsWith(PropertyCollector.PropertyTagSuffix)) {
            @this._adornmentManager.RemoveAdornment(key);
            VSServiceProvider.Current.Logger.WriteToLog("Removing obsolete property adornment with tag: " + keyAsString);
          }
        }
      }, () => @this._textViewTracker.TextView.IsClosed || @this._textViewTracker.TextView.HasAggregateFocus);

      //Create placeholder adornments for our new properties and queue them for contract lookup
      VSServiceProvider.Current.QueueWorkItem(() => {
        foreach (var tuple in newTuples) {
          PropertyDeclarationNode property;
          if (properties.TryGetValue(tuple, out property)) {
            if (tuple.Item1 != null && tuple.Item2 != null &&
                property.GetAccessorDeclaration.GetSpan().Start.Line == property.SetAccessorDeclaration.GetSpan().Start.Line)
            {
              // set and get on same line, don't add adornment
              VSServiceProvider.Current.Logger.WriteToLog("Skipping adornments for " + property.GetName(workingSnapshot) + " because get and set are on same line");
            }
            else
            {
              VSServiceProvider.Current.Logger.WriteToLog("Creating placeholder adornment and enqueueing for future contract lookup for: " + property.GetName(workingSnapshot));
              if (tuple.Item1 != null)
              {
                #region Create getter placeholder adornment
                VSServiceProvider.Current.Logger.WriteToLog(String.Format("\t(Creating getter placeholder with tag {0})", tuple.Item1));
                //We add the placeholder adornment here because our workingSnapshot corresponds most closely to the syntactic model's text
                var snapshotSpan = new SnapshotSpan(property.GetAccessorDeclaration.GetSpan().Convert(workingSnapshot).Start, 1);
                var trackingSpan = workingSnapshot.CreateTrackingSpan(snapshotSpan.Span, SpanTrackingMode.EdgeExclusive);
                var ops = AdornmentOptionsHelper.GetAdornmentOptions(VSServiceProvider.Current.VSOptionsPage);
                @this._adornmentManager.AddAdornment(new InheritanceContractAdornment(trackingSpan, @this._textViewTracker.VSTextProperties, VSServiceProvider.Current.Logger, @this._adornmentManager.QueueRefreshLineTransformer, ops), tuple.Item1);
                #endregion
              }
              if (tuple.Item2 != null)
              {
                #region Create setter placeholder adornment
                VSServiceProvider.Current.Logger.WriteToLog(String.Format("\t(Creating setter placeholder with tag {0})", tuple.Item2));
                //We add the placeholder adornment here because our workingSnapshot corresponds most closely to the syntactic model's text
                var snapshotSpan = new SnapshotSpan(property.SetAccessorDeclaration.GetSpan().Convert(workingSnapshot).Start, 1);
                var trackingSpan = workingSnapshot.CreateTrackingSpan(snapshotSpan.Span, SpanTrackingMode.EdgeExclusive);
                var ops = AdornmentOptionsHelper.GetAdornmentOptions(VSServiceProvider.Current.VSOptionsPage);
                @this._adornmentManager.AddAdornment(new InheritanceContractAdornment(trackingSpan, @this._textViewTracker.VSTextProperties, VSServiceProvider.Current.Logger, @this._adornmentManager.QueueRefreshLineTransformer, ops), tuple.Item2);
                #endregion
              }
              @this._propertiesNeedingContractLookup.Enqueue(new KeyValuePair<Tuple<object, object>, PropertyDeclarationNode>(tuple, property));
            }
          }
        }
      });

      //Most likely we've changed something (and this is a pretty cheap call), so let's ask for a refresh
      Utilities.Delay(() => VSServiceProvider.Current.QueueWorkItem(@this._adornmentManager.QueueRefreshLineTransformer, () => @this._textViewTracker.TextView.HasAggregateFocus), DelayAfterMembersRevalutation);

      //Ask for the new VS model so we can look up contracts
      Utilities.Delay(() => VSServiceProvider.Current.QueueWorkItem(VSServiceProvider.Current.AskForNewVSModel, () => @this._textViewTracker.TextView.HasAggregateFocus), DelayAfterMembersRevalutation);
    }

    static void RecursivelyLookupContractsForMethods(InheritanceTracker @this) {
      Contract.Requires(@this != null);

      #region Dequeue
      if (@this._methodsNeedingContractLookup.Count < 1)
        return;
      var methodPair = @this._methodsNeedingContractLookup.Dequeue();
      var method = methodPair.Value;
      var tag = methodPair.Key;
      #endregion
      try {
        VSServiceProvider.Current.Logger.WriteToLog(String.Format("Attempting to lookup contracts for '{0}'", tag.ToString()));
        var comp = @this._textViewTracker.LatestCompilation;
        if (comp == null) {
          VSServiceProvider.Current.Logger.WriteToLog("No LatestCompilation, waiting for a new semantic model.");
          @this._textViewTracker.IsLatestCompilationStale = true;
          Utilities.Delay(() => VSServiceProvider.Current.AskForNewVSModel(), DelayOnVSModelFailedBeforeTryingAgain);
          @this.semanticModelsFetchedCounter++;
          goto RequeueAndAbort;
        }
        #region Get semantic method from syntactic method
        CSharpMember semanticMethod = null;
        semanticMethod = comp.GetMemberForMethodDeclaration(method);
        if (semanticMethod == null) {
          if (@this.trackingNumberOfFetchedSemanticModels && @this.semanticModelsFetchedCounter <= 3) {
            VSServiceProvider.Current.Logger.WriteToLog("Failed to get semantic method from syntactic method, waiting for a new semantic model.");
            @this._textViewTracker.IsLatestCompilationStale = true;
            Utilities.Delay(() => VSServiceProvider.Current.AskForNewVSModel(), DelayOnVSModelFailedBeforeTryingAgain);
            @this.semanticModelsFetchedCounter++;
            goto RequeueAndAbort;
          } else {
            VSServiceProvider.Current.Logger.WriteToLog("Failed to get semantic method from syntactic method. Too many semantic models have already been fetched, skipping this method...");
            goto Continue;
          }
        }
        #endregion
        #region Try get the method that this method is inherited from
        CSharpMember inheritedFromMethod;
        if (!TryGetIheritedFromMember(semanticMethod, method.Parent as TypeDeclarationNode, out inheritedFromMethod)) {
          goto Continue;
        }
        #endregion
        #region Uninstantiated method
        semanticMethod = semanticMethod.Uninstantiate();
        #endregion
        #region Get our tool tip
        var toolTip = "";
        if (!semanticMethod.IsAbstract && !semanticMethod.ContainingType.IsInterface)
          toolTip = String.Format("Contracts inherited from {0}.", inheritedFromMethod.ContainingType.Name.Text);
        #endregion
        #region Try get method contracts and update adornment
        IMethodContract contracts = null;
        if (@this._textViewTracker.ProjectTracker.ContractsProvider.TryGetMethodContract(inheritedFromMethod, out contracts)) {
          var possibleAdornment = @this._adornmentManager.GetAdornment(tag);
          if (possibleAdornment != null) {
            var adornment = possibleAdornment as ContractAdornment;
            if (adornment != null) {
              adornment.SetContracts(contracts, toolTip);
            } else {
              VSServiceProvider.Current.Logger.WriteToLog("Placeholder adornment isn't a ContractAdornment (not good!), skipping method...");
            }
          } else {
            VSServiceProvider.Current.Logger.WriteToLog("Placeholder adornment not found, skipping method...");
          }
        }
        #endregion
      }
      #region Exception handeling
 catch (IllFormedSemanticModelException e) {
        if (@this.trackingNumberOfFetchedSemanticModels && @this.semanticModelsFetchedCounter <= 2) {
          VSServiceProvider.Current.Logger.WriteToLog("Error: An 'IllFormedSemanticModelException' occured: '" + e.Message + "' Asking for a new semantic model...");
          @this._textViewTracker.IsLatestCompilationStale = true;
          VSServiceProvider.Current.AskForNewVSModel();
          @this.semanticModelsFetchedCounter++;
          goto RequeueAndAbort;
        } else {
          VSServiceProvider.Current.Logger.WriteToLog("An 'IllFormedSemanticModelException' occured: '" + e.Message + "' Too many semantic models have been fetched, skipping this method...");
          goto Continue;
        }
      } catch (InvalidOperationException e) {
        if (e.Message.Contains(VSServiceProvider.InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate)) {
          if (@this.trackingNumberOfFetchedSemanticModels && @this.semanticModelsFetchedCounter <= 5) {
            VSServiceProvider.Current.Logger.WriteToLog("The Visual Studio Semantic/Syntactic model threw an exception (it's snapshot is out of date) while looking up contracts, getting new compilation...");
            @this._textViewTracker.IsLatestCompilationStale = true;
            VSServiceProvider.Current.AskForNewVSModel();
            @this.semanticModelsFetchedCounter++;
            goto RequeueAndAbort;
          } else {
            VSServiceProvider.Current.Logger.WriteToLog("The Visual Studio Semantic/Syntactic model threw an exception (it's snapshot is out of date) while looking up contracts. Too many compilations have already been fetched, skipping this method...");
            goto Continue;
          }
        } else
          throw e;
      } catch (COMException e) {
        if (e.Message.Contains(VSServiceProvider.COMExceptionMessage_BindingFailed)) {
          if (@this.trackingNumberOfFetchedSemanticModels && @this.semanticModelsFetchedCounter <= 5) {
            VSServiceProvider.Current.Logger.WriteToLog("The Visual Studio Semantic/Syntactic model threw an exception (it's snapshot is out of date) while looking up contracts, getting new compilation...");
            @this._textViewTracker.IsLatestCompilationStale = true;
            VSServiceProvider.Current.AskForNewVSModel();
            @this.semanticModelsFetchedCounter++;
            goto RequeueAndAbort;
          } else {
            VSServiceProvider.Current.Logger.WriteToLog("The Visual Studio Semantic/Syntactic model threw an exception (it's snapshot is out of date) while looking up contracts. Too many compilations have already been fetched, skipping this method...");
            goto Continue;
          }
        } else
          throw e;
      }
      #endregion
    Continue:
      VSServiceProvider.Current.QueueWorkItem(() => RecursivelyLookupContractsForMethods(@this));
      return;
    RequeueAndAbort:
      @this._methodsNeedingContractLookup.Enqueue(methodPair);
      return;
    }
    static void RecursivelyLookupContractsForProperties(InheritanceTracker @this) {
      Contract.Requires(@this != null);

      #region Dequeue
      if (@this._propertiesNeedingContractLookup.Count < 1)
        return;
      var propertyPair = @this._propertiesNeedingContractLookup.Dequeue();
      var property = propertyPair.Value;
      var tagTuple = propertyPair.Key;
      #endregion
      try {
        var comp = @this._textViewTracker.LatestCompilation;
        if (comp == null) {
          VSServiceProvider.Current.Logger.WriteToLog("No LatestCompilation, waiting for a new semantic model.");
          @this._textViewTracker.IsLatestCompilationStale = true;
          Utilities.Delay(() => VSServiceProvider.Current.AskForNewVSModel(), DelayOnVSModelFailedBeforeTryingAgain);
          @this.semanticModelsFetchedCounter++;
          goto RequeueAndAbort;
        }
        #region Get semantic property from syntactic property
        CSharpMember semanticProperty = null;
        semanticProperty = comp.GetMemberForPropertyDeclaration(property);
        if (semanticProperty == null) {
          if (@this.trackingNumberOfFetchedSemanticModels && @this.semanticModelsFetchedCounter <= 3) {
            VSServiceProvider.Current.Logger.WriteToLog("Failed to get semantic property from syntactic property, waiting for a new semantic model.");
            @this._textViewTracker.IsLatestCompilationStale = true;
            Utilities.Delay(() => VSServiceProvider.Current.AskForNewVSModel(), DelayOnVSModelFailedBeforeTryingAgain);
            @this.semanticModelsFetchedCounter++;
            goto RequeueAndAbort;
          } else {
            VSServiceProvider.Current.Logger.WriteToLog("Failed to get semantic property from syntactic property. Too many semantic models have already been fetched, skipping this property...");
            goto Continue;
          }
        }
        #endregion
        #region Try get the property that this property is inherited from
        CSharpMember inheritedFromProperty;
        if (!TryGetIheritedFromMember(semanticProperty, property.Parent as TypeDeclarationNode, out inheritedFromProperty)) {
          goto Continue;
        }
        #endregion
        #region Uninstantiated property
        semanticProperty = semanticProperty.Uninstantiate();
        #endregion
        #region Get our tool tip
        var toolTip = "";
        if (!semanticProperty.IsAbstract && !semanticProperty.ContainingType.IsInterface)
          toolTip = String.Format("Contracts inherited from {0}.", inheritedFromProperty.ContainingType.Name.Text);
        #endregion
        #region Try get accessor contracts and update adornment
        IMethodReference getterReference = null;
        IMethodReference setterReference = null;
        if (@this._textViewTracker.ProjectTracker.ContractsProvider.TryGetPropertyAccessorReferences(inheritedFromProperty, out getterReference, out setterReference)) {
          if (tagTuple.Item1 != null && getterReference != null) {
            IMethodContract getterContracts;
            if (@this._textViewTracker.ProjectTracker.ContractsProvider.TryGetMethodContract(getterReference, out getterContracts)) {
              var possibleAdornment = @this._adornmentManager.GetAdornment(tagTuple.Item1);
              if (possibleAdornment != null) {
                var adornment = possibleAdornment as ContractAdornment;
                if (adornment != null) {
                  adornment.SetContracts(getterContracts, toolTip);
                } else {
                  VSServiceProvider.Current.Logger.WriteToLog("Placeholder adornment isn't a ContractAdornment (not good!), skipping getter...");
                }
              } else {
                VSServiceProvider.Current.Logger.WriteToLog("Placeholder adornment not found, skipping getter...");
              }
            }
          }
          if (tagTuple.Item2 != null && setterReference != null) {
            IMethodContract setterContracts;
            if (@this._textViewTracker.ProjectTracker.ContractsProvider.TryGetMethodContract(setterReference, out setterContracts)) {
              var possibleAdornment = @this._adornmentManager.GetAdornment(tagTuple.Item2);
              if (possibleAdornment != null) {
                var adornment = possibleAdornment as ContractAdornment;
                if (adornment != null) {
                  adornment.SetContracts(setterContracts, toolTip);
                } else {
                  VSServiceProvider.Current.Logger.WriteToLog("Placeholder adornment isn't a ContractAdornment (not good!), skipping setter...");
                }
              } else {
                VSServiceProvider.Current.Logger.WriteToLog("Placeholder adornment not found, skipping setter...");
              }
            }
          }
        } else {
          VSServiceProvider.Current.Logger.WriteToLog("Failed to get CCI reference for: " + inheritedFromProperty.Name.Text);
        }
        #endregion
      }
      #region Exception handeling
 catch (IllFormedSemanticModelException e) {
        if (@this.trackingNumberOfFetchedSemanticModels && @this.semanticModelsFetchedCounter <= 2) {
          VSServiceProvider.Current.Logger.WriteToLog("Error: An 'IllFormedSemanticModelException' occured: '" + e.Message + "' Asking for a new semantic model...");
          @this._textViewTracker.IsLatestCompilationStale = true;
          VSServiceProvider.Current.AskForNewVSModel();
          @this.semanticModelsFetchedCounter++;
          goto RequeueAndAbort;
        } else {
          VSServiceProvider.Current.Logger.WriteToLog("An 'IllFormedSemanticModelException' occured: '" + e.Message + "' Too many semantic models have been fetched, skipping this property...");
          goto Continue;
        }
      } catch (InvalidOperationException e) {
        if (e.Message.Contains(VSServiceProvider.InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate)) {
          if (@this.trackingNumberOfFetchedSemanticModels && @this.semanticModelsFetchedCounter <= 5) {
            VSServiceProvider.Current.Logger.WriteToLog("The Visual Studio Semantic/Syntactic model threw an exception (it's snapshot is out of date) while looking up contracts, getting new compilation...");
            @this._textViewTracker.IsLatestCompilationStale = true;
            VSServiceProvider.Current.AskForNewVSModel();
            @this.semanticModelsFetchedCounter++;
            goto RequeueAndAbort;
          } else {
            VSServiceProvider.Current.Logger.WriteToLog("The Visual Studio Semantic/Syntactic model threw an exception (it's snapshot is out of date) while looking up contracts. Too many compilations have already been fetched, skipping this property...");
            goto Continue;
          }
        } else
          throw e;
      } catch (COMException e) {
        if (e.Message.Contains(VSServiceProvider.COMExceptionMessage_BindingFailed)) {
          if (@this.trackingNumberOfFetchedSemanticModels && @this.semanticModelsFetchedCounter <= 5) {
            VSServiceProvider.Current.Logger.WriteToLog("The Visual Studio Semantic/Syntactic model threw an exception (it's snapshot is out of date) while looking up contracts, getting new compilation...");
            @this._textViewTracker.IsLatestCompilationStale = true;
            VSServiceProvider.Current.AskForNewVSModel();
            @this.semanticModelsFetchedCounter++;
            goto RequeueAndAbort;
          } else {
            VSServiceProvider.Current.Logger.WriteToLog("The Visual Studio Semantic/Syntactic model threw an exception (it's snapshot is out of date) while looking up contracts. Too many compilations have already been fetched, skipping this property...");
            goto Continue;
          }
        } else
          throw e;
      }
      #endregion
    Continue:
      VSServiceProvider.Current.QueueWorkItem(() => RecursivelyLookupContractsForProperties(@this));
      return;
    RequeueAndAbort:
      @this._propertiesNeedingContractLookup.Enqueue(propertyPair);
      return;
    }

    static bool TryGetIheritedFromMember(CSharpMember semanticMember, TypeDeclarationNode syntacticParentType, out CSharpMember inheritedFromMember) {
      Contract.Requires(semanticMember != null);
      Contract.Requires(semanticMember.IsMethod);
      Contract.Ensures(!Contract.Result<bool>() || 
                       Contract.ValueAtReturn(out inheritedFromMember) != null && 
                       Contract.ValueAtReturn(out inheritedFromMember).IsMethod);

      inheritedFromMember = null;
      #region If member is from struct, ignore it
      if (semanticMember.ContainingType.IsValueType || semanticMember.ContainingType.IsStruct) {
        VSServiceProvider.Current.Logger.WriteToLog("Member is struct or value type, skipping member...");
        return false;
      }
      #endregion
      #region If member is from a contract class, ignore it
      //TODO: Get proper attributes from semantic model! Bug in semantic model, custom attributes don't seem to work right.
      bool ignoreIt = false;
      var containingType = semanticMember.ContainingType;
      if (containingType.IsClass) {
        if (syntacticParentType != null) {
          foreach (var attributeSection in syntacticParentType.Attributes) {
            foreach (var attribute in attributeSection.AttributeList) {
              var attributeName = attribute.AttributeName as IdentifierNode;
              if (attributeName != null) {
                if (attributeName.Name.Text.Contains("ContractClassFor"))
                  ignoreIt = true;
              }
            }
          }
        }
      }
      if (ignoreIt) {
        VSServiceProvider.Current.Logger.WriteToLog("Member has 'ContractClassForAttribute', skipping member...");
        return false;
      }
      #endregion
      // If member is override, get base member
      if (semanticMember.IsOverride) {
        if (!CSharpToCCIHelper.TryGetBaseMember(semanticMember, out inheritedFromMember)) {
          VSServiceProvider.Current.Logger.WriteToLog("Member is an override but we can't get its base member, skipping member...");
          return false; //If we can't get the base member, we don't want to keep going with this member.
        }
      }
      else if (semanticMember.ContainingType.IsInterface || semanticMember.IsAbstract)
      {
        inheritedFromMember = semanticMember;
      }
      #region Else member implements an interface or it doesn't have inherited contracts, get interface member
      else
      {
        if (!CSharpToCCIHelper.TryGetInterfaceMember(semanticMember, out inheritedFromMember))
        {
          VSServiceProvider.Current.Logger.WriteToLog("Member isn't override, abstract, in an interface or an interface member, skipping member...");
          return false; //If we can't get the interface member, we don't want to keep going with this member.
        }
      }
      #endregion
      return inheritedFromMember != null;
    }
  }
}