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

// File System.Net.cs
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


namespace System.Net
{
  public enum AuthenticationSchemes
  {
    None = 0, 
    Digest = 1, 
    Negotiate = 2, 
    Ntlm = 4, 
    Basic = 8, 
    Anonymous = 32768, 
    IntegratedWindowsAuthentication = 6, 
  }

  public delegate AuthenticationSchemes AuthenticationSchemeSelector(HttpListenerRequest httpRequest);

  public delegate IPEndPoint BindIPEndPoint(ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount);

  public enum DecompressionMethods
  {
    None = 0, 
    GZip = 1, 
    Deflate = 2, 
  }

  public delegate void DownloadDataCompletedEventHandler(Object sender, DownloadDataCompletedEventArgs e);

  public delegate void DownloadProgressChangedEventHandler(Object sender, DownloadProgressChangedEventArgs e);

  public delegate void DownloadStringCompletedEventHandler(Object sender, DownloadStringCompletedEventArgs e);

  public enum FtpStatusCode
  {
    Undefined = 0, 
    RestartMarker = 110, 
    ServiceTemporarilyNotAvailable = 120, 
    DataAlreadyOpen = 125, 
    OpeningData = 150, 
    CommandOK = 200, 
    CommandExtraneous = 202, 
    DirectoryStatus = 212, 
    FileStatus = 213, 
    SystemType = 215, 
    SendUserCommand = 220, 
    ClosingControl = 221, 
    ClosingData = 226, 
    EnteringPassive = 227, 
    LoggedInProceed = 230, 
    ServerWantsSecureSession = 234, 
    FileActionOK = 250, 
    PathnameCreated = 257, 
    SendPasswordCommand = 331, 
    NeedLoginAccount = 332, 
    FileCommandPending = 350, 
    ServiceNotAvailable = 421, 
    CantOpenData = 425, 
    ConnectionClosed = 426, 
    ActionNotTakenFileUnavailableOrBusy = 450, 
    ActionAbortedLocalProcessingError = 451, 
    ActionNotTakenInsufficientSpace = 452, 
    CommandSyntaxError = 500, 
    ArgumentSyntaxError = 501, 
    CommandNotImplemented = 502, 
    BadCommandSequence = 503, 
    NotLoggedIn = 530, 
    AccountNeeded = 532, 
    ActionNotTakenFileUnavailable = 550, 
    ActionAbortedUnknownPageType = 551, 
    FileActionAborted = 552, 
    ActionNotTakenFilenameNotAllowed = 553, 
  }

  public delegate void HttpContinueDelegate(int StatusCode, WebHeaderCollection httpHeaders);

  public enum HttpRequestHeader
  {
    CacheControl = 0, 
    Connection = 1, 
    Date = 2, 
    KeepAlive = 3, 
    Pragma = 4, 
    Trailer = 5, 
    TransferEncoding = 6, 
    Upgrade = 7, 
    Via = 8, 
    Warning = 9, 
    Allow = 10, 
    ContentLength = 11, 
    ContentType = 12, 
    ContentEncoding = 13, 
    ContentLanguage = 14, 
    ContentLocation = 15, 
    ContentMd5 = 16, 
    ContentRange = 17, 
    Expires = 18, 
    LastModified = 19, 
    Accept = 20, 
    AcceptCharset = 21, 
    AcceptEncoding = 22, 
    AcceptLanguage = 23, 
    Authorization = 24, 
    Cookie = 25, 
    Expect = 26, 
    From = 27, 
    Host = 28, 
    IfMatch = 29, 
    IfModifiedSince = 30, 
    IfNoneMatch = 31, 
    IfRange = 32, 
    IfUnmodifiedSince = 33, 
    MaxForwards = 34, 
    ProxyAuthorization = 35, 
    Referer = 36, 
    Range = 37, 
    Te = 38, 
    Translate = 39, 
    UserAgent = 40, 
  }

  public enum HttpResponseHeader
  {
    CacheControl = 0, 
    Connection = 1, 
    Date = 2, 
    KeepAlive = 3, 
    Pragma = 4, 
    Trailer = 5, 
    TransferEncoding = 6, 
    Upgrade = 7, 
    Via = 8, 
    Warning = 9, 
    Allow = 10, 
    ContentLength = 11, 
    ContentType = 12, 
    ContentEncoding = 13, 
    ContentLanguage = 14, 
    ContentLocation = 15, 
    ContentMd5 = 16, 
    ContentRange = 17, 
    Expires = 18, 
    LastModified = 19, 
    AcceptRanges = 20, 
    Age = 21, 
    ETag = 22, 
    Location = 23, 
    ProxyAuthenticate = 24, 
    RetryAfter = 25, 
    Server = 26, 
    SetCookie = 27, 
    Vary = 28, 
    WwwAuthenticate = 29, 
  }

  public enum HttpStatusCode
  {
    Continue = 100, 
    SwitchingProtocols = 101, 
    OK = 200, 
    Created = 201, 
    Accepted = 202, 
    NonAuthoritativeInformation = 203, 
    NoContent = 204, 
    ResetContent = 205, 
    PartialContent = 206, 
    MultipleChoices = 300, 
    Ambiguous = 300, 
    MovedPermanently = 301, 
    Moved = 301, 
    Found = 302, 
    Redirect = 302, 
    SeeOther = 303, 
    RedirectMethod = 303, 
    NotModified = 304, 
    UseProxy = 305, 
    Unused = 306, 
    TemporaryRedirect = 307, 
    RedirectKeepVerb = 307, 
    BadRequest = 400, 
    Unauthorized = 401, 
    PaymentRequired = 402, 
    Forbidden = 403, 
    NotFound = 404, 
    MethodNotAllowed = 405, 
    NotAcceptable = 406, 
    ProxyAuthenticationRequired = 407, 
    RequestTimeout = 408, 
    Conflict = 409, 
    Gone = 410, 
    LengthRequired = 411, 
    PreconditionFailed = 412, 
    RequestEntityTooLarge = 413, 
    RequestUriTooLong = 414, 
    UnsupportedMediaType = 415, 
    RequestedRangeNotSatisfiable = 416, 
    ExpectationFailed = 417, 
    InternalServerError = 500, 
    NotImplemented = 501, 
    BadGateway = 502, 
    ServiceUnavailable = 503, 
    GatewayTimeout = 504, 
    HttpVersionNotSupported = 505, 
  }

  public enum NetworkAccess
  {
    Accept = 128, 
    Connect = 64, 
  }

  public delegate void OpenReadCompletedEventHandler(Object sender, OpenReadCompletedEventArgs e);

  public delegate void OpenWriteCompletedEventHandler(Object sender, OpenWriteCompletedEventArgs e);

  public enum SecurityProtocolType
  {
    Ssl3 = 48, 
    Tls = 192, 
  }

  public enum TransportType
  {
    Udp = 1, 
    Connectionless = 1, 
    Tcp = 2, 
    ConnectionOriented = 2, 
    All = 3, 
  }

  public delegate void UploadDataCompletedEventHandler(Object sender, UploadDataCompletedEventArgs e);

  public delegate void UploadFileCompletedEventHandler(Object sender, UploadFileCompletedEventArgs e);

  public delegate void UploadProgressChangedEventHandler(Object sender, UploadProgressChangedEventArgs e);

  public delegate void UploadStringCompletedEventHandler(Object sender, UploadStringCompletedEventArgs e);

  public delegate void UploadValuesCompletedEventHandler(Object sender, UploadValuesCompletedEventArgs e);

  public enum WebExceptionStatus
  {
    Success = 0, 
    NameResolutionFailure = 1, 
    ConnectFailure = 2, 
    ReceiveFailure = 3, 
    SendFailure = 4, 
    PipelineFailure = 5, 
    RequestCanceled = 6, 
    ProtocolError = 7, 
    ConnectionClosed = 8, 
    TrustFailure = 9, 
    SecureChannelFailure = 10, 
    ServerProtocolViolation = 11, 
    KeepAliveFailure = 12, 
    Pending = 13, 
    Timeout = 14, 
    ProxyNameResolutionFailure = 15, 
    UnknownError = 16, 
    MessageLengthLimitExceeded = 17, 
    CacheEntryNotFound = 18, 
    RequestProhibitedByCachePolicy = 19, 
    RequestProhibitedByProxy = 20, 
  }
}
