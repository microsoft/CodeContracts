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

namespace System.Text
{
  // Summary:
  //     Passes a string to an encoding operation that is emitted instead of any input
  //     character that cannot be encoded.
  public abstract class EncoderFallbackBuffer
  {
    // Summary:
    //     Initializes a new instance of the System.Text.EncoderFallbackBuffer class.
    protected EncoderFallbackBuffer();

    // Summary:
    //     When overridden in a derived class, gets the number of characters in the
    //     current System.Text.EncoderFallbackBuffer object that remain to be processed.
    //
    // Returns:
    //     The number of characters in the current fallback buffer that have not yet
    //     been processed.
    public abstract int Remaining { get; }

    // Summary:
    //     When overridden in a derived class, prepares the fallback buffer to handle
    //     the specified input character.
    //
    // Parameters:
    //   charUnknown:
    //     An input character.
    //
    //   index:
    //     The index position of the character in the input buffer.
    //
    // Returns:
    //     true if the fallback buffer can process charUnknown; false if the fallback
    //     buffer ignores charUnknown.
    public abstract bool Fallback(char charUnknown, int index);
    //
    // Summary:
    //     When overridden in a derived class, prepares the fallback buffer to handle
    //     the specified surrogate pair.
    //
    // Parameters:
    //   charUnknownHigh:
    //     The high surrogate of the input pair.
    //
    //   charUnknownLow:
    //     The low surrogate of the input pair.
    //
    //   index:
    //     The index position of the surrogate pair in the input buffer.
    //
    // Returns:
    //     True if the fallback buffer can process charUnknownHigh and charUnknownLow;
    //     false if fallback buffer ignores the surrogate pair.
    public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);
    //
    // Summary:
    //     When overridden in a derived class, retrieves the next character in the fallback
    //     buffer.
    //
    // Returns:
    //     The next character in the fallback buffer.
    public abstract char GetNextChar();
    //
    // Summary:
    //     When overridden in a derived class, causes the next call to the System.Text.EncoderFallbackBuffer.GetNextChar()
    //     method to access the data buffer character position that is prior to the
    //     current character position.
    //
    // Returns:
    //     true if the System.Text.EncoderFallbackBuffer.MovePrevious() operation was
    //     successful; otherwise, false.
    public abstract bool MovePrevious();
    //
    // Summary:
    //     Initializes all data and state information pertaining to this fallback buffer.
    public virtual void Reset();
  }
}
