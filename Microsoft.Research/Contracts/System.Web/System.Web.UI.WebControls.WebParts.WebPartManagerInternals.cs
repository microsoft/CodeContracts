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

// File System.Web.UI.WebControls.WebParts.WebPartManagerInternals.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
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
  sealed public partial class WebPartManagerInternals
  {
    #region Methods and constructors
    public void AddWebPart (WebPart webPart)
    {
    }

    public void CallOnClosing (WebPart webPart)
    {
      Contract.Requires (webPart != null);
    }

    public void CallOnConnectModeChanged (WebPart webPart)
    {
      Contract.Requires (webPart != null);
    }

    public void CallOnDeleting (WebPart webPart)
    {
      Contract.Requires (webPart != null);
    }

    public void CallOnEditModeChanged (WebPart webPart)
    {
      Contract.Requires (webPart != null);
    }

    public bool ConnectionDeleted (WebPartConnection connection)
    {
      Contract.Requires (connection != null);

      return default(bool);
    }

    public Object CreateObjectFromType (Type type)
    {
      return default(Object);
    }

    public void DeleteConnection (WebPartConnection connection)
    {
      Contract.Requires (connection != null);
    }

    public string GetZoneID (WebPart webPart)
    {
      Contract.Requires (webPart != null);

      return default(string);
    }

    public void LoadConfigurationState (WebPartTransformer transformer, Object savedState)
    {
      Contract.Requires (transformer != null);
    }

    public void RemoveWebPart (WebPart webPart)
    {
    }

    public Object SaveConfigurationState (WebPartTransformer transformer)
    {
      Contract.Requires (transformer != null);

      return default(Object);
    }

    public void SetConnectErrorMessage (WebPart webPart, string connectErrorMessage)
    {
      Contract.Requires (webPart != null);
    }

    public void SetHasSharedData (WebPart webPart, bool hasSharedData)
    {
      Contract.Requires (webPart != null);
    }

    public void SetHasUserData (WebPart webPart, bool hasUserData)
    {
      Contract.Requires (webPart != null);
    }

    public void SetIsClosed (WebPart webPart, bool isClosed)
    {
      Contract.Requires (webPart != null);
    }

    public void SetIsShared (WebPart webPart, bool isShared)
    {
      Contract.Requires (webPart != null);
    }

    public void SetIsShared (WebPartConnection connection, bool isShared)
    {
      Contract.Requires (connection != null);
    }

    public void SetIsStandalone (WebPart webPart, bool isStandalone)
    {
      Contract.Requires (webPart != null);
    }

    public void SetIsStatic (WebPart webPart, bool isStatic)
    {
      Contract.Requires (webPart != null);
    }

    public void SetIsStatic (WebPartConnection connection, bool isStatic)
    {
      Contract.Requires (connection != null);
    }

    public void SetTransformer (WebPartConnection connection, WebPartTransformer transformer)
    {
      Contract.Requires (connection != null);
      Contract.Ensures (((System.Array)connection.Transformers).Length >= 0);
    }

    public void SetZoneID (WebPart webPart, string zoneID)
    {
      Contract.Requires (webPart != null);
    }

    public void SetZoneIndex (WebPart webPart, int zoneIndex)
    {
      Contract.Requires (webPart != null);
    }
    #endregion
  }
}
