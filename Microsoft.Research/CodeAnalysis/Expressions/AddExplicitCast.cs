// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            Contract.Requires(mDriver != null);

            MethodDriver = mDriver;
            castTo = MethodDriver.MetaDataDecoder.DeclaringType(MethodDriver.CurrentMethod);
        }

        protected override BoxedExpression Variable(BoxedExpression original, object var, PathElement[] path)
        {
            if (var is Variabl && path != null && path.Length > 0)
            {
                var varAsV = (Variabl)var;
                var pathAsFList = path.ToFList();

                if (MethodDriver.Context.ValueContext.IsRootedInParameter(pathAsFList) && path[0].AssumeNotNull().ToString() == "this")
                {
                    return BoxedExpression.VarCast(var, path, default(Type), castTo);
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
