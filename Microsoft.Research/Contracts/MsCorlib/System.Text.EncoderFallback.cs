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

namespace System.Text {
  // Summary:
  //     Provides a failure-handling mechanism, called a fallback, for an input character
  //     that cannot be converted to an encoded output byte sequence.
  
  public abstract class EncoderFallback {
    // Summary:
    //     Initializes a new instance of the System.Text.EncoderFallback class.
    protected EncoderFallback() { }

    // Summary:
    //     Gets an object that throws an exception when an input character cannot be
    //     encoded.
    //
    // Returns:
    //     A type derived from the System.Text.EncoderFallback class. The default value
    //     is a System.Text.EncoderExceptionFallback object.
    extern public static EncoderFallback ExceptionFallback { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets the maximum number of characters
    //     the current System.Text.EncoderFallback object can return.
    //
    // Returns:
    //     The maximum number of characters the current System.Text.EncoderFallback
    //     object can return.
    public abstract int MaxCharCount { get; }
    //
    // Summary:
    //     Gets an object that outputs a substitute string in place of an input character
    //     that cannot be encoded.
    //
    // Returns:
    //     A type derived from the System.Text.EncoderFallback class. The default value
    //     is a System.Text.EncoderReplacementFallback object that replaces unknown
    //     input characters with the QUESTION MARK character ("?", U+003F).
    extern public static EncoderFallback ReplacementFallback { get; }

    // Summary:
    //     When overridden in a derived class, initializes a new instance of the System.Text.EncoderFallbackBuffer
    //     class.
    //
    // Returns:
    //     A System.Text.EncoderFallbackBuffer object.
    public abstract EncoderFallbackBuffer CreateFallbackBuffer();
  }
}
