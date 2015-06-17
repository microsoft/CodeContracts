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
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis {

  public class ExpressionCodeVisitor<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>
  : CodeVisitor<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly,
                IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable>,
                IFunctionalMap<Variable,FList<Variable>>>
  where Type : IEquatable<Type>
  {
  }

  public class ValueCodeVisitor<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly>
    : CodeVisitor<Local, Parameter, Method, Field, Property, Event, Type, Variable, Variable, Attribute, Assembly,
                  IValueContext<Local, Parameter, Method, Field, Type, Variable>,
                  IFunctionalMap<Variable,FList<Variable>>>
    where Type : IEquatable<Type>
  {
  }

  /// <summary>
  /// This crawler visits each instruction once (currently ignores exception paths)
  ///
  /// Subclasses should override the MSIL instructions at which they want to perform certain actions, such
  /// as gathering facts.
  /// </summary>
  public class CodeVisitor<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly, ContextData, EdgeData>
    : MSILVisitor<APC, Local, Parameter, Method, Field, Type, Expression, Variable, bool, bool>
    , IAnalysis<APC, bool, 
                IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, bool, bool>,
                EdgeData>
    where Type : IEquatable<Type>
    where ContextData : IMethodContext<Field, Method>
  {

    #region State
    ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeData> codeLayer;
    #endregion

    #region Main Entry point
    public void Run(
      ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeData> codeLayer
      )
    {
      Contract.Requires(codeLayer != null);

      this.codeLayer = codeLayer;
      var closure = codeLayer.CreateForward<bool>(this, this.Options);
      closure(true);   // Do the analysis 
    }
    #endregion

    #region Available to sub classes
    public DFAOptions Options = new DFAOptions();
    public ContextData Context { get { return this.codeLayer.Decoder.Context; } }
    protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder { get { return this.codeLayer.MetaDataDecoder; } }
    #endregion

    #region IValueAnalysis<APC,bool,IVisitMSIL<APC,Local,Parameter,Method,Field,Type,Variable,Variable,bool,bool>,Variable,IExpressionContext<APC,Local,Parameter,Method,Field,Type,Expression,Variable>> Members

    public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, bool, bool> Visitor()
    {
      return this;
    }

    // Made virtual to count the # of join points in the SyntacticComplexityAnalysis
    virtual public bool Join(Microsoft.Research.DataStructures.Pair<APC, APC> edge, bool newState, bool prevState, out bool weaker, bool widen)
    {
      weaker = false;
      return true;
    }

    public bool IsBottom(APC pc, bool state)
    {
      return state == false;
    }

    public bool IsTop(APC pc, bool state)
    {
      return false;
    }

    public bool ImmutableVersion(bool state)
    {
      return state;
    }

    public bool MutableVersion(bool state)
    {
      return state;
    }

    public void Dump(Microsoft.Research.DataStructures.Pair<bool, System.IO.TextWriter> stateAndWriter)
    {
      // do nothing
    }

    public bool EdgeConversion(APC from, APC next, bool joinPoint, EdgeData edgeData, bool state)
    {
      return state;
    }

    public Predicate<APC> CacheStates(IFixpointInfo<APC, bool> fixpointInfo)
    {
      return null;
    }

    #endregion

    protected override bool Default(APC pc, bool data)
    {
      return data;
    }
  }
}
