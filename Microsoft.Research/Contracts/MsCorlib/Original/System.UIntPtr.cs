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

using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System {
  // Summary:
  //     A platform-specific type that is used to represent a pointer or a handle.
  public unsafe struct UIntPtr {
    // Summary:
    //     A read-only field that represents a pointer or handle that has been initialized
    //     to zero.
    public static readonly UIntPtr Zero;

    //
    // Summary:
    //     Initializes a new instance of the System.UIntPtr structure using the specified
    //     32-bit pointer or handle.
    //
    // Parameters:
    //   value:
    //     A pointer or handle contained in a 32-bit unsigned integer.
    public UIntPtr(uint value) {
    //
    // Summary:
    //     Initializes a new instance of System.UIntPtr using the specified 64-bit pointer
    //     or handle.
    //
    // Parameters:
    //   value:
    //     A pointer or handle contained in a 64-bit unsigned integer.
    //
    // Exceptions:
    //   System.OverflowException:
    //     On a 32-bit platform, value is too large to represent as an System.UIntPtr.
      return default(UIntPtr(uint);
    }
    public UIntPtr(ulong value) {
    //
    // Summary:
    //     Initializes a new instance of System.UIntPtr using the specified pointer
    //     to an unspecified type.
    //
    // Parameters:
    //   value:
    //     A pointer to an unspecified type.
      return default(UIntPtr(ulong);
    }
    public UIntPtr(void* value) {

    // Summary:
    //     Determines whether two specified instances of System.UIntPtr are not equal.
    //
    // Parameters:
    //   value1:
    //     A System.UIntPtr.
    //
    //   value2:
    //     A System.UIntPtr.
    //
    // Returns:
    //     true if value1 does not equal value2; otherwise, false.
      return default(UIntPtr(void*);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static bool operator!=(UIntPtr value1, UIntPtr value2) {
    //
    // Summary:
    //     Determines whether two specified instances of System.UIntPtr are equal.
    //
    // Parameters:
    //   value1:
    //     A System.UIntPtr.
    //
    //   value2:
    //     A System.UIntPtr.
    //
    // Returns:
    //     true if value1 equals value2; otherwise, false.
      return default(bool);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static bool operator==(UIntPtr value1, UIntPtr value2) {
    //
    // Summary:
    //     Converts the value of a 32-bit unsigned integer to an System.UIntPtr.
    //
    // Parameters:
    //   value:
    //     A 32-bit unsigned integer.
    //
    // Returns:
    //     A new instance of System.UIntPtr initialized to value.
      return default(bool);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator UIntPtr(uint value) {
    //
    // Summary:
    //     Converts the value of the specified System.UIntPtr to a 32-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A System.UIntPtr.
    //
    // Returns:
    //     The contents of value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     On a 64-bit platform, the value of value is too large to represent as a 32-bit
    //     unsigned integer.
      return default(explicit);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator uint(UIntPtr value) {
    //
    // Summary:
    //     Converts the value of the specified System.UIntPtr to a 64-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     A System.UIntPtr.
    //
    // Returns:
    //     The contents of value.
      return default(explicit);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator ulong(UIntPtr value) {
    //
    // Summary:
    //     Converts the value of the specified System.UIntPtr to a pointer to an unspecified
    //     type.
    //
    // Parameters:
    //   value:
    //     A System.UIntPtr.
    //
    // Returns:
    //     The contents of value.
      return default(explicit);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator void*(UIntPtr value) {
    //
    // Summary:
    //     Converts the value of a 64-bit unsigned integer to an System.UIntPtr.
    //
    // Parameters:
    //   value:
    //     A 64-bit unsigned integer.
    //
    // Returns:
    //     A new instance of System.UIntPtr initialized to value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     On a 32-bit platform, value is too large to represent as an System.UIntPtr.
      return default(explicit);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator UIntPtr(ulong value) {
    //
    // Summary:
    //     Converts the specified pointer to an unspecified type to a System.UIntPtr.
    //
    // Parameters:
    //   value:
    //     A pointer to an unspecified type.
    //
    // Returns:
    //     A new instance of System.UIntPtr initialized to value.
      return default(explicit);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator UIntPtr(void* value) {

    // Summary:
    //     Gets the size of this instance.
    //
    // Returns:
    //     The size of a pointer or handle on this platform, measured in bytes. The
    //     value of this property is 4 on a 32-bit platform, and 8 on a 64-bit platform.
      return default(explicit);
    }
    public static int Size { get; }

    // Summary:
    //     Returns a value indicating whether this instance is equal to a specified
    //     object.
    //
    // Parameters:
    //   obj:
    //     An object to compare with this instance or null.
    //
    // Returns:
    //     true if obj is an instance of System.UIntPtr and equals the value of this
    //     instance; otherwise, false.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public override bool Equals(object obj) {
    //
    // Summary:
    //     Returns the hash code for this instance.
    //
    // Returns:
    //     A 32-bit signed integer hash code.
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public override int GetHashCode() {
    //
    // Summary:
    //     Converts the value of this instance to a pointer to an unspecified type.
    //
    // Returns:
    //     A pointer to System.Void; that is, a pointer to memory containing data of
    //     an unspecified type.
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public void* ToPointer() {
    //
    // Summary:
    //     Converts the numeric value of this instance to its equivalent string representation.
    //
    // Returns:
    //     The string representation of the value of this instance.
      return default(void*);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public override string ToString() {
    //
    // Summary:
    //     Converts the value of this instance to a 32-bit unsigned integer.
    //
    // Returns:
    //     A 32-bit unsigned integer equal to the value of this instance.
    //
    // Exceptions:
    //   System.OverflowException:
    //     On a 64-bit platform, the value of this instance is too large to represent
    //     as a 32-bit unsigned integer.
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public uint ToUInt32() {
    //
    // Summary:
    //     Converts the value of this instance to a 64-bit unsigned integer.
    //
    // Returns:
    //     A 64-bit unsigned integer equal to the value of this instance.
      return default(uint);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public ulong ToUInt64() {
      return default(ulong);
    }
  }
}
