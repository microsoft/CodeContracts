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

// File System.IO.FileSystemInfo.cs
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


namespace System.IO
{
  abstract public partial class FileSystemInfo : MarshalByRefObject, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public abstract void Delete();

    protected FileSystemInfo()
    {
    }

    protected FileSystemInfo(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public void Refresh()
    {
    }
    #endregion

    #region Properties and indexers
    public FileAttributes Attributes
    {
      get
      {
        Contract.Ensures(((System.IO.FileAttributes)(Int32.MinValue)) <= Contract.Result<System.IO.FileAttributes>());
        Contract.Ensures(Contract.Result<System.IO.FileAttributes>() <= ((System.IO.FileAttributes)(Int32.MaxValue)));

        return default(FileAttributes);
      }
      set
      {
      }
    }

    public DateTime CreationTime
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public DateTime CreationTimeUtc
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public abstract bool Exists
    {
      get;
    }

    public string Extension
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(this.FullPath != null);

        return default(string);
      }
    }

    public virtual new string FullName
    {
      get
      {
        return default(string);
      }
    }

    public DateTime LastAccessTime
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public DateTime LastAccessTimeUtc
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public DateTime LastWriteTime
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public DateTime LastWriteTimeUtc
    {
      get
      {
        return default(DateTime);
      }
      set
      {
      }
    }

    public abstract string Name
    {
      get;
    }
    #endregion

    #region Fields
    protected string FullPath;
    protected string OriginalPath;
    #endregion
  }
}
