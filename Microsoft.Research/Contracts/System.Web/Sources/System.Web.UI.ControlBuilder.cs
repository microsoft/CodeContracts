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

// File System.Web.UI.ControlBuilder.cs
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


namespace System.Web.UI
{
  public partial class ControlBuilder
  {
    #region Methods and constructors
    public virtual new bool AllowWhitespaceLiterals()
    {
      return default(bool);
    }

    public virtual new void AppendLiteralString(string s)
    {
    }

    public virtual new void AppendSubBuilder(ControlBuilder subBuilder)
    {
    }

    public virtual new Object BuildObject()
    {
      return default(Object);
    }

    public virtual new void CloseControl()
    {
    }

    public ControlBuilder()
    {
    }

    public static ControlBuilder CreateBuilderFromType(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, System.Collections.IDictionary attribs, int line, string sourceFileName)
    {
      return default(ControlBuilder);
    }

    public virtual new Type GetChildControlType(string tagName, System.Collections.IDictionary attribs)
    {
      return default(Type);
    }

    public ObjectPersistData GetObjectPersistData()
    {
      return default(ObjectPersistData);
    }

    public string GetResourceKey()
    {
      return default(string);
    }

    public virtual new bool HasBody()
    {
      return default(bool);
    }

    public virtual new bool HtmlDecodeLiterals()
    {
      return default(bool);
    }

    public virtual new void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, System.Collections.IDictionary attribs)
    {
    }

    public virtual new bool NeedsTagInnerText()
    {
      return default(bool);
    }

    public virtual new void OnAppendToParentBuilder(ControlBuilder parentBuilder)
    {
    }

    public virtual new void ProcessGeneratedCode(System.CodeDom.CodeCompileUnit codeCompileUnit, System.CodeDom.CodeTypeDeclaration baseType, System.CodeDom.CodeTypeDeclaration derivedType, System.CodeDom.CodeMemberMethod buildMethod, System.CodeDom.CodeMemberMethod dataBindingMethod)
    {
    }

    public void SetResourceKey(string resourceKey)
    {
    }

    public void SetServiceProvider(IServiceProvider serviceProvider)
    {
    }

    public virtual new void SetTagInnerText(string text)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new Type BindingContainerType
    {
      get
      {
        return default(Type);
      }
    }

    public Type ControlType
    {
      get
      {
        return default(Type);
      }
    }

    public IFilterResolutionService CurrentFilterResolutionService
    {
      get
      {
        return default(IFilterResolutionService);
      }
    }

    public virtual new Type DeclareType
    {
      get
      {
        return default(Type);
      }
    }

    protected bool FChildrenAsProperties
    {
      get
      {
        return default(bool);
      }
    }

    protected bool FIsNonParserAccessor
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool HasAspCode
    {
      get
      {
        return default(bool);
      }
    }

    public string ID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected bool InDesigner
    {
      get
      {
        return default(bool);
      }
    }

    protected bool InPageTheme
    {
      get
      {
        return default(bool);
      }
    }

    public bool Localize
    {
      get
      {
        return default(bool);
      }
    }

    public Type NamingContainerType
    {
      get
      {
        return default(Type);
      }
    }

    internal protected TemplateParser Parser
    {
      get
      {
        return default(TemplateParser);
      }
    }

    public IServiceProvider ServiceProvider
    {
      get
      {
        return default(IServiceProvider);
      }
    }

    public string TagName
    {
      get
      {
        return default(string);
      }
    }

    public IThemeResolutionService ThemeResolutionService
    {
      get
      {
        return default(IThemeResolutionService);
      }
    }
    #endregion

    #region Fields
    public readonly static string DesignerFilter;
    #endregion
  }
}
