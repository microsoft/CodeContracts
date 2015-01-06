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

// File System.ServiceModel.Channels.LocalClientSecuritySettings.cs
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


namespace System.ServiceModel.Channels
{
  sealed public partial class LocalClientSecuritySettings
  {
    #region Methods and constructors
    public System.ServiceModel.Channels.LocalClientSecuritySettings Clone()
    {
      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.LocalClientSecuritySettings>() != null);

      return default(System.ServiceModel.Channels.LocalClientSecuritySettings);
    }

    public LocalClientSecuritySettings()
    {
      Contract.Ensures(this.CookieRenewalThresholdPercentage == 60);
      Contract.Ensures(this.ReconnectTransportOnFailure == true);
      Contract.Ensures(this.ReplayCacheSize == 900000);
    }
    #endregion

    #region Properties and indexers
    public bool CacheCookies
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int CookieRenewalThresholdPercentage
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        Contract.Ensures(Contract.Result<int>() <= 100);
        return default(int);
      }
      set
      {
        Contract.Requires(0 <= value);
        Contract.Requires(value <= 100);
      }
    }

    public bool DetectReplays
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.ServiceModel.Security.IdentityVerifier IdentityVerifier
    {
      get
      {
        return default(System.ServiceModel.Security.IdentityVerifier);
      }
      set
      {
      }
    }

    public TimeSpan MaxClockSkew
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        return default(TimeSpan);
      }
      set
      {
        Contract.Requires(TimeSpan.Zero <= value);
      }
    }

    public TimeSpan MaxCookieCachingTime
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        return default(TimeSpan);
      }
      set
      {
        Contract.Requires(TimeSpan.Zero <= value);
      }
    }

    public bool ReconnectTransportOnFailure
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int ReplayCacheSize
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        return default(int);
      }
      set
      {
        Contract.Requires(0 <= value);
      }
    }

    public TimeSpan ReplayWindow
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public TimeSpan SessionKeyRenewalInterval
    {
      get
      {
        Contract.Ensures(TimeSpan.Zero <= Contract.Result<TimeSpan>());
        return default(TimeSpan);
      }
      set
      {
        Contract.Requires(TimeSpan.Zero <= value);
      }
    }

    public TimeSpan SessionKeyRolloverInterval
    {
      get
      {
        Contract.Ensures(TimeSpan.Zero <= Contract.Result<TimeSpan>());
        return default(TimeSpan);
      }
      set
      {
        Contract.Requires(TimeSpan.Zero <= value);
      }
    }

    public TimeSpan TimestampValidityDuration
    {
      get
      {
        Contract.Ensures(TimeSpan.Zero <= Contract.Result<TimeSpan>());
        return default(TimeSpan);
      }
      set
      {
        Contract.Requires(TimeSpan.Zero <= value);
      }
    }
    #endregion
  }
}
