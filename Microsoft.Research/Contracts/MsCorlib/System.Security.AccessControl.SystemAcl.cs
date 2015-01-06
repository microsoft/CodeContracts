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
using System.Security.Principal;

namespace System.Security.AccessControl
{
  // Summary:
  //     Represents a System Access Control List (SACL).
  public sealed class SystemAcl 
  {
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.SystemAcl
    //     class with the specified values.
    //
    // Parameters:
    //   isContainer:
    //     true if the new System.Security.AccessControl.SystemAcl object is a container.
    //
    //   isDS:
    //     true if the new System.Security.AccessControl.SystemAcl object is a directory
    //     object Access Control List (ACL).
    //
    //   capacity:
    //     The number of Access Control Entries (ACEs) this System.Security.AccessControl.SystemAcl
    //     object can contain. This number is to be used only as a hint.
    extern public SystemAcl(bool isContainer, bool isDS, int capacity);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.SystemAcl
    //     class with the specified values from the specified System.Security.AccessControl.RawAcl
    //     object.
    //
    // Parameters:
    //   isContainer:
    //     true if the new System.Security.AccessControl.SystemAcl object is a container.
    //
    //   isDS:
    //     true if the new System.Security.AccessControl.SystemAcl object is a directory
    //     object Access Control List (ACL).
    //
    //   rawAcl:
    //     The underlying System.Security.AccessControl.RawAcl object for the new System.Security.AccessControl.SystemAcl
    //     object. Specify null to create an empty ACL.
    extern public SystemAcl(bool isContainer, bool isDS, RawAcl rawAcl);
    //
    // Summary:
    //     Initializes a new instance of the System.Security.AccessControl.SystemAcl
    //     class with the specified values.
    //
    // Parameters:
    //   isContainer:
    //     true if the new System.Security.AccessControl.SystemAcl object is a container.
    //
    //   isDS:
    //     true if the new System.Security.AccessControl.SystemAcl object is a directory
    //     object Access Control List (ACL).
    //
    //   revision:
    //     The revision level of the new System.Security.AccessControl.SystemAcl object.
    //
    //   capacity:
    //     The number of Access Control Entries (ACEs) this System.Security.AccessControl.SystemAcl
    //     object can contain. This number is to be used only as a hint.
    extern public SystemAcl(bool isContainer, bool isDS, byte revision, int capacity);

    // Summary:
    //     Adds an audit rule to the current System.Security.AccessControl.SystemAcl
    //     object.
    //
    // Parameters:
    //   auditFlags:
    //     The type of audit rule to add.
    //
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier for which to add an audit
    //     rule.
    //
    //   accessMask:
    //     The access mask for the new audit rule.
    //
    //   inheritanceFlags:
    //     Flags that specify the inheritance properties of the new audit rule.
    //
    //   propagationFlags:
    //     Flags that specify the inheritance propagation properties for the new audit
    //     rule.
    extern public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags);
    //
    // Summary:
    //     Adds an audit rule with the specified settings to the current System.Security.AccessControl.SystemAcl
    //     object. Use this method for directory object Access Control Lists (ACLs)
    //     when specifying the object type or the inherited object type for the new
    //     audit rule.
    //
    // Parameters:
    //   auditFlags:
    //     The type of audit rule to add.
    //
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier for which to add an audit
    //     rule.
    //
    //   accessMask:
    //     The access mask for the new audit rule.
    //
    //   inheritanceFlags:
    //     Flags that specify the inheritance properties of the new audit rule.
    //
    //   propagationFlags:
    //     Flags that specify the inheritance propagation properties for the new audit
    //     rule.
    //
    //   objectFlags:
    //     Flags that specify if the objectType and inheritedObjectType parameters contain
    //     non-null values.
    //
    //   objectType:
    //     The identity of the class of objects to which the new audit rule applies.
    //
    //   inheritedObjectType:
    //     The identity of the class of child objects which can inherit the new audit
    //     rule.
    extern public void AddAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType);
    //
    // Summary:
    //     Removes the specified audit rule from the current System.Security.AccessControl.SystemAcl
    //     object.
    //
    // Parameters:
    //   auditFlags:
    //     The type of audit rule to remove.
    //
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier for which to remove an audit
    //     rule.
    //
    //   accessMask:
    //     The access mask for the rule to be removed.
    //
    //   inheritanceFlags:
    //     Flags that specify the inheritance properties of the rule to be removed.
    //
    //   propagationFlags:
    //     Flags that specify the inheritance propagation properties for the rule to
    //     be removed.
    //
    // Returns:
    //     true if this method successfully removes the specified audit rule; otherwise,
    //     false.
    extern public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags);
    //
    // Summary:
    //     Removes the specified audit rule from the current System.Security.AccessControl.SystemAcl
    //     object. Use this method for directory object Access Control Lists (ACLs)
    //     when specifying the object type or the inherited object type.
    //
    // Parameters:
    //   auditFlags:
    //     The type of audit rule to remove.
    //
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier for which to remove an audit
    //     rule.
    //
    //   accessMask:
    //     The access mask for the rule to be removed.
    //
    //   inheritanceFlags:
    //     Flags that specify the inheritance properties of the rule to be removed.
    //
    //   propagationFlags:
    //     Flags that specify the inheritance propagation properties for the rule to
    //     be removed.
    //
    //   objectFlags:
    //     Flags that specify if the objectType and inheritedObjectType parameters contain
    //     non-null values.
    //
    //   objectType:
    //     The identity of the class of objects to which the removed audit control rule
    //     applies.
    //
    //   inheritedObjectType:
    //     The identity of the class of child objects which can inherit the removed
    //     audit rule.
    //
    // Returns:
    //     true if this method successfully removes the specified audit rule; otherwise,
    //     false.
    extern public bool RemoveAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType);
    //
    // Summary:
    //     Removes the specified audit rule from the current System.Security.AccessControl.DiscretionaryAcl
    //     object.
    //
    // Parameters:
    //   auditFlags:
    //     The type of audit rule to remove.
    //
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier for which to remove an audit
    //     rule.
    //
    //   accessMask:
    //     The access mask for the rule to be removed.
    //
    //   inheritanceFlags:
    //     Flags that specify the inheritance properties of the rule to be removed.
    //
    //   propagationFlags:
    //     Flags that specify the inheritance propagation properties for the rule to
    //     be removed.
    extern public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags);
    //
    // Summary:
    //     Removes the specified audit rule from the current System.Security.AccessControl.DiscretionaryAcl
    //     object. Use this method for directory object Access Control Lists (ACLs)
    //     when specifying the object type or the inherited object type.
    //
    // Parameters:
    //   auditFlags:
    //     The type of audit rule to remove.
    //
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier for which to remove an audit
    //     rule.
    //
    //   accessMask:
    //     The access mask for the rule to be removed.
    //
    //   inheritanceFlags:
    //     Flags that specify the inheritance properties of the rule to be removed.
    //
    //   propagationFlags:
    //     Flags that specify the inheritance propagation properties for the rule to
    //     be removed.
    //
    //   objectFlags:
    //     Flags that specify if the objectType and inheritedObjectType parameters contain
    //     non-null values.
    //
    //   objectType:
    //     The identity of the class of objects to which the removed audit control rule
    //     applies.
    //
    //   inheritedObjectType:
    //     The identity of the class of child objects which can inherit the removed
    //     audit rule.
    extern public void RemoveAuditSpecific(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType);
    //
    // Summary:
    //     Sets the specified audit rule for the specified System.Security.Principal.SecurityIdentifier
    //     object.
    //
    // Parameters:
    //   auditFlags:
    //     The audit condition to set.
    //
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier for which to set an audit
    //     rule.
    //
    //   accessMask:
    //     The access mask for the new audit rule.
    //
    //   inheritanceFlags:
    //     Flags that specify the inheritance properties of the new audit rule.
    //
    //   propagationFlags:
    //     Flags that specify the inheritance propagation properties for the new audit
    //     rule.
    extern public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags);
    //
    // Summary:
    //     Sets the specified audit rule for the specified System.Security.Principal.SecurityIdentifier
    //     object. Use this method for directory object Access Control Lists (ACLs)
    //     when specifying the object type or the inherited object type.
    //
    // Parameters:
    //   auditFlags:
    //     The audit condition to set.
    //
    //   sid:
    //     The System.Security.Principal.SecurityIdentifier for which to set an audit
    //     rule.
    //
    //   accessMask:
    //     The access mask for the new audit rule.
    //
    //   inheritanceFlags:
    //     Flags that specify the inheritance properties of the new audit rule.
    //
    //   propagationFlags:
    //     Flags that specify the inheritance propagation properties for the new audit
    //     rule.
    //
    //   objectFlags:
    //     Flags that specify if the objectType and inheritedObjectType parameters contain
    //     non-null values.
    //
    //   objectType:
    //     The identity of the class of objects to which the new audit rule applies.
    //
    //   inheritedObjectType:
    //     The identity of the class of child objects which can inherit the new audit
    //     rule.
    extern public void SetAudit(AuditFlags auditFlags, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType);
  }
}
