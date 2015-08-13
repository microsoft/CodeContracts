// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
