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
  //     Provides a type-safe way to manipulate HTTP cookies.
  public sealed class HttpCookieCollection : NameObjectCollectionBase
  {
    // Summary:
    //     Initializes a new instance of the System.Web.HttpCookieCollection class.
    extern public HttpCookieCollection();

    // Summary:
    //     Gets a string array containing all the keys (cookie names) in the cookie
    //     collection.
    //
    // Returns:
    //     An array of cookie names.
    public string[] AllKeys
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);
        return default(string[]);
      }
    }


    // Summary:
    //     Gets the cookie with the specified numerical index from the cookie collection.
    //
    // Parameters:
    //   index:
    //     The index of the cookie to retrieve from the collection.
    //
    // Returns:
    //     The System.Web.HttpCookie specified by index.
    extern public HttpCookie this[int index] { get; }
    //
    // Summary:
    //     Gets the cookie with the specified name from the cookie collection.
    //
    // Parameters:
    //   name:
    //     Name of cookie to retrieve.
    //
    // Returns:
    //     The System.Web.HttpCookie specified by name.
    extern public HttpCookie this[string name] { get; }

    // Summary:
    //     Adds the specified cookie to the cookie collection.
    //
    // Parameters:
    //   cookie:
    //     The System.Web.HttpCookie to add to the collection.
    public void Add(HttpCookie cookie)
    {
      Contract.Requires(cookie != null);
    }

    //
    // Summary:
    //     Clears all cookies from the cookie collection.
    extern public void Clear();
    //
    // Summary:
    //     Copies members of the cookie collection to an System.Array beginning at the
    //     specified index of the array.
    //
    // Parameters:
    //   dest:
    //     The destination System.Array.
    //
    //   index:
    //     The index of the destination array where copying starts.
    public void CopyTo(Array dest, int index)
    {
      Contract.Requires(dest != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index < dest.Length);
    }
    //
    // Summary:
    //     Returns the System.Web.HttpCookie item with the specified index from the
    //     cookie collection.
    //
    // Parameters:
    //   index:
    //     The index of the cookie to return from the collection.
    //
    // Returns:
    //     The System.Web.HttpCookie specified by index.
    extern public HttpCookie Get(int index);
    //
    // Summary:
    //     Returns the cookie with the specified name from the cookie collection.
    //
    // Parameters:
    //   name:
    //     The name of the cookie to retrieve from the collection.
    //
    // Returns:
    //     The System.Web.HttpCookie specified by name.
    extern public HttpCookie Get(string name);
    //
    // Summary:
    //     Returns the key (name) of the cookie at the specified numerical index.
    //
    // Parameters:
    //   index:
    //     The index of the key to retrieve from the collection.
    //
    // Returns:
    //     The name of the cookie specified by index.
    extern public string GetKey(int index);
    //
    // Summary:
    //     Removes the cookie with the specified name from the collection.
    //
    // Parameters:
    //   name:
    //     The name of the cookie to remove from the collection.
    extern public void Remove(string name);
    //
    // Summary:
    //     Updates the value of an existing cookie in a cookie collection.
    //
    // Parameters:
    //   cookie:
    //     The System.Web.HttpCookie object to update.
    public void Set(HttpCookie cookie)
    {
      Contract.Requires(cookie != null);
    }
  }
}