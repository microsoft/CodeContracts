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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Windows.Browser
{
  // Summary:
  //     Provides general information about the browser, such as name, version, and
  //     operating system.
  public sealed class BrowserInformation
  {
    // Summary:
    //     Gets the browser's version information.
    //
    // Returns:
    //     The version of the browser.
    public Version BrowserVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<Version>() != null);
        return default(Version);
      }
    }
    //
    // Summary:
    //     Gets a value that indicates whether the browser supports cookies.
    //
    // Returns:
    //     true if the browser supports cookies; otherwise, false.
    extern public bool CookiesEnabled { get; }
    //
    // Summary:
    //     Gets the browser's name.
    //
    // Returns:
    //     The name of the browser.
    public string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the name of the browser's operating system.
    //
    // Returns:
    //     The name of the operating system the browser is running on.
    public string Platform
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets the browser's user agent string.
    //
    // Returns:
    //     The user agent string that identifies the browser.
    public string UserAgent
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
  }
}
