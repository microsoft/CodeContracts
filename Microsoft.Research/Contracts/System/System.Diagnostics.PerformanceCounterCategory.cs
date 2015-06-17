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

    public class PerformanceCounterCategory
    {

        public string CategoryHelp
        {
          get;
        }

        public string MachineName
        {
          get;
          set;
        }

        public string! CategoryName
        {
          get;
          set
            Contract.Requires(value != null);
            Contract.Requires(value.Length != 0);
        }

        public InstanceDataCollectionCollection ReadCategory () {

          return default(InstanceDataCollectionCollection);
        }
        public static bool InstanceExists (string! instanceName, string! categoryName, string machineName) {
            Contract.Requires(instanceName != null);
            Contract.Requires(categoryName != null);
            Contract.Requires(categoryName.Length != 0);

          return default(bool);
        }
        public static bool InstanceExists (string instanceName, string categoryName) {

          return default(bool);
        }
        public bool InstanceExists (string! instanceName) {
            Contract.Requires(instanceName != null);

          return default(bool);
        }
        public String[] GetInstanceNames () {

          return default(String[]);
        }
        public static PerformanceCounterCategory[] GetCategories (string machineName) {

          return default(PerformanceCounterCategory[]);
        }
        public static PerformanceCounterCategory[] GetCategories () {

          return default(PerformanceCounterCategory[]);
        }
        public PerformanceCounter[] GetCounters (string! instanceName) {
            Contract.Requires(instanceName != null);

          return default(PerformanceCounter[]);
        }
        public PerformanceCounter[] GetCounters () {

          return default(PerformanceCounter[]);
        }
        public static bool Exists (string! categoryName, string machineName) {
            Contract.Requires(categoryName != null);
            Contract.Requires(categoryName.Length != 0);

          return default(bool);
        }
        public static bool Exists (string categoryName) {

          return default(bool);
        }
        public static void Delete (string categoryName) {

        }
        public static PerformanceCounterCategory Create (string categoryName, string categoryHelp, string counterName, string counterHelp) {

          return default(PerformanceCounterCategory);
        }
        public static PerformanceCounterCategory Create (string categoryName, string categoryHelp, CounterCreationDataCollection counterData) {

          return default(PerformanceCounterCategory);
        }
        public static bool CounterExists (string! counterName, string! categoryName, string machineName) {
            Contract.Requires(counterName != null);
            Contract.Requires(categoryName != null);
            Contract.Requires(categoryName.Length != 0);

          return default(bool);
        }
        public static bool CounterExists (string counterName, string categoryName) {

          return default(bool);
        }
        public bool CounterExists (string! counterName) {
            Contract.Requires(counterName != null);

          return default(bool);
        }
        public PerformanceCounterCategory (string! categoryName, string machineName) {
            Contract.Requires(categoryName != null);
            Contract.Requires(categoryName.Length != 0);

          return default(PerformanceCounterCategory);
        }
        public PerformanceCounterCategory (string categoryName) {

          return default(PerformanceCounterCategory);
        }
        public PerformanceCounterCategory () {
          return default(PerformanceCounterCategory);
        }
    }
}
