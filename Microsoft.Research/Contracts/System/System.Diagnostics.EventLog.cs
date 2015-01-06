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

namespace System.Diagnostics
{

    public class EventLog
    {

        public string LogDisplayName
        {
          get;
        }

        public bool EnableRaisingEvents
        {
          get;
          set;
        }

        public string MachineName
        {
          get;
          set;
        }

        public EventLogEntryCollection Entries
        {
          get;
        }

        public string! Log
        {
          get;
          set
            Contract.Requires(value != null);
        }

        public System.ComponentModel.ISynchronizeInvoke SynchronizingObject
        {
          get;
          set;
        }

        public string Source
        {
          get;
          set;
        }

        public static void WriteEntry (string source, string message, EventLogEntryType type, int eventID, Int16 category, Byte[] rawData) {

        }
        public void WriteEntry (string message, EventLogEntryType type, int eventID, Int16 category, Byte[] rawData) {

        }
        public static void WriteEntry (string source, string message, EventLogEntryType type, int eventID, Int16 category) {

        }
        public void WriteEntry (string message, EventLogEntryType type, int eventID, Int16 category) {

        }
        public static void WriteEntry (string source, string message, EventLogEntryType type, int eventID) {

        }
        public void WriteEntry (string message, EventLogEntryType type, int eventID) {

        }
        public static void WriteEntry (string source, string message, EventLogEntryType type) {

        }
        public void WriteEntry (string message, EventLogEntryType type) {

        }
        public static void WriteEntry (string source, string message) {

        }
        public void WriteEntry (string message) {

        }
        public static string LogNameFromSourceName (string source, string machineName) {

          return default(string);
        }
        public static bool SourceExists (string source, string machineName) {

          return default(bool);
        }
        public static bool SourceExists (string source) {

          return default(bool);
        }
        public static EventLog[] GetEventLogs (string machineName) {

          return default(EventLog[]);
        }
        public static EventLog[] GetEventLogs () {

          return default(EventLog[]);
        }
        public static bool Exists (string logName, string machineName) {

          return default(bool);
        }
        public static bool Exists (string logName) {

          return default(bool);
        }
        public void EndInit () {

        }
        public static void DeleteEventSource (string source, string machineName) {

        }
        public static void DeleteEventSource (string source) {

        }
        public static void Delete (string! logName, string machineName) {
            Contract.Requires(logName != null);

        }
        public static void Delete (string logName) {

        }
        public static void CreateEventSource (string source, string logName, string machineName) {

        }
        public static void CreateEventSource (string source, string logName) {

        }
        public void Close () {

        }
        public void Clear () {

        }
        public void BeginInit () {

        }
        public void remove_EntryWritten (EntryWrittenEventHandler value) {

        }
        public void add_EntryWritten (EntryWrittenEventHandler value) {

        }
        public EventLog (string! logName, string machineName, string source) {
            Contract.Requires(logName != null);

          return default(EventLog);
        }
        public EventLog (string logName, string machineName) {

          return default(EventLog);
        }
        public EventLog (string logName) {

          return default(EventLog);
        }
        public EventLog () {
          return default(EventLog);
        }
    }
}
