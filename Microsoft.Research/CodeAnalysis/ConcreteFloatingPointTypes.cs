// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    /// <summary>
    /// Define the concrete floating point types
    /// </summary>
    public enum ConcreteFloat
    {
        Float32, Float64, Float80, Uncompatible
    }

    public static class ConcreteFloatsExtensions
    {
        public static string ToCastString(this ConcreteFloat f)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            switch (f)
            {
                case ConcreteFloat.Float32:
                    return "(float)";

                case ConcreteFloat.Float64:
                    return "(double)";

                case ConcreteFloat.Float80:
                case ConcreteFloat.Uncompatible:
                default:
                    return "";
            }
        }
    }
}