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

using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Web
{
  // Summary:
  //     Enables ASP.NET to read the HTTP values sent by a client during a Web request.
  public sealed class HttpRequest
  {
    // Summary:
    //     Initializes an System.Web.HttpRequest object.
    //
    // Parameters:
    //   filename:
    //     The name of the file associated with the request.
    //
    //   url:
    //     Information regarding the URL of the current request.
    //
    //   queryString:
    //     The entire query string sent with the request (everything after the'?').
    public HttpRequest(string filename, string url, string queryString)
    {
      Contract.Requires(url != null);
    }

    // Summary:
    //     Gets a string array of client-supported MIME accept types.
    //
    // Returns:
    //     A string array of client-supported MIME accept types.
    extern public string[] AcceptTypes { get; }
    //
    // Summary:
    //     Gets the anonymous identifier for the user, if present.
    //
    // Returns:
    //     A string representing the current anonymous user identifier.
    extern public string AnonymousID { get; }
    //
    // Summary:
    //     Gets the ASP.NET application's virtual application root path on the server.
    //
    // Returns:
    //     The virtual path of the current application.
    extern public string ApplicationPath { get; }
    //
    // Summary:
    //     Gets the virtual path of the application root and makes it relative by using
    //     the tilde (~) notation for the application root (as in "~/page.aspx").
    //
    // Returns:
    //     The virtual path of the application root for the current request.
    extern public string AppRelativeCurrentExecutionFilePath { get; }
    //
    // Summary:
    //     Gets or sets information about the requesting client's browser capabilities.
    //
    // Returns:
    //     An System.Web.HttpBrowserCapabilities object listing the capabilities of
    //     the client's browser.
    extern public HttpBrowserCapabilities Browser { get; set; }
    //
    // Summary:
    //     Gets the current request's client security certificate.
    //
    // Returns:
    //     An System.Web.HttpClientCertificate object containing information about the
    //     client's security certificate settings.
    extern public HttpClientCertificate ClientCertificate { get; }
    //
    // Summary:
    //     Gets or sets the character set of the entity-body.
    //
    // Returns:
    //     An System.Text.Encoding object representing the client's character set.
    public Encoding ContentEncoding
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
      set
      {
      }
    }
    //
    // Summary:
    //     Specifies the length, in bytes, of content sent by the client.
    //
    // Returns:
    //     The length, in bytes, of content sent by the client.
    public int ContentLength
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }
    //
    // Summary:
    //     Gets or sets the MIME content type of the incoming request.
    //
    // Returns:
    //     A string representing the MIME content type of the incoming request, for
    //     example, "text/html".
    public string ContentType
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set { }
    }
    //
    // Summary:
    //     Gets a collection of cookies sent by the client.
    //
    // Returns:
    //     An System.Web.HttpCookieCollection object representing the client's cookie
    //     variables.
    public HttpCookieCollection Cookies
    {
      get
      {
        Contract.Ensures(Contract.Result<HttpCookieCollection>() != null);
        return default(HttpCookieCollection);
      }
    }

    //
    // Summary:
    //     Gets the virtual path of the current request.
    //
    // Returns:
    //     The virtual path of the current request.
    extern public string CurrentExecutionFilePath { get; }
    //
    // Summary:
    //     Gets the virtual path of the current request.
    //
    // Returns:
    //     The virtual path of the current request.
    extern public string FilePath { get; }
    //
    // Summary:
    //     Gets the collection of files uploaded by the client, in multipart MIME format.
    //
    // Returns:
    //     An System.Web.HttpFileCollection object representing a collection of files
    //     uploaded by the client. The items of the System.Web.HttpFileCollection object
    //     are of type System.Web.HttpPostedFile.
    public HttpFileCollection Files
    {
      get
      {
        Contract.Ensures(Contract.Result<HttpFileCollection>() != null);
        return default(HttpFileCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets the filter to use when reading the current input stream.
    //
    // Returns:
    //     A System.IO.Stream object to be used as the filter.
    //
    // Exceptions:
    //   System.Web.HttpException:
    //     The specified System.IO.Stream is invalid.
    public Stream Filter
    {
      get
      {
        Contract.Ensures(Contract.Result<Stream>() != null);
        return default(Stream);
      }
      set { }
    }
    //
    // Summary:
    //     Gets a collection of form variables.
    //
    // Returns:
    //     A System.Collections.Specialized.NameValueCollection representing a collection
    //     of form variables.
    public NameValueCollection Form
    {
      get
      {
        Contract.Ensures(Contract.Result<NameValueCollection>() != null);
        return default(NameValueCollection);
      }
    }
    //
    // Summary:
    //     Gets a collection of HTTP headers.
    //
    // Returns:
    //     A System.Collections.Specialized.NameValueCollection of headers.
    public NameValueCollection Headers
    {
      get
      {
        Contract.Ensures(Contract.Result<NameValueCollection>() != null);
        return default(NameValueCollection);
      }
    }
    //
    // Summary:
    //     Gets the HTTP data transfer method (such as GET, POST, or HEAD) used by the
    //     client.
    //
    // Returns:
    //     The HTTP data transfer method used by the client.
    public string HttpMethod
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    //
    // Summary:
    //     Gets the contents of the incoming HTTP entity body.
    //
    // Returns:
    //     A System.IO.Stream object representing the contents of the incoming HTTP
    //     content body.
    public Stream InputStream
    {
      get
      {
        Contract.Ensures(Contract.Result<Stream>() != null);
        return default(Stream);
      }
    }
    //
    // Summary:
    //     Gets a value indicating whether the request has been authenticated.
    //
    // Returns:
    //     true if the request is authenticated; otherwise, false.
    extern public bool IsAuthenticated { get; }
    //
    // Summary:
    //     Gets a value indicating whether the request is from the local computer.
    //
    // Returns:
    //     true if the request is from the local computer; otherwise, false.
    extern public bool IsLocal { get; }
    //
    // Summary:
    //     Gets a value indicting whether the HTTP connection uses secure sockets (that
    //     is, HTTPS).
    //
    // Returns:
    //     true if the connection is an SSL connection; otherwise, false.
    extern public bool IsSecureConnection { get; }
    //
    // Summary:
    //     Gets the System.Security.Principal.WindowsIdentity type for the current user.
    //
    // Returns:
    //     A System.Security.Principal.WindowsIdentity for the current Microsoft Internet
    //     Information Services (IIS) authentication settings.
    extern public WindowsIdentity LogonUserIdentity { get; }
    //
    // Summary:
    //     Gets a combined collection of System.Web.HttpRequest.QueryString, System.Web.HttpRequest.Form,
    //     System.Web.HttpRequest.ServerVariables, and System.Web.HttpRequest.Cookies
    //     items.
    //
    // Returns:
    //     A System.Collections.Specialized.NameValueCollection object.
    public NameValueCollection Params
    {
      get
      {
        Contract.Ensures(Contract.Result<NameValueCollection>() != null);
        return default(NameValueCollection);
      }
    }
    //
    // Summary:
    //     Gets the virtual path of the current request.
    //
    // Returns:
    //     The virtual path of the current request.
    public string Path
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(String);
      }
    }
    //
    // Summary:
    //     Gets additional path information for a resource with a URL extension.
    //
    // Returns:
    //     Additional path information for a resource.
    public string PathInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(String);
      }
    }
    //
    // Summary:
    //     Gets the physical file system path of the currently executing server application's
    //     root directory.
    //
    // Returns:
    //     The file system path of the current application's root directory.
    extern public string PhysicalApplicationPath { get; }
    //
    // Summary:
    //     Gets the physical file system path corresponding to the requested URL.
    //
    // Returns:
    //     The file system path of the current request.
    extern public string PhysicalPath { get; }
    //
    // Summary:
    //     Gets the collection of HTTP query string variables.
    //
    // Returns:
    //     A System.Collections.Specialized.NameValueCollection containing the collection
    //     of query string variables sent by the client. For example, If the request
    //     URL is http://www.contoso.com/default.aspx?id=44 then the value of System.Web.HttpRequest.QueryString
    //     is "id=44".
    public NameValueCollection QueryString
    {
      get
      {
        Contract.Ensures(Contract.Result<NameValueCollection>() != null);
        return default(NameValueCollection);
      }
    }
    //
    // Summary:
    //     Gets the raw URL of the current request.
    //
    // Returns:
    //     The raw URL of the current request.
    public string RawUrl
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
    //
    // Summary:
    //     Gets or sets the HTTP data transfer method (GET or POST) used by the client.
    //
    // Returns:
    //     A string representing the HTTP invocation type sent by the client.
    public string RequestType
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set { }
    }
    //
    // Summary:
    //     Gets a collection of Web server variables.
    //
    // Returns:
    //     A System.Collections.Specialized.NameValueCollection of server variables.
    public NameValueCollection ServerVariables
    {
      get
      {
        Contract.Ensures(Contract.Result<NameValueCollection>() != null);
        return default(NameValueCollection);
      }
    }
    //
    // Summary:
    //     Gets the number of bytes in the current input stream.
    //
    // Returns:
    //     The number of bytes in the input stream.
    public int TotalBytes
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }
    //
    // Summary:
    //     Gets information about the URL of the current request.
    //
    // Returns:
    //     A System.Uri object containing information regarding the URL of the current
    //     request.
    public Uri Url
    {
      get
      {
        Contract.Ensures(Contract.Result<Uri>() != null);

        return default(Uri);
      }
    }
    //
    // Summary:
    //     Gets information about the URL of the client's previous request that linked
    //     to the current URL.
    //
    // Returns:
    //     A System.Uri object.
    extern public Uri UrlReferrer { get; }
    //
    // Summary:
    //     Gets the raw user agent string of the client browser.
    //
    // Returns:
    //     The raw user agent string of the client browser.
    extern public string UserAgent { get; }
    //
    // Summary:
    //     Gets the IP host address of the remote client.
    //
    // Returns:
    //     The IP address of the remote client.
    extern public string UserHostAddress { get; }
    //
    // Summary:
    //     Gets the DNS name of the remote client.
    //
    // Returns:
    //     The DNS name of the remote client.
    extern public string UserHostName { get; }
    //
    // Summary:
    //     Gets a sorted string array of client language preferences.
    //
    // Returns:
    //     A sorted string array of client language preferences, or null if empty.
    extern public string[] UserLanguages { get; }

    // Summary:
    //     Gets the specified object from the System.Web.HttpRequest.Cookies, System.Web.HttpRequest.Form,
    //     System.Web.HttpRequest.QueryString or System.Web.HttpRequest.ServerVariables
    //     collections.
    //
    // Parameters:
    //   key:
    //     The name of the collection member to get.
    //
    // Returns:
    //     The System.Web.HttpRequest.QueryString, System.Web.HttpRequest.Form, System.Web.HttpRequest.Cookies,
    //     or System.Web.HttpRequest.ServerVariables collection member specified in
    //     the key parameter. If the specified key is not found, then null is returned.
    extern public string this[string key] { get; }

    // Summary:
    //     Performs a binary read of a specified number of bytes from the current input
    //     stream.
    //
    // Parameters:
    //   count:
    //     The number of bytes to read.
    //
    // Returns:
    //     A byte array.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     count is 0.  - or - count is greater than the number of bytes available.
    public byte[] BinaryRead(int count)
    {
      Contract.Requires(0 <= count);
      Contract.Requires(count <= this.TotalBytes);
      Contract.Ensures(Contract.Result<byte[]>() != null);
      return default(byte[]);
    }
    //
    // Summary:
    //     Maps an incoming image-field form parameter to appropriate x-coordinate and
    //     y-coordinate values.
    //
    // Parameters:
    //   imageFieldName:
    //     The name of the form image map.
    //
    // Returns:
    //     A two-dimensional array of integers.
    extern public int[] MapImageCoordinates(string imageFieldName);
    //
    // Summary:
    //     Maps the specified virtual path to a physical path.
    //
    // Parameters:
    //   virtualPath:
    //     The virtual path (absolute or relative) for the current request.
    //
    // Returns:
    //     The physical path on the server specified by virtualPath.
    //
    // Exceptions:
    //   System.Web.HttpException:
    //     No System.Web.HttpContext object is defined for the request.
    extern public string MapPath(string virtualPath);
    //
    // Summary:
    //     Maps the specified virtual path to a physical path.
    //
    // Parameters:
    //   virtualPath:
    //     The virtual path (absolute or relative) for the current request.
    //
    //   baseVirtualDir:
    //     The virtual base directory path used for relative resolution.
    //
    //   allowCrossAppMapping:
    //     true to indicate that virtualPath may belong to another application; otherwise,
    //     false.
    //
    // Returns:
    //     The physical path on the server.
    //
    // Exceptions:
    //   System.Web.HttpException:
    //     allowCrossMapping is false and virtualPath belongs to another application.
    //
    //   System.Web.HttpException:
    //     No System.Web.HttpContext object is defined for the request.
    extern public string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping);
    //
    // Summary:
    //     Saves an HTTP request to disk.
    //
    // Parameters:
    //   filename:
    //     The physical drive path.
    //
    //   includeHeaders:
    //     A Boolean value specifying whether an HTTP header should be saved to disk.
    //
    // Exceptions:
    //   System.Web.HttpException:
    //     The System.Web.Configuration.HttpRuntimeSection.RequireRootedSaveAsPath property
    //     of the System.Web.Configuration.HttpRuntimeSection is set to true but filename
    //     is not an absolute path.
    public void SaveAs(string filename, bool includeHeaders)
    {
      Contract.Requires(filename != null);
    }
    //
    // Summary:
    //     Causes validation to occur for the collections accessed through the System.Web.HttpRequest.Cookies,
    //     System.Web.HttpRequest.Form, and System.Web.HttpRequest.QueryString properties.
    //
    // Exceptions:
    //   System.Web.HttpRequestValidationException:
    //     Potentially dangerous data was received from the client.
    [Pure]
    extern public void ValidateInput();
  }
}
