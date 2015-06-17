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
using Microsoft.Cci;
using Microsoft.Cci.Analysis;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  /// <summary>
  /// CCI2 Slicer entry point
  /// </summary>
  public class ClousotSlicer2 : IDisposable
  {
    private readonly ISlicerWorker<MethodReferenceAdaptor, FieldReferenceAdaptor, TypeReferenceAdaptor, IAssemblyReference> worker;
    private readonly GeneralOptions options;
    private readonly Dictionary<string, IMethodAnalysis> methodAnalyzers;
    private readonly Dictionary<string, IClassAnalysis> classAnalyzers;
    protected readonly List<IDisposable> disposableObjectAllocatedByThisHost = new List<IDisposable>();

    public ClousotSlicer2(GeneralOptions options, ISimpleLineWriter output, Dictionary<string, IMethodAnalysis> methodAnalyzers, Dictionary<string, IClassAnalysis> classAnalyzers)
    {
      var env = Environment.For(options, methodAnalyzers, classAnalyzers);

      this.disposableObjectAllocatedByThisHost.Add(env);

      this.options = options;
      this.methodAnalyzers = methodAnalyzers;
      this.classAnalyzers = classAnalyzers;
      this.worker = SlicerWorkerFactory.Worker(options, output, env);
    }

    public ISlicerResult DoWork(Func<SliceDefinition, ISliceWorkResult> workOnSlice)
    {
      return this.worker.DoWork(workOnSlice);
    }

    #region IDisposable members & co
    private void Close()
    {
      foreach (var disposable in this.disposableObjectAllocatedByThisHost)
        disposable.Dispose();
      this.disposableObjectAllocatedByThisHost.Clear();
    }

    public virtual void Dispose()
    {
      this.Close();
      GC.SuppressFinalize(this);
    }

    ~ClousotSlicer2()
    {
      this.Close();
    }
    #endregion

    class Environment : BaseSlicerEnvironment<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, IPropertyDefinition, IEventDefinition, TypeReferenceAdaptor, ICustomAttribute, IAssemblyReference>, IDisposable
    {
      protected readonly List<IDisposable> disposableObjectAllocatedByThisHost = new List<IDisposable>();
      private readonly CCI2Slicer codeWriter;

      #region Factory

      public static Environment For(GeneralOptions options, Dictionary<string, IMethodAnalysis> methodAnalyzers, Dictionary<string, IClassAnalysis> classAnalyzers)
      {
        // TODO: cache & garbage collector

        var host = new HostEnvironment();
        var env = For(options, methodAnalyzers, classAnalyzers, host);
        if (host is IDisposable)
          env.disposableObjectAllocatedByThisHost.Add((IDisposable)host);

        return env;
      }

      public static Environment For(GeneralOptions options, Dictionary<string, IMethodAnalysis> methodAnalyzers, Dictionary<string, IClassAnalysis> classAnalyzers, HostEnvironment host)
      {
        var codeProvider = CciILCodeProvider.CreateCodeProvider(host);
        var codeWriter = CCI2Slicer.CreateSlicer(host);
        var metadataDecoder = codeProvider.MetaDataDecoder;
        var contractDecoder = codeProvider.ContractDecoder;

        return new Environment(options, codeWriter, metadataDecoder, contractDecoder, methodAnalyzers, classAnalyzers);
      }

      #endregion

      private Environment(GeneralOptions options, CCI2Slicer codeWriter, CciMetadataDecoder metadataDecoder, CciContractDecoder contractDecoder, Dictionary<string, IMethodAnalysis> methodAnalyzers, Dictionary<string, IClassAnalysis> classAnalyzers)
        : base(options, metadataDecoder, contractDecoder, methodAnalyzers, classAnalyzers)
      {
        this.codeWriter = codeWriter;
      }

      public override ICodeWriter<LocalDefAdaptor, IParameterTypeInformation, MethodReferenceAdaptor, FieldReferenceAdaptor, IPropertyDefinition, IEventDefinition, TypeReferenceAdaptor, ICustomAttribute, IAssemblyReference> CodeWriter
      {
        get { return this.codeWriter; }
      }

      protected override void SetTargetPlatform(string framework, string platformOption, IEnumerable<string> resolved, IEnumerable<string> libPaths)
      {
        // Nothing to do for CCI2
      }

      
      #region IDisposable members & co

      private void Close()
      {
        foreach (var disposable in this.disposableObjectAllocatedByThisHost)
          disposable.Dispose();
        this.disposableObjectAllocatedByThisHost.Clear();
      }

      public virtual void Dispose()
      {
        this.Close();
        GC.SuppressFinalize(this);
      }

      ~Environment()
      {
        this.Close();
      }

      #endregion
    }
  }
}
