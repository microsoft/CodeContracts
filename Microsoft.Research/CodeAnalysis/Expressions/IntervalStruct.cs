// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{
    public struct IntervalStruct
    {
        public readonly bool IsValid;
        public readonly BoxedExpression MinValue;
        public readonly BoxedExpression MaxValue;

        public static IntervalStruct None { get { return new IntervalStruct(); } }

        public IntervalStruct(Decimal MinValue, Decimal MaxValue, Func<Decimal, BoxedExpression> ToBoxedExpression)
        {
            Contract.Requires(ToBoxedExpression != null);

            this.IsValid = true;
            this.MinValue = ToBoxedExpression(MinValue);
            this.MaxValue = ToBoxedExpression(MaxValue);
        }

        public override string ToString()
        {
            return this.IsValid ? string.Format("({0}, {1})", this.MinValue, this.MaxValue) : "None";
        }
    }
}
