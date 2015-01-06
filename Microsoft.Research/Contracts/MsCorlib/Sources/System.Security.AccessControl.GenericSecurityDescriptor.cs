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

// File System.Security.AccessControl.GenericSecurityDescriptor.cs
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
  abstract public partial class GenericSecurityDescriptor
  {
    #region Methods and constructors
    protected GenericSecurityDescriptor()
    {
    }

    public void GetBinaryForm(byte[] binaryForm, int offset)
    {
      Contract.Requires((offset + 3) < binaryForm.Length);
      Contract.Ensures((Contract.OldValue(offset) - binaryForm.Length) < 0);
      Contract.Ensures(System.Security.AccessControl.GenericSecurityDescriptor.Revision == 1);
    }

    public string GetSddlForm(AccessControlSections includeSections)
    {
      Contract.Requires(3 < this.BinaryLength);
      Contract.Ensures(System.Security.AccessControl.GenericSecurityDescriptor.Revision == 1);

      return default(string);
    }

    public static bool IsSddlConversionSupported()
    {
      Contract.Ensures(Contract.Result<bool>() == true);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public int BinaryLength
    {
      get
      {
        return default(int);
      }
    }

    public abstract ControlFlags ControlFlags
    {
      get;
    }

    public abstract System.Security.Principal.SecurityIdentifier Group
    {
      get;
      set;
    }

    public abstract System.Security.Principal.SecurityIdentifier Owner
    {
      get;
      set;
    }

    public static byte Revision
    {
      get
      {
        Contract.Ensures(Contract.Result<byte>() == 1);

        return default(byte);
      }
    }
    #endregion
  }
}
