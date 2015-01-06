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

namespace Microsoft.Research.CodeAnalysis.Expressions
{
  public class AddExplicitCastToTheExpression<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variabl, LogOptions> 
    : BoxedExpressionTransformer<Void>
    where LogOptions : IFrameworkLogOptions
    where Type : IEquatable<Type>
 
  {
    readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variabl, LogOptions> MethodDriver;
    readonly private Type castTo;

    public AddExplicitCastToTheExpression(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variabl, LogOptions> mDriver)
    {
      Contract.Requires(mDriver!= null);

      this.MethodDriver = mDriver;
      this.castTo = this.MethodDriver.MetaDataDecoder.DeclaringType(this.MethodDriver.CurrentMethod);
    }

    protected override BoxedExpression Variable(BoxedExpression original, object var, PathElement[] path)
    {
      if (var is Variabl && path != null && path.Length > 0)
      {
        var varAsV = (Variabl)var;
        var pathAsFList = path.ToFList();

        if (this.MethodDriver.Context.ValueContext.IsRootedInParameter(pathAsFList) && path[0].AssumeNotNull().ToString() == "this")
        {
          return BoxedExpression.VarCast(var, path, default(Type), this.castTo);
        }
        else
        {
          return original;
        }
      }

      return null;
    }
  }
}
