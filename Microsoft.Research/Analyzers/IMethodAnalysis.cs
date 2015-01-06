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
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using System.IO;

namespace Microsoft.Research.CodeAnalysis
{

  [ContractClass(typeof(IProofObligationsContracts<,>))]
  public interface IProofObligations<Variable, HighLevelExpression>
  {
    string Name { get; }

    double Validate(IOutputResults output, ContractInferenceManager inferenceManager, IFactQuery<HighLevelExpression, Variable> query);

    bool PCWithProofObligation(APC pc);

    /// <summary>
    /// Is there a proof obligation at the given pc? If yes, which are those conditions?
    /// The implementer should append the conditions to conditions
    /// </summary>
    bool PCWithProofObligations(APC pc, List<ProofObligationBase<HighLevelExpression, Variable>> conditions);

    AnalysisStatistics Statistics { get; }
  }

  #region Contracts for IProogObligations

  [ContractClassFor(typeof(IProofObligations<,>))]
  abstract class IProofObligationsContracts<Variable, HighLevelExpression> : IProofObligations<Variable, HighLevelExpression>
  {
    #region IProofObligations<Variable,HighLevelExpression> Members

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public double Validate(IOutputResults output, ContractInferenceManager inferenceManager, IFactQuery<HighLevelExpression, Variable> query)
    {
      Contract.Requires(output != null);
      Contract.Requires(inferenceManager != null);
      Contract.Requires(query != null);

      throw new NotImplementedException();
    }

    public bool PCWithProofObligation(APC pc)
    {
      return default(bool);
    }

    public bool PCWithProofObligations(APC pc, List<ProofObligationBase<HighLevelExpression, Variable>> conditions)
    {
      Contract.Requires(conditions != null);  // The caller should not pass null
      Contract.Ensures(conditions.Count >= Contract.OldValue(conditions.Count)); // the callee should not delete any condition

      return default(bool);
    }

    public IProofObligations<Variable, HighLevelExpression> Where(Func<APC, bool> condition)
    {
      throw new NotImplementedException();
    }

    public AnalysisStatistics Statistics
    {
      get
      {
        return default(AnalysisStatistics);
      }
    }

    #endregion



  }

  #endregion

  public interface IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T>
    where Type : IEquatable<Type>
  {
    T Create<AState>(IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, AState, Variable> analysis);
  }

  public interface IMethodAnalysis
  {
    string Name { get; }
    bool ObligationsEnabled { get; }

    /// <summary>
    /// Return false if options or args were wrong and initialization failed
    /// </summary>
    bool Initialize(ILogOptions options, string[] args);

    IProofObligations<Variable, BoxedExpression>
      GetProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      (
        string fullMethodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver
      )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>;

    IMethodResult<Variable> Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
        string fullMethodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        Predicate<APC> cachePCs,
        IFactQuery<BoxedExpression, Variable> factQuery
      )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>;

    T Instantiate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T>(
      string fullMethodName,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
      Predicate<APC> cachePCs,
      IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T> factory
    )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>;

    void PrintOptions(string indent, TextWriter output);
    void PrintAnalysisSpecificStatistics(IOutput output);

    void SetDependency(IMethodAnalysis otherInfo);

    /// <summary>
    /// Mic: Enables an outside analysis (for example a class analysis) to launch some computation on abstract domain data
    /// without actually needing to know in advance which abstract domain the analysis uses.
    /// </summary>
    bool ExecuteAbstractDomainFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options, Result, Data>(
      IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options, IMethodResult<Variable>> cdriver,
      IResultsFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result, Data> functor,
      Data data,
      out Result result)
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
      where Options : IFrameworkLogOptions;

    IEnumerable<OptionParsing> Options { get; }
  }


  public interface IMethodResult<Variable>
  {
    IFactQuery<BoxedExpression, Variable> FactQuery { get; }

    /// <summary>
    /// Get the postcondition for the method.
    /// </summary>    
    /// <returns>True, if we want to generate a witness</returns>
    bool SuggestPostcondition(ContractInferenceManager inferenceManager);

    /// <summary>
    /// Get precondition for the method
    /// </summary>
    void SuggestPrecondition(ContractInferenceManager inferenceManager);

    /// <summary>
    /// Get the installed postconditions as a sequence of boxed expressions
    /// </summary>
    IEnumerable<BoxedExpression> GetPostconditionAsExpression();
  }

  public interface IAnalysisConsumer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result>
    where Type : IEquatable<Type>
  {
    Result CallBack<AState>(IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, AState, Variable> analysis);
  }


  public interface IAbstractDomainOperations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ADomain>
    where Type : IEquatable<Type>
    where Expression : IEquatable<Expression>
  {
    /// <summary>
    /// Return the abstract state of the analysis at the control point <code>pc</code>
    /// </summary>
    bool LookupState(IMethodResult<Variable> mr, APC pc, out ADomain astate);

    /// <summary>
    /// Return the join of the two abstract states
    /// </summary>
    ADomain Join(IMethodResult<Variable> mr, ADomain astate1, ADomain astate2);

    /// <summary>
    /// Return expressions corresponding to the abstract state
    /// </summary>
    List<BoxedExpression> ExtractAssertions(
      IMethodResult<Variable> mr,
      ADomain astate,
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder);

    /// <summary>
    /// Translate the variables from the vocabulary at a particular control point to
    /// the ones at some other control point
    /// </summary>
    /// <param name="mapping">Dictionary (reference value, [aliases])</param>
    bool AssignInParallel(IMethodResult<Variable> mr, ref ADomain astate, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> mapping, Converter<BoxedVariable<Variable>, BoxedExpression> convert);
  }

  public interface IResultsFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result, DataType>
    where Type : IEquatable<Type>
    where Expression : IEquatable<Expression>
  {
    /// <summary>
    /// Execute the operation on the data through an abstract domain
    /// </summary>
    bool Execute<ADomain>(IAbstractDomainOperations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ADomain> ado, DataType data, out Result result);
  }
}
