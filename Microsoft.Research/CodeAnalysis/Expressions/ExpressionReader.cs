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
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public struct ReaderInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable> 
    where Type : IEquatable<Type>
  {
    readonly public APC PC;
    readonly public IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context;
    readonly public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder;

    public ReaderInfo(APC pc, IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder)
    {
      Contract.Requires(Context != null);
      Contract.Requires(MetaDataDecoder != null);

      this.PC = pc;
      this.Context = Context;
      this.MetaDataDecoder = MetaDataDecoder;
    }
  }

  public class ExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable> 
    : BoxedExpressionTransformer<ReaderInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>>
    where Type : IEquatable<Type>
  {

    protected override Func<object, FList<PathElement>> GetPathFetcher(Func<object, FList<PathElement>> PathFetcher)
    {
      return (object o) =>
      {
        if (o is Variable)
        {
          return this.Info.Context.ValueContext.AccessPathList(this.Info.PC, (Variable)o, true, true);
        }
        else
        {
          return null;
        }
      };
    }

  }
}
