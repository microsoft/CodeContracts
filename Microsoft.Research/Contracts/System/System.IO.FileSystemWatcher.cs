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

    public class FileSystemWatcher
    {

        public bool EnableRaisingEvents
        {
          get;
          set;
        }

        public string Filter
        {
          get;
          set;
        }

        public NotifyFilters NotifyFilter
        {
          get;
          set;
        }

        public System.ComponentModel.ISite Site
        {
          get;
          set;
        }

        public int InternalBufferSize
        {
          get;
          set;
        }

        public bool IncludeSubdirectories
        {
          get;
          set;
        }

        public string Path
        {
          get;
          set;
        }

        public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
        {
          get;
          set;
        }

        public WaitForChangedResult WaitForChanged (WatcherChangeTypes changeType, int timeout) {

          return default(WaitForChangedResult);
        }
        public WaitForChangedResult WaitForChanged (WatcherChangeTypes changeType) {

          return default(WaitForChangedResult);
        }
        public void EndInit () {

        }
        public void BeginInit () {

        }
        public void remove_Renamed (RenamedEventHandler value) {

        }
        public void add_Renamed (RenamedEventHandler value) {

        }
        public void remove_Error (ErrorEventHandler value) {

        }
        public void add_Error (ErrorEventHandler value) {

        }
        public void remove_Deleted (FileSystemEventHandler value) {

        }
        public void add_Deleted (FileSystemEventHandler value) {

        }
        public void remove_Created (FileSystemEventHandler value) {

        }
        public void add_Created (FileSystemEventHandler value) {

        }
        public void remove_Changed (FileSystemEventHandler value) {

        }
        public void add_Changed (FileSystemEventHandler value) {

        }
        public FileSystemWatcher (string! path, string! filter) {
            Contract.Requires(path != null);
            Contract.Requires(filter != null);
            Contract.Requires(path.Length != 0);

          return default(FileSystemWatcher);
        }
        public FileSystemWatcher (string path) {

          return default(FileSystemWatcher);
        }
        public FileSystemWatcher () {
          return default(FileSystemWatcher);
        }
    }
}
