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
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics.Contracts;

namespace System.Xml.Linq
{
  // Summary:
  //     Represents a node or an attribute in an XML tree.
  public abstract class XObject //: IXmlLineInfo
  {
    extern internal XObject();
    // Summary:
    //     Gets the base URI for this System.Xml.Linq.XObject.
    //
    // Returns:
    //     A System.String that contains the base URI for this System.Xml.Linq.XObject.
    extern public string BaseUri { get; }
    //
    // Summary:
    //     Gets the System.Xml.Linq.XDocument for this System.Xml.Linq.XObject.
    //
    // Returns:
    //     The System.Xml.Linq.XDocument for this System.Xml.Linq.XObject.
    extern public XDocument Document { get; }
    //
    // Summary:
    //     Gets the node type for this System.Xml.Linq.XObject.
    //
    // Returns:
    //     The node type for this System.Xml.Linq.XObject.
    extern public virtual XmlNodeType NodeType { get; }
    //
    // Summary:
    //     Gets the parent System.Xml.Linq.XElement of this System.Xml.Linq.XObject.
    //
    // Returns:
    //     The parent System.Xml.Linq.XElement of this System.Xml.Linq.XObject.
    extern public XElement Parent { get; }

    // Summary:
    //     Raised when this System.Xml.Linq.XObject or any of its descendants have changed.
    // public event EventHandler<XObjectChangeEventArgs> Changed;
    //
    // Summary:
    //     Raised when this System.Xml.Linq.XObject or any of its descendants are about
    //     to change.
    // public event EventHandler<XObjectChangeEventArgs> Changing;

    // Summary:
    //     Adds an object to the annotation list of this System.Xml.Linq.XObject.
    //
    // Parameters:
    //   annotation:
    //     An System.Object that contains the annotation to add.
    public void AddAnnotation(object annotation)
    {
      Contract.Requires(annotation != null);
    }
    //
    // Summary:
    //     Get the first annotation object of the specified type from this System.Xml.Linq.XObject.
    //
    // Type parameters:
    //   T:
    //     The type of the annotation to retrieve.
    //
    // Returns:
    //     The first annotation object that matches the specified type, or null if no
    //     annotation is of the specified type.
    [Pure]
    extern public T Annotation<T>() where T : class;
    //
    // Summary:
    //     Gets the first annotation object of the specified type from this System.Xml.Linq.XObject.
    //
    // Parameters:
    //   type:
    //     The System.Type of the annotation to retrieve.
    //
    // Returns:
    //     The System.Object that contains the first annotation object that matches
    //     the specified type, or null if no annotation is of the specified type.
    [Pure]
    public object Annotation(Type type)
    {
      Contract.Requires(type != null);
      return default(object);
    }
    //
    // Summary:
    //     Gets a collection of annotations of the specified type for this System.Xml.Linq.XObject.
    //
    // Type parameters:
    //   T:
    //     The type of the annotations to retrieve.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> that contains the annotations
    //     for this System.Xml.Linq.XObject.
    [Pure]
    public IEnumerable<T> Annotations<T>() where T : class
    {
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
      return default(IEnumerable<T>);
    }
    //
    // Summary:
    //     Gets a collection of annotations of the specified type for this System.Xml.Linq.XObject.
    //
    // Parameters:
    //   type:
    //     The System.Type of the annotations to retrieve.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Object that contains
    //     the annotations that match the specified type for this System.Xml.Linq.XObject.
    [Pure]
    public IEnumerable<object> Annotations(Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);
      return default(IEnumerable<object>);
    }
    //
    // Summary:
    //     Removes the annotations of the specified type from this System.Xml.Linq.XObject.
    //
    // Type parameters:
    //   T:
    //     The type of annotations to remove.
    extern public void RemoveAnnotations<T>() where T : class;
    //
    // Summary:
    //     Removes the annotations of the specified type from this System.Xml.Linq.XObject.
    //
    // Parameters:
    //   type:
    //     The System.Type of annotations to remove.
    public void RemoveAnnotations(Type type)
    {
      Contract.Requires(type != null);
    }
  }
}
