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
//using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Diagnostics.Contracts;

namespace System.Windows
{
  // Summary:
  //     Represents the visual appearance of the control when it is in a specific
  //     state.
  //[ContentProperty("Storyboard", true)]
  public sealed class VisualState : DependencyObject
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.VisualState class.
    extern public VisualState();

    // Summary:
    //     Gets the name of the System.Windows.VisualState.
    //
    // Returns:
    //     The name of the System.Windows.VisualState.
    public string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    //
    // Summary:
    //     Gets or sets a System.Windows.Media.Animation.Storyboard that defines the
    //     appearance of the control when it is the state that is represented by the
    //     System.Windows.VisualState.
    //
    // Returns:
    //     A Storyboard that defines the appearance of the control when it is the state
    //     that is represented by the System.Windows.VisualState.
    public Storyboard Storyboard { get; set; }
  }
}
