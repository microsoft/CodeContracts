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

// File System.CodeDom.Compiler.CodeGenerator.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.CodeDom.Compiler
{
  abstract public partial class CodeGenerator : ICodeGenerator
  {
    #region Methods and constructors
    protected CodeGenerator()
    {
    }

    protected virtual new void ContinueOnNewLine(string st)
    {
      Contract.Requires(this.Output != null);
    }

    protected abstract string CreateEscapedIdentifier(string value);

    protected abstract string CreateValidIdentifier(string value);

    protected abstract void GenerateArgumentReferenceExpression(System.CodeDom.CodeArgumentReferenceExpression e);

    protected abstract void GenerateArrayCreateExpression(System.CodeDom.CodeArrayCreateExpression e);

    protected abstract void GenerateArrayIndexerExpression(System.CodeDom.CodeArrayIndexerExpression e);

    protected abstract void GenerateAssignStatement(System.CodeDom.CodeAssignStatement e);

    protected abstract void GenerateAttachEventStatement(System.CodeDom.CodeAttachEventStatement e);

    protected abstract void GenerateAttributeDeclarationsEnd(System.CodeDom.CodeAttributeDeclarationCollection attributes);

    protected abstract void GenerateAttributeDeclarationsStart(System.CodeDom.CodeAttributeDeclarationCollection attributes);

    protected abstract void GenerateBaseReferenceExpression(System.CodeDom.CodeBaseReferenceExpression e);

    protected virtual new void GenerateBinaryOperatorExpression(System.CodeDom.CodeBinaryOperatorExpression e)
    {
      Contract.Requires(e != null);
      Contract.Requires(this.Output != null);
    }

    protected abstract void GenerateCastExpression(System.CodeDom.CodeCastExpression e);

    public virtual new void GenerateCodeFromMember(System.CodeDom.CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
    {
    }

    protected abstract void GenerateComment(System.CodeDom.CodeComment e);

    protected virtual new void GenerateCommentStatement(System.CodeDom.CodeCommentStatement e)
    {
      Contract.Requires(e != null);
    }

    protected virtual new void GenerateCommentStatements(System.CodeDom.CodeCommentStatementCollection e)
    {
      Contract.Requires(e != null);
    }

    protected virtual new void GenerateCompileUnit(System.CodeDom.CodeCompileUnit e)
    {
      Contract.Requires(e != null);
    }

    protected virtual new void GenerateCompileUnitEnd(System.CodeDom.CodeCompileUnit e)
    {
      Contract.Requires(e != null);
    }

    protected virtual new void GenerateCompileUnitStart(System.CodeDom.CodeCompileUnit e)
    {
      Contract.Requires(e != null);
    }

    protected abstract void GenerateConditionStatement(System.CodeDom.CodeConditionStatement e);

    protected abstract void GenerateConstructor(System.CodeDom.CodeConstructor e, System.CodeDom.CodeTypeDeclaration c);

    protected virtual new void GenerateDecimalValue(Decimal d)
    {
      Contract.Requires(this.Output != null);
    }

    protected virtual new void GenerateDefaultValueExpression(System.CodeDom.CodeDefaultValueExpression e)
    {
    }

    protected abstract void GenerateDelegateCreateExpression(System.CodeDom.CodeDelegateCreateExpression e);

    protected abstract void GenerateDelegateInvokeExpression(System.CodeDom.CodeDelegateInvokeExpression e);

    protected virtual new void GenerateDirectionExpression(System.CodeDom.CodeDirectionExpression e)
    {
      Contract.Requires(e != null);
    }

    protected virtual new void GenerateDirectives(System.CodeDom.CodeDirectiveCollection directives)
    {
    }

    protected virtual new void GenerateDoubleValue(double d)
    {
      Contract.Requires(this.Output != null);
    }

    protected abstract void GenerateEntryPointMethod(System.CodeDom.CodeEntryPointMethod e, System.CodeDom.CodeTypeDeclaration c);

    protected abstract void GenerateEvent(System.CodeDom.CodeMemberEvent e, System.CodeDom.CodeTypeDeclaration c);

    protected abstract void GenerateEventReferenceExpression(System.CodeDom.CodeEventReferenceExpression e);

    protected void GenerateExpression(System.CodeDom.CodeExpression e)
    {
    }

    protected abstract void GenerateExpressionStatement(System.CodeDom.CodeExpressionStatement e);

    protected abstract void GenerateField(System.CodeDom.CodeMemberField e);

    protected abstract void GenerateFieldReferenceExpression(System.CodeDom.CodeFieldReferenceExpression e);

    protected abstract void GenerateGotoStatement(System.CodeDom.CodeGotoStatement e);

    protected abstract void GenerateIndexerExpression(System.CodeDom.CodeIndexerExpression e);

    protected abstract void GenerateIterationStatement(System.CodeDom.CodeIterationStatement e);

    protected abstract void GenerateLabeledStatement(System.CodeDom.CodeLabeledStatement e);

    protected abstract void GenerateLinePragmaEnd(System.CodeDom.CodeLinePragma e);

    protected abstract void GenerateLinePragmaStart(System.CodeDom.CodeLinePragma e);

    protected abstract void GenerateMethod(System.CodeDom.CodeMemberMethod e, System.CodeDom.CodeTypeDeclaration c);

    protected abstract void GenerateMethodInvokeExpression(System.CodeDom.CodeMethodInvokeExpression e);

    protected abstract void GenerateMethodReferenceExpression(System.CodeDom.CodeMethodReferenceExpression e);

    protected abstract void GenerateMethodReturnStatement(System.CodeDom.CodeMethodReturnStatement e);

    protected virtual new void GenerateNamespace(System.CodeDom.CodeNamespace e)
    {
      Contract.Requires(e != null);
      Contract.Requires(e.Comments != null);
    }

    protected abstract void GenerateNamespaceEnd(System.CodeDom.CodeNamespace e);

    protected abstract void GenerateNamespaceImport(System.CodeDom.CodeNamespaceImport e);

    protected void GenerateNamespaceImports(System.CodeDom.CodeNamespace e)
    {
      Contract.Requires(e != null);
      Contract.Requires(e.Imports != null);
    }

    protected void GenerateNamespaces(System.CodeDom.CodeCompileUnit e)
    {
      Contract.Requires(e != null);
      Contract.Requires(e.Namespaces != null);
    }

    protected abstract void GenerateNamespaceStart(System.CodeDom.CodeNamespace e);

    protected abstract void GenerateObjectCreateExpression(System.CodeDom.CodeObjectCreateExpression e);

    protected virtual new void GenerateParameterDeclarationExpression(System.CodeDom.CodeParameterDeclarationExpression e)
    {
      Contract.Requires(e != null);
    }

    protected virtual new void GeneratePrimitiveExpression(System.CodeDom.CodePrimitiveExpression e)
    {
      Contract.Requires(e != null);
    }

    protected abstract void GenerateProperty(System.CodeDom.CodeMemberProperty e, System.CodeDom.CodeTypeDeclaration c);

    protected abstract void GeneratePropertyReferenceExpression(System.CodeDom.CodePropertyReferenceExpression e);

    protected abstract void GeneratePropertySetValueReferenceExpression(System.CodeDom.CodePropertySetValueReferenceExpression e);

    protected abstract void GenerateRemoveEventStatement(System.CodeDom.CodeRemoveEventStatement e);

    protected virtual new void GenerateSingleFloatValue(float s)
    {
      Contract.Requires(this.Output != null);
    }

    protected virtual new void GenerateSnippetCompileUnit(System.CodeDom.CodeSnippetCompileUnit e)
    {
      Contract.Requires(e != null);
    }

    protected abstract void GenerateSnippetExpression(System.CodeDom.CodeSnippetExpression e);

    protected abstract void GenerateSnippetMember(System.CodeDom.CodeSnippetTypeMember e);

    protected virtual new void GenerateSnippetStatement(System.CodeDom.CodeSnippetStatement e)
    {
      Contract.Requires(e != null);
      Contract.Requires(this.Output != null);
    }

    protected void GenerateStatement(System.CodeDom.CodeStatement e)
    {
      Contract.Requires(e != null);
    }

    protected void GenerateStatements(System.CodeDom.CodeStatementCollection stms)
    {
      Contract.Requires(stms != null);
    }

    protected abstract void GenerateThisReferenceExpression(System.CodeDom.CodeThisReferenceExpression e);

    protected abstract void GenerateThrowExceptionStatement(System.CodeDom.CodeThrowExceptionStatement e);

    protected abstract void GenerateTryCatchFinallyStatement(System.CodeDom.CodeTryCatchFinallyStatement e);

    protected abstract void GenerateTypeConstructor(System.CodeDom.CodeTypeConstructor e);

    protected abstract void GenerateTypeEnd(System.CodeDom.CodeTypeDeclaration e);

    protected virtual new void GenerateTypeOfExpression(System.CodeDom.CodeTypeOfExpression e)
    {
      Contract.Requires(e != null);
      Contract.Requires(this.Output != null);
    }

    protected virtual new void GenerateTypeReferenceExpression(System.CodeDom.CodeTypeReferenceExpression e)
    {
      Contract.Requires(e != null);
    }

    protected void GenerateTypes(System.CodeDom.CodeNamespace e)
    {
      Contract.Requires(e != null);
      Contract.Requires(e.Types != null);
    }

    protected abstract void GenerateTypeStart(System.CodeDom.CodeTypeDeclaration e);

    protected abstract void GenerateVariableDeclarationStatement(System.CodeDom.CodeVariableDeclarationStatement e);

    protected abstract void GenerateVariableReferenceExpression(System.CodeDom.CodeVariableReferenceExpression e);

    protected abstract string GetTypeOutput(System.CodeDom.CodeTypeReference value);

    protected abstract bool IsValidIdentifier(string value);

    public static bool IsValidLanguageIndependentIdentifier(string value)
    {
      Contract.Requires(value != null);

      return default(bool);
    }

    protected virtual new void OutputAttributeArgument(System.CodeDom.CodeAttributeArgument arg)
    {
      Contract.Requires(arg != null);
    }

    protected virtual new void OutputAttributeDeclarations(System.CodeDom.CodeAttributeDeclarationCollection attributes)
    {
      Contract.Requires(attributes != null);
    }

    protected virtual new void OutputDirection(System.CodeDom.FieldDirection dir)
    {
    }

    protected virtual new void OutputExpressionList(System.CodeDom.CodeExpressionCollection expressions, bool newlineBetweenItems)
    {
      Contract.Requires(expressions != null);
    }

    protected virtual new void OutputExpressionList(System.CodeDom.CodeExpressionCollection expressions)
    {
      Contract.Requires(expressions != null);
    }

    protected virtual new void OutputFieldScopeModifier(System.CodeDom.MemberAttributes attributes)
    {
    }

    protected virtual new void OutputIdentifier(string ident)
    {
      Contract.Requires(this.Output != null);
    }

    protected virtual new void OutputMemberAccessModifier(System.CodeDom.MemberAttributes attributes)
    {
    }

    protected virtual new void OutputMemberScopeModifier(System.CodeDom.MemberAttributes attributes)
    {
    }

    protected virtual new void OutputOperator(System.CodeDom.CodeBinaryOperatorType op)
    {
    }

    protected virtual new void OutputParameters(System.CodeDom.CodeParameterDeclarationExpressionCollection parameters)
    {
      Contract.Requires(parameters != null);
    }

    protected abstract void OutputType(System.CodeDom.CodeTypeReference typeRef);

    protected virtual new void OutputTypeAttributes(System.Reflection.TypeAttributes attributes, bool isStruct, bool isEnum)
    {
    }

    protected virtual new void OutputTypeNamePair(System.CodeDom.CodeTypeReference typeRef, string name)
    {
    }

    protected abstract string QuoteSnippetString(string value);

    protected abstract bool Supports(GeneratorSupport support);

    string System.CodeDom.Compiler.ICodeGenerator.CreateEscapedIdentifier(string value)
    {
      return default(string);
    }

    string System.CodeDom.Compiler.ICodeGenerator.CreateValidIdentifier(string value)
    {
      return default(string);
    }

    void System.CodeDom.Compiler.ICodeGenerator.GenerateCodeFromCompileUnit(System.CodeDom.CodeCompileUnit e, TextWriter w, CodeGeneratorOptions o)
    {
    }

    void System.CodeDom.Compiler.ICodeGenerator.GenerateCodeFromExpression(System.CodeDom.CodeExpression e, TextWriter w, CodeGeneratorOptions o)
    {
    }

    void System.CodeDom.Compiler.ICodeGenerator.GenerateCodeFromNamespace(System.CodeDom.CodeNamespace e, TextWriter w, CodeGeneratorOptions o)
    {
    }

    void System.CodeDom.Compiler.ICodeGenerator.GenerateCodeFromStatement(System.CodeDom.CodeStatement e, TextWriter w, CodeGeneratorOptions o)
    {
    }

    void System.CodeDom.Compiler.ICodeGenerator.GenerateCodeFromType(System.CodeDom.CodeTypeDeclaration e, TextWriter w, CodeGeneratorOptions o)
    {
    }

    string System.CodeDom.Compiler.ICodeGenerator.GetTypeOutput(System.CodeDom.CodeTypeReference type)
    {
      return default(string);
    }

    bool System.CodeDom.Compiler.ICodeGenerator.IsValidIdentifier(string value)
    {
      return default(bool);
    }

    bool System.CodeDom.Compiler.ICodeGenerator.Supports(GeneratorSupport support)
    {
      return default(bool);
    }

    void System.CodeDom.Compiler.ICodeGenerator.ValidateIdentifier(string value)
    {
    }

    protected virtual new void ValidateIdentifier(string value)
    {
    }

    public static void ValidateIdentifiers(System.CodeDom.CodeObject e)
    {
    }
    #endregion

    #region Properties and indexers
    protected System.CodeDom.CodeTypeDeclaration CurrentClass
    {
      get
      {
        return default(System.CodeDom.CodeTypeDeclaration);
      }
    }

    protected System.CodeDom.CodeTypeMember CurrentMember
    {
      get
      {
        return default(System.CodeDom.CodeTypeMember);
      }
    }

    protected string CurrentMemberName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    protected string CurrentTypeName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    protected int Indent
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    protected bool IsCurrentClass
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsCurrentDelegate
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsCurrentEnum
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsCurrentInterface
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsCurrentStruct
    {
      get
      {
        return default(bool);
      }
    }

    protected abstract string NullToken
    {
      get;
    }

    protected CodeGeneratorOptions Options
    {
      get
      {
        return default(CodeGeneratorOptions);
      }
    }

    protected TextWriter Output
    {
      get
      {
        return default(TextWriter);
      }
    }
    #endregion
  }
}
