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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Bytecode
{
  public class Bytecode
  {
    public int AddThreValues(int x, int y, int z)
    {
      return x + y + z;
    }

    public int DeStack(int x, int y, int z)
    {
      int stack;     // Stack
      int CS_1_0000; // CS$1$0000 is an invalid C# name
      stack = x + y; 
      stack = stack + z;
      CS_1_0000 = stack;
      return CS_1_0000;
    }
  }

  public class Alias
  {
    int x;

    public void Foo(bool b)
    {
      Alias tmp = new Alias();

      tmp.x = -11;

      Alias alias = tmp;

      if(b)
      {
        alias.x = 10;
      }

      Contract.Assert(tmp.x == -11);
    }
  }

  public class AliasScalar
  {    
    public void Foo(bool b)
    {
      int svX = -11;
      if (b)
      {
        svX = 10;
      }
      Contract.Assert(svX == -11);
    }
  }
}
