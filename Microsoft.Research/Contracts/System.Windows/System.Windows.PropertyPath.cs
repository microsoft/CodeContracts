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

#region Assembly System.Windows.dll, v2.0.50727
// C:\Program Files\Reference Assemblies\Microsoft\Framework\Silverlight\v4.0\Profile\WindowsPhone\System.Windows.dll
#endregion

using System;

namespace System.Windows
{
  // Summary:
  //     Implements a data structure for describing a property as a path below another
  //     property, or below an owning type. Property paths are used in data binding
  //     to objects, and in storyboards and timelines for animations.
  public sealed class PropertyPath
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.PropertyPath class.
    //
    // Parameters:
    //   parameter:
    //     A dependency property identifier, or a property path string.
    extern public PropertyPath(object parameter);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.PropertyPath class.
    //
    // Parameters:
    //   path:
    //     The path string for this System.Windows.PropertyPath.
    //
    //   pathParameters:
    //     Do not use. See Remarks.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     Provided an array of length greater than zero for pathParameters.
    extern public PropertyPath(string path, params object[] pathParameters);

    // Summary:
    //     Gets the path value held by this System.Windows.PropertyPath.
    //
    // Returns:
    //     The path value held by this System.Windows.PropertyPath.
    public string Path { get; internal set; }
  }
}
