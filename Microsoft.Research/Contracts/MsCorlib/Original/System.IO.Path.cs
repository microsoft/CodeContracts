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

namespace System.IO
{

    public class Path
    {

        public static string Combine (string path1, string path2) {
            CodeContract.Requires(path1 != null);
            CodeContract.Requires(path2 != null);

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public static bool IsPathRooted (string! path) {

          return default(bool);
        }
        public static bool HasExtension (string! path) {

          return default(bool);
        }
        public static string GetTempFileName () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public static string GetTempPath () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public static string !GetPathRoot (string! path) {

          return default(string);
        }
        public static string GetFileNameWithoutExtension (string path) {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public static string GetFileName (string path) {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public static string GetFullPath (string path) {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public static string GetExtension (string path) {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public static string GetDirectoryName (string path) {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public static string ChangeExtension (string path, string extension) {
          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
    }
}
