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

using System.Diagnostics.Contracts;
using System;

namespace System
{

  public static class Environment
  {

    public enum SpecialFolder
    {
      // Fields
      ApplicationData = 0x1a,
      Favorites = 6,
      Personal = 5,
      Programs = 2,
      StartMenu = 11,
      Startup = 7,
#if !SILVERLIGHT_4_0_WP
      CommonApplicationData = 0x23,
      CommonProgramFiles = 0x2b,
      Cookies = 0x21,
      Desktop = 0,
      DesktopDirectory = 0x10,
      History = 0x22,
      InternetCache = 0x20,
      LocalApplicationData = 0x1c,
      MyComputer = 0x11,
      MyMusic = 13,
      MyPictures = 0x27,
      ProgramFiles = 0x26,
      Recent = 8,
      SendTo = 9,
      System = 0x25,
      Templates = 0x15
#endif
    }
#if NETFRAMEWORK_4_0
    public enum SpecialFolderOption {
        None = 0,
        Create = 32768,
        DoNotVerify = 16384,
    }
#endif

#if !SILVERLIGHT_4_0_WP
    extern public static int ExitCode
    {
      get;
      set;
    }
#endif

#if !SILVERLIGHT
    extern public static bool UserInteractive
    {
      get;
    }

    extern public static Int64 WorkingSet
    {
      get;
    }

    public static string MachineName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    public static string CommandLine
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#endif

    public static string NewLine
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(Contract.Result<string>().Length > 0);
        return default(string);
      }
    }

#if !SILVERLIGHT
    public static string UserDomainName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(Contract.Result<string>().Length > 0);
        return default(string);
      }
    }

    public static string UserName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(Contract.Result<string>().Length > 0);
        return default(string);
      }
    }
#endif

    extern public static int TickCount
    {
      get;
    }
#if !SILVERLIGHT

    public static string SystemDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(Contract.Result<string>().Length > 0);
        return default(string);
      }
    }

    extern public static string StackTrace
    {
      get;
    }
#endif

    public static string CurrentDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(Contract.Result<string>().Length > 0);
        return default(string);
      }

#if !SILVERLIGHT_5_0
      set
      {
        Contract.Requires(value != null);
        Contract.Requires(value.Length > 0);
      }
#endif
    }

    public static string GetFolderPath(SpecialFolder folder)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

#if NETFRAMEWORK_4_0
    public static string GetFolderPath(SpecialFolder folder, SpecialFolderOption options) {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
    }
#endif

#if !SILVERLIGHT
    public static String[] GetLogicalDrives()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);
      return default(String[]);
    }

    public static System.Collections.IDictionary GetEnvironmentVariables()
    {
      Contract.Ensures(Contract.Result<System.Collections.IDictionary>() != null);
      
      return default(System.Collections.IDictionary);
    }

    public static string GetEnvironmentVariable(string variable)
    {
      Contract.Requires(variable != null);

      return default(string);
    }
#endif


#if !SILVERLIGHT
    [Pure]
    public static String[] GetCommandLineArgs()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.Result<string[]>().Length >= 1); // The documentation implies that there is at list one element

      return default(String[]);
    }

    public static string ExpandEnvironmentVariables(string str)
    {
      Contract.Requires(str != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static void Exit(int exitCode)
    {
      Contract.Ensures(false);
    }
#endif

#if NETFRAMEWORK_4_0
    public static OperatingSystem OSVersion {
        get {
            Contract.Ensures(Contract.Result<OperatingSystem>() != null);
            return default(OperatingSystem);
        }
    }
#endif
  }
}
