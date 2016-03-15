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

#if NETFRAMEWORK_4_6

using System;
using System.Diagnostics.Contracts;

namespace System 
{
    /// <summary>
    /// A composite format string along with the arguments to be formatted. An instance of this
    /// type may result from the use of the C# or VB language primitive "interpolated string".
    /// </summary>
    [ContractClass(typeof(FormattableStringContract))]
    public abstract class FormattableString : IFormattable
    {
        /// <summary>
        /// The composite format string.
        /// </summary>
        public abstract String Format { get; }

        /// <summary>
        /// Returns an object array that contains zero or more objects to format. Clients should not
        /// mutate the contents of the array.
        /// </summary>
        public abstract Object[] GetArguments();

        /// <summary>
        /// The number of arguments to be formatted.
        /// </summary>
        public abstract int ArgumentCount { get; }

        /// <summary>
        /// Returns one argument to be formatted from argument position <paramref name="index"/>.
        /// </summary>
        public abstract Object GetArgument(int index);

        /// <summary>
        /// Format to a string using the given culture.
        /// </summary>
        public abstract String ToString(IFormatProvider formatProvider);

        String IFormattable.ToString(string ignored, IFormatProvider formatProvider)
        {
            return default(String);
        }

        /// <summary>
        /// Format the given object in the invariant culture. This static method may be
        /// imported in C# by
        /// <code>
        /// using static System.FormattableString;
        /// </code>.
        /// Within the scope
        /// of that import directive an interpolated string may be formatted in the
        /// invariant culture by writing, for example,
        /// <code>
        /// Invariant($"{{ lat = {latitude}; lon = {longitude} }}")
        /// </code>
        /// </summary>
        [Pure]
        public static String Invariant(FormattableString formattable)
        {
            Contract.Requires(formattable != null);
            Contract.Ensures(Contract.Result<String>() != null);
            return default(String);
        }
    }

    [ContractClassFor(typeof(FormattableString))]
    abstract class FormattableStringContract : FormattableString
    {
        /// <summary>
        /// The composite format string.
        /// </summary>
        public override String Format
        {
            get
            {
                Contract.Ensures(Contract.Result<String>() != null);
                return default(String);
            }
        }

        /// <summary>
        /// Returns an object array that contains zero or more objects to format. Clients should not
        /// mutate the contents of the array.
        /// </summary>
        [Pure]
        public override Object[] GetArguments()
        {
            Contract.Ensures(Contract.Result<Object[]>() != null);
            Contract.Ensures(Contract.Result<Object[]>().Length == ArgumentCount);
            return default(Object[]);
        }

        /// <summary>
        /// The number of arguments to be formatted.
        /// </summary>
        public override int ArgumentCount
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        /// <summary>
        /// Returns one argument to be formatted from argument position <paramref name="index"/>.
        /// </summary>
        [Pure]
        public override Object GetArgument(int index)
        {
            Contract.Requires(index >= 0);
            Contract.Requires(index < ArgumentCount);
            return default(Object);
        }

        /// <summary>
        /// Format to a string using the given culture.
        /// </summary>
        [Pure]
        public override String ToString(IFormatProvider formatProvider)
        {
            Contract.Ensures(Contract.Result<String>() != null);
            return default(String);
        }
    }
}

#endif
