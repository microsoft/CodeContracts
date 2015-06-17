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
using System.Compiler;
using System.Linq;
using System.Text;

namespace Microsoft.Cci.Pdb
{
  public interface ILocalScope
  {
    /// <summary>
    /// The offset of the first operation in the scope.
    /// </summary>
    uint Offset { get; }

    /// <summary>
    /// The length of the scope. Offset+Length equals the offset of the first operation outside the scope, or equals the method body length.
    /// </summary>
    uint Length { get; }

    /// <summary>
    /// The definition of the method in which this local scope is defined.
    /// </summary>
    Method MethodDefinition
    {
      get;
    }

  }


  internal sealed class PdbIteratorScope : ILocalScope
  {

    internal PdbIteratorScope(uint offset, uint length)
    {
      this.offset = offset;
      this.length = length;
    }

    public uint Offset
    {
      get { return this.offset; }
    }
    uint offset;

    public uint Length
    {
      get { return this.length; }
    }
    uint length;

    public Method MethodDefinition
    {
      get { return this.methodDefinition; }
      set { this.methodDefinition = value; }
    }
    Method methodDefinition;
  }

}
