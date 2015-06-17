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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Adornments;
using Microsoft.Cci;
using Microsoft.Cci.Contracts;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Windows.Media;
using System.Text;
using CSharpSourceEmitter;
using System.Timers;
using Microsoft.Cci.MutableContracts;
using System.Text.RegularExpressions;

namespace ContractAdornments {
  public sealed class MetadataTracker {
    IWpfTextView _textView;
    AdornmentManager _adornmentManager;
    int _layoutChangedCounter = 0;
    bool _collapsed = false;
    readonly ProjectTracker _projectTracker;
    readonly Timer _timer;
    VSTextProperties _vsTextProperties;

    [ContractInvariantMethod]
    void ObjectInvariant() {
      Contract.Invariant(_projectTracker != null);
      Contract.Invariant(_timer != null);
      Contract.Invariant(_textView != null);
    }

    const string MetadataTrackerKey = "MetadataTracker";

    /// <summary>
    /// Gets or creates a MetadataTracker for a particular text view.
    /// </summary>
    public static MetadataTracker GetOrCreateMetadataTracker(IWpfTextView textView, ProjectTracker projectTracker, VSTextProperties vsTextProperties) {
      Contract.Requires(textView != null);
      Contract.Assume(textView.Properties != null);
      return textView.Properties.GetOrCreateSingletonProperty<MetadataTracker>(MetadataTrackerKey, delegate { return new MetadataTracker(textView, projectTracker, vsTextProperties); });
    }

    private MetadataTracker(IWpfTextView textView, ProjectTracker projectTracker, VSTextProperties vsTextProperties) {
      Contract.Requires(projectTracker != null);
      Contract.Requires(textView != null);
      this._vsTextProperties = vsTextProperties;
      this._textView = textView;
      this._projectTracker = projectTracker;
      VSServiceProvider.Current.ExtensionFailed += OnExtensionFailed;

      _timer = new System.Timers.Timer();
      _timer.AutoReset = false;

        if (!AdornmentManager.TryGetAdornmentManager(textView, "MetadataAdornments", out _adornmentManager))
      {
        VSServiceProvider.Current.Logger.WriteToLog("Metadata adornments layer not instantiated.");
        return;
      }
      
      _timer.Elapsed += OnLayoutSettled;
      textView.LayoutChanged += OnLayoutChanged;
    }

    void OnExtensionFailed() {
      _timer.Stop();
      _textView.LayoutChanged -= OnLayoutChanged;
    }

    void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e) {
      try {
        if (VSServiceProvider.Current.ExtensionHasFailed) return;
        _layoutChangedCounter++;
        if (_timer != null) {
          _timer.Interval = 2000;
          _timer.Enabled = true;
        }
      } catch (Exception exn) {
        VSServiceProvider.Current.Logger.PublicEntryException(exn, "OnLayoutChanged");
      }
    }

    void OnLayoutSettled(object sender, System.Timers.ElapsedEventArgs e) {
      VSServiceProvider.Current.Logger.PublicEntry(() => {
        _textView.LayoutChanged -= OnLayoutChanged;
        _timer.Elapsed -= OnLayoutSettled;
        VSServiceProvider.Current.Logger.WriteToLog("Timer elapsed, waiting to get metadata contracts.");
        VSServiceProvider.Current.QueueWorkItem(DoWork);
      }, "OnLayoutSettled");
    }


    const string modifierFilter = @"\b(abstract|override|virtual|protected|internal|static|private|public|async|extern|partial|sealed|unsafe)\b";
    // The first lines of the metadata file should look like this:
      //#region Assembly Microsoft.Cci.MetadataHelper.dll, v0.0.0.2
      //// C:\dev\cci-vs2012\Microsoft.Research\ImportedCCI2\Microsoft.Cci.MetadataHelper.dll
      //#endregion

      //using System;
      //using System.Collections.Generic;
      //using System.Reflection;

      //namespace Microsoft.Cci {
      //  public static class UnitHelper {
    private void GetNecessaryInfo(
      out string assemblyName,
      out Version assemblyVersion,
      out string assemblyLocation,
      out string typeName,
      out int genericParameterCount
      ) {
      #region Parse the assembly name, assembly version, assembly location, type name, and generic parameter count from the metadata file
      assemblyName = null;
      assemblyVersion = null;
      assemblyLocation = null;
      typeName = "";
      genericParameterCount = 0;

      if (_textView.TextSnapshot == null) return;
      var text = _textView.TextSnapshot.GetText();
      if (text == null) return;
      var lines = text.Split(new string[]{Environment.NewLine, }, StringSplitOptions.RemoveEmptyEntries);
      if (lines.Length < 5) return;
      var firstLine = lines[0];
      var assemblyNameAndVersion = Regex.Match(firstLine, @"#region Assembly ((\w|\.)+), v((\d|\.)+)");
      if (!assemblyNameAndVersion.Success) return;
      assemblyName = assemblyNameAndVersion.Groups[1].Value;
      if (!Version.TryParse(assemblyNameAndVersion.Groups[3].Value, out assemblyVersion)) return;

      var secondLine = lines[1];
      var assemblyPathMatch = Regex.Match(secondLine, @"//( )*(.*)");
      if (!assemblyPathMatch.Success) return;
      assemblyLocation = assemblyPathMatch.Groups[2].Value;

      int i = 2;
      while (i < lines.Length && !lines[i].StartsWith("using")) i++;
      if (i == lines.Length) return;
      while (i < lines.Length && lines[i].StartsWith("using")) i++;
      if (i == lines.Length) return;

      string namespaceName = "";
      var namespaceMatch = Regex.Match(lines[i], @"namespace( )*((\w|\.)+)");
      if (namespaceMatch.Success) {
        i++;
        namespaceName = namespaceMatch.Groups[2].Value;
      }

      if (i == lines.Length) return;

      // skip line with single open curly
      if (Regex.IsMatch(lines[i], @"^( )*{")) i++;
      // skip comments
      while (i < lines.Length && Regex.IsMatch(lines[i], @"^( )*//")) i++;
      // skip custom attributes
      while (i < lines.Length && Regex.IsMatch(lines[i], @"^( )*\[")) i++;

      if (i >= lines.Length) return;

      Contract.Assume(lines[i] != null, "somehow we lost the forall from split");
      var typeNameMatch = Regex.Match(lines[i], @"(class|struct|interface|enum) (\w+)");
      if (!typeNameMatch.Success) return;
      typeName = typeNameMatch.Groups[2].Value;
      if (!String.IsNullOrWhiteSpace(namespaceName))
        typeName = namespaceName + "." + typeName;
      var regex = @"(class|struct|interface|enum) \w+<([^,>]+,?)+>";
      var genericParmeterCountMatch = Regex.Match(lines[i], regex);
      if (genericParmeterCountMatch.Success) {
        genericParameterCount = genericParmeterCountMatch.Groups[0].Value.Count(c => c == ',') + 1;
      }

      if (String.IsNullOrEmpty(assemblyLocation)
        || String.IsNullOrEmpty(assemblyName)
        || assemblyVersion == null
        || String.IsNullOrEmpty(typeName)) {
        VSServiceProvider.Current.Logger.WriteToLog("Failed to parse assembly/type information from metadatafile.");
      } else {
        VSServiceProvider.Current.Logger.WriteToLog("Assembly/type information found: "
                                              + "\n\tAssembly name: " + assemblyName
                                              + "\n\tAssembly version: " + assemblyVersion
                                              + "\n\tAssembly location: " + assemblyLocation
                                              + "\n\tType name: " + typeName
                                              + "\n\tGeneric parameter count: " + genericParameterCount
                                              );
      }
      #endregion
      return;
    }

    [ContractVerification(false)]
    void DoWork() {

      string assemblyName;
      Version assemblyVersion;
      string assemblyLocation;
      string typeName;
      int genericParameterCount;
      GetNecessaryInfo(out assemblyName, out assemblyVersion, out assemblyLocation, out typeName, out genericParameterCount);


      #region Create host
      var host = this._projectTracker.Host;

      if (host == null) {
        VSServiceProvider.Current.Logger.WriteToLog("Couldn't create host.");
        return;
      }
      #endregion
      #region Find and load assembly
      IAssembly iAssembly = null;

      if (assemblyName.Equals(host.CoreAssemblySymbolicIdentity.Name.Value, StringComparison.OrdinalIgnoreCase) && assemblyVersion.Equals(host.CoreAssemblySymbolicIdentity.Version)) {
        iAssembly = host.FindAssembly(host.CoreAssemblySymbolicIdentity);
      } else {
        var references = _projectTracker.References;
        VSLangProj.Reference reference = null;
        var assemblyNameWithoutExtension = Path.GetFileNameWithoutExtension(assemblyName);
        for (int i = 1; i <= references.Count; i++) {//TODO: Unify this code. This process of looking up a reference from a name is also done in ContractsProvider.TryGetAssemblyReference
          var tempRef = references.Item(i);
          string refName = tempRef.Name;
          if (refName.Equals(assemblyNameWithoutExtension, StringComparison.OrdinalIgnoreCase)) {
            reference = tempRef;
            break;
          }
        }
        if (reference != null) {
          IName iName = host.NameTable.GetNameFor(Path.GetFileNameWithoutExtension(reference.Path));
          string culture = reference.Culture;
          Version version = new Version(reference.MajorVersion, reference.MinorVersion, reference.BuildNumber, reference.RevisionNumber);
          string location = reference.Path;
          var tempRef2 = new Microsoft.Cci.Immutable.AssemblyReference(host, new AssemblyIdentity(iName, culture, version, Enumerable<byte>.Empty, location));
          iAssembly = host.LoadAssembly(tempRef2.AssemblyIdentity);
        } else {
          VSServiceProvider.Current.Logger.WriteToLog("Couldn't find reference for metadata file.");
          return;
        }
      }
      if (iAssembly == null || iAssembly == Dummy.Assembly) {
        VSServiceProvider.Current.Logger.WriteToLog("Couldn't get assembly for metadata file.");
        return;
      }
      #endregion
      #region Get contracts provider
      var contractsProvider = host.GetContractExtractor(iAssembly.UnitIdentity);
      if (contractsProvider == null) {
        VSServiceProvider.Current.Logger.WriteToLog("Couldn't get contracts provider.");
        return;
      }
      #endregion
      #region Collect contracts
      var type = UnitHelper.FindType(host.NameTable, iAssembly, typeName, genericParameterCount);
      if (type == null || type is Dummy) {
        VSServiceProvider.Current.Logger.WriteToLog("Couldn't find metadata type '" + typeName + "' in assembly.");
        return;
      }

      //This dictionaries will map the method/property signature to the contracts for the method/property
      var methodsToContracts = new Dictionary<string, IMethodContract>(type.Methods.Count());
      var gettersToContracts = new Dictionary<string, IMethodContract>();
      var settersToContracts = new Dictionary<string, IMethodContract>();

      //Set the formatting options for property getters and setters
      var propertySignatureFormattingOptions = NameFormattingOptions.OmitContainingNamespace |
                                                // NameFormattingOptions.ReturnType |
                                                NameFormattingOptions.TypeParameters |
                                                NameFormattingOptions.UseTypeKeywords |
                                                NameFormattingOptions.OmitContainingType;
                                                //NameFormattingOptions.Visibility;
      //Set the formating options for methods
      var methodSignatureFormattingOptions = NameFormattingOptions.OmitContainingNamespace |
                                              NameFormattingOptions.ReturnType |
                                              NameFormattingOptions.ParameterName |
                                              NameFormattingOptions.ParameterModifiers |
                                              NameFormattingOptions.TypeParameters |
                                              NameFormattingOptions.UseTypeKeywords |
                                              NameFormattingOptions.OmitContainingType |
                                              //NameFormattingOptions.Modifiers |
                                              NameFormattingOptions.Signature 
                                              //NameFormattingOptions.Visibility
                                              ;

      //var sourceEmitterOutput = new SourceEmitterOutputString();//TODO: Use source emitter for all my printing? Instead of the whole NameFormattingOptions ordeal.
      //var csSourceEmitter = new SourceEmitter(sourceEmitterOutput);

      foreach (var method in type.Methods) {
        var methodContract = ContractHelper.GetMethodContractForIncludingInheritedContracts(host, method);
        if (methodContract != null && methodContract != ContractDummy.MethodContract) {
          if (IsGetter(method)) {
            if (method.ParameterCount > 0) { //We have an indexer!
              var indexerSignature = PrintIndexer(method, true);
              gettersToContracts.Add(indexerSignature, methodContract);
            } else {
              var getterSignature = MemberHelper.GetMemberSignature(method, propertySignatureFormattingOptions);//Example: "XmlSchemaSet Schemas.get"
              getterSignature = getterSignature.Substring(0, getterSignature.LastIndexOf('.'));
              gettersToContracts.Add(getterSignature, methodContract);
            }
          } else if (IsSetter(method)) {
            if (method.ParameterCount > 1) { //We have an indexer!
              var indexerSignature = PrintIndexer(method, false);
              settersToContracts.Add(indexerSignature, methodContract);
            } else {
              var setterSignature = MemberHelper.GetMemberSignature(method, propertySignatureFormattingOptions);
              setterSignature = setterSignature.Substring(0, setterSignature.LastIndexOf('.'));
              settersToContracts.Add(setterSignature, methodContract);
            }
          } else {
            //#region Print method, stolen from CSharpSourceEmitter
            //csSourceEmitter.PrintMethodDefinitionVisibility(method);
            //csSourceEmitter.PrintMethodDefinitionModifiers(method);

            //bool conversion = csSourceEmitter.IsConversionOperator(method);
            //if (!conversion) {
            //  csSourceEmitter.PrintMethodDefinitionReturnType(method);
            //  if (!method.IsConstructor && !csSourceEmitter.IsDestructor(method))
            //    csSourceEmitter.PrintToken(CSharpToken.Space);
            //}
            //csSourceEmitter.PrintMethodDefinitionName(method);
            //if (conversion)
            //  csSourceEmitter.PrintMethodDefinitionReturnType(method);

            //if (method.IsGeneric) {
            //  csSourceEmitter.Traverse(method.GenericParameters);
            //}
            //csSourceEmitter.Traverse(method.Parameters);
            //#endregion
            //var methodSignature = sourceEmitterOutput.Data;
            //sourceEmitterOutput.ClearData();
            var methodSignature = MemberHelper.GetMemberSignature(method, methodSignatureFormattingOptions);//Example: "XmlAttribute CreateAttribute(string name)"
            methodsToContracts.Add(methodSignature, methodContract);
          }
        }
      }
      #endregion

      var hasMethodContracts = methodsToContracts.Count > 0;
      var hasPropertyContracts = gettersToContracts.Count > 0 || settersToContracts.Count > 0;

      if (!hasMethodContracts && !hasPropertyContracts) {
        VSServiceProvider.Current.Logger.WriteToLog("No contracts found.");
        return;
      }

      int propertyCounter = 0; //Counts the number of adornments added for property contracts
      int methodCounter = 0; //Counts the number of adornments added for method contracts
      foreach (var line in _textView.TextSnapshot.Lines) {
        var lineText = line.GetText();

        //Skip lines with comments
        //This assumes that no method/property decelerations in metadata files will have comments in them
        if (lineText.Contains("//"))
          continue;

        // bail out on nested types
        var typeNameMatch = Regex.Match(lineText, @"(class|struct|interface|enum) (\w+)");
        if (typeNameMatch.Success)
        {
            if (typeNameMatch.Groups[2].Value == type.Name.Value) continue;
            break;
        }

        if (hasPropertyContracts &&
          lineText.Contains('{') && (lineText.Contains(" get; ") || lineText.Contains(" set; ")) && lineText.Contains('}')) { //Check if line is a property decleration
          #region Add property contracts
          //Parse the property decleration to get a signature we can compare to our contract dictionaries
          //Example of a property decleration: "     public int Build { get; }"
          int endOfSig = lineText.IndexOf('{') - 1;
          int startOfSig = endOfSig - 1;
          bool hasHitSpace = false;
          bool isInPropertyParameter = false;
          for (int i = startOfSig; i > 0; i--) {
            char c = lineText[i];
            if (c == ']')
              isInPropertyParameter = true;
            else if (c == '[')
              isInPropertyParameter = false;
            else if (hasHitSpace && c == ' ') {
              startOfSig = i + 1;
              break;
            } else if (!isInPropertyParameter && c == ' ') {
              startOfSig = i + 1;
              break; // MAF: ignore return type of properties.
              //hasHitSpace = true;
            }
          }
          var propertySignature = lineText.Substring(startOfSig, endOfSig - startOfSig); //Example: "int Build"

          IMethodContract getterContract = null;
          IMethodContract setterContract = null;
          if (gettersToContracts.TryGetValue(propertySignature, out getterContract) | // yes eager evaluation!!!
              settersToContracts.TryGetValue(propertySignature, out setterContract)) {
            var tag = propertySignature.GetHashCode();//We use this to uniquely identify the particular method/property decleration

            //Find this first non-whitespace character. This is were we'll place the adornment.
            int firstNonWhitespace = 0;
            for (int i = 0; i < lineText.Length; i++) {
              char c = lineText[i];
              if (c != ' ') {
                firstNonWhitespace = i;
                break;
              }
            }
            var span = _textView.TextSnapshot.CreateTrackingSpan(line.Start + firstNonWhitespace, propertySignature.Length, SpanTrackingMode.EdgeExclusive);
            _vsTextProperties.LineHeight = _textView.LineHeight;
            var ops = AdornmentOptionsHelper.GetAdornmentOptions(VSServiceProvider.Current.VSOptionsPage);
            var adornment = new MetadataContractAdornment(span, _vsTextProperties, VSServiceProvider.Current.Logger, _adornmentManager.QueueRefreshLineTransformer, ops);
            adornment.SetContracts(getterContract, setterContract, null /* "Contracts from " + typeName + "." */);
            _adornmentManager.AddAdornment(adornment, tag);
            propertyCounter++;
          }
          #endregion
          continue;
        }
        if (hasMethodContracts &&
          lineText.Contains('(') && lineText.Contains(')') && lineText.Contains(';')) { //Check if line is a method decleration
          #region Add method contracts
          //Parse the method decleration to get a signature we can compare to our contract dictionaries
          //Example of a method decleration: "      public static Version Parse(string input);"
          int endOfSig = lineText.LastIndexOf(')') + 1;
          //int startOfSig = !Char.IsWhiteSpace(lineText[0]) ? 0 : lineText.IndexOf(lineText.First(c => !Char.IsWhiteSpace(c)));
          //int startOfSig = lineText.IndexOf('(');
          //bool hitSpace = false;
          //for (int i = startOfSig; i > 0; i--) {
          //  char c = lineText[i];
          //  if (c == ' ' && hitSpace) {
          //    startOfSig = i + 1;
          //    break;
          //  } else if (c == ' ') {
          //    hitSpace = true;
          //  }
          //}
          var methodSignature = lineText.Substring(0, endOfSig); //Example: "Version Parse(string input)"

          // remove modifiers
          methodSignature = Regex.Replace(methodSignature, modifierFilter, "");
          methodSignature = methodSignature.Trim();
          if (methodSignature.StartsWith(type.Name.Value + "("))
          {
              methodSignature = "void .ctor" + methodSignature.Substring(type.Name.Value.Length);
          }
          IMethodContract methodContract;
          if (methodsToContracts.TryGetValue(methodSignature, out methodContract)) {
            var tag = methodSignature.GetHashCode();//We use this to uniquely identify the particular method/property decleration

            //Find this first non-whitespace character. This is were we'll place the adornment.
            int firstNonWhitespace = 0;
            for (int i = 0; i < lineText.Length; i++) {
              char c = lineText[i];
              if (c != ' ') {
                firstNonWhitespace = i;
                break;
              }

            }
            var span = _textView.TextSnapshot.CreateTrackingSpan(line.Start + firstNonWhitespace, methodSignature.Length, SpanTrackingMode.EdgeExclusive);
            _vsTextProperties.LineHeight = _textView.LineHeight;
            var ops = AdornmentOptionsHelper.GetAdornmentOptions(VSServiceProvider.Current.VSOptionsPage);
            var adornment = new MetadataContractAdornment(span, _vsTextProperties, VSServiceProvider.Current.Logger, _adornmentManager.QueueRefreshLineTransformer, ops);
            adornment.SetContracts(methodContract, "Contracts from " + typeName + ".");
            _adornmentManager.AddAdornment(adornment, tag);
            //if (methodContract.IsPure) {
            //  var purityAdornment = new PurityAdornment(_vsTextProperties, adornment);
            //  //_adornmentManager.AddAdornment(purityAdornment, null);
            //}
            methodCounter++;
          }
          #endregion
          continue;
        }
      }

      #region Add button to collapse all contracts
      if (propertyCounter > 0 || methodCounter > 0) {
        var button = new Button();
        button.Content = "Hide all contracts";
        button.Click += OnCollapseAllClick;
        button.Cursor = Cursors.Hand;
        var collapseAllAdornment = new StaticAdornment(false, 10d, true, 10d, button);
        _adornmentManager.AddStaticAdornment(collapseAllAdornment);
      }
      #endregion
    }

    void OnCollapseAllClick(object sender, RoutedEventArgs e) {
      VSServiceProvider.Current.Logger.PublicEntry(() => {
        var button = sender as Button;
        if (button != null) {
          if (_collapsed) {
            button.Content = "Hide all contracts";
            _adornmentManager.ShowVisuals();
          } else {
            button.Content = "Show all contracts";
            _adornmentManager.CollapseVisuals();
          }
        }
        _collapsed = !_collapsed;
      }, "CollapseAllClick");
    }

    void RefreshLineTransformer() {
      this._textView.RefreshLineTransformer();
    }

    /// <summary>
    /// Checks if the method is a "get" accessor for a property
    /// </summary>
    public static bool IsGetter(IMethodDefinition methodDefinition) {
      Contract.Requires(methodDefinition != null);

      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("get_");
    }
    /// <summary>
    /// Checks if the method is a "set" accessor for a property
    /// </summary>
    public static bool IsSetter(IMethodDefinition methodDefinition) {
      Contract.Requires(methodDefinition != null);

      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("set_");
    }

    public static string PrintIndexer(IMethodDefinition method, bool isGetter) {
      Contract.Requires(method != null);
      
      var sourceEmitterOutput = new SourceEmitterOutputString();
      var csSourceEmitter = new SourceEmitter(sourceEmitterOutput);
      var indexerSignatureBuilder = new StringBuilder();
      csSourceEmitter.Traverse(method.Type);
      indexerSignatureBuilder.Append(sourceEmitterOutput.Data);
      sourceEmitterOutput.ClearData();
      indexerSignatureBuilder.Append(" ");
      // Indexers are always identified with a 'this' keyword, but might have an interface prefix
      string id = method.Name.Value;
      int lastDot = id.LastIndexOf('.');
      if (lastDot != -1)
        indexerSignatureBuilder.Append(id.Substring(0, lastDot + 1));
      indexerSignatureBuilder.Append("this");
      indexerSignatureBuilder.Append("[");
      bool fFirstParameter = true;
      var parms = method.Parameters;
      if (!isGetter)
      {
        // Use the setter's names except for the final 'value' parameter
        var l = new List<IParameterDefinition>(method.Parameters);
        if (l.Count > 0)
        {
          l.RemoveAt(l.Count - 1);
        }
        parms = l;
      }
      foreach (IParameterDefinition parameterDefinition in parms) {
        if (!fFirstParameter)
          indexerSignatureBuilder.Append(", ");
        Contract.Assume(parameterDefinition != null);
        csSourceEmitter.Traverse(parameterDefinition);
        indexerSignatureBuilder.Append(sourceEmitterOutput.Data);
        sourceEmitterOutput.ClearData();
        fFirstParameter = false;
      }
      indexerSignatureBuilder.Append("]");
      return indexerSignatureBuilder.ToString();
    }

    ///// <summary>
    ///// A custom host enviornment that makes sure all openned resources can be disposed. This is needed so that this host doesn't interfere with VS's build process.
    ///// </summary>
    //class ContractsProviderHost : CodeContractAwareHostEnvironment {
    //  /// <summary>
    //  /// Opens a binary document.
    //  /// </summary>
    //  public override IBinaryDocumentMemoryBlock OpenBinaryDocument(IBinaryDocument sourceDocument) {
    //    try {
    //      IBinaryDocumentMemoryBlock binDocMemoryBlock = UnmanagedBinaryMemoryBlock.CreateUnmanagedBinaryMemoryBlock(sourceDocument.Location, sourceDocument);
    //      return binDocMemoryBlock;
    //    } catch (IOException) {
    //      return null;
    //    }
    //  }
    //  /// <summary>
    //  /// Creates a ContracsProviderHost.
    //  /// </summary>
    //  /// <param name="libpaths"></param>
    //  public ContractsProviderHost(string[] libpaths) : base(libpaths) { }
    //}
  }
}