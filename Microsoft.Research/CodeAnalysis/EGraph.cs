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

#define ALLOW_MANIFESTATION

using System.Diagnostics;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;


namespace Microsoft.Research.CodeAnalysis
{

  public struct EGraphTerm<T> : IEquatable<EGraphTerm<T>> where T:IEquatable<T> {
    public readonly T Function;
    public readonly ESymValue[] Args;

    public EGraphTerm(T fun, params ESymValue[] args) {
      this.Function = fun;
      this.Args = args;
    }

    #region IEquatable<EGraphTerm<T>> Members

    public bool Equals(EGraphTerm<T> that)
    {
      if (!this.Function.Equals(that.Function)) return false;
      if (this.Args.Length != that.Args.Length) return false;
      for (int i = 0; i < this.Args.Length; i++) {
        if (!this.Args[i].Equals(that.Args[i])) return false;
      }
      return true;
    }

    #endregion
  }

  public interface IAbstractValueForEGraph<ADomain> : IAbstractValue<ADomain>
  {
    bool HasAllBottomFields { get; }

    /// <summary>
    /// Returns a correct Top approximation for a manifested field at a join point,
    /// where the other side has "this" abstract value
    /// For some domains, this allows recovering information, such as type information
    /// (that has to agree on branches).
    /// </summary>
    ADomain ForManifestedField();

  }

  public interface IConstantInfo
  {
    /// <summary>
    /// True if this contant should be maintained when joining bottom
    /// </summary>
    bool KeepAsBottomField { get; }
    /// <summary>
    /// True if this constant should be maintained when joining top
    /// </summary>
    bool ManifestField { get; }
  }

  public interface IEGraph<Constant, ADomain, T> : IAbstractValue<T> where ADomain : IAbstractValueForEGraph<ADomain> where Constant : IEquatable<Constant>, IConstantInfo
  {
    /// <summary>
    /// getter returns sv, such that sv == function(arg)
    /// 
    /// setter sets function(arg) == value, is equivalent to Eliminate(f, arg), followed by
    /// assume (f(arg) == value)
    /// </summary>
    /// <param name="function"></param>
    /// <param name="args"></param>
    ESymValue this[Constant function, ESymValue arg] {
      get;
      set;
    }

    ESymValue this[Constant function] {
      get;
      set;
    }

    /// <summary>
    /// returns sv, such that sv == function(arg), or null if not mapped
    /// </summary>
    ESymValue TryLookup(Constant function, ESymValue arg);

    /// <summary>
    /// returns sv, such that sv == function, or null if not mapped
    /// </summary>
    ESymValue TryLookup(Constant function);

    /// <summary>
    /// Assumes v1 == v2
    /// </summary>
    void AssumeEqual(ESymValue v1, ESymValue v2);

    /// <summary>
    /// Returns true if v1 == v2
    /// </summary>
    bool IsEqual(ESymValue v1, ESymValue v2);
    
    /// <summary>
    /// Removes the mapping from the egraph. Semantically equivalent to setting 
    /// the corresponding term to a Fresh symbolic value.
    /// </summary>
    void Eliminate(Constant function, ESymValue arg);

    /// <summary>
    /// Removes the mapping from the egraph. Semantically equivalent to setting 
    /// the corresponding term to a Fresh symbolic value.
    /// </summary>
    void Eliminate(Constant function);

    /// <summary>
    /// Removes all mappings from the egraph of the form g(from).
    /// </summary>
    void EliminateAll(ESymValue from);
    
    /// <summary>
    /// Create a fresh symbol
    /// </summary>
    /// <returns></returns>
    ESymValue FreshSymbol();

    /// <summary>
    /// Associates symval with an abstract value.
    /// 
    /// getter returns current association or Top
    /// setter sets current association (forgetting old association)
    /// </summary>
    ADomain this[ESymValue symval] { get; set; }
    
    /// <summary>
    /// Merge two EGraphs. Result is null if result is no different than this.
    /// MergeInfo provides the mapping of symbolic values in the result to values in the
    /// two incoming branches.
    /// </summary>
    T Join(T incoming, out IMergeInfo mergeInfo, bool widen);


    /// <summary>
    /// return the set of constant function symbols in this egraph
    /// </summary>
    IEnumerable<Constant> Constants { get; }

    /// <summary>
    /// return the set of unary function symbols f, such that f(symval) = sv' exists in
    /// the egraph.
    /// </summary>
    /// <param name="symval"></param>
    IEnumerable<Constant> Functions(ESymValue symval);

    /// <summary>
    /// Returns the set of defined symbolic values in the egraph that have outgoing edges.
    /// </summary>
    IEnumerable<ESymValue> SymbolicValues { get; }

    /// <summary>
    /// Return set of equivalent terms to this symbolic value
    /// </summary>
    IEnumerable<EGraphTerm<Constant>> EqTerms(ESymValue symval);

    /// <summary>
    /// Returns true if this egraph contains as much information as the given argument egraph.
    /// </summary>
    /// <returns>if true, the map out parameter contains a mapping from symbolic variables in this egraph to corresponding
    /// values in the other egraph. Equivalent (id) mappings are also present.</returns>
    bool LessEqual(T that,
                   out IFunctionalMap<ESymValue, FList<ESymValue>>/*?*/ forward,
                   out IFunctionalMap<ESymValue, ESymValue>/*?*/ backward);

  }


  public interface IMergeInfo {

    IEnumerable<Microsoft.Research.DataStructures.STuple<ESymValue, ESymValue, ESymValue>> MergeTriples { get; }

    bool IsCommon(ESymValue sv);

    /// <summary>
    /// Returns true if result is different from G1
    /// </summary>
    bool Changed { get; }

    bool IsResult<Constant, ADomain>(EGraph<Constant, ADomain> egraph)
      where Constant : IEquatable<Constant>, IConstantInfo
      where ADomain : IAbstractValueForEGraph<ADomain>, IEquatable<ADomain>;

    bool IsGraph1<Constant, ADomain>(EGraph<Constant, ADomain> egraph)
      where Constant : IEquatable<Constant>, IConstantInfo
      where ADomain : IAbstractValueForEGraph<ADomain>, IEquatable<ADomain>;

    bool IsGraph2<Constant, ADomain>(EGraph<Constant, ADomain> egraph)
      where Constant : IEquatable<Constant>, IConstantInfo
      where ADomain : IAbstractValueForEGraph<ADomain>, IEquatable<ADomain>;

    /// <summary>
    /// Returns substitution from result to G1
    /// </summary>
    IFunctionalMap<ESymValue, ESymValue> BackwardG1Map { get; }
    /// <summary>
    /// Returns substitution from result to G2
    /// </summary>
    IFunctionalMap<ESymValue, ESymValue> BackwardG2Map { get; }

    /// <summary>
    /// Returns substitution from G1 to result
    /// </summary>
    IFunctionalMap<ESymValue, FList<ESymValue>> ForwardG1Map { get; }
    /// <summary>
    /// Returns substitution from G2 to result
    /// </summary>
    IFunctionalMap<ESymValue, FList<ESymValue>> ForwardG2Map { get; }

  }

  public sealed class ESymValue : IEquatable<ESymValue>, IComparable<ESymValue>, IComparable {

    private static int globalNumber;

    public static int GetUniqueKey(ESymValue sv)
    {
      Contract.Requires(sv != null);

      return sv.GlobalId; 
    } 

    public readonly int UniqueId;
    public readonly int GlobalId;

    internal ESymValue(int id) {
      this.UniqueId = id;
      this.GlobalId = ++globalNumber;
    }


    //^ [Confined]
    public override string ToString() {
#if true
      return String.Format("sv{0} ({1})", this.UniqueId.ToString(), this.GlobalId.ToString());
#else
      return String.Format("sv{0}", this.UniqueId.ToString());
#endif

    }


    #region IEquatable<SymbolicValue> Members

    //^ [StateIndependent]
    public bool Equals(ESymValue other) {
      return this == other;
    }

    #endregion

    #region IComparable<ESymValue> Members

    public int CompareTo(ESymValue other) {
      return this.UniqueId - other.UniqueId;
    }

    #endregion

    #region IComparable Members

    public int CompareTo(object obj) {
      ESymValue that = obj as ESymValue;
      if (that == null) return 1;
      
      return this.UniqueId - that.UniqueId;
    }

    #endregion
  }

  public static class EGraphStats
  {
    #region global flags
    [ThreadStatic]
    public static bool IncrementalJoin = false;
    #endregion

    #region Statistics
    [ThreadStatic]
    internal static long Joins;
    [ThreadStatic]
    internal static long FullJoins;
    [ThreadStatic]
    internal static long IncrementalJoins;
    [ThreadStatic]
    internal static long FullJoinTicks;
    [ThreadStatic]
    internal static long IncrementalJoinTicks;

    private static float Average(long tickCount, float count)
    {
      float avg = (float)tickCount / count;
      return avg;
    }

    public static void PrintStats(ISimpleLineWriter tw)
    {
      Contract.Requires(tw != null);

      tw.WriteLine("EGraph Joins {0}", Joins);
      tw.WriteLine("  full       {0}", FullJoins);
      tw.WriteLine("  incr       {0}", IncrementalJoins);
      tw.WriteLine("Time         {0} ms", FullJoinTicks + IncrementalJoinTicks);
      tw.WriteLine("  full       {0} ms", FullJoinTicks);
      tw.WriteLine("  incr       {0} ms", IncrementalJoinTicks);
      tw.WriteLine("Avg Time     {0,3:F1} ms", Average(FullJoinTicks + IncrementalJoinTicks, Joins));
      tw.WriteLine("  full       {0,3:F1} ms", Average(FullJoinTicks, FullJoins));
      tw.WriteLine("  incr       {0,3:F1} ms", Average(IncrementalJoinTicks, IncrementalJoins));
    }
    #endregion
  }

  public class EGraph<Constant, AbstractValue> : IEGraph<Constant, AbstractValue, EGraph<Constant, AbstractValue>>
    where AbstractValue : IAbstractValueForEGraph<AbstractValue>, IEquatable<AbstractValue>
    where Constant : IEquatable<Constant>, IConstantInfo
  {
    #region Object invariant
    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.constRoot != null);
      Contract.Invariant(this.forwMap != null);
      Contract.Invariant(this.eqTermMap != null);
    }
    #endregion

    #region Privates

    int idCounter = 0;

    private static int egraphIdGen = 0;
    int egraphId;

    private ESymValue FreshSymbolicValue() {
      if (this.IsConstant)
      {
        Contract.Assume(false, "modifying a locked down egraph");
      }
      ESymValue v = new ESymValue(++idCounter);
      return v;
    }


    private readonly ESymValue constRoot;

    /// <summary>
    /// Used as a dummy node when joining graphs with allfieldbottomvalues as a placeholder for the
    /// non-represented fields.
    /// </summary>
    private readonly ESymValue bottomPlaceHolder;

    public ESymValue BottomPlaceHolder { get { return this.bottomPlaceHolder; } }

    private DoubleFunctionalMap<ESymValue, Constant, ESymValue> termMap;

    /// <summary>
    /// Used to represent multi-argument terms x = f(x1..xn). They are represented by individual
    /// edges  xi -[f,i]-> [x ,...]
    ///
    /// </summary>
    private DoubleFunctionalMap<ESymValue, MultiEdge, FList<ESymValue>> multiTermMap;

    private IFunctionalMap<ESymValue, AbstractValue> absMap;

    /// <summary>
    /// Used to represent equalities among symbolic values
    /// </summary>
    private IFunctionalMap<ESymValue, ESymValue> forwMap;

    private IFunctionalMap<ESymValue, FList<EGraphTerm<Constant>>> eqTermMap;

    /// <summary>
    /// The unique reverse map for x = f(x1..xn)
    /// </summary>
    private IFunctionalMap<ESymValue, EGraphTerm<Constant>> eqMultiTermMap;

    [Pure]
    private ESymValue Find(ESymValue v)
    {
      ESymValue result = this.forwMap[v];
      if (result == null) return v;
      return Find(result);
    }

    internal bool IsConstant { get { return this.constant; } }
    private bool constant; // once we make a copy of this, we set it to true to prevent further updates
    private readonly EGraph<Constant, AbstractValue>/*?*/ parent;
    private readonly EGraph<Constant, AbstractValue>/*?*/ root;
    private readonly int historySize;
    private readonly AbstractValue underlyingTopValue;
    private readonly AbstractValue underlyingBottomValue;

    private FList<Update>/*?*/ updates;


    /// <summary>
    /// Used to initialize the bottom egraph value
    /// </summary>
    private EGraph(AbstractValue topValue, AbstractValue bottomValue, bool dummy)
    {
      Contract.Requires(bottomValue.HasAllBottomFields);
      this.egraphId = egraphIdGen++;
      this.constRoot = FreshSymbolicValue();
      this.termMap = DoubleFunctionalMap<ESymValue, Constant, ESymValue>.Empty(ESymValue.GetUniqueKey);
      this.multiTermMap = DoubleFunctionalMap<ESymValue, MultiEdge, FList<ESymValue>>.Empty(ESymValue.GetUniqueKey);
      this.absMap = FunctionalIntKeyMap<ESymValue, AbstractValue>.Empty(ESymValue.GetUniqueKey);
      this.forwMap = FunctionalIntKeyMap<ESymValue, ESymValue>.Empty(ESymValue.GetUniqueKey);
      this.eqTermMap = FunctionalIntKeyMap<ESymValue, FList<EGraphTerm<Constant>>>.Empty(ESymValue.GetUniqueKey);
      this.eqMultiTermMap = FunctionalIntKeyMap<ESymValue, EGraphTerm<Constant>>.Empty(ESymValue.GetUniqueKey);
      this.bottomPlaceHolder = this.FreshSymbol();
      this.absMap = this.absMap.Add(this.bottomPlaceHolder, bottomValue);
      this.constant = false;
      this.parent = null;
      this.root = this;
      this.historySize = 1;
      this.updates = null;
      this.underlyingTopValue = topValue;
      this.underlyingBottomValue = bottomValue;
    }
    /// <summary>
    /// Requires: bottomValue.HasAllBottomFields
    /// </summary>
    /// <param name="topValue">top value for underlying abstract values associated with each symbolic value</param>
    /// <param name="bottomValue">bottom value for underlying abstract values associated with each symbolic value</param>
    public EGraph(AbstractValue topValue, AbstractValue bottomValue)
      : this(topValue, bottomValue, false)
    {
      // Initialize BottomValue
      if (BottomValue == null)
      {
        BottomValue = new EGraph<Constant, AbstractValue>(topValue, bottomValue, true);
      }
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="from"></param>
    private EGraph(EGraph<Constant, AbstractValue> from) {
      Contract.Requires(!from.IsBottom);
      this.egraphId = egraphIdGen++;
      this.constRoot = from.constRoot;
      this.bottomPlaceHolder = from.bottomPlaceHolder;
      this.termMap = from.termMap;
      this.multiTermMap = from.multiTermMap;
      this.idCounter = from.idCounter;
      this.absMap = from.absMap;
      this.forwMap = from.forwMap;
      this.eqTermMap = from.eqTermMap;
      this.eqMultiTermMap = from.eqMultiTermMap;
      this.underlyingTopValue = from.underlyingTopValue;
      this.underlyingBottomValue = from.underlyingBottomValue;

      // keep history
      this.updates = from.updates;
      this.parent = from;
      this.root = from.root;
      this.historySize = from.historySize + 1;

      // set from to constant
      from.LockDown();
    }

    /// <summary>
    /// Partial copy constructor. Keeps history and id's, but empties all the maps. Used when recomputing join from scratch
    /// </summary>
    private EGraph(EGraph<Constant, AbstractValue> from, bool partialDummy)
    {
      Contract.Requires(!from.IsBottom);
      this.egraphId = egraphIdGen++;
      this.constRoot = from.constRoot;
      this.bottomPlaceHolder = from.bottomPlaceHolder;
      this.idCounter = from.idCounter;
      this.underlyingTopValue = from.underlyingTopValue;
      this.underlyingBottomValue = from.underlyingBottomValue;

      // fresh maps
      this.termMap = DoubleFunctionalMap<ESymValue, Constant, ESymValue>.Empty(ESymValue.GetUniqueKey);
      this.multiTermMap = DoubleFunctionalMap<ESymValue, MultiEdge, FList<ESymValue>>.Empty(ESymValue.GetUniqueKey);
      this.absMap = FunctionalIntKeyMap<ESymValue, AbstractValue>.Empty(ESymValue.GetUniqueKey);
      this.forwMap = FunctionalIntKeyMap<ESymValue, ESymValue>.Empty(ESymValue.GetUniqueKey);
      this.eqTermMap = FunctionalIntKeyMap<ESymValue, FList<EGraphTerm<Constant>>>.Empty(ESymValue.GetUniqueKey);
      this.eqMultiTermMap = FunctionalIntKeyMap<ESymValue, EGraphTerm<Constant>>.Empty(ESymValue.GetUniqueKey);

      this.absMap = this.absMap.Add(this.bottomPlaceHolder, from.underlyingBottomValue);

      // keep history
      this.updates = from.updates;
      this.parent = from;
      this.root = from.root;
      this.historySize = from.historySize + 1;

      // set from to constant
      from.LockDown();
    }

    private int LastSymbolId {
      get {
        Contract.Requires(this.constant, "LastSymbolId only makes sense on locked down egraphs");
        return this.idCounter;
      }
    }

    private bool IsOldSymbol(ESymValue sv) {
      EGraph<Constant,AbstractValue> parent = this.parent;
      if (parent == null) return false;
      return (sv.UniqueId <= parent.LastSymbolId);
    }

    private void AddEdgeUpdate(ESymValue from, Constant function) {
      if (IsOldSymbol(from)) {
        AddUpdate(new MergeState.EdgeUpdate(from, function));
      }
    }

    private void AddMultiEdgeUpdate(ESymValue[] from, Constant function)
    {
      for (int i = 0; i < from.Length; i++)
      {
        if (!IsOldSymbol(from[i])) return;
      }
      AddUpdate(new MergeState.MultiEdgeUpdate(from, function));
    }

    private void AddAValUpdate(ESymValue sv)
    {
      if (IsOldSymbol(sv)) {
        AddUpdate(new MergeState.AValUpdate(sv));
      }
    }

    private void AddEqualityUpdate(ESymValue sv1, ESymValue sv2) {
      if (IsOldSymbol(sv1) && IsOldSymbol(sv2)) {
        AddUpdate(new MergeState.EqualityUpdate(sv1, sv2));
      }
    }

    private void AddEliminateEdgeUpdate(ESymValue from, Constant function) {
      if (IsOldSymbol(from)) {
        AddUpdate(new MergeState.EliminateEdgeUpdate(from, function));
      }
    }

    private void AddEliminateAllUpdate(ESymValue from) {
      if (IsOldSymbol(from)) {
        foreach (Constant function in this.termMap.Keys2(from)) {
          AddUpdate(new MergeState.EliminateEdgeUpdate(from, function));
        }
      }
    }

    [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-!this.IsConstant (modification of locked down egraph)")]
    private void AddUpdate(Update upd) {
      Contract.Requires(!this.IsConstant, "modification of locked down egraph");
      this.updates = this.updates.Cons(upd);
    }

    private abstract class Update {
      /// <summary>
      /// Replay update on merge state
      /// </summary>
      public abstract void Replay(MergeState merge);

      public abstract void ReplayElimination(MergeState merge);

      Update/*?*/ next; // during reversal

      public static Update Reverse(FList<Update> updates, FList<Update> common) {
        Update/*?*/ oldest = null;
        while (updates != common) {
          Update current = updates.Head;
          current.next = oldest;
          oldest = current;
          updates = updates.Tail;
        }
        return oldest;
      }

      public Update Next {
        get {
          Update next = this.next;
          //this.next = null;
          return next;
        }
      }
    }

    [ThreadStatic]
    private static bool debug;
    [ThreadStatic]
    private static bool statistics;
    
    #endregion

    #region Debugging
    public static bool DoDebug { get { return debug; } set { debug = value; statistics = true; } }
    public static bool Statistics { get { return statistics; } set { statistics = value; } }
    #endregion

    public ESymValue FreshSymbol() {
      return this.FreshSymbolicValue();
    }

    [Pure]
    public bool HasAllBottomFields(ESymValue sv)
    {
      if (sv == null) return false;
      return this[sv].HasAllBottomFields;
    }

    public AbstractValue this[ESymValue sym] {
      get {
        sym = Find(sym);
        if (this.absMap.Contains(sym)) {
          AbstractValue v = this.absMap[sym];
          return v;
        }
        return this.underlyingTopValue;
      }

      set {
        ESymValue sv = Find(sym);
        AbstractValue old = this[sym];
        if (!old.Equals(value)) {
          AddAValUpdate(sv);
          if (value.IsTop) {
            this.absMap = this.absMap.Remove(sv);
          }
          else {
            this.absMap = this.absMap.Add(sv, value);
          }
        }
      }
    }

    public ESymValue Root { get { return this.constRoot; } }

    public ESymValue LookupOrManifest(Constant function, ESymValue arg, out bool fresh)
    {
      int oldIdCount = this.idCounter;
      ESymValue result = this[function, arg];
      fresh = (oldIdCount < this.idCounter);
      return result;
    }

    public ESymValue TryLookup(Constant function, ESymValue arg) 
    {
      return LookupWithoutManifesting(arg, function);
    }

    public ESymValue TryLookup(Constant function) {
      return LookupWithoutManifesting(this.constRoot, function);
    }

    public ESymValue TryLookup(Constant function, params ESymValue[] args)
    {
      Contract.Requires(args != null);
      if (args.Length == 0)
      {
        return LookupWithoutManifesting(this.constRoot, function);
      }
      if (args.Length == 1)
      {
        return LookupWithoutManifesting(args[0], function);
      }
      return LookupWithoutManifesting(args, function);
    }

    private ESymValue LookupWithoutManifesting(ESymValue arg, Constant function)
    {
      if (arg == null) return null; // possible when manifesting during join
      arg = Find(arg);
      ESymValue v = this.termMap[arg, function];
      if (v == null) return v;
      return Find(v);
    }

    private ESymValue LookupOrBottomPlaceHolder(ESymValue arg, Constant function)
    {
      bool dummy;
      return LookupOrBottomPlaceHolder(arg, function, out dummy);
    }

    private ESymValue LookupOrBottomPlaceHolder(ESymValue arg, Constant function, out bool isPlaceholder)
    {
      var result = LookupWithoutManifesting(arg, function);
      if (result == null)
      {
        isPlaceholder = true;
        return this.bottomPlaceHolder;
      }
      isPlaceholder = false;
      return result;
    }

    [Pure]
    private ESymValue LookupWithoutManifesting(ESymValue[] args, Constant function)
    {
      Contract.Requires(args.Length > 1);

      int arity = args.Length;
      Debug.Assert(arity > 1);
      for (int i = 0; i < arity; i++)
      {
        args[i] = Find(args[i]);
      }
      // lookup first argument and reverse map and compare arguments
      var candidate = FindCandidate(args, function);
      return candidate;
    }

    private ESymValue this[ESymValue arg, Constant function] {
      get {
        arg = Find(arg);
        ESymValue v = this.termMap[arg, function];

        if (v == null) {
          v = FreshSymbolicValue();
          this.termMap = this.termMap.Add(arg, function, v);
          this.eqTermMap = this.eqTermMap.Add(v, FList<EGraphTerm<Constant>>.Cons(new EGraphTerm<Constant>(function, arg), null));
          this.AddEdgeUpdate(arg, function);
        }
        else {
          v = Find(v);
        }
        return v;
      }

      set {
        arg = Find(arg);
        value = Find(value);
        this.termMap = this.termMap.Add(arg, function, value);
        var existing = this.eqTermMap[value];
        if (existing != null && existing.Head.Function.Equals(function) && existing.Head.Args[0] == arg)
        {
          // already present.
        }
        else {
          this.eqTermMap = this.eqTermMap.Add(value, existing.Cons(new EGraphTerm<Constant>(function, arg)));
        }
        this.AddEdgeUpdate(arg, function);
      }
    }

    private ESymValue this[ESymValue[] args, Constant function]
    {
      get
      {
        Contract.Requires(args.Length > 1); // F: Added from Clousot precondition suggestion

        int arity = args.Length;
        Debug.Assert(arity > 1);
        for (int i = 0; i < arity; i++)
        {
          args[i] = Find(args[i]);
        }
        // lookup first argument and reverse map and compare arguments
        var candidate = FindCandidate(args, function);
        if (candidate != null) return candidate;
        // create fresh one
        var fresh = FreshSymbolicValue();
        for (int i = 0; i < arity; i++)
        {
          MultiEdge edge = new MultiEdge(function, i, arity);
          this.multiTermMap = this.multiTermMap.Add(args[i],edge, this.multiTermMap[args[i], edge].Cons(fresh));
        }
        this.eqMultiTermMap = this.eqMultiTermMap.Add(fresh, new EGraphTerm<Constant>(function, args));
        this.AddMultiEdgeUpdate(args, function);
        return fresh;
      }

      set
      {
        Contract.Requires(args.Length > 1); // F: Added from Clousot precondition suggestion

        int arity = args.Length;
        Debug.Assert(arity > 1);
        for (int i = 0; i < arity; i++)
        {
          args[i] = Find(args[i]);
        }
        // check if this is already present
        bool alreadyExisting = true;
        var term = this.eqMultiTermMap[value];
        if (term.Args != null)
        {
          for (int i = 0; i < arity; i++)
          {
            if (term.Args[i] != args[i])
            {
              alreadyExisting = false;
              break;
            }
          }
        }
        for (int i = 0; i < arity; i++)
        {
          MultiEdge edge = new MultiEdge(function, i, arity);
          var existing = this.multiTermMap[args[i], edge];
          if (alreadyExisting && !existing.Contains(value)) {
            // check for existing map
            alreadyExisting = false;
          }
          if (!alreadyExisting)
          {
            this.multiTermMap = this.multiTermMap.Add(args[i], edge, existing.Cons(value));
          }
        }
        if (!alreadyExisting)
        {
          this.eqMultiTermMap = this.eqMultiTermMap.Add(value, new EGraphTerm<Constant>(function, args));
          this.AddMultiEdgeUpdate(args, function);
        }
      }
    }

    private ESymValue FindCandidate(ESymValue[] args, Constant function)
    {
      int arity = args.Length;
      MultiEdge edge = new MultiEdge(function, 0, arity);
      var candidates = this.multiTermMap[args[0], edge];
      for (; candidates != null; candidates = candidates.Tail)
      {
        var term = eqMultiTermMap[candidates.Head];
        if (term.Args.Length != arity) continue;
        for (int i = 0; i < arity; i++)
        {
          if (Find(term.Args[i]) != args[i]) goto continueOuter;
        }
        return candidates.Head;
      continueOuter:
        ;
      }
      return null;
    }

    public ESymValue this[Constant function] {
      get {
        return this[this.constRoot, function];
      }
      set {
        this[this.constRoot, function] = value;
      }
    }
    public ESymValue this[Constant function, ESymValue arg] {
      get {
        return this[arg, function];
      }

      set {
        this[arg, function] = value;
      }
    }
    public ESymValue this[Constant function, params ESymValue[] args]
    {
      get
      {
        Contract.Requires(args != null);
        Contract.Requires(args.Length > 1);

        return this[args, function];
      }
    }

    public void Eliminate(Constant function, ESymValue arg) {
      ESymValue sv = Find(arg);
      var newTermMap = this.termMap.Remove(sv, function);
      if (newTermMap == this.termMap) return; // no change
      this.termMap = newTermMap;
      this.AddEliminateEdgeUpdate(sv, function);
    }

    public void Eliminate(Constant function) {
      this.termMap = this.termMap.Remove(this.constRoot, function);
      this.AddEliminateEdgeUpdate(this.constRoot, function);
    }

    public void EliminateAll(ESymValue arg) {
      ESymValue sv = Find(arg);
      this.AddEliminateAllUpdate(sv); // must be before RemoveAll, as it reads termMap
      this.termMap = this.termMap.RemoveAll(sv);
      this[arg] = underlyingTopValue;
    }

    internal struct MultiEdge : IEquatable<MultiEdge>
    {
      public readonly Constant Function;
      public readonly int Index;
      public readonly int Arity;

      public MultiEdge(Constant func, int index, int arity)
      {
        this.Function = func;
        this.Index = index;
        this.Arity = arity;
      }

      #region IEquatable<MultiArityEdge> Members

      public bool Equals(MultiEdge other)
      {
        return this.Index == other.Index && this.Arity == other.Arity && this.Function.Equals(other.Function);
      }

      public override bool Equals(object obj)
      {
        if (obj is MultiEdge)
        {
          MultiEdge that = (MultiEdge)obj;
          return Equals(that);
        }
        return false;
      }

      public override int GetHashCode()
      {
        return Arity + Index;
      }

      public override string ToString()
      {
        return String.Format("[{0}:{1}]", Function.ToString(), Index);
      }
      #endregion
    }

    private struct EqPair : IEquatable<EqPair> {
      public readonly ESymValue v1;
      public readonly ESymValue v2;

      public EqPair(ESymValue v1, ESymValue v2) {
        this.v1 = v1;
        this.v2 = v2;
      }

      public override int GetHashCode()
      {
        return GetV1HashCode() + v2.GlobalId;
      }

      private int GetV1HashCode()
      {
        if (v1 == null) return 1;
        return v1.GlobalId;
      }

      public bool Equals(EqPair that)
      {
        return this.v1 == that.v1 && this.v2 == that.v2;
      }

    }

    private void PushEquality(WorkList<EqPair> wl, ESymValue v1, ESymValue v2) {
      if (v1 == v2) return;
      wl.Add(new EqPair(v1, v2));
    }


    public void AssumeEqual(ESymValue v1, ESymValue v2) {

      WorkList<EqPair> wl = new WorkList<EqPair>();
      ESymValue v1rep = Find(v1);
      ESymValue v2rep = Find(v2);
      PushEquality(wl, v1rep, v2rep);

      if (!wl.IsEmpty) {
        // TODO: there's an opportunity for optimizing the number
        // of necessary updates that we need to record, since the induced
        // updates of the equality may end up as duplicates.
        AddEqualityUpdate(v1rep, v2rep);
      }
      DrainEqualityWorkList(wl);
    }

    private void DrainEqualityWorkList(WorkList<EqPair> wl) {
      while ( ! wl.IsEmpty ) {

        var eqpair = (EqPair)wl.Pull();
        var v1rep = Find(eqpair.v1);
        var v2rep = Find(eqpair.v2);
        if (v1rep == v2rep) continue;

        // always map new to older var
        if (v1rep.UniqueId < v2rep.UniqueId) {
          ESymValue temp = v1rep;
          v1rep = v2rep;
          v2rep = temp;
        }

        // perform congruence closure here:
        foreach(Constant f in this.Functions(v1rep)) {
          ESymValue target = this.LookupWithoutManifesting(v2rep, f);
          if (target == null) {
            this[v2rep, f] = this[v1rep,f];
          }
          else {
            PushEquality(wl, this[v1rep,f], target);
          }
        }
        AbstractValue av1 = this[v1rep];
        AbstractValue av2 = this[v2rep];
        // merge term map of v1 into v2
        foreach(EGraphTerm<Constant> eterm in this.eqTermMap[v1rep].GetEnumerable()) {
          this.eqTermMap = this.eqTermMap.Add(v2rep, this.eqTermMap[v2rep].Cons(eterm));
        }
        this.forwMap = this.forwMap.Add(v1rep, v2rep);
        this[v2rep] = av1.Meet(av2);
      }
    }


    public bool IsEqual(ESymValue v1, ESymValue v2) {
      return  (Find(v1) == Find(v2));
    }

    public IEnumerable<Constant> Constants {
      get { return this.termMap.Keys2(this.constRoot); }
    }


    public IEnumerable<Constant> Functions(ESymValue sv) {
      Contract.Ensures(Contract.Result<IEnumerable<Constant>>() != null);
      
      return this.termMap.Keys2(Find(sv));
    }

    internal IEnumerable<MultiEdge> MultiEdges(ESymValue sv)
    {
      Contract.Ensures(Contract.Result<IEnumerable<MultiEdge>>() != null);
     
      return this.multiTermMap.Keys2(Find(sv));
    }

    public IEnumerable<ESymValue> SymbolicValues
    {
      get { return this.termMap.Keys1; }
    }

    public IEnumerable<EGraphTerm<Constant>> EqTerms(ESymValue sv) {
      Contract.Ensures(Contract.Result<IEnumerable<EGraphTerm<Constant>>>() != null);
      
      foreach (EGraphTerm<Constant> eterm in this.eqTermMap[Find(sv)].GetEnumerable()) {
        // test if it is valid
        if (this.TryLookup(eterm.Function, eterm.Args) == sv) {
          yield return eterm;
        }
      }
    }

    public IEnumerable<EGraphTerm<Constant>> EqMultiTerms(ESymValue sv)
    {
      // for now this can only be 0 or 1
      var term = this.eqMultiTermMap[sv];
      if (term.Args != null)
      {
        if (IsValidMultiTerm(term))
        {
          yield return term;
        }
      }
    }


    public EGraph<Constant,AbstractValue> Clone() {
      Contract.Ensures(Contract.Result<EGraph<Constant, AbstractValue>>() != null);

      return new EGraph<Constant,AbstractValue>(this);
    }

    #region Join on EGraphs

    public EGraph<Constant, AbstractValue> Join(EGraph<Constant, AbstractValue> eg2, out bool weaker, bool widen) {
      EGraph<Constant, AbstractValue> eg1 = this;
      IMergeInfo info;
      EGraph<Constant, AbstractValue> result = eg1.Join(eg2, out info, widen);
      weaker = info.Changed;
      return result;
    }
    
    public EGraph<Constant,AbstractValue> Join(EGraph<Constant,AbstractValue> eg2, out IMergeInfo mergeInfo, bool widen) 
    {
      Contract.Ensures(Contract.ValueAtReturn(out mergeInfo) != null);
      Contract.Ensures(Contract.Result<EGraph<Constant, AbstractValue>>() != null);

      EGraphStats.Joins++;

      EGraph<Constant, AbstractValue> eg1 = this;

      int updateSize;
      EGraph<Constant,AbstractValue> common = ComputeCommonTail(eg1, eg2, out updateSize);

      bool doReplay = true;

      if (common == null) {
        doReplay = false;
      }
      if (Statistics) {
        Console.WriteLine("G1({5}):{0},{1} G2({6}):{2},{3} UpdateSize:{4}", eg1.historySize, eg1.LastSymbolId, eg2.historySize, eg2.LastSymbolId, updateSize, eg1.egraphId, eg2.egraphId);
        Console.WriteLine("  G1 Updates {0}", eg1.updates.Length());
        Console.WriteLine("  G2 Updates {0}", eg2.updates.Length());

        if (common != null)
        {
          Console.WriteLine("Common id:{0} updates {1}", common.egraphId, common.updates.Length());
        }
      }

      MergeState ms;

      // Heuristic for using Replay vs. full update
      doReplay &= (common != eg1.root);

      //doReplay &= (eg1.historySize > 3);
      //doReplay &= (eg2.historySize > 3);

      // Heuristic 1
      //doReplay &= (eg1.historySize - common.historySize < eg1.idCounter) && (eg2.historySize - common.historySize < eg2.idCounter);

      // Heuristic 2
      //doReplay &= (eg1.idCounter - common.idCounter <= common.idCounter);
      //doReplay &= (eg2.idCounter - common.idCounter <= common.idCounter);

      // Heuristic 3
      //doReplay &= (updateSize < 400);

      // Heuristic 4
      //   - the incremental list after merging is effectively tail.size + updateSize and the cost is updateSize
      //   - whereas a join will be proportional to the smaller egraph (idCounter) and cost in proportion to it
      //var incrCostSmaller = updateSize < eg1.idCounter && updateSize < eg2.idCounter;

      //var incrSizeCost = ((float)(common.idCounter + updateSize)) / Math.Min(eg1.idCounter, eg2.idCounter);
      //doReplay &= (incrSizeCost < 2.0);

      doReplay &= !widen;

      // No Incremental
      //common = eg1.root;
      //doReplay = false;

      doReplay &= EGraphStats.IncrementalJoin;

      if (DoDebug) {
        if (widen) {
          Console.WriteLine("EGraph widen");
        }
        else {
          Console.WriteLine("EGraph join");
        }
        Console.WriteLine("  Last common symbol: {0}", common.idCounter);
        if (doReplay) {
          Console.WriteLine("  Doing incremental join");
        }
        else {
          Console.WriteLine("  Doing full join");
        }
      }
      EGraph<Constant, AbstractValue> result;
      long replayTime;
      long fullTime;
      if (doReplay) {
        long startTime = Environment.TickCount;
        EGraphStats.IncrementalJoins++;
        result = new EGraph<Constant, AbstractValue>(common);
        ms = new MergeState(result, eg1, eg2, widen);
        ms.Replay(common);
        ms.Commit();
        // the incremental join computes a changed bit that is conservative (may say changed too often)
        // For widening, that is not acceptable, thus we compute a LessEqual here in that case.
        if (widen && ms.Changed) {
          ms.Changed = !result.LessEqual(eg1);
          if (DoDebug && ms.Changed)
          {
            Console.WriteLine("---EGraph changed due to LessEqual check on Widen");
          }
        }
        replayTime = Environment.TickCount - startTime;
        EGraphStats.IncrementalJoinTicks += replayTime;
      }
      else { // (!doReplay)
        long startTime = Environment.TickCount;
        EGraphStats.FullJoins++;
        result = new EGraph<Constant, AbstractValue>(common); // keep history, clear maps
        ms = new MergeState(result, eg1, eg2, widen);
        // need to eliminate things from common that were eliminated since
        if (DoDebug)
        {
          Console.WriteLine("  Start Replay Elimination: result update size now {0}", result.updates.Length());
        }
        
        // not needed if we use root.
        ms.ReplayEliminations(common); 

        ms.AddMapping(eg1.constRoot, eg2.constRoot, result.constRoot);
        if (DoDebug)
        {
          Console.WriteLine("  Start Join of roots: result update size now {0}", result.updates.Length());
        }
        ms.JoinSymbolicValue(eg1.constRoot, eg2.constRoot, result.constRoot);
        ms.Commit();
        fullTime = Environment.TickCount - startTime;
        EGraphStats.FullJoinTicks += fullTime;
      }
      // Log((fullTime < replayTime), common, eg1, eg2, widen);

      mergeInfo = ms;
      if (DoDebug)
      {
        Console.WriteLine("  Result update size {0}", result.updates.Length());
        Console.WriteLine("Done with Egraph join: changed = {0}", mergeInfo.Changed);

        Contract.Assume(this.LessEqual(result));
        Contract.Assume(eg2 != null);
        Contract.Assume(eg2.LessEqual(result));
      }
      return result;
    }

#if EGRAPH_LOG
    static TextWriter tw = new StreamWriter(@"c:\tmp\egraph.log");
    static void Log(bool fullFaster, EGraph<Constant, Label, AbstractValue> common, EGraph<Constant, Label, AbstractValue> eg1, EGraph<Constant, Label, AbstractValue> eg2, bool widen)
    {
      tw.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                    fullFaster, common.idCounter, common.historySize, eg1.idCounter, eg1.historySize, eg2.idCounter, eg2.historySize, widen);
    }
#endif

    [Pure]
    [ContractVerification(false)]
    private static EGraph<Constant,AbstractValue>/*?*/ ComputeCommonTail(EGraph<Constant,AbstractValue> g1, EGraph<Constant,AbstractValue> g2, out int updateSize) {

      EGraph<Constant,AbstractValue>/*?*/ current1 = g1;
      EGraph<Constant,AbstractValue>/*?*/ current2 = g2;

#if false
      if (g1.historySize <= 3 && g2.historySize > 100) { 
        updateSize = g1.historySize + g2.historySize;
        if (g1.root == g2.root) {
          return g1.root; 
        }
        return null;
      }
      if (g2.historySize <= 3 && g1.historySize > 100) { 
        updateSize = g1.historySize + g2.historySize;
        if (g1.root == g2.root) {
          return g1.root; 
        }
        return null;
      }
#endif

      while (current1 != current2) {
        if (current1 == null) {
          // no common tail
          current2 = null; break;
        }
        if (current2 == null) {
          // no common tail
          current1 = null; break;
        }
        if (current1.historySize > current2.historySize) {
          current1 = current1.parent; continue;
        }
        if (current2.historySize > current1.historySize) {
          current2 = current2.parent; continue;
        }
        // they have equal size
        current1 = current1.parent;
        current2 = current2.parent;
      }
      // now current1 == current2 == tail
      EGraph<Constant,AbstractValue> tail = current1;
      int tailSize = (tail != null)?tail.historySize:0;
      updateSize = g1.historySize + g2.historySize - tailSize - tailSize;
      return tail;
    }

    private class MergeState : IMergeInfo {

      public readonly EGraph<Constant, AbstractValue> Result;
      public readonly EGraph<Constant, AbstractValue> G1;
      public readonly EGraph<Constant, AbstractValue> G2;
      private DoubleFunctionalMap<ESymValue, ESymValue, ESymValue> Map;
      private OnDemandSet<ESymValue> manifested;

      /// <summary>
      /// Used for targets of multivariable functions 
      /// x0 = f(x1..xn) and y0 = f(y0..yn)
      /// Maps (x0,y0) to count of how many arg mappings (0..n) we've seen so far.
      /// When the count reaches n, we have a matching pair.
      /// </summary>
      private DoubleTable<ESymValue, ESymValue, int> PendingCounts;
      //private DoubleFunctionalMap<ESymValue, ESymValue, int> PendingCounts;

      private OnDemandSet<Microsoft.Research.DataStructures.STuple<ESymValue, ESymValue, MultiEdge>> VisitedMultiEdges;

      /// <summary>
      /// Returns true if the arity is reached.
      /// </summary>
      bool UpdatePendingCount(ESymValue xi, ESymValue yi, int arity)
      {
        int oldCount = 0;
        PendingCounts.TryGetValue(xi, yi, out oldCount);
        var newCount = oldCount + 1;
        PendingCounts[xi, yi] = newCount;
        if (newCount == arity)
        {
          return true;
        }
        return false;
      }

      /// <summary>
      /// These tuples may have a null in one of the two first args if they are a result of 
      /// materializing nodes during joins.
      /// Additionally, the list contains all triples in the Map above
      /// </summary>
      private FList<Microsoft.Research.DataStructures.STuple<ESymValue, ESymValue, ESymValue>> tuples;
      /// <summary>
      /// Used for manifest tuples (key1,null,result) that don't show up in the Map
      /// </summary>
      private IFunctionalSet<ESymValue> visitedKey1;

      private bool widen;
      private bool changed;
      public bool Changed {
        get { return this.changed; }
        set {
          this.changed = value;
        }
      }

      int lastCommonVariable;

      public bool IsCommon(ESymValue sv) {
        return (sv.UniqueId <= lastCommonVariable);
      }

      public bool AreCommon(ESymValue[] svs)
      {
        foreach (var sv in svs)
        {
          if (!IsCommon(sv)) return false;
        }
        return true;
      }

      public MergeState(EGraph<Constant, AbstractValue> result, EGraph<Constant, AbstractValue> g1, EGraph<Constant, AbstractValue> g2, bool widen)
      {
        this.Result = result;
        this.G1 = g1;
        this.G2 = g2;
        this.Map = DoubleFunctionalMap<ESymValue,ESymValue,ESymValue>.Empty(ESymValue.GetUniqueKey);
        //this.PendingCounts = DoubleFunctionalMap<ESymValue, ESymValue, int>.Empty(ESymValue.GetUniqueKey);
        this.PendingCounts = new DoubleTable<ESymValue, ESymValue, int>();
        this.visitedKey1 = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);
        this.changed = false;
        // capture the idCounter before we update the result structure.
        this.lastCommonVariable = result.idCounter;
        this.widen = widen;
      }

      /// <summary>
      /// It is possible that v1 or v2 are null. This happens when we manifest trees at join
      /// </summary>
      public void JoinSymbolicValue(ESymValue v1, ESymValue v2, ESymValue r) {
        // Contract.Requires(v1 != null);
        // Contract.Requires(v2 != null); // It can be null
        
        if (DoDebug) {
          Console.WriteLine("JoinSymbolicValue: [{0},{1}] -> {2}", v1, v2, r);
        }

        if (G2.HasAllBottomFields(v2))
        {
          // retain all fields of v1 in G1 and result is not changed
          if (v1 != null)
          {
            foreach (Constant function in G1.termMap.Keys2(v1))
            {
              bool isPlaceHolder;
              ESymValue v1target = G1.LookupWithoutManifesting(v1, function);
              ESymValue v2target = G2.LookupOrBottomPlaceHolder(v2, function, out isPlaceHolder);

              if (isPlaceHolder && !function.KeepAsBottomField) continue;

              ESymValue rtarget = AddJointEdge(v1target, v2target, function, r);

              if (rtarget != null) { JoinSymbolicValue(v1target, v2target, rtarget); }
            }
          }
        }
        else if (!widen && G1.HasAllBottomFields(v1))
        {
          // retain all fields of v2 in G2, but result graph is changed, as result is not a AllFieldsBottom symbol
          if (DoDebug)
          {
            Console.WriteLine("---EGraph changed due to an all bottom field value in G1 changing to non-bottom");
          }
          this.Changed = true;
          if (v2 != null)
          {
            foreach (Constant function in G2.termMap.Keys2(v2).AssumeNotNull())
            {
              bool isPlaceHolder;
              ESymValue v1target = G1.LookupOrBottomPlaceHolder(v1, function, out isPlaceHolder);
              ESymValue v2target = G2.LookupWithoutManifesting(v2, function);

              if (isPlaceHolder && !function.KeepAsBottomField) continue;

              ESymValue rtarget = AddJointEdge(v1target, v2target, function, r);

              if (rtarget != null) { JoinSymbolicValue(v1target, v2target, rtarget); }
            }
          }
        }
        else
        {
          IEnumerable<Constant> keys;
          bool extraManifest;
#if ALLOW_MANIFESTATION
          extraManifest = true;
#else
          extraManifest = false;
#endif
          if (!extraManifest || this.widen)
          {
            if (G1.termMap.Keys2Count(v1) <= G2.termMap.Keys2Count(v2))
            {
              keys = G1.termMap.Keys2(v1);
            }
            else
            {
              keys = G2.termMap.Keys2(v2);
              if (DoDebug)
              {
                Console.WriteLine("---EGraph changed because G2 has fewer keys for {0} than {1} in G1", v2, v1);
              }
              this.Changed = true; // since we have fewer keys in output
            }
          }
          else
          {
            // use larger key set to manifest fields: this won't guarantee that we manifest all as we would have to
            // take the union of left and write.
            if (G1.termMap.Keys2Count(v1) < G2.termMap.Keys2Count(v2))
            {
              keys = G2.termMap.Keys2(v2);
              if (DoDebug)
              {
                Console.WriteLine("---EGraph changed because G1 has fewer keys for {0} than {1} in G2", v1, v2);
              }
              this.Changed = true;
            }
            else
            {
              keys = G1.termMap.Keys2(v1);
            }
          }
          Contract.Assume(keys != null);
          foreach (Constant function in keys)
          {
            ESymValue v1target = G1.LookupWithoutManifesting(v1, function);
            ESymValue v2target = G2.LookupWithoutManifesting(v2, function);

            if (v1target == null)
            {
#if ALLOW_MANIFESTATION
              if (!this.widen && function.ManifestField)
              {
                // pretend that G1 has an unshared tree at v1,function with all top
                // mirroring the spanning tree rooted at v2,function.
                // Create such a tree in the result and map G2', nodes to the fresh
                // result nodes.

                // Result changed over G1 (not semantically, but representationally)
                if (DoDebug)
                {
                  Console.WriteLine("---EGraph changed due to manifestation of a top edge in G1");
                }
                this.Changed = true;
                // v1target == null is allowed in the remaining code
              }
              else
#endif
              {
                // no change in output over G1
                continue;
              }
            }
            if (v2target == null)
            {
              // absence considered Top.
#if ALLOW_MANIFESTATION
              if (!this.widen && function.ManifestField)
              {
                // manifest the value in G2
                // v2target == null is allowed in the remaining code
              }
              else
#endif
              {
                if (DoDebug)
                {
                  Console.WriteLine("---EGraph changed due to absence of map {0}-{1}-> in G2", v2, function.ToString());
                }
                this.Changed = true;
                continue;
              }
            }

            ESymValue rtarget = AddJointEdge(v1target, v2target, function, r);

            if (rtarget != null) { JoinSymbolicValue(v1target, v2target, rtarget); }
          }
        }

        #region Handle multi-variable function slices
        JoinMultiEdges(v1, v2);
        #endregion
      }

       private void JoinMultiEdges(ESymValue v1, ESymValue v2)
      {
        if (v1 != null && v2 != null)
        {
          // not manifesting v1 or v2
          int keysG1 = G1.multiTermMap.Keys2Count(v1);
          int keysG2 = G2.multiTermMap.Keys2Count(v2);
          IEnumerable<MultiEdge> multiKeys;
          if (keysG1 <= keysG2)
          {
            multiKeys = G1.multiTermMap.Keys2(v1);
          }
          else
          {
            multiKeys = G2.multiTermMap.Keys2(v2);
          }

          foreach (var slice in multiKeys)
          {
            JoinMultiEdge(v1, v2, slice);
          }
        }
      }

      private void JoinMultiEdge(ESymValue v1, ESymValue v2, MultiEdge slice)
      {
        // SUPER important:
        // For each v1, v2, and slice, we can only execute this once per merge or else (boom)
        // This is an issue when we perform incremental joins.
        var mark = new Microsoft.Research.DataStructures.STuple<ESymValue,ESymValue,MultiEdge>(v1,v2,slice);
        if (VisitedMultiEdges.Contains(mark)) return;
        VisitedMultiEdges.Add(mark);

        FList<ESymValue> v1candidateList = G1.multiTermMap[v1, slice];
        FList<ESymValue> v2candidateList = G2.multiTermMap[v2, slice];

        // for pair of candidates, we update the triggers
        if (v2candidateList != null)
        {
          for (var v1candidates = v1candidateList; v1candidates != null; v1candidates = v1candidates.Tail)
          {
            for (var v2candidates = v2candidateList; v2candidates != null; v2candidates = v2candidates.Tail)
            {
              var v1target = v1candidates.Head;
              var v2target = v2candidates.Head;

              if (UpdatePendingCount(v1target, v2target, slice.Arity))
              {
                // found matching functions
                var term1 = G1.eqMultiTermMap[v1target];
                var term2 = G2.eqMultiTermMap[v2target];

                if (term1.Args == null || term2.Args == null)
                {
                  // Debugger.Break();
                  break;
                }

                // it must be the case that the pairwise args are related already: find them
                Contract.Assume(term1.Args.Length > 1); // F: added as required below
                var resultArgs = new ESymValue[term1.Args.Length];
                for (int index = 0; index < resultArgs.Length; index++)
                {
                  var resultArg = Map[term1.Args[index], term2.Args[index]];
                  Contract.Assume(resultArg != null);
                  resultArgs[index] = resultArg;
                }
                var rtarget = AddJointEdge(v1target, v2target, slice.Function, resultArgs);
                if (rtarget != null) { JoinSymbolicValue(v1target, v2target, rtarget); }
              }
            }
          }
        }
      }

      private ESymValue/*?*/ AddJointEdge(ESymValue v1target, ESymValue v2target, Constant function, ESymValue[] resultRoots)
      {
        Contract.Requires(resultRoots.Length > 1); // F: Clousot does not suggest it because of some "return null"

        ESymValue rtarget = LookupMap(v1target, v2target);
        bool newBinding = false;
        if (rtarget == null)
        {
          // if we have visited v1target before, then the result graph is not isomorphic to G1
          if (VisitedKey1(v1target, v2target))
          {
            if (DoDebug)
            {
              Console.WriteLine("---Egraph changed due to pre-existing mapping in G1 of {0}", v1target);
            }
            this.Changed = true;

            if (v1target == null) return null; // manifesting
            if (v2target == null) return null; // manifesting
          }
          newBinding = true;
          if (v1target != null && v1target.UniqueId <= lastCommonVariable && v1target == v2target)
          {
            rtarget = v1target; // reuse old symbol
          }
          else
          {
            rtarget = Result.FreshSymbolicValue();
          }
          this.AddMapping(v1target, v2target, rtarget);
        }
        else
        {
          // See if info is already present
          ESymValue oldTarget = Result.LookupWithoutManifesting(resultRoots, function);
          if (oldTarget == rtarget)
          {
            // no change, don't record or change anything
            return null;
          }
        }
        Result[resultRoots, function] = rtarget;

        AbstractValue aval1 = G1AbstractValue(v1target);
        AbstractValue aval2 = G2AbstractValue(v2target);
        bool weaker;
        AbstractValue aresult = aval1.Join(aval2, out weaker, widen);
        Result[rtarget] = aresult;
        if (weaker)
        {
          if (DoDebug)
          {
            Console.WriteLine("-----EGraph changed due to join of aval of [{0},{1}] (prev {2}, new {3}, join {4})",
              v1target, v2target, aval1.ToString(), aval2.ToString(), aresult.ToString());
          }
          this.Changed = true;
        }

        if (DoDebug)
        {
          Console.WriteLine("AddJointEdge: ({0}) -{1}-> [{2},{3},{4}]",
            resultRoots.ToString(", "), Function2String(function), v1target, v2target, rtarget);
        }
        return (newBinding) ? rtarget : null;
      }

      /// <summary>
      /// Note that either v1target or v2target may be null in order to allow manifestation on joins
      /// </summary>
      private ESymValue/*?*/ AddJointEdge(ESymValue v1target, ESymValue v2target, Constant function, ESymValue resultRoot) {
        ESymValue rtarget = LookupMap(v1target, v2target);
        bool newBinding = false;
        if (rtarget == null) {
          // if we have visited v1target before, then the result graph is not isomorphic to G1
          if (VisitedKey1(v1target, v2target))
          {
            if (DoDebug)
            {
              Console.WriteLine("---Egraph changed due to pre-existing mapping in G1 of {0}", v1target);
            }
            this.Changed = true;

            if (v1target == null)
            {
              if (manifested.Contains(v2target))
              {
                return null; // can only manifest once
              }
              manifested.Add(v2target);
            }
            if (v2target == null)
            {
              if (manifested.Contains(v1target))
              {
                return null; // manifesting
              }
              manifested.Add(v1target);
            }
          }
          newBinding = true;
          if (v1target != null && v1target.UniqueId <= lastCommonVariable && v1target == v2target) {
            rtarget = v1target; // reuse old symbol
          }
          else {
            rtarget = Result.FreshSymbolicValue();
          }
          this.AddMapping(v1target, v2target, rtarget);
        }
        else {
          // See if info is already present
          ESymValue oldTarget = Result.LookupWithoutManifesting(resultRoot, function);
          if (oldTarget == rtarget) {
            // no change, don't record or change anything
            return null;
          }
        }
        Result[resultRoot, function] = rtarget;

        AbstractValue aval1 = G1AbstractValue(v1target);
        AbstractValue aval2 = G2AbstractValue(v2target);
        bool weaker;
        AbstractValue aresult =  aval1.Join(aval2, out weaker, widen);
        Result[rtarget] = aresult;
        if (weaker)
        {
          if (DoDebug)
          {
            Console.WriteLine("-----EGraph changed due to join of aval of [{0},{1}] (prev {2}, new {3}, join {4})",
              v1target, v2target, aval1.ToString(), aval2.ToString(), aresult.ToString());
          }
          this.Changed = true;
        }

        if (DoDebug) {
          Console.WriteLine("AddJointEdge: {0} -{1}-> [{2},{3},{4}]",
            resultRoot, Function2String(function), v1target, v2target, rtarget); 
        }
        return (newBinding)?rtarget:null;
      }

      private ESymValue LookupMap(ESymValue v1target, ESymValue v2target)
      {
        if (v1target == null || v2target == null) return null;
        return Map[v1target, v2target];
      }

      private AbstractValue G2AbstractValue(ESymValue v2target)
      {
        if (v2target == null) return G2.underlyingTopValue.ForManifestedField();
        return G2[v2target];
      }

      private AbstractValue G1AbstractValue(ESymValue v1target)
      {
        if (v1target == null) return G1.underlyingTopValue.ForManifestedField();
        return G1[v1target];
      }

      private bool VisitedKey1(ESymValue v1target, ESymValue v2target)
      {
        if (v1target == null)
        {
          // use visitedKey1 as a recursion breaker for v2target as well
          return visitedKey1.Contains(v2target);
        }
        return visitedKey1.Contains(v1target) || Map.ContainsKey1(v1target);
      }

      public void AddMapping(ESymValue v1, ESymValue v2, ESymValue result)
      {
        if (v2 == null)
        {
          // record the fact that we use v1
          this.visitedKey1 = this.visitedKey1.Add(v1);
        }
        else
        {
          if (v1 != null)
          {
            this.Map = this.Map.Add(v1, v2, result);
          }
          else
          {
            // add recursion breaker v2
            this.visitedKey1 = this.visitedKey1.Add(v2);
          }
        }
        this.AddTuple(v1, v2, result);
      }

      void AddTuple(ESymValue s1, ESymValue s2, ESymValue result)
      {
        this.tuples = this.tuples.Cons(new Microsoft.Research.DataStructures.STuple<ESymValue,ESymValue,ESymValue>(s1,s2,result));
      }

      public void Replay(EGraph<Constant,AbstractValue> common) {
        PrimeMapWithCommon();
        Replay(this.G1.updates, common.updates);
        Replay(this.G2.updates, common.updates);
      }

      public void ReplayEliminations(EGraph<Constant, AbstractValue> common)
      {
        ReplayEliminations(this.G1.updates, common.updates);
        ReplayEliminations(this.G2.updates, common.updates);
      }

      /// <summary>
      /// Must add to map entries for (sv,sv) -> sv for all common variables sv that are
      /// present in both G1 and G2
      /// </summary>
      private void PrimeMapWithCommon() {
        FList<ESymValue> triggers = null;
        foreach (ESymValue sv in G1.eqTermMap.Keys) {
          if (!IsCommon(sv)) continue;
          if (!(G2.eqTermMap.Contains(sv) || G2.eqMultiTermMap.Contains(sv))) continue;
          // common one
          if (G1.multiTermMap.ContainsKey1(sv)) triggers = triggers.Cons(sv);
          this.AddMapping(sv, sv, sv);
        }
        foreach (var sv in G1.eqMultiTermMap.Keys) {
          if (!IsCommon(sv)) continue;
          if (!(G2.eqTermMap.Contains(sv) || G2.eqMultiTermMap.Contains(sv))) continue;
          // common one
          if (this.Map[sv, sv] != null) continue;
          if (G1.multiTermMap.ContainsKey1(sv)) triggers = triggers.Cons(sv);
          this.AddMapping(sv, sv, sv);
        }
        while (triggers != null)
        {
          var trigger = triggers.Head;
          triggers = triggers.Tail;
          foreach (var slice in G1.multiTermMap.Keys2(trigger))
          {
            JoinMultiEdge(trigger, trigger, slice);
          }
        }
      }

      private void Replay(FList<Update> updates, FList<Update> common) {
        // First reverse updates.
        Update oldest = Update.Reverse(updates, common);
        while (oldest != null) {
          oldest.Replay(this);
          oldest = oldest.Next;
        }
      }

      private void ReplayEliminations(FList<Update> updates, FList<Update> common)
      {
        // First reverse updates.
        Update oldest = Update.Reverse(updates, common);
        while (oldest != null) {
          oldest.ReplayElimination(this);
          oldest = oldest.Next;
        }
      }


      #region Updates

      public class EdgeUpdate : Update {
        readonly ESymValue from;
        readonly Constant function;

        public EdgeUpdate(ESymValue from, Constant function) {
          this.from = from;
          this.function = function;
        }

        public override void ReplayElimination(MergeState merge)
        {
          // nothing to do
          return;
        }

        /// <summary>
        /// This replay actually throws away information in the following case.
        /// If from is not common, but is related to another
        /// variable in G2. This can happen as follows:
        /// Both graphs perform an edge update from a common variable c to b1 (b2) respectively. 
        /// These edge updates will be properly
        /// captured in this replay and b1 will be related to b2, mapping to b3 in the new graph.
        /// If both graphs further contain an edge update b1 (b2) -> c1 (c2), then the replay code below will throw it 
        /// away, even though it would make sense to map b3 -> c3 in the new graph, relating c1,c2 -> c3.
        /// </summary>
        public override void Replay(MergeState merge) {
          if (!merge.IsCommon(from)) return;

          ESymValue v1target = merge.G1.LookupWithoutManifesting(from,function);
          ESymValue v2target = merge.G2.LookupWithoutManifesting(from,function);

          if (DoDebug) {
            Console.WriteLine("Replay edge update: {0} -{3}-> [ {1}, {2} ]", from.ToString(), v1target, v2target, function);
          }

          if (v1target == null) {
            // not in G1
            if (function.KeepAsBottomField && merge.G1.HasAllBottomFields(from))
            {
              // allowed to maintain this edge
              v1target = merge.G1.bottomPlaceHolder;
            }
#if ALLOW_MANIFESTATION
            // can we manifest it?
            else if (v2target != null && !merge.widen && function.ManifestField)
            {
              // manifest in G1
              // Result changed over G1 (not semantically, but representationally)
              if (DoDebug)
              {
                Console.WriteLine("---EGraph changed due to manifestation of a top edge in G1");
              }
              merge.Changed = true;

              // leave v1target == null and let it be manifested in AddJointEdge
            }
            else
#endif
            {
              // no longer in G1
              return;
            }
          }
          if (v2target == null) {
            if (function.KeepAsBottomField && merge.G2.HasAllBottomFields(from))
            {
              // allowed to maintain this edge
              v2target = merge.G2.bottomPlaceHolder;
            }
#if ALLOW_MANIFESTATION
            else if (!merge.widen && function.ManifestField)
            {
              // manifest in G2
              // leave v2target == null and let it be manifested in AddJointEdge
            }
#endif
            else
            {
              // no longer in G2
              if (DoDebug)
              {
                Console.WriteLine("---EGraph changed during EdgeUpdate due to missing target in G2");
              }
              merge.Changed = true; // no longer in result.
              return;
            }
          }
        
          ESymValue rtarget = merge.AddJointEdge(v1target, v2target, function, from);

          if (rtarget != null && rtarget.UniqueId > merge.lastCommonVariable) {
            merge.JoinSymbolicValue(v1target, v2target, rtarget); 
          }
        }
      }

      public class MultiEdgeUpdate : Update
      {
        readonly ESymValue[] from;
        readonly Constant function;

        public MultiEdgeUpdate(ESymValue[] from, Constant function)
        {
          this.from = from;
          this.function = function;
        }

        public override void ReplayElimination(MergeState merge)
        {
          // nothing to do
          return;
        }

        /// <summary>
        /// </summary>
        public override void Replay(MergeState merge)
        {
          int arity = from.Length;
          for (int i = 0; i < arity; i++)
          {
            var arg = from[i];
            if (!merge.IsCommon(arg)) continue;

            merge.JoinMultiEdge(arg, arg, new MultiEdge(function, i, arity));
          }
        }
      }

      public class AValUpdate : Update
      {
        readonly ESymValue sv;

        public AValUpdate(ESymValue sv) {
          this.sv = sv;
        }

        public override void ReplayElimination(MergeState merge)
        {
          if (!merge.IsCommon(this.sv)) return;
          AbstractValue av1 = merge.G1[this.sv];
          if (av1.IsTop) {
            merge.Result[this.sv] = av1;
            return;
          }
          AbstractValue av2 = merge.G2[this.sv];
          if (av2.IsTop) {
            merge.Result[this.sv] = av2;
          }
          return;
        }

        public override void Replay(MergeState merge) {
          if (!merge.IsCommon(this.sv)) return;

          AbstractValue av1 = merge.G1[this.sv];
          AbstractValue av2 = merge.G2[this.sv];

          AbstractValue old = merge.Result[this.sv];

          bool weaker;
          AbstractValue join = av1.Join(av2, out weaker, merge.widen);
          if (weaker)
          {
            if (DoDebug)
            {
              Console.WriteLine("----EGraph changed during AValUpdate of {3} due to weaker aval join (prev {0}, new {1}, result {2})", av1.ToString(), av2.ToString(), join.ToString(), this.sv.ToString());
            }
            merge.Changed = true;
          }

          if (!join.Equals(old)) {
            merge.Result[this.sv] = join;
          }
        }
      }

      public class EqualityUpdate : Update {
        readonly ESymValue sv1;
        readonly ESymValue sv2;

        public EqualityUpdate(ESymValue sv1, ESymValue sv2) {
          this.sv1 = sv1;
          this.sv2 = sv2;
        }

        public override void ReplayElimination(MergeState merge)
        {
          return; // nothing to do
        }

        public override void Replay(MergeState merge) {
          if (!merge.IsCommon(this.sv1)) return;
          if (!merge.IsCommon(this.sv2)) return;

          if (merge.G1.IsEqual(this.sv1, this.sv2)) {
            if (merge.Result.IsEqual(this.sv1, this.sv2)) {
              // already present
              return;
            }
            if (merge.G2.IsEqual(this.sv1, this.sv2)) {
              // add equality
              merge.Result.AssumeEqual(this.sv1, this.sv2);
            }
            else {
              // Changed vs G1 (since not present in output)
              if (DoDebug) {
                Console.WriteLine("---Egraph changed during EqualityUpdate, as equality ({0},{1}) is missing in new", this.sv1.ToString(), this.sv2.ToString());
              }
              merge.Changed = true;
            }
          }
        }
      }

      public class EliminateEdgeUpdate : Update {
        readonly ESymValue from;
        readonly Constant function;

        public EliminateEdgeUpdate(ESymValue from, Constant function) {
          this.from = from;
          this.function = function;
        }

        public override void ReplayElimination(MergeState merge)
        {
          if (!merge.IsCommon(this.from)) return;
          merge.Result.Eliminate(this.function, this.from);
        }

        public override void Replay(MergeState merge) {
          if (!merge.IsCommon(this.from)) return;
          ESymValue v1target = merge.G1.LookupWithoutManifesting(this.from, this.function);
          ESymValue v2target = merge.G2.LookupWithoutManifesting(this.from, this.function);

          if (v1target != null && v2target != null) { 
            // outdated
            return;
          }

          if (v1target != null) {
            if (DoDebug) {
              Console.WriteLine("---Egraph changed due to EliminateEdgeUpdate {0}-{1}-> that is only in G2", this.from.ToString(), this.function.ToString());
            }
            merge.Changed = true;
          }
          ESymValue rtarget = merge.Result.LookupWithoutManifesting(this.from, this.function);
          if (rtarget == null) {
            // redundant
            return;
          }
          merge.Result.Eliminate(this.function, this.from);
        }
      }

      #endregion

      #region IMergeInfo members

      public IEnumerable<Microsoft.Research.DataStructures.STuple<ESymValue, ESymValue, ESymValue>> MergeTriples
      {
        get
        {
#if false
          foreach (ESymValue s1 in this.Map.Keys1)
          {
            foreach (ESymValue s2 in this.Map.Keys2(s1))
            {
              yield return new Tuple<ESymValue, ESymValue, ESymValue>(s1, s2, this.Map[s1, s2]);
            }
          }
#endif
          var tups = this.tuples;
          while (tups != null)
          {
            yield return tups.Head;
            tups = tups.Tail;
          }
        }
      }

      public bool IsResult<C, ADomain>(EGraph<C, ADomain> egraph)
        where C : IEquatable<C>, IConstantInfo
        where ADomain : IAbstractValueForEGraph<ADomain>, IEquatable<ADomain>
      {
        return (object)egraph == this.Result;
      }

      public bool IsGraph1<C, ADomain>(EGraph<C, ADomain> egraph)
        where C : IEquatable<C>, IConstantInfo
        where ADomain : IAbstractValueForEGraph<ADomain>, IEquatable<ADomain>
      {
        if ((object)egraph == this.G1) return true;
        if (this.G1.parent == (object)egraph && this.G1.updates == (object)egraph.updates) return true;
        return false;
      }

      public bool IsGraph2<C, ADomain>(EGraph<C, ADomain> egraph)
        where C : IEquatable<C>, IConstantInfo
        where ADomain : IAbstractValueForEGraph<ADomain>, IEquatable<ADomain>
      {
        if ((object)egraph == this.G2) return true;
        if (this.G2.parent == (object)egraph && this.G2.updates == (object)egraph.updates) return true;
        return false;
      }

      public IFunctionalMap<ESymValue, ESymValue> BackwardG1Map
      {
        get { throw new NotImplementedException(); }
      }

      public IFunctionalMap<ESymValue, ESymValue> BackwardG2Map
      {
        get { throw new NotImplementedException(); }
      }

      public IFunctionalMap<ESymValue, FList<ESymValue>> ForwardG1Map
      {
        get
        {
          IFunctionalMap<ESymValue, FList<ESymValue>> forward = FunctionalIntKeyMap<ESymValue, FList<ESymValue>>.Empty(ESymValue.GetUniqueKey);
          for (var tups = this.tuples; tups != null; tups = tups.Tail)
          {
            var tuple = tups.Head;
            var key1 = tuple.One;
            if (key1 != null)
            {
              forward = forward.Add(key1, forward[key1].Cons(tuple.Three));
            }
          }
          return forward;
        }
      }

      public IFunctionalMap<ESymValue, FList<ESymValue>> ForwardG2Map
      {
        get
        {
          IFunctionalMap<ESymValue, FList<ESymValue>> forward = FunctionalIntKeyMap<ESymValue, FList<ESymValue>>.Empty(ESymValue.GetUniqueKey);
          for (var tups = this.tuples; tups != null; tups = tups.Tail)
          {
            var tuple = tups.Head;
            var key2 = tuple.Two;
            if (key2 != null)
            {
              forward = forward.Add(key2, forward[key2].Cons(tuple.Three));
            }
          }
          return forward;
        }        
      }

      #endregion

      /// <summary>
      /// Performs additional check to see if Result == G1 for multiargument functions
      /// </summary>
      internal void Commit()
      {
        // we can only make Changed more true in this code.
        if (this.Changed) return;

        // check for each "valid" term in G1 whether we have the corresponding term in G2.
        // NOTE that some valid terms are actually unreachable and we need to consider that
        // as well.
        //
        foreach (var pair in this.G1.ValidMultiTerms)
        {
          var term = pair.Two;
          Contract.Assume(term.Args.Length > 1); // F: Added because of the precondition below
          ESymValue[] resultArgs = new ESymValue[term.Args.Length];
          Contract.Assert(term.Args.Length > 1, "Help Clousot");
          for (int i = 0; i < resultArgs.Length; i++)
          {
            var key1 = term.Args[i];
            if (!this.VisitedKey1(key1, null))
            {
              // key1 not reached. There are 2 cases:
              // 1) not reachable via normal edges in G1. Thus garbage.
              // 2) reachable via a multi-edge. In this case, there is another multiterm that we will
              //    find.
              // In either case, we can disregard this term.
              goto ContinueOuter;
            }
            int count = 0;
            foreach (var key2 in this.Map.Keys2(key1))
            {
              count++;
              if (count != 1) { goto Changed; }
              resultArgs[i] = Map[key1, key2];
              if (resultArgs[i] == null) { goto Changed; }
            }
            if (count != 1) { goto Changed; }
          }
          {
            var result = Result.LookupWithoutManifesting(resultArgs, term.Function);
            if (result == null) { goto Changed; }
            var target1 = pair.One;
            int count = 0;
            foreach (var target2 in this.Map.Keys2(target1).AssumeNotNull())
            {
              count++;
              if (count != 1) { goto Changed; }
              var target = Map[target1, target2];
              if (target != result) { goto Changed; }
            }
            if (count != 1) { goto Changed; }
          }
        ContinueOuter: ;

        }
        return; // ok
      Changed:
        this.Changed = true;
        return; 
      }
    }  


    #endregion



    [Pure]
    public void Dump(TextWriter tw) 
    {
      if(tw == null)
      {
        return;
      }

      var seen = new Set<ESymValue>();
      var wl = new WorkList<ESymValue>();
      var triggers = FunctionalIntKeyMap<ESymValue, int>.Empty(ESymValue.GetUniqueKey);
    
      Console.WriteLine("EGraphId:{0}", this.egraphId);
      Console.WriteLine("LastSymbolId:{0}", this.idCounter);
      foreach (Constant function in this.termMap.Keys2(this.constRoot))
      {
        ESymValue target = this[this.constRoot, function];

        tw.WriteLine("{0} = {1}", Function2String(function), target);

        wl.Add(target);
      }

      while (!wl.IsEmpty)
      {
        ESymValue v = wl.Pull();
        if (!seen.AddQ(v)) continue;

        foreach (var function in this.termMap.Keys2(v).AssumeNotNull())
        {
          ESymValue target = this[v, function];
          tw.WriteLine("{0}({2}) = {1}", Function2String(function), target, v);

          wl.Add(target);
        }
        foreach (var multiEdge in this.multiTermMap.Keys2(v))
        {
          for (var candidates = this.multiTermMap[v, multiEdge]; candidates != null; candidates = candidates.Tail)
          {
            if (UpdateTrigger(candidates.Head, multiEdge, ref triggers))
            {
              var target = candidates.Head;
              var mterm = this.eqMultiTermMap[target];
              if (mterm.Args != null)
              {
                // should always be valid, but let's not crash
                tw.Write("{0}(", Function2String(multiEdge.Function));
                for (int i = 0; i < mterm.Args.Length; i++)
                {
                  if (i > 0) { tw.Write(", "); }
                  tw.Write("{0}", mterm.Args[i]);
                }
                tw.WriteLine(") = {0}", target);
                wl.Add(target);
              }
            }
          }
        }
      }
      tw.WriteLine("**Abstract value map");
      foreach (ESymValue v in seen) {
        AbstractValue aval = this[v];
        if (!aval.IsTop) {
          tw.WriteLine("{0} -> {1}", v, aval);
      }
      }
    }

    IEnumerable<Pair<ESymValue, EGraphTerm<Constant>>> ValidMultiTerms
    {
      get
      {
        foreach (var key in this.eqMultiTermMap.Keys)
        {
          var term = this.eqMultiTermMap[key];
          if (IsValidMultiTerm(term))
          {
            yield return new Pair<ESymValue,EGraphTerm<Constant>>(key, term);
          }
        }        
      }
    }

    bool IsValidMultiTerm(EGraphTerm<Constant> term)
    {
      Contract.Requires(term.Args.Length > 1); // F: Added precondition suggested by Clousot

      return (this.LookupWithoutManifesting(term.Args, term.Function) != null);
    }

    public static string Function2String(Constant function) 
    {
      return function.ToString();
    }

    #region IAbstractValueDomain<EGraph<Constant,Label,AbstractValue>> Members

    public EGraph<Constant, AbstractValue> Top {
      get { return new EGraph<Constant, AbstractValue>(this.underlyingTopValue, this.underlyingBottomValue); }
    }

    [ThreadStatic] // Because it depends on the analysis driver
    private static EGraph<Constant, AbstractValue> BottomValue;

    public EGraph<Constant, AbstractValue> Bottom {
      get {
        if (BottomValue == null) {
          BottomValue = new EGraph<Constant, AbstractValue>(this.underlyingTopValue, this.underlyingBottomValue);
          BottomValue.LockDown();
        }
        return BottomValue;
      }
    }

    public bool IsTop {
      get {
        return this.termMap.Keys2Count(this.constRoot) == 0;
      }
    }

    public bool IsBottom {
      get {
        return this == BottomValue;
      }
    }

    public EGraph<Constant, AbstractValue> ImmutableVersion() {
      LockDown();
      
      return this;
    }

    private void LockDown()
    {
      this.constant = true; // prevents future mods
    }

    public EGraph<Constant, AbstractValue> Meet(EGraph<Constant, AbstractValue> that) {
      Contract.Ensures(Contract.Result<EGraph<Constant, AbstractValue>>() != null);
      
      if (this == that) return this;

      if (this.IsBottom) return this;
      if (that.IsBottom) return that;
      if (this.IsTop) return that;
      if (that.IsTop) return this;
      return this; // not very smart
    }

    #endregion

    #region IEGraph<Constant,Label,AbstractValue,EGraph<Constant,Label,AbstractValue>> Members

    static bool VisitedBefore(ESymValue s2,
      IFunctionalSet<ESymValue> backwardManifested,
      IFunctionalMap<ESymValue, ESymValue> backward, out ESymValue s1)
    {
      s1 = backward[s2];
      if (s1 == null)
      {
        return backwardManifested.Contains(s2);
      }
      return true;
    }


    /// <summary>
    /// Null maps signify identity mappings
    /// </summary>
    public bool LessEqual(EGraph<Constant, AbstractValue> that,
                          out IFunctionalMap<ESymValue, FList<ESymValue>>/*?*/ forwardMap,
                          out IFunctionalMap<ESymValue, ESymValue>/*?*/ backwardMap
                         )
    {
      if (this.IsSameEGraph(that))
      {
        forwardMap = null;
        backwardMap = null;
        return true;
        //return this.LessEqualIdentityMaps(that, out forwardMap, out backwardMap);
      }

      return InternalLessEqual(this, that, out forwardMap, out backwardMap);
    }

    private static bool InternalLessEqual(EGraph<Constant,AbstractValue> thisG, EGraph<Constant, AbstractValue> that, out IFunctionalMap<ESymValue, FList<ESymValue>> forwardMap, out IFunctionalMap<ESymValue, ESymValue> backwardMap)
    {
      int dummy;
      var common = ComputeCommonTail(thisG, that, out dummy);

      // we need to manifest things in thisG to check properly. Make sure we have a copy or a mutable graph
      if (thisG.IsConstant) { thisG = thisG.Clone(); }

      ContractHelpers.AssumeInvariant(thisG);

      WorkList<EqPair> workList = new WorkList<EqPair>();
      workList.Add(new EqPair(thisG.constRoot, that.constRoot));

      // The backward graph is our evidence. We have to compute mappings even for manifested values, but not include them
      // in the result. Thus, we use an extra set for those.
      IFunctionalSet<ESymValue> backwardManifested = FunctionalSet<ESymValue>.Empty(ESymValue.GetUniqueKey);
      IFunctionalMap<ESymValue, ESymValue> backward = FunctionalIntKeyMap<ESymValue, ESymValue>.Empty(ESymValue.GetUniqueKey);
      IFunctionalMap<ESymValue, FList<ESymValue>> forward = FunctionalIntKeyMap<ESymValue, FList<ESymValue>>.Empty(ESymValue.GetUniqueKey);
      IFunctionalMap<ESymValue, int> triggers = FunctionalIntKeyMap<ESymValue, int>.Empty(ESymValue.GetUniqueKey);

      if (debug)
      {
        Console.WriteLine("-----LessEqual");
        Console.WriteLine("-------this--");
        thisG.Dump(Console.Out);
        Console.WriteLine("-------that--");
        that.Dump(Console.Out);
      }
      while (!workList.IsEmpty)
      {
        EqPair pair = workList.Pull();
        ESymValue source1 = pair.v1;
        ESymValue source2 = pair.v2;
        if (debug)
        {
          Console.WriteLine(" Considering {0} <= {1}", source1, source2);
        }
        ESymValue corresponding;
        if (VisitedBefore(source2, backwardManifested, backward, out corresponding))
        {
          // visited source2 before. Makesure corresponding is equal to source1 && source1 is not null.
          if (corresponding != null)
          {
            if (corresponding == source1) continue; // already in relation
          }
          // graphs don't match
          if (debug)
          {
            Console.WriteLine("---LessEqual fails due to pre-existing relation: {0} <- {1}", corresponding, source2);
          }
          forwardMap = null;
          backwardMap = null;
          return false;
        }

        // check abstract value implication
        AbstractValue a1 = (source1 == null) ? thisG.underlyingTopValue.ForManifestedField() : thisG[source1];
        AbstractValue a2 = that[source2];

        if (!a1.LessEqual(a2))
        {
          // graphs differ
          if (debug)
          {
            Console.WriteLine("---LessEqual fails due to abstract values: !({0} <= {1})", a1, a2);
          }
          forwardMap = null;
          backwardMap = null;
          return false;
        }

        // add to relation
        if (source1 != null)
        {
          backward = backward.Add(source2, source1);
          // add inverse relation
          forward = forward.Add(source1, forward[source1].Cons(source2));
        }
        else
        {
          backwardManifested = backwardManifested.Add(source2);
        }
        // before relating the fields, check whether source1 is a bottom field value
        if (!thisG.HasAllBottomFields(source1))
        {
          if (that.HasAllBottomFields(source2))
          {
            // means source1 does not have bottom fields, but source2 has, meaning
            // the relation does not hold.
            if (debug)
            {
              Console.WriteLine("---LessEqual fails due to bottom field difference");
            }
            forwardMap = null;
            backwardMap = null;
            return false;
          }
          foreach (Constant c in that.Functions(source2))
          {
            // ESymValue target1 = thisG.TryLookup(c, source1);
            ESymValue target1 = thisG[c, source1]; // lookup or manifest
            ESymValue target2 = that[c, source2]; // must be present
            if (debug)
            {
              Console.WriteLine("    {0}-{1}->{2} <=? {3}-{4}->{5}", source1, c, target1, source2, c, target2);
            }
            workList.Add(new EqPair(target1, target2));
          }
          foreach (MultiEdge e in that.MultiEdges(source2))
          {
            for (var candidates = that.multiTermMap[source2, e]; candidates != null; candidates = candidates.Tail)
            {
              var target2 = candidates.Head;
              if (UpdateTrigger(target2, e, ref triggers))
              {
                var term2 = that.eqMultiTermMap[target2];
                Contract.Assume(term2.Args != null);
                // map args to this
                Contract.Assume(term2.Args.Length > 1); // F: Added because of precondition of LookUpWithoutManifesting below
                ESymValue[] thisArgs = new ESymValue[term2.Args.Length];
                for (int i = 0; i < thisArgs.Length; i++)
                {
                  thisArgs[i] = backward[term2.Args[i]];
                  Contract.Assume(thisArgs[i] != null);
                }
                // lookup term in this
                var target1 = thisG.LookupWithoutManifesting(thisArgs, e.Function);
                if (target1 == null)
                {
                  // term does not exist in this graph, so LessEqual does not hold.
                  if (debug)
                  {
                    Console.WriteLine("---LessEqual fails due to missing multi term {0}({1})", e.Function, term2.Args.ToString(", "));
                  }
                  forwardMap = null;
                  backwardMap = null;
                  return false;
                }
                // add target1,target2 to work list
                workList.Add(new EqPair(target1, target2));
              }
            }
          }
        }
      }
      // made it here means that all edges in that graph are also in this graph.
      forwardMap = forward;

      Contract.Assume(common != null, "Making the assumption explicit");

      backwardMap = CompleteWithCommon(backward, thisG, that, common.idCounter);
      return true;
    }

    private static IFunctionalMap<ESymValue,ESymValue> CompleteWithCommon(IFunctionalMap<ESymValue,ESymValue> map, EGraph<Constant, AbstractValue> G1, EGraph<Constant, AbstractValue> G2, int lastCommonId)
    {
      Contract.Requires(map != null);

      foreach (ESymValue sv in G1.eqTermMap.Keys)
      {
        if (!IsCommon(sv, lastCommonId)) continue;
        if (map.Contains(sv)) continue;
        map = map.Add(sv, sv);
      }
      foreach (var sv in G1.eqMultiTermMap.Keys)
      {
        if (!IsCommon(sv, lastCommonId)) continue;
        if (map.Contains(sv)) continue;
        map = map.Add(sv, sv);
      }
      return map;
    }

    private static bool IsCommon(ESymValue sv, int lastCommonId)
    {
      return sv.UniqueId <= lastCommonId;
    }

    private static bool UpdateTrigger(ESymValue source2, MultiEdge e, ref IFunctionalMap<ESymValue, int> triggers)
    {
      Contract.Ensures(triggers != null);

      var newCount = triggers[source2] + 1;
      triggers = triggers.Add(source2, newCount);

      Contract.Assert(triggers != null);

      if (newCount == e.Arity)
      {
        return true;
      }
      return false;
    }

    private int EqTermCount(ESymValue target)
    {
      int count = 0;
      foreach (EGraphTerm<Constant> eterm in this.eqTermMap[Find(target)].GetEnumerable())
      {
        // test if it is valid
        if (this.TryLookup(eterm.Function, eterm.Args) == target)
        {
          count++;
        }
      }
      return count;
    }

    private bool IsSameEGraph(EGraph<Constant, AbstractValue> that)
    {
      if (this == that) return true;
      return (that.parent == this && that.updates == this.updates);
    }

    private bool LessEqualIdentityMaps(EGraph<Constant, AbstractValue> that, out IFunctionalMap<ESymValue, FList<ESymValue>> forwardMap, out IFunctionalMap<ESymValue, ESymValue> backwardMap)
    {
      IFunctionalMap<ESymValue, ESymValue> backward = FunctionalIntKeyMap<ESymValue, ESymValue>.Empty(ESymValue.GetUniqueKey);
      IFunctionalMap<ESymValue, FList<ESymValue>> forward = FunctionalIntKeyMap<ESymValue, FList<ESymValue>>.Empty(ESymValue.GetUniqueKey);

      foreach (ESymValue sv in this.eqTermMap.Keys.Union(this.eqMultiTermMap.Keys))
      {
        backward = backward.Add(sv, sv);
        forward = forward.Add(sv, FList<ESymValue>.Cons(sv, null));
      }
      backwardMap = backward;
      forwardMap = forward;
      return true;
    }

    public IFunctionalMap<ESymValue, FList<ESymValue>> GetForwardIdentityMap()
    {
      IFunctionalMap<ESymValue, FList<ESymValue>> forward = FunctionalIntKeyMap<ESymValue, FList<ESymValue>>.Empty(ESymValue.GetUniqueKey);

      foreach (ESymValue sv in this.eqTermMap.Keys.Union(this.eqMultiTermMap.Keys))
      {
        forward = forward.Add(sv, FList<ESymValue>.Cons(sv, null));
      }
      return forward;
    }
    
    #endregion

    #region IAbstractValue<EGraph<Constant,Label,AbstractValue>> Members


    public bool LessEqual(EGraph<Constant, AbstractValue> that) {
      IFunctionalMap<ESymValue, FList<ESymValue>>/*?*/ forward;
      IFunctionalMap<ESymValue, ESymValue>/*?*/ backward;
      return this.LessEqual(that, out forward, out backward);
    }

    #endregion

    internal bool IsValidSymbolc(ESymValue sv)
    {
      return (this.eqTermMap.Contains(sv));
    }
  }
}
