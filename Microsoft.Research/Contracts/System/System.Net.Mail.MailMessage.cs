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

#if !SILVERLIGHT

using System;
using System.Collections.Specialized;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Net.Mail
{
  // Summary:
  //     Describes the delivery notification options for e-mail.
  public enum DeliveryNotificationOptions
  {
    // Summary:
    //     No notification.
    None = 0,
    //
    // Summary:
    //     Notify if the delivery is successful.
    OnSuccess = 1,
    //
    // Summary:
    //     Notify if the delivery is unsuccessful.
    OnFailure = 2,
    //
    // Summary:
    //     Notify if the delivery is delayed
    Delay = 4,
    //
    // Summary:
    //     Never notify.
    Never = 134217728,
  }
  // Summary:
  //     Represents an e-mail message that can be sent using the System.Net.Mail.SmtpClient
  //     class.
  public class MailMessage 
  {
    // Summary:
    //     Initializes an empty instance of the System.Net.Mail.MailMessage class.
    extern public MailMessage();
    //
    // Summary:
    //     Initializes a new instance of the System.Net.Mail.MailMessage class by using
    //     the specified System.Net.Mail.MailAddress class objects.
    //
    // Parameters:
    //   from:
    //     A System.Net.Mail.MailAddress that contains the address of the sender of
    //     the e-mail message.
    //
    //   to:
    //     A System.Net.Mail.MailAddress that contains the address of the recipient
    //     of the e-mail message.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     from is null.  -or- to is null.
    //
    //   System.FormatException:
    //     from or to is malformed.
    public MailMessage(MailAddress from, MailAddress to)
    {
      Contract.Requires(from != null);
      Contract.Requires(to != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Net.Mail.MailMessage class by using
    //     the specified System.String class objects.
    //
    // Parameters:
    //   from:
    //     A System.String that contains the address of the sender of the e-mail message.
    //
    //   to:
    //     A System.String that contains the addresses of the recipients of the e-mail
    //     message.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     from is null.  -or- to is null.
    //
    //   System.ArgumentException:
    //     from is System.String.Empty ("").  -or- to is System.String.Empty ("").
    //
    //   System.FormatException:
    //     from or to is malformed.
    public MailMessage(string from, string to)
    {
      Contract.Requires(!String.IsNullOrEmpty(from));
      Contract.Requires(!String.IsNullOrEmpty(to));
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Net.Mail.MailMessage class.
    //
    // Parameters:
    //   from:
    //     A System.String that contains the address of the sender of the e-mail message.
    //
    //   to:
    //     A System.String that contains the address of the recipient of the e-mail
    //     message.
    //
    //   subject:
    //     A System.String that contains the subject text.
    //
    //   body:
    //     A System.String that contains the message body.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     from is null.  -or- to is null.
    //
    //   System.ArgumentException:
    //     from is System.String.Empty ("").  -or- to is System.String.Empty ("").
    //
    //   System.FormatException:
    //     from or to is malformed.
    public MailMessage(string from, string to, string subject, string body)
    {
      Contract.Requires(!String.IsNullOrEmpty(from));
      Contract.Requires(!String.IsNullOrEmpty(to));
    }

    // Summary:
    //     Gets the attachment collection used to store alternate forms of the message
    //     body.
    //
    // Returns:
    //     A writable System.Net.Mail.AttachmentCollection.
    public AlternateViewCollection AlternateViews
    {
      get
      {
        Contract.Ensures(Contract.Result<AlternateViewCollection>() != null);
        return default(AlternateViewCollection);
      }
    }

    //
    // Summary:
    //     Gets the attachment collection used to store data attached to this e-mail
    //     message.
    //
    // Returns:
    //     A writable System.Net.Mail.AttachmentCollection.
    public AttachmentCollection Attachments
    {
      get
      {
        Contract.Ensures(Contract.Result<AttachmentCollection>() != null);
        return default(AttachmentCollection);
      }
    }
    //
    // Summary:
    //     Gets the address collection that contains the blind carbon copy (BCC) recipients
    //     for this e-mail message.
    //
    // Returns:
    //     A writable System.Net.Mail.MailAddressCollection object.
    public MailAddressCollection Bcc
    {
      get
      {
        Contract.Ensures(Contract.Result<MailAddressCollection>() != null);
        return default(MailAddressCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets the message body.
    //
    // Returns:
    //     A System.String value that contains the body text.
    public string Body
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set { }
    }
  
    //
    // Summary:
    //     Gets or sets the encoding used to encode the message body.
    //
    // Returns:
    //     An System.Text.Encoding applied to the contents of the System.Net.Mail.MailMessage.Body.
    extern public Encoding BodyEncoding { get; set; }
    //
    // Summary:
    //     Gets the address collection that contains the carbon copy (CC) recipients
    //     for this e-mail message.
    //
    // Returns:
    //     A writable System.Net.Mail.MailAddressCollection object.
    public MailAddressCollection CC
    {
      get
      {
        Contract.Ensures(Contract.Result<MailAddressCollection>() != null);
        return default(MailAddressCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets the delivery notifications for this e-mail message.
    //
    // Returns:
    //     A System.Net.Mail.DeliveryNotificationOptions value that contains the delivery
    //     notifications for this message.
    extern public DeliveryNotificationOptions DeliveryNotificationOptions { get; set; }
    //
    // Summary:
    //     Gets or sets the from address for this e-mail message.
    //
    // Returns:
    //     A System.Net.Mail.MailAddress that contains the from address information.
    public MailAddress From
    {
      get
      {
        Contract.Ensures(Contract.Result<MailAddress>() != null);
        return default(MailAddress);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }
    //
    // Summary:
    //     Gets the e-mail headers that are transmitted with this e-mail message.
    //
    // Returns:
    //     A System.Collections.Specialized.NameValueCollection that contains the e-mail
    //     headers.
    public NameValueCollection Headers
    {
      get
      {
        Contract.Ensures(Contract.Result<NameValueCollection>() != null);
        return default(NameValueCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets a value indicating whether the mail message body is in Html.
    //
    // Returns:
    //     true if the message body is in Html; else false. The default is false.
    extern public bool IsBodyHtml { get; set; }
    //
    // Summary:
    //     Gets or sets the priority of this e-mail message.
    //
    // Returns:
    //     A System.Net.Mail.MailPriority that contains the priority of this message.
#if false
    public MailPriority Priority { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets the ReplyTo address for the mail message.
    //
    // Returns:
    //     A MailAddress that indicates the value of the System.Net.Mail.MailMessage.ReplyTo
    //     field.
    extern public MailAddress ReplyTo { get; set; }
    //
    // Summary:
    //     Gets or sets the sender's address for this e-mail message.
    //
    // Returns:
    //     A System.Net.Mail.MailAddress that contains the sender's address information.
    extern public MailAddress Sender { get; set; }
    //
    // Summary:
    //     Gets or sets the subject line for this e-mail message.
    //
    // Returns:
    //     A System.String that contains the subject content.
    extern public string Subject { get; set; }
    //
    // Summary:
    //     Gets or sets the encoding used for the subject content for this e-mail message.
    //
    // Returns:
    //     An System.Text.Encoding that was used to encode the System.Net.Mail.MailMessage.Subject
    //     property.
    extern public Encoding SubjectEncoding { get; set; }
    //
    // Summary:
    //     Gets the address collection that contains the recipients of this e-mail message.
    //
    // Returns:
    //     A writable System.Net.Mail.MailAddressCollection object.
    public MailAddressCollection To
    {
      get
      {
        Contract.Ensures(Contract.Result<MailAddressCollection>() != null);
        return default(MailAddressCollection);
      }
    }
  }
}

#endif
