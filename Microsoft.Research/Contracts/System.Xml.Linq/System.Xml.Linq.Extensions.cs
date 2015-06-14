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
using System.Runtime.CompilerServices;
using System.Diagnostics.Contracts;

namespace System.Xml.Linq
{
  // Summary:
  //     Contains the LINQ to XML extension methods.
  public static class Extensions
  {
    // Summary:
    //     Returns a collection of elements that contains the ancestors of every node
    //     in the source collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode that
    //     contains the source collection.
    //
    // Type parameters:
    //   T:
    //     The type of the objects in source, constrained to System.Xml.Linq.XNode.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the ancestors of every node in the source collection.
    public static IEnumerable<XElement> Ancestors<T>(this IEnumerable<T> source) where T : XNode
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

      return default(IEnumerable<XElement>);
    }
    //
    // Summary:
    //     Returns a filtered collection of elements that contains the ancestors of
    //     every node in the source collection. Only elements that have a matching System.Xml.Linq.XName
    //     are included in the collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode that
    //     contains the source collection.
    //
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Type parameters:
    //   T:
    //     The type of the objects in source, constrained to System.Xml.Linq.XNode.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the ancestors of every node in the source collection. Only
    //     elements that have a matching System.Xml.Linq.XName are included in the collection.
    public static IEnumerable<XElement> Ancestors<T>(this IEnumerable<T> source, XName name) where T : XNode
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

      return default(IEnumerable<XElement>);
    }
 
    //
    // Summary:
    //     Returns a collection of elements that contains every element in the source
    //     collection, and the ancestors of every element in the source collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the source collection.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains every element in the source collection, and the ancestors of
    //     every element in the source collection.
    public static IEnumerable<XElement> AncestorsAndSelf(this IEnumerable<XElement> source)
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

      return default(IEnumerable<XElement>);
    }
 
    //
    // Summary:
    //     Returns a filtered collection of elements that contains every element in
    //     the source collection, and the ancestors of every element in the source collection.
    //     Only elements that have a matching System.Xml.Linq.XName are included in
    //     the collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the source collection.
    //
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains every element in the source collection, and the ancestors of
    //     every element in the source collection. Only elements that have a matching
    //     System.Xml.Linq.XName are included in the collection.
    public static IEnumerable<XElement> AncestorsAndSelf(this IEnumerable<XElement> source, XName name)
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

      return default(IEnumerable<XElement>);
    }
 
    //
    // Summary:
    //     Returns a collection of the attributes of every element in the source collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the source collection.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XAttribute
    //     that contains the attributes of every element in the source collection.
    public static IEnumerable<XAttribute> Attributes(this IEnumerable<XElement> source)
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XAttribute>>() != null);

      return default(IEnumerable<XAttribute>);
    }
 
    //
    // Summary:
    //     Returns a filtered collection of the attributes of every element in the source
    //     collection. Only elements that have a matching System.Xml.Linq.XName are
    //     included in the collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the source collection.
    //
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XAttribute
    //     that contains a filtered collection of the attributes of every element in
    //     the source collection. Only elements that have a matching System.Xml.Linq.XName
    //     are included in the collection.
    public static IEnumerable<XAttribute> Attributes(this IEnumerable<XElement> source, XName name)
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XAttribute>>() != null);

      return default(IEnumerable<XAttribute>);
    }

    //
    // Summary:
    //     Returns a collection of the descendant nodes of every document and element
    //     in the source collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XContainer
    //     that contains the source collection.
    //
    // Type parameters:
    //   T:
    //     The type of the objects in source, constrained to System.Xml.Linq.XContainer.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode of
    //     the descendant nodes of every document and element in the source collection.
    public static IEnumerable<XNode> DescendantNodes<T>(this IEnumerable<T> source) where T : XContainer
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XNode>>() != null);

      return default(IEnumerable<XNode>);
    }
      
    //
    // Summary:
    //     Returns a collection of nodes that contains every element in the source collection,
    //     and the descendant nodes of every element in the source collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the source collection.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode that
    //     contains every element in the source collection, and the descendant nodes
    //     of every element in the source collection.
    public static IEnumerable<XNode> DescendantNodesAndSelf(this IEnumerable<XElement> source)
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XNode>>() != null);

      return default(IEnumerable<XNode>);
    }

    //
    // Summary:
    //     Returns a collection of elements that contains the descendant elements of
    //     every element and document in the source collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XContainer
    //     that contains the source collection.
    //
    // Type parameters:
    //   T:
    //     The type of the objects in source, constrained to System.Xml.Linq.XContainer.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the descendant elements of every element and document in the
    //     source collection.
    public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source) where T : XContainer
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

      return default(IEnumerable<XElement>);
    }

    //
    // Summary:
    //     Returns a filtered collection of elements that contains the descendant elements
    //     of every element and document in the source collection. Only elements that
    //     have a matching System.Xml.Linq.XName are included in the collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XContainer
    //     that contains the source collection.
    //
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Type parameters:
    //   T:
    //     The type of the objects in source, constrained to System.Xml.Linq.XContainer.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the descendant elements of every element and document in the
    //     source collection. Only elements that have a matching System.Xml.Linq.XName
    //     are included in the collection.
    public static IEnumerable<XElement> Descendants<T>(this IEnumerable<T> source, XName name) where T : XContainer
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

      return default(IEnumerable<XElement>);
    }

    //
    // Summary:
    //     Returns a collection of elements that contains every element in the source
    //     collection, and the descendent elements of every element in the source collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the source collection.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains every element in the source collection, and the descendent
    //     elements of every element in the source collection.
    public static IEnumerable<XElement> DescendantsAndSelf(this IEnumerable<XElement> source)
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

      return default(IEnumerable<XElement>);
    }

    //
    // Summary:
    //     Returns a filtered collection of elements that contains every element in
    //     the source collection, and the descendents of every element in the source
    //     collection. Only elements that have a matching System.Xml.Linq.XName are
    //     included in the collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the source collection.
    //
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains every element in the source collection, and the descendents
    //     of every element in the source collection. Only elements that have a matching
    //     System.Xml.Linq.XName are included in the collection.
    public static IEnumerable<XElement> DescendantsAndSelf(this IEnumerable<XElement> source, XName name)
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

      return default(IEnumerable<XElement>);
    }

    //
    // Summary:
    //     Returns a collection of the child elements of every element and document
    //     in the source collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the source collection.
    //
    // Type parameters:
    //   T:
    //     The type of the objects in source, constrained to System.Xml.Linq.XContainer.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     of the child elements of every element or document in the source collection.
    public static IEnumerable<XElement> Elements<T>(this IEnumerable<T> source) where T : XContainer
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

      return default(IEnumerable<XElement>);
    }

    //
    // Summary:
    //     Returns a filtered collection of the child elements of every element and
    //     document in the source collection. Only elements that have a matching System.Xml.Linq.XName
    //     are included in the collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     that contains the source collection.
    //
    //   name:
    //     The System.Xml.Linq.XName to match.
    //
    // Type parameters:
    //   T:
    //     The type of the objects in source, constrained to System.Xml.Linq.XContainer.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XElement
    //     of the child elements of every element and document in the source collection.
    //     Only elements that have a matching System.Xml.Linq.XName are included in
    //     the collection.
    public static IEnumerable<XElement> Elements<T>(this IEnumerable<T> source, XName name) where T : XContainer
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XElement>>() != null);

      return default(IEnumerable<XElement>);
    }

    //
    // Summary:
    //     Returns a collection of nodes that contains all nodes in the source collection,
    //     sorted in document order.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode that
    //     contains the source collection.
    //
    // Type parameters:
    //   T:
    //     The type of the objects in source, constrained to System.Xml.Linq.XNode.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode that
    //     contains all nodes in the source collection, sorted in document order.
    public static IEnumerable<T> InDocumentOrder<T>(this IEnumerable<T> source) where T : XNode
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

      return default(IEnumerable<T>);
    }

    //
    // Summary:
    //     Returns a collection of the child nodes of every document and element in
    //     the source collection.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode that
    //     contains the source collection.
    //
    // Type parameters:
    //   T:
    //     The type of the objects in source, constrained to System.Xml.Linq.XContainer.
    //
    // Returns:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode of
    //     the child nodes of every document and element in the source collection.
    public static IEnumerable<XNode> Nodes<T>(this IEnumerable<T> source) where T : XContainer
    {
      Contract.Requires(source != null);

      Contract.Ensures(Contract.Result<IEnumerable<XNode>>() != null);

      return default(IEnumerable<XNode>);
    }

    //
    // Summary:
    //     Removes every node in the source collection from its parent node.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XNode that
    //     contains the source collection.
    //
    // Type parameters:
    //   T:
    //     The type of the objects in source, constrained to System.Xml.Linq.XNode.
    public static void Remove<T>(this IEnumerable<T> source) where T : XNode
    {
      Contract.Requires(source != null);
    }
    //
    // Summary:
    //     Removes every attribute in the source collection from its parent element.
    //
    // Parameters:
    //   source:
    //     An System.Collections.Generic.IEnumerable<T> of System.Xml.Linq.XAttribute
    //     that contains the source collection.
    public static void Remove(this IEnumerable<XAttribute> source)
    {
      Contract.Requires(source != null);
    }
  }
}
