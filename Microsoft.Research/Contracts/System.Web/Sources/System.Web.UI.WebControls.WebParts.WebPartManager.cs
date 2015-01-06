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

// File System.Web.UI.WebControls.WebParts.WebPartManager.cs
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


namespace System.Web.UI.WebControls.WebParts
{
  public partial class WebPartManager : System.Web.UI.Control, System.Web.UI.INamingContainer, IPersonalizable
  {
    #region Methods and constructors
    protected virtual new void ActivateConnections()
    {
    }

    public WebPart AddWebPart(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
    {
      return default(WebPart);
    }

    public virtual new void BeginWebPartConnecting(WebPart webPart)
    {
    }

    public virtual new void BeginWebPartEditing(WebPart webPart)
    {
    }

    public virtual new bool CanConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartTransformer transformer)
    {
      return default(bool);
    }

    public bool CanConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
    {
      return default(bool);
    }

    protected virtual new bool CheckRenderClientScript()
    {
      return default(bool);
    }

    public void CloseWebPart(WebPart webPart)
    {
    }

    public virtual new WebPartConnection ConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartTransformer transformer)
    {
      return default(WebPartConnection);
    }

    public WebPartConnection ConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
    {
      return default(WebPartConnection);
    }

    protected virtual new WebPart CopyWebPart(WebPart webPart)
    {
      return default(WebPart);
    }

    protected virtual new TransformerTypeCollection CreateAvailableTransformers()
    {
      return default(TransformerTypeCollection);
    }

    protected sealed override System.Web.UI.ControlCollection CreateControlCollection()
    {
      return default(System.Web.UI.ControlCollection);
    }

    protected virtual new WebPartDisplayModeCollection CreateDisplayModes()
    {
      return default(WebPartDisplayModeCollection);
    }

    protected virtual new string CreateDynamicConnectionID()
    {
      return default(string);
    }

    protected virtual new string CreateDynamicWebPartID(Type webPartType)
    {
      return default(string);
    }

    protected virtual new ErrorWebPart CreateErrorWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID, string errorMessage)
    {
      return default(ErrorWebPart);
    }

    protected virtual new WebPartPersonalization CreatePersonalization()
    {
      return default(WebPartPersonalization);
    }

    public virtual new GenericWebPart CreateWebPart(System.Web.UI.Control control)
    {
      return default(GenericWebPart);
    }

    public void DeleteWebPart(WebPart webPart)
    {
    }

    protected virtual new void DisconnectWebPart(WebPart webPart)
    {
    }

    public virtual new void DisconnectWebParts(WebPartConnection connection)
    {
    }

    public virtual new void EndWebPartConnecting()
    {
    }

    public virtual new void EndWebPartEditing()
    {
    }

    public virtual new void ExportWebPart(WebPart webPart, System.Xml.XmlWriter writer)
    {
    }

    public override void Focus()
    {
    }

    public virtual new ConsumerConnectionPointCollection GetConsumerConnectionPoints(WebPart webPart)
    {
      return default(ConsumerConnectionPointCollection);
    }

    public static System.Web.UI.WebControls.WebParts.WebPartManager GetCurrentWebPartManager(System.Web.UI.Page page)
    {
      return default(System.Web.UI.WebControls.WebParts.WebPartManager);
    }

    protected internal virtual new string GetDisplayTitle(WebPart webPart)
    {
      return default(string);
    }

    public string GetExportUrl(WebPart webPart)
    {
      return default(string);
    }

    public GenericWebPart GetGenericWebPart(System.Web.UI.Control control)
    {
      return default(GenericWebPart);
    }

    public virtual new ProviderConnectionPointCollection GetProviderConnectionPoints(WebPart webPart)
    {
      return default(ProviderConnectionPointCollection);
    }

    public virtual new WebPart ImportWebPart(System.Xml.XmlReader reader, out string errorMessage)
    {
      errorMessage = default(string);

      return default(WebPart);
    }

    public bool IsAuthorized(WebPart webPart)
    {
      return default(bool);
    }

    public virtual new bool IsAuthorized(Type type, string path, string authorizationFilter, bool isShared)
    {
      return default(bool);
    }

    protected internal override void LoadControlState(Object savedState)
    {
    }

    protected virtual new void LoadCustomPersonalizationState(PersonalizationDictionary state)
    {
    }

    public virtual new void MoveWebPart(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
    {
    }

    protected virtual new void OnAuthorizeWebPart(WebPartAuthorizationEventArgs e)
    {
    }

    protected virtual new void OnConnectionsActivated(EventArgs e)
    {
    }

    protected virtual new void OnConnectionsActivating(EventArgs e)
    {
    }

    protected virtual new void OnDisplayModeChanged(WebPartDisplayModeEventArgs e)
    {
    }

    protected virtual new void OnDisplayModeChanging(WebPartDisplayModeCancelEventArgs e)
    {
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected virtual new void OnSelectedWebPartChanged(WebPartEventArgs e)
    {
    }

    protected virtual new void OnSelectedWebPartChanging(WebPartCancelEventArgs e)
    {
    }

    protected internal override void OnUnload(EventArgs e)
    {
    }

    protected virtual new void OnWebPartAdded(WebPartEventArgs e)
    {
    }

    protected virtual new void OnWebPartAdding(WebPartAddingEventArgs e)
    {
    }

    protected virtual new void OnWebPartClosed(WebPartEventArgs e)
    {
    }

    protected virtual new void OnWebPartClosing(WebPartCancelEventArgs e)
    {
    }

    protected virtual new void OnWebPartDeleted(WebPartEventArgs e)
    {
    }

    protected virtual new void OnWebPartDeleting(WebPartCancelEventArgs e)
    {
    }

    protected virtual new void OnWebPartMoved(WebPartEventArgs e)
    {
    }

    protected virtual new void OnWebPartMoving(WebPartMovingEventArgs e)
    {
    }

    protected virtual new void OnWebPartsConnected(WebPartConnectionsEventArgs e)
    {
    }

    protected virtual new void OnWebPartsConnecting(WebPartConnectionsCancelEventArgs e)
    {
    }

    protected virtual new void OnWebPartsDisconnected(WebPartConnectionsEventArgs e)
    {
    }

    protected virtual new void OnWebPartsDisconnecting(WebPartConnectionsCancelEventArgs e)
    {
    }

    protected virtual new void RegisterClientScript()
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override Object SaveControlState()
    {
      return default(Object);
    }

    protected virtual new void SaveCustomPersonalizationState(PersonalizationDictionary state)
    {
    }

    protected void SetPersonalizationDirty()
    {
    }

    protected void SetSelectedWebPart(WebPart webPart)
    {
    }

    void System.Web.UI.WebControls.WebParts.IPersonalizable.Load(PersonalizationDictionary state)
    {
    }

    void System.Web.UI.WebControls.WebParts.IPersonalizable.Save(PersonalizationDictionary state)
    {
    }

    protected override void TrackViewState()
    {
    }

    public WebPartManager()
    {
    }
    #endregion

    #region Properties and indexers
    public TransformerTypeCollection AvailableTransformers
    {
      get
      {
        return default(TransformerTypeCollection);
      }
    }

    public virtual new string CloseProviderWarning
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public WebPartConnectionCollection Connections
    {
      get
      {
        return default(WebPartConnectionCollection);
      }
    }

    public override System.Web.UI.ControlCollection Controls
    {
      get
      {
        return default(System.Web.UI.ControlCollection);
      }
    }

    public virtual new string DeleteWarning
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new WebPartDisplayMode DisplayMode
    {
      get
      {
        return default(WebPartDisplayMode);
      }
      set
      {
      }
    }

    public WebPartDisplayModeCollection DisplayModes
    {
      get
      {
        return default(WebPartDisplayModeCollection);
      }
    }

    internal protected WebPartConnectionCollection DynamicConnections
    {
      get
      {
        return default(WebPartConnectionCollection);
      }
    }

    public virtual new bool EnableClientScript
    {
      get
      {
        return default(bool);
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

    public virtual new string ExportSensitiveDataWarning
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected WebPartManagerInternals Internals
    {
      get
      {
        return default(WebPartManagerInternals);
      }
    }

    protected virtual new bool IsCustomPersonalizationStateDirty
    {
      get
      {
        return default(bool);
      }
    }

    protected virtual new System.Security.PermissionSet MediumPermissionSet
    {
      get
      {
        return default(System.Security.PermissionSet);
      }
    }

    protected virtual new System.Security.PermissionSet MinimalPermissionSet
    {
      get
      {
        return default(System.Security.PermissionSet);
      }
    }

    public WebPartPersonalization Personalization
    {
      get
      {
        return default(WebPartPersonalization);
      }
    }

    public WebPart SelectedWebPart
    {
      get
      {
        return default(WebPart);
      }
    }

    public override string SkinID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public WebPartConnectionCollection StaticConnections
    {
      get
      {
        return default(WebPartConnectionCollection);
      }
    }

    public WebPartDisplayModeCollection SupportedDisplayModes
    {
      get
      {
        return default(WebPartDisplayModeCollection);
      }
    }

    bool System.Web.UI.WebControls.WebParts.IPersonalizable.IsDirty
    {
      get
      {
        return default(bool);
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

    public WebPartCollection WebParts
    {
      get
      {
        return default(WebPartCollection);
      }
    }

    public WebPartZoneCollection Zones
    {
      get
      {
        return default(WebPartZoneCollection);
      }
    }
    #endregion

    #region Events
    public event WebPartAuthorizationEventHandler AuthorizeWebPart
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler ConnectionsActivated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler ConnectionsActivating
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartDisplayModeEventHandler DisplayModeChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartDisplayModeCancelEventHandler DisplayModeChanging
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartEventHandler SelectedWebPartChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartCancelEventHandler SelectedWebPartChanging
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartEventHandler WebPartAdded
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartAddingEventHandler WebPartAdding
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartEventHandler WebPartClosed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartCancelEventHandler WebPartClosing
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartEventHandler WebPartDeleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartCancelEventHandler WebPartDeleting
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartEventHandler WebPartMoved
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartMovingEventHandler WebPartMoving
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartConnectionsEventHandler WebPartsConnected
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartConnectionsCancelEventHandler WebPartsConnecting
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartConnectionsEventHandler WebPartsDisconnected
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WebPartConnectionsCancelEventHandler WebPartsDisconnecting
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
    public readonly static WebPartDisplayMode BrowseDisplayMode;
    public readonly static WebPartDisplayMode CatalogDisplayMode;
    public readonly static WebPartDisplayMode ConnectDisplayMode;
    public readonly static WebPartDisplayMode DesignDisplayMode;
    public readonly static WebPartDisplayMode EditDisplayMode;
    #endregion
  }
}
