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

// File System.ComponentModel.Composition.AttributedModelServices.cs
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


namespace System.ComponentModel.Composition
{
  static public partial class AttributedModelServices
  {
    #region Methods and constructors
    public static System.ComponentModel.Composition.Primitives.ComposablePart AddExportedValue<T>(System.ComponentModel.Composition.Hosting.CompositionBatch batch, T exportedValue)
    {
      Contract.Requires(batch != null);
      Contract.Ensures(Contract.Result<System.ComponentModel.Composition.Primitives.ComposablePart>() != null);

      return default(System.ComponentModel.Composition.Primitives.ComposablePart);
    }

    public static System.ComponentModel.Composition.Primitives.ComposablePart AddExportedValue<T>(System.ComponentModel.Composition.Hosting.CompositionBatch batch, string contractName, T exportedValue)
    {
      Contract.Requires(batch != null);
      Contract.Requires(!string.IsNullOrEmpty(contractName));
      Contract.Ensures(Contract.Result<System.ComponentModel.Composition.Primitives.ComposablePart>() != null);

      return default(System.ComponentModel.Composition.Primitives.ComposablePart);
    }

    public static System.ComponentModel.Composition.Primitives.ComposablePart AddPart(System.ComponentModel.Composition.Hosting.CompositionBatch batch, Object attributedPart)
    {
      Contract.Requires(batch != null);

      return default(System.ComponentModel.Composition.Primitives.ComposablePart);
    }

    public static void ComposeExportedValue<T>(System.ComponentModel.Composition.Hosting.CompositionContainer container, T exportedValue)
    {
      Contract.Requires(container != null);
    }

    public static void ComposeExportedValue<T>(System.ComponentModel.Composition.Hosting.CompositionContainer container, string contractName, T exportedValue)
    {
      Contract.Requires(container != null);
      Contract.Requires(!string.IsNullOrEmpty(contractName));
    }

    public static void ComposeParts(System.ComponentModel.Composition.Hosting.CompositionContainer container, Object[] attributedParts)
    {
      Contract.Requires(attributedParts != null);
      Contract.Requires(container != null);
    }

    public static System.ComponentModel.Composition.Primitives.ComposablePart CreatePart(Object attributedPart)
    {
      return default(System.ComponentModel.Composition.Primitives.ComposablePart);
    }

    public static System.ComponentModel.Composition.Primitives.ComposablePartDefinition CreatePartDefinition(Type type, System.ComponentModel.Composition.Primitives.ICompositionElement origin)
    {
      return default(System.ComponentModel.Composition.Primitives.ComposablePartDefinition);
    }

    public static System.ComponentModel.Composition.Primitives.ComposablePartDefinition CreatePartDefinition(Type type, System.ComponentModel.Composition.Primitives.ICompositionElement origin, bool ensureIsDiscoverable)
    {
      return default(System.ComponentModel.Composition.Primitives.ComposablePartDefinition);
    }

    public static string GetContractName(Type type)
    {
      return default(string);
    }

    public static TMetadataView GetMetadataView<TMetadataView>(IDictionary<string, Object> metadata)
    {
      return default(TMetadataView);
    }

    public static string GetTypeIdentity(Type type)
    {
      return default(string);
    }

    public static string GetTypeIdentity(System.Reflection.MethodInfo method)
    {
      Contract.Requires(method != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static System.ComponentModel.Composition.Primitives.ComposablePart SatisfyImportsOnce(ICompositionService compositionService, Object attributedPart)
    {
      Contract.Requires(compositionService != null);

      return default(System.ComponentModel.Composition.Primitives.ComposablePart);
    }
    #endregion
  }
}
