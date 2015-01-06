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

// File System.Web.Configuration.PagesSection.cs
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


namespace System.Web.Configuration
{
  sealed public partial class PagesSection : System.Configuration.ConfigurationSection
  {
    #region Methods and constructors
    protected override void DeserializeSection(System.Xml.XmlReader reader)
    {
    }

    public PagesSection()
    {
    }
    #endregion

    #region Properties and indexers
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

    public bool AutoEventWireup
    {
      get
      {
        return default(bool);
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

    public System.Web.UI.ClientIDMode ClientIDMode
    {
      get
      {
        return default(System.Web.UI.ClientIDMode);
      }
      set
      {
      }
    }

    public System.Web.UI.CompilationMode CompilationMode
    {
      get
      {
        return default(System.Web.UI.CompilationMode);
      }
      set
      {
      }
    }

    public Version ControlRenderingCompatibilityVersion
    {
      get
      {
        return default(Version);
      }
      set
      {
      }
    }

    public TagPrefixCollection Controls
    {
      get
      {
        return default(TagPrefixCollection);
      }
    }

    public bool EnableEventValidation
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public PagesEnableSessionState EnableSessionState
    {
      get
      {
        return default(PagesEnableSessionState);
      }
      set
      {
      }
    }

    public bool EnableViewState
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

    public IgnoreDeviceFilterElementCollection IgnoreDeviceFilters
    {
      get
      {
        return default(IgnoreDeviceFilterElementCollection);
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

    public string MasterPageFile
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

    public NamespaceCollection Namespaces
    {
      get
      {
        return default(NamespaceCollection);
      }
    }

    public string PageBaseType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string PageParserFilterType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected override System.Configuration.ConfigurationPropertyCollection Properties
    {
      get
      {
        return default(System.Configuration.ConfigurationPropertyCollection);
      }
    }

    public bool RenderAllHiddenFieldsAtTopOfForm
    {
      get
      {
        return default(bool);
      }
      set
      {
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

    public string StyleSheetTheme
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TagMapCollection TagMapping
    {
      get
      {
        return default(TagMapCollection);
      }
    }

    public string Theme
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string UserControlBaseType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool ValidateRequest
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Web.UI.ViewStateEncryptionMode ViewStateEncryptionMode
    {
      get
      {
        return default(System.Web.UI.ViewStateEncryptionMode);
      }
      set
      {
      }
    }
    #endregion
  }
}
