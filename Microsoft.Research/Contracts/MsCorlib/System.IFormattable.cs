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

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System {
  // Summary:
  //     Provides functionality to format the value of an object into a string representation.
  [ContractClass(typeof(IFormattableContract))]
  public interface IFormattable
  {
    // Summary:
    //     Formats the value of the current instance using the specified format.
    //
    // Parameters:
    //   format:
    //     The System.String specifying the format to use.-or- null to use the default
    //     format defined for the type of the System.IFormattable implementation.
    //
    //   formatProvider:
    //     The System.IFormatProvider to use to format the value.-or- null to obtain
    //     the numeric format information from the current locale setting of the operating
    //     system.
    //
    // Returns:
    //     A System.String containing the value of the current instance in the specified
    //     format.
    [Pure]
    string ToString(string format, IFormatProvider formatProvider);
  }

  [ContractClassFor(typeof(IFormattable))]
  abstract class IFormattableContract : IFormattable
  {
    [Pure]
    string IFormattable.ToString(string format, IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
  }
}

