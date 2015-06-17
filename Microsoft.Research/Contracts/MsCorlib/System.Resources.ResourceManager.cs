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

using System.Diagnostics.Contracts;
using System;

namespace System.Resources
{

    public class ResourceManager
    {

        public bool IgnoreCase
        {
          get;
          set;
        }

        public Type ResourceSetType
        {
          get;
        }

        public string BaseName
        {
          get;
        }

        public object GetObject (string name, System.Globalization.CultureInfo culture) {
            Contract.Requires(name != null);

          return default(object);
        }
        public object GetObject (string name) {

          return default(object);
        }
        public string GetString (string name, System.Globalization.CultureInfo culture) {
          Contract.Requires(name != null);

          return default(string);
        }
        public string GetString (string name) {

          return default(string);
        }
        public ResourceSet GetResourceSet (System.Globalization.CultureInfo culture, bool createIfNotExists, bool tryParents) {
            Contract.Requires(culture != null);

          return default(ResourceSet);
        }
        public static ResourceManager CreateFileBasedResourceManager (string baseName, string resourceDir, Type usingResourceSet) {

          return default(ResourceManager);
        }
        public void ReleaseAllResources () {

        }
        public ResourceManager (Type resourceSource) {
            Contract.Requires(resourceSource != null);

          return default(ResourceManager);
        }
        public ResourceManager (string baseName, System.Reflection.Assembly assembly, Type usingResourceSet) {
            Contract.Requires(baseName != null);
            Contract.Requires(assembly != null);

          return default(ResourceManager);
        }
        public ResourceManager (string baseName, System.Reflection.Assembly assembly) {
            Contract.Requires(baseName != null);
            Contract.Requires(assembly != null);
          return default(ResourceManager);
        }
    }
}
