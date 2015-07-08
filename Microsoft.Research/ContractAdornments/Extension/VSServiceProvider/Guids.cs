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

// Guids.cs
// MUST match guids.h
using System;

namespace ContractAdornments
{
    static class GuidList
    {
        public const string guidVSServiceProviderString = "e58a49d1-8f41-4c65-9cc5-55489982e9ac";
        public const string guidVSServiceProviderCmdSetString = "d92e53bd-0f83-42dc-a068-3b735e2680a9";
        public const string guidOptionsPageGeneralString = "3B894609-478D-4DC4-BC64-0E3E9CE8DC23";

        public static readonly Guid guidVSServiceProviderCmdSet = new Guid(guidVSServiceProviderCmdSetString);
    };
}