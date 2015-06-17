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

//#define EMBEDDED_CONFIGURATION // this is the case when we want the app.config in the resources
#define HARDCODED_CONFIGURATION  // configuration done programmatically
// none (exe app.config)         // configuration from the app.config of the project

// We do not use Visual Studio Service Reference because it is not able to share service interfaces

using System;
using System.IO;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Research.Cloudot.Common;
using Clousot2_ServiceClient;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Threading;
using System.Diagnostics.CodeAnalysis;
#if EMBEDDED_CONFIGURATION
using System.Configuration;
using System.Reflection;
#endif

namespace Microsoft.Research.CodeAnalysis
{
  public static class ClousotViaService
  {
    private static readonly RetryOptions defaultRetryOptions = new RetryOptions
    {
      FirstMinTime = TimeSpan.FromSeconds(1),
      FirstMaxTime = TimeSpan.FromSeconds(2),
      MaxTime = TimeSpan.FromSeconds(20),
      MultiplyingFactor = 1.5,
      NumberOfRetries = 2
    };

#if EMBEDDED_CONFIGURATION
    private static readonly Configuration configuration;

    private static ClousotViaService()
    {
      var configFileName = "Microsoft.Research.CodeAnalysis.app.config";

      // It is not possible to load a resource as a configuration file
      // it has to be a real file on disk, that's why we use a temporary file
      var assembly = Assembly.GetExecutingAssembly();
      string contents;
      using (var configFileReader = new StreamReader(assembly.GetManifestResourceStream(configFileName)))
        contents = configFileReader.ReadToEnd();
      var tempFileName = Path.GetTempFileName();
      using (var tempFileWriter = new StreamWriter(tempFileName))
        tempFileWriter.Write(contents);

      var configFileMap = new ConfigurationFileMap(tempFileName);
      configuration = ConfigurationManager.OpenMappedMachineConfiguration(configFileMap);
    }
#endif

    // We have a dictionary because we can have different concrete types as ILineWriters
    private static readonly Dictionary<Type, DuplexChannelFactory<IClousotService>> channelFactoryForType = new Dictionary<Type, DuplexChannelFactory<IClousotService>>();
    private static readonly object LockChannelFactoryForType = new object();
    private static readonly TimeSpan TimeToWaitBeforeClosing = new TimeSpan(0, 0, 6); // 6 seconds

    // We need only one channel, but the WCF protocol requires us to provide a factory...
    private static DuplexChannelFactory<IClousotService> GetChannelFactoryForType(Type type)
    {
      lock (LockChannelFactoryForType)
      {
        DuplexChannelFactory<IClousotService> channelFactory;
        if (channelFactoryForType.TryGetValue(type, out channelFactory))
          return channelFactory;

#if EMBEDDED_CONFIGURATION
        channelFactory = new CustomDuplexChannelFactory<IClousotService>(configuration, type);
#elif HARDCODED_CONFIGURATION
        var binding = ClousotWCFServiceCommon.NewBinding(ClousotWCFServiceCommon.SecurityMode);
        binding.ReceiveTimeout = TimeSpan.FromDays(2);
        binding.SendTimeout = TimeSpan.FromDays(2);

        var endpointAddress = new EndpointAddress(ClousotWCFServiceCommon.BaseUri, ClousotWCFServiceCommon.Identity);

        channelFactory = new DuplexChannelFactory<IClousotService>(type, binding, endpointAddress);
#else
        channelFactory = new DuplexChannelFactory<IClousotService>(type);
#endif

        channelFactoryForType.Add(type, channelFactory);

        return channelFactory;
      }
    }

    static public string GetTmpDirectory
    {
      get
      {
        return @"C:\tmp\CloudotTest"; // TODO: use .net GetTmpDirectory
      }
    }

    public static int Main(string[] args, string forceServiceAddress = null)
    {
      string[] dummy;
      return Main2(args, Console.Out, out dummy, forceServiceAddress);
    }

    public static int Main(string[] args, out string[] clousotArgs, string forceServiceAddress = null)
    {
      return Main2(args, Console.Out, out clousotArgs, forceServiceAddress);
    }

    public static int Main2(string[] args, TextWriter textWriter, out string[] clousotArgs, string forceServiceAddress = null)
    {
      using(var tw = textWriter.AsLineWriter())
      {
        return Main2(args, tw, out clousotArgs, forceServiceAddress);
      }

    }

    /// <param name="forceServiceAddress">If != null, overwrites the service address, and do not not create the analysis package</param>
    public static int Main2(string[] args, IVerySimpleLineWriterWithEncoding lineWriter, out string[] clousotArgs, string forceServiceAddress = null)
    {
      Contract.Requires(lineWriter != null);

      int result = -1;
      var options = new ClousotServiceOptions(args, forceServiceAddress);

      clousotArgs = options.remainingArgs.ToArray();

      if (options.windowsService)
      {
        ClousotViaWindowsService.EnsureWindowsService();
      }

      var retryOptions = defaultRetryOptions.Clone();
      retryOptions.NumberOfRetries = options.serviceRetries;

      // Here we create the callback object, starting from the lineWriter provided by the caller
      var channelFactory = GetChannelFactoryForType(lineWriter.GetType());
      var channelAddress = String.IsNullOrWhiteSpace(options.serviceAddress)
        ? channelFactory.Endpoint.Address
        : new EndpointAddress(new Uri(options.serviceAddress), channelFactory.Endpoint.Address.Identity); // WCF wants us to provide the address for the factory but also for the instance...

      // We create the callback object
      var callbackInstanceContext = new InstanceContext(lineWriter);

      Func<IClousotService> GetAndOpenClousotService = () =>
        {
          // Let's create a channel with my callback and the address computed above
          var channel = channelFactory.CreateChannel(callbackInstanceContext, channelAddress);

          lineWriter.WriteLine(("Connecting to Cloudot at address " + channelAddress).PrefixWithCurrentTime());

          channel.Open(); // can throw an exception

          // Add an event that will be fired when the connection will enter a fault state.
          // Useful for debugging
          var channelAsDuplex = channel as IDuplexContextChannel;
          if (channelAsDuplex != null)
          {
            channelAsDuplex.Faulted +=
              (object sender, EventArgs e) =>
              {
                // This message is useful for debugging, so that we can see when the error happened
                Console.WriteLine("The connection to Cloudot entered a fault state!".PrefixWithCurrentTime());
              };
          }
          else
          {
            Contract.Assume(false, "We expect the service to be an instance of IDuplexContextChannel");
          }

          return channel;
        };

      IClousotService clousotService = null;
      try
      {
        // Step 0: We connect to the server 
        if (!Retry<ServerTooBusyException>.RetryOnException(GetAndOpenClousotService, retryOptions, out clousotService))
        {
          return -6666; // server too busy, could not connect
        }

        lineWriter.WriteLine("Connection with Cloudot established".PrefixWithCurrentTime());

        var clousotSpecificOptions = options.remainingArgs.ToArray();

        // Step 0.5: Are we simply requested to re-run an existing analysis? If this is the case just invoke cloudot
        if(forceServiceAddress != null)
        {
          result = clousotService.Main(args, lineWriter.Encoding.WebName);
          goto ok;
        }

        // Step 1: We pack all the files on the local machine and send them to the server
        FileTransfer.AnalysisPackageInfo packageInfo;
        var createThePackage = !options.useSharedDirs;
        if (FileTransfer.TryProcessPathsAndCreateAnalysisPackage(GetTmpDirectory, clousotSpecificOptions, createThePackage, out packageInfo))
        {
          clousotArgs = packageInfo.ExpandedClousotOptionsNormalized;

          // Step 2 -- First option: use shared dirs. We do not really want to use it, but it is interesting for debugging
          // We have all the command line options normalized to UNCs, and use them for the server
          if (!createThePackage)
          {
            if (packageInfo.ExpandedClousotOptionsNormalized != null)
            {

              // Step 2: We invoke the analysis on the server
              lineWriter.WriteLine("Invoking the analysis on cloudot using shared dirs".PrefixWithCurrentTime());
              lineWriter.WriteLine("Parameters: {0}".PrefixWithCurrentTime(), String.Join(" ", clousotArgs));

              result = clousotService.Main(clousotArgs, lineWriter.Encoding.WebName); // can throw an exception 

              goto ok;
            }
          }
          // Step 2 -- Second option: use a package
          // We have create a zip file with all the references. We copy it where the server tells us to.
          // Then, we invoke cloudot to perform the analysis
          else
          {
            var serverOut = clousotService.GetOutputDirectory();
            string fileOnTheServer;
            if (serverOut != null && FileTransfer.TrySendTheFile(packageInfo.PackageFileNameLocal, serverOut, out fileOnTheServer))
            {
              if (packageInfo.AssembliesToAnalyze != null && packageInfo.AssembliesToAnalyze.Length > 0)
              {
                packageInfo.PackageFileNameServer = fileOnTheServer;
                var name = Path.GetFileName(packageInfo.AssembliesToAnalyze[0]);
#if false
              // Just for debug
              string[] p;
              FileTransfer.TryRestoreAnalysisPackage(fileOnTheServer, Path.Combine(serverOut, "tmp"), out p);
#endif
                // Step 2: We invoke the analysis on the server
                lineWriter.WriteLine("Invoking the analysis on cloudot using analysis package (on server at {0})".PrefixWithCurrentTime(), fileOnTheServer);
                result = clousotService.AnalyzeUsingAnalysisPackage(name, fileOnTheServer, lineWriter.Encoding.WebName);

                goto ok;
              }
              else
              {
                lineWriter.WriteLine("Cannot figure out the assembly to analyze. We abort.");
              }
            }
          }
        }
        return -1;

      ok:
        // Step * : Close the service
        // We moved the Close action here, as it may be the case that the background thread is still writing something on the callback, while we close it
        // I do not really know how to see if this is the case, but it appears when we output a lot of information via a remote server
        // From what I saw on the internet, for similar cases, it seems the best solution is to use the Try-Catch-Abort pattern
        // Waiting also helps, but how much is a problem.
        clousotService.Close(TimeToWaitBeforeClosing);

        return result;
      }
      // We use the Try-Catch-Abort pattern for WCF
      catch (ActionNotSupportedException actionNotSupported)
      {
        Console.WriteLine("Something is wrong with the client/server contract");
        Console.WriteLine("This is the exception {0}", actionNotSupported);
        return -1;
      }
      catch (CommunicationException)
      {
        if (clousotService != null)
        {
          var channelAsDuplex = clousotService as IDuplexContextChannel;
          if (channelAsDuplex != null)
          {
            if (channelAsDuplex.State == CommunicationState.Faulted)
            {
              Console.WriteLine("The communication is a faulted state. Is Cloudot down?");
            }
          }
          (clousotService as IDuplexContextChannel).Abort();
        }
        else
        {
          Console.WriteLine("Something has gone very wrong. I did not expected clousotService to be null here");
        }
        return -1;

      }
      catch (TimeoutException)
      {
        Console.WriteLine("Hit a timeout in the communication with Cloudot");
        (clousotService as IDuplexContextChannel).Abort();

        return -1;
      }
      catch (Exception e)
      {
        var errMsg = e.Message + (e.InnerException != null ? string.Format("{0} -- {1}", Environment.NewLine, e.InnerException.Message) : "");
        Console.WriteLine(("Analysis terminated because of an error: " + errMsg).PrefixWithCurrentTime());

        return -1;
      }
      finally
      {
        if (clousotService != null)
        {
          clousotService.TryDispose();
        }
      }
    }



    /// <summary>
    /// Just an hack. We should have a better story for sending files to the server
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    static private string[] AdjustPaths(string[] args)
    {
      Contract.Requires(args != null);
      Contract.Requires(Contract.ForAll(args, a => a != null));

      var result = new string[args.Length];

      for (var i = 0; i < args.Length; i++)
      {
        var arg = args[i];
        if (arg.Length > 0)
        {
          if (arg[0] == '@')
          {
            var path = arg.Substring(1, arg.Length - 1);

            while (i < args.Length && !File.Exists(path))
            {
              i++;
              if (i < args.Length)
              {
                path += " " + args[i];
              }
            }
            if (i == args.Length)
            {
              return args;
            }

            var fullPath = Path.GetFullPath(path);
            string unc;
            if (FileSystemAbstractions.TryGetUniversalName(fullPath, out unc))
            {
              result[i] = "@" + unc;
              continue;
            }
          }
          else if (arg[0] != '-' && arg[0] != '/')
          {
            string unc;
            if (FileSystemAbstractions.TryGetUniversalName(arg, out unc))
            {
              result[i] = unc;
              continue;
            }
          }
        }

        result[i] = args[i];
      }

      return result;
    }
  }

  static class ClousotServiceExtensions
  {
    // ChannelFactory.CreateChannel returns a proxy that is at the same time a IClousotService, a IDuplexContextChannel, a IClientChannel, and maybe other things too

    public static void Open(this IClousotService channel)
    {
      Contract.Requires(channel != null);

      ((IDuplexContextChannel)channel).Open();
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant", Justification = "Thread sleeping")]
    public static bool Close(this IClousotService channel, TimeSpan wait)
    {
      Contract.Requires(channel != null);

      var channelAsDuplex = channel as IDuplexContextChannel;
      if (channelAsDuplex != null)
      {
        if (channelAsDuplex.State != CommunicationState.Faulted)
        {
          Console.WriteLine("Waiting {0} before closing the channel (in a state {1})", wait, channelAsDuplex.State);

          Thread.Sleep(wait);

          // It may be the case that while sleeping, the channel was closed
          if (channelAsDuplex.State != CommunicationState.Faulted)
          {
            Console.WriteLine("Now we close the channel");

            channelAsDuplex.Close(wait);

            return true;
          }
        }
      }
      return false;
    }
  }

  static class ObjectExtensions
  {
    public static bool TryDispose(this Object obj)
    {
      var disposable = obj as IDisposable;
      if (disposable == null)
        return false;
      disposable.Dispose();
      return true;
    }
  }
}
