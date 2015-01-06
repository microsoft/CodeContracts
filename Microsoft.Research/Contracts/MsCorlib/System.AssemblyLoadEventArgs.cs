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

using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
  // Summary:
  //     Provides data for the System.AppDomain.AssemblyLoad event.
  public class AssemblyLoadEventArgs : EventArgs
  {
    // Summary:
    //     Initializes a new instance of the System.AssemblyLoadEventArgs class using
    //     the specified System.Reflection.Assembly.
    //
    // Parameters:
    //   loadedAssembly:
    //     An instance that represents the currently loaded assembly.
    public AssemblyLoadEventArgs(Assembly loadedAssembly);

    // Summary:
    //     Gets an System.Reflection.Assembly that represents the currently loaded assembly.
    //
    // Returns:
    //     An instance of System.Reflection.Assembly that represents the currently loaded
    //     assembly.
    public Assembly LoadedAssembly { get; }
  }
}
