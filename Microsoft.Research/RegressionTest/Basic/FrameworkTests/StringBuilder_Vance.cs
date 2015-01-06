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


#if false

#define CONTRACTS_FULL


// F: This is the example of the StringBuilder class from Vance. 
//    Please note that I changed the name of the file so to recognize that it is not the "standard" StringBuilder, but an optimization
//
//    !!! We want to be able to prove all of it !!!!
//

// Changes:
// 1. All BCLDebug.Assert -> Contracts.Assert
// 2. Added preconditions
// 3. Split complex conditions requiring shortcuts

using System.Diagnostics.Contracts;


// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  StringBuilder
**
**
** Purpose: implementation of the StringBuilder
** class.  
===========================================================*/

namespace MyStringBuilder
{
  using System;
  using System.Runtime.Serialization;
  using System.Runtime.Versioning;
  using System.Globalization;

  class BCLDebug
  {
    [System.Diagnostics.Contracts.ContractVerification(false)]
    public static void Assert(bool val, string message)
    {
      System.Diagnostics.Debug.Assert(val, message);
    }
  }
  // TODO remove when in mscorlib
  class Environment
  {
    public static string GetResourceString(string s) { return s; }
    public static string GetResourceString(string s, string s1) { return s; }
    public static string NewLine { get { return "\r\n"; } }
    public static int ExitCode { set { System.Environment.ExitCode = value; } }
  };

  // This class represents a mutable string.  It is convenient for situations in
  // which it is desirable to modify a string, perhaps by removing, replacing, or 
  // inserting characters, without creating a new String subsequent to
  // each modification. 
  // 
  // The methods contained within this class do not return a new StringBuilder
  // object unless specified otherwise.  This class may be used in conjunction with the String
  // class to carry out modifications upon strings.
  // 
  // When passing null into a constructor in VJ and VC, the null
  // should be explicitly type cast.
  // For Example:
  // StringBuilder sb1 = new StringBuilder((StringBuilder)null);
  // StringBuilder sb2 = new StringBuilder((String)null);
  // Console.WriteLine(sb1);
  // Console.WriteLine(sb2);
  // 
  [System.Runtime.InteropServices.ComVisible(true)]
  [Serializable()]
  public sealed class StringBuilder : ISerializable
  {
    private const int DefaultCapacity = 16;
    private const String CapacityField = "Capacity";
    private const String MaxCapacityField = "m_MaxCapacity";
    private const String StringValueField = "m_StringValue";

    // We want to keep chunk arrays out of large object heap (< 85K bytes ~ 40K chars) to be sure.
    // Making the maximum chunk size big means less allocation code called, but also more waste
    // in unused characters and slower inserts / replaces (since you do need to slide characters over
    // within a buffer).  
    // TODO we need to tune this, I suspect a number about 8000 is probably optimal.  
    private const int MaxChunkSize = 8000;

    //
    //
    //CONSTRUCTORS
    //
    //

    // Creates a new empty string builder (i.e., it represents String.Empty)
    // with the default capacity (16 characters).
    public StringBuilder()
      : this(DefaultCapacity)
    {
    }

    // Create a new empty string builder (i.e., it represents String.Empty)
    // with the specified capacity.
    public StringBuilder(int capacity)
    {
      //if (capacity < 0)
      //{
      //  throw new ArgumentOutOfRangeException("capacity",
      //                                        String.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_MustBePositive"), "capacity"));
      //}
      //Contract.EndContractBlock();

      Contract.Requires(capacity > 0);

      m_ChunkChars = new char[capacity];
      m_MaxCapacity = int.MaxValue;
    }

    // Creates a new string builder from the specified string.  If value
    // is a null String (i.e., if it represents String.NullString)
    // then the new string builder will also be null (i.e., it will also represent
    //  String.NullString).
    // 
    public StringBuilder(String value)
      : this(value, DefaultCapacity)
    {
    }

    // Creates a new string builder from the specified string with the specified 
    // capacity.  If value is a null String (i.e., if it represents 
    // String.NullString) then the new string builder will also be null 
    // (i.e., it will also represent String.NullString).
    // The maximum number of characters this string may contain is set by capacity.
    // 
    public StringBuilder(String value, int capacity)
      : this(value, 0, ((value != null) ? value.Length : 0), capacity)
    {
    }

    // Creates a new string builder from the specifed substring with the specified
    // capacity.  The maximum number of characters is set by capacity.
    // 

    unsafe public StringBuilder(String value, int startIndex, int length, int capacity)
    {
      if (capacity < 0)
      {
        throw new ArgumentOutOfRangeException("capacity"); //,
                                              // String.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_MustBePositive"), "capacity"));
      }
      if (length < 0)
      {
        throw new ArgumentOutOfRangeException("length"); //,
                                              //String.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegNum"), "length"));
      }
      if (startIndex < 0)
      {
        throw new ArgumentOutOfRangeException("startIndex"); //, Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      }
      if (value == null)
      {
        value = String.Empty;
      }
      
      if (startIndex > value.Length - length)
      { // f: Requires startIndex + length < value.Length
        throw new ArgumentOutOfRangeException("length"); // , Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
      }

      Contract.EndContractBlock();

      if (capacity == 0)
      {
        capacity = DefaultCapacity;
      }
      if (capacity < length)
        capacity = length;

      m_ChunkChars = new char[capacity];
      m_ChunkLength = length;
      m_MaxCapacity = int.MaxValue;

      fixed (char* sourcePtr = value)
        ThreadSafeCopy(sourcePtr + startIndex, m_ChunkChars, 0, length);
    }

    // Creates an empty StringBuilder with a minimum capacity of capacity
    // and a maximum capacity of maxCapacity.
    public StringBuilder(int capacity, int maxCapacity)
    {
      if (capacity > maxCapacity)
      {
        throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
      }
      if (maxCapacity < 1)
      {
        throw new ArgumentOutOfRangeException("maxCapacity", Environment.GetResourceString("ArgumentOutOfRange_SmallMaxCapacity"));
      }

      if (capacity < 0)
      {
        throw new ArgumentOutOfRangeException("capacity",
                                              String.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_MustBePositive"), "capacity"));
      }
      if (capacity == 0)
      {
        capacity = Math.Min(DefaultCapacity, maxCapacity);
      }

      m_MaxCapacity = maxCapacity;
      m_ChunkChars = new char[capacity];
    }

    private StringBuilder(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException("info");
      Contract.EndContractBlock();

      int persistedCapacity = 0;
      string persistedString = "";
      int persistedMaxCapacity = Int32.MaxValue;
      bool capacityPresent = false;

      // Get the data
      SerializationInfoEnumerator enumerator = info.GetEnumerator();

      Contract.Assume(enumerator != null);

      while (enumerator.MoveNext())
      {
        switch (enumerator.Name)
        {
          case MaxCapacityField:
            persistedMaxCapacity = info.GetInt32(MaxCapacityField);
            break;
          case StringValueField:
            persistedString = info.GetString(StringValueField);
            Contract.Assume(persistedString != null);
            break;
          case CapacityField:
            persistedCapacity = info.GetInt32(CapacityField);
            capacityPresent = true;
            break;
          default:
            // Note: deliberately ignore "m_currentThread" which earlier
            // verisions incorrectly persisted.
            // Ignore other fields for forward compatability.
            break;
        }

      }

      if (persistedMaxCapacity < 1 || persistedString.Length > persistedMaxCapacity)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderMaxCapacity"));
      }

      if (!capacityPresent)
      {
        // StringBuilder in V1.X did not persist the Capacity, so this is a valid legacy code path.
        persistedCapacity = DefaultCapacity;
        if (persistedCapacity < persistedString.Length)
        {
          persistedCapacity = persistedString.Length;
        }
        if (persistedCapacity > persistedMaxCapacity)
        {
          persistedCapacity = persistedMaxCapacity;
        }
      }
 
      // We replace those tests with a serie of others, as Clousot has problems with Shorcuts now
      // BeginOld
      //if (persistedCapacity < 0 || persistedCapacity < persistedString.Length || persistedCapacity > persistedMaxCapacity)
      //{
      //  throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderCapacity"));
      //}
      // EndOld

      // BeginNew
      if (persistedCapacity < 0)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderCapacity"));
      }

      if (persistedCapacity < persistedString.Length)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderCapacity"));
      }

      if (persistedCapacity > persistedMaxCapacity)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_StringBuilderCapacity"));
      }
      // EndNEw

      m_MaxCapacity = persistedMaxCapacity;
      m_ChunkChars = new char[persistedCapacity];
      persistedString.CopyTo(0, m_ChunkChars, 0, persistedString.Length);
      m_ChunkLength = persistedString.Length;
     
      VerifyClassInvariant();

      // F: the candidate invariant m_ChunkChars.Length > 0 does not hold here
    }

    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
      {
        throw new ArgumentNullException("info");
      }
      Contract.EndContractBlock();


      VerifyClassInvariant();

      info.AddValue(MaxCapacityField, m_MaxCapacity);
      info.AddValue(CapacityField, Capacity);
      info.AddValue(StringValueField, ToString());
    }

    public int Capacity
    {
      get 
      {
        Contract.Ensures(Contract.Result<int>() == m_ChunkChars.Length + m_ChunkOffset);  // f: this should be inferred !!!

        return m_ChunkChars.Length + m_ChunkOffset; 
      }
      set
      {
        //if (value < 0)
        //{
        //  throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
        //}
        //if (value < Length)
        //{
        //  throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        //}

        //if (value > MaxCapacity)
        //{
        //  throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Capacity"));
        //}
        //Contract.EndContractBlock();

        Contract.Requires(value >= 0);
        Contract.Requires(value >= Length);
        Contract.Requires(value <= MaxCapacity);

        if (Capacity != value)
        {
          int newLen = value - m_ChunkOffset;
          char[] newArray = new char[newLen];   // f: imprecision in bounds
          Array.Copy(m_ChunkChars, newArray, m_ChunkLength);
          m_ChunkChars = newArray;
        }
      }
    }

    public int MaxCapacity
    {
      get { return m_MaxCapacity; }
    }

    // Read-Only Property 
    // Ensures that the capacity of this string builder is at least the specified value.  
    // If capacity is greater than the capacity of this string builder, then the capacity
    // is set to capacity; otherwise the capacity is unchanged.
    // 
    public int EnsureCapacity(int capacity)
    {
      //if (capacity < 0)
      //{
      //  throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
      //}
      //Contract.EndContractBlock();

      Contract.Requires(capacity >= 0);           
      Contract.Requires(capacity < MaxCapacity);    // f: I've added this one to satisfy the precondition for Capacity

      if (Capacity < capacity)
        Capacity = capacity;
      
      return Capacity;
    }



    // Converts a substring of this string builder to a String.
    unsafe public String ToString(int startIndex, int length)
    {
      int currentLength = this.Length;
      if (startIndex < 0)
      {
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      }
      if (startIndex > currentLength)
      {
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndexLargerThanLength"));
      }
      if (length < 0)
      {
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      }
      if (startIndex > (currentLength - length))
      {
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_IndexLength"));
      }

      VerifyClassInvariant();

      StringBuilder chunk = this;
      int sourceEndIndex = startIndex + length;

      // string ret = string.FastAllocateString(Length);
      char[] ret = new char[length];
      if (length > 0)
      {
        int curDestIndex = length;
        fixed (char* destinationPtr = ret)
        {

          while (curDestIndex > 0)
          {
            int chunkEndIndex = sourceEndIndex - chunk.m_ChunkOffset;
            if (chunkEndIndex >= 0)
            {
              if (chunkEndIndex > chunk.m_ChunkLength)
                chunkEndIndex = chunk.m_ChunkLength;

              int countLeft = curDestIndex;
              int chunkCount = countLeft;
              int chunkStartIndex = chunkEndIndex - countLeft;
              if (chunkStartIndex < 0)
              {
                chunkCount += chunkStartIndex;
                chunkStartIndex = 0;
              }
              curDestIndex -= chunkCount;

              if (chunkCount > 0)
              {
                // work off of local variables so that they are stable even in the presence of races (hackers might do this)
                char[] sourceArray = chunk.m_ChunkChars;

                // Check that we will not overrun our boundaries. 
                if ((uint)(chunkCount + curDestIndex) <= length)
                // F: I've split the condition in two
                {
                  if ((uint)(chunkCount + chunkStartIndex) <= (uint)sourceArray.Length)
                  {
                    fixed (char* sourcePtr = &sourceArray[chunkStartIndex])
                      CopyChars(destinationPtr + curDestIndex, sourcePtr, chunkCount);
                  }
                }
                else
                {
                  Contract.Assert(false, "Thread safety check failed");
                  throw new Exception("Thread safety exception");
                }
              }
            }
            chunk = chunk.m_ChunkPrevious;
          }
        }
      }

      return new string(ret);
    }


    // Sets the length of the String in this buffer.  If length is less than the current
    // instance, the StringBuilder is truncated.  If length is greater than the current 
    // instance, nulls are appended.  The capacity is adjusted to be the same as the length.

    public int Length
    {
      get
      {

        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() == m_ChunkOffset + m_ChunkLength);    // f: this should be inferred by Clousot

        return m_ChunkOffset + m_ChunkLength;
      }
      set
      {
        if (value == 0)
        {
          m_ChunkPrevious = null;
          m_ChunkLength = 0;
          m_ChunkOffset = 0;
          return;
        }
        if (value < 0)
        {
          throw new ArgumentOutOfRangeException("newlength", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
        }
        if (value > MaxCapacity)
        {
          throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
        }

        int delta = value - Length;
        if (delta > 0)
        {
          Append('\0', delta);        // We could improve on this, but who does this anyway?
        }
        else
        {
          StringBuilder chunk = FindChunkForIndex(value);
          if (chunk != this)
          {
            m_ChunkChars = chunk.m_ChunkChars;
            m_ChunkPrevious = chunk.m_ChunkPrevious;
            m_ChunkOffset = chunk.m_ChunkOffset;
          }
          m_ChunkLength = value - chunk.m_ChunkOffset;
          VerifyClassInvariant();
        }
      }
    }

    [System.Runtime.CompilerServices.IndexerName("Chars")]
    public char this[int index]
    {
      // TODO: we throw ArgumentOutOfRange on Set but IndexOutOfRange on Get, but that seems to be
      // what we did before.  
      get
      {
        StringBuilder chunk = this;
        for (; ; )
        {
          int indexInBlock = index - chunk.m_ChunkOffset;
          if (indexInBlock >= 0)
          {
            if (indexInBlock >= chunk.m_ChunkLength)
              throw new IndexOutOfRangeException();
            
            return chunk.m_ChunkChars[indexInBlock];    // f: Imprecision of the analysis: lower and upper bound
          }
          chunk = chunk.m_ChunkPrevious;
          if (chunk == null)
            throw new IndexOutOfRangeException();
        }
      }
      set
      {
        StringBuilder chunk = this;
        for (; ; )
        {
          int indexInBlock = index - chunk.m_ChunkOffset;
          if (indexInBlock >= 0)
          {
            if (indexInBlock >= chunk.m_ChunkLength)
              throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));

            chunk.m_ChunkChars[indexInBlock] = value;   // f: imprecision in the analysis (lower and upper bound)

            return;
          }
          chunk = chunk.m_ChunkPrevious;
          if (chunk == null)
            throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
        }
      }
    }

    // Appends a character at the end of this string builder. The capacity is adjusted as needed.
    public StringBuilder Append(char value, int repeatCount)
    {
      //if (repeatCount < 0)
      //  throw new ArgumentOutOfRangeException("repeatCount", Environment.GetResourceString("ArgumentOutOfRange_NegativeCount"));
      //Contract.EndContractBlock();

      Contract.Requires(repeatCount >= 0);

      int idx = m_ChunkLength;
      while (repeatCount > 0)
      {
        if (idx < m_ChunkChars.Length)
        {
          m_ChunkChars[idx++] = value;
          --repeatCount;
        }
        else
        {
          m_ChunkLength = idx;
          ExpandByABlock(repeatCount);

          Contract.Assert(m_ChunkLength == 0, "Expand should create a new block");
          idx = 0;
        }

      }
      m_ChunkLength = idx;
      VerifyClassInvariant();
      return this;
    }

    // Appends an array of characters at the end of this string builder. The capacity is adjusted as needed. 
    unsafe public StringBuilder Append(char[] value, int startIndex, int charCount)
    {
      if (value == null)
      {
        if (startIndex == 0 && charCount == 0)
        {
          return this;
        }
        throw new ArgumentNullException("value");
      }
      if (charCount == 0)
      {
        return this;
      }
      if (startIndex < 0)
      {
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      }
      if (charCount < 0)
      {
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      }
      if (charCount > value.Length - startIndex)
      {
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }

      fixed (char* valueChars = &value[startIndex])
        Append(valueChars, charCount);
      return this;
    }


    // Appends a copy of this string at the end of this string builder.
    unsafe public StringBuilder Append(String value)
    {
      if (value != null)
      {
        // This is a hand specialization of the 'AppendHelper' code below. 
        // We could have just called AppendHelper.  
        char[] chunkChars = m_ChunkChars;
        int chunkLength = m_ChunkLength;
        int valueLen = value.Length;
        int newCurrentIndex = chunkLength + valueLen;
        if (newCurrentIndex < chunkChars.Length)    // Use strictly < to avoid issue if count == 0, newIndex == length
        {
          // TODO see if this special casing is a good idea.  
          if (valueLen <= 2)
          {
            if (valueLen > 0)
              chunkChars[chunkLength] = value[0];
            if (valueLen > 1)
              chunkChars[chunkLength + 1] = value[1];
          }
          else
          {
            fixed (char* valuePtr = value)
            fixed (char* destPtr = &chunkChars[chunkLength])
              CopyChars(destPtr, valuePtr, valueLen);
          }
          m_ChunkLength = newCurrentIndex;
        }
        else
          AppendHelper(value);
      }
      return this;
    }




    // We put this fixed in its own helper to avoid the cost zero initing valueChars in the
    // case we don't actually us it.  
    unsafe private void AppendHelper(string value)
    {

      Contract.Requires(value != null);


      fixed (char* valueChars = value)
        Append(valueChars, value.Length);
    }

    // Appends a copy of the characters in value from startIndex to startIndex +
    // count at the end of this string builder.
    unsafe public StringBuilder Append(String value, int startIndex, int count)
    {
      //If the value being added is null, eat the null
      //and return.
      if (value == null)
      {
        if (startIndex == 0 && count == 0)
        {
          return this;
        }
        throw new ArgumentNullException("value");
      }

      if (count <= 0)
      {
        if (count == 0)
        {
          return this;
        }
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      }

      //if (startIndex < 0 || (startIndex > value.Length - count))
      //{
      //  throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      //}
      
      if (startIndex < 0)
      {
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
      if ((startIndex > value.Length - count))
      {
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }

      fixed (char* valueChars = value)
        Append(valueChars + startIndex, count);
      return this;
    }

    [System.Runtime.InteropServices.ComVisible(false)]
    public StringBuilder AppendLine()
    {
      return Append(Environment.NewLine);
    }

    [System.Runtime.InteropServices.ComVisible(false)]
    public StringBuilder AppendLine(string value)
    {
      Append(value);
      return Append(Environment.NewLine);
    }

    [System.Runtime.InteropServices.ComVisible(false)]
    unsafe public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
    {
      // TODO test this!
      if (destination == null)
      {
        throw new ArgumentNullException();
      }

      if (count < 0)
      {
        throw new ArgumentOutOfRangeException();
      }

      if (destinationIndex < 0)
      {
        throw new ArgumentOutOfRangeException();
      }

      if (destinationIndex > destination.Length - count)
      {
        throw new ArgumentException();
      }

      int currentLength = Length;
      if ((uint)sourceIndex > (uint)currentLength)
      {
        throw new ArgumentOutOfRangeException();
      }

      if (sourceIndex > currentLength - count)
      {
        throw new ArgumentException();
      }
      Contract.EndContractBlock();


      VerifyClassInvariant();

      StringBuilder chunk = this;
      int sourceEndIndex = sourceIndex + count;
      int curDestIndex = destinationIndex + count;

      Contract.Assert(curDestIndex >= 0); // f: I've added it

      while (count > 0)
      {
        int chunkEndIndex = sourceEndIndex - chunk.m_ChunkOffset;
        if (chunkEndIndex >= 0)
        {
          if (chunkEndIndex > chunk.m_ChunkLength)
            chunkEndIndex = chunk.m_ChunkLength;

          int chunkCount = count;
          int chunkStartIndex = chunkEndIndex - count;
          if (chunkStartIndex < 0)
          {
            chunkCount += chunkStartIndex;
            chunkStartIndex = 0;
          }
          curDestIndex -= chunkCount;
          count -= chunkCount;

          ThreadSafeCopy(chunk.m_ChunkChars, chunkStartIndex, destination, curDestIndex, chunkCount);
        }
        chunk = chunk.m_ChunkPrevious;
      }
    }

    // Inserts multiple copies of a string into this string builder at the specified position.
    // Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, this
    // string builder is not changed. 
    // 
    public unsafe StringBuilder Insert(int index, String value, int count)
    {
      // F: I am adding this precondition, which seems missing from Vance's code
      Contract.Requires(index >= 0);

      //Range check the index.
      int currentLength = Length;
      if ((uint)index > (uint)currentLength)
      {
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }

      if (count < 0)
      {
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      }

      //If value is null, empty or count is 0, do nothing. This is ECMA standard.
      // old: 
      // if (value == null || value.Length == 0 || count == 0)
      // {
      // return this;
      //}

      // begin new 
      if (value == null)
      {
        return this;
      }
      if (value.Length == 0)
      {
        return this;
      }
      if(count == 0)
      {
        return this;
      }
      //end new
      
      StringBuilder chunk;
      int indexInChunk;
      MakeRoom(index, value.Length * count, out chunk, out indexInChunk, false);
      fixed (char* valuePtr = value)
      {
        while (count > 0)
        {
          Contract.Assume(chunk.m_ChunkOffset >= 0);
          Contract.Assume(chunk.m_ChunkLength >= 0);

          ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, valuePtr, value.Length);
          --count;
        }
      }
      return this;
    }

    // Removes the specified characters from this string builder.
    // The length of this string builder is reduced by 
    // length, but the capacity is unaffected.
    unsafe public StringBuilder Remove(int startIndex, int count)
    {
      int length = Length;
      if (length == count && startIndex == 0)
      {
        // Optimization.  If we are deleting everything  
        Length = 0;
        return this;
      }

      if (count < 0)
      {
        throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NegativeLength"));
      }

      if (startIndex < 0)
      {
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      }

      if (count > length - startIndex)
      {
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }

      if (count > 0)
      {
        StringBuilder chunk;
        int indexInChunk;
        Remove(startIndex, count, out chunk, out indexInChunk);
      }
      return this;
    }

    //
    // PUBLIC INSTANCE FUNCTIONS
    //
    //

    /*====================================Append====================================
    **
    ==============================================================================*/
    // Appends a boolean to the end of this string builder.
    // The capacity is adjusted as needed. 
    public StringBuilder Append(bool value)
    {
      return Append(value.ToString());
    }

    // Appends an sbyte to this string builder.
    // The capacity is adjusted as needed. 
    public StringBuilder Append(sbyte value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    // Appends a ubyte to this string builder.
    // The capacity is adjusted as needed. 
    public StringBuilder Append(byte value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    // Appends a character at the end of this string builder. The capacity is adjusted as needed.
    unsafe public StringBuilder Append(char value)
    {
      // F: here we have some lose of precision in the analysis

      if (m_ChunkLength < m_ChunkChars.Length)
        m_ChunkChars[m_ChunkLength++] = value;
      else
        Append(value, 1);
      
      return this;
    }

    // Appends a short to this string builder.
    // The capacity is adjusted as needed. 
    public StringBuilder Append(short value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    // Appends an int to this string builder.
    // The capacity is adjusted as needed. 
    public StringBuilder Append(int value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    // Appends a long to this string builder. 
    // The capacity is adjusted as needed. 
    public StringBuilder Append(long value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    // Appends a float to this string builder. 
    // The capacity is adjusted as needed. 
    public StringBuilder Append(float value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    // Appends a double to this string builder. 
    // The capacity is adjusted as needed. 
    public StringBuilder Append(double value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    public StringBuilder Append(decimal value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    // Appends an ushort to this string builder. 
    // The capacity is adjusted as needed. 
    public StringBuilder Append(ushort value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    // Appends an uint to this string builder. 
    // The capacity is adjusted as needed. 
    public StringBuilder Append(uint value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    // Appends an unsigned long to this string builder. 
    // The capacity is adjusted as needed. 
    public StringBuilder Append(ulong value)
    {
      return Append(value.ToString(CultureInfo.CurrentCulture));
    }

    // Appends an Object to this string builder. 
    // The capacity is adjusted as needed. 
    public StringBuilder Append(Object value)
    {
      if (null == value)
      {
        //Appending null is now a no-op.
        return this;
      }
      return Append(value.ToString());
    }

    // Appends all of the characters in value to the current instance.
    unsafe public StringBuilder Append(char[] value)
    {
      // F: Split the test in two tests, as Clousot does not undestand it yet
      // Begin Old
      // if (null != value && value.Length > 0)
      // End Old

      // New
      if (null != value)
        if (value.Length > 0)
        {
          fixed (char* valueChars = &value[0])
            Append(valueChars, value.Length);
        }

      return this;
    }

    /*====================================Insert====================================
    **
    ==============================================================================*/

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed.
    // 
    unsafe public StringBuilder Insert(int index, String value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      if ((uint)index > (uint)Length)
      {
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }

      if (value != null)
      {
        fixed (char* sourcePtr = value)
          Insert(index, sourcePtr, value.Length);
      }
      return this;
    }

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed.
    // 
    public StringBuilder Insert(int index, bool value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(), 1);
    }

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed.
    // 
    public StringBuilder Insert(int index, sbyte value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed.
    // 
    public StringBuilder Insert(int index, byte value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed.
    // 
    public StringBuilder Insert(int index, short value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed.
    // 
    unsafe public StringBuilder Insert(int index, char value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      Insert(index, &value, 1);
      return this;
    }

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed.
    // 
    public StringBuilder Insert(int index, char[] value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      if ((uint)index > (uint)Length)
      {
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }

      if (value != null)
        Insert(index, value, 0, value.Length);
      return this;
    }

    // Returns a reference to the StringBuilder with charCount characters from 
    // value inserted into the buffer at index.  Existing characters are shifted
    // to make room for the new text and capacity is adjusted as required.  If value is null, the StringBuilder
    // is unchanged.  Characters are taken from value starting at position startIndex.
    unsafe public StringBuilder Insert(int index, char[] value, int startIndex, int charCount)
    {
      // f: I've added this precondition
      Contract.Requires(index >= 0);
      // endf

      int currentLength = Length;
      if ((uint)index > (uint)currentLength)
      {
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }

      //If they passed in a null char array, just jump out quickly.
      if (value == null)
      {
        if (startIndex == 0 && charCount == 0)
        {
          return this;
        }
        throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_String"));
      }

      //Range check the array.
      if (startIndex < 0)
      {
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_StartIndex"));
      }

      if (charCount < 0)
      {
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
      }

      if (startIndex > value.Length - charCount)
      {
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }

      if (charCount > 0)
      {
        fixed (char* sourcePtr = &value[startIndex])
          Insert(index, sourcePtr, charCount);
      }
      return this;
    }

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed.
    // 
    public StringBuilder Insert(int index, int value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed.
    // 
    public StringBuilder Insert(int index, long value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed.
    // 
    public StringBuilder Insert(int index, float value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    // Returns a reference to the StringBuilder with ; value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed. 
    // 
    public StringBuilder Insert(int index, double value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    public StringBuilder Insert(int index, decimal value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    // Returns a reference to the StringBuilder with value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. 
    // 
    public StringBuilder Insert(int index, ushort value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    // Returns a reference to the StringBuilder with value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. 
    // 
    public StringBuilder Insert(int index, uint value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    // Returns a reference to the StringBuilder with value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the new text.
    // The capacity is adjusted as needed. 
    // 
    public StringBuilder Insert(int index, ulong value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      return Insert(index, value.ToString(CultureInfo.CurrentCulture), 1);
    }

    // Returns a reference to this string builder with value inserted into 
    // the buffer at index. Existing characters are shifted to make room for the
    // new text.  The capacity is adjusted as needed. If value equals String.Empty, the
    // StringBuilder is not changed. No changes are made if value is null.
    // 
    public StringBuilder Insert(int index, Object value)
    {
      // <Clousot inferred>
      Contract.Requires((index >= 0));
      // </Clousot inferred>
      // TODO to be consistent it should fail if index < 0 && value == null but that is a breaking change

      //If we get a null 
      if (null == value)
      {
        return this;
      }
      return Insert(index, value.ToString(), 1);
    }

    public StringBuilder AppendFormat(String format, Object arg0)
    {
      
      Contract.Requires(format != null);

      return AppendFormat(null, format, new Object[] { arg0 });
    }

    public StringBuilder AppendFormat(String format, Object arg0, Object arg1)
    {
      
      Contract.Requires(format != null);

      return AppendFormat(null, format, new Object[] { arg0, arg1 });
    }

    public StringBuilder AppendFormat(String format, Object arg0, Object arg1, Object arg2)
    {
      
      Contract.Requires(format != null);

      return AppendFormat(null, format, new Object[] { arg0, arg1, arg2 });
    }

    public StringBuilder AppendFormat(String format, params Object[] args)
    {
      
      Contract.Requires(format != null);

      return AppendFormat(null, format, args);
    }

    private static void FormatError()
    {
      throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
    }

    public StringBuilder AppendFormat(IFormatProvider provider, String format, params Object[] args)
    {
      // BeginOld
      //if (format == null || args == null)
      //{
      //  throw new ArgumentNullException((format == null) ? "format" : "args");
      //}
      // EndOld
      
      //if(format == null)
      //  throw new ArgumentNullException((format == null) ? "format" : "args");

      //if (args == null)
      //    throw new ArgumentNullException((format == null) ? "format" : "args");
      //Contract.EndContractBlock();

      Contract.Requires(args != null);
      Contract.Requires(format != null);

      int pos = 0;
      int len = format.Length;
      char ch = '\x0';

      ICustomFormatter cf = null;
      if (provider != null)
      {
        cf = (ICustomFormatter)provider.GetFormat(typeof(ICustomFormatter));
      }

      while (true)
      {
        int p = pos;
        int i = pos;
        while (pos < len)
        {
          ch = format[pos];

          pos++;
          if (ch == '}')
          {
            if (pos < len && format[pos] == '}') // Treat as escape character for }}
              pos++;
            else
              FormatError();
          }

          if (ch == '{')
          {
            if (pos < len && format[pos] == '{') // Treat as escape character for {{
              pos++;
            else
            {
              pos--;
              break;
            }
          }

          Append(ch);
        }

        if (pos == len) break;
        pos++;
        if (pos == len || (ch = format[pos]) < '0' || ch > '9') FormatError();
        int index = 0;
        do
        {
          index = index * 10 + ch - '0';
          pos++;
          if (pos == len) FormatError();
          ch = format[pos];
        } while (ch >= '0' && ch <= '9' && index < 1000000);
        if (index >= args.Length) throw new FormatException(Environment.GetResourceString("Format_IndexOutOfRange"));
        while (pos < len && (ch = format[pos]) == ' ') pos++;
        bool leftJustify = false;
        int width = 0;
        if (ch == ',')
        {
          pos++;
          while (pos < len && format[pos] == ' ') pos++;

          if (pos == len) FormatError();
          ch = format[pos];
          if (ch == '-')
          {
            leftJustify = true;
            pos++;
            if (pos == len) FormatError();
            ch = format[pos];
          }
          if (ch < '0' || ch > '9') FormatError();
          do
          {
            width = width * 10 + ch - '0';
            pos++;
            if (pos == len) FormatError();
            ch = format[pos];
          } while (ch >= '0' && ch <= '9' && width < 1000000);
        }

        while (pos < len && (ch = format[pos]) == ' ') pos++;
        Object arg = args[index];
        StringBuilder fmt = null;
        if (ch == ':')
        {
          pos++;
          p = pos;
          i = pos;
          while (true)
          {
            if (pos == len) FormatError();
            ch = format[pos];
            pos++;
            if (ch == '{')
            {
              if (pos < len && format[pos] == '{')  // Treat as escape character for {{
                pos++;
              else
                FormatError();
            }
            else if (ch == '}')
            {
              if (pos < len && format[pos] == '}')  // Treat as escape character for }}
                pos++;
              else
              {
                pos--;
                break;
              }
            }

            if (fmt == null)
            {
              fmt = new StringBuilder();
            }
            fmt.Append(ch);
          }
        }
        if (ch != '}') FormatError();
        pos++;
        String sFmt = null;
        String s = null;
        if (cf != null)
        {
          if (fmt != null)
          {
            sFmt = fmt.ToString();
          }
          s = cf.Format(sFmt, arg, provider);
        }

        if (s == null)
        {
          IFormattable formattableArg = arg as IFormattable;
          if (formattableArg != null)
          {
            // if (sFmt == null && fmt != null)
            if (sFmt == null)
              if(fmt != null)
            {
              sFmt = fmt.ToString();
            }
            s = formattableArg.ToString(sFmt, provider);
          }
          else if (arg != null)
          {
            s = arg.ToString();
          }
        }

        if (s == null) 
          s = String.Empty;

        int pad = width - s.Length;
        if (!leftJustify && pad > 0) Append(' ', pad);
        Append(s);
        if (leftJustify && pad > 0) Append(' ', pad);
      }
      return this;
    }

    // Returns a reference to the current StringBuilder with all instances of oldString 
    // replaced with newString.  If startIndex and count are specified,
    // we only replace strings completely contained in the range of startIndex to startIndex + 
    // count.  The strings to be replaced are checked on an ordinal basis (e.g. not culture aware).  If 
    // newValue is null, instances of oldValue are removed (e.g. replaced with nothing.).
    //
    public StringBuilder Replace(String oldValue, String newValue)
    {
      return Replace(oldValue, newValue, 0, Length);
    }

    unsafe public StringBuilder Replace(String oldValue, String newValue, int startIndex, int count)
    {
      Contract.Requires(startIndex >= 0);   // f: this precondition should go away when I've finished the implementation of the support for uints

      int currentLength = Length;
      if ((uint)startIndex > (uint)currentLength)
      {
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
      
      //old if (count < 0 || startIndex > currentLength - count)
      //begin new
      if (count < 0)
      {
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
      
      if(startIndex > currentLength - count)
      {
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
      // end new
      if (oldValue == null)
      {
        throw new ArgumentNullException("oldValue");
      }
      if (oldValue.Length == 0)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "oldValue");
      }

      if (newValue == null)
        newValue = "";

      int deltaLength = newValue.Length - oldValue.Length;    // f: why is it here???

      // f: old
      // int[] replacements = null;          // A list of replacement positions in a chunk to apply

      // f: new 
      int[] replacements = new int[5];
            
      int replacementsCount = 0;

      // Find the chunk, indexInChunk for the starting point
      StringBuilder chunk = FindChunkForIndex(startIndex);
      int indexInChunk = startIndex - chunk.m_ChunkOffset;


      Contract.Assert(indexInChunk >= 0);   // f: does it works?
      while (count > 0)
      {
        //f: I had to add this Assertion here to make all work. Clousot can prove it, but for some reason cannot materialize it (I guess because of some incompleteness in the closure)
        Contract.Assert(replacementsCount <= replacements.Length);

        // f: I am adding it, as we do not get the object invariant for field accesses
        Contract.Assume(chunk.m_ChunkLength >= 0);
        Contract.Assume(chunk.m_ChunkOffset >= 0);

        // Look for a match in the chunk,indexInChunk pointer 
        if (StartsWith(chunk, indexInChunk, count, oldValue))
        {
          // Push it on my replacements array (with growth), we will do all replacements in a
          // given chunk in one operation below (see ReplaceAllInChunk) so we don't have to slide
          // many times.  

          // f: I moved it out, as the proof depends on the fact that StartsWith suceeds at least once ...
         /*  if (replacements == null)
            replacements = new int[5];
          else*/
          if (replacementsCount >= replacements.Length)
          {
            int[] newArray = new int[replacements.Length * 3 / 2 + 4];     // grow by 1.5X but more in the begining
            
            Array.Copy(replacements, newArray, replacements.Length);
            replacements = newArray;
          }
        
          replacements[replacementsCount++] = indexInChunk;
          indexInChunk += oldValue.Length;
          count -= oldValue.Length;
        }
        else
        {
          indexInChunk++;
          --count;
        }

        // f: for test, we can prove it
        Contract.Assert(indexInChunk >= 0);

        if (indexInChunk >= chunk.m_ChunkLength)       // Have we moved out of the current chunk
        {
          Contract.Assume(chunk.m_ChunkOffset >= 0);  // f: I've added it

          // Replacing mutates the blocks, so we need to convert to logical index and back afterward. 
          int index = indexInChunk + chunk.m_ChunkOffset;
          int indexBeforeAdjustment = index;

          // See if we accumulated any replacements, if so apply them 
          
          // f: With -unsafe we get a warning, as stripes do not capture <= 
          ReplaceAllInChunk(replacements, replacementsCount, chunk, oldValue.Length, newValue);

          // f: Ok: Clousot can prove it
          Contract.Assert(replacementsCount >= 0);

          // f: I've added this This may not always satisfied, as the parameter newValue can be == null, I should fix it
          // Contract.Assert(newValue.Length >= oldValue.Length);


          // The replacement has affected the logical index.  Adjust it.  
          index += ((newValue.Length - oldValue.Length) * replacementsCount);

          // f: for test only, we can prove it
          // Contract.Assert(index >= 0);

          replacementsCount = 0;

          chunk = FindChunkForIndex(index);

          Contract.Assert(index >= chunk.m_ChunkOffset);

          indexInChunk = index - chunk.m_ChunkOffset;

          Contract.Assert(index >= 0);

          // f: We can prove it
          Contract.Assert(indexInChunk >= 0);

          Contract.Assert(chunk != null || count == 0, "Chunks ended prematurely");
        }
      }
      VerifyClassInvariant();
      return this;
    }

    // Returns a StringBuilder with all instances of oldChar replaced with 
    // newChar.  The size of the StringBuilder is unchanged because we're only
    // replacing characters.  If startIndex and count are specified, we 
    // only replace characters in the range from startIndex to startIndex+count
    public StringBuilder Replace(char oldChar, char newChar)
    {
      return Replace(oldChar, newChar, 0, Length);
    }

    public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
    {
      int currentLength = Length;
      if ((uint)startIndex > (uint)currentLength)
      {
        throw new ArgumentOutOfRangeException("startIndex");
      }

      //if (count < 0 || startIndex > currentLength - count)
      //{
      //  throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      //}

      if (count < 0) 
      {
        throw new ArgumentOutOfRangeException("count");
      }

      if (startIndex > currentLength - count)
      {
      //  throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));

        throw new ArgumentOutOfRangeException("count");
      }
      Contract.EndContractBlock();


      int endIndex = startIndex + count;
      StringBuilder chunk = this;
      for (; ; )
      {
        int endIndexInChunk = endIndex - chunk.m_ChunkOffset;
        int startIndexInChunk = startIndex - chunk.m_ChunkOffset;
        
        if (endIndexInChunk >= 0)
        {
          int curInChunk = Math.Max(startIndexInChunk, 0);
          
          int endInChunk = Math.Min(chunk.m_ChunkLength, endIndexInChunk);

          
          while (curInChunk < endInChunk)
          {
            if (chunk.m_ChunkChars[curInChunk] == oldChar)
              chunk.m_ChunkChars[curInChunk] = newChar;
            curInChunk++;
          }
        }
        if (startIndexInChunk >= 0)
          break;
        chunk = chunk.m_ChunkPrevious;
      }
      return this;
    }

    public bool Equals(StringBuilder sb)
    {
      if (sb == null)
        return false;
      // f: original
      //if (Capacity != sb.Capacity || MaxCapacity != MaxCapacity || Length != sb.Length)
      //  return false;
      if (Capacity != sb.Capacity)
        return false;
      if (MaxCapacity != MaxCapacity)
        return false;
      if (Length != sb.Length)
        return false;
      if (sb == this)
        return true;

      StringBuilder thisChunk = this;
      int thisChunkIndex = thisChunk.m_ChunkLength;
      StringBuilder sbChunk = sb;
      int sbChunkIndex = sbChunk.m_ChunkLength;
      for (; ; )
      {
        // Decrement the pointer to the 'this' StringBuilder
        --thisChunkIndex;
        --sbChunkIndex;

        while (thisChunkIndex < 0)
        {
          thisChunk = thisChunk.m_ChunkPrevious;
          if (thisChunk == null)
            break;
          thisChunkIndex = thisChunk.m_ChunkLength + thisChunkIndex;
        }

        // Decrement the pointer to the 'this' StringBuilder
        while (sbChunkIndex < 0)
        {
          sbChunk = sbChunk.m_ChunkPrevious;
          if (sbChunk == null)
            break;
          sbChunkIndex = sbChunk.m_ChunkLength + sbChunkIndex;
        }

        if (thisChunkIndex < 0)
          return sbChunkIndex < 0;
        if (sbChunkIndex < 0)
          return false;
        if (thisChunk.m_ChunkChars[thisChunkIndex] != sbChunk.m_ChunkChars[sbChunkIndex])
          return false;
      }
    }

    /// <summary>
    /// Appends 'value' of length 'count' to the stringBuilder. 
    /// </summary>
    internal unsafe StringBuilder Append(char* value, int valueCount)
    {
      Contract.Requires(value != null, "Value can't be null");
      
      
      // Contract.Assert(valueCount >= 0, "Count can't be negative");
      Contract.Requires(valueCount >= 0);

      Contract.Requires(Contract.WritableBytes(value) >= (uint) valueCount *sizeof(char));


      // This case is so common we want to optimize for it heavily.       
      int newIndex = valueCount + m_ChunkLength;
      if (newIndex <= m_ChunkChars.Length)
      {
        ThreadSafeCopy(value, m_ChunkChars, m_ChunkLength, valueCount);
        m_ChunkLength = newIndex;
      }
      else
      {
        // Copy the first chunk
        int firstLength = m_ChunkChars.Length - m_ChunkLength;

        Contract.Assert(firstLength >= 0);    // F: I've added it
                       
        if (firstLength > 0)
        {
          // f: We need to keep track of 4 vars... Need Subpolyhedra
          ThreadSafeCopy(value, m_ChunkChars, m_ChunkLength, firstLength);
          m_ChunkLength = m_ChunkChars.Length;

          Contract.Assert(Length == Capacity); // F: I've added it
        }

        Contract.Assert(Length == Capacity); // F: I've added it

        // Expand the builder to add another chunk. 
        int restLength = valueCount - firstLength;
        ExpandByABlock(restLength);
        Contract.Assert(m_ChunkLength == 0, "Expand did not make a new block");

        // Copy the second chunk
        ThreadSafeCopy(value + firstLength, m_ChunkChars, 0, restLength);
        m_ChunkLength = restLength;
      }
      VerifyClassInvariant();
      return this;
    }

    /// <summary>
    /// Inserts 'value' of length 'cou
    /// </summary>
    unsafe private void Insert(int index, char* value, int valueCount)
    {
      #if UNSAFE
      Contract.Requires(index >= 0);
      // <Clousot inferred>
      Contract.Requires((System.Diagnostics.Contracts.Contract.WritableBytes(value) >= (System.UInt64)(valueCount * 2)));
      // </Clousot inferred>
      #endif
      if ((uint)index > (uint)Length)
      {
        throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }

      if (valueCount > 0)
      {
        StringBuilder chunk;
        int indexInChunk;
        MakeRoom(index, valueCount, out chunk, out indexInChunk, false);
        
        Contract.Assume(chunk.m_ChunkLength >= 0);
        Contract.Assume(chunk.m_ChunkOffset >= 0);

        ReplaceInPlaceAtChunk(ref chunk, ref indexInChunk, value, valueCount);
      }
    }

    /// <summary>
    /// 'replacements' is a list of index (relative to the begining of the 'chunk' to remove
    /// 'removeCount' characters and replace them with 'value'.   This routine does all those 
    /// replacements in bulk (and therefore very efficiently. 
    /// with the string 'value'.  
    /// </summary>
    unsafe private void ReplaceAllInChunk(int[] replacements, int replacementsCount, StringBuilder sourceChunk, int removeCount, string value)
    {
      Contract.Requires(replacements != null);
      Contract.Requires(value != null);
      Contract.Requires(sourceChunk != null);
      
      Contract.Requires(replacements.Length > 0);
      Contract.Requires(replacementsCount >= 0);
      Contract.Requires(replacementsCount <= replacements.Length);
      

      if (replacementsCount <= 0)
        return;

      fixed (char* valuePtr = value)
      {
        // calculate the total amount of extra space or space needed for all the replacements.  
        int delta = (value.Length - removeCount) * replacementsCount;

        StringBuilder targetChunk = sourceChunk;        // the target as we copy chars down

        // f: I am making them an assumption ...
        Contract.Assume(targetChunk.m_ChunkOffset >= 0);
        Contract.Assume(targetChunk.m_ChunkLength >= 0);

        int targetIndexInChunk = replacements[0];
        Contract.Assume(targetIndexInChunk >= 0); // f: made it an assumption as we do not handle array contents now

        // Make the room needed for all the new characters if needed. 
        if (delta > 0)
        {
          Contract.Assume(targetChunk.m_ChunkOffset >= 0);  // f: I've addded this because the we do not get it from the targetChunk invariant
          Contract.Assume(targetChunk.m_ChunkLength >= 0);

          MakeRoom(targetChunk.m_ChunkOffset + targetIndexInChunk, delta, out targetChunk, out targetIndexInChunk, true);
        }
       
        // We made certain that characters after the insertion point are not moved, 
        int i = 0;
        for (; ; )
        {
          Contract.Assume(targetChunk.m_ChunkOffset >= 0);  // f: I've addded this because the we do not get it from the targetChunk invariant
          Contract.Assume(targetChunk.m_ChunkLength >= 0);

          // Copy in the new string for the ith replacement
          ReplaceInPlaceAtChunk(ref targetChunk, ref targetIndexInChunk, valuePtr, value.Length);
          int gapStart = replacements[i] + removeCount;
          Contract.Assume(gapStart >= 0);   // F: made it an assumption as we do not handle the values inside arrays

          i++;
          if (i >= replacementsCount)
            break;

          int gapEnd = replacements[i];
          Contract.Assume(gapEnd >= 0);   // f: Adding the assumption, as we do not handle array contents yet

          // Contract.Assert(gapStart < sourceChunk.m_ChunkChars.Length, "gap starts at end of buffer.  Should not happen");
          Contract.Assume(gapStart < sourceChunk.m_ChunkChars.Length); // F: made it an assumption as we do not handle array contents

          // Contract.Assert(gapStart <= gapEnd, "negative gap size");
          Contract.Assume(gapStart <= gapEnd);    // f: Made it an assumption as we do not handle array contents

          // Contract.Assert(gapEnd <= sourceChunk.m_ChunkLength, "gap too big");
          Contract.Assume(gapEnd <= sourceChunk.m_ChunkLength);

          // f: I've added it because we do not propagate invariants on fields accesses
          Contract.Assume(sourceChunk.m_ChunkLength <= sourceChunk.m_ChunkChars.Length);


          if (delta != 0)     // can skip the sliding of gaps if source an target string are the same size.  
          {
            Contract.Assume(targetChunk.m_ChunkOffset >= 0);  // f: I've addded this because  we do not get it from the targetChunk invariant
            Contract.Assume(targetChunk.m_ChunkLength >= 0);


            // Copy the gap data between the current replacement and the the next replacement
            fixed (char* sourcePtr = &sourceChunk.m_ChunkChars[gapStart])
              ReplaceInPlaceAtChunk(ref targetChunk, ref targetIndexInChunk, sourcePtr, gapEnd - gapStart);
          }
          else
          {
            targetIndexInChunk += gapEnd - gapStart;

            Contract.Assert(targetIndexInChunk <= targetChunk.m_ChunkLength, "gap not in chunk");
          }
        }

        // Remove extra space if necessary. 
        if (delta < 0)
        {
          Contract.Assume(targetChunk.m_ChunkOffset >= 0);  // f: I've addded this because the we do not get it from the targetChunk invariant

          Remove(targetChunk.m_ChunkOffset + targetIndexInChunk, -delta, out targetChunk, out targetIndexInChunk);
        }
      }
    }

    /// <summary>
    /// Returns true if the string that is starts at 'chunk' and 'indexInChunk, and has a logical
    /// length of 'count' starts with the string 'value'. 
    /// </summary>
    private bool StartsWith(StringBuilder chunk, int indexInChunk, int count, string value)
    {
      
      Contract.Requires(chunk != null);
      Contract.Requires(value != null);
      

      Contract.Requires(indexInChunk >= 0); // F: Check what happens when indexInChunk < 0
      
      // f: I've added those two preconditions as we do not assume object invariants at field dereference
      Contract.Requires(chunk.m_ChunkLength >= 0);
      Contract.Requires(chunk.m_ChunkOffset >= 0);
      
      
      for (int i = 0; i < value.Length; i++)
      {
        if (count == 0)
          return false;
        
        if (indexInChunk >= chunk.m_ChunkLength)
        {
          // F: cannot prove the precondition, as the postcondition of Next is (null || result.chunk >= 0)
          chunk = Next(chunk); 
          if (chunk == null)
            return false;
          indexInChunk = 0;
        }

        // See if there no match, break out of the inner for loop
        if (value[i] != chunk.m_ChunkChars[indexInChunk]) 
          return false;

        indexInChunk++;
        --count;
      }
      return true;
    }

    /// <summary>
    /// ReplaceInPlaceAtChunk is the logical equivalent of 'memcpy'.  Given a chunk and ann index in
    /// that chunk, it copies in 'count' characters from 'value' and updates 'chunk, and indexInChunk to 
    /// point at the end of the characters just copyied (thus you can splice in strings from multiple 
    /// places by calling this mulitple times.  
    /// </summary>
    unsafe private void ReplaceInPlaceAtChunk(ref StringBuilder chunk, ref int indexInChunk, char* value, int count)
    {
      
      Contract.Requires(chunk != null);
      

      
      Contract.Requires(indexInChunk >= 0);
      Contract.Requires(count >= 0);

      // f: I've added those two because of object invariants
      Contract.Requires(chunk.m_ChunkLength >= 0);
      Contract.Requires(chunk.m_ChunkOffset >= 0);
      Contract.Requires(Contract.WritableBytes(value) >= (uint) count * sizeof(char));
      Contract.Ensures(indexInChunk >= 0);
      

      
      if (count != 0)
      {
        for (; ; )
        {
          int lengthInChunk = chunk.m_ChunkLength - indexInChunk;
          // Contract.Assert(lengthInChunk >= 0, "index not in chunk");

          Contract.Assume(lengthInChunk >= 0);      // F: I am putting is as an assumption as it depends on the object invariant, etc.

          int lengthToCopy = Math.Min(lengthInChunk, count);

          // f: Here should prove that WB(value) >= 2 * lenghtToCopy, but it is difficult because of the operations on lengthToCopy
          ThreadSafeCopy(value, chunk.m_ChunkChars, indexInChunk, lengthToCopy);

          // Advance the index. 
          indexInChunk += lengthToCopy;
          if (indexInChunk >= chunk.m_ChunkLength)
          {
            chunk = Next(chunk);
            indexInChunk = 0;
          }
          count -= lengthToCopy;
          if (count == 0)
            break;
          value += lengthToCopy;
        }
      }
    }

    /// <summary>
    /// We have to prevent hackers from causing modification off the end of an array.
    /// The only way to do this is to copy all interesting variables out of the heap and then do the
    /// bounds check.  This is what we do here.   
    /// </summary>
    unsafe private static void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
    {
      Contract.Requires(destination != null);      
      Contract.Requires(destinationIndex >= 0);      
      Contract.Requires(Contract.WritableBytes(sourcePtr) >= (uint) count * sizeof(char));

      if (count > 0)
      {
        // if ((uint)destinationIndex <= (uint)destination.Length && (destinationIndex + count) <= destination.Length)
        if ((uint)destinationIndex <= (uint)destination.Length)
          if ((destinationIndex + count) <= destination.Length)
          {
            fixed (char* destinationPtr = &destination[destinationIndex])
              CopyChars(destinationPtr, sourcePtr, count);
          }
          else
          {
            Contract.Assert(false, "Thread safety check failed");
            throw new Exception("Thread safety exception");
          }
      }
    }
    unsafe private static void ThreadSafeCopy(char[] source, int sourceIndex, char[] destination, int destinationIndex, int count)
    {

      Contract.Requires(source != null);
      Contract.Requires(destination != null);

      Contract.Requires(sourceIndex >= 0);
      Contract.Requires(destinationIndex >= 0);

      if (count > 0)
      {
        // if ((uint)sourceIndex <= (uint)source.Length && (sourceIndex + count) <= source.Length)
        if ((uint)sourceIndex <= (uint)source.Length)
          if ((sourceIndex + count) <= source.Length)
          {
            fixed (char* sourcePtr = &source[sourceIndex])
              ThreadSafeCopy(sourcePtr, destination, destinationIndex, count);
          }
          else
          {
            Contract.Assert(false, "Thread safety check failed");
            throw new Exception("Thread safety exception");
          }
      }
    }

    // TODO remove when in mscorlib
    unsafe private static void CopyChars(char* dest, char* source, int count)
    {
      Contract.Requires(Contract.WritableBytes(source) >= sizeof(char) * (uint)count);
      Contract.Requires(Contract.WritableBytes(dest) >= sizeof(char) * (uint)count);

      for (int i = 0; i < count; i++)
      {

        /** 
        // TODO assert below is not true for strings with embedded nulls, but 
        // it will catch a lot of failures, so we put it in initially. 
        Contract.Assert(source[i] != 0, "source has null in it");

        // TODO we currently only have clean buffers or printable things left over.  
        Contract.Assert(dest[i] == 0 || (dest[i] >= ' ' && dest[i] < 'z'), "destination seems corrupted");
         * 
        ***/

        dest[i] = source[i];
      }
    }

    /// <summary>
    /// Finds the chunk for the logical index (number of characters in the whole stringbuilder) 'index'
    /// YOu can then get the offset in this chunk by subtracting the m_BlockOffset field from 'index' 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private StringBuilder FindChunkForIndex(int index)
    {
      // f: It was : Debug.Assert(0 <= index && index <= Length, "index not in string");
      Contract.Requires(0 <= index);
      Contract.Requires(index <= Length);

      Contract.Ensures(Contract.Result<StringBuilder>().m_ChunkOffset <= index);
      Contract.Ensures(Contract.Result<StringBuilder>().m_ChunkOffset >= 0);
      Contract.Ensures(Contract.Result<StringBuilder>().m_ChunkLength >= 0);

      StringBuilder ret = this;
      while (ret.m_ChunkOffset > index)
      {
        ret = ret.m_ChunkPrevious;
        Contract.Assume(ret.m_ChunkOffset >= 0);    // f: I am adding these assumptions because we do not propagate invariants at accesses
        Contract.Assume(ret.m_ChunkLength >= 0);
      }


      Contract.Assert(ret != null, "index not in string");

      return ret;
    }

    /// <summary>
    /// Finds the chunk that logically follows the 'chunk' chunk.  Chunks only persist the pointer to 
    /// the chunk that is logically before it, so this routine has to start at the this pointer (which 
    /// is a assumed to point at the chunk representing the whole stringbuilder) and search
    /// until it finds the current chunk (thus is O(n)).  So it is more expensive than a field fetch!
    /// </summary>
    private StringBuilder Next(StringBuilder chunk)
    {
      // f: I am adding those two preconditions as we do not propagate the chuck invariant (yet?)
      //     It would be cool to automatically propagate them to the caller

      Contract.Requires(chunk.m_ChunkOffset >= 0);
      Contract.Requires(chunk.m_ChunkLength >= 0);
      
      Contract.Ensures(Contract.Result<StringBuilder>()== null || Contract.Result<StringBuilder>().m_ChunkOffset >= 0);
      Contract.Ensures(Contract.Result<StringBuilder>()== null || Contract.Result<StringBuilder>().m_ChunkLength >= 0);


      if (chunk == this)
        return null;
      return FindChunkForIndex(chunk.m_ChunkOffset + chunk.m_ChunkLength);
    }

    /// <summary>
    /// Assumes that 'this' is the last chunk in the list and that it is full.  Upon return the 'this'
    /// block is updated so that it is a new block that has at least 'minBlockCharCount' characters.
    /// that can be used to copy characters into it.   
    /// </summary>
    private void ExpandByABlock(int minBlockCharCount)
    {
      // Contract.Assert(Capacity == Length, "Expand expect to be called only when there is no space left");        // We are currently full
      // Contract.Assert(minBlockCharCount > 0, "Expansion request must be postitive");

    
      Contract.Requires(Capacity == Length);
      Contract.Requires(minBlockCharCount > 0);
    
      Contract.Ensures(this.m_ChunkLength == 0);

      VerifyClassInvariant();

      if (minBlockCharCount + Length > m_MaxCapacity)
        throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));

      // Compute the length of the new block we need 
      // We make the new chunk at least big enough for the current need (minBlockCharCount)
      // But also as big as the current length (thus doubling capacity), up to a maximum
      // (so we stay in the small object heap, and never allocate really big chunks even if
      // the string gets really big. 
      int newBlockLength = Math.Max(minBlockCharCount, Math.Min(Length, MaxChunkSize));

      // Copy the current block to the new block, and initialize this to point at the new buffer. 
      m_ChunkPrevious = new StringBuilder(this);
      m_ChunkOffset += m_ChunkLength;
      m_ChunkLength = 0;

      // Check for integer overflow (logical buffer size > int.MaxInt)
      if (m_ChunkOffset + newBlockLength < newBlockLength)
      {
        m_ChunkChars = null;
        throw new OutOfMemoryException();
      }
      m_ChunkChars = new char[newBlockLength];

      VerifyClassInvariant();
    }

    /// <summary>
    /// Used by ExpandByABlock to create a new chunk.  The new chunk is a copied from 'from'
    /// In particular the buffer is shared.  It is expected that 'from' chunk (which represents
    /// the whole list, is then updated to point to point to this new chunk. 
    /// </summary>
    private StringBuilder(StringBuilder from)
    {
      // f: I am adding the two as preconditions, 
      // <Clousot inferred>
      Contract.Requires((from.m_ChunkOffset >= 0));
      Contract.Requires((from.m_ChunkLength >= 0));
      // </Clousot inferred>

      Contract.Requires(from.m_ChunkLength <= from.m_ChunkChars.Length);

      m_ChunkLength = from.m_ChunkLength;
      m_ChunkOffset = from.m_ChunkOffset;
      m_ChunkChars = from.m_ChunkChars;
      m_ChunkPrevious = from.m_ChunkPrevious;
      m_MaxCapacity = from.m_MaxCapacity;
    }

    /// <summary>
    /// Creates a gap of size 'count' at the logical offset (count of characters in the whole string
    /// builder) 'index'.  It returns the 'chunk' and 'indexInChunk' which represents a pointer to
    /// this gap that was just created.  You can then use 'ReplaceInPlaceAtChunk' to fill in the
    /// chunk
    ///
    /// ReplaceAllChunks relies on the fact that indexes above 'index' are NOT moved outside 'chunk'
    /// by this process (because we make the space by creating the cap BEFORE the chunk).  If we
    /// change this ReplaceAllChunks needs to be updated. 
    ///
    /// If dontMoveFollowingChars is true, then the room must be made by inserting a chunk BEFORE the
    /// current chunk (this is what it does most of the time anyway)
    /// </summary>
    unsafe private void MakeRoom(int index, int count, out StringBuilder chunk, out int indexInChunk, bool doneMoveFollowingChars)
    {
      // VerifyClassInvariant();
      // Debug.Assert(count > 0, "Count must be strictly positive");
      // Debug.Assert(index >= 0, "Index can't be negative");

      Contract.Requires(count > 0);
      Contract.Requires(index >= 0);

      Contract.Ensures(Contract.ValueAtReturn<StringBuilder>(out chunk) != null);

      Contract.Ensures(Contract.ValueAtReturn<int>(out indexInChunk) >= 0);

      if (count + Length > m_MaxCapacity)
        throw new ArgumentOutOfRangeException("requiredLength", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));

      chunk = this;
      while (chunk.m_ChunkOffset > index)
      {
        chunk.m_ChunkOffset += count;
        chunk = chunk.m_ChunkPrevious;
      }
      indexInChunk = index - chunk.m_ChunkOffset; // F: this is an imprecision in the analysis!!! Should infer indexInChunk >= 0 ?

      // Cool, we have some space in this block, and you don't have to copy much to get it, go ahead
      // and use it.  This happens typically  when you repeatedly insert small strings at a spot
      // (typically the absolute front) of the buffer.    
      if (!doneMoveFollowingChars && chunk.m_ChunkLength <= DefaultCapacity * 2 && chunk.m_ChunkChars.Length - chunk.m_ChunkLength >= count)
      {
        for (int i = chunk.m_ChunkLength; i > indexInChunk; )
        {
          --i;
          chunk.m_ChunkChars[i + count] = chunk.m_ChunkChars[i];
        }
        chunk.m_ChunkLength += count;
        return;
      }

      // Allocate space for the new chunk (will go before this one)

      // f: I've added this assumption because we do not use object invariants at fields dereference
      Contract.Assume(chunk.m_MaxCapacity > 0);   
      
      Contract.Assume(chunk.m_ChunkPrevious.m_ChunkOffset >= 0);
      Contract.Assume(chunk.m_ChunkPrevious.m_ChunkLength >= 0);

      StringBuilder newChunk = new StringBuilder(Math.Max(count, DefaultCapacity), chunk.m_MaxCapacity, chunk.m_ChunkPrevious);
      newChunk.m_ChunkLength = count;

      // Copy the head of the buffer to the  new buffer. 
      int copyCount1 = Math.Min(count, indexInChunk);
      if (copyCount1 > 0)
      {
        fixed (char* chunkCharsPtr = chunk.m_ChunkChars)
        {
          ThreadSafeCopy(chunkCharsPtr, newChunk.m_ChunkChars, 0, copyCount1);

          // Slide characters in the current buffer over to make room. 
          int copyCount2 = indexInChunk - copyCount1;
          if (copyCount2 >= 0)
          {
            ThreadSafeCopy(chunkCharsPtr + copyCount1, chunk.m_ChunkChars, 0, copyCount2);
            indexInChunk = copyCount2;
          }
        }
      }

      chunk.m_ChunkPrevious = newChunk;           // Wire in the new chunk
      chunk.m_ChunkOffset += count;
      if (copyCount1 < count)
      {
        chunk = newChunk;
        indexInChunk = copyCount1;
      }

      VerifyClassInvariant();
    }

    /// <summary>
    ///  Used by MakeRoom to allocate another chunk.  
    /// </summary>
    private StringBuilder(int size, int maxCapacity, StringBuilder previousBlock)
    {

      Contract.Requires(size > 0); // , "size not positive");
      Contract.Requires(maxCapacity > 0); //, "maxCapacity not positive");
Contract.Requires(previousBlock.m_ChunkOffset >= 0);
      Contract.Requires(previousBlock.m_ChunkLength >= 0);


      m_ChunkChars = new char[size];
      m_MaxCapacity = maxCapacity;
      m_ChunkPrevious = previousBlock;
      if (previousBlock != null)
        m_ChunkOffset = previousBlock.m_ChunkOffset + previousBlock.m_ChunkLength;  // f: Warning here because of the assumption of the object invariant for previousBlock
    }

    /// <summary>
    /// Removes 'count' characters from the logical index 'startIndex' and returns the chunk and 
    /// index in the chunk of that logical index in the out parameters.  
    /// </summary>
    unsafe private void Remove(int startIndex, int count, out StringBuilder chunk, out int indexInChunk)
    {
      // VerifyClassInvariant();
      // Contract.Assert(startIndex >= 0 && startIndex < Length, "startIndex not in string");

      
      Contract.Requires(startIndex >= 0);
      Contract.Requires(count >= 0);    // f: This precondition was missing from Vance's code
      Contract.Requires(startIndex < Length);
      

      int endIndex = startIndex + count;

      // Find the chunks for the start and end of the block to delete. 
      chunk = this;
      StringBuilder endChunk = null;
      int endIndexInChunk = 0;
      for (; ; )
      {
        if (endIndex - chunk.m_ChunkOffset >= 0)
        {
          if (endChunk == null)
          {
            endChunk = chunk;
            endIndexInChunk = endIndex - endChunk.m_ChunkOffset;

            Contract.Assert(endIndexInChunk >= 0); // f: To be deleted, here just to debug Clousot
          }
          if (startIndex - chunk.m_ChunkOffset >= 0)
          {
            indexInChunk = startIndex - chunk.m_ChunkOffset;
            break;
          }
        }
        else
        {
          chunk.m_ChunkOffset -= count;
        }
        chunk = chunk.m_ChunkPrevious;
      }

      // f: Added by me, should be deleted, here just to debug Clousot
      Contract.Assert(endIndexInChunk >= 0);


      Contract.Assert(chunk != null, "fell off begning of string!");


      int copyTargetIndexInChunk = indexInChunk;
      int copyCount = endChunk.m_ChunkLength - endIndexInChunk;
      if (endChunk != chunk)
      {
        copyTargetIndexInChunk = 0;
        // Remove the characters after startIndex to end of the chunk
        chunk.m_ChunkLength = indexInChunk;

        // Remove the characters in chunks between start and end chunk
        endChunk.m_ChunkPrevious = chunk;
        endChunk.m_ChunkOffset = chunk.m_ChunkOffset + chunk.m_ChunkLength;

        // If the start is 0 then we can throw away the whole start chunk
        if (indexInChunk == 0)
        {
          endChunk.m_ChunkPrevious = chunk.m_ChunkPrevious;
          chunk = endChunk;
        }
      }
      endChunk.m_ChunkLength -= (endIndexInChunk - copyTargetIndexInChunk);

      // Remove any characters in the end chunk, by sliding the characters down. 
      if (copyTargetIndexInChunk != endIndexInChunk)  // Sometimes no move is necessary
        ThreadSafeCopy(endChunk.m_ChunkChars, endIndexInChunk, endChunk.m_ChunkChars, copyTargetIndexInChunk, copyCount);

#if NONNULL
      Contract.Assert(chunk != null, "fell off begning of string!");
#endif
      // VerifyClassInvariant();
    }

    [System.Diagnostics.Conditional("_DEBUG")]
    [System.Diagnostics.Contracts.ContractVerification(false)]
    private void VerifyClassInvariant()
    {
      Contract.Assert(m_ChunkOffset + m_ChunkChars.Length >= m_ChunkOffset, "integer overflow");
      StringBuilder currentBlock = this;
      int maxCapacity = this.m_MaxCapacity;
      for (; ; )
      {
        // All blocks have copy of the maxCapacity.
        Contract.Assert(currentBlock.m_MaxCapacity == maxCapacity, "bad maxCapacity");
        Contract.Assert(currentBlock.m_ChunkChars != null, "empty buffer");

        Contract.Assert(currentBlock.m_ChunkLength <= currentBlock.m_ChunkChars.Length, "Out of range length");
        Contract.Assert(currentBlock.m_ChunkLength >= 0, "negative length");
        Contract.Assert(currentBlock.m_ChunkOffset >= 0, "negative offset");

        StringBuilder prevBlock = currentBlock.m_ChunkPrevious;
        if (prevBlock == null)
        {
          Contract.Assert(currentBlock.m_ChunkOffset == 0, "first chunk's offset is not 0");
          break;
        }
        // There are no gaps in the blocks. 
        Contract.Assert(currentBlock.m_ChunkOffset == prevBlock.m_ChunkOffset + prevBlock.m_ChunkLength, "There is a gap between chunks!");
        currentBlock = prevBlock;
      }
    }

    // A StringBuilder is internally represented as a linked list of blocks each of which holds
    // a chunk of the string.  It turns out string as a whole can also be represented as just a chunk, 
    // so that is what we do.  
    private int m_ChunkLength;                  // The index in m_BlockChars that repsent the end of the block
    private int m_ChunkOffset;                  // The logial offset (sum of all characters in previous blocks)
    private char[] m_ChunkChars;                // The characters in this block
    private StringBuilder m_ChunkPrevious;      // Link to the block logically before this block
    private int m_MaxCapacity;                  // Limits the size of the stringbuilder (ideally we would remove this).   

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {

      Contract.Invariant(m_ChunkChars != null);
      Contract.Invariant(m_ChunkOffset >= 0);         // No negative offsets
      Contract.Invariant(m_ChunkLength >= 0);         // The Chunk lenght should be non zero

      // F: Is this an invariant?
     // Contract.Invariant(m_ChunkChars.Length > 0);    // At least one element
      Contract.Invariant(m_ChunkLength <= m_ChunkChars.Length);

    }
  }

}

namespace SageProofOfConcept
{
  class ShowInvariant
  {
    // Prints all the state
    static public void Here() { }

    // Projects the state
    static public void Here<T>(T x0) { }
    static public void Here<T>(T x0, T x1) { }
    static public void Here<T>(T x0, T x1, T x2) { }
    static public void Here<T>(T x0, T x1, T x2, T x3) { }
    static public void Here<T>(T x0, T x1, T x2, T x3, T x4) { }
    static public void Here<T>(T x0, T x1, T x2, T x3, T x4, T x5) { }

  }
}

#endif