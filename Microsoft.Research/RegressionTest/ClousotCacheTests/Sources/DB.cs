// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

// Stressing the function name maximum length that can be stored in the DB

namespace Caching
{
    using A = IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName.IAmAGenericClassWithARelativelyLongName<object, object>;
    namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName
    {
        internal class IAmAGenericClassWithARelativelyLongName<T1, T2> { }
    }
    namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName
    {
        using B = IAmAGenericClassWithARelativelyLongName<A, A>;
        namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName2
        {
            using C = IAmAGenericClassWithARelativelyLongName<B, B>;
            namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName3
            {
                using D = IAmAGenericClassWithARelativelyLongName<C, C>;
                namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName4
                {
                    using E = IAmAGenericClassWithARelativelyLongName<D, D>;
                    namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName5
                    {
                        using F = IAmAGenericClassWithARelativelyLongName<E, E>;
                        namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName6
                        {
                            using G = IAmAGenericClassWithARelativelyLongName<F, F>;

                            internal class IAmAStaticClassWithARelativelyLongName
                            {
                                [ClousotRegressionTest]
#if FIRST
                                [RegressionOutcome("No entry found in the cache")]
#else
                                [RegressionOutcome("No entry found in the cache")] // we won't save this as it is too long
                                                                                   //                [RegressionOutcome("An entry has been found in the cache")]
#endif
                                public static void IAmAFunctionWithARelativelyLongNameUsingTheGenericClassWithARelativelyLongName(G x)
                                {
                                    Contract.Requires(x != null);
                                    Console.WriteLine(x);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
