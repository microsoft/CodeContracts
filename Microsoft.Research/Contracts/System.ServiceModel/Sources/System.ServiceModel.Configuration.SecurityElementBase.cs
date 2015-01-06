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

// File System.ServiceModel.Configuration.SecurityElementBase.cs
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


namespace System.ServiceModel.Configuration
{
  public partial class SecurityElementBase : BindingElementExtensionElement
  {
    #region Methods and constructors
    protected void AddBindingTemplate(Dictionary<AuthenticationMode, System.ServiceModel.Channels.SecurityBindingElement> bindingTemplates, AuthenticationMode mode)
    {
      Contract.Requires(bindingTemplates != null);
    }

    protected virtual new void AddBindingTemplates(Dictionary<AuthenticationMode, System.ServiceModel.Channels.SecurityBindingElement> bindingTemplates)
    {
      Contract.Requires(bindingTemplates != null);
    }

    public override void ApplyConfiguration(System.ServiceModel.Channels.BindingElement bindingElement)
    {
    }

    public override void CopyFrom(ServiceModelExtensionElement from)
    {
    }

    protected internal virtual new System.ServiceModel.Channels.BindingElement CreateBindingElement(bool createTemplateOnly)
    {
      return default(System.ServiceModel.Channels.BindingElement);
    }

    protected internal override System.ServiceModel.Channels.BindingElement CreateBindingElement()
    {
      return default(System.ServiceModel.Channels.BindingElement);
    }

    protected internal override void InitializeFrom(System.ServiceModel.Channels.BindingElement bindingElement)
    {
    }

    protected virtual new void InitializeNestedTokenParameterSettings(System.ServiceModel.Security.Tokens.SecurityTokenParameters sp, bool initializeNestedBindings)
    {
    }

    internal SecurityElementBase()
    {
    }

    protected override bool SerializeToXmlElement(System.Xml.XmlWriter writer, string elementName)
    {
      return default(bool);
    }

    protected override void Unmerge(System.Configuration.ConfigurationElement sourceElement, System.Configuration.ConfigurationElement parentElement, System.Configuration.ConfigurationSaveMode saveMode)
    {
    }
    #endregion

    #region Properties and indexers
    public bool AllowInsecureTransport
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

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

    public AuthenticationMode AuthenticationMode
    {
      get
      {
        return default(AuthenticationMode);
      }
      set
      {
      }
    }

    public override Type BindingElementType
    {
      get
      {
        return default(Type);
      }
    }

    public bool CanRenewSecurityContextToken
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.ServiceModel.Security.SecurityAlgorithmSuite DefaultAlgorithmSuite
    {
      get
      {
        return default(System.ServiceModel.Security.SecurityAlgorithmSuite);
      }
      set
      {
      }
    }

    public bool EnableUnsecuredResponse
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IncludeTimestamp
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public IssuedTokenParametersElement IssuedTokenParameters
    {
      get
      {
        return default(IssuedTokenParametersElement);
      }
    }

    public System.ServiceModel.Security.SecurityKeyEntropyMode KeyEntropyMode
    {
      get
      {
        return default(System.ServiceModel.Security.SecurityKeyEntropyMode);
      }
      set
      {
      }
    }

    public LocalClientSecuritySettingsElement LocalClientSettings
    {
      get
      {
        return default(LocalClientSecuritySettingsElement);
      }
    }

    public LocalServiceSecuritySettingsElement LocalServiceSettings
    {
      get
      {
        return default(LocalServiceSecuritySettingsElement);
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

    public System.ServiceModel.MessageSecurityVersion MessageSecurityVersion
    {
      get
      {
        return default(System.ServiceModel.MessageSecurityVersion);
      }
      set
      {
      }
    }

    protected override System.Configuration.ConfigurationPropertyCollection Properties
    {
      get
      {
        return default(System.Configuration.ConfigurationPropertyCollection);
      }
    }

    public bool RequireDerivedKeys
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool RequireSecurityContextCancellation
    {
      get
      {
        return default(bool);
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

    public System.ServiceModel.Channels.SecurityHeaderLayout SecurityHeaderLayout
    {
      get
      {
        return default(System.ServiceModel.Channels.SecurityHeaderLayout);
      }
      set
      {
      }
    }
    #endregion
  }
}
