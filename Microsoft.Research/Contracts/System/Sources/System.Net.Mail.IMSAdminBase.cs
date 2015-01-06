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

// File System.Net.Mail.IMSAdminBase.cs
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


namespace System.Net.Mail
{
  internal partial interface IMSAdminBase
  {
    #region Methods and constructors
    int AddKey(IntPtr handle, string Path);

    int Backup(string Location, int Version, int Flags);

    int CloseKey(IntPtr handle);

    int CopyData(IntPtr sourcehandle, string SourcePath, IntPtr desthandle, string DestPath, int Attributes, int UserType, int DataType, bool CopyFlag);

    void CopyKey(IntPtr source, string SourcePath, IntPtr dest, string DestPath, bool OverwriteFlag, bool CopyFlag);

    void DeleteAllData(IntPtr handle, string Path, uint UserType, uint DataType);

    void DeleteBackup(string Location, int Version);

    void DeleteChildKeys(IntPtr handle, string Path);

    int DeleteData(IntPtr key, string path, uint Identifier, uint DataType);

    int DeleteKey(IntPtr handle, string Path);

    void EnumBackups(out string Location, out uint Version, out System.Runtime.InteropServices.ComTypes.FILETIME BackupTime, uint EnumIndex);

    int EnumKeys(IntPtr handle, string Path, StringBuilder Buffer, int EnumKeyIndex);

    int GetAllData(IntPtr handle, string Path, uint Attributes, uint UserType, uint DataType, out uint NumDataEntries, out uint DataSetNumber, uint BufferSize, IntPtr buffer, out uint RequiredBufferSize);

    void GetDataPaths(IntPtr handle, string Path, int Identifier, int DataType, int BufferSize, out char[] Buffer, out int RequiredBufferSize);

    void GetDataSetNumber(IntPtr handle, string Path, out uint DataSetNumber);

    int GetLastChangeTime(IntPtr handle, string Path, out System.Runtime.InteropServices.ComTypes.FILETIME LastChangeTime, bool LocalTime);

    int GetServerGuid();

    void GetSystemChangeNumber(out uint SystemChangeNumber);

    int KeyExchangePhase1();

    int KeyExchangePhase2();

    void RenameKey(IntPtr key, string path, string newName);

    int Restore(string Location, int Version, int Flags);

    void SaveData();

    void SetLastChangeTime(IntPtr handle, string Path, out System.Runtime.InteropServices.ComTypes.FILETIME LastChangeTime, bool LocalTime);
    #endregion
  }
}
