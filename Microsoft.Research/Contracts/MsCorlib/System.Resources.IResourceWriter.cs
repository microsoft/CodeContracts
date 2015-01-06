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

namespace System.Resources {
  // Summary:
  //     Provides functionality to write resources to an output file or stream.
  [ComVisible(true)]
  public interface IResourceWriter : IDisposable {
    // Summary:
    //     Adds an 8-bit unsigned integer array as a named resource to the list of resources
    //     to be written.
    //
    // Parameters:
    //   name:
    //     Name of a resource.
    //
    //   value:
    //     Value of a resource as an 8-bit unsigned integer array.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The name parameter is null.
    void AddResource(string name, byte[] value);
    //
    // Summary:
    //     Adds a named resource of type System.Object to the list of resources to be
    //     written.
    //
    // Parameters:
    //   name:
    //     The name of the resource.
    //
    //   value:
    //     The value of the resource.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The name parameter is null.
    void AddResource(string name, object value);
    //
    // Summary:
    //     Adds a named resource of type System.String to the list of resources to be
    //     written.
    //
    // Parameters:
    //   name:
    //     The name of the resource.
    //
    //   value:
    //     The value of the resource.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The name parameter is null.
    void AddResource(string name, string value);
    //
    // Summary:
    //     Closes the underlying resource file or stream, ensuring all the data has
    //     been written to the file.
    void Close();
    //
    // Summary:
    //     Writes all the resources added by the System.Resources.IResourceWriter.AddResource(System.String,System.String)
    //     method to the output file or stream.
    void Generate();
  }
}
