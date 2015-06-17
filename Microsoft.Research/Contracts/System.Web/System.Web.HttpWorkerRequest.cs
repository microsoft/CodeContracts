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
    public delegate void EndOfSendNotification (System.Web.HttpWorkerRequest wr, Object extraData);
    #endregion

    #region Methods and constructors
    public virtual new void CloseConnection ()
    {
    }

    public abstract void EndOfRequest ();

    public abstract void FlushResponse (bool finalFlush);

    public virtual new string GetAppPath ()
    {
      return default(string);
    }

    public virtual new string GetAppPathTranslated ()
    {
      return default(string);
    }

    public virtual new string GetAppPoolID ()
    {
      return default(string);
    }

    public virtual new long GetBytesRead ()
    {
      return default(long);
    }

    public virtual new byte[] GetClientCertificate ()
    {
      return default(byte[]);
    }

    public virtual new byte[] GetClientCertificateBinaryIssuer ()
    {
      return default(byte[]);
    }

    public virtual new int GetClientCertificateEncoding ()
    {
      return default(int);
    }

    public virtual new byte[] GetClientCertificatePublicKey ()
    {
      return default(byte[]);
    }

    public virtual new DateTime GetClientCertificateValidFrom ()
    {
      return default(DateTime);
    }

    public virtual new DateTime GetClientCertificateValidUntil ()
    {
      return default(DateTime);
    }

    public virtual new long GetConnectionID ()
    {
      return default(long);
    }

    public virtual new string GetFilePath ()
    {
      return default(string);
    }

    public virtual new string GetFilePathTranslated ()
    {
      return default(string);
    }

    public abstract string GetHttpVerbName ();

    public abstract string GetHttpVersion ();

    public virtual new string GetKnownRequestHeader (int index)
    {
      return default(string);
    }

    public static int GetKnownRequestHeaderIndex (string header)
    {
      return default(int);
    }

    public static string GetKnownRequestHeaderName (int index)
    {
      return default(string);
    }

    public static int GetKnownResponseHeaderIndex (string header)
    {
      return default(int);
    }

    public static string GetKnownResponseHeaderName (int index)
    {
      return default(string);
    }

    public abstract string GetLocalAddress ();

    public abstract int GetLocalPort ();

    public virtual new string GetPathInfo ()
    {
      return default(string);
    }

    public virtual new byte[] GetPreloadedEntityBody ()
    {
      return default(byte[]);
    }

    public virtual new int GetPreloadedEntityBody (byte[] buffer, int offset)
    {
      return default(int);
    }

    public virtual new int GetPreloadedEntityBodyLength ()
    {
      return default(int);
    }

    public virtual new string GetProtocol ()
    {
      return default(string);
    }

    public abstract string GetQueryString ();

    public virtual new byte[] GetQueryStringRawBytes ()
    {
      return default(byte[]);
    }

    public abstract string GetRawUrl ();

    public abstract string GetRemoteAddress ();

    public virtual new string GetRemoteName ()
    {
      return default(string);
    }

    public abstract int GetRemotePort ();

    public virtual new int GetRequestReason ()
    {
      return default(int);
    }

    public virtual new string GetServerName ()
    {
      return default(string);
    }

    public virtual new string GetServerVariable (string name)
    {
      return default(string);
    }

    public static string GetStatusDescription (int code)
    {
      return default(string);
    }

    public virtual new int GetTotalEntityBodyLength ()
    {
      return default(int);
    }

    public virtual new string GetUnknownRequestHeader (string name)
    {
      return default(string);
    }

    public virtual new string[][] GetUnknownRequestHeaders ()
    {
      return default(string[][]);
    }

    public abstract string GetUriPath ();

    public virtual new long GetUrlContextID ()
    {
      return default(long);
    }

    public virtual new IntPtr GetUserToken ()
    {
      return default(IntPtr);
    }

    public virtual new IntPtr GetVirtualPathToken ()
    {
      return default(IntPtr);
    }

    public bool HasEntityBody ()
    {
      return default(bool);
    }

    public virtual new bool HeadersSent ()
    {
      return default(bool);
    }

    protected HttpWorkerRequest ()
    {
    }

    public virtual new bool IsClientConnected ()
    {
      return default(bool);
    }

    public virtual new bool IsEntireEntityBodyIsPreloaded ()
    {
      return default(bool);
    }

    public virtual new bool IsSecure ()
    {
      return default(bool);
    }

    public virtual new string MapPath (string virtualPath)
    {
      return default(string);
    }

    public virtual new int ReadEntityBody (byte[] buffer, int size)
    {
      return default(int);
    }

    public virtual new int ReadEntityBody (byte[] buffer, int offset, int size)
    {
      return default(int);
    }

    public virtual new void SendCalculatedContentLength (long contentLength)
    {
    }

    public virtual new void SendCalculatedContentLength (int contentLength)
    {
    }

    public abstract void SendKnownResponseHeader (int index, string value);

    public abstract void SendResponseFromFile (IntPtr handle, long offset, long length);

    public abstract void SendResponseFromFile (string filename, long offset, long length);

    public abstract void SendResponseFromMemory (byte[] data, int length);

    public virtual new void SendResponseFromMemory (IntPtr data, int length)
    {
    }

    public abstract void SendStatus (int statusCode, string statusDescription);

    public abstract void SendUnknownResponseHeader (string name, string value);

    public virtual new void SetEndOfSendNotification (System.Web.HttpWorkerRequest.EndOfSendNotification callback, Object extraData)
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
    public const int HeaderAccept = 20;
    public const int HeaderAcceptCharset = 21;
    public const int HeaderAcceptEncoding = 22;
    public const int HeaderAcceptLanguage = 23;
    public const int HeaderAcceptRanges = 20;
    public const int HeaderAge = 21;
    public const int HeaderAllow = 10;
    public const int HeaderAuthorization = 24;
    public const int HeaderCacheControl = 0;
    public const int HeaderConnection = 1;
    public const int HeaderContentEncoding = 13;
    public const int HeaderContentLanguage = 14;
    public const int HeaderContentLength = 11;
    public const int HeaderContentLocation = 15;
    public const int HeaderContentMd5 = 16;
    public const int HeaderContentRange = 17;
    public const int HeaderContentType = 12;
    public const int HeaderCookie = 25;
    public const int HeaderDate = 2;
    public const int HeaderEtag = 22;
    public const int HeaderExpect = 26;
    public const int HeaderExpires = 18;
    public const int HeaderFrom = 27;
    public const int HeaderHost = 28;
    public const int HeaderIfMatch = 29;
    public const int HeaderIfModifiedSince = 30;
    public const int HeaderIfNoneMatch = 31;
    public const int HeaderIfRange = 32;
    public const int HeaderIfUnmodifiedSince = 33;
    public const int HeaderKeepAlive = 3;
    public const int HeaderLastModified = 19;
    public const int HeaderLocation = 23;
    public const int HeaderMaxForwards = 34;
    public const int HeaderPragma = 4;
    public const int HeaderProxyAuthenticate = 24;
    public const int HeaderProxyAuthorization = 35;
    public const int HeaderRange = 37;
    public const int HeaderReferer = 36;
    public const int HeaderRetryAfter = 25;
    public const int HeaderServer = 26;
    public const int HeaderSetCookie = 27;
    public const int HeaderTe = 38;
    public const  int HeaderTrailer = 5;
    public const int HeaderTransferEncoding = 6;
    public const int HeaderUpgrade = 7;
    public const int HeaderUserAgent = 39;
    public const int HeaderVary = 28;
    public const int HeaderVia = 8;
    public const int HeaderWarning = 9;
    public const int HeaderWwwAuthenticate = 29;
    public const int ReasonCachePolicy = 2;
    public const int ReasonCacheSecurity = 3;
    public const int ReasonClientDisconnect = 4;
    public const int ReasonDefault = 0;
    public const int ReasonFileHandleCacheMiss = 1;
    public const int ReasonResponseCacheMiss = 0;
    public const int RequestHeaderMaximum = 40;
    public const int ResponseHeaderMaximum = 30;
    #endregion
  }
}
