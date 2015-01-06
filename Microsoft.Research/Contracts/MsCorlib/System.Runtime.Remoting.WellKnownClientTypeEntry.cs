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

namespace System.Runtime.Remoting
{

  public class WellKnownClientTypeEntry
  {

    extern public string ApplicationUrl
    {
      get;
      set;
    }

    extern public Type ObjectType
    {
      get;
    }

    extern public string ObjectUrl
    {
      get;
    }

    public WellKnownClientTypeEntry(Type type, string objectUrl)
    {
      Contract.Requires(type != null);
      Contract.Requires(objectUrl != null);

      return default(WellKnownClientTypeEntry);
    }
    public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
    {
      Contract.Requires(typeName != null);
      Contract.Requires(assemblyName != null);
      Contract.Requires(objectUrl != null);
      return default(WellKnownClientTypeEntry);
    }
  }
}
