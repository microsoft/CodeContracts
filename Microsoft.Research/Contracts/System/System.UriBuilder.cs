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
using System.Diagnostics.Contracts;

namespace System
{

  public class UriBuilder
  {
    public UriBuilder(string uri)
    {
      Contract.Requires(uri != null);
    }
    public UriBuilder(Uri uri)
    {
      Contract.Requires(uri != null);
    }

    // public UriBuilder(string schemeName, string hostName);

    public UriBuilder(string scheme, string host, int portNumber)
    {
      Contract.Requires(portNumber >= -1);
    }

    public UriBuilder(string scheme, string host, int port, string pathValue)
    {
      Contract.Requires(port >= -1);
    }

    public UriBuilder(string scheme, string host, int port, string path, string extraValue)
    {
      Contract.Requires(port >= -1);
    }

    // Properties
    public string Fragment
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
      }
    }
    public string Host
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
      }
    }
    public string Password
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
      }
    }
    public string Path
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(Contract.Result<string>().Length > 0);
        return default(string);
      }

      set { }
    }
    public int Port
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= -1);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= -1);
        Contract.Requires(value <= 0xffff);
      }
    }
    public string Query
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
      }
    }
    public string Scheme
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set { }
    }
    public Uri Uri
    {
      get
      {
        Contract.Ensures(Contract.Result<Uri>() != null);
        return default(Uri);
      }
    }
    public string UserName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set { }
    }
  }
}