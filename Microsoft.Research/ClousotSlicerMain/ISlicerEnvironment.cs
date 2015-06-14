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
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  /// <summary>
  /// The environment should be in charge of loading assemblies but no more
  /// It should only depend on framework, plaform, and assembly-related options
  /// </summary>

  public interface ISlicerEnvironment<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
  {
    IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get; }
    IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder { get; }
    ICodeWriter<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> CodeWriter { get; }
    List<Assembly> AssembliesToAnalyze { get; }
    Set<string> AssembliesUnderAnalysis { get; }
    Dictionary<string, IMethodAnalysis> MethodAnalyzers { get; }
    Dictionary<string, IClassAnalysis> ClassAnalyzers { get; }
  }


  public abstract class BaseSlicerEnvironment<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : ISlicerEnvironment<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
  {
    protected readonly GeneralOptions options;
    protected readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadataDecoder;
    protected readonly IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder;
    protected readonly Dictionary<string, IMethodAnalysis> methodAnalyzers;
    protected readonly Dictionary<string, IClassAnalysis> classAnalyzers;
    protected readonly System.Collections.IDictionary assemblyCache = new System.Collections.Hashtable();
    protected readonly List<Assembly> assembliesToAnalyze = new List<Assembly>();
    protected readonly Set<string> assembliesUnderAnalysis = new Set<string>(StringComparer.InvariantCultureIgnoreCase);

    protected BaseSlicerEnvironment(
      GeneralOptions options,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadataDecoder,
      IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
      Dictionary<string, IMethodAnalysis> methodAnalyzers,
      Dictionary<string, IClassAnalysis> classAnalyzers)
    {
      this.options = options;
      this.metadataDecoder = metadataDecoder;
      this.contractDecoder = contractDecoder;
      this.methodAnalyzers = methodAnalyzers;
      this.classAnalyzers = classAnalyzers;

      // set shared contract library
      this.metadataDecoder.SharedContractClassAssembly = options.cclib;
      this.SetTargetPlatform(options.framework, options.platform, options.resolvedPaths, options.libPaths);

      this.LoadAssemblies(options.ContractAssemblies, errorHandler: null, onSuccess: null, onError: null);
      // TODO: report environment loading errors
      //output.WriteLine("Cannot load contract assembly '{0}'", contractAssembly);
      //options.AddError();

      this.assembliesUnderAnalysis.AddRange(this.options.Assemblies.Select(Path.GetFileNameWithoutExtension));

      this.LoadAssemblies(options.Assemblies, errorHandler: null /* this.output.EmitError */, onSuccess: this.assembliesToAnalyze.Add, onError: null);
      // Console.WriteLine("Cannot load assembly '{0}'", assembly);
      // this.options.AddError();
    }

    protected abstract void SetTargetPlatform(string framework, string platformOption, IEnumerable<string> resolved, IEnumerable<string> libPaths);

    public void LoadAssemblies(List<string> assemblies, Action<System.CodeDom.Compiler.CompilerError> errorHandler, Action<Assembly> onSuccess, Action<string> onError)
    {
      foreach (string assembly in assemblies)
      {
        Assembly assem;
        if (this.metadataDecoder.TryLoadAssembly(assembly, this.assemblyCache, errorHandler, out assem, this.options.IsLegacyAssemblyMode, this.options.reference, this.options.extractSourceText))
        {
          if (onSuccess != null)
            onSuccess(assem);
        }
        else if (onError != null)
          onError(assembly);
      }
    }

    public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get { return this.metadataDecoder; } }
    public IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder { get { return this.contractDecoder; } }
    public abstract ICodeWriter<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> CodeWriter { get; }
    public List<Assembly> AssembliesToAnalyze { get { return this.assembliesToAnalyze; } }
    public Set<string> AssembliesUnderAnalysis { get { return this.assembliesUnderAnalysis; } }
    public Dictionary<string, IMethodAnalysis> MethodAnalyzers { get { return this.methodAnalyzers; } }
    public Dictionary<string, IClassAnalysis> ClassAnalyzers { get { return this.classAnalyzers; } }
  }

}
