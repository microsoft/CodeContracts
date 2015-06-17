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

// File System.Web.Profile.ProfileBase.cs
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


namespace System.Web.Profile
{
  public partial class ProfileBase : System.Configuration.SettingsBase
  {
    #region Methods and constructors
    public static System.Web.Profile.ProfileBase Create (string username)
    {
      Contract.Ensures (Contract.Result<System.Web.Profile.ProfileBase>() != null);

      return default(System.Web.Profile.ProfileBase);
    }

    public static System.Web.Profile.ProfileBase Create (string username, bool isAuthenticated)
    {
      Contract.Ensures (Contract.Result<System.Web.Profile.ProfileBase>() != null);

      return default(System.Web.Profile.ProfileBase);
    }

    public ProfileGroupBase GetProfileGroup (string groupName)
    {
      Contract.Ensures (Contract.Result<System.Web.Profile.ProfileGroupBase>() != null);

      return default(ProfileGroupBase);
    }

    public Object GetPropertyValue (string propertyName)
    {
      return default(Object);
    }

    public void Initialize (string username, bool isAuthenticated)
    {
    }

    public ProfileBase ()
    {
    }

    public override void Save ()
    {
    }

    public void SetPropertyValue (string propertyName, Object propertyValue)
    {
    }
    #endregion

    #region Properties and indexers
    public bool IsAnonymous
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsDirty
    {
      get
      {
        return default(bool);
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

    public DateTime LastActivityDate
    {
      get
      {
        return default(DateTime);
      }
    }

    public DateTime LastUpdatedDate
    {
      get
      {
        return default(DateTime);
      }
    }

    new public static System.Configuration.SettingsPropertyCollection Properties
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Configuration.SettingsPropertyCollection>() != null);

        return default(System.Configuration.SettingsPropertyCollection);
      }
    }

    public string UserName
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
