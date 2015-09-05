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

    public class Directory
    {

        public static void Delete (string path, bool recursive) {

        }
        public static void Delete (string path) {

        }
        public static void Move (string! sourceDirName, string! destDirName) {
            CodeContract.Requires(sourceDirName != null);
            CodeContract.Requires(sourceDirName.Length != 0);
            CodeContract.Requires(destDirName != null);
            CodeContract.Requires(destDirName.Length != 0);

        }
        public static void SetCurrentDirectory (string! path) {
            CodeContract.Requires(path != null);
            CodeContract.Requires(path.Length != 0);
            CodeContract.Requires(path.Length < 260);

        }
        public static string GetCurrentDirectory () {

          return default(string);
        }
        public static string GetDirectoryRoot (string! path) {
            CodeContract.Requires(path != null);

          return default(string);
        }
        public static String[] GetLogicalDrives () {

          return default(String[]);
        }
        public static String[] GetFileSystemEntries (string! path, string! searchPattern) {
            CodeContract.Requires(path != null);
            CodeContract.Requires(searchPattern != null);

          return default(String[]);
        }
        public static String[] GetFileSystemEntries (string path) {

          return default(String[]);
        }
        public static String[] GetDirectories (string! path, string! searchPattern) {
            CodeContract.Requires(path != null);
            CodeContract.Requires(searchPattern != null);

          return default(String[]);
        }
        public static String[] GetDirectories (string path) {

          return default(String[]);
        }
        public static String[] GetFiles (string! path, string! searchPattern) {
            CodeContract.Requires(path != null);
            CodeContract.Requires(searchPattern != null);

          return default(String[]);
        }
        public static String[] GetFiles (string path) {

          return default(String[]);
        }
        public static DateTime GetLastAccessTimeUtc (string path) {

          return default(DateTime);
        }
        public static DateTime GetLastAccessTime (string path) {

          return default(DateTime);
        }
        public static void SetLastAccessTimeUtc (string path, DateTime lastAccessTimeUtc) {

        }
        public static void SetLastAccessTime (string path, DateTime lastAccessTime) {

        }
        public static DateTime GetLastWriteTimeUtc (string path) {

          return default(DateTime);
        }
        public static DateTime GetLastWriteTime (string path) {

          return default(DateTime);
        }
        public static void SetLastWriteTimeUtc (string path, DateTime lastWriteTimeUtc) {

        }
        public static void SetLastWriteTime (string path, DateTime lastWriteTime) {

        }
        public static DateTime GetCreationTimeUtc (string path) {

          return default(DateTime);
        }
        public static DateTime GetCreationTime (string path) {

          return default(DateTime);
        }
        public static void SetCreationTimeUtc (string path, DateTime creationTimeUtc) {

        }
        public static void SetCreationTime (string path, DateTime creationTime) {

        }
        public static bool Exists (string path) {

          return default(bool);
        }
        public static DirectoryInfo CreateDirectory (string! path) {
            CodeContract.Requires(path != null);
            CodeContract.Requires(path.Length != 0);

          return default(DirectoryInfo);
        }
        public static DirectoryInfo GetParent (string! path) {
            CodeContract.Requires(path != null);
            CodeContract.Requires(path.Length != 0);
          return default(DirectoryInfo);
        }
    }
}
