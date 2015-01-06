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

// #define SET_WITH_DICTIONARY

#define SET_WITH_HASHSET

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{
  [ContractClass(typeof(IReadOnlySetContracts<>))]
  public interface IReadonlySet<T> : IEnumerable<T>
  {
    /// <summary>
    /// Returns the cardinality of the set
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Return true if element is part of the set
    /// </summary>
    bool Contains(T element);

    /// <summary>
    /// Convert all the elements in the set
    /// </summary>
    IMutableSet<U> ConvertAll<U>(Converter<T, U> converter);

    /// <summary>
    /// Set difference (this \ b).
    /// We want the result to be a fresh set
    /// </summary>
    IMutableSet<T> Difference(IEnumerable<T> b);

    /// <summary>
    /// Return the subset of elements in this set that satisfy the predicate
    /// </summary>
    IMutableSet<T> FindAll(Predicate<T> predicate);

    /// <summary>
    /// Apply the <code>action</code> to all the elements of the set
    /// </summary>
    void ForEach(Action<T> action);

    /// <summary>
    /// Does it exist an element in the set that satisfies <code>predicate</code>
    /// </summary>
    bool Exists(Predicate<T> predicate);

    /// <summary>
    /// Set intersection.
    /// We want the result to be a fresh set
    /// </summary>
    IMutableSet<T> Intersection(IReadonlySet<T> b);

    /// <summary>
    /// True iff this is a subset of <code>s</code>
    /// </summary>
    bool IsSubset(IReadonlySet<T> s);

    ///<summary>
    /// True iff the set is empty
    ///</summary>
    bool IsEmpty { get; }

    /// <summary>
    /// True iff the set contains just one element
    /// </summary>
    bool IsSingleton { get; }

    /// <returns>
    /// An element of the set.
    /// It is not removed!!!
    /// </returns>
    T PickAnElement();

    /// <summary>
    ///  Check if the predicate holds for all the elements in the set
    /// </summary>
    bool TrueForAll(Predicate<T> predicate);

    /// <summary>
    /// Set union.
    /// We want the result to be a fresh set
    /// </summary>
    IMutableSet<T> Union(IReadonlySet<T> b);
  }

  [ContractClass(typeof(IMutabSetContracts<>))]
  public interface IMutableSet<T> : IReadonlySet<T>
  {
    /// <summary>
    /// Add element to set
    /// Returns true if the element was added, false if it was present
    /// </summary>
    bool Add(T element);

    /// <summary>
    /// Add all the elements in the <code>range</code> to this set
    /// </summary>
    void AddRange(IEnumerable<T> range);
  }

  #region Contracts
  [ContractClassFor(typeof(IReadonlySet<>))]
  abstract class IReadOnlySetContracts<T> : IReadonlySet<T>
  {

    #region IReadonlySet<T> Members

    int IReadonlySet<T>.Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
   
        throw new NotImplementedException();
      }
    }

    bool IReadonlySet<T>.Contains(T element)
    {      
      throw new NotImplementedException();
    }

    IMutableSet<U> IReadonlySet<T>.ConvertAll<U>(Converter<T, U> converter)
    {
      Contract.Requires(converter != null);
      Contract.Ensures(Contract.Result<IMutableSet<U>>() != null);
      
      throw new NotImplementedException();
    }

    IMutableSet<T> IReadonlySet<T>.Difference(IEnumerable<T> b)
    {
      Contract.Requires(b != null);
      Contract.Ensures(Contract.Result<IMutableSet<T>>() != null);
      
      throw new NotImplementedException();
    }

    IMutableSet<T> IReadonlySet<T>.FindAll(Predicate<T> predicate)
    {
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<IMutableSet<T>>() != null);

      throw new NotImplementedException();
    }

    void IReadonlySet<T>.ForEach(Action<T> action)
    {
      Contract.Requires(action != null);

      throw new NotImplementedException();
    }

    bool IReadonlySet<T>.Exists(Predicate<T> predicate)
    {
      Contract.Requires(predicate != null);

      throw new NotImplementedException();
    }

    IMutableSet<T> IReadonlySet<T>.Intersection(IReadonlySet<T> b)
    {
      Contract.Requires(b != null);
      Contract.Ensures(Contract.Result<IMutableSet<T>>() != null);

      throw new NotImplementedException();
    }

    bool IReadonlySet<T>.IsSubset(IReadonlySet<T> s)
    {
      Contract.Requires(s != null);

      throw new NotImplementedException();
    }

    bool IReadonlySet<T>.IsEmpty
    {
      get {
        return default(bool);
      }
    }

    bool IReadonlySet<T>.IsSingleton
    {
      get { throw new NotImplementedException(); }
    }

    T IReadonlySet<T>.PickAnElement()
    {
      throw new NotImplementedException();
    }

    bool IReadonlySet<T>.TrueForAll(Predicate<T> predicate)
    {
      Contract.Requires(predicate != null);

      throw new NotImplementedException();
    }

    IMutableSet<T> IReadonlySet<T>.Union(IReadonlySet<T> b)
    {
      Contract.Requires(b != null);
      Contract.Ensures(Contract.Result<IMutableSet<T>>() != null);


      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable<T> Members

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {      
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
  
  [ContractClassFor(typeof(IMutableSet<>))]
  abstract class IMutabSetContracts<T> : IMutableSet<T>
  {

    #region ISet<T> Members

    bool IMutableSet<T>.Add(T element)
    {
      throw new NotImplementedException();
    }

    void IMutableSet<T>.AddRange(IEnumerable<T> range)
    {
      Contract.Requires(range != null);
      
      throw new NotImplementedException();
    }

    #endregion

    #region IReadonlySet<T> Members

    int IReadonlySet<T>.Count
    {
      get { throw new NotImplementedException(); }
    }

    bool IReadonlySet<T>.Contains(T element)
    {
      throw new NotImplementedException();
    }

    IMutableSet<U> IReadonlySet<T>.ConvertAll<U>(Converter<T, U> converter)
    {
      throw new NotImplementedException();
    }

    IMutableSet<T> IReadonlySet<T>.Difference(IEnumerable<T> b)
    {
      throw new NotImplementedException();
    }

    IMutableSet<T> IReadonlySet<T>.FindAll(Predicate<T> predicate)
    {
      throw new NotImplementedException();
    }

    void IReadonlySet<T>.ForEach(Action<T> action)
    {
      throw new NotImplementedException();
    }

    bool IReadonlySet<T>.Exists(Predicate<T> predicate)
    {
      throw new NotImplementedException();
    }

    IMutableSet<T> IReadonlySet<T>.Intersection(IReadonlySet<T> b)
    {
      throw new NotImplementedException();
    }

    bool IReadonlySet<T>.IsSubset(IReadonlySet<T> s)
    {
      throw new NotImplementedException();
    }

    bool IReadonlySet<T>.IsEmpty
    {
      get { throw new NotImplementedException(); }
    }

    bool IReadonlySet<T>.IsSingleton
    {
      get { throw new NotImplementedException(); }
    }

    T IReadonlySet<T>.PickAnElement()
    {
      throw new NotImplementedException();
    }

    bool IReadonlySet<T>.TrueForAll(Predicate<T> predicate)
    {
      throw new NotImplementedException();
    }

    IMutableSet<T> IReadonlySet<T>.Union(IReadonlySet<T> b)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable<T> Members

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion
  }
  #endregion

#if SET_WITH_SIMPLEHASHTABLE
  /// <summary>
  /// An implementation of ISet based on the BCL class Dictionary
  /// </summary>
  /// <typeparam name="T">The type of the elements of the set</typeparam>
  [Serializable]
  public class Set<T> : ISet<T>
  {
  #region Private Fields
    private struct Dummy { }
    private static readonly Dummy dummy = new Dummy();
    private SimpleHashtable<T, Dummy> data;
  #endregion

  #region Constructors
    /// <summary>
    /// Create an empty set of standard capacity
    /// </summary>
    public Set()
    {
      this.data = new SimpleHashtable<T, Dummy>();
    }

    /// <summary>
    /// Create a set of initial <code>capacity</code>
    /// </summary>
    public Set(int capacity)
    {
      this.data = new SimpleHashtable<T, Dummy>((uint) capacity);
    }

    /// <summary>
    /// Create a singleton
    /// </summary>
    public Set(T singleton)
    {
      this.data = new SimpleHashtable<T, Dummy>();
      this.Add(singleton);
    }

    /// <summary>
    /// Create a set containing the same elements than <code>original</code>
    /// </summary>
    public Set(Set<T> original)
    {
      this.data = new SimpleHashtable<T, Dummy>(original.data);
    }

    /// <summary>
    /// Create a set containing the same elements than <code>original</code>
    /// </summary>
    public Set(IEnumerable<T> original)
    {
      this.data = new SimpleHashtable<T, Dummy>();
      AddRange(original);
    }
  #endregion

    public int Count
    {
      get
      {
        return this.data.Count;
      }
    }

    public bool IsEmpty
    {
      get
      {
        return this.Count == 0;
      }
    }

    public bool IsSingleton
    {
      get
      {
        return this.Count == 1;
      }
    }

    /// <summary>
    /// Add an element to the set. 
    /// If it already exists, it does nothing
    /// </summary>
    /// <returns>true if element was NOT previously present</returns>
    public bool AddQ(T a)
    {
      if (!this.data.ContainsKey(a))
      {
        this.data.Add(a, dummy);
        return true;
      }
      return false;
    }

    public void Add(T a)
    {
      this.data[a] = dummy;
    }

    /// <summary>
    /// Add all the elements in the <code>range</code> to this set
    /// </summary>
    public void AddRange(IEnumerable<T> range)
    {
      foreach (T a in range)
        Add(a);
    }

    /// <summary>
    /// Convert all the elements of the set
    /// </summary>
    public Set<U> ConvertAll<U>(Converter<T, U> converter)
    {
      Set<U> result = new Set<U>(this.Count);
      foreach (T element in this)
      {
        result.Add(converter(element));
      }
      return result;
    }

    /// <summary>
    ///  Check if the predicate holds for all the elements in the set
    /// </summary>
    public bool TrueForAll(Predicate<T> predicate)
    {
      foreach (T element in this)
        if (!predicate(element))
          return false;
      return true;
    }

    /// <summary>
    /// Return the subset of elements in this set that satisfy the predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public Set<T> FindAll(Predicate<T> predicate)
    {
      var result = new Set<T>();
      foreach (T element in this)
        if (predicate(element))
          result.Add(element);
      return result;
    }

    /// <summary>
    /// Does it exist an element in the set that satisfies <code>predicate</code>
    /// </summary>
    public bool Exists(Predicate<T> predicate)
    {
      foreach (T element in this)
      {
        if(predicate(element))
          return true;
      }

      return false;
    }

    /// <summary>
    /// Apply the <code>action</code> to all the elements of the set
    /// </summary>
    public void ForEach(Action<T> action)
    {
      foreach (T element in this)
        action(element);
    }

    /// <returns>
    /// An element of the set.
    /// It is not removed;
    /// </returns>
    public T PickAnElement()
    {
      IEnumerator<T> e = this.data.Keys.GetEnumerator();
      e.MoveNext();
      return e.Current;
    }

    /// <summary>
    /// Remove all the elements from this set.
    /// </summary>
    public void Clear()
    {
      data.Clear();
    }

    /// <summary>
    /// True iff <code>a</code> is in the set
    /// </summary>
    public bool Contains(T a)
    {
      return data.ContainsKey(a);
    }

    /// <summary>
    /// True iff this is a subset of <code>s</code>
    /// </summary>
    public bool IsSubset(Set<T> s)
    {
      if (this.Count > s.Count)
      {
        return false;
      }

      foreach (T e in this)
      {
        if (!s.Contains(e))
        {
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// Remove the element <code>a</code> from the set
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public bool Remove(T a)
    {
      return this.data.Remove(a);
    }

    //^ [Pure]
    public IEnumerator<T>/*!*/ GetEnumerator()
    {
      return data.Keys.GetEnumerator();
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Set union in infix form
    /// </summary>
    public static Set<T> operator |(Set<T> a, Set<T> b)
    {
      var result = new Set<T>(a);
      result.AddRange(b);

      return result;
    }

    /// <summary>
    /// Set union in 
    /// </summary>
    public Set<T> Union(Set<T> b)
    {
      if (this.Count == 0)
        return b;

      if (b.Count == 0)
        return this;

      Set<T> asSet = b as Set<T>;

      if (asSet == null)
        asSet = new Set<T>(b);

      return this | asSet;
    }

    /// <summary>
    /// Set intersection in infix form
    /// </summary>
    public static Set<T> operator &(Set<T> a, Set<T> b)
    {
      Set<T> result = new Set<T>();
      foreach (T element in a)
      {
        if (b.Contains(element))
          result.Add(element);
      }
      return result;
    }

    /// <summary>
    /// Set intersection 
    /// </summary>
    public Set<T> Intersection(Set<T> b)
    {
      if (b.Count == 0)
        return b;

      if (this.Count == 0)
        return this;

      Set<T> asSet = b as Set<T>;

      if (asSet == null)
        asSet = new Set<T>(b);

      return this & asSet;
    }

    /// <summary>
    /// Set difference in infix form
    /// </summary>
    public static Set<T> operator -(Set<T> a, Set<T> b)
    {
      Set<T> result = new Set<T>();
      foreach (T element in a)
        if (!b.Contains(element))
          result.Add(element);
      return result;
    }

    /// <summary>
    /// Set difference
    /// </summary>
    public Set<T> Difference(IEnumerable<T> b)
    {
      return this - new Set<T>(b);
    }

  #region Unused methods on sets

    public static Set<T> operator ^(Set<T> a, Set<T> b)
    {
      Set<T> result = new Set<T>();
      foreach (T element in a)
        if (!b.Contains(element))
          result.Add(element);
      foreach (T element in b)
        if (!a.Contains(element))
          result.Add(element);
      return result;
    }
    public Set<T> SymmetricDifference(IEnumerable<T> b)
    {
      return this ^ new Set<T>(b);
    }

    public static Set<T> Empty
    {
      get
      {
        return new Set<T>(0);
      }
    }

    public static bool operator <=(Set<T> a, Set<T> b)
    {
      foreach (T element in a)
        if (!b.Contains(element))
          return false;
      return true;
    }
    public static bool operator <(Set<T> a, Set<T> b)
    {
      return (a.Count < b.Count) && (a <= b);
    }

    public static bool operator >(Set<T> a, Set<T> b)
    {
      return b < a;
    }
    public static bool operator >=(Set<T> a, Set<T> b)
    {
      return (b <= a);
    }

    //^ [StateIndependent]
    public override bool Equals(object/*?*/ obj)
    {
      Set<T> a = this;
      Set<T>/*?*/ b = obj as Set<T>;
      if (Object.Equals(b, null))
        return false;
      return a == b;
    }

    //^ [Confined]
    public override int GetHashCode()
    {
      int hashcode = 0;
      foreach (T element in this)
      {
        Debug.Assert(element != null, "I was not expecting a null element in the set...");
        //^ assert element != null;

        hashcode ^= element.GetHashCode();
      }
      return hashcode;
    }

  #endregion

    //^ [Pure]
    IEnumerator/*!*/ IEnumerable.GetEnumerator()
    {
      return ((IEnumerable)data.Keys).GetEnumerator();
    }

    public void CopyTo(T[]/*!*/ array, int index)
    {
      int i = index;
      foreach (var x in this)
      {
        array[i++] = x;
      }

      //this.data.Keys.CopyTo(array, index);
    }

    //^ [Confined]
    override public string/*!*/ ToString()
    {
#if DEBUG
      string res = "{";

      foreach (T e in this)
      {
        res += (e != null? e.ToString() : "<null>") + " ,";
      }
      if (res[res.Length - 1] == ',')
          res=res.Substring(0, res.Length - 2);
            
      res += "}";
#else
      string res = this.Count.ToString() + " elements";
#endif
      return res;
    }

  #region ISet<T> Members

    void ISet<T>.AddRange(IEnumerable<T> range)
    {
      this.AddRange(range);
    }

    ISet<U> ISet<T>.ConvertAll<U>(Converter<T, U> converter)
    {
      return this.ConvertAll(converter);
    }

    ISet<T> ISet<T>.Difference(IEnumerable<T> b)
    {
      return this.Difference(b);
    }

    ISet<T> ISet<T>.FindAll(Predicate<T> predicate)
    {
      return this.FindAll(predicate);
    }

    void ISet<T>.ForEach(Action<T> action)
    {
      this.ForEach(action);
    }

    bool ISet<T>.Exists(Predicate<T> predicate)
    {
      return this.Exists(predicate);
    }

    ISet<T> ISet<T>.Intersection(ISet<T> b)
    {
      Set<T> bAsSet = b as Set<T>;
      if (bAsSet != null)
        return this.Intersection(bAsSet);
      else
        return this.Intersection(new Set<T>(b));
    }

    bool ISet<T>.IsSubset(ISet<T> s)
    {
      Set<T> sAsSet = s as Set<T>;
      if (sAsSet != null)
        return this.IsSubset(sAsSet);
      else
        return this.IsSubset(new Set<T>(s));
    }

    bool ISet<T>.IsEmpty
    {
      get { return this.IsEmpty; }
    }

    bool ISet<T>.IsSingleton
    {
      get { return this.IsSingleton; }
    }

    T ISet<T>.PickAnElement()
    {
      return this.PickAnElement();
    }

    bool ISet<T>.TrueForAll(Predicate<T> predicate)
    {
      return this.TrueForAll(predicate);
    }

    ISet<T> ISet<T>.Union(ISet<T> b)
    {
      Set<T> bAsSet = b as Set<T>;
      if (bAsSet != null)
        return this.Union(bAsSet);
      else
        return this.Union(new Set<T>(b));
    }

  #endregion

  }
#endif

#if SET_WITH_HASHSET
  /// <summary>
  /// An implementation of ISet based on the System.dll class HashSet
  /// </summary>
  /// <typeparam name="T">The type of the elements of the set</typeparam>
  [ContractVerification(true)]
  [Serializable]
  public class Set<T> : IMutableSet<T>
  {
    #region Static
    static private int idCount = 0;
    #endregion

    #region Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.data != null);
    }
    #endregion

    #region Private Fields

    private readonly HashSet<T> data;
    private readonly int id; // unused?

    #endregion

    #region Constructors
    /// <summary>
    /// Create an empty set of standard capacity
    /// </summary>
    public Set()
    {
      this.data = new HashSet<T>();
      this.id = idCount++;
    }

    /// <summary>
    /// Create an empty set using given comparison
    /// </summary>
    public Set(IEqualityComparer<T> comparer)
    {
      this.data = new HashSet<T>(comparer);
      this.id = idCount++;
    }

    /// <summary>
    /// Create a set of initial <code>capacity</code>
    /// </summary>
    public Set(int capacity)
    {
      this.data = new HashSet<T>();
      this.id = idCount++;
    }

    /// <summary>
    /// Create a singleton
    /// </summary>
    public Set(T singleton)
    {
      this.data = new HashSet<T>();
      this.Add(singleton);
      this.id = idCount++;
    }

    /// <summary>
    /// Create a set containing the same elements than <code>original</code>
    /// </summary>
    public Set(Set<T> original)
    {
      Contract.Requires(original != null);

      Contract.Assume(original.data != null);

      this.data = new HashSet<T>(original.data, original.data.Comparer);
      this.id = idCount++;
    }

    /// <summary>
    /// Create a set containing the same elements as <code>original</code>
    /// </summary>
    public Set(IEnumerable<T> original)
    {
      Contract.Requires(original != null);

      this.data = new HashSet<T>();
      AddRange(original);
      this.id = idCount++;
    }

    /// <summary>
    /// Create a set containing the same elements as <code>original</code>
    /// using the given comparer
    /// </summary>
    public Set(IEnumerable<T> original, IEqualityComparer<T> comparer)
    {
      Contract.Requires(original != null);

      this.data = new HashSet<T>(comparer);
      AddRange(original);
      this.id = idCount++;
    }

    public Set(IEnumerable<T> original, IEnumerable<T> original2)
    {
      Contract.Requires(original != null);
      Contract.Requires(original2 != null);

      this.data = new HashSet<T>();
      AddRange(original);
      if (!object.ReferenceEquals(original, original2))
      {
        AddRange(original2);
      }
      this.id = idCount++;
    }

    private Set(HashSet<T> set)
    {
      Contract.Requires(set != null);

      this.data = set;
      this.id = idCount++;
    }
    #endregion

    public int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() == this.data.Count);

        return this.data.Count;
      }
    }

    public bool IsEmpty
    {
      get
      {
        return this.Count == 0;
      }
    }

    public bool IsSingleton
    {
      get
      {
        return this.Count == 1;
      }
    }

    /// <summary>
    /// Add an element to the set. 
    /// If it already exists, it does nothing
    /// </summary>
    /// <returns>true if element was NOT previously present</returns>
    public bool AddQ(T a)
    {
      if (!this.data.Contains(a))
      {
        this.data.Add(a);
        return true;
      }
      return false;
    }

    public bool Add(T a)
    {
      return this.data.Add(a);
    }

    /// <summary>
    /// Add all the elements in the <code>range</code> to this set
    /// </summary>
    public void AddRange(IEnumerable<T> range)
    {
      foreach (var a in range)
        Add(a);
    }

    public void AddRange(Set<T> set)
    {
      Contract.Requires(set != null);
      this.data.UnionWith(set.data);
    }

    /// <summary>
    /// Convert all the elements of the set
    /// </summary>
    public Set<U> ConvertAll<U>(Converter<T, U> converter)
    {
      Contract.Requires(converter != null);
      Contract.Ensures(Contract.Result<Set<U>>() != null);
      
      var result = new Set<U>(this.Count);
      foreach (var element in this)
      {
        result.Add(converter(element));
      }
      return result;
    }

    /// <summary>
    ///  Check if the predicate holds for all the elements in the set
    /// </summary>
    [Pure]
    public bool TrueForAll(Predicate<T> predicate)
    {
      Contract.Requires(predicate != null);

      foreach (T element in this)
        if (!predicate(element))
          return false;
      return true;
    }

    /// <summary>
    /// Return the subset of elements in this set that satisfy the predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public Set<T> FindAll(Predicate<T> predicate)
    {
      Contract.Requires(predicate != null);
      Contract.Ensures(Contract.Result<Set<T>>() != null);      

      return FindAllInternal(predicate, new Set<T>());
    }

    protected R FindAllInternal<R>(Predicate<T> predicate, R result)
      where R : IMutableSet<T>
    {
      Contract.Requires(predicate != null);
      Contract.Requires(result != null);

      Contract.Ensures(Contract.Result<R>() != null);      

      foreach (T element in this)
        if (predicate(element))
          result.Add(element);

      Contract.Assume(result != null);

      return result;
    }

    /// <summary>
    /// Does it exist an element in the set that satisfies <code>predicate</code>
    /// </summary>
    [Pure]
    public bool Exists(Predicate<T> predicate)
    {
      Contract.Requires(predicate != null);

      foreach (T element in this)
      {
        if (predicate(element))
          return true;
      }

      return false;
    }

    /// <summary>
    /// Apply the <code>action</code> to all the elements of the set
    /// </summary>
    public void ForEach(Action<T> action)
    {
      Contract.Requires(action != null);

      foreach (T element in this)
        action(element);
    }

    /// <returns>
    /// An element of the set.
    /// It is not removed;
    /// </returns>
    [Pure]
    public T PickAnElement()
    {
      var e = this.data.GetEnumerator();
      e.MoveNext();
      return e.Current;
    }

    /// <summary>
    /// Remove all the elements from this set.
    /// </summary>
    public void Clear()
    {
      data.Clear();
    }

    [Pure]
    public List<T> ToList()
    {
      Contract.Ensures(Contract.Result<List<T>>() != null);

      var result = new List<T>(this.Count);
      foreach (var x in this)
      {
        result.Add(x);
      }

      return result;
    }

    /// <summary>
    /// True iff <code>a</code> is in the set
    /// </summary>
    [Pure]
    public bool Contains(T a)
    {
      return data.Contains(a);
    }

    /// <summary>
    /// True iff this is a subset of <code>s</code>
    /// </summary>
    [Pure]
    public bool IsSubset(Set<T> s)
    {
      Contract.Requires(s != null);

      if (this.Count > s.Count)
      {
        return false;
      }

      return data.IsSubsetOf(s.data);
    }

    /// <summary>
    /// Remove the element <code>a</code> from the set
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public bool Remove(T a)
    {
      return this.data.Remove(a);
    }

    [Pure]
    public IEnumerator<T> GetEnumerator()
    {
      return data.GetEnumerator();
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Set union in infix form
    /// </summary>
    public static Set<T> operator |(Set<T> a, Set<T> b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);

      Contract.Ensures(Contract.Result<Set<T>>() != null);
      

      var result = new HashSet<T>(a.data);
      result.UnionWith(b.data);

      return new Set<T>(result);
    }

    /// <summary>
    /// Set union in 
    /// </summary>
    public Set<T> Union(Set<T> b)
    {
      Contract.Requires(b != null);
      Contract.Ensures(Contract.Result<Set<T>>() != null);
      
      if (this.Count == 0)
        return b;

      if (b.Count == 0)
        return this;

      return this | b;
    }

    /// <summary>
    /// Set intersection in infix form
    /// </summary>
    public static Set<T> operator &(Set<T> a, Set<T> b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);
      Contract.Ensures(Contract.Result<Set<T>>() != null);

      return a.Count <= b.Count ? IntersectionInternal(a, b) : IntersectionInternal(b, a);
    }

    private static Set<T> IntersectionInternal(Set<T> a, Set<T> b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);
      Contract.Requires(a.Count <= b.Count);

      Contract.Ensures(Contract.Result<Set<T>>() != null);
      /*
      var intersection = new Set<T>(a.Count);

      foreach (var x in a)
      {
        if (b.Contains(x))
          intersection.Add(x);
      }

      return intersection;*/
      var intersection = new HashSet<T>(a.data);
      intersection.IntersectWith(b.data);

      return new Set<T>(intersection);
    }

    /// <summary>
    /// Set intersection 
    /// </summary>
    public Set<T> Intersection(Set<T> b)
    {
      Contract.Requires(b != null);
      Contract.Ensures(Contract.Result<Set<T>>() != null);      

      if (b.Count == 0)
        return b;

      if (this.Count == 0)
        return this;

      return this & b;
    }

    public Set<T> Intersection(IEnumerable<T> xs)
    {
      Contract.Requires(xs != null);
      Contract.Ensures(Contract.Result<Set<T>>() != null);

      var result = new Set<T>();

      if (this.Count == 0)
        return result; // empty set

      foreach (var x in xs)
      {
        if (this.Contains(x))
        {
          result.Add(x);
        }
      }

      return result;
    }

    /// <summary>
    /// Set difference in infix form
    /// </summary>
    public static Set<T> operator -(Set<T> a, Set<T> b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);

      Contract.Ensures(Contract.Result<Set<T>>() != null);
      
      var result = new Set<T>();
      foreach (T element in a)
        if (!b.Contains(element))
          result.Add(element);
      return result;
    }

    /// <summary>
    /// Set difference
    /// </summary>
    public Set<T> Difference(IEnumerable<T> b)
    {
      Contract.Requires(b != null);
      Contract.Ensures(Contract.Result<Set<T>>() != null);
      
      return this - new Set<T>(b);
    }

    public Set<T> Difference(Set<T> s)
    {
      Contract.Requires(s != null);
      Contract.Ensures(Contract.Result<Set<T>>() != null);

      return this - s;
    }

    #region Unused methods on sets

    public static Set<T> operator ^(Set<T> a, Set<T> b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);

      Contract.Ensures(Contract.Result<Set<T>>() != null);

      Set<T> result = new Set<T>();
      foreach (T element in a)
        if (!b.Contains(element))
          result.Add(element);
      foreach (T element in b)
        if (!a.Contains(element))
          result.Add(element);
      return result;
    }

    public Set<T> SymmetricDifference(IEnumerable<T> b)
    {
      Contract.Requires(b != null);

      Contract.Ensures(Contract.Result<Set<T>>() != null);

      return this ^ new Set<T>(b);
    }

    public static Set<T> Empty
    {
      get
      {
        return new Set<T>(0);
      }
    }

    public static bool operator <=(Set<T> a, Set<T> b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);

      foreach (T element in a)
        if (!b.Contains(element))
          return false;
      return true;
    }

    public static bool operator <(Set<T> a, Set<T> b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);

      return (a.Count < b.Count) && (a <= b);
    }

    public static bool operator >(Set<T> a, Set<T> b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);

      return b < a;
    }
    public static bool operator >=(Set<T> a, Set<T> b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);

      return (b <= a);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      if (Object.ReferenceEquals(this, obj))
      {
        return true;
      }

      var a = this;
      var b = obj as Set<T>;

      if (Object.Equals(b, null))
      {
        return false;
      }

      if (a.Count != b.Count)
        return false;

      foreach (var el in a)
      {
        if (!b.Contains(el))
          return false;
      }

      return true;
    }

    public override int GetHashCode()
    {
      int hashcode = 0;
      foreach (var element in this.data)
      {
        Contract.Assume(element != null, "I was not expecting a null element in the set...");

        hashcode ^= element.GetHashCode();
      }
      return hashcode;
    }

    #endregion

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable)data).GetEnumerator();
    }

    public void CopyTo(T[] array, int index)
    {
      Contract.Requires(array != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index + this.Count <= array.Length);

      this.data.CopyTo(array, index);
    }

    override public string ToString()
    {
#if DEBUG
      var elems = new List<string>();

      foreach (T e in this)
      {
        elems.Add(e!= null? e.ToString() : "<null>");
      }

      // Sort it to have easier debugging
      elems.Sort();

      var res = new StringBuilder();

      int count = 0;
      foreach (var s in elems)
      {
        res.AppendFormat("{0}{1}", s, count != elems.Count - 1 ? ',' : ' ');
        count++;
      }

#else
      string res = this.Count.ToString() + " elements";
#endif
      return res.ToString();
    }

    #region ISet<T> Members

    IMutableSet<U> IReadonlySet<T>.ConvertAll<U>(Converter<T, U> converter)
    {
      return this.ConvertAll(converter);
    }

    IMutableSet<T> IReadonlySet<T>.Difference(IEnumerable<T> b)
    {
      return this.Difference(b);
    }

    IMutableSet<T> IReadonlySet<T>.FindAll(Predicate<T> predicate)
    {
      return this.FindAll(predicate);
    }

    void IReadonlySet<T>.ForEach(Action<T> action)
    {
      this.ForEach(action);
    }

    bool IReadonlySet<T>.Exists(Predicate<T> predicate)
    {
      return this.Exists(predicate);
    }

    IMutableSet<T> IReadonlySet<T>.Intersection(IReadonlySet<T> b)
    {
      Set<T> bAsSet = b as Set<T>;
      if (bAsSet != null)
        return this.Intersection(bAsSet);
      else
        return this.Intersection(new Set<T>(b));
    }

    bool IReadonlySet<T>.IsSubset(IReadonlySet<T> s)
    {
      Set<T> sAsSet = s as Set<T>;
      if (sAsSet != null)
        return this.IsSubset(sAsSet);
      else
        return this.IsSubset(new Set<T>(s));
    }

    bool IReadonlySet<T>.IsEmpty
    {
      get { return this.IsEmpty; }
    }

    bool IReadonlySet<T>.IsSingleton
    {
      get { return this.IsSingleton; }
    }

    T IReadonlySet<T>.PickAnElement()
    {
      return this.PickAnElement();
    }

    bool IReadonlySet<T>.TrueForAll(Predicate<T> predicate)
    {
      return this.TrueForAll(predicate);
    }

    IMutableSet<T> IReadonlySet<T>.Union(IReadonlySet<T> b)
    {
      Set<T> bAsSet = b as Set<T>;
      if (bAsSet != null)
        return this.Union(bAsSet);
      else
        return this.Union(new Set<T>(b));
    }

    #endregion

  }

  //[Serializable]
  //public class SetOfNonNull<T> : Set<T>
  //  where T : class
  //{
  //  public SetOfNonNull()
  //    : base()
  //  {
  //  }

  //  public SetOfNonNull(IEqualityComparer<T> comparer)
  //    : base(comparer)
  //  {
  //    Contract.Requires(comparer != null);
  //  }

  //  public SetOfNonNull(int capacity)
  //    : base(capacity)
  //  {
  //  }

  //  public SetOfNonNull(T singleton)
  //    : base(singleton)
  //  {
  //    Contract.Requires(singleton != null);
  //  }

  //  public SetOfNonNull(SetOfNonNull<T> original)
  //    : base(original)
  //  {
  //  }

  //  public SetOfNonNull(IEnumerable<T> original)
  //    : base(original)
  //  {
  //    Contract.Requires(Contract.ForAll(original, x => x != null));
  //  }

  //  new public bool AddQ(T a)
  //  {
  //    Contract.Requires(a != null);

  //    return base.AddQ(a);
  //  }

  //  new public bool Add(T a)
  //  {
  //    Contract.Requires(a != null);

  //    return base.Add(a);
  //  }

  //  new public void AddRange(IEnumerable<T> range)
  //  {
  //    Contract.Requires(Contract.ForAll(range, x => x != null));

  //    base.AddRange(range);
  //  }

  //  new public SetOfNonNull<T> FindAll(Predicate<T> predicate)
  //  {
  //    return FindAllInternal(predicate, new SetOfNonNull<T>());
  //  }

  //  new public void ForEach(Action<T> action)
  //  {
  //    foreach (T element in this)
  //    {
  //      action(element);
  //      Contract.Assume(element != null);
  //    }
  //  }

  //  new public T PickAnElement()
  //  {
  //    Contract.Ensures(Contract.Result<T>() != null);

  //    var el = base.PickAnElement();

  //    Contract.Assert(el != null);

  //    return el;
  //  }

  //  new public SetOfNonNull<T> Intersection(SetOfNonNull<T> b)
  //  {
  //    Contract.Requires(b != null);
  //    Contract.Ensures(Contract.Result<SetOfNonNull<T>>() != null);

  //    return new SetOfNonNull<T>(base.Intersection(b));
      
  //  }

  //}
#endif

#if SET_WITH_DICTIONARY
  /// <summary>
  /// An implementation of ISet based on the BCL class Dictionary
  /// </summary>
  /// <typeparam name="T">The type of the elements of the set</typeparam>
  [Serializable]
  public class Set<T> : ISet<T>
  {
    #region Private Fields
    private struct Dummy { }
    private static Dummy dummy = new Dummy();
    private Dictionary<T, Dummy> data;
    #endregion

    #region Constructors
    /// <summary>
    /// Create an empty set of standard capacity
    /// </summary>
    public Set()
    {
      this.data = new Dictionary<T, Dummy>();
    }

    /// <summary>
    /// Create a set of initial <code>capacity</code>
    /// </summary>
    public Set(int capacity)
    {
      this.data = new Dictionary<T, Dummy>(capacity);
    }

    /// <summary>
    /// Create a singleton
    /// </summary>
    public Set(T singleton)
    {
      this.data = new Dictionary<T, Dummy>();
      this.Add(singleton);
    }

    /// <summary>
    /// Create a set containing the same elements than <code>original</code>
    /// </summary>
    public Set(Set<T> original)
    {
      this.data = new Dictionary<T, Dummy>(original.data);
    }

    /// <summary>
    /// Create a set containing the same elements than <code>original</code>
    /// </summary>
    public Set(IEnumerable<T> original)
    {
      this.data = new Dictionary<T, Dummy>();
      AddRange(original);
    }
    #endregion

    public int Count
    {
      get
      {
        return this.data.Count;
      }
    }

    public bool IsEmpty
    {
      get
      {
        return this.Count == 0;
      }
    }

    public bool IsSingleton
    {
      get
      {
        return this.Count == 1;
      }
    }

    /// <summary>
    /// Add an element to the set. 
    /// If it already exists, it does nothing
    /// </summary>
    /// <returns>true if element was NOT previously present</returns>
    public bool AddQ(T a)
    {
      if (!this.data.ContainsKey(a))
      {
        this.data.Add(a, dummy);
        return true;
      }
      return false;
    }

    public void Add(T a)
    {
      this.data[a] = dummy;
    }

    /// <summary>
    /// Add all the elements in the <code>range</code> to this set
    /// </summary>
    public void AddRange(IEnumerable<T> range)
    {
      foreach (T a in range)
        Add(a);
    }

    /// <summary>
    /// Convert all the elements of the set
    /// </summary>
    public Set<U> ConvertAll<U>(Converter<T, U> converter)
    {
      Set<U> result = new Set<U>(this.Count);
      foreach (T element in this)
      {
        result.Add(converter(element));
      }
      return result;
    }

    /// <summary>
    ///  Check if the predicate holds for all the elements in the set
    /// </summary>
    public bool TrueForAll(Predicate<T> predicate)
    {
      foreach (T element in this)
        if (!predicate(element))
          return false;
      return true;
    }

    /// <summary>
    /// Return the subset of elements in this set that satisfy the predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public Set<T> FindAll(Predicate<T> predicate)
    {
      var result = new Set<T>();
      foreach (T element in this)
        if (predicate(element))
          result.Add(element);
      return result;
    }

    /// <summary>
    /// Does it exist an element in the set that satisfies <code>predicate</code>
    /// </summary>
    public bool Exists(Predicate<T> predicate)
    {
      foreach (T element in this)
      {
        if (predicate(element))
          return true;
      }

      return false;
    }

    /// <summary>
    /// Apply the <code>action</code> to all the elements of the set
    /// </summary>
    public void ForEach(Action<T> action)
    {
      foreach (T element in this)
        action(element);
    }

    /// <returns>
    /// An element of the set.
    /// It is not removed;
    /// </returns>
    public T PickAnElement()
    {
      IEnumerator<T> e = this.data.Keys.GetEnumerator();
      e.MoveNext();
      return e.Current;
    }

    /// <summary>
    /// Remove all the elements from this set.
    /// </summary>
    public void Clear()
    {
      data.Clear();
    }

    /// <summary>
    /// True iff <code>a</code> is in the set
    /// </summary>
    public bool Contains(T a)
    {
      return data.ContainsKey(a);
    }

    /// <summary>
    /// True iff this is a subset of <code>s</code>
    /// </summary>
    public bool IsSubset(Set<T> s)
    {
      if (this.Count > s.Count)
      {
        return false;
      }

      foreach (T e in this)
      {
        if (!s.Contains(e))
        {
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// Remove the element <code>a</code> from the set
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public bool Remove(T a)
    {
      return this.data.Remove(a);
    }

    //^ [Pure]
    public IEnumerator<T>/*!*/ GetEnumerator()
    {
      return data.Keys.GetEnumerator();
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Set union in infix form
    /// </summary>
    public static Set<T> operator |(Set<T> a, Set<T> b)
    {
      var result = new Set<T>(a);
      result.AddRange(b);

      return result;
    }

    /// <summary>
    /// Set union in 
    /// </summary>
    public Set<T> Union(Set<T> b)
    {
      if (this.Count == 0)
        return b;

      if (b.Count == 0)
        return this;

      Set<T> asSet = b as Set<T>;

      if (asSet == null)
        asSet = new Set<T>(b);

      return this | asSet;
    }

    /// <summary>
    /// Set intersection in infix form
    /// </summary>
    public static Set<T> operator &(Set<T> a, Set<T> b)
    {
      Set<T> result = new Set<T>();
      foreach (T element in a)
      {
        if (b.Contains(element))
          result.Add(element);
      }
      return result;
    }

    /// <summary>
    /// Set intersection 
    /// </summary>
    public Set<T> Intersection(Set<T> b)
    {
      if (b.Count == 0)
        return b;

      if (this.Count == 0)
        return this;

      Set<T> asSet = b as Set<T>;

      if (asSet == null)
        asSet = new Set<T>(b);

      return this & asSet;
    }

    /// <summary>
    /// Set difference in infix form
    /// </summary>
    public static Set<T> operator -(Set<T> a, Set<T> b)
    {
      Set<T> result = new Set<T>();
      foreach (T element in a)
        if (!b.Contains(element))
          result.Add(element);
      return result;
    }

    /// <summary>
    /// Set difference
    /// </summary>
    public Set<T> Difference(IEnumerable<T> b)
    {
      return this - new Set<T>(b);
    }

    #region Unused methods on sets

    public static Set<T> operator ^(Set<T> a, Set<T> b)
    {
      Set<T> result = new Set<T>();
      foreach (T element in a)
        if (!b.Contains(element))
          result.Add(element);
      foreach (T element in b)
        if (!a.Contains(element))
          result.Add(element);
      return result;
    }
    public Set<T> SymmetricDifference(IEnumerable<T> b)
    {
      return this ^ new Set<T>(b);
    }

    public static Set<T> Empty
    {
      get
      {
        return new Set<T>(0);
      }
    }

    public static bool operator <=(Set<T> a, Set<T> b)
    {
      foreach (T element in a)
        if (!b.Contains(element))
          return false;
      return true;
    }
    public static bool operator <(Set<T> a, Set<T> b)
    {
      return (a.Count < b.Count) && (a <= b);
    }

    public static bool operator >(Set<T> a, Set<T> b)
    {
      return b < a;
    }
    public static bool operator >=(Set<T> a, Set<T> b)
    {
      return (b <= a);
    }

    //^ [StateIndependent]
    public override bool Equals(object/*?*/ obj)
    {
      Set<T> a = this;
      Set<T>/*?*/ b = obj as Set<T>;
      if (Object.Equals(b, null))
        return false;
      return a == b;
    }

    //^ [Confined]
    public override int GetHashCode()
    {
      int hashcode = 0;
      foreach (T element in this)
      {
        Debug.Assert(element != null, "I was not expecting a null element in the set...");
        //^ assert element != null;

        hashcode ^= element.GetHashCode();
      }
      return hashcode;
    }

    #endregion

    //^ [Pure]
    IEnumerator/*!*/ IEnumerable.GetEnumerator()
    {
      return ((IEnumerable)data.Keys).GetEnumerator();
    }

    public void CopyTo(T[]/*!*/ array, int index)
    {
      this.data.Keys.CopyTo(array, index);
    }

    //^ [Confined]
    override public string/*!*/ ToString()
    {
#if DEBUG
      string res = "{";

      foreach (T e in this)
      {
        res += (e != null ? e.ToString() : "<null>") + " ,";
      }
      if (res[res.Length - 1] == ',')
        res = res.Substring(0, res.Length - 2);

      res += "}";
#else
      string res = this.Count.ToString() + " elements";
#endif
      return res;
    }

    #region ISet<T> Members

    void ISet<T>.AddRange(IEnumerable<T> range)
    {
      this.AddRange(range);
    }

    ISet<U> ISet<T>.ConvertAll<U>(Converter<T, U> converter)
    {
      return this.ConvertAll(converter);
    }

    ISet<T> ISet<T>.Difference(IEnumerable<T> b)
    {
      return this.Difference(b);
    }

    ISet<T> ISet<T>.FindAll(Predicate<T> predicate)
    {
      return this.FindAll(predicate);
    }

    void ISet<T>.ForEach(Action<T> action)
    {
      this.ForEach(action);
    }

    bool ISet<T>.Exists(Predicate<T> predicate)
    {
      return this.Exists(predicate);
    }

    ISet<T> ISet<T>.Intersection(ISet<T> b)
    {
      Set<T> bAsSet = b as Set<T>;
      if (bAsSet != null)
        return this.Intersection(bAsSet);
      else
        return this.Intersection(new Set<T>(b));
    }

    bool ISet<T>.IsSubset(ISet<T> s)
    {
      Set<T> sAsSet = s as Set<T>;
      if (sAsSet != null)
        return this.IsSubset(sAsSet);
      else
        return this.IsSubset(new Set<T>(s));
    }

    bool ISet<T>.IsEmpty
    {
      get { return this.IsEmpty; }
    }

    bool ISet<T>.IsSingleton
    {
      get { return this.IsSingleton; }
    }

    T ISet<T>.PickAnElement()
    {
      return this.PickAnElement();
    }

    bool ISet<T>.TrueForAll(Predicate<T> predicate)
    {
      return this.TrueForAll(predicate);
    }

    ISet<T> ISet<T>.Union(ISet<T> b)
    {
      Set<T> bAsSet = b as Set<T>;
      if (bAsSet != null)
        return this.Union(bAsSet);
      else
        return this.Union(new Set<T>(b));
    }

    #endregion

  }

#endif

  /// <summary>
  /// A set implemented with a list.
  /// Useful when we already know the elements in the list are not redundants
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class SetList<T> : IMutableSet<T>
  {
    readonly private List<T> elements;
    private int version;

    public SetList()
    {
      this.elements = new List<T>();
      this.version = 0;
    }

    public SetList(T el)
    {
      this.elements = new List<T>() { el };
      this.version = 0;
    }

    public SetList(IMutableSet<T> set)
    {
      Contract.Requires(set != null);

      this.elements = new List<T>(set.Count);
      foreach (var x in set)
      {
        this.elements.Add(x);
      }
      this.version = 0;
    }

    private SetList(int count)
    {
      this.elements = new List<T>(Math.Max(0, count));
      this.version = 0;
    }

    private SetList(List<T> set)
    {
      this.elements = set;
      this.version = 0;
    }
    #region ISet<T> Members

    public bool Add(T element)
    {
      if (!this.elements.Contains(element))
      {
        this.elements.Add(element);
        this.version++;

        return true;
      }

      return false;
    }

    public void AddRange(IEnumerable<T> range)
    {
      foreach (var x in range)
      {
        this.Add(x);
      }
    }

    #endregion

    #region IReadonlySet<T> Members

    public int Count
    {
      get { return this.elements.Count; }
    }

    public bool Contains(T element)
    {
      return this.elements.Exists(x => x.Equals(element));
    }

    IMutableSet<U> IReadonlySet<T>.ConvertAll<U>(Converter<T, U> converter)
    {
      return this.ConvertAll(converter);
    }

    SetList<U> ConvertAll<U>(Converter<T, U> converter)
    {
      var result = new SetList<U>(this.Count);

      foreach (var x in this.elements)
      {
        result.Add(converter(x));
      }

      return result;
    }

    IMutableSet<T> IReadonlySet<T>.Difference(IEnumerable<T> b)
    {
      return this.Difference(b);
    }

    public SetList<T> Difference(IEnumerable<T> b)
    {
      var result = new List<T>(this.elements);

      foreach (var x in b)
      {
        if (this.elements.Contains(x))
        {
          result.Remove(x);
        }
      }

      return new SetList<T>(this.elements);
    }

    public IMutableSet<T> FindAll(Predicate<T> predicate)
    {
      var result = this.elements.FindAll(predicate);

      return new SetList<T>(result);
    }

    public void ForEach(Action<T> action)
    {
      this.elements.ForEach(action);
    }

    public bool Exists(Predicate<T> predicate)
    {
      return this.elements.Exists(predicate);
    }

    IMutableSet<T> IReadonlySet<T>.Intersection(IReadonlySet<T> b)
    {
      return this.Intersection(b);
    }

    public SetList<T> Intersection(IReadonlySet<T> b)
    {
      var result = new List<T>();

      foreach (var x in this.elements)
      {
        if (b.Contains(x))
        {
          result.Add(x);
        }
      }

      return new SetList<T>(result);
    }

    public SetList<T> Intersection(SetList<T> other)
    {
      var result = new List<T>();

      var left = this;
      var right = other;

      Min(ref left, ref right);

      foreach (var x in left.elements)
      {
        if (right.Contains(x))
        {
          result.Add(x);
        }
      }

      return new SetList<T>(result);
    }

    public bool IsSubset(IReadonlySet<T> s)
    {
      foreach (var x in this.elements)
      {
        if (!s.Contains(x))
        {
          return false;
        }
      }

      return true;
    }

    public bool IsEmpty
    {
      get { return this.elements.Count == 0; }
    }

    public bool IsSingleton
    {
      get { return this.elements.Count == 1; }
    }

    public T PickAnElement()
    {
      return this.elements[0];
    }

    public bool TrueForAll(Predicate<T> predicate)
    {
      return this.elements.TrueForAll(predicate);
    }

    IMutableSet<T> IReadonlySet<T>.Union(IReadonlySet<T> b)
    {
      return this.Union(b);
    }

    public SetList<T> Union(IReadonlySet<T> b)
    {
      var result = new List<T>(this.elements);

      foreach (var x in b)
      {
        if (!result.Contains(x))
        {
          result.Add(x);
        }
      }

      return new SetList<T>(result);
    }

    #endregion

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      return this.elements.GetEnumerator();
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    #endregion

    #region Helpers
    private static void Min(ref SetList<T> left, ref SetList<T> right)
    {
      if (left.Count > right.Count)
      {
        var tmp = left;
        left = right;
        right = tmp;
      }
    }
    #endregion
  }

  public static class SetHelpers
  {
    static public void AddIfNotNull<T>(this IMutableSet<T> set, T element)
    {
      if (element != null)
        set.Add(element);
    }
  }
}
