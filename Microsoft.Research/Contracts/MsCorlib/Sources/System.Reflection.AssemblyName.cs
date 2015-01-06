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

// File System.Reflection.AssemblyName.cs
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


namespace System.Reflection
{
  sealed public partial class AssemblyName : System.Runtime.InteropServices._AssemblyName, ICloneable, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback
  {
    #region Methods and constructors
    public AssemblyName(string assemblyName)
    {
    }

    public AssemblyName()
    {
    }

    public Object Clone()
    {
      return default(Object);
    }

    public static System.Reflection.AssemblyName GetAssemblyName(string assemblyFile)
    {
      return default(System.Reflection.AssemblyName);
    }

    public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public byte[] GetPublicKey()
    {
      return default(byte[]);
    }

    public byte[] GetPublicKeyToken()
    {
      return default(byte[]);
    }

    public void OnDeserialization(Object sender)
    {
    }

    public static bool ReferenceMatchesDefinition(System.Reflection.AssemblyName reference, System.Reflection.AssemblyName definition)
    {
      return default(bool);
    }

    public void SetPublicKey(byte[] publicKey)
    {
    }

    public void SetPublicKeyToken(byte[] publicKeyToken)
    {
    }

    void System.Runtime.InteropServices._AssemblyName.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._AssemblyName.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._AssemblyName.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._AssemblyName.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public string CodeBase
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Globalization.CultureInfo CultureInfo
    {
      get
      {
        return default(System.Globalization.CultureInfo);
      }
      set
      {
      }
    }

    public string EscapedCodeBase
    {
      get
      {
        return default(string);
      }
    }

    public AssemblyNameFlags Flags
    {
      get
      {
        return default(AssemblyNameFlags);
      }
      set
      {
      }
    }

    public string FullName
    {
      get
      {
        return default(string);
      }
    }

    public System.Configuration.Assemblies.AssemblyHashAlgorithm HashAlgorithm
    {
      get
      {
        return default(System.Configuration.Assemblies.AssemblyHashAlgorithm);
      }
      set
      {
      }
    }

    public StrongNameKeyPair KeyPair
    {
      get
      {
        return default(StrongNameKeyPair);
      }
      set
      {
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public ProcessorArchitecture ProcessorArchitecture
    {
      get
      {
        Contract.Ensures(((System.Reflection.ProcessorArchitecture)(0)) <= Contract.Result<System.Reflection.ProcessorArchitecture>());
        Contract.Ensures(Contract.Result<System.Reflection.ProcessorArchitecture>() <= ((System.Reflection.ProcessorArchitecture)(4)));

        return default(ProcessorArchitecture);
      }
      set
      {
      }
    }

    public Version Version
    {
      get
      {
        return default(Version);
      }
      set
      {
      }
    }

    public System.Configuration.Assemblies.AssemblyVersionCompatibility VersionCompatibility
    {
      get
      {
        return default(System.Configuration.Assemblies.AssemblyVersionCompatibility);
      }
      set
      {
      }
    }
    #endregion
  }
}
