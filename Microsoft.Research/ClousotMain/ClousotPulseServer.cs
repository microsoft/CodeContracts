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

#undef  DEBUG_PRINT

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Research.DataStructures;
using Microsoft.Research.ClousotPulse.Messages;

namespace Microsoft.Research.CodeAnalysis
{
  public class ClousotPulseServer
  {
    #region Object invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.GetClousotCurrentAnalysisState != null);
      Contract.Invariant(this.callbackNames != null);
      Contract.Invariant(this.namedPipeServer != null);
    }
    
    #endregion

    private const int TIMEOUTFORCONNECTION = 500;
    private readonly Func<ClousotAnalysisState> GetClousotCurrentAnalysisState;
    private readonly Set<string> callbackNames;
    private readonly Thread server;
    private readonly NamedPipeServerStream namedPipeServer;

    /// <summary>
    /// Creates and starts the ClousotPulse server
    /// </summary>
    public ClousotPulseServer(Func<ClousotAnalysisState> GetClousotCurrentAnalysisState)
    {
      Contract.Requires(GetClousotCurrentAnalysisState != null);

      this.GetClousotCurrentAnalysisState = GetClousotCurrentAnalysisState;
      this.callbackNames = new Set<string>();
      this.namedPipeServer = new NamedPipeServerStream(CommonNames.GetPipeNameForCCCheckPulse(), PipeDirection.InOut, 254);
   
      // Start the server
      this.server = new Thread(MainProcess);
      //this.server.IsBackground = true; // If we set it to true, then the server will die when the main one will. However, this is bad as it may cause the server to be killed while it is still sending the info back to the clients
      this.server.Start();
    }

    private void MainProcess(object data)
    {
#if DEBUG && DEBUG_PRINT
      Console.WriteLine("[DEBUG] Pulse Clousot at named channel {0}", CommonNames.GetPipeNameForCCCheckPulse());
#endif
      
      while (true)
      {
        try
        {
          this.namedPipeServer.WaitForConnection(); // wait for connection

          var stream = new PipeStreamSimple(namedPipeServer);
          
          string obj;

          if (stream.TryRead(out obj))
          {

            if (obj == "shutdown")
            {
              goto done;
            }

            this.callbackNames.AddIfNotNull(obj);
          }
          // Write what Clousot is doing now 
          stream.WriteIfConnected(GetClousotCurrentAnalysisState().ToList());

          this.namedPipeServer.Disconnect();
        }
        catch
        {
          // just swallow the error
        }
      }

    done:
      ;
    }

    public void NotifyListenersWeAreDone(ClousotAnalysisResults results)
    {
      Contract.Requires(results != null);

      results.NotifyDone();
      var namedPipeClient = new NamedPipeClientStream(".", CommonNames.GetPipeNameForCCCheckPulseCallBack(0), PipeDirection.InOut);

      try
      {
#if DEBUG && DEBUG_PRINT
        Console.WriteLine("[Debug] Trying to connect to the server to write back (we also wait {0})", TIMEOUTFORCONNECTION);
#endif
        namedPipeClient.Connect(TIMEOUTFORCONNECTION);
        new PipeStreamSimple(namedPipeClient).WriteIfConnected(results.ToList());

#if DEBUG && DEBUG_PRINT
        Console.WriteLine("[Debug] We are done writing back");
#endif
      }
      catch (Exception
#if DEBUG && DEBUG_PRINT
 e
#endif
        )
      {
#if DEBUG && DEBUG_PRINT
        Console.WriteLine("[Debug] We failed in writing back: {0}", e.Message);
#endif
        // do nothing
      }
      finally
      {
        namedPipeClient.Close();
      }
    }

    public void Kill()
    {      
      // We send ourself a shutdown message, to deblock the wait above
      // Should interrupt the WaitForConnection in a better way?
      // see: http://stackoverflow.com/questions/607872/what-is-a-good-way-to-shutdown-threads-blocked-on-namedpipeserverwaitforconnect
      try
      {
        var client = new NamedPipeClientStream(".", CommonNames.GetPipeNameForCCCheckPulse(), PipeDirection.InOut);
        client.Connect(); // we know we succed?
        new PipeStreamSimple(client).WriteIfConnected("shutdown");
      }
      catch
      {
      }
    }
  }
}
