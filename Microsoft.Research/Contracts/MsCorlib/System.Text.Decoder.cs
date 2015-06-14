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
  //     Converts a sequence of encoded bytes into a set of characters.

  public abstract class Decoder {
#if false
    // Summary:
    //     Initializes a new instance of the System.Text.Decoder class.
    protected Decoder();

    // Summary:
    //     Gets or sets a System.Text.DecoderFallback object for the current System.Text.Decoder
    //     object.
    //
    // Returns:
    //     A System.Text.DecoderFallback object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value in a set operation is null (Nothing).
    //
    //   System.ArgumentException:
    //     A new value cannot be assigned in a set operation because the current System.Text.DecoderFallbackBuffer
    //     object contains data that has not been decoded yet.

    public DecoderFallback Fallback { get; set; }
    //
    // Summary:
    //     Gets the System.Text.DecoderFallbackBuffer object associated with the current
    //     System.Text.Decoder object.
    //
    // Returns:
    //     A System.Text.DecoderFallbackBuffer object.
    public DecoderFallbackBuffer FallbackBuffer { get; }
    // Summary:
    //     Converts a buffer of encoded bytes to Unicode characters and stores the result
    //     in another buffer.
    //
    // Parameters:
    //   bytes:
    //     The address of a buffer that contains the byte sequences to convert.
    //
    //   byteCount:
    //     The number of bytes in bytes to convert.
    //
    //   chars:
    //     The address of a buffer to store the converted characters.
    //
    //   charCount:
    //     The maximum number of characters in chars to use in the conversion.
    //
    //   flush:
    //     true to indicate no further data is to be converted; otherwise, false.
    //
    //   bytesUsed:
    //     When this method returns, contains the number of bytes that were produced
    //     by the conversion. This parameter is passed uninitialized.
    //
    //   charsUsed:
    //     When this method returns, contains the number of characters from chars that
    //     were used in the conversion. This parameter is passed uninitialized.
    //
    //   completed:
    //     When this method returns, contains true if all the characters specified by
    //     byteCount were converted; otherwise, false. This parameter is passed uninitialized.
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
    //     Overload:System.Text.Decoder.GetCharCount method.
    //
    //   System.Text.DecoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Decoder.Fallback
    //     is set to System.Text.DecoderExceptionFallback.

    unsafe public virtual void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed);
    //
    // Summary:
    //     Converts an array of encoded bytes to Unicode characters and stores the result
    //     in a byte array.
    //
    // Parameters:
    //   bytes:
    //     A byte array to convert.
    //
    //   byteIndex:
    //     The first element of bytes to convert.
    //
    //   byteCount:
    //     The number of elements of bytes to convert.
    //
    //   chars:
    //     An array to store the converted characters.
    //
    //   charIndex:
    //     The first element of chars in which data is stored.
    //
    //   charCount:
    //     The maximum number of elements of chars to use in the conversion.
    //
    //   flush:
    //     true to indicate that no further data is to be converted; otherwise, false.
    //
    //   bytesUsed:
    //     When this method returns, contains the number of bytes that were used in
    //     the conversion. This parameter is passed uninitialized.
    //
    //   charsUsed:
    //     When this method returns, contains the number of characters from chars that
    //     were produced by the conversion. This parameter is passed uninitialized.
    //
    //   completed:
    //     When this method returns, contains true if all the characters specified by
    //     byteCount were converted; otherwise, false. This parameter is passed uninitialized.
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
    //     Overload:System.Text.Decoder.GetCharCount method.
    //
    //   System.Text.DecoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Decoder.Fallback
    //     is set to System.Text.DecoderExceptionFallback.

    public virtual void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed);
    //
    // Summary:
    //     When overridden in a derived class, calculates the number of characters produced
    //     by decoding a sequence of bytes starting at the specified byte pointer. A
    //     parameter indicates whether to clear the internal state of the decoder after
    //     the calculation.
    //
    // Parameters:
    //   bytes:
    //     A pointer to the first byte to decode.
    //
    //   count:
    //     The number of bytes to decode.
    //
    //   flush:
    //     true to simulate clearing the internal state of the encoder after the calculation;
    //     otherwise, false.
    //
    // Returns:
    //     The number of characters produced by decoding the specified sequence of bytes
    //     and any bytes in the internal buffer.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     bytes is null (Nothing in Visual Basic .NET).
    //
    //   System.ArgumentOutOfRangeException:
    //     count is less than zero.
    //
    //   System.Text.DecoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Decoder.Fallback
    //     is set to System.Text.DecoderExceptionFallback.

    unsafe public virtual int GetCharCount(byte* bytes, int count, bool flush);
    //
    // Summary:
    //     When overridden in a derived class, calculates the number of characters produced
    //     by decoding a sequence of bytes from the specified byte array.
    //
    // Parameters:
    //   bytes:
    //     The byte array containing the sequence of bytes to decode.
    //
    //   index:
    //     The index of the first byte to decode.
    //
    //   count:
    //     The number of bytes to decode.
    //
    // Returns:
    //     The number of characters produced by decoding the specified sequence of bytes
    //     and any bytes in the internal buffer.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     bytes is null (Nothing).
    //
    //   System.ArgumentOutOfRangeException:
    //     index or count is less than zero.-or- index and count do not denote a valid
    //     range in bytes.
    //
    //   System.Text.DecoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Decoder.Fallback
    //     is set to System.Text.DecoderExceptionFallback.
    public abstract int GetCharCount(byte[] bytes, int index, int count);
    //
    // Summary:
    //     When overridden in a derived class, calculates the number of characters produced
    //     by decoding a sequence of bytes from the specified byte array. A parameter
    //     indicates whether to clear the internal state of the decoder after the calculation.
    //
    // Parameters:
    //   bytes:
    //     The byte array containing the sequence of bytes to decode.
    //
    //   index:
    //     The index of the first byte to decode.
    //
    //   count:
    //     The number of bytes to decode.
    //
    //   flush:
    //     true to simulate clearing the internal state of the encoder after the calculation;
    //     otherwise, false.
    //
    // Returns:
    //     The number of characters produced by decoding the specified sequence of bytes
    //     and any bytes in the internal buffer.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     bytes is null (Nothing).
    //
    //   System.ArgumentOutOfRangeException:
    //     index or count is less than zero.-or- index and count do not denote a valid
    //     range in bytes.
    //
    //   System.Text.DecoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Decoder.Fallback
    //     is set to System.Text.DecoderExceptionFallback.

    public virtual int GetCharCount(byte[] bytes, int index, int count, bool flush);
    //
    // Summary:
    //     When overridden in a derived class, decodes a sequence of bytes starting
    //     at the specified byte pointer and any bytes in the internal buffer into a
    //     set of characters that are stored starting at the specified character pointer.
    //     A parameter indicates whether to clear the internal state of the decoder
    //     after the conversion.
    //
    // Parameters:
    //   bytes:
    //     A pointer to the first byte to decode.
    //
    //   byteCount:
    //     The number of bytes to decode.
    //
    //   chars:
    //     A pointer to the location at which to start writing the resulting set of
    //     characters.
    //
    //   charCount:
    //     The maximum number of characters to write.
    //
    //   flush:
    //     true to clear the internal state of the decoder after the conversion; otherwise,
    //     false.
    //
    // Returns:
    //     The actual number of characters written at the location indicated by the
    //     chars parameter.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     bytes is null (Nothing).-or- chars is null (Nothing).
    //
    //   System.ArgumentOutOfRangeException:
    //     byteCount or charCount is less than zero.
    //
    //   System.ArgumentException:
    //     charCount is less than the resulting number of characters.
    //
    //   System.Text.DecoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Decoder.Fallback
    //     is set to System.Text.DecoderExceptionFallback.

    unsafe public virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush);
    //
    // Summary:
    //     When overridden in a derived class, decodes a sequence of bytes from the
    //     specified byte array and any bytes in the internal buffer into the specified
    //     character array.
    //
    // Parameters:
    //   bytes:
    //     The byte array containing the sequence of bytes to decode.
    //
    //   byteIndex:
    //     The index of the first byte to decode.
    //
    //   byteCount:
    //     The number of bytes to decode.
    //
    //   chars:
    //     The character array to contain the resulting set of characters.
    //
    //   charIndex:
    //     The index at which to start writing the resulting set of characters.
    //
    // Returns:
    //     The actual number of characters written into chars.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     bytes is null (Nothing).-or- chars is null (Nothing).
    //
    //   System.ArgumentOutOfRangeException:
    //     byteIndex or byteCount or charIndex is less than zero.-or- byteindex and
    //     byteCount do not denote a valid range in bytes.-or- charIndex is not a valid
    //     index in chars.
    //
    //   System.ArgumentException:
    //     chars does not have enough capacity from charIndex to the end of the array
    //     to accommodate the resulting characters.
    //
    //   System.Text.DecoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Decoder.Fallback
    //     is set to System.Text.DecoderExceptionFallback.
    public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);
    //
    // Summary:
    //     When overridden in a derived class, decodes a sequence of bytes from the
    //     specified byte array and any bytes in the internal buffer into the specified
    //     character array. A parameter indicates whether to clear the internal state
    //     of the decoder after the conversion.
    //
    // Parameters:
    //   bytes:
    //     The byte array containing the sequence of bytes to decode.
    //
    //   byteIndex:
    //     The index of the first byte to decode.
    //
    //   byteCount:
    //     The number of bytes to decode.
    //
    //   chars:
    //     The character array to contain the resulting set of characters.
    //
    //   charIndex:
    //     The index at which to start writing the resulting set of characters.
    //
    //   flush:
    //     true to clear the internal state of the decoder after the conversion; otherwise,
    //     false.
    //
    // Returns:
    //     The actual number of characters written into the chars parameter.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     bytes is null (Nothing).-or- chars is null (Nothing).
    //
    //   System.ArgumentOutOfRangeException:
    //     byteIndex or byteCount or charIndex is less than zero.-or- byteindex and
    //     byteCount do not denote a valid range in bytes.-or- charIndex is not a valid
    //     index in chars.
    //
    //   System.ArgumentException:
    //     chars does not have enough capacity from charIndex to the end of the array
    //     to accommodate the resulting characters.
    //
    //   System.Text.DecoderFallbackException:
    //     A fallback occurred (see Understanding Encodings for fuller explanation)-and-System.Text.Decoder.Fallback
    //     is set to System.Text.DecoderExceptionFallback.
    public virtual int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush);
    //
    // Summary:
    //     When overridden in a derived class, sets the decoder back to its initial
    //     state.

    public virtual void Reset();
#endif

                                }
}
