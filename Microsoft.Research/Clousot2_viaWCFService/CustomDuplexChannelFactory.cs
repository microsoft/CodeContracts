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

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Research.Cloudot.Common;
using System.Diagnostics.Contracts;

namespace Clousot2_ServiceClient
{
  internal class CustomDuplexChannelFactory<TChannel> : DuplexChannelFactory<TChannel> where TChannel : class
  {
    private readonly ServiceModelSectionGroup group;

    public CustomDuplexChannelFactory(object callbackObject, Configuration configuration)
      : base(callbackObject)
    {
      if (configuration != null)
        this.group = ServiceModelSectionGroup.GetSectionGroup(configuration);
    }

    public CustomDuplexChannelFactory(InstanceContext callbackInstance, Configuration configuration)
      : this((object)callbackInstance, configuration)
    { }

    public CustomDuplexChannelFactory(System.Type callbackInstanceType, Configuration configuration)
      : this((object)callbackInstanceType, configuration)
    { }

    protected override ServiceEndpoint CreateDescription()
    {
      var serviceEndPoint = base.CreateDescription();

      if (this.group == null)
        return serviceEndPoint;

      var selectedEndpoint = this.group.Client.Endpoints.Cast<ChannelEndpointElement>().FirstOrDefault(ep => ep.Contract == serviceEndPoint.Contract.ConfigurationName);

      if (selectedEndpoint == null)
        return serviceEndPoint;

      if (serviceEndPoint.Binding == null)
        serviceEndPoint.Binding = this.CreateBinding(selectedEndpoint.Binding);
      if (serviceEndPoint.Address == null)
      {
        Contract.Assume(selectedEndpoint.Address != null);
        serviceEndPoint.Address = new EndpointAddress(selectedEndpoint.Address, this.GetIdentity(selectedEndpoint.Identity), selectedEndpoint.Headers.Headers);
      }
      if (!serviceEndPoint.Behaviors.Any() && !String.IsNullOrEmpty(selectedEndpoint.BehaviorConfiguration))
        this.AddBehaviors(selectedEndpoint.BehaviorConfiguration, serviceEndPoint);

      return serviceEndPoint;
    }

    private void AddBehaviors(string behaviorConfiguration, ServiceEndpoint serviceEndpoint)
    {
      foreach (var behaviorExtension in this.group.Behaviors.EndpointBehaviors[behaviorConfiguration])
      {
        var extension = behaviorExtension.GetType().InvokeMember("CreateBehavior", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, behaviorExtension, null) as IEndpointBehavior;
        if (extension != null)
          serviceEndpoint.Behaviors.Add(extension);
      }
    }

    private EndpointIdentity GetIdentity(IdentityElement element)
    {
      return typeof(EndpointIdentity).InvokeMember("LoadIdentity", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Static, null, null, new Object[] { element }) as EndpointIdentity;
    } 

    private Binding CreateBinding(string name)
    {
      var be = this.group.Bindings[name].ConfiguredBindings.FirstOrDefault();
      if (be == null)
        return null;
      var binding = this.GetBinding(be);
      be.ApplyConfiguration(binding);
      return binding;
    }

    private Binding GetBinding(IBindingConfigurationElement configurationElement)
    {
      if (configurationElement is CustomBindingElement)
        return new CustomBinding();
      if (configurationElement is BasicHttpBindingElement)
        return new BasicHttpBinding();
      if (configurationElement is NetMsmqBindingElement)
        return new NetMsmqBinding();
      if (configurationElement is NetNamedPipeBindingElement)
        return new NetNamedPipeBinding();
#if false
      if (configurationElement is NetPeerTcpBindingElement)
        return new NetPeerTcpBinding();
#endif
      if (configurationElement is NetTcpBindingElement)
        return new NetTcpBinding();
      if (configurationElement is WSDualHttpBindingElement)
        return new WSDualHttpBinding();
      if (configurationElement is WSHttpBindingElement)
        return new WSHttpBinding();
      if (configurationElement is WSFederationHttpBindingElement)
        return new WSFederationHttpBinding();
      return null;
    }
  }

  // Additional constructors allowing custom configuration

#if OLD_CODE
  internal partial class ClousotServiceClient 
  {
    private readonly DuplexChannelFactory<IClousotService> channelFactory;

    public ClousotServiceClient(Configuration configuration, InstanceContext callbackInstance)
      : base(callbackInstance, new WSDualHttpBinding(), new EndpointAddress("http://tempuri.org"))
    {
      if (configuration != null)
        this.channelFactory = new CustomDuplexChannelFactory<IClousotService>(callbackInstance, configuration);
          // new ConfigurationDuplexChannelFactory<IClousotService>(callbackInstance, "*", null, configuration);
    }

    protected override IClousotService CreateChannel()
    {
      if (this.channelFactory == null)
        return base.CreateChannel();

      foreach (var item in this.Endpoint.Behaviors) // should check that only one matches, or not?
        if (!this.channelFactory.Endpoint.Behaviors.Contains(item.GetType()))
          this.channelFactory.Endpoint.Behaviors.Add(item);

      return this.channelFactory.CreateChannel();
    }
  }
#endif
}

