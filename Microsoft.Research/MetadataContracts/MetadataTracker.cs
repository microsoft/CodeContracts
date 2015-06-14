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
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System.IO;
using Microsoft.Cci.ILToCodeModel;
using Microsoft.Cci;
using Microsoft.Cci.Contracts;
using System.Diagnostics;
using Adornments;
using System.Timers;

namespace MetadataContracts {
  internal sealed class MetadataTracker {
    IWpfTextView textView;
    double defaultFontSize;
    string fileName;
    Timer timer;
    AdornmentManager adornmentManager;

    public static MetadataTracker GetOrCreateMetadataTracker(IWpfTextView textView, double defaultFontSize = 10.0d) {
      return textView.Properties.GetOrCreateSingletonProperty<MetadataTracker>(delegate { return new MetadataTracker(textView, defaultFontSize); });
    }

    private MetadataTracker(IWpfTextView textView, double defaultFontSize = 10.0d) {
      this.defaultFontSize = defaultFontSize;
      this.textView = textView;

      bool isMetadataFile = false;
      fileName = textView.TextBuffer.GetFileName();
      if (fileName.Contains('$'))//TODO: This is a very weak check, we need a better way to check this!
        isMetadataFile = true;

      if (!isMetadataFile)
        return;

      adornmentManager = AdornmentManager.GetOrCreateAdornmentManager(textView, "MetadataAdornments");

      //timer = new Timer();
      //timer.BeginInit();
      //timer.AutoReset = false;
      //timer.Elapsed += OnLayoutSettled;
      //timer.Interval = 2000;
      //timer.EndInit();
      //timer.Start();

      textView.LayoutChanged += OnLayoutChanged;

      var foo = new ContractAdornment(11, textView.TextSnapshot.CreateTrackingSpan(0, 10, SpanTrackingMode.EdgeExclusive), defaultFontSize);
    }
    bool once = true;
    int fooCounter = 0;
    void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e) {
      if (once && fooCounter > 10) {
        OnLayoutSettled(null, null);
        once = false;
      }
      fooCounter++;
    }

    void OnLayoutSettled(object sender, ElapsedEventArgs e) {
      var splitFileName = fileName.Split('$');//TODO: This may have holes, check for corner cases!
      string assemblyName = splitFileName[1];
      string assemblyVersion = splitFileName[2];
      string typeName = Path.GetFileNameWithoutExtension(fileName);//.Split('.').Last();

      ContractsProviderHost host = null;
      string codeContractsInstallDir = Environment.GetEnvironmentVariable("CodeContractsInstallDir");
      if (!String.IsNullOrEmpty(codeContractsInstallDir)) {
        string contractsDir = Path.Combine(codeContractsInstallDir, @"contracts\3.5");
        host = new ContractsProviderHost(new string[] { contractsDir });
      }

      if (host == null)
        return;//TODO: Throw error?

      //var assemlyIdentity = new AssemblyIdentity(host.NameTable.GetNameFor(assemblyName), null, new Version(assemblyVersion), null, null);
      //var assembly = host.FindAssembly(assemlyIdentity);
      var assembly = host.LoadUnitFrom(Path.Combine(codeContractsInstallDir, @"Contracts\v3.5", Path.GetFileNameWithoutExtension(assemblyName) + ".contracts.dll"));

      if (assembly == null || assembly == Dummy.Assembly)
        return;//TODO: Throw error?

      var contractsProvider = host.GetContractProvider(assembly.UnitIdentity);

      if (contractsProvider == null)
        return; //TODO Throw error? Will it ever be null?

      var type = UnitHelper.FindType(host.NameTable, assembly, typeName);

      var methodsToContracts = new Dictionary<string, IMethodContract>(type.Methods.Count());
      var gettersToContracts = new Dictionary<string, IMethodContract>();
      var settersToContracts = new Dictionary<string, IMethodContract>();
      System.Xml.XmlDocument doc;
      foreach (var method in type.Methods) {
        var methodContract = contractsProvider.GetMethodContractFor(method);
        if (methodContract != null) {
          if (IsGetter(method)) {
            var getterSignature = MemberHelper.GetMemberSignature(method,
                                                            NameFormattingOptions.OmitContainingNamespace |
                                                            NameFormattingOptions.ReturnType |
                                                            NameFormattingOptions.TypeParameters |
                                                            NameFormattingOptions.UseTypeKeywords |
                                                            NameFormattingOptions.OmitContainingType |
                                                            NameFormattingOptions.Visibility
                                                            );
            getterSignature = getterSignature.Substring(0, getterSignature.LastIndexOf('.'));
            gettersToContracts.Add(getterSignature, methodContract);
          } else if (IsSetter(method)) {
            var setterSignature = MemberHelper.GetMemberSignature(method,
                                                            NameFormattingOptions.OmitContainingNamespace |
                                                            NameFormattingOptions.ReturnType |
                                                            NameFormattingOptions.TypeParameters |
                                                            NameFormattingOptions.UseTypeKeywords |
                                                            NameFormattingOptions.OmitContainingType |
                                                            NameFormattingOptions.Visibility
                                                            );
            setterSignature = setterSignature.Substring(0, setterSignature.LastIndexOf('.'));
            settersToContracts.Add(setterSignature, methodContract);
          } else {
            var methodSignature = MemberHelper.GetMemberSignature(method,
                                                            NameFormattingOptions.OmitContainingNamespace |
                                                            NameFormattingOptions.ReturnType |
                                                            NameFormattingOptions.ParameterName |
                                                            NameFormattingOptions.ParameterModifiers |
                                                            NameFormattingOptions.TypeParameters |
                                                            NameFormattingOptions.UseTypeKeywords |
                                                            NameFormattingOptions.OmitContainingType |
                                                            NameFormattingOptions.Modifiers |
                                                            NameFormattingOptions.Signature |
                                                            NameFormattingOptions.Visibility
                                                            );
            methodsToContracts.Add(methodSignature, methodContract);
          }
        }
      }



      foreach (var line in textView.TextSnapshot.Lines) {
        var lineText = line.GetText();

        if (lineText.Contains("//")) continue; //Skip comments

        if (lineText.Contains('{') && (lineText.Contains(" get; ") || lineText.Contains(" set; ")) && lineText.Contains('}')) { // If property
          int endOfSig = lineText.IndexOf('{') - 1;
          int startOfSig = endOfSig - 1;
          bool hitSpace = false;
          for (int i = startOfSig; i > 0; i--) {
            char c = lineText[i];
            if (c == ' ' && hitSpace) {
              startOfSig = i + 1;
              break;
            } else if (c == ' ') {
              hitSpace = true;
            }
          }
          var propertySignature = lineText.Substring(startOfSig, endOfSig - startOfSig);

          IMethodContract getterContract;
          IMethodContract setterContract = null;
          if (gettersToContracts.TryGetValue(propertySignature, out getterContract) || settersToContracts.TryGetValue(propertySignature, out setterContract)) {
            var tag = propertySignature.GetHashCode();
            int firstNonWhitespace = 0;
            for (int i = 0; i < lineText.Count(); i++) {
              char c = lineText[i];
              if (c != ' ') {
                firstNonWhitespace = i;
                break;
              }

            }
            var span = textView.TextSnapshot.CreateTrackingSpan(line.Start + firstNonWhitespace, propertySignature.Length, SpanTrackingMode.EdgeExclusive);
            var adornment = new ContractAdornment(tag, span, defaultFontSize, false, null, getterContract, setterContract, "Contracts from " + typeName + ".");
            adornmentManager.AddAdornment(adornment, tag);//TODO: Inherit from info is wrong.
          }
        } 
        else if (lineText.Contains('(') && lineText.Contains('(') && lineText.Contains(';')) { //If method
          int endOfSig = lineText.LastIndexOf(')') + 1;
          int startOfSig = lineText.IndexOf('(');
          bool hitSpace = false;
          for (int i = startOfSig; i > 0; i--) {
            char c = lineText[i];
            if (c == ' ' && hitSpace) {
              startOfSig = i + 1;
              break;
            } else if (c == ' ') {
              hitSpace = true;
            }
          }
          var methodSignature = lineText.Substring(startOfSig, endOfSig - startOfSig);

          IMethodContract methodContract;
          if (methodsToContracts.TryGetValue(methodSignature, out methodContract)) {
            var tag = methodSignature.GetHashCode();
            int firstNonWhitespace = 0;
            for (int i = 0; i < lineText.Count(); i++) {
              char c = lineText[i];
              if (c != ' ') {
                firstNonWhitespace = i;
                break;
              }

            }
            var span = textView.TextSnapshot.CreateTrackingSpan(line.Start + firstNonWhitespace, methodSignature.Length, SpanTrackingMode.EdgeExclusive);
            var adornment = new ContractAdornment(tag, span, defaultFontSize, false, null, methodContract, "Contracts from " + typeName + ".");
            adornmentManager.AddAdornment(adornment, tag);//TODO: Inherit from info is wrong.
          }
        }
      }

      if (true) {
      }
    }

    /// <summary>
    /// Checks if the method is a "get" accessor for a property
    /// </summary>
    public static bool IsGetter(IMethodDefinition methodDefinition) {
      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("get_");
    }

    /// <summary>
    /// Checks if the method is a "set" accessor for a property
    /// </summary>
    public static bool IsSetter(IMethodDefinition methodDefinition) {
      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("set_");
    }


    /// <summary>
    /// A custom host enviornment that makes sure all openned resources can be disposed. This is needed so that this host doesn't interfere with VS's build process.
    /// </summary>
    class ContractsProviderHost : CodeContractAwareHostEnvironment {
      /// <summary>
      /// Opens a binary document.
      /// </summary>
      public override IBinaryDocumentMemoryBlock OpenBinaryDocument(IBinaryDocument sourceDocument) {
        try {
          IBinaryDocumentMemoryBlock binDocMemoryBlock = UnmanagedBinaryMemoryBlock.CreateUnmanagedBinaryMemoryBlock(sourceDocument.Location, sourceDocument);
          return binDocMemoryBlock;
        } catch (IOException) {
          return null;
        }
      }
      /// <summary>
      /// Creates a ContracsProviderHost.
      /// </summary>
      /// <param name="libpaths"></param>
      public ContractsProviderHost(string[] libpaths) : base(libpaths) { }
    }
  }
}

//TODO: REMOVE
//foreach (var prop in textView.Properties.PropertyList)
//{
//    var key = prop.Key;
//    var value = prop.Value;
//}
//foreach (var prop in textView.TextBuffer.Properties.PropertyList)
//{
//    var key = prop.Key;
//    var value = prop.Value;
//}

//var typeName = Path.GetFileNameWithoutExtension(fileName);
//var textDoc = textView.TextBuffer.Properties[typeof(ITextDocument)] as ITextDocument;

//First, you'll have to figure out if you are looking at a metadata file or not.
//You can do this by looking at the "fileName" and seeing if it auto generated
//or not, or perhaps you can find a more elegant way. However, the "textView" and
//the textView's ITextBuffer don't seem to know if they are a metadata file or not.
//I'm doubtful that that information is exposed to us. I sent an email to the VSX
//alias to see if someone could confirm this.

//Second, you'll have to figure out which assembly the type you are looking at belongs
//to. (See "typeName"). Alternativly, if you may be able to figure out which assembly
//it belongs to simply by looking at the "fileName". You may want to sit in the debugger
//to see if this is possible.

//Once you have the assembly's strong name, you'll have to load it with a host and ask
//for the contracts for the type in question ("typeName"). You may have to do additional
//string parsing to get a strong type name. (I.e. Namespace, etc.)

//Once you have all the contracts for your type in question, you'll have to parse through
//the textView's TextBuffer's text and to see if you can match up the contracts with their
//corrisponding metadata method.

//Once you have a method's location and its contracts, you'll have to create a new
//"ContractAdornment", passing it the IMethodContract and ITrackingSpan as well as a "tag"
//object and other misc info. The tag can just be a hash of the method name or something.
//The tag is used to update the adornments, but I don't think you'll need to do that much.
//The "RefreshLineTransformer" parameter can be ignored for now, eventualy that should be
//implemented to provide the user with the most resposive experience.

//After you create the ContractAdornment, pass that to the "adornmentManager" via AddAdornment.
//The adornment manager should take it from there.

//Eventually, we'll want to hook up an outlining manager.

//Also, currently the ContractAdornment only handles method contracts. This will need to be
//expanded to handle invarients too.