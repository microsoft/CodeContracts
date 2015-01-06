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

// File System.Data.Services.DataService_1.cs
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
  public partial class DataService<T> : IRequestHandler, IDataService
  {
    #region Methods and constructors
    public void AttachHost(IDataServiceHost host)
    {
      Contract.Requires(host != null);
    }

    protected virtual new T CreateDataSource()
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public DataService()
    {
    }

    protected virtual new void HandleException(HandleExceptionArgs args)
    {
      Contract.Requires(args != null);
    }

    protected virtual new void OnStartProcessingRequest(ProcessRequestArgs args)
    {
    }

    public void ProcessRequest()
    {
    }

    public System.ServiceModel.Channels.Message ProcessRequestForMessage(Stream messageBody)
    {
      Contract.Requires(messageBody != null);

      Contract.Ensures(Contract.Result<System.ServiceModel.Channels.Message>() != null);

      return default(System.ServiceModel.Channels.Message);
    }

    void System.Data.Services.IDataService.DisposeDataSource()
    {
    }

    void System.Data.Services.IDataService.InternalApplyingExpansions(System.Linq.IQueryable queryable, ICollection<ExpandSegmentCollection> expandPaths)
    {
    }

    void System.Data.Services.IDataService.InternalHandleException(HandleExceptionArgs args)
    {
    }

    void System.Data.Services.IDataService.InternalOnStartProcessingRequest(ProcessRequestArgs args)
    {
    }
    #endregion

    #region Properties and indexers
    protected T CurrentDataSource
    {
      get
      {
        Contract.Ensures(Contract.Result<T>() != null);

        return default(T);
      }
    }

    public DataServiceProcessingPipeline ProcessingPipeline
    {
      get
      {
        Contract.Ensures(Contract.Result<DataServiceProcessingPipeline>() != null);

        return default(DataServiceProcessingPipeline);
      }
    }

    DataServiceConfiguration System.Data.Services.IDataService.Configuration
    {
      get
      {
        return default(DataServiceConfiguration);
      }
    }

    Object System.Data.Services.IDataService.Instance
    {
      get
      {
        return default(Object);
      }
    }

    DataServiceOperationContext System.Data.Services.IDataService.OperationContext
    {
      get
      {
        return default(DataServiceOperationContext);
      }
    }

    DataServiceProcessingPipeline System.Data.Services.IDataService.ProcessingPipeline
    {
      get
      {
        return default(DataServiceProcessingPipeline);
      }
    }
    #endregion
  }
}
