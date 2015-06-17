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

// File System.Environment.cs
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


namespace System
{
  static public partial class Environment
  {
    #region Enums
    public enum SpecialFolder
    {
      ApplicationData = 26, 
      CommonApplicationData = 35, 
      LocalApplicationData = 28, 
      Cookies = 33, 
      Desktop = 0, 
      Favorites = 6, 
      History = 34, 
      InternetCache = 32, 
      Programs = 2, 
      MyComputer = 17, 
      MyMusic = 13, 
      MyPictures = 39, 
      Recent = 8, 
      SendTo = 9, 
      StartMenu = 11, 
      Startup = 7, 
      System = 37, 
      Templates = 21, 
      DesktopDirectory = 16, 
      Personal = 5, 
      MyDocuments = 5, 
      ProgramFiles = 38, 
      CommonProgramFiles = 43, 
      AdminTools = 48, 
      CDBurning = 59, 
      CommonAdminTools = 47, 
      CommonDocuments = 46, 
      CommonMusic = 53, 
      CommonOemLinks = 58, 
      CommonPictures = 54, 
      CommonStartMenu = 22, 
      CommonPrograms = 23, 
      CommonStartup = 24, 
      CommonDesktopDirectory = 25, 
      CommonTemplates = 45, 
      CommonVideos = 55, 
      Fonts = 20, 
      MyVideos = 14, 
      NetworkShortcuts = 19, 
      PrinterShortcuts = 27, 
      UserProfile = 40, 
      CommonProgramFilesX86 = 44, 
      ProgramFilesX86 = 42, 
      Resources = 56, 
      LocalizedResources = 57, 
      SystemX86 = 41, 
      Windows = 36, 
    }

    public enum SpecialFolderOption
    {
      None = 0, 
      Create = 32768, 
      DoNotVerify = 16384, 
    }
    #endregion

    #region Methods and constructors
    public static void Exit(int exitCode)
    {
    }

    public static string ExpandEnvironmentVariables(string name)
    {
      return default(string);
    }

    public static void FailFast(string message, Exception exception)
    {
    }

    public static void FailFast(string message)
    {
    }

    public static string[] GetCommandLineArgs()
    {
      return default(string[]);
    }

    public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
    {
      return default(string);
    }

    public static string GetEnvironmentVariable(string variable)
    {
      return default(string);
    }

    public static System.Collections.IDictionary GetEnvironmentVariables()
    {
      Contract.Ensures(Contract.Result<System.Collections.IDictionary>() != null);

      return default(System.Collections.IDictionary);
    }

    public static System.Collections.IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
    {
      Contract.Ensures(Contract.Result<System.Collections.IDictionary>() != null);

      return default(System.Collections.IDictionary);
    }

    public static string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
    {
      return default(string);
    }

    public static string GetFolderPath(Environment.SpecialFolder folder)
    {
      return default(string);
    }

    public static string[] GetLogicalDrives()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    public static void SetEnvironmentVariable(string variable, string value)
    {
      Contract.Ensures(0 <= variable.Length);
      Contract.Ensures(variable.Length <= 32766);
    }

    public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
    {
      Contract.Ensures(0 <= variable.Length);
      Contract.Ensures(variable.Length <= 32766);
    }
    #endregion

    #region Properties and indexers
    public static string CommandLine
    {
      get
      {
        return default(string);
      }
    }

    public static string CurrentDirectory
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public static int ExitCode
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public static bool HasShutdownStarted
    {
      get
      {
        return default(bool);
      }
    }

    public static bool Is64BitOperatingSystem
    {
      get
      {
        return default(bool);
      }
    }

    public static bool Is64BitProcess
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == false);

        return default(bool);
      }
    }

    public static string MachineName
    {
      get
      {
        return default(string);
      }
    }

    public static string NewLine
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(Contract.Result<string>() == @"
");

        return default(string);
      }
    }

    public static OperatingSystem OSVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<System.OperatingSystem>() != null);

        return default(OperatingSystem);
      }
    }

    public static int ProcessorCount
    {
      get
      {
        return default(int);
      }
    }

    public static string StackTrace
    {
      get
      {
        return default(string);
      }
    }

    public static string SystemDirectory
    {
      get
      {
        return default(string);
      }
    }

    public static int SystemPageSize
    {
      get
      {
        return default(int);
      }
    }

    public static int TickCount
    {
      get
      {
        return default(int);
      }
    }

    public static string UserDomainName
    {
      get
      {
        return default(string);
      }
    }

    public static bool UserInteractive
    {
      get
      {
        return default(bool);
      }
    }

    public static string UserName
    {
      get
      {
        return default(string);
      }
    }

    public static Version Version
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Version>() != null);

        return default(Version);
      }
    }

    public static long WorkingSet
    {
      get
      {
        return default(long);
      }
    }
    #endregion
  }
}
