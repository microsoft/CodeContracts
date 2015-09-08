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
using System.Reflection;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Diagnostics {
  // Summary:
  //     Provides information about a System.Diagnostics.StackFrame, which represents
  //     a function call on the call stack for the current thread.
  public class StackFrame
  {
    // Summary:
    //     Defines the value that is returned from the System.Diagnostics.StackFrame.GetNativeOffset()
    //     or System.Diagnostics.StackFrame.GetILOffset() method when the native or
    //     Microsoft intermediate language (MSIL) offset is unknown. This field is constant.
    public const int OFFSET_UNKNOWN = -1;

    // Summary:
    //     Initializes a new instance of the System.Diagnostics.StackFrame class.
    extern public StackFrame();
    //
    // Summary:
    //     Initializes a new instance of the System.Diagnostics.StackFrame class, optionally
    //     capturing source information.
    //
    // Parameters:
    //   fNeedFileInfo:
    //     true to capture the file name, line number, and column number of the stack
    //     frame; otherwise, false.
    extern public StackFrame(bool fNeedFileInfo);
    //
    // Summary:
    //     Initializes a new instance of the System.Diagnostics.StackFrame class that
    //     corresponds to a frame above the current stack frame.
    //
    // Parameters:
    //   skipFrames:
    //     The number of frames up the stack to skip.
    extern public StackFrame(int skipFrames);
    //
    // Summary:
    //     Initializes a new instance of the System.Diagnostics.StackFrame class that
    //     corresponds to a frame above the current stack frame, optionally capturing
    //     source information.
    //
    // Parameters:
    //   skipFrames:
    //     The number of frames up the stack to skip.
    //
    //   fNeedFileInfo:
    //     true to capture the file name, line number, and column number of the stack
    //     frame; otherwise, false.
    extern public StackFrame(int skipFrames, bool fNeedFileInfo);
    //
    // Summary:
    //     Initializes a new instance of the System.Diagnostics.StackFrame class that
    //     contains only the given file name and line number.
    //
    // Parameters:
    //   fileName:
    //     The file name.
    //
    //   lineNumber:
    //     The line number in the specified file.
    extern public StackFrame(string fileName, int lineNumber);
    //
    // Summary:
    //     Initializes a new instance of the System.Diagnostics.StackFrame class that
    //     contains only the given file name, line number, and column number.
    //
    // Parameters:
    //   fileName:
    //     The file name.
    //
    //   lineNumber:
    //     The line number in the specified file.
    //
    //   colNumber:
    //     The column number in the specified file.
    extern public StackFrame(string fileName, int lineNumber, int colNumber);

    // Summary:
    //     Gets the column number in the file that contains the code that is executing.
    //     This information is typically extracted from the debugging symbols for the
    //     executable.
    //
    // Returns:
    //     The file column number.-or- Zero if the file column number cannot be determined.
    extern public virtual int GetFileColumnNumber();
    //
    // Summary:
    //     Gets the line number in the file that contains the code that is executing.
    //     This information is typically extracted from the debugging symbols for the
    //     executable.
    //
    // Returns:
    //     The file line number.-or- Zero if the file line number cannot be determined.
    extern public virtual int GetFileLineNumber();
    //
    // Summary:
    //     Gets the file name that contains the code that is executing. This information
    //     is typically extracted from the debugging symbols for the executable.
    //
    // Returns:
    //     The file name.-or- null if the file name cannot be determined.
    extern public virtual string GetFileName();
    //
    // Summary:
    //     Gets the offset from the start of the Microsoft intermediate language (MSIL)
    //     code for the method that is executing. This offset might be an approximation
    //     depending on whether the just-in-time (JIT) compiler is generating debugging
    //     code or not. The generation of this debugging information is controlled by
    //     the System.Diagnostics.DebuggableAttribute.
    //
    // Returns:
    //     The offset from the start of the MSIL code for the method that is executing.
    extern public virtual int GetILOffset();
    //
    // Summary:
    //     Gets the method in which the frame is executing.
    //
    // Returns:
    //     The method in which the frame is executing.
    extern public virtual MethodBase GetMethod();
    //
    // Summary:
    //     Gets the offset from the start of the native just-in-time (JIT)-compiled
    //     code for the method that is being executed. The generation of this debugging
    //     information is controlled by the System.Diagnostics.DebuggableAttribute class.
    //
    // Returns:
    //     The offset from the start of the JIT-compiled code for the method that is
    //     being executed.
    extern public virtual int GetNativeOffset();
  }
}
