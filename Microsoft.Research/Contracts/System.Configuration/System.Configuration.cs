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

// File System.Configuration.cs
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


namespace System.Configuration
{
  public enum ConfigurationAllowDefinition
  {
    MachineOnly = 0, 
    MachineToWebRoot = 100, 
    MachineToApplication = 200, 
    Everywhere = 300, 
  }

  public enum ConfigurationAllowExeDefinition
  {
    MachineOnly = 0, 
    MachineToApplication = 100, 
    MachineToRoamingUser = 200, 
    MachineToLocalUser = 300, 
  }

  public enum ConfigurationElementCollectionType
  {
    BasicMap = 0, 
    AddRemoveClearMap = 1, 
    BasicMapAlternate = 2, 
    AddRemoveClearMapAlternate = 3, 
  }

  public enum ConfigurationPropertyOptions
  {
    None = 0, 
    IsDefaultCollection = 1, 
    IsRequired = 2, 
    IsKey = 4,
#if FRAMEWORK_4_0
    IsTypeStringTransformationRequired = 8,
    IsAssemblyStringTransformationRequired = 16, 
    IsVersionCheckRequired = 32, 
#endif
  }

  public enum ConfigurationSaveMode
  {
    Modified = 0, 
    Minimal = 1, 
    Full = 2, 
  }

  public enum ConfigurationUserLevel
  {
    None = 0, 
    PerUserRoaming = 10, 
    PerUserRoamingAndLocal = 20, 
  }

  public enum OverrideMode
  {
    Inherit = 0, 
    Allow = 1, 
    Deny = 2, 
  }

  public enum PropertyValueOrigin
  {
    Default = 0, 
    Inherited = 1, 
    SetHere = 2, 
  }

  public delegate void ValidatorCallback (Object value);
}
