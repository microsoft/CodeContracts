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

// File System.Net.Mail.MailMessage.cs
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
  public partial class MailMessage : IDisposable
  {
    #region Methods and constructors
    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public MailMessage(string from, string to)
    {
    }

    public MailMessage(string from, string to, string subject, string body)
    {
    }

    public MailMessage(MailAddress from, MailAddress to)
    {
    }

    public MailMessage()
    {
    }
    #endregion

    #region Properties and indexers
    public AlternateViewCollection AlternateViews
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.Mail.AlternateViewCollection>() != null);

        return default(AlternateViewCollection);
      }
    }

    public AttachmentCollection Attachments
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.Mail.AttachmentCollection>() != null);

        return default(AttachmentCollection);
      }
    }

    public MailAddressCollection Bcc
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.Mail.MailAddressCollection>() != null);

        return default(MailAddressCollection);
      }
    }

    public string Body
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
      }
    }

    public Encoding BodyEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public MailAddressCollection CC
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.Mail.MailAddressCollection>() != null);

        return default(MailAddressCollection);
      }
    }

    public DeliveryNotificationOptions DeliveryNotificationOptions
    {
      get
      {
        return default(DeliveryNotificationOptions);
      }
      set
      {
      }
    }

    public MailAddress From
    {
      get
      {
        return default(MailAddress);
      }
      set
      {
      }
    }

    public System.Collections.Specialized.NameValueCollection Headers
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Specialized.NameValueCollection>() != null);

        return default(System.Collections.Specialized.NameValueCollection);
      }
    }

    public Encoding HeadersEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public bool IsBodyHtml
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public MailPriority Priority
    {
      get
      {
        return default(MailPriority);
      }
      set
      {
      }
    }

    public MailAddress ReplyTo
    {
      get
      {
        return default(MailAddress);
      }
      set
      {
      }
    }

    public MailAddressCollection ReplyToList
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.Mail.MailAddressCollection>() != null);

        return default(MailAddressCollection);
      }
    }

    public MailAddress Sender
    {
      get
      {
        return default(MailAddress);
      }
      set
      {
      }
    }

    public string Subject
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
      }
    }

    public Encoding SubjectEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }

    public MailAddressCollection To
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Net.Mail.MailAddressCollection>() != null);

        return default(MailAddressCollection);
      }
    }
    #endregion
  }
}
