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

    public class File
    {

        public static void Move (string! sourceFileName, string! destFileName) {
            CodeContract.Requires(sourceFileName != null);
            CodeContract.Requires(destFileName != null);
            CodeContract.Requires(sourceFileName.Length != 0);
            CodeContract.Requires(destFileName.Length != 0);

        }
        public static FileStream OpenWrite (string! path) {

          CodeContract.Ensures(CodeContract.Result<FileStream>() != null);
          return default(FileStream);
        }
        public static FileStream OpenRead (string! path) {

          CodeContract.Ensures(CodeContract.Result<FileStream>() != null);
          return default(FileStream);
        }
        public static void SetAttributes (string! path, FileAttributes fileAttributes) {

        }
        public static FileAttributes GetAttributes (string! path) {

          return default(FileAttributes);
        }
        public static DateTime GetLastWriteTimeUtc (string! path) {

          return default(DateTime);
        }
        public static DateTime GetLastWriteTime (string! path) {

          return default(DateTime);
        }
        public static void SetLastWriteTimeUtc (string! path, DateTime lastWriteTimeUtc) {

        }
        public static void SetLastWriteTime (string! path, DateTime lastWriteTime) {

        }
        public static DateTime GetLastAccessTimeUtc (string! path) {

          return default(DateTime);
        }
        public static DateTime GetLastAccessTime (string! path) {

          return default(DateTime);
        }
        public static void SetLastAccessTimeUtc (string! path, DateTime lastAccessTimeUtc) {

        }
        public static void SetLastAccessTime (string! path, DateTime lastAccessTime) {

        }
        public static DateTime GetCreationTimeUtc (string! path) {

          return default(DateTime);
        }
        public static DateTime GetCreationTime (string! path) {

          return default(DateTime);
        }
        public static void SetCreationTimeUtc (string! path, DateTime creationTimeUtc) {

        }
        public static void SetCreationTime (string! path, DateTime creationTime) {

        }
        public static FileStream Open (string! path, FileMode mode, FileAccess access, FileShare share) {

          CodeContract.Ensures(CodeContract.Result<FileStream>() != null);
          return default(FileStream);
        }
        public static FileStream Open (string! path, FileMode mode, FileAccess access) {

          CodeContract.Ensures(CodeContract.Result<FileStream>() != null);
          return default(FileStream);
        }
        public static FileStream Open (string! path, FileMode mode) {

          CodeContract.Ensures(CodeContract.Result<FileStream>() != null);
          return default(FileStream);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public static bool Exists (string! path) {

          return default(bool);
        }
        public static void Delete (string! path) {
            CodeContract.Requires(path != null);

        }
        public static FileStream Create (string! path, int bufferSize) {

          CodeContract.Ensures(CodeContract.Result<FileStream>() != null);
          return default(FileStream);
        }
        public static FileStream Create (string! path) {

          CodeContract.Ensures(CodeContract.Result<FileStream>() != null);
          return default(FileStream);
        }
        public static void Copy (string! sourceFileName, string! destFileName, bool overwrite) {

        }
        public static void Copy (string! sourceFileName, string! destFileName) {

        }
        public static StreamWriter AppendText (string! path) {
            CodeContract.Requires(path != null);

          CodeContract.Ensures(CodeContract.Result<StreamWriter>() != null);
          return default(StreamWriter);
        }
        public static StreamWriter CreateText (string! path) {
            CodeContract.Requires(path != null);

          CodeContract.Ensures(CodeContract.Result<StreamWriter>() != null);
          return default(StreamWriter);
        }
        public static StreamReader OpenText (string! path) {
            CodeContract.Requires(path != null);
          CodeContract.Ensures(CodeContract.Result<StreamReader>() != null);
          return default(StreamReader);
        }
    }
}
