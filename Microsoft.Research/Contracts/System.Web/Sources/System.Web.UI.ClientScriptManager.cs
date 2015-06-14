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

// File System.Web.UI.ClientScriptManager.cs
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
  sealed public partial class ClientScriptManager
  {
    #region Methods and constructors
    internal ClientScriptManager()
    {
    }

    public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context, bool useAsync)
    {
      return default(string);
    }

    public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context)
    {
      return default(string);
    }

    public string GetCallbackEventReference(string target, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsync)
    {
      return default(string);
    }

    public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsync)
    {
      return default(string);
    }

    public string GetPostBackClientHyperlink(Control control, string argument)
    {
      return default(string);
    }

    public string GetPostBackClientHyperlink(Control control, string argument, bool registerForEventValidation)
    {
      return default(string);
    }

    public string GetPostBackEventReference(Control control, string argument)
    {
      return default(string);
    }

    public string GetPostBackEventReference(Control control, string argument, bool registerForEventValidation)
    {
      return default(string);
    }

    public string GetPostBackEventReference(PostBackOptions options, bool registerForEventValidation)
    {
      return default(string);
    }

    public string GetPostBackEventReference(PostBackOptions options)
    {
      return default(string);
    }

    public string GetWebResourceUrl(Type type, string resourceName)
    {
      return default(string);
    }

    public bool IsClientScriptBlockRegistered(Type type, string key)
    {
      return default(bool);
    }

    public bool IsClientScriptBlockRegistered(string key)
    {
      return default(bool);
    }

    public bool IsClientScriptIncludeRegistered(string key)
    {
      return default(bool);
    }

    public bool IsClientScriptIncludeRegistered(Type type, string key)
    {
      return default(bool);
    }

    public bool IsOnSubmitStatementRegistered(Type type, string key)
    {
      return default(bool);
    }

    public bool IsOnSubmitStatementRegistered(string key)
    {
      return default(bool);
    }

    public bool IsStartupScriptRegistered(string key)
    {
      return default(bool);
    }

    public bool IsStartupScriptRegistered(Type type, string key)
    {
      return default(bool);
    }

    public void RegisterArrayDeclaration(string arrayName, string arrayValue)
    {
    }

    public void RegisterClientScriptBlock(Type type, string key, string script, bool addScriptTags)
    {
    }

    public void RegisterClientScriptBlock(Type type, string key, string script)
    {
    }

    public void RegisterClientScriptInclude(Type type, string key, string url)
    {
    }

    public void RegisterClientScriptInclude(string key, string url)
    {
    }

    public void RegisterClientScriptResource(Type type, string resourceName)
    {
    }

    public void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
    {
    }

    public void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
    {
    }

    public void RegisterForEventValidation(string uniqueId)
    {
    }

    public void RegisterForEventValidation(PostBackOptions options)
    {
    }

    public void RegisterForEventValidation(string uniqueId, string argument)
    {
    }

    public void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue)
    {
    }

    public void RegisterOnSubmitStatement(Type type, string key, string script)
    {
    }

    public void RegisterStartupScript(Type type, string key, string script)
    {
    }

    public void RegisterStartupScript(Type type, string key, string script, bool addScriptTags)
    {
    }

    public void ValidateEvent(string uniqueId, string argument)
    {
    }

    public void ValidateEvent(string uniqueId)
    {
    }
    #endregion
  }
}
