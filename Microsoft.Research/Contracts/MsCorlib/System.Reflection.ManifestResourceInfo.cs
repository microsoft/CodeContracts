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
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Reflection {
  // Summary:
  //     Provides access to manifest resources, which are XML files that describe
  //     application dependencies.
  [ComVisible(true)]
  public class ManifestResourceInfo {
    // Summary:
    //     Indicates the name of the file containing the manifest resource, if not the
    //     same as the manifest file. This property is read-only.
    //
    // Returns:
    //     A String that is the manifest resource's file name.
    public virtual string FileName { get; }
    //
    // Summary:
    //     Indicates the containing assembly. This property is read-only.
    //
    // Returns:
    //     An System.Reflection.Assembly object representing the manifest resource's
    //     containing assembly.
    public virtual Assembly ReferencedAssembly { get; }
    //
    // Summary:
    //     Indicates the manifest resource's location. This property is read-only.
    //
    // Returns:
    //     A combination of the System.Reflection.ResourceLocation flags.
    public virtual ResourceLocation ResourceLocation { get; }
  }
}
