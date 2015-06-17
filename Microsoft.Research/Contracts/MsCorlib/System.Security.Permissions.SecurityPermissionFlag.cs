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
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Security.Permissions {
  // Summary:
  //     Specifies access flags for the security permission object.
  [Serializable]
  [Flags]
  [ComVisible(true)]
  public enum SecurityPermissionFlag {
    // Summary:
    //     No security access.
    NoFlags = 0,
    //
    // Summary:
    //     Ability to assert that all this code's callers have the requisite permission
    //     for the operation.
    Assertion = 1,
    //
    // Summary:
    //     Ability to call unmanaged code.
    UnmanagedCode = 2,
    //
    // Summary:
    //     Ability to skip verification of code in this assembly. Code that is unverifiable
    //     can be run if this permission is granted.
    SkipVerification = 4,
    //
    // Summary:
    //     Permission for the code to run. Without this permission, managed code will
    //     not be executed.
    Execution = 8,
    //
    // Summary:
    //     Ability to use certain advanced operations on threads.
    ControlThread = 16,
    //
    // Summary:
    //     Ability to provide evidence, including the ability to alter the evidence
    //     provided by the common language runtime.
    ControlEvidence = 32,
    //
    // Summary:
    //     Ability to view and modify policy.
    ControlPolicy = 64,
    //
    // Summary:
    //     Ability to provide serialization services. Used by serialization formatters.
    SerializationFormatter = 128,
    //
    // Summary:
    //     Ability to specify domain policy.
    ControlDomainPolicy = 256,
    //
    // Summary:
    //     Ability to manipulate the principal object.
    ControlPrincipal = 512,
    //
    // Summary:
    //     Ability to create and manipulate an System.AppDomain.
    ControlAppDomain = 1024,
    //
    // Summary:
    //     Permission to configure Remoting types and channels.
    RemotingConfiguration = 2048,
    //
    // Summary:
    //     Permission to plug code into the common language runtime infrastructure,
    //     such as adding Remoting Context Sinks, Envoy Sinks and Dynamic Sinks.
    Infrastructure = 4096,
    //
    // Summary:
    //     Permission to perform explicit binding redirection in the application configuration
    //     file. This includes redirection of .NET Framework assemblies that have been
    //     unified as well as other assemblies found outside the .NET Framework.
    BindingRedirects = 8192,
    //
    // Summary:
    //     The unrestricted state of the permission.
    AllFlags = 16383,
  }
}
