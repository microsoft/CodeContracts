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

// File System.IO.FileInfo.cs
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
  sealed public partial class FileInfo : FileSystemInfo
  {
    #region Methods and constructors
    public StreamWriter AppendText()
    {
      Contract.Ensures(Contract.Result<System.IO.StreamWriter>() != null);

      return default(StreamWriter);
    }

    public System.IO.FileInfo CopyTo(string destFileName)
    {
      Contract.Ensures(Contract.Result<System.IO.FileInfo>() != null);

      return default(System.IO.FileInfo);
    }

    public System.IO.FileInfo CopyTo(string destFileName, bool overwrite)
    {
      Contract.Ensures(Contract.Result<System.IO.FileInfo>() != null);

      return default(System.IO.FileInfo);
    }

    public FileStream Create()
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public StreamWriter CreateText()
    {
      Contract.Ensures(Contract.Result<System.IO.StreamWriter>() != null);

      return default(StreamWriter);
    }

    public void Decrypt()
    {
    }

    public override void Delete()
    {
    }

    public void Encrypt()
    {
    }

    public FileInfo(string fileName)
    {
    }

    public System.Security.AccessControl.FileSecurity GetAccessControl(System.Security.AccessControl.AccessControlSections includeSections)
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.FileSecurity>() != null);

      return default(System.Security.AccessControl.FileSecurity);
    }

    public System.Security.AccessControl.FileSecurity GetAccessControl()
    {
      Contract.Ensures(Contract.Result<System.Security.AccessControl.FileSecurity>() != null);

      return default(System.Security.AccessControl.FileSecurity);
    }

    public void MoveTo(string destFileName)
    {
    }

    public FileStream Open(FileMode mode, FileAccess access)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public FileStream Open(FileMode mode, FileAccess access, FileShare share)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public FileStream Open(FileMode mode)
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public FileStream OpenRead()
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public StreamReader OpenText()
    {
      Contract.Ensures(Contract.Result<System.IO.StreamReader>() != null);

      return default(StreamReader);
    }

    public FileStream OpenWrite()
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream>() != null);

      return default(FileStream);
    }

    public System.IO.FileInfo Replace(string destinationFileName, string destinationBackupFileName)
    {
      Contract.Ensures(0 <= destinationFileName.Length);
      Contract.Ensures(Contract.Result<System.IO.FileInfo>() != null);

      return default(System.IO.FileInfo);
    }

    public System.IO.FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
      Contract.Ensures(0 <= destinationFileName.Length);
      Contract.Ensures(Contract.Result<System.IO.FileInfo>() != null);

      return default(System.IO.FileInfo);
    }

    public void SetAccessControl(System.Security.AccessControl.FileSecurity fileSecurity)
    {
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public DirectoryInfo Directory
    {
      get
      {
        return default(DirectoryInfo);
      }
    }

    public string DirectoryName
    {
      get
      {
        return default(string);
      }
    }

    public override bool Exists
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        Contract.Ensures(((System.IO.FileAttributes)(Int32.MinValue)) <= this.Attributes);
        Contract.Ensures(Contract.Result<bool>() == ((((this.Attributes & ((System.IO.FileAttributes)(1)))) == ((System.IO.FileAttributes)(0))) == false));
        Contract.Ensures(this.Attributes <= ((System.IO.FileAttributes)(Int32.MaxValue)));

        return default(bool);
      }
      set
      {
        Contract.Ensures(false);
      }
    }

    public long Length
    {
      get
      {
        return default(long);
      }
    }

    public override string Name
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
