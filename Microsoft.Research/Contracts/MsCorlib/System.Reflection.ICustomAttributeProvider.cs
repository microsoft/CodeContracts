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
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Reflection {
  // Summary:
  //     Provides custom attributes for reflection objects that support them.
  [ContractClass(typeof(CustomAttributeProviderContract))]
  public interface ICustomAttributeProvider {
    // Summary:
    //     Returns an array of all of the custom attributes defined on this member,
    //     excluding named attributes, or an empty array if there are no custom attributes.
    //
    // Parameters:
    //   inherit:
    //     When true, look up the hierarchy chain for the inherited custom attribute.
    //
    // Returns:
    //     An array of Objects representing custom attributes, or an empty array.
    //
    // Exceptions:
    //   System.TypeLoadException:
    //     The custom attribute type cannot be loaded.
    //
    //   System.Reflection.AmbiguousMatchException:
    //     There is more than one attribute of type attributeType defined on this member.
    [Pure]
    object[] GetCustomAttributes(bool inherit);
    //
    // Summary:
    //     Returns an array of custom attributes defined on this member, identified
    //     by type, or an empty array if there are no custom attributes of that type.
    //
    // Parameters:
    //   attributeType:
    //     The type of the custom attributes.
    //
    //   inherit:
    //     When true, look up the hierarchy chain for the inherited custom attribute.
    //
    // Returns:
    //     An array of Objects representing custom attributes, or an empty array.
    //
    // Exceptions:
    //   System.TypeLoadException:
    //     The custom attribute type cannot be loaded.
    //
    //   System.Reflection.AmbiguousMatchException:
    //     There is more than one attribute of type attributeType defined on this member.
    [Pure]
    object[] GetCustomAttributes(Type attributeType, bool inherit);
    //
    // Summary:
    //     Indicates whether one or more instance of attributeType is defined on this
    //     member.
    //
    // Parameters:
    //   attributeType:
    //     The type of the custom attributes.
    //
    //   inherit:
    //     When true, look up the hierarchy chain for the inherited custom attribute.
    //
    // Returns:
    //     true if the attributeType is defined on this member; false otherwise.
    [Pure]
    bool IsDefined(Type attributeType, bool inherit);
  }

  [ContractClassFor(typeof(ICustomAttributeProvider))]
  abstract class CustomAttributeProviderContract : ICustomAttributeProvider
  {
    #region ICustomAttributeProvider Members

    [Pure]
    object[] ICustomAttributeProvider.GetCustomAttributes(bool inherit)
    {
      Contract.Ensures(Contract.Result<object[]>() != null);

      throw new NotImplementedException();
    }

    [Pure]
    object[] ICustomAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)
    {
      Contract.Requires(attributeType != null);
      Contract.Ensures(Contract.Result<object[]>() != null);

      throw new NotImplementedException();
    }

    [Pure]
    bool ICustomAttributeProvider.IsDefined(Type attributeType, bool inherit)
    {
      Contract.Requires(attributeType != null);

      throw new NotImplementedException();
    }

    #endregion
  }
}
