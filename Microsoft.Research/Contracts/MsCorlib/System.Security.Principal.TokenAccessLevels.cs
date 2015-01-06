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

namespace System.Security.Principal
{
  // Summary:
  //     Defines the privileges of the user account associated with the access token.
  public enum TokenAccessLevels
  {
    // Summary:
    //     The user can attach a primary token to a process.
    AssignPrimary = 1,
    //
    // Summary:
    //     The user can duplicate the token.
    Duplicate = 2,
    //
    // Summary:
    //     The user can impersonate a client.
    Impersonate = 4,
    //
    // Summary:
    //     The user can query the token.
    Query = 8,
    //
    // Summary:
    //     The user can query the source of the token.
    QuerySource = 16,
    //
    // Summary:
    //     The user can enable or disable privileges in the token.
    AdjustPrivileges = 32,
    //
    // Summary:
    //     The user can change the attributes of the groups in the token.
    AdjustGroups = 64,
    //
    // Summary:
    //     The user can change the default owner, primary group, or discretionary access
    //     control list (DACL) of the token.
    AdjustDefault = 128,
    //
    // Summary:
    //     The user can adjust the session identifier of the token.
    AdjustSessionId = 256,
    //
    // Summary:
    //     The user has standard read rights and the System.Security.Principal.TokenAccessLevels.Query
    //     privilege for the token.
    Read = 131080,
    //
    // Summary:
    //     The user has standard write rights and the System.Security.Principal.TokenAccessLevels.AdjustPrivileges,
    //     F:System.Security.Principal.TokenAccessLevels.AdjustGroups, and System.Security.Principal.TokenAccessLevels.AdjustDefault
    //     privileges for the token.
    Write = 131296,
    //
    // Summary:
    //     The user has all possible access to the token.
    AllAccess = 983551,
    //
    // Summary:
    //     The maximum value that can be assigned for the System.Security.Principal.TokenAccessLevels
    //     enumeration.
    MaximumAllowed = 33554432,
  }
}
