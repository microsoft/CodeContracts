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


namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// A factory type used by compilers to create instances of the type <see cref="FormattableString"/>.
    /// </summary>
    public static class FormattableStringFactory
    {
        /// <summary>
        /// Create a <see cref="FormattableString"/> from a composite format string and object
        /// array containing zero or more objects to format.
        /// </summary>
        public static FormattableString Create(string format, params object[] arguments)
        {
            Contract.Requires(format != null);
            Contract.Requires(arguments != null);
            Contract.Ensures(Contract.Result<FormattableString>() != null);
            Contract.Ensures(Contract.Result<FormattableString>().ArgumentCount == arguments.Length);
            return default(FormattableString);
        }
    }
}

#endif
