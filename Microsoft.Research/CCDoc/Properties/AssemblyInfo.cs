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

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("CCDocGen")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("CCDocGen")]

[assembly: SuppressMessage("Microsoft.Contracts", "Detected call to method 'Microsoft.Cci.IteratorHelper.EnumerableIsNotEmpty<Microsoft.Cci.Contracts.IPrecondition>(System.Collections.Generic.IEnumerable`1<Microsoft.Cci.Contracts.IPrecondition>)' without [Pure] in contracts of method 'CCDoc.ContractTraverser+ContractPackager.PackagePreconditions(System.Collections.Generic.IEnumerable`1<Microsoft.Cci.Contracts.IPrecondition>,System.String,System.String)'.")]
[assembly: SuppressMessage("Microsoft.Contracts", "Detected call to method 'Microsoft.Cci.IteratorHelper.EnumerableIsNotEmpty<Microsoft.Cci.Contracts.IPostcondition>(System.Collections.Generic.IEnumerable`1<Microsoft.Cci.Contracts.IPostcondition>)' without [Pure] in contracts of method 'CCDoc.ContractTraverser+ContractPackager.PackagePostconditions(System.Collections.Generic.IEnumerable`1<Microsoft.Cci.Contracts.IPostcondition>,System.String,System.String)'.")]
[assembly: SuppressMessage("Microsoft.Contracts", "Detected call to method 'Microsoft.Cci.IteratorHelper.EnumerableIsNotEmpty<Microsoft.Cci.Contracts.IThrownException>(System.Collections.Generic.IEnumerable`1<Microsoft.Cci.Contracts.IThrownException>)' without [Pure] in contracts of method 'CCDoc.ContractTraverser+ContractPackager.PackageThrownExceptions(System.Collections.Generic.IEnumerable`1<Microsoft.Cci.Contracts.IThrownException>,System.String,System.String)'.")]
[assembly: SuppressMessage("Microsoft.Contracts", "Detected call to method 'Microsoft.Cci.IteratorHelper.EnumerableIsNotEmpty<Microsoft.Cci.Contracts.ITypeInvariant>(System.Collections.Generic.IEnumerable`1<Microsoft.Cci.Contracts.ITypeInvariant>)' without [Pure] in contracts of method 'CCDoc.ContractTraverser+ContractPackager.PackageInvariants(System.Collections.Generic.IEnumerable`1<Microsoft.Cci.Contracts.ITypeInvariant>)'.")]
