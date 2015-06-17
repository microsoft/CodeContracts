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

// File System.Web.Security.PassportIdentity.cs
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
  sealed public partial class PassportIdentity : System.Security.Principal.IIdentity, IDisposable
  {
    #region Methods and constructors
    public string AuthUrl(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth)
    {
      return default(string);
    }

    public string AuthUrl(string strReturnUrl)
    {
      return default(string);
    }

    public string AuthUrl()
    {
      return default(string);
    }

    public string AuthUrl(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, bool bUseSecureAuth)
    {
      return default(string);
    }

    public string AuthUrl2(string strReturnUrl)
    {
      return default(string);
    }

    public string AuthUrl2()
    {
      return default(string);
    }

    public string AuthUrl2(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, bool bUseSecureAuth)
    {
      return default(string);
    }

    public string AuthUrl2(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth)
    {
      return default(string);
    }

    public static string Compress(string strData)
    {
      return default(string);
    }

    public static bool CryptIsValid()
    {
      return default(bool);
    }

    public static int CryptPutHost(string strHost)
    {
      return default(int);
    }

    public static int CryptPutSite(string strSite)
    {
      return default(int);
    }

    public static string Decompress(string strData)
    {
      return default(string);
    }

    public static string Decrypt(string strData)
    {
      return default(string);
    }

    public static string Encrypt(string strData)
    {
      return default(string);
    }

    public Object GetCurrentConfig(string strAttribute)
    {
      return default(Object);
    }

    public string GetDomainAttribute(string strAttribute, int iLCID, string strDomain)
    {
      return default(string);
    }

    public string GetDomainFromMemberName(string strMemberName)
    {
      return default(string);
    }

    public bool GetIsAuthenticated(int iTimeWindow, int iForceLogin, int iCheckSecure)
    {
      return default(bool);
    }

    public bool GetIsAuthenticated(int iTimeWindow, bool bForceLogin, bool bCheckSecure)
    {
      return default(bool);
    }

    public string GetLoginChallenge()
    {
      return default(string);
    }

    public string GetLoginChallenge(string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, Object oExtraParams)
    {
      return default(string);
    }

    public string GetLoginChallenge(string strReturnUrl)
    {
      return default(string);
    }

    public Object GetOption(string strOpt)
    {
      return default(Object);
    }

    public Object GetProfileObject(string strProfileName)
    {
      return default(Object);
    }

    public bool HasFlag(int iFlagMask)
    {
      return default(bool);
    }

    public bool HasProfile(string strProfile)
    {
      return default(bool);
    }

    public bool HaveConsent(bool bNeedFullConsent, bool bNeedBirthdate)
    {
      return default(bool);
    }

    public int LoginUser()
    {
      return default(int);
    }

    public int LoginUser(string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, Object oExtraParams)
    {
      return default(int);
    }

    public int LoginUser(string szRetURL, int iTimeWindow, bool fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, bool fUseSecureAuth, Object oExtraParams)
    {
      return default(int);
    }

    public int LoginUser(string strReturnUrl)
    {
      return default(int);
    }

    public string LogoTag()
    {
      return default(string);
    }

    public string LogoTag(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, int iSecure, string strNameSpace, int iKPP, int iUseSecureAuth)
    {
      return default(string);
    }

    public string LogoTag(string strReturnUrl)
    {
      return default(string);
    }

    public string LogoTag(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, bool fSecure, string strNameSpace, int iKPP, bool bUseSecureAuth)
    {
      return default(string);
    }

    public string LogoTag2(string strReturnUrl)
    {
      return default(string);
    }

    public string LogoTag2()
    {
      return default(string);
    }

    public string LogoTag2(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, int iSecure, string strNameSpace, int iKPP, int iUseSecureAuth)
    {
      return default(string);
    }

    public string LogoTag2(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, bool fSecure, string strNameSpace, int iKPP, bool bUseSecureAuth)
    {
      return default(string);
    }

    public string LogoutURL(string szReturnURL, string szCOBrandArgs, int iLangID, string strDomain, int iUseSecureAuth)
    {
      return default(string);
    }

    public string LogoutURL()
    {
      return default(string);
    }

    public PassportIdentity()
    {
    }

    public void SetOption(string strOpt, Object vOpt)
    {
    }

    public static void SignOut(string strSignOutDotGifFileName)
    {
    }

    void System.IDisposable.Dispose()
    {
    }

    public Object Ticket(string strAttribute)
    {
      return default(Object);
    }
    #endregion

    #region Properties and indexers
    public string AuthenticationType
    {
      get
      {
        return default(string);
      }
    }

    public int Error
    {
      get
      {
        return default(int);
      }
    }

    public bool GetFromNetworkServer
    {
      get
      {
        return default(bool);
      }
    }

    public bool HasSavedPassword
    {
      get
      {
        return default(bool);
      }
    }

    public bool HasTicket
    {
      get
      {
        return default(bool);
      }
    }

    public string HexPUID
    {
      get
      {
        return default(string);
      }
    }

    public bool IsAuthenticated
    {
      get
      {
        return default(bool);
      }
    }

    public string this [string strProfileName]
    {
      get
      {
        return default(string);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public int TicketAge
    {
      get
      {
        return default(int);
      }
    }

    public int TimeSinceSignIn
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
