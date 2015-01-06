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
using System.Text;
using Microsoft.Research.Graphs;
using Microsoft.Research.DataStructures;
using System.IO;

namespace Microsoft.Research.CodeAnalysis {
  /// <summary>
  /// Given a collection of types, computes an approximate call graph 
  /// </summary>
  class CallGraph<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> : 
    IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Unit, Unit> {
    SingleEdgeGraph<Method, Unit> callGraph = new SingleEdgeGraph<Method, Unit>((u1, u2) => u1);
    int methodCount;

    readonly Set<string> assembliesUnderAnalysis = new Set<string>();
    readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> md;

    private bool UnderAnalysis(Method method)
    {
      if (assembliesUnderAnalysis.Contains(md.DeclaringModuleName(md.DeclaringType(method)))) {
        return true;
      }
      return false;
    }

    public CallGraph(IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> md,
                          Set<string> underAnalysis)
    {
      this.assembliesUnderAnalysis = underAnalysis;
      this.md = md;
    }

    #region IMethodOrder<Method,Type> Members


    /// <summary>
    /// Adds the methods of the given type to the method order as well
    /// as recursively calling AddType on nested types within the given type.
    /// Call this with every top-level type to be analyzed.
    /// Internally called with nested types as well.
    /// </summary>
    public void AddType(Type type)
    {
      //TODO:0 HACK HACK HACK for the TechFest demo
      // If it is a compiler generated class, we skip it
      foreach (Attribute a in this.md.GetAttributes(type)) {
        string fullname = md.FullName(md.AttributeType(a));
        if (fullname == "System.CodeDom.Compiler.GeneratedCodeAttribute") {
          return;
        }
        if (fullname == "System.Diagnostics.Contracts.ContractClassForAttribute") {
          return;
        }
      }


      foreach (Method method in md.Methods(type)) {
        if (md.Name(method) == "ObjectInvariant") {
          // ignore contract helper method
          continue;
        }
        this.AddMethod(method);
      }

      foreach (Type nested in md.NestedTypes(type)) {
        this.AddType(nested);
      }
    }

    protected void AddMethod(Method method)
    {
      // Make sure node is in graph
      this.callGraph.AddNode(method);

      // visit the method body
      if (md.HasBody(method)) {
        md.AccessMethodBody(method, this, Unit.Value);
      }
      methodCount++;
    }

    public int Count
    {
      get
      {
        return methodCount;
      }
    }

    public IEnumerable<Method> OrderedMethods()
    {
      // components are reverse ordered
      List<List<Method>> components = StronglyConnectedComponents.Compute(this.callGraph);
      for (int i = components.Count - 1; i >= 0; i--) {
        foreach (Method method in components[i]) {
          yield return method;
        }
      }
    }

    #endregion

    #region IMethodCodeConsumer<Local,Parameter,Method,Field,Type,Unit,Unit> Members

    private class CallQuery<Label, Handler> : MSILVisitor<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>,
      ICodeQuery<Label, Local, Parameter, Method, Field, Type, Unit, Unit>
    {
      CallGraph<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> parent;
      IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> codeProvider;
      Method method;

      public CallQuery(
        IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> codeProvider,
        CallGraph<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> parent,
        Method method
      )
      {
        this.codeProvider = codeProvider;
        this.parent = parent;
        this.method = method;
      }


      public void TraceSequentially(Label current)
      {

        do {
          codeProvider.Decode<CallQuery<Label,Handler>,Unit,Unit>(current, this, Unit.Value);
        } while (codeProvider.Next(current, out current));
      }

      #region ICodeQuery<Label,Type,Method,Unit,Unit> Members

      protected override Unit Default(Label pc, Unit data)
      {
        return data;
      }

      public Unit Aggregate(Label current, Label aggregateStart, bool canBeTargetOfBranch, Unit data)
      {
        TraceSequentially(aggregateStart);
        return data;
      }

      public override Unit Call<TypeList, ArgList>(Label pc, Method calledMethod, bool tail, bool virt, TypeList extraVarargs, Unit dest, ArgList args, Unit data)
      {
        this.parent.AddEdge(this.method, calledMethod);
        return data;
      }

      public override Unit ConstrainedCallvirt<TypeList, ArgList>(Label pc, Method calledMethod, bool tail, Type constraint, TypeList extraVarargs, Unit dest, ArgList args, Unit data)
      {
        this.parent.AddEdge(this.method, calledMethod);
        return data;
      }

      public override Unit Newobj<ArgList>(Label pc, Method ctor, Unit dest, ArgList args, Unit data)
      {
        this.parent.AddEdge(this.method, ctor);
        return data;
      }

      #endregion
    }
    public Unit Accept<Label, Handler>(
      IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> codeProvider,
      Label entryPoint, Method method, Unit data)
    {
      CallQuery<Label, Handler> query = new CallQuery<Label, Handler>(codeProvider, this, method);

      query.TraceSequentially(entryPoint);
      return Unit.Value;
    }


    #endregion

    void AddEdge(Method from, Method calledMethod)
    {
      // only add edge if target is in analyzed assemblies
      if (md.IsSpecialized(calledMethod)) {
        calledMethod = md.Unspecialized(calledMethod);
      }
      //if (this.UnderAnalysis(calledMethod)) {
      this.callGraph.AddEdge(from, Unit.Value, calledMethod);
      //}
    }

    public void EmitDotFile(string file)
    {
      TextWriter tw = new StreamWriter(file);
      tw.WriteLine("digraph callgraph {");

      foreach (Method node in this.callGraph.Nodes) {
        WriteNode(tw, node);

        foreach (Pair<Unit, Method> edge in this.callGraph.Successors(node)) {
          WriteEdge(tw, node, edge.Two);
        }
      }

      tw.WriteLine("}");
      tw.Close();
    }

    string MethodID(Method node)
    {
      return String.Format("\"{0}!{1}\"", md.DeclaringModule(md.DeclaringType(node)), md.MethodToken(node));
    }
    void WriteNode(TextWriter tw, Method node)
    {
      tw.WriteLine("  {0} [shape=box, label=\"{1}\"] ;", MethodID(node), md.FullName(node)); 
    }
    void WriteEdge(TextWriter tw, Method source, Method target)
    {
      tw.WriteLine("  {0} -> {1} ;", MethodID(source), MethodID(target));
    }
  }

}