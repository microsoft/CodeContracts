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

// File System.Net.WebProxy.cs
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
  public partial class WebProxy : IAutoWebProxy, IWebProxy, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public static System.Net.WebProxy GetDefaultProxy()
    {
      Contract.Ensures(Contract.Result<System.Net.WebProxy>() != null);

      return default(System.Net.WebProxy);
    }

    protected virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
      Contract.Requires(serializationInfo != null);
    }

    public Uri GetProxy(Uri destination)
    {
      return default(Uri);
    }

    public bool IsBypassed(Uri host)
    {
      return default(bool);
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }

    public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList)
    {
    }

    public WebProxy(Uri Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials)
    {
    }

    public WebProxy(Uri Address, bool BypassOnLocal)
    {
    }

    public WebProxy()
    {
    }

    public WebProxy(Uri Address)
    {
    }

    public WebProxy(string Address)
    {
    }

    public WebProxy(string Address, bool BypassOnLocal)
    {
    }

    public WebProxy(string Address, bool BypassOnLocal, string[] BypassList, ICredentials Credentials)
    {
    }

    public WebProxy(string Host, int Port)
    {
    }

    public WebProxy(string Address, bool BypassOnLocal, string[] BypassList)
    {
    }

    protected WebProxy(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
      Contract.Requires(serializationInfo != null);
    }
    #endregion

    #region Properties and indexers
    public Uri Address
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public System.Collections.ArrayList BypassArrayList
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ArrayList>() != null);

        return default(System.Collections.ArrayList);
      }
    }

    public string[] BypassList
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public bool BypassProxyOnLocal
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ICredentials Credentials
    {
      get
      {
        return default(ICredentials);
      }
      set
      {
      }
    }

    public bool UseDefaultCredentials
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}
