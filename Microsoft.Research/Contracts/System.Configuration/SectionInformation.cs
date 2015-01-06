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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Configuration
{
  public sealed class SectionInformation
  {
    private SectionInformation() { }
    // Methods

    // public ConfigurationSection GetParentSection();
    // public string GetRawXml();

    // public void ProtectSection(string protectionProvider);

    // public void RevertToParent();
    // public void SetRawXml(string rawXml);
    // public void UnprotectSection();

    // Properties
    // public ConfigurationAllowDefinition AllowDefinition { get; set; }
    // public ConfigurationAllowExeDefinition AllowExeDefinition { get; set; }

    // public bool AllowLocation { get; set; }

    // public bool AllowOverride { get; set; }

    public string ConfigSource
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
    // public bool ForceSave { get; set; }
    // public bool InheritInChildApplications { get; set; }
    // public bool IsDeclarationRequired { get; }
    // public bool IsDeclared { get; }

    // public bool IsLocked { get; }
    // public bool IsProtected { get; }

    public string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    // public OverrideMode OverrideMode { get; set; }
    // public OverrideMode OverrideModeDefault { get; set; }
    // public OverrideMode OverrideModeEffective { get; }
    public ProtectedConfigurationProvider ProtectionProvider
    {
      get
      {
        Contract.Ensures(Contract.Result<ProtectedConfigurationProvider>() != null);
        return default(ProtectedConfigurationProvider);
      }
    }
    // public bool RequirePermission { get; set; }
    // public bool RestartOnExternalChanges { get; set; }
    public string SectionName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public string Type
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }
  }

}
