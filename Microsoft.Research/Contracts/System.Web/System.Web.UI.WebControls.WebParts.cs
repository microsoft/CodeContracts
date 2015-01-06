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

// File System.Web.UI.WebControls.WebParts.cs
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
  public delegate void FieldCallback (Object fieldValue);

  public delegate void ParametersCallback (System.Collections.IDictionary parametersData);

  public enum PartChromeState
  {
    Normal = 0, 
    Minimized = 1, 
  }

  public enum PartChromeType
  {
    Default = 0, 
    TitleAndBorder = 1, 
    None = 2, 
    TitleOnly = 3, 
    BorderOnly = 4, 
  }

  public enum PersonalizationScope
  {
    User = 0, 
    Shared = 1, 
  }

  public delegate void RowCallback (Object rowData);

  public delegate void TableCallback (System.Collections.ICollection tableData);

  public delegate void WebPartAddingEventHandler (Object sender, WebPartAddingEventArgs e);

  public delegate void WebPartAuthorizationEventHandler (Object sender, WebPartAuthorizationEventArgs e);

  public delegate void WebPartCancelEventHandler (Object sender, WebPartCancelEventArgs e);

  public delegate void WebPartConnectionsCancelEventHandler (Object sender, WebPartConnectionsCancelEventArgs e);

  public delegate void WebPartConnectionsEventHandler (Object sender, WebPartConnectionsEventArgs e);

  public delegate void WebPartDisplayModeCancelEventHandler (Object sender, WebPartDisplayModeCancelEventArgs e);

  public delegate void WebPartDisplayModeEventHandler (Object sender, WebPartDisplayModeEventArgs e);

  public delegate void WebPartEventHandler (Object sender, WebPartEventArgs e);

  public enum WebPartExportMode
  {
    None = 0, 
    All = 1, 
    NonSensitiveData = 2, 
  }

  public enum WebPartHelpMode
  {
    Modal = 0, 
    Modeless = 1, 
    Navigate = 2, 
  }

  public delegate void WebPartMovingEventHandler (Object sender, WebPartMovingEventArgs e);

  public enum WebPartVerbRenderMode
  {
    Menu = 0, 
    TitleBar = 1, 
  }

  public delegate void WebPartVerbsEventHandler (Object sender, WebPartVerbsEventArgs e);
}
