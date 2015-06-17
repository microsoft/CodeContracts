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
using System.Runtime.InteropServices;

namespace System.Collections.Generic
{
  // Summary:
  //     Represents a node in a System.Collections.Generic.LinkedList<T>. This class
  //     cannot be inherited.
  //
  // Type parameters:
  //   T:
  //     Specifies the element type of the linked list.
  //[ComVisible(false)]
  public sealed class LinkedListNode<T>
  {
    // Summary:
    //     Initializes a new instance of the System.Collections.Generic.LinkedListNode<T>
    //     class, containing the specified value.
    //
    // Parameters:
    //   value:
    //     The value to contain in the System.Collections.Generic.LinkedListNode<T>.
    //public LinkedListNode(T value);

    // Summary:
    //     Gets the System.Collections.Generic.LinkedList<T> that the System.Collections.Generic.LinkedListNode<T>
    //     belongs to.
    //
    // Returns:
    //     A reference to the System.Collections.Generic.LinkedList<T> that the System.Collections.Generic.LinkedListNode<T>
    //     belongs to, or null if the System.Collections.Generic.LinkedListNode<T> is
    //     not linked.
    //public LinkedList<T> List { get; }
    //
    // Summary:
    //     Gets the next node in the System.Collections.Generic.LinkedList<T>.
    //
    // Returns:
    //     A reference to the next node in the System.Collections.Generic.LinkedList<T>,
    //     or null if the current node is the last element (System.Collections.Generic.LinkedList<T>.Last)
    //     of the System.Collections.Generic.LinkedList<T>.
    //public LinkedListNode<T> Next { get; }
    //
    // Summary:
    //     Gets the previous node in the System.Collections.Generic.LinkedList<T>.
    //
    // Returns:
    //     A reference to the previous node in the System.Collections.Generic.LinkedList<T>,
    //     or null if the current node is the first element (System.Collections.Generic.LinkedList<T>.First)
    //     of the System.Collections.Generic.LinkedList<T>.
    //public LinkedListNode<T> Previous { get; }
    //
    // Summary:
    //     Gets the value contained in the node.
    //
    // Returns:
    //     The value contained in the node.
    //public T Value { get; set; }
  }
}