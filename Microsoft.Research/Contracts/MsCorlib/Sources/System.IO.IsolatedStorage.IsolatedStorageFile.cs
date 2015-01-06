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

// File System.IO.IsolatedStorage.IsolatedStorageFile.cs
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


namespace System.IO.IsolatedStorage
{
  sealed public partial class IsolatedStorageFile : IsolatedStorage, IDisposable
  {
    #region Methods and constructors
    public void Close()
    {
    }

    public void CopyFile(string sourceFileName, string destinationFileName)
    {
    }

    public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
    {
    }

    public void CreateDirectory(string dir)
    {
    }

    public IsolatedStorageFileStream CreateFile(string path)
    {
      Contract.Ensures(0 <= path.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFileStream>() != null);

      return default(IsolatedStorageFileStream);
    }

    public void DeleteDirectory(string dir)
    {
    }

    public void DeleteFile(string file)
    {
    }

    public bool DirectoryExists(string path)
    {
      return default(bool);
    }

    public void Dispose()
    {
    }

    public bool FileExists(string path)
    {
      return default(bool);
    }

    public DateTimeOffset GetCreationTime(string path)
    {
      Contract.Ensures(false);

      return default(DateTimeOffset);
    }

    public string[] GetDirectoryNames()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public string[] GetDirectoryNames(string searchPattern)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static System.Collections.IEnumerator GetEnumerator(IsolatedStorageScope scope)
    {
      Contract.Ensures(Contract.Result<System.Collections.IEnumerator>() != null);

      return default(System.Collections.IEnumerator);
    }

    public string[] GetFileNames(string searchPattern)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public string[] GetFileNames()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public DateTimeOffset GetLastAccessTime(string path)
    {
      Contract.Ensures(false);

      return default(DateTimeOffset);
    }

    public DateTimeOffset GetLastWriteTime(string path)
    {
      Contract.Ensures(false);

      return default(DateTimeOffset);
    }

    public static IsolatedStorageFile GetMachineStoreForApplication()
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    public static IsolatedStorageFile GetMachineStoreForAssembly()
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    public static IsolatedStorageFile GetMachineStoreForDomain()
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    protected override System.Security.Permissions.IsolatedStoragePermission GetPermission(System.Security.PermissionSet ps)
    {
      return default(System.Security.Permissions.IsolatedStoragePermission);
    }

    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type applicationEvidenceType)
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Object applicationIdentity)
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, System.Security.Policy.Evidence domainEvidence, Type domainEvidenceType, System.Security.Policy.Evidence assemblyEvidence, Type assemblyEvidenceType)
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Object domainIdentity, Object assemblyIdentity)
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    public static IsolatedStorageFile GetUserStoreForApplication()
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    public static IsolatedStorageFile GetUserStoreForAssembly()
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    public static IsolatedStorageFile GetUserStoreForDomain()
    {
      Contract.Ensures(0 <= string.Empty.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFile>() != null);

      return default(IsolatedStorageFile);
    }

    public static IsolatedStorageFile GetUserStoreForSite()
    {
      Contract.Ensures(false);

      return default(IsolatedStorageFile);
    }

    public override bool IncreaseQuotaTo(long newQuotaSize)
    {
      return default(bool);
    }

    internal IsolatedStorageFile()
    {
    }

    public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
    {
    }

    public void MoveFile(string sourceFileName, string destinationFileName)
    {
    }

    public IsolatedStorageFileStream OpenFile(string path, FileMode mode)
    {
      Contract.Ensures(0 <= path.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFileStream>() != null);

      return default(IsolatedStorageFileStream);
    }

    public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access)
    {
      Contract.Ensures(0 <= path.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFileStream>() != null);

      return default(IsolatedStorageFileStream);
    }

    public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
    {
      Contract.Ensures(0 <= path.Length);
      Contract.Ensures(Contract.Result<System.IO.IsolatedStorage.IsolatedStorageFileStream>() != null);

      return default(IsolatedStorageFileStream);
    }

    public override void Remove()
    {
    }

    public static void Remove(IsolatedStorageScope scope)
    {
    }
    #endregion

    #region Properties and indexers
    public override long AvailableFreeSpace
    {
      get
      {
        return default(long);
      }
    }

    public override ulong CurrentSize
    {
      get
      {
        return default(ulong);
      }
    }

    public static bool IsEnabled
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == true);

        return default(bool);
      }
    }

    public override ulong MaximumSize
    {
      get
      {
        return default(ulong);
      }
    }

    public override long Quota
    {
      get
      {
        return default(long);
      }
      internal set
      {
      }
    }

    public override long UsedSize
    {
      get
      {
        return default(long);
      }
    }
    #endregion
  }
}
