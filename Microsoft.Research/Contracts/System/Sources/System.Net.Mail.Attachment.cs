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

// File System.Net.Mail.Attachment.cs
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
  public partial class Attachment : AttachmentBase
  {
    #region Methods and constructors
    public Attachment(Stream contentStream, string name)
    {
    }

    public Attachment(Stream contentStream, string name, string mediaType)
    {
    }

    public Attachment(Stream contentStream, System.Net.Mime.ContentType contentType)
    {
      Contract.Requires(contentType != null);
    }

    public Attachment(string fileName)
    {
      Contract.Requires(fileName != null);
    }

    public Attachment(string fileName, string mediaType)
    {
      Contract.Requires(fileName != null);
    }

    public Attachment(string fileName, System.Net.Mime.ContentType contentType)
    {
      Contract.Requires(contentType != null);
    }

    public static System.Net.Mail.Attachment CreateAttachmentFromString(string content, string name, Encoding contentEncoding, string mediaType)
    {
      Contract.Ensures(2 <= mediaType.Length);
      Contract.Ensures(Contract.Result<System.Net.Mail.Attachment>() != null);

      return default(System.Net.Mail.Attachment);
    }

    public static System.Net.Mail.Attachment CreateAttachmentFromString(string content, System.Net.Mime.ContentType contentType)
    {
      Contract.Requires(contentType != null);
      Contract.Ensures(Contract.Result<System.Net.Mail.Attachment>() != null);

      return default(System.Net.Mail.Attachment);
    }

    public static System.Net.Mail.Attachment CreateAttachmentFromString(string content, string name)
    {
      Contract.Ensures(2 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.Net.Mail.Attachment>() != null);

      return default(System.Net.Mail.Attachment);
    }
    #endregion

    #region Properties and indexers
    public System.Net.Mime.ContentDisposition ContentDisposition
    {
      get
      {
        return default(System.Net.Mime.ContentDisposition);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Encoding NameEncoding
    {
      get
      {
        return default(Encoding);
      }
      set
      {
      }
    }
    #endregion
  }
}
