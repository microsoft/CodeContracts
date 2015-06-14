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

// File System.ServiceModel.Administration.WbemNative.IWbemServices.cs
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


namespace System.ServiceModel.Administration
{
  internal partial class WbemNative
  {
    internal partial interface IWbemServices
    {
      #region Methods and constructors
      int CancelAsyncCall(System.ServiceModel.Administration.WbemNative.IWbemObjectSink pSink);

      int CreateClassEnumAsync(string strSuperclass, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, System.ServiceModel.Administration.WbemNative.IWbemObjectSink pResponseHandler);

      int CreateInstanceEnumAsync(string strFilter, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, System.ServiceModel.Administration.WbemNative.IWbemObjectSink pResponseHandler);

      int DeleteClass(string strClass, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, IntPtr ppCallResult);

      int DeleteClassAsync(string strClass, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, System.ServiceModel.Administration.WbemNative.IWbemObjectSink pResponseHandler);

      int DeleteInstance(string strObjectPath, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, IntPtr ppCallResult);

      int DeleteInstanceAsync(string strObjectPath, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, System.ServiceModel.Administration.WbemNative.IWbemObjectSink pResponseHandler);

      int ExecMethodAsync(string strObjectPath, string strMethodName, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, System.ServiceModel.Administration.WbemNative.IWbemClassObject pInParams, System.ServiceModel.Administration.WbemNative.IWbemObjectSink pResponseHandler);

      int ExecNotificationQueryAsync(string strQueryLanguage, string strQuery, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, System.ServiceModel.Administration.WbemNative.IWbemObjectSink pResponseHandler);

      int ExecQueryAsync(string strQueryLanguage, string strQuery, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, System.ServiceModel.Administration.WbemNative.IWbemObjectSink pResponseHandler);

      int GetObjectAsync(string strObjectPath, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, System.ServiceModel.Administration.WbemNative.IWbemObjectSink pResponseHandler);

      int PutClass(System.ServiceModel.Administration.WbemNative.IWbemClassObject pObject, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, IntPtr ppCallResult);

      int PutClassAsync(System.ServiceModel.Administration.WbemNative.IWbemClassObject pObject, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, System.ServiceModel.Administration.WbemNative.IWbemObjectSink pResponseHandler);

      int PutInstance(System.ServiceModel.Administration.WbemNative.IWbemClassObject pInst, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, IntPtr ppCallResult);

      int PutInstanceAsync(System.ServiceModel.Administration.WbemNative.IWbemClassObject pInst, int lFlags, System.ServiceModel.Administration.WbemNative.IWbemContext pCtx, System.ServiceModel.Administration.WbemNative.IWbemObjectSink pResponseHandler);
      #endregion
    }
  }
}
