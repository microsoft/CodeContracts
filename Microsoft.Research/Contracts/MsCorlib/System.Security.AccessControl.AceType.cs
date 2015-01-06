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
  //     Defines the available access control entry (ACE) types.
  public enum AceType
  {
    // Summary:
    //     Allows access to an object for a specific trustee identified by an System.Security.Principal.IdentityReference
    //     object.
    AccessAllowed = 0,
    //
    // Summary:
    //     Denies access to an object for a specific trustee identified by an System.Security.Principal.IdentityReference
    //     object.
    AccessDenied = 1,
    //
    // Summary:
    //     Causes an audit message to be logged when a specified trustee attempts to
    //     gain access to an object. The trustee is identified by an System.Security.Principal.IdentityReference
    //     object.
    SystemAudit = 2,
    //
    // Summary:
    //     Reserved for future use.
    SystemAlarm = 3,
    //
    // Summary:
    //     Defined but never used. Included here for completeness.
    AccessAllowedCompound = 4,
    //
    // Summary:
    //     Allows access to an object, property set, or property. The ACE contains a
    //     set of access rights, a GUID that identifies the type of object, and an System.Security.Principal.IdentityReference
    //     object that identifies the trustee to whom the system will grant access.
    //     The ACE also contains a GUID and a set of flags that control inheritance
    //     of the ACE by child objects.
    AccessAllowedObject = 5,
    //
    // Summary:
    //     Denies access to an object, property set, or property. The ACE contains a
    //     set of access rights, a GUID that identifies the type of object, and an System.Security.Principal.IdentityReference
    //     object that identifies the trustee to whom the system will grant access.
    //     The ACE also contains a GUID and a set of flags that control inheritance
    //     of the ACE by child objects.
    AccessDeniedObject = 6,
    //
    // Summary:
    //     Causes an audit message to be logged when a specified trustee attempts to
    //     gain access to an object or subobjects such as property sets or properties.
    //     The ACE contains a set of access rights, a GUID that identifies the type
    //     of object or subobject, and an System.Security.Principal.IdentityReference
    //     object that identifies the trustee for whom the system will audit access.
    //     The ACE also contains a GUID and a set of flags that control inheritance
    //     of the ACE by child objects.
    SystemAuditObject = 7,
    //
    // Summary:
    //     Reserved for future use.
    SystemAlarmObject = 8,
    //
    // Summary:
    //     Allows access to an object for a specific trustee identified by an System.Security.Principal.IdentityReference
    //     object. This ACE type may contain optional callback data. The callback data
    //     is a resource manager–specific BLOB that is not interpreted.
    AccessAllowedCallback = 9,
    //
    // Summary:
    //     Denies access to an object for a specific trustee identified by an System.Security.Principal.IdentityReference
    //     object. This ACE type can contain optional callback data. The callback data
    //     is a resource manager–specific BLOB that is not interpreted.
    AccessDeniedCallback = 10,
    //
    // Summary:
    //     Allows access to an object, property set, or property. The ACE contains a
    //     set of access rights, a GUID that identifies the type of object, and an System.Security.Principal.IdentityReference
    //     object that identifies the trustee to whom the system will grant access.
    //     The ACE also contains a GUID and a set of flags that control inheritance
    //     of the ACE by child objects. This ACE type may contain optional callback
    //     data. The callback data is a resource manager–specific BLOB that is not interpreted.
    AccessAllowedCallbackObject = 11,
    //
    // Summary:
    //     Denies access to an object, property set, or property. The ACE contains a
    //     set of access rights, a GUID that identifies the type of object, and an System.Security.Principal.IdentityReference
    //     object that identifies the trustee to whom the system will grant access.
    //     The ACE also contains a GUID and a set of flags that control inheritance
    //     of the ACE by child objects. This ACE type can contain optional callback
    //     data. The callback data is a resource manager–specific BLOB that is not interpreted.
    AccessDeniedCallbackObject = 12,
    //
    // Summary:
    //     Causes an audit message to be logged when a specified trustee attempts to
    //     gain access to an object. The trustee is identified by an System.Security.Principal.IdentityReference
    //     object. This ACE type can contain optional callback data. The callback data
    //     is a resource manager–specific BLOB that is not interpreted.
    SystemAuditCallback = 13,
    //
    // Summary:
    //     Reserved for future use.
    SystemAlarmCallback = 14,
    //
    // Summary:
    //     Causes an audit message to be logged when a specified trustee attempts to
    //     gain access to an object or subobjects such as property sets or properties.
    //     The ACE contains a set of access rights, a GUID that identifies the type
    //     of object or subobject, and an System.Security.Principal.IdentityReference
    //     object that identifies the trustee for whom the system will audit access.
    //     The ACE also contains a GUID and a set of flags that control inheritance
    //     of the ACE by child objects. This ACE type can contain optional callback
    //     data. The callback data is a resource manager–specific BLOB that is not interpreted.
    SystemAuditCallbackObject = 15,
    //
    // Summary:
    //     Tracks the maximum defined ACE type in the enumeration.
    MaxDefinedAceType = 16,
    //
    // Summary:
    //     Reserved for future use.
    SystemAlarmCallbackObject = 16,
  }
}
