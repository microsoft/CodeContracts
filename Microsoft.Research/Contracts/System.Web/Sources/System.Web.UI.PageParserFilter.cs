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

// File System.Web.UI.PageParserFilter.cs
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
  abstract public partial class PageParserFilter
  {
    #region Methods and constructors
    protected void AddControl(Type type, System.Collections.IDictionary attributes)
    {
    }

    public virtual new bool AllowBaseType(Type baseType)
    {
      return default(bool);
    }

    public virtual new bool AllowControl(Type controlType, ControlBuilder builder)
    {
      return default(bool);
    }

    public virtual new bool AllowServerSideInclude(string includeVirtualPath)
    {
      return default(bool);
    }

    public virtual new bool AllowVirtualReference(string referenceVirtualPath, VirtualReferenceType referenceType)
    {
      return default(bool);
    }

    public virtual new CompilationMode GetCompilationMode(CompilationMode current)
    {
      return default(CompilationMode);
    }

    public virtual new Type GetNoCompileUserControlType()
    {
      return default(Type);
    }

    protected virtual new void Initialize()
    {
    }

    protected PageParserFilter()
    {
    }

    public virtual new void ParseComplete(ControlBuilder rootBuilder)
    {
    }

    public virtual new void PreprocessDirective(string directiveName, System.Collections.IDictionary attributes)
    {
    }

    public virtual new bool ProcessCodeConstruct(CodeConstructType codeType, string code)
    {
      return default(bool);
    }

    public virtual new bool ProcessDataBindingAttribute(string controlId, string name, string value)
    {
      return default(bool);
    }

    public virtual new bool ProcessEventHookup(string controlId, string eventName, string handlerName)
    {
      return default(bool);
    }

    protected void SetPageProperty(string filter, string name, string value)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool AllowCode
    {
      get
      {
        return default(bool);
      }
    }

    protected bool CalledFromParseControl
    {
      get
      {
        return default(bool);
      }
      private set
      {
      }
    }

    protected int Line
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int NumberOfControlsAllowed
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int NumberOfDirectDependenciesAllowed
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int TotalNumberOfDependenciesAllowed
    {
      get
      {
        return default(int);
      }
    }

    protected string VirtualPath
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
