// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

