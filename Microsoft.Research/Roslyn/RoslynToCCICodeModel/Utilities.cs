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
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Semantics;
using Microsoft.Cci;
using Microsoft.Cci.MutableCodeModel;
using System.Diagnostics.Contracts;
using Microsoft.Cci.Contracts;
using Microsoft.Cci.MutableContracts;
using Microsoft.CodeAnalysis.Common;

namespace CSharp2CCI {
  public class Utilities {

    public static void ConvertRoslynToCCI(
      IMetadataHost host,
      Microsoft.CodeAnalysis.CSharp.SyntaxTree tree,
      Microsoft.CodeAnalysis.CSharp.Semantics.SemanticModel semanticModel,
      out IModule module,
      out ISourceLocationProvider sourceLocationProvider) {

      sourceLocationProvider = new SourceLocationProvider();

      var transformer = new NodeVisitor(host, semanticModel, sourceLocationProvider);
      var assembly = (Module)transformer.Visit(tree.GetRoot());
      assembly.Location = Path.GetFullPath(tree.FilePath);
      module = assembly;
    }

    /// <summary>
    /// Note that it leaves any calls to contract methods in their original locations,
    /// i.e., it does *not* extract contracts. That is up to the caller of this method.
    /// </summary>
    public static bool FileToUnitAndSourceLocationProvider(IMetadataHost host, string filename,
      List<string> libPaths,
      List<string> referencedAssemblies,
      out IModule module,
      out ISourceLocationProvider/*?*/ sourceLocationProvider) {

      var text = File.ReadAllText(filename);
      SyntaxTree tree = SyntaxTree.ParseText(text, filename);
      // This ctor isn't implemented yet.
      //var mscorlib = new Roslyn.Compilers.AssemblyObjectReference(typeof(object).Assembly);
      var mscorlib = MetadataReference.CreateAssemblyReference("mscorlib"); // Roslyn takes care of resolving where it lives.
      var refs = new List<MetadataReference>();
      refs.Add(mscorlib);
      foreach (var r in referencedAssemblies) {
        refs.Add(MetadataReference.CreateAssemblyReference(Path.GetFileNameWithoutExtension((r))));
      }

      var baseFileName = Path.GetFileNameWithoutExtension(filename);

      var defaultResolver = FileResolver.Default;
      var ar = new FileResolver(ReadOnlyArrayExtensions.AsReadOnly(libPaths.Select(lp => Path.GetFullPath(lp))), defaultResolver.KeyFileSearchPaths, defaultResolver.BaseDirectory, defaultResolver.ArchitectureFilter, defaultResolver.PreferredCulture);
      var c = Compilation.Create(
        baseFileName
        , syntaxTrees: new SyntaxTree[] { tree }
        , references: refs
        , fileResolver: ar
        , options: new CompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );
      var diags = c.GetDiagnostics();
      if (diags.Count() > 0) {
        foreach (var diag in diags) {
          Console.WriteLine(diag.ToString());
        }
        if (diags.Any(d => d.Info.Severity == DiagnosticSeverity.Error || d.Info.IsWarningAsError))
        {
          module = Dummy.Module;
          sourceLocationProvider = null;
          return false;
        }
      }
      //c = c.AddSyntaxTrees(tree); // Use this form to add another syntax tree to the same compilation (getting a new compilation since they're immutable)
      var binding = c.GetSemanticModel(tree);
      diags = binding.GetDiagnostics();
      if (diags.Count() > 0) {
        foreach (var d in diags) {
          Console.WriteLine(d.ToString());
        }
        if (diags.Any(d => d.Info.Severity == DiagnosticSeverity.Error || d.Info.IsWarningAsError))
        {
          module = Dummy.Module;
          sourceLocationProvider = null;
          return false;
        }
      }
      ConvertRoslynToCCI(host, tree, binding, out module, out sourceLocationProvider);
      return true;
    }

  }
}

namespace RoslynToCCICodeModel {

  public static class DocumentExtensions
  {
      public static CommonSyntaxTree GetSyntaxTree(this Document d)
      {
          return d.GetSyntaxTreeAsync().Result;
      }

  }
}
