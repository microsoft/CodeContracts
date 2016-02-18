// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{
    public delegate TResult Func<Arg1, Arg2, Arg3, Arg4, Arg5, TResult>(Arg1 arg1, Arg2 arg2, Arg3 arg3, Arg4 arg4, Arg5 arg5);

    [AttributeUsage(AttributeTargets.Interface)]
    public class ContextAdapterAttribute : Attribute
    {
    }

    public static class CallAdaption
    {
        #region Interface style call adaptors

        [ThreadStatic]
        private static List<object> contextAdapters;

        /// <summary>
        /// Need to have this property because there needs to be a contextAdapters list
        /// that is unique per thread. But the static field is initialized just once:
        /// only in the thread that executes the class constructor. So all other threads
        /// will see a null value.
        /// </summary>
        private static List<object> ContextAdapters
        {
            get
            {
                if (contextAdapters == null)
                    contextAdapters = new List<object>();
                return contextAdapters;
            }
        }


        private static object Last
        {
            get
            {
                Contract.Assume(ContextAdapters.Count > 0);
                return ContextAdapters[ContextAdapters.Count - 1];
            }
        }

        public static void Push<T>(T @this) where T : class
        {
            Contract.Requires(@this != null);

            ContextAdapters.Add(@this);
        }

        public static void Pop<T>(T @this) where T : class
        {
            Contract.Requires(@this != null);
            Contract.Assume(ContextAdapters.Count > 0);
            Contract.Assume(Last == @this || Last == null);

            ContextAdapters.RemoveAt(ContextAdapters.Count - 1);
        }

        public static T Dispatch<T>(T @this) where T : class
        {
            Contract.Requires(@this != null);
            Contract.Ensures(Contract.Result<T>() != null);

            Contract.Assume(ContextAdapters.Count > 0);
            Contract.Assume(Last == @this || Last == null);

            return FindAdaptorStartingAt<T>(@this, 0);
        }

        /// <summary>
        /// We just return the first adapter. The duplicates are lazyily removed when Inner is used
        /// </summary>
        private static T FindAdaptorStartingAt<T>(T @default, int startIndex) where T : class
        {
            Contract.Requires(startIndex >= 0);
            Contract.Ensures(Contract.Result<T>() != null || @default == null);

            List<object> adapters = ContextAdapters;
            for (int i = startIndex; i < adapters.Count; i++)
            {
                T adapter = adapters[i] as T;
                if (adapter != null)
                {
                    return adapter;
                }
            }
            return @default;
        }

        /// <summary>
        /// Clear duplicates below the current context
        /// </summary>
        public static T Inner<T>(T @this) where T : class
        {
            Contract.Ensures(Contract.Result<T>() != null);

            // find current adapter position and search next one from there
            for (int i = 0; i < ContextAdapters.Count; i++)
            {
                if (ContextAdapters[i] == @this)
                {
                    ClearDuplicates(@this, i + 1);
                    var result = FindAdaptorStartingAt<T>(null, i + 1);
                    if (result != null)
                    {
                        return result;
                    }
                    throw new InvalidOperationException("no inner context found");
                }
            }
            throw new InvalidOperationException("@this is not a current adapter");
        }

        private static void ClearDuplicates(object @this, int from)
        {
            Contract.Requires(from >= 0);

            for (int i = from; i < ContextAdapters.Count; i++)
            {
                if (ContextAdapters[i] == @this) ContextAdapters[i] = null;
            }
        }

        #endregion
    }
}
