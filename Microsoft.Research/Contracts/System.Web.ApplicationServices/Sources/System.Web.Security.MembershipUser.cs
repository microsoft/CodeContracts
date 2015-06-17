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

// File System.Web.Security.MembershipUser.cs
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
  public partial class MembershipUser
  {
    #region Methods and constructors
    public virtual new bool ChangePassword(string oldPassword, string newPassword)
    {
      return default(bool);
    }

    public virtual new bool ChangePasswordQuestionAndAnswer(string password, string newPasswordQuestion, string newPasswordAnswer)
    {
      return default(bool);
    }

    public virtual new string GetPassword()
    {
      return default(string);
    }

    public virtual new string GetPassword(string passwordAnswer)
    {
      return default(string);
    }

    public MembershipUser(string providerName, string name, Object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate)
    {
    }

    protected MembershipUser()
    {
    }

    public virtual new string ResetPassword()
    {
      return default(string);
    }

    public virtual new string ResetPassword(string passwordAnswer)
    {
      return default(string);
    }

    public virtual new bool UnlockUser()
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public virtual new string Comment
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new DateTime CreationDate
    {
      get
      {
        return default(DateTime);
      }
    }

    public virtual new string Email
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool IsApproved
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool IsLockedOut
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsOnline
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new DateTime LastActivityDate
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public virtual new DateTime LastLockoutDate
    {
      get
      {
        return default(DateTime);
      }
    }

    public virtual new DateTime LastLoginDate
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public virtual new DateTime LastPasswordChangedDate
    {
      get
      {
        return default(DateTime);
      }
    }

    public virtual new string PasswordQuestion
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string ProviderName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new Object ProviderUserKey
    {
      get
      {
        return default(Object);
      }
    }

    public virtual new string UserName
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
