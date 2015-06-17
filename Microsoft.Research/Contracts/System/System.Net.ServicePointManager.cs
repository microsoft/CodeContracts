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

namespace System.Net
{

    public class ServicePointManager
    {

        public static bool UseNagleAlgorithm
        {
          get;
          set;
        }

        public static bool Expect100Continue
        {
          get;
          set;
        }

        public static ICertificatePolicy CertificatePolicy
        {
          get;
          set;
        }

        public static int MaxServicePointIdleTime
        {
          get;
          set;
        }

        public static SecurityProtocolType SecurityProtocol
        {
          get;
          set
            Contract.Requires((int)value == 48 || (int)value == 192 || (int)value == 240);
        }

        public static int MaxServicePoints
        {
          get;
          set;
        }

        public static int DefaultConnectionLimit
        {
          get;
          set
            Contract.Requires(value > 0);
        }

        public static bool CheckCertificateRevocationList
        {
          get;
          set;
        }

        public static ServicePoint FindServicePoint (Uri! address, IWebProxy proxy) {
            Contract.Requires(address != null);

          return default(ServicePoint);
        }
        public static ServicePoint FindServicePoint (string uriString, IWebProxy proxy) {

          return default(ServicePoint);
        }
        public static ServicePoint FindServicePoint (Uri address) {
          return default(ServicePoint);
        }
    }
}
