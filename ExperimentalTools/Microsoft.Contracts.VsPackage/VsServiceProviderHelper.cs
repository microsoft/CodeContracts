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

// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//   Code stolen from Pex.
// 
// ==--==

using System;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts
{
  /// <summary>
  /// Helper method for <see cref="IServiceProvider"/> instances
  /// </summary>
  public static class VsServiceProviderHelper
  {
    /// <summary>
    /// Gets an <typeparamref name="T"/> service instance associated to <typeparamref name="V"/>
    /// </summary>
    /// <typeparam name="T"/>
    /// <typeparam name="V"/>
    public static T GetService<T, V>(IServiceProvider serviceProvider)
      where T : class
    {
      Contract.Requires(serviceProvider != null);
      return (T)serviceProvider.GetService(typeof(V));
    }

    /// <summary>
    /// Gets an service instance associated to <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>It can return null</returns>
    public static T GetService<T>(IServiceProvider serviceProvider)
      where T : class
    {
      Contract.Requires(serviceProvider != null);
      
      return (T)serviceProvider.GetService(typeof(T));
    }
  }
}
