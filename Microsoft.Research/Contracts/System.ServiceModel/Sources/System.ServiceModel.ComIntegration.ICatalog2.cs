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

// File System.ServiceModel.ComIntegration.ICatalog2.cs
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


namespace System.ServiceModel.ComIntegration
{
  internal partial interface ICatalog2
  {
    #region Methods and constructors
    void AliasComponent(string bstrSrcApplicationIDOrName, string bstrCLSIDOrProgID, string bstrDestApplicationIDOrName, string bstrNewProgId, string bstrNewClsid);

    bool AreApplicationInstancesPaused(Object pVarApplicationInstanceID);

    void BackupREGDB(string bstrBackupFilePath);

    Object Connect(string connectStr);

    void CopyApplications(string bstrSourcePartitionIDOrName, Object pVarApplicationID, string bstrDestinationPartitionIDOrName);

    void CopyComponents(string bstrSourceApplicationIDOrName, Object pVarCLSIDOrProgID, string bstrDestinationApplicationIDOrName);

    void CreateServiceForApplication(string bstrApplicationIDOrName, string bstrServiceName, string bstrStartType, string bstrErrorControl, string bstrDependencies, string bstrRunAs, string bstrPassword, bool bDesktopOk);

    void CurrentPartition(string bstrPartitionIDOrName);

    string CurrentPartitionID();

    string CurrentPartitionName();

    void DeleteServiceForApplication(string bstrApplicationIDOrName);

    string DumpApplicationInstance(string bstrApplicationInstanceID, string bstrDirectory, int lMaxImages);

    void ExportApplication(string bstrApplIdOrName, string bstrApplicationFile, int lOptions);

    void ExportPartition(string bstrPartitionIDOrName, string bstrPartitionFileName, int lOptions);

    void FlushPartitionCache();

    string GetApplicationInstanceIDFromProcessID(int lProcessID);

    Object GetCollection(string bstrCollName);

    Object GetCollectionByQuery(string collName, ref Object[] aQuery);

    Object GetCollectionByQuery2(string bstrCollectionName, Object pVarQueryStrings);

    int GetComponentVersionCount(string bstrCLSIDOrProgID);

    void GetEventClassesForIID(string bstrIID, out Object[] varCLSIDS, out Object[] varProgIDs, out Object[] varDescriptions);

    void GetMultipleComponentsInfo(string bstrApplIdOrName, Object varFileNames, out Object[] varCLSIDS, out Object[] varClassNames, out Object[] varFileFlags, out Object[] varComponentFlags);

    string GetPartitionID(string bstrApplicationIDOrName);

    string GetPartitionName(string bstrApplicationIDOrName);

    string GlobalPartitionID();

    void ImportComponent(string bstrApplIdOrName, string bstrCLSIDOrProgId);

    void ImportComponents(string bstrApplicationIDOrName, Object pVarCLSIDOrProgID, Object pVarComponentType);

    void ImportUnconfiguredComponents(string bstrApplicationIDOrName, Object pVarCLSIDOrProgID, Object pVarComponentType);

    void InstallApplication(string bstrApplicationFile, string bstrDestinationDirectory, int lOptions, string bstrUserId, string bstrPassword, string bstrRSN);

    void InstallComponent(string bstrApplIdOrName, string bstrDLL, string bstrTLB, string bstrPSDLL);

    void InstallEventClass(string bstrApplIdOrName, string bstrDLL, string bstrTLB, string bstrPSDLL);

    void InstallMultipleComponents(string bstrApplIdOrName, ref Object[] fileNames, ref Object[] CLSIDS);

    void InstallMultipleEventClasses(string bstrApplIdOrName, ref Object[] fileNames, ref Object[] CLSIDS);

    void InstallPartition(string bstrFileName, string bstrDestDirectory, int lOptions, string bstrUserID, string bstrPassword, string bstrRSN);

    bool Is64BitCatalogServer();

    bool IsApplicationInstanceDumpSupported();

    Object IsSafeToDelete(string bstrDllName);

    int MajorVersion();

    int MinorVersion();

    void MoveComponents(string bstrSourceApplicationIDOrName, Object pVarCLSIDOrProgID, string bstrDestinationApplicationIDOrName);

    void PauseApplicationInstances(Object pVarApplicationInstanceID);

    void PromoteUnconfiguredComponents(string bstrApplicationIDOrName, Object pVarCLSIDOrProgID, Object pVarComponentType);

    void QueryApplicationFile(string bstrApplicationFile, out string bstrApplicationName, out string bstrApplicationDescription, out bool bHasUsers, out bool bIsProxy, out Object[] varFileNames);

    Object QueryApplicationFile2(string bstrApplicationFile);

    void RecycleApplicationInstances(Object pVarApplicationInstanceID, int lReasonCode);

    void RefreshComponents();

    void RefreshRouter();

    void Reserved1();

    void Reserved2();

    void RestoreREGDB(string bstrBackupFilePath);

    void ResumeApplicationInstances(Object pVarApplicationInstanceID);

    int ServiceCheck(int lService);

    void ShutdownApplication(string bstrApplIdOrName);

    void ShutdownApplicationInstances(Object pVarApplicationInstanceID);

    void StartApplication(string bstrApplIdOrName);

    void StartRouter();

    void StopRouter();
    #endregion
  }
}
