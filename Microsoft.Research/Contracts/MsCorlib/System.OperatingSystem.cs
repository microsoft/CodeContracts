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

  public class OperatingSystem: ICloneable
  {

    public PlatformID Platform
    {
      [Pure]
      get
      {
        Contract.Ensures(Contract.Result<PlatformID>() >= PlatformID.Win32S);
        Contract.Ensures(Contract.Result<PlatformID>() <= PlatformID.MacOSX);
        return default(PlatformID);
      }
    }

    public string ServicePack
    {
      [Pure]
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public Version Version
    {
      [Pure]
      get
      {
        Contract.Ensures(Contract.Result<Version>() != null);
        return default(Version);
      }
    }

    public string VersionString
    {
      get
      {
        Contract.Ensures(!string.IsNullOrWhitespace(Contract.Result<string>()));
        return default(string);
      }
    }

    public object Clone()
    {

      return default(object);
    }

    public OperatingSystem(PlatformID platform, Version version)
    {
      Contract.Requires(platform >= PlatformID.Win32S)
      Contract.Requires(platform <= PlatformID.MacOSX)
      Contract.Requires(version != null);
      Contract.EnsuresOnThrow<ArgumentException>(true, "platform is not a PlatformID enumeration value.")
      Contract.EnsuresOnThrow<ArgumentNullException>(true, "version is null.")
    }
  }
}
