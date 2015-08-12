// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{
    public class LazyEval<T>
    {
        private readonly Func<T> func;
        private Optional<T> value;

        public T Value
        {
            get
            {
                if (!value.IsValid)
                {
                    value = func();
                }

                return value.Value;
            }
        }

        public LazyEval(Func<T> func)
        {
            Contract.Requires(func != null);

            this.func = func;
        }

        public LazyEval(LazyEval<T> l)
        {
            Contract.Requires(l != null);

            value = l.value;
        }
    }
}
