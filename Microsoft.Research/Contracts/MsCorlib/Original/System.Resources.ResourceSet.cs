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

namespace System.Resources
{

    public class ResourceSet
    {

        public object GetObject (string! name, bool ignoreCase) {
            CodeContract.Requires(name != null);

          return default(object);
        }
        public object GetObject (string! name) {
            CodeContract.Requires(name != null);

          return default(object);
        }
        public string GetString (string! name, bool ignoreCase) {
            CodeContract.Requires(name != null);

          return default(string);
        }
        public string GetString (string! name) {
            CodeContract.Requires(name != null);

          return default(string);
        }
        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public System.Collections.IDictionaryEnumerator GetEnumerator () {
            CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<System.Collections.IDictionaryEnumerator>() != null);
          return default(System.Collections.IDictionaryEnumerator);
        }
        public Type GetDefaultWriter () {

          return default(Type);
        }
        public Type GetDefaultReader () {

          return default(Type);
        }
        public void Dispose () {

        }
        public void Close () {

        }
        public ResourceSet (IResourceReader! reader) {
            CodeContract.Requires(reader != null);

          return default(ResourceSet);
        }
        public ResourceSet (System.IO.Stream stream) {

          return default(ResourceSet);
        }
        public ResourceSet (string fileName) {
          return default(ResourceSet);
        }
    }
}
