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

// File System.ServiceModel.Channels.AsymmetricSecurityBindingElement.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.ServiceModel.Channels
{
  sealed public partial class AsymmetricSecurityBindingElement : SecurityBindingElement, System.ServiceModel.Description.IPolicyExportExtension
  {
    #region Methods and constructors
    public AsymmetricSecurityBindingElement()
    {
    }

    public AsymmetricSecurityBindingElement(System.ServiceModel.Security.Tokens.SecurityTokenParameters recipientTokenParameters, System.ServiceModel.Security.Tokens.SecurityTokenParameters initiatorTokenParameters)
    {
    }

    public AsymmetricSecurityBindingElement(System.ServiceModel.Security.Tokens.SecurityTokenParameters recipientTokenParameters)
    {
    }

    protected override IChannelFactory<TChannel> BuildChannelFactoryCore<TChannel>(BindingContext context)
    {
      return default(IChannelFactory<TChannel>);
    }

    protected override IChannelListener<TChannel> BuildChannelListenerCore<TChannel>(BindingContext context)
    {
      return default(IChannelListener<TChannel>);
    }

    public override BindingElement Clone()
    {
      return default(BindingElement);
    }

    public override T GetProperty<T>(BindingContext context)
    {
      return default(T);
    }

    public override void SetKeyDerivation(bool requireDerivedKeys)
    {
    }

    void System.ServiceModel.Description.IPolicyExportExtension.ExportPolicy(System.ServiceModel.Description.MetadataExporter exporter, System.ServiceModel.Description.PolicyConversionContext context)
    {
    }

    #endregion

    #region Properties and indexers
    public bool AllowSerializedSigningTokenOnReply
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.ServiceModel.Security.Tokens.SecurityTokenParameters InitiatorTokenParameters
    {
      get
      {
        return default(System.ServiceModel.Security.Tokens.SecurityTokenParameters);
      }
      set
      {
      }
    }

    public System.ServiceModel.Security.MessageProtectionOrder MessageProtectionOrder
    {
      get
      {
        return default(System.ServiceModel.Security.MessageProtectionOrder);
      }
      set
      {
      }
    }

    public System.ServiceModel.Security.Tokens.SecurityTokenParameters RecipientTokenParameters
    {
      get
      {
        return default(System.ServiceModel.Security.Tokens.SecurityTokenParameters);
      }
      set
      {
      }
    }

    public bool RequireSignatureConfirmation
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}
