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

// File System.Web.UI.WebControls.WebParts.PersonalizationAdministration.cs
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


namespace System.Web.UI.WebControls.WebParts
{
  static public partial class PersonalizationAdministration
  {
    #region Methods and constructors
    public static PersonalizationStateInfoCollection FindInactiveUserState(string pathToMatch, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(PersonalizationStateInfoCollection);
    }

    public static PersonalizationStateInfoCollection FindInactiveUserState(string pathToMatch, string usernameToMatch, DateTime userInactiveSinceDate)
    {
      return default(PersonalizationStateInfoCollection);
    }

    public static PersonalizationStateInfoCollection FindSharedState(string pathToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(PersonalizationStateInfoCollection);
    }

    public static PersonalizationStateInfoCollection FindSharedState(string pathToMatch)
    {
      return default(PersonalizationStateInfoCollection);
    }

    public static PersonalizationStateInfoCollection FindUserState(string pathToMatch, string usernameToMatch)
    {
      return default(PersonalizationStateInfoCollection);
    }

    public static PersonalizationStateInfoCollection FindUserState(string pathToMatch, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(PersonalizationStateInfoCollection);
    }

    public static PersonalizationStateInfoCollection GetAllInactiveUserState(DateTime userInactiveSinceDate)
    {
      return default(PersonalizationStateInfoCollection);
    }

    public static PersonalizationStateInfoCollection GetAllInactiveUserState(DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(PersonalizationStateInfoCollection);
    }

    public static PersonalizationStateInfoCollection GetAllState(PersonalizationScope scope)
    {
      return default(PersonalizationStateInfoCollection);
    }

    public static PersonalizationStateInfoCollection GetAllState(PersonalizationScope scope, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(PersonalizationStateInfoCollection);
    }

    public static int GetCountOfInactiveUserState(DateTime userInactiveSinceDate)
    {
      return default(int);
    }

    public static int GetCountOfInactiveUserState(string pathToMatch, DateTime userInactiveSinceDate)
    {
      return default(int);
    }

    public static int GetCountOfState(PersonalizationScope scope)
    {
      return default(int);
    }

    public static int GetCountOfState(PersonalizationScope scope, string pathToMatch)
    {
      return default(int);
    }

    public static int GetCountOfUserState(string usernameToMatch)
    {
      return default(int);
    }

    public static int ResetAllState(PersonalizationScope scope)
    {
      return default(int);
    }

    public static int ResetInactiveUserState(string path, DateTime userInactiveSinceDate)
    {
      return default(int);
    }

    public static int ResetInactiveUserState(DateTime userInactiveSinceDate)
    {
      return default(int);
    }

    public static bool ResetSharedState(string path)
    {
      return default(bool);
    }

    public static int ResetSharedState(string[] paths)
    {
      return default(int);
    }

    public static int ResetState(PersonalizationStateInfoCollection data)
    {
      return default(int);
    }

    public static int ResetUserState(string[] usernames)
    {
      return default(int);
    }

    public static int ResetUserState(string path, string[] usernames)
    {
      return default(int);
    }

    public static bool ResetUserState(string path, string username)
    {
      return default(bool);
    }

    public static int ResetUserState(string path)
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

    public static PersonalizationProvider Provider
    {
      get
      {
        return default(PersonalizationProvider);
      }
    }

    public static PersonalizationProviderCollection Providers
    {
      get
      {
        return default(PersonalizationProviderCollection);
      }
    }
    #endregion
  }
}
