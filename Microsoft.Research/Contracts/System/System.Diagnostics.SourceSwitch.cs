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

#if !SILVERLIGHT
using System;

namespace System.Diagnostics
{
  // Summary:
  //     Provides a multilevel switch to control tracing and debug output without
  //     recompiling your code.
  public class SourceSwitch : Switch
  {
    // Summary:
    //     Initializes a new instance of the System.Diagnostics.SourceSwitch class,
    //     specifying the name of the source.
    //
    // Parameters:
    //   name:
    //     The name of the source.
    public SourceSwitch(string name) : base(null, null) { }
    //
    // Summary:
    //     Initializes a new instance of the System.Diagnostics.SourceSwitch class,
    //     specifying the display name and the default value for the source switch.
    //
    // Parameters:
    //   displayName:
    //     The name of the source switch.
    //
    //   defaultSwitchValue:
    //     The default value for the switch.
    //public SourceSwitch(string displayName, string defaultSwitchValue);

    // Summary:
    //     Gets or sets the level of the switch.
    //
    // Returns:
    //     One of the System.Diagnostics.SourceLevels values that represents the event
    //     level of the switch.
    //public SourceLevels Level { get; set; }

    //
    // Summary:
    //     Determines if trace listeners should be called, based on the trace event
    //     type.
    //
    // Parameters:
    //   eventType:
    //     One of the System.Diagnostics.TraceEventType values.
    //
    // Returns:
    //     True if the trace listeners should be called; otherwise, false.
    //public bool ShouldTrace(TraceEventType eventType);
  }
}
#endif