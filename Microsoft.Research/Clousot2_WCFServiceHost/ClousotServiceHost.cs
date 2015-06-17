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
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Research.Cloudot.Common;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.Cloudot
{
  public class ClousotServiceHost : ServiceHost
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.ServiceInstance != null);
    }

    public readonly ClousotService ServiceInstance;

    public ClousotServiceHost() : this(new ClousotService(), ClousotWCFServiceCommon.BaseUri) { }
      
    private ClousotServiceHost(ClousotService serviceInstance, Uri baseUri) : base(serviceInstance, baseUri)
    {
      Contract.Requires(serviceInstance != null);
      Contract.Requires(baseUri != null);
      Contract.Requires(baseUri.IsAbsoluteUri);

      this.ServiceInstance = serviceInstance;

      this.AddMainEndpoint(baseUri);

#if DEBUG
      // When we get an exception, we want to have the detail
      // However, the exceptions are turned into faults, and they break the service
      // TODO: handle Faults -- this may be possible somehow
      this.AddServiceDebugBehavior(); 
#endif
#if DEBUG && WS_HTTP_BINDING
      this.AddServiceMetadataBehavior();
#endif
    }

    // This is the equivalent of App.Config. 
    // It is best to do it programmatically, to avoid having an App.Config for each project
    private void AddMainEndpoint(Uri baseUri)
    {
      Contract.Requires(baseUri != null);
      Contract.Requires(baseUri.IsAbsoluteUri);

      var contract = ContractDescription.GetContract(typeof(IClousotService), typeof(ClousotService));
      var binding = ClousotWCFServiceCommon.NewBinding(ClousotWCFServiceCommon.SecurityMode);
      var identity = ClousotWCFServiceCommon.Identity;
      var address = new EndpointAddress(baseUri, identity);
      var endpoint = new ServiceEndpoint(contract, binding, address); // This is the point where we listen at
      this.AddServiceEndpoint(endpoint);
    }

    private void AddServiceDebugBehavior()
    {
      this.AddBehavior<ServiceDebugBehavior>(sdb => { sdb.IncludeExceptionDetailInFaults = true; });
    }

    private void AddServiceMetadataBehavior()
    {
      this.AddBehavior<ServiceMetadataBehavior>(smb => { smb.HttpGetEnabled = true; });
    }

    private void AddBehavior<B>(Action<B> f) where B : class, IServiceBehavior, new()
    {
      Contract.Requires(f != null);

      // We cannot have twice the same behavior, so before adding it we should check if it already in there
      var sb = this.Description.Behaviors.FirstOrDefault(b => b is B) as B;
      if (sb != null)
        f(sb);
      else
      {
        sb = new B();
        f(sb);
        this.Description.Behaviors.Add(sb);
      }
    }

    public void AddLogger(IVerySimpleLineWriter lineWriter)
    {
      
      // OnMain is an custom event that we added
      // The intended behavior is to Log all the times the service is invoked
      this.ServiceInstance.OnMain += args => WriteLogLine(lineWriter, "Main", String.Join(" ", args));
    }

    private static void WriteLogLine(IVerySimpleLineWriter lineWriter, string operationName, string arguments)
    {
      Contract.Requires(lineWriter != null);

      var str = string.Format("Request #{3} received from user '{0}' [{1}]: {2}",
        System.Threading.Thread.CurrentPrincipal.Identity.Name,
        operationName,
        arguments,
        ClousotService.RequestId-1); // -1 because by the time we are called we the instance is already created
      lineWriter.WriteLine(str.PrefixWithCurrentTime());
      lineWriter.WriteLine();
    }

    #region Customizations
    protected override void OnClosed()
    {
      CloudotLogging.WriteLine("The service is now closed");

      base.OnClosed();
    }

    protected override void OnAbort()
    {
      CloudotLogging.WriteLine("Aborting the service");

      base.OnAbort();
    }

    protected override void OnClosing()
    {
      CloudotLogging.WriteLine("Closing the service");

      base.OnClosing();
    }

    #endregion
  }
}
