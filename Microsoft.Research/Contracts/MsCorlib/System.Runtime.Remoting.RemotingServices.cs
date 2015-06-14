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

namespace System.Runtime.Remoting
{

    public class RemotingServices
    {

        public static void LogRemotingStage (int arg0) {

        }
        public static System.Runtime.Remoting.Messaging.IMethodReturnMessage ExecuteMessage (MarshalByRefObject target, System.Runtime.Remoting.Messaging.IMethodCallMessage reqMsg) {
            Contract.Requires(target != null);

          return default(System.Runtime.Remoting.Messaging.IMethodReturnMessage);
        }
        public static Type GetServerTypeForUri (string URI) {

          return default(Type);
        }
        public static bool IsOneWay (System.Reflection.MethodBase method) {

          return default(bool);
        }
        public static bool IsMethodOverloaded (System.Runtime.Remoting.Messaging.IMethodMessage msg) {

          return default(bool);
        }
        public static System.Reflection.MethodBase GetMethodBaseFromMethodMessage (System.Runtime.Remoting.Messaging.IMethodMessage msg) {

          return default(System.Reflection.MethodBase);
        }
        public static ObjRef GetObjRefForProxy (MarshalByRefObject obj) {

          return default(ObjRef);
        }
        public static System.Runtime.Remoting.Messaging.IMessageSink GetEnvoyChainForProxy (MarshalByRefObject obj) {

          return default(System.Runtime.Remoting.Messaging.IMessageSink);
        }
        public static bool Disconnect (MarshalByRefObject obj) {

          return default(bool);
        }
        public static object Connect (Type classToProxy, string url, object data) {

          return default(object);
        }
        public static object Connect (Type classToProxy, string url) {

          return default(object);
        }
        public static object Unmarshal (ObjRef objectRef, bool fRefine) {

          return default(object);
        }
        public static object Unmarshal (ObjRef objectRef) {

          return default(object);
        }
        public static void GetObjectData (object obj, System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) {
            Contract.Requires(obj != null);
            Contract.Requires(info != null);

        }
        public static ObjRef Marshal (MarshalByRefObject Obj, string ObjURI, Type RequestedType) {

          return default(ObjRef);
        }
        public static ObjRef Marshal (MarshalByRefObject Obj, string URI) {

          return default(ObjRef);
        }
        public static ObjRef Marshal (MarshalByRefObject Obj) {

          return default(ObjRef);
        }
        public static void SetObjectUriForMarshal (MarshalByRefObject obj, string uri) {

        }
        public static string GetObjectUri (MarshalByRefObject obj) {

          return default(string);
        }
        public static object GetLifetimeService (MarshalByRefObject obj) {

          return default(object);
        }
        public static string GetSessionIdForMethodMessage (System.Runtime.Remoting.Messaging.IMethodMessage msg) {

          return default(string);
        }
        public static System.Runtime.Remoting.Proxies.RealProxy GetRealProxy (object arg0) {

          return default(System.Runtime.Remoting.Proxies.RealProxy);
        }
        public static bool IsObjectOutOfAppDomain (object tp) {

          return default(bool);
        }
        public static bool IsObjectOutOfContext (object tp) {

          return default(bool);
        }
        public static bool IsTransparentProxy (object arg0) {
          return default(bool);
        }
    }
}
