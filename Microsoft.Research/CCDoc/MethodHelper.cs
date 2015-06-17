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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Cci;
using System.Diagnostics.Contracts;

namespace CCDoc {
  /// <summary>
  /// Helper class for method definitions and references
  /// </summary>
  public static class MethodHelper {
    /// <summary>
    /// Checks if the method is a "get" accessor for a property
    /// </summary>
    public static bool IsGetter(IMethodDefinition methodDefinition) {
      Contract.Requires(methodDefinition != null);
      Contract.Requires(methodDefinition.Name != null);
      Contract.Requires(methodDefinition.Name.Value != null);
      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("get_");
    }

    /// <summary>
    /// Checks if the method is a "set" accessor for a property
    /// </summary>
    public static bool IsSetter(IMethodDefinition methodDefinition) {
      Contract.Requires(methodDefinition != null);
      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("set_");
    }

    /// <summary>
    /// Returns the unspecialized version of the given method reference.
    /// </summary>
    public static IMethodReference Unspecialize(IMethodReference method) {
      var smr = method as ISpecializedMethodReference;
      if (smr != null) return smr.UnspecializedVersion;
      var gmir = method as IGenericMethodInstanceReference;
      if (gmir != null) return gmir.GenericMethod;
      return method;
    }

    /// <summary>
    /// Returns the unspecialized version of the given method reference.
    /// </summary>
    public static IMethodReference Unspecialize(IMethodDefinition method) {
      var smd = method as ISpecializedMethodDefinition;
      if (smd != null) return smd.UnspecializedVersion;
      var gmi = method as IGenericMethodInstance;
      if (gmi != null) return gmi.GenericMethod;
      return method;
    }

    /// <summary>
    /// Returns the unspecialized version of the given type reference.
    /// </summary>
    [Pure]
    public static ITypeReference Unspecialize(ITypeReference type) {
      var sntr = type as ISpecializedNestedTypeReference;
      if (sntr != null) return sntr.UnspecializedVersion;
      var gtir = type as IGenericTypeInstanceReference;
      if (gtir != null) return gtir.GenericType;
      return type;
    }

    /// <summary>
    /// Returns the unspecialized version of the given type definition.
    /// </summary>
    [Pure]
    public static ITypeReference Unspecialize(ITypeDefinition type) {
      var sntd = type as ISpecializedNestedTypeDefinition;
      if (sntd != null) return sntd.UnspecializedVersion;
      var gti = type as IGenericTypeInstance;
      if (gti != null) return gti.GenericType;
      return type;
    }
  }
}
