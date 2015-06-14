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
  //     Specifies common roles to be used with System.Security.Principal.WindowsPrincipal.IsInRole(System.String).
  public enum WindowsBuiltInRole
  {
    // Summary:
    //     Administrators have complete and unrestricted access to the computer or domain.
    Administrator = 544,
    //
    // Summary:
    //     Users are prevented from making accidental or intentional system-wide changes.
    //     Thus, users can run certified applications, but not most legacy applications.
    User = 545,
    //
    // Summary:
    //     Guests are more restricted than users.
    Guest = 546,
    //
    // Summary:
    //     Power users possess most administrative permissions with some restrictions.
    //     Thus, power users can run legacy applications, in addition to certified applications.
    PowerUser = 547,
    //
    // Summary:
    //     Account operators manage the user accounts on a computer or domain.
    AccountOperator = 548,
    //
    // Summary:
    //     System operators manage a particular computer.
    SystemOperator = 549,
    //
    // Summary:
    //     Print operators can take control of a printer.
    PrintOperator = 550,
    //
    // Summary:
    //     Backup operators can override security restrictions for the sole purpose
    //     of backing up or restoring files.
    BackupOperator = 551,
    //
    // Summary:
    //     Replicators support file replication in a domain.
    Replicator = 552,
  }
}
