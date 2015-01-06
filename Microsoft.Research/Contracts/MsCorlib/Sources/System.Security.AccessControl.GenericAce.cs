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

// File System.Security.AccessControl.GenericAce.cs
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


namespace System.Security.AccessControl
{
  abstract public partial class GenericAce
  {
    #region Methods and constructors
    public static bool operator != (GenericAce left, GenericAce right)
    {
      Contract.Ensures(Contract.Result<bool>() == ((left.Equals(right)) == false));

      return default(bool);
    }

    public static bool operator == (GenericAce left, GenericAce right)
    {
      return default(bool);
    }

    public GenericAce Copy()
    {
      Contract.Requires(0 <= this.BinaryLength);
      Contract.Ensures(Contract.Result<System.Security.AccessControl.GenericAce>() != null);

      return default(GenericAce);
    }

    public static GenericAce CreateFromBinaryForm(byte[] binaryForm, int offset)
    {
      Contract.Requires((offset + 3) < binaryForm.Length);
      Contract.Requires(0 <= offset);
      Contract.Ensures(Contract.Result<System.Security.AccessControl.GenericAce>() != null);

      return default(GenericAce);
    }

    public sealed override bool Equals(Object o)
    {
      return default(bool);
    }

    internal GenericAce()
    {
    }

    public abstract void GetBinaryForm(byte[] binaryForm, int offset);

    public sealed override int GetHashCode()
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public AceFlags AceFlags
    {
      get
      {
        return default(AceFlags);
      }
      set
      {
      }
    }

    public AceType AceType
    {
      get
      {
        return default(AceType);
      }
    }

    public AuditFlags AuditFlags
    {
      get
      {
        Contract.Ensures(((System.Security.AccessControl.AuditFlags)(0)) <= Contract.Result<System.Security.AccessControl.AuditFlags>());
        Contract.Ensures(Contract.Result<System.Security.AccessControl.AuditFlags>() <= ((System.Security.AccessControl.AuditFlags)(3)));

        return default(AuditFlags);
      }
    }

    public abstract int BinaryLength
    {
      get;
    }

    public InheritanceFlags InheritanceFlags
    {
      get
      {
        Contract.Ensures(((System.Security.AccessControl.InheritanceFlags)(0)) <= Contract.Result<System.Security.AccessControl.InheritanceFlags>());
        Contract.Ensures(Contract.Result<System.Security.AccessControl.InheritanceFlags>() <= ((System.Security.AccessControl.InheritanceFlags)(3)));

        return default(InheritanceFlags);
      }
    }

    public bool IsInherited
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == (((byte)((this.AceFlags & 16)) == 0) == false));

        return default(bool);
      }
    }

    public PropagationFlags PropagationFlags
    {
      get
      {
        Contract.Ensures(((System.Security.AccessControl.PropagationFlags)(0)) <= Contract.Result<System.Security.AccessControl.PropagationFlags>());
        Contract.Ensures(Contract.Result<System.Security.AccessControl.PropagationFlags>() <= ((System.Security.AccessControl.PropagationFlags)(3)));

        return default(PropagationFlags);
      }
    }
    #endregion
  }
}
