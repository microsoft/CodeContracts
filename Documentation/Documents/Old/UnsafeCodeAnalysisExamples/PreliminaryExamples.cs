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

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Contracts;

//This file contains three examples about the use of pointer and what it is necessary to add in the pre and post condition of methods
namespace Examples
{

  #region Example n.1
  unsafe class StringBuilder
  {
    public int m_ChunkLength;                  // The index in m_BlockChars that repsent the end of the block
    public char[] m_ChunkChars;                // The characters in this block
    public int m_MaxCapacity;                  // Limits the size of the stringbuilder (ideally we would remove this).   
    private const int DefaultCapacity = 16;

    unsafe public StringBuilder(String value, int startIndex, int length, int capacity)
    {
      if (capacity < 0)
      {
        throw new Exception("capacity");
      }
      if (capacity == 0)
      {
        capacity = DefaultCapacity;
      }
      //Ensure that capacity > 0

      if (length < 0)
      {
        throw new Exception("length");
      }
      if (startIndex < 0)
      {
        throw new Exception("startIndex");
      }
      if (value == null)
      {
        value = String.Empty;
      }
      //length >= 0 && startIndex >= 0 && value != null


      if (startIndex > value.Length - length)
      {
        throw new Exception("length");
      }
      if (capacity < length)
        capacity = length;
      //length <= capacity && length <= startIndex + value.Length


      m_ChunkChars = new char[capacity];
      //m_ChunkChars.Length = capacity

      m_ChunkLength = length;
      //m_ChunkLength >= capacity && m_ChunkLength <= startIndex + value.Length && m_ChunkLength >= 0

      m_MaxCapacity = int.MaxValue;

      fixed (char* sourcePtr = value) //sourcePtr.AllocatedUntil = value.Length
        ThreadSafeCopy(sourcePtr + startIndex, m_ChunkChars, 0, length);
      //In this invocation the following relations between parameters hold:
      //- m_ChunkLength <= startIndex + sourcePtr.AllocatedUntil && m_ChunkLength = length  ->  length <= startIndex + sourcePtr.AllocatedUntil
      //- m_ChunkChars.Length=capacity && capacity >= length  ->  m_ChunkChars.Length >= length

      //(4) It had to ensure that:
      //- m_ChunkChars.Length-0 >= length (by 3.a)
      //- sourcePtr.AllocatedUntil + startIndex >= length (by 3.b)
      //It holds!
    }

    unsafe private static void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
    {
      Contract.Requires(Contract.WritableBytes(sourcePtr) > (ulong)count);
      Contract.Required(destination.Length > (ulong)(count + destinationIndex));
      
      //(3) Requires that (a) destination.Length-destinationIndex>= count (by (2)) && (b) sourcePtr.AllocatedUntil >= count (by (1.b))
      if (count > 0)
      {
        if ((uint)destinationIndex <= (uint)destination.Length && (destinationIndex + count) <= destination.Length)
        {
          fixed (char* destinationPtr = &destination[destinationIndex]) //destinationPtr.AllocatedUntil = destination.Length-destinationIndex
            //(2) It should ensures that destination.Length-destinationIndex>= count


            //(1) It should ensures that (a) destinationPtr.AllocatedUntil >= count && (b) sourcePtr.AllocatedUntil >= count
            CopyChars(destinationPtr, sourcePtr, count);
        }
        else
        {
          BCLDebug.Assert(false, "Thread safety check failed");
          throw new Exception("Thread safety exception");
        }
      }
    }

    unsafe private static void CopyChars(char* dest, char* source, int count)
    {
      Contract.Requires(Contract.WritableBytes(dest) > (ulong)count);
      Contract.Requires(Contract.WritableBytes(source) > (ulong)count);

      //Requires dest.AllocatedUntil >= count && source.AllocatedUntil >= count
      for (int i = 0; i < count; i++)
      {
        dest[i] = source[i];
      }
    }
  }
  #endregion

  #region Example n.2
  public class Buffer
  {
    internal static unsafe int IndexOfByte(byte* src, byte value, int index, int count)
    {//Requires src.AllocatedUntil>=count+index
      byte* numPtr = src + index;//numPtr.AllocatedUntil = src.AllocatedUntil-index -> src.AllocatedUntil-index>= count by (1/2/3)
      while ((((int)numPtr) & 3) != 0)
      {
        if (count == 0)
        {
          return -1;
        }
        if (numPtr[0] == value)
        {
          return (int)((long)((numPtr - src) / 1));
        }
        count--;
        numPtr++;
      }//(3) Require numPtr.AllocatedUntil >= count
      uint num = (uint)((value << 8) + value);
      num = (num << 0x10) + num;
      while (count > 3)
      {
        uint num2 = *((uint*)numPtr);
        num2 ^= num;
        uint num3 = 0x7efefeff + num2;
        num2 ^= uint.MaxValue;
        num2 ^= num3;
        if ((num2 & 0x81010100) != 0)
        {
          int num4 = (int)((long)((numPtr - src) / 1));
          if (numPtr[0] == value)
          {
            return num4;
          }
          if (numPtr[1] == value)
          {
            return (num4 + 1);
          }
          if (numPtr[2] == value)
          {
            return (num4 + 2);
          }
          if (numPtr[3] == value)
          {
            return (num4 + 3);
          }
        }
        count -= 4;
        numPtr += 4;
      }//(2) Require numPtr.AllocatedUntil >= count
      while (count > 0)
      {
        if (numPtr[0] == value)
        {
          return (int)((long)((numPtr - src) / 1));
        }
        count--;
        numPtr++;
      }//(1) Require numPtr.AllocatedUntil >= count
      return -1;
    }
  }

  internal class ByteEqualityComparer : EqualityComparer<byte>
  {
    internal override unsafe int IndexOf(byte[] array, byte value, int startIndex, int count)
    {
      if (array == null)
      {
        throw new ArgumentNullException("array");
      }
      if (startIndex < 0)
      {
        throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
      }
      if (count < 0)
      {
        throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
      }
      if (count > (array.Length - startIndex))
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
      }
      if (count == 0)
      {
        return -1;
      }
      //All these if statements guarantee that: array != null && count > 0 && count <= array.Length-startIndex (1)
      fixed (byte* numRef = array) //numRef.AllocatedUntil = array.Length (2)
      {
        //Ensures by (1) and (2) that numRef.AllocatedUntil >=count+startIndex
        return Buffer.IndexOfByte(numRef, value, startIndex, count);
      }
    }
  }

  #endregion

  #region Example n.3
  public class MemberInfoCache<T> where T : MemberInfo
  {


    private unsafe void PopulateEvents(RuntimeType.RuntimeTypeCache.Filter filter, RuntimeTypeHandle declaringTypeHandle, MetadataImport scope, int* tkAssociates, int cAssociates, Hashtable csEventInfos, List<RuntimeEventInfo> list)
    {
      //Require tkAssociates.AllocatedUntil >= cAssociates
      for (int i = 0; i < cAssociates; i++)
      {
        int mdToken = tkAssociates[i];
        Utf8String name = scope.GetName(mdToken);
        if (filter.Match(name))
        {
          bool flag;
          RuntimeEventInfo item = new RuntimeEventInfo(mdToken, declaringTypeHandle.GetRuntimeType(), this.m_runtimeTypeCache, out flag);
          if ((declaringTypeHandle.Equals(this.m_runtimeTypeCache.RuntimeTypeHandle) || !flag) && (csEventInfos[item.Name] == null))
          {
            csEventInfos[item.Name] = item;
            list.Add(item);
          }
        }
      }
    }

    private unsafe void PopulateEvents(RuntimeType.RuntimeTypeCache.Filter filter, RuntimeTypeHandle declaringTypeHandle, Hashtable csEventInfos, List<RuntimeEventInfo> list)
    {
      int token = declaringTypeHandle.GetToken();
      if (!MetadataToken.IsNullToken(token))
      {
        MetadataImport metadataImport = declaringTypeHandle.GetModuleHandle().GetMetadataImport();
        int count = metadataImport.EnumEventsCount(token);
        int* result = stackalloc int[count]; //result.AllocatedUntil = count
        metadataImport.EnumEvents(token, result, count);
        this.PopulateEvents(filter, declaringTypeHandle, metadataImport, result, count, csEventInfos, list);
        //Ensures that result.AllocatedUntil >= count!
      }
    }
  }

  #endregion

}

#endif