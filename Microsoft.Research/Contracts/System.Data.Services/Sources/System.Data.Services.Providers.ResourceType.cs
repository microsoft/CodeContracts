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

// File System.Data.Services.Providers.ResourceType.cs
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


namespace System.Data.Services.Providers
{
  public partial class ResourceType
  {
    #region Methods and constructors
    public void AddEntityPropertyMappingAttribute(System.Data.Services.Common.EntityPropertyMappingAttribute attribute)
    {
      Contract.Requires(attribute != null);
      Contract.Requires(this.ResourceTypeKind != ResourceTypeKind.EntityType);
    }

    public void AddProperty(ResourceProperty property)
    {
      Contract.Requires(property != null);
    }

    public static System.Data.Services.Providers.ResourceType GetPrimitiveResourceType(Type type)
    {
      Contract.Requires(type != null);


      return default(System.Data.Services.Providers.ResourceType);
    }

    protected virtual new IEnumerable<ResourceProperty> LoadPropertiesDeclaredOnThisType()
    {
      Contract.Ensures(Contract.Result<IEnumerable<ResourceProperty>>() != null);

      return default(IEnumerable<ResourceProperty>);
    }

    public ResourceType(Type instanceType, ResourceTypeKind resourceTypeKind, ResourceType baseType, string namespaceName, string name, bool isAbstract)
    {
      Contract.Requires(instanceType != null);
      Contract.Requires(resourceTypeKind != ResourceTypeKind.Primitive);
      Contract.Ensures(!instanceType.IsValueType);
    }

    public void SetReadOnly()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Data.Services.Providers.ResourceType BaseType
    {
      get
      {
        return default(System.Data.Services.Providers.ResourceType);
      }
    }

    public bool CanReflectOnInstanceType
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Object CustomState
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<ResourceProperty> ETagProperties
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<System.Data.Services.Providers.ResourceProperty>>() != null);

        return default(System.Collections.ObjectModel.ReadOnlyCollection<ResourceProperty>);
      }
    }

    public string FullName
    {
      get
      {
        return default(string);
      }
    }

    public Type InstanceType
    {
      get
      {
        return default(Type);
      }
    }

    public bool IsAbstract
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsMediaLinkEntry
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsOpenType
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<ResourceProperty> KeyProperties
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<System.Data.Services.Providers.ResourceProperty>>() != null);

        return default(System.Collections.ObjectModel.ReadOnlyCollection<ResourceProperty>);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public string Namespace
    {
      get
      {
        return default(string);
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<ResourceProperty> Properties
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<System.Data.Services.Providers.ResourceProperty>>() != null);

        return default(System.Collections.ObjectModel.ReadOnlyCollection<ResourceProperty>);
      }
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<ResourceProperty> PropertiesDeclaredOnThisType
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<System.Data.Services.Providers.ResourceProperty>>() != null);

        return default(System.Collections.ObjectModel.ReadOnlyCollection<ResourceProperty>);
      }
    }

    public ResourceTypeKind ResourceTypeKind
    {
      get
      {
        return default(ResourceTypeKind);
      }
    }
    #endregion
  }
}
