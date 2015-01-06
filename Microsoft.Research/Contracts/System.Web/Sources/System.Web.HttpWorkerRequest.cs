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

// File System.Web.HttpWorkerRequest.cs
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


namespace System.Web
{
  abstract public partial class HttpWorkerRequest
  {
    #region Delegates
    public delegate void EndOfSendNotification(System.Web.HttpWorkerRequest wr, Object extraData);
    #endregion

    #region Methods and constructors
    public virtual new void CloseConnection()
    {
    }

    public abstract void EndOfRequest();

    public abstract void FlushResponse(bool finalFlush);

    public virtual new string GetAppPath()
    {
      return default(string);
    }

    public virtual new string GetAppPathTranslated()
    {
      return default(string);
    }

    public virtual new string GetAppPoolID()
    {
      return default(string);
    }

    public virtual new long GetBytesRead()
    {
      return default(long);
    }

    public virtual new byte[] GetClientCertificate()
    {
      return default(byte[]);
    }

    public virtual new byte[] GetClientCertificateBinaryIssuer()
    {
      return default(byte[]);
    }

    public virtual new int GetClientCertificateEncoding()
    {
      return default(int);
    }

    public virtual new byte[] GetClientCertificatePublicKey()
    {
      return default(byte[]);
    }

    public virtual new DateTime GetClientCertificateValidFrom()
    {
      return default(DateTime);
    }

    public virtual new DateTime GetClientCertificateValidUntil()
    {
      return default(DateTime);
    }

    public virtual new long GetConnectionID()
    {
      return default(long);
    }

    public virtual new string GetFilePath()
    {
      return default(string);
    }

    public virtual new string GetFilePathTranslated()
    {
      return default(string);
    }

    public abstract string GetHttpVerbName();

    public abstract string GetHttpVersion();

    public virtual new string GetKnownRequestHeader(int index)
    {
      return default(string);
    }

    public static int GetKnownRequestHeaderIndex(string header)
    {
      return default(int);
    }

    public static string GetKnownRequestHeaderName(int index)
    {
      return default(string);
    }

    public static int GetKnownResponseHeaderIndex(string header)
    {
      return default(int);
    }

    public static string GetKnownResponseHeaderName(int index)
    {
      return default(string);
    }

    public abstract string GetLocalAddress();

    public abstract int GetLocalPort();

    public virtual new string GetPathInfo()
    {
      return default(string);
    }

    public virtual new byte[] GetPreloadedEntityBody()
    {
      return default(byte[]);
    }

    public virtual new int GetPreloadedEntityBody(byte[] buffer, int offset)
    {
      return default(int);
    }

    public virtual new int GetPreloadedEntityBodyLength()
    {
      return default(int);
    }

    public virtual new string GetProtocol()
    {
      return default(string);
    }

    public abstract string GetQueryString();

    public virtual new byte[] GetQueryStringRawBytes()
    {
      return default(byte[]);
    }

    public abstract string GetRawUrl();

    public abstract string GetRemoteAddress();

    public virtual new string GetRemoteName()
    {
      return default(string);
    }

    public abstract int GetRemotePort();

    public virtual new int GetRequestReason()
    {
      return default(int);
    }

    public virtual new string GetServerName()
    {
      return default(string);
    }

    public virtual new string GetServerVariable(string name)
    {
      return default(string);
    }

    public static string GetStatusDescription(int code)
    {
      return default(string);
    }

    public virtual new int GetTotalEntityBodyLength()
    {
      return default(int);
    }

    public virtual new string GetUnknownRequestHeader(string name)
    {
      return default(string);
    }

    public virtual new string[][] GetUnknownRequestHeaders()
    {
      return default(string[][]);
    }

    public abstract string GetUriPath();

    public virtual new long GetUrlContextID()
    {
      return default(long);
    }

    public virtual new IntPtr GetUserToken()
    {
      return default(IntPtr);
    }

    public virtual new IntPtr GetVirtualPathToken()
    {
      return default(IntPtr);
    }

    public bool HasEntityBody()
    {
      return default(bool);
    }

    public virtual new bool HeadersSent()
    {
      return default(bool);
    }

    protected HttpWorkerRequest()
    {
    }

    public virtual new bool IsClientConnected()
    {
      return default(bool);
    }

    public virtual new bool IsEntireEntityBodyIsPreloaded()
    {
      return default(bool);
    }

    public virtual new bool IsSecure()
    {
      return default(bool);
    }

    public virtual new string MapPath(string virtualPath)
    {
      return default(string);
    }

    public virtual new int ReadEntityBody(byte[] buffer, int size)
    {
      return default(int);
    }

    public virtual new int ReadEntityBody(byte[] buffer, int offset, int size)
    {
      return default(int);
    }

    public virtual new void SendCalculatedContentLength(long contentLength)
    {
    }

    public virtual new void SendCalculatedContentLength(int contentLength)
    {
    }

    public abstract void SendKnownResponseHeader(int index, string value);

    public abstract void SendResponseFromFile(IntPtr handle, long offset, long length);

    public abstract void SendResponseFromFile(string filename, long offset, long length);

    public abstract void SendResponseFromMemory(byte[] data, int length);

    public virtual new void SendResponseFromMemory(IntPtr data, int length)
    {
    }

    public abstract void SendStatus(int statusCode, string statusDescription);

    public abstract void SendUnknownResponseHeader(string name, string value);

    public virtual new void SetEndOfSendNotification(System.Web.HttpWorkerRequest.EndOfSendNotification callback, Object extraData)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new string MachineConfigPath
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string MachineInstallDirectory
    {
      get
      {
        return default(string);
      }
    }

    public virtual new Guid RequestTraceIdentifier
    {
      get
      {
        return default(Guid);
      }
    }

    public virtual new string RootWebConfigPath
    {
      get
      {
        return default(string);
      }
    }
    #endregion

    #region Fields
    public static int HeaderAccept;
    public static int HeaderAcceptCharset;
    public static int HeaderAcceptEncoding;
    public static int HeaderAcceptLanguage;
    public static int HeaderAcceptRanges;
    public static int HeaderAge;
    public static int HeaderAllow;
    public static int HeaderAuthorization;
    public static int HeaderCacheControl;
    public static int HeaderConnection;
    public static int HeaderContentEncoding;
    public static int HeaderContentLanguage;
    public static int HeaderContentLength;
    public static int HeaderContentLocation;
    public static int HeaderContentMd5;
    public static int HeaderContentRange;
    public static int HeaderContentType;
    public static int HeaderCookie;
    public static int HeaderDate;
    public static int HeaderEtag;
    public static int HeaderExpect;
    public static int HeaderExpires;
    public static int HeaderFrom;
    public static int HeaderHost;
    public static int HeaderIfMatch;
    public static int HeaderIfModifiedSince;
    public static int HeaderIfNoneMatch;
    public static int HeaderIfRange;
    public static int HeaderIfUnmodifiedSince;
    public static int HeaderKeepAlive;
    public static int HeaderLastModified;
    public static int HeaderLocation;
    public static int HeaderMaxForwards;
    public static int HeaderPragma;
    public static int HeaderProxyAuthenticate;
    public static int HeaderProxyAuthorization;
    public static int HeaderRange;
    public static int HeaderReferer;
    public static int HeaderRetryAfter;
    public static int HeaderServer;
    public static int HeaderSetCookie;
    public static int HeaderTe;
    public static int HeaderTrailer;
    public static int HeaderTransferEncoding;
    public static int HeaderUpgrade;
    public static int HeaderUserAgent;
    public static int HeaderVary;
    public static int HeaderVia;
    public static int HeaderWarning;
    public static int HeaderWwwAuthenticate;
    public static int ReasonCachePolicy;
    public static int ReasonCacheSecurity;
    public static int ReasonClientDisconnect;
    public static int ReasonDefault;
    public static int ReasonFileHandleCacheMiss;
    public static int ReasonResponseCacheMiss;
    public static int RequestHeaderMaximum;
    public static int ResponseHeaderMaximum;
    #endregion
  }
}
