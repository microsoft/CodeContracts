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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;
using System.IO;
using System.Xml;


namespace Microsoft.Research.Cloudot.Common
{
  /// <summary>
  /// Generic class for Cloudot Logging
  /// </summary>
  public static class CloudotLogging
  {
    public static string CloudotOutputDirectory
    {
      get
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft", "Cloudot");
      }
    }

    private static string _logFileNameCached = null;
    
    public static string LogFileName
    {
      get
      {
        if (_logFileNameCached == null)
        {
          _logFileNameCached = Path.Combine(CloudotOutputDirectory, "CloudotLog.xml");
        }
        return _logFileNameCached;
      }
    }


    /// <summary>
    /// For the moment only a wrapper around Console.WriteLine
    /// We will extend it to take persist the log on disk
    /// </summary>
    static public void WriteLine(string format, params object[] args)
    {
      Contract.Requires(format != null);

      Console.WriteLine(format.PrefixWithCurrentTime(), args);
    }

    public static void WriteLine()
    {
      Console.WriteLine();
    }

    public static void LogAnalysiRequest(string what, string[] args)
    {
      try
      {
        EnsureLogFileIsInitialized();
        Log(what, DateTime.Now.ToString(), args);
      }
      catch(Exception)
      {
        CloudotLogging.WriteLine("Something went wrong while writing into log file. We swallow the exception and continue");
      }

    }

    #region Log file
    private static readonly object mylock = new object();
    private static XmlDocument logFile;

    static private void EnsureLogFileIsInitialized()
    {
      lock (mylock)
      {
        if (logFile == null)
        {
          logFile = new XmlDocument();

          if (File.Exists(LogFileName))
          {
            logFile.Load(LogFileName);
          }
          else
          {
            logFile.AppendChild(logFile.CreateElement("CommandLines"));
          }
        }
      }
    }

    static private void Log(string what, string when, string[] arguments)
    {
      lock (mylock)
      {
        var el = (XmlElement)logFile.DocumentElement.AppendChild(logFile.CreateElement("AnalysisRequest"));
        el.SetAttribute("What", what);
        el.SetAttribute("When", when);
        if (arguments != null)
        {
          foreach (var arg in arguments)
          {
            el.AppendChild(logFile.CreateElement("Argument")).InnerText = arg;
          }
        }
        var dir = Path.GetDirectoryName(LogFileName);
        if(!Directory.Exists(dir))
        {
          Directory.CreateDirectory(dir);
        }
        logFile.Save(LogFileName);
      }
    }

    #endregion

    public static StreamWriter GetAFreshOutputFileName(string name, out string fileName)
    {
      Contract.Requires(name != null);

      var dir = Path.Combine(CloudotOutputDirectory, "Output", name);
      if(!Directory.Exists(dir))
      {
        Directory.CreateDirectory(dir);
      }
      var retry=0;
      do
      {
        fileName = Path.Combine(dir, string.Format("{0}.{1}.{2}", name, retry, "txt"));
        retry++;
      } while (File.Exists(fileName));
      try
      {
        return File.CreateText(fileName);
      }
      catch(Exception)
      {
        fileName = "<no name - An exception had occurred>";
        return null;
      }
    }

  }
}
