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

// File System.Net.HttpListener.cs
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
  sealed public partial class HttpListener : IDisposable
  {
    #region Delegates
    public delegate System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy ExtendedProtectionSelector(HttpListenerRequest request);
    #endregion

    #region Methods and constructors
    public void Abort()
    {
    }

    public IAsyncResult BeginGetContext(AsyncCallback callback, Object state)
    {
      Contract.Ensures(Contract.Result<System.IAsyncResult>() != null);

      return default(IAsyncResult);
    }

    public void Close()
    {
    }

    public HttpListenerContext EndGetContext(IAsyncResult asyncResult)
    {
      Contract.Ensures(Contract.Result<System.Net.HttpListenerContext>() != null);

      return default(HttpListenerContext);
    }

    public HttpListenerContext GetContext()
    {
      Contract.Ensures(Contract.Result<System.Net.HttpListenerContext>() != null);

      return default(HttpListenerContext);
    }

    public HttpListener()
    {
    }

    public void Start()
    {
    }

    public void Stop()
    {
    }

    void System.IDisposable.Dispose()
    {
    }
    #endregion

    #region Properties and indexers
    public AuthenticationSchemes AuthenticationSchemes
    {
      get
      {
        return default(AuthenticationSchemes);
      }
      set
      {
      }
    }

    public AuthenticationSchemeSelector AuthenticationSchemeSelectorDelegate
    {
      get
      {
        return default(AuthenticationSchemeSelector);
      }
      set
      {
      }
    }

    public System.Security.Authentication.ExtendedProtection.ServiceNameCollection DefaultServiceNames
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Security.Authentication.ExtendedProtection.ServiceNameCollection>() != null);

        return default(System.Security.Authentication.ExtendedProtection.ServiceNameCollection);
      }
    }

    public System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy ExtendedProtectionPolicy
    {
      get
      {
        return default(System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy);
      }
      set
      {
        Contract.Ensures(value.CustomChannelBinding == null);
      }
    }

    public System.Net.HttpListener.ExtendedProtectionSelector ExtendedProtectionSelectorDelegate
    {
      get
      {
        return default(System.Net.HttpListener.ExtendedProtectionSelector);
      }
      set
      {
      }
    }

    public bool IgnoreWriteExceptions
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsListening
    {
      get
      {
        return default(bool);
      }
    }

    public static bool IsSupported
    {
      get
      {
        return default(bool);
      }
    }

    public HttpListenerPrefixCollection Prefixes
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.HttpListenerPrefixCollection>() != null);

        return default(HttpListenerPrefixCollection);
      }
    }

    public string Realm
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool UnsafeConnectionNtlmAuthentication
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
