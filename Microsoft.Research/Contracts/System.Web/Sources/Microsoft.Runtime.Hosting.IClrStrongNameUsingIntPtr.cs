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

// File Microsoft.Runtime.Hosting.IClrStrongNameUsingIntPtr.cs
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


namespace Microsoft.Runtime.Hosting
{
  internal partial interface IClrStrongNameUsingIntPtr
  {
    #region Methods and constructors
    int GetHashFromAssemblyFile(string pszFilePath, out int piHashAlg, out byte[] pbHash, int cchHash, out int pchHash);

    int GetHashFromAssemblyFileW(string pwzFilePath, out int piHashAlg, byte[] pbHash, int cchHash, out int pchHash);

    int GetHashFromBlob(IntPtr pbBlob, int cchBlob, out int piHashAlg, byte[] pbHash, int cchHash, out int pchHash);

    int GetHashFromFile(string pszFilePath, out int piHashAlg, byte[] pbHash, int cchHash, out int pchHash);

    int GetHashFromFileW(string pwzFilePath, out int piHashAlg, byte[] pbHash, int cchHash, out int pchHash);

    int GetHashFromHandle(IntPtr hFile, out int piHashAlg, byte[] pbHash, int cchHash, out int pchHash);

    int StrongNameCompareAssemblies(string pwzAssembly1, string pwzAssembly2, out int dwResult);

    int StrongNameFreeBuffer(IntPtr pbMemory);

    int StrongNameGetBlob(string pwzFilePath, byte[] pbBlob, out int pcbBlob);

    int StrongNameGetBlobFromImage(IntPtr pbBase, int dwLength, byte[] pbBlob, out int pcbBlob);

    int StrongNameGetPublicKey(string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob, out IntPtr ppbPublicKeyBlob, out int pcbPublicKeyBlob);

    int StrongNameHashSize(int ulHashAlg, out int cbSize);

    int StrongNameKeyDelete(string pwzKeyContainer);

    int StrongNameKeyGen(string pwzKeyContainer, int dwFlags, out IntPtr ppbKeyBlob, out int pcbKeyBlob);

    int StrongNameKeyGenEx(string pwzKeyContainer, int dwFlags, int dwKeySize, out IntPtr ppbKeyBlob, out int pcbKeyBlob);

    int StrongNameKeyInstall(string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob);

    int StrongNameSignatureGeneration(string pwzFilePath, string pwzKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob, IntPtr ppbSignatureBlob, out int pcbSignatureBlob);

    int StrongNameSignatureGenerationEx(string wszFilePath, string wszKeyContainer, IntPtr pbKeyBlob, int cbKeyBlob, IntPtr ppbSignatureBlob, out int pcbSignatureBlob, int dwFlags);

    int StrongNameSignatureSize(IntPtr pbPublicKeyBlob, int cbPublicKeyBlob, out int pcbSize);

    int StrongNameSignatureVerification(string pwzFilePath, int dwInFlags, out int dwOutFlags);

    int StrongNameSignatureVerificationEx(string pwzFilePath, bool fForceVerification, out bool fWasVerified);

    int StrongNameSignatureVerificationFromImage(IntPtr pbBase, int dwLength, int dwInFlags, out int dwOutFlags);

    int StrongNameTokenFromAssembly(string pwzFilePath, out IntPtr ppbStrongNameToken, out int pcbStrongNameToken);

    int StrongNameTokenFromAssemblyEx(string pwzFilePath, out IntPtr ppbStrongNameToken, out int pcbStrongNameToken, out IntPtr ppbPublicKeyBlob, out int pcbPublicKeyBlob);

    int StrongNameTokenFromPublicKey(IntPtr pbPublicKeyBlob, int cbPublicKeyBlob, out IntPtr ppbStrongNameToken, out int pcbStrongNameToken);
    #endregion
  }
}
