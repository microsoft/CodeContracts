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

// File System.Configuration.SectionInformation.cs
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


namespace System.Configuration
{
  sealed public partial class SectionInformation
  {
    #region Methods and constructors
    public void ForceDeclaration()
    {
      Contract.Ensures(!this.IsLocked);
    }

    public void ForceDeclaration(bool force)
    {
      Contract.Ensures(!this.IsLocked);
    }

    public ConfigurationSection GetParentSection()
    {
      return default(ConfigurationSection);
    }

    public string GetRawXml()
    {
      return default(string);
    }

    public void ProtectSection(string protectionProvider)
    {
    }

    public void RevertToParent()
    {
    }

    internal SectionInformation()
    {
    }

    public void SetRawXml(string rawXml)
    {
    }

    public void UnprotectSection()
    {
    }
    #endregion

    #region Properties and indexers
    public ConfigurationAllowDefinition AllowDefinition
    {
      get
      {
        return default(ConfigurationAllowDefinition);
      }
      set
      {
      }
    }

    public ConfigurationAllowExeDefinition AllowExeDefinition
    {
      get
      {
        return default(ConfigurationAllowExeDefinition);
      }
      set
      {
      }
    }

    public bool AllowLocation
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool AllowOverride
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string ConfigSource
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool ForceSave
    {
      get
      {
        return default(bool);
      }
      set
      {
        Contract.Ensures(!this.IsLocked);
      }
    }

    public bool InheritInChildApplications
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsDeclarationRequired
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsDeclared
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsLocked
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsProtected
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == true);

        return default(bool);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public OverrideMode OverrideMode
    {
      get
      {
        return default(OverrideMode);
      }
      set
      {
      }
    }

    public OverrideMode OverrideModeDefault
    {
      get
      {
        return default(OverrideMode);
      }
      set
      {
      }
    }

    public System.Configuration.OverrideMode OverrideModeEffective
    {
      get
      {
        Contract.Ensures(((System.Configuration.OverrideMode)(1)) <= Contract.Result<System.Configuration.OverrideMode>());
        Contract.Ensures(Contract.Result<System.Configuration.OverrideMode>() <= ((System.Configuration.OverrideMode)(2)));

        return default(System.Configuration.OverrideMode);
      }
    }

    public ProtectedConfigurationProvider ProtectionProvider
    {
      get
      {
        return default(ProtectedConfigurationProvider);
      }
    }

    public bool RequirePermission
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool RestartOnExternalChanges
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string SectionName
    {
      get
      {
        return default(string);
      }
    }

    public string Type
    {
      get
      {
        return default(string);
      }
      set
      {
        Contract.Ensures(!string.IsNullOrEmpty(value));
        Contract.Ensures(0 <= value.Length);
      }
    }
    #endregion
  }
}
