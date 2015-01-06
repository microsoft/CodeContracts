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


namespace Microsoft.Research.DataStructures {

  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.IO;
  using System.Linq;
  using System.Text;

  public static class Pair
  {
    public static Pair<L, R> For<L, R>(L l, R r) { return new Pair<L, R>(l, r); }

    public static L One<L, R>(Pair<L, R> p) { return p.One; }
    public static R Two<L, R>(Pair<L, R> p) { return p.Two; }
    public static bool NotTwo<L>(Pair<L, bool> p) { return !p.Two; }
  }

  /// <summary>
  /// Pair value
  /// </summary>
  /// <typeparam name="L">first component type</typeparam>
  /// <typeparam name="R">second component type</typeparam>
  [Serializable]
  public struct Pair<L, R> : IEquatable<Pair<L,R>> {
    /// <summary>
    /// First component of pair
    /// </summary>
    public readonly L One;

    /// <summary>
    /// Second component of pair
    /// </summary>
    public readonly R Two;

    /// <summary>
    /// These flags are used to avoid boxing in GetHashCode and ToString
    /// </summary>
    private static readonly bool LIsReferenceType = (object)default(L) == null; // Thread-safe if L is thread-safe
    private static readonly bool RIsReferenceType = (object)default(R) == null; // Thread-safe if R is thread-safe

    /// <summary>
    /// Pair constructor
    /// </summary>
    /// <param name="first">first component</param>
    /// <param name="second">second component</param>
    public Pair(L first, R second) {
      this.One = first;
      this.Two = second;
    }

    public override int GetHashCode()
    {
      var hc1 = (LIsReferenceType && One == null) ? 0 : One.GetHashCode();
      var hc2 = (RIsReferenceType && Two == null) ? 0 : Two.GetHashCode();

      return hc1 + hc2;
    }

    public override string ToString() {
      string oneStr = (LIsReferenceType && One == null) ? "<null>" : One.ToString();
      string twoStr = (RIsReferenceType && Two == null) ? "<null>" : Two.ToString();
      return String.Format("({0},{1})", oneStr, twoStr);
    }

    #region IEquatable<Pair<L,R>> Members

    //^ [StateIndependent]
    public bool Equals(Pair<L, R> other) {
      bool result = (this.One is IEquatable<L>) ? (((IEquatable<L>)this.One).Equals(other.One)) : Object.Equals(this.One, other.One);
      if (!result) return result;

      return (this.Two is IEquatable<R>) ? (((IEquatable<R>)this.Two).Equals(other.Two)) : Object.Equals(this.Two, other.Two);
    }

    #endregion
  }

  /// <summary>
  /// Pair value whose components are non-null
  /// </summary>
  /// <typeparam name="L">first component type</typeparam>
  /// <typeparam name="R">second component type</typeparam>
  public struct PairNonNull<L, R> 
    : IEquatable<Pair<L, R>>
    where L : class
    where R : class
  {
    #region object invariant
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(One != null);
      Contract.Invariant(Two != null);
    }

    #endregion

    /// <summary>
    /// First component of pair
    /// </summary>
    private L one;
    public L One
    {
      get
      {
        Contract.Ensures(Contract.Result<L>() != null);

        return this.one;
      }
    }

    /// <summary>
    /// Second component of pair
    /// </summary>
    private R two;
    public R Two
    {
      get
      {
        Contract.Ensures(Contract.Result<R>() != null);

        return this.two;
      }
    }

    /// <summary>
    /// Pair constructor
    /// </summary>
    /// <param name="first">first component - should be != null</param>
    /// <param name="second">second component - shoule be != null</param>
    public PairNonNull(L first, R second)
    {
      Contract.Requires(first != null);
      Contract.Requires(second != null);

      this.one = first;
      this.two = second;
    }

    public override int GetHashCode()
    {
      var hc1 = One.GetHashCode();
      var hc2 = Two.GetHashCode();

      return hc1 + hc2;
    }

    public override string ToString()
    {
      string oneStr = One.ToString();
      string twoStr = Two.ToString();
      return String.Format("({0},{1})", oneStr, twoStr);
    }

    #region IEquatable<Pair<L,R>> Members

    public bool Equals(Pair<L, R> other)
    {
      bool result = (this.One is IEquatable<L>) ? (((IEquatable<L>)this.One).Equals(other.One)) : Object.Equals(this.One, other.One);
      if (!result) return result;

      return (this.Two is IEquatable<R>) ? (((IEquatable<R>)this.Two).Equals(other.Two)) : Object.Equals(this.Two, other.Two);
    }

    #endregion
  }

  public static class STuple
  {
    public static STuple<A, B, C> For<A, B, C>(A first, B second, C third) { return new STuple<A, B, C>(first, second, third); }
    public static STuple<A, B, C, D> For<A, B, C, D>(A first, B second, C third, D fourth) { return new STuple<A, B, C, D>(first, second, third, fourth); }
  }

  [Serializable]
  public struct STuple<A, B, C> : IEquatable<STuple<A, B, C>>
  {
    /// <summary>
    /// First component
    /// </summary>
    public readonly A One;

    /// <summary>
    /// Second component
    /// </summary>
    public readonly B Two;

    /// <summary>
    /// Third component
    /// </summary>
    public readonly C Three;


    public STuple(A first, B second, C third)
    {
      this.One = first;
      this.Two = second;
      this.Three = third;
    }

    public override string ToString()
    {
      string oneStr = One == null ? "<null>" : One.ToString();
      string twoStr = Two == null ? "<null>" : Two.ToString();
      string threeStr = Three == null ? "<null>" : Three.ToString();
      return String.Format("({0},{1},{2})", oneStr, twoStr, threeStr);
    }

    #region IEquatable<Tuple<A,B,C>> Members

    //^ [StateIndependent]
    public bool Equals(STuple<A,B,C> other)
    {
      bool result = (this.One is IEquatable<A>) ? (((IEquatable<A>)this.One).Equals(other.One)) : Object.Equals(this.One, other.One);
      if (!result) return result;
      result = (this.Two is IEquatable<B>) ? (((IEquatable<B>)this.Two).Equals(other.Two)) : Object.Equals(this.Two, other.Two);
      if (!result) return result;
      result = (this.Three is IEquatable<C>) ? (((IEquatable<C>)this.Three).Equals(other.Three)) : Object.Equals(this.Three, other.Three);
      return result;
    }

    #endregion
  }

  public struct STuple<A, B, C, D> : IEquatable<STuple<A, B, C, D>>
  {
    /// <summary>
    /// First component
    /// </summary>
    public readonly A One;

    /// <summary>
    /// Second component
    /// </summary>
    public readonly B Two;

    /// <summary>
    /// Third component
    /// </summary>
    public readonly C Three;

    /// <summary>
    /// Fourth component
    /// </summary>
    public readonly D Four;

    public STuple(A first, B second, C third, D fourth)
    {
      this.One = first;
      this.Two = second;
      this.Three = third;
      this.Four = fourth;
    }

    public override string ToString()
    {
      string oneStr = One == null ? "<null>" : One.ToString();
      string twoStr = Two == null ? "<null>" : Two.ToString();
      string threeStr = Three == null ? "<null>" : Three.ToString();
      string fourStr = Four == null ? "<null>" : Four.ToString();
      return String.Format("({0},{1},{2},{3})", oneStr, twoStr, threeStr, fourStr);
    }

    #region IEquatable<Tuple<A,B,C>> Members

    //^ [StateIndependent]
    public bool Equals(STuple<A, B, C, D> other)
    {
      bool result = (this.One is IEquatable<A>) ? (((IEquatable<A>)this.One).Equals(other.One)) : Object.Equals(this.One, other.One);
      if (!result) return result;
      result = (this.Two is IEquatable<B>) ? (((IEquatable<B>)this.Two).Equals(other.Two)) : Object.Equals(this.Two, other.Two);
      if (!result) return result;
      result = (this.Three is IEquatable<C>) ? (((IEquatable<C>)this.Three).Equals(other.Three)) : Object.Equals(this.Three, other.Three);

      if (!result) return result;
      result = (this.Four is IEquatable<C>) ? (((IEquatable<C>)this.Four).Equals(other.Four)) : Object.Equals(this.Four, other.Four);
 
      return result;
    }

    #endregion
  }

  [ContractVerification(true)]
  public static class FList
  {
    /// <summary>
    /// Returns a new list representing the appended lists
    /// </summary>
    [Pure]
    public static FList<T>/*?*/ Append<T>(this FList<T>/*?*/ l1, FList<T>/*?*/ l2)
    {
      Contract.Ensures((l1 == null || l2 == null) || Contract.Result<FList<T>>() != null);
      Contract.Ensures(l1 != null || Contract.Result<FList<T>>() == l2);
      Contract.Ensures(l2 != null || Contract.Result<FList<T>>() == l1);

      if (l1 == null) return l2;

      if (l2 == null) return l1;

      return l1.Tail.Append(l2).Cons(l1.Head);
    }

    /// <summary>
    /// Gives the length of the list
    /// </summary>
    [Pure]
    [ContractVerification(false)]
    public static int Length<T>(this FList<T>/*?*/ l)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() >= 1 || l == null);
      Contract.Ensures(Contract.Result<int>() <= 0 || l != null);
      Contract.Ensures(Contract.Result<int>() <= 1 || l.Tail != null);

      return FList<T>.Length(l);
    }

    /// <summary>
    /// Construct a new list by consing the element to the head of tail list
    /// </summary>
    [Pure]
    public static FList<T> Cons<T>(this FList<T>/*?*/ rest, T elem)
    {
      Contract.Ensures(Contract.Result<FList<T>>() != null);

      return FList<T>.Cons(elem, rest);
    }

    [Pure]
    public static FList<T> ConsIfNotAlreadyThere<T>(this FList<T> list, T what, Comparer<T> comparer = null)
    {
      Contract.Ensures(Contract.Result<FList<T>>() != null);

      if(list == null || what == null)
      {
        return list.Cons(what);
      }

      if (comparer != null)
      {
        for (var curr = list; curr != FList<T>.Empty; curr = curr.Tail)
        {
          var head = curr.Head;
          if (comparer.Compare(head, what) == 0)
          {
            return list;
          }
        }
      }
      else
      {
        for (var curr = list; curr != FList<T>.Empty; curr = curr.Tail)
        {
          var head = curr.Head;
          if (what.Equals(head))
          {
            return list;
          }
        }

      }
      return list.Cons(what);
    }



    /// <summary>
    /// Constructs a new list that represents the reversed original list
    /// </summary>
    [Pure]
    public static FList<T>/*?*/ Reverse<T>(this FList<T>/*?*/ list)
    {
      Contract.Ensures(list == null || Contract.Result<FList<T>>() != null);

      return FList<T>.Reverse(list);
    }

    /// <summary>
    /// Constructs a new list that represents the reversed original list with the applied conversion
    /// </summary>
    [Pure]
    public static FList<R>/*?*/ Reverse<T,R>(this FList<T>/*?*/ list, Func<T,R> converter)
    {
      Contract.Ensures(list == null || Contract.Result<FList<R>>() != null);

      return FList<T>.Reverse(list, converter);
    }

    /// <summary>
    /// Return a list that only contains elements satisfying predicate in the same order 
    /// </summary>
    /// <param name="predicate">Predicate to be satisfied to be in result</param>
    [Pure]
    public static FList<T> Filter<T>(this FList<T> list, Predicate<T> predicate)
    {
      Contract.Requires(predicate != null);

      if (list == null) return null;
      var tail = Filter(list.Tail, predicate);
      if (!predicate(list.Head)) return tail;
      if (tail == list.Tail) return list;
      return tail.Cons(list.Head);
    }

    /// <summary>
    /// Applies action to each element in list
    /// </summary>
    /// <param name="list">List iterated over</param>
    /// <param name="action">Action called on each element</param>
    public static void Apply<T>(this FList<T>/*?*/ list, Action<T> action)
    {
      FList<T>.Apply(list, action);
    }

    [Pure]
    public static IEnumerable<T> GetEnumerable<T>(this FList<T>/*?*/ list)
    {
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

      return FList<T>.PrivateGetEnumerable(list);
    }

    [Pure]
    public static bool IsEmpty<T>(this FList<T> list)
    {
      Contract.Ensures(Contract.Result<bool>() == (list == null));
      return list == null;
    }

    [Pure]
    public static T Last<T>(this FList<T> list)
    {
      Contract.Requires(!list.IsEmpty());
      if (list.Tail == null)
      {
        return list.Head;
      }
      return Last(list.Tail);
    }

    [Pure]
    // Needed when looking at access paths
    public static T ButLast<T>(this FList<T> list)
    {
      Contract.Requires(!list.IsEmpty());
      Contract.Requires(list.Tail != null);
      return ButLastInternal(list.Tail, list.Head);
    }

    [Pure]
    private static T ButLastInternal<T>(this FList<T> list, T butLast)
    {
      Contract.Requires(list != null);

      if (list.Tail == null)
        return butLast;
      return ButLastInternal(list.Tail, list.Head);
    }

    [Pure]
    public static FList<T> Coerce<S, T>(this FList<S> list) where S : T
    {
      return list.Map(orig => (T)orig);
    }

    [Pure]
    public static bool Contains<T>(this FList<T> list, T value) where T : IEquatable<T>
    {
      for (; list != null; list = list.Tail)
      {
        if (list.Head.Equals(value)) return true;
      }
      return false;
    }

    [Pure]
    public static FList<T> Singleton<T>(T elem)
    {
      Contract.Ensures(Contract.Result<FList<T>>() != null);

      return Cons(elem, null);
    }

    [Pure]
    public static FList<T> Cons<T>(T elem, FList<T> tail)
    {
      Contract.Ensures(Contract.Result<FList<T>>() != null);

      return tail.Cons(elem);
    }

    [Pure]
    public static FList<C> Product<A, B, C>(FList<A> alist, FList<B> blist, Func<A, B, C> mapper, FList<C> accumulator = null)
    {
      Contract.Requires(mapper != null);

      var alist2 = alist;
      while (alist2 != null)
      {
        var blist2 = blist;
        while (blist2 != null)
        {
          accumulator = accumulator.Cons(mapper(alist2.Head, blist2.Head));
          blist2 = blist2.Tail;
        }
        alist2 = alist2.Tail;
      }
      return accumulator;
    }

    [Pure]
    public static FList<B> Product<A, B>(this FList<FList<A>> lists, Func<FList<A>, B> combine, FList<B> accumulator = null)
    {
      if (lists == null) return accumulator;
      var first = lists.Head;
      if (lists.Tail == null) { return first.Map(e => combine(Singleton(e)), accumulator); }
      while (first != null)
      {
        var subProducts = Product(lists.Tail, l => l);
        Contract.Assume(first != null, "for some reason first gets havoced after product");
        accumulator = subProducts.Map(rest => combine(rest.Cons(first.Head)), accumulator);
        first = first.Tail;
      }
      return accumulator;
    }

    [Pure]
    public static FList<B> Map<A, B>(this IEnumerable<A> enumerable, Func<A, B> mapper)
    {
      Contract.Requires(enumerable != null);
      Contract.Requires(mapper != null);

      FList<B> result = null;
      foreach (var a in enumerable)
      {
        result = result.Cons(mapper(a));
      }
      return result.Reverse();
    }

    /// <summary>
    /// Transforms a T list into an S list using the converter
    /// </summary>
    [Pure]
    public static FList<S>/*?*/ Map<T, S>(this FList<T>/*?*/ list, Converter<T, S> converter, FList<S> accumulator = null)
    {
      Contract.Requires(converter != null);

      if (list == null) return accumulator;
      FList<S>/*?*/ tail = list.Tail.Map(converter, accumulator);
      return tail.Cons(converter(list.Head));
    }

    /// <summary>
    /// Transforms a T list into an S list using the converter
    /// </summary>
    [Pure]
    public static FList<S>/*?*/ Map<T, S>(this FList<T>/*?*/ list, Converter<T, S> converter, int maxLength, FList<S> accumulator = null)
    {
      Contract.Requires(converter != null);

      if (list == null) return accumulator;
      if (maxLength <= 0) return accumulator;
      FList<S>/*?*/ tail = list.Tail.Map(converter, maxLength -1, accumulator);
      return tail.Cons(converter(list.Head));
    }

    [Pure]
    public static T[] ToArray<T>(this FList<T> list)
    {
      Contract.Ensures(Contract.Result<T[]>() != null);

      var result = new T[list.Length()];
      var i = 0;
      while (list != null)
      {
        Contract.Assume(i < result.Length, "need to relate list length with iteration");
        result[i++] = list.Head;
        list = list.Tail;
      }
      return result;
    }
  }

  /// <summary>
  /// Functional lists. null represents the empty list.
  /// </summary>
  [Serializable]
  public class FList<T> 
  {

    #region Privates
    private T elem;

    private FList<T>/*?*/ tail;

    private int count;
    #endregion

    public static FList<T> Cons(T elem, FList<T> tail)
    {
      Contract.Ensures(Contract.Result<FList<T>>() != null);

      return new FList<T>(elem, tail);
    }

    FList(T elem, FList<T>/*?*/ tail)
    {
      this.elem = elem;
      this.tail = tail;
      this.count = Length(tail) + 1;
    }

    /// <summary>
    /// The head element of the list
    /// </summary>
    public T Head { get { return this.elem; } }

    /// <summary>
    /// The tail of the list
    /// </summary>
    public FList<T>/*?*/ Tail {
      get { return this.tail; }
    }

    /// <summary>
    /// Reusable Empty list representation
    /// </summary>
    public const FList<T>/*?*/ Empty = null;

    /// <summary>
    /// Constructs a new list that represents the reversed original list
    /// </summary>
    public static FList<T>/*?*/ Reverse(FList<T>/*?*/ list) {
      Contract.Ensures(Contract.Result<FList<T>>() != null || list == null);

      if (list == null || list.Tail == null) return list;

      FList<T>/*?*/ tail = null;

      while (list != null) {
        tail = tail.Cons(list.elem);
        list = list.tail;
      }
      return tail;
    }

    /// <summary>
    /// Constructs a new list that represents the reversed original list
    /// </summary>
    public static FList<R>/*?*/ Reverse<R>(FList<T>/*?*/ list, Func<T,R> converter)
    {
      Contract.Ensures(Contract.Result<FList<R>>() != null || list == null);

      if (list == null) return null;

      FList<R>/*?*/ result = null;

      while (list != null)
      {
        result = result.Cons(converter(list.elem));
        list = list.tail;
      }
      return result;
    }


    internal static IEnumerable<T> PrivateGetEnumerable(FList<T>/*?*/ list)
    {
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

      FList<T>/*?*/ current = list;
      while (current != null)
      {
        T next = current.Head;
        current = current.Tail;
        yield return next;
      }
      yield break;
    }



    /// <summary>
    /// Query the list for the presence of an element
    /// </summary>
    public static bool Contains(FList<T>/*?*/ l, T/*!*/ o) {
      if (l == null) return false;

      if (o is IEquatable<T>) { if (((IEquatable<T>)o).Equals(l.elem)) return true; }
      else if (o.Equals(l.elem)) return true;

      return Contains(l.tail, o);
    }


    /// <summary>
    /// Applies action to each element in list
    /// </summary>
    /// <param name="list">List iterated over</param>
    /// <param name="action">Action called on each element</param>
    public static void Apply(FList<T>/*?*/ list, Action<T> action) {
      while (list != null) {
        action(list.Head);
        list = list.Tail;
      }
    }


    /// <summary>
    /// Given two sorted lists, compute their intersection
    /// </summary>
    /// <param name="l1">sorted list</param>
    /// <param name="l2">sorted list</param>
    /// <returns>sorted intersection</returns>
    public static FList<T>/*?*/ Intersect(FList<T>/*?*/ l1, FList<T>/*?*/ l2) {
      if (l1 == null || l2 == null) return null;

      int comp = System.Collections.Comparer.Default.Compare(l1.Head, l2.Head);
      if (comp < 0) {
        return Intersect(l1.Tail, l2);
      }
      if (comp > 0) {
        return Intersect(l1, l2.Tail);
      }
      // equal
      return Intersect(l1.Tail, l2.Tail).Cons(l1.Head);
    }

    /// <summary>
    /// Returns a new list that contains the elements of the argument list in increasing order
    /// </summary>
    public static FList<T>/*?*/ Sort(FList<T>/*?*/ l) {
      return Sort(l, null);
    }

    private static FList<T>/*?*/ Sort(FList<T>/*?*/ l, FList<T>/*?*/ tail) {
      // quicksort
      if (l == null) return tail;

      T pivot = l.Head;

      FList<T>/*?*/ less;
      FList<T>/*?*/ more;
      Partition(l.Tail, pivot, out less, out more);

      return Sort(less, Sort(more, tail).Cons(pivot));
    }

    private static void Partition(FList<T>/*?*/ l, T pivot, out FList<T>/*?*/ less, out FList<T>/*?*/ more) {
      less = null;
      more = null;
      if (l == null) {
        return;
      }
      foreach (T value in l.GetEnumerable()) {
        if (System.Collections.Comparer.Default.Compare(value, pivot) <= 0) {
          less = less.Cons(value);
        }
        else {
          more = more.Cons(value);
        }
      }
    }
    /// <summary>
    /// Gives the length of the list
    /// </summary>
    [Pure]
    public static int Length(FList<T>/*?*/ l)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      if (l == null) { return 0; }
      else return l.count;
    }

    /// <summary>
    /// Generates a string representing the list
    /// </summary>
    /// <returns></returns>
    //^ [Confined]
    public override string ToString() {
      var sb = new StringBuilder();

      this.BuildString(sb);
      return sb.ToString();
    }

    /// <summary>
    /// Adds a string representation of the list to the StringBuilder
    /// </summary>
    public void BuildString(StringBuilder sb) {
      string elemStr = this.elem == null ? "<null>" : this.elem.ToString();
      if (this.tail != null) {
        sb.AppendFormat("{0},", elemStr);
        this.tail.BuildString(sb);
      }
      else {
        sb.Append(elemStr);
      }
    }


  }

  public enum VisitStatus { ContinueVisit, StopVisit }

  public delegate VisitStatus VisitMapPair<Key,Value>(Key key, Value value);

  public partial interface IFunctionalMap<Key,Val> : IEquatable<IFunctionalMap<Key,Val>> {

    Val/*?*/ this[Key/*!*/ key] { get; }
    [Pure]
    IFunctionalMap<Key,Val> Add(Key/*!*/ key, Val val);
    [Pure]
    IFunctionalMap<Key,Val> Remove(Key/*!*/ key);
    [Pure]
    bool Contains(Key/*!*/ key);
    /// <summary>
    /// For non-empty domains, returns one of the keys
    /// </summary>
    Key AnyKey { get; }
    IEnumerable<Key> Keys { get; }
    void Visit(VisitMapPair<Key/*!*/,Val> visitor);
    int Count { get; }

    /// <summary>
    /// Returns an emtpy map
    /// </summary>
    IFunctionalMap<Key, Val> EmptyMap { get; }

    void Dump(System.IO.TextWriter tw);
  }

  #region IFunctionalMap<Key,Val> contract binding
  [ContractClass(typeof(IFunctionalMapContract<,>))]
  public partial interface IFunctionalMap<Key,Val>
  {

  }

  [ContractClassFor(typeof(IFunctionalMap<,>))]
  abstract class IFunctionalMapContract<Key,Val>: IFunctionalMap<Key,Val>
  {
    #region IFunctionalMap<Key,Val> Members

    public Val this[Key key]
    {
      get { throw new NotImplementedException(); }
    }

    public IFunctionalMap<Key, Val> Add(Key key, Val val)
    {
      Contract.Ensures(Contract.Result<IFunctionalMap<Key,Val>>() != null);
      throw new NotImplementedException();
    }

    public IFunctionalMap<Key, Val> Remove(Key key)
    {
      Contract.Ensures(Contract.Result<IFunctionalMap<Key, Val>>() != null);
      throw new NotImplementedException();
    }

    public bool Contains(Key key)
    {
      throw new NotImplementedException();
    }

    public Key AnyKey
    {
      get { throw new NotImplementedException(); }
    }

    public IEnumerable<Key> Keys
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<Key>>() != null);
        throw new NotImplementedException();
      }
    }

    public void Visit(VisitMapPair<Key, Val> visitor)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get { 
        Contract.Ensures(Contract.Result<int>() >= 0);
        throw new NotImplementedException(); 
      }
    }

    public IFunctionalMap<Key, Val> EmptyMap
    {
      get
      {
        Contract.Ensures(Contract.Result<IFunctionalMap<Key, Val>>() != null);

        throw new NotImplementedException();
      }
    }

    public void Dump(TextWriter tw)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEquatable<IFunctionalMap<Key,Val>> Members

    public bool Equals(IFunctionalMap<Key, Val> other)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
  #endregion

  [ContractClass(typeof(IFunctionalIntMapContracts<>))]
  public interface IFunctionalIntMap<B> {
    /*
     * Ideally, the key type in this API would be 
     * an interface IHasUniqueId with a single property
     * that return an int. However, in our use of this
     * API, we need to plug in objects that cannot be
     * unified under a common interface because we don't 
     * own some of the classes. So, awkwardly, we ask
     * the client to pass in BOTH a key object and its
     * integer under which it will be stored.
     */
    [Pure]
    B Lookup(int keyNumber);
    B this[int keyNumber] { get; }
    // For a non-empty map, returns any value
    B Any { get; }
    [Pure]
    bool Contains(int keyNumber);
    [Pure]
    IFunctionalIntMap<B> Add(int keyNumber, B val);
    IEnumerable<B> Values { get; }
    IEnumerable<int> Keys { get; }
    void Visit(Action<B> action);
    void Visit(Action<int, B> action);
    [Pure]
    IFunctionalIntMap<B> Remove(int keyNumber);
    int Count { get; }

    void Dump(System.IO.TextWriter tw);
  }

  [ContractClassFor(typeof(IFunctionalIntMap<>))]
  abstract class IFunctionalIntMapContracts<B> 
    : IFunctionalIntMap<B>
  {
    public IFunctionalIntMap<B> Add(int keyNumber, B val)
    {
      Contract.Ensures(Contract.Result<IFunctionalIntMap<B>>() != null);

      return null;
    }

    #region No Contracts
    public B Lookup(int keyNumber)
    {
      throw new NotImplementedException();
    }

    public B this[int keyNumber]
    {
      get { throw new NotImplementedException(); }
    }

    public B Any
    {
      get { throw new NotImplementedException(); }
    }

    public bool Contains(int keyNumber)
    {
      throw new NotImplementedException();
    }



    public IEnumerable<B> Values
    {
      get { throw new NotImplementedException(); }
    }

    public IEnumerable<int> Keys
    {
      get { throw new NotImplementedException(); }
    }

    public void Visit(Action<B> action)
    {
      throw new NotImplementedException();
    }

    public void Visit(Action<int, B> action)
    {
      throw new NotImplementedException();
    }

    public IFunctionalIntMap<B> Remove(int keyNumber)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get { throw new NotImplementedException(); }
    }

    public void Dump(TextWriter tw)
    {
      throw new NotImplementedException();
    }
    #endregion
  }

  public class FunctionalIntMap<B>
  {
    // Adapted from Okasaki and Gill, "Fast mergeable integer maps", ML Workshop, 1998.

    public static IFunctionalIntMap<B> EmptyMap
    {
      get
      {
        Contract.Ensures(Contract.Result<IFunctionalIntMap<B>>() != null); // F: addded
        return Empty.E;
      }
    }

    private abstract class PatriciaTree : IFunctionalIntMap<B>
    {
      public abstract B Lookup (int keyNumber);

      public B this[int keyNumber] { get { return this.Lookup(keyNumber); } }

      public abstract B Any { get; }

      public abstract IFunctionalIntMap<B> Add (int keyNumber, B val);

      public abstract int Count { get; }

      public abstract bool Contains(int keyNumber);

      internal abstract void AddValues (List<B> list);

      internal abstract void AddKeys(List<int> list);

      public abstract IFunctionalIntMap<B> Remove (int keyNumber);

      internal abstract void AppendToBuffer (System.Text.StringBuilder buffer);

      protected abstract int KeyNumber { get; }

      internal abstract void Dump (System.IO.TextWriter tw, string prefix);

      public void Dump (System.IO.TextWriter tw) { this.Dump(tw, ""); }

      public IEnumerable<B> Values 
      {
        // If gathering a list is too expensive, we can change this one day
        // That's why I made the return type IEnumerable rather than ICollection. RD
        get 
        { 
          List<B> list = new List<B>(this.Count);
          this.AddValues(list);
          return list;
        } 
      }

      public IEnumerable<int> Keys
      {
        // If gathering a list is too expensive, we can change this one day
        // That's why I made the return type IEnumerable rather than ICollection. RD
        get
        {
          List<int> list = new List<int>(this.Count);
          this.AddKeys(list);
          return list;
        }
      }

      public abstract void Visit(Action<B> action);
      public abstract void Visit(Action<int, B> action);

      //^ [Confined]
      public override string ToString ()
      {
        System.Text.StringBuilder buffer = new System.Text.StringBuilder();
        buffer.Append("[ ");
        this.AppendToBuffer(buffer);
        buffer.Append("]");
        return buffer.ToString();
      }

      protected static int LowestBit (int x) { return x & -x; }
      protected static int BranchingBit (int p0, int p1) { return LowestBit(p0 ^ p1); }
      protected static int MaskBits (int k, int m) { return k & (m-1); }
      protected static bool ZeroBit (int k, int m) { return (k & m) == 0; }
      protected static bool MatchPrefix (int k, int p, int m) { return MaskBits(k,m) == p; }


      protected static PatriciaTree Join (PatriciaTree t0, PatriciaTree t1)
      {
        int p0 = t0.KeyNumber, p1 = t1.KeyNumber;
        int m = BranchingBit(p0, p1);
        return ZeroBit(p0, m) ?
          new Branch(MaskBits(p0,m), m, t0, t1) :
          new Branch(MaskBits(p0,m), m, t1, t0);
      }
    }



    private class Empty : PatriciaTree
    {
      public static readonly Empty E = new Empty(); // Thread-safe

      protected override int KeyNumber { get { throw new System.NotImplementedException(); } }

      public override B Lookup(int key) { return default(B); }

      public override B Any
      {
        get { return default(B); }
      }

      public override IFunctionalIntMap<B> Add(int keyNumber, B val) 
      {  
        return new Leaf(keyNumber, val); 
      }

      public override IFunctionalIntMap<B> Remove (int keyNumber) { return this; }

      public override int Count { get { return 0; } }

      public override bool Contains(int keyNumber) {
        return false;
      }

      internal override void AddValues (List<B> list) { }

      internal override void AddKeys(List<int> list) { }

      public override void Visit(Action<B> action) { 
        // do nothing
      }

      public override void Visit(Action<int, B> action)
      {
        // do nothing
      }

      internal override void AppendToBuffer(System.Text.StringBuilder buffer) { }

      internal override void Dump (System.IO.TextWriter tw, string prefix) { tw.WriteLine(prefix + "<Empty/>"); }
    }



    private class Leaf : PatriciaTree
    {
      public readonly int UniqueId;
      public readonly B Value;

      public Leaf (int k, B val) { this.UniqueId = k; this.Value = val; }

      protected override int KeyNumber { get { return this.UniqueId; } }

      public override B Lookup(int key) 
      { 
        return key == this.UniqueId ? this.Value : default(B); 
      }

      public override B Any
      {
        get { return this.Value; }
      }

      public override IFunctionalIntMap<B> Add(int keyNumber, B val) 
      { 
        int thisUniqueId = this.UniqueId;
        return (keyNumber == thisUniqueId) ? 
          new Leaf(keyNumber, val) : 
          Join(new Leaf(keyNumber, val), this); 
      }

      public override IFunctionalIntMap<B> Remove (int keyNumber) 
      { 
        return keyNumber == this.UniqueId ? Empty.E : (IFunctionalIntMap<B>)this; 
      }

      public override int Count { get { return 1; } }

      public override bool Contains(int keyNumber) {
        return this.UniqueId == keyNumber;
      }

      internal override void AddValues (List<B> list) { list.Add(this.Value); }

      internal override void AddKeys(List<int> list) { list.Add(this.UniqueId); }

      public override void Visit(Action<B> action)
      {
        action(Value);
      }
      public override void Visit(Action<int, B> action)
      {
        action(this.KeyNumber, this.Value);
      }

      internal override void AppendToBuffer (System.Text.StringBuilder buffer) 
      { 
        buffer.AppendFormat("{0}->{1} ", this.KeyNumber, this.Value);
      }

      internal override void Dump (System.IO.TextWriter tw, string prefix) 
      { 
        tw.WriteLine(prefix + "<Leaf KeyInt={0} Value='{1}'/>", UniqueId, Value); 
      }
    }



    private class Branch : PatriciaTree
    {
      public readonly int Prefix;
      public readonly int Mask;
      public readonly PatriciaTree Left, Right;
      private readonly int count;

      [ContractInvariantMethod]
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.Left != null);
        Contract.Invariant(this.Right != null);
      }

      public Branch (int prefix, int mask, PatriciaTree left, PatriciaTree right) 
      {
        Contract.Requires(left != null);
        Contract.Requires(right != null);

        this.Prefix = prefix;
        this.Mask = mask; 
        this.Left = left; 
        this.Right = right; 
        this.count = left.Count + right.Count;
      }

      protected override int KeyNumber { get { return this.Prefix; } }

      public override B Lookup (int key) 
      {
        Branch current = this;
        do {
          if (ZeroBit(key, current.Mask)) {
            Branch next = current.Left as Branch;
            if (next == null) {
              return current.Left.Lookup(key);
            }
            current = next;
          }
          else {
            Branch next = current.Right as Branch;
            if (next == null) {
              return current.Right.Lookup(key);
            }
            current = next;
          }
        } while (true);
      }

      public override B Any
      {
        get { return Left.Any; }
      }

      public override IFunctionalIntMap<B> Add(int keyNumber, B val) 
      { 
        if (MatchPrefix(keyNumber, this.Prefix, this.Mask))
        {
          if (ZeroBit(keyNumber, this.Mask))
          {
            return new Branch(this.Prefix, this.Mask, 
              (PatriciaTree) this.Left.Add(keyNumber, val), this.Right);
          }
          else
          {
            return new Branch(this.Prefix, this.Mask, this.Left, 
              (PatriciaTree) this.Right.Add(keyNumber, val));
          }
        }
        else
          return Join(new Leaf(keyNumber, val), this);
      }

      public override IFunctionalIntMap<B> Remove (int keyNumber)
      {
        if (ZeroBit(keyNumber, this.Mask))
        {
          PatriciaTree newLeft = (PatriciaTree)this.Left.Remove(keyNumber);
          if (newLeft is Empty)
            return this.Right;
          return Join(newLeft, this.Right);
        }
        else
        {
          PatriciaTree newRight = (PatriciaTree)this.Right.Remove(keyNumber);
          if (newRight is Empty)
            return this.Left;
          return Join(this.Left, newRight);
        }
      }

      public override int Count { get { return this.count; } }

      public override bool Contains(int keyNumber) {
        if (MatchPrefix(keyNumber, this.Prefix, this.Mask)) {
          if (ZeroBit(keyNumber, this.Mask)) {
            return this.Left.Contains(keyNumber);
          }
          else {
            return this.Right.Contains(keyNumber);
          }
        }
        else
          return false;
      }


      internal override void AddValues (List<B> list) 
      { 
        this.Left.AddValues(list);
        this.Right.AddValues(list);
      }

      internal override void AddKeys(List<int> list)
      {
        this.Left.AddKeys(list);
        this.Right.AddKeys(list);
      }

      public override void Visit(Action<B> action) {
        Left.Visit(action);
        Right.Visit(action);
      }

      public override void Visit(Action<int, B> action)
      {
        Left.Visit(action);
        Right.Visit(action);
      }

      internal override void AppendToBuffer(System.Text.StringBuilder buffer) 
      { 
        this.Left.AppendToBuffer(buffer);
        this.Right.AppendToBuffer(buffer);
      }

      internal override void Dump (System.IO.TextWriter tw, string prefix) 
      { 
        tw.WriteLine(prefix + "<Branch Prefix={0} Mask={1}>", Prefix, Mask); 
        this.Left.Dump(tw, prefix + "  ");
        this.Right.Dump(tw, prefix + "  ");
        tw.WriteLine(prefix + "</Branch>");
      }
    }


  }


  public class FunctionalMap<A,B> : IFunctionalMap<A,B> where A:IEquatable<A> {
    /// <summary>
    /// Because we have no unique integers for the keys, we use HashCode and keep 
    /// conflicts in a list.
    /// </summary>
    readonly IFunctionalIntMap<FList<Pair<A/*!*/, B>>/*?*/> fimap;
    readonly int count;
    readonly FList<A> keys;

    private FunctionalMap(IFunctionalIntMap<FList<Pair<A/*!*/,B>>/*?*/> map, int count, FList<A> keys) {
      this.fimap = map;
      this.count = count;
      this.keys = keys;
    }

    public static readonly FunctionalMap<A,B> Empty = new FunctionalMap<A,B>(FunctionalIntMap<FList<Pair<A/*!*/,B>>/*?*/>.EmptyMap, 0, null); // Thread-safe

    public IFunctionalMap<A, B> EmptyMap { get { return Empty; } }

    #region IFunctionalMap Members

    public B/*?*/ this[A/*!*/ key] {
      get {
        FList<Pair<A/*!*/,B>>/*?*/ candidates = this.fimap[key.GetHashCode()];
        while (candidates != null) {
          A headkey = candidates.Head.One;
          if (key.Equals(headkey)) {
            return candidates.Head.Two;
          }
          candidates = candidates.Tail;
        }
        return default(B);
      }
    }

    public A AnyKey
    {
      get
      {
        return this.keys.Head;
      }
    }

    public bool Contains(A/*!*/ key) {
      FList<Pair<A/*!*/, B>>/*?*/ candidates = this.fimap[key.GetHashCode()];
      while (candidates != null) {
        A headkey = candidates.Head.One;
        if (key.Equals(headkey)) {
          return true;
        }
        candidates = candidates.Tail;
      }
      return false;
    }

    private FList<Pair<A/*!*/, B>>/*?*/ Remove(FList<Pair<A/*!*/, B>>/*?*/ from, A/*!*/ key) {
      if (from == null) return null;
      if (key.Equals(from.Head.One)) { return from.Tail; }
      FList<Pair<A/*!*/, B>>/*?*/ newTail = Remove(from.Tail, key);
      if (newTail == from.Tail) return from; // optimization
      return newTail.Cons(from.Head);
    }

    public IFunctionalMap<A,B> Add(A/*!*/ key, B val) {
      int keyNum = key.GetHashCode();
      FList<Pair<A/*!*/, B>>/*?*/ oldPairs = this.fimap[keyNum];
      var newPairs = Remove(oldPairs, key).Cons(new Pair<A/*!*/, B>(key, val));
      int addition = newPairs.Length() - oldPairs.Length();
      FList<A> keys = (addition == 0) ? this.keys : this.keys.Cons(key);
      return new FunctionalMap<A,B>(this.fimap.Add(keyNum, newPairs), this.count + addition, keys);
    }

    public IFunctionalMap<A,B> Remove(A/*!*/ key) {
      int keyNum = key.GetHashCode();
      FList<Pair<A/*!*/, B>>/*?*/ oldPairs = this.fimap[keyNum];
      if (oldPairs == null) return this; // no change

      FList<Pair<A/*!*/, B>>/*?*/ newPairs = Remove(oldPairs, key);
      if (newPairs == oldPairs) return this; // no change

      // Optimize: remove element if newPairs == null
      FList<A> keys = RemoveKey(key, this.keys);
      if (newPairs == null) {
        return new FunctionalMap<A, B>(this.fimap.Remove(keyNum), this.count - 1, keys);
      }
      return new FunctionalMap<A,B>(this.fimap.Add(keyNum, newPairs), this.count - 1, keys);
    }

    private static FList<A> RemoveKey(A key, FList<A> keys)
    {
      if (keys == null) { throw new InvalidOperationException(); }
      if (key.Equals(keys.Head)) {
        return keys.Tail;
      }
      return RemoveKey(key, keys.Tail).Cons(keys.Head);
    }

    public IEnumerable<A> Keys
    {
      get {
        return this.keys.GetEnumerable();
#if false
        List<A> result = new List<A>();
        this.fimap.Visit(delegate(FList<Pair<A/*!*/, B>>/*?*/ list) { while (list != null) { result.Add(list.Head.One); list = list.Tail; } });
        return result;
#endif
      }
    }

    public void Visit(VisitMapPair<A/*!*/,B> visitor) {
      this.fimap.Visit(delegate(FList<Pair<A/*!*/, B>>/*?*/ list) { while (list != null) { visitor(list.Head.One, list.Head.Two); list = list.Tail; } });
    }

    public int Count {
      get {
        return this.count;
      }
    }

    public void Dump(TextWriter tw) {
      this.fimap.Dump(tw);
    }

    #endregion

    #region IEquatable Members
    //^ [StateIndependent]
    public bool Equals(IFunctionalMap<A, B> other) { return this == other; }
    #endregion
  }

  public class FunctionalIntKeyMap<A, B> : IFunctionalMap<A, B> { 
    #region Privates
    private readonly IFunctionalIntMap<Pair<A/*!*/, B>> fimap;
    private readonly Converter<A/*!*/, int> KeyNumber;
    // private readonly FList<A> keys;

    private FunctionalIntKeyMap(IFunctionalIntMap<Pair<A/*!*/,B>> fimap, Converter<A/*!*/,int> keynumber/*, FList<A> keys*/) {
      this.fimap = fimap;
      this.KeyNumber = keynumber;
//      this.keys = keys;
    }
    #endregion

    public B/*?*/ this[A/*!*/ key] {
      get {
        return fimap[KeyNumber(key)].Two;
      }
    }

    public A AnyKey { get { return this.Keys.First(); } }

    public IFunctionalMap<A, B> Add(A/*!*/ key, B val) {
      //int oldCount = this.fimap.Count;
      IFunctionalIntMap<Pair<A,B>> newMap = this.fimap.Add(KeyNumber(key), new Pair<A/*!*/,B>(key,val));
      //FList<A> keys = this.keys;
      //if (newMap.Count != oldCount)
      //{
        //keys = keys.Cons(key);
      //}
      return new FunctionalIntKeyMap<A,B>(newMap, this.KeyNumber);
    }

    public IFunctionalMap<A, B> Remove(A/*!*/ key) {
      //int oldCount = this.fimap.Count;
      IFunctionalIntMap<Pair<A, B>> newMap = this.fimap.Remove(KeyNumber(key));
      //FList<A> keys = this.keys;
      //if (newMap.Count != oldCount) {
      //  // remove key
      //  keys = RemoveKey(KeyNumber(key), keys);
      //}
      return new FunctionalIntKeyMap<A, B>(newMap, this.KeyNumber);
    }

    private FList<A> RemoveKey(int keynumber, FList<A> keys)
    {
      if (keys == null) { throw new InvalidOperationException(); }
      if (KeyNumber(keys.Head) == keynumber) {
        return keys.Tail;
      }
      return RemoveKey(keynumber, keys.Tail).Cons(keys.Head);
    }

    private IEnumerable<A> keyCache;
    public IEnumerable<A> Keys {
      get
      {
#if false
        return this.keys.GetEnumerable();
#else
        if (keyCache == null)
        {
          List<A> l = new List<A>();
          fimap.Visit(delegate(Pair<A/*!*/, B> data) { l.Add(data.One); });
          this.keyCache = l;
        }
        return this.keyCache;
#endif
      }
    }

    public void Visit(VisitMapPair<A/*!*/, B> visitor) {
      fimap.Visit(delegate (Pair<A/*!*/,B> data) { visitor(data.One, data.Two); } );
    }

    public int Count {
      get { return fimap.Count; }
    }

    public bool Contains(A/*!*/ key) {
      return this.fimap.Contains(this.KeyNumber(key));
    }

    public void Dump(TextWriter tw) {
      fimap.Dump(tw);
    }

    public static IFunctionalMap<A, B> Empty(Converter<A/*!*/, int> keyNumber) {
      Contract.Ensures(Contract.Result<IFunctionalMap<A, B>>() != null);

      return new FunctionalIntKeyMap<A, B>(FunctionalIntMap<Pair<A/*!*/, B>>.EmptyMap, keyNumber);
    }

    public IFunctionalMap<A, B> EmptyMap { get { return Empty(this.KeyNumber); } }

    #region IEquatable Members
    //^ [StateIndependent]
    public bool Equals(IFunctionalMap<A, B> other) { return this == other; }
    #endregion
}

  public class DoubleFunctionalMap<A, B, C> where A:IEquatable<A> where B:IEquatable<B> {

    private IFunctionalMap<A, IFunctionalMap<B, C>> map;

    public C/*?*/ this[A/*!*/ key1, B/*!*/ key2] {
      get {
        IFunctionalMap<B, C>/*?*/ t = this.map[key1];

        if (t == null) {
          return default(C);
        }

        return t[key2];
      }
    }

    public DoubleFunctionalMap<A, B, C> Add(A/*!*/ key1, B/*!*/ key2, C value) {
      IFunctionalMap<B, C>/*?*/ t = this.map[key1];

      if (t == null) {
        t = FunctionalMap<B, C>.Empty;
      }

      return new DoubleFunctionalMap<A, B, C>(this.map.Add(key1, t.Add(key2, value)));
    }


    public DoubleFunctionalMap<A, B, C> RemoveAll(A/*!*/ key1) {
      return new DoubleFunctionalMap<A, B, C>(this.map.Remove(key1));
    }

    public DoubleFunctionalMap<A, B, C> Remove(A/*!*/ key1, B/*!*/ key2) {
      IFunctionalMap<B, C>/*?*/ t = this.map[key1];

      if (t == null) { return this; }

      var t2 = t.Remove(key2);
      if (t == t2) return this; // no-op

      return new DoubleFunctionalMap<A, B, C>(this.map.Add(key1, t2));
    }

    private DoubleFunctionalMap(IFunctionalMap<A, IFunctionalMap<B, C>> map) {
      this.map = map;
    }

    public static DoubleFunctionalMap<A, B, C> Empty(Converter<A/*!*/, int> keynumber) {
      return new DoubleFunctionalMap<A, B, C>(FunctionalIntKeyMap<A, IFunctionalMap<B, C>>.Empty(keynumber));
    }

    public bool Contains(A key1, B key2)
    {
      var map1 = this.map[key1];
      if (map1 == null) return false;
      return map1.Contains(key2);
    }

    public bool ContainsKey1(A/*!*/ key1) {
      return this.map[key1] != null;
    }

    public int Keys1Count
    {
      get
      {
        return this.map.Count;
      }
    }

    public IEnumerable<A> Keys1 {
      get { return this.map.Keys; }
    }

    public int Keys2Count(A/*!*/ key1) {
      if (key1 == null) return 0;
      IFunctionalMap<B, C>/*?*/ map2 = this.map[key1];

      if (map2 == null) { return 0; }
      return map2.Count;
    }

    private B[] EmptyCache = new B[0];

    public IEnumerable<B> Keys2(A/*!*/ key1) {
      if (key1 == null) { return EmptyCache; }
      IFunctionalMap<B, C>/*?*/ map2 = this.map[key1];

      if (map2 == null) { return EmptyCache; }
      return map2.Keys;
    }

  }


  [ContractClass(typeof(IFunctionalSetContracts<>))]
  public interface IFunctionalSet<T> where T : IEquatable<T>
  {
    IFunctionalSet<T> Add(T/*!*/ elem);
    IFunctionalSet<T> Remove(T/*!*/ elem);
    bool Contains(T/*!*/ elem);

    /// <summary>
    /// For non-empty sets, returns one of the elements
    /// </summary>
    T Any { get; }
    
    /// <summary>
    /// Returns true, if every element in this is contained in that.
    /// </summary>
    bool Contained(IFunctionalSet<T> that);

    /// <summary>
    /// Returns the intersection of this and that.
    /// </summary>
    IFunctionalSet<T> Intersect(IFunctionalSet<T> that);

    IFunctionalSet<T> Union(IFunctionalSet<T> that);

    void Visit(Action<T/*!*/> visitor);
    int Count { get; }
    IEnumerable<T> Elements { get; }

    void Dump(System.IO.TextWriter tw);
  }

  [ContractClassFor(typeof(IFunctionalSet<>))]
  abstract class IFunctionalSetContracts<T> : IFunctionalSet<T>
    where T : IEquatable<T>
  {
    #region IFunctionalSet<T> Members

    public IFunctionalSet<T> Add(T elem)
    {
      Contract.Ensures(Contract.Result<IFunctionalSet<T>>() != null);

      //Contract.Requires(elem != null);
      return null;
    }

    public IFunctionalSet<T> Remove(T elem)
    {
      //Contract.Requires(elem != null);
      return null;
    }

    public bool Contains(T elem)
    {
      //Contract.Requires(elem != null);
      return false;
    }

    public T Any
    {
      get
      {
        return default(T);
      }
    }

    public bool Contained(IFunctionalSet<T> that)
    {
      return false;
    }

    public IFunctionalSet<T> Intersect(IFunctionalSet<T> that)
    {
      Contract.Ensures(Contract.Result<IFunctionalSet<T>>() != null);

      return null;
    }

    public IFunctionalSet<T> Union(IFunctionalSet<T> that)
    {
      return null;
    }

    public void Visit(Action<T> visitor)
    {
      Contract.Requires(visitor != null);
    }

    public int Count
    {
      get 
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return 0;
      }
    }

    public IEnumerable<T> Elements
    {
      get 
      {
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

        return null;
      }
    }

    public void Dump(TextWriter tw)
    {

    }

    #endregion
  }

  public class FunctionalSet<T> : IFunctionalSet<T> where T : IEquatable<T>
  {
    // representation is a function intkeymap
    IFunctionalMap<T,Unit> rep;

    /// <summary>
    /// Optimized set using functional maps based on the element numbering provided.
    /// </summary>
    public static IFunctionalSet<T> Empty(Converter<T/*!*/, int> elementNumber) {
      Contract.Ensures(Contract.Result<IFunctionalSet<T>>() != null);
      
      return new FunctionalSet<T>(FunctionalIntKeyMap<T,Unit>.Empty(elementNumber)); }

    public static IFunctionalSet<T> Empty() {
      Contract.Ensures(Contract.Result<IFunctionalSet<T>>() != null);
      
      return new FunctionalSet<T>(FunctionalMap<T, Unit>.Empty);
    }

    private FunctionalSet(IFunctionalMap<T,Unit> underlying)
    {
      this.rep = underlying;
    }



    #region IFunctionalSet<T> Members

    public IFunctionalSet<T> Add(T/*!*/ elem)
    {
      return new FunctionalSet<T>(rep.Add(elem, Unit.Value));
    }

    public IFunctionalSet<T> Remove(T/*!*/ elem)
    {
      return new FunctionalSet<T>(rep.Remove(elem));
    }

    public bool Contains(T/*!*/ elem)
    {
      return rep.Contains(elem);
    }

    public T Any { get { return this.rep.AnyKey; } }

    public void Visit(Action<T/*!*/> visitor)
    {
      rep.Visit(delegate(T/*!*/ elem, Unit dummy) { visitor(elem); return VisitStatus.ContinueVisit; });
    }

    public int Count
    {
      get { return rep.Count; }
    }

    public void Dump(TextWriter tw)
    {
      var str = new StringBuilder();

      str.Append("{");
      var first = true;
      foreach (T elem in this.rep.Keys)
      {
        if (!first)
        {
          str.Append(", ");
        }
        else
        {
          first = false;
        }
        str.AppendFormat("{0}", elem.ToString());
      }
      str.Append("}");

      tw.WriteLine(str.ToString());
    }


    public IFunctionalSet<T> Intersect(IFunctionalSet<T> that)
    {
      if (this == that) { return this; }
      IFunctionalSet<T> smaller;
      IFunctionalSet<T> larger;

      if (this.Count == 0) { return this; }
      if (this.Count < that.Count) {
        smaller = this;
        larger = that;
      }
      else {
        if (that.Count == 0) { return that; }
        smaller = that;
        larger = this;
      }
      IFunctionalSet<T> result = smaller;
      smaller.Visit(delegate(T/*!*/ elem)
      {
        if (!larger.Contains(elem)) { result = result.Remove(elem); }
      });

      return result;
    }

    public IFunctionalSet<T> Union(IFunctionalSet<T> that)
    {
      if (this == that) { return this; }
      IFunctionalSet<T> smaller;
      IFunctionalSet<T> larger;

      if (this.Count == 0) { return that; }
      if (this.Count < that.Count) {
        smaller = this;
        larger = that;
      }
      else {
        if (that.Count == 0) { return this; }
        smaller = that;
        larger = this;
      }
      IFunctionalSet<T> result = larger;
      smaller.Visit(delegate(T/*!*/ elem)
      {
        result = result.Add(elem);
      });

      return result;
    }


    public bool Contained(IFunctionalSet<T> that)
    {
      if (this.Count > that.Count) return false;

      bool result = true;
      this.rep.Visit(delegate(T/*!*/ elem, Unit dummy) { if (!that.Contains(elem)) { result = false; return VisitStatus.StopVisit; } else return VisitStatus.ContinueVisit; });
      return result;
    }
    #endregion


    public IEnumerable<T> Elements
    {
      get
      {
        return rep.Keys;
      }
    }

  }

}