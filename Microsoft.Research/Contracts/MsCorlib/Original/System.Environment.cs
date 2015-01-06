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

    public class Environment
    {
    
        public enum SpecialFolder
        {
            // Fields
            ApplicationData = 0x1a,
            CommonApplicationData = 0x23,
            CommonProgramFiles = 0x2b,
            Cookies = 0x21,
            Desktop = 0,
            DesktopDirectory = 0x10,
            Favorites = 6,
            History = 0x22,
            InternetCache = 0x20,
            LocalApplicationData = 0x1c,
            MyComputer = 0x11,
            MyMusic = 13,
            MyPictures = 0x27,
            Personal = 5,
            ProgramFiles = 0x26,
            Programs = 2,
            Recent = 8,
            SendTo = 9,
            StartMenu = 11,
            Startup = 7,
            System = 0x25,
            Templates = 0x15
        }

        public static int ExitCode
        {
          get;
          set;
        }

        public static OperatingSystem OSVersion
        {
          get;
        }

        public static bool UserInteractive
        {
          get;
        }

        public static Int64 WorkingSet
        {
          get;
        }

        public static string! MachineName
        {
          get;
        }

        public static string CommandLine
        {
          get;
        }

        public static string! NewLine
        {
          get;
        }

        public static string UserDomainName
        {
          get;
        }

        public static string UserName
        {
          get;
        }

        public static bool HasShutdownStarted
        {
          get;
        }

        public static Version Version
        {
          get;
        }

        public static int TickCount
        {
          get;
        }

        public static string SystemDirectory
        {
          get;
        }

        public static string StackTrace
        {
          get;
        }

        public static string! CurrentDirectory
        {
          get;
          
          set
            CodeContract.Requires(value != null);
            CodeContract.Requires(value != "");
            //throws System.IO.IOException, System.IO.FileNotFoundException, System.IO.DirectoryNotFoundException, System.Security.SecurityException;
        }

        public static string GetFolderPath (SpecialFolder folder) {

          return default(string);
        }
        public static String[] GetLogicalDrives () {

          return default(String[]);
        }
        public static System.Collections.IDictionary GetEnvironmentVariables () {

          return default(System.Collections.IDictionary);
        }
        public static string GetEnvironmentVariable (string! variable) {
            CodeContract.Requires(variable != null);

          return default(string);
        }
        public static String[] GetCommandLineArgs () {

          return default(String[]);
        }
        public static string ExpandEnvironmentVariables (string! name) {
            CodeContract.Requires(name != null);

          return default(string);
        }
        public static void Exit (int exitCode) {
        }
    }
}
