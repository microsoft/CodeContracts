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

namespace System.Runtime.Remoting.Messaging
{

    public class ReturnMessage
    {

        public Object[] OutArgs
        {
          get;
        }

        public int ArgCount
        {
          get;
        }

        public string Uri
        {
          get;
          set;
        }

        public bool HasVarArgs
        {
          get;
        }

        public int OutArgCount
        {
          get;
        }

        public System.Collections.IDictionary Properties
        {
          get;
        }

        public Object[] Args
        {
          get;
        }

        public object ReturnValue
        {
          get;
        }

        public Exception Exception
        {
          get;
        }

        public object MethodSignature
        {
          get;
        }

        public LogicalCallContext LogicalCallContext
        {
          get;
        }

        public string TypeName
        {
          get;
        }

        public System.Reflection.MethodBase MethodBase
        {
          get;
        }

        public string MethodName
        {
          get;
        }

        public string GetOutArgName (int index) {

          return default(string);
        }
        public object GetOutArg (int argNum) {

          return default(object);
        }
        public string GetArgName (int index) {
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(index >= 0);

          return default(string);
        }
        public object GetArg (int argNum) {
            CodeContract.Requires(argNum >= 0);
            CodeContract.Requires(argNum >= 0);

          return default(object);
        }
        public ReturnMessage (Exception e, IMethodCallMessage mcm) {

          return default(ReturnMessage);
        }
          return default(ReturnMessage);
        }
        public ReturnMessage (object ret, Object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm) {
    }
}
