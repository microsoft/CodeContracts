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

// File System.Web.Security.SqlMembershipProvider.cs
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
  public partial class SqlMembershipProvider : MembershipProvider
  {
    #region Methods and constructors
    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
      return default(bool);
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
      return default(bool);
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, Object providerUserKey, out MembershipCreateStatus status)
    {
      status = default(MembershipCreateStatus);

      return default(MembershipUser);
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      return default(bool);
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(MembershipUserCollection);
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(MembershipUserCollection);
    }

    public virtual new string GeneratePassword()
    {
      return default(string);
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
      totalRecords = default(int);

      return default(MembershipUserCollection);
    }

    public override int GetNumberOfUsersOnline()
    {
      return default(int);
    }

    public override string GetPassword(string username, string passwordAnswer)
    {
      return default(string);
    }

    public override MembershipUser GetUser(Object providerUserKey, bool userIsOnline)
    {
      return default(MembershipUser);
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
      return default(MembershipUser);
    }

    public override string GetUserNameByEmail(string email)
    {
      return default(string);
    }

    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
    {
    }

    public override string ResetPassword(string username, string passwordAnswer)
    {
      return default(string);
    }

    public SqlMembershipProvider()
    {
    }

    public override bool UnlockUser(string username)
    {
      return default(bool);
    }

    public override void UpdateUser(MembershipUser user)
    {
    }

    public override bool ValidateUser(string username, string password)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public override string ApplicationName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override bool EnablePasswordReset
    {
      get
      {
        return default(bool);
      }
    }

    public override bool EnablePasswordRetrieval
    {
      get
      {
        return default(bool);
      }
    }

    public override int MaxInvalidPasswordAttempts
    {
      get
      {
        return default(int);
      }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
      get
      {
        return default(int);
      }
    }

    public override int MinRequiredPasswordLength
    {
      get
      {
        return default(int);
      }
    }

    public override int PasswordAttemptWindow
    {
      get
      {
        return default(int);
      }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
      get
      {
        return default(MembershipPasswordFormat);
      }
    }

    public override string PasswordStrengthRegularExpression
    {
      get
      {
        return default(string);
      }
    }

    public override bool RequiresQuestionAndAnswer
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresUniqueEmail
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
