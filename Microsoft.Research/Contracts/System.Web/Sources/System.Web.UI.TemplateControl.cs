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

// File System.Web.UI.TemplateControl.cs
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
  abstract public partial class TemplateControl : Control, INamingContainer, IFilterResolutionService
  {
    #region Methods and constructors
    protected virtual new void Construct()
    {
    }

    protected LiteralControl CreateResourceBasedLiteralControl(int offset, int size, bool fAsciiOnly)
    {
      return default(LiteralControl);
    }

    protected internal Object Eval(string expression)
    {
      return default(Object);
    }

    protected internal string Eval(string expression, string format)
    {
      return default(string);
    }

    protected virtual new void FrameworkInitialize()
    {
    }

    protected Object GetGlobalResourceObject(string className, string resourceKey, Type objType, string propName)
    {
      return default(Object);
    }

    protected Object GetGlobalResourceObject(string className, string resourceKey)
    {
      return default(Object);
    }

    protected Object GetLocalResourceObject(string resourceKey)
    {
      return default(Object);
    }

    protected Object GetLocalResourceObject(string resourceKey, Type objType, string propName)
    {
      return default(Object);
    }

    public Control LoadControl(string virtualPath)
    {
      return default(Control);
    }

    public Control LoadControl(Type t, Object[] parameters)
    {
      return default(Control);
    }

    public ITemplate LoadTemplate(string virtualPath)
    {
      return default(ITemplate);
    }

    protected virtual new void OnAbortTransaction(EventArgs e)
    {
    }

    protected virtual new void OnCommitTransaction(EventArgs e)
    {
    }

    protected virtual new void OnError(EventArgs e)
    {
    }

    public Control ParseControl(string content)
    {
      return default(Control);
    }

    public Control ParseControl(string content, bool ignoreParserFilter)
    {
      return default(Control);
    }

    public Object ReadStringResource()
    {
      return default(Object);
    }

    public static Object ReadStringResource(Type t)
    {
      return default(Object);
    }

    protected void SetStringResourcePointer(Object stringResourcePointer, int maxResourceOffset)
    {
    }

    int System.Web.UI.IFilterResolutionService.CompareFilters(string filter1, string filter2)
    {
      return default(int);
    }

    bool System.Web.UI.IFilterResolutionService.EvaluateFilter(string filterName)
    {
      return default(bool);
    }

    protected TemplateControl()
    {
    }

    public virtual new bool TestDeviceFilter(string filterName)
    {
      return default(bool);
    }

    protected void WriteUTF8ResourceString(HtmlTextWriter output, int offset, int size, bool fAsciiOnly)
    {
    }

    protected internal Object XPath(string xPathExpression, System.Xml.IXmlNamespaceResolver resolver)
    {
      return default(Object);
    }

    protected internal Object XPath(string xPathExpression)
    {
      return default(Object);
    }

    protected internal string XPath(string xPathExpression, string format, System.Xml.IXmlNamespaceResolver resolver)
    {
      return default(string);
    }

    protected internal string XPath(string xPathExpression, string format)
    {
      return default(string);
    }

    protected internal System.Collections.IEnumerable XPathSelect(string xPathExpression)
    {
      return default(System.Collections.IEnumerable);
    }

    protected internal System.Collections.IEnumerable XPathSelect(string xPathExpression, System.Xml.IXmlNamespaceResolver resolver)
    {
      return default(System.Collections.IEnumerable);
    }
    #endregion

    #region Properties and indexers
    public string AppRelativeVirtualPath
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected virtual new int AutoHandlers
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public override bool EnableTheming
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected virtual new bool SupportAutoEvents
    {
      get
      {
        return default(bool);
      }
    }
    #endregion

    #region Events
    public event EventHandler AbortTransaction
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler CommitTransaction
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Error
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
