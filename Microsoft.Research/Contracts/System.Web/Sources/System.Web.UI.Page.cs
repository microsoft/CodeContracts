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

// File System.Web.UI.Page.cs
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
  public partial class Page : TemplateControl, System.Web.IHttpHandler
  {
    #region Methods and constructors
    protected internal void AddContentTemplate(string templateName, ITemplate template)
    {
    }

    public void AddOnPreRenderCompleteAsync(System.Web.BeginEventHandler beginHandler, System.Web.EndEventHandler endHandler)
    {
    }

    public void AddOnPreRenderCompleteAsync(System.Web.BeginEventHandler beginHandler, System.Web.EndEventHandler endHandler, Object state)
    {
    }

    protected internal void AddWrappedFileDependencies(Object virtualFileDependencies)
    {
    }

    protected IAsyncResult AspCompatBeginProcessRequest(System.Web.HttpContext context, AsyncCallback cb, Object extraData)
    {
      return default(IAsyncResult);
    }

    protected void AspCompatEndProcessRequest(IAsyncResult result)
    {
    }

    protected IAsyncResult AsyncPageBeginProcessRequest(System.Web.HttpContext context, AsyncCallback callback, Object extraData)
    {
      return default(IAsyncResult);
    }

    protected void AsyncPageEndProcessRequest(IAsyncResult result)
    {
    }

    protected internal virtual new HtmlTextWriter CreateHtmlTextWriter(TextWriter tw)
    {
      return default(HtmlTextWriter);
    }

    public static HtmlTextWriter CreateHtmlTextWriterFromType(TextWriter tw, Type writerType)
    {
      return default(HtmlTextWriter);
    }

    public void DesignerInitialize()
    {
    }

    protected internal virtual new System.Collections.Specialized.NameValueCollection DeterminePostBackMode()
    {
      return default(System.Collections.Specialized.NameValueCollection);
    }

    public void ExecuteRegisteredAsyncTasks()
    {
    }

    public override Control FindControl(string id)
    {
      return default(Control);
    }

    protected override void FrameworkInitialize()
    {
    }

    public Object GetDataItem()
    {
      return default(Object);
    }

    public string GetPostBackClientEvent(Control control, string argument)
    {
      return default(string);
    }

    public string GetPostBackClientHyperlink(Control control, string argument)
    {
      return default(string);
    }

    public string GetPostBackEventReference(Control control, string argument)
    {
      return default(string);
    }

    public string GetPostBackEventReference(Control control)
    {
      return default(string);
    }

    public virtual new int GetTypeHashCode()
    {
      return default(int);
    }

    public ValidatorCollection GetValidators(string validationGroup)
    {
      return default(ValidatorCollection);
    }

    protected Object GetWrappedFileDependencies(string[] virtualFileDependencies)
    {
      return default(Object);
    }

    protected virtual new void InitializeCulture()
    {
    }

    protected internal virtual new void InitOutputCache(OutputCacheParameters cacheSettings)
    {
    }

    protected virtual new void InitOutputCache(int duration, string varyByHeader, string varyByCustom, OutputCacheLocation location, string varyByParam)
    {
    }

    protected virtual new void InitOutputCache(int duration, string varyByContentEncoding, string varyByHeader, string varyByCustom, OutputCacheLocation location, string varyByParam)
    {
    }

    public bool IsClientScriptBlockRegistered(string key)
    {
      return default(bool);
    }

    public bool IsStartupScriptRegistered(string key)
    {
      return default(bool);
    }

    protected internal virtual new Object LoadPageStateFromPersistenceMedium()
    {
      return default(Object);
    }

    public string MapPath(string virtualPath)
    {
      return default(string);
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected virtual new void OnInitComplete(EventArgs e)
    {
    }

    protected virtual new void OnLoadComplete(EventArgs e)
    {
    }

    protected virtual new void OnPreInit(EventArgs e)
    {
    }

    protected virtual new void OnPreLoad(EventArgs e)
    {
    }

    protected virtual new void OnPreRenderComplete(EventArgs e)
    {
    }

    protected virtual new void OnSaveStateComplete(EventArgs e)
    {
    }

    public Page()
    {
    }

    public virtual new void ProcessRequest(System.Web.HttpContext context)
    {
    }

    protected virtual new void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
    {
    }

    public void RegisterArrayDeclaration(string arrayName, string arrayValue)
    {
    }

    public void RegisterAsyncTask(PageAsyncTask task)
    {
    }

    public virtual new void RegisterClientScriptBlock(string key, string script)
    {
    }

    public virtual new void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue)
    {
    }

    public void RegisterOnSubmitStatement(string key, string script)
    {
    }

    public void RegisterRequiresControlState(Control control)
    {
    }

    public void RegisterRequiresPostBack(Control control)
    {
    }

    public virtual new void RegisterRequiresRaiseEvent(IPostBackEventHandler control)
    {
    }

    public void RegisterRequiresViewStateEncryption()
    {
    }

    public virtual new void RegisterStartupScript(string key, string script)
    {
    }

    public void RegisterViewStateHandler()
    {
    }

    protected internal override void Render(HtmlTextWriter writer)
    {
    }

    public bool RequiresControlState(Control control)
    {
      return default(bool);
    }

    protected internal virtual new void SavePageStateToPersistenceMedium(Object state)
    {
    }

    public void SetFocus(Control control)
    {
    }

    public void SetFocus(string clientID)
    {
    }

    public void UnregisterRequiresControlState(Control control)
    {
    }

    public virtual new void Validate()
    {
    }

    public virtual new void Validate(string validationGroup)
    {
    }

    public virtual new void VerifyRenderingInServerForm(Control control)
    {
    }
    #endregion

    #region Properties and indexers
    public System.Web.HttpApplicationState Application
    {
      get
      {
        return default(System.Web.HttpApplicationState);
      }
    }

    protected bool AspCompatMode
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected bool AsyncMode
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TimeSpan AsyncTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public Control AutoPostBackControl
    {
      get
      {
        return default(Control);
      }
      set
      {
      }
    }

    public bool Buffer
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Web.Caching.Cache Cache
    {
      get
      {
        return default(System.Web.Caching.Cache);
      }
    }

    public string ClientQueryString
    {
      get
      {
        return default(string);
      }
    }

    public ClientScriptManager ClientScript
    {
      get
      {
        return default(ClientScriptManager);
      }
    }

    public string ClientTarget
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int CodePage
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string ContentType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    internal protected override System.Web.HttpContext Context
    {
      get
      {
        return default(System.Web.HttpContext);
      }
    }

    public string Culture
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool EnableEventValidation
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool EnableViewState
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool EnableViewStateMac
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string ErrorPage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected System.Collections.ArrayList FileDependencies
    {
      set
      {
      }
    }

    public System.Web.UI.HtmlControls.HtmlForm Form
    {
      get
      {
        return default(System.Web.UI.HtmlControls.HtmlForm);
      }
    }

    public System.Web.UI.HtmlControls.HtmlHead Header
    {
      get
      {
        return default(System.Web.UI.HtmlControls.HtmlHead);
      }
    }

    public override string ID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new char IdSeparator
    {
      get
      {
        return default(char);
      }
    }

    public bool IsAsync
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsCallback
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsCrossPagePostBack
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPostBack
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPostBackEventControlRegistered
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsReusable
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsValid
    {
      get
      {
        return default(bool);
      }
    }

    public System.Collections.IDictionary Items
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
    }

    public int LCID
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public bool MaintainScrollPositionOnPostBack
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public MasterPage Master
    {
      get
      {
        return default(MasterPage);
      }
    }

    public virtual new string MasterPageFile
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int MaxPageStateFieldLength
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string MetaDescription
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string MetaKeywords
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.Adapters.PageAdapter PageAdapter
    {
      get
      {
        return default(System.Web.UI.Adapters.PageAdapter);
      }
    }

    protected virtual new PageStatePersister PageStatePersister
    {
      get
      {
        return default(PageStatePersister);
      }
    }

    public System.Web.UI.Page PreviousPage
    {
      get
      {
        return default(System.Web.UI.Page);
      }
    }

    public System.Web.HttpRequest Request
    {
      get
      {
        return default(System.Web.HttpRequest);
      }
    }

    public System.Web.HttpResponse Response
    {
      get
      {
        return default(System.Web.HttpResponse);
      }
    }

    public string ResponseEncoding
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.Routing.RouteData RouteData
    {
      get
      {
        return default(System.Web.Routing.RouteData);
      }
    }

    public System.Web.HttpServerUtility Server
    {
      get
      {
        return default(System.Web.HttpServerUtility);
      }
    }

    public virtual new System.Web.SessionState.HttpSessionState Session
    {
      get
      {
        return default(System.Web.SessionState.HttpSessionState);
      }
    }

    public bool SmartNavigation
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string StyleSheetTheme
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string Theme
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Title
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.TraceContext Trace
    {
      get
      {
        return default(System.Web.TraceContext);
      }
    }

    public bool TraceEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Web.TraceMode TraceModeValue
    {
      get
      {
        return default(System.Web.TraceMode);
      }
      set
      {
      }
    }

    protected int TransactionMode
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string UICulture
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    internal protected virtual new string UniqueFilePathSuffix
    {
      get
      {
        return default(string);
      }
    }

    public System.Security.Principal.IPrincipal User
    {
      get
      {
        return default(System.Security.Principal.IPrincipal);
      }
    }

    public ValidatorCollection Validators
    {
      get
      {
        return default(ValidatorCollection);
      }
    }

    public ViewStateEncryptionMode ViewStateEncryptionMode
    {
      get
      {
        return default(ViewStateEncryptionMode);
      }
      set
      {
      }
    }

    public string ViewStateUserKey
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override bool Visible
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler InitComplete
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler LoadComplete
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler PreInit
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler PreLoad
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler PreRenderComplete
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler SaveStateComplete
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public static string postEventArgumentID;
    public static string postEventSourceID;
    #endregion
  }
}
