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

#if !SILVERLIGHT
using System;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Diagnostics.Contracts;

namespace System.Net
{
    // Summary:
    //     Makes a request to a Uniform Resource Identifier (URI). This is an abstract
    //     class.
    public abstract class WebRequest //: MarshalByRefObject, ISerializable
    {
        // Summary:
        //     Initializes a new instance of the System.Net.WebRequest class.
        protected WebRequest() { }
        //
        // Summary:
        //     Initializes a new instance of the System.Net.WebRequest class from the specified
        //     instances of the System.Runtime.Serialization.SerializationInfo and System.Runtime.Serialization.StreamingContext
        //     classes.
        //
        // Parameters:
        //   serializationInfo:
        //     A System.Runtime.Serialization.SerializationInfo that contains the information
        //     required to serialize the new System.Net.WebRequest instance.
        //
        //   streamingContext:
        //     A System.Runtime.Serialization.StreamingContext that indicates the source
        //     of the serialized stream associated with the new System.Net.WebRequest instance.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to access the constructor, when the constructor is not
        //     overridden in a descendant class.
        //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        //protected WebRequest(SerializationInfo serializationInfo, StreamingContext streamingContext) { }

        // Summary:
        //     Gets or sets values indicating the level of authentication and impersonation
        //     used for this request.
        //
        // Returns:
        //     A bitwise combination of the System.Net.Security.AuthenticationLevel values.
        //     The default value is System.Net.Security.AuthenticationLevel.MutualAuthRequested.In
        //     mutual authentication, both the client and server present credentials to
        //     establish their identity. The System.Net.Security.AuthenticationLevel.MutualAuthRequired
        //     and System.Net.Security.AuthenticationLevel.MutualAuthRequested values are
        //     relevant for Kerberos authentication. Kerberos authentication can be supported
        //     directly, or can be used if the Negotiate security protocol is used to select
        //     the actual security protocol. For more information about authentication protocols,
        //     see Internet Authentication.To determine whether mutual authentication occurred,
        //     check the System.Net.WebResponse.IsMutuallyAuthenticated property. If you
        //     specify the System.Net.Security.AuthenticationLevel.MutualAuthRequired authentication
        //     flag value and mutual authentication does not occur, your application will
        //     receive an System.IO.IOException with a System.Net.ProtocolViolationException
        //     inner exception indicating that mutual authentication failed.
        //public AuthenticationLevel AuthenticationLevel { get; set; }
        //
        // Summary:
        //     Gets or sets the cache policy for this request.
        //
        // Returns:
        //     A System.Net.Cache.RequestCachePolicy object that defines a cache policy.
        //public virtual RequestCachePolicy CachePolicy { get; set; }

        //
        // Summary:
        //     When overridden in a descendant class, gets or sets the name of the connection
        //     group for the request.
        //
        // Returns:
        //     The name of the connection group for the request.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to get or set the property, when the property is not
        //     overridden in a descendant class.
        //public virtual string ConnectionGroupName { get; set; }
        //
        // Summary:
        //     When overridden in a descendant class, gets or sets the content length of
        //     the request data being sent.
        //
        // Returns:
        //     The number of bytes of request data being sent.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to get or set the property, when the property is not
        //     overridden in a descendant class.
        //public virtual long ContentLength { get; set; }

        //
        // Summary:
        //     When overridden in a descendant class, gets or sets the content type of the
        //     request data being sent.
        //
        // Returns:
        //     The content type of the request data.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to get or set the property, when the property is not
        //     overridden in a descendant class.
        //public virtual string ContentType { get; set; }

        //
        // Summary:
        //     When overridden in a descendant class, gets or sets the network credentials
        //     used for authenticating the request with the Internet resource.
        //
        // Returns:
        //     An System.Net.ICredentials containing the authentication credentials associated
        //     with the request. The default is null.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to get or set the property, when the property is not
        //     overridden in a descendant class.
        //public virtual ICredentials Credentials { get; set; }

        //
        // Summary:
        //     Gets or sets the default cache policy for this request.
        //
        // Returns:
        //     A System.Net.Cache.HttpRequestCachePolicy that specifies the cache policy
        //     in effect for this request when no other policy is applicable.
        //public static RequestCachePolicy DefaultCachePolicy { get; set; }

        //
        // Summary:
        //     Gets or sets the global HTTP proxy.
        //
        // Returns:
        //     An System.Net.IWebProxy used by every call to instances of System.Net.WebRequest.
        public static IWebProxy DefaultWebProxy
        {
            get
            {
                return default(IWebProxy);
            }
            set
            {
            }
        }

        //
        // Summary:
        //     When overridden in a descendant class, gets or sets the collection of header
        //     name/value pairs associated with the request.
        //
        // Returns:
        //     A System.Net.WebHeaderCollection containing the header name/value pairs associated
        //     with this request.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to get or set the property, when the property is not
        //     overridden in a descendant class.
        public virtual WebHeaderCollection Headers
        {
            get
            {
                Contract.Ensures(Contract.Result<WebHeaderCollection>() != null);
                return null;
            }
            set { }
        }

        //
        // Summary:
        //     Gets or sets the impersonation level for the current request.
        //
        // Returns:
        //     A System.Security.Principal.TokenImpersonationLevel value.
        //extern public TokenImpersonationLevel ImpersonationLevel { get; set; }

        //
        // Summary:
        //     When overridden in a descendant class, gets or sets the protocol method to
        //     use in this request.
        //
        // Returns:
        //     The protocol method to use in this request.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     If the property is not overridden in a descendant class, any attempt is made
        //     to get or set the property.
        public virtual string Method
        {
            get
            {
                Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));
                return null;
            }
            set
            {
                Contract.Requires(!String.IsNullOrEmpty(value));
            }
        }

        //
        // Summary:
        //     When overridden in a descendant class, indicates whether to pre-authenticate
        //     the request.
        //
        // Returns:
        //     true to pre-authenticate; otherwise, false.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to get or set the property, when the property is not
        //     overridden in a descendant class.
        extern public virtual bool PreAuthenticate { get; set; }

        //
        // Summary:
        //     When overridden in a descendant class, gets or sets the network proxy to
        //     use to access this Internet resource.
        //
        // Returns:
        //     The System.Net.IWebProxy to use to access the Internet resource.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to get or set the property, when the property is not
        //     overridden in a descendant class.
        public virtual IWebProxy Proxy
        {
            get
            {
                // default is not null, but it's possible to set proxy to null.
                return default(IWebProxy);
            }
            set
            {
            }
        }

        //
        // Summary:
        //     When overridden in a descendant class, gets the URI of the Internet resource
        //     associated with the request.
        //
        // Returns:
        //     A System.Uri representing the resource associated with the request
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to get or set the property, when the property is not
        //     overridden in a descendant class.
        //public virtual Uri RequestUri { get; }
        //
        // Summary:
        //     Gets or sets the length of time, in milliseconds, before the request times
        //     out.
        //
        // Returns:
        //     The length of time, in milliseconds, until the request times out, or the
        //     value System.Threading.Timeout.Infinite to indicate that the request does
        //     not time out. The default value is defined by the descendant class.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to get or set the property, when the property is not
        //     overridden in a descendant class.
        //public virtual int Timeout { get; set; }

        //
        // Summary:
        //     When overridden in a descendant class, gets or sets a System.Boolean value
        //     that controls whether System.Net.CredentialCache.DefaultCredentials are sent
        //     with requests.
        //
        // Returns:
        //     true if the default credentials are used; otherwise false. The default value
        //     is false.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     You attempted to set this property after the request was sent.
        //public virtual bool UseDefaultCredentials { get; set; }

        // Summary:
        //     Aborts the Request
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to access the method, when the method is not overridden
        //     in a descendant class.
        //public virtual void Abort();

        //
        // Summary:
        //     When overridden in a descendant class, provides an asynchronous version of
        //     the System.Net.WebRequest.GetRequestStream() method.
        //
        // Parameters:
        //   callback:
        //     The System.AsyncCallback delegate.
        //
        //   state:
        //     An object containing state information for this asynchronous request.
        //
        // Returns:
        //     An System.IAsyncResult that references the asynchronous request.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to access the method, when the method is not overridden
        //     in a descendant class.
        public virtual IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
        {
            Contract.Ensures(Contract.Result<IAsyncResult>() != null);
            return null;
        }

        //
        // Summary:
        //     When overridden in a descendant class, begins an asynchronous request for
        //     an Internet resource.
        //
        // Parameters:
        //   callback:
        //     The System.AsyncCallback delegate.
        //
        //   state:
        //     An object containing state information for this asynchronous request.
        //
        // Returns:
        //     An System.IAsyncResult that references the asynchronous request.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to access the method, when the method is not overridden
        //     in a descendant class.
        public virtual IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            Contract.Ensures(Contract.Result<IAsyncResult>() != null);
            return null;
        }

        //
        // Summary:
        //     Initializes a new System.Net.WebRequest instance for the specified URI scheme.
        //
        // Parameters:
        //   requestUriString:
        //     The URI that identifies the Internet resource.
        //
        // Returns:
        //     A System.Net.WebRequest descendant for the specific URI scheme.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     The request scheme specified in requestUriString has not been registered.
        //
        //   System.ArgumentNullException:
        //     requestUriString is null.
        //
        //   System.Security.SecurityException:
        //     The caller does not have permission to connect to the requested URI or a
        //     URI that the request is redirected to.
        //
        //   System.UriFormatException:
        //     The URI specified in requestUriString is not a valid URI.
        public static WebRequest Create(string requestUriString)
        {
            Contract.Requires(requestUriString != null);
            Contract.Ensures(Contract.Result<WebRequest>() != null);

            return default(WebRequest);
        }

        //
        // Summary:
        //     Initializes a new System.Net.WebRequest instance for the specified URI scheme.
        //
        // Parameters:
        //   requestUri:
        //     A System.Uri containing the URI of the requested resource.
        //
        // Returns:
        //     A System.Net.WebRequest descendant for the specified URI scheme.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     The request scheme specified in requestUri is not registered.
        //
        //   System.ArgumentNullException:
        //     requestUri is null.
        //
        //   System.Security.SecurityException:
        //     The caller does not have permission to connect to the requested URI or a
        //     URI that the request is redirected to.
        public static WebRequest Create(Uri requestUri)
        {
            Contract.Requires(requestUri != null);
            Contract.Ensures(Contract.Result<WebRequest>() != null);

            return default(WebRequest);
        }
        //
        // Summary:
        //     Initializes a new System.Net.WebRequest instance for the specified URI scheme.
        //
        // Parameters:
        //   requestUri:
        //     A System.Uri containing the URI of the requested resource.
        //
        // Returns:
        //     A System.Net.WebRequest descendant for the specified URI scheme.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     The request scheme specified in requestUri is not registered.
        //
        //   System.ArgumentNullException:
        //     requestUri is null.
        //
        //   System.Security.SecurityException:
        //     The caller does not have permission to connect to the requested URI or a
        //     URI that the request is redirected to.
        public static WebRequest CreateDefault(Uri requestUri)
        {
            Contract.Requires(requestUri != null);
            Contract.Ensures(Contract.Result<WebRequest>() != null);

            return default(WebRequest);
        }
        //
        // Summary:
        //     When overridden in a descendant class, returns a System.IO.Stream for writing
        //     data to the Internet resource.
        //
        // Parameters:
        //   asyncResult:
        //     An System.IAsyncResult that references a pending request for a stream.
        //
        // Returns:
        //     A System.IO.Stream to write data to.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to access the method, when the method is not overridden
        //     in a descendant class.
        public virtual Stream EndGetRequestStream(IAsyncResult asyncResult)
        {
            Contract.Requires(asyncResult != null);
            Contract.Ensures(Contract.Result<Stream>() != null);
            return null;
        }

        //
        // Summary:
        //     When overridden in a descendant class, returns a System.Net.WebResponse.
        //
        // Parameters:
        //   asyncResult:
        //     An System.IAsyncResult that references a pending request for a response.
        //
        // Returns:
        //     A System.Net.WebResponse that contains a response to the Internet request.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to access the method, when the method is not overridden
        //     in a descendant class.
        public virtual WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            Contract.Requires(asyncResult != null);
            Contract.Ensures(Contract.Result<WebResponse>() != null);
            return null;
        }

        //
        // Summary:
        //     Populates a System.Runtime.Serialization.SerializationInfo with the data
        //     needed to serialize the target object.
        //
        // Parameters:
        //   serializationInfo:
        //     The System.Runtime.Serialization.SerializationInfo to populate with data.
        //
        //   streamingContext:
        //     A System.Runtime.Serialization.StreamingContext that specifies the destination
        //     for this serialization.
        //protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext);
        //
        // Summary:
        //     When overridden in a descendant class, returns a System.IO.Stream for writing
        //     data to the Internet resource.
        //
        // Returns:
        //     A System.IO.Stream for writing data to the Internet resource.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to access the method, when the method is not overridden
        //     in a descendant class.
        public virtual Stream GetRequestStream()
        {
            Contract.Ensures(Contract.Result<Stream>() != null);
            return null;
        }

        //
        // Summary:
        //     When overridden in a descendant class, returns a response to an Internet
        //     request.
        //
        // Returns:
        //     A System.Net.WebResponse containing the response to the Internet request.
        //
        // Exceptions:
        //   System.NotImplementedException:
        //     Any attempt is made to access the method, when the method is not overridden
        //     in a descendant class.
        public virtual WebResponse GetResponse()
        {
            Contract.Ensures(Contract.Result<WebResponse>() != null);
            return null;
        }

        //
        // Summary:
        //     Returns a proxy configured with the Internet Explorer settings of the currently
        //     impersonated user.
        //
        // Returns:
        //     An System.Net.IWebProxy used by every call to instances of System.Net.WebRequest.
        public static IWebProxy GetSystemWebProxy()
        {
            Contract.Ensures(Contract.Result<IWebProxy>() != null);

            return default(IWebProxy);
        }

        //
        // Summary:
        //     Registers a System.Net.WebRequest descendant for the specified URI.
        //
        // Parameters:
        //   prefix:
        //     The complete URI or URI prefix that the System.Net.WebRequest descendant
        //     services.
        //
        //   creator:
        //     The create method that the System.Net.WebRequest calls to create the System.Net.WebRequest
        //     descendant.
        //
        // Returns:
        //     true if registration is successful; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     prefix is null-or- creator is null.
        //public static bool RegisterPrefix(string prefix, IWebRequestCreate creator);
    }
}
#endif