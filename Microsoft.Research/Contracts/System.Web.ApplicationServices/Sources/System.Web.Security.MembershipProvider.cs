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

// File System.Web.Security.MembershipProvider.cs
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
  abstract public partial class MembershipProvider : System.Configuration.Provider.ProviderBase
  {
    #region Methods and constructors
    public abstract bool ChangePassword(string username, string oldPassword, string newPassword);

    public abstract bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer);

    public abstract MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, Object providerUserKey, out MembershipCreateStatus status);

    protected virtual new byte[] DecryptPassword(byte[] encodedPassword)
    {
      return default(byte[]);
    }

    public abstract bool DeleteUser(string username, bool deleteAllRelatedData);

    protected virtual new byte[] EncryptPassword(byte[] password, System.Web.Configuration.MembershipPasswordCompatibilityMode legacyPasswordCompatibilityMode)
    {
      return default(byte[]);
    }

    protected virtual new byte[] EncryptPassword(byte[] password)
    {
      return default(byte[]);
    }

    public abstract MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords);

    public abstract MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

    public abstract MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);

    public abstract int GetNumberOfUsersOnline();

    public abstract string GetPassword(string username, string answer);

    public abstract MembershipUser GetUser(Object providerUserKey, bool userIsOnline);

    public abstract MembershipUser GetUser(string username, bool userIsOnline);

    public abstract string GetUserNameByEmail(string email);

    protected MembershipProvider()
    {
    }

    protected virtual new void OnValidatingPassword(ValidatePasswordEventArgs e)
    {
    }

    public abstract string ResetPassword(string username, string answer);

    public abstract bool UnlockUser(string userName);

    public abstract void UpdateUser(MembershipUser user);

    public abstract bool ValidateUser(string username, string password);
    #endregion

    #region Properties and indexers
    public abstract string ApplicationName
    {
      get;
      set;
    }

    public abstract bool EnablePasswordReset
    {
      get;
    }

    public abstract bool EnablePasswordRetrieval
    {
      get;
    }

    public abstract int MaxInvalidPasswordAttempts
    {
      get;
    }

    public abstract int MinRequiredNonAlphanumericCharacters
    {
      get;
    }

    public abstract int MinRequiredPasswordLength
    {
      get;
    }

    public abstract int PasswordAttemptWindow
    {
      get;
    }

    public abstract MembershipPasswordFormat PasswordFormat
    {
      get;
    }

    public abstract string PasswordStrengthRegularExpression
    {
      get;
    }

    public abstract bool RequiresQuestionAndAnswer
    {
      get;
    }

    public abstract bool RequiresUniqueEmail
    {
      get;
    }
    #endregion

    #region Events
    public event MembershipValidatePasswordEventHandler ValidatingPassword
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
