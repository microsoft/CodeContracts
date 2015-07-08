using System;

namespace Microsoft.Contracts.Foxtrot
{
    // TODO ST: consider replacing this struct by Lazy<T>
    public struct Cache<T>
    {
        private T cache;
        private Func<T> compute;
        private bool initialized;

        public Cache(Func<T> compute)
        {
            this.compute = compute;
            this.initialized = true;
            cache = default(T);
        }

        public bool IsInitialized
        {
            get { return this.initialized; }
        }

        public T Value
        {
            get
            {
                if (compute != null)
                {
                    cache = compute();
                    compute = null;
                }
                return cache;
            }
        }
    }
}