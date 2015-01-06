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

// File System.Diagnostics.TraceSwitch.cs
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


namespace System.Diagnostics
{
  public partial class TraceSwitch : Switch
  {
    #region Methods and constructors
    protected override void OnSwitchSettingChanged()
    {
    }

    protected override void OnValueChanged()
    {
    }

    public TraceSwitch(string displayName, string description, string defaultSwitchValue) : base (default(string), default(string))
    {
    }

    public TraceSwitch(string displayName, string description) : base (default(string), default(string))
    {
      Contract.Ensures(0 <= description.Length);
    }
    #endregion

    #region Properties and indexers
    public TraceLevel Level
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Diagnostics.TraceLevel>() == this.SwitchSetting);

        return default(TraceLevel);
      }
      set
      {
      }
    }

    public bool TraceError
    {
      get
      {
        Contract.Ensures(0 <= this.SwitchSetting);
        Contract.Ensures(Contract.Result<bool>() == ((this.Level < ((System.Diagnostics.TraceLevel)(1))) == false));
        Contract.Ensures(this.SwitchSetting <= 4);
        Contract.Ensures(this.SwitchSetting == this.Level);

        return default(bool);
      }
    }

    public bool TraceInfo
    {
      get
      {
        Contract.Ensures(0 <= this.SwitchSetting);
        Contract.Ensures(Contract.Result<bool>() == ((this.Level < ((System.Diagnostics.TraceLevel)(3))) == false));
        Contract.Ensures(this.SwitchSetting <= 4);
        Contract.Ensures(this.SwitchSetting == this.Level);

        return default(bool);
      }
    }

    public bool TraceVerbose
    {
      get
      {
        Contract.Ensures(0 <= this.SwitchSetting);
        Contract.Ensures(Contract.Result<bool>() == (this.Level == ((System.Diagnostics.TraceLevel)(4))));
        Contract.Ensures(this.SwitchSetting <= 4);
        Contract.Ensures(this.SwitchSetting == this.Level);

        return default(bool);
      }
    }

    public bool TraceWarning
    {
      get
      {
        Contract.Ensures(0 <= this.SwitchSetting);
        Contract.Ensures(Contract.Result<bool>() == ((this.Level < ((System.Diagnostics.TraceLevel)(2))) == false));
        Contract.Ensures(this.SwitchSetting <= 4);
        Contract.Ensures(this.SwitchSetting == this.Level);

        return default(bool);
      }
    }
    #endregion
  }
}
