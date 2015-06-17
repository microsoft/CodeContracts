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

// File System.Web.Security.FormsAuthentication.cs
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
  sealed public partial class FormsAuthentication
  {
    #region Methods and constructors
    public static bool Authenticate(string name, string password)
    {
      return default(bool);
    }

    public static FormsAuthenticationTicket Decrypt(string encryptedTicket)
    {
      return default(FormsAuthenticationTicket);
    }

    public static void EnableFormsAuthentication(System.Collections.Specialized.NameValueCollection configurationData)
    {
    }

    public static string Encrypt(FormsAuthenticationTicket ticket)
    {
      return default(string);
    }

    public FormsAuthentication()
    {
    }

    public static System.Web.HttpCookie GetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath)
    {
      return default(System.Web.HttpCookie);
    }

    public static System.Web.HttpCookie GetAuthCookie(string userName, bool createPersistentCookie)
    {
      return default(System.Web.HttpCookie);
    }

    public static string GetRedirectUrl(string userName, bool createPersistentCookie)
    {
      return default(string);
    }

    public static string HashPasswordForStoringInConfigFile(string password, string passwordFormat)
    {
      return default(string);
    }

    public static void Initialize()
    {
    }

    public static void RedirectFromLoginPage(string userName, bool createPersistentCookie, string strCookiePath)
    {
    }

    public static void RedirectFromLoginPage(string userName, bool createPersistentCookie)
    {
    }

    public static void RedirectToLoginPage()
    {
    }

    public static void RedirectToLoginPage(string extraQueryString)
    {
    }

    public static FormsAuthenticationTicket RenewTicketIfOld(FormsAuthenticationTicket tOld)
    {
      return default(FormsAuthenticationTicket);
    }

    public static void SetAuthCookie(string userName, bool createPersistentCookie)
    {
    }

    public static void SetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath)
    {
    }

    public static void SignOut()
    {
    }
    #endregion

    #region Properties and indexers
    public static string CookieDomain
    {
      get
      {
        return default(string);
      }
    }

    public static System.Web.HttpCookieMode CookieMode
    {
      get
      {
        return default(System.Web.HttpCookieMode);
      }
    }

    public static bool CookiesSupported
    {
      get
      {
        return default(bool);
      }
    }

    public static string DefaultUrl
    {
      get
      {
        return default(string);
      }
    }

    public static bool EnableCrossAppRedirects
    {
      get
      {
        return default(bool);
      }
    }

    public static string FormsCookieName
    {
      get
      {
        return default(string);
      }
    }

    public static string FormsCookiePath
    {
      get
      {
        return default(string);
      }
    }

    public static bool IsEnabled
    {
      get
      {
        return default(bool);
      }
    }

    public static string LoginUrl
    {
      get
      {
        return default(string);
      }
    }

    public static bool RequireSSL
    {
      get
      {
        return default(bool);
      }
    }

    public static bool SlidingExpiration
    {
      get
      {
        return default(bool);
      }
    }

    public static System.Web.Configuration.TicketCompatibilityMode TicketCompatibilityMode
    {
      get
      {
        return default(System.Web.Configuration.TicketCompatibilityMode);
      }
    }

    public static TimeSpan Timeout
    {
      get
      {
        return default(TimeSpan);
      }
    }
    #endregion
  }
}
