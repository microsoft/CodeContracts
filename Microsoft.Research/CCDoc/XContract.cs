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
using System.Xml;
using Microsoft.Cci.Contracts;
using System.Diagnostics.Contracts;
using Microsoft.Cci;
using Microsoft.Cci.MutableCodeModel;
using CSharpSourceEmitter;

namespace CCDoc {
  /// <summary>
  /// The kind of accessor.
  /// </summary>
  public enum XAccessorKind {
    /// <summary>
    /// A "get" accessor.
    /// </summary>
    Getter,
    /// <summary>
    /// A "set" accessor.
    /// </summary>
    Setter
  }
  /// <summary>
  /// A helper class for XAccessorKind.
  /// </summary>
  [ContractVerification(true)]
  public static class XAccessorKindHelper {
    /// <summary>
    /// Converts an XAccessorKind to a friendly string.
    /// </summary>
    public static string ToString(XAccessorKind kind) {
      switch (kind) {
        case XAccessorKind.Getter:
          return "getter";
        case XAccessorKind.Setter:
          return "setter";
        default:
          return null;
      }
    }
  }
  partial interface IContractWithException
  {
    void WriteExceptionTo(XmlWriter writer);
  }
  #region Contract binding
  [ContractClass(typeof(IContractWithExceptionContract))]
  partial interface IContractWithException
  {

  }

  [ContractClassFor(typeof(IContractWithException))]
  #endregion

  abstract class IContractWithExceptionContract : IContractWithException
  {

    #region IContractWithException Members

    void IContractWithException.WriteExceptionTo(XmlWriter writer)
    {
      Contract.Requires(writer != null);
    }

    #endregion
  }

  /// <summary>
  /// This custom class responsible for storing and writting contract information.
  /// </summary>
  [ContractVerification(true)]
  public class XContract {
    /// <summary>
    /// The id string of the method this contract was inherited from.
    /// </summary>
    readonly protected string inheritedFrom;
    /// <summary>
    /// The unqualified name of the type this contract was inherited from.
    /// </summary>
    readonly protected string inheritedFromTypeName;
    /// <summary>
    /// False by default, true after WriteTo is called.
    /// </summary>
    public bool wasWritten = false;
    /// <summary>
    /// Used for outputing error/debug messages.
    /// </summary>
    protected DocTracker docTracker;
    /// <summary>
    /// The host environment that corresponds to the contract. Needed, e.g.,
    /// for the VB source emitter.
    /// </summary>
    protected IMetadataHost host;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(docTracker != null);
    }

    /// <summary>
    /// Default constructor for an XContract.
    /// </summary>
    public XContract(IMetadataHost host, string inheritedFrom, string inheritedFromTypeName, DocTracker docTracker) {
      Contract.Requires(docTracker != null);

      this.host = host;
      this.inheritedFrom = inheritedFrom;
      this.inheritedFromTypeName = inheritedFromTypeName;
      this.docTracker = docTracker;
    }
    /// <summary>
    /// Writes the contract information into xml elements.
    /// </summary>
    public virtual void WriteTo(XmlWriter writer) {
      Contract.Requires(writer != null);
      wasWritten = true;
    }
    /// <summary>
    /// Gets the description string from a IContractElement
    /// </summary>
    protected static string GetDescription(IContractElement contract) {
      Contract.Requires(contract != null);

      ICompileTimeConstant descAsCTC = contract.Description as ICompileTimeConstant;
      if (descAsCTC != null && descAsCTC.Value != null)
        return descAsCTC.Value as string;      
      return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    protected string WriteCSharpFromAST(IExpression expression) {
      Contract.Requires(expression != null);

      SourceEmitterOutputString sourceEmitterOutput = new SourceEmitterOutputString();
      SourceEmitter CSSourceEmitter = new SourceEmitter(sourceEmitterOutput);
      ExpressionSimplifier es = new ExpressionSimplifier();
      expression = es.Rewrite(expression);
      CSSourceEmitter.Traverse(expression);
      return sourceEmitterOutput.Data;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    protected string WriteVBFromAST(IExpression expression) {
      Contract.Requires(expression != null);

      var sourceEmitterOutput = new VBSourceEmitter.SourceEmitterOutputString();
      var VBSourceEmitter = new VBSourceEmitter.SourceEmitter(this.host, sourceEmitterOutput);
      var es = new ExpressionSimplifier();
      expression = es.Rewrite(expression);
      VBSourceEmitter.Traverse(expression);
      return sourceEmitterOutput.Data;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    protected string WriteNeutralFromAST(IExpression expression) {
      Contract.Requires(expression != null);

      var sourceEmitterOutput = new SourceEmitterOutputString();
      var sourceEmitter = new NeutralSourceEmitter(sourceEmitterOutput);
      var es = new ExpressionSimplifier();
      expression = es.Rewrite(expression);
      sourceEmitter.Traverse(expression);
      return sourceEmitterOutput.Data;
    }

  }
  /// <summary>
  /// For storing, formatting and writting ITypeInvariant contracts.
  /// </summary>
  [ContractVerification(true)]
  public sealed class XInvariant : XContract {
    readonly ITypeInvariant invariant;
    /// <summary>
    /// Creates a new package for an ITypeInvariant contract.
    /// </summary>
    public XInvariant(IMetadataHost host, ITypeInvariant invariant, DocTracker docTracker)
      : base(host, null, null, docTracker) {
      Contract.Requires(invariant != null);
      Contract.Requires(docTracker != null);
      this.invariant = invariant;
    }

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.invariant != null);
    }

    /// <summary>
    /// Writes an ITypeInvariant into an xml element called "invariant".
    /// </summary>
    public override void WriteTo(XmlWriter writer) {
      string description = GetDescription(this.invariant);
      bool hasDescription = !String.IsNullOrEmpty(description);
      bool hasOriginalSource = !String.IsNullOrEmpty(this.invariant.OriginalSource);
      if (hasDescription || hasOriginalSource) {
        writer.WriteStartElement("invariant");
        if (hasDescription)
          writer.WriteAttributeString("description", description);
        if (hasOriginalSource)
          writer.WriteString(this.invariant.OriginalSource);
        else
          this.docTracker.WriteLine("Warning: No OriginalSource found.");
        writer.WriteEndElement();
      } else
        this.docTracker.WriteLine("Warning: Empty contract tag found.");
      base.WriteTo(writer);
    }
  }
  /// <summary>
  /// For storing, formatting and writting IPrecondition contracts.
  /// </summary>
  [ContractVerification(true)]
  public sealed class XPrecondition : XContract, IContractWithException {
    readonly IPrecondition precondition;
    readonly string exception;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(precondition != null);
    }

    /// <summary>
    /// Creates a new package for an IPrecondition contract.
    /// </summary>
    public XPrecondition(IMetadataHost host, IPrecondition precondition, string inheritedFrom, string inheritedFromTypeName, DocTracker docTracker)
      : base(host, inheritedFrom, inheritedFromTypeName, docTracker) {
      Contract.Requires(precondition != null);
      Contract.Requires(docTracker != null);
      this.precondition = precondition;
      this.exception = GetExceptionId(precondition);
    }
    /// <summary>
    /// Writes an IPrecondition into an xml element called "requires" and generates an "exception" element if the precondition has a exception defined.
    /// </summary>
    public override void WriteTo(XmlWriter writer) {
      string description = GetDescription(this.precondition);
      bool hasDescription = !String.IsNullOrEmpty(description);
      bool hasInheritedFrom = !String.IsNullOrEmpty(this.inheritedFrom);
      bool hasInheritedFromTypeName = !String.IsNullOrEmpty(this.inheritedFromTypeName);
      bool hasException = !String.IsNullOrEmpty(this.exception);
      bool hasOriginalSource = !String.IsNullOrEmpty(this.precondition.OriginalSource);
      if (hasDescription || hasInheritedFrom || hasInheritedFromTypeName || hasException || hasOriginalSource) {
        writer.WriteStartElement("requires");
        if (hasDescription)
          writer.WriteAttributeString("description", description);
        if (hasInheritedFrom)
          writer.WriteAttributeString("inheritedFrom", this.inheritedFrom);
        if (hasInheritedFromTypeName)
          writer.WriteAttributeString("inheritedFromTypeName", this.inheritedFromTypeName);
        if (hasException)
          writer.WriteAttributeString("exception", this.exception);

        var csharp = WriteCSharpFromAST(this.precondition.Condition);
        writer.WriteAttributeString("csharp", csharp);

        var vb = WriteVBFromAST(this.precondition.Condition);
        writer.WriteAttributeString("vb", vb);

        var n = WriteNeutralFromAST(this.precondition.Condition);
        writer.WriteString(n);

        //if (hasOriginalSource)
        //  writer.WriteString(this.precondition.OriginalSource);
        //else {
        //  this.docTracker.WriteLine("Warning: No OriginalSource found.");
        //  //Emit the condition instead of the OriginalSource
        //  var cSharpSource = WriteCSharpFromAST(this.precondition.Condition);
        //  writer.WriteString(cSharpSource);
        //}

        writer.WriteEndElement();
      } else
        this.docTracker.WriteLine("Warning: Empty contract tag found.");
      base.WriteTo(writer);
    }

    //private string WriteCSharpFromAST(IExpression expression) {
    //  SourceEmitterOutputString sourceEmitterOutput = new SourceEmitterOutputString();
    //  SourceEmitter CSSourceEmitter = new SourceEmitter(sourceEmitterOutput);
    //  ExpressionSimplifier es = new ExpressionSimplifier();
    //  expression = es.Rewrite(expression);
    //  CSSourceEmitter.Traverse(expression);
    //  return sourceEmitterOutput.Data;
    //}

    //private string WriteVBFromAST(IExpression expression) {
    //  var sourceEmitterOutput = new VBSourceEmitter.SourceEmitterOutputString();
    //  var VBSourceEmitter = new VBSourceEmitter.SourceEmitter(this.host, sourceEmitterOutput);
    //  var es = new ExpressionSimplifier();
    //  expression = es.Rewrite(expression);
    //  VBSourceEmitter.Traverse(expression);
    //  return sourceEmitterOutput.Data;
    //}

    /// <summary>
    /// Writes the exception thrown by this IPrecondition into an "exception" xml element.
    /// </summary>
    public void WriteExceptionTo(XmlWriter writer) {

      if (String.IsNullOrEmpty(this.exception)) return;
      writer.WriteStartElement("exception");
      writer.WriteAttributeString("cref", this.exception);
      if (!String.IsNullOrEmpty(this.precondition.OriginalSource))
        writer.WriteString(BooleanExpressionHelper.NegatePredicate(this.precondition.OriginalSource));
      else {
        this.docTracker.WriteLine("Warning: Writing exception, but no OriginalSource found.");
        //Emit the condition instead of the OriginalSource
        SourceEmitterOutputString sourceEmitterOutput = new SourceEmitterOutputString();
        SourceEmitter CSSourceEmitter = new SourceEmitter(sourceEmitterOutput);
        ExpressionSimplifier es = new ExpressionSimplifier();
        LogicalNot logicalNot = new LogicalNot();
        logicalNot.Operand = this.precondition.Condition;
        var condition = es.Rewrite(logicalNot);
        CSSourceEmitter.Traverse(condition);
        writer.WriteString(sourceEmitterOutput.Data);
      }
      writer.WriteEndElement();
    }
    private static string GetExceptionId(IPrecondition precondition) {
      Contract.Requires(precondition != null);
      if (precondition.ExceptionToThrow != null) {
        ITypeOf asTypeOf = precondition.ExceptionToThrow as ITypeOf;
        ITypeReference unspecializedExceptionType;        
        if (asTypeOf != null) 
          unspecializedExceptionType = MethodHelper.Unspecialize(asTypeOf.TypeToGet);
        else//Legacy contracts package exceptions differently then regular contracts.
          unspecializedExceptionType = MethodHelper.Unspecialize(precondition.ExceptionToThrow.Type);
        return TypeHelper.GetTypeName(unspecializedExceptionType, NameFormattingOptions.DocumentationId);
      }
      return null;
    }
  }
  /// <summary>
  /// For storing, formatting and writting IPostcondition contracts.
  /// </summary>
  [ContractVerification(true)]
  public sealed class XPostcondition : XContract {
    readonly IPostcondition postcondition;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.postcondition != null);
    }

    /// <summary>
    /// Creates a new package for an IPostcondition contract.
    /// </summary>
    public XPostcondition(IMetadataHost host, IPostcondition postcondition, string inheritedFrom, string inheritedFromTypeName, DocTracker docTracker)
      : base(host, inheritedFrom, inheritedFromTypeName, docTracker) {
      Contract.Requires(postcondition != null);
      Contract.Requires(docTracker != null);
      this.postcondition = postcondition;
    }
    /// <summary>
    /// Writes an IPostcondition into an xml element called "ensures".
    /// </summary>
    public override void WriteTo(XmlWriter writer) {
      string description = GetDescription(this.postcondition);
      bool hasDescription = !String.IsNullOrEmpty(description);
      bool hasInheritedFrom = !String.IsNullOrEmpty(this.inheritedFrom);
      bool hasInheritedFromTypeName = !String.IsNullOrEmpty(this.inheritedFromTypeName);
      bool hasOriginalSource = !String.IsNullOrEmpty(this.postcondition.OriginalSource);
      if (hasDescription || hasInheritedFrom || hasInheritedFromTypeName || hasOriginalSource) {
        writer.WriteStartElement("ensures");
        if (hasDescription)
          writer.WriteAttributeString("description", description);
        if (hasInheritedFrom)
          writer.WriteAttributeString("inheritedFrom", this.inheritedFrom);
        if (hasInheritedFromTypeName)
          writer.WriteAttributeString("inheritedFromTypeName", this.inheritedFromTypeName);

        var csharp = WriteCSharpFromAST(this.postcondition.Condition);
        writer.WriteAttributeString("csharp", csharp);

        var vb = WriteVBFromAST(this.postcondition.Condition);
        writer.WriteAttributeString("vb", vb);

        var n = WriteNeutralFromAST(this.postcondition.Condition);
        writer.WriteString(n);

        //if (hasOriginalSource)
        //  writer.WriteString(this.postcondition.OriginalSource);
        //else
        //  this.docTracker.WriteLine("Warning: No OriginalSource found.");
        
        writer.WriteEndElement();
      } else
        this.docTracker.WriteLine("Warning: Empty contract tag found.");
      base.WriteTo(writer);
    }
  }
  /// <summary>
  /// For storing, formatting and writting IThrownException contracts.
  /// </summary>
  [ContractVerification(true)]
  public sealed class XThrownException : XContract, IContractWithException {
    readonly IThrownException thrownException;
    readonly string exception;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(thrownException != null);
      Contract.Invariant(this.thrownException.Postcondition != null);
    }

    /// <summary>
    /// Creates a new package for an IThrownException contract.
    /// </summary>
    public XThrownException(IMetadataHost host, IThrownException thrownException, string inheritedFrom, string inheritedFromTypeName, DocTracker docTracker)
      : base(host, inheritedFrom, inheritedFromTypeName, docTracker) {
      Contract.Requires(docTracker != null);
      Contract.Requires(thrownException != null);
      Contract.Requires(thrownException.Postcondition != null);
      Contract.Requires(thrownException.ExceptionType != null);
      var post = thrownException.Postcondition;
      this.thrownException = thrownException;
      Contract.Assert(this.thrownException.Postcondition != null);
      var unspecializedExceptionType = MethodHelper.Unspecialize(thrownException.ExceptionType);
      Contract.Assert(this.thrownException.Postcondition != null);
      this.exception = TypeHelper.GetTypeName(unspecializedExceptionType, NameFormattingOptions.DocumentationId);
      Contract.Assert(this.thrownException == thrownException);
      Contract.Assert(thrownException.Postcondition == post);
      Contract.Assert(this.thrownException.Postcondition != null);
      }
    /// <summary>
    /// Writes an IThrownException into an xml element called "ensuresOnThrow".
    /// </summary>
    public override void WriteTo(XmlWriter writer) {
      string description = GetDescription(this.thrownException.Postcondition);
      bool hasDescription = !String.IsNullOrEmpty(description);
      bool hasInheritedFrom = !String.IsNullOrEmpty(this.inheritedFrom);
      bool hasInheritedFromTypeName = !String.IsNullOrEmpty(this.inheritedFromTypeName);
      bool hasException = !String.IsNullOrEmpty(this.exception);
      bool hasOriginalSource = !String.IsNullOrEmpty(this.thrownException.Postcondition.OriginalSource);
      if (hasDescription || hasInheritedFrom || hasInheritedFromTypeName || hasException || hasOriginalSource) {
        writer.WriteStartElement("ensuresOnThrow");
        if (hasDescription)
          writer.WriteAttributeString("description", description);
        if (hasInheritedFrom)
          writer.WriteAttributeString("inheritedFrom", this.inheritedFrom);
        if (hasInheritedFromTypeName)
          writer.WriteAttributeString("inheritedFromTypeName", this.inheritedFromTypeName);
        if (hasException)
          writer.WriteAttributeString("exception", this.exception);
        if (hasOriginalSource)
          writer.WriteString(this.thrownException.Postcondition.OriginalSource);
        else
          this.docTracker.WriteLine("Warning: No OriginalSource found.");
        writer.WriteEndElement();
      } else
        this.docTracker.WriteLine("Warning: Empty contract tag found.");
      base.WriteTo(writer);
    }
    /// <summary>
    /// Writes the exception referenced by this IThrownException into an "exception" xml element.
    /// </summary>
    public void WriteExceptionTo(XmlWriter writer) {

      if (String.IsNullOrEmpty(this.exception)) return;

      writer.WriteStartElement("exception");
      writer.WriteAttributeString("cref", this.exception);
      if(!String.IsNullOrEmpty(this.thrownException.Postcondition.OriginalSource))
        writer.WriteString(this.thrownException.Postcondition.OriginalSource + " will be true on throw.");
      else
        this.docTracker.WriteLine("Warning: Writing exception, but no OriginalSource found.");
      writer.WriteEndElement();
    }
  }
  /// <summary>
  /// For storing, formatting and writting contracts on accessors.
  /// </summary>
  /// <remarks>
  /// This is not a single contract but rather a collection of other XContracts.
  /// </remarks>
  [ContractVerification(true)]
  public sealed class XAccessorContract : XContract, IContractWithException {
    /// <summary>
    /// The kind of accessor.
    /// </summary>
    public readonly XAccessorKind kind;
    private readonly XContract[] contracts;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.contracts != null);
      Contract.Invariant(this.contracts.Length > 0);
    }

    /// <summary>
    /// Creates a new package for getter and setter accessors. Does not use the inheritedFrom string, that information is stored on individual contracts.
    /// </summary>
    public XAccessorContract(IMetadataHost host, XAccessorKind kind, XContract[] contracts, DocTracker docTracker)
      : base(host, null, null, docTracker) {
      Contract.Requires(docTracker != null);
      Contract.Requires(contracts != null);
      Contract.Requires(contracts.Length > 0);
      Contract.Requires(Contract.ForAll(contracts, c => c != null));

      this.kind = kind;
      this.contracts = contracts;
    }
    /// <summary>
    /// Writes a "getter" or "setter" element then populates it with XContracts useing XContract's WriteTo method.
    /// </summary>
    public override void WriteTo(XmlWriter writer) {
      writer.WriteStartElement(XAccessorKindHelper.ToString(this.kind));
      foreach (var contract in this.contracts) {
        Contract.Assume(contract != null);
        contract.WriteTo(writer);
      }
      writer.WriteEndElement();
      base.WriteTo(writer);
    }
    /// <summary>
    /// Writes all exceptions from preconditions or thrownExceptions into xml elements.
    /// </summary>
    public void WriteExceptionTo(XmlWriter writer) {

      foreach (var contract in this.contracts) {
        var contractWithException = contract as IContractWithException;
        if (contractWithException != null)
          contractWithException.WriteExceptionTo(writer);
      }
    }
  }
  /// <summary>
  /// For writting purity information.
  /// </summary>
  [ContractVerification(true)]
  public sealed class XPure : XContract {
    private bool isPure;
    /// <summary>
    /// Create a new XPure object.
    /// </summary>
    public XPure(IMetadataHost host, bool isPure, DocTracker docTracker)
      : base(host, null, null, docTracker) {
      Contract.Requires(docTracker != null);
      this.isPure = isPure;
    }
    /// <summary>
    /// Write a "pure" tag to the given writer.
    /// </summary>
    public override void WriteTo(XmlWriter writer) {
      if (this.isPure) {
        writer.WriteStartElement("pure");
        writer.WriteEndElement();
      }
      base.WriteTo(writer);
    }
  }


  class NeutralSourceEmitter : CSharpSourceEmitter.SourceEmitter {
    public NeutralSourceEmitter(ISourceEmitterOutput sourceEmitterOutput)
      : base(sourceEmitterOutput) {
    }
    public override void TraverseChildren(IOldValue oldValue) {
      base.sourceEmitterOutput.Write("old(");
      base.Traverse(oldValue.Expression);
      base.sourceEmitterOutput.Write(")");
    }
    public override void TraverseChildren(IReturnValue returnValue) {
      base.sourceEmitterOutput.Write("result");
    }
    public override void TraverseChildren(IMethodCall methodCall) {
      var mName = MemberHelper.GetMethodSignature(methodCall.MethodToCall);
      if (mName.Contains("System.Diagnostics.Contracts.Contract.ForAll")) {
        base.sourceEmitterOutput.Write("for all ");
        var n = methodCall.Arguments.Count();
        if (n == 2) {
          var lambda = methodCall.Arguments.ElementAt(1) as IAnonymousDelegate;
          sourceEmitterOutput.Write(lambda.Parameters.ElementAt(0).Name.Value);
          sourceEmitterOutput.Write(" in ");
          Traverse(methodCall.Arguments.ElementAt(0));
          sourceEmitterOutput.Write(" ");
          if (!PrintLambdaBody(lambda))
            Traverse(lambda.Body);
        } else {
          // n == 3
          var lambda = methodCall.Arguments.ElementAt(2) as IAnonymousDelegate;
          var paramName = lambda.Parameters.ElementAt(0).Name.Value;
          sourceEmitterOutput.Write(paramName);
          sourceEmitterOutput.Write(" where ");
          Traverse(methodCall.Arguments.ElementAt(0));
          sourceEmitterOutput.Write(" <= ");
          sourceEmitterOutput.Write(paramName);
          sourceEmitterOutput.Write(" < ");
          Traverse(methodCall.Arguments.ElementAt(1));
          sourceEmitterOutput.Write(" :: ");
          if (!PrintLambdaBody(lambda))
            Traverse(lambda.Body);
        }
        return;
      }
      base.TraverseChildren(methodCall);
    }
    private bool PrintLambdaBody(IAnonymousDelegate lambda) {
      if (lambda.Body.Statements.Count(s => !(s is IEmptyStatement)) != 1) return false;
      var nonEmptyStatement = lambda.Body.Statements.First(s => !(s is IEmptyStatement));

      var returnStatement = nonEmptyStatement as IReturnStatement;
      if (returnStatement != null && returnStatement.Expression != null) {
        this.Traverse(returnStatement.Expression);
        return true;
      }
      var expressionStatement = nonEmptyStatement as IExpressionStatement;
      if (expressionStatement != null) {
        this.Traverse(expressionStatement.Expression);
        return true;
      }
      return false;
    }
  }
}
