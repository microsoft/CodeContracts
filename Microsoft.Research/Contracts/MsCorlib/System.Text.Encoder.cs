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

namespace System.Text {
  // Summary:
  //     Converts a set of characters into a sequence of bytes.

  public abstract class Encoder {
    // Summary:
    //     Initializes a new instance of the System.Text.Encoder class.
    extern protected Encoder();

    // Summary:
    //     Gets or sets a System.Text.EncoderFallback object for the current System.Text.Encoder
    //     object.
    //
    // Returns:
    //     A System.Text.EncoderFallback object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value in a set operation is null (Nothing).
    //
    //   System.ArgumentException:
    //     A new value cannot be assigned in a set operation because the current System.Text.EncoderFallbackBuffer
    //     object contains data that has not been encoded yet.
    //
    //   System.Text.EncoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Encoder.Fallback
    //     is set to System.Text.EncoderExceptionFallback.
#if false
    public EncoderFallback Fallback { get; set; }
#endif
    //
    // Summary:
    //     Gets the System.Text.EncoderFallbackBuffer object associated with the current
    //     System.Text.Encoder object.
    //
    // Returns:
    //     A System.Text.EncoderFallbackBuffer object.
#if false
    [ComVisible(false)]
    public EncoderFallbackBuffer FallbackBuffer { get; }
#endif
    // Summary:
    //     Converts a buffer of Unicode characters to an encoded byte sequence and stores
    //     the result in another buffer.
    //
    // Parameters:
    //   chars:
    //     The address of a string of UTF-16 encoded characters to convert.
    //
    //   charCount:
    //     The number of characters in chars to convert.
    //
    //   bytes:
    //     The address of a buffer to store the converted bytes.
    //
    //   byteCount:
    //     The maximum number of bytes in bytes to use in the conversion.
    //
    //   flush:
    //     true to indicate no further data is to be converted; otherwise, false.
    //
    //   charsUsed:
    //     When this method returns, contains the number of characters from chars that
    //     were used in the conversion. This parameter is passed uninitialized.
    //
    //   bytesUsed:
    //     When this method returns, contains the number of bytes that were used in
    //     the conversion. This parameter is passed uninitialized.
    //
    //   completed:
    //     When this method returns, contains true if all the characters specified by
    //     charCount were converted; otherwise, false. This parameter is passed uninitialized.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     chars or bytes is null (Nothing).
    //
    //   System.ArgumentOutOfRangeException:
    //     charCount or byteCount is less than zero.
    //
    //   System.ArgumentException:
    //     The output buffer is too small to contain any of the converted input. The
    //     output buffer should be greater than or equal to the size indicated by the
    //     Overload:System.Text.Encoder.GetByteCount method.
    //
    //   System.Text.EncoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Encoder.Fallback
    //     is set to System.Text.EncoderExceptionFallback.
#if !SILVERLIGHT_5_0
    extern unsafe public virtual void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed);
#endif
    //
    // Summary:
    //     Converts an array of Unicode characters to an encoded byte sequence and stores
    //     the result in an array of bytes.
    //
    // Parameters:
    //   chars:
    //     An array of characters to convert.
    //
    //   charIndex:
    //     The first element of chars to convert.
    //
    //   charCount:
    //     The number of elements of chars to convert.
    //
    //   bytes:
    //     An array where the converted bytes are stored.
    //
    //   byteIndex:
    //     The first element of bytes in which data is stored.
    //
    //   byteCount:
    //     The maximum number of elements of bytes to use in the conversion.
    //
    //   flush:
    //     true to indicate no further data is to be converted; otherwise, false.
    //
    //   charsUsed:
    //     When this method returns, contains the number of characters from chars that
    //     were used in the conversion. This parameter is passed uninitialized.
    //
    //   bytesUsed:
    //     When this method returns, contains the number of bytes that were produced
    //     by the conversion. This parameter is passed uninitialized.
    //
    //   completed:
    //     When this method returns, contains true if all the characters specified by
    //     charCount were converted; otherwise, false. This parameter is passed uninitialized.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     chars or bytes is null (Nothing).
    //
    //   System.ArgumentOutOfRangeException:
    //     charIndex, charCount, byteIndex, or byteCount is less than zero.-or-The length
    //     of chars - charIndex is less than charCount.-or-The length of bytes - byteIndex
    //     is less than byteCount.
    //
    //   System.ArgumentException:
    //     The output buffer is too small to contain any of the converted input. The
    //     output buffer should be greater than or equal to the size indicated by the
    //     Overload:System.Text.Encoder.GetByteCount method.
    //
    //   System.Text.EncoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Encoder.Fallback
    //     is set to System.Text.EncoderExceptionFallback.
    extern public virtual void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed);
    //
    // Summary:
    //     When overridden in a derived class, calculates the number of bytes produced
    //     by encoding a set of characters starting at the specified character pointer.
    //     A parameter indicates whether to clear the internal state of the encoder
    //     after the calculation.
    //
    // Parameters:
    //   chars:
    //     A pointer to the first character to encode.
    //
    //   count:
    //     The number of characters to encode.
    //
    //   flush:
    //     true to simulate clearing the internal state of the encoder after the calculation;
    //     otherwise, false.
    //
    // Returns:
    //     The number of bytes produced by encoding the specified characters and any
    //     characters in the internal buffer.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     chars is null (Nothing in Visual Basic .NET).
    //
    //   System.ArgumentOutOfRangeException:
    //     count is less than zero.
    //
    //   System.Text.EncoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Encoder.Fallback
    //     is set to System.Text.EncoderExceptionFallback.
#if !SILVERLIGHT_5_0
    extern unsafe public virtual int GetByteCount(char* chars, int count, bool flush);
#endif
    //
    // Summary:
    //     When overridden in a derived class, calculates the number of bytes produced
    //     by encoding a set of characters from the specified character array. A parameter
    //     indicates whether to clear the internal state of the encoder after the calculation.
    //
    // Parameters:
    //   chars:
    //     The character array containing the set of characters to encode.
    //
    //   index:
    //     The index of the first character to encode.
    //
    //   count:
    //     The number of characters to encode.
    //
    //   flush:
    //     true to simulate clearing the internal state of the encoder after the calculation;
    //     otherwise, false.
    //
    // Returns:
    //     The number of bytes produced by encoding the specified characters and any
    //     characters in the internal buffer.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     chars is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index or count is less than zero.-or- index and count do not denote a valid
    //     range in chars.
    //
    //   System.Text.EncoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Encoder.Fallback
    //     is set to System.Text.EncoderExceptionFallback.
    public abstract int GetByteCount(char[] chars, int index, int count, bool flush);
    //
    // Summary:
    //     When overridden in a derived class, encodes a set of characters starting
    //     at the specified character pointer and any characters in the internal buffer
    //     into a sequence of bytes that are stored starting at the specified byte pointer.
    //     A parameter indicates whether to clear the internal state of the encoder
    //     after the conversion.
    //
    // Parameters:
    //   chars:
    //     A pointer to the first character to encode.
    //
    //   charCount:
    //     The number of characters to encode.
    //
    //   bytes:
    //     A pointer to the location at which to start writing the resulting sequence
    //     of bytes.
    //
    //   byteCount:
    //     The maximum number of bytes to write.
    //
    //   flush:
    //     true to clear the internal state of the encoder after the conversion; otherwise,
    //     false.
    //
    // Returns:
    //     The actual number of bytes written at the location indicated by the bytes
    //     parameter.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     chars is null (Nothing).-or- bytes is null (Nothing).
    //
    //   System.ArgumentOutOfRangeException:
    //     charCount or byteCount is less than zero.
    //
    //   System.ArgumentException:
    //     byteCount is less than the resulting number of bytes.
    //
    //   System.Text.EncoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Encoder.Fallback
    //     is set to System.Text.EncoderExceptionFallback.
#if !SILVERLIGHT_5_0
    extern unsafe public virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush);
#endif

    //
    // Summary:
    //     When overridden in a derived class, encodes a set of characters from the
    //     specified character array and any characters in the internal buffer into
    //     the specified byte array. A parameter indicates whether to clear the internal
    //     state of the encoder after the conversion.
    //
    // Parameters:
    //   chars:
    //     The character array containing the set of characters to encode.
    //
    //   charIndex:
    //     The index of the first character to encode.
    //
    //   charCount:
    //     The number of characters to encode.
    //
    //   bytes:
    //     The byte array to contain the resulting sequence of bytes.
    //
    //   byteIndex:
    //     The index at which to start writing the resulting sequence of bytes.
    //
    //   flush:
    //     true to clear the internal state of the encoder after the conversion; otherwise,
    //     false.
    //
    // Returns:
    //     The actual number of bytes written into bytes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     chars is null (Nothing).-or- bytes is null (Nothing).
    //
    //   System.ArgumentOutOfRangeException:
    //     charIndex or charCount or byteIndex is less than zero.-or- charIndex and
    //     charCount do not denote a valid range in chars.-or- byteIndex is not a valid
    //     index in bytes.
    //
    //   System.ArgumentException:
    //     bytes does not have enough capacity from byteIndex to the end of the array
    //     to accommodate the resulting bytes.
    //
    //   System.Text.EncoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Encoder.Fallback
    //     is set to System.Text.EncoderExceptionFallback.
    public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush);

#if !SILVERLIGHT
    //
    // Summary:
    //     When overridden in a derived class, sets the encoder back to its initial
    //     state.
    extern public virtual void Reset();
#endif
  }
}
