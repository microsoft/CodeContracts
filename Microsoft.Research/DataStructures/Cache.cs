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

// The interface and the implementation of a Cache of objects

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;


namespace Microsoft.Research.DataStructures
{
  /// <summary>
  /// The interface for a cache
  /// </summary>
  public interface ICache<In, Out>
  {
    void Add(In index, Out output);

    /// <summary>
    /// Set/Get an element in the cache.
    /// When setting, if the element is there, is replaced, otherwise it is created.
    /// When getting, if the element is not there, an exception is thrown.
    /// </summary>
    Out this[In index]
    {
      get;
      set;
    }

    /// <summary>
    /// Is the <code>index</code> in the cache?
    /// </summary>
    bool ContainsKey(In index);

    bool TryGetValue(In index, out Out value);
  }


  public abstract class GenericCache
  {
    #region Static fields
    [ThreadStatic]
    static public int DEFAULT_CACHE_SIZE;

    [ThreadStatic]
    protected static int cacheHit;
    [ThreadStatic]
    protected static int cacheMiss;
    [ThreadStatic]
    protected static int totalCacheAccesses;

    #endregion

    /// <summary>
    /// Get the usage statistics of this cache
    /// </summary>
    static public string Statistics
    {
      get
      {
        StringBuilder output = new StringBuilder();

        output.Append("Overall use of the FIFO cache:" + Environment.NewLine);
        output.AppendFormat("Cache accesses : {0}" + Environment.NewLine, totalCacheAccesses);
        output.AppendFormat("Cache Hit : {0} ({1:P})" + Environment.NewLine, cacheHit, totalCacheAccesses != 0 ? cacheHit / (double)totalCacheAccesses: 0);
        output.AppendFormat("Cache Miss: {0}" + Environment.NewLine, totalCacheAccesses - cacheHit);

        return output.ToString();
      }
    }
  }

  /// <summary>
  /// A cache where the politics for removing elements if a <code>FIFO</code>
  /// </summary>
  public class FIFOCache<In, Out> : GenericCache, ICache<In, Out>
  {
    #region Invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.elements != null);
      Contract.Invariant(this.fifo != null);
    }

    #endregion

    #region Private state
    readonly private Dictionary<In, Out> elements;
    readonly private LinkedList<In> fifo;
    private int size;
    #endregion

    #region Constructor
    /// <summary>
    /// The size of the cache
    /// </summary>
    /// <param name="size">Must be strictly positive</param>
    public FIFOCache(int size)
    {
      Contract.Requires(size > 0, "The size of the cache must be strictly positive.");

      this.elements = new Dictionary<In, Out>(size);
      this.fifo = new LinkedList<In>();
      this.size = size;
    }

    /// <summary>
    /// Construct a cache of default size
    /// </summary>
    public FIFOCache()
      : this(DEFAULT_CACHE_SIZE)
    {
    }

    #endregion

    #region ICache<In,Out> Members

    /// <summary>
    /// Add an element to the cache.
    /// If <code>index</code> is in the cache, then <code>this[index] == output</code>
    /// </summary>
    public void Add(In index, Out output)
    {
      if (this.elements.ContainsKey(index))
      {
        // Contract.Assert(!Object.Equals(this.elements[index], output), "Error: trying to set a different value in the cache for the same input");
        return;
      }

      if (this.elements.Count < this.size)    // there is still room for a new element in the cache
      {
        this.elements[index] = output;
        this.fifo.AddLast(index);           // Add at the end of the queue
      }
      else
      {
        //^ assert this.fifo.First != null;
        In toRemove = this.fifo.First.Value;
        this.fifo.RemoveFirst();  // Remove the first element of the queue
        this.elements.Remove(toRemove);

        this.Add(index, output);                // Now there must be space, so we call ourselves recursively
      }
    }

    public bool TryGetValue(In index, out Out value)
    {
      totalCacheAccesses++;

      bool b = this.elements.TryGetValue(index, out value);
      if (b)
      {
        cacheHit++;
      }

      return b;
    }

    /// <summary>
    /// Get/Sets elements of the cache
    /// </summary>
    public Out this[In index]
    {
      get
      {
        totalCacheAccesses++;
        return this.elements[index];
      }
      set
      {
        totalCacheAccesses++;
        this.Add(index, value);
      }
    }

    /// <summary>
    /// Is the <code>index</code> in this cache
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool ContainsKey(In index)
    {
      bool b = this.elements.ContainsKey(index);

      // Updates the statistics
      totalCacheAccesses++;

      if (b)
        cacheHit++;
      else
        cacheMiss++;

      return b;
    }

    #endregion
  }
}
