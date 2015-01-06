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

// File System.Security.Cryptography.cs
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


namespace System.Security.Cryptography
{
  public enum CngExportPolicies
  {
    None = 0, 
    AllowExport = 1, 
    AllowPlaintextExport = 2, 
    AllowArchiving = 4, 
    AllowPlaintextArchiving = 8, 
  }

  public enum CngKeyCreationOptions
  {
    None = 0, 
    MachineKey = 32, 
    OverwriteExistingKey = 128, 
  }

  public enum CngKeyHandleOpenOptions
  {
    None = 0, 
    EphemeralKey = 1, 
  }

  public enum CngKeyOpenOptions
  {
    None = 0, 
    UserKey = 0, 
    MachineKey = 32, 
    Silent = 64, 
  }

  public enum CngKeyUsages
  {
    None = 0, 
    Decryption = 1, 
    Signing = 2, 
    KeyAgreement = 4, 
    AllUsages = 16777215, 
  }

  public enum CngPropertyOptions
  {
    None = 0, 
    CustomProperty = 1073741824, 
    Persist = -2147483648, 
  }

  public enum CngUIProtectionLevels
  {
    None = 0, 
    ProtectKey = 1, 
    ForceHighProtection = 2, 
  }

  public enum ECDiffieHellmanKeyDerivationFunction
  {
    Hash = 0, 
    Hmac = 1, 
    Tls = 2, 
  }

  public enum ECKeyXmlFormat
  {
    Rfc4050 = 0, 
  }

  public enum SignatureVerificationResult
  {
    Valid = 0, 
    AssemblyIdentityMismatch = 1, 
    ContainingSignatureInvalid = 2, 
    PublicKeyTokenMismatch = 3, 
    PublisherMismatch = 4, 
    SystemError = -2146869247, 
    InvalidSignerCertificate = -2146869246, 
    InvalidCountersignature = -2146869245, 
    InvalidCertificateSignature = -2146869244, 
    InvalidTimestamp = -2146869243, 
    BadDigest = -2146869232, 
    BasicConstraintsNotObserved = -2146869223, 
    UnknownTrustProvider = -2146762751, 
    UnknownVerificationAction = -2146762750, 
    BadSignatureFormat = -2146762749, 
    CertificateNotExplicitlyTrusted = -2146762748, 
    MissingSignature = -2146762496, 
    CertificateExpired = -2146762495, 
    InvalidTimePeriodNesting = -2146762494, 
    InvalidCertificateRole = -2146762493, 
    PathLengthConstraintViolated = -2146762492, 
    UnknownCriticalExtension = -2146762491, 
    CertificateUsageNotAllowed = -2146762490, 
    IssuerChainingError = -2146762489, 
    CertificateMalformed = -2146762488, 
    UntrustedRootCertificate = -2146762487, 
    CouldNotBuildChain = -2146762486, 
    GenericTrustFailure = -2146762485, 
    CertificateRevoked = -2146762484, 
    UntrustedTestRootCertificate = -2146762483, 
    RevocationCheckFailure = -2146762482, 
    InvalidCertificateUsage = -2146762480, 
    CertificateExplicitlyDistrusted = -2146762479, 
    UntrustedCertificationAuthority = -2146762478, 
    InvalidCertificatePolicy = -2146762477, 
    InvalidCertificateName = -2146762476, 
  }
}
