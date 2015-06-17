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
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics.Contracts;
using System.Web.UI.Adapters;
using System.ComponentModel;

namespace System.Web.UI
{
  public class Control
  {
    protected internal virtual void AddedControl(Control control, int index)
    {
      Contract.Requires(control != null);
    }

    // protected virtual void AddParsedSubObject(object obj);

    // public virtual void ApplyStyleSheetSkin(Page page);

    // protected void BuildProfileTree(string parentId, bool calcViewState)

    // protected void ClearChildControlState();
    // protected void ClearChildState();
    // protected void ClearChildViewState();

    // protected internal virtual void CreateChildControls();
    // protected virtual ControlCollection CreateControlCollection();

    // public virtual void DataBind();
    // protected virtual void DataBind(bool raiseOnDataBinding);
    // protected virtual void DataBindChildren();

    // public virtual void Dispose();
    // protected virtual void EnsureChildControls();
    // protected void EnsureID();

    [Pure]
    public virtual Control FindControl(string id)
    {
      Contract.Requires(id != null);
      return default(Control);
    }

    [Pure]
    protected virtual Control FindControl(string id, int pathOffset)
    {
      Contract.Requires(id != null);
      return default(Control);
    }

    // public virtual void Focus();
    [Pure]
    protected virtual IDictionary GetDesignModeState()
    {
      Contract.Ensures(Contract.Result<IDictionary>() != null);
      return default(IDictionary);
    }

    [Pure]
    public virtual bool HasControls()
    {
      return default(bool);
    }

    [Pure]
    protected bool HasEvents()
    {
      return default(bool);
    }

    [Pure]
    protected bool IsLiteralContent()
    {
      return default(bool);
    }

    // protected internal virtual void LoadControlState(object savedState);
    // internal void LoadControlStateInternal(object savedStateObj);

    // protected virtual void LoadViewState(object savedState);

    protected internal string MapPathSecure(string virtualPath)
    {
      Contract.Requires(!String.IsNullOrEmpty(virtualPath));
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    protected virtual bool OnBubbleEvent(object source, EventArgs args)
    {
      Contract.Requires(source != null);
      Contract.Requires(args != null);
      return default(bool);
    }
    protected virtual void OnDataBinding(EventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected internal virtual void OnInit(EventArgs e)
    {
      Contract.Requires(e != null);
    }
    protected internal virtual void OnLoad(EventArgs e)
    {
      Contract.Requires(e != null);
    }
    protected internal virtual void OnPreRender(EventArgs e)
    {
      Contract.Requires(e != null);
    }
    protected internal virtual void OnUnload(EventArgs e)
    {
      Contract.Requires(e != null);
    }
    protected internal Stream OpenFile(string path)
    {
      Contract.Requires(path != null);
      Contract.Ensures(Contract.Result<Stream>() != null);
      return default(Stream);
    }
    protected void RaiseBubbleEvent(object source, EventArgs args)
    {
      Contract.Requires(source != null);
      Contract.Requires(args != null);
    }
    protected internal virtual void RemovedControl(Control control)
    {
      Contract.Requires(control != null);
    }
    protected internal virtual void Render(HtmlTextWriter writer)
    {
      Contract.Requires(writer != null);
    }

    protected internal virtual void RenderChildren(HtmlTextWriter writer)
    {
      Contract.Requires(writer != null);
    }

    public virtual void RenderControl(HtmlTextWriter writer)
    {
      Contract.Requires(writer != null);
    }

    protected void RenderControl(HtmlTextWriter writer, ControlAdapter adapter)
    {
      Contract.Requires(writer != null);
    }

    //protected virtual ControlAdapter ResolveAdapter()

    public virtual string ResolveClientUrl(string relativeUrl)
    {
      Contract.Requires(relativeUrl != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    public string ResolveUrl(string relativeUrl)
    {
      Contract.Requires(relativeUrl != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    // protected internal virtual object SaveControlState();
    // protected virtual object SaveViewState();

    // protected virtual void SetDesignModeState(IDictionary data);

    //public void SetRenderMethodDelegate(RenderMethod renderMethod);

    // protected virtual void TrackViewState();

    // Properties
    // protected ControlAdapter Adapter { get; }

    //public string AppRelativeTemplateSourceDirectory { get; [EditorBrowsable(EditorBrowsableState.Never)] set; }
    // public Control BindingContainer
    // protected bool ChildControlsCreated { get; set; }

    public virtual string ClientID
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    // protected char ClientIDSeparator { get; }
    // protected internal virtual HttpContext Context { get; }
    public virtual ControlCollection Controls
    {
      get
      {
        Contract.Ensures(Contract.Result<ControlCollection>() != null);
        return default(ControlCollection);
      }
    }

    // protected internal bool DesignMode { get; }
    // internal bool EnableLegacyRendering { get; }
    // public virtual bool EnableTheming { get; set; }
    // public virtual bool EnableViewState { get; set; }
    protected EventHandlerList Events
    {
      get
      {
        Contract.Ensures(Contract.Result<EventHandlerList>() != null);
        return default(EventHandlerList);
      }
    }
    // protected bool HasChildViewState { get; }

    // public virtual string ID { get; set; }
    // protected char IdSeparator { get; }
    // protected internal bool IsChildControlStateCleared { get; }
    // protected bool IsTrackingViewState { get; }
    // protected internal bool IsViewStateEnabled { get; }
    // protected bool LoadViewStateByID { get; }
    // public virtual Control NamingContainer { get; }

    // public virtual Page Page { get; set; }
    // public virtual Control Parent { get; }

    // public ISite Site { get; set; }
    public virtual string SkinID
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    //public TemplateControl TemplateControl { get; [EditorBrowsable(EditorBrowsableState.Never)] set; }

    public virtual string TemplateSourceDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public virtual string UniqueID
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    protected virtual StateBag 
      ViewState
    {
      get
      {
        Contract.Ensures(Contract.Result<StateBag>() != null);
        return default(StateBag);
      }
    }
    //protected virtual bool ViewStateIgnoresCase { get; }
    //public virtual bool Visible { get; set; }
  }

  public class StateBag
  {
  }

}