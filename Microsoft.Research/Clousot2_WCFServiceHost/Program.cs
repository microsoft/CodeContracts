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
using System.ServiceModel;
using Microsoft.Research.DataStructures;
using Microsoft.Research.Cloudot.Common;

namespace Microsoft.Research.Cloudot
{
  // You can use this project to easily debug the WCF service
  // This console application host the WCF service instead of the Windows Service, which requires installation
  internal class ConsoleHost
  {
    // This Clousot as Service host started from the command line
    // Usage: run it, and wait for readline to kill everything
    // We need a service per box
    public static void Main()
    {
      CloudotLogging.WriteLine("Starting Cloudot service...");
      CloudotLogging.WriteLine();

      try
      {
        using (var host = new ClousotServiceHost())
        {
          host.AddLogger(Console.Out.AsLineWriter()); // TODO: Replace the Console.Out with the equivalent in CloudotLogging

          host.Open();

          CloudotLogging.WriteLine(string.Format("The service is ready and listening at {0}", String.Join(", ", host.BaseAddresses)));
          CloudotLogging.WriteLine("Press <ENTER> to terminate service.");
          Console.ReadLine();

          host.Close();
        }
      }
      catch (TimeoutException timeProblem)
      {
        CloudotLogging.WriteLine(timeProblem.Message);
        if (timeProblem.InnerException != null)
        {
          CloudotLogging.WriteLine(timeProblem.InnerException.Message);
        }
        Console.ReadLine();
      }
      catch (CommunicationException commProblem)
      {
        CloudotLogging.WriteLine(commProblem.Message);
        if (commProblem.InnerException != null)
        {
          CloudotLogging.WriteLine(commProblem.InnerException.Message);
        }
        Console.ReadLine();
      }
    }
  }
}
