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
using Microsoft.Research.DataStructures;
using Microsoft.Research.Graphs;
using Microsoft.Research.Slicing;
using System.Diagnostics.Contracts;
using System.Threading;

namespace Microsoft.Research.CodeAnalysis
{
  public delegate bool MethodFilter<Method>(Method m, int index);

  public interface IMethodOrder<Method, Type>
  {
    /// <summary>
    /// Adds the methods of the given type to the method order as well
    /// as recursively calling AddType on nested types within the given type (if necessary).
    /// Call this with every top-level type to be analyzed.
    /// Internally called with nested types as well.
    /// </summary>
    void AddType(Type type);

    /// <summary>
    /// Adds a filter on the methods : a method will be analysed if it satisfies all the filters.
    /// If the order has a notion of dependency, it will also analyses the dependant methods 
    /// (in the same order as if there were no filters).
    /// </summary>
    /// <param name="filter"></param>
    void AddFilter(MethodFilter<Method> filter);

    /// <summary>
    /// The final order of the methods
    /// </summary>
    IEnumerable<Method> OrderedMethods();

    /// <summary>
    /// Only the selected methods, according to their final number.
    /// Methods are given in the same order as returned by OrderedMethods
    /// </summary>
    IEnumerable<Method> SelectedMethods(IEnumerable<Pair<Method, int>> numberedMethods, bool includeCalleesTransitively, Predicate<Method> ExtraFilter = null);
  }

  /// <summary>
  /// Interface to access methods in a particular order.
  /// Implementations typically try to order methods in a call-bottom up fashion
  /// </summary>
  public interface IMethodNumbers<Method, Type>
  {
    /// <summary>
    /// Returns true if the method is to be analysed, i.e. either it satisfies all the filters or it is a 
    /// dependency (maybe indirect) of a method that satisfies all the filters.
    /// It Must be called with one of the method instance returned by OrderedMethods
    /// </summary>
    bool IsSelected(Method method);

    /// <summary>
    /// Total number of recorded methods
    /// </summary>
    int Count { get; }

    /// <summary>
    /// The final order of the methods
    /// </summary>
    IEnumerable<Method> OrderedMethods();

    /// <summary>
    /// Get The rank of the method. 1-based. The rank is static (it is not resetted between assemblies).
    /// Can be asked only when the method has been enumerated in the OrderedMethods method.
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    int GetMethodNumber(Method method);
  }

  public abstract class BaseMethodOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : IMethodOrder<Method, Type>
  {
    protected readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md;
    protected readonly IDecodeContracts<Local, Parameter, Method, Field, Type> cd;
    protected readonly List<MethodFilter<Method>> filters = new List<MethodFilter<Method>>();
    protected readonly FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB;

    protected BaseMethodOrder(
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md,
      IDecodeContracts<Local, Parameter, Method, Field, Type> cd,
      FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB)
    {
      Contract.Requires(fieldsDB != null);

      this.md = md;
      this.cd = cd;
      this.fieldsDB = fieldsDB;
    }

    /// <summary>
    /// Adds the methods of the given type to the method order as well
    /// as recursively calling AddType on nested types within the given type (if necessary).
    /// Call this with every top-level type to be analyzed.
    /// Internally called with nested types as well.
    /// </summary>
    public virtual void AddType(Type type)
    {
      foreach (var a in this.md.GetAttributes(type))
      {
        var fullname = this.md.FullName(md.AttributeType(a));
        
        if (fullname == "System.Diagnostics.Contracts.ContractClassForAttribute")
          return;
      }

      foreach (var method in this.md.Methods(type).OrderBy(m => md.FullName(m)))
      {
        if (this.IsObjectInvariantMethod(method))
          continue; // ignore contract helper method

        if (this.md.IsConstructor(method))
          continue; // added separately
        this.AddMethod(method);
      }

      // add constructors so they have a chance to be analyzed first
      foreach (var method in this.md.Methods(type))
        if (this.md.IsConstructor(method) && !this.md.IsMethodHashAttributeConstructor(method))
          this.AddMethod(method);

      foreach (var nested in this.md.NestedTypes(type))
        this.AddType(nested);
    }

    public void AddFilter(MethodFilter<Method> filter)
    {
      this.filters.Add(filter);
    }

    protected bool IsSelected(Pair<Method, int> methodAndIndex)
    {
      return this.filters.All(p => p(methodAndIndex.One, methodAndIndex.Two));
    }

    private bool IsObjectInvariantMethod(Method method)
    {
      if (this.md.Name(method) == "ObjectInvariant")
        return true;

      return this.md.GetAttributes(method).Any(attr => this.md.Name(this.md.AttributeType(attr)) == "ContractInvariantMethodAttribute");
    }

    /// <summary>
    /// The final order of the methods
    /// </summary>
    public abstract IEnumerable<Method> OrderedMethods();

    public virtual IEnumerable<Method> SelectedMethods(IEnumerable<Pair<Method, int>> methods, bool includeCalleesTransitively, Predicate<Method> ExtraFilter)
    {
      return methods.Where(this.IsSelected).Select(Pair.One).Where(m => ExtraFilter == null || ExtraFilter(m));
    }

    /// <summary>
    /// Called from the AddType method
    /// </summary>
    protected abstract void AddMethod(Method method);
  }

  /// <summary>
  /// Given a collection of types, computes an order for the methods such that we analyze 
  /// less visible methods before more visible ones, setters before getters and before other methods
  /// </summary>
  public class ProtectionBasedMethodOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : BaseMethodOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
  {

    class VisibilityGroup
    {
      public readonly List<Method> Setters = new List<Method>();
      public readonly List<Method> Getters = new List<Method>();
      public readonly List<Method> Ordinary = new List<Method>();

      public void Add(Method method, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md)
      {
        string name = md.Name(method);
        if (name.StartsWith("set_"))
          this.Setters.Add(method);
        else if (name.StartsWith("get_"))
          this.Getters.Add(method);
        else
          this.Ordinary.Add(method);
      }

      public IEnumerable<Method> Methods
      {
        get
        {
          return this.Setters.Concat(this.Getters).Concat(this.Ordinary);
        }
      }
    }

    private readonly VisibilityGroup privateMethods = new VisibilityGroup();
    private readonly VisibilityGroup internalMethods = new VisibilityGroup();
    private readonly VisibilityGroup publicMethods = new VisibilityGroup();
    private CancellationToken cancellationToken;

    protected override void AddMethod(Method method)
    {
      cancellationToken.ThrowIfCancellationRequested();

      if (this.md.IsPrivate(method)) {
        this.privateMethods.Add(method, md);
      }
      else if (this.md.IsVisibleOutsideAssembly(method)) {
        this.publicMethods.Add(method, md);
      }
      else {
        this.internalMethods.Add(method, md);
      }
    }

    public ProtectionBasedMethodOrder(
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md,
      IDecodeContracts<Local, Parameter, Method, Field, Type> cd,
      FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB,
      CancellationToken cancellationToken)
      : base(md, cd, fieldsDB)
    {
      this.cancellationToken = cancellationToken;
    }

    public override IEnumerable<Method> OrderedMethods()
    {
      return this.privateMethods.Methods.Concat(this.internalMethods.Methods).Concat(this.publicMethods.Methods);
    }
  }

  /// <summary>
  /// Given a collection of types, computes an approximate call graph to order the methods bottom up.
  /// </summary>
  public class CallGraphOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> :
    BaseMethodOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>,
    IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Unit, Unit>
    where Type : IEquatable<Type>
    where Method : IEquatable<Method>
  {
    struct Node : IEquatable<Node>
    {
      public enum Tag { method, type }

      private readonly Tag tag;
      private readonly Method method;
      private readonly Type type;

      public Tag Kind { get { return this.tag; } }
      public Method Method
      {
        get
        {
          // Contract.Requires(this.Tag == Tag.method);
          return this.method;
        }
      }

      private Node(Method m)
      {
        this.tag = Tag.method;
        this.method = m;
        this.type = default(Type);
      }

      private Node(Type t)
      {
        this.tag = Tag.type;
        this.type = t;
        this.method = default(Method);
      }

      public static Node For(Method m)
      {
        return new Node(m);
      }

      public static Node For(Type t)
      {
        return new Node(t);
      }

      #region IEquatable<Node> members

      public override bool Equals(object obj)
      {
        return obj is Node && this.Equals((Node)obj);
      }

      public bool Equals(Node that)
      {
        if (this.tag != that.tag)
          return false;

        // MB: here we are using the IEquatable.Equals method for Types and for Methods
        switch (this.tag)
        {
          case Tag.method:
            return this.method.Equals(that.method);
          case Tag.type:
            return this.type.Equals(that.type);
          default:
            return true;
        }
      }

      public override int GetHashCode()
      {
        switch (tag)
        {
          case Tag.method:
            return this.method.GetHashCode();
          case Tag.type:
            return this.type.GetHashCode();
          default:
            return 1;
        }
      }

      #endregion

      public class EqualityComparer : IEqualityComparer<Node>
      {
        private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MD;

        public EqualityComparer(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MD)
        {
          this.MD = MD;
        }

        public bool Equals(Node x, Node y)
        {
          if (x.tag != y.tag)
            return false;

          switch (x.tag)
          {
            case Tag.method:
              return this.MD.Equal(x.method, y.method);
            case Tag.type:
              return this.MD.Equal(x.type, y.type);
            default:
              return true;
          }
        }

        public int GetHashCode(Node x)
        {
          return x.GetHashCode();
        }
      }
    }
    /// <summary>
    /// Stores edge information from caller to callee
    /// </summary>
    private readonly SingleEdgeGraph<Node, Unit> callGraph;
    private readonly bool constructorsFirst;
    private readonly Set<string> assembliesUnderAnalysis;
    private readonly ICodeConsumer<Local, Parameter, Method, Field, Type, Node, Unit> contractConsumer;
    private CancellationToken cancellationToken;

    public CallGraphOrder(
      bool constructorsFirst,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md,
      IDecodeContracts<Local, Parameter, Method, Field, Type> cd,
      Set<string> underAnalysis,
      FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB,
      CancellationToken cancellationToken)
      : base(md, cd, fieldsDB)
    {
      this.constructorsFirst = constructorsFirst;
      this.callGraph = new SingleEdgeGraph<Node, Unit>(null, new Node.EqualityComparer(md));
      this.assembliesUnderAnalysis = underAnalysis;
      this.contractConsumer = new ContractConsumer(this);
      this.cancellationToken = cancellationToken;
    }

    #region IBuildMethodOrder<Method,Type> Members

    public override void AddType(Type type)
    {
      cancellationToken.ThrowIfCancellationRequested();

      Node node = Node.For(type);
      if (this.callGraph.Contains(node))
        return;

      base.AddType(type);
      this.callGraph.AddNode(node);

      if (this.cd.HasInvariant(type))
        this.cd.AccessInvariant<Node, Unit>(type, this.contractConsumer, node);
    }
    
    protected override void AddMethod(Method method)
    {
      cancellationToken.ThrowIfCancellationRequested();

      // Make sure node is in graph
      var node = Node.For(method);
      this.callGraph.AddNode(node);

      if(this.constructorsFirst)
      {
        // We only get add the dependency node -> constructor if :
        //   * it is a proper method, or
        //   * it is an autoproperty
        if (!this.md.IsConstructor(method) && (!this.md.IsPropertyGetterOrSetter(method) || this.md.IsAutoPropertyMember(method)))
  
//        if(!this.md.IsConstructor(method) && !this.md.IsPropertySetter(method))
        {
          var declaringType = this.md.DeclaringType(method);
          foreach (var constructor in md.Methods(declaringType).Where(m => this.md.IsConstructor(m)))
          {
            this.AddEdge(node, constructor);
          }
        }
      }

      // visit the method body
      if (this.md.HasBody(method))
      {
        this.md.AccessMethodBody(method, this, Unit.Value);
        
        if (this.cd.HasRequires(method))
          this.cd.AccessRequires<Node, Unit>(method, this.contractConsumer, node);

        if (this.cd.HasEnsures(method))
          this.cd.AccessEnsures<Node, Unit>(method, this.contractConsumer, node);

        if (this.cd.HasInvariant(method))
          this.AddEdge(node, this.md.DeclaringType(method));

        this.fieldsDB.NotifySettersCalledFromTheMethod(method, this.callGraph.Successors(node).Where(pair => pair.Two.Method != null && this.md.IsPropertySetter(pair.Two.Method)).Select(pair => pair.Two.Method));
      }
    }

    // Callgraph-ordered
    public override IEnumerable<Method> OrderedMethods()
    {
      var components = StronglyConnectedComponents.Compute(this.callGraph);
      // components are in caller to callee order, so reverse it
      components.Reverse();

      var classGraph = new SingleEdgeGraph<Type, Unit>(null);
      var mAlreadyThere = new Set<Type>();
      Predicate<Node> nodeStartVisitor = n =>
        {
          if (n.Kind != Node.Tag.method) return true;
          var m = n.Method;
          var dt = this.md.DeclaringType(m);
          if (mAlreadyThere.Add(dt))
            classGraph.AddNode(dt);
          // add edge dt -> type(each successor)
          foreach (var suc in this.callGraph.Successors(n))
          {
            if (suc.Two.Kind != Node.Tag.method) continue;
            var succmethod = suc.Two.Method;
            var tsuc = this.md.DeclaringType(succmethod);
            if (mAlreadyThere.Add(tsuc))
              classGraph.AddNode(tsuc);
            classGraph.AddEdge(dt, Unit.Value, tsuc);
          }
          return true;
        };
      var dfs_construct_class_graph = new DepthFirst.Visitor<Node, Unit>(this.callGraph, nodeStartVisitor);
      dfs_construct_class_graph.VisitAll();

      // Now get the ordering of the class graph
      var class_components = StronglyConnectedComponents.Compute(classGraph);
      class_components.Reverse();

      // Stores the class order
      var class_order = new Dictionary<Type, int>();
      int index = 0;
      foreach (var compo in class_components)
        foreach (var cl in compo)
          class_order.Add(cl, index++);

      var indexes = new Dictionary<Method, int>();

      Comparison<Method> compareMethods = (m1, m2) =>
        {
          var t1 = class_order[this.md.DeclaringType(m1)];
          var t2 = class_order[this.md.DeclaringType(m2)];
          if (t1 != t2)
            return t2 - t1;
          // regarding to class order, m1 == m2, i.e. keep global methods order
          return indexes[m2] - indexes[m1];
        };

      // Stores the global method order to decide inside class graph strong components
      // And populate the method list
      var ordered_methods = new PriorityQueue<Method>(compareMethods);

      index = 0;
      foreach (var component in components)
        foreach (var node in component)
          if (node.Kind == Node.Tag.method)
          {
            indexes[node.Method] = index++;
            ordered_methods.Add(node.Method);
          }

      while (ordered_methods.Count > 0)
        yield return ordered_methods.Pull();
    }

    public override IEnumerable<Method> SelectedMethods(IEnumerable<Pair<Method, int>> methods, bool includeCalleesTransitively, Predicate<Method> ExtraFilter)
    {
      // todo list is ordered by method index from high to low
      var localIndex = new Dictionary<Method, int>();
      var todo = new PriorityQueue<Method>((x, y) => localIndex[x] - localIndex[y]);
      var totalOrder = new List<Method>();
      var selected = new List<Method>();

      foreach (var p in methods)
      {
        var method = p.One;
        localIndex[method] = totalOrder.Count;
        totalOrder.Add(method);
        if (this.IsSelected(p) && (ExtraFilter == null || ExtraFilter(method)))
          todo.Add(method);
      }

      while (todo.Count > 0)
      {
        var method = todo.Pull();
        var index = localIndex[method];
        selected.Add(method);

        // Console.WriteLine("Working on method index {0} '{1}'", index, this.md.FullName(method));

        if (includeCalleesTransitively)
        {
          // recall that the successors here are methods we called, thus potentially precede us in order
          foreach (var pred in this.callGraph.Successors(Node.For(method)))
          {
            if (pred.Two.Kind != Node.Tag.method) continue; // means we are skipping some dependencies due to invariants

            var succMethod = pred.Two.Method;
            // Console.WriteLine("  considering {0} (index {1})", this.md.FullName(pred.Two), dictionary[pred.Two]);
            // due to cycles we need to only add preds that have lower index.
            var predIndex = localIndex[succMethod];
            if (predIndex >= index) continue;

            // recursively find dependencies
            todo.Add(succMethod);
          }
        }
      }

      return selected;
    }

    #endregion

    #region IMethodCodeConsumer<Local,Parameter,Method,Field,Type,Unit,Unit> Members

    private class CallQuery<Label> : MSILVisitor<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>, ICodeQuery<Label, Local, Parameter, Method, Field, Type, Unit, Unit> 
    {
      private readonly CallGraphOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent;
      private readonly ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider;
      private readonly Node currentNode;
      public readonly Set<Field> StoredFields = new Set<Field>();

      public CallQuery(
        ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider,
        CallGraphOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent,
        Node currentNode
      )
      {
        this.codeProvider = codeProvider;
        this.parent = parent;
        this.currentNode = currentNode;
      }

      public void TraceSequentially(Label current)
      {
        do
        {
          this.codeProvider.Decode<CallQuery<Label>, Unit, Unit>(current, this, Unit.Value);
        } while (this.codeProvider.Next(current, out current));
      }

      #region ICodeQuery<Label,Type,Method,Unit,Unit> Members

      protected override Unit Default(Label pc, Unit data)
      {
        return data;
      }

      public Unit Aggregate(Label current, Label aggregateStart, bool canBeTargetOfBranch, Unit data)
      {
        this.TraceSequentially(aggregateStart);
        return data;
      }

      public override Unit Call<TypeList, ArgList>(Label pc, Method method, bool tail, bool virt, TypeList extraVarargs, Unit dest, ArgList args, Unit data)
      {
        this.parent.AddEdge(this.currentNode, method);
        return data;
      }

      public override Unit ConstrainedCallvirt<TypeList, ArgList>(Label pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Unit dest, ArgList args, Unit data)
      {
        this.parent.AddEdge(this.currentNode, method);
        return data;
      }

      public override Unit Newobj<ArgList>(Label pc, Method ctor, Unit dest, ArgList args, Unit data)
      {
        this.parent.AddEdge(this.currentNode, ctor);
        return data;
      }

      public override Unit Ldflda(Label pc, Field field, Unit dest, Unit obj, Unit data)
      {
        this.StoredFields.Add(field);
        return base.Ldflda(pc, field, dest, obj, data);
      }

      public override Unit Stfld(Label pc, Field field, bool @volatile, Unit obj, Unit value, Unit data)
      {
        this.StoredFields.Add(field);
        return base.Stfld(pc, field, @volatile, obj, value, data);
      }
      public override Unit Ldfld(Label pc, Field field, bool @volatile, Unit dest, Unit obj, Unit data)
      {
        return base.Ldfld(pc, field, @volatile, dest, obj, data);
      }

      #endregion
    }

    public Unit Accept<Label, Handler>(IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> codeProvider, Label entryPoint, Method method, Unit data)
    {
      CallQuery<Label> query = new CallQuery<Label>(codeProvider, this, Node.For(method));

      query.TraceSequentially(entryPoint);

      this.fieldsDB.NotifyPossiblyModifiedFields(method, query.StoredFields);

      return Unit.Value;
    }

    #endregion

    #region private helpers

    private bool UnderAnalysis(Method method)
    {
      return this.assembliesUnderAnalysis.Contains(this.md.DeclaringModuleName(this.md.DeclaringType(method)));
    }

    private void AddEdge(Node from, Method calledMethod)
    {
      // only add edge if target is in analyzed assemblies
      if (this.md.IsSpecialized(calledMethod))
        calledMethod = this.md.Unspecialized(calledMethod);

      if (this.UnderAnalysis(calledMethod))
        this.callGraph.AddEdge(from, Unit.Value, Node.For(calledMethod)); // NOTE: add edge from caller to callee
    }

    private void AddEdge(Node from, Type invariant)
    {
      // assuming type is generic here, not specialized and in current module

      this.callGraph.AddEdge(from, Unit.Value, Node.For(invariant));
    }

    #endregion

    class ContractConsumer : ICodeConsumer<Local, Parameter, Method, Field, Type, Node, Unit>
    {
      private readonly CallGraphOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent;

      public ContractConsumer(CallGraphOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> parent)
      {
        this.parent = parent;
      }

      #region ICodeConsumer<Local,Parameter,Method,Field,Type,Method,Unit> Members
      Unit ICodeConsumer<Local, Parameter, Method, Field, Type, Node, Unit>.Accept<Label>(ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider, Label entryPoint, Node current)
      {
        CallQuery<Label> query = new CallQuery<Label>(codeProvider, parent, current);

        query.TraceSequentially(entryPoint);
        return Unit.Value;
      }


      #endregion
    }

  }

  public class HashedFirstMethodOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : IMethodOrder<Method, Type>
  {
    private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md;
    private readonly IMethodOrder<Method, Type> underlying;
    private Dictionary<Method, int> privateIndex;

    public HashedFirstMethodOrder(
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md,
      IMethodOrder<Method, Type> underlyingMethodOrder)
    {
      this.md = md;
      this.underlying = underlyingMethodOrder;
    }

    public void AddType(Type type)
    {
      this.underlying.AddType(type);
    }

    public void AddFilter(MethodFilter<Method> filter)
    {
      this.underlying.AddFilter(filter);
    }

    public IEnumerable<Method> OrderedMethods()
    {
      var ordered = this.underlying.OrderedMethods().ToList();
      var splittedOrderedMethods = ordered.Split(this.md.MethodHashAttributeFirstInOrder);
      if (splittedOrderedMethods.One.Any() && splittedOrderedMethods.Two.Any())
        this.privateIndex = ordered.Select(KeyValuePair.For).ToDictionary();
      return splittedOrderedMethods.Two.Concat(splittedOrderedMethods.One);
    }

    public IEnumerable<Method> SelectedMethods(IEnumerable<Pair<Method, int>> methods, bool includeCalleesTransitively, Predicate<Method> ExtraFilter)
    {
      var reorder = this.privateIndex == null ? methods : methods.OrderBy(p => this.privateIndex[p.One]); // retrieve original order, if needed
      return this.underlying.SelectedMethods(reorder, includeCalleesTransitively, ExtraFilter);
    }
  }

  public class MethodNumbers<Method, Type> : IMethodNumbers<Method, Type>
  {
    private readonly List<Method> ordered;
    private readonly Dictionary<Method, int> number = new Dictionary<Method, int>();
    private readonly Set<Method> selected;

    private readonly List<Pair<Method, int>> methodsNumbered;
    private readonly IMethodOrder<Method, Type> methodOrder;

    public MethodNumbers(MethodNumbering numbering, IMethodOrder<Method, Type> methodOrder, bool includeCalleesTransitively)
    {
      this.methodsNumbered = new List<Pair<Method, int>>();
      this.methodOrder = methodOrder;

      this.ordered = methodOrder.OrderedMethods().ToList();
      foreach (var m in this.ordered)
      {
        var index = numbering.Next();
        if (this.number.ContainsKey(m)) continue; // there may be repetition when we used the constructor first approach
        this.number.Add(m, index); // we really want numbering.Next() called only once per method
        this.methodsNumbered.Add(Pair.For(m, index));
      }

      this.selected = new Set<Method>(methodOrder.SelectedMethods(this.methodsNumbered, includeCalleesTransitively));
    }

    public bool IsSelected(Method method) { return this.selected.Contains(method); }

    public int Count { get { return this.ordered.Count; } }

    public IEnumerable<Method> OrderedMethods() { return this.ordered; }

    public int GetMethodNumber(Method method) { return this.number[method]; }

    public IEnumerable<Method> GetMethodBucket(Method method)
    {
      var index = this.number.Where(pair => pair.Key.Equals(method)).First().Value;
     // We are recomputing it each time. Best to have a primitive in method order that builds it once and for all, but this is just easier for the implementation
      return this.methodOrder.SelectedMethods(this.methodsNumbered, true, m => method.Equals(m));
    }

  }

  public class MethodNumbering
  {
    private int current = 0;

    public int LastNumber { get { return this.current; } }

    internal int Next()
    {
      return ++this.current;
    }
  }

  /// <summary>
  /// First propose all the methods with the read from cache attribute, then continue with all the other methods, according to the CallGraphOrder
  /// </summary>
  public class HashedFirstAndTheCallGraphMethodOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : CallGraphOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    where Type : IEquatable<Type>
    where Method : IEquatable<Method>
  {
    #region Invariant
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.methodsToBeOnlyReadFromTheCache != null);
    }

    #endregion

    #region Private state
    private readonly List<Method> methodsToBeOnlyReadFromTheCache;
    #endregion

    public HashedFirstAndTheCallGraphMethodOrder(
      bool constructorsFirst,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md,
      IDecodeContracts<Local, Parameter, Method, Field, Type> cd,
      Set<string> underAnalysis,
      FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB,
      CancellationToken cancellationToken)
      : base(constructorsFirst, md, cd, underAnalysis, fieldsDB, cancellationToken)
    {
      this.methodsToBeOnlyReadFromTheCache = new List<Method>();
    }

    protected override void AddMethod(Method method)
    {
      if (this.md.GetMethodHashAttributeFlags(method).HasFlag(MethodHashAttributeFlags.OnlyFromCache))
      {
        this.methodsToBeOnlyReadFromTheCache.Add(method);
      }
      else
      {
        base.AddMethod(method);
      }
    }

    public override IEnumerable<Method> OrderedMethods()
    {
      return this.methodsToBeOnlyReadFromTheCache.Concat(base.OrderedMethods());
    }
  }

  public class ConstructorsFirstAndTheCallGraphMethodOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : CallGraphOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    where Type : IEquatable<Type>
    where Method : IEquatable<Method>
  {

    #region Private state
    private readonly List<Method> constructors;
    #endregion

    public ConstructorsFirstAndTheCallGraphMethodOrder(
      bool constructorsFirst, 
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md,
      IDecodeContracts<Local, Parameter, Method, Field, Type> cd,
      Set<string> underAnalysis,
      FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB,
      CancellationToken cancellationToken)
      : base(constructorsFirst, md, cd, underAnalysis, fieldsDB, cancellationToken)
    {
      this.constructors = new List<Method>();
    }

    protected override void AddMethod(Method method)
    {
      if (this.md.IsConstructor(method))
      {
        this.constructors.Add(method);
      }
      else
      {
        base.AddMethod(method);
      }
    }

    public override IEnumerable<Method> OrderedMethods()
    {
      return this.constructors.Concat(base.OrderedMethods());
    }
  }

}
