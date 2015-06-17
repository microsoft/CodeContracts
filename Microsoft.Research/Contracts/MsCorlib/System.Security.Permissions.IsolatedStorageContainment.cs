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
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  // Summary:
  //     Specifies the permitted use of isolated storage.
  public enum IsolatedStorageContainment
  {
    // Summary:
    //     Use of isolated storage is not allowed.
    None = 0,
    //
    // Summary:
    //     Storage is isolated first by user and then by domain and assembly. Storage
    //     is also isolated by computer. Data can only be accessed within the context
    //     of the same application and only when run by the same user. This is helpful
    //     when a third-party assembly wants to keep a private data store.
    DomainIsolationByUser = 16,
    //
    // Summary:
    //     Storage is isolated first by user and then by application. Storage is also
    //     isolated by computer. This provides a data store for the application that
    //     is accessible in any domain context. The per-application data compartment
    //     requires additional trust because it potentially provides a "tunnel" between
    //     applications that could compromise the data isolation of applications in
    //     particular Web sites.
    ApplicationIsolationByUser = 21,
    //
    // Summary:
    //     Storage is isolated first by user and then by code assembly. Storage is also
    //     isolated by computer. This provides a data store for the assembly that is
    //     accessible in any domain context. The per-assembly data compartment requires
    //     additional trust because it potentially provides a "tunnel" between applications
    //     that could compromise the data isolation of applications in particular Web
    //     sites.
    AssemblyIsolationByUser = 32,
    //
    // Summary:
    //     Storage is isolated first by computer and then by domain and assembly. Data
    //     can only be accessed within the context of the same application and only
    //     when run on the same computer. This is helpful when a third-party assembly
    //     wants to keep a private data store.
    DomainIsolationByMachine = 48,
    //
    // Summary:
    //     Storage is isolated first by computer and then by code assembly. This provides
    //     a data store for the assembly that is accessible in any domain context. The
    //     per-assembly data compartment requires additional trust because it potentially
    //     provides a "tunnel" between applications that could compromise the data isolation
    //     of applications in particular Web sites.
    AssemblyIsolationByMachine = 64,
    //
    // Summary:
    //     Storage is isolated first by computer and then by application. This provides
    //     a data store for the application that is accessible in any domain context.
    //     The per-application data compartment requires additional trust because it
    //     potentially provides a "tunnel" between applications that could compromise
    //     the data isolation of applications in particular Web sites.
    ApplicationIsolationByMachine = 69,
    //
    // Summary:
    //     Storage is isolated first by user and then by domain and assembly. Storage
    //     will roam if Windows user data roaming is enabled. Data can only be accessed
    //     within the context of the same application and only when run by the same
    //     user. This is helpful when a third-party assembly wants to keep a private
    //     data store.
    DomainIsolationByRoamingUser = 80,
    //
    // Summary:
    //     Storage is isolated first by user and then by assembly evidence. Storage
    //     will roam if Windows user data roaming is enabled. This provides a data store
    //     for the assembly that is accessible in any domain context. The per-assembly
    //     data compartment requires additional trust because it potentially provides
    //     a "tunnel" between applications that could compromise the data isolation
    //     of applications in particular Web sites.
    AssemblyIsolationByRoamingUser = 96,
    //
    // Summary:
    //     Storage is isolated first by user and then by application evidence. Storage
    //     will roam if Windows user data roaming is enabled. This provides a data store
    //     for the application that is accessible in any domain context. The per-application
    //     data compartment requires additional trust because it potentially provides
    //     a "tunnel" between applications that could compromise the data isolation
    //     of applications in particular Web sites.
    ApplicationIsolationByRoamingUser = 101,
    //
    // Summary:
    //     Unlimited administration ability for the user store. Allows browsing and
    //     deletion of the entire user store, but not read access other than the user's
    //     own domain/assembly identity.
    AdministerIsolatedStorageByUser = 112,
    //
    // Summary:
    //     Use of isolated storage is allowed without restriction. Code has full access
    //     to any part of the user store, regardless of the identity of the domain or
    //     assembly. This use of isolated storage includes the ability to enumerate
    //     the contents of the isolated storage data store.
    UnrestrictedIsolatedStorage = 240,
  }
}
