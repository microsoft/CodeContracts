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

// File System.CodeDom.Compiler.CodeDomProvider.cs
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
  abstract public partial class CodeDomProvider : System.ComponentModel.Component
  {
    #region Methods and constructors
    protected CodeDomProvider()
    {
    }

    public virtual new CompilerResults CompileAssemblyFromDom(CompilerParameters options, System.CodeDom.CodeCompileUnit[] compilationUnits)
    {
      return default(CompilerResults);
    }

    public virtual new CompilerResults CompileAssemblyFromFile(CompilerParameters options, string[] fileNames)
    {
      return default(CompilerResults);
    }

    public virtual new CompilerResults CompileAssemblyFromSource(CompilerParameters options, string[] sources)
    {
      return default(CompilerResults);
    }

    public abstract ICodeCompiler CreateCompiler();

    public virtual new string CreateEscapedIdentifier(string value)
    {
      return default(string);
    }

    public virtual new ICodeGenerator CreateGenerator(TextWriter output)
    {
      return default(ICodeGenerator);
    }

    public virtual new ICodeGenerator CreateGenerator(string fileName)
    {
      return default(ICodeGenerator);
    }

    public abstract ICodeGenerator CreateGenerator();

    public virtual new ICodeParser CreateParser()
    {
      return default(ICodeParser);
    }

    public static CodeDomProvider CreateProvider(string language, IDictionary<string, string> providerOptions)
    {
      return default(CodeDomProvider);
    }

    public static CodeDomProvider CreateProvider(string language)
    {
      return default(CodeDomProvider);
    }

    public virtual new string CreateValidIdentifier(string value)
    {
      return default(string);
    }

    public virtual new void GenerateCodeFromCompileUnit(System.CodeDom.CodeCompileUnit compileUnit, TextWriter writer, CodeGeneratorOptions options)
    {
    }

    public virtual new void GenerateCodeFromExpression(System.CodeDom.CodeExpression expression, TextWriter writer, CodeGeneratorOptions options)
    {
    }

    public virtual new void GenerateCodeFromMember(System.CodeDom.CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
    {
    }

    public virtual new void GenerateCodeFromNamespace(System.CodeDom.CodeNamespace codeNamespace, TextWriter writer, CodeGeneratorOptions options)
    {
    }

    public virtual new void GenerateCodeFromStatement(System.CodeDom.CodeStatement statement, TextWriter writer, CodeGeneratorOptions options)
    {
    }

    public virtual new void GenerateCodeFromType(System.CodeDom.CodeTypeDeclaration codeType, TextWriter writer, CodeGeneratorOptions options)
    {
    }

    public static CompilerInfo[] GetAllCompilerInfo()
    {
      return default(CompilerInfo[]);
    }

    public static CompilerInfo GetCompilerInfo(string language)
    {
      Contract.Ensures(Contract.Result<System.CodeDom.Compiler.CompilerInfo>() != null);

      return default(CompilerInfo);
    }

    public virtual new System.ComponentModel.TypeConverter GetConverter(Type type)
    {
      return default(System.ComponentModel.TypeConverter);
    }

    public static string GetLanguageFromExtension(string extension)
    {
      return default(string);
    }

    public virtual new string GetTypeOutput(System.CodeDom.CodeTypeReference type)
    {
      return default(string);
    }

    public static bool IsDefinedExtension(string extension)
    {
      return default(bool);
    }

    public static bool IsDefinedLanguage(string language)
    {
      return default(bool);
    }

    public virtual new bool IsValidIdentifier(string value)
    {
      return default(bool);
    }

    public virtual new System.CodeDom.CodeCompileUnit Parse(TextReader codeStream)
    {
      return default(System.CodeDom.CodeCompileUnit);
    }

    public virtual new bool Supports(GeneratorSupport generatorSupport)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public virtual new string FileExtension
    {
      get
      {
        return default(string);
      }
    }

    public virtual new LanguageOptions LanguageOptions
    {
      get
      {
        return default(LanguageOptions);
      }
    }
    #endregion
  }
}
