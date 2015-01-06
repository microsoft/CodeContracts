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
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Research.Cloudot.Common;
using Microsoft.Cci;
using Microsoft.Cci.Analysis;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;
using Microsoft.Research.ClousotPulse.Messages;

namespace Microsoft.Research.Cloudot
{
  // Main service

  // Single          ==> Only one instance of this class per service instance
  // ConcurrencyMode ==> Enables multiple calls at the same time
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
  public class ClousotService : IClousotService
  {
    #region Shared state for the service

    // The pool with the workers
    readonly static private ProcessPool workersPool = new ProcessPool();

    // Requests Ids
    static public int RequestId = 0;
   
    // CloudotDir

    const string CloudotDir = @"CloudotDir";
    const string SemanticIndexDir = @"c:\tmp\SemanticIndex"; // TODO: change it!!!!

    #endregion

    #region Session (instance-based) state

    public event Action<string[]> OnMain;

    private DateTime start;

    int myId; 

    #endregion

    #region ObjectInvariant
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
    }
    #endregion

    #region Constructor
    public ClousotService() 
    {
      CloudotLogging.WriteLine("[Debug] Inside Clousot Service constructor");
    }
    #endregion

    #region Service Contracts

    public string GetOutputDirectory()
    {
      var currTmp = string.Format(@"{0}\{1}", Path.GetTempPath(), CloudotDir);
      if(!Directory.Exists(currTmp))
      {
        Directory.CreateDirectory(currTmp);
      }
      string result;
      if(FileSystemAbstractions.TryGetUniversalName(currTmp, out result))
      {
        return result;
      }
      else
      {
        return null;
      }
    }

    public int AnalyzeUsingAnalysisPackage(string name, string packageLocation, string encodingWebName)
    {
      // Step 0: initialize the server
      var textWriter = InitializeServiceInstance(new string[] { packageLocation }, encodingWebName);

      // Step 1: Compute the location in the Semantic index
      var semanticIndexLocation = ComputeSemanticIndexLocation(name);

      // Step 1: restore the package
      string[] clousotOptions;
      if(FileTransfer.TryRestoreAnalysisPackage(packageLocation, semanticIndexLocation, out clousotOptions))
      {
        CloudotLogging.WriteLine("Package restored, command line on the server {0}", String.Join(" ", clousotOptions));
        return MainInternal(name, clousotOptions, textWriter);
      }

      return -1;
    }


    public int Main(string[] args, string encodingWebName)
    {
      var textWriter = InitializeServiceInstance(args, encodingWebName);

      return MainInternal("", args, textWriter);
    }

    private int MainInternal(string name, string[] args, TextWriterAsync textWriter)
    {
      Contract.Requires(name != null);
      Contract.Requires(args != null);
      Contract.Requires(textWriter != null);
      try
      {
        CloudotLogging.LogAnalysiRequest(name, args);
#if INTERNALCALL
        return CallClousotInternally(name, args, textWriter);
#else
        return CallClousotViaProcess(name, args, textWriter);
#endif
      }
      catch (ExitRequestedException e)
      {
        return e.ExitCode;
      }
      catch (Exception e)
      {
        CloudotLogging.WriteLine("Got an exception. Reporting it to the client and aborting the session.");
        // We need to catch all exception, otherwise the service is turned into an faulted state and remains unusable
        textWriter.WriteLine("Cloudot: caught exception: {0}".PrefixWithCurrentTime(), e);
        if (e.InnerException != null)
        {
          textWriter.WriteLine("Inner exception: {0}", e.InnerException);
        }
        return -4444;
      }
      finally
      {
        CloudotLogging.WriteLine("Analysis of request #{0} done in {1}", this.myId, DateTime.Now - this.start);
        textWriter.Close(); // make sure all pending output is flushed
      }
    }

    /// <summary>
    /// Common initialization for the service.
    /// Essentially writes down some logging string and getting the callback on which the service will respond
    /// </summary>
    /// <returns>The text writer object the service will write on</returns>
    private TextWriterAsync InitializeServiceInstance(string[] args, string encodingWebName)
    {
      Contract.Requires(args != null);

      // Update the request, and save some statistics
      this.myId = RequestId++;
      this.start = DateTime.Now;

      if (this.OnMain != null)
      {
        this.OnMain(args);
      }

      // here is the magic: the callback object is recovered in the line below
      var callbackLineWriter = OperationContext.Current.GetCallbackChannel<IVerySimpleLineWriter>();

      // And now we create the output factory for Clousot
      var textWriter = new TextWriterAsync(callbackLineWriter.AsTextWriter(encodingWebName));

      CloudotLogging.WriteLine("Starting processing request #" + myId);

      return textWriter;
    }

    #endregion

    #region Internal implementation of the service

    private string ComputeSemanticIndexLocation(string name)
    {
      var prefix = Path.Combine(SemanticIndexDir, name);
      var now = String.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

      return Path.Combine(prefix, now);
    }

    private int CallClousotViaProcess(string name, string[] args, TextWriter textwriter)
    {
      Contract.Requires(name != null);
      Contract.Requires(args != null);
      Contract.Requires(textwriter != null);

      try
      {
        string outputFileName;
        using (var outputFileOnServer = CloudotLogging.GetAFreshOutputFileName(name, out outputFileName))
        {
          CloudotLogging.WriteLine("Saving the output at {0}", outputFileName);
          // Start the process with the info we specified
          using (var exe = workersPool.GetAProcessFromTheQueue())
          {
            SendWorkToProcess(exe, args);

            // Reads the standard error and pushes it to the 
            Task.Factory.StartNew(
                          () =>
                          {
                            while (!exe.HasExited)
                            {
                              var lineErr = exe.StandardError.ReadLine();
                              textwriter.WriteLine(lineErr);
                              outputFileOnServer.WriteLine(lineErr);
                            }

//                            var lineOut = exe.StandardOutput.ReadToEnd();
                            var lineOut = exe.StandardError.ReadToEnd();
                            textwriter.Write(lineOut);
                            outputFileOnServer.Write(lineOut);
                          });

            // Reads the standard output
            string line;
            while (!exe.HasExited)
            {
              line = exe.StandardOutput.ReadLine();
              textwriter.WriteLine(line);
              outputFileOnServer.WriteLine(line);
            }

            line = exe.StandardOutput.ReadToEnd();
            textwriter.WriteLine(line);
            outputFileOnServer.WriteLine(line);

            //            line = exe.StandardError.ReadToEnd();
            //           textwriter.Write(line);
            //        outputFileOnServer.WriteLineAsync(line);

            // Before returning, let's flush everything
            textwriter.Flush();
            outputFileOnServer.Flush();

            //exe.WaitForExit(); // Why we need it?

            return exe.ExitCode;
          }
        }
      }
      catch (Exception e)
      {
        CloudotLogging.WriteLine("Some exception (of type {0}) happened during the analysis {1}", e.GetType(), this.myId);
        return -1;
      }
      finally
      {
        // do nothing
      }
    }  

    private void SendWorkToProcess(Process exeProcess, string[] args)
    {
      Contract.Requires(exeProcess != null);
      Contract.Requires(args != null);

      CloudotLogging.WriteLine("Dispatching the analysis to worker process {0}", exeProcess.Id);
      
      var pipeName = CommonNames.GetPipeNameForCloudotWorker(exeProcess.Id);

      var namedPipeClient = new NamedPipeClientStream(pipeName);
      try
      {
        // Try to connect to the process via the pipe
        namedPipeClient.Connect(200);

        // Send the arguments -- our protocol is that once the clousot process received the arguments, it will proceed with the analysis, as usual
        new BinaryFormatter().Serialize(namedPipeClient, args);
      }
      catch (Exception e)
      {
        CloudotLogging.WriteLine("Something went wrong while sending the arguments to process {0}{1}{2}", exeProcess.Id, Environment.NewLine, e.Message);
      }
    }

    private static int CallClousotInternally(string[] args, TextWriter textWriter)
    {
      var cciilProvider = CciILCodeProvider.CreateCodeProvider();
      var metadataDecoder = cciilProvider.MetaDataDecoder;
      var contractDecoder = cciilProvider.ContractDecoder;
      var assemblyCache = new System.Collections.Hashtable();

      var outputFactory = new FullTextWriterOutputFactory<MethodReferenceAdaptor, IAssemblyReference>(textWriter, "ServiceOutput");

      try
      {
        // We now call Clousot
        int returnCode = Clousot.ClousotMain(args,
          metadataDecoder,
          contractDecoder,
          assemblyCache,
          outputFactory: outputFactory,
          cacheAccessorFactories: null
        );
        return returnCode;
      }
      finally
      {
        cciilProvider.Dispose(); // make sure all open files are closed
      }
    }
    #endregion
  }


}
