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

// File System.Configuration.Configuration.cs
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
  sealed public partial class Configuration
  {
    #region Methods and constructors
    private Configuration() { }

    public ConfigurationSection GetSection (string sectionName)
    {
      return default(ConfigurationSection);
    }

    public ConfigurationSectionGroup GetSectionGroup (string sectionGroupName)
    {
      Contract.Requires (sectionGroupName != null);

      return default(ConfigurationSectionGroup);
    }

    public void Save ()
    {
    }

    public void Save (ConfigurationSaveMode saveMode, bool forceSaveAll)
    {
    }

    public void Save (ConfigurationSaveMode saveMode)
    {
    }

    public void SaveAs (string filename)
    {
    }

    public void SaveAs (string filename, ConfigurationSaveMode saveMode, bool forceSaveAll)
    {
    }

    public void SaveAs (string filename, ConfigurationSaveMode saveMode)
    {
    }
    #endregion

    #region Properties and indexers
    public AppSettingsSection AppSettings
    {
      get
      {
        return default(AppSettingsSection);
      }
    }

#if NETFRAMEWORK_4_0
    public Func<string, string> AssemblyStringTransformer
    {
      get
      {
        return default(Func<string, string>);
      }
      set
      {
      }
    }
#endif

    public ConnectionStringsSection ConnectionStrings
    {
      get
      {
        return default(ConnectionStringsSection);
      }
    }

    public ContextInformation EvaluationContext
    {
      get
      {
        return default(ContextInformation);
      }
    }

    public string FilePath
    {
      get
      {
        return default(string);
      }
    }

    public bool HasFile
    {
      get
      {
        return default(bool);
      }
    }

    public ConfigurationLocationCollection Locations
    {
      get
      {
        return default(ConfigurationLocationCollection);
      }
    }

    public bool NamespaceDeclared
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ConfigurationSectionGroup RootSectionGroup
    {
      get
      {
        return default(ConfigurationSectionGroup);
      }
    }

    public ConfigurationSectionGroupCollection SectionGroups
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Configuration.ConfigurationSectionGroupCollection>() == this.RootSectionGroup.SectionGroups);

        return default(ConfigurationSectionGroupCollection);
      }
    }

    public ConfigurationSectionCollection Sections
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Configuration.ConfigurationSectionCollection>() == this.RootSectionGroup.Sections);

        return default(ConfigurationSectionCollection);
      }
    }

#if NETFRAMEWORK_4_0
    public System.Runtime.Versioning.FrameworkName TargetFramework
    {
      get
      {
        return default(System.Runtime.Versioning.FrameworkName);
      }
      set
      {
      }
    }

    public Func<string, string> TypeStringTransformer
    {
      get
      {
        return default(Func<string, string>);
      }
      set
      {
      }
    }
#endif

    #endregion
  }
}
