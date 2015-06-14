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

// File System.ComponentModel.Composition.ReflectionModel.ReflectionModelServices.cs
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


namespace System.ComponentModel.Composition.ReflectionModel
{
  static public partial class ReflectionModelServices
  {
    #region Methods and constructors
    public static System.ComponentModel.Composition.Primitives.ExportDefinition CreateExportDefinition(LazyMemberInfo exportingMember, string contractName, Lazy<IDictionary<string, Object>> metadata, System.ComponentModel.Composition.Primitives.ICompositionElement origin)
    {
      Contract.Requires(contractName != null);
      Contract.Ensures(Contract.Result<System.ComponentModel.Composition.Primitives.ExportDefinition>() != null);

      return default(System.ComponentModel.Composition.Primitives.ExportDefinition);
    }

    public static System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition CreateImportDefinition(Lazy<System.Reflection.ParameterInfo> parameter, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, System.ComponentModel.Composition.Primitives.ImportCardinality cardinality, System.ComponentModel.Composition.CreationPolicy requiredCreationPolicy, System.ComponentModel.Composition.Primitives.ICompositionElement origin)
    {
      Contract.Requires(contractName != null);
      Contract.Ensures(Contract.Result<System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition>() != null);

      return default(System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition);
    }

    public static System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition CreateImportDefinition(LazyMemberInfo importingMember, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, System.ComponentModel.Composition.Primitives.ImportCardinality cardinality, bool isRecomposable, System.ComponentModel.Composition.CreationPolicy requiredCreationPolicy, System.ComponentModel.Composition.Primitives.ICompositionElement origin)
    {
      Contract.Requires(contractName != null);
      Contract.Ensures(Contract.Result<System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition>() != null);

      return default(System.ComponentModel.Composition.Primitives.ContractBasedImportDefinition);
    }

    public static System.ComponentModel.Composition.Primitives.ComposablePartDefinition CreatePartDefinition(Lazy<Type> partType, bool isDisposalRequired, Lazy<IEnumerable<System.ComponentModel.Composition.Primitives.ImportDefinition>> imports, Lazy<IEnumerable<System.ComponentModel.Composition.Primitives.ExportDefinition>> exports, Lazy<IDictionary<string, Object>> metadata, System.ComponentModel.Composition.Primitives.ICompositionElement origin)
    {
      Contract.Ensures(Contract.Result<System.ComponentModel.Composition.Primitives.ComposablePartDefinition>() != null);

      return default(System.ComponentModel.Composition.Primitives.ComposablePartDefinition);
    }

    public static LazyMemberInfo GetExportingMember(System.ComponentModel.Composition.Primitives.ExportDefinition exportDefinition)
    {
      return default(LazyMemberInfo);
    }

    public static LazyMemberInfo GetImportingMember(System.ComponentModel.Composition.Primitives.ImportDefinition importDefinition)
    {
      return default(LazyMemberInfo);
    }

    public static Lazy<System.Reflection.ParameterInfo> GetImportingParameter(System.ComponentModel.Composition.Primitives.ImportDefinition importDefinition)
    {
      return default(Lazy<System.Reflection.ParameterInfo>);
    }

    public static Lazy<Type> GetPartType(System.ComponentModel.Composition.Primitives.ComposablePartDefinition partDefinition)
    {
      return default(Lazy<Type>);
    }

    public static bool IsDisposalRequired(System.ComponentModel.Composition.Primitives.ComposablePartDefinition partDefinition)
    {
      return default(bool);
    }

    public static bool IsImportingParameter(System.ComponentModel.Composition.Primitives.ImportDefinition importDefinition)
    {
      return default(bool);
    }
    #endregion
  }
}
