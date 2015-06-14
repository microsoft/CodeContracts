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

namespace ExtractorChecks
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// </summary>
  [System.Diagnostics.Contracts.ContractClass(typeof(CacheConstraints<,>))]
  public interface ICache<TKey, TValue> : IDictionary<TKey, TValue>
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="expiration"></param>
    void AddExpiration(TKey key, Object expiration);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    Boolean RemoveExpiration(TKey key);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  [ContractClassFor(typeof(ICache<,>))]
  public abstract class CacheConstraints<TKey, TValue> : ICache<TKey, TValue>
  {
      /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="expiration"></param>
    public void AddExpiration(TKey key, object expiration)
    {
      if (expiration == null) throw new ArgumentNullException("expiration");
    //  Contract.EndContractBlock();
    //  Contract.Requires<ArgumentException>(expiration == null);
      Contract.Requires(Object.Equals(key, default(TKey)), "key");
      //Contract.Requires<ArgumentNullException>(!Object.Equals(expiration, null), "expiration");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    public Boolean RemoveExpiration(TKey key)
    {
      Contract.Requires(!Object.Equals(key, default(TKey)));
      return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public abstract void Add(TKey key, TValue value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public abstract bool ContainsKey(TKey key);
    
    /// <summary>
    /// 
    /// </summary>
    public abstract ICollection<TKey> Keys
    {
      get;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public abstract bool Remove(TKey key);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public abstract bool TryGetValue(TKey key, out TValue value);

    /// <summary>
    /// 
    /// </summary>
    public abstract ICollection<TValue> Values
    { get;
 }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public abstract TValue this[TKey key]
    {
      get;
      set;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public abstract void Add(KeyValuePair<TKey, TValue> item);

    /// <summary>
    /// 
    /// </summary>
    public abstract void Clear();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public abstract bool Contains(KeyValuePair<TKey, TValue> item);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public abstract void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex);
    
    /// <summary>
    /// 
    /// </summary>
    public abstract int Count
    {
      get;
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract bool IsReadOnly
    {
      get;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public abstract bool Remove(KeyValuePair<TKey, TValue> item);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      throw new NotSupportedException();
    }
  }

}
