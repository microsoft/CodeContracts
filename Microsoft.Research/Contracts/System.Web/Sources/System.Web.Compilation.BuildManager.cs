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

// File System.Web.Compilation.BuildManager.cs
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


namespace System.Web.Compilation
{
  sealed public partial class BuildManager
  {
    #region Methods and constructors
    public static void AddReferencedAssembly(System.Reflection.Assembly assembly)
    {
    }

    internal BuildManager()
    {
    }

    public static Stream CreateCachedFile(string fileName)
    {
      return default(Stream);
    }

    public static Object CreateInstanceFromVirtualPath(string virtualPath, Type requiredBaseType)
    {
      return default(Object);
    }

    public static BuildDependencySet GetCachedBuildDependencySet(System.Web.HttpContext context, string virtualPath)
    {
      return default(BuildDependencySet);
    }

    public static BuildDependencySet GetCachedBuildDependencySet(System.Web.HttpContext context, string virtualPath, bool ensureIsUpToDate)
    {
      return default(BuildDependencySet);
    }

    public static System.Reflection.Assembly GetCompiledAssembly(string virtualPath)
    {
      return default(System.Reflection.Assembly);
    }

    public static string GetCompiledCustomString(string virtualPath)
    {
      return default(string);
    }

    public static Type GetCompiledType(string virtualPath)
    {
      return default(Type);
    }

    public static System.Web.Util.IWebObjectFactory GetObjectFactory(string virtualPath, bool throwIfNotFound)
    {
      return default(System.Web.Util.IWebObjectFactory);
    }

    public static System.Collections.ICollection GetReferencedAssemblies()
    {
      return default(System.Collections.ICollection);
    }

    public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
    {
      return default(Type);
    }

    public static Type GetType(string typeName, bool throwOnError)
    {
      return default(Type);
    }

    public static System.Collections.ICollection GetVirtualPathDependencies(string virtualPath)
    {
      return default(System.Collections.ICollection);
    }

    public static Stream ReadCachedFile(string fileName)
    {
      return default(Stream);
    }
    #endregion

    #region Properties and indexers
    public static Nullable<bool> BatchCompilationEnabled
    {
      get
      {
        return default(Nullable<bool>);
      }
      set
      {
      }
    }

    public static System.Collections.IList CodeAssemblies
    {
      get
      {
        return default(System.Collections.IList);
      }
    }

    public static System.Runtime.Versioning.FrameworkName TargetFramework
    {
      get
      {
        return default(System.Runtime.Versioning.FrameworkName);
      }
    }
    #endregion
  }
}
