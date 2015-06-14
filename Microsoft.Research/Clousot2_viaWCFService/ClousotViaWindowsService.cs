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
using System.ServiceProcess;
using Microsoft.Research.Cloudot.Common;

namespace Microsoft.Research.CodeAnalysis
{
  public static class ClousotViaWindowsService
  {
    private static readonly TimeSpan maximumTimeToStartWindowsService = new TimeSpan(0, 0, 20); // 20 seconds

    public static bool EnsureWindowsService()
    {
      try
      {
        using (var serviceController = new ServiceController(ClousotWindowsServiceCommon.ServiceName))
        {
          if (serviceController.Status != ServiceControllerStatus.Running) // can throw a Win32Exception or an InvalidOperationException if service not installed
          {
            serviceController.Start(); // can throw a Win32Exception (or an InvalidOperationException if service not installed)
            serviceController.WaitForStatus(ServiceControllerStatus.Running, maximumTimeToStartWindowsService); // can throw a TimeoutException
          }
        }
      }
      catch
      {
        return false;
      }

      return true;
    }

    public static int Main(string[] args)
    {
      // Make sure the Windows Service is running
      EnsureWindowsService();

      // Call the WCF Service hosted by the Windows Service, assuming it is now running
      return ClousotViaService.Main(args);
    }
  }
}
