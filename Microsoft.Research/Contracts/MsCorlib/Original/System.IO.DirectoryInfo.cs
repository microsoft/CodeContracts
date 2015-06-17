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

    public class DirectoryInfo
    {

        public DirectoryInfo! Root
        {
          get;
        }

        public bool Exists
        {
          get;
        }

        public string! Name
        {
          get;
        }

        public DirectoryInfo! Parent
        {
          get;
        }

        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public void Delete (bool recursive) {

        }
        public void Delete () {

        }
        public void MoveTo (string! destDirName) {
            CodeContract.Requires(destDirName != null);
            CodeContract.Requires(destDirName.Length != 0);

        }
        public DirectoryInfo![] GetDirectories (string! searchPattern) {
            CodeContract.Requires(searchPattern != null);

          CodeContract.Ensures(CodeContract.Result<DirectoryInfo![]>() != null);
          return default(DirectoryInfo![]);
        }
        public FileSystemInfo![] GetFileSystemInfos () {

          CodeContract.Ensures(CodeContract.Result<FileSystemInfo![]>() != null);
          return default(FileSystemInfo![]);
        }
        public FileSystemInfo![] GetFileSystemInfos (string! searchPattern) {
            CodeContract.Requires(searchPattern != null);

          CodeContract.Ensures(CodeContract.Result<FileSystemInfo![]>() != null);
          return default(FileSystemInfo![]);
        }
        public DirectoryInfo![] GetDirectories () {

          CodeContract.Ensures(CodeContract.Result<DirectoryInfo![]>() != null);
          return default(DirectoryInfo![]);
        }
        public FileInfo![] GetFiles () {

          CodeContract.Ensures(CodeContract.Result<FileInfo![]>() != null);
          return default(FileInfo![]);
        }
        public FileInfo![] GetFiles (string! searchPattern) {
            CodeContract.Requires(searchPattern != null);

          CodeContract.Ensures(CodeContract.Result<FileInfo![]>() != null);
          return default(FileInfo![]);
        }
        public void Create () {

        }
        public DirectoryInfo CreateSubdirectory (string! path) {
            CodeContract.Requires(path != null);

          CodeContract.Ensures(CodeContract.Result<DirectoryInfo>() != null);
          return default(DirectoryInfo);
        }
        public DirectoryInfo (string! path) {
            CodeContract.Requires(path != null);
          return default(DirectoryInfo);
        }
    }
}
