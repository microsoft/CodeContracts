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
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.Web
{
  // Summary:
  //     Provides a type-safe way to create and manipulate individual HTTP cookies.
  public sealed class HttpCookie
  {
    // Summary:
    //     Creates and names a new cookie.
    //
    // Parameters:
    //   name:
    //     The name of the new cookie.
    extern public HttpCookie(string name);
    //
    // Summary:
    //     Creates, names, and assigns a value to a new cookie.
    //
    // Parameters:
    //   name:
    //     The name of the new cookie.
    //
    //   value:
    //     The value of the new cookie.
    extern public HttpCookie(string name, string value);

    // Summary:
    //     Gets or sets the domain to associate the cookie with.
    //
    // Returns:
    //     The name of the domain to associate the cookie with. The default value is
    //     the current domain.
    extern public string Domain { get; set; }
    //
    // Summary:
    //     Gets or sets the expiration date and time for the cookie.
    //
    // Returns:
    //     The time of day (on the client) at which the cookie expires.
    extern public DateTime Expires { get; set; }
    //
    // Summary:
    //     Gets a value indicating whether a cookie has subkeys.
    //
    // Returns:
    //     true if the cookie has subkeys, otherwise, false. The default value is false.
    extern public bool HasKeys { get; }
    //
    // Summary:
    //     Gets or sets a value that specifies whether a cookie is accessible by client-side
    //     script.
    //
    // Returns:
    //     true if the cookie has the HttpOnly attribute and cannot be accessed through
    //     a client-side script; otherwise, false. The default is false.
    extern public bool HttpOnly { get; set; }
    //
    // Summary:
    //     Gets or sets the name of a cookie.
    //
    // Returns:
    //     The default value is a null reference (Nothing in Visual Basic) unless the
    //     constructor specifies otherwise.
    extern public string Name { get; set; }
    //
    // Summary:
    //     Gets or sets the virtual path to transmit with the current cookie.
    //
    // Returns:
    //     The virtual path to transmit with the cookie. The default is the path of
    //     the current request.
    extern public string Path { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to transmit the cookie using Secure
    //     Sockets Layer (SSL)--that is, over HTTPS only.
    //
    // Returns:
    //     true to transmit the cookie over an SSL connection (HTTPS); otherwise, false.
    //     The default value is false.
    extern public bool Secure { get; set; }
    //
    // Summary:
    //     Gets or sets an individual cookie value.
    //
    // Returns:
    //     The value of the cookie. The default value is a null reference (Nothing in
    //     Visual Basic).
    extern public string Value { get; set; }
    //
    // Summary:
    //     Gets a collection of key/value pairs that are contained within a single cookie
    //     object.
    //
    // Returns:
    //     A collection of cookie values.
    public NameValueCollection Values
    {
      get
      {
        Contract.Ensures(Contract.Result<NameValueCollection>() != null);
        return default(NameValueCollection);
      }
    }

    // Summary:
    //     Gets a shortcut to the System.Web.HttpCookie.Values property. This property
    //     is provided for compatibility with previous versions of Active Server Pages
    //     (ASP).
    //
    // Parameters:
    //   key:
    //     The key (index) of the cookie value.
    //
    // Returns:
    //     The cookie value.
    extern public string this[string key] { get; set; }
  }
}
