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

namespace Microsoft.Win32
{

    public class RegistryKey
    {

        public int ValueCount
        {
          get;
        }

        public int SubKeyCount
        {
          get;
        }

        public string Name
        {
          get;
        }

        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          return default(string);
        }
        public void SetValue (string name, object! value) {
            CodeContract.Requires(value != null);
            CodeContract.Requires(name == null || name.Length < 255);

        }
        public object GetValue (string name) {

          return default(object);
        }
        public object GetValue (string name, object defaultValue) {

          return default(object);
        }
        public String[] GetValueNames () {

          return default(String[]);
        }
        public String[] GetSubKeyNames () {

          return default(String[]);
        }
        public RegistryKey OpenSubKey (string name) {

          return default(RegistryKey);
        }
        public RegistryKey OpenSubKey (string! name, bool writable) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(name.Length < 255);

          return default(RegistryKey);
        }
        public static RegistryKey OpenRemoteBaseKey (RegistryHive hKey, string! machineName) {
            CodeContract.Requires(machineName != null);
            CodeContract.Requires(((int)hKey & -16) == -2147483648);

          return default(RegistryKey);
        }
        public void DeleteValue (string! name, bool throwOnMissingValue) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(throwOnMissingValue == false);

        }
        public void DeleteValue (string name) {

        }
        public void DeleteSubKeyTree (string! subkey) {
            CodeContract.Requires(subkey != null);

        }
        public void DeleteSubKey (string! subkey, bool throwOnMissingSubKey) {
            CodeContract.Requires(subkey != null);
            CodeContract.Requires(throwOnMissingSubKey == false);
            CodeContract.Requires(throwOnMissingSubKey == false);

        }
        public void DeleteSubKey (string subkey) {

        }
        public RegistryKey CreateSubKey (string! subkey) {
            CodeContract.Requires(subkey != null);
            CodeContract.Requires(subkey.Length < 255);

          return default(RegistryKey);
        }
        public void Flush () {

        }
        public void Close () {
        }
    }
}
