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

namespace System.Text
{

  public class StringBuilder
  {

    [System.Runtime.CompilerServices.IndexerName("Chars")]
    public Char this[int index]
    {
      get
      {
        Contract.Requires(0 <= index);
        Contract.Requires(index < this.Length);
        return default(Char);
      }
      set
      {
        Contract.Requires(0 <= index);
        Contract.Requires(index < this.Length);
      }
    }

#if !SILVERLIGHT
    public int MaxCapacity
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= this.Length);
        return default(int);
      }
    }
#endif

    public int Length
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Ensures(Capacity >= value);

      }
    }

    public int Capacity
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() >= Length);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= Length);
      }
    }

    public StringBuilder Replace(Char oldChar, Char newChar, int startIndex, int count)
    {
      Contract.Requires(count >= 0);
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      return default(StringBuilder);
    }
    public StringBuilder Replace(Char oldChar, Char newChar)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      return default(StringBuilder);
    }
    [Pure]
    public bool Equals(StringBuilder sb)
    {
      return default(bool);
    }
    public StringBuilder Replace(string arg0, string arg1, int arg2, int arg3)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      return default(StringBuilder);
    }

    public StringBuilder Replace(string oldValue, string newValue)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      return default(StringBuilder);
    }

    public StringBuilder AppendFormat(IFormatProvider provider, string format, Object[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);

      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));

      return default(StringBuilder);
    }

    public StringBuilder AppendFormat(string format, Object[] args)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

#if !SILVERLIGHT
    public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder AppendFormat(string format, object arg0, object arg1)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder AppendFormat(string format, object arg0)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }
#endif

#if !SILVERLIGHT
    public StringBuilder Insert(int index, object value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, UInt64 value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, UInt32 value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, UInt16 value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, Decimal value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, double value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, Single value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, Int64 value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, int value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }
#endif
    public StringBuilder Insert(int arg0, Char[] arg1, int arg2, int count)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length == Contract.OldValue(Length) + count);
      return default(StringBuilder);
    }
    public StringBuilder Insert(int index, Char[] value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length == Contract.OldValue(Length) + value.Length);
      return default(StringBuilder);
    }
#if !SILVERLIGHT
    public StringBuilder Insert(int index, Char value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length == Contract.OldValue(Length) + 1);
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, Int16 value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, byte value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, SByte value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, bool value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }
#endif

    public StringBuilder Insert(int index, string value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(Char[] value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      Contract.Ensures(Length == Contract.OldValue(Length) + value.Length);

      return default(StringBuilder);
    }

    public StringBuilder Append(object value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(UInt64 value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(UInt32 value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(UInt16 value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

#if !SILVERLIGHT
    public StringBuilder Append(Decimal value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }
#endif

    public StringBuilder Append(double value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(Single value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(Int64 value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(int value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
            
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(Int16 value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(Char value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      
      Contract.Ensures(Length == Contract.OldValue(Length) + 1);
      return default(StringBuilder);
    }

    public StringBuilder Append(byte value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(SByte value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Append(bool value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      
      Contract.Ensures(Length >= Contract.OldValue(Length));
      return default(StringBuilder);
    }

    public StringBuilder Remove(int startIndex, int length)
    {
      Contract.Requires(0 <= startIndex);
      Contract.Requires(0 <= length);
      Contract.Requires(startIndex + length <= Length);
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      //modifies this.*;
      //Contract.Ensures(result == this);
      //Contract.Ensures(Length == old(Length) - length);

      return default(StringBuilder);
    }

    public StringBuilder Insert(int index, string value, int count)
    {
      Contract.Requires(value != null || (index == 0 && count == 0));
      Contract.Requires(0 <= index);
      Contract.Requires(index < Length);
      Contract.Requires(1 <= count);
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);


      //modifies this.*;
      //Contract.Ensures(result == this);
      //Contract.Ensures(Length == old(Length) + count * value.Length);

      return default(StringBuilder);
    }

    public StringBuilder Append(string value, int startIndex, int count)
    {
      Contract.Requires(value != null || (startIndex == 0 && count == 0));
      Contract.Requires(0 <= count);
      Contract.Requires(0 <= startIndex);
      Contract.Requires(startIndex <= (value.Length - count));
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      Contract.Ensures(Length == Contract.OldValue(Length) + count);
      //modifies this.*;
      //Contract.Ensures(result == this);
      //Contract.Ensures(Length == old(Length) + count);

      return default(StringBuilder);
    }

    public StringBuilder Append(string value)
    {
      //modifies this.*;
      //Contract.Ensures(result == this);
      // Contract.Ensures(!value == null || Length == Contract.OldValue<int>(Length)); //[t-cyrils] what to do here?
      //Contract.Ensures(!value != null || Length == Contract.OldValue<int>(Length) + value.Length);

      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      Contract.Ensures(value == null && Length == Contract.OldValue(Length) || value != null && Length == Contract.OldValue(Length) + value.Length);
      return default(StringBuilder);
    }

    public StringBuilder Append(Char[] value, int startIndex, int charCount)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(charCount >= 0);
      Contract.Requires(charCount <= (value.Length - startIndex));

      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      return default(StringBuilder);
    }

    public StringBuilder Append(Char value, int repeatCount)
    {
      Contract.Requires(repeatCount >= 0);

      Contract.Ensures(Contract.Result<StringBuilder>() == this);
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      return default(StringBuilder);
    }

    //
    // Summary:
    //     Appends the default line terminator to the end of the current System.Text.StringBuilder
    //     object.
    //
    // Returns:
    //     A reference to this instance after the append operation has completed.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
    public StringBuilder AppendLine()
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(Length > Contract.OldValue(Length));
      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      return default(StringBuilder);
    }
    //
    // Summary:
    //     Appends a copy of the specified string and the default line terminator to
    //     the end of the current System.Text.StringBuilder object.
    //
    // Parameters:
    //   value:
    //     The System.String to append.
    //
    // Returns:
    //     A reference to this instance after the append operation has completed.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     Enlarging the value of this instance would exceed System.Text.StringBuilder.MaxCapacity.
    public StringBuilder AppendLine(string value)
    {
      Contract.Ensures(Contract.Result<StringBuilder>() != null);
      Contract.Ensures(value == null && Length > Contract.OldValue(Length) ||
                       value != null && Length > Contract.OldValue(Length) + value.Length);
      Contract.Ensures(Contract.Result<StringBuilder>() == this);

      return default(StringBuilder);
    }

    [Pure]
    public string ToString(int startIndex, int length)
    {
      Contract.Requires(0 <= startIndex);
      Contract.Requires(0 <= length);
      Contract.Requires(startIndex + length <= Length);
      Contract.Ensures(Contract.Result<string>().Length == length);

      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

    [Pure]
    public override string ToString()
    {
      Contract.Ensures(Contract.Result<string>().Length == this.Length);

      return base.ToString();
    }

    public int EnsureCapacity(int capacity)
    {
      Contract.Requires(capacity >= 0);
      Contract.Ensures(Capacity >= capacity);

      return default(int);
    }

    public StringBuilder(int capacity, int maxCapacity)
    {
      Contract.Requires(capacity <= maxCapacity);
      Contract.Requires(maxCapacity >= 1);
      Contract.Requires(capacity >= 0);
      Contract.Ensures(Length == 0);
    }

    public StringBuilder(string value, int startIndex, int length, int capacity)
    {
      Contract.Requires(capacity >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(startIndex >= 0);
      Contract.Requires(value == null || startIndex + length <= value.Length);
      Contract.Ensures(this.Length == ((value == null) ? 0 : length));
    }

    public StringBuilder(string value, int capacity)
    {
      Contract.Requires(capacity >= 0);
      Contract.Ensures(this.Length == ((value == null) ? 0 : value.Length));
    }

    public StringBuilder(string value)
    {
      Contract.Ensures(this.Length == ((value == null) ? 0 : value.Length));
    }

    public StringBuilder(int capacity)
    {
      Contract.Requires(capacity >= 0);
      Contract.Ensures(Length == 0);
    }
    public StringBuilder()
    {
      Contract.Ensures(Length == 0);
    }
  }
}
