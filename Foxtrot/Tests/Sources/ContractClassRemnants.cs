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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

/// <summary>
/// A Function that takes a single argument of type P and returns a value of type R.
/// </summary>
public delegate R Function<P, R>(P p);

/// <summary>
/// The name of an entity. Typically name instances come from a common pool. Within the pool no two distinct instances will have the same Value or UniqueKey.
/// </summary>
[ContractClass(typeof(INameContract))]
public interface IName
{
  /// <summary>
  /// An integer that is unique within the pool from which the name instance has been allocated. Useful as a hashtable key.
  /// </summary>
  int UniqueKey
  {
    get;
    //^ ensures result > 0;
  }

  /// <summary>
  /// An integer that is unique within the pool from which the name instance has been allocated. Useful as a hashtable key.
  /// All name instances in the pool that have the same string value when ignoring the case of the characters in the string
  /// will have the same key value.
  /// </summary>
  int UniqueKeyIgnoringCase
  {
    get;
    //^ ensures result > 0;
  }

  /// <summary>
  /// The string value corresponding to this name.
  /// </summary>
  string Value { get; }
}

[ContractClassFor(typeof(IName))]
abstract class INameContract : IName
{
  public int UniqueKey
  {
    get
    {
      Contract.Ensures(Contract.Result<int>() > 0);
      throw new NotImplementedException();
    }
  }

  public int UniqueKeyIgnoringCase
  {
    get
    {
      Contract.Ensures(Contract.Result<int>() > 0);
      throw new NotImplementedException();
    }
  }

  public string Value
  {
    get
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }
  }
}



/// <summary>
/// Implemented by any entity that has a name.
/// </summary>
[ContractClass(typeof(INamedEntityContract))]
public interface INamedEntity
{
  /// <summary>
  /// The name of the entity.
  /// </summary>
  IName Name { get; }
}

[ContractClassFor(typeof(INamedEntity))]
abstract class INamedEntityContract : INamedEntity
{
  public IName Name
  {
    get
    {
      Contract.Ensures(Contract.Result<IName>() != null);
      throw new NotImplementedException();
    }
  }
}

/// <summary>
/// A collection of named members, with routines to search the collection.
/// </summary>
[ContractClass(typeof(IScopeContract<>))]
public interface IScope<MemberType>
  where MemberType : class, INamedEntity
{

  /// <summary>
  /// Return true if the given member instance is a member of this scope.
  /// </summary>
  [Pure]
  bool Contains(MemberType/*!*/ member);
  // ^ ensures result == exists{MemberType mem in this.Members; mem == member};

  /// <summary>
  /// Returns the list of members with the given name that also satisfy the given predicate.
  /// </summary>
  [Pure]
  IEnumerable<MemberType> GetMatchingMembersNamed(IName name, bool ignoreCase, Function<MemberType, bool> predicate);
  // ^ ensures forall{MemberType member in result; member.Name == name && predicate(member) && this.Contains(member)};
  // ^ ensures forall{MemberType member in this.Members; member.Name == name && predicate(member) ==> 
  // ^                                                            exists{INamespaceMember mem in result; mem == member}};

  /// <summary>
  /// Returns the list of members that satisfy the given predicate.
  /// </summary>
  [Pure]
  IEnumerable<MemberType> GetMatchingMembers(Function<MemberType, bool> predicate);
  // ^ ensures forall{MemberType member in result; predicate(member) && this.Contains(member)};
  // ^ ensures forall{MemberType member in this.Members; predicate(member) ==> exists{MemberType mem in result; mem == member}};

  /// <summary>
  /// Returns the list of members with the given name.
  /// </summary>
  [Pure]
  IEnumerable<MemberType> GetMembersNamed(IName name, bool ignoreCase);
  // ^ ensures forall{MemberType member in result; member.Name == name && this.Contains(member)};
  // ^ ensures forall{MemberType member in this.Members; member.Name == name ==> 
  // ^                                                            exists{INamespaceMember mem in result; mem == member}};

  /// <summary>
  /// The collection of member instances that are members of this scope.
  /// </summary>
  IEnumerable<MemberType> Members { get; }
}

#region IScope<MemberType> contract binding
[ContractClassFor(typeof(IScope<>))]
abstract class IScopeContract<MemberType> : IScope<MemberType>
  where MemberType : class, INamedEntity
{

  public bool Contains(MemberType member)
  {
    //Contract.Ensures(!Contract.Result<bool>() || Contract.Exists(this.Members, x => x == (object)this));
    throw new NotImplementedException();
  }

  public IEnumerable<MemberType> GetMatchingMembersNamed(IName name, bool ignoreCase, Function<MemberType, bool> predicate)
  {
    Contract.Requires(name != null);
    Contract.Requires(predicate != null);
    Contract.Ensures(Contract.Result<IEnumerable<MemberType>>() != null);
    Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(), x => x != null));
    Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(), x => this.Contains(x)));
    Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(),
      x => ignoreCase ? x.Name.UniqueKeyIgnoringCase == x.Name.UniqueKeyIgnoringCase : x.Name == name));
    //Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(), x => predicate(x)));
    throw new NotImplementedException();
  }

  public IEnumerable<MemberType> GetMatchingMembers(Function<MemberType, bool> predicate)
  {
    Contract.Requires(predicate != null);
    Contract.Ensures(Contract.Result<IEnumerable<MemberType>>() != null);
    Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(), x => x != null));
    Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(), x => this.Contains(x)));
    //Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(), x => predicate(x)));
    throw new NotImplementedException();
  }

  public IEnumerable<MemberType> GetMembersNamed(IName name, bool ignoreCase)
  {
    Contract.Requires(name != null);
    Contract.Ensures(Contract.Result<IEnumerable<MemberType>>() != null);
    Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(), x => x != null));
    Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(), x => this.Contains(x)));
    Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(),
      x => ignoreCase ? x.Name.UniqueKeyIgnoringCase == x.Name.UniqueKeyIgnoringCase : x.Name == name));
    throw new NotImplementedException();
  }

  public IEnumerable<MemberType> Members
  {
    get
    {
      Contract.Ensures(Contract.Result<IEnumerable<MemberType>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(), x => x != null));
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<MemberType>>(), x => this.Contains(x)));
      throw new NotImplementedException();
    }
  }
}
#endregion


namespace Tests.Sources
{

  class DummyScope : IScope<Member>
  {
    public bool Contains(Member member)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Member> GetMatchingMembersNamed(IName name, bool ignoreCase, Function<Member, bool> predicate)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Member> GetMatchingMembers(Function<Member, bool> predicate)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Member> GetMembersNamed(IName name, bool ignoreCase)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Member> Members
    {
      get { throw new NotImplementedException(); }
    }
  }

  partial class TestMain
  {
    partial void Run()
    {
      var x = new DummyScope();
      if (!this.behave)
      {
        x.GetMembersNamed(null, false);
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "name != null";
  }

  class Member : INamedEntity
  {
    IName INamedEntity.Name
    {
      get { throw new NotImplementedException(); }
    }
  }
}
