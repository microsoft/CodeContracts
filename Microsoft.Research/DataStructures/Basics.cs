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

namespace Microsoft.Research.DataStructures
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Text;
  using System.IO;
  using System.Linq;
  using System.Diagnostics.Contracts;


  /// <summary>
  /// A dummy empty type like void that can be used for generic instances
  /// </summary>
  public struct Unit : IEquatable<Unit>
  {

    public static readonly Unit Value; // Thread-safe

    #region IEquatable<Unit> Members

    public bool Equals(Unit other)
    {
      return true;
    }

    #endregion
  }



  /// <summary>
  /// An abstraction over sequences
  /// </summary>
  public partial interface IIndexable<T> {
    int Count { get; }
    T this[int index] { get; }
  }
  #region IIndexable contract binding
  [ContractClass(typeof(IIndexableContract<>))]
  public partial interface IIndexable<T>
  {
  }

  [ContractClassFor(typeof(IIndexable<>))]
  abstract class IIndexableContract<T> : IIndexable<T>
  {
    public int Count
    {
      get {
        Contract.Ensures(Contract.Result<int>() >= 0);
        throw new NotImplementedException();
      }
    }

    public T this[int index]
    {
      get {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);

        throw new NotImplementedException();
      }
    }
  }
  #endregion

  public struct IndexableEnumerable<T, IndexT> : IEnumerable<T>
    where IndexT : IIndexable<T>
  {
    IndexT indexable;

    public IndexableEnumerable(IndexT indexable)
    {
      Contract.Requires(indexable != null);
      this.indexable = indexable;
    }

    public struct Enumerator : IEnumerator<T>
    {
      IndexT indexable;
      int position;

      public Enumerator(IndexT indexable)
      {
        this.indexable = indexable;
        this.position = -1;
      }

      public T Current
      {
        get
        {
          if (position >= 0 && position < indexable.Count)
          {
            return this.indexable[this.position];
          }
          throw new InvalidOperationException();
        }
      }
      object System.Collections.IEnumerator.Current
      {
        get
        {
          return this.Current;
        }
      }

      public bool MoveNext()
      {
        position++;
        return position < indexable.Count;
      }

      public void Reset()
      {
        this.position = -1;
      }

      void IDisposable.Dispose()
      {
      }
    }
    public Enumerator GetEnumerator() { return new Enumerator(this.indexable); }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }

  [Serializable]
  public struct EmptyIndexable<T> : IIndexable<T>, IEnumerable<T>
  {
    #region IIndexable<T> Members

    public int Count {
      get { return 0; }
    }

    public T this[int index] {
      get { throw new IndexOutOfRangeException(); }
    }

    #endregion

    public static readonly EmptyIndexable<T> Empty = new EmptyIndexable<T>(); // Thread-safe

    public IEnumerator<T> GetEnumerator() { yield break; }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { yield break; }
  }

  public struct ThreeValued
  {
    int value; // 0 undetermined, 1 false, 2 true

    public ThreeValued(bool truth)
    {
      if (truth) value = 2;
      else value = 1;
    }

    public bool IsTrue { get { return value == 2; } }
    public bool IsFalse { get { return value == 1; } }
    public bool IsDetermined { get { return value != 0; } }
    public bool Truth { get { if (IsTrue) return true; if (IsFalse) return false; throw new InvalidOperationException(); } }
  }

  [Serializable]
  public struct UnitIndexable : IIndexable<Unit>, IEnumerable<Unit>
  {
    int count;

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(count >= 0);
    }

    public UnitIndexable(int count)
    {
      Contract.Requires(count >= 0);
      this.count = count;
    }

    #region IIndexable<Unit> Members

    public int Count
    {
      get { return this.count; }
    }

    public Unit this[int index]
    {
      get { return Unit.Value; }
    }

    #endregion

    #region IEnumerable<Unit> Members

    public IEnumerator<Unit> GetEnumerator()
    {
      for (int i = 0; i < count; i++)
      {
        yield return Unit.Value;
      }
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      for (int i = 0; i < count; i++)
      {
        yield return Unit.Value;
      }
    }

    #endregion
  }

  [Serializable]
  public struct TailIndexable<T> : IIndexable<T>, IEnumerable<T>
  {
    IIndexable<T> underlying;

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(underlying != null);
    }

    public TailIndexable(IIndexable<T> underlying)
    {
      Contract.Requires(underlying != null);
      this.underlying = underlying;
    }

    #region IIndexable<T> Members

    public int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == 0 || Contract.Result<int>() == underlying.Count - 1);

        var c = underlying.Count;
        if (c > 0) return c - 1;
        return 0;
      }
    }

    public T this[int index]
    {
      get
      {
        Contract.Assert(this.Count > 0, "helping the static checker");
        return underlying[index + 1];
      }
    }

    #endregion

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      for (int i = 0; i < this.Count; i++)
      {
        yield return this[i];
      }
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    #endregion
  }

  [Serializable]
  public struct ArrayIndexable<T> : IIndexable<T>, IEnumerable<T>
  {

    T[]/*?*/ array;

    public ArrayIndexable(T[]/*?*/ array) {
      Contract.Ensures(array == null && Contract.ValueAtReturn(out this).Count == 0 || 
                       Contract.ValueAtReturn(out this).Count == array.Length);
      this.array = array;
    }

    public int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == 0 || this.array != null && Contract.Result<int>() == this.array.Length);
        return (this.array == null) ? 0 : this.array.Length;
      }
    }

    public T this[int index]
    {
      get
      {
        Contract.Assert(this.Count > 0, "helping the static checker");
        return this.array[index];
      }
    }

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      for (int i = 0; i < this.Count; i++)
      {
        yield return this.array[i];
      }
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    #endregion
  }

  [Serializable]
  public struct EnumerableIndexable<T> : IIndexable<T>, IEnumerable<T>
  {
    List<T> list;
    [NonSerialized] IEnumerator<T> enumerator;

    /// <summary>
    /// Produce an IIndexable from the given IEnumerable. The IEnumerable is expanded on
    /// demand up to capacity elements. The count of the IIndexable is the minimum of either the
    /// actual number of elements in the enumeration, or the capacity specified.
    /// </summary>
    /// <param name="capacity">maximal expansion</param>
    public EnumerableIndexable(IEnumerable<T> enumerable, int maxCapacity)
    {
      Contract.Requires(enumerable != null);
      Contract.Requires(maxCapacity >= 0);

      list = new List<T>(maxCapacity);
      enumerator = enumerable.GetEnumerator();
    }

    /// <summary>
    /// Does enumerate the full enumerable and computes the final list
    /// </summary>
    /// <param name="enumerable"></param>
    public EnumerableIndexable(IEnumerable<T> enumerable)
    {
      Contract.Requires(enumerable != null);

      this.list = enumerable.ToList();
      enumerator = null;
    }


    public T this[int index]
    {
      get
      {
      tryAgain:
        if (index >= list.Count)
        {
          if (enumerator != null && index < list.Capacity)
          {
            if (enumerator.MoveNext())
            {
              list.Add(enumerator.Current);
              goto tryAgain;
            }
            else
            {
              enumerator = null;
            }
          }
          // done enumerating
          throw new IndexOutOfRangeException();
        }
        return list[index];
      }
    }

    public int Count
    {
      get
      {
        ForceEnumeration();
        return this.list.Count;
      }
    }

    private void ForceEnumeration()
    {
      while (enumerator != null && list.Count < list.Capacity)
      {
        if (enumerator.MoveNext())
        {
          list.Add(enumerator.Current);
        }
        else
        {
          enumerator = null;
        }
      }
    }

    [System.Runtime.Serialization.OnSerializing]
    private void OnSerializing(System.Runtime.Serialization.StreamingContext context)
    {
      this.ForceEnumeration();
    }

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      for (int i = 0; i < this.Count; i++)
      {
        yield return this[i];
      }
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    #endregion
  }

  [Serializable]
  public struct ListIndexable<T> : IIndexable<T>, IEnumerable<T>
  {
    List<T> list;
    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(list != null);
    }

    public ListIndexable(List<T> list) {
      Contract.Requires(list != null);
      Contract.Ensures(Contract.ValueAtReturn(out this).Count == list.Count);

      this.list = list;
    }
    public int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == list.Count);
        return list.Count;
      }
    }
    public T this[int index]
    {
      get
      {
        Contract.Assert(this.Count > 0, "help static checker");
        return list[index];
      }
    }

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      return this.list.GetEnumerator();
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    #endregion
  }

  public static class IndexableExtensions
  {
    public static IndexableEnumerable<T, IIndexable<T>> Enumerate<T>(this IIndexable<T> arg)
    {
      return new IndexableEnumerable<T, IIndexable<T>>(arg);
    }

    public static EnumerableIndexable<T> AsIndexable<T>(this IEnumerable<T> arg, int capacity)
    {
      return new EnumerableIndexable<T>(arg, capacity);
    }

    public static EnumerableIndexable<T> AsIndexable<T>(this IEnumerable<T> arg)
    {
      return new EnumerableIndexable<T>(arg);
    }

    public static EnumerableIndexable<T> AsIndexable<From,T>(this IEnumerable<From> arg, Func<From, T> convert)
    {
      return new EnumerableIndexable<T>(arg.Select(convert));
    }

    public static ListIndexable<T> AsIndexable<T>(this List<T> list)
    {
      return new ListIndexable<T>(list);
    }

    public static TailIndexable<T> Tail<T>(this IIndexable<T> underlying)
    {
      return new TailIndexable<T>(underlying);
    }

    public static ArrayIndexable<T> AsIndexable<T>(this T[] array)
    {
      return new ArrayIndexable<T>(array);
    }
  }

  /// <summary>
  /// Optional struct that works for all T, not just value types.
  /// </summary>
  [Serializable]
  public struct Optional<T>
  {
    public readonly bool IsValid;
    T value;
    public Optional(T value) {
      this.value = value;
      IsValid = true;
    }
    public T Value { get { return this.value; } }

    public static implicit operator Optional<T>(T value) { return new Optional<T>(value); }

  }

  public interface IVisibilityCheck<Member>
  {
    bool IsAsVisibleAs(Member member);
    bool IsVisibleFrom(Member member);
    bool RootImpliesParameterOrResultOrStatic { get; }
    bool IsTopOfStack(int localStackDepth);
    bool IsStackTemp { get; }
  }

  /// <summary>
  /// Filter for access paths
  /// </summary>
  public class AccessPathFilter<Member, Type>
  {
    enum MemberFilter
    {
      NO_FILTER,
      FROM_PRECONDITION,
      FROM_POSTCONDITION,
      FROM_INSIDE_METHOD,
      FROM_INSIDE_METHOD_OR_CALLED_POST,
    }

    [Flags]
    private enum Flags
    {
      AllowLocals = 0x01,
      RequireParameter = 0x02,
      AvoidCompilerGenerated = 0x04
    }

    private readonly Member member;
    private readonly MemberFilter memberFilter;
    private readonly Flags flags;
    private Type returnType;
    public int LocalStackDepth { get; private set; }

    public static AccessPathFilter<Member, Type> NoFilter = new AccessPathFilter<Member, Type>(); // Thread-safe
    public static AccessPathFilter<Member, Type> FromPrecondition(Member member) 
    {
      Contract.Ensures(Contract.Result<AccessPathFilter<Member, Type>>() != null);
  
      return new AccessPathFilter<Member, Type>(member, MemberFilter.FROM_PRECONDITION);     
    }    
    public static AccessPathFilter<Member, Type> FromPostcondition(Member member, Type returnType) 
    {
      Contract.Ensures(Contract.Result<AccessPathFilter<Member, Type>>() != null);
      
      return new AccessPathFilter<Member, Type>(member, MemberFilter.FROM_POSTCONDITION, returnType) { LocalStackDepth = 1 };
    }
    public static AccessPathFilter<Member, Type> IsVisibleFrom(Member member) 
    {
      Contract.Ensures(Contract.Result<AccessPathFilter<Member, Type>>() != null);
      
      return new AccessPathFilter<Member, Type>(member, MemberFilter.FROM_INSIDE_METHOD); 
    }
    public static AccessPathFilter<Member, Type> IsVisibleFromMethodOrCalledPost(Member member, Type returnType, int localStackDepth) 
    {
      Contract.Ensures(Contract.Result<AccessPathFilter<Member, Type>>() != null);
      
      return new AccessPathFilter<Member, Type>(member, MemberFilter.FROM_INSIDE_METHOD_OR_CALLED_POST, returnType) { LocalStackDepth = localStackDepth }; 
    }

    private AccessPathFilter(Member member, MemberFilter memberFilter)
    {
      this.member = member;
      this.memberFilter = memberFilter;
    }
    private AccessPathFilter(Member member, MemberFilter memberFilter, Type returnType) : this(member, memberFilter)
    {
      // We do not want compiler generated in postconditions
      if(memberFilter.HasFlag(MemberFilter.FROM_POSTCONDITION)) 
      {
        this.flags |= Flags.AvoidCompilerGenerated;
      }

      this.returnType = returnType;
    }
    private AccessPathFilter()
    {
      flags |= Flags.AllowLocals;
      memberFilter = MemberFilter.NO_FILTER;
    }

    public bool FilterOutPathElement<P>(P element) where P:IVisibilityCheck<Member>
    {
      switch (memberFilter)
      {
        case MemberFilter.FROM_INSIDE_METHOD:
          return element.IsStackTemp || !element.IsVisibleFrom(this.member);
        case MemberFilter.FROM_PRECONDITION:
          return element.IsStackTemp || !element.IsAsVisibleAs(this.member);
        case MemberFilter.FROM_POSTCONDITION:
          return !element.RootImpliesParameterOrResultOrStatic || !element.IsVisibleFrom(this.member);
        case MemberFilter.FROM_INSIDE_METHOD_OR_CALLED_POST:
          return element.IsStackTemp && !element.IsTopOfStack(this.LocalStackDepth) || !element.IsVisibleFrom(this.member);
        case MemberFilter.NO_FILTER:
        default:
          return element.IsStackTemp; // ignore stack temporaries (s0, s1, ...)
      }
    }

    public bool AllowLocal
    {
      get
      {
        return (this.memberFilter == MemberFilter.NO_FILTER && ((flags & Flags.AllowLocals) != 0))
          || this.memberFilter == MemberFilter.FROM_INSIDE_METHOD
          || this.memberFilter == MemberFilter.FROM_INSIDE_METHOD_OR_CALLED_POST;
      }
    }

    public bool AvoidCompilerGenerated { get { return (this.flags & Flags.AvoidCompilerGenerated) != 0; } }

    public bool HasVisibilityMember { get { return this.memberFilter != MemberFilter.NO_FILTER; } }

    public bool AllowReturnValue
    {
      get
      {
        switch (this.memberFilter)
        {
          case MemberFilter.FROM_INSIDE_METHOD_OR_CALLED_POST:
          case MemberFilter.FROM_POSTCONDITION:
            return true;
          default:
            return false;
        }
      }
    }
    /// <summary>
    /// Returns the underlying member used to determine visibility.
    /// </summary>
    public Member VisibilityMember { get { return this.member; } }

    public Type ReturnValueType
    {
      get
      {
        Contract.Requires(this.AllowReturnValue);

        return this.returnType;
      }
    }

    public bool AllowCompilerLocal
    {
      get
      {
        return this.memberFilter == MemberFilter.FROM_INSIDE_METHOD_OR_CALLED_POST;
      }
    }
  }
}