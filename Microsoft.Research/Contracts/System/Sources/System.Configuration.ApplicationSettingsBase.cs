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

// File System.Configuration.ApplicationSettingsBase.cs
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
  abstract public partial class ApplicationSettingsBase : SettingsBase, System.ComponentModel.INotifyPropertyChanged
  {
    #region Methods and constructors
    protected ApplicationSettingsBase(string settingsKey)
    {
    }

    protected ApplicationSettingsBase(System.ComponentModel.IComponent owner, string settingsKey)
    {
    }

    protected ApplicationSettingsBase()
    {
    }

    protected ApplicationSettingsBase(System.ComponentModel.IComponent owner)
    {
    }

    public Object GetPreviousVersion(string propertyName)
    {
      Contract.Requires(this.Properties != null);

      return default(Object);
    }

    protected virtual new void OnPropertyChanged(Object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
    }

    protected virtual new void OnSettingChanging(Object sender, SettingChangingEventArgs e)
    {
    }

    protected virtual new void OnSettingsLoaded(Object sender, SettingsLoadedEventArgs e)
    {
    }

    protected virtual new void OnSettingsSaving(Object sender, System.ComponentModel.CancelEventArgs e)
    {
    }

    public void Reload()
    {
    }

    public void Reset()
    {
    }

    public override void Save()
    {
    }

    public virtual new void Upgrade()
    {
    }
    #endregion

    #region Properties and indexers
    public override SettingsContext Context
    {
      get
      {
        return default(SettingsContext);
      }
    }

    public override Object this [string propertyName]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public override SettingsPropertyCollection Properties
    {
      get
      {
        return default(SettingsPropertyCollection);
      }
    }

    public override SettingsPropertyValueCollection PropertyValues
    {
      get
      {
        return default(SettingsPropertyValueCollection);
      }
    }

    public override SettingsProviderCollection Providers
    {
      get
      {
        return default(SettingsProviderCollection);
      }
    }

    public string SettingsKey
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SettingChangingEventHandler SettingChanging
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SettingsLoadedEventHandler SettingsLoaded
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SettingsSavingEventHandler SettingsSaving
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
