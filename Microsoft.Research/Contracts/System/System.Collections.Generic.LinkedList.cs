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


  namespace System.Collections.Generic
  {
    // Summary:
    //     Represents a doubly linked list.
    //
    // Type parameters:
    //   T:
    //     Specifies the element type of the linked list.
    // [Serializable]
    public class LinkedList<T> : ICollection<T> //, IEnumerable<T>, ICollection, IEnumerable, ISerializable, IDeserializationCallback
    {
      // Summary:
      //     Initializes a new instance of the System.Collections.Generic.LinkedList<T>
      //     class that is empty.
      public LinkedList()
      {
        Contract.Ensures(Count == 0);
      }
      //
      // Summary:
      //     Initializes a new instance of the System.Collections.Generic.LinkedList<T>
      //     class that contains elements copied from the specified System.Collections.IEnumerable
      //     and has sufficient capacity to accommodate the number of elements copied.
      //
      // Parameters:
      //   collection:
      //     The System.Collections.IEnumerable whose elements are copied to the new System.Collections.Generic.LinkedList<T>.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     collection is null.
      public LinkedList(IEnumerable<T> collection)
      {
        Contract.Requires(collection != null);
      }
      //
      // Summary:
      //     Initializes a new instance of the System.Collections.Generic.LinkedList<T>
      //     class that is serializable with the specified System.Runtime.Serialization.SerializationInfo
      //     and System.Runtime.Serialization.StreamingContext.
      //
      // Parameters:
      //   info:
      //     A System.Runtime.Serialization.SerializationInfo object containing the information
      //     required to serialize the System.Collections.Generic.LinkedList<T>.
      //
      //   context:
      //     A System.Runtime.Serialization.StreamingContext object containing the source
      //     and destination of the serialized stream associated with the System.Collections.Generic.LinkedList<T>.
      //protected LinkedList(SerializationInfo info, StreamingContext context);

      // Summary:
      //     Gets the number of nodes actually contained in the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     The number of nodes actually contained in the System.Collections.Generic.LinkedList<T>.
      extern public int Count
      {
        get;
      }
      //
      // Summary:
      //     Gets the first node of the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     The first System.Collections.Generic.LinkedListNode<T> of the System.Collections.Generic.LinkedList<T>.
      public LinkedListNode<T> First
      {
        get
        {
          Contract.Ensures(Count != 0 || Contract.Result<LinkedListNode<T>>() == null);
          Contract.Ensures(Count == 0 || Contract.Result<LinkedListNode<T>>() != null);

          return default(LinkedListNode<T>);
        }
      }
      //
      // Summary:
      //     Gets the last node of the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     The last System.Collections.Generic.LinkedListNode<T> of the System.Collections.Generic.LinkedList<T>.
      public LinkedListNode<T> Last
      {
        get
        {
          Contract.Ensures(Count != 0 || Contract.Result<LinkedListNode<T>>() == null);
          Contract.Ensures(Count == 0 || Contract.Result<LinkedListNode<T>>() != null);

          return default(LinkedListNode<T>);
        }
      }

      // Summary:
      //     Adds the specified new node after the specified existing node in the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   node:
      //     The System.Collections.Generic.LinkedListNode<T> after which to insert newNode.
      //
      //   newNode:
      //     The new System.Collections.Generic.LinkedListNode<T> to add to the System.Collections.Generic.LinkedList<T>.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     node is null.  -or- newNode is null.
      //
      //   System.InvalidOperationException:
      //     node is not in the current System.Collections.Generic.LinkedList<T>.  -or-
      //     newNode belongs to another System.Collections.Generic.LinkedList<T>.
      public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
      {
        Contract.Requires(node != null);
        Contract.Requires(newNode != null);

        Contract.Ensures(Count == Contract.OldValue(Count) +1);
      }
      //
      // Summary:
      //     Adds a new node containing the specified value after the specified existing
      //     node in the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   node:
      //     The System.Collections.Generic.LinkedListNode<T> after which to insert a
      //     new System.Collections.Generic.LinkedListNode<T> containing value.
      //
      //   value:
      //     The value to add to the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     The new System.Collections.Generic.LinkedListNode<T> containing value.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     node is null.
      //
      //   System.InvalidOperationException:
      //     node is not in the current System.Collections.Generic.LinkedList<T>.
      public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
      {
        Contract.Requires(node != null);
        Contract.Ensures(Contract.Result<LinkedListNode<T>>() != null);
        Contract.Ensures(Count == Contract.OldValue(Count) + 1);

        return default(LinkedListNode<T>);
      }
      //
      // Summary:
      //     Adds the specified new node before the specified existing node in the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   node:
      //     The System.Collections.Generic.LinkedListNode<T> before which to insert newNode.
      //
      //   newNode:
      //     The new System.Collections.Generic.LinkedListNode<T> to add to the System.Collections.Generic.LinkedList<T>.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     node is null.  -or- newNode is null.
      //
      //   System.InvalidOperationException:
      //     node is not in the current System.Collections.Generic.LinkedList<T>.  -or-
      //     newNode belongs to another System.Collections.Generic.LinkedList<T>.
      public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
      {
        Contract.Requires(node != null);
        Contract.Requires(newNode != null);

        Contract.Ensures(Count == Contract.OldValue(Count) + 1);
      }
      //
      // Summary:
      //     Adds a new node containing the specified value before the specified existing
      //     node in the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   node:
      //     The System.Collections.Generic.LinkedListNode<T> before which to insert a
      //     new System.Collections.Generic.LinkedListNode<T> containing value.
      //
      //   value:
      //     The value to add to the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     The new System.Collections.Generic.LinkedListNode<T> containing value.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     node is null.
      //
      //   System.InvalidOperationException:
      //     node is not in the current System.Collections.Generic.LinkedList<T>.
      public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
      {
        Contract.Requires(node != null);
        Contract.Ensures(Contract.Result<LinkedListNode<T>>() != null);
        Contract.Ensures(Count == Contract.OldValue(Count) + 1);

        return default(LinkedListNode<T>);
      }
      //
      // Summary:
      //     Adds the specified new node at the start of the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   node:
      //     The new System.Collections.Generic.LinkedListNode<T> to add at the start
      //     of the System.Collections.Generic.LinkedList<T>.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     node is null.
      //
      //   System.InvalidOperationException:
      //     node belongs to another System.Collections.Generic.LinkedList<T>.
      public void AddFirst(LinkedListNode<T> node)
      {
        Contract.Requires(node != null);

        Contract.Ensures(Count == Contract.OldValue(Count) + 1);
      }      
      
      //
      // Summary:
      //     Adds a new node containing the specified value at the start of the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   value:
      //     The value to add at the start of the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     The new System.Collections.Generic.LinkedListNode<T> containing value.
      public LinkedListNode<T> AddFirst(T value)
      {
        Contract.Ensures(Contract.Result<LinkedListNode<T>>() != null);
        Contract.Ensures(Count == Contract.OldValue(Count) + 1);

        return default(LinkedListNode<T>);
      }
      //
      // Summary:
      //     Adds the specified new node at the end of the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   node:
      //     The new System.Collections.Generic.LinkedListNode<T> to add at the end of
      //     the System.Collections.Generic.LinkedList<T>.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     node is null.
      //
      //   System.InvalidOperationException:
      //     node belongs to another System.Collections.Generic.LinkedList<T>.
      public void AddLast(LinkedListNode<T> node)
      {
        Contract.Requires(node != null);

        Contract.Ensures(Count == Contract.OldValue(Count) + 1);
      }
      //
      // Summary:
      //     Adds a new node containing the specified value at the end of the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   value:
      //     The value to add at the end of the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     The new System.Collections.Generic.LinkedListNode<T> containing value.
      public LinkedListNode<T> AddLast(T value)
      {
        Contract.Ensures(Contract.Result<LinkedListNode<T>>() != null);
        Contract.Ensures(Count == Contract.OldValue(Count) + 1);

        return default(LinkedListNode<T>);
      }
      //
      // Summary:
      //     Determines whether a value is in the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   value:
      //     The value to locate in the System.Collections.Generic.LinkedList<T>. The
      //     value can be null for reference types.
      //
      // Returns:
      //     true if value is found in the System.Collections.Generic.LinkedList<T>; otherwise,
      //     false.
      //public bool Contains(T value);
      //
      // Summary:
      //     Copies the entire System.Collections.Generic.LinkedList<T> to a compatible
      //     one-dimensional System.Array, starting at the specified index of the target
      //     array.
      //
      // Parameters:
      //   array:
      //     The one-dimensional System.Array that is the destination of the elements
      //     copied from System.Collections.Generic.LinkedList<T>. The System.Array must
      //     have zero-based indexing.
      //
      //   index:
      //     The zero-based index in array at which copying begins.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     array is null.
      //
      //   System.ArgumentOutOfRangeException:
      //     index is equal to or greater than the length of array.  -or- index is less
      //     than zero.
      //
      //   System.ArgumentException:
      //     The number of elements in the source System.Collections.Generic.LinkedList<T>
      //     is greater than the available space from index to the end of the destination
      //     array.
      extern public void CopyTo(T[] array, int index);

      //
      // Summary:
      //     Finds the first node that contains the specified value.
      //
      // Parameters:
      //   value:
      //     The value to locate in the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     The first System.Collections.Generic.LinkedListNode<T> that contains the
      //     specified value, if found; otherwise, null.
      //public LinkedListNode<T> Find(T value);
      //
      // Summary:
      //     Finds the last node that contains the specified value.
      //
      // Parameters:
      //   value:
      //     The value to locate in the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     The last System.Collections.Generic.LinkedListNode<T> that contains the specified
      //     value, if found; otherwise, null.
      //public LinkedListNode<T> FindLast(T value);
      //
      // Summary:
      //     Returns an enumerator that iterates through the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     An System.Collections.Generic.LinkedList<T>.Enumerator for the System.Collections.Generic.LinkedList<T>.
      //public LinkedList<T>.Enumerator GetEnumerator();
      //
      // Summary:
      //     Implements the System.Runtime.Serialization.ISerializable interface and returns
      //     the data needed to serialize the System.Collections.Generic.LinkedList<T>
      //     instance.
      //
      // Parameters:
      //   info:
      //     A System.Runtime.Serialization.SerializationInfo object that contains the
      //     information required to serialize the System.Collections.Generic.LinkedList<T>
      //     instance.
      //
      //   context:
      //     A System.Runtime.Serialization.StreamingContext object that contains the
      //     source and destination of the serialized stream associated with the System.Collections.Generic.LinkedList<T>
      //     instance.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     info is null.
      //public virtual void GetObjectData(SerializationInfo info, StreamingContext context);
      //
      // Summary:
      //     Implements the System.Runtime.Serialization.ISerializable interface and raises
      //     the deserialization event when the deserialization is complete.
      //
      // Parameters:
      //   sender:
      //     The source of the deserialization event.
      //
      // Exceptions:
      //   System.Runtime.Serialization.SerializationException:
      //     The System.Runtime.Serialization.SerializationInfo object associated with
      //     the current System.Collections.Generic.LinkedList<T> instance is invalid.
      //public virtual void OnDeserialization(object sender);
      //
      // Summary:
      //     Removes the specified node from the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   node:
      //     The System.Collections.Generic.LinkedListNode<T> to remove from the System.Collections.Generic.LinkedList<T>.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     node is null.
      //
      //   System.InvalidOperationException:
      //     node is not in the current System.Collections.Generic.LinkedList<T>.
      public void Remove(LinkedListNode<T> node)
      {
        Contract.Requires(node != null);

        Contract.Ensures(Count == Contract.OldValue(Count) -1);
      }
      //
      // Summary:
      //     Removes the first occurrence of the specified value from the System.Collections.Generic.LinkedList<T>.
      //
      // Parameters:
      //   value:
      //     The value to remove from the System.Collections.Generic.LinkedList<T>.
      //
      // Returns:
      //     true if the element containing value is successfully removed; otherwise,
      //     false. This method also returns false if value was not found in the original
      //     System.Collections.Generic.LinkedList<T>.
      virtual public bool Remove(T value)
      {
        Contract.Ensures(!Contract.Result<bool>() || Count == Contract.OldValue(Count) - 1);

        return default(bool);
      }
      //
      // Summary:
      //     Removes the node at the start of the System.Collections.Generic.LinkedList<T>.
      //
      // Exceptions:
      //   System.InvalidOperationException:
      //     The System.Collections.Generic.LinkedList<T> is empty.
      public void RemoveFirst()
      {
        Contract.Requires(Count > 0);

        Contract.Ensures(Count == Contract.OldValue(Count) - 1);
      }
      //
      // Summary:
      //     Removes the node at the end of the System.Collections.Generic.LinkedList<T>.
      //
      // Exceptions:
      //   System.InvalidOperationException:
      //     The System.Collections.Generic.LinkedList<T> is empty.
      public void RemoveLast()
      {
        Contract.Requires(Count > 0);

        Contract.Ensures(Count == Contract.OldValue(Count) -1);
      }

      // Summary:
      //     Enumerates the elements of a System.Collections.Generic.LinkedList<T>.
      //[Serializable]
      public struct Enumerator //: IEnumerator<T>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
      {

        // Summary:
        //     Gets the element at the current position of the enumerator.
        //
        // Returns:
        //     The element in the System.Collections.Generic.LinkedList<T> at the current
        //     position of the enumerator.
        //public T Current { get; }

        // Summary:
        //     Releases all resources used by the System.Collections.Generic.LinkedList<T>.Enumerator.
        //public void Dispose();
        //
        // Summary:
        //     Advances the enumerator to the next element of the System.Collections.Generic.LinkedList<T>.
        //
        // Returns:
        //     true if the enumerator was successfully advanced to the next element; false
        //     if the enumerator has passed the end of the collection.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The collection was modified after the enumerator was created.
        //public bool MoveNext();
      }

      #region ICollection<T> Members

      extern void ICollection<T>.Add(T item);
      extern public void Clear();
      extern public bool Contains(T item);
      extern bool ICollection<T>.IsReadOnly { get; }
      #endregion

      #region IEnumerable<T> Members

      extern IEnumerator<T> IEnumerable<T>.GetEnumerator();
      #endregion

      #region IEnumerable Members

      extern IEnumerator IEnumerable.GetEnumerator();
      #endregion
    }
  }
