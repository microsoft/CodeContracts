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


// File System.Web.SiteMapProvider.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Web
{
  abstract public partial class SiteMapProvider : System.Configuration.Provider.ProviderBase
  {
    #region Methods and constructors
    protected internal virtual new void AddNode (SiteMapNode node, SiteMapNode parentNode)
    {
    }

    protected virtual new void AddNode (SiteMapNode node)
    {
    }

    public virtual new SiteMapNode FindSiteMapNode (HttpContext context)
    {
      return default(SiteMapNode);
    }

    public abstract SiteMapNode FindSiteMapNode (string rawUrl);

    public virtual new SiteMapNode FindSiteMapNodeFromKey (string key)
    {
      return default(SiteMapNode);
    }

    public abstract SiteMapNodeCollection GetChildNodes (SiteMapNode node);

    public virtual new SiteMapNode GetCurrentNodeAndHintAncestorNodes (int upLevel)
    {
      return default(SiteMapNode);
    }

    public virtual new SiteMapNode GetCurrentNodeAndHintNeighborhoodNodes (int upLevel, int downLevel)
    {
      return default(SiteMapNode);
    }

    public abstract SiteMapNode GetParentNode (SiteMapNode node);

    public virtual new SiteMapNode GetParentNodeRelativeToCurrentNodeAndHintDownFromParent (int walkupLevels, int relativeDepthFromWalkup)
    {
      return default(SiteMapNode);
    }

    public virtual new SiteMapNode GetParentNodeRelativeToNodeAndHintDownFromParent (SiteMapNode node, int walkupLevels, int relativeDepthFromWalkup)
    {
      return default(SiteMapNode);
    }

    protected internal abstract SiteMapNode GetRootNodeCore ();

    protected static SiteMapNode GetRootNodeCoreFromProvider (SiteMapProvider provider)
    {
      Contract.Requires (provider != null);

      return default(SiteMapNode);
    }

    public virtual new void HintAncestorNodes (SiteMapNode node, int upLevel)
    {
    }

    public virtual new void HintNeighborhoodNodes (SiteMapNode node, int upLevel, int downLevel)
    {
    }

    public override void Initialize (string name, System.Collections.Specialized.NameValueCollection attributes)
    {
    }

    public virtual new bool IsAccessibleToUser (HttpContext context, SiteMapNode node)
    {
      return default(bool);
    }

    protected internal virtual new void RemoveNode (SiteMapNode node)
    {
    }

    protected SiteMapNode ResolveSiteMapNode (HttpContext context)
    {
      Contract.Requires (context != null);

      return default(SiteMapNode);
    }

    protected SiteMapProvider ()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new SiteMapNode CurrentNode
    {
      get
      {
        return default(SiteMapNode);
      }
    }

    public bool EnableLocalization
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new System.Web.SiteMapProvider ParentProvider
    {
      get
      {
        return default(System.Web.SiteMapProvider);
      }
      set
      {
      }
    }

    public string ResourceKey
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new SiteMapNode RootNode
    {
      get
      {
        return default(SiteMapNode);
      }
    }

    public virtual new System.Web.SiteMapProvider RootProvider
    {
      get
      {
        return default(System.Web.SiteMapProvider);
      }
    }

    public bool SecurityTrimmingEnabled
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}

