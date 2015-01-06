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
using Microsoft.Research.DataStructures;
using System.Diagnostics;

namespace Microsoft.Research.CodeAnalysis.ILCodeProvider
{
  enum Instruction
  {
    Pop,
  }

  static class Predefined
  {
    public static Subroutine OldEvalPopSubroutine<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly>(
      MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
      Subroutine oldEval
    )
    {
      Debug.Assert(oldEval.StackDelta == 1);
      var sub = new SimpleILCodeProvider<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly>(
        new Instruction[]{ Instruction.Pop },
        0).GetSubroutine(methodCache);

      sub.AddEdgeSubroutine(sub.Entry, sub.EntryAfterRequires, oldEval, "oldmanifest");
      return sub;
    }
  }

  class SimpleILCodeProvider<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> : ICodeProvider<int, Method, Type>, IDecodeMSIL<int, Local, Parameter, Method, Field, Type, Unit, Unit, Unit>
  {
    Instruction[] instructions;
    int stackDelta;

    /// <summary>
    /// Build a code provider with the given instruction sequence
    /// </summary>
    /// <param name="instructions"></param>
    /// <param name="stackDelta">The number of values on the stack left by this instruction sequence (can be negative)</param>
    public SimpleILCodeProvider(Instruction[] instructions, int stackDelta)
    {
      this.instructions = instructions;
      this.stackDelta = stackDelta;
    }

    public Subroutine GetSubroutine(MethodCache<Local,Parameter,Type,Method,Field,Property,Attribute,Assembly> methodCache) 
    {
      return methodCache.BuildSubroutine(this.stackDelta, this, this, 0);
    }

    #region ICodeProvider<int,Method,Type> Members

    public R Decode<T, R>(T data, int label, ICodeQuery<int, Type, Method, T, R> query)
    {
      switch (instructions[label])
      {
        case Instruction.Pop:
          return query.Atomic(data, label);
          
        default:
          throw new NotImplementedException("unknown instruction");
      }
    }

    public bool Next(int current, out int nextLabel)
    {
      current++;
      if (current < instructions.Length)
      {
        nextLabel = current;
        return true;
      }
      nextLabel = 0;
      return false;
    }

    public void PrintCodeAt(int pc, string indent, System.IO.TextWriter tw)
    {
      tw.Write("{0}{1}", indent, instructions[pc].ToString());
    }

    public string SourceAssertionCondition(int pc)
    {
      return null;
    }

    public bool HasSourceContext(int pc)
    {
      return false;
    }

    public string SourceDocument(int pc)
    {
      return null;
    }

    public int SourceStartLine(int pc)
    {
      return 0;
    }

    public int SourceEndLine(int pc)
    {
      return 0;
    }

    public int SourceStartColumn(int pc)
    {
      return 0;
    }

    public int SourceEndColumn(int pc)
    {
      return 0;
    }

    public int ILOffset(int pc)
    {
      return 0;
    }

    #endregion


    #region IDecodeMSIL<int,Local,Parameter,Method,Field,Type,Unit,Unit,Unit> Members

    public Result ForwardDecode<Data, Result, Visitor>(int lab, Visitor visitor, Data data) where Visitor : IVisitMSIL<int, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result>
    {
      switch (instructions[lab])
      {
        case Instruction.Pop:
          return visitor.Pop(lab, Unit.Value, data);

        default:
          throw new NotImplementedException("unknown instruction");
      }
    }

    public Transformer<int, Data, Result> CacheForwardDecoder<Data, Result>(IVisitMSIL<int, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result> visitor)
    {
      return delegate(int lab, Data data) { return this.ForwardDecode<Data, Result, IVisitMSIL<int, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result>>(lab, visitor, data); };
    }

    public Unit GetContext
    {
      get { return Unit.Value; }
    }

    #endregion
  }


}
