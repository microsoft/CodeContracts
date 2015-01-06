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

// File System.Web.Security.Roles.cs
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


namespace System.Web.Security
{
  static public partial class Roles
  {
    #region Methods and constructors
    public static void AddUsersToRole(string[] usernames, string roleName)
    {
    }

    public static void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
    }

    public static void AddUserToRole(string username, string roleName)
    {
    }

    public static void AddUserToRoles(string username, string[] roleNames)
    {
    }

    public static void CreateRole(string roleName)
    {
    }

    public static void DeleteCookie()
    {
    }

    public static bool DeleteRole(string roleName)
    {
      return default(bool);
    }

    public static bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
      return default(bool);
    }

    public static string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
      return default(string[]);
    }

    public static string[] GetAllRoles()
    {
      return default(string[]);
    }

    public static string[] GetRolesForUser()
    {
      return default(string[]);
    }

    public static string[] GetRolesForUser(string username)
    {
      return default(string[]);
    }

    public static string[] GetUsersInRole(string roleName)
    {
      return default(string[]);
    }

    public static bool IsUserInRole(string roleName)
    {
      return default(bool);
    }

    public static bool IsUserInRole(string username, string roleName)
    {
      return default(bool);
    }

    public static void RemoveUserFromRole(string username, string roleName)
    {
    }

    public static void RemoveUserFromRoles(string username, string[] roleNames)
    {
    }

    public static void RemoveUsersFromRole(string[] usernames, string roleName)
    {
    }

    public static void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
    }

    public static bool RoleExists(string roleName)
    {
      return default(bool);
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

    public static bool CacheRolesInCookie
    {
      get
      {
        return default(bool);
      }
    }

    public static string CookieName
    {
      get
      {
        return default(string);
      }
    }

    public static string CookiePath
    {
      get
      {
        return default(string);
      }
    }

    public static CookieProtection CookieProtectionValue
    {
      get
      {
        return default(CookieProtection);
      }
    }

    public static bool CookieRequireSSL
    {
      get
      {
        return default(bool);
      }
    }

    public static bool CookieSlidingExpiration
    {
      get
      {
        return default(bool);
      }
    }

    public static int CookieTimeout
    {
      get
      {
        return default(int);
      }
    }

    public static bool CreatePersistentCookie
    {
      get
      {
        return default(bool);
      }
    }

    public static string Domain
    {
      get
      {
        return default(string);
      }
    }

    public static bool Enabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public static int MaxCachedResults
    {
      get
      {
        return default(int);
      }
    }

    public static RoleProvider Provider
    {
      get
      {
        return default(RoleProvider);
      }
    }

    public static RoleProviderCollection Providers
    {
      get
      {
        return default(RoleProviderCollection);
      }
    }
    #endregion
  }
}
