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

namespace System.Security.AccessControl
{
  // Summary:
  //     These flags affect the security descriptor behavior.
  public enum ControlFlags
  {
    // Summary:
    //     No control flags.
    None = 0,
    //
    // Summary:
    //     Specifies that the owner System.Security.Principal.SecurityIdentifier was
    //     obtained by a defaulting mechanism. Set by resource managers only; should
    //     not be set by callers.
    OwnerDefaulted = 1,
    //
    // Summary:
    //     Specifies that the group System.Security.Principal.SecurityIdentifier was
    //     obtained by a defaulting mechanism. Set by resource managers only; should
    //     not be set by callers.
    GroupDefaulted = 2,
    //
    // Summary:
    //     Specifies that the DACL is not null. Set by resource managers or users.
    DiscretionaryAclPresent = 4,
    //
    // Summary:
    //     Specifies that the DACL was obtained by a defaulting mechanism. Set by resource
    //     managers only.
    DiscretionaryAclDefaulted = 8,
    //
    // Summary:
    //     Specifies that the SACL is not null. Set by resource managers or users.
    SystemAclPresent = 16,
    //
    // Summary:
    //     Specifies that the SACL was obtained by a defaulting mechanism. Set by resource
    //     managers only.
    SystemAclDefaulted = 32,
    //
    // Summary:
    //     Ignored.
    DiscretionaryAclUntrusted = 64,
    //
    // Summary:
    //     Ignored.
    ServerSecurity = 128,
    //
    // Summary:
    //     Ignored.
    DiscretionaryAclAutoInheritRequired = 256,
    //
    // Summary:
    //     Ignored.
    SystemAclAutoInheritRequired = 512,
    //
    // Summary:
    //     Specifies that the Discretionary Access Control List (DACL) has been automatically
    //     inherited from the parent. Set by resource managers only.
    DiscretionaryAclAutoInherited = 1024,
    //
    // Summary:
    //     Specifies that the System Access Control List (SACL) has been automatically
    //     inherited from the parent. Set by resource managers only.
    SystemAclAutoInherited = 2048,
    //
    // Summary:
    //     Specifies that the resource manager prevents auto-inheritance. Set by resource
    //     managers or users.
    DiscretionaryAclProtected = 4096,
    //
    // Summary:
    //     Specifies that the resource manager prevents auto-inheritance. Set by resource
    //     managers or users.
    SystemAclProtected = 8192,
    //
    // Summary:
    //     Specifies that the contents of the Reserved field are valid.
    RMControlValid = 16384,
    //
    // Summary:
    //     Specifies that the security descriptor binary representation is in the self-relative
    //     format. This flag is always set.
    SelfRelative = 32768,
  }
}
