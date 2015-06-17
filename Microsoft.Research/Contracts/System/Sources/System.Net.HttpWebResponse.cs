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

// File System.Net.HttpWebResponse.cs
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
  public partial class HttpWebResponse : WebResponse, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public override void Close()
    {
    }

    protected override void Dispose(bool disposing)
    {
    }

    protected override void GetObjectData(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }

    public string GetResponseHeader(string headerName)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public override Stream GetResponseStream()
    {
      return default(Stream);
    }

    protected HttpWebResponse(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
      Contract.Requires(serializationInfo != null);
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }
    #endregion

    #region Properties and indexers
    public string CharacterSet
    {
      get
      {
        return default(string);
      }
    }

    public string ContentEncoding
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public override long ContentLength
    {
      get
      {
        return default(long);
      }
    }

    public override string ContentType
    {
      get
      {
        return default(string);
      }
    }

    public CookieCollection Cookies
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.CookieCollection>() != null);

        return default(CookieCollection);
      }
      set
      {
      }
    }

    public override WebHeaderCollection Headers
    {
      get
      {
        return default(WebHeaderCollection);
      }
    }

    public override bool IsMutuallyAuthenticated
    {
      get
      {
        return default(bool);
      }
    }

    public DateTime LastModified
    {
      get
      {
        return default(DateTime);
      }
    }

    public string Method
    {
      get
      {
        return default(string);
      }
    }

    public Version ProtocolVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Version>() != null);

        return default(Version);
      }
    }

    public override Uri ResponseUri
    {
      get
      {
        return default(Uri);
      }
    }

    public string Server
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public HttpStatusCode StatusCode
    {
      get
      {
        return default(HttpStatusCode);
      }
    }

    public string StatusDescription
    {
      get
      {
        return default(string);
      }
    }

    public override bool SupportsHeaders
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
