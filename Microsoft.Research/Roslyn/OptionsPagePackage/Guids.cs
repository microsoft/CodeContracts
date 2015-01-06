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

namespace ClousotRoslynExtension.OptionsPagePackage
{
    public static class GuidList
    {
      public const string guidOptionsPagePackagePkgString = "1D686D6D-0A62-493A-9393-524598D28E5A";
      public const string guidOptionsPagePackageCmdSetString = "6EB1314F-67F8-4A82-B88A-5665C16D1420";

        public static readonly Guid guidOptionsPagePackageCmdSet = new Guid(guidOptionsPagePackageCmdSetString);
        public static readonly Guid guidOptionsPagePackagePkg = new Guid(guidOptionsPagePackagePkgString);
    };
}