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

using System.Diagnostics.Contracts;

namespace System {
  // Summary:
  //     A platform-specific type that is used to represent a pointer or a handle.
  public unsafe struct IntPtr {
    // Summary:
    //     A read-only field that represents a pointer or handle that has been initialized
    //     to zero.
    public static readonly IntPtr Zero;

    //
    // Summary:
    //     Initializes a new instance of System.IntPtr using the specified 32-bit pointer
    //     or handle.
    //
    // Parameters:
    //   value:
    //     A pointer or handle contained in a 32-bit signed integer.
    public IntPtr(int value) {
    }
    //
    // Summary:
    //     Initializes a new instance of System.IntPtr using the specified 64-bit pointer.
    //
    // Parameters:
    //   value:
    //     A pointer or handle contained in a 64-bit signed integer.
    //
    // Exceptions:
    //   System.OverflowException:
    //     On a 32-bit platform, value is too large or too small to represent as an
    //     System.IntPtr.
    public IntPtr(long value) {
    }
    //
    // Summary:
    //     Initializes a new instance of System.IntPtr using the specified pointer to
    //     an unspecified type.
    //
    // Parameters:
    //   value:
    //     A pointer to an unspecified type.
    public IntPtr(void* value)
    {
    }

    // Summary:
    //     Determines whether two specified instances of System.IntPtr are not equal.
    //
    // Parameters:
    //   value1:
    //     An System.IntPtr.
    //
    //   value2:
    //     An System.IntPtr.
    //
    // Returns:
    //     true if value1 does not equal value2; otherwise, false.
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static bool operator!=(IntPtr value1, IntPtr value2) {
      return default(bool);
    }
    //
    // Summary:
    //     Determines whether two specified instances of System.IntPtr are equal.
    //
    // Parameters:
    //   value1:
    //     An System.IntPtr.
    //
    //   value2:
    //     An System.IntPtr.
    //
    // Returns:
    //     true if value1 equals value2; otherwise, false.
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static bool operator==(IntPtr value1, IntPtr value2) {
    //
    // Summary:
    //     Converts the value of a 32-bit signed integer to an System.IntPtr.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    //
    // Returns:
    //     A new instance of System.IntPtr initialized to value.
      return default(bool);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator IntPtr(int value) {
      return default(IntPtr);
    }
    //
    // Summary:
    //     Converts the value of the specified System.IntPtr to a 64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An System.IntPtr.
    //
    // Returns:
    //     The contents of value.
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator long(IntPtr value) {
      return default(long);
    }
    //
    // Summary:
    //     Converts the value of the specified System.IntPtr to a pointer to an unspecified
    //     type.
    //
    // Parameters:
    //   value:
    //     An System.IntPtr.
    //
    // Returns:
    //     The contents of value.
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator void*(IntPtr value) {
        return default(void*);
    }
    //
    // Summary:
    //     Converts the value of the specified System.IntPtr to a 32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     An System.IntPtr.
    //
    // Returns:
    //     The contents of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     On a 64-bit platform, the value of value is too large to represent as a 32-bit
    //     signed integer.
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator int(IntPtr value) {
        return default(int);
    }
    //
    // Summary:
    //     Converts the value of a 64-bit signed integer to an System.IntPtr.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    //
    // Returns:
    //     A new instance of System.IntPtr initialized to value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     On a 32-bit platform, value is too large to represent as an System.IntPtr.
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator IntPtr(long value) {
      return default(IntPtr);
    }
    //
    // Summary:
    //     Converts the specified pointer to an unspecified type to an System.IntPtr.
    //
    // Parameters:
    //   value:
    //     A pointer to an unspecified type.
    //
    // Returns:
    //     A new instance of System.IntPtr initialized to value.
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator IntPtr(void* value) {

    // Summary:
    //     Gets the size of this instance.
    //
    // Returns:
    //     The size of a pointer or handle on this platform, measured in bytes. The
    //     value of this property is 4 on a 32-bit platform, and 8 on a 64-bit platform.
        return default(IntPtr);
    }

    public static int Size { get { Contract.Ensures(Contract.Result<int>() == 4 || Contract.Result<int>() == 8); return default(int); } }

    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public int ToInt32() {
    //
    // Summary:
    //     Converts the value of this instance to a 64-bit signed integer.
    //
    // Returns:
    //     A 64-bit signed integer equal to the value of this instance.
      return default(int);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public long ToInt64() {
    //
    // Summary:
    //     Converts the value of this instance to a pointer to an unspecified type.
    //
    // Returns:
    //     A pointer to System.Void; that is, a pointer to memory containing data of
    //     an unspecified type.
      return default(long);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public void* ToPointer() {
    //
    // Summary:
    //     Converts the numeric value of the current System.IntPtr object to its equivalent
    //     string representation.
    //
    // Returns:
    //     The string representation of the value of this instance.
      return default(void*);
    }

#if !SILVERLIGHT
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public string ToString(string format) {
      return default(string);
    }
#endif
  }
}
