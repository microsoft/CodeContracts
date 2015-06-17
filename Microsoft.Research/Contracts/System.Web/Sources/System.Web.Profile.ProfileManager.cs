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

// File System.Web.Profile.ProfileManager.cs
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


namespace System.Web.Profile
{
  static public partial class ProfileManager
  {
    #region Methods and constructors
    public static void AddDynamicProfileProperty(System.Web.Configuration.ProfilePropertySettings property)
    {
    }

    public static int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
    {
      return default(int);
    }

    public static bool DeleteProfile(string username)
    {
      return default(bool);
    }

    public static int DeleteProfiles(string[] usernames)
    {
      return default(int);
    }

    public static int DeleteProfiles(ProfileInfoCollection profiles)
    {
      return default(int);
    }

    public static ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate)
    {
      return default(ProfileInfoCollection);
    }

    public static ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(ProfileInfoCollection);
    }

    public static ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch)
    {
      return default(ProfileInfoCollection);
    }

    public static ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(ProfileInfoCollection);
    }

    public static ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
    {
      return default(ProfileInfoCollection);
    }

    public static ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(ProfileInfoCollection);
    }

    public static ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption)
    {
      return default(ProfileInfoCollection);
    }

    public static ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(ProfileInfoCollection);
    }

    public static int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
    {
      return default(int);
    }

    public static int GetNumberOfProfiles(ProfileAuthenticationOption authenticationOption)
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public static string ApplicationName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public static bool AutomaticSaveEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public static bool Enabled
    {
      get
      {
        return default(bool);
      }
    }

    public static ProfileProvider Provider
    {
      get
      {
        return default(ProfileProvider);
      }
    }

    public static ProfileProviderCollection Providers
    {
      get
      {
        return default(ProfileProviderCollection);
      }
    }
    #endregion
  }
}
