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
  //     Provides access to and organizes files uploaded by a client.
  public sealed class HttpFileCollection : NameObjectCollectionBase
  {
    internal HttpFileCollection() { }

    // Summary:
    //     Gets a string array containing the keys (names) of all members in the file
    //     collection.
    //
    // Returns:
    //     An array of file names.
    public string[] AllKeys
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);
        return default(string[]);
      }
    }

    // Summary:
    //     Gets the object with the specified numerical index from the System.Web.HttpFileCollection.
    //
    // Parameters:
    //   index:
    //     The index of the item to get from the file collection.
    //
    // Returns:
    //     The System.Web.HttpPostedFile specified by index.
    public HttpPostedFile this[int index]
    {
      get
      {
        Contract.Ensures(Contract.Result<HttpPostedFile>() != null);
        return default(HttpPostedFile);
      }
    }
    //
    // Summary:
    //     Gets the object with the specified name from the file collection.
    //
    // Parameters:
    //   name:
    //     Name of item to be returned.
    //
    // Returns:
    //     The System.Web.HttpPostedFile specified by name.
    extern public HttpPostedFile this[string name] { get; }

    // Summary:
    //     Copies members of the file collection to an System.Array beginning at the
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
    //     Returns the System.Web.HttpPostedFile object with the specified numerical
    //     index from the file collection.
    //
    // Parameters:
    //   index:
    //     The index of the object to be returned from the file collection.
    //
    // Returns:
    //     An System.Web.HttpPostedFile object.
    public HttpPostedFile Get(int index)
    {
      Contract.Ensures(Contract.Result<HttpPostedFile>() != null);
      return default(HttpPostedFile);
    }
    //
    // Summary:
    //     Returns the System.Web.HttpPostedFile object with the specified name from
    //     the file collection.
    //
    // Parameters:
    //   name:
    //     The name of the object to be returned from a file collection.
    //
    // Returns:
    //     An System.Web.HttpPostedFile object.
    extern public HttpPostedFile Get(string name);
    //
    // Summary:
    //     Returns the name of the System.Web.HttpFileCollection member with the specified
    //     numerical index.
    //
    // Parameters:
    //   index:
    //     The index of the object name to be returned.
    //
    // Returns:
    //     The name of the System.Web.HttpFileCollection member specified by index.
    extern public string GetKey(int index);
  }
}
