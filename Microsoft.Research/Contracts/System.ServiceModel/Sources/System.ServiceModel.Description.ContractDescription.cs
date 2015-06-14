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

// File System.ServiceModel.Description.ContractDescription.cs
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


namespace System.ServiceModel.Description
{
  public partial class ContractDescription
  {
    #region Methods and constructors
    public ContractDescription(string name)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
    }

    public ContractDescription(string name, string ns)
    {
      Contract.Requires(!String.IsNullOrEmpty(name));
    }

    public static System.ServiceModel.Description.ContractDescription GetContract(Type contractType, Type serviceType)
    {
      return default(System.ServiceModel.Description.ContractDescription);
    }

    public static System.ServiceModel.Description.ContractDescription GetContract(Type contractType)
    {
      return default(System.ServiceModel.Description.ContractDescription);
    }

    public static System.ServiceModel.Description.ContractDescription GetContract(Type contractType, Object serviceImplementation)
    {
      return default(System.ServiceModel.Description.ContractDescription);
    }

    public System.Collections.ObjectModel.Collection<System.ServiceModel.Description.ContractDescription> GetInheritedContracts()
    {
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.ServiceModel.Description.ContractDescription>>() != null);

      return default(System.Collections.ObjectModel.Collection<System.ServiceModel.Description.ContractDescription>);
    }

    public bool ShouldSerializeProtectionLevel()
    {
      Contract.Ensures(Contract.Result<bool>() == this.HasProtectionLevel);

      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public KeyedByTypeCollection<IContractBehavior> Behaviors
    {
      get
      {
        return default(KeyedByTypeCollection<IContractBehavior>);
      }
    }

    public Type CallbackContractType
    {
      get
      {
        return default(Type);
      }
      set
      {
      }
    }

    public string ConfigurationName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Type ContractType
    {
      get
      {
        return default(Type);
      }
      set
      {
      }
    }

    public bool HasProtectionLevel
    {
      get
      {
        return default(bool);
      }
    }

    public string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
      set
      {
        Contract.Requires(!String.IsNullOrEmpty(value));
      }
    }

    public string Namespace
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public OperationDescriptionCollection Operations
    {
      get
      {
        Contract.Ensures(Contract.Result<OperationDescriptionCollection>() != null);

        return default(OperationDescriptionCollection);
      }
    }

    public System.Net.Security.ProtectionLevel ProtectionLevel
    {
      get
      {
        return default(System.Net.Security.ProtectionLevel);
      }
      set
      {
      }
    }

    public System.ServiceModel.SessionMode SessionMode
    {
      get
      {
        return default(System.ServiceModel.SessionMode);
      }
      set
      {
      }
    }
    #endregion
  }
}
