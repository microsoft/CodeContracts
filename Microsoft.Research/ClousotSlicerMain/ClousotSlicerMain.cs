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

#define TRACE_PERFORMANCE

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using Microsoft.Research.DataStructures;
using Microsoft.Research.Slicing;

namespace Microsoft.Research.CodeAnalysis
{
  public interface ISlicerResult
  {
    IEnumerable<string> GetErrors();
  }
  public class SlicesResult<Method> : ISlicerResult
  {
    private readonly List<KeyValuePair<List<Method>, ISliceWorkResult>> slicesResults = new List<KeyValuePair<List<Method>, ISliceWorkResult>>();

    public void AddSliceResult(List<Method> methods, ISliceWorkResult sliceResult)
    {
      this.slicesResults.Add(new KeyValuePair<List<Method>, ISliceWorkResult>(methods, sliceResult));
    }

    public IEnumerable<string> GetErrors()
    {
      return this.slicesResults.Where(p => p.Value != null && p.Value.IsError).Select(p => p.Value.ErrorMessage).ToArray();
    }
  }

  public interface ISliceWorkResult
  {
    bool IsError { get; }
    string ErrorMessage { get; }
  }

  abstract class SliceWorkException : ISliceWorkResult
  {
    private readonly Exception e;

    public SliceWorkException(Exception e)
    {
      this.e = e;
    }

    public bool IsError { get { return true; } }

    public virtual string ErrorMessage
    {
      get { return String.Format("{0}{1}", this.Prefix, e); }
    }

    protected virtual string Prefix { get { return ""; } }
  }

  class WriteSliceToFileException : SliceWorkException
  {
    public WriteSliceToFileException(Exception e) : base(e) { }

    protected override string Prefix { get { return "Exception while writing slice to file: "; } }
  }

  class WorkOnSliceException : SliceWorkException
  {
    public WorkOnSliceException(Exception e) : base(e) { }

    protected override string Prefix { get { return "Exception while working on slice: "; } }
  }

  class WriteSliceToFileError : ISliceWorkResult
  {
    private readonly string sliceName;
    private readonly string directory;

    public WriteSliceToFileError(string sliceName, string directory)
    {
      this.sliceName = sliceName;
      this.directory = directory;
    }

    public bool IsError { get { return true; } }

    public string ErrorMessage
    {
      get { return String.Format("Error while writing slice '{0}' to '{1}'", this.sliceName, this.directory); }
    }
  }
  
#if DEBUG
  [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay}")]
#endif
  public class MethodId : ByteArray
  {
    public MethodId(ByteArray byteArray) : base(byteArray) { }
    public MethodId(byte[] hash) : base(hash) { }

    public static implicit operator MethodId(byte[] bytes) { return new MethodId(bytes); }

#if DEBUG
    private readonly string fullname;

    public MethodId(ByteArray byteArray, string fullname)
      : this(byteArray)
    {
      this.fullname = fullname;
    }

    private string DebuggerDisplay { get { return String.Format("{0}:{1}", this.ToString(), this.fullname); } }
#endif
  }

  public static class ByteArrayExtensions
  {
    public static MethodId AsMethodId(this ByteArray @this) { return new MethodId(@this); }
  }

  /// <summary>
  /// Minimal analizable unit
  /// For each slice, we keep the list of methods and their dependencies
  /// </summary>
  public class SliceDefinition
  {
    private readonly IEnumerable<MethodId> methods;
    private readonly IEnumerable<MethodId> dependencies;
    private readonly IEnumerable<MethodId> methodsInTheSameType;
    private readonly string dll;

    public SliceDefinition(IEnumerable<MethodId> methods, IEnumerable<MethodId> dependencies, IEnumerable<MethodId> methodsInTheSameType, string dll)
    {
      Contract.Requires(methods != null);
      Contract.Requires(dependencies != null);
      Contract.Requires(methodsInTheSameType != null);
      Contract.Requires(dll != null);

      this.methods = methods;
      this.dependencies = dependencies;
      this.methodsInTheSameType = methodsInTheSameType;
      this.dll = dll;
    }

    public IEnumerable<MethodId> Methods { get { return this.methods; } }
    public IEnumerable<MethodId> Dependencies { get { return this.dependencies; } }
    public IEnumerable<MethodId> MethodsInTheSameType { get { return this.methodsInTheSameType; } }
    public string Dll { get { return this.dll; } }
  }

  public interface ISlicerWorker<Method, Field, Type, Assembly>
  {
    ISlicerResult DoWork(Func<SliceDefinition, ISliceWorkResult> workOnSlice);
  }

  public static class SlicerWorkerFactory
  {
    public static ISlicerWorker<Method, Field, Type, Assembly> Worker<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      GeneralOptions options,
      ISimpleLineWriter output,
      ISlicerEnvironment<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> environment
      ) where Type : IEquatable<Type>
        where Method : IEquatable<Method>
    {
      return new SlicerWorker<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(options, output, environment);
    }
  }


  public class SlicerWorker<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : ISlicerWorker<Method, Field, Type, Assembly>
    where Type : IEquatable<Type>
    where Method : IEquatable<Method>
  {
    private readonly Random randGenerator = new Random();
    private readonly GeneralOptions options;
    private readonly ISlicerEnvironment<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> environment;
    private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
    private readonly IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder;
    private readonly IOutput output;

    private readonly byte[] OptionsData;
    private readonly Dictionary<Method, ByteArray> methodHashCache = new Dictionary<Method, ByteArray>();
    private readonly Dictionary<Method, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions>> methodDriverCache = new Dictionary<Method, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions>>();
    private readonly FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB;

    private readonly OldAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ILogOptions, IMethodResult<SymbolicValue>> driver;
    private readonly MethodNumbering methodNumbering = new MethodNumbering();

    public SlicerWorker(
      GeneralOptions options,
      ISimpleLineWriter outputLineWriter,
      ISlicerEnvironment<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> environment
      )
    {
      this.options = options;
      this.environment = environment;
      this.mdDecoder = environment.MetaDataDecoder;
      this.contractDecoder = environment.ContractDecoder;
      this.fieldsDB = new FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(this.mdDecoder);

      this.output = new TextWriterOutputWithPrefix(outputLineWriter.AsTextWriter(), options, "[SlicerWorker] "); // TODO

      var basicDriver = new BasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ILogOptions>(this.mdDecoder, this.contractDecoder, this.output, options);

      this.driver = new OldAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ILogOptions, IMethodResult<SymbolicValue>>(basicDriver);

      this.OptionsData = OptionsHashing.GetOptionsData(environment.MethodAnalyzers, environment.ClassAnalyzers, options);

      EGraphStats.IncrementalJoin = options.incrementalEgraphJoin; // used in OptimisticHeapAnalysis

      // TODO: initialize analysis needed by the slicer, but not more

      // TODO: is that it?
    }

    private IMethodNumbers<Method, Type> MethodOrder(Assembly assembly)
    {
      // Currently use MethodOrder to get the list of methods to analyze

      IMethodOrder<Method, Type> methodOrder;

      if (this.options.MayPropagateInferredRequiresOrEnsures)
      {
        methodOrder = new CallGraphOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          true, this.mdDecoder, this.contractDecoder, this.environment.AssembliesUnderAnalysis, this.fieldsDB, this.cancellationToken);
      }
      else
      {
        methodOrder = new ProtectionBasedMethodOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          this.mdDecoder, this.driver.ContractDecoder, this.fieldsDB, this.cancellationToken);
      }

      methodOrder.AddFilter((m, i) => this.options.analyzeFrom <= i && i <= this.options.analyzeTo);
      if (this.options.select != null && this.options.select.Count > 0)
        methodOrder.AddFilter((m, i) => this.options.select.Contains(i));

      if (!String.IsNullOrEmpty(this.options.NamespaceSelect))
        methodOrder.AddFilter((m, i) => this.mdDecoder.Namespace(this.mdDecoder.DeclaringType(m)).StartsWith(this.options.NamespaceSelect));

      if (!String.IsNullOrEmpty(this.options.TypeNameSelect))
        methodOrder.AddFilter((m, i) => this.mdDecoder.FullName(this.mdDecoder.DeclaringType(m)) == this.options.TypeNameSelect);

      if (!String.IsNullOrEmpty(this.options.memberNameSelect))
        methodOrder.AddFilter((m, i) => this.mdDecoder.DeclaringMemberCanonicalName(m) == this.options.memberNameSelect);

      // WriteLinePhase(output, "Ordering the methods");
      foreach (var t in this.mdDecoder.GetTypes(assembly))
        methodOrder.AddType(t);

      return new MethodNumbers<Method, Type>(this.methodNumbering, methodOrder, this.options.includeCalleesTransitively);
    }

    private IEnumerable<Method> Methods(Assembly assembly)
    {
      var methodOrder = this.MethodOrder(assembly);

      foreach (var method in methodOrder.OrderedMethods())
      {
        if (!this.mdDecoder.HasBody(method))
          continue;

        var namespaceSelected = !String.IsNullOrEmpty(this.options.NamespaceSelect) && this.mdDecoder.Namespace(this.mdDecoder.DeclaringType(method)).StartsWith(this.options.NamespaceSelect);
        var typeSelected = !String.IsNullOrEmpty(this.options.TypeNameSelect) && this.mdDecoder.FullName(this.mdDecoder.DeclaringType(method)) == this.options.TypeNameSelect;
        var memberSelected = !String.IsNullOrEmpty(this.options.MemberNameSelect) && this.mdDecoder.DeclaringMemberCanonicalName(method) == this.options.memberNameSelect;
        if (!this.contractDecoder.VerifyMethod(method, this.options.AnalyzeCompilerGeneratedCode, namespaceSelected, typeSelected, memberSelected))
          continue;

        if (!methodOrder.IsSelected(method)) // aren't all these checks a bit redundant?
          continue;

        if (this.options.IsRegression && !this.MethodHasRegressionAttribute(method))
          continue;

        yield return method;
      }
    }

    public ISlicerResult DoWork(Func<SliceDefinition, ISliceWorkResult> workOnSlice)
    {
      var result = new SlicesResult<Method>();

      var directory = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
      directory += " ";
      if (this.environment.AssembliesToAnalyze.Count <= 2)
        directory += String.Join(" ", this.environment.AssembliesToAnalyze.Select(this.mdDecoder.Name));
      else
        directory += this.mdDecoder.Name(this.environment.AssembliesToAnalyze.First()) + " and others";
      
      var baseDirectory = this.options.cacheDirectory ?? "";

      directory = Path.Combine(baseDirectory, directory);

      if (!Directory.Exists(directory))
        Directory.CreateDirectory(directory);

      foreach (var assembly in this.environment.AssembliesToAnalyze)
      {
        foreach (var method in this.Methods(assembly))
        {
          var methods = new List<Method> { method };
          var declaringType = this.mdDecoder.DeclaringType(method);
          var sliceName = String.Format("{0}.{1}.{2}.{3}", this.mdDecoder.Namespace(declaringType), this.mdDecoder.Name(declaringType), this.mdDecoder.Name(method), this.randGenerator.Next(0x10000).ToString("X4"));
          sliceName = SanitizeName(sliceName);

          output.WriteLine("Building slice {0} ...", sliceName);

#if TRACE_PERFORMANCE
          var startTime = DateTime.Now;
#endif
          var slice = this.BuildSlice(sliceName, assembly, methods, options.InferObjectInvariantsOnlyForReadonlyFields);

          string dll = null;
          ISliceWorkResult sliceResult = null;
          bool writeSliceToFileResult = false;

          try
          {
            // This is Mike
            writeSliceToFileResult = this.environment.CodeWriter.WriteSliceToFile(slice, directory, out dll);
          }
          catch (Exception e)
          {
            sliceResult = new WriteSliceToFileException(e);
          }

          if (sliceResult != null)
          { }
          else if (!writeSliceToFileResult)
          {
            //throw new NotImplementedException();
            output.WriteLine("Failed to write slice {0}", sliceName);

            sliceResult = new WriteSliceToFileError(sliceName, directory);
          }
          else
          {
            if (workOnSlice != null)
            {
#if DEBUG
              Func<Method, MethodId> createMethodId = (Method m) => new MethodId(this.GetMethodHash(m), this.mdDecoder.FullName(m));
#else
              Func<Method, MethodId> createMethodId = m => this.GetMethodHash(m).AsMethodId();
#endif

              var hashedMethods = methods.Select(createMethodId).ToArray(); // ToArray to force computation
              var hashedDependencies = slice.Dependencies.Select(createMethodId).ToArray();
              var hashedMethodsInTheSameType = slice.OtherMethodsInTheSameType.Select(createMethodId).ToArray();

              dll = Path.GetFullPath(dll);

              var sliceDef = new SliceDefinition(hashedMethods, hashedDependencies, hashedMethodsInTheSameType, dll);

              try
              {
                sliceResult = workOnSlice(sliceDef);
              }
              catch (Exception e)
              {
                sliceResult = new WorkOnSliceException(e);
              }
            }
            else
            {
              sliceResult = null; // todo?
            }
          }

#if TRACE_PERFORMANCE
          output.WriteLine("Time to build the slice: {0}, memory: {1}Mb", DateTime.Now - startTime, System.GC.GetTotalMemory(true) / (1024*1024));
#endif
          result.AddSliceResult(methods, sliceResult);
          this.methodDriverCache.Clear();
        }

        //this.methodDriverCache.Clear();
        this.methodHashCache.Clear();
      }

      return result;
    }

    private Slice<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> BuildSlice(string name, Assembly containingAssembly, IEnumerable<Method> methods, bool includeOtherMethodsInTheType)
    {
      var sliceBuilder = new SliceBuilder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, ExternalExpression<APC, SymbolicValue>, SymbolicValue>
        (name, methods, containingAssembly, this.mdDecoder, this.contractDecoder, this.GetMethodDriver, includeOtherMethodsInTheType);

      // We moved this code in the sliceBuilder constructor
      /*
      foreach (Method method in methods)
        sliceBuilder.AddMethod(method);
      */
      return sliceBuilder.ComputeSlice(this.GetMethodHash);
    }

    private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> GetMethodDriver(Method method, out ByteArray methodHash)
    {
      Contract.Ensures(!this.mdDecoder.HasBody(method) || null != Contract.Result<IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions>>());
      Contract.Ensures(!this.mdDecoder.HasBody(method) || null != Contract.ValueAtReturn(out methodHash));

      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mDriver;
      if (this.methodDriverCache.TryGetValue(method, out mDriver))
      {
        methodHash = this.methodHashCache[method];
        return mDriver;
      }

      if (this.mdDecoder.HasBody(method))
      {
        try // TODO: why does it crash?
        {
          mDriver = this.driver.MethodDriver(method, classDriver: null);
          using (var hasher = new MethodHasher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, ExternalExpression<APC, SymbolicValue>, SymbolicValue>(
            this.driver, mDriver, this.options.TraceCacheHashing))
          {
            methodHash = hasher.GetHash(this.OptionsData);
          }
        }
        catch
        {
          methodHash = null;
        }
      }
      else
      {
        // Cannot get a methodDriver of an empty method anyway...
        mDriver = null;
        methodHash = null;
      }
      //this.methodHashCache.Add(method, methodHash);
      this.methodHashCache[method] = methodHash;
      this.methodDriverCache.Add(method, mDriver);
      return mDriver;
    }

    private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> GetMethodDriver(Method method)
    {
      ByteArray methodHash;
      return this.GetMethodDriver(method, out methodHash);
    }

    private ByteArray GetMethodHash(Method method)
    {
      Contract.Ensures(!this.mdDecoder.HasBody(method) || null != Contract.Result<ByteArray>());

      ByteArray methodHash;
      if (!this.methodHashCache.TryGetValue(method, out methodHash))
        this.GetMethodDriver(method, out methodHash);
      return methodHash;
    }

    private bool MethodHasRegressionAttribute(Method method)
    {
      foreach (Attribute attr in this.mdDecoder.GetAttributes(method))
      {
        string typeName = this.mdDecoder.Name(this.mdDecoder.AttributeType(attr));
        if (typeName != "ClousotRegressionTestAttribute")
          continue;
        bool anyArgs = false;
        foreach (object arg in this.mdDecoder.PositionalArguments(attr).Enumerate())
        {
          anyArgs = true;
          string symbol = arg as string;
          if (symbol != null && this.options.define.Contains(symbol))
            return true;
        }
        if (!anyArgs)
          return true; // default configuration
      }
      return false;
    }

    private static readonly char[] InvalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
    private static string SanitizeName(string name)
    {
      const char replacement = '_';
      var res = name.ToCharArray();
      for (var index = 0; ; index++)
      {
        index = name.IndexOfAny(InvalidFileNameChars, index);
        if (index < 0)
          break;
        res[index] = replacement;
      }
      return new String(res);
    }

    public System.Threading.CancellationToken cancellationToken { get; set; }
  }
}
