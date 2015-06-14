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

// File System.Web.UI.Control.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;
using System.Collections;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0067
// Disable the "this event is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Web.UI
{
  public partial class Control : System.ComponentModel.IComponent, IDisposable, IParserAccessor, IUrlResolutionService, IDataBindingsAccessor, IControlBuilderAccessor, IControlDesignerAccessor, IExpressionsAccessor
  {
    #region Methods and constructors
    protected internal virtual new void AddedControl (System.Web.UI.Control control, int index)
    {
      Contract.Requires(control != null);
    }

    protected virtual new void AddParsedSubObject (Object obj)
    {
    }

    public virtual new void ApplyStyleSheetSkin (Page page)
    {
    }

    protected void BuildProfileTree (string parentId, bool calcViewState)
    {
    }

#if NETFRAMEWORK_4_0
    protected void ClearCachedClientID ()
    {
    }
#endif

    protected void ClearChildControlState ()
    {
    }

    protected void ClearChildState ()
    {
    }

    protected void ClearChildViewState ()
    {
    }

#if NETFRAMEWORK_4_0
    protected void ClearEffectiveClientIDMode ()
    {
    }
#endif
    public Control ()
    {
    }

    protected internal virtual new void CreateChildControls ()
    {
    }

    protected virtual new ControlCollection CreateControlCollection ()
    {
      Contract.Ensures(Contract.Result<ControlCollection>() != null);
      
      return default(ControlCollection);
    }

    protected virtual new void DataBind (bool raiseOnDataBinding)
    {
    }

    public virtual new void DataBind ()
    {
    }

    protected virtual new void DataBindChildren ()
    {
    }

    public virtual new void Dispose ()
    {
    }

    protected virtual new void EnsureChildControls ()
    {
    }

    protected void EnsureID ()
    {
    }
    [Pure]
    public virtual new System.Web.UI.Control FindControl (string id)
    {
      Contract.Requires(id != null);
      return default(System.Web.UI.Control);
    }

    [Pure]
    protected virtual new System.Web.UI.Control FindControl (string id, int pathOffset)
    {
      Contract.Requires(id != null);
      return default(System.Web.UI.Control);
    }

    public virtual new void Focus ()
    {
      Contract.Requires (this.Page != null);
    }

    [Pure]
    protected virtual new System.Collections.IDictionary GetDesignModeState ()
    {
      Contract.Ensures(Contract.Result<IDictionary>() != null);
      return default(System.Collections.IDictionary);
    }

#if NETFRAMEWORK_4_0
    public string GetRouteUrl (string routeName, Object routeParameters)
    {
      return default(string);
    }

    public string GetRouteUrl (Object routeParameters)
    {
      return default(string);
    }

    public string GetRouteUrl (System.Web.Routing.RouteValueDictionary routeParameters)
    {
      return default(string);
    }

    public string GetRouteUrl (string routeName, System.Web.Routing.RouteValueDictionary routeParameters)
    {
      return default(string);
    }

    public string GetUniqueIDRelativeTo (System.Web.UI.Control control)
    {
      Contract.Requires (control.NamingContainer != null);

      return default(string);
    }
#endif
    [Pure]
    public virtual new bool HasControls()
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

    protected internal virtual new void LoadControlState (Object savedState)
    {
    }

    protected virtual new void LoadViewState (Object savedState)
    {
    }

    protected internal string MapPathSecure (string virtualPath)
    {
      Contract.Requires(!String.IsNullOrEmpty(virtualPath));
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    protected virtual new bool OnBubbleEvent (Object source, EventArgs args)
    {
      Contract.Requires(source != null);
      Contract.Requires(args != null);
      return default(bool);
    }

    protected virtual new void OnDataBinding (EventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected internal virtual new void OnInit (EventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected internal virtual new void OnLoad (EventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected internal virtual new void OnPreRender (EventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected internal virtual new void OnUnload (EventArgs e)
    {
      Contract.Requires(e != null);
    }

    protected internal Stream OpenFile (string path)
    {
      Contract.Requires(path != null);
      Contract.Ensures(Contract.Result<Stream>() != null);

      return default(Stream);
    }

    protected void RaiseBubbleEvent (Object source, EventArgs args)
    {
      Contract.Requires(source != null);
      Contract.Requires(args != null);
    }

    protected internal virtual new void RemovedControl (System.Web.UI.Control control)
    {
      Contract.Requires(control != null);
    }

    protected internal virtual new void Render (HtmlTextWriter writer)
    {
      Contract.Requires(writer != null);
    }

    protected internal virtual new void RenderChildren (HtmlTextWriter writer)
    {
      Contract.Requires(writer != null);
    }

    protected void RenderControl (HtmlTextWriter writer, System.Web.UI.Adapters.ControlAdapter adapter)
    {
      Contract.Requires(writer != null);
    }

    public virtual new void RenderControl (HtmlTextWriter writer)
    {
      Contract.Requires(writer != null);
    }

    protected virtual new System.Web.UI.Adapters.ControlAdapter ResolveAdapter ()
    {
      return default(System.Web.UI.Adapters.ControlAdapter);
    }

    public string ResolveClientUrl (string relativeUrl)
    {
      Contract.Requires(relativeUrl != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    public string ResolveUrl (string relativeUrl)
    {
      Contract.Requires(relativeUrl != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    protected internal virtual new Object SaveControlState ()
    {
      return default(Object);
    }

    protected virtual new Object SaveViewState ()
    {
      return default(Object);
    }

    protected virtual new void SetDesignModeState (System.Collections.IDictionary data)
    {
    }

    public void SetRenderMethodDelegate (RenderMethod renderMethod)
    {
    }

    System.Collections.IDictionary System.Web.UI.IControlDesignerAccessor.GetDesignModeState ()
    {
      return default(System.Collections.IDictionary);
    }

    void System.Web.UI.IControlDesignerAccessor.SetDesignModeState (System.Collections.IDictionary data)
    {
    }

    void System.Web.UI.IControlDesignerAccessor.SetOwnerControl (System.Web.UI.Control owner)
    {
    }

    void System.Web.UI.IParserAccessor.AddParsedSubObject (Object obj)
    {
    }

    protected virtual new void TrackViewState ()
    {
    }
    #endregion

    #region Properties and indexers
    protected System.Web.UI.Adapters.ControlAdapter Adapter
    {
      get
      {
        return default(System.Web.UI.Adapters.ControlAdapter);
      }
    }

    public string AppRelativeTemplateSourceDirectory
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.Control BindingContainer
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

    protected bool ChildControlsCreated
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string ClientID
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        
        return default(string);
      }
    }
#if NETFRAMEWORK_4_0
    public virtual new ClientIDMode ClientIDMode
    {
      get
      {
        return default(ClientIDMode);
      }
      set
      {
      }
    }
#endif
    protected char ClientIDSeparator
    {
      get
      {
        Contract.Ensures (Contract.Result<char>() == 95);

        return default(char);
      }
    }

    internal protected virtual new System.Web.HttpContext Context
    {
      get
      {
        return default(System.Web.HttpContext);
      }
    }

    public virtual new ControlCollection Controls
    {
      get
      {
        Contract.Ensures(Contract.Result<ControlCollection>() != null);
        
        return default(ControlCollection);
      }
    }

#if NETFRAMEWORK_4_0
    public System.Web.UI.Control DataItemContainer
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

    public System.Web.UI.Control DataKeysContainer
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }
#endif

    internal protected bool DesignMode
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool EnableTheming
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool EnableViewState
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected System.ComponentModel.EventHandlerList Events
    {
      get
      {
        Contract.Ensures(Contract.Result<System.ComponentModel.EventHandlerList>() != null);

        return default(System.ComponentModel.EventHandlerList);
      }
    }

    protected bool HasChildViewState
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string ID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected char IdSeparator
    {
      get
      {
        return default(char);
      }
    }

    internal protected bool IsChildControlStateCleared
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsTrackingViewState
    {
      get
      {
        return default(bool);
      }
    }

    internal protected bool IsViewStateEnabled
    {
      get
      {
        return default(bool);
      }
    }

    protected bool LoadViewStateByID
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new System.Web.UI.Control NamingContainer
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

    public virtual new Page Page
    {
      get
      {
        return default(Page);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.Control Parent
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

#if NETFRAMEWORK_4_0
    public virtual new Version RenderingCompatibility
    {
      get
      {
        return default(Version);
      }
      set
      {
      }
    }
#endif

    public System.ComponentModel.ISite Site
    {
      get
      {
        return default(System.ComponentModel.ISite);
      }
      set
      {
      }
    }

    public virtual new string SkinID
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
      }
    }

    ControlBuilder System.Web.UI.IControlBuilderAccessor.ControlBuilder
    {
      get
      {
        return default(ControlBuilder);
      }
    }

    System.Collections.IDictionary System.Web.UI.IControlDesignerAccessor.UserData
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
    }

    DataBindingCollection System.Web.UI.IDataBindingsAccessor.DataBindings
    {
      get
      {
        return default(DataBindingCollection);
      }
    }

    bool System.Web.UI.IDataBindingsAccessor.HasDataBindings
    {
      get
      {
        return default(bool);
      }
    }

    ExpressionBindingCollection System.Web.UI.IExpressionsAccessor.Expressions
    {
      get
      {
        return default(ExpressionBindingCollection);
      }
    }

    bool System.Web.UI.IExpressionsAccessor.HasExpressions
    {
      get
      {
        return default(bool);
      }
    }

    public TemplateControl TemplateControl
    {
      get
      {
        return default(TemplateControl);
      }
      set
      {
      }
    }

    public virtual new string TemplateSourceDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public virtual new string UniqueID
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    protected virtual new StateBag ViewState
    {
      get
      {
        Contract.Ensures(Contract.Result<StateBag>() != null);
        return default(StateBag);
      }
    }

    protected virtual new bool ViewStateIgnoresCase
    {
      get
      {
        return default(bool);
      }
    }

#if NETFRAMEWORK_4_0
    public virtual new ViewStateMode ViewStateMode
    {
      get
      {
        return default(ViewStateMode);
      }
      set
      {
      }
    }
#endif

    public virtual new bool Visible
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

    #region IComponent Members

    extern public event EventHandler Disposed;

    #endregion
  }
}
