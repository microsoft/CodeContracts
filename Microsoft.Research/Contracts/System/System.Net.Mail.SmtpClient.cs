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
using System.ComponentModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;

namespace System.Net.Mail
{

  public enum SmtpDeliveryMethod
  {
    Network,
    SpecifiedPickupDirectory,
    PickupDirectoryFromIis
  }

 

 

  // Summary:
  //     Allows applications to send e-mail by using the Simple Mail Transfer Protocol
  //     (SMTP).
  public class SmtpClient
  {
    // Summary:
    //     Initializes a new instance of the System.Net.Mail.SmtpClient class by using
    //     configuration file settings.
    extern public SmtpClient();
    //
    // Summary:
    //     Initializes a new instance of the System.Net.Mail.SmtpClient class that sends
    //     e-mail by using the specified SMTP server.
    //
    // Parameters:
    //   host:
    //     A System.String that contains the name or IP address of the host computer
    //     used for SMTP transactions.
    extern public SmtpClient(string host);
    //
    // Summary:
    //     Initializes a new instance of the System.Net.Mail.SmtpClient class that sends
    //     e-mail by using the specified SMTP server and port.
    //
    // Parameters:
    //   host:
    //     A System.String that contains the name or IP address of the host used for
    //     SMTP transactions.
    //
    //   port:
    //     An System.Int32 greater than zero that contains the port to be used on host.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     port cannot be less than zero.
    public SmtpClient(string host, int port)
    {
      Contract.Requires(port >= 0);
      Contract.Requires(port <= 0xFFFF);

    }

    // Summary:
    //     Specify which certificates should be used to establish the Secure Sockets
    //     Layer (SSL) connection.
    //
    // Returns:
    //     An System.Security.Cryptography.X509Certificates.X509CertificateCollection,
    //     holding one or more client certificates. The default value is derived from
    //     the mail configuration attributes in a configuration file.
    public X509CertificateCollection ClientCertificates
    {
      get
      {
        Contract.Ensures(Contract.Result<X509CertificateCollection>() != null);
        return default(X509CertificateCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets the credentials used to authenticate the sender.
    //
    // Returns:
    //     An System.Net.ICredentialsByHost that represents the credentials to use for
    //     authentication; or null if no credentials have been specified.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     You cannot change the value of this property when an email is being sent.
#if false
    public ICredentialsByHost Credentials { get; set; }
#endif
    //
    // Summary:
    //     Specifies how outgoing email messages will be handled.
    //
    // Returns:
    //     An System.Net.Mail.SmtpDeliveryMethod that indicates how email messages are
    //     delivered.
    extern public SmtpDeliveryMethod DeliveryMethod { get; set; }
    //
    // Summary:
    //     Specify whether the System.Net.Mail.SmtpClient uses Secure Sockets Layer
    //     (SSL) to encrypt the connection.
    //
    // Returns:
    //     true if the System.Net.Mail.SmtpClient uses SSL; otherwise, false. The default
    //     is false.
    extern public bool EnableSsl { get; set; }
    //
    // Summary:
    //     Gets or sets the name or IP address of the host used for SMTP transactions.
    //
    // Returns:
    //     A System.String that contains the name or IP address of the computer to use
    //     for SMTP transactions.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value specified for a set operation is null.
    //
    //   System.ArgumentException:
    //     The value specified for a set operation is equal to System.String.Empty ("").
    //
    //   System.InvalidOperationException:
    //     You cannot change the value of this property when an email is being sent.
    public string Host
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }
    //
    // Summary:
    //     Gets or sets the folder where applications save mail messages to be processed
    //     by the local SMTP server.
    //
    // Returns:
    //     A System.String that specifies the pickup directory for mail messages.
    extern public string PickupDirectoryLocation { get; set; }
    //
    // Summary:
    //     Gets or sets the port used for SMTP transactions.
    //
    // Returns:
    //     An System.Int32 that contains the port number on the SMTP host. The default
    //     value is 25.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value specified for a set operation is less than or equal to zero.
    //
    //   System.InvalidOperationException:
    //     You cannot change the value of this property when an email is being sent.
    public int Port
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 0xFFFF);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 0xFFFF);
      }
    }
    //
    // Summary:
    //     Gets the network connection used to transmit the e-mail message.
    //
    // Returns:
    //     A System.Net.ServicePoint that connects to the System.Net.Mail.SmtpClient.Host
    //     property used for SMTP.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     System.Net.Mail.SmtpClient.Host is null or the empty string ("").  -or- System.Net.Mail.SmtpClient.Port
    //     is zero.
    public ServicePoint ServicePoint
    {
      get
      {
        Contract.Ensures(Contract.Result<ServicePoint>() != null);
        return default(ServicePoint);
      }
    }
    //
    // Summary:
    //     Gets or sets a value that specifies the amount of time after which a synchronous
    //     Overload:System.Net.Mail.SmtpClient.Send call times out.
    //
    // Returns:
    //     An System.Int32 that specifies the time-out value in milliseconds. The default
    //     value is 100,000 (100 seconds).
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value specified for a set operation was less than zero.
    //
    //   System.InvalidOperationException:
    //     You cannot change the value of this property when an email is being sent.
    public int Timeout
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }
    //
    // Summary:
    //     Gets or sets a System.Boolean value that controls whether the System.Net.CredentialCache.DefaultCredentials
    //     are sent with requests.
    //
    // Returns:
    //     true if the default credentials are used; otherwise false. The default value
    //     is false.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     You cannot change the value of this property when an e-mail is being sent.
    extern public bool UseDefaultCredentials { get; set; }

    // Summary:
    //     Occurs when an asynchronous e-mail send operation completes.
#if false
    extern public event SendCompletedEventHandler SendCompleted;
#endif
    // Summary:
    //     Raises the System.Net.Mail.SmtpClient.SendCompleted event.
    //
    // Parameters:
    //   e:
    //     An System.ComponentModel.AsyncCompletedEventArgs that contains event data.
#if false
    extern protected void OnSendCompleted(AsyncCompletedEventArgs e);
#endif
    //
    // Summary:
    //     Sends the specified message to an SMTP server for delivery.
    //
    // Parameters:
    //   message:
    //     A System.Net.Mail.MailMessage that contains the message to send.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     System.Net.Mail.MailMessage.From is null.  -or- System.Net.Mail.MailMessage.To
    //     is null.  -or- message is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     There are no recipients in System.Net.Mail.MailMessage.To, System.Net.Mail.MailMessage.CC,
    //     and System.Net.Mail.MailMessage.Bcc.
    //
    //   System.InvalidOperationException:
    //     This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync
    //     call in progress.  -or- System.Net.Mail.SmtpClient.Host is null.  -or- System.Net.Mail.SmtpClient.Host
    //     is equal to the empty string ("").  -or- System.Net.Mail.SmtpClient.Port
    //     is zero.
    //
    //   System.ObjectDisposedException:
    //     This object has been disposed.
    //
    //   System.Net.Mail.SmtpException:
    //     The connection to the SMTP server failed.  -or- Authentication failed.  -or-
    //     The operation timed out.
    //
    //   System.Net.Mail.SmtpFailedRecipientsException:
    //     The message could not be delivered to one or more of the recipients in System.Net.Mail.MailMessage.To,
    //     System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.
    public void Send(MailMessage message)
    {
      Contract.Requires(message != null);
    }
    //
    // Summary:
    //     Sends the specified e-mail message to an SMTP server for delivery. The message
    //     sender, recipients, subject, and message body are specified using System.String
    //     objects.
    //
    // Parameters:
    //   from:
    //     A System.String that contains the address information of the message sender.
    //
    //   recipients:
    //     A System.String that contains the addresses that the message is sent to.
    //
    //   subject:
    //     A System.String that contains the subject line for the message.
    //
    //   body:
    //     A System.String that contains the message body.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     from is null.  -or- recipient is null.
    //
    //   System.ArgumentException:
    //     from is System.String.Empty.  -or- recipient is System.String.Empty.
    //
    //   System.InvalidOperationException:
    //     This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync
    //     call in progress.  -or- System.Net.Mail.SmtpClient.Host is null.  -or- System.Net.Mail.SmtpClient.Host
    //     is equal to the empty string ("").  -or- System.Net.Mail.SmtpClient.Port
    //     is zero.
    //
    //   System.ObjectDisposedException:
    //     This object has been disposed.
    //
    //   System.Net.Mail.SmtpException:
    //     The connection to the SMTP server failed.  -or- Authentication failed.  -or-
    //     The operation timed out.
    //
    //   System.Net.Mail.SmtpFailedRecipientsException:
    //     The message could not be delivered to one or more of the recipients in recipients.
    public void Send(string from, string recipients, string subject, string body)
    {
      Contract.Requires(!String.IsNullOrEmpty(from));
      Contract.Requires(!String.IsNullOrEmpty(recipients));
    }
    //
    // Summary:
    //     Sends the specified e-mail message to an SMTP server for delivery. This method
    //     does not block the calling thread and allows the caller to pass an object
    //     to the method that is invoked when the operation completes.
    //
    // Parameters:
    //   message:
    //     A System.Net.Mail.MailMessage that contains the message to send.
    //
    //   userToken:
    //     A user-defined object that is passed to the method invoked when the asynchronous
    //     operation completes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     System.Net.Mail.MailMessage.From is null.  -or- System.Net.Mail.MailMessage.To
    //     is null.  -or- message is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     There are no recipients in System.Net.Mail.MailMessage.To, System.Net.Mail.MailMessage.CC,
    //     and System.Net.Mail.MailMessage.Bcc.
    //
    //   System.InvalidOperationException:
    //     This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync
    //     call in progress.  -or- System.Net.Mail.SmtpClient.Host is null.  -or- System.Net.Mail.SmtpClient.Host
    //     is equal to the empty string ("").  -or- System.Net.Mail.SmtpClient.Port
    //     is zero.
    //
    //   System.ObjectDisposedException:
    //     This object has been disposed.
    //
    //   System.Net.Mail.SmtpException:
    //     The connection to the SMTP server failed.  -or- Authentication failed.  -or-
    //     The operation timed out.
    //
    //   System.Net.Mail.SmtpFailedRecipientsException:
    //     The message could not be delivered to one or more of the recipients in System.Net.Mail.MailMessage.To,
    //     System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.
    public void SendAsync(MailMessage message, object userToken)
    {
      Contract.Requires(message != null);
    }
    //
    // Summary:
    //     Sends an e-mail message to an SMTP server for delivery. The message sender,
    //     recipients, subject, and message body are specified using System.String objects.
    //     This method does not block the calling thread and allows the caller to pass
    //     an object to the method that is invoked when the operation completes.
    //
    // Parameters:
    //   from:
    //     A System.String that contains the address information of the message sender.
    //
    //   recipients:
    //     A System.String that contains the address that the message is sent to.
    //
    //   subject:
    //     A System.String that contains the subject line for the message.
    //
    //   body:
    //     A System.String that contains the message body.
    //
    //   userToken:
    //     A user-defined object that is passed to the method invoked when the asynchronous
    //     operation completes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     from is null.  -or- recipient is null.
    //
    //   System.ArgumentException:
    //     from is System.String.Empty.  -or- recipient is System.String.Empty.
    //
    //   System.InvalidOperationException:
    //     This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync
    //     call in progress.  -or- System.Net.Mail.SmtpClient.Host is null.  -or- System.Net.Mail.SmtpClient.Host
    //     is equal to the empty string ("").  -or- System.Net.Mail.SmtpClient.Port
    //     is zero.
    //
    //   System.ObjectDisposedException:
    //     This object has been disposed.
    //
    //   System.Net.Mail.SmtpException:
    //     The connection to the SMTP server failed.  -or- Authentication failed.  -or-
    //     The operation timed out.
    //
    //   System.Net.Mail.SmtpFailedRecipientsException:
    //     The message could not be delivered to one or more of the recipients in recipients.
    public void SendAsync(string from, string recipients, string subject, string body, object userToken)
    {
      Contract.Requires(!String.IsNullOrEmpty(from));
      Contract.Requires(!String.IsNullOrEmpty(recipients));
    }
    //
    // Summary:
    //     Cancels an asynchronous operation to send an e-mail message.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     This object has been disposed.
    extern public void SendAsyncCancel();
  }
}

#endif
