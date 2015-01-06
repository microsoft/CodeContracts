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

// File System.Web.Routing.RouteCollection.cs
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


namespace System.Web.Routing
{
  public partial class RouteCollection : System.Collections.ObjectModel.Collection<RouteBase>
  {
    #region Methods and constructors
    public void Add (string name, RouteBase item)
    {
      Contract.Ensures (this.Count >= 1);
    }

    protected override void ClearItems ()
    {
    }

    public IDisposable GetReadLock ()
    {
      Contract.Ensures (Contract.Result<System.IDisposable>() != null);

      return default(IDisposable);
    }

    public RouteData GetRouteData (System.Web.HttpContextBase httpContext)
    {
      return default(RouteData);
    }

    public VirtualPathData GetVirtualPath (RequestContext requestContext, RouteValueDictionary values)
    {
      return default(VirtualPathData);
    }

    public VirtualPathData GetVirtualPath (RequestContext requestContext, string name, RouteValueDictionary values)
    {
      return default(VirtualPathData);
    }

    public IDisposable GetWriteLock ()
    {
      Contract.Ensures (Contract.Result<System.IDisposable>() != null);

      return default(IDisposable);
    }

    public void Ignore (string url)
    {
      Contract.Ensures (url.Length >= 0);
      Contract.Ensures (this.Count >= 1);
    }

    public void Ignore (string url, Object constraints)
    {
      Contract.Ensures (url.Length >= 0);
      Contract.Ensures (this.Count >= 1);
    }

    protected override void InsertItem (int index, RouteBase item)
    {
    }

    public Route MapPageRoute (string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess)
    {
      Contract.Ensures (Contract.Result<System.Web.Routing.Route>() != null);
      Contract.Ensures (routeUrl.Length >= 0);
      Contract.Ensures (this.Count >= 1);

      return default(Route);
    }

    public Route MapPageRoute (string routeName, string routeUrl, string physicalFile)
    {
      Contract.Ensures (Contract.Result<System.Web.Routing.Route>() != null);
      Contract.Ensures (routeUrl.Length >= 0);
      Contract.Ensures (this.Count >= 1);

      return default(Route);
    }

    public Route MapPageRoute (string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults)
    {
      Contract.Ensures (Contract.Result<System.Web.Routing.Route>() != null);
      Contract.Ensures (routeUrl.Length >= 0);
      Contract.Ensures (this.Count >= 1);

      return default(Route);
    }

    public Route MapPageRoute (string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
    {
      Contract.Ensures (Contract.Result<System.Web.Routing.Route>() != null);
      Contract.Ensures (routeUrl.Length >= 0);
      Contract.Ensures (this.Count >= 1);

      return default(Route);
    }

    public Route MapPageRoute (string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints)
    {
      Contract.Ensures (Contract.Result<System.Web.Routing.Route>() != null);
      Contract.Ensures (routeUrl.Length >= 0);
      Contract.Ensures (this.Count >= 1);

      return default(Route);
    }

    protected override void RemoveItem (int index)
    {
    }

    public RouteCollection ()
    {
    }

    public RouteCollection (System.Web.Hosting.VirtualPathProvider virtualPathProvider)
    {
    }

    protected override void SetItem (int index, RouteBase item)
    {
    }
    #endregion

    #region Properties and indexers
    public RouteBase this [string name]
    {
      get
      {
        return default(RouteBase);
      }
    }

    public bool RouteExistingFiles
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
