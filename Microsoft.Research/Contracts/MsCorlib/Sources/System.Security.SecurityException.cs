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

// File System.Security.SecurityException.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Security
{
  public partial class SecurityException : SystemException
  {
    #region Methods and constructors
    public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public SecurityException(string message, Type type, string state)
    {
    }

    public SecurityException(string message, Exception inner)
    {
    }

    public SecurityException(string message)
    {
    }

    public SecurityException(string message, Type type)
    {
    }

    protected SecurityException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public SecurityException()
    {
    }

    public SecurityException(string message, Object deny, Object permitOnly, System.Reflection.MethodInfo method, Object demanded, IPermission permThatFailed)
    {
      Contract.Ensures(0 <= string.Empty.Length);
    }

    public SecurityException(string message, System.Reflection.AssemblyName assemblyName, PermissionSet grant, PermissionSet refused, System.Reflection.MethodInfo method, System.Security.Permissions.SecurityAction action, Object demanded, IPermission permThatFailed, System.Security.Policy.Evidence evidence)
    {
      Contract.Ensures(0 <= string.Empty.Length);
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public System.Security.Permissions.SecurityAction Action
    {
      get
      {
        return default(System.Security.Permissions.SecurityAction);
      }
      set
      {
      }
    }

    public Object Demanded
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public Object DenySetInstance
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public System.Reflection.AssemblyName FailedAssemblyInfo
    {
      get
      {
        return default(System.Reflection.AssemblyName);
      }
      set
      {
      }
    }

    public IPermission FirstPermissionThatFailed
    {
      get
      {
        return default(IPermission);
      }
      set
      {
      }
    }

    public string GrantedSet
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Reflection.MethodInfo Method
    {
      get
      {
        return default(System.Reflection.MethodInfo);
      }
      set
      {
      }
    }

    public string PermissionState
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Type PermissionType
    {
      get
      {
        return default(Type);
      }
      set
      {
      }
    }

    public Object PermitOnlySetInstance
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public string RefusedSet
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Url
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public SecurityZone Zone
    {
      get
      {
        return default(SecurityZone);
      }
      set
      {
      }
    }
    #endregion
  }
}
