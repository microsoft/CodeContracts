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

using System;
using System.Diagnostics.Contracts;

namespace System.IO.IsolatedStorage
{

    public class IsolatedStorageFile
    {

        public UInt64 CurrentSize
        {
          get;
        }

        public UInt64 MaximumSize
        {
          get;
        }

        [Pure] [GlobalAccess(false)]
        public static System.Collections.IEnumerator GetEnumerator ([Escapes(true,false)] IsolatedStorageScope scope) {
            CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<System.Collections.IEnumerator>() != null);
          return default(System.Collections.IEnumerator);
        }
        public static void Remove (IsolatedStorageScope scope) {

        }
        public void Dispose () {

        }
        public void Close () {

        }
        public void Remove () {

        }
        public String[] GetDirectoryNames (string! searchPattern) {
            CodeContract.Requires(searchPattern != null);

          return default(String[]);
        }
        public String[] GetFileNames (string! searchPattern) {
            CodeContract.Requires(searchPattern != null);

          return default(String[]);
        }
        public void DeleteDirectory (string! dir) {
            CodeContract.Requires(dir != null);

        }
        public void CreateDirectory (string! dir) {
            CodeContract.Requires(dir != null);

        }
        public void DeleteFile (string! file) {
            CodeContract.Requires(file != null);

        }
        public static IsolatedStorageFile GetStore (IsolatedStorageScope scope, System.Security.Policy.Evidence! domainEvidence, Type domainEvidenceType, System.Security.Policy.Evidence! assemblyEvidence, Type assemblyEvidenceType) {
            CodeContract.Requires(domainEvidence != null);
            CodeContract.Requires(assemblyEvidence != null);

          return default(IsolatedStorageFile);
        }
        public static IsolatedStorageFile GetStore (IsolatedStorageScope scope, object! domainIdentity, object! assemblyIdentity) {
            CodeContract.Requires(domainIdentity != null);
            CodeContract.Requires(assemblyIdentity != null);

          return default(IsolatedStorageFile);
        }
        public static IsolatedStorageFile GetStore (IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType) {

          return default(IsolatedStorageFile);
        }
        public static IsolatedStorageFile GetUserStoreForAssembly () {

          return default(IsolatedStorageFile);
        }
        public static IsolatedStorageFile GetUserStoreForDomain () {
          return default(IsolatedStorageFile);
        }
    }
}
