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

namespace Microsoft.Research.CodeAnalysis
{
  using Domain = Boolean;

  public class EnumerableHelper<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, Expression, ILogOptions>
    : MSILVisitor<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Domain, Domain>
    , IAnalysis<APC, Domain, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Domain, Domain>,
                     IFunctionalMap<Variable,FList<Variable>>>
    where Type : IEquatable<Type>
    where Expression : IEquatable<Expression>
    where Variable : IEquatable<Variable> 
    where ILogOptions: IFrameworkLogOptions
  {
    IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> methodDriver;
    IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context { get { return this.methodDriver.ExpressionLayer.Decoder.Context; } }
    Method moveNext = default(Method);
    FunctionalMap<string, int> fieldsHold;

    public EnumerableClassInfo<Method, Parameter> ToBeAnalyzed {
      get {
        return new EnumerableClassInfo<Method, Parameter>(moveNext, fieldsHold);
      }
    }
    public EnumerableHelper(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> methodDriver) {
      this.methodDriver = methodDriver;
    }

    #region IValueAnalysis<APC,Domain,IVisitMSIL<APC,Local,Parameter,Method,Field,Type,Expression,Variable,Domain,Domain>,Variable,Expression,IExpressionContext<APC,Method,Type,Expression,Variable>> Members

    /// <summary>
    /// Here, we return the transfer function. Since we implement this via MSILVisitor, we just return this.
    ///
    /// </summary>
    /// <param name="context">The expression context is an interface we can use to find out more about expressions, such as their type etc.</param>
    /// <returns>The transfer function.</returns>
    public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Domain, Domain> Visitor() {
      return this;
    }

    protected override Domain Default(APC pc, Domain data) {
      return data;
    }

    public override Domain Newobj<ArgList>(APC pc, Method ctor, Variable dest, ArgList args, Domain data) {
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> decoder = this.methodDriver.MetaDataDecoder;
      string fullName = decoder.FullName(ctor);
      Type enumerableType = decoder.DeclaringType(ctor);
      IIndexable<Type> ignored;
      if (decoder.IsSpecialized(enumerableType, out ignored)) {
        enumerableType = decoder.Unspecialized(enumerableType);
      }
      Debug.Assert(enumerableType != null);
      foreach (Method m in decoder.Methods(enumerableType)) {
        if (decoder.Name(m) == "MoveNext") {
          this.moveNext = m;
          this.fieldsHold = FunctionalMap<string, int>.Empty;
          // DebugHelper.WriteLine("Find the desired MoveNext Method:{0}", m);
          break;
        }
      }
      return base.Newobj<ArgList>(pc, ctor, dest, args, data);
    }

    public override Domain Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, Domain data) {
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder = this.methodDriver.MetaDataDecoder;
      Expression exp = this.context.ExpressionContext.Refine(pc, value);
      IFullExpressionDecoder<Type, Variable, Expression> fullExpDecoder =
        ExternalExpressionDecoder.Create(mdDecoder, this.context);
      // BoxedExpressionDecoder beDecoder = BoxedExpressionDecoder.Decoder(fullExpDecoder);
      BoxedExpression be = BoxedExpression.For(exp, fullExpDecoder);
      IIndexable<Parameter> parameters = mdDecoder.Parameters(this.context.MethodContext.CurrentMethod);
      for (int i = 0, n = parameters.Count; i < n; i++) {
        Variable v;
        if (this.context.ValueContext.TryParameterValue(pc, parameters[i], out v))
        {
          if (v.Equals(value))
          {
            if (this.fieldsHold == null)
            {
              this.fieldsHold = FunctionalMap<string, int>.Empty;
            }
            this.fieldsHold = (FunctionalMap<string, int>)this.fieldsHold.Add(mdDecoder.Name(parameters[i]), i + 1);
            break;
          }
        }
      }
      return base.Stfld(pc, field, @volatile, obj, value, data);
    }
    /// <summary>
    /// Must implement the join/widen operation
    /// </summary>
    /// <param name="edge"></param>
    /// <param name="newState"></param>
    /// <param name="prevState"></param>
    /// <param name="weaker">should return false if result is less than or equal prevState.</param>
    /// <param name="widen">true if this is a widen operation. For our domain, this makes no difference</param>
    public Domain Join(Pair<APC, APC> edge, Domain newState, Domain prevState, out bool weaker, bool widen) {
      weaker = false;
      return prevState;
    }

    public bool IsBottom(APC pc, Domain state) {
      return false;
    }

    public bool IsTop(APC pc, Domain state)
    {
      return false;
    }

    public Domain ImmutableVersion(Domain state) {
      // our domain is pure
      return state;
    }

    public Domain MutableVersion(Domain state) {
      // our domain is pure
      return state;
    }

    public void Dump(Pair<Domain, System.IO.TextWriter> stateAndWriter) {
    }

    /// <summary>
    /// Here's where the actual work is. We get passed a list of pairs (source,targets) representing
    /// the assignments t = source for each t in targets.
    ///
    /// For our domain, if the source is in the non-null set, then we add all the targets to the non-null set.
    /// </summary>
    public Domain EdgeConversion(APC from, APC to, bool isJoin, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap, Domain state) {
      return state;
    }


    /// <summary>
    /// This method is called by the underlying driver of the fixpoint computation. It provides delegates for future lookup
    /// of the abstract state at given pcs.
    /// </summary>
    /// <returns>Return true only if you want the fixpoint computation to eagerly cache each pc state.</returns>
    public Predicate<APC> CacheStates(IFixpointInfo<APC, Domain> fixpointInfo) {
      return default(Predicate<APC>);
    }
    #endregion
  }

  /// <summary>
  /// An enumerable class refers to a helper class C# compiler builds for a method that returns
  /// an IEnumerable value. 
  /// </summary>
  public class EnumerableClassInfo<Method, Parameter> {
    public Method MoveNext;
    public FunctionalMap<string, int> FieldsHold;
    public EnumerableClassInfo(Method m, FunctionalMap<string, int> fieldsHold) {
      this.MoveNext = m;
      this.FieldsHold = fieldsHold;
    }
  }

  public class EnumerableClassSeeker<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, Expression, ILogOptions>
    where Type : IEquatable<Type>
    where Expression : IEquatable<Expression>
    where Variable : IEquatable<Variable> 
    where ILogOptions: IFrameworkLogOptions
  {
    public static EnumerableClassInfo<Method, Parameter> SeekEnumerableClass(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> methodDriver) {
      EnumerableHelper<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, Expression, ILogOptions> helper =
        new EnumerableHelper<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, Expression, ILogOptions>(methodDriver);
      Domain initVal = new Domain();
      methodDriver.ValueLayer.CreateForward(helper, new DFAOptions { })(initVal);
      return helper.ToBeAnalyzed;
    }
  }
}


