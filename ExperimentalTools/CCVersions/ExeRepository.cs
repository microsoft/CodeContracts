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
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;

namespace CCVersions
{
  #region ExeVersionSpec

  class ExeVersionSpec : ISourceManagerVersionSpec
  {
    int? revnum;

    public ExeVersionSpec(string value)
    {
      int rn;
      if (Int32.TryParse(value, out rn))
      {
        this.revnum = rn < 0 ? 0 : rn;
      }
    }
    public ExeVersionSpec(int revNum)
    {
      this.revnum = revNum;
    }
    public override string ToString()
    {
      if (this.revnum.HasValue)
      {
        var res = this.revnum.Value.ToString();
        Contract.Assert(res.Length >= 1);
        return res;
      }
      else
      {
        return "NULL";
      }
    }

    public long Id
    {
      get
      {
        if (this.revnum.HasValue)
        {
          return this.revnum.Value;
        }
        else
        {
          return -1;
        }
      }
    }

    public bool IsNull
    {
      get
      {
        return !this.revnum.HasValue;
      }
    }
  }

  #endregion ExeVersionSpec

  #region ExeRepository

  /// <summary>
  /// Abstraction for calling repository managers in the form of external .exe
  /// </summary>
  abstract class ExeRepository
  {
    #region ExeNotInstalledException

    public class ExeNotInstalledException : Exception
    {
      public ExeNotInstalledException()
      {
      }

      public ExeNotInstalledException(string message)
        : base(message)
      {
      }

      public ExeNotInstalledException(string message, Exception inner)
        : base(message, inner)
      {
      }
    }

    #endregion

    #region ExeInvokeException
    public class ExeInvokeException : Exception
    {
      public ExeInvokeException()
      {
      }
      public ExeInvokeException(string message)
        : base(message)
      {
      }
      public ExeInvokeException(string message, Exception inner)
        : base(message, inner)
      {
      }
    }
    #endregion ExeInvokeException

    #region InvokeResults
    protected class InvokeResults
    {
      public virtual string StdOut { get; protected set; }
      public virtual string StdErr { get; protected set; }
      public virtual int ExitCode { get; protected set; }
      protected InvokeResults() { }
      public InvokeResults(string stdout, string stderr, int exitcode)
      {
        this.StdOut = stdout;
        this.StdErr = stderr;
        this.ExitCode = exitcode;
      }
    }

    private class WaitInvokeResults : InvokeResults
    {
      public WaitInvokeResults(Process process, int timeoutMilliseconds, string stdinput)
      {
        this.StdOut = "";
        this.StdErr = "";
        process.OutputDataReceived += this.onOutputDataReceived;
        process.ErrorDataReceived += this.onErrorDataReceived;
        process.Start();
        if (stdinput != null)
        {
          process.StandardInput.Write(stdinput);
        }
        process.StandardInput.Flush();
        process.StandardInput.Close();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        if (timeoutMilliseconds > 0)
          process.WaitForExit(timeoutMilliseconds);
        else
          process.WaitForExit();
      }
      private void onOutputDataReceived(object sender, DataReceivedEventArgs line)
      {
        this.StdOut += line.Data + '\n';
      }
      private void onErrorDataReceived(object sender, DataReceivedEventArgs line)
      {
        this.StdErr += line.Data + '\n';
      }
    }

#if false // Causes deadlocks when the output is too large
    private class LazyInvokeResults : InvokeResults
    {
      private string _stdout;
      private string _stderr;
      private readonly Process _process;
      public override int ExitCode { get { return _process.ExitCode; } }
      public override string StdOut
      {
        get
        {
          if (this._stdout == null)
          {
            this._stdout = this._process.StandardOutput.ReadToEnd();
          }
          return this._stdout;
        }
      }
      public override string StdErr
      {
        get
        {
          if (this._stderr == null)
          {
            this._stderr = this._process.StandardError.ReadToEnd();
          }
          return this._stderr;
        }
      }
      public LazyInvokeResults(Process process)
      {
        Contract.Requires(process != null);
        this._process = process;
      }
    }
#endif
    #endregion

    #region initialization

    static private string ExeFileName;

    abstract protected string ExeName { get; }

    virtual protected string FallbackLocateExe()
    {
      return null;
    }

    private string LocateExe()
    {
      var envPath = Environment.GetEnvironmentVariable("PATH");
      if (envPath == null)
        return null;

      foreach (var path in envPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
      {
        var fileName = Path.Combine(path, this.ExeName);
        if (File.Exists(fileName))
          return fileName;
      }
      return this.FallbackLocateExe();
    }

    static private bool initialized = false;

    public void Initialize()
    {
      if (initialized)
        return;

      ExeFileName = this.LocateExe();
      if (String.IsNullOrWhiteSpace(ExeFileName))
      {
        throw new ExeNotInstalledException();
      }

      initialized = true;
    }
    #endregion

    #region Object invariants
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.WorkingDirectory != null);
    }
    #endregion

    public readonly string WorkingDirectory;

    public virtual string CurrentInstanceDirectory { get { return this.WorkingDirectory; } }

    public ExeRepository(string workingDirectory)
    {
      Contract.Requires(workingDirectory != null);
      Initialize();
      this.WorkingDirectory = workingDirectory;
    }

    protected const int shortTimeout = 10000; // 10s
    protected const int mediumTimeout = 120000; // 2min
    protected const int longTimeout = 36000000; // 10h

    protected abstract IEnumerable<Tuple<string, string>> EnvironmentVariables { get; }

    protected InvokeResults Invoke(string arguments, int timeoutMilliseconds = 0, string stdinput = null)
    {
      Contract.Requires(!String.IsNullOrWhiteSpace(arguments));
      Contract.Ensures(Contract.Result<InvokeResults>() != null);

      var psi = new ProcessStartInfo
      {
        Arguments = arguments,
        CreateNoWindow = true,
        ErrorDialog = false,
        FileName = ExeFileName,
        RedirectStandardError = true,
        RedirectStandardInput = true,
        RedirectStandardOutput = true,
        UseShellExecute = false,
        WindowStyle = ProcessWindowStyle.Hidden,
        WorkingDirectory = this.CurrentInstanceDirectory
      };

      foreach (var kv in this.EnvironmentVariables)
      {
        psi.EnvironmentVariables.Add(kv.Item1, kv.Item2);
      }
      try
      {
        var process = new Process();
        process.StartInfo = psi;
        return new WaitInvokeResults(process, timeoutMilliseconds, stdinput);
      }
      catch (Exception e)
      {
        throw new ExeInvokeException("Arguments = \"" + arguments + "\". Refer to inner exception for details.", e);
      }
    }
  }
  #endregion
}
