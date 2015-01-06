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

namespace System.IO
{

    public class FileInfo
    {

        public bool Exists
        {
          get;
        }

        public string Name
        {
          get;
        }

        public string DirectoryName
        {
          get;
        }

        public Int64 Length
        {
          get
            CodeContract.Ensures(result >= 0);
        }

        public DirectoryInfo Directory
        {
          get;
        }

        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          return default(string);
        }
        public void MoveTo (string! destFileName) {
            CodeContract.Requires(destFileName != null);
            CodeContract.Requires(destFileName.Length != 0);

        }
        public FileStream OpenWrite () {

          return default(FileStream);
        }
        public FileStream OpenRead () {

          return default(FileStream);
        }
        public FileStream Open (FileMode mode, FileAccess access, FileShare share) {

          return default(FileStream);
        }
        public FileStream Open (FileMode mode, FileAccess access) {

          return default(FileStream);
        }
        public FileStream Open (FileMode mode) {

          return default(FileStream);
        }
        public void Delete () {

        }
        public FileInfo CopyTo (string destFileName, bool overwrite) {

          return default(FileInfo);
        }
        public FileStream Create () {

          return default(FileStream);
        }
        public FileInfo CopyTo (string destFileName) {

          return default(FileInfo);
        }
        public StreamWriter AppendText () {

          return default(StreamWriter);
        }
        public StreamWriter CreateText () {

          return default(StreamWriter);
        }
        public StreamReader OpenText () {

          return default(StreamReader);
        }
        public FileInfo (string! fileName) {
            CodeContract.Requires(fileName != null);
          return default(FileInfo);
        }
    }
}
