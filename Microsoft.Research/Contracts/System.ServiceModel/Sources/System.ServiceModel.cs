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

// File System.ServiceModel.cs
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


namespace System.ServiceModel
{
  public enum AddressFilterMode
  {
    Exact = 0, 
    Prefix = 1, 
    Any = 2, 
  }

  public enum AuditLevel
  {
    None = 0, 
    Success = 1, 
    Failure = 2, 
    SuccessOrFailure = 3, 
  }

  public enum AuditLogLocation
  {
    Default = 0, 
    Application = 1, 
    Security = 2, 
  }

  public enum BasicHttpMessageCredentialType
  {
    UserName = 0, 
    Certificate = 1, 
  }

  public enum BasicHttpSecurityMode
  {
    None = 0, 
    Transport = 1, 
    Message = 2, 
    TransportWithMessageCredential = 3, 
    TransportCredentialOnly = 4, 
  }

  public enum CommunicationState
  {
    Created = 0, 
    Opening = 1, 
    Opened = 2, 
    Closing = 3, 
    Closed = 4, 
    Faulted = 5, 
  }

  public enum ConcurrencyMode
  {
    Single = 0, 
    Reentrant = 1, 
    Multiple = 2, 
  }

  public enum DeadLetterQueue
  {
    None = 0, 
    System = 1, 
    Custom = 2, 
  }

  public enum HostNameComparisonMode
  {
    StrongWildcard = 0, 
    Exact = 1, 
    WeakWildcard = 2, 
  }

  public enum HttpClientCredentialType
  {
    None = 0, 
    Basic = 1, 
    Digest = 2, 
    Ntlm = 3, 
    Windows = 4, 
    Certificate = 5, 
  }

  public enum HttpProxyCredentialType
  {
    None = 0, 
    Basic = 1, 
    Digest = 2, 
    Ntlm = 3, 
    Windows = 4, 
  }

  public enum ImpersonationOption
  {
    NotAllowed = 0, 
    Allowed = 1, 
    Required = 2, 
  }

  public enum InstanceContextMode
  {
    PerSession = 0, 
    PerCall = 1, 
    Single = 2, 
  }

  public enum MessageCredentialType
  {
    None = 0, 
    Windows = 1, 
    UserName = 2, 
    Certificate = 3, 
    IssuedToken = 4, 
  }

  public enum MsmqAuthenticationMode
  {
    None = 0, 
    WindowsDomain = 1, 
    Certificate = 2, 
  }

  public enum MsmqEncryptionAlgorithm
  {
    RC4Stream = 0, 
    Aes = 1, 
  }

  public enum MsmqSecureHashAlgorithm
  {
    MD5 = 0, 
    Sha1 = 1, 
    Sha256 = 2, 
    Sha512 = 3, 
  }

  public enum NetMsmqSecurityMode
  {
    None = 0, 
    Transport = 1, 
    Message = 2, 
    Both = 3, 
  }

  public enum NetNamedPipeSecurityMode
  {
    None = 0, 
    Transport = 1, 
  }

  public enum OperationFormatStyle
  {
    Document = 0, 
    Rpc = 1, 
  }

  public enum OperationFormatUse
  {
    Literal = 0, 
    Encoded = 1, 
  }

  public enum PeerMessageOrigination
  {
    Local = 0, 
    Remote = 1, 
  }

  public enum PeerMessagePropagation
  {
    None = 0, 
    Local = 1, 
    Remote = 2, 
    LocalAndRemote = 3, 
  }

  public enum PeerTransportCredentialType
  {
    Password = 0, 
    Certificate = 1, 
  }

  public enum QueuedDeliveryRequirementsMode
  {
    Allowed = 0, 
    Required = 1, 
    NotAllowed = 2, 
  }

  public enum QueueTransferProtocol
  {
    Native = 0, 
    Srmp = 1, 
    SrmpSecure = 2, 
  }

  public enum ReceiveErrorHandling
  {
    Fault = 0, 
    Drop = 1, 
    Reject = 2, 
    Move = 3, 
  }

  public enum ReleaseInstanceMode
  {
    None = 0, 
    BeforeCall = 1, 
    AfterCall = 2, 
    BeforeAndAfterCall = 3, 
  }

  public enum SecurityMode
  {
    None = 0, 
    Transport = 1, 
    Message = 2, 
    TransportWithMessageCredential = 3, 
  }

  public enum SessionMode
  {
    Allowed = 0, 
    Required = 1, 
    NotAllowed = 2, 
  }

  public enum TcpClientCredentialType
  {
    None = 0, 
    Windows = 1, 
    Certificate = 2, 
  }

  public enum TransactionFlowOption
  {
    NotAllowed = 0, 
    Allowed = 1, 
    Mandatory = 2, 
  }

  public enum TransferMode
  {
    Buffered = 0, 
    Streamed = 1, 
    StreamedRequest = 2, 
    StreamedResponse = 3, 
  }

  public enum WSDualHttpSecurityMode
  {
    None = 0, 
    Message = 1, 
  }

  public enum WSFederationHttpSecurityMode
  {
    None = 0, 
    Message = 1, 
    TransportWithMessageCredential = 2, 
  }

  public enum WSMessageEncoding
  {
    Text = 0, 
    Mtom = 1, 
  }
}
