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

using System.Collections;
using System.Diagnostics.Contracts;
namespace Microsoft.VisualBasic
{
  // Summary:
  //     A Visual Basic Collection is an ordered set of items that can be referred
  //     to as a unit.
  //[Serializable]
  //[DebuggerDisplay("Count = {Count}")]
  public sealed class Collection 
  {
    // Summary:
    //     Creates and returns a new Visual Basic Collection Object.
    //public Collection() { }

    // Summary:
    //     Returns an Integer containing the number of elements in a collection. Read-only.
    //
    // Returns:
    //     Returns an Integer containing the number of elements in a collection. Read-only.
    //public int Count { get; }

    // Summary:
    //     Returns a specific element of a Collection object either by position or by
    //     key. Read-only.
    //
    // Parameters:
    //   Index:
    //     (A) A numeric expression that specifies the position of an element of the
    //     collection. Index must be a number from 1 through the value of the collection's
    //     Count Property (Collection Object). Or (B) An Object expression that specifies
    //     the position or key string of an element of the collection.
    //
    // Returns:
    //     Returns a specific element of a Collection object either by position or by
    //     key. Read-only.
    //public object this[int Index] { get; }
    //
    // Summary:
    //     Returns a specific element of a Collection object either by position or by
    //     key. Read-only.
    //
    // Parameters:
    //   Index:
    //     (A) A numeric expression that specifies the position of an element of the
    //     collection. Index must be a number from 1 through the value of the collection's
    //     Count Property (Collection Object). Or (B) An Object expression that specifies
    //     the position or key string of an element of the collection.
    //
    // Returns:
    //     Returns a specific element of a Collection object either by position or by
    //     key. Read-only.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public object this[object Index] { get; }
    //
    // Summary:
    //     Returns a specific element of a Collection object either by position or by
    //     key. Read-only.
    //
    // Parameters:
    //   Key:
    //     A unique String expression that specifies a key string that can be used,
    //     instead of a positional index, to access an element of the collection. Key
    //     must correspond to the Key argument specified when the element was added
    //     to the collection.
    //
    // Returns:
    //     Returns a specific element of a Collection object either by position or by
    //     key. Read-only.
    //public object this[string Key] { get; }

    // Summary:
    //     Adds an element to a Collection object.
    //
    // Parameters:
    //   Item:
    //     Required. An object of any type that specifies the element to add to the
    //     collection.
    //
    //   Key:
    //     Optional. A unique String expression that specifies a key string that can
    //     be used instead of a positional index to access this new element in the collection.
    //
    //   Before:
    //     Optional. An expression that specifies a relative position in the collection.
    //     The element to be added is placed in the collection before the element identified
    //     by the Before argument. If Before is a numeric expression, it must be a number
    //     from 1 through the value of the collection's Count Property (Collection Object).
    //     If Before is a String expression, it must correspond to the key string specified
    //     when the element being referred to was added to the collection. You cannot
    //     specify both Before and After.
    //
    //   After:
    //     Optional. An expression that specifies a relative position in the collection.
    //     The element to be added is placed in the collection after the element identified
    //     by the After argument. If After is a numeric expression, it must be a number
    //     from 1 through the value of the collection's Count property. If After is
    //     a String expression, it must correspond to the key string specified when
    //     the element referred to was added to the collection. You cannot specify both
    //     Before and After.
    //public void Add(object Item, string Key, object Before, object After)

    // Summary:
    //     Returns a reference to an enumerator object, which is used to iterate over
    //     a Collection Object (Visual Basic).
    //
    // Returns:
    //     Returns a reference to an enumerator object, which is used to iterate over
    //     a Collection Object (Visual Basic).
    public IEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<IEnumerator>() != null);

      return default(IEnumerator);
    }
   
  }
}
