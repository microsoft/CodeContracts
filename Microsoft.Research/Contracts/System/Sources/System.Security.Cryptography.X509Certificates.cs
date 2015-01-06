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

// File System.Security.Cryptography.X509Certificates.cs
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


namespace System.Security.Cryptography.X509Certificates
{
  public enum OpenFlags
  {
    ReadOnly = 0, 
    ReadWrite = 1, 
    MaxAllowed = 2, 
    OpenExistingOnly = 4, 
    IncludeArchived = 8, 
  }

  public enum StoreLocation
  {
    CurrentUser = 1, 
    LocalMachine = 2, 
  }

  public enum StoreName
  {
    AddressBook = 1, 
    AuthRoot = 2, 
    CertificateAuthority = 3, 
    Disallowed = 4, 
    My = 5, 
    Root = 6, 
    TrustedPeople = 7, 
    TrustedPublisher = 8, 
  }

  public enum X500DistinguishedNameFlags
  {
    None = 0, 
    Reversed = 1, 
    UseSemicolons = 16, 
    DoNotUsePlusSign = 32, 
    DoNotUseQuotes = 64, 
    UseCommas = 128, 
    UseNewLines = 256, 
    UseUTF8Encoding = 4096, 
    UseT61Encoding = 8192, 
    ForceUTF8Encoding = 16384, 
  }

  public enum X509ChainStatusFlags
  {
    NoError = 0, 
    NotTimeValid = 1, 
    NotTimeNested = 2, 
    Revoked = 4, 
    NotSignatureValid = 8, 
    NotValidForUsage = 16, 
    UntrustedRoot = 32, 
    RevocationStatusUnknown = 64, 
    Cyclic = 128, 
    InvalidExtension = 256, 
    InvalidPolicyConstraints = 512, 
    InvalidBasicConstraints = 1024, 
    InvalidNameConstraints = 2048, 
    HasNotSupportedNameConstraint = 4096, 
    HasNotDefinedNameConstraint = 8192, 
    HasNotPermittedNameConstraint = 16384, 
    HasExcludedNameConstraint = 32768, 
    PartialChain = 65536, 
    CtlNotTimeValid = 131072, 
    CtlNotSignatureValid = 262144, 
    CtlNotValidForUsage = 524288, 
    OfflineRevocation = 16777216, 
    NoIssuanceChainPolicy = 33554432, 
  }

  public enum X509FindType
  {
    FindByThumbprint = 0, 
    FindBySubjectName = 1, 
    FindBySubjectDistinguishedName = 2, 
    FindByIssuerName = 3, 
    FindByIssuerDistinguishedName = 4, 
    FindBySerialNumber = 5, 
    FindByTimeValid = 6, 
    FindByTimeNotYetValid = 7, 
    FindByTimeExpired = 8, 
    FindByTemplateName = 9, 
    FindByApplicationPolicy = 10, 
    FindByCertificatePolicy = 11, 
    FindByExtension = 12, 
    FindByKeyUsage = 13, 
    FindBySubjectKeyIdentifier = 14, 
  }

  public enum X509IncludeOption
  {
    None = 0, 
    ExcludeRoot = 1, 
    EndCertOnly = 2, 
    WholeChain = 3, 
  }

  public enum X509KeyUsageFlags
  {
    None = 0, 
    EncipherOnly = 1, 
    CrlSign = 2, 
    KeyCertSign = 4, 
    KeyAgreement = 8, 
    DataEncipherment = 16, 
    KeyEncipherment = 32, 
    NonRepudiation = 64, 
    DigitalSignature = 128, 
    DecipherOnly = 32768, 
  }

  public enum X509NameType
  {
    SimpleName = 0, 
    EmailName = 1, 
    UpnName = 2, 
    DnsName = 3, 
    DnsFromAlternativeName = 4, 
    UrlName = 5, 
  }

  public enum X509RevocationFlag
  {
    EndCertificateOnly = 0, 
    EntireChain = 1, 
    ExcludeRoot = 2, 
  }

  public enum X509RevocationMode
  {
    NoCheck = 0, 
    Online = 1, 
    Offline = 2, 
  }

  public enum X509SubjectKeyIdentifierHashAlgorithm
  {
    Sha1 = 0, 
    ShortSha1 = 1, 
    CapiSha1 = 2, 
  }

  public enum X509VerificationFlags
  {
    NoFlag = 0, 
    IgnoreNotTimeValid = 1, 
    IgnoreCtlNotTimeValid = 2, 
    IgnoreNotTimeNested = 4, 
    IgnoreInvalidBasicConstraints = 8, 
    AllowUnknownCertificateAuthority = 16, 
    IgnoreWrongUsage = 32, 
    IgnoreInvalidName = 64, 
    IgnoreInvalidPolicy = 128, 
    IgnoreEndRevocationUnknown = 256, 
    IgnoreCtlSignerRevocationUnknown = 512, 
    IgnoreCertificateAuthorityRevocationUnknown = 1024, 
    IgnoreRootRevocationUnknown = 2048, 
    AllFlags = 4095, 
  }
}
