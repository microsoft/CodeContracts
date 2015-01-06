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

//An implementation for a sparse array, to optimize

// #define TRACE_PERFORMANCE
// #define CHECKINVARIANTS

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Collections;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  /// <summary>
  /// Sparse representation for a map int -> Rational
  /// </summary>
  [ContractVerification(true)]
  [ContractClass(typeof(SparseRationalArrayContracts))]
  abstract public class SparseRationalArray
  {
    public enum Representation { Dictionary, HashTable, HashTableWithBuckets, SimpleHashTable, List, Array }

    [ThreadStatic]
    private static Representation internalRepresentation;

    /// <summary>
    /// The representation for the sparse array
    /// </summary>
    public static Representation InternalRepresentation
    {
      set
      {
        internalRepresentation = value;
      }
    }

    static SparseRationalArray()
    {
      //internalRepresentation = Representation.Dictionary;
      internalRepresentation = Representation.SimpleHashTable;
    }

    static public SparseRationalArray New(int length, Int32 defaultElement)
    {
      Contract.Requires(length >= 0);

      return New(length, Rational.For(defaultElement));
    }

    /// <summary>
    /// A fresh sparse array
    /// </summary>
    static public SparseRationalArray New(int length, Rational defaultElement)
    {
      Contract.Requires(length >= 0);
      Contract.Requires(!object.Equals(defaultElement, null));

      switch (internalRepresentation)
      {
        case Representation.Dictionary:
          return new SparseRationalArray_Dictionary(length, defaultElement);

        case Representation.HashTable:
          return new SparseRationalArray_Hashtable(length, defaultElement);

        case Representation.HashTableWithBuckets:
          return new SparseRationalArray_HashtableWithBuckets(length, defaultElement);

        case Representation.SimpleHashTable:
          return new SparseRationalArray_SimpleHashtable(length, defaultElement);

        case Representation.List:
          return new SparseRationalArray_List(length, defaultElement);

        case Representation.Array:
          return new SparseRationalArray_Array(length, defaultElement);

        default:
          throw new AbstractInterpretationException("Unknown sparse array representation " + internalRepresentation);
      }
    }

    public static String Statistics
    {
      get
      {
        switch (internalRepresentation)
        {
          case Representation.Dictionary:
            return SparseRationalArray_Dictionary.Statistics;

          case Representation.HashTable:
            return SparseRationalArray_Hashtable.Statistics;

          case Representation.HashTableWithBuckets:
            return SparseRationalArray_HashtableWithBuckets.Statistics;

          case Representation.SimpleHashTable:
            return SparseRationalArray_SimpleHashtable.Statistics;

          case Representation.List:
            return SparseRationalArray_List.Statistics;

          default:
            throw new AbstractInterpretationException("Unknown sparse array representation " + internalRepresentation);
        }
      }
    }

    protected int nonDefaultElementsCount = 0;

    virtual public int NonDefaultElementsCount
    {
      get
      {
        return this.nonDefaultElementsCount;
      }
    }

    abstract public void ExpandBy(int additionalRows);
    abstract public bool ForEachKey(Predicate<int> pred);
    abstract public IEnumerable<KeyValuePair<int, Rational>> GetElements();
    abstract public IEnumerable<int> IndexesOfNonDefaultElements { get; }
    abstract public bool IsIndexOfNonDefaultElement(int key);
    abstract public bool IsIndexOfNonDefaultElement(int key, out Rational value);
    abstract public int Length { get; }
    abstract public void ResetToDefaultElement(int index);
    abstract public void ShrinkTo(int index);
    abstract public int SmallestIndexOfNonDefaultElement { get; }
    abstract public Rational this[int index] { get; set; }
    abstract public int Count { get; }
    abstract public void Swap(int x, int y);

    abstract public void CopyFrom(SparseRationalArray original);
    abstract public void CopyFrom(SparseRationalArray original, int len);
    abstract public SparseRationalArray Duplicate();

  }

  #region Contracts for SparseRationalArray

  [ContractClassFor(typeof(SparseRationalArray))]
  abstract class SparseRationalArrayContracts : SparseRationalArray
  {
    public override void ExpandBy(int additionalRows)
    {
      Contract.Requires(additionalRows >= 0);

      throw new NotImplementedException();
    }

    public override bool ForEachKey(Predicate<int> pred)
    {
      Contract.Requires(pred != null);
      
      throw new NotImplementedException();
    }

    public override IEnumerable<KeyValuePair<int, Rational>> GetElements()
    {
      Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<int, Rational>>>() != null);

      throw new NotImplementedException();
    }

    public override IEnumerable<int> IndexesOfNonDefaultElements
    {
      get {
        Contract.Ensures(Contract.Result<IEnumerable<int>>() != null);
  
        throw new NotImplementedException(); 
      }
    }

    public override bool IsIndexOfNonDefaultElement(int key)
    {
      Contract.Requires(key >= 0);

      throw new NotImplementedException();
    }

    public override bool IsIndexOfNonDefaultElement(int key, out Rational value)
    {
      Contract.Requires(key >= 0);
      Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out value), null));

      throw new NotImplementedException();
    }

    public override int Length
    {
      get { throw new NotImplementedException(); }
    }

    public override void ResetToDefaultElement(int index)
    {
      throw new NotImplementedException();
    }

    public override void ShrinkTo(int index)
    {
      throw new NotImplementedException();
    }

    public override int SmallestIndexOfNonDefaultElement
    {
      get { throw new NotImplementedException(); }
    }

    public override Rational this[int index]
    {
      get
      {
        Contract.Ensures(!object.Equals(Contract.Result<Rational>(), null));

        return default(Rational);
      }
      set
      {
        Contract.Requires(!object.Equals(value, null));
      }
    }

    public override int Count
    {
      get { throw new NotImplementedException(); }
    }

    public override void Swap(int x, int y)
    {
      throw new NotImplementedException();
    }

    public override void CopyFrom(SparseRationalArray original)
    {
      Contract.Requires(original != null);

      throw new NotImplementedException();
    }

    public override void CopyFrom(SparseRationalArray original, int len)
    {
      Contract.Requires(original != null);
      Contract.Requires(len >= 0);

      throw new NotImplementedException();
    }

    public override SparseRationalArray Duplicate()
    {
      Contract.Ensures(Contract.Result<SparseRationalArray>() != null);

      throw new NotImplementedException();
    }
  }
  #endregion

  [ContractVerification(true)]
  [ContractClass(typeof(SparseRationalArrayContracts<>))]
  abstract public class SparseRationalArray<T>
    : SparseRationalArray
    where T : SparseRationalArray
  {
    abstract public void CopyFrom(T original);
    abstract public void CopyFrom(T original, int len);
    abstract public T DuplicateMe();

    override public void CopyFrom(SparseRationalArray original)
    {
      this.CopyFrom((T)original);
    }

    override public void CopyFrom(SparseRationalArray original, int len)
    {
      this.CopyFrom((T)original, len);
    }

    override public SparseRationalArray Duplicate()
    {
      return this.DuplicateMe();
    }

    public override bool Equals(object obj)
    {
      var right = obj as SparseRationalArray<T>;

      if (right == null)
        return false;

      if (this.Count != right.Count)
        return false;

      foreach (var pair in this.GetElements())
      {
        Contract.Assume(!object.Equals(pair.Value, null));

        if (right[pair.Key] != pair.Value)
          return false;
      }

      return true;
    }

    public override int GetHashCode()
    {
      return this.Count + this.Length;
    }
  }

  #region Contracts for  SparseRationalArray<T>

  [ContractClassFor(typeof(SparseRationalArray<>))]
  abstract class SparseRationalArrayContracts<T> : SparseRationalArray<T>
    where T : SparseRationalArray
  {
    public override void CopyFrom(T original)
    {
      Contract.Requires(original != null);

      throw new NotImplementedException();
    }

    public override void CopyFrom(T original, int len)
    {
      Contract.Requires(original != null);

      throw new NotImplementedException();
    }

    public override T DuplicateMe()
    {
      Contract.Ensures(Contract.Result<T>() != null);

      throw new NotImplementedException();
    }

    public override void ExpandBy(int additionalRows)
    {
      throw new NotImplementedException();
    }

    public override bool ForEachKey(Predicate<int> pred)
    {
      throw new NotImplementedException();
    }

    public override IEnumerable<KeyValuePair<int, Rational>> GetElements()
    {
      throw new NotImplementedException();
    }

    public override IEnumerable<int> IndexesOfNonDefaultElements
    {
      get { throw new NotImplementedException(); }
    }

    public override bool IsIndexOfNonDefaultElement(int key)
    {
      throw new NotImplementedException();
    }

    public override bool IsIndexOfNonDefaultElement(int key, out Rational value)
    {
      throw new NotImplementedException();
    }

    public override int Length
    {
      get { throw new NotImplementedException(); }
    }

    public override void ResetToDefaultElement(int index)
    {
      throw new NotImplementedException();
    }

    public override void ShrinkTo(int index)
    {
      throw new NotImplementedException();
    }

    public override int SmallestIndexOfNonDefaultElement
    {
      get { throw new NotImplementedException(); }
    }

    public override Rational this[int index]
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public override int Count
    {
      get { throw new NotImplementedException(); }
    }

    public override void Swap(int x, int y)
    {
      throw new NotImplementedException();
    }
  }

  #endregion

  [ContractVerification(false)]
  sealed class SparseRationalArray_SimpleHashtable
  : SparseRationalArray<SparseRationalArray_SimpleHashtable>
  {
    #region Performance counters
#if TRACE_PERFORMANCE
    [ThreadStatic]
    static private int MaxLength;
    [ThreadStatic]
    static private double MaxNonDefaultElements;
#endif
    #endregion

    #region Private state

    private int length;                                   // The length of the array
    private SimpleHashtable<int, Rational> data;          // The data in the array

    readonly private Rational defaultElement;             // The default element

    #endregion

    #region Constructor
    public SparseRationalArray_SimpleHashtable(int length, Rational defaultElement)
    {
      Contract.Requires(length >= 0);

      UpdateMaxLength(length);

      this.length = length;
      this.data = new SimpleHashtable<int, Rational>();
      this.defaultElement = defaultElement;
    }

    private SparseRationalArray_SimpleHashtable(SparseRationalArray_SimpleHashtable original)
    {
      this.length = original.length;
      this.data = new SimpleHashtable<int, Rational>(original.data);    // TODO: share it
      this.defaultElement = original.defaultElement;
    }

    #endregion

    override public Rational this[int index]
    {
      get
      {
        CheckBounds(index);

        Rational value;
        if (this.data.TryGetValue(index, out value))
          return value;
        else
          return defaultElement;
      }
      set
      {
        CheckBounds(index);

        // We do not store the default element explicitly
        if (value == defaultElement)
        {
          if (this.data.ContainsKey(index))
          {
            this.data.Remove(index);
            this.nonDefaultElementsCount--;
          }
        }
        else
        {
          this.data[index] = value;
          this.nonDefaultElementsCount++;

          UpdateMaxNonDefaultElements();
        }
      }
    }

    [Conditional("TRACE_PERFORMANCE")]
    private static void UpdateMaxNonDefaultElements()
    {
#if TRACE_PERFORMANCE
      MaxNonDefaultElements = Math.Max(MaxNonDefaultElements, this.data.Count);
#endif
    }

    [Conditional("TRACE_PERFORMANCE")]
    private static void UpdateMaxLength(int length)
    {
#if TRACE_PERFORMANCE
      MaxLength = Math.Max(length, MaxLength);
#endif
    }

    override public int Length
    {
      get
      {
        return this.length;
      }
    }

    /// <summary>
    /// The number of non "default" elements in the sparse array
    /// </summary>
    override public int Count
    {
      get
      {
        return this.data.Count;
      }
    }

    override public void ExpandBy(int additionalRows)
    {
      //Contract.Requires(additionalRows >= 0);

      this.length += additionalRows;
    }

    override public void ShrinkTo(int index)
    {
      //Contract.Requires(index >= 0);
      //Contract.Requires(index <= this.Length);

      if (index == this.Length)
        return;

      for (int i = index; i < this.length; i++)
      {
        this.data.Remove(i);
      }

      this.length = index;
    }

    override public void ResetToDefaultElement(int index)
    {
      this.data.Remove(index);
    }


    override public IEnumerable<KeyValuePair<int, Rational>> GetElements()
    {
     return this.data.Elements;
    }

    override public IEnumerable<int> IndexesOfNonDefaultElements
    {
      get
      {
        return data.Keys;
      }
    }

    override public bool IsIndexOfNonDefaultElement(int key)
    {
      return this.data.ContainsKey(key);
    }

    override public bool IsIndexOfNonDefaultElement(int key, out Rational value)
    {
      if (this.data.TryGetValue(key, out value))
      {
        return true;
      }
      else
      {
        value = this.defaultElement;
        return false;
      }
    }

    override public bool ForEachKey(Predicate<int> pred)
    {
      foreach (int key in this.data.Keys)
      {
        if (!pred(key))
          return false;
      }

      return true;
    }

    override public int SmallestIndexOfNonDefaultElement
    {
      get
      {
        int min = Int32.MaxValue;

        foreach (int key in this.data.Keys)
        {
          if (key < min)
            min = key;
        }

        return min == Int32.MaxValue ? -1 : min;
      }
    }

    override public void CopyFrom(SparseRationalArray_SimpleHashtable original)
    {
      foreach (var pair in original.data.Elements)
      {
        this[pair.Key] = pair.Value;
      }
    }

    override public void Swap(int x, int y)
    {
      Rational xval, yval;
      bool xb = this.data.TryGetValue(x, out xval);
      bool yb = this.data.TryGetValue(y, out yval);

      Debug.Assert(xb ? xval != defaultElement : true);
      Debug.Assert(yb ? yval != defaultElement : true);

      if (xb)
      {
        if (yb)
        { // we can swap x with y
          this.data[x] = yval;
          this.data[y] = xval;
        }
        else
        { // y has a default value
          this.ResetToDefaultElement(x);
          this.data[y] = xval;
        }
      }
      else if (yb)
      { // x has the default value
        this.data[x] = yval;
        this.ResetToDefaultElement(y);
      }
      else
      { // both have the default value

      }
    }

    /// <summary>
    /// Copies just the elements of index [0, ... len-1]
    /// </summary>
    override public void CopyFrom(SparseRationalArray_SimpleHashtable original, int len)
    {
      foreach (var index_pair in original.data.Elements)
      {
        if (index_pair.Key < len)
        {
          this[index_pair.Key] = index_pair.Value;
        }
      }
    }

    // Idea: here we can share the internal representation?
    override public SparseRationalArray_SimpleHashtable DuplicateMe()
    {
      return new SparseRationalArray_SimpleHashtable(this);
    }

    public object/*!*/ Clone()
    {
      return this.Duplicate();
    }

    new static internal String Statistics
    {
      get
      {
#if TRACE_PERFORMANCE
        string s1 = string.Format("Max length of a sparse array : {0}", MaxLength);
        string s2 = string.Format("Max # of non-default elements: {0}", MaxNonDefaultElements);

        return s1 + Environment.NewLine + s2;
#else
        return "Performance tracing is off";
#endif
      }
    }

    [Conditional("DEBUG")]
    private void CheckBounds(int i)
    {
      if (i < 0 || i >= length)
      {
        System.Diagnostics.Debugger.Break();
       // throw new ArgumentOutOfRangeException();
      }
    }

    public override string ToString()
    {
      StringBuilder str = new StringBuilder();

      foreach (var pair in this.data.Elements)
      {
        str.AppendFormat("({0},{1})", pair.Key, pair.Value);
      }

      return str.ToString();
    }
  }

  [ContractVerification(false)]
  sealed class SparseRationalArray_Dictionary 
    : SparseRationalArray<SparseRationalArray_Dictionary>
  {
    #region Performance counters

    #if TRACE_PERFORMACE
    [ThreadStatic]
    static private int MaxLength;
    [ThreadStatic]
    static private double MaxNonDefaultElements;
    #endif

    #endregion

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.length >= 0);
      Contract.Invariant(this.data != null);
      Contract.Invariant(this.map != null);
      Contract.Invariant(!object.Equals(defaultElement, null));
      Contract.Invariant(this.length == this.map.Length);
#if CHECKINVARIANTS
      Contract.Invariant(Contract.ForAll(0, this.map.Length, i => this.map[i] == this.data.ContainsKey(i)));
#endif
    }

    #region Private state

    private int length;                                   // The length of the array
    private Dictionary<int, Rational> data;               // The data in the array
    private BitArray map;

    readonly private Rational defaultElement;             // The default element

    #endregion

    #region Constructor
    public SparseRationalArray_Dictionary(int length, Rational defaultElement)
    {
      Contract.Requires(length >= 0);
      Contract.Requires(!object.Equals(defaultElement, null));

      UpdateMaxLength(length);

      this.length = length;
      this.data = new Dictionary<int, Rational>(3);
      this.map = new BitArray(length);
      this.defaultElement = defaultElement;
    }

    private SparseRationalArray_Dictionary(SparseRationalArray_Dictionary original)
    {
      Contract.Requires(original != null);

      Contract.Assume(original.length >= 0);
      Contract.Assume(!object.Equals(original.defaultElement, null));

      this.length = original.length;
      this.data = new Dictionary<int, Rational>(original.data);    // TODO: share it
      this.map = new BitArray(original.map);
      this.defaultElement = original.defaultElement;
    }

    #endregion

    public override int NonDefaultElementsCount
    {
      get
      {
        return this.data.Count;
      }
    }

    override public Rational this[int index]
    {
      get
      {
        CheckBounds(index);

        Rational value;
        if(this.map[index] && this.data.TryGetValue(index, out value))
        {
          Contract.Assume(!object.Equals(value, null));
          return value;
        }
        else
        {
          return defaultElement;
        }
      }
      set
      {
        CheckBounds(index);

        // We do not store the default element explicitly
        if (value == defaultElement)
        {
          if (this.data.ContainsKey(index))
          {
            this.map[index] = false;
            this.data.Remove(index);
          }
        }
        else
        {
          this.map[index] = true;
          this.data[index] = value;

          UpdateMaxNonDefaultElements();
        }
      }
    }

    [Conditional("TRACE_PERFORMANCE")]
    private void UpdateMaxNonDefaultElements()
    {
#if TRACE_PERFORMANCE
      MaxNonDefaultElements = Math.Max(MaxNonDefaultElements, this.data.Count);
#endif
    }

    [Conditional("TRACE_PERFORMANCE")]
    private static void UpdateMaxLength(int length)
    {
#if TRACE_PERFORMANCE
      MaxLength = Math.Max(length, MaxLength);
#endif
    }

    override public int Length
    {
      get
      {
        return this.length;
      }
    }

    /// <summary>
    /// The number of non "default" elements in the sparse array
    /// </summary>
    override public int Count
    {
      get
      {
        return this.data.Count;
      }
    }

    override public void ExpandBy(int additionalRows)
    {
      this.length += additionalRows;

      this.map.Length = this.length;
    }

    override public void ShrinkTo(int index)
    {
      if (index == this.Length || index < 0)
      {
        return;
      }

      for (int i = index; i < this.length; i++)
      {
        this.data.Remove(i);
      }

      this.length = index;

      this.map.Length = this.length;
    }

    override public void ResetToDefaultElement(int index)
    {
      this.map[index] = false;
      this.data.Remove(index);    
    }

    /// <summary>
    /// A method used to iterate on the non-default elements of the sparse array
    /// </summary>
    /// <returns></returns>
    override public IEnumerable<KeyValuePair<int, Rational>> GetElements()
    {
      return data as IEnumerable<KeyValuePair<int, Rational>>;
    }

    override public IEnumerable<int> IndexesOfNonDefaultElements
    {
      get
      {
        return data.Keys;
      }
    }

    override public bool IsIndexOfNonDefaultElement(int key)
    {
      return this.map[key];
    }

    override public bool IsIndexOfNonDefaultElement(int key, out Rational value)
    {
      if (this.map[key] && this.data.TryGetValue(key, out value))
      {
        Contract.Assume(!object.Equals(value, null));

        return true;
      }
      else
      {
        value = this.defaultElement;

        return false;
      }
    }

    override public bool ForEachKey(Predicate<int> pred)
    {
      foreach (int key in this.data.Keys)
      {
        if (!pred(key))
          return false;
      }

      return true;
    }

    override public int SmallestIndexOfNonDefaultElement
    {
      get
      {
        for (var i = 0; i < this.map.Length; i++)
        {
          if (this.map[i])
            return i;
        }

        return -1;
      }
    }

    override public void CopyFrom(SparseRationalArray_Dictionary original)
    {
      Contract.Assume(original.data != null);
      foreach (var pair in original.data)
      {
        Contract.Assume(!object.Equals(pair.Value, null));
        this[pair.Key] = pair.Value;
      }
    }

    override public void Swap(int x, int y)
    {
      if (x == y)
        return;

      Rational xval, yval;
      xval = yval = defaultElement;
      var xb = this.map[x] && this.data.TryGetValue(x, out xval);
      var yb = this.map[y] && this.data.TryGetValue(y, out yval);

      // F: Proving the assertions will require some more complex quantified invariant
      Contract.Assume(xb ? (!object.Equals(xval, null) && xval != defaultElement) : true);
      Contract.Assume(yb ? (!object.Equals(yval, null) && yval != defaultElement) : true);

      if (xb)
      {
        if (yb)
        { // we can swap x with y
          this.data[x] = yval;
          this.data[y] = xval;

          this.map[x] = this.map[y] = true; // F: needless, but makes the code clearer
        }
        else
        { // y has a default value
          this.ResetToDefaultElement(x);
          this.data[y] = xval;

          this.map[y] = true;
        }
      }
      else if (yb)
      { // x has the default value
        this.data[x] = yval;
        this.ResetToDefaultElement(y);

        this.map[x] = true;
      }
      else
      { // both have the default value

      }
    }

    /// <summary>
    /// Copies just the elements of index [0, ... len-1]
    /// </summary>
    override public void CopyFrom(SparseRationalArray_Dictionary original, int len)
    {
      Contract.Assume(original.data != null);
      foreach (var pair in original.data)
      {
        Contract.Assume(!object.Equals(pair.Value, null));
        if (pair.Key < len)
        {
          this[pair.Key] = pair.Value;
        }
      }
    }

    // Idea: here we can share the internal representation?
    override public SparseRationalArray_Dictionary DuplicateMe()
    {
      return new SparseRationalArray_Dictionary(this);
    }

    public object Clone()
    {
      return this.Duplicate();
    }

    new static internal String Statistics
    {
      get
      {
#if TRACE_PERFORMANCE
        var s1 = string.Format("Max length of a sparse array : {0}", MaxLength);
        var s2 = string.Format("Max # of non-default elements: {0}", MaxNonDefaultElements);

        return s1 + Environment.NewLine + s2;
#else
        return "Performance tracing is off";
#endif
      }
    }

    [Conditional("DEBUG")]
    private void CheckBounds(int i)
    {
      if (i < 0 || i >= length)
      {
        System.Diagnostics.Debugger.Break();
        // throw new ArgumentOutOfRangeException();
      }
    }

    public override string ToString()
    {
      var str = new StringBuilder();

      foreach (var pair in this.data)
      {
        str.AppendFormat("({0},{1})", pair.Key, pair.Value);
      }

      return str.ToString();
    }
  }

  /// <summary>
  /// A sparse array implementation using a very stupid Hashtable
  /// </summary>
  [ContractVerification(false)]
  sealed class SparseRationalArray_Hashtable
    : SparseRationalArray<SparseRationalArray_Hashtable>
  {
    #region Performance counters
#if TRACE_PERFORMANCE
    [ThreadStatic]
    static private int MaxLength;
    [ThreadStatic]
    static private int ResizeCount;
#endif
    #endregion

    #region Private state

    private int length;                               // The length of the array
    private Entry[] data;                             // The data in the array
    private int count;

    readonly private Rational defaultElement;         // The default element

    private const int Capacity = 7;

    #endregion

    #region Constructor
    public SparseRationalArray_Hashtable(int length, Rational defaultElement)
    {
      Debug.Assert(length >= 0);

#if TRACE_PERFORMANCE
      MaxLength = Math.Max(length, MaxLength);
#endif

      this.length = length;
      this.data = new Entry[Capacity];
      this.count = 0;
      this.defaultElement = defaultElement;
    }

    private SparseRationalArray_Hashtable(SparseRationalArray_Hashtable original)
    {
      original.CheckInvariant();

      this.length = original.length;
      this.data = CloneFrom(original.data);
      this.count = original.count;
      this.defaultElement = original.defaultElement;
    }

    #endregion

    override public Rational this[int index]
    {
      get
      {
        CheckBounds(index);

        Rational value;
        if (this.TryGetValue(index, out value))
          return value;
        else
          return defaultElement;
      }
      set
      {
        CheckBounds(index);

        // We do not store the default element explicitly
        if (value == defaultElement)
        {
          int indexInTheArray;
          if (this.ContainsKey(index, out indexInTheArray))
          {
            this.Remove(index, indexInTheArray);
            this.nonDefaultElementsCount--;
          }
        }
        else
        {
          this.Set(index, value);
          this.nonDefaultElementsCount++;
        }

        CheckInvariant();
      }
    }

    override public int Length
    {
      get
      {
        return this.length;
      }
    }

    /// <summary>
    /// The number of non "default" elements in the sparse array
    /// </summary>
    override public int Count
    {
      get
      {
        return this.count;
      }
    }

    override public void ExpandBy(int additionalRows)
    {
      Debug.Assert(additionalRows >= 0);

      CheckInvariant();

      this.length += additionalRows;

      CheckInvariant();
    }

    override public void ShrinkTo(int index)
    {
      Debug.Assert(index >= 0);
      Debug.Assert(index <= this.Length);

      CheckInvariant();

      if (index == this.Length)
        return;

      for (int i = index; i < this.length; i++)
      {
        this.Remove(i);
      }

      this.length = index;

      CheckInvariant();
    }

    override public void ResetToDefaultElement(int index)
    {
      CheckInvariant();

      this.Remove(index);

      CheckInvariant();
    }

    /// <summary>
    /// A method used to iterate on the non-default elements of the sparse array
    /// </summary>
    /// <returns></returns>
    override public IEnumerable<KeyValuePair<int, Rational>> GetElements()
    {
      CheckInvariant();

      int nondef = 0;
      for (int i = 0; nondef < count && i < this.data.Length; i++)
      {
        if (this.data[i] != null)
        {
          nondef++;
          yield return new KeyValuePair<int, Rational>(this.data[i].Index, this.data[i].Value);
        }
      }
    }

    override public IEnumerable<int> IndexesOfNonDefaultElements
    {
      get
      {
        CheckInvariant();

        var nondef = 0;

        for (int i = 0; nondef < this.count && i < this.data.Length; i++)
        {
          if (this.data[i] != null)
          {
            nondef++;
             yield return this.data[i].Index;
          }
        }
      }
    }

    override public bool IsIndexOfNonDefaultElement(int key)
    {
      Rational dummy;

      return this.TryGetValue(key, out dummy);
    }

    override public bool IsIndexOfNonDefaultElement(int key, out Rational value)
    {
      if (this.TryGetValue(key, out value))
      {
        return true;
      }
      else
      {
        value = this.defaultElement;
        return false;
      }
    }

    override public bool ForEachKey(Predicate<int> pred)
    {
      foreach (int key in this.GetKeys())
      {
        if (!pred(key))
          return false;
      }

      return true;
    }

    override public int SmallestIndexOfNonDefaultElement
    {
      get
      {
        int min = Int32.MaxValue;

        foreach (int key in this.GetKeys())
        {
          if (key < min)
            min = key;
        }

        return min == Int32.MaxValue ? -1 : min;
      }
    }

    override public void CopyFrom(SparseRationalArray_Hashtable original)
    {
      foreach (var index_pair in original.data)
      {
        if (index_pair != null)
        {
          this[index_pair.Index] = index_pair.Value;
        }
      }
    }

    override public void Swap(int x, int y)
    {
      Rational xval, yval;
      bool xb = this.TryGetValue(x, out xval);
      bool yb = this.TryGetValue(y, out yval);

      Debug.Assert(xb ? xval != defaultElement : true);
      Debug.Assert(yb ? yval != defaultElement : true);

      if (xb)
      {
        if (yb)
        { // we can swap x with y
          this[x] = yval;
          this[y] = xval;
        }
        else
        { // y has a default value
          this.ResetToDefaultElement(x);
          this[y] = xval;
        }
      }
      else if (yb)
      { // x has the default value
        this[x] = yval;
        this.ResetToDefaultElement(y);
      }
      else
      { // both have the default value

      }
    }

    /// <summary>
    /// Copies just the elements of index [0, ... len-1]
    /// </summary>
    override public void CopyFrom(SparseRationalArray_Hashtable original, int len)
    {
      foreach (var index_pair in original.data)
      {
        if (index_pair != null && index_pair.Index < len)
        {
          this[index_pair.Index] = index_pair.Value;
        }
      }
    }

    // Idea: here we can share the internal representation?
    override public SparseRationalArray_Hashtable DuplicateMe()
    {
      return new SparseRationalArray_Hashtable(this);
    }

    [Conditional("DEBUG")]
    [Conditional("CHECKINVARIANTS")]
    private void CheckBounds(int i)
    {
      CheckInvariant();

      if (i < 0 || i >= length)
        throw new ArgumentOutOfRangeException();
    }

    new static internal String Statistics
    {
      get
      {
#if TRACE_PERFORMANCE
        return string.Format("Number of resizing {0}", ResizeCount);
#else
        return "Performance tracing is off";
#endif
      }
    }

    override public string ToString()
    {
      StringBuilder str = new StringBuilder();

      foreach (var pair in this.data)
      {
        if (pair != null)
        {
          str.AppendFormat("({0},{1})", pair.Index, pair.Value);
        }
      }

      return str.ToString();
    }

    [Conditional("CHECKINVARIANTS")]
    private void CheckInvariant()
    {
      var indexes = new Set<int>();

      foreach (var pair in this.data)
      {
        if (pair != null)
        {
          Debug.Assert(!indexes.Contains(pair.Index));

          indexes.Add(pair.Index);
        }
      }
    }

    #region HashTable Manipulation

    static private int Hash(int value)
    {
      // return value.GetHashCode();
      return value;
    }

    private int Pos(int value)
    {
      return value % this.data.Length;
    }

    static private Entry[] CloneFrom(Entry[] entry)
    {
      Entry[] result = new Entry[entry.Length];
      Array.Copy(entry, result, entry.Length);

      return result;
    }

    private bool TryGetValue(int index, out int indexInTheArray, out Rational value)
    {
      int start = Pos(Hash(index));

      if (this.data[start] != null && this.data[start].Index == index)
      {
        value = this.data[start].Value;
        indexInTheArray = start;
        return true;
      }

      for (int i = Pos(start + 1); i != start; i = Pos(i + 1))
      {
        if (this.data[i] != null && this.data[i].Index == index)
        {
          value = this.data[i].Value;
          indexInTheArray = i;
          return true;
        }
      }

      value = default(Rational);
      indexInTheArray = -1;
      return false;
    }

    private bool TryGetValue(int index, out Rational value)
    {
      int indexInTheArray;
      return this.TryGetValue(index, out indexInTheArray, out value);
    }

    private bool ContainsKey(int index, out int indexInTheArray)
    {
      Rational dummy;
      return this.TryGetValue(index, out indexInTheArray, out dummy);
    }

    private void Remove(int index)
    {
      int indexInTheArray;
      Rational dummy;

      if (this.TryGetValue(index, out indexInTheArray, out dummy))
      {
        this.Remove(index, indexInTheArray);
      }
    }

    private void Remove(int index, int indexInTheArray)
    {
      this.data[indexInTheArray] = null;
    }

    private void Set(int index, Rational value)
    {
      if (this.data.Length == this.count)
      {
        Resize(this.data.Length * 2 + 1);
      }

      this.Remove(index);

      Set(this.data, index, value);

      this.count++;
    }

    static private void Set(Entry[] where, int index, Rational value)
    {
      int len = where.Length;
      int start = Hash(index) % len;

      if (where[start] == null || where[start].Index == index)
      {
        where[start] = new Entry(index, value);
        return;
      }

      for (int i = (start + 1) % len; i != start; i = (i + 1) % len)
      {
        if (where[i] == null || where[i].Index == index)
        {
          where[i] = new Entry(index, value);
          return;
        }
      }

      Debug.Assert(false);  // Should be unreachable
    }

    private void Resize(int newSize)
    {
#if TRACE_PERFORMANCE
      ResizeCount++;
#endif

      Entry[] newArray = new Entry[newSize];

      for (int i = 0; i < this.data.Length; i++)
      {
        if (this.data[i] != null)
        {
          Set(newArray, this.data[i].Index, this.data[i].Value);
        }
      }

      this.data = newArray;
    }

    private IEnumerable<int> GetKeys()
    {
      int nondef = 0;

      for (int i = 0; nondef < count && i < this.data.Length; i++)
      {
        if (this.data[i] != null)
        {
          nondef++;
          yield return this.data[i].Index;
        }
      }
    }

    #endregion

    class Entry
    {
      int index;
      Rational value;

      public int Index
      {
        get
        {
          return this.index;
        }
      }

      public Rational Value
      {
        get
        {
          return this.value;
        }
      }

      public Entry(int index, Rational value)
      {
        this.index = index;
        this.value = value;
      }

      public override string ToString()
      {
        return string.Format("({0}, {1})", this.index, this.value);
      }
    }
  }

  [ContractVerification(false)]
  sealed class SparseRationalArray_HashtableWithBuckets
    : SparseRationalArray<SparseRationalArray_HashtableWithBuckets>
  {
    #region Performance counters
#if TRACE_PERFORMANCE
    [ThreadStatic]
    static private int MaxLength;
    [ThreadStatic]
    static private int ResizeCount;
#endif
    #endregion

    #region Private state

    private int length;                               // The length of the array
    private Entry[][] data;                           // The data in the array    
    private int[] lastElement;                        // The last element in a bucket

    private int count;

    readonly private Rational defaultElement;         // The default element

    private const int Capacity = 31;
    private const int BucketSize = 1;

    #endregion

    #region Constructor
    public SparseRationalArray_HashtableWithBuckets(int length, Rational defaultElement)
    {
      Debug.Assert(length >= 0);

#if TRACE_PERFORMANCE
      MaxLength = Math.Max(length, MaxLength);
#endif

      this.length = length;
      
      this.data = new Entry[Capacity][];
      this.lastElement = new int[Capacity];
     
      this.count = 0;
      this.defaultElement = defaultElement;
    }

    private SparseRationalArray_HashtableWithBuckets(SparseRationalArray_HashtableWithBuckets original)
    {
      original.CheckInvariant();

      this.length = original.length;
      this.data = CloneFrom(original.data);
      
      this.lastElement = new int[original.lastElement.Length];
      Array.Copy(original.lastElement, this.lastElement, original.lastElement.Length);

      this.count = original.count;
      this.defaultElement = original.defaultElement;
    }

    #endregion

    override public Rational this[int index]
    {
      get
      {
        CheckBounds(index);

        Rational value;
        if (this.TryGetValue(index, out value))
          return value;
        else
          return defaultElement;
      }
      set
      {
        CheckBounds(index);

        // We do not store the default element explicitly
        if (value == defaultElement)
        {
          int indexInTheArray, indexInTheBucket;
          if (this.ContainsKey(index, out indexInTheArray, out indexInTheBucket))
          {
            this.Remove(index, indexInTheArray, indexInTheBucket);
            this.nonDefaultElementsCount--;
          }
        }
        else
        {
          this.Set(index, value);
          this.nonDefaultElementsCount++;
        }

        CheckInvariant();
      }
    }

    override public int Length
    {
      get
      {
        return this.length;
      }
    }

    /// <summary>
    /// The number of non "default" elements in the sparse array
    /// </summary>
    override public int Count
    {
      get
      {
        return this.count;
      }
    }

    override public void ExpandBy(int additionalRows)
    {
      Debug.Assert(additionalRows >= 0);

      CheckInvariant();

      this.length += additionalRows;

      CheckInvariant();
    }

    override public void ShrinkTo(int index)
    {
      Debug.Assert(index >= 0);
      Debug.Assert(index <= this.Length);

      CheckInvariant();

      if (index == this.Length)
        return;

      for (int i = index; i < this.length; i++)
      {
        this.Remove(i);
      }

      this.length = index;

      CheckInvariant();
    }

    override public void ResetToDefaultElement(int index)
    {
      CheckInvariant();

      this.Remove(index);

      CheckInvariant();
    }

    /// <summary>
    /// A method used to iterate on the non-default elements of the sparse array
    /// </summary>
    /// <returns></returns>
    override public IEnumerable<KeyValuePair<int, Rational>> GetElements()
    {
      CheckInvariant();

      var nondef = 0;
      for (int i = 0; nondef < this.count && i < this.data.Length; i++)
      {
        if (this.data[i] != null)
        {
          for (int j = 0; j < this.lastElement[i]; j++)
          {
            var local = this.data[i][j];
            nondef++;

            yield return new KeyValuePair<int, Rational>(local.Index, local.Value);
          }
        }
      }
    }

    override public IEnumerable<int> IndexesOfNonDefaultElements
    {
      get
      {
        CheckInvariant();

        var nondef = 0;

        for (int i = 0; nondef < this.count && i < this.data.Length; i++)
        {
          if (this.data[i] != null)
          {
            for (int j = 0; j < this.lastElement[i]; j++)
            {
              nondef++;
               yield return this.data[i][j].Index;
            }
          }
        }
      }
    }

    override public bool IsIndexOfNonDefaultElement(int key)
    {
      Rational dummy;

      return this.TryGetValue(key, out dummy);
    }

    override public bool IsIndexOfNonDefaultElement(int key, out Rational value)
    {
      if (this.TryGetValue(key, out value))
      {
        return true;
      }
      else
      {
        value = this.defaultElement;
        return false;
      }
    }

    override public bool ForEachKey(Predicate<int> pred)
    {
      foreach (int key in this.GetKeys())
      {
        if (!pred(key))
          return false;
      }

      return true;
    }

    override public int SmallestIndexOfNonDefaultElement
    {
      get
      {
        int min = Int32.MaxValue;

        foreach (int key in this.GetKeys())
        {
          if (key < min)
            min = key;
        }

        return min == Int32.MaxValue ? -1 : min;
      }
    }

    override public void CopyFrom(SparseRationalArray_HashtableWithBuckets original)
    {
      foreach (var index_pair in original.GetElements())
      {
        this[index_pair.Key] = index_pair.Value;
      }
    }

    override public void Swap(int x, int y)
    {
      Rational xval, yval;
      bool xb = this.TryGetValue(x, out xval);
      bool yb = this.TryGetValue(y, out yval);

      Debug.Assert(xb ? xval != defaultElement : true);
      Debug.Assert(yb ? yval != defaultElement : true);

      if (xb)
      {
        if (yb)
        { // we can swap x with y
          this[x] = yval;
          this[y] = xval;
        }
        else
        { // y has a default value
          this.ResetToDefaultElement(x);
          this[y] = xval;
        }
      }
      else if (yb)
      { // x has the default value
        this[x] = yval;
        this.ResetToDefaultElement(y);
      }
      else
      { // both have the default value

      }
    }

    /// <summary>
    /// Copies just the elements of index [0, ... len-1]
    /// </summary>
    override public void CopyFrom(SparseRationalArray_HashtableWithBuckets original, int len)
    {
      foreach (var index_pair in original.GetElements())
      {
        if (index_pair.Key < len)
        {
          this[index_pair.Key] = index_pair.Value;
        }
      }
    }

    // Idea: here we can share the internal representation?
    override public SparseRationalArray_HashtableWithBuckets DuplicateMe()
    {
      return new SparseRationalArray_HashtableWithBuckets(this);
    }

    [Conditional("DEBUG")]
    [Conditional("CHECKINVARIANTS")]
    private void CheckBounds(int i)
    {
      CheckInvariant();

      if (i < 0 || i >= length)
        throw new ArgumentOutOfRangeException();
    }

    new static internal String Statistics
    {
      get
      {
#if TRACE_PERFORMANCE
        StringBuilder result = new StringBuilder();
        result.AppendFormat("Resize count {0} " + Environment.NewLine, ResizeCount);

        return result.ToString();
#else
        return "Performance tracing is off";
#endif
      }
    }

    override public string ToString()
    {
      StringBuilder str = new StringBuilder();

      for(int i = 0; i < this.data.Length; i++)
      {
        if (this.data[i] != null)
        {
          for (int j = 0; j < this.lastElement[i]; i++)
          {
            str.AppendFormat("({0},{1})", this.data[i][j].Index, this.data[i][j].Value);
          }
        }
      }

      return str.ToString();
    }

    [Conditional("CHECKINVARIANTS")]
    private void CheckInvariant()
    {
      var indexes = new Set<int>();

      Contract.Assert(Contract.ForAll(this.lastElement, i => i >= 0));

      for(int i = 0; i< this.data.Length; i++)
      {
        Contract.Assert(this.data[i] != null || this.lastElement[i] == 0);

        if (this.data[i] != null)
        {
          for (int j = 0; j < this.lastElement[i]; j++)
          {
            Contract.Assert(this.data[i][j] != null);
            Contract.Assert(!indexes.Contains(this.data[i][j].Index));

            indexes.Add(this.data[i][j].Index);
          }

          for (int j = this.lastElement[i]; j < this.data[i].Length; j++)
          {
            Contract.Assert(this.data[i][j] == null);
          }
        }
      }
    }

    #region HashTable Manipulation

    static private int Hash(int value)
    {
      return value % Capacity;
    }

    private int Pos(int value)
    {
      return value % this.data.Length;
    }

    static private Entry[][] CloneFrom(Entry[][] entry)
    {
      Entry[][] result = new Entry[entry.Length][];

      for (int i = 0; i < entry.Length; i++)
      {
        if (entry[i] != null)
        {
          result[i] = new Entry[entry[i].Length];
          Array.Copy(entry[i], result[i], entry[i].Length);
        }
      }
      return result;
    }

    private bool TryGetValue(int index, out int indexInTheArray, out int indexInTheBucket, out Rational value)
    {
      int start = Pos(Hash(index));

      if (this.data[start] == null)
      {
        indexInTheArray = -1;
        indexInTheBucket = -1;
        value = default(Rational);
      
        return false;
      }

      for (int i = 0; i < this.lastElement[start]; i++)
      {
        var local = this.data[start][i];
        if (local.Index == index)
        {
          indexInTheArray = start;
          indexInTheBucket = i;
          value = local.Value;

          return true;
        }
      }

      value = default(Rational);
      indexInTheArray = -1;
      indexInTheBucket = -1;
    
      return false;
    }

    private bool TryGetValue(int index, out Rational value)
    {
      int dummy1, dummy2;
      return this.TryGetValue(index, out dummy1, out dummy2, out value);
    }

    private bool ContainsKey(int index, out int indexInTheArray, out int indexInTheBucket)
    {
      Rational dummy;
      return this.TryGetValue(index, out indexInTheArray, out indexInTheBucket, out dummy);
    }

    private void Remove(int index)
    {
      int indexInTheArray, indexInTheBucket;
      Rational dummy;

      if (this.TryGetValue(index, out indexInTheArray, out indexInTheBucket, out dummy))
      {
        this.Remove(index, indexInTheArray, indexInTheBucket);
      }
    }

    private void Remove(int index, int indexInTheArray, int indexInTheBucket)
    {
      CheckInvariant();

      var lastElement = this.lastElement[indexInTheArray];
      if (indexInTheBucket == 0 && lastElement == 1)
      {
        // The last element ...
        this.data[indexInTheArray] = null;        
      }
      else
      {
        Swap(ref this.data[indexInTheArray][indexInTheBucket], ref this.data[indexInTheArray][lastElement-1]);

        this.data[indexInTheArray][lastElement-1] = null;
      }

      this.count--;
      this.lastElement[indexInTheArray]--;

      CheckInvariant();
    }

    private void Swap(ref Entry entry1, ref Entry entry2)
    {
      Entry tmp = entry1;
      entry1 = entry2;
      entry2 = tmp;
    }

    private void Set(int index, Rational value)
    {
      int indexInTheArray, indexInTheBucket;
      if (this.ContainsKey(index, out indexInTheArray, out indexInTheBucket))
      {
        this.data[indexInTheArray][indexInTheBucket] = new Entry(index, value);
      }
      else
      {
        indexInTheArray = Pos(Hash(index));

        var local = this.data[indexInTheArray];

        if (local == null)
        {
          this.data[indexInTheArray] = new Entry[BucketSize];
        }
        else if (this.lastElement[indexInTheArray] == local.Length)
        {
#if TRACE_PERFORMANCE
          ResizeCount++;
#endif
          if (local.Length > 1)
          {
            Console.WriteLine("Resize from {0} to {1}", local.Length, local.Length * 2 + 1);
          }

          Array.Resize(ref this.data[indexInTheArray], this.data[indexInTheArray].Length * 2 +1);
        }

        this.data[indexInTheArray][this.lastElement[indexInTheArray]] = new Entry(index, value);

        this.lastElement[indexInTheArray]++;
      }

      this.count++;
    }



    private IEnumerable<int> GetKeys()
    {
      int nondef = 0;

      for (int i = 0; nondef < count && i < this.data.Length; i++)
      {
        if (this.data[i] != null)
        {
          for (int j = 0; j < this.lastElement[i]; j++)
          {
            nondef++;
            yield return this.data[i][j].Index;
          }
        }
      }
    }

    #endregion

    class Entry
    {
      int index;
      Rational value;

      public int Index
      {
        get
        {
          return this.index;
        }
      }

      public Rational Value
      {
        get
        {
          return this.value;
        }
      }

      public Entry(int index, Rational value)
      {
        this.index = index;
        this.value = value;
      }

      public override string ToString()
      {
        return string.Format("({0}, {1})", this.index, this.value);
      }
    }
  }

  [ContractVerification(false)]
  sealed class SparseRationalArray_Array
    : SparseRationalArray<SparseRationalArray_Array>
  {
    #region Performance counters

#if TRACE_PERFORMACE
    [ThreadStatic]
    static private int MaxLength;
    [ThreadStatic]
    static private double MaxNonDefaultElements;
#endif

    #endregion

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.length >= 0);
      Contract.Invariant(this.map != null);
      Contract.Invariant(!object.Equals(defaultElement, null));
      Contract.Invariant(this.length == this.map.Length);
#if CHECKINVARIANTS
      Contract.Invariant(Contract.ForAll(0, this.map.Length, i => this.map[i] == this.data.ContainsKey(i)));
#endif
    }

    #region Private state

    private int length;                                   // The length of the array
    private Tuple<int, Rational>[] data;               // The data in the array
    private BitArray map;

    readonly private Rational defaultElement;             // The default element

    #endregion

    #region Constructor
    public SparseRationalArray_Array(int length, Rational defaultElement)
    {
      Contract.Requires(length >= 0);
      Contract.Requires(!object.Equals(defaultElement, null));

      UpdateMaxLength(length);

      this.length = length;
      this.data = null;
      this.map = new BitArray(length);
      this.defaultElement = defaultElement;
    }

    private SparseRationalArray_Array(SparseRationalArray_Array original)
    {
      Contract.Requires(original != null);

      Contract.Assume(original.length >= 0);
      Contract.Assume(!object.Equals(original.defaultElement, null));

      this.length = original.length;
      this.data = original.data.Duplicate();
      this.map = new BitArray(original.map);
      this.defaultElement = original.defaultElement;
    }

    #endregion

    public override int NonDefaultElementsCount
    {
      get
      {
        return this.data.CountNotNull();
      }
    }

    override public Rational this[int index]
    {
      get
      {
        CheckBounds(index);

        Rational value;
        if (this.map[index] && this.data.TryGetValue(index, out value))
        {
          Contract.Assume(!object.Equals(value, null));
          return value;
        }
        else
        {
          return defaultElement;
        }
      }
      set
      {
        CheckBounds(index);

        // We do not store the default element explicitly
        if (value == defaultElement)
        {
          if (this.data.ContainsKey(index))
          {
            this.map[index] = false;
            this.data.Remove(index);
          }
        }
        else
        {
          this.map[index] = true;
          //this.data[index] = value;

          this.data = this.data.Add(index, value);

          UpdateMaxNonDefaultElements();
        }
      }
    }

    [Conditional("TRACE_PERFORMANCE")]
    private void UpdateMaxNonDefaultElements()
    {
#if TRACE_PERFORMANCE
      MaxNonDefaultElements = Math.Max(MaxNonDefaultElements, this.data.Count);
#endif
    }

    [Conditional("TRACE_PERFORMANCE")]
    private static void UpdateMaxLength(int length)
    {
#if TRACE_PERFORMANCE
      MaxLength = Math.Max(length, MaxLength);
#endif
    }

    override public int Length
    {
      get
      {
        return this.length;
      }
    }

    /// <summary>
    /// The number of non "default" elements in the sparse array
    /// </summary>
    override public int Count
    {
      get
      {
        return this.data.CountNotNull();
      }
    }

    override public void ExpandBy(int additionalRows)
    {
      this.length += additionalRows;

      this.map.Length = this.length;
    }

    override public void ShrinkTo(int index)
    {
      if (index == this.Length || index < 0)
      {
        return;
      }

      for (int i = index; i < this.length; i++)
      {
        this.data.Remove(i);
      }

      this.length = index;

      this.map.Length = this.length;
    }

    override public void ResetToDefaultElement(int index)
    {
      this.map[index] = false;
      this.data.Remove(index);
    }

    /// <summary>
    /// A method used to iterate on the non-default elements of the sparse array
    /// </summary>
    /// <returns></returns>
    override public IEnumerable<KeyValuePair<int, Rational>> GetElements()
    {
      if (this.data == null)
        yield break;

      foreach (var x in this.data)
      {
        if (x != null)
          yield return new KeyValuePair<int, Rational>(x.Item1, x.Item2);
      }
    }

    override public IEnumerable<int> IndexesOfNonDefaultElements
    {
      get
      {
        if (this.data == null)
          yield break;

        foreach (var x in this.data)
        {
          if (x != null)
            yield return x.Item1;
        }
      }
    }

    override public bool IsIndexOfNonDefaultElement(int key)
    {
      return this.map[key];
    }

    override public bool IsIndexOfNonDefaultElement(int key, out Rational value)
    {
      if (this.map[key] && this.data.TryGetValue(key, out value))
      {
        Contract.Assume(!object.Equals(value, null));

        return true;
      }
      else
      {
        value = this.defaultElement;

        return false;
      }
    }

    override public bool ForEachKey(Predicate<int> pred)
    {
      foreach (var key in this.IndexesOfNonDefaultElements)
      {
        if (!pred(key))
          return false;
      }

      return true;
    }

    override public int SmallestIndexOfNonDefaultElement
    {
      get
      {
        for (var i = 0; i < this.map.Length; i++)
        {
          if (this.map[i])
            return i;
        }

        return -1;
      }
    }

    override public void CopyFrom(SparseRationalArray_Array original)
    {
      if (original.data == null)
      {
        this.data = null;
        return;
      }
      foreach (var pair in original.data)
      {
        if (pair != null)
        {
          this[pair.Item1] = pair.Item2;
        }
      }
    }

    override public void Swap(int x, int y)
    {
      if (x == y)
        return;

      Rational xval, yval;
      xval = yval = defaultElement;
      var xb = this.map[x] && this.data.TryGetValue(x, out xval);
      var yb = this.map[y] && this.data.TryGetValue(y, out yval);

      // F: Proving the assertions will require some more complex quantified invariant
      Contract.Assume(xb ? (!object.Equals(xval, null) && xval != defaultElement) : true);
      Contract.Assume(yb ? (!object.Equals(yval, null) && yval != defaultElement) : true);

      if (xb)
      {
        if (yb)
        { // we can swap x with y
          this[x] = yval;
          this[y] = xval;

          this.map[x] = this.map[y] = true; // F: needless, but makes the code clearer
        }
        else
        { // y has a default value
          this.ResetToDefaultElement(x);
          this[y] = xval;

          this.map[y] = true;
        }
      }
      else if (yb)
      { // x has the default value
        this[x] = yval;
        this.ResetToDefaultElement(y);

        this.map[x] = true;
      }
      else
      { // both have the default value

      }
    }

    /// <summary>
    /// Copies just the elements of index [0, ... len-1]
    /// </summary>
    override public void CopyFrom(SparseRationalArray_Array original, int len)
    {
      if (original.data == null)
      {
        this.data = null;
        return;
      }
      foreach (var pair in original.data)
      {
        if (pair != null)
        {
          if (pair.Item1 < len)
          {
            this[pair.Item1] = pair.Item2;
          }
        }
      }
    }

    // Idea: here we can share the internal representation?
    override public SparseRationalArray_Array DuplicateMe()
    {
      return new SparseRationalArray_Array(this);
    }

    public object Clone()
    {
      return this.Duplicate();
    }

    new static internal String Statistics
    {
      get
      {
#if TRACE_PERFORMANCE
        var s1 = string.Format("Max length of a sparse array : {0}", MaxLength);
        var s2 = string.Format("Max # of non-default elements: {0}", MaxNonDefaultElements);

        return s1 + Environment.NewLine + s2;
#else
        return "Performance tracing is off";
#endif
      }
    }

    [Conditional("DEBUG")]
    private void CheckBounds(int i)
    {
      if (i < 0 || i >= length)
      {
        System.Diagnostics.Debugger.Break();
        // throw new ArgumentOutOfRangeException();
      }
    }

    public override string ToString()
    {
      if (this.data == null)
        return "empty sparse array";

      var str = new StringBuilder();

      foreach (var pair in this.data)
      {
        if(pair != null)
          str.AppendFormat("({0},{1})", pair.Item1, pair.Item2);
      }

      return str.ToString();
    }
  }

  // Still some bug, it does not seem it gives performance gains
  sealed class SparseRationalArray_List
    : SparseRationalArray<SparseRationalArray_List>
  {
    private int length;                                   // The length of the array
    private List<Pair<int, Rational>> data;          // The data in the array

    readonly private Rational defaultElement;             // The default element

    public SparseRationalArray_List(int length, Rational defaultElement)
    {
      this.length = length;
      this.data = new List<Pair<int, Rational>>();
      this.defaultElement = defaultElement;
    }

    private SparseRationalArray_List(SparseRationalArray_List other)
    {
      this.length = other.length;
      this.data = new List<Pair<int, Rational>>(other.data);
      this.defaultElement = other.defaultElement;
      this.nonDefaultElementsCount = other.nonDefaultElementsCount;
    }

    private void Set(Pair<int, Rational> value)
    {
      for (var i = 0; i < this.data.Count; i++)
      {
        if (data[i].One == value.One)
        {
          if (value.Two == defaultElement)          
          {
            data.RemoveAt(i);
            nonDefaultElementsCount--;
          }
          else
          {
            data[i] = value;
            nonDefaultElementsCount++;
            return;
          }
        }
      }

      if (value.Two != defaultElement)
      {
        this.data.Add(value);
        nonDefaultElementsCount++;
      }
    }

    public override void CopyFrom(SparseRationalArray_List original)
    {
      foreach (var pair in original.data)
      {
        this.Set(pair);
      }
    }

    public override void CopyFrom(SparseRationalArray_List original, int len)
    {
      foreach (var pair in original.data)
      {
        if (pair.One < len)
        {
          this.Set(pair);
        }
      }
    }

    public override SparseRationalArray_List DuplicateMe()
    {
      return new SparseRationalArray_List(this);
    }

    public override void ExpandBy(int additionalRows)
    {
      this.length += additionalRows;
    }

    public override bool ForEachKey(Predicate<int> pred)
    {
      foreach (var pair in this.data)
      {
        if (!pred(pair.One))
          return false;
      }

      return true;
    }

    public override IEnumerable<KeyValuePair<int, Rational>> GetElements()
    {
      foreach(var pair in this.data)
      {
        yield return new KeyValuePair<int, Rational>(pair.One, pair.Two);
      }
    }

    public override IEnumerable<int> IndexesOfNonDefaultElements
    {
      get 
      {
        foreach (var x in this.data)
          yield return x.One;
      }
    }

    public override bool IsIndexOfNonDefaultElement(int key)
    {
      foreach (var pair in this.data)
      {
        if (pair.One == key)
          return true;
      }

      return false;
    }

    public override bool IsIndexOfNonDefaultElement(int key, out Rational value)
    {
      foreach (var pair in this.data)
      {
        if (pair.One == key)
        {
          value = pair.Two;
          return true;
        }
      }

      value = defaultElement;
      return false;
    }

    public override int Length
    {
      get { return this.length; }
    }

    public override void ResetToDefaultElement(int index)
    {
      for (var i = 0; i < this.data.Count; i++)
      {
        if (data[i].One == index)
        {
          this.data.RemoveAt(i);
          this.nonDefaultElementsCount--;

          return;
        }
      }
    }

    public override void ShrinkTo(int index)
    {
      //Contract.Requires(index >= 0);
      //Contract.Requires(index <= this.Length);

      if (index == this.Length)
      {
        return;
      }

      for(var i = 0; i < this.data.Count; i++)
      {
        if(this.data[i].One >= index)
        {
          this.data.RemoveAt(i);
          i--;
        }
      }

      this.length = index;
    }

    public override int SmallestIndexOfNonDefaultElement
    {
      get 
      {
        var min = Int32.MaxValue;

        foreach (var pair in this.data)
        {
          if (pair.One < min)
            min = pair.One;
        }

        return min == Int32.MaxValue ? -1 : min;
      }
    }

    public override Rational this[int index]
    {
      get
      {
        foreach (var pair in this.data)
        {
          if (pair.One == index)
            return pair.Two;
        }

        return defaultElement;
      }
      set
      {
        Set(new Pair<int, Rational>(index, value));
      }
    }

    public override int Count
    {
      get { return this.data.Count; }
    }

    public override void Swap(int x, int y)
    {
      int xPos = -1, yPos = -1, count = 0;

      for (var i = 0; count < 2 && i < this.data.Count; i++)
      {
        var element = this.data[i];
        if (element.One == x)
        {
          xPos = i;
          count++;
        }
        else if (element.One == y)
        {
          yPos = i;
          count++;
        }
      }

      switch (count)
      {
        case 0:
          return;

        case 1:
          {
            if (xPos == -1)
            {
              this.data[yPos] = new Pair<int, Rational>(x, this.data[yPos].Two);
            }
            if (yPos == -1)
            {
              this.data[xPos] = new Pair<int, Rational>(y, this.data[xPos].Two);
            }
          }
          break;

        case 2:
          {
            var tmp = this.data[xPos];
            this.data[xPos] = this.data[yPos];
            this.data[yPos] = tmp;
          }
          break;

        default:
          break;
      }
    }

    new static internal string Statistics
    {
      get { return ""; }
    }

    public override string ToString()
    {
      return this.data.ToString();
    }
  }
}

  