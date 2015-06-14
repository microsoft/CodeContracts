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

// File System.Net.Mail.SmtpClient.cs
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


namespace System.Net.Mail
{
  public partial class SmtpClient : IDisposable
  {
    #region Methods and constructors
    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    protected void OnSendCompleted(System.ComponentModel.AsyncCompletedEventArgs e)
    {
    }

    public void Send(string from, string recipients, string subject, string body)
    {
      Contract.Ensures(0 <= body.Length);
      Contract.Ensures(0 <= subject.Length);
    }

    public void Send(MailMessage message)
    {
    }

    public void SendAsync(MailMessage message, Object userToken)
    {
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void SendAsync(string from, string recipients, string subject, string body, Object userToken)
    {
      Contract.Ensures(0 <= body.Length);
      Contract.Ensures(0 <= subject.Length);
      Contract.Ensures(System.ComponentModel.AsyncOperationManager.SynchronizationContext == System.Threading.SynchronizationContext.Current);
    }

    public void SendAsyncCancel()
    {
    }

    public SmtpClient(string host, int port)
    {
    }

    public SmtpClient(string host)
    {
    }

    public SmtpClient()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
    {
      get
      {
        return default(System.Security.Cryptography.X509Certificates.X509CertificateCollection);
      }
    }

    public System.Net.ICredentialsByHost Credentials
    {
      get
      {
        return default(System.Net.ICredentialsByHost);
      }
      set
      {
      }
    }

    public SmtpDeliveryMethod DeliveryMethod
    {
      get
      {
        return default(SmtpDeliveryMethod);
      }
      set
      {
      }
    }

    public bool EnableSsl
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string Host
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string PickupDirectoryLocation
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int Port
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public System.Net.ServicePoint ServicePoint
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.ServicePoint>() != null);

        return default(System.Net.ServicePoint);
      }
    }

    public string TargetName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int Timeout
    {
      get
      {
        return default(int);
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

    #region Events
    public event SendCompletedEventHandler SendCompleted
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
