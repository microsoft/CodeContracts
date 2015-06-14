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
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Microsoft.Research.Cloudot.Common;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.Cloudot
{
  public class ProcessPool : IDisposable
  {

    private const int INITIALPOOLSIZE = 4;

#if !TEST
    static string PathEXE = "C:\\Program Files (x86)\\Microsoft\\Contracts\\Bin\\cccheck.exe"; // TODO:change!
#else
    static string PathEXE = "c:\\cci\\Microsoft.Research\\Clousot\\bin\\Debug\\clousot.exe"; // TODO:change!
#endif


    readonly private Queue<Process> availableWorkers;
    readonly private List<Process> scheduledWorkers;

    public ProcessPool(int initialCount = INITIALPOOLSIZE)
    {
      Contract.Requires(initialCount >= 0);
      this.availableWorkers = new Queue<Process>();
      this.scheduledWorkers = new List<Process>();

      CreateProcessesAndPopulateTheQueue(initialCount);
    }

    private void CreateProcessesAndPopulateTheQueue(int initialCount = INITIALPOOLSIZE)
    {
      Contract.Requires(initialCount >= 0);

      lock (this.availableWorkers)
      {
        CloudotLogging.WriteLine("Creating {0} workers", initialCount);
        for (var i = 0; i < initialCount; i++)
        {
          var processInfo = SetupProcessInfoToWait();

          try
          {
            var exe = Process.Start(processInfo);
            if (exe != null)
            {
              this.availableWorkers.Enqueue(exe);
            }
            else
            {
              CloudotLogging.WriteLine("Failed to create the Clousot process");
            }
          }
          catch
          {
            CloudotLogging.WriteLine("Error in starting the Clousot/cccheck process for the pool");
          }
        }
      }
    }

    public Process GetAProcessFromTheQueue()
    {
      lock (this.availableWorkers)
      {
        if (this.availableWorkers.Count == 0)
        {
          // TODO: This should happen after the dequeue and asyncronously
          CreateProcessesAndPopulateTheQueue();
        }

        // Get a worker from the worker queue 
        var exe =  this.availableWorkers.Dequeue();

        // It may be the case the exe was killed somehow
        if(exe.HasExited)
        {
          return GetAProcessFromTheQueue();
        }

        // remember we used this worker
        this.scheduledWorkers.Add(exe);
        return exe;
      }
    }

    static private ProcessStartInfo SetupProcessInfo(int whichHalf, ProcessStartInfo processInfo, string[] args)
    {
      Contract.Requires(processInfo != null);
      Contract.Requires(args != null);

      string half;
      switch (whichHalf)
      {
        case 1:
        case 2:
          half = string.Format(" -splitanalysis {0} -usecallgraph=false", whichHalf);
          break;

        default:
          half = "";
          break;
      }

      processInfo.FileName = PathEXE;
      processInfo.CreateNoWindow = false;
      processInfo.UseShellExecute = false;
      processInfo.WindowStyle = ProcessWindowStyle.Hidden;
      processInfo.RedirectStandardOutput = true;
      processInfo.RedirectStandardError = true;
      processInfo.Arguments = string.Join(" ", args) + half + " " + StringConstants.DoNotUseCloudot; // We use the convention that +cloudot means we should not use the service

      return processInfo;
    }

    static private ProcessStartInfo SetupProcessInfoToWait()
    {
      Contract.Ensures(Contract.Result<ProcessStartInfo>() != null);

      var processInfo = new ProcessStartInfo();

      processInfo.FileName = PathEXE;
      processInfo.CreateNoWindow = false;
      processInfo.UseShellExecute = false;
      processInfo.WindowStyle = ProcessWindowStyle.Hidden;
      processInfo.RedirectStandardOutput = true;
      processInfo.RedirectStandardError = true;
      processInfo.Arguments = StringConstants.ClousotWait; // We use the convention that +cloudot means we should not use the service

      return processInfo;
    }
  
    #region Clean up code for the childs

    ~ProcessPool()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool Disposing)
    {
      // We want to make sure there are no zombie processes left
      CloudotLogging.WriteLine("Killing all the workers");
      foreach (var exe in this.availableWorkers)
      {
        try
        {
          if (!exe.HasExited)
          {
            exe.Kill();
          }
        }
        catch(Exception e)
        {
          CloudotLogging.WriteLine("[Debug] Exception raised while trying to kill the idle process {0} -- We just continue as it means that the OS has already removed the process", exe.Id);
          CloudotLogging.WriteLine("[Debug] Exception {0}", e);
        }
      }

      foreach (var exe in this.scheduledWorkers)
      {
        try
        {
          if(!exe.HasExited)
          {
            exe.Kill();
          }
        }
        catch(Exception e)
        {
          CloudotLogging.WriteLine("Exception {0} raised while trying to kill an active process -- We just continue as it means that the process was already terminated", e.GetType());
        }
      }
    }

#endregion
  }
}
