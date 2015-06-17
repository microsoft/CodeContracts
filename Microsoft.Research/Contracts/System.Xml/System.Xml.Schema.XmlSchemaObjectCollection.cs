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
using System.Collections;
using System.Reflection;

namespace System.Xml.Schema
{
#if !SILVERLIGHT
  // Summary:
  //     A collection of System.Xml.Schema.XmlSchemaObjects.
  public class XmlSchemaObjectCollection // : CollectionBase
  {
    // Summary:
    //     Initializes a new instance of the XmlSchemaObjectCollection class.
    //public XmlSchemaObjectCollection();
    //
    // Summary:
    //     Initializes a new instance of the XmlSchemaObjectCollection class that takes
    //     an System.Xml.Schema.XmlSchemaObject.
    //
    // Parameters:
    //   parent:
    //     The System.Xml.Schema.XmlSchemaObject.
    //public XmlSchemaObjectCollection(XmlSchemaObject parent);

    // Summary:
    //     Gets the System.Xml.Schema.XmlSchemaObject at the specified index.
    //
    // Parameters:
    //   index:
    //     The index of the System.Xml.Schema.XmlSchemaObject.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchemaObject at the specified index.
    //public virtual XmlSchemaObject this[int index] { get; set; }

    // Summary:
    //     Adds an System.Xml.Schema.XmlSchemaObject to the XmlSchemaObjectCollection.
    //
    // Parameters:
    //   item:
    //     The System.Xml.Schema.XmlSchemaObject.
    //
    // Returns:
    //     The index at which the item has been added.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.  -or- index is greater than Count.
    //
    //   System.InvalidCastException:
    //     The System.Xml.Schema.XmlSchemaObject parameter specified is not of type
    //     System.Xml.Schema.XmlSchemaExternal or its derived types System.Xml.Schema.XmlSchemaImport,
    //     System.Xml.Schema.XmlSchemaInclude, and System.Xml.Schema.XmlSchemaRedefine.
    //public int Add(XmlSchemaObject item);
    //
    // Summary:
    //     Indicates if the specified System.Xml.Schema.XmlSchemaObject is in the XmlSchemaObjectCollection.
    //
    // Parameters:
    //   item:
    //     The System.Xml.Schema.XmlSchemaObject.
    //
    // Returns:
    //     true if the specified qualified name is in the collection; otherwise, returns
    //     false. If null is supplied, false is returned because there is no qualified
    //     name with a null name.
    //public bool Contains(XmlSchemaObject item);
    //
    // Summary:
    //     Copies all the System.Xml.Schema.XmlSchemaObjects from the collection into
    //     the given array, starting at the given index.
    //
    // Parameters:
    //   array:
    //     The one-dimensional array that is the destination of the elements copied
    //     from the XmlSchemaObjectCollection. The array must have zero-based indexing.
    //
    //   index:
    //     The zero-based index in the array at which copying begins.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     array is a null reference (Nothing in Visual Basic).
    //
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.
    //
    //   System.ArgumentException:
    //     array is multi-dimensional.  - or - index is equal to or greater than the
    //     length of array.  - or - The number of elements in the source System.Xml.Schema.XmlSchemaObject
    //     is greater than the available space from index to the end of the destination
    //     array.
    //
    //   System.InvalidCastException:
    //     The type of the source System.Xml.Schema.XmlSchemaObject cannot be cast automatically
    //     to the type of the destination array.
    //public void CopyTo(XmlSchemaObject[] array, int index);
    //
    // Summary:
    //     Returns an enumerator for iterating through the XmlSchemaObjects contained
    //     in the XmlSchemaObjectCollection.
    //
    // Returns:
    //     The iterator returns System.Xml.Schema.XmlSchemaObjectEnumerator.
    //public XmlSchemaObjectEnumerator GetEnumerator();
    //
    // Summary:
    //     Gets the collection index corresponding to the specified System.Xml.Schema.XmlSchemaObject.
    //
    // Parameters:
    //   item:
    //     The System.Xml.Schema.XmlSchemaObject whose index you want to return.
    //
    // Returns:
    //     The index corresponding to the specified System.Xml.Schema.XmlSchemaObject.
    //public int IndexOf(XmlSchemaObject item);
    //
    // Summary:
    //     Inserts an System.Xml.Schema.XmlSchemaObject to the XmlSchemaObjectCollection.
    //
    // Parameters:
    //   index:
    //     The zero-based index at which an item should be inserted.
    //
    //   item:
    //     The System.Xml.Schema.XmlSchemaObject to insert.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than zero.  -or- index is greater than Count.
    //public void Insert(int index, XmlSchemaObject item);
    //
    // Summary:
    //     Removes an System.Xml.Schema.XmlSchemaObject from the XmlSchemaObjectCollection.
    //
    // Parameters:
    //   item:
    //     The System.Xml.Schema.XmlSchemaObject to remove.
    //public void Remove(XmlSchemaObject item);
  }
#endif
}
