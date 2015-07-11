// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
                output.AppendFormat("Cache Hit : {0} ({1:P})" + Environment.NewLine, cacheHit, totalCacheAccesses != 0 ? cacheHit / (double)totalCacheAccesses : 0);
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
        private void ObjectInvariant()
        {
            Contract.Invariant(elements != null);
            Contract.Invariant(fifo != null);
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

            elements = new Dictionary<In, Out>(size);
            fifo = new LinkedList<In>();
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
            if (elements.ContainsKey(index))
            {
                // Contract.Assert(!Object.Equals(this.elements[index], output), "Error: trying to set a different value in the cache for the same input");
                return;
            }

            if (elements.Count < size)    // there is still room for a new element in the cache
            {
                elements[index] = output;
                fifo.AddLast(index);           // Add at the end of the queue
            }
            else
            {
                //^ assert this.fifo.First != null;
                In toRemove = fifo.First.Value;
                fifo.RemoveFirst();  // Remove the first element of the queue
                elements.Remove(toRemove);

                this.Add(index, output);                // Now there must be space, so we call ourselves recursively
            }
        }

        public bool TryGetValue(In index, out Out value)
        {
            totalCacheAccesses++;

            bool b = elements.TryGetValue(index, out value);
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
                return elements[index];
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
            bool b = elements.ContainsKey(index);

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
