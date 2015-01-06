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

namespace System.Runtime.Remoting.Contexts
{

    public class Context
    {

        public IContextProperty[] ContextProperties
        {
          get;
        }

        public int ContextID
        {
          get;
        }

        public static Context DefaultContext
        {
          get;
        }

        public static bool UnregisterDynamicProperty (string! name, ContextBoundObject obj, Context ctx) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(obj == null || ctx == null);

          return default(bool);
        }
        public static bool RegisterDynamicProperty (IDynamicProperty! prop, ContextBoundObject obj, Context ctx) {
            CodeContract.Requires(prop != null);
            CodeContract.Requires(obj == null || ctx == null);

          return default(bool);
        }
        public static object GetData (LocalDataStoreSlot slot) {

          return default(object);
        }
        public static void SetData (LocalDataStoreSlot slot, object data) {

        }
        public static void FreeNamedDataSlot (string name) {

        }
        public static LocalDataStoreSlot GetNamedDataSlot (string name) {

          return default(LocalDataStoreSlot);
        }
        public static LocalDataStoreSlot AllocateNamedDataSlot (string name) {

          return default(LocalDataStoreSlot);
        }
        public static LocalDataStoreSlot AllocateDataSlot () {

          return default(LocalDataStoreSlot);
        }
        public void DoCallBack (CrossContextDelegate! deleg) {
            CodeContract.Requires(deleg != null);

        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          return default(string);
        }
        public void Freeze () {

        }
        public void SetProperty (IContextProperty! prop) {
            CodeContract.Requires(prop != null);

        }
        public IContextProperty GetProperty (string name) {

          return default(IContextProperty);
        }
        public Context () {
          return default(Context);
        }
    }
}
