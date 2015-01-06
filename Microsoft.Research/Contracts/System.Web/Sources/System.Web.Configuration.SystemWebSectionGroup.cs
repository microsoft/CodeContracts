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

// File System.Web.Configuration.SystemWebSectionGroup.cs
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


namespace System.Web.Configuration
{
  sealed public partial class SystemWebSectionGroup : System.Configuration.ConfigurationSectionGroup
  {
    #region Methods and constructors
    public SystemWebSectionGroup()
    {
    }
    #endregion

    #region Properties and indexers
    public AnonymousIdentificationSection AnonymousIdentification
    {
      get
      {
        return default(AnonymousIdentificationSection);
      }
    }

    public AuthenticationSection Authentication
    {
      get
      {
        return default(AuthenticationSection);
      }
    }

    public AuthorizationSection Authorization
    {
      get
      {
        return default(AuthorizationSection);
      }
    }

    public System.Configuration.DefaultSection BrowserCaps
    {
      get
      {
        return default(System.Configuration.DefaultSection);
      }
    }

    public ClientTargetSection ClientTarget
    {
      get
      {
        return default(ClientTargetSection);
      }
    }

    public CompilationSection Compilation
    {
      get
      {
        return default(CompilationSection);
      }
    }

    public CustomErrorsSection CustomErrors
    {
      get
      {
        return default(CustomErrorsSection);
      }
    }

    public DeploymentSection Deployment
    {
      get
      {
        return default(DeploymentSection);
      }
    }

    public System.Configuration.DefaultSection DeviceFilters
    {
      get
      {
        return default(System.Configuration.DefaultSection);
      }
    }

    public FullTrustAssembliesSection FullTrustAssemblies
    {
      get
      {
        return default(FullTrustAssembliesSection);
      }
    }

    public GlobalizationSection Globalization
    {
      get
      {
        return default(GlobalizationSection);
      }
    }

    public HealthMonitoringSection HealthMonitoring
    {
      get
      {
        return default(HealthMonitoringSection);
      }
    }

    public HostingEnvironmentSection HostingEnvironment
    {
      get
      {
        return default(HostingEnvironmentSection);
      }
    }

    public HttpCookiesSection HttpCookies
    {
      get
      {
        return default(HttpCookiesSection);
      }
    }

    public HttpHandlersSection HttpHandlers
    {
      get
      {
        return default(HttpHandlersSection);
      }
    }

    public HttpModulesSection HttpModules
    {
      get
      {
        return default(HttpModulesSection);
      }
    }

    public HttpRuntimeSection HttpRuntime
    {
      get
      {
        return default(HttpRuntimeSection);
      }
    }

    public IdentitySection Identity
    {
      get
      {
        return default(IdentitySection);
      }
    }

    public MachineKeySection MachineKey
    {
      get
      {
        return default(MachineKeySection);
      }
    }

    public MembershipSection Membership
    {
      get
      {
        return default(MembershipSection);
      }
    }

    public System.Configuration.ConfigurationSection MobileControls
    {
      get
      {
        return default(System.Configuration.ConfigurationSection);
      }
    }

    public PagesSection Pages
    {
      get
      {
        return default(PagesSection);
      }
    }

    public PartialTrustVisibleAssembliesSection PartialTrustVisibleAssemblies
    {
      get
      {
        return default(PartialTrustVisibleAssembliesSection);
      }
    }

    public ProcessModelSection ProcessModel
    {
      get
      {
        return default(ProcessModelSection);
      }
    }

    public ProfileSection Profile
    {
      get
      {
        return default(ProfileSection);
      }
    }

    public System.Configuration.DefaultSection Protocols
    {
      get
      {
        return default(System.Configuration.DefaultSection);
      }
    }

    public RoleManagerSection RoleManager
    {
      get
      {
        return default(RoleManagerSection);
      }
    }

    public SecurityPolicySection SecurityPolicy
    {
      get
      {
        return default(SecurityPolicySection);
      }
    }

    public SessionStateSection SessionState
    {
      get
      {
        return default(SessionStateSection);
      }
    }

    public SiteMapSection SiteMap
    {
      get
      {
        return default(SiteMapSection);
      }
    }

    public TraceSection Trace
    {
      get
      {
        return default(TraceSection);
      }
    }

    public TrustSection Trust
    {
      get
      {
        return default(TrustSection);
      }
    }

    public UrlMappingsSection UrlMappings
    {
      get
      {
        return default(UrlMappingsSection);
      }
    }

    public WebControlsSection WebControls
    {
      get
      {
        return default(WebControlsSection);
      }
    }

    public WebPartsSection WebParts
    {
      get
      {
        return default(WebPartsSection);
      }
    }

    public System.Web.Services.Configuration.WebServicesSection WebServices
    {
      get
      {
        return default(System.Web.Services.Configuration.WebServicesSection);
      }
    }

    public XhtmlConformanceSection XhtmlConformance
    {
      get
      {
        return default(XhtmlConformanceSection);
      }
    }
    #endregion
  }
}
