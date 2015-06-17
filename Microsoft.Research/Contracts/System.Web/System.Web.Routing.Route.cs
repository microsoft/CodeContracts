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

// File System.Web.Routing.Route.cs
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
  public partial class Route : RouteBase
  {
    #region Methods and constructors
    public override RouteData GetRouteData (System.Web.HttpContextBase httpContext)
    {
      return default(RouteData);
    }

    public override VirtualPathData GetVirtualPath (RequestContext requestContext, RouteValueDictionary values)
    {
      return default(VirtualPathData);
    }

    protected virtual new bool ProcessConstraint (System.Web.HttpContextBase httpContext, Object constraint, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
    {
      Contract.Requires (values != null);

      return default(bool);
    }

    public Route (string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
    {
    }

    public Route (string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
    {
    }

    public Route (string url, IRouteHandler routeHandler)
    {
    }

    public Route (string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
    {
    }
    #endregion

    #region Properties and indexers
    public RouteValueDictionary Constraints
    {
      get
      {
        return default(RouteValueDictionary);
      }
      set
      {
      }
    }

    public RouteValueDictionary DataTokens
    {
      get
      {
        return default(RouteValueDictionary);
      }
      set
      {
      }
    }

    public RouteValueDictionary Defaults
    {
      get
      {
        return default(RouteValueDictionary);
      }
      set
      {
      }
    }

    public IRouteHandler RouteHandler
    {
      get
      {
        return default(IRouteHandler);
      }
      set
      {
      }
    }

    public string Url
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion
  }
}
