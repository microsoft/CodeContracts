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
using System.Reflection;
using System.Runtime.InteropServices;
// Use a non-specific version for the contract assembly to avoid having to redirect
// all assembly references in projects
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyCopyright("Microsoft")]

[assembly: AssemblyTitle("Microsoft.Contracts")]
#if !SILVERLIGHT
[assembly: AssemblyDescription("A self contained library for expressing contracts for CLR version < 4.0")]
#else
[assembly: AssemblyDescription("A self contained library for expressing contracts for Silverlight v2.0")]
[assembly: AssemblyConfiguration("Silverlight")]
#endif
[assembly: AssemblyProduct("Code Contracts")]
[assembly: AssemblyTrademark("Microsoft")]
[assembly: ComVisible(false)]

[assembly: CLSCompliant(true)]

// Security related
#if !SILVERLIGHT
[assembly: System.Security.AllowPartiallyTrustedCallers]
[assembly: System.Security.SecurityCritical]
#endif

