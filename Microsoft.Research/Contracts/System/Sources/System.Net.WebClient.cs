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

// File System.Net.WebClient.cs
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
  public partial class WebClient : System.ComponentModel.Component
  {
    #region Methods and constructors
    public void CancelAsync()
    {
    }

    public byte[] DownloadData(Uri address)
    {
      return default(byte[]);
    }

    public byte[] DownloadData(string address)
    {
      return default(byte[]);
    }

    public void DownloadDataAsync(Uri address)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void DownloadDataAsync(Uri address, Object userToken)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void DownloadFile(Uri address, string fileName)
    {
    }

    public void DownloadFile(string address, string fileName)
    {
    }

    public void DownloadFileAsync(Uri address, string fileName, Object userToken)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void DownloadFileAsync(Uri address, string fileName)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public string DownloadString(Uri address)
    {
      return default(string);
    }

    public string DownloadString(string address)
    {
      return default(string);
    }

    public void DownloadStringAsync(Uri address)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void DownloadStringAsync(Uri address, Object userToken)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    protected virtual new WebRequest GetWebRequest(Uri address)
    {
      return default(WebRequest);
    }

    protected virtual new WebResponse GetWebResponse(WebRequest request)
    {
      Contract.Requires(request != null);

      return default(WebResponse);
    }

    protected virtual new WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
    {
      Contract.Requires(request != null);

      return default(WebResponse);
    }

    protected virtual new void OnDownloadDataCompleted(DownloadDataCompletedEventArgs e)
    {
    }

    protected virtual new void OnDownloadFileCompleted(System.ComponentModel.AsyncCompletedEventArgs e)
    {
    }

    protected virtual new void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e)
    {
    }

    protected virtual new void OnDownloadStringCompleted(DownloadStringCompletedEventArgs e)
    {
    }

    protected virtual new void OnOpenReadCompleted(OpenReadCompletedEventArgs e)
    {
    }

    protected virtual new void OnOpenWriteCompleted(OpenWriteCompletedEventArgs e)
    {
    }

    protected virtual new void OnUploadDataCompleted(UploadDataCompletedEventArgs e)
    {
    }

    protected virtual new void OnUploadFileCompleted(UploadFileCompletedEventArgs e)
    {
    }

    protected virtual new void OnUploadProgressChanged(UploadProgressChangedEventArgs e)
    {
    }

    protected virtual new void OnUploadStringCompleted(UploadStringCompletedEventArgs e)
    {
    }

    protected virtual new void OnUploadValuesCompleted(UploadValuesCompletedEventArgs e)
    {
    }

    public Stream OpenRead(string address)
    {
      return default(Stream);
    }

    public Stream OpenRead(Uri address)
    {
      return default(Stream);
    }

    public void OpenReadAsync(Uri address)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void OpenReadAsync(Uri address, Object userToken)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public Stream OpenWrite(string address, string method)
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public Stream OpenWrite(Uri address, string method)
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public Stream OpenWrite(Uri address)
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public Stream OpenWrite(string address)
    {
      Contract.Ensures(Contract.Result<System.IO.Stream>() != null);

      return default(Stream);
    }

    public void OpenWriteAsync(Uri address, string method, Object userToken)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void OpenWriteAsync(Uri address, string method)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void OpenWriteAsync(Uri address)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public byte[] UploadData(string address, byte[] data)
    {
      return default(byte[]);
    }

    public byte[] UploadData(Uri address, string method, byte[] data)
    {
      return default(byte[]);
    }

    public byte[] UploadData(string address, string method, byte[] data)
    {
      return default(byte[]);
    }

    public byte[] UploadData(Uri address, byte[] data)
    {
      return default(byte[]);
    }

    public void UploadDataAsync(Uri address, byte[] data)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void UploadDataAsync(Uri address, string method, byte[] data)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void UploadDataAsync(Uri address, string method, byte[] data, Object userToken)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public byte[] UploadFile(Uri address, string method, string fileName)
    {
      return default(byte[]);
    }

    public byte[] UploadFile(string address, string fileName)
    {
      return default(byte[]);
    }

    public byte[] UploadFile(string address, string method, string fileName)
    {
      return default(byte[]);
    }

    public byte[] UploadFile(Uri address, string fileName)
    {
      return default(byte[]);
    }

    public void UploadFileAsync(Uri address, string method, string fileName, Object userToken)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void UploadFileAsync(Uri address, string method, string fileName)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void UploadFileAsync(Uri address, string fileName)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public string UploadString(Uri address, string method, string data)
    {
      return default(string);
    }

    public string UploadString(Uri address, string data)
    {
      return default(string);
    }

    public string UploadString(string address, string method, string data)
    {
      return default(string);
    }

    public string UploadString(string address, string data)
    {
      return default(string);
    }

    public void UploadStringAsync(Uri address, string data)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void UploadStringAsync(Uri address, string method, string data, Object userToken)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void UploadStringAsync(Uri address, string method, string data)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public byte[] UploadValues(string address, System.Collections.Specialized.NameValueCollection data)
    {
      Contract.Requires(data.AllKeys != null);
      Contract.Ensures(0 <= data.AllKeys.Length);

      return default(byte[]);
    }

    public byte[] UploadValues(Uri address, string method, System.Collections.Specialized.NameValueCollection data)
    {
      Contract.Requires(data.AllKeys != null);
      Contract.Ensures(0 <= data.AllKeys.Length);

      return default(byte[]);
    }

    public byte[] UploadValues(string address, string method, System.Collections.Specialized.NameValueCollection data)
    {
      Contract.Requires(data.AllKeys != null);
      Contract.Ensures(0 <= data.AllKeys.Length);

      return default(byte[]);
    }

    public byte[] UploadValues(Uri address, System.Collections.Specialized.NameValueCollection data)
    {
      Contract.Requires(data.AllKeys != null);
      Contract.Ensures(0 <= data.AllKeys.Length);

      return default(byte[]);
    }

    public void UploadValuesAsync(Uri address, string method, System.Collections.Specialized.NameValueCollection data)
    {
      Contract.Requires(data.AllKeys != null);
      Contract.Ensures(0 <= data.AllKeys.Length);
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void UploadValuesAsync(Uri address, System.Collections.Specialized.NameValueCollection data)
    {
      Contract.Requires(data.AllKeys != null);
      Contract.Ensures(0 <= data.AllKeys.Length);
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void UploadValuesAsync(Uri address, string method, System.Collections.Specialized.NameValueCollection data, Object userToken)
    {
      Contract.Requires(data.AllKeys != null);
      Contract.Ensures(0 <= data.AllKeys.Length);
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public WebClient()
    {
    }
    #endregion

    #region Properties and indexers
    public string BaseAddress
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Net.Cache.RequestCachePolicy CachePolicy
    {
      get
      {
        return default(System.Net.Cache.RequestCachePolicy);
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

    public Encoding Encoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public WebHeaderCollection Headers
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.WebHeaderCollection>() != null);

        return default(WebHeaderCollection);
      }
      set
      {
      }
    }

    public bool IsBusy
    {
      get
      {
        return default(bool);
      }
    }

    public IWebProxy Proxy
    {
      get
      {
        return default(IWebProxy);
      }
      set
      {
      }
    }

    public System.Collections.Specialized.NameValueCollection QueryString
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Specialized.NameValueCollection>() != null);

        return default(System.Collections.Specialized.NameValueCollection);
      }
      set
      {
      }
    }

    public WebHeaderCollection ResponseHeaders
    {
      get
      {
        return default(WebHeaderCollection);
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

    #region Events
    public event DownloadDataCompletedEventHandler DownloadDataCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.ComponentModel.AsyncCompletedEventHandler DownloadFileCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DownloadProgressChangedEventHandler DownloadProgressChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DownloadStringCompletedEventHandler DownloadStringCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event OpenReadCompletedEventHandler OpenReadCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event OpenWriteCompletedEventHandler OpenWriteCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event UploadDataCompletedEventHandler UploadDataCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event UploadFileCompletedEventHandler UploadFileCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event UploadProgressChangedEventHandler UploadProgressChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event UploadStringCompletedEventHandler UploadStringCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event UploadValuesCompletedEventHandler UploadValuesCompleted
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
