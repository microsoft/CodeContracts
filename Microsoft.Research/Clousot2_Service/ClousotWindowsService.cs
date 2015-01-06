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
using Microsoft.Research.DataStructures;
using System.Diagnostics;

namespace Microsoft.Research.Cloudot
{
  public partial class ClousotWindowsService : ServiceBase
  {
    private ClousotServiceHost serviceHost;

    public ClousotWindowsService()
    {
      this.InitializeComponent();

      this.ServiceName = ClousotWindowsServiceCommon.ServiceName;
    }

    protected override void OnStart(string[] args)
    {
      // Sanity check: if we do OnStart -> OnStop -> OnStart, and we do not do set serviceHost to null in OnStop
      if (this.serviceHost != null)
        this.serviceHost.Close();

      this.serviceHost = new ClousotServiceHost();

      this.EventLog.WriteEntry(String.Format("Starting service at {0}", String.Join(", ", this.serviceHost.BaseAddresses)));

      this.serviceHost.AddLogger(new EventLogLineWriter(this.EventLog));

      // We open the WCF service
      this.serviceHost.Open(); 

      this.EventLog.WriteEntry("Service started");

      // As it is a windows service, it remains alive
    }

    protected override void OnStop()
    {
      if (this.serviceHost != null)
      {
        this.EventLog.WriteEntry("Closing service");

        this.serviceHost.Close();
        this.serviceHost = null;

        this.EventLog.WriteEntry("Service closed");
      }
    }
  }

  class EventLogLineWriter : IVerySimpleLineWriter
  {
    private readonly EventLog eventLog;

    public EventLogLineWriter(EventLog eventLog)
    {
      this.eventLog = eventLog;
    }

    public void WriteLine(string value)
    {
      this.eventLog.WriteEntry(value);
    }
  }
}
