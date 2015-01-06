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

namespace System.Runtime.Serialization.Formatters
{

    public class InternalST
    {

        public static System.Reflection.Assembly LoadAssemblyFromString (string assemblyString) {

          return default(System.Reflection.Assembly);
        }
        public static void SerializationSetValue (System.Reflection.FieldInfo! fi, object! target, object! value) {
            CodeContract.Requires(fi != null);
            CodeContract.Requires(target != null);
            CodeContract.Requires(value != null);

        }
        public static void SoapAssert (bool condition, string message) {

        }
        public static void Soap (Object[] messages) {

        }
        public static bool SoapCheckEnabled () {

          return default(bool);
        }
        }
        public static void InfoSoap (Object[] messages) {
    }
}
