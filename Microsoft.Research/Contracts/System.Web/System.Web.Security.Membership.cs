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

// File System.Web.Security.Membership.cs
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


namespace System.Web.Security
{
  static public partial class Membership
  {
    #region Methods and constructors
    public static MembershipUser CreateUser (string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, Object providerUserKey, out MembershipCreateStatus status)
    {
      status = default(MembershipCreateStatus);

      return default(MembershipUser);
    }

    public static MembershipUser CreateUser (string username, string password)
    {
      Contract.Ensures (Contract.Result<System.Web.Security.MembershipUser>() != null);

      return default(MembershipUser);
    }

    public static MembershipUser CreateUser (string username, string password, string email)
    {
      Contract.Ensures (Contract.Result<System.Web.Security.MembershipUser>() != null);

      return default(MembershipUser);
    }

    public static MembershipUser CreateUser (string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, out MembershipCreateStatus status)
    {
      status = default(MembershipCreateStatus);

      return default(MembershipUser);
    }

    public static bool DeleteUser (string username)
    {
      return default(bool);
    }

    public static bool DeleteUser (string username, bool deleteAllRelatedData)
    {
      return default(bool);
    }

    public static MembershipUserCollection FindUsersByEmail (string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(MembershipUserCollection);
    }

    public static MembershipUserCollection FindUsersByEmail (string emailToMatch)
    {
      return default(MembershipUserCollection);
    }

    public static MembershipUserCollection FindUsersByName (string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(MembershipUserCollection);
    }

    public static MembershipUserCollection FindUsersByName (string usernameToMatch)
    {
      return default(MembershipUserCollection);
    }

    public static string GeneratePassword (int length, int numberOfNonAlphanumericCharacters)
    {
      return default(string);
    }

    public static MembershipUserCollection GetAllUsers ()
    {
      return default(MembershipUserCollection);
    }

    public static MembershipUserCollection GetAllUsers (int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(MembershipUserCollection);
    }

    public static int GetNumberOfUsersOnline ()
    {
      return default(int);
    }

    public static MembershipUser GetUser (string username)
    {
      return default(MembershipUser);
    }

    public static MembershipUser GetUser (bool userIsOnline)
    {
      return default(MembershipUser);
    }

    public static MembershipUser GetUser ()
    {
      return default(MembershipUser);
    }

    public static MembershipUser GetUser (string username, bool userIsOnline)
    {
      return default(MembershipUser);
    }

    public static MembershipUser GetUser (Object providerUserKey, bool userIsOnline)
    {
      return default(MembershipUser);
    }

    public static MembershipUser GetUser (Object providerUserKey)
    {
      return default(MembershipUser);
    }

    public static string GetUserNameByEmail (string emailToMatch)
    {
      return default(string);
    }

    public static void UpdateUser (MembershipUser user)
    {
    }

    public static bool ValidateUser (string username, string password)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public static string ApplicationName
    {
      get
      {
        Contract.Ensures (Contract.Result<string>() == System.Web.Security.Membership.Provider.ApplicationName);

        return default(string);
      }
      set
      {
      }
    }

    public static bool EnablePasswordReset
    {
      get
      {
        Contract.Ensures (Contract.Result<bool>() == System.Web.Security.Membership.Provider.EnablePasswordReset);

        return default(bool);
      }
    }

    public static bool EnablePasswordRetrieval
    {
      get
      {
        Contract.Ensures (Contract.Result<bool>() == System.Web.Security.Membership.Provider.EnablePasswordRetrieval);

        return default(bool);
      }
    }

    public static string HashAlgorithmType
    {
      get
      {
        return default(string);
      }
    }

    public static int MaxInvalidPasswordAttempts
    {
      get
      {
        Contract.Ensures (Contract.Result<int>() == System.Web.Security.Membership.Provider.MaxInvalidPasswordAttempts);

        return default(int);
      }
    }

    public static int MinRequiredNonAlphanumericCharacters
    {
      get
      {
        Contract.Ensures (Contract.Result<int>() == System.Web.Security.Membership.Provider.MinRequiredNonAlphanumericCharacters);

        return default(int);
      }
    }

    public static int MinRequiredPasswordLength
    {
      get
      {
        Contract.Ensures (Contract.Result<int>() == System.Web.Security.Membership.Provider.MinRequiredPasswordLength);

        return default(int);
      }
    }

    public static int PasswordAttemptWindow
    {
      get
      {
        Contract.Ensures (Contract.Result<int>() == System.Web.Security.Membership.Provider.PasswordAttemptWindow);

        return default(int);
      }
    }

    public static string PasswordStrengthRegularExpression
    {
      get
      {
        Contract.Ensures (Contract.Result<string>() == System.Web.Security.Membership.Provider.PasswordStrengthRegularExpression);

        return default(string);
      }
    }

    public static MembershipProvider Provider
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.Security.MembershipProvider>() != null);

        return default(MembershipProvider);
      }
    }

    public static MembershipProviderCollection Providers
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.Security.MembershipProviderCollection>() != null);

        return default(MembershipProviderCollection);
      }
    }

    public static bool RequiresQuestionAndAnswer
    {
      get
      {
        Contract.Ensures (Contract.Result<bool>() == System.Web.Security.Membership.Provider.RequiresQuestionAndAnswer);

        return default(bool);
      }
    }

    public static int UserIsOnlineTimeWindow
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
