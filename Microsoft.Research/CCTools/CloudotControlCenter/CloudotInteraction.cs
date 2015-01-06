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
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Research.CodeAnalysis;
using System.IO;
using System.Windows.Documents;


namespace CloudotControlCenter
{
  internal class CloudotInteraction
  {
    public static void FireAnalysisAndForget(string cloudotAddress, string assembly, string[] commandLine, Action<string> Logger)
    {
      var workerThread = new Worker(cloudotAddress, assembly, commandLine, Logger);

      workerThread.Start();
    }

    class Worker
    {
      static private uint Id = 0;

      readonly private string CloudotAddress;
      readonly private string Assembly;
      readonly private string[] CommandLine;
      readonly private uint myId;
      readonly Action<string> Logger;

      public Worker(string cloudotAddress, string assembly, string[] commandline, Action<string> Logger)
      {
        Contract.Requires(cloudotAddress != null);
        Contract.Requires(assembly != null);
        Contract.Requires(commandline != null);
        Contract.Requires(Logger != null);

        this.CloudotAddress = cloudotAddress;
        this.Assembly = assembly;
        this.CommandLine = commandline;
        this.myId = Id++;
        this.Logger = Logger;
      }

      public void Start()
      {
        var thread = ThreadPool.QueueUserWorkItem(this.InvokeCloudotAndWait);
      }

      private string GetOutputFile()
      {
        var outFile =  Path.Combine(Path.GetTempPath(), Path.Combine("Microsoft", "Cloudot", this.Assembly, this.myId + ".txt"));
        var dir = Path.GetDirectoryName(outFile);
        if(!Directory.Exists(dir))
        {
          Directory.CreateDirectory(dir);
        }

        return outFile;
      }
      private void InvokeCloudotAndWait(object threadContext)
      {
        var fileName = this.GetOutputFile();
        using (var outStream = new StreamWriter(fileName, false))
        {
          Logger(string.Format("Saving out in {0}", fileName));

          string[] dummy;
          var result = ClousotViaService.Main2(this.CommandLine, outStream, out dummy, CloudotAddress);
          Logger(string.Format("Cloudot invocation {0} exited with {1}", this.myId, result));
          Logger(string.Format("Result of invocation {0} wrote in {1}", this.myId, fileName));
        }
      }
    }
  }
}
