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

using Microsoft.Research.ClousotRegression;
using System;
// Taken from the mscorlib.dll sources



// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==


/*=============================================================================
**
** Class: Stack
**
** <OWNER>kimhamil</OWNER>
**
** Purpose: An array implementation of a stack.
**
**
=============================================================================*/
namespace System.Collections.Francesco
{
  using System;
  using System.Security.Permissions;
  using System.Diagnostics;
  using System.Diagnostics.Contracts;
  using Microsoft.Research.ClousotRegression;

  // A simple stack of objects.  Internally it is implemented as an array,
  // so Push can be O(n).  Pop is O(1).
  [System.Runtime.InteropServices.ComVisible(true)]
  [Serializable]
  public class Stack : ICloneable
  {
    private Object[] _array;     // Storage for stack elements
    private int _size;           // Number of items in the stack.
    private int _version;        // Used to keep enumerator in sync w/ collection.
    [NonSerialized]
    private Object _syncRoot;

    private const int _defaultCapacity = 10;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      // The invariant below is *just* commented for the regression test of numerical values
      // Contract.Invariant(_array != null);
      
      Contract.Invariant(_array.Length > 0);
      Contract.Invariant(_size >= 0);
      Contract.Invariant(_size <= _array.Length);
    }

    // The warning is ok: Cannot prove (of course...) that _array.Length > 0
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="invariant unreachable",PrimaryILOffset=12,MethodILOffset=9)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="invariant unreachable",PrimaryILOffset=30,MethodILOffset=9)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="invariant unreachable",PrimaryILOffset=55,MethodILOffset=9)]
    // We now detect early that because array == null, we never get to the invariant checks.
    //    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"invariant unproven: _array.Length > 0", PrimaryILOffset = 12, MethodILOffset = 9)]
    //    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 9)]
    //    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 9)]
    public Stack()
    {
    }

    // Create a stack with a specific initial capacity.  The initial capacity
    // must be a non-negative number.

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 63, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 22, MethodILOffset = 88)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 88)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 88)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 88)]
    public Stack(int initialCapacity)
    {
      Contract.Ensures(this._array.Length >= initialCapacity);

      if (initialCapacity < 0)
        throw new Exception();
      
      if (initialCapacity < _defaultCapacity)
        initialCapacity = _defaultCapacity;  // Simplify doubling logic in Push.
      
      _array = new Object[initialCapacity];
      _size = 0;
      _version = 0;
    }

    // Fills a Stack with the contents of a particular collection.  The items are
    // pushed onto the stack in the same order they are read by the enumerator.

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 76)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 76)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 76)]
    public Stack(ICollection col)
      : this((col == null ? 32 : col.Count))
    {
      if (col == null)
        throw new ArgumentNullException("col");
      IEnumerator en = col.GetEnumerator();
      
      while (en.MoveNext())
        Push(en.Current);
    }

    
    public virtual int Count
    {
      [ClousotRegressionTest]
      get { return _size; }
    }

    
    public virtual bool IsSynchronized
    {
      [ClousotRegressionTest]
      get { return false; }
    }

    
    public virtual Object SyncRoot
    {
      [ClousotRegressionTest]
      get
      {
        if (_syncRoot == null)
        {
          System.Threading.Interlocked.CompareExchange<Object>(ref _syncRoot, new Object(), null);
        }
        return _syncRoot;
      }
    }

    // Removes all Objects from the Stack.
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 41)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 41)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 41)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 14)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 14)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 64, MethodILOffset = 14)]
    public virtual void Clear()
    {
      Array.Clear(_array, 0, _size); // Don't need to doc this but we clear the elements so that the gc can reclaim the references.
      _size = 0;
      _version++;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 45)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 45)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 55, MethodILOffset = 45)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 79, MethodILOffset = 45)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 98, MethodILOffset = 45)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 132, MethodILOffset = 45)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 166, MethodILOffset = 45)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 17, MethodILOffset = 68)]
    public virtual Object Clone()
    {
      // Contract.Ensures(Contract.Result<Object>() != null);

      Stack s = new Stack(_size);

      s._size = _size;
      Array.Copy(_array, 0, s._array, 0, _size);
      s._version = _version;
      
      return s;
    }


    // Wrong: should be able to prove it!
    [ClousotRegressionTest]
    [Pure]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 55, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 55, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 65, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 65, MethodILOffset = 0)]
    public virtual bool Contains(Object obj)
    {
      int count = _size;

      while (count-- > 0)
      {
        if (obj == null)
        {
          if (_array[count] == null)
            return true;
        }
        else if (_array[count] != null && _array[count].Equals(obj))
        {
          return true;
        }
      }
      return false;
    }

    // Copies the stack into an array.
    // main feature : array access objArray[i + index] using slack variables
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 184, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 184, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 185, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 185, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 245, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 245, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 126, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 155, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 274)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 274)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 274)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 222, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 19, MethodILOffset = 249)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 43, MethodILOffset = 249)]
    public virtual void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException("array");
      if (array.Rank != 1)
        throw new Exception();
      if (index < 0)
        throw new Exception();
      if (index + _size > array.Length)
        throw new Exception();
      // Contract.Assert(index + _size <= array.Length);

      int i = 0;
      if (array is Object[])
      {
        Contract.Assert(index + _size <= array.Length);
        Object[] objArray = (Object[])array;
        Contract.Assert(index + _size <= objArray.Length);
        while (i < _size)
        {
          objArray[i + index] = _array[_size - i - 1];
          i++;
        }
      }
      else
      {
        while (i < _size)
        {
          Contract.Assert(i + index < array.Length);

          array.SetValue(_array[_size - i - 1], i + index);
          i++;
        }
      }
    }

    // Returns an IEnumerator for this Stack.
    [ClousotRegressionTest]
    [Pure]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 27, MethodILOffset = 2)]
    public virtual IEnumerator GetEnumerator()
    {
      return new StackEnumerator(this);
    }

    // Returns the top object on the stack without removing it.  If the stack
    // is empty, Peek throws an InvalidOperationException.
    [Pure]
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 37, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 37, MethodILOffset = 0)]
    public virtual Object Peek()
    {
      if (_size == 0)
        throw new Exception();
      return _array[_size - 1];
    }

    // Pops an item from the top of the stack.  If the stack is empty, Pop
    // throws an InvalidOperationException.
    [ClousotRegressionTest]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 60, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 60, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 75, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 75, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 81)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 81)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 81)]
    public virtual Object Pop()
    {
      if (_size == 0)
        throw new Exception();
      _version++;
      Object obj = _array[--_size];
      _array[_size] = null;     // Free memory quicker.
      return obj;
    }

    // Wrong: should be able to prove it!

    // Pushes an item to the top of the stack.
    // F: Main feature : array creation with multiplication
    [ClousotRegressionTest]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 116, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 116, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 85, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 131)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 131)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 131)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 55, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 79, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 98, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 132, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 166, MethodILOffset = 56)]
    public virtual void Push(Object obj)
    {
      if (_size == _array.Length)
      {
        Object[] newArray = new Object[2 * _array.Length];
        Array.Copy(_array, 0, newArray, 0, _size);

        _array = newArray;
        Contract.Assert(_size < _array.Length);
      }
      //{
      //  Contract.Assert(_size < _array.Length);
      //}
      _array[_size++] = obj;
      _version++;
    }

    // Returns a synchronized Stack.
    //
    [HostProtection(Synchronization = true)]
    [ClousotRegressionTest]
    public static Stack Synchronized(Stack stack)
    {
      if (stack == null)
        throw new Exception();

      return new SyncStack(stack);
    }


    // Copies the Stack to an array, in the same order Pop would return the items.
    [ClousotRegressionTest] // main feature : array access with two variables
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 37, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 37, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 61)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 61)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 61)]
    public virtual Object[] ToArray()
    {
      Object[] objArray = new Object[_size];
      int i = 0;
      while (i < _size)
      {
        objArray[i] = _array[_size - i - 1];
        i++;
      }
      return objArray;
    }

    [Serializable]
    private class SyncStack : Stack
    {
      private Stack _s;
      private Object _root;

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 28)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 28)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 28)]
      internal SyncStack(Stack stack)
      {
        // Contract.Requires(stack != null);
        _s = stack;
        _root = stack.SyncRoot;
      }

      [ContractInvariantMethod]
      private new void ObjectInvariant()
      {
        // Contract.Invariant(_s != null);
      }

      public override bool IsSynchronized
      {
        [ClousotRegressionTest]
        get { return true; }
      }

      public override Object SyncRoot
      {
        [ClousotRegressionTest]
        get
        {
          return _root;
        }
      }

      public override int Count
      {
        [ClousotRegressionTest]
        get
        {
          lock (_root)
          {
            return _s.Count;
          }
        }
      }

      [ClousotRegressionTest]
      public override bool Contains(Object obj)
      {
        lock (_root)
        {
          return _s.Contains(obj);
        }
      }

      [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=17,MethodILOffset=62)]
#else
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 17, MethodILOffset = 50)]
#endif
      public override Object Clone()
      {
        lock (_root)
        {
          return new SyncStack((Stack)_s.Clone());
        }
      }

      [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=12,MethodILOffset=52)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=30,MethodILOffset=52)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=55,MethodILOffset=52)]
#else
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 40)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 40)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 40)]
#endif
      public override void Clear()
      {
        lock (_root)
        {
          _s.Clear();
        }
      }

      [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=12,MethodILOffset=54)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=30,MethodILOffset=54)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=55,MethodILOffset=54)]
#else
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 42)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 42)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 42)]
#endif
      public override void CopyTo(Array array, int arrayIndex)
      {
        lock (_root)
        {
          _s.CopyTo(array, arrayIndex);
        }
      }

      [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=12,MethodILOffset=53)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=30,MethodILOffset=53)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=55,MethodILOffset=53)]
#else
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 41)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 41)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 41)]
#endif
      public override void Push(Object value)
      {
        lock (_root)
        {
          _s.Push(value);
        }
      }

      [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=12,MethodILOffset=52)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=30,MethodILOffset=52)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=55,MethodILOffset=52)]
#else
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 40)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 40)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 40)]
#endif
      public override Object Pop()
      {
        lock (_root)
        {
          return _s.Pop();
        }
      }

      [ClousotRegressionTest]
      public override IEnumerator GetEnumerator()
      {
        lock (_root)
        {
          return _s.GetEnumerator();
        }
      }

      [ClousotRegressionTest]
      public override Object Peek()
      {
        lock (_root)
        {
          return _s.Peek();
        }
      }

      [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=12,MethodILOffset=52)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=30,MethodILOffset=52)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=55,MethodILOffset=52)]
#else
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 40)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 30, MethodILOffset = 40)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 55, MethodILOffset = 40)]
#endif
      public override Object[] ToArray()
      {
        lock (_root)
        {
          return _s.ToArray();
        }
      }
    }


    [Serializable]
    private class StackEnumerator : IEnumerator, ICloneable
    {
      private Stack _stack;
      private int _index;
      private int _version;
      private Object currentElement;

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        // Contract.Invariant(_stack != null);
        // Contract.Invariant(_stack._array != null);

        Contract.Invariant(_index < _stack._array.Length);
        Contract.Invariant(_stack._size <= _stack._array.Length);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 22, MethodILOffset = 73)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 57, MethodILOffset = 73)]
      internal StackEnumerator(Stack stack)
      {
        // Contract.Requires(stack != null);
        // Contract.Requires(stack._array != null);
        Contract.Requires(stack._size <= stack._array.Length);

        _stack = stack;
        _version = _stack._version;
        _index = -2;
        currentElement = null;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 17, MethodILOffset = 11)]
      public Object Clone()
      {
        return MemberwiseClone();
      }

      [ClousotRegressionTest] // main feature : a difficult invariant proof
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 162, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 162, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 301, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 301, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 22, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 73, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)190, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 241, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 337, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 22, MethodILOffset = 348)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 57, MethodILOffset = 348)]
      public virtual bool MoveNext()
      {
        Contract.Assert(_index < _stack._array.Length);
        
        bool retval;
        if (_version != _stack._version)
        {
          Contract.Assert(_index < _stack._array.Length);

          throw new Exception();
        }

        if (_index == -2)
        {  // First call to enumerator.
          _index = _stack._size - 1;
          retval = (_index >= 0);

          if (retval)
          {
            currentElement = _stack._array[_index];
          }

          Contract.Assert(_index < _stack._array.Length);
          
          return retval;
        }
        if (_index == -1)
        {  // End of enumeration.
          
          Contract.Assert(_index < _stack._array.Length);
          
          return false;
        }

        retval = (--_index >= 0);
        if (retval)
          currentElement = _stack._array[_index];
        else
          currentElement = null;
        
        Contract.Assert(_index < _stack._array.Length);
      
        return retval;
      }

      
      public virtual Object Current
      {
        [ClousotRegressionTest]
        get
        {
          if (_index == -2)
            throw new Exception();

          if (_index == -1)
            throw new Exception();

          return currentElement;
        }
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 22, MethodILOffset = 45)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 57, MethodILOffset = 45)]
      public virtual void Reset()
      {
        if (_version != _stack._version)
          throw new Exception();

        _index = -2;
        currentElement = null;
      }
    }

    internal class StackDebugView
    {
      private Stack stack;

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        // Contract.Invariant(stack != null);
      }

      [ClousotRegressionTest]
      public StackDebugView(Stack stack)
      {
        if (stack == null)
          throw new ArgumentNullException("stack");

        this.stack = stack;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public Object[] Items
      {
        [ClousotRegressionTest]
        get
        {
          return stack.ToArray();
        }
      }
    }
  }
}


