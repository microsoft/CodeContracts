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

namespace System.Runtime.Remoting.Contexts
{

    public class SynchronizationAttribute
    {

        public bool Locked
        {
          get;
          set;
        }

        public bool IsReEntrant
        {
          get;
        }

        public System.Runtime.Remoting.Messaging.IMessageSink GetClientContextSink (System.Runtime.Remoting.Messaging.IMessageSink nextSink) {

          return default(System.Runtime.Remoting.Messaging.IMessageSink);
        }
        public System.Runtime.Remoting.Messaging.IMessageSink GetServerContextSink (System.Runtime.Remoting.Messaging.IMessageSink nextSink) {

          return default(System.Runtime.Remoting.Messaging.IMessageSink);
        }
        public void GetPropertiesForNewContext (System.Runtime.Remoting.Activation.IConstructionCallMessage ctorMsg) {

        }
        public bool IsContextOK (Context! ctx, System.Runtime.Remoting.Activation.IConstructionCallMessage! msg) {
            CodeContract.Requires(ctx != null);
            CodeContract.Requires(msg != null);

          return default(bool);
        }
        public SynchronizationAttribute (int flag, bool reEntrant) {

          return default(SynchronizationAttribute);
        }
        public SynchronizationAttribute (int flag) {

          return default(SynchronizationAttribute);
        }
        public SynchronizationAttribute (bool reEntrant) {

          return default(SynchronizationAttribute);
        }
        public SynchronizationAttribute () {
          return default(SynchronizationAttribute);
        }
    }
}
