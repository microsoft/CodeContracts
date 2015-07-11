// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Contracts.Foxtrot
{
    // TODO ST: consider replacing this struct by Lazy<T>
    public struct Cache<T>
    {
        private T _cache;
        private Func<T> _compute;
        private bool _initialized;

        public Cache(Func<T> compute)
        {
            _compute = compute;
            _initialized = true;
            _cache = default(T);
        }

        public bool IsInitialized
        {
            get { return _initialized; }
        }

        public T Value
        {
            get
            {
                if (_compute != null)
                {
                    _cache = _compute();
                    _compute = null;
                }
                return _cache;
            }
        }
    }
}