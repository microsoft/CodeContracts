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

// File System.Data.Services.IDataServiceHost.cs
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


namespace System.Data.Services
{
  [ContractClass(typeof(IDataServiceHostContracts))]
  public partial interface IDataServiceHost
  {
    #region Methods and constructors
    string GetQueryStringItem(string item);

    void ProcessException(HandleExceptionArgs args);
    #endregion

    #region Properties and indexers
    Uri AbsoluteRequestUri
    {
      get;
    }

    Uri AbsoluteServiceUri
    {
      get;
    }

    string RequestAccept
    {
      get;
    }

    string RequestAcceptCharSet
    {
      get;
    }

    string RequestContentType
    {
      get;
    }

    string RequestHttpMethod
    {
      get;
    }

    string RequestIfMatch
    {
      get;
    }

    string RequestIfNoneMatch
    {
      get;
    }

    string RequestMaxVersion
    {
      get;
    }

    Stream RequestStream
    {
      get;
    }

    string RequestVersion
    {
      get;
    }

    string ResponseCacheControl
    {
      get;
      set;
    }

    string ResponseContentType
    {
      get;
      set;
    }

    string ResponseETag
    {
      get;
      set;
    }

    string ResponseLocation
    {
      get;
      set;
    }

    int ResponseStatusCode
    {
      get;
      set;
    }

    Stream ResponseStream
    {
      get;
    }

    string ResponseVersion
    {
      get;
      set;
    }
    #endregion
  }

  [ContractClassFor(typeof(IDataServiceHost))]
  class IDataServiceHostContracts : IDataServiceHost
  {
    public string GetQueryStringItem(string item)
    {
      Contract.Requires(item != null);

      throw new NotImplementedException();
    }

    public void ProcessException(HandleExceptionArgs args)
    {
      throw new NotImplementedException();
    }

    public Uri AbsoluteRequestUri
    {
      get {
        Contract.Ensures(Contract.Result<Uri>() != null);
        throw new NotImplementedException(); 
      }
    }

    public Uri AbsoluteServiceUri
    {
      get
      {
        Contract.Ensures(Contract.Result<Uri>() != null);
        throw new NotImplementedException();
      }
    }

    public string RequestAccept
    {
      get {
        Contract.Ensures(Contract.Result<string>() != null);

        throw new NotImplementedException(); }
    }

    public string RequestAcceptCharSet
    {    
      // can be null
      get { throw new NotImplementedException(); }
    }

    public string RequestContentType
    {
      // can be null
      get { throw new NotImplementedException(); }
    }

    public string RequestHttpMethod
    {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        throw new NotImplementedException(); 
      }
    }

    public string RequestIfMatch
    {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        throw new NotImplementedException();
      }
    }

    public string RequestIfNoneMatch
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        throw new NotImplementedException();
      }
    }

    public string RequestMaxVersion
    {
      // can be null
      get { throw new NotImplementedException(); }
    }

    public Stream RequestStream
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        
        throw new NotImplementedException();
      }
    }

    public string RequestVersion
    {
      // can be null
      get { throw new NotImplementedException(); }
    }

    public string ResponseCacheControl
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public string ResponseContentType
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public string ResponseETag
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public string ResponseLocation
    {
      get
      {
        Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public int ResponseStatusCode
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public Stream ResponseStream
    {    
      // can be null
      get { throw new NotImplementedException(); }
    }

    public string ResponseVersion
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }
  }
}
