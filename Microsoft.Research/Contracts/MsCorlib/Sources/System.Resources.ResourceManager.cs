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

// File System.Resources.ResourceManager.cs
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


namespace System.Resources
{
  public partial class ResourceManager
  {
    #region Methods and constructors
    public static System.Resources.ResourceManager CreateFileBasedResourceManager(string baseName, string resourceDir, Type usingResourceSet)
    {
      Contract.Ensures(Contract.Result<System.Resources.ResourceManager>() != null);

      return default(System.Resources.ResourceManager);
    }

    protected static System.Globalization.CultureInfo GetNeutralResourcesLanguage(System.Reflection.Assembly a)
    {
      Contract.Ensures(Contract.Result<System.Globalization.CultureInfo>() != null);

      return default(System.Globalization.CultureInfo);
    }

    public virtual new Object GetObject(string name)
    {
      return default(Object);
    }

    public virtual new Object GetObject(string name, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    protected virtual new string GetResourceFileName(System.Globalization.CultureInfo culture)
    {
      Contract.Requires(culture != null);

      return default(string);
    }

    public virtual new ResourceSet GetResourceSet(System.Globalization.CultureInfo culture, bool createIfNotExists, bool tryParents)
    {
      return default(ResourceSet);
    }

    protected static Version GetSatelliteContractVersion(System.Reflection.Assembly a)
    {
      return default(Version);
    }

    public UnmanagedMemoryStream GetStream(string name, System.Globalization.CultureInfo culture)
    {
      return default(UnmanagedMemoryStream);
    }

    public UnmanagedMemoryStream GetStream(string name)
    {
      return default(UnmanagedMemoryStream);
    }

    public virtual new string GetString(string name, System.Globalization.CultureInfo culture)
    {
      return default(string);
    }

    public virtual new string GetString(string name)
    {
      return default(string);
    }

    protected virtual new ResourceSet InternalGetResourceSet(System.Globalization.CultureInfo culture, bool createIfNotExists, bool tryParents)
    {
      return default(ResourceSet);
    }

    public virtual new void ReleaseAllResources()
    {
    }

    public ResourceManager(string baseName, System.Reflection.Assembly assembly)
    {
      Contract.Ensures(this.ResourceSets != null);
    }

    protected ResourceManager()
    {
    }

    public ResourceManager(Type resourceSource)
    {
      Contract.Ensures(this.ResourceSets != null);
    }

    public ResourceManager(string baseName, System.Reflection.Assembly assembly, Type usingResourceSet)
    {
      Contract.Ensures(this.ResourceSets != null);
    }
    #endregion

    #region Properties and indexers
    public virtual new string BaseName
    {
      get
      {
        return default(string);
      }
    }

    protected UltimateResourceFallbackLocation FallbackLocation
    {
      get
      {
        return default(UltimateResourceFallbackLocation);
      }
      set
      {
      }
    }

    public virtual new bool IgnoreCase
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new Type ResourceSetType
    {
      get
      {
        return default(Type);
      }
    }
    #endregion

    #region Fields
    protected string BaseNameField;
    public readonly static int HeaderVersionNumber;
    public readonly static int MagicNumber;
    protected System.Reflection.Assembly MainAssembly;
    protected System.Collections.Hashtable ResourceSets;
    #endregion
  }
}
